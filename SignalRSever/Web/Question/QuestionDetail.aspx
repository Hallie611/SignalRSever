<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionDetail.aspx.cs" Inherits="SignalRSever.Web.Question.QuestionDetail"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GV_QuestionDetail" runat="server"></asp:GridView>
        <a href='javascript:history.go(-1)'>Go Back to Previous Page</a>
    </div>
    </form>
</body>
</html>
