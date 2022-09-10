using Eskisehirspor.Application.Common.Behaviours;
using Eskisehirspor.Application.Common.Identity;
using Eskisehirspor.Application.Common.Interfaces;
using Eskisehirspor.Application.UseCases.Feed.LatestThreads.Worker;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Eskisehirspor.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IIdentityManager, IdentityManager>();

        }
    }
}
