using System.Collections.Generic;
using Newtonsoft.Json;

namespace SocialHashTagWebForm.Core.Vkontakte.NewsSearchModels
{
    public class NewsSearch
    {
        [JsonProperty("response")]
        public Response Response { get; set; }
    }

    public class Response
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
    }

    public class Item
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("date")]
        public int Date { get; set; }
        [JsonProperty("owner_id")]
        public int OwnerId { get; set; }
        [JsonProperty("from_id")]
        public int FromId { get; set; }
        [JsonProperty("post_type")]
        public string PostType { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; set; }
        [JsonProperty("comments")]
        public Comments Comments { get; set; }
        [JsonProperty("likes")]
        public Likes Likes { get; set; }
        [JsonProperty("reposts")]
        public Reposts Reposts { get; set; }
    }

    public class Reposts
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }

    public class Likes
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }

    public class Attachment
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("video")]
        public Video Video { get; set; }
    }

    public class Video
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
        [JsonProperty("photo_640")]
        public string Photo640 { get; set; }
        [JsonProperty("access_key")]
        public string AccessKey { get; set; }
        [JsonProperty("can_add")]
        public int CanAdd { get; set; }
    }

    public class Comments
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}