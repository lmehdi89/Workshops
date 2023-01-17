using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class _Articles : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // ***** <ACL> *****
        if (Session["UserName"] == null)
        {
            Response.Redirect("/");
        }
        if (Session["Permissions"] != null)
        {
            List<string> Permissions = (List<string>)Session["Permissions"];

            if (!Permissions.Contains("12"))
            {
                Response.Redirect("~/");
            }
        }
        // ***** </ACL> *****

        // Change Filter Menu Texts
        Helper.ChangeFilterMenu(RadGrid1.FilterMenu);
        Helper.ChangeFilterMenu(RadGrid2.FilterMenu);
    }

    protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        DB db = new DB();
        DataTable dt = db.ArticlesSelect();

        // Modify Data, Convert Date to ShamsiDate
        dt.Columns.Add("CreatedShamsi", typeof(string));
        dt.Columns.Add("ModifiedShamsi", typeof(string));
        foreach (DataRow row in dt.Rows)
        {
            row["CreatedShamsi"] = Helper.DateToShamsi(row["Created"]);
            if (row["Modified"] != DBNull.Value)
            {
                row["ModifiedShamsi"] = Helper.DateToShamsi(row["Modified"]);
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

        // Limit Texts Size
        if (e.Item is GridDataItem)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item["Title"].Text.Length > 30)
            {
                item["Title"].Text = item["Title"].Text.Substring(0, 30) + "...";
            }

            if (item["Contents"].Text.Length > 50)
            {
                item["Contents"].Text = item["Contents"].Text.Substring(0, 50) + "...";
            }
        }
    }

    protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        // Get the Selected id
        string SelectedArticleId = (e.Item as GridEditableItem).GetDataKeyValue("ArticleId").ToString();

        if (SelectedArticleId != string.Empty && Helper.IsNumeric(SelectedArticleId))
        {
            // Delete the UserId
            DB db = new DB();
            if (db.ArticlesDelete(SelectedArticleId) == 0)
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
            TextBox box = filteringItem["Title"].Controls[0] as TextBox;
            box.Width = Unit.Percentage(60);
            box = filteringItem["Contents"].Controls[0] as TextBox;
            box.Width = Unit.Percentage(60);
            box = filteringItem["CreatedShamsi"].Controls[0] as TextBox;
            box.Width = Unit.Percentage(60);
            box = filteringItem["ModifiedShamsi"].Controls[0] as TextBox;
            box.Width = Unit.Percentage(60);
        }
    }

    protected void BtnRefresh_Click(object sender, EventArgs e)
    {
        RadGrid1.Rebind();
    }

    protected void BtnAddNewArticle_Click(object sender, EventArgs e)
    {
        // Set Error Label LB_AddNewUser to empty
        LB_AddNewArticle.Text = string.Empty;

        // Show Dialog
        WebDialogWindow_AddNewArticle.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Normal;
    }

    protected void BtnSaveNewArticle_Click(object sender, EventArgs e)
    {
        string Title = Tb_NewTitle.Text.Trim();
        string Category = DDL_NewCategory.SelectedValue;
        string Contents = Editor_NewContents.Content;
        bool IsActive = CHK_NewIsActive.Checked;

        if (Title != string.Empty &&
            Category != null)
        {
            // Save Image
            UploadedFileCollection UploadedFiles = AsyncUpload1.UploadedFiles;
            string ImagePath = "";
            if (UploadedFiles.Count > 0)
            {
                string GenerateFileName = DateTime.Now.ToString("yyyyMMdd-HHmmss_");
                UploadedFiles[0].SaveAs(Server.MapPath("/Uploads/") + GenerateFileName + UploadedFiles[0].GetName(), false);
                ImagePath = "/Uploads/" + GenerateFileName + UploadedFiles[0].GetName();
            }

            // Insert New Row
            DB db = new DB();
            if (db.ArticlesInsert(Title, Category, ImagePath, Contents, IsActive, Session["UserId"].ToString()) == 0)
            {
                RadNotification1.Show("عملیات با موفقیت انجام شد");

                // Refresh the Roles Grid
                RadGrid1.Rebind();

                // Clear the Textbox
                Tb_NewTitle.Text = string.Empty;
                DDL_NewCategory.SelectedIndex = -1;
                AsyncUpload1.UploadedFiles.Clear();
                Editor_NewContents.Content = string.Empty;
                CHK_NewIsActive.Checked = true;

                // Close the WebDialogWindow
                WebDialogWindow_AddNewArticle.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Hidden;
            }
            else
            {
                LB_AddNewArticle.Text = "خطا در انجام عملیات";
            }
        }
        else
        {
            LB_AddNewArticle.Text = "لطفا فیلدهای عنوان و محتوا را پر نمایید";
        }
    }

    protected void BtnEditArticle_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "EditArticle" && e.CommandArgument.ToString().Length > 0)
        {
            // Clear Error Label
            LB_EditArticle.Text = string.Empty;

            // Clear Edit Form
            Tb_EditTitle.Text = string.Empty;
            DDL_EditCategory.SelectedIndex = -1;
            EditAsyncUpload1.UploadedFiles.Clear();
            CHK_ImagePathDelete.Checked = false;
            Editor_EditContents.Content = string.Empty;
            CHK_EditIsActive.Checked = true;

            // Get the Selected ArticleId
            string SelectedArticleId = e.CommandArgument.ToString();

            // Populate Edit Form
            if (SelectedArticleId != string.Empty && Helper.IsNumeric(SelectedArticleId))
            {
                DB db = new DB();
                DataTable dt = db.GetArticleInfo(SelectedArticleId);
                if (dt.Rows.Count > 0)
                {
                    Tb_EditTitle.Text = dt.Rows[0]["Title"].ToString();
                    DDL_EditCategory.SelectedValue = dt.Rows[0]["CodeCategory"].ToString();
                    //EditAsyncUpload1.UploadedFiles[0].
                    Editor_EditContents.Content = dt.Rows[0]["Contents"].ToString();
                    CHK_EditIsActive.Checked = dt.Rows[0]["IsActive"].ToString() == "True" ? true : false;

                    // Set Dialog Header Caption
                    LB_SelectedArticleId.Text = SelectedArticleId;
                    WebDialogWindow_EditArticle.Header.CaptionText = "ویرایش مطلب : " + SelectedArticleId;

                    // Show Dialog
                    WebDialogWindow_EditArticle.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Normal;
                }
            }
        }
    }

    protected void BtnSaveEditArticle_Click(object sender, EventArgs e)
    {
        string Title = Tb_EditTitle.Text.Trim();
        string Category = DDL_EditCategory.SelectedValue;
        string Contents = Editor_EditContents.Content;
        bool IsActive = CHK_EditIsActive.Checked;

        if (Title != string.Empty &&
            Category != null)
        {
            // Save Image
            UploadedFileCollection UploadedFiles = EditAsyncUpload1.UploadedFiles;
            string ImagePath = "";

            if (CHK_ImagePathDelete.Checked == false)
            {
                ImagePath = "NoEdit";
            }
            else if (UploadedFiles.Count > 0)
            {
                string GenerateFileName = DateTime.Now.ToString("yyyyMMdd-HHmmss_");
                UploadedFiles[0].SaveAs(Server.MapPath("/Uploads/") + GenerateFileName + UploadedFiles[0].GetName(), false);
                ImagePath = "/Uploads/" + GenerateFileName + UploadedFiles[0].GetName();
            }

            // Update Row
            DB db = new DB();
            if (db.ArticlesUpdate(LB_SelectedArticleId.Text, Title, Category, ImagePath, Contents, IsActive, Session["UserId"].ToString()) == 0)
            {
                RadNotification1.Show("عملیات با موفقیت انجام شد");

                // Refresh the Roles Grid
                RadGrid1.Rebind();

                // Close the WebDialogWindow
                WebDialogWindow_EditArticle.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Hidden;
            }
            else
            {
                LB_EditArticle.Text = "خطا در انجام عملیات";
            }
        }
        else
        {
            LB_EditArticle.Text = "لطفا فیلدهای عنوان و محتوا را پر نمایید";
        }
    }

    protected void CHK_ImagePathDelete_CheckedChanged(object sender, EventArgs e)
    {
        if (CHK_ImagePathDelete.Checked)
        {
            EditAsyncUpload1.Enabled = true;
        }
        else
        {
            EditAsyncUpload1.Enabled = false;
        }
    }

    protected void DDL_EditCategory_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (DDL_EditCategory.SelectedValue == "footer")
        {
            LB_EditForRightLinks.Text = "برای ایجاد لینک در قسمت پیوندها، عنوان لینک را در فیلد 'عنوان' و آدرس لینک را در فیلد 'محتوا' وارد نمایید";
        }
        else
        {
            LB_EditForRightLinks.Text = string.Empty;
        }
    }

    protected void DDL_NewCategory_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (DDL_NewCategory.SelectedValue == "right_links")
        {
            LB_NewForRightLinks.Text = "برای ایجاد لینک در قسمت پیوندها، عنوان لینک را در فیلد 'عنوان' و آدرس لینک را در فیلد 'محتوا' وارد نمایید";
        }
        else
        {
            LB_NewForRightLinks.Text = string.Empty;
        }
    }

    protected void BtnEditGallery_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "EditGallery" && e.CommandArgument.ToString().Length > 0)
        {
            // Clear Error Label
            LB_EditGallery.Text = string.Empty;

            // Get the Selected ArticleId
            string SelectedArticleId2 = e.CommandArgument.ToString();

            // Populate Edit Form
            if (SelectedArticleId2 != string.Empty && Helper.IsNumeric(SelectedArticleId2))
            {
                // Set Dialog Header Caption
                LB_SelectedArticleId2.Text = SelectedArticleId2;
                WebDialogWindow_EditArticle.Header.CaptionText = "ویرایش گالری : " + SelectedArticleId2;

                // Show Dialog
                WebDialogWindow_EditGallery.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Normal;

                RadGrid2.Rebind();
            }
        }
    }

    protected void BtnSaveEditGallery_Click(object sender, EventArgs e)
    {
        // Get the Selected ArticleId
        string SelectedArticleId2 = LB_SelectedArticleId2.Text;

        if (SelectedArticleId2 != string.Empty && Helper.IsNumeric(SelectedArticleId2) && GalleryAsyncUpload1.UploadedFiles.Count != 0)
        {
            // Save Image
            string ImageDesc = Tb_ImageDesc.Text.Trim();

            UploadedFileCollection UploadedFiles = GalleryAsyncUpload1.UploadedFiles;
            string ImagePath = "";
            if (UploadedFiles.Count > 0)
            {
                string GenerateFileName = DateTime.Now.ToString("yyyyMMdd-HHmmss_");
                UploadedFiles[0].SaveAs(Server.MapPath("/Uploads/") + GenerateFileName + UploadedFiles[0].GetName(), false);
                ImagePath = "/Uploads/" + GenerateFileName + UploadedFiles[0].GetName();
            }

            // Insert New Row
            DB db = new DB();
            if (db.GalleryInsert(SelectedArticleId2, ImagePath, ImageDesc, Session["UserId"].ToString()) == 0)
            {
                LB_EditGallery.Text = "عملیات با موفقیت انجام شد";

                // Refresh the Roles Grid
                RadGrid2.Rebind();

                // Clear the Textbox
                Tb_ImageDesc.Text = string.Empty;
                GalleryAsyncUpload1.UploadedFiles.Clear();
            }
            else
            {
                LB_EditGallery.Text = "خطا در انجام عملیات";
            }
        }
        else
        {
            LB_EditGallery.Text = "هیچ فایلی انتخاب نشده";
        }
    }

    protected void RadGrid2_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        DB db = new DB();
        DataTable dt = db.GallerySelect(LB_SelectedArticleId2.Text);

        dt.Columns.Add("CreatedShamsi", typeof(string));
        foreach (DataRow row in dt.Rows)
        {
            row["CreatedShamsi"] = Helper.DateToShamsi(row["Created"]);
        }

        RadGrid2.DataSource = dt;
    }

    protected void RadGrid2_ItemDataBound(object sender, GridItemEventArgs e)
    {
        // Set the Row Numbers
        if (e.Item is GridDataItem)
        {
            Label lbl = e.Item.FindControl("LB_RowNumber") as Label;
            lbl.Text = (e.Item.ItemIndex + 1).ToString();
        }
    }

    protected void RadGrid2_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        // Get Edited Items
        Hashtable table = new Hashtable();
        (e.Item as GridEditableItem).ExtractValues(table);

        if (table["ImageDesc"] != null)
        {
            string id = (e.Item as GridEditableItem).GetDataKeyValue("id").ToString();
            string ImageDesc = table["ImageDesc"].ToString();

            //Update Row
            DB db = new DB();
            if (db.GalleryUpdate(id, ImageDesc, Session["UserId"].ToString()) == 0)
            {
                LB_EditGallery.Text = "عملیات با موفقیت انجام شد";
            }
            else
            {
                LB_EditGallery.Text = "خطا در انجام عملیات";
            }
        }
    }

    protected void RadGrid2_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        // Get the Selected id
        string id = (e.Item as GridEditableItem).GetDataKeyValue("id").ToString();

        // Delete the id
        DB db = new DB();
        if (db.GalleryDelete(id) == 0)
        {
            // Delete File


            LB_EditGallery.Text = "عملیات با موفقیت انجام شد";

            // Refresh the Grid    
            RadGrid2.Rebind();
        }
        else
        {
            LB_EditGallery.Text = "خطا در انجام عملیات";
        }
    }

    protected void RadGrid2_ItemCreated(object sender, GridItemEventArgs e)
    {
        // Change Filter TextBox Width
        if (e.Item is GridFilteringItem)
        {
            GridFilteringItem filteringItem = e.Item as GridFilteringItem;
            TextBox box = filteringItem["ImageDesc"].Controls[0] as TextBox;
            box.Width = Unit.Percentage(80);
            box = filteringItem["CreatedShamsi"].Controls[0] as TextBox;
            box.Width = Unit.Percentage(80);
        }
    }

    protected void BtnRefresh2_Click(object sender, EventArgs e)
    {
        RadGrid2.Rebind();
    }

}
