<%@ Page language="c#" Codebehind="BuyUnitCostInfo.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.Popup.BuyUnitCostInfo" %>
<%@ Register TagPrefix="uc1" TagName="CompanyControl" Src="../CompanyControl/CompanyControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearch" Src="../ItemControl/ItemSearch.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ItemInfo</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript">
		<!--
		function ResettxtBox()
		{	
			ResetBox();
			ResetComBox();
		}
		//-->
		</SCRIPT>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="774" border="0" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px">
				<TR>
					<TD align="center" height="30">
						<asp:Label id="Label1" runat="server" Font-Size="13pt" ForeColor="SteelBlue" Font-Bold="True"></asp:Label></TD>
				</TR>
				<TR>
					<TD align="left" height="30">
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" border="0">
							<TR>
								<TD width="600">
									<uc1:CompanyControl id="CSC1" runat="server"></uc1:CompanyControl></TD>
								<TD align="right" width="174"><INPUT style="WIDTH: 60px; HEIGHT: 20px" onclick="ResettxtBox()" type="button" value="초기화"></TD>
							</TR>
							<TR>
								<TD width="600">
									<uc1:ItemSearch id="ISC1" runat="server"></uc1:ItemSearch></TD>
								<TD align="right" width="174"><FONT face="굴림">
										<asp:button id="btSearch" runat="server" Width="60px" Height="20px" Text="검  색"></asp:button></FONT></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" style="FONT-SIZE: 0pt">
						<asp:DataGrid id="DataGrid1" runat="server" Width="768px" AutoGenerateColumns="False" Font-Size="10pt"
							GridLines="Horizontal" AllowCustomPaging="True" AllowPaging="True">
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
								<asp:BoundColumn DataField="UnitCostInfoIndex" HeaderText="Index No.">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ItemName" HeaderText="품목명">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ItemNum" HeaderText="품목번호">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CompanyName" HeaderText="거래처">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PropertyClassification" HeaderText="자산분류">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Standard" HeaderText="규격">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="UnitCostDistinction" HeaderText="단가구분">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></TD>
				</TR>
				<TR>
					<TD align="center">
						<asp:Button id="Button1" runat="server" Width="60px" Text="등  록" Height="20px"></asp:Button><FONT face="굴림">&nbsp;
						</FONT>
						<asp:Button id="Button2" runat="server" Width="60px" Text="창닫기" Height="20px"></asp:Button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
