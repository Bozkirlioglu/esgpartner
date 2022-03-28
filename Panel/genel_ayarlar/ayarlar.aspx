<%@ Page Title="" Language="C#" MasterPageFile="~/Panel/main/Panel.master" AutoEventWireup="true" CodeFile="ayarlar.aspx.cs" Inherits="genelayarlar_ayarlar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--JS --%>
    <link href="../library/js/jquery-ui.css" rel="stylesheet" />
    <script src="../library/js/jquery-ui.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="rightContent">
        <asp:Panel ID="panel_basarili" runat="server">
            <asp:HiddenField ID="hd_basarili" runat="server" ClientIDMode="Static" />
        </asp:Panel>

        <h3>Ayarlar</h3>
        <div class="shortcutHome">
            <a href="../genel_ayarlar/thumbayar.aspx">
                <img src="../img/thumbnail.png" alt="" /><br />
                Thumbnail Ayarları</a>
        </div>

        <div class="clear"></div>

    </div>

    <div class="clear"></div>

</asp:Content>
