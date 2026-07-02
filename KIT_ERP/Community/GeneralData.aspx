<%@ Page language="c#" Codebehind="GeneralData.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.GeneralData" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>일반 자료실</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<table id="a" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" borderColor="darkgray"
				cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
				<tr height="10">
					<td width="800" colSpan="3"><FONT face="굴림"></FONT></td>
				</tr>
				<tr height="27">
					<TD style="WIDTH: 70px" align="center"><FONT face="굴림">Categoy</FONT></TD>
					<TD align="left" width="330"><asp:dropdownlist id="DropDownList1" runat="server" AutoPostBack="True" BackColor="#EEEEE9"></asp:dropdownlist><asp:button id="Button1" runat="server" Text="삭  제" Width="65px" Height="20px"></asp:button></TD>
					<td align="right" width="400"><asp:button id="btnWrite" runat="server" Text="글쓰기" Width="65px" Height="20px"></asp:button><FONT face="굴림">&nbsp;&nbsp;</FONT>
					</td>
				</tr>
				<TR>
					<td vAlign="top" colSpan="3"><asp:datagrid id="DataGrid1" runat="server" BackColor="White" Width="800px" AllowPaging="True"
							AllowCustomPaging="True" AutoGenerateColumns="False" GridLines="Horizontal" CellPadding="0" BorderWidth="0px" BorderStyle="None"
							PageSize="15">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#339966"></SelectedItemStyle>
							<ItemStyle Height="32px" ForeColor="#333333" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="ControlDarkDark" BackColor="LightGray"></HeaderStyle>
							<FooterStyle Font-Bold="True" ForeColor="Red"></FooterStyle>
							<Columns>
								<asp:HyperLinkColumn DataNavigateUrlField="CGeneralDataIndex" DataNavigateUrlFormatString="GnAdjustment.aspx?CGeneralDataIndex={0}"
									DataTextField="Title" HeaderText="제&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;목">
									<HeaderStyle Height="20" HorizontalAlign="Center" Width="420px" VerticalAlign="Middle"></HeaderStyle>
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
							<PagerStyle Height="20" Font-Bold="True" HorizontalAlign="Center" ForeColor="ControlDarkDark"
								BackColor="LightGray" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</TR>
				<TR>
					<TD align="center" width="800" colSpan="3"><FONT face="굴림"><asp:label id="lblPageInfo" runat="server"></asp:label></FONT></TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
