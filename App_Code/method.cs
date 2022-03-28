using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for method
/// </summary>
public class method
{
    public static string WhichMonth(string month)
    {
        string ay = "";
        if (month == "01")
        {
            ay = "Ocak";
        }
        if (month == "02")
        {
            ay = "Şubat";
        }
        if (month == "03")
        {
            ay = "Mart";
        }
        if (month == "04")
        {
            ay = "Nisan";
        }
        if (month == "05")
        {
            ay = "Mayıs";
        }
        if (month == "06")
        {
            ay = "Haziran";
        }
        if (month == "07")
        {
            ay = "Temmuz";
        }
        if (month == "08")
        {
            ay = "Ağustos";
        }
        if (month == "09")
        {
            ay = "Eylül";
        }
        if (month == "10")
        {
            ay = "Ekim";
        }
        if (month == "11")
        {
            ay = "Kasım";
        }
        if (month == "12")
        {
            ay = "Aralık";
        }
        return ay;
    }
}