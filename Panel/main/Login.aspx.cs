using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class main_Login : System.Web.UI.Page
{
    General_Code sys = new General_Code();
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie Cookie = Request.Cookies["Kullanici"];

        if (Request.Cookies["Kullanici"] != null)
        {

            Response.Redirect("Default.aspx");

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        SqlConnection con = sys.baglan();

        SqlCommand cmd = new SqlCommand("SELECT * FROM admin_user WHERE user_name=@user_name AND user_password=@user_password", con);

        cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = txt_username.Text;
        cmd.Parameters.Add("@user_password", SqlDbType.VarChar).Value = txt_password.Text;


        SqlDataReader oku = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        if (oku.Read())
        {

            HttpCookie Cookie = new HttpCookie("Kullanici");
            Cookie["KullaniciBilgi"] = sifreleme.Encrypt(Server.UrlEncode(oku["display_name"].ToString() + " " + oku["display_surname"].ToString()));
            Cookie["GirisZamani"] = DateTime.Now.ToShortDateString();

            if (CheckBox_benihatirla.Checked)
            {
                Cookie.Expires = DateTime.Now.AddDays(7);
            }
            else
            {

                Cookie.Expires = DateTime.Now.AddDays(1);

            }

            //Ses1sion["Kullanici"] = oku["User_ID"].ToString();
            //Session["AdiSoyadi"] = oku["display_name"].ToString() + " " + oku["display_surname"].ToString();

            Response.Cookies.Add(Cookie);
            Response.Redirect("Default.aspx");
        }
        else
        {
            label_uyari.Text = "Böyle bir kullanıcı bulunamadı...";
            txt_username.Text = String.Empty;
            txt_password.Text = String.Empty;
        }
        oku.Close();
        con.Close();
    }
}