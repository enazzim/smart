<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Page language="c#" Codebehind="GoodsBuyingRequestAdd.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BusinessManagement.CollectMoneyRegistration" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>GoodsBuyingRequestAdd</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		
		function DataRegister()
		{
			if(document.GoodsBuyingRequestAdd.txtItemState.value == '양산품')
				Register()
			else
			{
				if(confirm("선택한 품목은 양산품이 아닙니다. 계속 등록하시겠습니까?") )	
				{
					this.Register();
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		
		function Register()
		{
			__doPostBack('LinkButton2');
			
		}
		
		function DoPost()
		{
			__doPostBack('LinkButton1','');
		}
		
		function wcItemName_AfterSelectChange(webComboId){
			//Add code to handle your event here.
			var obj = igcmbo_getComboById(webComboId);
			
			document.GoodsBuyingRequestAdd.tb_ItemNum.value = obj.getGrid().Rows.getRow(obj.getSelectedIndex()).getCellFromKey("ItemNum").getValue();
			document.GoodsBuyingRequestAdd.tb_ItemDrawNum.value = obj.getGrid().Rows.getRow(obj.getSelectedIndex()).getCellFromKey("ItemDrawNum").getValue();
			document.GoodsBuyingRequestAdd.tb_PropertyClassification.value = obj.getGrid().Rows.getRow(obj.getSelectedIndex()).getCellFromKey("PropertyClassification").getValue();//자산분류
			document.GoodsBuyingRequestAdd.UnitCost.value = obj.getGrid().Rows.getRow(obj.getSelectedIndex()).getCellFromKey("UnitCost").getValue();
			
		}
		
		
		function UltraWebGrid1_DblClickHandler(gridName, cellId){
			//Add code to handle your event here.
			var row = igtbl_getRowById(cellId);
			//var combo = igcmbo_getComboById("wcItemName");
			var combo1 = igdrp_getComboById("wdcDeliveryDemandDate1");
			var combo2 = igdrp_getComboById("wdcDeliveryDemandDate2");
			var combo3 = igdrp_getComboById("wdcDeliveryDemandDate3");
			var combo4 = igdrp_getComboById("wdcDeliveryDemandDate4");
			var combo5 = igdrp_getComboById("wdcDeliveryDemandDate5");
					
			
			
			
			//combo.setDataValue(row.getCellFromKey("ItemNum").getValue());
			//combo.setDisplayValue(row.getCellFromKey("ItemName").getValue());
			document.GoodsBuyingRequestAdd.tb_PropertyClassification.value = row.getCellFromKey("PropertyClassification").getValue();//자산분류
			document.GoodsBuyingRequestAdd.UnitCost.value = row.getCellFromKey("ApplyUnitCost").getValue();//단가
			
			//document.GoodsBuyingRequestAdd.lbBuyingRequestHistoryIndex.value = row.getCellFromKey("BuyingRequestHistoryIndex").getValue();//구매의뢰원장번호
			document.GoodsBuyingRequestAdd.lb_Index.value = row.getCellFromKey("BuyingRequestHistoryIndex").getValue();//구매의뢰원장번호
			
			document.GoodsBuyingRequestAdd.tb_DeliveryDemandQuantity1.value = MakeCommaSeparate(row.getCellFromKey("FirstDeliveryDemandQuantity").getValue());
			combo1.setValue(row.getCellFromKey("FirstDeliveryDemandDate").getValue());
			
			document.GoodsBuyingRequestAdd.tb_DeliveryDemandQuantity2.value = MakeCommaSeparate(row.getCellFromKey("SecondDeliveryDemandQuantity").getValue());
			if(row.getCellFromKey("SecondDeliveryDemandDate").getValue() == "")
				combo2.setValue(null);
			else
				combo2.setValue(row.getCellFromKey("SecondDeliveryDemandDate").getValue());//그리드에서 선택한 2차납품요구일을 넣음
			
			document.GoodsBuyingRequestAdd.tb_DeliveryDemandQuantity3.value = MakeCommaSeparate(row.getCellFromKey("ThirdDeliveryDemandQuantity").getValue());
			if(row.getCellFromKey("SecondDeliveryDemandDate").getValue() == "")
				combo3.setValue(null);
			else
				combo3.setValue(row.getCellFromKey("ThirdDeliveryDemandDate").getValue());//그리드에서 선택한 3차납품요구일을 넣음
			
			document.GoodsBuyingRequestAdd.tb_DeliveryDemandQuantity4.value = MakeCommaSeparate(row.getCellFromKey("FourthDeliveryDemandQuantity").getValue());
			if(row.getCellFromKey("SecondDeliveryDemandDate").getValue() == "")
				combo4.setValue(null);
			else
				combo4.setValue(row.getCellFromKey("FourthDeliveryDemandDate").getValue());//그리드에서 선택한 4차납품요구일을 넣음
			
			document.GoodsBuyingRequestAdd.tb_DeliveryDemandQuantity5.value = MakeCommaSeparate(row.getCellFromKey("FifthDeliveryDemandQuantity").getValue());
			if(row.getCellFromKey("SecondDeliveryDemandDate").getValue() == "")
				combo5.setValue(null);
			else
				combo5.setValue(row.getCellFromKey("FifthDeliveryDemandDate").getValue());//그리드에서 선택한 5차납품요구일을 넣음
			
			document.GoodsBuyingRequestAdd.tb_TotalDeliveryDemandQuantity.value = MakeCommaSeparate(row.getCellFromKey("OrderQuantity").getValue());//총의뢰량
		    
		    for(var i = 0; i < document.GoodsBuyingRequestAdd.dlBuyingRequestSource.options.length; i++)
		    {
				if(document.GoodsBuyingRequestAdd.dlBuyingRequestSource.options[i].innerText == row.getCellFromKey("BuyingRequestSource").getValue())
				{
					document.GoodsBuyingRequestAdd.dlBuyingRequestSource.options[i].selected = true;
					break;
				}
		    }
		    
		    document.GoodsBuyingRequestAdd.txtItemState.value = row.getCellFromKey("SmallClassificationName").getValue();
		    document.GoodsBuyingRequestAdd.ItemSearchControl1_txtItemNum.value = row.getCellFromKey("ItemNum").getValue();
			document.GoodsBuyingRequestAdd.ItemSearchControl1_txtItemName.value = row.getCellFromKey("ItemName").getValue();			
			document.GoodsBuyingRequestAdd.ItemSearchControl1_txtItemDrawNum.value = row.getCellFromKey("ItemDrawNum").getValue();
			
		    document.GoodsBuyingRequestAdd.bt_Update.disabled = false;
		    document.GoodsBuyingRequestAdd.bt_Delete.disabled = false;
		    document.GoodsBuyingRequestAdd.btAdd.disabled = true;
		}
		
		function Add(message)
		{
			if(confirm(message))
			{
				var date1 = igdrp_getComboById("wdcDeliveryDemandDate1");
				var date2 = igdrp_getComboById("wdcDeliveryDemandDate2");
				var date3 = igdrp_getComboById("wdcDeliveryDemandDate3");
				var date4 = igdrp_getComboById("wdcDeliveryDemandDate4");
				var date5 = igdrp_getComboById("wdcDeliveryDemandDate5");
				
				quantity =  new Array(5);//1차부터 5차까지의 납품요구량을 a 배열에 넣기 위해 배열선언
				date =  new Array(5);//1차부터 5차까지의 납품요구일을 b 배열에 넣기 위해 배열선언
				
				var first, second, third, fourth, fifth;
				
				// 1차 납기량
				if(GoodsBuyingRequestAdd.tb_DeliveryDemandQuantity1 != null || GoodsBuyingRequestAdd.tb_DeliveryDemandQuantity1.value != "")
					quantity[0] = Number(GoodsBuyingRequestAdd.tb_DeliveryDemandQuantity1.value);
				else
					quantity[0] = 0;
				
				// 2차 납기량
				if(GoodsBuyingRequestAdd.tb_DeliveryDemandQuantity2 != null || GoodsBuyingRequestAdd.tb_DeliveryDemandQuantity2.value != "")
					quantity[1] = Number(GoodsBuyingRequestAdd.tb_DeliveryDemandQuantity2.value);
				else
					quantity[1] = 0;
				
				// 3차 납기량
				if(GoodsBuyingRequestAdd.tb_DeliveryDemandQuantity3 != null || GoodsBuyingRequestAdd.tb_DeliveryDemandQuantity3.value != "")
					quantity[2] = Number(GoodsBuyingRequestAdd.tb_DeliveryDemandQuantity3.value);
				else
					quantity[2] = 0;
				
				// 4차 납기량
				if(GoodsBuyingRequestAdd.tb_DeliveryDemandQuantity4 != null || GoodsBuyingRequestAdd.tb_DeliveryDemandQuantity4.value != "")
					quantity[3] = Number(GoodsBuyingRequestAdd.tb_DeliveryDemandQuantity4.value);
				else
					quantity[3] = 0;
				
				// 5차 납기량
				if(GoodsBuyingRequestAdd.tb_DeliveryDemandQuantity5 != null || GoodsBuyingRequestAdd.tb_DeliveryDemandQuantity5.value != "")
					quantity[4] = Number(GoodsBuyingRequestAdd.tb_DeliveryDemandQuantity5.value);
				else
					quantity[4] = 0;
					
				value = first + second + third + fourth + fifth;
				
				//배열b에 1차부터 5차까지의 납기요구일을 넣음
							
				date[0] = date1.getValue();
				date[1] = date2.getValue();
				date[2] = date3.getValue();
				date[3] = date4.getValue();
				date[4] = date5.getValue();
				
				Check = true;
				for(i = 0; i < quantity.length; i++)
				{
					if(quantity[i] == 0)
					{
						if(date[i] == null || date[i] == "")
						{
							if(i != quantity.length-1)
							{
								if(quantity[i] < quantity[i+1])
								{
									Check = false;
									alert((i+1) + "차 납기량이 입력되지 않았습니다.");
									break;
								}
							}						
						}
						else
						{
							Check = false;
							alert((i+1) + "차 납기일이 잘못 입력되었습니다.");
							break;
						}
					}
					else
					{
						if(date[i] == null || date[i] == "")
						{
							Check = false;
							alert((i+1) + "차 납기일이 입력되지 않았습니다.");
							break;
						}
						if(i != date.length-1)
						{
							if(Date.parse(date[i]) > Date.parse(date[i+1]))
							{
								Check = false;
								alert((i+1) + "차 납기일이 잘못 입력되었습니다.");
								break;
							}
						}						
					}
				}
			
				if(Check)
				{
					return true
				}
				else
				{
					return false;
				}
			}
			else
				return false;
		}		
		
		
		
		
		function OK(text)
		{
			if(confirm(text + "하시겠습니까?"))
				return true;
			else
				return false;
		}
		
		
		function Process()	
		{
			var value = Number(RemoveComma(GoodsBuyingRequestAdd.tb_DeliveryDemandQuantity1.value)) + Number(RemoveComma(GoodsBuyingRequestAdd.tb_DeliveryDemandQuantity2.value))+Number(RemoveComma(GoodsBuyingRequestAdd.tb_DeliveryDemandQuantity3.value)) + Number(RemoveComma(GoodsBuyingRequestAdd.tb_DeliveryDemandQuantity4.value))+Number(RemoveComma(GoodsBuyingRequestAdd.tb_DeliveryDemandQuantity5.value));
			GoodsBuyingRequestAdd.tb_TotalDeliveryDemandQuantity.value = MakeCommaSeparate((Math.round((value * 10000)) / 10000));
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
		<form id="GoodsBuyingRequestAdd" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" height="460"
				cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD></TD>
					<TD style="PADDING-TOP: 10px" vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 800px; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top">[입력]</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800">
								<TR>
									<TD align="right" width="70" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림">의뢰원천</FONT></TD>
									<TD width="130" height="30">
										<asp:dropdownlist id="dlBuyingRequestSource" runat="server" Width="100px" Height="20px" BackColor="#EEEEE9"></asp:dropdownlist></TD>
									<TD align="right" width="70" height="30"><FONT face="굴림"></FONT></TD>
									<TD width="130" height="30"><FONT face="굴림"></FONT></TD>
									<TD align="right" width="70" height="30"><FONT face="굴림"></FONT></TD>
									<TD width="130" height="30"><FONT face="굴림"></FONT></TD>
									<TD align="right" width="70" height="30"><FONT face="굴림">품목상태&nbsp;</FONT></TD>
									<TD width="130" height="30" align="left"><FONT face="굴림">
											<asp:textbox id="txtItemState" runat="server" BackColor="#EEEEE9" Width="100px" ReadOnly="True"
												BorderStyle="Inset"></asp:textbox></FONT></TD>
								</TR>
								<TR>
									<TD colSpan="6" height="25">
										<uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></TD>
									<TD align="right" height="25"><FONT face="굴림">자산분류&nbsp; </FONT>
									</TD>
									<TD height="25">
										<asp:textbox id="tb_PropertyClassification" runat="server" Width="100px" BackColor="#EEEEE9"
											BorderStyle="Inset" ReadOnly="True"></asp:textbox></TD>
								</TR>
								<TR>
									<TD align="center" width="800" colSpan="8" height="90">
										<TABLE id="Table4" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BORDER-BOTTOM: dimgray 1px solid"
											cellSpacing="0" borderColorDark="white" cellPadding="0" width="770" align="center"
											borderColorLight="dimgray" border="1">
											<TR>
												<TD vAlign="middle" align="center" width="70" bgColor="#dcdcdc" height="23">구분</TD>
												<TD vAlign="middle" align="center" width="100" bgColor="gainsboro" height="23">1차</TD>
												<TD vAlign="middle" align="center" width="100" bgColor="gainsboro" height="23">2차</TD>
												<TD vAlign="middle" align="center" width="100" bgColor="gainsboro" height="23">3차</TD>
												<TD vAlign="middle" align="center" width="100" bgColor="gainsboro" height="23">4차</TD>
												<TD vAlign="middle" align="center" width="100" bgColor="gainsboro" height="23">5차</TD>
												<TD vAlign="middle" align="center" width="220" bgColor="gainsboro" colSpan="2" height="23"><STRONG>총의뢰량</STRONG></TD>
											</TR>
											<TR>
												<TD align="center" height="35">납기량
												</TD>
												<TD align="center" width="100" height="35">
													<asp:textbox id="tb_DeliveryDemandQuantity1" style="TEXT-ALIGN: right" runat="server" Width="100px"
														Height="20px" BackColor="#EEEEE9" BorderStyle="Inset" Font-Size="9pt">0</asp:textbox></TD>
												<TD align="center" width="100" height="35">
													<asp:textbox id="tb_DeliveryDemandQuantity2" style="TEXT-ALIGN: right" runat="server" Width="100px"
														Height="20px" BackColor="#EEEEE9" BorderStyle="Inset" Font-Size="9pt">0</asp:textbox></TD>
												<TD align="center" width="100" height="35">
													<asp:textbox id="tb_DeliveryDemandQuantity3" style="TEXT-ALIGN: right" runat="server" Width="100px"
														Height="20px" BackColor="#EEEEE9" BorderStyle="Inset" Font-Size="9pt">0</asp:textbox></TD>
												<TD align="center" width="100" height="35">
													<asp:textbox id="tb_DeliveryDemandQuantity4" style="TEXT-ALIGN: right" runat="server" Width="100px"
														Height="20px" BackColor="#EEEEE9" BorderStyle="Inset" Font-Size="9pt">0</asp:textbox></TD>
												<TD align="center" width="100" height="35">
													<asp:textbox id="tb_DeliveryDemandQuantity5" style="TEXT-ALIGN: right" runat="server" Width="100px"
														Height="20px" BackColor="#EEEEE9" BorderStyle="Inset" Font-Size="9pt">0</asp:textbox></TD>
												<TD align="center" width="220" colSpan="2" height="35" rowSpan="2"><FONT face="굴림">
														<asp:textbox id="tb_TotalDeliveryDemandQuantity" style="TEXT-ALIGN: right" runat="server" Width="100px"
															BackColor="#EEEEE9" BorderStyle="Inset">0</asp:textbox></FONT></TD>
											</TR>
											<TR>
												<TD align="center" height="35">납기일</TD>
												<TD align="center" width="100" height="35">
													<igsch:webdatechooser id="wdcDeliveryDemandDate1" runat="server" Width="100px" Height="20px" BackColor="#EEEEE9"
														Text="Null" NullDateLabel=" ">
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
												<TD align="center" width="100" height="35">
													<igsch:webdatechooser id="wdcDeliveryDemandDate2" runat="server" Width="100px" Height="20px" BackColor="#EEEEE9"
														Text="Null" NullDateLabel=" ">
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
												<TD align="center" width="100" height="35">
													<igsch:webdatechooser id="wdcDeliveryDemandDate3" runat="server" Width="100px" Height="20px" BackColor="#EEEEE9"
														Text="Null" NullDateLabel=" ">
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
												<TD align="center" width="100" height="35">
													<igsch:webdatechooser id="wdcDeliveryDemandDate4" runat="server" Width="100px" Height="20px" BackColor="#EEEEE9"
														Text="Null" NullDateLabel=" ">
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
												<TD align="center" width="100" height="35">
													<igsch:webdatechooser id="wdcDeliveryDemandDate5" runat="server" Width="100px" Height="20px" BackColor="#EEEEE9"
														Text="Null" NullDateLabel=" ">
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
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD align="right" width="800" colSpan="8" height="8"><FONT face="굴림"></FONT></TD>
								</TR>
								<TR>
									<TD align="right" width="800" colSpan="8">
										<asp:LinkButton id="LinkButton2" runat="server" Visible="False">LinkButton</asp:LinkButton>
										<asp:LinkButton id="LinkButton1" runat="server" Visible="False">LinkButton</asp:LinkButton><INPUT id="UnitCost" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" value="0" name="Hidden1"
											runat="server"><INPUT id="lb_Index" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" value="0" runat="server">
										<asp:button id="bt_Clear" runat="server" Width="60px" Height="20px" Font-Size="10pt" Text="초기화"></asp:button>&nbsp;
										<asp:button id="bt_Update" runat="server" Width="60px" Height="20px" Font-Size="10pt" Text="수정"
											Enabled="False"></asp:button>&nbsp;
										<asp:button id="bt_Delete" runat="server" Width="60px" Height="20px" Font-Size="10pt" Text="삭제"
											Enabled="False"></asp:button>&nbsp;<INPUT id="btAdd" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:DataRegister()"
											type="button" value="추  가" name="btnReset" runat="server">&nbsp;</TD>
								</TR>
								<TR>
									<TD align="right" width="800" colSpan="8" height="5"><FONT face="굴림"></FONT></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="0"><FONT face="굴림"></FONT></TD>
					<TD style="PADDING-TOP: 10px" vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 800px; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top">[입력결과]</LEGEND><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="310px" Width="100%">
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
									<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</HeaderStyleDefault>
									<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
									<FrameStyle Width="100%" BorderWidth="1px" Font-Size="9pt" Font-Names="Verdana" BorderStyle="Solid"
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
											<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" EditorControlID="" Format="" HeaderClickAction="SortMulti"
												BaseColumnName="ItemNum" NullText="" FooterText="">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" EditorControlID="" Format="" HeaderClickAction="SortMulti"
												BaseColumnName="ItemDrawNum" NullText="" FooterText="">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" EditorControlID="" Format="" HeaderClickAction="SortMulti"
												BaseColumnName="ItemName" NullText="" FooterText="">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="자산분류" Key="PropertyClassification" EditorControlID="" Format="" HeaderClickAction="SortMulti"
												BaseColumnName="PropertyClassification" NullText="" FooterText="">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="1차납기요구량" Key="FirstDeliveryDemandQuantity" EditorControlID="" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="FirstDeliveryDemandQuantity" NullText="" FooterText="">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="1차납기요구일" Key="FirstDeliveryDemandDate" EditorControlID="" Format="yyyy-MM-dd"
												HeaderClickAction="SortMulti" BaseColumnName="FirstDeliveryDemandDate" NullText="" FooterText="">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="2차납기요구량" Key="SecondDeliveryDemandQuantity" EditorControlID="" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="SecondDeliveryDemandQuantity" NullText="" FooterText="">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="2차납기요구일" Key="SecondDeliveryDemandDate" EditorControlID="" Format="yyyy-MM-dd"
												HeaderClickAction="SortMulti" BaseColumnName="SecondDeliveryDemandDate" NullText="" FooterText="">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="3차납기요구량" Key="ThirdDeliveryDemandQuantity" EditorControlID="" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="ThirdDeliveryDemandQuantity" NullText="" FooterText="">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="3차납기요구일" Key="ThirdDeliveryDemandDate" EditorControlID="" Format="yyyy-MM-dd"
												HeaderClickAction="SortMulti" BaseColumnName="ThirdDeliveryDemandDate" NullText="" FooterText="">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="4차납기요구량" Key="FourthDeliveryDemandQuantity" EditorControlID="" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="FourthDeliveryDemandQuantity" NullText="" FooterText="">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="4차납기요구일" Key="FourthDeliveryDemandDate" EditorControlID="" Format="yyyy-MM-dd"
												HeaderClickAction="SortMulti" BaseColumnName="FourthDeliveryDemandDate" NullText="" FooterText="">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="5차납기요구량" Key="FifthDeliveryDemandQuantity" EditorControlID="" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="FifthDeliveryDemandQuantity" NullText="" FooterText="">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="5차납기요구일" Key="FifthDeliveryDemandDate" EditorControlID="" Format="yyyy-MM-dd"
												HeaderClickAction="SortMulti" BaseColumnName="FifthDeliveryDemandDate" NullText="" FooterText="">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="총요구량" Key="OrderQuantity" EditorControlID="" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="OrderQuantity" NullText="" FooterText="">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" EditorControlID="" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
												BaseColumnName="ApplyUnitCost" NullText="" FooterText="">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="품목상태" Key="SmallClassificationName" Hidden="True" BaseColumnName="SmallClassificationName"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="총금액" Key="TotalCost" EditorControlID="" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
												BaseColumnName="TotalCost" NullText="" FooterText="">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="구매의뢰원천" Key="BuyingRequestSource" EditorControlID="" Format="" HeaderClickAction="SortMulti"
												BaseColumnName="BuyingRequestSource" NullText="" FooterText="">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="구매의뢰원천코드" Key="BuyingRequestSourceCode" EditorControlID="" Hidden="True"
												Format="" HeaderClickAction="SortMulti" BaseColumnName="BuyingRequestSourceCode" NullText="" FooterText="">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" EditorControlID="" Format="" HeaderClickAction="SortMulti"
												BaseColumnName="RegistrationPerson" NullText="" FooterText="">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" EditorControlID="" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="RegistrationDate" NullText="" FooterText="">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" EditorControlID="" Format="" HeaderClickAction="SortMulti"
												BaseColumnName="UpdatingPerson" NullText="" FooterText="">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" EditorControlID="" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="UpdatingDate" NullText="" FooterText="">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="번호" Key="BuyingRequestHistoryIndex" EditorControlID="" Format="" HeaderClickAction="SortMulti"
												BaseColumnName="BuyingRequestHistoryIndex" NullText="" FooterText="">
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
