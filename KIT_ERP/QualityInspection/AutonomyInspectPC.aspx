<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Page language="c#" Codebehind="AutonomyInspectPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.QualityInspection.AutonomyInspectPC" codePage="949" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AutonomyInspectPC</title>
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
					ResetTextBox();
					var objCombo1 = igcmbo_getComboById("wcWCName");
					var objCombo3 = igcmbo_getComboById("wcProcessName");
					
					var objChooser1 = igdrp_getComboById("txtStartDate");
					var objChooser2 = igdrp_getComboById("txtEndDate");
					
					objCombo1.setDisplayValue("");
					objCombo3.setDisplayValue("");

					objChooser1.setValue(null);
					objChooser2.setValue(null);
				}
		//-->
		</script>
	</HEAD>
	<body bottomMargin="0" topMargin="0" rightMargin="0" bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" border="0" style="LEFT: 10px; POSITION: absolute; TOP: 10px">
				<TR>
					<TD vAlign="top" align="center" height="80">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; 90BORDER-RIGHT: black 1px solid"
							align="middle"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색조건 ]
							</LEGEND>
							<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
								<TR>
									<TD align="left" colSpan="10" height="25">
										<uc1:ItemSearchControl id="ItemSearchControl1" runat="server"></uc1:ItemSearchControl></TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30">
										<P>WC명&nbsp;</P>
									</TD>
									<TD width="130" height="30"><igcmbo:webcombo id="wcWCName" runat="server" Height="20px" BorderStyle="Groove" DropImageXP2="/ig_common/WebGrid3/ig_cmboDown2.bmp"
											DropImageXP1="/ig_common/WebGrid3/ig_cmboDown1.bmp" SelForeColor="White" ForeColor="Black" SelectedIndex="-1" SelBackColor="10, 36, 106"
											BorderColor="Gray" BorderWidth="1px" Version="3.00" BackColor="#EEEEE9" Width="120px" Editable="True">
											<DropDownLayout DropdownWidth="380px" BorderCollapse="Separate" RowSelectors="No" AllowColSizing="Free"
												RowHeightDefault="20px" HeaderClickAction="SortSingle" DropdownHeight="345px" TableLayout="Fixed"
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
									<TD align="right" width="70" height="30">공정명&nbsp;</TD>
									<TD width="130" height="30"><igcmbo:webcombo id="wcProcessName" runat="server" Height="20px" BorderStyle="Groove" DropImageXP2="/ig_common/WebGrid3/ig_cmboDown2.bmp"
											DropImageXP1="/ig_common/WebGrid3/ig_cmboDown1.bmp" SelForeColor="White" ForeColor="Black" SelectedIndex="-1" SelBackColor="10, 36, 106"
											BorderColor="Gray" BorderWidth="1px" Version="3.00" BackColor="#EEEEE9" Width="120px" Editable="True">
											<DropDownLayout DropdownWidth="150px" BorderCollapse="Separate" RowSelectors="No" RowHeightDefault="20px"
												HeaderClickAction="SortSingle" DropdownHeight="350px" TableLayout="Fixed" StationaryMargins="Header">
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
									<TD align="right" width="70" height="30">검사일&nbsp;</TD>
									<TD width="120" height="30"><igsch:webdatechooser id="txtStartDate" runat="server" Height="20px" Text="Null" NullDateLabel=" " BorderColor="Gray"
											BorderStyle="Solid" BackColor="#EEEEE9">
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
									<TD vAlign="middle" align="center" width="15" height="30">&nbsp;~&nbsp;</TD>
									<TD height="30" width="120"><igsch:webdatechooser id="txtEndDate" runat="server" Height="20px" Text="Null" NullDateLabel=" " BorderColor="Gray"
											BorderStyle="Solid" BackColor="#EEEEE9">
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
									<TD height="30" style="WIDTH: 62px" colSpan="2"></TD>
								</TR>
								<TR>
									<TD align="right" height="25"></TD>
									<TD height="25"></TD>
									<TD align="right" height="25"></TD>
									<TD height="25"></TD>
									<TD align="right" height="25"></TD>
									<TD height="25"></TD>
									<TD height="25"></TD>
									<TD height="25" align="right" colSpan="3"><INPUT style="WIDTH: 60px; HEIGHT: 20px" onclick="ResettxtBox()" type="button" value="초기화">&nbsp;
										<asp:button id="btnSearch" runat="server" Width="60px" Height="20px" Text="검  색"></asp:button>&nbsp;&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD align="center"><br>
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; 90BORDER-RIGHT: black 1px solid"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 
								검색결과 ]
							</LEGEND>
							<table border="0" cellpadding="0" cellspacing="0">
								<tr>
									<td style="HEIGHT: 366px">
										<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="380px" Width="800px">
											<DisplayLayout ColFootersVisibleDefault="Yes" StationaryMargins="Header" AutoGenerateColumns="False"
												AllowSortingDefault="OnClient" RowHeightDefault="20px" Version="3.00" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect">
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
												<HeaderStyleDefault Cursor="Hand" BorderColor="DimGray" BorderStyle="Solid" HorizontalAlign="Center"
													BackColor="LightGray" Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<RowSelectorStyleDefault Cursor="Hand" BackColor="#EBEFF6"></RowSelectorStyleDefault>
												<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="Verdana" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="380px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="품목 번호" Key="" HeaderClickAction="SortMulti" BaseColumnName="ItemNum"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면 번호" Key="" HeaderClickAction="SortMulti" BaseColumnName="ItemDrawNum"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목 명" Key="" HeaderClickAction="SortMulti" BaseColumnName="ItemName"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업장 명" Key="" HeaderClickAction="SortMulti" BaseColumnName="wcname"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정 순서" Key="" HeaderClickAction="SortMulti" BaseColumnName="ProcessSequenceNum">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정 명" Key="" HeaderClickAction="SortMulti" BaseColumnName="ProcessName"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="계획 수량" Key="" Format="###,###,##0.00" HeaderClickAction="SortMulti"
															BaseColumnName="WorkPlanQuantity" FooterTotal="Sum">
															<FooterStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</FooterStyle>
															<CellStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="검사 수량" Key="" Format="###,###,##0.00" HeaderClickAction="SortMulti"
															BaseColumnName="ThisWorkCompletionQuantity" FooterTotal="Sum">
															<FooterStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</FooterStyle>
															<CellStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="적합 수량" Key="" Format="###,###,##0.00" HeaderClickAction="SortMulti"
															BaseColumnName="SuitabilityQuantity" FooterTotal="Sum">
															<FooterStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</FooterStyle>
															<CellStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합 수량" Key="" Format="###,###,##0.00" HeaderClickAction="SortMulti"
															BaseColumnName="UnSuitabilityQuantity" FooterTotal="Sum">
															<FooterStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</FooterStyle>
															<CellStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합 금액" Key="" Format="###,###,##0.00" HeaderClickAction="SortMulti"
															BaseColumnName="UnSuitabilityCost" FooterTotal="Sum">
															<FooterStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</FooterStyle>
															<CellStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합 현상" Key="" HeaderClickAction="SortMulti" BaseColumnName="UnSuitabilityStatusMeaning"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합 원인" Key="" HeaderClickAction="SortMulti" BaseColumnName="UnSuitabilityCauseMeaning"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합 세부사항" Key="" HeaderClickAction="SortMulti" BaseColumnName="UnSuitabilityDetailMeaning"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업 시작시간" Key="" Format="yyyy-MM-dd hh:mm tt" HeaderClickAction="SortMulti"
															BaseColumnName="WorkBeginTime"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업 종료시간" Key="" Format="yyyy-MM-dd hh:mm tt" HeaderClickAction="SortMulti"
															BaseColumnName="WorkEndTime"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업자" Key="" HeaderClickAction="SortMulti" BaseColumnName="Worker"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="" Format="yyyy-MM-dd hh:mm tt" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationDate"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPerson"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="최종 수정일" Key="" Format="yyyy-MM-dd hh:mm tt" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingDate"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="최종 수정자" Key="" HeaderClickAction="SortMulti" BaseColumnName="UpdatingPerson"></igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid>
									</td>
								</tr>
								<tr height="40">
									<td>&nbsp;
										<asp:button id="Button2" runat="server" Width="65px" Height="20px" Text="Excel"></asp:button>
										<igtblexp:UltraWebGridExcelExporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:UltraWebGridExcelExporter>
									</td>
								</tr>
							</table>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			&nbsp;
		</form>
	</body>
</HTML>
