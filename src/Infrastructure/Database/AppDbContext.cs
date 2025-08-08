using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

internal sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{

}
