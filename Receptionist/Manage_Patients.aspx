<%@ Page Language="VB" AutoEventWireup="true" CodeFile="Manage_Patients.aspx.vb" Inherits="Manage_Patients" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Patients</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="container mt-5">
        
        <div class="mb-3">
            <asp:Button ID="btnBack" runat="server" Text="Back to Dashboard" CssClass="btn btn-secondary" OnClick="btnBack_Click" />
        </div>
        <h3 class="mb-4">Manage Patients</h3>

        <!-- Search -->
        <div class="mb-3 row">
            <div class="col-md-4">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search by Name or ID"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
            </div>
        </div>

        <!-- Add New Patient -->
        <div class="row g-2 mb-3">
            <div class="col-md-2"><asp:TextBox ID="txtId" runat="server" CssClass="form-control" placeholder="Patient ID" /></div>
            <div class="col-md-2"><asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Name" /></div>
            <div class="col-md-1"><asp:TextBox ID="txtAge" runat="server" CssClass="form-control" placeholder="Age" TextMode="Number" /></div>
            <div class="col-md-2">
                <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Select Gender" Value="" />
                    <asp:ListItem Text="Male" Value="M" />
                    <asp:ListItem Text="Female" Value="F" />
                </asp:DropDownList>
            </div>
            <div class="col-md-3"><asp:TextBox ID="txtContact" runat="server" CssClass="form-control" placeholder="Contact No." /></div>
            <div class="col-md-2">
                <asp:Button ID="btnAdd" runat="server" Text="Add Patient" CssClass="btn btn-success w-100" OnClick="btnAdd_Click"/>
            </div>
        </div>

        <!-- Grid -->
        <asp:GridView ID="gvPatients" runat="server" CssClass="table table-bordered"
                      AutoGenerateColumns="False" DataKeyNames="PATIENT_ID"
                      OnRowEditing="gvPatients_RowEditing"
                      OnRowUpdating="gvPatients_RowUpdating"
                      OnRowCancelingEdit="gvPatients_RowCancelingEdit"
                      OnRowDeleting="gvPatients_RowDeleting">
            <Columns>
                <asp:BoundField DataField="PATIENT_ID" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="PATIENT_NAME" HeaderText="Name" />
                <asp:BoundField DataField="PATIENT_AGE" HeaderText="Age" />
                <asp:BoundField DataField="GENDER" HeaderText="Gender" />
                <asp:BoundField DataField="CONTACT" HeaderText="Contact" />
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>

        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger fw-bold"></asp:Label>

    </form>
</body>
</html>