<%@ Page language="c#" Codebehind="AddItemReceivePC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BusinessManagement.AddItemReceivePC" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddItemReceivePC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--
		
		function ResettxtBox()
		{
			
			ResetTextBox();
			ResetBox();
			var objChooser1 = igdrp_getComboById("wdcStartDate");
			var objChooser2 = igdrp_getComboById("wdcEndDate");
			objChooser1.setValue(null);
			objChooser2.setValue(null);
			document.AddItemReceivePC.dl_ProgressState.options[0].selected=true;
			document.AddItemReceivePC.ddlItemClassification1.options[0].selected=true;
		}

		var GridName;
		
		function UltraWebGrid1_ColumnHeaderClickHandler(gridName, columnId, button)
		{
			GridName = gridName;
    			if(document.AddItemReceivePC.chkAll.value == "true")
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
			document.AddItemReceivePC.chkAll.value = "false";
		}


		function AllUncheck() // 전부체크 해제하기
		{
			var grid = igtbl_getGridById(GridName);
			
			for(var i=0; i<grid.Rows.length; i++)
			{
				var row = grid.Rows.getRow(i);
				row.getCellFromKey("chk").setValue(false);
			}
			document.AddItemReceivePC.chkAll.value = "true";
		}
		function OK(text)
		{
			if(confirm(text + "하시겠습니까?"))
				return true;
			else
				return false;
		}
		
		
		function UltraWebGrid1_AfterRowTemplateCloseHandler(gridName, rowId, bSaveChanges){
			//Add code to handle your event here.
			//그리드의 Row 값을 저장함
			//document.AddItemReceivePC.lb_RowSelectIndex.value = document.AddItemReceivePC.lb_RowIndex.value;
			if(event.srcElement.id == "igtbl_reOkBtn")
			{
				var row = igtbl_getRowById(rowId);
				var date = igdrp_getComboById("UltraWebGrid1xxctl0xwdcxEditReceivingDate");
				var date1 = igdrp_getComboById("UltraWebGrid1xxctl0xwdcxDate1");
				
				if(row.getCellFromKey("ReceivingOrderDate").getValue() !=null || row.getCellFromKey("ReceivingOrderDate").getValue() !="")
					row.getCellFromKey("ReceivingOrderDate").setValue(date.getValue());
				if(row.getCellFromKey("DeliveryRequestDate1").getValue() !=null || row.getCellFromKey("DeliveryRequestDate1").getValue() !="")				
					row.getCellFromKey("DeliveryRequestDate1").setValue(date1.getValue());
				document.AddItemReceivePC.lb_Total.value = row.getCellFromKey("TotalReceiveingOrderQuantity").getValue() // tb_TotalReceiveingOrderQuantity.value;
			}
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
				document.AddItemReceivePC.lb_RowIndex.value = parts[1];
			}
		}
		
		//수정창이 열릴때
		function UltraWebGrid1_BeforeRowTemplateOpenHandler(gridName, rowId, templateId){
			//Add code to handle your event here.
			
			var objGrid = igtbl_getActiveRow(gridName);
			var row = igtbl_getRowById(rowId);
			
			document.AddItemReceivePC.lb_RowSelectIndex.value = document.AddItemReceivePC.lb_RowIndex.value;
			//document.AddItemReceivePC.ttt.value = document.AddItemReceivePC.lb_RowIndex.value;
						
			if(row.getCellFromKey("ProgressCondition").getValue() == "대기")
			{
				
				
				//수주일자 및 납기요구일
				var date  = igdrp_getComboById("UltraWebGrid1xxctl0xwdcxEditReceivingDate");
				var date1 = igdrp_getComboById("UltraWebGrid1xxctl0xwdcxDate1");
				if(row.getCellFromKey("ReceivingOrderDate").getValue() !=null)
					date.setValue(row.getCellFromKey("ReceivingOrderDate").getValue());
				if(row.getCellFromKey("DeliveryRequestDate1").getValue() !=null)
					date1.setValue(row.getCellFromKey("DeliveryRequestDate1").getValue());				
			}
			else if(row.getCellFromKey("Progress").getValue() == "진행")
			{
				alert("진행중인 품목은 수정할 수 없습니다.!");
				return true;
			}
			else if(row.getCellFromKey("Progress").getValue() == "중단")
			{
				alert("중단된 품목은 수정할 수 없습니다.!");
				return true;
			}
			else if(row.getCellFromKey("Progress").getValue() == "완료")
			{
				alert("이미 완료된 품목은 수정할 수 없습니다.!");
				return true;
			}
			
			
		}
		
		function Process()	
		{
			var total_Quantity = Number(AddItemReceivePC.igtbl_TextBox_0_8.value);
			var total_Cost = total_Quantity * Number(AddItemReceivePC.igtbl_TextBox_0_8.value);
			AddItemReceivePC.tb_TotalReceiveingOrderQuantity.value = total_Quantity;
			AddItemReceivePC.tb_TotalCost.value = total_Cost;
		}
		
		
		function OnKeyDown_Float(obj) 
		{   
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
		<form id="AddItemReceivePC" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; WIDTH: 813px; POSITION: absolute; TOP: 10px; HEIGHT: 550px"
				height="550" cellSpacing="0" cellPadding="0" width="813" border="0">
				<TR>
					<TD vAlign="top" align="center" width="800" colSpan="2">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 800px; BORDER-BOTTOM: #696969 2px solid"><LEGEND style="FONT-SIZE: 9pt" align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800">
								<TR>
									<TD width="800" colSpan="9" height="30"><FONT face="굴림">
											<table id="Table3" border="0" cellpadding="0" cellspacing="0">
												<tr>
													<td width="200"><uc1:CompanySearchControl id="CSC1" runat="server"></uc1:CompanySearchControl></td>
													<td width="600"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol><asp:linkbutton id="lnk_Update" runat="server" Font-Size="9pt" Visible="False">LinkButton</asp:linkbutton></td>
												</tr>
											</table>
										</FONT>
									</TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림">진행상태&nbsp;</FONT></TD>
									<TD width="100" height="30">
										<asp:dropdownlist id="dl_ProgressState" runat="server" Width="100px" Height="20px" BackColor="#EEEEE9">
											<asp:ListItem Value=" ">-선 택-</asp:ListItem>
											<asp:ListItem Value="미납">미납</asp:ListItem>
											<asp:ListItem Value="중단">중단</asp:ListItem>
											<asp:ListItem Value="완료">완료</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD align="right" width="70" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림">납기일자&nbsp;</FONT></TD>
									<TD width="100" height="30">
										<igsch:webdatechooser id="wdcStartDate" runat="server" Width="100px" Height="22px" BackColor="#EEEEE9"
											BorderColor="DimGray" BorderStyle="Solid" Text=" " NullDateLabel=" ">
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
									<TD align="center" width="5" height="30"><FONT face="굴림">~</FONT></TD>
									<TD width="100" height="30">
										<igsch:webdatechooser id="wdcEndDate" runat="server" Width="100px" Height="22px" BackColor="#EEEEE9" BorderColor="DimGray"
											BorderStyle="Solid" Text=" " NullDateLabel=" ">
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
									<td width="70" align="right"><FONT face="굴림">제품팀&nbsp; </FONT>
									</td>
									<td width="115" align="left">
										<asp:dropdownlist id="ddlItemClassification1" runat="server" BackColor="#EEEEE9" Width="115px"></asp:dropdownlist></td>
									<TD align="right" width="170" height="30"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
											type="button" value="초기화" name="btnReset"><FONT face="굴림">&nbsp; </FONT>
										<asp:button id="bt_Search" runat="server" Width="60px" Height="20px" Text="검  색"></asp:button>&nbsp;</TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="5" colSpan="9"><FONT face="굴림"></FONT><FONT face="굴림"></FONT></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" width="800" colSpan="2">&nbsp;
						<br>
						<FIELDSET title="86" style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND style="FONT-SIZE: 9pt" align="top">[검색결과]</LEGEND>
							<TABLE id="Table4" style="WIDTH: 100%" cellSpacing="1" cellPadding="1" width="800" border="0">
								<tr>
									<td align="center" width="800" colSpan="2"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="386px" Width="100%">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect"
												NoDataMessage="해당 자료가 없습니다!" AllowUpdateDefault="RowTemplateOnly">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</AddNewBox>
												<Pager PageSize="16" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</Pager>
												<HeaderStyleDefault Cursor="Auto" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
												<FrameStyle Width="100%" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
													BackColor="Silver" Height="386px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents ColumnHeaderClickHandler="UltraWebGrid1_ColumnHeaderClickHandler" BeforeRowTemplateOpenHandler="UltraWebGrid1_BeforeRowTemplateOpenHandler"
													AfterRowTemplateCloseHandler="UltraWebGrid1_AfterRowTemplateCloseHandler" MouseOverHandler="UltraWebGrid1_MouseOverHandler"></ClientSideEvents>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault BackColor="LightSteelBlue"></RowAlternateStyleDefault>
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
														<igtbl:UltraGridColumn HeaderText="발주번호" Key="OrderNum" HeaderClickAction="SortMulti" BaseColumnName="OrderNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목상태" Key="ItemState" BaseColumnName="ItemState"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ItemDrawNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" HeaderClickAction="SortMulti" BaseColumnName="CompanyName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="BusinessRegistrationNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="자산분류" Key="PropertyClassification" Width="70px" HeaderClickAction="SortMulti"
															BaseColumnName="PropertyClassification">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="생산의뢰여부" Key="ProductionRequestDivision" Hidden="True" BaseColumnName="ProductionRequestDivision">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수주일자" Key="ReceivingOrderDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="ReceivingOrderDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="1차납기요구량" Key="DeliveryRequestQuantity1" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestQuantity1">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총수주량" Key="TotalReceiveingOrderQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
															BaseColumnName="TotalReceiveingOrderQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="납기요구일" Key="DeliveryRequestDate1" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="DeliveryRequestDate1">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="납품장소" Key="DeliveryPlace" HeaderClickAction="SortMulti" BaseColumnName="DeliveryPlace">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="2차납기요구량" Key="DeliveryRequestQuantity2" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestQuantity2">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="2차납기요구일" Key="DeliveryRequestDate2" Hidden="True" Format="yyyy-MM-dd"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestDate2">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="3차납기요구량" Key="DeliveryRequestQuantity3" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestQuantity3">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="3차납기요구일" Key="DeliveryRequestDate3" Hidden="True" Format="yyyy-MM-dd"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestDate3">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="4차납기요구량" Key="DeliveryRequestQuantity4" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestQuantity4">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="4차납기요구일" Key="DeliveryRequestDate4" Hidden="True" Format="yyyy-MM-dd"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestDate4">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="5차납기요구량" Key="DeliveryRequestQuantity5" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestQuantity5">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="5차납기요구일" Key="DeliveryRequestDate5" Hidden="True" Format="yyyy-MM-dd"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestDate5">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
															BaseColumnName="ApplyUnitCost">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총금액" Key="TotalCost" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
															BaseColumnName="TotalCost">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="출고수량" Key="OutStorehouseQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
															BaseColumnName="OutStorehouseQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="적합수량" Key="SuitabilityQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
															BaseColumnName="SuitabilityQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="미검수량" Key="UnInspectionQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
															BaseColumnName="UnInspectionQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="납품잔량" Key="RemainderQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
															BaseColumnName="RemainderQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="볼륨번호" Key="VolumNum" HeaderClickAction="SortMulti" BaseColumnName="VolumNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="진행상태" Key="Progress" HeaderClickAction="SortMulti" BaseColumnName="Progress">
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
														<igtbl:UltraGridColumn HeaderText="번호" Key="ReceivingOrderHistoryIndex" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ReceivingOrderHistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="상태" Key="ProgressCondition" Hidden="True" BaseColumnName="ProgressCondition"></igtbl:UltraGridColumn>
													</Columns>
													<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
														<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
													</RowTemplateStyle>
													<RowEditTemplate>
														<TABLE style="WIDTH: 612px; COLOR: buttontext; HEIGHT: 266px; BACKGROUND-COLOR: whitesmoke">
															<TR>
																<TD align="right" width="110" bgColor="#dcdcdc" height="16" aline="right"><FONT face="굴림">품목번호</FONT></TD>
																<TD height="16" aline="left"><INPUT id="igtbl_TextBox_0_1" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																		disabled readOnly type="text" columnKey="ItemNum"></TD>
																<TD align="right" width="110" bgColor="#dcdcdc" height="16" aline="right"><FONT face="굴림">도면번호</FONT></TD>
																<TD height="16" aline="left"><INPUT id="igtbl_TextBox_0_2" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																		disabled readOnly type="text" columnKey="ItemDrawNum"></TD>
																<TD align="right" width="110" bgColor="#dcdcdc" height="16" aline="right"><FONT face="굴림">품목명</FONT></TD>
																<TD height="16" aline="left"><INPUT id="igtbl_TextBox_0_3" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																		disabled readOnly type="text" columnKey="ItemName"></TD>
															</TR>
															<TR>
																<TD align="right" width="110" bgColor="#dcdcdc" height="16" aline="right"><FONT face="굴림">수주일자</FONT></TD>
																<TD height="16" aline="left">
																	<igsch:webdatechooser id="wdc_EditReceivingDate" runat="server" Height="16px" Width="110px" NullDateLabel=" "
																		Text="Null" columnKey="ReceivingOrderDate">
																		<CALENDARLAYOUT DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
																			ShowTitle="False" ShowFooter="False">
																			<SELECTEDDAYSTYLE ForeColor="White" BackColor="#0A246A"></SELECTEDDAYSTYLE>
																			<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																			<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																			<DAYHEADERSTYLE ForeColor="#D4D0C8" BackColor="Gray"></DAYHEADERSTYLE>
																		</CALENDARLAYOUT>
																		<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
																		<DROPBUTTON ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DROPBUTTON>
																		<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
																	</igsch:webdatechooser></TD>
																<TD align="right" width="110" bgColor="#dcdcdc" height="16" aline="right"><FONT style="FONT-SIZE: 9pt" face="굴림">자산분류</FONT></TD>
																<TD height="16" aline="left"><INPUT id="igtbl_TextBox_0_6" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																		disabled readOnly type="text" columnKey="PropertyClassification"></TD>
																<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT style="FONT-SIZE: 9pt" face="굴림">총수주량</FONT></TD>
																<TD height="16" aline="left"><FONT face="굴림"><INPUT id="tb_TotalReceiveingOrderQuantity" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																			onkeyup="return Process();" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																			onfocus="OnFocus_Obj(this);" type="text" disabled readOnly columnKey="TotalReceiveingOrderQuantity"></FONT></TD>
															</TR>
															<TR>
																<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT style="FONT-SIZE: 9pt" face="굴림">1차납기요구량</FONT></TD>
																<TD height="16" aline="left"><FONT face="굴림"><FONT face="굴림"><INPUT id="igtbl_TextBox_0_8" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																				onkeyup="return Process();" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																				onfocus="OnFocus_Obj(this);" type="text" columnKey="DeliveryRequestQuantity1"></FONT></FONT></TD>
																<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT style="FONT-SIZE: 9pt" face="굴림">1차납기요구일</FONT></TD>
																<TD height="16" aline="left"><FONT face="굴림">
																		<igsch:webdatechooser id="wdc_Date1" runat="server" Height="20px" Width="110px" NullDateLabel=" " Text="Null"
																			columnKey="DeliveryRequestDate1">
																			<CALENDARLAYOUT DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
																				ShowTitle="False" ShowFooter="False">
																				<SELECTEDDAYSTYLE ForeColor="White" BackColor="#0A246A"></SELECTEDDAYSTYLE>
																				<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																				<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																				<DAYHEADERSTYLE ForeColor="#D4D0C8" BackColor="Gray"></DAYHEADERSTYLE>
																			</CALENDARLAYOUT>
																			<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
																			<DROPBUTTON ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DROPBUTTON>
																			<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
																		</igsch:webdatechooser></FONT></TD>
																<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT style="FONT-SIZE: 9pt" face="굴림">납품장소</FONT></TD>
																<TD height="16" aline="left"><FONT face="굴림"><INPUT id="igtbl_TextBox_0_26" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																			type="text" columnKey="DeliveryPlace"></FONT></TD>
															</TR>
															<TR>
																<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT face="굴림">발주번호</FONT></TD>
																<TD height="16" aline="left"><FONT face="굴림"><INPUT id="igtbl_TextBox_0_25" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																			type="text" columnKey="OrderNum"></FONT></TD>
																<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT style="FONT-SIZE: 9pt" face="굴림">단가</FONT></TD>
																<TD height="16" aline="left"><FONT face="굴림"><INPUT id="igtbl_TextBox_0_19" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																			onkeyup="return Process();" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																			onfocus="OnFocus_Obj(this);" type="text" columnKey="ApplyUnitCost"></FONT></TD>
																<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT face="굴림"><FONT style="FONT-SIZE: 9pt" face="굴림"></FONT></FONT></TD>
																<TD height="16" aline="left"><FONT face="굴림"><FONT face="굴림"></FONT></FONT></TD>
															</TR>
															<TR>
																<TD style="HEIGHT: 2px" colSpan="6"><FONT face="굴림"><FONT face="굴림"><FONT face="굴림"><FONT face="굴림"></FONT></FONT></FONT></FONT></TD>
															</TR>
															<TR>
																<TD style="HEIGHT: 61px" colSpan="6">
																	<P><FONT face="굴림"></FONT>&nbsp;</P>
																	<P><FONT face="굴림"></FONT>&nbsp;</P>
																</TD>
															</TR>
															<TR>
																<TD align="center" colSpan="6"><INPUT id="igtbl_reOkBtn" style="WIDTH: 60px; HEIGHT: 20px" onclick="igtbl_gRowEditButtonClick(event);__doPostBack('lnk_Update','');"
																		type="button" value="수 정" name="igtbl_reOkBtn">&nbsp; <INPUT id="igtbl_reCancelBtn" style="WIDTH: 60px; HEIGHT: 20px" onclick="igtbl_gRowEditButtonClick(event);"
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
									<TD style="WIDTH: 34px" height="30"><asp:button id="Button3" runat="server" Height="20px" Width="60px" Text="Excel"></asp:button></TD>
									<TD align="right" height="30"><INPUT id="tb_TotalCost" style="WIDTH: 80px; HEIGHT: 20px; BACKGROUND-COLOR: #eeeee9" type="hidden"
											value="0" name="Hidden1" runat="server"><INPUT id="chkAll" style="WIDTH: 80px; HEIGHT: 20px; BACKGROUND-COLOR: #eeeee9" type="hidden"
											value="true" runat="server" NAME="chkAll">&nbsp;&nbsp;
										<asp:button id="bt_Delete" runat="server" Height="20px" Width="60px" Text="삭  제"></asp:button>&nbsp;
										<asp:button id="bt_Stop" runat="server" Height="20px" Width="60px" Text="중  단"></asp:button></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			<igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter><INPUT id="lb_Total" style="Z-INDEX: 102; LEFT: 856px; WIDTH: 80px; POSITION: absolute; TOP: 576px; HEIGHT: 20px; BACKGROUND-COLOR: #eeeee9"
				type="hidden" value="0" runat="server" size="8" NAME="lb_Total"><INPUT id="lb_RowSelectIndex" style="Z-INDEX: 103; LEFT: 848px; WIDTH: 80px; POSITION: absolute; TOP: 88px; HEIGHT: 20px"
				type="hidden" name="Hidden1" runat="server"><INPUT id="lb_RowIndex" style="Z-INDEX: 104; LEFT: 848px; WIDTH: 80px; POSITION: absolute; TOP: 120px; HEIGHT: 20px"
				type="hidden" name="Hidden1" runat="server"></form>
	</body>
</HTML>
