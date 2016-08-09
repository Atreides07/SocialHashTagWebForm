using System;
using System.Collections.Generic;
using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace SocialHashTagWebForm
{
    public static class SimpleInjectorWebApiInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static Container Initialize(HttpConfiguration config, List<Type> controllerTypes)
        {
            // Did you know the container can diagnose your configuration? Go to: https://bit.ly/YE8OJj.
            var container = new Container();

            InitializeContainer(container);

            foreach (var controllerType in controllerTypes)
            {
                container.Register(controllerType);
            }

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            return container;
        }

        private static void InitializeContainer(Container container)
        {
        }
    }
}
