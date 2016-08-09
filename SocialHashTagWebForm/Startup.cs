using System.Data.Entity.Migrations;
using Microsoft.Owin;
using Owin;
using SocialHashTagWebForm.Core.Repository;

[assembly: OwinStartupAttribute(typeof(SocialHashTagWebForm.Startup))]
namespace SocialHashTagWebForm
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
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

            ConfigureAuth(app);
        }
    }
}
