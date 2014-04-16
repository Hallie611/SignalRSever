<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MasterPage.Master" AutoEventWireup="true" CodeBehind="QuestionDetail.aspx.cs" Inherits="SignalRSever.Web.Question.QuestionDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 139px;
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

        .auto-style5 {
            height: 23px;
            width: 139px;
        }

        .auto-style6 {
            width: 225px;
            height: 23px;
        }

        .tag {
            float: left;
            position: absolute;
            border-style:solid;
            border-color: red;
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
                        <td rowspan="8">
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
                        <td class="auto-style5"></td>
                        <td class="auto-style6"></td>
                    </tr>
                    <tr>
                        <td class="auto-style1" colspan="2">
                            <asp:Chart ID="ChartSC" runat="server">
                                <Series>
                                    <asp:Series Name="Series1" ChartType="Pie" IsValueShownAsLabel="true"></asp:Series>
                                </Series>
                                <Legends>
                                    <asp:Legend Alignment="Center" Docking="Bottom"
                                        IsTextAutoFit="False" Name="Default"
                                        LegendStyle="Row" />
                                </Legends>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </td>
                    </tr>
                </table>
                <a href='javascript:history.go(-1)'>Go Back to Previous Page</a>
            </div>
        </article>
    </div>

    <div class="art-layout-cell art-content" runat="server" id="FindBugs">
        <article class="art-post art-article">
            <h2 class="art-postheader">Question Detail Find Bugs</h2>
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
                        <td rowspan="11">
                            <div style="position: relative; float:right;">
                                <div class="tag" runat="server" id="imageCSS"></div>
                                <asp:Image ID="ImageFB" runat="server" BorderStyle="Solid" />
                            </div>
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
                        <td class="auto-style1">&nbsp;</td>
                        <td class="auto-style2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style1" colspan="2">
                            <asp:Chart ID="ChartFB" runat="server">
                                <Series>
                                    <asp:Series Name="Series1" ChartType="Pie" IsValueShownAsLabel="true"></asp:Series>
                                </Series>
                                <Legends>
                                    <asp:Legend Alignment="Center" Docking="Bottom"
                                        IsTextAutoFit="False" Name="Default"
                                        LegendStyle="Row" />
                                </Legends>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </td>
                    </tr>
                </table>
                <a href='javascript:history.go(-1)'>Go Back to Previous Page</a>
            </div>
        </article>
    </div>

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
                        <td rowspan="11">
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
                        <td class="auto-style1"></td>
                        <td class="auto-style3" colspan="3">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1" colspan="4">
                            <asp:Chart ID="ChartFK" runat="server">
                                <Series>
                                    <asp:Series Name="Series1" ChartType="Pie" IsValueShownAsLabel="true"></asp:Series>
                                </Series>
                                <Legends>
                                    <asp:Legend Alignment="Center" Docking="Bottom"
                                        IsTextAutoFit="False" Name="Default"
                                        LegendStyle="Row" />
                                </Legends>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </td>
                    </tr>
                </table>
                <a href='javascript:history.go(-1)'>Go Back to Previous Page</a>
            </div>
        </article>
    </div>
</asp:Content>
