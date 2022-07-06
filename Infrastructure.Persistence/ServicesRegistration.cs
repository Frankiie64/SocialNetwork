using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Infrastructure.Persistence.Contexts;
using RedSocial.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public static class ServicesRegistration
    {
        //Se le conoce como un Extension Methods - Decorator
        public static void AddPersitsenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Configuration of Database
            if (configuration.GetValue<bool>("UseInMemoryDataBase"))
            {
                //Base de datos de texteo
                services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("DBSocial"));
            }
            else
            {
                // Base de datos en producion

                services.AddDbContext<ApplicationDbContext>(Options =>
                Options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                m => m.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }
            #endregion

            #region Dependency Injection

            //Generics
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //Other repos

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPublicationRepository, PublicationRepository>();
            services.AddTransient<IFriendsRepository,FriendsRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();

            #endregion
        }
    }
}