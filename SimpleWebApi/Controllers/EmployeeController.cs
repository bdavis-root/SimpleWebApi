using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleWebApi.Data;
using SimpleWebApi.Models;

namespace SimpleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(SimpleWebApiContext context) : ControllerBase
    {
        private readonly SimpleWebApiContext _context = context;

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<(User, Employee)>>> GetEmployee()
        {
            var query = _context.Set<Employee>()
                    .Join(
                        _context.Set<User>(),
                        e => e.UserId,
                        u => u.Id,
                        (e, u) => new { User = u, Employee = e }
                    );

            var anonymousResults = await query.ToListAsync();

            var finalResult = anonymousResults
                .Select(item => (item.User, item.Employee))
                .ToList();

            return Ok(finalResult);
        }
    }
}
