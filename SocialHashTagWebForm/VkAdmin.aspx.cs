using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocialHashTagWebForm.Core;
using SocialHashTagWebForm.Core.Repository;
using SocialHashTagWebForm.Core.ViewModels;
using SocialHashTagWebForm.Core.Vkontakte;

namespace SocialHashTagWebForm
{
    public partial class VkAdmin : System.Web.UI.Page
    {
        VkMessageProvider vkMessageProvider=new VkMessageProvider();
        static MessagesViewModel messagesViewModel;

        protected async void Page_Load(object sender, EventArgs e)
        {
            AuthButtonLinkButton.Visible = false;
            if (messagesViewModel == null)
            {
                try
                {
                    var tag = WebConfigurationManager.AppSettings["hashtag"];

                    var messages = await vkMessageProvider.Get(tag);

                    messagesViewModel = new MessagesViewModel(messages, vkMessageProvider.ProviderName);
                }
                catch (UnauthorizedAccessException)
                {
                    AuthButtonLinkButton.Visible = true;
                }
                
            }
            
            if (!IsPostBack)
            {
                
                try
                {
                    
                    MessagesRepeater.DataSource = messagesViewModel;
                    MessagesRepeater.DataBind();
                }
                catch (UnauthorizedAccessException)
                {
                    AuthButtonLinkButton.Visible = true;
                }
                
            }
        }

        protected void OnClick(object sender, EventArgs e)
        {
            var authUrl = GetVkAuthUrl();
            Response.Redirect(authUrl);
        }

        private string GetVkAuthUrl()
        {
            var localUrl = LocalUrlHelper.GetLocalUrl();

            return $"https://oauth.vk.com/authorize?client_id=5575720&redirect_uri={localUrl}vkadminverify&scope=video&response_type=code&v=5.53";
        }

        protected async void Like_Click(object sender, EventArgs e)
        {
            var linkButton = sender as LinkButton;
            string messageId = linkButton.CommandArgument;
            if (messagesViewModel != null)
            {
                var mvm = messagesViewModel.FirstOrDefault(i => i.Id == messageId);
                if (mvm != null)
                {
                    var videoTag=new VideoHashTag();
                    videoTag.UniqueId = mvm.UniqueId;
                    videoTag.Id = mvm.Id;
                    videoTag.AddTime = DateTime.UtcNow;
                    videoTag.SourceProvider = vkMessageProvider.ProviderName;
                    videoTag.Tag = WebConfigurationManager.AppSettings["hashtag"];
                    videoTag.VideoUrl = mvm.VideoEmbebbedUrl;

                    var context=new VideoHashTagDbContext();
                    var videoHashTag = context.Videos.First(i => i.UniqueId == videoTag.UniqueId);
                    if (videoHashTag==null)
                    {
                        context.Videos.Add(videoTag);
                        await context.SaveChangesAsync();
                    }
                    
                }
            }

        }
    }
}