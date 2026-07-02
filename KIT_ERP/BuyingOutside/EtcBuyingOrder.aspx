<%@ Page language="c#" Codebehind="EtcBuyingOrder.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BuyingOutside.EtcBuyingOrder" codePage="949" trace="False" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>EtcBuyingOrder</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!--	
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
		
		
		function CheckForm(actionState)
		{		
			if ( MonthCloseing == "0" )
			{
				var objDChooser = igdrp_getComboById("wcDeliveryDate");
				var deliveryDate = objDChooser.getValue();
				var objCombo = igcmbo_getComboById("wcCompanyName");
				var companyName = objCombo.getDisplayValue();
				
				if (
					document.Form1.txtItemName.value.trim() == "" 
					|| document.Form1.txtCost.value.trim() == "" 
					|| document.Form1.txtCost.value.trim() == "0" 
					|| document.Form1.txtQuantity.value.trim() == "" 
					|| document.Form1.txtQuantity.value.trim() == "0" 
					|| deliveryDate == null 
					|| deliveryDate == "" 
					|| companyName == ""
				)
				{
					alert("필수 항목들을 정확하게 입력해 주세요.");
					return false;
				}
				else
				{
					if ( confirm( actionState + " 하시겠습니까?") ) 
						return true;
					else 
						return false;
				}
			}
			else if ( MonthCloseing == "1" )
			{
				alert('월마감이 되어 등록할 수 없습니다.');
				return false;
			}
		}
		
		function Process()
		{
			document.Form1.txtTotalCost.value 
				= MakeCommaSeparate((Number(RemoveComma(document.Form1.txtCost.value)) 
				* Number(RemoveComma(document.Form1.txtQuantity.value))));
		}
		
		function Reset_TextBox()
		{
			document.Form1.txtItemName.value = "";
			document.Form1.txtCost.value = "0";
			document.Form1.txtQuantity.value = "0";
			document.Form1.txtTotalCost.value = "0";
			
			var objDChooser = igdrp_getComboById("wcDeliveryDate");
			objDChooser.setValue(null);
			ResetBox();
			document.Form1.btnUpdate.disabled = true;
			document.Form1.btnDelete.disabled = true;
			document.Form1.ddlItemClassification1.options[0].selected=true;
		}
		
		// Javascript Trim() 메서드 
		String.prototype.trim = function()
		{
			return this.replace(/(^\s*)|(\s*$)/g,"");
		}
					
		function UltraWebGrid1_CellClickHandler(gridName, cellId, button)
		{
			var objDChooser = igdrp_getComboById("wcDeliveryDate");
			var row = igtbl_getRowById(cellId);
			var frm = document.Form1;

			frm.txtItemName.value =  row.getCellFromKey("ItemNum").getValue() ;
			frm.txtCost.value =  MakeCommaSeparate( row.getCellFromKey("Cost").getValue() );
			frm.txtQuantity.value =  MakeCommaSeparate( row.getCellFromKey("Quantity").getValue() );
			frm.txtTotalCost.value =  MakeCommaSeparate( row.getCellFromKey("TotalCost").getValue() );
			
			// 클라이언트에서 WebDateChooser에 값을 넣기 위해서는 반드시 JavaScript Date 객체를 만들어서 넣어줘야 한다.
			var dDate = new Date( row.getCellFromKey("DeliveryDate").getValue() );
			objDChooser.setValue(dDate);
			
			frm.CSC1_txtCompanyName.value = row.getCellFromKey("CompanyName").getValue();
			frm.CSC1_txtBusinessRegistrationNum.value = row.getCellFromKey("BusinessRegistrationNum").getValue();

			frm.btnUpdate.disabled = false;
			frm.btnDelete.disabled = false;
			frm.hdHistoryIndex.value = row.getCellFromKey("IndexNum").getValue();
			
			
			for(var i = 0; i < frm.ddlItemClassification1.options.length; i++)
		    {
				if(frm.ddlItemClassification1.options[i].innerText == row.getCellFromKey("ReasonName").getValue())
				{
					frm.ddlItemClassification1.options[i].selected = true;
					break;
				}
		    }
		}
