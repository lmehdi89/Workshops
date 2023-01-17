<%@ Page Title="مطالب" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="_Articles" CodeBehind="Articles.aspx.cs" %>

<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.1028, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <br />
    <br />
    <div class="Pages">
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadAjaxPanel1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadAjaxPanel1" LoadingPanelID="LoadingPanel1"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>

        <telerik:RadAjaxLoadingPanel runat="server" ID="LoadingPanel1" InitialDelayTime="1000">
            <asp:Label ID="Label2" runat="server" ForeColor="#666666" Font-Names="Tahoma" Font-Size="13px" BackColor="#d8cbef">در حال پردازش...</asp:Label>
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">

            <telerik:RadNotification ID="RadNotification1" runat="server" ShowCloseButton="true" KeepOnMouseOver="true" OffsetY="40"
                AutoCloseDelay="5000" Position="TopCenter" Animation="Fade" AnimationDuration="500" ContentIcon="none" TitleIcon="none"
                EnableRoundedCorners="true" EnableShadow="true" CssClass="MyNotif">
            </telerik:RadNotification>

            <telerik:RadGrid ID="RadGrid1" runat="server" PageSize="10" MasterTableView-EditMode="PopUp"
                OnNeedDataSource="RadGrid1_NeedDataSource" OnItemCreated="RadGrid1_ItemCreated" OnItemDataBound="RadGrid1_ItemDataBound"
                OnDeleteCommand="RadGrid1_DeleteCommand"
                AllowPaging="True" AllowSorting="true" RenderMode="Lightweight" Skin="Bootstrap" HeaderStyle-HorizontalAlign="Center">
                <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
                <MasterTableView AutoGenerateColumns="false" AllowMultiColumnSorting="true" AllowFilteringByColumn="true" TableLayout="Fixed"
                    DataKeyNames="ArticleId" CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnFirstPage"
                    Font-Names="Tahoma" Font-Size="12px">
                    <CommandItemSettings ShowExportToCsvButton="false" ShowExportToExcelButton="false" ShowExportToPdfButton="false" ShowExportToWordButton="false" />
                    <NoRecordsTemplate>
                        <div style="text-align: right; padding: 5px; color: #383779;">
                            هیچ رکوردی برای نمایش وجود ندارد!
                        </div>
                    </NoRecordsTemplate>
                    <CommandItemTemplate>
                        <br />
                        <telerik:RadButton RenderMode="Lightweight" ID="BtnAddNewArticle" runat="server" OnClick="BtnAddNewArticle_Click"
                            Text="افزودن مطلب" Font-Names="Tahoma">
                            <Icon PrimaryIconCssClass="rbAdd" />
                        </telerik:RadButton>
                        <telerik:RadButton RenderMode="Lightweight" ID="BtnRefresh" runat="server" OnClick="BtnRefresh_Click"
                            Text="بارگذاری مجدد" Font-Names="Tahoma">
                            <Icon PrimaryIconCssClass="rbRefresh" />
                        </telerik:RadButton>

                        <p style="float: left; font-size: 20px;">مدیریت مطالب</p>
                        <hr />

                    </CommandItemTemplate>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="RowNumber" HeaderText="#" AllowFiltering="false">
                            <ItemTemplate>
                                <asp:Label ID="LB_RowNumber" runat="server" Width="15px" />
                            </ItemTemplate>
                            <HeaderStyle Width="15px" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridNumericColumn DataField="ArticleId" HeaderText="کد مطلب" SortExpression="ArticleId"
                            UniqueName="ArticleId" ItemStyle-HorizontalAlign="Center" Visible="false">
                            <HeaderStyle Width="40px" />
                        </telerik:GridNumericColumn>
                        <telerik:GridBoundColumn DataField="Title" HeaderText="عنوان" SortExpression="Title" UniqueName="Title"
                            ItemStyle-HorizontalAlign="Center" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                            <HeaderStyle Width="75px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Contents" HeaderText="محتوا" SortExpression="Contents" UniqueName="Contents"
                            ItemStyle-HorizontalAlign="Center" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" Visible="false">
                            <HeaderStyle Width="65px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="دسته بندی" ItemStyle-HorizontalAlign="Center" UniqueName="Category" DataField="CodeCategory">
                            <FilterTemplate>
                                <telerik:RadComboBox RenderMode="Lightweight" runat="server" ID="CB_Category_Filter" DataSourceID="DS_Category"
                                    DataTextField="NameCategory" DataValueField="CodeCategory" Width="100%" Height="300" AppendDataBoundItems="true"
                                    SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("Category").CurrentFilterValue %>'
                                    OnClientSelectedIndexChanged="CategoryIndexChanged" EnableTextSelection="false">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="--همه موارد--" />
                                    </Items>
                                </telerik:RadComboBox>
                                <telerik:RadScriptBlock runat="server">
                                    <script type="text/javascript">
                                        function CategoryIndexChanged(sender, args) {
                                            var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                            tableView.filter("Category", args.get_item().get_value(), "EqualTo");
                                        }
                                    </script>
                                </telerik:RadScriptBlock>
                            </FilterTemplate>
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "NameCategory")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <telerik:RadComboBox RenderMode="Lightweight" runat="server" ID="CB_Category" DataSourceID="DS_Category"
                                    DataTextField="NameCategory" DataValueField="CodeCategory" SelectedValue='<%#Bind("CodeCategory") %>'
                                    Width="90%" Height="300">
                                </telerik:RadComboBox>
                            </EditItemTemplate>
                            <HeaderStyle Width="80px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridCheckBoxColumn DataField="IsActive" HeaderText="فعال" SortExpression="IsActive"
                            UniqueName="IsActive" ItemStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true">
                            <HeaderStyle Width="30px" />
                        </telerik:GridCheckBoxColumn>
                        <telerik:GridBoundColumn DataField="CreatedShamsi" HeaderText="تاریخ ایجاد" SortExpression="CreatedShamsi" ReadOnly="true"
                            UniqueName="CreatedShamsi" DataFormatString="{00:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center"
                            CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                            <HeaderStyle Width="45px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ModifiedShamsi" HeaderText="تاریخ ویرایش" SortExpression="ModifiedShamsi" ReadOnly="true"
                            UniqueName="ModifiedShamsi" DataFormatString="{00:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center"
                            CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                            <HeaderStyle Width="45px" />
                        </telerik:GridBoundColumn>

                        <telerik:GridTemplateColumn UniqueName="EditGallery" HeaderText="گالری تصاویر" AllowFiltering="false" Visible="true" HeaderStyle-Font-Size="10px">
                            <ItemTemplate>
                                <telerik:RadButton RenderMode="Lightweight" ID="BtnEditGallery" runat="server" Width="20px" Height="20px"
                                    CommandName="EditGallery" CommandArgument='<%# Eval("ArticleId") %>' OnCommand="BtnEditGallery_Command"
                                    ToolTip="گالری تصاویر">
                                    <Image ImageUrl="none" IsBackgroundImage="true"></Image>
                                    <Icon PrimaryIconUrl="/images/EditGallery.png" PrimaryIconWidth="20px" PrimaryIconHeight="20px"
                                        PrimaryIconTop="0" PrimaryIconLeft="-15px"></Icon>
                                </telerik:RadButton>
                            </ItemTemplate>
                            <HeaderStyle Width="20px" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="EditArticle" HeaderText="ویرایش" AllowFiltering="false" Visible="true" HeaderStyle-Font-Size="10px">
                            <ItemTemplate>
                                <telerik:RadButton RenderMode="Lightweight" ID="BtnEditArticle" runat="server" Width="20px" Height="20px"
                                    CommandName="EditArticle" CommandArgument='<%# Eval("ArticleId") %>' OnCommand="BtnEditArticle_Command"
                                    ToolTip="ویرایش مطلب">
                                    <Image ImageUrl="none" IsBackgroundImage="true"></Image>
                                    <Icon PrimaryIconUrl="/images/EditArticle.png" PrimaryIconWidth="16px" PrimaryIconHeight="16px"
                                        PrimaryIconTop="2px" PrimaryIconLeft="-12px"></Icon>
                                </telerik:RadButton>
                            </ItemTemplate>
                            <HeaderStyle Width="20px" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridButtonColumn CommandName="Delete" Text="حذف" UniqueName="DeleteColumn" HeaderText="حذف" Visible="true"
                            HeaderStyle-Font-Size="11px" ConfirmDialogType="Classic" ConfirmText="آیا از حذف این آیتم اطمینان دارید؟">
                            <HeaderStyle Width="15px" />
                        </telerik:GridButtonColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings AllowColumnsReorder="false" AllowColumnHide="false" AllowDragToGroup="false">
                    <Selecting AllowRowSelect="true" />
                    <Scrolling AllowScroll="false" UseStaticHeaders="true" />
                </ClientSettings>
                <PagerStyle PageButtonCount="7" Mode="NextPrevNumericAndAdvanced" AlwaysVisible="true" Font-Names="Tahoma" Font-Size="12px"
                    FirstPageImageUrl="/images/PagingFirst.png" LastPageImageUrl="/images/PagingLast.png"
                    NextPageImageUrl="/images/PagingNext.png" PrevPageImageUrl="/images/PagingPrev.png"
                    PageSizeLabelText="تعداد رکورد در هر صفحه" PagerTextFormat="{4} صفحه {0} از {1} - نمایش رکوردهای {2} تا {3} از تعداد کل {5}" />
                <FilterMenu RenderMode="Lightweight">
                </FilterMenu>
                <HeaderContextMenu RenderMode="Lightweight">
                </HeaderContextMenu>
            </telerik:RadGrid>

            <%-- Dialog For Add New Article --%>
            <ig:WebDialogWindow runat="server" ID="WebDialogWindow_AddNewArticle" InitialLocation="Centered"
                Height="850px" Width="1000px" Modal="true" WindowState="Hidden" Style="line-height: normal" ModalBackgroundCssClass="MyModal">
                <Header CaptionAlignment="Center" CaptionText="افزودن مطلب">
                    <CloseBox Visible="true" />
                </Header>
                <ContentPane BackColor="White">
                    <Template>
                        <div style="position: relative; padding: 20px;">
                            <br />
                            <table border="0" class="MyTable">
                                <colgroup>
                                    <col style="width: 200px;" />
                                    <col style="width: 800px;" />
                                </colgroup>
                                <tr>
                                    <td>عنوان : </td>
                                    <td>
                                        <telerik:RadTextBox runat="server" RenderMode="Lightweight" ID="Tb_NewTitle" Width="400"></telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>دسته بندی : </td>
                                    <td>
                                        <telerik:RadDropDownList RenderMode="Lightweight" runat="server" ID="DDL_NewCategory"
                                            DataSourceID="DS_Category" Width="300px" AppendDataBoundItems="true"
                                            CssClass="MyDropDownList" DataTextField="NameCategory" DataValueField="CodeCategory"
                                            AutoPostBack="true" OnSelectedIndexChanged="DDL_NewCategory_SelectedIndexChanged">
                                            <Items>
                                                <telerik:DropDownListItem Text="" />
                                            </Items>
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <%# DataBinder.Eval(Container, "Text") %>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </telerik:RadDropDownList>

                                        <asp:Label ID="LB_NewForRightLinks" Text="" runat="server" Style="font-size: 14px; color: red;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>تصویر : </td>
                                    <td>
                                        <telerik:RadAsyncUpload RenderMode="Lightweight" ID="AsyncUpload1" runat="server"
                                            MaxFileSize="1048576" AllowedFileExtensions="jpg,png,gif,bmp" MaxFileInputsCount="1"
                                            Localization-Cancel="لغو آپلود" Localization-Remove="حذف تصویر"
                                            Localization-Select="آپلود تصویر" />
                                        <asp:Label ID="Label1" Text="* حجم تصویر حداکثر 1 مگابایت می باشد" runat="server"
                                            Style="font-size: 12px; color: red;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>محتوا : </td>
                                    <td>
                                        <telerik:RadEditor RenderMode="Lightweight" runat="server" ID="Editor_NewContents"
                                            Skin="Silk" SkinID="DefaultSetOfTools" Height="450px" CssClass="MyEditor">
                                            <ImageManager EnableAsyncUpload="true" RenderMode="Lightweight"
                                                ViewPaths="/Uploads" UploadPaths="/Uploads" DeletePaths="/Uploads"></ImageManager>
                                        </telerik:RadEditor>
                                    </td>
                                </tr>
                                <tr>
                                    <td>فعال : </td>
                                    <td>
                                        <asp:CheckBox runat="server" ID="CHK_NewIsActive" Width="200" Checked="true"></asp:CheckBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center;">
                                        <asp:Label runat="server" ID="LB_AddNewArticle" Text="" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center;">
                                        <telerik:RadButton runat="server" ID="BtnSaveNewArticle" Text="تایید" RenderMode="Lightweight" OnClick="BtnSaveNewArticle_Click"></telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </Template>
                </ContentPane>
            </ig:WebDialogWindow>

            <%-- Dialog For Edit Article --%>
            <ig:WebDialogWindow runat="server" ID="WebDialogWindow_EditArticle" InitialLocation="Centered"
                Height="850px" Width="1000px" Modal="true" WindowState="Hidden" Style="line-height: normal" ModalBackgroundCssClass="MyModal">
                <Header CaptionAlignment="Center" CaptionText="ویرایش مطلب">
                    <CloseBox Visible="true" />
                </Header>
                <ContentPane BackColor="White">
                    <Template>
                        <div style="position: relative; padding: 20px;">
                            <fieldset>
                                <legend>ویرایش مطلب</legend>
                                <br />
                                <table border="0" class="MyTable">
                                    <colgroup>
                                        <col style="width: 200px;" />
                                        <col style="width: 800px;" />
                                    </colgroup>
                                    <tr>
                                        <td>عنوان : </td>
                                        <td>
                                            <telerik:RadTextBox runat="server" RenderMode="Lightweight" ID="Tb_EditTitle" Width="400"></telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>دسته بندی : </td>
                                        <td>
                                            <telerik:RadDropDownList RenderMode="Lightweight" runat="server" ID="DDL_EditCategory"
                                                DataSourceID="DS_Category" Width="300px" AppendDataBoundItems="true"
                                                CssClass="MyDropDownList" DataTextField="NameCategory" DataValueField="CodeCategory"
                                                AutoPostBack="true" OnSelectedIndexChanged="DDL_EditCategory_SelectedIndexChanged">
                                                <Items>
                                                    <telerik:DropDownListItem Text="" />
                                                </Items>
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <%# DataBinder.Eval(Container, "Text") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </telerik:RadDropDownList>

                                            <asp:Label ID="LB_EditForRightLinks" Text="" runat="server" Style="font-size: 14px; color: red;"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>تصویر : </td>
                                        <td>
                                            <asp:CheckBox runat="server" ID="CHK_ImagePathDelete" Width="200" Checked="false"
                                                Text="حذف تصویر فعلی" AutoPostBack="true"
                                                OnCheckedChanged="CHK_ImagePathDelete_CheckedChanged"></asp:CheckBox>

                                            <telerik:RadAsyncUpload RenderMode="Lightweight" ID="EditAsyncUpload1" runat="server"
                                                MaxFileSize="1048576" AllowedFileExtensions="jpg,png,gif,bmp" MaxFileInputsCount="1"
                                                Localization-Cancel="لغو آپلود" Localization-Remove="حذف تصویر"
                                                Localization-Select="آپلود تصویر" Enabled="false" />

                                            <asp:Label ID="Label3" Text="* حجم تصویر حداکثر 1 مگابایت می باشد" runat="server"
                                                Style="font-size: 12px; color: red;"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>محتوا : </td>
                                        <td>
                                            <telerik:RadEditor RenderMode="Lightweight" runat="server" ID="Editor_EditContents"
                                                Skin="Silk" SkinID="DefaultSetOfTools" Height="450px" CssClass="MyEditor">
                                                <ImageManager EnableAsyncUpload="true" RenderMode="Lightweight"
                                                    ViewPaths="~/Uploads" UploadPaths="~/Uploads" DeletePaths="~/Uploads"></ImageManager>
                                            </telerik:RadEditor>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>فعال : </td>
                                        <td>
                                            <asp:CheckBox runat="server" ID="CHK_EditIsActive" Width="200" Checked="true"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <asp:Label runat="server" ID="LB_EditArticle" Text="" ForeColor="Red"></asp:Label>
                                            <asp:Label runat="server" ID="LB_SelectedArticleId" Text="" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <telerik:RadButton runat="server" ID="BtnSaveEditArticle" Text="تایید" RenderMode="Lightweight" OnClick="BtnSaveEditArticle_Click"></telerik:RadButton>
                                        </td>
                                    </tr>
                                </table>

                            </fieldset>
                        </div>
                    </Template>
                </ContentPane>
            </ig:WebDialogWindow>

            <%-- Dialog For Edit Gallery --%>
            <ig:WebDialogWindow runat="server" ID="WebDialogWindow_EditGallery" InitialLocation="Centered"
                Height="700px" Width="900px" Modal="true" WindowState="Hidden" Style="line-height: normal" ModalBackgroundCssClass="MyModal">
                <Header CaptionAlignment="Center" CaptionText="گالری تصاویر">
                    <CloseBox Visible="true" />
                </Header>
                <ContentPane BackColor="White">
                    <Template>
                        <div class="gridHeader">
                            <table border="0" cellspacing="2" cellpadding="2" style="width: 100%;">
                                <colgroup>
                                    <col style="width: 10%;" />
                                    <col style="width: 30%;" />
                                    <col style="width: 10%;" />
                                    <col style="width: 40%;" />
                                    <col style="width: 10%;" />
                                </colgroup>
                                <tr>
                                    <td class="gridHeader_label">انتخاب تصویر : </td>
                                    <td>
                                        <telerik:RadAsyncUpload RenderMode="Lightweight" ID="GalleryAsyncUpload1" runat="server"
                                            MaxFileSize="1048576" AllowedFileExtensions="jpg,png,gif,bmp" MaxFileInputsCount="1"
                                            Localization-Cancel="لغو آپلود" Localization-Remove="حذف تصویر"
                                            Localization-Select="آپلود" Width="90%" />
                                    </td>
                                    <td class="gridHeader_label">توضیحات تصویر : </td>
                                    <td>
                                        <telerik:RadTextBox runat="server" RenderMode="Lightweight" ID="Tb_ImageDesc" Width="90%"></telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadButton runat="server" ID="BtnSaveEditGallery" Text="تایید" RenderMode="Lightweight"
                                            OnClick="BtnSaveEditGallery_Click">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="Label5" Text="* حجم تصویر حداکثر 1 مگابایت می باشد" runat="server"
                                            Style="font-size: 12px; color: red;"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <asp:Label runat="server" ID="LB_EditGallery" Text="" ForeColor="BlueViolet"></asp:Label>
                            <asp:Label runat="server" ID="LB_SelectedArticleId2" Text="" Visible="false"></asp:Label>
                        </div>

                        <telerik:RadGrid ID="RadGrid2" runat="server" MasterTableView-EditMode="InPlace"
                            OnNeedDataSource="RadGrid2_NeedDataSource" OnItemCreated="RadGrid2_ItemCreated" OnItemDataBound="RadGrid2_ItemDataBound"
                            OnUpdateCommand="RadGrid2_UpdateCommand" OnDeleteCommand="RadGrid2_DeleteCommand"
                            AllowPaging="True" AllowSorting="true" RenderMode="Lightweight" Skin="Bootstrap" HeaderStyle-HorizontalAlign="Center">
                            <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
                            <MasterTableView AutoGenerateColumns="false" AllowMultiColumnSorting="true" AllowFilteringByColumn="true" TableLayout="Fixed"
                                DataKeyNames="id" CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnFirstPage"
                                Font-Names="Tahoma" Font-Size="12px">
                                <NoRecordsTemplate>
                                    <div style="text-align: right; padding: 5px; color: #383779;">
                                        هیچ رکوردی برای نمایش وجود ندارد!
                                    </div>
                                </NoRecordsTemplate>
                                <CommandItemTemplate>
                                    <br />
                                    <telerik:RadButton RenderMode="Lightweight" ID="BtnRefresh2" runat="server" OnClick="BtnRefresh2_Click"
                                        Text="بارگذاری مجدد" Font-Names="Tahoma">
                                        <Icon PrimaryIconCssClass="rbRefresh" />
                                    </telerik:RadButton>

                                    <p style="float: left; font-size: 20px;">گالری تصاویر</p>
                                    <hr />

                                </CommandItemTemplate>
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="RowNumber" HeaderText="#" AllowFiltering="false">
                                        <ItemTemplate>
                                            <asp:Label ID="LB_RowNumber" runat="server" Width="15px" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="15px" />
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridNumericColumn DataField="id" HeaderText="کد" SortExpression="id"
                                        UniqueName="id" ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <HeaderStyle Width="40px" />
                                    </telerik:GridNumericColumn>
                                    <telerik:GridImageColumn DataImageUrlFields="ImagePath" HeaderText="تصویر" UniqueName="ImagePath"
                                        ItemStyle-HorizontalAlign="Center" ImageAlign="Middle" ImageWidth="100px"
                                        AllowFiltering="false" AllowSorting="false">
                                        <HeaderStyle Width="150px" />
                                    </telerik:GridImageColumn>
                                    <telerik:GridBoundColumn DataField="ImageDesc" HeaderText="توضیحات" SortExpression="ImageDesc" UniqueName="ImageDesc"
                                        ItemStyle-HorizontalAlign="Center" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                        <HeaderStyle Width="150px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CreatedShamsi" HeaderText="تاریخ ایجاد" SortExpression="CreatedShamsi" ReadOnly="true"
                                        UniqueName="CreatedShamsi" DataFormatString="{00:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center"
                                        CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                        <HeaderStyle Width="75px" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridEditCommandColumn UniqueName="EditColumn" HeaderText="ویرایش" EditText="ویرایش"
                                        HeaderStyle-Font-Size="11px" Visible="true">
                                        <HeaderStyle Width="30px" />
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridButtonColumn CommandName="Delete" Text="حذف" UniqueName="DeleteColumn" HeaderText="حذف" Visible="true"
                                        HeaderStyle-Font-Size="11px" ConfirmDialogType="Classic" ConfirmText="آیا از حذف این آیتم اطمینان دارید؟">
                                        <HeaderStyle Width="30px" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings AllowColumnsReorder="false" AllowColumnHide="false" AllowDragToGroup="false">
                                <Selecting AllowRowSelect="true" />
                                <Scrolling AllowScroll="false" UseStaticHeaders="true" />
                            </ClientSettings>
                            <FilterMenu RenderMode="Lightweight">
                            </FilterMenu>
                            <HeaderContextMenu RenderMode="Lightweight">
                            </HeaderContextMenu>
                        </telerik:RadGrid>

                    </Template>
                </ContentPane>
            </ig:WebDialogWindow>

            <asp:SqlDataSource ID="DS_Category" runat="server"
                ConnectionString="<%$ ConnectionStrings:DBConnection %>"
                SelectCommand="CategorySelect"
                SelectCommandType="StoredProcedure"></asp:SqlDataSource>

        </telerik:RadAjaxPanel>
    </div>
    <br />
    <br />
</asp:Content>
