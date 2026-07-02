<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="BusinessPlanInfo.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ManagementInfomation.BusinessPlanInfo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>   
	<HEAD>
		<title>BusinessPlanInfo</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#f6f7f7">
		<form id="Form1" method="post" runat="server">
			<FONT face="굴림">
				<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 0px" height="550"
					cellSpacing="0" cellPadding="0" width="800" border="0">
					<TR>
						<TD vAlign="top" align="center" width="800">
							<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 805px; BORDER-BOTTOM: #696969 2px solid; HEIGHT: 50px">
								<P><LEGEND style="FONT-SIZE: 9pt" align="top">[입력]
									</LEGEND>
									<br>
									<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800">
										<TR>
											<TD align="right" width="10"></TD>
											<TD style="WIDTH: 232px" align="right"><INPUT id="Find" style="FONT-SIZE: 9pt; WIDTH: 500px; HEIGHT: 20px; BACKGROUND-COLOR: #eeeee9"
													type="file" size="64" name="Find" runat="server"></TD>
											<TD align="right"><FONT face="굴림">
													<asp:hyperlink id="HyperLink1" runat="server" Font-Size="9pt" NavigateUrl="../ExcelDown/Business.xls">Excel양식 다운로드</asp:hyperlink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												</FONT>
												<asp:button id="bt_Register" runat="server" Height="20px" Width="60px" Font-Size="10pt" Text="등  록"></asp:button>&nbsp;&nbsp;</TD>
										</TR>
										<TR>
											<TD align="right" width="10" height="10"></TD>
											<TD style="WIDTH: 232px" align="right" height="10"></TD>
											<TD align="right" height="10"></TD>
										</TR>
									</TABLE>
								</P>
							</FIELDSET>
						</TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" width="800"><br>
							<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND style="FONT-SIZE: 9pt" align="top">[입력결과]</LEGEND>
								<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="450px">
									<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
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
											BackColor="Silver" Height="450px"></FrameStyle>
										<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
											<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
										</FooterStyleDefault>
										<ClientSideEvents AfterSelectChangeHandler="UltraWebGrid1_AfterSelectChangeHandler"></ClientSideEvents>
										<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
										<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
										<RowAlternateStyleDefault BackColor="LightSteelBlue"></RowAlternateStyleDefault>
										<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
											<Padding Left="3px"></Padding>
											<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
										</RowStyleDefault>
									</DisplayLayout>
									<Bands>
										<igtbl:UltraGridBand>
											<Columns>
												<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
													<HeaderStyle Height="25px"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" HeaderClickAction="SortMulti" BaseColumnName="ItemDrawNum">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="계획년도" Key="PlanYear" HeaderClickAction="SortMulti" BaseColumnName="PlanYear">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="월" Key="PlanMonth" HeaderClickAction="SortMulti" BaseColumnName="PlanMonth">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="일" Key="PlanDay" HeaderClickAction="SortMulti" BaseColumnName="PlanDay">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" HeaderClickAction="SortMulti" BaseColumnName="CompanyName">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Top" HorizontalAlign="Left"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="계획수량" Key="PlanQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
													BaseColumnName="PlanQuantity">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="판매단가" Key="SaleUnitCost" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
													BaseColumnName="SaleUnitCost">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="계획 총 금액" Key="PlanTotalCost" Format="###,###,###.##" HeaderClickAction="SortMulti"
													BaseColumnName="PlanTotalCost">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" BaseColumnName="BusinessRegistrationNum">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="레코드상태" Key="RecodingState" Hidden="True" BaseColumnName="RecodingState">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPerson">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
													BaseColumnName="RegistrationDate">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" HeaderClickAction="SortMulti" BaseColumnName="UpdatingPerson">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
													BaseColumnName="UpdatingDate">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="번호" Key="BusinessPlanInfoIndex" Hidden="True" HeaderClickAction="SortMulti"
													BaseColumnName="BusinessPlanInfoIndex">
													<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
											</Columns>
										</igtbl:UltraGridBand>
									</Bands>
								</igtbl:ultrawebgrid></FIELDSET>
						</TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
