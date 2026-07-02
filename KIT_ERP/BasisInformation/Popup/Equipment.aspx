<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="Equipment.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.Popup.Equipment" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Equipment</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../StyleSheet2.css" type="text/css" rel="stylesheet">
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
								<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="450px" Width="800px">
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
										<SelectedRowStyleDefault BackColor="#E0E5CD"></SelectedRowStyleDefault>
										<RowAlternateStyleDefault Cursor="Hand" BorderColor="DarkGray" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
										<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
											<Padding Left="3px"></Padding>
											<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
										</RowStyleDefault>
									</DisplayLayout>
									<Bands>
										<igtbl:UltraGridBand>
											<Columns>
												<igtbl:UltraGridColumn HeaderText="설비번호" Key="EquipmentNum" HeaderClickAction="SortMulti" BaseColumnName="EquipmentNum">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="설비명" Key="EquipmentName" HeaderClickAction="SortMulti" BaseColumnName="EquipmentName">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="설비분류" Key="EquipmentClassification" HeaderClickAction="SortMulti" BaseColumnName="EquipmentClassification">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="규격" Key="Standard" HeaderClickAction="SortMulti" BaseColumnName="Standard">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="Capacity" Key="Capacity" Format="###,###,###.##" HeaderClickAction="SortMulti"
													BaseColumnName="Capacity">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="전력량" Key="ElectricCapacity" Format="###,###,###.##" HeaderClickAction="SortMulti"
													BaseColumnName="ElectricCapacity">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="시간당 사용료" Key="UnitTimeUseCost" Format="###,###,###.##" HeaderClickAction="SortMulti"
													BaseColumnName="UnitTimeUseCost">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="단위" Key="Unit" Format="" HeaderClickAction="SortMulti" BaseColumnName="Unit">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="WC명" Key="WCName" HeaderClickAction="SortMulti" BaseColumnName="WCName">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="투입인원" Key="InjectionStaffNum" Format="###,###,###.##" HeaderClickAction="SortMulti"
													BaseColumnName="InjectionStaffNum">
													<ValueList>
														<ValueListItems>
															<igtbl:ValueListItem Key="1" DisplayText="예"></igtbl:ValueListItem>
															<igtbl:ValueListItem Key="0" DisplayText="아니오"></igtbl:ValueListItem>
														</ValueListItems>
													</ValueList>
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="위치" Key="Location" Type="DropDownList" HeaderClickAction="SortMulti"
													BaseColumnName="Location">
													<ValueList>
														<ValueListItems>
															<igtbl:ValueListItem Key="1" DisplayText="예"></igtbl:ValueListItem>
															<igtbl:ValueListItem Key="1" DisplayText="아니오"></igtbl:ValueListItem>
														</ValueListItems>
													</ValueList>
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="차지면적" Key="OccupancyArea" Format="###,###,###.##" HeaderClickAction="SortMulti"
													BaseColumnName="OccupancyArea">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="구입일" Key="BuyingDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
													BaseColumnName="BuyingDate">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="구입가격" Key="BuyingCost" Format="###,###,###.##" HeaderClickAction="SortMulti"
													BaseColumnName="BuyingCost">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="기기고유번호" Key="InstrumentNum" HeaderClickAction="SortMulti" BaseColumnName="InstrumentNum">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="설비상태" Key="EquipmentState" HeaderClickAction="SortMulti" BaseColumnName="EquipmentState">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="검교정일" Key="CheckDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
													BaseColumnName="CheckDate">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="유효기간" Key="ValidPeriod" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
													BaseColumnName="ValidPeriod">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="검교정기관" Key="CheckAgency" HeaderClickAction="SortMulti" BaseColumnName="CheckAgency">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="가동준비시간" Key="ReadyTime" Format="###,###,###.##" HeaderClickAction="SortMulti"
													BaseColumnName="ReadyTime">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="Cavity" Key="Cavity" Format="###,###,###.##" HeaderClickAction="SortMulti"
													BaseColumnName="Cavity">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="내용년수" Key="ValidityYear" Format="#########" HeaderClickAction="SortMulti"
													BaseColumnName="ValidityYear">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="설계Shot" Key="DesignShot" Format="###,###,###.##" HeaderClickAction="SortMulti"
													BaseColumnName="DesignShot">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="초기Shot" Key="FirstShot" Format="###,###,###.##" HeaderClickAction="SortSingle"
													BaseColumnName="FirstShot">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="누계Shot" Key="TotalShot" Format="###,###,###.##" HeaderClickAction="SortMulti"
													BaseColumnName="TotalShot">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="작업Shot" Key="WorkShot" Format="###,###,###.##" HeaderClickAction="SortMulti"
													BaseColumnName="WorkShot">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="관리주기1" Key="ManagePeriod1" HeaderClickAction="SortMulti" BaseColumnName="ManagePeriod1">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="관리일1" Key="ManageDate1" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
													BaseColumnName="ManageDate1">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="관리주기2" Key="ManagePeriod2" HeaderClickAction="SortMulti" BaseColumnName="ManagePeriod2">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="관리일2" Key="ManageDate2" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
													BaseColumnName="ManageDate2">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="관리주기3" Key="ManagePeriod3" HeaderClickAction="SortMulti" BaseColumnName="ManagePeriod3">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="관리일3" Key="ManageDate3" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
													BaseColumnName="ManageDate3">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="관리주기4" Key="ManagePeriod4" HeaderClickAction="SortMulti" BaseColumnName="ManagePeriod4">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="관리일4" Key="ManageDate4" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
													BaseColumnName="ManageDate4">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="번호" Key="EquipmentInfoIndex" Hidden="True" BaseColumnName="EquipmentInfoIndex">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
											</Columns>
										</igtbl:UltraGridBand>
									</Bands>
								</igtbl:ultrawebgrid></FONT></TD>
					</TR>
				</TABLE>
				<igtblexp:UltraWebGridExcelExporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:UltraWebGridExcelExporter></FIELDSET>
		</form>
	</body>
</HTML>
