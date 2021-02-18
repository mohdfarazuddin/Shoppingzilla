using Azure.Storage.Blobs;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.DataContext;
using DataAccessLayer.Entities;
using DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogicLayer.Extensions;

namespace BusinessLogicLayer.Functions
{
    public class Admin : IAdmin
    {

        private readonly ShoppingzillaDbContext _context = new ShoppingzillaDbContext(ShoppingzillaDbContext.ops.dbOptions);

        

        public bool RoleExists(int id)
        {
            return _context.Roles.Any(r => r.Id == id);
        }

        public async Task<RoleDTO> GetRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role is null)
                throw new Exception();
            return role.AsDTO();
        }

        public async Task<List<RoleDTO>> GetRoles()
        {
            var roles = await _context.Roles.Select(r => r.AsDTO()).ToListAsync();
            return roles;
        }

        public async Task<RoleDTO> CreateRole(CreateRoleDTO role)
        {
            if (role.RoleType is null)
                throw new Exception("RoleType cannot be empty");
            Roles roles = role.AsModel();
            await _context.Roles.AddAsync(roles);
            await _context.SaveChangesAsync();
            return roles.AsDTO();
        }

        public async Task<UserDTO> UpdateUserRole(Guid id, int roleid)
        {
            var user = await _context.UserDetails.FindAsync(id);
            if (user == null)
                throw new Exception("User Not Found");
            user.RoleID = roleid;
            await _context.SaveChangesAsync();
            user = await _context.UserDetails.Include(u => u.Logincredential)
                                        .Include(u => u.Role).FirstOrDefaultAsync(u => u.UserID == id);
            return user.AsDTO();
        }

        public PageList<UserDTO> GetUsers(int page = 1)
        {
            var users = PageList<UserDTO>.ToPagedList(_context.UserDetails
                                                .Include(u => u.Logincredential)
                                                .Include(u => u.Role).Select(u => new UserDTO()
                                                {
                                                    UserID = u.UserID,
                                                    EmailID = u.Logincredential.EmailID,
                                                    FirstName = u.FirstName,
                                                    LastName = u.LastName,
                                                    MobileNo = u.MobileNo,
                                                    RoleID = u.RoleID
                                                })
                                                .OrderBy(u => u.FirstName), page);
            return users;
        }

        public async Task<CategoryDTO> CreateCategory(CreateCategoryDTO category)
        {
            if (category.Name.Length < 3)
                throw new Exception("Category Name should be atleast 3 characters long.");
            var cat = category.AsModel();
            await _context.Categories.AddAsync(cat);
            await _context.SaveChangesAsync();
            return cat.AsDTO();
        }

        public async Task<SubCategoryDTO> CreateSubCategory(CreateSubCategoryDTO subcategory)
        {
            if (subcategory.Name.Length < 3)
                throw new Exception("Sub-Category Name should be atleast 3 characters long.");
            var scat = subcategory.AsModel();
            await _context.SubCategories.AddAsync(scat);
            await _context.SaveChangesAsync();
            return scat.AsDTO();
        }

        public async Task<BrandDTO> CreateBrand(CreateBrandDTO brand)
        {
            if (brand.Name.Length < 2)
                throw new Exception("Brand Name should be atleast 2 characters long.");
            var b = brand.AsModel();
            await _context.Brands.AddAsync(b);
            await _context.SaveChangesAsync();
            return b.AsDTO();
        }

        public async Task<ProductDTO> CreateProduct(CreateProductDTO product)
        {
            if (product.Name.Length < 3)
                throw new Exception("Product Name should be atleast 3 characters long.");
            if (product.Description.Length < 5)
                throw new Exception("Product Description should be atleast 5 characters long.");
            var p = product.AsModel();
            await _context.Products.AddAsync(p);
            await _context.SaveChangesAsync();
            return p.AsDTO();
        }

        public async Task<ModelDTO> CreateModel(CreateModelDTO model)
        {
            if (model.Price == 0 )
                throw new Exception("Price cannot be 0.");
            var m = model.AsModel();
            await _context.Models.AddAsync(m);
            await _context.SaveChangesAsync();
            return m.AsDTO();
        }

        

    }
}
