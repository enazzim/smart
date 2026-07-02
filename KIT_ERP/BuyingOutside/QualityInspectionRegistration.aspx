<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="QualityInspectionRegistration.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BuyingOutside.QualityInspectionRegister" codePage="949" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v1.1, Version=1.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>품질검사 등록</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!--	
		
		
					function Enter_Check(){
						// 엔터키의 코드는 13입니다.
						if(event.keyCode == 13){
							QualityInspectionRegister.btnRegist.submit();
						}
					}
					
					
					
							
					// 등록버튼을 누른경우 폼을 체크
					function RegistCheck() 
					{
						
						
						
						
						var SQ =  QualityInspectionRegister.txtSuccessQuantity.value ;
						var RQ =  QualityInspectionRegister.txtRequestQuantity_Result.value ;
						var IQ =  QualityInspectionRegister.txtIncongruityQuantity.value ;
						
						if ( SQ == "" )
						{
							alert("- 적합수량을 입력하세요.  ");
							return false;
						}
						else
						{
							if( RemoveComma(IQ) > 0.00 )	
							{
								if(QualityInspectionRegister.ddlIncongruityDecision.selectedIndex == 0) 	
								{
									alert("- 부적합 항목에 대한 검사판정을 선택하세요."); return false;
								} else	if( QualityInspectionRegister.ddlIncongruityCause.selectedIndex == 0 ) 
								{
									alert("- 부적합 항목에 대한 부적합 원인을 선택하세요."); return false;
								} else	if( QualityInspectionRegister.ddlIncongruityPhenomenon.selectedIndex == 0 )	
								{
									alert("- 부적합 항목에 대한 부적합 현상을 선택하세요."); return false;
								}
							}
							else if( RemoveComma(IQ) == 0.00 )		
							{
								if(QualityInspectionRegister.ddlIncongruityDecision.selectedIndex != 0)  
								{
									alert("- 전수합격인 경우는 검사판정 항목을 선택할 수 없습니다.");		return false;
								} else if(QualityInspectionRegister.ddlIncongruityCause.selectedIndex != 0)  
								{
									alert("- 전수합격인 경우는 부적합 원인 항목을 선택할 수 없습니다.");	return false;
								} else if( QualityInspectionRegister.ddlIncongruityPhenomenon.selectedIndex != 0 )  
								{
									alert("- 전수합격인 경우는 부적합 현상 항목을 선택할 수 없습니다.");	return false;
								} else if( QualityInspectionRegister.txtIncongruityDetailContent_Result.value != "" )  
								{
									alert("- 전수합격인 경우는 부적합 세부내용 항목을 기입할 수 없습니다.");	return false;
								} else if ( QualityInspectionRegister.txtIncongruityMoney.value != "0" )   
								{
									alert("- 전수합격인 경우는 부적합 금액을 기입할 수 없습니다.");	return false;
								}
							}		
							
							if(confirm("- 검사결과를 등록 하시겠습니까? ") )	
							{
								this.DataReady();
								return true;
							}
							else
							{
								return false;
							}
						}
					} 
										
					function DataReady()
					{
						var id = document.QualityInspectionRegister.hidden_DataValue.value;
						var frmData = document.QualityInspectionRegister.hidden_DataValue;
						var frm = document.QualityInspectionRegister;
						
						frmData.value = "";
						
						frmData.value = document.getElementById(id).children[20].innerText + "ㅙ";	// Key
						frmData.value += RemoveComma(frm.txtSuccessQuantity.value) + "ㅙ";
						frmData.value += RemoveComma(frm.txtIncongruityQuantity.value) + "ㅙ";
						frmData.value += frm.ddlIncongruityDecision.options[ frm.ddlIncongruityDecision.selectedIndex ].innerText + "ㅙ";
						frmData.value += frm.ddlIncongruityCause.options[ frm.ddlIncongruityCause.selectedIndex].innerText + "ㅙ";
						frmData.value += frm.ddlIncongruityCause.options[ frm.ddlIncongruityCause.selectedIndex].value + "ㅙ";
						frmData.value += frm.ddlIncongruityPhenomenon.options[frm.ddlIncongruityPhenomenon.selectedIndex].innerText + "ㅙ";
						frmData.value += frm.ddlIncongruityPhenomenon.options[frm.ddlIncongruityPhenomenon.selectedIndex].value + "ㅙ";
						frmData.value += frm.txtIncongruityDetailContent_Result.value + "ㅙ";
						frmData.value += frm.txtPerson_Result.value + "ㅙ";
						frmData.value += RemoveComma(frm.txtIncongruityMoney.value) + "ㅙ";
						frmData.value += document.getElementById(id).children[0].innerText + "ㅙ";				// 원장구분("구매", "외주", "자가")
						frmData.value += document.getElementById(id).children[17].innerText + "ㅙ";			// 구분된 원장1의 인덱스
						frmData.value += document.getElementById(id).children[18].innerText + "ㅙ";			// 원장구분2 - (구매발주원장, 외주발주원장)
						frmData.value += document.getElementById(id).children[19].innerText + "ㅙ";			// 원장구분2 인덱스
						frmData.value += frm.txtItemName_Result.value + "ㅙ";
						frmData.value += frm.txtItemNumber_Result.value + "ㅙ";
						frmData.value += frm.txtDrawingNumber_Result.value + "ㅙ";
						frmData.value += frm.txtCustomerName_Result.value + "ㅙ";
						frmData.value += frm.txtBusinessRegistrationNum_Result.value + "ㅙ";
						frmData.value += RemoveComma(frm.txtPrice.value) + "ㅙ";
						frmData.value += document.getElementById(id).children[7].innerText + "ㅙ";
						frmData.value += document.getElementById(id).children[9].innerText + "ㅙ";
						frmData.value += document.getElementById(id).children[21].innerText + "ㅙ";			// 시자공정코드
						frmData.value += document.getElementById(id).children[10].innerText + "ㅙ";			// 종료공정명
						frmData.value += document.getElementById(id).children[22].innerText + "ㅙ";			// 종료공정코드
						frmData.value += frm.ddlIncongruityDecision.selectedIndex + "ㅙ";	
						frmData.value += document.getElementById(id).children[14].innerText + "ㅙ";
						
						frmData.value += frm.ddlYear.options[frm.ddlYear.selectedIndex].value + "ㅙ";
						frmData.value += frm.ddlMon.options[frm.ddlMon.selectedIndex].value + "ㅙ";
						
						//var date = igdrp_getComboById("wdcQualityInspectionCompleteDate");
						//if(date.getValue() != null)
						//{
						//	var year = date.getValue().getFullYear();
						//	var mon = date.getValue().getMonth()+1;
						//	var day = date.getValue().getDay();						
						//	frmData.value += year+ "ㅙ";
						//	frmData.value += mon+ "ㅙ";
						//	frmData.value += day;
						//}
						//else
						//{
						//	var date = new Date();
						//	var year  = date.getFullYear();
						//	var mon = date.getMonth() + 1; 
						//	var day   = date.getDate();
						//	frmData.value += year+ "ㅙ";
						//	frmData.value += mon+ "ㅙ";
						//	frmData.value += day;
						//
						//}
						
						
					}
					
			
					// Body OnLoad때 호출됨.
					function Reset()	{
						QualityInspectionRegister.txtSuccessQuantity.value = "";
						QualityInspectionRegister.txtIncongruityQuantity.value = "";
						QualityInspectionRegister.txtIncongruityMoney.value = "";
					}
					
					// 콤마제거 함수
					function RemoveComma(Ovalue)
					{
						var array = new String(Ovalue).split(",");
						var Rvalue = new String();
						for ( var i = 0 ; i < array.length ; i++)	{
							Rvalue += array[i];
						}
						return Rvalue;
					}
					
					// 함격수량 입력시 onkeyup 이벤트 때 발생.
					function Process()	
					{
					
						var txtIncongruityQuantity = QualityInspectionRegister.txtIncongruityQuantity;
						var value = Number(RemoveComma(QualityInspectionRegister.txtRequestQuantity_Result.value)) - Number(RemoveComma(QualityInspectionRegister.txtSuccessQuantity.value));
						var sqTxt = QualityInspectionRegister.txtSuccessQuantity;
					
						if ( value < 0 )	{
							alert("- 합격수량이 의뢰된 수량보다 많습니다");
							sqTxt.value = "" //sqTxt.value.substring(0, sqTxt.value.length-1);
							sqTxt.focus();
						} else {
							txtIncongruityQuantity.value = MakeCommaSeparate((Math.round((value * 10000)) / 10000));
						}
						
						if ( value >= 0.00 )
						{
							QualityInspectionRegister.ddlIncongruityCause.selectedIndex = 0;
							QualityInspectionRegister.ddlIncongruityDecision.selectedIndex = 0;
							QualityInspectionRegister.ddlIncongruityPhenomenon.selectedIndex = 0;
							QualityInspectionRegister.txtIncongruityDetailContent_Result.value = "";
							QualityInspectionRegister.txtIncongruityMoney.value = "0";
						}
					
					}
						
			
