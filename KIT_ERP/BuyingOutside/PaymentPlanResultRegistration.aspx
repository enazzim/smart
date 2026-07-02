<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="PaymentPlanResultRegistration.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BuyingOutside.PaymentPlanResultRegistraton" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v1.1" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../MessageWindows.js"></script>
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		
		function RemoveComma(Ovalue)
		{
			var array = new String(Ovalue).split(",");
			var Rvalue = new String();
			for ( var i = 0 ; i < array.length ; i++)	{
				Rvalue += array[i];
			}
			return Rvalue;
		}
		
		
		function Comma()
		{
			document.Form1.txtEvidence.value = 	MakeCommaSeparate(document.Form1.txtEvidence.value);			
		}
		
		function Process()	
		{
			var Tax = Number(RemoveComma(document.Form1.BillMoney.value))/10;
			var Tax1 = (Math.round(Tax,1));
			document.Form1.PaymentMoney.value = MakeCommaSeparate((Math.round((Tax1 * 10000)) / 10000));
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
		
		function DoPost1()
		{
			__doPostBack('LinkButton1','');
		}
		
		/*****************************************************************************************************************************************************************/
		/*그리드를 선택했을때 입력란 부분으로 그리드의 내용을 보여주는 함수                                                                                              */
		/*****************************************************************************************************************************************************************/
		function uwgP_HT_DblClickHandler(gridName, cellId){
			
			var objChooser0 = igdrp_getComboById("PaymentDate")//지급일 WebChooser
			var objChooser1 = igdrp_getComboById("SettleDate1")//결제일자 WebChooser
			var objChooser2 = igdrp_getComboById("SettleDate2")//결제일자 WebChooser
			var objChooser3 = igdrp_getComboById("SettleDate3")//결제일자 WebChooser
			
			var objGrid = igtbl_getActiveRow(gridName)//현재 선택되어진 객체의 행의 가져옴
			
			var objSettleDropDown = document.Form1.SettleDropDown;//결제방법 DropDownList
			var objBankingDropdown1 = document.Form1.BankingDropdown1;//결제은행1 DropDownList
			var objBankingDropdown2 = document.Form1.BankingDropdown2;//결제은행2 DropDownList
			var objBankingDropdown3 = document.Form1.BankingDropdown3;//결제은행3 DropDownList
			
			var row = igtbl_getRowById(cellId);
			
			//row.getCellFromKey("CompanyName").getValue();//
			//row.getCellFromKey("BusinessRegistrationNum").getValue();
			document.Form1.CSC1_txtCompanyName.value = row.getCellFromKey("CompanyName").getValue();
			document.Form1.CSC1_txtBusinessRegistrationNum.value = row.getCellFromKey("BusinessRegistrationNum").getValue();
						
			objChooser0.setValue(objGrid.getCellFromKey("PaymentDate").getValue());//그리드에서 선택한 지급일을 넣음
			
			document.Form1.hdYear.value = objChooser0.getValue().getFullYear();
			document.Form1.hdMon.value = objChooser0.getValue().getMonth()+1;
			
						
			//그리드에서 선택한 결제방법을 넣음
			for(var i=0;i<objSettleDropDown.options.length;i++)
			{
				if(objSettleDropDown.options[i].innerText == row.getCellFromKey("DecisionMethod").getValue())
				{
					objSettleDropDown.options[i].selected=true
				}
			}
			
			document.Form1.BillMoney.value = row.getCellFromKey("ItemPaymentCost").getValue();//그리드에서 선택한 수금액(물품대)을 넣음		
			document.Form1.lbOldCost.value = row.getCellFromKey("ItemPaymentCost").getValue();
			
			document.Form1.PaymentMoney.value = row.getCellFromKey("SupplementaryValueTaxPaymentCost").getValue();//그리드에서 선택한 지급액(부가세)을 넣음		
			
			//그리드에서 선택한 증거자료를 넣음		
			//if(row.getCellFromKey("ProofData").getValue() == null)
			//	document.Form1.txtEvidence.value =="";
			//else
			//	document.Form1.txtEvidence.value = row.getCellFromKey("ProofData").getValue();
			
			//그리드에서 선택한 어음번호1을 넣음		
			if(row.getCellFromKey("BillNum1").getValue() == null)
				document.Form1.txtDraftNum1.value ="";
			else
				document.Form1.txtDraftNum1.value = row.getCellFromKey("BillNum1").getValue();
			
			
			//그리드에서 선택한 결제일자1을 넣음
			if(objGrid.getCellFromKey("BillPaymentDate1").getValue() == "")
				objChooser1.setValue(null);
			else
				objChooser1.setValue(objGrid.getCellFromKey("BillPaymentDate1").getValue());
				
				
			//그리드에서 선택한 결제은행1을 넣음
			for(var i=0;i<objBankingDropdown1.options.length;i++)
			{
				if(objBankingDropdown1.options[i].innerText == row.getCellFromKey("BankName1").getValue())
				{
					objBankingDropdown1.options[i].selected=true
				}
			}
			
			//그리드에서 선택한 어음번호2을 넣음		
			if(row.getCellFromKey("BillNum2").getValue() == null)
				document.Form1.txtDraftNum2.value ="";
			else
				document.Form1.txtDraftNum2.value = row.getCellFromKey("BillNum2").getValue();
			
			//그리드에서 선택한 결제일자2을 넣음
			if(objGrid.getCellFromKey("BillPaymentDate2").getValue() == "")
				objChooser2.setValue(null);
			else
				objChooser2.setValue(objGrid.getCellFromKey("BillPaymentDate2").getValue());
			
			
			
			
			//그리드에서 선택한 결제은행2을 넣음
			for(var i=0;i<objBankingDropdown2.options.length;i++)
			{
				if(objBankingDropdown2.options[i].innerText == row.getCellFromKey("BankName2").getValue())
				{
					objBankingDropdown2.options[i].selected=true
				}
			}
			
			//그리드에서 선택한 어음번호3을 넣음		
			if(row.getCellFromKey("BillNum3").getValue() == null)
				document.Form1.txtDraftNum3.value ="";
			else
				document.Form1.txtDraftNum3.value = row.getCellFromKey("BillNum3").getValue();
			
			//그리드에서 선택한 결제일자3을 넣음
			if(objGrid.getCellFromKey("BillPaymentDate3").getValue() == "")
				objChooser3.setValue(null);
			else
				objChooser3.setValue(objGrid.getCellFromKey("BillPaymentDate3").getValue());
						
				
			//그리드에서 선택한 결제은행3을 넣음
			for(var i=0;i<objBankingDropdown3.options.length;i++)
			{
				if(objBankingDropdown3.options[i].innerText == row.getCellFromKey("BankName3").getValue())
				{
					objBankingDropdown3.options[i].selected=true
				}
			}
	
			//그리드에서 선택한 인덱스를 가지고 있는 히든필드
			document.Form1.hdindex.value = row.getCellFromKey("PaymentHistoryIndex").getValue();
			
			
			document.Form1.btnEdit.disabled = false;
		    document.Form1.btnDelete.disabled = false;
		    document.Form1.btnInput.disabled = true;
		}
		function wcbCompany_AfterSelectChange(webComboId){
				var obj = igcmbo_getComboById(webComboId);
			document.Form1.txtEvidence.value = obj.getGrid().Rows.getRow(obj.getSelectedIndex()).getCellFromKey("UnPaymentMoney").getValue();
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
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" MS_POSITIONING="GridLayout"
		bgColor="#f7f6f6" XMLNS:igtxt="http://schemas.infragistics.com/ASPNET/WebControls/WebDataInput">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; WIDTH: 800px; POSITION: absolute; TOP: 10px; HEIGHT: 550px"
				cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD style="HEIGHT: 185px" width="20" height="185"><FONT face="굴림"></FONT></TD>
					<TD style="HEIGHT: 185px" height="185">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; LEFT: 10px; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; TOP: 10px; HEIGHT: 150px"
							align="top"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 입력 ]
							</LEGEND>
							<TABLE style="WIDTH: 800px; HEIGHT: 42px" cellSpacing="0" cellPadding="0" width="800">
								<TR>
									<TD style="WIDTH: 366px; HEIGHT: 26px" align="left" colSpan="2"><FONT face="굴림" size="2"></FONT><FONT face="굴림">
											<uc1:CompanySearchControl id="CSC1" runat="server"></uc1:CompanySearchControl></FONT></TD>
									<TD style="WIDTH: 126px; HEIGHT: 26px" align="right"><FONT face="굴림" size="2">지급일&nbsp;</FONT></TD>
									<TD style="WIDTH: 3px; HEIGHT: 26px"><igsch:webdatechooser id="PaymentDate" runat="server" Width="110px" Font-Size="9pt" Text=" " NullDateLabel=" "
											BackColor="#EEEEE9" Height="20px" BorderStyle="Solid" BorderColor="DimGray">
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
									<TD style="WIDTH: 196px; HEIGHT: 26px" align="right"><FONT face="굴림" size="2">결재방법&nbsp;</FONT></TD>
									<TD style="HEIGHT: 26px" align="left"><asp:dropdownlist id="SettleDropDown" runat="server" Width="110px" Font-Size="9pt" BackColor="#EEEEE9"
											Height="20px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 120px; HEIGHT: 26px" align="right"><FONT face="굴림" size="2">지급액(물품대)&nbsp;</FONT></TD>
									<TD style="WIDTH: 246px; HEIGHT: 26px"><asp:textbox id="BillMoney" runat="server" Width="110px" Font-Size="9pt" BorderStyle="Solid"
											style="TEXT-ALIGN: right" BackColor="#EEEEE9" Height="20px" BorderWidth="1px" BorderColor="DimGray">0</asp:textbox></TD>
									<TD style="WIDTH: 126px; HEIGHT: 26px" align="right"><FONT face="굴림" size="2">지급액(부가세)&nbsp;</FONT></TD>
									<TD style="WIDTH: 3px; HEIGHT: 26px"><asp:textbox id="PaymentMoney" runat="server" Width="110px" BorderStyle="Solid" BackColor="#EEEEE9"
											style="TEXT-ALIGN: right" Height="20px" Font-Size="9pt" BorderWidth="1px" BorderColor="DimGray">0</asp:textbox></TD>
									<TD style="WIDTH: 196px; HEIGHT: 26px" align="right"><FONT face="굴림" size="2">미지급액&nbsp;</FONT></TD>
									<TD style="HEIGHT: 26px" align="right">
										<igtxt:webnumericedit id="WebNumericEdit1" runat="server" BorderColor="DimGray" BorderStyle="Solid" Height="20px"
											BackColor="#EEEEE9" Font-Size="10pt" Width="110px" BorderWidth="1px" ReadOnly="True" ValueText="0">
											<Padding Right="4px"></Padding>
										</igtxt:webnumericedit></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 120px; HEIGHT: 4px" align="right"><FONT face="굴림" size="2">어음번호1&nbsp;</FONT></TD>
									<TD style="WIDTH: 246px; HEIGHT: 4px"><asp:textbox id="txtDraftNum1" runat="server" Width="110px" Font-Size="9pt" BorderStyle="Solid"
											BackColor="#EEEEE9" Height="20px" BorderWidth="1px" BorderColor="DimGray"></asp:textbox></TD>
									<TD style="WIDTH: 126px; HEIGHT: 4px" align="right"><FONT face="굴림" size="2">결제일자1&nbsp;</FONT></TD>
									<TD style="WIDTH: 3px; HEIGHT: 4px"><igsch:webdatechooser id="SettleDate1" runat="server" Width="110px" Font-Size="9pt" Text=" " NullDateLabel=" "
											BackColor="#EEEEE9" Height="20px" BorderStyle="Solid" BorderColor="DimGray">
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
									<TD style="WIDTH: 196px; HEIGHT: 4px" align="right"><FONT face="굴림" size="2">금융기관1&nbsp;</FONT></TD>
									<TD style="HEIGHT: 4px" align="left"><asp:dropdownlist id="BankingDropdown1" runat="server" Width="110px" Font-Size="9pt" BackColor="#EEEEE9"
											Height="20px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 120px; HEIGHT: 26px" align="right"><FONT face="굴림" size="2">어음번호2&nbsp;</FONT></TD>
									<TD style="WIDTH: 246px; HEIGHT: 26px"><asp:textbox id="txtDraftNum2" runat="server" Width="110px" Font-Size="9pt" BorderStyle="Solid"
											BackColor="#EEEEE9" Height="20px" BorderWidth="1px" BorderColor="DimGray"></asp:textbox></TD>
									<TD style="WIDTH: 126px; HEIGHT: 26px" align="right"><FONT face="굴림" size="2">결제일자2&nbsp;</FONT></TD>
									<TD style="WIDTH: 3px; HEIGHT: 26px"><igsch:webdatechooser id="SettleDate2" runat="server" Width="110px" Font-Size="9pt" Text=" " NullDateLabel=" "
											BackColor="#EEEEE9" Height="20px" BorderStyle="Solid" BorderColor="DimGray">
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
									<TD style="WIDTH: 196px; HEIGHT: 26px" align="right"><FONT face="굴림" size="2">금융기관2&nbsp;</FONT></TD>
									<TD style="HEIGHT: 26px" align="left"><asp:dropdownlist id="BankingDropdown2" runat="server" Width="110px" Font-Size="9pt" BackColor="#EEEEE9"
											Height="20px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 120px" align="right"><FONT face="굴림" size="2">어음번호3&nbsp;</FONT></TD>
									<TD style="WIDTH: 246px"><asp:textbox id="txtDraftNum3" runat="server" Width="110px" Font-Size="9pt" BorderStyle="Solid"
											BackColor="#EEEEE9" Height="20px" BorderWidth="1px" BorderColor="DimGray"></asp:textbox></TD>
									<TD style="WIDTH: 126px" align="right"><FONT face="굴림" size="2">결제일자3&nbsp;</FONT></TD>
									<TD style="WIDTH: 3px"><igsch:webdatechooser id="SettleDate3" runat="server" Width="110px" Font-Size="9pt" Text=" " NullDateLabel=" "
											BackColor="#EEEEE9" Height="20px" BorderStyle="Solid" BorderColor="DimGray">
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
									<TD style="WIDTH: 196px" align="right"><FONT face="굴림" size="2">금융기관3&nbsp;</FONT></TD>
									<TD align="left"><asp:dropdownlist id="BankingDropdown3" runat="server" Width="110px" Font-Size="9pt" BackColor="#EEEEE9"
											Height="20px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 120px" align="right" height="8"><FONT face="굴림"></FONT></TD>
									<TD style="WIDTH: 246px" height="8"></TD>
									<TD style="WIDTH: 126px" align="right" height="8"></TD>
									<TD style="WIDTH: 3px" height="8"></TD>
									<TD style="WIDTH: 196px" align="right" height="8"></TD>
									<TD align="left" height="8"></TD>
								</TR>
							</TABLE>
							<TABLE cellSpacing="0" cellPadding="0" width="800">
								<TR>
									<TD style="WIDTH: 480px"><FONT face="굴림">&nbsp; <INPUT id="hdindex" style="WIDTH: 48px; HEIGHT: 22px; BACKGROUND-COLOR: #eeeee9" type="hidden"
												size="2" name="Hidden1" runat="server"> <INPUT id="lbOldCost" style="WIDTH: 48px; HEIGHT: 22px; BACKGROUND-COLOR: #eeeee9" type="hidden"
												size="2" name="Hidden1" runat="server"><INPUT id="hdYear" style="WIDTH: 48px; HEIGHT: 22px; BACKGROUND-COLOR: #eeeee9" type="hidden"
												size="2" name="Hidden1" runat="server"><INPUT id="hdMon" style="WIDTH: 48px; HEIGHT: 22px; BACKGROUND-COLOR: #eeeee9" type="hidden"
												size="2" name="Hidden1" runat="server">
											<asp:LinkButton id="LinkButton1" runat="server" Visible="False"></asp:LinkButton>
											<asp:textbox id="txtEvidence" style="TEXT-ALIGN: right" runat="server" BorderColor="DimGray"
												BorderStyle="Solid" Height="20px" BackColor="#EEEEE9" Font-Size="9pt" Width="110px" BorderWidth="1px"
												ReadOnly="True" Visible="False"></asp:textbox>
										</FONT>
									</TD>
									<TD style="WIDTH: 70px" align="right"><asp:button id="btnInit" runat="server" Height="20px" Width="65px" Font-Size="9pt" Text="초기화"></asp:button><FONT face="굴림">&nbsp;&nbsp;
										</FONT>
									</TD>
									<TD style="WIDTH:70px" align="right"><asp:button id="btnEdit" runat="server" Height="20px" Width="65px" Font-Size="9pt" Text="수   정"
											Enabled="False"></asp:button><FONT face="굴림">&nbsp;&nbsp; </FONT>
									</TD>
									<TD style="WIDTH: 70px" align="right"><asp:button id="btnDelete" runat="server" Height="20px" Width="65px" Font-Size="10pt" Text="삭   제"
											Enabled="False"></asp:button><FONT face="굴림">&nbsp;&nbsp; </FONT>
									</TD>
									<TD style="WIDTH: 70px" align="right"><asp:button id="btnInput" runat="server" Height="20px" Width="65px" Font-Size="9pt" Text="등   록"></asp:button><FONT face="굴림">&nbsp;</FONT></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 480px" height="8"><FONT face="굴림"></FONT></TD>
									<TD style="WIDTH: 70px" align="right" height="8"></TD>
									<TD style="WIDTH: 70px" align="right" height="8"></TD>
									<TD style="WIDTH: 70px" align="right" height="8"></TD>
									<TD style="WIDTH: 70px" align="right" height="8"></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="20" height="300"><FONT face="굴림"></FONT></TD>
					<TD vAlign="top" align="left" height="300">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 360px"
							align="top"><LEGEND style="FONT-SIZE: 10pt" align="left">[입력결과 ]
							</LEGEND>
							<TABLE cellSpacing="0" cellPadding="0" width="800" height="300">
								<TR>
									<TD vAlign="top" align="left"><FONT face="굴림"><igtbl:ultrawebgrid id="uwgP_HT" runat="server" Height="340px" Width="800px">
												<DisplayLayout AutoGenerateColumns="False" AllowSortingDefault="Yes" RowHeightDefault="20px" Version="3.00"
													SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti"
													BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="uwgPxHT"
													CellClickActionDefault="RowSelect">
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
													<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DimGray"
														BorderStyle="Solid" BackColor="Silver" Height="340px"></FrameStyle>
													<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</FooterStyleDefault>
													<ClientSideEvents DblClickHandler="uwgP_HT_DblClickHandler"></ClientSideEvents>
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
													<igtbl:UltraGridBand>
														<Columns>
															<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" Width="150px" BaseColumnName="CompanyName">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" BaseColumnName="BusinessRegistrationNum">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="지급일" Key="PaymentDate" Format="yyyy-MM-dd" BaseColumnName="PaymentDate">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="지급액(물품대)" Key="ItemPaymentCost" Format="###,###,###" BaseColumnName="ItemPaymentCost">
																<CellStyle HorizontalAlign="Right">
																	<Padding Right="4px"></Padding>
																</CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="지급액(부가세)" Key="SupplementaryValueTaxPaymentCost" Format="###,###,###"
																BaseColumnName="SupplementaryValueTaxPaymentCost">
																<CellStyle HorizontalAlign="Right">
																	<Padding Right="4px"></Padding>
																</CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="결제방법코드" Key="DecisionMethodCode" Hidden="True" BaseColumnName="DecisionMethodCode">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="결제방법" Key="DecisionMethod" BaseColumnName="DecisionMethod">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="증거자료" Key="ProofData" Hidden="True" BaseColumnName="ProofData">
																<CellStyle HorizontalAlign="Left"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="어음번호1" Key="BillNum1" BaseColumnName="BillNum1">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="결제일자1" Key="BillPaymentDate1" Format="yyyy-MM-dd" BaseColumnName="BillPaymentDate1">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="은행코드1" Key="BankCode1" Hidden="True" BaseColumnName="BankCode1">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="은행명1" Key="BankName1" BaseColumnName="BankName1">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="어음번호2" Key="BillNum2" BaseColumnName="BillNum2">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="결제일자2" Key="BillPaymentDate2" Format="yyyy-MM-dd" BaseColumnName="BillPaymentDate2">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="은행코드2" Key="BankCode2" Hidden="True" BaseColumnName="BankCode2">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="은행명2" Key="BankName2" BaseColumnName="BankName2">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="어음번호3" Key="BillNum3" BaseColumnName="BillNum3">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="결제일자3" Key="BillPaymentDate3" Format="yyyy-MM-dd" BaseColumnName="BillPaymentDate3">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="은행코드3" Key="BankCode3" Hidden="True" BaseColumnName="BankCode3">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="은행명3" Key="BankName3" BaseColumnName="BankName3">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" Hidden="True" BaseColumnName="RegistrationPerson">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" Hidden="True" BaseColumnName="UpdatingPerson">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="지급원장번호" Key="PaymentHistoryIndex" Hidden="True" BaseColumnName="PaymentHistoryIndex">
																<CellStyle HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
														</Columns>
													</igtbl:UltraGridBand>
												</Bands>
											</igtbl:ultrawebgrid></FONT></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			&nbsp;
		</form>
	</body>
</HTML>
