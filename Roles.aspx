<%@ Page Title="نقش ها" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="_Roles" CodeBehind="Roles.aspx.cs" %>

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

            <telerik:RadGrid ID="RadGrid1" runat="server" PageSize="20" MasterTableView-EditMode="InPlace"
                OnNeedDataSource="RadGrid1_NeedDataSource" OnItemCreated="RadGrid1_ItemCreated" OnItemDataBound="RadGrid1_ItemDataBound"
                OnUpdateCommand="RadGrid1_UpdateCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
                AllowPaging="True" AllowSorting="true" RenderMode="Lightweight" Skin="Bootstrap" HeaderStyle-HorizontalAlign="Center">
                <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
                <MasterTableView AutoGenerateColumns="false" AllowMultiColumnSorting="true" AllowFilteringByColumn="true" TableLayout="Fixed"
                    DataKeyNames="RoleId" CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnFirstPage"
                    Font-Names="Tahoma" Font-Size="12px">
                    <CommandItemSettings ShowExportToCsvButton="false" ShowExportToExcelButton="false" ShowExportToPdfButton="false" ShowExportToWordButton="false" />
                    <NoRecordsTemplate>
                        <div style="text-align: right; padding: 5px; color: #383779;">
                            هیچ رکوردی برای نمایش وجود ندارد!
                        </div>
                    </NoRecordsTemplate>
                    <CommandItemTemplate>
                        <br />
                        <telerik:RadButton RenderMode="Lightweight" ID="BtnAddNewRole" runat="server" OnClick="BtnAddNewRole_Click"
                            Text="افزودن نقش" Font-Names="Tahoma">
                            <Icon PrimaryIconCssClass="rbAdd" />
                        </telerik:RadButton>
                        <telerik:RadButton RenderMode="Lightweight" ID="BtnRefresh" runat="server" OnClick="BtnRefresh_Click"
                            Text="بارگذاری مجدد" Font-Names="Tahoma">
                            <Icon PrimaryIconCssClass="rbRefresh" />
                        </telerik:RadButton>

                        <p style="float: left; font-size: 20px;">مدیریت نقش ها</p>
                        <hr />

                    </CommandItemTemplate>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="RowNumber" HeaderText="#" AllowFiltering="false">
                            <ItemTemplate>
                                <asp:Label ID="LB_RowNumber" runat="server" Width="7px" />
                            </ItemTemplate>
                            <HeaderStyle Width="7px" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridNumericColumn DataField="RoleId" HeaderText="کد نقش" SortExpression="RoleId"
                            UniqueName="RoleId" ItemStyle-HorizontalAlign="Center" Visible="false">
                            <HeaderStyle Width="50px" />
                        </telerik:GridNumericColumn>
                        <telerik:GridBoundColumn DataField="RoleName" HeaderText="نام نقش" SortExpression="RoleName"
                            UniqueName="RoleName" ColumnEditorID="RoleName" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle Width="150px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn DataField="IsActive" HeaderText="فعال" SortExpression="IsActive"
                            UniqueName="IsActive" ItemStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true">
                            <HeaderStyle Width="30px" />
                        </telerik:GridCheckBoxColumn>

                        <telerik:GridTemplateColumn UniqueName="PermissionsInRoles" HeaderText="مدیریت مجوزها" AllowFiltering="false"
                            HeaderStyle-Font-Size="13px" Visible="false">
                            <ItemTemplate>
                                <telerik:RadButton RenderMode="Lightweight" ID="BtnPermissionsInRoles" runat="server" Width="20px" Height="20px"
                                    CommandName="PermissionsInRoles" CommandArgument='<%# Eval("RoleId") %>' OnCommand="BtnPermissionsInRoles_Command"
                                    ToolTip="مدیریت مجوزها">
                                    <Image ImageUrl="none" IsBackgroundImage="true"></Image>
                                    <Icon PrimaryIconUrl="/images/PermissionsInRoles.png" PrimaryIconWidth="20px" PrimaryIconHeight="20px"
                                        PrimaryIconTop="-1px" PrimaryIconLeft="-15px"></Icon>
                                </telerik:RadButton>
                            </ItemTemplate>
                            <HeaderStyle Width="15px" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridTemplateColumn UniqueName="UsersInRoles" HeaderText="مدیریت کاربران" AllowFiltering="false"
                            HeaderStyle-Font-Size="13px" Visible="false">
                            <ItemTemplate>
                                <telerik:RadButton RenderMode="Lightweight" ID="BtnUsersInRoles" runat="server" Width="20px" Height="20px"
                                    CommandName="UsersInRoles" CommandArgument='<%# Eval("RoleId") %>' OnCommand="BtnUsersInRoles_Command"
                                    ToolTip="مدیریت کاربران">
                                    <Image ImageUrl="none" IsBackgroundImage="true"></Image>
                                    <Icon PrimaryIconUrl="/images/UsersInRoles.png" PrimaryIconWidth="20px" PrimaryIconHeight="20px"
                                        PrimaryIconTop="-2px" PrimaryIconLeft="-15px"></Icon>
                                </telerik:RadButton>
                            </ItemTemplate>
                            <HeaderStyle Width="15px" />
                        </telerik:GridTemplateColumn>

                        <telerik:GridEditCommandColumn UniqueName="EditColumn" HeaderText="ویرایش" EditText="ویرایش"
                            HeaderStyle-Font-Size="13px" Visible="false">
                            <HeaderStyle Width="15px" />
                        </telerik:GridEditCommandColumn>
                        <telerik:GridButtonColumn CommandName="Delete" Text="حذف" UniqueName="DeleteColumn" HeaderText="حذف"
                            HeaderStyle-Font-Size="13px" Visible="false"
                            ConfirmDialogType="Classic" ConfirmText="آیا از حذف این آیتم اطمینان دارید؟">
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

            <%-- Dialog For Add New Role --%>
            <ig:WebDialogWindow runat="server" ID="WebDialogWindow_AddNewRole" InitialLocation="Centered"
                Height="300px" Width="400px" Modal="true" WindowState="Hidden" Style="line-height: normal" ModalBackgroundCssClass="MyModal">
                <Header CaptionAlignment="Center" CaptionText="افزودن نقش">
                    <CloseBox Visible="true" />
                </Header>
                <ContentPane BackColor="White">
                    <Template>
                        <div style="position: relative; padding: 20px;">
                            <fieldset>
                                <legend>افزودن نقش</legend>
                                <br />
                                <table border="0" class="MyTable">
                                    <colgroup>
                                        <col style="width: 100px;" />
                                        <col style="width: 300px;" />
                                    </colgroup>
                                    <tr>
                                        <td>نام نقش : </td>
                                        <td>
                                            <telerik:RadTextBox runat="server" RenderMode="Lightweight" ID="Tb_NewRoleName" Width="200"></telerik:RadTextBox>
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
                                            <asp:Label runat="server" ID="LB_AddNewRole" Text="" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <telerik:RadButton runat="server" ID="BtnSaveNewRole" Text="تایید" RenderMode="Lightweight" OnClick="BtnSaveNewRole_Click"></telerik:RadButton>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                    </Template>
                </ContentPane>
            </ig:WebDialogWindow>

            <%-- Dialog For UsersInRoles --%>
            <ig:WebDialogWindow runat="server" ID="WebDialogWindow_UsersInRoles" InitialLocation="Centered"
                Height="425px" Width="550px" Modal="true" WindowState="Hidden" Style="line-height: normal" ModalBackgroundCssClass="MyModal">
                <Header CaptionAlignment="Center">
                    <CloseBox Visible="true" />
                </Header>
                <ContentPane BackColor="White">
                    <Template>
                        <div style="position: relative; padding: 20px;">
                            <fieldset>
                                <legend>مدیریت کاربران در نقش</legend>
                                <br />
                                <table border="0">
                                    <colgroup>
                                        <col style="width: 250px;" />
                                        <col style="width: 250px;" />
                                    </colgroup>
                                    <tr>
                                        <td>لیست کاربران موجود در نقش</td>
                                        <td>لیست سایر کاربران</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadListBox runat="server" ID="ListBox_UsersInRoles" RenderMode="Lightweight" AllowTransfer="true"
                                                AllowTransferOnDoubleClick="true" TransferToID="ListBox_UsersNotInRoles" dir="rtl"
                                                Width="235px" Height="200px" ButtonSettings-AreaWidth="35px" ButtonSettings-Position="Left">
                                            </telerik:RadListBox>
                                        </td>
                                        <td>
                                            <telerik:RadListBox runat="server" ID="ListBox_UsersNotInRoles" RenderMode="Lightweight"
                                                Width="200px" Height="200px" dir="rtl">
                                            </telerik:RadListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <asp:Label runat="server" ID="LB_SelectedRoleId" Text="" Visible="false"></asp:Label>
                                            <asp:Label runat="server" ID="LB_UsersInRoles" Text="" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <telerik:RadButton runat="server" ID="BtnSaveUsersInRoles" Text="تایید" RenderMode="Lightweight" OnClick="BtnSaveUsersInRoles_Click"></telerik:RadButton>
                                        </td>
                                    </tr>
                                </table>
                                <br />

                            </fieldset>
                        </div>
                    </Template>
                </ContentPane>
            </ig:WebDialogWindow>

            <%-- Dialog For PermissionsInRoles --%>
            <ig:WebDialogWindow runat="server" ID="WebDialogWindow_PermissionsInRoles" InitialLocation="Centered"
                Height="500px" Width="500px" Modal="true" WindowState="Hidden" Style="line-height: normal" ModalBackgroundCssClass="MyModal">
                <Header CaptionAlignment="Center">
                    <CloseBox Visible="true" />
                </Header>
                <ContentPane BackColor="White">
                    <Template>
                        <div style="position: relative; padding: 20px;">
                            <fieldset>
                                <legend>مدیریت مجوزها</legend>
                                <br />
                                <table border="0">
                                    <tr>
                                        <td>لیست مجوزها</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadListBox runat="server" ID="ListBox_Permissions" RenderMode="Lightweight" dir="rtl"
                                                CheckBoxes="true" ShowCheckAll="true" Localization-CheckAll="--انتخاب همه موارد--"
                                                Width="400px" Height="275px">
                                            </telerik:RadListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <asp:Label runat="server" ID="LB_PermissionsInRoles" Text="" ForeColor="Red"></asp:Label>
                                            <asp:Label runat="server" ID="LB_PermissionsInRoles_SelectedRoleId" Text="" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <telerik:RadButton runat="server" ID="BtnSavePermissionsInRoles" Text="تایید" RenderMode="Lightweight" OnClick="BtnSavePermissionsInRoles_Click"></telerik:RadButton>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                    </Template>
                </ContentPane>
            </ig:WebDialogWindow>

        </telerik:RadAjaxPanel>

    </div>
    <br />
    <br />
</asp:Content>
