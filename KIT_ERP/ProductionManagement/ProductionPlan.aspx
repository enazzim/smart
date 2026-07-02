<%@ Page language="c#" Codebehind="ProductionPlan.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.ProductionPlan" codePage="949" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ProductionPlan</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../LinkLine.css" type="text/css" rel="stylesheet">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../GridCheck.js"></script>
		<script language="javascript" src="../MessageWindows.js"></script>
	</HEAD>
	<body bgColor="#f7f6f6" bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE style="LEFT: 10px; WIDTH: 800px; POSITION: absolute; TOP: 10px" cellSpacing="0"
				cellPadding="0" id="Table1" width="800" border="0">
				<TBODY>
					<TR>
						<TD style="WIDTH: 11px" width="11"></TD>
						<TD vAlign="top" align="center" width="800">
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[검색조건]</LEGEND>
								<TABLE id="Table2" style="WIDTH: 800px" height="40" cellSpacing="0" cellPadding="0" width="800"
									border="0" DESIGNTIMEDRAGDROP="53">
									<TR>
										<TD vAlign="middle" align="left" colSpan="8" height="30" width="800">
											<table id="table3" border="0" cellpadding="0" cellspacing="0" width="800">
												<tr>
													<td width="600"><uc1:ItemSearchControl id="ItemSearchControl1" runat="server"></uc1:ItemSearchControl>
													</td>
													<td width="70" align="right">제품팀&nbsp;</td>
													<td width="130" align="left">
														<asp:dropdownlist id="ddlItemClassification1" runat="server" Width="100px" BackColor="#EEEEE9"></asp:dropdownlist></td>
												</tr>
											</table>
										</TD>
									</TR>
									<TR>
										<TD vAlign="middle" align="right" width="85" height="30">납기일&nbsp;</TD>
										<TD vAlign="middle" align="left" width="100" height="30">
											<igsch:webdatechooser id="wdcMinDate" runat="server" Font-Size="10pt" BackColor="#EEEEE9" Width="100px"
												Height="18px" NullDateLabel=" " Text=" " BorderColor="DimGray" BorderStyle="Solid">
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
										<TD vAlign="middle" align="center" width="5" height="30">~</TD>
										<TD vAlign="middle" align="left" width="100" height="30">
											<igsch:webdatechooser id="wdcMaxDate" runat="server" Font-Size="10pt" BackColor="#EEEEE9" Width="100px"
												Height="18px" NullDateLabel=" " Text=" " BorderColor="DimGray" BorderStyle="Solid">
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
										<TD vAlign="middle" align="right" width="520" height="30" colspan="4">&nbsp;
											<asp:button id="Button1" runat="server" Font-Size="10pt" Width="120px" Height="20px" Text="실행계획 생산수립"
												CommandName="Search"></asp:button>&nbsp;
											<asp:button id="btnClear" runat="server" Font-Size="10pt" Width="60px" Height="20px" Text="초기화"
												CommandName="Clear"></asp:button>&nbsp;
											<asp:button id="btnSearch" runat="server" Font-Size="10pt" Width="60px" Height="20px" Text="검   색"
												CommandName="Search"></asp:button>&nbsp;&nbsp;
										</TD>
									</TR>
									<TR>
										<TD vAlign="middle" align="right" width="70" colSpan="8" height="8"></TD>
									</TR>
								</TABLE>
							</FIELDSET>
							<BR>
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"
								DESIGNTIMEDRAGDROP="245"><LEGEND align="top">[검색결과]</LEGEND>
								<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" border="0">
									<TBODY>
										<TR>
											<TD height="30"><asp:checkbox id="CheckBox1" runat="server" Text="전체선택" Visible="False"></asp:checkbox></TD>
											<TD align="right" width="15%">생산시작일&nbsp;</TD>
											<TD align="left" height="30" width="15%"><igsch:webdatechooser id="wdcStartDate" runat="server" Height="18px" Width="100px" Font-Size="10pt" Text=" "
													NullDateLabel=" " BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray">
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
										</TR>
										<TR>
											<TD vAlign="top" align="center" colSpan="3"><igtbl:ultrawebgrid id="uwgPR_HT" runat="server" Height="350px" Width="100%">
													<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
														RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
														HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
														RowSelectorsDefault="No" Name="uwgPRxHT" TableLayout="Fixed" CellClickActionDefault="RowSelect"
														NoDataMessage="해당 데이타가 없습니다." AllowUpdateDefault="Yes">
														<AddNewBox>
															<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
															</Style>
														</AddNewBox>
														<Pager PageSize="15" StyleMode="ComboBox" AllowPaging="True">
															<Style VerticalAlign="Middle" BorderWidth="1px" BorderStyle="Solid" HorizontalAlign="Center"
																BackColor="LightGray">
															</Style>
														</Pager>
														<HeaderStyleDefault Cursor="Hand" BorderColor="Black" BorderStyle="Solid" HorizontalAlign="Center" ForeColor="Black"
															BackColor="LightGray" Height="25px">
															<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
														</HeaderStyleDefault>
														<GroupByRowStyleDefault ForeColor="Black" BackColor="#95B9F0"></GroupByRowStyleDefault>
														<RowSelectorStyleDefault BorderStyle="Solid"></RowSelectorStyleDefault>
														<FrameStyle Width="100%" Cursor="Default" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림"
															BorderColor="DimGray" BorderStyle="Solid" BackColor="Silver" Height="350px"></FrameStyle>
														<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
															<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
														</FooterStyleDefault>
														<ClientSideEvents ColumnHeaderClickHandler="ColumnHeaderClickHandler"></ClientSideEvents>
														<GroupByBox>
															<Style BackColor="#B5CFF7">
															</Style>
															<BandLabelStyle ForeColor="White" BackColor="#6372D4"></BandLabelStyle>
														</GroupByBox>
														<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
														<SelectedGroupByRowStyleDefault BorderColor="Yellow" BackColor="Yellow"></SelectedGroupByRowStyleDefault>
														<SelectedRowStyleDefault Cursor="Hand" Font-Bold="True" ForeColor="Linen" BackColor="Navy"></SelectedRowStyleDefault>
														<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
														<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid" ForeColor="Black"
															BackColor="#EBEFF6">
															<Padding Left="3px"></Padding>
															<BorderDetails ColorTop="Gray" WidthLeft="0px" WidthTop="0px" ColorLeft="Gray"></BorderDetails>
														</RowStyleDefault>
													</DisplayLayout>
													<Bands>
														<igtbl:UltraGridBand AllowUpdate="RowTemplateOnly">
															<Columns>
																<igtbl:UltraGridColumn HeaderText="선택" Key="chk" EditorControlID="" Width="40px" Type="CheckBox" Format=""
																	HeaderClickAction="Select" BaseColumnName="" NullText="" AllowUpdate="Yes" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="품목상태" Key="ItemState" Width="96px" BaseColumnName="ItemState"></igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" EditorControlID="" Width="150px" Format="" BaseColumnName="ItemNum"
																	NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" EditorControlID="" Hidden="True" Format="" BaseColumnName="ItemDrawNum"
																	NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" EditorControlID="" Width="150px" Format="" BaseColumnName="ItemName"
																	NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="자산분류" Key="PropertyClassification" Hidden="True" BaseColumnName="PropertyClassification">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="생산의뢰원천" Key="ProductionRequestSource" EditorControlID="" Format="" BaseColumnName="ProductionRequestSource"
																	NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="총 생산의뢰량" Key="ProductionRequestQuantity" Width="120px" Format="###,###,##0.00"
																	BaseColumnName="ProductionRequestQuantity">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="1차 의뢰요구량" Key="RequestQuantity1" EditorControlID="" Hidden="True" Format="###,###,##0.00"
																	BaseColumnName="RequestQuantity1" NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="납기요구일" Key="RequestDate1" EditorControlID="" Width="140px" Format="yyyy-MM-dd"
																	BaseColumnName="RequestDate1" NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="2차 의뢰요구량" Key="RequestQuantity2" EditorControlID="" Hidden="True" Format="###,###,##0.00"
																	BaseColumnName="RequestQuantity2" NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="2차 의뢰요구일" Key="RequestDate2" EditorControlID="" Hidden="True" Format="yyyy-MM-dd"
																	BaseColumnName="RequestDate2" NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="3차 의뢰요구량" Key="RequestQuantity3" EditorControlID="" Hidden="True" Format="###,###,##0.00"
																	BaseColumnName="RequestQuantity3" NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="3차 의뢰요구일" Key="RequestDate3" EditorControlID="" Hidden="True" Format="yyyy-MM-dd"
																	BaseColumnName="RequestDate3" NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="4차 의뢰요구량" Key="RequestQuantity4" EditorControlID="" Hidden="True" Format="###,###,##0.00"
																	BaseColumnName="RequestQuantity4" NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="4차 의뢰요구일" Key="RequestDate4" EditorControlID="" Hidden="True" Format="yyyy-MM-dd"
																	BaseColumnName="RequestDate4" NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="5차 의뢰요구량" Key="RequestQuantity5" EditorControlID="" Hidden="True" Format="###,###,##0.00"
																	BaseColumnName="RequestQuantity5" NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="5차 의뢰요구일" Key="RequestDate5" EditorControlID="" Hidden="True" Format="yyyy-MM-dd"
																	BaseColumnName="RequestDate5" NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="적용단가" Key="ApplyUnitCost" EditorControlID="" Hidden="True" Format="\ ###,###,##0.00"
																	BaseColumnName="ApplyUnitCost" NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" EditorControlID="" Hidden="True" Format=""
																	BaseColumnName="ProgressCondition" NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" EditorControlID="" Hidden="True" Format=""
																	BaseColumnName="RegistrationPerson" NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" EditorControlID="" Hidden="True" Format="yyyy-MM-dd"
																	BaseColumnName="RegistrationDate" NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" EditorControlID="" Hidden="True" Format=""
																	BaseColumnName="UpdatingPerson" NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" EditorControlID="" Hidden="True" Format="yyyy-MM-dd"
																	BaseColumnName="UpdatingDate" NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="불륨번호" Key="VolumNum" EditorControlID="" Hidden="True" Format="" BaseColumnName="VolumNum"
																	NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="생산의뢰원장번호" Key="ProductionRequestHistoryIndex" Hidden="True" BaseColumnName="ProductionRequestHistoryIndex">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
															</Columns>
															<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
																<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
															</RowTemplateStyle>
														</igtbl:UltraGridBand>
													</Bands>
												</igtbl:ultrawebgrid><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="380px" Width="100%">
													<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
														RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
														HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
														RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect"
														NoDataMessage="해당 데이타가 없습니다." AllowUpdateDefault="Yes">
														<AddNewBox>
															<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
															</Style>
														</AddNewBox>
														<Pager>
															<Style VerticalAlign="Middle" BorderWidth="1px" BorderStyle="Solid" HorizontalAlign="Center"
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
															BorderColor="DimGray" BorderStyle="Solid" BackColor="Silver" Height="380px"></FrameStyle>
														<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
															<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
														</FooterStyleDefault>
														<ClientSideEvents ColumnHeaderClickHandler="ColumnHeaderClickHandler"></ClientSideEvents>
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
																<igtbl:UltraGridColumn HeaderText="선택" Key="chk" EditorControlID="" Width="40px" Type="CheckBox" Format=""
																	HeaderClickAction="Select" BaseColumnName="" NullText="" AllowUpdate="Yes" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="품목상태" Key="ItemState" BaseColumnName="ItemState"></igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" EditorControlID="" Width="150px" Format="" BaseColumnName="ItemNum"
																	NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" EditorControlID="" Hidden="True" Format="" BaseColumnName="ItemDrawNum"
																	NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" EditorControlID="" Width="150px" Format="" BaseColumnName="ItemName"
																	NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="실행계획일" Key="PlanDate" EditorControlID="" Width="120px" Format="yyyy-MM-dd"
																	BaseColumnName="PlanDate" NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="실행계획수량" Key="PlanQuantity" EditorControlID="" Width="120px" Format="###,###,##0.00"
																	BaseColumnName="PlanQuantity" NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="적용단가" Key="SaleUnitCost" EditorControlID="" Width="115px" Format="\ ###,###,##0.00"
																	BaseColumnName="SaleUnitCost" NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" EditorControlID="" Hidden="True" Format=""
																	BaseColumnName="RegistrationPerson" NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" EditorControlID="" Hidden="True" Format="yyyy-MM-dd"
																	BaseColumnName="RegistrationDate" NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" EditorControlID="" Hidden="True" Format=""
																	BaseColumnName="UpdatingPerson" NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" EditorControlID="" Hidden="True" Format="yyyy-MM-dd"
																	BaseColumnName="UpdatingDate" NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="실행계획번호" Key="ExecutionPlanInfoIndex" Hidden="True" BaseColumnName="ExecutionPlanInfoIndex">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
															</Columns>
															<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
																<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
															</RowTemplateStyle>
														</igtbl:UltraGridBand>
													</Bands>
												</igtbl:ultrawebgrid></TD>
										</TR>
										<TR>
											<TD vAlign="middle" height="35" align="right" colSpan="3"><asp:button id="btnOK" runat="server" Height="20" Width="65" Font-Size="10pt" Text="수    립"
													CommandName="Found"></asp:button>&nbsp;</TD>
										</TR>
									</TBODY>
								</TABLE>
							</FIELDSET>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
		</TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
	</body>
</HTML>
