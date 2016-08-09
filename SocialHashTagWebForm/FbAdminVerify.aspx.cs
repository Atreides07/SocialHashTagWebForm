using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocialHashTagWebForm.Core.Providers.FaceBook;

namespace SocialHashTagWebForm
{
    public partial class FbAdminVerify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var token = Request["access_token"];

            if (token != null)
            {
                FacebookMessageProvider.AccessCode = token;

                Response.Redirect("FbAdmin.aspx");
            }
        }
    }
}