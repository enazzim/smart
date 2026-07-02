<%@ Register TagPrefix="taeyo" Namespace="TaeyoNetLib" Assembly="TaeyoNetLib" %>
<%@ Page language="c#" Codebehind="WorkDailyList.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.WorkDailyList" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WorkDailyList</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body BGCOLOR="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<table style="Z-INDEX: 101; POSITION: absolute; TOP: 10px; LEFT: 10px" borderColor="darkgray"
				cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
				<TBODY>
					<tr height="10">
						<td colSpan="3">
							<table id="table1" cellSpacing="0" cellPadding="0" border="0" width="800">
								<tr>
									<td width="50" align="right">년도</td>
									<td width="100" align="left">
										<asp:DropDownList id="DropDownList1" runat="server">
											<asp:ListItem Value="-선 택-">-선 택-</asp:ListItem>
											<asp:ListItem Value="2007">2007</asp:ListItem>
											<asp:ListItem Value="2008">2008</asp:ListItem>
											<asp:ListItem Value="2009">2009</asp:ListItem>
											<asp:ListItem Value="2010">2010</asp:ListItem>
											<asp:ListItem Value="2011">2011</asp:ListItem>
											<asp:ListItem Value="2012">2012</asp:ListItem>
											<asp:ListItem Value="2013">2013</asp:ListItem>
											<asp:ListItem Value="2014">2014</asp:ListItem>
										</asp:DropDownList></td>
									<td width="50" align="right">월</td>
									<td width="100" align="left">
										<asp:DropDownList id="DropDownList2" runat="server">
											<asp:ListItem Value="-선 택-">-선 택-</asp:ListItem>
											<asp:ListItem Value="1">1</asp:ListItem>
											<asp:ListItem Value="2">2</asp:ListItem>
											<asp:ListItem Value="3">3</asp:ListItem>
											<asp:ListItem Value="4">4</asp:ListItem>
											<asp:ListItem Value="5">5</asp:ListItem>
											<asp:ListItem Value="6">6</asp:ListItem>
											<asp:ListItem Value="7">7</asp:ListItem>
											<asp:ListItem Value="8">8</asp:ListItem>
											<asp:ListItem Value="9">9</asp:ListItem>
											<asp:ListItem Value="10">10</asp:ListItem>
											<asp:ListItem Value="11">11</asp:ListItem>
											<asp:ListItem Value="12">12</asp:ListItem>
										</asp:DropDownList></td>
									<td width="50" align="right">일</td>
									<td width="100" align="left">
										<asp:DropDownList id="DropDownList3" runat="server">
											<asp:ListItem Value="-선 택-">-선 택-</asp:ListItem>
											<asp:ListItem Value="1">1</asp:ListItem>
											<asp:ListItem Value="2">2</asp:ListItem>
											<asp:ListItem Value="3">3</asp:ListItem>
											<asp:ListItem Value="4">4</asp:ListItem>
											<asp:ListItem Value="5">5</asp:ListItem>
											<asp:ListItem Value="6">6</asp:ListItem>
											<asp:ListItem Value="7">7</asp:ListItem>
											<asp:ListItem Value="8">8</asp:ListItem>
											<asp:ListItem Value="9">9</asp:ListItem>
											<asp:ListItem Value="10">10</asp:ListItem>
											<asp:ListItem Value="11">11</asp:ListItem>
											<asp:ListItem Value="12">12</asp:ListItem>
											<asp:ListItem Value="13">13</asp:ListItem>
											<asp:ListItem Value="14">14</asp:ListItem>
											<asp:ListItem Value="15">15</asp:ListItem>
											<asp:ListItem Value="16">16</asp:ListItem>
											<asp:ListItem Value="17">17</asp:ListItem>
											<asp:ListItem Value="18">18</asp:ListItem>
											<asp:ListItem Value="19">19</asp:ListItem>
											<asp:ListItem Value="20">20</asp:ListItem>
											<asp:ListItem Value="21">21</asp:ListItem>
											<asp:ListItem Value="22">22</asp:ListItem>
											<asp:ListItem Value="23">23</asp:ListItem>
											<asp:ListItem Value="24">24</asp:ListItem>
											<asp:ListItem Value="25">25</asp:ListItem>
											<asp:ListItem Value="26">26</asp:ListItem>
											<asp:ListItem Value="27">27</asp:ListItem>
											<asp:ListItem Value="28">28</asp:ListItem>
											<asp:ListItem Value="29">29</asp:ListItem>
											<asp:ListItem Value="30">30</asp:ListItem>
											<asp:ListItem Value="31">31</asp:ListItem>
										</asp:DropDownList></td>
									<td width="65" align="left">
										<asp:button id="btSearch" runat="server" Height="20px" Width="65px" Text="검색"></asp:button></td>
									<td width="285" align="right"><asp:button id="btnWrite" runat="server" Text="일지작성" Width="65px" Height="20px"></asp:button></td>
								</tr>
							</table>
						</td>
					</tr>
					<TR>
						<td vAlign="top" colSpan="3"><asp:datagrid id="DataGrid1" runat="server" PageSize="15" BorderColor="#336666" BorderStyle="None"
								BorderWidth="0px" BackColor="White" CellPadding="4" GridLines="Horizontal" AutoGenerateColumns="False" AllowPaging="True"
								Width="800px">
								<FooterStyle Font-Bold="True" ForeColor="Red"></FooterStyle>
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#339966"></SelectedItemStyle>
								<ItemStyle Height="10px" ForeColor="#333333" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="ControlDarkDark" BackColor="LightGray"></HeaderStyle>
								<Columns>
									<asp:HyperLinkColumn DataNavigateUrlField="seq" DataNavigateUrlFormatString="WorkDailyContent.aspx?seq={0}"
										DataTextField="title" HeaderText="&amp;nbsp;제&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;목">
										<HeaderStyle HorizontalAlign="Center" Height="20px" Width="500px" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
									</asp:HyperLinkColumn>
									<asp:BoundColumn DataField="writer" HeaderText="작&amp;nbsp;성&amp;nbsp;자">
										<HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="transdate" HeaderText="작&amp;nbsp;&amp;nbsp;성&amp;nbsp;&amp;nbsp;일">
										<HeaderStyle HorizontalAlign="Center" Width="200px" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="ControlDarkDark" BackColor="LightGray"
									Mode="NumericPages"></PagerStyle>
							</asp:datagrid></td>
					</TR>
					<TR>
						<TD vAlign="top" align="center" colSpan="3"><FONT face="굴림"></FONT></TD>
					</TR>
					<tr height="31">
						<td align="left" width="300"><FONT face="굴림">&nbsp;&nbsp; </FONT>
						</td>
						<td align="center" width="300"></td>
						<td width="300"><FONT face="굴림"></FONT></td>
					</tr>
				</TBODY>
			</table>
		</form>
	</body>
</HTML>
