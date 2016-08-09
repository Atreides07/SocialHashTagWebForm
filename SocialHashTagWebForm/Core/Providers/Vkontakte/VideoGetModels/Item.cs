using System;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using SocialHashTagWebForm.Core.Vkontakte.NewsSearchModels;

namespace SocialHashTagWebForm.Core.Vkontakte.VideoGetModels
{
    public class Item
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("owner_id")]
        public int OwnerId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("duration")]
        public int Duration { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("date")]
        public int Date { get; set; }
        [JsonProperty("views")]
        public int Views { get; set; }
        [JsonProperty("comments")]
        public int Comments { get; set; }
        [JsonProperty("photo_130")]
        public string Photo130 { get; set; }
        [JsonProperty("photo_320")]
        public string Photo320 { get; set; }
        [JsonProperty("photo_800")]
        public string Photo800 { get; set; }
        [JsonProperty("player")]
        public string Player { get; set; }
        [JsonProperty("can_add")]
        public int CanAdd { get; set; }
    }
}