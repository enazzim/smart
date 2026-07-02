<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="SubItemDelete.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.SubItemDelete" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SubItemDelete</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript"><!--
		
		function OK(text)
		{
			if(confirm(text + "하시겠습니까?"))
				return true;
			else
				return false;
		}
		
		function UltraWebGrid1_DblClickHandler(gridName, cellId){
			//Add code to handle your event here.
			var row = igtbl_getRowById(cellId);
			document.SubItemDelete.hdItemNum.value = row.getCellFromKey("ItemNum").getValue()				
			document.SubItemDelete.hdItemDrawNum.value = row.getCellFromKey("ItemDrawNum").getValue()				
			document.SubItemDelete.hdItemName.value = row.getCellFromKey("ItemName").getValue()				
			document.SubItemDelete.hdProcessSequenceNum.value = row.getCellFromKey("ProcessSequenceNum").getValue()				
			document.SubItemDelete.hdProcessCode.value = row.getCellFromKey("ProcessCode").getValue()				
			document.SubItemDelete.hdProcessName.value = row.getCellFromKey("ProcessName").getValue()	
		}
		
		
		
		function OnKeyDown_Float(obj) {   
						if (event.ctrlKey || event.shiftKey || event.altKey)   {
							SetEventPass();
							return true;
						}
						var sFieldValue = new String(obj.value);
						// 숫자검사 + Numeric Keypad 숫자검사
						if( (event.keyCode<45 || event.keyCode>57) && (event.keyCode<96 || event.keyCode>105))  {
    						if( event.keyCode == 110 || event.keyCode == 190 ) {
    							// "." 가 2개있는지 검사
	    						if( sFieldValue.indexOf(".") != -1 )	{
	    							SetEventCancel();
									return obj.value;
	    						}
							} else if( !IsNumericKey() ) {
	    						SetEventCancel();
								return obj.value;
							}	    
						} else {    
    						if( sFieldValue.indexOf(".") == -1 ) {
	    						// "0" 을 입력했을 경우 "0" 다음에 "." 가 아니면 무시하여야 한다.	
	    						// 48, 96(Numeric Keypad)
								if( sFieldValue.substr(0,1) == "0" && (event.keyCode == 48 || event.keyCode == 96) ) {
									SetEventCancel();
									return obj.value;
								}
							}
						}
						SetEventPass();
						return true;
					}

					function OnFocus_Obj(obj) {
						obj.select();
					}
					
					function OnBlur_Float(obj) {
						if (ValidateFloat(obj.value) == false) {
							obj.value = "";
    						return false;
						}
					}
					
					function ValidateFloat(a_sValue)	{	
						var s_FieldValue = new String(a_sValue);
						
						if (isNaN(s_FieldValue))
							return false;
							
						return true;
					}
		
