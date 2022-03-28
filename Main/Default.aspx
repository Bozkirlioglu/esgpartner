<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Main_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../library/extensions/owlcarousel/assets/owl.carousel.min.css" rel="stylesheet" />
    <link href="../library/extensions/owlcarousel/assets/owl.theme.default.min.css" rel="stylesheet" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- Hero Header -->
    <header class="hero-header" style="background-image: url('../library/img/slider2.png');">
        <div class="container">
            <div class="intro-text">
                <div class="intro-lead-in">Sürdürülebilir bir dünya için birlikte hareket edelim...</div>
                <div class="intro-heading">Sürdürülebilirlik Danışmanlığı <br />
                    Karbon Ayakizi Danışmanlığı <br />
                    Eğitimler ve Çalıştaylar
                </div>
                
                <a href="/content/iletisim" class="btn btn-primary btn-lg">Bize Ulaşın</a>
            </div>
        </div>
    </header>

    <!-- Intro -->
    <section class="page-section section-grey" id="intro">
        <div class="container intro">
            <div class="row margin-bottom-50">
                <div class="col-md-12 text-center">
                    <h1 class="title-section"><span class="title-regular"><strong>İklim Odaklı</strong></span><br />
                        Stratejik Bir Vizyon Geliştirin</h1>
                    <hr class="title-underline-center">
                    <p class="lead">Etkin bir çevre yönetimi ile hem ticari fayda sağlayabilirsiniz hem de sürdürülebilir kalkınma hedefleri doğrultusunda gezegenimizin korunmasına katkı sunabilirsiniz.</p>
                </div>
            </div>
            <%--<div class="row text-center">
                <div>
                    <div class="col-md-3 col-sm-6">
                        <div>
                            <i class="fa fa-desktop"></i>
                            <label><strong>14</strong>
                                <br/>Complete Projects</label>
                            <p>Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore</p>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-6">
                        <div>
                            <i class="fa fa-user"></i>
                            <label><strong>2543</strong>
                                <br/>Happy Clients</label>
                            <p>Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore</p>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-6">
                        <div>
                            <i class="fa fa-code"></i>
                            <label><strong>14489</strong>
                                <br/>Line Of Coding</label>
                            <p>Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore</p>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-6">
                        <div>
                            <i class="fa fa-trophy"></i>
                            <label><strong>21</strong>
                                <br/>Awards</label>
                            <p>Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore</p>
                        </div>
                    </div>
                </div>
            </div>--%>
        </div>
    </section>

    <!-- Our Clients -->
    <section class="page-section">
        <div class="container">
            <div class="row">
                <div class="col-md-4 col-md-push-8">
                    <h2 class="title-section"><span class="title-regular">REFERANSLARIMIZ</h2>
                    <hr class="title-underline" />
                    <p>
                        Referanslarımız arasında GiZ (Alman İşbiriliği Kurumu), Çevre ve Şehircilik Bakanlığı, ASO 2.OSB, İstanbul Sanayi Odası, Tezmaksan gibi kuruluşlar bulunmaktadır.
                    </p>
                </div>
                <div class="col-md-8 col-md-pull-4 text-center">
                    <div class="row">
                        <div class="col-md-4">
                            <img src="../library/img/referanslar/giz.png" alt="giz"  />
                            <%--<img src="http://placehold.it/168x168" alt="" class="client-logo" />--%>
                        </div>
                        <div class="col-md-4">
                            <img src="../library/img/referanslar/cevre.png" alt="cevresehircilikbakanligi" />
                        </div>
                        <div class="col-md-4">
                            <img src="../library/img/referanslar/aso.png" alt="aso" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <img src="../library/img/referanslar/iso.png" alt="iso" />
                        </div>
                        <div class="col-md-4">
                            <img src="../library/img/referanslar/tezmaksan.png" alt="tezmaksan"/>
                        </div>
                        <div class="col-md-4">
                           
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>

