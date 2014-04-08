<%@ Page Title="Player Management" Language="C#" MasterPageFile="~/Web/MasterPage.Master" AutoEventWireup="true" CodeBehind="PlayerManagerPage.aspx.cs" Inherits="SignalRSever.Web.UserManagerPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="art-layout-cell art-content"><article class="art-post art-article">
         <h2 class="art-postheader">Player Management</h2>                      
         <div class="art-postcontent art-postcontent-0 clearfix"><p><br/></p></div> 

         <div>
            <asp:GridView ID="GVPlayer" runat="server" CssClass="EU_DataTable" AllowPaging="true"
                PageSize="10" OnSelectedIndexChanged="GVPlayer_SelectedIndexChanged" AutoGenerateSelectButton="True" OnRowDataBound="GVPlayer_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging" AllowSorting="True" OnSorting="GridView1_Sorting">
            </asp:GridView>
        </div> 
                  
    </article></div>
</asp:Content>