Partial Class Admin_Dashboard
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Optional: Add role-based access check here if needed
    End Sub

    Protected Sub btnAddEmployee_Click(sender As Object, e As EventArgs)
        Response.Redirect("Add_Employee.aspx")
    End Sub

    Protected Sub btnDeleteEmployee_Click(sender As Object, e As EventArgs)
        Response.Redirect("Delete_Employee.aspx")
    End Sub

    Protected Sub btnEditEmployee_Click(sender As Object, e As EventArgs)
        Response.Redirect("Edit_Employee.aspx")
    End Sub
End Class