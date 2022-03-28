using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Panel_slider_yonetimi_slider_ekle : System.Web.UI.Page
{
    General_Code code = new General_Code();
    string slider_adi;
    string slider_durumu;
    string slider_kategori;
    string slider_etiket;
    string slider_kisaaciklama;
    string slider_anahtarkelime;
    string slider_link;
    string slider_aciklama;
    string slider_resim;
    string slider_tarih;
    protected void Page_Load(object sender, EventArgs e)
    {
        txt_slideradi.Focus();
        KategoriGetir();
        panel_basarili.Visible = false;
        panel_hatali.Visible = false;
        panel_uyari.Visible = false;
    }
    protected void Kaydet_Click(object sender, EventArgs e)
    {

        slider_adi = txt_slideradi.Text;
        if (CheckBox1.Checked == true)
        {
            slider_durumu = "True";
        }
        else
        {
            slider_durumu = "False";
        }
        slider_kategori = DropDownList_sliderkategori.SelectedValue;
        slider_etiket = txt_etiket.Text;
        slider_kisaaciklama = txt_kisaaciklama.Text;
        slider_anahtarkelime = txt_anahtarkelime.Text;
        slider_link = txt_sliderlink.Text;
        slider_aciklama = CKEditor_aciklama.Text;
        slider_tarih = DateTime.Now.ToString();

        SqlConnection con = code.baglan();
        try
        {
            slider_resim = code.ResimEkle(resimekle, txt_slideradi, Server.MapPath("slider_resim/"), "_sliderresim_", panel_uyari, panel_basarili, label_hata);


            string kategori_ekle = "INSERT INTO slider(slider_adi, slider_etiket, slider_kisaaciklama, slider_anahtar, slider_url, slider_kategori, slider_durum, slider_aciklama, slider_tarih, slider_resim ) VALUES (@slider_adi, @slider_etiket, @slider_kisaaciklama, @slider_anahtar, @slider_url, @slider_kategori, @slider_durum, @slider_aciklama, @slider_tarih, @slider_resim)";
            SqlCommand command = new SqlCommand(kategori_ekle, con);
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
            if (command.ExecuteNonQuery() > 0)
            {
                command.Dispose();
                con.Close();
                panel_basarili.CssClass = "aktif";
                panel_basarili.Visible = true;
                hd_basarili.Value = slider_adi + " isimli sliderınız " + DropDownList_sliderkategori.SelectedItem.Text + " slider kategorisine başarıyla eklenmiştir. Slider listeleme sayfasına yönlendiriliyorsunuz. Lütfen Bekleyiniz...";

            }
            else
            { }
        }
        catch (Exception ex)
        {
            panel_hatali.CssClass = "aktif";
            panel_hatali.Visible = true;
            hd_uyari.Value = "Slider ekleme sırasında bir hata oluştu... Hata: " + ex ;
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