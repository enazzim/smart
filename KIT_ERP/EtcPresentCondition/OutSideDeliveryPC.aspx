<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="OutSideDeliveryPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.EtcPresentCondition.OutSideDeliveryPC" %>
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
		
--></SCRIPT>
	</HEAD>
	<body bottomMargin="0" bgColor="#f7f6f6" leftMargin="0" topMargin="0" rightMargin="0"
		MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; WIDTH: 800px; POSITION: absolute; TOP: 10px; HEIGHT: 550px"
				cellSpacing="0" cellPadding="0" width="300" border="0">
				<TR>
					<TD width="20" height="60"><FONT face="굴림"></FONT></TD>
					<TD height="60">&nbsp;
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 20px"
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
													<uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></TD>
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
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 400px"
							align="top"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색결과 ]
							</LEGEND>
							<TABLE id="Table3" style="WIDTH: 100%; HEIGHT: 400px" cellSpacing="0" cellPadding="0">
								<TBODY>
									<TR>
										<TD vAlign="top" align="left"><igtbl:ultrawebgrid id="uwgOSD_HT" runat="server" Width="100%" Height="407px">
												<DisplayLayout AutoGenerateColumns="False" AllowSortingDefault="Yes" RowHeightDefault="20px" Version="3.00"
													SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti"
													BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="uwgOSDxHT"
													CellClickActionDefault="RowSelect">
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
													<FrameStyle Width="100%" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DimGray"
														BorderStyle="Solid" BackColor="Silver" Height="407px"></FrameStyle>
													<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</FooterStyleDefault>
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
															<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="120px" BaseColumnName="ItemNum">
																<CellStyle HorizontalAlign="Center"></CellStyle>
																<HeaderStyle Height="25px"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" BaseColumnName="ItemDrawNum">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" BaseColumnName="ItemName">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="순서" Key="ProcessSequenceNum" Width="80px" BaseColumnName="ProcessSequenceNum">
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
															<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" BaseColumnName="UpdatingPerson">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
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
														</Columns>
													</igtbl:UltraGridBand>
												</Bands>
											</igtbl:ultrawebgrid></TD>
									</TR>
								</TBODY>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
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
								<TD align="left"><asp:button id="btnExcel" runat="server" Width="65px" Height="20px" Text="Excel" Font-Size="9pt"></asp:button><igtblexp:ultrawebgridexcelexporter id="uwgExcel" runat="server"></igtblexp:ultrawebgridexcelexporter></TD>
								<TD align="left">&nbsp;</TD>
								<TD style="WIDTH: 15px" align="right"></TD>
								<TD align="right" width="200">&nbsp;</TD>
							</TR>
						</table>
					</TD>
				</TR>
			</TABLE>
			&nbsp;
		</form>
	</body>
</HTML>
