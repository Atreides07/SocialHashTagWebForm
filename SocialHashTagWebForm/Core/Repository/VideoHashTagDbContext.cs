using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SocialHashTagWebForm.Core.Repository
{
    public class VideoHashTagDbContext : DbContext
    {
        public VideoHashTagDbContext() : base("DefaultConnection")
        {
            Database.SetInitializer<VideoHashTagDbContext>(null);
        }

        public static VideoHashTagDbContext Create()
        {
            return new VideoHashTagDbContext();
        }

        public DbSet<VideoHashTag> Videos { get; set; }
    }
}