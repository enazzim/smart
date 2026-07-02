<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Page language="c#" Codebehind="BuyingOrder.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BuyingOutside.BuyingOrder" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BuyingOrder</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript">
		<!--
		
		var GridName;
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
		
		checkState = true;
		
		function uwgBO_HT_ColumnHeaderClickHandler(gridName, columnId, button)
		{
			var grid = igtbl_getGridById(gridName);
			if(checkState)
   			{
				for(var i=0; i<grid.Rows.length; i++)
				{
					var row = grid.Rows.getRow(i);
					row.getCellFromKey("chk").setValue(true);
				}
				checkState = false;
			}
			else
			{
				for(var i=0; i<grid.Rows.length; i++)
				{
					var row = grid.Rows.getRow(i);
					row.getCellFromKey("chk").setValue(false);
				}
				checkState = true;
			}
		}
		
		function OkCancel()
		{
			if(confirm("선택한 품목들의 발주를 보내시겠습니까?")) 
				return true;
			else
				return false;
		}
		
		function ResettxtBox()
		{
			
			ResetTextBox();
			ResetBox();
			
			var objChooser1 = igdrp_getComboById("wdcMinDate");
			var objChooser2 = igdrp_getComboById("wdcMaxDate");
			objChooser1.setValue(null);
			objChooser2.setValue(null);	
			document.Form1.ddlItemClassification1.options[0].selected=true;	
					
		}
		//-->
		</SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="Table" style="POSITION: absolute; WIDTH: 800px; TOP: 10px; LEFT: 10px" cellSpacing="0"
				cellPadding="0" border="0">
				<tr>
					<td style="HEIGHT: 40px">
						<FIELDSET style="BORDER-BOTTOM: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; HEIGHT: 40px; FONT-SIZE: 9pt; BORDER-TOP: dimgray 2px solid; BORDER-RIGHT: dimgray 2px solid"><LEGEND align="top">[검색조건]</LEGEND>
							<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD align="left" colSpan="8" height="30">
										<table id="Table3" cellSpacing="0" cellPadding="0" border="0">
											<tr>
												<td width="200">
													<uc1:CompanySearchControl id="CSC1" runat="server"></uc1:CompanySearchControl></td>
												<td width="600"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></td>
											</tr>
										</table>
									</TD>
								</TR>
								<TR>
									<td align="right" width="70"><FONT face="굴림">제품팀</FONT></td>
									<td width="100"><FONT face="굴림"><asp:dropdownlist id="ddlItemClassification1" runat="server" BackColor="#EEEEE9" Width="100px"></asp:dropdownlist></FONT></td>
									<TD align="right" width="70" height="30"><FONT face="굴림">납입요구일</FONT>&nbsp;</TD>
									<TD vAlign="middle" align="left" width="100" height="30"><igsch:webdatechooser id="wdcMinDate" runat="server" NullDateLabel=" " Text=" " Font-Size="10pt" BorderColor="DimGray"
											BorderStyle="Solid" BackColor="#EEEEE9" Height="18px" Width="100px">
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
									<TD vAlign="middle" align="left" width="100" height="30"><igsch:webdatechooser id="wdcMaxDate" runat="server" NullDateLabel=" " Text=" " Font-Size="10pt" BorderColor="DimGray"
											BorderStyle="Solid" BackColor="#EEEEE9" Height="18px" Width="100px">
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
									<TD style="FONT-SIZE: 9pt" vAlign="middle" align="right" width="355" colSpan="2" height="30"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
											type="button" value="초기화" name="btnReset">&nbsp;
										<asp:button id="btnSearch" runat="server" Text="검   색" Font-Size="9pt" BorderStyle="Outset"
											Height="20px" Width="60px" CommandName="Search"></asp:button>&nbsp;&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</td>
				</tr>
				<TR>
					<TD vAlign="top" align="left" height="5"></TD>
				</TR>
				<tr>
					<td vAlign="top" align="left">
						<FIELDSET style="BORDER-BOTTOM: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; HEIGHT: 100%; BORDER-TOP: dimgray 2px solid; BORDER-RIGHT: dimgray 2px solid"
							align="absBottom"><LEGEND style="FONT-SIZE: 10pt" align="left">[검색결과]&nbsp;
							</LEGEND>
							<TABLE id="Table2" style="WIDTH: 100%; HEIGHT: 405px" cellSpacing="0" cellPadding="0" border="0">
								<TR>
									<TD vAlign="top" align="center"><igtbl:ultrawebgrid id="uwgBO_HT" runat="server" Height="420px" Width="800px" DESIGNTIMEDRAGDROP="20">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
												RowHeightDefault="20px" Version="3.00" ViewType="Hierarchical" SelectTypeRowDefault="Single"
												AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate"
												AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="uwgBOxHT" TableLayout="Fixed" CellClickActionDefault="RowSelect">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</AddNewBox>
												<Pager>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray" Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<RowSelectorStyleDefault BackColor="LightGray"></RowSelectorStyleDefault>
												<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="420px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents ColumnHeaderClickHandler="uwgBO_HT_ColumnHeaderClickHandler"></ClientSideEvents>
												<RowExpAreaStyleDefault BackColor="White"></RowExpAreaStyleDefault>
												<SelectedHeaderStyleDefault BackColor="Silver"></SelectedHeaderStyleDefault>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedGroupByRowStyleDefault BackColor="Silver"></SelectedGroupByRowStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DarkGray"
													BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand HeaderClickAction="SortMulti" AllowSorting="Yes" CellClickAction="RowSelect" AllowColSizing="Free"
													AllowColumnMoving="OnServer">
													<HeaderStyle Cursor="Hand"></HeaderStyle>
													<RowAlternateStyle BackColor="LightSteelBlue"></RowAlternateStyle>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="선택" Key="chk" Width="30px" Type="CheckBox" HeaderClickAction="Select"
															BaseColumnName="chk" AllowUpdate="Yes">
															<CellStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="120px" HeaderClickAction="Select" BaseColumnName="ItemNum">
															<CellStyle HorizontalAlign="Left">
																<Padding Left="2px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="130px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
															<CellStyle HorizontalAlign="Left">
																<Padding Left="2px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단위" Key="Unit" Width="35px" BaseColumnName="Unit">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="규격" Key="Standard" Width="150px" BaseColumnName="Standard"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래처" Key="CompanyName" Width="90px" BaseColumnName="CompanyName"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="1차납품요구량" Key="FirstDeliveryDemandQuantity" Width="70px" Hidden="True"
															Format="###,###,##0.00" HeaderClickAction="SortMulti" BaseColumnName="FirstDeliveryDemandQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="납입요구일" Key="FirstDeliveryDemandDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="FirstDeliveryDemandDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총의뢰량" Key="OrderQuantity" Width="80px" Format="###,###,##0.00" HeaderClickAction="SortMulti"
															BaseColumnName="OrderQuantity">
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
														<igtbl:UltraGridColumn HeaderText="총금액" Key="TotalCost" Format="###,###,###" HeaderClickAction="SortMulti"
															BaseColumnName="TotalCost">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="구매의뢰원장번호" Key="BuyingRequestHistoryIndex" Hidden="True" Format="###,###,##0"
															HeaderClickAction="SortMulti" BaseColumnName="BuyingRequestHistoryIndex">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
													</Columns>
													<RowStyle BackColor="#EBEFF6"></RowStyle>
												</igtbl:UltraGridBand>
												<igtbl:UltraGridBand HeaderClickAction="SortMulti" AddButtonCaption="BO_HT수정" AllowSorting="Yes" Key="BO_HT수정"
													CellClickAction="RowSelect" AllowColSizing="Free" AllowColumnMoving="OnServer" RowSelectors="No"
													Expandable="Yes">
													<HeaderStyle Cursor="Hand"></HeaderStyle>
													<RowAlternateStyle Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyle>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" BaseColumnName="ItemNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" BaseColumnName="ItemDrawNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" BaseColumnName="ItemName">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" Width="150px" BaseColumnName="CompanyName">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" BaseColumnName="BusinessRegistrationNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="1차납품요구량" Key="FirstDeliveryDemandQuantity" Hidden="True" Format="###,###,##0"
															BaseColumnName="FirstDeliveryDemandQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="1차납품요구일" Key="FirstDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
															BaseColumnName="FirstDeliveryDemandDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="2차납품요구량" Key="SecondDeliveryDemandQuantity" Hidden="True" Format="###,###,##0"
															BaseColumnName="SecondDeliveryDemandQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="2차납품요구일" Key="SecondDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
															BaseColumnName="SecondDeliveryDemandDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="3차납품요구량" Key="ThirdDeliveryDemandQuantity" Hidden="True" Format="###,###,##0"
															BaseColumnName="ThirdDeliveryDemandQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="3차납품요구일" Key="ThirdDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
															BaseColumnName="ThirdDeliveryDemandDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="4차납품요구량" Key="FourthDeliveryDemandQuantity" Hidden="True" Format="###,###,##0"
															BaseColumnName="FourthDeliveryDemandQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="4차납품요구일" Key="FourthDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
															BaseColumnName="FourthDeliveryDemandDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="5차납품요구량" Key="FifthDeliveryDemandQuantity" Hidden="True" Format="###,###,##0"
															BaseColumnName="FifthDeliveryDemandQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="5차납품요구일" Key="FifthDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
															BaseColumnName="FifthDeliveryDemandDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총발주량" Key="OrderQuantity" Format="###,###,##0" BaseColumnName="OrderQuantity">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Format="###,###,##0" BaseColumnName="ApplyUnitCost">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총금액" Key="TotalCost" Format="###,###,##0" BaseColumnName="TotalCost">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="발주비율" Key="OrderRate" Hidden="True" BaseColumnName="OrderRate">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="잔량" Key="RemainQuantity" Hidden="True" Format="###,###,##0" BaseColumnName="RemainQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="볼륨번호" Key="VolumNum" Hidden="True" Format="###,###,##0" BaseColumnName="VolumNum">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" Hidden="True" BaseColumnName="ProgressCondition">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" Hidden="True" BaseColumnName="RegistrationPerson">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Hidden="True" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" Hidden="True" BaseColumnName="UpdatingPerson">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Hidden="True" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="구매의뢰원장번호" Key="BuyingRequestHistoryIndex" Hidden="True" Format="###,###,##0"
															BaseColumnName="BuyingRequestHistoryIndex">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="구매발주원장번호" Key="BuyingOrderHistoryIndex" Hidden="True" Format="###,###,##0"
															BaseColumnName="BuyingOrderHistoryIndex">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
													</Columns>
													<RowStyle BackColor="#EBEFF6"></RowStyle>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" height="30"><INPUT id="chkAll" style="WIDTH: 51px; HEIGHT: 22px" type="hidden" size="3" value="true"
											name="Hidden1" runat="server">
										<asp:button id="btnRegistration" runat="server" Text="발   주" BackColor="#EEEEE9" Height="20px"></asp:button><FONT face="굴림">&nbsp;</FONT></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
