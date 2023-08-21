
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Contacts - SimpleContactsApp.Web.UI</title>
    <link rel="stylesheet" href="contents/lib/bootstrap/dist/css/bootstrap.min.css">
			<link rel="stylesheet" href="contents/css/site.css?v=AKvNjO3dCPPS0eSU1Ez8T2wI280i08yGycV9ndytL-c">
			<link rel="stylesheet" href="contents/css/styles.css?v=iA7m9pVEX7Q_YjPj4IuBpcfiQr2H_8agvWLjVQU5q4w">
			<link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet">



</head>
<body>
<%

    if Session("userId") <> "" then
    
    else
        Response.Redirect("login.asp")
    end If

    Set objHTTP = CreateObject("MSXML2.ServerXMLHTTP")
    objHTTP.SetOption(2) = (objHTTP.GetOption(2) - SXH_SERVER_CERT_IGNORE_ALL_SERVER_ERRORS)

    if len(Request.QueryString("handler")) > 0 then

	    id = Request.QueryString("id")

	    objHTTP.Open "DELETE", "https://simplecontactapp.azurewebsites.net/api/Contacts/"& id & "?userId=" & Session("userId"), False

	    objHTTP.setRequestHeader "User-Agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0)"
	    objHTTP.setRequestHeader "Content-Type", "application/json"
	    objHTTP.send 


        objHTTP.Open "GET", "https://simplecontactapp.azurewebsites.net/api/contacts/get" & "?userId=" & Session("userId"), False

	    objHTTP.setRequestHeader "User-Agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0)"
	    objHTTP.setRequestHeader "Content-Type", "application/json"
	    objHTTP.send 

	    contacts = objHTTP.responseText
	    status = objHTTP.statusText

        if status="OK" then
            response.write("<input type='hidden' name='hdnValue' id='hdnValue' value='" & contacts & "'/>") 
        else
            response.write("<input type='hidden' name='hdnValue' id='hdnValue' value=''/>")     
        end if 

    else
	
	    objHTTP.Open "GET", "https://simplecontactapp.azurewebsites.net/api/contacts/get"& "?userId=" & Session("userId"), False

	    objHTTP.setRequestHeader "User-Agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0)"
	    objHTTP.setRequestHeader "Content-Type", "application/json"
	    objHTTP.send 

	    contacts = objHTTP.responseText
	    status = objHTTP.statusText

        if status="OK" then
            response.write("<input type='hidden' name='hdnValue' id='hdnValue' value='" & contacts & "'/>") 
        else
            response.write("<input type='hidden' name='hdnValue' id='hdnValue' value=''/>")     
        end if 
    end if

    

	
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
            



<div class="container mt-5">
    <h1 class="mb-4">Contacts</h1>
    <div class="d-flex justify-content-between align-items-center mb-3">
        <a class="btn btn-primary" href="Create.asp">
            <i class="fas fa-plus me-2"></i> Create New
        </a>
        <form method="get" class="d-flex">
            <input type="text" name="searchTerm" placeholder="Search by name, email, or phone" class="form-control me-2" value="" />
            <button type="submit" class="btn btn-primary">Search</button>
        </form>


    </div>
    <!-- The rest of your code remains the same for creating and searching contacts -->

    <div class="table-responsive">
        <table class="table table-bordered table-hover">
            <thead class="table-light">
                <tr>
                    <th class="text-center">Name</th>
                    <th class="text-center">Email</th>
                    <th class="text-center">Phone</th>
                    <th class="text-center">Actions</th>
                </tr>
            </thead>
            <tbody id="tbodyData">
                    
            </tbody>
        </table>
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

        <script type="text/javascript">

            $(function () {
                var contacts = $('#hdnValue').val();
                if (contacts != "") {
                    var json = $.parseJSON(contacts);
                    console.log(json)
                    
                    if (json.length > 0)
                    {
                        var contactHtml = '';
                        for (var i = 0; i < json.length; i++) {
                            
                            contactHtml += '<tr><td class="text-center">' + json[i].name + '</td><td class="text-center">' + json[i].email + '</td><td class="text-center">' + json[i].phone + '</td>' +
                                '<td class="text-center"><a class="btn btn-info btn-sm" href="details.asp?id=' + json[i].id + '"> <i class="fas fa-info-circle"></i></a>&nbsp;' +
                                '<a class="btn btn-warning btn-sm mr-2" href="edit.asp?id=' + json[i].id + '"><i class="fas fa-edit"></i></a>&nbsp;'+
                                '<form method="post" onsubmit="return validate();" class="d-inline" action="contacts.asp?id=' + json[i].id + '&amp;handler=Delete">' +
                                '<button type="submit" class="btn btn-danger btn-sm"><i class="fas fa-trash-alt"></i></button>' +
                                '</form>' +
                                '</td></tr>';
                        }

                       $('#tbodyData').html(contactHtml);
                    };
                }
            }); 


            function validate()
            {
                return confirm("Are you sure you want to delete this contact ? ")
            }
        </script>
    </body>
</html>