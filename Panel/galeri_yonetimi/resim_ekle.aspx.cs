using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ImageResizer;

public partial class galeri_yonetimi_resim_ekle : System.Web.UI.Page
{
    General_Code code = new General_Code();
    int eklenen;
    protected void Page_Load(object sender, EventArgs e)
    {
        panel_basarili.Visible = false;
        panel_hatali.Visible = false;
        panel_uyari.Visible = false;
        KategoriGetir();
    }

    protected void UploadMultipleFiles(object sender, EventArgs e)
    {
        eklenen = 0;
        int cikan = 0;
        if (FileUpload1.HasFile)
        {
            foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
            {
                string uzanti = Path.GetExtension(postedFile.FileName);
                if (uzanti == ".jpg" || uzanti == ".png")
                {
                    eklenen = eklenen + 1;
                    string thumbadi = DateTime.Now.Day.ToString() + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Hour + DateTime.Now.Second + DateTime.Now.Minute + DateTime.Now.Millisecond;
                    string resimadi = thumbadi + uzanti;

                    SqlConnection con = code.baglan();
                    try
                    {
                        string resim_adi = "";
                        string resim_kisaaciklama = "";
                        string resim_aciklama = "";
                        int resim_kategori = Convert.ToInt32(DropDownList_kategori.SelectedValue);
                        bool resim_durum = true;
                        string resim_ekle = "INSERT INTO photo(resim_adi, resim_kisaaciklama, resim_aciklama, galeri_resim, resim_thumb1, resim_thumb2, resim_durum, resim_tarih, resim_kategori) VALUES (@resim_adi, @resim_kisaaciklama, @resim_aciklama, @galeri_resim, @resim_thumb1, @resim_thumb2, @resim_durum, @resim_tarih, @resim_kategori)";
                        SqlCommand command = new SqlCommand(resim_ekle, con);
                        command.Parameters.AddWithValue("@resim_adi", resim_adi);
                        command.Parameters.AddWithValue("@resim_kisaaciklama", resim_kisaaciklama);
                        command.Parameters.AddWithValue("@resim_aciklama", resim_aciklama);
                        command.Parameters.AddWithValue("@galeri_resim", resimadi);
                        command.Parameters.AddWithValue("@resim_thumb1", resimadi);
                        command.Parameters.AddWithValue("@resim_thumb2", resimadi);
                        command.Parameters.AddWithValue("@resim_durum", resim_durum);
                        command.Parameters.AddWithValue("@resim_tarih", DateTime.Now);
                        command.Parameters.AddWithValue("@resim_kategori", resim_kategori);
                        if (command.ExecuteNonQuery() > 0)
                        {
                            postedFile.SaveAs(Server.MapPath("galeri_resim/") + resimadi);

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
                                    ResizeSettings resizecropsetting1 = new ResizeSettings("width=" + thumb1width + "&" + "height=" + thumb1height + "&format=" + uzanti + "&crop=auto");
                                    string fileName = Path.Combine("~/Panel/galeri_yonetimi/galeri_resim/thumb1/" + thumbadi);
                                    fileName = ImageBuilder.Current.Build("~/Panel/galeri_yonetimi/galeri_resim/" + resimadi, fileName, resizecropsetting1, false, true);
                                }
                                if (thumb2width != "0" && thumb2height != "0")
                                {
                                    ResizeSettings resizecropsetting1 = new ResizeSettings("width=" + thumb2width + "&" + "height=" + thumb2height + "&format=" + uzanti + "&crop=auto");
                                    string fileName = Path.Combine("~/Panel/galeri_yonetimi/galeri_resim/thumb2/" + thumbadi);
                                    fileName = ImageBuilder.Current.Build("~/Panel/galeri_yonetimi/galeri_resim/" + resimadi, fileName, resizecropsetting1, false, true);
                                }
                                //Thumb resmi oluşturma sonu *************
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        panel_hatali.CssClass = "aktif";
                        panel_hatali.Visible = true;
                        hd_uyari.Value = "resim ekleme sırasında bir hata oluştu... Hata: " + ex;
                    }
                    con.Close();

                    /*Son Kaydedilen Resimleri Getiriyoruz*/
                    Image img = new Image();
                    img.ID = "img" + eklenen;
                    img.ImageUrl = "galeri_resim/thumb1/" + resimadi;

                    panel_txt.Controls.Add(new LiteralControl("<div class = 'eklenen'>"));
                    panel_txt.Controls.Add(new LiteralControl("<div class = 'resim-alan'>"));
                    panel_txt.Controls.Add(img);
                    panel_txt.Controls.Add(new LiteralControl("</div>"));

                    TextBox txt = new TextBox();
                    txt.ID = "txt" + eklenen;
                    txt.Attributes.Add("placeholder", "Resim Adı");
                    panel_txt.Controls.Add(new LiteralControl("<div class = 'txt-alan'>"));
                    panel_txt.Controls.Add(txt);
                    panel_txt.Controls.Add(new LiteralControl("</div>"));

                    TextBox txtkisaaciklama = new TextBox();
                    txtkisaaciklama.ID = "kisaaciklama" + eklenen;
                    txtkisaaciklama.Attributes.Add("placeholder", "Resim Kısa Açıklama");
                    panel_txt.Controls.Add(new LiteralControl("<div class = 'txt-alan'>"));
                    panel_txt.Controls.Add(txtkisaaciklama);
                    panel_txt.Controls.Add(new LiteralControl("</div>"));

                    panel_txt.Controls.Add(new LiteralControl("</div>")); //En dış <div class='eklenen'> kapanışı

                }
                else
                {
                    cikan = cikan + 1;
                }
            }
            //panel_basarili.CssClass = "aktif";
            //panel_basarili.Visible = true;
            //panel_basarili.Visible = true;
            //hd_basarili.Value = string.Format("{0} resim başarıyla eklendi...", eklenen);

        }
    }

