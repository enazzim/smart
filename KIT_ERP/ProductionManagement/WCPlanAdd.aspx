<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v1.1, Version=1.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="WCPlanAdd.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.WCPlanAdd" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WCPlanAdd</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<FONT face="굴림">
				<TABLE id="Table1" style="Z-INDEX: 105; LEFT: 8px; WIDTH: 800px; POSITION: absolute; TOP: 8px"
					cellSpacing="0" cellPadding="0" width="800" border="0">
					<TR>
						<TD>
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[입&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;력]</LEGEND>
								<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="800" border="0">
									<TR>
										<TD colSpan="8" height="30"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></TD>
									</TR>
									<TR>
										<TD align="right" height="30">생산계획 수량&nbsp;
										</TD>
										<TD height="30"><igtxt:webnumericedit id="wneProductionPlanQuantity" runat="server" Font-Size="10pt" BorderWidth="1px"
												BorderStyle="Solid" BorderColor="DimGray" BackColor="#EEEEE9" Width="80px" Height="20px"></igtxt:webnumericedit></TD>
										<TD align="right" height="30">생산시작일&nbsp;</TD>
										<TD height="30"><igsch:webdatechooser id="wdcStartDate" runat="server" Font-Size="10pt" BorderStyle="Solid" BorderColor="DimGray"
												BackColor="#EEEEE9" Width="100px" Height="18px" NullDateLabel=" " Text="날짜 선택">
												<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
													ShowTitle="False" ShowFooter="False">
													<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
													<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
													<DropDownStyle BackColor="White"></DropDownStyle>
													<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
												</CalendarLayout>
												<DropDownStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></DropDownStyle>
												<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
												<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
											</igsch:webdatechooser></TD>
										<TD align="right" height="30">생산계획 원천&nbsp;
										</TD>
										<TD height="30"><asp:dropdownlist id="ddlProductionPlanHistorySource" runat="server" Font-Size="10pt" BackColor="#EEEEE9"
												Width="75px" Height="20px"></asp:dropdownlist></TD>
										<TD align="right" colSpan="2" height="30"><asp:button id="btnClear" runat="server" Font-Size="10pt" Width="60px" Height="20px" Text="초기화"></asp:button>&nbsp;
											<asp:button id="btnUpDate" runat="server" Font-Size="10pt" Width="60px" Height="20px" Text="수   정"
												Enabled="False"></asp:button>&nbsp;
											<asp:button id="btnDelete" runat="server" Font-Size="10pt" Width="60px" Height="20px" Text="삭   제"
												Enabled="False"></asp:button>&nbsp;
											<asp:button id="btnAdd" runat="server" Font-Size="10pt" Width="60px" Height="20px" Text="추가"></asp:button>&nbsp;&nbsp;
										</TD>
									</TR>
									<TR>
										<TD align="right" colSpan="8" height="8"></TD>
									</TR>
								</TABLE>
							</FIELDSET>
						</TD>
					</TR>
					<TR>
						<TD height="5"></TD>
					</TR>
					<TR>
						<TD>
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[입력결과]</LEGEND>
								<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" border="0">
									<TR>
										<TD vAlign="top" align="center" colSpan="3"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="100%" Height="430px" DESIGNTIMEDRAGDROP="256">
												<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
													RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
													HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
													RowSelectorsDefault="No" Name="uwgPPxHT" TableLayout="Fixed" CellClickActionDefault="RowSelect"
													NoDataMessage="데이터가 없습니다.">
													<AddNewBox>
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														</Style>
													</AddNewBox>
													<Pager>
														<Style VerticalAlign="Middle" BorderWidth="1px" BorderStyle="Solid" HorizontalAlign="Center"
															BackColor="LightGray">
														</Style>
													</Pager>
													<HeaderStyleDefault Cursor="Hand" BorderColor="Black" BorderStyle="Solid" ForeColor="Black" BackColor="LightGray"
														Height="25px">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</HeaderStyleDefault>
													<GroupByRowStyleDefault ForeColor="Black" BackColor="#95B9F0"></GroupByRowStyleDefault>
													<RowSelectorStyleDefault BorderStyle="Solid"></RowSelectorStyleDefault>
													<FrameStyle Width="100%" Cursor="Default" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림"
														BorderColor="DimGray" BorderStyle="Solid" BackColor="Silver" Height="430px"></FrameStyle>
													<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</FooterStyleDefault>
													<ClientSideEvents DblClickHandler="uwgPP_HT_DblClickHandler"></ClientSideEvents>
													<GroupByBox>
														<Style BackColor="#B5CFF7">
														</Style>
														<BandLabelStyle ForeColor="White" BackColor="#6372D4"></BandLabelStyle>
													</GroupByBox>
													<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
													<SelectedGroupByRowStyleDefault BorderColor="Yellow" BackColor="Yellow"></SelectedGroupByRowStyleDefault>
													<SelectedRowStyleDefault Cursor="Hand" Font-Bold="True" ForeColor="Linen" BackColor="Navy"></SelectedRowStyleDefault>
													<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
													<RowStyleDefault Cursor="Hand" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DimGray"
														BorderStyle="Solid" ForeColor="Black" BackColor="#EBEFF6">
														<Padding Left="3px"></Padding>
														<BorderDetails ColorTop="Gray" WidthLeft="0px" WidthTop="0px" ColorLeft="Gray"></BorderDetails>
													</RowStyleDefault>
												</DisplayLayout>
												<Bands>
													<igtbl:UltraGridBand AllowUpdate="No">
														<Columns>
															<igtbl:UltraGridColumn HeaderText="품목번호" Hidden="True" Key="ItemNum" BaseColumnName="ItemNum">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="도면번호" Hidden="True" Key="ItemDrawNum" BaseColumnName="ItemDrawNum">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" BaseColumnName="ItemName">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="생산계획원천코드" Key="ProductionPlanHistorySourceCode" Hidden="True" BaseColumnName="ProductionPlanHistorySourceCode">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="생산계획원천" Key="ProductionPlanHistorySource" BaseColumnName="ProductionPlanHistorySource">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="생산계획량" Key="ProductionPlanQuantity" Format="###,###,##0.00" BaseColumnName="ProductionPlanQuantity">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="생산시작일" Key="ProductionBeginDate" Format="yyyy-MM-dd" BaseColumnName="ProductionBeginDate">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="자재소요량산출여부" Key="RowMaterialCalculation" BaseColumnName="RowMaterialCalculation">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="불륨번호" Key="VolumNum" Hidden="True" BaseColumnName="VolumNum">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" BaseColumnName="ProgressCondition">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" BaseColumnName="RegistrationPerson">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" BaseColumnName="UpdatingPerson">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="생산계획원장번호" Key="ProductionPlanHistoryIndex" Hidden="True" BaseColumnName="ProductionPlanHistoryIndex">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="이전번호" Key="HistoryIndex" Hidden="True" BaseColumnName="HistoryIndex">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="이전구분" Key="HistorySection" Hidden="True" BaseColumnName="HistorySection">
																<SelectedCellStyle VerticalAlign="Middle" HorizontalAlign="Center"></SelectedCellStyle>
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
														</Columns>
														<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
															<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
														</RowTemplateStyle>
														<RowEditTemplate></RowEditTemplate>
													</igtbl:UltraGridBand>
												</Bands>
											</igtbl:ultrawebgrid></TD>
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
