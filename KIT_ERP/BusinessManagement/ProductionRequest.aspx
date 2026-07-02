<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="ProductionRequest.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BusinessManagement.ProductionRequestPC" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TITLE>ProductionRequestPC</TITLE>
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
			document.Form1.ddlItemClassification1.options[0].selected=true;
		}
		
		function UltraWebGrid1_AfterRowTemplateCloseHandler(gridName, rowId, bSaveChanges){
			//Add code to handle your event here.
			var row = igtbl_getRowById(rowId);
			row.getCellFromKey("RemainderQuantity").setValue(Form1.tb_TotalReceiveingOrderQuantity.value);
		}
		
		function UltraWebGrid1_ColumnHeaderClickHandler(gridName, columnId, button)
		{
			GridName = gridName;
    			if(document.Form1.chkAll.value == "true")
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
			document.Form1.chkAll.value = "false";
		}


		function AllUncheck() // 전부체크 해제하기
		{
			var grid = igtbl_getGridById(GridName);
			
			for(var i=0; i<grid.Rows.length; i++)
			{
				var row = grid.Rows.getRow(i);
				row.getCellFromKey("chk").setValue(false);
			}
			document.Form1.chkAll.value = "true";
		}
		
		function OK(text)
		{
			if(confirm(text + "하시겠습니까?"))
				return true;
			else
				return false;
		}
		
		
		
		function Process()	
		{
			var total_Quantity = Number(Form1.igtbl_TextBox_0_8.value) + Number(Form1.igtbl_TextBox_0_10.value)+Number(Form1.igtbl_TextBox_0_12.value) + Number(Form1.igtbl_TextBox_0_14.value)+Number(Form1.igtbl_TextBox_0_16.value);
			
			Form1.tb_TotalReceiveingOrderQuantity.value = total_Quantity;			
			
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
		function UltraWebGrid1_MouseOverHandler(gridName, id, button)
		{
		
			//Add code to handle your event here.
			if(button == 0)
			{ // Are we over a cell
				var cell = igtbl_getElementById(id);
				cell.style.cursor = 'hand';
				//var label = igtbl_getElementById("lb_RowIndex");
				var parts = id.split("_");
				document.Form1.hdIndex.value = parts[1];
			}
		}
		
		function UltraWebGrid1_DblClickHandler(gridName, cellId){
			//Add code to handle your event here.
			 Form1.hdRowIndex.value = Form1.hdIndex.value;		
			
		}
		
	
		
		
--></SCRIPT>
	</HEAD>
	<body XMLNS:igtbl="http://schemas.infragistics.com/ASPNET/WebControls/UltraWebGrid" MS_POSITIONING="GridLayout"
		bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; WIDTH: 800px; POSITION: absolute; TOP: 10px; HEIGHT: 536px"
				height="536" cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD width="20"><FONT face="굴림"></FONT></TD>
					<TD style="PADDING-TOP: 10px" vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 800px; BORDER-BOTTOM: #696969 2px solid; HEIGHT: 32px"
							DESIGNTIMEDRAGDROP="3865"><FONT face="굴림"></FONT><LEGEND align="top" style="FONT-SIZE: 9pt">[검색조건]</LEGEND>
							<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
								<TR height="35">
									<TD align="left" width="600" colSpan="8" height="30">&nbsp;
										<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" border="0">
											<TR>
												<TD align="left" width="200" height="30">
													<uc1:companysearchcontrol id="CSC1" runat="server"></uc1:companysearchcontrol></TD>
												<TD align="left" width="600" height="30">
													<uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30">납기요구일&nbsp;</TD>
									<TD align="left" width="100" height="30">
										<igsch:webdatechooser id="wdcStartDate" runat="server" Width="100px" BorderStyle="Solid" BackColor="#EEEEE9"
											Height="20px" BorderColor="DimGray" Text=" " NullDateLabel=" ">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderWidth="1px" BorderStyle="Solid"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD align="center" width="5" height="30">~</TD>
									<TD width="100" height="30">
										<igsch:webdatechooser id="wdcEndDate" runat="server" Width="100px" BorderStyle="Solid" BackColor="#EEEEE9"
											Height="20px" BorderColor="DimGray" Text=" " NullDateLabel=" ">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderWidth="1px" BorderStyle="Solid"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<td align="right" width="70">제품팀&nbsp;</td>
									<td align="left" width="100"><asp:dropdownlist id="ddlItemClassification1" runat="server" Width="115px" BackColor="#EEEEE9"></asp:dropdownlist></td>
									<TD align="right" width="355" colSpan="2" height="30"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
											type="button" value="초기화" name="btnReset">&nbsp;
										<asp:button id="btnSearch" runat="server" Font-Size="9pt" Height="20px" Width="60px" Text="검   색"
											CommandName="Search"></asp:button>&nbsp;</TD>
								</TR>
								<TR>
									<TD align="right" width="70" colSpan="8" height="8"></TD>
								</TR>
							</TABLE>
							<FONT face="굴림">
								<asp:Label id="lb_Index" runat="server" Visible="False"></asp:Label></FONT></FIELDSET>
						<BR>
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 800px; BORDER-BOTTOM: #696969 2px solid; HEIGHT: 425px"><LEGEND align="top" style="FONT-SIZE: 9pt">[검색결과]</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800">
								<TR>
									<TD colSpan="2">
										<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="390px" Width="800px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect"
												AllowUpdateDefault="RowTemplateOnly">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray"></Style>
												</AddNewBox>
												<Pager PageSize="17" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray"></Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="Verdana" BorderStyle="Solid"
													BackColor="Silver" Height="390px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents ColumnHeaderClickHandler="UltraWebGrid1_ColumnHeaderClickHandler" DblClickHandler="UltraWebGrid1_DblClickHandler"
													AfterRowTemplateCloseHandler="UltraWebGrid1_AfterRowTemplateCloseHandler" MouseOverHandler="UltraWebGrid1_MouseOverHandler"></ClientSideEvents>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" BackColor="#E0E5CD"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand AllowUpdate="RowTemplateOnly">
													<Columns>
														<igtbl:UltraGridColumn HeaderText="전체" Key="chk" Width="30px" Type="CheckBox" HeaderClickAction="Select"
															BaseColumnName="chk" AllowResize="Free" AllowUpdate="Yes">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목상태" Key="ItemState" BaseColumnName="ItemState"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ItemDrawNum">
															<CellStyle VerticalAlign="Top" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" Width="168px" HeaderClickAction="SortMulti"
															BaseColumnName="CompanyName">
															<CellStyle VerticalAlign="Top" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사업자번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="BusinessRegistrationNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="자산분류" Key="PropertyClassification" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="PropertyClassification">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수주일자" Key="ReceivingOrderDate" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="ReceivingOrderDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="납기량" Key="TotalReceiveingOrderQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
															BaseColumnName="TotalReceiveingOrderQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="납기요구일" Key="DeliveryRequestDate1" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="DeliveryRequestDate1">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="1차납기요구량" Key="DeliveryRequestQuantity1" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestQuantity1">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="2차납기요구량" Key="DeliveryRequestQuantity2" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestQuantity2">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="2차납기요구일" Key="DeliveryRequestDate2" Hidden="True" Format="yyyy-MM-dd"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestDate2">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="3차납기요구량" Key="DeliveryRequestQuantity3" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestQuantity3">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="3차납기요구일" Key="DeliveryRequestDate3" Hidden="True" Format="yyyy-MM-dd"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestDate3">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="4차납기요구량" Key="DeliveryRequestQuantity4" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestQuantity4">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="4차납기요구일" Key="DeliveryRequestDate4" Hidden="True" Format="yyyy-MM-dd"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestDate4">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="5차납기요구량" Key="DeliveryRequestQuantity5" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestQuantity5">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="5차납기요구일" Key="DeliveryRequestDate5" Hidden="True" Format="yyyy-MM-dd"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestDate5">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Hidden="True" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
															BaseColumnName="ApplyUnitCost">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="출고수량" Key="OutStorehouseQuantity" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="OutStorehouseQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="합격수량" Key="SuitabilityQuantity" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="SuitabilityQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="미검수량" Key="UnInspectionQuantity" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="UnInspectionQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="볼륨번호" Key="VolumNum" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="VolumNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ProgressCondition">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="납품잔량" Key="RemainderQuantity" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="RemainderQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationPerson">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationPersonID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingPerson">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingPersonID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="번호" Key="ReceivingOrderHistoryIndex" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ReceivingOrderHistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
													</Columns>
													<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
														<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
													</RowTemplateStyle>
													<RowEditTemplate>
														<TABLE style="WIDTH: 612px; COLOR: buttontext; HEIGHT: 110px; BACKGROUND-COLOR: whitesmoke">
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
																<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT style="FONT-SIZE: 9pt" face="굴림">1차납기요구량</FONT></TD>
																<TD height="16" aline="left"><FONT face="굴림"><FONT face="굴림"><INPUT id="igtbl_TextBox_0_8" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																				onkeyup="return Process();" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																				onfocus="OnFocus_Obj(this);" type="text" columnKey="DeliveryRequestQuantity1"></FONT></FONT></TD>
																<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT style="FONT-SIZE: 9pt" face="굴림"><FONT style="FONT-SIZE: 9pt" face="굴림">2차납기요구량</FONT></FONT></TD>
																<TD height="16" aline="left"><FONT face="굴림"><INPUT id="igtbl_TextBox_0_10" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																			onkeyup="return Process();" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																			onfocus="OnFocus_Obj(this);" type="text" columnKey="DeliveryRequestQuantity2"></FONT></TD>
																<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT style="FONT-SIZE: 9pt" face="굴림"><FONT style="FONT-SIZE: 9pt" face="굴림">3차납기요구량</FONT></FONT></TD>
																<TD height="16" aline="left"><FONT face="굴림"><INPUT id="igtbl_TextBox_0_12" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																			onkeyup="return Process();" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																			onfocus="OnFocus_Obj(this);" type="text" columnKey="DeliveryRequestQuantity3"></FONT></TD>
															</TR>
															<TR>
																<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT face="굴림">4차납기요구량</FONT></TD>
																<TD height="16" aline="left"><FONT face="굴림"><INPUT id="igtbl_TextBox_0_14" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																			onkeyup="return Process();" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																			onfocus="OnFocus_Obj(this);" type="text" columnKey="DeliveryRequestQuantity4"></FONT></TD>
																<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT face="굴림">5차납기요구량</FONT></TD>
																<TD height="16" aline="left"><FONT face="굴림"><INPUT id="igtbl_TextBox_0_16" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																			onkeyup="return Process();" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																			onfocus="OnFocus_Obj(this);" type="text" columnKey="DeliveryRequestQuantity5"></FONT></TD>
																<TD align="right" width="110" bgColor="#dcdcdc" height="16"><FONT style="FONT-SIZE: 9pt" face="굴림">총수주량</FONT></TD>
																<TD height="16" aline="left"><FONT face="굴림"><INPUT id="tb_TotalReceiveingOrderQuantity" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																			onkeyup="return Process();" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																			onfocus="OnFocus_Obj(this);" type="text" columnKey="TotalReceiveingOrderQuantity"></FONT></TD>
															</TR>
															<TR>
																<TD align="center" colSpan="6"><INPUT id="igtbl_reOkBtn" style="WIDTH: 60px; HEIGHT: 20px" onclick="igtbl_gRowEditButtonClick(event);__doPostBack('lnk_Update','');"
																		type="button" value="생산의뢰" name="igtbl_reOkBtn">&nbsp; <INPUT id="igtbl_reCancelBtn" style="WIDTH: 60px; HEIGHT: 20px" onclick="igtbl_gRowEditButtonClick(event);"
																		type="button" value="취 소">
																</TD>
															</TR>
														</TABLE>
													</RowEditTemplate>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
							</TABLE>
							<TABLE id="Table4" style="HEIGHT: 4px" cellSpacing="0" cellPadding="0" width="800">
								<TR>
									<TD align="left" height="30"><FONT face="굴림">&nbsp;</FONT>
										<asp:button id="Button1" runat="server" Height="20px" Width="107px" Text="제품 잉여분 보기"></asp:button><INPUT id="hdRowIndex" style="FONT-SIZE: 9pt; WIDTH: 29px; HEIGHT: 21px" type="hidden"
											size="1" value="-1" name="Hidden1" runat="server"></TD>
									<TD align="right" height="30"><FONT face="굴림">&nbsp;
											<asp:linkbutton id="lnk_Update" runat="server" Font-Size="9pt" Visible="False">LinkButton</asp:linkbutton>&nbsp;&nbsp;&nbsp;<INPUT id="hdRemainQuantity" style="FONT-SIZE: 9pt; WIDTH: 29px; HEIGHT: 21px" type="hidden"
												size="1" value="0" name="Hidden1" runat="server"><INPUT id="hdTotalCost" style="FONT-SIZE: 9pt; WIDTH: 29px; HEIGHT: 21px" type="hidden"
												size="1" value="0" name="Hidden1" runat="server"><INPUT id="hdIndex" style="FONT-SIZE: 9pt; WIDTH: 29px; HEIGHT: 21px" type="hidden" size="1"
												value="0" name="Hidden1" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </FONT>
										<INPUT id="chkAll" type="hidden" value="true" style="FONT-SIZE: 9pt">&nbsp;
										<asp:button id="bt_Regist" runat="server" Width="70px" Height="20px" Text="생산의뢰"></asp:button><FONT face="굴림">&nbsp;</FONT></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
		</form>
		</SCRIPT>
	</body>
</HTML>
