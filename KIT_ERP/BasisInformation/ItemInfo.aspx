<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Page language="c#" Codebehind="ItemInfo.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.ItemInfo"  %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ItemInfo</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		function OK(text)
		{
			if(confirm(text + "하시겠습니까?"))
				return true;
			else
				return false;
		}
		
		function DoPost()
		{
			__doPostBack('LinkButton1','');
		}
		
		function UltraWebGrid1_AfterSelectChangeHandler(gridName, id){
			//Add code to handle your event here.
			ItemInfo.lb_ReferenceTable.value = document.getElementById(id).children[2].innerText;
			ItemInfo.lb_ReferenceIndex.value = document.getElementById(id).children[3].innerText;
			
		}
		function SubmitLinkButton(inputValue)
        {
			document.ItemInfo.ItemIndex.value = inputValue;
			__doPostBack('LinkButton1','');
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
	<body bgColor="#f7f6f6" MS_POSITIONING="GridLayout">
		<form id="ItemInfo" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; POSITION: absolute; TOP: 10px; LEFT: 10px" height="550"
				cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD>
						<table id="a" height="500" cellSpacing="0" cellPadding="0">
							<tr>
								<td>
									<table style="HEIGHT: 20px" cellSpacing="0" cellPadding="0" width="100%">
										<tr>
											<td align="left" width="600" colSpan="8"><FONT face="굴림">&nbsp;</FONT>&nbsp;<INPUT id="ItemIndex" style="WIDTH: 30px; HEIGHT: 16px" type="hidden" size="1" name="ItemIndex"
													runat="server">
												<asp:linkbutton id="LinkButton1" runat="server" Visible="False">LinkButton</asp:linkbutton><asp:label id="lb_ItemIndex" runat="server" Font-Size="10pt" Visible="False"></asp:label>
											</td>
											<td vAlign="bottom" align="right" width="200"><asp:linkbutton id="LinkButton2" runat="server" Font-Size="9pt">품목정보보기</asp:linkbutton></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td>
									<fieldset style="BORDER-BOTTOM: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; HEIGHT: 520px; BORDER-TOP: dimgray 2px solid; BORDER-RIGHT: dimgray 2px solid"><legend style="BORDER-BOTTOM-COLOR: black; BORDER-RIGHT-WIDTH: thin; BORDER-TOP-COLOR: black; WIDTH: 32px; BORDER-COLLAPSE: collapse; BORDER-TOP-WIDTH: thin; TABLE-LAYOUT: auto; BORDER-BOTTOM-WIDTH: thin; HEIGHT: 18px; BORDER-RIGHT-COLOR: black; BORDER-LEFT-COLOR: black; BORDER-LEFT-WIDTH: thin"
											align="bottom">[입력]</legend>
										<table id="b" cellSpacing="0" cellPadding="0" width="800" border="0">
											<TR>
												<TD vAlign="middle" align="left" width="600" colSpan="6"><FONT face="굴림">
														<uc1:ItemSearchControl id="ItemSearchControl1" runat="server"></uc1:ItemSearchControl><INPUT id="lb_ReferenceTable" style="WIDTH: 50px; HEIGHT: 14px" type="hidden" size="3"
															name="lb_ReferenceTable" runat="server"><INPUT id="lb_ReferenceIndex" style="WIDTH: 50px; HEIGHT: 10px" type="hidden" size="3"
															name="lb_ReferenceIndex" runat="server"><INPUT id="lb_Property" style="WIDTH: 48px; HEIGHT: 9px" type="hidden" size="2" name="Hidden1"
															runat="server"></FONT></TD>
												<TD align="right" width="70">&nbsp;<FONT style="COLOR: red" face="굴림">*자산분류</FONT></TD>
												<TD style="HEIGHT: 18px" align="left" width="130"><asp:dropdownlist id="dl_PropertyClassification" runat="server" BackColor="#EEEEE9" Font-Size="9pt"
														Height="20px" Width="100px">
														<asp:ListItem Value="원자재">원자재</asp:ListItem>
														<asp:ListItem Value="반제품">반제품</asp:ListItem>
														<asp:ListItem Value="제품">제품</asp:ListItem>
														<asp:ListItem Value="상품">상품</asp:ListItem>
														<asp:ListItem Value="부자재">부자재</asp:ListItem>
														<asp:ListItem Value="소모품">소모품</asp:ListItem>
													</asp:dropdownlist></TD>
											</TR>
											<tr>
												<td align="right" width="70"><FONT style="COLOR: red" face="굴림">*단위&nbsp; </FONT>
												</td>
												<td vAlign="middle" align="left" width="130"><asp:dropdownlist id="dl_Unit" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px" Width="100px"></asp:dropdownlist></td>
												<td align="right" width="70"><FONT face="굴림">규격&nbsp;</FONT></td>
												<td style="HEIGHT: 18px" align="left" width="130"><asp:textbox id="tb_Standard" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></asp:textbox></td>
												<td align="right" width="70"><FONT face="굴림">과세여부&nbsp;</FONT></td>
												<td style="HEIGHT: 18px" align="left" width="130"><asp:dropdownlist id="dl_Texture" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px">
														<asp:ListItem Value="1">예</asp:ListItem>
														<asp:ListItem Value="0">아니오</asp:ListItem>
													</asp:dropdownlist></td>
												<td align="right" width="70"><FONT face="굴림">부가세율&nbsp;</FONT></td>
												<td style="HEIGHT: 18px" align="left" width="130"><asp:textbox id="tb_SupplementaryValueTaxRate" style="TEXT-ALIGN: right" runat="server" BackColor="#EEEEE9"
														Height="20px" Width="100px" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid">10</asp:textbox>%
												</td>
											</tr>
											<tr>
												<td align="right" width="70"><FONT face="굴림">재고단위&nbsp; </FONT>
												</td>
												<td vAlign="middle" align="left" width="130"><asp:dropdownlist id="dl_StockUnit" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px"></asp:dropdownlist></td>
												<td align="right" width="70"><FONT face="굴림">BOM단위&nbsp;</FONT></td>
												<td align="left" width="130"><asp:dropdownlist id="dl_BOMUnit" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px"></asp:dropdownlist></td>
												<td align="right" width="70"><FONT face="굴림">구매단위&nbsp;</FONT></td>
												<td style="HEIGHT: 16px" align="left" width="130"><asp:dropdownlist id="dl_PurchaseUnit" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px"></asp:dropdownlist></td>
												<td align="right" width="70"><FONT face="굴림">판매단위&nbsp;</FONT></td>
												<td style="HEIGHT: 16px" align="left" width="130"><asp:dropdownlist id="dl_SaleUnit" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td align="right" width="70"><FONT face="굴림" style="COLOR: red">*자재산출&nbsp; </FONT>
												</td>
												<td vAlign="middle" align="left" width="130"><asp:dropdownlist id="dl_IOChackable" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px">
														<asp:ListItem Value="1">예</asp:ListItem>
														<asp:ListItem Value="0">아니오</asp:ListItem>
													</asp:dropdownlist></td>
												<td align="right" width="70"><FONT face="굴림" style="FONT-SIZE: 8pt">재고관리여부&nbsp;</FONT></td>
												<td style="HEIGHT: 20px" align="left" width="130"><asp:dropdownlist id="dl_StockManagable" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px">
														<asp:ListItem Value="1">예</asp:ListItem>
														<asp:ListItem Value="0" Selected="True">아니오</asp:ListItem>
													</asp:dropdownlist></td>
												<td align="right" width="70"><FONT style="COLOR: red" face="굴림">*검사구분&nbsp;</FONT></td>
												<td style="HEIGHT: 20px" align="left" width="130"><asp:dropdownlist id="dl_CheckDistinction" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px">
														<asp:ListItem Value="1">검사</asp:ListItem>
														<asp:ListItem Value="0">무검사</asp:ListItem>
													</asp:dropdownlist></td>
												<td align="right" width="70"><FONT face="굴림">발주방침&nbsp;</FONT></td>
												<td style="HEIGHT: 20px" align="left" width="130"><asp:dropdownlist id="dl_OrderPlan" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px">
														<asp:ListItem Value="1">계산발주</asp:ListItem>
														<asp:ListItem Value="0">임의발주</asp:ListItem>
													</asp:dropdownlist></td>
											</tr>
											<tr>
												<td align="right" width="70"><FONT face="굴림">품목타입&nbsp;</FONT></td>
												<td vAlign="middle" align="left" width="130"><asp:dropdownlist id="dl_ItemType" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px"></asp:dropdownlist></td>
												<td align="right" width="70"><FONT face="굴림">재질&nbsp;</FONT></td>
												<td align="left" width="130"><asp:dropdownlist id="dl_MateralQuality" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px"></asp:dropdownlist></td>
												<td align="right" width="70"><FONT face="굴림" color="#ff0066">*품목상태&nbsp;</FONT></td>
												<td style="HEIGHT: 15px" align="left" width="130"><asp:dropdownlist id="dl_ItemState" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px"></asp:dropdownlist></td>
												<td align="right" width="70"><FONT face="굴림">메이커&nbsp;</FONT></td>
												<td style="HEIGHT: 15px" align="left" width="130"><asp:textbox id="tb_Maker" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px" Width="100px"
														BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></asp:textbox></td>
											</tr>
											<tr>
												<td align="right" width="70"><FONT style="COLOR: red" face="굴림">*품목분류1&nbsp;</FONT></td>
												<td vAlign="middle" align="left" width="130"><asp:dropdownlist id="dl_ItemClassification1" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px"></asp:dropdownlist></td>
												<td align="right" width="70"><FONT face="굴림" color="#ff0066">*품목분류2&nbsp;</FONT></td>
												<td align="left" width="130"><asp:dropdownlist id="dl_ItemClassification2" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px"></asp:dropdownlist></td>
												<td align="right" width="70"><FONT face="굴림" color="#ff0066">*품목분류3&nbsp;</FONT></td>
												<td style="HEIGHT: 20px" align="left" width="130"><asp:dropdownlist id="dl_ItemClassification3" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px"></asp:dropdownlist></td>
												<td align="right" width="70"><FONT face="굴림">품목분류4&nbsp;</FONT></td>
												<td style="HEIGHT: 20px" align="left" width="130"><asp:dropdownlist id="dl_ItemClassification4" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td align="right" width="70"><FONT face="굴림">규격1&nbsp;</FONT></td>
												<td vAlign="middle" align="left" width="130"><asp:textbox id="tb_Standard1" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></asp:textbox></td>
												<td align="right" width="70"><FONT face="굴림">단위1&nbsp;</FONT></td>
												<td align="left" width="130"><asp:dropdownlist id="dl_Unit1" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px" Width="100px"></asp:dropdownlist></td>
												<td align="right" width="70"><FONT face="굴림">규격2&nbsp;</FONT></td>
												<td style="HEIGHT: 21px" align="left" width="130"><asp:textbox id="tb_Standard2" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></asp:textbox></td>
												<td align="right" width="70"><FONT face="굴림">단위2&nbsp;</FONT></td>
												<td style="HEIGHT: 21px" align="left" width="130"><asp:dropdownlist id="dl_Unit2" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px" Width="100px"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td align="right" width="70"><FONT face="굴림">제품중량&nbsp;</FONT></td>
												<td vAlign="middle" align="left" width="130"><asp:textbox id="tb_ProductWeight" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></asp:textbox></td>
												<td align="right" width="70"><FONT face="굴림">소재중량&nbsp;</FONT></td>
												<td align="left" width="130"><asp:textbox id="tb_MaterialWeight" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></asp:textbox></td>
												<td align="right" width="70"><FONT face="굴림">조달기간&nbsp;</FONT></td>
												<td align="left" width="130"><asp:textbox id="tb_SupplyTerm" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></asp:textbox></td>
												<td align="right" width="70"><FONT face="굴림" style="COLOR: blue; FONT-SIZE: 8pt">완성리드타임</FONT></td>
												<td align="left" width="130">
													<asp:textbox id="txtLeadTime" style="TEXT-ALIGN: right" runat="server" Width="100px" Height="20px"
														BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" BorderWidth="1px">0</asp:textbox></td>
											</tr>
											<tr>
												<td align="right" width="70"><FONT face="굴림">내외자구분&nbsp;</FONT></td>
												<td vAlign="middle" align="left" width="130"><asp:dropdownlist id="dl_DomesticImportDistinction" runat="server" BackColor="#EEEEE9" Font-Size="9pt"
														Height="20px" Width="100px">
														<asp:ListItem Value="1">내자</asp:ListItem>
														<asp:ListItem Value="0">외자</asp:ListItem>
													</asp:dropdownlist></td>
												<td align="right" width="70"><FONT face="굴림">안전재고량&nbsp;</FONT></td>
												<td align="left" width="130"><asp:textbox id="tb_SafetyStockQuantity" style="TEXT-ALIGN: right" runat="server" BackColor="#EEEEE9"
														Font-Size="9pt" Height="20px" Width="100px" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid">0</asp:textbox></td>
												<td align="right" width="70"><FONT face="굴림" style="FONT-SIZE: 8pt">발주간격수량&nbsp;</FONT></td>
												<td align="left" width="130"><asp:textbox id="tb_OrderIntervalQuantity" style="TEXT-ALIGN: right" runat="server" BackColor="#EEEEE9"
														Font-Size="9pt" Height="20px" Width="100px" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid">0</asp:textbox></td>
												<td align="right" width="70"><FONT face="굴림">최소발주량&nbsp;</FONT></td>
												<td align="left" width="130"><asp:textbox id="tb_MinOrderQuantity" style="TEXT-ALIGN: right" runat="server" BackColor="#EEEEE9"
														Height="20px" Width="100px" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid">0</asp:textbox></td>
											</tr>
											<tr>
												<td align="right" width="70"><FONT face="굴림">경화깊이&nbsp;</FONT></td>
												<td vAlign="middle" align="left" width="130"><asp:textbox id="tb_StiffenDeep" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></asp:textbox></td>
												<td align="right" width="70"><FONT style="FONT-SIZE: 7pt" face="굴림">제품열처리사양&nbsp;</FONT></td>
												<td align="left" width="130"><asp:textbox id="tb_ProductHeatTreatmentDescription" runat="server" BackColor="#EEEEE9" Font-Size="9pt"
														Height="20px" Width="100px" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></asp:textbox></td>
												<td align="right" width="70"><FONT style="FONT-SIZE: 7pt" face="굴림">소재열처리사양&nbsp;</FONT></td>
												<td align="left" width="130"><asp:textbox id="tb_MaterialHeatTreatmentDescription" runat="server" BackColor="#EEEEE9" Font-Size="9pt"
														Height="20px" Width="100px" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></asp:textbox></td>
												<td align="right" width="70"><FONT style="FONT-SIZE: 7pt" face="굴림">소재열처리정도&nbsp;</FONT></td>
												<td align="left" width="130"><asp:textbox id="tb_MaterialHeatTreatmentRequestDegree" runat="server" BackColor="#EEEEE9" Font-Size="9pt"
														Height="20px" Width="100px" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></asp:textbox></td>
											</tr>
											<tr>
												<td align="right" width="70"><FONT face="굴림">절단여유&nbsp;</FONT></td>
												<td vAlign="middle" align="left" width="130"><asp:textbox id="tb_CuttingSpace" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></asp:textbox></td>
												<td align="right" width="70"><FONT face="굴림">관세율&nbsp;</FONT></td>
												<td align="left" width="130"><asp:textbox id="tb_TariffRate" style="TEXT-ALIGN: right" runat="server" BackColor="#EEEEE9"
														Font-Size="9pt" Height="20px" Width="100px" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid">0</asp:textbox></td>
												<td align="right" width="70"><FONT style="COLOR: blue" face="굴림">*기준단가&nbsp;</FONT></td>
												<td align="left" width="130"><asp:textbox id="tb_StandardUnitCost" style="TEXT-ALIGN: right" runat="server" BackColor="#EEEEE9"
														Font-Size="9pt" Height="20px" Width="100px" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid">0</asp:textbox></td>
												<td align="right" width="70"><FONT face="굴림"></FONT></td>
												<td align="left" width="130"><FONT face="굴림"></FONT></td>
											</tr>
											<tr>
												<td align="right" width="70"><FONT face="굴림">주구입처&nbsp;</FONT></td>
												<td vAlign="middle" align="left" width="130"><asp:textbox id="tb_MainPurchaseCompany" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></asp:textbox></td>
												<td align="right" width="70"><FONT face="굴림">주외주처&nbsp;</FONT></td>
												<td align="left" width="130"><asp:textbox id="tb_MainOutSideOrderCompany" runat="server" BackColor="#EEEEE9" Font-Size="9pt"
														Height="20px" Width="100px" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></asp:textbox></td>
												<td align="right" width="70"><FONT face="굴림">주판매처&nbsp;</FONT></td>
												<td align="left" width="130"><asp:textbox id="tb_MainSaleCompany" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></asp:textbox></td>
												<td align="right" width="70"><FONT face="굴림">담당자&nbsp;</FONT></td>
												<td align="left" width="130"><asp:dropdownlist id="dl_ChargePerson" runat="server" BackColor="#EEEEE9" Font-Size="10pt" Height="20px"
														Width="100px">
														<asp:ListItem Value="아무개">아무개</asp:ListItem>
														<asp:ListItem Value="장길산">장길산</asp:ListItem>
													</asp:dropdownlist></td>
											</tr>
											<TR>
												<TD style="HEIGHT: 21px" align="right" width="70"></TD>
												<TD style="HEIGHT: 21px" vAlign="middle" align="left" width="130"><asp:textbox id="tb_Standard3" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid" Visible="False"></asp:textbox></TD>
												<TD style="HEIGHT: 21px" align="right" width="70"></TD>
												<TD style="HEIGHT: 21px" align="left" width="130"><asp:dropdownlist id="dl_Unit3" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px" Width="100px"
														Visible="False"></asp:dropdownlist></TD>
												<TD style="HEIGHT: 21px" align="right" width="70"></TD>
												<TD style="HEIGHT: 21px" align="left" width="130"><asp:textbox id="tb_Standard4" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
														Width="100px" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid" Visible="False"></asp:textbox></TD>
												<TD style="HEIGHT: 21px" align="right" width="70"><FONT face="굴림">관리여부&nbsp;</FONT></TD>
												<TD style="HEIGHT: 21px" align="left" width="130"><asp:dropdownlist id="dl_Unit4" runat="server" BackColor="#EEEEE9" Font-Size="9pt" Height="20px" Width="100px">
														<asp:ListItem Value="예" Selected="True">예</asp:ListItem>
														<asp:ListItem Value="아니오">아니오</asp:ListItem>
													</asp:dropdownlist></TD>
											</TR>
											<tr>
												<td align="right" width="70" style="HEIGHT: 9px"><asp:button id="bt_Reference" runat="server" Font-Size="9pt" Height="20px" Text="참고보기" Width="60px"></asp:button></td>
												<td vAlign="middle" align="right" width="130" style="HEIGHT: 9px" colSpan="3"><INPUT style="BACKGROUND-COLOR: #eeeee9; WIDTH: 300px; HEIGHT: 20px" type="file" size="31"
														id="File1" name="File1" runat="server"></td>
												<td align="right" width="70" style="HEIGHT: 9px"><FONT face="굴림">
														<asp:button id="tbTotalUpdate" runat="server" Width="60px" Height="20px" Text="일괄수정"></asp:button></FONT></td>
												<td align="right" width="330" colSpan="3" style="HEIGHT: 9px"><asp:button id="bt_Clear" runat="server" Font-Size="9pt" Height="20px" Text="초기화" Width="60px"></asp:button><FONT face="굴림">&nbsp;</FONT><asp:button id="bt_Update" runat="server" Font-Size="9pt" Height="20px" Text="수  정" Width="60px"
														Enabled="False"></asp:button><FONT face="굴림">&nbsp;</FONT><asp:button id="bt_Delete" runat="server" Font-Size="9pt" Height="20px" Text="삭  제" Width="60px"
														Enabled="False"></asp:button><FONT face="굴림">&nbsp;</FONT><asp:button id="bt_Registration" runat="server" Font-Size="9pt" Height="20px" Text="등  록" Width="60px"></asp:button>&nbsp;</td>
											</tr>
											<TR>
												<TD align="right" width="70" height="5"><FONT face="굴림"><asp:label id="lb_Index" runat="server" Font-Size="10pt" Height="18px" Width="52px" Visible="False"></asp:label></FONT></TD>
												<TD vAlign="middle" align="left" width="130" height="5"></TD>
												<TD align="right" width="70" height="5"></TD>
												<TD align="left" width="130" height="5"><FONT face="굴림"></FONT></TD>
												<TD align="right" width="70" height="5"></TD>
												<TD align="right" width="330" colSpan="3" height="5"></TD>
											</TR>
										</table>
										<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="195px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
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
												<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
												<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="195px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents AfterSelectChangeHandler="UltraWebGrid1_AfterSelectChangeHandler"></ClientSideEvents>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedRowStyleDefault ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault BorderColor="DarkGray" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="품목정보테이블" Key="StandardinfoTableID" Width="150px" BaseColumnName="StandardinfoTableID">
															<HeaderStyle Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목정보인덱스" Key="SITIndex" Width="50px" Hidden="True" BaseColumnName="SITIndex"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="자료실테이블" Key="CommunityDataTableID" Width="150px" BaseColumnName="CommunityDataTableID"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="자료실인덱스" Key="CDTIndex" Width="50px" Hidden="True" BaseColumnName="CDTIndex"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="제           목" Key="Title" Width="500px" HeaderClickAction="SortSingle"
															BaseColumnName="Title"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="번호" Key="CRelationShipIndex" Width="50px" Hidden="True" BaseColumnName="CRelationShipIndex"></igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></fieldset>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
