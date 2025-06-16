Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient

Partial Class Manage_PatientTests
    Inherits System.Web.UI.Page

    Private ReadOnly cs As String = ConfigurationManager.ConnectionStrings("Hospital_Management_SystemConnectionString").ConnectionString

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadTests()
        End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Response.Redirect("Receptionist_Dashboard.aspx")
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        LoadTests(txtSearch.Text.Trim())
    End Sub

    Private Sub LoadTests(Optional searchTerm As String = "")
        Dim query As String = "SELECT TEST_ID, TEST_NAME, TEST_COST FROM TEST"

        If Not String.IsNullOrEmpty(searchTerm) Then
            query &= " WHERE TEST_ID LIKE @term OR TEST_NAME LIKE @term"
        End If

        Using conn As New SqlConnection(cs), cmd As New SqlCommand(query, conn)
            If Not String.IsNullOrEmpty(searchTerm) Then
                cmd.Parameters.AddWithValue("@term", "%" & searchTerm & "%")
            End If

            Dim dt As New DataTable()
            Using da As New SqlDataAdapter(cmd)
                da.Fill(dt)
            End Using

            gvTests.DataSource = dt
            gvTests.DataBind()
        End Using
    End Sub

End Class