<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtbl1" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="OutSideOrderPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BuyingOutside.OutSideOrderPC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../MessageWindows.js"></script>
		<script language="javascript" src="../GridCheck.js"></script>
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--	
		function ResettxtBox()
		{
			
			ResetTextBox();
			ResetBox();
			var objChooser1 = igdrp_getComboById("wdcStartDate");
			var objChooser2 = igdrp_getComboById("wdcEndDate");
			objChooser1.setValue(null);
			objChooser2.setValue(null);	
			document.Form1.ddlState.options[0].selected=true;
			document.Form1.ddlItemClassification1.options[0].selected=true;
					
		}		
			var preOrderRate;
			var OrderQuantity;
			var first1;
			var second1;
			var third1;
			var fourth1;
			var fifth1;

		//Row Template 창이 열리기 전에 해야할 일
		function uwgOO_HT_BeforeRowTemplateOpenHandler(gridName, rowId, templateId)
		{
			
			var row = igtbl_getRowById(rowId);		
			var parts = rowId.split("_");
				document.Form1.hdRowIndex.value = parts[1];//그리드의 선택되어진 RowIndex를 저장하는 Hidden 필드
				
			if(row.getCellFromKey("ProgressCondition").getValue() != "대기")
			{
				alert("수정할 수 없는 항목 입니다.!");
				return true;
			}			
		}
		
		//Row Template 을 열면 각 켄트롤에 해당 값들이 들어간다.	
		function uwgOO_HT_AfterRowTemplateOpenHandler(gridName, rowId){
			
			var row = igtbl_getRowById(rowId);
			
			var combo1 = igdrp_getComboById("uwgOOxHTxxctl0xFirstDate");
			var combo2 = igdrp_getComboById("uwgOOxHTxxctl0xSecondDate");
			var combo3 = igdrp_getComboById("uwgOOxHTxxctl0xThirdDate");
			var combo4 = igdrp_getComboById("uwgOOxHTxxctl0xFourthDate");
			var combo5 = igdrp_getComboById("uwgOOxHTxxctl0xFifthDate");	
			
			
			
			preOrderRate = row.getCellFromKey("OrderRate").getValue();//발주비율
			OrderQuantity = Number(row.getCellFromKey("OrderQuantity").getValue())/Number(preOrderRate) * 100; // 발주의뢰한 수량;
			
			first1 =  row.getCellFromKey("FirstDeliveryDemandQuantity").getValue();//1차 납기요구량
			second1 = row.getCellFromKey("SecondDeliveryDemandQuantity").getValue();//2차 납기요구량
			third1 = row.getCellFromKey("ThirdDeliveryDemandQuantity").getValue();//3차납기 요구량
			fourth1 = row.getCellFromKey("FourthDeliveryDemandQuantity").getValue();//4차 납기 요구량
			fifth1 = row.getCellFromKey("FifthDeliveryDemandQuantity").getValue();//5차납기요구량
			
			
			combo1.setValue(row.getCellFromKey("FirstDeliveryDemandDate").getValue());
			combo2.setValue(row.getCellFromKey("SecondDeliveryDemandDate").getValue());
			combo3.setValue(row.getCellFromKey("ThirdDeliveryDemandDate").getValue());	
			combo4.setValue(row.getCellFromKey("FourthDeliveryDemandDate").getValue());	
			combo5.setValue(row.getCellFromKey("FifthDeliveryDemandDate").getValue());
		}
		
		//Row Template 을 닫는 순간에 각 컨트롤의 값들이 해당 그리드의  해당Row에 값들이 들어간다.
		function uwgOO_HT_AfterRowTemplateCloseHandler(gridName, rowId, bSaveChanges)
		{
			
			var combo1 = igdrp_getComboById("uwgOOxHTxxctl0xFirstDate");
			var combo2 = igdrp_getComboById("uwgOOxHTxxctl0xSecondDate");
			var combo3 = igdrp_getComboById("uwgOOxHTxxctl0xThirdDate");
			var combo4 = igdrp_getComboById("uwgOOxHTxxctl0xFourthDate");
			var combo5 = igdrp_getComboById("uwgOOxHTxxctl0xFifthDate");
			
			var row =  igtbl_getRowById(rowId);
			var frm = document.Form1;			
			
			Check = true;
		
			if(Check)
			{
				
				//1차부터 5차까지의 납품요구일이 제대로 입력이 되고 그리드에 수정된 내용이 들어간다.
				row.getCellFromKey("FirstDeliveryDemandDate").setValue(combo1.getValue());
				row.getCellFromKey("SecondDeliveryDemandDate").setValue(combo2.getValue());
				row.getCellFromKey("ThirdDeliveryDemandDate").setValue(combo3.getValue());
				row.getCellFromKey("FourthDeliveryDemandDate").setValue(combo4.getValue());
				row.getCellFromKey("FifthDeliveryDemandDate").setValue(combo5.getValue());
				
				
				//1차부터 5차까지의 납품요구량이 제대로 입력이 되고 그리드에 수정된 내용이 들어간다.
				row.getCellFromKey("FirstDeliveryDemandQuantity").setValue(frm.txtTotalQuantity.value);
				
				if(frm.SecondQuantity.value =="")
					row.getCellFromKey("SecondDeliveryDemandQuantity").setValue(0);
				else
					row.getCellFromKey("SecondDeliveryDemandQuantity").setValue(frm.SecondQuantity.value);
				
				if(frm.ThirdQuantity.value =="")
					row.getCellFromKey("ThirdDeliveryDemandQuantity").setValue(0);
				else
					row.getCellFromKey("ThirdDeliveryDemandQuantity").setValue(frm.ThirdQuantity.value);
				
				if(frm.FourthQuantity.value == "")
					row.getCellFromKey("FourthDeliveryDemandQuantity").setValue(0);
				else
					row.getCellFromKey("FourthDeliveryDemandQuantity").setValue(frm.FourthQuantity.value);
				
				if(frm.FifthQuantity.value == "")
					row.getCellFromKey("FifthDeliveryDemandQuantity").setValue(0);
				else
				row.getCellFromKey("FifthDeliveryDemandQuantity").setValue(frm.FifthQuantity.value);
				
				
				
				row.getCellFromKey("OrderQuantity").setValue(frm.txtTotalQuantity.value);//총발주량
				row.getCellFromKey("RemainQuantity").setValue(frm.txtTotalQuantity.value);//잔량
												
				row.getCellFromKey("ApplyUnitCost").setValue(frm.txtApplyUnitCost.value); //적용단가가 그리드에 수정된 내용이 들어간다.
				row.getCellFromKey("TotalCost").setValue(Number(frm.txtTotalQuantity.value)*Number(frm.txtApplyUnitCost.value));//그리드에 수정된 총금액이 들어간다.
				
				return true;
			
						
			}
			else
			{				
				return false;
			}
		}
		
		
		
		//1차부터 5차까지 납품량 합구하는 함수
		function Process()	
		{	
			var total_Quantity = Number(Form1.FirstQuantity.value) + Number(Form1.SecondQuantity.value) + Number(Form1.ThirdQuantity.value) + Number(Form1.FourthQuantity.value) + Number(Form1.FifthQuantity.value);
			var value = Number(Form1.TotalQuantity.value) - Number(total_Quantity)
			
			if(value < 0)
			{
				alert("수정한 수량이 발주수량보다 많습니다.");
				Form1.FirstQuantity.value = "";
				Form1.SecondQuantity.value = "";
				Form1.ThirdQuantity.value = "";
				Form1.FourthQuantity.value = "";
				Form1.FifthQuantity.value = "";
				
				Form1.txtTotalQuantity.value = "";
				Form1.FirstQuantity.focus();
			}
			else
			{
				document.Form1.txtTotalQuantity.value = total_Quantity;
			}		
		}
		
		
		//발주비율을 수정했을때 각각의 박스에 값을 넣는함수
		function Process1()	
		{
			//발주비율은 0%가 될수없으므로 0%가 된다면 메세지창을 띄워주고 기존의 값들로 다시 채운다.
			if(Form1.txtOrderRate.value== "" || Form1.txtOrderRate.value == null)
			{
				alert("발주비율이 0%는 될 수 없습니다. 다시 수정해 주세요!");			
				
				var first = first1; //1차 납기량박스에 처음에 그리드에 있던 발주량이 들어감
				var second = second1;
				var third = third1;
				var fourth = fourth1;
				var fifth = fifth1;
				var orderrate = preOrderRate;
							
				Form1.TotalQuantity.value = Number(first)+Number(second)+Number(third)+Number(fourth)+Number(fifth);
				
				Form1.FirstQuantity.value = first;
				Form1.SecondQuantity.value = second;
				Form1.ThirdQuantity.value = third;
				Form1.FourthQuantity.value = fourth;
				Form1.FifthQuantity.value = fifth;				
								
				Form1.txtTotalQuantity.value = Form1.TotalQuantity.value;				
				
			}
			else if(Form1.txtOrderRate.value > 100)
			{
				alert("발주비율이 100%를 넘을 수 없습니다. 다시 수정해 주세요!");			
				
				var first = first1; //1차 납기량박스에 처음에 그리드에 있던 발주량이 들어감
				var second = second1;
				var third = third1;
				var fourth = fourth1;
				var fifth = fifth1;	
				var orderrate = preOrderRate;
				
				Form1.TotalQuantity.value = Number(first)+Number(second)+Number(third)+Number(fourth)+Number(fifth);
				
				Form1.FirstQuantity.value = first;
				Form1.SecondQuantity.value = second;
				Form1.ThirdQuantity.value = third;
				Form1.FourthQuantity.value = fourth;
				Form1.FifthQuantity.value = fifth;		
								
				Form1.txtTotalQuantity.value = Form1.TotalQuantity.value;
				
			}
			//발주수량을 고치면 고친 발주비율별로 수량이 조정이 된다.
			else
			{
				//var orderrate = preOrderRate;
				var first = Number(Form1.txtOrderRate.value)/Number(preOrderRate) * Number(first1); //발주비율을 수정하고 난뒤의 1차량
				var second = Number(Form1.txtOrderRate.value)/Number(preOrderRate) * Number(second1); //발주비율을 수정하고 난뒤의 2차량
				var third = Number(Form1.txtOrderRate.value)/Number(preOrderRate) * Number(third1); //발주비율을 수정하고 난뒤의 3차량
				var fourth = Number(Form1.txtOrderRate.value)/Number(preOrderRate) * Number(fourth1); //발주비율을 수정하고 난뒤의 4차량
				var fifth = Number(Form1.txtOrderRate.value)/Number(preOrderRate) * Number(fifth1); //발주비율을 수정하고 난뒤의 5차량
				
				var total_Quantity = Number(OrderQuantity)*(Number(Form1.txtOrderRate.value)/100);//수정하고 난뒤의 발주량
								
				Form1.TotalQuantity.value = total_Quantity;
				
				Form1.FirstQuantity.value = first;
				Form1.SecondQuantity.value = second;
				Form1.ThirdQuantity.value = third;
				Form1.FourthQuantity.value = fourth;
				Form1.FifthQuantity.value = fifth;
				
				Form1.txtTotalQuantity.value = total_Quantity;				
			}
			
		}	
		
		//////////////////////////////////////////////////////////////////////////////////////////
		//															Float	        				//
		//////////////////////////////////////////////////////////////////////////////////////////
				
		function OnKeyDown_Float(obj) 
		{   
			if (event.ctrlKey || event.shiftKey || event.altKey)   
			{
				SetEventPass();
				return true;
			}
			var sFieldValue = new String(obj.value);
			// 숫자검사 + Numeric Keypad 숫자검사
			if( (event.keyCode<45 || event.keyCode>57) && (event.keyCode<96 || event.keyCode>105))  
			{
    				if( event.keyCode == 110 || event.keyCode == 190 ) 
    				{
    					// "." 가 2개있는지 검사
	    				if( sFieldValue.indexOf(".") != -1 )	
	    				{
	    					SetEventCancel();
						return obj.value;
	    				}
				}
				else if( !IsNumericKey() ) 
				{
	    				SetEventCancel();
					return obj.value;
				}	    
			}
			else
			{    
    				if( sFieldValue.indexOf(".") == -1 ) 
    				{
	    				// "0" 을 입력했을 경우 "0" 다음에 "." 가 아니면 무시하여야 한다.	
	    				// 48, 96(Numeric Keypad)
					if( sFieldValue.substr(0,1) == "0" && (event.keyCode == 48 || event.keyCode == 96) ) 
					{
						SetEventCancel();
						return obj.value;
					}
				}
			}
			SetEventPass();
			return true;
		}

		function OnFocus_Obj(obj) 
		{
			obj.select();
		}
		
		function OnBlur_Float(obj) 
		{
			if (ValidateFloat(obj.value) == false) 
			{
				obj.value = "";
    				return false;
			}
		}
		
		function ValidateFloat(a_sValue)	
		{	
			var s_FieldValue = new String(a_sValue);
			
			if (isNaN(s_FieldValue))
				return false;
				
			return true;
		}

