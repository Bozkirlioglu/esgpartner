using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class icerikyonetimi_icerik : System.Web.UI.Page
{
    General_Code code = new General_Code();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TemaGetir();
            Listele();
        }
        panel_basarili.Visible = false;
    }

    void Listele()
    {
        string tema_id = dd_tema.SelectedValue;
        string sql = "";
        if (tema_id == "0")
        {
            sql = "SELECT dbo.contents.icerik_id, theme.theme_name, theme_content.theme_image, theme_content.image_createdate, theme_content.image_thumb1, theme_content.image_thumb2, dbo.contents.icerik_adi, theme_content.themerecord_id FROM theme_content INNER JOIN theme ON theme_content.theme_id = theme.theme_id INNER JOIN dbo.contents ON theme_content.theme_icerik = dbo.contents.icerik_id ORDER BY themerecord_id DESC";
        }
        else
        {
            sql = "SELECT dbo.contents.icerik_id, theme.theme_name, theme_content.theme_image, theme_content.image_createdate, theme_content.image_thumb1, theme_content.image_thumb2, dbo.contents.icerik_adi, theme_content.themerecord_id FROM theme_content INNER JOIN theme ON theme_content.theme_id = theme.theme_id INNER JOIN dbo.contents ON theme_content.theme_icerik = dbo.contents.icerik_id WHERE (theme.theme_id = '" + tema_id + "') ";
        }

        SqlCommand cmd = new SqlCommand(sql);
        DataTable dt = code.GetData(cmd);
        rpliste.DataSource = dt;
        rpliste.DataBind();

        cmd.Dispose();
    }

    void TemaGetir()
    {
        string strQuery = "SELECT theme_id, theme_name FROM theme";
        SqlCommand cmd = new SqlCommand(strQuery);
        DataTable dt = code.GetData(cmd);
        if (dt.Rows.Count > 0)
        {
            dd_tema.Items.Add(new ListItem("Tümü", "0"));
            foreach (DataRow dr in dt.Rows)
            {
                dd_tema.Items.Add(new ListItem(dr["theme_name"].ToString(), dr["theme_id"].ToString()));
            }
        }
        dt.Dispose();
    }

    protected void rpliste_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string id = Convert.ToString(e.CommandArgument);
            SqlConnection con = code.baglan();
            string sqlresimad = "SELECT theme_image, image_thumb1, image_thumb2 FROM theme_content WHERE (themerecord_id = @record_id)";
            SqlCommand command = new SqlCommand(sqlresimad, con);
            command.Parameters.AddWithValue("@record_id", id.Trim());
            DataRow dr = code.GetDataRow(command);
            if (dr != null)
            {
                string resimadi = dr["theme_image"].ToString();
                if ((!String.IsNullOrEmpty(resimadi)) || (resimadi != "default_image.png"))
                {
                    File.Delete(Server.MapPath("tema_resim/thumb2/" + resimadi));
                    File.Delete(Server.MapPath("tema_resim/thumb1/" + resimadi));
                    File.Delete(Server.MapPath("tema_resim/" + resimadi));
                }
            }
            command.Dispose();
            string silsql = "DELETE FROM theme_content WHERE themerecord_id=@record_id";
            SqlCommand cmd = new SqlCommand(silsql, con);
            cmd.Parameters.AddWithValue("record_id", id.Trim());
            cmd.ExecuteNonQuery();
            panel_basarili.CssClass = "aktif";
            panel_basarili.Visible = true;
            hd_basarili.Value = "Tema içerik resmi başarıyla silinmiştir. Lütfen Bekleyiniz. Tema içerik Listesi güncelleniyor...";
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }
        if (e.CommandName == "Guncelle")
        {
            string id = Convert.ToString(e.CommandArgument);
            Response.Redirect("tema_resim_duzenle.aspx?temaicerikid=" + id);
        }
    }
    protected void dd_kattur_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listele();
    }
}