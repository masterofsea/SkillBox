using HomeWork6.Archivers;
using HomeWork6.GroupsCalculator;
using HomeWork6.GroupsWriters;
using HomeWork6.NLoaders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HomeWork6
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public IServiceCollection ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<INloader, NFileLoader>()
                .AddSingleton<IGroupsArchiver, ZipDefaultArchiver>()
                .AddSingleton<Fehu>()
                .AddSingleton<IGroupsCounter, LightWeightGroupsCounter>()
                .Configure<NLoaderConfiguration>(Configuration.GetSection("NLoaderConfiguration"))
                .Configure<FehuConfiguration>(Configuration.GetSection("FehuConfiguration"));
        }
    }
}