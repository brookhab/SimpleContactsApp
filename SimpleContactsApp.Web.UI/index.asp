
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Home page - SimpleContactsApp.Web.UI</title>
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
            
<div class="text-center">
    <h1 class="display-4">Welcome</h1>
    <p>A Simple Classic ASP Web App </a>.</p>
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