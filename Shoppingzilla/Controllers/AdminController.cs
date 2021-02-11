using BusinessLogicLayer.Interfaces;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppingzilla.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Roles = "3")]
    public class AdminController : ControllerBase
    {
        IAdmin _registerservice;

        public AdminController(IAdmin registerservice)
        {
            _registerservice = registerservice;
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var role = await _registerservice.GetRoles();
                return Ok(role);
            }
            catch (Exception e)
            {
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }

        [HttpGet("roles/{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            try
            {
                var role = await _registerservice.GetRole(id);
                return Ok(role);
            }
            catch (Exception e)
            {
                if (!_registerservice.RoleExists(id))
                    return NotFound();
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }

        [HttpPost("createrole")]
        public async Task<IActionResult> CreateRole(CreateRoleDTO role)
        {
            try
            {
                var createrole = await _registerservice.CreateRole(role);
                return CreatedAtAction(nameof(GetRole), new { id = createrole.Id }, createrole);
            }
            catch (Exception e)
            {
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }

        [HttpPost("updateuserrole")]
        public async Task<IActionResult> UpdateUserRole(Guid id, int roleid)
        {
            try
            {
                var user = await _registerservice.UpdateUserRole(id, roleid);
                return Ok(user);
            }
            catch (Exception e)
            {
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }

        
        [HttpGet("users")]
        public IActionResult GetUsers([FromQuery] int page = 1)
        {
            var users = _registerservice.GetUsers(page);

            var metadata = new
            {
                users.TotalCount,
                users.PageSize,
                users.CurrentPage,
                users.TotalPages,
                users.HasNext,
                users.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(users);
        }

        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            try
            {
                var cat = await _registerservice.GetCategory(id);
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


        [HttpPost("createcategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO category)
        {
            try
            {
                var cat = await _registerservice.CreateCategory(category);
                return CreatedAtAction(nameof(GetCategory), new { id = cat.Id }, cat);
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
                var scat = await _registerservice.GetSubCategory(id);
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


        [HttpPost("createsubcategory")]
        public async Task<IActionResult> CreateSubCategory(CreateSubCategoryDTO scategory)
        {
            try
            {
                var scat = await _registerservice.CreateSubCategory(scategory);
                return CreatedAtAction(nameof(GetSubCategory), new { id = scat.Id }, scat);
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
                var b = await _registerservice.GetBrand(id);
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


        [HttpPost("createbrand")]
        public async Task<IActionResult> CreateBrand(CreateBrandDTO brand)
        {
            try
            {
                var b = await _registerservice.CreateBrand(brand);
                return CreatedAtAction(nameof(GetBrand), new { id = b.Id }, b);
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
                var p = await _registerservice.GetProduct(id);
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


        [HttpPost("createproduct")]
        public async Task<IActionResult> CreateProduct(CreateProductDTO product)
        {
            try
            {
                var p = await _registerservice.CreateProduct(product);
                return CreatedAtAction(nameof(GetProduct), new { id = p.Id }, p);
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
                var m = await _registerservice.GetModel(id);
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


        [HttpPost("createmodel")]
        public async Task<IActionResult> CreateModel(CreateModelDTO model)
        {
            try
            {
                var m = await _registerservice.CreateModel(model);
                return CreatedAtAction(nameof(GetModel), new { id = m.Id }, m);
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
