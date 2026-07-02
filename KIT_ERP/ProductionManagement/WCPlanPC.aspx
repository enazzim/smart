<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v1.1, Version=1.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="WCPlanPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.WCPlanPC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>WCPlanPC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../LinkLine.css" type="text/css" rel="stylesheet">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--
		function ResettxtBox()
		{
			
			ResetTextBox();
			var objChooser1 = igdrp_getComboById("wdcFromDate");
			var objChooser2 = igdrp_getComboById("wdcToDate");
			var objChooser3 = igdrp_getComboById("wdcDeliveryFromDate");
			var objChooser4 = igdrp_getComboById("wdcDeliveryToDate");
			var objChooser5 = igdrp_getComboById("wdcBeginDate");
			var objChooser6 = igdrp_getComboById("wdcEndDate");
			objChooser1.setValue(null);
			objChooser2.setValue(null);
			objChooser3.setValue(null);
			objChooser4.setValue(null);
			objChooser5.setValue(null);
			objChooser6.setValue(null);
			document.Form1.dlProgress.options[0].selected=true;
			document.Form1.ddlItemGroup1.options[0].selected=true;
			document.Form1.ddlItemGroup2.options[0].selected=true;
			
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
		function UltraWebGrid1_ColumnHeaderClickHandler(gridName, columnId, button){
			//Add code to handle your event here.
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
		
		
		function UltraWebGrid1_BeforeRowTemplateOpenHandler(gridName, rowId, templateId){
			//Add code to handle your event here.
			//먼저작업일, 납기요구일, 작업지시일
			var row = igtbl_getRowById(rowId);
	
			if((row.getCellFromKey("ProgressCondition").getValue() == "대기") || (row.getCellFromKey("ProgressCondition").getValue() == "지시") || (row.getCellFromKey("ProgressCondition").getValue() == "진행"))
			{
				var workdate = igedit_getById("UltraWebGrid1__ctl0_WorkDate", "igtxtUltraWebGrid1__ctl0_WorkDate");
				var deliverydate = igedit_getById("UltraWebGrid1__ctl0_DeliveryDate", "igtxtUltraWebGrid1__ctl0_DeliveryDate");
				var workCompletedate = igedit_getById("UltraWebGrid1__ctl0_WorkCompleteDate", "igtxtUltraWebGrid1__ctl0_WorkCompleteDate");
				
				workCompletedate.setValue(row.getCellFromKey("WorkCompleteDate").getValue());
				deliverydate.setValue(row.getCellFromKey("DeliveryDate").getValue());
				workdate.setValue(row.getCellFromKey("WorkDate").getValue());
				document.Form1.lb_Index.value = rowId;
				
				
				
				//자가외주
				for(var i=0;i<document.Form1.UltraWebGrid1__ctl0_DropDownList1.options.length;i++)
				{
					if(document.Form1.UltraWebGrid1__ctl0_DropDownList1.options[i].innerText == row.getCellFromKey("WorkDistinction").getValue())
					{
						document.Form1.UltraWebGrid1__ctl0_DropDownList1.options[i].selected=true;
						break;
					}
					else
						document.Form1.UltraWebGrid1__ctl0_DropDownList1.options[0].selected=true;
				}
				
			}
			else
			{
				alert("수정할 수 없습니다.!");
				return true;		
			}
			
		}
		function UltraWebGrid1_AfterRowTemplateCloseHandler(gridName, rowId, bSaveChanges){
			if(event.srcElement.id == "igtbl_reOkBtn")
			{
				var row = igtbl_getRowById(rowId);
				var workdate = igedit_getById("UltraWebGrid1__ctl0_WorkDate", "igtxtUltraWebGrid1__ctl0_WorkDate");
				var deliverydate = igedit_getById("UltraWebGrid1__ctl0_DeliveryDate", "igtxtUltraWebGrid1__ctl0_DeliveryDate");
				var workCompletedate = igedit_getById("UltraWebGrid1__ctl0_WorkCompleteDate", "igtxtUltraWebGrid1__ctl0_WorkCompleteDate");
				
				row.getCellFromKey("WorkDate").setValue(workdate.getValue());		
				row.getCellFromKey("DeliveryDate").setValue(deliverydate.getValue());
				row.getCellFromKey("WorkCompleteDate").setValue(workCompletedate.getValue());
				//document.Form1.lb_Index.value = rowId;
				
			}
		}
		
		
		
		
//////////////////////////////////////////////////////////////////////////////////////////
//					Currency															//
//////////////////////////////////////////////////////////////////////////////////////////
		function OnFocus_Obj(obj) {
						obj.select();
		}
		
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
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table10" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" cellSpacing="0"
				cellPadding="0" width="800" align="center" border="0">
				<TR>
					<TD vAlign="top" align="center" width="20"></TD>
					<TD vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 805px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" style="WIDTH: 800px; HEIGHT: 40px" cellSpacing="0" cellPadding="0" width="800"
								align="center" border="0">
								<TR>
									<TD vAlign="middle" align="left" width="600" colSpan="6" height="30"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol>
										<igtblexp:UltraWebGridExcelExporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:UltraWebGridExcelExporter></TD>
									<td align="right" width="70">진행상태&nbsp;</td>
									<td width="130"><asp:dropdownlist id="dlProgress" runat="server" Width="80px">
											<asp:ListItem Value=" ">-선 택-</asp:ListItem>
											<asp:ListItem Value="대기">대기</asp:ListItem>
											<asp:ListItem Value="지시">지시</asp:ListItem>
											<asp:ListItem Value="진행">진행</asp:ListItem>
											<asp:ListItem Value="중단">중단</asp:ListItem>
											<asp:ListItem Value="완료">완료</asp:ListItem>
										</asp:dropdownlist></td>
								</TR>
								<tr>
									<td align="left" colSpan="8">
										<table cellSpacing="0" cellPadding="0" border="0">
											<TR>
												<TD vAlign="middle" align="right" height="30" width="65"><FONT face="굴림">작업완료일</FONT>&nbsp;
												</TD>
												<TD vAlign="middle" align="left" height="30">
													<igsch:webdatechooser id="wdcBeginDate" runat="server" Width="100px" Height="18px" Text=" " Font-Size="10pt"
														BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" NullDateLabel=" ">
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
												<TD vAlign="middle" align="center" width="10" height="30">~</TD>
												<TD vAlign="middle" align="left" height="30"><FONT face="굴림">
														<igsch:webdatechooser id="wdcEndDate" runat="server" Width="100px" Height="18px" Text=" " Font-Size="10pt"
															BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" NullDateLabel=" ">
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
												<TD vAlign="middle" align="right" width="65" height="30"><FONT face="굴림">작업시작일&nbsp; </FONT>
												</TD>
												<TD vAlign="middle" align="right" height="30"><FONT face="굴림"><igsch:webdatechooser id="wdcFromDate" runat="server" Width="100px" NullDateLabel=" " BorderColor="DimGray"
															BorderStyle="Solid" BackColor="#EEEEE9" Font-Size="10pt" Text=" " Height="18px">
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
												<TD vAlign="middle" align="center" width="10" height="30">~</TD>
												<TD vAlign="middle" align="left" height="30"><igsch:webdatechooser id="wdcToDate" runat="server" Width="100px" NullDateLabel=" " BorderColor="DimGray"
														BorderStyle="Solid" BackColor="#EEEEE9" Font-Size="10pt" Text=" " Height="18px">
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
												<td align="right" width="60">&nbsp;</td>
												<td><FONT face="굴림"></FONT></td>
											</TR>
										</table>
									</td>
								</tr>
								<TR>
									<TD style="HEIGHT: 19px" vAlign="middle" align="right" width="800" colSpan="8">
										<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="800" border="0">
											<TR>
												<TD align="right" width="65" height="20"><FONT face="굴림">품목분류1&nbsp;</FONT></TD>
												<TD align="left" width="100" height="20"><asp:dropdownlist id="ddlItemGroup1" runat="server"></asp:dropdownlist></TD>
												<TD align="right" width="65" height="20"><FONT face="굴림">품목분류2&nbsp;</FONT></TD>
												<TD align="left" width="100" height="20"><asp:dropdownlist id="ddlItemGroup2" runat="server"></asp:dropdownlist></TD>
												<TD vAlign="middle" align="right" height="30" width="70">납기요구일&nbsp;
												</TD>
												<TD vAlign="middle" align="left" height="30" width="100">
													<igsch:webdatechooser id="wdcDeliveryFromDate" runat="server" Width="100px" Height="18px" Text=" " Font-Size="10pt"
														BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" NullDateLabel=" ">
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
												<TD vAlign="middle" align="center" width="10" height="30">~</TD>
												<TD vAlign="middle" align="left" height="30" width="100">
													<igsch:webdatechooser id="wdcDeliveryToDate" runat="server" Width="100px" Height="18px" Text=" " Font-Size="10pt"
														BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" NullDateLabel=" ">
														<EditStyle>
															<BorderDetails ColorTop="DimGray" StyleBottom="Solid" ColorBottom="DimGray" ColorRight="DimGray"
																StyleTop="Solid" StyleRight="Solid" StyleLeft="Solid" ColorLeft="DimGray"></BorderDetails>
														</EditStyle>
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
												<TD align="right" height="20" width="195"><FONT face="굴림"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
															type="button" value="초기화" name="btnReset">&nbsp;
														<asp:button id="bt_Search" runat="server" Width="60px" Font-Size="9pt" Text="검   색" Height="20px"
															CommandName="Search"></asp:button>&nbsp; </FONT>
												</TD>
											</TR>
										</TABLE>
										&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" width="800" colSpan="8" height="5"></TD>
								</TR>
							</TABLE>
						</FIELDSET>
						<BR>
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[검색결과]</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
								<TR>
									<TD style="HEIGHT: 24px" align="right" colSpan="2"><INPUT id="Volum" style="WIDTH: 26px; HEIGHT: 12px" type="hidden" size="1" value="0" name="Hidden1"
											runat="server"><INPUT id="lb_Index" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1"
											runat="server"><asp:button id="btnPre" runat="server" Width="16px" Font-Size="8pt" Text="◀" Height="16px" CommandName="Video"
											CommandArgument="Pre"></asp:button><asp:button id="btnNow" runat="server" Width="16px" Font-Size="8pt" Text="■" Height="16px" CommandName="Video"
											CommandArgument="Now"></asp:button><asp:button id="btnNext" runat="server" Width="16px" Font-Size="8pt" Text="▶" Height="16px"
											CommandName="Video" CommandArgument="Next"></asp:button>&nbsp;&nbsp;
									</TD>
								</TR>
								<TR>
									<TD vAlign="top" align="center" colSpan="2"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="406px">
<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" RowHeightDefault="20px" Version="3.00" BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect" AllowUpdateDefault="RowTemplateOnly">

<AddNewBox>

<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
</Style>

</AddNewBox>

<Pager PageSize="17" StyleMode="ComboBox" AllowPaging="True">

<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
</Style>

</Pager>

<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray" Height="25px">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

</HeaderStyleDefault>

<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderColor="DimGray" BorderStyle="Solid" BackColor="Silver" Height="406px">
</FrameStyle>

<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

</FooterStyleDefault>

<ClientSideEvents ColumnHeaderClickHandler="UltraWebGrid1_ColumnHeaderClickHandler" BeforeRowTemplateOpenHandler="UltraWebGrid1_BeforeRowTemplateOpenHandler" AfterRowTemplateCloseHandler="UltraWebGrid1_AfterRowTemplateCloseHandler">
</ClientSideEvents>

<RowAlternateStyleDefault BackColor="LightSteelBlue">
</RowAlternateStyleDefault>

<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">

<Padding Left="3px">
</Padding>

<BorderDetails WidthLeft="0px" WidthTop="0px">
</BorderDetails>

</RowStyleDefault>

</DisplayLayout>

<Bands>
<igtbl:UltraGridBand>
<Columns>
<igtbl:UltraGridColumn HeaderText="선택" Key="chk" Width="35px" Type="CheckBox" BaseColumnName="chk" AllowUpdate="Yes">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
<SelectedCellStyle HorizontalAlign="Center">
</SelectedCellStyle>

<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="ItemDrawNum">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="공정순서" Key="ProcessSequenceNum" Width="60px" HeaderClickAction="SortMulti" BaseColumnName="ProcessSequenceNum">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="공정코드" Key="ProcessCode" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="ProcessCode">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="공정명" Key="ProcessName" HeaderClickAction="SortMulti" BaseColumnName="ProcessName">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="규 격" Key="Standard" Width="150px" BaseColumnName="Standard"></igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="기타정보" Key="EtcText" BaseColumnName="EtcText"></igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="제품번호" Key="ProductItemNum" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="ProductItemNum">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="제품도면번호" Key="ProductDrawNum" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="ProductDrawNum">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="제품명" Key="ProductName" HeaderClickAction="SortMulti" BaseColumnName="ProductName">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="작업지시일" Key="WorkDenotationDate" Hidden="True" BaseColumnName="WorkDenotationDate"></igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="작업완료일" Key="WorkCompleteDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti" BaseColumnName="WorkCompleteDate">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="납기요구일" Key="DeliveryDate" Format="yyyy-MM-dd" BaseColumnName="DeliveryDate"></igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="작업시작일" Key="WorkDate" Format="yyyy-MM-dd" BaseColumnName="WorkDate"></igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="모품목번호" Key="ParentItemNum" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="ParentItemNum">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="모품목도면번호" Key="ParentDrawNum" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="ParentDrawNum">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="모품목명" Key="ParentName" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="ParentName">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="작업장명" Key="WCName" HeaderClickAction="SortMulti" BaseColumnName="WCName">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="작업구분" Key="WorkDistinction" HeaderClickAction="SortMulti" BaseColumnName="WorkDistinction">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="작업계획수량" Key="WorkPlanQuantity" Format="###,###,###" HeaderClickAction="SortMulti" BaseColumnName="WorkPlanQuantity">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="작업완료수량" Key="WorkCompletionQuantity" Format="###,###,###" HeaderClickAction="SortMulti" BaseColumnName="WorkCompletionQuantity">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="외주리드타임" Key="OrderLeadTime" Hidden="True" Format="#########" HeaderClickAction="SortMulti" BaseColumnName="OrderLeadTime">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" HeaderClickAction="SortMulti" BaseColumnName="ProgressCondition">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPerson">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti" BaseColumnName="RegistrationDate">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" HeaderClickAction="SortMulti" BaseColumnName="UpdatingPerson">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti" BaseColumnName="UpdatingDate">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="생산계획번호" Key="ProductionPlanHistoryIndex" Format="#########" HeaderClickAction="SortSingle" BaseColumnName="ProductionPlanHistoryIndex">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="WC작업계획번호" Key="WCDailyWorkPlanHistoryIndex" Format="#########" HeaderClickAction="SortMulti" BaseColumnName="WCDailyWorkPlanHistoryIndex">
<CellStyle VerticalAlign="Middle" HorizontalAlign="Center">
</CellStyle>

<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center">
</HeaderStyle>
</igtbl:UltraGridColumn>
</Columns>

<RowTemplateStyle BorderColor="Silver" BorderStyle="Ridge" BackColor="Silver">

<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px">
</BorderDetails>

</RowTemplateStyle>

<RowEditTemplate>
														<TABLE id="Table1" style="FONT-WEIGHT: normal; COLOR: buttontext" cellSpacing="2" cellPadding="0"
															bgColor="whitesmoke" border="0">
															<TR>
																<TD align="right" width="110" bgColor="gainsboro" height="20">품목 번호&nbsp;</TD>
																<TD width="110" height="20"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
																		disabled readOnly type="text" size="12" name="ItemNum" columnkey="ItemNum"></TD>
																<TD align="right" width="90" bgColor="gainsboro" height="20">품목명&nbsp;</TD>
																<TD width="110" height="20"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
																		disabled readOnly type="text" size="12" name="ItemName" columnkey="ItemName"></TD>
																<TD align="right" width="110" bgColor="gainsboro" height="20">도면 번호&nbsp;</TD>
																<TD width="110" height="20"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; COLOR: #000033; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
																		disabled readOnly type="text" size="12" name="ItemDrawNum" columnkey="ItemDrawNum"></TD>
															</TR>
															<TR>
																<TD align="right" bgColor="#dcdcdc" height="20">공정순서&nbsp;</TD>
																<TD width="10" height="20"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
																		disabled readOnly type="text" size="12" columnKey="ProcessSequenceNum"></TD>
																<TD align="right" bgColor="#dcdcdc" height="20">공정명&nbsp;</TD>
																<TD height="20"><INPUT style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
																		disabled readOnly type="text" size="12" columnKey="ProcessName"></TD>
																<TD align="right" bgColor="#dcdcdc" height="20">작업장명&nbsp;</TD>
																<TD height="20">
																	<asp:DropDownList id=dl_WCName runat="server" Width="110px" Font-Size="8pt" columnkey="WCName" DataSource="<%# WCName %>" DataTextField="WCName" DataValueField="WCName">
																	</asp:DropDownList></TD>
															<TR>
																<TD align="right" bgColor="#dcdcdc" height="20">작업계획수량&nbsp;</TD>
																<TD width="10" height="20"><INPUT id="Text1" onkeydown="OnKeyDown_Currency(this)" onblur="OnBlur_Cur(this);" onkeyup="OnKeyUp_Currency(this)"
																		style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																		onfocus="OnFocus_Obj(this);" type="text" size="12" name="Text1" runat="server" columnKey="WorkPlanQuantity"></TD>
																<TD align="right" bgColor="#dcdcdc" height="20">작업구분&nbsp;</TD>
																<TD height="20">
																	<asp:DropDownList id="DropDownList1" runat="server" Width="110px" Font-Size="8pt" columnkey="WorkDistinction">
																		<asp:ListItem Value="자가">자가</asp:ListItem>
																		<asp:ListItem Value="외주">외주</asp:ListItem>
																	</asp:DropDownList></TD>
																<TD align="right" bgColor="#dcdcdc" height="20">외주리드타임&nbsp;</TD>
																<TD height="20"><INPUT id="Text2" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 110px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
																		type="text" size="12" name="Text2" runat="server" columnKey="OrderLeadTime"></TD>
															</TR>
															<TR>
																<TD align="right" bgColor="#dcdcdc" height="20">작업완료일&nbsp;</TD>
																<TD width="10" height="20">
																	<igtxt:WebDateTimeEdit id="WorkCompleteDate" runat="server" Width="105px" BorderColor="DimGray" BorderStyle="Solid" 
 Height="20px" columnKey="WorkCompleteDate" HorizontalAlign="Center" DisplayModeFormat="d" UseBrowserDefaults="False" 
 BorderWidth="1px">
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
																<TD align="right" bgColor="#dcdcdc" height="20">납기요구일&nbsp;</TD>
																<TD width="10" height="20">
																	<igtxt:WebDateTimeEdit id="DeliveryDate" runat="server" Width="105px" BorderColor="DimGray" BorderStyle="Solid" 
 Height="20px" columnKey="DeliveryDate" HorizontalAlign="Center" DisplayModeFormat="d" UseBrowserDefaults="False" 
 BorderWidth="1px">
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
																<TD align="right" bgColor="#dcdcdc" height="20">작업시작일&nbsp;</TD>
																<TD height="20">
																	<igtxt:WebDateTimeEdit id="WorkDate" runat="server" Width="105px" BorderColor="DimGray" BorderStyle="Solid" 
 Height="20px" columnKey="WorkDate" HorizontalAlign="Center" DisplayModeFormat="d" UseBrowserDefaults="False" 
 BorderWidth="1px">
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
															<TR>
																<TD align="center" colSpan="6"><INPUT id="igtbl_reOkBtn" style="WIDTH: 50px" onclick="igtbl_gRowEditButtonClick(event);__doPostBack('lnk_Update','');"
																		type="button" value="OK">&nbsp; <INPUT id="igtbl_reCancelBtn" style="WIDTH: 50px" onclick="igtbl_gRowEditButtonClick(event);"
																		type="button" value="Cancel">
																</TD>
															</TR>
														</TABLE>
													
</RowEditTemplate>
</igtbl:UltraGridBand>
</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 234px" vAlign="middle" align="left" height="30"><FONT face="굴림">&nbsp;
											<asp:button id="btExcel" runat="server" Width="60px" Font-Size="10pt" Text="Excel" Height="20px"></asp:button>&nbsp;
											<asp:button id="btCommit" runat="server" Width="60px" Font-Size="10pt" Text="완    료" Height="20px"></asp:button>&nbsp;
											<asp:button id="btCommitCancel" runat="server" Width="60px" Font-Size="10pt" Text="완료취소" Height="20px"></asp:button>&nbsp;</FONT></TD>
									<TD style="WIDTH: 600px" vAlign="middle" align="right" height="30">
										<asp:button id="btAdd" runat="server" Width="65px" Font-Size="10pt" Text="추   가" Height="20px"></asp:button>&nbsp;
										<asp:button id="btChangeInOut" runat="server" Width="65px" Font-Size="10pt" Text="작업 변경" Height="20px"></asp:button>&nbsp;&nbsp;
										<asp:button id="btCancle" runat="server" Width="60px" Font-Size="10pt" Text="취    소" Height="20px"
											Enabled="False"></asp:button>&nbsp;
										<asp:button id="btDelete" runat="server" Width="65px" Font-Size="10pt" Text="삭    제" Height="20px"></asp:button>&nbsp;
										<asp:button id="btStop" runat="server" Width="65px" Font-Size="10pt" Text="중   단" Height="20px"></asp:button>&nbsp;
										<asp:button id="btOrderCancel" runat="server" Width="96px" Font-Size="10pt" Text="작업지시취소" Height="20px"></asp:button>&nbsp;&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="chkAll" style="Z-INDEX: 102; LEFT: 832px; WIDTH: 56px; POSITION: absolute; TOP: 600px; HEIGHT: 22px; BACKGROUND-COLOR: #eeeee9"
				type="hidden" size="4" value="true" name="chkAll" runat="server">
			<asp:linkbutton id="lnk_Update" style="Z-INDEX: 103; LEFT: 840px; POSITION: absolute; TOP: 568px"
				runat="server" Visible="False">LinkButton</asp:linkbutton></form>
	</body>
</HTML>
