Imports System
Imports System.Web.Security

Partial Class Receptionist_Dashboard
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' Ensure only receptionists can access
            Dim ticket = FormsAuthentication.Decrypt(Request.Cookies(FormsAuthentication.FormsCookieName).Value)
            If ticket.UserData.Split("|"c)(1) <> "RECEPTIONIST" Then
                Response.Redirect("~/Login.aspx")
            End If
        End If
    End Sub

    Protected Sub lnkPatients_Click(sender As Object, e As EventArgs)
        Response.Redirect("Manage_Patients.aspx")
    End Sub

    Protected Sub lnkRooms_Click(sender As Object, e As EventArgs)
        Response.Redirect("Manage_Rooms.aspx")
    End Sub

    Protected Sub lnkAppointments_Click(sender As Object, e As EventArgs)
        Response.Redirect("Manage_Appointments.aspx")
    End Sub

    Protected Sub lnkTests_Click(sender As Object, e As EventArgs)
        Response.Redirect("Manage_PatientTests.aspx")
    End Sub

    Protected Sub lnkAdmissions_Click(sender As Object, e As EventArgs)
        Response.Redirect("Manage_Admissions.aspx")
    End Sub

    Protected Sub lnkInvoices_Click(sender As Object, e As EventArgs)
        Response.Redirect("Manage_Invoices.aspx")
    End Sub
End Class