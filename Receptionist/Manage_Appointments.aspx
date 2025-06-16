<%@ Page Language="VB" AutoEventWireup="true" CodeFile="Manage_Appointments.aspx.vb" Inherits="Manage_Appointments" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Appointments</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="container mt-5">

        <div class="mt-4">
            <asp:Button ID="btnBack" runat="server" Text="Back to Dashboard" CssClass="btn btn-secondary" OnClick="btnBack_Click" />
        </div>
        <h3 class="mb-4">Manage Appointments</h3>

        <hr />
        <h4 class="mt-5">Search Doctors by Department</h4>
        <div class="row mb-3">
            <div class="col-md-4">
                <asp:TextBox ID="txtDepartmentSearch" runat="server" CssClass="form-control" placeholder="Enter Department" />
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnSearchDoctors" runat="server" Text="Search Doctors" CssClass="btn btn-primary" OnClick="btnSearchDoctors_Click"/>
            </div>
        </div>

        <asp:GridView ID="gvDoctors" runat="server" CssClass="table table-bordered"
                      AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="EMPLOYEE_ID" HeaderText="Doctor ID" />
                <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="Name" />
                <asp:BoundField DataField="EMPLOYEE_DEPARTMENT" HeaderText="Department" />
                <asp:BoundField DataField="EMPLOYEE_DESIGNATION" HeaderText="Designation" />
                <asp:BoundField DataField="ROOM_ID" HeaderText="Room ID" />
            </Columns>
        </asp:GridView>


        <div class="mb-3 row">
            <div class="col-md-4">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search by Patient or Doctor ID"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click"/>
            </div>
        </div>

        <!-- Add Appointment -->
        <div class="row g-2 mb-3">
            <div class="col-md-3"><asp:TextBox ID="txtAppointmentID" runat="server" CssClass="form-control" placeholder="Appointment ID" /></div>
            <div class="col-md-3"><asp:TextBox ID="txtEmployeeID" runat="server" CssClass="form-control" placeholder="Doctor ID" /></div>
            <div class="col-md-3"><asp:TextBox ID="txtPatientID" runat="server" CssClass="form-control" placeholder="Patient ID" /></div>
            <div class="col-md-3"><asp:TextBox ID="txtDate" runat="server" CssClass="form-control" TextMode="DateTimeLocal" /></div>
        </div>
        <div class="row mb-3">
            <div class="col-md-2">
                <asp:Button ID="btnAdd" runat="server" Text="Add Appointment" CssClass="btn btn-success w-100" OnClick="btnAdd_Click" />
            </div>
        </div>

        <!-- Grid -->
        <asp:GridView ID="gvAppointments" runat="server" CssClass="table table-bordered"
                      AutoGenerateColumns="False" DataKeyNames="APPOINTMENT_ID"
                      OnRowEditing="gvAppointments_RowEditing"
                      OnRowUpdating="gvAppointments_RowUpdating"
                      OnRowCancelingEdit="gvAppointments_RowCancelingEdit"
                      OnRowDeleting="gvAppointments_RowDeleting">
            <Columns>
                <asp:BoundField DataField="APPOINTMENT_ID" HeaderText="Appointment ID" ReadOnly="True" />
                <asp:BoundField DataField="EMPLOYEE_ID" HeaderText="Doctor ID" />
                <asp:BoundField DataField="PATIENT_ID" HeaderText="Patient ID" />
                <asp:BoundField DataField="APPOINTMENT_DATE" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>

        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger fw-bold" />
    </form>
</body>
</html>