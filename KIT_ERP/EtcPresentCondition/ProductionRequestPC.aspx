<%@ Page language="c#" Codebehind="ProductionRequestPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.EtcPresentCondition.ProductionRequestPC" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ProductionRequest</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
			document.ProductionRequestPC.dl_ProgressState.options[0].selected=true;
			document.ProductionRequestPC.dl_Source.options[0].selected=true;
			document.ProductionRequestPC.bt_Cancel.disable = true;
			
		}
		--></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; WIDTH: 800px; POSITION: absolute; TOP: 8px"
				height="550" cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD width="20" height="550"></TD>
					<TD style="PADDING-TOP: 10px" vAlign="top" align="center" width="800" height="550"><FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 805px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 60px"><LEGEND style="FONT-SIZE: 9pt" align="top">[검색조건]</LEGEND>
							<TABLE id="Table3" height="68" cellSpacing="0" cellPadding="0" width="800">
								<TR>
									<TD width="600" colSpan="9" height="30">
										<TABLE id="table4" cellSpacing="0" cellPadding="0" width="800" border="0">
											<TBODY>
												<TR>
													<TD width="200">
														<uc1:CompanySearchControl id="CSC1" runat="server"></uc1:CompanySearchControl></FONT></TD>
									</TD>
									<TD width="600">
										<uc1:ItemSearchControl id="ItemSearchControl1" runat="server"></uc1:ItemSearchControl></TD>
								</TR>
							</TABLE>
					</TD>
				</TR>
				<TR>
					<TD align="right" width="70" height="30"><FONT face="굴림">진행상태&nbsp; </FONT>
					</TD>
					<TD width="130" height="30">
						<asp:dropdownlist id="dl_ProgressState" runat="server" Font-Size="9pt" Width="100px" BackColor="#EEEEE9"></asp:dropdownlist></TD>
					<TD align="right" width="70" height="30"><FONT face="굴림">의뢰원천&nbsp;</FONT></TD>
					<TD width="130" height="30">
						<asp:dropdownlist id="dl_Source" runat="server" Font-Size="9pt" Width="122px" BackColor="#EEEEE9"></asp:dropdownlist></TD>
					<TD align="right" width="70" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림">납기일&nbsp;</FONT></TD>
					<TD width="130" height="30">
						<igsch:webdatechooser id="wdcStartDate" runat="server" Width="122px" BackColor="#EEEEE9" Text=" " MaxDate="2076-06-06"
							NullDateLabel=" " BorderColor="DimGray" Height="20px" BorderStyle="Solid">
							<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
								MaxDate="2076-06-06" ShowTitle="False" ShowFooter="False">
								<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
								<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
								<DropDownStyle BackColor="White"></DropDownStyle>
								<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
							</CalendarLayout>
							<DropDownStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></DropDownStyle>
							<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
							<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
						</igsch:webdatechooser></TD>
					<TD width="5" height="30"><FONT face="굴림">~</FONT></TD>
					<TD vAlign="middle" align="left" width="195" colSpan="2" height="30"><FONT face="굴림"></FONT>
						<igsch:webdatechooser id="wdcEndDate" runat="server" Width="122px" BackColor="#EEEEE9" Text=" " MaxDate="2076-06-06"
							NullDateLabel=" " BorderColor="DimGray" Height="20px" BorderStyle="Solid">
							<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
								MaxDate="2076-06-06" ShowTitle="False" ShowFooter="False">
								<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
								<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
								<DropDownStyle BackColor="White"></DropDownStyle>
								<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
							</CalendarLayout>
							<DropDownStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></DropDownStyle>
							<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
							<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
						</igsch:webdatechooser><FONT face="굴림"></FONT></TD>
				</TR>
				<TR>
					<TD align="right" colSpan="9" height="20">&nbsp;&nbsp;<INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
							type="button" value="초기화" name="btnReset">
						<asp:button id="bt_Search" runat="server" Width="60px" Text="검색" Height="20px"></asp:button>&nbsp;</TD>
				</TR>
				<TR>
					<TD align="right" colSpan="9" height="8"></TD>
				</TR>
			</TABLE>
			</FIELDSET>
			<BR>
			<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; FONT-SIZE: 0pt; BORDER-LEFT: #696969 2px solid; WIDTH: 807px; BORDER-BOTTOM: #696969 2px solid; HEIGHT: 400px"
				DESIGNTIMEDRAGDROP="293"><LEGEND style="FONT-SIZE: 9pt" align="top">[검색결과]</LEGEND>
				<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800">
					<TR>
						<TD vAlign="top" align="right" width="100"><INPUT id="Volum" style="WIDTH: 26px; HEIGHT: 12px" type="hidden" size="1" value="0" name="Hidden1"
								runat="server">
						</TD>
						<TD align="right">
							<asp:button id="btnPre" runat="server" Font-Size="8pt" Width="16px" Text="◀" Height="16px" CommandName="Pre"></asp:button><asp:button id="btnNow" runat="server" Font-Size="8pt" Width="16px" Text="■" Height="16px" CommandName="Now"></asp:button><asp:button id="btnNext" runat="server" Font-Size="8pt" Width="16px" Text="▶" Height="16px"
								CommandName="Next"></asp:button></TD>
					</TR>
					<TR>
						<TD colSpan="2">
							<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="366px">
								<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
									RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
									HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
									RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect">
									<AddNewBox>
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										</Style>
									</AddNewBox>
									<Pager PageSize="15" StyleMode="ComboBox" AllowPaging="True">
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										</Style>
									</Pager>
									<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</HeaderStyleDefault>
									<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
										BackColor="Silver" Height="366px"></FrameStyle>
									<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</FooterStyleDefault>
									<ClientSideEvents BeforeRowTemplateOpenHandler="UltraWebGrid1_BeforeRowTemplateOpenHandler"></ClientSideEvents>
									<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
									<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
									<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
									<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
										<Padding Left="3px"></Padding>
										<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
									</RowStyleDefault>
								</DisplayLayout>
								<Bands>
									<igtbl:UltraGridBand AllowUpdate="RowTemplateOnly" CellClickAction="RowSelect">
										<Columns>
											<igtbl:UltraGridColumn HeaderText="품목상태" Key="ItemState" BaseColumnName="ItemState">
												<HeaderStyle Height="25px"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="ItemDrawNum">
												<CellStyle VerticalAlign="Top" HorizontalAlign="Left"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="자산분류" Key="PropertyClassification" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="PropertyClassification">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="생산의뢰원천코드" Key="ProductionRequestSourceCode" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="ProductionRequestSourceCode">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="생산의뢰원천" Key="ProductionRequestSource" HeaderClickAction="SortMulti"
												BaseColumnName="ProductionRequestSource">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" HeaderClickAction="SortMulti" BaseColumnName="CompanyName">
												<CellStyle VerticalAlign="Top" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="사업자번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="BusinessRegistrationNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="총수주량" Key="ProductionRequestQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
												BaseColumnName="ProductionRequestQuantity">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="1차납기요구량" Key="RequestQuantity1" Hidden="True" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="RequestQuantity1">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="납기요구일" Key="RequestDate1" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="RequestDate1">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="2차납기요구량" Key="RequestQuantity2" Hidden="True" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="RequestQuantity2">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="2차납기요구일" Key="RequestDate2" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="RequestDate2">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="3차납기요구량" Key="RequestQuantity3" Hidden="True" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="RequestQuantity3">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="3차납기요구일" Key="RequestDate3" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="RequestDate3">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="4차납기요구량" Key="RequestQuantity4" Hidden="True" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="RequestQuantity4">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="4차납기요구일" Key="RequestDate4" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="RequestDate4">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="5차납기요구량" Key="RequestQuantity5" Hidden="True" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="RequestQuantity5">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="5차납기요구일" Key="RequestDate5" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="RequestDate5">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
												BaseColumnName="ApplyUnitCost">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" HeaderClickAction="SortMulti" BaseColumnName="ProgressCondition">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPerson">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="RegistrationPersonID">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="RegistrationDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" HeaderClickAction="SortMulti" BaseColumnName="UpdatingPerson">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="UpdatingPersonID">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="UpdatingDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="발주원장번호" Key="ReceivingOrderHistoryIndex" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="ReceivingOrderHistoryIndex">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="볼륨번호" Key="VolumNum" Width="70px" HeaderClickAction="SortMulti" BaseColumnName="VolumNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="번호" Key="ProductionRequestHistoryIndex" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="ProductionRequestHistoryIndex">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
										</Columns>
									</igtbl:UltraGridBand>
								</Bands>
							</igtbl:ultrawebgrid></TD>
					</TR>
					<TR>
						<TD align="left" height="30">
							<asp:button id="bt_Excel" runat="server" Width="60px" Text="Excel" Height="20px"></asp:button>
							<igtblexp:UltraWebGridExcelExporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:UltraWebGridExcelExporter></TD>
						<TD align="right" height="30">&nbsp;&nbsp;&nbsp;&nbsp;</TD>
					</TR>
				</TABLE>
			</FIELDSET>
			</TD></TR></TBODY></TABLE>
		</form>
	</body>
</HTML>
