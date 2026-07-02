<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="OutSideDeliveryPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BuyingOutside.OutSideDeliveryPC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../MessageWindows.js"></script>
		<script language="javascript" src="../GridCheck.js"></script>
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
			document.Form1.ddlState.options[0].selected=true;
			document.Form1.ddlItemClassification1.options[0].selected=true;
					
		}
		
		//Row Template 창이 열리기 전에 진행상태가 대기가 아닌것은 수정을 할 수 없도록 경고 메세지를 띄운다.
		function uwgOSD_HT_BeforeRowTemplateOpenHandler(gridName, rowId, templateId){
			
			var frm = document.Form1;
			var row = igtbl_getRowById(rowId);
			if(row.getCellFromKey("ProgressCondition").getValue() == "대기")
			{
				//수정하기 이전의 납품수량
				frm.hdquantity.value = row.getCellFromKey("DeliveryQuantity").getValue();
				
				//매입년도선택
				for(var i=0;i<document.Form1.uwgOSD_HT__ctl0_ddlYear.options.length;i++)
				{
					if(document.Form1.uwgOSD_HT__ctl0_ddlYear.options[i].value == row.getCellFromKey("Year").getValue())
					{
						document.Form1.uwgOSD_HT__ctl0_ddlYear.options[i].selected=true
					}
				}
				//매입월 선택
				for(var i=0;i<document.Form1.uwgOSD_HT__ctl0_ddlMon.options.length;i++)
				{
					if(document.Form1.uwgOSD_HT__ctl0_ddlMon.options[i].value == row.getCellFromKey("Month").getValue())
					{
						
						document.Form1.uwgOSD_HT__ctl0_ddlMon.options[i].selected=true
					}
				}
				
			}
			else
			{
				alert("진행상태가 완료인 항목은 수정할 수 없습니다.!");
				return true;
			}
		}
		
		
		//Row Template 창이 열릴때 해야할 일을 Client Side Event에서 작성
		function uwgOSD_HT_AfterRowTemplateOpenHandler(gridName, rowId){
			
			var frm = document.Form1;
			var row = igtbl_getRowById(rowId);
			
			///////////RowIndex Hidden 에 저장////////////////
			var num = rowId.split('_');
			frm.hdRowIndex.value = num[1];
			/////////////////////////////////////////////////
			
			//입출고일자
			var date1 = igdrp_getComboById("uwgOSDxHTxxctl0xwdcDeliveryDate");
			if(row.getCellFromKey("DeliveryDate").getValue() !=null)
			{
					date1.setValue(row.getCellFromKey("DeliveryDate").getValue());
			}
			
			document.Form1.hdMonth.value = date1.getValue().getUTCMonth()+1;
			document.Form1.hdYear.value = date1.getValue().getFullYear();
			document.Form1.hdApplyCost.value = row.getCellFromKey("ApplyUnitCost").getValue();
			
			document.Form1.hd_year.value = row.getCellFromKey("Year").getValue();
			document.Form1.hdmon.value = row.getCellFromKey("Month").getValue();
			
		}
		function uwgOSD_HT_BeforeRowTemplateCloseHandler(gridName, rowId, bSaveChanges){
			//Add code to handle your event here.
			var row = igtbl_getRowById(rowId);
			var date1 = igdrp_getComboById("uwgOSDxHTxxctl0xwdcDeliveryDate");			
			row.getCellFromKey("DeliveryDate").setValue(date1.getValue());
			document.Form1.hdReason.value = document.Form1.uwgOSD_HT__ctl0_txtReason.value;			
			
			row.getCellFromKey("Year").setValue(document.Form1.uwgOSD_HT__ctl0_ddlYear.value);
			row.getCellFromKey("Month").setValue(document.Form1.uwgOSD_HT__ctl0_ddlMon.value);	
		}
