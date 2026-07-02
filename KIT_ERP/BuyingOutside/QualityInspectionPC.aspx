<%@ Page language="c#" Codebehind="QualityInspectionPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BuyingOutside.QualityInspectionPresentCondition" codePage="949" %>
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
		
		function OK(text)
		{
			if(confirm(text + "하시겠습니까?"))
				return true;
			else
				return false;
		}
		
					// 초기화 버튼
					function ResettxtBox(){
						ResetTextBox();
						ResetBox();
						var objChooser1 = igdrp_getComboById("txtStartDate");
						var objChooser2 = igdrp_getComboById("txtEndDate");
						
						objChooser1.setValue(null);
						objChooser2.setValue(null);
						document.QualityInspectionPresentCondition.ddlDivision.options[0].selected=true;	
						document.QualityInspectionPresentCondition.ddlProcessDiv.options[1].selected=true;	
						
						
						
					}
		
					// 함격수량 입력시 onkeyup 이벤트 때 발생.
					function Process()	{
								var frm = QualityInspectionPresentCondition.txtIncongruityQuantity;
								var value = Number(QualityInspectionPresentCondition.RQ.value) - Number(QualityInspectionPresentCondition.txtSuccessQuantity.value);
								var sqTxt = QualityInspectionPresentCondition.txtSuccessQuantity;

										if(value < 0 )	{
											alert("- 합격수량이 의뢰된 수량보다 많습니다");
											sqTxt.value = sqTxt.value.substring(0, sqTxt.value.length-1);
											sqTxt.focus();
										}else{
											var IncongruityQuantity = (Math.round((value * 10000)) / 10000);
											frm.value = IncongruityQuantity;
											
											var frmDDL1 = document.QualityInspectionPresentCondition.DataGrid1__ctl0_DropDownList1;
											var frmDDL2 = document.QualityInspectionPresentCondition.DataGrid1__ctl0_DropDownList2;
											var frmDDL3 = document.QualityInspectionPresentCondition.DataGrid1__ctl0_DropDownList3;
											
											if ( IncongruityQuantity == 0 )
											{
													frmDDL1.disabled = true;
													frmDDL2.disabled = true;
													frmDDL3.disabled = true;
													document.QualityInspectionPresentCondition.UnDetail.disabled = true;
													document.QualityInspectionPresentCondition.UnSuitabilityCost.disabled = true;
											}
											else
											{
													frmDDL1.disabled = false;
													frmDDL2.disabled = false;
													frmDDL3.disabled = false;											
													document.QualityInspectionPresentCondition.UnDetail.disabled = false;
													document.QualityInspectionPresentCondition.UnSuitabilityCost.disabled = false;
											}
										}
					}
					
					
		//////////////////////////////////////////////////////////////////////////////////////////
		//					Float	        														//
		//////////////////////////////////////////////////////////////////////////////////////////
					
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
					
					
					function DataGrid1_AfterRowTemplateOpenHandler(gridName, rowId)
					{
						var row = igtbl_getRowById(rowId);
						
						QualityInspectionPresentCondition.hdBeforeSucessQauntity.value = row.getCellFromKey("적합수량").getValue();
						QualityInspectionPresentCondition.hdIncongruityQuantity.value = row.getCellFromKey("부적합수량").getValue();
						QualityInspectionPresentCondition.hdBeforeApplyUintCost.value = row.getCellFromKey("ApplyUnitCost").getValue();
						
						var num = rowId.split('_');
						QualityInspectionPresentCondition.hdIndex.value = num[1];
						
						
						
						// Hedden 속성에 키값 저장
						document.QualityInspectionPresentCondition.hidden_UpdateKey.value 
						= row.getCellFromKey("QualityInspectionHistoryIndex").getValue()
						
						var frmDDL1 = document.QualityInspectionPresentCondition.DataGrid1__ctl0_DropDownList1;
						var frmDDL2 = document.QualityInspectionPresentCondition.DataGrid1__ctl0_DropDownList2;
						var frmDDL3 = document.QualityInspectionPresentCondition.DataGrid1__ctl0_DropDownList3;
						var frmDDL4 = document.QualityInspectionPresentCondition.DataGrid1__ctl0_ddlYear;
						var frmDDL5 = document.QualityInspectionPresentCondition.DataGrid1__ctl0_ddlMon;
						
						// 현재 품목의 진행상태 가져오기.
						var varProgressCondition = document.QualityInspectionPresentCondition.txtProgressCondition.value;
						
						// 월마감, 입고완료 되었다면 수정불가 - MonthCloseing 이 1 이면 월마감
						if ( varProgressCondition == "입고완료" ||  MonthCloseing == "1" ) 
						{						
								if ( varProgressCondition == "입고완료")
								{
									document.all("lblProgresscondi").innerText = "입고 완료된 품목은 단가만 수정할 수 있습니다.   ";
									
									document.all("igtbl_reOkBtn").innerText = "수정";	
									document.all("igtbl_reCancelBtn").innerText = "닫기";							
									document.all("txtSuccessQuantity").disabled = true;
									frmDDL1.disabled = true;
									frmDDL2.disabled = true;
									frmDDL3.disabled = true;
									frmDDL4.disabled = true;
									frmDDL5.disabled = true;
									document.QualityInspectionPresentCondition.UnDetail.disabled = true;
									document.QualityInspectionPresentCondition.UnSuitabilityCost.disabled = true;
									//document.all("UnSuitabilityCost").disabled = false;
									
								}
								else if (MonthCloseing == "1" )	// 1 이면 월마감
								{	
									document.all("lblProgresscondi").innerText = "월 마감이 되어 수정할 수 없습니다.   ";
									
									
									document.all("igtbl_reOkBtn").disabled = true;
									document.all("igtbl_reCancelBtn").innerText = "닫기";							
									document.all("txtSuccessQuantity").disabled = true;
									frmDDL1.disabled = true;
									frmDDL2.disabled = true;
									frmDDL3.disabled = true;
									document.QualityInspectionPresentCondition.UnDetail.disabled = true;
									document.QualityInspectionPresentCondition.UnSuitabilityCost.disabled = true;
									document.all("UnSuitabilityCost").disabled = true;
								}
								
								//document.all("igtbl_reOkBtn").disabled = true;
								//document.all("igtbl_reCancelBtn").innerText = "닫기";							
								//document.all("txtSuccessQuantity").disabled = true;
								//frmDDL1.disabled = true;
								//frmDDL2.disabled = true;
								//frmDDL3.disabled = true;
								//document.QualityInspectionPresentCondition.UnDetail.disabled = true;
								//document.all("UnSuitabilityCost").disabled = true;
						}					
						else if ( row.getCellFromKey("부적합수량").getValue() != 0 )		//부적합 수량이 0 이 아니 라면
						{											
								for( var i = 0 ; i < frmDDL1.options.length ; i ++ )
								{
									if( frmDDL1.options[i].innerText == row.getCellFromKey("부적합원인").getValue() )
									{
										frmDDL1.options[i].selected = true ;
									}
								}
								
								for( var i = 0 ; i < frmDDL2.options.length ; i ++ )
								{
									if( frmDDL2.options[i].innerText == row.getCellFromKey("부적합현상").getValue() )
									{
										frmDDL2.options[i].selected = true ;
									}
								}
								
								for( var i = 0 ; i < frmDDL3.options.length ; i ++ )
								{
									if ( frmDDL3.options[i].innerText == row.getCellFromKey("검사판정").getValue() )
									{
										frmDDL3.options[i].selected = true ; 
									}
								}
								
								
								
								document.all("lblProgresscondi").innerText = "";
								document.all("igtbl_reOkBtn").disabled = false;
								document.all("igtbl_reCancelBtn").innerText = "취소";							
								document.all("txtSuccessQuantity").disabled = false;
								frmDDL1.disabled = false;
								frmDDL2.disabled = false;
								frmDDL3.disabled = false;
								document.QualityInspectionPresentCondition.UnDetail.disabled = false;
								document.all("UnSuitabilityCost").disabled = false;
								
								
						}
						else
						{
								frmDDL1.disabled = true;
								frmDDL2.disabled = true;
								frmDDL3.disabled = true;
								
								document.QualityInspectionPresentCondition.UnDetail.disabled = true;
						}
						
						
								document.QualityInspectionPresentCondition.hdyear.value = row.getCellFromKey("Year").getValue();
								document.QualityInspectionPresentCondition.hdmon.value = row.getCellFromKey("Month").getValue();
								
						// rowID 보존
						document.QualityInspectionPresentCondition.hidden_TemplateRow_UpdateValue.value = rowId;
						
						// 기존의 합격수량 보존
						document.all("hidden_BeforeSuccessQuantity").value = document.all("txtSuccessQuantity").value
						//기존 단가 보존
						
						
						
					}
					
					
					
					function Rowedit()
					{
							var row = igtbl_getRowById(document.QualityInspectionPresentCondition.hidden_TemplateRow_UpdateValue.value);
							var id = document.QualityInspectionPresentCondition.hidden_TemplateRow_UpdateValue.value;
							document.QualityInspectionPresentCondition.hidden_TemplateRow_UpdateValue.value = "";
							
							var beforeSuccessQuantity = document.all("hidden_BeforeSuccessQuantity").value;
							
							var val = document.QualityInspectionPresentCondition.hidden_TemplateRow_UpdateValue;
							var frm = document.QualityInspectionPresentCondition;//.value
							
							
							// 0 업데이트 될 품질검사원장의 키
							val.value += frm.hidden_UpdateKey.value + "〉";
							
							// 1 완료수량 = 수정된수량 
							val.value += frm.txtSuccessQuantity.value + "〉";
							
							// 2 부적합 수량
							val.value += frm.txtIncongruityQuantity.value + "〉";
							
							if ( frm.txtSuccessQuantity.value ==  frm.RQ.value && frm.txtIncongruityQuantity.value == 0)
							{
								val.value += "〉";		// 검사판정
								val.value += "〉";		// 부적합원인
								val.value += "〉";		// 부적합원인코드
								val.value += "〉";		// 부적합현상
								val.value += "〉";		// 부적합현상코드
								val.value += "〉";		// 부적합세부내용
							}
							else
							{
								// 3 검사판정
								val.value += frm.DataGrid1__ctl0_DropDownList3.options[frm.DataGrid1__ctl0_DropDownList3.selectedIndex].innerText + "〉"
								
								// 4 부적합 원인
								val.value += frm.DataGrid1__ctl0_DropDownList1.options[frm.DataGrid1__ctl0_DropDownList1.selectedIndex].innerText + "〉"
								
								// 5 부적합원인코드
								val.value += frm.DataGrid1__ctl0_DropDownList1.options[frm.DataGrid1__ctl0_DropDownList1.selectedIndex].value + "〉"
								
								// 6 부적합 현상
								val.value += frm.DataGrid1__ctl0_DropDownList2.options[frm.DataGrid1__ctl0_DropDownList2.selectedIndex].innerText + "〉"
								
								// 7 부적합현상코드 
								val.value += frm.DataGrid1__ctl0_DropDownList2.options[frm.DataGrid1__ctl0_DropDownList2.selectedIndex].value + "〉"
								
								// 8 부적합 세부내용
								val.value += frm.UnDetail.value + "〉";
							}
							
							// 9 검사자
							val.value += '<%= Session["UserName"].ToString()%>' + "〉";
							
							// 10 검사자 ID
							val.value += '<%= Session["ID"].ToString()%>' + "〉";
							
							// 11 부적합금액
							val.value += frm.UnSuitabilityCost.value + "〉";
							
							// 12 원장구분1
							val.value += row.getCellFromKey("historysection1").getValue()  + "〉";
							
							// 13 원장구분1 번호
							val.value += row.getCellFromKey("HistoryIndex1").getValue()  + "〉";
							
							// 14 원장구분2
							val.value += row.getCellFromKey("HistorySection2").getValue()  + "〉";
							
							// 15 원장구분2 번호
							val.value += row.getCellFromKey("HistoryIndex2").getValue()  + "〉";
							
							// 16 품목명
							val.value += frm.ItemName.value + "〉";
							
							// 17 품목번호
							val.value += frm.ItemNum.value + "〉";
							
							// 18 도면번호
							val.value += frm.ItemDrawNum.value + "〉";
							
							// 19 거래처명
							val.value += frm.CompanyName.value + "〉";
							
							// 20 거래처코드
							val.value += frm.BusinessCompanyNum.value + "〉";
							
							// 21 단가
							val.value += frm.txtApplyUnitCost.value + "〉";
							
							// 22 시작공정 순서번호
							val.value += row.getCellFromKey("시작공정순서").getValue()  + "〉";
							
							// 23 종료공정 순서번호
							val.value += row.getCellFromKey("종료공정순서").getValue()  + "〉";
							
							// 24 시작공정 코드
							val.value += row.getCellFromKey("시작공정코드").getValue()  + "〉";
							
							// 25 종료 공정명
							val.value += row.getCellFromKey("종료공정명").getValue()  + "〉";
							
							// 26 종료공정 코드
							val.value += row.getCellFromKey("종료공정코드").getValue() +  "〉";
							
							// 27 차이수량 = 현재 수정한 수량 - 이전 완료 수량
							val.value += Number(frm.txtSuccessQuantity.value) - Number(beforeSuccessQuantity)+  "〉";
							
							val.value += frm.hdyear.value +  "〉";
							val.value += frm.hdmon.value+  "〉";
							
							frm.hdyear1.value = frm.DataGrid1__ctl0_ddlYear.options[frm.DataGrid1__ctl0_ddlYear.selectedIndex].innerText;
							frm.hdmon1.value = frm.DataGrid1__ctl0_ddlMon.options[frm.DataGrid1__ctl0_ddlMon.selectedIndex].innerText;
							
							val.value += frm.hdyear1.value +  "〉";
							val.value += frm.hdmon1.value;
							
							
							Quantity();
							
							__doPostBack("Button3");
					}
					
					function Quantity()
					{
						QualityInspectionPresentCondition.hdSucessQuantity.value = document.all("txtSuccessQuantity").value
					}
					
					
				
