<%@ Page Title="Player Management" Language="C#" MasterPageFile="~/Web/MasterPage.Master" AutoEventWireup="true" CodeBehind="PlayerManagerPage.aspx.cs" Inherits="SignalRSever.Web.UserManagerPage" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/Web/jquery-1.7.1.min.js"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#<%=lblNoRecords.ClientID%>').css('display', 'none');

            $('#<%=btnSubmit.ClientID%>').click(function (e) {
                $('#<%=lblNoRecords.ClientID%>').css('display', 'none'); // Hide No records to display label.
                $("#<%=GVPlayer.ClientID%> tr:has(td)").hide(); // Hide all the rows.

                var iCounter = 0;
                var sSearchTerm = $('#<%=txtSearch.ClientID%>').val(); //Get the search box value

                if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                {
                    $("#<%=GVPlayer.ClientID%> tr:has(td)").show();
                return false;
            }
                //Iterate through all the td.
                $("#<%=GVPlayer.ClientID%> tr:has(td)").children().each(function () {
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
            <h2 class="art-postheader">Player Management</h2>
            <div class="art-postcontent art-postcontent-0 clearfix">
                <br />
                <div>
                    Search Text :
                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                    &nbsp;
                <asp:Button ID="btnSubmit" runat="server" Text="Search" />
                <asp:Button ID="btExport" runat="server" Text="Export" style="float: right" OnClick="btExport_Click" />
                </div>
            </div>
            <div>
                <br />
                <asp:GridView ID="GVPlayer" runat="server" CssClass="EU_DataTable" AllowPaging="true"
                    PageSize="10" OnSelectedIndexChanged="GVPlayer_SelectedIndexChanged" AutoGenerateSelectButton="True"
                    OnRowDataBound="GVPlayer_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging"
                    AllowSorting="True" OnSorting="GridView1_Sorting">
                </asp:GridView>
                <asp:Label ID="lblNoRecords" Text="No records to display" runat="server" ForeColor="red"></asp:Label>
            </div>
        </article>
    </div>
</asp:Content>
