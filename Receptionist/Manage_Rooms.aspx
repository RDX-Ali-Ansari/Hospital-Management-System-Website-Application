<%@ Page Language="VB" AutoEventWireup="true" CodeFile="Manage_Rooms.aspx.vb" Inherits="Manage_Rooms" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Rooms</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="container mt-5">
        
        <div class="mt-4">
            <asp:Button ID="btnBack" runat="server" Text="Back to Dashboard" CssClass="btn btn-secondary" OnClick="btnBack_Click" />
        </div>
        <h3 class="mb-4">Manage Rooms</h3>

        <!-- Search -->
        <div class="mb-3 row">
            <div class="col-md-4">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search by Room ID or Type"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
            </div>
        </div>

        <!-- Rooms Grid -->
        <asp:GridView ID="gvRooms" runat="server" CssClass="table table-bordered"
                      AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ROOM_ID" HeaderText="Room ID" />
                <asp:BoundField DataField="ROOM_TYPE" HeaderText="Type" />
                <asp:BoundField DataField="ROOM_CAPACITY" HeaderText="Capacity" />
                <asp:BoundField DataField="ROOM_AVAILABILITY" HeaderText="Availability" />
            </Columns>
        </asp:GridView>

        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger fw-bold"></asp:Label>       
    </form>
</body>
</html>