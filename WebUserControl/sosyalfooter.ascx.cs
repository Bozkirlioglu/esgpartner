using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUserControl_sosyalfooter : System.Web.UI.UserControl
{
    General_Code code = new General_Code();
    protected void Page_Load(object sender, EventArgs e)
    {
        SosyalGetir();
    }
    void SosyalGetir()
    {
        string sql = "SELECT sosyal_id, sosyal_adi, sosyal_link FROM social";
        SqlCommand cmd = new SqlCommand(sql);
        DataTable dt = code.GetData(cmd);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!String.IsNullOrEmpty(dt.Rows[i]["sosyal_link"].ToString()))
                {
                    switch (i)
                    {
                        case 0:
                            facebook.Visible = true;
                            hyperlink_facebook.NavigateUrl = dt.Rows[i]["sosyal_link"].ToString();
                            break;
                        case 1:
                            twitter.Visible = true;
                            hyperlink_twitter.NavigateUrl = dt.Rows[i]["sosyal_link"].ToString();
                            break;
                        case 2:
                            instagram.Visible = true;
                            hyperlink_instagram.NavigateUrl = dt.Rows[i]["sosyal_link"].ToString();
                            break;
                        case 3:
                            youtube.Visible = true;
                            hyperlink_youtube.NavigateUrl = dt.Rows[i]["sosyal_link"].ToString();
                            break;
                        case 4:
                            googleplus.Visible = true;
                            hyperlink_googleplus.NavigateUrl = dt.Rows[i]["sosyal_link"].ToString();
                            break;
                        case 5:

                            break;
                        case 6:
                            linkedin.Visible = true;
                            hyperlink_linkedin.NavigateUrl = dt.Rows[i]["sosyal_link"].ToString();
                            break;
                    }
                }
                else
                {
                    switch (i)
                    {
                        case 0:
                            facebook.Visible = false;
                            break;
                        case 1:
                            twitter.Visible = false;
                            break;
                        case 2:
                            instagram.Visible = false;
                            break;
                        case 3:
                            youtube.Visible = false;
                            break;
                        case 4:
                            googleplus.Visible = false;
                            break;
                        case 5:
                            break;
                        case 6:
                            linkedin.Visible = false;
                            break;
                    }
                }
            }
        }
        else
        {
            facebook.Visible = false;
            twitter.Visible = false;
            instagram.Visible = false;
            youtube.Visible = false;
            googleplus.Visible = false;
            linkedin.Visible = false;
        }
        cmd.Dispose();
    }
}