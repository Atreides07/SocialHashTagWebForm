using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using SocialHashTagWebForm.Core.ViewModels;

namespace SocialHashTagWebForm.Core.Repository
{
    public class RepositoryManager
    {
        public async Task SaveMessageViewModel(MessageItemViewModel mvm, string providerName)
        {
            var videoTag = new VideoHashTag();
            videoTag.UniqueId = mvm.UniqueId;
            videoTag.Id = mvm.Id;
            videoTag.AddTime = DateTime.UtcNow;
            videoTag.SourceProvider = providerName;
            videoTag.Tag = WebConfigurationManager.AppSettings["hashtag"];
            videoTag.VideoUrl = mvm.VideoEmbebbedUrl;

            var context = new VideoHashTagDbContext();
            var videoHashTag = await context.Videos.FirstOrDefaultAsync(i => i.UniqueId == videoTag.UniqueId);
            if (videoHashTag == null)
            {
                context.Videos.Add(videoTag);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteMessageViewModel(MessageItemViewModel mvm, string providerName)
        {
            var uniqueId = mvm.UniqueId;
            var context = new VideoHashTagDbContext();
            var videoHashTag = await context.Videos.FirstOrDefaultAsync(i => i.UniqueId == uniqueId);
            if (videoHashTag != null)
            {
                context.Videos.Remove(videoHashTag);
                await context.SaveChangesAsync();
            }
        }
    }
}