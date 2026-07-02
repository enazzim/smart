<%@ Page language="c#" Codebehind="SubBuyingDelivery.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BuyingOutside.SubBuyingDelivery" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SubBuyingDelivery</title>
		<meta http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../MessageWindows.js"></script>
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--
		function ResettxtBox()
		{
			
			ResetTextBox();
			ResetBox();
			var objChooser1 = igdrp_getComboById("wcStartDate");
			var objChooser2 = igdrp_getComboById("wcEndDate");
			objChooser1.setValue(null);
			objChooser2.setValue(null);	
			
		}
		
		
		
		var Test = 0;
		
		
		
		
		function UltraWebGrid1_DblClickHandler(gridName, cellId){
			
			
			// 초기화
			document.Form1.txtItemNum.value = "";
			document.Form1.hdItemDrawNum.value = "";
			document.Form1.txtCost.value = "";
			document.Form1.txtItemName.value = "";
			document.Form1.txtDeliveryQuantity.value = "";
			document.Form1.txtOrderQuantity.value = "";
			document.Form1.txtCashQuantity.value = "";
			document.Form1.txtRemainQuantity.value = "";
			
			Form1.btnRegistration.focus();
			
			
			
			var row = igtbl_getRowById(cellId);//선택한 로우의 인덱스
		
		
				//그리드에서 부모내용을 선택했을때 항목 값채우기
				document.Form1.txtItemNum.value = row.getCellFromKey("ItemNum").getValue();//품목번호
				document.Form1.hdItemDrawNum.value = row.getCellFromKey("ItemDrawNum").getValue();//도면번호
				document.Form1.txtCost.value = row.getCellFromKey("ApplyUnitCost").getValue();//적용단가
				document.Form1.txtItemName.value = row.getCellFromKey("ItemName").getValue();//품목명
				document.Form1.txtDeliveryQuantity.value = row.getCellFromKey("DeliveryRemainQuantity").getValue();//납품수량
				document.Form1.txtOrderQuantity.value = row.getCellFromKey("DeliveryQuantity").getValue();//발주수량
				document.Form1.txtCashQuantity.value = Number(row.getCellFromKey("DeliveryQuantity").getValue()) - Number(row.getCellFromKey("DeliveryRemainQuantity").getValue());//납입수량
				document.Form1.txtRemainQuantity.value = row.getCellFromKey("DeliveryRemainQuantity").getValue();//잔량
				document.Form1.UnitCost.value = row.getCellFromKey("UnitCost").getValue();//단가
				
				document.Form1.HistoryIndex.value = "0";//원장구분 (발주원장을 선택한 것을 표시함)
				document.Form1.IndexNum.value = row.getCellFromKey("SubBuyingOrderHistoryIndex").getValue();//구매발주원장번호
				
		}
		
		function Process()
		{
		
		
			var value = Number(Form1.txtRemainQuantity.value) - Number(Form1.txtDeliveryQuantity.value);
			
			if(value < 0)
			{
				if(Test = 0)
				{
					if(confirm("입고수량이 잔량보다 많습니다! 그래도입고하시겠습니까?")) 
					{
							Test =1;
							return true;
					}
					else
					{
						Form1.txtDeliveryQuantity.value = 0;	
						Form1.txtDeliveryQuantity.focus();	
							return false;
					}
				}
			}
			
		}
		
