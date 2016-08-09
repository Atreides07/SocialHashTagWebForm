using Newtonsoft.Json;

namespace SocialHashTagWebForm.Core.Vkontakte.VideoGetModels
{
    public class VedioGetResponse
    {
        [JsonProperty("response")]
        public Response Response { get; set; }
    }
}