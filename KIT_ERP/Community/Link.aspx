<%@ Page language="c#" Codebehind="Link.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.Link" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>관련 SITE</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!--
			function Delete_Check()
			{
				if ( confirm("선택한 항목을 삭제 하시겠습니까?") )
					return true;
				else
					return false;
			}
			function add()
			{
				if(confirm("새로운 Category를 추가하시겠습니까?"))
					return true;
				else
					return false;
			}
			function del()
			{
				if(confirm("선택된 Category의 항목 모두삭제됩니다. 삭제하시겠습니까?"))
					return true;
				else
					return false;
			}
		//-->
		</script>
	</HEAD>
	<body bgColor="#f7f6f6">
		<form id="Form1" method="post" encType="multipart/form-data" runat="server">
			<TABLE id="Table2" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" borderColor="#0"
				height="550" cellSpacing="0" cellPadding="0" width="780" border="0">
				<tr height="35">
					<td style="HEIGHT: 38px" width="20"><FONT face="굴림"></FONT></td>
					<td style="HEIGHT: 38px"><FONT face="굴림"></FONT></td>
				</tr>
				<TR>
					<td width="20"><FONT face="굴림"></FONT></td>
					<TD vAlign="top" align="right">
						<div align="left"><FONT face="굴림">&nbsp;</FONT><asp:dropdownlist id="DropDownList1" runat="server" AutoPostBack="True">
								<asp:ListItem Value="0" Selected="True">Category</asp:ListItem>
							</asp:dropdownlist>
							<asp:textbox id="TextBox1" runat="server" Width="130px" Height="21px" BorderColor="Gray" BorderStyle="Solid"
								BorderWidth="1px"></asp:textbox><FONT face="굴림">&nbsp; </FONT>
							<asp:button id="Button3" runat="server" Width="65px" Height="20px" Text="추  가" CausesValidation="False"></asp:button><FONT face="굴림">&nbsp;</FONT><asp:button id="Button4" runat="server" Width="65px" Height="20px" Text="삭  제" CausesValidation="False"></asp:button><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="새로 등록할 Category명을 입력해주세요!"
								ControlToValidate="TextBox1" Display="None"></asp:requiredfieldvalidator><asp:validationsummary id="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
								ShowSummary="False"></asp:validationsummary><asp:datagrid id="DataGrid1" runat="server" Width="800px" AllowCustomPaging="True" AllowPaging="True"
								PageSize="20" AutoGenerateColumns="False">
								<FooterStyle Font-Bold="True" ForeColor="Red"></FooterStyle>
								<ItemStyle Height="29px"></ItemStyle>
								<HeaderStyle Font-Bold="True" Height="25px" ForeColor="ControlDarkDark" BackColor="LightGray"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="선택">
										<HeaderStyle HorizontalAlign="Center" Width="5%" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox id="CheckBox1" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Classification" HeaderText="Category">
										<HeaderStyle HorizontalAlign="Center" Width="12%" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SiteName" HeaderText="사이트 명(설명)">
										<HeaderStyle HorizontalAlign="Center" Width="40%" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
									<asp:HyperLinkColumn Target="_blank" DataNavigateUrlField="Link" DataTextField="Link" HeaderText="링크주소">
										<HeaderStyle HorizontalAlign="Center" Width="40%" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
									</asp:HyperLinkColumn>
								</Columns>
								<PagerStyle Height="20" VerticalAlign="Middle" Font-Bold="True" HorizontalAlign="Center" ForeColor="ControlDarkDark"
									BackColor="LightGray" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></div>
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="800" border="0">
							<TR>
								<TD width="280" height="30"><FONT face="굴림"></FONT></TD>
								<TD align="center" width="280" height="30"><asp:label id="lblPageInfo" runat="server">lblPageInfo</asp:label></TD>
								<TD align="right" height="30">
									<P><asp:button id="Button1" runat="server" Width="80" Height="20px" Text="추  가" CausesValidation="False"></asp:button><FONT face="굴림">&nbsp;</FONT><asp:button id="Button2" runat="server" Width="80" Height="20px" Text="삭  제" CausesValidation="False"></asp:button></P>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
