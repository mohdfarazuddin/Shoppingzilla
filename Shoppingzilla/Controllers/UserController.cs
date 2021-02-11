using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;
using DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyStore.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        IUser _userservice;

        public UserController(IUser userservice)
        {
            _userservice = userservice;
        }

        [HttpGet("{emailid}")]
        public async Task<IActionResult> GetUser(string emailid)
        {
            try
            {
                var user = await _userservice.GetUser(emailid);
                return Ok(user);
            }
            catch (Exception e)
            {
                if (!_userservice.UserExists(emailid))
                    return NotFound();
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserDTO user)
        {
            try
            {
                var login = await _userservice.RegisterUser(user);
                return CreatedAtAction(nameof(GetUser), new { emailid = login.EmailID}, login);
            }
            catch (Exception e)
            {
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(loginDTO user)
        {
            try
            {
                var login = await _userservice.loginUser(user);
                return Ok(login);
            }
            catch (Exception e)
            {
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }

        [HttpPost("logout/{id}")]
        public async Task<IActionResult> logout(Guid id)
        {
            try
            {
                await _userservice.logout(id);
                return Ok();
            }
            catch (Exception e)
            {
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }


        [HttpPost("refresh")]
        public async Task<IActionResult> refresh(RefreshTokenDTO refreshtoken)
        {
            try
            {
                var token = await _userservice.Refresh(refreshtoken);
                return Ok(token);
            }
            catch (Exception e)
            {
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }


    }
}
