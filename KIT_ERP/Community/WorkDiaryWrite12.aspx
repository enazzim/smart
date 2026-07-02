<%@ Page language="c#" Codebehind="WorkDiaryWrite12.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.WorkDiaryWrite12" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WorkDiaryWrite12</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<table width="700" border="1" cellspacing="0" cellpadding="0" style="BORDER-BOTTOM-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-TOP-STYLE: none; BORDER-LEFT-STYLE: none"
				borderColor="#000000" id="table1">
				<tr>
					<td height="38" colspan="2" style="BORDER-BOTTOM: #000000 1px; BORDER-LEFT: #000000 1px solid; HEIGHT: 38px; BORDER-TOP: #000000 1px solid; BORDER-RIGHT: #000000 1px solid"
						align="center">&nbsp;
						<DIV style="WIDTH: 180px; DISPLAY: inline; FONT-SIZE: 25pt" ms_positioning="FlowLayout">¾÷ 
							¹« º¸ °í</DIV>
					</td>
				</tr>
				<tr>
					<td style="BORDER-RIGHT-STYLE: none; BORDER-TOP-STYLE: none" width="400">&nbsp;
						<asp:Label id="lbDate" runat="server"></asp:Label></td>
					<td style="BORDER-TOP-STYLE: none; BORDER-LEFT-STYLE: none" align="left" width="300">&nbsp;
						<asp:Label id="Label2" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td style="BORDER-BOTTOM: #000000 1px; BORDER-LEFT: #000000 1px solid; WORD-BREAK: break-all; BORDER-TOP: #000000 1px; BORDER-RIGHT: #000000 1px solid"
						vAlign="top" height="200" colspan="2" width="700">&nbsp;
						<asp:Literal id="Literal1" runat="server"></asp:Literal>
						<asp:TextBox id="TextBox1" runat="server" BorderStyle="None" TextMode="MultiLine" Height="200px"
							Width="700px" Font-Size="9pt" Font-Names="±¼¸²"></asp:TextBox></td>
				</tr>
				<tr>
					<td colspan="2" style="BORDER-BOTTOM-STYLE: none; WORD-BREAK: break-all" vAlign="top"
						height="200" width="700"><FONT face="±¼¸²">&nbsp;
							<asp:Literal id="Literal2" runat="server"></asp:Literal>
							<asp:TextBox id="TextBox3" runat="server" BorderStyle="None" TextMode="MultiLine" Height="200px"
								Width="700px" Font-Size="9pt" Font-Names="±¼¸²"></asp:TextBox></FONT></td>
				</tr>
				<TR>
					<TD style="BORDER-BOTTOM-STYLE: none; WORD-BREAK: break-all" vAlign="top" colSpan="2"
						height="150" width="700"><FONT face="±¼¸²">&nbsp;
							<asp:Literal id="Literal3" runat="server"></asp:Literal>
						</FONT>
						<asp:TextBox id="TextBox4" runat="server" BorderStyle="None" TextMode="MultiLine" Height="150px"
							Width="700px" Font-Size="9pt" Font-Names="±¼¸²"></asp:TextBox></TD>
				</TR>
				<tr>
					<td colspan="2" style="BORDER-BOTTOM-STYLE: none; WORD-BREAK: break-all" vAlign="top"
						height="100" width="700">&nbsp;
						<asp:Literal id="Literal5" runat="server"></asp:Literal>
						<asp:TextBox id="TextBox2" runat="server" Width="700px" Font-Size="9pt" Height="100px" TextMode="MultiLine"
							BorderStyle="None" Font-Names="±¼¸²"></asp:TextBox></td>
				</tr>
				<tr>
					<td colspan="2" style="BORDER-BOTTOM-STYLE: none; WORD-BREAK: break-all" vAlign="top"
						height="100" width="700">&nbsp;
						<asp:Literal id="Literal4" runat="server"></asp:Literal>
						<asp:TextBox id="TextBox5" runat="server" Width="700px" Font-Size="9pt" Height="100px" TextMode="MultiLine"
							BorderStyle="None" Font-Names="±¼¸²"></asp:TextBox></td>
				</tr>
				<tr>
					<td colspan="2" style="BORDER-BOTTOM-STYLE: none; WORD-BREAK: break-all" vAlign="top"
						height="100" width="700">&nbsp;
						<asp:Literal id="Literal10" runat="server"></asp:Literal>
						<asp:TextBox id="TextBox10" runat="server" Width="700px" Font-Size="9pt" Height="100px" TextMode="MultiLine"
							BorderStyle="None" Font-Names="±¼¸²"></asp:TextBox></td>
				</tr>
				<TR style="BORDER-TOP-STYLE: none">
					<TD style="BORDER-RIGHT-STYLE: none; BORDER-TOP: #000000 1px solid" vAlign="middle"
						align="left" height="25" width="400"><FONT face="±¼¸²">&nbsp;
							<asp:Button id="Button3" runat="server" Height="20px" Width="80px" Text="µ¹¾Æ°¡±â"></asp:Button><INPUT id="hdIndex" type="hidden" name="Hidden1" runat="server"></FONT></TD>
					<TD style="BORDER-LEFT-STYLE: none; BORDER-TOP: black 1px solid" vAlign="middle" align="right"
						height="25" width="300"><FONT face="±¼¸²">
							<asp:Button id="Button4" runat="server" Height="20px" Width="80px" Text="°áÀçÃë¼Ò" Visible="False"></asp:Button>&nbsp;
							<asp:Button id="Button1" runat="server" Height="20px" Width="80px" Text="°á  Àç"></asp:Button>&nbsp;
							<asp:Button id="Button2" runat="server" Height="20px" Width="80px" Text="µî  ·Ï"></asp:Button>&nbsp;</FONT></TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