//-->
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" height="550" cellSpacing="0" cellPadding="0" width="800" border="0"
				style="LEFT: 10px; POSITION: absolute; TOP: 10px">
				<TR>
					<TD width="20" height="100"></TD>
					<TD>
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid"
							align="middle"><LEGEND style="FONT-SIZE: 9pt" align="left">[&nbsp;등 록 ]
							</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
								<TR>
									<TD align="right" width="70" height="35">품목명&nbsp;
									</TD>
									<TD width="130" height="35"><asp:textbox id="txtItemName" runat="server" BorderColor="DimGray" Height="20px" BackColor="#EEEEE9"
											BorderWidth="1px" BorderStyle="Solid" Width="120px"></asp:textbox></TD>
									<TD align="right" width="70" height="35">개별 단가&nbsp;
									</TD>
									<TD align="left" width="100" height="35"><asp:textbox id="txtCost" style="TEXT-ALIGN: right" runat="server" BorderColor="DimGray" Height="20px"
											BackColor="#EEEEE9" BorderWidth="1px" BorderStyle="Solid" Width="96px">0</asp:textbox></TD>
									<TD align="right" width="40" height="35">수량&nbsp;</TD>
									<TD align="left" width="100" height="35"><asp:textbox id="txtQuantity" style="TEXT-ALIGN: right" runat="server" BorderColor="DimGray"
											Height="20px" BackColor="#EEEEE9" BorderWidth="1px" BorderStyle="Solid" Width="96px">0</asp:textbox></TD>
									<TD align="right" width="60" height="35">총금액&nbsp;
									</TD>
									<TD align="left" height="35"><asp:textbox id="txtTotalCost" style="TEXT-ALIGN: right" runat="server" BorderColor="DimGray"
											Height="20px" BackColor="#EEEEE9" BorderWidth="1px" BorderStyle="Solid" Width="96px" ReadOnly="True">0</asp:textbox></TD>
								</TR>
								<TR>
									<TD align="left" width="200" height="35" colSpan="2">
										<uc1:CompanySearchControl id="CSC1" runat="server"></uc1:CompanySearchControl></TD>
									<TD align="right" width="70" height="35">납기요구일&nbsp;</TD>
									<TD align="left" width="100" height="35"><igsch:webdatechooser id="wcDeliveryDate" runat="server" BorderColor="DimGray" Height="20px" BackColor="#EEEEE9"
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
										</igsch:webdatechooser></TD>
									<td align="right" width="40">구분&nbsp;</td>
									<td>
										<asp:DropDownList id="ddlItemClassification1" runat="server" BackColor="#EEEEE9"></asp:DropDownList></td>
									<TD align="right" colSpan="2" height="35"><INPUT id="btnInit" style="WIDTH: 65px; HEIGHT: 20px" onclick="Reset_TextBox();" type="button"
											size="20" value="초기화">&nbsp;<asp:button id="btnUpdate" runat="server" Height="20px" Width="65px" Text="수정" Enabled="False"></asp:button>&nbsp;<asp:button id="btnDelete" runat="server" Height="20px" Width="65px" Text="삭제" Enabled="False"></asp:button>&nbsp;<asp:button id="btnReg" runat="server" Height="20px" Width="65px" Text="등 록" Font-Size="9pt"></asp:button>&nbsp;</TD>
								</TR>
								<TR>
									<TD align="right" width="70" colSpan="9" height="8"></TD>
								</TR>
							</TABLE>
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
									<TD vAlign="top" align="center" rowSpan="1"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="390px" Width="800px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" RowHeightDefault="20px" Version="3.00"
												BorderCollapseDefault="Separate" RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed"
												CellClickActionDefault="RowSelect">
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
												<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray" Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="390px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents CellClickHandler="UltraWebGrid1_CellClickHandler"></ClientSideEvents>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<RowAlternateStyleDefault BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemNum" Width="150px" BaseColumnName="품목명">
															<HeaderStyle Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" Width="140px" BaseColumnName="거래처명"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래처등록번호" Key="BusinessRegistrationNum" Width="110px" Hidden="True" BaseColumnName="거래처등록번호">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="개별단가" Key="Cost" Width="80px" Format="###,###,##0.00" BaseColumnName="개별단가">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수량" Key="Quantity" Format="###,###,##0.00" BaseColumnName="수량">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총금액" Key="TotalCost" Width="120px" Format="###,###,##0.00" BaseColumnName="총금액">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="납기요구일" Key="DeliveryDate" Format="yyyy-MM-dd" BaseColumnName="납기요구일">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="구분" Key="ReasonName" Width="108px" BaseColumnName="구분"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="구분코드" Key="ReasonCode" Hidden="True" BaseColumnName="구분코드"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="인덱스번호" Key="IndexNum" Hidden="True" Format="#########" BaseColumnName="인덱스번호"></igtbl:UltraGridColumn>
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
