
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Create Contact - SimpleContactsApp.Web.UI</title>
            <link rel="stylesheet" href="contents/lib/bootstrap/dist/css/bootstrap.min.css">
			<link rel="stylesheet" href="contents/css/site.css?v=AKvNjO3dCPPS0eSU1Ez8T2wI280i08yGycV9ndytL-c">
			<link rel="stylesheet" href="contents/css/styles.css?v=iA7m9pVEX7Q_YjPj4IuBpcfiQr2H_8agvWLjVQU5q4w">
			<link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet">
</head>
<body>

<%


if Session("UserId") <> "" then
    
else
   Response.Redirect("login.asp")
end If

if len(Request.Form("Name")) > 0 then
	name = Request.Form("Name")
	email =Request.Form("Email")
    phone =Request.Form("Phone")
    userId = Session("UserId")

	body = "{'name': '" & name & "', 'email' : '" & email & "', 'phone' : '" & phone & "', 'userId' : '" & userId & "'}"

	body = REPLACE(body,"'","""")

	Set objHTTP = CreateObject("MSXML2.ServerXMLHTTP")

	objHTTP.SetOption(2) = (objHTTP.GetOption(2) - SXH_SERVER_CERT_IGNORE_ALL_SERVER_ERRORS)

	objHTTP.Open "POST", "https://simplecontactapp.azurewebsites.net/api/Contacts/Create"& "?userId=" & Session("userId"), False

	objHTTP.setRequestHeader "User-Agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0)"
	objHTTP.setRequestHeader "Content-Type", "application/json"
	objHTTP.send body

    createResult = objHTTP.responseText
	createStatus = objHTTP.statusText

    response.write createStatus

	If(createStatus = "Created") then
		Response.Redirect("contacts.asp")
	End If		

End If	
%>
        <header>
            <nav b-6wk4q94kw7 class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
                <div b-6wk4q94kw7 class="container">
                    <a class="navbar-brand" href="/">Simple Contacts App</a>
                    <button b-6wk4q94kw7 class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                            aria-expanded="false" aria-label="Toggle navigation">
                        <span b-6wk4q94kw7 class="navbar-toggler-icon"></span>
                    </button>
                    <div b-6wk4q94kw7 class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                        <ul b-6wk4q94kw7 class="navbar-nav flex-grow-1">
                            <li b-6wk4q94kw7 class="nav-item">
                                <a class="nav-link text-dark" href="index.asp">Home</a>
                            </li>
                            <li b-6wk4q94kw7 class="nav-item">
                                <a class="nav-link text-dark" href="contacts.asp">Contacts</a>
                            </li>
                        </ul>
                        <form method="post" action="login.asp">
                            <input type="hidden" name="Logout" value="1" />
                            <button b-6wk4q94kw7 type="submit" class="btn btn-primary">Logout</button>
                        <input name="__RequestVerificationToken" type="hidden" value="CfDJ8IIcHGDCIylBmE_ZP06iTmtGmhBux-vWCKMomuMKk0lekpzzC8kwuaeHj8tB5B-Gw9fGE2ReoC93Y6FBGB0zgrEzc9WNY22yFI6aYGmy2JAiAUDQwPMNagxDc4yAz5onn1zX9krfzYq6rkewzyHmkUs" /></form>
                    </div>
                </div>
            </nav>
        </header>
    <div b-6wk4q94kw7 class="container">
        <main b-6wk4q94kw7 role="main" class="pb-3">
            

<div class="boddy">
    <div class="form-container">
        <h1>Create Contact</h1>

        <form method="post">
            <div class="text-danger validation-summary-valid" data-valmsg-summary="true"><ul><li style="display:none"></li>
</ul></div>

        

            <div class="form-group">
                <label for="Name">Name</label>
                <input class="form-control" type="text" data-val="true" data-val-required="Name is required" id="Name" name="Name" value="" />
                <span class="text-danger field-validation-valid" data-valmsg-for="Name" data-valmsg-replace="true"></span>
            </div>
            <div class="form-group">
                <label for="Email">Email</label>
                <input class="form-control" type="email" data-val="true" data-val-email="Invalid email address" data-val-required="Email is required" id="Email" name="Email" value="" />
                <span class="text-danger field-validation-valid" data-valmsg-for="Email" data-valmsg-replace="true"></span>
            </div>
            <div class="form-group">
                <label for="Phone">Phone</label>
                <input class="form-control" type="tel" data-val="true" data-val-phone="Invalid phone number" data-val-required="The Phone field is required." id="Phone" name="Phone" value="" />
                <span class="text-danger field-validation-valid" data-valmsg-for="Phone" data-valmsg-replace="true"></span>
            </div>
            <button type="submit" class="btn btn-primary">Create</button>
        <input name="__RequestVerificationToken" type="hidden" value="CfDJ8IIcHGDCIylBmE_ZP06iTmu3BVifd7oIAsdUE4WW2eDrwCZU8Gtqci-66QgrN5Ff108h0RfJ8PJEg9GQtTMZDtT8RRf6f3Ujp7mhGm4_3Onv8M3Iru5P8nIa__IkkoBqUZxkJb5yC03kJ3D6lp6X7HI" /></form>

        <div class="mt-3">
            <a class="btn btn-secondary btn-block" href="Contacts.asp">Back to List</a>
        </div>
    </div>
</div>

        </main>
    </div>

    <footer b-6wk4q94kw7 class="border-top footer text-muted">
        <div b-6wk4q94kw7 class="container">
            &copy; 2023 - SimpleContactsApp.Web.UI - <a href="/Privacy">Privacy</a>
        </div>
    </footer>

    <script src="contents/lib/jquery/dist/jquery.min.js"></script>
		<script src="contents/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
		<script src="contents/js/site.js?v=4q1jwFhaPaZgr8WAUSrux6hAuh0XDg9kPS3xIVq36I0"></script>




		<script src="contents/lib/_browser-refresh.js"></script>
    </body>
</html>