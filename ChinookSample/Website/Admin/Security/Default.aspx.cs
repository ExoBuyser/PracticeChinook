using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Security namespace
using ChinookSystem.Security;
#endregion
public partial class Admin_Security_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void RoleListView_ItemDeleted(object sender, ListViewDeletedEventArgs e)
    {
        DataBind();
    }

    protected void RoleListView_ItemInserted(object sender, ListViewInsertedEventArgs e)
    {
        DataBind();
    }

    protected void RefreshAll(object sender, EventArgs e)
    {
        DataBind();
    }
}