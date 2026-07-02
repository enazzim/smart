<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v1.1, Version=1.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp1" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="SubItemDeletePC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.SubItemDeletePC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SubItemDeletePC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript"><!--
		function OK(text)
		{
			if(confirm(text + "하시겠습니까?"))
				return true;
			else
				return false;
		}
		function AllCheck() // 전부 체크하기
		{
			var grid = igtbl_getGridById(GridName);
			
			for(var i=0; i<grid.Rows.length; i++)
			{
				var row = grid.Rows.getRow(i);
				row.getCellFromKey("chk").setValue(true);
			}
			document.SubItemDeletePC.chkAll.value = "false";
		}
		function AllUncheck() // 전부체크 해제하기
		{
			var grid = igtbl_getGridById(GridName);
			
			for(var i=0; i<grid.Rows.length; i++)
			{
				var row = grid.Rows.getRow(i);
				row.getCellFromKey("chk").setValue(false);
			}
			document.SubItemDeletePC.chkAll.value = "true";
		}
		
		function UltraWebGrid1_ColumnHeaderClickHandler(gridName, columnId, button){
			//Add code to handle your event here.
			GridName = gridName;
    			if(document.SubItemDeletePC.chkAll.value == "true")
    			{
					AllCheck();
				}
				else
				{
					AllUncheck();
				}
		}
		
		
		function UltraWebGrid1_AfterRowTemplateOpenHandler(gridName, rowId, templateId)
		{
			
			var row = igtbl_getRowById(rowId);
			var date = igedit_getById("UltraWebGrid1__ctl0_wdeStartDate", "igtxtUltraWebGrid1__ctl0_wdeStartDate");
			date.setValue(row.getCellFromKey("ThrowingDate").getValue());
			
			SubItemDeletePC.hdPreThorwQuantity.value = row.getCellFromKey("ThrowingQuantity").getValue();
			//작업장
			for(var i=0;i<SubItemDeletePC.UltraWebGrid1__ctl0_Dropdownlist1.options.length;i++)
			{
				if(SubItemDeletePC.UltraWebGrid1__ctl0_Dropdownlist1.options[i].value == row.getCellFromKey("WCName").getValue())
				{
					SubItemDeletePC.UltraWebGrid1__ctl0_Dropdownlist1.options[i].selected=true;
					break;
				}
				else
					SubItemDeletePC.UltraWebGrid1__ctl0_Dropdownlist1.options[0].selected=true;
			}			
			
		}
		
		//수정창(RowEditTemplate)의 WebDateChooser의 의해 선택된 날짜를 Grid의 해당 Row의 해당 Column의 값을 변경
		function UltraWebGrid1T_AfterRowTemplateCloseHandler(gridName, rowId, bSaveChanges)
		{
			if(event.srcElement.id == "igtbl_reOkBtn")
			{
				var row = igtbl_getRowById(rowId);
				
				var date = igedit_getById("UltraWebGrid1__ctl0_wdeStartDate", "igtxtUltraWebGrid1__ctl0_wdeStartDate");
				row.getCellFromKey("ThrowingDate").setValue(date.getValue());
				
				
				//작업장
				for(var i=0;i<SubItemDeletePC.UltraWebGrid1__ctl0_Dropdownlist1.options.length;i++)
				{
					if(SubItemDeletePC.UltraWebGrid1__ctl0_dl_Dropdownlist1.options[i].value == row.getCellFromKey("WCName").getValue())
					{
						SubItemDeletePC.UltraWebGrid1__ctl0_dl_Dropdownlist1.options[i].selected=true;
						break;
					}
					else
						SubItemDeletePC.UltraWebGrid1__ctl0_dl_Dropdownlist1.options[0].selected=true;			
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
		function UltraWebGrid1_AfterRowTemplateOpenHandler(gridName, rowId){
			//Add code to handle your event here.
		}
		
		
--></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="SubItemDeletePC" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" height="530"
				cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD vAlign="top" align="center" width="100%" height="70">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD align="left" width="800" colSpan="7" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림"></FONT><FONT face="굴림"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></FONT></TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30"><FONT face="굴림">작업장&nbsp; </FONT>
									</TD>
									<TD width="130" height="30"><FONT face="굴림"><asp:dropdownlist id="dl_WCName" runat="server" Width="115px" Height="20px" BackColor="#EEEEE9">
												<asp:ListItem Value="0">- 선 택 -</asp:ListItem>
												<asp:ListItem Value="4">원자재창고</asp:ListItem>
												<asp:ListItem Value="5">생산창고</asp:ListItem>
												<asp:ListItem Value="1">영업1창고</asp:ListItem>
												<asp:ListItem Value="2">영업2창고</asp:ListItem>
												<asp:ListItem Value="3">영업3창고</asp:ListItem>
											</asp:dropdownlist></FONT></TD>
									<TD align="right" width="100" height="30"><FONT face="굴림">투입일자&nbsp; </FONT>
									</TD>
									<TD align="right" width="100" height="30"><FONT face="굴림"><igsch:webdatechooser id="wdcMinDate" runat="server" Width="100px" Height="18px" BackColor="#EEEEE9" Text="날짜 선택"
												BorderStyle="Solid" Font-Size="10pt" BorderColor="DimGray" NullDateLabel=" ">
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
											</igsch:webdatechooser></FONT></TD>
									<TD align="center" width="15" height="30"><FONT face="굴림">~</FONT></TD>
									<TD align="right" width="100" height="30"><FONT face="굴림"><igsch:webdatechooser id="wdcMaxDate" runat="server" Width="100px" Height="18px" BackColor="#EEEEE9" Text="날짜 선택"
												BorderStyle="Solid" Font-Size="10pt" BorderColor="DimGray" NullDateLabel=" ">
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
											</igsch:webdatechooser></FONT></TD>
									<TD align="right" width="290" height="30"><asp:button id="bt_Clear" runat="server" Width="60px" Height="20px" Text="초기화"></asp:button><FONT face="굴림">&nbsp;</FONT>
										<asp:button id="bt_Search" runat="server" Width="60px" Height="20px" Text="검  색"></asp:button><FONT face="굴림">&nbsp;&nbsp;</FONT></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" height="10"></TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top">[검색결과]</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr>
									<td align="center" colSpan="8"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="407px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" RowHeightDefault="20px" Version="3.00"
												SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" BorderCollapseDefault="Separate"
												AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed"
												CellClickActionDefault="RowSelect" AllowUpdateDefault="RowTemplateOnly">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</AddNewBox>
												<Pager PageSize="17" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</Pager>
												<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<RowSelectorStyleDefault BackColor="Navy"></RowSelectorStyleDefault>
												<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="407px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents ColumnHeaderClickHandler="UltraWebGrid1_ColumnHeaderClickHandler" AfterRowTemplateOpenHandler="UltraWebGrid1_AfterRowTemplateOpenHandler"
													AfterRowTemplateCloseHandler="UltraWebGrid1_AfterRowTemplateOpenHandler"></ClientSideEvents>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" BackColor="DodgerBlue"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="선택" Key="chk" EditorControlID="" Width="30px" Type="CheckBox" Format=""
															BaseColumnName="chk" NullText="" AllowUpdate="Yes" FooterText="">
															<CellStyle HorizontalAlign="Center"></CellStyle>
															<HeaderStyle Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품 목" Key="ItemNum" EditorControlID="" Width="200px" Format="" BaseColumnName="ItemNum"
															NullText="" FooterText="">
															<CellStyle HorizontalAlign="Left"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" EditorControlID="" Hidden="True" Format="" BaseColumnName="ItemDrawNum"
															NullText="" FooterText=""></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" EditorControlID="" Hidden="True" Format="" BaseColumnName="ItemName"
															NullText="" FooterText=""></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정순서" Key="ProcessSequenceNum" EditorControlID="" Width="30px" Hidden="True"
															Format="" BaseColumnName="ProcessSequenceNum" NullText="" FooterText="">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정코드" Key="ProcessCode" EditorControlID="" Hidden="True" Format="" BaseColumnName="ProcessCode"
															NullText="" FooterText=""></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정명" Key="ProcessName" EditorControlID="" Width="100px" Format="" BaseColumnName="ProcessName"
															NullText="" FooterText="">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업장명" Key="WCName" EditorControlID="" Width="100px" Format="" BaseColumnName="WCName"
															NullText="" FooterText="">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="창고번호" Key="StoreNum" Hidden="True" BaseColumnName="StoreNum"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="투입량" Key="ThrowingQuanity" EditorControlID="" Width="100px" Format="###,###,###.##"
															BaseColumnName="ThrowingQuanity" NullText="" FooterText="">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="투입일자" Key="ThrowingDate" EditorControlID="" Width="100px" Format="yyyy-MM-dd"
															BaseColumnName="ThrowingDate" NullText="" FooterText="">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" EditorControlID="" Width="100px" Format=""
															BaseColumnName="RegistrationPerson" NullText="" FooterText=""></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" EditorControlID="" Hidden="True" Format=""
															BaseColumnName="RegistrationPersonID" NullText="" FooterText=""></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" EditorControlID="" Width="100px" Format="yyyy-MM-dd"
															BaseColumnName="RegistrationDate" NullText="" FooterText="">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" EditorControlID="" Width="100px" Format=""
															BaseColumnName="UpdatingPerson" NullText="" FooterText=""></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" EditorControlID="" Hidden="True" Format=""
															BaseColumnName="UpdatingPersonID" NullText="" FooterText=""></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" EditorControlID="" Width="100px" Format="yyyy-MM-dd"
															BaseColumnName="UpdatingDate" NullText="" FooterText="">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="번호" Key="SubItemThrowingIndex" EditorControlID="" Hidden="True" Format="#########"
															BaseColumnName="SubItemThrowingIndex" NullText="" FooterText=""></igtbl:UltraGridColumn>
													</Columns>
													<RowTemplateStyle BorderColor="Silver" BorderStyle="Ridge">
														<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
													</RowTemplateStyle>
													<RowEditTemplate>
														<TABLE id="Edit" height="90" cellSpacing="0" cellPadding="0" width="600" align="center"
															border="0">
															<TR height="20">
																<TD align="right" width="100" bgColor="whitesmoke" height="20">품 목&nbsp;</TD>
																<TD align="left" width="300" colSpan="3" height="20"><INPUT id="igtbl_TextBox_0_1" style="WIDTH: 300px" readOnly type="text" columnKey="ItemNum">
																</TD>
																<TD align="right" width="100" bgColor="whitesmoke" height="20">공정명&nbsp;</TD>
																<TD align="left" width="100" height="20"><INPUT id="igtbl_TextBox_0_6" style="WIDTH: 100px" readOnly type="text" columnKey="ProcessName">
																</TD>
															</TR>
															<TR>
																<TD align="right" width="100" bgColor="whitesmoke" height="20">작업장명&nbsp;</TD>
																<TD align="left" width="100" height="20">
																	<asp:DropDownList id=Dropdownlist1 runat="server" Width="100px" Font-Size="9pt" DataValueField="WCName" DataTextField="WCName" DataSource="<%# WCName %>" columnkey="WCName">
																	</asp:DropDownList></TD>
																<TD align="right" width="100" bgColor="whitesmoke" height="20">투입량&nbsp;</TD>
																<TD align="left" width="100" height="20"><INPUT id="igtbl_TextBox_0_8" style="WIDTH: 100px; TEXT-ALIGN: right" onkeydown="OnKeyDown_Float(this);"
																		onblur="OnBlur_Float(this);" type="text" columnKey="ThrowingQuanity">
																</TD>
																<TD align="right" width="100" bgColor="whitesmoke" height="20">투입일자&nbsp;</TD>
																<TD align="left" width="100" height="20">
																	<igtxt:WebDateTimeEdit id="wdeStartDate" runat="server" Width="100px" Height="20px" BorderStyle="Solid"
																		BorderColor="DimGray" columnKey="ThrowingDate" BorderWidth="1px" HorizontalAlign="Center" DisplayModeFormat="d"
																		UseBrowserDefaults="False">
																		<ButtonsAppearance>
																			<ButtonPressedStyle BorderStyle="Inset">
																				<BorderDetails ColorBottom="White" ColorRight="White"></BorderDetails>
																			</ButtonPressedStyle>
																			<ButtonStyle BorderWidth="2px" BorderColor="White" BorderStyle="Outset" BackColor="Control">
																				<BorderDetails StyleBottom="Ridge" ColorBottom="0, 160, 160, 160" ColorRight="0, 160, 160, 160"
																					StyleRight="Ridge"></BorderDetails>
																			</ButtonStyle>
																		</ButtonsAppearance>
																		<SpinButtons Display="OnRight"></SpinButtons>
																		<BorderDetails ColorTop="0, 160, 160, 160" StyleTop="Ridge" StyleLeft="Ridge" ColorLeft="0, 160, 160, 160"></BorderDetails>
																	</igtxt:WebDateTimeEdit></TD>
															</TR>
															<TR height="20">
																<TD align="center" width="600" colSpan="6" height="20"><INPUT id="igtbl_reOkBtn" style="WIDTH: 50px" onclick="igtbl_gRowEditButtonClick(event);__doPostBack('LinkButton1','')"
																		type="button" value="수 정">&nbsp; <INPUT id="igtbl_reCancelBtn" style="WIDTH: 50px" onclick="igtbl_gRowEditButtonClick(event);"
																		type="button" value="취 소">
																</TD>
															</TR>
														</TABLE>
													</RowEditTemplate>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></td>
								</tr>
								<TR>
									<TD align="left" width="800" height="30"><asp:button id="btnExcel" runat="server" Width="60px" Height="20px" Text="Excel" Font-Size="10pt"
											CommandName="Excel"></asp:button>
										<igtblexp1:UltraWebGridExcelExporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp1:UltraWebGridExcelExporter></TD>
									<TD align="right" width="800" colSpan="7" height="30"><FONT face="굴림"><INPUT id="hdPreThorwQuantity" style="WIDTH: 51px; HEIGHT: 22px" type="hidden" size="3"
												runat="server"><asp:linkbutton id="LinkButton1" runat="server" Visible="False">LinkButton</asp:linkbutton><INPUT id="chkAll" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" value="true"
												name="Hidden1" runat="server">&nbsp; </FONT><FONT face="굴림">
											<asp:button id="bt_Delete" runat="server" Width="60px" Height="20px" Text="삭제" Font-Size="10pt"></asp:button>&nbsp;
										</FONT>
									</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
