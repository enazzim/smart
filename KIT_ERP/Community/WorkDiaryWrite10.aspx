<%@ Page language="c#" Codebehind="WorkDiaryWrite10.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.WorkDiaryWrite10" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WorkDiaryWrite10</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<table width="700" border="1" cellspacing="0" cellpadding="0" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none"
				borderColor="#000000" id="table1">
				<tr>
					<td height="38" colspan="2" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px; HEIGHT: 38px"
						align="center" Width="700">&nbsp;
						<DIV style="DISPLAY: inline; FONT-SIZE: 25pt; WIDTH: 180px" ms_positioning="FlowLayout">¾÷ 
							¹« º¸ °í</DIV>
					</td>
				</tr>
				<tr>
					<td style="WORD-BREAK: break-all; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none"
						width="400">&nbsp;
						<asp:Label id="lbDate" runat="server"></asp:Label></td>
					<td style="WORD-BREAK: break-all; BORDER-TOP-STYLE: none; BORDER-LEFT-STYLE: none" align="left"
						width="300">&nbsp;
						<asp:Label id="Label2" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px; BORDER-LEFT: #000000 1px solid; WORD-BREAK: break-all; BORDER-BOTTOM: #000000 1px"
						vAlign="top" height="150" colspan="2" Width="700">&nbsp;
						<asp:Literal id="Literal1" runat="server"></asp:Literal>
						<asp:TextBox id="TextBox1" runat="server" BorderStyle="None" TextMode="MultiLine" Height="150px"
							Width="700px" Font-Size="9pt" Font-Names="±¼¸²"></asp:TextBox></td>
				</tr>
				<tr>
					<td colspan="2" style="WORD-BREAK: break-all; BORDER-BOTTOM-STYLE: none" vAlign="top"
						height="200" Width="700"><FONT face="±¼¸²">&nbsp;
							<asp:Literal id="Literal2" runat="server"></asp:Literal>
							<asp:TextBox id="TextBox3" runat="server" BorderStyle="None" TextMode="MultiLine" Height="200px"
								Width="700px" Font-Size="9pt" Font-Names="±¼¸²"></asp:TextBox></FONT></td>
				</tr>
				<TR>
					<TD style="WORD-BREAK: break-all; BORDER-BOTTOM-STYLE: none" vAlign="top" width="700"
						colSpan="2" height="150"><FONT face="±¼¸²">&nbsp; </FONT>
						<asp:Literal id="Literal3" runat="server"></asp:Literal>
						<asp:TextBox id="TextBox4" runat="server" Font-Names="±¼¸²" Font-Size="9pt" Width="700px" Height="150px"
							TextMode="MultiLine" BorderStyle="None"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="WORD-BREAK: break-all; BORDER-BOTTOM-STYLE: none" vAlign="top" width="700"
						colSpan="2" height="150"><FONT face="±¼¸²">&nbsp; </FONT>
						<asp:Literal id="Literal5" runat="server"></asp:Literal>
						<asp:TextBox id="TextBox2" runat="server" Font-Names="±¼¸²" Font-Size="9pt" Width="700px" Height="150px"
							TextMode="MultiLine" BorderStyle="None"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD style="WORD-BREAK: break-all; BORDER-BOTTOM-STYLE: none" vAlign="top" width="700"
						colSpan="2" height="150"><FONT face="±¼¸²">&nbsp; </FONT>
						<asp:Literal id="Literal4" runat="server"></asp:Literal>
						<asp:TextBox id="TextBox5" runat="server" Font-Names="±¼¸²" Font-Size="9pt" Width="700px" Height="150px"
							TextMode="MultiLine" BorderStyle="None"></asp:TextBox></TD>
				</TR>
				<tr>
					<td colspan="2" style="BORDER-BOTTOM-STYLE: none; WORD-BREAK: break-all" vAlign="top"
						height="100" width="700">&nbsp;
						<asp:Literal id="Literal10" runat="server"></asp:Literal>
						<asp:TextBox id="TextBox10" runat="server" Width="700px" Font-Size="9pt" Height="100px" TextMode="MultiLine"
							BorderStyle="None" Font-Names="±¼¸²"></asp:TextBox></td>
				</tr>
				<TR style="BORDER-TOP-STYLE: none">
					<TD style="BORDER-TOP: #000000 1px solid; WORD-BREAK: break-all; BORDER-RIGHT-STYLE: none"
						vAlign="middle" align="left" height="25" Width="400"><FONT face="±¼¸²">&nbsp;
							<asp:Button id="Button3" runat="server" Height="20px" Width="80px" Text="µ¹¾Æ°¡±â"></asp:Button><INPUT id="hdIndex" type="hidden" name="Hidden1" runat="server"></FONT></TD>
					<TD style="BORDER-TOP: black 1px solid; BORDER-LEFT-STYLE: none" vAlign="middle" align="right"
						height="25" Width="300"><FONT face="±¼¸²">
							<asp:Button id="Button4" runat="server" Height="20px" Width="80px" Text="°áÀçÃë¼Ò" Visible="False"></asp:Button>&nbsp;
							<asp:Button id="Button1" runat="server" Height="20px" Width="80px" Text="°á  Àç"></asp:Button>&nbsp;
							<asp:Button id="Button2" runat="server" Height="20px" Width="80px" Text="µî  ·Ï"></asp:Button>&nbsp;</FONT></TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
