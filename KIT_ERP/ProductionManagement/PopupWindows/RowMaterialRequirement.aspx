<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="RowMaterialRequirement.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.PopupWindows.RowMaterialRequirement" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RowMaterialRequirement</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			function Search()
			{				
				window.opener.SearchButton();self.close();	
			}
		</script>
	</HEAD>
	<body bgColor="#d4d0c8" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 5px; POSITION: absolute; TOP: 0px" cellSpacing="0"
				cellPadding="0" bgColor="#d4d0c8" border="0">
				<tr>
					<td width="80" align="right"><FONT face="굴림">최종납기일자</FONT>
					</td>
					<td width="820"><igsch:webdatechooser id="wdcDate" runat="server" Height="18px" Width="100px" Font-Size="10pt" Text=" "
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
					<TD style="HEIGHT: 289px" colSpan="2" width="900"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="296px" Width="900px">
							<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
								TabDirection="TopToBottom" RowHeightDefault="20px" Version="3.00" AllowColumnMovingDefault="OnServer"
								HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
								RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="Edit">
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
								<FrameStyle Width="900px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
									Height="296px"></FrameStyle>
								<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
									<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
								</FooterStyleDefault>
								<EditCellStyleDefault BorderWidth="0px" BorderStyle="None" HorizontalAlign="Right"></EditCellStyleDefault>
								<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
								<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EDEFF6">
									<Padding Left="3px"></Padding>
									<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
								</RowStyleDefault>
							</DisplayLayout>
							<Bands>
								<igtbl:UltraGridBand>
									<Columns>
										<igtbl:UltraGridColumn HeaderText="품목번호" Key="" Width="123px" BaseColumnName="ItemNum">
											<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Width="120px" Hidden="True" BaseColumnName="ItemDrawNum">
											<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="115px" BaseColumnName="ItemName">
											<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="단위" Key="Unit" Width="40px" BaseColumnName="Unit"></igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="규격" Key="Standard" Width="65px" BaseColumnName="Standard"></igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="총 소요량" Key="TotalQuantity" Width="70px" Format="###,###,###.###" BaseColumnName="TotalQuantity">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="외주창고 재고량" Key="OutRowQuantity" Width="100px" Hidden="True" Format="###,###,###.###"
											BaseColumnName="OutRowQuantity">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="자재산출대기량" Key="RowMaterialReadyQuantity" Hidden="True" Format="###,###,###.###"
											BaseColumnName="RowMaterialReadyQuantity">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="발주대기량" Key="OrderRequestReadyQuantityReflection" Width="70px" Format="###,###,###.###"
											BaseColumnName="OrderRequestReadyQuantityReflection">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="발주미입고량" Key="OrderNonInStorehouseReflection" Width="80px" Format="###,###,###.###"
											BaseColumnName="OrderNonInStorehouseReflection">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="순 소요량" Key="PureQuantity" Width="60px" Format="###,###,###.###" BaseColumnName="PureQuantity">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="안전재고량" Key="SafyRowMaterialQuantityReflection" Width="70px" Format="###,###,###.###"
											BaseColumnName="SafyRowMaterialQuantityReflection">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="발주간격량" Key="OrderGapQuantityReflection" Width="70px" Format="###,###,###.###"
											BaseColumnName="OrderGapQuantityReflection">
											<CellStyle HorizontalAlign="Right">
												<Padding Right="4px"></Padding>
											</CellStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="최소발주량" Key="MinimumOrderQuantityReflection" Width="65px" Format="###,###,###.###"
											BaseColumnName="MinimumOrderQuantityReflection">
											<CellStyle HorizontalAlign="Right">
												<Padding Right="4px"></Padding>
											</CellStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="구매의뢰량" Key="DeliveryOrderRequestQuantity" Width="70px" Format="###,###,###.###"
											BaseColumnName="DeliveryOrderRequestQuantity" AllowUpdate="Yes">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
										</igtbl:UltraGridColumn>
									</Columns>
								</igtbl:UltraGridBand>
							</Bands>
						</igtbl:ultrawebgrid></TD>
				</TR>
				<TR>
					<td vAlign="top" width="80"><asp:button id="Button1" runat="server" Height="20px" Width="50px" Font-Size="10pt" CommandName="Cancle"
							Text="Excel"></asp:button></td>
					<TD vAlign="top" align="center" width="820"><FONT face="굴림"><asp:button id="btnCancle" runat="server" Height="20px" Width="65px" Font-Size="10pt" CommandName="Cancle"
								Text="취   소"></asp:button>&nbsp;&nbsp;
							<asp:button id="btnOK" runat="server" Height="20px" Width="65px" Font-Size="10pt" CommandName="OK"
								Text="확   인"></asp:button></FONT></TD>
				</TR>
				<tr>
					<td colSpan="2" width="900"><igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter></td>
					</TD></tr>
				<tr>
					<td colSpan="2" width="900"><igtbl:ultrawebgrid id="uwgMRC_HT" runat="server" Width="710px" Visible="False">
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
						</igtbl:ultrawebgrid></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
