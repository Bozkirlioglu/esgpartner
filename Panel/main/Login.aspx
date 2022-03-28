<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="main_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../css/adminstyle.css" rel="stylesheet" />
    <title>SeikoSoft Panel | Login</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="kapla">

            <div id="header">
            </div>

            <div id="loginForm">

                <div class="headloginForm">

                    <table style="width: 420px">
                        <tr>
                            <td style="width: 280px;">Admin Panel Login</td>
                            <td style="width: 140px; float: right;">Şifremi Unuttum</td>
                        </tr>
                    </table>
                </div>

                <div class="fieldlogin">

                    <div class="txtarea">
                        <asp:TextBox ID="txt_username" runat="server" MaxLength="20" PlaceHolder="Kullanıcı Adı" CssClass="txt"></asp:TextBox>
                    </div>
                    <div class="txtarea">
                        <asp:TextBox ID="txt_password" runat="server" TextMode="Password" MaxLength="20" PlaceHolder="Şifre" CssClass="txt"></asp:TextBox>
                    </div>
                    <div class="btnsatir">
                        <div class="benihatirla">
                            <asp:CheckBox ID="CheckBox_benihatirla" runat="server" Text="Beni Hatırla" />
                        </div>
                        <div class="btnarea">
                            <asp:Button ID="Button1" runat="server" Text="Giriş" CssClass="button" OnClick="Button1_Click" />
                        </div>
                    </div>
                    <div class="txtarea">
                        <asp:Label ID="label_uyari" runat="server" CssClass="labeluyari"></asp:Label>
                    </div>

                </div>

            </div>

            <div class="powered">ADMİN PANEL | Powered by SEIKOSOFT</div>

        </div>
    </form>
</body>
</html>
