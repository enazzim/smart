<%@ Page language="c#" Codebehind="QualityInspectionPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.EtcPresentCondition.QualityInspectionPC" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>품질검사 현황</title>
		<meta http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../GridCheck.js"></script>
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
		<script language="javascript"><!--
		
		function ResettxtBox()
		{
			ResetTextBox();
			ResetBox();
			var objChooser1 = igdrp_getComboById("txtStartDate");
			var objChooser2 = igdrp_getComboById("txtEndDate");
			
			objChooser1.setValue(null);
			objChooser2.setValue(null);
			document.QualityInspectionPresentCondition.ddlDivision.options[0].selected=true;	
			document.QualityInspectionPresentCondition.ddlProcessDiv.options[1].selected=true;	
		}
		
		//-->
		</script>
	</HEAD>
	<body bottomMargin="0" bgColor="#f7f6f6" leftMargin="0" topMargin="0" rightMargin="0"
		ms_positioning="GridLayout">
		<form id="QualityInspectionPresentCondition" method="post" runat="server">
			<TABLE id="Table3" style="Z-INDEX: 100; LEFT: 10px; POSITION: absolute; TOP: 10px" height="400"
				cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD vAlign="top" align="center">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; 90BORDER-RIGHT: black 1px solid"
							align="baseline"><LEGEND align="left">[ 검색조건 ]
							</LEGEND>
							<TABLE cellSpacing="0" cellPadding="0" width="800" border="0" DESIGNTIMEDRAGDROP="855">
								<TR>
									<TD width="800" colSpan="10" height="30">
										<TABLE id="table10" cellSpacing="0" cellPadding="0" width="800" border="0">
											<tr>
												<td width="600"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></td>
												<td width="200"><uc1:companysearchcontrol id="CSC1" runat="server"></uc1:companysearchcontrol></td>
											</tr>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD align="right" width="80" height="30">진행상태&nbsp;</TD>
									<TD width="100" height="30"><asp:dropdownlist id="ddlProcessDiv" runat="server" DESIGNTIMEDRAGDROP="856" BackColor="#EEEEE9" Width="100px"
											Height="20px">
											<asp:ListItem Value="0">전체</asp:ListItem>
											<asp:ListItem Value="1" Selected="True">검사완료</asp:ListItem>
											<asp:ListItem Value="2">입고완료</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD align="right" width="70" height="30">등록일&nbsp;</TD>
									<TD width="100" height="30"><igsch:webdatechooser id="txtStartDate" runat="server" BackColor="#EEEEE9" Width="100px" Height="20px"
											BorderStyle="Solid" BorderColor="DimGray" NullDateLabel=" " Text=" ">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD width="10" height="30">~</TD>
									<TD width="100" height="30"><igsch:webdatechooser id="txtEndDate" runat="server" BackColor="#EEEEE9" Width="100px" Height="20px" BorderStyle="Solid"
											BorderColor="DimGray" NullDateLabel=" " Text=" ">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD align="right" width="70" height="30">구&nbsp; 분&nbsp;</TD>
									<TD width="100" height="30"><asp:dropdownlist id="ddlDivision" runat="server" DESIGNTIMEDRAGDROP="856" BackColor="#EEEEE9" Width="100px"
											Height="20px">
											<asp:ListItem Value="전체" Selected="True">전체</asp:ListItem>
											<asp:ListItem Value="구매">구매</asp:ListItem>
											<asp:ListItem Value="외주">외주</asp:ListItem>
											<asp:ListItem Value="자가">자가</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD align="right" width="270" colSpan="2" height="30"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
											type="button" value="초기화" name="btnReset">&nbsp;
										<asp:button id="btnSearch" runat="server" Width="65px" Height="20px" Text="검  색" Font-Size="10pt"
											CausesValidation="False"></asp:button></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD align="center">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 300px"><LEGEND align="left">[ 
								검색결과 ]
							</LEGEND>
							<igtbl:ultrawebgrid id="DataGrid1" runat="server" Height="387px" Width="100%">
								<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" RowHeightDefault="20px" RowSizingDefault="Free"
									Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti"
									BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="DataGrid1"
									TableLayout="Fixed" CellClickActionDefault="RowSelect">
									<AddNewBox>
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

										</Style>
									</AddNewBox>
									<Pager PageSize="16" StyleMode="ComboBox" AllowPaging="True">
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

										</Style>
									</Pager>
									<HeaderStyleDefault Cursor="Hand" VerticalAlign="Middle" BorderColor="DimGray" BorderStyle="Solid" HorizontalAlign="Center"
										ForeColor="Black" BackColor="LightGray" Height="25px">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</HeaderStyleDefault>
									<RowSelectorStyleDefault BorderStyle="Solid"></RowSelectorStyleDefault>
									<FrameStyle Width="100%" Cursor="Default" BorderWidth="1px" Font-Size="9pt" Font-Names="Verdana"
										BorderColor="DimGray" BorderStyle="Groove" BackColor="Silver" Height="387px"></FrameStyle>
									<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</FooterStyleDefault>
									<ActivationObject BorderStyle="Groove" BorderWidth="2px" BorderColor="White"></ActivationObject>
									<RowExpAreaStyleDefault BackColor="White"></RowExpAreaStyleDefault>
									<SelectedHeaderStyleDefault BackColor="White"></SelectedHeaderStyleDefault>
									<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
									<SelectedRowStyleDefault Cursor="Hand" BorderWidth="2px" Font-Bold="True" BorderStyle="Groove" ForeColor="PapayaWhip"
										BackColor="Navy"></SelectedRowStyleDefault>
									<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
									<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" ForeColor="Black"
										BackColor="#EBEFF6">
										<BorderDetails ColorTop="Gray" WidthLeft="0px" WidthTop="0px" ColorLeft="Gray"></BorderDetails>
									</RowStyleDefault>
								</DisplayLayout>
								<Bands>
									<igtbl:UltraGridBand HeaderClickAction="SortSingle" AllowUpdate="RowTemplateOnly" ColHeadersVisible="Yes"
										CellClickAction="RowSelect">
										<RowExpandEffects ShadowColor="Gray" Duration="100" ShadowWidth="5"></RowExpandEffects>
										<Columns>
											<igtbl:UltraGridColumn HeaderText="구분" Key="historysection1" Width="40px" HeaderClickAction="SortMulti"
												BaseColumnName="historysection1" AllowResize="Free">
												<SelectedHeaderStyle BackColor="White"></SelectedHeaderStyle>
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle Height="25px"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" HeaderClickAction="SortMulti" BaseColumnName="ItemNum"
												AllowResize="Free">
												<CellStyle>
													<Padding Left="5px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Width="130px" HeaderClickAction="SortMulti"
												BaseColumnName="ItemDrawNum" AllowResize="Free">
												<CellStyle>
													<Padding Left="5px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="110px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
												<CellStyle>
													<Padding Left="5px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" Width="150px" HeaderClickAction="SortMulti"
												BaseColumnName="CompanyName">
												<CellStyle>
													<Padding Left="5px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessCompanyNum" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="BusinessRegistrationNum">
												<CellStyle>
													<Padding Left="5px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="시작공정순서" Key="시작공정순서" Width="80px" Format="###,###,##0.00" HeaderClickAction="SortMulti"
												BaseColumnName="시작공정순서">
												<CellStyle HorizontalAlign="Right">
													<Padding Right="8px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="시작공정명" Key="시작공정명" Width="80px" HeaderClickAction="SortMulti" BaseColumnName="시작공정명">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="종료공정순서" Key="종료공정순서" Width="80px" Format="###,###,##0.00" HeaderClickAction="SortMulti"
												BaseColumnName="종료공정순서">
												<CellStyle HorizontalAlign="Right">
													<Padding Right="8px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="종료공정명" Key="종료공정명" Width="80px" HeaderClickAction="SortMulti" BaseColumnName="종료공정명">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="공정코드" Key="종료공정코드" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="종료공정코드"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="의뢰수량" Key="RequestQuantity" Width="65px" Format="###,###,##0.00" HeaderClickAction="SortMulti"
												BaseColumnName="RequestQuantity">
												<CellStyle HorizontalAlign="Right">
													<Padding Right="4px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="적합수량" Key="적합수량" Format="###,###,##0.00" HeaderClickAction="SortMulti"
												BaseColumnName="적합수량">
												<CellStyle HorizontalAlign="Right">
													<Padding Right="4px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="부적합수량" Key="부적합수량" Format="###,###,##0.00" HeaderClickAction="SortMulti"
												BaseColumnName="부적합수량">
												<CellStyle HorizontalAlign="Right">
													<Padding Right="4px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="부적합원인" Key="부적합원인" HeaderClickAction="SortMulti" BaseColumnName="부적합원인">
												<CellStyle>
													<Padding Left="8px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="부적합현상" Key="부적합현상" HeaderClickAction="SortMulti" BaseColumnName="부적합현상">
												<CellStyle>
													<Padding Left="8px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="부적합세부내용" Key="부적합세부내용" HeaderClickAction="SortMulti" BaseColumnName="부적합세부내용">
												<CellStyle>
													<Padding Left="8px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="검사판정" Key="검사판정" HeaderClickAction="SortMulti" BaseColumnName="검사판정">
												<CellStyle>
													<Padding Left="8px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="부적합금액" Key="UnSuitabilityCost" Format="###,###,##0.00" HeaderClickAction="SortMulti"
												BaseColumnName="부적합금액">
												<CellStyle HorizontalAlign="Right">
													<Padding Right="4px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" HeaderClickAction="SortMulti" BaseColumnName="진행상태">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Width="100px" Format="" HeaderClickAction="SortMulti"
												BaseColumnName="RegistrationDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" Width="60px" HeaderClickAction="SortMulti"
												BaseColumnName="RegistrationPerson">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Width="90px" Format="###,###,##0" HeaderClickAction="SortMulti"
												BaseColumnName="ApplyUnitCost"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="원장번호1" Key="HistoryIndex1" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="HistoryIndex1"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="원장구분2" Key="HistorySection2" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="HistorySection2"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="원장번호2" Key="HistoryIndex2" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="HistoryIndex2"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="key" Key="QualityInspectionHistoryIndex" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="QualityInspectionHistoryIndex"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="품질검사 완료일" Key="QualityInspectionCompleteDate" Format="" HeaderClickAction="SortMulti"
												BaseColumnName="QualityInspectionCompleteDate">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="검사자" Key="Investigator" Width="60px" HeaderClickAction="SortMulti" BaseColumnName="Investigator">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="시작공정코드" Key="시작공정코드" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="시작공정코드"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="종료공정코드" Key="종료공정코드" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="종료공정코드"></igtbl:UltraGridColumn>
										</Columns>
									</igtbl:UltraGridBand>
								</Bands>
							</igtbl:ultrawebgrid>
							<TABLE id="Table4" style="WIDTH: 801px; HEIGHT: 14px" cellSpacing="0" cellPadding="0" border="0">
								<TR>
									<TD align="right" height="5"></TD>
									<TD height="5"></TD>
									<TD align="right" height="5"></TD>
									<TD align="right" height="5"></TD>
									<TD align="right" height="5"></TD>
									<TD height="5"></TD>
									<TD align="right" height="5"></TD>
									<TD height="5"></TD>
									<TD align="right" height="5"></TD>
									<TD height="5"></TD>
									<TD height="5"></TD>
								</TR>
								<TR>
									<TD align="right" width="40"></TD>
									<TD align="left" width="112"><asp:literal id="Literal1" runat="server"></asp:literal></TD>
									<TD align="right" width="40"></TD>
									<TD align="left" width="112"><asp:literal id="Literal2" runat="server"></asp:literal></TD>
									<TD align="right" width="40"></TD>
									<TD align="left" width="112"><asp:literal id="Literal3" runat="server"></asp:literal></TD>
									<TD align="right" width="40"></TD>
									<TD align="left" width="112"><asp:literal id="Literal4" runat="server"></asp:literal></TD>
									<TD align="right" width="40"></TD>
									<TD align="left" width="112"><asp:literal id="Literal5" runat="server"></asp:literal></TD>
									<TD align="center" width="40"></TD>
								</TR>
								<TR>
									<TD align="left" colSpan="8"><asp:button id="btExcel" runat="server" Height="20px" Text="Excel"></asp:button></TD>
									<TD align="right" colSpan="3">&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			<igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter>&nbsp;&nbsp;</form>
	</body>
</HTML>
