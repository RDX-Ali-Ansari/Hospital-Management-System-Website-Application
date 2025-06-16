<%@ Page Language="VB" AutoEventWireup="true" CodeFile="Receptionist_Dashboard.aspx.vb" Inherits="Receptionist_Dashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Receptionist Dashboard</title>
  <meta name="viewport" content="width=device-width, initial-scale=1" />
  <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/css/bootstrap.min.css" rel="stylesheet" />
  <style>
    body { background-color: #f4f7f9; }
    .dashboard { max-width: 1000px; margin: 50px auto; }
    .dashboard-header {text-align: center; margin-bottom: 40px; }
    .card { border: none; border-radius: 12px; box-shadow: 0 4px 12px rgba(0,0,0,0.05); transition: transform .2s; }
    .card:hover { transform: translateY(-5px); }
    .card-body { text-align: center; padding: 2rem; }
    .card-icon { font-size: 2.5rem; color: #0d6efd; margin-bottom: .5rem; }
    .card-title { font-size: 1.2rem; font-weight: 600; }
  </style>
</head>
<body>
  <form runat="server" class="dashboard">
    <div class="row g-4">
      <div class="dashboard-header">
        <h2 class="fw-bold">Receptionist Dashboard</h2>
        <p class="text-muted">Choose an action to perform</p>
      </div>
      
      <!-- Manage Patients -->
      <div class="col-md-4">
        <asp:LinkButton ID="lnkPatients" runat="server" OnClick="lnkPatients_Click" CssClass="text-decoration-none">
          <div class="card">
            <div class="card-body">
              <div class="card-icon">👥</div>
              <div class="card-title">Manage Patients</div>
            </div>
          </div>
        </asp:LinkButton>
      </div>

      <!-- Manage Rooms -->
      <div class="col-md-4">
        <asp:LinkButton ID="lnkRooms" runat="server" OnClick="lnkRooms_Click" CssClass="text-decoration-none">
          <div class="card">
            <div class="card-body">
              <div class="card-icon">🚪</div>
              <div class="card-title">Manage Rooms</div>
            </div>
          </div>
        </asp:LinkButton>
      </div>

      <!-- Manage Appointments -->
      <div class="col-md-4">
        <asp:LinkButton ID="lnkAppointments" runat="server" OnClick="lnkAppointments_Click" CssClass="text-decoration-none">
          <div class="card">
            <div class="card-body">
              <div class="card-icon">📅</div>
              <div class="card-title">Manage Appointments</div>
            </div>
          </div>
        </asp:LinkButton>
      </div>

      <!-- Manage Patient Tests -->
      <div class="col-md-4">
        <asp:LinkButton ID="lnkTests" runat="server" OnClick="lnkTests_Click" CssClass="text-decoration-none">
          <div class="card">
            <div class="card-body">
              <div class="card-icon">🧪</div>
              <div class="card-title">Manage Tests</div>
            </div>
          </div>
        </asp:LinkButton>
      </div>

      <!-- Manage Admissions
      <div class="col-md-4">
        <asp:LinkButton ID="lnkAdmissions" runat="server" OnClick="lnkAdmissions_Click" CssClass="text-decoration-none">
          <div class="card">
            <div class="card-body">
              <div class="card-icon">🏥</div>
              <div class="card-title">Manage Admissions</div>
            </div>
          </div>
        </asp:LinkButton>
      </div>

      <!-- Manage Invoices 
      <div class="col-md-4">
        <asp:LinkButton ID="lnkInvoices" runat="server" OnClick="lnkInvoices_Click" CssClass="text-decoration-none">
          <div class="card">
            <div class="card-body">
              <div class="card-icon">💳</div>
              <div class="card-title">Manage Invoices</div>
            </div>
          </div>
        </asp:LinkButton>
      </div>
          -->

    </div>
  </form>
  <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/js/bootstrap.bundle.min.js"></script>
</body>
</html>