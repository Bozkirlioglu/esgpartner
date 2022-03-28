<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="library_notification_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="css/base.css" rel="stylesheet" />
    <link href="css/notifyme.css" rel="stylesheet" />
    <script src="js/modernizr.js"></script>
    <script src="js/jquery.js"></script>
    <script src="js/notifyme.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.error').on('click', function () {
                $(this).notifyMe(
                    'bottom',
                    'error',
                    'Lorem Ipsum Text',
                    'Lorem ipsum dolos lorem uisnsnd h jsakdh ajkdbh',
                    200,
                    5000
                );
            });

            $('.info').on('click', function () {
                $(this).notifyMe(
                    'top',
                    'info',
                    'Lorem Ipsum Text',
                    'Lorem ipsum dolos lorem uisnsnd h jsakdh ajkdbh',
                    300,
                    1500
                );
            });

            $('.success').on('click', function () {
                $(this).notifyMe(
                    'left',
                    'success',
                    'Lorem Ipsum Text',
                    'Lorem ipsum dolos lorem uisnsnd h jsakdh ajkdbh',
                    600,
                    3000
                );
            });

            $('.default').on('click', function () {
                $(this).notifyMe(
                    'right',
                    'default',
                    'Lorem Ipsum Text',
                    'Lorem ipsum dolos lorem uisnsnd h jsakdh ajkdbh',
                    500,
                    2000
                );
            });
        });
	</script>

    <script>

        ohSnap("deneme", red);
        
        </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="btn-group">
			<a class="btn default"><i class="fa fa-coffee"></i> Default</a>
			<a class="btn error"><i class="fa fa-warning"></i> Error</a>
			<a class="btn info"><i class="fa fa-info"></i> Info</a>
			<a class="btn success"><i class="fa fa-check"></i> Success</a>
		</div>
    </form>
</body>
</html>
