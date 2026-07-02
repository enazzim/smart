<%@ Page language="c#" Codebehind="ItemGroupPlanRegistration.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.ItemGroupPlanRegistration" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ItemGroupPlanRegistration</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--

		function SearchButton()
			{
				<%= Page.GetPostBackEventReference(btnSearch) %>;
				
			}
			
		function ResettxtBox()
		{	
			ResetTextBox();
			var objChooser1 = igdrp_getComboById("wdcMaxDate");
			var objChooser2 = igdrp_getComboById("wdcMinDate");
			objChooser1.setValue(null);
			objChooser2.setValue(null);	
			document.Form1.ddlItemClassification1.options[0].selected=true;
					
		}
		
--></SCRIPT>
	</HEAD>
	<body bottomMargin="0" bgColor="#f7f6f6" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="LEFT: 10px; WIDTH: 800px; POSITION: absolute; TOP: 10px" cellSpacing="0"
				cellPadding="0" width="800" border="0">
				<TBODY>
					<TR>
						<TD style="WIDTH: 11px" width="11"></TD>
						<TD vAlign="top" align="center" width="800">
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[검색조건]</LEGEND>
								<TABLE id="Table2" style="WIDTH: 800px" height="40" cellSpacing="0" cellPadding="0" width="800"
									border="0" DESIGNTIMEDRAGDROP="53">
									<TR>
										<TD vAlign="middle" align="left" width="800" colSpan="8" height="30">
											<table id="table3" cellSpacing="0" cellPadding="0" width="800" border="0">
												<tr>
													<td width="600"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></td>
													<td align="right" width="70">제품팀&nbsp;</td>
													<td align="left" width="130"><asp:dropdownlist id="ddlItemClassification1" runat="server" BackColor="#EEEEE9" Width="100px"></asp:dropdownlist></td>
												</tr>
											</table>
										</TD>
									</TR>
									<TR>
										<TD vAlign="middle" align="right" width="85" height="30">산출기준일&nbsp;</TD>
										<TD vAlign="middle" align="left" width="100" height="30"><igsch:webdatechooser id="wdcMinDate" runat="server" BackColor="#EEEEE9" Width="100px" BorderStyle="Solid"
												BorderColor="DimGray" Text=" " NullDateLabel=" " Height="18px" Font-Size="10pt">
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
										<TD vAlign="middle" align="center" width="5" height="30">~</TD>
										<TD vAlign="middle" align="left" width="100" height="30"><igsch:webdatechooser id="wdcMaxDate" runat="server" BackColor="#EEEEE9" Width="100px" BorderStyle="Solid"
												BorderColor="DimGray" Text=" " NullDateLabel=" " Height="18px" Font-Size="10pt">
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
										<TD vAlign="middle" align="right" width="520" colSpan="4" height="30">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
												type="button" value="초기화" name="btnReset">
											<asp:button id="btnSearch" runat="server" Width="60px" Text="검   색" Height="20px" Font-Size="10pt"
												CommandName="Search"></asp:button>&nbsp;&nbsp;
										</TD>
									</TR>
									<TR>
										<TD vAlign="middle" align="right" width="70" colSpan="8" height="8"></TD>
									</TR>
								</TABLE>
							</FIELDSET>
							<BR>
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"
								DESIGNTIMEDRAGDROP="245"><LEGEND align="top">[검색결과]</LEGEND>
								<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" border="0">
									<TBODY>
										<TR>
											<TD vAlign="top" align="center" colSpan="3"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="100%" Height="350px">
													<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
														RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
														HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
														RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect"
														NoDataMessage="해당 데이타가 없습니다." AllowUpdateDefault="Yes">
														<AddNewBox>
															<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
															</Style>
														</AddNewBox>
														<Pager PageSize="15" StyleMode="ComboBox" AllowPaging="True">
															<Style VerticalAlign="Middle" BorderWidth="1px" BorderStyle="Solid" HorizontalAlign="Center"
																BackColor="LightGray">
															</Style>
														</Pager>
														<HeaderStyleDefault Cursor="Hand" BorderColor="Black" BorderStyle="Solid" HorizontalAlign="Center" ForeColor="Black"
															BackColor="LightGray" Height="25px">
															<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
														</HeaderStyleDefault>
														<GroupByRowStyleDefault ForeColor="Black" BackColor="#95B9F0"></GroupByRowStyleDefault>
														<RowSelectorStyleDefault BorderStyle="Solid"></RowSelectorStyleDefault>
														<FrameStyle Width="100%" Cursor="Default" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림"
															BorderColor="DimGray" BorderStyle="Solid" BackColor="Silver" Height="350px"></FrameStyle>
														<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
															<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
														</FooterStyleDefault>
														<GroupByBox>
															<Style BackColor="#B5CFF7">
															</Style>
															<BandLabelStyle ForeColor="White" BackColor="#6372D4"></BandLabelStyle>
														</GroupByBox>
														<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
														<SelectedGroupByRowStyleDefault BorderColor="Yellow" BackColor="Yellow"></SelectedGroupByRowStyleDefault>
														<SelectedRowStyleDefault Cursor="Hand" Font-Bold="True" ForeColor="Linen" BackColor="Navy"></SelectedRowStyleDefault>
														<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
														<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid" ForeColor="Black"
															BackColor="#EBEFF6">
															<Padding Left="3px"></Padding>
															<BorderDetails ColorTop="Gray" WidthLeft="0px" WidthTop="0px" ColorLeft="Gray"></BorderDetails>
														</RowStyleDefault>
													</DisplayLayout>
													<Bands>
														<igtbl:UltraGridBand AllowUpdate="RowTemplateOnly">
															<Columns>
																<igtbl:UltraGridColumn HeaderText="품목상태" Key="ItemState" Width="126px" BaseColumnName="ItemState">
																	<HeaderStyle Height="25px"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" EditorControlID="" Width="185px" Format="" BaseColumnName="ItemNum"
																	NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" EditorControlID="" Width="185px" Format="" BaseColumnName="ItemName"
																	NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="총 생산량" Key="Quantity" Width="150px" Format="###,###,##0.00" BaseColumnName="Quantity">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																		<Padding Right="4px"></Padding>
																	</CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="고객생산시작일" Key="DeliveryDate" EditorControlID="" Width="150px" Format="yyyy-MM-dd"
																	BaseColumnName="DeliveryDate" NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" EditorControlID="" Hidden="True" Format=""
																	BaseColumnName="ProgressCondition" NullText="" FooterText="">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="번호" Key="ItemGroupIndex" Hidden="True" BaseColumnName="ItemGroupIndex">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
															</Columns>
															<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
																<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
															</RowTemplateStyle>
														</igtbl:UltraGridBand>
													</Bands>
												</igtbl:ultrawebgrid></TD>
										</TR>
										<TR>
											<TD vAlign="middle" align="right" colSpan="3" height="35"><INPUT id="chkAll" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" value="true"
													name="Hidden1" runat="server"><asp:button id="btnOK" runat="server" Width="65" Text="수    립" Height="20" Font-Size="10pt"
													CommandName="Found"></asp:button>&nbsp;</TD>
										</TR>
									</TBODY>
								</TABLE>
							</FIELDSET>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
		</TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
	</body>
</HTML>
