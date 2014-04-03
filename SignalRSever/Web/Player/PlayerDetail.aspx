<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlayerDetail.aspx.cs" Inherits="SignalRSever.Web.Player.PlayerDetail"   EnableEventValidation="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblname" runat="server" Text="Label"></asp:Label>
        <asp:GridView ID="GVPlayer_Detail" runat="server" OnRowDataBound="GVPlayer_Detail_RowDataBound" OnSelectedIndexChanged="GVPlayer_Detail_SelectedIndexChanged" AutoGenerateSelectButton="True"></asp:GridView>
        <a href='javascript:history.go(-1)'>Go Back to Previous Page</a>
    </div>
    </form>
</body>
</html>
