using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Panel_kategori_yonetimi_altkategori_ekle : System.Web.UI.Page
{
    General_Code code = new General_Code();
    string kategori_adi;
    string kategori_durumu;
    string kategori_turu;
    string kategori_etiket;
    string kategori_kisaaciklama;
    string kategori_anahtarkelime;
    string kategori_link;
    string kategori_aciklama;
    string kategori_resim;
    string kategori_ustid;
    protected void Page_Load(object sender, EventArgs e)
    {
        panel_basarili.Visible = false;
        panel_hatali.Visible = false;
        panel_uyari.Visible = false;
        if (!IsPostBack)
        {
            KategorileriGetir();
            KategoritipGetir();
        }

        // İliskiliKategoriGetir();
    }
    protected void Kaydet_Click(object sender, EventArgs e)
    {
        kategori_adi = txt_kategoriadi.Text;
        if (CheckBox1.Checked == true)
        {
            kategori_durumu = "True";
        }
        else
        {
            kategori_durumu = "False";
        }
        kategori_turu = DropDownList_kategoritype.SelectedValue;
        kategori_etiket = txt_etiket.Text;
        kategori_kisaaciklama = txt_kisaaciklama.Text;
        kategori_anahtarkelime = txt_anahtarkelime.Text;
        kategori_link = txt_kategorilink.Text;
        kategori_aciklama = CKEditor_aciklama.Text;
        kategori_ustid = dd_kategori.SelectedValue;

        SqlConnection con = code.baglan();
        try
        {
            kategori_resim = code.ResimEkle(resimekle, txt_kategoriadi, Server.MapPath("kategori_resim/"), "_kategoriresim_", panel_uyari, panel_basarili, label_hata);

            string kategori_ekle = "INSERT INTO category(kategori_adi, kategori_etiket, kategori_kisaaciklama, kategori_anahtar, kategori_url, kategori_turu, kategori_durum, kategori_aciklama,kategori_resim, kategori_ustid) VALUES (@kategori_adi, @kategori_etiket, @kategori_kisaaciklama, @kategori_anahtar, @kategori_url, @kategori_turu, @kategori_durum, @kategori_aciklama, @kategori_resim, @kategori_ustid)";
            SqlCommand command = new SqlCommand(kategori_ekle, con);
            command.Parameters.AddWithValue("@kategori_adi", kategori_adi);
            command.Parameters.AddWithValue("@kategori_etiket", kategori_etiket);
            command.Parameters.AddWithValue("@kategori_kisaaciklama", kategori_kisaaciklama);
            command.Parameters.AddWithValue("@kategori_anahtar", kategori_anahtarkelime);
            command.Parameters.AddWithValue("@kategori_url", kategori_link);
            command.Parameters.AddWithValue("@kategori_aciklama", kategori_aciklama);
            command.Parameters.AddWithValue("@kategori_durum", kategori_durumu.ToString());
            command.Parameters.AddWithValue("@kategori_turu", kategori_turu);
            command.Parameters.AddWithValue("@kategori_resim", kategori_resim);
            command.Parameters.AddWithValue("@kategori_ustid", kategori_ustid);
            if (command.ExecuteNonQuery() > 0)
            {
                panel_basarili.CssClass = "aktif";
                panel_basarili.Visible = true;
                panel_basarili.Visible = true;
                hd_basarili.Value = DropDownList_kategoritype.SelectedItem.Text + " " + "kategorisi başarıyla eklenmiştir...";

            }
            command.Dispose();
            con.Close();
        }
        catch (Exception ex)
        {
            panel_hatali.CssClass = "aktif";
            panel_hatali.Visible = true;
            hd_uyari.Value = "Kategori ekleme sırasında bir hata oluştu... Hata: " + ex;
        }
    }

    protected void KategoritipGetir()
    {
        SqlConnection con = code.baglan();
        SqlCommand sorgu = new SqlCommand("SELECT categorytype_id, category_type FROM category_type", con);
        SqlDataAdapter da = new SqlDataAdapter(sorgu);
        DataTable dt = new DataTable();

        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DropDownList_kategoritype.Items.Add(new ListItem(dt.Rows[i]["category_type"].ToString(), dt.Rows[i]["categorytype_id"].ToString()));
            }
        }
        con.Close();
        da.Dispose();
    }

    protected void DropDownList_kategoritype_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = code.baglan();
        SqlCommand sorgu = new SqlCommand("SELECT kategori_id, kategori_adi FROM category WHERE (kategori_turu = '" + DropDownList_kategoritype.SelectedValue + "')", con);
        SqlDataAdapter da = new SqlDataAdapter(sorgu);
        DataTable dt = new DataTable();

        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dd_kategori.Items.Add(new ListItem(dt.Rows[i]["kategori_adi"].ToString(), dt.Rows[i]["kategori_id"].ToString()));
            }
        }
        con.Close();
        da.Dispose();
    }

    private void KategorileriGetir()
    {
        SqlConnection con = code.baglan();
        SqlCommand sorgu = new SqlCommand("SELECT kategori_id, kategori_adi FROM category WHERE (kategori_turu = 1) AND (kategori_ustid = 0)", con);
        SqlDataAdapter da = new SqlDataAdapter(sorgu);
        DataTable dt = new DataTable();

        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dd_kategori.Items.Add(new ListItem(dt.Rows[i]["kategori_adi"].ToString(), dt.Rows[i]["kategori_id"].ToString()));
                AltKategori(dt.Rows[i]["kategori_id"].ToString(), "");
            }
        }
        con.Close();
        da.Dispose();
    }
    private void AltKategori(string katid, string girinti)
    {
        girinti = girinti + " --";
        SqlConnection con = code.baglan();
        SqlCommand sorgu = new SqlCommand("SELECT kategori_id, kategori_adi FROM category WHERE (kategori_turu = 1) AND (kategori_ustid = '" + katid + "')", con);
        SqlDataAdapter da = new SqlDataAdapter(sorgu);
        DataTable dt = new DataTable();

        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dd_kategori.Items.Add(new ListItem(girinti + " > " + dt.Rows[i]["kategori_adi"].ToString(), dt.Rows[i]["kategori_id"].ToString()));
                AltKategori(dt.Rows[i]["kategori_id"].ToString(), girinti);
            }
        }
        con.Close();
        da.Dispose();
    }
}