//////////////////////////////////////////////////////////////////////////////////////////
//					Currency															//
//////////////////////////////////////////////////////////////////////////////////////////
					function OnKeyDown_Currency(obj) {   
						if (event.ctrlKey || event.shiftKey || event.altKey) {
							SetEventPass();
							return true;
						}
						var sFieldValue = new String(obj.value);        
						if( (event.keyCode<45 || event.keyCode>57) && (event.keyCode<96 || event.keyCode>105))	 {
    						if( event.keyCode == 110 || event.keyCode == 190 )	// "."(점) 이라면
    						{
    							// "." 가 2개있는지 검사
	    						if( sFieldValue.indexOf(".") != -1 ) 	{
	    							SetEventCancel();
									return obj.value;
	    						}
							} else if( !IsCurrencyKey() ){
	    						SetEventCancel();
								return obj.value;
							}	    
						}	else	{    
    						if( sFieldValue.indexOf(".") == -1 )	{
	    						// "0" 을 입력했을 경우 "0" 다음에 "." 가 아니면 무시하여야 한다.	
	    						// 48, 96(Numeric Keypad)
								if( sFieldValue.substr(0,1) == "0" && (event.keyCode == 48 || event.keyCode == 96) )	{
									SetEventCancel();
									return obj.value;
								}
							}
						}
						SetEventPass();
						return true;
					}
					
					function OnKeyUp_Currency(obj)		{
						sFieldValue = new String(obj.value);
						sFieldValue = sFieldValue.replace(/,/gi,"");
						if ( IsNumericKey() )		{
							SetEventPass();
							return true;
						}
						obj.value = MakeCommaSeparate(sFieldValue);
					}
					
					function OnBlur_Cur(obj)	{
						if (ValidateCurrency(obj.value) == false) {
							obj.value = "";
    						return false;
						}
					}
					
					function ValidateCurrency( a_sValue ) {	
						var s_FieldValue = new String(a_sValue);
						
						if (isNaN(parseFloat(s_FieldValue)))
							return false;
							
						return true;
					}

					// OnKeyUp_Currency 에서 호출할 소수점 찍기
					function MakeCommaSeparate(a_Value)	{
						var fl = "";
						var nPointPos = 0;    
					    
						if(isNaN(a_Value)) 	{ 
    						event.returnValue = false;
    						this.value = "";
    						return "";
						}
					    
						if(a_Value == 0) return a_Value;
					    
						if(a_Value < 0) { 
							a_Value=a_Value*(-1);
							fl = "-";
						}else if(a_Value == 0)	{
    						// 처음 입력값이 0부터 시작할때 이것을 제거한다.
    						a_Value = a_Value*1; 
						}
					    
						var a_Value = new String(a_Value);
						var temp = "";
						var sRemain = "";
						var co = 3;
					    
						nPointPos = a_Value.indexOf(".");

						if( nPointPos == -1 )  {
    						num_len = a_Value.length;
						}else{
    						// "." 가 포함되어 있을 경우에 재계산
    						if( parseInt(a_Value.substr(0,nPointPos)) == 0 )	{
    							a_Value = "0" + a_Value.substr(nPointPos);
    							nPointPos = a_Value.indexOf(".");
    						}
    						num_len = nPointPos;
    						sRemain = a_Value.substr(nPointPos);
						}
					    
						while (num_len>0)	{
							num_len = num_len - co;
					        
							if(num_len<0)	{
        						co=num_len+co;
        						num_len=0;
							}
					        temp = "," + a_Value.substr(num_len,co) + temp;
						}
						return fl + temp.substr(1) + sRemain;
					}
					
					function IsNumericKey()  {
						if (
							event.keyCode == 8 ||
							event.keyCode == 9 ||
							event.keyCode == 35 ||
							event.keyCode == 36 ||
							event.keyCode == 37 ||
							event.keyCode == 39 ||
							event.keyCode == 38 ||
							event.keyCode == 40 ||
							event.keyCode == 46 ||
							event.keyCode == 189 ||
							event.keyCode == 109 ||
							event.keyCode == 110 ||		// "."
							event.keyCode == 190			// 키패드 "."
							)
    						return true;
						else
    						return false;
					}
										
					function SetEventPass() {
							event.cancelBubble = false;
							event.returnValue = true;
					}

					function SetEventCancel() {
							event.cancelBubble = true;
							event.returnValue = false;
					}
