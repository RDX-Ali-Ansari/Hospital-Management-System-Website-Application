Imports System.Data.SqlClient
Imports System.Configuration

Partial Class Delete_Employee
    Inherits System.Web.UI.Page

    Private ReadOnly _cs As String = ConfigurationManager.ConnectionStrings("Hospital_Management_SystemConnectionString").ConnectionString

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        pnlEmpInfo.Visible = False
        litMsg.Text = ""

        If txtEmpId.Text = "" Then
            litMsg.Text = "<div class='alert alert-warning'>Please enter Employee ID.</div>"
            Return
        End If

        Using conn As New SqlConnection(_cs),
              cmd As New SqlCommand("SELECT EMPLOYEE_NAME, EMPLOYEE_DEPARTMENT, EMPLOYEE_DESIGNATION FROM EMPLOYEE WHERE EMPLOYEE_ID = @id", conn)
            cmd.Parameters.AddWithValue("@id", txtEmpId.Text.Trim())

            conn.Open()
            Dim rdr = cmd.ExecuteReader()

            If rdr.Read() Then
                lblName.Text = rdr("EMPLOYEE_NAME").ToString()
                lblDept.Text = rdr("EMPLOYEE_DEPARTMENT").ToString()
                lblDesig.Text = rdr("EMPLOYEE_DESIGNATION").ToString()
                pnlEmpInfo.Visible = True
            Else
                litMsg.Text = "<div class='alert alert-danger'>Employee not found.</div>"
            End If
        End Using
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs)
        Dim empId = txtEmpId.Text.Trim()
        Try
            Using conn As New SqlConnection(_cs)
                conn.Open()
                Dim trans = conn.BeginTransaction()

                Try
                    ' USERS
                    Using cmd As New SqlCommand("DELETE FROM USERS WHERE RELATED_ID = @id", conn, trans)
                        cmd.Parameters.AddWithValue("@id", empId)
                        cmd.ExecuteNonQuery()
                    End Using

                    ' EMPLOYEE_CONTACT
                    Using cmd As New SqlCommand("DELETE FROM EMPLOYEE_CONTACT WHERE EMPLOYEE_ID = @id", conn, trans)
                        cmd.Parameters.AddWithValue("@id", empId)
                        cmd.ExecuteNonQuery()
                    End Using

                    ' EMPLOYEE_QUALIFICATION
                    Using cmd As New SqlCommand("DELETE FROM EMPLOYEE_QUALIFICATION WHERE EMPLOYEE_ID = @id", conn, trans)
                        cmd.Parameters.AddWithValue("@id", empId)
                        cmd.ExecuteNonQuery()
                    End Using

                    ' EMPLOYEE
                    Using cmd As New SqlCommand("DELETE FROM EMPLOYEE WHERE EMPLOYEE_ID = @id", conn, trans)
                        cmd.Parameters.AddWithValue("@id", empId)
                        cmd.ExecuteNonQuery()
                    End Using

                    trans.Commit()
                    litMsg.Text = "<div class='alert alert-success'>Employee deleted successfully.</div>"
                    pnlEmpInfo.Visible = False
                    txtEmpId.Text = ""

                Catch ex As Exception
                    trans.Rollback()
                    litMsg.Text = "<div class='alert alert-danger'>Deletion failed: " & ex.Message & "</div>"
                End Try
            End Using
        Catch ex As Exception
            litMsg.Text = "<div class='alert alert-danger'>Error: " & ex.Message & "</div>"
        End Try
    End Sub
End Class