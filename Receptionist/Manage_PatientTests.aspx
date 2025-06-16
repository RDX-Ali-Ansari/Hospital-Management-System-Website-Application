<%@ Page Language="VB" AutoEventWireup="true" CodeFile="Manage_PatientTests.aspx.vb" Inherits="Manage_PatientTests" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Patient Tests</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="container mt-5">

        <div class="mt-4">
            <asp:Button ID="btnBack" runat="server" Text="Back to Dashboard" CssClass="btn btn-secondary" OnClick="btnBack_Click" />
        </div>
        <h3 class="mb-4">Manage Patient Tests</h3>

        <!-- Search -->
        <div class="mb-3 row">
            <div class="col-md-4">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search by Test Name or ID"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary w-100" OnClick="btnSearch_Click" />
            </div>
        </div>

        <!-- Grid -->
        <asp:GridView ID="gvTests" runat="server" CssClass="table table-bordered"
                      AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="TEST_ID" HeaderText="Test ID" />
                <asp:BoundField DataField="TEST_NAME" HeaderText="Test Name" />
                <asp:BoundField DataField="TEST_COST" HeaderText="Cost" DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>

    </form>
</body>
</html>