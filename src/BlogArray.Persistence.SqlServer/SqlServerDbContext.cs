using Microsoft.EntityFrameworkCore;
namespace BlogArray.Persistence.SqlServer;

public class SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : AppDbContext(options)
{

}
