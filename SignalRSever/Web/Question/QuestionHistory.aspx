<%@ Page Title="Question History" Language="C#" MasterPageFile="~/Web/MasterPage.Master" AutoEventWireup="true" CodeBehind="QuestionHistory.aspx.cs" Inherits="SignalRSever.Web.Question.QuestionManager" EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/Web/jquery-1.7.1.min.js"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#<%=lblNoRecords.ClientID%>').css('display', 'none');

            $('#<%=btnSubmit.ClientID%>').click(function (e) {
                $('#<%=lblNoRecords.ClientID%>').css('display', 'none'); // Hide No records to display label.
                    $("#<%=GV_historyQuestion.ClientID%> tr:has(td)").hide(); // Hide all the rows.

                    var iCounter = 0;
                    var sSearchTerm = $('#<%=txtSearch.ClientID%>').val(); //Get the search box value

                if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                {
                    $("#<%=GV_historyQuestion.ClientID%> tr:has(td)").show();
                    return false;
                }
                    //Iterate through all the td.
                    $("#<%=GV_historyQuestion.ClientID%> tr:has(td)").children().each(function () {
                        var cellText = $(this).text().toLowerCase();
                        if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                        {
                            $(this).parent().show();
                            iCounter++;
                            return true;
                        }
                    });
                    if (iCounter == 0) {
                        $('#<%=lblNoRecords.ClientID%>').css('display', '');
                }
                    e.preventDefault();
                })
        })
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="art-layout-cell art-content">
        <article class="art-post art-article">
            <h2 class="art-postheader">Question History</h2>
            <div class="art-postcontent art-postcontent-0 clearfix">
                <div style="float: right">
                    Search Text :
                <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                    &nbsp;
                <asp:Button ID="btnSubmit" runat="server" Text="Search" />
                </div>
            </div>
            <div>
                <br />
                <asp:GridView ID="GV_historyQuestion" runat="server" CssClass="EU_DataTable" AllowPaging="true"
                    PageSize="10" AutoGenerateSelectButton="True" OnRowDataBound="GV_historyQuestion_RowDataBound"
                    OnSelectedIndexChanged="GV_historyQuestion_SelectedIndexChanged"
                    OnPageIndexChanging="GridView1_PageIndexChanging" AllowSorting="True" OnSorting="GridView1_Sorting">
                </asp:GridView>
                <asp:Label ID="lblNoRecords" Text="No records to display" runat="server" ForeColor="red"></asp:Label>
            </div>
            <div style="float: right">
                <asp:Button ID="btExport" runat="server" Text="Export" OnClick="btExport_Click" />
            </div>
            <br />
            <br />
        </article>
    </div>
</asp:Content>
