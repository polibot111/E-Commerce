
using E_Commerce.Application.Abstractions.Services;
using E_Commerce.Application.Abstractions.Services.Configuration;
using E_Commerce.Application.Abstractions.Storage;
using E_Commerce.Application.Abstractions.Token;
using E_Commerce.Infrastructure.Enums;
using E_Commerce.Infrastructure.Services;
using E_Commerce.Infrastructure.Services.Configurations;
using E_Commerce.Infrastructure.Services.Storage;
using E_Commerce.Infrastructure.Services.Storage.Local;
using E_Commerce.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure
{
    public static class ServiceRegistiration
    {
        public static void AddInfrestructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
        }

        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T:class, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
            serviceCollection.AddScoped<IMailService, MailService>();
            serviceCollection.AddScoped<IApplicationService, ApplicationService>();
        }

        public static void AddStorage<T>(this IServiceCollection serviceCollection, StorageType storageType)
        {

            switch (storageType)
            {
                case StorageType.Local:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    break;
                case StorageType.AWS:
                    break;
               
                default:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }
    }
}
