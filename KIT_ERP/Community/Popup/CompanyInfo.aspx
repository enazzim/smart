<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Page language="c#" Codebehind="CompanyInfo.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.Popup.CompanyInfo" %>
<%@ Register TagPrefix="uc1" TagName="CompanyControl" Src="../CompanyControl/CompanyControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>거래처 정보</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../StyleSheet1.css" type="text/css" rel="stylesheet">
		<LINK href="../../LinkLine.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<FONT face="굴림"></FONT>
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" border="0" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px">
				<TR>
					<TD align="center" height="30"><FONT face="굴림"><asp:label id="Label1" runat="server" Font-Bold="True" ForeColor="SteelBlue" Font-Size="13pt"></asp:label></FONT></TD>
				</TR>
				<TR>
					<TD align="left" height="30">
						<table id="table2" cellpadding="0" cellspacing="0" border="0">
							<tr>
								<td align="left" width="250">
									<uc1:CompanyControl id="CSC1" runat="server"></uc1:CompanyControl>
								</td>
								<td align="right" width="550">
									<asp:button id="btSearch" runat="server" Width="60px" Height="20px" Text="검  색"></asp:button>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 0pt" vAlign="top" align="center"><asp:datagrid id="DataGrid1" runat="server" Font-Size="10pt" CssClass="StyleSheet1.css" GridLines="Horizontal"
							AutoGenerateColumns="False" Width="800px" AllowCustomPaging="True" AllowPaging="True">
							<ItemStyle Height="30px"></ItemStyle>
							<HeaderStyle Font-Bold="True" Height="25px" ForeColor="ControlDarkDark" BackColor="LightGray"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="선택">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemTemplate>
										<asp:CheckBox id="CheckBox1" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="CompanyInfoIndex" HeaderText="Index No.">
									<HeaderStyle HorizontalAlign="Center" Width="9%" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:HyperLinkColumn DataTextField="CompanyName" HeaderText="거래처명">
									<HeaderStyle HorizontalAlign="Center" Width="20%" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:HyperLinkColumn>
								<asp:BoundColumn DataField="PresidentName" HeaderText="대표자">
									<HeaderStyle HorizontalAlign="Center" Width="8%" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="BusinessRegistrationNum" HeaderText="사업자 번호">
									<HeaderStyle HorizontalAlign="Center" Width="18%" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="BusinessCompanyAddress" HeaderText="사업장 주소">
									<HeaderStyle HorizontalAlign="Center" Width="40%" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<tr>
					<td align="center"><asp:button id="Button2" runat="server" Width="60px" Text="등  록" Height="20px"></asp:button><FONT face="굴림">&nbsp;</FONT>
						<asp:button id="Button1" runat="server" Width="60px" Text="창닫기" Height="20px"></asp:button></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
