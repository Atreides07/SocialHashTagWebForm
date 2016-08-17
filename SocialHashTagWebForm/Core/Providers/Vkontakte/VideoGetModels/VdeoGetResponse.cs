using Newtonsoft.Json;

namespace SocialHashTagWebForm.Core.Vkontakte.VideoGetModels
{
    public class VideoGetResponse
    {
        [JsonProperty("response")]
        public Response Response { get; set; }
    }
}