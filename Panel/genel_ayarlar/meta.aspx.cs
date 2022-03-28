using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class genelayarlar_meta : System.Web.UI.Page
{
    General_Code code = new General_Code();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        panel_basarili.Visible = false;
        panel_hatali.Visible = false;
        panel_uyari.Visible = false;
        if(!IsPostBack)
        { 
            MetaGetir();
        }
    }
    protected void Kaydet_Click(object sender, EventArgs e)
    {
        string title = txt_title.Text;
        string description = txt_description.Text;
        string keywords = txt_keywords.Text;
        string publisher = txt_publisher.Text;
        string copyright = txt_copyright.Text;
        
        SqlConnection con = code.baglan();
        try
        {
            string metaguncelle = "UPDATE meta SET meta_title=@meta_title,meta_description=@meta_description,meta_keywords=@meta_keywords,meta_publisher=@meta_publisher, meta_copyright=@meta_copyright where meta_id = 1";
            SqlCommand cmdmeta = new SqlCommand(metaguncelle, con);
            cmdmeta.Parameters.AddWithValue("@meta_title", title);
            cmdmeta.Parameters.AddWithValue("@meta_description", description);
            cmdmeta.Parameters.AddWithValue("@meta_keywords", keywords);
            cmdmeta.Parameters.AddWithValue("@meta_publisher", publisher);
            cmdmeta.Parameters.AddWithValue("@meta_copyright", copyright);
            cmdmeta.ExecuteNonQuery();
            cmdmeta.Dispose();

            con.Close();
            panel_basarili.CssClass = "aktif";
            panel_basarili.Visible = true;
            hd_basarili.Value = "Meta tanımlamaları başarıyla eklendi.";

        }
        catch (Exception ex)
        {
            panel_hatali.CssClass = "aktif";
            panel_hatali.Visible = true;
            hd_uyari.Value = "Meta tanımlamaları sırasında bir hata oluştu... Hata: " + ex;
        }
        
    }

    void MetaGetir()
    {
        SqlConnection con = code.baglan();
        SqlCommand sorgu = new SqlCommand("SELECT meta.* FROM meta ", con);
        SqlDataAdapter da = new SqlDataAdapter(sorgu);
        DataTable dt = new DataTable();

        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            txt_title.Text = dt.Rows[0][1].ToString();
            txt_description.Text = dt.Rows[0][2].ToString();
            txt_keywords.Text = dt.Rows[0][3].ToString();
            txt_publisher.Text = dt.Rows[0][4].ToString();
            txt_copyright.Text = dt.Rows[0][5].ToString();
        }
        con.Close();
        da.Dispose();    
    }
}