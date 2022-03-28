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

public partial class tema_yonetimi_tema_icerik_ekle : System.Web.UI.Page
{
    General_Code code = new General_Code();
    string tema_resim;
    string tema_id;
    string icerik_id;
    string icerik_adi;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        panel_basarili.Visible = false;
        panel_hatali.Visible = false;
        panel_uyari.Visible = false;

        TemalariGetir();
        IcerikleriGetir();
    }
    protected void Kaydet_Click(object sender, EventArgs e)
    {
        tema_id = DropDownList_tema.SelectedValue;
        icerik_id = DropDownList_Icerik.SelectedValue;
        icerik_adi = DropDownList_Icerik.SelectedItem.Text;

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

                    string dosyayolu = Server.MapPath("tema_resim/");
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
                        string fileName = Path.Combine("~/Panel/tema_yonetimi/tema_resim/thumb1/" + thumbadi);
                        fileName = ImageBuilder.Current.Build("~/Panel/tema_yonetimi/tema_resim/" + resimadi, fileName, resizecropsetting1, false, true);
                    }
                    if (thumb2width != "0" && thumb2height != "0")
                    {
                        ResizeSettings resizecropsetting1 = new ResizeSettings("width=" + thumb2width + "&" + "height=" + thumb2height + "&format=" + dosyauzantisi + "&crop=auto");
                        string fileName = Path.Combine("~/Panel/tema_yonetimi/tema_resim/thumb2/" + thumbadi);
                        fileName = ImageBuilder.Current.Build("~/Panel/tema_yonetimi/tema_resim/" + resimadi, fileName, resizecropsetting1, false, true);
                    }
                    //File.Delete(Server.MapPath("icerik_resim/thumb1/" + resimadi));
                    //Thumb resmi oluşturma sonu *************
                }
                //Resim Ekleme Bitiş
            }
            tema_resim = resimadi;
            string kayitekle = "INSERT INTO theme_content(theme_image, theme_icerik, theme_id, image_createdate, image_thumb1, image_thumb2) VALUES (@theme_image, @theme_icerik, @theme_id , @image_createdate, @image_thumb1, @image_thumb2)";
            SqlCommand command = new SqlCommand(kayitekle, con);
            command.Parameters.AddWithValue("@theme_image", tema_resim);
            command.Parameters.AddWithValue("@theme_icerik", icerik_id);
            command.Parameters.AddWithValue("@theme_id", tema_id);
            command.Parameters.AddWithValue("@image_createdate", DateTime.Now);
            command.Parameters.AddWithValue("@image_thumb1", tema_resim);
            command.Parameters.AddWithValue("@image_thumb2", tema_resim);

            if (command.ExecuteNonQuery() > 0)
            {
                panel_basarili.CssClass = "aktif";
                panel_basarili.Visible = true;
                panel_basarili.Visible = true;
                hd_basarili.Value = icerik_adi + " isimli içeriğiniz için tema içerik resmi " + DropDownList_tema.SelectedItem.Text + " temasına başarıyla eklenmiştir.. Lütfen Bekleyiniz. Tema Resim Listeleme sayfasına yönlendiriliyorsunuz...";

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
            hd_uyari.Value = "Tema resim eklemesi sırasında bir hata oluştu... Hata: " + ex;
        }
    }

    protected void TemalariGetir()
    {
        SqlConnection con = code.baglan();
        SqlCommand sorgu = new SqlCommand("SELECT theme_id, theme_name FROM theme", con);
        SqlDataAdapter da = new SqlDataAdapter(sorgu);
        DataTable dt = new DataTable();

        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            DropDownList_tema.Items.Add(new ListItem("Seçiniz", "0"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DropDownList_tema.Items.Add(new ListItem(dt.Rows[i]["theme_name"].ToString(), dt.Rows[i]["theme_id"].ToString()));
            }
        }
        con.Close();
        da.Dispose();
    }

    protected void IcerikleriGetir()
    {
        SqlConnection con = code.baglan();
        SqlCommand sorgu = new SqlCommand("SELECT icerik_id, icerik_adi FROM contents", con);
        SqlDataAdapter da = new SqlDataAdapter(sorgu);
        DataTable dt = new DataTable();

        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            DropDownList_Icerik.Items.Add(new ListItem("Seçiniz", "0"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DropDownList_Icerik.Items.Add(new ListItem(dt.Rows[i]["icerik_adi"].ToString(), dt.Rows[i]["icerik_id"].ToString()));
            }
        }
        con.Close();
        da.Dispose();
    }

}