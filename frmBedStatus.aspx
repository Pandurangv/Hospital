<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmBedStatus.aspx.cs" Inherits="Hospital.frmBedStatus" %>
<%@ Reference Control="~/UserControls/BedImageControl.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Chakan Criticare Hospital</title>
    <link rel="icon" type="image/png" href="" />
    <style type="text/css">
        .accordionHeader
        {
            border: 1px solid #2F4F4F;
            color: white;
            background-color: Green;
            font-family: Arial, Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
            text-align: left;
        }
        .accordionHeaderSelected
        {
            border: 1px solid #2F4F4F;
            color: white;
            background-color: Green;
            font-family: Arial, Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
            text-align: left;
        }
        .accordionContent
        {
            background-color: #E0F0E8;
            border: 1px solid green;
            border-top: none;
            padding: 5px;
            padding-top: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="95%" style="padding-left: 0px;">
        <tr>
            <td align="left">
                <asp:Label ID="Label1" runat="server" Text="Bed Status " Font-Bold="true" Font-Names="Verdana"
                    Font-Size="16px" ForeColor="Green"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="100%" style="padding-left: 0px;">
        <tr>
            <td>
                <cc:Accordion ID="accGovind" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                    ContentCssClass="accordionContent" FramesPerSecond="40" TransitionDuration="250"
                    SelectedIndex="0" SuppressHeaderPostbacks="True" Width="1030px" Height="400px"
                    RequireOpenedPane="False">
                    <Panes>
                        <cc:AccordionPane ID="acpProfile" runat="server" Width="100%">
                            <Header>
                                <a href="" style="color: White">First Floor :</a>
                            </Header>
                            <Content>
                                <asp:Panel ID="pnlFirst" runat="server" Width="100%" ScrollBars="Horizontal" BorderWidth="1px"
                                    BorderColor="Green" BackColor="#E0F0E8">
                         
                                </asp:Panel>
                            </Content>
                        </cc:AccordionPane>
                        <cc:AccordionPane ID="acpCompanyProfile" runat="server">
                            <Header>
                                <a href="" style="color: White">Second Floor :</a>
                            </Header>
                            <Content>
                                <asp:Panel ID="pnlSecond" runat="server" Width="100%" ScrollBars="Horizontal" BorderWidth="1px"
                                    BorderColor="Green" BackColor="#E0F0E8">
                                </asp:Panel>
                            </Content>
                        </cc:AccordionPane>
                        <cc:AccordionPane ID="AcpInfra" runat="server">
                            <Header>
                                <a href="" style="color: White">Third Floor :</a>
                            </Header>
                            <Content>
                                <asp:Panel ID="pnlThird" runat="server" Width="100%" ScrollBars="Horizontal" BorderWidth="1px"
                                    BorderColor="Green" BackColor="#E0F0E8">
                                </asp:Panel>
                            </Content>
                        </cc:AccordionPane>
                        <cc:AccordionPane ID="acpClients" runat="server">
                            <Header>
                                <a href="" style="color: White">Fourth Floor :</a>
                            </Header>
                            <Content>
                                <asp:Panel ID="pnlFourth" runat="server" Width="100%" ScrollBars="Horizontal" BorderWidth="1px"
                                    BorderColor="Green" BackColor="#E0F0E8">
                                </asp:Panel>
                            </Content>
                        </cc:AccordionPane>
                    </Panes>
                </cc:Accordion>
            </td>
        </tr>
    </table>
</asp:Content>
