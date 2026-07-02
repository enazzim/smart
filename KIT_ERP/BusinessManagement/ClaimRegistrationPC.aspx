<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Page language="c#" Codebehind="ClaimRegistrationPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BusinessManagement.ClaimRegistrationPC" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ClaimPC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--
		
		var GridName;
		
		function ResettxtBox()
		{
			ResetTextBox();
			ResetBox();
			var objChooser1 = igdrp_getComboById("wdcFromDate");
			var objChooser2 = igdrp_getComboById("wdcToDate");
			objChooser1.setValue(null);
			objChooser2.setValue(null);
			
		}
		
		
		function UltraWebGrid1_ColumnHeaderClickHandler(gridName, columnId, button)
		{
			GridName = gridName;
    			if(document.ClaimRegistrationPC.chkAll.value == "true")
    			{
					AllCheck();
				}
				else
				{
					AllUncheck();
				}
		}
		
		function AllCheck() // 전부 체크하기
		{
			var grid = igtbl_getGridById(GridName);
			
			for(var i=0; i<grid.Rows.length; i++)
			{
				var row = grid.Rows.getRow(i);
				row.getCellFromKey("chk").setValue(true);
			}
			document.ClaimRegistrationPC.chkAll.value = "false";
		}


		function AllUncheck() // 전부체크 해제하기
		{
			var grid = igtbl_getGridById(GridName);
			
			for(var i=0; i<grid.Rows.length; i++)
			{
				var row = grid.Rows.getRow(i);
				row.getCellFromKey("chk").setValue(false);
			}
			document.ClaimRegistrationPC.chkAll.value = "true";
		}
		function UltraWebGrid1_MouseOverHandler(gridName, id, button)
		{
		
			//Add code to handle your event here.
			if(button == 0)
			{ // Are we over a cell
				var cell = igtbl_getElementById(id);
				cell.style.cursor = 'hand';
				//var label = igtbl_getElementById("lb_RowIndex");
				var parts = id.split("_");                                                                                                                                                                                                                       
				document.ClaimRegistrationPC.lb_RowIndex.value = parts[1];
			}
		}
		
		// 수정창이 열리면서 값을 입력하는 함수
		function UltraWebGrid1_AfterRowTemplateOpenHandler(gridName, rowId)
		{
			var objGrid = igtbl_getActiveRow(gridName)//현재 선택되어진 객체의 행의 가져옴
			var row = igtbl_getRowById(rowId);
			
			
			//Add code to handle your event here.
			document.ClaimRegistrationPC.lb_RowSelectIndex.value = document.ClaimRegistrationPC.lb_RowIndex.value;
			
			
			//이전크레임금액
			document.ClaimRegistrationPC.lb_Cost.value = row.getCellFromKey("ClaimCost").getValue();//기존금액  
			
			document.ClaimRegistrationPC.igtbl_TextBox_0_7.value = MakeCommaSeparate(row.getCellFromKey("ClaimQuantity").getValue());
			document.ClaimRegistrationPC.igtbl_TextBox_0_8.value = MakeCommaSeparate(row.getCellFromKey("ClaimCost").getValue());			
			
			//클레임현상
			for(var i=0;i<document.ClaimRegistrationPC.UltraWebGrid1__ctl0_DropDownList1.options.length;i++)
			{
				if(document.ClaimRegistrationPC.UltraWebGrid1__ctl0_DropDownList1.options[i].innerText == row.getCellFromKey("ClaimStatusMeaning").getValue())
				{
					document.ClaimRegistrationPC.UltraWebGrid1__ctl0_DropDownList1.options[i].selected=true;
					break;
				}
				else
					document.ClaimRegistrationPC.UltraWebGrid1__ctl0_DropDownList1.options[0].selected=true;
			}
			
			//클레임원인
			for(var i=0;i<document.ClaimRegistrationPC.UltraWebGrid1__ctl0_DropDownList2.options.length;i++)
			{
				if(document.ClaimRegistrationPC.UltraWebGrid1__ctl0_DropDownList2.options[i].innerText == row.getCellFromKey("ClaimCauseMeaning").getValue())
				{
					document.ClaimRegistrationPC.UltraWebGrid1__ctl0_DropDownList2.options[i].selected=true;
					break;
				}
				else
					document.ClaimRegistrationPC.UltraWebGrid1__ctl0_DropDownList2.options[0].selected=true;
			}
			
			//수금일자, 결재일자1,2,3
			var date = igdrp_getComboById("UltraWebGrid1xxctl0xwdcDate");
			date.setValue(objGrid.getCellFromKey("ReceiptDate").getValue());
			
			document.ClaimRegistrationPC.hd_year.value = date.getValue().getFullYear();
			document.ClaimRegistrationPC.hd_month.value = date.getValue().getMonth()+1;
			
		}
		
		
		// 수정창이 닫히면서 값을 그리드에 뿌려주는 함수
		function UltraWebGrid1_AfterRowTemplateCloseHandler(gridName, rowId, bSaveChanges){
			if(event.srcElement.id == "igtbl_reOkBtn")
			{
				var frm = document.ClaimRegistrationPC;
				var row = igtbl_getRowById(rowId);
				
				//접수일
				var date = igdrp_getComboById("UltraWebGrid1xxctl0xwdcDate");
				row.getCellFromKey("ReceiptDate").setValue(date.getValue());
				
				//크레임현상
				row.getCellFromKey("ClaimStatusCode").setValue(frm.UltraWebGrid1__ctl0_DropDownList1.options[frm.UltraWebGrid1__ctl0_DropDownList1.selectedIndex].value);
				row.getCellFromKey("ClaimStatusMeaning").setValue(frm.UltraWebGrid1__ctl0_DropDownList1.options[frm.UltraWebGrid1__ctl0_DropDownList1.selectedIndex].innerText);
				
				//크레임원인
				row.getCellFromKey("ClaimCauseCode").setValue(frm.UltraWebGrid1__ctl0_DropDownList2.options[frm.UltraWebGrid1__ctl0_DropDownList2.selectedIndex].value);
				row.getCellFromKey("ClaimCauseMeaning").setValue(frm.UltraWebGrid1__ctl0_DropDownList2.options[frm.UltraWebGrid1__ctl0_DropDownList2.selectedIndex].innerText);
				
			}
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
					
					function OnKeyDown_Float(obj) {   
						if (event.ctrlKey || event.shiftKey || event.altKey)   {
							SetEventPass();
							return true;
						}
												
						// 합격수량 TextBox 객체의 value로 배열을 만든다.
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
//															Currency															//
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
	<body style="FONT-SIZE: x-small" MS_POSITIONING="GridLayout" bgColor="#f7f6f6">
		<form id="ClaimRegistrationPC" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" height="550"
				cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD vAlign="top" align="center" width="800"><FONT face="굴림">
							<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 800px; BORDER-BOTTOM: #696969 2px solid; HEIGHT: 48px"><LEGEND align="top">[검색조건]</LEGEND>
								<TABLE id="Table2" style="HEIGHT: 28px" cellSpacing="0" cellPadding="0" width="800">
									<TR>
										<TD width="185" colSpan="2" height="30">
											<uc1:CompanySearchControl id="CSC1" runat="server"></uc1:CompanySearchControl></TD>
										<TD width="615" colSpan="3" height="30">
											<uc1:ItemSearchControl id="ItemSearchControl1" runat="server"></uc1:ItemSearchControl></TD>
									</TR>
									<TR>
										<TD align="right" width="70" height="30"><FONT face="굴림"><FONT face="굴림">접수일&nbsp; </FONT>
											</FONT>
										</TD>
										<TD width="115" height="30">
											<igsch:webdatechooser id="wdcFromDate" runat="server" BorderStyle="Solid" BackColor="#EEEEE9" Height="20px"
												BorderColor="DimGray" Width="115px" Font-Size="9pt" Text=" " NullDateLabel=" ">
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
										<TD align="center" width="15" height="30"><FONT face="굴림">~</FONT></TD>
										<TD width="130" height="30">
											<igsch:webdatechooser id="wdcToDate" runat="server" BorderStyle="Solid" BackColor="#EEEEE9" Height="20px"
												BorderColor="DimGray" Width="115px" Font-Size="9pt" Text=" " NullDateLabel=" ">
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
										<TD align="right" width="470" height="30"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
												type="button" value="초기화" name="btnReset">&nbsp;
											<asp:button id="bt_Search" runat="server" Height="20px" Width="60px" Font-Size="10pt" Text="검색"></asp:button>&nbsp;
										</TD>
									</TR>
								</TABLE>
							</FIELDSET>
						</FONT>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 800px; BORDER-BOTTOM: #696969 2px solid; HEIGHT: 432px"><LEGEND align="top" style="FONT-SIZE: 9pt">[검색결과]</LEGEND>
							<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="800">
								<tr>
									<td style="HEIGHT: 389px" colSpan="2"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="386px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect"
												AllowUpdateDefault="RowTemplateOnly">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</AddNewBox>
												<Pager PageSize="16" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="Silver">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<RowSelectorStyleDefault Cursor="Hand" BackColor="#E0E5DE"></RowSelectorStyleDefault>
												<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
													BackColor="Silver" Height="386px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents ColumnHeaderClickHandler="UltraWebGrid1_ColumnHeaderClickHandler" AfterRowTemplateOpenHandler="UltraWebGrid1_AfterRowTemplateOpenHandler"
													AfterRowTemplateCloseHandler="UltraWebGrid1_AfterRowTemplateCloseHandler" MouseOverHandler="UltraWebGrid1_MouseOverHandler"></ClientSideEvents>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="전체" Key="chk" Width="30px" Type="CheckBox" HeaderClickAction="Select"
															BaseColumnName="chk" AllowUpdate="Yes">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ItemDrawNum">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" HeaderClickAction="SortMulti" BaseColumnName="CompanyName">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="BusinessRegistrationNum">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="접수일" Key="ReceiptDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="ReceiptDate">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="클레임수량" Key="ClaimQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
															BaseColumnName="ClaimQuantity">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="금액" Key="ClaimCost" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
															BaseColumnName="ClaimCost">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="클레임 사유" Key="ClaimStatusMeaning" HeaderClickAction="SortMulti" BaseColumnName="ClaimStatusMeaning">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="클레임 사유코드" Key="ClaimStatusCode" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ClaimStatusCode">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="클레임 원인" Key="ClaimCauseMeaning" HeaderClickAction="SortMulti" BaseColumnName="ClaimCauseMeaning">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="클레임 원인코드" Key="ClaimCauseCode" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ClaimCauseCode">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPerson">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationPersonID">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationDate">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" HeaderClickAction="SortMulti" BaseColumnName="UpdatingPerson">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingPersonID">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingDate">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="번호" Key="ClameHistoryIndex" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ClameHistoryIndex">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
													</Columns>
													<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
														<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
													</RowTemplateStyle>
													<RowEditTemplate>
														<P align="center">
															<TABLE id="Table3" style="HEIGHT: 80px" cellSpacing="0" cellPadding="0" width="800" border="0">
																<TR>
																	<TD align="right"><FONT style="FONT-SIZE: x-small" face="굴림">품목번호</FONT></TD>
																	<TD align="left"><FONT face="굴림"><INPUT id="igtbl_TextBox_0_1" style="WIDTH: 150px; BACKGROUND-COLOR: #eeeee9" readOnly
																				type="text" columnKey="ItemNum"></FONT></TD>
																	<TD align="right"><FONT style="FONT-SIZE: x-small" face="굴림">도면번호</FONT></TD>
																	<TD align="left"><FONT face="굴림"><INPUT id="igtbl_TextBox_0_2" style="WIDTH: 150px; BACKGROUND-COLOR: #eeeee9" readOnly
																				type="text" columnKey="ItemDrawNum"></FONT></TD>
																	<TD align="right"><FONT style="FONT-SIZE: x-small" face="굴림">품목명</FONT></TD>
																	<TD align="left"><INPUT id="igtbl_TextBox_0_3" style="WIDTH: 150px; BACKGROUND-COLOR: #eeeee9" readOnly
																			type="text" columnKey="ItemName"></TD>
																</TR>
																<TR>
																	<TD align="right"><FONT style="FONT-SIZE: x-small" face="굴림">거래처명</FONT></TD>
																	<TD align="left"><INPUT id="igtbl_TextBox_0_4" style="WIDTH: 150px; BACKGROUND-COLOR: #eeeee9" readOnly
																			type="text" columnKey="CompanyName"></TD>
																	<TD align="right"><FONT style="FONT-SIZE: x-small" face="굴림">클레임수량</FONT></TD>
																	<TD align="left"><FONT face="굴림"><INPUT id="igtbl_TextBox_0_7" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Cur(this);"
																				onkeyup="OnKeyUp_Currency(this);" style="WIDTH: 150px; BACKGROUND-COLOR: #eeeee9" onfocus="OnFocus_Obj(this);"
																				type="text" columnKey="ClaimQuantity"></FONT></TD>
																	<TD align="right"><FONT style="FONT-SIZE: x-small" face="굴림">금액</FONT></TD>
																	<TD align="left"><FONT face="굴림"><INPUT id="igtbl_TextBox_0_8" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																				onkeyup="OnKeyUp_Currency(this);" style="WIDTH: 150px; BACKGROUND-COLOR: #eeeee9" onfocus="OnFocus_Obj(this);"
																				type="text" columnKey="ClaimCost"></FONT></TD>
																</TR>
																<TR>
																	<TD align="right"><FONT style="FONT-SIZE: x-small" face="굴림">접수일</FONT></TD>
																	<TD align="left">
																		<igsch:WebDateChooser id="wdcDate" runat="server" NullDateLabel=" " Text="Null" Width="116px" BorderColor="DimGray"
																			Height="20px" BorderStyle="Solid">
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
																	<TD align="right"><FONT style="FONT-SIZE: x-small" face="굴림">클레임현상</FONT></TD>
																	<TD align="left">
																		<asp:DropDownList id=DropDownList1 runat="server" Width="142px" BackColor="#EEEEE9" columnKey="ClaimStatusMeaning" DataTextField="SmallClassificationName" DataValueField="SmallClassificationCode" DataSource="<%# ClaimStatus %>">
																		</asp:DropDownList></TD>
																	<TD align="right"><FONT style="FONT-SIZE: x-small; TEXT-ALIGN: right" face="굴림">클레임원인</FONT></TD>
																	<TD align="left">
																		<asp:DropDownList id=DropDownList2 runat="server" Width="151px" BackColor="#EEEEE9" columnKey="ClaimCauseMeaning" DataTextField="SmallClassificationName" DataValueField="SmallClassificationCode" DataSource="<%# ClaimCause %>">
																		</asp:DropDownList></TD>
																</TR>
															</TABLE>
														</P>
														<P align="center">&nbsp;</P>
														<P align="center"><FONT face="굴림"></FONT>&nbsp;</P>
														<P align="center"><FONT face="굴림"></FONT>&nbsp;</P>
														<P align="center"><FONT face="굴림"></FONT>&nbsp;</P>
														<P style="TEXT-ALIGN: center" align="center"><INPUT id="igtbl_reOkBtn" style="WIDTH: 50px" onclick="igtbl_gRowEditButtonClick(event);__doPostBack('lnk_Update','');"
																type="button" value="수 정">&nbsp; <INPUT id="igtbl_reCancelBtn" style="WIDTH: 50px" onclick="igtbl_gRowEditButtonClick(event);"
																type="button" value="취 소"></P>
													</RowEditTemplate>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></td>
								</tr>
								<TR>
									<TD style="WIDTH: 432px" height="30"><FONT face="굴림">&nbsp;</FONT><asp:button id="bt_Excel" runat="server" Width="60px" Height="20px" Text="Excel"></asp:button><INPUT id="lb_RowIndex" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1"
											runat="server"><INPUT id="lb_RowSelectIndex" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1"
											runat="server"><INPUT id="lb_Cost" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1" runat="server"><INPUT id="hd_year" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1" runat="server"><INPUT id="hd_month" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1" runat="server"></TD>
									<TD align="right" height="30"><asp:linkbutton id="lnk_Update" runat="server"></asp:linkbutton><INPUT id="chkAll" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" value="true">
										<asp:button id="bt_Delete" runat="server" Width="60px" Height="20px" Text="삭제"></asp:button>&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			<igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter></form>
	</body>
</HTML>
