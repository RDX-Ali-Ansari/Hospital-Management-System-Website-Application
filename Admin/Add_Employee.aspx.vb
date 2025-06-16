Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Security.Cryptography
Imports System.Text

Partial Class Add_Employee
    Inherits System.Web.UI.Page

    Private ReadOnly _cs As String = ConfigurationManager.ConnectionStrings("Hospital_Management_SystemConnectionString").ConnectionString

    Protected Sub btnAddEmp_Click(sender As Object, e As EventArgs)
        Try
            Using conn As New SqlConnection(_cs)
                conn.Open()
                Dim trans = conn.BeginTransaction()

                Try
                    ' EMPLOYEE
                    Dim sqlEmp = "INSERT INTO EMPLOYEE (EMPLOYEE_ID, EMPLOYEE_NAME, EMPLOYEE_DEPARTMENT, EMPLOYEE_DESIGNATION, ROOM_ID) VALUES (@id, @name, @dept, @desig, @room)"
                    Using cmd As New SqlCommand(sqlEmp, conn, trans)
                        cmd.Parameters.AddWithValue("@id", txtEmpId.Text.Trim())
                        cmd.Parameters.AddWithValue("@name", txtEmpName.Text.Trim())
                        cmd.Parameters.AddWithValue("@dept", txtDept.Text.Trim())
                        cmd.Parameters.AddWithValue("@desig", txtDesig.Text.Trim())
                        cmd.Parameters.AddWithValue("@room", If(txtRoomId.Text.Trim() = "", DBNull.Value, txtRoomId.Text.Trim()))
                        cmd.ExecuteNonQuery()
                    End Using

                    ' CONTACT
                    Dim sqlContact = "INSERT INTO EMPLOYEE_CONTACT (EMPLOYEE_ID, EMPLOYEE_CONTACT_NUMBER) VALUES (@id, @contact)"
                    Using cmd As New SqlCommand(sqlContact, conn, trans)
                        cmd.Parameters.AddWithValue("@id", txtEmpId.Text.Trim())
                        cmd.Parameters.AddWithValue("@contact", txtContact.Text.Trim())
                        cmd.ExecuteNonQuery()
                    End Using

                    ' QUALIFICATION
                    Dim sqlQual = "INSERT INTO EMPLOYEE_QUALIFICATION (EMPLOYEE_ID, QUALIFICATION) VALUES (@id, @qual)"
                    Using cmd As New SqlCommand(sqlQual, conn, trans)
                        cmd.Parameters.AddWithValue("@id", txtEmpId.Text.Trim())
                        cmd.Parameters.AddWithValue("@qual", txtQual.Text.Trim())
                        cmd.ExecuteNonQuery()
                    End Using

                    ' USERS
                    Dim sqlUser = "INSERT INTO USERS (USERNAME, PASSWORD, EMAIL, USER_TYPE, RELATED_ID) VALUES (@username, @password, @email, @role, @related)"
                    Using cmd As New SqlCommand(sqlUser, conn, trans)
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim())
                        cmd.Parameters.AddWithValue("@password", HashPassword(txtPassword.Text.Trim()))
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim())
                        cmd.Parameters.AddWithValue("@role", ddlRole.SelectedValue)
                        cmd.Parameters.AddWithValue("@related", txtEmpId.Text.Trim())
                        cmd.ExecuteNonQuery()
                    End Using

                    trans.Commit()
                    Response.Write("<script>alert('Employee added successfully.');</script>")
                    ClearFields()

                Catch ex As Exception
                    trans.Rollback()
                    Response.Write("<script>alert('Error: " & ex.Message & "');</script>")
                End Try
            End Using
        Catch ex As Exception
            Response.Write("<script>alert('Connection error: " & ex.Message & "');</script>")
        End Try
    End Sub

    Private Sub ClearFields()
        txtEmpId.Text = ""
        txtEmpName.Text = ""
        txtDept.Text = ""
        txtDesig.Text = ""
        txtRoomId.Text = ""
        txtContact.Text = ""
        txtQual.Text = ""
        txtUsername.Text = ""
        txtEmail.Text = ""
        txtPassword.Text = ""
        ddlRole.SelectedIndex = 0
    End Sub

    Private Function HashPassword(pwd As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pwd))
            Dim sb As New StringBuilder()
            For Each b In bytes
                sb.Append(b.ToString("x2"))
            Next
            Return sb.ToString()
        End Using
    End Function
End Class