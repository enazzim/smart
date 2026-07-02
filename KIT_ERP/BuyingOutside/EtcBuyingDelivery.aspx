<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="EtcBuyingDelivery.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BuyingOutside.EtcBuyingDelivery" codePage="949" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>EtcBuyingDelivery</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--		
			// Javascript Trim() 메서드 
			String.prototype.trim = function()
			{
				return this.replace(/(^\s*)|(\s*$)/g,"");
			}
		
			function UltraWebGrid1_BeforeRowActivateHandler(gridName, rowId)
			{
				var row = igtbl_getRowById(rowId);
				var frm = document.Form1;
				frm.txtD_ItemName.value = row.getCellFromKey("ItemName").getValue();
				frm.txtD_CompanyName.value = row.getCellFromKey("CompanyName").getValue();
				frm.txtD_OrderQuantity.value = MakeCommaSeparate(row.getCellFromKey("OrderQuantity").getValue());
				frm.HDIndexNum.value = row.getCellFromKey("EtcBuyingOrderHistoryIndex").getValue();
				frm.HDBusinessRegNum.value = row.getCellFromKey("BusinessRegistrationNum").getValue();
				frm.txtD_RemainQuantity.value = MakeCommaSeparate(row.getCellFromKey("RemainQuantity").getValue());
				frm.txtD_DeliveryQuantity.value = MakeCommaSeparate(row.getCellFromKey("RemainQuantity").getValue());
				frm.txtD_DeliveryQuantity.select();
				frm.tbApplycost.value = MakeCommaSeparate(row.getCellFromKey("ApplyUnitCost").getValue());	
				frm.hdReasonCode.value = row.getCellFromKey("ReasonCode").getValue();				
				frm.hdReasonName.value = row.getCellFromKey("ReasonName").getValue();	
				var parts = rowId.split("_");
				document.Form1.HDRowIndex.value = parts[1];				//그리드의 선택되어진 RowIndex를 저장하는 Hidden 필드
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
		
			// 등록버튼을 누른경우 Form 체크
			function CheckForm()
			{
			
			
				if ( MonthCloseing == "0" )
				{
					var objWdc = igdrp_getComboById("wDcDDeliveryDate");
					var deliveryDate = objWdc.getValue();


					if (
						document.Form1.txtD_ItemName.value.trim() == "" 
						|| document.Form1.txtD_CompanyName.value.trim() == "" 
						|| document.Form1.txtD_OrderQuantity.value.trim() == "" 
						|| document.Form1.txtD_RemainQuantity.value.trim() == "" 
						|| document.Form1.txtD_DeliveryQuantity.value.trim() == "" 
						//|| deliveryDate == null 
						//|| deliveryDate == "" 
						|| document.Form1.HDRowIndex.value == ""
					)
					{
						alert("필수 항목들을 정확하게 입력해 주세요.");
						return false;
					}
					else
					{
						if ( confirm( "등록 하시겠습니까?") ) 
							return true;
						else 
							return false;
					}
				}
				else
				{
					alert("월마감 되어 등록할 수 없습니다.");
					return false;
				}
			}
			
			// 초기화 버튼 핸들러
			function Reset_TextBox()
			{
				var frm = document.Form1;
				frm.txtItemName.value = "";
				
				ResetBox();
				
				var objDChooser = igdrp_getComboById("wcDeliveryDate");
				objDChooser.setValue(null);		
			}

--></SCRIPT>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" height="550" cellSpacing="0" cellPadding="0" width="800" border="0"
				style="LEFT: 10px; POSITION: absolute; TOP: 10px">
				<TR>
					<TD style="HEIGHT: 11px" width="20"></TD>
					<TD style="HEIGHT: 11px">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 60px"
							align="middle"><LEGEND style="FONT-SIZE: 9pt" align="left">[ 검색조건 ]
							</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
								<TR>
									<TD align="right" width="50" height="30">품목명&nbsp;
									</TD>
									<TD width="100" height="30"><asp:textbox id="txtItemName" runat="server" BorderColor="DimGray" Height="20px" BackColor="#EEEEE9"
											BorderWidth="1px" BorderStyle="Solid" Width="100px"></asp:textbox></TD>
									<TD align="left" width="200" height="30" colSpan="2">
										<uc1:CompanySearchControl id="CSC1" runat="server"></uc1:CompanySearchControl>
									</TD>
									<TD align="right" width="65" height="30">납기요구일&nbsp;
									</TD>
									<TD align="left" width="100" height="30"><igsch:webdatechooser id="wcDeliveryDate" runat="server" BorderColor="DimGray" Height="20px" BackColor="#EEEEE9"
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
									<TD align="center" width="15" height="30">~</TD>
									<TD align="left" width="75" colSpan="2" height="30">
										<igsch:webdatechooser id="wcDeliveryDate1" runat="server" Width="100px" BorderStyle="Solid" BackColor="#EEEEE9"
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
										</igsch:webdatechooser></TD>
									<TD align="right" height="30"><INPUT id="btnInit" style="HEIGHT: 20px" onclick="Reset_TextBox();" type="button" value="초기화"
											name="btnInit">&nbsp;<asp:button id="btnSearch" runat="server" Height="20px" Width="65px" Text="검 색" Font-Size="9pt"></asp:button>&nbsp;
									</TD>
								</TR>
								<TR>
									<TD align="right" width="70" colSpan="10" height="8"></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="20" height="5"></TD>
					<TD vAlign="top" align="center" height="5"></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 400px" width="20"></TD>
					<TD vAlign="top" align="center">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-WEIGHT: normal; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 100%"
							align="middle"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색 ]
							</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" align="center">
								<TR height="35">
									<TD vAlign="top" align="center" rowSpan="1"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="350px" Width="800px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" RowHeightDefault="20px" Version="3.00"
												BorderCollapseDefault="Separate" RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed"
												CellClickActionDefault="RowSelect">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</AddNewBox>
												<Pager PageSize="15" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</Pager>
												<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray" Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="350px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents BeforeRowActivateHandler="UltraWebGrid1_BeforeRowActivateHandler"></ClientSideEvents>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<RowAlternateStyleDefault BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" BaseColumnName="ItemName"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" Width="140px" BaseColumnName="CompanyName"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래처등록번호" Key="BusinessRegistrationNum" Width="110px" Hidden="True" BaseColumnName="BusinessRegistrationNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="구분코드" Key="ReasonCode" Hidden="True" BaseColumnName="ReasonCode"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="구분명" Key="ReasonName" Hidden="True" BaseColumnName="ReasonName"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="발주수량" Key="OrderQuantity" Format="###,###,##0.00" BaseColumnName="OrderQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Width="80px" Format="###,###,###" BaseColumnName="ApplyUnitCost">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총금액" Key="TotalCost" Width="120px" Format="###,###,##0.00" BaseColumnName="TotalCost">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="잔량" Key="RemainQuantity" Width="100px" Format="###,###,##0.00" BaseColumnName="RemainQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="납기요구일" Key="DeliveryDemandDate" Format="yyyy-MM-dd" BaseColumnName="DeliveryDemandDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" Hidden="True" BaseColumnName="RegistrationPerson"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Hidden="True" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" Hidden="True" BaseColumnName="UpdatingPerson"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Hidden="True" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="인덱스번호" Key="EtcBuyingOrderHistoryIndex" Hidden="True" Format="#########"
															BaseColumnName="EtcBuyingOrderHistoryIndex"></igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="20"></TD>
					<TD>
						<TABLE id="Table4" style="WIDTH: 100%; HEIGHT: 46px" cellSpacing="0" cellPadding="0">
							<TR>
								<TD align="right" colSpan="12" height="5"></TD>
							</TR>
							<TR>
								<TD align="right" width="50" height="30">품목명&nbsp;</TD>
								<TD width="100" height="30"><asp:textbox id="txtD_ItemName" runat="server" BorderColor="DimGray" Height="20px" BackColor="#EEEEE9"
										BorderWidth="1px" BorderStyle="Solid" Width="100px" ReadOnly="True"></asp:textbox></TD>
								<TD align="right" width="60" height="30">거래처명&nbsp;</TD>
								<TD width="100" height="30"><asp:textbox id="txtD_CompanyName" runat="server" BorderColor="DimGray" Height="20px" BackColor="#EEEEE9"
										BorderWidth="1px" BorderStyle="Solid" Width="100px" ReadOnly="True"></asp:textbox></TD>
								<TD align="right" width="65" height="30">발주수량&nbsp;</TD>
								<TD width="100" height="30"><asp:textbox id="txtD_OrderQuantity" style="TEXT-ALIGN: right" runat="server" BorderColor="DimGray"
										Height="20px" BackColor="#EEEEE9" BorderWidth="1px" BorderStyle="Solid" Width="100px" ReadOnly="True"></asp:textbox></TD>
								<TD height="30" width="40" align="right">단가&nbsp;</TD>
								<TD height="30" width="70">
									<asp:textbox id="tbApplycost" style="TEXT-ALIGN: right" runat="server" Width="70px" BorderStyle="Solid"
										BorderWidth="1px" BackColor="#EEEEE9" Height="20px" BorderColor="DimGray">0</asp:textbox></TD>
								<td width="60" align="right">매입년도</td>
								<td width="55">
									<asp:dropdownlist id="ddlYear" runat="server" Width="55px" BackColor="#EEEEE9">
										<asp:ListItem Value="2020">2020</asp:ListItem>
												<asp:ListItem Value="2021">2021</asp:ListItem>
												<asp:ListItem Value="2022">2022</asp:ListItem>
												<asp:ListItem Value="2023>2023</asp:ListItem>
												<asp:ListItem Value="2024">2024</asp:ListItem>
												<asp:ListItem Value="2025">2025</asp:ListItem>
												<asp:ListItem Value="2026">2026</asp:ListItem>
												<asp:ListItem Value="2027">2027</asp:ListItem>
												<asp:ListItem Value="2028">2028</asp:ListItem>
												<asp:ListItem Value="2029">2029</asp:ListItem>
												<asp:ListItem Value="2030">2030</asp:ListItem>
									</asp:dropdownlist></td>
								<td width="40" align="right">매입월</td>
								<td>
									<asp:dropdownlist id="ddlMon" runat="server" Width="40px" BackColor="#EEEEE9">
										<asp:ListItem Value="1">1</asp:ListItem>
										<asp:ListItem Value="2">2</asp:ListItem>
										<asp:ListItem Value="3">3</asp:ListItem>
										<asp:ListItem Value="4">4</asp:ListItem>
										<asp:ListItem Value="5">5</asp:ListItem>
										<asp:ListItem Value="6">6</asp:ListItem>
										<asp:ListItem Value="7">7</asp:ListItem>
										<asp:ListItem Value="8">8</asp:ListItem>
										<asp:ListItem Value="9">9</asp:ListItem>
										<asp:ListItem Value="10">10</asp:ListItem>
										<asp:ListItem Value="11">11</asp:ListItem>
										<asp:ListItem Value="12">12</asp:ListItem>
									</asp:dropdownlist></td>
							</TR>
							<TR>
								<TD align="right" height="30">잔량&nbsp;
								</TD>
								<TD height="30"><asp:textbox id="txtD_RemainQuantity" style="TEXT-ALIGN: right" runat="server" BorderColor="DimGray"
										Height="20px" BackColor="#EEEEE9" BorderWidth="1px" BorderStyle="Solid" Width="100px" ReadOnly="True"></asp:textbox></TD>
								<TD align="right" height="30">납품수량&nbsp;</TD>
								<TD height="30"><asp:textbox id="txtD_DeliveryQuantity" style="TEXT-ALIGN: right" runat="server" BorderColor="DimGray"
										Height="20px" BackColor="#EEEEE9" BorderWidth="1px" BorderStyle="Solid" Width="100px"></asp:textbox></TD>
								<TD align="right" height="30">납품일&nbsp;</TD>
								<TD width="100" height="30"><igsch:webdatechooser id="wDcDDeliveryDate" runat="server" BorderColor="DimGray" Height="20px" BackColor="#EEEEE9"
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
								<TD align="right" height="30" colSpan="6">
									<asp:button id="Button1" runat="server" Width="65px" Height="20px" Text="Excel" Font-Size="9pt"></asp:button>&nbsp;
									<asp:button id="btnReg" runat="server" Height="20px" Width="65px" Text="등 록" Font-Size="9pt"></asp:button>&nbsp;&nbsp;
								</TD>
							</TR>
						</TABLE>
						<INPUT id="HDBusinessRegNum" style="WIDTH: 24px; HEIGHT: 21px" type="hidden" size="1" runat="server"><INPUT id="HDIndexNum" style="WIDTH: 23px; HEIGHT: 21px" type="hidden" size="1" runat="server"><INPUT id="HDRowIndex" style="WIDTH: 24px; HEIGHT: 21px" type="hidden" size="1" runat="server">
						<INPUT id="hdReasonCode" style="WIDTH: 24px; HEIGHT: 21px" type="hidden" size="1" name="hdReasonCode"
							runat="server"><INPUT id="hdReasonName" style="WIDTH: 24px; HEIGHT: 21px" type="hidden" size="1" name="hdReasonName"
							runat="server">
					</TD>
				</TR>
			</TABLE>
			<igtblexp:UltraWebGridExcelExporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:UltraWebGridExcelExporter>
		</form>
	</body>
</HTML>
