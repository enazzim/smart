<%@ Page language="c#" Codebehind="CollectMoneyRegistration.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.CollectMoneyRegistration" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v1.1, Version=1.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>CollectMoneyRegistration</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		
		function wcCompany_AfterSelectChange(webComboId){
			//Add code to handle your event here.
			var obj = igcmbo_getComboById(webComboId);
			document.CollectMoneyRegistration.txtUncollectMoney.value = obj.getGrid().Rows.getRow(obj.getSelectedIndex()).getCellFromKey("UncollectMoney").getValue();
			
		}
		
		function UltraWebGrid1_DblClickHandler(gridName, cellId){
			
			var row = igtbl_getRowById(cellId);
			var objCombo = igcmbo_getComboById("wcCompany");//거래처
			var objChooser = igdrp_getComboById("wdcCollectMoneyDate");//수금일자
			var objChooser1 = igdrp_getComboById("wdcPaymentDate1");//결재일자1
			var objChooser2 = igdrp_getComboById("wdcPaymentDate2");//결재일자2
			var objChooser3 = igdrp_getComboById("wdcPaymentDate");//결재일자3
			
			objCombo.setDataValue(row.getCellFromKey("CompanyName").getValue())//WebCombo에 그리드에서 선택한 거래처명을 넣음
			objCombo.setDataValue(row.getCellFromKey("BusinessRegistrationNum").getValue());//그리드에서 선택한 사업자등록번호를 Hidden 필드에 넣음
			
			document.CollectMoneyRegistration.tb_ItemPaymentCost.value = row.getCellFromKey("ItemPaymentCost").getValue();//수금액(물품대)
			document.CollectMoneyRegistration.lb_OldCost.value = row.getCellFromKey("ItemPaymentCost").getValue();//수금액(물품대)
			document.CollectMoneyRegistration.tb_SupplementaryValueTaxPaymentCost.value = row.getCellFromKey("SupplementaryValueTaxPaymentCost").getValue();//수금액(부가세)
			
			
			for(var i=0;i<document.CollectMoneyRegistration.dl_DecisionMethod.options.length;i++)//결재방법
			{
				if(document.CollectMoneyRegistration.dl_DecisionMethod.options[i].innerText == row.getCellFromKey("DecisionMethod").getValue())
				{
					document.CollectMoneyRegistration.dl_DecisionMethod.options[i].selected=true
					break;
				}
				document.CollectMoneyRegistration.dl_DecisionMethod.options[0].selected=true
			}
			
			if(row.getCellFromKey("BillNum1").getValue() == null)
				document.CollectMoneyRegistration.tb_BillNum1.value ="";
			else
				document.CollectMoneyRegistration.tb_BillNum1.value = row.getCellFromKey("BillNum1").getValue();//어음번호1
				
			if(row.getCellFromKey("BillNum2").getValue() == null)
				document.CollectMoneyRegistration.tb_BillNum1.value ="";
			else
				document.CollectMoneyRegistration.tb_BillNum1.value = row.getCellFromKey("BillNum2").getValue();//어음번호1
				
			if(row.getCellFromKey("BillNum3").getValue() == null)
				document.CollectMoneyRegistration.tb_BillNum1.value ="";
			else
				document.CollectMoneyRegistration.tb_BillNum1.value = row.getCellFromKey("BillNum3").getValue();//어음번호1
			
			//수금일자
			//objChooser.setValue(row.getCellFromKey("CollectMoneyDate").getValue());
			objChooser.setValue(row.getCellFromKey("CollectMoneyDate").getValue());//등록일
						
			if(row.getCellFromKey("BillPaymentDate1").getValue() == "")
				objChooser1.setValue(null);
			else
				objChooser1.setValue(row.getCellFromKey("BillPaymentDate1").getValue());//결재일자1
			
			if(row.getCellFromKey("BillPaymentDate2").getValue() == "")
				objChooser2.setValue(null);
			else
				objChooser2.setValue(row.getCellFromKey("BillPaymentDate2").getValue());//결재일자2
				
			if(row.getCellFromKey("BillPaymentDate3").getValue() == "")
				objChooser3.setValue(null);
			else
				objChooser3.setValue(row.getCellFromKey("BillPaymentDate3").getValue());//결재일자3
			
			
			
			for(var i=0;i<document.CollectMoneyRegistration.dl_BankName1.options.length;i++)//금융기관1
			{
				if(document.CollectMoneyRegistration.dl_BankName1.options[i].innerText == row.getCellFromKey("BankName1").getValue())
				{
					document.CollectMoneyRegistration.dl_BankName1.options[i].selected=true
					break;
				}
				document.CollectMoneyRegistration.dl_BankName1.options[0].selected=true
			}
			for(var i=0;i<document.CollectMoneyRegistration.dl_BankName2.options.length;i++)//금융기관2BankName1
			{
				if(document.CollectMoneyRegistration.dl_BankName2.options[i].innerText == row.getCellFromKey("BankName2").getValue())
				{
					document.CollectMoneyRegistration.dl_BankName2.options[i].selected=true
					break;
				}
				document.CollectMoneyRegistration.dl_BankName2.options[0].selected=true
			}
			for(var i=0;i<document.CollectMoneyRegistration.dl_BankName3.options.length;i++)//금융기관3
			{
				if(document.CollectMoneyRegistration.dl_BankName3.options[i].innerText == row.getCellFromKey("BankName3").getValue())
				{
					document.CollectMoneyRegistration.dl_BankName3.options[i].selected=true
					break;
				}
				document.CollectMoneyRegistration.dl_BankName3.options[0].selected=true
			}
			
			document.CollectMoneyRegistration.lb_Index.value = row.getCellFromKey("CollectMoneyHistoryIndex").getValue();//원장인덱스
			
			//Add code to handle your event here.
			var year = objChooser.getValue().getFullYear();
			var mon  =objChooser.getValue().getMonth()+1;
			var day = objChooser.getValue().getDate();
			document.CollectMoneyRegistration.lb_CollectMoneyDate.value = year+'-'+mon+'-'+day;
			
			document.CollectMoneyRegistration.bt_Update.disabled = false;
		    document.CollectMoneyRegistration.bt_Delete.disabled = false;
		    document.CollectMoneyRegistration.bt_Register.disabled = true;
		}
		
		function OK(text)
		{
			if(confirm(text + "하시겠습니까?"))
				return true;
			else
				return false;
		}
		
		function RemoveComma(Ovalue)
		{
			var array = new String(Ovalue).split(",");
			var Rvalue = new String();
			for ( var i = 0 ; i < array.length ; i++)	{
				Rvalue += array[i];
			}
			return Rvalue;
		}
		
		function Process()	
		{
			var Tax = Number(RemoveComma(document.CollectMoneyRegistration.tb_ItemPaymentCost.value))/10;
			var Tax1 = (Math.round(Tax,1));
			document.CollectMoneyRegistration.tb_SupplementaryValueTaxPaymentCost.value = MakeCommaSeparate((Math.round((Tax1 * 10000)) / 10000));
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
	<body bgColor="#f7f6f6" MS_POSITIONING="GridLayout">
		<form id="CollectMoneyRegistration" method="post" runat="server">
			<TABLE id="Table1" style="LEFT: 10px; WIDTH: 800px; POSITION: absolute; TOP: 10px" cellSpacing="0"
				cellPadding="0" width="800" border="0">
				<TR>
					<TD style="HEIGHT: 205px" height="205"></TD>
					<TD style="HEIGHT: 205px" vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top">[입력]</LEGEND>
							<TABLE id="Table3" height="185" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD align="right" width="110" height="30"><FONT face="굴림">거래처명&nbsp;</FONT></TD>
									<TD align="left" width="156" height="30"><igcmbo:webcombo id="wcCompany" runat="server" Font-Size="9pt" DropImageXP2="/ig_common/WebGrid3/ig_cmboDown2.bmp"
											DropImageXP1="/ig_common/WebGrid3/ig_cmboDown1.bmp" BorderStyle="Solid" BorderWidth="1px" SelBackColor="10, 36, 106" BackColor="#EEEEE9"
											Width="130px" SelectedIndex="-1" Height="20px" BorderColor="DimGray" SelForeColor="White" Version="3.00" ForeColor="Black" Editable="True">
											<Columns>
												<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" Width="250px" BaseColumnName="CompanyName">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="대표자명" Key="PresidentName" Width="0px" Hidden="True" BaseColumnName="PresidentName">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Width="300px" BaseColumnName="BusinessRegistrationNum">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="번호" Key="CompanyInfoIndex" Width="0px" Hidden="True" BaseColumnName="CompanyInfoIndex">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="미수금" Key="UncollectMoney" Hidden="True" Format="###,###,###.##" BaseColumnName="UncollectMoney"></igtbl:UltraGridColumn>
											</Columns>
											<ClientSideEvents AfterSelectChange="wcCompany_AfterSelectChange"></ClientSideEvents>
											<DropDownLayout DropdownWidth="550px" BorderCollapse="Separate" RowSelectors="No" RowHeightDefault="20px"
												HeaderClickAction="Select" AutoGenerateColumns="False" DropdownHeight="400px" TableLayout="Fixed"
												StationaryMargins="Header">
												<RowStyle BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="White">
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
									<TD align="right" width="110" height="30"><FONT face="굴림">수금일자&nbsp;</FONT></TD>
									<TD align="left" width="156" height="30"><igsch:webdatechooser id="wdcCollectMoneyDate" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Width="130px"
											Height="20px" MaxDate="2076-06-06" Text=" " NullDateLabel=" ">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												MaxDate="2076-06-06" ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD align="right" width="110" height="30"><FONT face="굴림">결재방법&nbsp;</FONT></TD>
									<TD align="left" width="157" height="30"><asp:dropdownlist id="dl_DecisionMethod" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Width="130px"
											Height="20px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD align="right" width="110" height="30"><FONT face="굴림">수금액(물품대)&nbsp;</FONT></TD>
									<TD align="left" width="156" height="30"><asp:textbox id="tb_ItemPaymentCost" style="TEXT-ALIGN: right" runat="server" Font-Size="9pt"
											BorderStyle="Inset" BackColor="#EEEEE9" Width="130px" Height="20px">0</asp:textbox></TD>
									<TD align="right" width="110" height="30"><FONT face="굴림">수금액&nbsp;(부가세)&nbsp;</FONT></TD>
									<TD align="left" width="156" height="30"><asp:textbox id="tb_SupplementaryValueTaxPaymentCost" style="TEXT-ALIGN: right" runat="server"
											Font-Size="9pt" BorderStyle="Inset" BackColor="#EEEEE9" Width="130px" Height="20px">0</asp:textbox></TD>
									<TD align="right" width="110" height="30"><FONT face="굴림">미 수 금&nbsp; </FONT>
									</TD>
									<TD align="left" width="157" height="30"><FONT face="굴림"><asp:textbox id="txtUncollectMoney" style="TEXT-ALIGN: right" runat="server" Font-Size="9pt"
												BorderStyle="Inset" BackColor="#EEEEE9" Width="130px" Height="20px" ReadOnly="True">0</asp:textbox></FONT></TD>
								</TR>
								<TR>
									<TD align="right" width="110" height="30"><FONT face="굴림">어음번호1&nbsp;</FONT></TD>
									<TD align="left" width="156" height="30"><asp:textbox id="tb_BillNum1" runat="server" Font-Size="9pt" BorderStyle="Inset" BackColor="#EEEEE9"
											Width="130px" Height="20px"></asp:textbox></TD>
									<TD align="right" width="110" height="30"><FONT face="굴림">결재일자1&nbsp;</FONT></TD>
									<TD align="left" width="156" height="30"><igsch:webdatechooser id="wdcPaymentDate1" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Width="130px"
											Height="20px" MaxDate="2076-06-06" Text="Null" NullDateLabel=" ">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												MaxDate="2076-06-06" ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderStyle="Inset"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD align="right" width="110" height="30"><FONT face="굴림">금융기관1&nbsp;</FONT></TD>
									<TD align="left" width="157" height="30"><asp:dropdownlist id="dl_BankName1" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Width="130px"
											Height="20px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD align="right" width="110" height="30"><FONT face="굴림">어음번호2&nbsp;</FONT></TD>
									<TD align="left" width="156" height="30"><asp:textbox id="tb_BillNum2" runat="server" Font-Size="9pt" BorderStyle="Inset" BackColor="#EEEEE9"
											Width="130px" Height="20px"></asp:textbox></TD>
									<TD align="right" width="110" height="30"><FONT face="굴림">결재일자2&nbsp;</FONT></TD>
									<TD align="left" width="156" height="30"><igsch:webdatechooser id="wdcPaymentDate2" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Width="130px"
											Height="20px" MaxDate="2076-06-06" Text="Null" NullDateLabel=" ">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												MaxDate="2076-06-06" ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderStyle="Inset"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD align="right" width="110" height="30"><FONT face="굴림">금융기관2&nbsp;</FONT></TD>
									<TD align="left" width="157" height="30"><asp:dropdownlist id="dl_BankName2" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Width="130px"
											Height="20px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD align="right" width="110" height="30"><FONT face="굴림">어음번호3&nbsp;</FONT></TD>
									<TD align="left" width="156" height="30"><asp:textbox id="tb_BillNum3" runat="server" Font-Size="9pt" BorderStyle="Inset" BackColor="#EEEEE9"
											Width="130px" Height="20px"></asp:textbox></TD>
									<TD align="right" width="110" height="30"><FONT face="굴림">결재일자3&nbsp;</FONT></TD>
									<TD align="left" width="156" height="30"><FONT face="굴림"><igsch:webdatechooser id="wdcPaymentDate" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Width="130px"
												Height="20px" MaxDate="2076-06-06" Text="Null" NullDateLabel=" ">
												<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
													MaxDate="2076-06-06" ShowTitle="False" ShowFooter="False">
													<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
													<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
													<DropDownStyle BackColor="White"></DropDownStyle>
													<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
												</CalendarLayout>
												<DropDownStyle BorderStyle="Inset"></DropDownStyle>
												<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
												<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
											</igsch:webdatechooser></FONT></TD>
									<TD align="right" width="110" height="30"><FONT face="굴림">금융기관3&nbsp;</FONT></TD>
									<TD align="left" width="157" height="30"><asp:dropdownlist id="dl_BankName3" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Width="130px"
											Height="20px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD align="right" width="800" colSpan="6" height="7" style="HEIGHT: 7px"><FONT face="굴림"></FONT></TD>
								</TR>
								<TR>
									<TD align="right" width="800" colSpan="6" height="30" vAlign="middle"><INPUT id="hdYear" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden4" runat="server"><INPUT id="hdMon" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden3" runat="server"><INPUT style="WIDTH: 50px; HEIGHT: 20px" type="hidden" runat="server" id="lb_CollectMoneyDate"><INPUT id="lb_OldCost" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" value="0" name="Hidden1"
											runat="server"><INPUT id="lb_Index" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" value="0" name="Hidden1"
											runat="server">
										<asp:button id="bt_Clear" runat="server" Width="60px" Height="20px" Text="초기화"></asp:button>&nbsp;
										<asp:button id="bt_Delete" runat="server" Width="60px" Height="20px" Text="삭  제" Enabled="False"></asp:button>&nbsp;
										<asp:button id="bt_Update" runat="server" Width="60px" Height="20px" Text="수  정" Enabled="False"></asp:button>&nbsp;<asp:button id="bt_Register" runat="server" Width="60px" Height="20px" Text="등  록"></asp:button>&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="0"></TD>
					<TD vAlign="top" align="center" width="800">&nbsp;
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top">[입력결과]</LEGEND><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="100%" Height="310px">
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
									<Pager>
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
										BackColor="Silver" Height="310px"></FrameStyle>
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
											<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" HeaderClickAction="SortMulti" BaseColumnName="CompanyName">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="BusinessRegistrationNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수금일" Key="CollectMoneyDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti" BaseColumnName="CollectMoneyDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수금액(물품대)" Key="ItemPaymentCost" Format="\ ###,###,##0" HeaderClickAction="SortMulti" BaseColumnName="ItemPaymentCost">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수금액(부가세)" Key="SupplementaryValueTaxPaymentCost" Format="\ ###,###,##0" HeaderClickAction="SortMulti" BaseColumnName="SupplementaryValueTaxPaymentCost">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="결재방법코드" Key="DecisionMethodCode" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="DecisionMethodCode">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="결재방법" Key="DecisionMethod" HeaderClickAction="SortMulti" BaseColumnName="DecisionMethod">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="어음번호1" Key="BillNum1" HeaderClickAction="SortMulti" BaseColumnName="BillNum1">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="어음결재일자1" Key="BillPaymentDate1" Format="yyyy-MM-dd" HeaderClickAction="SortMulti" BaseColumnName="BillPaymentDate1">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="결재은행코드1" Key="BankCode1" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="BankCode1">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="금융기관1" Key="BankName1" HeaderClickAction="SortMulti" BaseColumnName="BankName1">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="어음번호2" Key="BillNum2" HeaderClickAction="SortMulti" BaseColumnName="BillNum2">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="어음결재일자2" Key="BillPaymentDate2" Format="yyyy-MM-dd" HeaderClickAction="SortMulti" BaseColumnName="BillPaymentDate2">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="결재은행코드2" Key="BankCode1" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="BankCode1">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="금융기관2" Key="BankName2" HeaderClickAction="SortMulti" BaseColumnName="BankName2">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="어음번호3" Key="BillNum3" HeaderClickAction="SortMulti" BaseColumnName="BillNum3">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="어음결재일자3" Key="BillPaymentDate3" Format="yyyy-MM-dd" HeaderClickAction="SortMulti" BaseColumnName="BillPaymentDate3">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="결재은행코드3" Key="BankCode3" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="BankCode3">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="금융기관3" Key="BankName3" HeaderClickAction="SortMulti" BaseColumnName="BankName3">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPerson">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPersonID">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti" BaseColumnName="RegistrationDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" HeaderClickAction="SortMulti" BaseColumnName="UpdatingPerson">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="UpdatingPersonID">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" HeaderClickAction="SortMulti" BaseColumnName="UpdatingDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="번호" Key="CollectMoneyHistoryIndex" HeaderClickAction="SortMulti" BaseColumnName="CollectMoneyHistoryIndex">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
										</Columns>
									</igtbl:UltraGridBand>
								</Bands>
							</igtbl:ultrawebgrid></FIELDSET>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
