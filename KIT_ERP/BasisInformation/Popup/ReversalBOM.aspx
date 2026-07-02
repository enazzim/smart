<%@ Page language="c#" Codebehind="ReversalBOM.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.Popup.ReversalBOM" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearch" Src="ReversalItem/ItemSearch.ascx" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ReversalBOM</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../StyleSheet2.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" style="LEFT: 5px; POSITION: absolute; TOP: 10px" cellSpacing="0" cellPadding="0"
				width="800" border="0">
				<TR>
					<TD height="20">
						<table cellSpacing="0" cellPadding="0" width="800" border="0">
							<tr>
								<td colspan="3" height="20" align="left">
									<uc1:ItemSearch id="ItemSearch1" runat="server"></uc1:ItemSearch>
								</td>
							</tr>
							<tr>
								<td align="right" width="70"><FONT face="굴림">단계설정&nbsp;</FONT>
								</td>
								<td align="left" width="200"><asp:radiobutton id="rbNext" runat="server" Text="모품목 전개" GroupName="Level" Checked="True"></asp:radiobutton><asp:radiobutton id="rbEnd" runat="server" Text="최종품 전개" GroupName="Level"></asp:radiobutton></td>
								<td align="right" width="530">
									<asp:Button id="Button1" runat="server" Width="80px" Height="20px" Text="역전개 풀기"></asp:Button></td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD height="5"></TD>
				</TR>
				<TR>
					<TD height="10">
						<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="445px">
							<DisplayLayout ColFootersVisibleDefault="Yes" StationaryMargins="Header" AutoGenerateColumns="False"
								RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
								HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
								RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect">
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
								<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="Verdana" BorderColor="DimGray"
									BorderStyle="Solid" BackColor="Silver" Height="445px"></FrameStyle>
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
								<igtbl:UltraGridBand>
									<Columns>
										<igtbl:UltraGridColumn HeaderText="자품목번호" Key="ChildItemNum" Width="170px" BaseColumnName="ChildItemNum">
											<HeaderStyle Height="25px"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="자품목명" Key="ChildItemName" Width="170px" BaseColumnName="ChildItemName"></igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="최종품목번호" Key="EndItemNum" Width="170px" BaseColumnName="EndItemNum">
											<CellStyle VerticalAlign="Middle"></CellStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="최종품목명" Key="EndItemName" Width="170px" BaseColumnName="EndItemName"></igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="자품소요량" Key="NeedQuantity" Width="115px" Format="###,###,###.##" BaseColumnName="NeedQuantity">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
												<Padding Right="4px"></Padding>
											</CellStyle>
										</igtbl:UltraGridColumn>
									</Columns>
								</igtbl:UltraGridBand>
							</Bands>
						</igtbl:ultrawebgrid>
						<igtbl:ultrawebgrid id="UltraWebGrid2" runat="server" Width="800px" Height="445px">
							<DisplayLayout ColFootersVisibleDefault="Yes" StationaryMargins="Header" AutoGenerateColumns="False"
								RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
								HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
								RowSelectorsDefault="No" Name="UltraWebGrid2" TableLayout="Fixed" CellClickActionDefault="RowSelect">
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
								<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="Verdana" BorderColor="DimGray"
									BorderStyle="Solid" BackColor="Silver" Height="445px"></FrameStyle>
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
								<igtbl:UltraGridBand>
									<Columns>
										<igtbl:UltraGridColumn HeaderText="자품목번호" Key="ChildItemNum" Width="170px" BaseColumnName="ChildItemNum">
											<HeaderStyle Height="25px"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="자품목명" Key="ChildItemName" Width="170px" BaseColumnName="ChildItemName"></igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="모품목번호" Key="ParentItemNum" Width="170px" BaseColumnName="ParentItemNum">
											<CellStyle VerticalAlign="Middle"></CellStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="모품목명" Key="ParentItemName" Width="170px" BaseColumnName="ParentItemName"></igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="자품소요량" Key="NeedQuantity" Width="115px" Format="###,###,###.##" BaseColumnName="NeedQuantity">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
												<Padding Right="4px"></Padding>
											</CellStyle>
										</igtbl:UltraGridColumn>
									</Columns>
								</igtbl:UltraGridBand>
							</Bands>
						</igtbl:ultrawebgrid></TD>
				</TR>
				<TR>
					<TD>&nbsp;</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
