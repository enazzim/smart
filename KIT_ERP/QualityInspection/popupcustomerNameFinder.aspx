<%@ Page language="c#" Codebehind="popupcustomerNameFinder.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.QualityInspection.popupcustomerNameFinder" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>거래처 명 찾기</title>
		<meta content="False" name="vs_showGrid">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="StyleSheet1.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			<!--
			function OpenerBind(companyName, businessCompanyNum) {
					var arr = new Array();
					arr["companyName"] = companyName;
					arr["businessCompanyNum"] = businessCompanyNum;
					window.returnValue = arr;
					window.close();
			}
			//-->
		</script>
	</HEAD>
	<body bottomMargin="5" bgColor="#f7f6f6" leftMargin="5" topMargin="5" rightMargin="5">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="800" align="center">
				<tr>
					<td align="center"><asp:literal id="Literal1" runat="server"></asp:literal><BR>
						<BR>
						<asp:datagrid id="DataGrid1" runat="server" AutoGenerateColumns="False" CellPadding="4" BackColor="White"
							BorderWidth="1px" BorderStyle="None" BorderColor="#3366CC">
							<SelectedItemStyle Font-Bold="True" ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
							<ItemStyle ForeColor="#003399" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="#CCCCFF" BackColor="#003399"></HeaderStyle>
							<FooterStyle ForeColor="#003399" BackColor="#99CCCC"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="선택">
									<ItemTemplate>
										<a href="#" onclick='javascript:OpenerBind("<%#DataBinder.Eval(Container.DataItem,"CI_CompanyName")%>","<%#DataBinder.Eval(Container.DataItem,"CI_BusinessCompanyNum")%>")'>
											선택</a>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="CI_CompanyName" HeaderText="거래처 명">
									<HeaderStyle Width="100px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CI_PresidentName" HeaderText="대표자명">
									<HeaderStyle Width="70px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CI_BusinessCompanyNum" HeaderText="사업자 등록번호">
									<HeaderStyle Width="100px"></HeaderStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
