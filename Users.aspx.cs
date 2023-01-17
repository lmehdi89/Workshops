using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class _Users : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // ***** <ACL> *****
        //if (Session["UserName"] == null)
        //{
        //    Response.Redirect("/");
        //}
        //if (Session["Permissions"] != null)
        //{
        //    List<string> Permissions = (List<string>)Session["Permissions"];

        //    if (!Permissions.Contains("2"))
        //    {
        //        Response.Redirect("~/");
        //    }
        //}
        // ***** </ACL> *****

        // Change Filter Menu Texts
        Helper.ChangeFilterMenu(RadGrid1.FilterMenu);
    }

    protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        DB db = new DB();
        DataTable dt = db.UsersSelect();

        // Modify Data, Convert Date to ShamsiDate
        dt.Columns.Add("CreatedShamsi", typeof(string));
        dt.Columns.Add("LastLoginDateShamsi", typeof(string));
        foreach (DataRow row in dt.Rows)
        {
            row["CreatedShamsi"] = Helper.DateToShamsi(row["Created"]);
            if (row["LastLoginDate"] != DBNull.Value)
            {
                row["LastLoginDateShamsi"] = Helper.DateToShamsi(row["LastLoginDate"]);
            }
        }

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

    protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        // Get the Selected UserId
        string SelectedUserId = (e.Item as GridEditableItem).GetDataKeyValue("UserId").ToString();

        if (SelectedUserId != string.Empty && Helper.IsNumeric(SelectedUserId))
        {
            // Delete the UserId
            DB db = new DB();
            if (db.UsersDelete(SelectedUserId) == 0)
            {
                RadNotification1.Show("عملیات با موفقیت انجام شد");
            }
            else
            {
                RadNotification1.Show("خطا در انجام عملیات");
            }
        }
        // Refresh the Users Grid    
        RadGrid1.Rebind();
    }

    protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
    {
        // Change Filter TextBox Width
        if (e.Item is GridFilteringItem)
        {
            GridFilteringItem filteringItem = e.Item as GridFilteringItem;
            TextBox box = filteringItem["UserName"].Controls[0] as TextBox;
            box.Width = Unit.Percentage(60);
            box = filteringItem["FirstName"].Controls[0] as TextBox;
            box.Width = Unit.Percentage(60);
            box = filteringItem["LastName"].Controls[0] as TextBox;
            box.Width = Unit.Percentage(60);
            box = filteringItem["CreatedShamsi"].Controls[0] as TextBox;
            box.Width = Unit.Percentage(60);
            box = filteringItem["LastLoginDateShamsi"].Controls[0] as TextBox;
            box.Width = Unit.Percentage(60);
        }
    }

    protected void BtnRefresh_Click(object sender, EventArgs e)
    {
        RadGrid1.Rebind();
    }

    protected void BtnAddNewUser_Click(object sender, EventArgs e)
    {
        // Set Error Label LB_AddNewUser to empty
        LB_AddNewUser.Text = string.Empty;

        // Show Dialog
        WebDialogWindow_AddNewUser.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Normal;
    }

    protected void BtnSaveNewUser_Click(object sender, EventArgs e)
    {
        string UserName = Tb_NewUserName.Text.Trim();
        string Password = Tb_NewPassword.Text.Trim();
        string Password2 = Tb_NewPassword2.Text.Trim();
        string FirstName = Tb_NewFirstName.Text.Trim();
        string LastName = Tb_NewLastName.Text.Trim();
        string Email = Tb_NewEmail.Text.Trim();
        string Tel = Tb_NewTel.Text.Trim();
        string Mobile = Tb_NewMobile.Text.Trim();
        string Address = Tb_NewAddress.Text.Trim();
        string PostalCode = Tb_NewPostalCode.Text.Trim();
        bool IsActive = CHK_NewIsActive.Checked;

        if (Password != Password2)
        {
            LB_AddNewUser.Text = "رمز عبور و تکرار آن مساوی نیستند";
        }
        else if (UserName != string.Empty &&
            Password != string.Empty &&
            Password2 != string.Empty &&
            FirstName != string.Empty &&
            LastName != string.Empty)
        {
            // If not Exist in Table, Insert New Row
            DB db = new DB();
            if (db.UsersInsert(UserName, Password, FirstName, LastName, Email, Tel, Mobile, Address, PostalCode, IsActive, Session["UserId"].ToString()) == 0)
            {
                RadNotification1.Show("عملیات با موفقیت انجام شد");

                // Refresh the Roles Grid
                RadGrid1.Rebind();

                // Clear the Textbox
                Tb_NewUserName.Text = string.Empty;
                Tb_NewPassword.Text = string.Empty;
                Tb_NewPassword2.Text = string.Empty;
                Tb_NewFirstName.Text = string.Empty;
                Tb_NewLastName.Text = string.Empty;
                Tb_NewEmail.Text = string.Empty;
                Tb_NewTel.Text = string.Empty;
                Tb_NewMobile.Text = string.Empty;
                Tb_NewAddress.Text = string.Empty;
                Tb_NewPostalCode.Text = string.Empty;
                CHK_NewIsActive.Checked = true;

                // Close the WebDialogWindow
                WebDialogWindow_AddNewUser.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Hidden;
            }
            else
            {
                LB_AddNewUser.Text = "خطا در انجام عملیات";
            }
        }
        else
        {
            LB_AddNewUser.Text = "لطفا فیلدهای ستاره دار را پر نمایید";
        }
    }

    protected void BtnEditUser_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "EditUser" && e.CommandArgument.ToString().Length > 0)
        {
            // Clear Error Label
            LB_EditUser.Text = string.Empty;

            // Clear Edit Form
            Tb_EditUserName.Text = string.Empty;
            Tb_EditPassword.Text = string.Empty;
            Tb_EditPassword2.Text = string.Empty;
            Tb_EditFirstName.Text = string.Empty;
            Tb_EditLastName.Text = string.Empty;
            Tb_EditEmail.Text = string.Empty;
            Tb_EditTel.Text = string.Empty;
            Tb_EditMobile.Text = string.Empty;
            Tb_EditAddress.Text = string.Empty;
            Tb_EditPostalCode.Text = string.Empty;
            CHK_EditIsActive.Checked = true;

            // Get the Selected UserId
            string SelectedUserId = e.CommandArgument.ToString();

            // Populate Edit Form
            if (SelectedUserId != string.Empty && Helper.IsNumeric(SelectedUserId))
            {
                DB db = new DB();
                DataTable dt = db.GetUserInfo(SelectedUserId);
                if (dt.Rows.Count > 0)
                {
                    Tb_EditUserName.Text = dt.Rows[0]["UserName"].ToString();
                    Tb_EditPassword.Text = string.Empty;
                    Tb_EditPassword2.Text = string.Empty;
                    Tb_EditFirstName.Text = dt.Rows[0]["FirstName"].ToString();
                    Tb_EditLastName.Text = dt.Rows[0]["LastName"].ToString();
                    Tb_EditEmail.Text = dt.Rows[0]["Email"].ToString();
                    Tb_EditTel.Text = dt.Rows[0]["Tel"].ToString();
                    Tb_EditMobile.Text = dt.Rows[0]["Mobile"].ToString();
                    Tb_EditAddress.Text = dt.Rows[0]["Address"].ToString();
                    Tb_EditPostalCode.Text = dt.Rows[0]["PostalCode"].ToString();
                    CHK_EditIsActive.Checked = dt.Rows[0]["IsActive"].ToString() == "True" ? true : false;

                    // Set Dialog Header Caption
                    LB_SelectedUserId.Text = SelectedUserId;
                    WebDialogWindow_EditUser.Header.CaptionText = "ویرایش پروفایل : " + SelectedUserId;

                    // Show Dialog
                    WebDialogWindow_EditUser.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Normal;
                }
            }
            else
            {
                // Clear Edit Form
                Tb_EditUserName.Text = string.Empty;
                Tb_EditPassword.Text = string.Empty;
                Tb_EditPassword2.Text = string.Empty;
                Tb_EditFirstName.Text = string.Empty;
                Tb_EditLastName.Text = string.Empty;
                Tb_EditEmail.Text = string.Empty;
                Tb_EditTel.Text = string.Empty;
                Tb_EditMobile.Text = string.Empty;
                Tb_EditAddress.Text = string.Empty;
                Tb_EditPostalCode.Text = string.Empty;
                CHK_EditIsActive.Checked = true;
            }
        }
    }

    protected void BtnSaveEditUser_Click(object sender, EventArgs e)
    {
        string UserId = LB_SelectedUserId.Text;
        string UserName = Tb_EditUserName.Text.Trim();
        string Password = Tb_EditPassword.Text.Trim();
        string Password2 = Tb_EditPassword2.Text.Trim();
        string FirstName = Tb_EditFirstName.Text.Trim();
        string LastName = Tb_EditLastName.Text.Trim();
        string Email = Tb_EditEmail.Text.Trim();
        string Tel = Tb_EditTel.Text.Trim();
        string Mobile = Tb_EditMobile.Text.Trim();
        string Address = Tb_EditAddress.Text.Trim();
        string PostalCode = Tb_EditPostalCode.Text.Trim();
        bool IsActive = CHK_EditIsActive.Checked;

        if (Password.Length > 0 && Password.Length < 4)
        {
            LB_EditUser.Text = "تعداد کاراکتر رمز عبور نمی تواند کمتر از 4 باشد";
        }
        else if (Password != Password2)
        {
            LB_EditUser.Text = "رمز عبور و تکرار آن مساوی نیستند";
        }
        else if (UserId != string.Empty &&
            UserName != string.Empty &&
            FirstName != string.Empty &&
            LastName != string.Empty)
        {
            // If not Exist in Table, Insert New Row
            DB db = new DB();
            if (db.UsersUpdate(UserId, UserName, Password, FirstName, LastName, Email, Tel, Mobile, Address, PostalCode, IsActive) == 0)
            {
                RadNotification1.Show("عملیات با موفقیت انجام شد");

                // Refresh the Roles Grid
                RadGrid1.Rebind();

                // Close the WebDialogWindow
                WebDialogWindow_EditUser.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Hidden;
            }
            else
            {
                LB_EditUser.Text = "خطا در انجام عملیات";
            }
        }
        else
        {
            LB_EditUser.Text = "لطفا فیلدهای ستاره دار را پر نمایید";
        }
    }

}
