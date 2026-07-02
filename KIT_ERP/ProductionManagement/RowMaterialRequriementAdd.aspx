<%@ Page language="c#" Codebehind="RowMaterialRequriementAdd.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.RowMaterialRequriementAdd" codePage="949" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>RowMaterialRequriementAdd</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<LINK href="../LinkLine.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../MessageWindows.js"></script>
		<SCRIPT type="text/javascript">
		<!--
		// 추가시 날짜 문제 체크
		function Add(message)
		{
			if(confirm(message))
			{
				var date1 = igdrp_getComboById("wdcFirstDeliveryDemandDate");
				var date2 = igdrp_getComboById("wdcSecondDeliveryDemandDate");
				var date3 = igdrp_getComboById("wdcThirdDeliveryDemandDate");
				var date4 = igdrp_getComboById("wdcFourthDeliveryDemandDate");
				var date5 = igdrp_getComboById("wdcFifthDeliveryDemandDate");
				
				quantity =  new Array(5);//1차부터 5차까지의 납품요구량을 a 배열에 넣기 위해 배열선언
				date =  new Array(5);//1차부터 5차까지의 납품요구일을 b 배열에 넣기 위해 배열선언
				
				var first, second, third, fourth, fifth;
				
				// 1차 납기량
				if(Form1.txtFirstDeliveryDemandQuantity != null || Form1.txtFirstDeliveryDemandQuantity.value != "")
					quantity[0] = Number(RemoveComma(Form1.txtFirstDeliveryDemandQuantity.value));
				else
					quantity[0] = 0;
				
				// 2차 납기량
				if(Form1.txtSecondDeliveryDemandQuantity != null || Form1.txtSecondDeliveryDemandQuantity.value != "")
					quantity[1] = Number(RemoveComma(Form1.txtSecondDeliveryDemandQuantity.value));
				else
					quantity[1] = 0;
				
				// 3차 납기량
				if(Form1.txtThirdDeliveryDemandQuantity != null || Form1.txtThirdDeliveryDemandQuantity.value != "")
					quantity[2] = Number(RemoveComma(Form1.txtThirdDeliveryDemandQuantity.value));
				else
					quantity[2] = 0;
				
				// 4차 납기량
				if(Form1.txtFourthDeliveryDemandQuantity != null || Form1.txtFourthDeliveryDemandQuantity.value != "")
					quantity[3] = Number(RemoveComma(Form1.txtFourthDeliveryDemandQuantity.value));
				else
					quantity[3] = 0;
				
				// 5차 납기량
				if(Form1.txtFifthDeliveryDemandQuantity != null || Form1.txtFifthDeliveryDemandQuantity.value != "")
					quantity[4] = Number(RemoveComma(Form1.txtFifthDeliveryDemandQuantity.value));
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
		
		// 납기량 입력시 onkeyup 이벤트 때 발생.
		function Process()	
		{
			var first, second, third, fourth, fifth;
			
			// 1차 납기량
			if(Form1.txtFirstDeliveryDemandQuantity != null || Form1.txtFirstDeliveryDemandQuantity.value != "")
				first = Number(RemoveComma(Form1.txtFirstDeliveryDemandQuantity.value));
			else
				first = 0;
			
			// 2차 납기량
			if(Form1.txtSecondDeliveryDemandQuantity != null || Form1.txtSecondDeliveryDemandQuantity.value != "")
				second = Number(RemoveComma(Form1.txtSecondDeliveryDemandQuantity.value));
			else
				second = 0;
			
			// 3차 납기량
			if(Form1.txtThirdDeliveryDemandQuantity != null || Form1.txtThirdDeliveryDemandQuantity.value != "")
				third = Number(RemoveComma(Form1.txtThirdDeliveryDemandQuantity.value));
			else
				third = 0;
			
			// 4차 납기량
			if(Form1.txtFourthDeliveryDemandQuantity != null || Form1.txtFourthDeliveryDemandQuantity.value != "")
				fourth = Number(RemoveComma(Form1.txtFourthDeliveryDemandQuantity.value));
			else
				fourth = 0;
			
			// 5차 납기량
			if(Form1.txtFifthDeliveryDemandQuantity != null || Form1.txtFifthDeliveryDemandQuantity.value != "")
				fifth = Number(RemoveComma(Form1.txtFifthDeliveryDemandQuantity.value));
			else
				fifth = 0;
				
			value = first + second + third + fourth + fifth;
				
			Form1.txtOrderQuantity.value = MakeCommaSeparate((Math.round((value * 100)) / 100));
			Form1.txtTotalCost.value = MakeCommaSeparate((Math.round((value * Form1.applyUnitCost.value * 100)) / 100));
		}
		

//////////////////////////////////////////////////////////////////////////////////////////
//										Float						        			//
//////////////////////////////////////////////////////////////////////////////////////////
		
		function OnKeyDown_Float(obj) {   
		
			// 입력수량 TextBox 객체의 value로 배열을 만든다.
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
//										Currency										//
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
				event.keyCode == 110 ||		// "."
				event.keyCode == 190		// 키패드 "."
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
		function wcbItemName_AfterSelectChange(webComboId){
			var grid = igcmbo_getComboById(webComboId).getGrid();
			var row = grid.Rows.getRow(igcmbo_getComboById(webComboId).getSelectedIndex());
			document.Form1.applyUnitCost.value = row.getCellFromKey("StandardUnitCost").getValue();
			document.Form1.txtItemDrawNum.value = row.getCellFromKey("ItemDrawNum").getValue();
			Process();
		}
		function uwgBR_HT_DblClickHandler(gridName, cellId){
			var row = igtbl_getRowById(cellId);
			//var combo = igcmbo_getComboById("wcbItemName");
			var date1 = igdrp_getComboById("wdcFirstDeliveryDemandDate");
			var date2 = igdrp_getComboById("wdcSecondDeliveryDemandDate");
			var date3 = igdrp_getComboById("wdcThirdDeliveryDemandDate");
			var date4 = igdrp_getComboById("wdcFourthDeliveryDemandDate");
			var date5 = igdrp_getComboById("wdcFifthDeliveryDemandDate");
			
		
			//combo.setDataValue(row.getCellFromKey("ItemNum").getValue());
			//combo.setDisplayValue(row.getCellFromKey("ItemName").getValue());
			date1.setValue(row.getCellFromKey("FirstDeliveryDemandDate").getValue());
			date2.setValue(row.getCellFromKey("SecondDeliveryDemandDate").getValue());
			date3.setValue(row.getCellFromKey("ThirdDeliveryDemandDate").getValue());
			date4.setValue(row.getCellFromKey("FourthDeliveryDemandDate").getValue());
			date5.setValue(row.getCellFromKey("FifthDeliveryDemandDate").getValue());
			document.Form1.ItemSearchControl1_txtItemNum.value = row.getCellFromKey("ItemNum").getValue();
			document.Form1.ItemSearchControl1_txtItemDrawNum.value = row.getCellFromKey("ItemDrawNum").getValue();
			document.Form1.ItemSearchControl1_txtItemName.value = row.getCellFromKey("ItemName").getValue();
					
			document.Form1.txtFirstDeliveryDemandQuantity.value = MakeCommaSeparate(row.getCellFromKey("FirstDeliveryDemandQuantity").getValue());
			document.Form1.txtSecondDeliveryDemandQuantity.value = MakeCommaSeparate(row.getCellFromKey("SecondDeliveryDemandQuantity").getValue());
			document.Form1.txtThirdDeliveryDemandQuantity.value = MakeCommaSeparate(row.getCellFromKey("ThirdDeliveryDemandQuantity").getValue());
			document.Form1.txtFourthDeliveryDemandQuantity.value = MakeCommaSeparate(row.getCellFromKey("FourthDeliveryDemandQuantity").getValue());
			document.Form1.txtFifthDeliveryDemandQuantity.value = MakeCommaSeparate(row.getCellFromKey("FifthDeliveryDemandQuantity").getValue());
			document.Form1.txtOrderQuantity.value = MakeCommaSeparate(row.getCellFromKey("OrderQuantity").getValue());
			document.Form1.txtTotalCost.value = MakeCommaSeparate(row.getCellFromKey("TotalCost").getValue());
			document.Form1.applyUnitCost.value = MakeCommaSeparate(row.getCellFromKey("ApplyUnitCost").getValue());
			
			document.Form1.txtItemDrawNum.value = row.getCellFromKey("ItemDrawNum").getValue();
			document.Form1.txtHistoryIndex.value = row.getCellFromKey("BuyingRequestHistoryIndex").getValue();
		   
		    for(var i = 0; i < document.Form1.ddlBuyingRequestSource.options.length; i++)
		    {
				if(document.Form1.ddlBuyingRequestSource.options[i].innerText == row.getCellFromKey("BuyingRequestSource").getValue())
				{
					document.Form1.ddlBuyingRequestSource.options[i].selected = true;
					break;
				}
		    }
		    
		    for(var i = 0; i < document.Form1.ddlRequestPost.options.length; i++)
		    {
				if(document.Form1.ddlRequestPost.options[i].innerText == row.getCellFromKey("RequestPost").getValue())
				{
					document.Form1.ddlRequestPost.options[i].selected = true;
					break;
				}
		    }
		    
		    document.Form1.btnUpDate.disabled = false;
		    document.Form1.btnDelete.disabled = false;
		    document.Form1.btnAdd.disabled = true;
		}
		
		function DoPost()
		{
			__doPostBack("LinkButton1", "");
		}
		
		//-->
		</SCRIPT>
</HEAD>
	<body bgColor="#f7f6f6" MS_POSITIONING="GridLayout" XMLNS:igtbl="http://schemas.infragistics.com/ASPNET/WebControls/UltraWebGrid">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="LEFT: 10px; WIDTH: 800px; POSITION: absolute; TOP: 10px" cellSpacing="0"
				cellPadding="0" width="800" border="0">
				<TR>
					<TD></TD>
					<TD vAlign="top" align="left" width="800">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[입&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;력]</LEGEND>
							<TABLE id="Table2" height="30" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD colSpan="8" height="30">
										<uc1:ItemSearchControl id="ItemSearchControl1" runat="server"></uc1:ItemSearchControl>
									</TD>
								</TR>
								<TR>
									<TD align="right" height="30" width="70">
										의뢰부서&nbsp;</TD>
									<TD vAlign="middle" align="left" height="30" width="120">
										<asp:dropdownlist id="ddlRequestPost" runat="server" Height="18px" Width="120px" BackColor="#EEEEE9"
											Font-Size="10pt"></asp:dropdownlist></TD>
									<TD vAlign="middle" align="right" height="30" width="100">구매의뢰 원천&nbsp;</TD>
									<TD align="left" height="30" width="100">
										<asp:dropdownlist id="ddlBuyingRequestSource" runat="server" Height="18px" Width="100px" BackColor="#EEEEE9"
											Font-Size="10pt"></asp:dropdownlist></TD>
									<TD vAlign="middle" align="center" height="30" width="200" colSpan="2"></TD>
									<TD align="right" height="30" width="210"></TD>
									<TD vAlign="middle" align="right" height="30"></TD>
								</TR>
								<TR>
									<TD vAlign="middle" width="100%" colSpan="8" align="center"><TABLE id="Table4" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BORDER-BOTTOM: dimgray 1px solid"
											cellSpacing="0" cellPadding="0" width="770" border="1" align="center" borderColorDark="white" borderColorLight="dimgray">
											<TR>
												<TD vAlign="middle" align="center" bgColor="#dcdcdc" height="23" width="70">구분</TD>
												<TD vAlign="middle" align="center" bgColor="gainsboro" height="23" width="100">1차</TD>
												<TD vAlign="middle" align="center" bgColor="gainsboro" height="23" width="100">2차</TD>
												<TD vAlign="middle" align="center" width="100" bgColor="gainsboro" height="23">3차</TD>
												<TD vAlign="middle" align="center" bgColor="gainsboro" height="23" width="100">4차</TD>
												<TD vAlign="middle" align="center" bgColor="gainsboro" height="23" width="100">5차</TD>
												<TD vAlign="middle" align="center" bgColor="gainsboro" height="23" width="110"><STRONG>총구매량</STRONG></TD>
												<TD vAlign="middle" align="center" bgColor="gainsboro" height="23" width="110"><STRONG>금액</STRONG></TD>
											</TR>
											<TR>
												<TD align="center" height="35">
													납기량
												</TD>
												<TD align="center" height="35" width="100"><INPUT onkeypress="if(event.keyCode==13){return false;}" id="txtFirstDeliveryDemandQuantity"
														onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Cur(this);" onkeyup="Process(); OnKeyUp_Currency(this);" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 90px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: #eeeee9; TEXT-ALIGN: right"
														onfocus="OnFocus_Obj(this);" type="text" name="txtFirstDeliveryDemandQuantity" runat="server"></TD>
												<TD align="center" height="35" width="100"><INPUT onkeypress="if(event.keyCode==13){return false;}" id="txtSecondDeliveryDemandQuantity"
														onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Cur(this);" onkeyup="Process(); OnKeyUp_Currency(this);" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 90px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: #eeeee9; TEXT-ALIGN: right"
														onfocus="OnFocus_Obj(this);" type="text" name="txtSecondDeliveryDemandQuantity" runat="server"></TD>
												<TD align="center" height="35" width="100"><INPUT onkeypress="if(event.keyCode==13){return false;}" id="txtThirdDeliveryDemandQuantity"
														onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Cur(this);" onkeyup="Process(); OnKeyUp_Currency(this);" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 90px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: #eeeee9; TEXT-ALIGN: right"
														onfocus="OnFocus_Obj(this);" type="text" name="txtThirdDeliveryDemandQuantity" runat="server"></TD>
												<TD align="center" height="35" width="100"><INPUT onkeypress="if(event.keyCode==13){return false;}" id="txtFourthDeliveryDemandQuantity"
														onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Cur(this);" onkeyup="Process(); OnKeyUp_Currency(this);" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 90px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: #eeeee9; TEXT-ALIGN: right"
														onfocus="OnFocus_Obj(this);" type="text" name="txtFourthDeliveryDemandQuantity" runat="server"></TD>
												<TD align="center" height="35" width="100"><INPUT onkeypress="if(event.keyCode==13){return false;}" id="txtFifthDeliveryDemandQuantity"
														onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Cur(this);" onkeyup="Process(); OnKeyUp_Currency(this);" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 90px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: #eeeee9; TEXT-ALIGN: right"
														onfocus="OnFocus_Obj(this);" type="text" name="txtFifthDeliveryDemandQuantity" runat="server"></TD>
												<TD align="center" width="100" rowSpan="2" height="35"><INPUT id="txtOrderQuantity" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 90px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: #eeeee9; TEXT-ALIGN: right"
														readOnly type="text" name="txtOrderQuantity" runat="server"></TD>
												<TD align="center" width="100" rowSpan="2" height="35"><INPUT id="txtTotalCost" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 90px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: #eeeee9; TEXT-ALIGN: right"
														readOnly type="text" name="txtTotalCost" runat="server"></TD>
											</TR>
											<TR>
												<TD align="center" height="35">
													납기일</TD>
												<TD align="center" width="100" height="35">
													<igsch:webdatechooser id="wdcFirstDeliveryDemandDate" runat="server" Height="20px" Width="90px" Font-Size="10pt"
														Text=" " NullDateLabel=" " BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray">
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
													<igsch:webdatechooser id="wdcSecondDeliveryDemandDate" runat="server" Height="20px" Width="90px" Font-Size="10pt"
														Text=" " NullDateLabel=" " BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray">
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
												<TD align="center" height="35" width="100">
													<igsch:webdatechooser id="wdcThirdDeliveryDemandDate" runat="server" Height="20px" Width="90px" Font-Size="10pt"
														Text=" " NullDateLabel=" " BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray">
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
													<igsch:webdatechooser id="wdcFourthDeliveryDemandDate" runat="server" Height="20px" Width="90px" Font-Size="10pt"
														Text=" " NullDateLabel=" " BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray">
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
													<igsch:webdatechooser id="wdcFifthDeliveryDemandDate" runat="server" Height="20px" Width="90px" Font-Size="10pt"
														Text=" " NullDateLabel=" " BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray">
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
									<TD style="WIDTH: 55px" vAlign="middle" align="left" height="35"><INPUT id="applyUnitCost" style="WIDTH: 81px; HEIGHT: 22px; BACKGROUND-COLOR: #eeeee9"
											type="hidden" size="8" runat="server" value="0"></TD>
									<TD style="WIDTH: 124px" vAlign="middle" align="left" height="35"><INPUT id="txtItemDrawNum" style="WIDTH: 81px; HEIGHT: 22px; BACKGROUND-COLOR: #eeeee9"
											type="hidden" size="8" name="Hidden1" runat="server"></TD>
									<TD vAlign="middle" align="right" height="35"><INPUT id="txtHistoryIndex" style="WIDTH: 63px; HEIGHT: 22px; BACKGROUND-COLOR: #eeeee9"
											type="hidden" size="5" name="Hidden2" runat="server"></TD>
									<TD vAlign="middle" align="left" height="35"><FONT face="굴림">
											<asp:LinkButton id="LinkButton1" runat="server"></asp:LinkButton></FONT></TD>
									<TD vAlign="middle" align="right" colSpan="4" height="35"><asp:button id="btnClear" runat="server" Font-Size="9pt" Width="60px" Height="20px" Text="초기화"
											BorderStyle="Outset"></asp:button>&nbsp;&nbsp;
										<asp:button id="btnUpDate" runat="server" Font-Size="9pt" Width="60px" Height="20px" Text="수   정"
											Enabled="False" BorderStyle="Outset"></asp:button>&nbsp;&nbsp;
										<asp:button id="btnDelete" runat="server" Font-Size="9pt" Width="60px" Height="20px" Text="삭   제"
											Enabled="False" BorderStyle="Outset"></asp:button>&nbsp;&nbsp;
										<asp:button id="btnAdd" runat="server" Font-Size="9pt" Width="60px" Height="20px" Text="추  가"
											BorderStyle="Outset"></asp:button>&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
						<BR>
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"
							DESIGNTIMEDRAGDROP="86"><LEGEND align="top">[입력결과]</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD vAlign="top" align="center" colSpan="3"><igtbl:ultrawebgrid id="uwgBR_HT" runat="server" Width="100%" Height="320px">
<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient" RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="uwgBRxHT" TableLayout="Fixed" CellClickActionDefault="RowSelect">

<AddNewBox>

<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
</Style>

</AddNewBox>

<Pager>

<Style VerticalAlign="Middle" BorderWidth="1px" BorderStyle="Solid" HorizontalAlign="Center" BackColor="LightGray">
</Style>

</Pager>

<HeaderStyleDefault Cursor="Hand" BorderColor="Black" ForeColor="Black" BackColor="LightGray" Height="25px">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

</HeaderStyleDefault>

<GroupByRowStyleDefault ForeColor="Black" BackColor="#95B9F0">
</GroupByRowStyleDefault>

<RowSelectorStyleDefault BorderStyle="Solid">
</RowSelectorStyleDefault>

<FrameStyle Width="100%" Cursor="Default" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DimGray" BorderStyle="Solid" BackColor="Silver" Height="320px">
</FrameStyle>

<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

</FooterStyleDefault>

<ClientSideEvents BeforeRowTemplateCloseHandler="uwgBR_HT_BeforeRowTemplateCloseHandler" BeforeRowTemplateOpenHandler="uwgBR_HT_BeforeRowTemplateOpenHandler" DblClickHandler="uwgBR_HT_DblClickHandler">
</ClientSideEvents>

<GroupByBox>

<Style BackColor="#B5CFF7">
</Style>

<BandLabelStyle ForeColor="White" BackColor="#6372D4">
</BandLabelStyle>

</GroupByBox>

<EditCellStyleDefault BorderWidth="0px" BorderStyle="None">
</EditCellStyleDefault>

<SelectedGroupByRowStyleDefault BorderColor="Yellow" BackColor="Yellow">
</SelectedGroupByRowStyleDefault>

<SelectedRowStyleDefault Cursor="Hand" Font-Bold="True" ForeColor="Linen" BackColor="Navy">
</SelectedRowStyleDefault>

<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue">
</RowAlternateStyleDefault>

<RowStyleDefault Cursor="Hand" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="Gray" BorderStyle="Solid" ForeColor="Black" BackColor="#EBEFF6">

<Padding Left="3px">
</Padding>

<BorderDetails ColorTop="Gray" WidthLeft="0px" WidthTop="0px" ColorLeft="Gray">
</BorderDetails>

</RowStyleDefault>

</DisplayLayout>

<Bands>
<igtbl:UltraGridBand>
<Columns>
<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" BaseColumnName="ItemNum">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Left">

<Padding Left="2px">
</Padding>

</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" BaseColumnName="ItemDrawNum">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" BaseColumnName="ItemName">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Left">

<Padding Left="2px">
</Padding>

</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="단위" Key="Unit" Width="40px" BaseColumnName="Unit">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="의뢰부서" Key="RequestPost" BaseColumnName="RequestPost">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="구매의뢰 원천" Key="BuyingRequestSource" BaseColumnName="BuyingRequestSource">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="1차납기량" Key="FirstDeliveryDemandQuantity" Format="###,###,##0.00" BaseColumnName="FirstDeliveryDemandQuantity">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="1차납기일" Key="FirstDeliveryDemandDate" Format="yyyy-MM-dd" BaseColumnName="FirstDeliveryDemandDate">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="2차납기량" Key="SecondDeliveryDemandQuantity" Format="###,###,##0.00" BaseColumnName="SecondDeliveryDemandQuantity">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="2차납기일" Key="SecondDeliveryDemandDate" Format="yyyy-MM-dd" BaseColumnName="SecondDeliveryDemandDate">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="3차납기량" Key="ThirdDeliveryDemandQuantity" Format="###,###,##0.00" BaseColumnName="ThirdDeliveryDemandQuantity">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="3차납기일" Key="ThirdDeliveryDemandDate" Format="yyyy-MM-dd" BaseColumnName="ThirdDeliveryDemandDate">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="4차납기량" Key="FourthDeliveryDemandQuantity" Format="###,###,##0.00" BaseColumnName="FourthDeliveryDemandQuantity">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="4차납기일" Key="FourthDeliveryDemandDate" Format="yyyy-MM-dd" BaseColumnName="FourthDeliveryDemandDate">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="5차납기량" Key="FifthDeliveryDemandQuantity" Format="###,###,##0.00" BaseColumnName="FifthDeliveryDemandQuantity">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="5차납기일" Key="FifthDeliveryDemandDate" Format="yyyy-MM-dd" BaseColumnName="FifthDeliveryDemandDate">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="총구매량" Key="OrderQuantity" Format="###,###,##0.00" BaseColumnName="OrderQuantity">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Format="\ ###,###,##0.00" BaseColumnName="ApplyUnitCost">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="금액" Key="TotalCost" Format="\ ###,###,##0.00" BaseColumnName="TotalCost">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="자재소요원장번호" Key="BuyingRequestHistoryIndex" Hidden="True" BaseColumnName="BuyingRequestHistoryIndex">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
</Columns>

<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">

<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px">
</BorderDetails>

</RowTemplateStyle>

<RowEditTemplate>
														<P align="center">&nbsp;</P>
													
</RowEditTemplate>
</igtbl:UltraGridBand>
</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>