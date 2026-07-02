<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Page language="c#" Codebehind="IncongruenceIndex.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.IncongruenceIndex" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>IncongruenceIndex</title>
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
					//Form1.dlWCName.options[0].selected = true
					Form1.dlProcess.options[0].selected = true
					Form1.ddlWorker.options[0].selected = true
					Form1.dlWorkDivision.options[0].selected = true
					
					var objCombo1 = igcmbo_getComboById("wcWCName");
					//var objCombo3 = igcmbo_getComboById("wcProcessName");
					
					var objChooser1 = igdrp_getComboById("txtStartDate");
					var objChooser2 = igdrp_getComboById("txtEndDate");
					
					objCombo1.setDisplayValue("");
					//objCombo3.setDisplayValue("");

					objChooser1.setValue(null);
					objChooser2.setValue(null);
				}
		//-->
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table3" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" cellSpacing="0"
				cellPadding="0" width="800" border="0">
				<TR>
					<TD vAlign="top" align="center">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; 90BORDER-RIGHT: black 1px solid"
							align="middle"><LEGEND style="FONT-SIZE: 9pt" align="left">[ 검색조건 ]
							</LEGEND>
							<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
								<TR>
									<TD style="FONT-SIZE: 1pt" align="left" width="800" colSpan="8" height="30">
										<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="790" align="center" border="0">
											<TR>
												<TD align="left" width="600"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></TD>
												<td style="FONT-SIZE: 9pt" align="right" width="70"><FONT face="굴림">작업구분</FONT>&nbsp;
												</td>
												<td style="FONT-SIZE: 9pt" align="left" width="120"><asp:dropdownlist id="dlWorkDivision" runat="server" Width="120px" BackColor="#EEEEE9">
														<asp:ListItem Value="-전체-">-전체-</asp:ListItem>
														<asp:ListItem Value="구매">구매</asp:ListItem>
														<asp:ListItem Value="외주">외주</asp:ListItem>
														<asp:ListItem Value="자가">자가</asp:ListItem>
													</asp:dropdownlist></td>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 9pt" align="right" width="90" height="30"><P>WC명&nbsp;</P>
									</TD>
									<TD width="120" height="30"><igcmbo:webcombo id="wcWCName" runat="server" Width="120px" BackColor="#EEEEE9" Version="3.00" BorderWidth="1px"
											BorderColor="DimGray" SelBackColor="10, 36, 106" SelectedIndex="-1" ForeColor="Black" SelForeColor="White" DropImageXP1="/ig_common/WebGrid3/ig_cmboDown1.bmp"
											DropImageXP2="/ig_common/WebGrid3/ig_cmboDown2.bmp" BorderStyle="Solid" Height="18px" Font-Size="10pt">
											<Columns>
												<igtbl:UltraGridColumn HeaderText="작업장명" Key="WCName" Width="180px" BaseColumnName="WCName"></igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="번호" Key="WCInfoIndex" Hidden="True" BaseColumnName="WCInfoIndex"></igtbl:UltraGridColumn>
											</Columns>
											<DropDownLayout DropdownWidth="200px" BorderCollapse="Separate" RowSelectors="No" AllowColSizing="Free"
												RowHeightDefault="20px" HeaderClickAction="Select" AutoGenerateColumns="False" DropdownHeight="300px"
												TableLayout="Fixed" StationaryMargins="Header">
												<RowStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid" BackColor="White">
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyle>
												<SelectedRowStyle ForeColor="White" BackColor="#0A246A"></SelectedRowStyle>
												<HeaderStyle BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyle>
												<FrameStyle Width="100%" Cursor="Default" Font-Size="10pt" Font-Names="Verdana" BackColor="Silver"
													Height="100%"></FrameStyle>
											</DropDownLayout>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igcmbo:webcombo></TD>
									<TD style="FONT-SIZE: 9pt" align="right" width="90" height="30">공정명&nbsp;</TD>
									<TD width="120" height="30"><asp:dropdownlist id="dlProcess" runat="server" Width="120px" BackColor="#EEEEE9"></asp:dropdownlist></TD>
									<TD style="FONT-SIZE: 9pt" align="right" width="90" height="30">작업일&nbsp;</TD>
									<TD width="120" height="30"><igsch:webdatechooser id="txtStartDate" runat="server" BackColor="#EEEEE9" BorderColor="Gray" BorderStyle="Solid"
											Height="20px" NullDateLabel=" " Text="Null">
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
									<TD width="455" height="30"><igsch:webdatechooser id="txtEndDate" runat="server" BackColor="#EEEEE9" BorderColor="Gray" BorderStyle="Solid"
											Height="20px" NullDateLabel=" " Text="Null">
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
										</igsch:webdatechooser><FONT face="굴림"></FONT></TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 21px" align="right" width="90"><FONT face="굴림"><FONT face="굴림">작업자&nbsp;</FONT></FONT></TD>
									<TD style="HEIGHT: 21px" width="120"><FONT face="굴림">
											<asp:dropdownlist id="ddlWorker" runat="server" BackColor="#EEEEE9" Width="120px"></asp:dropdownlist></FONT></TD>
									<TD style="HEIGHT: 21px" align="right" width="90"><FONT face="굴림"></FONT></TD>
									<TD style="HEIGHT: 21px" width="120"></TD>
									<TD style="HEIGHT: 21px" align="right" width="90"></TD>
									<TD style="HEIGHT: 21px" width="120"><FONT face="굴림"></FONT></TD>
									<TD style="HEIGHT: 21px" width="15"></TD>
									<TD style="HEIGHT: 21px" align="right" width="455"><INPUT style="WIDTH: 60px; HEIGHT: 20px" onclick="ResettxtBox()" type="button" value="초기화">&nbsp;
										<asp:button id="btnSearch" runat="server" Width="60px" Height="20px" Text="검  색"></asp:button>&nbsp;&nbsp;</TD>
								</TR>
								<TR>
									<TD align="right" width="90" height="10"></TD>
									<TD width="120" height="10"></TD>
									<TD align="right" width="90" height="10"></TD>
									<TD width="120" height="10"></TD>
									<TD align="right" width="90" height="10"></TD>
									<TD width="120" height="10"></TD>
									<TD width="15" height="10"></TD>
									<TD align="right" width="455" height="10"></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 1pt" align="center" height="5"><FONT face="굴림"></FONT></TD>
				</TR>
				<TR>
					<TD align="center">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; 90BORDER-RIGHT: black 1px solid"><LEGEND style="FONT-SIZE: 9pt" align="left">[ 
								검색결과 ]
							</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" border="0">
								<TR>
									<TD style="HEIGHT: 366px"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="360px">
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
												<Pager PageSize="14" StyleMode="ComboBox" AllowPaging="True">
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
													BorderStyle="Solid" BackColor="Silver" Height="360px"></FrameStyle>
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
														<igtbl:UltraGridColumn HeaderText="품목 번호" Key="ItemNum" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
															<HeaderStyle Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면 번호" Key="ItemDrawNum" HeaderClickAction="SortMulti" BaseColumnName="ItemDrawNum"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목 명" Key="ItemName" HeaderClickAction="SortMulti" BaseColumnName="ItemName"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업장 명" Key="WCName" HeaderClickAction="SortMulti" BaseColumnName="WCName"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정 순서" Key="ProcessSequenceNum" HeaderClickAction="SortMulti" BaseColumnName="ProcessSequenceNum">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정 명" Key="ProcessName" HeaderClickAction="SortMulti" BaseColumnName="ProcessName"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업 수량" Key="ThisWorkCompletionQuantity" Format="###,###,##0" HeaderClickAction="SortMulti"
															BaseColumnName="ThisWorkCompletionQuantity" FooterTotal="Sum">
															<FooterStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</FooterStyle>
															<CellStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="적합 수량" Key="SuitabilityQuantity" Format="###,###,##0" HeaderClickAction="SortMulti"
															BaseColumnName="SuitabilityQuantity" FooterTotal="Sum">
															<FooterStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</FooterStyle>
															<CellStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합 수량" Key="UnSuitabilityQuantity" Format="###,###,##0" HeaderClickAction="SortMulti"
															BaseColumnName="UnSuitabilityQuantity" FooterTotal="Sum">
															<FooterStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</FooterStyle>
															<CellStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합 금액" Key="UnSuitabilityCost" Format="###,###,##0" HeaderClickAction="SortMulti"
															BaseColumnName="UnSuitabilityCost" FooterTotal="Sum">
															<FooterStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</FooterStyle>
															<CellStyle HorizontalAlign="Right">
																<Padding Right="7px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합 현상" Key="UnSuitabilityStatusMeaning" HeaderClickAction="SortMulti"
															BaseColumnName="UnSuitabilityStatusMeaning"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합 원인" Key="UnSuitabilityCauseMeaning" HeaderClickAction="SortMulti"
															BaseColumnName="UnSuitabilityCauseMeaning"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합 세부사항" Key="UnSuitabilityDetailMeaning" HeaderClickAction="SortMulti"
															BaseColumnName="UnSuitabilityDetailMeaning"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업 시작시간" Key="WorkBeginTime" Format="yyyy-MM-dd hh:mm tt" HeaderClickAction="SortMulti"
															BaseColumnName="WorkBeginTime"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업 종료시간" Key="WorkEndTime" Format="yyyy-MM-dd hh:mm tt" HeaderClickAction="SortMulti"
															BaseColumnName="WorkEndTime"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업자" Key="Worker" HeaderClickAction="SortMulti" BaseColumnName="Worker"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationDate"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPerson"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="최종 수정일" Key="UpdatingDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingDate"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="최종 수정자" Key="UpdatingDate" HeaderClickAction="SortMulti" BaseColumnName="UpdatingPerson"></igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
								<TR height="40">
									<TD>&nbsp;
										<asp:button id="Button2" runat="server" Height="20px" Width="65px" Text="Excel"></asp:button>
										<igtblexp:UltraWebGridExcelExporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:UltraWebGridExcelExporter></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			&nbsp;
		</form>
	</body>
</HTML>
