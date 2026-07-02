<%@ Page language="c#" Codebehind="ReceiveingPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.EtcPresentCondition.ReceiveingPC" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ReceiveingPC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
			document.ReceiveingPC.dl_ProgressState.options[0].selected=true;
			document.ReceiveingPC.ddlItemClassification1.options[0].selected=true;
		}
		--></SCRIPT>
	</HEAD>
	<body bgColor="#f7f6f6" MS_POSITIONING="GridLayout">
		<form id="ReceiveingPC" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; WIDTH: 813px; POSITION: absolute; TOP: 10px; HEIGHT: 550px"
				height="550" cellSpacing="0" cellPadding="0" width="813" border="0">
				<TR>
					<TD vAlign="top" align="center" width="800" colSpan="2">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 800px; BORDER-BOTTOM: #696969 2px solid"><LEGEND style="FONT-SIZE: 9pt" align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800">
								<TR>
									<TD width="800" colSpan="9" height="30"><FONT face="굴림">
											<table id="Table3" border="0" cellpadding="0" cellspacing="0">
												<tr>
													<td width="200"><uc1:CompanySearchControl id="CSC1" runat="server"></uc1:CompanySearchControl></td>
													<td width="600"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></td>
												</tr>
											</table>
										</FONT>
									</TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림">진행상태&nbsp;</FONT></TD>
									<TD width="100" height="30">
										<asp:dropdownlist id="dl_ProgressState" runat="server" Width="100px" Height="20px" BackColor="#EEEEE9">
											<asp:ListItem Value=" ">-선 택-</asp:ListItem>
											<asp:ListItem Value="미납">미납</asp:ListItem>
											<asp:ListItem Value="중단">중단</asp:ListItem>
											<asp:ListItem Value="완료">완료</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD align="right" width="70" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림">납기일자&nbsp;</FONT></TD>
									<TD width="100" height="30">
										<igsch:webdatechooser id="wdcStartDate" runat="server" Width="100px" Height="22px" BackColor="#EEEEE9"
											BorderColor="DimGray" BorderStyle="Solid" Text=" " NullDateLabel=" ">
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
									<TD align="center" width="5" height="30"><FONT face="굴림">~</FONT></TD>
									<TD width="100" height="30">
										<igsch:webdatechooser id="wdcEndDate" runat="server" Width="100px" Height="22px" BackColor="#EEEEE9" BorderColor="DimGray"
											BorderStyle="Solid" Text=" " NullDateLabel=" ">
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
									<td width="70" align="right"><FONT face="굴림">제품팀&nbsp; </FONT>
									</td>
									<td width="115" align="left">
										<asp:dropdownlist id="ddlItemClassification1" runat="server" BackColor="#EEEEE9" Width="115px"></asp:dropdownlist></td>
									<TD align="right" width="170" height="30"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
											type="button" value="초기화" name="btnReset"><FONT face="굴림">&nbsp; </FONT>
										<asp:button id="bt_Search" runat="server" Width="60px" Height="20px" Text="검  색"></asp:button>&nbsp;</TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="5" colSpan="9"><FONT face="굴림"></FONT><FONT face="굴림"></FONT></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" width="800" colSpan="2">&nbsp;
						<br>
						<FIELDSET title="86" style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND style="FONT-SIZE: 9pt" align="top">[검색결과]</LEGEND>
							<TABLE id="Table4" style="WIDTH: 100%" cellSpacing="1" cellPadding="1" width="800" border="0">
								<TR>
									<TD align="left" width="100" height="10"><FONT face="굴림"></FONT></TD>
									<td align="right" height="10"><INPUT id="Volum" style="WIDTH: 26px; HEIGHT: 12px" type="hidden" size="1" value="0" name="Hidden1"
											runat="server"><asp:button id="btnPre" runat="server" Height="16px" Width="16px" Font-Size="8pt" Text="◀" CommandName="Pre"></asp:button><asp:button id="btnNow" runat="server" Height="16px" Width="16px" Font-Size="8pt" Text="■" CommandName="Now"></asp:button><asp:button id="btnNext" runat="server" Height="16px" Width="16px" Font-Size="8pt" Text="▶"
											CommandName="Next"></asp:button></td>
								</TR>
								<tr>
									<td align="center" width="800" colSpan="2"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="386px" Width="100%">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect"
												NoDataMessage="해당 자료가 없습니다!">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</AddNewBox>
												<Pager PageSize="16" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</Pager>
												<HeaderStyleDefault Cursor="Auto" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
												<FrameStyle Width="100%" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
													BackColor="Silver" Height="386px"></FrameStyle>
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
														<igtbl:UltraGridColumn HeaderText="품목상태" Key="ItemState" BaseColumnName="ItemState">
															<HeaderStyle Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ItemDrawNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" HeaderClickAction="SortMulti" BaseColumnName="CompanyName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="BusinessRegistrationNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="자산분류" Key="PropertyClassification" Width="70px" HeaderClickAction="SortMulti"
															BaseColumnName="PropertyClassification">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="생산의뢰여부" Key="ProductionRequestDivision" Hidden="True" BaseColumnName="ProductionRequestDivision">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수주일자" Key="ReceivingOrderDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="ReceivingOrderDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="1차납기요구량" Key="DeliveryRequestQuantity1" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestQuantity1">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총수주량" Key="TotalReceiveingOrderQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
															BaseColumnName="TotalReceiveingOrderQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="납기요구일" Key="DeliveryRequestDate1" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="DeliveryRequestDate1">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="2차납기요구량" Key="DeliveryRequestQuantity2" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestQuantity2">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="2차납기요구일" Key="DeliveryRequestDate2" Hidden="True" Format="yyyy-MM-dd"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestDate2">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="3차납기요구량" Key="DeliveryRequestQuantity3" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestQuantity3">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="3차납기요구일" Key="DeliveryRequestDate3" Hidden="True" Format="yyyy-MM-dd"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestDate3">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="4차납기요구량" Key="DeliveryRequestQuantity4" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestQuantity4">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="4차납기요구일" Key="DeliveryRequestDate4" Hidden="True" Format="yyyy-MM-dd"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestDate4">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="5차납기요구량" Key="DeliveryRequestQuantity5" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestQuantity5">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="5차납기요구일" Key="DeliveryRequestDate5" Hidden="True" Format="yyyy-MM-dd"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestDate5">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
															BaseColumnName="ApplyUnitCost">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총금액" Key="TotalCost" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
															BaseColumnName="TotalCost">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="출고수량" Key="OutStorehouseQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
															BaseColumnName="OutStorehouseQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="적합수량" Key="SuitabilityQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
															BaseColumnName="SuitabilityQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="미검수량" Key="UnInspectionQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
															BaseColumnName="UnInspectionQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="납품잔량" Key="RemainderQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
															BaseColumnName="RemainderQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="발주번호" Key="OrderNum" HeaderClickAction="SortMulti" BaseColumnName="OrderNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="납품장소" Key="DeliveryPlace" HeaderClickAction="SortMulti" BaseColumnName="DeliveryPlace">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="볼륨번호" Key="VolumNum" HeaderClickAction="SortMulti" BaseColumnName="VolumNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" HeaderClickAction="SortMulti" BaseColumnName="ProgressCondition">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPerson">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationPersonID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" HeaderClickAction="SortMulti" BaseColumnName="UpdatingPerson">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingPersonID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="번호" Key="ReceivingOrderHistoryIndex" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ReceivingOrderHistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="상태" Key="Progress" Hidden="True" BaseColumnName="Progress"></igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></td>
								</tr>
								<TR>
									<TD style="WIDTH: 34px" height="30"><asp:button id="Button3" runat="server" Height="20px" Width="60px" Text="Excel"></asp:button></TD>
									<TD align="right" height="30">&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			<igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter></form>
	</body>
</HTML>
