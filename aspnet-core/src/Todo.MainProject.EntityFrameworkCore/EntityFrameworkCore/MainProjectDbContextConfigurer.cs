using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Todo.MainProject.EntityFrameworkCore
{
    public static class MainProjectDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<MainProjectDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<MainProjectDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}