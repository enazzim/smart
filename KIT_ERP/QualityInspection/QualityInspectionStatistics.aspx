<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="QualityInspectionStatistics.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.QualityInspection.QualityInspectionStatistics" codePage="949" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>QualityInspectionStatistics</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!--		
			// 초기화 버튼
			function ResettxtBox()
			{
				var objChooser1 = igdrp_getComboById("txtStartDate");
				var objChooser2 = igdrp_getComboById("txtEndDate");

				objChooser1.setValue(null);
				objChooser2.setValue(null);
				
				document.Form1.ddlDiv.options[0].selected = true;
			}
		//-->
		</script>
	</HEAD>
	<body bottomMargin="0" bgColor="#f7f6f6" topMargin="10" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" style="LEFT: 10px; POSITION: absolute; TOP: 10px" cellSpacing="0" cellPadding="0"
				width="800" border="0">
				<TR>
					<TD width="20" height="60"></TD>
					<TD height="60">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; 90BORDER-RIGHT: black 1px solid"
							align="middle"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색조건 ]
							</LEGEND>
							<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD align="right" width="80" height="35">구분&nbsp;
									</TD>
									<TD align="left" width="145" height="35"><asp:dropdownlist id="ddlDiv" runat="server" BackColor="#EEEEE9" Height="20px" Width="115px">
											<asp:ListItem Value="0">전체</asp:ListItem>
											<asp:ListItem Value="1">제품부적합</asp:ListItem>
											<asp:ListItem Value="2">사내부적합</asp:ListItem>
											<asp:ListItem Value="3">구매부적합</asp:ListItem>
											<asp:ListItem Value="4">외주부적합</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD style="WIDTH: 37px" align="right" height="35">기간&nbsp;</TD>
									<TD align="left" width="100" height="35"><igsch:webdatechooser id="txtStartDate" runat="server" BackColor="#EEEEE9" Height="20px" Width="100px"
											Text="Null" NullDateLabel=" " BorderStyle="Solid" BorderColor="DimGray">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD align="left" width="11" height="35">&nbsp;~&nbsp;</TD>
									<TD height="35"><igsch:webdatechooser id="txtEndDate" runat="server" BackColor="#EEEEE9" Height="20px" Width="100px" Text="Null"
											NullDateLabel=" " BorderStyle="Solid" BorderColor="DimGray">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD align="right" colSpan="2" height="35"><INPUT style="WIDTH: 65px; HEIGHT: 20px" onclick="ResettxtBox()" type="button" value="초기화">&nbsp;
										<asp:button id="btnSearch" runat="server" Height="20px" Width="65px" Text="검  색"></asp:button>&nbsp;&nbsp;</TD>
								</TR>
								<TR>
									<TD align="right" width="80" colSpan="8" height="8"></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="20"></TD>
					<TD align="center">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; 90BORDER-RIGHT: black 1px solid"><br>
							<LEGEND style="FONT-SIZE: 10pt" align="left">
								[ 검색결과 ]
							</LEGEND>
							<table border="0" cellpadding="0" cellspacing="0" width="800" id="tbl">
								<tr>
									<td>
										<igtbl:ultrawebgrid id="dgStatisticsResult" runat="server" Height="425px" Width="100%" DESIGNTIMEDRAGDROP="533">
											<DisplayLayout ColFootersVisibleDefault="Yes" StationaryMargins="Header" AllowSortingDefault="OnClient"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="dgStatisticsResult" TableLayout="Fixed" CellClickActionDefault="RowSelect"
												ExpandableDefault="No">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</AddNewBox>
												<Pager>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Center"
													BackColor="LightGray" Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<FrameStyle Width="100%" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="425px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<RowExpAreaStyleDefault BackColor="White"></RowExpAreaStyleDefault>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None" BackColor="White"></EditCellStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" Font-Bold="True" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand></igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid>
									</td>
								</tr>
							</table>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD height="40">&nbsp;
										<asp:button id="Button2" runat="server" Height="20px" Width="65px" Text="Excel"></asp:button><igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server" WorksheetName="QualityInspectionStatistics"
											DownloadName="QualityInspectionStatistics.XLS"></igtblexp:ultrawebgridexcelexporter></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
