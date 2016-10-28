using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Security Namespace
using ChinookSystem.Security;
#endregion
public partial class Admin_Security_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void RefreshAll(object sender, EventArgs e)
    {
        DataBind();
    }

    protected void UnregisteredUsersGridView_SelectedIndexChanging(object sender,
                                GridViewSelectEventArgs e)
    {
        //position the gridview to the selectindex (row) that caused
        //the postback
        UnregisteredUsersGridView.SelectedIndex = e.NewSelectedIndex;

        //setup a variable that will be the physical pointer to the selected row
        GridViewRow agvrow = UnregisteredUsersGridView.SelectedRow;

        //you can already check a pointer to see if something has been
        //obtained
        if (agvrow != null)
        {
            //access information contained in a textbox on the gv row
            //use the method .FindControl("controlidname") as controltype
            //once you have a pointer to the control, you can access the
            //    data content of the control using the control's access method
            string assignedusername = "";
            TextBox inputControl = agvrow.FindControl("AssignedUserName") as TextBox;
            if (inputControl != null)
            {
                assignedusername = inputControl.Text;
            }
            string assignedemail = (agvrow.FindControl("AssignedEmail") as TextBox).Text;

            //create the UnregisteredUser instance
            //during creation, i will pass to it the needed data
            //to load the instance attributes

            //accessing boundfields on a gridview row uses .Cells[index].Text
            //index represents the column of the grid.
            //columns are index (starting at 0)
            UnRegisteredUserProfile user = new UnRegisteredUserProfile()
            {
                CustomerEmployeeId = int.Parse(UnregisteredUsersGridView.SelectedDataKey.Value.ToString()),
                UserType = (UnRegisteredUserType)Enum.Parse(typeof(UnRegisteredUserType), agvrow.Cells[1].Text),
                FirstName = agvrow.Cells[2].Text,
                LastName = agvrow.Cells[3].Text,
                AssignedUserName = assignedusername,
                AssignedEmail = assignedemail
            };

            //register the user via the Chinook.UserManger controller
            UserManager sysmgr = new UserManager();
            sysmgr.RegisterUser(user);

            //assume successful creation of a user
            //refresh the form
            DataBind();
        }
    }

    protected void UserListView_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        //one needs to walk through the checkboxlist

        //create the RoleMembership string List<> of selected roles
        var addtoroles = new List<string>();

        //point to the phyiscal checkboxlist control
        var roles = e.Item.FindControl("RoleMemberships") as CheckBoxList;

        //does the control exist  -safety check
        if (roles != null)
        {
            //cycle through the checkboxlist
            //find which roles have been selected (checked)
            //add to the List<string>
            //assign the List<string> to the inserting instance
            //     represented by e
            foreach (ListItem role in roles.Items)
            {
                if (role.Selected)
                {
                    addtoroles.Add(role.Value);
                }
                e.Values["RoleMemberships"] = addtoroles;
            }
        }
    }
}