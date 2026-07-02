<%@ Page language="c#" Codebehind="ItemAll.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.Popup.ItemAll" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ItemAll</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<STYLE type="text/css">BODY { SCROLLBAR-ARROW-COLOR: #400040; SCROLLBAR-FACE-COLOR: #ffffff; SCROLLBAR-DARKSHADOW-COLOR: #808080; SCROLLBAR-HIGHLIGHT-COLOR: #ffffff; SCROLLBAR-SHADOW-COLOR: #ffffff; SCROLLBAR-TRACK-COLOR: #ffffff; SCROLLBAR-3DLIGHT-COLOR: #808080 }
		</STYLE>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="buttonface">
		<form id="Form1" method="post" runat="server">
			<FIELDSET style="Z-INDEX: 101; BORDER-BOTTOM: dimgray 2px solid; POSITION: absolute; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; HEIGHT: 480px; BORDER-TOP: dimgray 2px solid; TOP: 8px; BORDER-RIGHT: dimgray 2px solid; LEFT: 8px"
				align="left"><LEGEND>[ 품목정보 ]</LEGEND>
				<asp:Button id="bt_Excel" runat="server" Text="Excel" Height="20px" Width="60px"></asp:Button>
				<TABLE id="Table1" style="HEIGHT: 424px" height="424" width="800">
					<TR>
						<TD width="850" height="10"><FONT face="굴림">
								<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="448px">
									<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
										RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
										HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
										RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect">
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
											<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
										</HeaderStyleDefault>
										<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
											BackColor="Silver" Height="448px"></FrameStyle>
										<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
											<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
										</FooterStyleDefault>
										<ClientSideEvents AfterSelectChangeHandler="UltraWebGrid1_AfterSelectChangeHandler"></ClientSideEvents>
										<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
										<SelectedRowStyleDefault Cursor="Hand" BackColor="#E0E5CD"></SelectedRowStyleDefault>
										<RowAlternateStyleDefault Cursor="Hand" BorderColor="DarkGray" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
										<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
											<Padding Left="3px"></Padding>
											<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
										</RowStyleDefault>
									</DisplayLayout>
									<Bands>
										<igtbl:UltraGridBand>
											<Columns>
												<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" BaseColumnName="ItemNum">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle Height="25px"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" BaseColumnName="ItemDrawNum">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" BaseColumnName="ItemName">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="자산분류" Key="PropertyClassification" BaseColumnName="PropertyClassification">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="단위" Key="Unit" BaseColumnName="Unit">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="규격" Key="Standard" BaseColumnName="Standard">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="과세여부" Key="Texture" Hidden="True" BaseColumnName="Texture">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="부가세율" Key="SupplementaryValueTaxRate" Hidden="True" Format="###,###,###.##"
													BaseColumnName="SupplementaryValueTaxRate">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="기준단가" Key="StandardUnitCost" Format="###,###,###.##" BaseColumnName="StandardUnitCost">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="재고단위" Key="StockUnit" BaseColumnName="StockUnit">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="BOM단위" Key="BOMUnit" Type="DropDownList" BaseColumnName="BOMUnit">
													<ValueList>
														<ValueListItems>
															<igtbl:ValueListItem Key="1" DisplayText="예"></igtbl:ValueListItem>
															<igtbl:ValueListItem Key="0" DisplayText="아니오"></igtbl:ValueListItem>
														</ValueListItems>
													</ValueList>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="구매단위" Key="PurchaseUnit" Type="DropDownList" BaseColumnName="PurchaseUnit">
													<ValueList>
														<ValueListItems>
															<igtbl:ValueListItem Key="1" DisplayText="예"></igtbl:ValueListItem>
															<igtbl:ValueListItem Key="1" DisplayText="아니오"></igtbl:ValueListItem>
														</ValueListItems>
													</ValueList>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="판매단위" Key="SaleUnit" Type="DropDownList" BaseColumnName="SaleUnit">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="자재산출" Key="IOChackable" Type="DropDownList" BaseColumnName="IOChackable">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="재고관리여부" Key="StockManagable" Format="" BaseColumnName="StockManagable">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="검사구분" Key="CheckDistinction" BaseColumnName="CheckDistinction">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="발주방침" Key="OrderPlan" BaseColumnName="OrderPlan">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="품목타입" Key="ItemType" BaseColumnName="ItemType">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="재질" Key="MateralQuality" Format="" BaseColumnName="MateralQuality">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="품목상태" Key="ItemState" Hidden="True" BaseColumnName="ItemState">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="메이커" Key="Maker" Hidden="True" BaseColumnName="Maker">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="품목분류1" Key="ItemClassification1" Format="" BaseColumnName="ItemClassification1">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="품목분류2" Key="ItemClassification2" BaseColumnName="ItemClassification2">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="품목분류3" Key="ItemClassification3" BaseColumnName="ItemClassification3">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="품목분류4" Key="ItemClassification4" BaseColumnName="ItemClassification4">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="규격1" Key="Standard1" BaseColumnName="Standard1">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="단위1" Key="Unit1" BaseColumnName="Unit1">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="규격2" Key="Standard2" BaseColumnName="Standard2">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="단위2" Key="Unit2" BaseColumnName="Unit2">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="규격3" Key="Standard3" Hidden="True" BaseColumnName="Standard3">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="단위3" Key="Unit3" Hidden="True" BaseColumnName="Unit3">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="규격4" Key="Standard4" Hidden="True" BaseColumnName="Standard4">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="단위4" Key="Unit4" Hidden="True" BaseColumnName="Unit4">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="제품중량" Key="ProductWeight" BaseColumnName="ProductWeight">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="소재중량" Key="MaterialWeight" BaseColumnName="MaterialWeight">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="조달기간" Key="SupplyTerm" BaseColumnName="SupplyTerm">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="완성리드타임" Key="CompleteLeadTime" BaseColumnName="CompleteLeadTime"></igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="내외자구분" Key="DomesticImportDistinction" BaseColumnName="DomesticImportDistinction">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="안전재고량" Key="SafetyStockQuantity" Format="###,###,###.##" BaseColumnName="SafetyStockQuantity">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="발주간격수량" Key="OrderIntervalQuantity" Format="###,###,###.##" BaseColumnName="OrderIntervalQuantity">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="최소발주량" Key="MinOrderQuantity" Format="###,###,###.##" BaseColumnName="MinOrderQuantity">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="경화깊이" Key="StiffenDeep" BaseColumnName="StiffenDeep">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="제품열처리사양" Key="ProductHeatTreatmentDescription" BaseColumnName="ProductHeatTreatmentDescription">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="소재열처리사양" Key="MaterialHeatTreatmentDescription" BaseColumnName="MaterialHeatTreatmentDescription">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="소재열처리정도" Key="MaterialHeatTreatmentRequestDegree" BaseColumnName="MaterialHeatTreatmentRequestDegree">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="절단여유" Key="CuttingSpace" BaseColumnName="CuttingSpace">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="관세율" Key="TariffRate" Format="###,###,###.##" BaseColumnName="TariffRate">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="주구매처" Key="MainPurchaseCompany" BaseColumnName="MainPurchaseCompany">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="주외주처" Key="MainOutSideOrderCompany" BaseColumnName="MainOutSideOrderCompany">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="주판매처" Key="MainSaleCompany" BaseColumnName="MainSaleCompany">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="담당자" Key="ChargePerson" BaseColumnName="ChargePerson">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="번호" Key="ItemInfoIndex" Hidden="True" BaseColumnName="ItemInfoIndex">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
												</igtbl:UltraGridColumn>
											</Columns>
										</igtbl:UltraGridBand>
									</Bands>
								</igtbl:ultrawebgrid></FONT></TD>
					</TR>
				</TABLE>
			</FIELDSET>
			<igtbl:ultrawebgrid id="UltraWebGrid2" style="Z-INDEX: 102; POSITION: absolute; TOP: 536px; LEFT: 16px"
				runat="server" Width="800px" Height="448px" Visible="False">
				<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
					RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
					HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
					RowSelectorsDefault="No" Name="UltraWebGrid2" TableLayout="Fixed" CellClickActionDefault="RowSelect">
					<AddNewBox>
						<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

						</Style>
					</AddNewBox>
					<Pager PageSize="19" StyleMode="ComboBox" Alignment="Center" AllowPaging="True">
						<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

						</Style>
					</Pager>
					<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
						<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
					</HeaderStyleDefault>
					<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
						BackColor="Transparent" Height="448px"></FrameStyle>
					<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
						<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
					</FooterStyleDefault>
					<ClientSideEvents AfterSelectChangeHandler="UltraWebGrid1_AfterSelectChangeHandler"></ClientSideEvents>
					<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
					<SelectedRowStyleDefault Cursor="Hand" BackColor="Transparent"></SelectedRowStyleDefault>
					<RowAlternateStyleDefault Cursor="Hand" BorderColor="Gray"></RowAlternateStyleDefault>
					<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid">
						<Padding Left="3px"></Padding>
						<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
					</RowStyleDefault>
				</DisplayLayout>
				<Bands>
					<igtbl:UltraGridBand>
						<Columns>
							<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" BaseColumnName="ItemNum">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
								<HeaderStyle Height="25px"></HeaderStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" BaseColumnName="ItemDrawNum">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" BaseColumnName="ItemName">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="자산분류" Key="PropertyClassification" BaseColumnName="PropertyClassification">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="단위" Key="Unit" BaseColumnName="Unit">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="규격" Key="Standard" BaseColumnName="Standard">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="과세여부" Key="Texture" BaseColumnName="Texture">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="부가세율" Key="SupplementaryValueTaxRate" Format="###,###,###.##" BaseColumnName="SupplementaryValueTaxRate">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="재고단위" Key="StockUnit" BaseColumnName="StockUnit">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="BOM단위" Key="BOMUnit" Type="DropDownList" BaseColumnName="BOMUnit">
								<ValueList>
									<ValueListItems>
										<igtbl:ValueListItem Key="1" DisplayText="예"></igtbl:ValueListItem>
										<igtbl:ValueListItem Key="0" DisplayText="아니오"></igtbl:ValueListItem>
									</ValueListItems>
								</ValueList>
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="구매단위" Key="PurchaseUnit" Type="DropDownList" BaseColumnName="PurchaseUnit">
								<ValueList>
									<ValueListItems>
										<igtbl:ValueListItem Key="1" DisplayText="예"></igtbl:ValueListItem>
										<igtbl:ValueListItem Key="1" DisplayText="아니오"></igtbl:ValueListItem>
									</ValueListItems>
								</ValueList>
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="판매단위" Key="SaleUnit" Type="DropDownList" BaseColumnName="SaleUnit">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="자재산출" Key="IOChackable" Type="DropDownList" BaseColumnName="IOChackable">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="재고관리여부" Key="StockManagable" Format="" BaseColumnName="StockManagable">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="검사구분" Key="CheckDistinction" BaseColumnName="CheckDistinction">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="발주방침" Key="OrderPlan" BaseColumnName="OrderPlan">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="품목타입" Key="ItemType" BaseColumnName="ItemType">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="재질" Key="MateralQuality" Format="" BaseColumnName="MateralQuality">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="품목상태" Key="ItemState" BaseColumnName="ItemState">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="메이커" Key="Maker" BaseColumnName="Maker">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="품목분류1" Key="ItemClassification1" Format="" BaseColumnName="ItemClassification1">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="품목분류2" Key="ItemClassification2" BaseColumnName="ItemClassification2">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="품목분류3" Key="ItemClassification3" BaseColumnName="ItemClassification3">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="품목분류4" Key="ItemClassification4" BaseColumnName="ItemClassification4">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="규격1" Key="Standard1" BaseColumnName="Standard1">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="단위1" Key="Unit1" BaseColumnName="Unit1">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="규격2" Key="Standard2" BaseColumnName="Standard2">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="단위2" Key="Unit2" BaseColumnName="Unit2">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="규격3" Key="Standard3" Hidden="True" BaseColumnName="Standard3">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="단위3" Key="Unit3" Hidden="True" BaseColumnName="Unit3">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="규격4" Key="Standard4" Hidden="True" BaseColumnName="Standard4">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="단위4" Key="Unit4" Hidden="True" BaseColumnName="Unit4">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="제품중량" Key="ProductWeight" BaseColumnName="ProductWeight">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="소재중량" Key="MaterialWeight" BaseColumnName="MaterialWeight">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="조달기간" Key="SupplyTerm" BaseColumnName="SupplyTerm">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="완성리드타임" Key="CompleteLeadTime" BaseColumnName="CompleteLeadTime"></igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="내외자구분" Key="DomesticImportDistinction" BaseColumnName="DomesticImportDistinction">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="안전재고량" Key="SafetyStockQuantity" Format="###,###,###.##" BaseColumnName="SafetyStockQuantity">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="발주간격수량" Key="OrderIntervalQuantity" Format="###,###,###.##" BaseColumnName="OrderIntervalQuantity">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="최소발주량" Key="MinOrderQuantity" Format="###,###,###.##" BaseColumnName="MinOrderQuantity">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="경화깊이" Key="StiffenDeep" BaseColumnName="StiffenDeep">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="제품열처리사양" Key="ProductHeatTreatmentDescription" BaseColumnName="ProductHeatTreatmentDescription">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="소재열처리사양" Key="MaterialHeatTreatmentDescription" BaseColumnName="MaterialHeatTreatmentDescription">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="소재열처리정도" Key="MaterialHeatTreatmentRequestDegree" BaseColumnName="MaterialHeatTreatmentRequestDegree">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="절단여유" Key="CuttingSpace" BaseColumnName="CuttingSpace">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="관세율" Key="TariffRate" Format="###,###,###.##" BaseColumnName="TariffRate">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="기준단가" Key="StandardUnitCost" Format="###,###,###.##" BaseColumnName="StandardUnitCost">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="주구입처" Key="MainPurchaseCompany" BaseColumnName="MainPurchaseCompany">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="주외주처" Key="MainOutSideOrderCompany" BaseColumnName="MainOutSideOrderCompany">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="주판매처" Key="MainSaleCompany" BaseColumnName="MainSaleCompany">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="담당자" Key="ChargePerson" BaseColumnName="ChargePerson">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="번호" Key="ItemInfoIndex" Hidden="True" BaseColumnName="ItemInfoIndex">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
							</igtbl:UltraGridColumn>
						</Columns>
					</igtbl:UltraGridBand>
				</Bands>
			</igtbl:ultrawebgrid>
			<igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter></form>
	</body>
</HTML>
