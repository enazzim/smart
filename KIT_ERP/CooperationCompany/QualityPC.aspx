<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="QualityPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.CooperationCompany.QualityPC" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
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
			<TABLE id="Table1" style="LEFT: 10px; POSITION: absolute; TOP: 10px" cellSpacing="0" cellPadding="0"
				border="0" width="800">
				<TR>
					<TD align="center">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"
							align="top"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색 ]
							</LEGEND>
							<TABLE id="Table2" align="center" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR height="35">
									<TD align="left" colSpan="8" height="30">
										<uc1:ItemSearchControl id="ItemSearchControl1" runat="server"></uc1:ItemSearchControl>
									</TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30"><FONT face="굴림">납품일자&nbsp;</FONT></TD>
									<TD width="120" height="30"><igsch:webdatechooser id="wdcStartDate" runat="server" Text=" " NullDateLabel=" " BorderColor="DimGray"
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
										</igsch:webdatechooser></TD>
									<TD align="center" width="15" height="30"><FONT face="굴림">~</FONT></TD>
									<TD align="left" width="120" height="30"><igsch:webdatechooser id="wdcEndDate" runat="server" Text=" " NullDateLabel=" " BorderColor="DimGray"
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
										</igsch:webdatechooser></TD>
									<TD align="center" width="15" height="30"><FONT face="굴림"></FONT></TD>
									<TD width="100" height="30"><FONT face="굴림"></FONT></TD>
									<TD align="right" width="70" height="30"><FONT face="굴림"></FONT></TD>
									<TD align="right" height="30"><asp:button id="btnInit" runat="server" Text="초기화" Width="65px" Font-Size="10pt" Height="20px"></asp:button><FONT face="굴림">&nbsp;</FONT><asp:button id="btnSearch" runat="server" Text="검   색" Width="65px" Font-Size="10pt" Height="20px"></asp:button>&nbsp;
									</TD>
								</TR>
								<TR>
									<TD align="right" height="8" colSpan="8"><FONT face="굴림"></FONT></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"
							align="top"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색결과 ]
							</LEGEND>
							<table id="Table3" width="800" cellSpacing="0" cellPadding="0" align="center" border="0">
								<tr>
									<td vAlign="top" align="center">
										<igtbl:UltraWebGrid id="uwgQPC" runat="server" Height="400px" Width="100%">
											<DisplayLayout AutoGenerateColumns="False" AllowSortingDefault="Yes" RowHeightDefault="20px" Version="3.00"
												AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate"
												AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="uwgQPC" CellClickActionDefault="RowSelect">
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
												<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
												<FrameStyle Width="100%" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DimGray"
													BorderStyle="Solid" Height="400px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<RowExpAreaStyleDefault Cursor="Hand" BackColor="Gold"></RowExpAreaStyleDefault>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
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
														<igtbl:UltraGridColumn HeaderText="거래처" Key="CompanyName" BaseColumnName="CompanyName">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" BaseColumnName="BusinessRegistrationNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품질검사완료일자" Key="QualityInspectionCompleteDate" Format="yyyy-MM-dd" BaseColumnName="QualityInspectionCompleteDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="시작공정번호" Key="BeginProcessSequenceNum" BaseColumnName="BeginProcessSequenceNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="시작공정명" Key="BeginName" BaseColumnName="BeginName">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="시작공정코드" Key="BeginProcessName" Hidden="True" BaseColumnName="BeginProcessName"
															NullText="">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="종료공정번호" Key="EndProcessSequenceNum" BaseColumnName="EndProcessSequenceNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="종료공정명" Key="EndName" BaseColumnName="EndName">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="종료공정코드" Key="EndProcessName" Hidden="True" BaseColumnName="EndProcessName"
															NullText="">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="의뢰량" Key="RequestQuantity" Format="###,###,##0.00" BaseColumnName="RequestQuantity">
															<SelectedCellStyle HorizontalAlign="Right"></SelectedCellStyle>
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="적합수량" Key="SuitabilityQuantity" Format="###,###,##0.00" BaseColumnName="SuitabilityQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="불합격수량" Key="UnSuitabilityQuantity" Format="###,###,##0.00" BaseColumnName="UnSuitabilityQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Format="" BaseColumnName="ApplyUnitCost">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="불합격금액" Key="UnSuitabilityCost" Format="###,###,##0.00" BaseColumnName="UnSuitabilityCost">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="검사판정코드" Key="InspectionDecisionCode" Hidden="True" BaseColumnName="InspectionDecisionCode">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="검사판정내용" Key="InspectionDecisionMeaning" BaseColumnName="InspectionDecisionMeaning">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합원인코드" Key="UnSuitabilityCauseCode" Hidden="True" BaseColumnName="UnSuitabilityCauseCode">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합내용" Key="UnSuitabilityCauseMeaning" BaseColumnName="UnSuitabilityCauseMeaning">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합현상코드" Key="UnSuitabilityStatusCode" Hidden="True" BaseColumnName="UnSuitabilityStatusCode">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합현상내용" Key="UnSuitabilityStatusMeaning" BaseColumnName="UnSuitabilityStatusMeaning">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합세부내용" Key="UnSuitabilityDetailMeaning" BaseColumnName="UnSuitabilityDetailMeaning">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="검사자" Key="Investigator" BaseColumnName="Investigator">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="검사자ID" Key="InvestigatorID" Hidden="True" BaseColumnName="InvestigatorID">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" BaseColumnName="ProgressCondition">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" BaseColumnName="RegistrationPerson">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" BaseColumnName="UpdatingPerson">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="원장번호" Key="HistoryIndex1" Hidden="True" BaseColumnName="HistoryIndex1">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="원장도면" Key="HistorySection1" Hidden="True" BaseColumnName="HistorySection1">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="원장번호" Key="HistoryIndex2" Hidden="True" BaseColumnName="HistoryIndex2">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="원장도면" Key="HistorySection2" Hidden="True" BaseColumnName="HistorySection2">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="검사원장번호" Key="QualityInspectionHistoryIndex" Hidden="True" BaseColumnName="QualityInspectionHistoryIndex">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:UltraWebGrid>
									</td>
								</tr>
							</table>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="right">
						<table id="Table4" style="HEIGHT: 8px" height="8" width="100%" cellSpacing="0" cellPadding="0">
							<TR>
								<TD style="WIDTH: 10px" align="left" height="8"></TD>
								<TD align="left" height="8"></TD>
							</TR>
							<tr>
								<td style="WIDTH: 10px" align="left"><asp:button id="btnExcel" runat="server" Text="Excel" Width="65px" Font-Size="9pt" Height="20px"></asp:button></td>
								<td align="left"><asp:button id="Button3" runat="server" Text="XML" Width="65px" Font-Size="9pt" Height="20px"
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
