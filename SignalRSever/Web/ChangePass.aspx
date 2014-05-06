<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MasterPage.Master" AutoEventWireup="true" CodeBehind="ChangePass.aspx.cs" Inherits="SignalRSever.Web.ChangePass" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="art-layout-cell art-content"><article class="art-post art-article">
         <h2 class="art-postheader">Login</h2>                      
         <div class="art-postcontent art-postcontent-0 clearfix"><p>
             <asp:Label ID="lbMess" runat="server" ForeColor="Red"></asp:Label>
             <br/></p></div> 
         <table style="width: 100%;">
            <tr>
                <td>Current Password:</td>
                <td><asp:TextBox ID="txtPassCurrent" runat="server" TextMode="Password" Width="400px"></asp:TextBox></td>
            </tr>
             <tr>
                <td>New Password:</td>
                <td><asp:TextBox ID="txtPassNew" runat="server" TextMode="Password" Width="400px"></asp:TextBox></td>
            </tr>
             <tr>
                <td>Confirm New Password:</td>
                <td><asp:TextBox ID="txtPassConfirm" runat="server" TextMode="Password" Width="400px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td><asp:Button ID="btConfirm" runat="server" Text="Confirm" OnClick="btConfirm_Click" /> </td>
            </tr>
        </table>             
    </article></div>
</asp:Content>
