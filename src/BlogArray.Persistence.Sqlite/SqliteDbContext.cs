using Microsoft.EntityFrameworkCore;

namespace BlogArray.Persistence.Sqlite;

public class SqliteDbContext(DbContextOptions<SqliteDbContext> options) : AppDbContext(options)
{

}
