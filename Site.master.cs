using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class SiteMaster : MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // ***** <ACL> *****
        if (Session["Permissions"] != null)
        {
            List<string> Permissions = (List<string>)Session["Permissions"];

            //if (Permissions.Contains("2"))
            //{
            //    RadToolBar1.Items.FindItemByValue("Users").Visible = true;
            //}
        }
        // ***** </ACL> *****

        // Version
        LB_Version.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        LB_ReleaseDate.Text = ((AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(
            Assembly.GetExecutingAssembly(), typeof(AssemblyCopyrightAttribute), false)).Copyright;

        // Change Filter Menu Texts

    }

  
}
