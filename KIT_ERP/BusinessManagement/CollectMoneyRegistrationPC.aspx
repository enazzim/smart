<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="CollectMoneyRegistrationPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BusinessManagement.CollectMoneyRegistrationPC" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CollectMoneyRegistrationPC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--

		var GridName;
		
		
		function UltraWebGrid1_ColumnHeaderClickHandler(gridName, columnId, button)
		{
			GridName = gridName;
    			if(document.CollectMoneyRegistrationPC.chkAll.value == "true")
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
			document.CollectMoneyRegistrationPC.chkAll.value = "false";
		}


		function AllUncheck() // 전부체크 해제하기
		{
			var grid = igtbl_getGridById(GridName);
			
			for(var i=0; i<grid.Rows.length; i++)
			{
				var row = grid.Rows.getRow(i);
				row.getCellFromKey("chk").setValue(false);
			}
			document.CollectMoneyRegistrationPC.chkAll.value = "true";
		}
		
		function UltraWebGrid1_MouseOverHandler(gridName, id, button)
		{
			if(button == 0)
			{ 
				var cell = igtbl_getElementById(id);
				cell.style.cursor = 'hand';
				var parts = id.split("_");                                                                                                                                                                                                                       
				document.CollectMoneyRegistrationPC.lb_RowIndex.value = parts[1];
			}
		}
		
		//수정창이 열릴때
		function UltraWebGrid1_AfterRowTemplateOpenHandler(gridName, rowId)
		{
			var objGrid = igtbl_getActiveRow(gridName)//현재 선택되어진 객체의 행의 가져옴
			var row = igtbl_getRowById(rowId);
			//Add code to handle your event here.
			document.CollectMoneyRegistrationPC.lb_RowSelectIndex.value = document.CollectMoneyRegistrationPC.lb_RowIndex.value;
			
			//이전물품액
			document.CollectMoneyRegistrationPC.lb_Cost.value = document.getElementById(rowId).children[4].innerText;
			//이전수금액
			document.CollectMoneyRegistrationPC.lb_Tax.value = document.getElementById(rowId).children[5].innerText;
			
			document.CollectMoneyRegistrationPC.igtbl_TextBox_0_4.value = document.CollectMoneyRegistrationPC.lb_Cost.value;
			document.CollectMoneyRegistrationPC.igtbl_TextBox_0_5.value = document.CollectMoneyRegistrationPC.lb_Tax.value;			
			
			//결재방법
			for(var i=0;i<CollectMoneyRegistrationPC.UltraWebGrid1__ctl0_DropDownList1.options.length;i++)
			{
				if(CollectMoneyRegistrationPC.UltraWebGrid1__ctl0_DropDownList1.options[i].innerText == document.getElementById(rowId).children[7].innerText)
				{
					CollectMoneyRegistrationPC.UltraWebGrid1__ctl0_DropDownList1.options[i].selected=true;
					break;
				}
				else
					CollectMoneyRegistrationPC.UltraWebGrid1__ctl0_DropDownList1.options[0].selected=true;
			}
			//결재은행1
			for(var j=0;j<CollectMoneyRegistrationPC.UltraWebGrid1__ctl0_Dropdownlist2.options.length;j++)
			{
				if(CollectMoneyRegistrationPC.UltraWebGrid1__ctl0_Dropdownlist2.options[j].innerText == document.getElementById(rowId).children[11].innerText)
				{
					CollectMoneyRegistrationPC.UltraWebGrid1__ctl0_Dropdownlist2.options[j].selected=true;
					break;
				}
				else
					CollectMoneyRegistrationPC.UltraWebGrid1__ctl0_Dropdownlist2.options[0].selected=true;
				
			}
			//결재은행2
			for(var k=0;k<CollectMoneyRegistrationPC.UltraWebGrid1__ctl0_Dropdownlist3.options.length;k++)
			{
				if(CollectMoneyRegistrationPC.UltraWebGrid1__ctl0_Dropdownlist3.options[k].innerText == document.getElementById(rowId).children[15].innerText)
				{
					CollectMoneyRegistrationPC.UltraWebGrid1__ctl0_Dropdownlist3.options[k].selected=true;
					break;
				}
				else
					CollectMoneyRegistrationPC.UltraWebGrid1__ctl0_Dropdownlist3.options[0].selected=true;
				
			}
			//결재은행3
			for(var l=0;l<CollectMoneyRegistrationPC.UltraWebGrid1__ctl0_Dropdownlist4.options.length;l++)
			{
				if(CollectMoneyRegistrationPC.UltraWebGrid1__ctl0_Dropdownlist4.options[l].innerText == document.getElementById(rowId).children[19].innerText)
				{
					CollectMoneyRegistrationPC.UltraWebGrid1__ctl0_Dropdownlist4.options[l].selected=true;
					break;
				}
				else
					CollectMoneyRegistrationPC.UltraWebGrid1__ctl0_Dropdownlist4.options[0].selected=true;
				
			}
			
			//수금일자, 결재일자1,2,3
			var date = igdrp_getComboById("UltraWebGrid1xxctl0xWebdatechooser1");
			var date1 = igdrp_getComboById("UltraWebGrid1xxctl0xWebdatechooser2");
			var date2 = igdrp_getComboById("UltraWebGrid1xxctl0xWebdatechooser3");
			var date3 = igdrp_getComboById("UltraWebGrid1xxctl0xWebdatechooser4");
			
			date.setValue(objGrid.getCellFromKey("CollectMoneyDate").getValue());
			date1.setValue(objGrid.getCellFromKey("BillPaymentDate1").getValue());
			date2.setValue(objGrid.getCellFromKey("BillPaymentDate2").getValue());
			date3.setValue(objGrid.getCellFromKey("BillPaymentDate3").getValue());
			//물품액,부가세
			CollectMoneyRegistrationPC.lb_Cost.value = row.getCellFromKey("ItemPaymentCost").getValue();
			CollectMoneyRegistrationPC.lb_Tax.value = row.getCellFromKey("SupplementaryValueTaxPaymentCost").getValue();
			
			document.CollectMoneyRegistrationPC.hd_year.value = date.getValue().getFullYear();
			document.CollectMoneyRegistrationPC.hd_month.value = date.getValue().getMonth()+1;
			
		}
		
		//수정창이 닫히면서 들어가는 것
		function UltraWebGrid1_AfterRowTemplateCloseHandler(gridName, rowId, bSaveChanges)
		{
			if(event.srcElement.id == "igtbl_reOkBtn")
			{
				var frm = document.CollectMoneyRegistrationPC;
				var row = igtbl_getRowById(rowId);
				var date = igdrp_getComboById("UltraWebGrid1xxctl0xWebdatechooser1");
				var date1 = igdrp_getComboById("UltraWebGrid1xxctl0xWebdatechooser2");
				var date2 = igdrp_getComboById("UltraWebGrid1xxctl0xWebdatechooser3");
				var date3 = igdrp_getComboById("UltraWebGrid1xxctl0xWebdatechooser4");
				row.getCellFromKey("CollectMoneyDate").setValue(date.getValue());
				row.getCellFromKey("BillPaymentDate1").setValue(date1.getValue());
				row.getCellFromKey("BillPaymentDate2").setValue(date2.getValue());
				row.getCellFromKey("BillPaymentDate3").setValue(date3.getValue());
				
				//결재방법
				row.getCellFromKey("DecisionMethodCode").setValue(frm.UltraWebGrid1__ctl0_DropDownList1.options[frm.UltraWebGrid1__ctl0_DropDownList1.selectedIndex].value);
				row.getCellFromKey("DecisionMethod").setValue(frm.UltraWebGrid1__ctl0_DropDownList1.options[frm.UltraWebGrid1__ctl0_DropDownList1.selectedIndex].innerText);
				//결재은행1
				if(frm.UltraWebGrid1__ctl0_Dropdownlist2.options[frm.UltraWebGrid1__ctl0_Dropdownlist2.selectedIndex].value == "")
				{
					row.getCellFromKey("BankCode1").setValue(frm.UltraWebGrid1__ctl0_Dropdownlist2.options[0].value);
					row.getCellFromKey("BankName1").setValue(frm.UltraWebGrid1__ctl0_Dropdownlist2.options[0].value);
				}
				else
				{
					row.getCellFromKey("BankCode1").setValue(frm.UltraWebGrid1__ctl0_Dropdownlist2.options[frm.UltraWebGrid1__ctl0_Dropdownlist2.selectedIndex].value);
					row.getCellFromKey("BankName1").setValue(frm.UltraWebGrid1__ctl0_Dropdownlist2.options[frm.UltraWebGrid1__ctl0_Dropdownlist2.selectedIndex].innerText);
				}
				//결재은행2
				if(frm.UltraWebGrid1__ctl0_Dropdownlist3.options[frm.UltraWebGrid1__ctl0_Dropdownlist3.selectedIndex].value == "")
				{
					row.getCellFromKey("BankCode2").setValue(frm.UltraWebGrid1__ctl0_Dropdownlist3.options[0].value);
					row.getCellFromKey("BankName2").setValue(frm.UltraWebGrid1__ctl0_Dropdownlist3.options[0].value);
				}
				else
				{
					row.getCellFromKey("BankCode2").setValue(frm.UltraWebGrid1__ctl0_Dropdownlist3.options[frm.UltraWebGrid1__ctl0_Dropdownlist3.selectedIndex].value);
					row.getCellFromKey("BankName2").setValue(frm.UltraWebGrid1__ctl0_Dropdownlist3.options[frm.UltraWebGrid1__ctl0_Dropdownlist3.selectedIndex].innerText);
				}
				//결재은행3
				if(frm.UltraWebGrid1__ctl0_Dropdownlist4.options[frm.UltraWebGrid1__ctl0_Dropdownlist4.selectedIndex].value == "")
				{
					row.getCellFromKey("BankCode3").setValue(frm.UltraWebGrid1__ctl0_Dropdownlist4.options[0].value);
					row.getCellFromKey("BankName3").setValue(frm.UltraWebGrid1__ctl0_Dropdownlist4.options[0].value);
				}
				else
				{
					row.getCellFromKey("BankCode3").setValue(frm.UltraWebGrid1__ctl0_Dropdownlist4.options[frm.UltraWebGrid1__ctl0_Dropdownlist4.selectedIndex].value);
					row.getCellFromKey("BankName3").setValue(frm.UltraWebGrid1__ctl0_Dropdownlist4.options[frm.UltraWebGrid1__ctl0_Dropdownlist4.selectedIndex].innerText);
				}
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
			var Tax = Number(RemoveComma(document.CollectMoneyRegistrationPC.igtbl_TextBox_0_4.value))/10;
			var Tax1 = (Math.round(Tax,1));
			document.CollectMoneyRegistrationPC.igtbl_TextBox_0_5.value = MakeCommaSeparate((Math.round((Tax1 * 10000)) / 10000));
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
	<body bgColor="#f7f6f6" MS_POSITIONING="GridLayout">
		<form id="CollectMoneyRegistrationPC" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" height="550"
				cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD style="PADDING-TOP: 10px" vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD align="right" width="70" height="30"><FONT face="굴림">거래처명&nbsp;</FONT></TD>
									<TD style="WIDTH: 109px" height="30">
										<igcmbo:webcombo id="wc_Company" runat="server" Editable="True" ForeColor="Black" BorderStyle="Solid"
											BorderWidth="1px" SelBackColor="10, 36, 106" BackColor="#EEEEE9" SelectedIndex="-1" Height="20px"
											BorderColor="DimGray" SelForeColor="White" Version="3.00" DropImageXP1="/ig_common/WebGrid3/ig_cmboDown1.bmp"
											DropImageXP2="/ig_common/WebGrid3/ig_cmboDown2.bmp" Width="100px" Font-Size="9pt">
											<Columns>
												<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" Width="250px" BaseColumnName="CompanyName">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="대표자명" Key="PresidentName" Width="0px" Hidden="True" BaseColumnName="PresidentName">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Width="300px" BaseColumnName="BusinessRegistrationNum">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="번호" Key="CompanyInfoIndex" Width="0px" Hidden="True" BaseColumnName="CompanyInfoIndex">
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
										</igcmbo:webcombo></TD>
									<TD align="right" width="70" height="30"><FONT face="굴림">결재방법&nbsp;</FONT></TD>
									<TD style="WIDTH: 87px" height="30">
										<asp:dropdownlist id="dl_DecisionMethod" runat="server" BackColor="#EEEEE9" Height="20px" Width="100px"
											Font-Size="9pt"></asp:dropdownlist></TD>
									<TD align="right" width="70" height="30"><FONT face="굴림">수금일자&nbsp;</FONT></TD>
									<TD style="WIDTH: 104px" height="30">
										<igsch:webdatechooser id="wdc_FromDate" runat="server" BackColor="#EEEEE9" Height="20px" Width="100px"
											Font-Size="9pt" Text=" " NullDateLabel=" ">
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
									<TD style="WIDTH: 108px" height="30">
										<igsch:webdatechooser id="wdc_ToDate" runat="server" BackColor="#EEEEE9" Height="20px" Width="100px" Font-Size="9pt"
											Text=" " NullDateLabel=" ">
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
									<TD align="right" height="30">
										<asp:button id="bt_Clear" runat="server" Height="20px" Width="60px" Font-Size="10pt" Text="초기화"></asp:button>&nbsp;
										<asp:button id="bt_Search" runat="server" Height="20px" Width="60px" Font-Size="10pt" Text="검색"></asp:button>&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" width="100%">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top">[검색결과]</LEGEND>
							<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="100%">
								<tr>
									<td colSpan="2" width="100%"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="100%" Height="426px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect"
												AllowUpdateDefault="RowTemplateOnly">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</AddNewBox>
												<Pager PageSize="18" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</Pager>
												<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<RowSelectorStyleDefault Cursor="Hand" BackColor="Navy"></RowSelectorStyleDefault>
												<FrameStyle Width="100%" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="426px"></FrameStyle>
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
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" HeaderClickAction="SortMulti" BaseColumnName="CompanyName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="BusinessRegistrationNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수금일" Key="CollectMoneyDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="CollectMoneyDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수금액(물품대)" Key="ItemPaymentCost" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
															BaseColumnName="ItemPaymentCost">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수금액(부가세)" Key="SupplementaryValueTaxPaymentCost" Format="\ ###,###,##0"
															HeaderClickAction="SortMulti" BaseColumnName="SupplementaryValueTaxPaymentCost">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="결재방법코드" Key="DecisionMethodCode" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="DecisionMethodCode">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="결재방법" Key="DecisionMethod" HeaderClickAction="SortMulti" BaseColumnName="DecisionMethod">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="어음번호1" Key="BillNum1" HeaderClickAction="SortMulti" BaseColumnName="BillNum1">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="어음결재일자1" Key="BillPaymentDate1" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="BillPaymentDate1">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="결재은행코드1" Key="BankCode1" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="BankCode1">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="금융기관1" Key="BankName1" HeaderClickAction="SortMulti" BaseColumnName="BankName1">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="어음번호2" Key="BillNum2" HeaderClickAction="SortMulti" BaseColumnName="BillNum2">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="어음결재일자2" Key="BillPaymentDate2" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="BillPaymentDate2">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="결재은행코드2" Key="BankCode2" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="BankCode2">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="금융기관2" Key="BankName2" HeaderClickAction="SortMulti" BaseColumnName="BankName2">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="어음번호3" Key="BillNum3" HeaderClickAction="SortMulti" BaseColumnName="BillNum3">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="어음결재일자3" Key="BillPaymentDate3" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="BillPaymentDate3">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="결재은행코드3" Key="BankCode3" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="BankCode3">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="금융기관3" Key="BankName3" HeaderClickAction="SortMulti" BaseColumnName="BankName3">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPerson">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationPersonID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" HeaderClickAction="SortMulti" BaseColumnName="UpdatingPerson">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingPersonID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" HeaderClickAction="SortMulti" BaseColumnName="UpdatingDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="번호" Key="CollectMoneyHistoryIndex" HeaderClickAction="SortMulti" BaseColumnName="CollectMoneyHistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
													</Columns>
													<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
														<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
													</RowTemplateStyle>
													<RowEditTemplate>
														<TABLE style="WIDTH: 800px">
															<TR>
																<TD align="right"><FONT face="굴림">거래처명</FONT></TD>
																<TD><INPUT id="igtbl_TextBox_0_1" style="WIDTH: 90px" disabled type="text" columnKey="CompanyName"></TD>
																<TD><FONT face="굴림">수금일</FONT></TD>
																<TD><FONT face="굴림">
																		<igsch:webdatechooser id="Webdatechooser1" runat="server" Width="90px" NullDateLabel=" " Text="Null" columnKey="CollectMoneyDate">
																			<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
																				DayNameFormat="FirstLetter">
																				<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
																				<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																				<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																				<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
																			</CALENDARLAYOUT>
																			<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
																			<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
																			<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
																		</igsch:webdatechooser></FONT></TD>
																<TD><FONT face="굴림">물품대</FONT></TD>
																<TD><INPUT id="igtbl_TextBox_0_4" onkeydown="OnKeyDown_Float(this);" onkeyup="Process();OnKeyUp_Currency(this);"
																		onblur="OnBlur_Cur(this);" style="WIDTH: 100px; HEIGHT: 22px; TEXT-ALIGN: right" onfocus="OnFocus_Obj(this);"
																		type="text" size="12" columnKey="ItemPaymentCost"></TD>
																<TD><FONT face="굴림">부가세</FONT></TD>
																<TD><INPUT id="igtbl_TextBox_0_5" onkeydown="OnKeyDown_Float(this);" onkeyup="OnKeyUp_Currency(this);"
																		onblur="OnBlur_Cur(this);" style="WIDTH: 100px; HEIGHT: 22px; TEXT-ALIGN: right" onfocus="OnFocus_Obj(this);"
																		type="text" size="11" columnKey="SupplementaryValueTaxPaymentCost"></TD>
																<TD><FONT face="굴림">결재방법</FONT></TD>
																<TD><FONT face="굴림">
																		<asp:DropDownList id=DropDownList1 runat="server" Width="90px" columnKey="DecisionMethod" DataValueField="SmallClassificationCode" DataTextField="SmallClassificationName" DataSource="<%# Method %>">
																		</asp:DropDownList></FONT></TD>
															</TR>
															<TR>
																<TD><FONT face="굴림">어음번호1</FONT></TD>
																<TD><INPUT id="igtbl_TextBox_0_8" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																		style="WIDTH: 90px" onfocus="OnFocus_Obj(this);" type="text" columnKey="BillNum1"></TD>
																<TD><FONT face="굴림">결재일1</FONT></TD>
																<TD>
																	<igsch:webdatechooser id="Webdatechooser2" runat="server" Width="90px" NullDateLabel=" " Text="Null" columnKey="BillPaymentDate1">
																		<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
																			DayNameFormat="FirstLetter">
																			<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
																			<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																			<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																			<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
																		</CALENDARLAYOUT>
																		<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
																		<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
																		<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
																	</igsch:webdatechooser></TD>
																<TD><FONT face="굴림">금융기관2</FONT></TD>
																<TD>
																	<asp:DropDownList id=Dropdownlist2 runat="server" Width="90px" columnKey="BankName1" DataValueField="SmallClassificationCode" DataTextField="SmallClassificationName" DataSource="<%# Bank %>">
																	</asp:DropDownList></TD>
																<TD><FONT face="굴림">어음번호2</FONT></TD>
																<TD><INPUT id="igtbl_TextBox_0_12" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																		style="WIDTH: 90px" onfocus="OnFocus_Obj(this);" type="text" columnKey="BillNum2"></TD>
																<TD><FONT face="굴림">결재일2</FONT></TD>
																<TD>
																	<igsch:webdatechooser id="Webdatechooser3" runat="server" Width="90px" NullDateLabel=" " Text="Null" columnKey="BillPaymentDate2">
																		<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
																			DayNameFormat="FirstLetter">
																			<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
																			<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																			<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																			<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
																		</CALENDARLAYOUT>
																		<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
																		<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
																		<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
																	</igsch:webdatechooser></TD>
															</TR>
															<TR>
																<TD><FONT face="굴림">금융기관2</FONT></TD>
																<TD>
																	<asp:DropDownList id=Dropdownlist3 runat="server" Width="90px" columnKey="BankName2" DataValueField="SmallClassificationCode" DataTextField="SmallClassificationName" DataSource="<%# Bank %>">
																	</asp:DropDownList></TD>
																<TD><FONT face="굴림">어음번호3</FONT></TD>
																<TD><INPUT id="igtbl_TextBox_0_16" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																		style="WIDTH: 90px" onfocus="OnFocus_Obj(this);" type="text" columnKey="BillNum3"></TD>
																<TD><FONT face="굴림">결재일3</FONT></TD>
																<TD>
																	<igsch:webdatechooser id="Webdatechooser4" runat="server" Width="90px" NullDateLabel=" " Text="Null" columnKey="BillPaymentDate3">
																		<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
																			DayNameFormat="FirstLetter">
																			<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
																			<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																			<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																			<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
																		</CALENDARLAYOUT>
																		<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
																		<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
																		<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
																	</igsch:webdatechooser></TD>
																<TD><FONT face="굴림">금융기관3</FONT></TD>
																<TD>
																	<asp:DropDownList id=Dropdownlist4 runat="server" Width="90px" columnKey="BankName3" DataValueField="SmallClassificationCode" DataTextField="SmallClassificationName" DataSource="<%# Bank %>">
																	</asp:DropDownList></TD>
																<TD><FONT face="굴림"></FONT></TD>
																<TD><FONT face="굴림"></FONT></TD>
															</TR>
														</TABLE>
														<P align="center"><FONT face="굴림"></FONT>&nbsp;</P>
														<P align="center"><FONT face="굴림"></FONT>&nbsp;</P>
														<P align="center">&nbsp;</P>
														<P align="center">&nbsp; <INPUT id="igtbl_reOkBtn" style="WIDTH: 50px" onclick="igtbl_gRowEditButtonClick(event);__doPostBack('lnk_Update','');"
																type="button" value="수정">&nbsp; <INPUT id="igtbl_reCancelBtn" style="WIDTH: 50px" onclick="igtbl_gRowEditButtonClick(event);"
																type="button" value="취소"></P>
													</RowEditTemplate>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></td>
								</tr>
								<TR>
									<TD height="30"><asp:button id="bt_Excel" runat="server" Width="60px" Height="20px" Text="Excel"></asp:button><INPUT id="lb_RowIndex" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1"
											runat="server"><INPUT id="lb_RowSelectIndex" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1"
											runat="server"><INPUT id="lb_Cost" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1" runat="server"
											size="3"><INPUT id="lb_Tax" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1" runat="server"><INPUT id="hd_year" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1" runat="server"><INPUT id="hd_month" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" size="3" name="Hidden1"
											runat="server"></TD>
									<TD align="right" height="30"><asp:linkbutton id="lnk_Update" runat="server"></asp:linkbutton><INPUT id="chkAll" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" value="true">
										<asp:button id="bt_Delete" runat="server" Width="60px" Height="20px" Text="삭제"></asp:button></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			<igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter></form>
	</body>
</HTML>