--></script>
	</HEAD>
	<body bgColor="#f7f6f6" MS_POSITIONING="GridLayout">
		<form id="SubItemDelete" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 100; LEFT: 10px; POSITION: absolute; TOP: 10px" height="530"
				cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD vAlign="top" align="center" width="100%">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD align="right" width="70" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림">창고&nbsp;
										</FONT>
									</TD>
									<TD width="130" height="30">
										<asp:dropdownlist id="dl_Store" runat="server" Width="115px" Height="20px" BackColor="#EEEEE9">
											<asp:ListItem Value="0">- 선 택 -</asp:ListItem>
											<asp:ListItem Value="4">원자재창고</asp:ListItem>
											<asp:ListItem Value="5">생산창고</asp:ListItem>
											<asp:ListItem Value="1">영업1창고</asp:ListItem>
											<asp:ListItem Value="2">영업2창고</asp:ListItem>
											<asp:ListItem Value="3">영업3창고</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD width="600" height="30"><FONT face="굴림">
											<uc1:ItemSearchControl id="ItemSearchControl1" runat="server"></uc1:ItemSearchControl></FONT></TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30"><FONT face="굴림"> </FONT>
									</TD>
									<TD width="130" height="30"><FONT face="굴림"></FONT></TD>
									<TD align="right" width="600" height="30">
										<asp:button id="bt_Clear" runat="server" Width="60px" Height="20px" Text="초기화"></asp:button><FONT face="굴림">&nbsp;</FONT>
										<asp:button id="bt_Search" runat="server" Width="60px" Height="20px" Text="검  색"></asp:button><FONT face="굴림">&nbsp;&nbsp;</FONT></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" height="10"></TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top">[검색결과]</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr>
									<td align="center" colSpan="8" width="800"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="411px" Width="100%">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</AddNewBox>
												<Pager PageSize="18" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</Pager>
												<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
												<FrameStyle Width="100%" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
													BackColor="Silver" Height="411px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents DblClickHandler="UltraWebGrid1_DblClickHandler"></ClientSideEvents>
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
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="450px" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Width="150px" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ItemDrawNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ItemName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
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
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정명" Key="ProcessName" Width="87px" HeaderClickAction="SortMulti" BaseColumnName="ProcessName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="재고수량" Key="StockQuantity12" Width="100px" Format="###,###,###.##" HeaderClickAction="SortMulti"
															BaseColumnName="StockQuantity12">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="재고금액" Key="StockCost12" Width="100px" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
															BaseColumnName="StockCost12">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="번호" Key="StorehouseIndex" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="StorehouseIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></td>
								</tr>
								<TR>
									<TD align="right" width="50" height="30"><FONT face="굴림">WC 명&nbsp; </FONT>
									</TD>
									<TD width="115" height="30"><FONT face="굴림">
											<asp:dropdownlist id="dl_WC" runat="server" BackColor="#EEEEE9" Height="20px" Width="115px"></asp:dropdownlist></FONT></TD>
									<TD align="right" width="50" height="30"><FONT face="굴림">수 량&nbsp; </FONT>
									</TD>
									<TD align="left" width="100" height="30"><FONT face="굴림">
											<asp:textbox id="tb_Quantity" style="TEXT-ALIGN: right" runat="server" BackColor="#EEEEE9" Height="20px"
												Width="100px" BorderStyle="Inset">0</asp:textbox></FONT></TD>
									<TD align="right" width="100" height="30"><FONT face="굴림">투입일자&nbsp; </FONT>
									</TD>
									<TD align="left" width="100" height="30">
										<igsch:webdatechooser id="wdcDate" runat="server" BackColor="#EEEEE9" Height="18px" Width="100px" Text="날짜 선택"
											BorderStyle="Solid" Font-Size="10pt" BorderColor="DimGray" NullDateLabel=" ">
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
									<TD align="right" width="285" colSpan="2" height="30"><FONT face="굴림"><INPUT id="lb_RowSelectIndex" type="hidden" name="Hidden1" runat="server" style="WIDTH: 60px; HEIGHT: 22px"
												size="4"><INPUT id="lb_RowIndex" style="WIDTH: 35px; HEIGHT: 22px" type="hidden" size="1" runat="server">
											<asp:button id="bt_Throw" runat="server" Height="20px" Width="60px" Font-Size="10pt" Text="투입"></asp:button>&nbsp;&nbsp;&nbsp;
										</FONT>
									</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hdProcessName" style="Z-INDEX: 109; LEFT: 840px; WIDTH: 33px; POSITION: absolute; TOP: 392px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden2" runat="server"><INPUT id="hdProcessCode" style="Z-INDEX: 108; LEFT: 840px; WIDTH: 33px; POSITION: absolute; TOP: 360px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden2" runat="server"><INPUT id="hdProcessSequenceNum" style="Z-INDEX: 106; LEFT: 840px; WIDTH: 33px; POSITION: absolute; TOP: 328px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden2" runat="server"><INPUT id="hdItemDrawNum" style="Z-INDEX: 104; LEFT: 840px; WIDTH: 33px; POSITION: absolute; TOP: 264px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden1" runat="server"><INPUT id="Hidden5" style="Z-INDEX: 101; LEFT: 840px; WIDTH: 33px; POSITION: absolute; TOP: 232px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden5" runat="server"><INPUT id="hdItemName" style="Z-INDEX: 105; LEFT: 840px; WIDTH: 33px; POSITION: absolute; TOP: 296px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden2" runat="server"><INPUT id="Hidden1" style="Z-INDEX: 103; LEFT: 840px; WIDTH: 33px; POSITION: absolute; TOP: 264px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden1" runat="server"><INPUT id="hdItemNum" style="Z-INDEX: 102; LEFT: 840px; WIDTH: 33px; POSITION: absolute; TOP: 232px; HEIGHT: 22px"
				type="hidden" size="1" runat="server">
		</form>
	</body>
</HTML>
