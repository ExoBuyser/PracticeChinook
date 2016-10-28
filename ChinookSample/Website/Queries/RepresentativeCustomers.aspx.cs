using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Queries_RepresentitiveCustomers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //are you logged in.
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            //else
            //{
            //    //are you allowed to be on this page
            //    if (!User.IsInRole(SecurityRoles.WebsiteAdmins))
            //    {
            //        Response.Redirect("~/Default.aspx");
            //    }
            //}
        }
    }
}