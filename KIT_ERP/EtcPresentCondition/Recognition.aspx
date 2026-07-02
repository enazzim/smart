<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="Recognition.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.EtcPresentCondition.Recognition" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Recognition</title>
		<meta http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../MessageWindows.js"></script>
		<script language="javascript" src="../GridCheck.js"></script>
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
			document.Form1.ddlState.options[0].selected=true;			
					
		}
		
		function UltraWebGrid1_ColumnHeaderClickHandler(gridName, columnId, button)
		{
			GridName = gridName;
    			if(document.Form1.chkAll.value == "true")
    			{
					AllCheck();
				}
				else
				{
					AllUncheck();
				}
		}
		
		function AllCheck() // 전부 체크하기
		{
			var grid = igtbl_getGridById(GridName);
			
			for(var i=0; i<grid.Rows.length; i++)
			{
				var row = grid.Rows.getRow(i);
				row.getCellFromKey("chk").setValue(true);
			}
			document.Form1.chkAll.value = "false";
		}


		function AllUncheck() // 전부체크 해제하기
		{
			var grid = igtbl_getGridById(GridName);
			
			for(var i=0; i<grid.Rows.length; i++)
			{
				var row = grid.Rows.getRow(i);
				row.getCellFromKey("chk").setValue(false);
			}
			document.Form1.chkAll.value = "true";
		}
		
