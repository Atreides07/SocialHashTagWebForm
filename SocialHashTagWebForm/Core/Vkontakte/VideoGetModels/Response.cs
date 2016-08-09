using System.Collections.Generic;
using Newtonsoft.Json;

namespace SocialHashTagWebForm.Core.Vkontakte.VideoGetModels
{
    public class Response
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }
}