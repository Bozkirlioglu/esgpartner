<%@ Page Title="" Language="C#" MasterPageFile="~/Panel/main/Panel.master" AutoEventWireup="true" CodeFile="slider.aspx.cs" Inherits="Panel_slider_yonetimi_slider" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <%-- DataTables --%>
    <link href="../library/datatables/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="../library/datatables/jquery-1.10.2.min.js"></script>
    <script src="../library/datatables/jquery.dataTables.min.js"></script>

    <%--JS --%>
    <link href="../library/js/jquery-ui.css" rel="stylesheet" />
    <script src="../library/js/jquery-ui.js"></script>

    <script type="text/javascript" charset="utf-8">
        var _confirm = false;
        function confirmCheckIn(button) {
            if (_confirm == false) {
                jQuery('<div>')
                    .html("<p>Bu kaydı silmek istediğinize emin misiniz?</p>")
                    .dialog({
                        autoOpen: true,
                        modal: true,
                        title: "Slider Silme !",
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
        </asp:Panel>
        <h3>Slider Yönetimi</h3>
        <div class="shortcutHome">
            <a href="../slider_yonetimi/slider_ekle.aspx">
                <img src="../img/addslide.png" alt="" /><br />
                Slider Ekle</a>
        </div>

        <div class="clear"></div>

        <h3>Kayıtlı Slider Listesi</h3>
        <div style="width: 800px; margin: 25px 0 0 10px;">

            <asp:Repeater ID="rpliste" runat="server" DataSourceID="SqlDataSource1" OnItemCommand="rpliste_ItemCommand">
                <HeaderTemplate>
                    <table id="dtable" class="display" style="width: 100%">
                        <thead>

                            <tr>
                                <th style="width: 7%">Sıra</th>
                                <th style="width: 13%;">Slider Resim</th>
                                <th style="width: 25%">Slider Adı</th>
                                <th style="width: 25%">Slider Kategori</th>
                                <th style="width: 20%">Slider Durum</th>
                                <th style="width: 10%">İşlem</th>

                            </tr>
                        </thead>
                </HeaderTemplate>

                <ItemTemplate>
                    <tr>

                        <td><%# GetItemIndex(Container.ItemIndex + 1 )  %> </td>
                        <td>
                            <img height="36" src="slider_resim/<%#Eval("slider_resim") %>" /></td>
                        <td><%# Eval("slider_adi") %></td>
                        <td><%# Eval("kategori_adi") %></td>
                        <td><%# Eval("slider_durum").ToString() == "True" ? "<img src='../img/true.png' />" : "<img src='../img/false.png' />" %></td>
                        <td>
                            <asp:Button runat="server" CssClass="linkbdataedit" CommandName="Guncelle" CommandArgument='<%#Eval("slider_id") %>'></asp:Button>
                            <asp:Button ID="sil" runat="server" CssClass="linkbdatadelete" CommandName="Delete" CommandArgument='<%#Eval("slider_id") %>' OnClientClick="return confirmCheckIn(this);"></asp:Button>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>    
                </FooterTemplate>

            </asp:Repeater>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT slider.slider_kategori, category.kategori_adi, slider.* FROM slider INNER JOIN category ON slider.slider_kategori = category.kategori_id ORDER BY category.kategori_id"></asp:SqlDataSource>
        </div>

        <div class="clear"></div>
    </div>

    <div class="clear"></div>

</asp:Content>
