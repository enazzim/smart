<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Page language="c#" Codebehind="ReceiveingRegistration.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BusinessManagement.ReceiveingRegistration" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ReceiveingRegistration</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		function ResettxtBox()
		{
			
			ResetTextBox();
			ResetBox();
			var objChooser1 = igdrp_getComboById("wdcReceivingOrderDate");
			var objChooser2 = igdrp_getComboById("wdcDeliveryRequestDate1");
			objChooser1.setValue(null);
			objChooser2.setValue(null);	
			//수주번호
			document.ReceiveingRegistration.lb_Index.value = "0";
			document.ReceiveingRegistration.hdBusinessRegistrationNum.value = "";
			document.ReceiveingRegistration.hdItemNum.value = "";
			
			
			//생산의뢰여부
			document.ReceiveingRegistration.dlProductionRequestDivision.options[0].selected=true;
			
			//단위,규격
			document.ReceiveingRegistration.tb_Unit.value = ""
			document.ReceiveingRegistration.tb_Standard.value = "";
			
			
			
			
			
			//담당자 tb_CompanyPersonInCharge
			document.ReceiveingRegistration.tb_CompanyPersonInCharge.value ="";
			//연락처 tb_TelephoneNum
			document.ReceiveingRegistration.tb_TelephoneNum.value = "";
			//자산분류 PropertyClassification
			document.ReceiveingRegistration.tb_PropertyClassification.value = "";
			
			//품목상태 txtItemState
			document.ReceiveingRegistration.txtItemState.value = "";
			
			//적용단가 및 수주단가 발주량
			document.ReceiveingRegistration.tb_ReceiveUnitCost.value = "0";
			document.ReceiveingRegistration.tb_ApplyUnitCost.value = "0"
			document.ReceiveingRegistration.tb_TotalReceiveingOrderQuantity.value = "0";
			//총금액
			document.ReceiveingRegistration.tb_TotalCost.value = "0";
			
			//발주번호 tb_OrderNum
			document.ReceiveingRegistration.tb_OrderNum.value = "";
			//납품장소 tb_DeliveryPlace
			document.ReceiveingRegistration.tb_DeliveryPlace.value = "";
			
			
			__doPostBack('LinkButton');
			
			
					
		}
		
		function DataRegister()
		{
			if(document.ReceiveingRegistration.txtItemState.value == '양산품')
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
			__doPostBack('LinkButton3');
			
		}
		
		function UltraWebGrid1_AfterSelectChangeHandler(gridName, id){
			//Add code to handle your event here.
			var objChooser1 = igdrp_getComboById("wdcReceivingOrderDate");//수주일자
			var objChooser2 = igdrp_getComboById("wdcDeliveryRequestDate1");//납기요구일
			var row = igtbl_getRowById(id);
			document.ReceiveingRegistration.lb_Index.value = row.getCellFromKey("ReceivingOrderHistoryIndex").getValue()
		}
		
		function UltraWebGrid1_DblClickHandler(gridName, cellId){
			var row = igtbl_getRowById(cellId);
			var objChooser1 = igdrp_getComboById("wdcReceivingOrderDate");//수주일자
			var objChooser2 = igdrp_getComboById("wdcDeliveryRequestDate1");//납기요구일
			//수주일자
			objChooser1.setValue(row.getCellFromKey("ReceivingOrderDate").getValue());
			
			//품번,도번,품명
			document.ReceiveingRegistration.ItemSearchControl1_txtItemNum.value = row.getCellFromKey("ItemNum").getValue();//그리드에서 선택한 품목번호를 유저컨트롤내의 품목번호 컨트롤에 넣음
			document.ReceiveingRegistration.ItemSearchControl1_txtItemDrawNum.value = row.getCellFromKey("ItemDrawNum").getValue();//그리드에서 선택한 도면번호를 유저컨트롤내의 품목번호 컨트롤에 넣음
			document.ReceiveingRegistration.ItemSearchControl1_txtItemName.value = row.getCellFromKey("ItemName").getValue();//그리드에서 선택한 품목명를 유저컨트롤내의 품목번호 컨트롤에 넣음
			
			//단위,규격
			document.ReceiveingRegistration.tb_Unit.value = row.getCellFromKey("Unit").getValue();
			
			if(row.getCellFromKey("Standard").getValue() == null || row.getCellFromKey("Standard").getValue() == "")
				document.ReceiveingRegistration.tb_Standard.value = "";
			else
				document.ReceiveingRegistration.tb_Standard.value = row.getCellFromKey("Standard").getValue();
			
			//거래처, 사업자번호
			document.ReceiveingRegistration.CSC1_txtCompanyName.value = row.getCellFromKey("CompanyName").getValue();//그리드에서 선택한 품목명를 유저컨트롤내의 품목번호 컨트롤에 넣음
			document.ReceiveingRegistration.CSC1_txtBusinessRegistrationNum.value = row.getCellFromKey("BusinessRegistrationNum").getValue();//그리드에서 선택한 품목명를 유저컨트롤내의 품목번호 컨트롤에 넣음
			
			document.ReceiveingRegistration.hdBusinessRegistrationNum.value = row.getCellFromKey("BusinessRegistrationNum").getValue();//그리드에서 선택한 품목번호를 Hidden 필드에 넣음
			document.ReceiveingRegistration.hdItemNum.value = row.getCellFromKey("ItemNum").getValue();//WebCombo에 그리드에서 선택한 품목명을 넣음
			
			//발주량,요구일
			document.ReceiveingRegistration.tb_TotalReceiveingOrderQuantity.value = MakeCommaSeparate(row.getCellFromKey("TotalReceiveingOrderQuantity").getValue());//총수주량
			objChooser2.setValue(row.getCellFromKey("DeliveryRequestDate1").getValue());//그리드에서 선택한 1차납품요구일을 넣음
			
			//담당자 tb_CompanyPersonInCharge
			if(row.getCellFromKey("CompanyPersonInCharge").getValue() == null || row.getCellFromKey("CompanyPersonInCharge").getValue() == "")
				document.ReceiveingRegistration.tb_CompanyPersonInCharge.value = "";
			else
				document.ReceiveingRegistration.tb_CompanyPersonInCharge.value = row.getCellFromKey("CompanyPersonInCharge").getValue();
			//연락처 tb_TelephoneNum
			if(row.getCellFromKey("TelephoneNum").getValue() == null || row.getCellFromKey("TelephoneNum").getValue() == "")
				document.ReceiveingRegistration.tb_TelephoneNum.value = "";
			else
				document.ReceiveingRegistration.tb_TelephoneNum.value = row.getCellFromKey("TelephoneNum").getValue();
			//자산분류 PropertyClassification
			document.ReceiveingRegistration.tb_PropertyClassification.value = row.getCellFromKey("PropertyClassification").getValue();
			
			//품목상태 txtItemState
			if(row.getCellFromKey("ItemState").getValue() == null || row.getCellFromKey("ItemState").getValue() == "")
				document.ReceiveingRegistration.txtItemState.value = "";
			else
				
				document.ReceiveingRegistration.txtItemState.value = row.getCellFromKey("ItemState").getValue();
			
			//적용단가 및 수주단가
			document.ReceiveingRegistration.tb_ReceiveUnitCost.value = MakeCommaSeparate(row.getCellFromKey("ApplyUnitCost").getValue());//적용단가
			document.ReceiveingRegistration.tb_ApplyUnitCost.value = MakeCommaSeparate(row.getCellFromKey("ApplyUnitCost").getValue());//적용단가
			
			//발주번호 tb_OrderNum
			if(row.getCellFromKey("OrderNum").getValue() == null || row.getCellFromKey("OrderNum").getValue() == "")
				document.ReceiveingRegistration.tb_OrderNum.value = "";
			else
				document.ReceiveingRegistration.tb_OrderNum.value = row.getCellFromKey("OrderNum").getValue();
				
			//납품장소 tb_DeliveryPlace
			if(row.getCellFromKey("DeliveryPlace").getValue() == null || row.getCellFromKey("DeliveryPlace").getValue() == "")
				document.ReceiveingRegistration.tb_DeliveryPlace.value = "";
			else
				document.ReceiveingRegistration.tb_DeliveryPlace.value = row.getCellFromKey("DeliveryPlace").getValue();
			
			//생산의뢰여부 ProductionRequestDivision
			if(row.getCellFromKey("ProductionRequestDivision").getValue() =="예")
				document.ReceiveingRegistration.dlProductionRequestDivision.options[0].selected=true;
			else
				document.ReceiveingRegistration.dlProductionRequestDivision.options[1].selected=true;
			
			//총금액
			document.ReceiveingRegistration.tb_TotalCost.value = MakeCommaSeparate(row.getCellFromKey("TotalCost").getValue());//총금액			
			//수주번호
			document.ReceiveingRegistration.lb_Index.value = row.getCellFromKey("ReceivingOrderHistoryIndex").getValue();//원장 인덱스 번호
			
			
			document.ReceiveingRegistration.bt_Update.disabled = false;
		    document.ReceiveingRegistration.bt_Delete.disabled = false;
		    document.ReceiveingRegistration.btRegister.disabled = true;
			
		}
		
		
		function Process()	
		{
			var total = Number(RemoveComma(ReceiveingRegistration.tb_TotalReceiveingOrderQuantity.value));
			//var total = Number(RemoveComma(ReceiveingRegistration.tb_TotalCost.value));
			//document.ReceiveingRegistration.tb_TotalReceiveingOrderQuantity.value = MakeCommaSeparate((Math.round((total * 10000)) / 10000));
			//document.ReceiveingRegistration.tb_TotalReceiveingOrderQuantity.value = MakeCommaSeparate(total);
			var cost = total * Number(RemoveComma(ReceiveingRegistration.tb_ApplyUnitCost.value));
			ReceiveingRegistration.tb_TotalCost.value = MakeCommaSeparate(Math.round(cost));
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
					
					function DoPost()
					{
						__doPostBack('LinkButton1','');
					}
					
					function DoPost1()
					{
						__doPostBack('LinkButton2','');
					}
				
		</script>
	</HEAD>
	<body bgColor="#f7f6f6" MS_POSITIONING="GridLayout">
		<form id="ReceiveingRegistration" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; WIDTH: 800px; POSITION: absolute; TOP: 10px; HEIGHT: 550px"
				cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD></TD>
				<TR>
					<TD height="500"><FONT face="굴림"></FONT></TD>
					<TD vAlign="top" align="center" width="800" height="500">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: #696969 2px solid; WIDTH: 800px; BORDER-BOTTOM: #696969 2px solid"><LEGEND style="FONT-SIZE: 9pt" align="top">[입력]</LEGEND>
							<TABLE id="Table2" style="WIDTH: 800px; HEIGHT: 139px" cellSpacing="0" cellPadding="0"
								width="800">
								<TR>
									<TD align="left" width="200" colSpan="2" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림"><uc1:companysearchcontrol id="CSC1" runat="server"></uc1:companysearchcontrol></FONT></TD>
									<TD align="right" width="70" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림">담당자&nbsp;</FONT></TD>
									<TD align="left" width="130" height="30"><asp:textbox id="tb_CompanyPersonInCharge" runat="server" Font-Size="9pt" Width="100px" Height="20px"
											BackColor="#EEEEE9" BorderStyle="Inset" ReadOnly="True"></asp:textbox></TD>
									<TD align="right" width="70" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림">연락처&nbsp;</FONT></TD>
									<TD align="left" width="130" height="30"><asp:textbox id="tb_TelephoneNum" runat="server" Font-Size="9pt" Width="100px" Height="20px"
											BackColor="#EEEEE9" BorderStyle="Inset" ReadOnly="True"></asp:textbox></TD>
									<TD align="right" width="70" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림">수주일자&nbsp;</FONT></TD>
									<TD align="left" width="130" height="30"><igsch:webdatechooser id="wdcReceivingOrderDate" runat="server" Width="100px" Height="20px" BackColor="#EEEEE9"
											NullDateLabel=" " Text="Null">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderStyle="Inset" BackColor="#EEEEE9"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 4px" align="right" width="600" colSpan="6"><FONT style="FONT-SIZE: 9pt" face="굴림">
											<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="600" border="0">
												<TR>
													<TD><FONT face="굴림"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol><FONT face="굴림"><FONT style="FONT-SIZE: 9pt" face="굴림"><FONT style="FONT-SIZE: 9pt" face="굴림"><FONT style="FONT-SIZE: 9pt" face="굴림"><FONT face="굴림"><FONT style="FONT-SIZE: 9pt" face="굴림"></FONT><FONT style="FONT-SIZE: 9pt" face="굴림"></FONT><FONT face="굴림"></FONT></FONT></FONT></FONT></FONT></FONT></FONT></TD>
												</TR>
											</TABLE>
										</FONT>
									</TD>
									<TD style="HEIGHT: 4px" align="right" width="70"><FONT style="FONT-SIZE: 9pt" face="굴림">자산분류&nbsp;</FONT></TD>
									<TD style="HEIGHT: 4px" align="left" width="130"><asp:textbox id="tb_PropertyClassification" runat="server" Font-Size="9pt" Width="100px" Height="20px"
											BackColor="#EEEEE9" BorderStyle="Inset" ReadOnly="True"></asp:textbox></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 9pt" align="right" width="70" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림" size="3">품목상태&nbsp;
										</FONT>
									</TD>
									<TD align="left" width="130" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림"><asp:textbox id="txtItemState" runat="server" Font-Size="9pt" Width="100px" Height="20px" BackColor="#EEEEE9"
												BorderStyle="Inset" ReadOnly="True"></asp:textbox></FONT></TD>
									<TD align="right" width="70" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림">단위&nbsp;</FONT></TD>
									<TD align="left" width="130" height="30"><asp:textbox id="tb_Unit" runat="server" Font-Size="9pt" Width="100px" Height="20px" BackColor="#EEEEE9"
											BorderStyle="Inset" ReadOnly="True"></asp:textbox></TD>
									<TD align="right" width="70" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림">규격&nbsp;</FONT></TD>
									<TD align="left" width="130" height="30"><FONT face="굴림"></FONT><asp:textbox id="tb_Standard" runat="server" Font-Size="9pt" Width="100px" Height="20px" BackColor="#EEEEE9"
											BorderStyle="Inset" ReadOnly="True"></asp:textbox></TD>
									<TD align="right" width="70" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림">수주단가&nbsp;</FONT></TD>
									<TD align="left" width="130" height="30"><asp:textbox id="tb_ReceiveUnitCost" style="TEXT-ALIGN: right" runat="server" Font-Size="9pt"
											Width="100px" Height="20px" BackColor="#EEEEE9" BorderStyle="Inset" ReadOnly="True">0</asp:textbox></TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30"><FONT face="굴림">생산의뢰&nbsp;</FONT></TD>
									<TD align="left" width="130" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림"><FONT face="굴림"><FONT style="FONT-SIZE: 9pt" face="굴림"></FONT><FONT style="FONT-SIZE: 9pt" face="굴림"></FONT><FONT face="굴림"><asp:dropdownlist id="dlProductionRequestDivision" runat="server" Font-Size="9pt" Width="100px" Height="20px"
														BackColor="#EEEEE9">
														<asp:ListItem Value="1">예</asp:ListItem>
														<asp:ListItem Value="0">아니오</asp:ListItem>
													</asp:dropdownlist></FONT></FONT></FONT></TD>
									<TD align="right" width="70" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림"><FONT style="FONT-SIZE: 9pt" face="굴림">발주번호&nbsp;
											</FONT></FONT>
									</TD>
									<TD align="left" width="130" height="30"><asp:textbox id="tb_OrderNum" runat="server" Font-Size="9pt" Width="100px" Height="20px" BackColor="#EEEEE9"
											BorderStyle="Inset"></asp:textbox></TD>
									<TD align="right" width="70" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림"><FONT style="FONT-SIZE: 9pt" face="굴림">납품장소&nbsp;
											</FONT></FONT>
									</TD>
									<TD align="left" width="130" height="30"><asp:textbox id="tb_DeliveryPlace" runat="server" Font-Size="9pt" Width="100px" Height="20px"
											BackColor="#EEEEE9" BorderStyle="Inset"></asp:textbox></TD>
									<TD align="right" width="70" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림">적용단가&nbsp;</FONT></TD>
									<TD align="left" width="130" height="30"><FONT face="굴림"></FONT><asp:textbox id="tb_ApplyUnitCost" style="TEXT-ALIGN: right" runat="server" Font-Size="9pt" Width="100px"
											Height="20px" BackColor="#EEEEE9" BorderStyle="Inset">0</asp:textbox></TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30"><FONT face="굴림"></FONT></TD>
									<TD align="left" width="130" height="30"></TD>
									<TD align="right" width="70" height="30"><FONT face="굴림">총 수주량&nbsp; </FONT>
									</TD>
									<TD align="left" width="130" height="30"><asp:textbox id="tb_TotalReceiveingOrderQuantity" style="TEXT-ALIGN: right" runat="server" Font-Size="9pt"
											Width="100px" Height="20px" BackColor="#EEEEE9" BorderStyle="Inset">0</asp:textbox></TD>
									<TD align="right" width="70" height="30"><FONT face="굴림">납기요구일</FONT></TD>
									<TD align="left" width="130" height="30"><igsch:webdatechooser id="wdcDeliveryRequestDate1" runat="server" Width="100px" Height="20px" BackColor="#EEEEE9"
											NullDateLabel=" " Text="Null">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderStyle="Solid" BackColor="#EEEEE9"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD align="right" width="70" height="30"><FONT face="굴림">총 금액&nbsp; &nbsp;</FONT></TD>
									<TD align="left" width="130" height="30"><asp:textbox id="tb_TotalCost" style="TEXT-ALIGN: right" runat="server" Font-Size="9pt" Width="100px"
											Height="20px" BackColor="#EEEEE9" BorderStyle="Inset" ReadOnly="True">0</asp:textbox></TD>
								</TR>
								<TR>
									<TD align="right" colSpan="8" height="8"></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 71px; HEIGHT: 21px" align="right"><asp:button id="Button1" runat="server" Width="60px" Height="20px" Text="재고보기" Visible="False"></asp:button></TD>
									<TD style="WIDTH: 119px; HEIGHT: 21px" align="left"><FONT face="굴림"><INPUT id="hdItemNum" style="WIDTH: 36px; HEIGHT: 18px; BACKGROUND-COLOR: #eeeee9" type="hidden"
												size="1" name="Hidden1" runat="server"><INPUT id="hdBusinessRegistrationNum" style="WIDTH: 36px; HEIGHT: 18px; BACKGROUND-COLOR: #eeeee9"
												type="hidden" size="1" name="Hidden4" runat="server"></FONT></TD>
									<TD style="FONT-SIZE: 9pt; HEIGHT: 21px" align="right" colSpan="6">
										<asp:linkbutton id="LinkButton" runat="server" Visible="False">LinkButton</asp:linkbutton>
										<asp:linkbutton id="LinkButton3" runat="server" Visible="False">LinkButton</asp:linkbutton>&nbsp;
										<asp:linkbutton id="LinkButton2" runat="server" Visible="False">LinkButton</asp:linkbutton><asp:linkbutton id="LinkButton1" runat="server" Visible="False">LinkButton</asp:linkbutton><INPUT id="lb_Index" style="WIDTH: 27px; HEIGHT: 16px" type="hidden" size="1" value="0"
											runat="server"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
											type="button" value="초기화" name="btnReset">&nbsp;
										<asp:button id="bt_Update" runat="server" Width="60px" Height="20px" Text="수  정" Enabled="False"></asp:button>&nbsp;
										<asp:button id="bt_Delete" runat="server" Width="60px" Height="20px" Text="삭  제" Enabled="False"></asp:button>&nbsp;
										<INPUT id="btRegister" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:DataRegister();"
											type="button" value="등  록" name="btnReset" runat="server">&nbsp;</TD>
								</TR>
								<TR>
									<TD style="WIDTH: 71px" align="right" colSpan="8" height="8"><FONT face="굴림"></FONT></TD>
								</TR>
							</TABLE>
						</FIELDSET>
						<BR>
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 806px; BORDER-BOTTOM: #696969 2px solid; HEIGHT: 270px"><LEGEND style="FONT-SIZE: 9pt" align="top">[입력결과]</LEGEND><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="250px">
								<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
									RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
									BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1"
									TableLayout="Fixed" CellClickActionDefault="RowSelect">
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
									<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
									<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
										BackColor="Silver" Height="250px"></FrameStyle>
									<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</FooterStyleDefault>
									<ClientSideEvents AfterSelectChangeHandler="UltraWebGrid1_AfterSelectChangeHandler" DblClickHandler="UltraWebGrid1_DblClickHandler"></ClientSideEvents>
									<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
									<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
									<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
									<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
										<Padding Left="3px"></Padding>
										<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
									</RowStyleDefault>
								</DisplayLayout>
								<Bands>
									<igtbl:UltraGridBand AllowUpdate="RowTemplateOnly">
										<Columns>
											<igtbl:UltraGridColumn HeaderText="품목상태" Key="ItemState" Width="80px" BaseColumnName="ItemState"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" HeaderClickAction="Select" BaseColumnName="ItemNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
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
											<igtbl:UltraGridColumn HeaderText="수주일자" Key="ReceivingOrderDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="ReceivingOrderDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="총수주량" Key="TotalReceiveingOrderQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
												BaseColumnName="TotalReceiveingOrderQuantity">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="납기요구일" Key="DeliveryRequestDate1" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="DeliveryRequestDate1">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
												BaseColumnName="ApplyUnitCost">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="총금액" Key="TotalCost" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
												BaseColumnName="TotalCost">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="자산분류" Key="PropertyClassification" HeaderClickAction="SortMulti" BaseColumnName="PropertyClassification">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="생산의뢰여부" Key="ProductionRequestDivision" HeaderClickAction="SortMulti"
												BaseColumnName="ProductionRequestDivision">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="출고수량" Key="OutStorehouseQuantity" Hidden="True" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="OutStorehouseQuantity">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="적합수량" Key="SuitabilityQuantity" Hidden="True" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="SuitabilityQuantity">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="미검수량" Key="UnInspectionQuantity" Hidden="True" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="UnInspectionQuantity">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="납품잔량" Key="RemainderQuantity" Hidden="True" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="RemainderQuantity">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="발주번호" Key="OrderNum" HeaderClickAction="SortMulti" BaseColumnName="OrderNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="납품장소" Key="DeliveryPlace" HeaderClickAction="SortMulti" BaseColumnName="DeliveryPlace">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="ProgressCondition">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="RegistrationPerson">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="RegistrationPersonID">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="RegistrationDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="UpdatingPerson">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="UpdatingPersonID">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="UpdatingDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="번호" Key="ReceivingOrderHistoryIndex" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="ReceivingOrderHistoryIndex">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="단위" Key="Unit" Hidden="True" BaseColumnName="Unit"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="규격" Key="Standard" Hidden="True" BaseColumnName="Standard"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="담당자" Key="CompanyPersonInCharge" Hidden="True" BaseColumnName="CompanyPersonInCharge"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="연락처" Key="TelephoneNum" Hidden="True" BaseColumnName="TelephoneNum"></igtbl:UltraGridColumn>
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
