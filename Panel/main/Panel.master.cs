using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class main_Panel : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie Cookie = Request.Cookies["Kullanici"];

        if (Request.Cookies["Kullanici"] == null)
        {

            Response.Redirect("../main/login.aspx");

        }
        else
        {
            literal_kullanici.Text = Server.UrlDecode(sifreleme.Decrypt(Cookie["KullaniciBilgi"].ToString()));
        }
    }
    protected void LinkbuttonCikis_Click(object sender, EventArgs e)
    {
        Response.Cookies["Kullanici"].Expires = DateTime.Now.AddDays(-1);
        Response.Redirect("../main/login.aspx");
        //Session.Abandon();
        //Response.Redirect("login.aspx");
    }
}
