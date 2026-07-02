<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="PaymentPlanResultRegistrationPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BuyingOutside.PaymentPlanResultPC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>WebForm1</title>
<meta content="Microsoft Visual Studio .NET 7.1" name=GENERATOR>
<meta content=C# name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema>
<script language=javascript src="../MessageWindows.js"></script>

<script language=javascript src="../GridCheck.js"></script>
<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
<SCRIPT type=text/javascript><!--
	
		function uwgPPR_HT_AfterRowTemplateCloseHandler(gridName, rowId, bSaveChanges){
		
			var frm = document.Form1;
			
			if(event.srcElement.id == "igtbl_reOkBtn")
			{
				var row = igtbl_getRowById(rowId);
				var combo1 = igdrp_getComboById("uwgPPRxHTxxctl0xwdcPaymentDate");
				var combo2 = igdrp_getComboById("uwgPPRxHTxxctl0xwdcBillPaymentDate1");
				var combo3 = igdrp_getComboById("uwgPPRxHTxxctl0xwdcBillPaymentDate2");
				var combo4 = igdrp_getComboById("uwgPPRxHTxxctl0xwdcBillPaymentDate3");
				
				
				
				
				row.getCellFromKey("PaymentDate").setValue(combo1.getValue());
				row.getCellFromKey("BillPaymentDate1").setValue(combo2.getValue());
				row.getCellFromKey("BillPaymentDate2").setValue(combo3.getValue());
				row.getCellFromKey("BillPaymentDate3").setValue(combo4.getValue());		
				
				//결재방법
				if(frm.uwgPPR_HT__ctl0_DDL_DecisionMethod.options[frm.uwgPPR_HT__ctl0_DDL_DecisionMethod.selectedIndex].value == "")
				{
					row.getCellFromKey("DecisionMethodCode").setValue(frm.uwgPPR_HT__ctl0_DDL_DecisionMethod.options[0].value);
					row.getCellFromKey("DecisionMethod").setValue(frm.uwgPPR_HT__ctl0_DDL_DecisionMethod.options[0].value);
				}
				else
				{
					row.getCellFromKey("DecisionMethodCode").setValue(frm.uwgPPR_HT__ctl0_DDL_DecisionMethod.options[frm.uwgPPR_HT__ctl0_DDL_DecisionMethod.selectedIndex].value);
					row.getCellFromKey("DecisionMethod").setValue(frm.uwgPPR_HT__ctl0_DDL_DecisionMethod.options[frm.uwgPPR_HT__ctl0_DDL_DecisionMethod.selectedIndex].innerText);
				}
				
				
				
				//결재은행1
				if(frm.uwgPPR_HT__ctl0_DDL_Bank1.options[frm.uwgPPR_HT__ctl0_DDL_Bank1.selectedIndex].value == "")
				{
					row.getCellFromKey("BankCode1").setValue(frm.uwgPPR_HT__ctl0_DDL_Bank1.options[0].value);
					row.getCellFromKey("BankName1").setValue(frm.uwgPPR_HT__ctl0_DDL_Bank1.options[0].value);
				}
				else
				{
					row.getCellFromKey("BankCode1").setValue(frm.uwgPPR_HT__ctl0_DDL_Bank1.options[frm.uwgPPR_HT__ctl0_DDL_Bank1.selectedIndex].value);
					row.getCellFromKey("BankName1").setValue(frm.uwgPPR_HT__ctl0_DDL_Bank1.options[frm.uwgPPR_HT__ctl0_DDL_Bank1.selectedIndex].innerText);
				}
				//결재은행2
				if(frm.uwgPPR_HT__ctl0_DDL_Bank2.options[frm.uwgPPR_HT__ctl0_DDL_Bank2.selectedIndex].value == "")
				{
					row.getCellFromKey("BankCode2").setValue(frm.uwgPPR_HT__ctl0_DDL_Bank2.options[0].value);
					row.getCellFromKey("BankName2").setValue(frm.uwgPPR_HT__ctl0_DDL_Bank2.options[0].value);
				}
				else
				{
					row.getCellFromKey("BankCode2").setValue(frm.uwgPPR_HT__ctl0_DDL_Bank2.options[frm.uwgPPR_HT__ctl0_DDL_Bank2.selectedIndex].value);
					row.getCellFromKey("BankName2").setValue(frm.uwgPPR_HT__ctl0_DDL_Bank2.options[frm.uwgPPR_HT__ctl0_DDL_Bank2.selectedIndex].innerText);
				}
				//결재은행3
				if(frm.uwgPPR_HT__ctl0_DDL_Bank3.options[frm.uwgPPR_HT__ctl0_DDL_Bank3.selectedIndex].value == "")
				{
					row.getCellFromKey("BankCode3").setValue(frm.uwgPPR_HT__ctl0_DDL_Bank3.options[0].value);
					row.getCellFromKey("BankName3").setValue(frm.uwgPPR_HT__ctl0_DDL_Bank3.options[0].value);
				}
				else
				{
					row.getCellFromKey("BankCode3").setValue(frm.uwgPPR_HT__ctl0_DDL_Bank3.options[frm.uwgPPR_HT__ctl0_DDL_Bank3.selectedIndex].value);
					row.getCellFromKey("BankName3").setValue(frm.uwgPPR_HT__ctl0_DDL_Bank3.options[frm.uwgPPR_HT__ctl0_DDL_Bank3.selectedIndex].innerText);
				}
			}		
		}
	
		
		function uwgPPR_HT_AfterRowTemplateOpenHandler(gridName, rowId){
					
			var frm = document.Form1;
			var row = igtbl_getRowById(rowId);
			
			///////////RowIndex Hidden 에 저장////////////////
			var num = rowId.split('_');
			document.Form1.hdRowIndex.value = num[1];
			/////////////////////////////////////////////////
			
									
			var combo1 = igdrp_getComboById("uwgPPRxHTxxctl0xwdcPaymentDate");
			var combo2 = igdrp_getComboById("uwgPPRxHTxxctl0xwdcBillPaymentDate1");
			var combo3 = igdrp_getComboById("uwgPPRxHTxxctl0xwdcBillPaymentDate2");
			var combo4 = igdrp_getComboById("uwgPPRxHTxxctl0xwdcBillPaymentDate3");
			
			
			combo1.setValue(row.getCellFromKey("PaymentDate").getValue());
			combo2.setValue(row.getCellFromKey("BillPaymentDate1").getValue());
			combo3.setValue(row.getCellFromKey("BillPaymentDate2").getValue());
			combo4.setValue(row.getCellFromKey("BillPaymentDate3").getValue());	
			document.Form1.hdYear.value = combo1.getValue().getFullYear();
			document.Form1.hdMon.value = combo1.getValue().getMonth()+1;
		
			frm.hd_ItemPaymentCost.value = row.getCellFromKey("ItemPaymentCost").getValue();// Hidden 필드에 수정전 물품액 저장
			
			
			frm.txtSupplementaryValueTaxPaymentCost.value = MakeCommaSeparate(row.getCellFromKey("SupplementaryValueTaxPaymentCost").getValue());
			frm.txtItemPaymentCost.value = MakeCommaSeparate(row.getCellFromKey("ItemPaymentCost").getValue());	
						
			//결재방법				
			for(var i=0;i<frm.uwgPPR_HT__ctl0_DDL_DecisionMethod.options.length;i++)
			{
			
				if(frm.uwgPPR_HT__ctl0_DDL_DecisionMethod.options[i].innerText == document.getElementById(rowId).children[7].innerText)			
				{				
				frm.uwgPPR_HT__ctl0_DDL_DecisionMethod.options[i].selected=true;	
				break;
				}			
				else
				frm.uwgPPR_HT__ctl0_DDL_DecisionMethod.options[0].selected=true;				
			}
			
			//결재은행1		
			for(var j=0;j<frm.uwgPPR_HT__ctl0_DDL_Bank1.options.length;j++)	
			{				
				if(frm.uwgPPR_HT__ctl0_DDL_Bank1.options[j].innerText == document.getElementById(rowId).children[12].innerText)
				{				
					frm.uwgPPR_HT__ctl0_DDL_Bank1.options[j].selected=true;		
					break;		
				}			
				else					
					frm.uwgPPR_HT__ctl0_DDL_Bank1.options[0].selected=true;
			}			
		
			
			//결재은행2	
			for(var k=0;k<frm.uwgPPR_HT__ctl0_DDL_Bank2.options.length;k++)	
			{
				if(frm.uwgPPR_HT__ctl0_DDL_Bank2.options[k].innerText == document.getElementById(rowId).children[16].innerText)		
				{
					frm.uwgPPR_HT__ctl0_DDL_Bank2.options[k].selected=true;	
					break;					
				}		
				else					
					frm.uwgPPR_HT__ctl0_DDL_Bank2.options[0].selected=true;
			}	
					
			//결재은행3	
			for(var l=0;l<frm.uwgPPR_HT__ctl0_DDL_Bank3.options.length;l++)	
			{		
			
				if(frm.uwgPPR_HT__ctl0_DDL_Bank3.options[l].innerText == document.getElementById(rowId).children[20].innerText)
				{			
					frm.uwgPPR_HT__ctl0_DDL_Bank3.options[l].selected=true;	
					break;
				}
				else
				frm.uwgPPR_HT__ctl0_DDL_Bank3.options[0].selected=true;
			}
						
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
			var Tax = Number(RemoveComma(document.Form1.txtItemPaymentCost.value))/10;
			var Tax1 = (Math.round(Tax,1));
			document.Form1.txtSupplementaryValueTaxPaymentCost.value = MakeCommaSeparate((Math.round((Tax1 * 10000)) / 10000));
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
		
--></SCRIPT>
</HEAD>
<BODY bottomMargin=0 bgColor=#f7f6f6 leftMargin=0 topMargin=0 rightMargin=0>
<form id=Form1 method=post runat="server">
<TABLE id=Table1 
style="Z-INDEX: 101; LEFT: 10px; WIDTH: 800px; POSITION: absolute; TOP: 10px; HEIGHT: 550px" 
cellSpacing=0 cellPadding=0 border=0>
  <TR>
    <TD align=left colSpan=2>
      <FIELDSET 
      style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 60px" 
      align=top><LEGEND style="FONT-SIZE: 10pt" align=left 
      >[ 검색 ] </LEGEND>
      <TABLE style="WIDTH: 800px; HEIGHT: 30px; cellSpacing: " cellSpacing=0 
      cellPadding=0>
        <TR>
          <TD align=right width=70 height=30><FONT 
            face=굴림>거래처명&nbsp;</FONT></TD>
          <TD width=120 height=30><font face=굴림 
            ></FONT><igcmbo:webcombo id=wcbCompany runat="server" DropImageXP2="/ig_common/WebGrid3/ig_cmboDown2.bmp" DropImageXP1="/ig_common/WebGrid3/ig_cmboDown1.bmp" Version="3.00" Font-Size="9pt" Width="120px" BorderStyle="Solid" BackColor="#EEEEE9" SelBackColor="10, 36, 106" BorderWidth="1px" ForeColor="Black" SelectedIndex="-1" Height="20px" SelForeColor="White" BorderColor="DimGray">
											<DropDownLayout DropdownWidth="550px" BorderCollapse="Separate" RowSelectors="No" RowHeightDefault="20px"
												HeaderClickAction="Select" DropdownHeight="350px" TableLayout="Fixed" StationaryMargins="Header">
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
          <TD align=right width=70 height=30 center><FONT 
            face=굴림>결제방법&nbsp;</FONT></TD>
          <TD style="WIDTH: 100px; vAlign: " height=30 
          centeralign="middle"><asp:dropdownlist id=SettleDropDown runat="server" Font-Size="9pt" Width="100px" BackColor="#EEEEE9" Height="20px"></asp:dropdownlist></TD>
          <TD align=right width=70 height=30 center><FONT 
            face=굴림>지급일&nbsp;</FONT></TD>
          <TD width=100 height=30 
            centeralign="middle"><FONT face=굴림></FONT><igsch:webdatechooser id=wdcStartDate runat="server" Font-Size="9pt" Width="100px" BorderStyle="Solid" BackColor="#EEEEE9" Height="20px" BorderColor="DimGray" NullDateLabel=" " Text=" ">
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
          <TD align=center width=10 height=30 
            center><FONT face=굴림>~</FONT></TD>
          <TD width=85 height=30 centeralign="middle"><igsch:webdatechooser id=wdcEndDate runat="server" Font-Size="9pt" Width="100px" BorderStyle="Solid" BackColor="#EEEEE9" Height="20px" BorderColor="DimGray" NullDateLabel=" " Text=" " DropDownAlignment="Center">
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
          <TD vAlign=middle align=right width=145 colSpan=2 height=30 
          ><asp:button id=btnInit runat="server" Font-Size="9pt" Width="65px" Height="20px" Text="초기화"></asp:button><FONT 
            face=굴림>&nbsp;</FONT> <asp:button id=btnSearch runat="server" Font-Size="9pt" Width="65px" Height="20px" Text="검   색"></asp:button><FONT 
            face=굴림></FONT></TD></TR></TABLE></FIELDSET> 
    </TD></TR>
  <TR>
    <TD style="HEIGHT: 444px" vAlign=top align=left colSpan=2 
    >
      <FIELDSET 
      style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 440px" 
      align=top><LEGEND style="FONT-SIZE: 10pt" align=left 
      >[ 검색결과 ] </LEGEND>
      <TABLE style="WIDTH: 100%; HEIGHT: 432px" cellSpacing=0 cellPadding=0 
      >
        <TR>
          <TD vAlign=top align=center><FONT face=굴림 
            ><igtbl:ultrawebgrid id=uwgPPR_HT runat="server" Width="800px" Height="435px">
<DisplayLayout AutoGenerateColumns="False" AllowSortingDefault="Yes" RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="uwgPPRxHT" CellClickActionDefault="RowSelect" AllowUpdateDefault="RowTemplateOnly">

<AddNewBox>

<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

</Style>

</AddNewBox>

<Pager PageSize="20" StyleMode="ComboBox" AllowPaging="True">

<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

</Style>

</Pager>

<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray" Height="25px">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

</HeaderStyleDefault>

<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DimGray" BorderStyle="Solid" BackColor="Silver" Height="435px">
</FrameStyle>

<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

</FooterStyleDefault>

<ClientSideEvents ColumnHeaderClickHandler="ColumnHeaderClickHandler" AfterRowTemplateOpenHandler="uwgPPR_HT_AfterRowTemplateOpenHandler" AfterRowTemplateCloseHandler="uwgPPR_HT_AfterRowTemplateCloseHandler">
</ClientSideEvents>

<EditCellStyleDefault BorderWidth="0px" BorderStyle="None">
</EditCellStyleDefault>

<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy">
</SelectedRowStyleDefault>

<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue">
</RowAlternateStyleDefault>

<RowStyleDefault Cursor="Hand" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">

<Padding Left="3px">
</Padding>

<BorderDetails WidthLeft="0px" WidthTop="0px">
</BorderDetails>

</RowStyleDefault>

</DisplayLayout>

<Bands>
<igtbl:UltraGridBand>
<Columns>
<igtbl:UltraGridColumn HeaderText="선택" Key="chk" Width="40px" Type="CheckBox" HeaderClickAction="Select" BaseColumnName="" AllowUpdate="Yes">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="거래처" Key="CompanyName" BaseColumnName="CompanyName">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" BaseColumnName="BusinessRegistrationNum">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="지급일" Key="PaymentDate" Format="yyyy-MM-dd" BaseColumnName="PaymentDate">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="수금액(물품대)" Key="ItemPaymentCost" Format="###,###,###.00" BaseColumnName="ItemPaymentCost">
<CellStyle HorizontalAlign="Right">

<Padding Right="4px">
</Padding>

</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="지급액(부가세)" Key="SupplementaryValueTaxPaymentCost" Format="###,###,###.00" BaseColumnName="SupplementaryValueTaxPaymentCost">
<CellStyle HorizontalAlign="Right">

<Padding Right="4px">
</Padding>

</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="결제방법코드" Key="DecisionMethodCode" Hidden="True" BaseColumnName="DecisionMethodCode">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="결제방법" Key="DecisionMethod" BaseColumnName="DecisionMethod">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="증거자료" Key="ProofData" Hidden="True" BaseColumnName="ProofData">
<CellStyle HorizontalAlign="Left">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="어음번호1" Key="BillNum1" BaseColumnName="BillNum1">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="결제일자1" Key="BillPaymentDate1" Format="yyyy-MM-dd" BaseColumnName="BillPaymentDate1">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="은행코드1" Key="BankCode1" Hidden="True" BaseColumnName="BankCode1">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="은행명1" Key="BankName1" BaseColumnName="BankName1">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="어음번호2" Key="BillNum2" BaseColumnName="BillNum2">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="결제일자2" Key="BillPaymentDate2" Format="yyyy-MM-dd" BaseColumnName="BillPaymentDate2">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="은행코드2" Key="BankCode2" Hidden="True" BaseColumnName="BankCode2">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="은행명2" Key="BankName2" BaseColumnName="BankName2">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="어음번호3" Key="BillNum3" BaseColumnName="BillNum3">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="결제일자3" Key="BillPaymentDate3" Format="yyyy-MM-dd" BaseColumnName="BillPaymentDate3">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="은행코드3" Key="BankCode3" Hidden="True" BaseColumnName="BankCode3">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="은행명3" Key="BankName3" BaseColumnName="BankName3">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" BaseColumnName="RegistrationPerson">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" BaseColumnName="RegistrationPersonID">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" BaseColumnName="UpdatingPerson">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" BaseColumnName="UpdatingPersonID">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="지급원장번호" Key="PaymentHistoryIndex" BaseColumnName="PaymentHistoryIndex">
<CellStyle HorizontalAlign="Right">
</CellStyle>
</igtbl:UltraGridColumn>
</Columns>

<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">

<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px">
</BorderDetails>

</RowTemplateStyle>

<RowEditTemplate>
															<P align="right">
																<TABLE id="Table2" style="HEIGHT: 104px" cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD style="WIDTH: 65px" align="right">거래처</TD>
																		<TD><INPUT id="txtCompanyName" style="WIDTH: 100px; HEIGHT: 20px" type="text" columnKey="CompanyName"></TD>
																		<TD style="WIDTH: 62px" align="right">지급일&nbsp;</TD>
																		<TD>
																			<igsch:WebDateChooser id="wdcPaymentDate" runat="server" Width="100px" Height="20px" NullDateLabel=" " Text="Null">
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
																			</igsch:WebDateChooser></TD>
																		<TD style="WIDTH: 92px" align="right">수금액(물품대)</TD>
																		<TD><INPUT id="txtItemPaymentCost"  onkeydown=OnKeyDown_Float(this); onkeyup="Process();OnKeyUp_Currency(this);" onblur=OnBlur_Cur(this); onfocus=OnFocus_Obj(this); style="WIDTH: 100px; HEIGHT: 22px; TEXT-ALIGN: right"  type=text size=11 columnKey="ItemPaymentCost"></TD>
																		<TD style="WIDTH: 96px" align="right">지급액(부가세)</TD>
																		<TD><INPUT id="txtSupplementaryValueTaxPaymentCost"  onkeydown=OnKeyDown_Float(this); onkeyup=OnKeyUp_Currency(this); onblur=OnBlur_Cur(this); onfocus=OnFocus_Obj(this); style="WIDTH: 100px; HEIGHT: 22px; TEXT-ALIGN: right"  type=text size=11 columnKey="SupplementaryValueTaxPaymentCost"></TD>
																	</TR>
																	<TR>
																		<TD style="WIDTH: 65px" align="right">결제방법</TD>
																		<TD>
																			<asp:DropDownList id=DDL_DecisionMethod runat="server" Width="100px" DataValueField="SmallClassificationCode" DataSource="<%# dsDecision %>" DataTextField="SmallClassificationName">
																			</asp:DropDownList>
																		<TD style="WIDTH: 62px" align="right">증거자료</TD>
																		<TD colSpan="5"><INPUT id="txtProofData" style="WIDTH: 464px; HEIGHT: 20px" type="text" size="72" columnKey="ProofData"></TD>
																	</TR>
																	<TR>
																		<TD style="WIDTH: 65px; HEIGHT: 4px" align="right">어음번호1</TD>
																		<TD style="HEIGHT: 4px"><INPUT id="Bill1" style="WIDTH: 100px; HEIGHT: 20px" type="text" columnKey="BillNum1"></TD>
																		<TD style="WIDTH: 62px; HEIGHT: 4px" align="right">결제일자1</TD>
																		<TD style="HEIGHT: 4px">
																			<igsch:WebDateChooser id="wdcBillPaymentDate1" runat="server" Width="100px" Height="20px" NullDateLabel=" " Text="Null">
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
																			</igsch:WebDateChooser></TD>
																		<TD style="WIDTH: 92px; HEIGHT: 4px" align="right">은행명1&nbsp;</TD>
																		<TD style="HEIGHT: 4px">
																			<asp:DropDownList id=DDL_Bank1 runat="server" Width="100px" Height="20px" DataValueField="SmallClassificationCode" DataSource="<%# dsBank %>" DataTextField="SmallClassificationName">
																			</asp:DropDownList></TD>
																		<TD style="WIDTH: 96px; HEIGHT: 4px"></TD>
																		<TD style="HEIGHT: 4px"></TD>
																	</TR>
																	<TR>
																		<TD style="WIDTH: 65px" align="right">어음번호2</TD>
																		<TD><INPUT id="Bill2" style="WIDTH: 100px; HEIGHT: 20px" type="text" columnKey="BillNum2"></TD>
																		<TD style="WIDTH: 62px" align="right">결제일자2</TD>
																		<TD>
																			<igsch:WebDateChooser id="wdcBillPaymentDate2" runat="server" Width="100px" Height="20px" NullDateLabel=" " Text="Null">
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
																			</igsch:WebDateChooser></TD>
																		<TD style="WIDTH: 92px" align="right">은행명2&nbsp;</TD>
																		<TD>
																			<asp:DropDownList id=DDL_Bank2 runat="server" Width="100px" Height="20px" DataValueField="SmallClassificationCode" DataSource="<%# dsBank %>" DataTextField="SmallClassificationName">
																			</asp:DropDownList></TD>
																		<TD style="WIDTH: 96px"></TD>
																		<TD></TD>
																	</TR>
																	<TR>
																		<TD style="WIDTH: 65px" align="right">어음번호3</TD>
																		<TD><INPUT id="Bill3" style="WIDTH: 100px; HEIGHT: 20px" type="text" columnKey="BillNum3"></TD>
																		<TD style="WIDTH: 62px" align="right">결제일자3</TD>
																		<TD>
																			<igsch:WebDateChooser id="wdcBillPaymentDate3" runat="server" Width="100px" Height="20px" NullDateLabel=" " Text="Null">
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
																			</igsch:WebDateChooser></TD>
																		<TD style="WIDTH: 92px" align="right">은행명3&nbsp;</TD>
																		<TD>
																			<asp:DropDownList id=DDL_Bank3 runat="server" Width="100px" Height="20px" DataValueField="SmallClassificationCode" DataSource="<%# dsBank %>" DataTextField="SmallClassificationName">
																			</asp:DropDownList></TD>
																		<TD style="WIDTH: 96px" colSpan="1"></TD>
																		<TD><FONT face="굴림">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</FONT>
																			<INPUT id="igtbl_reOkBtn" style="WIDTH: 53px; HEIGHT: 20px" onclick="igtbl_gRowEditButtonClick(event);__doPostBack('linkUpdate','');"
																				type="button" value="수   정">&nbsp;&nbsp;
																		</TD>
																	</TR>
																</TABLE>
															</P>
															<BR>
															<P align="center">&nbsp;
															</P>
															<P align="center">&nbsp;</P>
															<P align="center">&nbsp;</P>
															<P align="center">&nbsp;</P>
															<P align="center">&nbsp;</P>
														
</RowEditTemplate>
</igtbl:UltraGridBand>
</Bands>
</igtbl:ultrawebgrid></FONT></TD></TR></TABLE></FIELDSET> </TD></TR>
  <TR>
    <TD vAlign=top align=left colSpan=2>
      <table style="WIDTH: 100%; HEIGHT: 8px" height=8>
        <TR>
          <TD style="WIDTH: 716px" align=left height=5 
            ><FONT face=굴림></FONT></TD>
          <TD align=right height=5></TD></TR>
        <TR>
          <TD style="WIDTH: 716px" align=left><asp:button id=btnExcel runat="server" Font-Size="9pt" Width="65px" Height="20px" Text="Excel"></asp:button><igtblexp:ultrawebgridexcelexporter 
            id=uwgExcel 
            runat="server"></igtblexp:ultrawebgridexcelexporter><INPUT 
            id=hd_ItemPaymentCost 
            style="WIDTH: 75px; HEIGHT: 22px; BACKGROUND-COLOR: #eeeee9" 
            type=hidden size=7 value=0 name=Hidden1 
            runat="server"> <asp:linkbutton id=linkUpdate runat="server" Visible="False">LinkButton</asp:linkbutton><INPUT 
            id=hdRowIndex 
            style="WIDTH: 75px; HEIGHT: 22px; BACKGROUND-COLOR: #eeeee9" 
            type=hidden size=4 value=0 name=Hidden1 
            runat="server"><INPUT id=hdYear 
            style="WIDTH: 24px; HEIGHT: 22px; BACKGROUND-COLOR: #eeeee9" 
            type=hidden size=1 name=Hidden1 
            runat="server"><INPUT id=hdMon 
            style="WIDTH: 19px; HEIGHT: 22px; BACKGROUND-COLOR: #eeeee9" 
            type=hidden size=1 name=Hidden1 
          runat="server"></TD>
          <TD align=right><asp:button id=btnDelete runat="server" Font-Size="9pt" Width="65px" Height="20px" Text="삭   제"></asp:button><FONT 
            face=굴림 
  ></FONT></TD></TR></TABLE></TD></TR></TABLE></FORM>
	</BODY>
</HTML>
