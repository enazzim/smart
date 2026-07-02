<%@ Page language="c#" Codebehind="FreeBoard.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.FreeBoard" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Notice</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body BGCOLOR="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<table style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" borderColor="darkgray"
				cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
				<TBODY>
					<tr height="10">
						<td width="20"></td>
						<td colSpan="3"></td>
					</tr>
					<tr height="27">
						<td width="20"></td>
						<td align="right" colSpan="3"><asp:button id="btnWrite" runat="server" Text="글쓰기" Width="65px" Height="20px"></asp:button><FONT face="굴림">&nbsp;&nbsp;</FONT></td>
					</tr>
					<TR>
						<td width="20"></td>
						<td vAlign="top" colSpan="3"><asp:datagrid id="DataGrid1" runat="server" PageSize="15" BorderColor="#336666" BorderStyle="None"
								BorderWidth="0px" BackColor="White" CellPadding="4" GridLines="Horizontal" AutoGenerateColumns="False" AllowCustomPaging="True"
								AllowPaging="True" Width="800px">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#339966"></SelectedItemStyle>
								<ItemStyle Height="10px" ForeColor="#333333" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="ControlDarkDark" BackColor="LightGray"></HeaderStyle>
								<FooterStyle Font-Bold="True" ForeColor="Red"></FooterStyle>
								<Columns>
									<asp:HyperLinkColumn DataNavigateUrlField="CFreeBoardIndex" DataNavigateUrlFormatString="FreeAdjustment.aspx?CFreeBoardIndex={0}"
										DataTextField="Title" HeaderText="&amp;nbsp;제&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;목">
										<HeaderStyle HorizontalAlign="Center" Height="20px" Width="420px" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
									</asp:HyperLinkColumn>
									<asp:BoundColumn DataField="RegistrationPerson" HeaderText="작&amp;nbsp;성&amp;nbsp;자">
										<HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RegistrationDate" HeaderText="작&amp;nbsp;&amp;nbsp;성&amp;nbsp;&amp;nbsp;일">
										<HeaderStyle HorizontalAlign="Center" Width="200px" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Hits" HeaderText="조회수">
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
						<td height="20" width="20"></td>
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
