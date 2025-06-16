Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web.Security

Partial Class Patient_Dashboard
    Inherits System.Web.UI.Page

    Private ReadOnly _connString As String =
        ConfigurationManager.ConnectionStrings("Hospital_Management_SystemConnectionString").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            ' Read patient ID from auth ticket
            Dim ticket = FormsAuthentication.Decrypt(Request.Cookies(FormsAuthentication.FormsCookieName).Value)
            Dim parts = ticket.UserData.Split("|"c)
            Dim patientId = parts(2)

            ' Populate controls
            txtPatientId.Text = patientId
            LoadProfile(patientId)
            LoadAppointments(patientId)
            LoadTests(patientId)
            LoadDoctors(patientId)
        End If
    End Sub

    Private Sub LoadProfile(patientId As String)
        Const sql As String =
            "SELECT P.PATIENT_NAME, P.PATIENT_AGE, P.GENDER, C.PATIENT_CONTACT_NUMBER " &
            "FROM PATIENT P " &
            "JOIN PATIENT_CONTACT C ON P.PATIENT_ID = C.PATIENT_ID " &
            "WHERE P.PATIENT_ID = @PatientId;"

        Using conn As New SqlConnection(_connString),
              cmd As New SqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@PatientId", patientId)
            conn.Open()
            Using rdr = cmd.ExecuteReader()
                If rdr.Read() Then
                    txtName.Text = rdr("PATIENT_NAME").ToString()
                    txtAge.Text = rdr("PATIENT_AGE").ToString()
                    ddlGender.SelectedValue = rdr("GENDER").ToString()
                    txtContact.Text = rdr("PATIENT_CONTACT_NUMBER").ToString()
                End If
            End Using
        End Using
    End Sub

    Protected Sub btnUpdateProfile_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim patientId = txtPatientId.Text.Trim()

        ' Update PATIENT
        Const sqlUpdPat As String =
            "UPDATE PATIENT SET PATIENT_NAME = @Name, PATIENT_AGE = @Age, GENDER = @Gender " &
            "WHERE PATIENT_ID = @PatientId;"

        Using conn As New SqlConnection(_connString),
              cmd As New SqlCommand(sqlUpdPat, conn)
            cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim())
            cmd.Parameters.AddWithValue("@Age", Integer.Parse(txtAge.Text.Trim()))
            cmd.Parameters.AddWithValue("@Gender", ddlGender.SelectedValue)
            cmd.Parameters.AddWithValue("@PatientId", patientId)
            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()
        End Using

        ' Update PATIENT_CONTACT
        Const sqlUpdCont As String =
            "UPDATE PATIENT_CONTACT SET PATIENT_CONTACT_NUMBER = @Contact " &
            "WHERE PATIENT_ID = @PatientId;"

        Using conn As New SqlConnection(_connString),
              cmd As New SqlCommand(sqlUpdCont, conn)
            cmd.Parameters.AddWithValue("@Contact", txtContact.Text.Trim())
            cmd.Parameters.AddWithValue("@PatientId", patientId)
            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()
        End Using

        pnlSuccess.Visible = True
        litSuccess.Text = "Profile updated successfully."
    End Sub

    Private Sub LoadAppointments(patientId As String)
        Const sql As String =
            "SELECT A.APPOINTMENT_DATE AS AppointmentDate, D.DISEASE, " &
            "       P.MEDICINE_NAME AS MedicineName, P.DOSAGE, P.DURATION " &
            "FROM APPOINTMENT A " &
            "JOIN APPOINTMENT_DISEASE D ON A.APPOINTMENT_ID = D.APPOINTMENT_ID " &
            "JOIN APPOINTMENT_PERSCRIPTION P ON A.APPOINTMENT_ID = P.APPOINTMENT_ID " &
            "WHERE A.PATIENT_ID = @PatientId AND A.APPOINTMENT_DATE >= GETDATE() " &
            "ORDER BY A.APPOINTMENT_DATE;"

        BindGrid(sql, patientId, gvAppointments)
    End Sub

    Private Sub LoadTests(patientId As String)
        Const sql As String =
            "SELECT T.TEST_NAME AS TestName, PT.TEST_DATE AS TestDate, PT.TEST_RESULT AS TestResult " &
            "FROM PATIENT_TESTS PT " &
            "JOIN TEST T ON PT.TEST_ID = T.TEST_ID " &
            "WHERE PT.PATIENT_ID = @PatientId " &
            "ORDER BY PT.TEST_DATE DESC;"

        BindGrid(sql, patientId, gvTests)
    End Sub

    Private Sub LoadDoctors(patientId As String)
        Const sql As String =
            "SELECT DISTINCT E.EMPLOYEE_NAME, E.EMPLOYEE_DEPARTMENT, E.EMPLOYEE_DESIGNATION, " &
            "       Q.QUALIFICATION, E.ROOM_ID " &
            "FROM EMPLOYEE E " &
            "JOIN EMPLOYEE_QUALIFICATION Q ON E.EMPLOYEE_ID = Q.EMPLOYEE_ID " &
            "JOIN APPOINTMENT A ON E.EMPLOYEE_ID = A.EMPLOYEE_ID " &
            "WHERE A.PATIENT_ID = @PatientId;"

        BindGrid(sql, patientId, gvDoctors)
    End Sub

    Private Sub BindGrid(sql As String, patientId As String, grid As GridView)
        Dim dt As New DataTable()
        Using conn As New SqlConnection(_connString),
              da As New SqlDataAdapter(sql, conn)
            da.SelectCommand.Parameters.AddWithValue("@PatientId", patientId)
            da.Fill(dt)
        End Using
        grid.DataSource = dt
        grid.DataBind()
    End Sub
End Class
