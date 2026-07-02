<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="ClaimRegistration.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BusinessManagement.ClaimRegistration" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ClaimRegistration</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--

		function OK(text)
		{
			if(confirm(text + "하시겠습니까?"))
				return true;
			else
				return false;
		}
		
				
		function UltraWebGrid1_DblClickHandler(gridName, cellId){
			//Add code to handle your event here.
			var row = igtbl_getRowById(cellId);
			//var combo = igcmbo_getComboById("wcItemNum");//품목명
			var combo2 = igdrp_getComboById("wdcDate");//등록일자
			var combo3 = igcmbo_getComboById("wcCompany");//거래처명
			
			document.ClaimRegistration.ItemSearchControl1_txtItemNum.value = row.getCellFromKey("ItemNum").getValue();
			document.ClaimRegistration.ItemSearchControl1_txtItemName.value = row.getCellFromKey("ItemName").getValue();			
			document.ClaimRegistration.ItemSearchControl1_txtItemDrawNum.value = row.getCellFromKey("ItemDrawNum").getValue();
			combo2.setValue(row.getCellFromKey("ReceiptDate").getValue());//등록일
			combo3.setDataValue(row.getCellFromKey("BusinessRegistrationNum").getValue());//사업자등록번호
			combo3.setDisplayValue(row.getCellFromKey("CompanyName").getValue());//거래처명
			
			var year = combo2.getValue().getFullYear();
			var mon = combo2.getValue().getMonth()+1;
			var day = combo2.getValue().getDate();
			
			document.ClaimRegistration.lb_ReceiptDate.value = year+'-'+mon+'-'+day;
			
			document.ClaimRegistration.lbItemNum.value = row.getCellFromKey("ItemDrawNum").getValue()	//품목번호
			document.ClaimRegistration.lbItemNum.value = row.getCellFromKey("ItemNum").getValue();		//품목번호
			document.ClaimRegistration.tb_Cost.value = MakeCommaSeparate(row.getCellFromKey("ClaimCost").getValue());		//금액
			document.ClaimRegistration.tbQuantity.value = MakeCommaSeparate(row.getCellFromKey("ClaimQuantity").getValue());//수량
			document.ClaimRegistration.lb_OldCost.value = row.getCellFromKey("ClaimCost").getValue();//기존금액
			
			
			//클레임현상
			for(var i=0;i<document.ClaimRegistration.ddlClaimStatus.options.length;i++)
			{
				if(document.ClaimRegistration.ddlClaimStatus.options[i].innerText == row.getCellFromKey("ClaimStatusMeaning").getValue())
				{
					document.ClaimRegistration.ddlClaimStatus.options[i].selected=true;
					break;
				}
				else
					document.ClaimRegistration.ddlClaimStatus.options[0].selected=true;
			}
			
			//클레임원인
			for(var i=0;i<document.ClaimRegistration.ddlClaimCause.options.length;i++)
			{
				if(document.ClaimRegistration.ddlClaimCause.options[i].innerText == row.getCellFromKey("ClaimCauseMeaning").getValue())
				{
					document.ClaimRegistration.ddlClaimCause.options[i].selected=true;
					break;
				}
				else
					document.ClaimRegistration.ddlClaimCause.options[0].selected=true;
			}
			
		    document.ClaimRegistration.lb_Index.value = row.getCellFromKey("ClameHistoryIndex").getValue();//원장인덱스
		    
		    
		      
		    document.ClaimRegistration.bt_Update.disabled = false;
		    document.ClaimRegistration.bt_Delete.disabled = false;
		    document.ClaimRegistration.bt_Register.disabled = true;
		}
		
		
		function currentdate() 
		{ 
			var date = new Date();
			var year  = date.getFullYear();
			var month = date.getMonth() + 1; 
			var day   = date.getDate();
			
			if (("" + month).length == 1)
			{ 
				month = "0" + month; 
			}
			if (("" + day).length   == 1) 
			{
				day   = "0" + day;   
			}
		 
			return ("" + year +"-"+ month + "-"+ day)
		}
		
		// 초기화 버튼
		function ResettxtBox(){
			ResetTextBox();
			var frm = document.ClaimRegistration;
			
			//var objCombo1 = igcmbo_getComboById("wcItemNum");
			var objCombo2 = igcmbo_getComboById("wcCompany");
				
			//objCombo1.setDisplayValue("");
			objCombo2.setDisplayValue("");
			
			
						
			frm.tb_Cost.value="0";
			frm.lb_OldCost.value="0";
			frm.lb_Index.value="0";
			frm.wdcDate_input.value= currentdate();
			frm.tbQuantity.value = "0";
			frm.lb_Index.value = "0";
			frm.lb_OldCost.value = "0";
			frm.lbItemDrawNum.value = "";
			frm.lbItemNum.value = "";			
			frm.ddlClaimCause.options[0].selected=true;
			frm.ddlClaimStatus.options[0].selected=true;
		}
		
		function wcItemNum_AfterSelectChange(webComboId){
			//Add code to handle your event here.
			var obj = igcmbo_getComboById(webComboId);
			if(obj.getGrid().Rows.getRow(obj.getSelectedIndex()).getCellFromKey("ItemDrawNum").getValue() != null)
				document.ClaimRegistration.lbItemDrawNum.value = obj.getGrid().Rows.getRow(obj.getSelectedIndex()).getCellFromKey("ItemDrawNum").getValue();
			if(obj.getGrid().Rows.getRow(obj.getSelectedIndex()).getCellFromKey("ItemNum").getValue() != null)
				document.ClaimRegistration.lbItemNum.value = obj.getGrid().Rows.getRow(obj.getSelectedIndex()).getCellFromKey("ItemNum").getValue();
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
	<body bgColor="#f7f6f6" MS_POSITIONING="GridLayout" XMLNS:igtbl="http://schemas.infragistics.com/ASPNET/WebControls/UltraWebGrid">
		<form id="ClaimRegistration" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; WIDTH: 800px; POSITION: absolute; TOP: 10px"
				height="550" cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD style="PADDING-TOP: 10px" vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid">
							<LEGEND align="top">
								[입력]</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD align="right" width="100" height="25"><FONT face="굴림">거래처명&nbsp;</FONT></TD>
									<TD width="110" height="25">
										<igcmbo:webcombo id="wcCompany" runat="server" DropImageXP1="/ig_common/WebGrid3/ig_cmboDown1.bmp"
											DropImageXP2="/ig_common/WebGrid3/ig_cmboDown2.bmp" Font-Size="9pt" ForeColor="Black" Version="3.00"
											SelForeColor="White" BorderColor="DimGray" Height="20px" SelectedIndex="-1" Width="100px" BackColor="#EEEEE9"
											SelBackColor="10, 36, 106" BorderWidth="1px" BorderStyle="Solid">
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
										</igcmbo:webcombo></TD>
									<TD width="595" colSpan="6" height="25"><FONT face="굴림"></FONT>
										<uc1:ItemSearchControl id="ItemSearchControl1" runat="server"></uc1:ItemSearchControl></TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" width="100" height="25"><FONT face="굴림">접수일 </FONT>
									</TD>
									<TD vAlign="bottom" align="left" width="110" height="25">
										<igsch:webdatechooser id="wdcDate" runat="server" Font-Size="9pt" Height="20px" Width="100px" BackColor="#EEEEE9"
											MaxDate="2076-06-06" Text=" " NullDateLabel=" " BorderStyle="Solid" BorderColor="DimGray">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												MaxDate="2076-06-06" ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD align="right" width="70" height="25"><FONT face="굴림">수량&nbsp; </FONT>
									</TD>
									<TD width="130" height="25">
										<asp:textbox id="tbQuantity" style="TEXT-ALIGN: right" runat="server" Font-Size="9pt" Height="20px"
											Width="100px" BackColor="#EEEEE9" BorderStyle="Inset">0</asp:textbox></TD>
									<TD align="right" width="70" height="25"><FONT face="굴림">금액&nbsp; </FONT>
									</TD>
									<TD width="330" height="25">
										<asp:textbox id="tb_Cost" style="TEXT-ALIGN: right" runat="server" Height="20px" Width="100px"
											BackColor="#EEEEE9" BorderStyle="Inset">0</asp:textbox><INPUT id="lbItemNum" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1" runat="server"><INPUT id="lbItemDrawNum" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1"
											runat="server"><INPUT id="lb_OldCost" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" value="0" name="Hidden1"
											runat="server"><INPUT id="lb_ReceiptDate" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1"
											runat="server"></TD>
									<TD colSpan="2" height="25"></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 9pt" align="right" width="100" height="25"><FONT style="FONT-SIZE: 9pt" face="굴림">클레임현상&nbsp;
										</FONT>
									</TD>
									<TD width="110" height="25">
										<asp:dropdownlist id="ddlClaimStatus" runat="server" Font-Size="9pt" Height="20px" Width="100px" BackColor="#EEEEE9"></asp:dropdownlist></TD>
									<TD style="FONT-SIZE: 9pt" align="right" width="70" height="25"><FONT style="FONT-SIZE: 9pt" face="굴림">클레임원인&nbsp;
										</FONT>
									</TD>
									<TD width="130" height="25">
										<asp:dropdownlist id="ddlClaimCause" runat="server" Font-Size="9pt" Height="20px" Width="105px" BackColor="#EEEEE9"></asp:dropdownlist></TD>
									<TD align="right" width="400" colSpan="4" height="25"><FONT face="굴림"><INPUT id="lb_Index" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" value="0" name="Hidden1"
												runat="server">&nbsp;
											<asp:button id="Button1" runat="server" Width="60px" Height="20px" Text="초기화"></asp:button>
											<asp:button id="bt_Delete" runat="server" Height="20px" Width="60px" Text="삭  제" Enabled="False"></asp:button>
											<asp:button id="bt_Update" runat="server" Height="20px" Width="60px" Text="수  정" Enabled="False"></asp:button>
											<asp:button id="bt_Register" runat="server" Height="20px" Width="60px" Text="등  록"></asp:button>&nbsp;&nbsp;
										</FONT>
									</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 9pt" align="right" width="100" height="8"></TD>
									<TD width="110" height="8"></TD>
									<TD style="FONT-SIZE: 9pt" align="right" width="70" height="8"></TD>
									<TD width="130" height="8"></TD>
									<TD align="right" width="400" colSpan="4" height="8"></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" width="800"><FONT face="굴림"></FONT><br>
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top">
								<P>[입력결과]</P>
							</LEGEND>
							<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="410px" Width="800px">
								<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
									RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
									HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
									RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect">
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
									<RowSelectorStyleDefault Cursor="Hand" BackColor="#C0FFFF"></RowSelectorStyleDefault>
									<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderColor="DimGray"
										BorderStyle="Solid" BackColor="Silver" Height="410px"></FrameStyle>
									<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</FooterStyleDefault>
									<ClientSideEvents DblClickHandler="UltraWebGrid1_DblClickHandler"></ClientSideEvents>
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
											<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" HeaderClickAction="SortMulti" BaseColumnName="ItemDrawNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" HeaderClickAction="SortMulti" BaseColumnName="CompanyName">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="BusinessRegistrationNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="접수일" Key="ReceiptDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="ReceiptDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="클레임수량" Key="ClaimQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
												BaseColumnName="ClaimQuantity">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="금액" Key="ClaimCost" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
												BaseColumnName="ClaimCost">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="클레임 사유" Key="ClaimStatusMeaning" HeaderClickAction="SortMulti" BaseColumnName="ClaimStatusMeaning">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="클레임 사유코드" Key="ClaimStatusCode" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="ClaimStatusCode">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="클레임 원인" Key="ClaimCauseMeaning" HeaderClickAction="SortMulti" BaseColumnName="ClaimCauseMeaning">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="클레임 원인코드" Key="ClaimCauseCode" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="ClaimCauseCode">
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
											<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="UpdatingDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="번호" Key="ClameHistoryIndex" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="ClameHistoryIndex">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
										</Columns>
									</igtbl:UltraGridBand>
								</Bands>
							</igtbl:ultrawebgrid></FIELDSET>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
