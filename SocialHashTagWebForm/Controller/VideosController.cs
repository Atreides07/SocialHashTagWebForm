using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.OData;
using Microsoft.AspNet.Identity.Owin;
using SocialHashTagWebForm.Core.Repository;

namespace SocialHashTagWebForm.Controller
{
    [EnableQuery]
    public class VideosController : ODataController
    {
        public IQueryable<VideoHashTag> Get() => Request.GetOwinContext().Get<VideoHashTagDbContext>().Videos;
    }
}