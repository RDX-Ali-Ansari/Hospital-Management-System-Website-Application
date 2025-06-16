Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Security.Cryptography
Imports System.Text

Partial Class Signup
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Nothing special on initial load
    End Sub

    Protected Sub btnSignup_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If Page.IsValid Then
            Try
                ' Only PATIENT registrations allowed here
                Dim userType As String = "PATIENT"

                ' Get connection string
                Dim connectionString As String = ConfigurationManager.ConnectionStrings("Hospital_Management_SystemConnectionString").ConnectionString
                Dim conn As New SqlConnection(connectionString)

                ' 1) Generate new patient ID
                Dim patientId As String = GenerateNewID("P", "SELECT MAX(PATIENT_ID) FROM PATIENT")

                ' 2) Insert into PATIENT
                Using cmdPatient As New SqlCommand(
                    "INSERT INTO PATIENT (PATIENT_ID, PATIENT_NAME, PATIENT_AGE, GENDER) " &
                    "VALUES (@PatientId, @Name, @Age, @Gender)", conn)

                    cmdPatient.Parameters.AddWithValue("@PatientId", patientId)
                    cmdPatient.Parameters.AddWithValue("@Name", txtPatientName.Text.Trim())
                    cmdPatient.Parameters.AddWithValue("@Age", Convert.ToInt32(txtPatientAge.Text))
                    cmdPatient.Parameters.AddWithValue("@Gender", ddlGender.SelectedValue)

                    conn.Open()
                    cmdPatient.ExecuteNonQuery()
                    conn.Close()
                End Using

                ' 3) Insert into PATIENT_CONTACT
                Using cmdContact As New SqlCommand(
                    "INSERT INTO PATIENT_CONTACT (PATIENT_ID, PATIENT_CONTACT_NUMBER) " &
                    "VALUES (@PatientId, @ContactNumber)", conn)

                    cmdContact.Parameters.AddWithValue("@PatientId", patientId)
                    cmdContact.Parameters.AddWithValue("@ContactNumber", txtContactNumber.Text.Trim())

                    conn.Open()
                    cmdContact.ExecuteNonQuery()
                    conn.Close()
                End Using

                ' 4) Create the USER record
                Dim hashedPassword As String = HashPassword(txtPassword.Text)

                Using cmdUser As New SqlCommand(
                    "INSERT INTO USERS (USERNAME, PASSWORD, EMAIL, USER_TYPE, RELATED_ID) " &
                    "VALUES (@Username, @Password, @Email, @UserType, @RelatedId)", conn)

                    cmdUser.Parameters.AddWithValue("@Username", txtUsername.Text.Trim())
                    cmdUser.Parameters.AddWithValue("@Password", hashedPassword)
                    cmdUser.Parameters.AddWithValue("@Email", txtEmail.Text.Trim())
                    cmdUser.Parameters.AddWithValue("@UserType", userType)
                    cmdUser.Parameters.AddWithValue("@RelatedId", patientId)

                    conn.Open()
                    cmdUser.ExecuteNonQuery()
                    conn.Close()
                End Using

                ' 5) Redirect to Login
                Response.Redirect("Login.aspx?registered=true")
            Catch ex As Exception
                lblMessage.Text = "Error: " & ex.Message
            End Try
        End If
    End Sub

    Private Function GenerateNewID(prefix As String, sqlQuery As String) As String
        Dim newId As String = String.Empty
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("Hospital_Management_SystemConnectionString").ConnectionString
        Dim conn As New SqlConnection(connectionString)

        Try
            Using cmd As New SqlCommand(sqlQuery, conn)
                conn.Open()
                Dim lastId As String = Convert.ToString(cmd.ExecuteScalar())

                If String.IsNullOrEmpty(lastId) Then
                    newId = prefix & "001"
                Else
                    Dim numericPart As String = lastId.Substring(prefix.Length)
                    Dim nextNumber As Integer = Convert.ToInt32(numericPart) + 1
                    newId = prefix & nextNumber.ToString().PadLeft(3, "0"c)
                End If
            End Using
        Catch ex As Exception
            Throw New Exception("Error generating new ID: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try

        Return newId
    End Function

    Private Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(password))
            Dim builder As New StringBuilder()
            For Each b As Byte In bytes
                builder.Append(b.ToString("x2"))
            Next
            Return builder.ToString()
        End Using
    End Function
End Class