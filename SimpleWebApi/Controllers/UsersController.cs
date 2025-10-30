using Azure.Core.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleWebApi.Data;
using SimpleWebApi.Models;

namespace SimpleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(SimpleWebApiContext context) : ControllerBase
    {
        private readonly SimpleWebApiContext _context = context;

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/Users/2
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        // Example error. Should not provide stack trace to caller due to GlogalExceptionHandler.
        [HttpGet("error")]
        public Task<ActionResult<User>> ThrowError()
        {
            throw new Exception("This is an example error.");
        }
    }
}
