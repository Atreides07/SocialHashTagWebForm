using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialHashTagWebForm.Core.ViewModels
{
    public class MessagesViewModel : List<MessageItemViewModel>
    {
        public MessagesViewModel(IList<MessageItem> messages, string provderName)
        {
            RequestTime = DateTime.Now;

            foreach(var message in messages)
            {
                Add(new MessageItemViewModel(message, provderName));
            }
        }


        public DateTime RequestTime { get; private set; }
        
    }

    public class MessageItemViewModel
    {
        
        public MessageItemViewModel(MessageItem messageItem, string providerName)
        {
            this.Id = messageItem.Id;
            this.Message = messageItem.Message;
            this.MessageUrl = messageItem.MessageUrl;
            this.Provider = messageItem.Provider;
            this.VideoEmbebbedUrl = messageItem.VideoEmbebbedUrl;
            UniqueId = providerName + "_" + messageItem.Id;
        }

        public string Id { get; set; }
        public string Provider { get; set; }
        public string VideoEmbebbedUrl { get; set; }
        public string Message { get; set; }
        public string MessageUrl { get; set; }

        public string Display => string.IsNullOrEmpty(VideoEmbebbedUrl) ? "none" : "block";
        public string UniqueId { get; private set; }
    }
}