<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="UpdateUnitCostHistory.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.UpdateUnitCostHistory" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>UpdateUnitCostHistory</title>
		<meta http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../GridCheck.js"></script>
		<script language="javascript" src="../MessageWindows.js"></script>
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript">
		<!--
		function ResettxtBox()
		{
			ResetTextBox();
			ResetBox();
			
			var objDChooser = igdrp_getComboById("wdcBeginDate");
			objDChooser.setValue(null);
			var objDChooser1 = igdrp_getComboById("wdcEndDate");
			objDChooser1.setValue(null);
			
			Form1.ddlUserName.options[0].selected=true;				
		}
		//-->
		</SCRIPT>
</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="PADDING-LEFT: 10px; LEFT: 0px; PADDING-TOP: 10px; POSITION: absolute; TOP: 0px"
				height="540" cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 800px; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top"><asp:label id="searchTitle" Runat="server" text="[검색조건]" Font-Size="10pt">[검색조건]</asp:label></LEGEND>
							<TABLE id="Table2" style="HEIGHT: 2px" cellSpacing="0" cellPadding="0" width="800">
								<TR>
									<TD colSpan="7" height="30">
									<table id="table3" border="0" cellpadding="0" cellspacing="0" width="800">
											<tr>
												<td width="200">
													<uc1:CompanySearchControl id="CSC1" runat="server"></uc1:CompanySearchControl></td>
												<td width="600"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></td>
											</tr>
										</table>
									
									</TD>
								</TR>
								<TR>
									<td align="right" width="70">수정일&nbsp;
									</td>
									<td align="left" width="100">
										<igsch:webdatechooser id="wdcBeginDate" runat="server" Font-Size="10pt" BackColor="#EEEEE9" Width="100px"
											Text=" " Height="18px" BorderStyle="Solid" BorderColor="DimGray" NullDateLabel=" ">
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
										</igsch:webdatechooser></td>
									<td align="right" width="10">&nbsp; ~
									</td>
									<td align="left" width="100">
										<igsch:webdatechooser id="wdcEndDate" runat="server" Font-Size="10pt" BackColor="#EEEEE9" Width="100px"
											Text=" " Height="18px" BorderStyle="Solid" BorderColor="DimGray" NullDateLabel=" ">
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
										</igsch:webdatechooser></td>
									<td width="70" align="right">등록자&nbsp;</td>
									<td width="130">
										<asp:DropDownList id="ddlUserName" runat="server" Width="100px"></asp:DropDownList></td>
									<TD align="right" height="30" width=320><FONT face="굴림"><INPUT style="WIDTH: 60px; HEIGHT: 20px" onclick="ResettxtBox()" type="button" value="초기화">&nbsp;</FONT>
										<asp:button id="bt_Search" runat="server" Width="60px" Height="20px" Text="검색"></asp:button><FONT face="굴림">&nbsp;
										</FONT>
									</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top"><asp:label id="Label2" Runat="server" text="[검색조건]" Font-Size="10pt">[검색결과]</asp:label></LEGEND>
							<TABLE id="Table3" style="HEIGHT: 440px" cellSpacing="1" cellPadding="1" width="800" border="0">
								<TR>
									<TD><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="450px">
<DisplayLayout AutoGenerateColumns="False" AllowSortingDefault="OnClient" RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1" CellClickActionDefault="RowSelect">

<AddNewBox>

<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
</Style>

</AddNewBox>

<Pager PageSize="20" StyleMode="ComboBox" AllowPaging="True">

<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
</Style>

</Pager>

<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

</HeaderStyleDefault>

<RowSelectorStyleDefault Cursor="Hand">
</RowSelectorStyleDefault>

<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderColor="DimGray" BorderStyle="Solid" BackColor="Silver" Height="450px">
</FrameStyle>

<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

</FooterStyleDefault>

<EditCellStyleDefault BorderWidth="0px" BorderStyle="None">
</EditCellStyleDefault>

<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy">
</SelectedRowStyleDefault>

<RowAlternateStyleDefault Cursor="Hand" BorderColor="Gray" BackColor="LightSteelBlue">
</RowAlternateStyleDefault>

<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">

<Padding Left="3px">
</Padding>

<BorderDetails WidthLeft="0px" WidthTop="0px">
</BorderDetails>

</RowStyleDefault>

</DisplayLayout>

<Bands>
<igtbl:UltraGridBand>
<Columns>
<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="93px" BaseColumnName="ItemNum">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Left">

<Padding Left="2px">
</Padding>

</CellStyle>

<HeaderStyle Height="25px">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="80px" BaseColumnName="ItemName">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Left">

<Padding Left="2px">
</Padding>

</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="변경사유" Key="UpdateReason" Width="85px" BaseColumnName="UpdateReason"></igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="단가구분" Key="UnitCostDistinction" Width="60px" BaseColumnName="UnitCostDistinction">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" Width="90px" BaseColumnName="CompanyName">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="시작공정" Key="BeginProcess" Width="60px" BaseColumnName="BeginProcess">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="종료공정" Key="EndProcess" Width="60px" BaseColumnName="EndProcess">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="기준단가" Key="StandardUnitCost" Width="60px" Format="\ ###,###,##0" BaseColumnName="StandardUnitCost">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="적용시작일" Key="BeginDate" Width="80px" Format="yyyy-MM-dd" BaseColumnName="BeginDate">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" Width="50px" BaseColumnName="UpdatingPerson">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Width="80px" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
</Columns>
</igtbl:UltraGridBand>
</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
								<tr>
									<td align="left">&nbsp;
										<asp:button id="Button1" runat="server" Width="60px" Text="Excel" Height="20px"></asp:button></td>
								</tr>
							</TABLE>
							<igtblexp:UltraWebGridExcelExporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:UltraWebGridExcelExporter>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>