<%@ Control Language="C#" AutoEventWireup="true" CodeFile="sosyalfooter.ascx.cs" Inherits="WebUserControl_sosyalfooter" %>

<ul class="small-icon no-margin-bottom">
    <li id="facebook" runat="server" visible="false">
        <asp:HyperLink ID="hyperlink_facebook" runat="server" CssClass="facebook text-deep-yellow" Target="_blank" rel="nofollow">
        <i class="fab fa-facebook-f" aria-hidden="true"></i>
        </asp:HyperLink>
    </li>
    <li id="twitter" runat="server" visible="false">
        <asp:HyperLink ID="hyperlink_twitter" runat="server" CssClass="twitter text-deep-yellow" Target="_blank" rel="nofollow">
        <i class="fab fa-twitter" aria-hidden="true"></i>
        </asp:HyperLink>
    </li>
    <li id="instagram" runat="server" visible="false">
        <asp:HyperLink ID="hyperlink_instagram" runat="server" CssClass="instagram text-deep-yellow" Target="_blank" rel="nofollow">
        <i class="fab fa-instagram no-margin-right" aria-hidden="true"></i>
        </asp:HyperLink>
    </li>
    <li id="youtube" runat="server" visible="false">
        <asp:HyperLink ID="hyperlink_youtube" runat="server" CssClass="youtube text-deep-yellow" Target="_blank" rel="nofollow">
        <i class="fab fa-youtube no-margin-right" aria-hidden="true"></i>
        </asp:HyperLink>
    </li>
    <li id="googleplus" runat="server" visible="false">
        <asp:HyperLink ID="hyperlink_googleplus" runat="server" CssClass="google text-deep-yellow" Target="_blank" rel="nofollow">
        <i class="fab fa-google-plus-g no-margin-right" aria-hidden="true"></i>
        </asp:HyperLink>
    </li>

    <li id="linkedin" runat="server">
        <asp:HyperLink ID="hyperlink_linkedin" runat="server" CssClass="linkedin text-deep-yellow" Target="_blank" rel="nofollow">
        <i class="fab fa-linkedin no-margin-right" aria-hidden="true"></i>
        </asp:HyperLink>
    </li>
</ul>
