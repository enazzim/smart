<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="BuyingDeliveryPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.EtcPresentCondition.BuyingDeliveryPC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BuyingDeliveryPC</title>
		<meta http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../GridCheck.js"></script>
		<script language="javascript" src="../MessageWindows.js"></script>
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--
		function ResettxtBox()
		{
			
			ResetTextBox();
			ResetBox();
			var objChooser1 = igdrp_getComboById("wdcStartDate");
			var objChooser2 = igdrp_getComboById("wdcEndDate");
			objChooser1.setValue(null);
			objChooser2.setValue(null);	
			document.Form1.ddlItemClassification1.options[0].selected=true;
		}
		
--></SCRIPT>
	</HEAD>
	<body bottomMargin="0" bgColor="#f7f6f6" leftMargin="0" topMargin="0" rightMargin="0"
		MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="LEFT: 10px; POSITION: absolute; TOP: 10px" cellSpacing="0" cellPadding="0"
				width="800" border="0">
				<TR>
					<TD style="HEIGHT: 92px"><FONT face="굴림"></FONT></TD>
					<TD style="WIDTH: 800px; HEIGHT: 92px" height="92">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 20px"
							align="middle"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색 ]
							</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
								<TR height="35">
									<TD align="left" width="800" colSpan="8" height="30">
										<table id="table4" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
											<tr>
												<td width="200"><uc1:companysearchcontrol id="CSC1" runat="server"></uc1:companysearchcontrol></td>
												<td width="600"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></td>
											</tr>
										</table>
									</TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30">입고일자&nbsp;</TD>
									<TD align="left" width="100" height="30"><igsch:webdatechooser id="wdcStartDate" runat="server" NullDateLabel=" " Text=" " Width="100px" BackColor="#EEEEE9"
											BorderStyle="Solid" Height="20px" BorderColor="DimGray">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid" BackColor="#EEEEE9"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD align="center" width="5" height="30">~</TD>
									<TD width="100" height="30"><igsch:webdatechooser id="wdcEndDate" runat="server" NullDateLabel=" " Text=" " Width="100px" BackColor="#EEEEE9"
											BorderStyle="Solid" Height="20px" BorderColor="DimGray">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid" BackColor="#EEEEE9"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<td align="right" width="70">제품팀&nbsp;</td>
									<td align="left" width="100"><asp:dropdownlist id="ddlItemClassification1" runat="server" Width="115px" BackColor="#EEEEE9"></asp:dropdownlist></td>
									<TD align="right" width="355" colSpan="2" height="30"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
											type="button" value="초기화" name="btnReset">&nbsp;
										<asp:button id="btnSearch" runat="server" Text="검   색" Width="65px" Height="20px" Font-Size="9pt"></asp:button>&nbsp;
									</TD>
								</TR>
								<TR>
									<TD align="right" colSpan="8" height="8"></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 400px" width="20"></TD>
					<TD style="WIDTH: 800px; HEIGHT: 400px" vAlign="top">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; BORDER-BOTTOM: dimgray 2px solid"
							align="top"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색결과 ]
							</LEGEND>
							<TABLE id="Table4" height="400" cellSpacing="0" cellPadding="0" width="800" align="right">
								<TR>
									<TD vAlign="middle" align="center" height="400"><igtbl:ultrawebgrid id="uwgBD_HT" runat="server" Width="800px" Height="407px" UseAccessibleHeader="True">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="uwgBDxHT" TableLayout="Fixed" CellClickActionDefault="RowSelect"
												NoDataMessage="해당 데이타가 없습니다.">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</AddNewBox>
												<Pager QuickPages="5" PageSize="17" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray" Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<RowSelectorStyleDefault Cursor="Hand" BackColor="LightGray"></RowSelectorStyleDefault>
												<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="407px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" BorderStyle="Dotted" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="Gray"
													BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="120px" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
															<HeaderStyle Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ItemDrawNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="규 격" Key="Standard" BaseColumnName="Standard"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="회사명" Key="CompanyName" HeaderClickAction="SortMulti" BaseColumnName="CompanyName">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="BusinessRegistrationNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="입고수량" Key="DeliveryQuantity" Format="###,###,##0.00" HeaderClickAction="SortMulti"
															BaseColumnName="DeliveryQuantity">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="입고일자" Key="DeliveryDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="DeliveryDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Format="###,###,##0.00" HeaderClickAction="SortMulti"
															BaseColumnName="ApplyUnitCost">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="Lot번호" Key="LotNum" Hidden="True" BaseColumnName="LotNum"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총금액" Key="TotalCost" Format="###,###,##0.00" HeaderClickAction="SortMulti"
															BaseColumnName="TotalCost">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" HeaderClickAction="SortMulti" BaseColumnName="ProgressCondition">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPerson">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationPersonID">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" HeaderClickAction="SortMulti" BaseColumnName="UpdatingPerson">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingPersonID">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="구매발주원장번호" Key="BuyingOrderHistoryIndex" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="BuyingOrderHistoryIndex">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="구매의뢰납품원장번호" Key="BuyingDeliveryRequestHistoryIndex" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="BuyingDeliveryRequestHistoryIndex">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="구매납품원장번호" Key="BuyingDeliveryHistoryIndex" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="BuyingDeliveryHistoryIndex">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="입고대기량" Key="InStoreWaitingQuantity" Hidden="True" BaseColumnName="InStoreWaitingQuantity"></igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="20"></TD>
					<TD>
						<TABLE id="Table6" style="HEIGHT: 20px" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD style="WIDTH: 100px" align="left"><asp:button id="btnExcel" runat="server" Text="Excel" Width="65px" Height="20px" Font-Size="9pt"></asp:button></TD>
								<TD align="right">&nbsp;</TD>
								<TD align="right" colSpan="2">
									<igtblexp:ultrawebgridexcelexporter id="uwgExcel" runat="server"></igtblexp:ultrawebgridexcelexporter><asp:literal id="Literal1" runat="server"></asp:literal>&nbsp;</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			&nbsp;
			<P align="center"><FONT face="굴림"></FONT>&nbsp;</P>
		</form>
	</body>
</HTML>
