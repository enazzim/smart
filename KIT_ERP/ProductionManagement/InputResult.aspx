<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="InputResult.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.InputResult" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>InputResult</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 0px; WIDTH: 816px; POSITION: absolute; TOP: 0px; HEIGHT: 520px"
				cellSpacing="0" cellPadding="0" width="816" border="0">
				<TR>
					<TD style="HEIGHT: 520px" width="20"></TD>
					<TD style="FONT-SIZE: 0pt; LEFT: 10px; PADDING-TOP: 10px; POSITION: absolute; TOP: 10px"
						vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 805px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 80px"><LEGEND align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD vAlign="middle" align="left" colSpan="8" height="30"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 9pt" vAlign="middle" align="right" width="70" height="30"><FONT face="굴림">일자</FONT>&nbsp;</TD>
									<TD vAlign="middle" align="left" width="100" height="30"><igsch:webdatechooser id="wdc_FromDate" runat="server" BorderColor="DimGray" BorderStyle="Solid" BackColor="#EEEEE9"
											Text=" " NullDateLabel=" " Width="100px" Height="18px" Font-Size="10pt">
											<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
												DayNameFormat="FirstLetter">
												<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
												<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
												<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
												<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
											</CALENDARLAYOUT>
											<DROPDOWNSTYLE BorderStyle="Solid" BorderColor="DimGray" BorderWidth="1px"></DROPDOWNSTYLE>
											<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
											<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
										</igsch:webdatechooser></TD>
									<TD vAlign="middle" align="center" width="15" height="30">~</TD>
									<TD vAlign="middle" align="left" width="100" height="30"><igsch:webdatechooser id="wdc_ToDate" runat="server" BorderColor="DimGray" BorderStyle="Solid" BackColor="#EEEEE9"
											Text=" " NullDateLabel=" " Width="100px" Height="18px" Font-Size="10pt">
											<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
												DayNameFormat="FirstLetter">
												<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
												<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
												<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
												<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
											</CALENDARLAYOUT>
											<DROPDOWNSTYLE BorderStyle="Solid" BorderColor="DimGray" BorderWidth="1px"></DROPDOWNSTYLE>
											<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
											<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
										</igsch:webdatechooser></TD>
									<TD style="FONT-SIZE: 9pt" vAlign="middle" align="right" width="70" height="30">WC명&nbsp;</TD>
									<TD vAlign="middle" align="left" width="125" height="30">
										<igcmbo:webcombo id="wc_WCName" runat="server" Font-Size="10pt" Height="18px" Width="120px" BackColor="#EEEEE9"
											BorderStyle="Solid" BorderColor="DimGray" DropImageXP1="/ig_common/WebGrid3/ig_cmboDown1.bmp"
											DropImageXP2="/ig_common/WebGrid3/ig_cmboDown2.bmp" ForeColor="Black" BorderWidth="1px" Version="3.00"
											SelForeColor="White" SelectedIndex="-1" SelBackColor="10, 36, 106">
											<Columns>
												<igtbl:UltraGridColumn HeaderText="작업장명" Key="WCName" Width="180px" BaseColumnName="WCName"></igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="번호" Key="WCInfoIndex" Hidden="True" BaseColumnName="WCInfoIndex"></igtbl:UltraGridColumn>
											</Columns>
											<DropDownLayout DropdownWidth="200px" BorderCollapse="Separate" RowSelectors="No" AllowColSizing="Free"
												RowHeightDefault="20px" HeaderClickAction="Select" AutoGenerateColumns="False" DropdownHeight="300px"
												TableLayout="Fixed" StationaryMargins="Header">
												<RowStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid" BackColor="White">
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyle>
												<SelectedRowStyle ForeColor="White" BackColor="#0A246A"></SelectedRowStyle>
												<HeaderStyle BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyle>
												<FrameStyle Width="100%" Cursor="Default" Font-Size="10pt" Font-Names="Verdana" BackColor="Silver"
													Height="100%"></FrameStyle>
											</DropDownLayout>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igcmbo:webcombo></TD>
									<TD vAlign="middle" align="right" width="320" colSpan="2" height="30"><asp:button id="Button2" runat="server" Text="초기화" Width="60px" Height="20px" Font-Size="9pt"></asp:button>&nbsp;
										<asp:button id="Button1" runat="server" Text="검   색" Width="60px" Height="20px" Font-Size="9pt"></asp:button>&nbsp;&nbsp;</TD>
								</TR>
								<TR>
									<TD align="right" colSpan="8" height="8"><FONT face="굴림"></FONT></TD>
								</TR>
							</TABLE>
						</FIELDSET>
						<BR>
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[검색결과]</LEGEND>
							<TABLE id="Table4" style="WIDTH: 800px" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD vAlign="top" align="center">
										<igtbl:ultrawebgrid id="UltraWebGrid2" runat="server" Height="410px" Width="800px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="UltraWebGrid2" TableLayout="Fixed" CellClickActionDefault="RowSelect">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</AddNewBox>
												<Pager PageSize="17" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<RowSelectorStyleDefault Cursor="Hand" BackColor="#C0FFFF"></RowSelectorStyleDefault>
												<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="410px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents DblClickHandler="UltraWebGrid1_DblClickHandler"></ClientSideEvents>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="일자" Key="Date" Width="100px" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="Date">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="200px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="투입작업장" Key="ThrowWCName" Width="100px" HeaderClickAction="SortMulti"
															BaseColumnName="ThrowWCName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="투입창고" Key="StoreNum" Width="100px" HeaderClickAction="SortMulti" BaseColumnName="StoreNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="투입수량" Key="ThrowingQuanity" Width="100px" Format="###,###,###.##" HeaderClickAction="SortMulti"
															BaseColumnName="ThrowingQuanity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="생산수량" Key="ProductQuantity" Width="100px" Format="###,###,###.##" HeaderClickAction="SortMulti"
															BaseColumnName="ProductQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="생산작업장" Key="ProductWCName" Width="100px" HeaderClickAction="SortMulti"
															BaseColumnName="ProductWCName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
								<tr>
									<td><asp:button id="bt_Excel" runat="server" Text="Excel" Width="60px" Height="20px" Font-Size="10pt"></asp:button>
										<igtblexp:UltraWebGridExcelExporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:UltraWebGridExcelExporter></td>
								</tr>
							</TABLE>
						</FIELDSET>
						&nbsp;
						<BR>
						<BR>
						<BR>
						<BR>
						<BR>
						<BR>
						</FONT></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
