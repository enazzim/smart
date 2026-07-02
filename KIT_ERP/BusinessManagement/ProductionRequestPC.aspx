<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Page language="c#" Codebehind="ProductionRequestPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BusinessManagement.ProductionRequest" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ProductionRequest</title>
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
			document.ProductionRequestPC.dl_ProgressState.options[0].selected=true;
			document.ProductionRequestPC.dl_Source.options[0].selected=true;
		}
		var GridName;
				
		function OK(text)
		{
			if(confirm(text + "하시겠습니까?"))
				return true;
			else
				return false;
		}
		
		
		function UltraWebGrid1_ColumnHeaderClickHandler(gridName, columnId, button)
		{
			GridName = gridName;
    			if(document.ProductionRequestPC.chkAll.value == "true")
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
			document.ProductionRequestPC.chkAll.value = "false";
		}


		function AllUncheck() // 전부체크 해제하기
		{
			var grid = igtbl_getGridById(GridName);
			
			for(var i=0; i<grid.Rows.length; i++)
			{
				var row = grid.Rows.getRow(i);
				row.getCellFromKey("chk").setValue(false);
			}
			document.ProductionRequestPC.chkAll.value = "true";
		}
		
		function OK(text)
		{
			if(confirm(text + "하시겠습니까?"))
				return true;
			else
				return false;
		}
		// 수정창이 열릴때
		function UltraWebGrid1_AfterRowTemplateOpenHandler(gridName, rowId)
		{
			//document.ProductionRequestPC.lb_RowSelectIndex.value = document.ProductionRequestPC.lb_RowIndex.value			
		}
		// 그리드 선택시 Row 인덱스 찾기
		function UltraWebGrid1_MouseOverHandler(gridName, id, button){
			//Add code to handle your event here.
			//Add code to handle your event here.
			if(button == 0)
			{ // Are we over a cell
				var cell = igtbl_getElementById(id);
				cell.style.cursor = 'hand';
				//var label = igtbl_getElementById("lb_RowIndex");
				var parts = id.split("_");
				document.ProductionRequestPC.lb_RowIndex.value = parts[1];
			}
		}
		// 수정창이 닫힐때
		function UltraWebGrid1_AfterRowTemplateCloseHandler(gridName, rowId, bSaveChanges)
		{
			if(event.srcElement.id == "igtbl_reOkBtn")
			{
				var row = igtbl_getRowById(rowId);
				var date1 = igdrp_getComboById("UltraWebGrid1xxctl0xWebdatechooser1");
				var date2 = igdrp_getComboById("UltraWebGrid1xxctl0xWebdatechooser2");
				var date3 = igdrp_getComboById("UltraWebGrid1xxctl0xWebdatechooser3");
				var date4 = igdrp_getComboById("UltraWebGrid1xxctl0xWebdatechooser4");
				var date5 = igdrp_getComboById("UltraWebGrid1xxctl0xWebdatechooser5");
				
				row.getCellFromKey("ProductionRequestQuantity").setValue(document.ProductionRequestPC.igtbl_TextBox_0_9.value);		
				
				if(row.getCellFromKey("RequestDate1").getValue() != null)
					row.getCellFromKey("RequestDate1").setValue(date1.getValue());
				if(row.getCellFromKey("RequestDate2").getValue() != null)
					row.getCellFromKey("RequestDate2").setValue(date1.getValue());
				if(row.getCellFromKey("RequestDate3").getValue() != null)
					row.getCellFromKey("RequestDate3").setValue(date1.getValue());
				if(row.getCellFromKey("RequestDate4").getValue() != null)
					row.getCellFromKey("RequestDate4").setValue(date1.getValue());
				if(row.getCellFromKey("RequestDate5").getValue() != null)
					row.getCellFromKey("RequestDate5").setValue(date1.getValue());
					
					
				//의뢰원천
				row.getCellFromKey("ProductionRequestSourceCode").setValue(document.ProductionRequestPC.UltraWebGrid1__ctl0_DropDownList1.options[document.ProductionRequestPC.UltraWebGrid1__ctl0_DropDownList1.selectedIndex].value);
				row.getCellFromKey("ProductionRequestSource").setValue(document.ProductionRequestPC.UltraWebGrid1__ctl0_DropDownList1.options[document.ProductionRequestPC.UltraWebGrid1__ctl0_DropDownList1.selectedIndex].innerText);
				
				document.ProductionRequestPC.lb_SourceCode.value = document.ProductionRequestPC.UltraWebGrid1__ctl0_DropDownList1.options[document.ProductionRequestPC.UltraWebGrid1__ctl0_DropDownList1.selectedIndex].value;
				document.ProductionRequestPC.lb_Source.value = document.ProductionRequestPC.UltraWebGrid1__ctl0_DropDownList1.options[document.ProductionRequestPC.UltraWebGrid1__ctl0_DropDownList1.selectedIndex].innerText;
				
				
				document.ProductionRequestPC.lb_Total.value = ProductionRequestPC.igtbl_TextBox_0_9.value;
				
				
			}
			
		}
		//수정창이 열릴때
		function UltraWebGrid1_BeforeRowTemplateOpenHandler(gridName, rowId, templateId)
		{
			//Add code to handle your event here.
			document.ProductionRequestPC.lb_RowSelectIndex.value = document.ProductionRequestPC.lb_RowIndex.value	
			var objGrid = igtbl_getActiveRow(gridName);
			var row = igtbl_getRowById(rowId);
			
			
			
			//if((row.getCellFromKey("ProgressCondition").getValue() == "대기") && (row.getCellFromKey("ProductionRequestSource").getValue() != "정상"))
			if(row.getCellFromKey("ProgressCondition").getValue() == "대기")
			{
					
			
				//구매의뢰원천 입력
				for(var i=0;i<document.ProductionRequestPC.UltraWebGrid1__ctl0_DropDownList1.options.length;i++)
				{
					if(document.ProductionRequestPC.UltraWebGrid1__ctl0_DropDownList1.options[i].innerText == row.getCellFromKey("ProductionRequestSource").getValue())
					{
						document.ProductionRequestPC.UltraWebGrid1__ctl0_DropDownList1.options[i].selected=true;
					}
				}
			
				//납기요구일
				var date1 = igdrp_getComboById("UltraWebGrid1xxctl0xWebdatechooser1");
				var date2 = igdrp_getComboById("UltraWebGrid1xxctl0xWebdatechooser2");
				var date3 = igdrp_getComboById("UltraWebGrid1xxctl0xWebdatechooser3");
				var date4 = igdrp_getComboById("UltraWebGrid1xxctl0xWebdatechooser4");
				var date5 = igdrp_getComboById("UltraWebGrid1xxctl0xWebdatechooser5");
				
				if(row.getCellFromKey("RequestDate1").getValue() !=null)
					date1.setValue(row.getCellFromKey("RequestDate1").getValue());
				if(row.getCellFromKey("RequestDate2").getValue() !=null)
					date2.setValue(row.getCellFromKey("RequestDate2").getValue());
				if(row.getCellFromKey("RequestDate3").getValue() !=null)
					date3.setValue(row.getCellFromKey("RequestDate3").getValue());
				if(row.getCellFromKey("RequestDate4").getValue() !=null)
					date4.setValue(row.getCellFromKey("RequestDate4").getValue());
				if(row.getCellFromKey("RequestDate5").getValue() !=null)
					date5.setValue(row.getCellFromKey("RequestDate5").getValue());
			}
			else
			{
				alert("수정할 수 없습니다.!");
				return true;
			}
		}
		
		
		function Process()	
		{
			var total_Quantity = Number(ProductionRequestPC.igtbl_TextBox_0_4.value) + Number(ProductionRequestPC.igtbl_TextBox_0_5.value)+Number(ProductionRequestPC.igtbl_TextBox_0_6.value) + Number(ProductionRequestPC.igtbl_TextBox_0_7.value)+Number(ProductionRequestPC.igtbl_TextBox_0_8.value);
			ProductionRequestPC.igtbl_TextBox_0_9.value = total_Quantity;
			//ProductionRequestPC.lb_Total.value = total_Quantity;			
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
		<form id="ProductionRequestPC" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; WIDTH: 800px; POSITION: absolute; TOP: 10px"
				height="550" cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD width="20" height="550"></TD>
					<TD style="PADDING-TOP: 10px" vAlign="top" align="center" width="800" height="550">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 805px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 60px"><LEGEND align="top" style="FONT-SIZE: 9pt">[검색조건]</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" height="68">
								<TR>
									<TD width="600" colSpan="9" height="30">
										<table id="table4" border="0" cellpadding="0" cellspacing="0" width="800">
											<TBODY>
												<tr>
													<td width="200">
														<uc1:CompanySearchControl id="CSC1" runat="server"></uc1:CompanySearchControl></FONT></td>
									</TD>
									<td width="600"><uc1:ItemSearchControl id="ItemSearchControl1" runat="server"></uc1:ItemSearchControl></td>
								</TR>
							</TABLE>
					</TD>
				</TR>
				<tr>
					<td height="30" align="right" width="70"><FONT face="굴림">진행상태&nbsp; </FONT>
					</td>
					<td height="30" width="130">
						<asp:dropdownlist id="dl_ProgressState" runat="server" Font-Size="9pt" Width="100px" BackColor="#EEEEE9"></asp:dropdownlist></td>
					<td height="30" align="right" width="70"><FONT face="굴림">의뢰원천&nbsp;</FONT></td>
					<td height="30" width="130"><asp:dropdownlist id="dl_Source" runat="server" Font-Size="9pt" Width="122px" BackColor="#EEEEE9"></asp:dropdownlist></td>
					<td height="30" align="right" width="70"><FONT style="FONT-SIZE: 9pt" face="굴림">납기일&nbsp;</FONT></td>
					<td height="30" width="120">
						<igsch:webdatechooser id="wdcStartDate" runat="server" Height="20px" BackColor="#EEEEE9" Text=" " MaxDate="2076-06-06"
							NullDateLabel=" " BorderStyle="Solid" BorderColor="DimGray">
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
						</igsch:webdatechooser></td>
					<td width="5" height="30"><FONT face="굴림">~</FONT></td>
					<td height="30" align="left" width="205" vAlign="middle" colSpan="2"><FONT face="굴림"></FONT>
						<igsch:webdatechooser id="wdcEndDate" runat="server" BackColor="#EEEEE9" Height="20px" NullDateLabel=" "
							MaxDate="2076-06-06" Text=" " BorderStyle="Solid" BorderColor="DimGray">
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
						</igsch:webdatechooser><FONT face="굴림"></FONT></td>
				</tr>
				<tr>
					<td height="20" colspan="9" align="right"><INPUT id="lb_RowIndex" style="FONT-SIZE: 9pt; WIDTH: 50px; HEIGHT: 20px" type="hidden"
							size="3" name="Hidden1" runat="server"><INPUT id="lb_RowSelectIndex" style="FONT-SIZE: 9pt; WIDTH: 50px; HEIGHT: 20px" type="hidden"
							size="3" name="Hidden1" runat="server">
						<asp:linkbutton id="LinkButton1" runat="server" Visible="False">LinkButton</asp:linkbutton>&nbsp;<INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
							type="button" value="초기화" name="btnReset">
						<asp:button id="bt_Search" runat="server" Height="20px" Width="60px" Text="검색"></asp:button>&nbsp;</td>
				</tr>
				<TR>
					<TD align="right" colSpan="9" height="8"></TD>
				</TR>
			</TABLE>
			</FIELDSET>
			<BR>
			<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; FONT-SIZE: 0pt; BORDER-LEFT: #696969 2px solid; WIDTH: 807px; BORDER-BOTTOM: #696969 2px solid; HEIGHT: 400px"
				DESIGNTIMEDRAGDROP="293"><LEGEND style="FONT-SIZE: 9pt" align="top">[검색결과]</LEGEND>
				<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800">
					<TR>
						<TD align="right" vAlign="top" width="100">
							<INPUT id="Volum" style="WIDTH: 26px; HEIGHT: 12px" type="hidden" size="1" value="0" name="Hidden1"
								runat="server">
						</TD>
						<td align="right">
							<asp:button id="btnPre" runat="server" Height="16px" Width="16px" Font-Size="8pt" Text="◀" CommandName="Pre"></asp:button><asp:button id="btnNow" runat="server" Height="16px" Width="16px" Font-Size="8pt" Text="■" CommandName="Now"></asp:button><asp:button id="btnNext" runat="server" Height="16px" Width="16px" Font-Size="8pt" Text="▶"
								CommandName="Next"></asp:button>
						</td>
					</TR>
					<TR>
						<TD colSpan="2">
							<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="366px" Width="800px">
								<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
									RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
									HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
									RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect"
									AllowUpdateDefault="RowTemplateOnly">
									<AddNewBox>
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										</Style>
									</AddNewBox>
									<Pager PageSize="15" StyleMode="ComboBox" AllowPaging="True">
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										</Style>
									</Pager>
									<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</HeaderStyleDefault>
									<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
										BackColor="Silver" Height="366px"></FrameStyle>
									<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</FooterStyleDefault>
									<ClientSideEvents ColumnHeaderClickHandler="UltraWebGrid1_ColumnHeaderClickHandler" BeforeRowTemplateOpenHandler="UltraWebGrid1_BeforeRowTemplateOpenHandler"
										AfterRowTemplateOpenHandler="UltraWebGrid1_AfterRowTemplateOpenHandler" AfterRowTemplateCloseHandler="UltraWebGrid1_AfterRowTemplateCloseHandler"
										MouseOverHandler="UltraWebGrid1_MouseOverHandler"></ClientSideEvents>
									<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
									<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
									<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
									<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
										<Padding Left="3px"></Padding>
										<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
									</RowStyleDefault>
								</DisplayLayout>
								<Bands>
									<igtbl:UltraGridBand AllowUpdate="RowTemplateOnly" CellClickAction="RowSelect">
										<Columns>
											<igtbl:UltraGridColumn HeaderText="전체" Key="chk" Width="30px" Type="CheckBox" HeaderClickAction="Select"
												BaseColumnName="chk" AllowResize="Free" AllowUpdate="Yes">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="품목상태" Key="ItemState" BaseColumnName="ItemState"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="ItemDrawNum">
												<CellStyle VerticalAlign="Top" HorizontalAlign="Left"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="자산분류" Key="PropertyClassification" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="PropertyClassification">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="생산의뢰원천코드" Key="ProductionRequestSourceCode" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="ProductionRequestSourceCode">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="생산의뢰원천" Key="ProductionRequestSource" HeaderClickAction="SortMulti"
												BaseColumnName="ProductionRequestSource">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" HeaderClickAction="SortMulti" BaseColumnName="CompanyName">
												<CellStyle VerticalAlign="Top" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="사업자번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="BusinessRegistrationNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="총수주량" Key="ProductionRequestQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
												BaseColumnName="ProductionRequestQuantity">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="1차납기요구량" Key="RequestQuantity1" Hidden="True" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="RequestQuantity1">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="납기요구일" Key="RequestDate1" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="RequestDate1">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="2차납기요구량" Key="RequestQuantity2" Hidden="True" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="RequestQuantity2">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="2차납기요구일" Key="RequestDate2" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="RequestDate2">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="3차납기요구량" Key="RequestQuantity3" Hidden="True" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="RequestQuantity3">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="3차납기요구일" Key="RequestDate3" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="RequestDate3">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="4차납기요구량" Key="RequestQuantity4" Hidden="True" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="RequestQuantity4">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="4차납기요구일" Key="RequestDate4" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="RequestDate4">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="5차납기요구량" Key="RequestQuantity5" Hidden="True" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="RequestQuantity5">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="5차납기요구일" Key="RequestDate5" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="RequestDate5">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
												BaseColumnName="ApplyUnitCost">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="볼륨번호" Key="VolumNum" HeaderClickAction="SortMulti" BaseColumnName="VolumNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" HeaderClickAction="SortMulti" BaseColumnName="ProgressCondition">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPerson">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="RegistrationPersonID">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="RegistrationDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" HeaderClickAction="SortMulti" BaseColumnName="UpdatingPerson">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="UpdatingPersonID">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="UpdatingDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="발주원장번호" Key="ReceivingOrderHistoryIndex" HeaderClickAction="SortMulti"
												BaseColumnName="ReceivingOrderHistoryIndex">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="번호" Key="ProductionRequestHistoryIndex" HeaderClickAction="SortMulti"
												BaseColumnName="ProductionRequestHistoryIndex">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
										</Columns>
										<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
											<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
										</RowTemplateStyle>
										<RowEditTemplate>
											<TABLE>
												<TR>
													<TD align="right">
														<asp:Label id="Label8" runat="server" Font-Size="9pt">품목번호</asp:Label></TD>
													<TD>
														<INPUT id="igtbl_TextBox_0_1" style="FONT-SIZE: 9pt; WIDTH: 100px" readOnly type="text"
															columnKey="ItemNum"></TD>
													<TD align="right">
														<asp:Label id="Label9" runat="server" Font-Size="9pt">도면번호</asp:Label></TD>
													<TD>
														<INPUT id="igtbl_TextBox_0_2" style="FONT-SIZE: 9pt; WIDTH: 100px" readOnly type="text"
															columnKey="ItemDrawNum"></TD>
													<TD align="right">
														<asp:Label id="Label10" runat="server" Font-Size="9pt">품목명</asp:Label></TD>
													<TD>
														<INPUT id="igtbl_TextBox_0_3" style="FONT-SIZE: 9pt; WIDTH: 100px" readOnly type="text"
															columnKey="ItemName"></TD>
													<TD align="right">
														<asp:Label id="Label15" runat="server" Font-Size="9pt">의뢰원천</asp:Label></TD>
													<TD>
														<asp:DropDownList id=DropDownList1 runat="server" Width="102px" columnKey="ProductionRequestSource" DataSource="<%# ProSource %>" DataTextField="SmallClassificationName" DataValueField="SmallClassificationCode">
														</asp:DropDownList></TD>
												</TR>
												<TR>
													<TD align="right">
														<asp:Label id="Label13" runat="server" Font-Size="9pt">1차납기요구량</asp:Label></TD>
													<TD>
														<INPUT id="igtbl_TextBox_0_4" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
															onkeyup="return Process();" style="FONT-SIZE: 9pt; WIDTH: 100px; TEXT-ALIGN: right"
															type="text" value="0" columnKey="RequestQuantity1"></TD>
													<TD>
														<asp:Label id="Label16" runat="server" Font-Size="9pt">1차납기요구일</asp:Label></TD>
													<TD>
														<igsch:webdatechooser id="Webdatechooser1" runat="server" Height="20px" Width="100px" Font-Size="9pt"
															Text="Null" MaxDate="2076-06-06" NullDateLabel=" " columnKey="RequestDate1">
															<CALENDARLAYOUT MaxDate="2076-06-06" DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
																ShowTitle="False" ShowFooter="False">
																<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
																<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
															</CALENDARLAYOUT>
															<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
															<DROPBUTTON ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DROPBUTTON>
															<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
														</igsch:webdatechooser></TD>
													<TD>
														<asp:Label id="Label12" runat="server" Font-Size="9pt">2차납기요구량</asp:Label></TD>
													<TD>
														<INPUT id="igtbl_TextBox_0_5" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
															onkeyup="return Process();" style="FONT-SIZE: 9pt; WIDTH: 100px; TEXT-ALIGN: right"
															type="text" value="0" columnKey="RequestQuantity2"></TD>
													<TD>
														<asp:Label id="Label17" runat="server" Font-Size="9pt">2차납기요구일</asp:Label></TD>
													<TD>
														<igsch:webdatechooser id="Webdatechooser2" runat="server" Height="20px" Width="100px" Font-Size="9pt"
															Text="Null" MaxDate="2076-06-06" NullDateLabel=" " columnKey="RequestDate2">
															<CALENDARLAYOUT MaxDate="2076-06-06" DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
																ShowTitle="False" ShowFooter="False">
																<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
																<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
															</CALENDARLAYOUT>
															<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
															<DROPBUTTON ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DROPBUTTON>
															<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
														</igsch:webdatechooser></TD>
												</TR>
												<TR>
													<TD align="right">
														<asp:Label id="Label18" runat="server" Font-Size="9pt">3차납기요구량</asp:Label></TD>
													<TD>
														<INPUT id="igtbl_TextBox_0_6" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
															onkeyup="return Process();" style="FONT-SIZE: 9pt; WIDTH: 100px; TEXT-ALIGN: right"
															type="text" value="0" columnKey="RequestQuantity3"></TD>
													<TD>
														<asp:Label id="Label19" runat="server" Font-Size="9pt">3차납기요구일</asp:Label></TD>
													<TD>
														<igsch:webdatechooser id="Webdatechooser3" runat="server" Height="20px" Width="100px" Font-Size="9pt"
															Text="Null" MaxDate="2076-06-06" NullDateLabel=" " columnKey="RequestDate3">
															<CALENDARLAYOUT MaxDate="2076-06-06" DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
																ShowTitle="False" ShowFooter="False">
																<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
																<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
															</CALENDARLAYOUT>
															<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
															<DROPBUTTON ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DROPBUTTON>
															<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
														</igsch:webdatechooser></TD>
													<TD>
														<asp:Label id="Label20" runat="server" Font-Size="9pt">4차납기요구량</asp:Label></TD>
													<TD>
														<INPUT id="igtbl_TextBox_0_7" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
															onkeyup="return Process();" style="FONT-SIZE: 9pt; WIDTH: 100px; TEXT-ALIGN: right"
															type="text" value="0" columnKey="RequestQuantity4"></TD>
													<TD>
														<asp:Label id="Label21" runat="server" Font-Size="9pt">4차납기요구일</asp:Label></TD>
													<TD>
														<igsch:webdatechooser id="Webdatechooser4" runat="server" Height="20px" Width="100px" Font-Size="9pt"
															Text="Null" MaxDate="2076-06-06" NullDateLabel=" " columnKey="RequestDate4">
															<CALENDARLAYOUT MaxDate="2076-06-06" DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
																ShowTitle="False" ShowFooter="False">
																<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
																<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
															</CALENDARLAYOUT>
															<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
															<DROPBUTTON ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DROPBUTTON>
															<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
														</igsch:webdatechooser></TD>
												</TR>
												<TR>
													<TD align="right">
														<asp:Label id="Label11" runat="server" Font-Size="9pt">5차납기요구량</asp:Label></TD>
													<TD>
														<INPUT id="igtbl_TextBox_0_8" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
															onkeyup="return Process();" style="FONT-SIZE: 9pt; WIDTH: 100px; TEXT-ALIGN: right"
															type="text" value="0" columnKey="RequestQuantity5"></TD>
													<TD>
														<asp:Label id="Label14" runat="server" Font-Size="9pt">5차납기요구일</asp:Label></TD>
													<TD>
														<igsch:webdatechooser id="Webdatechooser5" runat="server" Height="20px" Width="100px" Font-Size="9pt"
															Text="Null" MaxDate="2076-06-06" NullDateLabel=" " columnKey="RequestDate5">
															<CALENDARLAYOUT MaxDate="2076-06-06" DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
																ShowTitle="False" ShowFooter="False">
																<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
																<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
															</CALENDARLAYOUT>
															<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
															<DROPBUTTON ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DROPBUTTON>
															<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
														</igsch:webdatechooser></TD>
													<TD align="right">
														<asp:Label id="Label22" runat="server" Font-Size="9pt">총의뢰량</asp:Label></TD>
													<TD>
														<INPUT id="igtbl_TextBox_0_9" style="FONT-SIZE: 9pt; WIDTH: 100px; TEXT-ALIGN: right" type="text"
															value="0" columnKey="ProductionRequestQuantity"></TD>
													<TD><FONT face="굴림"></FONT></TD>
													<TD></TD>
												</TR>
											</TABLE>
											<P><FONT face="굴림"></FONT>&nbsp;</P>
											<P><FONT face="굴림"></FONT>&nbsp;</P>
											<P><FONT face="굴림"></FONT>&nbsp;</P>
											<P><FONT face="굴림"></FONT>&nbsp;</P>
											<P align="center"><BR>
												&nbsp; <INPUT id="igtbl_reOkBtn" style="WIDTH: 50px" onclick="igtbl_gRowEditButtonClick(event);__doPostBack('LinkButton1','');"
													type="button" value="수정">&nbsp; <INPUT id="igtbl_reCancelBtn" style="WIDTH: 50px" onclick="igtbl_gRowEditButtonClick(event);"
													type="button" value="취소"></P>
										</RowEditTemplate>
									</igtbl:UltraGridBand>
								</Bands>
							</igtbl:ultrawebgrid></TD>
					</TR>
					<TR>
						<TD align="left" height="30">
							<asp:button id="bt_Excel" runat="server" Height="20px" Width="60px" Text="Excel"></asp:button></TD>
						<TD align="right" height="30"><INPUT id="lb_Source" style="FONT-SIZE: 9pt; WIDTH: 50px; HEIGHT: 20px" type="hidden" size="3"
								name="Hidden1" runat="server"><INPUT id="lb_SourceCode" style="FONT-SIZE: 9pt; WIDTH: 50px; HEIGHT: 20px" type="hidden"
								size="3" name="Hidden1" runat="server"><INPUT id="lb_Total" style="FONT-SIZE: 9pt; WIDTH: 50px; HEIGHT: 20px; BACKGROUND-COLOR: #eeeee9"
								type="hidden" size="3" value="0" runat="server"><INPUT id="chkAll" style="FONT-SIZE: 9pt; WIDTH: 50px; HEIGHT: 20px; BACKGROUND-COLOR: #eeeee9"
								type="hidden" value="true">
							<asp:button id="bt_Cancel" runat="server" Height="20px" Width="60px" Text="취  소" Enabled="False"></asp:button>&nbsp;
							<asp:button id="bt_Delete" runat="server" Height="20px" Width="60px" Text="삭  제"></asp:button>&nbsp;
							<asp:button id="bt_Stop" runat="server" Height="20px" Width="60px" Text="중  단"></asp:button></TD>
					</TR>
				</TABLE>
			</FIELDSET>
			</TD></TR></TBODY></TABLE>
			<igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter></form>
	</body>
</HTML>
