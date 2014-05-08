<%@ Page Title="Player Management" Language="C#" MasterPageFile="~/Web/MasterPage.Master" AutoEventWireup="true" CodeBehind="PlayerManagerPage.aspx.cs" Inherits="SignalRSever.Web.UserManagerPage" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/Web/jquery-1.7.1.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="art-layout-cell art-content">
        <article class="art-post art-article">
            <h2 class="art-postheader">Player Management</h2>
            <div class="art-postcontent art-postcontent-0 clearfix">
                <br />
                <div>
                    Search Text :
                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                    &nbsp;
                <asp:Button ID="btnSubmit" runat="server" Text="Search" OnClick="btnSubmit_Click" />
                <asp:Button ID="btExport" runat="server" Text="Export" style="float: right" OnClick="btExport_Click" />
                </div>
            </div>
            <div>
                <br />
                <asp:GridView ID="GVPlayer" runat="server" CssClass="EU_DataTable" AllowPaging="true" PageSize="10"
                     OnSelectedIndexChanged="GVPlayer_SelectedIndexChanged" AutoGenerateSelectButton="True"
                    OnRowDataBound="GVPlayer_RowDataBound"  OnPageIndexChanging="GridView1_PageIndexChanging"
                    AllowSorting="True" OnSorting="GridView1_Sorting">
                </asp:GridView>
                <asp:Label ID="lblNoRecords" Text="No records to display" runat="server" ForeColor="Red" Visible="False"></asp:Label>
            </div>
        </article>
    </div>
</asp:Content>
