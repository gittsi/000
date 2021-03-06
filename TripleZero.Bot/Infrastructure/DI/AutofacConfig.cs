﻿using Autofac;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Caching.Memory;
using TripleZero.Bot.Helper;
using TripleZero.Bot.Settings;
using TripleZero.Core;
using TripleZero.Infrastructure.DI;

namespace TripleZero.Bot.Infrastructure.DI
{
    public static class AutofacConfig
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            //Resolvers
            //builder.RegisterType<TripleZero.Infrastructure.DI.IResolver>().As<IStartable>().SingleInstance();
            builder.RegisterType<IResolver>().As<IStartable>().SingleInstance();
            //builder.RegisterType<TripleZero.Core.Caching.Infrastructure.DI.IResolver>().As<IStartable>().SingleInstance();
            
            //configuration
            builder.RegisterType<ApplicationSettings>().SingleInstance();                        
            builder.RegisterType<SettingsConfiguration>().As<ISettingsConfiguration>().SingleInstance();
            builder.RegisterType<Logo>().SingleInstance();

            builder.RegisterType<PlayerContext>().As<IPlayerContext>().SingleInstance();

            builder.RegisterType<MemoryCache>().As<IMemoryCache>().SingleInstance().WithParameter("MemoryCacheOptions", new MemoryCacheOptions());

            ////moules
            //builder.RegisterType<HelpModule>().InstancePerDependency();
            //builder.RegisterType<FunModule>().InstancePerDependency();
            //builder.RegisterType<GuildModule>().InstancePerDependency();
            //builder.RegisterType<CharacterModule>().InstancePerDependency();
            //builder.RegisterType<ModsModule>().InstancePerDependency();
            //builder.RegisterType<PlayerModule>().InstancePerDependency();
            //builder.RegisterType<AdminModule>().InstancePerDependency();
            //builder.RegisterType<DBStatsModule>().InstancePerDependency();
            //builder.RegisterType<ArenaModule>().InstancePerDependency();



            //discord
            builder.RegisterType<DiscordSocketClient>().SingleInstance();

            //commandService
            builder.RegisterType<CommandService>().InstancePerDependency();

            ////repositories
            //builder.RegisterType<SWGoHRepository>().As<ISWGoHRepository>().InstancePerDependency();
            //builder.RegisterType<MongoDBRepository>().As<IMongoDBRepository>().InstancePerDependency();

            ////validator
            ////builder.RegisterType<FluentValidationModelValidatorProvider>().As<ModelValidatorProvider>();
            //builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly())
            //       .Where(t => t.Name.EndsWith("Validator"))
            //       .AsImplementedInterfaces()
            //       .InstancePerLifetimeScope();

            


            ////builder.RegisterType<AutofacValidatorFactory2>().As<IValidatorFactory>().SingleInstance();
            //builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly())
            //.Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
            //.AsImplementedInterfaces().InstancePerLifetimeScope();

            //builder.RegisterType<PlayerValidator>().As<IValidator>().SingleInstance();

            ////container.Register(typeof(IValidator<>), new[] { System.Reflection.Assembly.GetExecutingAssembly() }, Lifestyle.Singleton);
            ////container.RegisterSingleton<IValidatorFactory>(() => new IocValidatorFactory(container));

            return builder.Build();
        }
    }

}
