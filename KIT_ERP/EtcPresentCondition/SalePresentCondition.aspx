<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Page language="c#" Codebehind="SalePresentCondition.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.EtcPresentCondition.SalePresentCondition" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SalePresentCondition</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		function ResettxtBox()
		{
			ResetTextBox();
			ResetBox();
			var objChooser1 = igdrp_getComboById("wdcFromDate");
			var objChooser2 = igdrp_getComboById("wdcToDate");
						
			objChooser1.setValue(null);
			objChooser2.setValue(null);
		}	
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; POSITION: absolute; TOP: 8px; LEFT: 8px" cellSpacing="0"
				cellPadding="0" width="800" border="0">
				<TR>
					<TD style="HEIGHT: 103px; PADDING-TOP: 10px" vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-BOTTOM: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-TOP: #696969 2px solid; BORDER-RIGHT: #696969 2px solid"><LEGEND align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD style="HEIGHT: 30px" align="left" width="170" colSpan="2" height="30"><FONT face="굴림"><uc1:companysearchcontrol id="CompanySearchControl1" runat="server"></uc1:companysearchcontrol></FONT></TD>
									<TD style="HEIGHT: 30px" vAlign="middle" align="left" width="630" colSpan="3" height="30"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30"><FONT face="굴림">매출일자&nbsp;</FONT></TD>
									<TD width="100" height="30"><igsch:webdatechooser id="wdcFromDate" runat="server" Font-Size="9pt" BorderColor="DimGray" Height="20px"
											Width="100px" BackColor="#EEEEE9" BorderStyle="Solid" Text=" " NullDateLabel=" " MaxDate="2076-06-06">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												MaxDate="2076-06-06" ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD align="center" width="15" height="30"><FONT face="굴림">~</FONT></TD>
									<TD width="100" height="30"><igsch:webdatechooser id="wdcToDate" runat="server" Font-Size="9pt" BorderColor="DimGray" Height="20px"
											Width="100px" BackColor="#EEEEE9" BorderStyle="Solid" Text=" " NullDateLabel=" " MaxDate="2076-06-06">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												MaxDate="2076-06-06" ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD align="right" width="515" height="30"><FONT face="굴림"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
												type="button" value="초기화" name="btnReset">
											<asp:button id="btComSearch" runat="server" Height="20px" Width="60px" Text="검  색"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
										</FONT>
									</TD>
								</TR>
							</TABLE>
							<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="800">
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-BOTTOM: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-TOP: #696969 2px solid; BORDER-RIGHT: #696969 2px solid"><LEGEND align="top">[검색결과]</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800">
								<TR>
									<TD colSpan="6" height="406"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="406px" Width="100%">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect"
												NoDataMessage="검색결과가 없습니다!">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</AddNewBox>
												<Pager PageSize="17" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
												<FrameStyle Width="100%" BorderWidth="1px" Font-Size="9pt" Font-Names="Verdana" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="406px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BackColor="#EBEFF6"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand AllowUpdate="RowTemplateOnly">
													<Columns>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="120px" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<HeaderStyle Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Width="120px" HeaderClickAction="SortMulti"
															BaseColumnName="ItemDrawNum">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="120px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" Width="150px" HeaderClickAction="SortMulti"
															BaseColumnName="CompanyName">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="BusinessRegistrationNum">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총매출량" Key="Quantity" Width="150px" Format="###,###,###" HeaderClickAction="SortMulti"
															BaseColumnName="Quantity">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총금액" Key="Cost" Width="140px" Format="###,###,###" HeaderClickAction="SortMulti"
															BaseColumnName="Cost">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
													</Columns>
													<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
														<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
													</RowTemplateStyle>
													<RowEditTemplate>
														<TABLE style="WIDTH: 800px; HEIGHT: 58px">
															<TR>
																<TD align="right" aline="right"><FONT face="굴림">품목명</FONT></TD>
																<TD aline="left"><FONT face="굴림"><INPUT id="igtbl_TextBox_0_3" style="WIDTH: 100px" readOnly type="text" columnKey="ItemName"></FONT></TD>
																<TD aline="right"><FONT face="굴림">거래처</FONT></TD>
																<TD aline="left"><FONT face="굴림"><INPUT id="igtbl_TextBox_0_4" style="WIDTH: 100px" readOnly type="text" columnKey="CompanyName"></FONT></TD>
																<TD align="right" aline="right"><FONT face="굴림">출고량</FONT></TD>
																<TD aline="left"><INPUT id="igtbl_TextBox_0_6" style="WIDTH: 100px; TEXT-ALIGN: right" readOnly type="text"
																		columnKey="OutStorehouseQuantity"></TD>
																<TD align="right" aline="right"><FONT face="굴림">적합량</FONT></TD>
																<TD aline="left"><INPUT id="igtbl_TextBox_0_7" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																		onkeyup="return Process();" style="WIDTH: 100px; TEXT-ALIGN: right" onfocus="OnFocus_Obj(this);"
																		type="text" value="0" columnKey="SuitabilityQuantity"></TD>
																<TD align="right" aline="right"><FONT face="굴림">단가</FONT></TD>
																<TD aline="left"><INPUT id="igtbl_TextBox_0_8" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																		onkeyup="return Process();" style="WIDTH: 100px; TEXT-ALIGN: right" type="text" value="0" columnKey="ApplyUnitCost"></TD>
															</TR>
															<TR>
																<TD align="right" aline="right"><FONT face="굴림">발행일</FONT></TD>
																<TD aline="left">
																	<igsch:WebDateChooser id="WebDateChooser1" runat="server" Width="101px" Text="Null" NullDateLabel=" ">
																		<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
																			DayNameFormat="FirstLetter">
																			<SELECTEDDAYSTYLE ForeColor="White" BackColor="#0A246A"></SELECTEDDAYSTYLE>
																			<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																			<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																			<DAYHEADERSTYLE ForeColor="#D4D0C8" BackColor="Gray"></DAYHEADERSTYLE>
																		</CALENDARLAYOUT>
																		<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
																		<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
																		<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
																	</igsch:WebDateChooser></TD>
																<TD align="right" aline="right"><FONT face="굴림">매출일</FONT></TD>
																<TD aline="left">
																	<igsch:WebDateChooser id="WebDateChooser2" runat="server" Width="101px" Text="Null" NullDateLabel=" ">
																		<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
																			DayNameFormat="FirstLetter">
																			<SELECTEDDAYSTYLE ForeColor="White" BackColor="#0A246A"></SELECTEDDAYSTYLE>
																			<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																			<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																			<DAYHEADERSTYLE ForeColor="#D4D0C8" BackColor="Gray"></DAYHEADERSTYLE>
																		</CALENDARLAYOUT>
																		<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
																		<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
																		<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
																	</igsch:WebDateChooser></TD>
																<TD aline="right"><FONT face="굴림"></FONT></TD>
																<TD aline="left"></TD>
																<TD align="right" aline="right"><FONT face="굴림">총금액</FONT></TD>
																<TD aline="left"><INPUT id="igtbl_TextBox_0_9" style="WIDTH: 100px; TEXT-ALIGN: right" type="text" value="0"
																		columnKey="TotalCost"></TD>
																<TD align="right" aline="right"><FONT face="굴림">부가세</FONT></TD>
																<TD aline="left"><INPUT id="igtbl_TextBox_0_10" style="WIDTH: 100px; TEXT-ALIGN: right" type="text" value="10"
																		columnKey="SupplementaryValueTaxRate"></TD>
															</TR>
														</TABLE>
														<P align="center">&nbsp;</P>
														<P align="center"><FONT face="굴림"></FONT>&nbsp;</P>
														<P align="center">&nbsp;</P>
														<P align="center"><INPUT id="igtbl_reOkBtn" style="WIDTH: 50px; HEIGHT: 22px" onclick="igtbl_gRowEditButtonClick(event);__doPostBack('lnk_Update','');"
																type="button" value="수정">&nbsp; <INPUT id="igtbl_reCancelBtn" style="WIDTH: 50px; HEIGHT: 22px" onclick="igtbl_gRowEditButtonClick(event);"
																type="button" value="취소"></P>
													</RowEditTemplate>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 265px" align="left" colSpan="2" height="30"><FONT face="굴림">&nbsp;</FONT>
										<asp:button id="Button1" runat="server" Font-Size="10pt" Height="20px" Width="60px" Text="Excel"></asp:button></TD>
									<TD align="left" colSpan="4" height="30">&nbsp;&nbsp;
									</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			<igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter></form>
	</body>
</HTML>
