using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace SocialHashTagWebForm.Core.Providers.FaceBook
{
    public class FacebookMessageProvider : IMessageProvider
    {
        public string ClientId = "597660050424155";
        public string ClientSecret = "f2ba8fdd5757d41a4747c3b08005210e";

        public string AccessCode { get; set; }

        public Task<IList<MessageItem>> Get(string tag)
        {
            if (string.IsNullOrEmpty(AccessCode))
            {
                throw new UnauthorizedAccessException();
            }
            var response = new WebClient().DownloadStringTaskAsync(RequestUrl);
            throw new NotImplementedException();
        }

        public string AuthUrl
        {
            get
            {
                var localUrl = LocalUrlHelper.GetLocalUrl();
                return $"https://www.facebook.com/dialog/oauth?client_id={ClientId}&redirect_uri={localUrl}FbAdminVerify.aspx";
            }
        }

        public string RequestUrl
            => $"https://graph.facebook.com/search?q=fsharp&|597660050424155|f2ba8fdd5757d41a4747c3b08005210e";

        public string ProviderName { get; } = "Facebook";
    }
}