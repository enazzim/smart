<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="WorkDailyReportRegistrationPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.EtcPresentCondition.WorkDailyReportRegistrationPC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WorkDailyReportRegistrationPC</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--
		function ResettxtBox()
		{
			
			ResetTextBox();
			var objChooser1 = igdrp_getComboById("wdcFromDate");
			var objChooser2 = igdrp_getComboById("wdcToDate");
			objChooser1.setValue(null);
			objChooser2.setValue(null);
			document.Form1.ddlWorker.options[0].selected=true;
			document.Form1.dlWCName.options[0].selected=true;
		}
		
		
		
//-->
		</SCRIPT>
	</HEAD>
	<body bgColor="#f7f6f6" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="LEFT: 10px; POSITION: absolute; TOP: 10px" cellSpacing="0" cellPadding="0"
				width="800" border="0">
				<TR>
					<TD></TD>
					<TD vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD vAlign="middle" align="left" colSpan="6" height="30"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></TD>
									<TD vAlign="middle" align="right" height="30" width="70">&nbsp;작업자&nbsp;</TD>
									<TD vAlign="middle" align="left" height="30" width="130">
										<asp:DropDownList id="ddlWorker" runat="server" Width="100px" BackColor="#EEEEE9"></asp:DropDownList></TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" width="70" height="30">등록일&nbsp;</TD>
									<TD vAlign="middle" align="left" width="100" height="30"><igsch:webdatechooser id="wdcFromDate" runat="server" BorderStyle="Solid" BorderColor="DimGray" Text=" "
											NullDateLabel=" " BackColor="#EEEEE9" Width="100px" Height="18px" Font-Size="10pt">
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
									<TD vAlign="middle" align="center" width="15" height="30">~</TD>
									<TD vAlign="middle" align="right" width="100" height="30"><igsch:webdatechooser id="wdcToDate" runat="server" BorderStyle="Solid" BorderColor="DimGray" Text=" "
											NullDateLabel=" " BackColor="#EEEEE9" Width="100px" Height="18px" Font-Size="10pt">
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
									<TD vAlign="middle" align="right" width="70" height="30">WC명&nbsp;</TD>
									<TD vAlign="middle" align="left" width="245" height="30">
										<asp:DropDownList id="dlWCName" runat="server" BackColor="#EEEEE9" Width="100px"></asp:DropDownList></TD>
									<TD vAlign="middle" align="right" colSpan="2" height="30" width="200"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
											type="button" value="초기화" name="btnReset">&nbsp;
										<asp:button id="bt_Search" runat="server" Text="검   색" Width="60px" Height="20px" Font-Size="9pt"></asp:button>&nbsp;&nbsp;
									</TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" colSpan="8" height="8"></TD>
								</TR>
							</TABLE>
						</FIELDSET>
						&nbsp;&nbsp;&nbsp;
						<BR>
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[검색결과]</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD vAlign="top" align="center" colSpan="3">
										<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="100%" Height="405px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect"
												AllowUpdateDefault="RowTemplateOnly">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</AddNewBox>
												<Pager PageSize="17" StyleMode="ComboBox" AllowPaging="True">
													<Style VerticalAlign="Middle" BorderWidth="1px" BorderStyle="Solid" HorizontalAlign="Right"
														BackColor="LightGray">
													</Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" BorderColor="Black" BorderStyle="Solid" ForeColor="Black" BackColor="LightGray"
													Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<GroupByRowStyleDefault ForeColor="Black" BackColor="#95B9F0"></GroupByRowStyleDefault>
												<RowSelectorStyleDefault Cursor="Hand" BorderStyle="Solid"></RowSelectorStyleDefault>
												<FrameStyle Width="100%" Cursor="Default" BorderWidth="1px" Font-Size="10pt" Font-Names="굴림"
													BorderColor="DimGray" BorderStyle="Solid" BackColor="Silver" Height="405px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<GroupByBox>
													<Style BackColor="#B5CFF7">
													</Style>
													<BandLabelStyle ForeColor="White" BackColor="#6372D4"></BandLabelStyle>
												</GroupByBox>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedGroupByRowStyleDefault BorderColor="Yellow" BackColor="Yellow"></SelectedGroupByRowStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" Font-Bold="True" ForeColor="Linen" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" ForeColor="Black"
													BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails ColorTop="Gray" WidthLeft="0px" WidthTop="0px" ColorLeft="Gray"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" BaseColumnName="ItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" BaseColumnName="ItemDrawNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" BaseColumnName="ItemName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업장명" Key="WCName" BaseColumnName="WCName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업자ID" Key="WorkerID" Hidden="True" BaseColumnName="WorkerID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업자" Key="Worker" BaseColumnName="Worker">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정명" Key="ProcessName" BaseColumnName="ProcessName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정코드" Key="ProcessCode" Hidden="True" BaseColumnName="ProcessCode">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="Lot 번호" Key="LotNum" Hidden="True" BaseColumnName="LotNum">
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="금번완료수량" Key="ThisWorkCompletionQuantity" Format="###,###,###.##" BaseColumnName="ThisWorkCompletionQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="적합수량" Key="SuitabilityQuantity" Format="###,###,###.##" BaseColumnName="SuitabilityQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합수량" Key="UnSuitabilityQuantity" Format="###,###,###.##" BaseColumnName="UnSuitabilityQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업시작시간" Key="WorkBeginTime" Format="yyyy-MM-dd hh:mm tt" BaseColumnName="WorkBeginTime">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업종료시간" Key="WorkEndTime" Format="yyyy-MM-dd hh:mm tt" BaseColumnName="WorkEndTime">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정순서" Key="ProcessSequenceNum" Width="70px" Hidden="True" BaseColumnName="ProcessSequenceNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합현상" Key="UnSuitabilityStatusMeaning" BaseColumnName="UnSuitabilityStatusMeaning">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합원인" Key="UnSuitabilityCauseMeaning" BaseColumnName="UnSuitabilityCauseMeaning">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합금액" Key="UnSuitabilityCost" Format="###,###,###.##" BaseColumnName="UnSuitabilityCost">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="검사판정" Key="InspectionDecision" BaseColumnName="InspectionDecision">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합세부사유" Key="UnSuitabilityDetailMeaning" BaseColumnName="UnSuitabilityDetailMeaning">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="비작업시간1" Key="NonWorkTime1" BaseColumnName="NonWorkTime1">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="비작업사유1" Key="NonWorkTimeReason1" BaseColumnName="NonWorkTimeReason1">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="제품명" Key="ProductName" Hidden="True" BaseColumnName="ProductName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="모품목번호" Key="ParentItemNum" Hidden="True" BaseColumnName="ParentItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="모품목도면번호" Key="ParentDrawNum" Hidden="True" BaseColumnName="ParentDrawNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="모품목명" Key="ParentName" Hidden="True" BaseColumnName="ParentName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="WC번호" Key="WCInfoIndex" Hidden="True" BaseColumnName="WCInfoIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업계획수량" Key="WorkPlanQuantity" Hidden="True" Format="###,###,###.##"
															BaseColumnName="WorkPlanQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="이전완료수량" Key="WorkCompletionQuantity" Hidden="True" Format="###,###,###.##"
															BaseColumnName="WorkCompletionQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="잔량" Key="RemainQuantity" Hidden="True" BaseColumnName="RemainQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="비작업사유코드1" Key="NonWorkTimeCode1" Hidden="True" BaseColumnName="NonWorkTimeCode1">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="제품번호" Key="ProductItemNum" Hidden="True" BaseColumnName="ProductItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="비작업시간2" Key="NonWorkTime2" BaseColumnName="NonWorkTime2">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="비작업사유코드2" Key="NonWorkTimeCode2" Hidden="True" BaseColumnName="NonWorkTimeCode2">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="비작업사유2" Key="NonWorkTimeReason2" BaseColumnName="NonWorkTimeReason2">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="비작업시간3" Key="NonWorkTime3" BaseColumnName="NonWorkTime3">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="비작업사유코드3" Key="NonWorkTimeCode3" Hidden="True" BaseColumnName="NonWorkTimeCode3">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="비작업사유3" Key="NonWorkTimeReason3" BaseColumnName="NonWorkTimeReason3">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합현상코드" Key="UnSuitabilityStatusCode" Hidden="True" BaseColumnName="UnSuitabilityStatusCode">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합원인코드" Key="UnSuitabilityCauseCode" Hidden="True" BaseColumnName="UnSuitabilityCauseCode">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="검사판정코드" Key="InspectionDecisionCode" Hidden="True" BaseColumnName="InspectionDecisionCode">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="기대번호(From)" Key="EtcNum1" BaseColumnName="EtcNum1"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="기대번호(To)" Key="EtcNum2" BaseColumnName="EtcNum2"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사용공구1" Key="UseTool1" BaseColumnName="UseTool1">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사용치구2" Key="UseJig1" BaseColumnName="UseJig1">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사용공구2" Key="UseTool2" BaseColumnName="UseTool2">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사용치구2" Key="UseJig2" BaseColumnName="UseJig2">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사용공구3" Key="UseTool3" BaseColumnName="UseTool3">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사용치구3" Key="UseJig3" BaseColumnName="UseJig3">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" BaseColumnName="RegistrationPerson">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" BaseColumnName="UpdatingPerson">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="생산계획번호" Key="ProductionPlanHistoryIndex" Hidden="True" BaseColumnName="ProductionPlanHistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="WC작업계획번호" Key="WCDailyWorkPlanHistoryIndex" Hidden="True" BaseColumnName="WCDailyWorkPlanHistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업일보번호" Key="WorkDailyReportHistoryIndex" Hidden="True" BaseColumnName="WorkDailyReportHistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="제품도면번호" Key="ProductDrawNum" Hidden="True" BaseColumnName="ProductDrawNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
								<TR>
									<TD style="PADDING-LEFT: 10px; HEIGHT: 35px"><asp:button id="bt_Excel" runat="server" Text="Excel" Width="60px" Height="20px" Font-Size="9pt"></asp:button></TD>
									<TD style="PADDING-RIGHT: 10px" vAlign="middle" align="right" colSpan="2">&nbsp;&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			<igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter></form>
	</body>
</HTML>
