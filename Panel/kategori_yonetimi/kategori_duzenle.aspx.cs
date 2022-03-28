using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class kategori_duzenle : System.Web.UI.Page
{
    General_Code code = new General_Code();
    string id, kategori_resim;
    protected void Page_Load(object sender, EventArgs e)
    {
        panel_basarili.Visible = false;
        panel_hatali.Visible = false;
        panel_uyari.Visible = false;
        txt_slideradi.Focus();
        if (!IsPostBack)
        {
            KategoriTurGetir();
            VeriGetir();   
        }
    }
    protected void Guncelle_Click(object sender, EventArgs e)
    {

        id = Request.QueryString["kategoriid"];

        string kategori_adi = txt_slideradi.Text;
        string kategori_durumu = "";
        if (CheckBox1.Checked == true)
        {
            kategori_durumu = "True";
        }
        else
        {
            kategori_durumu = "False";
        }
        string kategori_turu = DropDownList_sliderkategori.SelectedValue;
        string kategori_etiket = txt_etiket.Text;
        string kategori_kisaaciklama = txt_kisaaciklama.Text;
        string kategori_anahtarkelime = txt_anahtarkelime.Text;
        string kategori_link = txt_sliderlink.Text;
        string kategori_aciklama = CKEditor_aciklama.Text;

        SqlConnection con = code.baglan();
        try
        {
            if (resimekle.HasFile)
            {
                kategori_resim = code.ResimEkle(resimekle, txt_slideradi, Server.MapPath("kategori_resim/"), "_kategoriresim_", panel_uyari, panel_basarili, label_hata);
            }
            else
            {
                string strQuery = "select kategori_resim from category where kategori_id = @id";
                SqlCommand cmd = new SqlCommand(strQuery);
                cmd.Parameters.AddWithValue("@id", id.Trim());
                DataRow dr = code.GetDataRow(cmd);
                if (dr != null)
                {
                    kategori_resim = dr["kategori_resim"].ToString();
                }
            }
            string guncelle = "UPDATE category SET kategori_adi=@kategori_adi, kategori_etiket=@kategori_etiket, kategori_kisaaciklama=@kategori_kisaaciklama, kategori_anahtar=@kategori_anahtar, kategori_url = @kategori_url, kategori_turu = @kategori_turu,kategori_resim = @kategori_resim ,kategori_durum = @kategori_durum, kategori_aciklama=@kategori_aciklama, kategori_tarih = @kategori_tarih WHERE kategori_id = @kategori_id";
            SqlCommand command = new SqlCommand(guncelle, con);
            command.Parameters.AddWithValue("@kategori_adi", kategori_adi);
            command.Parameters.AddWithValue("@kategori_etiket", kategori_etiket);
            command.Parameters.AddWithValue("@kategori_kisaaciklama", kategori_kisaaciklama);
            command.Parameters.AddWithValue("@kategori_anahtar", kategori_anahtarkelime);
            command.Parameters.AddWithValue("@kategori_url", kategori_link);
            command.Parameters.AddWithValue("@kategori_aciklama", kategori_aciklama);
            command.Parameters.AddWithValue("@kategori_durum", kategori_durumu.ToString());
            command.Parameters.AddWithValue("@kategori_turu", kategori_turu);
            command.Parameters.AddWithValue("@kategori_resim", kategori_resim);
            command.Parameters.AddWithValue("@kategori_tarih", DateTime.Now);
            command.Parameters.AddWithValue("@kategori_id", id);
            command.ExecuteNonQuery();
            command.Dispose();
            con.Close();
            panel_basarili.CssClass = "aktif";
            panel_basarili.Visible = true;
            hd_basarili.Value = kategori_adi + " isimli kategoriniz başarıyla güncellenmiştir. Kategori listeleme sayfasına yönlendiriliyorsunuz. Lütfen Bekleyiniz...";
        }
        catch (Exception ex)
        {
            panel_hatali.CssClass = "aktif";
            panel_hatali.Visible = true;
            hd_uyari.Value = "Kategori düzenleme sırasında bir hata oluştu... Hata: " + ex;
        }
    }
    protected void VeriGetir()
    {
        try
        {
            id = Request.QueryString["kategoriid"];

            string strQuery = "select * from category where kategori_id = @id";
            SqlCommand cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@id", id.Trim());
            DataRow dr = code.GetDataRow(cmd);
            if (dr != null)
            {
                txt_slideradi.Text = dr["kategori_adi"].ToString();
                string durum = dr["kategori_durum"].ToString();
                if (durum == "True")
                {
                    CheckBox1.Checked = true;
                }
                DropDownList_sliderkategori.SelectedValue = dr["kategori_turu"].ToString();
                txt_etiket.Text = dr["kategori_etiket"].ToString();
                txt_kisaaciklama.Text = dr["kategori_kisaaciklama"].ToString();
                txt_anahtarkelime.Text = dr["kategori_anahtar"].ToString();
                txt_sliderlink.Text = dr["kategori_url"].ToString();
                kategori_resim = dr["kategori_resim"].ToString();
                kategori_image.ImageUrl = "kategori_resim/" + dr["kategori_resim"].ToString();
                CKEditor_aciklama.Text = dr["kategori_aciklama"].ToString();
            }
        }
        catch (Exception ex)
        {
            panel_hatali.CssClass = "aktif";
            panel_hatali.Visible = true;
            hd_uyari.Value = "Bir hata oluştu. Kategori listeme sayfasına yönlendiriliyorsunuz. Lütfen bekleyiniz... Hata: " + ex;
        }
    }
    protected void KategoriTurGetir()
    {
        SqlConnection con = code.baglan();
        SqlCommand sorgu = new SqlCommand("SELECT categorytype_id, category_type FROM category_type", con);
        SqlDataAdapter da = new SqlDataAdapter(sorgu);
        DataTable dt = new DataTable();

        da.Fill(dt);

        List<ListItem> items = new List<ListItem>();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                items.Add(new ListItem(dt.Rows[i]["category_type"].ToString(), dt.Rows[i]["categorytype_id"].ToString())); //DropDownList_sliderkategori.Items.Add(dt.Rows[i]["kategori_adi"].ToString());     
            }
            DropDownList_sliderkategori.Items.AddRange(items.ToArray());
        }
        con.Close();
        da.Dispose();
    }
}