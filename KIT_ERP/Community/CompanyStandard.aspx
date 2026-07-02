<%@ Page language="c#" Codebehind="CompanyStandard.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.CompanyStandard" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>»ç³»Ç¥ÁØÇöÈ²</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<table style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" borderColor="darkgray"
				cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
				<TBODY>
					<TR>
						<TD style="HEIGHT: 15px" width="20"></TD>
						<TD style="HEIGHT: 15px" align="right" width="780" colSpan="4"><FONT face="±¼¸²"></FONT></TD>
					</TR>
					<tr>
						<td style="HEIGHT: 14px" width="20" align="right"><FONT face="±¼¸²"></FONT></td>
						<TD align="center" width="70"><FONT face="±¼¸²"><FONT face="±¼¸²">Categoy</FONT></FONT></TD>
						<TD align="left" width="780"><FONT face="±¼¸²">
								<asp:dropdownlist id="DropDownList1" runat="server" BackColor="#EEEEE9" AutoPostBack="True"></asp:dropdownlist>
								<asp:button id="Button1" runat="server" Text="»è  Á¦" Width="65px" Height="20px"></asp:button></FONT></TD>
						<td align="right" width="780" colSpan="2"><FONT face="±¼¸²">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							</FONT>
							<asp:button id="btnWrite" runat="server" Height="20px" Width="65px" Text="±Û¾²±â"></asp:button><FONT face="±¼¸²"></FONT></td>
					</tr>
					<TR>
						<td width="10" vAlign="top"><FONT face="±¼¸²"></FONT></td>
						<td vAlign="top" width="780" colSpan="4">
							<asp:datagrid id="DataGrid1" runat="server" Width="800px" AllowPaging="True" AllowCustomPaging="True"
								AutoGenerateColumns="False" GridLines="Horizontal" CellPadding="4" BackColor="White" BorderWidth="0px"
								BorderStyle="None" PageSize="15">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#339966"></SelectedItemStyle>
								<ItemStyle Height="20px" ForeColor="#333333" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="ControlDarkDark" BackColor="LightGray"></HeaderStyle>
								<FooterStyle Font-Bold="True" ForeColor="Red"></FooterStyle>
								<Columns>
									<asp:HyperLinkColumn DataNavigateUrlField="CCompanyStandardIndex" DataNavigateUrlFormatString="CSAdjustment.aspx?CCompanyStandardIndex={0}"
										DataTextField="Title" HeaderText="Á¦&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;¸ñ">
										<HeaderStyle HorizontalAlign="Center" Width="420px" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
									</asp:HyperLinkColumn>
									<asp:BoundColumn DataField="RegistrationPerson" HeaderText="ÀÛ&amp;nbsp;¼º&amp;nbsp;ÀÚ">
										<HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RegistrationDate" HeaderText="ÀÛ&amp;nbsp;&amp;nbsp;¼º&amp;nbsp;&amp;nbsp;ÀÏ">
										<HeaderStyle HorizontalAlign="Center" Width="200px" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Hits" HeaderText="Á¶È¸¼ö">
										<HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
										<FooterStyle HorizontalAlign="Center" VerticalAlign="Middle"></FooterStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="ControlDarkDark" BackColor="LightGray"
									Mode="NumericPages"></PagerStyle>
							</asp:datagrid></td>
					</TR>
					<tr height="31">
						<td align="center" colSpan="5"><FONT face="±¼¸²">&nbsp;&nbsp; </FONT>
							<asp:label id="lblPageInfo" runat="server" Font-Size="9pt"></asp:label><FONT face="±¼¸²"></FONT></td>
					</tr>
				</TBODY>
			</table>
		</form>
	</body>
</HTML>
