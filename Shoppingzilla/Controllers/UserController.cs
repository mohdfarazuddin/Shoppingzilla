using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;
using DTOs;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;

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

        [HttpGet("logout")]
        public async Task<IActionResult> logout()
        {
            try
            {
                var authorization = Request.Headers[HeaderNames.Authorization];
                if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
                {
                    var jwt = headerValue.Parameter;
                    await _userservice.logout(jwt);
                }
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

        [HttpGet("isloggedin")]
        public async Task<IActionResult> Isloggedin()
        {
            try
            {
                var authorization = Request.Headers[HeaderNames.Authorization];
                if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
                {
                    var jwt = headerValue.Parameter;
                    if (jwt == "null")
                        return Ok(false);
                    var a= await _userservice.Isloggedin(jwt);
                    return Ok(a);
                }
                return Ok(false);
            }
            catch (Exception e)
            {
                if (e.Message.Contains("token is expired"))
                    return StatusCode(401);
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }

        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            try
            {
                var cat = await _userservice.GetCategory(id);
                return Ok(cat);
            }
            catch (Exception e)
            {
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }

        [HttpGet("subcategory/{id}")]
        public async Task<IActionResult> GetSubCategory(Guid id)
        {
            try
            {
                var scat = await _userservice.GetSubCategory(id);
                return Ok(scat);
            }
            catch (Exception e)
            {
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }

        [HttpGet("brand/{id}")]
        public async Task<IActionResult> GetBrand(Guid id)
        {
            try
            {
                var b = await _userservice.GetBrand(id);
                return Ok(b);
            }
            catch (Exception e)
            {
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }

        [HttpGet("product/{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            try
            {
                var p = await _userservice.GetProduct(id);
                return Ok(p);
            }
            catch (Exception e)
            {
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }

        [HttpGet("model/{id}")]
        public async Task<IActionResult> GetModel(Guid id)
        {
            try
            {
                var m = await _userservice.GetModel(id);
                return Ok(m);
            }
            catch (Exception e)
            {
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }

        [HttpGet("getcategories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var cat = await _userservice.GetCategories();
                return Ok(cat);
            }
            catch (Exception e)
            {
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }

        [HttpGet("getsubcategories/{id}")]
        public async Task<IActionResult> GetSubCategories(Guid id)
        {
            try
            {
                var scat = await _userservice.GetSubCategories(id);
                return Ok(scat);
            }
            catch (Exception e)
            {
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }

        [HttpGet("getbrands/{id}")]
        public async Task<IActionResult> GetBrands(Guid id)
        {
            try
            {
                var brands = await _userservice.GetBrands(id);
                return Ok(brands);
            }
            catch (Exception e)
            {
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }

        [HttpGet("getproducts/{id}")]
        public async Task<IActionResult> GetProducts(Guid id)
        {
            try
            {
                var products = await _userservice.GetProducts(id);
                return Ok(products);
            }
            catch (Exception e)
            {
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }

        [HttpGet("getmodels/{id}")]
        public async Task<IActionResult> GetModels(Guid id)
        {
            try
            {
                var models = await _userservice.GetModels(id);
                return Ok(models);
            }
            catch (Exception e)
            {
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }

        [HttpGet("getproductsbycat/{id}")]
        public async Task<IActionResult> GetProductsbycat(Guid id)
        {
            try
            {
                var products = await _userservice.GetProductsbycat(id);
                return Ok(products);
            }
            catch (Exception e)
            {
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }

        [HttpGet("getproductsbyscat/{id}")]
        public async Task<IActionResult> GetProductsbyscat(Guid id)
        {
            try
            {
                var products = await _userservice.GetProductsbyscat(id);
                return Ok(products);
            }
            catch (Exception e)
            {
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }

        [HttpGet("getproductsbybrand/{id}")]
        public async Task<IActionResult> GetProductsbybrand(Guid id)
        {
            try
            {
                var products = await _userservice.GetProductsbybrand(id);
                return Ok(products);
            }
            catch (Exception e)
            {
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }

        [HttpGet("getproductbyid/{id}")]
        public async Task<IActionResult> GetProductbyid(Guid id)
        {
            try
            {
                var product = await _userservice.GetProductbyid(id);
                return Ok(product);
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
