<%@ Page Language="VB" AutoEventWireup="true" CodeFile="Add_Employee.aspx.vb" Inherits="Add_Employee" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add New Employee</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body { background-color: #f0f2f5; }
        .container { max-width: 800px; margin-top: 50px; }
        .card { box-shadow: 0 2px 8px rgba(0,0,0,0.1); }
        .card-header { background-color: #0d6efd; color: white; }
    </style>
</head>
<body>
    <form runat="server">
        <div class="container">
            <div class="card">
                <div class="card-header">
                    <h5 class="mb-0">Add Employee</h5>
                </div>
                <div class="card-body">
                    <!-- EMPLOYEE -->
                    <div class="mb-3">
                        <label class="form-label">Employee ID</label>
                        <asp:TextBox ID="txtEmpId" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Name</label>
                        <asp:TextBox ID="txtEmpName" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Department</label>
                        <asp:TextBox ID="txtDept" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Designation</label>
                        <asp:TextBox ID="txtDesig" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Room ID</label>
                        <asp:TextBox ID="txtRoomId" runat="server" CssClass="form-control" />
                    </div>

                    <!-- CONTACT -->
                    <div class="mb-3">
                        <label class="form-label">Contact Number</label>
                        <asp:TextBox ID="txtContact" runat="server" CssClass="form-control" />
                    </div>

                    <!-- QUALIFICATION -->
                    <div class="mb-3">
                        <label class="form-label">Qualification</label>
                        <asp:TextBox ID="txtQual" runat="server" CssClass="form-control" />
                    </div>

                    <!-- USERS -->
                    <hr />
                    <h5>User Account</h5>
                    <div class="mb-3">
                        <label class="form-label">Username</label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Password</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Role</label>
                        <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Select role" Value="" />
                            <asp:ListItem Text="Doctor" Value="DOCTOR" />
                            <asp:ListItem Text="Nurse" Value="NURSE" />
                            <asp:ListItem Text="Receptionist" Value="RECEPTIONIST" />
                            <asp:ListItem Text="Lab Technician" Value="LAB_TECHNICIAN" />
                        </asp:DropDownList>
                    </div>

                    <asp:Button ID="btnAddEmp" runat="server" Text="Add Employee" CssClass="btn btn-primary" OnClick="btnAddEmp_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>