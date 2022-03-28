using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class kategoriyonetimi_kategori : System.Web.UI.Page
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

        string cattype = dd_kattur.SelectedValue;
        string sql = "";
        if (cattype == "0")
        {
            sql = "SELECT category.kategori_id, category.kategori_adi, category.kategori_durum, category_type.category_type, category.kategori_resim FROM category INNER JOIN category_type ON category.kategori_turu = category_type.categorytype_id ORDER BY kategori_turu, kategori_id";
        }
        else
        {
            sql = "SELECT category.kategori_id, category.kategori_adi, category.kategori_durum, category_type.category_type, category.kategori_resim FROM category INNER JOIN category_type ON category.kategori_turu = category_type.categorytype_id WHERE (kategori_turu='" + cattype + "') ORDER BY kategori_turu, kategori_id";
        }
        
        SqlCommand cmd = new SqlCommand(sql);
        DataTable dt = code.GetData(cmd);
        rpliste.DataSource = dt;
        rpliste.DataBind();

        cmd.Dispose();
    }

    void KategoriTurGetir()
    {
        string strQuery = "SELECT categorytype_id, category_type FROM category_type";
        SqlCommand cmd = new SqlCommand(strQuery);
        DataTable dt = code.GetData(cmd);
        if (dt.Rows.Count > 0 )
        {
            dd_kattur.Items.Add(new ListItem("Tümü", "0"));
            foreach (DataRow dr in dt.Rows)
            {
                dd_kattur.Items.Add(new ListItem(dr["category_type"].ToString(),dr["categorytype_id"].ToString()));
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
            string silsql = "DELETE FROM category WHERE kategori_id='" + id + "'";
            SqlCommand command = new SqlCommand(silsql, con);
            command.ExecuteNonQuery();
            panel_basarili.CssClass = "aktif";
            panel_basarili.Visible = true;
            hd_basarili.Value = "Kategori başarıyla silinmiştir...";
            command.Dispose();
            con.Close();
            con.Dispose();
        }
        if (e.CommandName == "Guncelle")
        {
            string id = Convert.ToString(e.CommandArgument);
            Response.Redirect("kategori_duzenle.aspx?kategoriid=" + id);
        }
    }
    protected void dd_kattur_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listele();
    }
}