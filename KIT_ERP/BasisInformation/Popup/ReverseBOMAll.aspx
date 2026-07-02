<%@ Register TagPrefix="uc1" TagName="ItemSearch" Src="ReversalItem/ItemSearch.ascx" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="ReverseBOMAll.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.Popup.ReverseBOMAll" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ReverseBOMAll</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../StyleSheet2.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" style="POSITION: absolute; TOP: 10px; LEFT: 5px" cellSpacing="0" cellPadding="0"
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
								<td><asp:Button style="Z-INDEX: 0" id="bt_Excel" runat="server" Text="Excel" Height="20px" Width="60px"></asp:Button>
								</td>
								<td align="right" width="68" style="WIDTH: 740px" colSpan="2">
									<asp:Button id="Button1" runat="server" Text="역전개 풀기" Height="20px"></asp:Button>
								</td>
							</tr>
						</table>
					</TD>
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
										<igtbl:UltraGridColumn HeaderText="자품목번호" Key="ChildItemNum" Width="120px" BaseColumnName="ChildItemNum">
											<HeaderStyle Height="25px"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="자품목명" Key="ChildItemName" Width="120px" BaseColumnName="ChildItemName"></igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="모품목번호" Key="ParentItemNum" Width="120px" BaseColumnName="ParentItemNum"></igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="모품목명" Key="ParentItemName" Width="120px" BaseColumnName="ParentItemName"></igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="최종품목번호" Key="EndItemNum" Width="120px" BaseColumnName="EndItemNum">
											<CellStyle VerticalAlign="Middle"></CellStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="최종품목명" Key="EndItemName" Width="120px" BaseColumnName="EndItemName"></igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="자품소요량" Key="NeedQuantity" Width="78px" Format="###,###,###.##" BaseColumnName="NeedQuantity">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
												<Padding Right="4px"></Padding>
											</CellStyle>
										</igtbl:UltraGridColumn>
									</Columns>
								</igtbl:UltraGridBand>
							</Bands>
						</igtbl:ultrawebgrid></TD>
				</TR>
			</TABLE>
			<igtblexp:UltraWebGridExcelExporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:UltraWebGridExcelExporter>
		</form>
	</body>
</HTML>
