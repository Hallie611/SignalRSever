<%@ Page Title="Question Detail" Language="C#" MasterPageFile="~/Web/MasterPage.Master" AutoEventWireup="true" CodeBehind="QuestionDetail.aspx.cs" Inherits="SignalRSever.Web.Question.QuestionDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="art-layout-cell art-content"><article class="art-post art-article">
         <h2 class="art-postheader">Question Detail</h2>                      
         <div class="art-postcontent art-postcontent-0 clearfix"><p><br/></p></div> 
         <div>
        <asp:GridView ID="GV_QuestionDetail" runat="server" CssClass="EU_DataTable">
             </asp:GridView>
        <a href='javascript:history.go(-1)'>Go Back to Previous Page</a>
        </div>         
    </article></div>
</asp:Content>