/*			
					function WindowOpen(flag) {
							var vlaue;
							if(flag == 'item') {
										value = document.QualityInspectionRegister.txtItemNumber.value;
										if( SearchCheck() ) {
												var arr = null
												arr = window.showModalDialog("popupItemNumberFinder.aspx?itemNum="+ value + "","f","dialogHeight: 400px; dialogWidth: 450px; center: yes; help: no; resizable: no; status: no;");
													if (arr != null) {
																		QualityInspectionRegister.txtItemNumber.value = arr["ItemNum"];
																		QualityInspectionRegister.txtDrawingNumber.value = arr["ItemDrawNum"];
																		QualityInspectionRegister.txtItemName.value = arr["ItemName"];
													}	
										}
							} else if ( flag == 'customer' ) {
										value = document.QualityInspectionRegister.txtCustomerName.value;
										if( SearchCheck() ) {
												var arr = null
												arr = window.showModalDialog("popupcustomerNameFinder.aspx?customerName="+ value + "","f","dialogHeight: 400px; dialogWidth: 450px; dialogTop=100 ; dialogLeft=300; center: yes; help: no; resizable: no; status: no;");
													if (arr != null) {
														QualityInspectionRegister.txtCustomerName.value = arr["companyName"];
														QualityInspectionRegister.hidden_businessCompanyNum.value = arr["businessCompanyNum"];
													}	
										}
							}						
					} // function WindowOpen(flag) 					
*/					
					
					// 초기화 버튼
					function ResettxtBox()
					{
						ResetTextBox();
						var objCombo2 = igcmbo_getComboById("wcCompanyName");
						var objChooser1 = igdrp_getComboById("txtStartDate");
						var objChooser2 = igdrp_getComboById("txtEndDate");
						objCombo2.setDisplayValue("");
						objChooser1.setValue(null);
						objChooser2.setValue(null);
					}
					
					
