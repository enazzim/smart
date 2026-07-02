<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="UnitCost.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.Popup.UnitCost" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>UnitCost</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<STYLE type="text/css">BODY { SCROLLBAR-FACE-COLOR: #ffffff; SCROLLBAR-HIGHLIGHT-COLOR: #ffffff; SCROLLBAR-SHADOW-COLOR: #ffffff; SCROLLBAR-3DLIGHT-COLOR: #808080; SCROLLBAR-ARROW-COLOR: #400040; SCROLLBAR-TRACK-COLOR: #ffffff; SCROLLBAR-DARKSHADOW-COLOR: #808080 }
	</STYLE>
</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="buttonface">
		<form id="Form1" method="post" runat="server">
			<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; Z-INDEX: 101; LEFT: 8px; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; POSITION: absolute; TOP: 8px; HEIGHT: 450px"
				align="left"><LEGEND>[ 단가정보 ]</LEGEND>
				<asp:Button id="bt_Excel" runat="server" Height="20px" Width="60px" Text="Excel"></asp:Button>
				<igtblexp:UltraWebGridExcelExporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:UltraWebGridExcelExporter>
				<table WIDTH="800" HEIGHT="450">
					<tr>
						<td WIDTH="850" HEIGHT="10">
							<igtbl:UltraWebGrid id="UltraWebGrid1" runat="server" Width="800px" Height="450px">
<DisplayLayout AutoGenerateColumns="False" AllowSortingDefault="OnClient" RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1" CellClickActionDefault="RowSelect">

<AddNewBox>

<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

</Style>

</AddNewBox>

<Pager PageSize="19" StyleMode="ComboBox" AllowPaging="True">

<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

</Style>

</Pager>

<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

</HeaderStyleDefault>

<RowSelectorStyleDefault Cursor="Hand">
</RowSelectorStyleDefault>

<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid" BackColor="Silver" Height="450px">
</FrameStyle>

<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

</FooterStyleDefault>

<ClientSideEvents AfterSelectChangeHandler="UltraWebGrid1_AfterSelectChangeHandler">
</ClientSideEvents>

<EditCellStyleDefault BorderWidth="0px" BorderStyle="None">
</EditCellStyleDefault>

<SelectedRowStyleDefault Cursor="Hand" BackColor="#E0E5CD">
</SelectedRowStyleDefault>

<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue">
</RowAlternateStyleDefault>

<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EDEFF6">

<Padding Left="3px">
</Padding>

<BorderDetails WidthLeft="0px" WidthTop="0px">
</BorderDetails>

</RowStyleDefault>

</DisplayLayout>

<Bands>
<igtbl:UltraGridBand>
<Columns>
<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle Height="25px">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" HeaderClickAction="SortMulti" BaseColumnName="ItemDrawNum">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="단가구분" Key="UnitCostDistinction" HeaderClickAction="SortMulti" BaseColumnName="UnitCostDistinction">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" HeaderClickAction="SortMulti" BaseColumnName="CompanyName">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="BusinessRegistrationNum">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="시작공정" Key="BeginProcess" HeaderClickAction="SortMulti" BaseColumnName="BeginProcess">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="종료공정" Key="EndProcess" HeaderClickAction="SortMulti" BaseColumnName="EndProcess">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="발주비율" Key="OrderRate" Format="###,###,###.##" HeaderClickAction="SortMulti" BaseColumnName="OrderRate">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="기준단가" Key="StandardUnitCost" Format="\ ###,###,##0" HeaderClickAction="SortMulti" BaseColumnName="StandardUnitCost">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="할인단가" Key="DiscountUnitCost" Format="\ ###,###,##0" HeaderClickAction="SortMulti" BaseColumnName="DiscountUnitCost">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="적용시작일" Key="BeginDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti" BaseColumnName="BeginDate">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="적용종료일" Key="EndDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti" BaseColumnName="EndDate">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="레코드상태" Key="RecodingState" Hidden="True" BaseColumnName="RecodingState">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPerson">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti" BaseColumnName="RegistrationDate">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" HeaderClickAction="SortMulti" BaseColumnName="UpdatingPerson">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti" BaseColumnName="UpdatingDate">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="번호" Key="UnitCostInfoIndex" BaseColumnName="UnitCostInfoIndex">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="시작" Key="BeginCode" Hidden="True" BaseColumnName="BeginCode"></igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="종료" Key="EndCode" Hidden="True" BaseColumnName="EndCode"></igtbl:UltraGridColumn>
</Columns>
</igtbl:UltraGridBand>
</Bands>
							</igtbl:UltraWebGrid>
						</td>
					</tr>
				</table>
			</FIELDSET>
		</form>
	</body>
</HTML>
