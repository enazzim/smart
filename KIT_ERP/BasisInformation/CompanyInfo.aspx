<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Page language="c#" Codebehind="CompanyInfo.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.CompanyInfo" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CompanyInfo</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<STYLE type="text/css">BODY { SCROLLBAR-ARROW-COLOR: #400040; SCROLLBAR-FACE-COLOR: #ffffff; SCROLLBAR-DARKSHADOW-COLOR: #808080; SCROLLBAR-HIGHLIGHT-COLOR: #ffffff; SCROLLBAR-SHADOW-COLOR: #ffffff; SCROLLBAR-TRACK-COLOR: #ffffff; SCROLLBAR-3DLIGHT-COLOR: #808080 }
		</STYLE>
		<script language="javascript">
		function DoPost1()
		{
			__doPostBack("Linkbutton2","");
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
			var row = igtbl_getRowById(id);
			CompanyInfo.lb_ReferenceTable.value = row.getCellFromKey("CommunityDataTableID").getValue();
			CompanyInfo.lb_ReferenceIndex.value = row.getCellFromKey("CDTIndex").getValue();
			//CompanyInfo.lb_ReferenceTable.value = document.getElementById(id).children[2].innerText;
			//CompanyInfo.lb_ReferenceIndex.value = document.getElementById(id).children[3].innerText;
		}
		
		function PreventSubmitOnEnter()
		{
			if(event.keyCode==13)
			{
				return false;
			}
		}
		function Load()
		{
			var inputs = document.getElementsByTagName("INPUT")         
			for(var i = 0; i < inputs.length ; i++)
			{               
				if(inputs[i].type == "text")
				{           
					inputs[i].attachEvent("onkeypress", PreventSubmitOnEnter);      
				}
			}
		}
		
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
				} else if( !IsNumericKey() ){
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

		function OnFocus_Obj(obj) {
			obj.select();
		}
	
		function ValidateFloat(a_sValue)	{	
			var s_FieldValue = new String(a_sValue);
			
			if (isNaN(s_FieldValue))
				return false;
				
			return true;
		}		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="Load()" bgColor="#f7f6f6">
		<form id="CompanyInfo" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; POSITION: absolute; TOP: 10px; LEFT: 10px" cellSpacing="0"
				cellPadding="0" width="800" border="0">
				<TR>
					<TD>
						<table style="WIDTH: 800px" width="800" border="0" cellSpacing="0" cellPadding="0" height="540">
							<tr>
								<td vAlign="middle" align="left" height="20">
									<table id="a" width="800" border="0" cellSpacing="0" cellPadding="0" height="20">
										<tr>
											<td vAlign="middle" align="left" width="210" colSpan="2">
												<uc1:CompanySearchControl id="CSC1" runat="server"></uc1:CompanySearchControl></td>
											<td align="right" vAlign="bottom" width="595">
												<asp:linkbutton id="Linkbutton2" runat="server" Visible="False">LinkButton</asp:linkbutton><asp:LinkButton id="LinkButton1" runat="server" Font-Size="9pt">거래처 정보 보기</asp:LinkButton><FONT face="굴림">
												</FONT>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td vAlign="top" height="520">
									<FIELDSET style="Z-INDEX: 102; BORDER-BOTTOM: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-RIGHT: dimgray 2px solid; LEFT: 8px"
										align="left"><LEGEND>[ 입 력 ]</LEGEND>
										<TABLE id="b" width="800" border="0" cellSpacing="0" cellPadding="0">
											<TR>
												<TD align="right" width="94" colSpan="2" style="WIDTH: 94px"><FONT face="굴림" color="red">*거래처구분&nbsp;</FONT></TD>
												<TD style="WIDTH: 127px" align="center" width="127" colSpan="3">
													<asp:checkbox id="cb_ReceiveingOrderCompany" runat="server" Font-Size="10pt" Width="90px" Text="수주거래처"></asp:checkbox></TD>
												<TD style="WIDTH: 188px" align="center" width="188" colSpan="3">
													<asp:checkbox id="cb_OutSideOrderCompany" runat="server" Font-Size="10pt" Width="90px" Text="외주거래처"></asp:checkbox></TD>
												<TD align="center" width="120" colSpan="3">
													<asp:checkbox id="cb_BuyingCompany" runat="server" Font-Size="10pt" Width="90px" Text="구매거래처"></asp:checkbox></TD>
												<TD style="WIDTH: 114px" align="center" width="114" colSpan="3">
													<asp:checkbox id="cb_CostCompany" runat="server" Font-Size="10pt" Width="90px" Text="비용거래처"></asp:checkbox></TD>
												<TD width="40"></TD>
												<TD width="40"></TD>
												<TD style="WIDTH: 15px" width="15"></TD>
												<TD width="40"></TD>
												<TD width="40"></TD>
												<TD width="40"></TD>
											</TR>
											<TR style="FONT-SIZE: 1px" height="5">
												<TD width="40"></TD>
												<TD width="53" style="WIDTH: 53px"></TD>
												<TD width="40"></TD>
												<TD style="WIDTH: 48px" width="48"></TD>
												<TD style="WIDTH: 26px" width="26"></TD>
												<TD style="WIDTH: 81px" width="81"></TD>
												<TD style="WIDTH: 34px" width="34"></TD>
												<TD style="WIDTH: 48px" width="48"></TD>
												<TD width="40"></TD>
												<TD style="WIDTH: 34px" width="34"></TD>
												<TD width="40"></TD>
												<TD width="40"></TD>
												<TD width="40"></TD>
												<TD style="WIDTH: 30px" width="30"></TD>
												<TD width="40"></TD>
												<TD width="40"></TD>
												<TD style="WIDTH: 15px" width="15"></TD>
												<TD width="40"></TD>
												<TD width="40"></TD>
												<TD width="40"></TD>
											</TR>
											<TR>
												<TD align="right" width="94" colSpan="2" style="WIDTH: 94px"><FONT style="COLOR: red" face="굴림">*거래처명&nbsp;</FONT></TD>
												<TD style="WIDTH: 127px" width="127" colSpan="3">
													<asp:textbox id="tb_CompanyName" runat="server" Height="20px" Width="110px" BorderStyle="Solid"
														BackColor="#EEEEE9" Font-Size="9pt" BorderWidth="1px" BorderColor="DimGray"></asp:textbox>
												<TD style="WIDTH: 81px" align="right"><FONT style="COLOR: red" face="굴림">*대표자명&nbsp;</FONT></TD>
												<TD width="80" colSpan="3">
													<asp:textbox id="tb_PresidentName" runat="server" Height="20px" Width="90px" BorderStyle="Solid"
														BackColor="#EEEEE9" Font-Size="9pt" BorderWidth="1px" BorderColor="DimGray"></asp:textbox></TD>
												<TD align="right" width="80" colSpan="2"><FONT style="COLOR: red" face="굴림">*사업자번호&nbsp;</FONT></TD>
												<TD style="WIDTH: 114px" width="114" colSpan="3">
													<asp:textbox id="tb_BusinessCompanyNum" runat="server" Height="20px" Width="115px" BorderStyle="Solid"
														BackColor="#EEEEE9" Font-Size="9pt" BorderWidth="1px" BorderColor="DimGray"></asp:textbox></TD>
												<TD align="right" width="100" colSpan="3"><FONT face="굴림">법인번호&nbsp;</FONT></TD>
												<TD width="100" colSpan="3">
													<asp:textbox id="tb_CorporationRegistrationNum" runat="server" Height="20px" Width="100px" BorderStyle="Solid"
														BackColor="#EEEEE9" Font-Size="9pt" BorderWidth="1px" BorderColor="DimGray"></asp:textbox></TD>
											</TR>
											<TR>
												<TD align="right" width="94" colSpan="2" style="WIDTH: 94px"><FONT style="COLOR: red" face="굴림">*사업장주소&nbsp;</FONT></TD>
												<TD width="647" colSpan="16">
													<asp:textbox id="tb_BusinessCompanyAddress" runat="server" Height="20px" Width="450px" BorderStyle="Solid"
														BackColor="#EEEEE9" Font-Size="9pt" BorderWidth="1px" BorderColor="DimGray"></asp:textbox>
													<asp:button id="bt_Post1" runat="server" Font-Size="10pt" Height="20px" Width="60px" Text="주소찾기"></asp:button></TD>
												<TD width="40" style="COLOR: red"></TD>
												<TD width="40"></TD>
											</TR>
											<TR>
												<TD align="right" width="94" colSpan="2" style="WIDTH: 94px"><FONT style="COLOR: red" face="굴림">*계산서주소&nbsp;</FONT></TD>
												<TD width="647" colSpan="16">
													<asp:textbox id="tb_TaxBillAddress" runat="server" Height="20px" Width="450px" BorderStyle="Solid"
														BackColor="#EEEEE9" Font-Size="9pt" BorderWidth="1px" BorderColor="DimGray"></asp:textbox>
													<asp:button id="bt_Post2" runat="server" Font-Size="10pt" Height="20px" Width="60px" Text="주소찾기"></asp:button></TD>
												<TD width="40"></TD>
												<TD width="40"></TD>
											</TR>
											<TR>
												<TD align="right" width="94" colSpan="2" style="WIDTH: 94px"><FONT face="굴림">홈페이지&nbsp;</FONT></TD>
												<TD style="WIDTH: 327px" width="327" colSpan="8">
													<asp:textbox id="tb_HomepageAddress" runat="server" Height="20px" Width="300px" BorderStyle="Solid"
														BackColor="#EEEEE9" Font-Size="9pt" BorderWidth="1px" BorderColor="DimGray"></asp:textbox></TD>
												<TD width="40"></TD>
												<TD width="40"></TD>
												<TD width="40"></TD>
												<TD style="WIDTH: 30px" width="30"></TD>
												<TD width="40"></TD>
												<TD width="40"></TD>
												<TD style="WIDTH: 15px" width="15"></TD>
												<TD width="40"><FONT face="굴림"></FONT></TD>
												<TD width="40"></TD>
												<TD width="40"></TD>
											</TR>
											<TR>
												<TD align="right" width="94" colSpan="2" style="WIDTH: 94px"><FONT face="굴림">업태&nbsp;</FONT></TD>
												<TD align="left" width="120" colSpan="2">
													<asp:textbox id="tb_BusinessClassification" runat="server" Height="20px" Width="90px" BorderStyle="Solid"
														BackColor="#EEEEE9" Font-Size="9pt" BorderWidth="1px" BorderColor="DimGray"></asp:textbox></TD>
												<TD align="right" width="100" colSpan="2"><FONT face="굴림">종목&nbsp;</FONT></TD>
												<TD align="left" width="100" colSpan="2">
													<asp:textbox id="tb_BusinessItem" runat="server" Height="20px" Width="90px" BorderStyle="Solid"
														BackColor="#EEEEE9" Font-Size="9pt" BorderWidth="1px" BorderColor="DimGray"></asp:textbox></TD>
												<TD align="right" width="80" colSpan="2"><FONT face="굴림">거래상태&nbsp;</FONT></TD>
												<TD align="left" width="120" colSpan="2">
													<asp:dropdownlist id="dl_CurrentTradeState" runat="server" Font-Size="10pt" Height="20px" Width="70px"
														BackColor="#EEEEE9">
														<asp:ListItem Value="1">거래중</asp:ListItem>
														<asp:ListItem Value="0">거래중지</asp:ListItem>
													</asp:dropdownlist></TD>
												<TD align="right" width="80" colSpan="2"><FONT face="굴림">부가세처리&nbsp;</FONT></TD>
												<TD align="left" width="120" colSpan="2">
													<asp:dropdownlist id="dl_SupplementaryValueTax" runat="server" Font-Size="10pt" Height="20px" Width="70px"
														BackColor="#EEEEE9">
														<asp:ListItem Value="1">예</asp:ListItem>
														<asp:ListItem Value="0">아니오</asp:ListItem>
													</asp:dropdownlist></TD>
												<TD align="right" width="70" colSpan="2"><FONT face="굴림">세 율&nbsp;</FONT></TD>
												<TD align="left" width="130" colSpan="2">
													<asp:textbox id="tb_SupplementaryValueTaxRate" style="TEXT-ALIGN: right" runat="server" Height="20px"
														Width="50px" BorderStyle="Inset" BackColor="#EEEEE9" Font-Size="9pt">10</asp:textbox>%</TD>
											</TR>
											<TR>
												<TD align="right" width="94" colSpan="2" style="WIDTH: 94px"><FONT face="굴림">전화&nbsp;</FONT></TD>
												<TD style="WIDTH: 90px" width="90" colSpan="2">
													<asp:textbox id="tb_TelephoneNum" runat="server" Height="20px" Width="90px" BorderStyle="Solid"
														BackColor="#EEEEE9" Font-Size="9pt" BorderWidth="1px" BorderColor="DimGray"></asp:textbox></TD>
												<TD style="WIDTH: 96px" align="right" width="96" colSpan="2"><FONT face="굴림">팩스&nbsp;</FONT></TD>
												<TD style="WIDTH: 103px" width="103" colSpan="2">
													<asp:textbox id="tb_FaxNum" runat="server" Height="20px" Width="90px" BorderStyle="Solid" BackColor="#EEEEE9"
														Font-Size="9pt" BorderWidth="1px" BorderColor="DimGray"></asp:textbox></TD>
												<TD style="WIDTH: 77px" align="right" width="77" colSpan="2"><FONT face="굴림">분 류1&nbsp;</FONT></TD>
												<TD width="80" colSpan="2">
													<asp:dropdownlist id="dl_TradeClassification1" runat="server" Font-Size="10pt" Height="20px" Width="70px"
														BackColor="#EEEEE9"></asp:dropdownlist></TD>
												<TD style="WIDTH: 72px" align="right" width="72" colSpan="2"><P><FONT face="굴림">분 류2&nbsp;</FONT></P>
												</TD>
												<TD width="80" colSpan="2">
													<asp:dropdownlist id="dl_TradeClassification2" runat="server" Font-Size="10pt" Height="20px" Width="70px"
														BackColor="#EEEEE9"></asp:dropdownlist></TD>
												<TD align="right" width="80" colSpan="2"><FONT face="굴림">분 류3&nbsp;</FONT></TD>
												<TD width="80" colSpan="2">
													<asp:dropdownlist id="dl_TradeClassification3" runat="server" Font-Size="10pt" Height="20px" Width="70px"
														BackColor="#EEEEE9"></asp:dropdownlist></TD>
											</TR>
											<TR>
												<TD align="right" width="94" colSpan="2" style="WIDTH: 94px; HEIGHT: 24px"><FONT face="굴림">판매기준일&nbsp;</FONT></TD>
												<TD style="WIDTH: 90px; HEIGHT: 24px" width="90" colSpan="2">
													<asp:textbox id="tb_SaleStandardDate" runat="server" Height="20px" Width="50px" BorderStyle="Solid"
														BackColor="#EEEEE9" Font-Size="9pt" BorderWidth="1px" BorderColor="DimGray"></asp:textbox>일</TD>
												<TD style="WIDTH: 96px; HEIGHT: 24px" align="right" width="96" colSpan="2"><FONT face="굴림" style="FONT-SIZE: 8pt">판매기준어음일&nbsp;</FONT></TD>
												<TD style="WIDTH: 103px; HEIGHT: 24px" width="103" colSpan="2">
													<asp:textbox id="tb_SaleStandardBillDate" runat="server" Height="20px" Width="50px" BorderStyle="Solid"
														BackColor="#EEEEE9" Font-Size="9pt" BorderWidth="1px" BorderColor="DimGray"></asp:textbox>일</TD>
												<TD style="HEIGHT: 24px" align="right" width="95" colSpan="2"><FONT face="굴림" style="FONT-SIZE: 8pt">어음결재기준&nbsp;</FONT></TD>
												<TD width="110" colSpan="2" style="HEIGHT: 24px">
													<asp:dropdownlist id="dl_BillApprovalStandard" runat="server" Font-Size="10pt" Height="20px" Width="70px"
														BackColor="#EEEEE9">
														<asp:ListItem Value="0">-선택-</asp:ListItem>
														<asp:ListItem Value="1">1</asp:ListItem>
														<asp:ListItem Value="2">2</asp:ListItem>
														<asp:ListItem Value="3">3</asp:ListItem>
														<asp:ListItem Value="4">4</asp:ListItem>
														<asp:ListItem Value="5">5</asp:ListItem>
														<asp:ListItem Value="6">6</asp:ListItem>
														<asp:ListItem Value="7">7</asp:ListItem>
														<asp:ListItem Value="8">8</asp:ListItem>
														<asp:ListItem Value="9">9</asp:ListItem>
														<asp:ListItem Value="10">10</asp:ListItem>
														<asp:ListItem Value="11">11</asp:ListItem>
														<asp:ListItem Value="12">12</asp:ListItem>
													</asp:dropdownlist></TD>
												<TD width="40" style="HEIGHT: 24px"></TD>
												<TD style="WIDTH: 30px; HEIGHT: 24px" width="30"></TD>
												<TD width="40" style="HEIGHT: 24px"></TD>
												<TD width="40" style="HEIGHT: 24px"></TD>
												<TD style="WIDTH: 15px; HEIGHT: 24px" width="15"></TD>
												<TD width="40" style="HEIGHT: 24px"></TD>
												<TD width="40" style="HEIGHT: 24px"></TD>
												<TD width="40" style="HEIGHT: 24px"></TD>
											</TR>
											<TR>
												<TD align="right" width="94" colSpan="2" style="WIDTH: 94px"><FONT face="굴림">정기수금일1&nbsp;</FONT></TD>
												<TD style="WIDTH: 90px" width="90" colSpan="2">
													<asp:textbox id="tb_FixPeriodCollectMoneyDate1" runat="server" Height="20px" Width="50px" BorderStyle="Solid"
														BackColor="#EEEEE9" Font-Size="9pt" BorderWidth="1px" BorderColor="DimGray"></asp:textbox>일</TD>
												<TD style="WIDTH: 96px" align="right" width="96" colSpan="2"><FONT face="굴림">정기수금일2&nbsp;&nbsp;</FONT></TD>
												<TD style="WIDTH: 103px" width="103" colSpan="2">
													<asp:textbox id="tb_FixPeriodCollectMoneyDate2" runat="server" Height="20px" Width="50px" BackColor="#EEEEE9"
														Font-Size="9pt" BorderStyle="Solid" BorderWidth="1px" BorderColor="DimGray"></asp:textbox>일</TD>
												<TD style="WIDTH: 77px" width="77" colSpan="2"></TD>
												<TD width="80" colSpan="2"></TD>
												<TD width="40"></TD>
												<TD style="WIDTH: 30px" width="30"></TD>
												<TD width="40"></TD>
												<TD width="40"></TD>
												<TD style="WIDTH: 15px" width="15"></TD>
												<TD width="40"></TD>
												<TD width="40"></TD>
												<TD width="40"></TD>
											</TR>
											<TR>
												<TD align="right" width="94" colSpan="2" style="WIDTH: 94px"><FONT face="굴림">거래처담당자&nbsp;</FONT></TD>
												<TD style="WIDTH: 90px" width="90" colSpan="2">
													<asp:textbox id="tb_CompanyPersonInCharge" runat="server" Height="20px" Width="80px" BorderStyle="Solid"
														BackColor="#EEEEE9" Font-Size="9pt" BorderWidth="1px" BorderColor="DimGray"></asp:textbox></TD>
												<TD style="WIDTH: 96px" align="right" width="96" colSpan="2">E-Mail&nbsp;&nbsp;&nbsp;</TD>
												<TD width="200" colSpan="5">
													<asp:textbox id="tb_Email" runat="server" Font-Size="9pt" Height="20px" Width="190px" BorderStyle="Solid"
														BackColor="#EEEEE9" BorderWidth="1px" BorderColor="DimGray"></asp:textbox></TD>
												<TD width="40"></TD>
												<TD width="40"></TD>
												<TD style="WIDTH: 30px" width="30"></TD>
												<TD width="40"></TD>
												<TD width="40"></TD>
												<TD style="WIDTH: 15px" width="15"></TD>
												<TD width="40"></TD>
												<TD width="40"></TD>
												<TD width="40"></TD>
											</TR>
											<TR>
												<TD align="right" width="94" colSpan="2" style="WIDTH: 94px">
													<asp:button id="bt_Reference" runat="server" Height="20px" Width="80px" Text="참고보기"></asp:button></TD>
												<TD width="40"><INPUT id="lb_ReferenceIndex" style="WIDTH: 30px" type="hidden" name="lb_ReferenceIndex"
														runat="server">
												</TD>
												<TD style="WIDTH: 48px" width="48"><INPUT id="lb_ReferenceTable" style="WIDTH: 30px" type="hidden" name="lb_ReferenceTable"
														runat="server">
												</TD>
												<TD style="WIDTH: 26px" width="26"></TD>
												<TD style="WIDTH: 81px" width="81">
													<asp:label id="Label1" runat="server" Enabled="False" Visible="False">Label</asp:label>
													<asp:label id="Label2" runat="server" Enabled="False" Visible="False">Label</asp:label></TD>
												<TD align="right" colSpan="14"></FONT>
													<asp:label id="lb_CompanyIndex" runat="server" Width="10px" Visible="False"></asp:label>
													<asp:label id="lb_Index" runat="server" Width="10px" Visible="False"></asp:label>
													<asp:button id="bt_Clear" runat="server" Height="20px" Width="60px" Text="초기화"></asp:button>
													<asp:button id="bt_Update" runat="server" Height="20px" Width="60px" Text="수  정" Enabled="False"></asp:button>
													<asp:button id="bt_Delete" runat="server" Height="20px" Width="60px" Text="삭  제" Enabled="False"></asp:button>
													<asp:button id="bt_Registration" runat="server" Height="20px" Width="60px" Text="등  록"></asp:button>&nbsp;</TD>
											</TR>
											<TR>
												<TD style="WIDTH: 94px" align="right" width="94" colSpan="2" height="10"><FONT face="굴림"></FONT></TD>
												<TD width="40" height="10"></TD>
												<TD style="WIDTH: 48px" width="48" height="10"></TD>
												<TD style="WIDTH: 26px" width="26" height="10"></TD>
												<TD style="WIDTH: 81px" width="81" height="10"></TD>
												<TD align="right" colSpan="14" height="10"></TD>
											</TR>
											<TR>
												<TD colSpan="20" height="10">
													<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="255px" Width="800px">
														<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
															RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Extended" AllowColumnMovingDefault="OnServer"
															HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
															RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect"
															NoDataMessage="">
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
															<HeaderStyleDefault Cursor="Hand" VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Center"
																BackColor="LightGray">
																<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
															</HeaderStyleDefault>
															<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
															<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderColor="DimGray"
																BorderStyle="Solid" BackColor="Silver" Height="255px"></FrameStyle>
															<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
																<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
															</FooterStyleDefault>
															<ClientSideEvents AfterSelectChangeHandler="UltraWebGrid1_AfterSelectChangeHandler"></ClientSideEvents>
															<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
															<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
															<RowAlternateStyleDefault Cursor="Hand" BorderColor="DarkGray" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
															<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
																<Padding Left="3px"></Padding>
																<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
															</RowStyleDefault>
														</DisplayLayout>
														<Bands>
															<igtbl:UltraGridBand>
																<Columns>
																	<igtbl:UltraGridColumn HeaderText="거래처정보테이블" Key="StandardinfoTableID" Width="150px" BaseColumnName="StandardinfoTableID">
																		<HeaderStyle Height="25px"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="품목정보인덱스" Key="SITIndex" Width="50px" Hidden="True" BaseColumnName="SITIndex"></igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="자료실테이블" Key="CommunityDataTableID" Width="150px" BaseColumnName="CommunityDataTableID"></igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="자료실인덱스" Key="CDTIndex" Width="50px" Hidden="True" BaseColumnName="CDTIndex"></igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="제목" Key="Title" Width="500px" HeaderClickAction="SortSingle" BaseColumnName="Title"></igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="번호" Key="CRelationShipIndex" Width="50px" Hidden="True" BaseColumnName="CRelationShipIndex"></igtbl:UltraGridColumn>
																</Columns>
															</igtbl:UltraGridBand>
														</Bands>
													</igtbl:ultrawebgrid></TD>
											</TR>
										</TABLE>
									</FIELDSET>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
