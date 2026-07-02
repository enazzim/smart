<%@ Page language="c#" Codebehind="BuyingPaymentPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BuyingOutside.BuyingPaymentPC" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BuyingPaymentPC</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript">
		// ÃÊ±âÈ­ ¹öÆ°
					function ResettxtBox(){
						ResetTextBox();
						var frm = document.QualityInspectionPresentCondition;
						var objCombo = igcmbo_getComboById("wcCompany");
						var objChooser1 = igdrp_getComboById("wdcFromDate");
						var objChooser2 = igdrp_getComboById("wdcToDate");
						
						objCombo.setDisplayValue("");
						objChooser1.setValue(null);
						objChooser2.setValue(null);
					}
					
		</SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<FONT face="±¼¸²">
				<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; WIDTH: 813px; POSITION: absolute; TOP: 8px; HEIGHT: 550px"
					height="550" cellSpacing="0" cellPadding="0" width="813" border="0">
					<TR>
						<TD vAlign="top" align="center" width="800" colSpan="2">
							<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 800px; BORDER-BOTTOM: #696969 2px solid"><LEGEND style="FONT-SIZE: 9pt" align="top">[°Ë»öÁ¶°Ç]</LEGEND>
								<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800">
									<TR>
										<TD width="600" colSpan="6" height="30"><FONT face="±¼¸²">
												<uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol>
												<igtblexp:UltraWebGridExcelExporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:UltraWebGridExcelExporter></FONT></TD>
										<TD align="right" width="70" height="30"></TD>
										<TD width="130" height="30"></TD>
									</TR>
									<TR>
										<TD align="right" width="70" height="30"><FONT style="FONT-SIZE: 9pt" face="±¼¸²">°Å·¡Ã³¸í&nbsp;</FONT></TD>
										<TD width="130" height="30">
											<igcmbo:webcombo id="wcCompany" runat="server" Font-Size="10pt" Width="115px" Height="22px" BackColor="#EEEEE9"
												Editable="True" DropImageXP2="/ig_common/WebGrid3/ig_cmboDown2.bmp" DropImageXP1="/ig_common/WebGrid3/ig_cmboDown1.bmp"
												Version="3.00" SelForeColor="White" BorderColor="DimGray" SelectedIndex="-1" SelBackColor="10, 36, 106"
												BorderWidth="1px" BorderStyle="Solid" ForeColor="Black">
												<Columns>
													<igtbl:UltraGridColumn HeaderText="°Å·¡Ã³¸í" Key="CompanyName" Width="250px" BaseColumnName="CompanyName">
														<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="´ëÇ¥ÀÚ¸í" Key="PresidentName" Width="0px" Hidden="True" BaseColumnName="PresidentName">
														<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="»ç¾÷ÀÚµî·Ï¹øÈ£" Key="BusinessRegistrationNum" Width="300px" BaseColumnName="BusinessRegistrationNum">
														<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="¹øÈ£" Key="CompanyInfoIndex" Width="0px" Hidden="True" BaseColumnName="CompanyInfoIndex">
														<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
													</igtbl:UltraGridColumn>
												</Columns>
												<DropDownLayout DropdownWidth="550px" BorderCollapse="Separate" RowSelectors="No" RowHeightDefault="20px"
													HeaderClickAction="Select" AutoGenerateColumns="False" DropdownHeight="400px" TableLayout="Fixed"
													StationaryMargins="Header">
													<RowStyle BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="White">
														<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
													</RowStyle>
													<SelectedRowStyle ForeColor="White" BackColor="#0A246A"></SelectedRowStyle>
													<HeaderStyle BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</HeaderStyle>
													<FrameStyle Width="100%" Cursor="Default" BorderWidth="2px" Font-Size="10pt" Font-Names="Verdana"
														BorderStyle="Ridge" BackColor="Silver" Height="100%"></FrameStyle>
												</DropDownLayout>
												<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
											</igcmbo:webcombo></TD>
										<TD align="right" width="70" height="30"><FONT style="FONT-SIZE: 9pt" face="±¼¸²">ÀÔ°íÀÏÀÚ&nbsp;</FONT></TD>
										<TD width="100" height="30">
											<igsch:webdatechooser id="wdcFromDate" runat="server" Width="100px" Height="22px" BackColor="#EEEEE9"
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
										<TD align="center" width="15" height="30"><FONT face="±¼¸²">~</FONT></TD>
										<TD width="215" height="30">
											<igsch:webdatechooser id="wdcToDate" runat="server" Width="100px" Height="22px" BackColor="#EEEEE9" BorderColor="DimGray"
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
										<TD align="right" width="200" colSpan="2" height="30"><INPUT id="bt_Clear" style="WIDTH: 60px; HEIGHT: 20px" type="button" value="ÃÊ±âÈ­" onclick="ResettxtBox()">&nbsp;
											<asp:button id="Button1" runat="server" Height="20px" Width="60px" Text="Ç° ¸ñ"></asp:button>&nbsp;
											<asp:button id="bt_Search" runat="server" Width="60px" Height="20px" Text="°Å·¡Ã³"></asp:button>&nbsp;</TD>
									</TR>
									<TR>
										<TD align="right" width="70" height="5"><FONT face="±¼¸²"></FONT></TD>
										<TD width="130" height="5"><FONT face="±¼¸²"></FONT></TD>
										<TD align="right" width="70" height="5"></TD>
										<TD width="100" height="5"></TD>
										<TD align="center" width="15" height="5"></TD>
										<TD width="215" height="5"></TD>
										<TD align="right" width="200" colSpan="2" height="5"></TD>
									</TR>
								</TABLE>
							</FIELDSET>
						</TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" width="800" colSpan="2">&nbsp;
							<BR>
							<FIELDSET title="86" style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND style="FONT-SIZE: 9pt" align="top">[°Ë»ö°á°ú]</LEGEND>
								<TABLE id="Table4" style="WIDTH: 100%" cellSpacing="1" cellPadding="1" width="800" border="0">
									<TR>
										<TD align="center" width="800" colSpan="2">
											<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="100%" Height="390px">
												<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" RowHeightDefault="20px" Version="3.00"
													SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti"
													BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1"
													TableLayout="Fixed" NoDataMessage="ÇØ´ç ÀÚ·á°¡ ¾ø½À´Ï´Ù!">
													<AddNewBox>
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														</Style>
													</AddNewBox>
													<Pager PageSize="17" StyleMode="ComboBox">
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														</Style>
													</Pager>
													<HeaderStyleDefault Cursor="Auto" BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</HeaderStyleDefault>
													<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
													<FrameStyle Width="100%" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
														BackColor="Silver" Height="390px"></FrameStyle>
													<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</FooterStyleDefault>
													<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
													<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
													<RowAlternateStyleDefault BackColor="#EBEFF6"></RowAlternateStyleDefault>
													<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
														<Padding Left="3px"></Padding>
														<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
													</RowStyleDefault>
												</DisplayLayout>
												<Bands>
													<igtbl:UltraGridBand>
														<Columns>
															<igtbl:UltraGridColumn HeaderText="°Å·¡Ã³¸í" Key="CompanyName" Width="200px" HeaderClickAction="SortMulti"
																BaseColumnName="CompanyName">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="Ç°¸ñ¹øÈ£" Key="ItemNum" Width="135px" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="Ç°¸ñ¸í" Key="ItemName" Width="140px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="ÀÔ°íÀÏ" Key="DeliveryDate" Width="80px" Format="yyyy-MM-dd" BaseColumnName="DeliveryDate">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="ÃÑÀÔ°í·®" Key="PaymentQuantity" Width="80px" Format="###,###,###" HeaderClickAction="SortMulti"
																BaseColumnName="PaymentQuantity">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																	<Padding Right="2px"></Padding>
																	<Margin Right="2px"></Margin>
																</CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="´Ü°¡" Key="UnitCost" Width="50px" Format="\ ###,###,##0" BaseColumnName="UnitCost">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																	<Padding Right="2px"></Padding>
																	<Margin Right="2px"></Margin>
																</CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="ÃÑÁö±Þ¾×" Key="TotalPaymentCost" Width="100px" Format="\ ###,###,##0" BaseColumnName="TotalPaymentCost">
																<CellStyle HorizontalAlign="Right">
																	<Padding Right="2px"></Padding>
																	<Margin Right="2px"></Margin>
																</CellStyle>
															</igtbl:UltraGridColumn>
														</Columns>
														<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
															<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
														</RowTemplateStyle>
														<RowEditTemplate>
															<TABLE id="Table3" style="WIDTH: 612px; COLOR: buttontext; HEIGHT: 266px; BACKGROUND-COLOR: whitesmoke">
																<TR>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16" aline="right"><FONT face="±¼¸²">Ç°¸ñ¹øÈ£</FONT></TD>
																	<TD height="16" aline="left"><INPUT id="igtbl_TextBox_0_1" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																			disabled readOnly type="text" name="igtbl_TextBox_0_1" columnKey="ItemNum"></TD>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16" aline="right"><FONT face="±¼¸²">µµ¸é¹øÈ£</FONT></TD>
																	<TD height="16" aline="left"><INPUT id="igtbl_TextBox_0_2" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																			disabled readOnly type="text" name="igtbl_TextBox_0_2" columnKey="ItemDrawNum"></TD>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16" aline="right"><FONT face="±¼¸²">Ç°¸ñ¸í</FONT></TD>
																	<TD height="16" aline="left"><INPUT id="igtbl_TextBox_0_3" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																			disabled readOnly type="text" name="igtbl_TextBox_0_3" columnKey="ItemName"></TD>
																</TR>
																<TR>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16" aline="right"><FONT face="±¼¸²">¼öÁÖÀÏÀÚ</FONT></TD>
																	<TD height="16" aline="left">
																		<igsch:webdatechooser id="wdc_EditReceivingDate" runat="server" Width="110px" Height="16px" Text="Null"
																			NullDateLabel=" " columnKey="ReceivingOrderDate">
																			<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
																				DayNameFormat="FirstLetter">
																				<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
																				<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																				<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																				<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
																			</CALENDARLAYOUT>
																			<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
																			<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
																			<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
																		</igsch:webdatechooser></TD>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16" aline="right"><FONT style="FONT-SIZE: 9pt" face="±¼¸²">ÀÚ»êºÐ·ù</FONT></TD>
																	<TD height="16" aline="left"><INPUT id="igtbl_TextBox_0_6" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																			disabled readOnly type="text" name="igtbl_TextBox_0_6" columnKey="PropertyClassification"></TD>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT style="FONT-SIZE: 9pt" face="±¼¸²">ÃÑ¼öÁÖ·®</FONT></TD>
																	<TD height="16" aline="left"><FONT face="±¼¸²"><INPUT id="tb_TotalReceiveingOrderQuantity" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																				onkeyup="return Process();" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																				onfocus="OnFocus_Obj(this);" type="text" name="tb_TotalReceiveingOrderQuantity" columnKey="TotalReceiveingOrderQuantity"></FONT></TD>
																</TR>
																<TR>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT style="FONT-SIZE: 9pt" face="±¼¸²">1Â÷³³±â¿ä±¸·®</FONT></TD>
																	<TD height="16" aline="left"><FONT face="±¼¸²"><FONT face="±¼¸²"><INPUT id="igtbl_TextBox_0_8" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																					onkeyup="return Process();" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																					onfocus="OnFocus_Obj(this);" type="text" name="igtbl_TextBox_0_8" columnKey="DeliveryRequestQuantity1"></FONT></FONT></TD>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT style="FONT-SIZE: 9pt" face="±¼¸²">1Â÷³³±â¿ä±¸ÀÏ</FONT></TD>
																	<TD height="16" aline="left"><FONT face="±¼¸²">
																			<igsch:webdatechooser id="wdc_Date1" runat="server" Width="110px" Height="20px" Text="Null" NullDateLabel=" "
																				columnKey="DeliveryRequestDate1">
																				<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
																					DayNameFormat="FirstLetter">
																					<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
																					<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																					<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																					<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
																				</CALENDARLAYOUT>
																				<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
																				<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
																				<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
																			</igsch:webdatechooser></FONT></TD>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT style="FONT-SIZE: 9pt" face="±¼¸²">2Â÷³³±â¿ä±¸·®</FONT></TD>
																	<TD height="16" aline="left"><FONT face="±¼¸²"><INPUT id="igtbl_TextBox_0_10" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																				onkeyup="return Process();" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																				onfocus="OnFocus_Obj(this);" type="text" name="igtbl_TextBox_0_10" columnKey="DeliveryRequestQuantity2"></FONT></TD>
																</TR>
																<TR>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT face="±¼¸²">2Â÷³³±â¿ä±¸ÀÏ</FONT></TD>
																	<TD height="16" aline="left"><FONT face="±¼¸²">
																			<igsch:webdatechooser id="wdc_Date2" runat="server" Width="110px" Height="20px" Text="Null" NullDateLabel=" "
																				columnKey="DeliveryRequestDate2">
																				<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
																					DayNameFormat="FirstLetter">
																					<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
																					<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																					<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																					<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
																				</CALENDARLAYOUT>
																				<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
																				<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
																				<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
																			</igsch:webdatechooser></FONT></TD>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT style="FONT-SIZE: 9pt" face="±¼¸²">3Â÷³³±â¿ä±¸·®</FONT></TD>
																	<TD height="16" aline="left"><FONT face="±¼¸²"><INPUT id="igtbl_TextBox_0_12" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																				onkeyup="return Process();" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																				onfocus="OnFocus_Obj(this);" type="text" name="igtbl_TextBox_0_12" columnKey="DeliveryRequestQuantity3"></FONT></TD>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT face="±¼¸²"><FONT style="FONT-SIZE: 9pt" face="±¼¸²">3Â÷³³±â¿ä±¸ÀÏ</FONT></FONT></TD>
																	<TD height="16" aline="left"><FONT face="±¼¸²"><FONT face="±¼¸²">
																				<igsch:webdatechooser id="wdc_Date3" runat="server" Width="110px" Height="20px" Text="Null" NullDateLabel=" "
																					columnKey="DeliveryRequestDate3">
																					<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
																						DayNameFormat="FirstLetter">
																						<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
																						<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																						<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																						<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
																					</CALENDARLAYOUT>
																					<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
																					<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
																					<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
																				</igsch:webdatechooser></FONT></FONT></TD>
																</TR>
																<TR>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT face="±¼¸²">4Â÷³³±â¿ä±¸·®</FONT></TD>
																	<TD height="16" aline="left"><FONT face="±¼¸²"><INPUT id="igtbl_TextBox_0_14" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																				onkeyup="return Process();" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																				onfocus="OnFocus_Obj(this);" type="text" name="igtbl_TextBox_0_14" columnKey="DeliveryRequestQuantity4"></FONT></TD>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT face="±¼¸²">4Â÷³³±â¿ä±¸ÀÏ</FONT></TD>
																	<TD height="16" aline="left"><FONT face="±¼¸²">
																			<igsch:webdatechooser id="wdc_Date4" runat="server" Width="110px" Height="20px" Text="Null" NullDateLabel=" "
																				columnKey="DeliveryRequestDate4">
																				<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
																					DayNameFormat="FirstLetter">
																					<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
																					<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																					<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																					<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
																				</CALENDARLAYOUT>
																				<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
																				<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
																				<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
																			</igsch:webdatechooser></FONT></TD>
																	<TD width="110" bgColor="#dcdcdc" height="16" aline="right"><FONT face="±¼¸²">&nbsp;5Â÷³³±â¿ä±¸·®</FONT></TD>
																	<TD height="16" aline="left"><FONT face="±¼¸²"><INPUT id="igtbl_TextBox_0_16" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																				onkeyup="return Process();" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																				onfocus="OnFocus_Obj(this);" type="text" name="igtbl_TextBox_0_16" columnKey="DeliveryRequestQuantity5"></FONT></TD>
																</TR>
																<TR>
																	<TD align="right" bgColor="#dcdcdc" height="16"><FONT face="±¼¸²">5Â÷³³±â¿ä±¸ÀÏ</FONT></TD>
																	<TD><FONT face="±¼¸²">
																			<igsch:webdatechooser id="wdc_Date5" runat="server" Width="110px" Height="20px" Text="Null" NullDateLabel=" "
																				columnKey="DeliveryRequestDate5">
																				<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
																					DayNameFormat="FirstLetter">
																					<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
																					<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																					<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																					<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
																				</CALENDARLAYOUT>
																				<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
																				<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
																				<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
																			</igsch:webdatechooser></FONT></TD>
																	<TD align="right" bgColor="#dcdcdc" height="16"><FONT face="±¼¸²">Àû¿ë´Ü°¡</FONT></TD>
																	<TD height="16"><FONT face="±¼¸²"><INPUT id="igtbl_TextBox_0_19" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																				onkeyup="return Process();" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																				onfocus="OnFocus_Obj(this);" type="text" name="igtbl_TextBox_0_19" columnKey="ApplyUnitCost"></FONT></TD>
																	<TD align="right" bgColor="#dcdcdc" height="16"><FONT face="±¼¸²">³³Ç°Àå¼Ò</FONT></TD>
																	<TD height="16"><FONT face="±¼¸²"><FONT face="±¼¸²"><INPUT id="igtbl_TextBox_0_26" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																					type="text" name="igtbl_TextBox_0_26" columnKey="DeliveryPlace"></FONT></FONT></TD>
																</TR>
																<TR>
																	<TD style="HEIGHT: 16px" align="right" bgColor="#dcdcdc"><FONT face="±¼¸²"><FONT face="±¼¸²"><FONT face="±¼¸²">¹ßÁÖ¹øÈ£</FONT></FONT></FONT></TD>
																	<TD style="HEIGHT: 16px"><FONT face="±¼¸²"><FONT face="±¼¸²"><FONT face="±¼¸²"><INPUT id="igtbl_TextBox_0_25" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																						type="text" name="igtbl_TextBox_0_25" columnKey="OrderNum"></FONT></FONT></FONT></TD>
																	<TD style="HEIGHT: 16px" colSpan="4"><FONT face="±¼¸²"></FONT></TD>
																</TR>
																<TR>
																	<TD style="HEIGHT: 2px" colSpan="6"><FONT face="±¼¸²"><FONT face="±¼¸²"><FONT face="±¼¸²"><FONT face="±¼¸²"></FONT></FONT></FONT></FONT></TD>
																</TR>
																<TR>
																	<TD style="HEIGHT: 61px" colSpan="6">
																		<P><FONT face="±¼¸²"></FONT>&nbsp;</P>
																		<P><FONT face="±¼¸²"></FONT>&nbsp;</P>
																	</TD>
																</TR>
																<TR>
																	<TD align="center" colSpan="6"><INPUT id="igtbl_reOkBtn" style="WIDTH: 60px; HEIGHT: 20px" onclick="igtbl_gRowEditButtonClick(event);__doPostBack('lnk_Update','');"
																			type="button" value="¼ö Á¤" name="igtbl_reOkBtn">&nbsp; <INPUT id="igtbl_reCancelBtn" style="WIDTH: 60px; HEIGHT: 20px" onclick="igtbl_gRowEditButtonClick(event);"
																			type="button" value="Ãë ¼Ò" name="igtbl_reCancelBtn">
																	</TD>
																</TR>
															</TABLE>
														</RowEditTemplate>
													</igtbl:UltraGridBand>
												</Bands>
											</igtbl:ultrawebgrid>
											<br>
											<igtbl:ultrawebgrid id="UltraWebGrid2" runat="server" Height="390px" Width="100%" Visible="False">
												<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" RowHeightDefault="20px" Version="3.00"
													SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti"
													BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="Ultrawebgrid2"
													TableLayout="Fixed" NoDataMessage="ÇØ´ç ÀÚ·á°¡ ¾ø½À´Ï´Ù!">
													<AddNewBox>
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														</Style>
													</AddNewBox>
													<Pager PageSize="17" StyleMode="ComboBox">
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														</Style>
													</Pager>
													<HeaderStyleDefault Cursor="Auto" BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</HeaderStyleDefault>
													<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
													<FrameStyle Width="100%" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
														BackColor="Silver" Height="390px"></FrameStyle>
													<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</FooterStyleDefault>
													<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
													<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
													<RowAlternateStyleDefault BackColor="#EBEFF6"></RowAlternateStyleDefault>
													<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
														<Padding Left="3px"></Padding>
														<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
													</RowStyleDefault>
												</DisplayLayout>
												<Bands>
													<igtbl:UltraGridBand>
														<Columns>
															<igtbl:UltraGridColumn HeaderText="Ç°¸ñ¹øÈ£" Key="ItemNum" Width="135px" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="Ç°¸ñ¸í" Key="ItemName" Width="140px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="°Å·¡Ã³¸í" Key="CompanyName" Width="200px" HeaderClickAction="SortMulti"
																BaseColumnName="CompanyName">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="ÀÔ°íÀÏ" Key="DeliveryDate" Width="80px" Format="yyyy-MM-dd" BaseColumnName="DeliveryDate">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="ÃÑÀÔ°í·®" Key="PaymentQuantity" Width="80px" Format="###,###,###" HeaderClickAction="SortMulti"
																BaseColumnName="PaymentQuantity">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																	<Padding Right="2px"></Padding>
																	<Margin Right="2px"></Margin>
																</CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="´Ü°¡" Key="UnitCost" Width="50px" Format="\ ###,###,##0" BaseColumnName="UnitCost">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																	<Padding Right="2px"></Padding>
																	<Margin Right="2px"></Margin>
																</CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="ÃÑÁö±Þ¾×" Key="TotalPaymentCost" Width="100px" Format="\ ###,###,##0" BaseColumnName="TotalPaymentCost">
																<CellStyle HorizontalAlign="Right">
																	<Padding Right="2px"></Padding>
																	<Margin Right="2px"></Margin>
																</CellStyle>
															</igtbl:UltraGridColumn>
														</Columns>
														<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
															<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
														</RowTemplateStyle>
														<RowEditTemplate>
															<TABLE id="Table3" style="WIDTH: 612px; COLOR: buttontext; HEIGHT: 266px; BACKGROUND-COLOR: whitesmoke">
																<TR>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16" aline="right"><FONT face="±¼¸²">Ç°¸ñ¹øÈ£</FONT></TD>
																	<TD height="16" aline="left"><INPUT id="igtbl_TextBox_0_1" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																			disabled readOnly type="text" name="igtbl_TextBox_0_1" columnKey="ItemNum"></TD>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16" aline="right"><FONT face="±¼¸²">µµ¸é¹øÈ£</FONT></TD>
																	<TD height="16" aline="left"><INPUT id="igtbl_TextBox_0_2" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																			disabled readOnly type="text" name="igtbl_TextBox_0_2" columnKey="ItemDrawNum"></TD>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16" aline="right"><FONT face="±¼¸²">Ç°¸ñ¸í</FONT></TD>
																	<TD height="16" aline="left"><INPUT id="igtbl_TextBox_0_3" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																			disabled readOnly type="text" name="igtbl_TextBox_0_3" columnKey="ItemName"></TD>
																</TR>
																<TR>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16" aline="right"><FONT face="±¼¸²">¼öÁÖÀÏÀÚ</FONT></TD>
																	<TD height="16" aline="left">
																		<igsch:webdatechooser id="wdc_EditReceivingDate" runat="server" Width="110px" Height="16px" Text="Null"
																			NullDateLabel=" " columnKey="ReceivingOrderDate">
																			<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
																				DayNameFormat="FirstLetter">
																				<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
																				<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																				<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																				<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
																			</CALENDARLAYOUT>
																			<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
																			<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
																			<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
																		</igsch:webdatechooser></TD>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16" aline="right"><FONT style="FONT-SIZE: 9pt" face="±¼¸²">ÀÚ»êºÐ·ù</FONT></TD>
																	<TD height="16" aline="left"><INPUT id="igtbl_TextBox_0_6" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																			disabled readOnly type="text" name="igtbl_TextBox_0_6" columnKey="PropertyClassification"></TD>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT style="FONT-SIZE: 9pt" face="±¼¸²">ÃÑ¼öÁÖ·®</FONT></TD>
																	<TD height="16" aline="left"><FONT face="±¼¸²"><INPUT id="tb_TotalReceiveingOrderQuantity" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																				onkeyup="return Process();" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																				onfocus="OnFocus_Obj(this);" type="text" name="tb_TotalReceiveingOrderQuantity" columnKey="TotalReceiveingOrderQuantity"></FONT></TD>
																</TR>
																<TR>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT style="FONT-SIZE: 9pt" face="±¼¸²">1Â÷³³±â¿ä±¸·®</FONT></TD>
																	<TD height="16" aline="left"><FONT face="±¼¸²"><FONT face="±¼¸²"><INPUT id="igtbl_TextBox_0_8" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																					onkeyup="return Process();" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																					onfocus="OnFocus_Obj(this);" type="text" name="igtbl_TextBox_0_8" columnKey="DeliveryRequestQuantity1"></FONT></FONT></TD>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT style="FONT-SIZE: 9pt" face="±¼¸²">1Â÷³³±â¿ä±¸ÀÏ</FONT></TD>
																	<TD height="16" aline="left"><FONT face="±¼¸²">
																			<igsch:webdatechooser id="wdc_Date1" runat="server" Width="110px" Height="20px" Text="Null" NullDateLabel=" "
																				columnKey="DeliveryRequestDate1">
																				<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
																					DayNameFormat="FirstLetter">
																					<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
																					<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																					<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																					<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
																				</CALENDARLAYOUT>
																				<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
																				<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
																				<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
																			</igsch:webdatechooser></FONT></TD>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT style="FONT-SIZE: 9pt" face="±¼¸²">2Â÷³³±â¿ä±¸·®</FONT></TD>
																	<TD height="16" aline="left"><FONT face="±¼¸²"><INPUT id="igtbl_TextBox_0_10" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																				onkeyup="return Process();" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																				onfocus="OnFocus_Obj(this);" type="text" name="igtbl_TextBox_0_10" columnKey="DeliveryRequestQuantity2"></FONT></TD>
																</TR>
																<TR>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT face="±¼¸²">2Â÷³³±â¿ä±¸ÀÏ</FONT></TD>
																	<TD height="16" aline="left"><FONT face="±¼¸²">
																			<igsch:webdatechooser id="wdc_Date2" runat="server" Width="110px" Height="20px" Text="Null" NullDateLabel=" "
																				columnKey="DeliveryRequestDate2">
																				<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
																					DayNameFormat="FirstLetter">
																					<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
																					<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																					<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																					<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
																				</CALENDARLAYOUT>
																				<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
																				<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
																				<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
																			</igsch:webdatechooser></FONT></TD>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT style="FONT-SIZE: 9pt" face="±¼¸²">3Â÷³³±â¿ä±¸·®</FONT></TD>
																	<TD height="16" aline="left"><FONT face="±¼¸²"><INPUT id="igtbl_TextBox_0_12" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																				onkeyup="return Process();" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																				onfocus="OnFocus_Obj(this);" type="text" name="igtbl_TextBox_0_12" columnKey="DeliveryRequestQuantity3"></FONT></TD>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT face="±¼¸²"><FONT style="FONT-SIZE: 9pt" face="±¼¸²">3Â÷³³±â¿ä±¸ÀÏ</FONT></FONT></TD>
																	<TD height="16" aline="left"><FONT face="±¼¸²"><FONT face="±¼¸²">
																				<igsch:webdatechooser id="wdc_Date3" runat="server" Width="110px" Height="20px" Text="Null" NullDateLabel=" "
																					columnKey="DeliveryRequestDate3">
																					<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
																						DayNameFormat="FirstLetter">
																						<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
																						<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																						<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																						<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
																					</CALENDARLAYOUT>
																					<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
																					<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
																					<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
																				</igsch:webdatechooser></FONT></FONT></TD>
																</TR>
																<TR>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT face="±¼¸²">4Â÷³³±â¿ä±¸·®</FONT></TD>
																	<TD height="16" aline="left"><FONT face="±¼¸²"><INPUT id="igtbl_TextBox_0_14" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																				onkeyup="return Process();" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																				onfocus="OnFocus_Obj(this);" type="text" name="igtbl_TextBox_0_14" columnKey="DeliveryRequestQuantity4"></FONT></TD>
																	<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT face="±¼¸²">4Â÷³³±â¿ä±¸ÀÏ</FONT></TD>
																	<TD height="16" aline="left"><FONT face="±¼¸²">
																			<igsch:webdatechooser id="wdc_Date4" runat="server" Width="110px" Height="20px" Text="Null" NullDateLabel=" "
																				columnKey="DeliveryRequestDate4">
																				<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
																					DayNameFormat="FirstLetter">
																					<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
																					<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																					<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																					<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
																				</CALENDARLAYOUT>
																				<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
																				<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
																				<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
																			</igsch:webdatechooser></FONT></TD>
																	<TD width="110" bgColor="#dcdcdc" height="16" aline="right"><FONT face="±¼¸²">&nbsp;5Â÷³³±â¿ä±¸·®</FONT></TD>
																	<TD height="16" aline="left"><FONT face="±¼¸²"><INPUT id="igtbl_TextBox_0_16" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																				onkeyup="return Process();" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																				onfocus="OnFocus_Obj(this);" type="text" name="igtbl_TextBox_0_16" columnKey="DeliveryRequestQuantity5"></FONT></TD>
																</TR>
																<TR>
																	<TD align="right" bgColor="#dcdcdc" height="16"><FONT face="±¼¸²">5Â÷³³±â¿ä±¸ÀÏ</FONT></TD>
																	<TD><FONT face="±¼¸²">
																			<igsch:webdatechooser id="wdc_Date5" runat="server" Width="110px" Height="20px" Text="Null" NullDateLabel=" "
																				columnKey="DeliveryRequestDate5">
																				<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
																					DayNameFormat="FirstLetter">
																					<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
																					<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																					<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																					<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
																				</CALENDARLAYOUT>
																				<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
																				<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
																				<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
																			</igsch:webdatechooser></FONT></TD>
																	<TD align="right" bgColor="#dcdcdc" height="16"><FONT face="±¼¸²">Àû¿ë´Ü°¡</FONT></TD>
																	<TD height="16"><FONT face="±¼¸²"><INPUT id="igtbl_TextBox_0_19" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																				onkeyup="return Process();" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																				onfocus="OnFocus_Obj(this);" type="text" name="igtbl_TextBox_0_19" columnKey="ApplyUnitCost"></FONT></TD>
																	<TD align="right" bgColor="#dcdcdc" height="16"><FONT face="±¼¸²">³³Ç°Àå¼Ò</FONT></TD>
																	<TD height="16"><FONT face="±¼¸²"><FONT face="±¼¸²"><INPUT id="igtbl_TextBox_0_26" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																					type="text" name="igtbl_TextBox_0_26" columnKey="DeliveryPlace"></FONT></FONT></TD>
																</TR>
																<TR>
																	<TD style="HEIGHT: 16px" align="right" bgColor="#dcdcdc"><FONT face="±¼¸²"><FONT face="±¼¸²"><FONT face="±¼¸²">¹ßÁÖ¹øÈ£</FONT></FONT></FONT></TD>
																	<TD style="HEIGHT: 16px"><FONT face="±¼¸²"><FONT face="±¼¸²"><FONT face="±¼¸²"><INPUT id="igtbl_TextBox_0_25" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																						type="text" name="igtbl_TextBox_0_25" columnKey="OrderNum"></FONT></FONT></FONT></TD>
																	<TD style="HEIGHT: 16px" colSpan="4"><FONT face="±¼¸²"></FONT></TD>
																</TR>
																<TR>
																	<TD style="HEIGHT: 2px" colSpan="6"><FONT face="±¼¸²"><FONT face="±¼¸²"><FONT face="±¼¸²"><FONT face="±¼¸²"></FONT></FONT></FONT></FONT></TD>
																</TR>
																<TR>
																	<TD style="HEIGHT: 61px" colSpan="6">
																		<P><FONT face="±¼¸²"></FONT>&nbsp;</P>
																		<P><FONT face="±¼¸²"></FONT>&nbsp;</P>
																	</TD>
																</TR>
																<TR>
																	<TD align="center" colSpan="6"><INPUT id="igtbl_reOkBtn" style="WIDTH: 60px; HEIGHT: 20px" onclick="igtbl_gRowEditButtonClick(event);__doPostBack('lnk_Update','');"
																			type="button" value="¼ö Á¤" name="igtbl_reOkBtn">&nbsp; <INPUT id="igtbl_reCancelBtn" style="WIDTH: 60px; HEIGHT: 20px" onclick="igtbl_gRowEditButtonClick(event);"
																			type="button" value="Ãë ¼Ò" name="igtbl_reCancelBtn">
																	</TD>
																</TR>
															</TABLE>
														</RowEditTemplate>
													</igtbl:UltraGridBand>
												</Bands>
											</igtbl:ultrawebgrid>
										</TD>
									</TR>
									<TR>
										<TD style="WIDTH: 34px" height="30">
											<asp:button id="Button3" runat="server" Width="60px" Height="20px" Text="Excel"></asp:button></TD>
										<TD align="right" height="30">&nbsp;&nbsp;&nbsp;&nbsp;</TD>
									</TR>
								</TABLE>
							</FIELDSET>
						</TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
