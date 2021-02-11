using DataAccessLayer.Entities;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public static class Extensions
    {

        public static string Hash(this string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hash;
            }
        }

        public static string GenerateRefreshToken(this byte[] rnd)
        {
            var randomnumber = rnd;
            using (var randomnumberGenerator = RandomNumberGenerator.Create())
            {
                randomnumberGenerator.GetBytes(randomnumber);
                return Convert.ToBase64String(randomnumber);
            }
        }

        public static RoleDTO AsDTO(this Roles role)
        {
            return new RoleDTO
            {
                Id = role.Id,
                RoleType = role.RoleType
            };
        }

        public static Roles AsModel(this CreateRoleDTO role)
        {
            return new Roles
            {
                RoleType = role.RoleType
            };
        }

        public static UserDTO AsDTO(this UserDetails user)
        {
            return new UserDTO
            {
                UserID = user.UserID,
                EmailID = user.Logincredential.EmailID,
                RoleID = user.RoleID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MobileNo = user.MobileNo
            };
        }


        public static UserDetails AsModel(this CreateUserDTO user)
        {
            return new UserDetails
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                MobileNo = user.MobileNo
            };
        }

        public static Categories AsModel(this CreateCategoryDTO category)
        {
            return new Categories
            {
                Name = category.Name
            };
        }

        public static CategoryDTO AsDTO(this Categories category)
        {
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public static SubCategories AsModel(this CreateSubCategoryDTO subcategory)
        {
            return new SubCategories
            {
                CategoryID = subcategory.CategoryID,
                Name = subcategory.Name
            };
        }

        public static SubCategoryDTO AsDTO(this SubCategories subcategory)
        {
            return new SubCategoryDTO
            {
                Id = subcategory.Id,
                CategoryID = subcategory.CategoryID,
                Name = subcategory.Name
            };
        }

        public static Brands AsModel(this CreateBrandDTO brand)
        {
            return new Brands
            {
                SubCategoryID = brand.SubCategoryID,
                Name = brand.Name
            };
        }

        public static BrandDTO AsDTO(this Brands brand)
        {
            return new BrandDTO
            {
                Id = brand.Id,
                SubCategoryID = brand.SubCategoryID,
                Name = brand.Name
            };
        }

        public static Products AsModel(this CreateProductDTO product)
        {
            return new Products
            {
                BrandID = product.BrandID,
                Name = product.Name,
                Description = product.Description
            };
        }

        public static ProductDTO AsDTO(this Products product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                BrandID = product.BrandID,
                Name = product.Name,
                Description = product.Description
            };
        }

        public static Models AsModel(this CreateModelDTO model)
        {
            return new Models
            {
                ProductID = model.ProductID,
                Colour = model.Colour,
                Size = model.Size,
                Stock = model.Stock,
                Price = model.Price
            };
        }

        public static ModelDTO AsDTO(this Models model)
        {
            return new ModelDTO
            {
                Id = model.Id,
                ProductID = model.ProductID,
                Colour = model.Colour,
                Size = model.Size,
                Stock = model.Stock,
                Price = model.Price
            };
        }

        public class PageList<T> : List<T>
        {

            public int CurrentPage { get; private set; }
            public int TotalPages { get; private set; }
            public int PageSize { get; private set; } = 20;
            public int TotalCount { get; private set; }
            public bool HasPrevious => CurrentPage > 1;
            public bool HasNext => CurrentPage < TotalPages;

            public PageList(List<T> items, int count, int pageNumber)
            {
                TotalCount = count;
                PageSize = 20;
                CurrentPage = pageNumber;
                TotalPages = (int)Math.Ceiling(count / (double)PageSize);

                AddRange(items);
            }

            public static PageList<T> ToPagedList(IQueryable<T> source, int pageNumber)
            {
                var pageSize = 20;
                var count = source.Count();
                var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                return new PageList<T>(items, count, pageNumber);
            }

        }


    }
}
