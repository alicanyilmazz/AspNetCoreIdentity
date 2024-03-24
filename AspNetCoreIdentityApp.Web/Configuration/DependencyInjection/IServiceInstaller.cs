namespace AspNetCoreIdentityApp.Web.Configuration.DependencyInjection
{
    public interface IServiceInstaller
    {
        void Install(IServiceCollection services, IConfiguration configuration);
    }
}
