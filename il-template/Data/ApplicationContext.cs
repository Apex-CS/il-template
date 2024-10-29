using il_template.Models;

using Microsoft.EntityFrameworkCore;

namespace il_template.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext()
    {
        
    }

    public ApplicationContext(DbContextOptions options) : base(options)
    {
        
    }

    public virtual DbSet<User> Users { get; set; }
}