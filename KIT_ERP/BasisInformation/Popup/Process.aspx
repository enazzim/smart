<%@ Page language="c#" Codebehind="Process.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.Popup.Process" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Process</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<STYLE type="text/css">BODY { SCROLLBAR-FACE-COLOR: #ffffff; SCROLLBAR-HIGHLIGHT-COLOR: #ffffff; SCROLLBAR-SHADOW-COLOR: #ffffff; SCROLLBAR-3DLIGHT-COLOR: #808080; SCROLLBAR-ARROW-COLOR: #400040; SCROLLBAR-TRACK-COLOR: #ffffff; SCROLLBAR-DARKSHADOW-COLOR: #808080 }
		</STYLE>
		<SCRIPT type="text/javascript"><!--

		function UltraWebGrid1_AfterSelectChangeHandler(gridName, id){
			var row = igtbl_getRowById(id);
			var parts = id.split("_");  
			Process.ItemIndex.value = row.getCellFromKey("ProcessSequenceInfoIndex").getValue();
			Process.RowIndex.value = parts[1];  
		}
--></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="buttonface">
		<form id="Process" method="post" runat="server">
			<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 0pt; Z-INDEX: 101; LEFT: 8px; BORDER-LEFT: dimgray 2px solid; WIDTH: 80.55%; BORDER-BOTTOM: dimgray 2px solid; POSITION: absolute; TOP: 8px; HEIGHT: 384px"><LEGEND align="top"><asp:label id="Label2" Runat="server" Font-Size="10pt"> [결&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;과]</asp:label></LEGEND><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="375px" Width="800px">
					<DisplayLayout AutoGenerateColumns="False" AllowSortingDefault="OnClient" RowHeightDefault="20px"
						Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti"
						BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1"
						TableLayout="Fixed" CellClickActionDefault="RowSelect">
						<AddNewBox>
							<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

							</Style>
						</AddNewBox>
						<Pager PageSize="16" StyleMode="ComboBox" Alignment="Center" AllowPaging="True">
							<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

							</Style>
						</Pager>
						<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
							<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
						</HeaderStyleDefault>
						<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
						<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
							BackColor="Silver" Height="375px"></FrameStyle>
						<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
							<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
						</FooterStyleDefault>
						<ClientSideEvents AfterSelectChangeHandler="UltraWebGrid1_AfterSelectChangeHandler"></ClientSideEvents>
						<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
						<SelectedRowStyleDefault Cursor="Hand" BackColor="#E0E5CD"></SelectedRowStyleDefault>
						<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
						<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EDEFF6">
							<Padding Left="3px"></Padding>
							<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
						</RowStyleDefault>
					</DisplayLayout>
					<Bands>
						<igtbl:UltraGridBand>
							<Columns>
								<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="200px" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
									<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
									<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
								</igtbl:UltraGridColumn>
								<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Width="200px" HeaderClickAction="SortMulti"
									BaseColumnName="ItemDrawNum">
									<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
									<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
								</igtbl:UltraGridColumn>
								<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="200px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
									<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
									<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
								</igtbl:UltraGridColumn>
								<igtbl:UltraGridColumn HeaderText="번호" Key="ItemInfoIndex" Hidden="True" BaseColumnName="ItemInfoIndex">
									<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
									<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
								</igtbl:UltraGridColumn>
								<igtbl:UltraGridColumn HeaderText="자산분류" Key="PropertyClassification" Hidden="True" BaseColumnName="PropertyClassification">
									<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
								</igtbl:UltraGridColumn>
								<igtbl:UltraGridColumn HeaderText="단위" Key="Unit" Hidden="True" BaseColumnName="Unit">
									<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
								</igtbl:UltraGridColumn>
								<igtbl:UltraGridColumn HeaderText="규격" Key="Standard" Hidden="True" BaseColumnName="Standard">
									<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
								</igtbl:UltraGridColumn>
								<igtbl:UltraGridColumn HeaderText="공정순서" Key="ProcessSequenceNum" Width="80px" HeaderClickAction="SortMulti"
									BaseColumnName="ProcessSequenceNum">
									<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
									<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
								</igtbl:UltraGridColumn>
								<igtbl:UltraGridColumn HeaderText="공정명" Key="ProcessName" Width="116px" HeaderClickAction="SortMulti" BaseColumnName="ProcessName">
									<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
									<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
								</igtbl:UltraGridColumn>
								<igtbl:UltraGridColumn HeaderText="공정코드" Key="ProcessCode" Hidden="True" BaseColumnName="ProcessCode">
									<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
									<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
								</igtbl:UltraGridColumn>
								<igtbl:UltraGridColumn HeaderText="번호" Key="ProcessSequenceInfoIndex" Hidden="True" BaseColumnName="ProcessSequenceInfoIndex">
									<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
									<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
								</igtbl:UltraGridColumn>
							</Columns>
						</igtbl:UltraGridBand>
					</Bands>
				</igtbl:ultrawebgrid></FIELDSET>
			<INPUT id="ItemIndex" style="Z-INDEX: 102; LEFT: 8px; WIDTH: 32px; POSITION: absolute; TOP: 392px; HEIGHT: 22px"
				type="hidden" size="1" name="ItemIndex" runat="server"> <INPUT id="RowIndex" style="Z-INDEX: 103; LEFT: 48px; WIDTH: 32px; POSITION: absolute; TOP: 392px; HEIGHT: 22px"
				type="hidden" size="1" name="RowIndex" runat="server"></form>
	</body>
</HTML>
