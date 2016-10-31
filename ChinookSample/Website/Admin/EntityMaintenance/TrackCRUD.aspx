<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="TrackCRUD.aspx.cs" Inherits="Admin_EntityMaintenance_TrackCRUD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="jumbotron">
            <h3>Wired ListView CRUD</h3>
        </div> 
    <asp:ListView ID="TrackList" runat="server" DataSourceID="TrackListODS" InsertItemPosition="LastItem" DataKeyNames="TrackId">
        <AlternatingItemTemplate>
            <tr style="background-color: #FFFFFF; color: #284775;">
                <td>
                    <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                    <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                </td>
                <td>
                    <asp:Label Text='<%# Eval("TrackId") %>' runat="server" ID="TrackIdLabel"/></td>
                <td>
                    <asp:Label Text='<%# Eval("Name") %>' runat="server" ID="NameLabel" /></td>
                <td>
                    <asp:DropDownList ID="AlbumList" runat="server" DataSourceID="AlbumListODS" DataTextField="DisplayText" 
                        DataValueField="PFKeyIdentifier" selectedvalue='<%# Bind("AlbumId") %>'></asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="MediaTypeList" runat="server" DataSourceID="MediaTypeListODS" DataTextField="DisplayText" DataValueField="PFKeyIdentifier"
                        selectedvalue='<%# Bind("MediaTypeId") %>'></asp:DropDownList></td>
                <td>
                    <asp:DropDownList ID="GenreList" runat="server" DataSourceID="GenreListODS" DataTextField="DisplayText" DataValueField="PFKeyIdentifier"
                        selectedvalue='<%# Bind("GenreId") %>'></asp:DropDownList></td>
                <td>
                    <asp:Label Text='<%# Eval("Composer") %>' runat="server" ID="ComposerLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Milliseconds") %>' runat="server" ID="MillisecondsLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Bytes") %>' runat="server" ID="BytesLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("UnitPrice") %>' runat="server" ID="UnitPriceLabel" /></td>
                
            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <tr style="background-color: #999999;">
                <td>
                    <asp:Button runat="server" CommandName="Update" Text="Update" ID="UpdateButton" />
                    <asp:Button runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" />
                </td>
                <td>
                    <asp:TextBox Text='<%# Bind("TrackId") %>' runat="server" ID="TrackIdTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("Name") %>' runat="server" ID="NameTextBox" /></td>
                <td>
                    <asp:DropDownList ID="AlbumList" runat="server" DataSourceID="AlbumListODS" DataTextField="DisplayText" 
                        DataValueField="PFKeyIdentifier" selectedvalue='<%# Bind("AlbumId") %>'></asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="MediaTypeList" runat="server" DataSourceID="MediaTypeListODS" DataTextField="DisplayText" DataValueField="PFKeyIdentifier"
                        selectedvalue='<%# Bind("MediaTypeId") %>'></asp:DropDownList></td>
                <td>
                    <asp:DropDownList ID="GenreList" runat="server" DataSourceID="GenreListODS" DataTextField="DisplayText" DataValueField="PFKeyIdentifier"
                        selectedvalue='<%# Bind("GenreId") %>'></asp:DropDownList></td>
                <td>
                    <asp:TextBox Text='<%# Bind("Composer") %>' runat="server" ID="ComposerTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("Milliseconds") %>' runat="server" ID="MillisecondsTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("Bytes") %>' runat="server" ID="BytesTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("UnitPrice") %>' runat="server" ID="UnitPriceTextBox" /></td>
                
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <tr style="">
                <td>
                    <asp:Button runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" />
                    <asp:Button runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                </td>
                <td>
                    <asp:TextBox Text='<%# Bind("TrackId") %>' runat="server" ID="TrackIdTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("Name") %>' runat="server" ID="NameTextBox" /></td>
                <td>
                    <asp:DropDownList ID="AlbumList" runat="server" DataSourceID="AlbumListODS" DataTextField="DisplayText" 
                        DataValueField="PFKeyIdentifier" selectedvalue='<%# Bind("AlbumId") %>'></asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="MediaTypeList" runat="server" DataSourceID="MediaTypeListODS" DataTextField="DisplayText" DataValueField="PFKeyIdentifier"
                        selectedvalue='<%# Bind("MediaTypeId") %>'></asp:DropDownList></td>
                <td>
                    <asp:DropDownList ID="GenreList" runat="server" DataSourceID="GenreListODS" DataTextField="DisplayText" DataValueField="PFKeyIdentifier"
                        selectedvalue='<%# Bind("GenreId") %>'></asp:DropDownList></td>
                <td>
                    <asp:TextBox Text='<%# Bind("Composer") %>' runat="server" ID="ComposerTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("Milliseconds") %>' runat="server" ID="MillisecondsTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("Bytes") %>' runat="server" ID="BytesTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("UnitPrice") %>' runat="server" ID="UnitPriceTextBox" /></td>
                
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="background-color: #E0FFFF; color: #333333;">
                <td>
                    <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                    <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                </td>
                <td>
                    <asp:Label Text='<%# Eval("TrackId") %>' runat="server" ID="TrackIdLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Name") %>' runat="server" ID="NameLabel" /></td>
                <td>
                    <asp:DropDownList ID="AlbumList" runat="server" DataSourceID="AlbumListODS" DataTextField="DisplayText" 
                        DataValueField="PFKeyIdentifier" selectedvalue='<%# Bind("AlbumId") %>'></asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="MediaTypeList" runat="server" DataSourceID="MediaTypeListODS" DataTextField="DisplayText" DataValueField="PFKeyIdentifier"
                        selectedvalue='<%# Bind("MediaTypeId") %>'></asp:DropDownList></td>
                <td>
                    <asp:DropDownList ID="GenreList" runat="server" DataSourceID="GenreListODS" DataTextField="DisplayText" DataValueField="PFKeyIdentifier"
                        selectedvalue='<%# Bind("GenreId") %>'></asp:DropDownList></td>
                <td>
                    <asp:Label Text='<%# Eval("Composer") %>' runat="server" ID="ComposerLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Milliseconds") %>' runat="server" ID="MillisecondsLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Bytes") %>' runat="server" ID="BytesLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("UnitPrice") %>' runat="server" ID="UnitPriceLabel" /></td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                            <tr runat="server" style="background-color: #E0FFFF; color: #333333;">
                                <th runat="server"></th>
                                <th runat="server">Id</th>
                                <th runat="server">Name</th>
                                <th runat="server">Album</th>
                                <th runat="server">MediaType</th>
                                <th runat="server">Genre</th>
                                <th runat="server">Composer</th>
                                <th runat="server">Milliseconds</th>
                                <th runat="server">Bytes</th>
                                <th runat="server">$</th>
                   
                            </tr>
                            <tr runat="server" id="itemPlaceholder"></tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="text-align: center; background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif; color: #FFFFFF">
                        <asp:DataPager runat="server" ID="DataPager1">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                <asp:NumericPagerField></asp:NumericPagerField>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr style="background-color: #E2DED6; font-weight: bold; color: #333333;">
                <td>
                    <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                    <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                </td>
                <td>
                    <asp:Label Text='<%# Eval("TrackId") %>' runat="server" ID="TrackIdLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Name") %>' runat="server" ID="NameLabel" /></td>
                <td>
                    <asp:DropDownList ID="AlbumList" runat="server" DataSourceID="AlbumListODS" DataTextField="DisplayText" 
                        DataValueField="PFKeyIdentifier" selectedvalue='<%# Bind("AlbumId") %>'></asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="MediaTypeList" runat="server" DataSourceID="MediaTypeListODS" DataTextField="DisplayText" DataValueField="PFKeyIdentifier"
                        selectedvalue='<%# Bind("MediaTypeId") %>'></asp:DropDownList></td>
                <td>
                    <asp:DropDownList ID="GenreList" runat="server" DataSourceID="GenreListODS" DataTextField="DisplayText" DataValueField="PFKeyIdentifier"
                        selectedvalue='<%# Bind("GenreId") %>'></asp:DropDownList></td>
                <td>
                    <asp:Label Text='<%# Eval("Composer") %>' runat="server" ID="ComposerLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Milliseconds") %>' runat="server" ID="MillisecondsLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Bytes") %>' runat="server" ID="BytesLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("UnitPrice") %>' runat="server" ID="UnitPriceLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("MediaType") %>' runat="server" ID="MediaTypeLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Album") %>' runat="server" ID="AlbumLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Genre") %>' runat="server" ID="GenreLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("InvoiceLines") %>' runat="server" ID="InvoiceLinesLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("PlayLists") %>' runat="server" ID="PlayListsLabel" /></td>
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>




    <asp:ObjectDataSource ID="TrackListODS" runat="server" DataObjectTypeName="ChinookSystem.Data.Entities.Track" DeleteMethod="DeleteTrack" InsertMethod="AddTrack" OldValuesParameterFormatString="original_{0}" SelectMethod="ListTracks" TypeName="ChinookSystem.BLL.TrackController" UpdateMethod="UpdateTrack"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="AlbumListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="AlbumList" TypeName="ChinookSystem.BLL.AlbumController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="MediaTypeListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="MediaTypeList" TypeName="ChinookSystem.BLL.MediaTypeController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="GenreListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GenreList" TypeName="ChinookSystem.BLL.GenreController"></asp:ObjectDataSource>
</asp:Content>

