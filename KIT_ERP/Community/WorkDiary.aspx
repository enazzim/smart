<%@ Page language="c#" Codebehind="WorkDiary.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.WorkDiary" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WorkDiary</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<table style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px" borderColor="darkgray"
				cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
				<TBODY>
					<tr height="10">
						<td colSpan="3"><FONT face="굴림"></FONT></td>
					</tr>
					<tr height="27">
						<td align="right" colSpan="3"><FONT face="굴림"></FONT></td>
					</tr>
					<TR>
						<td vAlign="top" colSpan="3"><asp:datagrid id="DataGrid1" runat="server" Width="800px" PageSize="15" BorderStyle="None" BorderWidth="0px"
								BackColor="White" CellPadding="0" GridLines="Horizontal" AutoGenerateColumns="False" AllowCustomPaging="True" AllowPaging="True">
								<FooterStyle Font-Bold="True" ForeColor="Red"></FooterStyle>
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#339966"></SelectedItemStyle>
								<ItemStyle Height="32px" ForeColor="#333333" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="ControlDarkDark" BackColor="LightGray" Height="25px"></HeaderStyle>
								<Columns>
									<asp:HyperLinkColumn DataNavigateUrlField="Index" DataNavigateUrlFormatString="WorkDiaryPage.aspx?Index={0}"
										DataTextField="WorkDateTitle" HeaderText="일자별 업무일지">
										<HeaderStyle HorizontalAlign="Center" Width="50%" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:HyperLinkColumn>
									<asp:BoundColumn DataField="WorkDiaryWriter" HeaderText="작 성 자">
										<HeaderStyle HorizontalAlign="Center" Width="25%" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Closing" HeaderText="마감여부">
										<HeaderStyle HorizontalAlign="Center" Width="25%" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="Black" BackColor="LightGray"
									Mode="NumericPages"></PagerStyle>
							</asp:datagrid></td>
					</TR>
					<TR>
						<TD vAlign="middle" align="right" colSpan="3" height="25"><asp:button id="Button1" runat="server" Width="80px" Height="20px" Text="신규등록"></asp:button><asp:label id="lblPageInfo" runat="server"></asp:label><FONT face="굴림">&nbsp;</FONT></TD>
					</TR>
				</TBODY>
			</table>
		</form>
	</body>
</HTML>
