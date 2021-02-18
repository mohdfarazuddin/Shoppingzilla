using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BusinessLogicLayer.Interfaces;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Shoppingzilla.Controllers
{
    [Route("api/admin")]
    [ApiController]
    
    public class AdminController : ControllerBase
    {
        IAdmin _adminservice;
        IUser _userservice;

        private readonly BlobServiceClient _blobServiceClient;
        public AdminController(IAdmin adminservice, IUser userservice, BlobServiceClient blobServiceClient)
        {
            _adminservice = adminservice;
            _userservice = userservice;
            _blobServiceClient = blobServiceClient;
        }

        [Authorize(Roles = "3")]
        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var role = await _adminservice.GetRoles();
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

        [Authorize(Roles = "3")]
        [HttpGet("roles/{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            try
            {
                var role = await _adminservice.GetRole(id);
                return Ok(role);
            }
            catch (Exception e)
            {
                if (!_adminservice.RoleExists(id))
                    return NotFound();
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }

        [Authorize(Roles = "3")]
        [HttpPost("createrole")]
        public async Task<IActionResult> CreateRole(CreateRoleDTO role)
        {
            try
            {
                var createrole = await _adminservice.CreateRole(role);
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

        [Authorize(Roles = "3")]
        [HttpGet("updateuserrole")]
        public async Task<IActionResult> UpdateUserRole(Guid id, int roleid)
        {
            try
            {
                var user = await _adminservice.UpdateUserRole(id, roleid);
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

        [Authorize(Roles = "3")]
        [HttpGet("users")]
        public IActionResult GetUsers([FromQuery] int page = 1)
        {
            var users = _adminservice.GetUsers(page);

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

        [Authorize(Roles = "3")]
        [HttpPost("createcategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO category)
        {
            try
            {
                var cat = await _adminservice.CreateCategory(category);
                return StatusCode(201, cat);
            }
            catch (Exception e)
            {
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }

        [Authorize(Roles = "3")]
        [HttpPost("createsubcategory")]
        public async Task<IActionResult> CreateSubCategory(CreateSubCategoryDTO scategory)
        {
            try
            {
                var scat = await _adminservice.CreateSubCategory(scategory);
                return StatusCode(201, scat);
            }
            catch (Exception e)
            {
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }

        [Authorize(Roles = "3")]
        [HttpPost("createbrand")]
        public async Task<IActionResult> CreateBrand(CreateBrandDTO brand)
        {
            try
            {
                var b = await _adminservice.CreateBrand(brand);
                return StatusCode(201, b);
            }
            catch (Exception e)
            {
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }

        [Authorize(Roles = "3")]
        [HttpPost("createproduct")]
        public async Task<IActionResult> CreateProduct(CreateProductDTO product)
        {
            try
            {
                var p = await _adminservice.CreateProduct(product);
                return StatusCode(201, p);
            }
            catch (Exception e)
            {
                if (e.Message.Length > 0)
                    return BadRequest(e.Message);
                else
                    throw;
            }
        }

        [Authorize(Roles = "3")]
        [HttpPost("createmodel")]
        public async Task<IActionResult> CreateModel([FromForm] CreateModelDTO formmodel)
        {
            try
            {
                IFormFile file = Request.Form.Files[0];
                var containerClient = _blobServiceClient.GetBlobContainerClient("images");
                var blobClient = containerClient.GetBlobClient(file.FileName);
                await blobClient.UploadAsync(file.OpenReadStream(), new BlobHttpHeaders { ContentType = file.ContentType });
                var url = blobClient.Uri.AbsoluteUri;
                formmodel.imgurl = url;
                var m = await _adminservice.CreateModel(formmodel);
                return StatusCode(201, m);
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
