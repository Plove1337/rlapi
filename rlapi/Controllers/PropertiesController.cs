using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rlapi.Data;
using rlapi.Models;
using System.Security.Claims;


namespace rlapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        ApiDbContext _dbContext = new ApiDbContext();

        [HttpGet("PropertyList")]
        [Authorize]
        public IActionResult GetProperties(int categoryId)
        {
            var propertiesResult = _dbContext.Properties.Where(c => c.CategoryID == categoryId);
            if(propertiesResult == null)
            {
                return NotFound();
            }
            return Ok(propertiesResult);
        }

        [HttpGet("PropertyDetail")]
        [Authorize]
        public IActionResult GetDetail(int id)
        {
            var propertiesResult = _dbContext.Properties.FirstOrDefault(p=>p.Id == id);
            if (propertiesResult == null)
            {
                return NotFound();
            }
            return Ok(propertiesResult);
        }

        [HttpGet("TrendingProperties")]
        [Authorize]
        public IActionResult GetTrendingProperties()
        {
            var propertiesResult = _dbContext.Properties.Where(c => c.isTrending == true);
            if (propertiesResult == null)
            {
                return NotFound();
            }
            return Ok(propertiesResult);
        }

        [HttpGet("SearchProperties")]
        [Authorize]

        public IActionResult GetSearchProperties(string address)
        {
            var propertiesResult = _dbContext.Properties.Where(p=>p.Address.Contains(address));
            if (propertiesResult == null)
            {
                return NotFound();
            }
            return Ok(propertiesResult);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] Property property)
        {
            if (property == null)
            {
                return NoContent();
            }
            else
            {
                var UserEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var user = _dbContext.Users.FirstOrDefault(u => u.Email == UserEmail);
                if (user == null) return StatusCode(StatusCodes.Status404NotFound);
                property.isTrending = false;
                property.UserID = user.Id;
                _dbContext.Properties.Add(property);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }

        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] Property property)
        {
            var propertyResult = _dbContext.Properties.FirstOrDefault(p => p.Id == id);
            if (propertyResult == null)
            {
                return NoContent();
            }
            else
            {
                var UserEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var user = _dbContext.Users.FirstOrDefault(u => u.Email == UserEmail);
                if (user == null) return StatusCode(StatusCodes.Status404NotFound);
                if (propertyResult.UserID == user.Id)
                {
                    propertyResult.Name = property.Name;
                    propertyResult.Detal = property.Detal;
                    propertyResult.Price = property.Price;
                    propertyResult.Address = property.Address;
                    property.isTrending = false;
                    property.UserID = user.Id;
                    _dbContext.SaveChanges();
                    return Ok("Record has been updated succesfully");
                }
                return BadRequest();
            }

        }
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var propertyResult = _dbContext.Properties.FirstOrDefault(p => p.Id == id);
            if (propertyResult == null)
            {
                return NoContent();
            }
            else
            {
                var UserEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var user = _dbContext.Users.FirstOrDefault(u => u.Email == UserEmail);
                if (user == null) return StatusCode(StatusCodes.Status404NotFound);
                if (propertyResult.UserID == user.Id)
                {   
                    _dbContext.Properties.Remove(propertyResult);
                    _dbContext.SaveChanges();
                    return Ok("Record has been deleted succesfully");
                }
                return BadRequest();

            }
        }
    }
}
