<%@ Page language="c#" Codebehind="WorkDiaryWrite13.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.WorkDiaryWrite13" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WorkDiaryWrite13</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<table id="table1" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none"
				borderColor="#000000" cellSpacing="0" cellPadding="0" width="700" border="1">
				<tr>
					<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px; HEIGHT: 38px"
						align="center" width="700" colSpan="2" height="38">&nbsp;
						<DIV style="DISPLAY: inline; FONT-SIZE: 25pt; WIDTH: 180px" ms_positioning="FlowLayout">¾÷ 
							¹« º¸ °í</DIV>
					</td>
				</tr>
				<tr>
					<td style="WORD-BREAK: break-all; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none"
						width="400">&nbsp;
						<asp:label id="lbDate" runat="server"></asp:label></td>
					<td style="BORDER-TOP-STYLE: none; BORDER-LEFT-STYLE: none" align="left" width="300">&nbsp;
						<asp:label id="Label2" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px; BORDER-LEFT: #000000 1px solid; WORD-BREAK: break-all; BORDER-BOTTOM: #000000 1px"
						vAlign="top" width="700" colSpan="2" height="150">&nbsp;
						<asp:Literal id="Literal1" runat="server"></asp:Literal><asp:textbox id="TextBox1" runat="server" Width="700px" Font-Size="9pt" Height="150px" TextMode="MultiLine"
							BorderStyle="None" Font-Names="±¼¸²"></asp:textbox></td>
				</tr>
				<TR>
					<TD style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; WORD-BREAK: break-all; BORDER-BOTTOM: #000000 1px"
						vAlign="top" width="700" colSpan="2" height="150"><FONT face="±¼¸²">&nbsp;
							<asp:Literal id="Literal2" runat="server"></asp:Literal>
						</FONT>
						<asp:textbox id="TextBox3" runat="server" Width="700px" Font-Size="9pt" Height="150px" TextMode="MultiLine"
							BorderStyle="None" Font-Names="±¼¸²"></asp:textbox></TD>
				</TR>
				<tr>
					<td style="WORD-BREAK: break-all; BORDER-BOTTOM-STYLE: none" vAlign="top" width="700"
						colSpan="2" height="150"><FONT face="±¼¸²">&nbsp;
							<asp:Literal id="Literal3" runat="server"></asp:Literal><asp:textbox id="TextBox4" runat="server" Width="700px" Font-Size="9pt" Height="150px" TextMode="MultiLine"
								BorderStyle="None" Font-Names="±¼¸²"></asp:textbox></FONT></td>
				</tr>
				<TR>
					<TD style="WORD-BREAK: break-all; BORDER-BOTTOM-STYLE: none" vAlign="top" width="700"
						colSpan="2" height="150"><FONT face="±¼¸²">&nbsp;
							<asp:Literal id="Literal4" runat="server"></asp:Literal>
						</FONT>
						<asp:textbox id="TextBox5" runat="server" Width="700px" Font-Size="9pt" Height="150px" TextMode="MultiLine"
							BorderStyle="None" Font-Names="±¼¸²"></asp:textbox></TD>
				</TR>
				<tr>
					<td style="WORD-BREAK: break-all; BORDER-BOTTOM-STYLE: none" vAlign="top" width="700"
						colSpan="2" height="100">&nbsp;
						<asp:Literal id="Literal6" runat="server"></asp:Literal><asp:textbox id="TextBox2" runat="server" Width="700px" Font-Size="9pt" Height="100px" TextMode="MultiLine"
							BorderStyle="None" Font-Names="±¼¸²"></asp:textbox></td>
				</tr>
				<tr>
					<td style="WORD-BREAK: break-all; BORDER-BOTTOM-STYLE: none" vAlign="top" width="700"
						colSpan="2" height="100">&nbsp;
						<asp:Literal id="Literal5" runat="server"></asp:Literal><asp:textbox id="TextBox6" runat="server" Width="700px" Font-Size="9pt" Height="100px" TextMode="MultiLine"
							BorderStyle="None" Font-Names="±¼¸²"></asp:textbox></td>
				</tr>
				<tr>
					<td colspan="2" style="BORDER-BOTTOM-STYLE: none; WORD-BREAK: break-all" vAlign="top"
						height="100" width="700">&nbsp;
						<asp:Literal id="Literal10" runat="server"></asp:Literal>
						<asp:TextBox id="TextBox10" runat="server" Width="700px" Font-Size="9pt" Height="100px" TextMode="MultiLine"
							BorderStyle="None" Font-Names="±¼¸²"></asp:TextBox></td>
				</tr>
				<TR style="BORDER-TOP-STYLE: none">
					<TD style="BORDER-TOP: #000000 1px solid; WORD-BREAK: break-all; BORDER-RIGHT-STYLE: none"
						vAlign="middle" align="left" width="400" height="25"><FONT face="±¼¸²">&nbsp;
							<asp:button id="Button3" runat="server" Width="80px" Height="20px" Text="µ¹¾Æ°¡±â"></asp:button><INPUT id="hdIndex" type="hidden" name="Hidden1" runat="server"></FONT></TD>
					<TD style="BORDER-TOP: black 1px solid; WORD-BREAK: break-all; BORDER-LEFT-STYLE: none"
						vAlign="middle" align="right" width="300" height="25"><FONT face="±¼¸²"><asp:button id="Button4" runat="server" Width="80px" Height="20px" Text="°áÀçÃë¼Ò" Visible="False"></asp:button>&nbsp;
							<asp:button id="Button1" runat="server" Width="80px" Height="20px" Text="°á  Àç"></asp:button>&nbsp;
							<asp:button id="Button2" runat="server" Width="80px" Height="20px" Text="µî  ·Ï"></asp:button>&nbsp;</FONT></TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
