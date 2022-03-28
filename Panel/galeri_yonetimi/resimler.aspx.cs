using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class galeriyonetimi_resimler : System.Web.UI.Page
{
    General_Code code = new General_Code();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Listele();
        }
        panel_basarili.Visible = false;
    }

    void Listele()
    {

        string sql = "SELECT resim_id, resim_adi, galeri_resim, resim_durum FROM photo ORDER BY resim_id DESC";
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

            string sqlresimad = "SELECT galeri_resim, resim_thumb1, resim_thumb2 FROM photo WHERE (resim_id = @resim_id)";
            SqlCommand cmd = new SqlCommand(sqlresimad, con);
            cmd.Parameters.AddWithValue("@resim_id", id.Trim());
            DataRow dr = code.GetDataRow(cmd);
            if (dr != null)
            {
                string resimadi = dr["galeri_resim"].ToString();
                if ((!String.IsNullOrEmpty(resimadi)) || (resimadi != "default_image.png"))
                {
                    File.Delete(Server.MapPath("galeri_resim/thumb2/" + resimadi));
                    File.Delete(Server.MapPath("galeri_resim/thumb1/" + resimadi));
                    File.Delete(Server.MapPath("galeri_resim/" + resimadi));
                }
            }
            cmd.Dispose();

            string silsql = "DELETE FROM photo WHERE resim_id='" + id + "'";
            SqlCommand command = new SqlCommand(silsql, con);
            command.ExecuteNonQuery();
            panel_basarili.CssClass = "aktif";
            panel_basarili.Visible = true;
            hd_basarili.Value = "Resim başarıyla silinmiştir. Lütfen Bekleyiniz. Resim Listesi güncelleniyor...";
            command.Dispose();
            con.Close();
            con.Dispose();
        }
        if (e.CommandName == "Guncelle")
        {
            string id = Convert.ToString(e.CommandArgument);
            Response.Redirect("resim_duzenle.aspx?resimid=" + id);
        }
    }
}