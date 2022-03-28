using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class slider_duzenle : System.Web.UI.Page
{
    General_Code code = new General_Code();
    string id, slider_resim;
    protected void Page_Load(object sender, EventArgs e)
    {

        panel_basarili.Visible = false;
        panel_hatali.Visible = false;
        panel_uyari.Visible = false;
        txt_slideradi.Focus();
        if (!IsPostBack)
        {
            KategoriGetir();
            VeriGetir();
        }
    }
    protected void Guncelle_Click(object sender, EventArgs e)
    {

        id = Request.QueryString["sliderid"];

        string slider_adi = txt_slideradi.Text;
        string slider_durumu = "";
        if (CheckBox1.Checked == true)
        {
            slider_durumu = "True";
        }
        else
        {
            slider_durumu = "False";
        }
        string slider_kategori = DropDownList_sliderkategori.SelectedValue;
        string slider_etiket = txt_etiket.Text;
        string slider_kisaaciklama = txt_kisaaciklama.Text;
        string slider_anahtarkelime = txt_anahtarkelime.Text;
        string slider_link = txt_sliderlink.Text;
        string slider_aciklama = CKEditor_aciklama.Text;
        string slider_tarih = DateTime.Now.ToString();


        SqlConnection con = code.baglan();
        try
        {

            if (resimekle.HasFile)
            {
                slider_resim = code.ResimEkle(resimekle, txt_slideradi, Server.MapPath("slider_resim/"), "_sliderresim_", panel_uyari, panel_basarili, label_hata);
            }
            else
            { 
                string strQuery = "select slider_resim from slider where slider_id = @id";
                SqlCommand cmd = new SqlCommand(strQuery);
                cmd.Parameters.AddWithValue("@id", id.Trim());
                DataRow dr = code.GetDataRow(cmd);
                if (dr != null)
                {
                    slider_resim = dr["slider_resim"].ToString();
                }
            }

            string slider_guncelle = "UPDATE slider SET slider_adi=@slider_adi, slider_etiket=@slider_etiket, slider_kisaaciklama=@slider_kisaaciklama, slider_anahtar=@slider_anahtar, slider_url = @slider_url, slider_kategori = @slider_kategori, slider_resim = @slider_resim ,slider_durum = @slider_durum, slider_aciklama=@slider_aciklama, slider_tarih = @slider_tarih WHERE slider_id = @slider_id";
            SqlCommand command = new SqlCommand(slider_guncelle, con);
            command.Parameters.AddWithValue("@slider_adi", slider_adi);
            command.Parameters.AddWithValue("@slider_etiket", slider_etiket);
            command.Parameters.AddWithValue("@slider_kisaaciklama", slider_kisaaciklama);
            command.Parameters.AddWithValue("@slider_anahtar", slider_anahtarkelime);
            command.Parameters.AddWithValue("@slider_url", slider_link);
            command.Parameters.AddWithValue("@slider_aciklama", slider_aciklama);
            command.Parameters.AddWithValue("@slider_durum", slider_durumu.ToString());
            command.Parameters.AddWithValue("@slider_kategori", slider_kategori);
            command.Parameters.AddWithValue("@slider_resim", slider_resim);
            command.Parameters.AddWithValue("@slider_tarih", slider_tarih);
            command.Parameters.AddWithValue("@slider_id", id);
            command.ExecuteNonQuery();
            command.Dispose();
            con.Close();
            panel_basarili.CssClass = "aktif";
            panel_basarili.Visible = true;
            hd_basarili.Value = slider_adi + " Sliderınız başarıyla güncellenmiştir. Slider listeleme sayfasına yönlendiriliyorsunuz. Lütfen Bekleyiniz...";

        }
        catch (Exception ex)
        {
            panel_hatali.CssClass = "aktif";
            panel_hatali.Visible = true;
            hd_uyari.Value = "Slider ekleme sırasında bir hata oluştu... Hata: " + ex;
        }

    }
    protected void VeriGetir()
    {
        try
        {
            id = Request.QueryString["sliderid"];

            string strQuery = "select * from slider where slider_id = @id";
            SqlCommand cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@id", id.Trim());
            DataRow dr = code.GetDataRow(cmd);
            if (dr != null)
            {
                txt_slideradi.Text = dr["slider_adi"].ToString();
                string durum = dr["slider_durum"].ToString();
                if (durum == "True")
                {
                    CheckBox1.Checked = true;
                }
                DropDownList_sliderkategori.SelectedValue = dr["slider_kategori"].ToString();
                txt_etiket.Text = dr["slider_etiket"].ToString();
                txt_kisaaciklama.Text = dr["slider_kisaaciklama"].ToString();
                txt_anahtarkelime.Text = dr["slider_anahtar"].ToString();
                txt_sliderlink.Text = dr["slider_url"].ToString();
                slider_resim = dr["slider_resim"].ToString();
                slider_image.ImageUrl = "slider_resim/" + dr["slider_resim"].ToString();
                CKEditor_aciklama.Text = dr["slider_aciklama"].ToString();
            }
        }
        catch (Exception ex)
        {
            panel_hatali.CssClass = "aktif";
            panel_hatali.Visible = true;
            hd_uyari.Value = "Bir hata oluştu. Slider listeme sayfasına yönlendiriliyorsunuz. Lütfen bekleyiniz... Hata: " + ex;
        }
    }

    protected void KategoriGetir()
    {
        SqlConnection con = code.baglan();
        SqlCommand sorgu = new SqlCommand("SELECT kategori_id, kategori_adi FROM category WHERE (kategori_turu = 3) AND (kategori_durum = 1)", con);
        SqlDataAdapter da = new SqlDataAdapter(sorgu);
        DataTable dt = new DataTable();

        da.Fill(dt);

        List<ListItem> items = new List<ListItem>();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                items.Add(new ListItem(dt.Rows[i]["kategori_adi"].ToString(), dt.Rows[i]["kategori_id"].ToString())); //DropDownList_sliderkategori.Items.Add(dt.Rows[i]["kategori_adi"].ToString());     
            }
            DropDownList_sliderkategori.Items.AddRange(items.ToArray());
        }
        con.Close();
        da.Dispose();
    }
}