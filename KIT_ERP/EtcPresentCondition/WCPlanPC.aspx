<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v1.1, Version=1.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="WCPlanPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.EtcPresentCondition.WCPlanPC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WCPlanPC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../LinkLine.css" type="text/css" rel="stylesheet">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--
		function ResettxtBox()
		{
			
			ResetTextBox();
			var objChooser1 = igdrp_getComboById("wdcFromDate");
			var objChooser2 = igdrp_getComboById("wdcToDate");
			var objChooser3 = igdrp_getComboById("wdcDeliveryFromDate");
			var objChooser4 = igdrp_getComboById("wdcDeliveryToDate");
			var objChooser5 = igdrp_getComboById("wdcBeginDate");
			var objChooser6 = igdrp_getComboById("wdcEndDate");
			objChooser1.setValue(null);
			objChooser2.setValue(null);
			objChooser3.setValue(null);
			objChooser4.setValue(null);
			objChooser5.setValue(null);
			objChooser6.setValue(null);
			document.Form1.dlProgress.options[0].selected=true;
			document.Form1.ddlItemGroup1.options[0].selected=true;
			document.Form1.ddlItemGroup2.options[0].selected=true;
			
		}
		
		//-->
		</SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table10" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" cellSpacing="0"
				cellPadding="0" width="800" align="center" border="0">
				<TR>
					<TD vAlign="top" align="center" width="20"></TD>
					<TD vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 805px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" style="WIDTH: 800px; HEIGHT: 40px" cellSpacing="0" cellPadding="0" width="800"
								align="center" border="0">
								<TR>
									<TD vAlign="middle" align="left" width="600" colSpan="6" height="30"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol>
										<igtblexp:UltraWebGridExcelExporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:UltraWebGridExcelExporter></TD>
									<td align="right" width="70">진행상태&nbsp;</td>
									<td width="130"><asp:dropdownlist id="dlProgress" runat="server" Width="80px">
											<asp:ListItem Value=" ">-선 택-</asp:ListItem>
											<asp:ListItem Value="대기">대기</asp:ListItem>
											<asp:ListItem Value="지시">지시</asp:ListItem>
											<asp:ListItem Value="진행">진행</asp:ListItem>
											<asp:ListItem Value="중단">중단</asp:ListItem>
											<asp:ListItem Value="완료">완료</asp:ListItem>
										</asp:dropdownlist></td>
								</TR>
								<tr>
									<td align="left" colSpan="8">
										<table cellSpacing="0" cellPadding="0" border="0">
											<TR>
												<TD vAlign="middle" align="right" height="30" width="65"><FONT face="굴림">작업완료일</FONT>&nbsp;
												</TD>
												<TD vAlign="middle" align="left" height="30">
													<igsch:webdatechooser id="wdcBeginDate" runat="server" Width="100px" Height="18px" Text=" " Font-Size="10pt"
														BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" NullDateLabel=" ">
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
												<TD vAlign="middle" align="center" width="10" height="30">~</TD>
												<TD vAlign="middle" align="left" height="30"><FONT face="굴림">
														<igsch:webdatechooser id="wdcEndDate" runat="server" Width="100px" Height="18px" Text=" " Font-Size="10pt"
															BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" NullDateLabel=" ">
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
												<TD vAlign="middle" align="right" width="65" height="30"><FONT face="굴림">작업시작일&nbsp; </FONT>
												</TD>
												<TD vAlign="middle" align="right" height="30"><FONT face="굴림"><igsch:webdatechooser id="wdcFromDate" runat="server" Width="100px" NullDateLabel=" " BorderColor="DimGray"
															BorderStyle="Solid" BackColor="#EEEEE9" Font-Size="10pt" Text=" " Height="18px">
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
												<TD vAlign="middle" align="center" width="10" height="30">~</TD>
												<TD vAlign="middle" align="left" height="30"><igsch:webdatechooser id="wdcToDate" runat="server" Width="100px" NullDateLabel=" " BorderColor="DimGray"
														BorderStyle="Solid" BackColor="#EEEEE9" Font-Size="10pt" Text=" " Height="18px">
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
												<td align="right" width="60">&nbsp;</td>
												<td><FONT face="굴림"></FONT></td>
											</TR>
										</table>
									</td>
								</tr>
								<TR>
									<TD style="HEIGHT: 19px" vAlign="middle" align="right" width="800" colSpan="8">
										<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="800" border="0">
											<TR>
												<TD align="right" width="65" height="20"><FONT face="굴림">품목분류1&nbsp;</FONT></TD>
												<TD align="left" width="100" height="20"><asp:dropdownlist id="ddlItemGroup1" runat="server"></asp:dropdownlist></TD>
												<TD align="right" width="65" height="20"><FONT face="굴림">품목분류2&nbsp;</FONT></TD>
												<TD align="left" width="100" height="20"><asp:dropdownlist id="ddlItemGroup2" runat="server"></asp:dropdownlist></TD>
												<TD vAlign="middle" align="right" height="30" width="70">납기요구일&nbsp;
												</TD>
												<TD vAlign="middle" align="left" height="30" width="100">
													<igsch:webdatechooser id="wdcDeliveryFromDate" runat="server" Width="100px" Height="18px" Text=" " Font-Size="10pt"
														BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" NullDateLabel=" ">
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
												<TD vAlign="middle" align="center" width="10" height="30">~</TD>
												<TD vAlign="middle" align="left" height="30" width="100">
													<igsch:webdatechooser id="wdcDeliveryToDate" runat="server" Width="100px" Height="18px" Text=" " Font-Size="10pt"
														BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" NullDateLabel=" ">
														<EditStyle>
															<BorderDetails ColorTop="DimGray" StyleBottom="Solid" ColorBottom="DimGray" ColorRight="DimGray"
																StyleTop="Solid" StyleRight="Solid" StyleLeft="Solid" ColorLeft="DimGray"></BorderDetails>
														</EditStyle>
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
												<TD align="right" height="20" width="195"><FONT face="굴림"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
															type="button" value="초기화" name="btnReset">&nbsp;
														<asp:button id="bt_Search" runat="server" Width="60px" Font-Size="9pt" Text="검   색" Height="20px"
															CommandName="Search"></asp:button>&nbsp; </FONT>
												</TD>
											</TR>
										</TABLE>
										&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" width="800" colSpan="8" height="5"></TD>
								</TR>
							</TABLE>
						</FIELDSET>
						<BR>
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[검색결과]</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
								<TR>
									<TD style="HEIGHT: 24px" align="right" colSpan="2"><INPUT id="Volum" style="WIDTH: 26px; HEIGHT: 12px" type="hidden" size="1" value="0" name="Hidden1"
											runat="server"><INPUT id="lb_Index" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1"
											runat="server"><asp:button id="btnPre" runat="server" Width="16px" Font-Size="8pt" Text="◀" Height="16px" CommandName="Video"
											CommandArgument="Pre"></asp:button><asp:button id="btnNow" runat="server" Width="16px" Font-Size="8pt" Text="■" Height="16px" CommandName="Video"
											CommandArgument="Now"></asp:button><asp:button id="btnNext" runat="server" Width="16px" Font-Size="8pt" Text="▶" Height="16px"
											CommandName="Video" CommandArgument="Next"></asp:button>&nbsp;&nbsp;
									</TD>
								</TR>
								<TR>
									<TD vAlign="top" align="center" colSpan="2"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="366px">
											<DisplayLayout AutoGenerateColumns="False" RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single"
												AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate"
												AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1" CellClickActionDefault="RowSelect"
												AllowUpdateDefault="RowTemplateOnly">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</AddNewBox>
												<Pager PageSize="15" StyleMode="ComboBox" AllowPaging="True">
													<Style VerticalAlign="Middle" BorderWidth="1px" BorderStyle="Solid" HorizontalAlign="Right"
														BackColor="LightGray">
													</Style>
												</Pager>
												<HeaderStyleDefault BorderColor="Black" BorderStyle="Solid" ForeColor="Black" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<GroupByRowStyleDefault ForeColor="Black" BackColor="#95B9F0"></GroupByRowStyleDefault>
												<RowSelectorStyleDefault BorderStyle="Solid"></RowSelectorStyleDefault>
												<FrameStyle Width="800px" Cursor="Default" BorderWidth="1px" Font-Size="10pt" Font-Names="굴림"
													BorderStyle="Solid" BackColor="Silver" Height="366px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents ColumnHeaderClickHandler="UltraWebGrid1_ColumnHeaderClickHandler" BeforeRowTemplateOpenHandler="UltraWebGrid1_BeforeRowTemplateOpenHandler"
													AfterRowTemplateCloseHandler="UltraWebGrid1_AfterRowTemplateCloseHandler"></ClientSideEvents>
												<GroupByBox>
													<Style BackColor="#B5CFF7">
													</Style>
													<BandLabelStyle ForeColor="White" BackColor="#6372D4"></BandLabelStyle>
												</GroupByBox>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedGroupByRowStyleDefault BorderColor="Yellow" BackColor="Yellow"></SelectedGroupByRowStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" Font-Bold="True" BorderColor="White" ForeColor="Linen" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" ForeColor="Black"
													BackColor="#EDEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails ColorTop="Gray" WidthLeft="0px" WidthTop="0px" ColorLeft="Gray"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
															<SelectedCellStyle HorizontalAlign="Center"></SelectedCellStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ItemDrawNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정순서" Key="ProcessSequenceNum" Width="60px" HeaderClickAction="SortMulti"
															BaseColumnName="ProcessSequenceNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정코드" Key="ProcessCode" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ProcessCode">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정명" Key="ProcessName" HeaderClickAction="SortMulti" BaseColumnName="ProcessName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="기타정보" Key="EtcText" BaseColumnName="EtcText"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="제품번호" Key="ProductItemNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ProductItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="제품도면번호" Key="ProductDrawNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ProductDrawNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="제품명" Key="ProductName" HeaderClickAction="SortMulti" BaseColumnName="ProductName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업시작일" Key="WorkDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="WorkDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="납기요구일" Key="DeliveryDate" Format="yyyy-MM-dd" BaseColumnName="DeliveryDate"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업지시일" Key="WorkDenotationDate" Format="yyyy-MM-dd" BaseColumnName="WorkDenotationDate"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="모품목번호" Key="ParentItemNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ParentItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="모품목도면번호" Key="ParentDrawNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ParentDrawNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="모품목명" Key="ParentName" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="ParentName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업장명" Key="WCName" HeaderClickAction="SortMulti" BaseColumnName="WCName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업구분" Key="WorkDistinction" HeaderClickAction="SortMulti" BaseColumnName="WorkDistinction">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업계획수량" Key="WorkPlanQuantity" Format="###,###,###" HeaderClickAction="SortMulti"
															BaseColumnName="WorkPlanQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업완료수량" Key="WorkCompletionQuantity" Format="###,###,###" HeaderClickAction="SortMulti"
															BaseColumnName="WorkCompletionQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="외주리드타임" Key="OrderLeadTime" Hidden="True" Format="#########" HeaderClickAction="SortMulti"
															BaseColumnName="OrderLeadTime">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" HeaderClickAction="SortMulti" BaseColumnName="ProgressCondition">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPerson">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" HeaderClickAction="SortMulti" BaseColumnName="UpdatingPerson">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="생산계획번호" Key="ProductionPlanHistoryIndex" Format="#########" HeaderClickAction="SortSingle"
															BaseColumnName="ProductionPlanHistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="WC작업계획번호" Key="WCDailyWorkPlanHistoryIndex" Format="#########" HeaderClickAction="SortMulti"
															BaseColumnName="WCDailyWorkPlanHistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
													</Columns>
													<RowTemplateStyle BorderColor="Silver" BorderStyle="Ridge" BackColor="Silver">
														<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
													</RowTemplateStyle>
													<RowEditTemplate>
														<TABLE id="Table1" style="FONT-WEIGHT: normal; COLOR: buttontext" cellSpacing="2" cellPadding="0"
															bgColor="whitesmoke" border="0">
															<TR>
																<TD align="right" width="110" bgColor="gainsboro" height="20">품목 번호&nbsp;</TD>
																<TD width="110" height="20"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
																		disabled readOnly type="text" size="12" name="ItemNum" columnkey="ItemNum"></TD>
																<TD align="right" width="90" bgColor="gainsboro" height="20">품목명&nbsp;</TD>
																<TD width="110" height="20"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
																		disabled readOnly type="text" size="12" name="ItemName" columnkey="ItemName"></TD>
																<TD align="right" width="110" bgColor="gainsboro" height="20">도면 번호&nbsp;</TD>
																<TD width="110" height="20"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; COLOR: #000033; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
																		disabled readOnly type="text" size="12" name="ItemDrawNum" columnkey="ItemDrawNum"></TD>
															</TR>
															<TR>
																<TD align="right" bgColor="#dcdcdc" height="20">공정순서&nbsp;</TD>
																<TD width="10" height="20"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
																		disabled readOnly type="text" size="12" columnKey="ProcessSequenceNum"></TD>
																<TD align="right" bgColor="#dcdcdc" height="20">공정명&nbsp;</TD>
																<TD height="20"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
																		disabled readOnly type="text" size="12" columnKey="ProcessName"></TD>
																<TD align="right" bgColor="#dcdcdc" height="20">작업장명&nbsp;</TD>
																<TD height="20">
																	<asp:DropDownList id=dl_WCName runat="server" Width="110px" Font-Size="8pt" columnkey="WCName" DataValueField="WCName" DataTextField="WCName" DataSource="<%# WCName %>">
																	</asp:DropDownList></TD>
															<TR>
																<TD align="right" bgColor="#dcdcdc" height="20">작업계획수량&nbsp;</TD>
																<TD width="10" height="20"><INPUT id="Text1" onkeydown="OnKeyDown_Currency(this)" onblur="OnBlur_Cur(this);" onkeyup="OnKeyUp_Currency(this)"
																		style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																		onfocus="OnFocus_Obj(this);" type="text" size="12" name="Text1" runat="server" columnKey="WorkPlanQuantity"></TD>
																<TD align="right" bgColor="#dcdcdc" height="20">작업구분&nbsp;</TD>
																<TD height="20">
																	<asp:DropDownList id="DropDownList1" runat="server" Width="110px" Font-Size="8pt" columnkey="WorkDistinction">
																		<asp:ListItem Value="자가">자가</asp:ListItem>
																		<asp:ListItem Value="외주">외주</asp:ListItem>
																	</asp:DropDownList></TD>
																<TD align="right" bgColor="#dcdcdc" height="20">외주리드타임&nbsp;</TD>
																<TD height="20"><INPUT id="Text2" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
																		type="text" size="12" name="Text2" runat="server" columnKey="OrderLeadTime"></TD>
															</TR>
															<TR>
																<TD align="right" bgColor="#dcdcdc" height="20">작업지시일&nbsp;</TD>
																<TD width="10" height="20">
																	<igtxt:WebDateTimeEdit id="WorkDenotationDate" runat="server" Width="105px" Height="20px" BorderStyle="Solid"
																		BorderColor="DimGray" BorderWidth="1px" columnKey="WorkDenotationDate" UseBrowserDefaults="False" DisplayModeFormat="d"
																		HorizontalAlign="Center">
																		<ButtonsAppearance>
																			<ButtonPressedStyle BorderStyle="Inset">
																				<BorderDetails ColorBottom="White" ColorRight="White"></BorderDetails>
																			</ButtonPressedStyle>
																			<ButtonStyle BorderWidth="2px" BorderColor="White" BorderStyle="Outset" BackColor="Control">
																				<BorderDetails StyleBottom="Ridge" ColorBottom="0, 160, 160, 160" ColorRight="0, 160, 160, 160"
																					StyleRight="Ridge"></BorderDetails>
																			</ButtonStyle>
																		</ButtonsAppearance>
																		<SpinButtons Display="OnRight"></SpinButtons>
																		<BorderDetails ColorTop="0, 160, 160, 160" StyleTop="Ridge" StyleLeft="Ridge" ColorLeft="0, 160, 160, 160"></BorderDetails>
																	</igtxt:WebDateTimeEdit></TD>
																<TD align="right" bgColor="#dcdcdc" height="20">납기요구일&nbsp;</TD>
																<TD width="10" height="20">
																	<igtxt:WebDateTimeEdit id="DeliveryDate" runat="server" Width="105px" Height="20px" BorderStyle="Solid"
																		BorderColor="DimGray" BorderWidth="1px" columnKey="DeliveryDate" UseBrowserDefaults="False" DisplayModeFormat="d"
																		HorizontalAlign="Center">
																		<ButtonsAppearance>
																			<ButtonPressedStyle BorderStyle="Inset">
																				<BorderDetails ColorBottom="White" ColorRight="White"></BorderDetails>
																			</ButtonPressedStyle>
																			<ButtonStyle BorderWidth="2px" BorderColor="White" BorderStyle="Outset" BackColor="Control">
																				<BorderDetails StyleBottom="Ridge" ColorBottom="0, 160, 160, 160" ColorRight="0, 160, 160, 160"
																					StyleRight="Ridge"></BorderDetails>
																			</ButtonStyle>
																		</ButtonsAppearance>
																		<SpinButtons Display="OnRight"></SpinButtons>
																		<BorderDetails ColorTop="0, 160, 160, 160" StyleTop="Ridge" StyleLeft="Ridge" ColorLeft="0, 160, 160, 160"></BorderDetails>
																	</igtxt:WebDateTimeEdit></TD>
																<TD align="right" bgColor="#dcdcdc" height="20">작업시작일&nbsp;</TD>
																<TD height="20">
																	<igtxt:WebDateTimeEdit id="WorkDate" runat="server" Width="105px" Height="20px" BorderStyle="Solid" BorderColor="DimGray"
																		BorderWidth="1px" columnKey="WorkDate" UseBrowserDefaults="False" DisplayModeFormat="d" HorizontalAlign="Center">
																		<ButtonsAppearance>
																			<ButtonPressedStyle BorderStyle="Inset">
																				<BorderDetails ColorBottom="White" ColorRight="White"></BorderDetails>
																			</ButtonPressedStyle>
																			<ButtonStyle BorderWidth="2px" BorderColor="White" BorderStyle="Outset" BackColor="Control">
																				<BorderDetails StyleBottom="Ridge" ColorBottom="0, 160, 160, 160" ColorRight="0, 160, 160, 160"
																					StyleRight="Ridge"></BorderDetails>
																			</ButtonStyle>
																		</ButtonsAppearance>
																		<SpinButtons Display="OnRight"></SpinButtons>
																		<BorderDetails ColorTop="0, 160, 160, 160" StyleTop="Ridge" StyleLeft="Ridge" ColorLeft="0, 160, 160, 160"></BorderDetails>
																	</igtxt:WebDateTimeEdit></TD>
															</TR>
															<TR>
																<TD align="center" colSpan="6"><INPUT id="igtbl_reOkBtn" style="WIDTH: 50px" onclick="igtbl_gRowEditButtonClick(event);__doPostBack('lnk_Update','');"
																		type="button" value="OK">&nbsp; <INPUT id="igtbl_reCancelBtn" style="WIDTH: 50px" onclick="igtbl_gRowEditButtonClick(event);"
																		type="button" value="Cancel">
																</TD>
															</TR>
														</TABLE>
													</RowEditTemplate>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 234px" vAlign="middle" align="left" height="30"><FONT face="굴림">&nbsp;
											<asp:button id="btExcel" runat="server" Width="60px" Font-Size="10pt" Text="Excel" Height="20px"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</FONT></TD>
									<TD style="WIDTH: 600px" vAlign="middle" align="right" height="30">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="chkAll" style="Z-INDEX: 102; LEFT: 832px; WIDTH: 56px; POSITION: absolute; TOP: 600px; HEIGHT: 22px; BACKGROUND-COLOR: #eeeee9"
				type="hidden" size="4" value="true" name="chkAll" runat="server">
			<asp:linkbutton id="lnk_Update" style="Z-INDEX: 103; LEFT: 840px; POSITION: absolute; TOP: 568px"
				runat="server" Visible="False">LinkButton</asp:linkbutton></form>
	</body>
</HTML>
