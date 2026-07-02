<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="StockPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.CooperationCompany.StockPC" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px; HEIGHT: 550px"
				cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD align="right">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; BORDER-BOTTOM: dimgray 2px solid"
							align="top"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색 ]
							</LEGEND>
							<table id="Table2" cellSpacing="0" cellPadding="0" width="100%" height="30">
								<tr>
									<td vAlign="middle" align="left" width="670" colSpan="4"><FONT face="굴림"></FONT><FONT face="굴림">
											<uc1:ItemSearchControl id="ItemSearchControl1" runat="server"></uc1:ItemSearchControl></FONT></td>
									<TD style="HEIGHT: 43px" vAlign="middle" align="right" width="140"><asp:button id="btnInit" runat="server" Text="초기화" Width="60px" Font-Size="10pt" Height="20px"></asp:button><FONT face="굴림">&nbsp;</FONT><asp:button id="btnSearch" runat="server" Text="검   색" Width="60px" Font-Size="10pt" Height="20px"></asp:button><FONT face="굴림">&nbsp;</FONT></TD>
								</tr>
							</table>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="right" height="400">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 400px"
							align="top"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색결과 ]
							</LEGEND>
							<table id="Table3" cellSpacing="0" cellPadding="0" width="100%">
								<tr>
									<td vAlign="top" align="center"><FONT face="굴림">
											<igtbl:UltraWebGrid id="UltraWebGrid3" runat="server" Height="430px" Width="100%">
												<DisplayLayout AutoGenerateColumns="False" AllowSortingDefault="Yes" RowHeightDefault="20px" Version="3.00"
													AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate"
													AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid3" CellClickActionDefault="RowSelect">
													<AddNewBox>
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														</Style>
													</AddNewBox>
													<Pager QuickPages="5" PageSize="20" Alignment="Center" AllowPaging="True">
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														</Style>
													</Pager>
													<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray" Height="25px">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</HeaderStyleDefault>
													<RowSelectorStyleDefault Cursor="Hand" BackColor="Gold"></RowSelectorStyleDefault>
													<FrameStyle Width="100%" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DimGray"
														BorderStyle="Solid" Height="430px"></FrameStyle>
													<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</FooterStyleDefault>
													<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
													<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
													<RowAlternateStyleDefault BackColor="LightSteelBlue"></RowAlternateStyleDefault>
													<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
														<Padding Left="3px"></Padding>
														<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
													</RowStyleDefault>
												</DisplayLayout>
												<Bands>
													<igtbl:UltraGridBand>
														<Columns>
															<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="120px" BaseColumnName="ItemNum">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" BaseColumnName="ItemDrawNum">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" BaseColumnName="ItemName">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="공정순서번호" Key="ProcessSequenceNum" BaseColumnName="ProcessSequenceNum">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="공정명" Key="ProcessName" BaseColumnName="ProcessName">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="공정코드" Key="ProcessCode" Hidden="True" BaseColumnName="ProcessCode">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="전년도이월수량" Key="LastYearTransferQuantity" Hidden="True" Format="###,###,##0.00"
																BaseColumnName="LastYearTransferQuantity">
																<CellStyle HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="전년도이월금액" Key="LastYearTransferCost" Hidden="True" Format="###,###,##0.00"
																BaseColumnName="LastYearTransferCost">
																<CellStyle HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="현재고수량" Key="StockQuantity12" Format="###,###,##0.00" BaseColumnName="StockQuantity12">
																<CellStyle HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="현재고금액" Key="StockCost12" Format="###,###,##0.00" BaseColumnName="StockCost12">
																<CellStyle HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="원자재창고순서번호" Key="RowMetarialStorehouseIndex" Hidden="True" BaseColumnName="RowMetarialStorehouseIndex">
																<CellStyle HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="생산창고순서번호" Key="ProductionStorehouseIndex" Hidden="True" BaseColumnName="ProductionStorehouseIndex">
																<CellStyle HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
														</Columns>
													</igtbl:UltraGridBand>
												</Bands>
											</igtbl:UltraWebGrid></FONT>
									</td>
								</tr>
							</table>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="right">
						<table id="Table4" height="40" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD style="WIDTH: 10px" align="left" height="5"></TD>
								<TD align="left" height="5"><FONT face="굴림"></FONT></TD>
							</TR>
							<tr>
								<td style="WIDTH: 10px" align="left"><asp:button id="btnExcel" runat="server" Text="Excel" Width="65px" Font-Size="9pt" Height="20px"></asp:button></td>
								<td align="left"><asp:button id="btnXML" runat="server" Text="XML" Width="65px" Font-Size="9pt" Height="20px"
										Enabled="False"></asp:button>
									<igtblexp:UltraWebGridExcelExporter id="uwgExcelExporter1" runat="server"></igtblexp:UltraWebGridExcelExporter></td>
							</tr>
						</table>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
