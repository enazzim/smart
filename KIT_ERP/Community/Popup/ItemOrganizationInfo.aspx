<%@ Register TagPrefix="uc1" TagName="ItemSearch" Src="../ItemControl/ItemSearch.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ParentItemSearch" Src="../ParentItemControl/ParentItemSearch.ascx" %>
<%@ Page language="c#" Codebehind="ItemOrganizationInfo.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.Popup.ItemOrganizationInfo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>품목구성 정보</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript">
		<!--
		function ResettxtBox()
		{
			ResetTextBox();
			ResetBox();
		}
		//-->
		</SCRIPT>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 0px; WIDTH: 680px; POSITION: absolute; TOP: 0px; HEIGHT: 237px"
				cellSpacing="1" cellPadding="1" width="592" border="0">
				<TR>
					<TD align="center" height="30"><asp:label id="Label1" runat="server" Font-Bold="True" ForeColor="SteelBlue" Font-Size="13pt"></asp:label></TD>
				</TR>
				<TR>
					<TD align="center" height="30">
						<table id="table2" cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td align="left" width="600">
									<uc1:ParentItemSearch id="ISC1" runat="server"></uc1:ParentItemSearch></td>
								<td align="right" width="80"><INPUT style="WIDTH: 60px; HEIGHT: 20px" onclick="ResettxtBox()" type="button" value="초기화">
								</td>
							</tr>
							<tr>
								<td align="left" width="600">
									<uc1:ItemSearch id="ISC2" runat="server"></uc1:ItemSearch></td>
								<td align="right" width="80"><asp:button id="btSearch" runat="server" Text="검  색" Height="20px" Width="60px"></asp:button></td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 0pt; HEIGHT: 179px" vAlign="top" align="center"><asp:datagrid id="DataGrid1" runat="server" Font-Size="10pt" Width="680px" AllowPaging="True"
							AllowCustomPaging="True" GridLines="Horizontal" AutoGenerateColumns="False">
							<ItemStyle Height="30px"></ItemStyle>
							<HeaderStyle Font-Bold="True" Height="25px" ForeColor="ControlDarkDark" BackColor="LightGray"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="선택">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="CheckBox1" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="ItemOrganizationInfoIndex" HeaderText="Index No.">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ParentItemNum" HeaderText="모품번호">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ChildItemNum" HeaderText="자품번호">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="NeedQuantityNumerator" HeaderText="소요량분자">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="NeedQuantityDenominator" HeaderText="소요량분모">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR>
					<TD align="center"><asp:button id="Button1" runat="server" Text="등  록" Height="20px" Width="60px"></asp:button><FONT face="굴림">&nbsp;
						</FONT>
						<asp:button id="Button2" runat="server" Text="창닫기" Height="20px" Width="60px"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
