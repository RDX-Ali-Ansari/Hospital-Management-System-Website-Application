Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient

Partial Class Manage_Appointments
    Inherits System.Web.UI.Page

    Private ReadOnly cs As String = ConfigurationManager.ConnectionStrings("Hospital_Management_SystemConnectionString").ConnectionString

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then LoadAppointments()
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Response.Redirect("Receptionist_Dashboard.aspx")
    End Sub

    Protected Sub btnSearchDoctors_Click(sender As Object, e As EventArgs)
        Dim dept As String = txtDepartmentSearch.Text.Trim()
        If dept <> "" Then
            LoadDoctorsByDepartment(dept)
        End If
    End Sub

    Private Sub LoadDoctorsByDepartment(dept As String)
        Dim query As String = "SELECT EMPLOYEE_ID, EMPLOYEE_NAME, EMPLOYEE_DEPARTMENT, EMPLOYEE_DESIGNATION, ROOM_ID FROM EMPLOYEE WHERE (EMPLOYEE_DESIGNATION LIKE '%Doctor%') AND EMPLOYEE_DEPARTMENT LIKE @dept"

        Using conn As New SqlConnection(cs), cmd As New SqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@dept", "%" & dept & "%") ' partial match
            Dim dt As New DataTable()
            Using da As New SqlDataAdapter(cmd)
                da.Fill(dt)
            End Using
            gvDoctors.DataSource = dt
            gvDoctors.DataBind()
        End Using
    End Sub


    Private Sub LoadAppointments(Optional search As String = "")
        Dim query As String = "SELECT * FROM APPOINTMENT"
        If Not String.IsNullOrWhiteSpace(search) Then
            query &= " WHERE PATIENT_ID LIKE @term OR EMPLOYEE_ID LIKE @term"
        End If

        Using conn As New SqlConnection(cs), cmd As New SqlCommand(query, conn)
            If Not String.IsNullOrWhiteSpace(search) Then
                cmd.Parameters.AddWithValue("@term", "%" & search & "%")
            End If
            Dim dt As New DataTable()
            Using da As New SqlDataAdapter(cmd)
                da.Fill(dt)
            End Using
            gvAppointments.DataSource = dt
            gvAppointments.DataBind()
        End Using
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        lblMessage.Text = ""

        If txtAppointmentID.Text = "" OrElse txtEmployeeID.Text = "" OrElse txtPatientID.Text = "" OrElse txtDate.Text = "" Then
            lblMessage.Text = "All fields are required."
            Return
        End If

        Try
            Using conn As New SqlConnection(cs)
                conn.Open()
                Dim checkCmd As New SqlCommand("SELECT COUNT(*) FROM APPOINTMENT WHERE APPOINTMENT_ID = @id", conn)
                checkCmd.Parameters.AddWithValue("@id", txtAppointmentID.Text.Trim())
                If CInt(checkCmd.ExecuteScalar()) > 0 Then
                    lblMessage.Text = "Appointment ID already exists."
                    Return
                End If

                Dim insertCmd As New SqlCommand("INSERT INTO APPOINTMENT (APPOINTMENT_ID, EMPLOYEE_ID, PATIENT_ID, APPOINTMENT_DATE) VALUES (@aid, @eid, @pid, @dt)", conn)
                insertCmd.Parameters.AddWithValue("@aid", txtAppointmentID.Text.Trim())
                insertCmd.Parameters.AddWithValue("@eid", txtEmployeeID.Text.Trim())
                insertCmd.Parameters.AddWithValue("@pid", txtPatientID.Text.Trim())
                insertCmd.Parameters.AddWithValue("@dt", DateTime.Parse(txtDate.Text.Trim()))
                insertCmd.ExecuteNonQuery()
            End Using
            lblMessage.CssClass = "text-success"
            lblMessage.Text = "Appointment added successfully."
            LoadAppointments()
        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        gvAppointments.EditIndex = -1
        LoadAppointments(txtSearch.Text.Trim())
    End Sub

    Protected Sub gvAppointments_RowEditing(sender As Object, e As GridViewEditEventArgs)
        gvAppointments.EditIndex = e.NewEditIndex
        LoadAppointments()
    End Sub

    Protected Sub gvAppointments_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs)
        gvAppointments.EditIndex = -1
        LoadAppointments()
    End Sub

    Protected Sub gvAppointments_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)
        Dim row = gvAppointments.Rows(e.RowIndex)
        Dim id = gvAppointments.DataKeys(e.RowIndex).Value.ToString()
        Dim eid = CType(row.Cells(1).Controls(0), TextBox).Text
        Dim pid = CType(row.Cells(2).Controls(0), TextBox).Text
        Dim dt = CType(row.Cells(3).Controls(0), TextBox).Text

        Try
            Using conn As New SqlConnection(cs)
                conn.Open()
                Dim updateCmd As New SqlCommand("UPDATE APPOINTMENT SET EMPLOYEE_ID=@e, PATIENT_ID=@p, APPOINTMENT_DATE=@d WHERE APPOINTMENT_ID=@id", conn)
                updateCmd.Parameters.AddWithValue("@e", eid)
                updateCmd.Parameters.AddWithValue("@p", pid)
                updateCmd.Parameters.AddWithValue("@d", DateTime.Parse(dt))
                updateCmd.Parameters.AddWithValue("@id", id)
                updateCmd.ExecuteNonQuery()
            End Using
            gvAppointments.EditIndex = -1
            LoadAppointments()
        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try
    End Sub

    Protected Sub gvAppointments_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim id = gvAppointments.DataKeys(e.RowIndex).Value.ToString()
        Try
            Using conn As New SqlConnection(cs)
                conn.Open()
                Dim delCmd As New SqlCommand("DELETE FROM APPOINTMENT WHERE APPOINTMENT_ID=@id", conn)
                delCmd.Parameters.AddWithValue("@id", id)
                delCmd.ExecuteNonQuery()
            End Using
            LoadAppointments()
        Catch ex As Exception
            lblMessage.Text = "Error deleting: " & ex.Message
        End Try
    End Sub
End Class