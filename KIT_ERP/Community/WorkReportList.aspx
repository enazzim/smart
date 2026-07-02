<%@ Page language="c#" Codebehind="WorkReportList.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.WorkReportList" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WorkReportList</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../StyleSheet1.css">
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE style="POSITION: absolute; TOP: 0px; LEFT: 10px" id="Table1" border="0" cellSpacing="0"
				borderColor="darkgray" cellPadding="0" width="800" align="center">
				<TR height="10">
					<TD width="800"></TD>
				</TR>
				<TR>
					<TD height="30" vAlign="middle" width="800">
						<table id="table1" border="0" cellSpacing="0" cellPadding="0" width="800">
							<tr>
								<td width="70" align="right"><FONT face="굴림"><asp:label id="Label1" runat="server">작성자 : </asp:label>&nbsp;</FONT></td>
								<td width="100"><asp:dropdownlist id="DropDownList1" runat="server" Width="100px"></asp:dropdownlist></td>
								<td width="70" align="right"><asp:label id="Label2" runat="server">결재여부 :</asp:label><FONT face="굴림">&nbsp;</FONT></td>
								<td width="100"><asp:dropdownlist id="DropDownList2" runat="server" Width="80px">
										<asp:ListItem Value="- 전 체 -">- 전 체 -</asp:ListItem>
										<asp:ListItem Value="기결">기 결</asp:ListItem>
										<asp:ListItem Value="미결">미 결</asp:ListItem>
									</asp:dropdownlist></td>
								<td width="100"><asp:button id="Button2" runat="server" Width="60px" Text="검 색" Height="20px"></asp:button></td>
								<TD align="center" width="360"><asp:HyperLink id="HyperLink1" runat="server" Target="_self" Visible="False">로더 재고 현황</asp:HyperLink></TD>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD height="5" width="800" align="right"><FONT face="굴림">&nbsp;&nbsp;</FONT></TD>
				</TR>
				<TR>
					<TD vAlign="top" width="800"><asp:datagrid id="DataGrid1" runat="server" Width="800px" PageSize="15" BorderColor="#336666"
							BorderStyle="None" BorderWidth="0px" BackColor="White" CellPadding="4" GridLines="Horizontal" AutoGenerateColumns="False"
							AllowPaging="True" AllowCustomPaging="True">
							<FooterStyle Font-Bold="True" ForeColor="Red"></FooterStyle>
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#339966"></SelectedItemStyle>
							<ItemStyle Height="10px" ForeColor="#333333" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="ControlDarkDark" BackColor="LightGray"></HeaderStyle>
							<Columns>
								<asp:HyperLinkColumn DataNavigateUrlField="Index" DataNavigateUrlFormatString="WorkPageMove.aspx?Index={0}"
									DataTextField="ReportDate" HeaderText="일자별 업무일지">
									<HeaderStyle HorizontalAlign="Center" Width="50%" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:HyperLinkColumn>
								<asp:BoundColumn DataField="RegistrationPerson" HeaderText="작 성 자">
									<HeaderStyle HorizontalAlign="Center" Width="30%" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Approval" HeaderText="결재여부">
									<HeaderStyle HorizontalAlign="Center" Width="20%" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="ControlDarkDark" BackColor="LightGray"
								Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD height="30" width="800">
						<table id="table3" border="0" cellSpacing="0" cellPadding="0" width="800">
							<tr>
								<td height="30" width="400"><FONT face="굴림"><asp:label id="lblPageInfo" runat="server"></asp:label></FONT></td>
								<td height="30" width="400" align="right"><FONT face="굴림"><asp:button id="Button1" runat="server" Width="80px" Text="신규등록" Height="20px"></asp:button></FONT></td>
							</tr>
						</table>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
