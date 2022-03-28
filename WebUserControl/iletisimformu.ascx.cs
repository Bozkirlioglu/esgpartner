using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUserControl_iletisimformu : System.Web.UI.UserControl
{
    General_Code code = new General_Code();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn_onayla_Click(object sender, EventArgs e)
    {
        string adsoyad = txt_adsoyad.Text.Trim();
        string tel = txt_tel.Text.Trim();
        string mail = txt_email.Text.Trim();
        string konu = "Siteden Bize Ulaşın Formu Dolduruldu.";
        string msg = txt_dusunceler.Text.Trim();

        if (!String.IsNullOrEmpty(adsoyad) && !String.IsNullOrEmpty(tel) && !String.IsNullOrEmpty(mail) && !String.IsNullOrEmpty(msg))
        {
            SqlConnection con = code.baglan();
            string formkaydet = "INSERT INTO contactform(iform_adi, iform_tel, iform_mail, iform_konu, iform_mesaj, iform_okundu, iform_tarih) VALUES (@iform_adi, @iform_tel, @iform_mail, @iform_konu, @iform_mesaj, @iform_okundu, @iform_tarih)";
            SqlCommand command = new SqlCommand(formkaydet, con);
            command.Parameters.AddWithValue("@iform_adi", adsoyad);
            command.Parameters.AddWithValue("@iform_tel", tel);
            command.Parameters.AddWithValue("@iform_mail", mail);
            command.Parameters.AddWithValue("@iform_konu", konu);
            command.Parameters.AddWithValue("@iform_mesaj", msg);
            command.Parameters.AddWithValue("@iform_okundu", "False");
            command.Parameters.AddWithValue("@iform_tarih", DateTime.Now.ToString());

            if (command.ExecuteNonQuery() > 0)
            {
                pnl_alert.Visible = true;
                pnl_alert.CssClass = "alert alert-success";
                label_uyari.ForeColor = System.Drawing.Color.Green;
                label_uyari.Text = "Doldurmuş olduğunuz form başarılı bir şekilde gönderildi...";

                txt_adsoyad.Text = "";
                txt_tel.Text = "";
                txt_email.Text = "";
                txt_dusunceler.Text = "";
                txt_adsoyad.Focus();
            }
        }
        else
        {
            pnl_alert.Visible = true;
            label_uyari.Text = "Lütfen zorunlu olan tüm alanları doldurunuz!";
            label_uyari.ForeColor = System.Drawing.Color.Red;
        }
    }
}