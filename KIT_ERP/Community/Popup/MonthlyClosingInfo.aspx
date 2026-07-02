<%@ Page language="c#" Codebehind="MonthlyClosingInfo.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.Popup.MonthlyClosingInfo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>월마감 정보</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../StyleSheet1.css" type="text/css" rel="stylesheet">
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
					<TD style="FONT-SIZE: 0pt" vAlign="top" align="center"><asp:datagrid id="DataGrid1" runat="server" Font-Size="10pt" CssClass="StyleSheet1.css" GridLines="Horizontal"
							AutoGenerateColumns="False" Width="560px">
							<ItemStyle Height="30px"></ItemStyle>
							<HeaderStyle Font-Bold="True" Height="25px" ForeColor="ControlDarkDark" BackColor="LightGray"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="선택">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="CheckBox1" runat="server"></asp:CheckBox>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Center" VerticalAlign="Middle"></FooterStyle>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="MonthClosingInfoIndex" HeaderText="Index No.">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AffairDistinction" HeaderText="업무구분">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ClosingYear" HeaderText="년도">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ClosingMonth" HeaderText="월">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ClosingPersonID" HeaderText="마감지시자ID">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ClosingPerson" HeaderText="마감지시자">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
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
