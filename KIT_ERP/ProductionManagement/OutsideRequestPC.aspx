<%@ Page language="c#" Codebehind="OutsideRequestPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.OutsideRequestPC" codePage="949" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>OutsideRequestPC</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
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
			var objChooser1 = igdrp_getComboById("wdcFromDate");
			var objChooser2 = igdrp_getComboById("wdcToDate");
			objChooser1.setValue(null);
			objChooser2.setValue(null);
			document.Form1.ddlItemClassification1.options[0].selected=true;						
			document.Form1.btnCancel.disable = true;
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
		
		//수정창 열리기전에 이전 수량을 저장해두어야 한다
		function UltraWebGrid1_BeforeRowTemplateOpenHandler(gridName,rowId, templateId)
		{
			var row = igtbl_getRowById(rowId);	
						
			var combo1 = igdrp_getComboById("UltraWebGrid1xxctl0xwdcxFirstDeliveryDemandDate");
			var combo2 = igdrp_getComboById("UltraWebGrid1xxctl0xwdcxSecondDeliveryDemandDate");
			var combo3 = igdrp_getComboById("UltraWebGrid1xxctl0xwdcxThirdDeliveryDemandDate");
			var combo4 = igdrp_getComboById("UltraWebGrid1xxctl0xwdcxFourthDeliveryDemandDate");
			var combo5 = igdrp_getComboById("UltraWebGrid1xxctl0xwdcxFifthDeliveryDemandDate");
				
			var parts = rowId.split("_");
			
			if(row.getCellFromKey("ProgressCondition").getValue() != "대기")
			{
				alert("수정할 수 없는 항목 입니다.!");
				return true;
			}
			else
			{		
				document.Form1.lb_RowSelectIndex.value = parts[1];
				
				document.Form1.lb_Total.value = row.getCellFromKey("TotalCost").getValue();
				
				document.Form1.lb_FirstDeliveryDemandQuantity.value = row.getCellFromKey("FirstDeliveryDemandQuantity").getValue();
				document.Form1.lb_SecondDeliveryDemandQuantity.value = row.getCellFromKey("SecondDeliveryDemandQuantity").getValue();
				document.Form1.lb_ThirdDeliveryDemandQuantity.value = row.getCellFromKey("ThirdDeliveryDemandQuantity").getValue();
				document.Form1.lb_FourthDeliveryDemandQuantity.value = row.getCellFromKey("FourthDeliveryDemandQuantity").getValue();
				document.Form1.lb_FifthDeliveryDemandQuantity.value = row.getCellFromKey("FifthDeliveryDemandQuantity").getValue();
				
				
				combo1.setValue(row.getCellFromKey("FirstDeliveryDemandDate").getValue());
				if(row.getCellFromKey("SecondDeliveryDemandDate").getValue() !=null)
					combo2.setValue(row.getCellFromKey("SecondDeliveryDemandDate").getValue());
				if(row.getCellFromKey("ThirdDeliveryDemandDate").getValue() !=null)
					combo3.setValue(row.getCellFromKey("ThirdDeliveryDemandDate").getValue());	
				if(row.getCellFromKey("FourthDeliveryDemandDate").getValue() !=null)
					combo4.setValue(row.getCellFromKey("FourthDeliveryDemandDate").getValue());	
				if(row.getCellFromKey("FifthDeliveryDemandDate").getValue() !=null)
					combo5.setValue(row.getCellFromKey("FifthDeliveryDemandDate").getValue());
			}
		}
		
		//편집창이 닫힐때
		function UltraWebGrid1_AfterRowTemplateCloseHandler(gridName, rowId, bSaveChanges)
		{
			var row = igtbl_getRowById(rowId);	
			var frm = document.Form1;	
						
			var combo1 = igdrp_getComboById("UltraWebGrid1xxctl0xwdcxFirstDeliveryDemandDate");
			var combo2 = igdrp_getComboById("UltraWebGrid1xxctl0xwdcxSecondDeliveryDemandDate");
			var combo3 = igdrp_getComboById("UltraWebGrid1xxctl0xwdcxThirdDeliveryDemandDate");
			var combo4 = igdrp_getComboById("UltraWebGrid1xxctl0xwdcxFourthDeliveryDemandDate");
			var combo5 = igdrp_getComboById("UltraWebGrid1xxctl0xwdcxFifthDeliveryDemandDate");
			
			if(event.srcElement.id == "igtbl_reOkBtn")
			{			
				b =  new Array(5);//1차부터 5차까지의 납품요구일을 b 배열에 넣기 위해 배열선언
				
				//배열b에 1차부터 5차까지의 납기요구일을 넣음
				b[0] =  combo1.getValue();
				b[1] =  combo2.getValue();
				b[2] =  combo3.getValue();
				b[3] =  combo4.getValue();
				b[4] =  combo5.getValue();
				
				Check = true;
				for(i = 0; i < b.length; i++)
				{
					if(Date.parse(b[i]) > Date.parse(b[i+1]))
					{											
						alert("잘못 입력된 납기요구일이 있습니다. 다시 수정 해 주세요!!");												
						Check = false;
					}				
				}
				
				if(Check)
				{				
					//1차부터 5차까지의 납품요구일이 제대로 입력이 되고 그리드에 수정된 내용이 들어간다.
					row.getCellFromKey("FirstDeliveryDemandDate").setValue(combo1.getValue());
					row.getCellFromKey("SecondDeliveryDemandDate").setValue(combo2.getValue());
					row.getCellFromKey("ThirdDeliveryDemandDate").setValue(combo3.getValue());
					row.getCellFromKey("FourthDeliveryDemandDate").setValue(combo4.getValue());
					row.getCellFromKey("FifthDeliveryDemandDate").setValue(combo5.getValue());
					
					//1차부터 5차까지의 납품요구량이 제대로 입력이 되고 그리드에 수정된 내용이 들어간다.
					row.getCellFromKey("FirstDeliveryDemandQuantity").setValue(frm.UltraWebGrid1__ctl0_tb_FirstDeliveryDemandQuantity.value);
					row.getCellFromKey("SecondDeliveryDemandQuantity").setValue(frm.UltraWebGrid1__ctl0_tb_SecondDeliveryDemandQuantity.value);
					row.getCellFromKey("ThirdDeliveryDemandQuantity").setValue(frm.UltraWebGrid1__ctl0_tb_ThirdDeliveryDemandQuantity.value);
					row.getCellFromKey("FourthDeliveryDemandQuantity").setValue(frm.UltraWebGrid1__ctl0_tb_FourthDeliveryDemandQuantity.value);
					row.getCellFromKey("FifthDeliveryDemandQuantity").setValue(frm.UltraWebGrid1__ctl0_tb_FifthDeliveryDemandQuantity.value);
														
					__doPostBack('lnk_Update','');	
					return true;				
				}
				else
				{		
					return false;					
				}
			}
		}
		
		function Process()	
		{
			var Quantity = Number(document.Form1.UltraWebGrid1__ctl0_tb_FirstDeliveryDemandQuantity.value) + Number(document.Form1.UltraWebGrid1__ctl0_tb_SecondDeliveryDemandQuantity.value) +Number(document.Form1.UltraWebGrid1__ctl0_tb_ThirdDeliveryDemandQuantity.value) + Number(document.Form1.UltraWebGrid1__ctl0_tb_FourthDeliveryDemandQuantity.value) + Number(document.Form1.UltraWebGrid1__ctl0_tb_FifthDeliveryDemandQuantity.value);
			
			if(Quantity > Number(document.Form1.UltraWebGrid1__ctl0_tb_OrderQuantity.value) )	
			{
				alert("- 총수량이 의뢰수량보다 많습니다!");
				document.Form1.UltraWebGrid1__ctl0_tb_FirstDeliveryDemandQuantity.value = document.Form1.lb_FirstDeliveryDemandQuantity.value;
				document.Form1.UltraWebGrid1__ctl0_tb_SecondDeliveryDemandQuantity.value = document.Form1.lb_SecondDeliveryDemandQuantity.value;
				document.Form1.UltraWebGrid1__ctl0_tb_ThirdDeliveryDemandQuantity.value = document.Form1.lb_ThirdDeliveryDemandQuantity.value;
				document.Form1.UltraWebGrid1__ctl0_tb_FourthDeliveryDemandQuantity.value = document.Form1.lb_FourthDeliveryDemandQuantity.value;
				document.Form1.UltraWebGrid1__ctl0_tb_FifthDeliveryDemandQuantity.value = document.Form1.lb_FifthDeliveryDemandQuantity.value;
				document.Form1.UltraWebGrid1__ctl0_tb_FirstDeliveryDemandQuantity.focus();
			}
		}
		
		
		function OnKeyDown_Float(obj)
		{   
			if (event.ctrlKey || event.shiftKey || event.altKey)   
			{
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

		function OnFocus_Obj(obj) 
		{
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
		function OnKeyDown_Currency(obj) 
		{   
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
		
		function OnKeyUp_Currency(obj)		
		{
			sFieldValue = new String(obj.value);
			sFieldValue = sFieldValue.replace(/,/gi,"");
			if ( IsNumericKey() )		{
				SetEventPass();
				return true;
			}
			obj.value = MakeCommaSeparate(sFieldValue);
		}
		
		function OnBlur_Cur(obj)	
		{
			if (ValidateCurrency(obj.value) == false) {
				obj.value = "";
    			return false;
			}
		}
		
		function ValidateCurrency( a_sValue ) 
		{	
			var s_FieldValue = new String(a_sValue);
			
			if (isNaN(parseFloat(s_FieldValue)))
				return false;
				
			return true;
		}

		// OnKeyUp_Currency 에서 호출할 소수점 찍기
		function MakeCommaSeparate(a_Value)	
		{
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
		
		function IsNumericKey()  
		{
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
							
		function SetEventPass() 
		{
				event.cancelBubble = false;
				event.returnValue = true;
		}

		function SetEventCancel() 
		{
				event.cancelBubble = true;
				event.returnValue = false;
		}
		
		
--></SCRIPT>
	</HEAD>
	<body bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="LEFT: 10px; WIDTH: 800px; POSITION: absolute; TOP: 10px" cellSpacing="0"
				cellPadding="0" width="800" border="0">
				<TR>
					<TD style="WIDTH: 10px; HEIGHT: 550px" width="10"></TD>
					<TD vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" style="WIDTH: 800px; HEIGHT: 35px" height="35" cellSpacing="0" cellPadding="0"
								width="800" border="0" DESIGNTIMEDRAGDROP="66">
								<tr>
									<td colSpan="8">
										<table cellSpacing="0" cellPadding="0" width="800" border="0">
											<TR>
												<TD vAlign="middle" align="left" width="200" height="30"><uc1:companysearchcontrol id="CSC1" runat="server"></uc1:companysearchcontrol></TD>
												<TD vAlign="middle" align="left" height="30"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></TD>
											</TR>
										</table>
									</td>
								</tr>
								<TR>
									<TD vAlign="middle" align="right" width="70" height="30">의뢰일&nbsp;
									</TD>
									<TD vAlign="middle" align="left" width="100" height="30"><igsch:webdatechooser id="wdcFromDate" runat="server" BorderStyle="Solid" BorderColor="DimGray" BackColor="#EEEEE9"
											NullDateLabel=" " Text=" " Width="100px" Height="18px" Font-Size="10pt">
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
									<TD vAlign="middle" align="left" width="100" height="30"><igsch:webdatechooser id="wdcToDate" runat="server" BorderStyle="Solid" BorderColor="DimGray" BackColor="#EEEEE9"
											NullDateLabel=" " Text=" " Width="100px" Height="18px" Font-Size="10pt">
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
									<TD vAlign="middle" align="right" width="70" height="30">제품팀&nbsp;</TD>
									<TD vAlign="middle" align="left" width="130" height="30"><asp:dropdownlist id="ddlItemClassification1" runat="server" BackColor="#EEEEE9" Width="100px"></asp:dropdownlist></TD>
									<TD vAlign="middle" align="right" width="415" colSpan="2" height="30"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
											type="button" value="초기화" name="btnReset">
										<asp:button id="bt_Search" runat="server" Text="검   색" Width="60px" Height="20px" Font-Size="10pt"></asp:button>&nbsp;&nbsp;
									</TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" width="70" colSpan="8" height="8"></TD>
								</TR>
							</TABLE>
						</FIELDSET>
						<BR>
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[검색결과]</LEGEND>
							<TABLE id="Table3" style="WIDTH: 800px" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD style="HEIGHT: 28px" width="343" height="28"></TD>
									<TD style="WIDTH: 35px; HEIGHT: 28px" align="right" width="35" height="28"></TD>
									<TD align="right"><INPUT id="Volum" style="WIDTH: 26px; HEIGHT: 12px" type="hidden" size="1" value="0" name="Hidden1"
											runat="server"><INPUT id="lb_Index" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1"
											runat="server"><asp:button id="btnPre" runat="server" Text="◀" Width="16px" Height="16px" Font-Size="8pt"></asp:button><asp:button id="btnNow" runat="server" Text="■" Width="16px" Height="16px" Font-Size="8pt"></asp:button><asp:button id="btnNext" runat="server" Text="▶" Width="16px" Height="16px" Font-Size="8pt"></asp:button>&nbsp;&nbsp;</TD>
								</TR>
								<TR>
									<TD vAlign="top" align="center" colSpan="3"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="100%" Height="385px">
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
													BorderColor="DimGray" BorderStyle="Solid" BackColor="Silver" Height="385px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents ColumnHeaderClickHandler="UltraWebGrid1_ColumnHeaderClickHandler" BeforeRowTemplateOpenHandler="UltraWebGrid1_BeforeRowTemplateOpenHandler"
													AfterRowTemplateCloseHandler="UltraWebGrid1_AfterRowTemplateCloseHandler"></ClientSideEvents>
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
													BorderStyle="Solid" ForeColor="Black" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails ColorTop="Gray" WidthLeft="0px" WidthTop="0px" ColorLeft="Gray"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="선택" Key="chk" Width="35px" Type="CheckBox" BaseColumnName="chk" AllowUpdate="Yes">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" BaseColumnName="ItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" BaseColumnName="ItemDrawNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" BaseColumnName="ItemName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단가구분" Key="UnitCostDistinction" Hidden="True" Format="" BaseColumnName="UnitCostDistinction">
															<CellStyle VerticalAlign="Top" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="시작공정코드" Key="BeginProcessCode" Hidden="True" BaseColumnName="BeginProcessCode">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="시작공정" Key="BeginProcess" Width="70px" BaseColumnName="BeginProcess">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="종료공정코드" Key="EndProcessCode" Hidden="True" BaseColumnName="EndProcessCode">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="종료공정" Key="EndProcess" Width="70px" BaseColumnName="EndProcess">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래처" Key="CompanyName" BaseColumnName="CompanyName"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총의뢰량" Key="OrderQuantity" Format="###,###,###.##" BaseColumnName="OrderQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="납기요구일" Key="FirstDeliveryDemandDate" Format="yyyy-MM-dd" BaseColumnName="FirstDeliveryDemandDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Hidden="True" Format="\ ###,###,##0" BaseColumnName="ApplyUnitCost">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="1차납기요구량" Key="FirstDeliveryDemandQuantity" Hidden="True" Format="###,###,###.##"
															BaseColumnName="FirstDeliveryDemandQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="2차납기요구량" Key="SecondDeliveryDemandQuantity" Hidden="True" Format="###,###,###.##"
															BaseColumnName="SecondDeliveryDemandQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="2차납기요구일" Key="SecondDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
															BaseColumnName="SecondDeliveryDemandDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="3차납기요구량" Key="ThirdDeliveryDemandQuantity" Hidden="True" Format="###,###,###.##"
															BaseColumnName="ThirdDeliveryDemandQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="3차납기요구일" Key="ThirdDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
															BaseColumnName="ThirdDeliveryDemandDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="4차납기요구량" Key="FourthDeliveryDemandQuantity" Hidden="True" Format="###,###,###.##"
															BaseColumnName="FourthDeliveryDemandQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="4차납기요구일" Key="FourthDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
															BaseColumnName="FourthDeliveryDemandDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="5차납기요구량" Key="FifthDeliveryDemandQuantity" Hidden="True" Format="###,###,###.##"
															BaseColumnName="FifthDeliveryDemandQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="5차납기요구일" Key="FifthDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
															BaseColumnName="FifthDeliveryDemandDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총 금액" Key="TotalCost" Hidden="True" Format="\ ###,###,##0" BaseColumnName="TotalCost">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="볼륨번호" Key="VolumNum" Hidden="True" BaseColumnName="VolumNum">
															<SelectedCellStyle VerticalAlign="Middle" HorizontalAlign="Center"></SelectedCellStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" BaseColumnName="ProgressCondition">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" BaseColumnName="RegistrationPerson">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" BaseColumnName="UpdatingPerson">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="생산계획번호" Key="ProductionPlanHistoryIndex" BaseColumnName="ProductionPlanHistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업계획번호" Key="WCDailyWorkPlanHistoryIndex" Hidden="True" Format="" BaseColumnName="WCDailyWorkPlanHistoryIndex"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="외주의뢰번호" Key="OutSideOrderRequestHistoryIndex" BaseColumnName="OutSideOrderRequestHistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
													</Columns>
													<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
														<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
													</RowTemplateStyle>
													<RowEditTemplate>
														<TABLE id="Table8" style="FONT-SIZE: 10pt; WIDTH: 800px; HEIGHT: 30px">
															<TR>
																<TD align="right">품목번호</TD>
																<TD>
																	<asp:TextBox id="TextBox3" runat="server" Width="90px" columnkey="ItemNum" ReadOnly="True"></asp:TextBox></TD>
																<TD align="right">도면번호</TD>
																<TD>
																	<asp:TextBox id="TextBox2" runat="server" Width="90px" columnkey="ItemDrawNum" ReadOnly="True"></asp:TextBox></TD>
																<TD align="right">품목명</TD>
																<TD>
																	<asp:TextBox id="TextBox1" runat="server" Width="90px" columnkey="ItemName" ReadOnly="True"></asp:TextBox></TD>
																<TD align="right">총 의뢰량</TD>
																<TD>
																	<asp:TextBox id="tb_OrderQuantity" runat="server" Width="90px" columnkey="OrderQuantity" ReadOnly="True"></asp:TextBox></TD>
															</TR>
															<TR>
																<TD align="right">1차납기요구량</TD>
																<TD>
																	<asp:TextBox id="tb_FirstDeliveryDemandQuantity" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																		onkeyup="return Process();" runat="server" Width="90px" columnkey="FirstDeliveryDemandQuantity"></asp:TextBox></TD>
																<TD align="right">1차납기요구일</TD>
																<TD>
																	<igsch:webdatechooser id="wdc_FirstDeliveryDemandDate" runat="server" Font-Size="10pt" Height="18px" Width="100px"
																		Text="" NullDateLabel=" " columnkey="FirstDeliveryDemandDate">
																		<CALENDARLAYOUT DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
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
																<TD align="right">2차납기요구량</TD>
																<TD>
																	<asp:TextBox id="tb_SecondDeliveryDemandQuantity" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																		onkeyup="return Process();" onfocus="OnFocus_Obj(this);" runat="server" Width="90px" columnkey="SecondDeliveryDemandQuantity"></asp:TextBox></TD>
																<TD align="right">2차납기요구일</TD>
																<TD>
																	<igsch:webdatechooser id="wdc_SecondDeliveryDemandDate" runat="server" Font-Size="10pt" Height="18px"
																		Width="100px" Text="날짜 선택" NullDateLabel=" " columnkey="SecondDeliveryDemandDate">
																		<CALENDARLAYOUT DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
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
																<TD align="right">3차납기요구량</TD>
																<TD>
																	<asp:TextBox id="tb_ThirdDeliveryDemandQuantity" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																		onkeyup="return Process();" onfocus="OnFocus_Obj(this);" runat="server" Width="90px" columnkey="ThirdDeliveryDemandQuantity"></asp:TextBox></TD>
																<TD align="right">3차납기요구일</TD>
																<TD>
																	<igsch:webdatechooser id="wdc_ThirdDeliveryDemandDate" runat="server" Font-Size="10pt" Height="18px" Width="100px"
																		Text="날짜 선택" NullDateLabel=" " columnkey="ThirdDeliveryDemandDate">
																		<CALENDARLAYOUT DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
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
																<TD align="right">4차납기요구량</TD>
																<TD>
																	<asp:TextBox id="tb_FourthDeliveryDemandQuantity" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																		onkeyup="return Process();" onfocus="OnFocus_Obj(this);" runat="server" Width="90px" columnkey="FourthDeliveryDemandQuantity"></asp:TextBox></TD>
																<TD align="right">4차납기요구일</TD>
																<TD>
																	<igsch:webdatechooser id="wdc_FourthDeliveryDemandDate" runat="server" Font-Size="10pt" Height="18px"
																		Width="100px" Text="날짜 선택" NullDateLabel=" " columnkey="FourthDeliveryDemandDate">
																		<CALENDARLAYOUT DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
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
																<TD style="HEIGHT: 30px" align="right">5차납기요구량</TD>
																<TD style="HEIGHT: 30px">
																	<asp:TextBox id="tb_FifthDeliveryDemandQuantity" onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Float(this);"
																		onkeyup="return Process();" onfocus="OnFocus_Obj(this);" runat="server" Width="90px" columnkey="FifthDeliveryDemandQuantity"></asp:TextBox></TD>
																<TD style="HEIGHT: 30px" align="right">5차납기요구일</TD>
																<TD style="HEIGHT: 30px">
																	<igsch:webdatechooser id="wdc_FifthDeliveryDemandDate" runat="server" Font-Size="10pt" Height="18px" Width="100px"
																		Text="날짜 선택" NullDateLabel=" " columnkey="FifthDeliveryDemandDate">
																		<CALENDARLAYOUT DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
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
																<TD align="right"></TD>
																<TD align="right">
																	<INPUT id="igtbl_reOkBtn" style="WIDTH: 50px" onclick="igtbl_gRowEditButtonClick(event);"
																		type="button" value="OK"></TD>
																<TD>
																	<INPUT id="igtbl_reCancelBtn" style="WIDTH: 50px" onclick="igtbl_gRowEditButtonClick(event);"
																		type="button" value="Cancel"></TD>
															</TR>
														</TABLE>
													</RowEditTemplate>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 40px">&nbsp;<asp:button id="bt_Excel" runat="server" Text="Excel" Width="60px" Height="20px" Font-Size="10pt"></asp:button><INPUT id="chkAll" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" value="true"
											name="Hidden1" runat="server">
										<asp:linkbutton id="lnk_Update" runat="server" Visible="False"></asp:linkbutton><INPUT id="lb_FirstDeliveryDemandQuantity" style="WIDTH: 24px; HEIGHT: 22px" type="hidden"
											size="1" name="Hidden1" runat="server"><INPUT id="lb_ThirdDeliveryDemandQuantity" style="WIDTH: 24px; HEIGHT: 22px" type="hidden"
											size="1" name="Hidden1" runat="server"><INPUT id="lb_FourthDeliveryDemandQuantity" style="WIDTH: 24px; HEIGHT: 22px" type="hidden"
											size="1" name="Hidden1" runat="server"><INPUT id="lb_SecondDeliveryDemandQuantity" style="WIDTH: 24px; HEIGHT: 22px" type="hidden"
											size="1" name="Hidden1" runat="server"><INPUT id="lb_FifthDeliveryDemandQuantity" style="WIDTH: 24px; HEIGHT: 22px" type="hidden"
											size="1" name="Hidden1" runat="server"><INPUT id="lb_RowSelectIndex" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1"
											name="Hidden1" runat="server"><INPUT id="lb_Total" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" runat="server"></TD>
									<TD style="HEIGHT: 40px" vAlign="middle" align="right" colSpan="2">&nbsp;&nbsp;&nbsp;
										<asp:button id="bt_Delete" runat="server" Text="삭   제" Width="60px" Height="20px" Font-Size="10pt"></asp:button>&nbsp;
										<asp:button id="btnCancel" runat="server" Text="취   소" Width="60px" Height="20px" Font-Size="10pt"
											Enabled="False"></asp:button>&nbsp;
										<asp:button id="bt_Stop" runat="server" Text="중   단" Width="60px" Height="20px" Font-Size="10pt"></asp:button>&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			<igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter></form>
	</body>
</HTML>
