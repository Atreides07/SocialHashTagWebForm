using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocialHashTagWebForm.Core.Repository;

namespace SocialHashTagWebForm
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var videoHashTags = new VideoHashTagDbContext().Videos.OrderByDescending(i=>i.AddTime).Take(10).ToList();
                MessagesRepeater.DataSource = videoHashTags;
                MessagesRepeater.DataBind();
            }
        }
    }
}