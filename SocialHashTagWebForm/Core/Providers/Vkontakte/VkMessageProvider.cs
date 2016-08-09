using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SocialHashTagWebForm.Core.Vkontakte.NewsSearchModels;
using SocialHashTagWebForm.Core.Vkontakte.VideoGetModels;
using Item = SocialHashTagWebForm.Core.Vkontakte.NewsSearchModels.Item;

namespace SocialHashTagWebForm.Core.Providers.Vkontakte
{
    public class VkMessageProvider : IMessageProvider
    {
        public async Task<IList<MessageItem>> Get(string tag)
        {
            if (string.IsNullOrEmpty(AccessCode))
            {
                throw new UnauthorizedAccessException();
            }

            var response=await new Requester<NewsSearch>().GetResponse(requestUri);
            var requestUri = string.Format(requestUrlFormat, Uri.EscapeDataString(tag));
            var result = new List<MessageItem>();
            foreach (var item in response.Response.Items)
            {
                var messageItem = await GetMessageItem(item);
                result.Add(messageItem);
            }
            return result;
        }

        private async Task<MessageItem> GetMessageItem(Item item)
        {
            var messageItem = new MessageItem()
            {
                Id = item.Id.ToString(),
                Provider = "VK",
                Message = item.Text,
                MessageUrl = item.Id.ToString(),
               
            };
            if (messageItem.Message != null && messageItem.Message.Length > 1000)
            {
                messageItem.Message = messageItem.Message.Substring(0, 1000);
            }
            var videoAttach = item.Attachments?.FirstOrDefault(i => i.Type == "video");
            if (videoAttach != null)
            {
                var videId = GetVideoId(videoAttach);
                if (videId != null)
                {
                    messageItem.VideoEmbebbedUrl = await GetVideoUrl(videoAttach, videId);
                }
            }
            return messageItem;

        }



        private async Task<string> GetVideoUrl(Attachment videoAttach, string videId)
        {
            var url = $"https://api.vk.com/method/video.get?count=50&videos={videId}&v=5.53&access_token=" + AccessCode;
            var videoGetResponse = await new Requester<VedioGetResponse>().GetResponse(url);

            return videoGetResponse.Response?.Items.FirstOrDefault()?.Player;
        }

        private static string GetVideoId(Attachment videoAttach)
        {
            var video = videoAttach?.Video;
            if (video == null)
            {
                return null;
            }
            if (video.AccessKey != null)
            {
                return video.OwnerId + "_" + video.Id + "_" + video.AccessKey;
            }
            return video.OwnerId + "_" + video.Id;
        }

        private string requestUrlFormat = "https://api.vk.com/method/newsfeed.search?count=100&q={0}&v=5.53";


        private string authUrl = "https://oauth.vk.com/authorize?client_id=5575720&redirect_uri=http://hashtags.1gb.ru/verify";

        private string token = "a431445d5b967cd10927b06e5e654b33e3fe017db7772c5bc172c5c543cab44ab11bb5234bef58ffee46b";
        public static string AccessCode { get; set; }
        public string ProviderName { get; } = "Vkontakte";
    }

    public class AccessResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}