--></SCRIPT>
	</HEAD>
	<body bottomMargin="0" bgColor="#f7f6f6" leftMargin="0" topMargin="0" rightMargin="0"
		MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; WIDTH: 800px; POSITION: absolute; TOP: 10px; HEIGHT: 550px"
				cellSpacing="0" cellPadding="0" width="300" border="0">
				<TR>
					<TD width="20" height="60"><FONT face="굴림"></FONT></TD>
					<TD height="60">&nbsp;
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 20px"
							align="middle"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색 ]
							</LEGEND>
							<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
								<TR height="35">
									<TD align="left" width="600" colSpan="9" height="30">
										<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="800" border="0">
											<TR>
												<TD align="left" width="200" height="30">
													<uc1:CompanySearchControl id="CSC1" runat="server"></uc1:CompanySearchControl></TD>
												<TD align="left" width="600" height="30">
													<uc1:ItemSearchControl id="ItemSearchControl1" runat="server"></uc1:ItemSearchControl></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30">승인상태</TD>
									<TD width="100" height="30">
										<asp:dropdownlist id="ddlState" runat="server" Height="20px" Width="100px" BackColor="#EEEEE9" Font-Size="10pt">
											<asp:ListItem Value="전체">전체</asp:ListItem>
											<asp:ListItem Value="미승인">미승인</asp:ListItem>
											<asp:ListItem Value="승인">승인</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD align="right" width="70" height="30">입고일자&nbsp;</TD>
									<TD align="left" width="100" height="30"><igsch:webdatechooser id="wdcStartDate" runat="server" BackColor="#EEEEE9" BorderStyle="Solid" Width="100px"
											Height="20px" BorderColor="DimGray" Text=" " NullDateLabel=" ">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid" BackColor="#EEEEE9"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD align="center" width="5" height="30">~</TD>
									<TD width="100" height="30"><igsch:webdatechooser id="wdcEndDate" runat="server" BackColor="#EEEEE9" BorderStyle="Solid" Width="100px"
											Height="20px" BorderColor="DimGray" Text=" " NullDateLabel=" ">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid" BackColor="#EEEEE9"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<td width="70" align="right"></td>
									<td width="100" align="left"></td>
									<TD align="right" height="30">&nbsp;<INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
											type="button" value="초기화" name="btnReset">
										<asp:button id="btnSearch" runat="server" Width="65px" Height="20px" Text="검   색" Font-Size="9pt"></asp:button>&nbsp;
									</TD>
								</TR>
								<TR>
									<TD align="right" colSpan="9" height="8"></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="20" height="400"></TD>
					<TD height="400">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 400px"
							align="top"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색결과 ]
							</LEGEND>
							<TABLE id="Table3" style="WIDTH: 100%; HEIGHT: 400px" cellSpacing="0" cellPadding="0">
								<TBODY>
									<TR>
										<TD vAlign="top" align="left">
											<igtbl:ultrawebgrid id="DataGrid1" runat="server" Width="100%" Height="387px">
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
													<ClientSideEvents ColumnHeaderClickHandler="UltraWebGrid1_ColumnHeaderClickHandler"></ClientSideEvents>
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
															<igtbl:UltraGridColumn HeaderText="선택" Key="chk" Width="40px" Type="CheckBox" HeaderClickAction="Select"
																BaseColumnName="chk" AllowUpdate="Yes">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="승인" Key="Recognition" Width="50px" BaseColumnName="Recognition">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" HeaderClickAction="SortMulti" BaseColumnName="ItemNum"
																AllowResize="Free">
																<CellStyle>
																	<Padding Left="5px"></Padding>
																</CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Width="130px" Hidden="True" HeaderClickAction="SortMulti"
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
															<igtbl:UltraGridColumn HeaderText="공정명" Key="EndProcessName" Width="80px" HeaderClickAction="SortMulti"
																BaseColumnName="EndProcessName">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="합격수량" Key="SuitabilityQuantity" Width="65px" Format="###,###,###" HeaderClickAction="SortMulti"
																BaseColumnName="SuitabilityQuantity">
																<CellStyle HorizontalAlign="Right">
																	<Padding Right="4px"></Padding>
																</CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="표준단가" Key="StandardUnitCost" Width="80px" Format="###,###,##0.00" BaseColumnName="StandardUnitCost">
																<CellStyle HorizontalAlign="Right">
																	<Padding Right="4px"></Padding>
																</CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Width="80px" Format="###,###,##0.00" HeaderClickAction="SortMulti"
																BaseColumnName="ApplyUnitCost">
																<CellStyle HorizontalAlign="Right">
																	<Padding Right="4px"></Padding>
																</CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="금액" Key="TotalCost" Width="80px" Format="###,###,##0.0" BaseColumnName="TotalCost">
																<CellStyle HorizontalAlign="Right">
																	<Padding Right="4px"></Padding>
																</CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="구분" Key="Division" Width="40px" HeaderClickAction="SortMulti" BaseColumnName="Division"
																AllowResize="Free">
																<SelectedHeaderStyle BackColor="White"></SelectedHeaderStyle>
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle Height="25px"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" Width="80px" HeaderClickAction="SortMulti"
																BaseColumnName="ProgressCondition">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
																BaseColumnName="RegistrationDate">
																<CellStyle HorizontalAlign="Center"></CellStyle>
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
															<igtbl:UltraGridColumn HeaderText="시작공정순서" Key="BeginProcessSequenceNum" Width="80px" Hidden="True" Format=""
																HeaderClickAction="SortMulti" BaseColumnName="BeginProcessSequenceNum">
																<CellStyle HorizontalAlign="Right">
																	<Padding Right="8px"></Padding>
																</CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="시작공정명" Key="BeginProcessName" Width="80px" Hidden="True" HeaderClickAction="SortMulti"
																BaseColumnName="BeginProcessName">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="공정순서" Key="EndProcessSequenceNum" Width="50px" Hidden="True" Format=""
																HeaderClickAction="SortMulti" BaseColumnName="EndProcessSequenceNum">
																<CellStyle HorizontalAlign="Right">
																	<Padding Right="8px"></Padding>
																</CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="공정코드" Key="종료공정코드" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="종료공정코드"></igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="key" Key="HistoryIndex" Hidden="True" HeaderClickAction="SortMulti"
																BaseColumnName="HistoryIndex"></igtbl:UltraGridColumn>
														</Columns>
													</igtbl:UltraGridBand>
												</Bands>
											</igtbl:ultrawebgrid></TD>
									</TR>
								</TBODY>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="20"></TD>
					<TD>
						<table id="Table4" style="WIDTH: 100%; HEIGHT: 30px" height="30" cellSpacing="0" cellPadding="0"
							width="100%">
							<TR>
								<TD align="left" height="5"></TD>
								<TD align="right" height="5"></TD>
								<TD style="WIDTH: 15px" align="right" height="5"></TD>
								<TD align="right" width="200" height="5"></TD>
							</TR>
							<TR>
								<TD align="center" colSpan="4" height="5"></TD>
							</TR>
							<TR>
								<TD align="left"><asp:button id="btnExcel" runat="server" Width="65px" Height="20px" Text="Excel" Font-Size="9pt"></asp:button><igtblexp:ultrawebgridexcelexporter id="uwgExcel" runat="server"></igtblexp:ultrawebgridexcelexporter></TD>
								<TD align="left">&nbsp;</TD>
								<TD style="WIDTH: 15px" align="right"></TD>
								<TD align="right" width="200">
									<asp:button id="btnRecognition" runat="server" Width="65px" Height="20px" Text="승  인" Enabled="False"></asp:button>&nbsp;
									<asp:button id="btnCancel" runat="server" Width="65px" Height="20px" Text="취  소" Enabled="False"></asp:button>&nbsp;</TD>
							</TR>
						</table>
					</TD>
				</TR>
			</TABLE>
			&nbsp;
		</form>
	</body>
</HTML>
