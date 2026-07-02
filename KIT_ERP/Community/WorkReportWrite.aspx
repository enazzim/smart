<%@ Page language="c#" Codebehind="WorkReportWrite.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.WorkReportWrite" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WorkReportWrite</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<table width="800" border="1" cellspacing="0" cellpadding="0" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none"
				borderColor="#000000">
				<tr>
					<td height="38" colspan="2" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px; HEIGHT: 38px"
						align="center">&nbsp;
						<DIV style="DISPLAY: inline; FONT-SIZE: 25pt; WIDTH: 180px" ms_positioning="FlowLayout">업 
							무 보 고</DIV>
					</td>
				</tr>
				<tr>
					<td colspan="2" style="BORDER-TOP-STYLE: none">&nbsp;
						<asp:Label id="lbDate" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px"
						vAlign="top" height="100">&nbsp;
						<asp:Label id="Label1" runat="server" Width="400px"></asp:Label>
						<asp:TextBox id="TextBox1" runat="server" BorderStyle="None" TextMode="MultiLine" Height="200px"
							Width="394px" Font-Size="9pt"></asp:TextBox></td>
					<td style="BORDER-TOP-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none"
						vAlign="top" height="100">&nbsp;
						<asp:Label id="Label2" runat="server" Width="400px"></asp:Label>
						<asp:TextBox id="TextBox2" runat="server" BorderStyle="None" TextMode="MultiLine" Height="200px"
							Width="394px" Font-Size="9pt"></asp:TextBox></td>
				</tr>
				<tr>
					<td colspan="2" style="BORDER-BOTTOM-STYLE: none" vAlign="top" height="100"><FONT face="굴림">&nbsp;
							<asp:Label id="Label3" runat="server" Width="800px"></asp:Label>
							<asp:TextBox id="TextBox3" runat="server" BorderStyle="None" TextMode="MultiLine" Height="200px"
								Width="800px" Font-Size="9pt"></asp:TextBox></FONT></td>
				</tr>
				<tr>
					<td colspan="2" style="BORDER-BOTTOM-STYLE: none" vAlign="top" height="100">&nbsp;
						<asp:Label id="Label4" runat="server" Width="800px"></asp:Label>
						<asp:TextBox id="TextBox4" runat="server" BorderStyle="None" TextMode="MultiLine" Height="200px"
							Width="800px" Font-Size="9pt"></asp:TextBox></td>
				</tr>
				<tr>
					<td colspan="2" style="BORDER-BOTTOM-STYLE: none" vAlign="top" height="100">&nbsp;
						<asp:Label id="Label5" runat="server" Width="800px"></asp:Label>
						<asp:TextBox id="TextBox5" runat="server" BorderStyle="None" TextMode="MultiLine" Height="200px"
							Width="800px" Font-Size="9pt"></asp:TextBox></td>
				</tr>
				<tr>
					<td colspan="2" vAlign="top" style="BORDER-LEFT-STYLE: solid" height="100">&nbsp;
						<asp:Label id="Label6" runat="server" Width="800px"></asp:Label>
						<asp:TextBox id="TextBox6" runat="server" BorderStyle="None" TextMode="MultiLine" Height="200px"
							Width="800px" Font-Size="9pt"></asp:TextBox></td>
				</tr>
				<TR style="BORDER-TOP-STYLE: none">
					<TD style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none" vAlign="middle" align="left"
						height="25"><FONT face="굴림">&nbsp;
							<asp:Button id="Button3" runat="server" Height="20px" Width="80px" Text="돌아가기"></asp:Button><INPUT id="hdIndex" type="hidden" name="Hidden1" runat="server"></FONT></TD>
					<TD style="BORDER-TOP-STYLE: none; BORDER-LEFT-STYLE: none" vAlign="middle" align="right"
						colSpan="2" height="25"><FONT face="굴림">
							<asp:Button id="Button4" runat="server" Height="20px" Width="80px" Text="결재취소" Visible="False"></asp:Button>&nbsp;
							<asp:Button id="Button1" runat="server" Height="20px" Width="80px" Text="결  재"></asp:Button>&nbsp;
							<asp:Button id="Button2" runat="server" Height="20px" Width="80px" Text="등  록"></asp:Button>&nbsp;</FONT></TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
