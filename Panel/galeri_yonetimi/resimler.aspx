<%@ Page Title="" Language="C#" MasterPageFile="~/Panel/main/Panel.master" AutoEventWireup="true" CodeFile="resimler.aspx.cs" Inherits="galeriyonetimi_resimler" %>

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
        var _confirm = false;
        function confirmCheckIn(button) {
            if (_confirm == false) {
                jQuery('<div>')
                    .html("<p>Bu kaydı silmek istediğinize emin misiniz?</p>")
                    .dialog({
                        autoOpen: true,
                        modal: true,
                        title: "İçerik Silme !",
                        buttons: {
                            "Hayır": function () {
                                jQuery(this).dialog("close");
                            },
                            "Evet": function () {
                                jQuery(this).dialog("close");
                                _confirm = true;
                                button.click();
                            }
                        },
                        close: function () {
                            jQuery(this).remove();
                        }
                    });
            }

            return _confirm;
        }

        $(document).ready(function () {
            $('#dtable').dataTable({
                "ServerSide": true,
                "language": {
                    "lengthMenu": "Sayfada _MENU_ adet kayıt göstereliyor. ",
                    "zeroRecords": "Üzgünüz Eşleşen Kayıt Bulunamadı...",
                    "info": "_PAGES_ sayfadan _PAGE_. sayfa gösteriliyor.",
                    "infoEmpty": "Üzgünüz Kayıt Bulunamadı...",
                    "sSearch": "Ara",
                    "infoFiltered": "(toplam _MAX_ kayıt içerisinden)"

                }
            });

            /*Panel Başarılı*/

            if ($('#ContentPlaceHolder1_panel_basarili').hasClass('aktif')) {
                $('.success').notifyMe(
                   'top',
                   'success',
                   'Galeri Yönetimi',
                   $('#hd_basarili').val(),
                   200,
                   7000
               );
                setTimeout(function () {
                    window.location = "../galeri_yonetimi/resimler.aspx";
                }, 5000);
            }

        });
    </script>

    <script language="C#" runat="Server">
        //needed as cannot get Container.ItemIndex directly
        int GetItemIndex(int index)
        {
            return index;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="rightContent">
        <asp:Panel ID="panel_basarili" runat="server">
            <asp:HiddenField ID="hd_basarili" runat="server" ClientIDMode="Static" />
        </asp:Panel>
        
        <h3>Resim Ekle</h3>
        <div class="shortcutHome">
            <a href="../galeri_yonetimi/resim_ekle.aspx">
                <img src="../img/addcat.png" alt="" /><br />
                Resim Ekle</a>
        </div>
       
        <div class="clear"></div>
       
        <h3> Kayıtlı Resim Listesi </h3>
        <div style="width: 800px; margin: 25px 0 0 10px;">

            <asp:Repeater ID="rpliste" runat="server" OnItemCommand="rpliste_ItemCommand">
                <HeaderTemplate>
                    <table id="dtable" class="display" style="width: 100%">
                        <thead>

                            <tr>
                                <th style="width: 7%">Sıra</th>
                                <th style="width: 13%;">Resim</th>
                                <th style="width: 35%">Resim Adı</th>
                                <th style="width: 15%">Resim Durumu</th>
                                <th style="width: 30%">İşlem</th>

                            </tr>
                        </thead>
                </HeaderTemplate>

                <ItemTemplate>
                    <tr>

                        <td><%# GetItemIndex(Container.ItemIndex + 1 )  %> </td>
                        <td>
                            <img height="36" src="galeri_resim/<%#Eval("galeri_resim") %>" /></td>
                        <td><%# Eval("resim_adi") %></td>
                        <td><%# Eval("resim_durum").ToString() == "True" ? "<img src='../img/true.png' />" : "<img src='../img/false.png' />" %></td>
                        <td>
                            <asp:Button runat="server" CssClass="linkbdataedit" CommandName="Guncelle" CommandArgument='<%#Eval("resim_id") %>'></asp:Button>
                            <asp:Button ID="sil" runat="server" CssClass="linkbdatadelete" CommandName="Delete" CommandArgument='<%#Eval("resim_id") %>' OnClientClick="return confirmCheckIn(this);"></asp:Button>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>    
                </FooterTemplate>
            
            </asp:Repeater>

        </div>

        <div class="clear"></div>
    </div>

    <div class="clear"></div>

</asp:Content>
