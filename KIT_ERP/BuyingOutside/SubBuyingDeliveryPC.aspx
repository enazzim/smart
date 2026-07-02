<%@ Page language="c#" Codebehind="SubBuyingDeliveryPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BuyingOutside.SubBuyingDeliveryPC" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SubBuyingDeliveryPC</title>
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
			document.Form1.ddlProperty.options[0].selected=true;
		}
		
		function uwgSBD_HT_BeforeRowTemplateOpenHandler(gridName, rowId, templateId)
		{
			var frm = document.Form1;
			var row = igtbl_getRowById(rowId);
			//수정하기 이전의 납품수량
			frm.hdquantity.value = row.getCellFromKey("DeliveryQuantity").getValue();
			
			
			//매입년도선택
			for(var i=0;i<document.Form1.uwgSBD_HT__ctl0_ddlYear.options.length;i++)
			{
				if(document.Form1.uwgSBD_HT__ctl0_ddlYear.options[i].value == row.getCellFromKey("Year").getValue())
				{
					document.Form1.uwgSBD_HT__ctl0_ddlYear.options[i].selected=true
				}
			}
			//매입월 선택
			for(var i=0;i<document.Form1.uwgSBD_HT__ctl0_ddlMon.options.length;i++)
			{
				if(document.Form1.uwgSBD_HT__ctl0_ddlMon.options[i].value == row.getCellFromKey("Month").getValue())
				{
					
					document.Form1.uwgSBD_HT__ctl0_ddlMon.options[i].selected=true
				}
			}
		}
		function uwgSBD_HT_AfterRowTemplateOpenHandler(gridName, rowId){
			//Add code to handle your event here.
			
			var frm = document.Form1;
			var row = igtbl_getRowById(rowId);
			///////////RowIndex Hidden 에 저장////////////////
			var num = rowId.split('_');
			document.Form1.hdRowIndex.value = num[1];
			/////////////////////////////////////////////////
			
			
			//입출고일자
			var date1 = igdrp_getComboById("uwgSBDxHTxxctl0xwdcInDate");
			if(row.getCellFromKey("DeliveryDate").getValue() !=null)
			{
					date1.setValue(row.getCellFromKey("DeliveryDate").getValue());
			}
			document.Form1.hdOldMonth.value = date1.getValue().getUTCMonth()+1;
			document.Form1.hdOldYear.value = date1.getValue().getFullYear();
			document.Form1.hdquantity.value = row.getCellFromKey("DeliveryQuantity").getValue();
			document.Form1.hdOldCost.value = row.getCellFromKey("TotalCost").getValue();
			document.Form1.hdyear.value = row.getCellFromKey("Year").getValue();
			document.Form1.hdmon.value = row.getCellFromKey("Month").getValue();
		}
		function uwgSBD_HT_AfterRowTemplateCloseHandler(gridName, rowId, bSaveChanges){
			var row = igtbl_getRowById(rowId);
			var date1 = igdrp_getComboById("uwgSBDxHTxxctl0xwdcInDate");			
			row.getCellFromKey("DeliveryDate").setValue(date1.getValue());
			document.Form1.hdReason.value = document.Form1.uwgSBD_HT__ctl0_txtReason.value;
			
			row.getCellFromKey("Year").setValue(document.Form1.uwgSBD_HT__ctl0_ddlYear.value);
			row.getCellFromKey("Month").setValue(document.Form1.uwgSBD_HT__ctl0_ddlMon.value);			
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
									<TD align="left" width="800" colSpan="9" height="30">
										<table id="table4" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
											<tr>
												<td style="HEIGHT: 24px" width="200"><uc1:companysearchcontrol id="CSC1" runat="server"></uc1:companysearchcontrol></td>
												<td style="HEIGHT: 24px" width="600"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></td>
											</tr>
										</table>
									</TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30">입고일자&nbsp;</TD>
									<TD align="left" width="100" height="30"><igsch:webdatechooser id="wdcStartDate" runat="server" BorderColor="DimGray" Height="20px" BorderStyle="Solid"
											BackColor="#EEEEE9" Width="100px" Text=" " NullDateLabel=" ">
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
									<TD width="100" height="30"><igsch:webdatechooser id="wdcEndDate" runat="server" BorderColor="DimGray" Height="20px" BorderStyle="Solid"
											BackColor="#EEEEE9" Width="100px" Text=" " NullDateLabel=" ">
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
									<td align="left" width="100"><asp:dropdownlist id="ddlItemClassification1" runat="server" BackColor="#EEEEE9" Width="115px"></asp:dropdownlist></td>
									<td align="right" width="70">자산분류&nbsp;</td>
									<td align="left" width="85">
										<asp:dropdownlist id="ddlProperty" runat="server" Width="80px" BackColor="#EEEEE9" Height="20px">
											<asp:ListItem Value="">-전체-</asp:ListItem>
											<asp:ListItem Value="부자재">부자재</asp:ListItem>
											<asp:ListItem Value="소모품">소모품</asp:ListItem>
										</asp:dropdownlist></td>
									<TD align="right" width="200" height="30"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
											type="button" value="초기화" name="btnReset">&nbsp;
										<asp:button id="btnSearch" runat="server" Height="20px" Width="65px" Text="검   색" Font-Size="9pt"></asp:button>&nbsp;
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
									<TD vAlign="middle" align="center" height="400"><igtbl:ultrawebgrid id="uwgSBD_HT" runat="server" Height="407px" Width="800px" UseAccessibleHeader="True">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="uwgSBDxHT" TableLayout="Fixed" CellClickActionDefault="RowSelect"
												NoDataMessage="해당 데이타가 없습니다." AllowUpdateDefault="RowTemplateOnly">
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
												<ClientSideEvents ColumnHeaderClickHandler="ColumnHeaderClickHandler" BeforeRowTemplateOpenHandler="uwgSBD_HT_BeforeRowTemplateOpenHandler"
													AfterRowTemplateOpenHandler="uwgSBD_HT_AfterRowTemplateOpenHandler" AfterRowTemplateCloseHandler="uwgSBD_HT_AfterRowTemplateCloseHandler"></ClientSideEvents>
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
														<igtbl:UltraGridColumn HeaderText="선택" Key="chk" Width="30px" Type="CheckBox" HeaderClickAction="Select"
															BaseColumnName="" AllowUpdate="Yes">
															<CellStyle HorizontalAlign="Center"></CellStyle>
															<HeaderStyle Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="분류" Key="PropertyClassification" Width="60px" HeaderClickAction="SortMulti"
															BaseColumnName="PropertyClassification">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" BaseColumnName="ItemDrawNum"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단위" Key="Unit" Width="35px" BaseColumnName="Unit">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="규 격" Key="Standard" BaseColumnName="Standard"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="회사이름" Key="CompanyName" HeaderClickAction="SortMulti" BaseColumnName="CompanyName">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="BusinessRegistrationNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="입고수량" Key="DeliveryQuantity" Format="###,###,##0.00" HeaderClickAction="SortMulti"
															BaseColumnName="DeliveryQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="입고일자" Key="DeliveryDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="DeliveryDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Format="###,###,##0.00" HeaderClickAction="SortMulti"
															BaseColumnName="ApplyUnitCost">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총금액" Key="TotalCost" Format="###,###,##0.00" HeaderClickAction="SortMulti"
															BaseColumnName="TotalCost">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ProgressCondition">
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
														<igtbl:UltraGridColumn HeaderText="부자재발주번호" Key="SubBuyingOrderHistoryIndex" HeaderClickAction="SortMulti"
															BaseColumnName="SubBuyingOrderHistoryIndex">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부자재입고번호" Key="SubBuyingDeliveryHistoryIndex" HeaderClickAction="SortMulti"
															BaseColumnName="SubBuyingDeliveryHistoryIndex">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="검사여부" Key="CheckDistinction" Hidden="True" BaseColumnName="CheckDistinction"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="매입년도" Key="Year" Width="60px" BaseColumnName="Year"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="매입월" Key="Month" Width="50px" BaseColumnName="Month"></igtbl:UltraGridColumn>
													</Columns>
													<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
														<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
													</RowTemplateStyle>
													<RowEditTemplate>
														<FONT face="굴림">
															<P align="center"><FONT face="굴림">
																	<TABLE id="Table2" style="WIDTH: 700px" cellSpacing="1" cellPadding="1" width="700" border="0">
																		<TR>
																			<TD align="right"><FONT face="굴림">품목번호:</FONT></TD>
																			<TD><FONT face="굴림">
																					<asp:TextBox id="txtItemNum" tabIndex="1" runat="server" Width="100px" columnKey="ItemNum" Enabled="False"></asp:TextBox></FONT></TD>
																			<TD align="right"><FONT face="굴림">도면번호:</FONT></TD>
																			<TD><FONT face="굴림">
																					<asp:TextBox id="txtItemDrawNum" runat="server" Width="100px" columnKey="ItemDrawNum" Enabled="False"></asp:TextBox></FONT></TD>
																			<TD align="right"><FONT face="굴림">품목명:</FONT></TD>
																			<TD>
																				<asp:TextBox id="txtItemName" runat="server" Width="100px" columnKey="ItemName" Enabled="False"></asp:TextBox></TD>
																			<TD align="right">입고일자:&nbsp;
																			</TD>
																			<TD>
																				<igsch:webdatechooser id="wdcInDate" runat="server" NullDateLabel=" " Text=" " Width="100px" BackColor="#EEEEE9"
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
																		</TR>
																		<TR>
																			<TD align="right"><FONT face="굴림">납품량:</FONT></TD>
																			<TD>
																				<asp:TextBox id="txtDeliveryQuantity" style="TEXT-ALIGN: right" runat="server" Width="100px"
																					columnKey="DeliveryQuantity"></asp:TextBox></TD>
																			<TD align="right">단&nbsp; 가&nbsp;:</TD>
																			<TD><FONT face="굴림">
																					<asp:TextBox id="txtCost" style="TEXT-ALIGN: right" runat="server" Width="100px" columnKey="ApplyUnitCost"></asp:TextBox></FONT></TD>
																			<TD align="right"><FONT face="굴림">사유:</FONT></TD>
																			<TD><INPUT id="txtReason" style="WIDTH: 100px" type="text" name="txtReason" runat="server"></TD>
																			<TD align="right">Lot 번호 :&nbsp;
																			</TD>
																			<TD>
																				<asp:TextBox id="txtLot" runat="server" Width="100px" columnKey="LotNum"></asp:TextBox></TD>
																		</TR>
																		<TR>
																			<TD align="right"><FONT face="굴림">매입년도:</FONT></TD>
																			<TD>
																				<asp:DropDownList id="ddlYear" runat="server" columnKey="[Year]">
																					<asp:ListItem Value="2020">2020</asp:ListItem>
												<asp:ListItem Value="2021">2021</asp:ListItem>
												<asp:ListItem Value="2022">2022</asp:ListItem>
												<asp:ListItem Value="2023>2023</asp:ListItem>
												<asp:ListItem Value="2024">2024</asp:ListItem>
												<asp:ListItem Value="2025">2025</asp:ListItem>
												<asp:ListItem Value="2026">2026</asp:ListItem>
												<asp:ListItem Value="2027">2027</asp:ListItem>
												<asp:ListItem Value="2028">2028</asp:ListItem>
												<asp:ListItem Value="2029">2029</asp:ListItem>
												<asp:ListItem Value="2030">2030</asp:ListItem>
																					
																				</asp:DropDownList></TD>
																			<TD align="right">매&nbsp;입&nbsp;월&nbsp;:</TD>
																			<TD>
																				<asp:DropDownList id="ddlMon" runat="server" columnKey="[Month]">
																					<asp:ListItem Value="1">1월</asp:ListItem>
																					<asp:ListItem Value="2">2월</asp:ListItem>
																					<asp:ListItem Value="3">3월</asp:ListItem>
																					<asp:ListItem Value="4">4월</asp:ListItem>
																					<asp:ListItem Value="5">5월</asp:ListItem>
																					<asp:ListItem Value="6">6월</asp:ListItem>
																					<asp:ListItem Value="7">7월</asp:ListItem>
																					<asp:ListItem Value="8">8월</asp:ListItem>
																					<asp:ListItem Value="9">9월</asp:ListItem>
																					<asp:ListItem Value="10">10월</asp:ListItem>
																					<asp:ListItem Value="11">11월</asp:ListItem>
																					<asp:ListItem Value="12">12월</asp:ListItem>
																				</asp:DropDownList></TD>
																			<TD colSpan="4"></TD>
																		</TR>
																		<TR height="200">
																			<TD colSpan="8"></TD>
																		</TR>
																		<TR>
																			<TD align="center" colSpan="8"><INPUT id="igtbl_reOkBtn" style="WIDTH: 60px" onclick="igtbl_gRowEditButtonClick(event);__doPostBack('linkUpdate','');"
																					type="button" value="수   정">&nbsp;</TD>
																		</TR>
																	</TABLE>
																</FONT>
															</P>
														</FONT>
													</RowEditTemplate>
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
								<TD style="WIDTH: 100px" align="left" height="10"></TD>
								<TD style="WIDTH: 514px" align="right" height="10"></TD>
								<TD style="WIDTH: 60px" align="right" height="10"></TD>
								<TD style="WIDTH: 203px" align="right" height="10"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" align="left"><asp:button id="btnExcel" runat="server" Height="20px" Width="65px" Text="Excel" Font-Size="9pt"></asp:button></TD>
								<TD align="right"><INPUT id="hdmon" style="WIDTH: 21px; HEIGHT: 21px" type="hidden" size="1" name="hdOldMonth"
										runat="server"><INPUT id="hdyear" style="WIDTH: 21px; HEIGHT: 21px" type="hidden" size="1" name="hdOldYear"
										runat="server"><INPUT id="hdOldCost" style="WIDTH: 18px; HEIGHT: 21px" type="hidden" size="1" name="hdOldYear"
										runat="server"><INPUT id="hdReason" style="WIDTH: 18px; HEIGHT: 21px" type="hidden" size="1" name="hdOldYear"
										runat="server"><INPUT id="hdOldYear" style="WIDTH: 22px; HEIGHT: 21px" type="hidden" size="1" name="hdOldYear"
										runat="server"><INPUT id="hdOldMonth" style="WIDTH: 21px; HEIGHT: 21px" type="hidden" size="1" name="hdOldMonth"
										runat="server">&nbsp;<INPUT id="hdquantity" style="WIDTH: 22px; HEIGHT: 21px" type="hidden" size="1" name="Hidden1"
										runat="server"><INPUT id="hdRowIndex" style="WIDTH: 24px; HEIGHT: 21px" type="hidden" size="1" name="Hidden1"
										runat="server">
									<asp:linkbutton id="linkUpdate" runat="server" Visible="False">linkUpdate</asp:linkbutton><asp:literal id="Literal1" runat="server"></asp:literal></TD>
								<TD align="right" colSpan="2"><asp:button id="btnCancel" runat="server" Height="20px" Width="65px" Text="삭   제" Font-Size="9pt"></asp:button>&nbsp;</TD>
							</TR>
						</TABLE>
						<igtblexp:ultrawebgridexcelexporter id="uwgExcel" runat="server"></igtblexp:ultrawebgridexcelexporter></TD>
				</TR>
			</TABLE>
			&nbsp;
			<P align="center"><FONT face="굴림"></FONT>&nbsp;</P>
		</form>
	</body>
</HTML>
