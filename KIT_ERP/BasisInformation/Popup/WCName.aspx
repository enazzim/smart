<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1" %>
<%@ Page language="c#" Codebehind="WCName.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.Popup.WCName" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WCName</title>
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
				align="left"><LEGEND>[ 작업장정보 ]</LEGEND>
				<TABLE id="Table1" width="800" cellSpacing="0" cellPadding="0">
					<tr>
						<td>
							<asp:Button id="bt_Excel" runat="server" Width="60px" Height="20px" Text="Excel"></asp:Button>
							<igtblexp:UltraWebGridExcelExporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:UltraWebGridExcelExporter></td>
					</tr>
				</TABLE>
				<TABLE id="Table1" height="450" width="800" cellSpacing="0" cellPadding="0">
					<TR>
						<TD width="800" height="10">
							<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="450px">
								<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
									RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
									HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
									RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect">
									<AddNewBox>
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

										</Style>
									</AddNewBox>
									<Pager PageSize="20" Alignment="Center" AllowPaging="True">
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

										</Style>
									</Pager>
									<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</HeaderStyleDefault>
									<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
										BackColor="Silver" Height="450px"></FrameStyle>
									<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</FooterStyleDefault>
									<ClientSideEvents AfterSelectChangeHandler="UltraWebGrid1_AfterSelectChangeHandler"></ClientSideEvents>
									<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
									<SelectedRowStyleDefault Cursor="Hand" BackColor="#E0E5CD"></SelectedRowStyleDefault>
									<RowAlternateStyleDefault Cursor="Hand" BorderColor="DarkGray" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
									<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EDEFF6">
										<Padding Left="3px"></Padding>
										<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
									</RowStyleDefault>
								</DisplayLayout>
								<Bands>
									<igtbl:UltraGridBand>
										<Columns>
											<igtbl:UltraGridColumn HeaderText="작업장명" Key="WCName" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="WCName">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle Height="25px"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="대표공정" Key="MainProcessCode" Width="150px" HeaderClickAction="SortMulti"
												BaseColumnName="MainProcessCode">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="보유인원" Key="RetentionStaff" Width="100px" HeaderClickAction="SortMulti"
												BaseColumnName="RetentionStaff">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="현상태" Key="State" Width="100px" HeaderClickAction="SortMulti" BaseColumnName="State">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="Capa구분" Key="CapacityDistinction" Width="100px" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="CapacityDistinction">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="가동시간" Key="OperationTime" Width="100px" Format="###,###,###.##" HeaderClickAction="SortMulti"
												BaseColumnName="OperationTime">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="전력용량" Key="ElectricCapacity" Width="100px" Format="###,###,###.##" HeaderClickAction="SortMulti"
												BaseColumnName="ElectricCapacity">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="시간당사용료" Key="UnitTimeUseCost" Width="100px" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="UnitTimeUseCost">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="작업장정보번호" Key="WCInfoIndex" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="WCInfoIndex">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
										</Columns>
									</igtbl:UltraGridBand>
								</Bands>
							</igtbl:ultrawebgrid></TD>
					</TR>
				</TABLE>
		</form>
		</FIELDSET>
	</body>
</HTML>
