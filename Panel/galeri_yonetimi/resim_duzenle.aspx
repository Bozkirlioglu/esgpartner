<%@ Page Title="" Language="C#" MasterPageFile="~/Panel/main/Panel.master" AutoEventWireup="true" CodeFile="resim_duzenle.aspx.cs" Inherits="galeriyonetimi_resimduzenle" %>

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
                   'Resim Düzenleme',
                   $('#hd_basarili').val(),
                   200,
                   7000
               );
               setTimeout(function () {
                    window.location = "../galeri_yonetimi/resimler.aspx";
                }, 5000);
            }

            /*Hata varsa*/

            if ($('#ContentPlaceHolder1_panel_hatali').hasClass('aktif')) {
                $('.error').notifyMe(
                   'top',
                   'error',
                   'İçerik Düzenleme',
                   $('#hd_uyari').val() , //Hata Mesajını buraya yazdırdık...
                   200,
                   7000
               );
                setTimeout(function () {
                    window.location = "../galeri_yonetimi/resim_duzenle.aspx";
                }, 7000);
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
            <div class="baslik">Resim Adı:</div>
            <div class="txtarea">
                <asp:TextBox ID="txt_resimadi" runat="server" MaxLength="100" CssClass="txt"></asp:TextBox>
                (*)
                <asp:RequiredFieldValidator ID="kontrol_resimadi" runat="server" ControlToValidate="txt_resimadi" SetFocusOnError="true" ErrorMessage="Resim adı boş geçilemez..." ValidationGroup="slider_guncelle"></asp:RequiredFieldValidator>

            </div>
        </div>

        <div class="formsatir">
            <div class="baslik">Durumu: </div>
            <div class="txtarea">
                <asp:CheckBox ID="CheckBox1" runat="server" class="checkbox" Text="Aktif" />
            </div>
        </div>

        <asp:Panel ID="panel_kisaaciklama" runat="server">
            <div class="formsatir">
                <div class="baslik">
                    Resim Kısa Açıklama:
                </div>
                <div class="txtarea">
                    <asp:TextBox ID="txt_kisaaciklama" runat="server" MaxLength="250" CssClass="txt"></asp:TextBox>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="panel_resim" runat="server">
            <div class="formsatir">
                <div class="baslik">
                    Resim:
                </div>
                <div class="fileuploadarea">
                    <asp:FileUpload ID="resimekle" runat="server" />
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="resimekle" ErrorMessage="Dosya formatı uyumsuz(jpg,png,bmp,gif)..." ValidationExpression="^.*\.(jpg|JPG|gif|GIF|doc|DOC|png|PNG)$"></asp:RegularExpressionValidator>
                    <%--<asp:TextBox ID="TextBox1" runat="server" MaxLength="250" CssClass="txt"></asp:TextBox>--%>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="panel_icon" runat="server">
            <div class="formsatir">
                <div class="baslik">
                    Mevcut Resim:
                </div>
                <div class="fileuploadarea">
                    <asp:Image ID="galeri_image" runat="server" Width="75px" />
                </div>
            </div>
        </asp:Panel>

        <div class="formsatir">
            <div class="CKEbaslik">Resim Açıklaması:</div>
            <div class="CKEarea">
                <CKEditor:CKEditorControl ID="CKEditor_aciklama" BasePath="/ckeditor/" runat="server"></CKEditor:CKEditorControl>
            </div>
        </div>

        <div class="formsatir">
            <div class="butonarea">
                <asp:Button ID="buton_guncelle" runat="server" Text="Güncelle" CssClass="button" OnClick="Guncelle_Click" ValidationGroup="slider_guncelle" />
            </div>
        </div>

       <div class="clear"></div>

    </div>

</asp:Content>

