<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Page language="c#" Codebehind="ExecutionPlanInfo.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.ExecutionPlanInfo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ExecutionPlanInfo</title>
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
			var combo = igdrp_getComboById("wdcDate");//날짜
			
			combo.setValue(row.getCellFromKey("PlanDate").getValue());//날짜
			
			document.ExecutionPlanInfo.tb_PlanQuantity.value = row.getCellFromKey("PlanQuantity").getValue()	//발주비율
			document.ExecutionPlanInfo.tb_SaleUnitCost.value = row.getCellFromKey("SaleUnitCost").getValue();//기준단가
			document.ExecutionPlanInfo.tb_PlanTotalCost.value = row.getCellFromKey("PlanTotalCost").getValue();	//할인단가
			document.ExecutionPlanInfo.lb_Index.value = row.getCellFromKey("ExecutionPlanInfoIndex").getValue();
			
			document.ExecutionPlanInfo.bt_Update.disabled = false;
		    document.ExecutionPlanInfo.bt_Delete.disabled = false;
		    document.ExecutionPlanInfo.bt_Registration.disabled = true;
		}
		
		function Ultrawebgrid2_AfterSelectChangeHandler(gridName, id){
			//Add code to handle your event here.
			var row = igtbl_getRowById(id);
			ExecutionPlanInfo.lb_ReferenceTable.value = row.getCellFromKey("CommunityDataTableID").getValue();
			ExecutionPlanInfo.lb_ReferenceIndex.value = row.getCellFromKey("CDTIndex").getValue();
		}
		
		
		
		// 수량 입력시 onkeyup 이벤트 때 발생.
		function Process()	
		{
			var total = Number(ExecutionPlanInfo.tb_PlanQuantity.value) * Number(ExecutionPlanInfo.tb_SaleUnitCost.value);
			ExecutionPlanInfo.tb_PlanTotalCost.value = total;
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
	<body MS_POSITIONING="GridLayout" bgColor="#f7f6f6">
		<form id="ExecutionPlanInfo" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" cellSpacing="0"
				cellPadding="0" width="800" border="0" height="550">
				<TR>
					<TD>
						<table id="table1" style="WIDTH: 800px; HEIGHT: 20px" width="800" cellSpacing="0" cellPadding="0">
							<TR>
								<td align="right" width="600" colspan="3"></td>
								<td align="right" colSpan="5" vAlign="bottom" width="200"><asp:linkbutton id="LinkButton1" runat="server" Font-Size="10pt">실행계획보기</asp:linkbutton></td>
							</TR>
						</table>
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 480px"
							align="left"><legend>[ 입 력 ]</legend>
							<table id="a" style="HEIGHT: 8px" width="800" cellSpacing="0" cellPadding="0">
								<TR>
									<TD align="left" width="600" colSpan="6" height="20" style="WIDTH: 0px; HEIGHT: 0px">
										<uc1:ItemSearchControl id="ItemSearchControl1" runat="server"></uc1:ItemSearchControl></TD>
									<TD align="right" width="70" height="20"><FONT face="굴림">자산분류&nbsp;</FONT></TD>
									<TD align="left" width="130" height="20"><asp:textbox id="tb_PropertyClassification" runat="server" Width="100px" Height="20px" BorderStyle="Solid"
											ReadOnly="True" Font-Size="9pt" BackColor="#EEEEE9" BorderWidth="1px" BorderColor="DimGray"></asp:textbox></TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="20" style="WIDTH: 0px; HEIGHT: 0px"><FONT face="굴림"><FONT face="굴림"><FONT face="굴림">단위&nbsp;</FONT></FONT></FONT></TD>
									<TD align="left" width="130" height="20"><asp:textbox id="tb_Unit" runat="server" Width="100px" Height="20px" BorderStyle="Solid" ReadOnly="True"
											Font-Size="9pt" BackColor="#EEEEE9" BorderWidth="1px" BorderColor="DimGray"></asp:textbox></TD>
									<TD align="right" width="70" height="20"><FONT face="굴림">규격&nbsp;</FONT></TD>
									<TD align="left" width="130" height="20"><asp:textbox id="tb_Standard" runat="server" Width="100px" Height="20px" BorderStyle="Solid"
											ReadOnly="True" Font-Size="9pt" BackColor="#EEEEE9" BorderWidth="1px" BorderColor="DimGray"></asp:textbox></TD>
									<TD align="right" width="70" height="20">
										<asp:LinkButton id="LinkButton2" runat="server" Visible="False">LinkButton</asp:LinkButton></TD>
									<TD align="left" width="130" height="20"></TD>
									<TD align="right" width="70" height="20"></TD>
									<TD align="left" width="130" height="20">
										<asp:button id="bt_ExecutionPlanView" runat="server" Width="100px" Height="20px" Text="실행계획보기"></asp:button></TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="5"><FONT face="굴림"><FONT face="굴림"></FONT></FONT></TD>
									<TD align="left" width="130" height="5"></TD>
									<TD align="right" width="70" height="5"></TD>
									<TD align="left" width="130" height="5">
										<INPUT id="ItemIndex" style="WIDTH: 30px" type="hidden" size="1" name="ItemIndex" runat="server"></TD>
									<TD align="right" width="70" height="5">
										<asp:Label id="lb_ItemIndex" runat="server" Visible="False">Label</asp:Label></TD>
									<TD align="left" width="130" height="5"></TD>
									<TD align="right" width="70" height="5"></TD>
									<TD align="left" width="130" height="5"></TD>
								</TR>
								<TR>
									<TD align="right" width="800" colSpan="8" height="100"><FONT face="굴림"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="280px">
												<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
													RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
													HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
													RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect">
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
														BorderStyle="Solid" BackColor="Silver" Height="280px"></FrameStyle>
													<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</FooterStyleDefault>
													<ClientSideEvents DblClickHandler="UltraWebGrid1_DblClickHandler"></ClientSideEvents>
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
															<igtbl:UltraGridColumn HeaderText="날 짜" Key="PlanDate" Format="yyyy-MM-dd" BaseColumnName="PlanDate">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="계획수량" Key="PlanQuantity" Format="###,###,###.##" BaseColumnName="PlanQuantity">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="판매단가" Key="SaleUnitCost" Format="\ ###,###,##0" BaseColumnName="SaleUnitCost">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="계획총금액" Key="PlanTotalCost" Format="\ ###,###,##0" BaseColumnName="PlanTotalCost">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
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
															<igtbl:UltraGridColumn HeaderText="번호" Key="ExecutionPlanInfoIndex" BaseColumnName="ExecutionPlanInfoIndex">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
														</Columns>
													</igtbl:UltraGridBand>
												</Bands>
											</igtbl:ultrawebgrid></FONT></TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="5"></TD>
									<TD align="left" width="130" height="5"></TD>
									<TD align="right" width="70" height="5"></TD>
									<TD align="left" width="130" height="5"></TD>
									<TD align="right" width="70" height="5"><FONT face="굴림"></FONT></TD>
									<TD align="left" width="130" height="5"></TD>
									<TD align="right" width="70" height="5"></TD>
									<TD align="left" width="130" height="5"></TD>
								</TR>
								<tr>
									<td style="WIDTH: 0px; HEIGHT: 0px" align="right" width="70"><FONT style="COLOR: red" face="굴림">*날 
											짜&nbsp;</FONT></td>
									<td style="HEIGHT: 13px" align="left" width="130"><FONT face="굴림"><igsch:webdatechooser id="wdcDate" runat="server" Width="100px" Height="20px" Text="Null" NullDateLabel=" "
												MaxDate="2079-06-06" Editable="False" BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray">
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
											</igsch:webdatechooser></FONT></td>
									<td style="HEIGHT: 13px" align="right" width="70"><FONT style="COLOR: red" face="굴림">계획수량&nbsp;</FONT></td>
									<td style="HEIGHT: 13px" align="left" width="130"><FONT face="굴림">
											<asp:textbox id="tb_PlanQuantity" style="TEXT-ALIGN: right" runat="server" Height="20px" Width="100px"
												Font-Size="9pt" BorderStyle="Solid" BackColor="#EEEEE9" BorderWidth="1px" BorderColor="DimGray">0</asp:textbox></FONT></td>
									<td style="HEIGHT: 13px" align="right" width="70"><FONT style="COLOR: red" face="굴림">판매단가&nbsp;</FONT></td>
									<td style="HEIGHT: 13px" align="left" width="130">
										<asp:textbox id="tb_SaleUnitCost" style="TEXT-ALIGN: right" runat="server" Height="20px" Width="88px"
											Font-Size="9pt" BorderStyle="Solid" BackColor="#EEEEE9" BorderWidth="1px" BorderColor="DimGray">0</asp:textbox><FONT face="굴림">원</FONT></td>
									<td style="HEIGHT: 13px" align="right" width="70"><FONT style="COLOR: red" face="굴림">계획총금액&nbsp;</FONT></td>
									<td style="HEIGHT: 13px" align="left" width="130">
										<asp:textbox id="tb_PlanTotalCost" style="TEXT-ALIGN: right" runat="server" Height="20px" Width="88px"
											Font-Size="9pt" BorderStyle="Solid" BackColor="#EEEEE9" BorderWidth="1px" BorderColor="DimGray">0</asp:textbox><FONT face="굴림">원</FONT></td>
								</tr>
								<tr>
									<td align="right" width="70"><asp:button id="bt_Reference" runat="server" Width="60px" Height="20px" Text="참고 보기"></asp:button>&nbsp;</td>
									<td align="left" width="130">&nbsp; <INPUT id="lb_Index" style="WIDTH: 61px; HEIGHT: 22px" type="hidden" size="4" value="0"
											runat="server" NAME="lb_Index"></td>
									<td align="right" width="70"><INPUT id="lb_ReferenceIndex" style="WIDTH: 30px" type="hidden" name="lb_ReferenceIndex"
											runat="server"></FONT><FONT face="굴림"><INPUT id="lb_ReferenceTable" style="WIDTH: 30px" type="hidden" name="lb_ReferenceTable"
												runat="server"></FONT></td>
									<td align="left" width="130"><FONT face="굴림"></FONT></td>
									<td align="right" width="70"></td>
									<td align="right" width="330" colSpan="3"><asp:button id="bt_Clear" runat="server" Width="60px" Height="20px" Text="초기화"></asp:button>
										<asp:button id="bt_Update" runat="server" Height="20px" Width="60px" Text="수  정" Enabled="False"></asp:button>
										<asp:button id="bt_Delete" runat="server" Height="20px" Width="60px" Text="삭  제" Enabled="False"></asp:button>
										<asp:button id="bt_Registration" runat="server" Height="20px" Width="60px" Text="등  록"></asp:button>&nbsp;</td>
								</tr>
								<TR>
									<TD align="right" width="70" height="5"></TD>
									<TD align="left" width="130" height="5"></TD>
									<TD align="right" width="70" height="5"></TD>
									<TD align="left" width="130" height="5"></TD>
									<TD align="right" width="70" height="5"></TD>
									<TD align="right" width="330" colSpan="3" height="5"></TD>
								</TR>
								<tr>
									<td height="10" colspan="8" width="800">
										<igtbl:ultrawebgrid id="Ultrawebgrid2" runat="server" Width="800px" Height="128px">
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
													BorderStyle="Solid" BackColor="Silver" Height="128px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents AfterSelectChangeHandler="Ultrawebgrid2_AfterSelectChangeHandler"></ClientSideEvents>
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
														<igtbl:UltraGridColumn HeaderText="실행계획테이블" Key="StandardinfoTableID" Width="150px" BaseColumnName="StandardinfoTableID">
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
										</igtbl:ultrawebgrid>
									</td>
								</tr>
							</table>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
