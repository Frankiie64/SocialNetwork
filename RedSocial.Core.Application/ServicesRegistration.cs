using Microsoft.Extensions.DependencyInjection;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application
{
    public static class ServicesRegistration
    {
        //Se le conoce como un Extension Methods - Decorator
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            #region AutoMapper

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #endregion
            #region Services 

            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<IUserService, UserServices>();
            services.AddTransient<IPublicationService, PublicationServices>();
            services.AddTransient<IFriendsService, FriendService>();
            services.AddTransient<ICommentService, CommentService>();


            #endregion

        }
    }
}
