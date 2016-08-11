using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using SocialHashTagWebForm.Core.Repository;

namespace SocialHashTagWebForm.Core.ViewModels
{
    public class MessagesViewModel : List<MessageItemViewModel>
    {
        public MessagesViewModel(IList<MessageItem> messages, string provderName)
        {
            RequestTime = DateTime.Now;
            using (var context = new VideoHashTagDbContext())
            {
                foreach (var message in messages)
                {
                    Add(new MessageItemViewModel(message, provderName,context));
                }
            }
        }


        public DateTime RequestTime { get; private set; }
        
    }

    public class MessageItemViewModel
    {
        
        public MessageItemViewModel(MessageItem messageItem, string providerName, VideoHashTagDbContext context)
        {
            this.Id = messageItem.Id;
            this.Message = messageItem.Message;
            this.MessageUrl = messageItem.MessageUrl;
            this.Provider = messageItem.Provider;
            this.VideoEmbebbedUrl = messageItem.VideoEmbebbedUrl;
            this.AuthorName=messageItem.AuthorName;
            this.AuthorUrl=messageItem.AuthorUrl;
            UniqueId = providerName + "_" + messageItem.Id;
            var videoHashTag = context.Videos.FirstOrDefault(i => i.UniqueId == UniqueId);
            Approved = videoHashTag!=null;
            Unknown = !Approved;
        }

        public string AuthorUrl { get; set; }

        public string AuthorName { get; set; }

        public bool Unknown { get; set; }

        public bool Approved { get; set; }

        public string Id { get; set; }
        public string Provider { get; set; }
        public string VideoEmbebbedUrl { get; set; }
        public string Message { get; set; }
        public string MessageUrl { get; set; }

        public string Display => string.IsNullOrEmpty(VideoEmbebbedUrl) ? "none" : "block";
        public string UniqueId { get; private set; }
    }
}