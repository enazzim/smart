<%@ Page language="c#" Codebehind="EtcBuyingDeliveryPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.EtcPresentCondition.EtcBuyingDeliveryPC" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v1.1, Version=1.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>EtcBuyingDeliveryPC</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
		<script language="javascript">
<!--
		// 초기화 버튼 핸들러
		function Reset_TextBox()
		{
			ResetBox();
			var frm = document.Form1;
			frm.txtItemName.value = "";
			var objDChooser = igdrp_getComboById("wcDeliveryDate");
			objDChooser.setValue(null);		
			var objDChooser1 = igdrp_getComboById("wcDeliveryDate1");
			objDChooser1.setValue(null);	
		}
					
//-->
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="LEFT: 10px; POSITION: absolute; TOP: 10px" cellSpacing="0" cellPadding="0"
				width="800" border="0">
				<TR>
					<TD width="20" height="80"></TD>
					<TD vAlign="top" height="80">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid"
							align="middle">
							<LEGEND style="FONT-SIZE: 9pt" align="left">
								[ 검색&nbsp;]
							</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
								<TR>
									<TD align="right" width="50" height="30">품목명&nbsp;
									</TD>
									<TD width="100" height="30"><asp:textbox id="txtItemName" runat="server" BorderColor="DimGray" Height="20px" BackColor="#EEEEE9"
											BorderWidth="1px" BorderStyle="Solid" Width="100px"></asp:textbox></TD>
									<TD align="left" width="210" height="30" colSpan="2">
										<uc1:CompanySearchControl id="CSC1" runat="server"></uc1:CompanySearchControl>
									</TD>
									<TD align="right" width="50" height="30">납품일&nbsp;
									</TD>
									<TD align="left" width="100" height="30"><igsch:webdatechooser id="wcDeliveryDate" runat="server" BorderColor="DimGray" Height="20px" BackColor="#EEEEE9"
											BorderStyle="Solid" Width="100px" NullDateLabel=" " Text=" ">
											<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
												DayNameFormat="FirstLetter">
												<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
												<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
												<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
												<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
											</CALENDARLAYOUT>
											<DROPDOWNSTYLE BorderStyle="Solid" BorderWidth="1px" BorderColor="DimGray"></DROPDOWNSTYLE>
											<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
											<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
										</igsch:webdatechooser></TD>
									<TD align="center" width="15" height="30">~</TD>
									<TD align="right" width="80" height="30"><igsch:webdatechooser id="wcDeliveryDate1" runat="server" BorderColor="DimGray" Height="20px" BackColor="#EEEEE9"
											BorderStyle="Solid" Width="100px" NullDateLabel=" " Text=" ">
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
									<TD align="right" height="30"><INPUT id="btnInit" style="HEIGHT: 20px" onclick="Reset_TextBox();" type="button" value="초기화"
											name="btnInit">&nbsp;
										<asp:button id="btnSearch" runat="server" Height="20px" Width="65px" Text="검 색" Font-Size="9pt"></asp:button>&nbsp;
									</TD>
								</TR>
								<TR>
									<TD align="right" width="70" colSpan="9" height="8"></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="20" height="400">
						<P>&nbsp;</P>
					</TD>
					<TD vAlign="top" align="center">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-WEIGHT: normal; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 100%"
							align="middle">
							<LEGEND style="FONT-SIZE: 10pt" align="left">
								[&nbsp;검색결과 ]
							</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" align="center">
								<TR height="35">
									<TD vAlign="top" align="center" colSpan="2"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="385px" Width="800px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect"
												AllowUpdateDefault="Yes">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</AddNewBox>
												<Pager PageSize="16" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray" Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="385px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents BeforeRowTemplateCloseHandler="UltraWebGrid1_BeforeRowTemplateCloseHandler" BeforeRowTemplateOpenHandler="UltraWebGrid1_BeforeRowTemplateOpenHandler"
													AfterRowTemplateOpenHandler="UltraWebGrid1_AfterRowTemplateOpenHandler"></ClientSideEvents>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedRowStyleDefault ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand AllowUpdate="RowTemplateOnly">
													<Columns>
														<igtbl:UltraGridColumn HeaderText="선택" Key="chk" Width="40px" Type="CheckBox" HeaderClickAction="Select"
															BaseColumnName="" AllowUpdate="Yes">
															<CellStyle HorizontalAlign="Center"></CellStyle>
															<HeaderStyle Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="130px" BaseColumnName="ItemName"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" Width="120px" BaseColumnName="CompanyName"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래처등록번호" Key="BusinessRegistrationNum" Hidden="True" BaseColumnName="BusinessRegistrationNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="발주수량" Key="OrderQuantity" Width="80px" Format="###,###,##0" BaseColumnName="OrderQuantity">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="납품수량" Key="DeliveryQuantity" BaseColumnName="DeliveryQuantity">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="개별단가" Key="ApplyUnitCost" Width="120px" Format="###,###,##0" BaseColumnName="ApplyUnitCost">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총금액" Key="TotalCost" Width="120px" Format="###,###,##0" BaseColumnName="TotalCost">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="납품일" Key="DeliveryDemandDate" Format="yyyy-MM-dd" BaseColumnName="DeliveryDemandDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자 ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" BaseColumnName="RegistrationPerson"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자 ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" BaseColumnName="UpdatingPerson"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="납품원장번호" Key="EtcBuyingDeliveryHistoryIndex" Hidden="True" BaseColumnName="EtcBuyingDeliveryHistoryIndex"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="발주원장번호" Key="EtcBuyingOrderHistoryIndex" Hidden="True" BaseColumnName="EtcBuyingOrderHistoryIndex"></igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="left" height="30" width="640">&nbsp;
										<asp:button id="btnExcel" runat="server" Height="20px" Width="65px" Text="Excel"></asp:button><igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter></TD>
									<TD vAlign="middle" align="right" height="30">&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
