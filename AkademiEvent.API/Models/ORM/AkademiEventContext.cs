using Microsoft.EntityFrameworkCore;

namespace AkademiEvent.API.Models.ORM;

public class AkademiEventContext:DbContext
{
    public AkademiEventContext(DbContextOptions<AkademiEventContext> options) : base(options)
    {

    }
    public DbSet<Category> Categories { get; set; }
}