//////////////////////////////////////////////////////////////////////////////////////////
//					Currency															//
//////////////////////////////////////////////////////////////////////////////////////////
		function OnKeyDown_Currency(obj)
		{   
			if (event.ctrlKey || event.shiftKey || event.altKey) 
			{
				SetEventPass();
				return true;
			}
			var sFieldValue = new String(obj.value);        
			if( (event.keyCode<45 || event.keyCode>57) && (event.keyCode<96 || event.keyCode>105))	 
			{
    				if( event.keyCode == 110 || event.keyCode == 190 )	// "."(점) 이라면
    				{
    					// "." 가 2개있는지 검사
	    				if( sFieldValue.indexOf(".") != -1 ) 	
	    				{
	    					SetEventCancel();
						return obj.value;
	    				}
				} 
				else if( !IsCurrencyKey() )
				{
	    				SetEventCancel();
					return obj.value;
				}	    
			}
			else	
			{    
    				if( sFieldValue.indexOf(".") == -1 )	
    				{
	    				// "0" 을 입력했을 경우 "0" 다음에 "." 가 아니면 무시하여야 한다.	
	    				// 48, 96(Numeric Keypad)
					if( sFieldValue.substr(0,1) == "0" && (event.keyCode == 48 || event.keyCode == 96) )	
					{
						SetEventCancel();
						return obj.value;
					}
				}
			}
			SetEventPass();
			return true;
		}
		
		function OnKeyUp_Currency(obj)		
		{
			sFieldValue = new String(obj.value);
			sFieldValue = sFieldValue.replace(/,/gi,"");
			if ( IsNumericKey() )		
			{
				SetEventPass();
				return true;
			}
			obj.value = MakeCommaSeparate(sFieldValue);
		}
		
		function OnBlur_Cur(obj)	
		{
			if (ValidateCurrency(obj.value) == false) 
			{
				obj.value = "";
    				return false;
			}
		}
		
		function ValidateCurrency( a_sValue ) 
		{	
			var s_FieldValue = new String(a_sValue);
			
			if (isNaN(parseFloat(s_FieldValue)))
				return false;
				
			return true;
		}

		// OnKeyUp_Currency 에서 호출할 소수점 찍기
		function MakeCommaSeparate(a_Value)	
		{
			var fl = "";
			var nPointPos = 0;    
			
			if(isNaN(a_Value)) 	
			{ 
    				event.returnValue = false;
    				this.value = "";
    				return "";
			}
			
			if(a_Value == 0) 
				return a_Value;
			
			if(a_Value < 0) 
			{ 
				a_Value=a_Value*(-1);
				fl = "-";
			}
			else if(a_Value == 0)	
			{
    				// 처음 입력값이 0부터 시작할때 이것을 제거한다.
    				a_Value = a_Value*1; 
			}
			
			var a_Value = new String(a_Value);
			var temp = "";
			var sRemain = "";
			var co = 3;
			
			nPointPos = a_Value.indexOf(".");

			if( nPointPos == -1 )  
			{
    				num_len = a_Value.length;
			}
			else
			{
    				// "." 가 포함되어 있을 경우에 재계산
    				if( parseInt(a_Value.substr(0,nPointPos)) == 0 )	
    				{
    					a_Value = "0" + a_Value.substr(nPointPos);
    					nPointPos = a_Value.indexOf(".");
    				}
    				num_len = nPointPos;
    				sRemain = a_Value.substr(nPointPos);
			}
			
			while (num_len>0)	
			{
				num_len = num_len - co;
				
				if(num_len<0)	
				{
        				co=num_len+co;
        				num_len=0;
				}
				temp = "," + a_Value.substr(num_len,co) + temp;
			}
			
			return fl + temp.substr(1) + sRemain;
		}
		
		function IsNumericKey()  
		{
			if (event.keyCode == 8 || event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57 || event.keyCode == 190)
	   			return true;
			else
    				return false;
		}
							
		function SetEventPass() 
		{
				event.cancelBubble = false;
				event.returnValue = true;
		}

		function SetEventCancel() 
		{
				event.cancelBubble = true;
				event.returnValue = false;
		}
	


