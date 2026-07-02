<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="RealWorkStandard.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.Popup.RealWorkStandard" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RealWorkStandardInfo</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<STYLE type="text/css">BODY { SCROLLBAR-FACE-COLOR: #ffffff; SCROLLBAR-HIGHLIGHT-COLOR: #ffffff; SCROLLBAR-SHADOW-COLOR: #ffffff; SCROLLBAR-3DLIGHT-COLOR: #808080; SCROLLBAR-ARROW-COLOR: #400040; SCROLLBAR-TRACK-COLOR: #ffffff; SCROLLBAR-DARKSHADOW-COLOR: #808080 }
		</STYLE>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="buttonface">
		<form id="Form1" method="post" runat="server">
			<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; Z-INDEX: 101; LEFT: 8px; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; POSITION: absolute; TOP: 8px; HEIGHT: 450px"
				align="left"><LEGEND>[ 진작업표준정보 ]</LEGEND>
				<asp:Button id="bt_Excel" runat="server" Text="Excel" Width="60px" Height="20px"></asp:Button>
				<igtblexp:UltraWebGridExcelExporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:UltraWebGridExcelExporter>
				<table WIDTH="800" HEIGHT="450" cellSpacing="0" cellPadding="0">
					<tr>
						<td WIDTH="800" HEIGHT="10">
							<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="447px">
								<DisplayLayout CompactRendering="False" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
									RowHeightDefault="20px" RowSizingDefault="Free" Version="3.00" SelectTypeRowDefault="Single"
									AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate"
									AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed"
									CellClickActionDefault="RowSelect">
									<AddNewBox>
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

										</Style>
									</AddNewBox>
									<Pager PageSize="19" StyleMode="ComboBox" AllowPaging="True">
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

										</Style>
									</Pager>
									<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</HeaderStyleDefault>
									<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
										BackColor="Silver" Height="447px"></FrameStyle>
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
									<igtbl:UltraGridBand AllowSorting="OnClient" AllowColSizing="Free" AllowColumnMoving="OnServer" RowSizing="Free">
										<Columns>
											<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" HeaderClickAction="SortMulti" BaseColumnName="ItemDrawNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="공정순서" Key="ProcessSequenceNum" HeaderClickAction="SortMulti" BaseColumnName="ProcessSequenceNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="공정코드" Key="ProcessCode" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="ProcessCode">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="공정명" Key="SmallClassificationName" HeaderClickAction="SortMulti" BaseColumnName="SmallClassificationName">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="WC명" Key="WCName" HeaderClickAction="SortMulti" BaseColumnName="WCName">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="우선순위" Key="PriorityOrder" HeaderClickAction="SortMulti" BaseColumnName="PriorityOrder">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="주작업자ID" Key="MainWorkerID" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="MainWorkerID">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="주작업자" Key="MainWorker" HeaderClickAction="SortMulti" BaseColumnName="MainWorker">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="사용공구1" Key="ToolName1" HeaderClickAction="SortMulti" BaseColumnName="ToolName1">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="사용공구2" Key="ToolName2" HeaderClickAction="SortMulti" BaseColumnName="ToolName2">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="사용공구3" Key="ToolName3" HeaderClickAction="SortMulti" BaseColumnName="ToolName3">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="사용치구1" Key="JigName1" HeaderClickAction="SortMulti" BaseColumnName="JigName1">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="사용치구2" Key="JigName2" HeaderClickAction="SortMulti" BaseColumnName="JigName2">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="사용치구3" Key="JigName3" HeaderClickAction="SortMulti" BaseColumnName="JigName3">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="셋업타임" Key="SetupTime" Format="###,###,###.##" HeaderClickAction="SortMulti"
												BaseColumnName="SetupTime">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="실가공시간" Key="RealProcessingTime" Format="###,###,###.##" HeaderClickAction="SortMulti"
												BaseColumnName="RealProcessingTime">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="여유시간" Key="SpaceTime" Format="###,###,###.##" HeaderClickAction="SortMulti"
												BaseColumnName="SpaceTime">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="표준시간" Key="StandardTime" Format="###,###,###.##" HeaderClickAction="SortMulti"
												BaseColumnName="StandardTime">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="대기시간" Key="WaitTime" Format="###,###,###.##" HeaderClickAction="SortMulti"
												BaseColumnName="WaitTime">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="LotSize" Key="LotSize" Format="###,###,###.##" HeaderClickAction="SortMulti"
												BaseColumnName="LotSize">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="Cavity" Key="Cavity" Format="###,###,###.##" HeaderClickAction="SortMulti"
												BaseColumnName="Cavity">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="레코드상태" Key="RecodingState" Hidden="True" BaseColumnName="RecodingState">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPerson">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="RegistrationDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" HeaderClickAction="SortMulti" BaseColumnName="UpdatingPerson">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="UpdatingDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="번호" Key="RealWorkStandardInfoIndex" Hidden="True" BaseColumnName="RealWorkStandardInfoIndex">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
										</Columns>
									</igtbl:UltraGridBand>
								</Bands>
							</igtbl:ultrawebgrid>
						</td>
					</tr>
				</table>
			</FIELDSET>
		</form>
	</body>
</HTML>
