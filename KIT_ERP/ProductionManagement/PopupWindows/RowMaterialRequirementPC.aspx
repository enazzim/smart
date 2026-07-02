<%@ Page language="c#" Codebehind="RowMaterialRequirementPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.PopupWindows.RowMaterialRequirementPC" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RowMaterialRequirementPC</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			function Search()
			{				
				window.opener.SearchButton();self.close();	
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#d4d0c8">
		<base target="_self">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px" cellSpacing="0"
				cellPadding="0" border="0" bgColor="#d4d0c8">
				<tr>
					<td height="30" align="right" width="70"><FONT face="굴림">의뢰일자 </FONT>
					</td>
					<td width="930"><igsch:webdatechooser id="wdcDate" runat="server" Height="18px" Width="100px" Font-Size="10pt" Text=" "
							BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" NullDateLabel=" ">
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
				</tr>
				<TR>
					<TD style="HEIGHT: 289px" colSpan="2">
						<igtbl:UltraWebGrid id="UltraWebGrid1" runat="server" Width="1000px" Height="296px" Visible="False">
							<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
								RowHeightDefault="20px" Version="3.00" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti"
								BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1"
								TableLayout="Fixed">
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
								<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
									<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
								</HeaderStyleDefault>
								<FrameStyle Width="1000px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
									Height="296px"></FrameStyle>
								<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
									<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
								</FooterStyleDefault>
								<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
								<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
								<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EDEFF6">
									<Padding Left="3px"></Padding>
									<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
								</RowStyleDefault>
							</DisplayLayout>
							<Bands>
								<igtbl:UltraGridBand>
									<Columns>
										<igtbl:UltraGridColumn HeaderText="품목번호" Key="" Width="150px" BaseColumnName="ItemNum">
											<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Width="150px" Hidden="True" BaseColumnName="ItemDrawNum">
											<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" BaseColumnName="ItemName">
											<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="총 소요량" Key="TotalQuantity" Width="100px" Format="" BaseColumnName="TotalQuantity">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="외주창고 재고량" Key="OutRowQuantity" Width="100px" Hidden="True" Format=""
											BaseColumnName="OutRowQuantity">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="자재산출대기량" Key="RowMaterialReadyQuantity" BaseColumnName="RowMaterialReadyQuantity">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="발주의뢰대기량" Key="OrderRequestReadyQuantityReflection" BaseColumnName="OrderRequestReadyQuantityReflection">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="구매발주미입고량" Key="OrderNonInStorehouseReflection" BaseColumnName="OrderNonInStorehouseReflection">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="순 소요량" Key="PureQuantity" Width="100px" Format="" BaseColumnName="PureQuantity">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="안전 재고량" Key="SafyRowMaterialQuantityReflection" Format="" BaseColumnName="SafyRowMaterialQuantityReflection">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="구매의뢰량" Key="DeliveryOrderRequestQuantity" Format="" BaseColumnName="DeliveryOrderRequestQuantity">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
										</igtbl:UltraGridColumn>
									</Columns>
								</igtbl:UltraGridBand>
							</Bands>
						</igtbl:UltraWebGrid>
						<igtbl:UltraWebGrid id="UltraWebGrid2" runat="server" Height="296px" Width="1000px">
							<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" RowHeightDefault="20px" Version="3.00"
								AllowColumnMovingDefault="OnServer" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
								RowSelectorsDefault="No" Name="UltraWebGrid2" TableLayout="Fixed">
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
								<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
									<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
								</HeaderStyleDefault>
								<FrameStyle Width="1000px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
									Height="296px"></FrameStyle>
								<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
									<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
								</FooterStyleDefault>
								<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
								<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
								<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EDEFF6">
									<Padding Left="3px"></Padding>
									<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
								</RowStyleDefault>
							</DisplayLayout>
							<Bands>
								<igtbl:UltraGridBand>
									<Columns>
										<igtbl:UltraGridColumn HeaderText="품목번호" Key="" Width="150px" BaseColumnName="ItemNum">
											<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Width="150px" BaseColumnName="ItemDrawNum">
											<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" BaseColumnName="ItemName">
											<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="총 소요량" Key="TotalQuantity" Width="90px" Format="" BaseColumnName="TotalQuantity">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="산출대기량" Key="RowMaterialReadyQuantity" Width="90px" BaseColumnName="RowMaterialReadyQuantity">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="발주미입량" Key="OrderNonInStorehouseReflection" Width="90px" BaseColumnName="OrderNonInStorehouseReflection">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="잉여재고량" Key="" Width="90px" BaseColumnName=""></igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="부족량" Key="DeliveryOrderRequestQuantity" Width="90px" Format="" BaseColumnName="DeliveryOrderRequestQuantity">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="조정량" Key="" Width="95px" BaseColumnName="" AllowUpdate="Yes"></igtbl:UltraGridColumn>
									</Columns>
								</igtbl:UltraGridBand>
							</Bands>
						</igtbl:UltraWebGrid></TD>
				</TR>
				<TR>
					<td vAlign="bottom">
						<asp:Button id="Button1" runat="server" Height="20px" Width="50px" Text="Excel" CommandName="Cancle"
							Font-Size="10pt"></asp:Button></td>
					<TD style="PADDING-TOP: 10px" align="center" width="950"><FONT face="굴림">
							<asp:Button id="btnCancle" runat="server" Width="65px" Height="20px" Font-Size="10pt" CommandName="Cancle"
								Text="취   소"></asp:Button>&nbsp;&nbsp;
							<asp:Button id="btnOK" runat="server" Width="65px" Height="20px" Font-Size="10pt" CommandName="OK"
								Text="확   인"></asp:Button></FONT></TD>
				</TR>
			</TABLE>
			<igtbl:UltraWebGrid id="uwgMRC_HT" style="Z-INDEX: 102; LEFT: -8px; POSITION: absolute; TOP: 672px"
				runat="server" Visible="False" Width="710px">
				<DisplayLayout RowHeightDefault="20px" Version="3.00" BorderCollapseDefault="Separate" Name="uwgMRCxHT">
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
					<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
						<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
					</HeaderStyleDefault>
					<FrameStyle Width="710px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
						Height="200px"></FrameStyle>
					<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
						<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
					</FooterStyleDefault>
					<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
					<RowStyleDefault BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid">
						<Padding Left="3px"></Padding>
						<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
					</RowStyleDefault>
				</DisplayLayout>
				<Bands>
					<igtbl:UltraGridBand></igtbl:UltraGridBand>
				</Bands>
			</igtbl:UltraWebGrid>
			<TABLE id="Table2" style="LEFT: 0px; WIDTH: 400px; POSITION: absolute; TOP: 0px; HEIGHT: 57px"
				cellSpacing="0" cellPadding="0" width="400" border="0">
				<TR>
					<TD>
						<igtblexp:UltraWebGridExcelExporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:UltraWebGridExcelExporter></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
