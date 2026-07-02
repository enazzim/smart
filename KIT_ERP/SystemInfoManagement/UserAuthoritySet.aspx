<%@ Page language="c#" Codebehind="UserAuthoritySet.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.SystemInfoManagement.UserAuthoritySet" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AuthoritySetting</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../GridCheck.js"></script>
		<script language="javascript" src="../PopupWindows.js"></script>
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#f6f7f7">
		<form id="Form1" method="post" runat="server">
			<FONT face="굴림">
				<TABLE id="Table" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" cellSpacing="0"
					cellPadding="0">
					<TR>
						<TD style="HEIGHT: 77px">
							<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; Z-INDEX: 103; BORDER-LEFT: #696969 2px solid; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top">
									<asp:label id="searchTitle" Runat="server" Font-Size="10pt" text="[검색조건]">[검색조건]</asp:label></LEGEND>
								<TABLE id="Table2" style="HEIGHT: 2px" cellSpacing="0" cellPadding="0" width="800">
									<TR>
										<TD align="right" width="40" height="30"><FONT face="굴림">모&nbsp; 듈&nbsp; </FONT>
										</TD>
										<TD align="right" width="100" height="30">
											<asp:dropdownlist id="DropDownList1" runat="server" Width="100px">
												<asp:ListItem Value=" ">- 선 택 -</asp:ListItem>
												<asp:ListItem Value="영업">영업</asp:ListItem>
												<asp:ListItem Value="생산">생산</asp:ListItem>
												<asp:ListItem Value="구매/외주">구매/외주</asp:ListItem>
												<asp:ListItem Value="경영정보">경영정보</asp:ListItem>
												<asp:ListItem Value="통계및지표">통계및지표</asp:ListItem>
												<asp:ListItem Value="현황관리">현황관리</asp:ListItem>
												<asp:ListItem Value="기준정보">기준정보</asp:ListItem>
												<asp:ListItem Value="커뮤니티">커뮤니티</asp:ListItem>
												<asp:ListItem Value="시스템정보">시스템정보</asp:ListItem>
											</asp:dropdownlist></TD>
										<TD align="right" colSpan="3" height="30"><FONT face="굴림"></FONT><FONT face="굴림">&nbsp;</FONT>
											<asp:button id="bt_Search" runat="server" Width="60px" Text="검색" Height="20px"></asp:button><FONT face="굴림">&nbsp;
											</FONT>
										</TD>
									</TR>
									<TR>
										<TD align="right" width="70" colSpan="5" height="8"><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"></FONT></TD>
									</TR>
								</TABLE>
							</FIELDSET>
						</TD>
					</TR>
					<TR>
						<TD>
							<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top">
									<asp:label id="Label2" Runat="server" Font-Size="10pt" Width="72px">[검색결과]</asp:label></LEGEND>
								<TABLE id="Table3" style="WIDTH: 806px; HEIGHT: 446px" cellSpacing="1" cellPadding="1"
									width="806" border="0">
									<TR>
										<TD height="400" vAlign="top">
											<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="410px">
												<DisplayLayout ColWidthDefault="130px" StationaryMargins="Header" RowHeightDefault="20px" Version="3.00"
													SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" BorderCollapseDefault="Separate"
													AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="DataGrid1" TableLayout="Fixed"
													CellClickActionDefault="RowSelect" AllowUpdateDefault="Yes">
													<AddNewBox>
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														</Style>
													</AddNewBox>
													<Pager>
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														</Style>
													</Pager>
													<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray" Height="25px">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</HeaderStyleDefault>
													<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="Verdana" BorderColor="DimGray"
														BorderStyle="Solid" BackColor="Silver" Height="410px"></FrameStyle>
													<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</FooterStyleDefault>
													<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
													<SelectedRowStyleDefault Cursor="Hand" Font-Bold="True" BorderColor="Gray" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
													<RowAlternateStyleDefault Cursor="Hand"></RowAlternateStyleDefault>
													<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
														<Padding Left="3px"></Padding>
														<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
													</RowStyleDefault>
												</DisplayLayout>
												<Bands>
													<igtbl:UltraGridBand></igtbl:UltraGridBand>
												</Bands>
											</igtbl:ultrawebgrid></TD>
									</TR>
									<TR>
										<TD align="right" height="30">&nbsp;
											<asp:button id="bt_Register" runat="server" Width="60px" Text="설   정" Height="20px"></asp:button></TD>
									</TR>
								</TABLE>
							</FIELDSET>
						</TD>
					</TR>
				</TABLE>
				&nbsp; </FONT>
		</form>
	</body>
</HTML>
