<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearch" Src="ItemSearch/ItemSearch.ascx" %>
<%@ Page language="c#" Codebehind="RowItemExhaustPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.RowItemExhaustPC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RowItemExhaustPC</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		
		function ResettxtBox()
		{
			
			ResetTextBox();
			ResetBox();
			var objChooser1 = igdrp_getComboById("wdcStartDate");
			var objChooser2 = igdrp_getComboById("wdcEndDate");
			objChooser1.setValue(null);
			objChooser2.setValue(null);
			document.Form1.ddlWorker.options[0].selected=true;
					
		}
		
		
		function uwgE_HT_ColumnHeaderClickHandler(gridName, columnId, button)
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
				} else if( !IsNumericKey() ){
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

		function OnFocus_Obj(obj) {
			obj.select();
		}
	
		function ValidateFloat(a_sValue)	{	
			var s_FieldValue = new String(a_sValue);
			
			if (isNaN(s_FieldValue))
				return false;
				
			return true;
		}
		function uwgE_HT_BeforeRowTemplateCloseHandler(gridName, rowId, bSaveChanges){
			var row = igtbl_getRowById(rowId);
			var date1 = igdrp_getComboById("uwgExHTxxctl0xwdcExhaustDate");		
			row.getCellFromKey("ExhaustDate").setValue(date1.getValue());
			row.getCellFromKey("WorkerName").setValue(document.Form1.uwgE_HT__ctl0_ddlWorker.options[document.Form1.uwgE_HT__ctl0_ddlWorker.selectedIndex].innerText);
			row.getCellFromKey("WorkerID").setValue(document.Form1.uwgE_HT__ctl0_ddlWorker.options[document.Form1.uwgE_HT__ctl0_ddlWorker.selectedIndex].value);
				
		}
		function uwgE_HT_BeforeRowTemplateOpenHandler(gridName, rowId, templateId){
			
			var frm = document.Form1;
			var row = igtbl_getRowById(rowId);
			///////////RowIndex Hidden 에 저장////////////////
			var num = rowId.split('_');
			document.Form1.hdRowIndex.value = num[1];
			/////////////////////////////////////////////////
			
			var row = igtbl_getRowById(rowId);
			document.Form1.hdquantity.value = row.getCellFromKey("ExhaustQuantity").getValue();
			document.Form1.hdOldCreateQuantity.value = row.getCellFromKey("CreateQuantity").getValue();
			
			//입출고일자
			var date1 = igdrp_getComboById("uwgExHTxxctl0xwdcExhaustDate");
			if(row.getCellFromKey("ExhaustDate").getValue() !=null)
			{
					date1.setValue(row.getCellFromKey("ExhaustDate").getValue());
			}
			
			//작업자
			for(var l=0;l<document.Form1.uwgE_HT__ctl0_ddlWorker.options.length;l++)
			{
				if(document.Form1.uwgE_HT__ctl0_ddlWorker.options[l].value == row.getCellFromKey("WorkerID").getValue())
				{
					document.Form1.uwgE_HT__ctl0_ddlWorker.options[l].selected=true;
					break;
				}
				else
					document.Form1.uwgE_HT__ctl0_ddlWorker.options[0].selected=true;					
			}
			
			
			document.Form1.hdOldMonth.value = date1.getValue().getUTCMonth()+1;
			document.Form1.hdOldYear.value = date1.getValue().getFullYear();
			
			
						
		}
		
		
		</script>
	</HEAD>
	<body bgColor="#f7f6f6" MS_POSITIONING="GridLayout" XMLNS:igtbl="http://schemas.infragistics.com/ASPNET/WebControls/UltraWebGrid">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="LEFT: 10px; WIDTH: 800px; POSITION: absolute; TOP: 10px" cellSpacing="0"
				cellPadding="0" width="800" border="0">
				<TR>
					<TD></TD>
					<TD vAlign="top" align="left" width="800">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[입&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;력]</LEGEND>
							<TABLE id="Table2" height="30" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD colSpan="8" height="30"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="left" width="100%" colSpan="8"><uc1:itemsearch id="ItemSearch1" runat="server"></uc1:itemsearch></TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="left" width="100%" colSpan="8">
										<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="800" border="0">
											<tr>
											<TR>
												<TD align="right" width="70" height="30">소진 일자&nbsp;</TD>
												<TD align="left" width="100" height="30"><igsch:webdatechooser id="wdcStartDate" runat="server" BackColor="#EEEEE9" Width="100px" Height="20px"
														BorderColor="DimGray" BorderStyle="Solid" NullDateLabel=" " Text=" ">
														<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
															ShowTitle="False" ShowFooter="False">
															<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
															<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
															<DropDownStyle BackColor="White"></DropDownStyle>
															<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
														</CalendarLayout>
														<DropDownStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid" BackColor="#EEEEE9"></DropDownStyle>
														<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
														<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
													</igsch:webdatechooser></TD>
												<TD align="center" width="5" height="30">~</TD>
												<TD width="100" height="30"><igsch:webdatechooser id="wdcEndDate" runat="server" BackColor="#EEEEE9" Width="100px" Height="20px" BorderColor="DimGray"
														BorderStyle="Solid" NullDateLabel=" " Text=" ">
														<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
															ShowTitle="False" ShowFooter="False">
															<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
															<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
															<DropDownStyle BackColor="White"></DropDownStyle>
															<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
														</CalendarLayout>
														<DropDownStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid" BackColor="#EEEEE9"></DropDownStyle>
														<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
														<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
													</igsch:webdatechooser></TD>
												<td align="right" width="70">작업자</td>
												<td align="left" width="100"><asp:dropdownlist id="ddlWorker" runat="server" BackColor="#EEEEE9" Width="100px"></asp:dropdownlist></td>
												<td align="right" width="355" colSpan="2"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
														type="button" value="초기화" name="btnReset">&nbsp;
													<asp:button id="btnSearch" runat="server" Width="65px" Height="20px" Text="검   색" Font-Size="9pt"></asp:button>&nbsp;</td>
											</TR>
								</TR>
							</TABLE>
					</TD>
				</TR>
			</TABLE>
			</FIELDSET>
			<BR>
			<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"
				DESIGNTIMEDRAGDROP="86"><LEGEND align="top">[입력결과]</LEGEND>
				<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" border="0">
					<TR>
						<TD vAlign="top" align="left" colSpan="3"><igtbl:ultrawebgrid id="uwgE_HT" runat="server" Width="100%" Height="405px">
								<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
									RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
									HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
									RowSelectorsDefault="No" Name="uwgExHT" TableLayout="Fixed" CellClickActionDefault="RowSelect"
									AllowUpdateDefault="RowTemplateOnly">
									<AddNewBox>
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray"></Style>
									</AddNewBox>
									<Pager QuickPages="5" PageSize="17" StyleMode="ComboBox" AllowPaging="True">
										<Style VerticalAlign="Middle" BorderWidth="1px" BorderStyle="Solid" HorizontalAlign="Right"
											BackColor="LightGray"></Style>
									</Pager>
									<HeaderStyleDefault Cursor="Hand" BorderColor="Black" ForeColor="Black" BackColor="LightGray" Height="25px">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</HeaderStyleDefault>
									<GroupByRowStyleDefault ForeColor="Black" BackColor="#95B9F0"></GroupByRowStyleDefault>
									<RowSelectorStyleDefault BorderStyle="Solid"></RowSelectorStyleDefault>
									<FrameStyle Width="100%" Cursor="Default" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림"
										BorderColor="DimGray" BorderStyle="Solid" BackColor="Silver" Height="405px"></FrameStyle>
									<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</FooterStyleDefault>
									<ClientSideEvents ColumnHeaderClickHandler="uwgE_HT_ColumnHeaderClickHandler" BeforeRowTemplateCloseHandler="uwgE_HT_BeforeRowTemplateCloseHandler"
										BeforeRowTemplateOpenHandler="uwgE_HT_BeforeRowTemplateOpenHandler"></ClientSideEvents>
									<GroupByBox>
										<Style BackColor="#B5CFF7"></Style>
										<BandLabelStyle ForeColor="White" BackColor="#6372D4"></BandLabelStyle>
									</GroupByBox>
									<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
									<SelectedGroupByRowStyleDefault BorderColor="Yellow" BackColor="Yellow"></SelectedGroupByRowStyleDefault>
									<SelectedRowStyleDefault Cursor="Hand" Font-Bold="True" ForeColor="Linen" BackColor="Navy"></SelectedRowStyleDefault>
									<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
									<RowStyleDefault Cursor="Hand" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="Gray"
										BorderStyle="Solid" ForeColor="Black" BackColor="#EBEFF6">
										<Padding Left="3px"></Padding>
										<BorderDetails ColorTop="Gray" WidthLeft="0px" WidthTop="0px" ColorLeft="Gray"></BorderDetails>
									</RowStyleDefault>
								</DisplayLayout>
								<Bands>
									<igtbl:UltraGridBand>
										<Columns>
											<igtbl:UltraGridColumn HeaderText="선택" Key="chk" Width="30px" Type="CheckBox" HeaderClickAction="Select"
												BaseColumnName="chk" AllowUpdate="Yes">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="소진 품목번호" Key="ExhaustItemNum" Width="150px" BaseColumnName="ExhaustItemNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Left">
													<Padding Left="2px"></Padding>
												</CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="단위" Key="ExhaustUnit" Width="40px" Hidden="True" BaseColumnName="ExhaustUnit">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="소진수량" Key="ExhaustQuantity" Width="80px" Format="###,###,##0.00" BaseColumnName="ExhaustQuantity">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
													<Padding Right="4px"></Padding>
												</CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="작업일" Key="ExhaustDate" Format="yyyy-MM-dd" BaseColumnName="ExhaustDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="생성수량" Key="CreateQuantity" Width="80px" Format="###,###,##0.00" BaseColumnName="CreateQuantity">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
													<Padding Right="4px"></Padding>
												</CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="생성 품목번호" Key="CreateItemNum" Width="150px" BaseColumnName="CreateItemNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Left">
													<Padding Left="2px"></Padding>
												</CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="생성 품목명" Key="CreateItemName" Width="150px" BaseColumnName="CreateItemName">
												<CellStyle HorizontalAlign="Left">
													<Padding Left="2px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="용 도" Key="Use" Width="150px" BaseColumnName="Use">
												<CellStyle HorizontalAlign="Left">
													<Padding Left="4px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="시작시간" Key="StartTime" Width="80px" BaseColumnName="StartTime">
												<CellStyle HorizontalAlign="Left">
													<Padding Left="4px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="종료시간" Key="EndTime" Width="80px" BaseColumnName="EndTime">
												<CellStyle HorizontalAlign="Left">
													<Padding Left="4px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="작업자" Key="WorkerName" Width="70px" BaseColumnName="WorkerName"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="단위" Key="CreateUnit" Width="40px" Hidden="True" BaseColumnName="CreateUnit">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="소진 품목명" Key="ExhaustItemName" Width="150px" BaseColumnName="ExhaustItemName">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Left">
													<Padding Left="2px"></Padding>
												</CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="도면번호" Key="ExhaustItemDrawNum" Hidden="True" BaseColumnName="ExhaustItemDrawNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="규격" Key="ExhaustStandard" Hidden="True" BaseColumnName="ExhaustStandard">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="도면번호" Key="CreateItemDrawNum" Hidden="True" BaseColumnName="CreateItemDrawNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="규격" Key="CreateStandard" Hidden="True" BaseColumnName="CreateStandard">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="ID" Key="WorkerID" Hidden="True" BaseColumnName="WorkerID"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="자재소진원장번호" Key="ExhaustHistoryIndex" Hidden="True" BaseColumnName="ExhaustHistoryIndex">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
										</Columns>
										<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
											<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
										</RowTemplateStyle>
										<RowEditTemplate>
											<P align="center">
												<TABLE id="tb" style="BORDER-TOP-WIDTH: 1px; BORDER-LEFT-WIDTH: 1px; BORDER-LEFT-COLOR: dimgray; BORDER-BOTTOM-WIDTH: 1px; BORDER-BOTTOM-COLOR: dimgray; BORDER-TOP-COLOR: dimgray; BORDER-RIGHT-WIDTH: 1px; BORDER-RIGHT-COLOR: dimgray"
													cellSpacing="0" cellPadding="0" align="center" bgColor="aliceblue" border="0">
													<TR>
														<TD align="right" width="70">소진품번&nbsp;
														</TD>
														<TD width="150"><INPUT id="txtExhaustItemNum" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 150px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
																readOnly type="text" columnKey="ExhaustItemNum">
														</TD>
														<TD align="right" width="70">소진품명&nbsp;
														</TD>
														<TD width="150"><INPUT id="txtExhaustItemName" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 150px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
																readOnly type="text" columnKey="ExhaustItemName">
														</TD>
														<TD align="right" width="70"><FONT face="굴림">작업자</FONT>&nbsp;</TD>
														<TD width="100">
															<asp:DropDownList id=ddlWorker runat="server" BackColor="#EEEEE9" Font-Size="8pt" DataValueField="WorkerID" DataTextField="Worker" DataSource="<%# Worker %>" columnkey="Worker">
															</asp:DropDownList></TD>
														<TD align="right" width="70">소진수량&nbsp;
														</TD>
														<TD width="80"><INPUT id="txtExhaustQuantity" onkeydown="OnKeyDown_Currency(this)" onblur="OnBlur_Cur(this);"
																style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 80px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																onfocus="OnFocus_Obj(this);" type="text" value="0" columnKey="ExhaustQuantity">
														</TD>
														<TD width="15"></TD>
													</TR>
													<TR>
														<TD align="right" width="70">생성품번&nbsp;
														</TD>
														<TD width="150"><INPUT id="txtCreateItemNum" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 150px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
																readOnly type="text" columnKey="CreateItemNum">
														</TD>
														<TD align="right" width="70">생성품명&nbsp;
														</TD>
														<TD width="150"><INPUT id="txtCreateItemName" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 150px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white"
																readOnly type="text" columnKey="CreateItemName">
														</TD>
														<TD align="right" width="70"><FONT face="굴림">소진일자</FONT>&nbsp;</TD>
														<TD width="100">
															<igsch:webdatechooser id="wdcExhaustDate" runat="server" Text=" " NullDateLabel=" " BorderStyle="Solid"
																BorderColor="DimGray" Height="20px" Width="100px" BackColor="#EEEEE9">
																<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
																	ShowTitle="False" ShowFooter="False">
																	<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
																	<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
																	<DropDownStyle BackColor="White"></DropDownStyle>
																	<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
																</CalendarLayout>
																<DropDownStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid" BackColor="#EEEEE9"></DropDownStyle>
																<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
																<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
															</igsch:webdatechooser></TD>
														<TD align="right" width="70">생성수량&nbsp;
														</TD>
														<TD width="80"><INPUT id="txtCreateQuantity" onkeydown="OnKeyDown_Currency(this)" onblur="OnBlur_Cur(this);"
																style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 80px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: right"
																onfocus="OnFocus_Obj(this);" type="text" value="0" columnKey="CreateQuantity">
														</TD>
														<TD width="15"></TD>
													<TR>
														<TD colSpan="9">
															<TABLE id="t" style="BORDER-TOP-WIDTH: 1px; BORDER-LEFT-WIDTH: 1px; BORDER-LEFT-COLOR: dimgray; BORDER-BOTTOM-WIDTH: 1px; BORDER-BOTTOM-COLOR: dimgray; BORDER-TOP-COLOR: dimgray; BORDER-RIGHT-WIDTH: 1px; BORDER-RIGHT-COLOR: dimgray"
																cellSpacing="0" cellPadding="0" align="center" bgColor="aliceblue" border="0">
																<TR>
																	<TD align="right" width="50">용도
																	</TD>
																	<TD width="150"><INPUT id="txtUse" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 150px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: left"
																			type="text" columnKey="Use"></TD>
																	<TD align="right" width="70">시작시간
																	</TD>
																	<TD width="100"><INPUT id="txtStart" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 100px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: left"
																			type="text" columnKey="StartTime"></TD>
																	<TD align="right" width="70">종료시간
																	</TD>
																	<TD width="100"><INPUT id="txtEnd" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 100px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: white; TEXT-ALIGN: left"
																			type="text" columnKey="EndTime"></TD>
																	<TD width="235"></TD>
																</TR>
															</TABLE>
														</TD>
													</TR>
													<TR>
														<TD colSpan="9" height="150"></TD>
													</TR>
													<TR>
														<TD align="center" colSpan="9"><INPUT id="igtbl_reOkBtn" style="WIDTH: 50px" onclick="igtbl_gRowEditButtonClick(event);__doPostBack('linkUpdate','');"
																type="button" value="수정">&nbsp; <INPUT id="igtbl_reCancelBtn" style="WIDTH: 50px" onclick="igtbl_gRowEditButtonClick(event);"
																type="button" value="취소">
														</TD>
													</TR>
												</TABLE>
												<BR>
											</P>
										</RowEditTemplate>
									</igtbl:UltraGridBand>
								</Bands>
							</igtbl:ultrawebgrid></TD>
					</TR>
					<TR>
						<TD width="80">&nbsp;
							<asp:button id="btnExcel" runat="server" Width="65px" Height="20px" Text="Excel" Font-Size="9pt"></asp:button></TD>
						<td width="520"><igtblexp:ultrawebgridexcelexporter id="uwgExcel" runat="server"></igtblexp:ultrawebgridexcelexporter><INPUT id="hdRowIndex" style="WIDTH: 24px; HEIGHT: 21px" type="hidden" size="1" name="Hidden1"
								runat="server"><INPUT id="hdquantity" style="WIDTH: 22px; HEIGHT: 21px" type="hidden" size="1" name="Hidden1"
								runat="server"><INPUT id="hdOldMonth" style="WIDTH: 21px; HEIGHT: 21px" type="hidden" size="1" name="hdOldMonth"
								runat="server"><INPUT id="hdOldYear" style="WIDTH: 22px; HEIGHT: 21px" type="hidden" size="1" name="hdOldYear"
								runat="server">
							<asp:linkbutton id="linkUpdate" runat="server" Visible="False">linkUpdate</asp:linkbutton><INPUT id="chkAll" style="WIDTH: 23px; HEIGHT: 22px" type="hidden" size="1" value="true"
								name="chkAll"><INPUT id="hdOldCreateQuantity" style="WIDTH: 22px; HEIGHT: 21px" type="hidden" size="1"
								name="Hidden1" runat="server"></td>
						<td align="right" width="200"><asp:button id="btnCancel" runat="server" Width="65px" Height="20px" Text="삭   제" Font-Size="9pt"></asp:button>&nbsp;&nbsp;</td>
					</TR>
				</TABLE>
			</FIELDSET>
			</TD></TR></TABLE></form>
	</body>
</HTML>
