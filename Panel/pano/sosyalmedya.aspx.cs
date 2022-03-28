using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pano_sosyalmedya : System.Web.UI.Page
{
    General_Code code = new General_Code();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        panel_basarili.Visible = false;
        panel_hatali.Visible = false;
        panel_uyari.Visible = false;
        if(!IsPostBack)
        { 
            SosyalMedyaGetir();
        }
    }
    protected void Kaydet_Click(object sender, EventArgs e)
    {
        string facebook = txt_facebook.Text;
        string twitter = txt_twitter.Text;
        string instagram = txt_instagram.Text;
        string youtube = txt_youtube.Text;
        string googleplus = txt_googleplus.Text;
        string linkedin = txt_linkedin.Text;
      
        SqlConnection con = code.baglan();
        try
        {
            string faceguncelle = "UPDATE social SET sosyal_link=@sosyal_link where sosyal_id = 1";
            SqlCommand cmdface = new SqlCommand(faceguncelle, con);
            cmdface.Parameters.AddWithValue("@sosyal_link", facebook);
            cmdface.ExecuteNonQuery();
            cmdface.Dispose();

            string twitguncelle = "UPDATE social SET sosyal_link=@sosyal_link where sosyal_id = 2";
            SqlCommand cmdtwit = new SqlCommand(twitguncelle, con);
            cmdtwit.Parameters.AddWithValue("@sosyal_link", twitter);
            cmdtwit.ExecuteNonQuery();
            cmdtwit.Dispose();

            string instagramguncelle = "UPDATE social SET sosyal_link=@sosyal_link where sosyal_id = 3";
            SqlCommand cmdinstagram = new SqlCommand(instagramguncelle, con);
            cmdinstagram.Parameters.AddWithValue("@sosyal_link", instagram);
            cmdinstagram.ExecuteNonQuery();
            cmdinstagram.Dispose();

            string youtubeguncelle = "UPDATE social SET sosyal_link=@sosyal_link where sosyal_id = 4";
            SqlCommand cmdyoutube = new SqlCommand(youtubeguncelle, con);
            cmdyoutube.Parameters.AddWithValue("@sosyal_link", youtube);
            cmdyoutube.ExecuteNonQuery();
            cmdyoutube.Dispose();

            string googleguncelle = "UPDATE social SET sosyal_link=@sosyal_link where sosyal_id = 5";
            SqlCommand cmdgoogle = new SqlCommand(googleguncelle, con);
            cmdgoogle.Parameters.AddWithValue("@sosyal_link", googleplus);
            cmdgoogle.ExecuteNonQuery();
            cmdgoogle.Dispose();

            string linkedinguncelle = "UPDATE social SET sosyal_link=@sosyal_link where sosyal_id = 7";
            SqlCommand cmdlinkedin = new SqlCommand(linkedinguncelle, con);
            cmdlinkedin.Parameters.AddWithValue("@sosyal_link", linkedin);
            cmdlinkedin.ExecuteNonQuery();
            cmdlinkedin.Dispose();

            con.Close();
            panel_basarili.CssClass = "aktif";
            panel_basarili.Visible = true;
            hd_basarili.Value = " Sosyal medya linkleri başarıyla eklendi. Pano' ya yönlendiriliyorsunuz. Lütfen bekleyiniz...";

        }
        catch (Exception ex)
        {
            panel_hatali.CssClass = "aktif";
            panel_hatali.Visible = true;
            hd_uyari.Value = "Sosyal medya ekleme sırasında bir hata oluştu... Hata: " + ex;
        }
        
    }

    void SosyalMedyaGetir()
    {
        SqlConnection con = code.baglan();
        SqlCommand sorgu = new SqlCommand("SELECT social.* FROM social ", con);
        SqlDataAdapter da = new SqlDataAdapter(sorgu);
        DataTable dt = new DataTable();

        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            txt_facebook.Text = dt.Rows[0][2].ToString();
            txt_twitter.Text = dt.Rows[1][2].ToString();
            txt_instagram.Text = dt.Rows[2][2].ToString();
            txt_youtube.Text = dt.Rows[3][2].ToString();
            txt_googleplus.Text = dt.Rows[4][2].ToString();
            txt_linkedin.Text = dt.Rows[6][2].ToString();
        }
        con.Close();
        da.Dispose();    
    
    }
}