﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rlapi.Data;

namespace rlapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ApiDbContext _dbContext = new ApiDbContext();

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok(_dbContext.Categories);
        }   
    }
}
