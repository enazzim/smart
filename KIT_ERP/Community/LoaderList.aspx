<%@ Page language="c#" Codebehind="LoaderList.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.LoaderList" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>LoaderList</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<table style="Z-INDEX: 101; POSITION: absolute; TOP: 10px; LEFT: 10px" borderColor="darkgray"
				cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
				<TBODY>
					<tr height="10">
						<td width="20"></td>
						<td colSpan="3"><FONT face="±¼¸²"></FONT></td>
					</tr>
					<tr height="27">
						<td width="20"></td>
						<td align="right" colSpan="3"><asp:button id="btnWrite" runat="server" Text="±Û¾²±â" Width="65px" Height="20px"></asp:button><FONT face="±¼¸²">&nbsp;&nbsp;</FONT></td>
					</tr>
					<TR>
						<td vAlign="top" colSpan="3"><asp:datagrid id="DataGrid1" runat="server" Width="800px" AllowPaging="True" AutoGenerateColumns="False"
								GridLines="Horizontal" CellPadding="4" BackColor="White" BorderWidth="0px" BorderStyle="None" BorderColor="#336666"
								PageSize="15">
								<FooterStyle Font-Bold="True" ForeColor="Red"></FooterStyle>
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#339966"></SelectedItemStyle>
								<ItemStyle Height="10px" ForeColor="#333333" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="ControlDarkDark" BackColor="LightGray"></HeaderStyle>
								<Columns>
									<asp:HyperLinkColumn Target="_self" DataNavigateUrlField="seq" DataNavigateUrlFormatString="LoaderContent.aspx?seq={0}"
										DataTextField="title" HeaderText="&amp;nbsp;Á¦&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;¸ñ">
										<HeaderStyle HorizontalAlign="Center" Height="20px" Width="500px" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
									</asp:HyperLinkColumn>
									<asp:BoundColumn DataField="writer" HeaderText="ÀÛ&amp;nbsp;¼º&amp;nbsp;ÀÚ">
										<HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="transdate" HeaderText="ÀÛ&amp;nbsp;&amp;nbsp;¼º&amp;nbsp;&amp;nbsp;ÀÏ">
										<HeaderStyle HorizontalAlign="Center" Width="200px" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="ControlDarkDark" BackColor="LightGray"
									Mode="NumericPages"></PagerStyle>
							</asp:datagrid></td>
					</TR>
					<TR>
						<TD vAlign="top" align="center" colSpan="3"><FONT face="±¼¸²"></FONT></TD>
					</TR>
					<tr height="31">
						<td align="left" width="300"><FONT face="±¼¸²">&nbsp;&nbsp; </FONT>
						</td>
						<td align="center" width="300"></td>
						<td width="300"><FONT face="±¼¸²"></FONT></td>
					</tr>
				</TBODY>
			</table>
		</form>
	</body>
</HTML>
