<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v1.1, Version=1.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="ProductionPlanPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.ProductionPlanPC" codePage="949" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ProductionPlanPC</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../LinkLine.css" type="text/css" rel="stylesheet">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../GridCheck.js"></script>
		<script language="javascript" src="../GridDateUpDate.js"></script>
		<script language="javascript" src="../PopupWindows.js"></script>
		<script language="javascript" src="../MessageWindows.js"></script>
		<script>
		function ResettxtBox()
		{	
			ResetTextBox();			
			var objChooser1 = igdrp_getComboById("wdcMinDate");
			var objChooser2 = igdrp_getComboById("wdcMaxDate");
			var objChooser3 = igdrp_getComboById("wdcStartDate");
			var objChooser4 = igdrp_getComboById("wdcEndDate");
			objChooser1.setValue(null);
			objChooser2.setValue(null);	
			objChooser3.setValue(null);
			objChooser4.setValue(null);	
			document.Form1.ddlState.options[0].selected=true;
			document.Form1.ddlItemClassification1.options[0].selected=true;
					
		}
		</script>
	</HEAD>
	<body bgColor="#f7f6f6" bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE style="LEFT: 10px; WIDTH: 800px; POSITION: absolute; TOP: 10px" cellSpacing="0"
				cellPadding="0" id="Table1" width="800" border="0">
				<TR>
					<TD style="WIDTH: 19px; HEIGHT: 540px" width="19"><FONT face="굴림"></FONT></TD>
					<TD vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<td colspan="11">
										<table id="Table4" border="0" cellpadding="0" cellspacing="0">
											<tr>
												<TD vAlign="middle" align="left" colSpan="6" height="30" width="600"><FONT face="굴림">
														<uc1:ItemSearchControl id="ItemSearchControl1" runat="server"></uc1:ItemSearchControl></FONT></TD>
												<TD vAlign="middle" align="right" width="70" height="30"><FONT face="굴림">제품팀</FONT></TD>
												<TD vAlign="middle" align="left" width="130" height="30"><FONT face="굴림"></FONT><FONT face="굴림">
														<asp:dropdownlist id="ddlItemClassification1" runat="server" BackColor="#EEEEE9" Width="100px"></asp:dropdownlist></FONT></TD>
											</tr>
										</table>
									</td>
								</TR>
								<TR>
									<TD vAlign="middle" align="center" width="50" height="30">생산일&nbsp;</TD>
									<TD vAlign="middle" align="left" width="100" height="30"><FONT face="굴림">
											<igsch:webdatechooser id="wdcMinDate" runat="server" Font-Size="10pt" Height="18px" Width="100px" BackColor="#EEEEE9"
												Text="날짜 선택" NullDateLabel=" " BorderStyle="Solid" BorderColor="DimGray">
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
									<TD vAlign="middle" align="center" width="5" height="30"><FONT face="굴림">~</FONT></TD>
									<TD vAlign="middle" align="left" width="100" height="30">
										<igsch:webdatechooser id="wdcMaxDate" runat="server" Font-Size="10pt" Height="18px" Width="100px" BackColor="#EEEEE9"
											Text="날짜 선택" NullDateLabel=" " BorderStyle="Solid" BorderColor="DimGray">
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
									<TD vAlign="middle" align="center" width="50" height="30">납기일</TD>
									<TD vAlign="middle" align="left" width="100" height="30"><FONT face="굴림">
											<igsch:webdatechooser id="wdcStartDate" runat="server" Font-Size="10pt" Height="18px" Width="100px" BackColor="#EEEEE9"
												Text="날짜 선택" NullDateLabel=" " BorderStyle="Solid" BorderColor="DimGray">
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
									<TD vAlign="middle" align="center" width="5" height="30"><FONT face="굴림">~</FONT></TD>
									<TD vAlign="middle" align="left" width="100" height="30">
										<igsch:webdatechooser id="wdcEndDate" runat="server" Font-Size="10pt" Height="18px" Width="100px" BackColor="#EEEEE9"
											Text="날짜 선택" NullDateLabel=" " BorderStyle="Solid" BorderColor="DimGray">
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
									<TD vAlign="middle" align="right" width="40"><FONT face="굴림">상태&nbsp;</FONT></TD>
									<TD vAlign="middle" align="left" width="90"><FONT face="굴림">
											<asp:dropdownlist id="ddlState" runat="server" BackColor="#EEEEE9" Width="90px">
												<asp:ListItem Value="  ">-선 택-</asp:ListItem>
												<asp:ListItem Value="대기">대기</asp:ListItem>
												<asp:ListItem Value="진행">진행</asp:ListItem>
												<asp:ListItem Value="완료">완료</asp:ListItem>
												<asp:ListItem Value="중단">중단</asp:ListItem>
											</asp:dropdownlist></FONT></TD>
									<TD vAlign="middle" align="right" height="30" width="160"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
											type="button" value="초기화" name="btnReset">&nbsp;
										<asp:button id="btnSearch" runat="server" Font-Size="10pt" Height="20px" Width="60px" Text="검   색"
											CommandName="Search"></asp:button>&nbsp;
									</TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" width="80" colSpan="11" height="8"><FONT face="굴림"></FONT></TD>
								</TR>
							</TABLE>
						</FIELDSET>
						<BR>
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[검색결과]</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD width="70%" height="20" colSpan="2"></TD>
									<TD style="PADDING-RIGHT: 10px" align="right" height="20"><INPUT id="volumNum" style="WIDTH: 26px; HEIGHT: 12px" type="hidden" size="1" value="0"
											name="Hidden1" runat="server"><asp:button id="btnPre" runat="server" Font-Size="8pt" Width="16px" Height="16px" Text="◀" CommandName="Video"
											CommandArgument="Pre"></asp:button><asp:button id="btnNow" runat="server" Font-Size="8pt" Width="16px" Height="16px" Text="■" CommandName="Video"
											CommandArgument="Now"></asp:button><asp:button id="btnNext" runat="server" Font-Size="8pt" Width="16px" Height="16px" Text="▶"
											CommandName="Video" CommandArgument="Next"></asp:button></TD>
								</TR>
								<TR>
									<TD vAlign="top" align="center" colSpan="3"><igtbl:ultrawebgrid id="uwgPP_HT" runat="server" Width="100%" Height="366px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="uwgPPxHT" TableLayout="Fixed" CellClickActionDefault="RowSelect"
												AllowUpdateDefault="RowTemplateOnly">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</AddNewBox>
												<Pager QuickPages="5" PageSize="15" StyleMode="ComboBox" AllowPaging="True">
													<Style VerticalAlign="Middle" BorderWidth="1px" BorderStyle="Solid" HorizontalAlign="Right"
														BackColor="LightGray">
													</Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" BorderColor="Black" BorderStyle="Solid" ForeColor="Black" BackColor="LightGray"
													Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<GroupByRowStyleDefault ForeColor="Black" BackColor="#95B9F0"></GroupByRowStyleDefault>
												<RowSelectorStyleDefault BorderStyle="Solid"></RowSelectorStyleDefault>
												<FrameStyle Width="100%" Cursor="Default" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림"
													BorderColor="DimGray" BorderStyle="Solid" BackColor="Silver" Height="366px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents ColumnHeaderClickHandler="ColumnHeaderClickHandler" BeforeRowTemplateOpenHandler="uwgPP_HT_BeforeRowTemplateOpenHandler"
													AfterRowTemplateCloseHandler="uwgPP_HT_AfterRowTemplateCloseHandler"></ClientSideEvents>
												<GroupByBox>
													<Style BackColor="#B5CFF7">
													</Style>
													<BandLabelStyle ForeColor="White" BackColor="#6372D4"></BandLabelStyle>
												</GroupByBox>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedGroupByRowStyleDefault BorderColor="Yellow" BackColor="Yellow"></SelectedGroupByRowStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" Font-Bold="True" ForeColor="Linen" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="Gray"
													BorderStyle="Solid" ForeColor="Black" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails ColorTop="Gray" WidthLeft="0px" WidthTop="0px" ColorLeft="Gray"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand AllowUpdate="RowTemplateOnly">
													<Columns>
														<igtbl:UltraGridColumn HeaderText="선택" Key="chk" Width="40px" Type="CheckBox" HeaderClickAction="Select"
															BaseColumnName="chk" AllowUpdate="Yes">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목상태" Key="ItemState" BaseColumnName="ItemState"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" BaseColumnName="ItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left">
																<Padding Left="2px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" BaseColumnName="ItemDrawNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left">
																<Padding Left="2px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" BaseColumnName="ItemName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left">
																<Padding Left="2px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="생산계획원천코드" Key="ProductionPlanHistorySourceCode" Hidden="True" BaseColumnName="ProductionPlanHistorySourceCode">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="생산계획원천" Key="ProductionPlanHistorySource" Width="80px" BaseColumnName="ProductionPlanHistorySource">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="생산계획량" Key="ProductionPlanQuantity" Width="70px" Format="###,###,###"
															BaseColumnName="ProductionPlanQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="생산시작일" Key="ProductionBeginDate" Format="yyyy-MM-dd" BaseColumnName="ProductionBeginDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="납기요구일" Key="DeliveryDate" Format="yyyy-MM-dd" BaseColumnName="DeliveryDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="자재산출여부" Key="RowMaterialCalculation" Width="70px" BaseColumnName="RowMaterialCalculation">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" Width="70px" BaseColumnName="ProgressCondition">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="불륨번호" Key="VolumNum" Width="70px" BaseColumnName="VolumNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" BaseColumnName="RegistrationPerson">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" BaseColumnName="UpdatingPerson">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="이전번호" Key="HistoryIndex" Hidden="True" BaseColumnName="HistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="이전구분" Key="HistorySection" Hidden="True" BaseColumnName="HistorySection">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="생산계획원장번호" Key="ProductionPlanHistoryIndex" Hidden="True" BaseColumnName="ProductionPlanHistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
													</Columns>
													<RowTemplateStyle BackColor="White"></RowTemplateStyle>
													<RowEditTemplate>
														<P align="center">
															<TABLE id="Table5" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
																cellSpacing="0" cellPadding="0" width="800" border="0">
																<TR>
																	<TD align="right" bgColor="lavender" height="25">
																		<asp:Label id="Label1" runat="server" Font-Size="10pt" Font-Names="굴림">품목번호 </asp:Label><FONT face="굴림">&nbsp;</FONT></TD>
																	<TD height="25">
																		<asp:TextBox id="txtItemNum" runat="server" BorderColor="DimGray" BorderStyle="Solid" BackColor="#F0F0F0"
																			Width="110px" Height="20px" Font-Size="10pt" Font-Names="굴림" BorderWidth="1px" ReadOnly="True"
																			columnkey="ItemNum"></asp:TextBox></TD>
																	<TD align="right" bgColor="lavender" height="25">
																		<asp:Label id="Label6" runat="server" Font-Size="10pt" Font-Names="굴림">도면번호 </asp:Label><FONT face="굴림">&nbsp;</FONT></TD>
																	<TD height="25">
																		<asp:TextBox id="txtItemDrawNum" runat="server" BorderColor="DimGray" BorderStyle="Solid" BackColor="#F0F0F0"
																			Width="110px" Height="20px" Font-Size="10pt" Font-Names="굴림" BorderWidth="1px" ReadOnly="True"
																			columnkey="ItemDrawNum"></asp:TextBox></TD>
																	<TD align="right" width="100" bgColor="lavender" height="25">
																		<asp:Label id="Label7" runat="server" Font-Size="10pt" Font-Names="굴림">품목명 </asp:Label><FONT face="굴림">&nbsp;</FONT></TD>
																	<TD height="25">
																		<asp:TextBox id="txtItemName" runat="server" BorderColor="DimGray" BorderStyle="Solid" BackColor="#F0F0F0"
																			Width="110px" Height="20px" Font-Size="10pt" Font-Names="굴림" BorderWidth="1px" ReadOnly="True"
																			columnkey="ItemName"></asp:TextBox></TD>
																</TR>
																<TR>
																	<TD align="right" width="100" bgColor="lavender" height="25">
																		<asp:Label id="Label8" runat="server" Font-Size="10pt" Font-Names="굴림">생산계획수량 </asp:Label><FONT face="굴림">&nbsp;</FONT></TD>
																	<TD height="25"><FONT face="굴림">
																			<igtxt:WebNumericEdit id="wnePlanQuantity" runat="server" BorderColor="DimGray" BorderStyle="Solid" Width="110px"
																				Height="20px" Font-Size="10pt" BorderWidth="1px" columnkey="ProductionPlanQuantity"></igtxt:WebNumericEdit></FONT></TD>
																	<TD align="right" width="100" bgColor="lavender" height="25">
																		<asp:Label id="Label9" runat="server" Font-Size="10pt" Font-Names="굴림">생산시작일 </asp:Label><FONT face="굴림">&nbsp;</FONT></TD>
																	<TD height="25">
																		<igtxt:WebDateTimeEdit id="wdeStartDate" runat="server" BorderColor="DimGray" BorderStyle="Solid" Width="110px"
																			Height="20px" BorderWidth="1px" HorizontalAlign="Center" UseBrowserDefaults="False" DisplayModeFormat="d">
																			<ButtonsAppearance>
																				<ButtonPressedStyle BorderStyle="Inset">
																					<BorderDetails ColorBottom="White" ColorRight="White"></BorderDetails>
																				</ButtonPressedStyle>
																				<ButtonStyle BorderWidth="2px" BorderColor="White" BorderStyle="Outset" BackColor="Control">
																					<BorderDetails StyleBottom="Ridge" ColorBottom="0, 160, 160, 160" ColorRight="0, 160, 160, 160"
																						StyleRight="Ridge"></BorderDetails>
																				</ButtonStyle>
																			</ButtonsAppearance>
																			<SpinButtons Display="OnRight"></SpinButtons>
																			<BorderDetails ColorTop="0, 160, 160, 160" StyleTop="Ridge" StyleLeft="Ridge" ColorLeft="0, 160, 160, 160"></BorderDetails>
																		</igtxt:WebDateTimeEdit></TD>
																	<TD height="25"><FONT face="굴림"></FONT></TD>
																	<TD style="PADDING-RIGHT: 10px" align="right" height="25"><FONT face="굴림"><INPUT id="igtbl_reOkBtn" style="WIDTH: 60px; HEIGHT: 20px" onclick="igtbl_gRowEditButtonClick(event);__doPostBack('lnkRowUpDate','');"
																				type="button" value="수   정"></FONT></TD>
																</TR>
																<TR>
																	<TD style="PADDING-RIGHT: 10px" align="right" colSpan="6" height="10"></TD>
																</TR>
															</TABLE>
														</P>
													</RowEditTemplate>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
								<TR>
									<td><FONT face="굴림">&nbsp;</FONT><asp:button id="btnExcel" runat="server" Font-Size="10pt" Width="60px" Height="20px" Text="Excel"
											CommandName="Excel" Enabled="False"></asp:button><igtblexp:ultrawebgridexcelexporter id="uwgExcel" runat="server"></igtblexp:ultrawebgridexcelexporter><asp:linkbutton id="lnkRowUpDate" runat="server"></asp:linkbutton></td>
									<TD style="HEIGHT: 46px" vAlign="middle" align="right" colSpan="2"><asp:button id="btnDelete" runat="server" Font-Size="10pt" Width="60px" Height="20px" Text="삭   제"
											CommandName="Delete" Enabled="False"></asp:button><FONT face="굴림">&nbsp;&nbsp; </FONT>
										<asp:button id="btnCancle" runat="server" Font-Size="10pt" Width="60px" Height="20px" Text="취   소"
											CommandName="Cancle" Enabled="False"></asp:button><FONT face="굴림">&nbsp;&nbsp; </FONT>
										<asp:button id="btnStop" runat="server" Font-Size="10pt" Width="60px" Height="20px" Text="중   단"
											CommandName="Stop" Enabled="False"></asp:button><FONT face="굴림">&nbsp;</FONT></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="ProductionPlanHistoryIndex" style="Z-INDEX: 102; LEFT: 24px; POSITION: absolute; TOP: 616px"
				type="hidden" name="Hidden1" runat="server">
		</form>
	</body>
</HTML>
