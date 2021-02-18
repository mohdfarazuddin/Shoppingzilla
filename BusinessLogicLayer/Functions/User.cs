using DataAccessLayer.Entities;
using DataAccessLayer.DataContext;
using BusinessLogicLayer;
using System;
using System.Linq;
using System.Threading.Tasks;
using DTOs;
using BusinessLogicLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Collections.Generic;

namespace BusinessLogicLayer.Functions
{
    public class User : IUser
    {

        private readonly ShoppingzillaDbContext _context = new ShoppingzillaDbContext(ShoppingzillaDbContext.ops.dbOptions);

        private string key;

        public User(string key)
        {
            this.key = key;
        }

        public bool UserExists(string emailid)
        {
            return _context.LoginDetails.Any(l => l.EmailID == emailid);
        }


        public async Task<UserDTO> GetUser(string emailid)
        {
            var user = await _context.UserDetails.Include(u => u.Role).Include(u => u.Logincredential)
                                      .FirstOrDefaultAsync(u => u.Logincredential.EmailID == emailid);
            if (user == null)
                throw new Exception("User not Found");
            return user.AsDTO();
        }

        public async Task<UserDTO> RegisterUser(CreateUserDTO user)
        {
            if (user.password != user.password2)
                throw new Exception("Passwords donot match.");
            if(user.FirstName.Trim().Length < 3)
                throw new Exception("FirstName must be atleast 3 characters long");
            if(user.LastName.Trim().Length < 3)
                throw new Exception("LastName must be atleast 3 characters long");
            if (user.password.Trim().Length < 6)
                throw new Exception("Password must be atleast 6 characters long");
            if (user.password.Contains(" "))
                throw new Exception("Password should not contain spaces");
            if (user.MobileNo.Length != 10)
                throw new Exception("Mobile Number should be 10 digits.");
            var login = new LoginCredentials();
            login.PasswordHash = user.password.Hash();
            var roleid = await _context.Roles.Where(r => r.RoleType == "User").Select(r => r.Id).FirstOrDefaultAsync();
            login.User = user.AsModel();
            login.User.RoleID = roleid;
            login.EmailID = user.EmailID;
            await _context.LoginDetails.AddAsync(login);
            await _context.SaveChangesAsync();
            var newuser = await _context.UserDetails.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserID == login.User.UserID);
            var userdto = newuser.AsDTO();
            var token = await GenerateToken(newuser.UserID);
            userdto.JwtToken = token.JwtToken;
            userdto.RefreshToken = token.RefreshToken;
            return userdto;
        }

        public async Task<UserDTO> loginUser(loginDTO login)
        {
            if (!UserExists(login.EmailID))
                throw new Exception("User does not exist");
            var passhash = login.Password.Hash();
            var user = await _context.UserDetails.Include(u => u.Logincredential)
                                        .Include(u => u.Role).FirstOrDefaultAsync(u => u.Logincredential.EmailID == login.EmailID);
            if (user.Logincredential.PasswordHash != passhash)
                throw new Exception("Incorrect Id or Password");
            UserDTO userdto = user.AsDTO();
            var token = await GenerateToken(user.UserID);
            userdto.JwtToken = token.JwtToken;
            userdto.RefreshToken = token.RefreshToken;
            return userdto; 
        }

