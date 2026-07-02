<%@ Page language="c#" Codebehind="UnitCostView.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.EtcPresentCondition.UnitCostView" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>UnitCostView</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--
		function ResettxtBox()
		{
			
			ResetTextBox();
			ResetBox();
			document.Form1.ddlDistinction.options[0].selected=true;			
					
		}
		//-->
		</SCRIPT>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; WIDTH: 800px; POSITION: absolute; TOP: 10px; HEIGHT: 550px"
				cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD width="20" height="60"><FONT face="굴림"></FONT></TD>
					<TD colSpan="2" height="60">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 811px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 78px"
							align="top"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색 ]
							</LEGEND>
							<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
								<TR>
									<td width="800" colSpan="9" height="30">
										<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="800" border="0">
											<tr>
												<TD align="left" width="200" height="30"><uc1:companysearchcontrol id="CSC1" runat="server"></uc1:companysearchcontrol></TD>
												<TD align="left" width="600" height="30"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></TD>
											</tr>
										</TABLE>
									</td>
								</TR>
								<TR>
									<TD align="right" width="70" height="30"><FONT face="굴림">단가구분</FONT>&nbsp;</TD>
									<TD width="100" height="30"><asp:dropdownlist id="ddlDistinction" runat="server" Font-Size="10pt" Width="100px" BackColor="#EEEEE9"
											Height="20px">
											<asp:ListItem Value="전체">전체</asp:ListItem>
											<asp:ListItem Value="판매단가">판매</asp:ListItem>
											<asp:ListItem Value="구매단가">구매</asp:ListItem>
											<asp:ListItem Value="외주단가">외주</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD align="right" width="630" height="30" colSpan="7">&nbsp;<FONT face="굴림"></FONT><FONT face="굴림"></FONT><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
											type="button" value="초기화" name="btnReset">&nbsp;
										<asp:button id="btnSearch" runat="server" Height="20px" Width="60px" Font-Size="9pt" Text="검   색"
											CommandName="Search"></asp:button>&nbsp;</TD>
								</TR>
								<TR>
									<TD align="right" width="70" colSpan="9" height="8"></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="20" height="400"></TD>
					<TD vAlign="top" height="400">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 811px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 400px"
							align="top"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색결과 ]
							</LEGEND>
							<TABLE id="Table3" style="WIDTH: 100%; HEIGHT: 400px" cellSpacing="0" cellPadding="0">
								<TR>
									<TD vAlign="middle" align="right" width="875">&nbsp;&nbsp;&nbsp;</TD>
								</TR>
								<TR>
									<TD vAlign="top" align="center" width="875"><igtbl:ultrawebgrid id="uwg" runat="server" Width="100%" Height="428px">
											<DisplayLayout AutoGenerateColumns="False" AllowSortingDefault="Yes" RowHeightDefault="20px" Version="3.00"
												SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti"
												BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="uwg"
												CellClickActionDefault="RowSelect">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</AddNewBox>
												<Pager PageSize="18" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray" Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<FrameStyle Width="100%" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="428px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="Gray"
													BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="단가구분" Key="UnitCostDistinction" Width="60px" BaseColumnName="UnitCostDistinction"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" Width="130px" BaseColumnName="CompanyName">
															<CellStyle HorizontalAlign="Left">
																<Margin Left="4px"></Margin>
															</CellStyle>
															<HeaderStyle Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="180px" BaseColumnName="ItemNum">
															<CellStyle HorizontalAlign="Left">
																<Margin Left="4px"></Margin>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="180px" BaseColumnName="ItemName">
															<CellStyle HorizontalAlign="Left">
																<Margin Left="4px"></Margin>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="규격" Key="Standard" Width="150px" BaseColumnName="Standard">
															<CellStyle>
																<Margin Left="4px"></Margin>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="시작공정" Key="StartProcess" Width="60px" BaseColumnName="StartProcess">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="종료공정" Key="EndProcess" Width="60px" BaseColumnName="EndProcess">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" BaseColumnName="BusinessRegistrationNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="적용단가" Key="StandardUnitCost" Width="73px" Format="###,###,##0.00" BaseColumnName="StandardUnitCost">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="발주비율" Key="OrderRate" Width="60px" Format="###,###,##0.00" BaseColumnName="OrderRate">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="20"><FONT face="굴림"></FONT></TD>
					<TD>
						<TABLE id="Table4" style="WIDTH: 100%; HEIGHT: 8px" height="8" cellSpacing="0" cellPadding="0"
							width="100%">
							<TR>
								<TD style="WIDTH: 500px" align="left" height="5"></TD>
								<TD align="right" colSpan="3" height="5"></TD>
								<TD align="right" colSpan="3" height="5"></TD>
								<TD align="right" height="5"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 500px" align="left"><asp:button id="btnExcel" runat="server" Font-Size="9pt" Width="65px" Height="20px" Text="Excel"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<igtblexp:ultrawebgridexcelexporter id="uwgExcel" runat="server"></igtblexp:ultrawebgridexcelexporter></TD>
								<TD align="right" colSpan="7">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			&nbsp;
			<P align="right">&nbsp;</P>
			</TD></TR></TABLE></form>
	</body>
</HTML>
