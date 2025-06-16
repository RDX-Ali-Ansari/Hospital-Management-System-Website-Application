<%@ Page Language="VB" AutoEventWireup="true" CodeFile="Login.aspx.vb" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hospital Management System - Login</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f8f9fa;
        }
        .login-container {
            max-width: 450px;
            margin: 100px auto;
            padding: 30px;
            background-color: #fff;
            border-radius: 5px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
        }
        .header {
            text-align: center;
            margin-bottom: 30px;
        }
        .form-group {
            margin-bottom: 20px;
        }
        .btn-login {
            width: 100%;
            background-color: #0d6efd;
            border: none;
            padding: 10px;
            font-size: 16px;
        }
        .alert {
            margin-top: 20px;
        }
        .register-link {
            display: block;
            text-align: center;
            margin-top: 20px;
        }
    </style>
</head>
<body>
    <form id="formLogin" runat="server">
        <div class="container login-container">
            <div class="header">
                <h2>Hospital Management System</h2>
                <h4>Login to Your Account</h4>
            </div>
            
            <!-- Success message after registration -->
            <asp:Panel ID="pnlSuccess" runat="server" CssClass="alert alert-success" Visible="false">
                <asp:Literal ID="litSuccess" runat="server"></asp:Literal>
            </asp:Panel>
            
            <!-- Error message -->
            <asp:Panel ID="pnlError" runat="server" CssClass="alert alert-danger" Visible="false">
                <asp:Literal ID="litError" runat="server"></asp:Literal>
            </asp:Panel>
            
            <div class="form-group">
                <label for="txtUsername" class="form-label">Username or Email:</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter username or email" />
                <asp:RequiredFieldValidator ID="rfvUsername" runat="server"
                    ControlToValidate="txtUsername" ErrorMessage="Username or email is required"
                    ForeColor="Red" Display="Dynamic" />
            </div>
            
            <div class="form-group">
                <label for="txtPassword" class="form-label">Password:</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" 
                    placeholder="Enter password" />
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                    ControlToValidate="txtPassword" ErrorMessage="Password is required"
                    ForeColor="Red" Display="Dynamic" />
            </div>
            
            <div class="form-group form-check">
                <asp:CheckBox ID="chkRememberMe" runat="server" CssClass="form-check-input" />
                <label for="chkRememberMe" class="form-check-label">Remember me</label>
            </div>
            
            <div class="form-group">
                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary btn-login"
                    OnClick="btnLogin_Click" />
            </div>
            
            <div class="text-center">
                <a href="#" id="forgotPassword">Forgot Password?</a>
                <a href="Signup.aspx" class="register-link">Don't have an account? Register now</a>
            </div>

            </div>
    </form>
    
    <!-- JavaScript for Bootstrap -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/js/bootstrap.bundle.min.js"></script>
</body>
</html>