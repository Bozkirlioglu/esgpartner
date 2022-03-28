<%@ Page Title="" Language="C#" MasterPageFile="~/Panel/main/Panel.master" AutoEventWireup="true" CodeFile="altkategori_ekle.aspx.cs" Inherits="Panel_kategori_yonetimi_altkategori_ekle" %>

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
                   'Kategori Ekleme',
                   $('#hd_basarili').val(),
                   200,
                   7000
               );
                setTimeout(function () {
                    window.location = "../kategori_yonetimi/kategori.aspx";
                }, 5000);
            }

            /*Hata varsa*/

            if ($('#ContentPlaceHolder1_panel_hatali').hasClass('aktif')) {
                $('.error').notifyMe(
                   'top',
                   'error',
                   'Kategori Ekleme',
                   $('#hd_uyari').val(), //Hata Mesajını buraya yazdırdık...
                   200,
                   7000
               );
                //setTimeout(function () {
                //    window.location = "../slider_yonetimi/slider.aspx";
                //}, 3000);
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
                    <asp:Label ID="label_uyari" runat="server" Visible="false"></asp:Label></div>
            </div>
        </asp:Panel>
        <asp:Panel ID="panel_hatali" runat="server">
            <asp:HiddenField ID="hd_uyari" runat="server" ClientIDMode="Static" />
            <asp:Label ID="label_hata" runat="server" Visible="false"></asp:Label></asp:Panel>

        <div class="formsatir">

            <div class="baslik">
                Kategori Adı:
            </div>

            <div class="txtarea">
                <asp:TextBox ID="txt_kategoriadi" runat="server" MaxLength="100" CssClass="txt"></asp:TextBox>
                (*)
                <asp:RequiredFieldValidator ID="kontrol_kategoriadi" runat="server" ControlToValidate="txt_kategoriadi" SetFocusOnError="true" ErrorMessage="Kategori adı boş geçilemez..." ValidationGroup="kategori_ekle"></asp:RequiredFieldValidator>
            </div>

        </div>

        <div class="formsatir">

            <div class="baslik">
                Durumu:
            </div>

            <div class="txtarea">
                <asp:CheckBox ID="CheckBox1" runat="server" class="checkbox" Text="Aktif" />
            </div>

        </div>

        <div class="formsatir">

            <div class="baslik">
                Kategori Türü:
            </div>

            <div class="dropdownarea">
                <asp:DropDownList ID="DropDownList_kategoritype" runat="server" CssClass="dropdown" OnSelectedIndexChanged="DropDownList_kategoritype_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </div>


        </div>

        <div class="formsatir">

            <div class="baslik">
                İlişkili Kategori:
            </div>

            <div class="dropdownarea">
                <asp:DropDownList ID="dd_kategori" runat="server" CssClass="dropdown"></asp:DropDownList>
            </div>


        </div>

        <asp:Panel ID="panel_etiket" runat="server">
            <div class="formsatir">

                <div class="baslik">
                    Etiket:
                </div>

                <div class="txtarea">
                    <asp:TextBox ID="txt_etiket" runat="server" MaxLength="250" CssClass="txt"></asp:TextBox>
                </div>


            </div>

        </asp:Panel>


        <asp:Panel ID="panel_kisaaciklama" runat="server">
            <div class="formsatir">

                <div class="baslik">
                    Kategori Kısa Açıklama:
                </div>

                <div class="txtarea">
                    <asp:TextBox ID="txt_kisaaciklama" runat="server" MaxLength="250" CssClass="txt"></asp:TextBox>
                </div>


            </div>
        </asp:Panel>

        <asp:Panel ID="panel_kategorianahtar" runat="server">
            <div class="formsatir">

                <div class="baslik">
                    Anahtar Kelime:
                </div>

                <div class="txtarea">
                    <asp:TextBox ID="txt_anahtarkelime" runat="server" MaxLength="250" CssClass="txt"></asp:TextBox>
                </div>


            </div>
        </asp:Panel>


        <asp:Panel ID="panel_kategorilink" runat="server">
            <div class="formsatir">

                <div class="baslik">
                    Kategori Link:
                </div>

                <div class="txtarea">
                    <asp:TextBox ID="txt_kategorilink" runat="server" MaxLength="250" CssClass="txt"></asp:TextBox>
                </div>

            </div>
        </asp:Panel>

        <asp:Panel ID="panel_kategoriresim" runat="server">
            <div class="formsatir">

                <div class="baslik">
                    Kategori Resim:
                </div>

                <div class="fileuploadarea">
                    <asp:FileUpload ID="resimekle" runat="server" />
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="resimekle" ErrorMessage="Dosya formatı uyumsuz(jpg,png,bmp,gif)..." ValidationExpression="^.*\.(jpg|JPG|gif|GIF|doc|DOC|png|PNG)$"></asp:RegularExpressionValidator>
                    <%--<asp:TextBox ID="TextBox1" runat="server" MaxLength="250" CssClass="txt"></asp:TextBox>--%>
                </div>


            </div>
        </asp:Panel>

        <asp:Panel ID="panel_kategori_aciklama" runat="server">
            <div class="formsatir">

                <div class="CKEbaslik">
                    Kategori Açıklama:
                </div>

                <div class="CKEarea">
                    <CKEditor:CKEditorControl ID="CKEditor_aciklama" runat="server" Width="830px" Height="250px"></CKEditor:CKEditorControl>
                </div>

            </div>
        </asp:Panel>

        <div class="formsatir">


            <div class="butonarea">
                <asp:Button ID="buton_kaydet" runat="server" Text="Kaydet" CssClass="button" OnClick="Kaydet_Click" ValidationGroup="kategori_ekle" />
            </div>

        </div>

        <div class="clear"></div>

    </div>

</asp:Content>

