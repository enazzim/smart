<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v1.1, Version=1.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="RowMaterialRequriement.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.RowMaterialRequriement" codePage="949" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RowMaterialRequriement</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript">
		<!--
		
		function ResettxtBox()
		{
			ResetTextBox();
			var objChooser1 = igdrp_getComboById("wdcDemandDate");
			objChooser1.setValue(null);
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
			var first, second, third, fourth, fifth;
			
			// 1차 납기량
			if(Form1.txtFirstDeliveryDemandQuantity != null || Form1.txtFirstDeliveryDemandQuantity.value != "")
				first = Number(RemoveComma(Form1.txtFirstDeliveryDemandQuantity.value));
			else
				first = 0;
			
			// 2차 납기량
			if(Form1.txtSecondDeliveryDemandQuantity != null || Form1.txtSecondDeliveryDemandQuantity.value != "")
				second = Number(RemoveComma(Form1.txtSecondDeliveryDemandQuantity.value));
			else
				second = 0;
			
			// 3차 납기량
			if(Form1.txtThirdDeliveryDemandQuantity != null || Form1.txtThirdDeliveryDemandQuantity.value != "")
				third = Number(RemoveComma(Form1.txtThirdDeliveryDemandQuantity.value));
			else
				third = 0;
			
			// 4차 납기량
			if(Form1.txtFourthDeliveryDemandQuantity != null || Form1.txtFourthDeliveryDemandQuantity.value != "")
				fourth = Number(RemoveComma(Form1.txtFourthDeliveryDemandQuantity.value));
			else
				fourth = 0;
			
			// 5차 납기량
			if(Form1.txtFifthDeliveryDemandQuantity != null || Form1.txtFifthDeliveryDemandQuantity.value != "")
				fifth = Number(RemoveComma(Form1.txtFifthDeliveryDemandQuantity.value));
			else
				fifth = 0;
				
			value = first + second + third + fourth + fifth;
				
			Form1.txtOrderQuantity.value = MakeCommaSeparate((Math.round((value * 100)) / 100));
			Form1.txtTotalCost.value = MakeCommaSeparate((Math.round((value * Form1.ApplyUnitCost.value * 100)) / 100));
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

		function uwgBR_HT_BeforeRowTemplateOpenHandler(gridName, rowId, templateId)
		{
			var row = igtbl_getRowById(rowId);
			var date1 = igedit_getById("uwgBR_HT__ctl0_wdeFirstDeliveryDemandDate", null);
			var date2 = igedit_getById("uwgBR_HT__ctl0_wdeSecondDeliveryDemandDate", null);
			var date3 = igedit_getById("uwgBR_HT__ctl0_wdeThirdDeliveryDemandDate", null);
			var date4 = igedit_getById("uwgBR_HT__ctl0_wdeFourthDeliveryDemandDate", null);
			var date5 = igedit_getById("uwgBR_HT__ctl0_wdeFifthDeliveryDemandDate", null);
			
			date1.setValue(row.getCellFromKey("FirstDeliveryDemandDate").getValue());
			date2.setValue(row.getCellFromKey("SecondDeliveryDemandDate").getValue());
			date3.setValue(row.getCellFromKey("ThirdDeliveryDemandDate").getValue());
			date4.setValue(row.getCellFromKey("FourthDeliveryDemandDate").getValue());
			date5.setValue(row.getCellFromKey("FifthDeliveryDemandDate").getValue());
			Form1.OrderQuanatity.value = MakeCommaSeparate(row.getCellFromKey("OrderQuantity").getValue());
			Form1.ApplyUnitCost.value = MakeCommaSeparate(row.getCellFromKey("ApplyUnitCost").getValue());
			Form1.TotalCost.value = MakeCommaSeparate(row.getCellFromKey("TotalCost").getValue());
			Form1.txtOrderQuantity.value = MakeCommaSeparate(row.getCellFromKey("OrderQuantity").getValue());
			Form1.txtTotalCost.value = MakeCommaSeparate(row.getCellFromKey("TotalCost").getValue());
			if(row.getCellFromKey("FirstDeliveryDemandQuantity").getValue() == null)
				Form1.txtFirstDeliveryDemandQuantity.value = "";
			else
				Form1.txtFirstDeliveryDemandQuantity.value = MakeCommaSeparate(row.getCellFromKey("FirstDeliveryDemandQuantity").getValue());
			if(row.getCellFromKey("SecondDeliveryDemandQuantity").getValue() == null)
				Form1.txtSecondDeliveryDemandQuantity.value = "";
			else
				Form1.txtSecondDeliveryDemandQuantity.value = MakeCommaSeparate(row.getCellFromKey("SecondDeliveryDemandQuantity").getValue());
			if(row.getCellFromKey("ThirdDeliveryDemandQuantity").getValue() == null)
				Form1.txtThirdDeliveryDemandQuantity.value = "";
			else
				Form1.txtThirdDeliveryDemandQuantity.value = MakeCommaSeparate(row.getCellFromKey("ThirdDeliveryDemandQuantity").getValue());
			if(row.getCellFromKey("FourthDeliveryDemandQuantity").getValue() == null)
				Form1.txtFourthDeliveryDemandQuantity.value = "";
			else
				Form1.txtFourthDeliveryDemandQuantity.value = MakeCommaSeparate(row.getCellFromKey("FourthDeliveryDemandQuantity").getValue());
			if(row.getCellFromKey("FifthDeliveryDemandQuantity").getValue() == null)
				Form1.txtFifthDeliveryDemandQuantity.value = "";
			else
				Form1.txtFifthDeliveryDemandQuantity.value = MakeCommaSeparate(row.getCellFromKey("FifthDeliveryDemandQuantity").getValue());
		}
		
		function uwgBR_HT_BeforeRowTemplateCloseHandler(gridName, rowId, bSaveChanges)
		{
			if(event.srcElement.id == "igtbl_reOkBtn")
			{
				var row =  igtbl_getRowById(rowId);
				var date1 = igedit_getById("uwgBR_HT__ctl0_wdeFirstDeliveryDemandDate", null);
				var date2 = igedit_getById("uwgBR_HT__ctl0_wdeSecondDeliveryDemandDate", null);
				var date3 = igedit_getById("uwgBR_HT__ctl0_wdeThirdDeliveryDemandDate", null);
				var date4 = igedit_getById("uwgBR_HT__ctl0_wdeFourthDeliveryDemandDate", null);
				var date5 = igedit_getById("uwgBR_HT__ctl0_wdeFifthDeliveryDemandDate", null);
				
				quantity =  new Array(5);//1차부터 5차까지의 납품요구량을 a 배열에 넣기 위해 배열선언
				date =  new Array(5);//1차부터 5차까지의 납품요구일을 b 배열에 넣기 위해 배열선언
				
				var first, second, third, fourth, fifth;
				
				// 1차 납기량
				if(Form1.txtFirstDeliveryDemandQuantity != null || Form1.txtFirstDeliveryDemandQuantity.value != "")
					quantity[0] = Number(RemoveComma(Form1.txtFirstDeliveryDemandQuantity.value));
				else
					quantity[0] = 0;
				
				// 2차 납기량
				if(Form1.txtSecondDeliveryDemandQuantity != null || Form1.txtSecondDeliveryDemandQuantity.value != "")
					quantity[1] = Number(RemoveComma(Form1.txtSecondDeliveryDemandQuantity.value));
				else
					quantity[1] = 0;
				
				// 3차 납기량
				if(Form1.txtThirdDeliveryDemandQuantity != null || Form1.txtThirdDeliveryDemandQuantity.value != "")
					quantity[2] = Number(RemoveComma(Form1.txtThirdDeliveryDemandQuantity.value));
				else
					quantity[2] = 0;
				
				// 4차 납기량
				if(Form1.txtFourthDeliveryDemandQuantity != null || Form1.txtFourthDeliveryDemandQuantity.value != "")
					quantity[3] = Number(RemoveComma(Form1.txtFourthDeliveryDemandQuantity.value));
				else
					quantity[3] = 0;
				
				// 5차 납기량
				if(Form1.txtFifthDeliveryDemandQuantity != null || Form1.txtFifthDeliveryDemandQuantity.value != "")
					quantity[4] = Number(RemoveComma(Form1.txtFifthDeliveryDemandQuantity.value));
				else
					quantity[4] = 0;
					
				value = first + second + third + fourth + fifth;
				
				//배열b에 1차부터 5차까지의 납기요구일을 넣음
				date[0] = date1.getValue();
				date[1] = date2.getValue();
				date[2] = date3.getValue();
				date[3] = date4.getValue();
				date[4] = date5.getValue();
				
				Check = true;
				for(i = 0; i < quantity.length; i++)
				{
					if(quantity[i] == 0)
					{
						if(date[i] == null || date[i] == "")
						{
							if(i != quantity.length-1)
							{
								if(quantity[i] < quantity[i+1])
								{
									Check = false;
									alert((i+1) + "차 납기량이 입력되지 않았습니다.");
									break;
								}
							}						
						}
						else
						{
							Check = false;
							alert((i+1) + "차 납기일이 잘못 입력되었습니다.");
							break;
						}
					}
					else
					{
						if(date[i] == null || date[i] == "")
						{
							Check = false;
							alert((i+1) + "차 납기일이 입력되지 않았습니다.");
							break;
						}
						if(i != date.length-1)
						{
							if(Date.parse(date[i]) > Date.parse(date[i+1]))
							{
								Check = false;
								alert((i+1) + "차 납기일이 잘못 입력되었습니다.");
								break;
							}
						}						
					}
				}
			
				if(Check)
				{
					if(Number(RemoveComma(Form1.OrderQuanatity.value)) == Number(RemoveComma(Form1.txtOrderQuantity.value)))
					{
						//1차부터 5차까지의 납품요구일이 제대로 입력이 되고 그리드에 수정된 내용이 들어간다.
						row.getCellFromKey("FirstDeliveryDemandDate").setValue(date1.getValue());
						row.getCellFromKey("SecondDeliveryDemandDate").setValue(date2.getValue());
						row.getCellFromKey("ThirdDeliveryDemandDate").setValue(date3.getValue());
						row.getCellFromKey("FourthDeliveryDemandDate").setValue(date4.getValue());
						row.getCellFromKey("FifthDeliveryDemandDate").setValue(date5.getValue());
						
						//1차부터 5차까지의 납품요구량이 제대로 입력이 되고 그리드에 수정된 내용이 들어간다.
						row.getCellFromKey("FirstDeliveryDemandQuantity").setValue(Number(RemoveComma(Form1.txtFirstDeliveryDemandQuantity.value)));
						row.getCellFromKey("SecondDeliveryDemandQuantity").setValue(Number(RemoveComma(Form1.txtSecondDeliveryDemandQuantity.value)));
						row.getCellFromKey("ThirdDeliveryDemandQuantity").setValue(Number(RemoveComma(Form1.txtThirdDeliveryDemandQuantity.value)));
						row.getCellFromKey("FourthDeliveryDemandQuantity").setValue(Number(RemoveComma(Form1.txtFourthDeliveryDemandQuantity.value)));
						row.getCellFromKey("FifthDeliveryDemandQuantity").setValue(Number(RemoveComma(Form1.txtFifthDeliveryDemandQuantity.value)));
					}
					else
					{
						alert("의뢰된 총구매량과 수정된 총구매량이 일치하지 않습니다.");
					}
							
				}
			}
		}
		function uwgBR_HT_ColumnHeaderClickHandler(gridName, columnId, button){
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
	<body bgColor="#f7f6f6" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="800" border="0" style="LEFT: 10px; POSITION: absolute; TOP: 10px">
				<tr>
					<td>
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD align="left" height="30" width="600">
										<uc1:ItemSearchControl id="ItemSearchControl1" runat="server"></uc1:ItemSearchControl>
									</TD>
									<td align="right" width="200"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
											type="button" value="초기화" name="btnReset">&nbsp;
										<asp:button id="btnSearch" runat="server" BorderStyle="Outset" Width="60px" Height="20px" Text="검   색"
											Font-Size="9pt" CommandName="Search"></asp:button>&nbsp;
									</td>
								</TR>
							</TABLE>
						</FIELDSET>
					</td>
				</tr>
				<TR>
					<TD height="10"></TD>
				</TR>
				<TR>
					<TD>
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top" style="WIDTH: 56px; HEIGHT: 14px">[검색현황]</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD vAlign="top" align="center" width="70%" height="30"></TD>
									<TD style="FONT-SIZE: 9pt" align="right" width="20%" height="30">최종납기일&nbsp;</TD>
									<TD style="PADDING-RIGHT: 10px" align="right" height="30">
										<igsch:webdatechooser id="wdcDemandDate" runat="server" BorderColor="DimGray" BorderStyle="Solid" BackColor="#EEEEE9"
											Width="100px" Height="18px" Text=" " NullDateLabel=" " Font-Size="10pt">
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
								</TR>
								<TR>
									<TD style="FONT-SIZE: 0pt" vAlign="top" align="center" width="100%" colSpan="3" height="300">
										<igtbl:ultrawebgrid id="uwgBR_HT" runat="server" Width="100%" Height="400px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="uwgBRxHT" TableLayout="Fixed" CellClickActionDefault="RowSelect">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</AddNewBox>
												<Pager>
													<Style VerticalAlign="Middle" BorderWidth="1px" BorderStyle="Solid" HorizontalAlign="Center"
														BackColor="LightGray">
													</Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" BorderColor="Black" BorderStyle="Solid" ForeColor="Black" BackColor="LightGray"
													Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<GroupByRowStyleDefault ForeColor="Black" BackColor="#95B9F0"></GroupByRowStyleDefault>
												<FrameStyle Width="100%" Cursor="Default" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림"
													BorderColor="DimGray" BorderStyle="Solid" BackColor="Silver" Height="400px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents ColumnHeaderClickHandler="uwgBR_HT_ColumnHeaderClickHandler" BeforeRowTemplateCloseHandler="uwgBR_HT_BeforeRowTemplateCloseHandler"
													BeforeRowTemplateOpenHandler="uwgBR_HT_BeforeRowTemplateOpenHandler"></ClientSideEvents>
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
													<BorderDetails ColorTop="Gray" WidthLeft="0px" WidthTop="0px" ColorLeft="Gray"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand AllowUpdate="RowTemplateOnly">
													<Columns>
														<igtbl:UltraGridColumn HeaderText="선택" Key="chk" Width="30px" Type="CheckBox" HeaderClickAction="Select"
															BaseColumnName="chk" AllowUpdate="Yes">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" BaseColumnName="ItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" BaseColumnName="ItemDrawNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" BaseColumnName="ItemName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="재질" Key="MateralQuality" BaseColumnName="MateralQuality">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="규격" Key="Standard" BaseColumnName="Standard">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="1차납기량" Key="FirstDeliveryDemandQuantity" Format="###,###,##0.00" BaseColumnName="FirstDeliveryDemandQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="1차납기일" Key="FirstDeliveryDemandDate" Format="yyyy-MM-dd" BaseColumnName="FirstDeliveryDemandDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="2차납기량" Key="SecondDeliveryDemandQuantity" Format="###,###,##0.00" BaseColumnName="SecondDeliveryDemandQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="2차납기일" Key="SecondDeliveryDemandDate" Format="yyyy-MM-dd" BaseColumnName="SecondDeliveryDemandDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="3차납기량" Key="ThirdDeliveryDemandQuantity" Format="###,###,##0.00" BaseColumnName="ThirdDeliveryDemandQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="3차납기일" Key="ThirdDeliveryDemandDate" Format="yyyy-MM-dd" BaseColumnName="ThirdDeliveryDemandDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="4차납기량" Key="FourthDeliveryDemandQuantity" Format="###,###,##0.00" BaseColumnName="FourthDeliveryDemandQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="4차납기일" Key="FourthDeliveryDemandDate" Format="yyyy-MM-dd" BaseColumnName="FourthDeliveryDemandDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="5차납기량" Key="FifthDeliveryDemandQuantity" Format="###,###,##0.00" BaseColumnName="FifthDeliveryDemandQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="5차납기일" Key="FifthDeliveryDemandDate" Format="yyyy-MM-dd" BaseColumnName="FifthDeliveryDemandDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총구매량" Key="OrderQuantity" Format="###,###,##0.00" BaseColumnName="OrderQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Format="\ ###,###,##0.00" BaseColumnName="ApplyUnitCost">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="금액" Key="TotalCost" Format="\ ###,###,##0.00" BaseColumnName="TotalCost">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="자재소요원장번호" Key="HistoryIndex" Hidden="True" BaseColumnName="HistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
													</Columns>
													<RowTemplateStyle BorderWidth="1px" BorderColor="Black" BorderStyle="Solid"></RowTemplateStyle>
													<RowEditTemplate>
														<P align="center">
															<TABLE id="Table5" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BORDER-BOTTOM: dimgray 1px solid"
																cellSpacing="0" cellPadding="0" width="800" bgColor="aliceblue" border="0">
																<TR>
																	<TD align="right" width="120" height="30">
																		<asp:Label id="Label3" runat="server" Font-Size="10pt">품목번호 </asp:Label>&nbsp;</TD>
																	<TD height="30">
																		<asp:TextBox id="TextBox1" runat="server" BorderColor="DimGray" BorderStyle="Solid" BackColor="WhiteSmoke"
																			Width="130px" Height="20px" BorderWidth="1px" ColumnKey="ItemNum" ReadOnly="True"></asp:TextBox></TD>
																	<TD align="right" width="120" height="30">
																		<asp:Label id="Label4" runat="server" Font-Size="10pt">도면번호 </asp:Label>&nbsp;</TD>
																	<TD height="30">
																		<asp:TextBox id="TextBox2" runat="server" BorderColor="DimGray" BorderStyle="Solid" BackColor="WhiteSmoke"
																			Width="130px" Height="20px" BorderWidth="1px" ColumnKey="ItemDrawNum" ReadOnly="True"></asp:TextBox></TD>
																	<TD align="right" width="100" height="30">
																		<asp:Label id="Label14" runat="server" Font-Size="10pt">품목명 </asp:Label>&nbsp;</TD>
																	<TD height="30">
																		<asp:TextBox id="TextBox3" runat="server" BorderColor="DimGray" BorderStyle="Solid" BackColor="WhiteSmoke"
																			Width="160px" Height="20px" BorderWidth="1px" ColumnKey="ItemName" ReadOnly="True"></asp:TextBox></TD>
																</TR>
																<TR>
																	<TD align="right" height="30">
																		<asp:Label id="Label15" runat="server" Font-Size="10pt">총구매량 </asp:Label>&nbsp;</TD>
																	<TD height="30"><INPUT id="OrderQuanatity" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 130px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: #f5f5f5; TEXT-ALIGN: right"
																			readOnly type="text"></TD>
																	<TD align="right" height="30">
																		<asp:Label id="Label16" runat="server" Font-Size="10pt">단가 </asp:Label>&nbsp;</TD>
																	<TD height="30"><INPUT id="ApplyUnitCost" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 130px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: #f5f5f5; TEXT-ALIGN: right"
																			readOnly type="text"></TD>
																	<TD align="right" width="100" height="30">
																		<asp:Label id="Label17" runat="server" Font-Size="10pt">금액 </asp:Label>&nbsp;</TD>
																	<TD height="30"><INPUT id="TotalCost" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 130px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: #f5f5f5; TEXT-ALIGN: right"
																			readOnly type="text"></TD>
																</TR>
																<TR>
																	<TD vAlign="top" align="center" colSpan="6">
																		<TABLE id="Table4" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 29px"
																			borderColor="#ffffff" cellSpacing="0" cellPadding="0" width="790" align="center" borderColorLight="#696969"
																			border="1">
																			<TR>
																				<TD style="WIDTH: 65px" vAlign="middle" align="center" width="65" bgColor="#dcdcdc"
																					height="23">&nbsp;</TD>
																				<TD vAlign="middle" align="center" bgColor="#dcdcdc" height="23">
																					<asp:Label id="Label5" runat="server" Font-Size="10pt"> 1차</asp:Label></TD>
																				<TD vAlign="middle" align="center" bgColor="#dcdcdc" height="23">
																					<asp:Label id="Label2" runat="server" Font-Size="10pt"> 2차</asp:Label></TD>
																				<TD vAlign="middle" align="center" width="10%" bgColor="#dcdcdc" height="23">
																					<asp:Label id="Label7" runat="server" Font-Size="10pt"> 3차</asp:Label></TD>
																				<TD vAlign="middle" align="center" bgColor="#dcdcdc" height="23">
																					<asp:Label id="Label8" runat="server" Font-Size="10pt"> 4차</asp:Label></TD>
																				<TD vAlign="middle" align="center" bgColor="#dcdcdc" height="23">
																					<asp:Label id="Label9" runat="server" Font-Size="10pt"> 5차</asp:Label></TD>
																				<TD vAlign="middle" align="center" bgColor="#dcdcdc" height="23">
																					<asp:Label id="Label10" runat="server" Font-Size="10pt"> 총구매량</asp:Label></TD>
																				<TD vAlign="middle" align="center" bgColor="#dcdcdc" height="23">
																					<asp:Label id="Label11" runat="server" Font-Size="10pt"> 금액</asp:Label></TD>
																			</TR>
																			<TR>
																				<TD style="WIDTH: 65px" align="center" height="30">
																					<asp:Label id="Label12" runat="server" Font-Size="10pt">납기량</asp:Label></TD>
																				<TD align="center" height="30"><INPUT onkeypress="if(event.keyCode==13){return false;}" id="txtFirstDeliveryDemandQuantity"
																						onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Cur(this);" onkeyup="Process(); OnKeyUp_Currency(this);" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 100px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; TEXT-ALIGN: right"
																						onfocus="OnFocus_Obj(this);" type="text"></TD>
																				<TD align="center" height="30"><INPUT onkeypress="if(event.keyCode==13){return false;}" id="txtSecondDeliveryDemandQuantity"
																						onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Cur(this);" onkeyup="Process(); OnKeyUp_Currency(this);" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 100px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; TEXT-ALIGN: right"
																						onfocus="OnFocus_Obj(this);" type="text"></TD>
																				<TD align="center" height="30"><INPUT onkeypress="if(event.keyCode==13){return false;}" id="txtThirdDeliveryDemandQuantity"
																						onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Cur(this);" onkeyup="Process(); OnKeyUp_Currency(this);" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 100px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; TEXT-ALIGN: right"
																						onfocus="OnFocus_Obj(this);" type="text"></TD>
																				<TD align="center" height="30"><INPUT onkeypress="if(event.keyCode==13){return false;}" id="txtFourthDeliveryDemandQuantity"
																						onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Cur(this);" onkeyup="Process(); OnKeyUp_Currency(this);" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 100px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; TEXT-ALIGN: right"
																						onfocus="OnFocus_Obj(this);" type="text"></TD>
																				<TD align="center" height="30"><INPUT onkeypress="if(event.keyCode==13){return false;}" id="txtFifthDeliveryDemandQuantity"
																						onkeydown="OnKeyDown_Float(this);" onblur="OnBlur_Cur(this);" onkeyup="Process(); OnKeyUp_Currency(this);" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 100px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; TEXT-ALIGN: right"
																						onfocus="OnFocus_Obj(this);" type="text"></TD>
																				<TD align="center" width="15%" height="30" rowSpan="2"><INPUT id="txtOrderQuantity" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 84px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; TEXT-ALIGN: right"
																						readOnly type="text" size="8"></TD>
																				<TD align="center" width="15%" height="30" rowSpan="2"><INPUT id="txtTotalCost" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 87px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 20px; TEXT-ALIGN: right"
																						readOnly type="text" size="9"></TD>
																			</TR>
																			<TR>
																				<TD align="center" width="10%" height="30">
																					<asp:Label id="Label13" runat="server" Font-Size="10pt">납기일</asp:Label></TD>
																				<TD align="center" width="10%" height="30">
																					<igtxt:WebDateTimeEdit id="wdeFirstDeliveryDemandDate" runat="server" BorderColor="White" BorderStyle="Inset"
																						Width="95px" Height="20px" BorderWidth="2px" HorizontalAlign="Center" UseBrowserDefaults="False" DisplayModeFormat="d">
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
																				<TD align="center" width="10%" height="30">
																					<igtxt:WebDateTimeEdit id="wdeSecondDeliveryDemandDate" runat="server" BorderColor="White" BorderStyle="Inset"
																						Width="95px" Height="20px" BorderWidth="2px" HorizontalAlign="Center" UseBrowserDefaults="False" DisplayModeFormat="d">
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
																				<TD align="center" height="30">
																					<igtxt:WebDateTimeEdit id="wdeThirdDeliveryDemandDate" runat="server" BorderColor="White" BorderStyle="Inset"
																						Width="95px" Height="20px" BorderWidth="2px" HorizontalAlign="Center" UseBrowserDefaults="False" DisplayModeFormat="d">
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
																				<TD align="center" width="10%" height="30">
																					<igtxt:WebDateTimeEdit id="wdeFourthDeliveryDemandDate" runat="server" BorderColor="White" BorderStyle="Inset"
																						Width="95px" Height="20px" BorderWidth="2px" HorizontalAlign="Center" UseBrowserDefaults="False" DisplayModeFormat="d">
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
																				<TD align="center" width="10%" height="30">
																					<igtxt:WebDateTimeEdit id="wdeFifthDeliveryDemandDate" runat="server" BorderColor="White" BorderStyle="Inset"
																						Width="95px" Height="20px" BorderWidth="2px" HorizontalAlign="Center" UseBrowserDefaults="False" DisplayModeFormat="d">
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
																		</TABLE>
																	</TD>
																</TR>
																<TR>
																	<TD style="PADDING-RIGHT: 10px" align="right" colSpan="6" height="35"><INPUT id="igtbl_reOkBtn" style="WIDTH: 65px; HEIGHT: 20px" onclick="igtbl_gRowEditButtonClick(event);__doPostBack('lnkRowUpDate','');"
																			type="button" value="수   정">
																	</TD>
																</TR>
															</TABLE>
														</P>
													</RowEditTemplate>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" colSpan="3" height="35"><INPUT id="chkAll" style="WIDTH: 51px; HEIGHT: 22px" type="hidden" size="3" value="true"
											name="Hidden1" runat="server">
										<asp:button id="btnRequest" runat="server" BorderStyle="Outset" Width="60px" Height="20px" Text="의   뢰"
											Font-Size="9pt"></asp:button>&nbsp;
									</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
