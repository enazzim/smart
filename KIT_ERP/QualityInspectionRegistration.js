					// 등록버튼을 누른경우 폼을 체크
					function RegistCheck() {
						if(QualityInspectionRegister.txtSuccessQuantity.value != "" && 
						QualityInspectionRegister.txtRequestQuantity_Result.value != "" && QualityInspectionRegister.txtIncongruityQuantity.value != 0)	{
							var frmSq = Number(QualityInspectionRegister.txtSuccessQuantity.value);
							var frmRq = Number(QualityInspectionRegister.txtRequestQuantity_Result.value);
							if(frmSq < frmRq)  { // 부적합품이 발생하였다면.
								if(QualityInspectionRegister.ddlIncongruityDecision.selectedIndex == 0) 	{
									alert("부적합 항목에 대한 검사판정을 선택하세요."); return false;
								} else	if( QualityInspectionRegister.ddlIncongruityCause.selectedIndex == 0 ) {
									alert("부적합 항목에 대한 부적합 원인을 선택하세요."); return false;
								} else	if( QualityInspectionRegister.ddlIncongruityPhenomenon.selectedIndex == 0 )	{
									alert("부적합 항목에 대한 부적합 현상을 선택하세요."); return false;
								}
							}
						} else if(QualityInspectionRegister.txtIncongruityQuantity.value == 0 && 
						(QualityInspectionRegister.txtSuccessQuantity.value == QualityInspectionRegister.txtRequestQuantity_Result.value) )	{
							if(QualityInspectionRegister.ddlIncongruityDecision.selectedIndex != 0)  {
								alert("전수합격인 경우는 검사판정 항목을 선택할 수 없습니다.");		return false;
							} else if(QualityInspectionRegister.ddlIncongruityCause.selectedIndex != 0)  {
								alert("전수합격인 경우는 부적합 원인 항목을 선택할 수 없습니다.");	return false;
							} else if( QualityInspectionRegister.ddlIncongruityPhenomenon.selectedIndex != 0 )  {
								alert("전수합격인 경우는 부적합 현상 항목을 선택할 수 없습니다.");	return false;
							} else if( QualityInspectionRegister.txtIncongruityDetailContent_Result.value != "" )  {
								alert("전수합격인 경우는 부적합 세부내용 항목을 기입할 수 없습니다.");	return false;
							} else if ( QualityInspectionRegister.txtIncongruityMoney.value != "" )   {
								alert("전수합격인 경우는 부적합 금액을 기입할 수 없습니다.");	return false;
							}
						}		
						if(confirm(" 검사결과를 등록 하시겠습니까? ") )	return true;		else		return false;		
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

										if(value < 0 )	{
											alert("- 합격수량이 의뢰된 수량보다 많습니다");
											sqTxt.value = ""//sqTxt.value.substring(0, sqTxt.value.length-1);
											sqTxt.focus();
										}else{
											txtIncongruityQuantity.value = MakeCommaSeparate((Math.round((value * 10000)) / 10000));
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
					function ResettxtBox(){
						
						var frm = document.QualityInspectionRegister;
						frm.txtItemNumber.value="";
						frm.txtDrawingNumber.value="";
						frm.txtItemName.value="";
						frm.txtCustomerName.value="";
						frm.txtStartDate_input.value="";
						frm.txtEndDate_input.value="";
						frm.hidden_businessCompanyNum.value="";
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
					
					function DataGrid1_AfterSelectChangeHandler(gridName, id) {
						// 초기화
						document.QualityInspectionRegister.txtSuccessQuantity.value = "";
						document.QualityInspectionRegister.txtIncongruityQuantity.value = "";
						document.QualityInspectionRegister.txtIncongruityDetailContent_Result.value = "";
						
						document.QualityInspectionRegister.ddlIncongruityDecision.selectedIndex = 0;
						document.QualityInspectionRegister.ddlIncongruityCause.selectedIndex = 0;
						document.QualityInspectionRegister.ddlIncongruityPhenomenon.selectedIndex = 0;
						//document.QualityInspectionRegister.ddlInspectionPost.selectedIndex = 0;
						
						document.QualityInspectionRegister.txtItemNumber_Result.value = document.getElementById(id).children[1].innerText;
						document.QualityInspectionRegister.txtDrawingNumber_Result.value = document.getElementById(id).children[2].innerText;
						document.QualityInspectionRegister.txtItemName_Result.value = document.getElementById(id).children[3].innerText;
						// 거래처명
						document.QualityInspectionRegister.txtCustomerName_Result.value = document.getElementById(id).children[4].innerText;
						
						// 공정명
						var value = document.getElementById(id).children[5].innerText;
						value += "(" + document.getElementById(id).children[6].innerText + ")";
						document.QualityInspectionRegister.txtBeginProcess_Result.value = value
						
						value = document.getElementById(id).children[7].innerText;
						value += "(" + document.getElementById(id).children[8].innerText +")";
						document.QualityInspectionRegister.txtEndProcess_Result.value = value
						
						// 품목단가
						document.QualityInspectionRegister.txtPrice.value = document.getElementById(id).children[14].innerText;
						
						// 사업자등록번호
						document.QualityInspectionRegister.txtBusinessRegistrationNum_Result.value = document.getElementById(id).children[10].innerText;
						// 의뢰수량
						document.QualityInspectionRegister.txtRequestQuantity_Result.value = document.getElementById(id).children[11].innerText;
						
						document.QualityInspectionRegister.txtSuccessQuantity.disabled = false;
						document.QualityInspectionRegister.txtSuccessQuantity.focus();
						document.QualityInspectionRegister.btnRegist.disabled = false;
						
						// Key
						document.QualityInspectionRegister.hidden_key.value = document.getElementById(id).children[16].innerText;
						// 원장도면 - (구매납품, 외주납품, 작업일보)
						document.QualityInspectionRegister.hidden_historySection1.value = document.getElementById(id).children[0].innerText;
						// 도면된 원장1의 인덱스
						document.QualityInspectionRegister.hidden_historyIndex1.value = document.getElementById(id).children[12].innerText;
						// 원장도면 2 - (구매발주원장, 외주발주원장)
						document.QualityInspectionRegister.hidden_historySection2.value = document.getElementById(id).children[13].innerText;
						// 도면된 원장2 의 인덱스
						document.QualityInspectionRegister.hidden_historyIndex2.value = document.getElementById(id).children[14].innerText;
						// 공정번호
						document.QualityInspectionRegister.hidden_ProgressNum.value = document.getElementById(id).children[6].innerText;
					}
