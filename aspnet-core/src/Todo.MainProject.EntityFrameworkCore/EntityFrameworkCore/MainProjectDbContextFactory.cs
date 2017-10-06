using Todo.MainProject.Configuration;
using Todo.MainProject.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Todo.MainProject.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class MainProjectDbContextFactory : IDesignTimeDbContextFactory<MainProjectDbContext>
    {
        public MainProjectDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MainProjectDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            MainProjectDbContextConfigurer.Configure(builder, configuration.GetConnectionString(MainProjectConsts.ConnectionStringName));

            return new MainProjectDbContext(builder.Options);
        }
    }
}