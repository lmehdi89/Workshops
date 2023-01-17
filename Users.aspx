<%@ Page Title="کاربران" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="_Users" CodeBehind="Users.aspx.cs" %>

<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.1028, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="active-section" id="blog">
        <section class="section-block">
            <h4 class="title">مدیریت کاربران</h4>
            <div class="row">
                <div class="col-md-12">

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

                        <telerik:RadGrid ID="RadGrid1" runat="server" PageSize="20" MasterTableView-EditMode="PopUp"
                            OnNeedDataSource="RadGrid1_NeedDataSource" OnItemCreated="RadGrid1_ItemCreated" OnItemDataBound="RadGrid1_ItemDataBound"
                            OnDeleteCommand="RadGrid1_DeleteCommand"
                            AllowPaging="True" AllowSorting="true" RenderMode="Lightweight" Skin="Bootstrap" HeaderStyle-HorizontalAlign="Center">
                            <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
                            <MasterTableView AutoGenerateColumns="false" AllowMultiColumnSorting="true" AllowFilteringByColumn="true" TableLayout="Fixed"
                                DataKeyNames="UserId" CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnFirstPage"
                                Font-Names="Tahoma" Font-Size="12px">
                                <CommandItemSettings ShowExportToCsvButton="false" ShowExportToExcelButton="false" ShowExportToPdfButton="false" ShowExportToWordButton="false" />
                                <NoRecordsTemplate>
                                    <div style="text-align: right; padding: 5px; color: #383779;">
                                        هیچ رکوردی برای نمایش وجود ندارد!
                                    </div>
                                </NoRecordsTemplate>
                                <CommandItemTemplate>
                                    <br />
                                    <telerik:RadButton RenderMode="Lightweight" ID="BtnAddNewUser" runat="server" OnClick="BtnAddNewUser_Click"
                                        Text="افزودن کاربر" Font-Names="Tahoma">
                                        <Icon PrimaryIconCssClass="rbAdd" />
                                    </telerik:RadButton>
                                    <telerik:RadButton RenderMode="Lightweight" ID="BtnRefresh" runat="server" OnClick="BtnRefresh_Click"
                                        Text="بارگذاری مجدد" Font-Names="Tahoma">
                                        <Icon PrimaryIconCssClass="rbRefresh" />
                                    </telerik:RadButton>

                                    <p style="float: left; font-size: 20px;">مدیریت کاربران</p>
                                    <hr />

                                </CommandItemTemplate>
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="RowNumber" HeaderText="#" AllowFiltering="false">
                                        <ItemTemplate>
                                            <asp:Label ID="LB_RowNumber" runat="server" Width="15px" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="15px" />
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridNumericColumn DataField="UserId" HeaderText="کد کاربری" SortExpression="UserId"
                                        UniqueName="UserId" ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <HeaderStyle Width="40px" />
                                    </telerik:GridNumericColumn>
                                    <telerik:GridBoundColumn DataField="UserName" HeaderText="نام کاربری" SortExpression="UserName" UniqueName="UserName"
                                        ItemStyle-HorizontalAlign="Center" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                        <HeaderStyle Width="50px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FirstName" HeaderText="نام" SortExpression="FirstName" UniqueName="FirstName"
                                        ItemStyle-HorizontalAlign="Center" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                        <HeaderStyle Width="60px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="LastName" HeaderText="نام خانوادگی" SortExpression="LastName" UniqueName="LastName"
                                        ItemStyle-HorizontalAlign="Center" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                        <HeaderStyle Width="60px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridCheckBoxColumn DataField="IsActive" HeaderText="فعال" SortExpression="IsActive"
                                        UniqueName="IsActive" ItemStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true">
                                        <HeaderStyle Width="30px" />
                                    </telerik:GridCheckBoxColumn>
                                    <telerik:GridBoundColumn DataField="CreatedShamsi" HeaderText="تاریخ ایجاد" SortExpression="CreatedShamsi" ReadOnly="true"
                                        UniqueName="CreatedShamsi" DataFormatString="{00:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center"
                                        CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                        <HeaderStyle Width="45px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="LastLoginDateShamsi" HeaderText="تاریخ آخرین ورود" SortExpression="LastLoginDateShamsi" ReadOnly="true"
                                        UniqueName="LastLoginDateShamsi" DataFormatString="{00:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center"
                                        CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                        <HeaderStyle Width="45px" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridTemplateColumn UniqueName="EditUser" HeaderText="" AllowFiltering="false" Visible="true">
                                        <ItemTemplate>
                                            <telerik:RadButton RenderMode="Lightweight" ID="BtnEditUser" runat="server" Width="20px" Height="20px"
                                                CommandName="EditUser" CommandArgument='<%# Eval("UserId") %>' OnCommand="BtnEditUser_Command"
                                                ToolTip="ویرایش پروفایل">
                                                <Image ImageUrl="none" IsBackgroundImage="true"></Image>
                                                <Icon PrimaryIconUrl="/images/EditUser.png" PrimaryIconWidth="20px" PrimaryIconHeight="20px"
                                                    PrimaryIconTop="-1px" PrimaryIconLeft="-5px"></Icon>
                                            </telerik:RadButton>
                                        </ItemTemplate>
                                        <HeaderStyle Width="15px" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridButtonColumn CommandName="Delete" Text="حذف" UniqueName="DeleteColumn" HeaderText="" Visible="true"
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

                        <%-- Dialog For Add New User --%>
                        <ig:WebDialogWindow runat="server" ID="WebDialogWindow_AddNewUser" InitialLocation="Centered"
                            Height="610px" Width="500px" Modal="true" WindowState="Hidden" Style="line-height: normal" ModalBackgroundCssClass="MyModal">
                            <Header CaptionAlignment="Center" CaptionText="افزودن کاربر">
                                <CloseBox Visible="true" />
                            </Header>
                            <ContentPane BackColor="White">
                                <Template>
                                    <div style="position: relative; padding: 20px;">

                                     

                                         <fieldset>
                                                <legend>افزودن کاربر</legend>
                                                <br />
                                                <table border="0" class="MyTable">
                                                    <colgroup>
                                                        <col style="width: 100px;" />
                                                        <col style="width: 300px;" />
                                                    </colgroup>
                                                   <tr>
                                                       <td style="width: 50px;">فعال :<span style="width: 50px;"><asp:CheckBox runat="server" ID="CHK_NewIsActive" Width="200" Checked="true"></asp:CheckBox></span></td>
                                                       <td></td>
                                                   </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <telerik:RadTextBox runat="server" RenderMode="Lightweight" CssClass="form-control" placeholder="نام کاربری" ID="Tb_NewUserName" Width="200"></telerik:RadTextBox>
                                                            *
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <telerik:RadTextBox runat="server" RenderMode="Lightweight" CssClass="form-control" placeholder="رمز عبور" ID="Tb_NewPassword" Width="200"></telerik:RadTextBox>
                                                            *
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <telerik:RadTextBox runat="server" RenderMode="Lightweight" CssClass="form-control" placeholder="تکرار رمز عبور" ID="Tb_NewPassword2" Width="200"></telerik:RadTextBox>
                                                            *
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <telerik:RadTextBox runat="server" RenderMode="Lightweight" CssClass="form-control" placeholder="نام" ID="Tb_NewFirstName" Width="200"></telerik:RadTextBox>
                                                            *
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <telerik:RadTextBox runat="server" RenderMode="Lightweight" CssClass="form-control" placeholder="نام خانوادگی" ID="Tb_NewLastName" Width="200"></telerik:RadTextBox>
                                                            *
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <telerik:RadTextBox runat="server" RenderMode="Lightweight" CssClass="form-control" placeholder="ایمیل" ID="Tb_NewEmail" Width="200"></telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <telerik:RadTextBox runat="server" RenderMode="Lightweight" CssClass="form-control" placeholder="تلفن" ID="Tb_NewTel" Width="200"></telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <telerik:RadTextBox runat="server" RenderMode="Lightweight" CssClass="form-control" placeholder="تلفن همراه" ID="Tb_NewMobile" Width="200"></telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <telerik:RadTextBox runat="server" RenderMode="Lightweight" CssClass="form-control" placeholder="آدرس" ID="Tb_NewAddress" Width="200"></telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <telerik:RadTextBox runat="server" RenderMode="Lightweight" CssClass="form-control" placeholder="کد پستی" ID="Tb_NewPostalCode" Width="200"></telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td colspan="2" style="text-align: center;">
                                                            <asp:Label runat="server" ID="LB_AddNewUser" Text="" ForeColor="Red"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="text-align: center;">
                                                            <telerik:RadButton runat="server" ID="BtnSaveNewUser" Text="تایید" RenderMode="Lightweight" OnClick="BtnSaveNewUser_Click"></telerik:RadButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                        </div>
                                        </fieldset>
                                    </div>
                                </Template>
                            </ContentPane>
                        </ig:WebDialogWindow>

                        <%-- Dialog For Edit User --%>
                        <ig:WebDialogWindow runat="server" ID="WebDialogWindow_EditUser" InitialLocation="Centered"
                            Height="610px" Width="450px" Modal="true" WindowState="Hidden" Style="line-height: normal" ModalBackgroundCssClass="MyModal">
                            <Header CaptionAlignment="Center" CaptionText="ویرایش پروفایل">
                                <CloseBox Visible="true" />
                            </Header>
                            <ContentPane BackColor="White">
                                <Template>
                                    <div style="position: relative; padding: 20px;">
                                        <fieldset>
                                            <legend>ویرایش پروفایل</legend>
                                            <br />
                                            <table border="0" class="MyTable">
                                                <colgroup>
                                                    <col style="width: 150px;" />
                                                    <col style="width: 300px;" />
                                                </colgroup>
                                                <tr>
                                                    <td>نام کاربری : </td>
                                                    <td>
                                                        <telerik:RadTextBox runat="server" RenderMode="Lightweight" ID="Tb_EditUserName" Width="200"></telerik:RadTextBox>
                                                        *
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>رمز عبور : </td>
                                                    <td>
                                                        <telerik:RadTextBox runat="server" RenderMode="Lightweight" ID="Tb_EditPassword" Width="200"></telerik:RadTextBox>
                                                        *
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تکرار رمز عبور : </td>
                                                    <td>
                                                        <telerik:RadTextBox runat="server" RenderMode="Lightweight" ID="Tb_EditPassword2" Width="200"></telerik:RadTextBox>
                                                        *
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نام : </td>
                                                    <td>
                                                        <telerik:RadTextBox runat="server" RenderMode="Lightweight" ID="Tb_EditFirstName" Width="200"></telerik:RadTextBox>
                                                        *
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>نام خانوادگی : </td>
                                                    <td>
                                                        <telerik:RadTextBox runat="server" RenderMode="Lightweight" ID="Tb_EditLastName" Width="200"></telerik:RadTextBox>
                                                        *
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>ایمیل : </td>
                                                    <td>
                                                        <telerik:RadTextBox runat="server" RenderMode="Lightweight" ID="Tb_EditEmail" Width="200"></telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تلفن : </td>
                                                    <td>
                                                        <telerik:RadTextBox runat="server" RenderMode="Lightweight" ID="Tb_EditTel" Width="200"></telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>تلفن همراه : </td>
                                                    <td>
                                                        <telerik:RadTextBox runat="server" RenderMode="Lightweight" ID="Tb_EditMobile" Width="200"></telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>آدرس : </td>
                                                    <td>
                                                        <telerik:RadTextBox runat="server" RenderMode="Lightweight" ID="Tb_EditAddress" Width="200"></telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>کد پستی : </td>
                                                    <td>
                                                        <telerik:RadTextBox runat="server" RenderMode="Lightweight" ID="Tb_EditPostalCode" Width="200"></telerik:RadTextBox>
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
                                                        <asp:Label runat="server" ID="LB_EditUser" Text="" ForeColor="Red"></asp:Label>
                                                        <asp:Label runat="server" ID="LB_SelectedUserId" Text="" Visible="false"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="text-align: center;">
                                                        <telerik:RadButton runat="server" ID="BtnSaveEditUser" Text="تایید" RenderMode="Lightweight" OnClick="BtnSaveEditUser_Click"></telerik:RadButton>
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
            </div>
        </section>
    </div>
    <br />
</asp:Content>
