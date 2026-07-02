<%@ Page language="c#" Codebehind="BusinessPlanInfoPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ManagementInfomation.BusinessPlanInfoPC" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>BusinessPlanInfoPC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		
		function Success()
		{
			__doPostBack('ComSelect','');
		}
		
		//거래처 선택시
		function wcCompany_AfterSelectChange(webComboId){
			//Add code to handle your event here.
			if(document.BusinessPlanInfo.ItemSearchControl1_txtItemNum.value == "")
			{
				alert('품목을 먼저 선택하세요!');
				return false;
			}
			else
				return Success();
		}
		
		function DoPost()
		{
			__doPostBack('LinkButton2','');
		}
		
		function UltraWebGrid1_DblClickHandler(gridName, cellId){
			//Add code to handle your event here.
			var row = igtbl_getRowById(cellId);
			
			var combo = igcmbo_getComboById("wcCompany");//거래처명
			
			combo.setDataValue(row.getCellFromKey("BusinessRegistrationNum").getValue());//사업자등록번호
			combo.setDisplayValue(row.getCellFromKey("CompanyName").getValue());//거래처명
			
			document.BusinessPlanInfo.tb_PlanQuantity.value = row.getCellFromKey("PlanQuantity").getValue()	//계획량
			document.BusinessPlanInfo.tb_SaleUnitCost.value = row.getCellFromKey("SaleUnitCost").getValue();//기준단가
			document.BusinessPlanInfo.tb_PlanTotalCost.value = row.getCellFromKey("PlanTotalCost").getValue();		//금액
			document.BusinessPlanInfo.lb_Index.value = row.getCellFromKey("BusinessPlanInfoIndex").getValue();
			
			
			//년
			for(var i=0;i<document.BusinessPlanInfo.dl_Year.options.length;i++)
			{
				if(document.BusinessPlanInfo.dl_Year.options[i].innerText == row.getCellFromKey("PlanYear").getValue())
				{
					document.BusinessPlanInfo.dl_Year.options[i].selected=true;
				}
			}
			
			//월
			for(var i=0;i<document.BusinessPlanInfo.dl_Month.options.length;i++)
			{
				if(document.BusinessPlanInfo.dl_Month.options[i].innerText == row.getCellFromKey("PlanMonth").getValue())
				{
					document.BusinessPlanInfo.dl_Month.options[i].selected=true;
				}
			}
			//일
			for(var i=0;i<document.BusinessPlanInfo.dl_Day.options.length;i++)
			{
				if(document.BusinessPlanInfo.dl_Day.options[i].innerText == row.getCellFromKey("PlanDay").getValue())
				{
					document.BusinessPlanInfo.dl_Day.options[i].selected=true;
				}
			}
			
			
			document.BusinessPlanInfo.bt_Update.disabled = false;
		    document.BusinessPlanInfo.bt_Delete.disabled = false;
		    document.BusinessPlanInfo.bt_Registration.disabled = true;
			
		}
		
		
		function Ultrawebgrid2_AfterSelectChangeHandler(gridName, id){
			//Add code to handle your event here.
			var row = igtbl_getRowById(id);
			BusinessPlanInfo.lb_ReferenceTable.value = row.getCellFromKey("CommunityDataTableID").getValue();
			BusinessPlanInfo.lb_ReferenceIndex.value = row.getCellFromKey("CDTIndex").getValue();
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
		
		function OK(text)
		{
			if(confirm(text + "하시겠습니까?"))
				return true;
			else
				return false;
		}
		
		
		
		// 함격수량 입력시 onkeyup 이벤트 때 발생.
		function Process()	
		{
			var total = Number(BusinessPlanInfo.tb_PlanQuantity.value) * Number(BusinessPlanInfo.tb_SaleUnitCost.value);
			BusinessPlanInfo.tb_PlanTotalCost.value = total;
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
	<body bgColor="#f7f6f6" onload="Load()" MS_POSITIONING="GridLayout">
		<form id="BusinessPlanInfo" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" height="540"
				cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE id="table1" height="30" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD align="left" width="600" colSpan="2" height="30"><FONT face="굴림"></FONT></TD>
								<TD vAlign="bottom" align="right" width="200" height="30"><asp:linkbutton id="LinkButton1" runat="server" Font-Size="9pt">전체사업계획보기</asp:linkbutton></TD>
							</TR>
						</TABLE>
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"
							align="left"><LEGEND>[ 입 력 ]</LEGEND>
							<TABLE cellSpacing="0" cellPadding="0" width="800">
								<TR>
									<TD align="left" width="600" colSpan="4" height="20"><FONT face="굴림"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></FONT></TD>
									<TD align="right" width="70" height="20"><FONT face="굴림">자산분류&nbsp; </FONT>
									</TD>
									<TD align="left" width="130" height="20"><FONT face="굴림"><asp:textbox id="tb_PropertyClassification" runat="server" Font-Size="9pt" ReadOnly="True" BorderStyle="Solid"
												BorderWidth="1px" BorderColor="DimGray" BackColor="#EEEEE9" Height="20px" Width="100px"></asp:textbox></FONT></TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="5"><FONT face="굴림"></FONT></TD>
									<TD align="left" width="150" height="5"></TD>
									<TD align="right" width="70" height="5"></TD>
									<TD align="left" width="150" height="5"></TD>
									<TD align="right" width="70" height="5"></TD>
									<TD align="left" width="130" height="5"></TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="left" width="800" colSpan="6" height="20"><FONT face="굴림">
											<TABLE id="Table2" height="20" cellSpacing="0" cellPadding="0" width="800" border="0">
												<TBODY>
													<TR>
														<TD align="right" width="70"><FONT style="COLOR: black" face="굴림">단 위&nbsp; </FONT>
														</TD>
														<TD width="130"><asp:textbox id="tb_Unit" runat="server" Font-Size="9pt" ReadOnly="True" BorderStyle="Solid"
																BorderWidth="1px" BorderColor="DimGray" BackColor="#EEEEE9" Height="20px" Width="97px"></asp:textbox></TD>
														<TD align="right" width="70"><FONT style="COLOR: black" face="굴림" color="#ff0000">규 격</FONT>
											&nbsp; </FONT><FONT face="굴림"></FONT>
									</TD>
									<TD width="130"><asp:textbox id="tb_Standard" runat="server" Font-Size="9pt" ReadOnly="True" BorderStyle="Solid"
											BorderWidth="1px" BorderColor="DimGray" BackColor="#EEEEE9" Height="20px" Width="100px"></asp:textbox></TD>
									<TD width="70"></TD>
									<TD width="130"></TD>
									<TD width="70"><asp:label id="lb_ItemIndex" runat="server" Font-Size="8pt" Height="8px" Width="23px" Visible="False">0</asp:label></TD>
									<TD width="130"><asp:button id="bt_BusinessPlanView" runat="server" Font-Size="9pt" Height="20px" Width="90px"
											Text="사업계획보기"></asp:button></TD>
								</TR>
							</TABLE></FONT></TD>
				</TR>
				<TR>
					<TD align="right" width="70" height="8"></TD>
					<TD align="left" width="150" height="8"></TD>
					<TD align="right" width="70" height="8"></TD>
					<TD align="left" width="150" height="8"><INPUT id="c" style="WIDTH: 30px; HEIGHT: 12px" type="hidden" size="1" name="ItemIndex"
							runat="server">
						<asp:linkbutton id="LinkButton2" runat="server" Visible="False">LinkButton</asp:linkbutton></TD>
					<TD align="right" width="200" colSpan="2" height="8"></TD>
				</TR>
				<TR>
					<TD width="800" colSpan="6" height="250"><FONT face="굴림"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="250px" Width="800px">
<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes" RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect">

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

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

</HeaderStyleDefault>

<RowSelectorStyleDefault Cursor="Hand">
</RowSelectorStyleDefault>

<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderColor="DimGray" BorderStyle="Solid" BackColor="Silver" Height="250px">
</FrameStyle>

<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

</FooterStyleDefault>

<ClientSideEvents DblClickHandler="UltraWebGrid1_DblClickHandler">
</ClientSideEvents>

<EditCellStyleDefault BorderWidth="0px" BorderStyle="None">
</EditCellStyleDefault>

<SelectedRowStyleDefault ForeColor="White" BackColor="Navy">
</SelectedRowStyleDefault>

<RowAlternateStyleDefault BorderColor="DarkGray" BackColor="LightSteelBlue">
</RowAlternateStyleDefault>

<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">

<Padding Left="3px">
</Padding>

<BorderDetails WidthLeft="0px" WidthTop="0px">
</BorderDetails>

</RowStyleDefault>

</DisplayLayout>

<Bands>
<igtbl:UltraGridBand>
<Columns>
<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" BaseColumnName="ItemNum">
<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellButtonStyle>

<HeaderStyle Height="25px">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" BaseColumnName="ItemDrawNum">
<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellButtonStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" BaseColumnName="ItemName">
<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellButtonStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="계획년도" Key="PlanYear" BaseColumnName="PlanYear">
<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellButtonStyle>

<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="월" Key="PlanMonth" BaseColumnName="PlanMonth">
<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellButtonStyle>

<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="일" Key="PlanDay" Hidden="True" BaseColumnName="PlanDay">
<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellButtonStyle>

<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="계획수량" Key="PlanQuantity" Format="###,###,###.##" BaseColumnName="PlanQuantity">
<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right">
</CellButtonStyle>

<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="판매단가" Key="SaleUnitCost" Format="\ ###,###,##0" BaseColumnName="SaleUnitCost">
<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right">
</CellButtonStyle>

<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="계획 총 금액" Key="PlanTotalCost" Format="###,###,###.##" BaseColumnName="PlanTotalCost">
<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right">
</CellButtonStyle>

<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" BaseColumnName="CompanyName">
<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellButtonStyle>

<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" BaseColumnName="BusinessRegistrationNum">
<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellButtonStyle>

<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="레코드상태" Key="RecodingState" Hidden="True" BaseColumnName="RecodingState">
<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellButtonStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" BaseColumnName="RegistrationPerson">
<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellButtonStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID">
<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellButtonStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate">
<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellButtonStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" BaseColumnName="UpdatingPerson">
<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellButtonStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellButtonStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellButtonStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="번호" Key="BusinessPlanInfoIndex" BaseColumnName="BusinessPlanInfoIndex">
<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellButtonStyle>
</igtbl:UltraGridColumn>
</Columns>
</igtbl:UltraGridBand>
</Bands>
							</igtbl:ultrawebgrid></FONT></TD>
				</TR>
				<TR>
					<TD colSpan="6"><BR>
						<TABLE style="WIDTH: 800px; HEIGHT: 18px" cellSpacing="0" cellPadding="0">
							<TBODY>
								<TR>
									<TD align="right" width="70"><FONT style="COLOR: #ff0000" face="굴림">*거래처명&nbsp;</FONT></TD>
									<TD align="left" width="100"><FONT face="굴림"><igcmbo:webcombo id="wcCompany" runat="server" Font-Size="9pt" BorderStyle="Solid" BorderWidth="1px"
												BorderColor="DimGray" BackColor="#EEEEE9" Height="20px" Width="100" SelForeColor="White" SelectedIndex="-1" SelBackColor="10, 36, 106"
												Version="3.00" DropImageXP1="/ig_common/WebGrid3/ig_cmboDown1.bmp" DropImageXP2="/ig_common/WebGrid3/ig_cmboDown2.bmp" ForeColor="Black">
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
												<ClientSideEvents AfterSelectChange="wcCompany_AfterSelectChange"></ClientSideEvents>
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
											</igcmbo:webcombo></FONT></TD>
									<TD align="right" width="10"><FONT style="COLOR: #ff0000" face="굴림">년</FONT></TD>
									<TD align="left" width="70"><asp:dropdownlist id="dl_Year" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Height="20px" Width="80px">
											<asp:ListItem Value="0">=선택=</asp:ListItem>
											<asp:ListItem Value="2004">2004</asp:ListItem>
											<asp:ListItem Value="2005">2005</asp:ListItem>
											<asp:ListItem Value="2006">2006</asp:ListItem>
											<asp:ListItem Value="2007">2007</asp:ListItem>
											<asp:ListItem Value="2008">2008</asp:ListItem>
											<asp:ListItem Value="2009">2009</asp:ListItem>
											<asp:ListItem Value="2010">2010</asp:ListItem>
											<asp:ListItem Value="2011">2011</asp:ListItem>
											<asp:ListItem Value="2012">2012</asp:ListItem>
											<asp:ListItem Value="2013">2013</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD align="right" width="10"><FONT style="COLOR: #ff0000" face="굴림">월</FONT></TD>
									<TD align="left" width="70"><asp:dropdownlist id="dl_Month" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Height="20px" Width="80px">
											<asp:ListItem Value="0">=선택=</asp:ListItem>
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
									<TD align="right" width="10"><FONT style="COLOR: #ff0000" face="굴림">일</FONT></TD>
									<TD align="left" width="70"><asp:dropdownlist id="dl_Day" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Height="20px" Width="80px">
											<asp:ListItem Value="0">=선택=</asp:ListItem>
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
											<asp:ListItem Value="13">13</asp:ListItem>
											<asp:ListItem Value="14">14</asp:ListItem>
											<asp:ListItem Value="15">15</asp:ListItem>
											<asp:ListItem Value="16">16</asp:ListItem>
											<asp:ListItem Value="17">17</asp:ListItem>
											<asp:ListItem Value="18">18</asp:ListItem>
											<asp:ListItem Value="19">19</asp:ListItem>
											<asp:ListItem Value="20">20</asp:ListItem>
											<asp:ListItem Value="21">21</asp:ListItem>
											<asp:ListItem Value="22">22</asp:ListItem>
											<asp:ListItem Value="23">23</asp:ListItem>
											<asp:ListItem Value="24">24</asp:ListItem>
											<asp:ListItem Value="25">25</asp:ListItem>
											<asp:ListItem Value="26">26</asp:ListItem>
											<asp:ListItem Value="27">27</asp:ListItem>
											<asp:ListItem Value="28">28</asp:ListItem>
											<asp:ListItem Value="29">29</asp:ListItem>
											<asp:ListItem Value="30">30</asp:ListItem>
											<asp:ListItem Value="31">31</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD align="right" width="35"><FONT face="굴림"><FONT style="COLOR: #ff0000" face="굴림">수량&nbsp;</FONT></FONT></TD>
									<TD align="left" width="90"><asp:textbox id="tb_PlanQuantity" style="TEXT-ALIGN: right" runat="server" Font-Size="9pt" BorderStyle="Solid"
											BorderWidth="1px" BorderColor="DimGray" BackColor="#EEEEE9" Height="20px" Width="70px">0</asp:textbox></TD>
									<TD align="right" width="35"><FONT face="굴림"><FONT style="COLOR: #ff0000" face="굴림">단가&nbsp;</FONT></FONT>
									</TD>
									<TD align="left" width="90"><asp:textbox id="tb_SaleUnitCost" style="TEXT-ALIGN: right" runat="server" Font-Size="9pt" BorderStyle="Solid"
											BorderWidth="1px" BorderColor="DimGray" BackColor="#EEEEE9" Height="20px" Width="90px">0</asp:textbox></TD>
									<TD style="COLOR: #ff0000" align="right" width="35">금액&nbsp;</TD>
									<TD align="left" width="90"><asp:textbox id="tb_PlanTotalCost" style="TEXT-ALIGN: right" runat="server" Font-Size="9pt" BorderStyle="Solid"
											BorderWidth="1px" BorderColor="DimGray" BackColor="#EEEEE9" Height="20px" Width="70px">0</asp:textbox></TD>
								</TR>
								<TR>
									<TD align="right" width="70"><asp:button id="bt_Reference" runat="server" Font-Size="9pt" Height="20px" Width="60px" Text="참고 보기"></asp:button>&nbsp;</TD>
									<TD align="right" width="740" colSpan="13"><asp:label id="lb_Idx" runat="server" Height="10px" Width="8px" Visible="False">0</asp:label><INPUT id="lb_ReferenceTable" style="WIDTH: 30px" type="hidden" name="lb_ReferenceTable"
											runat="server"> <INPUT id="lb_Index" style="WIDTH: 50px" type="hidden" value="0" name="lb_Index" runat="server"><INPUT id="lb_ReferenceIndex" style="WIDTH: 30px" type="hidden" name="lb_ReferenceIndex"
											runat="server">
										<asp:linkbutton id="ComSelect" runat="server" Visible="False">ComSelect</asp:linkbutton><asp:button id="bt_Clear" runat="server" Height="20px" Width="60px" Text="초기화"></asp:button><asp:button id="bt_Update" runat="server" Height="20px" Width="60px" Text="수  정" Enabled="False"></asp:button><asp:button id="bt_Delete" runat="server" Height="20px" Width="60px" Text="삭  제" Enabled="False"></asp:button><asp:button id="bt_Registration" runat="server" Height="20px" Width="60px" Text="등  록"></asp:button>&nbsp;</TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="5"><FONT face="굴림"></FONT></TD>
									<TD align="right" width="730" colSpan="13" height="5"><FONT face="굴림"></FONT></TD>
								</TR>
				</TR>
			</TABLE></TD>
			<TR>
				<TD width="800" colSpan="6" height="10"><igtbl:ultrawebgrid id="Ultrawebgrid2" runat="server" Height="135px" Width="800px">
						<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
							RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Extended" AllowColumnMovingDefault="OnServer"
							BorderCollapseDefault="Separate" RowSelectorsDefault="No" Name="Ultrawebgrid2" TableLayout="Fixed"
							CellClickActionDefault="RowSelect" NoDataMessage="">
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
								BorderStyle="Solid" BackColor="Silver" Height="135px"></FrameStyle>
							<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
								<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
							</FooterStyleDefault>
							<ClientSideEvents AfterSelectChangeHandler="Ultrawebgrid2_AfterSelectChangeHandler"></ClientSideEvents>
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
									<igtbl:UltraGridColumn HeaderText="사업계획정보테이블" Key="StandardinfoTableID" Width="150px" BaseColumnName="StandardinfoTableID">
										<HeaderStyle Height="25px"></HeaderStyle>
									</igtbl:UltraGridColumn>
									<igtbl:UltraGridColumn HeaderText="품목정보인덱스" Key="SITIndex" Width="50px" Hidden="True" BaseColumnName="SITIndex"></igtbl:UltraGridColumn>
									<igtbl:UltraGridColumn HeaderText="자료실테이블" Key="CommunityDataTableID" Width="150px" BaseColumnName="CommunityDataTableID"></igtbl:UltraGridColumn>
									<igtbl:UltraGridColumn HeaderText="자료실인덱스" Key="CDTIndex" Width="50px" Hidden="True" BaseColumnName="CDTIndex"></igtbl:UltraGridColumn>
									<igtbl:UltraGridColumn HeaderText="제목" Key="Title" Width="710px" HeaderClickAction="SortSingle" BaseColumnName="Title"></igtbl:UltraGridColumn>
									<igtbl:UltraGridColumn HeaderText="번호" Key="CRelationShipIndex" Width="50px" Hidden="True" BaseColumnName="CRelationShipIndex"></igtbl:UltraGridColumn>
								</Columns>
							</igtbl:UltraGridBand>
						</Bands>
					</igtbl:ultrawebgrid></TD>
			</TR></TBODY></TABLE></FIELDSET></TD></TR></TBODY></TABLE></form>
	</body>
</HTML>
