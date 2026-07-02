<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Page language="c#" Codebehind="PreGoodsManufactureOutStorehouse.aspx.cs" AutoEventWireup="false" Inherits="mlim_ERP.BusinessManagement.PreGoodsManufactureOutStorehouse" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PreGoodsManufactureOutStorehouse</title>
		<meta content="False" name="vs_snapToGrid">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--
		
		function DoPost()
		{
			__doPostBack("LinkButton1","");
		}
		
		function DoPost1()
		{
			__doPostBack('LinkButton2','');
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
		/*
			var value = Number(document.Form1.tbStoreQuantity.value) - Number(document.Form1.txtThisTimeOutStorehouseQuantity.value);
			var test = 0;
			
			if(value < 0 )	
			{
				alert("- 출고수량이 재고수량보다 많습니다");
				document.Form1.txtThisTimeOutStorehouseQuantity.value = 0;
				document.Form1.txtThisTimeOutStorehouseQuantity.focus();
				test =1;
			}
		*/	
			
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
	<body MS_POSITIONING="FlowLayout" bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="600" border="0">
				<TR>
					<TD vAlign="top" align="center">
						<TABLE id="Table1" style="BORDER-RIGHT: darkgray thin outset; BORDER-TOP: darkgray thin outset; BORDER-LEFT: darkgray thin outset; WIDTH: 680px; BORDER-BOTTOM: darkgray thin outset; HEIGHT: 289px"
							cellSpacing="0" cellPadding="0" border="0">
							<TBODY>
								<TR>
									<TD style="FONT-WEIGHT: bold; FONT-SIZE: 13pt; COLOR: white; BORDER-BOTTOM: darkgray double; HEIGHT: 30px; BACKGROUND-COLOR: darkgray"
										align="left" bgColor="dimgray" colSpan="6" height="30">&nbsp;수주외 출고</TD>
								</TR>
								<TR>
									<TD width="680" colspan="6" align="right" bgColor="dimgray"></TD>
								</TR>
								<TR>
									<TD width="680" align="left" bgColor="gainsboro" colSpan="6"><FONT face="굴림"></FONT><FONT face="굴림"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></FONT></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 210px; BORDER-TOP-STYLE: none; BORDER-TOP-COLOR: white; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; HEIGHT: 16px; BORDER-BOTTOM-STYLE: none"
										align="left" bgColor="gainsboro" colSpan="2">
										<uc1:CompanySearchControl id="CSC1" runat="server"></uc1:CompanySearchControl></TD>
									<TD style="WIDTH: 80px; HEIGHT: 16px" align="right" bgColor="gainsboro"><FONT face="굴림">출고일&nbsp;</FONT></TD>
									<TD style="WIDTH: 130px; HEIGHT: 16px" bgColor="gainsboro"><igsch:webdatechooser id="wdcOutStorehouseDate" runat="server" BorderStyle="Solid" Width="130px" BorderColor="DimGray"
											Text="Null" NullDateLabel=" " BackColor="#EEEEE9">
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
									<TD style="WIDTH: 60px; HEIGHT: 16px" align="right" bgColor="gainsboro"><FONT face="굴림">출고량</FONT>&nbsp;</TD>
									<td style="WIDTH: 200px; HEIGHT: 16px" align="left" bgColor="gainsboro">
										<asp:textbox id="txtThisTimeOutStorehouseQuantity" style="TEXT-ALIGN: right" runat="server" BorderColor="DimGray"
											Width="110px" BorderStyle="Solid" Height="20px" BackColor="#EEEEE9" BorderWidth="1px">0</asp:textbox></td>
								</TR>
								<TR>
									<TD style="WIDTH: 70px; BORDER-TOP-STYLE: none; BORDER-TOP-COLOR: white; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; HEIGHT: 16px; BORDER-BOTTOM-STYLE: none"
										align="right" bgColor="#dcdcdc"><FONT face="굴림">창고수량 </FONT>&nbsp;</TD>
									<TD style="WIDTH: 140px; HEIGHT: 16px" bgColor="#dcdcdc"><FONT face="굴림">
											<asp:textbox id="tbStoreQuantity" style="TEXT-ALIGN: right" runat="server" BackColor="#EEEEE9"
												BorderColor="DimGray" Width="110px" BorderStyle="Solid" BorderWidth="1px" Height="20px" ReadOnly="True">0</asp:textbox></FONT></TD>
									<TD style="WIDTH: 80px; HEIGHT: 16px" align="right" bgColor="#dcdcdc"><FONT face="굴림">적용단가</FONT>&nbsp;
									</TD>
									<TD style="WIDTH: 130px; HEIGHT: 16px" bgColor="#dcdcdc"><FONT face="굴림">
											<asp:textbox id="tbApplyUnitCost" style="TEXT-ALIGN: right" runat="server" BackColor="#EEEEE9"
												BorderColor="DimGray" Width="110px" BorderStyle="Solid" BorderWidth="1px" Height="20px">0</asp:textbox></FONT></TD>
									<TD style="WIDTH: 60px; HEIGHT: 16px" align="right" bgColor="#dcdcdc"><FONT face="굴림">부가세</FONT></TD>
									<TD align="left" bgColor="#dcdcdc" width="200"><FONT face="굴림">
											<asp:textbox id="tbTax" style="TEXT-ALIGN: right" runat="server" BackColor="#EEEEE9" BorderColor="DimGray"
												Width="39px" BorderStyle="Solid" BorderWidth="1px" Height="20px">10</asp:textbox>%</FONT></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 680px; HEIGHT: 3px" align="right" bgColor="#696969" colspan="6"></TD>
								</TR>
								<TR>
									<TD width="680" style="HEIGHT: 21px" align="right" bgColor="lightgrey" colSpan="6">
										<asp:LinkButton id="LinkButton2" runat="server"></asp:LinkButton><INPUT id="hdQuantity" style="WIDTH: 40px; HEIGHT: 22px" type="hidden" size="1" value="0"
											name="hdQuantity" runat="server"><INPUT id="hdBusinessRegistrationNum" style="WIDTH: 40px; HEIGHT: 22px" type="hidden" size="1"
											name="Hidden1" runat="server"><INPUT id="hdItemNum" style="WIDTH: 40px; HEIGHT: 22px" type="hidden" size="1" runat="server"
											NAME="hdItemNum">
										<asp:LinkButton id="LinkButton1" runat="server"></asp:LinkButton><asp:button id="btnClose" runat="server" Width="60px" Height="20px" Text="닫   기" Font-Size="9pt"></asp:button>&nbsp;&nbsp;
										<asp:button id="btnInit" runat="server" Text="초 기 화" Width="60px" Height="20px" Font-Size="9pt"></asp:button>&nbsp;
										<asp:button id="Button1" runat="server" Width="60px" Height="20px" Text="등   록" Font-Size="9pt"></asp:button>&nbsp;</TD>
					</TD>
				</TR>
				<TR>
					<TD style="WIDTH: 680px; HEIGHT: 3px" align="right" bgColor="dimgray" colSpan="6"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 680px; HEIGHT: 28px" align="right" bgColor="#dcdcdc" colSpan="6"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 680px; HEIGHT: 13px" bgColor="#dcdcdc" colspan="6">
						<P><FONT face="굴림"></FONT>&nbsp;</P>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
					</TD>
				</TR>
			</TABLE>
			<P><FONT face="굴림"></FONT>&nbsp;</P>
			</TD></TR></TBODY></TABLE></form>
	</body>
</HTML>
