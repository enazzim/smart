<%@ Page language="c#" Codebehind="GoodsManufactureOutStorehouse.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BusinessManagement.GoodsManufactureOutStorehouse" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v1.1, Version=1.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>GoodsManufactureOutStorehouse</title>
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
			var objChooser1 = igdrp_getComboById("wdcFromDate");
			var objChooser2 = igdrp_getComboById("wdcToDate");
			objChooser1.setValue(null);
			objChooser2.setValue(null);
			document.GoodsManufactureOutStorehouse.ddlPropertyClassification.options[0].selected=true;
		} 
		
		function UltraWebGrid1_AfterSelectChangeHandler(gridName, id){
		
			
		var row = igtbl_getActiveRow("UltraWebGrid1");
			document.GoodsManufactureOutStorehouse.tb_ItemNum.value = row.getCellFromKey("ItemNum").getValue(); //document.getElementById(id).children[0].innerText;
			document.GoodsManufactureOutStorehouse.tb_ItemDrawNum.value = row.getCellFromKey("ItemDrawNum").getValue();//document.getElementById(id).children[1].innerText;
			document.GoodsManufactureOutStorehouse.tb_ItemName.value = row.getCellFromKey("ItemName").getValue();//document.getElementById(id).children[2].innerText;
			document.GoodsManufactureOutStorehouse.tb_RemainderQuantity.value = MakeCommaSeparate(row.getCellFromKey("RemainderQuantity").getValue());//document.getElementById(id).children[23].innerText;
			document.GoodsManufactureOutStorehouse.tb_UnInspectionQuantity.value = MakeCommaSeparate(row.getCellFromKey("UnInspectionQuantity").getValue());//document.getElementById(id).children[22].innerText;
			document.GoodsManufactureOutStorehouse.tb_OutStoreQuantity.value = MakeCommaSeparate(row.getCellFromKey("OutStorehouseQuantity").getValue());//document.getElementById(id).children[20].innerText;
			document.GoodsManufactureOutStorehouse.idx.value = row.getCellFromKey("ReceivingOrderHistoryIndex").getValue();//document.getElementById(id).children[32].innerText;
			document.GoodsManufactureOutStorehouse.lb_RowSelectIndex.value = document.GoodsManufactureOutStorehouse.lb_RowIndex.value;
			if(row.getCellFromKey("OrderNum").getValue()  == null)
				document.GoodsManufactureOutStorehouse.txtOrderNum.value = "";
			else
				document.GoodsManufactureOutStorehouse.txtOrderNum.value = row.getCellFromKey("OrderNum").getValue();
		
			
			
				__doPostBack('lnk_Update','');
			
		}
		
		function Process()	
		{
			var value = Number(GoodsManufactureOutStorehouse.tb_StoreQuantity.value) - Number(GoodsManufactureOutStorehouse.tb_OutStoreQuantity.value);
			var value1 = Number(GoodsManufactureOutStorehouse.tb_RemainderQuantity.value) - Number(GoodsManufactureOutStorehouse.tb_OutStoreQuantity.value);
			var test = 0;
			/*
			if(value < 0 )	
			{
				alert("- 출고수량이 재고수량보다 많습니다");
				//GoodsManufactureOutStorehouse.tb_OutStoreQuantity.value = GoodsManufactureOutStorehouse.tb_OutStoreQuantity.value.substring(0, GoodsManufactureOutStorehouse.tb_OutStoreQuantity.value.length-1);
				GoodsManufactureOutStorehouse.tb_OutStoreQuantity.value = 0;
				GoodsManufactureOutStorehouse.tb_OutStoreQuantity.focus();
				test =1;
			}
			*/
			/*else if(value1 < 0 && test == 0)
			{
				alert("- 출고수량이 납품잔량보다 많습니다");			
				GoodsManufactureOutStorehouse.tb_OutStoreQuantity.value = 0;
				GoodsManufactureOutStorehouse.tb_OutStoreQuantity.focus();
			}
			*/
			
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
		// 선택된 그리드 Row 인덱스 찾기
		function UltraWebGrid1_MouseOverHandler(gridName, id, button){
			//Add code to handle your event here.
			if(button == 0) { // Are we over a cell
            var cell = igtbl_getElementById(id);
            cell.style.cursor = 'hand';
            //var label = igtbl_getElementById("lb_RowIndex");
            var parts = id.split("_");
            document.GoodsManufactureOutStorehouse.lb_RowIndex.value = parts[1];
            
			}
		}
--></SCRIPT>
</HEAD>
	<body bgColor="#f7f6f6" MS_POSITIONING="GridLayout">
		<form id="GoodsManufactureOutStorehouse" method="post" runat="server">
			<TABLE id="Table1" style="LEFT: 10px; POSITION: absolute; TOP: 10px" height="550" cellSpacing="0"
				cellPadding="0" width="800" border="0">
				<TR>
					<TD vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top">[검색조건]</LEGEND>
							<TABLE id="Table8" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD style="FONT-SIZE: 9pt" width="600" colSpan="9" height="30">
										<table id="table3" border="0" cellpadding="0" cellspacing="0" width="800">
											<tr>
												<td width="200">
													<uc1:CompanySearchControl id="CSC1" runat="server"></uc1:CompanySearchControl></td>
												<td width="600"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol><INPUT id="lb_RowIndex" style="WIDTH: 18px; HEIGHT: 20px; BACKGROUND-COLOR: #eeeee9" type="hidden"
														value="0" runat="server" size="1" NAME="lb_RowIndex"></td>
											</tr>
										</table>
									</TD>
								</TR>
								<TR>
									<TD align="right" width="60" height="30"><FONT face="굴림">수주일자&nbsp; </FONT>
									</TD>
									<TD width="100" height="30"><igsch:webdatechooser id="wdcFromDate" runat="server" Width="100px" BorderColor="DimGray" Height="20px"
											BackColor="#EEEEE9" BorderStyle="Solid" Text=" " NullDateLabel=" ">
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
									<TD align="center" width="15" height="30"><FONT face="굴림">~</FONT></TD>
									<TD width="100" height="30"><igsch:webdatechooser id="wdcToDate" runat="server" Width="100px" BorderColor="DimGray" Height="20px"
											BackColor="#EEEEE9" BorderStyle="Solid" Text=" " NullDateLabel=" ">
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
									<TD align="right" width="60" height="30"><FONT face="굴림">발주번호&nbsp; </FONT>
									</TD>
									<TD align="left" width="100" height="30">
										<asp:textbox id="txtOrderNum1" runat="server" BorderStyle="Inset" BackColor="#EEEEE9" Height="20px"
											Width="100px" Font-Size="9pt"></asp:textbox></TD>
									<TD align="right" width="60" height="30"><FONT face="굴림">자산분류&nbsp; </FONT>
									</TD>
									<TD align="left" width="100" height="30">
										<asp:dropdownlist id="ddlPropertyClassification" runat="server" BackColor="#EEEEE9" Height="20px"
											Width="80px" >
<asp:ListItem Value="0">- 전 체 -</asp:ListItem>
<asp:ListItem Value="1">보용품</asp:ListItem>
										</asp:dropdownlist>
									</TD>
									<td width="205" align="right" height ="30"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
											type="button" value="초기화" name="btnReset">
										<asp:button id="bt_Search" runat="server" Font-Size="10pt" Width="60px" Height="20px" Text="수주검색"></asp:button><FONT face="굴림">
										</FONT></td>&nbsp;&nbsp;
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" width="800">&nbsp;
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="right" width="800"><FONT face="굴림">
							<asp:LinkButton id="LinkButton1" runat="server">수주외 출고</asp:LinkButton></FONT></TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" width="800"><FONT face="굴림">
							<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top">[검색결과]</LEGEND><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="166px">
									<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
										RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
										HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
										RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect">
										<AddNewBox>
											<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
											</Style>
										</AddNewBox>
										<Pager PageSize="5" StyleMode="ComboBox" AllowPaging="True">
											<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
											</Style>
										</Pager>
										<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray">
											<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
										</HeaderStyleDefault>
										<RowSelectorStyleDefault Cursor="Move"></RowSelectorStyleDefault>
										<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderColor="Silver"
											BorderStyle="Solid" BackColor="Silver" Height="166px"></FrameStyle>
										<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
											<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
										</FooterStyleDefault>
										<ClientSideEvents AfterSelectChangeHandler="UltraWebGrid1_AfterSelectChangeHandler" MouseOverHandler="UltraWebGrid1_MouseOverHandler"></ClientSideEvents>
										<EditCellStyleDefault Cursor="Hand" BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
										<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
										<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
										<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
											<Padding Left="3px"></Padding>
											<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
										</RowStyleDefault>
									</DisplayLayout>
									<Bands>
										<igtbl:UltraGridBand AllowUpdate="RowTemplateOnly">
											<Columns>
												<igtbl:UltraGridColumn HeaderText="발주번호" Key="OrderNum" BaseColumnName="OrderNum" NullText=""></igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="140px" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle Height="25px"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="ItemDrawNum">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="140px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" HeaderClickAction="SortMulti" BaseColumnName="CompanyName">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="사업자번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="BusinessRegistrationNum">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="생산의뢰여부" Key="ProductionRequestDivision" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="ProductionRequestDivision">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="자산분류" Key="PropertyClassification" HeaderClickAction="SortMulti" BaseColumnName="PropertyClassification">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="수주일자" Key="ReceivingOrderDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti" BaseColumnName="ReceivingOrderDate">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="총수주량" Key="TotalReceiveingOrderQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti" BaseColumnName="TotalReceiveingOrderQuantity">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="납기요구일" Key="DeliveryRequestDate1" Format="yyyy-MM-dd" HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestDate1">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="1차납기요구량" Key="DeliveryRequestQuantity1" Hidden="True" Format="###,###,###.##" HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestQuantity1">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="2차납기요구량" Key="DeliveryRequestQuantity2" Hidden="True" Format="###,###,###.##" HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestQuantity2">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="2차납기요구일" Key="DeliveryRequestDate2" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestDate2">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="3차납기요구량" Key="DeliveryRequestQuantity3" Hidden="True" Format="###,###,###.##" HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestQuantity3">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="3차납기요구일" Key="DeliveryRequestDate3" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestDate3">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="4차납기요구량" Key="DeliveryRequestQuantity4" Hidden="True" Format="###,###,###.##" HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestQuantity4">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="4차납기요구일" Key="DeliveryRequestDate4" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestDate4">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="5차납기요구량" Key="DeliveryRequestQuantity5" Hidden="True" Format="###,###,###.##" HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestQuantity5">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="5차납기요구일" Key="DeliveryRequestDate5" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestDate5">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Hidden="True" Format="\ ###,###,##0" HeaderClickAction="SortMulti" BaseColumnName="ApplyUnitCost">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="출고수량" Key="OutStorehouseQuantity" Hidden="True" Format="###,###,###.##" HeaderClickAction="SortMulti" BaseColumnName="OutStorehouseQuantity">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="합격수량" Key="SuitabilityQuantity" Hidden="True" Format="###,###,###.##" HeaderClickAction="SortMulti" BaseColumnName="SuitabilityQuantity">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="미검수량" Key="UnInspectionQuantity" Hidden="True" Format="###,###,###.##" HeaderClickAction="SortMulti" BaseColumnName="UnInspectionQuantity">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="납품잔량" Key="RemainderQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti" BaseColumnName="RemainderQuantity">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="볼륨번호" Key="VolumNum" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="VolumNum">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="ProgressCondition">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPerson">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti" BaseColumnName="RegistrationDate">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPersonID">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="UpdatingPerson">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="UpdatingPersonID">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti" BaseColumnName="UpdatingDate">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="번호" Key="ReceivingOrderHistoryIndex" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="ReceivingOrderHistoryIndex">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
											</Columns>
											<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
												<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
											</RowTemplateStyle>
										</igtbl:UltraGridBand>
									</Bands>
								</igtbl:ultrawebgrid></FIELDSET>
						</FONT>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" width="800">&nbsp;
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 800px; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top">[입력]</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD style="WIDTH: 61px; HEIGHT: 15px" align="right" width="61"><FONT face="굴림">출고창고</FONT></TD>
									<TD style="WIDTH: 99px; HEIGHT: 15px"><asp:dropdownlist id="dl_Store" runat="server" Width="100px" Height="20px" BackColor="#EEEEE9" AutoPostBack="True">
<asp:ListItem Value="1">영업1창고</asp:ListItem>
<asp:ListItem Value="7">보용품창고</asp:ListItem>
<asp:ListItem Value="2">영업2창고</asp:ListItem>
<asp:ListItem Value="3">영업3창고</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD style="HEIGHT: 15px" align="right" width="60"><FONT face="굴림">품목번호</FONT></TD>
									<TD style="WIDTH: 100px; HEIGHT: 15px"><asp:textbox id="tb_ItemNum" runat="server" Font-Size="9pt" Width="100px" Height="20px" BackColor="#EEEEE9"
											BorderStyle="Inset" ReadOnly="True"></asp:textbox></TD>
									<TD style="HEIGHT: 15px" align="right" width="60"><FONT face="굴림">도면번호</FONT></TD>
									<TD style="WIDTH: 100px; HEIGHT: 15px"><asp:textbox id="tb_ItemDrawNum" runat="server" Font-Size="9pt" Width="100px" Height="20px" BackColor="#EEEEE9"
											BorderStyle="Inset" ReadOnly="True"></asp:textbox></TD>
									<TD style="HEIGHT: 15px" align="right" width="60"><FONT face="굴림">품명</FONT></TD>
									<TD style="WIDTH: 100px; HEIGHT: 15px"><asp:textbox id="tb_ItemName" runat="server" Font-Size="9pt" Width="100px" Height="20px" BackColor="#EEEEE9"
											BorderStyle="Inset" ReadOnly="True"></asp:textbox></TD>
									<TD style="HEIGHT: 15px" width="100"><FONT face="굴림"></FONT></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 61px" align="right" width="61"><FONT face="굴림">재고수량</FONT></TD>
									<TD style="WIDTH: 99px"><igtxt:webnumericedit id="tb_StoreQuantity" runat="server" Font-Size="9pt" Width="100px" BorderColor="Transparent"
											Height="20px" BackColor="#EEEEE9" BorderStyle="Inset" ValueText="0"></igtxt:webnumericedit></TD>
									<TD align="right" width="60"><FONT face="굴림">납품잔량</FONT></TD>
									<TD style="WIDTH: 100px"><asp:textbox id="tb_RemainderQuantity" style="TEXT-ALIGN: right" runat="server" Font-Size="9pt"
											Width="100px" Height="20px" BackColor="#EEEEE9" BorderStyle="Inset" ReadOnly="True">0</asp:textbox></TD>
									<TD align="right" width="60"><FONT face="굴림">출고수량</FONT></TD>
									<TD style="WIDTH: 100px"><asp:textbox id="tb_OutStoreQuantity" style="TEXT-ALIGN: right" runat="server" Font-Size="9pt"
											Width="100px" Height="20px" BackColor="#EEEEE9" BorderStyle="Inset">0</asp:textbox></TD>
									<TD align="right" width="60"><FONT face="굴림">미검수량</FONT></TD>
									<TD style="WIDTH: 100px"><asp:textbox id="tb_UnInspectionQuantity" style="TEXT-ALIGN: right" runat="server" Font-Size="9pt"
											Width="100px" Height="20px" BackColor="#EEEEE9" BorderStyle="Inset" ReadOnly="True">0</asp:textbox></TD>
									<TD align="right" width="100"><FONT face="굴림"><asp:button id="bt_OutStore" runat="server" Width="60px" Height="20px" Text="출  고"></asp:button>&nbsp;</FONT></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 61px" align="right" width="61"><FONT face="굴림">출고일자</FONT>
									</TD>
									<TD style="WIDTH: 99px"><igsch:webdatechooser id="wdcStartDate" runat="server" Font-Size="10pt" Width="100px" BorderColor="DimGray"
											Height="18px" BackColor="#EEEEE9" BorderStyle="Solid" Text="날짜 선택" NullDateLabel=" " DESIGNTIMEDRAGDROP="116">
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
									<TD align="right" width="60"><FONT face="굴림">발주번호</FONT></TD>
									<TD colSpan="2"><FONT face="굴림">
											<asp:textbox id="txtOrderNum" runat="server" Font-Size="9pt" Width="136px" Height="20px" BackColor="#EEEEE9"
												BorderStyle="Inset" ReadOnly="True"></asp:textbox></FONT></TD>
									<TD style="WIDTH: 100px"><FONT face="굴림"></FONT></TD>
									<TD align="right" width="60"></TD>
									<TD style="WIDTH: 100px"><FONT face="굴림"><asp:textbox id="tb_StoreQuantit" style="TEXT-ALIGN: right" runat="server" Font-Size="9pt" Width="100px"
												Height="20px" BackColor="#EEEEE9" BorderStyle="Inset" ReadOnly="True" ToolTip="tb_StoreQuantity">0</asp:textbox></FONT></TD>
									<TD align="right" width="100"></TD>
								</TR>
								<TR>
									<TD width="100%" colSpan="9" height="20"><igtbl:ultrawebgrid id="UltraWebGrid2" runat="server" Width="100%" Height="166px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" RowHeightDefault="20px" Version="3.00"
												SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti"
												BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid2"
												TableLayout="Fixed" CellClickActionDefault="RowSelect">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</AddNewBox>
												<Pager PageSize="5" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
												<FrameStyle Width="100%" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderColor="Silver"
													BorderStyle="Solid" BackColor="Silver" Height="166px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<RowExpAreaStyleDefault Cursor="Hand"></RowExpAreaStyleDefault>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Groove" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="발주번호" Key="OrderNum" BaseColumnName="OrderNum"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="ItemDrawNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" HeaderClickAction="SortMulti" BaseColumnName="CompanyName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="BusinessRegistrationNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="출고일" Key="OutStoreDate" Format="yyyy-MM-dd" BaseColumnName="OutStoreDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="출고수량" Key="OutStorehouseQuantity" Format="#########" HeaderClickAction="SortMulti" BaseColumnName="OutStorehouseQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="미검수량" Key="UnInspectionQuantity" Width="92px" Format="###,###,###" HeaderClickAction="SortMulti" BaseColumnName="UnInspectionQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="적합수량" Key="SuitabilityQuantity" Hidden="True" Format="#########" HeaderClickAction="SortMulti" BaseColumnName="SuitabilityQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합수량" Key="UnSuitabilityQuantity" Hidden="True" Format="#########" HeaderClickAction="SortMulti" BaseColumnName="UnSuitabilityQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합원인코드" Key="UnSuitabilityCauseCode" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="UnSuitabilityCauseCode">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합원인" Key="UnSuitabilityCauseMeaning" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="UnSuitabilityCauseMeaning">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합내용코드" Key="UnSuitabilityStatusCode" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="UnSuitabilityStatusCode">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합내용" Key="UnSuitabilityStatusMeaning" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="UnSuitabilityStatusMeaning">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합세부내용" Key="UnSuitabilityDetailMeaning" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="UnSuitabilityDetailMeaning">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합금액" Key="UnSuitabilityCost" Hidden="True" Format="\ ###,###,##0" HeaderClickAction="SortMulti" BaseColumnName="UnSuitabilityCost">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Right"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="적용단가" Key="ApplyUnitCost" Hidden="True" Format="\ ###,###,##0" HeaderClickAction="SortMulti" BaseColumnName="ApplyUnitCost">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="검사판정코드" Key="InspectionDecisionCode" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="InspectionDecisionCode">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="검사판정" Key="InspectionDecisionMeaning" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="InspectionDecisionMeaning">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="출고창고명" Key="BusinessStorehouseNum" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="BusinessStorehouseNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래명세서번호" Key="" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" Width="92px" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="ProgressCondition">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPerson">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPersonID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti" BaseColumnName="RegistrationDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="UpdatingPerson">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자ID" Key="" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti" BaseColumnName="UpdatingDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수주원장번호" Key="ReceivingOrderHistoryIndex" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="ReceivingOrderHistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="출고원장번호" Key="OutStorehouseHistoryIndex" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="OutStorehouseHistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
													</Columns>
													<RowTemplateStyle BorderColor="Silver" BorderStyle="Ridge" BackColor="Silver">
														<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
													</RowTemplateStyle>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
								<tr>
									<TD align="right" width="800" colSpan="9" height="30"><asp:button id="btLot" runat="server" Height="20px" Text="..." Visible="False"></asp:button><asp:textbox id="Textbox3" runat="server" Font-Size="9pt" Width="136px" Height="20px" BackColor="#EEEEE9"
											BorderStyle="Inset" ReadOnly="True" Visible="False"></asp:textbox><INPUT id="hdLotNum" style="WIDTH: 19px; HEIGHT: 22px" type="hidden" size="1" runat="server"
											NAME="hdLotNum"><asp:label id="hd_Num" runat="server" Visible="False"></asp:label><asp:linkbutton id="lnk_Update" runat="server" Font-Size="9pt" Visible="False"></asp:linkbutton><INPUT id="lb_RowSelectIndex" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" runat="server"
											NAME="lb_RowSelectIndex"><INPUT id="idx" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" value="0" name="idx" runat="server"><asp:button id="bt_Publication" runat="server" Width="60px" Height="20px" Text="발  행"></asp:button><FONT face="굴림">&nbsp;</FONT></TD>
								</tr>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
		</form></TR></TBODY></TABLE>
	</body>
</HTML>
