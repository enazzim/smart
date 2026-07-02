<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Page language="c#" Codebehind="SaleHistoryRegistration.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BusinessManagement.SaleHistoryRegistration" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SaleHistoryRegistration</title>
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
		} 
		
		function OK(text)
		{
			if(confirm(text + "하시겠습니까?"))
				return true;
			else
				return false;
		}

		
		// 선택된 그리드의 행 찾기
		function UltraWebGrid1_MouseOverHandler(gridName, id, button)
		{
			//Add code to handle your event here.
			if(button == 0) 
			{ // Are we over a cell
				var cell = igtbl_getElementById(id);
				cell.style.cursor = 'hand';
				var parts = id.split("_");
				document.SaleHistoryRegistration.lb_RowIndex.value = parts[1];
			}
			
		}
		// 찾은 그리드 행의 값을 저장
		
		
		function UltraWebGrid1_DblClickHandler(gridName, cellId)
		{

			//Add code to handle your event here.
			
			
			var row = igtbl_getRowById(cellId);
			
			document.SaleHistoryRegistration.lb_RowSelectIndex.value = document.SaleHistoryRegistration.lb_RowIndex.value;
			document.SaleHistoryRegistration.tb_ItemNum.value = row.getCellFromKey("ItemNum").getValue();//
			//document.SaleHistoryRegistration.tb_ItemDrawNum.value = row.getCellFromKey("ItemDrawNum").getValue();//
			document.SaleHistoryRegistration.tb_ItemName.value = row.getCellFromKey("ItemName").getValue();//
			document.SaleHistoryRegistration.tb_OutStorehouseQuantity.value = row.getCellFromKey("OutStorehouseQuantity").getValue();//
			document.SaleHistoryRegistration.tb_SuitabilityQuantity.value = row.getCellFromKey("OutStorehouseQuantity").getValue();//
			document.SaleHistoryRegistration.tb_ApplyUnitCost.value = row.getCellFromKey("ApplyUnitCost").getValue();//
			document.SaleHistoryRegistration.lb_Com.value = row.getCellFromKey("BusinessRegistrationNum").getValue();//
			document.SaleHistoryRegistration.tb_SupplementaryValueTaxRate.value = row.getCellFromKey("SupplementaryValueTaxRate").getValue();//
			
			
			var row = igtbl_getRowById(cellId);
			var objChooser1 = igdrp_getComboById("wdcSaleDate");//출고일자
			//수주일자
			objChooser1.setValue(row.getCellFromKey("OutStoreDate").getValue());
						
		}
		
		function UltraWebGrid1_AfterSelectChangeHandler(gridName, id)
		{
			//Add code to handle your event here.
			document.SaleHistoryRegistration.lb_RowSelectIndex.value = document.SaleHistoryRegistration.lb_RowIndex.value;
			document.SaleHistoryRegistration.tb_ItemNum.value = document.getElementById(id).children[0].innerText;
			document.SaleHistoryRegistration.tb_ItemDrawNum.value = document.getElementById(id).children[1].innerText;
			document.SaleHistoryRegistration.tb_ItemName.value = document.getElementById(id).children[2].innerText;
			document.SaleHistoryRegistration.tb_OutStorehouseQuantity.value = document.getElementById(id).children[5].innerText;			
			
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
		
		// 함격수량 입력시 onkeyup 이벤트 때 발생.
		function Process()
		{
		
			var frm = document.SaleHistoryRegistration.tb_UnSuitabilityQuantity;//부적합수량
			var value = Number(RemoveComma(document.SaleHistoryRegistration.tb_OutStorehouseQuantity.value)) - Number(RemoveComma(document.SaleHistoryRegistration.tb_SuitabilityQuantity.value));//출고수량-합격수량
			var sqTxt = document.SaleHistoryRegistration.tb_SuitabilityQuantity;//합격수량

			if(value < 0 )
			{
				alert("- 합격수량이 출고된 수량보다 많습니다");
				sqTxt.value = sqTxt.value.substring(0, sqTxt.value.length-1);
				sqTxt.focus();
			}
			else
			{
				var IncongruityQuantity = (Math.round((value * 10000)) / 10000);
				frm.value = IncongruityQuantity;
			}
		}
		

//////////////////////////////////////////////////////////////////////////////////////////
//															Float	        														//
//////////////////////////////////////////////////////////////////////////////////////////
		
		function OnKeyDown_Float(obj) 
		{   
			if (event.ctrlKey || event.shiftKey || event.altKey)   
			{
				SetEventPass();
				return true;
			}
			var sFieldValue = new String(obj.value);
			// 숫자검사 + Numeric Keypad 숫자검사
			if( (event.keyCode<45 || event.keyCode>57) && (event.keyCode<96 || event.keyCode>105))  
			{
    			if( event.keyCode == 110 || event.keyCode == 190 ) 
    			{
    				// "." 가 2개있는지 검사
	    			if( sFieldValue.indexOf(".") != -1 )	
	    			{
	    				SetEventCancel();
						return obj.value;
	    			}
				}
				else if( !IsNumericKey() ) 
				{
	    			SetEventCancel();
					return obj.value;
				}	    
			}
			else
			{    
    			if( sFieldValue.indexOf(".") == -1 )
    			{
	    			// "0" 을 입력했을 경우 "0" 다음에 "." 가 아니면 무시하여야 한다.	
	    			// 48, 96(Numeric Keypad)
					if( sFieldValue.substr(0,1) == "0" && (event.keyCode == 48 || event.keyCode == 96) )
					{
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
		
		function OnBlur_Float(obj) 
		{
			if (ValidateFloat(obj.value) == false) 
			{
				obj.value = "";
    			return false;
			}
		}
		
		function ValidateFloat(a_sValue)	
		{	
			var s_FieldValue = new String(a_sValue);
			
			if (isNaN(s_FieldValue))
				return false;
				
			return true;
		}
		
//////////////////////////////////////////////////////////////////////////////////////////
//															Currency															//
//////////////////////////////////////////////////////////////////////////////////////////
		function OnKeyDown_Currency(obj) 
		{   
			if (event.ctrlKey || event.shiftKey || event.altKey) 
			{
				SetEventPass();
				return true;
			}
			var sFieldValue = new String(obj.value);        
			if( (event.keyCode<45 || event.keyCode>57) && (event.keyCode<96 || event.keyCode>105))	 
			{
    			if( event.keyCode == 110 || event.keyCode == 190 )	// "."(점) 이라면
    			{
    				// "." 가 2개있는지 검사
	    			if( sFieldValue.indexOf(".") != -1 ) 	
	    			{
	    				SetEventCancel();
						return obj.value;
	    			}
				} 
				else if( !IsCurrencyKey() )
				{
	    			SetEventCancel();
					return obj.value;
				}	    
			}	
			else	
			{    
    			if( sFieldValue.indexOf(".") == -1 )	
    			{
	    			// "0" 을 입력했을 경우 "0" 다음에 "." 가 아니면 무시하여야 한다.	
	    			// 48, 96(Numeric Keypad)
					if( sFieldValue.substr(0,1) == "0" && (event.keyCode == 48 || event.keyCode == 96) )	
					{
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
			if ( IsNumericKey() )		
			{
				SetEventPass();
				return true;
			}
			obj.value = MakeCommaSeparate(sFieldValue);
		}
		
		function OnBlur_Cur(obj)	
		{
			if (ValidateCurrency(obj.value) == false) 
			{
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
			
			if(isNaN(a_Value)) 	
			{ 
    			event.returnValue = false;
    			this.value = "";
    			return "";
			}
			
			if(a_Value == 0) return a_Value;
			
			if(a_Value < 0) 
			{ 
				a_Value=a_Value*(-1);
				fl = "-";
			}
			else if(a_Value == 0)	
			{
    			// 처음 입력값이 0부터 시작할때 이것을 제거한다.
    			a_Value = a_Value*1; 
			}
			
			var a_Value = new String(a_Value);
			var temp = "";
			var sRemain = "";
			var co = 3;
			
			nPointPos = a_Value.indexOf(".");

			if( nPointPos == -1 )  
			{
    			num_len = a_Value.length;
			}
			else
			{
    			// "." 가 포함되어 있을 경우에 재계산
    			if( parseInt(a_Value.substr(0,nPointPos)) == 0 )	
    			{
    				a_Value = "0" + a_Value.substr(nPointPos);
    				nPointPos = a_Value.indexOf(".");
    			}
    			num_len = nPointPos;
    			sRemain = a_Value.substr(nPointPos);
			}
			
			while (num_len>0)	
			{
				num_len = num_len - co;
				
				if(num_len<0)	
				{
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
	<body bgColor="#f7f6f6" MS_POSITIONING="GridLayout">
		<form id="SaleHistoryRegistration" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" height="550"
				cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD style="WIDTH: 7px" width="7"></TD>
					<TD style="PADDING-TOP: 10px" vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 800px; BORDER-BOTTOM: #696969 2px solid; HEIGHT: 40px"><LEGEND style="FONT-SIZE: 9pt" align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" style="WIDTH: 800px" cellSpacing="0" cellPadding="0" width="800">
								<tr>
									<TD style="FONT-SIZE: 9pt" width="600" colSpan="8" height="30">
										<table id="table3" border="0" cellpadding="0" cellspacing="0" width="800">
											<tr>
												<td width="200">
													<uc1:CompanySearchControl id="CSC1" runat="server"></uc1:CompanySearchControl></td>
												<td width="600"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></td>
											</tr>
										</table>
									</TD>
								</tr>
								<tr>
									<td align="right" width="70" height="30">출고일자&nbsp;</td>
									<td width="100" height="30"><igsch:webdatechooser id="wdcFromDate" runat="server" BackColor="#EEEEE9" BorderStyle="Solid" Height="20px"
											BorderColor="DimGray" Width="100px" Font-Size="10pt" Text=" " NullDateLabel=" ">
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
									<td align="center" width="15" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림">~</FONT></td>
									<td width="100" height="30"><igsch:webdatechooser id="wdcToDate" runat="server" BackColor="#EEEEE9" BorderStyle="Solid" Height="20px"
											BorderColor="DimGray" Width="100px" Font-Size="10pt" Text=" " NullDateLabel=" ">
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
									<td align="right" width="515" height="30"><FONT face="굴림"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
												type="button" value="초기화" name="btnReset">&nbsp;</FONT>
										<asp:button id="bt_Search" runat="server" Height="20px" Width="60px" Font-Size="10pt" Text="검  색"></asp:button><FONT face="굴림">&nbsp;
										</FONT>
									</td>
								</tr>
							</TABLE>
						</FIELDSET>
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND style="FONT-SIZE: 9pt" align="top">[검색결과]</LEGEND><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="166px" Width="800px">
								<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
									RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
									HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
									RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect"
									NoDataMessage="">
									<AddNewBox>
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										</Style>
									</AddNewBox>
									<Pager PageSize="5" StyleMode="ComboBox" AllowPaging="True">
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										</Style>
									</Pager>
									<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</HeaderStyleDefault>
									<RowSelectorStyleDefault Cursor="Hand" BackColor="#C0FFFF"></RowSelectorStyleDefault>
									<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="Verdana" BorderColor="DimGray"
										BorderStyle="Solid" BackColor="Silver" Height="166px"></FrameStyle>
									<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</FooterStyleDefault>
									<ClientSideEvents DblClickHandler="UltraWebGrid1_DblClickHandler" MouseOverHandler="UltraWebGrid1_MouseOverHandler"></ClientSideEvents>
									<RowExpAreaStyleDefault Cursor="Hand"></RowExpAreaStyleDefault>
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
											<igtbl:UltraGridColumn HeaderText="발주번호" Key="OrderNum" BaseColumnName="OrderNum"></igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" HeaderClickAction="Select" BaseColumnName="ItemNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="ItemDrawNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" HeaderClickAction="SortMulti" BaseColumnName="CompanyName">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="BusinessRegistrationNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="출고수량" Key="OutStorehouseQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
												BaseColumnName="OutStorehouseQuantity">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
													<Padding Right="4px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="출고일" Key="OutStoreDate" Format="yyyy-MM-dd" BaseColumnName="OutStoreDate">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="미검수량" Key="UnInspectionQuantity" Hidden="True" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="UnInspectionQuantity">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
													<Padding Right="4px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="적합수량" Key="SuitabilityQuantity" Hidden="True" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="SuitabilityQuantity">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
													<Padding Right="4px"></Padding>
												</CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="부적합수량" Key="UnSuitabilityQuantity" Hidden="True" Format="###,###,###.##"
												HeaderClickAction="SortMulti" BaseColumnName="UnSuitabilityQuantity">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
													<Padding Right="4px"></Padding>
												</CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="부적합원인코드" Key="UnSuitabilityCauseCode" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="UnSuitabilityCauseCode">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="부적합원인" Key="UnSuitabilityCauseMeaning" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="UnSuitabilityCauseMeaning">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="부적합내용코드" Key="UnSuitabilityStatusCode" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="UnSuitabilityStatusCode">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="부적합내용" Key="UnSuitabilityStatusMeaning" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="UnSuitabilityStatusMeaning">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="부적합세부내용" Key="UnSuitabilityDetailMeaning" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="UnSuitabilityDetailMeaning">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="부적합금액" Key="UnSuitabilityCost" Hidden="True" Format="\ ###,###,##0"
												HeaderClickAction="SortMulti" BaseColumnName="UnSuitabilityCost">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Right"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="적용단가" Key="ApplyUnitCost" Format="###,###,###.##" HeaderClickAction="SortMulti"
												BaseColumnName="ApplyUnitCost">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="검사판정코드" Key="InspectionDecisionCode" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="InspectionDecisionCode">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="검사판정" Key="InspectionDecisionMeaning" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="InspectionDecisionMeaning">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="출고창고명" Key="BusinessStorehouseNum" HeaderClickAction="SortMulti" BaseColumnName="BusinessStorehouseNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="거래명세서번호" Key="" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="ProgressCondition">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="RegistrationPerson">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="RegistrationPersonID">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="RegistrationDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="UpdatingPerson">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정자ID" Key="" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="UpdatingDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수주원장번호" Key="ReceivingOrderHistoryIndex" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="ReceivingOrderHistoryIndex">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="출고원장번호" Key="OutStorehouseHistoryIndex" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="OutStorehouseHistoryIndex">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="부가세율" Key="SupplementaryValueTaxRate" Hidden="True" Format="###,###,###"
												BaseColumnName="SupplementaryValueTaxRate">
												<CellStyle HorizontalAlign="Right">
													<Padding Right="4px"></Padding>
												</CellStyle>
											</igtbl:UltraGridColumn>
										</Columns>
									</igtbl:UltraGridBand>
								</Bands>
							</igtbl:ultrawebgrid></FIELDSET>
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND style="FONT-SIZE: 9pt" align="top">[입력]</LEGEND>
							<table id="Table4" style="HEIGHT: 149px" cellSpacing="0" cellPadding="0" width="800">
								<tr>
									<td style="WIDTH: 101px" align="right" width="101" height="25"><FONT style="FONT-SIZE: 9pt" face="굴림">품목번호&nbsp;</FONT></td>
									<td style="WIDTH: 94px" width="94" height="25"><asp:textbox id="tb_ItemNum" runat="server" BackColor="#EEEEE9" BorderStyle="Inset" Height="20px"
											Width="100px" Font-Size="9pt"></asp:textbox></td>
									<td style="WIDTH: 92px" align="right" width="92" height="25"><FONT face="굴림"><FONT style="FONT-SIZE: 9pt" face="굴림">품목명</FONT></FONT></td>
									<td style="WIDTH: 104px" width="104" height="25"><asp:textbox id="tb_ItemName" runat="server" BackColor="#EEEEE9" BorderStyle="Inset" Height="20px"
											Width="100px" Font-Size="9pt"></asp:textbox></td>
									<td style="WIDTH: 98px" align="right" width="98" height="25"><FONT face="굴림"><FONT style="FONT-SIZE: 9pt" face="굴림">출고수량</FONT></FONT></td>
									<td style="WIDTH: 42px" width="42" height="25"><asp:textbox id="tb_OutStorehouseQuantity" style="TEXT-ALIGN: right" runat="server" BackColor="#EEEEE9"
											BorderStyle="Inset" Height="20px" Width="100px" Font-Size="9pt" ReadOnly="True">0</asp:textbox></td>
									<td style="WIDTH: 88px" align="right" width="88" height="25"><FONT style="FONT-SIZE: 9pt" face="굴림"><FONT style="FONT-SIZE: 9pt" face="굴림">적합수량</FONT></FONT></td>
									<td width="110" height="25"><asp:textbox id="tb_SuitabilityQuantity" style="TEXT-ALIGN: right" runat="server" BackColor="#EEEEE9"
											BorderStyle="Inset" Height="20px" Width="100px" Font-Size="9pt">0</asp:textbox></td>
									<td style="WIDTH: 77px" align="right" width="77" height="25"><FONT style="FONT-SIZE: 9pt" face="굴림"><FONT face="굴림">적용단가</FONT></FONT></td>
									<td width="100" height="25">
										<asp:textbox id="tb_ApplyUnitCost" style="TEXT-ALIGN: right" runat="server" Font-Size="9pt" Width="100px"
											Height="20px" BackColor="#EEEEE9">0</asp:textbox></td>
								</tr>
								<tr>
									<td style="WIDTH: 101px" align="right" width="101" height="25"><FONT style="FONT-SIZE: 9pt" face="굴림">부적합수량</FONT></td>
									<td style="WIDTH: 94px" width="94" height="25"><asp:textbox id="tb_UnSuitabilityQuantity" style="TEXT-ALIGN: right" runat="server" BackColor="#EEEEE9"
											BorderStyle="Inset" Height="20px" Width="100px" Font-Size="9pt" ReadOnly="True">0</asp:textbox></td>
									<td style="WIDTH: 92px" align="right" width="92" height="25"><FONT style="FONT-SIZE: 8pt" face="굴림">부적합현상</FONT></td>
									<td style="WIDTH: 104px" width="104" height="25"><asp:dropdownlist id="dl_UnSuitabilityStatus" runat="server" BackColor="#EEEEE9" Height="20px" Width="100px"
											Font-Size="9pt"></asp:dropdownlist></td>
									<td style="WIDTH: 100px" align="right" width="98" height="25"><FONT style="FONT-SIZE: 9pt" face="굴림">부적합원인</FONT></td>
									<td style="WIDTH: 42px" width="42" height="25"><asp:dropdownlist id="dl_UnSuitabilityCause" runat="server" BackColor="#EEEEE9" Height="20px" Width="100px"
											Font-Size="9pt"></asp:dropdownlist></td>
									<td style="WIDTH: 88px" vAlign="middle" align="right" width="88" colSpan="1" height="25"
										rowSpan="2">
										<P><FONT style="FONT-SIZE: 9pt" face="굴림">부적합<br>
												세부사유</FONT></P>
									</td>
									<td width="100" height="25" rowSpan="2"><asp:textbox id="tb_UnSuitabilityDetailMeaning" runat="server" BackColor="#EEEEE9" BorderStyle="Inset"
											Height="40px" Width="100px" Font-Size="9pt" TextMode="MultiLine"></asp:textbox></td>
									<td style="WIDTH: 70px" align="right" width="77" height="25"><FONT style="FONT-SIZE: 9pt" face="굴림">부가세율</FONT></td>
									<td width="100" height="25"><asp:textbox id="tb_SupplementaryValueTaxRate" style="TEXT-ALIGN: right" runat="server" BackColor="#EEEEE9"
											BorderStyle="Inset" Height="20px" Width="85px" Font-Size="9pt">10</asp:textbox><asp:label id="Label16" runat="server" Font-Size="10pt">%</asp:label></td>
								</tr>
								<tr>
									<td style="WIDTH: 101px" align="right" width="101" height="25"><FONT style="FONT-SIZE: 9pt" face="굴림">부적합금액</FONT></td>
									<td style="WIDTH: 94px" width="94" height="25"><asp:textbox id="tb_UnSuitabilityCost" style="TEXT-ALIGN: right" runat="server" BackColor="#EEEEE9"
											BorderStyle="Inset" Height="20px" Width="100px" Font-Size="9pt">0</asp:textbox></td>
									<td style="WIDTH: 92px" align="right" width="92" height="25"><FONT style="FONT-SIZE: 9pt" face="굴림">검사판정</FONT></td>
									<td style="WIDTH: 104px" width="104" height="25"><asp:dropdownlist id="dl_InspectionDecision" runat="server" BackColor="#EEEEE9" Height="20px" Width="100px"
											Font-Size="9pt"></asp:dropdownlist></td>
									<td style="WIDTH: 98px" align="right" width="98" height="25"><FONT face="굴림">매출일</FONT></td>
									<td style="WIDTH: 42px" width="42" height="25"><FONT face="굴림">
											<igsch:webdatechooser id="wdcSaleDate" runat="server" Width="100px" Height="20px" BackColor="#EEEEE9"
												NullDateLabel=" " Text="Null">
												<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
													ShowTitle="False" ShowFooter="False">
													<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
													<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
													<DropDownStyle BackColor="White"></DropDownStyle>
													<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
												</CalendarLayout>
												<DropDownStyle BorderStyle="Solid" BackColor="#EEEEE9"></DropDownStyle>
												<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
												<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
											</igsch:webdatechooser></FONT></td>
									<TD align="right" height="25"><FONT face="굴림"></FONT></TD>
									<td align="right" height="25"><FONT face="굴림">
											<asp:button id="bt_Register" runat="server" Font-Size="10pt" Width="60px" Height="20px" Text="매출등록"></asp:button></FONT></td>
								</tr>
								<TR>
									<TD align="right" colSpan="10" height="30"><igtbl:ultrawebgrid id="UltraWebGrid2" runat="server" Height="140px" Width="800px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="UltraWebGrid2" TableLayout="Fixed" CellClickActionDefault="RowSelect">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</AddNewBox>
												<Pager Alignment="Center">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
												<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="Verdana" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="140px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand AllowUpdate="RowTemplateOnly">
													<Columns>
														<igtbl:UltraGridColumn HeaderText="발주번호" Key="OrderNum" BaseColumnName="OrderNum"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" HeaderClickAction="Select" BaseColumnName="ItemNum">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<HeaderStyle Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ItemDrawNum">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" HeaderClickAction="SortMulti" BaseColumnName="CompanyName">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="BusinessRegistrationNum">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="출고수량" Key="OutStorehouseQuantity" Format="###,###,###" HeaderClickAction="SortMulti"
															BaseColumnName="OutStorehouseQuantity">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="적합수량" Key="SuitabilityQuantity" Format="###,###,###" HeaderClickAction="SortMulti"
															BaseColumnName="SuitabilityQuantity">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
															BaseColumnName="ApplyUnitCost">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총금액" Key="TotalCost" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
															BaseColumnName="TotalCost">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="부가세" Key="SupplementaryValueTaxRate" Hidden="True" Format="#########"
															HeaderClickAction="SortMulti" BaseColumnName="SupplementaryValueTaxRate">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="세금계산서번호" Key="BillNum" Hidden="True" Format="" HeaderClickAction="SortMulti"
															BaseColumnName="BillNum">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="계산서발행일자" Key="SupplementaryValueTaxFloatationDate" Hidden="True" Format="yyyy-MM-dd"
															HeaderClickAction="SortMulti" BaseColumnName="SupplementaryValueTaxFloatationDate">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPerson">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationPersonID">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationDate">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" HeaderClickAction="SortMulti" BaseColumnName="UpdatingPerson">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingPersonID">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingDate">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="출고원장번호" Key="OutStorehouseHistoryIndex" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="OutStorehouseHistoryIndex">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="발주원장번호" Key="ReceivingOrderHistoryIndex" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ReceivingOrderHistoryIndex">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="번호" Key="SaleHistoryIndex" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="SaleHistoryIndex">
															<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
								<tr>
									<td align="right" colSpan="10" height="30"><INPUT id="lb_RowIndex" style="WIDTH: 16px; HEIGHT: 20px" type="hidden" size="1" runat="server"
											NAME="lb_RowIndex"><INPUT id="lb_RowSelectIndex" style="WIDTH: 16px; HEIGHT: 20px" type="hidden" size="1"
											name="Hidden1" runat="server"><INPUT id="lb_Com" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1" runat="server"><INPUT id="lb_Num" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden2" runat="server">
										<asp:button id="Button5" runat="server" Height="20px" Width="60px" Text="발행"></asp:button>&nbsp;</td>
								</tr>
							</table>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
