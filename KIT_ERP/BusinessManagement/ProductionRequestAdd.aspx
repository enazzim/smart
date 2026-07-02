<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Page language="c#" Codebehind="ProductionRequestAdd.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BusinessManagement.ProductionRequestAdd" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ProductionRequestAdd</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		
		function DataRegister()
		{
			if(document.ProductionRequestAdd.txtItemState.value == '양산품')
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
		
		function wcPhantom_AfterSelectChange(webComboId){
			//Add code to handle your event here.
			var obj = igcmbo_getComboById(webComboId);
			alert(obj.getGrid().Rows.getRow(obj.getSelectedIndex()).getCellFromKey("PropertyClassification").getValue());
			document.ProductionRequestAdd.tb_ItemNum.value = obj.getGrid().Rows.getRow(obj.getSelectedIndex()).getCellFromKey("ItemNum").getValue();
			document.ProductionRequestAdd.tb_ItemDrawNum.value = obj.getGrid().Rows.getRow(obj.getSelectedIndex()).getCellFromKey("ItemDrawNum").getValue();
			document.ProductionRequestAdd.tb_PropertyClassification.value = obj.getGrid().Rows.getRow(obj.getSelectedIndex()).getCellFromKey("PropertyClassification").getValue();//자산분류
			document.ProductionRequestAdd.UnitCost.value = obj.getGrid().Rows.getRow(obj.getSelectedIndex()).getCellFromKey("UnitCost").getValue();//단가
		}
		
		function wcItemName_AfterSelectChange(webComboId){
			//Add code to handle your event here.
			var obj = igcmbo_getComboById(webComboId);
			
			document.ProductionRequestAdd.tb_ItemNum.value = obj.getGrid().Rows.getRow(obj.getSelectedIndex()).getCellFromKey("ItemNum").getValue();
			document.ProductionRequestAdd.tb_ItemDrawNum.value = obj.getGrid().Rows.getRow(obj.getSelectedIndex()).getCellFromKey("ItemDrawNum").getValue();
			document.ProductionRequestAdd.tb_PropertyClassification.value = obj.getGrid().Rows.getRow(obj.getSelectedIndex()).getCellFromKey("PropertyClassification").getValue();//자산분류
			document.ProductionRequestAdd.UnitCost.value = obj.getGrid().Rows.getRow(obj.getSelectedIndex()).getCellFromKey("UnitCost").getValue();//단가
		}
		
		function UltraWebGrid1_DblClickHandler(gridName, cellId){
			//Add code to handle your event here.
			var row = igtbl_getRowById(cellId);
			//var combo ;
			//if(row.getCellFromKey("ItemNum").getValue() == "제품")
			//	combo = igcmbo_getComboById("wcItemName");
			//else
			//	combo = igcmbo_getComboById("wcPhantom");
				
			var combo1 = igdrp_getComboById("wdcRequestDate1");
			var combo2 = igdrp_getComboById("wdcRequestDate2");
			var combo3 = igdrp_getComboById("wdcRequestDate3");
			var combo4 = igdrp_getComboById("wdcRequestDate4");
			var combo5 = igdrp_getComboById("wdcRequestDate5");
					
			//combo.setDataValue(row.getCellFromKey("ItemNum").getValue());
			//combo.setDisplayValue(row.getCellFromKey("ItemName").getValue());
			document.ProductionRequestAdd.ItemSearchControl1_txtItemNum.value = row.getCellFromKey("ItemNum").getValue();
			document.ProductionRequestAdd.ItemSearchControl1_txtItemName.value = row.getCellFromKey("ItemName").getValue();			
			document.ProductionRequestAdd.ItemSearchControl1_txtItemDrawNum.value = row.getCellFromKey("ItemDrawNum").getValue();
			
			
			document.ProductionRequestAdd.tb_PropertyClassification.value = row.getCellFromKey("PropertyClassification").getValue();//자산분류
			document.ProductionRequestAdd.UnitCost.value = row.getCellFromKey("ApplyUnitCost").getValue();//단가
			
			document.ProductionRequestAdd.lb_Index.value = row.getCellFromKey("ProductionRequestHistoryIndex").getValue();//생산의뢰원장번호
			
			
			document.ProductionRequestAdd.tb_RequestQuantity1.value = row.getCellFromKey("RequestQuantity1").getValue();
			combo1.setValue(row.getCellFromKey("RequestDate1").getValue());
			
			document.ProductionRequestAdd.tb_RequestQuantity2.value = row.getCellFromKey("RequestQuantity2").getValue();
			if(row.getCellFromKey("RequestDate2").getValue() == "")
				combo2.setValue(null);
			else
				combo2.setValue(row.getCellFromKey("RequestDate2").getValue());//그리드에서 선택한 2차납품요구일을 넣음
			
			document.ProductionRequestAdd.tb_RequestQuantity3.value = row.getCellFromKey("RequestQuantity3").getValue();
			if(row.getCellFromKey("RequestDate3").getValue() == "")
				combo3.setValue(null);
			else
				combo3.setValue(row.getCellFromKey("RequestDate3").getValue());//그리드에서 선택한 3차납품요구일을 넣음
			
			document.ProductionRequestAdd.tb_RequestQuantity4.value = row.getCellFromKey("RequestQuantity4").getValue();
			if(row.getCellFromKey("RequestDate4").getValue() == "")
				combo4.setValue(null);
			else
				combo4.setValue(row.getCellFromKey("RequestDate4").getValue());//그리드에서 선택한 4차납품요구일을 넣음
			
			document.ProductionRequestAdd.tb_RequestQuantity5.value = row.getCellFromKey("RequestQuantity5").getValue();
			if(row.getCellFromKey("RequestDate5").getValue() == "")
				combo5.setValue(null);
			else
				combo5.setValue(row.getCellFromKey("RequestDate5").getValue());//그리드에서 선택한 5차납품요구일을 넣음
			
			document.ProductionRequestAdd.tb_TotalQuantity.value = row.getCellFromKey("ProductionRequestQuantity").getValue();//총의뢰량
		    
		    for(var i = 0; i < document.ProductionRequestAdd.dl_ProductionRequestSource.options.length; i++)
		    {
				if(document.ProductionRequestAdd.dl_ProductionRequestSource.options[i].innerText == row.getCellFromKey("ProductionRequestSource").getValue())
				{
					document.ProductionRequestAdd.dl_ProductionRequestSource.options[i].selected = true;
					break;
				}
		    }
		    
		    document.ProductionRequestAdd.txtItemState.value = row.getCellFromKey("SmallClassificationName").getValue();
		    document.ProductionRequestAdd.bt_Update.disabled = false;
		    document.ProductionRequestAdd.bt_Delete.disabled = false;
		   // document.ProductionRequestAdd.bt_Registration.disabled = true;
		    document.ProductionRequestAdd.btAdd.disabled=true;
		    
		}
		
		
		
		function Add(message)
		{
			if(confirm(message))
			{
				var date1 = igdrp_getComboById("wdcRequestDate1");
				var date2 = igdrp_getComboById("wdcRequestDate2");
				var date3 = igdrp_getComboById("wdcRequestDate3");
				var date4 = igdrp_getComboById("wdcRequestDate4");
				var date5 = igdrp_getComboById("wdcRequestDate5");
				
				quantity =  new Array(5);//1차부터 5차까지의 납품요구량을 a 배열에 넣기 위해 배열선언
				date =  new Array(5);//1차부터 5차까지의 납품요구일을 b 배열에 넣기 위해 배열선언
				
				var first, second, third, fourth, fifth;
				
				// 1차 납기량
				if(ProductionRequestAdd.tb_RequestQuantity1 != null || ProductionRequestAdd.tb_RequestQuantity1.value != "")
					quantity[0] = Number(ProductionRequestAdd.tb_RequestQuantity1.value);
				else
					quantity[0] = 0;
				
				// 2차 납기량
				if(ProductionRequestAdd.tb_RequestQuantity2 != null || ProductionRequestAdd.tb_RequestQuantity2.value != "")
					quantity[1] = Number(ProductionRequestAdd.tb_RequestQuantity2.value);
				else
					quantity[1] = 0;
				
				// 3차 납기량
				if(ProductionRequestAdd.tb_RequestQuantity3 != null || ProductionRequestAdd.tb_RequestQuantity3.value != "")
					quantity[2] = Number(ProductionRequestAdd.tb_RequestQuantity3.value);
				else
					quantity[2] = 0;
				
				// 4차 납기량
				if(ProductionRequestAdd.tb_RequestQuantity4 != null || ProductionRequestAdd.tb_RequestQuantity4.value != "")
					quantity[3] = Number(ProductionRequestAdd.tb_RequestQuantity4.value);
				else
					quantity[3] = 0;
				
				// 5차 납기량
				if(ProductionRequestAdd.tb_RequestQuantity5 != null || ProductionRequestAdd.tb_RequestQuantity5.value != "")
					quantity[4] = Number(ProductionRequestAdd.tb_RequestQuantity5.value);
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
		function UltraWebGrid1_AfterSelectChangeHandler(gridName, id){
			//Add code to handle your event here.
			document.ProductionRequestAdd.lb_Index.value = document.getElementById(id).children[28].innerText;
		}
		
		
		function Process()	
		{
			ProductionRequestAdd.tb_TotalQuantity.value = Number(ProductionRequestAdd.tb_RequestQuantity1.value) + Number(ProductionRequestAdd.tb_RequestQuantity2.value)+Number(ProductionRequestAdd.tb_RequestQuantity3.value) + Number(ProductionRequestAdd.tb_RequestQuantity4.value)+Number(ProductionRequestAdd.tb_RequestQuantity5.value);
			ProductionRequestAdd.tb_TotalCost.value = Number(ProductionRequestAdd.tb_TotalQuantity.value) * Number(ProductionRequestAdd.UnitCost.value);
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
		<form id="ProductionRequestAdd" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; WIDTH: 800px; POSITION: absolute; TOP: 10px"
				height="550" cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD width="20"></TD>
					<TD style="PADDING-TOP: 10px; TOP: 10px" vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid; HEIGHT: 136px"><LEGEND style="FONT-SIZE: 9pt" align="top">[입력]
							</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800">
								<TR>
									<TD vAlign="top" align="left" width="600" colSpan="6" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림">&nbsp;</FONT>
										<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="300" border="0">
											<TR>
												<TD align="right" width="70"><FONT style="FONT-SIZE: 9pt" face="굴림">의뢰원천</FONT></TD>
												<TD>
													<asp:dropdownlist id="dl_ProductionRequestSource" runat="server" Height="20px" BackColor="#EEEEE9"
														Width="100px"></asp:dropdownlist></TD>
											</TR>
										</TABLE>
									</TD>
									<TD align="right" width="70" height="30"><FONT face="굴림">품목상태</FONT></TD>
									<TD align="left" width="130" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림">
											<asp:textbox id="txtItemState" runat="server" Width="100px" BackColor="#EEEEE9" Height="20px"
												BorderStyle="Inset" ReadOnly="True" Font-Size="9pt"></asp:textbox></FONT></TD>
								</TR>
								<TR>
									<TD width="600" colSpan="6" height="30"><FONT face="굴림"></FONT>
										<uc1:ItemSearchControl id="ItemSearchControl1" runat="server"></uc1:ItemSearchControl></TD>
									<TD align="right" width="70" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림">자산분류</FONT></TD>
									<TD width="130" height="30">
										<asp:textbox id="tb_PropertyClassification" runat="server" Height="20px" BackColor="#EEEEE9"
											Width="100px" Font-Size="9pt" ReadOnly="True" BorderStyle="Inset"></asp:textbox><FONT face="굴림"></FONT></TD>
								</TR>
								<TR>
									<TD align="center" width="100%" colSpan="8" height="90"><FONT face="굴림">&nbsp;</FONT>
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
												<TD vAlign="middle" align="center" width="110" bgColor="gainsboro" height="23"><STRONG>총의뢰량</STRONG></TD>
												<TD vAlign="middle" align="center" width="110" bgColor="gainsboro" height="23"><STRONG>총금액</STRONG></TD>
											</TR>
											<TR>
												<TD align="center" width="70" height="35">납기량
												</TD>
												<TD align="center" width="100" height="35">
													<asp:textbox id="tb_RequestQuantity1" style="TEXT-ALIGN: right" runat="server" Height="20px"
														BackColor="#EEEEE9" Width="100px" Font-Size="9pt" BorderStyle="Inset">0</asp:textbox></TD>
												<TD align="center" width="100" height="35">
													<asp:textbox id="tb_RequestQuantity2" style="TEXT-ALIGN: right" runat="server" Height="20px"
														BackColor="#EEEEE9" Width="100px" Font-Size="9pt" BorderStyle="Inset">0</asp:textbox></TD>
												<TD align="center" width="100" height="35">
													<asp:textbox id="tb_RequestQuantity3" style="TEXT-ALIGN: right" runat="server" Height="20px"
														BackColor="#EEEEE9" Width="100px" Font-Size="9pt" BorderStyle="Inset">0</asp:textbox></TD>
												<TD align="center" width="100" height="35">
													<asp:textbox id="tb_RequestQuantity4" style="TEXT-ALIGN: right" runat="server" Height="20px"
														BackColor="#EEEEE9" Width="100px" Font-Size="9pt" BorderStyle="Inset">0</asp:textbox></TD>
												<TD align="center" width="100" height="35">
													<asp:textbox id="tb_RequestQuantity5" style="TEXT-ALIGN: right" runat="server" Height="20px"
														BackColor="#EEEEE9" Width="100px" Font-Size="9pt" BorderStyle="Inset">0</asp:textbox></TD>
												<TD align="center" width="110" height="35" rowSpan="2">
													<asp:textbox id="tb_TotalQuantity" style="TEXT-ALIGN: right" runat="server" Height="20px" BackColor="#EEEEE9"
														Width="100px" Font-Size="9pt" ReadOnly="True" BorderStyle="Inset">0</asp:textbox></TD>
												<TD align="center" width="110" height="35" rowSpan="2">
													<asp:textbox id="tb_TotalCost" style="TEXT-ALIGN: right" runat="server" Height="20px" BackColor="#EEEEE9"
														Width="100px" Font-Size="9pt" ReadOnly="True" BorderStyle="Inset">0</asp:textbox></TD>
											</TR>
											<TR>
												<TD align="center" width="70" height="35">납기일</TD>
												<TD align="center" width="100" height="35">
													<igsch:webdatechooser id="wdcRequestDate1" runat="server" BackColor="#EEEEE9" Width="100px" Editable="False"
														Text="Null" NullDateLabel=" ">
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
												<TD align="center" width="100" height="35">
													<igsch:webdatechooser id="wdcRequestDate2" runat="server" BackColor="#EEEEE9" Width="100px" Editable="False"
														Text="Null" NullDateLabel=" ">
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
												<TD align="center" width="100" height="35">
													<igsch:webdatechooser id="wdcRequestDate3" runat="server" BackColor="#EEEEE9" Width="100px" Editable="False"
														Text="Null" NullDateLabel=" ">
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
												<TD align="center" width="100" height="35">
													<igsch:webdatechooser id="wdcRequestDate4" runat="server" BackColor="#EEEEE9" Width="100px" Editable="False"
														Text="Null" NullDateLabel=" ">
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
												<TD align="center" width="100" height="35">
													<igsch:webdatechooser id="wdcRequestDate5" runat="server" BackColor="#EEEEE9" Width="100px" Editable="False"
														Text="Null" NullDateLabel=" ">
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
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD align="right" width="100%" colSpan="8" height="5"><FONT face="굴림"></FONT></TD>
								</TR>
								<TR>
									<TD align="right" width="100%" colSpan="8" height="20">
										<asp:button id="bt_Registration" runat="server" Height="20px" Width="60px" Text="추  가" Visible="False"></asp:button>
										<asp:LinkButton id="LinkButton2" runat="server" Visible="False">LinkButton</asp:LinkButton>
										<asp:LinkButton id="LinkButton1" runat="server" Visible="False">LinkButton</asp:LinkButton><INPUT id="lb_Index" style="FONT-SIZE: 9pt; WIDTH: 67px; HEIGHT: 22px; BACKGROUND-COLOR: #eeeee9"
											type="hidden" size="5" value="0" name="Hidden1" runat="server"><INPUT id="UnitCost" style="FONT-SIZE: 9pt; WIDTH: 67px; HEIGHT: 22px; BACKGROUND-COLOR: #eeeee9"
											type="hidden" size="5" value="0" name="Hidden1" runat="server">
										<asp:button id="bt_Clear" runat="server" Height="20px" Width="60px" Text="초기화"></asp:button>&nbsp;
										<asp:button id="bt_Update" runat="server" Height="20px" Width="60px" Text="수  정" Enabled="False"></asp:button>&nbsp;
										<asp:button id="bt_Delete" runat="server" Height="20px" Width="60px" Text="삭  제" Enabled="False"></asp:button>&nbsp;&nbsp;<INPUT id="btAdd" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:DataRegister()"
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
					<TD width="20"></TD>
					<TD vAlign="top" align="center" width="800">&nbsp;
						<BR>
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"
							DESIGNTIMEDRAGDROP="1887"><LEGEND align="top">[입력결과]</LEGEND><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="300px" Width="800px">
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
									<Pager PageSize="2">
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

										</Style>
									</Pager>
									<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</HeaderStyleDefault>
									<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
										BackColor="Silver" Height="300px"></FrameStyle>
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
									<igtbl:UltraGridBand AllowUpdate="RowTemplateOnly" CellClickAction="RowSelect">
										<Columns>
											<igtbl:UltraGridColumn HeaderText="품목상태" Key="SmallClassificationName" BaseColumnName="SmallClassificationName"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle Height="25px"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="ItemDrawNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="생산의뢰원천" Key="ProductionRequestSource" Width="90px" HeaderClickAction="SortMulti"
												BaseColumnName="ProductionRequestSource">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="생산의뢰원천코드" Key="ProductionRequestSourceCode" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="ProductionRequestSourceCode">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="자산분류" Key="PropertyClassification" HeaderClickAction="SortMulti" BaseColumnName="PropertyClassification">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="CompanyName">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="사업자번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="BusinessRegistrationNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="1차납기요구량" Key="RequestQuantity1" Hidden="True" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="RequestQuantity1">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="납기량" Key="ProductionRequestQuantity" Width="107px" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="ProductionRequestQuantity">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="납기요구일" Key="RequestDate1" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="RequestDate1">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="2차납기요구량" Key="RequestQuantity2" Hidden="True" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="RequestQuantity2">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="2차납기요구일" Key="RequestDate2" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="RequestDate2">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="3차납기요구량" Key="RequestQuantity3" Hidden="True" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="RequestQuantity3">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="3차납기요구일" Key="RequestDate3" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="RequestDate3">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="4차납기요구량" Key="RequestQuantity4" Hidden="True" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="RequestQuantity4">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="4차납기요구일" Key="RequestDate4" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="RequestDate4">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="5차납기요구량" Key="RequestQuantity5" Hidden="True" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="RequestQuantity5">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="5차납기요구일" Key="RequestDate5" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="RequestDate5">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Hidden="True" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
												BaseColumnName="ApplyUnitCost">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="볼륨번호" Key="VolumNum" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="VolumNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="ProgressCondition">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="RegistrationPerson">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="RegistrationPersonID">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="RegistrationDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="UpdatingPerson">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="UpdatingPersonID">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="UpdatingDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="발주원장번호" Key="ReceivingOrderHistoryIndex" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="ReceivingOrderHistoryIndex">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="번호" Key="ProductionRequestHistoryIndex" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="ProductionRequestHistoryIndex">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
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
