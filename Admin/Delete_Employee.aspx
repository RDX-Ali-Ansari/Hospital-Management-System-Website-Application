<%@ Page Language="VB" AutoEventWireup="true" CodeFile="Delete_Employee.aspx.vb" Inherits="Delete_Employee" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Remove Employee</title>
  <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/css/bootstrap.min.css" rel="stylesheet" />
  <style>
    body { background-color: #f0f2f5; }
    .container { max-width: 700px; margin-top: 50px; }
    .card { box-shadow: 0 2px 8px rgba(0,0,0,0.1); }
    .card-header { background-color: #dc3545; color: white; }
  </style>
</head>
<body>
  <form runat="server">
    <div class="container">
      <div class="card">
        <div class="card-header">
          <h5 class="mb-0">Remove Employee</h5>
        </div>
        <div class="card-body">
          <div class="mb-3">
            <label class="form-label">Enter Employee ID</label>
            <asp:TextBox ID="txtEmpId" runat="server" CssClass="form-control" />
          </div>
          <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-secondary mb-3" Text="Search Employee" OnClick="btnSearch_Click" />
          <asp:Panel ID="pnlEmpInfo" runat="server" Visible="False" CssClass="mt-3">
            <h6>Employee Info</h6>
            <p><strong>Name:</strong> <asp:Label ID="lblName" runat="server" /></p>
            <p><strong>Department:</strong> <asp:Label ID="lblDept" runat="server" /></p>
            <p><strong>Designation:</strong> <asp:Label ID="lblDesig" runat="server" /></p>
            <asp:Button ID="btnDelete" runat="server" Text="Delete Employee" CssClass="btn btn-danger" OnClick="btnDelete_Click" />
          </asp:Panel>
          <asp:Literal ID="litMsg" runat="server" />
        </div>
      </div>
    </div>
  </form>
</body>
</html>