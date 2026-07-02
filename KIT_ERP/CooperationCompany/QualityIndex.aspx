<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="QualityIndex.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.CooperationCompany.QualityIndex" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		
			// 초기화 버튼
			function ResettxtBox()
			{
				var objCombo1 = igcmbo_getComboById("wcbItem");				
				var objChooser1 = igdrp_getComboById("wdcStartDate");
				var objChooser2 = igdrp_getComboById("wdcEndDate");
				
				objCombo1.setDisplayValue("");			
				objChooser1.setValue(null);
				objChooser2.setValue(null);
			}
	
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" cellSpacing="0"
				cellPadding="0" border="0" width="800" height="550">
				<TR>
					<TD style="HEIGHT: 83px" align="right" colSpan="1">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 60px"
							align="top"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색 ]
							</LEGEND>
							<TABLE id="Table2" align="center" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR height="35">
									<TD align="left" width="600" colSpan="8" height="30">
										<uc1:ItemSearchControl id="ItemSearchControl1" runat="server"></uc1:ItemSearchControl>
									</TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30"><FONT face="굴림">납품일자&nbsp;</FONT></TD>
									<TD width="120" height="30"><FONT face="굴림"><igsch:webdatechooser id="wdcStartDate" runat="server" Text=" " NullDateLabel=" " BorderColor="DimGray"
												BorderStyle="Solid">
												<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
													ShowTitle="False" ShowFooter="False">
													<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
													<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
													<DropDownStyle BackColor="White"></DropDownStyle>
													<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
												</CalendarLayout>
												<DropDownStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></DropDownStyle>
												<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
												<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
											</igsch:webdatechooser></FONT></TD>
									<TD align="center" width="10" height="30"><FONT face="굴림">~</FONT></TD>
									<TD align="left" width="120" height="30"><FONT face="굴림"><igsch:webdatechooser id="wdcEndDate" runat="server" Text=" " NullDateLabel=" " BorderColor="DimGray"
												BorderStyle="Solid">
												<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
													ShowTitle="False" ShowFooter="False">
													<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
													<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
													<DropDownStyle BackColor="White"></DropDownStyle>
													<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
												</CalendarLayout>
												<DropDownStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></DropDownStyle>
												<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
												<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
											</igsch:webdatechooser></FONT></TD>
									<TD align="center" width="15" height="30"><FONT face="굴림"></FONT></TD>
									<TD width="100" height="30"><FONT face="굴림"></FONT></TD>
									<TD align="right" width="70" height="30"></TD>
									<TD align="right" height="30"><asp:button id="btnInit" runat="server" Width="65px" Height="20px" Text="초기화" Font-Size="10pt"></asp:button><FONT face="굴림">&nbsp;</FONT><asp:button id="btnSearch" runat="server" Width="65px" Height="20px" Text="검색" Font-Size="10pt"></asp:button>&nbsp;
									</TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="8"></TD>
									<TD width="120" height="8"></TD>
									<TD align="center" width="10" height="8"></TD>
									<TD align="left" width="120" height="8"></TD>
									<TD align="center" width="15" height="8"></TD>
									<TD width="100" height="8"></TD>
									<TD align="right" width="70" height="8"></TD>
									<TD align="right" height="8"></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="left" height="400">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 400px"
							align="top"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색결과 ]
							</LEGEND>
							<table id="Table3" cellSpacing="0" cellPadding="0" width="800">
								<tr>
									<td vAlign="middle" align="center"><FONT face="굴림"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="430px">
												<DisplayLayout ColFootersVisibleDefault="Yes" StationaryMargins="Header" AutoGenerateColumns="False"
													AllowSortingDefault="Yes" RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single"
													AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate"
													AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed"
													CellClickActionDefault="RowSelect">
													<AddNewBox>
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

														</Style>
													</AddNewBox>
													<Pager QuickPages="5" PageSize="20" Alignment="Center" AllowPaging="True">
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

														</Style>
													</Pager>
													<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray" Height="25px">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</HeaderStyleDefault>
													<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DimGray"
														BorderStyle="Solid" Height="430px"></FrameStyle>
													<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</FooterStyleDefault>
													<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
													<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
													<RowAlternateStyleDefault BackColor="LightSteelBlue"></RowAlternateStyleDefault>
													<RowStyleDefault Cursor="Hand" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="Gray"
														BorderStyle="Solid" BackColor="#EBEFF6">
														<Padding Left="3px"></Padding>
														<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
													</RowStyleDefault>
												</DisplayLayout>
												<Bands>
													<igtbl:UltraGridBand>
														<Columns>
															<igtbl:UltraGridColumn HeaderText="품목명" Key="" Width="120px" BaseColumnName="품목명">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="총Lot 수" Key="" Width="70px" BaseColumnName="총 Lot 수" FooterTotal="Sum">
																<FooterStyle HorizontalAlign="Right">
																	<Padding Right="7px"></Padding>
																</FooterStyle>
																<CellStyle HorizontalAlign="Right">
																	<Padding Right="7px"></Padding>
																</CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="부적합건수" Key="" Width="80px" BaseColumnName="부적합건수" FooterTotal="Sum">
																<FooterStyle HorizontalAlign="Right">
																	<Padding Right="7px"></Padding>
																</FooterStyle>
																<CellStyle HorizontalAlign="Right">
																	<Padding Right="7px"></Padding>
																</CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="부적합율(%)" Key="" Width="85px" Format="###,###,##0.00" BaseColumnName="부적합율 (%)"
																FooterTotal="Avg">
																<FooterStyle HorizontalAlign="Right">
																	<Padding Right="7px"></Padding>
																</FooterStyle>
																<CellStyle HorizontalAlign="Right">
																	<Padding Right="7px"></Padding>
																</CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="손실금액" Key="" Width="120px" Format="###,###,##0.00" BaseColumnName="손실 금액"
																FooterTotal="Sum">
																<FooterStyle HorizontalAlign="Right">
																	<Padding Right="7px"></Padding>
																</FooterStyle>
																<CellStyle HorizontalAlign="Right">
																	<Padding Right="7px"></Padding>
																</CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="거래수량" Key="" Width="120px" Format="###,###,##0.00" BaseColumnName="거래 수량"
																FooterTotal="Sum">
																<FooterStyle HorizontalAlign="Right">
																	<Padding Right="7px"></Padding>
																</FooterStyle>
																<CellStyle HorizontalAlign="Right">
																	<Padding Right="7px"></Padding>
																</CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="부적합수량" Key="" Width="120px" Format="###,###,##0.00" BaseColumnName="부적합 수량"
																FooterTotal="Sum">
																<FooterStyle HorizontalAlign="Right">
																	<Padding Right="7px"></Padding>
																</FooterStyle>
																<CellStyle HorizontalAlign="Right">
																	<Padding Right="7px"></Padding>
																</CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="PPM" Key="" Width="120px" Format="###,###,##0.00" BaseColumnName="PPM"
																FooterTotal="Avg">
																<FooterStyle HorizontalAlign="Right">
																	<Padding Right="7px"></Padding>
																</FooterStyle>
																<CellStyle HorizontalAlign="Right">
																	<Padding Right="7px"></Padding>
																</CellStyle>
															</igtbl:UltraGridColumn>
														</Columns>
													</igtbl:UltraGridBand>
												</Bands>
											</igtbl:ultrawebgrid></FONT></td>
								</tr>
							</table>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
