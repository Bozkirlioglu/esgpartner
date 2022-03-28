<%@ Control Language="C#" AutoEventWireup="true" CodeFile="iletisimformu.ascx.cs" Inherits="WebUserControl_iletisimformu" ClientIDMode="Static" %>



<%--<asp:TextBox ID="txt_konu" runat="server" CssClass="border-radius-4 bg-white medium-input" placeholder="Konu (*)" MaxLength="50"></asp:TextBox>--%>

<div class="form-group">
    <asp:TextBox ID="txt_adsoyad" runat="server" CssClass="form-control" placeholder="Ad Soyad(*)" MaxLength="50"></asp:TextBox>
</div>
<div class="form-group">
    <asp:TextBox ID="txt_tel" runat="server" CssClass="form-control" placeholder="Telefon(*)" MaxLength="17"></asp:TextBox>
    <%-- <input type="text" class="form-control" id="email" placeholder="E-Mail">--%>
</div>
<div class="form-group">
    <asp:TextBox ID="txt_email" runat="server" CssClass="form-control" placeholder="E-Mail(*)" MaxLength="50"></asp:TextBox>
    <%-- <input type="text" class="form-control" id="email" placeholder="E-Mail">--%>
</div>
<div class="form-group">
    <asp:TextBox ID="txt_dusunceler" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" placeholder="Mesajınız(*)"></asp:TextBox>
</div>


<asp:Panel ID="pnl_alert" CssClass="alert alert-danger" runat="server" Visible="false">
    <p><asp:Label ID="label_uyari" runat="server"></asp:Label></p>
</asp:Panel>

<asp:LinkButton ID="btn_onayla" runat="server" CssClass="btn btn-default" OnClick="btn_onayla_Click"> Gönder <i class="fa fa-envelope"></i></asp:LinkButton>
<%--<button type="button" class="btn btn-default">Gönder <i class="fa fa-envelope"></i></button>--%>
<%--<asp:Button ID="btn_onayla" runat="server" Text="Gönder" CssClass="btn btn-default" OnClick="btn_onayla_Click" />--%>