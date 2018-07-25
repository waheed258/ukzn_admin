<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>AdminLTE 2 | Log in</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css">
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css">
    <!-- Theme style -->
    <link rel="stylesheet" href="dist/css/AdminLTE.min.css">
    <!-- iCheck -->
    <link rel="stylesheet" href="plugins/iCheck/square/blue.css">
    <link href="Css/Custom.css" rel="stylesheet" />

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->
</head>
<body class="hold-transition login-page">
    <div class="login-box">
        <div class="login-logo">
            <a href="../../index2.html"><b>UKZN</b></a>
        </div>
        <!-- /.login-logo -->
        <div class="login-box-body">
            <p class="login-box-msg">Forgot Password</p>
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
            <form action="#" method="post" runat="server">
                <div class="form-group has-feedback">
                    <asp:TextBox runat="server" ID="txtEmailId" class=" form-control" placeholder="Enter Email Id"
                                    autofocus="" ValidationGroup="ForgotPassword"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtEmailId" runat="server" ID="rfvtxtLoginId"
                                    ValidationGroup="ForgotPassword" ErrorMessage="Enter your EmailId." Text="Enter your EmailId."
                                    class="validationred" />
                    <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                </div>
               
                <div class="row">
                   <div class="col-xs-8">
                       </div>
                    <!-- /.col -->
                    <div class="col-xs-4">
                        <asp:Button runat="server" ID="cmdSubmit" class="btn btn-primary btn-block btn-flat"
                            ValidationGroup="ForgotPassword" Text="Send"   OnClick="cmdSubmit_Click" />
                        
                    </div>
                    <!-- /.col -->
                </div>
            </form>


        </div>
        <!-- /.login-box-body -->
    </div>
    <!-- /.login-box -->

    <!-- jQuery 2.2.3 -->
    <script src="plugins/jQuery/jquery-2.2.3.min.js"></script>
    <!-- Bootstrap 3.3.6 -->
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <!-- iCheck -->
    <script src="plugins/iCheck/icheck.min.js"></script>
    <script>
        $(function () {
            $('input').iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue',
                increaseArea: '20%' // optional
            });
        });
</script>
</body>
</html>