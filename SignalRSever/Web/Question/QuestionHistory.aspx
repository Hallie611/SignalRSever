<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionHistory.aspx.cs" Inherits="SignalRSever.Web.Question.QuestionManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GV_historyQuestion" runat="server" AutoGenerateSelectButton="True" OnRowDataBound="GV_historyQuestion_RowDataBound" OnSelectedIndexChanged="GV_historyQuestion_SelectedIndexChanged"></asp:GridView>
    </div>
    </form>
</body>
</html>
