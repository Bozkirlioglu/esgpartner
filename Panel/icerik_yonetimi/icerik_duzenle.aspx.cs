using ImageResizer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class icerik_duzenle : System.Web.UI.Page
{
    General_Code code = new General_Code();
    string id, icerik_resim;
    protected void Page_Load(object sender, EventArgs e)
    {

        panel_basarili.Visible = false;
        panel_hatali.Visible = false;
        panel_uyari.Visible = false;
        txt_icerikadi.Focus();
        if (!IsPostBack)
        {
            KategoriGetir();
            VeriGetir();
        }
    }
    protected void Guncelle_Click(object sender, EventArgs e)
    {

        id = Request.QueryString["icerikid"];

        string icerik_adi = txt_icerikadi.Text;
        string icerik_durumu = "";
        if (CheckBox1.Checked == true)
        {
            icerik_durumu = "True";
        }
        else
        {
            icerik_durumu = "False";
        }
        string icerik_kategori = DropDownList_icerikkategori.SelectedValue;
        string icerik_etiket = txt_etiket.Text;
        string icerik_kisaaciklama = txt_kisaaciklama.Text;
        string icerik_anahtarkelime = txt_anahtarkelime.Text;
        string icerik_link = txt_link.Text;
        string icerik_aciklama = CKEditor_aciklama.Text;

        SqlConnection con = code.baglan();
        try
        {
            if (resimekle.HasFile)
            {
                int dosyaboyutu = 0;
                string resimadi = "";
                string dosyauzantisi = "";
                string thumbadi = "";

                dosyaboyutu = resimekle.PostedFile.ContentLength;
                if (dosyaboyutu <= 1000000)
                {
                    string strQuery = "select icerik_resim from contents where icerik_id = @id";
                    SqlCommand cmdresim = new SqlCommand(strQuery);
                    cmdresim.Parameters.AddWithValue("@id", id.Trim());
                    DataRow dr = code.GetDataRow(cmdresim);
                    if (dr != null)
                    {
                        icerik_resim = dr["icerik_resim"].ToString();
                        File.Delete(Server.MapPath("icerik_resim/thumb2/" + icerik_resim));
                        File.Delete(Server.MapPath("icerik_resim/thumb1/" + icerik_resim));
                        File.Delete(Server.MapPath("icerik_resim/" + icerik_resim));
                    }
                    cmdresim.Dispose();

                    dosyauzantisi = Path.GetExtension(resimekle.PostedFile.FileName);
                    thumbadi = General_Code.ToUrl(icerik_adi) + "_" + DateTime.Now.Day + General_Code.GetUniqueKey(5);
                    resimadi = thumbadi + dosyauzantisi;

                    string dosyayolu = Server.MapPath("icerik_resim/");
                    resimekle.SaveAs(dosyayolu + resimadi);
                    icerik_resim = resimadi;
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
            else
            {
                string strQuery = "select icerik_resim from contents where icerik_id = @id";
                SqlCommand cmd = new SqlCommand(strQuery);
                cmd.Parameters.AddWithValue("@id", id.Trim());
                DataRow dr = code.GetDataRow(cmd);
                if (dr != null)
                {
                    icerik_resim = dr["icerik_resim"].ToString();
                }
                cmd.Dispose();
            }

            string guncelle = "UPDATE contents SET icerik_adi=@icerik_adi, icerik_etiket=@icerik_etiket, icerik_kisaaciklama=@icerik_kisaaciklama, icerik_anahtar=@icerik_anahtar, icerik_url = @icerik_url, icerik_kategori = @icerik_kategori, icerik_resim = @icerik_resim, icerik_durum = @icerik_durum, icerik_aciklama=@icerik_aciklama, icerik_tarih = @icerik_tarih, icerik_thumb1 = @icerik_thumb1, icerik_thumb2 = @icerik_thumb2 WHERE icerik_id = @icerik_id";
            SqlCommand command = new SqlCommand(guncelle, con);
            command.Parameters.AddWithValue("@icerik_adi", icerik_adi);
            command.Parameters.AddWithValue("@icerik_etiket", icerik_etiket);
            command.Parameters.AddWithValue("@icerik_kisaaciklama", icerik_kisaaciklama);
            command.Parameters.AddWithValue("@icerik_anahtar", icerik_anahtarkelime);
            command.Parameters.AddWithValue("@icerik_url", icerik_link);
            command.Parameters.AddWithValue("@icerik_aciklama", icerik_aciklama);
            command.Parameters.AddWithValue("@icerik_durum", icerik_durumu.ToString());
            command.Parameters.AddWithValue("@icerik_kategori", icerik_kategori);
            command.Parameters.AddWithValue("@icerik_resim", icerik_resim);
            command.Parameters.AddWithValue("@icerik_tarih", DateTime.Now);
            command.Parameters.AddWithValue("@icerik_thumb1", icerik_resim);
            command.Parameters.AddWithValue("@icerik_thumb2", icerik_resim);
            command.Parameters.AddWithValue("@icerik_id", id.Trim());
            command.ExecuteNonQuery();
            command.Dispose();
            con.Close();
            panel_basarili.CssClass = "aktif";
            panel_basarili.Visible = true;
            hd_basarili.Value = icerik_adi + " isimli içeriğiniz başarıyla güncellenmiştir. İçerik listeleme sayfasına yönlendiriliyorsunuz. Lütfen Bekleyiniz...";

        }
        catch (Exception ex)
        {
            panel_hatali.CssClass = "aktif";
            panel_hatali.Visible = true;
            hd_uyari.Value = "İçerik düzenleme sırasında bir hata oluştu... Hata: " + ex;
        }
        
    }
    protected void VeriGetir()
    {
        try
        {
            id = Request.QueryString["icerikid"];

            string strQuery = "select * from contents where icerik_id = @id";
            SqlCommand cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@id", id.Trim());
            DataRow dr = code.GetDataRow(cmd);
            if (dr != null)
            {
                txt_icerikadi.Text = dr["icerik_adi"].ToString();
                string durum = dr["icerik_durum"].ToString();
                if (durum == "True")
                {
                    CheckBox1.Checked = true;
                }
                DropDownList_icerikkategori.SelectedValue = dr["icerik_kategori"].ToString();
                txt_etiket.Text = dr["icerik_etiket"].ToString();
                txt_kisaaciklama.Text = dr["icerik_kisaaciklama"].ToString();
                txt_anahtarkelime.Text = dr["icerik_anahtar"].ToString();
                txt_link.Text = dr["icerik_url"].ToString();
                icerik_resim = dr["icerik_resim"].ToString();
                icerik_image.ImageUrl = "icerik_resim/thumb1/" + dr["icerik_resim"].ToString();
                CKEditor_aciklama.Text = dr["icerik_aciklama"].ToString();
            }
        }
        catch (Exception ex)
        {
            panel_hatali.CssClass = "aktif";
            panel_hatali.Visible = true;
            hd_uyari.Value = "Bir hata oluştu. İçerik listeme sayfasına yönlendiriliyorsunuz. Lütfen bekleyiniz... Hata: " + ex;
        }
    }

    protected void KategoriGetir()
    {
        SqlConnection con = code.baglan();
        SqlCommand sorgu = new SqlCommand("SELECT kategori_id, kategori_adi FROM category WHERE (kategori_turu = 1) AND (kategori_durum = 1)", con);
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
            DropDownList_icerikkategori.Items.AddRange(items.ToArray());
        }
        con.Close();
        da.Dispose();
    }
}