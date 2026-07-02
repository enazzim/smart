<%@ Page language="c#" Codebehind="ItemOrderPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.EtcPresentCondition.ItemOrderPC" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ItemOrderPC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!--
		function ResettxtBox()
		{
			
			var date = new Date();
			var year = date.getFullYear();
			var month = date.getMonth()+1;
			
			for(var i=0;i<document.Form1.ddlYear.options.length;i++)
			{
				if(document.Form1.ddlYear.options[i].innerText == year)
				{
					document.Form1.ddlYear.options[i].selected=true;
					break;
				}
				else
					document.Form1.ddlYear.options[0].selected=true;
			}
			
			for(var i=0;i<document.Form1.ddlMonth.options.length;i++)
			{
				if(document.Form1.ddlMonth.options[i].innerText == month)
				{
					document.Form1.ddlMonth.options[i].selected=true;
					break;
				}
				else
					document.Form1.ddlMonth.options[0].selected=true;
			}
			
			
		}
		
		//-->
		</script>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; POSITION: absolute; TOP: 8px; LEFT: 8px" cellSpacing="0"
				cellPadding="0" width="800" border="0">
				<TR>
					<TD style="HEIGHT: 50px; PADDING-TOP: 10px" vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-BOTTOM: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-TOP: #696969 2px solid; BORDER-RIGHT: #696969 2px solid"><LEGEND align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD align="right" width="70" height="30"><FONT face="굴림">년&nbsp; 도&nbsp;</FONT></TD>
									<TD width="100" height="30"><FONT face="굴림"><asp:dropdownlist id="ddlYear" runat="server" Height="20px" Width="70px" BackColor="#EEEEE9" Font-Size="9pt">
												<asp:ListItem Value="2013">2013</asp:ListItem>
												<asp:ListItem Value="2015">2015</asp:ListItem>
												<asp:ListItem Value="2016">2016</asp:ListItem>
												<asp:ListItem Value="2017">2017</asp:ListItem>
												<asp:ListItem Value="2018">2018</asp:ListItem>
												<asp:ListItem Value="2019">2019</asp:ListItem>
												<asp:ListItem Value="2020">2020</asp:ListItem>
												<asp:ListItem Value="2021">2021</asp:ListItem>
												<asp:ListItem Value="2022">2022</asp:ListItem>
												<asp:ListItem Value="2023">2023</asp:ListItem>
											</asp:dropdownlist></FONT></TD>
									<TD align="center" width="15" height="30"><FONT face="굴림">월</FONT></TD>
									<TD width="100" height="30"><FONT face="굴림"><asp:dropdownlist id="ddlMonth" runat="server" Height="20px" Width="50px" BackColor="#EEEEE9" Font-Size="9pt">
												<asp:ListItem Value="1">1</asp:ListItem>
												<asp:ListItem Value="2">2</asp:ListItem>
												<asp:ListItem Value="3">3</asp:ListItem>
												<asp:ListItem Value="4">4</asp:ListItem>
												<asp:ListItem Value="5">5</asp:ListItem>
												<asp:ListItem Value="6">6</asp:ListItem>
												<asp:ListItem Value="7">7</asp:ListItem>
												<asp:ListItem Value="8">8</asp:ListItem>
												<asp:ListItem Value="9">9</asp:ListItem>
												<asp:ListItem Value="10">10</asp:ListItem>
												<asp:ListItem Value="11">11</asp:ListItem>
												<asp:ListItem Value="12">12</asp:ListItem>
											</asp:dropdownlist></FONT></TD>
									<TD align="right" width="515" height="30"><FONT face="굴림"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
												type="button" value="초기화" name="btnReset">&nbsp;&nbsp;
											<asp:button id="btItemSearch" runat="server" Height="20px" Width="60px" Text="검 색"></asp:button>&nbsp;&nbsp;
										</FONT>
									</TD>
								</TR>
							</TABLE>
							<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="800">
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" width="800"></TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-BOTTOM: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; HEIGHT: 430px; BORDER-TOP: #696969 2px solid; BORDER-RIGHT: #696969 2px solid"><LEGEND align="top">[검색결과]</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800">
								<TR>
									<TD width="800" colSpan="6" height="410"><FONT face="굴림"><DISPLAYLAYOUT CellClickActionDefault="RowSelect" TableLayout="Fixed" Name="UltraWebGrid1" RowSelectorsDefault="No"
												AllowColSizingDefault="Free" BorderCollapseDefault="Separate" AllowColumnMovingDefault="OnServer" SelectTypeRowDefault="Single" Version="3.00"
												RowHeightDefault="20px" AutoGenerateColumns="False" StationaryMargins="Header"><ADDNEWBOX>
													<STYLE BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
													</STYLE>
												</ADDNEWBOX>
												<PAGER StyleMode="ComboBox" PageSize="17">
													<STYLE BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
													</STYLE>
												</PAGER>
												<HEADERSTYLEDEFAULT BackColor="LightGray" Height="25px" BorderStyle="Solid" Cursor="Hand">
													<BORDERDETAILS ColorLeft="White" WidthTop="1px" WidthLeft="1px" ColorTop="White"></BORDERDETAILS>
												</HEADERSTYLEDEFAULT>
												<FRAMESTYLE Font-Size="9pt" BackColor="Silver" Width="100%" Height="407px" BorderStyle="Solid"
													BorderWidth="1px" ForeColor="Transparent" BorderColor="DimGray" Font-Names="Verdana"></FRAMESTYLE>
												<FOOTERSTYLEDEFAULT BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
													<BORDERDETAILS ColorLeft="White" WidthTop="1px" WidthLeft="1px" ColorTop="White"></BORDERDETAILS>
												</FOOTERSTYLEDEFAULT>
												<SELECTEDHEADERSTYLEDEFAULT Cursor="Hand"></SELECTEDHEADERSTYLEDEFAULT>
												<EDITCELLSTYLEDEFAULT BorderStyle="None" BorderWidth="0px"></EDITCELLSTYLEDEFAULT>
												<SELECTEDROWSTYLEDEFAULT BackColor="Navy" Cursor="Hand" ForeColor="White"></SELECTEDROWSTYLEDEFAULT>
												<ROWALTERNATESTYLEDEFAULT BackColor="Transparent" Cursor="Hand"></ROWALTERNATESTYLEDEFAULT>
												<ROWSTYLEDEFAULT BackColor="#EBEFF6" BorderStyle="Solid" BorderWidth="1px" Cursor="Hand" BorderColor="Gray">
													<PADDING Left="3px"></PADDING>
													<BORDERDETAILS WidthTop="0px" WidthLeft="0px"></BORDERDETAILS>
												</ROWSTYLEDEFAULT>
											</DISPLAYLAYOUT><BANDS>
												<IGTBL:ULTRAGRIDBAND>
													<COLUMNS>
														<IGTBL:ULTRAGRIDCOLUMN Width="75px" BaseColumnName="구분1" Key="구분1" HeaderText="구분1">
															<CELLSTYLE HorizontalAlign="Left">
																<PADDING Left="4px"></PADDING>
															</CELLSTYLE>
															<HEADERSTYLE Height="25px"></HEADERSTYLE>
														</IGTBL:ULTRAGRIDCOLUMN>
														<IGTBL:ULTRAGRIDCOLUMN Width="70px" BaseColumnName="구분2" Key="구분2" HeaderText="구분2">
															<CELLSTYLE HorizontalAlign="Left">
																<PADDING Left="4px"></PADDING>
															</CELLSTYLE>
														</IGTBL:ULTRAGRIDCOLUMN>
														<IGTBL:ULTRAGRIDCOLUMN BaseColumnName="구분3" Key="구분3" HeaderText="구분3">
															<CELLSTYLE HorizontalAlign="Left">
																<PADDING Left="4px"></PADDING>
															</CELLSTYLE>
														</IGTBL:ULTRAGRIDCOLUMN>
														<IGTBL:ULTRAGRIDCOLUMN Width="100px" BaseColumnName="전월입고수량" Key="전월입고수량" HeaderText="전월입고수량" Format="###,###,###.#">
															<CELLSTYLE HorizontalAlign="Right">
																<PADDING Right="4px"></PADDING>
															</CELLSTYLE>
														</IGTBL:ULTRAGRIDCOLUMN>
														<IGTBL:ULTRAGRIDCOLUMN Width="100px" BaseColumnName="전월입고금액" Key="전월입고금액" HeaderText="전월입고금액" Format="###,###,###.#">
															<CELLSTYLE HorizontalAlign="Right">
																<PADDING Right="4px"></PADDING>
															</CELLSTYLE>
														</IGTBL:ULTRAGRIDCOLUMN>
														<IGTBL:ULTRAGRIDCOLUMN Width="90px" BaseColumnName="전월재고수량" Key="전월재고수량" HeaderText="전월재고수량" Format="###,###,###.#">
															<CELLSTYLE HorizontalAlign="Right">
																<PADDING Right="4px"></PADDING>
															</CELLSTYLE>
														</IGTBL:ULTRAGRIDCOLUMN>
														<IGTBL:ULTRAGRIDCOLUMN Width="100px" BaseColumnName="전월재고금액" Key="전월재고금액" HeaderText="전월재고금액" Format="###,###,###.#">
															<CELLSTYLE HorizontalAlign="Right">
																<PADDING Right="4px"></PADDING>
															</CELLSTYLE>
														</IGTBL:ULTRAGRIDCOLUMN>
														<IGTBL:ULTRAGRIDCOLUMN Width="100px" BaseColumnName="금월입고수량" Key="금월입고수량" HeaderText="금월입고수량" Format="###,###,###.#">
															<CELLSTYLE HorizontalAlign="Right">
																<PADDING Right="4px"></PADDING>
															</CELLSTYLE>
														</IGTBL:ULTRAGRIDCOLUMN>
														<IGTBL:ULTRAGRIDCOLUMN Width="100px" BaseColumnName="금월입고금액" Key="금월입고금액" HeaderText="금월입고금액" Format="###,###,###.#">
															<CELLSTYLE HorizontalAlign="Right">
																<PADDING Right="4px"></PADDING>
															</CELLSTYLE>
														</IGTBL:ULTRAGRIDCOLUMN>
														<IGTBL:ULTRAGRIDCOLUMN Width="90px" BaseColumnName="금월출고수량" Key="금월출고수량" HeaderText="금월출고수량" Format="###,###,###.#">
															<CELLSTYLE HorizontalAlign="Right">
																<PADDING Right="4px"></PADDING>
															</CELLSTYLE>
														</IGTBL:ULTRAGRIDCOLUMN>
														<IGTBL:ULTRAGRIDCOLUMN Width="100px" BaseColumnName="금월재고수량" Key="금월재고수량" HeaderText="금월재고수량" Format="###,###,###.#">
															<CELLSTYLE HorizontalAlign="Right">
																<PADDING Right="4px"></PADDING>
															</CELLSTYLE>
														</IGTBL:ULTRAGRIDCOLUMN>
														<IGTBL:ULTRAGRIDCOLUMN Width="100px" BaseColumnName="금월재고금액" Key="금월재고금액" HeaderText="금월재고금액" Format="###,###,###.#">
															<CELLSTYLE HorizontalAlign="Right">
																<PADDING Right="4px"></PADDING>
															</CELLSTYLE>
														</IGTBL:ULTRAGRIDCOLUMN>
														<IGTBL:ULTRAGRIDCOLUMN Width="100px" BaseColumnName="증감매입수량" Key="증감매입수량" HeaderText="증감매입수량" Format="###,###,###.#">
															<CELLSTYLE HorizontalAlign="Right">
																<PADDING Right="4px"></PADDING>
															</CELLSTYLE>
														</IGTBL:ULTRAGRIDCOLUMN>
														<IGTBL:ULTRAGRIDCOLUMN Width="100px" BaseColumnName="증감매입금액" Key="증감매입금액" HeaderText="증감매입금액" Format="###,###,###.#">
															<CELLSTYLE HorizontalAlign="Right">
																<PADDING Right="4px"></PADDING>
															</CELLSTYLE>
														</IGTBL:ULTRAGRIDCOLUMN>
														<IGTBL:ULTRAGRIDCOLUMN Width="100px" BaseColumnName="증감재고수량" Key="증감재고수량" HeaderText="증감재고수량" Format="###,###,###.#">
															<CELLSTYLE HorizontalAlign="Right">
																<PADDING Right="4px"></PADDING>
															</CELLSTYLE>
														</IGTBL:ULTRAGRIDCOLUMN>
														<IGTBL:ULTRAGRIDCOLUMN Width="100px" BaseColumnName="증감재고금액" Key="증감재고금액" HeaderText="증감재고금액" Format="###,###,###.#">
															<CELLSTYLE HorizontalAlign="Right">
																<PADDING Right="4px"></PADDING>
															</CELLSTYLE>
														</IGTBL:ULTRAGRIDCOLUMN>
													</COLUMNS>
												</IGTBL:ULTRAGRIDBAND>
											</BANDS><br>
											<DISPLAYLAYOUT CellClickActionDefault="RowSelect" TableLayout="Fixed" Name="UltraWebGrid2" RowSelectorsDefault="No"
												AllowColSizingDefault="Free" BorderCollapseDefault="Separate" AllowColumnMovingDefault="OnServer"
												SelectTypeRowDefault="Single" Version="3.00" RowHeightDefault="20px" AutoGenerateColumns="False"
												StationaryMargins="Header">
												<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="100%" Height="407px">
													<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" RowHeightDefault="20px" Version="3.00"
														SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" BorderCollapseDefault="Separate"
														AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed"
														CellClickActionDefault="RowSelect">
														<AddNewBox>
															<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
															</Style>
														</AddNewBox>
														<Pager PageSize="17" StyleMode="ComboBox">
															<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
															</Style>
														</Pager>
														<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray" Height="25px">
															<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
														</HeaderStyleDefault>
														<FrameStyle Width="100%" BorderWidth="1px" Font-Size="9pt" Font-Names="Verdana" BorderColor="DimGray"
															BorderStyle="Solid" ForeColor="Transparent" BackColor="Silver" Height="407px"></FrameStyle>
														<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
															<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
														</FooterStyleDefault>
														<SelectedHeaderStyleDefault Cursor="Hand"></SelectedHeaderStyleDefault>
														<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
														<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
														<RowAlternateStyleDefault Cursor="Hand" BackColor="Transparent"></RowAlternateStyleDefault>
														<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
															<Padding Left="3px"></Padding>
															<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
														</RowStyleDefault>
													</DisplayLayout>
													<Bands>
														<igtbl:UltraGridBand>
															<Columns>
																<igtbl:UltraGridColumn HeaderText="구분1" Key="구분1" Width="75px" BaseColumnName="구분1">
																	<CellStyle HorizontalAlign="Left">
																		<Padding Left="4px"></Padding>
																	</CellStyle>
																	<HeaderStyle Height="25px"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="구분2" Key="구분2" Width="70px" BaseColumnName="구분2">
																	<CellStyle HorizontalAlign="Left">
																		<Padding Left="4px"></Padding>
																	</CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="전월입고수량" Key="전월입고수량" Width="100px" Format="###,###,###.#" BaseColumnName="전월입고수량">
																	<CellStyle HorizontalAlign="Right">
																		<Padding Right="4px"></Padding>
																	</CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="전월입고금액" Key="전월입고금액" Width="100px" Format="###,###,###.#" BaseColumnName="전월입고금액">
																	<CellStyle HorizontalAlign="Right">
																		<Padding Right="4px"></Padding>
																	</CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="전월재고수량" Key="전월재고수량" Width="90px" Format="###,###,###.#" BaseColumnName="전월재고수량">
																	<CellStyle HorizontalAlign="Right">
																		<Padding Right="4px"></Padding>
																	</CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="전월재고금액" Key="전월재고금액" Width="100px" Format="###,###,###.#" BaseColumnName="전월재고금액">
																	<CellStyle HorizontalAlign="Right">
																		<Padding Right="4px"></Padding>
																	</CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="금월입고수량" Key="금월입고수량" Width="100px" Format="###,###,###.#" BaseColumnName="금월입고수량">
																	<CellStyle HorizontalAlign="Right">
																		<Padding Right="4px"></Padding>
																	</CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="금월입고금액" Key="금월입고금액" Width="100px" Format="###,###,###.#" BaseColumnName="금월입고금액">
																	<CellStyle HorizontalAlign="Right">
																		<Padding Right="4px"></Padding>
																	</CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="금월출고수량" Key="금월출고수량" Width="90px" Format="###,###,###.#" BaseColumnName="금월출고수량">
																	<CellStyle HorizontalAlign="Right">
																		<Padding Right="4px"></Padding>
																	</CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="금월재고수량" Key="금월재고수량" Width="100px" Format="###,###,###.#" BaseColumnName="금월재고수량">
																	<CellStyle HorizontalAlign="Right">
																		<Padding Right="4px"></Padding>
																	</CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="금월재고금액" Key="금월재고금액" Width="100px" Format="###,###,###.#" BaseColumnName="금월재고금액">
																	<CellStyle HorizontalAlign="Right">
																		<Padding Right="4px"></Padding>
																	</CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="증감매입수량" Key="증감매입수량" Width="100px" Format="###,###,###.#" BaseColumnName="증감매입수량">
																	<CellStyle HorizontalAlign="Right">
																		<Padding Right="4px"></Padding>
																	</CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="증감매입금액" Key="증감매입금액" Width="100px" Format="###,###,###.#" BaseColumnName="증감매입금액">
																	<CellStyle HorizontalAlign="Right">
																		<Padding Right="4px"></Padding>
																	</CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="증감재고수량" Key="증감재고수량" Width="100px" Format="###,###,###.#" BaseColumnName="증감재고수량">
																	<CellStyle HorizontalAlign="Right">
																		<Padding Right="4px"></Padding>
																	</CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="증감재고금액" Key="증감재고금액" Width="100px" Format="###,###,###.#" BaseColumnName="증감재고금액">
																	<CellStyle HorizontalAlign="Right">
																		<Padding Right="4px"></Padding>
																	</CellStyle>
																</igtbl:UltraGridColumn>
															</Columns>
														</igtbl:UltraGridBand>
													</Bands>
												</igtbl:ultrawebgrid></FONT><ADDNEWBOX>
											<STYLE BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
											</STYLE>
										</ADDNEWBOX>
										<PAGER StyleMode="ComboBox" PageSize="20">
											<STYLE BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
											</STYLE>
										</PAGER>
										<HEADERSTYLEDEFAULT BackColor="LightGray" Height="25px" BorderStyle="Solid" Cursor="Hand">
											<BORDERDETAILS ColorLeft="White" WidthTop="1px" WidthLeft="1px" ColorTop="White"></BORDERDETAILS>
										</HEADERSTYLEDEFAULT>
										<FRAMESTYLE Font-Size="9pt" BackColor="Silver" Width="100%" Height="450px" BorderStyle="Solid"
											BorderWidth="1px" ForeColor="Transparent" BorderColor="DimGray" Font-Names="Verdana"></FRAMESTYLE>
										<FOOTERSTYLEDEFAULT BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
											<BORDERDETAILS ColorLeft="White" WidthTop="1px" WidthLeft="1px" ColorTop="White"></BORDERDETAILS>
										</FOOTERSTYLEDEFAULT>
										<SELECTEDHEADERSTYLEDEFAULT Cursor="Hand"></SELECTEDHEADERSTYLEDEFAULT>
										<EDITCELLSTYLEDEFAULT BorderStyle="None" BorderWidth="0px"></EDITCELLSTYLEDEFAULT>
										<SELECTEDROWSTYLEDEFAULT BackColor="Navy" Cursor="Hand" ForeColor="White"></SELECTEDROWSTYLEDEFAULT>
										<ROWALTERNATESTYLEDEFAULT BackColor="Transparent" Cursor="Hand"></ROWALTERNATESTYLEDEFAULT>
										<ROWSTYLEDEFAULT BackColor="#EBEFF6" BorderStyle="Solid" BorderWidth="1px" Cursor="Hand" BorderColor="Gray">
											<PADDING Left="3px"></PADDING>
											<BORDERDETAILS WidthTop="0px" WidthLeft="0px"></BORDERDETAILS>
										</ROWSTYLEDEFAULT></DISPLAYLAYOUT><BANDS>
											<IGTBL:ULTRAGRIDBAND>
												<COLUMNS>
													<IGTBL:ULTRAGRIDCOLUMN Width="80px" BaseColumnName="ItemNum" Key="ItemNum" HeaderText="품목번호">
														<CELLSTYLE HorizontalAlign="Left">
															<PADDING Left="4px"></PADDING>
														</CELLSTYLE>
														<HEADERSTYLE Height="25px"></HEADERSTYLE>
													</IGTBL:ULTRAGRIDCOLUMN>
													<IGTBL:ULTRAGRIDCOLUMN BaseColumnName="ItemName" Key="ItemName" HeaderText="품목명">
														<CELLSTYLE HorizontalAlign="Left">
															<PADDING Left="4px"></PADDING>
														</CELLSTYLE>
													</IGTBL:ULTRAGRIDCOLUMN>
													<IGTBL:ULTRAGRIDCOLUMN Width="80px" BaseColumnName="전월입고수량" Key="전월입고수량" HeaderText="전월입고수량" Format="###,###,###.#">
														<CELLSTYLE HorizontalAlign="Right">
															<PADDING Right="4px"></PADDING>
														</CELLSTYLE>
													</IGTBL:ULTRAGRIDCOLUMN>
													<IGTBL:ULTRAGRIDCOLUMN Width="80px" BaseColumnName="전월입고금액" Key="전월입고금액" HeaderText="전월입고금액" Format="###,###,###.#">
														<CELLSTYLE HorizontalAlign="Right">
															<PADDING Right="4px"></PADDING>
														</CELLSTYLE>
													</IGTBL:ULTRAGRIDCOLUMN>
													<IGTBL:ULTRAGRIDCOLUMN Width="80px" BaseColumnName="전월재고수량" Key="전월재고수량" HeaderText="전월재고수량" Format="###,###,###.#">
														<CELLSTYLE HorizontalAlign="Right">
															<PADDING Right="4px"></PADDING>
														</CELLSTYLE>
													</IGTBL:ULTRAGRIDCOLUMN>
													<IGTBL:ULTRAGRIDCOLUMN Width="80px" BaseColumnName="전월재고금액" Key="전월재고금액" HeaderText="전월재고금액" Format="###,###,###.#">
														<CELLSTYLE HorizontalAlign="Right">
															<PADDING Right="4px"></PADDING>
														</CELLSTYLE>
													</IGTBL:ULTRAGRIDCOLUMN>
													<IGTBL:ULTRAGRIDCOLUMN Width="80px" BaseColumnName="금월입고수량" Key="금월입고수량" HeaderText="금월입고수량" Format="###,###,###.#">
														<CELLSTYLE HorizontalAlign="Right">
															<PADDING Right="4px"></PADDING>
														</CELLSTYLE>
													</IGTBL:ULTRAGRIDCOLUMN>
													<IGTBL:ULTRAGRIDCOLUMN Width="80px" BaseColumnName="금월입고금액" Key="금월입고금액" HeaderText="금월입고금액" Format="###,###,###.#">
														<CELLSTYLE HorizontalAlign="Right">
															<PADDING Right="4px"></PADDING>
														</CELLSTYLE>
													</IGTBL:ULTRAGRIDCOLUMN>
													<IGTBL:ULTRAGRIDCOLUMN Width="80px" BaseColumnName="금월출고수량" Key="금월출고수량" HeaderText="금월출고수량" Format="###,###,###.#">
														<CELLSTYLE HorizontalAlign="Right">
															<PADDING Right="4px"></PADDING>
														</CELLSTYLE>
													</IGTBL:ULTRAGRIDCOLUMN>
													<IGTBL:ULTRAGRIDCOLUMN Width="80px" BaseColumnName="금월재고수량" Key="금월재고수량" HeaderText="금월재고수량" Format="###,###,###.#">
														<CELLSTYLE HorizontalAlign="Right">
															<PADDING Right="4px"></PADDING>
														</CELLSTYLE>
													</IGTBL:ULTRAGRIDCOLUMN>
													<IGTBL:ULTRAGRIDCOLUMN Width="80px" BaseColumnName="금월재고금액" Key="금월재고금액" HeaderText="금월재고금액" Format="###,###,###.#">
														<CELLSTYLE HorizontalAlign="Right">
															<PADDING Right="4px"></PADDING>
														</CELLSTYLE>
													</IGTBL:ULTRAGRIDCOLUMN>
													<IGTBL:ULTRAGRIDCOLUMN Width="80px" BaseColumnName="증감매입수량" Key="증감매입수량" HeaderText="증감매입수량" Format="###,###,###.#">
														<CELLSTYLE HorizontalAlign="Right">
															<PADDING Right="4px"></PADDING>
														</CELLSTYLE>
													</IGTBL:ULTRAGRIDCOLUMN>
													<IGTBL:ULTRAGRIDCOLUMN Width="80px" BaseColumnName="증감매입금액" Key="증감매입금액" HeaderText="증감매입금액" Format="###,###,###.#">
														<CELLSTYLE HorizontalAlign="Right">
															<PADDING Right="4px"></PADDING>
														</CELLSTYLE>
													</IGTBL:ULTRAGRIDCOLUMN>
													<IGTBL:ULTRAGRIDCOLUMN Width="80px" BaseColumnName="증감재고수량" Key="증감재고수량" HeaderText="증감재고수량" Format="###,###,###.#">
														<CELLSTYLE HorizontalAlign="Right">
															<PADDING Right="4px"></PADDING>
														</CELLSTYLE>
													</IGTBL:ULTRAGRIDCOLUMN>
													<IGTBL:ULTRAGRIDCOLUMN Width="80px" BaseColumnName="증감재고금액" Key="증감재고금액" HeaderText="증감재고금액" Format="###,###,###.#">
														<CELLSTYLE HorizontalAlign="Right">
															<PADDING Right="4px"></PADDING>
														</CELLSTYLE>
													</IGTBL:ULTRAGRIDCOLUMN>
													<IGTBL:ULTRAGRIDCOLUMN Width="80px" BaseColumnName="Division1" Key="Division1" HeaderText="품목분류1">
														<CELLSTYLE HorizontalAlign="Left">
															<PADDING Left="4px"></PADDING>
														</CELLSTYLE>
													</IGTBL:ULTRAGRIDCOLUMN>
													<IGTBL:ULTRAGRIDCOLUMN Width="80px" BaseColumnName="Division2" Key="Division2" HeaderText="품목분류2">
														<CELLSTYLE HorizontalAlign="Left">
															<PADDING Left="4px"></PADDING>
														</CELLSTYLE>
													</IGTBL:ULTRAGRIDCOLUMN>
													<IGTBL:ULTRAGRIDCOLUMN Width="80px" BaseColumnName="Division3" Key="Division3" HeaderText="품목분류3">
														<CELLSTYLE HorizontalAlign="Left">
															<PADDING Left="4px"></PADDING>
														</CELLSTYLE>
													</IGTBL:ULTRAGRIDCOLUMN>
													<IGTBL:ULTRAGRIDCOLUMN Width="80px" BaseColumnName="Division4" Key="Division4" HeaderText="품목분류4">
														<CELLSTYLE HorizontalAlign="Left">
															<PADDING Left="4px"></PADDING>
														</CELLSTYLE>
													</IGTBL:ULTRAGRIDCOLUMN>
												</COLUMNS>
											</IGTBL:ULTRAGRIDBAND>
										</BANDS></FONT></FONT></TD>
								</TR>
								<TR>
									<TD align="left" width="65" height="30"><FONT face="굴림">&nbsp;</FONT>
										<asp:button id="Button1" runat="server" Height="20px" Width="60px" Font-Size="10pt" Text="Excel"></asp:button></TD>
									<TD align="right" width="65" height="30"><FONT face="굴림">&nbsp;</FONT></TD>
									<TD align="left" colSpan="4" height="30" width="670">&nbsp;&nbsp;
										<igtblexp:UltraWebGridExcelExporter id="UWGExcel" runat="server"></igtblexp:UltraWebGridExcelExporter></TD>
								</TR>
							</TABLE>
							<P></P>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
