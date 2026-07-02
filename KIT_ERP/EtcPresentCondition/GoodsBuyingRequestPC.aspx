<%@ Page language="c#" Codebehind="GoodsBuyingRequestPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.EtcPresentCondition.Table1" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>GoodsBuyingRequestPC</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--
		function ResettxtBox()
		{
			ResetTextBox();
			ResetBox();
			var objChooser1 = igdrp_getComboById("wdcStartDate");
			var objChooser2 = igdrp_getComboById("wdcEndDate");
			objChooser1.setValue(null);
			objChooser2.setValue(null);
			document.From1.dlProgress.options[0].selected=true;
			document.From1.dlSource.options[0].selected=true;
		}
  		
--></SCRIPT>
	</HEAD>
	<body bgColor="#f7f6f6" MS_POSITIONING="GridLayout">
		<form id="From1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" height="460"
				cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD></TD>
					<TD style="PADDING-TOP: 10px" vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 800px; BORDER-BOTTOM: #696969 2px solid; HEIGHT: 20px"><LEGEND align="top">&nbsp;[검색조건]</LEGEND>
							<TABLE id="Table2" style="HEIGHT: 20px" cellSpacing="0" cellPadding="0" width="800">
								<TR>
									<TD width="600" colSpan="6" height="30">
										<uc1:ItemSearchControl id="ItemSearchControl1" runat="server"></uc1:ItemSearchControl></TD>
									<TD align="right" width="70" height="30"><FONT face="굴림">진행상태&nbsp; </FONT>
									</TD>
									<TD width="130" height="30">
										<asp:dropdownlist id="dlProgress" runat="server" Width="100px" BackColor="#EEEEE9" Height="20px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30"><FONT face="굴림">의뢰원천&nbsp; </FONT>
									</TD>
									<TD width="130" height="30">
										<asp:dropdownlist id="dlSource" runat="server" Width="100px" BackColor="#EEEEE9" Height="20px"></asp:dropdownlist></TD>
									<TD align="right" width="70" height="30"><FONT face="굴림">납기요구일&nbsp; </FONT>
									</TD>
									<TD width="100" height="30">
										<igsch:webdatechooser id="wdcStartDate" runat="server" Width="90px" BackColor="#EEEEE9" Height="20px"
											Text="Null" NullDateLabel=" ">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderStyle="Inset"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD width="15" height="30"><FONT face="굴림">~</FONT></TD>
									<TD width="215" height="30">
										<igsch:webdatechooser id="wdcEndDate" runat="server" Width="90px" BackColor="#EEEEE9" Height="20px" Text="Null"
											NullDateLabel=" ">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderStyle="Inset"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD align="right" width="200" colSpan="2" height="30"><FONT face="굴림"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
												type="button" value="초기화" name="btnReset">&nbsp;
											<asp:button id="bt_Search" runat="server" Width="60px" Height="20px" Text="검색" Font-Size="10pt"></asp:button>&nbsp;</FONT></TD>
								</TR>
							</TABLE>
							<FONT face="굴림"></FONT>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="0"></TD>
					<TD style="PADDING-TOP: 10px" vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 800px; BORDER-BOTTOM: #696969 2px solid"><LEGEND style="WIDTH: 19px; HEIGHT: 15px" align="top">[검색결과]</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800">
								<tr>
									<td align="right" colSpan="2" height="10"><INPUT id="Volum" style="WIDTH: 34px; HEIGHT: 16px" type="hidden" size="1" value="0" name="Hidden1"
											runat="server"><asp:label id="lb_Vol" runat="server" Font-Size="8pt"></asp:label><asp:button id="btnPre" runat="server" Height="16px" Width="16px" Text="◀" Font-Size="8pt" CommandName="Pre"></asp:button><asp:button id="btnNow" runat="server" Height="16px" Width="16px" Text="■" Font-Size="8pt" CommandName="Now"></asp:button><asp:button id="btnNext" runat="server" Height="16px" Width="16px" Text="▶" Font-Size="8pt"
											CommandName="Next"></asp:button><FONT face="굴림">&nbsp;</FONT></td>
								</tr>
								<TR>
									<TD colSpan="2"><FONT face="굴림"><igtbl:ultrawebgrid id="uwgRO" runat="server" Height="386px" Width="800px">
												<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
													RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
													HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
													RowSelectorsDefault="No" Name="uwgRO" TableLayout="Fixed" CellClickActionDefault="RowSelect">
													<AddNewBox>
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														</Style>
													</AddNewBox>
													<Pager PageSize="16" StyleMode="ComboBox" AllowPaging="True">
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														</Style>
													</Pager>
													<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</HeaderStyleDefault>
													<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
													<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
														BackColor="Silver" Height="386px"></FrameStyle>
													<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</FooterStyleDefault>
													<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
													<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
													<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
													<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
														<Padding Left="3px"></Padding>
														<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
													</RowStyleDefault>
												</DisplayLayout>
												<Bands>
													<igtbl:UltraGridBand>
														<Columns>
															<igtbl:UltraGridColumn HeaderText="품목상태" Key="ItemState" HeaderClickAction="Select" BaseColumnName="ItemState"
																AllowResize="Free" AllowUpdate="Yes">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle Height="25px"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" HeaderClickAction="SortMulti"
																BaseColumnName="ItemDrawNum">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="구매의뢰원천코드" Key="BuyingRequestSourceCode" Hidden="True" HeaderClickAction="SortMulti"
																BaseColumnName="BuyingRequestSourceCode">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="총 발주량" Key="OrderQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
																BaseColumnName="OrderQuantity">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="납기요구일" Key="FirstDeliveryDemandDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
																BaseColumnName="FirstDeliveryDemandDate">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
																BaseColumnName="ApplyUnitCost">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="총금액" Key="TotalCost" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
																BaseColumnName="TotalCost">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="구매의뢰원천" Key="BuyingRequestSource" HeaderClickAction="SortMulti" BaseColumnName="BuyingRequestSource">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" HeaderClickAction="SortMulti" BaseColumnName="ProgressCondition">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="자산분류" Key="PropertyClassification" HeaderClickAction="SortMulti" BaseColumnName="PropertyClassification">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPerson">
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
															<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
																BaseColumnName="UpdatingDate">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="이전원장번호" Key="HistoryIndex" Hidden="True" HeaderClickAction="SortMulti"
																BaseColumnName="HistoryIndex">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="이전원장구분" Key="HistorySection" Hidden="True" HeaderClickAction="SortMulti"
																BaseColumnName="HistorySection">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="번호" Key="BuyingRequestHistoryIndex" HeaderClickAction="SortMulti" BaseColumnName="BuyingRequestHistoryIndex">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
														</Columns>
													</igtbl:UltraGridBand>
												</Bands>
											</igtbl:ultrawebgrid></FONT></TD>
								</TR>
								<TR>
									<TD height="25"><FONT face="굴림">&nbsp;</FONT><asp:button id="bt_Excel" runat="server" Height="20px" Width="60px" Text="Excel"></asp:button></TD>
									<TD align="right" height="25">&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			<igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter></form>
	</body>
</HTML>
