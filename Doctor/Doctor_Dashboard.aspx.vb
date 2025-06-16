Imports System
Imports System.Data
Imports System.Data.SqlClient

Partial Class Doctor_Dashboard
    Inherits System.Web.UI.Page

    Private ReadOnly Property DoctorID As Integer
        Get
            If Session("UserID") IsNot Nothing Then
                Return Convert.ToInt32(Session("UserID"))
            Else
                Return 0
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If DoctorID > 0 Then
                LoadAppointments()
            Else
                Response.Redirect("Login.aspx")
            End If
        End If
    End Sub

    Private Sub LoadAppointments()
        Dim connString As String = System.Configuration.ConfigurationManager.ConnectionStrings("HMSConnectionString").ConnectionString
        Using conn As New SqlConnection(connString)
            Dim sql As String = "SELECT a.AppointmentID, p.FullName AS PatientName, a.AppointmentDate, r.RoomNumber, a.Status " &
                                "FROM Appointments a " &
                                "JOIN Patients p ON a.PatientID = p.PatientID " &
                                "LEFT JOIN Rooms r ON a.RoomID = r.RoomID " &
                                "WHERE a.DoctorID = @DoctorID " &
                                "ORDER BY a.AppointmentDate DESC"
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@DoctorID", DoctorID)
                Dim dt As New DataTable()
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
                gvAppointments.DataSource = dt
                gvAppointments.DataBind()
            End Using
        End Using
    End Sub
End Class