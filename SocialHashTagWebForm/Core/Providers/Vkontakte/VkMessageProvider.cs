using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin;
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

            var requestUri = string.Format(requestUrlFormat, Uri.EscapeDataString(tag));
            var response = await new Requester<NewsSearch>().GetResponse(requestUri);

            var listGroups = (response.Response.Groups ?? new List<Group>());
            var listProfiles = (response.Response.Profiles ?? new List<Profile>());
            foreach (var listProfile in listProfiles)
            {
                listProfile.FirstName = GetUtf8NameFromWindows1251(listProfile.FirstName);
                listProfile.LastName = GetUtf8NameFromWindows1251(listProfile.LastName);
            }

            foreach (var listGroup in listGroups)
            {
                listGroup.Name = GetUtf8NameFromWindows1251(listGroup.Name);
            }

            var groups = listGroups.ToDictionary(i => i.Id, i => i);
            var profiles = listProfiles.ToDictionary(i => i.Id, i => i);

            var result = new List<MessageItem>();
            foreach (var item in response.Response.Items)
            {
                var messageItem = await GetMessageItem(item, groups, profiles);
                result.Add(messageItem);
            }
            return result;
        }

        private async Task<MessageItem> GetMessageItem(Item item, Dictionary<int, Group> groups, Dictionary<int, Profile> profiles)
        {
            string name = null;
            string authorUrl;

            if (item.FromId < 0)
            {
                var groupId = -item.FromId;
                authorUrl = "https://vk.com/club" + groupId;
                if (groups.ContainsKey(groupId))
                {
                    var group = groups[groupId];
                    name = group.Name;
                }
            }
            else
            {
                authorUrl = "https://vk.com/id" + item.FromId;
                if (profiles.ContainsKey(item.FromId))
                {
                    var profile = profiles[item.FromId];
                    name = $"{profile.LastName} {profile.FirstName}";
                }
            }

            //var utf8Name = GetUtf8NameFromWindows1251(name);

            var messageItem = new MessageItem()
            {
                Id = item.Id.ToString(),
                Provider = "VK",
                Message = item.Text,
                MessageUrl = item.Id.ToString(),
                AuthorName = name,
                AuthorUrl = authorUrl
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

        private string GetUtf8NameFromWindows1251(string name)
        {
            var bytes = Encoding.GetEncoding("Windows-1251").GetBytes(name);
            var utf8Name = Encoding.UTF8.GetString(bytes);
            return utf8Name;
        }

        private async Task<string> GetVideoUrl(Attachment videoAttach, string videId)
        {
            var url = $"https://api.vk.com/method/video.get?count=50&videos={videId}&v=5.53&access_token=" + AccessCode;
            var videoGetResponse = await new Requester<VideoGetResponse>().GetResponse(url);

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

        private string requestUrlFormat = "https://api.vk.com/method/newsfeed.search?count=100&q={0}&extended=1&v=5.53";

        private string authUrl = "https://oauth.vk.com/authorize?client_id=5575720&redirect_uri=http://hashtags.1gb.ru/verify";

        private string token = "a431445d5b967cd10927b06e5e654b33e3fe017db7772c5bc172c5c543cab44ab11bb5234bef58ffee46b";

        readonly static Dictionary<string, string> _sessionsAccessCodes = new Dictionary<string, string>();

        public static string AccessCode
        {
            get
            {
                if (HttpContext.Current.Request.Cookies.Keys.Cast<string>().Any(k => k == CookiesMiddleware.COOCKIE_SESSION))
                {
                    string code;

                    if (_sessionsAccessCodes.TryGetValue(HttpContext.Current.Request.Cookies[CookiesMiddleware.COOCKIE_SESSION].Value, out code))
                    {
                        return code;
                    }
                }

                return String.Empty;
            }
            set
            {
                if (HttpContext.Current.Request.Cookies.Keys.Cast<string>().Any(k => k == CookiesMiddleware.COOCKIE_SESSION))
                {
                    _sessionsAccessCodes[HttpContext.Current.Request.Cookies[CookiesMiddleware.COOCKIE_SESSION].Value] = value;
                }
            }
        }

        public string ProviderName { get; } = "Vkontakte";
    }

    public class AccessResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
