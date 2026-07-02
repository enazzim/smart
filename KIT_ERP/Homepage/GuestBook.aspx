<%@ Page language="c#" Codebehind="GuestBook.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Homepage.GuestBook" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>GuestBook</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="FlowLayout" bgcolor="whitesmoke">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="FONT-SIZE: 0pt; Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px"
				cellSpacing="0" cellPadding="0" width="800" border="0" align="center">
				<TR height="30">
					<TD vAlign="middle" width="10"><FONT face="±¼¸²"></FONT></TD>
					<TD vAlign="middle" align="center" colSpan="3">
						<TABLE id="Table1" style="FONT-SIZE: 0pt" height="30" cellSpacing="0" cellPadding="0" width="150"
							border="0">
							<TR height="30">
								<TD style="BORDER-BOTTOM: dimgray 2px solid" vAlign="bottom" align="center" height="30"><asp:label id="Label1" runat="server" Font-Bold="True" ForeColor="SteelBlue" Font-Size="11pt"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR height="27">
					<TD width="10"><FONT face="±¼¸²"></FONT></TD>
					<TD align="right" colSpan="3" style="FONT-SIZE: 10pt"><FONT style="FONT-SIZE: 0pt" face="±¼¸²"><asp:button id="btnWrite" runat="server" Width="65px" Height="20px" Text="±Û¾²±â"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</FONT></TD>
				</TR>
				<TR>
					<TD width="10" align="center"><FONT face="±¼¸²"></FONT></TD>
					<TD style="FONT-SIZE: 0pt" vAlign="top" colSpan="3" align="center"><FONT face="±¼¸²"><asp:datagrid id="DataGrid1" runat="server" Font-Size="9pt" Width="100%" AllowPaging="True" AllowCustomPaging="True"
								GridLines="Horizontal" AutoGenerateColumns="False" BorderStyle="None" BorderWidth="0px">
								<FooterStyle Font-Bold="True" ForeColor="Red"></FooterStyle>
								<ItemStyle Height="32px"></ItemStyle>
								<HeaderStyle Font-Bold="True" Height="20px" ForeColor="ControlDarkDark" BackColor="LightGray"></HeaderStyle>
								<Columns>
									<asp:HyperLinkColumn DataNavigateUrlField="HGuestIndex" DataNavigateUrlFormatString="GuestBookAdjustment.aspx?HGuestIndex={0}"
										DataTextField="Title" HeaderText="Á¦&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;¸ñ">
										<HeaderStyle HorizontalAlign="Center" Width="60%" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
									</asp:HyperLinkColumn>
									<asp:BoundColumn DataField="Registrationperson" HeaderText="ÀÛ ¼º ÀÚ">
										<HeaderStyle HorizontalAlign="Center" Width="13%" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RegistrationDate" HeaderText="ÀÛ ¼º ÀÏ">
										<HeaderStyle HorizontalAlign="Center" Width="14%" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Hits" HeaderText="Á¶È¸¼ö">
										<HeaderStyle HorizontalAlign="Center" Width="13%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle VerticalAlign="Middle" Height="20px" Font-Bold="True" HorizontalAlign="Center" ForeColor="ControlDarkDark"
									BackColor="LightGray" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></FONT>
						<div align="center">&nbsp;</div>
					</TD>
				</TR>
				<tr height="25">
					<td width="10"><FONT face="±¼¸²"></FONT></td>
					<td align="center" colspan="3" style="FONT-SIZE: 10pt">
						<DIV align="center"><asp:label id="lblPageInfo" runat="server"></asp:label></DIV>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
