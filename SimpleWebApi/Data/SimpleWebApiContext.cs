using Microsoft.EntityFrameworkCore;
using SimpleWebApi.Models;

namespace SimpleWebApi.Data
{
    public class SimpleWebApiContext(DbContextOptions<SimpleWebApiContext> options) : DbContext(options)
    {
        public DbSet<User> User { get; set; } = default!;
        public DbSet<Employee> Employee { get; set; } = default!;
    }
}
