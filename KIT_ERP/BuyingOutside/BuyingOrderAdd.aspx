<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo1" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Page language="c#" Codebehind="BuyingOrderAdd.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BuyingOutside.BuyingOrderAdd" codePage="949" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../MessageWindows.js"></script>
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--
	
		function ResettxtBox()
		{
			ResetBox();
			ResetTextBox();
		}
		
		
		
		
		//////////////////////////////////////////////////////////////////////////////////////////
		//															Float	        				//
		//////////////////////////////////////////////////////////////////////////////////////////
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
							event.keyCode == 46 ||
							event.keyCode == 48 ||							
							event.keyCode == 49 ||
							event.keyCode == 50 ||
							event.keyCode == 51 ||
							event.keyCode == 52 ||
							event.keyCode == 53 ||
							event.keyCode == 54 ||
							event.keyCode == 55 ||
							event.keyCode == 56 ||
							event.keyCode == 57 ||							
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
						
		/*****************************************************************************************************************************************************************/
		/*그리드를 선택했을때 입력란 부분으로 그리드의 내용을 보여주는 함수                                                                                              */
		/*****************************************************************************************************************************************************************/
		function uwgBuyingOrderAdd_DblClickHandler(gridName, cellId){
		
			
			
			var objChooser1 = igdrp_getComboById("txtFirstDate");//1차 납기요구일 WebChooser
			var objGrid = igtbl_getActiveRow(gridName)//현재 선택되어진 객체의 행의 가져옴
			
			var row = igtbl_getRowById(cellId);
						
			document.Form1.ItemSearchControl1_txtItemNum.value = row.getCellFromKey("ItemNum").getValue();//그리드에서 선택한 품목번호를 유저컨트롤내의 품목번호 컨트롤에 넣음
			document.Form1.ItemSearchControl1_txtItemDrawNum.value = row.getCellFromKey("ItemDrawNum").getValue();//그리드에서 선택한 도면번호를 유저컨트롤내의 품목번호 컨트롤에 넣음
			document.Form1.ItemSearchControl1_txtItemName.value = row.getCellFromKey("ItemName").getValue();//그리드에서 선택한 품목명를 유저컨트롤내의 품목번호 컨트롤에 넣음
			
			document.Form1.CSC1_txtCompanyName.value = row.getCellFromKey("CompanyName").getValue();//그리드에서 선택한 품목명를 유저컨트롤내의 품목번호 컨트롤에 넣음
			document.Form1.CSC1_txtBusinessRegistrationNum.value = row.getCellFromKey("BusinessRegistrationNum").getValue();//그리드에서 선택한 품목명를 유저컨트롤내의 품목번호 컨트롤에 넣음
			document.Form1.hdBusinessRegistrationNum.value = row.getCellFromKey("BusinessRegistrationNum").getValue();//그리드에서 선택한 품목번호를 Hidden 필드에 넣음
			document.Form1.hdItemNum.value = row.getCellFromKey("ItemNum").getValue();//WebCombo에 그리드에서 선택한 품목명을 넣음
			
					
			//1차납품요구량,요구일
			document.Form1.txtFirstQuantity.value = row.getCellFromKey("FirstDeliveryDemandQuantity").getValue();//그리드에서 선택한 1차납품요구량을 넣음
			objChooser1.setValue(objGrid.getCellFromKey("FirstDeliveryDemandDate").getValue());//그리드에서 선택한 1차납품요구일을 넣음
			
				
			
			//document.Form1.txtTotalQuantity.value = row.getCellFromKey("OrderQuantity").getValue();//그리드에서 선택한 총발주량을 넣음
			document.Form1.txtApplyUnitCost.value = row.getCellFromKey("ApplyUnitCost").getValue();//그리드에서 선택한 단가를 넣음
			document.Form1.hdindex.value = row.getCellFromKey("BuyingOrderHistoryIndex").getValue();//원장 인덱스 번호
			
			document.Form1.btnEdit.disabled = false;
		    document.Form1.btnDelete.disabled = false;
		    document.Form1.btnOrder.disabled = true;
					
					
		}
		function uwgBuyingOrderAdd_AfterSelectChangeHandler(gridName, rowId){
			
			///////////RowIndex Hidden 에 저장////////////////
			var num = rowId.split('_');
			document.Form1.rowIndex.value = num[1];
			/////////////////////////////////////////////////
		}
		
		function DoPost()
		{
			__doPostBack("LinkButton1","");
		}
		
		function DoPost1()
		{
			__doPostBack("LinkButton2","");
		}
	
	
					
					
		--></SCRIPT>
	</HEAD>
	<body text="#0" bottomMargin="0" bgColor="#f7f6f6" leftMargin="0" topMargin="0" rightMargin="0"
		ms_positioning="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 100; POSITION: absolute; WIDTH: 800px; LEFT: 10px" cellSpacing="0"
				cellPadding="0" border="0">
				<TR>
					<TD width="20" height="40"><FONT face="굴림"></FONT></TD>
					<TD align="center" height="40">
						<FIELDSET style="BORDER-BOTTOM: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; HEIGHT: 40px; BORDER-TOP: dimgray 2px solid; BORDER-RIGHT: dimgray 2px solid; BORDER-: black 2px solid"
							align="top"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 입력 ]</LEGEND>
							<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="856" border="0">
								<TR>
									<TD vAlign="middle" align="left" width="200" bgColor="#f5f5f5" colSpan="2" height="20"><uc1:companysearchcontrol id="CSC1" runat="server"></uc1:companysearchcontrol></TD>
									<TD vAlign="middle" align="left" width="600" colSpan="6" height="20"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="left" width="800" bgColor="#f5f5f5" colSpan="8" height="30">
										<table id="Table5" cellSpacing="0" cellPadding="0" border="0">
											<tr>
												<td align="right" width="40">단위</td>
												<td width="70"><asp:textbox id="txtUnit" runat="server" Width="70px" BorderStyle="Solid" BorderWidth="1px" BackColor="#EEEEE9"
														Height="20px" BorderColor="DimGray"></asp:textbox></td>
												<td align="right" width="100">발주간격량</td>
												<td width="50"><asp:textbox id="txtOrderIntervalQuantity" style="TEXT-ALIGN: right" runat="server" Width="50px"
														BorderStyle="Solid" BorderWidth="1px" BackColor="#EEEEE9" Height="20px" BorderColor="DimGray" Font-Size="9pt">0</asp:textbox></td>
												<td align="right" width="200">발주량
												</td>
												<td width="100"><asp:textbox id="txtFirstQuantity" style="TEXT-ALIGN: right" runat="server" Width="100px" BorderStyle="Solid"
														BorderWidth="1px" BackColor="#EEEEE9" Height="20px" BorderColor="DimGray" Font-Size="9pt">0</asp:textbox></td>
												<td align="right" width="70">단가
												</td>
												<td width="100"><asp:textbox id="txtApplyUnitCost" style="TEXT-ALIGN: right" runat="server" Width="100px" BorderStyle="Solid"
														BorderWidth="1px" BackColor="#EEEEE9" Height="20px" BorderColor="DimGray" Font-Size="9pt">0</asp:textbox></td>
												<td align="right" width="80">납기일자
												</td>
												<td><igsch:webdatechooser id="txtFirstDate" runat="server" Width="100px" BorderStyle="Solid" BackColor="#EEEEE9"
														Height="20px" BorderColor="DimGray" Font-Size="9pt" NullDateLabel=" " Text=" ">
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
													</igsch:webdatechooser></td>
											</tr>
										</table>
									</TD>
								</TR>
							</TABLE>
							<TABLE style="WIDTH: 800px; HEIGHT: 26px" cellSpacing="0" cellPadding="0" width="856">
								<TR>
									<TD style="WIDTH: 1px" align="right" colSpan="2" height="30" rowSpan="2"></TD>
									<TD style="WIDTH: 3px" align="right" colSpan="2" height="30" rowSpan="2"></TD>
									<TD style="WIDTH: 4px" align="right" colSpan="2" height="30" rowSpan="2"></TD>
									<TD style="WIDTH: 363px" align="left" colSpan="2" height="30" rowSpan="2"><INPUT id="rowIndex" style="BACKGROUND-COLOR: #eeeee9; WIDTH: 36px; HEIGHT: 21px" type="hidden"
											size="1" name="Hidden4" runat="server"><INPUT id="hdItemNum" style="BACKGROUND-COLOR: #eeeee9; WIDTH: 36px; HEIGHT: 21px" type="hidden"
											size="1" name="Hidden1" runat="server"><INPUT id="lblItemDrawNum" style="BACKGROUND-COLOR: #eeeee9; WIDTH: 36px; HEIGHT: 21px"
											type="hidden" size="1" name="Hidden4" runat="server"><INPUT id="hdBusinessRegistrationNum" style="BACKGROUND-COLOR: #eeeee9; WIDTH: 36px; HEIGHT: 21px"
											type="hidden" size="1" name="Hidden4" runat="server"><INPUT id="hdindex" style="BACKGROUND-COLOR: #eeeee9; WIDTH: 36px; HEIGHT: 21px" type="hidden"
											size="1" name="hdindex" runat="server"></TD>
									<TD style="WIDTH: 69px" align="right" colSpan="2" height="30" rowSpan="2"><asp:linkbutton id="LinkButton1" runat="server"></asp:linkbutton><asp:linkbutton id="Linkbutton2" runat="server"></asp:linkbutton></TD>
									<TD style="WIDTH: 18px" align="right" colSpan="2" height="30" rowSpan="2"></TD>
									<TD align="right" colSpan="2" height="30" rowSpan="2">
										<P><asp:button id="btnInit" runat="server" Width="65px" Height="20px" Font-Size="9pt" Text="초기화"></asp:button>&nbsp;<asp:button id="btnEdit" runat="server" Width="65px" Height="20px" Font-Size="9pt" Text="수   정"
												Enabled="False"></asp:button>&nbsp;<asp:button id="btnDelete" runat="server" Width="65px" Height="20px" Font-Size="9pt" Text="삭   제"
												Enabled="False"></asp:button>&nbsp;<asp:button id="btnOrder" runat="server" Width="65px" Height="20px" Font-Size="9pt" Text="발   주"></asp:button>&nbsp;</P>
									</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="20" height="380"></TD>
					<TD vAlign="top" align="center" width="100%">
						<FIELDSET style="BORDER-BOTTOM: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; HEIGHT: 320px; BORDER-TOP: dimgray 2px solid; BORDER-RIGHT: dimgray 2px solid"
							align="top" DESIGNTIMEDRAGDROP="95"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 
								입력결과 ]
							</LEGEND>
							<TABLE style="WIDTH: 100%; HEIGHT: 0.29%" cellSpacing="0" cellPadding="0">
								<TR>
									<TD vAlign="middle" align="center"><igtbl:ultrawebgrid id="uwgBuyingOrderAdd" runat="server" Width="100%" Height="350px">
											<DisplayLayout AutoGenerateColumns="False" AllowSortingDefault="Yes" RowHeightDefault="20px" Version="3.00"
												SelectTypeRowDefault="Extended" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti"
												BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="uwgBuyingOrderAdd"
												CellClickActionDefault="RowSelect">
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
												<FrameStyle Width="100%" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="350px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents AfterSelectChangeHandler="uwgBuyingOrderAdd_AfterSelectChangeHandler" DblClickHandler="uwgBuyingOrderAdd_DblClickHandler"></ClientSideEvents>
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
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="120px" HeaderClickAction="Select" BaseColumnName="ItemNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" HeaderClickAction="SortSingle"
															BaseColumnName="ItemDrawNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" HeaderClickAction="Select" BaseColumnName="ItemName">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" HeaderClickAction="Select" BaseColumnName="CompanyName">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="Select"
															BaseColumnName="BusinessRegistrationNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="1차납품요구량" Key="FirstDeliveryDemandQuantity" Hidden="True" Format="###,###,##0.00"
															HeaderClickAction="Select" BaseColumnName="FirstDeliveryDemandQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총발주량" Key="OrderQuantity" Format="###,###,##0.00" HeaderClickAction="Select"
															BaseColumnName="OrderQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="납기요구일" Key="FirstDeliveryDemandDate" Format="yyyy-MM-dd" HeaderClickAction="Select"
															BaseColumnName="FirstDeliveryDemandDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="현재고" Key="StoreQuantity" BaseColumnName="StoreQuantity">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="2차납품요구량" Key="SecondDeliveryDemandQuantity" Hidden="True" Format="###,###,##0.00"
															HeaderClickAction="Select" BaseColumnName="SecondDeliveryDemandQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="2차납품요구일" Key="SecondDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
															HeaderClickAction="Select" BaseColumnName="SecondDeliveryDemandDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="3차납품요구량" Key="ThirdDeliveryDemandQuantity" Hidden="True" Format="###,###,##0.00"
															HeaderClickAction="Select" BaseColumnName="ThirdDeliveryDemandQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="3차납품요구일" Key="ThirdDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
															HeaderClickAction="Select" BaseColumnName="ThirdDeliveryDemandDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="4차납품요구량" Key="FourthDeliveryDemandQuantity" Hidden="True" Format="###,###,##0.00"
															HeaderClickAction="Select" BaseColumnName="FourthDeliveryDemandQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="4차납품요구일" Key="FourthDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
															HeaderClickAction="Select" BaseColumnName="FourthDeliveryDemandDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="5차납품요구량" Key="FifthDeliveryDemandQuantity" Hidden="True" Format="###,###,##0.00"
															HeaderClickAction="Select" BaseColumnName="FifthDeliveryDemandQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="5차납품요구일" Key="FifthDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
															HeaderClickAction="Select" BaseColumnName="FifthDeliveryDemandDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Format="###,###,##0.00" HeaderClickAction="Select"
															BaseColumnName="ApplyUnitCost">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총금액" Key="TotalCost" Format="###,###,##0.00" HeaderClickAction="Select"
															BaseColumnName="TotalCost">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="발주비율" Key="OrderRate" Hidden="True" Format="###,###,##0.00" HeaderClickAction="Select"
															BaseColumnName="OrderRate">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="잔량" Key="RemainQuantity" Hidden="True" Format="###,###,##0.00" HeaderClickAction="Select"
															BaseColumnName="RemainQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="볼륨번호" Key="VolumNum" Hidden="True" HeaderClickAction="Select" BaseColumnName="VolumNum">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" Hidden="True" HeaderClickAction="Select"
															BaseColumnName="ProgressCondition">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" HeaderClickAction="Select" BaseColumnName="RegistrationPerson">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" HeaderClickAction="Select"
															BaseColumnName="RegistrationPersonID">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" HeaderClickAction="Select"
															BaseColumnName="RegistrationDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" Hidden="True" HeaderClickAction="Select" BaseColumnName="UpdatingPerson">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" HeaderClickAction="Select"
															BaseColumnName="UpdatingPersonID">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="Select"
															BaseColumnName="UpdatingDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="구매발주원장번호" Key="BuyingOrderHistoryIndex" Hidden="True" HeaderClickAction="Select"
															BaseColumnName="BuyingOrderHistoryIndex">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
													</Columns>
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
