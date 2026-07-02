<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Page language="c#" Codebehind="AddItemReceive.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BusinessManagement.AddItemReceive" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddItemReceive</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!--
		/*****************************************************************************************************************************************************************/
		/*그리드를 선택했을때 입력란 부분으로 그리드의 내용을 보여주는 함수                                                                                              */
		/*****************************************************************************************************************************************************************/
		function UltraWebGrid1_DblClickHandler(gridName, cellId){
		
			
			
			var objChooser1 = igdrp_getComboById("wcDeliveryDate");//1차 납기요구일 WebChooser
			var objChooser = igdrp_getComboById("wcReceiveDate");//수주일 WebChooser
			var objGrid = igtbl_getActiveRow(gridName)//현재 선택되어진 객체의 행의 가져옴
			
			var row = igtbl_getRowById(cellId);
						
			document.Form1.ItemSearchControl1_txtItemNum.value = row.getCellFromKey("ItemNum").getValue();//그리드에서 선택한 품목번호를 유저컨트롤내의 품목번호 컨트롤에 넣음
			document.Form1.ItemSearchControl1_txtItemDrawNum.value = row.getCellFromKey("ItemDrawNum").getValue();//그리드에서 선택한 도면번호를 유저컨트롤내의 품목번호 컨트롤에 넣음
			document.Form1.ItemSearchControl1_txtItemName.value = row.getCellFromKey("ItemName").getValue();//그리드에서 선택한 품목명를 유저컨트롤내의 품목번호 컨트롤에 넣음
			
			document.Form1.CSC1_txtCompanyName.value = row.getCellFromKey("CompanyName").getValue();//그리드에서 선택한 품목명를 유저컨트롤내의 품목번호 컨트롤에 넣음
			document.Form1.CSC1_txtBusinessRegistrationNum.value = row.getCellFromKey("BusinessRegistrationNum").getValue();//그리드에서 선택한 품목명를 유저컨트롤내의 품목번호 컨트롤에 넣음
			document.Form1.hdBusinessRegistrationNum.value = row.getCellFromKey("BusinessRegistrationNum").getValue();//그리드에서 선택한 품목번호를 Hidden 필드에 넣음
			document.Form1.hdItemNum.value = row.getCellFromKey("ItemNum").getValue();//그리드에서 선택한 품목명을 넣음
			
					
			//납기일
			document.Form1.txtQuantity.value = row.getCellFromKey("TotalReceiveingOrderQuantity").getValue();//그리드에서 선택한 1차납품요구량을 넣음
			objChooser1.setValue(objGrid.getCellFromKey("DeliveryRequestDate1").getValue());//그리드에서 선택한 1차납품요구일을 넣음
			objChooser.setValue(objGrid.getCellFromKey("ReceivingOrderDate").getValue());//그리드에서 선택한 수주일을 넣음
			
					
			document.Form1.txtCost.value = row.getCellFromKey("ApplyUnitCost").getValue();//그리드에서 선택한 단가를 넣음
			document.Form1.hdindex.value = row.getCellFromKey("ReceivingOrderHistoryIndex").getValue();//원장 인덱스 번호
			document.Form1.txtTotalCost.value = row.getCellFromKey("TotalCost").getValue();//그리드에서 선택한 단가를 넣음
			document.Form1.txtPropertyClassification.value = row.getCellFromKey("PropertyClassification").getValue();//자산분류
			if(row.getCellFromKey("OrderNum").getValue() == null)
				document.Form1.txtOrderNum.value = ""
			else
				document.Form1.txtOrderNum.value = row.getCellFromKey("OrderNum").getValue();//발주번호
			
			
			document.Form1.btnUpdate.disabled = false;
		    document.Form1.btnDelete.disabled = false;
		    document.Form1.btnReg.disabled = true;
					
					
		}
		function Reset_TextBox()
		{
			ResetBox();
			ResetTextBox();
			document.Form1.txtCost.value = "0";
			document.Form1.txtQuantity.value = "0";
			document.Form1.txtTotalCost.value = "0";
			document.Form1.hdBusinessRegistrationNum.value="";
			document.Form1.hdHistoryIndex.value="";
			document.Form1.hdItemNum.value = "";
			document.Form1.rowIndex.value="";
			document.Form1.lblItemDrawNum.value="";
			document.Form1.txtPropertyClassification.value = "";
			document.Form1.txtOrderNum.value = "";
			
			
			
			var objDChooser = igdrp_getComboById("wcDeliveryDate");
			objDChooser.setValue(null);
			var objDChooser1 = igdrp_getComboById("wcReceiveDate");
			objDChooser1.setValue(null);
						
			document.Form1.btnUpdate.disabled = true;
			document.Form1.btnDelete.disabled = true;
		}
			
		function OnKeyDown_Float(obj) 
		{   
			if (event.ctrlKey || event.shiftKey || event.altKey)   
			{
				SetEventPass();
				return true;
			}
									
			// 합격수량 TextBox 객체의 value로 배열을 만든다.
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
		}	// OnKeyDown_Float() 끝
		
		function ValidateFloat(a_sValue)	
		{	
			var s_FieldValue = new String(a_sValue);
			
			if (isNaN(s_FieldValue))
				return false;
				
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
				event.keyCode == 110 ||			// "."
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
		
		// 콤마제거 함수
		function RemoveComma(Ovalue)
		{
			var array = new String(Ovalue).split(",");
			var Rvalue = new String();
			for ( var i = 0 ; i < array.length ; i++)	
			{
				Rvalue += array[i];
			}
			return Rvalue;
		}
		
		
		
		
		function Process()
		{
			document.Form1.txtTotalCost.value 
				= MakeCommaSeparate((Number(RemoveComma(document.Form1.txtCost.value)) 
				* Number(RemoveComma(document.Form1.txtQuantity.value))));
		}
		
		
		
		// Javascript Trim() 메서드 
		String.prototype.trim = function()
		{
			return this.replace(/(^\s*)|(\s*$)/g,"");
		}
					
		
		function UltraWebGrid1_AfterSelectChangeHandler(gridName, rowId){
			
			///////////RowIndex Hidden 에 저장////////////////
			var num = rowId.split('_');
			document.Form1.rowIndex.value = num[1];
			/////////////////////////////////////////////////
		}
		
		function DoPost()
		{
			__doPostBack('LinkButton1','');
		}
					
		
//-->
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="LEFT: 10px; POSITION: absolute; TOP: 10px" height="550" cellSpacing="0"
				cellPadding="0" width="800" border="0">
				<TR>
					<TD width="20" height="100"></TD>
					<TD>
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid"
							align="middle"><LEGEND style="FONT-SIZE: 9pt" align="left">[&nbsp;등 록 ]
							</LEGEND>
							<table id="table0" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
								<tr>
									<td colSpan="8">
										<table id="table4" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
											<tr>
												<td width="200"><uc1:companysearchcontrol id="CSC1" runat="server"></uc1:companysearchcontrol></td>
												<td width="600"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></td>
											</tr>
										</table>
									</td>
								</tr>
								<TR>
									<TD colSpan="8" vAlign="bottom" align="center">
										<table id="table5" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
											<tr>
												<td width="60" align="right">단가&nbsp;</td>
												<td width="100"><asp:textbox id="txtCost" style="TEXT-ALIGN: right" runat="server" Width="96px" BorderStyle="Solid"
														BorderWidth="1px" BackColor="#EEEEE9" Height="20px" BorderColor="DimGray">0</asp:textbox></td>
												<td width="60" align="right">수량&nbsp;</td>
												<td width="100"><asp:textbox id="txtQuantity" style="TEXT-ALIGN: right" runat="server" Width="96px" BorderStyle="Solid"
														BorderWidth="1px" BackColor="#EEEEE9" Height="20px" BorderColor="DimGray">0</asp:textbox></td>
												<td width="60" align="right">금액&nbsp;</td>
												<td width="100"><asp:textbox id="txtTotalCost" style="TEXT-ALIGN: right" runat="server" Width="96px" BorderStyle="Solid"
														BorderWidth="1px" BackColor="#EEEEE9" Height="20px" BorderColor="DimGray" ReadOnly="True">0</asp:textbox></td>
												<td width="60" align="right">납기일&nbsp;</td>
												<td width="100"><igsch:webdatechooser id="wcDeliveryDate" runat="server" Width="100px" BorderStyle="Solid" BackColor="#EEEEE9"
														Height="20px" BorderColor="DimGray" NullDateLabel=" " Text=" ">
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
												<td width="60" align="right">수주일&nbsp;</td>
												<td width="100">
													<igsch:webdatechooser id="wcReceiveDate" runat="server" BorderColor="DimGray" Height="20px" BackColor="#EEEEE9"
														BorderStyle="Solid" Width="100px" Text=" " NullDateLabel=" ">
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
											</tr>
											<TR>
												<TD align="right" width="60">발주번호&nbsp;
												</TD>
												<TD width="100">
													<asp:textbox id="txtOrderNum" runat="server" BorderColor="DimGray" Height="20px" BackColor="#EEEEE9"
														BorderWidth="1px" BorderStyle="Solid" Width="96px"></asp:textbox></TD>
												<TD align="right" width="60">자산분류&nbsp;</TD>
												<TD width="100">
													<asp:textbox id="txtPropertyClassification" runat="server" BorderColor="DimGray" Height="20px"
														BackColor="#EEEEE9" BorderWidth="1px" BorderStyle="Solid" Width="100px" ReadOnly="True" Font-Size="9pt"></asp:textbox></TD>
												<TD align="right" width="60"></TD>
												<TD width="100"></TD>
												<TD align="right" width="60"></TD>
												<TD width="100"></TD>
												<TD align="right" width="60"></TD>
												<TD width="100"></TD>
											</TR>
										</table>
									</TD>
								</TR>
								<tr>
									<td align="right" width="800" colSpan="8" height="30"><INPUT id="hdindex" style="WIDTH: 36px; HEIGHT: 21px; BACKGROUND-COLOR: #eeeee9" type="hidden"
											size="1" name="hdindex" runat="server" value="0"><INPUT id="rowIndex" style="WIDTH: 36px; HEIGHT: 21px; BACKGROUND-COLOR: #eeeee9" type="hidden"
											size="1" name="Hidden4" runat="server"><INPUT id="hdItemNum" style="WIDTH: 36px; HEIGHT: 21px; BACKGROUND-COLOR: #eeeee9" type="hidden"
											size="1" name="Hidden1" runat="server"><INPUT id="lblItemDrawNum" style="WIDTH: 36px; HEIGHT: 21px; BACKGROUND-COLOR: #eeeee9"
											type="hidden" size="2" name="Hidden4" runat="server"><INPUT id="hdBusinessRegistrationNum" style="WIDTH: 36px; HEIGHT: 21px; BACKGROUND-COLOR: #eeeee9"
											type="hidden" size="1" name="Hidden4" runat="server"><asp:linkbutton id="LinkButton1" runat="server"></asp:linkbutton><INPUT id="btnInit" style="WIDTH: 65px; HEIGHT: 20px" onclick="Reset_TextBox();" type="button"
											size="20" value="초기화" name="btnInit">&nbsp;
										<asp:button id="btnUpdate" runat="server" Width="65px" Height="20px" Text="수정" Enabled="False"></asp:button>&nbsp;
										<asp:button id="btnDelete" runat="server" Width="65px" Height="20px" Text="삭제" Enabled="False"></asp:button>&nbsp;
										<asp:button id="btnReg" runat="server" Width="65px" Height="20px" Text="등 록" Font-Size="9pt"></asp:button></td>
								</tr>
							</table>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="20" height="10"></TD>
					<TD vAlign="top" align="left" height="10"></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 400px" width="20"></TD>
					<TD vAlign="top" align="left">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-WEIGHT: normal; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 100%"
							align="middle"><LEGEND style="FONT-SIZE: 10pt" align="left">[&nbsp;입력결과 ]
							</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" align="center">
								<TR height="35">
									<TD vAlign="top" align="center" rowSpan="1">
										<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="366px" Width="800px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
												BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1"
												TableLayout="Fixed" CellClickActionDefault="RowSelect">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</AddNewBox>
												<Pager>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray" Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
												<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
													BackColor="Silver" Height="366px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents AfterSelectChangeHandler="UltraWebGrid1_AfterSelectChangeHandler" DblClickHandler="UltraWebGrid1_DblClickHandler"></ClientSideEvents>
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
														<igtbl:UltraGridColumn HeaderText="품목상태" Key="ItemState" Hidden="True" BaseColumnName="ItemState"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" HeaderClickAction="Select" BaseColumnName="ItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
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
														<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" HeaderClickAction="SortMulti" BaseColumnName="CompanyName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="BusinessRegistrationNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="자산분류" Key="PropertyClassification" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="PropertyClassification">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="생산의뢰여부" Key="ProductionRequestDivision" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ProductionRequestDivision">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총수주량" Key="TotalReceiveingOrderQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
															BaseColumnName="TotalReceiveingOrderQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="납기요구일" Key="DeliveryRequestDate1" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="DeliveryRequestDate1">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
															BaseColumnName="ApplyUnitCost">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총금액" Key="TotalCost" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
															BaseColumnName="TotalCost">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수주일자" Key="ReceivingOrderDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="ReceivingOrderDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="출고수량" Key="OutStorehouseQuantity" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="OutStorehouseQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="적합수량" Key="SuitabilityQuantity" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="SuitabilityQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="미검수량" Key="UnInspectionQuantity" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="UnInspectionQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="납품잔량" Key="RemainderQuantity" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="RemainderQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="발주번호" Key="OrderNum" HeaderClickAction="SortMulti" BaseColumnName="OrderNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="납품장소" Key="DeliveryPlace" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="DeliveryPlace">
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
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationPersonID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
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
														<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingPersonID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="번호" Key="ReceivingOrderHistoryIndex" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ReceivingOrderHistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단위" Key="Unit" Hidden="True" BaseColumnName="Unit"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="규격" Key="Standard" Hidden="True" BaseColumnName="Standard"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="담당자" Key="CompanyPersonInCharge" Hidden="True" BaseColumnName="CompanyPersonInCharge"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="연락처" Key="TelephoneNum" Hidden="True" BaseColumnName="TelephoneNum"></igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hdHistoryIndex" style="WIDTH: 40px; HEIGHT: 21px" type="hidden" size="1" name="Hidden1"
				runat="server"></form>
	</body>
</HTML>
