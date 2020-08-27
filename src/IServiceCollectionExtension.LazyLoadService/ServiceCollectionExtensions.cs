﻿using System;
using Microsoft.Extensions.DependencyInjection;

namespace IServiceCollectionExtension.LazyLoadService
{
    public static class ServiceCollectionExtensions
    {
        #region AddLazy

        public static IServiceCollection AddLazy<TService, TImplementation>(this IServiceCollection services,
            ServiceLifetime serviceLifetime)
            where TService : class
            where TImplementation : class, TService
        {
            return serviceLifetime switch
            {
                ServiceLifetime.Singleton => services.AddLazySingleton<TService, TImplementation>(),
                ServiceLifetime.Scoped => services.AddLazyScoped<TService, TImplementation>(),
                ServiceLifetime.Transient => services.AddLazyTransient<TService, TImplementation>(),
                _ => throw new ArgumentOutOfRangeException(nameof(serviceLifetime), serviceLifetime, null)
            };
        }

        public static IServiceCollection AddLazy<TService>(this IServiceCollection services,
            ServiceLifetime serviceLifetime)
            where TService : class
        {
            return serviceLifetime switch
            {
                ServiceLifetime.Singleton => services.AddLazySingleton<TService>(),
                ServiceLifetime.Scoped => services.AddLazyScoped<TService>(),
                ServiceLifetime.Transient => services.AddLazyTransient<TService>(),
                _ => throw new ArgumentOutOfRangeException(nameof(serviceLifetime), serviceLifetime, null)
            };
        }

        public static IServiceCollection AddLazy<TService>(
            this IServiceCollection services,
            Func<IServiceProvider, TService> implementationFactory, ServiceLifetime serviceLifetime)
            where TService : class

        {
            return serviceLifetime switch
            {
                ServiceLifetime.Singleton => services.AddLazySingleton<TService>(implementationFactory),
                ServiceLifetime.Scoped => services.AddLazyScoped<TService>(implementationFactory),
                ServiceLifetime.Transient => services.AddLazyTransient<TService>(implementationFactory),
                _ => throw new ArgumentOutOfRangeException(nameof(serviceLifetime), serviceLifetime, null)
            };
        }

        public static IServiceCollection AddLazy<TService, TImplementation>(
            this IServiceCollection services,
            Func<IServiceProvider, TImplementation> implementationFactory, ServiceLifetime serviceLifetime)
            where TService : class
            where TImplementation : class, TService

        {
            return serviceLifetime switch
            {
                ServiceLifetime.Singleton =>
                services.AddLazySingleton<TService, TImplementation>(implementationFactory),
                ServiceLifetime.Scoped => services.AddLazyScoped<TService, TImplementation>(implementationFactory),
                ServiceLifetime.Transient =>
                services.AddLazyTransient<TService, TImplementation>(implementationFactory),
                _ => throw new ArgumentOutOfRangeException(nameof(serviceLifetime), serviceLifetime, null)
            };
        }

        #endregion

        #region Transient

        public static IServiceCollection AddLazyTransient<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            services.AddTransient<TService, TImplementation>();
            RegisterTransientLazy<TService>(services);
            return services;
        }

        public static IServiceCollection AddLazyTransient<TService>(this IServiceCollection services)
            where TService : class
        {
            services.AddTransient<TService>();
            RegisterTransientLazy<TService>(services);
            return services;
        }

        public static IServiceCollection AddLazyTransient<TService>(
            this IServiceCollection services,
            Func<IServiceProvider, TService> implementationFactory)
            where TService : class

        {
            services.AddTransient<TService>(implementationFactory);
            RegisterTransientLazy<TService>(services);
            return services;
        }

        public static IServiceCollection AddLazyTransient<TService, TImplementation>(
            this IServiceCollection services,
            Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService

        {
            services.AddTransient<TService, TImplementation>(implementationFactory);
            RegisterTransientLazy<TService>(services);
            return services;
        }

        #endregion

        #region Scope

        public static IServiceCollection AddLazyScoped<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            services.AddScoped<TService, TImplementation>();
            RegisterScopedLazy<TService>(services);
            return services;
        }

        public static IServiceCollection AddLazyScoped<TService>(this IServiceCollection services)
            where TService : class
        {
            services.AddScoped<TService>();
            RegisterScopedLazy<TService>(services);
            return services;
        }

        public static IServiceCollection AddLazyScoped<TService>(
            this IServiceCollection services,
            Func<IServiceProvider, TService> implementationFactory)
            where TService : class

        {
            services.AddScoped<TService>(implementationFactory);
            RegisterScopedLazy<TService>(services);
            return services;
        }

        public static IServiceCollection AddLazyScoped<TService, TImplementation>(
            this IServiceCollection services,
            Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService

        {
            services.AddScoped<TService, TImplementation>(implementationFactory);
            RegisterScopedLazy<TService>(services);
            return services;
        }

        #endregion

        #region Singleton

        public static IServiceCollection AddLazySingleton<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            services.AddSingleton<TService, TImplementation>();
            RegisterSingletonLazy<TService>(services);
            return services;
        }

        public static IServiceCollection AddLazySingleton<TService>(this IServiceCollection services)
            where TService : class
        {
            services.AddSingleton<TService>();
            RegisterSingletonLazy<TService>(services);
            return services;
        }

        public static IServiceCollection AddLazySingleton<TService>(
            this IServiceCollection services,
            Func<IServiceProvider, TService> implementationFactory)
            where TService : class

        {
            services.AddSingleton<TService>(implementationFactory);
            RegisterSingletonLazy<TService>(services);
            return services;
        }

        public static IServiceCollection AddLazySingleton<TService, TImplementation>(
            this IServiceCollection services,
            Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService

        {
            services.AddSingleton<TService, TImplementation>(implementationFactory);
            RegisterSingletonLazy<TService>(services);
            return services;
        }

        #endregion

        #region Private Support Methods

        private static void RegisterSingletonLazy<TService>(IServiceCollection services)
            where TService : class
        {
            services.AddSingleton(sp => new Lazy<TService>(sp.GetRequiredService<TService>));
        }

        private static void RegisterScopedLazy<TService>(IServiceCollection services)
            where TService : class
        {
            services.AddScoped(sp => new Lazy<TService>(sp.GetRequiredService<TService>));
        }

        private static void RegisterTransientLazy<TService>(IServiceCollection services)
            where TService : class
        {
            services.AddTransient(sp => new Lazy<TService>(sp.GetRequiredService<TService>));
        }

        #endregion
    }
}
