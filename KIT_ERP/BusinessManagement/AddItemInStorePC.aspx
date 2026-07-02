<%@ Page language="c#" Codebehind="AddItemInStorePC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BusinessManagement.AddItemInStorePC" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddItemInStorePC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript"><!--
		function ResettxtBox()
		{
			ResetTextBox();
			var objChooser1 = igdrp_getComboById("wdcFromDate");
			var objChooser2 = igdrp_getComboById("wdcToDate");
			objChooser1.setValue(null);
			objChooser2.setValue(null);
		}
		
		function UltraWebGrid1_ColumnHeaderClickHandler(gridName, columnId, button)
		{
			GridName = gridName;
    			if(document.AddItemInStorePC.chkAll.value == "true")
    			{
					AllCheck();
				}
				else
				{
					AllUncheck();
				}
		}
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
			document.AddItemInStorePC.chkAll.value = "false";
		}


		function AllUncheck() // 전부체크 해제하기
		{
			var grid = igtbl_getGridById(GridName);
			
			for(var i=0; i<grid.Rows.length; i++)
			{
				var row = grid.Rows.getRow(i);
				row.getCellFromKey("chk").setValue(false);
			}
			document.AddItemInStorePC.chkAll.value = "true";
		}
		
		function UltraWebGrid1_AfterRowTemplateOpenHandler(gridName, rowId){
			//Add code to handle your event here.
			var row = igtbl_getRowById(rowId);//InOutStorehouseDistinction
			
			document.AddItemInStorePC.lb_Index.value = document.AddItemInStorePC.lb_RowIndex.value;//그리드 인덱스
			document.AddItemInStorePC.lb_Quantity.value = row.getCellFromKey("InstoreQuantity").getValue();//document.getElementById(rowId).children[10].innerText;//기존수량입력
					
			
			//입출고 사유
			for(var i=0;i<document.AddItemInStorePC.UltraWebGrid1__ctl0_dl_Reason.options.length;i++)
			{
				if(document.AddItemInStorePC.UltraWebGrid1__ctl0_dl_Reason.options[i].innerText == row.getCellFromKey("ReasonName").getValue())
				{
					document.AddItemInStorePC.UltraWebGrid1__ctl0_dl_Reason.options[i].selected=true
				}
			}
			
			
			
			document.AddItemInStorePC.igtbl_TextBox_0_1.value = row.getCellFromKey("ItemNum").getValue();//document.getElementById(rowId).children[1].innerText;
						document.AddItemInStorePC.igtbl_TextBox_0_3.value = row.getCellFromKey("ItemName").getValue();//document.getElementById(rowId).children[3].innerText;
			document.AddItemInStorePC.igtbl_TextBox_0_5.value = row.getCellFromKey("ProcessSequenceNum").getValue();//document.getElementById(rowId).children[3].innerText;
			document.AddItemInStorePC.igtbl_TextBox_0_6.value = row.getCellFromKey("ProcessName").getValue();//document.getElementById(rowId).children[3].innerText;
			document.AddItemInStorePC.igtbl_TextBox_0_7.value = row.getCellFromKey("InstoreQuantity").getValue();//document.getElementById(rowId).children[10].innerText;
			
			//입출고일자
			var date1 = igdrp_getComboById("UltraWebGrid1xxctl0xWebDateChooser2");
			if(row.getCellFromKey("InstoreDate").getValue() !=null)
			{
					date1.setValue(row.getCellFromKey("InstoreDate").getValue());
			}
			document.AddItemInStorePC.hdMon.value = date1.getValue().getUTCMonth()+1;
			document.AddItemInStorePC.hdYear.value = date1.getValue().getFullYear();
		}
		
		function Update()
		{
			document.AddItemInStorePC.lb_ReasonCode.value = document.AddItemInStorePC.UltraWebGrid1__ctl0_dl_Reason.value;
			document.AddItemInStorePC.lb_Reason.value = document.AddItemInStorePC.UltraWebGrid1__ctl0_dl_Reason.options[document.AddItemInStorePC.UltraWebGrid1__ctl0_dl_Reason.selectedIndex].innerText;
			
			__doPostBack('lnk_Update','');
			
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
		function UltraWebGrid1_MouseOverHandler(gridName, id, button){
			//Add code to handle your event here.  
			if(button == 0) 
			{ 
				// Are we over a cell  var cell = igtbl_getElementById(id);
				//cell.style.cursor = 'hand';  
				//var label = igtbl_getElementById("lb_RowIndex");  
				var parts = id.split("_");  
				document.AddItemInStorePC.lb_RowIndex.value = parts[1];  
			}
		}
		function UltraWebGrid1_AfterRowTemplateCloseHandler(gridName, rowId, bSaveChanges){
			
			
			
			// 수정창이 닫히면서 값을 그리드에 뿌려주는 함수
			if(event.srcElement.id == "igtbl_reOkBtn")
			{
				var frm = document.AddItemInStorePC;
				var row = igtbl_getRowById(rowId);
				var date = igdrp_getComboById("UltraWebGrid1xxctl0xWebDateChooser2");
				
				
				//입출고사유
				row.getCellFromKey("ReasonCode").setValue(frm.UltraWebGrid1__ctl0_dl_Reason.options[frm.UltraWebGrid1__ctl0_dl_Reason.selectedIndex].value);
				row.getCellFromKey("ReasonName").setValue(frm.UltraWebGrid1__ctl0_dl_Reason.options[frm.UltraWebGrid1__ctl0_dl_Reason.selectedIndex].innerText);
				
				
				//입출고일자
				row.getCellFromKey("InstoreDate").setValue(date.getValue());
			}
		}
		
				
--></script>
	</HEAD>
	<body bgColor="#f7f6f6">
		<form id="AddItemInStorePC" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" height="530"
				cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD vAlign="top" align="center" width="100%">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD align="left" width="800" colSpan="5" height="30"><FONT face="굴림"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></FONT></TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30">입고일&nbsp;</FONT>&nbsp;
									</TD>
									<TD width="115" height="30"><igsch:webdatechooser id="wdcFromDate" runat="server" BackColor="#EEEEE9" Height="20px" Width="115px"
											Text=" " BorderStyle="Solid" Font-Size="9pt" BorderColor="DimGray" NullDateLabel=" ">
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
									<td align="center" width="15"><FONT face="굴림">~</FONT></td>
									<td align="left" width="130"><igsch:webdatechooser id="wdcToDate" runat="server" BackColor="#EEEEE9" Height="20px" Width="115px" Text=" "
											BorderStyle="Solid" Font-Size="9pt" BorderColor="DimGray" NullDateLabel=" ">
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
									<TD align="right" width="470" height="30"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
											type="button" value="초기화" name="btnReset">&nbsp;&nbsp;<FONT face="굴림">&nbsp;</FONT>
										<asp:button id="btSearch" runat="server" Height="20px" Width="60px" Text="검  색"></asp:button><FONT face="굴림">&nbsp;&nbsp;</FONT></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" height="10"><FONT face="굴림"></FONT></TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"
							DESIGNTIMEDRAGDROP="1525"><LEGEND align="top">[검색결과]</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" border="0">
								<tr>
									<td align="center" width="100%" colSpan="8"><DISPLAYLAYOUT CellClickActionDefault="RowSelect" TableLayout="Fixed" Name="UltraWebGrid1" RowSelectorsDefault="No"
											AllowColSizingDefault="Free" BorderCollapseDefault="Separate" HeaderClickActionDefault="SortMulti" AllowColumnMovingDefault="OnServer"
											SelectTypeRowDefault="Single" Version="3.00" RowHeightDefault="20px" AllowSortingDefault="OnClient" AutoGenerateColumns="False" StationaryMargins="Header"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="427px" Width="100%">
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
													<Pager PageSize="18" StyleMode="ComboBox" AllowPaging="True">
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

														</Style>
													</Pager>
													<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</HeaderStyleDefault>
													<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
													<FrameStyle Width="100%" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
														BackColor="Silver" Height="427px"></FrameStyle>
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
															<igtbl:UltraGridColumn HeaderText="선택" Key="chk" Width="30px" Type="CheckBox" HeaderClickAction="Select"
																BaseColumnName="chk" AllowUpdate="Yes">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" BaseColumnName="ItemNum">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" BaseColumnName="ItemDrawNum">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" BaseColumnName="ItemName">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="공정순서" Key="ProcessSequenceNum" Width="65px" Hidden="True" BaseColumnName="ProcessSequenceNum">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="공정코드" Key="ProcessCode" Hidden="True" BaseColumnName="ProcessCode">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="공정명" Key="ProcessName" Width="80px" Hidden="True" BaseColumnName="ProcessName">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="입고수량" Key="InstoreQuantity" Width="80px" Format="###,###,###.##" BaseColumnName="InstoreQuantity">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																	<Padding Right="4px"></Padding>
																</CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="입고사유코드" Key="ReasonCode" Hidden="True" BaseColumnName="ReasonCode"></igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="입고일자" Key="InstoreDate" Format="yyyy-MM-dd" BaseColumnName="InstoreDate"></igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="입고사유" Key="ReasonName" BaseColumnName="ReasonName"></igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" BaseColumnName="RegistrationPerson">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID"></igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" BaseColumnName="UpdatingPerson">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID"></igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="번호" Key="AddItemInStoreIndex" Hidden="True" BaseColumnName="AddItemInStoreIndex">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="창고" Key="StoreName" Hidden="True" BaseColumnName="StoreName"></igtbl:UltraGridColumn>
														</Columns>
														<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
															<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
														</RowTemplateStyle>
														<RowEditTemplate>
															<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="780" border="0">
																<TR>
																	<TD align="right"><FONT face="굴림">품목번호&nbsp; </FONT>
																	</TD>
																	<TD><FONT face="굴림"></FONT><INPUT id="igtbl_TextBox_0_1" style="WIDTH: 100px" readOnly type="text" columnKey="ItemNum"></TD>
																	<TD align="right"><FONT face="굴림">품목명&nbsp; </FONT>
																	</TD>
																	<TD><FONT face="굴림"><INPUT id="igtbl_TextBox_0_3" style="WIDTH: 100px" readOnly type="text" columnKey="ItemName"></FONT></TD>
																	<TD align="right"><FONT face="굴림">순서&nbsp; </FONT>
																	</TD>
																	<TD align="left"><FONT face="굴림"><INPUT id="igtbl_TextBox_0_5" style="WIDTH: 100px" readOnly type="text" columnKey="ProcessSequenceNum"></FONT></TD>
																	<TD align="right"><FONT face="굴림">공정명&nbsp; </FONT>
																	</TD>
																	<TD align="left"><FONT face="굴림"><INPUT id="igtbl_TextBox_0_6" style="WIDTH: 100px" readOnly type="text" columnKey="ProcessName"></FONT></TD>
																</TR>
																<TR>
																	<TD align="right" colSpan="3"><FONT face="굴림">입고사유&nbsp; </FONT>
																	</TD>
																	<TD align="left"><FONT face="굴림">
																			<asp:DropDownList id=dl_Reason runat="server" DataValueField="SmallClassificationCode" DataTextField="SmallClassificationName" DataSource="<%# AddItemStore %>">
																			</asp:DropDownList></FONT></TD>
																	<TD align="right"><FONT face="굴림">입고일자&nbsp; </FONT>
																	</TD>
																	<TD align="left">
																		<igsch:webdatechooser id="WebDateChooser2" runat="server" BackColor="#EEEEE9" Height="20px" Width="100px"
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
																	<TD align="right"><FONT face="굴림">수량&nbsp; </FONT>
																	</TD>
																	<TD align="left"><INPUT id="igtbl_TextBox_0_7" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																			style="WIDTH: 100px; TEXT-ALIGN: right" onfocus="OnFocus_Obj(this);" type="text" columnKey="InstoreQuantity"></TD>
																</TR>
															</TABLE>
															<P align="center"><FONT face="굴림"></FONT>&nbsp;</P>
															<P align="center">&nbsp;</P>
															<P align="center"><FONT face="굴림"></FONT>&nbsp;</P>
															<P align="center">&nbsp;</P>
															<P align="center"><INPUT id="igtbl_reOkBtn" style="WIDTH: 50px; HEIGHT: 22px" onclick="igtbl_gRowEditButtonClick(event);Update();"
																	type="button" value="수정">&nbsp; <INPUT id="igtbl_reCancelBtn" style="WIDTH: 50px; HEIGHT: 22px" onclick="igtbl_gRowEditButtonClick(event);"
																	type="button" value="취소"></P>
														</RowEditTemplate>
													</igtbl:UltraGridBand>
												</Bands>
											</igtbl:ultrawebgrid></td>
								</tr>
								<TR>
									<TD vAlign="middle" align="left" width="10%" height="25"><asp:button id="btExcel" runat="server" Height="20px" Width="60px" Text="Excel" Font-Size="10pt"></asp:button></TD>
									<TD vAlign="middle" align="center" width="80%" colSpan="6" height="25"><igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter><INPUT id="hdYear" style="WIDTH: 10px; HEIGHT: 20px" type="hidden" size="1" name="Hidden1"
											runat="server"><INPUT id="hdMon" style="WIDTH: 10px; HEIGHT: 20px" type="hidden" name="Hidden1" runat="server"><INPUT id="lb_Quantity" style="WIDTH: 10px; HEIGHT: 20px" type="hidden" size="1" name="Hidden1"
											runat="server"><INPUT id="lb_Index" style="WIDTH: 10px; HEIGHT: 20px" type="hidden" name="Hidden1" runat="server"><INPUT id="lb_RowIndex" style="WIDTH: 10px; HEIGHT: 20px" type="hidden" name="Hidden1"
											runat="server"><FONT face="굴림"><INPUT id="chkAll" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" value="true" name="chkAll"><INPUT id="lb_ReasonCode" style="WIDTH: 10px; HEIGHT: 20px" type="hidden" name="lb_ReasonCode"
												runat="server"><INPUT id="lb_Reason" style="WIDTH: 10px" type="hidden" name="lb_Reason" runat="server">
											<asp:linkbutton id="lnk_Update" runat="server" Visible="False">LinkButton</asp:linkbutton></FONT></TD>
									<TD vAlign="middle" align="right" width="10%" height="25"><asp:button id="btDelete" runat="server" Height="20px" Width="60px" Text="삭 제" Font-Size="10pt"></asp:button></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			&nbsp;
		</form>
	</body>
</HTML>
