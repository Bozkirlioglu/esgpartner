using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class formyonetimi_formlar : System.Web.UI.Page
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
        string sql = "SELECT iform_id, iform_adi, iform_tarih, iform_okundu, iform_mail FROM contactform ORDER BY iform_id DESC";
        
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
            string silsql = "DELETE FROM contactform WHERE iform_id=@iform_id";
            SqlCommand cmd = new SqlCommand(silsql, con);
            cmd.Parameters.AddWithValue("iform_id", id.Trim());
            cmd.ExecuteNonQuery();
            panel_basarili.CssClass = "aktif";
            panel_basarili.Visible = true;
            hd_basarili.Value = "İletişim formu başarıyla silinmiştir. Lütfen Bekleyiniz. İletişim formu listesi güncelleniyor...";
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }
        if (e.CommandName == "Guncelle")
        {
            string id = Convert.ToString(e.CommandArgument);

            SqlConnection con = code.baglan();
            string guncelle = "UPDATE contactform SET iform_okundu=@iform_okundu WHERE iform_id = @iform_id";
            SqlCommand cmdguncelle = new SqlCommand(guncelle, con);
            cmdguncelle.Parameters.AddWithValue("@iform_okundu", "True");
            cmdguncelle.Parameters.AddWithValue("@iform_id", id.Trim());
            cmdguncelle.ExecuteNonQuery();
            cmdguncelle.Dispose();
            con.Close();
            Response.Redirect("formgoster.aspx?formid=" + id);
        }
    }
}