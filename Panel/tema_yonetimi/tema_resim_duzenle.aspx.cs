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

public partial class tema_yonetimi_tema_resim_duzenle : System.Web.UI.Page
{
    General_Code code = new General_Code();
    string id, icerik_resim;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        panel_basarili.Visible = false;
        panel_hatali.Visible = false;
        panel_uyari.Visible = false;

        if (!IsPostBack)
        {
            TemalariGetir();
            IcerikleriGetir();
            IcerikDoldur();
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

    protected void IcerikDoldur()
    {

        try
        {
            id = Request.QueryString["temaicerikid"];

            string strQuery = "select * from theme_content where themerecord_id = @id";
            SqlCommand cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@id", id.Trim());
            DataRow dr = code.GetDataRow(cmd);
            if (dr != null)
            {
                DropDownList_tema.SelectedValue = dr["theme_id"].ToString();
                DropDownList_Icerik.SelectedValue = dr["theme_icerik"].ToString();
                icerik_resim = dr["theme_image"].ToString();
                theme_image.ImageUrl = "tema_resim/thumb1/" + dr["theme_image"].ToString();
            }
        }
        catch (Exception ex)
        {
            panel_hatali.CssClass = "aktif";
            panel_hatali.Visible = true;
            hd_uyari.Value = "Bir hata oluştu. Tema listeme sayfasına yönlendiriliyorsunuz. Lütfen bekleyiniz... Hata: " + ex;
        }
    }

    protected void buton_guncelle_Click(object sender, EventArgs e)
    {
        id = Request.QueryString["temaicerikid"];

        string tema_id = DropDownList_tema.SelectedValue;
        string icerik_id = DropDownList_Icerik.SelectedValue;
        string icerik_adi = DropDownList_Icerik.SelectedItem.Text;

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
                    string strQuery = "select theme_image from theme_content where themerecord_id = @id";
                    SqlCommand cmdresim = new SqlCommand(strQuery);
                    cmdresim.Parameters.AddWithValue("@id", id.Trim());
                    DataRow dr = code.GetDataRow(cmdresim);
                    if (dr != null)
                    {
                        icerik_resim = dr["theme_image"].ToString();
                        File.Delete(Server.MapPath("tema_resim/thumb2/" + icerik_resim));
                        File.Delete(Server.MapPath("tema_resim/thumb1/" + icerik_resim));
                        File.Delete(Server.MapPath("tema_resim/" + icerik_resim));
                    }
                    cmdresim.Dispose();

                    dosyauzantisi = Path.GetExtension(resimekle.PostedFile.FileName);
                    thumbadi = General_Code.ToUrl(icerik_adi) + "_" + DateTime.Now.Day + General_Code.GetUniqueKey(5);
                    resimadi = thumbadi + dosyauzantisi;

                    string dosyayolu = Server.MapPath("tema_resim/");
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
            else
            {
                string strQuery = "select theme_image from theme_content where themerecord_id = @id";
                SqlCommand cmd = new SqlCommand(strQuery);
                cmd.Parameters.AddWithValue("@id", id.Trim());
                DataRow dr = code.GetDataRow(cmd);
                if (dr != null)
                {
                    icerik_resim = dr["theme_image"].ToString();
                }
                cmd.Dispose();
            }

            string guncelle = "UPDATE theme_content SET theme_image=@theme_image, theme_icerik=@theme_icerik, theme_id=@theme_id, image_thumb1=@image_thumb1, image_thumb2 = @image_thumb2 WHERE themerecord_id = @record_id";
            SqlCommand command = new SqlCommand(guncelle, con);
            command.Parameters.AddWithValue("@theme_image", icerik_resim);
            command.Parameters.AddWithValue("@theme_icerik", icerik_id);
            command.Parameters.AddWithValue("@theme_id", tema_id);
            command.Parameters.AddWithValue("@image_thumb1", icerik_resim);
            command.Parameters.AddWithValue("@image_thumb2", icerik_resim);
            command.Parameters.AddWithValue("@record_id", id.Trim());
            command.ExecuteNonQuery();
            command.Dispose();
            con.Close();
            panel_basarili.CssClass = "aktif";
            panel_basarili.Visible = true;
            hd_basarili.Value = icerik_adi + " isimli tema resim değişikliğiniz başarıyla güncellenmiştir. Tema listeleme sayfasına yönlendiriliyorsunuz. Lütfen Bekleyiniz...";

        }
        catch (Exception ex)
        {
            panel_hatali.CssClass = "aktif";
            panel_hatali.Visible = true;
            hd_uyari.Value = "Tema resim düzenleme sırasında bir hata oluştu... Hata: " + ex;
        }
    }
}