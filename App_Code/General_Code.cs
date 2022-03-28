using ImageResizer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for General_Code
/// </summary>
public class General_Code
{
    public General_Code()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public SqlConnection baglan()
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        conn.Open();
        return conn;
    }

    public static string ToUrl(string text)
    {
        if (text == "" || text == null) { return ""; }
        text = text.Replace("ş", "s");
        text = text.Replace("Ş", "S");
        text = text.Replace(".", "");
        text = text.Replace(":", "");
        text = text.Replace(";", "");
        text = text.Replace(",", "");
        text = text.Replace(" ", "_");
        text = text.Replace("!", "");
        text = text.Replace("(", "");
        text = text.Replace(")", "");
        text = text.Replace("'", "");
        text = text.Replace("ğ", "g");
        text = text.Replace("Ğ", "G");
        text = text.Replace("ı", "i");
        text = text.Replace("I", "i");
        text = text.Replace("ç", "c");
        text = text.Replace("ç", "C");
        text = text.Replace("ö", "o");
        text = text.Replace("Ö", "O");
        text = text.Replace("ü", "u");
        text = text.Replace("Ü", "U");
        text = text.Replace("`", "");
        text = text.Replace("=", "");
        text = text.Replace("&", "");
        text = text.Replace("%", "");
        text = text.Replace("#", "");
        text = text.Replace("<", "");
        text = text.Replace(">", "");
        text = text.Replace("*", "");
        text = text.Replace("?", "");
        text = text.Replace("+", "-");
        text = text.Replace("\"", "-");
        text = text.Replace("»", "-");
        text = text.Replace("|", "-");
        text = text.Replace("^", "");
        return text;
    }

    public static string GetUniqueKey(Int32 max)
    {
        int maxSize = max;
        //int minSize = 5;
        char[] chars = new char[62];
        string a;
        a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        chars = a.ToCharArray();
        int size = maxSize;
        byte[] data = new byte[1];
        RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
        crypto.GetNonZeroBytes(data);
        size = maxSize;
        data = new byte[size];
        crypto.GetNonZeroBytes(data);
        StringBuilder result = new StringBuilder(size);
        foreach (byte b in data)
        { result.Append(chars[b % (chars.Length - 1)]); }
        return result.ToString();
    }

    public string ResimEkle(FileUpload resimekle, TextBox resim_adi, string dosyayolu, string ayrac, Panel uyari, Panel success, Label labelerror)
    {

        int dosyaboyutu = 0;
        string resimadi = "default_image.png";
        string dosyauzantisi = "";

        if (resimekle.HasFile)
        {

            dosyaboyutu = resimekle.PostedFile.ContentLength;
            if (dosyaboyutu <= 1000000)
            {
                dosyauzantisi = Path.GetExtension(resimekle.PostedFile.FileName);
                resimadi = ToUrl(resim_adi.Text) + ayrac + DateTime.Now.Day + "_" + GetUniqueKey(5) + dosyauzantisi;
                resimekle.SaveAs(dosyayolu + resimadi);

            }
            else
            {
                uyari.Visible = true;
                labelerror.Text = "Dosya Boyutu Yüksek ! Max 1 mb";
                //updatepanel.DataBind();
            }

        }

        return resimadi;
    }

    public DataTable GetData(SqlCommand cmd)
    {
        DataTable dt = new DataTable();
        //  String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

        SqlConnection con = this.baglan();
        SqlDataAdapter sda = new SqlDataAdapter();
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;

        try
        {
            // con.Open();
            sda.SelectCommand = cmd;
            sda.Fill(dt);

            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " (" + cmd + ")");

        }
        finally
        {
            con.Close();
            sda.Dispose();
            con.Dispose();
            cmd.Dispose();
        }
    }
    public DataRow GetDataRow(SqlCommand cmd)
    {
        DataTable table = GetData(cmd);
        if (table.Rows.Count == 0) return null;
        return table.Rows[0];
    }
    public int Command(string CommandText)
    {
        SqlConnection con = this.baglan();
        SqlCommand cmd = new SqlCommand(CommandText, con);
        int sonuc = 0;
        try
        {
            sonuc = cmd.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            throw new Exception(ex.Message + " (" + CommandText + ")");
        }
        cmd.Dispose();
        con.Close();
        return (sonuc);
    }

    public bool MailKontrolu(string inputEmail)
    {
        string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
              @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
              @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        Regex re = new Regex(strRegex);
        if (re.IsMatch(inputEmail))
            return (true);
        else
            return (false);
    }
}