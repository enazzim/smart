<%@ Register TagPrefix="igchartprop" Namespace="Infragistics.UltraChart.Resources.Appearance" Assembly="Infragistics.UltraChart.Resources.v4.1, Version=4.1.20042.36, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igchart" Namespace="Infragistics.WebUI.UltraWebChart" Assembly="Infragistics.WebUI.UltraWebChart.v4.1, Version=4.1.20042.36, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="IncongruityPresentIndex.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.QualityInspection.IncongruityPresentIndex" codePage="949" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>IncongruityPresentIndex</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!--
			function btnSearch_Click_ProgressBar()
			{
					document.all.divBody.style.display="none";			
					document.all.img.src="../images/loadingbar.gif"
					document.all.progressBar.style.display="";
			}
	
			// 초기화 버튼
			function ResettxtBox()
			{
				document.Form1.ddlDiv.options[0].selected = true;
			}
		//-->
		</script>
	</HEAD>
	<body bottomMargin="0" bgColor="#f7f6f6" topMargin="0" rightMargin="0">
		<form id="Form1" runat="server">
			<TABLE id="Table2" style="LEFT: 10px; POSITION: absolute; TOP: 10px" height="500" cellSpacing="0"
				cellPadding="0" width="800" border="0">
				<TR>
					<TD width="20" height="40" style="HEIGHT: 40px"></TD>
					<TD style="HEIGHT: 40px">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; 90BORDER-RIGHT: black 1px solid"
							align="absBottom"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색조건 ]
							</LEGEND>
							<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD align="right" width="80" height="20">구분&nbsp;
									</TD>
									<TD align="left" width="115" height="20"><asp:dropdownlist id="ddlDiv" runat="server" Height="20px" Width="100px" BackColor="#EEEEE9">
											<asp:ListItem Value="0">전체</asp:ListItem>
											<asp:ListItem Value="1">납품/클레임</asp:ListItem>
											<asp:ListItem Value="2">사내부적합</asp:ListItem>
											<asp:ListItem Value="3">구매부적합</asp:ListItem>
											<asp:ListItem Value="4">외주부적합</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD style="WIDTH: 37px" align="right" colSpan="3" height="20"></TD>
									<TD width="150" height="20" align="right" colSpan="2"><INPUT style="WIDTH: 65px; HEIGHT: 20px" onclick="ResettxtBox()" type="button" value="초기화">&nbsp;
										<asp:button id="btnSearch" runat="server" Width="65px" Height="20px" Text="검  색"></asp:button>&nbsp;&nbsp;</TD>
								</TR>
								<TR>
									<TD align="right" width="80" colSpan="7" height="8"></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="20" height="550" style="HEIGHT: 450px"></TD>
					<TD width="800" style="HEIGHT: 450px"><br>
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; 90BORDER-RIGHT: black 1px solid"
							align="middle"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색결과 ]</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" border="0">
								<tr>
									<td height="257" style="HEIGHT: 257px">
										<table height="100%" cellSpacing="0" cellPadding="0" width="800" border="0">
											<tr>
												<td align="center" vAlign="top"><igchart:ultrachart id="UltraChart1" runat="server" Height="328px" Width="495px" EnableViewState="False"
														CrossHairColor="Gray" DoughnutChart-InnerRadius="40" DoughnutChart-RadiusFactor="80" DoughnutChart-BreakDistancePercentage="10"
														DoughnutChart-StartAngle="0" DoughnutChart-BreakAllSlices="False" DoughnutChart-PieThickness="20" DoughnutChart-ColumnIndex="0"
														DoughnutChart-BreakOthersSlice="False" DoughnutChart-OthersCategoryPercent="3" DoughnutChart-BreakAlternatingSlices="False"
														ChartType="StackColumnChart" EnableCrossHair="False" SplineChart-DrawStyle="Solid" SplineChart-Thickness="3" SplineChart-NullHandling="Zero"
														SplineChart-MidPointAnchors="True" SplineChart-StartStyle="ArrowAnchor" SplineChart-HighLightLines="True" SplineChart-SplineTension="0.2"
														SplineChart-EndStyle="Round" EmptyChartText=" " SplineAreaChart-SplineTension="0.2" SplineAreaChart-LineDrawStyle="Solid" SplineAreaChart-LineThickness="4"
														SplineAreaChart-NullHandling="Zero" SplineAreaChart-MidPointAnchors="True" SplineAreaChart-LineStartCapStyle="DiamondAnchor"
														SplineAreaChart-LineEndCapStyle="DiamondAnchor" Transform3D-YRotation="12" Transform3D-XRotation="144" Transform3D-Scale="83"
														Transform3D-ZRotation="0" BackColor="WhiteSmoke">
														<ColumnChart SeriesSpacing="1" ColumnSpacing="0" NullHandling="Zero">
															<ChartText>
																<igchartprop:ChartTextAppearance Visible="True" ChartTextFont="Arial, 7pt" PositionFromRadius="50" Row="-2" ItemFormatString="&lt;DATA_VALUE:00.00&gt;"
																	Column="-2" FontColor="Black" HorizontalAlign="Center" VerticalAlign="Center"></igchartprop:ChartTextAppearance>
															</ChartText>
														</ColumnChart>
														<ColorModel ColorBegin="Cornsilk" ColorEnd="Blue" AlphaLevel="150" ModelStyle="CustomSkin" Grayscale="False"
															Scaling="Increasing">
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
																	<igchartprop:PaintElement Fill="192, 255, 192" StrokeOpacity="255" FillGradientStyle="None" ElementType="SolidFill"
																		FillOpacity="255" FillStopColor="Transparent" Hatch="None" FillStopOpacity="255" ImageWrapMode="Tile"
																		ImageFitStyle="StretchedFit" StrokeWidth="1" Stroke="Transparent"></igchartprop:PaintElement>
																	<igchartprop:PaintElement Fill="192, 255, 255" StrokeOpacity="255" FillGradientStyle="None" ElementType="SolidFill"
																		FillOpacity="255" FillStopColor="Transparent" Hatch="None" FillStopOpacity="255" ImageWrapMode="Tile"
																		ImageFitStyle="StretchedFit" StrokeWidth="1" Stroke="Transparent"></igchartprop:PaintElement>
																	<igchartprop:PaintElement Fill="192, 192, 255" StrokeOpacity="255" FillGradientStyle="None" ElementType="SolidFill"
																		FillOpacity="255" FillStopColor="Transparent" Hatch="None" FillStopOpacity="255" ImageWrapMode="Tile"
																		ImageFitStyle="StretchedFit" StrokeWidth="1" Stroke="Transparent"></igchartprop:PaintElement>
																	<igchartprop:PaintElement Fill="Magenta" StrokeOpacity="255" FillGradientStyle="None" ElementType="SolidFill"
																		FillOpacity="255" FillStopColor="Transparent" Hatch="None" FillStopOpacity="255" ImageWrapMode="Tile"
																		ImageFitStyle="StretchedFit" StrokeWidth="1" Stroke="Transparent"></igchartprop:PaintElement>
																</PEs>
															</Skin>
														</ColorModel>
														<TitleTop Font="새굴림, 9pt" Visible="True" Text="부적합 현상 지표  " FontSizeBestFit="False" Orientation="Horizontal"
															WrapText="False" Extent="33" FontColor="Black" HorizontalAlign="Near" VerticalAlign="Center"
															Location="Top">
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
																<MinorGridLines AlphaLevel="255" DrawStyle="Dot" Color="LightGray" Visible="False" Thickness="1"></MinorGridLines>
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
																	SeriesFormatString="&lt;SERIES_LABEL&gt;" FontSizeBestFit="False" ClipText="True" ItemFormatString="&lt;ITEM_LABEL&gt;"
																	Flip="False" ItemFormat="ItemLabel" VerticalAlign="Center" FontColor="Black" Orientation="VerticalLeftFacing"
																	OrientationAngle="0"></Labels>
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
																	SeriesFormatString="" FontSizeBestFit="False" ClipText="True" ItemFormatString="" Flip="False"
																	ItemFormat="None" VerticalAlign="Center" FontColor="Black" Orientation="Horizontal"
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
																	SeriesFormatString="" FontSizeBestFit="False" ClipText="True" ItemFormatString="&lt;DATA_VALUE:00.00&gt;"
																	Flip="False" ItemFormat="DataValue" VerticalAlign="Center" FontColor="Black" Orientation="Horizontal"
																	OrientationAngle="0"></Labels>
																<MajorGridLines AlphaLevel="255" DrawStyle="Dot" Color="Gainsboro" Visible="True" Thickness="1"></MajorGridLines>
																<MinorGridLines AlphaLevel="255" DrawStyle="Dot" Color="LightGray" Visible="False" Thickness="1"></MinorGridLines>
																<TimeAxisStyle TimeAxisStyle="Continuous"></TimeAxisStyle>
															</Z>
															<X LineEndCapStyle="NoAnchor" LineDrawStyle="Solid" Visible="True" RangeMin="0" LineColor="Black"
																RangeType="Automatic" TickmarkInterval="0" LineThickness="2" Extent="80" LogBase="10"
																RangeMax="0" TickmarkStyle="Percentage" TickmarkPercentage="10" NumericAxisType="Linear">
																<StripLines Interval="2" Visible="False">
																	<PE Fill="Transparent" StrokeOpacity="255" FillGradientStyle="None" ElementType="SolidFill"
																		FillOpacity="255" FillStopColor="Transparent" Hatch="None" FillStopOpacity="255"
																		ImageWrapMode="Tile" ImageFitStyle="StretchedFit" StrokeWidth="1" Stroke="Transparent"></PE>
																</StripLines>
																<ScrollScale Scale="1" Scroll="0" Height="10" Width="15" Visible="False"></ScrollScale>
																<Labels Font="Microsoft Sans Serif, 7.8pt" Visible="True" HorizontalAlign="Near" WrapText="False"
																	SeriesFormatString="&lt;SERIES_LABEL&gt;" FontSizeBestFit="False" ClipText="True" ItemFormatString="&lt;ITEM_LABEL&gt;"
																	Flip="False" ItemFormat="ItemLabel" VerticalAlign="Center" FontColor="Black" Orientation="VerticalLeftFacing"
																	OrientationAngle="0"></Labels>
																<MajorGridLines AlphaLevel="255" DrawStyle="Dot" Color="Gainsboro" Visible="True" Thickness="1"></MajorGridLines>
																<MinorGridLines AlphaLevel="255" DrawStyle="Dot" Color="LightGray" Visible="False" Thickness="1"></MinorGridLines>
																<TimeAxisStyle TimeAxisStyle="Continuous"></TimeAxisStyle>
															</X>
														</Axis>
														<Data DataMember="" SwapRowsAndColumns="True" UseMinMax="False" UseRowLabelsColumn="False"
															MinValue="-1.7976931348623157E+308" RowLabelsColumn="-1" ZeroAligned="True" MaxValue="1.7976931348623157E+308"></Data>
														<Legend Font="Arial, 8.25pt" Visible="False" AlphaLevel="146" BorderThickness="1" BorderStyle="Solid"
															SpanPercentage="23" BorderColor="SlateGray" FontColor="Black" BackgroundColor="FloralWhite"
															DataAssociation="LineData" Location="Bottom" FormatString="&lt;ITEM_LABEL&gt;">
															<Margins Bottom="12" Left="15" Top="5" Right="15"></Margins>
														</Legend>
														<Tooltips BorderThickness="1" FormatString="[&lt;ITEM_LABEL&gt;] : &lt;DATA_VALUE:,###,##0.00&gt;"
															EnableFadingEffect="False" Format="Custom" FontColor="Black" BorderColor="Black" Display="MouseMove"
															BackColor="AntiqueWhite" Padding="0"></Tooltips>
														<TitleRight Font="Microsoft Sans Serif, 7.8pt" Visible="False" Text="UltraChart for .NET is #1 !"
															FontSizeBestFit="False" Orientation="VerticalRightFacing" WrapText="False" Extent="26" FontColor="Black"
															HorizontalAlign="Near" VerticalAlign="Center" Location="Right">
															<Margins Bottom="5" Left="5" Top="5" Right="5"></Margins>
														</TitleRight>
														<TitleLeft Font="Microsoft Sans Serif, 7.8pt" Visible="False" Text="Component Market" FontSizeBestFit="False"
															Orientation="VerticalLeftFacing" WrapText="False" Extent="26" FontColor="Black" HorizontalAlign="Near"
															VerticalAlign="Center" Location="Left">
															<Margins Bottom="5" Left="5" Top="5" Right="5"></Margins>
														</TitleLeft>
														<DeploymentScenario Scenario="Session" ImageURL="ChartImages/Chart1.jpg" ImageType="Png" FilePath="ChartImages"></DeploymentScenario>
													</igchart:ultrachart></td>
												<td align="center" vAlign="top"><igchart:ultrachart id="UltraChart2" runat="server" Height="328px" Width="295px" EnableViewState="False"
														CrossHairColor="SlateGray" DoughnutChart-InnerRadius="50" DoughnutChart-RadiusFactor="90" DoughnutChart-BreakDistancePercentage="10"
														DoughnutChart-StartAngle="0" DoughnutChart-BreakAllSlices="False" DoughnutChart-PieThickness="20" DoughnutChart-ColumnIndex="0"
														DoughnutChart-BreakOthersSlice="False" DoughnutChart-OthersCategoryPercent="0" DoughnutChart-BreakAlternatingSlices="True" ChartType="DoughnutChart"
														EnableCrossHair="False" EmptyChartText=" " Transform3D-YRotation="12" Transform3D-XRotation="28" Transform3D-Scale="100" Transform3D-ZRotation="0"
														BackColor="WhiteSmoke">
														<ColorModel ColorBegin="Yellow" ColorEnd="Blue" AlphaLevel="199" ModelStyle="CustomSkin" Grayscale="False"
															Scaling="Increasing">
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
																</PEs>
															</Skin>
														</ColorModel>
														<TitleTop Font="Microsoft Sans Serif, 8.25pt" Visible="True" Text="부적합 현상별 집계  " FontSizeBestFit="False"
															Orientation="Horizontal" WrapText="False" Extent="33" FontColor="Black" HorizontalAlign="Near"
															VerticalAlign="Center" Location="Top">
															<Margins Bottom="5" Left="5" Top="5" Right="5"></Margins>
														</TitleTop>
														<Border CornerRadius="0" DrawStyle="Solid" Raised="False" Color="Black" Thickness="1"></Border>
														<TitleBottom Font="Microsoft Sans Serif, 7.8pt" Visible="True" Text=" " FontSizeBestFit="False"
															Orientation="Horizontal" WrapText="False" Extent="26" FontColor="Black" HorizontalAlign="Far"
															VerticalAlign="Center" Location="Bottom">
															<Margins Bottom="5" Left="5" Top="5" Right="5"></Margins>
														</TitleBottom>
														<Axis BackColor="Cornsilk">
															<Y LineEndCapStyle="NoAnchor" LineDrawStyle="Solid" Visible="False" RangeMin="0" LineColor="Black"
																RangeType="Automatic" TickmarkInterval="0" LineThickness="2" Extent="80" LogBase="10"
																RangeMax="0" TickmarkStyle="Percentage" TickmarkPercentage="10" NumericAxisType="Linear">
																<StripLines Interval="2" Visible="False">
																	<PE Fill="Transparent" StrokeOpacity="255" FillGradientStyle="None" ElementType="SolidFill"
																		FillOpacity="255" FillStopColor="Transparent" Hatch="None" FillStopOpacity="255"
																		ImageWrapMode="Tile" ImageFitStyle="StretchedFit" StrokeWidth="1" Stroke="Transparent"></PE>
																</StripLines>
																<ScrollScale Scale="1" Scroll="0" Height="10" Width="15" Visible="False"></ScrollScale>
																<Labels Font="Microsoft Sans Serif, 7.8pt" Visible="True" HorizontalAlign="Far" WrapText="False"
																	SeriesFormatString="&lt;SERIES_LABEL&gt;" FontSizeBestFit="False" ClipText="True" ItemFormatString="&lt;DATA_VALUE:00.00&gt;"
																	Flip="False" ItemFormat="DataValue" VerticalAlign="Center" FontColor="Black" Orientation="Horizontal"
																	OrientationAngle="0"></Labels>
																<MajorGridLines AlphaLevel="255" DrawStyle="Dot" Color="Gainsboro" Visible="True" Thickness="1"></MajorGridLines>
																<MinorGridLines AlphaLevel="255" DrawStyle="Dot" Color="LightGray" Visible="False" Thickness="1"></MinorGridLines>
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
																	SeriesFormatString="&lt;SERIES_LABEL&gt;" FontSizeBestFit="False" ClipText="True" ItemFormatString=""
																	Flip="False" ItemFormat="None" VerticalAlign="Center" FontColor="Black" Orientation="Horizontal"
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
																	SeriesFormatString="&lt;SERIES_LABEL&gt;" FontSizeBestFit="False" ClipText="True" ItemFormatString=""
																	Flip="False" ItemFormat="None" VerticalAlign="Center" FontColor="Black" Orientation="Horizontal"
																	OrientationAngle="0"></Labels>
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
																	SeriesFormatString="" FontSizeBestFit="False" ClipText="True" ItemFormatString="" Flip="False"
																	ItemFormat="None" VerticalAlign="Center" FontColor="Black" Orientation="Horizontal"
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
																<Labels Font="Microsoft Sans Serif, 7.8pt" Visible="True" HorizontalAlign="Far" WrapText="False"
																	SeriesFormatString="" FontSizeBestFit="False" ClipText="True" ItemFormatString="&lt;DATA_VALUE:00.00&gt;"
																	Flip="False" ItemFormat="DataValue" VerticalAlign="Center" FontColor="Black" Orientation="Horizontal"
																	OrientationAngle="0"></Labels>
																<MajorGridLines AlphaLevel="255" DrawStyle="Dot" Color="Gainsboro" Visible="True" Thickness="1"></MajorGridLines>
																<MinorGridLines AlphaLevel="255" DrawStyle="Dot" Color="LightGray" Visible="False" Thickness="1"></MinorGridLines>
																<TimeAxisStyle TimeAxisStyle="Continuous"></TimeAxisStyle>
															</Z>
															<X LineEndCapStyle="NoAnchor" LineDrawStyle="Solid" Visible="False" RangeMin="0" LineColor="Gray"
																RangeType="Automatic" TickmarkInterval="0" LineThickness="2" Extent="80" LogBase="10"
																RangeMax="0" TickmarkStyle="Percentage" TickmarkPercentage="10" NumericAxisType="Linear">
																<StripLines Interval="2" Visible="False">
																	<PE Fill="Transparent" StrokeOpacity="255" FillGradientStyle="None" ElementType="SolidFill"
																		FillOpacity="255" FillStopColor="Transparent" Hatch="None" FillStopOpacity="255"
																		ImageWrapMode="Tile" ImageFitStyle="StretchedFit" StrokeWidth="1" Stroke="Transparent"></PE>
																</StripLines>
																<ScrollScale Scale="1" Scroll="0" Height="10" Width="15" Visible="False"></ScrollScale>
																<Labels Font="Microsoft Sans Serif, 7.8pt" Visible="True" HorizontalAlign="Near" WrapText="False"
																	SeriesFormatString="&lt;SERIES_LABEL&gt;" FontSizeBestFit="False" ClipText="True" ItemFormatString="&lt;ITEM_LABEL&gt;"
																	Flip="False" ItemFormat="ItemLabel" VerticalAlign="Center" FontColor="Black" Orientation="Horizontal"
																	OrientationAngle="0"></Labels>
																<MajorGridLines AlphaLevel="255" DrawStyle="Dot" Color="Gainsboro" Visible="True" Thickness="1"></MajorGridLines>
																<MinorGridLines AlphaLevel="255" DrawStyle="Dot" Color="LightGray" Visible="False" Thickness="1"></MinorGridLines>
																<TimeAxisStyle TimeAxisStyle="Continuous"></TimeAxisStyle>
															</X>
														</Axis>
														<Data DataMember="" SwapRowsAndColumns="False" UseMinMax="False" UseRowLabelsColumn="False"
															MinValue="-1.7976931348623157E+308" RowLabelsColumn="-1" ZeroAligned="False" MaxValue="1.7976931348623157E+308"></Data>
														<Legend Font="Microsoft Sans Serif, 7.8pt" Visible="True" AlphaLevel="91" BorderThickness="1"
															BorderStyle="Solid" SpanPercentage="30" BorderColor="LightSlateGray" FontColor="Black"
															BackgroundColor="FloralWhite" DataAssociation="DefaultData" Location="Bottom" FormatString="&lt;ITEM_LABEL&gt;">
															<Margins Bottom="13" Left="5" Top="5" Right="5"></Margins>
														</Legend>
														<Tooltips BorderThickness="1" FormatString="[&lt;ITEM_LABEL&gt;] : &lt;DATA_VALUE:###,##0.00&gt; : &lt;PERCENT_VALUE:#0.00&gt;% "
															EnableFadingEffect="False" Format="Custom" FontColor="Black" BorderColor="Black" Display="MouseMove"
															BackColor="AntiqueWhite" Padding="2"></Tooltips>
														<TitleRight Font="Microsoft Sans Serif, 7.8pt" Visible="False" Text="UltraChart for .NET is #1 !"
															FontSizeBestFit="False" Orientation="VerticalRightFacing" WrapText="False" Extent="26" FontColor="Black"
															HorizontalAlign="Near" VerticalAlign="Center" Location="Right">
															<Margins Bottom="5" Left="5" Top="5" Right="5"></Margins>
														</TitleRight>
														<TitleLeft Font="Microsoft Sans Serif, 7.8pt" Visible="False" Text="Component Market" FontSizeBestFit="False"
															Orientation="VerticalLeftFacing" WrapText="False" Extent="26" FontColor="Black" HorizontalAlign="Near"
															VerticalAlign="Center" Location="Left">
															<Margins Bottom="5" Left="5" Top="5" Right="5"></Margins>
														</TitleLeft>
														<DeploymentScenario Scenario="Session" ImageURL="ChartImages/Chart2.jpg" ImageType="Png" FilePath="ChartImages"></DeploymentScenario>
													</igchart:ultrachart></td>
											</tr>
										</table>
									</td>
								</tr>
								<TR>
									<TD vAlign="top" height="149" style="HEIGHT: 149px" align="center"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="170px" Width="100%">
											<DisplayLayout StationaryMargins="Header" AllowSortingDefault="OnClient" RowHeightDefault="20px"
												Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortSingle"
												BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1"
												TableLayout="Fixed" CellClickActionDefault="RowSelect">
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
												<FrameStyle Width="100%" BorderWidth="1px" Font-Size="10pt" Font-Names="Verdana" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="170px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None" BackColor="SteelBlue"></EditCellStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" Font-Bold="True" BorderStyle="Groove" ForeColor="WhiteSmoke" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
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
							</TABLE>
							<TABLE id="Table4" height="8" cellSpacing="0" cellPadding="0" width="800" border="0" style="WIDTH: 800px; HEIGHT: 8px">
								<TR>
									<TD height="33">&nbsp;&nbsp;<asp:button id="Button2" runat="server" Height="20px" Width="65px" Text="Excel"></asp:button>
										<igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
