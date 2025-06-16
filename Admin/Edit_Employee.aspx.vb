Imports System.Data.SqlClient
Imports System.Configuration

Partial Class Edit_Employee
    Inherits System.Web.UI.Page

    Private ReadOnly _cs As String =
        ConfigurationManager.ConnectionStrings("Hospital_Management_SystemConnectionString").ConnectionString

    Protected Sub btnLoad_Click(sender As Object, e As EventArgs)
        pnlError.Visible = False
        pnlSuccess.Visible = False

        Dim empId = txtSearchEmpId.Text.Trim()
        If empId = "" Then
            ShowError("Please enter an Employee ID.")
            Return
        End If

        ' Load existing data
        Dim sql As String = " SELECT E.EMPLOYEE_NAME, E.EMPLOYEE_DEPARTMENT, E.EMPLOYEE_DESIGNATION, E.ROOM_ID, C.EMPLOYEE_CONTACT_NUMBER, Q.QUALIFICATION FROM EMPLOYEE E LEFT JOIN EMPLOYEE_CONTACT C ON E.EMPLOYEE_ID = C.EMPLOYEE_ID LEFT JOIN EMPLOYEE_QUALIFICATION Q ON E.EMPLOYEE_ID = Q.EMPLOYEE_ID WHERE E.EMPLOYEE_ID = @id"

        Using conn As New SqlConnection(_cs),
              cmd As New SqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@id", empId)
            conn.Open()
            Using rdr = cmd.ExecuteReader()
                If rdr.Read() Then
                    pnlEdit.Visible = True
                    txtEmpName.Text = rdr("EMPLOYEE_NAME").ToString()
                    txtDept.Text = rdr("EMPLOYEE_DEPARTMENT").ToString()
                    txtDesig.Text = rdr("EMPLOYEE_DESIGNATION").ToString()
                    txtRoomId.Text = If(IsDBNull(rdr("ROOM_ID")), "", rdr("ROOM_ID").ToString())
                    txtContact.Text = If(IsDBNull(rdr("EMPLOYEE_CONTACT_NUMBER")), "", rdr("EMPLOYEE_CONTACT_NUMBER").ToString())
                    txtQual.Text = If(IsDBNull(rdr("QUALIFICATION")), "", rdr("QUALIFICATION").ToString())
                Else
                    ShowError("Employee ID not found.")
                    pnlEdit.Visible = False
                End If
            End Using
        End Using
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs)
        pnlError.Visible = False
        pnlSuccess.Visible = False

        Dim empId = txtSearchEmpId.Text.Trim()
        Using conn As New SqlConnection(_cs)
            conn.Open()
            Dim trans = conn.BeginTransaction()
            Try
                ' Update EMPLOYEE
                Dim sqlEmp = " UPDATE EMPLOYEE SET EMPLOYEE_NAME = @name, EMPLOYEE_DEPARTMENT = @dept, EMPLOYEE_DESIGNATION = @desig, ROOM_ID = @room WHERE EMPLOYEE_ID = @id"
                Using cmd As New SqlCommand(sqlEmp, conn, trans)
                    cmd.Parameters.AddWithValue("@id", empId)
                    cmd.Parameters.AddWithValue("@name", txtEmpName.Text.Trim())
                    cmd.Parameters.AddWithValue("@dept", txtDept.Text.Trim())
                    cmd.Parameters.AddWithValue("@desig", txtDesig.Text.Trim())
                    cmd.Parameters.AddWithValue("@room", If(txtRoomId.Text = "", DBNull.Value, txtRoomId.Text))
                    cmd.ExecuteNonQuery()
                End Using

                ' Update EMPLOYEE_CONTACT
                Dim sqlCon = "IF EXISTS (SELECT 1 FROM EMPLOYEE_CONTACT WHERE EMPLOYEE_ID = @id) UPDATE EMPLOYEE_CONTACT SET EMPLOYEE_CONTACT_NUMBER = @contact WHERE EMPLOYEE_ID = @id ELSE INSERT INTO EMPLOYEE_CONTACT (EMPLOYEE_ID, EMPLOYEE_CONTACT_NUMBER) VALUES (@id, @contact)"
                Using cmd As New SqlCommand(sqlCon, conn, trans)
                    cmd.Parameters.AddWithValue("@id", empId)
                    cmd.Parameters.AddWithValue("@contact", txtContact.Text.Trim())
                    cmd.ExecuteNonQuery()
                End Using

                ' Update EMPLOYEE_QUALIFICATION
                Dim sqlQual = " IF EXISTS (SELECT 1 FROM EMPLOYEE_QUALIFICATION WHERE EMPLOYEE_ID = @id) UPDATE EMPLOYEE_QUALIFICATION SET QUALIFICATION = @qual WHERE EMPLOYEE_ID = @id ELSE INSERT INTO EMPLOYEE_QUALIFICATION (EMPLOYEE_ID, QUALIFICATION) VALUES (@id, @qual)"
                Using cmd As New SqlCommand(sqlQual, conn, trans)
                    cmd.Parameters.AddWithValue("@id", empId)
                    cmd.Parameters.AddWithValue("@qual", txtQual.Text.Trim())
                    cmd.ExecuteNonQuery()
                End Using

                trans.Commit()
                ShowSuccess("Employee updated successfully.")
            Catch ex As Exception
                trans.Rollback()
                ShowError("Update failed: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub ShowError(msg As String)
        litError.Text = msg
        pnlError.Visible = True
    End Sub

    Private Sub ShowSuccess(msg As String)
        litSuccess.Text = msg
        pnlSuccess.Visible = True
    End Sub
End Class