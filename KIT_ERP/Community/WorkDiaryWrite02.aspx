<%@ Page language="c#" Codebehind="WorkDiaryWrite02.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.WorkDiaryWrite02" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WorkDiaryWrite02</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../StyleSheet1.css">
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<table style="BORDER-BOTTOM-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-TOP-STYLE: none; BORDER-LEFT-STYLE: none"
				id="table1" border="1" cellSpacing="0" borderColor="#000000" cellPadding="0" width="700">
				<tr>
					<td style="BORDER-BOTTOM: #000000 1px; BORDER-LEFT: #000000 1px solid; HEIGHT: 38px; BORDER-TOP: #000000 1px solid; BORDER-RIGHT: #000000 1px solid"
						height="38" width="700" colSpan="2" align="center">&nbsp;
						<DIV style="WIDTH: 180px; DISPLAY: inline; FONT-SIZE: 25pt" ms_positioning="FlowLayout">¾÷ 
							¹« º¸ °í</DIV>
					</td>
				</tr>
				<tr>
					<td style="BORDER-RIGHT-STYLE: none; BORDER-TOP-STYLE: none" width="400">&nbsp;
						<asp:label id="lbDate" runat="server"></asp:label></td>
					<td style="BORDER-TOP-STYLE: none; BORDER-LEFT-STYLE: none" width="300" align="left">&nbsp;
						<asp:label id="Label2" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td style="BORDER-BOTTOM: #000000 1px; BORDER-LEFT: #000000 1px solid; WORD-BREAK: break-all; BORDER-TOP: #000000 1px; BORDER-RIGHT: #000000 1px solid"
						height="300" vAlign="top" width="700" colSpan="2">&nbsp;
						<asp:literal id="Literal1" runat="server"></asp:literal><asp:textbox id="TextBox1" runat="server" Font-Names="±¼¸²" Font-Size="9pt" Width="700px" Height="300px"
							TextMode="MultiLine" BorderStyle="None"></asp:textbox></td>
				</tr>
				<tr>
					<td style="BORDER-BOTTOM-STYLE: none; WORD-BREAK: break-all" height="150" vAlign="top"
						width="700" colSpan="2"><FONT face="±¼¸²">&nbsp;
							<asp:literal id="Literal2" runat="server"></asp:literal><asp:textbox id="TextBox3" runat="server" Font-Names="±¼¸²" Font-Size="9pt" Width="700px" Height="150px"
								TextMode="MultiLine" BorderStyle="None"></asp:textbox></FONT></td>
				</tr>
				<TR>
					<TD style="BORDER-BOTTOM-STYLE: none; WORD-BREAK: break-all" height="150" vAlign="top"
						width="700" colSpan="2"><FONT face="±¼¸²">&nbsp;
							<asp:literal id="Literal3" runat="server"></asp:literal></FONT><asp:textbox id="TextBox4" runat="server" Font-Names="±¼¸²" Font-Size="9pt" Width="700px" Height="150px"
							TextMode="MultiLine" BorderStyle="None"></asp:textbox></TD>
				</TR>
				<tr>
					<td style="BORDER-BOTTOM-STYLE: none; WORD-BREAK: break-all" height="100" vAlign="top"
						width="700" colSpan="2">&nbsp;
						<asp:label id="Label5" runat="server" Width="700px"></asp:label><asp:textbox id="TextBox5" runat="server" Font-Names="±¼¸²" Font-Size="9pt" Width="700px" Height="100px"
							TextMode="MultiLine" BorderStyle="None"></asp:textbox></td>
				</tr>
				<tr>
					<td colspan="2" style="BORDER-BOTTOM-STYLE: none; WORD-BREAK: break-all" vAlign="top"
						height="100" width="700">&nbsp;
						<asp:Literal id="Literal10" runat="server"></asp:Literal>
						<asp:TextBox id="TextBox10" runat="server" Width="700px" Font-Size="9pt" Height="100px" TextMode="MultiLine"
							BorderStyle="None" Font-Names="±¼¸²"></asp:TextBox></td>
				</tr>
				<TR style="BORDER-TOP-STYLE: none">
					<TD style="BORDER-RIGHT-STYLE: none; BORDER-TOP: #000000 1px solid" height="25" vAlign="middle"
						width="400" align="left"><FONT face="±¼¸²">&nbsp;
							<asp:button id="Button3" runat="server" Width="80px" Height="20px" Text="µ¹¾Æ°¡±â"></asp:button><INPUT id="hdIndex" type="hidden" name="Hidden1" runat="server"></FONT></TD>
					<TD style="BORDER-LEFT-STYLE: none; BORDER-TOP: black 1px solid" height="25" vAlign="middle"
						width="300" align="right"><FONT face="±¼¸²"><asp:button id="Button4" runat="server" Width="80px" Height="20px" Text="°áÀçÃë¼Ò" Visible="False"></asp:button>&nbsp;
							<asp:button id="Button1" runat="server" Width="80px" Height="20px" Text="°á  Àç"></asp:button>&nbsp;
							<asp:button id="Button2" runat="server" Width="80px" Height="20px" Text="µî  ·Ï"></asp:button>&nbsp;</FONT></TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
