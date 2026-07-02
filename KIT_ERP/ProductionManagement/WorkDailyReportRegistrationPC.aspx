<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="WorkDailyReportRegistrationPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.WorkDailyReportRegistrationPC" codePage="949" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WorkDailyReportRegistrationPC</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--
		
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
		function UltraWebGrid1_ColumnHeaderClickHandler(gridName, columnId, button){
			//Add code to handle your event here.
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
		
		function Process()	
		{
			var Remain = Number(document.Form1.UltraWebGrid1__ctl0_tb_WorkPlanQuantity.value) - Number(document.Form1.UltraWebGrid1__ctl0_tb_WorkCompletionQuantity.value);
			
			if(Remain < 0 )	
			{
				if(confirm("전체 완료수량이 계획수량보다 많습니다!! 그래도 등록하시겠습니까?")) 
				{
						return true;
				}
				else
				{
					//alert("- 완료수량이 계획수량보다 많습니다!");
					document.Form1.UltraWebGrid1__ctl0_tb_WorkCompletionQuantity.value = document.Form1.UltraWebGrid1__ctl0_tb_WorkPlanQuantity.value;
					document.Form1.UltraWebGrid1__ctl0_tb_WorkCompletionQuantity.focus();
					return false;
				}			
			}
			
		}
		function Suitability()
		{
			var Quantity = Number(document.Form1.UltraWebGrid1__ctl0_tb_WorkCompletionQuantity.value) - Number(document.Form1.UltraWebGrid1__ctl0_tb_SuitabilityQuantity.value);
			if(Quantity < 0)
			{
				alert("- 적합수량이 완료수량보다 많습니다!");
				document.Form1.UltraWebGrid1__ctl0_tb_SuitabilityQuantity.value = 0;
				document.Form1.UltraWebGrid1__ctl0_tb_SuitabilityQuantity.focus();
			}
			else
			{
				document.Form1.UltraWebGrid1__ctl0_tb_UnSuitabilityQuantity.value = Quantity;				
			}
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
		
		//수정창 닫히기전
		function UltraWebGrid1_AfterRowTemplateCloseHandler(gridName, rowId, bSaveChanges){
			//Add code to handle your event here.
			if(event.srcElement.id == "igtbl_reOkBtn")
			{
				var frm = document.Form1;;
				var row = igtbl_getRowById(rowId);
				
				if(frm.UltraWebGrid1__ctl0_EtcNum1.value == "")
					row.getCellFromKey("EtcNum1").setValue("");
				else
					row.getCellFromKey("EtcNum1").setValue(frm.UltraWebGrid1__ctl0_EtcNum1.value);
				
				if(frm.UltraWebGrid1__ctl0_EtcNum2.value == "")
					row.getCellFromKey("EtcNum2").setValue("");
				else
					row.getCellFromKey("EtcNum2").setValue(frm.UltraWebGrid1__ctl0_EtcNum2.value);
				
				//부적합현상
				if(frm.UltraWebGrid1__ctl0_dl_UnSuitabilityStatusMeaning.options[frm.UltraWebGrid1__ctl0_dl_UnSuitabilityStatusMeaning.selectedIndex].value == "")
				{
					row.getCellFromKey("UnSuitabilityStatusCode").setValue("");
					row.getCellFromKey("UnSuitabilityStatusMeaning").setValue("");
				}
				else
				{
					row.getCellFromKey("UnSuitabilityStatusCode").setValue(frm.UltraWebGrid1__ctl0_dl_UnSuitabilityStatusMeaning.options[frm.UltraWebGrid1__ctl0_dl_UnSuitabilityStatusMeaning.selectedIndex].value);
					row.getCellFromKey("UnSuitabilityStatusMeaning").setValue(frm.UltraWebGrid1__ctl0_dl_UnSuitabilityStatusMeaning.options[frm.UltraWebGrid1__ctl0_dl_UnSuitabilityStatusMeaning.selectedIndex].innerText);
				}
				
				//부적합원인
				if(frm.UltraWebGrid1__ctl0_dl_UnSuitabilityCauseMeaning.options[frm.UltraWebGrid1__ctl0_dl_UnSuitabilityCauseMeaning.selectedIndex].value == "")
				{
					row.getCellFromKey("UnSuitabilityCauseCode").setValue("");
					row.getCellFromKey("UnSuitabilityCauseMeaning").setValue("");
				}
				else
				{
					row.getCellFromKey("UnSuitabilityCauseCode").setValue(frm.UltraWebGrid1__ctl0_dl_UnSuitabilityCauseMeaning.options[frm.UltraWebGrid1__ctl0_dl_UnSuitabilityCauseMeaning.selectedIndex].value);
					row.getCellFromKey("UnSuitabilityCauseMeaning").setValue(frm.UltraWebGrid1__ctl0_dl_UnSuitabilityCauseMeaning.options[frm.UltraWebGrid1__ctl0_dl_UnSuitabilityCauseMeaning.selectedIndex].innerText);
				}
				
				//검사판정
				if(frm.UltraWebGrid1__ctl0_dl_InspectionDecision.options[frm.UltraWebGrid1__ctl0_dl_InspectionDecision.selectedIndex].value == "")
				{
					row.getCellFromKey("InspectionDecisionCode").setValue("");
					row.getCellFromKey("InspectionDecision").setValue("");
				}
				else
				{
					row.getCellFromKey("InspectionDecisionCode").setValue(frm.UltraWebGrid1__ctl0_dl_InspectionDecision.options[frm.UltraWebGrid1__ctl0_dl_InspectionDecision.selectedIndex].value);
					row.getCellFromKey("InspectionDecision").setValue(frm.UltraWebGrid1__ctl0_dl_InspectionDecision.options[frm.UltraWebGrid1__ctl0_dl_InspectionDecision.selectedIndex].innerText);
				}
				
				//비작업사유1
				if(frm.UltraWebGrid1__ctl0_dl_NonWorkTimeReason1.options[frm.UltraWebGrid1__ctl0_dl_NonWorkTimeReason1.selectedIndex].value == "")
				{
					row.getCellFromKey("NonWorkTimeCode1").setValue("");
					row.getCellFromKey("NonWorkTimeReason1").setValue("");
				}
				else
				{
					row.getCellFromKey("NonWorkTimeCode1").setValue(frm.UltraWebGrid1__ctl0_dl_NonWorkTimeReason1.options[frm.UltraWebGrid1__ctl0_dl_NonWorkTimeReason1.selectedIndex].value);
					row.getCellFromKey("NonWorkTimeReason1").setValue(frm.UltraWebGrid1__ctl0_dl_NonWorkTimeReason1.options[frm.UltraWebGrid1__ctl0_dl_NonWorkTimeReason1.selectedIndex].innerText);
				}
				//비작업사유2
				if(frm.UltraWebGrid1__ctl0_dl_NonWorkTimeReason2.options[frm.UltraWebGrid1__ctl0_dl_NonWorkTimeReason2.selectedIndex].value == "")
				{
					row.getCellFromKey("NonWorkTimeCode2").setValue("");
					row.getCellFromKey("NonWorkTimeReason2").setValue("");
				}
				else
				{
					row.getCellFromKey("NonWorkTimeCode2").setValue(frm.UltraWebGrid1__ctl0_dl_NonWorkTimeReason2.options[frm.UltraWebGrid1__ctl0_dl_NonWorkTimeReason2.selectedIndex].value);
					row.getCellFromKey("NonWorkTimeReason2").setValue(frm.UltraWebGrid1__ctl0_dl_NonWorkTimeReason2.options[frm.UltraWebGrid1__ctl0_dl_NonWorkTimeReason2.selectedIndex].innerText);
				}
				//비작업사유3
				if(frm.UltraWebGrid1__ctl0_dl_NonWorkTimeReason3.options[frm.UltraWebGrid1__ctl0_dl_NonWorkTimeReason3.selectedIndex].value == "")
				{
					row.getCellFromKey("NonWorkTimeCode3").setValue("");
					row.getCellFromKey("NonWorkTimeReason3").setValue("");
				}
				else
				{
					row.getCellFromKey("NonWorkTimeCode3").setValue(frm.UltraWebGrid1__ctl0_dl_NonWorkTimeReason3.options[frm.UltraWebGrid1__ctl0_dl_NonWorkTimeReason3.selectedIndex].value);
					row.getCellFromKey("NonWorkTimeReason3").setValue(frm.UltraWebGrid1__ctl0_dl_NonWorkTimeReason3.options[frm.UltraWebGrid1__ctl0_dl_NonWorkTimeReason3.selectedIndex].innerText);
				}
				
				//사용공구1
				if(frm.UltraWebGrid1__ctl0_dl_UseTool1.options[frm.UltraWebGrid1__ctl0_dl_UseTool1.selectedIndex].value == "")
				{
					row.getCellFromKey("UseTool1").setValue("");
				}
				else
				{
					row.getCellFromKey("UseTool1").setValue(frm.UltraWebGrid1__ctl0_dl_UseTool1.options[frm.UltraWebGrid1__ctl0_dl_UseTool1.selectedIndex].value);
				}
				//사용치구1
				if(frm.UltraWebGrid1__ctl0_dl_UseJig1.options[frm.UltraWebGrid1__ctl0_dl_UseJig1.selectedIndex].value == "")
				{
					row.getCellFromKey("UseJig1").setValue("");
				}
				else
				{
					row.getCellFromKey("UseJig1").setValue(frm.UltraWebGrid1__ctl0_dl_UseJig1.options[frm.UltraWebGrid1__ctl0_dl_UseJig1.selectedIndex].value);
				}
				
				//사용공구2
				if(frm.UltraWebGrid1__ctl0_dl_UseTool2.options[frm.UltraWebGrid1__ctl0_dl_UseTool2.selectedIndex].value == "")
				{
					row.getCellFromKey("UseTool2").setValue("");
				}
				else
				{
					row.getCellFromKey("UseTool2").setValue(frm.UltraWebGrid1__ctl0_dl_UseTool2.options[frm.UltraWebGrid1__ctl0_dl_UseTool2.selectedIndex].value);
				}
				//사용치구2
				if(frm.UltraWebGrid1__ctl0_dl_UseJig2.options[frm.UltraWebGrid1__ctl0_dl_UseJig2.selectedIndex].value == "")
				{
					row.getCellFromKey("UseJig2").setValue("");
				}
				else
				{
					row.getCellFromKey("UseJig2").setValue(frm.UltraWebGrid1__ctl0_dl_UseJig2.options[frm.UltraWebGrid1__ctl0_dl_UseJig2.selectedIndex].value);
				}
				
				//사용공구3
				if(frm.UltraWebGrid1__ctl0_dl_UseTool3.options[frm.UltraWebGrid1__ctl0_dl_UseTool3.selectedIndex].value == "")
				{
					row.getCellFromKey("UseTool3").setValue("");
				}
				else
				{
					row.getCellFromKey("UseTool3").setValue(frm.UltraWebGrid1__ctl0_dl_UseTool3.options[frm.UltraWebGrid1__ctl0_dl_UseTool3.selectedIndex].value);
				}
				//사용치구3
				if(frm.UltraWebGrid1__ctl0_dl_UseJig3.options[frm.UltraWebGrid1__ctl0_dl_UseJig3.selectedIndex].value == "")
				{
					row.getCellFromKey("UseJig3").setValue("");
				}
				else
				{
					row.getCellFromKey("UseJig3").setValue(frm.UltraWebGrid1__ctl0_dl_UseJig3.options[frm.UltraWebGrid1__ctl0_dl_UseJig3.selectedIndex].value);
				}
				
				row.getCellFromKey("Worker").setValue(frm.UltraWebGrid1__ctl0_dl_Worker.options[frm.UltraWebGrid1__ctl0_dl_Worker.selectedIndex].innerText);
				row.getCellFromKey("WorkerID").setValue(frm.UltraWebGrid1__ctl0_dl_Worker.options[frm.UltraWebGrid1__ctl0_dl_Worker.selectedIndex].value);
				
							
				//작업시작시간
				var begin = String(igdrp_getComboById("UltraWebGrid1xxctl0xwdcBeginTime").getValue().getYear() + "-" +(igdrp_getComboById("UltraWebGrid1xxctl0xwdcBeginTime").getValue().getMonth()+1) + "-" + igdrp_getComboById("UltraWebGrid1xxctl0xwdcBeginTime").getValue().getDate());
				document.Form1.BeginTime.value = begin +" "+String(document.Form1.UltraWebGrid1__ctl0_TextBox1.value)+":"+String(document.Form1.UltraWebGrid1__ctl0_TextBox2.value);
				
				//작업종료시간
				var end = String(igdrp_getComboById("UltraWebGrid1xxctl0xwdcEndTime").getValue().getYear() + "-" +(igdrp_getComboById("UltraWebGrid1xxctl0xwdcEndTime").getValue().getMonth()+1) + "-" + igdrp_getComboById("UltraWebGrid1xxctl0xwdcEndTime").getValue().getDate());
				document.Form1.EndTime.value = end +" "+String(document.Form1.UltraWebGrid1__ctl0_TextBox3.value)+":"+String(document.Form1.UltraWebGrid1__ctl0_TextBox4.value);
			}
			else
			{
				//alert(igdrp_getComboById("UltraWebGrid1xxctl0xwdcBeginTime").getValue().getYear() + "-" +(igdrp_getComboById("UltraWebGrid1xxctl0xwdcBeginTime").getValue().getMonth()+1) + "-" + igdrp_getComboById("UltraWebGrid1xxctl0xwdcBeginTime").getValue().getDate());
			}
		}
		
		//수정창열기전
		function UltraWebGrid1_AfterRowTemplateOpenHandler(gridName, rowId){
			//Add code to handle your event here.
			var row = igtbl_getRowById(rowId);
			document.Form1.UltraWebGrid1__ctl0_TextBox1.value = (row.getCellFromKey("WorkBeginTime").getValue()).getHours();
			if(document.Form1.UltraWebGrid1__ctl0_TextBox1.value.length == 1)
				document.Form1.UltraWebGrid1__ctl0_TextBox1.value = String(0)+String(document.Form1.UltraWebGrid1__ctl0_TextBox1.value)
			
			document.Form1.UltraWebGrid1__ctl0_TextBox2.value = (row.getCellFromKey("WorkBeginTime").getValue()).getMinutes();
			if(document.Form1.UltraWebGrid1__ctl0_TextBox2.value.length == 1)
				document.Form1.UltraWebGrid1__ctl0_TextBox2.value = String(0)+String(document.Form1.UltraWebGrid1__ctl0_TextBox2.value)
			
			document.Form1.UltraWebGrid1__ctl0_TextBox3.value = (row.getCellFromKey("WorkEndTime").getValue()).getHours();
			if(document.Form1.UltraWebGrid1__ctl0_TextBox3.value.length == 1)
				document.Form1.UltraWebGrid1__ctl0_TextBox3.value = String(0)+String(document.Form1.UltraWebGrid1__ctl0_TextBox3.value)
				
			document.Form1.UltraWebGrid1__ctl0_TextBox4.value = (row.getCellFromKey("WorkEndTime").getValue()).getMinutes();
			if(document.Form1.UltraWebGrid1__ctl0_TextBox4.value.length == 1)
				document.Form1.UltraWebGrid1__ctl0_TextBox4.value = String(0)+String(document.Form1.UltraWebGrid1__ctl0_TextBox4.value)
			
			var date = igdrp_getComboById("UltraWebGrid1xxctl0xwdcBeginTime");
			var date1 = igdrp_getComboById("UltraWebGrid1xxctl0xwdcEndTime");
			date.setValue(row.getCellFromKey("WorkBeginTime").getValue());
			date1.setValue(row.getCellFromKey("WorkEndTime").getValue());
			
			document.Form1.hdYear.value = date1.getValue().getFullYear();
			document.Form1.hdMon.value = date1.getValue().getMonth()+1;
			
					
			document.Form1.lb_RowSelectIndex.value = document.Form1.lb_RowIndex.value;
			
			// 이전수량을 저장해둔다.
			document.Form1.lb_PreWorkCompletionQuantity.value = row.getCellFromKey("ThisWorkCompletionQuantity").getValue();//금번작업완료수량
			document.Form1.lb_PreRemainQuantity.value = row.getCellFromKey("RemainQuantity").getValue();//잔량
			document.Form1.lb_PreSuitabilityQuantity.value = row.getCellFromKey("SuitabilityQuantity").getValue();//적합수량
			document.Form1.lb_PreUnSuitabilityQuantity.value = row.getCellFromKey("UnSuitabilityQuantity").getValue();//부적합수량
			
			document.Form1.lb_UseTool1.value = row.getCellFromKey("UseTool1").getValue();//사용공구1
			document.Form1.lb_UseTool2.value = row.getCellFromKey("UseTool2").getValue();//사용공구2
			document.Form1.lb_UseTool3.value = row.getCellFromKey("UseTool3").getValue();//사용공구3
			document.Form1.lb_UseJig1.value = row.getCellFromKey("UseJig1").getValue();//사용치구1
			document.Form1.lb_UseJig2.value = row.getCellFromKey("UseJig2").getValue();//사용치구2
			document.Form1.lb_UseJig3.value = row.getCellFromKey("UseJig3").getValue();//사용치구3
			
			var SuitabilityQuantity = row.getCellFromKey("SuitabilityQuantity").getValue();//합격수량
			if(SuitabilityQuantity == null)
				document.Form1.UltraWebGrid1__ctl0_tb_SuitabilityQuantity.value = 0;
			else
				document.Form1.UltraWebGrid1__ctl0_tb_SuitabilityQuantity.value = SuitabilityQuantity;
			
			var UnSuitabilityQuantity = row.getCellFromKey("UnSuitabilityQuantity").getValue();//부적합수량
			if(UnSuitabilityQuantity == null)
				document.Form1.UltraWebGrid1__ctl0_tb_UnSuitabilityQuantity.value = 0;
			else
				document.Form1.UltraWebGrid1__ctl0_tb_UnSuitabilityQuantity.value = UnSuitabilityQuantity;
			
			if(row.getCellFromKey("EtcNum1").getValue() != null)
						document.Form1.UltraWebGrid1__ctl0_EtcNum1.value = row.getCellFromKey("EtcNum1").getValue();
			if(row.getCellFromKey("EtcNum2").getValue() != null)
						document.Form1.UltraWebGrid1__ctl0_EtcNum2.value = row.getCellFromKey("EtcNum2").getValue();
			
			var objGrid = igtbl_getActiveRow(gridName)//현재 선택되어진 객체의 행의 가져옴
			var row = igtbl_getRowById(rowId);
			
			
			//작업장
			for(var i=0;i<Form1.UltraWebGrid1__ctl0_dl_WCName.options.length;i++)
			{
				if(Form1.UltraWebGrid1__ctl0_dl_WCName.options[i].value == row.getCellFromKey("WCName").getValue())
				{
					Form1.UltraWebGrid1__ctl0_dl_WCName.options[i].selected=true;
					break;
				}
				else
					Form1.UltraWebGrid1__ctl0_dl_WCName.options[0].selected=true;
			}
			
			//사용공구1
			for(var i=0;i<Form1.UltraWebGrid1__ctl0_dl_UseTool1.options.length;i++)
			{
				if(Form1.UltraWebGrid1__ctl0_dl_UseTool1.options[i].value == row.getCellFromKey("UseTool1").getValue())
				{
					Form1.UltraWebGrid1__ctl0_dl_UseTool1.options[i].selected=true;
					break;
				}
				else
					Form1.UltraWebGrid1__ctl0_dl_UseTool1.options[0].selected=true;
			}
			//사용치구1
			for(var i=0;i<Form1.UltraWebGrid1__ctl0_dl_UseJig1.options.length;i++)
			{
				if(Form1.UltraWebGrid1__ctl0_dl_UseJig1.options[i].value == row.getCellFromKey("UseJig1").getValue())
				{
					Form1.UltraWebGrid1__ctl0_dl_UseJig1.options[i].selected=true;
					break;
				}
				else
					Form1.UltraWebGrid1__ctl0_dl_UseJig1.options[0].selected=true;
			}
			
			//사용공구2
			for(var i=0;i<Form1.UltraWebGrid1__ctl0_dl_UseTool2.options.length;i++)
			{
				if(Form1.UltraWebGrid1__ctl0_dl_UseTool2.options[i].value == row.getCellFromKey("UseTool2").getValue())
				{
					Form1.UltraWebGrid1__ctl0_dl_UseTool2.options[i].selected=true;
					break;
				}
				else
					Form1.UltraWebGrid1__ctl0_dl_UseTool2.options[0].selected=true;
			}
			//사용치구2
			for(var i=0;i<Form1.UltraWebGrid1__ctl0_dl_UseJig2.options.length;i++)
			{
				if(Form1.UltraWebGrid1__ctl0_dl_UseJig2.options[i].value == row.getCellFromKey("UseJig2").getValue())
				{
					Form1.UltraWebGrid1__ctl0_dl_UseJig2.options[i].selected=true;
					break;
				}
				else
					Form1.UltraWebGrid1__ctl0_dl_UseJig2.options[0].selected=true;
			}
			
			//사용공구3
			for(var i=0;i<Form1.UltraWebGrid1__ctl0_dl_UseTool3.options.length;i++)
			{
				if(Form1.UltraWebGrid1__ctl0_dl_UseTool3.options[i].value == row.getCellFromKey("UseTool3").getValue())
				{
					Form1.UltraWebGrid1__ctl0_dl_UseTool3.options[i].selected=true;
					break;
				}
				else
					Form1.UltraWebGrid1__ctl0_dl_UseTool3.options[0].selected=true;
			}
			//사용치구3
			for(var i=0;i<Form1.UltraWebGrid1__ctl0_dl_UseJig3.options.length;i++)
			{
				if(Form1.UltraWebGrid1__ctl0_dl_UseJig3.options[i].value == row.getCellFromKey("UseJig3").getValue())
				{
					Form1.UltraWebGrid1__ctl0_dl_UseJig3.options[i].selected=true;
					break;
				}
				else
					Form1.UltraWebGrid1__ctl0_dl_UseJig3.options[0].selected=true;
			}
			
			
			//비작업사유1
			for(var i=0;i<Form1.UltraWebGrid1__ctl0_dl_NonWorkTimeReason1.options.length;i++)
			{
				if(Form1.UltraWebGrid1__ctl0_dl_NonWorkTimeReason1.options[i].innerText == row.getCellFromKey("NonWorkTimeReason1").getValue())
				{
					Form1.UltraWebGrid1__ctl0_dl_NonWorkTimeReason1.options[i].selected=true;
					break;
				}
				else
					Form1.UltraWebGrid1__ctl0_dl_NonWorkTimeReason1.options[0].selected=true;
			}
			//비작업사유2
			for(var i=0;i<Form1.UltraWebGrid1__ctl0_dl_NonWorkTimeReason2.options.length;i++)
			{
				if(Form1.UltraWebGrid1__ctl0_dl_NonWorkTimeReason2.options[i].innerText == row.getCellFromKey("NonWorkTimeReason2").getValue())
				{
					Form1.UltraWebGrid1__ctl0_dl_NonWorkTimeReason2.options[i].selected=true;
					break;
				}
				else
					Form1.UltraWebGrid1__ctl0_dl_NonWorkTimeReason2.options[0].selected=true;
			}
			//비작업사유3
			for(var i=0;i<Form1.UltraWebGrid1__ctl0_dl_NonWorkTimeReason3.options.length;i++)
			{
				if(Form1.UltraWebGrid1__ctl0_dl_NonWorkTimeReason3.options[i].innerText == row.getCellFromKey("NonWorkTimeReason3").getValue())
				{
					Form1.UltraWebGrid1__ctl0_dl_NonWorkTimeReason3.options[i].selected=true;
					break;
				}
				else
					Form1.UltraWebGrid1__ctl0_dl_NonWorkTimeReason3.options[0].selected=true;
			}
			
			//부적합현상
			for(var i=0;i<Form1.UltraWebGrid1__ctl0_dl_UnSuitabilityStatusMeaning.options.length;i++)
			{
				if(Form1.UltraWebGrid1__ctl0_dl_UnSuitabilityStatusMeaning.options[i].innerText == row.getCellFromKey("UnSuitabilityStatusMeaning").getValue())
				{
					Form1.UltraWebGrid1__ctl0_dl_UnSuitabilityStatusMeaning.options[i].selected=true;
					break;
				}
				else
					Form1.UltraWebGrid1__ctl0_dl_UnSuitabilityStatusMeaning.options[0].selected=true;
			}
			//부적합원인
			for(var j=0;j<Form1.UltraWebGrid1__ctl0_dl_UnSuitabilityCauseMeaning.options.length;j++)
			{
				if(Form1.UltraWebGrid1__ctl0_dl_UnSuitabilityCauseMeaning.options[j].innerText == row.getCellFromKey("UnSuitabilityCauseMeaning").getValue())
				{
					Form1.UltraWebGrid1__ctl0_dl_UnSuitabilityCauseMeaning.options[j].selected=true;
					break;
				}
				else
					Form1.UltraWebGrid1__ctl0_dl_UnSuitabilityCauseMeaning.options[0].selected=true;
			}
			//검사판정
			for(var k=0;k<Form1.UltraWebGrid1__ctl0_dl_InspectionDecision.options.length;k++)
			{
				if(Form1.UltraWebGrid1__ctl0_dl_InspectionDecision.options[k].innerText == row.getCellFromKey("InspectionDecision").getValue())
				{
					Form1.UltraWebGrid1__ctl0_dl_InspectionDecision.options[k].selected=true;
					break;
				}
				else
					Form1.UltraWebGrid1__ctl0_dl_InspectionDecision.options[0].selected=true;					
			}
			
			//작업자
			for(var l=0;l<Form1.UltraWebGrid1__ctl0_dl_Worker.options.length;l++)
			{
				if(Form1.UltraWebGrid1__ctl0_dl_Worker.options[l].value == row.getCellFromKey("WorkerID").getValue())
				{
					Form1.UltraWebGrid1__ctl0_dl_Worker.options[l].selected=true;
					break;
				}
				else
					Form1.UltraWebGrid1__ctl0_dl_Worker.options[0].selected=true;					
			}
			
		}
		
		
		function UltraWebGrid1_AfterSelectChangeHandler(gridName, id){
			
				var cell = igtbl_getElementById(id);
				cell.style.cursor = 'hand';
				var parts = id.split("_");
				document.Form1.lb_RowIndex.value = parts[1];
		}
		
		
		
		function Process1()	
		{
			
			if(document.Form1.UltraWebGrid1__ctl0_TextBox1.value >= 24)
			{
				alert('0-23까지만 가능합니다!');
				document.Form1.UltraWebGrid1__ctl0_TextBox1.value = 0;
				document.Form1.UltraWebGrid1__ctl0_TextBox1.focus();
			}
				
		}
		
		function Process2()	
		{
			
			if(document.Form1.UltraWebGrid1__ctl0_TextBox2.value >= 60)
			{
				alert('0-59까지만 가능합니다!');
				document.Form1.UltraWebGrid1__ctl0_TextBox2.value = 0;
				document.Form1.UltraWebGrid1__ctl0_TextBox2.focus();
			}
				
		}
		
		function Process3()	
		{
			
			if(document.Form1.UltraWebGrid1__ctl0_TextBox3.value >= 24)
			{
				alert('0-23까지만 가능합니다!');
				document.Form1.UltraWebGrid1__ctl0_TextBox3.value = 0;
				document.Form1.UltraWebGrid1__ctl0_TextBox3.focus();
			}
				
		}
		
		function Process4()	
		{
			
			if(document.Form1.UltraWebGrid1__ctl0_TextBox4.value >= 60)
			{
				alert('0-59까지만 가능합니다!');
				document.Form1.UltraWebGrid1__ctl0_TextBox4.value = 0;
				document.Form1.UltraWebGrid1__ctl0_TextBox4.focus();
			}
				
		}
		
		function OnlyNum(obj)
		{
			if (event.ctrlKey || event.shiftKey || event.altKey)   {
				SetEventPass();
				return true;
			}
			var sFieldValue = new String(obj.value);
			// 숫자검사 + Numeric Keypad 숫자검사
			if( (event.keyCode<45 || event.keyCode>57) && (event.keyCode<96 || event.keyCode>105))  {
    			if( !IsNumKey() ) {
	    			SetEventCancel();
					return obj.value;
				}	    
			}
			else if( sFieldValue.substr(0,2) == "00" && (event.keyCode == 48 || event.keyCode == 96) ) {
						SetEventCancel();
						return obj.value;
					}
			SetEventPass();
			return true;
		}
		
		function IsNumKey()  {
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
				event.keyCode == 109 
				)
    			return true;
			else
    			return false;
		}
		
--></SCRIPT>
	</HEAD>
	<body bgColor="#f7f6f6" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="LEFT: 10px; POSITION: absolute; TOP: 10px" cellSpacing="0" cellPadding="0"
				width="800" border="0">
				<TR>
					<TD></TD>
					<TD vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD vAlign="middle" align="left" colSpan="6" height="30"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></TD>
									<TD vAlign="middle" align="right" width="70" height="30">&nbsp;작업자&nbsp;</TD>
									<TD vAlign="middle" align="left" width="130" height="30"><asp:dropdownlist id="ddlWorker" runat="server" BackColor="#EEEEE9" Width="100px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" width="70" height="30">작업일&nbsp;</TD>
									<TD vAlign="middle" align="left" width="100" height="30"><igsch:webdatechooser id="wdc_FromDate" runat="server" BackColor="#EEEEE9" Width="100px" Font-Size="10pt"
											Height="18px" NullDateLabel=" " Text=" " BorderColor="DimGray" BorderStyle="Solid">
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
									<TD vAlign="middle" align="center" width="15" height="30">~</TD>
									<TD vAlign="middle" align="right" width="100" height="30"><igsch:webdatechooser id="wdc_ToDate" runat="server" BackColor="#EEEEE9" Width="100px" Font-Size="10pt"
											Height="18px" NullDateLabel=" " Text=" " BorderColor="DimGray" BorderStyle="Solid">
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
									<TD vAlign="middle" align="right" width="70" height="30">WC명&nbsp;</TD>
									<TD vAlign="middle" align="left" width="245" height="30"><igcmbo:webcombo id="wc_WCName" runat="server" BackColor="#EEEEE9" Width="124px" Font-Size="10pt"
											Height="18px" BorderColor="DimGray" BorderStyle="Solid" ForeColor="Black" BorderWidth="1px" SelBackColor="10, 36, 106" SelectedIndex="-1" SelForeColor="White"
											Version="3.00" DropImageXP1="/ig_common/WebGrid3/ig_cmboDown1.bmp" DropImageXP2="/ig_common/WebGrid3/ig_cmboDown2.bmp">
											<Columns>
												<igtbl:UltraGridColumn HeaderText="작업장명" Key="WCName" Width="180px" BaseColumnName="WCName"></igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="번호" Key="WCInfoIndex" Hidden="True" BaseColumnName="WCInfoIndex"></igtbl:UltraGridColumn>
											</Columns>
											<DropDownLayout DropdownWidth="200px" RowSelectors="No" AllowColSizing="Free" RowHeightDefault="20px"
												AutoGenerateColumns="False" DropdownHeight="300px" TableLayout="Fixed" StationaryMargins="Header">
												<RowStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid" BackColor="White">
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyle>
												<SelectedRowStyle ForeColor="White" BackColor="#0A246A"></SelectedRowStyle>
												<HeaderStyle BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyle>
												<FrameStyle Width="100%" Cursor="Default" Font-Size="10pt" Font-Names="Verdana" BackColor="Silver"
													Height="100%"></FrameStyle>
											</DropDownLayout>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igcmbo:webcombo></TD>
									<TD vAlign="middle" align="right" width="200" colSpan="2" height="30"><asp:button id="bt_Clear" runat="server" Width="60px" Font-Size="9pt" Height="20px" Text="초기화"></asp:button>&nbsp;
										<asp:button id="bt_Search" runat="server" Width="60px" Font-Size="9pt" Height="20px" Text="검   색"></asp:button>&nbsp;&nbsp;
									</TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" colSpan="8" height="8"></TD>
								</TR>
							</TABLE>
						</FIELDSET>
						&nbsp;&nbsp;&nbsp;
						<BR>
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[검색결과]</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD vAlign="top" align="center" colSpan="3"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="100%" Height="405px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect"
												AllowUpdateDefault="RowTemplateOnly">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</AddNewBox>
												<Pager PageSize="17" StyleMode="ComboBox" AllowPaging="True">
													<Style VerticalAlign="Middle" BorderWidth="1px" BorderStyle="Solid" HorizontalAlign="Right"
														BackColor="LightGray">
													</Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" BorderColor="Black" BorderStyle="Solid" ForeColor="Black" BackColor="LightGray"
													Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<GroupByRowStyleDefault ForeColor="Black" BackColor="#95B9F0"></GroupByRowStyleDefault>
												<RowSelectorStyleDefault Cursor="Hand" BorderStyle="Solid"></RowSelectorStyleDefault>
												<FrameStyle Width="100%" Cursor="Default" BorderWidth="1px" Font-Size="10pt" Font-Names="굴림"
													BorderColor="DimGray" BorderStyle="Solid" BackColor="Silver" Height="405px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents ColumnHeaderClickHandler="UltraWebGrid1_ColumnHeaderClickHandler" AfterSelectChangeHandler="UltraWebGrid1_AfterSelectChangeHandler"
													AfterRowTemplateOpenHandler="UltraWebGrid1_AfterRowTemplateOpenHandler" AfterRowTemplateCloseHandler="UltraWebGrid1_AfterRowTemplateCloseHandler"></ClientSideEvents>
												<GroupByBox>
													<Style BackColor="#B5CFF7">
													</Style>
													<BandLabelStyle ForeColor="White" BackColor="#6372D4"></BandLabelStyle>
												</GroupByBox>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedGroupByRowStyleDefault BorderColor="Yellow" BackColor="Yellow"></SelectedGroupByRowStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" Font-Bold="True" ForeColor="Linen" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" ForeColor="Black"
													BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails ColorTop="Gray" WidthLeft="0px" WidthTop="0px" ColorLeft="Gray"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="선택" Key="chk" Width="35px" Type="CheckBox" HeaderClickAction="Select"
															BaseColumnName="chk" AllowUpdate="Yes">
															<CellButtonStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" BaseColumnName="ItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left">
																<Padding Left="2px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" BaseColumnName="ItemDrawNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" BaseColumnName="ItemName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left">
																<Padding Left="2px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업장명" Key="WCName" BaseColumnName="WCName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업자ID" Key="WorkerID" Hidden="True" BaseColumnName="WorkerID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업자" Key="Worker" BaseColumnName="Worker">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정명" Key="ProcessName" BaseColumnName="ProcessName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정코드" Key="ProcessCode" Hidden="True" BaseColumnName="ProcessCode">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="Lot 번호" Key="LotNum" Hidden="True" BaseColumnName="LotNum">
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="금번완료수량" Key="ThisWorkCompletionQuantity" Format="###,###,###" BaseColumnName="ThisWorkCompletionQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="적합수량" Key="SuitabilityQuantity" Format="###,###,###" BaseColumnName="SuitabilityQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합수량" Key="UnSuitabilityQuantity" Format="###,###,###" BaseColumnName="UnSuitabilityQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업시작시간" Key="WorkBeginTime" Format="yyyy-MM-dd hh:mm tt" BaseColumnName="WorkBeginTime">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업종료시간" Key="WorkEndTime" Format="yyyy-MM-dd hh:mm tt" BaseColumnName="WorkEndTime">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정순서번호" Key="ProcessSequenceNum" Hidden="True" BaseColumnName="ProcessSequenceNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합현상" Key="UnSuitabilityStatusMeaning" BaseColumnName="UnSuitabilityStatusMeaning">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합원인" Key="UnSuitabilityCauseMeaning" BaseColumnName="UnSuitabilityCauseMeaning">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합금액" Key="UnSuitabilityCost" Format="###,###,###.##" BaseColumnName="UnSuitabilityCost">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="검사판정" Key="InspectionDecision" BaseColumnName="InspectionDecision">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합세부사유" Key="UnSuitabilityDetailMeaning" BaseColumnName="UnSuitabilityDetailMeaning">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="비작업시간1" Key="NonWorkTime1" BaseColumnName="NonWorkTime1">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="비작업사유1" Key="NonWorkTimeReason1" BaseColumnName="NonWorkTimeReason1">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="제품명" Key="ProductName" Hidden="True" BaseColumnName="ProductName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="모품목번호" Key="ParentItemNum" Hidden="True" BaseColumnName="ParentItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="모품목도면번호" Key="ParentDrawNum" Hidden="True" BaseColumnName="ParentDrawNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="모품목명" Key="ParentName" Hidden="True" BaseColumnName="ParentName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="WC번호" Key="WCInfoIndex" Hidden="True" BaseColumnName="WCInfoIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업계획수량" Key="WorkPlanQuantity" Hidden="True" Format="###,###,###" BaseColumnName="WorkPlanQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="이전완료수량" Key="WorkCompletionQuantity" Hidden="True" Format="###,###,###"
															BaseColumnName="WorkCompletionQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="잔량" Key="RemainQuantity" Hidden="True" BaseColumnName="RemainQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="비작업사유코드1" Key="NonWorkTimeCode1" Hidden="True" BaseColumnName="NonWorkTimeCode1">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="제품번호" Key="ProductItemNum" Hidden="True" BaseColumnName="ProductItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="비작업시간2" Key="NonWorkTime2" BaseColumnName="NonWorkTime2">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="비작업사유코드2" Key="NonWorkTimeCode2" Hidden="True" BaseColumnName="NonWorkTimeCode2">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="비작업사유2" Key="NonWorkTimeReason2" BaseColumnName="NonWorkTimeReason2">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="비작업시간3" Key="NonWorkTime3" BaseColumnName="NonWorkTime3">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="비작업사유코드3" Key="NonWorkTimeCode3" Hidden="True" BaseColumnName="NonWorkTimeCode3">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="비작업사유3" Key="NonWorkTimeReason3" BaseColumnName="NonWorkTimeReason3">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합현상코드" Key="UnSuitabilityStatusCode" Hidden="True" BaseColumnName="UnSuitabilityStatusCode">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부적합원인코드" Key="UnSuitabilityCauseCode" Hidden="True" BaseColumnName="UnSuitabilityCauseCode">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="검사판정코드" Key="InspectionDecisionCode" Hidden="True" BaseColumnName="InspectionDecisionCode">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="기대번호(From)" Key="EtcNum1" BaseColumnName="EtcNum1"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="기대번호(To)" Key="EtcNum2" BaseColumnName="EtcNum2"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사용공구1" Key="UseTool1" BaseColumnName="UseTool1">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사용치구2" Key="UseJig1" BaseColumnName="UseJig1">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사용공구2" Key="UseTool2" BaseColumnName="UseTool2">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사용치구2" Key="UseJig2" BaseColumnName="UseJig2">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사용공구3" Key="UseTool3" BaseColumnName="UseTool3">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사용치구3" Key="UseJig3" BaseColumnName="UseJig3">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" BaseColumnName="RegistrationPerson">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" BaseColumnName="UpdatingPerson">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="생산계획번호" Key="ProductionPlanHistoryIndex" BaseColumnName="ProductionPlanHistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="WC작업계획번호" Key="WCDailyWorkPlanHistoryIndex" BaseColumnName="WCDailyWorkPlanHistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업일보번호" Key="WorkDailyReportHistoryIndex" BaseColumnName="WorkDailyReportHistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="제품도면번호" Key="ProductDrawNum" Hidden="True" BaseColumnName="ProductDrawNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
													</Columns>
													<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
														<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
													</RowTemplateStyle>
													<RowEditTemplate>
														<TABLE id="Table8" style="FONT-SIZE: 10pt; WIDTH: 803px; HEIGHT: 167px">
															<TR>
																<TD align="right">품목번호</TD>
																<TD>
																	<asp:TextBox id="tb_ItemNum" runat="server" Width="90px" Font-Size="8pt" BorderStyle="Groove"
																		ReadOnly="True" columnkey="ItemNum"></asp:TextBox></TD>
																<TD align="right">도면번호</TD>
																<TD style="WIDTH: 105px">
																	<asp:TextBox id="tb_ItemDrawNum" runat="server" Width="90px" Font-Size="8pt" BorderStyle="Groove"
																		ReadOnly="True" columnkey="ItemDrawNum"></asp:TextBox></TD>
																<TD align="right">품목명</TD>
																<TD>
																	<asp:TextBox id="tb_ItemName" runat="server" Width="90px" Font-Size="8pt" BorderStyle="Groove"
																		ReadOnly="True" columnkey="ItemName"></asp:TextBox></TD>
																<TD align="right"><FONT face="굴림">Lot 번호 </FONT>
																</TD>
																<TD>
																	<asp:TextBox id="TextBox5" runat="server" Width="90px" Font-Size="8pt" BorderStyle="Groove" columnkey="LotNum"></asp:TextBox></TD>
															</TR>
															<TR>
																<TD align="right">작업시작시간</TD>
																<TD>
																	<igsch:webdatechooser id="wdcBeginTime" runat="server" Width="90px" Font-Size="8pt" Height="18px" NullDateLabel=" "
																		Text="날짜 선택" columnkey="WorkBeginTime">
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
																<TD align="left">
																	<asp:TextBox id="TextBox1" onkeydown="OnlyNum(this);" onkeyup="return Process1()" style="TEXT-ALIGN: right"
																		onfocus="OnFocus_Obj(this);" runat="server" Width="20px" Font-Size="8pt" BorderStyle="Groove"></asp:TextBox>:
																	<asp:TextBox id="TextBox2" onkeydown="OnlyNum(this);" onkeyup="return Process2()" style="TEXT-ALIGN: right"
																		onfocus="OnFocus_Obj(this);" runat="server" Width="20px" Font-Size="8pt" BorderStyle="Groove"></asp:TextBox></TD>
																<TD style="WIDTH: 105px"></TD>
																<TD align="right">작업완료시간</TD>
																<TD>
																	<igsch:webdatechooser id="wdcEndTime" runat="server" Width="90px" Font-Size="8pt" Height="18px" NullDateLabel=" "
																		Text="날짜 선택" columnkey="WorkEndTime">
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
																<TD>
																	<asp:TextBox id="TextBox3" onkeydown="OnlyNum(this);" onkeyup="return Process3()" style="TEXT-ALIGN: right"
																		onfocus="OnFocus_Obj(this);" runat="server" Width="20px" Font-Size="8pt" BorderStyle="Groove"></asp:TextBox>:
																	<asp:TextBox id="TextBox4" onkeydown="OnlyNum(this);" onkeyup="return Process4()" style="TEXT-ALIGN: right"
																		onfocus="OnFocus_Obj(this);" runat="server" Width="20px" BorderStyle="Groove"></asp:TextBox></TD>
																<TD></TD>
															</TR>
															<TR>
																<TD align="right">작업계획수량</TD>
																<TD>
																	<asp:TextBox id="tb_WorkPlanQuantity" style="TEXT-ALIGN: right" runat="server" Width="90px" Font-Size="8pt"
																		ReadOnly="True" columnkey="WorkPlanQuantity"></asp:TextBox></TD>
																<TD align="right">금번완료수량</TD>
																<TD style="WIDTH: 105px">
																	<asp:TextBox id="tb_WorkCompletionQuantity" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																		onkeyup="return Process();" style="TEXT-ALIGN: right" onfocus="OnFocus_Obj(this);" runat="server"
																		Width="90px" Font-Size="8pt" ReadOnly="True" columnkey="ThisWorkCompletionQuantity"></asp:TextBox></TD>
																<TD align="right">적합수량</TD>
																<TD>
																	<asp:TextBox id="tb_SuitabilityQuantity" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																		onkeyup="return Suitability();" style="TEXT-ALIGN: right" onfocus="OnFocus_Obj(this);" runat="server"
																		Width="90px" Font-Size="8pt" ReadOnly="True" columnkey="SuitabilityQuantity"></asp:TextBox></TD>
																<TD align="right">부적합수량</TD>
																<TD>
																	<asp:TextBox id="tb_UnSuitabilityQuantity" style="TEXT-ALIGN: right" runat="server" Width="90px"
																		Font-Size="8pt" ReadOnly="True" columnkey="UnSuitabilityQuantity"></asp:TextBox></TD>
															</TR>
															<TR>
																<TD align="right">부적합금액</TD>
																<TD>
																	<asp:TextBox id="tb_UnSuitabilityCost" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																		style="TEXT-ALIGN: right" runat="server" Width="90px" Font-Size="8pt" ReadOnly="True" columnkey="UnSuitabilityCost"></asp:TextBox></TD>
																<TD align="right">부적합현상</TD>
																<TD style="WIDTH: 105px">
																	<asp:DropDownList id=dl_UnSuitabilityStatusMeaning runat="server" Width="90px" Font-Size="8pt" Height="31px" columnkey="UnSuitabilityStatusMeaning" DataSource="<%# UnSuitabilityStatus %>" DataTextField="SmallClassificationName" DataValueField="SmallClassificationCode">
																	</asp:DropDownList></TD>
																<TD align="right">부적합원인</TD>
																<TD>
																	<asp:DropDownList id=dl_UnSuitabilityCauseMeaning runat="server" Width="90px" Font-Size="8pt" columnkey="UnSuitabilityCauseMeaning" DataSource="<%# UnSuitabilityCause %>" DataTextField="SmallClassificationName" DataValueField="SmallClassificationCode">
																	</asp:DropDownList></TD>
																<TD align="right">부적합세부사유</TD>
																<TD rowSpan="2">
																	<asp:TextBox id="tb_UnSuitabilityDetailMeaning" runat="server" Width="90px" Font-Size="8pt" Height="41px"
																		columnkey="UnSuitabilityDetailMeaning" TextMode="MultiLine"></asp:TextBox></TD>
															</TR>
															<TR>
																<TD align="right">비작업시간1</TD>
																<TD>
																	<asp:TextBox id="tb_NonWorkTime1" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																		style="TEXT-ALIGN: right" runat="server" Width="90px" Font-Size="8pt" columnkey="NonWorkTime1"></asp:TextBox></TD>
																<TD align="right">비작업사유1</TD>
																<TD style="WIDTH: 105px">
																	<asp:DropDownList id=dl_NonWorkTimeReason1 runat="server" Width="90px" Font-Size="8pt" Height="31px" columnkey="NonWorkTimeReason1" DataSource="<%# NonTime %>" DataTextField="SmallClassificationName" DataValueField="SmallClassificationCode">
																	</asp:DropDownList></TD>
																<TD align="right">검사판정</TD>
																<TD>
																	<asp:DropDownList id=dl_InspectionDecision runat="server" Width="90px" Font-Size="8pt" columnkey="InspectionDecision" DataSource="<%# InspectionDecision %>" DataTextField="SmallClassificationName" DataValueField="SmallClassificationCode">
																	</asp:DropDownList></TD>
																<TD align="right"></TD>
															</TR>
															<TR>
																<TD style="HEIGHT: 27px" align="right">비작업시간2</TD>
																<TD style="HEIGHT: 27px">
																	<asp:TextBox id="tb_NonWorkTime2" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																		style="TEXT-ALIGN: right" runat="server" Width="90px" Font-Size="8pt" columnkey="NonWorkTime2"></asp:TextBox></TD>
																<TD style="HEIGHT: 27px" align="right">비작업사유2</TD>
																<TD style="WIDTH: 105px; HEIGHT: 27px">
																	<asp:DropDownList id=dl_NonWorkTimeReason2 runat="server" Width="90px" Font-Size="8pt" Height="31px" columnkey="NonWorkTimeReason2" DataSource="<%# NonTime %>" DataTextField="SmallClassificationName" DataValueField="SmallClassificationCode">
																	</asp:DropDownList></TD>
																<TD style="HEIGHT: 27px" align="right">비작업시간3</TD>
																<TD style="HEIGHT: 27px">
																	<asp:TextBox id="tb_onWorkTime3" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																		style="TEXT-ALIGN: right" runat="server" Width="90px" columnkey="NonWorkTime3"></asp:TextBox></TD>
																<TD style="HEIGHT: 27px" align="right">비작업사유3</TD>
																<TD style="HEIGHT: 27px">
																	<asp:DropDownList id=dl_NonWorkTimeReason3 runat="server" Width="90px" Font-Size="8pt" columnkey="NonWorkTimeReason3" DataSource="<%# NonTime %>" DataTextField="SmallClassificationName" DataValueField="SmallClassificationCode">
																	</asp:DropDownList></TD>
															</TR>
															<TR>
																<TD style="HEIGHT: 10px" align="right">사용공구1</TD>
																<TD style="HEIGHT: 10px">
																	<asp:DropDownList id=dl_UseTool1 runat="server" Width="90px" Font-Size="8pt" Height="31px" columnkey="UseTool1" DataSource="<%# Tool %>" DataTextField="EquipmentName" DataValueField="EquipmentNum">
																	</asp:DropDownList></TD>
																<TD style="HEIGHT: 10px" align="right">사용공구2</TD>
																<TD style="WIDTH: 105px; HEIGHT: 10px">
																	<asp:DropDownList id=dl_UseTool2 runat="server" Width="90px" Font-Size="8pt" Height="31px" columnkey="UseTool2" DataSource="<%# Tool %>" DataTextField="EquipmentName" DataValueField="EquipmentNum">
																	</asp:DropDownList></TD>
																<TD style="HEIGHT: 10px" align="right">사용공구3</TD>
																<TD style="HEIGHT: 10px">
																	<asp:DropDownList id=dl_UseTool3 runat="server" Width="90px" Font-Size="8pt" columnkey="UseTool3" DataSource="<%# Tool %>" DataTextField="EquipmentName" DataValueField="EquipmentNum">
																	</asp:DropDownList></TD>
																<TD style="HEIGHT: 10px" align="right">작업장명</TD>
																<TD style="HEIGHT: 10px">
																	<asp:DropDownList id=dl_WCName runat="server" Width="90px" Font-Size="8pt" columnkey="WCName" DataSource="<%# WCName %>" DataTextField="WCName" DataValueField="WCName">
																	</asp:DropDownList></TD>
															</TR>
															<TR>
																<TD align="right">사용치구1</TD>
																<TD>
																	<asp:DropDownList id=dl_UseJig1 runat="server" Width="90px" Font-Size="8pt" columnkey="UseJig1" DataSource="<%# Jig %>" DataTextField="EquipmentName" DataValueField="EquipmentNum">
																	</asp:DropDownList></TD>
																<TD align="right">사용치구2</TD>
																<TD style="WIDTH: 105px">
																	<asp:DropDownList id=dl_UseJig2 runat="server" Width="90px" Font-Size="8pt" Height="31px" columnkey="UseJig2" DataSource="<%# Jig %>" DataTextField="EquipmentName" DataValueField="EquipmentNum">
																	</asp:DropDownList></TD>
																<TD align="right">사용치구3</TD>
																<TD>
																	<asp:DropDownList id=dl_UseJig3 runat="server" Width="90px" Font-Size="8pt" columnkey="UseJig3" DataSource="<%# Jig %>" DataTextField="EquipmentName" DataValueField="EquipmentNum">
																	</asp:DropDownList></TD>
																<TD align="right">작업자</TD>
																<TD>
																	<asp:DropDownList id=dl_Worker runat="server" Width="90px" Font-Size="8pt" columnkey="Worker" DataSource="<%# Worker %>" DataTextField="Worker" DataValueField="workerID">
																	</asp:DropDownList></TD>
															</TR>
															<TR>
																<TD align="right"><FONT face="굴림">기대번호&nbsp; </FONT>
																</TD>
																<TD>
																	<asp:textbox id="EtcNum1" runat="server" Width="100px" Font-Size="9pt" Height="20px" BorderColor="DimGray"
																		BorderStyle="Groove" BorderWidth="1px"></asp:textbox></TD>
																<TD align="right"><FONT face="굴림">기대번호&nbsp; </FONT>
																</TD>
																<TD>
																	<asp:textbox id="EtcNum2" runat="server" Width="100px" Font-Size="9pt" Height="20px" BorderColor="DimGray"
																		BorderStyle="Groove" BorderWidth="1px"></asp:textbox></TD>
																<TD align="right"></TD>
																<TD></TD>
																<TD></TD>
																<TD></TD>
															</TR>
															<TR>
																<TD align="center" colSpan="8"><INPUT id="igtbl_reOkBtn" style="FONT-SIZE: 8pt" onclick="igtbl_gRowEditButtonClick(event);__doPostBack('lnk_Update','');"
																		type="button" size="20" value="수정">&nbsp; <INPUT id="igtbl_reCancelBtn" style="FONT-SIZE: 8pt" onclick="igtbl_gRowEditButtonClick(event);"
																		type="button" size="20" value="취소"></TD>
															</TR>
														</TABLE>
													</RowEditTemplate>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
								<TR>
									<TD style="PADDING-LEFT: 10px; HEIGHT: 35px"><asp:button id="bt_Excel" runat="server" Width="60px" Font-Size="9pt" Height="20px" Text="Excel"></asp:button><INPUT id="lb_RowIndex" style="WIDTH: 20px; HEIGHT: 22px" type="hidden" size="3" name="Hidden1"
											runat="server"><INPUT id="lb_RowSelectIndex" style="WIDTH: 20px; HEIGHT: 22px" type="hidden" size="1"
											name="Hidden1" runat="server"><INPUT id="lb_PreWorkCompletionQuantity" style="WIDTH: 20px; HEIGHT: 22px" type="hidden"
											size="3" name="Hidden1" runat="server"><INPUT id="lb_PreRemainQuantity" style="WIDTH: 20px; HEIGHT: 22px" type="hidden" size="3"
											name="Hidden1" runat="server"><INPUT id="lb_PreSuitabilityQuantity" style="WIDTH: 20px; HEIGHT: 22px" type="hidden" size="3"
											name="Hidden1" runat="server"><INPUT id="lb_PreUnSuitabilityQuantity" style="WIDTH: 20px; HEIGHT: 22px" type="hidden"
											size="1" name="Hidden1" runat="server"><INPUT id="lb_UseTool1" style="WIDTH: 20px; HEIGHT: 22px" type="hidden" size="3" name="Hidden1"
											runat="server"><INPUT id="lb_UseTool2" style="WIDTH: 20px; HEIGHT: 22px" type="hidden" size="3" name="Hidden1"
											runat="server"><INPUT id="lb_UseTool3" style="WIDTH: 20px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1"
											runat="server"><INPUT id="lb_UseJig1" style="WIDTH: 20px; HEIGHT: 22px" type="hidden" size="3" name="Hidden1"
											runat="server"><INPUT id="lb_UseJig2" style="WIDTH: 20px; HEIGHT: 22px" type="hidden" size="3" name="Hidden1"
											runat="server"><INPUT id="lb_UseJig3" style="WIDTH: 20px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1"
											runat="server"><INPUT id="lb_WorkerID" style="WIDTH: 20px; HEIGHT: 22px" type="hidden" size="3" name="Hidden1"
											runat="server"><INPUT id="BeginTime" style="WIDTH: 48px; HEIGHT: 22px" type="hidden" size="2" name="Hidden1"
											runat="server"><INPUT id="EndTime" style="WIDTH: 48px; HEIGHT: 22px" type="hidden" size="2" name="Hidden1"
											runat="server"><INPUT id="hdYear" style="WIDTH: 20px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1"
											runat="server"><INPUT id="hdMon" style="WIDTH: 20px; HEIGHT: 22px" type="hidden" size="3" name="Hidden1"
											runat="server"></TD>
									<TD style="PADDING-RIGHT: 10px" vAlign="middle" align="right" colSpan="2"><asp:linkbutton id="lnk_Update" runat="server" Visible="False"></asp:linkbutton><INPUT id="chkAll" style="WIDTH: 51px; HEIGHT: 22px" type="hidden" size="3" value="true"
											name="Hidden1" runat="server">
										<asp:button id="bt_Delete" runat="server" Width="60px" Font-Size="9pt" Height="20px" Text="삭  제"></asp:button>&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			<igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter></form>
	</body>
</HTML>
