<%@ Control Language="C#" AutoEventWireup="true" CodeFile="slider.ascx.cs" Inherits="WebUserControl_slider" %>


<div class="carousel slide">
    <ol class="carousel-indicators">

        <asp:Repeater ID="rp_indicators" runat="server">
            <ItemTemplate>
                <li data-target="#main-slider" data-slide-to="<%#Container.ItemIndex %>"></li>
            </ItemTemplate>
        </asp:Repeater>

    </ol>
    <div class="carousel-inner">

        <asp:Repeater ID="rp_slider" runat="server">
            <ItemTemplate>

                <div class="item"  <%# "style='background-image: url(/Panel/slider_yonetimi/slider_resim/" + Eval("slider_resim") + ")'" %>>
                    <div class="container">
                        <div class="row slide-margin">
                            <div class="col-sm-6">
                                <div class="carousel-content">
                                    <%--<h1 class="animation animated-item-1"><%#Eval("slider_adi") %> </h1>--%>
                                    <h2 class="animation animated-item-2"><%#Eval("slider_kisaaciklama") %> </h2>
                                    <%--<a class="btn-slide animation animated-item-3" href="#">Read More</a>--%>
                                </div>
                            </div>

                            <%--<div class="col-sm-6 hidden-xs animation animated-item-4">
                            <div class="slider-img">
                                <img src="images/slider/img1.png" class="img-responsive" />
                            </div>
                        </div>--%>
                        </div>
                    </div>
                </div>

            </ItemTemplate>
        </asp:Repeater>

    </div>
    <!--/.carousel-inner-->
</div>
<!--/.carousel-->

<a class="prev hidden-xs" href="#main-slider" data-slide="prev">
    <i class="fa fa-chevron-left"></i>
</a>
<a class="next hidden-xs" href="#main-slider" data-slide="next">
    <i class="fa fa-chevron-right"></i>
</a>