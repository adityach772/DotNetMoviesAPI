using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly searchmoviesContext _context;

        public AuthController(searchmoviesContext context)
        {
            _context = context;
        }

        // POST: api/Auth/SignIn
        [HttpPost("SignIn")]
        public async Task<ActionResult<string>> SignIn(SignIn credentials)
        {
            try
            {
                // Find user by username/email and password
                var user = await _context.Registrations.FirstOrDefaultAsync(u =>
                    (u.Uname == credentials.Uname || u.Email == credentials.Email) &&
                    u.Passwrd == credentials.Passwrd);

                // If user is found, return 200 with success message
                if (user != null)
                {
                    return Ok("Authentication successful.");
                }
                else
                {
                    // If user is not found, return 401 with error message
                    return Unauthorized("Invalid username/email or password.");
                }
            }
            catch (Exception ex)
            {
                // If an error occurs, return 500 with error message
                return StatusCode(StatusCodes.Status500InternalServerError, "Error during authentication: " + ex.Message);
            }
        }
    }
}
