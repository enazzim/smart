<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="GoodsBuyingRequestPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BusinessManagement.GoodsBuyingRequestPC" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>GoodsBuyingRequestPC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--
			
		function UltraWebGrid1_ColumnHeaderClickHandler(gridName, columnId, button)
		{
			GridName = gridName;
    			if(document.GoodsBuyingRequestPC.chkAll.value == "true")
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
			document.GoodsBuyingRequestPC.chkAll.value = "false";
		}


		function AllUncheck() // 전부체크 해제하기
		{
			var grid = igtbl_getGridById(GridName);
			
			for(var i=0; i<grid.Rows.length; i++)
			{
				var row = grid.Rows.getRow(i);
				row.getCellFromKey("chk").setValue(false);
			}
			document.GoodsBuyingRequestPC.chkAll.value = "true";
		}

		function UltraWebGrid1_MouseOverHandler(gridName, id, button)
		{
			//Add code to handle your event here.
			if(button == 0)
			{ // Are we over a cell
				var cell = igtbl_getElementById(id);
				cell.style.cursor = 'hand';
				var parts = id.split("_");
				document.GoodsBuyingRequestPC.lb_RowIndex.value = parts[1];
            }
			
		}
		
		function UltraWebGrid1_BeforeRowTemplateOpenHandler(gridName, rowId, templateId)
		{
			//현재 선택되어진 객체의 행의 가져옴
			var objGrid = igtbl_getActiveRow(gridName);
			var row = igtbl_getRowById(rowId);
			
			document.GoodsBuyingRequestPC.lb_RowSelectIndex.value = document.GoodsBuyingRequestPC.lb_RowIndex.value;
	
			if((row.getCellFromKey("ProgressCondition").getValue() == "대기") && (row.getCellFromKey("BuyingRequestSource").getValue() != "정상"))
			{
				//구매의뢰원천 입력
				for(var i=0;i<document.GoodsBuyingRequestPC.uwgRO__ctl0_DropDownList1.options.length;i++)
				{
					if(document.GoodsBuyingRequestPC.uwgRO__ctl0_DropDownList1.options[i].innerText == document.getElementById(rowId).children[6].innerText)
					{
						document.GoodsBuyingRequestPC.uwgRO__ctl0_DropDownList1.options[i].selected=true;
					}
				}
				
				//납기일
				var date1 = igdrp_getComboById("uwgROxxctl0xWebdatechooser1");
				var date2 = igdrp_getComboById("uwgROxxctl0xWebdatechooser2");
				var date3 = igdrp_getComboById("uwgROxxctl0xWebdatechooser3");
				var date4 = igdrp_getComboById("uwgROxxctl0xWebdatechooser4");
				var date5 = igdrp_getComboById("uwgROxxctl0xWebdatechooser5");
				
				date1.setValue(objGrid.getCellFromKey("FirstDeliveryDemandDate").getValue());
				date2.setValue(objGrid.getCellFromKey("SecondDeliveryDemandDate").getValue());
				date3.setValue(objGrid.getCellFromKey("ThirdDeliveryDemandDate").getValue());
				date2.setValue(objGrid.getCellFromKey("FourthDeliveryDemandDate").getValue());
				date3.setValue(objGrid.getCellFromKey("FifthDeliveryDemandDate").getValue());
			}
			else
			{
				alert("수정할 수 없습니다.!");
				return true;
			}
		}
		function UltraWebGrid1_AfterRowTemplateCloseHandler(gridName, rowId, bSaveChanges){
			//Add code to handle your event here.
			if(event.srcElement.id == "igtbl_reOkBtn")
			{
				var frm = document.GoodsBuyingRequestPC;
				var row = igtbl_getRowById(rowId);
				
				//납기일
				var date1 = igdrp_getComboById("uwgROxxctl0xWebdatechooser1");
				var date2 = igdrp_getComboById("uwgROxxctl0xWebdatechooser2");
				var date3 = igdrp_getComboById("uwgROxxctl0xWebdatechooser3");
				var date4 = igdrp_getComboById("uwgROxxctl0xWebdatechooser4");
				var date5 = igdrp_getComboById("uwgROxxctl0xWebdatechooser5");
				row.getCellFromKey("FirstDeliveryDemandDate").setValue(date1.getValue());
				row.getCellFromKey("SecondDeliveryDemandDate").setValue(date2.getValue());
				row.getCellFromKey("ThirdDeliveryDemandDate").setValue(date3.getValue());
				row.getCellFromKey("FourthDeliveryDemandDate").setValue(date4.getValue());
				row.getCellFromKey("FifthDeliveryDemandDate").setValue(date5.getValue());
				
				//구매의뢰원천
				row.getCellFromKey("BuyingRequestSourceCode").setValue(document.GoodsBuyingRequestPC.UltraWebGrid1__ctl0_DropDownList1.options[document.GoodsBuyingRequestPC.UltraWebGrid1__ctl0_DropDownList1.selectedIndex].value);
				row.getCellFromKey("BuyingRequestSource").setValue(document.GoodsBuyingRequestPC.UltraWebGrid1__ctl0_DropDownList1.options[document.GoodsBuyingRequestPC.UltraWebGrid1__ctl0_DropDownList1.selectedIndex].innerText);
				
				//총요구량, 총금액
				var total = Number(frm.igtbl_TextBox_0_6.value) + Number(frm.igtbl_TextBox_0_8.value) + Number(frm.igtbl_TextBox_0_10.value) + Number(frm.igtbl_TextBox_0_12.value) + Number(frm.igtbl_TextBox_0_14.value);
				var cost = total * row.getCellFromKey("ApplyUnitCost").getValue();
				
				frm.lb_Total.value = total;
				frm.lb_Cost.value = cost;
				
			}
		}
		
		
		
		// 그리드 인덱스 저장
		function UltraWebGrid1_AfterSelectChangeHandler(gridName, id){
			//Add code to handle your event here.
			document.GoodsBuyingRequestPC.lb_RowSelectIndex.value = document.GoodsBuyingRequestPC.lb_RowIndex.value
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
		<form id="GoodsBuyingRequestPC" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" height="460"
				cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD></TD>
					<TD style="PADDING-TOP: 10px" vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 800px; BORDER-BOTTOM: #696969 2px solid; HEIGHT: 20px"><LEGEND align="top">&nbsp;[검색조건]</LEGEND>
							<TABLE id="Table2" style="HEIGHT: 20px" cellSpacing="0" cellPadding="0" width="800">
								<TR>
									<TD width="600" colSpan="6" height="30">
										<uc1:ItemSearchControl id="ItemSearchControl1" runat="server"></uc1:ItemSearchControl></TD>
									<TD align="right" width="70" height="30"><FONT face="굴림">진행상태&nbsp; </FONT>
									</TD>
									<TD width="130" height="30">
										<asp:dropdownlist id="dl_Progress" runat="server" Width="100px" BackColor="#EEEEE9" Height="20px"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30"><FONT face="굴림">의뢰원천&nbsp; </FONT>
									</TD>
									<TD width="130" height="30">
										<asp:dropdownlist id="dl_Source" runat="server" Width="100px" BackColor="#EEEEE9" Height="20px"></asp:dropdownlist></TD>
									<TD align="right" width="70" height="30"><FONT face="굴림">수주일자&nbsp; </FONT>
									</TD>
									<TD width="100" height="30">
										<igsch:webdatechooser id="wdc_FromDate" runat="server" Width="90px" BackColor="#EEEEE9" Height="20px"
											Text="Null" NullDateLabel=" ">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderStyle="Inset"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD width="15" height="30"><FONT face="굴림">~</FONT></TD>
									<TD width="215" height="30">
										<igsch:webdatechooser id="wdc_ToDate" runat="server" Width="90px" BackColor="#EEEEE9" Height="20px" Text="Null"
											NullDateLabel=" ">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderStyle="Inset"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD align="right" width="200" colSpan="2" height="30"><FONT face="굴림">
											<asp:button id="tb_Clear" runat="server" Width="60px" Height="20px" Text="초기화" Font-Size="10pt"></asp:button>&nbsp;
											<asp:button id="bt_Search" runat="server" Width="60px" Height="20px" Text="검색" Font-Size="10pt"></asp:button>&nbsp;</FONT></TD>
								</TR>
							</TABLE>
							<FONT face="굴림"></FONT>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="0"></TD>
					<TD style="PADDING-TOP: 10px" vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 800px; BORDER-BOTTOM: #696969 2px solid"><LEGEND style="WIDTH: 19px; HEIGHT: 15px" align="top">[검색결과]</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800">
								<tr>
									<td align="right" colSpan="2" height="10"><INPUT id="Volum" style="WIDTH: 34px; HEIGHT: 16px" type="hidden" size="1" value="0" name="Hidden1"
											runat="server"><asp:label id="lb_Vol" runat="server" Font-Size="8pt"></asp:label><asp:button id="btnPre" runat="server" Height="16px" Width="16px" Text="◀" Font-Size="8pt" CommandName="Pre"></asp:button><asp:button id="btnNow" runat="server" Height="16px" Width="16px" Text="■" Font-Size="8pt" CommandName="Now"></asp:button><asp:button id="btnNext" runat="server" Height="16px" Width="16px" Text="▶" Font-Size="8pt"
											CommandName="Next"></asp:button><FONT face="굴림">&nbsp;</FONT></td>
								</tr>
								<TR>
									<TD colSpan="2"><FONT face="굴림"><igtbl:ultrawebgrid id="uwgRO" runat="server" Height="386px" Width="800px">
												<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
													RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
													HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
													RowSelectorsDefault="No" Name="uwgRO" TableLayout="Fixed" CellClickActionDefault="RowSelect"
													AllowUpdateDefault="RowTemplateOnly">
													<AddNewBox>
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														</Style>
													</AddNewBox>
													<Pager PageSize="16" StyleMode="ComboBox" AllowPaging="True">
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														</Style>
													</Pager>
													<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</HeaderStyleDefault>
													<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
													<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
														BackColor="Silver" Height="386px"></FrameStyle>
													<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</FooterStyleDefault>
													<ClientSideEvents ColumnHeaderClickHandler="UltraWebGrid1_ColumnHeaderClickHandler" BeforeRowTemplateOpenHandler="UltraWebGrid1_BeforeRowTemplateOpenHandler"
														AfterSelectChangeHandler="UltraWebGrid1_AfterSelectChangeHandler" AfterRowTemplateCloseHandler="UltraWebGrid1_AfterRowTemplateCloseHandler"
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
													<igtbl:UltraGridBand>
														<Columns>
															<igtbl:UltraGridColumn HeaderText="전체" Key="chk" Width="30px" Type="CheckBox" HeaderClickAction="Select"
																BaseColumnName="chk" AllowResize="Free" AllowUpdate="Yes">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle Height="25px"></HeaderStyle>
															</igtbl:UltraGridColumn>
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
															<igtbl:UltraGridColumn HeaderText="구매의뢰원천코드" Key="BuyingRequestSourceCode" Hidden="True" HeaderClickAction="SortMulti"
																BaseColumnName="BuyingRequestSourceCode">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="1차납기요구량" Key="FirstDeliveryDemandQuantity" Hidden="True" Format="###,###,###.##"
																HeaderClickAction="SortMulti" BaseColumnName="FirstDeliveryDemandQuantity">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="납기요구일" Key="FirstDeliveryDemandDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
																BaseColumnName="FirstDeliveryDemandDate">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="2차납기요구량" Key="SecondDeliveryDemandQuantity" Hidden="True" Format="###,###,###.##"
																HeaderClickAction="SortMulti" BaseColumnName="SecondDeliveryDemandQuantity">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="2차납기요구일" Key="SecondDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
																HeaderClickAction="SortMulti" BaseColumnName="SecondDeliveryDemandDate">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="3차납기요구량" Key="ThirdDeliveryDemandQuantity" Hidden="True" Format="###,###,###.##"
																HeaderClickAction="SortMulti" BaseColumnName="ThirdDeliveryDemandQuantity">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="3차납기요구일" Key="ThirdDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
																HeaderClickAction="SortMulti" BaseColumnName="ThirdDeliveryDemandDate">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="4차납기요구량" Key="FourthDeliveryDemandQuantity" Hidden="True" Format="###,###,###.##"
																HeaderClickAction="SortMulti" BaseColumnName="FourthDeliveryDemandQuantity">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="4차납기요구일" Key="FourthDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
																HeaderClickAction="SortMulti" BaseColumnName="FourthDeliveryDemandDate">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="5차납기요구량" Key="FifthDeliveryDemandQuantity" Hidden="True" Format="###,###,###.##"
																HeaderClickAction="SortMulti" BaseColumnName="FifthDeliveryDemandQuantity">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="5차납기요구일" Key="FifthDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
																HeaderClickAction="SortMulti" BaseColumnName="FifthDeliveryDemandDate">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="총요구량" Key="OrderQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
																BaseColumnName="OrderQuantity">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
																BaseColumnName="ApplyUnitCost">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="총금액" Key="TotalCost" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
																BaseColumnName="TotalCost">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="구매의뢰원천" Key="BuyingRequestSource" Hidden="True" HeaderClickAction="SortMulti"
																BaseColumnName="BuyingRequestSource">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" HeaderClickAction="SortMulti" BaseColumnName="ProgressCondition">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="자산분류" Key="PropertyClassification" Hidden="True" HeaderClickAction="SortMulti"
																BaseColumnName="PropertyClassification">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPerson">
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
															<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
																BaseColumnName="UpdatingDate">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="이전원장번호" Key="HistoryIndex" Hidden="True" HeaderClickAction="SortMulti"
																BaseColumnName="HistoryIndex">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="이전원장구분" Key="HistorySection" Hidden="True" HeaderClickAction="SortMulti"
																BaseColumnName="HistorySection">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="번호" Key="BuyingRequestHistoryIndex" HeaderClickAction="SortMulti" BaseColumnName="BuyingRequestHistoryIndex">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
														</Columns>
														<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
															<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
														</RowTemplateStyle>
														<RowEditTemplate>
															<TABLE>
																<TR>
																	<TD>
																		<asp:Label id="Label1" runat="server" Font-Size="9pt">품목번호</asp:Label></TD>
																	<TD>
																		<INPUT id="igtbl_TextBox_0_1" style="WIDTH: 100px" readOnly type="text" columnKey="ItemNum"></TD>
																	<TD>
																		<asp:Label id="Label4" runat="server" Font-Size="9pt">도면번호</asp:Label></TD>
																	<TD>
																		<INPUT id="igtbl_TextBox_0_2" style="WIDTH: 100px; HEIGHT: 22px" readOnly type="text" size="14"
																			columnKey="ItemDrawNum"></TD>
																	<TD>
																		<asp:Label id="Label5" runat="server" Font-Size="9pt">품목명</asp:Label></TD>
																	<TD>
																		<INPUT id="igtbl_TextBox_0_3" style="WIDTH: 100px" readOnly type="text" columnKey="ItemName"></TD>
																	<TD>
																		<asp:Label id="Label7" runat="server" Font-Size="9pt">자산분류</asp:Label></TD>
																	<TD>
																		<INPUT id="igtbl_TextBox_0_4" style="WIDTH: 100px" readOnly type="text" columnKey="PropertyClassification"></TD>
																</TR>
																<TR>
																	<TD style="HEIGHT: 4px">
																		<asp:Label id="Label14" runat="server" Font-Size="9pt">구매의뢰원천</asp:Label></TD>
																	<TD style="HEIGHT: 4px">
																		<asp:DropDownList id=DropDownList1 runat="server" Width="100px" DataSource="<%# Source %>" DataValueField="SmallClassificationCode" DataTextField="SmallClassificationName">
																		</asp:DropDownList></TD>
																	<TD style="HEIGHT: 4px">
																		<asp:Label id="Label15" runat="server" Font-Size="9pt">1차납기요구량</asp:Label></TD>
																	<TD style="HEIGHT: 4px">
																		<INPUT id="igtbl_TextBox_0_6" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																			style="WIDTH: 100px; HEIGHT: 20px; TEXT-ALIGN: right" onfocus="OnFocus_Obj(this);"
																			type="text" size="11" value="0" Width="100px" columnKey="FirstDeliveryDemandQuantity"></TD>
																	<TD style="HEIGHT: 4px">
																		<asp:Label id="Label16" runat="server" Font-Size="10pt">1차납기요구일</asp:Label></TD>
																	<TD style="HEIGHT: 4px">
																		<igsch:webdatechooser id="Webdatechooser1" runat="server" Width="100px" Height="20px" NullDateLabel=" "
																			Text="Null" columnKey="DeliveryRequestDate1">
																			<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
																				DayNameFormat="FirstLetter">
																				<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
																				<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																				<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																				<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
																			</CALENDARLAYOUT>
																			<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
																			<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
																			<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
																		</igsch:webdatechooser></TD>
																	<TD style="HEIGHT: 4px">
																		<asp:Label id="Label17" runat="server" Font-Size="9pt">2차납기요구량</asp:Label></TD>
																	<TD style="HEIGHT: 4px">
																		<INPUT id="igtbl_TextBox_0_8" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																			style="WIDTH: 100px; HEIGHT: 20px; TEXT-ALIGN: right" onfocus="OnFocus_Obj(this);"
																			type="text" size="14" value="0" Width="100px" columnKey="SecondDeliveryDemandQuantity"></TD>
																</TR>
																<TR>
																	<TD style="HEIGHT: 25px">
																		<asp:Label id="Label18" runat="server" Font-Size="9pt">2차납기요구일</asp:Label></TD>
																	<TD style="HEIGHT: 25px">
																		<igsch:webdatechooser id="Webdatechooser2" runat="server" Width="100px" Height="20px" NullDateLabel=" "
																			Text="Null" columnKey="DeliveryRequestDate1">
																			<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
																				DayNameFormat="FirstLetter">
																				<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
																				<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																				<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																				<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
																			</CALENDARLAYOUT>
																			<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
																			<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
																			<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
																		</igsch:webdatechooser></TD>
																	<TD style="HEIGHT: 25px">
																		<asp:Label id="Label19" runat="server" Font-Size="9pt">3차납기요구량</asp:Label></TD>
																	<TD style="HEIGHT: 25px">
																		<INPUT id="igtbl_TextBox_0_10" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																			style="WIDTH: 100px; HEIGHT: 20px; TEXT-ALIGN: right" onfocus="OnFocus_Obj(this);"
																			type="text" size="11" value="0" Width="100px" columnKey="ThirdDeliveryDemandQuantity"></TD>
																	<TD style="HEIGHT: 25px">
																		<asp:Label id="Label20" runat="server" Font-Size="9pt">3차납기요구일</asp:Label></TD>
																	<TD style="HEIGHT: 25px">
																		<igsch:webdatechooser id="Webdatechooser3" runat="server" Width="100px" Height="20px" NullDateLabel=" "
																			Text="Null" columnKey="DeliveryRequestDate1">
																			<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
																				DayNameFormat="FirstLetter">
																				<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
																				<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																				<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																				<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
																			</CALENDARLAYOUT>
																			<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
																			<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
																			<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
																		</igsch:webdatechooser></TD>
																	<TD style="HEIGHT: 25px">
																		<asp:Label id="Label21" runat="server" Font-Size="9pt">4차납기요구량</asp:Label></TD>
																	<TD style="HEIGHT: 25px">
																		<INPUT id="igtbl_TextBox_0_12" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																			style="WIDTH: 100px; HEIGHT: 20px; TEXT-ALIGN: right" onfocus="OnFocus_Obj(this);"
																			type="text" size="11" value="0" Width="100px" columnKey="FourthDeliveryDemandQuantity"></TD>
																</TR>
																<TR>
																	<TD>
																		<asp:Label id="Label10" runat="server" Font-Size="9pt">4차납기요구일</asp:Label></TD>
																	<TD>
																		<igsch:webdatechooser id="Webdatechooser4" runat="server" Width="100px" Height="20px" NullDateLabel=" "
																			Text="Null" columnKey="DeliveryRequestDate1">
																			<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
																				DayNameFormat="FirstLetter">
																				<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
																				<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																				<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																				<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
																			</CALENDARLAYOUT>
																			<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
																			<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
																			<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
																		</igsch:webdatechooser></TD>
																	<TD>
																		<asp:Label id="Label11" runat="server" Font-Size="9pt">5차납기요구량</asp:Label></TD>
																	<TD>
																		<INPUT id="igtbl_TextBox_0_14" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																			style="WIDTH: 100px; HEIGHT: 20px; TEXT-ALIGN: right" onfocus="OnFocus_Obj(this);"
																			type="text" size="11" value="0" Width="100px" columnKey="FifthDeliveryDemandQuantity"></TD>
																	<TD>
																		<asp:Label id="Label12" runat="server" Font-Size="9pt">5차납기요구일</asp:Label></TD>
																	<TD>
																		<igsch:webdatechooser id="Webdatechooser5" runat="server" Width="100px" Height="20px" NullDateLabel=" "
																			Text="Null" columnKey="FirstDeliveryDemandDate">
																			<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
																				DayNameFormat="FirstLetter">
																				<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
																				<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
																				<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
																				<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
																			</CALENDARLAYOUT>
																			<DROPDOWNSTYLE BorderStyle="Inset"></DROPDOWNSTYLE>
																			<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
																			<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
																		</igsch:webdatechooser></TD>
																	<TD></TD>
																	<TD></TD>
																</TR>
																<TR height="50">
																	<TD align="right" colSpan="8" height="30" width="780">
																		<P>
																			<DIV id="lb_Source" style="DISPLAY: inline; FONT-WEIGHT: bold; COLOR: #990033" ms_positioning="FlowLayout"></DIV>
																		<P></P>
																		<P>
																			<DIV style="DISPLAY: inline; FONT-WEIGHT: bold; COLOR: #990033" ms_positioning="FlowLayout">&nbsp;</DIV>
																		<P></P>
																		<P>
																			<DIV style="DISPLAY: inline; FONT-WEIGHT: bold; COLOR: #990033" ms_positioning="FlowLayout">&nbsp;</DIV>
																		<P></P>
																		<P>
																			<DIV style="DISPLAY: inline; FONT-WEIGHT: bold; COLOR: #990033" ms_positioning="FlowLayout">&nbsp;</DIV>
																		<P></P>
																		<P>
																			<DIV style="DISPLAY: inline; FONT-WEIGHT: bold; COLOR: #990033" ms_positioning="FlowLayout">&nbsp;</DIV>
																		<P></P>
																	</TD>
																</TR>
															</TABLE>
															<P align="center">
																<INPUT id="igtbl_reOkBtn" style="WIDTH: 50px" onclick="igtbl_gRowEditButtonClick(event);__doPostBack('lnk_Update','');"
																	type="button" value="수정">&nbsp; <INPUT id="igtbl_reCancelBtn" style="WIDTH: 50px" onclick="igtbl_gRowEditButtonClick(event);"
																	type="button" value="취소"></P>
														</RowEditTemplate>
													</igtbl:UltraGridBand>
												</Bands>
											</igtbl:ultrawebgrid></FONT></TD>
								</TR>
								<TR>
									<TD height="25"><FONT face="굴림">&nbsp;</FONT><asp:button id="bt_Excel" runat="server" Height="20px" Width="60px" Text="Excel"></asp:button></TD>
									<TD align="right" height="25"><INPUT id="lb_Cost" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" value="0" name="Hidden1"
											runat="server"><INPUT id="lb_Total" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" size="3" value="0"
											runat="server"><asp:linkbutton id="lnk_Update" runat="server"></asp:linkbutton><INPUT id="lb_RowSelectIndex" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1"
											runat="server"><INPUT id="lb_RowIndex" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" runat="server"><INPUT id="chkAll" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" value="true" runat="server">
										<asp:button id="bt_Cancel" runat="server" Height="20px" Width="60px" Text="취소" Enabled="False"></asp:button>&nbsp;
										<asp:button id="bt_Delete" runat="server" Height="20px" Width="60px" Text="삭제"></asp:button>
										<asp:button id="bt_Stop" runat="server" Height="20px" Width="60px" Text="중단"></asp:button>&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			<igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter></form>
	</body>
</HTML>
