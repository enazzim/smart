<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="Bom.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.Popup.Bom" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Bom</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../StyleSheet2.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<FONT face="굴림">
				<TABLE id="Table2" style="POSITION: absolute; TOP: 0px; LEFT: 0px" cellSpacing="0" cellPadding="0"
					width="800" border="0">
					<TR>
						<TD height="20">&nbsp;
							<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="450px" Width="800px">
								<DisplayLayout ColFootersVisibleDefault="Yes" StationaryMargins="Header" AutoGenerateColumns="False"
									RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
									HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
									RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect">
									<AddNewBox>
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										</Style>
									</AddNewBox>
									<Pager>
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										</Style>
									</Pager>
									<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray" Height="25px">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</HeaderStyleDefault>
									<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="Verdana" BorderColor="DimGray"
										BorderStyle="Solid" BackColor="Silver" Height="450px"></FrameStyle>
									<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</FooterStyleDefault>
									<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
									<SelectedRowStyleDefault Cursor="Hand" Font-Bold="True" BorderColor="Gray" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
									<RowAlternateStyleDefault Cursor="Hand"></RowAlternateStyleDefault>
									<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
										<Padding Left="3px"></Padding>
										<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
									</RowStyleDefault>
								</DisplayLayout>
								<Bands>
									<igtbl:UltraGridBand>
										<Columns>
											<igtbl:UltraGridColumn HeaderText="자산분류" Key="PropertyClassification" Width="80px" BaseColumnName="PropertyClassification"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="레벨" Key="Lvl" Width="30px" BaseColumnName="Lvl"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="품목분류1" Key="ItemClassification1" BaseColumnName="ItemClassification1"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="산출여부" Key="IOChackable" Width="55px" BaseColumnName="IOChackable"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" BaseColumnName="ItemNum">
												<HeaderStyle Height="25px"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" BaseColumnName="ItemDrawNum"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" BaseColumnName="ItemName"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="단위" Key="Unit" Width="50px" BaseColumnName="Unit">
												<CellStyle VerticalAlign="Middle"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="규격" Key="Standard" BaseColumnName="Standard"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="재질" Key="MateralQuality" Width="70px" BaseColumnName="MateralQuality">
												<CellStyle VerticalAlign="Middle"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="구매거래처" Key="CompanyName" BaseColumnName="CompanyName">
												<CellStyle VerticalAlign="Middle"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="소요량" Key="NeedQuantity" Width="50px" Format="###,###,###.##" BaseColumnName="NeedQuantity">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
													<Padding Right="4px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="구매단가" Key="StandardUnitCost" Width="70px" Format="###,###,###" BaseColumnName="StandardUnitCost">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
													<Padding Right="4px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="재고량" Key="Quantity" Width="80px" Format="###,###,###" BaseColumnName="Quantity">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
													<Padding Right="4px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="미입고량" Key="RemainQuantity" Format="###,###,###" BaseColumnName="RemainQuantity">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
													<Padding Right="4px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="공정작업장" Key="PSIWC" BaseColumnName="PSIWC"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="표준작업장" Key="WSIWC" BaseColumnName="WSIWC"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="외주거래처" Key="OutSideCompany" BaseColumnName="OutSideCompany"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="외주단가" Key="OutSideCost" Format="###,###,###.##" BaseColumnName="OutSideCost">
												<CellStyle HorizontalAlign="Right">
													<Padding Right="4px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="외주창고량" Key="OutSideQuantity" Width="80px" Format="###,###,###.##" BaseColumnName="OutSideQuantity">
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
						<TD height="10"></TD>
					</TR>
					<TR>
						<TD>&nbsp;
							<asp:Button id="Button1" runat="server" Text="Excel"></asp:Button>
							<igtblexp:UltraWebGridExcelExporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:UltraWebGridExcelExporter></TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
