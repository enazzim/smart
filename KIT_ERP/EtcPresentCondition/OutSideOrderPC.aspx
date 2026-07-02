<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtbl1" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="OutSideOrderPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.EtcPresentCondition.OutSideOrderPC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>OutSideOrderPC</title>
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
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" height="550"
				cellSpacing="0" cellPadding="0" width="800" bgColor="#f7f6f6" border="0">
				<TR>
					<TD width="20" height="60"><FONT face="굴림"></FONT></TD>
					<TD height="60">&nbsp;
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 811px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 78px"
							align="top"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색 ]
							</LEGEND>
							<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
								<TR height="35">
									<TD align="left" width="600" colSpan="9" height="30">
										<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="800" border="0">
											<TR>
												<TD align="left" width="200" height="30"><uc1:companysearchcontrol id="CSC1" runat="server"></uc1:companysearchcontrol></TD>
												<TD align="left" width="600" height="30"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30">진행상태&nbsp;</TD>
									<TD width="100" height="30"><asp:dropdownlist id="ddlState" runat="server" Font-Size="10pt" Height="20px" BackColor="#EEEEE9"
											Width="100px">
											<asp:ListItem Value="전체">전체</asp:ListItem>
											<asp:ListItem Value="미납">미납</asp:ListItem>
											<asp:ListItem Value="중단">중단</asp:ListItem>
											<asp:ListItem Value="완료">완료</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD align="right" width="90" height="30">납기요구일&nbsp;</TD>
									<TD align="left" width="100" height="30"><igsch:webdatechooser id="wdcStartDate" runat="server" Height="20px" BackColor="#EEEEE9" Width="100px"
											Text=" " NullDateLabel=" " BorderStyle="Solid" BorderColor="DimGray">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderWidth="1px" BorderStyle="Solid"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD align="center" width="10" height="30">~</TD>
									<TD width="100" height="30"><igsch:webdatechooser id="wdcEndDate" runat="server" Height="20px" BackColor="#EEEEE9" Width="100px" Text=" "
											NullDateLabel=" " BorderStyle="Solid" BorderColor="DimGray">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderWidth="1px" BorderStyle="Solid"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD align="right" width="70">제품팀</TD>
									<TD align="left" width="100"><asp:dropdownlist id="ddlItemClassification1" runat="server" BackColor="#EEEEE9" Width="115px"></asp:dropdownlist></TD>
									<TD align="right" width="160"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
											type="button" value="초기화" name="btnReset">&nbsp;<asp:button id="btnSearch" runat="server" Font-Size="9pt" Height="20px" Width="60px" Text="검   색"
											CommandName="Search"></asp:button>&nbsp;</TD>
								</TR>
								<TR>
									<TD align="right" width="70" colSpan="9" height="8"></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="20" height="390"></TD>
					<TD vAlign="top" height="390">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 390px"
							align="top"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색결과 ]
							</LEGEND>
							<TABLE id="Table3" style="WIDTH: 100%; HEIGHT: 400px" cellSpacing="0" cellPadding="0">
								<TR>
									<TD vAlign="middle" align="right" width="875"><INPUT id="Volum" style="WIDTH: 26px; HEIGHT: 12px" type="hidden" size="1" value="0" name="Hidden1"
											runat="server"><asp:button id="btnPre" runat="server" Height="16px" Width="16px" Text="◀" CommandName="Video"
											CommandArgument="Pre"></asp:button><asp:button id="btnNow" runat="server" Height="16px" Width="16px" Text="■" CommandName="Video"
											CommandArgument="Now"></asp:button><asp:button id="btnNext" runat="server" Height="16px" Width="16px" Text="▶" CommandName="Video"
											CommandArgument="Next"></asp:button>&nbsp;&nbsp;&nbsp;</TD>
								</TR>
								<TR>
									<TD vAlign="top" align="center" width="875" height="390"><igtbl1:ultrawebgrid id="uwgOO_HT" runat="server" Height="385px" Width="800px">
											<DisplayLayout AutoGenerateColumns="False" AllowSortingDefault="Yes" RowHeightDefault="20px" Version="3.00"
												ViewType="Hierarchical" SelectTypeRowDefault="Extended" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="uwgOOxHT" CellClickActionDefault="RowSelect" NoDataMessage="해당품목이 없습니다."
												AllowUpdateDefault="RowTemplateOnly">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</AddNewBox>
												<Pager QuickPages="5" PageSize="16" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray" Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="385px"></FrameStyle>
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
												<igtbl1:UltraGridBand>
													<Columns>
														<igtbl1:UltraGridColumn HeaderText="거래처명" Key="CompanyName" BaseColumnName="CompanyName">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="120px" BaseColumnName="ItemNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
															<HeaderStyle Height="25px"></HeaderStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" BaseColumnName="ItemDrawNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="품목명" Key="ItemName" BaseColumnName="ItemName">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" Width="80px" BaseColumnName="ProgressCondition">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="시작공정코드" Key="BeginProcessCode" Hidden="True" BaseColumnName="BeginProcessCode">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="시작공정" Key="BeginProcess" BaseColumnName="BeginProcess">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="종료공정코드" Key="EndProcessCode" Hidden="True" BaseColumnName="EndProcessCode">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="종료공정" Key="EndProcess" BaseColumnName="EndProcess">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" BaseColumnName="BusinessRegistrationNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="발주비율" Key="OrderRate" Hidden="True" Format="###,###,##0.00" BaseColumnName="OrderRate">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="총발주량" Key="OrderQuantity" Format="###,###,##0.00" BaseColumnName="OrderQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="납기요구일" Key="FirstDeliveryDemandDate" Format="yyyy-MM-dd" BaseColumnName="FirstDeliveryDemandDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="단가구분" Key="UnitCostDistinction" Hidden="True" BaseColumnName="UnitCostDistinction">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Format="###,###,##0.00" BaseColumnName="ApplyUnitCost">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="총금액" Key="TotalCost" Format="###,###,##0.00" BaseColumnName="TotalCost">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="잔량" Key="RemainQuantity" Format="###,###,##0.00" BaseColumnName="RemainQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="볼륨번호" Key="VolumNum" BaseColumnName="VolumNum">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" BaseColumnName="RegistrationPerson">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" BaseColumnName="UpdatingPerson">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="외주의뢰원장번호" Key="OutSideOrderRequestHistoryIndex" Hidden="True" BaseColumnName="OutSideOrderRequestHistoryIndex">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="외주발주원장번호" Key="OutSideOrderHistoryIndex" Hidden="True" BaseColumnName="OutSideOrderHistoryIndex">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl1:UltraGridColumn>
													</Columns>
												</igtbl1:UltraGridBand>
											</Bands>
										</igtbl1:ultrawebgrid></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="20"></TD>
					<TD>
						<table id="Table4" style="WIDTH: 100%; HEIGHT: 35px" height="8" cellSpacing="0" cellPadding="0">
							<TR>
								<TD style="WIDTH: 500px" align="left"><asp:button id="btnExcel" runat="server" Font-Size="10pt" Height="20px" Width="65px" Text="Excel"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								<TD style="WIDTH: 244px" align="right">&nbsp;</TD>
								<TD style="WIDTH: 28px" align="right"></TD>
								<TD align="right">
									<P>&nbsp;</P>
								</TD>
							</TR>
						</table>
						<igtblexp:ultrawebgridexcelexporter id="uwgExcel" runat="server"></igtblexp:ultrawebgridexcelexporter></TD>
				</TR>
			</TABLE>
			</TD></TR></TABLE></form>
	</body>
</HTML>
