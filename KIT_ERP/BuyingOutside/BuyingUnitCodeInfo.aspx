<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Page language="c#" Codebehind="BuyingUnitCodeInfo.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.BuyingUnitCodeInfo" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BuyingUnitCostInfo</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<STYLE type="text/css">BODY { SCROLLBAR-FACE-COLOR: #ffffff; SCROLLBAR-HIGHLIGHT-COLOR: #ffffff; SCROLLBAR-SHADOW-COLOR: #ffffff; SCROLLBAR-3DLIGHT-COLOR: #808080; SCROLLBAR-ARROW-COLOR: #400040; SCROLLBAR-TRACK-COLOR: #ffffff; SCROLLBAR-DARKSHADOW-COLOR: #808080 }
		</STYLE>
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
			__doPostBack('LinkButton2','');
		}
		
		
		function UltraWebGrid1_DblClickHandler(gridName, cellId){
			//Add code to handle your event here.
			var row = igtbl_getRowById(cellId);
			
			var combo = igcmbo_getComboById("wcCompany");//거래처명
			var combo2 = igdrp_getComboById("wdcBeginDate");//적용일
			var combo3 = igdrp_getComboById("wdcEndDate");//종료일
			
			combo.setDataValue(row.getCellFromKey("BusinessRegistrationNum").getValue());//사업자등록번호
			combo.setDisplayValue(row.getCellFromKey("CompanyName").getValue());//거래처명
			
			combo2.setValue(row.getCellFromKey("BeginDate").getValue());//적용일
			combo3.setValue(row.getCellFromKey("EndDate").getValue());//종료일
			
			document.BuyingUnitCodeInfo.tb_OrderRate.value = row.getCellFromKey("OrderRate").getValue()	//발주비율
			document.BuyingUnitCodeInfo.tb_StandardUnitCost.value = row.getCellFromKey("StandardUnitCost").getValue();//기준단가
			document.BuyingUnitCodeInfo.tb_DiscountUnitCost.value = row.getCellFromKey("DiscountUnitCost").getValue();	//할인단가
			document.BuyingUnitCodeInfo.lb_Index.value = row.getCellFromKey("UnitCostInfoIndex").getValue();
			
			document.BuyingUnitCodeInfo.bt_Update.disabled = false;
		    document.BuyingUnitCodeInfo.bt_Delete.disabled = false;
		    document.BuyingUnitCodeInfo.bt_Registration.disabled = true;
		}
		
		
		function Ultrawebgrid2_AfterSelectChangeHandler(gridName, id){
			//Add code to handle your event here.
			var row = igtbl_getRowById(id);
			BuyingUnitCodeInfo.lb_ReferenceTable.value = row.getCellFromKey("CommunityDataTableID").getValue();
			BuyingUnitCodeInfo.lb_ReferenceIndex.value = row.getCellFromKey("CDTIndex").getValue();
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
	<body MS_POSITIONING="GridLayout" onload="Load()" bgColor="#f7f6f6">
		<form id="BuyingUnitCodeInfo" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" cellSpacing="0"
				cellPadding="0" width="800" border="0">
				<TBODY>
					<TR>
						<TD vAlign="top">
							<table style="WIDTH: 800px; HEIGHT: 30px" cellSpacing="0" cellPadding="0">
								<TR>
									<TD align="right" width="600" colSpan="7"><FONT face="굴림"></FONT></TD>
									<TD vAlign="middle" align="right" width="200">
										<asp:linkbutton id="LinkButton1" runat="server" Font-Size="10pt">단가정보보기</asp:linkbutton></TD>
								</TR>
							</table>
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"
								align="left"><LEGEND>[ 입 력 ]</LEGEND>
								<TABLE style="WIDTH: 800px; HEIGHT: 200px" cellSpacing="0" cellPadding="0">
									<TR>
										<TD align="left" width="600" colSpan="6" height="20"><FONT face="굴림">
												<uc1:ItemSearchControl id="ItemSearchControl1" runat="server"></uc1:ItemSearchControl></FONT></TD>
										<TD align="right" width="70" height="20"><FONT style="COLOR: black" face="굴림">자산분류&nbsp;</FONT></TD>
										<TD align="left" width="130" height="20">
											<asp:textbox id="tb_PropertyClassification" runat="server" Width="100px" Height="20px" Font-Size="9pt"
												BackColor="#EEEEE9" BorderColor="DimGray" BorderWidth="1px" BorderStyle="Solid" ReadOnly="True"></asp:textbox></TD>
									</TR>
									<TR>
										<TD align="right" width="70"><FONT style="COLOR: black" face="굴림">단위&nbsp;</FONT></TD>
										<TD align="left" width="130">
											<asp:textbox id="tb_Unit" runat="server" BorderStyle="Solid" BorderWidth="1px" BorderColor="DimGray"
												BackColor="#EEEEE9" Font-Size="9pt" Height="20px" Width="100px" ReadOnly="True"></asp:textbox></TD>
										<TD align="right" width="70"><FONT style="COLOR: black" face="굴림">규격&nbsp;</FONT></TD>
										<TD align="left" width="130">
											<asp:textbox id="tb_Standard" runat="server" BorderStyle="Solid" BorderWidth="1px" BorderColor="DimGray"
												BackColor="#EEEEE9" Font-Size="9pt" Height="20px" Width="100px" ReadOnly="True"></asp:textbox></TD>
										<TD align="right" width="70">
											<asp:label id="lb_ItemIndex" runat="server" Font-Size="10pt" Visible="False"></asp:label></TD>
										<TD align="left" width="130"><INPUT id="ItemIndex" style="WIDTH: 30px" type="hidden" name="ItemIndex" runat="server">
											<asp:LinkButton id="LinkButton2" runat="server" Visible="False">LinkButton</asp:LinkButton></TD>
										<TD align="right" width="70">&nbsp;</TD>
										<TD align="left" width="130">
											<asp:button id="bt_BuyingUnitCost" runat="server" Height="20px" Width="100px" Text="구매단가보기"></asp:button><FONT face="굴림">&nbsp;</FONT></TD>
									</TR>
									<TR>
										<TD style="HEIGHT: 157px" vAlign="top" align="center" width="800" colSpan="8" height="157"><ADDNEWBOX>
												<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="250px" Width="800px">
													<DisplayLayout AutoGenerateColumns="False" AllowSortingDefault="OnClient" RowHeightDefault="20px"
														Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti"
														BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1"
														CellClickActionDefault="RowSelect">
														<AddNewBox>
															<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
															</Style>
														</AddNewBox>
														<Pager>
															<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
															</Style>
														</Pager>
														<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray">
															<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
														</HeaderStyleDefault>
														<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
														<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderColor="DimGray"
															BorderStyle="Solid" BackColor="Silver" Height="250px"></FrameStyle>
														<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
															<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
														</FooterStyleDefault>
														<ClientSideEvents DblClickHandler="UltraWebGrid1_DblClickHandler"></ClientSideEvents>
														<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
														<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
														<RowAlternateStyleDefault Cursor="Hand" BorderColor="Gray" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
														<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
															<Padding Left="3px"></Padding>
															<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
														</RowStyleDefault>
													</DisplayLayout>
													<Bands>
														<igtbl:UltraGridBand>
															<Columns>
																<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" BaseColumnName="ItemNum">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle Height="25px"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" BaseColumnName="ItemDrawNum">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" BaseColumnName="ItemName">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="단가구분" Key="UnitCostDistinction" BaseColumnName="UnitCostDistinction">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" BaseColumnName="CompanyName">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" BaseColumnName="BusinessRegistrationNum">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="시작공정" Key="SmallClassificationName" Hidden="True" BaseColumnName="SmallClassificationName">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="종료공정" Key="SmallClassificationName" Hidden="True" BaseColumnName="SmallClassificationName">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="발주비율" Key="OrderRate" Format="###,###,###.##" BaseColumnName="OrderRate">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="기준단가" Key="StandardUnitCost" Format="\ ###,###,##0" BaseColumnName="StandardUnitCost">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="할인단가" Key="DiscountUnitCost" Format="\ ###,###,##0" BaseColumnName="DiscountUnitCost">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="적용시작일" Key="BeginDate" Format="yyyy-MM-dd" BaseColumnName="BeginDate">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="적용종료일" Key="EndDate" Format="yyyy-MM-dd" BaseColumnName="EndDate">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="레코드상태" Key="RecodingState" Hidden="True" BaseColumnName="RecodingState">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" BaseColumnName="RegistrationPerson">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" BaseColumnName="UpdatingPerson">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="번호" Key="UnitCostInfoIndex" Hidden="True" BaseColumnName="UnitCostInfoIndex">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
															</Columns>
														</igtbl:UltraGridBand>
													</Bands>
												</igtbl:ultrawebgrid>
												<STYLE BorderStyle="Solid" BorderWidth="1px" BackColor="LightGray">
												</STYLE>
											</ADDNEWBOX><PAGER>
												<STYLE BorderStyle="Solid" BorderWidth="1px" BackColor="LightGray">
												</STYLE>
											</PAGER><HEADERSTYLEDEFAULT BorderStyle="Solid" BackColor="LightGray"><BORDERDETAILS WidthLeft="1px" WidthTop="1px" ColorTop="White" ColorLeft="White"></BORDERDETAILS>
											</HEADERSTYLEDEFAULT><FRAMESTYLE BorderStyle="Solid" BorderWidth="1px" Font-Size="8pt" Height="150px" Width="800px"
												Font-Names="Verdana"></FRAMESTYLE><FOOTERSTYLEDEFAULT BorderStyle="Solid" BorderWidth="1px" BackColor="LightGray"><BORDERDETAILS WidthLeft="1px" WidthTop="1px" ColorTop="White" ColorLeft="White"></BORDERDETAILS>
											</FOOTERSTYLEDEFAULT><EDITCELLSTYLEDEFAULT BorderStyle="None" BorderWidth="0px"></EDITCELLSTYLEDEFAULT><ROWSTYLEDEFAULT BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray"><PADDING Left="3px"></PADDING>
												<BORDERDETAILS WidthLeft="0px" WidthTop="0px"></BORDERDETAILS>
											</ROWSTYLEDEFAULT></DISPLAYLAYOUT><BANDS></BANDS></TD>
									</TR>
									<TR>
										<TD align="right" width="70" height="5"><FONT face="굴림"></FONT></TD>
										<TD align="left" width="130" height="5"></TD>
										<TD align="right" width="70" height="5"></TD>
										<TD align="left" width="130" height="5"></TD>
										<TD align="right" width="70" height="5"></TD>
										<TD align="left" width="130" height="5"></TD>
										<TD align="right" width="70" height="5"></TD>
										<TD align="left" width="130" height="5"></TD>
									</TR>
									<TR>
										<TD align="right" width="70"><FONT style="COLOR: red" face="굴림">*거래처&nbsp;</FONT></TD>
										<TD align="left" width="130"><DROPDOWNLAYOUT DropdownHeight="130px" HeaderClickAction="Select" RowHeightDefault="20px" BorderCollapse="Separate"
												DropdownWidth="325px">
												<igcmbo:webcombo id="wcCompany" runat="server" BorderStyle="Solid" BorderWidth="1px" BorderColor="DimGray"
													BackColor="#EEEEE9" Font-Size="10pt" Height="22px" Width="115px" ForeColor="Black" DropImageXP2="/ig_common/WebGrid3/ig_cmboDown2.bmp"
													DropImageXP1="/ig_common/WebGrid3/ig_cmboDown1.bmp" Version="3.00" SelBackColor="10, 36, 106"
													SelectedIndex="-1" SelForeColor="White">
													<Columns>
														<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" Width="250px" BaseColumnName="CompanyName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="대표자명" Key="PresidentName" Hidden="True" BaseColumnName="PresidentName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Width="300px" BaseColumnName="BusinessRegistrationNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="번호" Key="CompanyInfoIndex" Hidden="True" BaseColumnName="CompanyInfoIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
													</Columns>
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
												</igcmbo:webcombo>
												<ROWSTYLE BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray" BackColor="White">
													<BORDERDETAILS WidthLeft="0px" WidthTop="0px"></BORDERDETAILS>
												</ROWSTYLE>
												<SELECTEDROWSTYLE BackColor="DarkBlue" ForeColor="White"></SELECTEDROWSTYLE>
												<HEADERSTYLE BorderStyle="Solid" BackColor="LightGray">
													<BORDERDETAILS WidthLeft="1px" WidthTop="1px" ColorTop="White" ColorLeft="White"></BORDERDETAILS>
												</HEADERSTYLE>
												<FRAMESTYLE BorderStyle="Ridge" BorderWidth="2px" BackColor="Silver" Font-Size="10pt" Height="100%"
													Width="100%" Cursor="Default" Font-Names="Verdana"></FRAMESTYLE>
											</DROPDOWNLAYOUT><EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS></TD>
										<TD align="right" width="70"><FONT style="COLOR: red" face="굴림">*발주비율&nbsp;</FONT></TD>
										<TD align="left" width="130"><FONT face="굴림" size="3">
												<asp:textbox id="tb_OrderRate" style="TEXT-ALIGN: right" runat="server" BorderStyle="Solid" BorderWidth="1px"
													BorderColor="DimGray" BackColor="#EEEEE9" Font-Size="9pt" Height="20px" Width="80px">0</asp:textbox>%</FONT>
										</TD>
										<TD align="right" width="70"><FONT style="COLOR: red" face="굴림">*기준단가&nbsp;</FONT></TD>
										<TD align="left" width="130">
											<asp:TextBox id="tb_StandardUnitCost" style="TEXT-ALIGN: right" runat="server" BorderStyle="Solid"
												BorderWidth="1px" BorderColor="DimGray" BackColor="#EEEEE9" Font-Size="9pt" Height="20px"
												Width="80px"></asp:TextBox>원
										</TD>
										<TD align="right" width="70"><FONT face="굴림">할인단가&nbsp;</FONT></TD>
										<TD align="left" width="130"><DROPDOWNLAYOUT DropdownHeight="130px" HeaderClickAction="Select" RowHeightDefault="20px" BorderCollapse="Separate"
												DropdownWidth="325px">
<asp:textbox id="tb_DiscountUnitCost" style="TEXT-ALIGN: right" runat="server" BorderStyle="Solid"
													BorderWidth="1px" BorderColor="DimGray" BackColor="#EEEEE9"
													Font-Size="9pt" Height="20px" Width="80px">0</asp:textbox>원 
            <ROWSTYLE BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray" BackColor="White">
													<BORDERDETAILS WidthLeft="0px" WidthTop="0px"></BORDERDETAILS>
												</ROWSTYLE><SELECTEDROWSTYLE BackColor="DarkBlue" ForeColor="White"></SELECTEDROWSTYLE><HEADERSTYLE BorderStyle="Solid" BackColor="LightGray">
													<BORDERDETAILS WidthLeft="1px" WidthTop="1px" ColorTop="White" ColorLeft="White"></BORDERDETAILS>
												</HEADERSTYLE><FRAMESTYLE BorderStyle="Ridge" BorderWidth="2px" BackColor="Silver" Font-Size="10pt" Height="100%"
													Width="100%" Cursor="Default" Font-Names="Verdana"></FRAMESTYLE></DROPDOWNLAYOUT><EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS></TD>
									</TR>
									<TR>
										<TD align="right" width="70" height="5"><FONT face="굴림"></FONT></TD>
										<TD align="left" width="130" height="5"></TD>
										<TD align="right" width="70" height="5"></TD>
										<TD align="left" width="130" height="5"></TD>
										<TD align="right" width="70" height="5"></TD>
										<TD align="left" width="130" height="5"></TD>
										<TD align="right" width="70" height="5"></TD>
										<TD align="left" width="130" height="5"></TD>
									</TR>
									<TR>
										<TD align="right" width="70"><FONT style="COLOR: red" face="굴림">*적용시작일&nbsp;</FONT></TD>
										<TD align="left" width="130"><CALENDARLAYOUT FooterFormat="Today: {0:d}">
												<igsch:webdatechooser id="wdcBeginDate" runat="server" BorderStyle="Solid" BorderColor="DimGray" BackColor="#EEEEE9"
													Height="20px" Width="100px" Text="Null" MaxDate="2079-06-06" NullDateLabel=" ">
													<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
														MaxDate="2079-06-06" ShowTitle="False" ShowFooter="False">
														<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
														<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
														<DropDownStyle BackColor="White"></DropDownStyle>
														<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
													</CalendarLayout>
													<DropDownStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></DropDownStyle>
													<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
													<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
												</igsch:webdatechooser>
											</CALENDARLAYOUT><EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS></TD>
										<TD style="HEIGHT: 7px" align="right" width="70"><FONT face="굴림">적용종료일&nbsp;</FONT></TD>
										<TD style="HEIGHT: 7px" align="left" width="130"><CALENDARLAYOUT FooterFormat="Today: {0:d}">
												<igsch:webdatechooser id="wdcEndDate" runat="server" BorderStyle="Solid" BorderColor="DimGray" BackColor="#EEEEE9"
													Height="20px" Width="90px" Text="Null" MaxDate="2079-06-06" NullDateLabel=" ">
													<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
														MaxDate="2079-06-06" ShowTitle="False" ShowFooter="False">
														<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
														<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
														<DropDownStyle BackColor="White"></DropDownStyle>
														<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
													</CalendarLayout>
													<DropDownStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></DropDownStyle>
													<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
													<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
												</igsch:webdatechooser>
											</CALENDARLAYOUT><EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS></TD>
										<TD style="HEIGHT: 7px" align="right" width="70">&nbsp;</TD>
										<TD style="HEIGHT: 7px" align="left" width="130"><FONT face="굴림"><INPUT id="lb_ReferenceIndex" style="WIDTH: 30px; HEIGHT: 22px" type="hidden" name="lb_ReferenceIndex"
													runat="server" size="1"><FONT face="굴림"><INPUT id="lb_ReferenceTable" style="WIDTH: 30px; HEIGHT: 22px" type="hidden" name="lb_ReferenceTable"
														runat="server" size="1"></FONT></FONT></TD>
										<TD style="RIGHT: 97px; HEIGHT: 7px" width="70"></TD>
										<TD style="HEIGHT: 7px" align="left" width="130"><FONT face="굴림"><INPUT id="lb_Index" style="WIDTH: 77px; HEIGHT: 22px" type="hidden" size="7" value="0"
													name="lb_Index" runat="server"></FONT></TD>
									</TR>
									<TR>
										<TD align="right" width="70" height="5"><FONT face="굴림"></FONT></TD>
										<TD align="left" width="130" height="5"></TD>
										<TD align="right" width="70" height="5"></TD>
										<TD align="left" width="130" height="5"></TD>
										<TD align="right" width="70" height="5"></TD>
										<TD align="left" width="130" height="5"></TD>
										<TD style="RIGHT: 97px" width="70" height="5"></TD>
										<TD align="left" width="130" height="5"></TD>
									</TR>
									<TR>
										<TD align="right" width="70">
											<asp:button id="bt_RelationView" runat="server" Height="20px" Width="60px" Text="참고보기"></asp:button><FONT face="굴림"></FONT></TD>
										<TD align="left" colSpan="4"><FONT face="굴림">&nbsp;&nbsp;<INPUT id="File1" style="WIDTH: 320px; HEIGHT: 20px" type="file" size="34" name="File1"
													runat="server">&nbsp;
												<asp:button id="tbTotalUpdate" runat="server" Height="20px" Width="60px" Text="일괄수정"></asp:button>
											</FONT>
										</TD>
										<TD align="right" width="400" colSpan="3">
											<asp:button id="bt_Clear" runat="server" Height="20px" Width="60px" Text="초기화"></asp:button>
											<asp:button id="bt_Update" runat="server" Height="20px" Width="60px" Text="수  정" Enabled="False"></asp:button>
											<asp:button id="bt_Delete" runat="server" Height="20px" Width="60px" Text="삭  제" Enabled="False"></asp:button>
											<asp:button id="bt_Registration" runat="server" Height="20px" Width="60px" Text="등  록"></asp:button>&nbsp;</TD>
									</TR>
									<TR>
										<TD align="right" width="70" height="5"><FONT face="굴림"></FONT></TD>
										<TD align="left" width="130" height="5"><FONT face="굴림"></FONT></TD>
										<TD align="right" width="70" height="5"></TD>
										<TD align="left" width="130" height="5"></TD>
										<TD align="right" width="70" height="5"></TD>
										<TD align="right" width="400" colSpan="3" height="5"></TD>
									</TR>
									<TR>
										<TD colSpan="8" height="10">
											<igtbl:ultrawebgrid id="Ultrawebgrid2" runat="server" Height="120px" Width="800px">
												<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
													RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Extended" AllowColumnMovingDefault="OnServer"
													HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
													RowSelectorsDefault="No" Name="Ultrawebgrid2" TableLayout="Fixed" CellClickActionDefault="RowSelect"
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
													<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</HeaderStyleDefault>
													<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
													<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderColor="DimGray"
														BorderStyle="Solid" BackColor="Silver" Height="120px"></FrameStyle>
													<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</FooterStyleDefault>
													<ClientSideEvents AfterSelectChangeHandler="Ultrawebgrid2_AfterSelectChangeHandler"></ClientSideEvents>
													<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
													<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="White"></SelectedRowStyleDefault>
													<RowAlternateStyleDefault Cursor="Hand" BorderColor="DarkGray" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
													<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
														<Padding Left="3px"></Padding>
														<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
													</RowStyleDefault>
												</DisplayLayout>
												<Bands>
													<igtbl:UltraGridBand>
														<Columns>
															<igtbl:UltraGridColumn HeaderText="단가정보테이블" Key="StandardinfoTableID" Width="150px" BaseColumnName="StandardinfoTableID">
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
						</TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
	</body>
</HTML>
