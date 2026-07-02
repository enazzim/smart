<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Page language="c#" Codebehind="ItemGroupInStoreResult.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.EtcPresentCondition.ItemGroupInStoreResult" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ItemGroupInStoreResult</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!--
		function ResettxtBox()
		{
			ResetBox();
			var objChooser1 = igdrp_getComboById("wdcFromDate");
			var objChooser2 = igdrp_getComboById("wdcToDate");
			document.ItemGroupInStoreResult.ddlItemGroup1.options[0].selected = true;
			document.ItemGroupInStoreResult.ddlItemGroup2.options[0].selected = true;
			document.ItemGroupInStoreResult.ddlItemGroup3.options[0].selected = true;
			document.ItemGroupInStoreResult.ddlItemGroup4.options[0].selected = true;
			objChooser1.setValue(null);
			objChooser2.setValue(null);
		}
		//-->
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="ItemGroupInStoreResult" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; POSITION: absolute; TOP: 8px; LEFT: 8px" cellSpacing="0"
				cellPadding="0" width="800" border="0">
				<TR>
					<TD style="HEIGHT: 103px; PADDING-TOP: 10px" vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-BOTTOM: #696969 2px solid; BORDER-LEFT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-RIGHT: #696969 2px solid"><LEGEND align="top">[검색조건]</LEGEND>
							<table id="Table2" cellSpacing="0" cellPadding="0" width="800">
								<tr>
									<td colSpan="8">
										<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="680">
											<tr>
												<td align="right" width="70" height="30"><FONT face="굴림">품목분류1&nbsp; </FONT>
												</td>
												<td align="left" width="100" height="30"><FONT face="굴림"><asp:dropdownlist id="ddlItemGroup1" runat="server"></asp:dropdownlist></FONT></td>
												<td align="right" width="70" height="30"><FONT face="굴림">품목분류2&nbsp; </FONT>
												</td>
												<td align="left" width="100" height="30"><asp:dropdownlist id="ddlItemGroup2" runat="server"></asp:dropdownlist></td>
												<td align="right" width="70" height="30"><FONT face="굴림">품목분류3&nbsp; </FONT>
												</td>
												<td align="left" width="100" height="30"><asp:dropdownlist id="ddlItemGroup3" runat="server"></asp:dropdownlist></td>
												<td align="right" width="70" height="30"><FONT face="굴림">품목분류4&nbsp; </FONT>
												</td>
												<td align="left" width="100" height="30"><asp:dropdownlist id="ddlItemGroup4" runat="server"></asp:dropdownlist></td>
											</tr>
										</TABLE>
									</td>
								</tr>
								<TR>
									<TD colSpan="8">
										<table id="Table3" cellSpacing="0" cellPadding="0" width="800">
											<TR>
												<TD style="HEIGHT: 30px" align="left" width="200" colSpan="2" height="30"><uc1:companysearchcontrol id="CSC" runat="server"></uc1:companysearchcontrol></TD>
												<TD style="HEIGHT: 30px" align="right" width="70" height="30">입고일자&nbsp;</TD>
												<TD style="HEIGHT: 30px" align="left" width="100" height="30"><igsch:webdatechooser id="wdcFromDate" runat="server" Text=" " MaxDate="2076-06-06" NullDateLabel=" "
														BorderStyle="Solid" BorderColor="DimGray" Font-Size="9pt" BackColor="#EEEEE9" Width="100px" Height="20px">
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
												<TD style="HEIGHT: 30px" align="center" width="15" height="30">~</TD>
												<TD style="HEIGHT: 30px" align="left" width="100" height="30"><igsch:webdatechooser id="wdcToDate" runat="server" Text=" " MaxDate="2076-06-06" NullDateLabel=" " BorderStyle="Solid"
														BorderColor="DimGray" Font-Size="9pt" BackColor="#EEEEE9" Width="100px" Height="20px">
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
												<TD style="HEIGHT: 30px" align="right" width="315" colSpan="2" height="30"><FONT face="굴림"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
															type="button" value="초기화" name="btnReset">
														<asp:button id="btCompanyGroupSearch" runat="server" Text="검 색" Width="60px" Height="20px"></asp:button>&nbsp;
													</FONT>
												</TD>
											</TR>
										</table>
									</TD>
								</TR>
							</table>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-BOTTOM: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-TOP: #696969 2px solid; BORDER-RIGHT: #696969 2px solid"><LEGEND align="top">[검색결과]</LEGEND>
							<TABLE id="Table8" cellSpacing="0" cellPadding="0" width="800">
								<TR>
									<TD colSpan="8" height="450"><FONT face="굴림"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="100%" Height="450px">
												<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" RowHeightDefault="20px" Version="3.00"
													SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" BorderCollapseDefault="Separate"
													AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed"
													AllowUpdateDefault="RowTemplateOnly">
													<AddNewBox>
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														</Style>
													</AddNewBox>
													<Pager PageSize="20" StyleMode="ComboBox" AllowPaging="True">
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														</Style>
													</Pager>
													<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray" Height="25px">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</HeaderStyleDefault>
													<FrameStyle Width="100%" BorderWidth="1px" Font-Size="9pt" Font-Names="Verdana" BorderColor="DimGray"
														BorderStyle="Solid" ForeColor="Transparent" BackColor="Silver" Height="450px"></FrameStyle>
													<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</FooterStyleDefault>
													<SelectedHeaderStyleDefault Cursor="Hand"></SelectedHeaderStyleDefault>
													<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
													<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
													<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
													<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
														<Padding Left="3px"></Padding>
														<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
													</RowStyleDefault>
												</DisplayLayout>
												<Bands>
													<igtbl:UltraGridBand>
														<Columns>
															<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="120px" BaseColumnName="ItemNum">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="120px" BaseColumnName="ItemName">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" Width="150px" BaseColumnName="CompanyName">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="입고일자" Key="DeliveryDate" Width="120px" Format="yyyy-MM-dd" BaseColumnName="DeliveryDate">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" BaseColumnName="BusinessRegistrationNum">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="수량" Key="DeliveryQuantity" Width="90px" Format="###,###,###" BaseColumnName="DeliveryQuantity">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																	<Padding Right="4px"></Padding>
																</CellStyle>
																<HeaderStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Width="98px" Format="###,###,###" BaseColumnName="ApplyUnitCost">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																	<Padding Right="4px"></Padding>
																</CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="총 금액" Key="TotalCost" Format="###,###,###" BaseColumnName="TotalCost">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																	<Padding Right="4px"></Padding>
																</CellStyle>
																<HeaderStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
														</Columns>
													</igtbl:UltraGridBand>
												</Bands>
											</igtbl:ultrawebgrid></FONT></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 265px" align="left" colSpan="2" height="30"><FONT face="굴림">&nbsp;</FONT>
										<asp:button id="Button1" runat="server" Text="Excel" Width="60px" Height="20px"></asp:button></TD>
									<TD align="left" colSpan="6" height="30">&nbsp;&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			<igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter><FONT face="굴림"></FONT></form>
	</body>
</HTML>
