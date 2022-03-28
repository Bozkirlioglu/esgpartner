<%@ Page Title="" Language="C#" MasterPageFile="~/Panel/main/Panel.master" AutoEventWireup="true" CodeFile="sosyalmedya.aspx.cs" Inherits="pano_sosyalmedya" %>
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
                   'Sosyal Medya Yönetimi',
                   $('#hd_basarili').val(),
                   200,
                   7000
               );
                setTimeout(function () {
                    window.location = "../main/Default.aspx";
                }, 5000);
            }

            /*Hata varsa*/

            if ($('#ContentPlaceHolder1_panel_hatali').hasClass('aktif')) {
                $('.error').notifyMe(
                   'top',
                   'error',
                   'Sosyal Medya Yönetimi',
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
                Facebook:
            </div>

            <div class="txtarea">
                <asp:TextBox ID="txt_facebook" runat="server" MaxLength="100" CssClass="txt"></asp:TextBox>

            </div>

        </div>

        <asp:Panel ID="panel_etiket" runat="server">
            <div class="formsatir">

                <div class="baslik">
                    Twitter:
                </div>

                <div class="txtarea">
                    <asp:TextBox ID="txt_twitter" runat="server" MaxLength="250" CssClass="txt"></asp:TextBox>
                </div>

            </div>

        </asp:Panel>


        <asp:Panel ID="panel_kisaaciklama" runat="server">
            <div class="formsatir">

                <div class="baslik">
                    Instagram:
                </div>

                <div class="txtarea">
                    <asp:TextBox ID="txt_instagram" runat="server" MaxLength="250" CssClass="txt"></asp:TextBox>
                </div>

            </div>
        </asp:Panel>

        <asp:Panel ID="panel_kategorianahtar" runat="server">
            <div class="formsatir">

                <div class="baslik">
                    Youtube:
                </div>

                <div class="txtarea">
                    <asp:TextBox ID="txt_youtube" runat="server" MaxLength="250" CssClass="txt"></asp:TextBox>
                </div>

            </div>
        </asp:Panel>


        <asp:Panel ID="panel_kategorilink" runat="server">
            <div class="formsatir">

                <div class="baslik">
                    GooglePlus:
                </div>

                <div class="txtarea">
                    <asp:TextBox ID="txt_googleplus" runat="server" MaxLength="250" CssClass="txt"></asp:TextBox>
                </div>

            </div>
        </asp:Panel>

        <asp:Panel ID="panel_linkedin" runat="server">
            <div class="formsatir">

                <div class="baslik">
                    Linkedin:
                </div>

                <div class="txtarea">
                    <asp:TextBox ID="txt_linkedin" runat="server" MaxLength="250" CssClass="txt"></asp:TextBox>
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

