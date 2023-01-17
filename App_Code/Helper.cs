using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public class Helper
{
    public Helper()
    {

    }

    public static void ChangeFilterMenu(GridFilterMenu menu)
    {
        menu.CssClass = "myFilterMenu";

        foreach (RadMenuItem item in menu.Items)
        {
            // Change Style
            item.CssClass = "myFilterMenuItem";

            // Change Texts
            switch (item.Text)
            {
                case "NoFilter":
                    item.Text = "--بدون فیلتر--";
                    break;
                case "Contains":
                    item.Text = "شامل می شود";
                    break;
                case "DoesNotContain":
                    item.Text = "شامل نمی شود";
                    break;
                case "StartsWith":
                    item.Text = "شروع شود با";
                    break;
                case "EndsWith":
                    item.Text = "ختم شود با";
                    break;
                case "EqualTo":
                    item.Text = "دقیقا برابر است با";
                    break;
                case "NotEqualTo":
                    item.Text = "دقیقا برابر نیست با";
                    break;
                case "GreaterThan":
                    item.Text = "بزرگتر است از";
                    break;
                case "LessThan":
                    item.Text = "کوچکتر است از";
                    break;
                case "GreaterThanOrEqualTo":
                    item.Text = "بزرگتر یا مساوی است از";
                    break;
                case "LessThanOrEqualTo":
                    item.Text = "کوچکتر یا مساوی است از";
                    break;
                case "Between":
                    item.Text = "مابین";
                    break;
                case "NotBetween":
                    item.Text = "مابین نیست";
                    break;
                case "IsEmpty":
                    item.Text = "خالی می باشد";
                    break;
                case "NotIsEmpty":
                    item.Text = "خالی نمی باشد";
                    break;
                case "IsNull":
                    item.Text = "تهی می باشد";
                    break;
                case "NotIsNull":
                    item.Text = "تهی نمی باشد";
                    break;

            }
        }
    }

    public static void ChangePagerStyle(GridPagerItem pager)
    {
        Button ChangePageSizeLinkButton = (Button)pager.FindControl("ChangePageSizeLinkButton");
        ChangePageSizeLinkButton.Text = "تایید";
        Button GoToPageLinkButton = (Button)pager.FindControl("GoToPageLinkButton");
        GoToPageLinkButton.Text = "تایید";
        Label PageOfLabel = (Label)pager.FindControl("PageOfLabel");
        PageOfLabel.Text = " از " + pager.Paging.PageCount.ToString();
        Label GoToPageLabel = (Label)pager.FindControl("GoToPageLabel");
        GoToPageLabel.Text = "برو به صفحه:";
        RadNumericTextBox GoToPageTextBox = (RadNumericTextBox)pager.FindControl("GoToPageTextBox");
        GoToPageTextBox.Height = Unit.Pixel(31);
        GoToPageTextBox.Width = Unit.Pixel(50);
        RadNumericTextBox ChangePageSizeTextBox = (RadNumericTextBox)pager.FindControl("ChangePageSizeTextBox");
        ChangePageSizeTextBox.Height = Unit.Pixel(31);
        ChangePageSizeTextBox.Width = Unit.Pixel(50);
    }

    public static string DateTimeToShamsi(object date)
    {
        System.Globalization.PersianCalendar Shamsi = new System.Globalization.PersianCalendar();
        DateTime thisDate = (DateTime)date;

        return new DateTime(
            Shamsi.GetYear(thisDate),
            Shamsi.GetMonth(thisDate),
            Shamsi.GetDayOfMonth(thisDate),
            Shamsi.GetHour(thisDate),
            Shamsi.GetMinute(thisDate),
            Shamsi.GetSecond(thisDate)
            ).ToString("yyyy/MM/dd HH:mm:ss");
    }

    public static string DateToShamsi(object date)
    {
        System.Globalization.PersianCalendar Shamsi = new System.Globalization.PersianCalendar();
        DateTime thisDate = (DateTime)date;

        // Edit Month
        int GetMonth = Shamsi.GetMonth(thisDate);
        string Month;
        if (GetMonth <= 9)
        {
            Month = "0" + GetMonth;
        }
        else
        {
            Month = GetMonth.ToString();
        }

        // Edit Day
        int GetDay = Shamsi.GetDayOfMonth(thisDate);
        string Day;
        if (GetDay <= 9)
        {
            Day = "0" + GetDay;
        }
        else
        {
            Day = GetDay.ToString();
        }

        return
            Shamsi.GetYear(thisDate) + "/" +
            Month + "/" +
            Day + "";
    }

    public static string CurrentShamsiYear()
    {
        System.Globalization.PersianCalendar Shamsi = new System.Globalization.PersianCalendar();
        DateTime thisDate = DateTime.Now;

        return
            Shamsi.GetYear(thisDate).ToString().Substring(2, 2);
    }

    public static string DateToMiladi(string date)
    {
        DateTime dt = new DateTime();
        System.Globalization.PersianCalendar Shamsi = new System.Globalization.PersianCalendar();

        int pYear = Convert.ToInt32(date.ToString().Substring(0, 4));
        int pMonth = Convert.ToInt32(date.ToString().Substring(5, 2));
        int pDay = Convert.ToInt32(date.ToString().Substring(8));

        dt = Shamsi.ToDateTime(pYear, pMonth, pDay, 0, 0, 0, 0);
        return dt.ToShortDateString();
    }

    public static bool IsNumeric(string str)
    {
        Int32 a;
        if (Int32.TryParse(str, out a))
        {
            // Value is numeric
            return true;
        }

        return false;
    }

}