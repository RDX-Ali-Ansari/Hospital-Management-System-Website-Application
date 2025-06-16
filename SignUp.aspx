<%@ Page Language="VB" AutoEventWireup="true" CodeFile="Signup.aspx.vb" Inherits="Signup" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hospital Management System - Patient Sign Up</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f8f9fa;
        }
        .signup-container {
            max-width: 500px;
            margin: 50px auto;
            padding: 20px;
            background-color: #fff;
            border-radius: 5px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
        }
        .header {
            text-align: center;
            margin-bottom: 30px;
        }
        .form-group {
            margin-bottom: 15px;
        }
        .btn-signup {
            width: 100%;
            background-color: #0d6efd;
            border: none;
        }
    </style>
</head>
<body>
    <form id="formSignup" runat="server">
        <div class="container signup-container">
            <div class="header">
                <h2>Hospital Management System</h2>
                <h4>Patient Registration</h4>
            </div>
            
            <!-- Patient-specific fields -->
            <div class="form-group">
                <label for="txtPatientName" class="form-label">Full Name:</label>
                <asp:TextBox ID="txtPatientName" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvPatientName" runat="server"
                    ControlToValidate="txtPatientName" ErrorMessage="Name is required"
                    ForeColor="Red" Display="Dynamic" />
            </div>
                
            <div class="form-group">
                <label for="txtPatientAge" class="form-label">Age:</label>
                <asp:TextBox ID="txtPatientAge" runat="server" CssClass="form-control" TextMode="Number" />
                <asp:RequiredFieldValidator ID="rfvPatientAge" runat="server"
                    ControlToValidate="txtPatientAge" ErrorMessage="Age is required"
                    ForeColor="Red" Display="Dynamic" />
                <asp:RangeValidator ID="rvPatientAge" runat="server" 
                    ControlToValidate="txtPatientAge" ErrorMessage="Age must be between 1 and 120"
                    MinimumValue="1" MaximumValue="120" Type="Integer"
                    ForeColor="Red" Display="Dynamic" />
            </div>
                
            <div class="form-group">
                <label for="ddlGender" class="form-label">Gender:</label>
                <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Select gender" Value="" Selected="True" />
                    <asp:ListItem Text="Male" Value="M" />
                    <asp:ListItem Text="Female" Value="F" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvGender" runat="server"
                    ControlToValidate="ddlGender" ErrorMessage="Please select gender"
                    ForeColor="Red" Display="Dynamic" />
            </div>
                
            <div class="form-group">
                <label for="txtContactNumber" class="form-label">Contact Number:</label>
                <asp:TextBox ID="txtContactNumber" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvContactNumber" runat="server"
                    ControlToValidate="txtContactNumber" ErrorMessage="Contact number is required"
                    ForeColor="Red" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="revContactNumber" runat="server"
                    ControlToValidate="txtContactNumber" 
                    ErrorMessage="Enter a valid contact number (e.g., 03001234567)"
                    ValidationExpression="^[0-9]{11}$"
                    ForeColor="Red" Display="Dynamic" />
            </div>
            
            <!-- Common credentials fields -->
            <div class="form-group">
                <label for="txtUsername" class="form-label">Username:</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvUsername" runat="server"
                    ControlToValidate="txtUsername" ErrorMessage="Username is required"
                    ForeColor="Red" Display="Dynamic" />
            </div>
            
            <div class="form-group">
                <label for="txtEmail" class="form-label">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                    ControlToValidate="txtEmail" ErrorMessage="Email is required"
                    ForeColor="Red" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="revEmail" runat="server"
                    ControlToValidate="txtEmail" ErrorMessage="Enter a valid email address"
                    ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[A-Za-z]{2,}$"
                    ForeColor="Red" Display="Dynamic" />
            </div>
            
            <div class="form-group">
                <label for="txtPassword" class="form-label">Password:</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                    ControlToValidate="txtPassword" ErrorMessage="Password is required"
                    ForeColor="Red" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="revPassword" runat="server"
                    ControlToValidate="txtPassword" 
                    ErrorMessage="Password must be at least 8 characters with letters and numbers"
                    ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"
                    ForeColor="Red" Display="Dynamic" />
            </div>
            
            <div class="form-group">
                <label for="txtConfirmPassword" class="form-label">Confirm Password:</label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" />
                <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server"
                    ControlToValidate="txtConfirmPassword" ErrorMessage="Please confirm your password"
                    ForeColor="Red" Display="Dynamic" />
                <asp:CompareValidator ID="cvPassword" runat="server"
                    ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword"
                    ErrorMessage="Passwords do not match" ForeColor="Red" Display="Dynamic" />
            </div>
            
            <div class="form-group mb-4">
                <asp:Button ID="btnSignup" runat="server" Text="Sign Up" CssClass="btn btn-primary btn-signup"
                    OnClick="btnSignup_Click" />
            </div>
            
            <div class="text-center">
                <p>Already have an account? <a href="Login.aspx">Log In</a></p>
            </div>
            
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
        </div>
    </form>
    
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/js/bootstrap.bundle.min.js"></script>
</body>
</html>