<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="BusinessPlan.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.Popup.BusinessPlan" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BusinessPlan</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<STYLE type="text/css">BODY { SCROLLBAR-FACE-COLOR: #ffffff; SCROLLBAR-HIGHLIGHT-COLOR: #ffffff; SCROLLBAR-SHADOW-COLOR: #ffffff; SCROLLBAR-3DLIGHT-COLOR: #808080; SCROLLBAR-ARROW-COLOR: #400040; SCROLLBAR-TRACK-COLOR: #ffffff; SCROLLBAR-DARKSHADOW-COLOR: #808080 }
		</STYLE>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="buttonface">
		<form id="Form1" method="post" runat="server">
			<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; Z-INDEX: 101; LEFT: 8px; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; POSITION: absolute; TOP: 8px; HEIGHT: 480px"
				align="left"><LEGEND>[ 사업계획 ]</LEGEND>
				<table style="HEIGHT: 432px" height="432" width="800">
					<tr>
						<td width="850" height="10">
							<asp:Button id="bt_Excel" runat="server" Height="20px" Width="60px" Text="Excel"></asp:Button>
							<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="430px" Width="800px">
								<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
									RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
									HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
									RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect">
									<AddNewBox>
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

										</Style>
									</AddNewBox>
									<Pager PageSize="20" Alignment="Center" AllowPaging="True">
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
										Height="430px"></FrameStyle>
									<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</FooterStyleDefault>
									<ClientSideEvents AfterSelectChangeHandler="UltraWebGrid1_AfterSelectChangeHandler"></ClientSideEvents>
									<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
									<SelectedRowStyleDefault ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
									<RowAlternateStyleDefault BorderColor="DarkGray" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
									<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid" BackColor="#EBEFF6">
										<Padding Left="3px"></Padding>
										<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
									</RowStyleDefault>
								</DisplayLayout>
								<Bands>
									<igtbl:UltraGridBand>
										<Columns>
											<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" BaseColumnName="ItemNum">
												<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
												<HeaderStyle Height="25px"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" BaseColumnName="ItemDrawNum">
												<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" BaseColumnName="ItemName">
												<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="계획년도" Key="PlanYear" BaseColumnName="PlanYear">
												<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="월" Key="PlanMonth" BaseColumnName="PlanMonth">
												<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="일" Key="PlanDay" BaseColumnName="PlanDay">
												<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" BaseColumnName="CompanyName">
												<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="계획수량" Key="PlanQuantity" Format="###,###,###.##" BaseColumnName="PlanQuantity">
												<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellButtonStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="판매단가" Key="SaleUnitCost" Format="\ ###,###,##0" BaseColumnName="SaleUnitCost">
												<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellButtonStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="계획 총 금액" Key="PlanTotalCost" Format="###,###,###.##" BaseColumnName="PlanTotalCost">
												<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellButtonStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" BaseColumnName="BusinessRegistrationNum">
												<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="레코드상태" Key="RecodingState" Hidden="True" BaseColumnName="RecodingState">
												<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" BaseColumnName="RegistrationPerson">
												<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID">
												<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate">
												<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" BaseColumnName="UpdatingPerson">
												<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
												<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
												<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="번호" Key="BusinessPlanInfoIndex" BaseColumnName="BusinessPlanInfoIndex">
												<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
											</igtbl:UltraGridColumn>
										</Columns>
									</igtbl:UltraGridBand>
								</Bands>
							</igtbl:ultrawebgrid></td>
					</tr>
				</table>
			</FIELDSET>
			<igtblexp:UltraWebGridExcelExporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:UltraWebGridExcelExporter></form>
	</body>
</HTML>
