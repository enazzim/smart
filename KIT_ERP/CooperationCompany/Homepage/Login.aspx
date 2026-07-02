<%@ Page language="c#" Codebehind="Login.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Homepage.Login" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Login</title>
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
	<body MS_POSITIONING="GridLayout" onload="document.Form1.TextBox1.focus();">
		<form id="Form1" method="post" runat="server">
			<TABLE id="a" WIDTH="199" background="../Images/login_ok.gif" height="170" BORDER="0" CELLPADDING="0"
				CELLSPACING="0" style="Z-INDEX: 101; LEFT: 0px; PADDING-BOTTOM: 1px; PADDING-TOP: 1px; POSITION: absolute; TOP: 0px">
				<tr>
					<td background="../Images/login_01.gif" height="85" colspan="2" width="199" align="right"
						valign="middle">
						<asp:TextBox id="TextBox1" runat="server" Width="115px" BackColor="#EEEEE9"></asp:TextBox><FONT face="±¼¸²">&nbsp;&nbsp;
						</FONT>
						<br>
						<asp:TextBox id="TextBox2" runat="server" Width="115px" TextMode="Password" BackColor="#EEEEE9"></asp:TextBox><FONT face="±¼¸²">&nbsp;&nbsp;
						</FONT>
					</td>
				</tr>
				<tr>
					<td height="76" width="132" align="center" valign="top"><FONT face="±¼¸²"><asp:Label id="Label1" runat="server" Width="100px" ForeColor="Red" Font-Size="8pt"></asp:Label></FONT></td>
					<td height="76" width="67" align="left" valign="top">
						<asp:ImageButton id="ImageButton1" runat="server" ImageUrl="../Images/h_19.gif"></asp:ImageButton></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
