<%@ Page language="c#" Codebehind="popupItemNumberFinder.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.QualityInspection.popupItemNumberFinder" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>품목번호 찾기</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="StyleSheet1.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!--
				function OpenerBind(ItemNum, ItemDrawNum, ItemName)	{
					var arr = new Array();
					arr["ItemNum"] = ItemNum;
					arr["ItemDrawNum"] = ItemDrawNum;
					arr["ItemName"] = ItemName;
					window.returnValue = arr;
					window.close();
				}
		//-->
		</script>
	</HEAD>
	<body bottomMargin="0" bgColor="#f7f6f6" leftMargin="0" topMargin="0" rightMargin="0">
		<FORM id="Form1" method="post" runat="server">
			<FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림">
			</FONT>
			<br>
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="800" align="center">
				<TR>
					<TD align="center"><asp:literal id="Literal1" runat="server"></asp:literal><FONT face="굴림"><BR>
						</FONT>
						<BR>
						<asp:datagrid id="DataGrid1" runat="server" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px"
							BackColor="White" CellPadding="4" AutoGenerateColumns="False">
							<SelectedItemStyle Font-Bold="True" ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
							<ItemStyle ForeColor="#003399" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Size="13px" Font-Bold="True" HorizontalAlign="Center" ForeColor="#CCCCFF" VerticalAlign="Middle"
								BackColor="#003399"></HeaderStyle>
							<FooterStyle ForeColor="#003399" BackColor="#99CCCC"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="선택">
									<ItemTemplate>
										<a href="#" onclick='javascript:OpenerBind("<%#DataBinder.Eval(Container.DataItem,"II_ItemNum")%>","<%#DataBinder.Eval(Container.DataItem,"II_ItemDrawNum")%>",
										"<%#DataBinder.Eval(Container.DataItem,"II_ItemName")%>")'>선택</a>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="II_ItemNum" HeaderText="품목번호">
									<HeaderStyle Width="100px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="II_ItemName" HeaderText="품목명">
									<HeaderStyle Width="70px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="II_ItemDrawNum" HeaderText="도면번호">
									<HeaderStyle Width="100px"></HeaderStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Left" ForeColor="#003399" BackColor="#99CCCC" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
				<TR height="35">
					<TD align="left"><FONT face="굴림">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </FONT><INPUT style="WIDTH: 64px; HEIGHT: 21px" type="button" value="닫기" onclick="javascript:self.close()"></TD>
				</TR>
			</TABLE>
		</FORM>
		</FONT>
	</body>
</HTML>
