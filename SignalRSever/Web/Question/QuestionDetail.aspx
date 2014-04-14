<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MasterPage.Master" AutoEventWireup="true" CodeBehind="QuestionDetail.aspx.cs" Inherits="SignalRSever.Web.Question.QuestionDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 122px;
        }

        .auto-style2 {
            width: 225px;
        }

        .auto-style3 {
            width: 360px;
        }

        .auto-style4 {
            width: 120px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="art-layout-cell art-content" runat="server" id="SingleChoice">
        <article class="art-post art-article">
            <h2 class="art-postheader">Question Detail Single Choice</h2>
            <div class="art-postcontent art-postcontent-0 clearfix">
                <p>
                    <br />
                </p>
            </div>
            <div>
                <table style="width: 100%;">
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style2">&nbsp;</td>
                        <td rowspan="16">
                            <asp:Image ID="ImageSC" runat="server" ImageAlign="Right" BorderStyle="Solid" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Question ID: </td>
                        <td class="auto-style2">
                            <asp:Label ID="lbQuestionIDSC" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Type: </td>
                        <td class="auto-style2">
                            <asp:Label ID="lbTypeSC" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Difficulty: </td>
                        <td class="auto-style2">
                            <asp:Label ID="lbDifSC" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Answer: </td>
                        <td class="auto-style2">
                            <asp:Label ID="lbAnswerSC" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style2">&nbsp;</td>
                    </tr>
                </table>
                <a href='javascript:history.go(-1)'>Go Back to Previous Page</a>
            </div>
        </article>
    </div>

    <div class="art-layout-cell art-content" runat="server" id="FindBugs"><article class="art-post art-article">
         <h2 class="art-postheader">Question Detail Find Bugs</h2>                      
         <div class="art-postcontent art-postcontent-0 clearfix"><p><br/></p></div> 
         <div>
        <table style="width: 100%;">
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style2">&nbsp;</td>
                        <td rowspan="16">
                            <asp:Image ID="ImageFB" runat="server" ImageAlign="Right" BorderStyle="Solid" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Question ID: </td>
                        <td class="auto-style2">
                            <asp:Label ID="lbQuestionIDFB" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Type: </td>
                        <td class="auto-style2">
                            <asp:Label ID="lbTypeFB" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Difficulty: </td>
                        <td class="auto-style2">
                            <asp:Label ID="lbDifFB" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Width: </td>
                        <td class="auto-style2">
                            <asp:Label ID="lbWidth" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Height: </td>
                        <td class="auto-style2"><asp:Label ID="lbHeight" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Top: </td>
                        <td class="auto-style2"><asp:Label ID="lbTop" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Left: </td>
                        <td class="auto-style2"><asp:Label ID="lbLeft" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style2">&nbsp;</td>
                    </tr>
                </table>
        <a href='javascript:history.go(-1)'>Go Back to Previous Page</a>
        </div>         
    </article></div>

        <div class="art-layout-cell art-content" runat="server" id="FillBlank">
        <article class="art-post art-article">
            <h2 class="art-postheader">Question Detail Fill Blanks</h2>
            <div class="art-postcontent art-postcontent-0 clearfix">
                <p>
                    <br />
                </p>
            </div>
            <div>
                <table style="width: 100%;">
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style3" colspan="3">&nbsp;</td>
                        <td rowspan="16">
                            <asp:Image ID="ImageFK" runat="server" ImageAlign="Right" BorderStyle="Solid" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style3" colspan="3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Question ID: </td>
                        <td class="auto-style3" colspan="3">
                            <asp:Label ID="lbQuestionIDFK" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Type: </td>
                        <td class="auto-style3" colspan="3">
                            <asp:Label ID="lbTypeFK" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Difficulty: </td>
                        <td class="auto-style3" colspan="3">
                            <asp:Label ID="lbDifFK" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Index: </td>
                        <td class="auto-style3" colspan="3">
                            <asp:Label ID="lbIndex" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="auto-style1">List Answer: </td>
                        <td class="auto-style4">
                            <asp:Label ID="lbList1_1" runat="server"></asp:Label>
                        </td>
                        <td class="auto-style4">
                            <asp:Label ID="lbList1_2" runat="server"></asp:Label>
                        </td>
                        <td class="auto-style4">
                            <asp:Label ID="lbList1_3" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1"></td>
                        <td class="auto-style4">
                            <asp:Label ID="lbList2_1" runat="server"></asp:Label>
                        </td>
                        <td class="auto-style4">
                            <asp:Label ID="lbList2_2" runat="server"></asp:Label>
                        </td>
                        <td class="auto-style4">
                            <asp:Label ID="lbList2_3" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style4">

                            <asp:Label ID="lbList3_1" runat="server" Text=""></asp:Label>

                        </td>
                        <td class="auto-style4">

                            <asp:Label ID="lbList3_2" runat="server" Text=""></asp:Label>

                        </td>
                        <td class="auto-style4">

                            <asp:Label ID="lbList3_3" runat="server" Text=""></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style3" colspan="3">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style3" colspan="3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style3" colspan="3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1"></td>
                        <td class="auto-style3" colspan="3"></td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style3" colspan="3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style3" colspan="3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style3" colspan="3">&nbsp;</td>
                    </tr>
                </table>
                <asp:GridView ID="GV_QuestionDetail" runat="server" CssClass="EU_DataTable">
                </asp:GridView>
                <a href='javascript:history.go(-1)'>Go Back to Previous Page</a>
            </div>
        </article>
    </div>
</asp:Content>
