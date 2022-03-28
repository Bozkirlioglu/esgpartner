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

public partial class galeriyonetimi_resimduzenle : System.Web.UI.Page
{
    General_Code code = new General_Code();
    string id, galeriresim;
    protected void Page_Load(object sender, EventArgs e)
    {

        panel_basarili.Visible = false;
        panel_hatali.Visible = false;
        panel_uyari.Visible = false;
        txt_resimadi.Focus();
        if (!IsPostBack)
        {
            VeriGetir();
        }
    }
    protected void Guncelle_Click(object sender, EventArgs e)
    {

        id = Request.QueryString["resimid"];

        string resim_adi = txt_resimadi.Text;
        string resim_durumu = "";
        if (CheckBox1.Checked == true)
        {
            resim_durumu = "True";
        }
        else
        {
            resim_durumu = "False";
        }
        string resim_kisaaciklama = txt_kisaaciklama.Text;
        string resim_aciklama = CKEditor_aciklama.Text;

        SqlConnection con = code.baglan();
        try
        {

            if (resimekle.HasFile)
            {
                //galeriresim = code.ResimEkle(resimekle, txt_resimadi, Server.MapPath("galeri_resim/"), "_img_", panel_uyari, panel_basarili, label_hata);
                int dosyaboyutu = 0;
                string dosyauzantisi = "";
                string thumbadi = "";


                dosyaboyutu = resimekle.PostedFile.ContentLength;
                if (dosyaboyutu <= 1000000)
                {
                    string strQuery = "select galeri_resim from photo where resim_id = @id";
                    SqlCommand cmdresim = new SqlCommand(strQuery);
                    cmdresim.Parameters.AddWithValue("@id", id.Trim());
                    DataRow dr = code.GetDataRow(cmdresim);
                    if (dr != null)
                    {
                        galeriresim = dr["galeri_resim"].ToString();
                        File.Delete(Server.MapPath("galeri_resim/thumb2/" + galeriresim));
                        File.Delete(Server.MapPath("galeri_resim/thumb1/" + galeriresim));
                        File.Delete(Server.MapPath("galeri_resim/" + galeriresim));
                    }
                    cmdresim.Dispose();

                    dosyauzantisi = Path.GetExtension(resimekle.PostedFile.FileName);
                    thumbadi = General_Code.ToUrl(resim_adi) + "_" + DateTime.Now.Day + General_Code.GetUniqueKey(5);
                    galeriresim = thumbadi + dosyauzantisi;

                    string dosyayolu = Server.MapPath("galeri_resim/");
                    resimekle.SaveAs(dosyayolu + galeriresim);
                    
                }
                else
                {
                    panel_hatali.CssClass = "aktif";
                    panel_hatali.Visible = true;
                    hd_uyari.Value = "Dosya Boyutu Yüksek ! Max 1 mb";
                }

                string sqlolcu = "SELECT thumb_id, thumb1_width, thumb1_height, thumb2_width, thumb2_height FROM thumb WHERE (thumb_id = 2)";
                SqlCommand cmd = new SqlCommand(sqlolcu);
                DataRow drolcu = code.GetDataRow(cmd);
                if (drolcu != null)
                {
                    string thumb1width = drolcu["thumb1_width"].ToString();
                    string thumb1height = drolcu["thumb1_height"].ToString();

                    string thumb2width = drolcu["thumb2_width"].ToString();
                    string thumb2height = drolcu["thumb2_height"].ToString();

                    //Thumb resmi oluşturuyoruz
                    if (thumb1width != "0" && thumb1height != "0")
                    {
                        ResizeSettings resizecropsetting1 = new ResizeSettings("width=" + thumb1width + "&" + "height=" + thumb1height + "&format=" + dosyauzantisi + "&crop=auto");
                        string fileName = Path.Combine("~/Panel/galeri_yonetimi/galeri_resim/thumb1/" + thumbadi);
                        fileName = ImageBuilder.Current.Build("~/Panel/galeri_yonetimi/galeri_resim/" + galeriresim, fileName, resizecropsetting1, false, true);
                    }
                    if (thumb2width != "0" && thumb2height != "0")
                    {
                        ResizeSettings resizecropsetting1 = new ResizeSettings("width=" + thumb2width + "&" + "height=" + thumb2height + "&format=" + dosyauzantisi + "&crop=auto");
                        string fileName = Path.Combine("~/Panel/galeri_yonetimi/galeri_resim/thumb2/" + thumbadi);
                        fileName = ImageBuilder.Current.Build("~/Panel/galeri_yonetimi/galeri_resim/" + galeriresim, fileName, resizecropsetting1, false, true);
                    }
                    //Thumb resmi oluşturma sonu *************
                }

            }
            else
            {
                string strQuery = "select galeri_resim from photo where resim_id = @id";
                SqlCommand cmd = new SqlCommand(strQuery);
                cmd.Parameters.AddWithValue("@id", id.Trim());
                DataRow dr = code.GetDataRow(cmd);
                if (dr != null)
                {
                    galeriresim = dr["galeri_resim"].ToString();
                }
            }

            string guncelle = "UPDATE photo SET resim_adi=@resim_adi, resim_kisaaciklama=@resim_kisaaciklama, galeri_resim = @galeri_resim, resim_thumb1 = @resim_thumb1, resim_thumb2 = @resim_thumb2, resim_durum = @resim_durum, resim_aciklama=@resim_aciklama, resim_tarih = @resim_tarih WHERE resim_id = @resim_id";
            SqlCommand command = new SqlCommand(guncelle, con);
            command.Parameters.AddWithValue("@resim_adi", resim_adi);
            command.Parameters.AddWithValue("@resim_kisaaciklama", resim_kisaaciklama);
            command.Parameters.AddWithValue("@resim_aciklama", resim_aciklama);
            command.Parameters.AddWithValue("@resim_durum", resim_durumu.ToString());
            command.Parameters.AddWithValue("@galeri_resim", galeriresim);
            command.Parameters.AddWithValue("@resim_thumb1", galeriresim);
            command.Parameters.AddWithValue("@resim_thumb2", galeriresim);
            command.Parameters.AddWithValue("@resim_tarih", DateTime.Now);
            command.Parameters.AddWithValue("@resim_id", id);
            command.ExecuteNonQuery();
            command.Dispose();
            con.Close();
            panel_basarili.CssClass = "aktif";
            panel_basarili.Visible = true;
            hd_basarili.Value = resim_adi + " isimli resminiz başarıyla güncellenmiştir. Resim listeleme sayfasına yönlendiriliyorsunuz. Lütfen Bekleyiniz...";

        }
        catch (Exception ex)
        {
            panel_hatali.CssClass = "aktif";
            panel_hatali.Visible = true;
            hd_uyari.Value = "Resim düzenleme sırasında bir hata oluştu... Hata: " + ex;
        }

    }
    protected void VeriGetir()
    {
        try
        {
            id = Request.QueryString["resimid"];

            string strQuery = "select * from photo where resim_id = @id";
            SqlCommand cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@id", id.Trim());
            DataRow dr = code.GetDataRow(cmd);
            if (dr != null)
            {
                txt_resimadi.Text = dr["resim_adi"].ToString();
                string durum = dr["resim_durum"].ToString();
                if (durum == "True")
                {
                    CheckBox1.Checked = true;
                }
                txt_kisaaciklama.Text = dr["resim_kisaaciklama"].ToString();
                galeriresim = dr["galeri_resim"].ToString();
                galeri_image.ImageUrl = "galeri_resim/" + dr["galeri_resim"].ToString();
                CKEditor_aciklama.Text = dr["resim_aciklama"].ToString();
            }
        }
        catch (Exception ex)
        {
            panel_hatali.CssClass = "aktif";
            panel_hatali.Visible = true;
            hd_uyari.Value = "Bir hata oluştu. Resim listeme sayfasına yönlendiriliyorsunuz. Lütfen bekleyiniz... Hata: " + ex;
        }
    }
}