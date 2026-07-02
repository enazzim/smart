<%@ Page language="c#" Codebehind="RowMaterialRequriementPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.EtcPresentCondition.RowMaterialRequriementPC" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v1.1, Version=1.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RowMaterialRequriementPC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../GridCheck.js"></script>
		<script language="javascript" src="../PopupWindows.js"></script>
		<script language="javascript" src="../MessageWindows.js"></script>
		<SCRIPT type="text/javascript">
		<!--
		
		function ResettxtBox()
		{
			
			ResetTextBox();
			ResetBox();
			var objChooser3 = igdrp_getComboById("wdcFromDate");
			var objChooser4 = igdrp_getComboById("wdcToDate");
			objChooser3.setValue(null);
			objChooser4.setValue(null);
			document.Form1.ddlItemClassification1.options[0].selected=true;
			document.Form1.ddlState.options[0].selected=true;
			document.Form1.btnCancle.disable = true;
			
		}
		
		var GridName;
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
		
		// 납기량 입력시 onkeyup 이벤트 때 발생.
		function Process()	
		{
			
		}
		

//////////////////////////////////////////////////////////////////////////////////////////
//										Float						        			//
//////////////////////////////////////////////////////////////////////////////////////////
		
		function OnKeyDown_Float(obj) {   
		
			// 입력수량 TextBox 객체의 value로 배열을 만든다.
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
//										Currency										//
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
				event.keyCode == 110 ||		// "."
				event.keyCode == 190		// 키패드 "."
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
			function uwgBR_HT_BeforeRowTemplateOpenHandler(gridName, rowId, templateId){
			
			var frm = document.Form1;
			var row = igtbl_getRowById(rowId);
			///////////RowIndex Hidden 에 저장////////////////
			var num = rowId.split('_');
			document.Form1.HistoryIndex.value = num[1];
			/////////////////////////////////////////////////
			
			
			
			
				var row = igtbl_getRowById(rowId);
		
				if((row.getCellFromKey("ProgressCondition").getValue() == "대기"))
				{
					var row = igtbl_getRowById(rowId);
					var date1 = igedit_getById("uwgBR_HT__ctl0_wdeFirstDeliveryDemandDate", null);
									
					date1.setValue(row.getCellFromKey("FirstDeliveryDemandDate").getValue());
					Form1.OrderQuanatity.value = MakeCommaSeparate(row.getCellFromKey("OrderQuantity").getValue());
					
					
				}
				else
				{
					alert("수정할 수 없습니다.!");
					return true;
				}
			}
			
			function uwgBR_HT_AfterRowTemplateCloseHandler(gridName, rowId, bSaveChanges)
			{
				if(event.srcElement.id == "igtbl_reOkBtn")
				{
					var row =  igtbl_getRowById(rowId);
					var date1 = igedit_getById("uwgBR_HT__ctl0_wdeFirstDeliveryDemandDate", null);
					
					row.getCellFromKey("FirstDeliveryDemandDate").setValue(date1.getValue());
					row.getCellFromKey("OrderQuantity").setValue(Form1.OrderQuanatity.value);
					__doPostBack('lnkRowUpDate','');					
				}
			}
		function uwgBR_HT_ColumnHeaderClickHandler(gridName, columnId, button)
		{
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
		
		//-->
		</SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<FONT face="굴림">
				<TABLE id="Table1" style="Z-INDEX: 100; LEFT: 10px; POSITION: absolute; TOP: 10px" cellSpacing="0"
					cellPadding="0" width="800" border="0">
					<TR>
						<TD style="WIDTH: 18px; HEIGHT: 530px" width="18"></TD>
						<TD vAlign="top" align="center" width="800">
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[검색조건]</LEGEND>
								<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" border="0">
									<TR>
										<TD vAlign="middle" align="left" colSpan="9" height="30">
											<table id="table3" border="0" cellpadding="0" cellspacing="0" width="800">
												<tr>
													<td width="200" align="left">
														<uc1:CompanySearchControl id="CSC1" runat="server"></uc1:CompanySearchControl>
													<td width="600"><uc1:ItemSearchControl id="ItemSearchControl1" runat="server"></uc1:ItemSearchControl></td>
												</tr>
											</table>
										</TD>
									</TR>
									<TR>
										<TD vAlign="middle" align="left" colspan="9" height="30">
											<table id="Table4" cellpadding="0" cellspacing="0" border="0" width="800">
												<tr>
													<td align="right" width="70">제품팀</td>
													<td width="100">
														<asp:dropdownlist id="ddlItemClassification1" runat="server" BackColor="#EEEEE9" Width="100px"></asp:dropdownlist></td>
													<TD vAlign="middle" align="right" width="70" height="30">
														진행상태</TD>
													<TD vAlign="middle" align="left" width="100" height="30">
														<asp:dropdownlist id="ddlState" runat="server" Width="100px" BackColor="#EEEEE9">
															<asp:ListItem Value=" ">- 선택하세요 -</asp:ListItem>
															<asp:ListItem Value="대기">대기</asp:ListItem>
															<asp:ListItem Value="완료">완료</asp:ListItem>
															<asp:ListItem Value="중단">중단</asp:ListItem>
														</asp:dropdownlist></TD>
													<td align="right" width="70">납기요구일</td>
													<td width="100">
														<igsch:webdatechooser id="wdcFromDate" runat="server" BorderColor="DimGray" BorderStyle="Solid" BackColor="#EEEEE9"
															Text=" " NullDateLabel=" " Height="18px" Width="100px" Font-Size="10pt">
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
													<td width="5">~</td>
													<td width="100">
														<igsch:webdatechooser id="wdcToDate" runat="server" BorderColor="DimGray" BorderStyle="Solid" BackColor="#EEEEE9"
															Text=" " NullDateLabel=" " Height="18px" Width="100px" Font-Size="10pt">
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
													<TD vAlign="middle" align="right" height="30"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
															type="button" value="초기화" name="btnReset">&nbsp;
														<asp:button id="btnSearch" runat="server" Font-Size="9pt" BorderStyle="Outset" Width="60px"
															Height="20px" Text="검   색" CommandName="Search"></asp:button>&nbsp;&nbsp;</TD>
												</tr>
											</table>
										</TD>
									</TR>
									<TR>
										<TD vAlign="middle" align="right" width="80" height="8" colSpan="9"></TD>
									</TR>
								</TABLE>
							</FIELDSET>
							<BR>
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[검색결과]</LEGEND>
								<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" border="0">
									<TR>
										<TD style="HEIGHT: 28px" width="70%" height="28"></TD>
										<TD style="WIDTH: 35px; HEIGHT: 28px" align="right" width="35" height="28"></TD>
										<TD style="PADDING-RIGHT: 10px" align="right"><INPUT id="volumNum" style="WIDTH: 26px; HEIGHT: 12px" type="hidden" size="1" value="0"
												name="Hidden1" runat="server"><asp:button id="btnPre" runat="server" Font-Size="8pt" Height="16px" Width="16px" Text="◀" CommandName="Video"
												CommandArgument="Pre"></asp:button><asp:button id="btnNow" runat="server" Font-Size="8pt" Height="16px" Width="16px" Text="■" CommandName="Video"
												CommandArgument="Now"></asp:button><asp:button id="btnNext" runat="server" Font-Size="8pt" Height="16px" Width="16px" Text="▶"
												CommandName="Video" CommandArgument="Next"></asp:button></TD>
									</TR>
									<TR>
										<TD vAlign="top" align="center" colSpan="3">
											<P>
												<igtbl:ultrawebgrid id="uwgBR_HT" runat="server" Width="100%" Height="350px">
													<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
														RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
														HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
														RowSelectorsDefault="No" Name="uwgBRxHT" TableLayout="Fixed" CellClickActionDefault="RowSelect">
														<AddNewBox>
															<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
															</Style>
														</AddNewBox>
														<Pager PageSize="15" StyleMode="ComboBox" AllowPaging="True">
															<Style VerticalAlign="Middle" BorderWidth="1px" BorderStyle="Solid" HorizontalAlign="Right"
																BackColor="LightGray">
															</Style>
														</Pager>
														<HeaderStyleDefault Cursor="Hand" BorderColor="Black" BorderStyle="Solid" ForeColor="Black" BackColor="LightGray"
															Height="25px">
															<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
														</HeaderStyleDefault>
														<GroupByRowStyleDefault ForeColor="Black" BackColor="#95B9F0"></GroupByRowStyleDefault>
														<RowSelectorStyleDefault BorderStyle="Solid"></RowSelectorStyleDefault>
														<FrameStyle Width="100%" Cursor="Default" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림"
															BorderColor="DimGray" BorderStyle="Solid" BackColor="Silver" Height="350px"></FrameStyle>
														<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
															<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
														</FooterStyleDefault>
														<ClientSideEvents ColumnHeaderClickHandler="uwgBR_HT_ColumnHeaderClickHandler" BeforeRowTemplateOpenHandler="uwgBR_HT_BeforeRowTemplateOpenHandler"
															AfterRowTemplateCloseHandler="uwgBR_HT_AfterRowTemplateCloseHandler"></ClientSideEvents>
														<GroupByBox>
															<Style BackColor="#B5CFF7">
															</Style>
															<BandLabelStyle ForeColor="White" BackColor="#6372D4"></BandLabelStyle>
														</GroupByBox>
														<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
														<SelectedGroupByRowStyleDefault BorderColor="Yellow" BackColor="Yellow"></SelectedGroupByRowStyleDefault>
														<SelectedRowStyleDefault Cursor="Hand" Font-Bold="True" ForeColor="Linen" BackColor="Navy"></SelectedRowStyleDefault>
														<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
														<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" ForeColor="Black"
															BackColor="#EBEFF6">
															<Padding Left="3px"></Padding>
															<BorderDetails ColorTop="Gray" WidthLeft="0px" WidthTop="0px" ColorLeft="Gray"></BorderDetails>
														</RowStyleDefault>
													</DisplayLayout>
													<Bands>
														<igtbl:UltraGridBand AllowUpdate="RowTemplateOnly">
															<Columns>
																<igtbl:UltraGridColumn HeaderText="선택" Key="chk" Width="35px" Type="CheckBox" BaseColumnName="chk" AllowUpdate="Yes">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="130px" BaseColumnName="ItemNum">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Left">
																		<Padding Left="2px"></Padding>
																	</CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Width="140px" Hidden="True" BaseColumnName="ItemDrawNum">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="130px" BaseColumnName="ItemName">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Left">
																		<Padding Left="2px"></Padding>
																	</CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="규격" Key="Standard" Width="160px" BaseColumnName="Standard"></igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="의뢰부서" Key="RequestPost" Hidden="True" BaseColumnName="RequestPost">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="구매의뢰 원천" Key="BuyingRequestSource" Hidden="True" BaseColumnName="BuyingRequestSource">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="구매의뢰원천코드" Key="BuyingRequestSourceCode" Hidden="True" BaseColumnName="BuyingRequestSourceCode">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="거래처" Key="CompanyName" BaseColumnName="CompanyName"></igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="1차납기량" Key="FirstDeliveryDemandQuantity" Hidden="True" Format="###,###,##0.00"
																	BaseColumnName="FirstDeliveryDemandQuantity">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="총구매량" Key="OrderQuantity" Width="80px" Format="###,###,##0.00" BaseColumnName="OrderQuantity">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" Width="60px" BaseColumnName="ProgressCondition">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="납기일자" Key="FirstDeliveryDemandDate" Format="yyyy-MM-dd" BaseColumnName="FirstDeliveryDemandDate">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="2차납기량" Key="SecondDeliveryDemandQuantity" Hidden="True" Format="###,###,##0.00"
																	BaseColumnName="SecondDeliveryDemandQuantity">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="2차납기일" Key="SecondDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
																	BaseColumnName="SecondDeliveryDemandDate">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="3차납기량" Key="ThirdDeliveryDemandQuantity" Hidden="True" Format="###,###,##0.00"
																	BaseColumnName="ThirdDeliveryDemandQuantity">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="3차납기일" Key="ThirdDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
																	BaseColumnName="ThirdDeliveryDemandDate">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="4차납기량" Key="FourthDeliveryDemandQuantity" Hidden="True" Format="###,###,##0.00"
																	BaseColumnName="FourthDeliveryDemandQuantity">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="4차납기일" Key="FourthDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
																	BaseColumnName="FourthDeliveryDemandDate">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="5차납기량" Key="FifthDeliveryDemandQuantity" Hidden="True" Format="###,###,##0.00"
																	BaseColumnName="FifthDeliveryDemandQuantity">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="5차납기일" Key="FifthDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
																	BaseColumnName="FifthDeliveryDemandDate">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Hidden="True" Format="\ ###,###,##0.00" BaseColumnName="ApplyUnitCost">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="금액" Key="TotalCost" Hidden="True" Format="\ ###,###,##0.00" BaseColumnName="TotalCost">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																	<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="의뢰부서코드" Key="RequestPostCode" Hidden="True" BaseColumnName="RequestPostCode">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="원장번호" Key="HistoryIndex" Hidden="True" BaseColumnName="HistoryIndex">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="원장구분" Key="HistorySection" Hidden="True" BaseColumnName="HistorySection">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="구매의뢰원장번호" Key="BuyingRequestHistoryIndex" Hidden="True" BaseColumnName="BuyingRequestHistoryIndex">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
															</Columns>
														</igtbl:UltraGridBand>
													</Bands>
												</igtbl:ultrawebgrid></P>
										</TD>
									</TR>
									<TR>
										<TD width="500" height="35">&nbsp;<asp:button id="btnExcel" runat="server" Font-Size="9pt" Height="20px" Width="60px" Text="Excel"
												CommandName="Excel" Enabled="False" BorderStyle="Outset"></asp:button><igtblexp:ultrawebgridexcelexporter id="uwgExcel" runat="server"></igtblexp:ultrawebgridexcelexporter><INPUT id="chkAll" style="WIDTH: 51px; HEIGHT: 22px" type="hidden" size="3" value="true"
												name="Hidden1" runat="server"></TD>
										<TD vAlign="middle" align="right" colSpan="2" width="300" height="35"><FONT face="굴림">&nbsp;&nbsp;&nbsp;
											</FONT><FONT face="굴림">&nbsp;&nbsp;&nbsp; </FONT>&nbsp;</TD>
									</TR>
								</TABLE>
							</FIELDSET>
						</TD>
					</TR>
				</TABLE>
				<INPUT id="HistoryIndex" style="Z-INDEX: 102; LEFT: 8px; POSITION: absolute; TOP: 640px"
					type="hidden" name="Hidden1" runat="server"> </FONT>
		</form>
	</body>
</HTML>
