<%@ Page Title="صفحه اصلی" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="_Default" CodeBehind="Default.aspx.cs" %>

<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.1028, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

      <telerik:RadGrid ID="RadGrid1" runat="server" PageSize="10" MasterTableView-EditMode="PopUp"
                                                OnNeedDataSource="RadGrid1_NeedDataSource" OnItemCreated="RadGrid1_ItemCreated" OnItemDataBound="RadGrid1_ItemDataBound"
                                                OnDeleteCommand="RadGrid1_DeleteCommand"
                                                AllowPaging="True" AllowSorting="true" RenderMode="Lightweight" Skin="Bootstrap" HeaderStyle-HorizontalAlign="Center">
                                                <ExportSettings ExportOnlyData="true" IgnorePaging="true"></ExportSettings>
                                                <MasterTableView AutoGenerateColumns="false" AllowMultiColumnSorting="true" AllowFilteringByColumn="true" TableLayout="Fixed"
                                                    DataKeyNames="WorkshopId" CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnFirstPage"
                                                    Font-Names="BYekan" Font-Size="13px">
                                                    <CommandItemSettings ShowExportToCsvButton="false" ShowExportToExcelButton="false" ShowExportToPdfButton="false" ShowExportToWordButton="false" />
                                                    <NoRecordsTemplate>
                                                        <div style="text-align: right; padding: 5px; color: #383779;">
                                                            هیچ رکوردی برای نمایش وجود ندارد!
                                                        </div>
                                                    </NoRecordsTemplate>
                                                    <CommandItemTemplate>
                                                    </CommandItemTemplate>
                                                    <Columns>
                                                        <telerik:GridTemplateColumn UniqueName="RowNumber" HeaderText="#" AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LB_RowNumber" runat="server" Width="15px" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="15px" />
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridNumericColumn DataField="WorkshopId" HeaderText="کد کارگاه" SortExpression="WorkshopId"
                                                            UniqueName="WorkshopId" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                            <HeaderStyle Width="30px" />
                                                        </telerik:GridNumericColumn>
                                                        <telerik:GridBoundColumn DataField="Name" HeaderText="نام کارگاه" SortExpression="Title" UniqueName="Name"
                                                            ItemStyle-HorizontalAlign="Center" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                                            <HeaderStyle Width="75px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridBoundColumn DataField="StartTimeShamsi" HeaderText="تاریخ شروع" SortExpression="StartTimeShamsi" ReadOnly="true"
                                                            UniqueName="StartTimeShamsi" DataFormatString="{00:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center"
                                                            CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                                            <HeaderStyle Width="45px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="EndTimeShamsi" HeaderText="تاریخ پایان" SortExpression="EndTimeShamsi" ReadOnly="true"
                                                            UniqueName="EndTimeShamsi" DataFormatString="{00:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center"
                                                            CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                                            <HeaderStyle Width="45px" />
                                                        </telerik:GridBoundColumn>

                                                        <telerik:GridTemplateColumn UniqueName="EditArticle" HeaderText="ویرایش" AllowFiltering="false" Visible="true" HeaderStyle-Font-Size="10px">
                                                            <ItemTemplate>
                                                                <telerik:RadButton RenderMode="Lightweight" ID="BtnEditWorkshop" runat="server" Width="20px" Height="20px"
                                                                    CommandName="EditWorkshop" CommandArgument='<%# Eval("WorkshopId") %>' OnCommand="BtnEditWorkshop_Command"
                                                                    ToolTip="ویرایش کارگاه">
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
                                                <PagerStyle PageButtonCount="7" Mode="NextPrevNumericAndAdvanced" AlwaysVisible="true" Font-Size="12px" CssClass=""
                                                    FirstPageImageUrl="/images/PagingFirst.png" LastPageImageUrl="/images/PagingLast.png"
                                                    NextPageImageUrl="/images/PagingNext.png" PrevPageImageUrl="/images/PagingPrev.png"
                                                    PageSizeLabelText="تعداد رکورد در هر صفحه" PagerTextFormat="{4} صفحه {0} از {1} - نمایش رکوردهای {2} تا {3} از تعداد کل {5}" />
                                                <FilterMenu RenderMode="Lightweight">
                                                </FilterMenu>
                                                <HeaderContextMenu RenderMode="Lightweight">
                                                </HeaderContextMenu>
                                            </telerik:RadGrid>

                                       
                                    </div>
                                </div>
                            </section>
                        </div>

</asp:Content>
