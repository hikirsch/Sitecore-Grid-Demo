<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BaseLayout.aspx.cs" Inherits="GridDemo.Web.Layouts.BaseLayout" %>
<!doctype html>
<html>
	<head runat="server">
		<title>Sitecore Grid Demo</title>

		<link rel="stylesheet" href="/Assets/css/reset.css" />
		<link rel="stylesheet" href="/Assets/css/screen.css" />

		<sc:Placeholder runat="server" ID="webedit" />
	</head>
	<body>
		<div class="page-container">
			<h1><sc:FieldRenderer runat="server" FieldName="Title" /></h1>

			<sc:Placeholder runat="server" Key="main-content" />
		</div>
	</body>
</html>
