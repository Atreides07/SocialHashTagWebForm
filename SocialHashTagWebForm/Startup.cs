﻿using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using System.Web.OData.Routing;
using System.Web.OData.Routing.Conventions;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SocialHashTagWebForm.Core.Repository;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace SocialHashTagWebForm
{    
    public partial class Startup
    {
        private HttpConfiguration _config;
        private Container _container;

        public void Configuration(IAppBuilder app)
        {
            app.Use(typeof(CookiesMiddleware));

            app.UseCors(new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = (request) => 
                    {
                        return Task.FromResult(new System.Web.Cors.CorsPolicy
                        {
                            AllowAnyHeader = true,
                            AllowAnyMethod = true,
                            AllowAnyOrigin = true,
                            PreflightMaxAge = 100,
                            SupportsCredentials = true
                        });
                    }
                }
            });

            // Мигрируем базу с нормальным запуском Seed из Configuration
            // http://stackoverflow.com/a/17339310
            var configuration = new DbMigrationsConfiguration<VideoHashTagDbContext>
            {
                ContextType = typeof(VideoHashTagDbContext),
                AutomaticMigrationsEnabled = true
            };

            var migrator = new DbMigrator(configuration);

            //This will run the migration update script and will run Seed() method
            migrator.Update();
            // -----------------------------------

            app.CreatePerOwinContext(VideoHashTagDbContext.Create);

            ConfigureAuth(app);

            ConfigureWebApi(app);
        }

        void ConfigureWebApi(IAppBuilder appBuilder)
        {
            _config = new HttpConfiguration();

            // Удаляем сериализатор в xml.
            _config.Formatters.Remove(_config.Formatters.XmlFormatter);
            // Сериализовать объекты с циклическими ссылками.
            _config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            // Сериализовать enum'ы как строку.
            _config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());

            // Attribute routing
            _config.MapHttpAttributeRoutes();

            ConfigureOData(_config);

            appBuilder.UseWebApi(_config);

            var services = _config.Services;

            var webApiControllerTypes = services.GetHttpControllerTypeResolver().GetControllerTypes(services.GetAssembliesResolver()).ToList();

            _container = SimpleInjectorWebApiInitializer.Initialize(_config, webApiControllerTypes);

            _config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(_container);

            _config.EnsureInitialized();
        }

        static void ConfigureOData(HttpConfiguration config)
        {
            ODataModelBuilder modelBuilder = new ODataConventionModelBuilder();

            modelBuilder.EntitySet<VideoHashTag>("Videos");

            config.MapODataServiceRoute(routeName: "ODataRoute", routePrefix: "api", model: modelBuilder.GetEdmModel());
        }
    }
}
