<%@ Page Title="" Language="C#" MasterPageFile="~/Panel/main/Panel.master" AutoEventWireup="true" CodeFile="formgoster.aspx.cs" Inherits="formyonetimi_formgoster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%-- DataTables --%>
    <link href="../library/datatables/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="../library/datatables/jquery-1.10.2.min.js"></script>
    <script src="../library/datatables/jquery.dataTables.min.js"></script>

    <%--JS --%>
    <link href="../library/js/jquery-ui.css" rel="stylesheet" />
    <script src="../library/js/jquery-ui.js"></script>

    <!--NOTIFICATION-->
    <link href="../library/notification/css/notifyme.css" rel="stylesheet" />
    <script src="../library/notification/js/notifyme.js"></script>
    <script src="../library/notification/js/modernizr.js"></script>

    <script type="text/javascript" charset="utf-8">

        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>İletişim Formu Detayı</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }

        $(document).ready(function () {

            /*Panel Başarılı*/

            if ($('#ContentPlaceHolder1_panel_basarili').hasClass('aktif')) {
                $('.success').notifyMe(
                   'top',
                   'success',
                   'İçerik Yönetimi',
                   $('#hd_basarili').val(),
                   200,
                   7000
               );
                setTimeout(function () {
                    window.location = "../icerik_yonetimi/icerik.aspx";
                }, 5000);
            }

            /*Hata varsa*/
            if ($('#ContentPlaceHolder1_panel_hatali').hasClass('aktif')) {
                $('.error').notifyMe(
                   'top',
                   'error',
                   'İçerik Düzenleme',
                   $('#hd_uyari').val(), //Hata Mesajını buraya yazdırdık...
                   200,
                   7000
               );
                setTimeout(function () {
                    window.location = "../form_yonetimi/formlar.aspx";
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
        <asp:Panel ID="panel_hatali" runat="server">
            <asp:HiddenField ID="hd_uyari" runat="server" ClientIDMode="Static" />
            <asp:Label ID="label_hata" runat="server" Visible="false"></asp:Label></asp:Panel>

        <h3>Form Yönetimi</h3>
        <div class="shortcutHome">
            <a href="../form_yonetimi/formlar.aspx">
                <img src="../img/cntform2.png" alt="" /><br />
                İletişim Formları</a>
        </div>
        <div class="shortcutHome">
            <a href="../icerik_yonetimi/icerik_ekle.aspx">
                <img src="../img/ikform.png" alt="" /><br />
                İş Başvuru Formları</a>
        </div>
        <div class="clear"></div>

        <h3>İletişim Formu Detayı </h3>
        <div class="form-dis-alan">

            <asp:Panel ID="pnlContents" runat="server">

                <span class="form-detay-baslik">Tarih :  </span>
                <span class="form-detay-duzyazi">
                    <asp:Literal ID="lt_tarih" runat="server"></asp:Literal>
                </span>
                <br />
                <br />

                <span class="form-detay-baslik">Ad Soyad :  </span>
                <span class="form-detay-duzyazi">
                    <asp:Literal ID="lt_adsoyad" runat="server"></asp:Literal>
                </span>
                <br />
                <br />
                <span class="form-detay-baslik">E-Posta :  </span>
                <span class="form-detay-duzyazi">
                    <asp:Literal ID="lt_mail" runat="server"></asp:Literal>
                </span>
                <br />
                <br />
                <span class="form-detay-baslik">Telefon :  </span>
                <span class="form-detay-duzyazi">
                    <asp:Literal ID="lt_tel" runat="server"></asp:Literal>
                </span>
                <br />
                <br />
                <span class="form-detay-baslik">Konu :  </span>
                <span class="form-detay-duzyazi">
                    <asp:Literal ID="lt_konu" runat="server"></asp:Literal>
                </span>
                <br />
                <br />
                <span class="form-detay-baslik">Mesaj :  </span>
                <span class="form-detay-duzyazi">
                    <asp:Literal ID="lt_mesaj" runat="server"></asp:Literal>
                </span>
                <br />
                <br />
            </asp:Panel>
            <br />
            <asp:Button ID="btnPrint" runat="server" CssClass="form-yazdir" Text="Yazdır" OnClientClick="return PrintPanel();" />

        </div>

        <div class="clear"></div>
    </div>

    <div class="clear"></div>

</asp:Content>
