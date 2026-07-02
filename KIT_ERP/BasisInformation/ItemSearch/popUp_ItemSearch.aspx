<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="popUp_ItemSearch.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.ItemSearch.popUp_ItemSearch" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>품목검색</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<base target="_self">
		<SCRIPT type="text/javascript"><!--
		function UltraWebGrid1_AfterRowActivateHandler(gridName, rowId){
			var objRow = igtbl_getActiveRow(gridName);
			var ItemNum = objRow.getCellFromKey("itemnum").getValue();
			var ItemDrawNum = objRow.getCellFromKey("itemdrawnum").getValue();
			var ItemName = objRow.getCellFromKey("itemname").getValue();
			var arrReturnValues = new Array();
			arrReturnValues["ItemNum"] = ItemNum;
			arrReturnValues["ItemDrawNum"] = ItemDrawNum;
			arrReturnValues["ItemName"] = ItemName;
			window.returnValue = arrReturnValues;
			window.close();
		}
--></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="800" border="0">
				<tr>
					<td vAlign="top" height="330"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="330px" Width="800px">
							<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" RowHeightDefault="20px" Version="3.00"
								BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1"
								TableLayout="Fixed" CellClickActionDefault="RowSelect">
								<AddNewBox>
									<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

									</Style>
								</AddNewBox>
								<Pager QuickPages="10" PageSize="14" StyleMode="ComboBox" Alignment="Center" AllowPaging="True">
									<Style BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderStyle="Solid" BackColor="LightGray"
										Height="20px">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

									</Style>
								</Pager>
								<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray" Height="23px">
									<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
								</HeaderStyleDefault>
								<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DimGray"
									BorderStyle="Solid" Height="330px"></FrameStyle>
								<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
									<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
								</FooterStyleDefault>
								<ClientSideEvents AfterRowActivateHandler="UltraWebGrid1_AfterRowActivateHandler"></ClientSideEvents>
								<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
								<RowAlternateStyleDefault BackColor="LightSteelBlue"></RowAlternateStyleDefault>
								<RowStyleDefault BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
									<Padding Left="3px"></Padding>
									<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
								</RowStyleDefault>
							</DisplayLayout>
							<Bands>
								<igtbl:UltraGridBand>
									<Columns>
										<igtbl:UltraGridColumn HeaderText="품목번호" Key="itemnum" Width="200px" BaseColumnName="품목번호"></igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="도면번호" Key="itemdrawnum" Width="200px" BaseColumnName="도면번호"></igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="품목명" Key="itemname" Width="200px" BaseColumnName="품목명"></igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="규격" Key="Standard" Width="198px" BaseColumnName="Standard"></igtbl:UltraGridColumn>
									</Columns>
								</igtbl:UltraGridBand>
							</Bands>
						</igtbl:ultrawebgrid></td>
				</tr>
				<tr>
					<td align="right">&nbsp;
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
