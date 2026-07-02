<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v1.1, Version=1.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="GoodsManufactureOutStorehousePC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BusinessManagement.GoodsManufactureOutStorehousePC" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>GoodsManufactureOutStorehousePC</title>
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
			document.GoodsManufactureOutStorehousePC.dlProgressState.options[0].selected=true;
		} 
		var GridName;
		
		function UltraWebGrid1_ColumnHeaderClickHandler(gridName, columnId, button)
		{
			GridName = gridName;
    		if(document.GoodsManufactureOutStorehousePC.chkAll.value == "true")
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
			document.GoodsManufactureOutStorehousePC.chkAll.value = "false";
		}


		function AllUncheck() // 전부체크 해제하기
		{
			var grid = igtbl_getGridById(GridName);
			
			for(var i=0; i<grid.Rows.length; i++)
			{
				var row = grid.Rows.getRow(i);
				row.getCellFromKey("chk").setValue(false);
			}
			document.GoodsManufactureOutStorehousePC.chkAll.value = "true";
		}
		
		function OK(text)
		{
			if(confirm(text + "하시겠습니까?"))
				return true;
			else
				return false;
		}
		
		
		//수정창열기전
		function UltraWebGrid1_BeforeRowTemplateOpenHandler(gridName, rowId, templateId){
			//Add code to handle your event here.
			
			var row = igtbl_getRowById(rowId);
			if(row.getCellFromKey("ProgressCondition").getValue() == "완료")
			{
				alert("완료된 항목은 수정할 수 없습니다.!");
				return true;
			}
			else
			{
				alert("수정할 수 없습니다.!");
					return true;
				
			
				var date = igedit_getById("UltraWebGrid1__ctl0_wdeStartDate", "igtxtUltraWebGrid1__ctl0_wdeStartDate");
				date.setValue(row.getCellFromKey("OutStoreDate").getValue());
				
				document.GoodsManufactureOutStorehousePC.hdyear.value = date.getValue().getFullYear();
				document.GoodsManufactureOutStorehousePC.hdmon.value = date.getValue().getMonth()+1;
				
				document.GoodsManufactureOutStorehousePC.lb_RowSelectIndex.value = document.GoodsManufactureOutStorehousePC.lb_RowIndex.value;
				
				//수정창이 열리기전에 기존의 창고번호와 수량을 저장한다
				var store = row.getCellFromKey("BusinessStorehouseNum").getValue();//창고번호				
				document.GoodsManufactureOutStorehousePC.lb_Store.value = store;
				var quantity = row.getCellFromKey("OutStorehouseQuantity").getValue();//출고수량
				document.GoodsManufactureOutStorehousePC.lb_Quantity.value = quantity;
				
				var UnInspectionQuantity = row.getCellFromKey("UnInspectionQuantity").getValue();//미검수량
				if(UnInspectionQuantity == null )
					document.GoodsManufactureOutStorehousePC.lb_UnInspectionQuantity.value = 0;
				else
					document.GoodsManufactureOutStorehousePC.lb_UnInspectionQuantity.value = UnInspectionQuantity;
				
				var SuitabilityQuantity = row.getCellFromKey("SuitabilityQuantity").getValue();//합격수량
				if(SuitabilityQuantity == null)
					document.GoodsManufactureOutStorehousePC.lb_SuitabilityQuantity.value = 0;
				else
					document.GoodsManufactureOutStorehousePC.lb_SuitabilityQuantity.value = SuitabilityQuantity;
				
				var UnSuitabilityQuantity = row.getCellFromKey("UnSuitabilityQuantity").getValue();//부적합수량
				if(UnSuitabilityQuantity == null)
					document.GoodsManufactureOutStorehousePC.lb_UnSuitabilityQuantity.value = 0;
				else
					document.GoodsManufactureOutStorehousePC.lb_UnSuitabilityQuantity.value = UnSuitabilityQuantity;
				
				if((row.getCellFromKey("ProgressCondition").getValue() != "중단"))
				{
					var objGrid = igtbl_getActiveRow(gridName)//현재 선택되어진 객체의 행의 가져옴
					var row = igtbl_getRowById(rowId);
					//Add code to handle your event here.
					//출고창고
					for(var i=0;i<GoodsManufactureOutStorehousePC.UltraWebGrid1__ctl0_dl_StoreEdit.options.length;i++)
					{
					
						
						if(GoodsManufactureOutStorehousePC.UltraWebGrid1__ctl0_dl_StoreEdit.options[i].innerText == row.getCellFromKey("CompanyName").getValue())
						{
							GoodsManufactureOutStorehousePC.UltraWebGrid1__ctl0_dl_StoreEdit.options[i].selected=true;
							break;
						}
						else
						{
							GoodsManufactureOutStorehousePC.UltraWebGrid1__ctl0_dl_StoreEdit.options[0].selected=true;
						}
					}
					
					
					
					//부적합현상
					for(var i=0;i<GoodsManufactureOutStorehousePC.UltraWebGrid1__ctl0_dl_UnSuitabilityStatusEdit.options.length;i++)
					{
						if(GoodsManufactureOutStorehousePC.UltraWebGrid1__ctl0_dl_UnSuitabilityStatusEdit.options[i].innerText == row.getCellFromKey("UnSuitabilityStatusMeaning").getValue())
						{
							GoodsManufactureOutStorehousePC.UltraWebGrid1__ctl0_dl_UnSuitabilityStatusEdit.options[i].selected=true;
							break;
						}
						else
							GoodsManufactureOutStorehousePC.UltraWebGrid1__ctl0_dl_UnSuitabilityStatusEdit.options[0].selected=true;
					}
					//부적합원인
					for(var j=0;j<GoodsManufactureOutStorehousePC.UltraWebGrid1__ctl0_dl_UnSuitabilityCauseEdit.options.length;j++)
					{
						if(GoodsManufactureOutStorehousePC.UltraWebGrid1__ctl0_dl_UnSuitabilityCauseEdit.options[j].innerText == row.getCellFromKey("UnSuitabilityCauseMeaning").getValue())
						{
							GoodsManufactureOutStorehousePC.UltraWebGrid1__ctl0_dl_UnSuitabilityCauseEdit.options[j].selected=true;
							break;
						}
						else
							GoodsManufactureOutStorehousePC.UltraWebGrid1__ctl0_dl_UnSuitabilityCauseEdit.options[0].selected=true;
					}
					//검사판정
					for(var k=0;k<GoodsManufactureOutStorehousePC.UltraWebGrid1__ctl0_dl_InspectionDecisionEdit.options.length;k++)
					{
						if(GoodsManufactureOutStorehousePC.UltraWebGrid1__ctl0_dl_InspectionDecisionEdit.options[k].innerText == row.getCellFromKey("InspectionDecisionMeaning").getValue())
						{
							GoodsManufactureOutStorehousePC.UltraWebGrid1__ctl0_dl_InspectionDecisionEdit.options[k].selected=true;
							break;
						}
						else
							GoodsManufactureOutStorehousePC.UltraWebGrid1__ctl0_dl_InspectionDecisionEdit.options[0].selected=true;					
					}
				}
				else
				{
					alert("수정할 수 없습니다.!");
					return true;
				}
			}
		}
		function UltraWebGrid1_AfterRowTemplateCloseHandler(gridName, rowId, bSaveChanges)
		{
			if(event.srcElement.id == "igtbl_reOkBtn")
			{
				var frm = document.GoodsManufactureOutStorehousePC;
				var row = igtbl_getRowById(rowId);
				
				var date = igedit_getById("UltraWebGrid1__ctl0_wdeStartDate", "igtxtUltraWebGrid1__ctl0_wdeStartDate");
				row.getCellFromKey("OutStoreDate").setValue(date.getValue());
				
				//출고창고
				row.getCellFromKey("BusinessStorehouseNum").setValue(frm.UltraWebGrid1__ctl0_dl_StoreEdit.options[frm.UltraWebGrid1__ctl0_dl_StoreEdit.selectedIndex].value);
				
				//부적합현상
				if(frm.UltraWebGrid1__ctl0_dl_UnSuitabilityStatusEdit.options[frm.UltraWebGrid1__ctl0_dl_UnSuitabilityStatusEdit.selectedIndex].value == "")
				{
					row.getCellFromKey("UnSuitabilityStatusCode").setValue("");
					row.getCellFromKey("UnSuitabilityStatusMeaning").setValue("");
				}
				else
				{
					row.getCellFromKey("UnSuitabilityStatusCode").setValue(frm.UltraWebGrid1__ctl0_dl_UnSuitabilityStatusEdit.options[frm.UltraWebGrid1__ctl0_dl_UnSuitabilityStatusEdit.selectedIndex].value);
					row.getCellFromKey("UnSuitabilityStatusMeaning").setValue(frm.UltraWebGrid1__ctl0_dl_UnSuitabilityStatusEdit.options[frm.UltraWebGrid1__ctl0_dl_UnSuitabilityStatusEdit.selectedIndex].innerText);
				}
				//부적합원인
				if(frm.UltraWebGrid1__ctl0_dl_UnSuitabilityCauseEdit.options[frm.UltraWebGrid1__ctl0_dl_UnSuitabilityCauseEdit.selectedIndex].value == "")
				{
					row.getCellFromKey("UnSuitabilityCauseCode").setValue(frm.UltraWebGrid1__ctl0_dl_UnSuitabilityCauseEdit.options[0].value);
					row.getCellFromKey("UnSuitabilityCauseMeaning").setValue(frm.UltraWebGrid1__ctl0_dl_UnSuitabilityCauseEdit.options[0].value);
				}
				else
				{
					row.getCellFromKey("UnSuitabilityCauseCode").setValue(frm.UltraWebGrid1__ctl0_dl_UnSuitabilityCauseEdit.options[frm.UltraWebGrid1__ctl0_dl_UnSuitabilityCauseEdit.selectedIndex].value);
					row.getCellFromKey("UnSuitabilityCauseMeaning").setValue(frm.UltraWebGrid1__ctl0_dl_UnSuitabilityCauseEdit.options[frm.UltraWebGrid1__ctl0_dl_UnSuitabilityCauseEdit.selectedIndex].innerText);
				}
				//검사판정
				if(frm.UltraWebGrid1__ctl0_dl_InspectionDecisionEdit.options[frm.UltraWebGrid1__ctl0_dl_InspectionDecisionEdit.selectedIndex].value == "")
				{
					row.getCellFromKey("InspectionDecisionCode").setValue(frm.UltraWebGrid1__ctl0_dl_InspectionDecisionEdit.options[0].value);
					row.getCellFromKey("InspectionDecisionMeaning").setValue(frm.UltraWebGrid1__ctl0_dl_InspectionDecisionEdit.options[0].value);
				}
				else
				{
					row.getCellFromKey("InspectionDecisionCode").setValue(frm.UltraWebGrid1__ctl0_dl_InspectionDecisionEdit.options[frm.UltraWebGrid1__ctl0_dl_InspectionDecisionEdit.selectedIndex].value);
					row.getCellFromKey("InspectionDecisionMeaning").setValue(frm.UltraWebGrid1__ctl0_dl_InspectionDecisionEdit.options[frm.UltraWebGrid1__ctl0_dl_InspectionDecisionEdit.selectedIndex].innerText);
				}
			}
		}
		
		function Process()	
		{			
			var frm = document.GoodsManufactureOutStorehousePC.UltraWebGrid1__ctl0_tb_OutStorehouseQuantityEdit;
			var value = Number(document.GoodsManufactureOutStorehousePC.UltraWebGrid1__ctl0_tb_OutStorehouseQuantityEdit.value) - Number(document.GoodsManufactureOutStorehousePC.UltraWebGrid1__ctl0_tb_SuitabilityQuantityEdit.value);
			var sqTxt = GoodsManufactureOutStorehousePC.UltraWebGrid1__ctl0_tb_SuitabilityQuantityEdit;
			//부적합수량
			if(sqTxt.value = "");
			{
				value = "";
			}
			document.GoodsManufactureOutStorehousePC.UltraWebGrid1__ctl0_lb_UnSuitabilityQuantityEdit.value = value;
			
			if(value < 0 )	{
				alert("- 합격수량이 출고 수량보다 많습니다");
				sqTxt.value = sqTxt.value.substring(0, sqTxt.value.length-1);
				sqTxt.focus();
			}
			else
			{
				var frmDDL1 = document.GoodsManufactureOutStorehousePC.UltraWebGrid1__ctl0_dl_UnSuitabilityCauseEdit;
				var frmDDL2 = document.GoodsManufactureOutStorehousePC.UltraWebGrid1__ctl0_dl_UnSuitabilityStatusEdit;
				var frmDDL3 = document.GoodsManufactureOutStorehousePC.UltraWebGrid1__ctl0_dl_InspectionDecisionEdit;
				
				if ( document.GoodsManufactureOutStorehousePC.UltraWebGrid1__ctl0_lb_UnSuitabilityQuantityEdit.value == 0 )
				{
						frmDDL1.disabled = true;
						frmDDL2.disabled = true;
						frmDDL3.disabled = true;
				}
				else
				{
						frmDDL1.disabled = false;
						frmDDL2.disabled = false;
						frmDDL3.disabled = false;	
				}
			}
			
			
			
			
			
		}
		
		
		function OnKeyDown_Float(obj) 
		{   
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
			{ // Are we over a cell
				var cell = igtbl_getElementById(id);
				cell.style.cursor = 'hand';
				//var label = igtbl_getElementById("lb_RowIndex");
				var parts = id.split("_");
				document.GoodsManufactureOutStorehousePC.lb_RowIndex.value = parts[1];
			}
		}
		
		
