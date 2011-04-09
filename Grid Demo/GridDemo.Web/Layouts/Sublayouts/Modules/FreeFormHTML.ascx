<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FreeFormHTML.ascx.cs" Inherits="GridDemo.Web.Layouts.Sublayouts.Modules.FreeFormHTML" %>

<div class="module free-form-module">
	<h3>
		<sc:FieldRenderer runat="server" FieldName="Title" ID="TitleFieldRenderer" />
	</h3>

	<sc:FieldRenderer runat="server" FieldName="FreeFormHTML" ID="FreeFormHTMLRenderer" />
</div>