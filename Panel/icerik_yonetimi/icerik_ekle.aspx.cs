using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ImageResizer;
using System.IO;
using System.Drawing;

public partial class icerik_yonetimi_icerik_ekle : System.Web.UI.Page
{
    General_Code code = new General_Code();
    string icerik_adi;
    string icerik_durumu;
    string icerik_kategori;
    string icerik_etiket;
    string icerik_kisaaciklama;
    string icerik_anahtarkelime;
    string icerik_link;
    string icerik_aciklama;
    string icerik_resim;
    protected void Page_Load(object sender, EventArgs e)
    {
        panel_basarili.Visible = false;
        panel_hatali.Visible = false;
        panel_uyari.Visible = false;

        KategoriGetir();
    }
    protected void Kaydet_Click(object sender, EventArgs e)
    {
        icerik_adi = txt_icerikadi.Text;
        if (CheckBox1.Checked == true)
        {
            icerik_durumu = "True";
        }
        else
        {
            icerik_durumu = "False";
        }
        icerik_kategori = DropDownList_kategori.SelectedValue;
        icerik_etiket = txt_etiket.Text;
        icerik_kisaaciklama = txt_kisaaciklama.Text;
        icerik_anahtarkelime = txt_anahtarkelime.Text;
        icerik_link = txt_link.Text;
        icerik_aciklama = CKEditor_aciklama.Text;

        SqlConnection con = code.baglan();
        try
        {
            // icerik_resim = code.ResimEkle(resimekle, txt_icerikadi, Server.MapPath("icerik_resim/"), "_icerikresim_", panel_uyari, panel_basarili, label_hata);

            //Resim Ekleme
            int dosyaboyutu = 0;
            string resimadi = "default_image.png";
            string dosyauzantisi = "";
            string thumbadi = "";

            if (resimekle.HasFile)
            {
                dosyaboyutu = resimekle.PostedFile.ContentLength;
                if (dosyaboyutu <= 1000000)
                {
                    dosyauzantisi = Path.GetExtension(resimekle.PostedFile.FileName);
                    thumbadi = General_Code.ToUrl(icerik_adi) + "_" + DateTime.Now.Day + General_Code.GetUniqueKey(5);
                    resimadi = thumbadi + dosyauzantisi;
                    
                    string dosyayolu = Server.MapPath("icerik_resim/");
                    resimekle.SaveAs(dosyayolu + resimadi);
                }
                else
                {
                    panel_hatali.CssClass = "aktif";
                    panel_hatali.Visible = true;
                    hd_uyari.Value = "Dosya Boyutu Yüksek ! Max 1 mb";
                }

                string sqlolcu = "SELECT thumb_id, thumb1_width, thumb1_height, thumb2_width, thumb2_height FROM thumb WHERE (thumb_id = 1)";
                SqlCommand cmd = new SqlCommand(sqlolcu);
                DataRow drolcu = code.GetDataRow(cmd);
                if (drolcu != null)
                {
                    string thumb1width = drolcu["thumb1_width"].ToString();
                    string thumb1height = drolcu["thumb1_height"].ToString();

                    string thumb2width = drolcu["thumb2_width"].ToString();
                    string thumb2height = drolcu["thumb2_height"].ToString();

                    //Thumb resmi oluşturuyoruz
                    //string dosyauzantisi = Path.GetExtension(resimekle.PostedFile.FileName);
                    //string resimadi = General_Code.ToUrl(icerik_adi) + "_" + DateTime.Now.Day + "_" + General_Code.GetUniqueKey(5) + dosyauzantisi;

                    //string uploadfolder = Server.MapPath("icerik_resim/thumb1/");
                    //resimekle.SaveAs(uploadfolder + resimadi);

                    if (thumb1width != "0" && thumb1height != "0")
                    {
                        ResizeSettings resizecropsetting1 = new ResizeSettings("width=" + thumb1width + "&" + "height=" + thumb1height + "&format=" + dosyauzantisi + "&crop=auto");
                        string fileName = Path.Combine("~/Panel/icerik_yonetimi/icerik_resim/thumb1/" + thumbadi);
                        fileName = ImageBuilder.Current.Build("~/Panel/icerik_yonetimi/icerik_resim/" + resimadi, fileName, resizecropsetting1, false, true);
                    }
                    if (thumb2width != "0" && thumb2height != "0")
                    {
                        ResizeSettings resizecropsetting1 = new ResizeSettings("width=" + thumb2width + "&" + "height=" + thumb2height + "&format=" + dosyauzantisi + "&crop=auto");
                        string fileName = Path.Combine("~/Panel/icerik_yonetimi/icerik_resim/thumb2/" + thumbadi);
                        fileName = ImageBuilder.Current.Build("~/Panel/icerik_yonetimi/icerik_resim/" + resimadi, fileName, resizecropsetting1, false, true);
                    }
                    //File.Delete(Server.MapPath("icerik_resim/thumb1/" + resimadi));

                    //Thumb resmi oluşturma sonu *************
                }
                //Resim Ekleme Bitiş
            }
            icerik_resim = resimadi;
            string icerik_ekle = "INSERT INTO contents(icerik_adi, icerik_kategori, icerik_etiket ,icerik_kisaaciklama, icerik_anahtar, icerik_url, icerik_durum, icerik_aciklama, icerik_resim, icerik_tarih, icerik_thumb1, icerik_thumb2 ) VALUES (@icerik_adi, @icerik_kategori, @icerik_etiket, @icerik_kisaaciklama ,@icerik_anahtar, @icerik_url, @icerik_durum, @icerik_aciklama, @icerik_resim, @icerik_tarih, @icerik_thumb1, @icerik_thumb2)";
            SqlCommand command = new SqlCommand(icerik_ekle, con);
            command.Parameters.AddWithValue("@icerik_adi", icerik_adi);
            command.Parameters.AddWithValue("@icerik_kategori", icerik_kategori);
            command.Parameters.AddWithValue("@icerik_etiket", icerik_etiket);
            command.Parameters.AddWithValue("@icerik_kisaaciklama", icerik_kisaaciklama);
            command.Parameters.AddWithValue("@icerik_anahtar", icerik_anahtarkelime);
            command.Parameters.AddWithValue("@icerik_url", icerik_link);
            command.Parameters.AddWithValue("@icerik_aciklama", icerik_aciklama);
            command.Parameters.AddWithValue("@icerik_durum", icerik_durumu.ToString());
            command.Parameters.AddWithValue("@icerik_resim", icerik_resim);
            command.Parameters.AddWithValue("@icerik_tarih", DateTime.Now);
            command.Parameters.AddWithValue("@icerik_thumb1", icerik_resim);
            command.Parameters.AddWithValue("@icerik_thumb2", icerik_resim);

            if (command.ExecuteNonQuery() > 0)
            {
                panel_basarili.CssClass = "aktif";
                panel_basarili.Visible = true;
                panel_basarili.Visible = true;
                hd_basarili.Value = icerik_adi + " isimli içeriğiniz " + DropDownList_kategori.SelectedItem.Text + " " + "içerik kategorisine başarıyla eklenmiştir.. Lütfen Bekleyiniz. İçerik Listeleme sayfasına yönlendiriliyorsunuz...";

            }
            else
            { }
            command.Dispose();
            con.Close();
        }
        catch (Exception ex)
        {
            panel_hatali.CssClass = "aktif";
            panel_hatali.Visible = true;
            hd_uyari.Value = "İçerik ekleme sırasında bir hata oluştu... Hata: " + ex;
        }
    }

    protected void KategoriGetir()
    {
        SqlConnection con = code.baglan();
        SqlCommand sorgu = new SqlCommand("SELECT kategori_id, kategori_adi FROM category WHERE (kategori_turu = 1)", con);
        SqlDataAdapter da = new SqlDataAdapter(sorgu);
        DataTable dt = new DataTable();

        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DropDownList_kategori.Items.Add(new ListItem(dt.Rows[i]["kategori_adi"].ToString(), dt.Rows[i]["kategori_id"].ToString()));
            }
        }
        con.Close();
        da.Dispose();
    }

}