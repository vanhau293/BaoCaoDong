<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="BaoCaoDong.main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="PanelChonBang" runat="server" BackColor="White"  Height="200px">
                <h2>Bảng</h2> 
                <br />
                <asp:CheckBoxList ID="CheckBoxListTable" runat="server">
                    <asp:ListItem>hihi</asp:ListItem>
                    <asp:ListItem>haha</asp:ListItem>
                </asp:CheckBoxList>
                <br />
            </asp:Panel>
</asp:Content>
