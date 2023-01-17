using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ChangeFilterMenu(RadGrid1.FilterMenu);
    }

    protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        DB db = new DB();
        DataTable dt = db.WorkshopsSelect(0);

        // Modify Data, Convert Date to ShamsiDate
        dt.Columns.Add("StartTimeShamsi", typeof(string));
        dt.Columns.Add("EndTimeShamsi", typeof(string));
        foreach (DataRow row in dt.Rows)
        {
            if (row["StartTime"] != DBNull.Value)
            {
                row["StartTimeShamsi"] = Helper.DateToShamsi(row["StartTime"]);
            }
            if (row["EndTime"] != DBNull.Value)
            {
                row["EndTimeShamsi"] = Helper.DateToShamsi(row["EndTime"]);
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
        //// Get the Selected id
        //string SelectedArticleId = (e.Item as GridEditableItem).GetDataKeyValue("ArticleId").ToString();

        //if (SelectedArticleId != string.Empty && Helper.IsNumeric(SelectedArticleId))
        //{
        //    // Delete the UserId
        //    DB db = new DB();
        //    if (db.ArticlesDelete(SelectedArticleId) == 0)
        //    {
        //        RadNotification1.Show("عملیات با موفقیت انجام شد");
        //    }
        //    else
        //    {
        //        RadNotification1.Show("خطا در انجام عملیات");
        //    }
        //}
        //// Refresh the Users Grid    
        //RadGrid1.Rebind();
    }

    protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
    {
        // Change Filter TextBox Width
        if (e.Item is GridFilteringItem)
        {
            GridFilteringItem filteringItem = e.Item as GridFilteringItem;
            TextBox box = filteringItem["Name"].Controls[0] as TextBox;
            box.Width = Unit.Percentage(60);
            box = filteringItem["StartTimeShamsi"].Controls[0] as TextBox;
            box.Width = Unit.Percentage(60);
            box = filteringItem["EndTimeShamsi"].Controls[0] as TextBox;
            box.Width = Unit.Percentage(60);
        }
    }

    protected void BtnRefresh_Click(object sender, EventArgs e)
    {
        RadGrid1.Rebind();
    }

    protected void BtnEditWorkshop_Command(object sender, CommandEventArgs e)
    {
        //if (e.CommandName == "EditArticle" && e.CommandArgument.ToString().Length > 0)
        //{
        //    // Clear Error Label
        //    LB_EditArticle.Text = string.Empty;

        //    // Clear Edit Form
        //    Tb_EditTitle.Text = string.Empty;
        //    DDL_EditCategory.SelectedIndex = -1;
        //    EditAsyncUpload1.UploadedFiles.Clear();
        //    CHK_ImagePathDelete.Checked = false;
        //    Editor_EditContents.Content = string.Empty;
        //    CHK_EditIsActive.Checked = true;

        //    // Get the Selected ArticleId
        //    string SelectedArticleId = e.CommandArgument.ToString();

        //    // Populate Edit Form
        //    if (SelectedArticleId != string.Empty && Helper.IsNumeric(SelectedArticleId))
        //    {
        //        DB db = new DB();
        //        DataTable dt = db.GetArticleInfo(SelectedArticleId);
        //        if (dt.Rows.Count > 0)
        //        {
        //            Tb_EditTitle.Text = dt.Rows[0]["Title"].ToString();
        //            DDL_EditCategory.SelectedValue = dt.Rows[0]["CodeCategory"].ToString();
        //            //EditAsyncUpload1.UploadedFiles[0].
        //            Editor_EditContents.Content = dt.Rows[0]["Contents"].ToString();
        //            CHK_EditIsActive.Checked = dt.Rows[0]["IsActive"].ToString() == "True" ? true : false;

        //            // Set Dialog Header Caption
        //            LB_SelectedArticleId.Text = SelectedArticleId;
        //            WebDialogWindow_EditArticle.Header.CaptionText = "ویرایش مطلب : " + SelectedArticleId;

        //            // Show Dialog
        //            WebDialogWindow_EditArticle.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Normal;
        //        }
        //    }
        //}
    }


    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        //string UserName = Tb_UserName.Text.Trim();
        //string Password = Tb_Password.Text.Trim();

        //DB db = new DB();
        //if (db.ValidateUser(UserName, Password))
        //{
        //    // Initialize and Set Sessions
        //    List<string> Permissions = new List<string>();

        //    DataTable RoleIds = db.GetRolesForUserName(UserName);
        //    if (RoleIds.Rows.Count > 0)
        //    {
        //        // Get All of Permissions In Roles
        //        foreach (DataRow RoleId in RoleIds.Rows)
        //        {
        //            Permissions = db.PermissionInRolesSelect(RoleId[0].ToString(), Permissions);
        //        }

        //        // If it has Permission Login
        //        if (Permissions.Contains("1"))
        //        {
        //            Session["UserName"] = UserName;
        //            Session["UserId"] = db.GetUserId(UserName);
        //            Session["Permissions"] = Permissions;

        //            // Redirect
        //            if (string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
        //            {
        //                FormsAuthentication.SetAuthCookie(UserName, false);
        //                Response.Redirect("~/");
        //            }
        //            else
        //                FormsAuthentication.RedirectFromLoginPage(UserName, false);
        //        }
        //        else
        //        {
        //            LB_LoginError.Text = "شما مجوز ورود به سامانه را ندارید";
        //        }
        //    }
        //    else
        //    {
        //        LB_LoginError.Text = "برای کاربر هیچ نقشی تعریف نشده است";
        //    }
        //}
        //else
        //{
        //    LB_LoginError.Text = "اطلاعات صحیح نمی باشد.";
        //}
    }


}