//////////////////////////////////////////////////////////////////////////////////////////
//															Float	        														//
//////////////////////////////////////////////////////////////////////////////////////////
					
					function OnKeyDown_Float(obj) {   
						if (event.ctrlKey || event.shiftKey || event.altKey)   {
							SetEventPass();
							return true;
						}
												
						// 합격수량 TextBox 객체의 value로 배열을 만든다.
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
					
					
					function ValidateFloat(a_sValue)	{	
						var s_FieldValue = new String(a_sValue);
						
						if (isNaN(s_FieldValue))
							return false;
							
						return true;
					}
					
//////////////////////////////////////////////////////////////////////////////////////////
//															Currency															//
//////////////////////////////////////////////////////////////////////////////////////////

					
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
					
					
					// 그리드의 행 클릭시
					function DataGrid1_AfterSelectChangeHandler(gridName, id) 
					{
							var boolMonthClosing = "<%# MonthClosing() %>".toLowerCase();
							var frm = document.QualityInspectionRegister;
							// 초기화
							frm.txtSuccessQuantity.value = "";
							frm.txtIncongruityQuantity.value = "";
							frm.txtIncongruityDetailContent_Result.value = "";
							
							frm.ddlIncongruityDecision.selectedIndex = 0;
							frm.ddlIncongruityCause.selectedIndex = 0;
							frm.ddlIncongruityPhenomenon.selectedIndex = 0;
							frm.txtItemNumber_Result.value = document.getElementById(id).children[1].innerText;
							frm.txtDrawingNumber_Result.value = document.getElementById(id).children[2].innerText;
							frm.txtItemName_Result.value = document.getElementById(id).children[3].innerText;
							
							// 거래처명
							frm.txtCustomerName_Result.value = document.getElementById(id).children[6].innerText;
							
							// 시작공정순서, 공정명
							var value = document.getElementById(id).children[7].innerText;
							value += "(" + document.getElementById(id).children[8].innerText + ")";
							document.QualityInspectionRegister.txtBeginProcess_Result.value = value
							
							// 종료공정순서, 공정명
							value = document.getElementById(id).children[9].innerText;
							value += "(" + document.getElementById(id).children[10].innerText +")";
							frm.txtEndProcess_Result.value = value
							frm.hdseq.value = document.getElementById(id).children[9].innerText;
							
							// 품목단가
							frm.txtPrice.value = document.getElementById(id).children[16].innerText;
							// 사업자등록번호
							frm.txtBusinessRegistrationNum_Result.value = document.getElementById(id).children[11].innerText;
							// 의뢰수량
							frm.txtRequestQuantity_Result.value = document.getElementById(id).children[13].innerText;
							
							frm.hdRequestQuantity.value = frm.txtRequestQuantity_Result.value;
							
							frm.txtRequestQuantity_Result.value = document.getElementById(id).children[13].innerText;
							
							
							
							//매입년도선택
							for(var i=0;i<frm.ddlYear.options.length;i++)
							{
								if(frm.ddlYear.options[i].value == document.getElementById(id).children[23].innerText)
								{
									frm.ddlYear.options[i].selected=true
								}
							}
							//매입월 선택
							for(var i=0;i<frm.ddlMon.options.length;i++)
							{
								if(frm.ddlMon.options[i].value == document.getElementById(id).children[24].innerText)
								{
									frm.ddlMon.options[i].selected=true
								}
							}
							
						
						
						if ( boolMonthClosing == "true")
						{
							// 검사자 이름
							frm.txtPerson_Result.value = '<%=Session["UserName"].ToString()%>';
							
							frm.txtSuccessQuantity.disabled = false;
							frm.txtSuccessQuantity.focus();
							frm.btnRegist.disabled = false;
							
							frm.hidden_DataValue.value = id;	// 그리드id 값을 보존
						}
						else
						{
							frm.txtSuccessQuantity.disabled = true;
							document.all("divMonthClosing").innerText = "월 마감이 되어 등록할 수 없습니다.";
						}	
					}
		function DataGrid1_DblClickHandler(gridName, cellId){
			//Add code to handle your event here.
			QualityInspectionRegister.hdIndex.value = cellId;
			alert(QualityInspectionRegister.hdIndex.value);
		}
		
//-->
		</script>
	</HEAD>
	<body bottomMargin="0" bgColor="#f7f6f6" leftMargin="0" topMargin="0" onload="Reset();"
		rightMargin="0" ms_positioning="GridLayout">
		<form id="QualityInspectionRegister" method="post" runat="server">
			<TABLE id="Table3" style="Z-INDEX: 100; POSITION: absolute; TOP: 10px; LEFT: 10px" height="550"
				cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD style="HEIGHT: 20px" width="2" height="20"></TD>
					<TD vAlign="middle">
						<FIELDSET style="BORDER-BOTTOM: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 804px; HEIGHT: 20.69%; BORDER-TOP: dimgray 2px solid; BORDER-RIGHT: dimgray 2px solid; 90BORDER-RIGHT: black 1px solid"
							align="middle"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색조건 ]
							</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
								<TR>
									<TD style="WIDTH: 693px" align="left" colSpan="13" height="30"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30">거래처명&nbsp;
									</TD>
									<TD align="left" width="130" height="30"><igcmbo:webcombo id="wcCompanyName" runat="server" Editable="True" DropImageXP2="/ig_common/WebGrid3/ig_cmboDown2.bmp"
											DropImageXP1="/ig_common/WebGrid3/ig_cmboDown1.bmp" Version="3.00" BorderWidth="1px" ForeColor="Black" BorderColor="Gray" SelBackColor="10, 36, 106"
											SelectedIndex="-1" SelForeColor="White" BackColor="#EEEEE9" BorderStyle="Solid" Width="120px" Height="20px">
											<DropDownLayout DropdownWidth="365px" BorderCollapse="Separate" RowSelectors="No" RowHeightDefault="20px"
												HeaderClickAction="Select" DropdownHeight="350px" TableLayout="Fixed" StationaryMargins="Header">
												<RowStyle BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="White">
													<Padding Left="5px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyle>
												<SelectedRowStyle ForeColor="White" BackColor="#0A246A"></SelectedRowStyle>
												<HeaderStyle BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyle>
												<FrameStyle Width="100%" Cursor="Default" BorderWidth="2px" Font-Size="10pt" Font-Names="Verdana"
													BorderStyle="Ridge" BackColor="Silver" Height="100%"></FrameStyle>
											</DropDownLayout>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igcmbo:webcombo></TD>
									<TD align="right" width="70" height="30">의뢰일&nbsp;
									</TD>
									<TD align="left" width="100" height="30"><igsch:webdatechooser id="txtStartDate" runat="server" BorderColor="Gray" BackColor="#EEEEE9" BorderStyle="Solid"
											Width="100px" Height="20px" NullDateLabel=" " Text="Null">
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
									<TD align="center" width="15" height="30">~</TD>
									<TD align="left" colSpan="7" height="30"><igsch:webdatechooser id="txtEndDate" runat="server" BorderColor="Gray" BackColor="#EEEEE9" BorderStyle="Solid"
											Width="100px" Height="20px" NullDateLabel=" " Text="Null">
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
									<TD align="right" width="315" height="30"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
											type="button" value="초기화" name="btnReset">&nbsp;
										<asp:button id="btnSearch" runat="server" Width="65px" Height="20px" Text="검  색" Font-Size="10pt"
											CausesValidation="False"></asp:button>&nbsp;&nbsp;</TD>
								</TR>
								<TR>
									<TD align="right" width="70" colSpan="13" height="8"></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 269px" width="20" height="269"></TD>
					<TD vAlign="middle"><BR>
						<FIELDSET style="BORDER-BOTTOM: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 804px; HEIGHT: 20.69%; BORDER-TOP: dimgray 2px solid; BORDER-RIGHT: dimgray 2px solid; 90BORDER-RIGHT: black 1px solid"
							align="absBottom"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색결과 ]
							</LEGEND>
							<igtbl:ultrawebgrid id="DataGrid1" runat="server" Width="800px" Height="230px">
								<DisplayLayout ColFootersVisibleDefault="Yes" StationaryMargins="Header" AutoGenerateColumns="False"
									AllowSortingDefault="OnClient" RowHeightDefault="20px" RowSizingDefault="Free" Version="3.00"
									SelectTypeRowDefault="Single" HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate"
									AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="DataGrid1" TableLayout="Fixed"
									CellClickActionDefault="RowSelect">
									<AddNewBox>
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

										</Style>
									</AddNewBox>
									<Pager PageSize="3" Alignment="Center">
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

										</Style>
									</Pager>
									<HeaderStyleDefault Cursor="Hand" VerticalAlign="Middle" BorderColor="DimGray" BorderStyle="Solid" HorizontalAlign="Center"
										ForeColor="Black" BackColor="LightGray" Height="25px">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</HeaderStyleDefault>
									<FrameStyle Width="800px" Cursor="Default" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림"
										BorderColor="DimGray" BorderStyle="Solid" BackColor="Silver" Height="230px"></FrameStyle>
									<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</FooterStyleDefault>
									<ClientSideEvents AfterSelectChangeHandler="DataGrid1_AfterSelectChangeHandler" DblClickHandler="DataGrid1_DblClickHandler"></ClientSideEvents>
									<ActivationObject BorderStyle="Groove" BorderWidth="2px" BorderColor="White"></ActivationObject>
									<RowExpAreaStyleDefault BackColor="White"></RowExpAreaStyleDefault>
									<SelectedHeaderStyleDefault BackColor="White"></SelectedHeaderStyleDefault>
									<SelectedRowStyleDefault Cursor="Hand" BorderWidth="1px" Font-Bold="True" BorderStyle="Groove" ForeColor="Linen"
										BackColor="Navy"></SelectedRowStyleDefault>
									<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
									<RowStyleDefault Cursor="Hand" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="Gray"
										BorderStyle="Solid" ForeColor="Black" BackColor="#EBEFF6">
										<BorderDetails ColorTop="Gray" WidthLeft="0px" WidthTop="0px" ColorLeft="Gray"></BorderDetails>
									</RowStyleDefault>
								</DisplayLayout>
								<Bands>
									<igtbl:UltraGridBand HeaderClickAction="SortSingle" ColHeadersVisible="Yes">
										<Columns>
											<igtbl:UltraGridColumn HeaderText="구분" Key="aaa" Width="40px" HeaderClickAction="SortMulti" BaseColumnName="historysection1"
												AllowResize="Free">
												<SelectedHeaderStyle BackColor="White"></SelectedHeaderStyle>
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle Height="25px"></HeaderStyle>
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
											<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="BusinessRegistrationNum">
												<CellStyle>
													<Padding Left="5px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="현재고" Key="NowStockQuantity" Width="65px" Format="###,###,###.##" BaseColumnName="NowStockQuantity">
												<CellStyle HorizontalAlign="Right">
													<Padding Right="4px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="의뢰수량" Key="RequestQuantity" Width="65px" Format="###,###,##0.00" HeaderClickAction="SortMulti"
												BaseColumnName="RequestQuantity" FooterTotal="Sum">
												<CellStyle HorizontalAlign="Right">
													<Padding Right="5px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Width="100px" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="RegistrationDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" Width="60px" HeaderClickAction="SortMulti"
												BaseColumnName="RegistrationPerson">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Width="90px" Format="###,###,##0.00" HeaderClickAction="SortMulti"
												BaseColumnName="ApplyUnitCost" FooterTotal="Sum">
												<CellStyle HorizontalAlign="Right">
													<Padding Right="5px"></Padding>
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
											<igtbl:UltraGridColumn HeaderText="시작공정코드" Key="시작공정코드" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="시작공정코드"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="종료공정코드" Key="종료공정코드" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="종료공정코드"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="매입년도" Key="Year" Width="60px" BaseColumnName="Year"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="매입월" Key="Month" Width="50px" BaseColumnName="Month"></igtbl:UltraGridColumn>
										</Columns>
									</igtbl:UltraGridBand>
								</Bands>
							</igtbl:ultrawebgrid></FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 205px" width="20"></TD>
					<TD style="HEIGHT: 205px" vAlign="middle"><br>
						<FIELDSET style="BORDER-BOTTOM: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 804px; HEIGHT: 20.69%; BORDER-TOP: dimgray 2px solid; BORDER-RIGHT: dimgray 2px solid; 90BORDER-RIGHT: black 1px solid"
							align="absBottom"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 입력 ]
							</LEGEND>
							<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="800" border="0">
								<tr>
									<td height="10"></td>
									<td style="WIDTH: 117px" height="10"></td>
								</tr>
								<TR>
									<TD align="right" width="120" height="25">품목번호&nbsp;</TD>
									<TD style="WIDTH: 117px" width="117" height="25"><INPUT id="Text1" style="BORDER-BOTTOM: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BACKGROUND-COLOR: #eeeee9; BORDER-TOP: dimgray 1px solid; FONT-WEIGHT: normal; BORDER-RIGHT: dimgray 1px solid"
											readOnly size="14" name="txtItemNumber_Result"></TD>
									<TD style="WIDTH: 127px; FONT-SIZE: 8pt" align="right" width="127" height="25">시작공정순서(명)&nbsp;</TD>
									<TD style="WIDTH: 115px" width="115" height="25"><INPUT style="BORDER-BOTTOM: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BACKGROUND-COLOR: #eeeee9; BORDER-TOP: dimgray 1px solid; FONT-WEIGHT: normal; BORDER-RIGHT: dimgray 1px solid"
											readOnly size="14" name="txtBeginProcess_Result"></TD>
									<TD style="FONT-SIZE: 8pt" align="right" width="90" height="25">적합품수량&nbsp;</TD>
									<TD style="WIDTH: 117px" width="117" height="25"><asp:textbox id="txtSuccessQuantity" style="TEXT-ALIGN: right" runat="server" BorderWidth="1px"
											BorderColor="DimGray" BackColor="#EEEEE9" BorderStyle="Solid" Width="110px" Height="20px" Enabled="False"></asp:textbox></TD>
									<TD style="WIDTH: 117px" align="right" width="117" height="25">부적합 수량&nbsp;</TD>
									<TD width="120" height="25"><asp:textbox id="txtIncongruityQuantity" style="TEXT-ALIGN: right" runat="server" BorderWidth="1px"
											BorderColor="DimGray" BackColor="#EEEEE9" BorderStyle="Solid" Width="110px" Height="20px" EnableViewState="False"
											ReadOnly="True"></asp:textbox></TD>
								</TR>
								<TR>
									<TD align="right" width="110" height="25">도면번호&nbsp;</TD>
									<TD style="WIDTH: 117px" height="25"><INPUT id="Text2" style="BORDER-BOTTOM: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BACKGROUND-COLOR: #eeeee9; BORDER-TOP: dimgray 1px solid; FONT-WEIGHT: normal; BORDER-RIGHT: dimgray 1px solid"
											readOnly size="14" name="txtDrawingNumber_Result"></TD>
									<TD style="WIDTH: 127px; FONT-SIZE: 8pt" align="right" height="25">종료공정순서(명)&nbsp;</TD>
									<TD style="WIDTH: 115px" height="25"><INPUT style="BORDER-BOTTOM: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BACKGROUND-COLOR: #eeeee9; BORDER-TOP: dimgray 1px solid; FONT-WEIGHT: normal; BORDER-RIGHT: dimgray 1px solid"
											readOnly size="14" name="txtEndProcess_Result"></TD>
									<TD style="FONT-SIZE: 8pt" align="right" height="25">부적합현상&nbsp;</TD>
									<TD style="WIDTH: 117px" height="25"><asp:dropdownlist id="ddlIncongruityPhenomenon" runat="server" BackColor="#EEEEE9" Width="110px" Height="20px">
											<asp:ListItem Value="0" Selected="True">- 선택하세요 -</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD style="WIDTH: 117px" align="right" height="25">검사판정&nbsp;</TD>
									<TD height="25"><asp:dropdownlist id="ddlIncongruityDecision" runat="server" BackColor="#EEEEE9" Width="110px" Height="20px">
											<asp:ListItem Value="0" Selected="True">- 선택하세요 -</asp:ListItem>
										</asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD align="right" width="110" height="25">품목명&nbsp;</TD>
									<TD style="WIDTH: 117px" height="25"><INPUT id="Text3" style="BORDER-BOTTOM: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BACKGROUND-COLOR: #eeeee9; BORDER-TOP: dimgray 1px solid; FONT-WEIGHT: normal; BORDER-RIGHT: dimgray 1px solid"
											readOnly size="14" name="txtItemName_Result"></TD>
									<TD style="WIDTH: 127px" align="right" height="25">의뢰수량 &nbsp;</TD>
									<TD style="WIDTH: 115px" height="25"><INPUT style="BORDER-BOTTOM: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BACKGROUND-COLOR: #eeeee9; BORDER-TOP: dimgray 1px solid; FONT-WEIGHT: normal; BORDER-RIGHT: dimgray 1px solid"
											readOnly size="14" name="txtRequestQuantity_Result"></TD>
									<TD style="FONT-SIZE: 8pt" align="right" height="25">부적합원인&nbsp;</TD>
									<TD style="WIDTH: 117px" height="25"><asp:dropdownlist id="ddlIncongruityCause" runat="server" BackColor="#EEEEE9" Width="110px" Height="20px">
											<asp:ListItem Value="0" Selected="True">- 선택하세요 -</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD style="WIDTH: 117px; FONT-SIZE: 8pt" align="right" height="25">부적합세부내용&nbsp;</TD>
									<TD height="25"><asp:textbox id="txtIncongruityDetailContent_Result" runat="server" BorderWidth="1px" BorderColor="DimGray"
											BackColor="#EEEEE9" BorderStyle="Solid" Width="110px" Height="20px" EnableViewState="False"></asp:textbox></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 8pt" align="right" width="110" height="25">사업자등록번호&nbsp;</TD>
									<TD style="WIDTH: 117px" height="25"><INPUT style="BORDER-BOTTOM: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BACKGROUND-COLOR: #eeeee9; BORDER-TOP: dimgray 1px solid; FONT-WEIGHT: normal; BORDER-RIGHT: dimgray 1px solid"
											readOnly size="14" name="txtBusinessRegistrationNum_Result"></TD>
									<TD style="WIDTH: 127px" align="right" height="25">단가 &nbsp;</TD>
									<TD style="WIDTH: 115px" height="25"><INPUT style="BORDER-BOTTOM: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BACKGROUND-COLOR: #eeeee9; BORDER-TOP: dimgray 1px solid; FONT-WEIGHT: normal; BORDER-RIGHT: dimgray 1px solid"
											readOnly size="14" name="txtPrice"></TD>
									<TD align="right" height="25">검사자&nbsp;</TD>
									<TD style="WIDTH: 117px" height="25"><asp:textbox id="txtPerson_Result" runat="server" BorderWidth="1px" BorderColor="DimGray" BackColor="#EEEEE9"
											BorderStyle="Solid" Width="110px" Height="20px"></asp:textbox></TD>
									<TD style="WIDTH: 117px" align="right" height="25">부적합 금액&nbsp;</TD>
									<TD height="25"><asp:textbox id="txtIncongruityMoney" style="TEXT-ALIGN: right" runat="server" BorderWidth="1px"
											BorderColor="DimGray" BackColor="#EEEEE9" BorderStyle="Solid" Width="110px" Height="20px" EnableViewState="False"></asp:textbox></TD>
								</TR>
								<TR>
									<TD align="right" width="110" height="25">거래처 명&nbsp;</TD>
									<TD style="WIDTH: 117px" height="25"><INPUT style="BORDER-BOTTOM: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BACKGROUND-COLOR: #eeeee9; BORDER-TOP: dimgray 1px solid; FONT-WEIGHT: normal; BORDER-RIGHT: dimgray 1px solid"
											readOnly size="14" name="txtCustomerName_Result"></TD>
									<TD style="WIDTH: 127px" align="right" height="25">입고일&nbsp;&nbsp;</TD>
									<TD style="WIDTH: 115px" height="25"><igsch:webdatechooser id="wdcQualityInspectionCompleteDate" runat="server" BorderColor="Gray" BackColor="#EEEEE9"
											BorderStyle="Solid" Width="100px" Height="20px" NullDateLabel=" " Text="Null">
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
									<TD height="25" align="right">매입년도&nbsp;</TD>
									<TD style="WIDTH: 117px" height="25">
										<asp:dropdownlist id="ddlYear" runat="server" Width="55px" BackColor="#EEEEE9">
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
										</asp:dropdownlist></TD>
									<TD style="WIDTH: 117px" height="25" align="right">매입월&nbsp;</TD>
									<TD align="left" height="25">
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
										</asp:dropdownlist>&nbsp;&nbsp;&nbsp;</TD>
								</TR>
								<TR>
									<TD width="110" height="25"></TD>
									<TD style="WIDTH: 117px" height="25"><INPUT id="Hidden1" style="WIDTH: 64px; HEIGHT: 21px" type="hidden" size="5" name="hidden_DataValue"></TD>
									<TD style="WIDTH: 127px" height="25"><INPUT id="hdRequestQuantity" style="WIDTH: 26px; HEIGHT: 21px" type="hidden" size="1"
											value="0" name="hdRequestQuantity" runat="server"> <INPUT id="hdIndex" style="WIDTH: 26px; HEIGHT: 21px" type="hidden" size="1" value="0"
											runat="server"></TD>
									<TD style="WIDTH: 115px" height="25"><INPUT id="hdseq" style="WIDTH: 26px; HEIGHT: 21px" type="hidden" size="1" value="0" name="Hidden2"
											runat="server"></TD>
									<TD height="25"></TD>
									<TD style="WIDTH: 232px" align="right" colSpan="2" height="25">
										<DIV id="divMonthClosing" style="DISPLAY: inline; COLOR: #990033; FONT-WEIGHT: bold"
											ms_positioning="FlowLayout"></DIV>
									</TD>
									<TD align="right" height="25"><asp:button id="btnRegist" runat="server" Width="64px" Height="20px" Text="등록" Enabled="False"></asp:button>&nbsp;&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
