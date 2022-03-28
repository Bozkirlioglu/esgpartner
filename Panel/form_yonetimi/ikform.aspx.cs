using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class formyonetimi_ikform : System.Web.UI.Page
{
    General_Code code = new General_Code();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //KategoriTurGetir();
            Listele();
        }
        panel_basarili.Visible = false;
    }

    void Listele()
    {
        string sql = "SELECT ik_id, ik_adsoyad, ik_mail, ik_tel, ik_file, ik_kayittarih, ik_okundu FROM ikform ORDER BY ik_id DESC";
        
        SqlCommand cmd = new SqlCommand(sql);
        DataTable dt = code.GetData(cmd);
        rpliste.DataSource = dt;
        rpliste.DataBind();

        cmd.Dispose();
    }

    protected void rpliste_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string id = Convert.ToString(e.CommandArgument);
            SqlConnection con = code.baglan();
            
            string strQuery = "select * from ikform where ik_id = @id";
            SqlCommand cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@id", id.Trim());
            DataRow dr = code.GetDataRow(cmd);
            if (dr != null)
            {
                string dosyaadi = dr["ik_file"].ToString();
                if ((!String.IsNullOrEmpty(dosyaadi)) || (dosyaadi != "default_image.png"))
                {
                    File.Delete(Server.MapPath("~/Panel/form_yonetimi/dosya/" + dosyaadi));
                }
            }
            string silsql = "DELETE FROM ikform WHERE ik_id=@ik_id";
            cmd = new SqlCommand(silsql, con);
            cmd.Parameters.AddWithValue("ik_id", id.Trim());
            cmd.ExecuteNonQuery();
            panel_basarili.CssClass = "aktif";
            panel_basarili.Visible = true;
            hd_basarili.Value = "İnsan Kaynakları formu başarıyla silinmiştir. Lütfen Bekleyiniz. İnsan Kaynakları formu listesi güncelleniyor...";
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }
        if (e.CommandName == "Goruntule")
        {
            string id = Convert.ToString(e.CommandArgument);

            SqlConnection con = code.baglan();
            string guncelle = "UPDATE ikform SET ik_okundu=@ik_okundu WHERE ik_id = @ik_id";
            SqlCommand cmdguncelle = new SqlCommand(guncelle, con);
            cmdguncelle.Parameters.AddWithValue("@ik_okundu", "True");
            cmdguncelle.Parameters.AddWithValue("@ik_id", id.Trim());
            cmdguncelle.ExecuteNonQuery();
            cmdguncelle.Dispose();
            
            string strQuery = "select * from ikform where ik_id = @id";
            SqlCommand cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@id", id.Trim());
            DataRow dr = code.GetDataRow(cmd);
            if (dr != null)
            {
                Response.Redirect("~/Panel/form_yonetimi/dosya/" + dr["ik_file"].ToString());
            }
            con.Close();
        }
    }
}