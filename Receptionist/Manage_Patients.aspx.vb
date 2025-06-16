Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient

Partial Class Manage_Patients
    Inherits System.Web.UI.Page

    Private ReadOnly cs As String = ConfigurationManager.ConnectionStrings("Hospital_Management_SystemConnectionString").ConnectionString

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadPatients()
        End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Response.Redirect("Receptionist_Dashboard.aspx")
    End Sub

    Private Sub LoadPatients(Optional searchTerm As String = "")
        Dim query As String = "SELECT P.PATIENT_ID, P.PATIENT_NAME, P.PATIENT_AGE, P.GENDER, C.PATIENT_CONTACT_NUMBER AS CONTACT " &
                              "FROM PATIENT P INNER JOIN PATIENT_CONTACT C ON P.PATIENT_ID = C.PATIENT_ID"

        If Not String.IsNullOrEmpty(searchTerm) Then
            query &= " WHERE P.PATIENT_ID LIKE @term OR P.PATIENT_NAME LIKE @term"
        End If

        Using conn As New SqlConnection(cs), cmd As New SqlCommand(query, conn)
            If Not String.IsNullOrEmpty(searchTerm) Then
                cmd.Parameters.AddWithValue("@term", "%" & searchTerm & "%")
            End If
            Dim dt As New DataTable()
            Using da As New SqlDataAdapter(cmd)
                da.Fill(dt)
            End Using
            gvPatients.DataSource = dt
            gvPatients.DataBind()
        End Using
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        lblMessage.Text = ""
        lblMessage.CssClass = "text-danger"

        ' Validate inputs
        If txtId.Text.Trim() = "" OrElse txtName.Text.Trim() = "" OrElse
       txtAge.Text.Trim() = "" OrElse ddlGender.SelectedValue = "" OrElse
       txtContact.Text.Trim() = "" Then
            lblMessage.Text = "All fields are required."
            Return
        End If

        Try
            Using conn As New SqlConnection(cs)
                conn.Open()

                ' Check for duplicate ID
                Dim checkCmd As New SqlCommand("SELECT COUNT(*) FROM PATIENT WHERE PATIENT_ID=@id", conn)
                checkCmd.Parameters.AddWithValue("@id", txtId.Text.Trim())
                If Convert.ToInt32(checkCmd.ExecuteScalar()) > 0 Then
                    lblMessage.Text = "Patient ID already exists. Please use a different ID."
                    Return
                End If

                ' Start transaction
                Dim transaction = conn.BeginTransaction()

                Try
                    ' Insert into PATIENT
                    Dim cmd1 As New SqlCommand("INSERT INTO PATIENT (PATIENT_ID, PATIENT_NAME, PATIENT_AGE, GENDER) VALUES (@id, @name, @age, @gender)", conn, transaction)
                    cmd1.Parameters.AddWithValue("@id", txtId.Text.Trim())
                    cmd1.Parameters.AddWithValue("@name", txtName.Text.Trim())
                    cmd1.Parameters.AddWithValue("@age", Convert.ToInt32(txtAge.Text.Trim()))
                    cmd1.Parameters.AddWithValue("@gender", ddlGender.SelectedValue)
                    cmd1.ExecuteNonQuery()

                    ' Insert into PATIENT_CONTACT
                    Dim cmd2 As New SqlCommand("INSERT INTO PATIENT_CONTACT (PATIENT_ID, PATIENT_CONTACT_NUMBER) VALUES (@id, @contact)", conn, transaction)
                    cmd2.Parameters.AddWithValue("@id", txtId.Text.Trim())
                    cmd2.Parameters.AddWithValue("@contact", txtContact.Text.Trim())
                    cmd2.ExecuteNonQuery()

                    transaction.Commit()
                    lblMessage.CssClass = "text-success"
                    lblMessage.Text = "Patient added successfully."
                    ClearForm()
                    LoadPatients()
                Catch ex As Exception
                    transaction.Rollback()
                    lblMessage.Text = "Error: " & ex.Message
                End Try
            End Using
        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        gvPatients.EditIndex = -1
        LoadPatients(txtSearch.Text.Trim())
    End Sub

    Private Sub ClearForm()
        txtId.Text = ""
        txtName.Text = ""
        txtAge.Text = ""
        txtContact.Text = ""
        ddlGender.SelectedIndex = 0
    End Sub

    Protected Sub gvPatients_RowEditing(sender As Object, e As GridViewEditEventArgs)
        gvPatients.EditIndex = e.NewEditIndex
        LoadPatients()
    End Sub

    Protected Sub gvPatients_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs)
        gvPatients.EditIndex = -1
        LoadPatients()
    End Sub

    Protected Sub gvPatients_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)
        Dim row = gvPatients.Rows(e.RowIndex)
        Dim id = gvPatients.DataKeys(e.RowIndex).Value.ToString()
        Dim name = CType(row.Cells(1).Controls(0), TextBox).Text
        Dim age = CType(row.Cells(2).Controls(0), TextBox).Text
        Dim gender = CType(row.Cells(3).Controls(0), TextBox).Text
        Dim contact = CType(row.Cells(4).Controls(0), TextBox).Text

        Try
            Using conn As New SqlConnection(cs)
                conn.Open()
                Dim cmd1 As New SqlCommand("UPDATE PATIENT SET PATIENT_NAME=@name, PATIENT_AGE=@age, GENDER=@gender WHERE PATIENT_ID=@id", conn)
                cmd1.Parameters.AddWithValue("@id", id)
                cmd1.Parameters.AddWithValue("@name", name)
                cmd1.Parameters.AddWithValue("@age", age)
                cmd1.Parameters.AddWithValue("@gender", gender)
                cmd1.ExecuteNonQuery()

                Dim cmd2 As New SqlCommand("UPDATE PATIENT_CONTACT SET PATIENT_CONTACT_NUMBER=@contact WHERE PATIENT_ID=@id", conn)
                cmd2.Parameters.AddWithValue("@id", id)
                cmd2.Parameters.AddWithValue("@contact", contact)
                cmd2.ExecuteNonQuery()
            End Using
            gvPatients.EditIndex = -1
            LoadPatients()
        Catch ex As Exception
            lblMessage.Text = "Error: " & ex.Message
        End Try
    End Sub

    Protected Sub gvPatients_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim id = gvPatients.DataKeys(e.RowIndex).Value.ToString()

        Try
            Using conn As New SqlConnection(cs)
                conn.Open()

                Dim tran As SqlTransaction = conn.BeginTransaction()

                Try
                    ' First delete from dependent table: PATIENT_CONTACT
                    Dim cmd1 As New SqlCommand("DELETE FROM PATIENT_CONTACT WHERE PATIENT_ID=@id", conn, tran)
                    cmd1.Parameters.AddWithValue("@id", id)
                    cmd1.ExecuteNonQuery()

                    ' Then delete from main table: PATIENT
                    Dim cmd2 As New SqlCommand("DELETE FROM PATIENT WHERE PATIENT_ID=@id", conn, tran)
                    cmd2.Parameters.AddWithValue("@id", id)
                    cmd2.ExecuteNonQuery()

                    tran.Commit()
                    lblMessage.CssClass = "text-success"
                    lblMessage.Text = "Patient deleted successfully."
                Catch exInner As SqlException
                    tran.Rollback()

                    If exInner.Number = 547 Then
                        lblMessage.CssClass = "text-danger"
                        lblMessage.Text = "Cannot delete: This patient has related records (appointments, admissions, etc.)."
                    Else
                        lblMessage.CssClass = "text-danger"
                        lblMessage.Text = "SQL Error: " & exInner.Message
                    End If
                End Try
            End Using

            gvPatients.EditIndex = -1
            LoadPatients()
        Catch ex As Exception
            lblMessage.CssClass = "text-danger"
            lblMessage.Text = "Error: " & ex.Message
        End Try
    End Sub

End Class