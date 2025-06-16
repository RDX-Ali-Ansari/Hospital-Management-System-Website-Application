<%@ Page Language="VB" AutoEventWireup="true" CodeFile="Admin_Dashboard.aspx.vb" Inherits="Admin_Dashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Admin - Employee Management Dashboard</title>
  <meta name="viewport" content="width=device-width, initial-scale=1" />
  <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/css/bootstrap.min.css" rel="stylesheet" />
  <style>
    body {
      background-color: #f0f2f5;
      font-family: 'Segoe UI', sans-serif;
    }

    .dashboard-container {
      max-width: 900px;
      margin: 60px auto;
      padding: 20px;
    }

    .dashboard-header {
      text-align: center;
      margin-bottom: 40px;
    }

    .action-card {
      cursor: pointer;
      transition: transform 0.2s ease, box-shadow 0.2s ease;
      border: none;
      border-radius: 12px;
      overflow: hidden;
      background: #ffffff;
      box-shadow: 0 4px 12px rgba(0,0,0,0.1);
    }

    .action-card:hover {
      transform: translateY(-5px);
      box-shadow: 0 6px 20px rgba(0,0,0,0.15);
    }

    .card-icon {
      font-size: 48px;
      color: #0d6efd;
      margin-bottom: 10px;
    }

    .card-title {
      font-size: 20px;
      font-weight: 600;
    }

    .card-subtitle {
      color: #6c757d;
    }
  </style>
</head>
<body>
  <form id="form1" runat="server">
    <div class="dashboard-container">
      <div class="dashboard-header">
        <h2 class="fw-bold">Employee Management</h2>
        <p class="text-muted">Choose an action to perform</p>
      </div>

      <div class="row g-4 justify-content-center">
      <!-- Add Employee Card -->
      <div class="col-md-5">
        <asp:LinkButton runat="server" ID="btnAddEmployee" OnClick="btnAddEmployee_Click" CssClass="text-decoration-none">
          <div class="card action-card text-center p-4">
            <div class="card-icon">➕</div>
            <div class="card-title">Add Employee</div>
            <div class="card-subtitle">Register new employee and user account</div>
          </div>
        </asp:LinkButton>
      </div>

      <!-- Remove Employee Card -->
      <div class="col-md-5">
        <asp:LinkButton runat="server" ID="btnDeleteEmployee" OnClick="btnDeleteEmployee_Click" CssClass="text-decoration-none">
          <div class="card action-card text-center p-4">
            <div class="card-icon">🗑️</div>
            <div class="card-title">Remove Employee</div>
            <div class="card-subtitle">Delete employee & related records</div>
          </div>
        </asp:LinkButton>
      </div>

      <!-- Edit Employee Card -->
      <div class="col-md-5">
        <asp:LinkButton runat="server" ID="btnEditEmployee" OnClick="btnEditEmployee_Click" CssClass="text-decoration-none">
          <div class="card action-card text-center p-4">
            <div class="card-icon">✏️</div>
            <div class="card-title">Edit Employee</div>
            <div class="card-subtitle">Update employee details and access</div>
          </div>
        </asp:LinkButton>
      </div>
    </div>


    </div>
  </form>

  <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/js/bootstrap.bundle.min.js"></script>
</body>
</html>