<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="OtherInOutStorehousePC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ManagementInfomation.OtherInOutStorehousePC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>OtherInOutStorehousePC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--

		var GridName;
		
		function UltraWebGrid1_ColumnHeaderClickHandler(gridName, columnId, button)
		{
			GridName = gridName;
    			if(document.OtherInOutStorehousePC.chkAll.value == "true")
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
			document.OtherInOutStorehousePC.chkAll.value = "false";
		}


		function AllUncheck() // 전부체크 해제하기
		{
			var grid = igtbl_getGridById(GridName);
			
			for(var i=0; i<grid.Rows.length; i++)
			{
				var row = grid.Rows.getRow(i);
				row.getCellFromKey("chk").setValue(false);
			}
			document.OtherInOutStorehousePC.chkAll.value = "true";
		}
		function OK(text)
		{
			if(confirm(text + "하시겠습니까?"))
				return true;
			else
				return false;
		}
		
		// 수정창이 열릴때
		function UltraWebGrid1_AfterRowTemplateOpenHandler(gridName, rowId){
			//Add code to handle your event here.
			var row = igtbl_getRowById(rowId);//InOutStorehouseDistinction
			
			var Inout = row.getCellFromKey("InOutStorehouseDistinction").getValue();//BusinessStorehouseNum
			document.OtherInOutStorehousePC.lb_Index.value = document.OtherInOutStorehousePC.lb_RowIndex.value;//그리드 인덱스
			document.OtherInOutStorehousePC.lb_Store.value = row.getCellFromKey("StorehouseName").getValue();//document.getElementById(rowId).children[7].innerText;//창고번호
			document.OtherInOutStorehousePC.lb_StoreNum.value = row.getCellFromKey("BusinessStorehouseNum").getValue();//document.getElementById(rowId).children[8].innerText;//창고번호
			
			document.OtherInOutStorehousePC.lb_Quantity.value = row.getCellFromKey("InOutStorehouseQuantity").getValue();//document.getElementById(rowId).children[10].innerText;//기존수량입력
			
			if(Inout == "입고")//입출고도면
			{
				document.OtherInOutStorehousePC.lb_InOut.value = "1";
			}
			else 
			{
				document.OtherInOutStorehousePC.lb_InOut.value = "0";
			}
			
			
			
			//창고명
			for(var i=0;i<document.OtherInOutStorehousePC.UltraWebGrid1__ctl0_dl_EditStore.options.length;i++)
			{
				if(document.OtherInOutStorehousePC.UltraWebGrid1__ctl0_dl_EditStore.options[i].innerText == row.getCellFromKey("StorehouseName").getValue())
				{
					
					
					document.OtherInOutStorehousePC.UltraWebGrid1__ctl0_dl_EditStore.options[i].selected=true
				}
			}
			
			//입출고도면
			for(var i=0;i<document.OtherInOutStorehousePC.UltraWebGrid1__ctl0_dl_EditInOut.options.length;i++)
			{
				if(document.OtherInOutStorehousePC.UltraWebGrid1__ctl0_dl_EditInOut.options[i].innerText == row.getCellFromKey("InOutStorehouseDistinction").getValue())
				{
					document.OtherInOutStorehousePC.UltraWebGrid1__ctl0_dl_EditInOut.options[i].selected=true
				}
			}
			
			//입출고 사유
			for(var i=0;i<document.OtherInOutStorehousePC.UltraWebGrid1__ctl0_dl_Reason.options.length;i++)
			{
				if(document.OtherInOutStorehousePC.UltraWebGrid1__ctl0_dl_Reason.options[i].innerText == row.getCellFromKey("InOutStorehouseReason").getValue())
				{
					document.OtherInOutStorehousePC.UltraWebGrid1__ctl0_dl_Reason.options[i].selected=true
				}
			}
			
			
			
			document.OtherInOutStorehousePC.igtbl_TextBox_0_1.value = row.getCellFromKey("ItemNum").getValue();//document.getElementById(rowId).children[1].innerText;
			//document.OtherInOutStorehousePC.igtbl_TextBox_0_2.value = row.getCellFromKey("ItemDrawNum").getValue();//document.getElementById(rowId).children[2].innerText;
			document.OtherInOutStorehousePC.igtbl_TextBox_0_3.value = row.getCellFromKey("ItemName").getValue();//document.getElementById(rowId).children[3].innerText;
			document.OtherInOutStorehousePC.igtbl_TextBox_0_5.value = row.getCellFromKey("ProcessSequenceNum").getValue();//document.getElementById(rowId).children[3].innerText;
			document.OtherInOutStorehousePC.igtbl_TextBox_0_6.value = row.getCellFromKey("ProcessName").getValue();//document.getElementById(rowId).children[3].innerText;
			document.OtherInOutStorehousePC.igtbl_TextBox_0_7.value = row.getCellFromKey("InOutStorehouseQuantity").getValue();//document.getElementById(rowId).children[10].innerText;
			if(row.getCellFromKey("InOutStorehouseDetailReason").getValue() == null)
				document.OtherInOutStorehousePC.igtbl_TextBox_0_10.value = "";
			else
				document.OtherInOutStorehousePC.igtbl_TextBox_0_10.value = row.getCellFromKey("InOutStorehouseDetailReason").getValue();//document.getElementById(rowId).children[13].innerText;
			
			//입출고일자
			var date1 = igdrp_getComboById("UltraWebGrid1xxctl0xWebDateChooser2");
			if(row.getCellFromKey("InOutDate").getValue() !=null)
			{
					date1.setValue(row.getCellFromKey("InOutDate").getValue());
			}
			document.OtherInOutStorehousePC.hdMon.value = date1.getValue().getUTCMonth()+1;
			document.OtherInOutStorehousePC.hdYear.value = date1.getValue().getFullYear();
		}
		
		function Update()
		{
			document.OtherInOutStorehousePC.lb_StoreName.value = document.OtherInOutStorehousePC.UltraWebGrid1__ctl0_dl_EditStore.value;
			document.OtherInOutStorehousePC.lb_Distinction.value = document.OtherInOutStorehousePC.UltraWebGrid1__ctl0_dl_EditInOut.value;
			document.OtherInOutStorehousePC.lb_ReasonCode.value = document.OtherInOutStorehousePC.UltraWebGrid1__ctl0_dl_Reason.value;
			document.OtherInOutStorehousePC.lb_Reason.value = document.OtherInOutStorehousePC.UltraWebGrid1__ctl0_dl_Reason.options[document.OtherInOutStorehousePC.UltraWebGrid1__ctl0_dl_Reason.selectedIndex].innerText;
			
			__doPostBack('lnk_Update','');
			
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
		function UltraWebGrid1_MouseOverHandler(gridName, id, button){
			//Add code to handle your event here.  
			if(button == 0) 
			{ 
				// Are we over a cell  var cell = igtbl_getElementById(id);
				//cell.style.cursor = 'hand';  
				//var label = igtbl_getElementById("lb_RowIndex");  
				var parts = id.split("_");  
				document.OtherInOutStorehousePC.lb_RowIndex.value = parts[1];  
			}
		}
		function UltraWebGrid1_AfterRowTemplateCloseHandler(gridName, rowId, bSaveChanges){
			
			
			
			// 수정창이 닫히면서 값을 그리드에 뿌려주는 함수
			if(event.srcElement.id == "igtbl_reOkBtn")
			{
				var frm = document.OtherInOutStorehousePC;
				var row = igtbl_getRowById(rowId);
				var date = igdrp_getComboById("UltraWebGrid1xxctl0xWebDateChooser2");
				
				//기타입출고창고
				row.getCellFromKey("StorehouseName").setValue(frm.UltraWebGrid1__ctl0_dl_EditStore.options[frm.UltraWebGrid1__ctl0_dl_EditStore.selectedIndex].innerText);
				
				//입출고사유
				row.getCellFromKey("InOutStorehouseReasonCode").setValue(frm.UltraWebGrid1__ctl0_dl_Reason.options[frm.UltraWebGrid1__ctl0_dl_Reason.selectedIndex].value);
				row.getCellFromKey("InOutStorehouseReason").setValue(frm.UltraWebGrid1__ctl0_dl_Reason.options[frm.UltraWebGrid1__ctl0_dl_Reason.selectedIndex].innerText);
				
				//입출고구분
				row.getCellFromKey("InOutStorehouseDistinction").setValue(frm.UltraWebGrid1__ctl0_dl_EditInOut.options[frm.UltraWebGrid1__ctl0_dl_EditInOut.selectedIndex].value);
				
				//입출고일자
				row.getCellFromKey("InOutDate").setValue(date.getValue());
			}
		}
		
--></SCRIPT>
	</HEAD>
	<BODY ms_positioning="GridLayout">
		<form id="OtherInOutStorehousePC" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 7px" height="480"
				cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 780px; BORDER-BOTTOM: #696969 2px solid; HEIGHT: 40px"><LEGEND align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800">
								<tr>
									<td width="600" colSpan="6" height="30"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></td>
									<td align="right" width="70" height="30"><FONT face="굴림">창고&nbsp; </FONT>
									</td>
									<td width="130" height="30"><asp:dropdownlist id="dl_Store" runat="server" BackColor="#EEEEE9" Height="20px" Width="100px">
											<asp:ListItem Value="0">- 선 택 -</asp:ListItem>
											<asp:ListItem Value="원자재창고">원자재창고</asp:ListItem>
											<asp:ListItem Value="생산창고">생산창고</asp:ListItem>
											<asp:ListItem Value="외주창고">외주창고</asp:ListItem>
											<asp:ListItem Value="영업1창고">영업1창고</asp:ListItem>
											<asp:ListItem Value="영업2창고">영업2창고</asp:ListItem>
											<asp:ListItem Value="영업3창고">영업3창고</asp:ListItem>
											<asp:ListItem Value="납품창고">납품창고</asp:ListItem>
										</asp:dropdownlist></td>
								</tr>
								<tr>
									<td align="right" width="70" height="30"><FONT face="굴림">입출고일자&nbsp; </FONT>
									</td>
									<td width="100" height="30"><FONT face="굴림"><igsch:webdatechooser id="wdc_FromDate" runat="server" BackColor="#EEEEE9" Height="20px" Width="100px"
												NullDateLabel=" " Text=" ">
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
											</igsch:webdatechooser></FONT></td>
									<td align="right" width="15" height="30"><FONT face="굴림">~ </FONT>
									</td>
									<td width="100" height="30"><FONT face="굴림"><igsch:webdatechooser id="wdc_ToDate" runat="server" BackColor="#EEEEE9" Height="20px" Width="100px" NullDateLabel=" "
												Text=" ">
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
											</igsch:webdatechooser></FONT></td>
									<td align="center" width="100" height="30"><FONT face="굴림"><FONT face="굴림"><FONT face="굴림"></FONT></FONT></FONT></td>
									<td width="215" height="30"><FONT face="굴림"></FONT></td>
									<td align="right" width="215" colSpan="2" height="30"><FONT face="굴림"><INPUT id="lb_Distinction" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1"
												runat="server"><INPUT id="lb_StoreName" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1"
												runat="server"><INPUT id="lb_Reason" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1" runat="server"><INPUT id="lb_ReasonCode" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1"
												runat="server">
											<asp:button id="bt_Clear" runat="server" Height="20px" Width="60px" Text="초기화"></asp:button>&nbsp;
											<asp:button id="bt_Search" runat="server" Height="20px" Width="60px" Text="검  색"></asp:button>&nbsp;
										</FONT>
									</td>
								</tr>
								<TR>
									<TD align="right" width="780" colSpan="10">&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
						&nbsp;&nbsp;&nbsp;&nbsp;
						<BR>
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top">[검색결과]</LEGEND>
							<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD width="800" colSpan="2"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="407px" Width="100%">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect"
												AllowUpdateDefault="RowTemplateOnly">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</AddNewBox>
												<Pager PageSize="17" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</Pager>
												<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<RowSelectorStyleDefault Cursor="Hand" BackColor="#C0FFFF"></RowSelectorStyleDefault>
												<FrameStyle Width="100%" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
													BackColor="Silver" Height="407px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents ColumnHeaderClickHandler="UltraWebGrid1_ColumnHeaderClickHandler" AfterRowTemplateOpenHandler="UltraWebGrid1_AfterRowTemplateOpenHandler"
													AfterRowTemplateCloseHandler="UltraWebGrid1_AfterRowTemplateCloseHandler" MouseOverHandler="UltraWebGrid1_MouseOverHandler"></ClientSideEvents>
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
														<igtbl:UltraGridColumn HeaderText="전체" Key="chk" Width="30px" Type="CheckBox" HeaderClickAction="Select"
															BaseColumnName="chk" AllowUpdate="Yes">
															<SelectedCellStyle VerticalAlign="Middle" HorizontalAlign="Center"></SelectedCellStyle>
															<CellStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
															<CellStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Left">
																<Padding Left="2px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ItemDrawNum">
															<CellStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
															<CellStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Left">
																<Padding Left="2px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정순서" Key="ProcessSequenceNum" Width="70px" HeaderClickAction="SortMulti"
															BaseColumnName="ProcessSequenceNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정코드" Key="ProcessCode" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ProcessCode">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정명" Key="ProcessName" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="ProcessName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="창고명" Key="StorehouseName" HeaderClickAction="SortMulti" BaseColumnName="StorehouseName">
															<CellStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="영업창고번호" Key="BusinessStorehouseNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="BusinessStorehouseNum">
															<CellStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="입출고" Key="InOutStorehouseDistinction" Width="50px" HeaderClickAction="SortMulti"
															BaseColumnName="InOutStorehouseDistinction">
															<CellStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="입출고일자" Key="InOutDate" Format="yyyy-MM-dd" BaseColumnName="InOutDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수량" Key="InOutStorehouseQuantity" HeaderClickAction="SortMulti" BaseColumnName="InOutStorehouseQuantity">
															<CellStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="2px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사유코드" Key="InOutStorehouseReasonCode" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="InOutStorehouseReasonCode">
															<CellStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="입출고사유" Key="InOutStorehouseReason" HeaderClickAction="SortMulti" BaseColumnName="InOutStorehouseReason">
															<CellStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="세부사유" Key="InOutStorehouseDetailReason" HeaderClickAction="SortMulti"
															BaseColumnName="InOutStorehouseDetailReason">
															<CellStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" HeaderClickAction="SortMulti" BaseColumnName="CompanyName">
															<CellStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="BusinessRegistrationNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Left"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPerson">
															<CellStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationPersonID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationDate">
															<CellStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" HeaderClickAction="SortMulti" BaseColumnName="UpdatingPerson">
															<CellStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingPersonID">
															<CellStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingDate">
															<CellStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="번호" Key="OtherInOutStorehouseHistoryIndex" HeaderClickAction="SortMulti"
															BaseColumnName="OtherInOutStorehouseHistoryIndex">
															<CellStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
													</Columns>
													<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
														<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
													</RowTemplateStyle>
													<RowEditTemplate>
														<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="780" border="0">
															<TR>
																<TD align="right"><FONT face="굴림">품목번호&nbsp; </FONT>
																</TD>
																<TD><FONT face="굴림"></FONT><INPUT id="igtbl_TextBox_0_1" style="WIDTH: 100px" readOnly type="text" columnKey="ItemNum"></TD>
																<TD align="right"><FONT face="굴림">품목명&nbsp; </FONT>
																</TD>
																<TD><FONT face="굴림"><INPUT id="igtbl_TextBox_0_3" style="WIDTH: 100px" readOnly type="text" columnKey="ItemName"></FONT></TD>
																<TD align="right"><FONT face="굴림">순서&nbsp; </FONT>
																</TD>
																<TD align="left"><FONT face="굴림"><INPUT id="igtbl_TextBox_0_5" style="WIDTH: 100px" readOnly type="text" columnKey="ProcessSequenceNum"></FONT></TD>
																<TD align="right"><FONT face="굴림">공정명&nbsp; </FONT>
																</TD>
																<TD align="left"><FONT face="굴림"><INPUT id="igtbl_TextBox_0_6" style="WIDTH: 100px" readOnly type="text" columnKey="ProcessName"></FONT></TD>
															</TR>
															<TR>
																<TD align="right"><FONT face="굴림">창고명&nbsp; </FONT>
																</TD>
																<TD>
																	<asp:DropDownList id="dl_EditStore" runat="server">
																		<asp:ListItem Value="0">-선 택-</asp:ListItem>
																		<asp:ListItem Value="4">원자재창고</asp:ListItem>
																		<asp:ListItem Value="5">생산창고</asp:ListItem>
																		<asp:ListItem Value="6">외주창고</asp:ListItem>
																		<asp:ListItem Value="1">영업1창고</asp:ListItem>
																		<asp:ListItem Value="2">영업2창고</asp:ListItem>
																		<asp:ListItem Value="3">영업3창고</asp:ListItem>
																		<asp:ListItem Value="7">납품창고</asp:ListItem>
																	</asp:DropDownList></TD>
																<TD align="right"><FONT face="굴림">입출고&nbsp; </FONT>
																</TD>
																<TD>
																	<asp:DropDownList id="dl_EditInOut" runat="server" Width="66px">
																		<asp:ListItem Value="0">출고</asp:ListItem>
																		<asp:ListItem Value="1">입고</asp:ListItem>
																	</asp:DropDownList></TD>
																<TD align="right"><FONT face="굴림">입고일자&nbsp; </FONT>
																</TD>
																<TD align="left">
																	<igsch:webdatechooser id="WebDateChooser2" runat="server" BackColor="#EEEEE9" Height="20px" Width="100px"
																		NullDateLabel=" " Text=" ">
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
																<TD align="right"><FONT face="굴림">수량&nbsp; </FONT>
																</TD>
																<TD align="left"><INPUT id="igtbl_TextBox_0_7" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																		style="WIDTH: 100px; TEXT-ALIGN: right" onfocus="OnFocus_Obj(this);" type="text" columnKey="InOutStorehouseQuantity"></TD>
															</TR>
															<TR>
																<TD align="right"><FONT face="굴림">입출고사유&nbsp; </FONT>
																</TD>
																<TD align="left"><FONT face="굴림">
																		<asp:DropDownList id=dl_Reason runat="server" DataValueField="SmallClassificationCode" DataTextField="SmallClassificationName" DataSource="<%# Store %>">
																		</asp:DropDownList></FONT></TD>
																<TD align="right"><FONT face="굴림">세부사유&nbsp; </FONT>
																</TD>
																<TD align="left" colSpan="5"><INPUT id="igtbl_TextBox_0_10" style="WIDTH: 284px; HEIGHT: 22px" type="text" size="42"
																		columnKey="InOutStorehouseDetailReason"></TD>
															</TR>
														</TABLE>
														<P align="center"><FONT face="굴림"></FONT>&nbsp;</P>
														<P align="center">&nbsp;</P>
														<P align="center"><FONT face="굴림"></FONT>&nbsp;</P>
														<P align="center">&nbsp;</P>
														<P align="center"><INPUT id="igtbl_reOkBtn" style="WIDTH: 50px; HEIGHT: 22px" onclick="igtbl_gRowEditButtonClick(event);Update();"
																type="button" value="수정">&nbsp; <INPUT id="igtbl_reCancelBtn" style="WIDTH: 50px; HEIGHT: 22px" onclick="igtbl_gRowEditButtonClick(event);"
																type="button" value="취소"></P>
													</RowEditTemplate>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
								<TR>
									<TD height="30">&nbsp;<asp:button id="bt_Excel" runat="server" Height="20px" Width="60px" Text="Excel"></asp:button><INPUT id="lb_Index" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="lb_Index" runat="server">
										<INPUT id="lb_Update" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="lb_Update"
											runat="server"> <INPUT id="lb_Quantity" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" value="수정전 수량"
											name="Hidden1" runat="server"> <INPUT id="lb_Store" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" value="수정전 창고명" name="Hidden1"
											runat="server"> <INPUT id="lb_StoreNum" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" value="수정전 창고번호"
											name="Hidden1" runat="server"> <INPUT id="lb_RowIndex" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1"
											runat="server"> <INPUT id="lb_InOut" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1" runat="server"><INPUT id="hdMon" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="hdMon" runat="server"><INPUT id="hdYear" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="hdMon" runat="server"></TD>
									<TD style="COLOR: blue; TEXT-DECORATION: underline" align="right" height="30"><asp:linkbutton id="lnk_Update" runat="server" Visible="False">LinkButton</asp:linkbutton><INPUT id="chkAll" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" value="true"><asp:button id="bt_Delete" runat="server" Height="20px" Width="60px" Text="삭  제"></asp:button><FONT face="굴림">&nbsp;</FONT></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			<igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter></form>
	</BODY>
</HTML>
