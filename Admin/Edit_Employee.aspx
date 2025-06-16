<%@ Page Language="VB" AutoEventWireup="true" CodeFile="Edit_Employee.aspx.vb" Inherits="Edit_Employee" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Edit Employee</title>
  <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/css/bootstrap.min.css" rel="stylesheet" />
  <style>
    body { background-color: #f0f2f5; }
    .container { max-width: 700px; margin-top: 50px; }
    .card { box-shadow: 0 2px 8px rgba(0,0,0,0.1); }
    .card-header { background-color: #ffc107; color: #212529; }
  </style>
</head>
<body>
  <form runat="server">
    <div class="container">
      <asp:Panel ID="pnlError" runat="server" CssClass="alert alert-danger" Visible="False">
        <asp:Literal ID="litError" runat="server" />
      </asp:Panel>
      <asp:Panel ID="pnlSuccess" runat="server" CssClass="alert alert-success" Visible="False">
        <asp:Literal ID="litSuccess" runat="server" />
      </asp:Panel>

      <div class="card">
        <div class="card-header">
          <h5 class="mb-0">Edit Employee</h5>
        </div>
        <div class="card-body">
          <!-- Search -->
          <div class="mb-3 row">
            <div class="col-md-8">
              <asp:TextBox ID="txtSearchEmpId" runat="server" CssClass="form-control"
                           placeholder="Enter Employee ID to load" />
            </div>
            <div class="col-md-4">
              <asp:Button ID="btnLoad" runat="server" CssClass="btn btn-warning w-100"
                          Text="Load Employee" OnClick="btnLoad_Click" />
            </div>
          </div>

          <!-- Editable Fields -->
          <asp:Panel ID="pnlEdit" runat="server" Visible="False">
            <!-- EMPLOYEE -->
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

            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-warning"
                        Text="Save Changes" OnClick="btnUpdate_Click" />
          </asp:Panel>
        </div>
      </div>
    </div>
  </form>
</body>
</html>