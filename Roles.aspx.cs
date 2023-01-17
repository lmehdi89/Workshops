using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Web.Security;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class _Roles : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // ***** <ACL> *****
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/");
        }
        if (Session["Permissions"] != null)
        {
            List<string> Permissions = (List<string>)Session["Permissions"];

            if (!Permissions.Contains("6"))
            {
                Response.Redirect("~/");
            }
            else
            {
                if (Permissions.Contains("7"))
                {

                }
                if (Permissions.Contains("8"))
                {
                    RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = true;
                }
                if (Permissions.Contains("9"))
                {
                    RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = true;
                }
                if (Permissions.Contains("10"))
                {
                    RadGrid1.MasterTableView.GetColumn("UsersInRoles").Visible = true;
                }
                if (Permissions.Contains("11"))
                {
                    RadGrid1.MasterTableView.GetColumn("PermissionsInRoles").Visible = true;
                }
            }
        }
        // ***** </ACL> *****

        // Change Filter Menu Texts
        Helper.ChangeFilterMenu(RadGrid1.FilterMenu);
    }

    protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        DB db = new DB();
        DataTable dt = db.RolesSelect();
        RadGrid1.DataSource = dt;
    }

    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        // Set the Row Numbers
        if (e.Item is GridDataItem)
        {
            Label lbl = e.Item.FindControl("LB_RowNumber") as Label;
            lbl.Text = (e.Item.ItemIndex + 1).ToString();
        }

        // Set PagerStyle Texts
        if (e.Item is GridPagerItem)
        {
            Helper.ChangePagerStyle((GridPagerItem)e.Item);
        }
    }

    protected void RadGrid1_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        // Get Edited Items
        Hashtable table = new Hashtable();
        (e.Item as GridEditableItem).ExtractValues(table);

        // if Edited Field is not empty
        if (table["RoleName"] != null)
        {
            string RoleId = (e.Item as GridEditableItem).GetDataKeyValue("RoleId").ToString();
            string RoleName = table["RoleName"].ToString();
            string IsActive = table["IsActive"] != null ? table["IsActive"].ToString() : "";

            //Update Row
            DB db = new DB();
            if (db.RolesUpdate(RoleId, RoleName, IsActive) == 0)
            {
                RadNotification1.Show("عملیات با موفقیت انجام شد");
            }
            else
            {
                RadNotification1.Show("خطا در انجام عملیات");
            }
        }
        else
        {
            RadNotification1.Show("فیلد نمی تواند خالی باشد");
        }
    }

    protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        // Get the Selected RoleId
        string RoleId = (e.Item as GridEditableItem).GetDataKeyValue("RoleId").ToString();

        // Delete the RoleId
        DB db = new DB();
        if (db.RolesDelete(RoleId) == 0)
        {
            RadNotification1.Show("عملیات با موفقیت انجام شد");

            // Refresh the Grid    
            RadGrid1.Rebind();
        }
        else
        {
            RadNotification1.Show("امکان حذف نقش وجود ندارد");
        }
    }

    protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
    {
        // Change RoleName Filter TextBox Width
        if (e.Item is GridFilteringItem)
        {
            GridFilteringItem filteringItem = e.Item as GridFilteringItem;
            TextBox box = filteringItem["RoleName"].Controls[0] as TextBox;
            box.Width = Unit.Pixel(150);
        }
    }

    protected void BtnAddNewRole_Click(object sender, EventArgs e)
    {
        // Set Error Label LB_AddNewRole to empty
        LB_AddNewRole.Text = "";

        // Show Dialog
        WebDialogWindow_AddNewRole.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Normal;
    }

    protected void BtnSaveNewRole_Click(object sender, EventArgs e)
    {
        string RoleName = Tb_NewRoleName.Text.Trim();
        bool IsActive = CHK_NewIsActive.Checked;

        if (RoleName != string.Empty)
        {
            DB db = new DB();
            if (db.RolesInsert(RoleName, IsActive) == 0)
            {
                RadNotification1.Show("عملیات با موفقیت انجام شد");

                // Refresh the Roles Grid
                RadGrid1.Rebind();

                // Clear the Textbox
                Tb_NewRoleName.Text = string.Empty;
                CHK_NewIsActive.Checked = true;

                // Close the WebDialogWindow
                WebDialogWindow_AddNewRole.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Hidden;
            }
            else
            {
                LB_AddNewRole.Text = "خطا در انجام عملیات";
            }
        }
        else
        {
            LB_AddNewRole.Text = "نام نقش نمی تواند خالی باشد";
        }
    }

    protected void BtnRefresh_Click(object sender, EventArgs e)
    {
        RadGrid1.Rebind();
    }

    protected void BtnUsersInRoles_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "UsersInRoles" && e.CommandArgument.ToString().Length > 0)
        {
            // Clear Error Label
            LB_UsersInRoles.Text = string.Empty;

            // Get the Selected RoleId
            string SelectedRoleId = e.CommandArgument.ToString();

            // Set Dialog Header Caption
            LB_SelectedRoleId.Text = SelectedRoleId;
            WebDialogWindow_UsersInRoles.Header.CaptionText = "مدیریت کاربران در نقش : " + SelectedRoleId;

            // Clear 2 ListBoxes
            ListBox_UsersInRoles.Items.Clear();
            ListBox_UsersNotInRoles.Items.Clear();

            // Get Users in & not in SelectedRoleId and Populate 2 ListBoxes
            DB db = new DB();
            ListBox_UsersInRoles.DataSource = db.GetUsersInRole(SelectedRoleId);
            ListBox_UsersInRoles.DataTextField = "UserName";
            ListBox_UsersInRoles.DataValueField = "UserId";
            ListBox_UsersInRoles.DataBind();
            ListBox_UsersNotInRoles.DataSource = db.GetUsersNotInRole(SelectedRoleId);
            ListBox_UsersNotInRoles.DataTextField = "UserName";
            ListBox_UsersNotInRoles.DataValueField = "UserId";
            ListBox_UsersNotInRoles.DataBind();

            // Show Dialog
            WebDialogWindow_UsersInRoles.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Normal;
        }
    }

    protected void BtnSaveUsersInRoles_Click(object sender, EventArgs e)
    {
        // Convert Listbox Items to DataTable
        DataTable UserIds = new DataTable();
        UserIds.Columns.Add("UserId");
        for (int i = 0; i < ListBox_UsersInRoles.Items.Count; i++)
        {
            DataRow row = UserIds.NewRow();
            row[0] = ListBox_UsersInRoles.Items[i].Value;
            UserIds.Rows.Add(row);
        }

        // Add UserIds To RoleId
        DB db = new DB();
        if (db.UsersInRolesUpdate(LB_SelectedRoleId.Text, UserIds) == 0)
        {
            RadNotification1.Show("عملیات با موفقیت انجام شد");

            // Close the WebDialogWindow
            WebDialogWindow_UsersInRoles.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Hidden;
        }
        else
        {
            RadNotification1.Show("عملیات با موفقیت انجام شد");
        }
    }

    protected void BtnPermissionsInRoles_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "PermissionsInRoles" && e.CommandArgument.ToString().Length > 0)
        {
            // Set Error Label LB_AddNewRole to empty
            LB_PermissionsInRoles.Text = "";

            // Get the Selected RoleName
            string SelectedRoleId = e.CommandArgument.ToString();

            // Set Dialog Header Caption
            LB_PermissionsInRoles_SelectedRoleId.Text = SelectedRoleId;
            WebDialogWindow_UsersInRoles.Header.CaptionText = "مدیریت مجوزها در نقش : " + SelectedRoleId;

            // Get Permissions and Populate ListBox_Permissions
            DB db = new DB();
            ListBox_Permissions.Items.Clear();
            ListBox_Permissions.DataSource = db.PermissionsSelect();
            ListBox_Permissions.DataTextField = "PermissionName";
            ListBox_Permissions.DataValueField = "PermissionId";
            ListBox_Permissions.DataBind();

            // Get Permissions For SelectedRoleId and Set Checked Items in the ListBox_Permissions
            List<string> PermissionsForRole = new List<string>();
            PermissionsForRole = db.PermissionInRolesSelect(SelectedRoleId, PermissionsForRole);
            for (int i = 0; i < ListBox_Permissions.Items.Count; i++)
            {
                if (PermissionsForRole.Contains(ListBox_Permissions.Items[i].Value))
                {
                    ListBox_Permissions.Items[i].Checked = true;
                }
            }

            // Show Dialog
            WebDialogWindow_PermissionsInRoles.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Normal;
        }
    }

    protected void BtnSavePermissionsInRoles_Click(object sender, EventArgs e)
    {
        string SelectedRoleId = LB_PermissionsInRoles_SelectedRoleId.Text;

        DB db = new DB();
        // Remove Unchecked Items From DB
        for (int i = 0; i < ListBox_Permissions.Items.Count; i++)
        {
            if (ListBox_Permissions.Items[i].Checked == false)
            {
                db.PermissionInRolesDelete(SelectedRoleId, ListBox_Permissions.Items[i].Value);
            }
        }
        // Insert Checked Items To DB
        for (int i = 0; i < ListBox_Permissions.CheckedItems.Count; i++)
        {
            db.PermissionInRolesInsert(SelectedRoleId, ListBox_Permissions.CheckedItems[i].Value);
        }

        // Set Successful Label Text
        RadNotification1.Show("عملیات با موفقیت انجام شد");

        // Close the WebDialogWindow
        WebDialogWindow_PermissionsInRoles.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Hidden;
    }

}
