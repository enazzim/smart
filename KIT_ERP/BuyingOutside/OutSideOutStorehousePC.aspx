<%@ Page language="c#" Codebehind="OutSideOutStorehousePC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BuyingOutside.OutSideOutStorehousePC" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../MessageWindows.js"></script>
		<script language="javascript" src="../GridCheck.js"></script>
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--//wdcOutStoreDate

		function ResettxtBox()
		{
			
			ResetTextBox();
			ResetBox();
			var objChooser1 = igdrp_getComboById("wdcStartDate");
			var objChooser2 = igdrp_getComboById("wdcEndDate");
			objChooser1.setValue(null);
			objChooser2.setValue(null);	
			document.Form1.ddlItemClassification1.options[0].selected=true;
			
		}

		function uwgOOS_HT_AfterRowTemplateOpenHandler(gridName, rowId){
			
			var row = igtbl_getRowById(rowId);						
			var wdcDate = igdrp_getComboById("uwgOOSxHTxxctl0xwdcOutStoreDate");
						
			wdcDate.setValue(row.getCellFromKey("OutStorehouseDate").getValue());//출고일자
			
			document.Form1.hdYear.value = wdcDate.getValue().getFullYear();
			document.Form1.hdMonth.value = wdcDate.getValue().getMonth()+1;
			
			var frm = document.Form1;
					
			///////////RowIndex Hidden 에 저장////////////////
			var num = rowId.split('_');
			frm.hdRowIndex.value = num[1];
			/////////////////////////////////////////////////
		}
		
		
		function uwgOOS_HT_BeforeRowTemplateOpenHandler(gridName, rowId, templateId){
			
			var frm = document.Form1;
			var row = igtbl_getRowById(rowId);
			if(row.getCellFromKey("ProgressCondition").getValue() == "대기")
			{
				//수정하기 이전의 출고수량
				frm.hdquantity.value = row.getCellFromKey("ThistimeOutStorehouseQuantity").getValue();
			}
			else
			{
				alert("진행상태가 완료인 항목은 수정할 수 없습니다.!");
				return true;
			}
		}
		
		
		function uwgOOS_HT_AfterRowTemplateCloseHandler(gridName, rowId, bSaveChanges){
			
			var row = igtbl_getRowById(rowId);
			var wdcDate = igdrp_getComboById("uwgOOSxHTxxctl0xwdcOutStoreDate");
			
			row.getCellFromKey("OutStorehouseDate").setValue(wdcDate.getValue());
	
		}
