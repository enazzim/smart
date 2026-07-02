<%@ Page language="c#" Codebehind="RowMaterialRequirementCalculate.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.RowMaterialRequirementCalculate" codePage="949" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RowMaterialRequirementCalculate</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../GridCheck.js"></script>
		<script language="javascript" src="../PopupWindows.js"></script>
		<script language="javascript">
		<!--
			function SearchButton()
			{
				<%= Page.GetPostBackEventReference(btnSearch) %>;
				
			}
			
		//-->
		</script>
	</HEAD>
	<body bgColor="#f7f6f6" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="LEFT: 0px; WIDTH: 800px; POSITION: absolute; TOP: 0px" cellSpacing="0"
				cellPadding="0" width="800" border="0">
				<TR>
					<TD style="WIDTH: 437px" width="437">
					<TD style="LEFT: 10px; POSITION: absolute; TOP: 10px" vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" style="WIDTH: 800px" height="30" cellSpacing="0" cellPadding="0" width="810"
								border="0">
								<TR>
									<TD vAlign="middle" align="left" colSpan="8" height="30"><table id="Table4" cellSpacing="0" cellPadding="0" border="0">
											<tr>
												<td align="left" width="610"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></td>
												<td align="right" width="90">제품팀&nbsp;
												</td>
												<td align="left" width="100"><asp:dropdownlist id="ddlItemClassification1" runat="server" BackColor="#EEEEE9" Width="100px"></asp:dropdownlist></td>
											</tr>
										</table>
									</TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" width="70" height="30">생산시작일&nbsp;</TD>
									<TD vAlign="middle" align="left" width="100" height="30"><FONT face="굴림"><igsch:webdatechooser id="wdcMinDate" runat="server" BorderColor="DimGray" BorderStyle="Solid" NullDateLabel=" "
												Text=" " BackColor="#EEEEE9" Width="100px" Height="18px" Font-Size="10pt">
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
											</igsch:webdatechooser></FONT></TD>
									<TD vAlign="middle" align="center" width="5" height="30">~</TD>
									<TD vAlign="middle" align="right" width="100" height="30"><igsch:webdatechooser id="wdcMaxDate" runat="server" BorderColor="DimGray" BorderStyle="Solid" NullDateLabel=" "
											Text=" " BackColor="#EEEEE9" Width="100px" Height="18px" Font-Size="10pt">
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
									<TD vAlign="middle" align="right" width="525" colSpan="4" height="30"><asp:button id="btnClear" runat="server" BorderStyle="Outset" Text="초기화" Width="60px" Height="20px"
											Font-Size="9pt" CommandName="Clear"></asp:button>&nbsp;
										<asp:button id="btnSearch" runat="server" BorderStyle="Outset" Text="검   색" Width="60px" Height="20px"
											Font-Size="9pt" CommandName="Search"></asp:button>&nbsp;&nbsp;
									</TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" colSpan="8" height="8"></TD>
								</TR>
							</TABLE>
						</FIELDSET>
						<BR>
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[검색결과]</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD vAlign="top" align="center" colSpan="3"><igtbl:ultrawebgrid id="uwgPP_HT" runat="server" Width="100%" Height="405px" DESIGNTIMEDRAGDROP="2710">
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
													BorderColor="DimGray" BorderStyle="Solid" BackColor="Silver" Height="405px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents ColumnHeaderClickHandler="ColumnHeaderClickHandler"></ClientSideEvents>
												<GroupByBox>
													<Style BackColor="#B5CFF7">
													</Style>
													<BandLabelStyle ForeColor="White" BackColor="#6372D4"></BandLabelStyle>
												</GroupByBox>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedGroupByRowStyleDefault BorderColor="Yellow" BackColor="Yellow"></SelectedGroupByRowStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" Font-Bold="True" ForeColor="Linen" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="Gray"
													BorderStyle="Solid" ForeColor="Black" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails ColorTop="Gray" WidthLeft="0px" WidthTop="0px" ColorLeft="Gray"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand AllowUpdate="No">
													<Columns>
														<igtbl:UltraGridColumn HeaderText="선택" Key="chk" Width="40px" Type="CheckBox" HeaderClickAction="Select"
															BaseColumnName="" AllowUpdate="Yes">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" BaseColumnName="ItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left">
																<Padding Left="2px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" BaseColumnName="ItemDrawNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left">
																<Padding Left="2px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" BaseColumnName="ItemName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left">
																<Padding Left="2px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="생산계획원천코드" Key="ProductionPlanHistorySourceCode" Hidden="True" BaseColumnName="ProductionPlanHistorySourceCode">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="계획원천" Key="ProductionPlanHistorySource" Width="70px" BaseColumnName="ProductionPlanHistorySource">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="생산계획량" Key="ProductionPlanQuantity" Width="70px" Format="###,###,###"
															BaseColumnName="ProductionPlanQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="생산시작일" Key="ProductionBeginDate" Format="yyyy-MM-dd" BaseColumnName="ProductionBeginDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="자재소요량산출여부" Key="RowMaterialCalculation" Hidden="True" BaseColumnName="RowMaterialCalculation">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="불륨번호" Key="VolumNum" Hidden="True" BaseColumnName="VolumNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" Width="70px" BaseColumnName="ProgressCondition">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" BaseColumnName="RegistrationPerson">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" BaseColumnName="UpdatingPerson">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
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
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
													</Columns>
													<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
														<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
													</RowTemplateStyle>
													<RowEditTemplate>
														<P align="center">&nbsp;</P>
													</RowEditTemplate>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" colSpan="3" height="40"><INPUT id="hdWorkPlan1" style="WIDTH: 64px; HEIGHT: 22px" type="hidden" size="5" name="WorkPlan1"
											runat="server"><INPUT id="hdWorkPlan2" style="WIDTH: 64px; HEIGHT: 22px" type="hidden" size="5" name="WorkPlan2"
											runat="server"><INPUT id="hdWorkPlan3" style="WIDTH: 64px; HEIGHT: 22px" type="hidden" size="5" name="WorkPlan3"
											runat="server"><asp:linkbutton id="LinkButton1" runat="server" Visible="False">LinkButton</asp:linkbutton><asp:button id="btnProduce" runat="server" BorderStyle="Outset" Text="산   출" Width="60px" Height="20px"
											Font-Size="9pt" CommandName="Produce" Enabled="False"></asp:button>&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidPresentRowMaterialUsed" style="Z-INDEX: 102; LEFT: 24px; WIDTH: 64px; POSITION: absolute; TOP: 600px; HEIGHT: 22px"
				type="hidden" size="5" name="Hidden1" runat="server"><INPUT id="hidSafeyRowMaterialUsed" style="Z-INDEX: 103; LEFT: 96px; WIDTH: 64px; POSITION: absolute; TOP: 600px; HEIGHT: 22px"
				type="hidden" size="5" name="Hidden1" runat="server"><INPUT id="hidOrderNonInStorehouseUsed" style="Z-INDEX: 104; LEFT: 168px; WIDTH: 64px; POSITION: absolute; TOP: 600px; HEIGHT: 22px"
				type="hidden" size="5" name="Hidden1" runat="server"><INPUT id="hidOrderGapInStorehouseUsed" style="Z-INDEX: 105; LEFT: 240px; WIDTH: 64px; POSITION: absolute; TOP: 600px; HEIGHT: 22px"
				type="hidden" size="5" name="Hidden1" runat="server"><INPUT id="hidMinimumGapUsed" style="Z-INDEX: 106; LEFT: 312px; WIDTH: 64px; POSITION: absolute; TOP: 600px; HEIGHT: 22px"
				type="hidden" size="5" name="Hidden1" runat="server"><INPUT id="hidOrderRequestStandbyUsed" style="Z-INDEX: 107; LEFT: 384px; WIDTH: 64px; POSITION: absolute; TOP: 600px; HEIGHT: 22px"
				type="hidden" size="5" name="Hidden1" runat="server"><INPUT id="hidOutOrderUsed" style="Z-INDEX: 107; LEFT: 384px; WIDTH: 64px; POSITION: absolute; TOP: 600px; HEIGHT: 22px"
				type="hidden" size="5" name="Hidden1" runat="server">
		</form>
	</body>
</HTML>
