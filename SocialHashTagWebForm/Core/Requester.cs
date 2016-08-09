using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SocialHashTagWebForm.Core
{
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
}