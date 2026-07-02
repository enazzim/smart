<%@ Page language="c#" Codebehind="PDS.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Homepage.PDS" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PDS</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!--
			function del()
			{
				if(confirm("선택된 Category의 항목도 모두 삭제됩니다. 삭제하시겠습니까?"))
					return true;
				else
					return false;
			}
		//-->
		</script>
	</HEAD>
	<body bgColor="whitesmoke" MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="FONT-SIZE: 0pt; Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px"
				cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
				<TR height="30">
					<TD vAlign="middle" width="10"><FONT face="굴림"></FONT></TD>
					<TD vAlign="middle" align="center" colSpan="3">
						<TABLE id="Table2" style="FONT-SIZE: 0pt" height="30" cellSpacing="0" cellPadding="0" width="150"
							border="0">
							<TR height="30">
								<TD style="BORDER-BOTTOM: dimgray 2px solid" vAlign="bottom" align="center" height="30"><asp:label id="Label1" runat="server" Font-Bold="True" ForeColor="SteelBlue" Font-Size="11pt"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR height="27">
					<TD width="10"><FONT face="굴림"></FONT></TD>
					<TD style="FONT-SIZE: 10pt" align="left" colSpan="3"><FONT style="FONT-SIZE: 0pt" face="굴림">
							<TABLE id="Table3" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:dropdownlist id="DropDownList1" runat="server" BackColor="#EEEEE9" AutoPostBack="True"></asp:dropdownlist><asp:button id="Button1" runat="server" Visible="False" Width="65px" Height="20px" Text="삭  제"></asp:button></TD>
									<TD align="right"><asp:button id="btnWrite" runat="server" Width="65px" Height="20px" Text="글쓰기" Enabled="False"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								</TR>
							</TABLE>
						</FONT>
					</TD>
				</TR>
				<TR>
					<TD align="center" width="10"><FONT face="굴림"></FONT></TD>
					<TD style="FONT-SIZE: 0pt" vAlign="top" align="center" colSpan="3"><FONT face="굴림"><asp:datagrid id="DataGrid1" runat="server" Font-Size="9pt" Width="100%" AllowPaging="True" AllowCustomPaging="True"
								GridLines="Horizontal" AutoGenerateColumns="False" BorderStyle="None" BorderWidth="0px">
								<FooterStyle Font-Bold="True" ForeColor="Red"></FooterStyle>
								<ItemStyle Height="32px"></ItemStyle>
								<HeaderStyle Font-Bold="True" Height="20px" ForeColor="ControlDarkDark" BackColor="LightGray"></HeaderStyle>
								<Columns>
									<asp:HyperLinkColumn DataNavigateUrlField="HDataIndex" DataNavigateUrlFormatString="PDS_Adjustment.aspx?HDataIndex={0}"
										DataTextField="Title" HeaderText="제&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;목">
										<HeaderStyle HorizontalAlign="Center" Width="60%" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
									</asp:HyperLinkColumn>
									<asp:BoundColumn DataField="Registrationperson" HeaderText="작 성 자">
										<HeaderStyle HorizontalAlign="Center" Width="13%" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="RegistrationDate" HeaderText="작 성 일">
										<HeaderStyle HorizontalAlign="Center" Width="14%" VerticalAlign="Middle"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Hits" HeaderText="조회수">
										<HeaderStyle HorizontalAlign="Center" Width="13%"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle VerticalAlign="Middle" Height="20px" Font-Bold="True" HorizontalAlign="Center" ForeColor="ControlDarkDark"
									BackColor="LightGray" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></FONT>
						<div align="center">&nbsp;</div>
					</TD>
				</TR>
				<tr height="25">
					<td width="10"><FONT face="굴림"></FONT></td>
					<td style="FONT-SIZE: 10pt" align="center" colSpan="3">
						<DIV align="center"><asp:label id="lblPageInfo" runat="server"></asp:label></DIV>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
