using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.AspNet.Identity.Owin;
using SocialHashTagWebForm.Core;
using SocialHashTagWebForm.Core.Providers.Vkontakte;
using SocialHashTagWebForm.Core.Repository;

namespace SocialHashTagWebForm.Controller
{
    public class ActionsController : ApiController
    {
        VideoHashTagDbContext _db;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            _db = Request.GetOwinContext().Get<VideoHashTagDbContext>();
        }

        [HttpGet]
        [Route("api/" + nameof(RefreshAllProviders))]
        public IHttpActionResult RefreshAllProviders()
        {
            var allProviders = new List<IMessageProvider> { new VkMessageProvider() };
            var tag = WebConfigurationManager.AppSettings["hashtag"];

            var availableVideosUrl = new HashSet<string>(_db.Videos.Select(v => v.VideoUrl).ToList());

            allProviders
                .SelectMany(p => p.Get(tag).Result.Select(m => new { provider = p, message = m }))
                .GroupBy(m => m.message.VideoEmbebbedUrl)
                .Where(g => !availableVideosUrl.Contains(g.Key))
                .ToList().ForEach(g => {
                    var m = g.First();

                    _db.Videos.Add(new VideoHashTag
                    {
                        UniqueId = Guid.NewGuid().ToString(),
                        Id = m.message.Id,
                        AddTime = DateTime.UtcNow,
                        SourceProvider = m.provider.ProviderName,
                        Tag = WebConfigurationManager.AppSettings["hashtag"],
                        VideoUrl = m.message.VideoEmbebbedUrl,
                        AuthorUrl = m.message.AuthorUrl,
                        AuthorName = m.message.AuthorName
                    });
                });

            _db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }
    } 
}