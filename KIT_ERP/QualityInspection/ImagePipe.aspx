<%@ Register TagPrefix="igchart" Namespace="Infragistics.WebUI.UltraWebChart" Assembly="Infragistics.WebUI.UltraWebChart.v4.1, Version=4.1.20042.36, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="ImagePipe.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.QualityInspection.ImagePipe" codePage="949" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ImagePipe</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="FlowLayout" bgColor="#f7f6f6" leftMargin="15">
		<form id="Form1" method="post" runat="server">
			<igchart:SecureImagePipe id="SecureImagePipe1" runat="server"></igchart:SecureImagePipe>
		</form>
	</body>
</HTML>
