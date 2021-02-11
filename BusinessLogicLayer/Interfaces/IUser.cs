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

        Task logout(Guid id);
    }
}
