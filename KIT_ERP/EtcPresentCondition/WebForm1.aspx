<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Page language="c#" Codebehind="WebForm1.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.EtcPresentCondition.WebForm1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<FONT face="굴림">
				<P>
					<uc1:companysearchcontrol id="CSC" runat="server"></uc1:companysearchcontrol>
					<TABLE id="Table4" cellSpacing="0" cellPadding="0" border="0">
						<TR>
							<TD style="HEIGHT: 30px" align="right" width="70" height="30"><FONT face="굴림">품목분류1&nbsp;</FONT></TD>
							<TD style="HEIGHT: 30px" align="left" width="100" height="30"><FONT face="굴림">
									<asp:dropdownlist id="ddlItemGroup1" runat="server"></asp:dropdownlist></FONT></TD>
							<TD align="right" width="70" height="30"><FONT face="굴림">품목분류2&nbsp;</FONT></TD>
							<TD align="left" width="100" height="30">
								<asp:dropdownlist id="ddlItemGroup2" runat="server"></asp:dropdownlist></TD>
							<TD align="right" width="70" height="30"><FONT face="굴림">품목분류3&nbsp;</FONT></TD>
							<TD align="left" width="100" height="30">
								<asp:dropdownlist id="ddlItemGroup3" runat="server"></asp:dropdownlist></TD>
							<TD align="right" width="70" height="30"><FONT face="굴림">거래처분류</FONT></TD>
							<TD align="left" width="210" height="30">
								<asp:dropdownlist id="ddlCompanyGroup" runat="server"></asp:dropdownlist></TD>
						</TR>
					</TABLE>
				</P>
				<P>
					<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="450px" Width="800px">
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
							<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="Verdana" BorderColor="DimGray"
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
									<igtbl:UltraGridColumn HeaderText="납품일자" Key="SaleDate" Width="120px" Format="yyyy-MM-dd" BaseColumnName="SaleDate">
										<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
										<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
									</igtbl:UltraGridColumn>
									<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" BaseColumnName="BusinessRegistrationNum">
										<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
									</igtbl:UltraGridColumn>
									<igtbl:UltraGridColumn HeaderText="납품수량" Key="SaleQuantity" Width="90px" Format="###,###,###" BaseColumnName="SaleQuantity">
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
									<igtbl:UltraGridColumn HeaderText="총 금액" Key="TotalSaleCost" Format="###,###,###" BaseColumnName="TotalSaleCost">
										<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
											<Padding Right="4px"></Padding>
										</CellStyle>
										<HeaderStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
									</igtbl:UltraGridColumn>
								</Columns>
							</igtbl:UltraGridBand>
						</Bands>
					</igtbl:ultrawebgrid></P>
				<P>&nbsp;</P>
				<P>
					<igsch:webdatechooser id="wdcFromDate" runat="server" Height="20px" Width="100px" BackColor="#EEEEE9"
						BorderStyle="Solid" BorderColor="DimGray" Font-Size="9pt" NullDateLabel=" " MaxDate="2076-06-06" Text=" ">
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
					</igsch:webdatechooser>
					<igsch:webdatechooser id="wdcToDate" runat="server" Height="20px" Width="100px" BackColor="#EEEEE9" BorderStyle="Solid"
						BorderColor="DimGray" Font-Size="9pt" NullDateLabel=" " MaxDate="2076-06-06" Text=" ">
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
					</igsch:webdatechooser>
					<asp:button id="Button1" runat="server" Height="20px" Width="60px" Font-Size="10pt" Text="Excel"></asp:button><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
						type="button" value="초기화" name="btnReset">
			</FONT>
			<asp:button id="btCompanyGroupSearch" runat="server" Height="20px" Width="60px" Text="검 색"></asp:button>
			<igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter></P>
			<P>&nbsp;</P>
			<P>&nbsp;</P>
			<P>&nbsp;</P>
			</FONT>
		</form>
	</body>
</HTML>
