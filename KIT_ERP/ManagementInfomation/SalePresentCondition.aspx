<%@ Page language="c#" Codebehind="SalePresentCondition.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ManagementInfomation.SalePresentCondition" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SalePresentCondition</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		function Reset()
		{
			ResetTextBox();
			var frm = document.QualityInspectionPresentCondition;
			var objCombo = igcmbo_getComboById("wcCompanyName");
			var objChooser1 = igdrp_getComboById("txtStartDate");
			var objChooser2 = igdrp_getComboById("txtEndDate");
						
			objCombo.setDisplayValue("");
			objChooser1.setValue(null);
			objChooser2.setValue(null);
		}	
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; Z-INDEX: 101; LEFT: 8px; BORDER-LEFT: #696969 2px solid; WIDTH: 800px; BORDER-BOTTOM: #696969 2px solid; POSITION: absolute; TOP: 8px"><LEGEND style="FONT-SIZE: 9pt" align="top">[검색조건]</LEGEND>
				<TABLE id="Table2" style="HEIGHT: 40px" cellSpacing="0" cellPadding="0" width="800" border="0">
					<TR>
						<TD style="WIDTH: 88px" align="right" width="90" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림">거래처명&nbsp;
							</FONT>
						</TD>
						<TD width="100" height="30">
							<igcmbo:webcombo id="wcCompanyName" runat="server" BorderStyle="Solid" BorderWidth="1px" SelBackColor="10, 36, 106"
								BackColor="#EEEEE9" Width="100px" SelectedIndex="-1" ForeColor="Black" Height="20px" BorderColor="DimGray"
								SelForeColor="White" Version="3.00" Editable="True" DropImageXP1="/ig_common/WebGrid3/ig_cmboDown1.bmp"
								DropImageXP2="/ig_common/WebGrid3/ig_cmboDown2.bmp" Font-Size="10pt">
								<Columns>
									<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" Width="250px" BaseColumnName="CompanyName">
										<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
										<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
									</igtbl:UltraGridColumn>
									<igtbl:UltraGridColumn HeaderText="대표자명" Key="PresidentName" Width="0px" Hidden="True" BaseColumnName="PresidentName">
										<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
										<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
									</igtbl:UltraGridColumn>
									<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Width="300px" BaseColumnName="BusinessRegistrationNum">
										<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
										<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
									</igtbl:UltraGridColumn>
									<igtbl:UltraGridColumn HeaderText="번호" Key="CompanyInfoIndex" Width="0px" Hidden="True" BaseColumnName="CompanyInfoIndex">
										<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
										<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
									</igtbl:UltraGridColumn>
								</Columns>
								<DropDownLayout DropdownWidth="550px" BorderCollapse="Separate" RowSelectors="No" RowHeightDefault="20px"
									HeaderClickAction="Select" AutoGenerateColumns="False" DropdownHeight="400px" TableLayout="Fixed"
									StationaryMargins="Header">
									<RowStyle BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="White">
										<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
									</RowStyle>
									<SelectedRowStyle ForeColor="White" BackColor="#0A246A"></SelectedRowStyle>
									<HeaderStyle BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</HeaderStyle>
									<FrameStyle Width="100%" Cursor="Default" BorderWidth="2px" Font-Size="10pt" Font-Names="Verdana"
										BorderStyle="Ridge" BackColor="Silver" Height="100%"></FrameStyle>
								</DropDownLayout>
								<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
							</igcmbo:webcombo></TD>
						<TD width="610" colSpan="3" height="30">
							<uc1:ItemSearchControl id="ItemSearchControl1" runat="server"></uc1:ItemSearchControl></TD>
					</TR>
					<TR>
						<TD align="right" width="90" height="30"><FONT face="굴림"><FONT style="FONT-SIZE: 9pt" face="굴림">매출일자&nbsp;
								</FONT></FONT>
						</TD>
						<TD width="100" height="30">
							<igsch:webdatechooser id="txtStartDate" runat="server" BorderStyle="Solid" BackColor="#EEEEE9" Width="100px"
								Height="20px" BorderColor="DimGray" Font-Size="10pt" NullDateLabel=" " Text=" ">
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
						<TD align="center" width="15" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림">~</FONT></TD>
						<TD width="100" height="30">
							<igsch:webdatechooser id="txtEndDate" runat="server" BorderStyle="Solid" BackColor="#EEEEE9" Width="100px"
								Height="20px" BorderColor="DimGray" Font-Size="10pt" NullDateLabel=" " Text=" ">
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
						<TD align="right" width="495" height="30"><FONT face="굴림"><INPUT id="btClear" style="WIDTH: 60px; HEIGHT: 20px" type="button" value="초기화">&nbsp;</FONT>
							<asp:button id="bt_Search" runat="server" Width="60px" Height="20px" Font-Size="10pt" Text="검  색"></asp:button><FONT face="굴림">&nbsp;</FONT>
						</TD>
					</TR>
				</TABLE>
			</FIELDSET>
			<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; Z-INDEX: 102; LEFT: 8px; BORDER-LEFT: #696969 2px solid; WIDTH: 800px; BORDER-BOTTOM: #696969 2px solid; POSITION: absolute; TOP: 112px"><LEGEND style="FONT-SIZE: 9pt" align="top">[검색결과]</LEGEND>
				<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" border="0">
					<TR>
						<TD style="HEIGHT: 310px" colSpan="2" height="310">
							<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="406px">
								<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
									RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
									HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
									RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect"
									NoDataMessage="검색결과가 없습니다!">
									<AddNewBox>
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										</Style>
									</AddNewBox>
									<Pager PageSize="17" StyleMode="ComboBox" AllowPaging="True">
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										</Style>
									</Pager>
									<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</HeaderStyleDefault>
									<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
									<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="Verdana" BorderColor="DimGray"
										BorderStyle="Solid" BackColor="Silver" Height="406px"></FrameStyle>
									<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</FooterStyleDefault>
									<ClientSideEvents ColumnHeaderClickHandler="UltraWebGrid1_ColumnHeaderClickHandler" BeforeRowTemplateOpenHandler="UltraWebGrid1_BeforeRowTemplateOpenHandler"
										AfterRowTemplateCloseHandler="UltraWebGrid1_AfterRowTemplateCloseHandler" MouseOverHandler="UltraWebGrid1_MouseOverHandler"></ClientSideEvents>
									<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
									<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
									<RowAlternateStyleDefault Cursor="Hand" BackColor="#EBEFF6"></RowAlternateStyleDefault>
									<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
										<Padding Left="3px"></Padding>
										<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
									</RowStyleDefault>
								</DisplayLayout>
								<Bands>
									<igtbl:UltraGridBand AllowUpdate="RowTemplateOnly">
										<Columns>
											<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="120px" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
												<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
												<HeaderStyle Height="25px"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Width="120px" HeaderClickAction="SortMulti"
												BaseColumnName="ItemDrawNum">
												<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="120px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
												<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" Width="150px" HeaderClickAction="SortMulti"
												BaseColumnName="CompanyName">
												<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="BusinessRegistrationNum">
												<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="총매출량" Key="Quantity" Width="150px" Format="###,###,###" HeaderClickAction="SortMulti"
												BaseColumnName="Quantity">
												<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="총금액" Key="Cost" Width="140px" Format="###,###,###" HeaderClickAction="SortMulti"
												BaseColumnName="Cost">
												<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellButtonStyle>
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
										</Columns>
										<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
											<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
										</RowTemplateStyle>
										<RowEditTemplate>
											<TABLE style="WIDTH: 800px; HEIGHT: 58px">
												<TR>
													<TD align="right" aline="right"><FONT face="굴림">품목명</FONT></TD>
													<TD aline="left"><FONT face="굴림"><INPUT id="igtbl_TextBox_0_3" style="WIDTH: 100px" readOnly type="text" columnKey="ItemName"></FONT></TD>
													<TD aline="right"><FONT face="굴림">거래처</FONT></TD>
													<TD aline="left"><FONT face="굴림"><INPUT id="igtbl_TextBox_0_4" style="WIDTH: 100px" readOnly type="text" columnKey="CompanyName"></FONT></TD>
													<TD align="right" aline="right"><FONT face="굴림">출고량</FONT></TD>
													<TD aline="left"><INPUT id="igtbl_TextBox_0_6" style="WIDTH: 100px; TEXT-ALIGN: right" readOnly type="text"
															columnKey="OutStorehouseQuantity"></TD>
													<TD align="right" aline="right"><FONT face="굴림">적합량</FONT></TD>
													<TD aline="left"><INPUT id="igtbl_TextBox_0_7" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
															onkeyup="return Process();" style="WIDTH: 100px; TEXT-ALIGN: right" onfocus="OnFocus_Obj(this);"
															type="text" value="0" columnKey="SuitabilityQuantity"></TD>
													<TD align="right" aline="right"><FONT face="굴림">단가</FONT></TD>
													<TD aline="left"><INPUT id="igtbl_TextBox_0_8" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
															onkeyup="return Process();" style="WIDTH: 100px; TEXT-ALIGN: right" type="text" value="0" columnKey="ApplyUnitCost"></TD>
												</TR>
												<TR>
													<TD align="right" aline="right"><FONT face="굴림">발행일</FONT></TD>
													<TD aline="left">
														<igsch:WebDateChooser id="WebDateChooser1" runat="server" Width="101px" Text="Null" NullDateLabel=" ">
															<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
																DayNameFormat="FirstLetter">
																<SELECTEDDAYSTYLE ForeColor="White" BackColor="#0A246A"></SELECTEDDAYSTYLE>
																<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																<DAYHEADERSTYLE ForeColor="#D4D0C8" BackColor="Gray"></DAYHEADERSTYLE>
															</CALENDARLAYOUT>
															<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
															<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
															<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
														</igsch:WebDateChooser></TD>
													<TD align="right" aline="right"><FONT face="굴림">매출일</FONT></TD>
													<TD aline="left">
														<igsch:WebDateChooser id="WebDateChooser2" runat="server" Width="101px" Text="Null" NullDateLabel=" ">
															<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
																DayNameFormat="FirstLetter">
																<SELECTEDDAYSTYLE ForeColor="White" BackColor="#0A246A"></SELECTEDDAYSTYLE>
																<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																<DAYHEADERSTYLE ForeColor="#D4D0C8" BackColor="Gray"></DAYHEADERSTYLE>
															</CALENDARLAYOUT>
															<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
															<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
															<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
														</igsch:WebDateChooser></TD>
													<TD aline="right"><FONT face="굴림"></FONT></TD>
													<TD aline="left"></TD>
													<TD align="right" aline="right"><FONT face="굴림">총금액</FONT></TD>
													<TD aline="left"><INPUT id="igtbl_TextBox_0_9" style="WIDTH: 100px; TEXT-ALIGN: right" type="text" value="0"
															columnKey="TotalCost"></TD>
													<TD align="right" aline="right"><FONT face="굴림">부가세</FONT></TD>
													<TD aline="left"><INPUT id="igtbl_TextBox_0_10" style="WIDTH: 100px; TEXT-ALIGN: right" type="text" value="10"
															columnKey="SupplementaryValueTaxRate"></TD>
												</TR>
											</TABLE>
											<P align="center">&nbsp;</P>
											<P align="center"><FONT face="굴림"></FONT>&nbsp;</P>
											<P align="center">&nbsp;</P>
											<P align="center"><INPUT id="igtbl_reOkBtn" style="WIDTH: 50px; HEIGHT: 22px" onclick="igtbl_gRowEditButtonClick(event);__doPostBack('lnk_Update','');"
													type="button" value="수정">&nbsp; <INPUT id="igtbl_reCancelBtn" style="WIDTH: 50px; HEIGHT: 22px" onclick="igtbl_gRowEditButtonClick(event);"
													type="button" value="취소"></P>
										</RowEditTemplate>
									</igtbl:UltraGridBand>
								</Bands>
							</igtbl:ultrawebgrid></TD>
					</TR>
					<TR>
						<TD height="30">&nbsp;
							<asp:button id="bt_Excel" runat="server" Width="60px" Height="20px" Text="Excel"></asp:button>
						</TD>
						<TD align="right" height="30">&nbsp;&nbsp;<FONT face="굴림">&nbsp;</FONT></TD>
					</TR>
				</TABLE>
			</FIELDSET>
		</form>
	</body>
</HTML>
