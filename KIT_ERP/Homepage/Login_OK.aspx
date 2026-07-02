<%@ Page language="c#" Codebehind="Login_OK.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Homepage.Login_OK" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Login_OK</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
		<!--
			function Submit()
			{
				__doPostBack('HyperLink1','');
			}
		//-->
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px" cellSpacing="0" cellPadding="0"
				width="199" background="../Images/login_ok.gif" border="0">
				<TR>
					<TD vAlign="bottom" align="center" width="199" colSpan="2" height="47"><FONT face="±¼¸²">
							<asp:Label id="Label2" runat="server" Font-Size="9pt"></asp:Label>
						</FONT>
					</TD>
				</TR>
				<tr>
					<td vAlign="top" align="center" width="199" colSpan="2" height="47"><FONT face="±¼¸²"><asp:Label id="Label1" runat="server" Font-Size="9pt"></asp:Label></FONT></td>
				</tr>
				<TR>
					<TD vAlign="top" align="left" width="132" height="50"><FONT face="±¼¸²">
							<asp:HyperLink id="HyperLink1" runat="server" Target="_parent" NavigateUrl="../main.htm" ImageUrl="../images/home2-loginform_12.gif"></asp:HyperLink></FONT></TD>
					<TD vAlign="top" align="left" width="67" height="50"><FONT face="±¼¸²"><asp:imagebutton id="ImageButton1" runat="server" ImageUrl="../Images/Logout.gif"></asp:imagebutton></FONT></TD>
				</TR>
				<TR>
					<TD colSpan="2" height="26"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
