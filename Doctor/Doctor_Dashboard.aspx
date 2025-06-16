<%@ Page Language="vb" AutoEventWireup="false" CodeFile="Doctor_Dashboard.aspx.vb" Inherits="Doctor_Dashboard" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Doctor Dashboard</title>
    <style>
        .dashboard-container { width: 80%; margin: 20px auto; font-family: Arial, sans-serif; }
        .dashboard-header { text-align: center; margin-bottom: 20px; }
        .gridview { width: 100%; border: 1px solid #ccc; border-collapse: collapse; }
        .gridview th, .gridview td { padding: 8px; border: 1px solid #ccc; text-align: left; }
        .gridview th { background-color: #f4f4f4; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="dashboard-container">
            <h2 class="dashboard-header">My Appointments</h2>
            <asp:GridView ID="gvAppointments" runat="server" CssClass="gridview" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="AppointmentID" HeaderText="ID" />
                    <asp:BoundField DataField="PatientName" HeaderText="Patient" />
                    <asp:BoundField DataField="AppointmentDate" HeaderText="Date & Time" DataFormatString="{0:MM/dd/yyyy HH:mm}" />
                    <asp:BoundField DataField="RoomNumber" HeaderText="Room" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>