    protected void KategoriGetir()
    {
        SqlConnection con = code.baglan();
        SqlCommand sorgu = new SqlCommand("SELECT kategori_id, kategori_adi FROM category WHERE (kategori_turu = 4)", con);
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

    protected void Kaydet_Click(object sender, EventArgs e)
    {
        List<string> kisaaciklama = new List<string>();
        List<string> resimadi = new List<string>();
        List<string> resimdurum = new List<string>();

        foreach (string key in Request.Form.AllKeys.Where((key => key.Contains("txt"))))
        {
            eklenen = eklenen + 1;
            resimadi.Add(Request.Form[key]);
        }
        foreach (string key in Request.Form.AllKeys.Where((key => key.Contains("kisaaciklama"))))
        {
            kisaaciklama.Add(Request.Form[key]);
        }

        string sql = "select TOP(" + eklenen + ") * FROM photo ORDER BY resim_id DESC";
        SqlCommand cmd = new SqlCommand(sql);
        DataTable dt = code.GetData(cmd);

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                eklenen = eklenen - 1;
                string id = dr["resim_id"].ToString();
               // code.Command("UPDATE photo SET resim_adi='" + resimadi[eklenen].ToString() + "', resim_kisaaciklama='" + kisaaciklama[eklenen].ToString() + "' Where resim_id=" + id.Trim());
                SqlConnection con = code.baglan();
                string guncelle = "UPDATE photo SET resim_adi=@resim_adi, resim_kisaaciklama=@resim_kisaaciklama Where resim_id=@resim_id";
                SqlCommand command = new SqlCommand(guncelle, con);
                command.Parameters.AddWithValue("@resim_adi", resimadi[eklenen].ToString());
                command.Parameters.AddWithValue("@resim_kisaaciklama", kisaaciklama[eklenen].ToString());
                command.Parameters.AddWithValue("@resim_id", id.Trim());
                command.ExecuteNonQuery();
                command.Dispose();
                con.Close();

            }

            panel_basarili.CssClass = "aktif";
            panel_basarili.Visible = true;
            hd_basarili.Value = "Resimleriniz galeriye başarıyla eklendi. Resim listeleme sayfasına yönlendiriliyorsunuz. Lütfen Bekleyiniz...";
        }
    }
}