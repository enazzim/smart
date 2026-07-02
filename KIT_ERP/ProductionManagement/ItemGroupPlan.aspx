<%@ Page language="c#" Codebehind="ItemGroupPlan.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.ItemGroupPlan" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ItemGroupPlan</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!--
		
		//-->
		</script>
</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<FONT face="굴림">
				<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" height="550"
					cellSpacing="0" cellPadding="0" width="800" border="0">
					<TR>
						<TD width="20"><FONT face="굴림"></FONT></TD>
						<TD style="PADDING-TOP: 10px" vAlign="top" align="left" width="550">
							<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 805px; BORDER-BOTTOM: #696969 2px solid; HEIGHT: 113px">
								<P><LEGEND style="FONT-SIZE: 9pt" align="top">[입력]
									</LEGEND>
								</P>
								<P><FONT face="굴림">
										<igtblexp:UltraWebGridExcelExporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:UltraWebGridExcelExporter></FONT></P>
								<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800">
									<TR>
										<TD align="left" width="540"><INPUT id="Find" style="FONT-SIZE: 9pt; WIDTH: 400px; HEIGHT: 20px; BACKGROUND-COLOR: #eeeee9"
												type="file" size="71" name="Find" runat="server"><FONT face="굴림">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												<asp:hyperlink id="HyperLink1" runat="server" NavigateUrl="../ExcelDown/ItemGroup.xls" Font-Size="9pt">Excel양식 다운로드</asp:hyperlink>
											</FONT>
										</TD>
										<TD align="right" width="260">
											<asp:button id="btSearch" runat="server" Height="20px" Width="60px" Text="에러List" Font-Size="10pt"></asp:button>&nbsp;
											<asp:button id="bt_Register" runat="server" Font-Size="10pt" Text="등  록" Width="60px" Height="20px"></asp:button></TD>
									</TR>
								</TABLE>
							</FIELDSET>
							<BR>
							<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 806px; BORDER-BOTTOM: #696969 2px solid; HEIGHT: 366px"><LEGEND style="FONT-SIZE: 9pt" align="top">[입력결과]</LEGEND>
								<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="390px">
									<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
										RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
										HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
										RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect"
										AllowUpdateDefault="RowTemplateOnly">
										<AddNewBox>
											<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
											</Style>
										</AddNewBox>
										<Pager PageSize="17" StyleMode="ComboBox" AllowPaging="True">
											<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
											</Style>
										</Pager>
										<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray">
											<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
										</HeaderStyleDefault>
										<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
										<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
											BackColor="Silver" Height="390px"></FrameStyle>
										<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
											<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
										</FooterStyleDefault>
										<ClientSideEvents AfterSelectChangeHandler="UltraWebGrid1_AfterSelectChangeHandler"></ClientSideEvents>
										<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
										<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
										<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
										<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
											<Padding Left="3px"></Padding>
											<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
										</RowStyleDefault>
									</DisplayLayout>
									<Bands>
										<igtbl:UltraGridBand AllowUpdate="RowTemplateOnly">
											<Columns>
												<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemGroup" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemGroup">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle Height="25px"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Width="150px" HeaderClickAction="SortMulti" 
 BaseColumnName="ItemDrawNum">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="계획일자" Key="ReceivingOrderDate" Width="120px" Format="yyyy-MM-dd" HeaderClickAction="SortMulti" 
 BaseColumnName="ReceivingOrderDate">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="수량" Key="Quantity" Width="108px" Format="###,###,###.##" HeaderClickAction="SortMulti" 
 BaseColumnName="Quantity">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
														<Padding Right="4px"></Padding>
													</CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="고객생산시작일" Key="DeliveryDate" Width="120px" Format="yyyy-MM-dd" HeaderClickAction="SortMulti" 
 BaseColumnName="DeliveryDate">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="번호" Key="ItemGroupIndex" Hidden="True" HeaderClickAction="SortMulti" 
 BaseColumnName="ItemGroupIndex">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
											</Columns>
										</igtbl:UltraGridBand>
									</Bands>
								</igtbl:ultrawebgrid>
								<igtbl:ultrawebgrid id="UltraWebGrid2" runat="server" Height="390px" Width="800px" Visible="False">
									<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
										RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
										HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
										RowSelectorsDefault="No" Name="UltraWebGrid2" TableLayout="Fixed" CellClickActionDefault="RowSelect"
										AllowUpdateDefault="RowTemplateOnly">
										<AddNewBox>
											<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
											</Style>
										</AddNewBox>
										<Pager PageSize="17" StyleMode="ComboBox" AllowPaging="True">
											<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
											</Style>
										</Pager>
										<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray">
											<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
										</HeaderStyleDefault>
										<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
										<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
											BackColor="Silver" Height="390px"></FrameStyle>
										<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
											<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
										</FooterStyleDefault>
										<ClientSideEvents AfterSelectChangeHandler="UltraWebGrid1_AfterSelectChangeHandler"></ClientSideEvents>
										<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
										<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
										<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
										<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
											<Padding Left="3px"></Padding>
											<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
										</RowStyleDefault>
									</DisplayLayout>
									<Bands>
										<igtbl:UltraGridBand AllowUpdate="RowTemplateOnly">
											<Columns>
												<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle Height="25px"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" Width="150px" BaseColumnName="CompanyName">
													<CellStyle HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="사업자번호" Key="BusinessRegistrationNum" Hidden="True" BaseColumnName="BusinessRegistrationNum"></igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" Hidden="True" HeaderClickAction="SortMulti" 
 BaseColumnName="ItemName">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="에러사유" Key="ErrRegion" Width="150px" BaseColumnName="ErrRegion">
													<CellStyle HorizontalAlign="Center"></CellStyle>
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="수주일자" Key="ReceivingOrderDate" Width="120px" Format="yyyy-MM-dd" HeaderClickAction="SortMulti" 
 BaseColumnName="ReceivingOrderDate">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="수량" Key="TotalReceiveingOrderQuantity" Width="108px" Format="###,###,###.##" 
 HeaderClickAction="SortMulti" BaseColumnName="TotalReceiveingOrderQuantity">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
														<Padding Right="4px"></Padding>
													</CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="납기일" Key="DeliveryRequestDate" Width="120px" Format="yyyy-MM-dd" HeaderClickAction="SortMulti" 
 BaseColumnName="DeliveryRequestDate">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="번호" Key="ItemGroupErrIndex" Hidden="True" HeaderClickAction="SortMulti" 
 BaseColumnName="ItemGroupErrIndex">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
											</Columns>
										</igtbl:UltraGridBand>
									</Bands>
								</igtbl:ultrawebgrid><BR>
								<TABLE id="table3" cellSpacing="0" cellPadding="0" width="800" border="0">
									<TR>
										<TD width="550" height="30">
											<asp:button id="Button3" runat="server" Text="Excel" Width="60px" Height="20px"></asp:button></TD>
									</TR>
								</TABLE>
							</FIELDSET>
						</TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
