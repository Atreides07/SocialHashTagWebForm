using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SocialHashTagWebForm.Core.Repository
{
    public class VideoHashTagDbContext : DbContext
    {
        public VideoHashTagDbContext()
        {
            Database.SetInitializer<VideoHashTagDbContext>(null);
        }

        public DbSet<VideoHashTag> Videos { get; set; }
    }
}