<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="AutonomyInspectStatistics.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.QualityInspection.AutonomyInspectStatistics" codePage="949" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AutonomyInspectStatistics</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!--
				// 초기화 버튼
				function ResettxtBox()
				{
					var objCombo1 = igcmbo_getComboById("wcWCName");
					var objChooser1 = igdrp_getComboById("wcStartDate");
					var objChooser2 = igdrp_getComboById("wcEndDate");
									
					objCombo1.setDisplayValue("");
					objChooser1.setValue(null);
					objChooser2.setValue(null);
					
					var frm = document.Form1;
					frm.ddlItemClassification1.options[0].selected = true
					frm.ddlItemClassification2.options[0].selected = true
					frm.ddlItemClassification3.options[0].selected = true
					frm.ddlItemClassification4.options[0].selected = true
				}
		//-->
		</script>
	</HEAD>
	<body bottomMargin="0" topMargin="0" rightMargin="0" bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" border="0" align="left"
				style="LEFT: 10px; POSITION: absolute; TOP: 10px">
				<TR>
					<TD vAlign="top" align="center" height="80">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; 90BORDER-RIGHT: black 1px solid"
							align="middle"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색조건 ]
							</LEGEND>
							<TABLE id="Table1" height="50" cellSpacing="0" cellPadding="0" width="100%" align="center"
								border="0">
								<TR>
									<TD align="right" width="90" height="30">
										품목분류1&nbsp;
									</TD>
									<TD width="100" height="30"><asp:dropdownlist id="ddlItemClassification1" runat="server" Width="120px" BackColor="#EEEEE9"></asp:dropdownlist></TD>
									<TD align="right" width="90" height="30">품목분류2&nbsp;</TD>
									<TD align="left" width="100" height="30"><asp:dropdownlist id="ddlItemClassification2" runat="server" Width="120px" BackColor="#EEEEE9"></asp:dropdownlist></TD>
									<TD align="right" width="15" height="30"></TD>
									<TD width="80" height="30" align="right">품목분류3&nbsp;</TD>
									<TD align="left" width="100" height="30">
										<asp:dropdownlist id="ddlItemClassification3" runat="server" Width="120px" BackColor="#EEEEE9"></asp:dropdownlist></TD>
									<TD width="90" height="30" align="right">품목분류4&nbsp;</TD>
									<TD vAlign="middle" align="left" height="30" width="150" colSpan="3">
										<asp:dropdownlist id="ddlItemClassification4" runat="server" Width="120px" BackColor="#EEEEE9"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD align="right" height="30">
										<P>WC명&nbsp;</P>
									</TD>
									<TD height="30">
										<igcmbo:webcombo id="wcWCName" runat="server" Editable="True" Width="120px" BackColor="#EEEEE9" Version="3.00"
											BorderWidth="1px" BorderColor="Gray" SelBackColor="10, 36, 106" SelectedIndex="-1" ForeColor="Black"
											SelForeColor="White" DropImageXP1="/ig_common/WebGrid3/ig_cmboDown1.bmp" DropImageXP2="/ig_common/WebGrid3/ig_cmboDown2.bmp"
											BorderStyle="Groove" Height="20px">
											<DropDownLayout DropdownWidth="380px" BorderCollapse="Separate" RowSelectors="No" AllowColSizing="Free"
												RowHeightDefault="20px" HeaderClickAction="Select" DropdownHeight="350px" TableLayout="Fixed"
												StationaryMargins="Header">
												<RowStyle BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="White">
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyle>
												<SelectedRowStyle ForeColor="White" BackColor="#0A246A"></SelectedRowStyle>
												<HeaderStyle BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyle>
												<FrameStyle Width="100%" Cursor="Default" BorderWidth="2px" Font-Size="10pt" Font-Names="Verdana"
													BorderStyle="Ridge" BackColor="Silver" Height="100%"></FrameStyle>
											</DropDownLayout>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igcmbo:webcombo></TD>
									<TD align="right" height="30">기간&nbsp;
									</TD>
									<TD align="right" height="30">
										<igsch:webdatechooser id="wcStartDate" runat="server" Height="20px" NullDateLabel=" " Text="Null" BorderStyle="Solid"
											BorderColor="Gray" BackColor="#EEEEE9">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD align="center" height="30">~
									</TD>
									<TD align="left" colSpan="2" height="30">
										<igsch:webdatechooser id="wcEndDate" runat="server" Height="20px" NullDateLabel=" " Text="Null" BorderStyle="Solid"
											BorderColor="Gray" BackColor="#EEEEE9">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD height="30" align="right" colSpan="4"><INPUT style="WIDTH: 60px; HEIGHT: 20px" onclick="ResettxtBox()" type="button" value="초기화">&nbsp;
										<asp:button id="btnSearch" runat="server" Width="60px" Height="20px" Text="검  색"></asp:button>&nbsp;&nbsp;</TD>
								</TR>
								<TR>
									<TD align="right" height="8" colSpan="11"></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD align="center"><BR>
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; 90BORDER-RIGHT: black 1px solid"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 
								검색결과 ]
							</LEGEND>
							<table border="0" cellpadding="0" cellspacing="0" align="center">
								<tr>
									<td vAlign="top" align="center">
										<igtbl:ultrawebgrid id="dgStatisticsResult" runat="server" Width="100%" Height="400px" DESIGNTIMEDRAGDROP="533">
											<DisplayLayout ColFootersVisibleDefault="Yes" StationaryMargins="Header" AutoGenerateColumns="False"
												AllowSortingDefault="OnClient" RowHeightDefault="20px" Version="3.00" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="dgStatisticsResult" TableLayout="Fixed" CellClickActionDefault="RowSelect">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</AddNewBox>
												<Pager>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Center"
													BackColor="LightGray" Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<FrameStyle Width="100%" BorderWidth="1px" Font-Size="9pt" Font-Names="Verdana" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="400px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="작업장 명" Key="" Width="120px" HeaderClickAction="SortMulti" BaseColumnName="작업장 명"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총Lot 수" Key="" Width="70px" Format="###,###,##0.00" HeaderClickAction="SortMulti"
															BaseColumnName="총 Lot 수" FooterTotal="Sum">
															<FooterStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</FooterStyle>
															<CellStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합건수" Key="" Width="80px" Format="###,###,##0.00" HeaderClickAction="SortMulti"
															BaseColumnName="부적합건수" FooterTotal="Sum">
															<FooterStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</FooterStyle>
															<CellStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합율(%)" Key="" Width="85px" Format="###,###,##0.00" HeaderClickAction="SortMulti"
															BaseColumnName="부적합율 (%)">
															<FooterStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</FooterStyle>
															<CellStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="손실금액" Key="" Width="120px" Format="###,###,##0.00" HeaderClickAction="SortMulti"
															BaseColumnName="손실 금액" FooterTotal="Sum">
															<FooterStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</FooterStyle>
															<CellStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래수량" Key="" Width="120px" Format="###,###,##0.00" HeaderClickAction="SortMulti"
															BaseColumnName="거래 수량" FooterTotal="Sum">
															<FooterStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</FooterStyle>
															<CellStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합수량" Key="" Width="120px" Format="###,###,##0.00" HeaderClickAction="SortMulti"
															BaseColumnName="부적합 수량" FooterTotal="Sum">
															<FooterStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</FooterStyle>
															<CellStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="PPM" Key="" Width="120px" Format="###,###,##0.00" HeaderClickAction="SortMulti"
															BaseColumnName="PPM">
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
										</igtbl:ultrawebgrid>
									</td>
								</tr>
								<tr height="40">
									<td>&nbsp;
										<asp:button id="Button2" runat="server" Width="65px" Height="20px" Text="Excel"></asp:button>
										<igtblexp:UltraWebGridExcelExporter id="UltraWebGridExcelExporter1" runat="server" WorksheetName="AutonomyInspectIndex"
											DownloadName="AutonomyInspectIndex.XLS"></igtblexp:UltraWebGridExcelExporter>
									</td>
								</tr>
							</table>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
