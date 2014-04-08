<%@ Page Title="Player Detail" Language="C#" MasterPageFile="~/Web/MasterPage.Master" AutoEventWireup="true" CodeBehind="PlayerDetail.aspx.cs" Inherits="SignalRSever.Web.Player.PlayerDetail" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            width: 144px;
            height: 23px;
        }
        .auto-style4 {
            width: 366px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="art-layout-cell art-content"><article class="art-post art-article">
         <h2 class="art-postheader">Player Detail</h2>                      
         <div class="art-postcontent art-postcontent-0 clearfix"><p><br/></p></div> 
         <div>
             <table style="width: 100%;">
                 <tr>
                     <td class="auto-style2"></td>
                     <td class="auto-style4"></td>
                     <td rowspan="10">
                         <asp:Chart ID="Chart1" runat="server">
                             <Series>
                                 <asp:Series Name="Series1" ChartType="Pie"></asp:Series>
                             </Series>
                             <legends>
                                <asp:Legend Alignment="Center" Docking="Bottom"
                                IsTextAutoFit="False" Name="Default"
                                LegendStyle="Row" />
                            </legends>
                             <ChartAreas>
                                 <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                             </ChartAreas>
                         </asp:Chart>
                     </td>
                 </tr>
                 <tr>
                     <td class="auto-style2"></td>
                     <td class="auto-style4"></td>
                 </tr>
                 <tr>
                     <td class="auto-style2">Player Name: </td>
                     <td class="auto-style4"><asp:Label ID="lblname" runat="server"></asp:Label></td>
                 </tr>
                 <tr>
                     <td class="auto-style2">Level: </td>
                     <td class="auto-style4"><asp:Label ID="lbllevel" runat="server"></asp:Label></td>
                 </tr>
                 <tr>
                     <td class="auto-style2">Point: </td>
                     <td class="auto-style4"><asp:Label ID="lblpoint" runat="server"></asp:Label></td>
                 </tr>
                 <tr>
                     <td class="auto-style2">Win: </td>
                     <td class="auto-style4"><asp:Label ID="lblwin" runat="server"></asp:Label></td>
                 </tr>
                 <tr>
                     <td class="auto-style2">Lose: </td>
                     <td class="auto-style4"><asp:Label ID="lbllose" runat="server"></asp:Label></td>
                 </tr>
                 <tr>
                     <td></td>
                     <td class="auto-style4"></td>
                 </tr>
                 <tr>
                     <td></td>
                     <td class="auto-style4"></td>
                 </tr>
                 <tr>
                     <td></td>
                     <td class="auto-style4"></td>
                 </tr>
             </table>
             <div></div>
             <asp:GridView ID="GVPlayer_Detail" runat="server" CssClass="EU_DataTable" AllowPaging="true"
                PageSize="10" OnRowDataBound="GVPlayer_Detail_RowDataBound" OnSelectedIndexChanged="GVPlayer_Detail_SelectedIndexChanged" AutoGenerateSelectButton="True" OnPageIndexChanging="GridView1_PageIndexChanging">
             </asp:GridView>
             <div></div>
             <div></div>
             <div>Top 5</div>
             <asp:GridView ID="GV_games" runat="server" CssClass="EU_DataTable"></asp:GridView>
             <a href='javascript:history.go(-1)'>Go Back to Previous Page</a>
         </div>           
    </article></div>
</asp:Content>
