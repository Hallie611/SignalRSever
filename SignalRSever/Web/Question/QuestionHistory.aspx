<%@ Page Title="Question History" Language="C#" MasterPageFile="~/Web/MasterPage.Master" AutoEventWireup="true" CodeBehind="QuestionHistory.aspx.cs" Inherits="SignalRSever.Web.Question.QuestionManager" EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/Web/jquery-1.7.1.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="art-layout-cell art-content">
        <article class="art-post art-article">
            <h2 class="art-postheader">Question History</h2>
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
                <asp:GridView ID="GV_historyQuestion" runat="server" CssClass="EU_DataTable"  AutoGenerateSelectButton="True" OnRowDataBound="GV_historyQuestion_RowDataBound"
                    OnSelectedIndexChanged="GV_historyQuestion_SelectedIndexChanged" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging"
                     AllowSorting="True" OnSorting="GridView1_Sorting">
                </asp:GridView>
                <asp:Label ID="lblNoRecords" Text="No records to display" runat="server" ForeColor="red"></asp:Label>
            </div>
        </article>
    </div>
</asp:Content>
