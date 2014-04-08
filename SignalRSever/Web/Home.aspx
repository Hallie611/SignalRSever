<%@ Page Title="Home" Language="C#" MasterPageFile="~/Web/MasterPage.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SignalRSever.Web.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="art-layout-cell art-content"><article class="art-post art-article">
         <h2 class="art-postheader">Login</h2>                      
         <div class="art-postcontent art-postcontent-0 clearfix"><p><br/></p></div> 
         <table style="width: 100%;">
            <tr>
                <td>Username:</td>
                <td><asp:TextBox ID="txtUser" runat="server" Width="400px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Password:</td>
                <td><asp:TextBox ID="txtPass" runat="server" TextMode="Password" Width="400px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td><asp:Button ID="btLogin" runat="server" Text="Login" /> </td>
            </tr>
        </table>             
    </article></div>
</asp:Content>

