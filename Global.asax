<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        Routes(RouteTable.Routes);
    }

    void Routes(RouteCollection rountes)
    {
        rountes.MapPageRoute("anasayfa", "", "~/Main/Default.aspx");
        rountes.MapPageRoute("hakkimizda", "content/hakkimizda", "~/Sayfalar/hakkimizda.aspx");
        rountes.MapPageRoute("cozumler", "content/karbon-ayakizi-danismanligi", "~/Sayfalar/karbon.aspx");
        rountes.MapPageRoute("surdurulebilirlik", "content/surdurebilirlik-danismanligi", "~/Sayfalar/surdurebilirlik.aspx");
        rountes.MapPageRoute("egitim", "content/egitim-ve-calistaylar", "~/Sayfalar/egitim.aspx");
        rountes.MapPageRoute("referans", "content/referanslar", "~/Sayfalar/referanslar.aspx");
        rountes.MapPageRoute("blog", "content/blog", "~/Sayfalar/blog.aspx");
        rountes.MapPageRoute("blogdetay", "blog/{url}", "~/Sayfalar/blogdetay.aspx");
        rountes.MapPageRoute("iletisim", "content/iletisim", "~/Sayfalar/iletisim.aspx");

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown
    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

</script>