--></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" MS_POSITIONING="GridLayout"
		bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101;POSITION: absolute;PADDING-LEFT: 10px;WIDTH: 800px;HEIGHT: 550px;TOP: 0px;PADDING-TOP: 10px;LEFT: 0px"
				cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD height="54">
						<FIELDSET style="BORDER-BOTTOM: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; HEIGHT: 60px; BORDER-TOP: dimgray 2px solid; BORDER-RIGHT: dimgray 2px solid"
							align="top"><LEGEND align="top">[검색]</LEGEND>
							<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
								<TR height="35">
									<TD align="left" width="600" colSpan="8" height="30">&nbsp;
										<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" border="0">
											<TR>
												<TD align="left" width="200" height="30">
													<uc1:companysearchcontrol id="CSC1" runat="server"></uc1:companysearchcontrol></TD>
												<TD align="left" width="600" height="30">
													<uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30">출고일자</TD>
									<TD align="left" width="100" height="30">
										<igsch:webdatechooser id="wdcStartDate" runat="server" Width="100px" BorderStyle="Solid" BackColor="#EEEEE9"
											Height="20px" BorderColor="DimGray" Text=" " NullDateLabel=" ">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderWidth="1px" BorderStyle="Solid"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD align="center" width="5" height="30">~</TD>
									<TD width="100" height="30">
										<igsch:webdatechooser id="wdcEndDate" runat="server" Width="100px" BorderStyle="Solid" BackColor="#EEEEE9"
											Height="20px" BorderColor="DimGray" Text=" " NullDateLabel=" ">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderWidth="1px" BorderStyle="Solid"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD align="right" width="70" height="30">제품팀&nbsp;</TD>
									<TD width="100" height="30">
										<asp:dropdownlist id="ddlItemClassification1" runat="server" BackColor="#EEEEE9" Width="100px"></asp:dropdownlist></TD>
									<TD align="right" height="30" colSpan="2" width="355">&nbsp;<INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
											type="button" value="초기화" name="btnReset">
										<asp:button id="btnSearch" runat="server" Font-Size="9pt" Height="20px" Width="60px" Text="검   색"
											CommandName="Search"></asp:button>&nbsp;</TD>
								</TR>
								<TR>
									<TD align="right" width="70" colSpan="8" height="8"></TD>
								</TR>
							</TABLE>
							</LEGEND>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" height="400">
						<FIELDSET style="BORDER-BOTTOM: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; HEIGHT: 400px; BORDER-TOP: dimgray 2px solid; BORDER-RIGHT: dimgray 2px solid"
							align="top"><LEGEND align="top">[검색결과]</LEGEND>
							<TABLE cellSpacing="0" cellPadding="0" height="400">
								<TR>
									<TD vAlign="top" align="left"><igtbl:ultrawebgrid id="uwgOOS_HT" runat="server" Height="387px" Width="800px">
											<DisplayLayout AutoGenerateColumns="False" AllowSortingDefault="Yes" RowHeightDefault="20px" Version="3.00"
												SelectTypeRowDefault="Extended" AllowColumnMovingDefault="OnServer" BorderCollapseDefault="Separate"
												AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="uwgOOSxHT" CellClickActionDefault="RowSelect"
												AllowUpdateDefault="RowTemplateOnly">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</AddNewBox>
												<Pager QuickPages="5" PageSize="16" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray" Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="387px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents ColumnHeaderClickHandler="ColumnHeaderClickHandler" BeforeRowTemplateOpenHandler="uwgOOS_HT_BeforeRowTemplateOpenHandler"
													AfterRowTemplateOpenHandler="uwgOOS_HT_AfterRowTemplateOpenHandler" AfterRowTemplateCloseHandler="uwgOOS_HT_AfterRowTemplateCloseHandler"></ClientSideEvents>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="Gray"
													BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="선택" Key="chk" Width="40px" Type="CheckBox" BaseColumnName="" AllowUpdate="Yes">
															<FooterStyle HorizontalAlign="Center"></FooterStyle>
															<SelectedHeaderStyle HorizontalAlign="Center"></SelectedHeaderStyle>
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="120px" BaseColumnName="ItemNum">
															<CellStyle HorizontalAlign="Left">
																<Padding Left="2px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" BaseColumnName="ItemDrawNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="120px" BaseColumnName="ItemName">
															<CellStyle HorizontalAlign="Left">
																<Padding Left="2px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단위" Key="Unit" Width="35px" BaseColumnName="Unit">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="발주비율" Key="OrderRate" Hidden="True" Format="###,###,##0.00" BaseColumnName="OrderRate">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="진척비율" Key="ProgressRate" Hidden="True" Format="###,###,##0.00" BaseColumnName="ProgressRate">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="순서" Key="ProcessSequenceNum" Width="50px" BaseColumnName="ProcessSequenceNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정코드" Key="ProcessCode" Hidden="True" BaseColumnName="ProcessCode">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정명" Key="ProcessName" Width="80px" BaseColumnName="ProcessName">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" BaseColumnName="CompanyName">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" BaseColumnName="BusinessRegistrationNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="금번출고량" Key="ThistimeOutStorehouseQuantity" Width="80px" Format="###,###,##0.00"
															BaseColumnName="ThistimeOutStorehouseQuantity">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="출고일" Key="OutStorehouseDate" Format="yyyy-MM-dd" BaseColumnName="OutStorehouseDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" Width="88px" Hidden="True" BaseColumnName="ProgressCondition">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" BaseColumnName="RegistrationPerson">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" BaseColumnName="UpdatingPerson">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="외주발주원장번호" Key="OutSideOrderHistoryIndex" BaseColumnName="OutSideOrderHistoryIndex">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="외주출고원장번호" Key="OutSideOutStorehouseHistoryIndex" Hidden="True" BaseColumnName="OutSideOutStorehouseHistoryIndex">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="금번발주량" Key="ThisOrderQuantity" Hidden="True" Format="###,###,###" BaseColumnName="ThisOrderQuantity"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="금번납기일" Key="ThisDeliveryDate" Hidden="True" Format="yyyy-MM-dd" BaseColumnName="ThisDeliveryDate"></igtbl:UltraGridColumn>
													</Columns>
													<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
														<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
													</RowTemplateStyle>
													<RowEditTemplate>
														<P align="right"><FONT face="굴림">
																<TABLE id="Table3" style="WIDTH: 520px; HEIGHT: 87px" cellSpacing="1" cellPadding="1" width="520"
																	border="0">
																	<TR>
																		<TD align="right" bgColor="lightgrey">품목번호&nbsp;</TD>
																		<TD><INPUT id="igtbl_TextBox_0_0" style="WIDTH: 100px" readOnly type="text" columnKey="ItemNum"></TD>
																		<TD align="right" bgColor="lightgrey">도면번호&nbsp;</TD>
																		<TD><INPUT id="Text1" style="WIDTH: 100px" readOnly type="text" columnKey="ItemDrawNum"></TD>
																		<TD align="right" bgColor="lightgrey">품목명&nbsp;</TD>
																		<TD><INPUT id="Text2" style="WIDTH: 100px" readOnly type="text" columnKey="ItemName"></TD>
																	</TR>
																	<TR>
																		<TD align="right" bgColor="lightgrey">거래처명&nbsp;</TD>
																		<TD><INPUT id="igtbl_TextBox_0_8" style="WIDTH: 100px" readOnly type="text" columnKey="CompanyName"></TD>
																		<TD align="right" bgColor="lightgrey">공정명&nbsp;</TD>
																		<TD><INPUT id="Text3" style="WIDTH: 100px" readOnly type="text" columnKey="ProcessName"></TD>
																		<TD><FONT face="굴림"></FONT></TD>
																		<TD><FONT face="굴림"></FONT></TD>
																	</TR>
																	<TR>
																		<TD align="right" bgColor="lightgrey">금번출고량&nbsp;</TD>
																		<TD><INPUT id="ThisQuantity" style="WIDTH: 100px" type="text" columnKey="ThistimeOutStorehouseQuantity"></TD>
																		<TD align="right" bgColor="lightgrey">출고일&nbsp;</TD>
																		<TD>
																			<igsch:WebDateChooser id="wdcOutStoreDate" runat="server" NullDateLabel=" " Text="Null" Width="100px">
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
																			</igsch:WebDateChooser></TD>
																		<TD></TD>
																		<TD><INPUT id="igtbl_reCancelBtn" style="WIDTH: 50px" onclick="igtbl_gRowEditButtonClick(event);"
																				type="button" value="취   소">&nbsp; <INPUT id="igtbl_reOkBtn" style="WIDTH: 50px" onclick="igtbl_gRowEditButtonClick(event);__doPostBack('linkUpdate','');"
																				type="button" value="수   정"></TD>
																	</TR>
																</TABLE>
															</FONT>
														</P>
													</RowEditTemplate>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD height="25">
						<table height="25" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD align="left" width="392" style="WIDTH: 391px"><asp:button id="btnExcel" runat="server" Height="20px" Width="60px" Text="Excel" Font-Size="9pt"></asp:button>&nbsp;
									<asp:button id="Button1" runat="server" Text="출문증 발행" Height="20px" Width="80px" Font-Size="9pt"></asp:button>&nbsp;&nbsp;<igtblexp:ultrawebgridexcelexporter id="uwgExcel" runat="server"></igtblexp:ultrawebgridexcelexporter></TD>
								<TD align="right" width="181" style="WIDTH: 181px"><INPUT id="hdquantity" style="BACKGROUND-COLOR: #eeeee9; WIDTH: 56px; HEIGHT: 21px" type="hidden"
										size="4" name="hdquantity" runat="server"><INPUT id="hdRowIndex" style="BACKGROUND-COLOR: #eeeee9; WIDTH: 56px; HEIGHT: 21px" type="hidden"
										size="4" name="hdRowIndex" runat="server">
									<asp:linkbutton id="linkUpdate" runat="server" Visible="False">linkUpdate</asp:linkbutton></TD>
								<TD style="WIDTH: 139px" align="right"><INPUT id="hdYear" style="BACKGROUND-COLOR: #eeeee9; WIDTH: 56px; HEIGHT: 21px" type="hidden"
										size="4" name="hdRowIndex" runat="server"><INPUT id="hdMonth" style="BACKGROUND-COLOR: #eeeee9; WIDTH: 56px; HEIGHT: 21px" type="hidden"
										size="4" name="hdquantity" runat="server"></TD>
								<TD align="right" width="200">
									<asp:button id="btnDelete" runat="server" Font-Size="9pt" Width="60px" Height="20px" Text="삭   제"></asp:button>&nbsp;<asp:button id="btnStop" runat="server" Height="20px" Width="60px" Text="중   단" Font-Size="9pt"></asp:button>&nbsp;</TD>
							</TR>
						</table>
					</TD>
				</TR>
			</TABLE>
			&nbsp;&nbsp;
		</form>
	</body>
</HTML>
