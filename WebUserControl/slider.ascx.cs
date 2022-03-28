using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUserControl_slider : System.Web.UI.UserControl
{
    General_Code code = new General_Code();
    protected void Page_Load(object sender, EventArgs e)
    {
        SliderGetir();
    }
    void SliderGetir()
    {
        string sql = "SELECT slider_id, slider_adi, slider_kisaaciklama, slider_url, slider_resim FROM slider WHERE (slider_durum = 1)";
        SqlCommand cmd = new SqlCommand(sql);
        DataTable dt = code.GetData(cmd);
        if (dt.Rows.Count > 0)
        {
            rp_slider.DataSource = dt;
            rp_slider.DataBind();

            rp_indicators.DataSource = dt;
            rp_indicators.DataBind();
        }
        dt.Dispose();
    }    
}