<%@ Page Title="Player Detail" Language="C#" MasterPageFile="~/Web/MasterPage.Master" AutoEventWireup="true" CodeBehind="PlayerDetail.aspx.cs" Inherits="SignalRSever.Web.Player.PlayerDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="art-layout-cell art-content"><article class="art-post art-article">
         <h2 class="art-postheader">Player Detail</h2>                      
         <div class="art-postcontent art-postcontent-0 clearfix"><p><br/></p></div> 
         <div>
            <asp:Label ID="lblname" runat="server" Text="Label"></asp:Label>
             <asp:GridView ID="GVPlayer_Detail" runat="server" CssClass="EU_DataTable" AllowPaging="true"
                PageSize="10" OnRowDataBound="GVPlayer_Detail_RowDataBound" OnSelectedIndexChanged="GVPlayer_Detail_SelectedIndexChanged" AutoGenerateSelectButton="True" OnPageIndexChanging="GridView1_PageIndexChanging">
             </asp:GridView>
             <a href='javascript:history.go(-1)'>Go Back to Previous Page</a>
         </div>           
    </article></div>
</asp:Content>
