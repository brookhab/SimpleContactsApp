
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title> - SimpleContactsApp.Web.UI</title>
	<link rel="stylesheet" href="contents/lib/bootstrap/dist/css/bootstrap.min.css">
	<link rel="stylesheet" href="contents/css/site.css?v=AKvNjO3dCPPS0eSU1Ez8T2wI280i08yGycV9ndytL-c">
	<link rel="stylesheet" href="contents/css/styles.css?v=iA7m9pVEX7Q_YjPj4IuBpcfiQr2H_8agvWLjVQU5q4w">
	<link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet">

</head>	
<%


if len(Request.Form("Username")) > 0 then
	username = Request.Form("Username")
	password =Request.Form("Password")

	body = "{'username': '" & username & "', 'password' : '" & password & "'}"

	body = REPLACE(body,"'","""")

	Set objHTTP = CreateObject("MSXML2.ServerXMLHTTP")

	objHTTP.SetOption(2) = (objHTTP.GetOption(2) - SXH_SERVER_CERT_IGNORE_ALL_SERVER_ERRORS)

	objHTTP.Open "POST", "https://simplecontactapp.azurewebsites.net/api/account/register" & "?userId=" & Session("userId"), False

	objHTTP.setRequestHeader "User-Agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0)"
	objHTTP.setRequestHeader "Content-Type", "application/json"
	objHTTP.send body

	loginResult = objHTTP.responseText
	loginStatus = objHTTP.statusText

	response.write loginResult
	response.write loginStatus

	If(loginStatus = "OK") then
		Session("userId") = Mid(objHTTP.responseText,26,37)

		Response.Redirect("index.asp")
	End If		

End If	


%>

	<body>
		<div b-6wk4q94kw7="" class="container">
		<main b-6wk4q94kw7 role="main" class="pb-3">
            <!-- Rest of your login page code -->
			<div class="container mt-5">
				<div class="row justify-content-center">
					<div class="col-md-6">
						<h2 class="mb-4">Register</h2>
						<form method="post" class="card p-4">
							<div class="text-danger validation-summary-valid" data-valmsg-summary="true"><ul><li style="display:none"></li>
								</ul></div>
							<div class="mb-3">
								<label class="form-label" for="Username">Username</label>
								<input class="form-control" type="text" data-val="true" data-val-required="The Username field is required." id="Username" name="Username" value="" />
								<span class="text-danger field-validation-valid" data-valmsg-for="Username" data-valmsg-replace="true"></span>
							</div>
							<div class="mb-3">
								<label class="form-label" for="Password">Password</label>
								<input class="form-control" type="password" data-val="true" data-val-required="The Password field is required." id="Password" name="Password" />
								<span class="text-danger field-validation-valid" data-valmsg-for="Password" data-valmsg-replace="true"></span>
							</div>
							<button type="submit" class="btn btn-primary">Register</button>
						<input name="__RequestVerificationToken" type="hidden" value="CfDJ8IIcHGDCIylBmE_ZP06iTmtQ5tDqxX7j5bSg2DyFX8fwN2PjRPYwXQrnWvcltVj-sfkTEG5hBhOnKoXDTHjeeldcLbzzHxWDSrTdWW_91JuEMdQ0_rmrytDPX3dkLEca7AV1dEIjIJybB-mvZnpFKIE" /></form>
						<p class="mt-3 text-center">Already have an account? <a href="login.asp">Login here</a>.</p>
					</div>
				</div>
			</div>

        </main>
		</div>

		<footer b-6wk4q94kw7="" class="border-top footer text-muted">
		<div b-6wk4q94kw7="" class="container">
		© 2023 - SimpleContactsApp.Web.UI - <a href="/Privacy">Privacy</a>
		</div>
		</footer>

		<script src="contents/lib/jquery/dist/jquery.min.js"></script>
		<script src="contents/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
		<script src="contents/js/site.js?v=4q1jwFhaPaZgr8WAUSrux6hAuh0XDg9kPS3xIVq36I0"></script>

		<script src="contents/lib/_browser-refresh.js"></script>

<script>

	$(function()
	{
		var loginStatus = $('#hdnStatus').val();
		var loginResult = $('#hdnValue').val();

		if(loginStatus == "OK")
		{
			//location.href='index.asp';
		}
		else if(loginStatus == "Bad Request")
		{
			$('.validation-summary-valid li').show().html('Invalid username or password');
		}
		//var json = $.parseJSON(loginResult);
	});

	function validation()
	{
		var username=$('#Username').val();
		var password = $('#Password').val();

		if(username == '' )
		{
			$('.field-validation-valid').html('Username field is required').show();
			return false;
		}

		if(password == '' )
		{
			$('.field-validation-valid').html('Password field is required').show();
			return false;
		}

		return true;
	}
     
</script>

	</body>
</html>