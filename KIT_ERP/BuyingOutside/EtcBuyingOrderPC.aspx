<%@ Page language="c#" Codebehind="EtcBuyingOrderPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BuyingOutside.EtcBuyingOrderPC" codePage="949" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>EtcBuyingOrderPC</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
			<script language="javascript">
		<!--
					function UltraWebGrid1_BeforeRowTemplateOpenHandler(gridName, rowId, templateId)
					{
						var row = igtbl_getRowById(rowId);		
						var parts = rowId.split("_");
						document.Form1.hdIndex.value = parts[1];				//그리드의 선택되어진 RowIndex를 저장하는 Hidden 필드
							
						if(row.getCellFromKey("RecodingState").getValue() != "대기")
						{
							alert("현재 품목은 납품이 진행중 이거나 완료 되었으므로 수정할 수 없습니다.");
							return true;
						}
					}
					
					function UltraWebGrid1_AfterRowTemplateOpenHandler(gridName, rowId)
					{
					
						var row = igtbl_getRowById(rowId);
						var objChoo = igdrp_getComboById("UltraWebGrid1xxctl0xRTxDeliveryDemandDate");
						
						var dDate = new Date(row.getCellFromKey("DeliveryDemandDate").getValue());
						objChoo.setValue( dDate );
						
						document.Form1.RT_TotalCost.value = MakeCommaSeparate( document.Form1.RT_TotalCost.value );
						document.Form1.RT_Cost.value = MakeCommaSeparate( document.Form1.RT_Cost.value );
						document.Form1.RT_OrderQuantity.value = MakeCommaSeparate( document.Form1.RT_OrderQuantity.value );
						
						
						
						for( var i = 0 ; i < document.Form1.UltraWebGrid1__ctl0_ddlReason.options.length ; i ++ )
						{
							if( document.Form1.UltraWebGrid1__ctl0_ddlReason.options[i].innerText == row.getCellFromKey("ReasonName").getValue() )
							{
								document.Form1.UltraWebGrid1__ctl0_ddlReason.options[i].selected = true ;
							}
						}
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
					
					// 계산
					function Process()
					{
						document.Form1.RT_TotalCost.value 
							= MakeCommaSeparate((Number(RemoveComma(document.Form1.RT_Cost.value)) 
							* Number(RemoveComma(document.Form1.RT_OrderQuantity.value))));
					}
					
					function CheckProcessDoPostBack()
					{
							// 수정 입력값 체크
							if ( (document.Form1.RT_Cost.value.trim() == "0" || document.Form1.RT_Cost.value.trim() == "") ||
								(document.Form1.RT_OrderQuantity.value.trim() == "0" || document.Form1.RT_OrderQuantity.value.trim() == "") ||
								(document.Form1.RT_TotalCost.value.trim() == "0" || document.Form1.RT_TotalCost.value.trim() == "") ||
								(document.Form1.RT_TotalCost.value.trim() == "0" || document.Form1.RT_TotalCost.value.trim() == "") ||
								(document.Form1.RT_ItemName.value.trim() == "")
							)
							{
								alert("수정 사항들을 정확히 입력해 주세요.");
							}
							else
							{
								if ( confirm("수정 하시겠습니까?") )
								{
									// 그리드에 업데이트
									igtbl_gRowEditButtonClick(event); 
									
									// Row Template의 WebDateChooser의 값은 그리드에 igtbl_gRowEditButtonClick() 함수로 
									// 저장(업데이트)되지 않으므로 수동으로 값을 넣어준다.
									var rowId = "UltraWebGrid1r_" + document.Form1.hdIndex.value
									var row =  igtbl_getRowById(rowId);
									var objChoo = igdrp_getComboById("UltraWebGrid1xxctl0xRTxDeliveryDemandDate");
									row.getCellFromKey("DeliveryDemandDate").setValue(objChoo.getValue());
									row.getCellFromKey("ReasonCode").setValue(document.Form1.UltraWebGrid1__ctl0_ddlReason.options[document.Form1.UltraWebGrid1__ctl0_ddlReason.selectedIndex].value);
									row.getCellFromKey("ReasonName").setValue(document.Form1.UltraWebGrid1__ctl0_ddlReason.options[document.Form1.UltraWebGrid1__ctl0_ddlReason.selectedIndex].innerText);
				
									
									// 클릭이벤트
									__doPostBack("lkbtnUpdate");	
								}
							}
						
					}
	
					// Javascript Trim() 메서드 
					String.prototype.trim = function()
					{
						return this.replace(/(^\s*)|(\s*$)/g,"");
					}
					
					// 월마감 체크, 삭제여부 체크
					function DeleteCheck()
					{
						var grid = igtbl_getGridById("UltraWebGrid1");
						var bIsClick = false;
						
						for ( var i = 0 ; i < grid.Rows.length ; i ++ )
						{
							var row = grid.Rows.getRow(i);
							if (row.getCellFromKey("chk").getValue() )
							{
								bIsClick = true;
								break;;
							}
						}
						
						if ( bIsClick == true)
						{
							return confirm('정말 삭제 하시겠습니까?');
						}
						else
						{
							alert("삭제할 품목의 체크박스를 선택하고 삭제버튼을 눌러주세요.");
							return false;
						}
					}
					
					// 초기화 버튼 핸들러
					function Reset_TextBox()
					{
						var frm = document.Form1;
						frm.txtItemName.value = "";
						frm.ddlState.options[0].selected = true;
						
						ResetBox();
						
						var objDChooser = igdrp_getComboById("wcDeliveryDate");
						objDChooser.setValue(null);
						
						var objDChooser1 = igdrp_getComboById("wcDeliveryDate1");
						objDChooser1.setValue(null);
						
						
					}
		function UltraWebGrid1_ColumnHeaderClickHandler(gridName, columnId, button)
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
		


		//-->
			</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="800" border="0" style="POSITION: absolute; TOP: 10px; LEFT: 10px">
				<TR>
					<TD width="20" height="80"></TD>
					<TD vAlign="top" height="80">
						<FIELDSET style="BORDER-BOTTOM: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-TOP: dimgray 2px solid; BORDER-RIGHT: dimgray 2px solid"
							align="middle">
							<LEGEND style="FONT-SIZE: 9pt" align="left">[ 검색&nbsp;] </LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
								<TR>
									<TD align="right" width="60" height="30">품목명&nbsp;
									</TD>
									<TD width="100" height="30"><asp:textbox id="txtItemName" runat="server" BorderColor="DimGray" Height="20px" BackColor="#EEEEE9"
											BorderWidth="1px" BorderStyle="Solid" Width="100px"></asp:textbox></TD>
									<TD align="left" width="200" height="30" colSpan="2">
										<uc1:CompanySearchControl id="CSC1" runat="server"></uc1:CompanySearchControl>
									</TD>
									<TD align="right" width="65" height="30">납기요구일&nbsp;
									</TD>
									<TD align="left" width="100" height="30"><igsch:webdatechooser id="wcDeliveryDate" runat="server" BorderColor="DimGray" Height="20px" BackColor="#EEEEE9"
											BorderStyle="Solid" Width="100px" NullDateLabel=" " Text=" ">
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
									<TD align="center" width="15" height="30">~
									</TD>
									<TD align="right" width="80" height="30">
										<igsch:webdatechooser id="wcDeliveryDate1" runat="server" Width="100px" BorderStyle="Solid" BackColor="#EEEEE9"
											Height="20px" BorderColor="DimGray" Text=" " NullDateLabel=" ">
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
									<TD align="right" height="30">진행상태&nbsp;
									</TD>
									<TD align="left" height="30"><asp:dropdownlist id="ddlState" runat="server" Height="20px" Width="80px">
											<asp:ListItem Value="전체">전체</asp:ListItem>
											<asp:ListItem Value="대기">납품대기</asp:ListItem>
											<asp:ListItem Value="완료">납품완료</asp:ListItem>
										</asp:dropdownlist>&nbsp;&nbsp;&nbsp;
									</TD>
								</TR>
								<TR>
									<TD align="right" colSpan="10" height="30"><INPUT id="btnInit" style="HEIGHT: 20px" onclick="Reset_TextBox();" type="button" value="초기화"
											name="btnInit">&nbsp;
										<asp:button id="btnSearch" runat="server" Width="65px" Height="20px" Text="검 색" Font-Size="9pt"></asp:button></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="20" height="400">
						<P>&nbsp;</P>
					</TD>
					<TD vAlign="top" align="center">
						<FIELDSET style="BORDER-BOTTOM: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; HEIGHT: 100%; BORDER-TOP: dimgray 2px solid; FONT-WEIGHT: normal; BORDER-RIGHT: dimgray 2px solid"
							align="middle">
							<LEGEND style="FONT-SIZE: 10pt" align="left">[&nbsp;검색결과 ] </LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" align="center">
								<TR height="35">
									<TD vAlign="top" align="center" colSpan="2">
										<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="387px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" RowHeightDefault="20px" Version="3.00"
												SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" BorderCollapseDefault="Separate"
												AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed"
												CellClickActionDefault="RowSelect" AllowUpdateDefault="Yes">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray"></Style>
												</AddNewBox>
												<Pager PageSize="16" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray"></Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray" Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="387px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents ColumnHeaderClickHandler="UltraWebGrid1_ColumnHeaderClickHandler" BeforeRowTemplateOpenHandler="UltraWebGrid1_BeforeRowTemplateOpenHandler"
													AfterRowTemplateOpenHandler="UltraWebGrid1_AfterRowTemplateOpenHandler"></ClientSideEvents>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedRowStyleDefault ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand AllowUpdate="RowTemplateOnly">
													<Columns>
														<igtbl:UltraGridColumn HeaderText="선택" Key="chk" Width="40px" Type="CheckBox" HeaderClickAction="Select"
															BaseColumnName="" AllowUpdate="Yes">
															<CellStyle HorizontalAlign="Center"></CellStyle>
															<HeaderStyle Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="130px" BaseColumnName="ItemName"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" Width="120px" BaseColumnName="CompanyName"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래처등록번호" Key="BusinessRegistrationNum" BaseColumnName="BusinessRegistrationNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="발주수량" Key="OrderQuantity" Width="80px" Format="###,###,##0" BaseColumnName="OrderQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="개별단가" Key="ApplyUnitCost" Width="120px" Format="###,###,##0" BaseColumnName="ApplyUnitCost">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총금액" Key="TotalCost" Width="120px" Format="###,###,##0" BaseColumnName="TotalCost">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="납기요구일" Key="DeliveryDemandDate" Format="yyyy-MM-dd" BaseColumnName="DeliveryDemandDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="구분" Key="ReasonName" BaseColumnName="ReasonName"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자 ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" BaseColumnName="RegistrationPerson"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자 ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" BaseColumnName="UpdatingPerson"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="발주원장번호" Key="EtcBuyingOrderHistoryIndex" Hidden="True" BaseColumnName="EtcBuyingOrderHistoryIndex"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="상태" Key="RecodingState" BaseColumnName="RecodingState"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="구분코드" Key="ReasonCode" Hidden="True" BaseColumnName="ReasonCode"></igtbl:UltraGridColumn>
													</Columns>
													<RowTemplateStyle Cursor="Default" BorderWidth="2px" BorderColor="Black" BorderStyle="Solid" BackColor="White">
														<Padding Bottom="3px" Left="3px" Top="3px" Right="3px"></Padding>
														<Margin Bottom="3px" Left="3px" Top="3px" Right="3px"></Margin>
														<BorderDetails WidthLeft="1px" WidthTop="1px" WidthRight="1px" WidthBottom="1px"></BorderDetails>
													</RowTemplateStyle>
													<RowEditTemplate>
														<TABLE id="Table4" cellSpacing="2" cellPadding="0" align="center" bgColor="#f5f5f5">
															<TR>
																<TD align="right" width="70" height="10"></TD>
																<TD width="150" height="10"></TD>
																<TD align="right" width="65" height="10"></TD>
																<TD vAlign="middle" width="150" height="10"></TD>
																<TD align="right" width="80" height="10"></TD>
																<TD vAlign="middle" width="135" colSpan="3" height="10"></TD>
															</TR>
															<TR>
																<TD align="right" width="70" bgColor="#a9a9a9" colSpan="8" height="2"></TD>
															</TR>
															<TR>
																<TD align="right" bgColor="#dcdcdc" height="25">품목명&nbsp;
																</TD>
																<TD height="25"><INPUT id="RT_ItemName" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BORDER-BOTTOM: dimgray 1px solid; BACKGROUND-COLOR: #eeeee9"
																		type="text" size="15" name="RT_ItemName" columnKey="ItemName">
																</TD>
																<TD align="right" bgColor="#dcdcdc" height="25">거래처명&nbsp;</TD>
																<TD vAlign="middle" height="25"><INPUT id="RT_CompanyName" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BORDER-BOTTOM: dimgray 1px solid; BACKGROUND-COLOR: #eeeee9"
																		readOnly type="text" size="15" name="RT_CompanyName" columnKey="CompanyName"></TD>
																<TD align="right" bgColor="#dcdcdc" height="25">개별단가&nbsp;
																</TD>
																<TD vAlign="middle" colSpan="3" height="25"><INPUT id="RT_Cost" onkeydown="OnKeyDown_Float(this);" onkeyup="Process();  OnKeyUp_Currency(this);"
																		style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BORDER-BOTTOM: dimgray 1px solid; BACKGROUND-COLOR: #eeeee9"
																		type="text" size="15" name="RT_Cost" columnKey="ApplyUnitCost"></TD>
															</TR>
															<TR>
																<TD align="right" bgColor="#dcdcdc" height="25">납기요구일&nbsp;
																</TD>
																<TD height="25">
																	<igsch:webdatechooser id="RT_DeliveryDemandDate" runat="server" BorderStyle="Solid" BackColor="#EEEEE9"
																		Height="20px" BorderColor="DimGray" Text=" " NullDateLabel=" " BrowserTarget="UpLevel">
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
																<TD align="right" bgColor="#dcdcdc" height="25">발주수량&nbsp;</TD>
																<TD height="25"><INPUT id="RT_OrderQuantity" onkeydown="OnKeyDown_Float(this);" onkeyup="Process(); OnKeyUp_Currency(this);"
																		style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BORDER-BOTTOM: dimgray 1px solid; BACKGROUND-COLOR: #eeeee9"
																		type="text" size="15" name="RT_OrderQuantity" columnKey="OrderQuantity"></TD>
																<TD align="right" bgColor="#dcdcdc" height="25">총금액&nbsp;
																</TD>
																<TD height="25"><INPUT id="RT_TotalCost" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BORDER-BOTTOM: dimgray 1px solid; BACKGROUND-COLOR: #eeeee9"
																		readOnly type="text" size="15" name="RT_TotalCost" columnKey="TotalCost">
																</TD>
															</TR>
															<TR>
																<TD align="right" bgColor="#dcdcdc" height="25">구분&nbsp;
																</TD>
																<TD height="25"><FONT face="굴림">
																		<asp:DropDownList id=ddlReason runat="server" BackColor="#EEEEE9" DataTextField="SmallClassificationName" DataSource="<%# EtcStore %>" DataValueField="SmallClassificationCode">
																		</asp:DropDownList></FONT></TD>
																<TD align="right" bgColor="#dcdcdc" height="25">&nbsp;
																</TD>
																<TD height="25"><FONT face="굴림"></FONT></TD>
																<TD align="right" bgColor="#dcdcdc" height="25">&nbsp;
																</TD>
																<TD height="25"><FONT face="굴림"></FONT></TD>
															</TR>
															<TR>
																<TD align="right" bgColor="#a9a9a9" colSpan="8" height="2"></TD>
															</TR>
															<TR>
																<TD align="right" height="45"></TD>
																<TD height="45"></TD>
																<TD align="right" height="45"></TD>
																<TD height="45"></TD>
																<TD align="right" colSpan="4" height="45"><INPUT id="igtbl_reOkBtn" style="WIDTH: 60px" onclick="CheckProcessDoPostBack();" type="button"
																		value="수정" name="igtbl_reOkBtn">&nbsp;<INPUT id="igtbl_reCancelBtn" style="WIDTH: 60px" onclick="igtbl_gRowEditButtonClick(event);"
																		type="button" value="닫기" name="igtbl_reCancelBtn">&nbsp;</TD>
															</TR>
														</TABLE>
													</RowEditTemplate>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="left" height="30">&nbsp;
										<asp:button id="btnExcel" runat="server" Height="20px" Width="65px" Text="Excel"></asp:button>&nbsp;
										<asp:button id="Button1" runat="server" Width="75px" Height="20px" Text="발주서 발행" Font-Size="9pt"></asp:button><igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter><INPUT id="hdIndex" style="WIDTH: 32px; HEIGHT: 21px" type="hidden" size="1" name="Hidden1"
											runat="server">
										<asp:linkbutton id="lkbtnUpdate" runat="server"></asp:linkbutton><INPUT type="hidden" size="1" value="true" id="chkAll"></TD>
									<TD vAlign="middle" align="right" height="30"><asp:button id="btnDelete" runat="server" Height="20px" Width="65px" Text="삭제"></asp:button>&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
