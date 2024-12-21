using API_.NET_CRUD_Minimal_E.F_ORM.Models;
using Microsoft.EntityFrameworkCore;

namespace API_.NET_CRUD_Minimal_E.F_ORM.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
