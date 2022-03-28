<%@ Page Title="" Language="C#" MasterPageFile="~/Panel/main/Panel.master" AutoEventWireup="true" CodeFile="tema_resim_ekle.aspx.cs" Inherits="tema_yonetimi_tema_icerik_ekle" %>
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

            if ($('#ContentPlaceHolder1_panel_basarili').hasClass('aktif')) {
                $('.success').notifyMe(
                   'top',
                   'success',
                   'Tema Yönetimi',
                   $('#hd_basarili').val(),
                   200,
                   7000
               );
                setTimeout(function () {
                    window.location = "../tema_yonetimi/tema_listesi.aspx";
                }, 5000);
            }

            /*Hata varsa*/

            if ($('#ContentPlaceHolder1_panel_hatali').hasClass('aktif')) {
                $('.error').notifyMe(
                   'top',
                   'error',
                   'Tema Yönetimi',
                   $('#hd_uyari').val(), //Hata Mesajını buraya yazdırdık...
                   200,
                   7000
               );
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
                <div class="uyari"><asp:Label ID="label_uyari" runat="server" Visible="false" ></asp:Label></div>
            </div>
        </asp:Panel>
        <asp:Panel ID="panel_hatali" runat="server"><asp:HiddenField ID ="hd_uyari" runat="server" ClientIDMode="Static" /><asp:Label ID="label_hata" runat="server" Visible="false"></asp:Label></asp:Panel>

        <div class="formsatir">

            <div class="baslik">
                Tema:
            </div>

            <div class="dropdownarea">

                <asp:DropDownList ID="DropDownList_tema" runat="server" CssClass="dropdown"></asp:DropDownList> (*)
                
            </div>

        </div>

        <div class="formsatir">

            <div class="baslik">
                Tema Resminin Bağlı Olduğu İçerik :
            </div>

            <div class="dropdownarea">
                <asp:DropDownList ID="DropDownList_Icerik" runat="server" CssClass="dropdown"></asp:DropDownList> (*)
            </div>

        </div>

        <asp:Panel ID="panel_kategoriresim" runat="server">
            <div class="formsatir">

                <div class="baslik">
                    İçerik Resim:
                </div>

                <div class="fileuploadarea">
                    <asp:FileUpload ID="resimekle" runat="server" />
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="resimekle" ErrorMessage="Dosya formatı uyumsuz(jpg,png,bmp,gif)..." ValidationExpression="^.*\.(jpg|JPG|gif|GIF|doc|DOC|png|PNG)$" ValidationGroup="icerik"></asp:RegularExpressionValidator>
                    <%--<asp:TextBox ID="TextBox1" runat="server" MaxLength="250" CssClass="txt"></asp:TextBox>--%>
                </div>


            </div>
        </asp:Panel>

        <div class="formsatir">

            <div class="butonarea">
                <asp:Button ID="buton_kaydet" runat="server" Text="Kaydet" CssClass="button" OnClick="Kaydet_Click" ValidationGroup="icerik"/>
            </div>

        </div>

        <div class="clear"></div>

    </div>

</asp:Content>

