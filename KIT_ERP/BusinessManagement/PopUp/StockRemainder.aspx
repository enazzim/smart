<%@ Page language="c#" Codebehind="StockRemainder.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BusinessManagement.PopUp.StockRemainder" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>StockRemainder</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../StyleSheet2.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; Z-INDEX: 101; LEFT: 8px; BORDER-LEFT: dimgray 2px solid; WIDTH: 808px; BORDER-BOTTOM: dimgray 2px solid; POSITION: absolute; TOP: 8px; HEIGHT: 1.96%"
				align="absBottom"><LEGEND style="FONT-SIZE: 10pt" align="left">[검색결과]&nbsp;
				</LEGEND>
				<TABLE id="Table2" style="WIDTH: 100%; HEIGHT: 136px" cellSpacing="0" cellPadding="0" border="0">
					<TR>
						<TD vAlign="top" align="center">
							<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" DESIGNTIMEDRAGDROP="20" Width="800px" Height="144px">
								<DisplayLayout AutoGenerateColumns="False" AllowSortingDefault="Yes" RowHeightDefault="20px" Version="3.00"
									ViewType="Hierarchical" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti"
									BorderCollapseDefault="Separate" AllowColSizingDefault="Free" Name="UltraWebGrid1" TableLayout="Fixed"
									CellClickActionDefault="RowSelect">
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
									<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray" Height="25px">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</HeaderStyleDefault>
									<RowSelectorStyleDefault BackColor="LightGray"></RowSelectorStyleDefault>
									<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DimGray"
										BorderStyle="Solid" BackColor="Silver" Height="144px"></FrameStyle>
									<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</FooterStyleDefault>
									<ClientSideEvents ColumnHeaderClickHandler="uwgBO_HT_ColumnHeaderClickHandler"></ClientSideEvents>
									<RowExpAreaStyleDefault BackColor="White"></RowExpAreaStyleDefault>
									<SelectedHeaderStyleDefault BackColor="Silver"></SelectedHeaderStyleDefault>
									<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
									<SelectedGroupByRowStyleDefault BackColor="Silver"></SelectedGroupByRowStyleDefault>
									<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
									<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
									<RowStyleDefault Cursor="Hand" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DarkGray"
										BorderStyle="Solid" BackColor="#EBEFF6">
										<Padding Left="3px"></Padding>
										<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
									</RowStyleDefault>
								</DisplayLayout>
								<Bands>
									<igtbl:UltraGridBand HeaderClickAction="SortMulti" AddButtonCaption="Stock" AllowSorting="Yes" Key="Stock"
										CellClickAction="RowSelect" AllowColSizing="Free" AllowColumnMoving="OnServer" RowSelectors="No">
										<HeaderStyle Cursor="Hand"></HeaderStyle>
										<RowAlternateStyle BackColor="LightSteelBlue"></RowAlternateStyle>
										<Columns>
											<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" HeaderClickAction="Select" BaseColumnName="ItemNum">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" HeaderClickAction="SortMulti" BaseColumnName="ItemDrawNum">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="생산창고재고량" Key="PS_MTStock" Format="###,###,###,0" HeaderClickAction="SortMulti"
												BaseColumnName="PS_MTStock">
												<CellStyle HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="영업창고재고량" Key="BS_MTStock" Format="###,###,###,0" HeaderClickAction="SortMulti"
												BaseColumnName="BS_MTStock">
												<CellStyle HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="납품창고재고량" Key="DS_MTStock" Format="###,###,###,0" HeaderClickAction="SortMulti"
												BaseColumnName="DS_MTStock">
												<CellStyle HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="생산의뢰량" Key="PR_HTStock" Format="###,###,###,0" HeaderClickAction="SortMulti"
												BaseColumnName="PR_HTStock">
												<CellStyle HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="작업계획량" Key="WDWP_HTStock" Format="###,###,###,0" HeaderClickAction="SortMulti"
												BaseColumnName="WDWP_HTStock">
												<CellStyle HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="외주재고량" Key="OO_HTStock" Format="###,###,###,0" HeaderClickAction="SortMulti"
												BaseColumnName="OO_HTStock">
												<CellStyle HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수주잔량" Key="RO_HTStock" Format="###,###,###,0" BaseColumnName="RO_HTStock">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="잉여재고량" Key="RemainderQuantity" Format="###,###,###,0" BaseColumnName="RemainderQuantity">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
										</Columns>
										<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
											<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
										</RowTemplateStyle>
										<RowStyle Width="1500px"></RowStyle>
										<RowSelectorStyle BackColor="ActiveBorder"></RowSelectorStyle>
										<RowEditTemplate>
											<BR>
											<P align="center"><INPUT id="igtbl_reOkBtn" style="WIDTH: 50px" onclick="igtbl_gRowEditButtonClick(event);"
													type="button" value="OK" name="igtbl_reOkBtn">&nbsp; <INPUT id="igtbl_reCancelBtn" style="WIDTH: 50px" onclick="igtbl_gRowEditButtonClick(event);"
													type="button" value="Cancel" name="igtbl_reCancelBtn"></P>
										</RowEditTemplate>
									</igtbl:UltraGridBand>
									<igtbl:UltraGridBand HeaderClickAction="SortMulti" AddButtonCaption="RO_HT" AllowSorting="Yes" Key="RO_HT"
										CellClickAction="RowSelect" AllowColSizing="Free" AllowColumnMoving="OnServer" RowSelectors="No"
										Expandable="Yes">
										<HeaderStyle Cursor="Hand"></HeaderStyle>
										<RowAlternateStyle Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyle>
										<Columns>
											<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" HeaderClickAction="SortMulti" BaseColumnName="ItemDrawNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" HeaderClickAction="SortMulti" BaseColumnName="CompanyName">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="BusinessRegistrationNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="자산분류" Key="PropertyClassification" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="PropertyClassification">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="생산의뢰여부" Key="ProductionRequestDivision" Hidden="True" BaseColumnName="ProductionRequestDivision">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수주일자" Key="ReceivingOrderDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="ReceivingOrderDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="총수주량" Key="TotalReceiveingOrderQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
												BaseColumnName="TotalReceiveingOrderQuantity">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="납품잔량" Key="RemainderQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
												BaseColumnName="RemainderQuantity">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="1차납기요구량" Key="DeliveryRequestQuantity1" Format="###,###,###.##" HeaderClickAction="SortMulti"
												BaseColumnName="DeliveryRequestQuantity1">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="1차납기요구일" Key="DeliveryRequestDate1" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="DeliveryRequestDate1">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="2차납기요구량" Key="DeliveryRequestQuantity2" Format="###,###,###.##" HeaderClickAction="SortMulti"
												BaseColumnName="DeliveryRequestQuantity2">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="2차납기요구일" Key="DeliveryRequestDate2" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="DeliveryRequestDate2">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="3차납기요구량" Key="DeliveryRequestQuantity3" Format="###,###,###.##" HeaderClickAction="SortMulti"
												BaseColumnName="DeliveryRequestQuantity3">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="3차납기요구일" Key="DeliveryRequestDate3" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="DeliveryRequestDate3">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="4차납기요구량" Key="DeliveryRequestQuantity4" Format="###,###,###.##" HeaderClickAction="SortMulti"
												BaseColumnName="DeliveryRequestQuantity4">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="4차납기요구일" Key="DeliveryRequestDate4" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="DeliveryRequestDate4">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="5차납기요구량" Key="DeliveryRequestQuantity5" Format="###,###,###.##" HeaderClickAction="SortMulti"
												BaseColumnName="DeliveryRequestQuantity5">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="5차납기요구일" Key="DeliveryRequestDate5" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="DeliveryRequestDate5">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
												BaseColumnName="ApplyUnitCost">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="총금액" Key="TotalCost" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
												BaseColumnName="TotalCost">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="출고수량" Key="OutStorehouseQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
												BaseColumnName="OutStorehouseQuantity">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="적합수량" Key="SuitabilityQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
												BaseColumnName="SuitabilityQuantity">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="미검수량" Key="UnInspectionQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
												BaseColumnName="UnInspectionQuantity">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="발주번호" Key="OrderNum" HeaderClickAction="SortMulti" BaseColumnName="OrderNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="납품장소" Key="DeliveryPlace" HeaderClickAction="SortMulti" BaseColumnName="DeliveryPlace">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="볼륨번호" Key="VolumNum" HeaderClickAction="SortMulti" BaseColumnName="VolumNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" HeaderClickAction="SortMulti" BaseColumnName="ProgressCondition">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPerson">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="RegistrationPersonID">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="RegistrationDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" HeaderClickAction="SortMulti" BaseColumnName="UpdatingPerson">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="UpdatingPersonID">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="UpdatingDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="번호" Key="ReceivingOrderHistoryIndex" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="ReceivingOrderHistoryIndex">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
										</Columns>
										<RowSelectorStyle BackColor="Linen"></RowSelectorStyle>
									</igtbl:UltraGridBand>
								</Bands>
							</igtbl:ultrawebgrid></TD>
					</TR>
					<TR>
					</TR>
				</TABLE>
			</FIELDSET>
		</form>
	</body>
</HTML>