/*
					function EditRegistCheck()
					{
						if(document.QualityInspectionPresentCondition.txtSuccessQuantity.value == "")
						{
							alert("적합수량에 수량이 기입되지 않았습니다.");
							return false;
						}
										
						if(confirm("현재 품목을 수정 하시겠습니까?"))
							return true;
						else
							return false;
					}
*/					
/*					
					function DataGrid1_ColumnHeaderClickHandler(gridName, columnId, button)
					{
						var myCol = igtbl_getColumnById(columnId);

						if (myCol.Key == "ChBox")
						{
							var myGrid = igtbl_getGridById(gridName);
							for (i = 0; i < myGrid.Rows.length; i++)
							{
								if(myGrid.Rows.getRow(i) .getCellFromKey("ChBox").getValue() == 0)
									myGrid.Rows.getRow(i) .getCellFromKey("ChBox").setValue(1);
								else
									myGrid.Rows.getRow(i) .getCellFromKey("ChBox").setValue(0) ;
							}
						}
					}
		
*/
		
		
			//-->
		</script>
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
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
									<TD align="right" width="70" height="30">검사일&nbsp;</TD>
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
									<TD align="right" width="270" colSpan="2" height="30"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" type="button" value="초기화" name="btnReset"
											runat="server">&nbsp;
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
							<igtbl:ultrawebgrid id="DataGrid1" runat="server" Width="100%" Height="402px">
								<DisplayLayout ColFootersVisibleDefault="Yes" StationaryMargins="Header" AutoGenerateColumns="False"
									RowHeightDefault="20px" RowSizingDefault="Free" Version="3.00" SelectTypeRowDefault="Single"
									AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate"
									AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="DataGrid1" TableLayout="Fixed"
									CellClickActionDefault="RowSelect">
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
										BorderColor="DimGray" BorderStyle="Groove" BackColor="Silver" Height="402px"></FrameStyle>
									<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</FooterStyleDefault>
									<ClientSideEvents ColumnHeaderClickHandler="ColumnHeaderClickHandler" AfterRowTemplateOpenHandler="DataGrid1_AfterRowTemplateOpenHandler"
										TemplateUpdateCellsHandler="DataGrid1_TemplateUpdateCellsHandler"></ClientSideEvents>
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
											<igtbl:UltraGridColumn HeaderText="선택" Key="chk" Width="25px" Type="CheckBox" BaseColumnName="" AllowUpdate="Yes">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle Height="25px"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="구분" Key="historysection1" Width="40px" HeaderClickAction="SortMulti"
												BaseColumnName="historysection1" AllowResize="Free">
												<SelectedHeaderStyle BackColor="White"></SelectedHeaderStyle>
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
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
											<igtbl:UltraGridColumn HeaderText="단위" Key="Unit" Width="40px" BaseColumnName="Unit">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="규격" Key="Standard" BaseColumnName="Standard">
												<CellStyle>
													<Padding Left="2px"></Padding>
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
											<igtbl:UltraGridColumn HeaderText="현재고" Key="NowStockQuantity" Width="65px" Format="###,###,###.##" BaseColumnName="NowStockQuantity">
												<CellStyle HorizontalAlign="Right">
													<Padding Right="4px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="의뢰수량" Key="RequestQuantity" Width="65px" Format="###,###,##0.00" HeaderClickAction="SortMulti"
												BaseColumnName="RequestQuantity" FooterTotal="Sum">
												<FooterStyle HorizontalAlign="Right">
													<Padding Right="5px"></Padding>
												</FooterStyle>
												<CellStyle HorizontalAlign="Right">
													<Padding Right="5px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="적합수량" Key="적합수량" Format="###,###,##0.00" HeaderClickAction="SortMulti"
												BaseColumnName="적합수량" FooterTotal="Sum">
												<FooterStyle HorizontalAlign="Right">
													<Padding Right="5px"></Padding>
												</FooterStyle>
												<CellStyle HorizontalAlign="Right">
													<Padding Right="5px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="부적합수량" Key="부적합수량" Format="###,###,##0.00" HeaderClickAction="SortMulti"
												BaseColumnName="부적합수량" FooterTotal="Sum">
												<FooterStyle HorizontalAlign="Right">
													<Padding Right="5px"></Padding>
												</FooterStyle>
												<CellStyle HorizontalAlign="Right">
													<Padding Right="5px"></Padding>
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
												BaseColumnName="부적합금액" FooterTotal="Sum">
												<FooterStyle HorizontalAlign="Right">
													<Padding Right="5px"></Padding>
												</FooterStyle>
												<CellStyle HorizontalAlign="Right">
													<Padding Right="5px"></Padding>
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
												BaseColumnName="ApplyUnitCost" FooterTotal="Sum">
												<FooterStyle HorizontalAlign="Right">
													<Padding Right="5px"></Padding>
												</FooterStyle>
												<CellStyle HorizontalAlign="Right">
													<Padding Right="5px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="표준단가" Key="StandardUnitCost" Width="60px" BaseColumnName="StandardUnitCost">
												<CellStyle HorizontalAlign="Right">
													<Padding Right="4px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
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
											<igtbl:UltraGridColumn HeaderText="매입년도" Key="Year" Width="60px" BaseColumnName="Year"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="매입월" Key="Month" Width="50px" BaseColumnName="Month"></igtbl:UltraGridColumn>
										</Columns>
										<RowTemplateStyle Cursor="Default" BorderColor="Silver" BorderStyle="Groove" HorizontalAlign="Justify"
											BackColor="WhiteSmoke">
											<Padding Bottom="3px" Left="3px" Top="3px" Right="3px"></Padding>
											<BorderDetails WidthLeft="1px" WidthTop="1px" WidthRight="1px" WidthBottom="1px"></BorderDetails>
										</RowTemplateStyle>
										<RowEditTemplate>
											<TABLE id="Table1" style="FONT-WEIGHT: normal; COLOR: buttontext" cellSpacing="2" cellPadding="0"
												bgColor="whitesmoke" border="0">
												<TR>
													<TD align="right" width="110" bgColor="gainsboro" height="20">품목 번호&nbsp;
													</TD>
													<TD width="110" height="20"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
															disabled readOnly type="text" size="12" name="ItemNum" columnkey="ItemNum"></TD>
													<TD align="right" width="90" bgColor="gainsboro" height="20">품목명&nbsp;
													</TD>
													<TD width="110" height="20"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
															disabled readOnly type="text" size="12" name="ItemName" columnkey="ItemName"></TD>
													<TD align="right" width="110" bgColor="gainsboro" height="20">도면 번호&nbsp;
													</TD>
													<TD width="110" height="20"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; COLOR: #000033; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
															disabled readOnly type="text" size="12" name="ItemDrawNum" columnkey="ItemDrawNum"></TD>
												</TR>
												<TR>
													<TD align="right" bgColor="gainsboro" height="20">구분&nbsp;
													</TD>
													<TD width="10" height="20"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
															disabled readOnly type="text" size="12" columnkey="historysection1"></TD>
													<TD align="right" bgColor="gainsboro" height="20">거래처명&nbsp;
													</TD>
													<TD height="20"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
															disabled readOnly type="text" size="12" name="CompanyName" columnkey="CompanyName"></TD>
													<TD align="right" bgColor="gainsboro" height="20">사업자 등록번호&nbsp;</TD>
													<TD height="20"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
															disabled readOnly type="text" size="12" name="BusinessCompanyNum" columnkey="BusinessCompanyNum"></TD>
												</TR>
												<TR>
													<TD align="right" bgColor="#dcdcdc" height="20">시작 공정순서&nbsp;
													</TD>
													<TD width="10" height="20"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
															disabled readOnly type="text" size="12" columnkey="시작공정순서"></TD>
													<TD align="right" bgColor="#dcdcdc" height="20">시작 공정명&nbsp;
													</TD>
													<TD height="20"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
															disabled readOnly type="text" size="12" columnkey="시작공정명"></TD>
													<TD align="right" bgColor="gainsboro" height="20">의뢰 수량&nbsp;
													</TD>
													<TD height="20"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
															disabled readOnly type="text" size="12" name="RQ" columnkey="RequestQuantity"></TD>
												</TR>
												<TR>
													<TD align="right" bgColor="gainsboro" height="20">종료 공정순서&nbsp;
													</TD>
													<TD height="20"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
															disabled readOnly type="text" columnkey="종료공정순서"></TD>
													<TD align="right" bgColor="gainsboro" height="20">종료 공정명&nbsp;
													</TD>
													<TD height="20"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
															disabled readOnly type="text" columnkey="종료공정명"></TD>
													<TD align="right" bgColor="#dcdcdc" height="20">진행상태&nbsp;
													</TD>
													<TD height="20"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: left"
															disabled readOnly type="text" name="txtProgressCondition" columnkey="ProgressCondition">
													</TD>
												</TR>
												<TR>
													<TD align="right" bgColor="darkgray" colSpan="6" height="2"></TD>
												</TR>
												<TR>
													<TD align="right" bgColor="gainsboro" height="20">적합수량&nbsp;
													</TD>
													<TD align="right" bgColor="gainsboro" height="20"><INPUT onblur="OnBlur_Float(this);" onkeyup="return Process()" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
															onfocus="OnFocus_Obj(this);" type="text" name="txtSuccessQuantity" columnkey="적합수량" onOnKeyDown="OnKeyDown_Float(this);"></TD>
													<TD align="right" bgColor="gainsboro" height="20">부적합 수량&nbsp;
													</TD>
													<TD height="20"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
															disabled readOnly type="text" size="12" name="txtIncongruityQuantity" columnkey="부적합수량"></TD>
													<TD align="right" bgColor="#dcdcdc" height="20">단가&nbsp;</TD>
													<TD height="20"><INPUT onblur="OnBlur_Float(this);" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
															type="text" name="txtApplyUnitCost" columnkey="ApplyUnitCost"></TD>
												</TR>
												<TR>
													<TD style="HEIGHT: 16px" align="right" bgColor="gainsboro" height="16">부적합 원인&nbsp;
													</TD>
													<TD style="HEIGHT: 16px" height="16">
														<asp:dropdownlist id=DropDownList1 runat="server" Height="20px" Width="110px" DataTextField="SmallClassificationName" DataSource="<%# DDL_DataSource.Tables[0]%>" DataValueField="SmallClassificationCode">
														</asp:dropdownlist></TD>
													<TD style="HEIGHT: 16px" align="right" bgColor="gainsboro" height="16">부적합 현상&nbsp;
													</TD>
													<TD style="HEIGHT: 16px" height="16">
														<asp:dropdownlist id=DropDownList2 runat="server" Height="20px" Width="110px" DataTextField="SmallClassificationName" DataSource="<%# DDL_DataSource.Tables[1]%>" DataValueField="SmallClassificationCode">
														</asp:dropdownlist></TD>
													<TD align="right" bgColor="gainsboro" height="16">검사판정&nbsp;
													</TD>
													<TD style="HEIGHT: 16px" height="16">
														<asp:dropdownlist id=DropDownList3 runat="server" Height="20px" Width="110px" DataTextField="SmallClassificationName" DataSource="<%# DDL_DataSource.Tables[2]%>" DataValueField="SmallClassificationCode">
														</asp:dropdownlist></TD>
												</TR>
												<TR>
													<TD align="right" bgColor="gainsboro">부적합&nbsp;세부내용&nbsp;
													</TD>
													<TD colSpan="3" rowSpan="1"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: left"
															type="text" name="UnDetail" columnkey="부적합세부내용">
													</TD>
													<TD align="right" bgColor="#dcdcdc">부적합 금액&nbsp;
													</TD>
													<TD><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
															type="text" name="UnSuitabilityCost" columnkey="UnSuitabilityCost"></TD>
												</TR>
												<TR>
													<TD align="right" bgColor="gainsboro" height="20">등록자&nbsp;
													</TD>
													<TD height="20"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
															disabled type="text" size="13" columnkey="RegistrationPerson"></TD>
													<TD align="right" bgColor="gainsboro" height="20">검사자&nbsp;
													</TD>
													<TD height="20"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
															disabled type="text" size="13" columnkey="Investigator"></TD>
													<TD align="right" bgColor="gainsboro" height="20">수정사유&nbsp;</TD>
													<TD height="20"><INPUT id="UpdateRegion" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: left"
															type="text">
													</TD>
												</TR>
												<TR>
													<TD align="right" bgColor="gainsboro" height="20">매입년도&nbsp;
													</TD>
													<TD height="20"><FONT face="굴림">
															<asp:dropdownlist id="ddlYear" runat="server" Width="55px" BackColor="#EEEEE9" columnkey="Year">
																<asp:ListItem Value="2020">2020</asp:ListItem>
																<asp:ListItem Value="2021">2021</asp:ListItem>
																<asp:ListItem Value="2022">2022</asp:ListItem>
																<asp:ListItem Value="2023">2023</asp:ListItem>
																<asp:ListItem Value="2024">2024</asp:ListItem>
																<asp:ListItem Value="2025">2025</asp:ListItem>
																<asp:ListItem Value="2026">2026</asp:ListItem>
																<asp:ListItem Value="2027">2027</asp:ListItem>
																<asp:ListItem Value="2028">2028</asp:ListItem>
																<asp:ListItem Value="2029">2029</asp:ListItem>
																<asp:ListItem Value="2030">2030</asp:ListItem>
															</asp:dropdownlist></FONT></TD>
													<TD align="right" bgColor="gainsboro" height="20">매입월&nbsp;
													</TD>
													<TD height="20"><FONT face="굴림">
															<asp:dropdownlist id="ddlMon" runat="server" Width="40px" BackColor="#EEEEE9" columnkey="Month">
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
															</asp:dropdownlist></FONT></TD>
													<TD align="right" bgColor="gainsboro" height="20"></TD>
													<TD height="20"></TD>
												</TR>
												<TR>
													<TD style="HEIGHT: 14px" align="right" bgColor="#dcdcdc">등록일&nbsp;
													</TD>
													<TD style="HEIGHT: 14px"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
															disabled type="text" size="13" columnkey="RegistrationDate"></TD>
													<TD style="HEIGHT: 14px" align="right" bgColor="#dcdcdc">검사완료일&nbsp;
													</TD>
													<TD style="HEIGHT: 14px"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
															disabled type="text" size="13" columnkey="QualityInspectionCompleteDate"></TD>
													<TD align="right" bgColor="#dcdcdc">수정일&nbsp;</TD>
													<TD style="HEIGHT: 14px">
														<DIV id="divDate" align="center"><%= System.DateTime.Today.ToString().Substring(0, 10)%></DIV>
														<INPUT type=hidden value='<%= Session["ID"].ToString()%>' name=userid>
													</TD>
												</TR>
												<TR height="50">
													<TD align="right" colSpan="5" height="30">
														<DIV id="lblProgresscondi" style="DISPLAY: inline; FONT-WEIGHT: bold; COLOR: #990033"
															ms_positioning="FlowLayout">&nbsp;</DIV>
													</TD>
													<TD><INPUT id="igtbl_reOkBtn" style="WIDTH: 50px" onclick="Rowedit(); " type="button" value="수정"
															name="igtbl_reOkBtn">&nbsp; <INPUT id="igtbl_reCancelBtn" style="WIDTH: 50px" onclick="igtbl_gRowEditButtonClick(event);"
															type="button" value="취소" name="igtbl_reCancelBtn"> <!--
														<INPUT id="igtbl_reOkBtn" style="WIDTH: 50px" onclick="igtbl_gRowEditButtonClick(event); __doPostBack('linkUpdate','');"
															type="button" value="수정" name="igtbl_reOkBtn">&nbsp; <INPUT id="igtbl_reCancelBtn" style="WIDTH: 50px" onclick="igtbl_gRowEditButtonClick(event);"
															type="button" value="취소" name="igtbl_reCancelBtn">
														--></TD>
												</TR>
											</TABLE>
										</RowEditTemplate>
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
									<TD align="left" colSpan="8"><asp:button id="btExcel" runat="server" Height="20px" Text="Excel"></asp:button><INPUT id="hdUpdateRegion" style="WIDTH: 24px; HEIGHT: 21px" type="hidden" size="1" runat="server"><INPUT id="hdOldYear" style="WIDTH: 24px; HEIGHT: 21px" type="hidden" size="1" name="Hidden1"
											runat="server"><INPUT id="hdOldMonth" style="WIDTH: 24px; HEIGHT: 21px" type="hidden" size="1" name="Hidden2"
											runat="server"><INPUT id="hdyear" style="WIDTH: 24px; HEIGHT: 21px" type="hidden" size="1" name="Hidden1"
											runat="server"><INPUT id="hdmon" style="WIDTH: 24px; HEIGHT: 21px" type="hidden" size="1" name="Hidden2"
											runat="server"><INPUT id="hdyear1" style="WIDTH: 24px; HEIGHT: 21px" type="hidden" size="1" name="Hidden1"
											runat="server"><INPUT id="hdmon1" style="WIDTH: 24px; HEIGHT: 21px" type="hidden" size="1" name="Hidden2"
											runat="server"></TD>
									<TD align="right" colSpan="3"><asp:button id="Button3" runat="server" Height="20px" Text="Button" Visible="False"></asp:button><asp:button id="btCancle" runat="server" Height="20px" Text="검사취소"></asp:button>&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hdBeforeApplyUintCost" style="Z-INDEX: 109; LEFT: 856px; POSITION: absolute; TOP: 360px"
				type="hidden" value="0" name="Hidden1" runat="server">
			<igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter><INPUT id="hdBeforeSucessQauntity" style="Z-INDEX: 108; LEFT: 856px; POSITION: absolute; TOP: 232px"
				type="hidden" value="0" name="hdBeforeSucessQauntity" runat="server"> <INPUT id="hdIndex" style="Z-INDEX: 106; LEFT: 856px; POSITION: absolute; TOP: 328px" type="hidden"
				value="0" name="Hidden1" runat="server"> <INPUT id="hdIncongruityQuantity" style="Z-INDEX: 105; LEFT: 856px; POSITION: absolute; TOP: 296px"
				type="hidden" value="0" runat="server"> <INPUT style="Z-INDEX: 101; LEFT: 832px; POSITION: absolute; TOP: 24px" type="hidden" name="hidden_BeforeSuccessQuantity"><INPUT style="Z-INDEX: 102; LEFT: 832px; POSITION: absolute; TOP: 56px" type="hidden" name="hidden_UpdateKey"><INPUT id="hidden_TemplateRow_UpdateValue" style="Z-INDEX: 103; LEFT: 832px; POSITION: absolute; TOP: 88px"
				type="hidden" name="hidden_TemplateRow_UpdateValue"><INPUT id="hdSucessQuantity" style="Z-INDEX: 104; LEFT: 856px; POSITION: absolute; TOP: 264px"
				type="hidden" value="0" runat="server"></form>
	</body>
</HTML>
