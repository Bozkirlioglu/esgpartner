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
            KategoriTurGetir();
            Listele();
        }
        panel_basarili.Visible = false;
    }

    void Listele()
    {
        string kategori_id = dd_kattur.SelectedValue;
        string sql = "";
        if (kategori_id == "0")
        {
            sql = "SELECT contents.icerik_id, contents.icerik_adi, contents.icerik_kategori, contents.icerik_resim, contents.icerik_durum, category.kategori_adi FROM contents INNER JOIN category ON contents.icerik_kategori = category.kategori_id ORDER BY contents.icerik_kategori, contents.icerik_id DESC";
        }
        else
        {
            sql = "SELECT contents.icerik_id, contents.icerik_adi, contents.icerik_kategori, contents.icerik_resim, contents.icerik_durum, category.kategori_adi FROM contents INNER JOIN category ON contents.icerik_kategori = category.kategori_id WHERE (category.kategori_id='" + kategori_id + "') ";
        }

        SqlCommand cmd = new SqlCommand(sql);
        DataTable dt = code.GetData(cmd);
        rpliste.DataSource = dt;
        rpliste.DataBind();

        cmd.Dispose();
    }

    void KategoriTurGetir()
    {
        string strQuery = "SELECT kategori_id, kategori_adi FROM category WHERE (kategori_turu = 1)";
        SqlCommand cmd = new SqlCommand(strQuery);
        DataTable dt = code.GetData(cmd);
        if (dt.Rows.Count > 0)
        {
            dd_kattur.Items.Add(new ListItem("Tümü", "0"));
            foreach (DataRow dr in dt.Rows)
            {
                dd_kattur.Items.Add(new ListItem(dr["kategori_adi"].ToString(), dr["kategori_id"].ToString()));
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
            string sqlresimad = "SELECT icerik_resim, icerik_thumb1, icerik_thumb2 FROM contents WHERE (icerik_id = @icerik_id)";
            SqlCommand command = new SqlCommand(sqlresimad, con);
            command.Parameters.AddWithValue("@icerik_id", id.Trim());
            DataRow dr = code.GetDataRow(command);
            if (dr != null)
            {
                string resimadi = dr["icerik_resim"].ToString();
                if ((!String.IsNullOrEmpty(resimadi)) || (resimadi != "default_image.png"))
                {
                    File.Delete(Server.MapPath("icerik_resim/thumb2/" + resimadi));
                    File.Delete(Server.MapPath("icerik_resim/thumb1/" + resimadi));
                    File.Delete(Server.MapPath("icerik_resim/" + resimadi));
                }
            }
            command.Dispose();
            string silsql = "DELETE FROM contents WHERE icerik_id=@icerik_id";
            SqlCommand cmd = new SqlCommand(silsql, con);
            cmd.Parameters.AddWithValue("icerik_id", id.Trim());
            cmd.ExecuteNonQuery();
            panel_basarili.CssClass = "aktif";
            panel_basarili.Visible = true;
            hd_basarili.Value = "İçerik başarıyla silinmiştir. Lütfen Bekleyiniz. İçerik Listesi güncelleniyor...";
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }
        if (e.CommandName == "Guncelle")
        {
            string id = Convert.ToString(e.CommandArgument);
            Response.Redirect("icerik_duzenle.aspx?icerikid=" + id);
        }
    }
    protected void dd_kattur_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listele();
    }
}