<%@ Page language="c#" Codebehind="SubNotice.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.CooperationCompany.SubNotice" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SubNotice</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<table id="a" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" borderColor="darkgray"
				cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
				<TBODY>
					<tr height="27">
						<td width="20"></td>
						<td align="right" colSpan="3"><asp:button id="btnWrite" runat="server" Text="글쓰기" Width="65px" Height="20px" Font-Size="9pt"></asp:button><FONT face="굴림">&nbsp;&nbsp;</FONT></td>
					</tr>
					<TR>
						<td width="20"></td>
						<td vAlign="top" colSpan="3"><asp:datagrid id="DataGrid1" runat="server" Width="800px" AllowPaging="True" AllowCustomPaging="True"
								AutoGenerateColumns="False" GridLines="Horizontal" CellPadding="4" BackColor="White" BorderWidth="0px" BorderStyle="None"
								PageSize="15" Font-Size="9pt">
								<FooterStyle Font-Bold="True" ForeColor="Red"></FooterStyle>
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#339966"></SelectedItemStyle>
								<ItemStyle Height="32px" ForeColor="#333333" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="ControlDarkDark" BackColor="#C0C0FF"></HeaderStyle>
								<Columns>
									<asp:HyperLinkColumn DataNavigateUrlField="SubNoticeIndex" DataNavigateUrlFormatString="SubAdjustment.aspx?SubNoticeIndex={0}"
										DataTextField="Title" HeaderText="제&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;목">
										<HeaderStyle HorizontalAlign="Center" Width="400px" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
									</asp:HyperLinkColumn>
									<asp:BoundColumn DataField="RegistrationPerson" HeaderText="작&amp;nbsp;성&amp;nbsp;자">
										<HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RegistrationDate" HeaderText="작&amp;nbsp;&amp;nbsp;성&amp;nbsp;&amp;nbsp;일">
										<HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Hits" HeaderText="조회수">
										<HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
										<FooterStyle HorizontalAlign="Center" VerticalAlign="Middle"></FooterStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="ControlDarkDark" BackColor="#C0C0FF"
									Mode="NumericPages"></PagerStyle>
							</asp:datagrid></td>
					</TR>
					<tr height="31">
						<td width="20"></td>
						<td align="left" width="300"><FONT face="굴림">&nbsp;&nbsp; </FONT>
						</td>
						<td align="center" width="300"><asp:label id="lblPageInfo" runat="server"></asp:label></td>
						<td width="300"><FONT face="굴림"></FONT></td>
					</tr>
				</TBODY>
			</table>
		</form>
	</body>
</HTML>
