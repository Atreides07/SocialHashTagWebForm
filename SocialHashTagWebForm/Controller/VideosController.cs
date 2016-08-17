using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.OData;
using Microsoft.AspNet.Identity.Owin;
using SocialHashTagWebForm.Core.Repository;

namespace SocialHashTagWebForm.Controller
{
    [EnableQuery]
    public class VideosController : ODataController
    {
        VideoHashTagDbContext _db;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            _db = Request.GetOwinContext().Get<VideoHashTagDbContext>();
        }

        public IQueryable<VideoHashTag> Get() => Request.GetOwinContext().Get<VideoHashTagDbContext>().Videos;

        public VideoHashTag Get([FromODataUri]string key) => Request.GetOwinContext().Get<VideoHashTagDbContext>().Videos.Single(v => v.Id == key);

        public async Task<IHttpActionResult> Post(VideoHashTag video)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _db.Videos.Add(video);
            await _db.SaveChangesAsync();

            return Created(video);
        }

        public async Task<IHttpActionResult> Patch([FromODataUri] string key, Delta<VideoHashTag> video)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = await _db.Videos.FirstOrDefaultAsync<VideoHashTag>(v => v.Id == key);
            if (entity == null) return NotFound();

            video.Patch(entity);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _db.Videos.AnyAsync(v => v.Id == key) == false) return NotFound();

                throw;
            }

            return Updated(entity);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] string key)
        {
            var video = new VideoHashTag { Id = key };
            _db.Videos.Attach(video);
            _db.Videos.Remove(video);
            await _db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