--></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" onload="document.Form1.btnRegistration.focus();"
		rightMargin="0" XMLNS:igtbl="http://schemas.infragistics.com/ASPNET/WebControls/UltraWebGrid"
		ms_positioning="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 100; LEFT: 10px; POSITION: relative; TOP: 10px" height="550"
				cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD style="HEIGHT: 11px" width="20"></TD>
					<TD style="HEIGHT: 11px">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 20px"
							align="middle"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색 ]
							</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
								<TR height="35">
									<TD align="left" width="800" colSpan="8" height="30">
										<table id="table4" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
											<tr>
												<td width="200"><uc1:companysearchcontrol id="CSC1" runat="server"></uc1:companysearchcontrol></td>
												<td width="600"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></td>
											</tr>
										</table>
									</TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30">납기일&nbsp;</TD>
									<TD align="left" width="100" height="30"><igsch:webdatechooser id="wcStartDate" runat="server" NullDateLabel=" " Text=" " Width="100px" BorderStyle="Solid"
											BackColor="#EEEEE9" Height="20px" BorderColor="DimGray">
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
									<TD width="100" height="30"><igsch:webdatechooser id="wcEndDate" runat="server" NullDateLabel=" " Text=" " Width="100px" BorderStyle="Solid"
											BackColor="#EEEEE9" Height="20px" BorderColor="DimGray">
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
									<td align="right" width="100"></td>
									<td align="left" width="100"></td>
									<TD align="right" width="325" colSpan="2" height="30"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
											type="button" value="초기화" name="btnReset">&nbsp;
										<asp:button id="btnSearch" runat="server" Text="검   색" Width="65px" Height="20px" Font-Size="9pt"></asp:button>&nbsp;
									</TD>
								</TR>
								<TR>
									<TD align="right" colSpan="8" height="8"></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 400px" width="20"></TD>
					<TD style="HEIGHT: 400px" vAlign="top" align="left">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-WEIGHT: normal; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 100%"
							align="middle"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색 ]
							</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" align="center">
								<TR height="35">
									<TD vAlign="top" align="center" rowSpan="1"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="386px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
												RowHeightDefault="20px" Version="3.00" ViewType="Hierarchical" SelectTypeRowDefault="Extended"
												ScrollBarView="Horizontal" ScrollBar="Always" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti"
												BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1"
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
												<HeaderStyleDefault VerticalAlign="Middle" BorderStyle="Solid" BackColor="LightGray" Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="386px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents DblClickHandler="UltraWebGrid1_DblClickHandler"></ClientSideEvents>
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
												<igtbl:UltraGridBand Key="ItemNum">
													<Columns>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="120px" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
															<CellStyle HorizontalAlign="Left">
																<Padding Left="2px"></Padding>
															</CellStyle>
															<HeaderStyle Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ItemDrawNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="120px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
															<CellStyle HorizontalAlign="Left">
																<Padding Left="2px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단위" Key="Unit" Width="35px" BaseColumnName="Unit">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="규 격" Key="Standard" BaseColumnName="Standard"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" HeaderClickAction="SortMulti" BaseColumnName="CompanyName">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="BusinessRegistrationNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="발주량" Key="DeliveryQuantity" Width="80px" Format="###,###,##0" HeaderClickAction="SortMulti"
															BaseColumnName="DeliveryQuantity">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="잔량" Key="DeliveryRemainQuantity" Width="70px" Format="###,###,##0" HeaderClickAction="SortMulti"
															BaseColumnName="DeliveryRemainQuantity">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="납기요구일" Key="DeliveryDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="DeliveryDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="표준단가" Key="UnitCost" Width="60px" BaseColumnName="UnitCost">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="발주단가" Key="ApplyUnitCost" Width="60px" Format="###,###,##0" HeaderClickAction="SortMulti"
															BaseColumnName="ApplyUnitCost">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총금액" Key="TotalCost" Format="###,###,##0" HeaderClickAction="SortMulti"
															BaseColumnName="TotalCost">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" Width="70px" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ProgressCondition">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPerson">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationPersonID"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" HeaderClickAction="SortMulti" BaseColumnName="UpdatingPerson">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingPersonID"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="번호" Key="SubBuyingOrderHistoryIndex" Width="50px" HeaderClickAction="SortMulti"
															BaseColumnName="SubBuyingOrderHistoryIndex">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="20"></TD>
					<TD>
						<TABLE style="WIDTH: 800px; HEIGHT: 46px" cellSpacing="0" cellPadding="0">
							<TR>
								<TD align="right" width="60" height="8"></TD>
								<TD width="100" height="8"></TD>
								<TD align="right" width="60" height="8"></TD>
								<TD width="100" height="8"></TD>
								<TD align="right" width="60" height="8"></TD>
								<TD width="100" height="8"></TD>
								<TD align="right" width="60" height="8"></TD>
								<TD align="left" width="100" height="8"></TD>
								<TD align="right" width="160" height="8"><INPUT id="hdItemDrawNum" style="WIDTH: 67px; HEIGHT: 21px" type="hidden" size="5" name="hdLotNum"
										runat="server"><INPUT id="IndexNum" style="WIDTH: 15px; HEIGHT: 21px" type="hidden" size="1" name="IndexNum"
										runat="server"><INPUT id="HistoryIndex" style="WIDTH: 15px; HEIGHT: 21px" type="hidden" size="1" name="HistoryIndex"
										runat="server"><INPUT id="UnitCost" style="WIDTH: 15px; HEIGHT: 21px" type="hidden" size="1" name="Hidden1"
										runat="server"></TD>
							</TR>
							<TR>
								<TD align="right" width="60">품번&nbsp;</TD>
								<TD width="100"><asp:textbox id="txtItemNum" runat="server" Width="100px" BorderStyle="Solid" BackColor="#EEEEE9"
										Height="20px" BorderColor="DimGray" BorderWidth="1px" Enabled="False"></asp:textbox></TD>
								<TD align="right" width="60">품명&nbsp;</TD>
								<TD width="100"><asp:textbox id="txtItemName" runat="server" Width="100px" BorderStyle="Solid" BackColor="#EEEEE9"
										Height="20px" BorderColor="DimGray" BorderWidth="1px" Enabled="False"></asp:textbox></TD>
								<TD title="품목명:" align="right" width="60">입고량&nbsp;</TD>
								<TD width="100"><asp:textbox id="txtDeliveryQuantity" style="TEXT-ALIGN: right" runat="server" Width="100px"
										BorderStyle="Solid" BackColor="#EEEEE9" Height="20px" BorderColor="DimGray" BorderWidth="1px">0</asp:textbox></TD>
								<TD align="right" width="60">단가 &nbsp;</TD>
								<TD align="left" width="100"><asp:textbox id="txtCost" style="TEXT-ALIGN: right" runat="server" Width="100px" BorderStyle="Solid"
										BackColor="#EEEEE9" Height="20px" BorderColor="DimGray" BorderWidth="1px">0</asp:textbox></TD>
								<TD align="right" width="160">년도
									<asp:dropdownlist id="ddlYear" runat="server" Width="55px" BackColor="#EEEEE9">
										<asp:ListItem Value="2020">2020</asp:ListItem>
												<asp:ListItem Value="2021">2021</asp:ListItem>
												<asp:ListItem Value="2022">2022</asp:ListItem>
												<asp:ListItem Value="2023>2023</asp:ListItem>
												<asp:ListItem Value="2024">2024</asp:ListItem>
												<asp:ListItem Value="2025">2025</asp:ListItem>
												<asp:ListItem Value="2026">2026</asp:ListItem>
												<asp:ListItem Value="2027">2027</asp:ListItem>
												<asp:ListItem Value="2028">2028</asp:ListItem>
												<asp:ListItem Value="2029">2029</asp:ListItem>
												<asp:ListItem Value="2030">2030</asp:ListItem>
									</asp:dropdownlist>월
									<asp:dropdownlist id="ddlMon" runat="server" Width="40px" BackColor="#EEEEE9">
										<asp:ListItem Value="1">1</asp:ListItem>
										<asp:ListItem Value="2">2</asp:ListItem>
										<asp:ListItem Value="3">3</asp:ListItem>
										<asp:ListItem Value="4">4</asp:ListItem>
										<asp:ListItem Value="5">5</asp:ListItem>
										<asp:ListItem Value="6">6</asp:ListItem>
										<asp:ListItem Value="7">7</asp:ListItem>
										<asp:ListItem Value="8">8</asp:ListItem>
										<asp:ListItem Value="9">9</asp:ListItem>
										<asp:ListItem Value="10">10</asp:ListItem>
										<asp:ListItem Value="11">11</asp:ListItem>
										<asp:ListItem Value="12">12</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD title="발주수량:" style="HEIGHT: 24px" align="right" width="60">발주량&nbsp;</TD>
								<TD style="HEIGHT: 24px" width="100"><asp:textbox id="txtOrderQuantity" style="TEXT-ALIGN: right" runat="server" Width="100px" BorderStyle="Solid"
										BackColor="#EEEEE9" Height="20px" BorderColor="DimGray" BorderWidth="1px" Enabled="False"></asp:textbox></TD>
								<TD style="HEIGHT: 24px" align="right" width="60">납입량&nbsp;</TD>
								<TD style="HEIGHT: 24px" width="100"><asp:textbox id="txtCashQuantity" style="TEXT-ALIGN: right" runat="server" Width="100px" BorderStyle="Solid"
										BackColor="#EEEEE9" Height="20px" BorderColor="DimGray" BorderWidth="1px" Enabled="False"></asp:textbox></TD>
								<TD style="HEIGHT: 24px" align="right" width="60">잔량&nbsp;</TD>
								<TD style="HEIGHT: 24px" width="100"><asp:textbox id="txtRemainQuantity" style="TEXT-ALIGN: right" runat="server" Width="100px" BorderStyle="Solid"
										BackColor="#EEEEE9" Height="20px" BorderColor="DimGray" BorderWidth="1px" Enabled="False"></asp:textbox></TD>
								<TD style="HEIGHT: 24px" align="right" width="60">입고일&nbsp;</TD>
								<TD style="HEIGHT: 24px" align="left" width="100"><igsch:webdatechooser id="wdcDeliveryDate" runat="server" NullDateLabel=" " Width="100px" BackColor="#EEEEE9"
										Height="20px">
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
								<TD align="right" width="160"><asp:button id="btnRegistration" runat="server" Text="등   록" Width="65px" Height="20px" Font-Size="9pt"></asp:button>&nbsp;</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