--></SCRIPT>
	</HEAD>
	<body bottomMargin="0" bgColor="#f7f6f6" leftMargin="0" topMargin="0" rightMargin="0"
		MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; POSITION: absolute; WIDTH: 800px; HEIGHT: 550px; TOP: 10px; LEFT: 10px"
				cellSpacing="0" cellPadding="0" width="300" border="0">
				<TR>
					<TD width="20" height="60"><FONT face="굴림"></FONT></TD>
					<TD height="60">&nbsp;
						<FIELDSET style="BORDER-BOTTOM: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; HEIGHT: 20px; BORDER-TOP: dimgray 2px solid; BORDER-RIGHT: dimgray 2px solid"
							align="middle"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색 ]
							</LEGEND>
							<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
								<TR height="35">
									<TD align="left" width="600" colSpan="9" height="30">
										<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="800" border="0">
											<TR>
												<TD align="left" width="200" height="30">
													<uc1:companysearchcontrol id="CSC1" runat="server"></uc1:companysearchcontrol></TD>
												<TD align="left" width="600" height="30">
													<uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol><INPUT id="hdYear" style="WIDTH: 48px; HEIGHT: 21px" type="hidden" size="2" name="Hidden1"
														runat="server"><INPUT id="hdMonth" style="WIDTH: 48px; HEIGHT: 21px" type="hidden" size="2" name="Hidden1"
														runat="server"><INPUT id="hdApplyCost" style="WIDTH: 48px; HEIGHT: 21px" type="hidden" size="2" name="Hidden1"
														runat="server"></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30">진행상태</TD>
									<TD width="100" height="30">
										<asp:dropdownlist id="ddlState" runat="server" Height="20px" Width="100px" BackColor="#EEEEE9" Font-Size="10pt">
											<asp:ListItem Value="전체">전체</asp:ListItem>
											<asp:ListItem Value="완료">완료</asp:ListItem>
											<asp:ListItem Value="미납">미납</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD align="right" width="70" height="30">입고일자&nbsp;</TD>
									<TD align="left" width="100" height="30"><igsch:webdatechooser id="wdcStartDate" runat="server" BackColor="#EEEEE9" BorderStyle="Solid" Width="100px"
											Height="20px" BorderColor="DimGray" Text=" " NullDateLabel=" ">
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
									<TD width="100" height="30"><igsch:webdatechooser id="wdcEndDate" runat="server" BackColor="#EEEEE9" BorderStyle="Solid" Width="100px"
											Height="20px" BorderColor="DimGray" Text=" " NullDateLabel=" ">
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
									<td width="70" align="right">제품팀</td>
									<td width="100" align="left">
										<asp:dropdownlist id="ddlItemClassification1" runat="server" BackColor="#EEEEE9" Width="115px"></asp:dropdownlist></td>
									<TD align="right" height="30">&nbsp;<INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
											type="button" value="초기화" name="btnReset">
										<asp:button id="btnSearch" runat="server" Width="65px" Height="20px" Text="검   색" Font-Size="9pt"></asp:button>&nbsp;
									</TD>
								</TR>
								<TR>
									<TD align="right" colSpan="9" height="8"></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="20" height="400"></TD>
					<TD height="400">
						<FIELDSET style="BORDER-BOTTOM: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; HEIGHT: 400px; BORDER-TOP: dimgray 2px solid; BORDER-RIGHT: dimgray 2px solid"
							align="top"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색결과 ]
							</LEGEND>
							<TABLE id="Table3" style="WIDTH: 100%; HEIGHT: 400px" cellSpacing="0" cellPadding="0">
								<TBODY>
									<TR>
										<TD vAlign="top" align="left"><igtbl:ultrawebgrid id="uwgOSD_HT" runat="server" Width="100%" Height="407px">
												<DisplayLayout AutoGenerateColumns="False" AllowSortingDefault="Yes" RowHeightDefault="20px" Version="3.00"
													SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti"
													BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="uwgOSDxHT"
													CellClickActionDefault="RowSelect" AllowUpdateDefault="RowTemplateOnly">
													<AddNewBox>
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray"> <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"> </BorderDetails> 
														</Style>
													</AddNewBox>
													<Pager QuickPages="5" PageSize="17" StyleMode="ComboBox" AllowPaging="True">
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray"> <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"> </BorderDetails> 
														</Style>
													</Pager>
													<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray" Height="25px">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</HeaderStyleDefault>
													<FrameStyle Width="100%" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DimGray"
														BorderStyle="Solid" BackColor="Silver" Height="407px"></FrameStyle>
													<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</FooterStyleDefault>
													<ClientSideEvents ColumnHeaderClickHandler="ColumnHeaderClickHandler" BeforeRowTemplateCloseHandler="uwgOSD_HT_BeforeRowTemplateCloseHandler"
														BeforeRowTemplateOpenHandler="uwgOSD_HT_BeforeRowTemplateOpenHandler" AfterRowTemplateOpenHandler="uwgOSD_HT_AfterRowTemplateOpenHandler"></ClientSideEvents>
													<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
													<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
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
															<igtbl:UltraGridColumn HeaderText="선택" Key="chk" Width="40px" Type="CheckBox" HeaderClickAction="Select"
																BaseColumnName="" AllowUpdate="Yes">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" BaseColumnName="ItemNum">
																<CellStyle HorizontalAlign="Left">
																	<Padding Left="2px"></Padding>
																</CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" BaseColumnName="ItemDrawNum">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" BaseColumnName="ItemName">
																<CellStyle HorizontalAlign="Left">
																	<Padding Left="2px"></Padding>
																</CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="단위" Key="Unit" Width="35px" BaseColumnName="Unit">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="공정번호" Key="ProcessSequenceNum" BaseColumnName="ProcessSequenceNum">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="공정코드" Key="ProcessCode" Hidden="True" BaseColumnName="ProcessCode">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="공정명" Key="ProcessName" BaseColumnName="ProcessName">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" BaseColumnName="CompanyName">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" BaseColumnName="BusinessRegistrationNum">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="입고수량" Key="DeliveryQuantity" Format="###,###,##0.00" BaseColumnName="DeliveryQuantity">
																<CellStyle HorizontalAlign="Right">
																	<Padding Right="4px"></Padding>
																</CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="입고일자" Key="DeliveryDate" Format="yyyy-MM-dd" BaseColumnName="DeliveryDate"></igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="Lot번호" Key="LotNum" Hidden="True" BaseColumnName="LotNum">
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Format="###,###,##0.00" BaseColumnName="ApplyUnitCost">
																<CellStyle HorizontalAlign="Right">
																	<Padding Right="4px"></Padding>
																</CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="총금액" Key="TotalCost" Format="###,###,##0.00" BaseColumnName="TotalCost">
																<CellStyle HorizontalAlign="Right">
																	<Padding Right="4px"></Padding>
																</CellStyle>
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
															<igtbl:UltraGridColumn HeaderText="검사자" Key="UpdatingPerson" BaseColumnName="UpdatingPerson">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="검사일" Key="UpdatingDate" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="외주발주원장번호" Key="OutSideOrderHistoryIndex" Hidden="True" BaseColumnName="OutSideOrderHistoryIndex">
																<CellStyle HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="외주납품의뢰원장번호" Key="OutSideDeliveryRequestHistoryIndex" Hidden="True" BaseColumnName="OutSideDeliveryRequestHistoryIndex">
																<CellStyle HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="외주납품원장번호" Key="OutSideDeliveryHistoryIndex" Hidden="True" BaseColumnName="OutSideDeliveryHistoryIndex">
																<CellStyle HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="매입년도" Key="Year" Width="50px" BaseColumnName="Year"></igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="매입월" Key="Month" Width="30px" BaseColumnName="Month"></igtbl:UltraGridColumn>
														</Columns>
														<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
															<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
														</RowTemplateStyle>
														<RowEditTemplate>
															<P align="right">
																<TABLE id="Table2" style="WIDTH: 710px; HEIGHT: 48px" cellSpacing="1" cellPadding="1" width="510"
																	border="0">
																	<TBODY>
																		<TR>
																			<TD align="right"><FONT face="굴림">품목번호:</FONT></TD>
																			<TD><FONT face="굴림"><asp:textbox id="txtItemNum" runat="server" Width="100px" Enabled="False" columnKey="ItemNum"></asp:textbox></FONT></TD>
																			<TD align="right"><FONT face="굴림">도면번호:</FONT></TD>
																			<TD><FONT face="굴림"><asp:textbox id="txtItemDrawNum" runat="server" Width="100px" Enabled="False" columnKey="ItemDrawNum"></asp:textbox></FONT></TD>
																			<TD align="right"><FONT face="굴림">품목명:</FONT></TD>
																			<TD><asp:textbox id="txtItemName" runat="server" Width="100px" Enabled="False" columnKey="ItemName"></asp:textbox></TD>
																			<TD align="right"><FONT face="굴림">납품량:</FONT></TD>
																			<TD><asp:textbox id="txtDeliveryQuantity" style="TEXT-ALIGN: right" runat="server" Width="100" columnKey="DeliveryQuantity"></asp:textbox></TD>
																		</TR>
																		<TR>
																			<TD align="right"><FONT face="굴림">수정사유&nbsp;:</FONT></TD>
																			<TD><FONT face="굴림"><asp:textbox id="txtReason" style="TEXT-ALIGN: right" runat="server" Width="100"></asp:textbox></FONT></TD>
																			<TD align="right"><FONT face="굴림">입고일자&nbsp;:</FONT></TD>
																			<TD><FONT face="굴림"><igsch:webdatechooser id="wdcDeliveryDate" runat="server" BackColor="#EEEEE9" Width="100px" Height="20px"
																						NullDateLabel=" " Text=" " BorderColor="DimGray" BorderStyle="Solid">
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
																					</igsch:webdatechooser></FONT></TD>
																			<TD align="right"><FONT face="굴림">Lot 번호&nbsp;:</FONT></TD>
																			<TD><asp:textbox id="txtLot" style="TEXT-ALIGN: right" runat="server" Width="100" columnKey="LotNum"></asp:textbox></FONT></TD></TD>
										<TD><FONT face="굴림">&nbsp;단가 :</FONT></TD>
										<TD><FONT face="굴림"><asp:textbox id="txtApplyCost" style="TEXT-ALIGN: right" runat="server" Width="100" columnKey="ApplyUnitCost"></asp:textbox></FONT></TD>
									</TR>
									<TR>
										<TD align="right"><FONT face="굴림">매입년도:</FONT></TD>
										<TD><asp:dropdownlist id="ddlYear" runat="server" columnKey="[Year]">
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
											</asp:dropdownlist></TD>
										<TD align="right">매&nbsp;입&nbsp;월&nbsp;:</TD>
										<TD><asp:dropdownlist id="ddlMon" runat="server" columnKey="[Month]">
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
											</asp:dropdownlist></TD>
										<TD colSpan="4"></TD>
									</TR>
									<TR>
										<TD align="center" colSpan="8">
											<P>&nbsp;</P>
											<P>&nbsp;</P>
											<P>&nbsp;</P>
											<P>&nbsp;</P>
											<P>&nbsp;</P>
											<P>&nbsp;</P>
											<P>&nbsp;</P>
											<P><INPUT id="igtbl_reOkBtn" style="WIDTH: 60px" onclick="igtbl_gRowEditButtonClick(event);__doPostBack('linkUpdate','');"
													type="button" value="수   정">&nbsp;&nbsp;
											</P>
										</TD>
									</TR>
								</TBODY>
							</TABLE>
						</P> </RowEditTemplate> </igtbl:UltraGridBand> </Bands> </igtbl:ultrawebgrid></TD>
				</TR>
			</TABLE>
			</FIELDSET> </TD></TR>
			<TR>
				<TD width="20"></TD>
				<TD>
					<table id="Table4" style="WIDTH: 100%; HEIGHT: 30px" height="30" cellSpacing="0" cellPadding="0"
						width="100%">
						<TR>
							<TD align="left" height="5"></TD>
							<TD align="right" height="5"></TD>
							<TD style="WIDTH: 15px" align="right" height="5"></TD>
							<TD align="right" width="200" height="5"></TD>
						</TR>
						<TR>
							<TD align="center" colSpan="4" height="5"><asp:literal id="Literal1" runat="server"></asp:literal></TD>
						</TR>
						<TR>
							<TD align="left"><asp:button id="btnExcel" runat="server" Width="65px" Height="20px" Text="Excel" Font-Size="9pt"></asp:button><igtblexp:ultrawebgridexcelexporter id="uwgExcel" runat="server"></igtblexp:ultrawebgridexcelexporter><INPUT id="hd_year" style="WIDTH: 19px; HEIGHT: 21px" type="hidden" size="1" name="Hidden1"
									runat="server"><INPUT id="hdmon" style="WIDTH: 19px; HEIGHT: 21px" type="hidden" size="1" name="Hidden1"
									runat="server"></TD>
							<TD align="left"><INPUT id="hdquantity" style="WIDTH: 19px; HEIGHT: 21px" type="hidden" size="1" name="Hidden1"
									runat="server"><INPUT id="hdRowIndex" style="WIDTH: 19px; HEIGHT: 21px" type="hidden" size="1" name="Hidden1"
									runat="server">
								<asp:linkbutton id="linkUpdate" runat="server" Visible="False">LinkButton</asp:linkbutton><INPUT id="hdOldMonth" style="WIDTH: 16px; HEIGHT: 21px" type="hidden" size="1" name="Hidden1"
									runat="server"></TD>
							<TD style="WIDTH: 15px" align="right"></TD>
							<TD align="right" width="200"><INPUT id="hdReason" style="WIDTH: 16px; HEIGHT: 21px" type="hidden" size="1" name="Hidden1"
									runat="server"><asp:button id="btnDelete" runat="server" Font-Size="9pt" Width="65px" Height="20px" Text="삭   제"></asp:button>&nbsp;<asp:button id="btnStop" runat="server" Font-Size="9pt" Width="65px" Height="20px" Text="중   단"></asp:button></TD>
						</TR>
					</table>
				</TD>
			</TR>
			</TBODY></TABLE> &nbsp;
		</form>
	</body>
</HTML>
