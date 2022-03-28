<%@ Page Title="" Language="C#" MasterPageFile="~/Panel/main/Panel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="main_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="rightContent">
        <h3>Pano</h3>
        <div class="shortcutHome">
            <a href="../genel_ayarlar/meta.aspx">
                <img src="../img/seo.png" alt="" /><br />
                Seo Yönetimi</a>
        </div>
        <div class="shortcutHome">
            <a href="../slider_yonetimi/slider.aspx">
                <img src="../img/addslide.png" alt="" /><br />
                Slayt Yönetimi</a>
        </div>
        <div class="shortcutHome">
            <a href="../galeri_yonetimi/resimler.aspx">
                <img src="../img/gallery.png" alt="" /><br />
                Galeri Yönetimi</a>
        </div>
        <div class="shortcutHome">
            <a href="../pano/sosyalmedya.aspx">
                <img src="../img/social.png" alt="" /><br />
                Sosyal Medya</a>
        </div>

        <div class="clear"></div>

    </div>

</asp:Content>