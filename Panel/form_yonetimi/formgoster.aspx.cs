using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class formyonetimi_formgoster : System.Web.UI.Page
{
    General_Code code = new General_Code();
    string id;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FormGoster();
        }
        panel_basarili.Visible = false;
    }
    void FormGoster()
    {

        try
        {
            id = Request.QueryString["formid"];
            
            string strQuery = "select * from contactform where iform_id = @id";
            SqlCommand cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@id", id.Trim());
            DataRow dr = code.GetDataRow(cmd);
            if (dr != null)
            {
                lt_tarih.Text = dr["iform_tarih"].ToString();
                lt_adsoyad.Text = dr["iform_adi"].ToString();
                lt_mail.Text = dr["iform_mail"].ToString();
                lt_tel.Text = dr["iform_tel"].ToString();
                lt_konu.Text = dr["iform_konu"].ToString();
                lt_mesaj.Text = dr["iform_mesaj"].ToString();
            }
        }
        catch (Exception ex)
        {
            panel_hatali.CssClass = "aktif";
            panel_hatali.Visible = true;
            hd_uyari.Value = "Bir hata oluştu. Formlar sayfasına yönlendiriliyorsunuz. Lütfen bekleyiniz... Hata: " + ex;
        }

    }
}