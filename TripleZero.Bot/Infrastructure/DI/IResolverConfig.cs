
using Autofac;
using SWGoH.Model;
using TripleZero.Bot.Settings;
using TripleZero.Core.Caching;
using TripleZero.Core.Caching.Strategy;

namespace TripleZero.Infrastructure.DI
{
    public abstract class ResolverConfig
    {
        internal IContainer Container { get; set; }
        public ApplicationSettings ApplicationSettings { get { return Container.Resolve<ApplicationSettings>(); } }
        public SettingsTripleZeroBot SettingsTripleZeroBot { get { return Container.Resolve<SettingsTripleZeroBot>(); } }
        public SettingsTripleZeroRepository SettingsTripleZeroRepository { get { return Container.Resolve<SettingsTripleZeroRepository>(); } }
        //public ShipSettings ShipSettings { get { return Container.Resolve<ShipSettings>(); } }
        public CacheClient CacheClient { get { return Container.Resolve<CacheClient>(); } }
        //public ISWGoHRepository SWGoHRepository { get { return Container.Resolve<ISWGoHRepository>(); } }
        //public IMongoDBRepository MongoDBRepository { get { return Container.Resolve<IMongoDBRepository>(); } }
        //public AutofacValidatorFactory ValidatorFactory { get { return Container.Resolve<AutofacValidatorFactory>(); } }
        //public IValidatorFactory ValidatorFactory { get { return Container.Resolve<IValidatorFactory>(); } }
        //public ValidatorFactory ValidatorFactory3 { get { return Container.Resolve<ValidatorFactory>(); } 

        public CachingFactory CachingFactory { get { return Container.Resolve<CachingFactory>(); } }
        public CachingStrategyContext CachingStrategyContext { get { return Container.Resolve<CachingStrategyContext>(); } }
        public CachingRepositoryStrategy CachingRepositoryStrategy { get { return Container.Resolve<CachingRepositoryStrategy>(); } }
        public CachingModuleStrategy CachingModuleStrategy { get { return Container.Resolve<CachingModuleStrategy>(); } }

        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            //configurations            
            builder.RegisterType<ApplicationSettings>().SingleInstance();
            builder.RegisterType<SettingsTripleZeroBot>().SingleInstance();
            builder.RegisterType<SettingsTripleZeroRepository>().SingleInstance();
            
            builder.RegisterType<SettingsConfiguration>().As<ISettingsConfiguration>().SingleInstance();

            ////repositories
            //builder.RegisterType<SWGoHRepository>().As<ISWGoHRepository>().InstancePerDependency();
            //builder.RegisterType<MongoDBRepository>().As<IMongoDBRepository>().InstancePerDependency();

            //cachclient
            builder.RegisterType<CacheClient>().SingleInstance();

            //configurations
           
            builder.RegisterType<CachingFactory>().SingleInstance();
            builder.RegisterType<SettingsConfiguration>().As<ISettingsConfiguration>().SingleInstance();
            builder.RegisterType<CacheConfiguration>().As<ICacheConfiguration>().SingleInstance();

            //strategies
            builder.RegisterType<CachingStrategy>().As<ICachingStrategy>().InstancePerDependency();
            builder.RegisterType<CachingRepositoryStrategy>().SingleInstance();
            builder.RegisterType<CachingModuleStrategy>().SingleInstance();

            //context            
            builder.RegisterType<CachingStrategyContext>().InstancePerDependency();

            //validator     

            //builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly())
            //.Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
            //.AsImplementedInterfaces().InstancePerLifetimeScope();

            ////builder.RegisterType<AutofacValidatorFactory>().As<IValidatorFactory>().SingleInstance();
            ////builder.RegisterType<PlayerValidator>().As<IValidator>().SingleInstance();

            //builder.RegisterType<PlayerValidator>()
            //    .Keyed<IValidator>(typeof(IValidator<Player>))
            //    .As<IValidator>();

            //builder.RegisterType<AutofacValidatorFactory>().SingleInstance();
            //builder.RegisterType<ValidatorFactory>().As<IValidatorFactory>().SingleInstance();

            return builder.Build();
        }
    }
}
