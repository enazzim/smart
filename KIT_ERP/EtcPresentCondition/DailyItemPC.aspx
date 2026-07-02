<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="DailyItemPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.EtcPresentCondition.DailyItemPC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DailyItemPC</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!--
		function ResettxtBox()
		{
			ResetTextBox();
			ResetBox();
			var objChooser1 = igdrp_getComboById("wdcFromDate");
			var objChooser2 = igdrp_getComboById("wdcToDate");
			objChooser1.setValue(null);
			objChooser2.setValue(null);
			document.CustomerPurchaseResult.ddlDivision.options[0].selected=true;
			document.CustomerPurchaseResult.ddlYear.options[0].selected=true;
			document.CustomerPurchaseResult.ddlMon.options[0].selected=true;
			
		}
		//-->
		</script>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="DailyItemPC" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; POSITION: absolute; TOP: 8px; LEFT: 8px" cellSpacing="0"
				cellPadding="0" width="800" border="0">
				<TR>
					<TD style="HEIGHT: 103px; PADDING-TOP: 10px" vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-BOTTOM: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-TOP: #696969 2px solid; BORDER-RIGHT: #696969 2px solid"><LEGEND align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD colSpan="11">
										<table id="tb" cellSpacing="0" cellPadding="0" width="800" border="0">
											<TR>
												<TD style="HEIGHT: 30px" align="left" width="170" colSpan="2" height="30"><FONT face="굴림"><uc1:companysearchcontrol id="CompanySearchControl1" runat="server"></uc1:companysearchcontrol></FONT></TD>
												<TD style="HEIGHT: 30px" vAlign="middle" align="left" width="630" colSpan="5" height="30"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></TD>
											</TR>
										</table>
									</TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30"><FONT face="굴림">입고일자&nbsp;</FONT></TD>
									<TD width="100" height="30"><igsch:webdatechooser id="wdcFromDate" runat="server" BorderStyle="Solid" BorderColor="DimGray" Font-Size="9pt"
											BackColor="#EEEEE9" NullDateLabel=" " MaxDate="2076-06-06" Text=" " Width="100px" Height="20px">
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
									<TD width="100" height="30"><igsch:webdatechooser id="wdcToDate" runat="server" BorderStyle="Solid" BorderColor="DimGray" Font-Size="9pt"
											BackColor="#EEEEE9" NullDateLabel=" " MaxDate="2076-06-06" Text=" " Width="100px" Height="20px">
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
									<td width="40" align="right"><FONT face="굴림">구분&nbsp; </FONT>
									</td>
									<td width="70">
										<asp:DropDownList id="ddlDivision" runat="server" Width="70px" BackColor="#EEEEE9">
											<asp:ListItem Value="전체">전체</asp:ListItem>
											<asp:ListItem Value="구매">구매</asp:ListItem>
											<asp:ListItem Value="외주">외주</asp:ListItem>
											<asp:ListItem Value="기타">기타</asp:ListItem>
											<asp:ListItem Value="부자재">부자재</asp:ListItem>
										</asp:DropDownList>
									</td>
									<td width="40" align="right">&nbsp;
									</td>
									<td width="60"><FONT face="굴림"></FONT>
									</td>
									<td width="50" align="right">&nbsp;
									</td>
									<td width="50">
									</td>
									<td width="215" align="right">
										<FONT face="굴림"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
												type="button" value="초기화" name="btnReset">
											<asp:button id="btComSearch" runat="server" Text="검  색" Width="60px" Height="20px"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
										</FONT>
									</td>
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
									<TD colSpan="6" height="450"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="100%" Height="447px">
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
													BorderStyle="Solid" ForeColor="Transparent" BackColor="Silver" Height="447px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<SelectedHeaderStyleDefault Cursor="Hand"></SelectedHeaderStyleDefault>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BackColor="Transparent"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" Width="110px" BaseColumnName="CompanyName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" BaseColumnName="BusinessRegistrationNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="입고일자" Key="DeliveryDate" Width="90px" Format="yyyy-MM-dd" BaseColumnName="DeliveryDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="110px" BaseColumnName="ItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="110px" BaseColumnName="ItemName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정코드" Key="ProcessCode" Hidden="True" BaseColumnName="ProcessCode"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정명" Key="ProcessName" Width="70px" BaseColumnName="ProcessName">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="현재고" Key="NowStockQuantity" Width="87px" BaseColumnName="NowStockQuantity">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Width="70px" Format="###,###,###" BaseColumnName="ApplyUnitCost">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="입고량" Key="DeliveryQuantity" Width="70px" Format="###,###,###" BaseColumnName="DeliveryQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="출고량" Key="" Format="###,###,###" BaseColumnName="">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총 금액" Key="TotalCost" Width="87px" Format="###,###,###" BaseColumnName="TotalCost">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
															<HeaderStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="구분" Key="Division" Width="80px" BaseColumnName="Division">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="현재고" Key="" BaseColumnName="">
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
									<TD style="WIDTH: 265px" align="left" colSpan="2" height="30"><FONT face="굴림">&nbsp;</FONT>
										<asp:button id="Button1" runat="server" Font-Size="10pt" Text="Excel" Width="60px" Height="20px"></asp:button></TD>
									<TD align="left" colSpan="4" height="30">&nbsp;&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
						<igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter></TD>
				</TR>
			</TABLE>
			&nbsp;
		</form>
	</body>
</HTML>
