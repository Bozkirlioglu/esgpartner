<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="iletisim.aspx.cs" Inherits="Sayfalar_iletisim" %>

<%@ Register Src="~/WebUserControl/iletisimformu.ascx" TagPrefix="uc1" TagName="iletisimformu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- Page Header -->
    <div class="page-header">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <ul class="breadcrumb">
                        <li><a href="/">Ana Sayfa</a></li>
                        <li>İletişim</li>
                    </ul>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <h1>İletişim</h1>
                </div>
            </div>
        </div>
    </div>


    <!-- CONTAKT US-->
    <section class="page-section">
        <div class="container">
            <div class="row ">

                <div class="col-md-8">
                    <h2 class="title-section"><span class="title-regular">Bize Ulaşın</span></h2>
                    <hr class="title-underline " />
                    <p>
                        Eğitim almak, teklif almak vb. istekleriniz için bizimle iletişime geçiniz.
                    </p>

                    <uc1:iletisimformu runat="server" ID="iletisimformu" />
                    <%--<div class="form-group">
                        <input type="text" class="form-control" id="usr" placeholder="Ad Soyad">
                    </div>
                    <div class="form-group">
                        <input type="text" class="form-control" id="email" placeholder="E-Mail">
                    </div>
                    <div class="form-group">
                        <textarea class="form-control" rows="5" id="message" placeholder="Messajınız"></textarea>
                    </div>
                    <button type="button" class="btn btn-default">Gönder <i class="fa fa-envelope"></i></button>--%>
                </div>

                <div class="col-md-4">
                    <%--<h3>More Info</h3>
                    <p>sit amet, consectetur adipiscing elit. Integer placerat metus id orci facilisis, in luctus eros laoreet. Mauris interdum augue varius, faucibus massa id, imperdiet tortor. Donec vel tortor molestie, hendrerit sem a, hendrerit arcu. Aliquam erat volutpat. Proin varius eros eros, non condimentum nis.</p>--%>
                    <p>
                        <strong>Adres:</strong>
                        <br />
                        Mustafa Kemal Mah. Dumlupınar Bul. No: 274 <br />
                        Mahall İş Merkezi E Blok No:78 06530 Çankaya/ANKARA/TÜRKİYE
                    </p>
                    <p><strong>Telefon:</strong>+90 (535) 372 96 98</p>
                    <p><strong>E-Mail:</strong> info@esgpartner.com.tr</p>
                    <p><strong>Çalışma Saatlerimiz:</strong> Pzt-Cum: 09.00-17.00 </p>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
