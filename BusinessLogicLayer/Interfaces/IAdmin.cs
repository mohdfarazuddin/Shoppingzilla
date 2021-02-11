using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogicLayer.Extensions;

namespace BusinessLogicLayer.Interfaces
{
    public interface IAdmin
    {

        Task<List<RoleDTO>> GetRoles();

        Task<RoleDTO> GetRole(int id);

        Task<RoleDTO> CreateRole(CreateRoleDTO role);

        Task<UserDTO> UpdateUserRole(Guid id, int roleid);

        PageList<UserDTO> GetUsers(int page);

        Task<CategoryDTO> CreateCategory(CreateCategoryDTO category);

        Task<CategoryDTO> GetCategory(Guid id);

        Task<SubCategoryDTO> CreateSubCategory(CreateSubCategoryDTO subcategory);

        Task<SubCategoryDTO> GetSubCategory(Guid id);

        Task<BrandDTO> CreateBrand(CreateBrandDTO brand);

        Task<BrandDTO> GetBrand(Guid id);

        Task<ProductDTO> CreateProduct(CreateProductDTO product);

        Task<ProductDTO> GetProduct(Guid id);

        Task<ModelDTO> CreateModel(CreateModelDTO model);

        Task<ModelDTO> GetModel(Guid id);



        bool RoleExists(int id);

    }
}
