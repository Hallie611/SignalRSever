<%@ Page Title="Question History" Language="C#" MasterPageFile="~/Web/MasterPage.Master" AutoEventWireup="true" CodeBehind="QuestionHistory.aspx.cs" Inherits="SignalRSever.Web.Question.QuestionManager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="art-layout-cell art-content"><article class="art-post art-article">
         <h2 class="art-postheader">Question History</h2>                      
         <div class="art-postcontent art-postcontent-0 clearfix"><p><br/></p></div> 
         <div>
             <asp:GridView ID="GV_historyQuestion" runat="server" CssClass="EU_DataTable" AllowPaging="true"
                PageSize="10" AutoGenerateSelectButton="True" OnRowDataBound="GV_historyQuestion_RowDataBound" OnSelectedIndexChanged="GV_historyQuestion_SelectedIndexChanged" OnPageIndexChanging="GridView1_PageIndexChanging">
             </asp:GridView>
        </div>           
    </article></div>
</asp:Content>