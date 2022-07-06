using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Domain.Settings;
using RedSocial.Infrastructure.Shared.Services;

namespace RedSocial.Infrastructure.Shared
{
    public static class ServicesRegistration
    {
        //Se le conoce como un Extension Methods - Decorator
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
