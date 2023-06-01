using BookStoreBE.Models;
using BookStoreBE.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Transactions;

namespace BookStoreBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] Users user)
        {
            try
            {
                if (user != null)
                {
                    Users objuser = _userRepository.Login(user);
                    if (objuser != null)
                    {
                        return new OkObjectResult(objuser);
                    }
                    else
                        return new NoContentResult();
                }
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your application's requirements
                Console.WriteLine($"An error occurred while logging in: {ex.Message}");

                // Return a server error response
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("Registration")]
        public IActionResult Registration([FromBody] Users user)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    _userRepository.Registration(user);
                    scope.Complete();
                    return new OkObjectResult(user);
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your application's requirements
                Console.WriteLine($"An error occurred while registering user: {ex.Message}");

                // Return a server error response
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
