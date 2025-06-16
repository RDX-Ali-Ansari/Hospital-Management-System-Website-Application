<%@ Page Language="VB" AutoEventWireup="true" CodeFile="Patient_Dashboard.aspx.vb" Inherits="Patient_Dashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Patient Dashboard</title>
  <meta name="viewport" content="width=device-width, initial-scale=1" />
  <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/css/bootstrap.min.css"
        rel="stylesheet" />
  <style>
    body { background-color: #f4f7f9; }
    .dashboard { max-width: 1200px; margin: 30px auto; }
    .card { box-shadow: 0 2px 8px rgba(0,0,0,0.05); }
    .card-header { background-color: #0d6efd; color: #fff; }
    .card-body { max-height: 300px; overflow-y: auto; }
    .form-control[readonly] { background-color: #e9ecef; }
  </style>
</head>
<body>
  <form runat="server" class="dashboard">
    <asp:Panel ID="pnlError" runat="server" CssClass="alert alert-danger" Visible="False">
      <asp:Literal ID="litError" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlSuccess" runat="server" CssClass="alert alert-success" Visible="False">
      <asp:Literal ID="litSuccess" runat="server" />
    </asp:Panel>

    <div class="row g-4">
      <!-- Profile / Edit Card -->
      <div class="col-lg-4">
        <div class="card">
          <div class="card-header">
            <h5 class="mb-0">My Profile</h5>
          </div>
          <div class="card-body">
            <div class="mb-3">
              <label class="form-label">Patient ID</label>
              <asp:TextBox ID="txtPatientId" runat="server" CssClass="form-control" ReadOnly="True" />
            </div>
            <div class="mb-3">
              <label class="form-label">Name</label>
              <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
              <label class="form-label">Age</label>
              <asp:TextBox ID="txtAge" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
              <label class="form-label">Gender</label>
              <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-select">
                <asp:ListItem Text="M" Value="M" />
                <asp:ListItem Text="F" Value="F" />
              </asp:DropDownList>
            </div>
            <div class="mb-3">
              <label class="form-label">Contact</label>
              <asp:TextBox ID="txtContact" runat="server" CssClass="form-control" />
            </div>
            <div class="text-end">
              <asp:Button ID="btnUpdateProfile" runat="server" CssClass="btn btn-primary"
                          Text="Update Profile" OnClick="btnUpdateProfile_Click" />
            </div>
          </div>
        </div>
      </div>

      <!-- Appointments Card -->
      <div class="col-lg-8">
        <div class="card">
          <div class="card-header">
            <h5 class="mb-0">Upcoming Appointments</h5>
          </div>
          <div class="card-body p-0">
            <asp:GridView ID="gvAppointments" runat="server"
                          CssClass="table table-hover mb-0"
                          AutoGenerateColumns="False">
              <Columns>
                <asp:BoundField DataField="AppointmentDate" HeaderText="Date"
                                DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                <asp:BoundField DataField="Disease" HeaderText="Disease" />
                <asp:BoundField DataField="MedicineName" HeaderText="Prescription" />
                <asp:BoundField DataField="Dosage" HeaderText="Dosage" />
                <asp:BoundField DataField="Duration" HeaderText="Duration" />
              </Columns>
              <EmptyDataTemplate>
                <div class="text-center text-muted py-3">No upcoming appointments.</div>
              </EmptyDataTemplate>
            </asp:GridView>
          </div>
        </div>
      </div>

      <!-- Tests Card -->
      <div class="col-lg-6">
        <div class="card">
          <div class="card-header">
            <h5 class="mb-0">Recent Test Results</h5>
          </div>
          <div class="card-body p-0">
            <asp:GridView ID="gvTests" runat="server"
                          CssClass="table table-hover mb-0"
                          AutoGenerateColumns="False">
              <Columns>
                <asp:BoundField DataField="TestName" HeaderText="Test" />
                <asp:BoundField DataField="TestDate" HeaderText="Date"
                                DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="TestResult" HeaderText="Result" />
              </Columns>
              <EmptyDataTemplate>
                <div class="text-center text-muted py-3">No test results found.</div>
              </EmptyDataTemplate>
            </asp:GridView>
          </div>
        </div>
      </div>

      <!-- Doctors Card -->
      <div class="col-lg-6">
        <div class="card">
          <div class="card-header">
            <h5 class="mb-0">My Doctors</h5>
          </div>
          <div class="card-body p-0">
            <asp:GridView ID="gvDoctors" runat="server"
                          CssClass="table table-hover mb-0"
                          AutoGenerateColumns="False">
              <Columns>
                <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="Doctor" />
                <asp:BoundField DataField="EMPLOYEE_DEPARTMENT" HeaderText="Department" />
                <asp:BoundField DataField="EMPLOYEE_DESIGNATION" HeaderText="Designation" />
                <asp:BoundField DataField="QUALIFICATION" HeaderText="Qualification" />
                <asp:BoundField DataField="ROOM_ID" HeaderText="Room #" />
              </Columns>
              <EmptyDataTemplate>
                <div class="text-center text-muted py-3">No doctors found.</div>
              </EmptyDataTemplate>
            </asp:GridView>
          </div>
        </div>
      </div>
    </div>
  </form>

  <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/js/bootstrap.bundle.min.js">
  </script>
</body>
</html>