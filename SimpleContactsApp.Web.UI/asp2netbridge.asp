<%
' This should only be called from api only. '
' Check if the requested URL matches the API URL
if (Request.ServerVariables("HTTP_REFERER") = "https://simplecontactapp.azurewebsites.net/api/account") then
    If (Request.QueryString("userId") <> "") Then
        Session("userId") = Request.QueryString("userId")
        Response.Write "Session variable set!" + Session("userId")
    End If
End If
%>