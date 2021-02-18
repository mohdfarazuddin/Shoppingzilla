using DataAccessLayer.Entities;
using DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUser
    {

        Task<UserDTO> RegisterUser(CreateUserDTO user);

        Task<UserDTO> GetUser(string emailid);

        bool UserExists(string emailid);

        Task<UserDTO> loginUser(loginDTO login);

        Task<RefreshTokenDTO> Refresh(RefreshTokenDTO refreshtoken);

        Task logout(string jwt);

        Task<Boolean> Isloggedin(string jwt);

        Task<ModelDTO> GetModel(Guid id);

        Task<List<ModelDTO>> GetModels(Guid id);

        Task<ProductDTO> GetProduct(Guid id);

        Task<List<ProductDTO>> GetProducts(Guid id);

        Task<BrandDTO> GetBrand(Guid id);

        Task<List<BrandDTO>> GetBrands(Guid id);

        Task<SubCategoryDTO> GetSubCategory(Guid id);

        Task<List<SubCategoryDTO>> GetSubCategories(Guid id);

        Task<CategoryDTO> GetCategory(Guid id);

        Task<List<CategoryDTO>> GetCategories();

        Task<List<ProductDTO>> GetProductsbycat(Guid id);

        Task<List<ProductDTO>> GetProductsbyscat(Guid id);

        Task<List<ProductDTO>> GetProductsbybrand(Guid id);

        Task<ProductDTO> GetProductbyid(Guid id);





    }
}
