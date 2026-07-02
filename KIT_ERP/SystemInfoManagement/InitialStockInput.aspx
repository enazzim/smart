<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp1" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="InitialStockInput.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.SystemInfoManagement.InitialStockInput" codePage="949" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>InitialStockInput</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" border="0" style="POSITION: absolute; TOP: 10px; LEFT: 10px">
				<TR>
					<TD width="20"></TD>
					<TD align="center">
						<FIELDSET style="BORDER-BOTTOM: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-TOP: dimgray 2px solid; BORDER-RIGHT: dimgray 2px solid; 90BORDER-RIGHT: black 1px solid">
							<FIELDSET style="BORDER-BOTTOM: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-RIGHT: dimgray 2px solid"
								align="middle" DESIGNTIMEDRAGDROP="255"><LEGEND style="FONT-SIZE: 10pt" align="left">[&nbsp;양식파일 
									다운로드/업로드&nbsp;]
								</LEGEND>
								<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="800" border="0">
									<TR>
										<TD align="right" width="50" height="35">창고&nbsp;
										</TD>
										<TD align="left" colSpan="7" height="35"><asp:dropdownlist id="DropDownList1" runat="server" Width="112px">
												<asp:ListItem Value="영업1창고">영업1창고</asp:ListItem>
												<asp:ListItem Value="영업2창고">영업2창고</asp:ListItem>
												<asp:ListItem Value="영업3창고">영업3창고</asp:ListItem>
												<asp:ListItem Value="생산창고">생산창고</asp:ListItem>
												<asp:ListItem Value="원자재창고">원자재창고</asp:ListItem>
												<asp:ListItem Value="납품창고">납품창고</asp:ListItem>
												<asp:ListItem Value="외주창고">외주창고</asp:ListItem>
												<asp:ListItem Value="보용품창고">보용품창고</asp:ListItem>
												<asp:ListItem Value="매입매출">매입매출</asp:ListItem>
											</asp:dropdownlist>&nbsp;&nbsp;&nbsp;
										</TD>
										<TD align="right" height="35">
											<asp:linkbutton style="Z-INDEX: 0" id="LinkButton1" runat="server" Font-Size="9pt">이력보기</asp:linkbutton>&nbsp;
											<asp:Button id="Button3" runat="server" Width="100px" Text="현재창고보기" Height="20px"></asp:Button>&nbsp;
											<asp:button id="Button2" runat="server" Width="150px" Text="Excel 양식 다운로드" Height="20px"></asp:button>&nbsp;&nbsp;
										</TD>
									</TR>
									<TR>
										<TD align="center" height="8"></TD>
										<TD align="center" colSpan="7" height="8"></TD>
										<TD align="center" height="8"></TD>
									</TR>
								</TABLE>
							</FIELDSET>
							<BR>
							<LEGEND style="FONT-SIZE: 10pt" align="left">
								[&nbsp;재고상황 ]
							</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD align="center" height="40">
										<igtbl:ultrawebgrid id="uwgStock" runat="server" DESIGNTIMEDRAGDROP="533" Width="800px" Height="425px">
											<DisplayLayout StationaryMargins="Header" AllowSortingDefault="OnClient" RowHeightDefault="20px"
												Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti"
												BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="uwgStock"
												TableLayout="Fixed" CellClickActionDefault="RowSelect" ExpandableDefault="No">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</AddNewBox>
												<Pager PageSize="19" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Center"
													BackColor="LightGray" Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="425px"></FrameStyle>
												<FooterStyleDefault BackColor="Transparent">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<RowExpAreaStyleDefault BackColor="White"></RowExpAreaStyleDefault>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None" BackColor="White"></EditCellStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" Font-Bold="True" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
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
									<TD height="40">&nbsp;
										<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="800" border="0">
											<TR>
												<TD align="right" width="50" height="35">창고&nbsp;
												</TD>
												<TD align="right" width="50" height="35">
													<asp:dropdownlist id="Dropdownlist2" runat="server" Width="112px">
														<asp:ListItem Value="영업1창고">영업1창고</asp:ListItem>
														<asp:ListItem Value="영업2창고">영업2창고</asp:ListItem>
														<asp:ListItem Value="영업3창고">영업3창고</asp:ListItem>
														<asp:ListItem Value="생산창고">생산창고</asp:ListItem>
														<asp:ListItem Value="원자재창고">원자재창고</asp:ListItem>
														<asp:ListItem Value="납품창고">납품창고</asp:ListItem>
														<asp:ListItem Value="외주창고">외주창고</asp:ListItem>
														<asp:ListItem Value="보용품창고">보용품창고</asp:ListItem>
														<asp:ListItem Value="매입매출">매입매출</asp:ListItem>
													</asp:dropdownlist></TD>
												<TD align="right" width="50" height="35">경로&nbsp;
												</TD>
												<TD align="left" colSpan="7" height="35"><INPUT id="File1" style="BORDER-BOTTOM: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BACKGROUND-COLOR: #eeeee9; WIDTH: 420px; HEIGHT: 22px; BORDER-TOP: dimgray 1px solid; BORDER-RIGHT: dimgray 1px solid"
														type="file" size="61" name="File1" runat="server">
												</TD>
												<TD align="right" height="35">
													<asp:Button id="Button1" runat="server" Width="83px" Height="20px" Text="변경적용"></asp:Button>&nbsp;&nbsp;
												</TD>
											</TR>
											<TR>
												<TD align="center" height="8"></TD>
												<TD align="center" height="8"></TD>
												<TD align="center" height="8"></TD>
												<TD align="center" colSpan="7" height="8">
													<igtblexp1:UltraWebGridExcelExporter id="UltraWebGridExcelExporter1" runat="server" DownloadName="Stock.XLS" WorksheetName="Stock"></igtblexp1:UltraWebGridExcelExporter></TD>
												<TD align="center" height="8"></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
