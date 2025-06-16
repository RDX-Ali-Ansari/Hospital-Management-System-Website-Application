Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web.Security
Imports System.Text
Imports System.Security.Cryptography

Partial Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' If we were bounced here with a ReturnUrl (e.g. from /Admin/Admin_Dashboard.aspx),
            ' clear it so the URL is just /Login.aspx
            If Not String.IsNullOrEmpty(Request.QueryString("ReturnUrl")) Then
                FormsAuthentication.SignOut()
                Session.Clear()
                Response.Redirect("~/Login.aspx", True)
                Return
            End If

            ' Force logout for testing (you can remove once sign-out link is in place)
            FormsAuthentication.SignOut()
            Session.Clear()

            ' Show any messages
            If Request.QueryString("registered") = "true" Then
                pnlSuccess.Visible = True
                litSuccess.Text = "Registration successful! Please login with your credentials."
            End If

            If Request.QueryString("timeout") = "true" Then
                pnlError.Visible = True
                litError.Text = "Your session has timed out. Please login again."
            End If

            ' If somehow still authenticated, send them on
            If User.Identity.IsAuthenticated Then
                RedirectToUserDashboard()
            End If
        End If
    End Sub

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If Page.IsValid Then
            Try
                Dim connectionString As String = ConfigurationManager.ConnectionStrings("Hospital_Management_SystemConnectionString").ConnectionString
                Dim conn As New SqlConnection(connectionString)
                Dim hashedPassword As String = HashPassword(txtPassword.Text)
                Dim userId As Integer = 0
                Dim userType As String = String.Empty
                Dim relatedId As String = String.Empty
                Dim username As String = String.Empty

                ' Check if input is username or email
                Dim isEmail As Boolean = txtUsername.Text.Contains("@")
                Dim query As String

                If isEmail Then
                    query = "SELECT USER_ID, USERNAME, USER_TYPE, RELATED_ID FROM USERS WHERE EMAIL = @UserInput AND PASSWORD = @Password AND IS_ACTIVE = 1"
                Else
                    query = "SELECT USER_ID, USERNAME, USER_TYPE, RELATED_ID FROM USERS WHERE USERNAME = @UserInput AND PASSWORD = @Password AND IS_ACTIVE = 1"
                End If

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@UserInput", txtUsername.Text.Trim())
                    cmd.Parameters.AddWithValue("@Password", hashedPassword)

                    conn.Open()
                    Dim reader As SqlDataReader = cmd.ExecuteReader()

                    If reader.Read() Then
                        userId = Convert.ToInt32(reader("USER_ID"))
                        username = reader("USERNAME").ToString()
                        userType = reader("USER_TYPE").ToString()
                        relatedId = reader("RELATED_ID").ToString()

                        reader.Close()

                        ' Update last login time
                        Using cmdUpdate As New SqlCommand("UPDATE USERS SET LAST_LOGIN = GETDATE() WHERE USER_ID = @UserId", conn)
                            cmdUpdate.Parameters.AddWithValue("@UserId", userId)
                            cmdUpdate.ExecuteNonQuery()
                        End Using

                        ' Create authentication ticket
                        CreateAuthenticationTicket(userId.ToString(), username, userType, relatedId, chkRememberMe.Checked)

                        ' Redirect to appropriate dashboard based on user type
                        RedirectToUserDashboard(userType)
                    Else
                        pnlError.Visible = True
                        litError.Text = "Invalid username or password. Please try again."
                    End If
                End Using

                conn.Close()

            Catch ex As Exception
                pnlError.Visible = True
                litError.Text = "Error: " & ex.Message
            End Try
        End If
    End Sub

    ' Create Forms Authentication Ticket
    Private Sub CreateAuthenticationTicket(ByVal userId As String, ByVal username As String,
                                          ByVal userType As String, ByVal relatedId As String,
                                          ByVal rememberMe As Boolean)
        ' Create user data string (pipe-separated values)
        Dim userData As String = String.Format("{0}|{1}|{2}", userId, userType, relatedId)

        ' Set ticket expiration
        Dim expirationTime As DateTime
        If rememberMe Then
            expirationTime = DateTime.Now.AddDays(7) ' Remember for 7 days
        Else
            expirationTime = DateTime.Now.AddMinutes(30) ' Session times out after 30 minutes
        End If

        Dim authenticationTicket As New FormsAuthenticationTicket(1, username, DateTime.Now, expirationTime, rememberMe, userData, FormsAuthentication.FormsCookiePath)


        ' Encrypt the ticket
        Dim encryptedTicket As String = FormsAuthentication.Encrypt(authenticationTicket)

        ' Create the cookie
        Dim authCookie As New HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)

        If rememberMe Then
            authCookie.Expires = authenticationTicket.Expiration
        End If

        ' Add the cookie to the response
        Response.Cookies.Add(authCookie)
    End Sub

    ' Redirect user to appropriate dashboard based on role
    Private Sub RedirectToUserDashboard(Optional ByVal userType As String = "")
        If String.IsNullOrEmpty(userType) Then
            ' Get user type from authentication ticket if not provided
            Dim ticket As FormsAuthenticationTicket = FormsAuthentication.Decrypt(Request.Cookies(FormsAuthentication.FormsCookieName).Value)
            Dim userData As String() = ticket.UserData.Split("|"c)
            userType = userData(1)
        End If

        ' Redirect based on user type
        Select Case userType
            Case "PATIENT"
                Response.Redirect("~/Patient/Patient_Dashboard.aspx")
            Case "DOCTOR"
                Response.Redirect("~/Doctor/Doctor_Dashboard.aspx")
            Case "RECEPTIONIST"
                Response.Redirect("~/Receptionist/Receptionist_Dashboard.aspx")
            Case "LAB_TECHNICIAN"
                Response.Redirect("~/LabTech/Dashboard.aspx")
            Case "ADMIN"
                Response.Redirect("~/Admin/Admin_Dashboard.aspx")
        End Select
    End Sub

    ' Hash password using SHA256
    Private Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(password))
            Dim builder As New StringBuilder()

            For i As Integer = 0 To bytes.Length - 1
                builder.Append(bytes(i).ToString("x2"))
            Next

            Return builder.ToString()
        End Using
    End Function
End Class