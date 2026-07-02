<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics.WebUI.UltraWebNavigator.v3.2, Version=3.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="Bom1.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.Popup.Bom1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Bom1</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<STYLE type="text/css">BODY { SCROLLBAR-FACE-COLOR: #ffffff; SCROLLBAR-HIGHLIGHT-COLOR: #ffffff; SCROLLBAR-SHADOW-COLOR: #ffffff; SCROLLBAR-3DLIGHT-COLOR: #808080; SCROLLBAR-ARROW-COLOR: #400040; SCROLLBAR-TRACK-COLOR: #ffffff; SCROLLBAR-DARKSHADOW-COLOR: #808080 }
		</STYLE>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<ignav:ultrawebtree id="UltraWebTree1" style="Z-INDEX: 102; LEFT: 8px; POSITION: absolute; TOP: 8px"
				runat="server" Width="100%" Height="700px" Font-Size="10pt" DefaultSelectedImage="ig_treeFolderOpen.gif"
				WebTreeTarget="ClassicTree" DefaultImage="ig_treeFolder.gif" CollapseImage="ig_treeMinus.gif">
				<SelectedNodeStyle ForeColor="White" BackColor="Navy"></SelectedNodeStyle>
				<ParentNodeStyle Font-Bold="True"></ParentNodeStyle>
				<Levels>
					<ignav:Level Index="0"></ignav:Level>
					<ignav:Level Index="1"></ignav:Level>
				</Levels>
			</ignav:ultrawebtree></form>
	</body>
</HTML>
