using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocialHashTagWebForm.Core;
using SocialHashTagWebForm.Core.Providers.FaceBook;
using SocialHashTagWebForm.Core.Providers.Vkontakte;
using SocialHashTagWebForm.Core.Repository;
using SocialHashTagWebForm.Core.ViewModels;

namespace SocialHashTagWebForm
{
    public partial class FbAdmin : System.Web.UI.Page
    {
        readonly FacebookMessageProvider fbMessageProvider = new FacebookMessageProvider();
        static MessagesViewModel _messagesViewModel;
        protected async void Page_Load(object sender, EventArgs e)
        {
            AuthButtonLinkButton.Visible = false;
            if (_messagesViewModel == null)
            {
                try
                {
                    var tag = WebConfigurationManager.AppSettings["hashtag"];

                    var messages = await fbMessageProvider.Get(tag);

                    _messagesViewModel = new MessagesViewModel(messages, fbMessageProvider.ProviderName);
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

                    MessagesRepeater.DataSource = _messagesViewModel;
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
            var authUrl = fbMessageProvider.AuthUrl;

            Response.Redirect(authUrl);
        }

        protected async void Like_Click(object sender, EventArgs e)
        {
            var linkButton = sender as LinkButton;
            string messageId = linkButton?.CommandArgument;
            var mvm = _messagesViewModel?.FirstOrDefault(i => i.Id == messageId);
            if (mvm != null)
            {
                await new RepositoryManager().SaveMessageViewModel(mvm, fbMessageProvider.ProviderName);
            }
        }
    }
}