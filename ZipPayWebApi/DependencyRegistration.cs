using Microsoft.Extensions.DependencyInjection;
using ZipPay.DAL;
using ZipPayWebApp.BAL;

namespace ZipPayWebApp
{
    public class DependencyRegistration
    {
        public static void RegisterApplicationServices(IServiceCollection services)
        {
            services.AddScoped<IUsersService, UsersService>()
                    .AddScoped<IAccountsService, AccountsService>()
                    .AddScoped<IUsersRepo, UsersRepo>()
                    .AddScoped<IAccountsRepo, AccountsRepo>();
        }
    }
}