--></SCRIPT>
	</HEAD>
	<body bgColor="#f7f6f6" MS_POSITIONING="GridLayout">
		<form id="GoodsManufactureOutStorehousePC" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" height="520"
				cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 780px; BORDER-BOTTOM: #696969 2px solid"
							DESIGNTIMEDRAGDROP="12"><FONT face="굴림"></FONT><LEGEND style="FONT-SIZE: 10pt" align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800">
								<TR>
									<TD style="FONT-SIZE: 9pt" width="600" colSpan="8" height="30">
										<table id="table3" cellSpacing="0" cellPadding="0" width="800" border="0">
											<tr>
												<td width="200"><uc1:companysearchcontrol id="CSC1" runat="server"></uc1:companysearchcontrol></td>
												<td width="600"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></td>
											</tr>
										</table>
									</TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30"><FONT face="굴림">&nbsp;진행상태&nbsp; </FONT>
									</TD>
									<TD width="130" height="30"><asp:dropdownlist id="dlProgressState" runat="server" BackColor="#EEEEE9" Height="20px" Width="100px"
											Font-Size="10pt">
											<asp:ListItem>-선  택-</asp:ListItem>
											<asp:ListItem Value="대기">대기</asp:ListItem>
											<asp:ListItem Value="완료">완료</asp:ListItem>
											<asp:ListItem Value="중단">중단</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD align="right" width="70" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림">출고일자&nbsp;
										</FONT>
									</TD>
									<TD width="100" height="30"><igsch:webdatechooser id="wdcFromDate" runat="server" BackColor="#EEEEE9" Height="20px" Width="100px"
											Font-Size="10pt" BorderStyle="Solid" BorderColor="DimGray" Text=" " NullDateLabel=" ">
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
									<TD align="center" width="15" height="30"><FONT style="FONT-SIZE: 10pt" face="굴림">~</FONT></TD>
									<TD width="215" height="30"><igsch:webdatechooser id="wdcToDate" runat="server" BackColor="#EEEEE9" Height="20px" Width="100px" Font-Size="10pt"
											BorderStyle="Solid" BorderColor="DimGray" Text=" " NullDateLabel=" ">
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
									<TD align="right" width="200" colSpan="2" height="30"><FONT face="굴림"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
												type="button" value="초기화" name="btnReset">&nbsp;
											<asp:button id="bt_Search" runat="server" Height="20px" Width="60px" Text="검  색"></asp:button>&nbsp;
										</FONT>
									</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" width="800"></TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" width="800">&nbsp;
						<BR>
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 800px; BORDER-BOTTOM: #696969 2px solid"><LEGEND style="FONT-SIZE: 10pt" align="top">[검색결과]</LEGEND>
							<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="800">
								<tr>
									<td width="800" colSpan="2"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="406px" Width="800px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect"
												NoDataMessage="">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</AddNewBox>
												<Pager PageSize="17" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</Pager>
												<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<RowSelectorStyleDefault Cursor="Hand" BackColor="White"></RowSelectorStyleDefault>
												<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="Verdana" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="406px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents ColumnHeaderClickHandler="UltraWebGrid1_ColumnHeaderClickHandler" MouseOverHandler="UltraWebGrid1_MouseOverHandler"></ClientSideEvents>
												<RowExpAreaStyleDefault Cursor="Hand"></RowExpAreaStyleDefault>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" BorderColor="White" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand AllowUpdate="RowTemplateOnly">
													<Columns>
														<igtbl:UltraGridColumn HeaderText="전체" Key="chk" Width="30px" Type="CheckBox" HeaderClickAction="Select"
															BaseColumnName="chk" AllowUpdate="Yes">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Left" Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="발주번호" Key="OrderNum" BaseColumnName="OrderNum"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ItemDrawNum">
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
														<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="BusinessRegistrationNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="출고일자" Key="OutStoreDate" Format="yyyy-MM-dd" BaseColumnName="OutStoreDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="출고수량" Key="OutStorehouseQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
															BaseColumnName="OutStorehouseQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="미검수량" Key="UnInspectionQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
															BaseColumnName="UnInspectionQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="적합수량" Key="SuitabilityQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
															BaseColumnName="SuitabilityQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합수량" Key="UnSuitabilityQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
															BaseColumnName="UnSuitabilityQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합원인코드" Key="UnSuitabilityCauseCode" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="UnSuitabilityCauseCode">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합원인" Key="UnSuitabilityCauseMeaning" HeaderClickAction="SortMulti"
															BaseColumnName="UnSuitabilityCauseMeaning">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합내용코드" Key="UnSuitabilityStatusCode" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="UnSuitabilityStatusCode">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합내용" Key="UnSuitabilityStatusMeaning" HeaderClickAction="SortMulti"
															BaseColumnName="UnSuitabilityStatusMeaning">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합세부내용" Key="UnSuitabilityDetailMeaning" HeaderClickAction="SortMulti"
															BaseColumnName="UnSuitabilityDetailMeaning">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합금액" Key="UnSuitabilityCost" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
															BaseColumnName="UnSuitabilityCost">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Right"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="Lot 번호" Key="LotNum" BaseColumnName="LotNum">
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="적용단가" Key="ApplyUnitCost" Format="###,###,###.##" HeaderClickAction="SortMulti"
															BaseColumnName="ApplyUnitCost">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="검사판정코드" Key="InspectionDecisionCode" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="InspectionDecisionCode">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="검사판정" Key="InspectionDecisionMeaning" HeaderClickAction="SortMulti"
															BaseColumnName="InspectionDecisionMeaning">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="출고창고명" Key="BusinessStorehouseNum" HeaderClickAction="SortMulti" BaseColumnName="BusinessStorehouseNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래명세서번호" Key="" HeaderClickAction="SortMulti" BaseColumnName="">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" HeaderClickAction="SortMulti" BaseColumnName="ProgressCondition">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPerson">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationPersonID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" HeaderClickAction="SortMulti" BaseColumnName="UpdatingPerson">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자ID" Key="" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수주원장번호" Key="ReceivingOrderHistoryIndex" HeaderClickAction="SortMulti"
															BaseColumnName="ReceivingOrderHistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="출고원장번호" Key="OutStorehouseHistoryIndex" HeaderClickAction="SortMulti"
															BaseColumnName="OutStorehouseHistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
													</Columns>
													<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
														<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
													</RowTemplateStyle>
													<RowEditTemplate>
														<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="780">
															<TR>
																<TD align="right" width="100" aline="right">
																	<asp:Label id="Label9" runat="server" Font-Size="10pt">출고수량</asp:Label></TD>
																<TD width="115" aline="left"><INPUT id="tb_OutStorehouseQuantityEdit" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																		onkeyup="return Process();" style="WIDTH: 107px; HEIGHT: 22px; TEXT-ALIGN: right" onfocus="OnFocus_Obj(this);"
																		type="text" size="12" value="0" name="tb_OutStorehouseQuantityEdit" runat="server" Width="115" columnKey="OutStorehouseQuantity"></TD>
																<TD align="right" width="100" aline="right"><FONT face="굴림">
																		<asp:Label id="Label8" runat="server" Font-Size="10pt">출고창고</asp:Label></FONT></TD>
																<TD width="115" aline="left">
																	<asp:dropdownlist id="dl_StoreEdit" runat="server" Width="90px" columnkey="BusinessStorehouseNum">
																		<asp:ListItem Value="0">-선택-</asp:ListItem>
																		<asp:ListItem Value="1">영업1창고</asp:ListItem>
																		<asp:ListItem Value="2">영업2창고</asp:ListItem>
																		<asp:ListItem Value="3">영업3창고</asp:ListItem>
																		<asp:ListItem Value="7">보용품창고</asp:ListItem>
																	</asp:dropdownlist></TD>
																<TD align="right" width="100" aline="right">
																	<asp:Label id="Label14" runat="server" Font-Size="10pt">적합수량</asp:Label></TD>
																<TD width="115" aline="left"><INPUT id="tb_SuitabilityQuantityEdit" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																		onkeyup="return Process();" style="WIDTH: 106px; HEIGHT: 22px; TEXT-ALIGN: right" onfocus="OnFocus_Obj(this);"
																		type="text" size="12" value="0" name="tb_SuitabilityQuantityEdit" runat="server" Width="115" columnkey="SuitabilityQuantity"></TD>
																<TD align="right" width="100" aline="right">
																	<asp:Label id="Label1" runat="server" Font-Size="10pt">부적합수량</asp:Label></TD>
																<TD width="115" aline="left"><INPUT id="lb_UnSuitabilityQuantityEdit" style="WIDTH: 130px; HEIGHT: 22px; TEXT-ALIGN: right"
																		readOnly type="text" size="16" value="0" name="lb_UnSuitabilityQuantityEdit" runat="server" Width="115" columnkey="UnSuitabilityQuantity"></TD>
															</TR>
															<TR>
																<TD align="right" width="100" aline="right"><FONT face="굴림">
																		<asp:Label id="Label11" runat="server" Font-Size="10pt">부적합현상</asp:Label></FONT></TD>
																<TD width="115" aline="left">
																	<asp:DropDownList id=dl_UnSuitabilityStatusEdit runat="server" Width="110px" columnkey="UnSuitabilityStatusMeaning" DataSource="<%# UnSuitabilityStatus %>" DataTextField="SmallClassificationName" DataValueField="SmallClassificationCode">
																	</asp:DropDownList></TD>
																<TD align="right" width="100" aline="right">
																	<asp:Label id="Label10" runat="server" Font-Size="10pt">부적합원인</asp:Label></TD>
																<TD width="115" aline="left">
																	<asp:DropDownList id=dl_UnSuitabilityCauseEdit runat="server" Width="110px" columnkey="UnSuitabilityCauseMeaning" DataSource="<%# UnSuitabilityCause %>" DataTextField="SmallClassificationName" DataValueField="SmallClassificationCode">
																	</asp:DropDownList></TD>
																<TD align="right" width="100" aline="right">
																	<asp:Label id="Label13" runat="server" Font-Size="10pt">검사판정</asp:Label></TD>
																<TD width="115" aline="left">
																	<asp:DropDownList id=dl_InspectionDecisionEdit runat="server" Width="110px" columnkey="InspectionDecisionMeaning" DataSource="<%# InspectionDecision %>" DataTextField="SmallClassificationName" DataValueField="SmallClassificationCode">
																	</asp:DropDownList></TD>
																<TD align="right" width="100" aline="right">
																	<asp:Label id="Label12" runat="server" Width="96px" Font-Size="10pt">부적합세부내용</asp:Label></TD>
																<TD width="115" rowSpan="2" aline="left">
																	<asp:TextBox id="tb_UnSuitabilityDetailMeaningEdit" runat="server" Height="47px" Width="130px"
																		columnkey="UnSuitabilityDetailMeaning" TextMode="MultiLine"></asp:TextBox></TD>
															</TR>
															<TR>
																<TD align="right" width="100" aline="right"><FONT face="굴림">출고일자&nbsp; </FONT>
																</TD>
																<TD width="115" aline="left">
																	<igtxt:WebDateTimeEdit id="wdeStartDate" runat="server" Height="20px" Width="110px" BorderStyle="Solid"
																		BorderColor="DimGray" BorderWidth="1px" HorizontalAlign="Center" UseBrowserDefaults="False" DisplayModeFormat="d">
																		<ButtonsAppearance>
																			<ButtonPressedStyle BorderStyle="Inset">
																				<BorderDetails ColorBottom="White" ColorRight="White"></BorderDetails>
																			</ButtonPressedStyle>
																			<ButtonStyle BorderWidth="2px" BorderColor="White" BorderStyle="Outset" BackColor="Control">
																				<BorderDetails StyleBottom="Ridge" ColorBottom="0, 160, 160, 160" ColorRight="0, 160, 160, 160"
																					StyleRight="Ridge"></BorderDetails>
																			</ButtonStyle>
																		</ButtonsAppearance>
																		<SpinButtons Display="OnRight"></SpinButtons>
																		<BorderDetails ColorTop="0, 160, 160, 160" StyleTop="Ridge" StyleLeft="Ridge" ColorLeft="0, 160, 160, 160"></BorderDetails>
																	</igtxt:WebDateTimeEdit></TD>
																<TD align="right" width="100" aline="right"></TD>
																<TD width="115" aline="left"></TD>
																<TD align="right" width="100" aline="right"><FONT face="굴림">Lot 번호 </FONT>
																</TD>
																<TD width="215" colSpan="2" aline="left"><FONT face="굴림"><INPUT id="txtLot" style="WIDTH: 156px; HEIGHT: 22px" type="text" size="20" name="tbLot"
																			runat="server" Width="115" columnKey="LotNum"></FONT></TD>
															</TR>
															<TR>
																<TD align="center" width="860" colSpan="8" aline="center"><INPUT id="igtbl_reOkBtn" style="WIDTH: 60px; HEIGHT: 20px" onclick="igtbl_gRowEditButtonClick(event);__doPostBack('lnk_Update','');"
																		type="button" value="수정">&nbsp; <INPUT id="igtbl_reCancelBtn" style="WIDTH: 60px; HEIGHT: 20px" onclick="igtbl_gRowEditButtonClick(event);"
																		type="button" value="취소"></TD>
															</TR>
														</TABLE>
													</RowEditTemplate>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></td>
								</tr>
								<TR>
									<TD lang="30" width="500"><FONT face="굴림">&nbsp;</FONT><asp:button id="bt_Excel" runat="server" Height="20px" Width="60px" Font-Size="10pt" Text="Excel"></asp:button>
											<INPUT id="lb_Store" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1" runat="server">
											<INPUT id="lb_Quantity" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" size="3" value="0" name="Hidden1" runat="server">
											<INPUT id="lb_UnInspectionQuantity" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" value="0" name="Hidden1" runat="server">
											<INPUT id="lb_SuitabilityQuantity" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" value="0" name="Hidden1" runat="server">
											<INPUT id="lb_UnSuitabilityQuantity" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" value="0" name="Hidden1" runat="server">
											<asp:linkbutton id="lnk_Update" runat="server" Visible="False">LinkButton</asp:linkbutton>
										<INPUT id="hdyear" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1" runat="server"><INPUT id="hdmon" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1" runat="server">
										<igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter></TD>
									<TD lang="30" style="FONT-SIZE: 10pt; COLOR: blue; TEXT-DECORATION: underline" align="right"
										width="300"><asp:button id="bt_Update" runat="server" Height="20px" Width="60px" Font-Size="10pt" Text="수정발행"></asp:button>&nbsp;&nbsp;
										<asp:button id="bt_Delete" runat="server" Height="20px" Width="60px" Font-Size="10pt" Text="삭  제"></asp:button>&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="lb_RowSelectIndex" style="Z-INDEX: 102; LEFT: 856px; WIDTH: 50px; POSITION: absolute; TOP: 496px; HEIGHT: 20px"
				type="hidden" name="Hidden1" runat="server"><INPUT id="lb_RowIndex" style="Z-INDEX: 103; LEFT: 864px; WIDTH: 50px; POSITION: absolute; TOP: 472px; HEIGHT: 20px"
				type="hidden" name="Hidden1" runat="server"><INPUT id="chkAll" style="Z-INDEX: 104; LEFT: 872px; WIDTH: 50px; POSITION: absolute; TOP: 552px; HEIGHT: 20px"
				type="hidden" value="true" name="chkAll" runat="server"></form>
	</body>
</HTML>
