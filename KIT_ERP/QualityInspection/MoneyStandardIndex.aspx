<%@ Register TagPrefix="igchart" Namespace="Infragistics.WebUI.UltraWebChart" Assembly="Infragistics.WebUI.UltraWebChart.v4.1, Version=4.1.20042.36, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igchartprop" Namespace="Infragistics.UltraChart.Resources.Appearance" Assembly="Infragistics.UltraChart.Resources.v4.1, Version=4.1.20042.36, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="MoneyStandardIndex.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ManagementInfomation.MoneyStandardIndex" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BizPlanResultIndex</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" bgColor="#f7f6f6" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" style="LEFT: 10px; POSITION: absolute; TOP: 10px" cellSpacing="0" cellPadding="0"
				width="800" border="0">
				<TR>
					<TD style="HEIGHT: 96px" height="96">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 100%; 90BORDER-RIGHT: black 1px solid"
							align="absBottom"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색조건 ]
							</LEGEND>
							<TABLE id="Table1" height="50" cellSpacing="0" cellPadding="0" width="800" align="center"
								border="0">
								<TR>
									<TD align="right" width="70" height="30">품목분류1
									</TD>
									<TD width="100" height="30"><asp:dropdownlist id="ddlItemClassification1" runat="server" Font-Size="9pt" BackColor="#eeeee9" Height="20px"
											Width="120px"></asp:dropdownlist></TD>
									<TD align="right" width="70" height="30">품목분류2</TD>
									<TD align="left" width="100" colSpan="2" height="30"><asp:dropdownlist id="ddlItemClassification2" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Height="20px"
											Width="120px"></asp:dropdownlist></TD>
									<TD align="right" width="70" height="30">품목분류3</TD>
									<TD align="left" width="100" height="30"><asp:dropdownlist id="ddlItemClassification3" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Height="20px"
											Width="120px"></asp:dropdownlist></TD>
									<TD align="right" width="70" height="30">품목분류4&nbsp;</TD>
									<TD vAlign="middle" align="left" colSpan="3" height="30"><asp:dropdownlist id="ddlItemClassification4" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Height="20px"
											Width="120px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD align="right" width="80" height="30">거래처 명&nbsp;
									</TD>
									<TD width="100" height="30">
										<igcmbo:webcombo id="wcCompanyName" runat="server" Width="120px" Height="20px" BackColor="#EEEEE9"
											ForeColor="Black" Version="3.00" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray" SelBackColor="10, 36, 106"
											SelectedIndex="-1" SelForeColor="White" DropImageXP2="/ig_common/WebGrid3/ig_cmboDown2.bmp"
											DropImageXP1="/ig_common/WebGrid3/ig_cmboDown1.bmp" Editable="True">
											<DropDownLayout DropdownWidth="365px" BorderCollapse="Separate" RowSelectors="No" RowHeightDefault="20px"
												HeaderClickAction="Select" DropdownHeight="350px" TableLayout="Fixed" StationaryMargins="Header">
												<RowStyle BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="White">
													<Padding Left="5px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyle>
												<SelectedRowStyle ForeColor="White" BackColor="#0A246A"></SelectedRowStyle>
												<HeaderStyle BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyle>
												<FrameStyle Width="100%" Cursor="Default" BorderWidth="2px" Font-Size="10pt" Font-Names="Verdana"
													BorderStyle="Ridge" BackColor="Silver" Height="100%"></FrameStyle>
											</DropDownLayout>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igcmbo:webcombo></TD>
									<TD align="right" width="80" height="30">구분&nbsp;</TD>
									<TD align="left" width="100" colSpan="2" height="30"><asp:dropdownlist id="ddlDIV" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Height="20px" Width="120px">
											<asp:ListItem Value="전체" Selected="True">전체</asp:ListItem>
											<asp:ListItem Value="상품">사내 부적합</asp:ListItem>
											<asp:ListItem Value="원자재">구매 부적합</asp:ListItem>
											<asp:ListItem Value="외주">외주 부적합</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD align="right" width="80" height="30"></TD>
									<TD align="right" colSpan="5" height="30"><asp:button id="Button1" runat="server" Height="20px" Width="60px" Text="초기화"></asp:button>&nbsp;
										<asp:button id="btnSearch" runat="server" Height="20px" Width="60px" Text="검  색"></asp:button>&nbsp;&nbsp;
									</TD>
								</TR>
								<TR>
									<TD align="right" height="8" colSpan="11">&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD>
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 100%; 90BORDER-RIGHT: black 1px solid"
							align="middle"><BR>
							<LEGEND style="FONT-SIZE: 10pt" align="left">
								[ 검색결과 ]
							</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD vAlign="top" align="center"><igchart:ultrachart id="UltraChart1" runat="server" BackColor="WhiteSmoke" Height="350px" Width="796px"
											ForeColor="#E0E0E0" EnableViewState="False" BackgroundImageStyle="StretchedFit" CrossHairColor="SlateGray" DoughnutChart-InnerRadius="50"
											DoughnutChart-RadiusFactor="100" DoughnutChart-BreakDistancePercentage="10" DoughnutChart-StartAngle="0" DoughnutChart-BreakAllSlices="True"
											DoughnutChart-PieThickness="0" DoughnutChart-ColumnIndex="0" DoughnutChart-BreakOthersSlice="False" DoughnutChart-OthersCategoryPercent="0"
											DoughnutChart-BreakAlternatingSlices="True" ChartType="StackColumnChart" EnableCrossHair="False" EmptyChartText=" " Transform3D-YRotation="12"
											Transform3D-XRotation="28" Transform3D-Scale="100" Transform3D-ZRotation="0">
											<ColumnChart SeriesSpacing="1" ColumnSpacing="1" NullHandling="Zero"></ColumnChart>
											<ColorModel ColorBegin="Yellow" ColorEnd="Blue" AlphaLevel="199" ModelStyle="CustomSkin" Grayscale="False"
												Scaling="Decreasing">
												<Skin ApplyRowWise="True">
													<PEs>
														<igchartprop:PaintElement Fill="255, 192, 192" StrokeOpacity="255" FillGradientStyle="None" ElementType="SolidFill"
															FillOpacity="255" FillStopColor="Transparent" Hatch="None" FillStopOpacity="255" ImageWrapMode="Tile"
															ImageFitStyle="StretchedFit" StrokeWidth="1" Stroke="Transparent"></igchartprop:PaintElement>
														<igchartprop:PaintElement Fill="255, 192, 128" StrokeOpacity="255" FillGradientStyle="None" ElementType="SolidFill"
															FillOpacity="255" FillStopColor="Transparent" Hatch="None" FillStopOpacity="255" ImageWrapMode="Tile"
															ImageFitStyle="StretchedFit" StrokeWidth="1" Stroke="Transparent"></igchartprop:PaintElement>
														<igchartprop:PaintElement Fill="255, 255, 128" StrokeOpacity="255" FillGradientStyle="None" ElementType="SolidFill"
															FillOpacity="255" FillStopColor="Transparent" Hatch="None" FillStopOpacity="255" ImageWrapMode="Tile"
															ImageFitStyle="StretchedFit" StrokeWidth="1" Stroke="Transparent"></igchartprop:PaintElement>
														<igchartprop:PaintElement Fill="128, 255, 128" StrokeOpacity="255" FillGradientStyle="None" ElementType="SolidFill"
															FillOpacity="255" FillStopColor="Transparent" Hatch="None" FillStopOpacity="255" ImageWrapMode="Tile"
															ImageFitStyle="StretchedFit" StrokeWidth="1" Stroke="Transparent"></igchartprop:PaintElement>
														<igchartprop:PaintElement Fill="128, 255, 255" StrokeOpacity="255" FillGradientStyle="None" ElementType="SolidFill"
															FillOpacity="255" FillStopColor="Transparent" Hatch="None" FillStopOpacity="255" ImageWrapMode="Tile"
															ImageFitStyle="StretchedFit" StrokeWidth="1" Stroke="Transparent"></igchartprop:PaintElement>
														<igchartprop:PaintElement Fill="128, 128, 255" StrokeOpacity="255" FillGradientStyle="None" ElementType="SolidFill"
															FillOpacity="255" FillStopColor="Transparent" Hatch="None" FillStopOpacity="255" ImageWrapMode="Tile"
															ImageFitStyle="StretchedFit" StrokeWidth="1" Stroke="Transparent"></igchartprop:PaintElement>
														<igchartprop:PaintElement Fill="255, 128, 255" StrokeOpacity="255" FillGradientStyle="None" ElementType="SolidFill"
															FillOpacity="255" FillStopColor="Transparent" Hatch="None" FillStopOpacity="255" ImageWrapMode="Tile"
															ImageFitStyle="StretchedFit" StrokeWidth="1" Stroke="Transparent"></igchartprop:PaintElement>
														<igchartprop:PaintElement Fill="Red" StrokeOpacity="255" FillGradientStyle="None" ElementType="SolidFill"
															FillOpacity="255" FillStopColor="Transparent" Hatch="None" FillStopOpacity="255" ImageWrapMode="Tile"
															ImageFitStyle="StretchedFit" StrokeWidth="1" Stroke="Transparent"></igchartprop:PaintElement>
														<igchartprop:PaintElement Fill="128, 64, 0" StrokeOpacity="255" FillGradientStyle="None" ElementType="SolidFill"
															FillOpacity="255" FillStopColor="Transparent" Hatch="None" FillStopOpacity="255" ImageWrapMode="Tile"
															ImageFitStyle="StretchedFit" StrokeWidth="1" Stroke="Transparent"></igchartprop:PaintElement>
														<igchartprop:PaintElement Fill="Green" StrokeOpacity="255" FillGradientStyle="None" ElementType="SolidFill"
															FillOpacity="255" FillStopColor="Transparent" Hatch="None" FillStopOpacity="255" ImageWrapMode="Tile"
															ImageFitStyle="StretchedFit" StrokeWidth="1" Stroke="Transparent"></igchartprop:PaintElement>
														<igchartprop:PaintElement Fill="Navy" StrokeOpacity="255" FillGradientStyle="None" ElementType="SolidFill"
															FillOpacity="255" FillStopColor="Transparent" Hatch="None" FillStopOpacity="255" ImageWrapMode="Tile"
															ImageFitStyle="StretchedFit" StrokeWidth="1" Stroke="Transparent"></igchartprop:PaintElement>
														<igchartprop:PaintElement Fill="Purple" StrokeOpacity="255" FillGradientStyle="None" ElementType="SolidFill"
															FillOpacity="255" FillStopColor="Transparent" Hatch="None" FillStopOpacity="255" ImageWrapMode="Tile"
															ImageFitStyle="StretchedFit" StrokeWidth="1" Stroke="Transparent"></igchartprop:PaintElement>
													</PEs>
												</Skin>
											</ColorModel>
											<TitleTop Font="Microsoft Sans Serif, 7.8pt" Visible="True" Text="금액 기준지표 - 월별 손실률(%)   "
												FontSizeBestFit="False" Orientation="Horizontal" WrapText="False" Extent="33" FontColor="Black"
												HorizontalAlign="Near" VerticalAlign="Center" Location="Top">
												<Margins Bottom="5" Left="5" Top="5" Right="5"></Margins>
											</TitleTop>
											<Border CornerRadius="0" DrawStyle="Solid" Raised="False" Color="Black" Thickness="1"></Border>
											<TitleBottom Font="Microsoft Sans Serif, 7.8pt" Visible="True" Text=" " FontSizeBestFit="False"
												Orientation="Horizontal" WrapText="False" Extent="26" FontColor="Black" HorizontalAlign="Far"
												VerticalAlign="Center" Location="Bottom">
												<Margins Bottom="5" Left="5" Top="5" Right="5"></Margins>
											</TitleBottom>
											<Axis BackColor="Cornsilk">
												<Y LineEndCapStyle="NoAnchor" LineDrawStyle="Solid" Visible="True" RangeMin="0" LineColor="Black"
													RangeType="Automatic" TickmarkInterval="0" LineThickness="2" Extent="80" LogBase="10"
													RangeMax="0" TickmarkStyle="Percentage" TickmarkPercentage="10" NumericAxisType="Linear">
													<StripLines Interval="2" Visible="False">
														<PE Fill="Transparent" StrokeOpacity="255" FillGradientStyle="None" ElementType="SolidFill"
															FillOpacity="255" FillStopColor="Transparent" Hatch="None" FillStopOpacity="255"
															ImageWrapMode="Tile" ImageFitStyle="StretchedFit" StrokeWidth="1" Stroke="Transparent"></PE>
													</StripLines>
													<ScrollScale Scale="1" Scroll="0" Height="10" Width="15" Visible="False"></ScrollScale>
													<Labels Font="Microsoft Sans Serif, 7.8pt" Visible="True" HorizontalAlign="Far" WrapText="False"
														SeriesFormatString="" FontSizeBestFit="False" ClipText="True" ItemFormatString="&lt;DATA_VALUE:00.00&gt;"
														Flip="False" ItemFormat="DataValue" VerticalAlign="Center" FontColor="Black" Orientation="Horizontal"
														OrientationAngle="0"></Labels>
													<MajorGridLines AlphaLevel="255" DrawStyle="Dot" Color="Gainsboro" Visible="True" Thickness="1"></MajorGridLines>
													<MinorGridLines AlphaLevel="255" DrawStyle="Dot" Color="LightGray" Visible="True" Thickness="1"></MinorGridLines>
													<TimeAxisStyle TimeAxisStyle="Continuous"></TimeAxisStyle>
												</Y>
												<Y2 LineEndCapStyle="NoAnchor" LineDrawStyle="Solid" Visible="False" RangeMin="0" LineColor="Black"
													RangeType="Automatic" TickmarkInterval="0" LineThickness="2" Extent="80" LogBase="10"
													RangeMax="0" TickmarkStyle="Percentage" TickmarkPercentage="10" NumericAxisType="Linear">
													<StripLines Interval="2" Visible="False">
														<PE Fill="Transparent" StrokeOpacity="255" FillGradientStyle="None" ElementType="SolidFill"
															FillOpacity="255" FillStopColor="Transparent" Hatch="None" FillStopOpacity="255"
															ImageWrapMode="Tile" ImageFitStyle="StretchedFit" StrokeWidth="1" Stroke="Transparent"></PE>
													</StripLines>
													<ScrollScale Scale="1" Scroll="0" Height="10" Width="15" Visible="False"></ScrollScale>
													<Labels Font="Microsoft Sans Serif, 7.8pt" Visible="True" HorizontalAlign="Near" WrapText="False"
														SeriesFormatString="" FontSizeBestFit="False" ClipText="True" ItemFormatString="&lt;DATA_VALUE:00.00&gt;"
														Flip="False" ItemFormat="DataValue" VerticalAlign="Center" FontColor="Black" Orientation="Horizontal"
														OrientationAngle="0"></Labels>
													<MajorGridLines AlphaLevel="255" DrawStyle="Dot" Color="Gainsboro" Visible="True" Thickness="1"></MajorGridLines>
													<MinorGridLines AlphaLevel="255" DrawStyle="Dot" Color="LightGray" Visible="False" Thickness="1"></MinorGridLines>
													<TimeAxisStyle TimeAxisStyle="Continuous"></TimeAxisStyle>
												</Y2>
												<X2 LineEndCapStyle="NoAnchor" LineDrawStyle="Solid" Visible="False" RangeMin="0" LineColor="Black"
													RangeType="Automatic" TickmarkInterval="0" LineThickness="2" Extent="80" LogBase="10"
													RangeMax="0" TickmarkStyle="Percentage" TickmarkPercentage="10" NumericAxisType="Linear">
													<StripLines Interval="2" Visible="False">
														<PE Fill="Transparent" StrokeOpacity="255" FillGradientStyle="None" ElementType="SolidFill"
															FillOpacity="255" FillStopColor="Transparent" Hatch="None" FillStopOpacity="255"
															ImageWrapMode="Tile" ImageFitStyle="StretchedFit" StrokeWidth="1" Stroke="Transparent"></PE>
													</StripLines>
													<ScrollScale Scale="1" Scroll="0" Height="10" Width="15" Visible="False"></ScrollScale>
													<Labels Font="Microsoft Sans Serif, 7.8pt" Visible="True" HorizontalAlign="Far" WrapText="False"
														SeriesFormatString="&lt;SERIES_LABEL&gt;" FontSizeBestFit="False" ClipText="True"
														ItemFormatString="&lt;ITEM_LABEL&gt;" Flip="False" ItemFormat="ItemLabel" VerticalAlign="Center"
														FontColor="Black" Orientation="VerticalLeftFacing" OrientationAngle="0"></Labels>
													<MajorGridLines AlphaLevel="255" DrawStyle="Dot" Color="Gainsboro" Visible="True" Thickness="1"></MajorGridLines>
													<MinorGridLines AlphaLevel="255" DrawStyle="Dot" Color="LightGray" Visible="False" Thickness="1"></MinorGridLines>
													<TimeAxisStyle TimeAxisStyle="Continuous"></TimeAxisStyle>
												</X2>
												<Z2 LineEndCapStyle="NoAnchor" LineDrawStyle="Solid" Visible="False" RangeMin="0" LineColor="Black"
													RangeType="Automatic" TickmarkInterval="0" LineThickness="2" Extent="80" LogBase="10"
													RangeMax="0" TickmarkStyle="Percentage" TickmarkPercentage="10" NumericAxisType="Linear">
													<StripLines Interval="2" Visible="False">
														<PE Fill="Transparent" StrokeOpacity="255" FillGradientStyle="None" ElementType="SolidFill"
															FillOpacity="255" FillStopColor="Transparent" Hatch="None" FillStopOpacity="255"
															ImageWrapMode="Tile" ImageFitStyle="StretchedFit" StrokeWidth="1" Stroke="Transparent"></PE>
													</StripLines>
													<ScrollScale Scale="1" Scroll="0" Height="10" Width="15" Visible="False"></ScrollScale>
													<Labels Font="Microsoft Sans Serif, 7.8pt" Visible="True" HorizontalAlign="Near" WrapText="False"
														SeriesFormatString="" FontSizeBestFit="False" ClipText="True" ItemFormatString=""
														Flip="False" ItemFormat="None" VerticalAlign="Center" FontColor="Black" Orientation="Horizontal"
														OrientationAngle="0"></Labels>
													<MajorGridLines AlphaLevel="255" DrawStyle="Dot" Color="Gainsboro" Visible="True" Thickness="1"></MajorGridLines>
													<MinorGridLines AlphaLevel="255" DrawStyle="Dot" Color="LightGray" Visible="False" Thickness="1"></MinorGridLines>
													<TimeAxisStyle TimeAxisStyle="Continuous"></TimeAxisStyle>
												</Z2>
												<Z LineEndCapStyle="NoAnchor" LineDrawStyle="Solid" Visible="False" RangeMin="0" LineColor="Black"
													RangeType="Automatic" TickmarkInterval="0" LineThickness="2" Extent="80" LogBase="10"
													RangeMax="0" TickmarkStyle="Percentage" TickmarkPercentage="10" NumericAxisType="Linear">
													<StripLines Interval="2" Visible="False">
														<PE Fill="Transparent" StrokeOpacity="255" FillGradientStyle="None" ElementType="SolidFill"
															FillOpacity="255" FillStopColor="Transparent" Hatch="None" FillStopOpacity="255"
															ImageWrapMode="Tile" ImageFitStyle="StretchedFit" StrokeWidth="1" Stroke="Transparent"></PE>
													</StripLines>
													<ScrollScale Scale="1" Scroll="0" Height="10" Width="15" Visible="False"></ScrollScale>
													<Labels Font="Microsoft Sans Serif, 7.8pt" Visible="True" HorizontalAlign="Near" WrapText="False"
														SeriesFormatString="" FontSizeBestFit="False" ClipText="True" ItemFormatString="&lt;ITEM_LABEL&gt;"
														Flip="False" ItemFormat="ItemLabel" VerticalAlign="Center" FontColor="Black" Orientation="Horizontal"
														OrientationAngle="0"></Labels>
													<MajorGridLines AlphaLevel="255" DrawStyle="Dot" Color="Gainsboro" Visible="True" Thickness="1"></MajorGridLines>
													<MinorGridLines AlphaLevel="255" DrawStyle="Dot" Color="LightGray" Visible="False" Thickness="1"></MinorGridLines>
													<TimeAxisStyle TimeAxisStyle="Continuous"></TimeAxisStyle>
												</Z>
												<X LineEndCapStyle="NoAnchor" LineDrawStyle="Solid" Visible="True" RangeMin="0" LineColor="Gray"
													RangeType="Automatic" TickmarkInterval="0" LineThickness="2" Extent="80" LogBase="10"
													RangeMax="0" TickmarkStyle="Percentage" TickmarkPercentage="10" NumericAxisType="Linear">
													<StripLines Interval="2" Visible="False">
														<PE Fill="Transparent" StrokeOpacity="255" FillGradientStyle="None" ElementType="SolidFill"
															FillOpacity="255" FillStopColor="Transparent" Hatch="None" FillStopOpacity="255"
															ImageWrapMode="Tile" ImageFitStyle="StretchedFit" StrokeWidth="1" Stroke="Transparent"></PE>
													</StripLines>
													<ScrollScale Scale="1" Scroll="0" Height="10" Width="15" Visible="False"></ScrollScale>
													<Labels Font="Microsoft Sans Serif, 7.8pt" Visible="True" HorizontalAlign="Near" WrapText="False"
														SeriesFormatString="&lt;SERIES_LABEL&gt;" FontSizeBestFit="False" ClipText="True"
														ItemFormatString="&lt;ITEM_LABEL&gt;" Flip="False" ItemFormat="ItemLabel" VerticalAlign="Center"
														FontColor="Black" Orientation="VerticalLeftFacing" OrientationAngle="0"></Labels>
													<MajorGridLines AlphaLevel="255" DrawStyle="Dot" Color="Gainsboro" Visible="True" Thickness="1"></MajorGridLines>
													<MinorGridLines AlphaLevel="255" DrawStyle="Dot" Color="LightGray" Visible="False" Thickness="1"></MinorGridLines>
													<TimeAxisStyle TimeAxisStyle="Continuous"></TimeAxisStyle>
												</X>
											</Axis>
											<Data DataMember="" SwapRowsAndColumns="True" UseMinMax="True" UseRowLabelsColumn="False"
												MinValue="-1.7976931348623157E+308" RowLabelsColumn="-1" ZeroAligned="True" MaxValue="1.7976931348623157E+308"></Data>
											<Legend Font="Microsoft Sans Serif, 7.8pt" Visible="False" AlphaLevel="91" BorderThickness="1"
												BorderStyle="Solid" SpanPercentage="15" BorderColor="LightSlateGray" FontColor="Black"
												BackgroundColor="FloralWhite" DataAssociation="DefaultData" Location="Left" FormatString="&lt;ITEM_LABEL&gt;">
												<Margins Bottom="50" Left="5" Top="5" Right="5"></Margins>
											</Legend>
											<Tooltips BorderThickness="1" FormatString="&lt;DATA_VALUE:00.00&gt; %" EnableFadingEffect="False"
												Format="Custom" FontColor="Black" BorderColor="Black" Display="MouseMove" BackColor="AntiqueWhite"
												Padding="2"></Tooltips>
											<TitleRight Font="Microsoft Sans Serif, 7.8pt" Visible="False" Text="UltraChart for .NET is #1 !"
												FontSizeBestFit="False" Orientation="VerticalRightFacing" WrapText="False" Extent="26"
												FontColor="Black" HorizontalAlign="Near" VerticalAlign="Center" Location="Right">
												<Margins Bottom="5" Left="5" Top="5" Right="5"></Margins>
											</TitleRight>
											<TitleLeft Font="Microsoft Sans Serif, 7.8pt" Visible="False" Text="Component Market" FontSizeBestFit="False"
												Orientation="VerticalLeftFacing" WrapText="False" Extent="26" FontColor="Black" HorizontalAlign="Near"
												VerticalAlign="Center" Location="Left">
												<Margins Bottom="5" Left="5" Top="5" Right="5"></Margins>
											</TitleLeft>
											<DeploymentScenario Scenario="Session" ImageURL="ChartImages/Chart_#SEQNUM(100).jpg" ImageType="Png"
												FilePath="ChartImages"></DeploymentScenario>
										</igchart:ultrachart></TD>
								</TR>
								<TR>
									<TD vAlign="top" align="center"><igtbl:ultrawebgrid id="DataGrid1" runat="server" Width="100%" Height="200px">
											<DisplayLayout StationaryMargins="Header" RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single"
												AllowColumnMovingDefault="OnServer" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="DataGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect">
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
												<FrameStyle Width="100%" BorderWidth="1px" Font-Size="9pt" Font-Names="Verdana" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="200px"></FrameStyle>
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
												<igtbl:UltraGridBand></igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
								<tr height="40">
									<td height="40">&nbsp;
										<asp:button id="Button2" runat="server" Width="60px" Height="20px" Text="Excel"></asp:button><igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server" DownloadName="MoneyStandardIndex.XLS"></igtblexp:ultrawebgridexcelexporter></td>
								</tr>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
