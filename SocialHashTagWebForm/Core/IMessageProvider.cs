using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace SocialHashTagWebForm.Core
{
    public static class LocalUrlHelper
    {
        public static string GetLocalUrl()
        {
            var request = HttpContext.Current.Request;
            var localUrl = string.Concat(
                request.Url.Scheme, "://",
                request.Url.Authority);
            if (!localUrl.EndsWith("/"))
            {
                localUrl += "/";
            }
            return localUrl;
        }
    }

    public interface IMessageProvider
    {
        Task<IList<MessageItem>> Get(string tag);
    }

    public class Requester<T>
    {
        public async Task<T> GetResponse(string url, RequestMethod method=RequestMethod.Get, string body=null)
        {
            var httpClient = new WebClient();
            string response = null;
            if (method == RequestMethod.Get || body==null)
            {
                response = await httpClient.DownloadStringTaskAsync(url);
            }
            else
            {
                response = await httpClient.UploadStringTaskAsync(url, "POST", body);
            }
            return JsonConvert.DeserializeObject<T>(response);
        }
    }

    public enum RequestMethod
    {
        Get,
        Post,
    }

    public class MessageItem
    {
        public string Id { get; set; }
        public string Provider { get; set; }
        public string VideoEmbebbedUrl { get; set; }
        public string Message { get; set; }
        public string MessageUrl { get; set; }
        
    }
}