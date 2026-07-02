<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="BuyingOrderDelivery.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.EtcPresentCondition.BuyingOrderDelivery" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BuyingOrderDelivery</title>
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../StyleSheet2.css">
		<SCRIPT type="text/javascript"><!--
		function ResettxtBox()
		{
			
			ResetTextBox();
			ResetBox();
			var objChooser1 = igdrp_getComboById("wdcFromDate");
			var objChooser2 = igdrp_getComboById("wdcToDate");
			objChooser1.setValue(null);
			objChooser2.setValue(null);
			document.all.ddlDivision.options[0].selected=true;
					
		}
		//-->
		</SCRIPT>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE style="Z-INDEX: 101; POSITION: absolute; TOP: 8px; LEFT: 8px" id="Table1" border="0"
				cellSpacing="0" cellPadding="0" width="800">
				<TR>
					<TD style="HEIGHT: 103px; PADDING-TOP: 10px" vAlign="top" width="800" align="center">
						<FIELDSET style="BORDER-BOTTOM: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-TOP: #696969 2px solid; BORDER-RIGHT: #696969 2px solid"><LEGEND align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="0" width="800">
								<TR>
									<TD height="30" width="800" colSpan="10" align="right">
										<table border="0" cellSpacing="0" cellPadding="0" width="800">
											<tr>
												<td width="200"><uc1:companysearchcontrol id="CompanySearchControl1" runat="server"></uc1:companysearchcontrol></td>
												<td width="600"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></td>
											</tr>
										</table>
									</TD>
								</TR>
								<TR>
									<TD height="30" width="70" align="right"><FONT face="굴림">입고일 : &nbsp;</FONT></TD>
									<TD height="30" width="100"><FONT face="굴림"><igsch:webdatechooser id="wdcFromDate" runat="server" Height="20px" Width="100px" BackColor="#EEEEE9"
												Font-Size="9pt" Text=" " BorderStyle="Solid" BorderColor="DimGray" MaxDate="2076-06-06" NullDateLabel=" ">
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
											</igsch:webdatechooser></FONT></TD>
									<TD height="30" width="15" align="center"><FONT face="굴림">~</FONT></TD>
									<TD height="30" width="100"><FONT face="굴림"><igsch:webdatechooser id="wdcToDate" runat="server" Height="20px" Width="100px" BackColor="#EEEEE9" Font-Size="9pt"
												Text=" " BorderStyle="Solid" BorderColor="DimGray" MaxDate="2076-06-06" NullDateLabel=" ">
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
											</igsch:webdatechooser></FONT></TD>
									<td width="70" align="right"><FONT face="굴림">구&nbsp; 분&nbsp; </FONT>
									</td>
									<td width="100" align="left"><FONT face="굴림"><asp:dropdownlist id="ddlDivision" runat="server" Width="100px" BackColor="#EEEEE9">
												<asp:ListItem Value="구  매">구  매</asp:ListItem>
												<asp:ListItem Value="외  주">외  주</asp:ListItem>
											</asp:dropdownlist></FONT></td>
									<td width="70" align="right"><FONT face="굴림"></FONT></td>
									<td width="100" align="left"><FONT face="굴림"></FONT></td>
									<TD height="30" width="270" align="right"><FONT face="굴림"><INPUT style="WIDTH: 65px; HEIGHT: 20px" id="btnReset" onclick="javascript:ResettxtBox()"
												value="초기화" type="button" name="btnReset">&nbsp;&nbsp;
											<asp:button id="btItemSearch" runat="server" Height="20px" Width="60px" Text="검 색"></asp:button>&nbsp;&nbsp;
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
					<TD vAlign="top" width="800" align="center">
						<FIELDSET style="BORDER-BOTTOM: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-TOP: #696969 2px solid; BORDER-RIGHT: #696969 2px solid"><LEGEND align="top">[검색결과]</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800">
								<TR>
									<TD height="450" colSpan="6"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="446px" Width="100%">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" RowHeightDefault="20px" Version="3.00"
												SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" BorderCollapseDefault="Separate"
												AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed"
												CellClickActionDefault="RowSelect">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</AddNewBox>
												<Pager PageSize="19" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray" Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<FrameStyle Width="100%" BorderWidth="1px" Font-Size="9pt" Font-Names="Verdana" BorderColor="DimGray"
													BorderStyle="Solid" ForeColor="Transparent" BackColor="Silver" Height="446px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<SelectedHeaderStyleDefault Cursor="Hand"></SelectedHeaderStyleDefault>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="기종" Key="ItemClassification2" Width="70px" BaseColumnName="ItemClassification2"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래처" Key="CompanyName" Width="90px" Format="" BaseColumnName="CompanyName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="100px" BaseColumnName="ItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="110px" BaseColumnName="ItemName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정" Key="ProcessName" Width="50px" BaseColumnName="ProcessName">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단위" Key="Unit" HTMLEncodeContent="True" Width="50px" BaseColumnName="Unit">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="규격" Key="Standard" HTMLEncodeContent="True" Width="150px" BaseColumnName="Standard">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="발주량" Key="OrderQuantity" Width="70px" Format="###,###,###" BaseColumnName="OrderQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
															<HeaderStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="발주단가" Key="OrderUnitCost" Width="70px" Format="###,###,###" BaseColumnName="OrderUnitCost"
															FooterText="">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="발주금액" Key="OrderTotalCost" Width="70px" Format="###,###,###" BaseColumnName="OrderTotalCost"
															FooterText="">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="발주일" Key="OrderDate" Width="77px" Format="yyyy-MM-dd" BaseColumnName="OrderDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="입고량" Key="DeliveryQuantity" Width="70px" Format="###,###,###" BaseColumnName="DeliveryQuantity">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="입고단가" Key="UnitCost" Width="70px" Format="###,###,###" BaseColumnName="UnitCost">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="입고금액" Key="TotalCost" Width="90px" Format="###,###,###" BaseColumnName="TotalCost">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="입고일" Key="DeliveryDate" Width="77px" Format="yyyy-MM-dd" BaseColumnName="DeliveryDate"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="재고량" Key="NowStockQuantity" Width="70px" Format="###,###,###" BaseColumnName="NowStockQuantity">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 265px" height="30" colSpan="2" align="left"><FONT face="굴림">&nbsp;</FONT>
										<asp:button id="Button1" runat="server" Height="20px" Width="60px" Font-Size="10pt" Text="Excel"></asp:button></TD>
									<TD height="30" colSpan="4" align="left">&nbsp;&nbsp;
										<igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
