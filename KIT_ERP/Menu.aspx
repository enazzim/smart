<%@ Page language="c#" Codebehind="Menu.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Menu1" %>
<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics.WebUI.UltraWebNavigator.v3.2, Version=3.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Menu</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<STYLE type="text/css">BODY { SCROLLBAR-FACE-COLOR: #ffffff; SCROLLBAR-HIGHLIGHT-COLOR: #ffffff; SCROLLBAR-SHADOW-COLOR: #ffffff; SCROLLBAR-3DLIGHT-COLOR: #808080; SCROLLBAR-ARROW-COLOR: #400040; SCROLLBAR-TRACK-COLOR: #ffffff; SCROLLBAR-DARKSHADOW-COLOR: #808080 }
		</STYLE>
	</HEAD>
	<body bgcolor="#f6f6f6" style="BACKGROUND-POSITION: 0% 98%; BACKGROUND-ATTACHMENT: fixed; BACKGROUND-IMAGE: url(Images/Menu_bg.jpg); BACKGROUND-REPEAT: no-repeat"
		text="black" vLink="purple" aLink="red" link="blue">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px" height="545"
				cellSpacing="0" cellPadding="0" width="230" border="0" background="./images/menu_bg.jpg">
				<tr>
					<td height="10"></td>
				</tr>
				<TR>
					<TD style="PADDING-RIGHT: 20px; FONT-SIZE: 0pt" vAlign="top" height="550">
						<ignav:ultrawebtree id="UltraWebTree1" runat="server" CollapseImage="ig_treeMinus.gif" DefaultImage="ig_treeFolder.gif"
							WebTreeTarget="ClassicTree" DefaultSelectedImage="ig_treeFolderOpen.gif" Font-Size="9pt" Width="180px"
							Height="100%">
							<SelectedNodeStyle ForeColor="White" BackColor="Navy"></SelectedNodeStyle>
							<ParentNodeStyle Font-Bold="True"></ParentNodeStyle>
							<Levels>
								<ignav:Level Index="0"></ignav:Level>
								<ignav:Level Index="1"></ignav:Level>
							</Levels>
						</ignav:ultrawebtree>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
