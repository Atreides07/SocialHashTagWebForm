using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using SocialHashTagWebForm.Core;
using SocialHashTagWebForm.Core.Vkontakte;

namespace SocialHashTagWebForm
{
    public partial class VkAdminVerify : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            var code = Request["code"];
            var baseUrl = LocalUrlHelper.GetLocalUrl();
            var tokenUrl =
                $"https://oauth.vk.com/access_token?client_id=5575720&client_secret=PyG8WmqwSiZggNnJCKno&redirect_uri={baseUrl}vkadminverify&code=" + code;

            var token = await new Requester<AccessResponse>().GetResponse(tokenUrl);
            VkMessageProvider.AccessCode = token.AccessToken;
            Response.Redirect("VkAdmin.aspx");
        }
    }
}