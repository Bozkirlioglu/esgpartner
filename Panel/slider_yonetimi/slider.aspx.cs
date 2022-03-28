using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Panel_slider_yonetimi_slider : System.Web.UI.Page
{
    General_Code code = new General_Code();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Listele();
        }
        panel_basarili.Visible = false;
    }

    void Listele()
    {
        string sql = "SELECT slider.slider_kategori, category.kategori_adi, slider.* FROM slider INNER JOIN category ON slider.slider_kategori = category.kategori_id ORDER BY category.kategori_id";
        SqlCommand cmd = new SqlCommand(sql);
        DataTable dt = code.GetData(cmd);
        rpliste.DataSource = dt;
        rpliste.DataBind();

        cmd.Dispose();
    }

    protected void rpliste_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Panel panel_silmebasarili = (Panel)e.Item.FindControl("panel_silmebasarili");
        if (e.CommandName == "Delete")
        {
            string id = Convert.ToString(e.CommandArgument);
            SqlConnection con = code.baglan();
            string silsql = "DELETE FROM slider WHERE slider_id='" + id + "'";
            SqlCommand command = new SqlCommand(silsql, con);
            command.ExecuteNonQuery();
            command.Dispose();
            con.Close();
            con.Dispose();
            Response.Redirect(Request.RawUrl);
            //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
        }
        if (e.CommandName == "Guncelle")
        {

            string id = Convert.ToString(e.CommandArgument);
            Response.Redirect("slider_duzenle.aspx?sliderid=" + id);
        }
    }


}