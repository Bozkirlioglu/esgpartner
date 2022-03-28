using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class genelayarlar_thumbayar : System.Web.UI.Page
{
    General_Code code = new General_Code();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        panel_basarili.Visible = false;
        panel_hatali.Visible = false;
        panel_uyari.Visible = false;
        if(!IsPostBack)
        { 
            ThumbGetir();
        }
    }
    protected void Kaydet_Click(object sender, EventArgs e)
    {
        string icerikthumb1w = txt_icthumb1width.Text;
        string icerikthumb1h = txt_icthumb1height.Text;
        string icerikthumb2w = txt_icthumb2width.Text;
        string icerikthumb2h = txt_icthumb2height.Text;

        string galerithumb1w = txt_galerithumb1width.Text;
        string galerithumb1h = txt_galerithumb1height.Text;
        string galerithumb2w = txt_galerithumb2width.Text;
        string galerithumb2h = txt_galerithumb2height.Text;


        SqlConnection con = code.baglan();
        try
        {
            string icerikthumb = "UPDATE thumb SET thumb1_width=@thumb1_width,thumb1_height=@thumb1_height,thumb2_width=@thumb2_width,thumb2_height=@thumb2_height where thumb_id = 1";
            SqlCommand cmd_icerikthumb = new SqlCommand(icerikthumb, con);
            cmd_icerikthumb.Parameters.AddWithValue("@thumb1_width", icerikthumb1w);
            cmd_icerikthumb.Parameters.AddWithValue("@thumb1_height", icerikthumb1h);
            cmd_icerikthumb.Parameters.AddWithValue("@thumb2_width", icerikthumb2w);
            cmd_icerikthumb.Parameters.AddWithValue("@thumb2_height", icerikthumb2h);
            cmd_icerikthumb.ExecuteNonQuery();
            cmd_icerikthumb.Dispose();

            string galerithumb = "UPDATE thumb SET thumb1_width=@thumb1_width,thumb1_height=@thumb1_height,thumb2_width=@thumb2_width,thumb2_height=@thumb2_height where thumb_id = 2";
            SqlCommand cmd_galerithumb = new SqlCommand(galerithumb, con);
            cmd_galerithumb.Parameters.AddWithValue("@thumb1_width", galerithumb1w);
            cmd_galerithumb.Parameters.AddWithValue("@thumb1_height", galerithumb1h);
            cmd_galerithumb.Parameters.AddWithValue("@thumb2_width", galerithumb2w);
            cmd_galerithumb.Parameters.AddWithValue("@thumb2_height", galerithumb2h);
            cmd_galerithumb.ExecuteNonQuery();
            cmd_galerithumb.Dispose();

            con.Close();
            panel_basarili.CssClass = "aktif";
            panel_basarili.Visible = true;
            hd_basarili.Value = "Thumb ölçüleri başarıyla tanımlandı...";

        }
        catch (Exception ex)
        {
            panel_hatali.CssClass = "aktif";
            panel_hatali.Visible = true;
            hd_uyari.Value = "Thumb ölçüleri tanımlamaları sırasında bir hata oluştu... Hata: " + ex;
        }
        finally
        {
            con.Close();
        }
        
    }

    void ThumbGetir()
    {
        SqlConnection con = code.baglan();
        SqlCommand sorgu = new SqlCommand("SELECT thumb.* FROM thumb ", con);
        SqlDataAdapter da = new SqlDataAdapter(sorgu);
        DataTable dt = new DataTable();

        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            txt_icthumb1width.Text = dt.Rows[0][1].ToString();
            txt_icthumb1height.Text = dt.Rows[0][2].ToString();
            txt_icthumb2width.Text = dt.Rows[0][3].ToString();
            txt_icthumb2height.Text = dt.Rows[0][4].ToString();

            txt_galerithumb1width.Text = dt.Rows[1][1].ToString();
            txt_galerithumb1height.Text = dt.Rows[1][2].ToString();
            txt_galerithumb2width.Text = dt.Rows[1][3].ToString();
            txt_galerithumb2height.Text = dt.Rows[1][4].ToString();
            
        }
        con.Close();
        da.Dispose();    
    }
}