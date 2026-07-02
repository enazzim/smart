<%@ Page language="c#" Codebehind="WorkDailyReportRegistration.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.WorkDailyReportRegistration" codePage="949" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>WorkDailyReportRegistration</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--
		
		var complete;
		function Lot(inputValue)
        {
			document.Form1.hdLotNum.value = inputValue;
			document.Form1.Textbox5.value = inputValue;
			
		}
		function SubmitLinkButton()
        {
			__doPostBack('lnk_Update','');
		}

		function UltraWebGrid2_DblClickHandler(gridName, cellId){
			var objGrid = igtbl_getActiveRow(gridName)//현재 선택되어진 객체의 행의 가져옴
			var row = igtbl_getRowById(cellId);
			
			if(row.getCellFromKey("ProductItemNum").getValue() == null || row.getCellFromKey("ProductItemNum").getValue() == "null")
			{
				document.Form1.tb_ProductNum.value = "";
				document.Form1.tb_ProductDrawNum.value = "";
				document.Form1.tb_ProductName.value = "";
			}
			else
			{
				document.Form1.tb_ProductNum.value = row.getCellFromKey("ProductItemNum").getValue();
				document.Form1.tb_ProductDrawNum.value = row.getCellFromKey("ProductDrawNum").getValue();
				document.Form1.tb_ProductName.value = row.getCellFromKey("ProductName").getValue();
			}
			document.Form1.tb_ParentItemNum.value = row.getCellFromKey("ParentItemNum").getValue();
			document.Form1.tb_ParentDrawNum.value = row.getCellFromKey("ParentDrawNum").getValue();
			document.Form1.tb_ParentName.value = row.getCellFromKey("ParentName").getValue();
			document.Form1.tb_ItemNum.value = row.getCellFromKey("ItemNum").getValue();
			document.Form1.tb_ItemDrawNum.value = row.getCellFromKey("ItemDrawNum").getValue();
			document.Form1.tb_ItemName.value = row.getCellFromKey("ItemName").getValue();
			document.Form1.tb_ProcessName.value = row.getCellFromKey("ProcessName").getValue();//공정명
			document.Form1.tb_ProcessSequenceNum.value = row.getCellFromKey("ProcessSequenceNum").getValue();//공정순서번호
			document.Form1.tb_ProcessCode.value = row.getCellFromKey("ProcessCode").getValue();//공정코드
			document.Form1.tb_WorkPlanQuantity.value = row.getCellFromKey("WorkPlanQuantity").getValue();//작업계획량
			document.Form1.tb_WorkCompletionQuantity.value = row.getCellFromKey("WorkCompletionQuantity").getValue();//이전작업완료량
			document.Form1.tb_WorkPlanQuantity.value = row.getCellFromKey("WorkPlanQuantity").getValue();//작업계획량
			document.Form1.tb_ProductionPlanHistoryIndex.value = row.getCellFromKey("ProductionPlanHistoryIndex").getValue();//생산계획원장번호
			document.Form1.tb_WorkPlanHistoryIndex.value = row.getCellFromKey("WorkPlanHistoryIndex").getValue();//작업계획원장번호
			document.Form1.tb_WCDailyWorkPlanHistoryIndex.value = row.getCellFromKey("WCDailyWorkPlanHistoryIndex").getValue();//WC작업계획원장번호
			document.Form1.tb_RemainQuantity.value = document.Form1.tb_WorkPlanQuantity.value - document.Form1.tb_WorkCompletionQuantity.value;
			complete = row.getCellFromKey("WorkCompletionQuantity").getValue();//작업완료량
			
			document.Form1.TextBox1.value = "08";
			document.Form1.TextBox2.value = "10";
			document.Form1.TextBox3.value = "20";
			document.Form1.TextBox4.value = "00";
			
			document.Form1.tb_WorkCompletionQuantity1.focus();
		}
		
		
		function Process1()	
		{
			
			if(document.Form1.TextBox1.value >= 24)
			{
				alert('0-23까지만 가능합니다!');
				document.Form1.TextBox1.value = 0;
				document.Form1.TextBox1.focus();
			}
				
		}
		
		function Process2()	
		{
			
			if(document.Form1.TextBox2.value >= 60)
			{
				alert('0-59까지만 가능합니다!');
				document.Form1.TextBox2.value = 0;
				document.Form1.TextBox2.focus();
			}
				
		}
		
		function Process3()	
		{
			
			if(document.Form1.TextBox3.value >= 24)
			{
				alert('0-23까지만 가능합니다!');
				document.Form1.TextBox3.value = 0;
				document.Form1.TextBox3.focus();
			}
				
		}
		
		function Process4()	
		{
			
			if(document.Form1.TextBox4.value >= 60)
			{
				alert('0-59까지만 가능합니다!');
				document.Form1.TextBox4.value = 0;
				document.Form1.TextBox4.focus();
			}
				
		}
		
		function OnlyNum(obj)
		{
			if (event.ctrlKey || event.shiftKey || event.altKey)   {
				SetEventPass();
				return true;
			}
			var sFieldValue = new String(obj.value);
			// 숫자검사 + Numeric Keypad 숫자검사
			if( (event.keyCode<45 || event.keyCode>57) && (event.keyCode<96 || event.keyCode>105))  {
    			if( !IsNumKey() ) {
	    			SetEventCancel();
					return obj.value;
				}	    
			}
			else if( sFieldValue.substr(0,2) == "00" && (event.keyCode == 48 || event.keyCode == 96) ) {
						SetEventCancel();
						return obj.value;
					}
			SetEventPass();
			return true;
		}
		
		function IsNumKey()  {
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
				event.keyCode == 109 
				)
    			return true;
			else
    			return false;
		}
		
		function Process()	
		{
			var Remain = Number(document.Form1.tb_WorkPlanQuantity.value) - (Number(document.Form1.tb_WorkCompletionQuantity.value) +Number(document.Form1.tb_WorkCompletionQuantity1.value) );
			/*
			if(Remain < 0 )	
			{
				if(confirm("전체 완료수량이 계획수량보다 많습니다!! 그래도 등록하시겠습니까?")) 
				{
						return true;
				}
				else
				{
					document.Form1.tb_WorkCompletionQuantity1.value = Number(document.Form1.tb_WorkPlanQuantity.value) -  Number(document.Form1.tb_WorkCompletionQuantity.value);
					document.Form1.tb_RemainQuantity.value = 0;
					document.Form1.tb_WorkCompletionQuantity1.focus();
						return false;
				}			
			}
			else
			{
				document.Form1.tb_RemainQuantity.value = Remain;
			}
			*/
			document.Form1.tb_RemainQuantity.value = Remain;
			var Quantity1 = Number(document.Form1.tb_WorkCompletionQuantity1.value) - Number(document.Form1.tb_SuitabilityQuantity.value);
			if(Quantity1 < 0)
			{
				alert("- 적합수량이 완료수량보다 많습니다!");
				document.Form1.tb_SuitabilityQuantity.value = 0;
				document.Form1.tb_WorkCompletionQuantity1.focus();
			}
			else
			{
				document.Form1.tb_UnSuittabilityQuantity.value = Quantity1;				
			}
		}
		function Suitability()
		{
			var Quantity = Number(document.Form1.tb_WorkCompletionQuantity1.value) - Number(document.Form1.tb_SuitabilityQuantity.value);
			if(Quantity < 0)
			{
				alert("- 적합수량이 완료수량보다 많습니다!");
				document.Form1.tb_SuitabilityQuantity.value = 0;
				document.Form1.tb_SuitabilityQuantity.focus();
			}
			else
			{
				document.Form1.tb_UnSuittabilityQuantity.value = Quantity;				
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
		
		
					
					
--></SCRIPT>
</HEAD>
	<body bgColor="#f7f6f6" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="LEFT: 10px; WIDTH: 800px; POSITION: absolute; TOP: 10px" cellSpacing="0"
				cellPadding="0" width="800" border="0">
				<TR>
					<TD style="HEIGHT: 520px" width="20"></TD>
					<TD style="FONT-SIZE: 0pt; PADDING-TOP: 10px" vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD vAlign="middle" align="left" colSpan="8" height="30"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" width="70" height="30">작업일&nbsp;</TD>
									<TD vAlign="middle" align="left" width="100" height="30"><igsch:webdatechooser id="wdc_FromDate" runat="server" Font-Size="10pt" Height="18px" Width="100px" NullDateLabel=" "
											Text=" " BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray">
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
									<TD vAlign="middle" align="center" width="15" height="30">~</TD>
									<TD vAlign="middle" align="left" width="100" height="30"><igsch:webdatechooser id="wdc_ToDate" runat="server" Font-Size="10pt" Height="18px" Width="100px" NullDateLabel=" "
											Text=" " BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray">
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
									<TD vAlign="middle" align="right" width="70" height="30">WC명&nbsp;</TD>
									<TD vAlign="middle" align="center" width="125" height="30"><asp:dropdownlist id="dlWCName" tabIndex="21" runat="server" Font-Size="10pt" Height="20px" Width="120px"
											BackColor="#EEEEE9"></asp:dropdownlist></TD>
									<TD vAlign="middle" align="right" width="320" colSpan="2" height="30"><asp:button id="Button5" tabIndex="22" runat="server" Font-Size="9pt" Height="20px" Width="60px"
											Text="초기화"></asp:button>&nbsp;
										<asp:button id="Button1" runat="server" Font-Size="9pt" Height="20px" Width="60px" Text="검   색"></asp:button>&nbsp;&nbsp;</TD>
								</TR>
								<TR>
									<TD align="right" colSpan="8" height="8"></TD>
								</TR>
							</TABLE>
						</FIELDSET>
						<br>
						<br>
						<br>
						<br>
						<br>
						<br>
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[검색결과]</LEGEND>
							<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD vAlign="top" align="center"><igtbl:ultrawebgrid id="UltraWebGrid2" runat="server" Height="150px" Width="100%">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" RowHeightDefault="20px" Version="3.00"
												SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti"
												BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid2"
												TableLayout="Fixed" CellClickActionDefault="RowSelect" NoDataMessage="해당 데이타가 없습니다.">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</AddNewBox>
												<Pager PageSize="5" StyleMode="ComboBox" AllowPaging="True">
													<Style VerticalAlign="Middle" BorderWidth="1px" BorderStyle="Solid" HorizontalAlign="Center"
														BackColor="LightGray">
													</Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" Font-Size="9pt" Font-Names="굴림" BorderColor="Black" BorderStyle="Solid"
													ForeColor="Black" BackColor="LightGray" Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<GroupByRowStyleDefault ForeColor="Black" BackColor="#95B9F0"></GroupByRowStyleDefault>
												<RowSelectorStyleDefault BorderStyle="Solid"></RowSelectorStyleDefault>
												<FrameStyle Width="100%" Cursor="Default" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림"
													BorderColor="DimGray" BorderStyle="Solid" BackColor="Silver" Height="150px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents DblClickHandler="UltraWebGrid2_DblClickHandler"></ClientSideEvents>
												<GroupByBox>
													<Style BackColor="#B5CFF7">
													</Style>
													<BandLabelStyle ForeColor="White" BackColor="#6372D4"></BandLabelStyle>
												</GroupByBox>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedGroupByRowStyleDefault BorderColor="Yellow" BackColor="Yellow"></SelectedGroupByRowStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" Font-Bold="True" ForeColor="Linen" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="Gray"
													BorderStyle="Solid" ForeColor="Black" BackColor="#EBEEF6">
													<Padding Left="3px"></Padding>
													<BorderDetails ColorTop="Gray" WidthLeft="0px" WidthTop="0px" ColorLeft="Gray"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand AllowUpdate="No">
													<Columns>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="130px" BaseColumnName="ItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Width="130px" BaseColumnName="ItemDrawNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="145px" BaseColumnName="ItemName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="순서" Key="ProcessSequenceNum" Width="50px" BaseColumnName="ProcessSequenceNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정코드" Key="ProcessCode" Hidden="True" BaseColumnName="ProcessCode">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정명" Key="ProcessName" Width="80px" BaseColumnName="ProcessName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="이전공정" Key="PreProcessName" Hidden="True" BaseColumnName="PreProcessName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="제품번호" Key="ProductItemNum" Hidden="True" BaseColumnName="ProductItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="제품도면번호" Key="ProductDrawNum" Hidden="True" BaseColumnName="ProductDrawNum">
															<CellStyle VerticalAlign="Top" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="제품명" Key="ProductName" BaseColumnName="ProductName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="모품목번호" Key="ParentItemNum" Hidden="True" BaseColumnName="ParentItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="모품목도면번호" Key="ParentDrawNum" Hidden="True" BaseColumnName="ParentDrawNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="모품목명" Key="ParentName" Hidden="True" BaseColumnName="ParentName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="WC명" Key="WCName" Hidden="True" BaseColumnName="WCName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업구분" Key="WorkDistinction" Hidden="True" BaseColumnName="WorkDistinction">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업계획수량" Key="WorkPlanQuantity" Width="80px" Format="###,###,###.##" 
 BaseColumnName="WorkPlanQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업완료수량" Key="WorkCompletionQuantity" Width="80px" Format="###,###,###.##" 
 BaseColumnName="WorkCompletionQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="이전완료수량" Key="PreWorkCompletionQuantity" Hidden="True" Format="###,###,###.##" 
 BaseColumnName="PreWorkCompletionQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="외주리드타임" Key="OrderLeadTime" Hidden="True" BaseColumnName="OrderLeadTime">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업일자" Key="WorkDate" Hidden="True" Format="yyyy-MM-dd" BaseColumnName="WorkDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" Hidden="True" BaseColumnName="ProgressCondition">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="생산계획번호" Key="ProductionPlanHistoryIndex" Hidden="True" BaseColumnName="ProductionPlanHistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업계획번호" Key="WorkPlanHistoryIndex" Hidden="True" BaseColumnName="WorkPlanHistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="WC작업계획번호" Key="WCDailyWorkPlanHistoryIndex" Hidden="True" BaseColumnName="WCDailyWorkPlanHistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
													</Columns>
													<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
														<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
													</RowTemplateStyle>
													<RowEditTemplate>
														<P align="center">&nbsp;</P>
													</RowEditTemplate>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
							</TABLE>
						</FIELDSET>
						<br>
						<br>
						<br>
						<br>
						<br>
						<br>
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[입력]</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD align="right" width="120">제품명&nbsp;</TD>
									<TD style="PADDING-LEFT: 10px" width="120"><asp:textbox id="tb_ProductName" runat="server" Font-Size="10pt" Height="20px" Width="120px"
											BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
									<TD width="80"></TD>
									<TD width="120"><INPUT id="tb_ProductDrawNum" style="WIDTH: 41px; HEIGHT: 13px; BACKGROUND-COLOR: #eeeee9"
											type="hidden" size="1" runat="server"> <INPUT id="tb_ProductNum" style="WIDTH: 41px; HEIGHT: 13px; BACKGROUND-COLOR: #eeeee9"
											type="hidden" size="1" runat="server"></TD>
									<TD width="120"><INPUT id="tb_ParentItemNum" style="WIDTH: 41px; HEIGHT: 13px; BACKGROUND-COLOR: #eeeee9"
											type="hidden" size="1" runat="server"></TD>
									<TD width="120"><INPUT id="tb_ParentName" style="WIDTH: 41px; HEIGHT: 13px; BACKGROUND-COLOR: #eeeee9"
											type="hidden" size="1" runat="server"><INPUT id="tb_ParentDrawNum" style="WIDTH: 41px; HEIGHT: 13px; BACKGROUND-COLOR: #eeeee9"
											type="hidden" size="1" runat="server"></TD>
									<TD width="120"></TD>
								</TR>
								<TR>
									<TD align="right" width="120">품목번호&nbsp;</TD>
									<TD style="PADDING-LEFT: 10px" width="120"><asp:textbox id="tb_ItemNum" runat="server" Font-Size="10pt" Height="20px" Width="120px" BackColor="#EEEEE9"
											BorderStyle="Solid" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
									<TD width="80"></TD>
									<TD align="right" width="120">도면번호&nbsp;</TD>
									<TD style="PADDING-LEFT: 10px" width="120"><asp:textbox id="tb_ItemDrawNum" runat="server" Font-Size="10pt" Height="20px" Width="120px"
											BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
									<TD align="right" width="120">품목명&nbsp;</TD>
									<TD style="PADDING-LEFT: 10px" width="120"><asp:textbox id="tb_ItemName" runat="server" Font-Size="10pt" Height="20px" Width="120px" BackColor="#EEEEE9"
											BorderStyle="Solid" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
								</TR>
								<TR>
									<TD align="right" width="120">공정명&nbsp;</TD>
									<TD style="PADDING-LEFT: 10px" width="120"><asp:textbox id="tb_ProcessName" runat="server" Font-Size="10pt" Height="20px" Width="120px"
											BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
									<TD width="80"></TD>
									<TD align="right" width="120">공정순서번호&nbsp;</TD>
									<TD style="PADDING-LEFT: 10px" width="120"><asp:textbox id="tb_ProcessSequenceNum" runat="server" Font-Size="10pt" Height="20px" Width="120px"
											BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
									<TD width="120"><INPUT id="tb_ProcessCode" style="WIDTH: 41px; HEIGHT: 13px; BACKGROUND-COLOR: #eeeee9"
											type="hidden" size="1" name="Hidden1" runat="server"></TD>
									<TD width="120"></TD>
								</TR>
								<TR>
									<TD align="right" width="120">작업계획수량&nbsp;</TD>
									<TD style="PADDING-LEFT: 10px" width="120"><asp:textbox id="tb_WorkPlanQuantity" style="TEXT-ALIGN: right" runat="server" Font-Size="10pt"
											Height="20px" Width="120px" BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" BorderWidth="1px" ReadOnly="True">0</asp:textbox></TD>
									<TD width="80"></TD>
									<TD align="right" width="120"><FONT color="#ff0000">금일완료수량&nbsp;</FONT></TD>
									<TD style="PADDING-LEFT: 10px" width="120"><asp:textbox id="tb_WorkCompletionQuantity1" style="TEXT-ALIGN: right" tabIndex="1" runat="server"
											Font-Size="10pt" Height="20px" Width="120px" BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" BorderWidth="1px">0</asp:textbox></TD>
									<TD align="right" width="120">잔량&nbsp;</TD>
									<TD style="PADDING-LEFT: 10px" width="120"><asp:textbox id="tb_RemainQuantity" style="TEXT-ALIGN: right" runat="server" Font-Size="10pt"
											Height="20px" Width="120px" BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" BorderWidth="1px" ReadOnly="True">0</asp:textbox></TD>
								</TR>
								<TR>
									<TD align="right" width="120">이전완료수량&nbsp;</TD>
									<TD style="PADDING-LEFT: 10px" align="left" width="120"><asp:textbox id="tb_WorkCompletionQuantity" style="TEXT-ALIGN: right" runat="server" Font-Size="10pt"
											Height="20px" Width="120px" BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" BorderWidth="1px" ReadOnly="True">0</asp:textbox></TD>
									<TD width="80"></TD>
									<TD align="right" width="120"><FONT color="#ff0000">적합수량&nbsp;</FONT></TD>
									<TD style="PADDING-LEFT: 10px" width="120"><asp:textbox id="tb_SuitabilityQuantity" style="TEXT-ALIGN: right" tabIndex="2" runat="server"
											Font-Size="10pt" Height="20px" Width="120px" BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" BorderWidth="1px">0</asp:textbox></TD>
									<TD align="right" width="120">부적합수량&nbsp;</TD>
									<TD style="PADDING-LEFT: 10px" width="120"><asp:textbox id="tb_UnSuittabilityQuantity" style="TEXT-ALIGN: right" runat="server" Font-Size="10pt"
											Height="20px" Width="120px" BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" BorderWidth="1px" ReadOnly="True">0</asp:textbox></TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 11px" align="right" width="120"><FONT color="#ff0000">작업시작시간&nbsp;</FONT></TD>
									<TD style="PADDING-LEFT: 10px; HEIGHT: 11px" align="left" width="120"><igsch:webdatechooser id="wdcBeginTime" tabIndex="3" runat="server" Font-Size="10pt" Height="18px" NullDateLabel=" "
											Text=" " BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray">
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
									<TD style="HEIGHT: 11px" width="80">&nbsp;
										<asp:textbox id="TextBox1" tabIndex="4" runat="server" Width="20px" BackColor="#EEEEE9" BorderStyle="Solid"
											BorderColor="DimGray" BorderWidth="1px"></asp:textbox>:
										<asp:textbox id="TextBox2" tabIndex="5" runat="server" Width="20px" BackColor="#EEEEE9" BorderStyle="Solid"
											BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
									<TD style="HEIGHT: 11px" align="right" width="120">비작업시간1&nbsp;</TD>
									<TD style="PADDING-LEFT: 10px; HEIGHT: 11px" width="120"><asp:textbox id="tb_NonWorkTime1" tabIndex="9" runat="server" Font-Size="10pt" Height="20px"
											Width="50px" BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" BorderWidth="1px">0</asp:textbox>분</TD>
									<TD style="HEIGHT: 11px" align="right" width="120">사유1&nbsp;</TD>
									<TD style="PADDING-LEFT: 10px; HEIGHT: 11px" width="120"><asp:dropdownlist id="dl_Reason1" tabIndex="10" runat="server" Font-Size="10pt" Height="20px" Width="120px"
											BackColor="#EEEEE9"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 14px" align="right" width="120"><FONT color="#ff0000">작업종료시간&nbsp;</FONT></TD>
									<TD style="PADDING-LEFT: 10px; HEIGHT: 14px" vAlign="middle" width="120"><igsch:webdatechooser id="wdcEndTime" tabIndex="6" runat="server" Font-Size="10pt" Height="18px" NullDateLabel=" "
											Text=" " BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray">
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
									<TD style="HEIGHT: 14px" width="80">&nbsp;
										<asp:textbox id="TextBox3" tabIndex="7" runat="server" Width="20px" BackColor="#EEEEE9" BorderStyle="Solid"
											BorderColor="DimGray" BorderWidth="1px"></asp:textbox>:
										<asp:textbox id="TextBox4" tabIndex="8" runat="server" Width="20px" BackColor="#EEEEE9" BorderStyle="Solid"
											BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
									<TD style="HEIGHT: 14px" align="right" width="120">비작업시간2&nbsp;</TD>
									<TD style="PADDING-LEFT: 10px; HEIGHT: 14px" width="120"><asp:textbox id="tb_NonWorkTime2" tabIndex="11" runat="server" Font-Size="10pt" Height="20px"
											Width="50px" BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" BorderWidth="1px">0</asp:textbox>분</TD>
									<TD style="HEIGHT: 14px" align="right" width="120">사유2&nbsp;</TD>
									<TD style="PADDING-LEFT: 10px; HEIGHT: 14px" width="120"><asp:dropdownlist id="dl_Reason2" tabIndex="12" runat="server" Font-Size="10pt" Height="20px" Width="120px"
											BackColor="#EEEEE9"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD align="right" width="120"></TD>
									<TD width="120"></TD>
									<TD width="80"></TD>
									<TD align="right" width="120">비작업시간3&nbsp;</TD>
									<TD style="PADDING-LEFT: 10px" width="120"><asp:textbox id="tb_NonWorkTime3" tabIndex="13" runat="server" Font-Size="10pt" Height="20px"
											Width="50px" BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" BorderWidth="1px">0</asp:textbox>분</TD>
									<TD align="right" width="120">사유3&nbsp;</TD>
									<TD style="PADDING-LEFT: 10px" width="120"><asp:dropdownlist id="dl_Reason3" tabIndex="14" runat="server" Font-Size="10pt" Height="20px" Width="120px"
											BackColor="#EEEEE9"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD align="right" width="120">기대번호(From)&nbsp;</TD>
									<TD align="right"><asp:textbox id="Textbox5" tabIndex="15" runat="server" Font-Size="9pt" Height="20px" Width="120px"
											BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
									<TD width="80"><asp:button id="btLot" runat="server" Height="20px" Width="24px" Text="..." Visible="False"></asp:button></TD>
									<TD align="right" width="120">부적합현상&nbsp;</TD>
									<TD style="PADDING-LEFT: 10px" width="120"><asp:dropdownlist id="dl_UnSuitabilityStatus" tabIndex="17" runat="server" Font-Size="10pt" Height="20px"
											Width="120px" BackColor="#EEEEE9"></asp:dropdownlist></TD>
									<TD align="right" width="120">부적합원인&nbsp;</TD>
									<TD style="PADDING-LEFT: 10px" width="120"><asp:dropdownlist id="dl_UnSuitabilityCause" tabIndex="18" runat="server" Font-Size="10pt" Height="26px"
											Width="120px" BackColor="#EEEEE9"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD align="right" width="120">기대번호(To)&nbsp;</TD>
									<TD align="right"><asp:textbox id="Textbox6" tabIndex="16" runat="server" Font-Size="9pt" Height="20px" Width="120px"
											BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
									<TD width="80"></TD>
									<TD align="right" width="120">부적합금액&nbsp;</TD>
									<TD style="PADDING-LEFT: 10px" width="120"><asp:textbox id="tb_UnSuitabilityCost" style="TEXT-ALIGN: right" tabIndex="19" runat="server"
											Font-Size="10pt" Height="20px" Width="125px" BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" BorderWidth="1px">0</asp:textbox></TD>
									<TD style="PADDING-LEFT: 10px" align="right" width="120">부적합세부내용&nbsp;</TD>
									<TD style="PADDING-LEFT: 10px" vAlign="middle" width="120" rowSpan="2"><asp:textbox id="tb_UnSuitabilityDetailMeaning" tabIndex="20" runat="server" Font-Size="10pt"
											Height="40px" Width="120px" BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" BorderWidth="1px" TextMode="MultiLine"></asp:textbox></TD>
								</TR>
								<TR>
									<TD align="right" width="120"></TD>
									<TD align="right"></TD>
									<TD width="80"></TD>
									<TD align="right" width="120">검사판정&nbsp;</TD>
									<TD style="PADDING-LEFT: 10px" width="120"><asp:dropdownlist id="dl_InspectionDecision" tabIndex="21" runat="server" Font-Size="10pt" Height="20px"
											Width="120px" BackColor="#EEEEE9"></asp:dropdownlist></TD>
									<TD width="120"></TD>
								</TR>
								<TR>
									<TD align="right" width="120" height="35"><INPUT id="tb_ProductionPlanHistoryIndex" style="WIDTH: 41px; HEIGHT: 13px; BACKGROUND-COLOR: #eeeee9"
											type="hidden" size="1" name="Hidden1" runat="server"> <INPUT id="tb_WorkPlanHistoryIndex" style="WIDTH: 41px; HEIGHT: 13px; BACKGROUND-COLOR: #eeeee9"
											type="hidden" size="1" name="Hidden1" runat="server"></TD>
									<TD width="120" height="35"><INPUT id="tb_WCDailyWorkPlanHistoryIndex" style="WIDTH: 41px; HEIGHT: 13px; BACKGROUND-COLOR: #eeeee9"
											type="hidden" size="1" value="0" name="Hidden1" runat="server"><INPUT id="hdLotNum" style="WIDTH: 55px; HEIGHT: 20px" type="hidden" size="3" name="hdLotNum"
											runat="server"></TD>
									<TD width="80" height="35"></TD>
									<TD align="right" width="120" height="35"></TD>
									<TD style="PADDING-LEFT: 10px" width="120" height="35"></TD>
									<TD width="120" height="35"><asp:linkbutton id="lnk_Update" runat="server" Font-Size="X-Small" Visible="False">LinkButton</asp:linkbutton></TD>
									<TD style="PADDING-LEFT: 10px" width="120" height="35">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:button id="Button2" runat="server" Font-Size="9pt" Height="20px" Width="60px" Text="등   록"></asp:button></TD>
								</TR>
							</TABLE>
						</FIELDSET> 
      </FONT></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>