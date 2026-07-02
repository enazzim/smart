<%@ Page language="c#" Codebehind="View.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.Popup.View" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>View</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../StyleSheet2.css" type="text/css" rel="stylesheet">
		<STYLE type="text/css">BODY { SCROLLBAR-FACE-COLOR: #ffffff; SCROLLBAR-HIGHLIGHT-COLOR: #ffffff; SCROLLBAR-SHADOW-COLOR: #ffffff; SCROLLBAR-3DLIGHT-COLOR: #808080; SCROLLBAR-ARROW-COLOR: #400040; SCROLLBAR-TRACK-COLOR: #ffffff; SCROLLBAR-DARKSHADOW-COLOR: #808080 }
		</STYLE>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="buttonface">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; WIDTH: 488px; POSITION: absolute; TOP: 8px; HEIGHT: 400px"
				height="400" cellSpacing="0" cellPadding="0" width="488" border="1">
				<TR>
					<TD style="WIDTH: 50px; HEIGHT: 28px" align="center"><FONT face="±º∏≤">¿€º∫¿⁄</FONT></TD>
					<TD style="HEIGHT: 28px"><FONT face="±º∏≤">&nbsp;</FONT>
						<asp:Label id="lb_Writer" runat="server"></asp:Label></TD>
					<TD style="HEIGHT: 28px" align="center"><FONT face="±º∏≤">¿€º∫¿œ</FONT></TD>
					<TD style="HEIGHT: 28px"><FONT face="±º∏≤">&nbsp;</FONT>
						<asp:Label id="lb_WriteDay" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 50px; HEIGHT: 30px" align="center"><FONT face="±º∏≤">¡¶∏Ò</FONT></TD>
					<TD colspan="3" style="HEIGHT: 30px"><FONT face="±º∏≤">&nbsp;</FONT>
						<asp:Label id="lb_Title" runat="server"></asp:Label></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 50px" align="center"><FONT face="±º∏≤">∆ƒ¿œ</FONT></TD>
					<TD colspan="3"><FONT face="±º∏≤">&nbsp;
							<asp:HyperLink id="HyperLink1" runat="server"></asp:HyperLink>
							<asp:Label id="lb_Size" runat="server"></asp:Label></FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 50px; HEIGHT: 304px" align="center">
						<P><FONT face="±º∏≤">≥ª</FONT></P>
						<P><FONT face="±º∏≤">øÎ</FONT></P>
					</TD>
					<TD colspan="3" style="HEIGHT: 304px">
						<asp:TextBox id="tb_Contents" runat="server" Width="428px" TextMode="MultiLine" Height="289px"
							BackColor="#EEEEE9"></asp:TextBox></TD>
				</TR>
			</TABLE>
			<asp:Button id="bt_Close" style="Z-INDEX: 102; LEFT: 440px; POSITION: absolute; TOP: 408px"
				runat="server" Height="20px" Width="56px" Text="¥› ±‚" Font-Size="9pt"></asp:Button>
		</form>
	</body>
</HTML>
