<%@ Page Title="" Language="C#" MasterPageFile="~/Panel/main/Panel.master" ClientIDMode="Static" AutoEventWireup="true" CodeFile="resim_ekle.aspx.cs" Inherits="galeri_yonetimi_resim_ekle" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!--CKE EDiTÖR-->
    <script src="../ckeditor/_Samples/ckeditor/ckeditor.js"></script>

    <!--NOTIFICATION-->
    <link href="../library/notification/css/notifyme.css" rel="stylesheet" />
    <script src="../library/notification/js/notifyme.js"></script>
    <script src="../library/notification/js/modernizr.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            if ($('#panel_basarili').hasClass('aktif')) {
                $('.success').notifyMe(
                   'top',
                   'success',
                   'Resim Ekleme',
                   $('#hd_basarili').val(),
                   200,
                   7000
               );
                setTimeout(function () {
                    window.location = "../galeri_yonetimi/resimler.aspx";
                }, 5000);
            }

            /*Hata varsa*/

            if ($('#panel_hatali').hasClass('aktif')) {
                $('.error').notifyMe(
                   'top',
                   'error',
                   'Resim Ekleme',
                   $('#hd_uyari').val(), //Hata Mesajını buraya yazdırdık...
                   200,
                   7000
               );s
                setTimeout(function () {
                    window.location = "../galeri_yonetimi/resimler.aspx";
                }, 5000);
            }


        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="rightContent">

        <asp:Panel ID="panel_basarili" runat="server">
            <asp:HiddenField ID="hd_basarili" runat="server" ClientIDMode="Static" />
        </asp:Panel>
        <asp:Panel ID="panel_uyari" runat="server">
            <div class="uyari_hatali">
                <img src="../img/hatali.png" />
                <div class="uyari">
                    <asp:Label ID="label_uyari" runat="server" Visible="false"></asp:Label>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="panel_hatali" runat="server">
            <asp:HiddenField ID="hd_uyari" runat="server" ClientIDMode="Static" />
            <asp:Label ID="label_hata" runat="server" Visible="false"></asp:Label>
        </asp:Panel>


        <div class="formsatir">

            <div class="baslik">
                Galeri Kategorisi
            </div>

            <div class="dropdownarea">
                <asp:DropDownList ID="DropDownList_kategori" runat="server" CssClass="dropdown"></asp:DropDownList>
            </div>

        </div>


        <div class="formsatir">

            <div class="baslik">
                Resim Seç:
            </div>

            <div class="txtarea">
                <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true" />
                <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="UploadMultipleFiles" accept="image/*" />
            </div>

        </div>
        <div class="formsatir">

            <asp:Panel ID="panel_txt" runat="server" CssClass="eklenen-resimler"></asp:Panel>

            <asp:Label ID="lblSuccess" runat="server" ForeColor="Green" />

        </div>


        <%--<div class="formsatir">

            <div class="baslik">
                Durumu:
            </div>

            <div class="txtarea">
                <asp:CheckBox ID="CheckBox1" runat="server" class="checkbox" Text="Aktif" />
            </div>

        </div>--%>

        <div class="formsatir">

            <div class="butonarea">
                <asp:Button ID="buton_kaydet" runat="server" Text="Kaydet" CssClass="button" OnClick="Kaydet_Click" ValidationGroup="icerik" />
            </div>

        </div>

        <div class="clear"></div>

    </div>

</asp:Content>

