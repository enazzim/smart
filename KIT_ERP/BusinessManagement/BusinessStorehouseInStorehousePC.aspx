<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v1.1, Version=1.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Page language="c#" Codebehind="BusinessStorehouseInStorehousePC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BusinessManagement.BusinessStorehouseInStorehousePC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BusinessStorehouseInStorehousePC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--

		var GridName;
		
		function UltraWebGrid1_ColumnHeaderClickHandler(gridName, columnId, button)
		{
			GridName = gridName;
    			if(document.BusinessStorehouseInStorehousePC.chkAll.value == "true")
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
			document.BusinessStorehouseInStorehousePC.chkAll.value = "false";
		}


		function AllUncheck() // 전부체크 해제하기
		{
			var grid = igtbl_getGridById(GridName);
			
			for(var i=0; i<grid.Rows.length; i++)
			{
				var row = grid.Rows.getRow(i);
				row.getCellFromKey("chk").setValue(false);
			}
			document.BusinessStorehouseInStorehousePC.chkAll.value = "true";
		}
		
		//수정창이 열릴때
		function UltraWebGrid1_BeforeRowTemplateOpenHandler(gridName, rowId, templateId)
		{
			var row = igtbl_getRowById(rowId);
			var date = igedit_getById("UltraWebGrid1__ctl0_wdeInStoreDate", "igtxtUltraWebGrid1__ctl0_wdeInStoreDate");
			date.setValue(row.getCellFromKey("InStoreDate").getValue());
			
			document.BusinessStorehouseInStorehousePC.hd_year.value = date.getValue().getFullYear();
			document.BusinessStorehouseInStorehousePC.hd_month.value = date.getValue().getMonth()+1;
			
			//Add code to handle your event here.
			document.BusinessStorehouseInStorehousePC.lb_RowSelectIndex.value = document.BusinessStorehouseInStorehousePC.lb_RowIndex.value;
			//이전수량
			//document.BusinessStorehouseInStorehousePC.lb_quantity.value = document.getElementById(rowId).children[7].innerText;
			document.BusinessStorehouseInStorehousePC.lb_quantity.value = row.getCellFromKey("InStorehouseQuantity").getValue();
			
			for(var i=0;i<BusinessStorehouseInStorehousePC.UltraWebGrid1__ctl0_DropDownList1.options.length;i++)
			{
				if(BusinessStorehouseInStorehousePC.UltraWebGrid1__ctl0_DropDownList1.options[i].innerText == row.getCellFromKey("BusinessStorehouseNum").getValue())
				{
					BusinessStorehouseInStorehousePC.UltraWebGrid1__ctl0_DropDownList1.options[i].selected=true;
					
					//이전 영업창고번호 입력
					if(BusinessStorehouseInStorehousePC.UltraWebGrid1__ctl0_DropDownList1.options[i].selected==true)
					{
					
						if(row.getCellFromKey("BusinessStorehouseNum").getValue() == '영업1창고')
							document.BusinessStorehouseInStorehousePC.lb_Index.value = 1;
						else if(row.getCellFromKey("BusinessStorehouseNum").getValue() == '영업2창고')
							document.BusinessStorehouseInStorehousePC.lb_Index.value = 2;
						else 
							document.BusinessStorehouseInStorehousePC.lb_Index.value = 3;
					}
				}
			}
			

		}
		//창이 닫힐때
		function UltraWebGrid1_BeforeRowTemplateCloseHandler(gridName, rowId, bSaveChanges){
			if(event.srcElement.id == "igtbl_reOkBtn")
			{
				var row = igtbl_getRowById(rowId);
				var date = igedit_getById("UltraWebGrid1__ctl0_wdeInStoreDate", "igtxtUltraWebGrid1__ctl0_wdeInStoreDate");
				
				row.getCellFromKey("InStoreDate").setValue(date.getValue());
			}
		}
		
		
		function UltraWebGrid1_MouseOverHandler(gridName, id, button){
			  //Add code to handle your event here.  
			if(button == 0) 
			{ 
				// Are we over a cell  var cell = igtbl_getElementById(id);
				//cell.style.cursor = 'hand';  
				//var label = igtbl_getElementById("lb_RowIndex");  
				var parts = id.split("_");  
				document.BusinessStorehouseInStorehousePC.lb_RowIndex.value = parts[1];  
			}
		}
		
		
		function Process()	
		{
			//var frm = BusinessStorehouseInStorehouse.tb_InstorehouseQuantity;
			//var value = Number(BusinessStorehouseInStorehouse.lb_quantity.value) - Number(frm.value);
			//var sqTxt = QualityInspectionPresentCondition.txtSuccessQuantity;

			//		if(value < 0 )	
			//		{
			//			alert("- 의뢰수량이상 입고할수 없습니다!");
			//			frm.value = BusinessStorehouseInStorehouse.lb_quantity.value;
			//			frm.focus();
			//		}
					
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
		<form id="BusinessStorehouseInStorehousePC" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" height="550"
				cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD width="20"></TD>
					<TD style="PADDING-TOP: 10px" vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800">
								<tr>
									<td height="30" width="600" colSpan="4">
										<uc1:ItemSearchControl id="ItemSearchControl1" runat="server"></uc1:ItemSearchControl></td>
									<td height="30" align="right" width="200"><asp:linkbutton id="lnk_Update" runat="server" Visible="False">LinkButton</asp:linkbutton><asp:button id="bt_Clear" runat="server" Height="20px" Width="60px" Text="초기화"></asp:button><FONT face="굴림">&nbsp;</FONT>
										<asp:button id="bt_Search" runat="server" Height="20px" Width="60px" Text="검색"></asp:button><FONT face="굴림">&nbsp;
										</FONT>
									</td>
								</tr>
								<tr>
									<td height="30" align="right" width="70"><FONT face="굴림">입고일자&nbsp;</FONT></td>
									<td height="30" width="100"><igsch:webdatechooser id="wdc_FromDate" runat="server" BackColor="#EEEEE9" Height="20px" Width="100px"
											NullDateLabel=" " Text=" " BorderColor="DimGray" BorderStyle="Solid">
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
									<td height="30" align="center" width="15"><FONT face="굴림">~</FONT></td>
									<td height="30" width="415"><igsch:webdatechooser id="wdc_ToDate" runat="server" BackColor="#EEEEE9" Height="20px" Width="100px" NullDateLabel=" "
											Text=" " BorderColor="DimGray" BorderStyle="Solid">
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
									<td height="30" width="200"><FONT face="굴림"></FONT></td>
								</tr>
							</TABLE>
						</FIELDSET>
						<BR>
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top">[검색결과]</LEGEND>
							<TABLE id="Table3" style="HEIGHT: 435px" cellSpacing="0" cellPadding="0" width="800">
								<TR>
									<TD colSpan="2"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="386px" Width="800px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect"
												AllowUpdateDefault="RowTemplateOnly">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</AddNewBox>
												<Pager PageSize="16" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="#E0E0E0">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
												<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="386px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents ColumnHeaderClickHandler="UltraWebGrid1_ColumnHeaderClickHandler" BeforeRowTemplateCloseHandler="UltraWebGrid1_BeforeRowTemplateCloseHandler"
													BeforeRowTemplateOpenHandler="UltraWebGrid1_BeforeRowTemplateOpenHandler" MouseOverHandler="UltraWebGrid1_MouseOverHandler"></ClientSideEvents>
												<SelectedHeaderStyleDefault BackColor="#E0E5CD"></SelectedHeaderStyleDefault>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedGroupByRowStyleDefault Cursor="Hand" BackColor="#E0E5CD"></SelectedGroupByRowStyleDefault>
												<SelectedRowStyleDefault ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
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
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left">
																<Padding Left="2px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ItemDrawNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left">
																<Padding Left="2px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="순서" Key="ProcessSequenceNum" Width="30px" HeaderClickAction="SortMulti"
															BaseColumnName="ProcessSequenceNum">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정코드" Key="ProcessCode" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ProcessCode">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정명" Key="ProcessName" Width="80px" HeaderClickAction="SortMulti" BaseColumnName="ProcessName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="입고수량" Key="InStorehouseQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
															BaseColumnName="InStorehouseQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="입고일자" Key="InStoreDate" Format="yyyy-MM-dd" BaseColumnName="InStoreDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="영업창고" Key="BusinessStorehouseNum" HeaderClickAction="SortMulti" BaseColumnName="BusinessStorehouseNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ProgressCondition">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" Width="80px" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationPerson">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationPersonID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Width="80px" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" Width="80px" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingPerson">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingPersonID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Width="80px" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="입고의뢰원장번호" Key="InStorehouseRequestHistoryIndex" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="InStorehouseRequestHistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="번호" Key="InStorehouseHistoryIndex" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="InStorehouseHistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
													</Columns>
													<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
														<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
													</RowTemplateStyle>
													<RowEditTemplate>
														<FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림">
														</FONT><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림">
														</FONT><FONT face="굴림"></FONT><FONT style="BACKGROUND-COLOR: #ffffff" face="굴림"></FONT>
														<FONT face="굴림"></FONT><FONT face="굴림"></FONT>
														<BR>
														<TABLE style="WIDTH: 780px; HEIGHT: 54px" bgColor="#f7f6f6">
															<TR>
																<TD align="right">품목번호</TD>
																<TD align="left"><INPUT id="igtbl_TextBox_0_3" style="WIDTH: 90px; BACKGROUND-COLOR: #eeeee9" readOnly type="text"
																		columnKey="ItemName"></TD>
																<TD align="right">입고일자</TD>
																<TD align="left">
																	<igtxt:WebDateTimeEdit id="wdeInStoreDate" runat="server" Width="110px" Height="20px" BorderStyle="Solid"
																		BorderColor="DimGray" BackColor="#EEEEE9" BorderWidth="1px" HorizontalAlign="Center" DisplayModeFormat="d"
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
																<TD align="right">입고수량</TD>
																<TD align="left"><INPUT id="igtbl_TextBox_0_6" style="WIDTH: 90px; BACKGROUND-COLOR: #eeeee9" readOnly type="text"
																		columnKey="InStorehouseQuantity"></TD>
																<TD align="right">영업창고</TD>
																<TD align="left">
																	<asp:DropDownList id="DropDownList1" runat="server" Width="90px" BackColor="#EEEEE9" columnKey="BusinessStorehouseNum">
																		<asp:ListItem Value="0">-선 택-</asp:ListItem>
																		<asp:ListItem Value="1">영업1창고</asp:ListItem>
																		<asp:ListItem Value="2">영업2창고</asp:ListItem>
																		<asp:ListItem Value="3">영업3창고</asp:ListItem>
																	</asp:DropDownList></TD>
															</TR>
															<TR>
																<TD align="center" colSpan="10"><INPUT id="igtbl_reOkBtn" style="WIDTH: 60px; HEIGHT: 20px" onclick="igtbl_gRowEditButtonClick(event); __doPostBack('lnk_Update','')"
																		type="button" value="수정">&nbsp; <INPUT id="igtbl_reCancelBtn" style="WIDTH: 60px; HEIGHT: 20px" onclick="igtbl_gRowEditButtonClick(event);"
																		type="button" value="취소">
																</TD>
															</TR>
														</TABLE>
														<BR>
													</RowEditTemplate>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
								<TR>
									<TD align="left" height="30"><FONT face="굴림">&nbsp;</FONT><asp:button id="bt_Excel" runat="server" Height="20px" Width="60px" Text="Excel"></asp:button></TD>
									<TD align="right" height="30"><INPUT id="hd_year" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1" runat="server"><INPUT id="hd_month" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1" runat="server"><INPUT id="lb_RowIndex" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1"
											runat="server"><INPUT id="lb_RowSelectIndex" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1"
											runat="server"><INPUT id="lb_quantity" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" runat="server"><INPUT id="lb_Index" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" runat="server"><INPUT id="chkAll" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" value="true" runat="server">
										<asp:button id="bt_Delete" runat="server" Height="20px" Width="60px" Text="삭제"></asp:button>&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			<igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter></form>
	</body>
</HTML>
