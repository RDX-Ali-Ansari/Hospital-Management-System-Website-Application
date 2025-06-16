Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient

Partial Class Manage_Rooms
    Inherits System.Web.UI.Page

    Private ReadOnly cs As String = ConfigurationManager.ConnectionStrings("Hospital_Management_SystemConnectionString").ConnectionString

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadRooms()
        End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Response.Redirect("Receptionist_Dashboard.aspx")
    End Sub

    Private Sub LoadRooms(Optional searchTerm As String = "")
        Dim query As String = "SELECT ROOM_ID, ROOM_TYPE, ROOM_CAPACITY, ROOM_AVAILABILITY FROM ROOMS"

        If Not String.IsNullOrWhiteSpace(searchTerm) Then
            query &= " WHERE ROOM_ID LIKE @term OR ROOM_TYPE LIKE @term"
        End If

        Using conn As New SqlConnection(cs), cmd As New SqlCommand(query, conn)
            If Not String.IsNullOrWhiteSpace(searchTerm) Then
                cmd.Parameters.AddWithValue("@term", "%" & searchTerm & "%")
            End If

            Dim dt As New DataTable()
            Using da As New SqlDataAdapter(cmd)
                da.Fill(dt)
            End Using

            gvRooms.DataSource = dt
            gvRooms.DataBind()
        End Using
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        LoadRooms(txtSearch.Text.Trim())
    End Sub
End Class