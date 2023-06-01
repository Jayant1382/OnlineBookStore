using BookStoreBE.Models;
using BookStoreBE.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using System;
using System.Linq;

namespace BookStoreBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly MyDbContext _myDbContext;
        public AdminController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] Users user)
        {
            try
            {
                if (user != null)
                {
                    Users objuser = _myDbContext.users.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
                    if (objuser != null)
                        return Ok();
                    else
                        return NoContent();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your application's requirements
                // For example, you can use logging frameworks like Serilog, NLog, etc.
                Console.WriteLine($"An error occurred: {ex.Message}");

                // Return a server error response
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
