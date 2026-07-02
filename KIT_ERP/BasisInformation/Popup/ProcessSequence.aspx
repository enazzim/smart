<%@ Page language="c#" Codebehind="ProcessSequence.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.Popup.ProcessSequence" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ProcessSequence</title>
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
				align="left"><LEGEND>[ 공정순서 ]</LEGEND>
				<asp:Button id="bt_Excel" runat="server" Width="60px" Height="20px" Text="Excel"></asp:Button>
				<table WIDTH="800" HEIGHT="450">
					<tr>
						<td WIDTH="850" HEIGHT="10">
							<igtbl:UltraWebGrid id="UltraWebGrid1" runat="server" Height="450px" Width="800px">
								<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
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
									<Pager PageSize="19" StyleMode="ComboBox" Alignment="Center" AllowPaging="True">
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
										BackColor="Silver" Height="450px"></FrameStyle>
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
											<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" BaseColumnName="ItemNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle Height="25px"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" BaseColumnName="ItemDrawNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" BaseColumnName="ItemName">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="공정순서번호" Key="ProcessSequenceNum" BaseColumnName="ProcessSequenceNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="공정코드" Key="ProcessCode" Hidden="True" BaseColumnName="ProcessCode">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="공정명" Key="SmallClassificationName" BaseColumnName="SmallClassificationName">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="작업구분" Key="WorkDistinction" BaseColumnName="WorkDistinction">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="WC명" Key="WCName" BaseColumnName="WCName">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="발주비율" Key="OutsideOrderRate" BaseColumnName="OutsideOrderRate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="진척비율" Key="ProgressRate" BaseColumnName="ProgressRate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="리드타임" Key="LeadTime" BaseColumnName="LeadTime">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="기타" Key="EtcText" BaseColumnName="EtcText"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="레코드상태" Key="RecodingState" Hidden="True" BaseColumnName="RecodingState">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" BaseColumnName="RegistrationPerson">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" BaseColumnName="UpdatingPerson">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="번호" Key="ProcessSequenceInfoIndex" BaseColumnName="ProcessSequenceInfoIndex">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
										</Columns>
									</igtbl:UltraGridBand>
								</Bands>
							</igtbl:UltraWebGrid>
						</td>
					</tr>
				</table>
			</FIELDSET>
			<igtblexp:UltraWebGridExcelExporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:UltraWebGridExcelExporter>
		</form>
	</body>
</HTML>