--></SCRIPT>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; POSITION: absolute; TOP: 10px; LEFT: 10px" height="550"
				cellSpacing="0" cellPadding="0" width="800" bgColor="#f7f6f6" border="0">
				<TR>
					<TD width="20" height="60"><FONT face="굴림"></FONT></TD>
					<TD height="60">&nbsp;
						<FIELDSET style="BORDER-BOTTOM: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 811px; HEIGHT: 78px; BORDER-TOP: dimgray 2px solid; BORDER-RIGHT: dimgray 2px solid"
							align="top"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색 ]
							</LEGEND>
							<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
								<TR height="35">
									<TD align="left" width="600" colSpan="9" height="30">
										<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="800" border="0">
											<TR>
												<TD align="left" width="200" height="30"><uc1:companysearchcontrol id="CSC1" runat="server"></uc1:companysearchcontrol></TD>
												<TD align="left" width="600" height="30"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30">진행상태&nbsp;</TD>
									<TD width="100" height="30">
										<asp:dropdownlist id="ddlState" runat="server" Width="100px" BackColor="#EEEEE9" Height="20px" Font-Size="10pt">
											<asp:ListItem Value="전체">전체</asp:ListItem>
											<asp:ListItem Value="미납">미납</asp:ListItem>
											<asp:ListItem Value="중단">중단</asp:ListItem>
											<asp:ListItem Value="완료">완료</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD align="right" width="90" height="30">납기요구일&nbsp;</TD>
									<TD align="left" width="100" height="30"><igsch:webdatechooser id="wdcStartDate" runat="server" Height="20px" BackColor="#EEEEE9" Width="100px"
											Text=" " NullDateLabel=" " BorderStyle="Solid" BorderColor="DimGray">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderWidth="1px" BorderStyle="Solid"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD align="center" width="10" height="30">~</TD>
									<TD width="100" height="30"><igsch:webdatechooser id="wdcEndDate" runat="server" Height="20px" BackColor="#EEEEE9" Width="100px" Text=" "
											NullDateLabel=" " BorderStyle="Solid" BorderColor="DimGray">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderWidth="1px" BorderStyle="Solid"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD align="right" width="70">제품팀</TD>
									<TD align="left" width="100"><asp:dropdownlist id="ddlItemClassification1" runat="server" BackColor="#EEEEE9" Width="115px"></asp:dropdownlist></TD>
									<TD align="right" width="160"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
											type="button" value="초기화" name="btnReset">&nbsp;<asp:button id="btnSearch" runat="server" Font-Size="9pt" Height="20px" Width="60px" Text="검   색"
											CommandName="Search"></asp:button>&nbsp;</TD>
								</TR>
								<TR>
									<TD align="right" width="70" colSpan="9" height="8"></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="20" height="390"></TD>
					<TD vAlign="top" height="390">
						<FIELDSET style="BORDER-BOTTOM: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; HEIGHT: 390px; BORDER-TOP: dimgray 2px solid; BORDER-RIGHT: dimgray 2px solid"
							align="top"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색결과 ]
							</LEGEND>
							<TABLE id="Table3" style="WIDTH: 100%; HEIGHT: 400px" cellSpacing="0" cellPadding="0">
								<TR>
									<TD vAlign="middle" align="right" width="875"><INPUT id="Volum" style="WIDTH: 26px; HEIGHT: 12px" type="hidden" size="1" value="0" name="Hidden1"
											runat="server"><asp:button id="btnPre" runat="server" Height="16px" Width="16px" Text="◀" CommandName="Video"
											CommandArgument="Pre"></asp:button><asp:button id="btnNow" runat="server" Height="16px" Width="16px" Text="■" CommandName="Video"
											CommandArgument="Now"></asp:button><asp:button id="btnNext" runat="server" Height="16px" Width="16px" Text="▶" CommandName="Video"
											CommandArgument="Next"></asp:button>&nbsp;&nbsp;&nbsp;</TD>
								</TR>
								<TR>
									<TD vAlign="top" align="center" width="875" height="390"><igtbl1:ultrawebgrid id="uwgOO_HT" runat="server" Height="385px" Width="800px">
											<DisplayLayout AutoGenerateColumns="False" AllowSortingDefault="Yes" RowHeightDefault="20px" Version="3.00"
												ViewType="Hierarchical" SelectTypeRowDefault="Extended" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="uwgOOxHT" CellClickActionDefault="RowSelect" NoDataMessage="해당품목이 없습니다."
												AllowUpdateDefault="RowTemplateOnly">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</AddNewBox>
												<Pager QuickPages="5" PageSize="16" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray" Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="385px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents ColumnHeaderClickHandler="ColumnHeaderClickHandler" BeforeRowTemplateOpenHandler="uwgOO_HT_BeforeRowTemplateOpenHandler"
													AfterRowTemplateOpenHandler="uwgOO_HT_AfterRowTemplateOpenHandler" AfterRowTemplateCloseHandler="uwgOO_HT_AfterRowTemplateCloseHandler"></ClientSideEvents>
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
												<igtbl1:UltraGridBand>
													<Columns>
														<igtbl1:UltraGridColumn HeaderText="선택" Key="chk" Width="40px" Type="CheckBox" HeaderClickAction="Select"
															BaseColumnName="" AllowUpdate="Yes">
															<SelectedHeaderStyle HorizontalAlign="Center"></SelectedHeaderStyle>
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" BaseColumnName="ItemNum">
															<CellStyle HorizontalAlign="Left">
																<Padding Left="2px"></Padding>
															</CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" BaseColumnName="ItemDrawNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" BaseColumnName="ItemName">
															<CellStyle HorizontalAlign="Left">
																<Padding Left="2px"></Padding>
															</CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="단위" Key="Unit" Width="35px" BaseColumnName="Unit">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" BaseColumnName="ProgressCondition">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="시작공정코드" Key="BeginProcessCode" Hidden="True" BaseColumnName="BeginProcessCode">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="시작공정" Key="BeginProcess" BaseColumnName="BeginProcess">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="종료공정코드" Key="EndProcessCode" Hidden="True" BaseColumnName="EndProcessCode">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="종료공정" Key="EndProcess" BaseColumnName="EndProcess">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="거래처명" Key="CompanyName" BaseColumnName="CompanyName">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" BaseColumnName="BusinessRegistrationNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="발주비율" Key="OrderRate" Hidden="True" Format="###,###,###" BaseColumnName="OrderRate">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="총발주량" Key="OrderQuantity" Format="###,###,##0.00" BaseColumnName="OrderQuantity">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="1차납품요구량" Key="FirstDeliveryDemandQuantity" Hidden="True" Format="###,###,##0.00"
															BaseColumnName="FirstDeliveryDemandQuantity">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="납기요구일" Key="FirstDeliveryDemandDate" Format="yyyy-MM-dd" BaseColumnName="FirstDeliveryDemandDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="2차납품요구량" Key="SecondDeliveryDemandQuantity" Hidden="True" Format="###,###,##0.00"
															BaseColumnName="SecondDeliveryDemandQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="2차납품요구일" Key="SecondDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
															BaseColumnName="SecondDeliveryDemandDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="3차납품요구량" Key="ThirdDeliveryDemandQuantity" Hidden="True" Format="###,###,##0.00"
															BaseColumnName="ThirdDeliveryDemandQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="3차납품요구일" Key="ThirdDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
															BaseColumnName="ThirdDeliveryDemandDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="4차납품요구량" Key="FourthDeliveryDemandQuantity" Hidden="True" Format="###,###,##0.00"
															BaseColumnName="FourthDeliveryDemandQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="4차납품요구일" Key="FourthDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
															BaseColumnName="FourthDeliveryDemandDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="5차납품요구량" Key="FifthDeliveryDemandQuantity" Hidden="True" Format="###,###,##0.00"
															BaseColumnName="FifthDeliveryDemandQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="5차납품요구일" Key="FifthDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
															BaseColumnName="FifthDeliveryDemandDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="단가구분" Key="UnitCostDistinction" Hidden="True" BaseColumnName="UnitCostDistinction">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Format="###,###,##0.00" BaseColumnName="ApplyUnitCost">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="총금액" Key="TotalCost" Format="###,###,##0.00" BaseColumnName="TotalCost">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="잔량" Key="RemainQuantity" Format="###,###,##0.00" BaseColumnName="RemainQuantity">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="볼륨번호" Key="VolumNum" BaseColumnName="VolumNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" BaseColumnName="RegistrationPerson">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" BaseColumnName="UpdatingPerson">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="외주의뢰원장번호" Key="OutSideOrderRequestHistoryIndex" Hidden="True" BaseColumnName="OutSideOrderRequestHistoryIndex">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="외주발주원장번호" Key="OutSideOrderHistoryIndex" BaseColumnName="OutSideOrderHistoryIndex">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="품목분류1" Key="ItemClassification1" BaseColumnName="ItemClassification1">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="품목분류2" Key="ItemClassification2" BaseColumnName="ItemClassification2">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="품목분류3" Key="ItemClassification3" BaseColumnName="ItemClassification3">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
														<igtbl1:UltraGridColumn HeaderText="품목분류4" Key="ItemClassification4" BaseColumnName="ItemClassification4">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl1:UltraGridColumn>
													</Columns>
													<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
														<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
													</RowTemplateStyle>
													<RowEditTemplate>
														<P>
															<TABLE id="Table2" height="150" cellSpacing="1" cellPadding="1" width="100%" border="0">
																<TR>
																	<TD align="right" bgColor="lightgrey" height="20">품목명</TD>
																	<TD height="20"><INPUT id="txtItemName" style="WIDTH: 100px" readOnly type="text" columnKey="ItemName"></TD>
																	<TD align="right" bgColor="lightgrey" height="20">거래처명</TD>
																	<TD height="20"><INPUT id="txtCompanyName" style="WIDTH: 100px" readOnly type="text" columnKey="CompanyName"></TD>
																	<TD height="20"><FONT face="굴림"></FONT></TD>
																	<TD height="20"><FONT face="굴림"></FONT></TD>
																	<TD height="20"><FONT face="굴림"></FONT></TD>
																	<TD height="20"></TD>
																</TR>
																<TR>
																	<TD style="HEIGHT: 10px" align="right" bgColor="lightgrey">시작공정</TD>
																	<TD style="HEIGHT: 10px"><INPUT id="txtBeginProcess" style="WIDTH: 100px" readOnly type="text" columnKey="BeginProcess"></TD>
																	<TD style="HEIGHT: 10px" align="right" bgColor="lightgrey">종료공정</TD>
																	<TD style="HEIGHT: 10px"><INPUT id="txtEndProcess" style="WIDTH: 100px" readOnly type="text" columnKey="EndProcess"></TD>
																	<TD style="HEIGHT: 10px" align="right" bgColor="lightgrey"><FONT face="굴림">발주량</FONT></TD>
																	<TD style="HEIGHT: 10px"><FONT face="굴림"><INPUT id="TotalQuantity" style="WIDTH: 100px" readOnly type="text" columnKey="OrderQuantity"></FONT></TD>
																	<TD style="HEIGHT: 10px" align="right" bgColor="lightgrey">발주비율</TD>
																	<TD style="HEIGHT: 10px"><INPUT id="txtOrderRate" onkeydown="OnKeyDown_Float(this)" onblur="OnBlur_Float(this)"
																			onkeyup="return Process1()" style="WIDTH: 100px" onfocus="OnFocus_Obj(this)" type="text" name="Text1" columnKey="OrderRate"></TD>
																</TR>
																<TR>
																	<TD style="FONT-SIZE: 5px" bgColor="dimgray" height="5"><FONT face="굴림"></FONT></TD>
																	<TD bgColor="dimgray" height="5"><FONT face="굴림"></FONT></TD>
																	<TD bgColor="dimgray" height="5"><FONT face="굴림"></FONT></TD>
																	<TD bgColor="dimgray" height="5"><FONT face="굴림"></FONT></TD>
																	<TD bgColor="dimgray" height="5"><FONT face="굴림"></FONT></TD>
																	<TD bgColor="dimgray" height="5"><FONT face="굴림"></FONT></TD>
																	<TD bgColor="dimgray" height="5"><FONT face="굴림"></FONT></TD>
																	<TD bgColor="dimgray" height="5"><FONT face="굴림"></FONT></TD>
																</TR>
																<TR>
																	<TD style="HEIGHT: 23px"><FONT face="굴림"></FONT></TD>
																	<TD style="HEIGHT: 23px" vAlign="middle" align="center" bgColor="lightgrey"><FONT face="굴림">1차</FONT></TD>
																	<TD style="HEIGHT: 23px" vAlign="middle" align="center" bgColor="lightgrey"><FONT face="굴림">2차</FONT></TD>
																	<TD style="HEIGHT: 23px" vAlign="middle" align="center" bgColor="lightgrey"><FONT face="굴림">3차</FONT></TD>
																	<TD style="HEIGHT: 23px" vAlign="middle" align="center" bgColor="lightgrey"><FONT face="굴림">4차</FONT></TD>
																	<TD style="HEIGHT: 23px" vAlign="middle" align="center" bgColor="lightgrey"><FONT face="굴림">5차</FONT></TD>
																	<TD style="HEIGHT: 23px" vAlign="middle" align="center" bgColor="lightgrey"><FONT face="굴림">총발주량</FONT></TD>
																	<TD style="HEIGHT: 23px" vAlign="middle" align="center" bgColor="lightgrey"><FONT face="굴림">적용단가</FONT></TD>
																</TR>
																<TR>
																	<TD style="HEIGHT: 21px" align="right" bgColor="lightgrey"><FONT face="굴림">납품량</FONT></TD>
																	<TD style="HEIGHT: 21px"><FONT face="굴림"><INPUT id="FirstQuantity" onkeydown="OnKeyDown_Float(this)" onblur="OnBlur_Float(this)"
																				onkeyup="return Process()" style="WIDTH: 100px" onfocus="OnFocus_Obj(this)" readOnly type="text" size="11" columnKey="FirstDeliveryDemandQuantity"></FONT></TD>
																	<TD style="HEIGHT: 21px"><FONT face="굴림"><INPUT id="SecondQuantity" onkeydown="OnKeyDown_Float(this)" onblur="OnBlur_Float(this)"
																				onkeyup="return Process()" style="WIDTH: 100px" onfocus="OnFocus_Obj(this)" readOnly type="text" size="11" columnKey="SecondDeliveryDemandQuantity"></FONT></TD>
																	<TD style="HEIGHT: 21px"><FONT face="굴림"><INPUT id="ThirdQuantity" onkeydown="OnKeyDown_Float(this)" onblur="OnBlur_Float(this)"
																				onkeyup="return Process()" style="WIDTH: 100px" onfocus="OnFocus_Obj(this)" readOnly type="text" size="11" columnKey="ThirdDeliveryDemandQuantity"></FONT></TD>
																	<TD style="HEIGHT: 21px"><FONT face="굴림"><INPUT id="FourthQuantity" onkeydown="OnKeyDown_Float(this)" onblur="OnBlur_Float(this)"
																				onkeyup="return Process()" style="WIDTH: 100px" onfocus="OnFocus_Obj(this)" readOnly type="text" size="11" columnKey="FourthDeliveryDemandQuantity"></FONT></TD>
																	<TD style="HEIGHT: 21px"><FONT face="굴림"><INPUT id="FifthQuantity" onkeydown="OnKeyDown_Float(this)" onblur="OnBlur_Float(this)"
																				onkeyup="return Process()" style="WIDTH: 100px" onfocus="OnFocus_Obj(this)" readOnly type="text" size="11" columnKey="FifthDeliveryDemandQuantity"></FONT></TD>
																	<TD style="HEIGHT: 21px"><INPUT id="txtTotalQuantity" style="WIDTH: 100px" type="text" columnKey="OrderQuantity"></TD>
																	<TD style="HEIGHT: 21px"><INPUT id="txtApplyUnitCost" style="WIDTH: 100px" type="text" columnKey="ApplyUnitCost"></TD>
																</TR>
																<TR>
																	<TD style="HEIGHT: 6px" align="right" bgColor="lightgrey"><FONT face="굴림">납품일</FONT></TD>
																	<TD style="HEIGHT: 6px" align="center"><FONT face="굴림">
																			<igsch:WebDateChooser id="FirstDate" runat="server" Width="100px" BorderColor="DimGray" BorderStyle="Solid"
																				NullDateLabel=" ">
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
																			</igsch:WebDateChooser></FONT></TD>
																	<TD style="HEIGHT: 6px" align="center">
																		<igsch:WebDateChooser id="SecondDate" runat="server" Width="100px" BorderColor="DimGray" BorderStyle="Solid"
																			NullDateLabel=" " ReadOnly="True" Enabled="False">
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
																		</igsch:WebDateChooser></TD>
																	<TD style="HEIGHT: 6px" align="center">
																		<igsch:WebDateChooser id="ThirdDate" runat="server" Width="100px" BorderColor="DimGray" BorderStyle="Solid"
																			NullDateLabel=" " ReadOnly="True" Enabled="False">
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
																		</igsch:WebDateChooser></TD>
																	<TD style="HEIGHT: 6px" align="center">
																		<igsch:WebDateChooser id="FourthDate" runat="server" Width="100px" BorderColor="DimGray" BorderStyle="Solid"
																			NullDateLabel=" " ReadOnly="True" Enabled="False">
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
																		</igsch:WebDateChooser></TD>
																	<TD style="HEIGHT: 6px" align="center">
																		<igsch:WebDateChooser id="FifthDate" runat="server" Width="100px" BorderColor="DimGray" BorderStyle="Solid"
																			NullDateLabel=" " ReadOnly="True" Enabled="False">
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
																		</igsch:WebDateChooser></TD>
																	<TD style="HEIGHT: 6px"></TD>
																	<TD style="HEIGHT: 6px"></TD>
																</TR>
																<TR>
																	<TD height="10"><FONT face="굴림"></FONT></TD>
																	<TD height="10"></TD>
																	<TD height="10"></TD>
																	<TD height="10"></TD>
																	<TD height="10"></TD>
																	<TD height="10"></TD>
																	<TD align="right" height="10"><FONT face="굴림"><INPUT id="igtbl_reCancelBtn" style="WIDTH: 60px" onclick="igtbl_gRowEditButtonClick(event);"
																				type="button" value="취   소"></FONT></TD>
																	<TD align="left" height="10"><FONT face="굴림"></FONT><INPUT id="igtbl_reOkBtn" style="WIDTH: 60px" onclick="igtbl_gRowEditButtonClick(event); __doPostBack('linkUpdate','');"
																			type="button" value="수   정" name="igtbl_reOkBtn">&nbsp;</TD>
																</TR>
															</TABLE>
														</P>
														<P><FONT face="굴림"></FONT>&nbsp;
														</P>
														<P><FONT face="굴림"></FONT>&nbsp;</P>
														<P><BR>
														</P>
														<P align="center">&nbsp;
														</P>
													</RowEditTemplate>
												</igtbl1:UltraGridBand>
											</Bands>
										</igtbl1:ultrawebgrid></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="20"></TD>
					<TD>
						<table id="Table4" style="WIDTH: 100%; HEIGHT: 35px" height="8" cellSpacing="0" cellPadding="0">
							<TR>
								<TD style="WIDTH: 500px" align="left"><asp:button id="btnExcel" runat="server" Font-Size="10pt" Height="20px" Width="65px" Text="Excel"></asp:button>&nbsp;
									<asp:button id="btEnd" runat="server" Font-Size="9pt" Height="20px" Width="65px" Text="완   료"></asp:button>&nbsp;
									<asp:button id="btEndCancel" runat="server" Font-Size="9pt" Height="20px" Width="65px" Text="완료취소"></asp:button>&nbsp;
									<asp:button id="Button1" runat="server" Font-Size="9pt" Height="20px" Width="60px" Text="발주서"></asp:button></TD>
								<TD style="WIDTH: 244px" align="right"><asp:linkbutton id="linkUpdate" runat="server" Visible="False">LinkButton</asp:linkbutton><INPUT id="hdRowIndex" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1" runat="server"><asp:button id="btnDelete" runat="server" Font-Size="9pt" Height="20px" Width="60px" Text="삭   제"></asp:button>&nbsp;</TD>
								<TD style="WIDTH: 28px" align="right"><asp:button id="btnCancel" runat="server" Font-Size="9pt" Height="20px" Width="60px" Text="취   소"></asp:button></TD>
								<TD align="right">
									<P><asp:button id="btnStop" runat="server" Font-Size="9pt" Height="20px" Width="60px" Text="중   단"></asp:button></P>
								</TD>
							</TR>
						</table>
						<igtblexp:ultrawebgridexcelexporter id="uwgExcel" runat="server"></igtblexp:ultrawebgridexcelexporter></TD>
				</TR>
			</TABLE>
			</TD></TR></TABLE></form>
	</body>
</HTML>