        private async Task<RefreshTokenDTO> GenerateToken(Guid id)
        {
            var user = await _context.UserDetails.Include(u => u.Logincredential)
                                        .Include(u => u.Role).FirstOrDefaultAsync(u => u.UserID == id);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", user.UserID.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            var jwtToken = tokenHandler.WriteToken(token);
            var rnd = new byte[32];
            var refreshToken = rnd.GenerateRefreshToken();
            RefreshTokens refreshtoken = new()
            {
                RefreshToken = refreshToken,
                UserID = user.UserID
            };
            bool exist = _context.RefreshTokens.Any(r => r.UserID == refreshtoken.UserID);
            if (!exist)
                await _context.RefreshTokens.AddAsync(refreshtoken);
            else if (exist)
            {
                var rt = await _context.RefreshTokens.FirstOrDefaultAsync(r => r.UserID == refreshtoken.UserID);
                rt.RefreshToken = refreshToken;
            }
            await _context.SaveChangesAsync();
            return new RefreshTokenDTO
            {
                JwtToken = jwtToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<RefreshTokenDTO> Refresh(RefreshTokenDTO refreshtoken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken;
            var principal = tokenHandler.ValidateToken(refreshtoken.JwtToken,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false
                }, out validatedToken );
            var jwtToken = validatedToken as JwtSecurityToken;
            if(jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Token is Invalid");

            var userid = jwtToken.Claims.ToList()[0].Value;
            var rt = await _context.RefreshTokens.FindAsync(new Guid(userid));
            if (refreshtoken.RefreshToken != rt.RefreshToken)
                throw new SecurityTokenException("Refresh Token not valid");
            RefreshTokenDTO refreshToken = await GenerateToken(new Guid(userid));
            return refreshToken;
        }

        public async Task logout(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken;
            var principal = tokenHandler.ValidateToken(jwt,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                }, out validatedToken);
            var jwtToken = validatedToken as JwtSecurityToken;
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Token is Invalid");
            var id = jwtToken.Claims.ToList()[0].Value;
            var rt = await _context.RefreshTokens.FindAsync(new Guid(id));
            _context.RefreshTokens.Remove(rt);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task<Boolean> Isloggedin(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken;
            var principal = tokenHandler.ValidateToken(jwt,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                }, out validatedToken);
            var jwtToken = validatedToken as JwtSecurityToken;
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Token is Invalid");
            var id = jwtToken.Claims.ToList()[0].Value;
            var u = await _context.RefreshTokens.FindAsync(new Guid(id));
            if(u == null)
                return false;
            return true;
        }

        public async Task<CategoryDTO> GetCategory(Guid id)
        {
            var cat = await _context.Categories.FindAsync(id);
            if (cat == null)
                throw new Exception("Category not Found");
            return cat.AsDTO();
        }

        public async Task<SubCategoryDTO> GetSubCategory(Guid id)
        {
            var scat = await _context.SubCategories.FindAsync(id);
            if (scat == null)
                throw new Exception("Sub-Category not Found");
            return scat.AsDTO();
        }

        public async Task<BrandDTO> GetBrand(Guid id)
        {
            var b = await _context.Brands.FindAsync(id);
            if (b == null)
                throw new Exception("Brand not Found");
            return b.AsDTO();
        }

        public async Task<ProductDTO> GetProduct(Guid id)
        {
            var p = await _context.Products.FindAsync(id);
            if (p == null)
                throw new Exception("Brand not Found");
            return p.AsDTO();
        }

        public async Task<ModelDTO> GetModel(Guid id)
        {
            var m = await _context.Models.FindAsync(id);
            if (m == null)
                throw new Exception("Brand not Found");
            return m.AsDTO();
        }

        public async Task<List<CategoryDTO>> GetCategories()
        {
            var categories = await _context.Categories.OrderBy(c => c.Name).Select(c => c.AsDTO()).ToListAsync();
            return categories;
        }

        public async Task<List<SubCategoryDTO>> GetSubCategories(Guid id)
        {
            var subcategories = await _context.SubCategories.Where(sc => sc.CategoryID == id)
                                    .OrderBy(sc => sc.Name).Select(sc => sc.AsDTO()).ToListAsync();
            return subcategories;
        }

        public async Task<List<BrandDTO>> GetBrands(Guid id)
        {
            var brands = await _context.Brands.Where(b => b.SubCategoryID == id)
                                    .OrderBy(b => b.Name).Select(b => b.AsDTO()).ToListAsync();
            return brands;
        }

        public async Task<List<ProductDTO>> GetProducts(Guid id)
        {
            var products = await _context.Products.Where(p => p.BrandID == id)
                                    .OrderBy(p => p.Name).Select(p => p.AsDTO()).ToListAsync();
            return products;
        }

        public async Task<List<ModelDTO>> GetModels(Guid id)
        {
            var models = await _context.Models.Where(m => m.ProductID == id)
                                    .OrderBy(m => m.Price).Select(m => m.AsDTO()).ToListAsync();
            return models;
        }

        public async Task<List<ProductDTO>> GetProductsbycat(Guid id)
        {
            var products = await _context.Products.Where(p => p.CategoryID == id).Include(p => p.Models)
                                    .OrderBy(p => p.Name).Select(p => p.AsDTO()).ToListAsync();
            return products;
        }

        public async Task<List<ProductDTO>> GetProductsbyscat(Guid id)
        {
            var products = await _context.Products.Where(p => p.SubCategoryID == id).Include(p => p.Models)
                                    .OrderBy(p => p.Name).Select(p => p.AsDTO()).ToListAsync();
            return products;
        }

        public async Task<List<ProductDTO>> GetProductsbybrand(Guid id)
        {
            var products = await _context.Products.Where(p => p.BrandID == id).Include(p => p.Models)
                                    .OrderBy(p => p.Name).Select(p => p.AsDTO()).ToListAsync();
            return products;
        }

        public async Task<ProductDTO> GetProductbyid(Guid id)
        {
            var product = await _context.Products.Include(p => p.Models).FirstOrDefaultAsync(p => p.Id == id);
            return product.AsDTO();
        }

    }
}
