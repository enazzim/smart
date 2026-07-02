<%@ Page language="c#" Codebehind="RowItemExhaust.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.RowItemExhaust" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearch" Src="ItemSearch/ItemSearch.ascx" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RowItemExhaust</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<LINK href="../LinkLine.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		
		
		
		
		function ResettxtBox()
		{
			
			ResetTextBox();
			ResetBox();
			var objChooser1 = igdrp_getComboById("wdcExhaustDate");
			objChooser1.setValue(null);
			document.Form1.tbRowUnit.value = "";
			document.Form1.tbRowStandard.value = "";
			document.Form1.tbCreateUnit.value = "";
			document.Form1.tbCreateStandard.value = "";
			
			document.Form1.tbExhaustQuantity.value = "0";
			document.Form1.tbCreateQuantity.value = "0";
			document.Form1.txtHistoryIndex.value = "";
			document.Form1.txtOldQuantity.value = "0";
			document.Form1.txtUse.value = "";
			document.Form1.txtStartHour.value = "";
			document.Form1.txtStartMinute.value = "";
			document.Form1.txtEndHour.value = "";
			document.Form1.txtEndMinute.value = "";
			document.Form1.ddlWorker.options[0].selected=true;
					
		}
		
		
		function DoPost()
		{
			__doPostBack('LinkButton3','');
		}
		
		function DoPostChild()
		{
			__doPostBack('LinkButton4','');
		}
		
		
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
				} else if( !IsNumericKey() ){
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

		function OnFocus_Obj(obj) {
			obj.select();
		}
	
		function ValidateFloat(a_sValue)	{	
			var s_FieldValue = new String(a_sValue);
			
			if (isNaN(s_FieldValue))
				return false;
				
			return true;
		}
		
		
		</script>
	</HEAD>
	<body bgColor="#f7f6f6" MS_POSITIONING="GridLayout" XMLNS:igtbl="http://schemas.infragistics.com/ASPNET/WebControls/UltraWebGrid">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="LEFT: 10px; WIDTH: 800px; POSITION: absolute; TOP: 10px" cellSpacing="0"
				cellPadding="0" width="800" border="0">
				<TR>
					<TD></TD>
					<TD vAlign="top" align="left" width="800">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">[입&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;력]</LEGEND>
							<TABLE id="Table2" height="30" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD width="600" colSpan="4" height="30"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol><asp:linkbutton id="LinkButton3" runat="server" Visible="False">LinkButton</asp:linkbutton></TD>
									<TD align="right" width="40">단위</TD>
									<TD width="40"><asp:textbox id="tbRowUnit" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Width="40px" Height="20px"
											BorderColor="DimGray" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True"></asp:textbox></TD>
									<TD align="right" width="40">규격</TD>
									<TD align="left" width="80"><asp:textbox id="tbRowStandard" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Width="80px"
											Height="20px" BorderColor="DimGray" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True"></asp:textbox></TD>
								</TR>
								<TR>
									<TD align="left" colSpan="8">
										<table id="tb" cellSpacing="0" cellPadding="0" width="800" border="0">
											<tr>
												<td align="right" width="50">&nbsp;소진량</td>
												<td width="70"><asp:textbox id="tbExhaustQuantity" style="TEXT-ALIGN: right" runat="server" Font-Size="9pt"
														BackColor="#EEEEE9" Width="70px" Height="20px" BorderColor="DimGray" BorderStyle="Solid" BorderWidth="1px">0</asp:textbox></td>
												<td align="right" width="50">일자&nbsp;</td>
												<td width="100"><igsch:webdatechooser id="wdcExhaustDate" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Width="95px"
														Height="20px" BorderColor="DimGray" BorderStyle="Solid" NullDateLabel=" " Text="Null" MaxDate="2079-06-06">
														<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
															MaxDate="2079-06-06" ShowTitle="False" ShowFooter="False">
															<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
															<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
															<DropDownStyle BackColor="White"></DropDownStyle>
															<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
														</CalendarLayout>
														<DropDownStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></DropDownStyle>
														<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
														<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
													</igsch:webdatechooser></td>
												<td align="right" width="50">용도</td>
												<td width="150"><asp:textbox id="txtUse" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Width="150px" Height="20px"
														BorderColor="DimGray" BorderStyle="Solid" BorderWidth="1px"></asp:textbox></td>
												<td align="right" width="50">작업자</td>
												<td align="left"><asp:dropdownlist id="ddlWorker" runat="server" BackColor="#EEEEE9"></asp:dropdownlist></td>
											</tr>
										</table>
									</TD>
								</TR>
								<TR>
									<TD align="left" colSpan="8" height="10"></TD>
								</TR>
								<TR>
									<TD width="600" colSpan="4" height="30"><uc1:itemsearch id="ItemSearch1" runat="server"></uc1:itemsearch><asp:linkbutton id="Linkbutton4" runat="server" Visible="False">LinkButton</asp:linkbutton></TD>
									<TD align="right" width="40">단위</TD>
									<TD width="40"><asp:textbox id="tbCreateUnit" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Width="40px"
											Height="20px" BorderColor="DimGray" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True"></asp:textbox></TD>
									<TD align="right" width="40">규격</TD>
									<TD align="left" width="80"><asp:textbox id="tbCreateStandard" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Width="80px"
											Height="20px" BorderColor="DimGray" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True"></asp:textbox></TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="left" width="100%" colSpan="8">
										<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="800" border="0">
											<TR>
												<TD align="right" width="50">생성량</TD>
												<TD width="80"><asp:textbox id="tbCreateQuantity" style="TEXT-ALIGN: right" runat="server" Font-Size="9pt" BackColor="#EEEEE9"
														Width="70px" Height="20px" BorderColor="DimGray" BorderStyle="Solid" BorderWidth="1px">0</asp:textbox></TD>
												<TD align="right" width="50">시작</TD>
												<TD align="right" width="40"><asp:textbox id="txtStartHour" style="TEXT-ALIGN: right" runat="server" Font-Size="9pt" BackColor="#EEEEE9"
														Width="40px" Height="20px" BorderColor="DimGray" BorderStyle="Solid" BorderWidth="1px"></asp:textbox></TD>
												<TD width="10">시</TD>
												<TD align="right" width="40"><asp:textbox id="txtStartMinute" style="TEXT-ALIGN: right" runat="server" Font-Size="9pt" BackColor="#EEEEE9"
														Width="40px" Height="20px" BorderColor="DimGray" BorderStyle="Solid" BorderWidth="1px"></asp:textbox></TD>
												<TD width="50">분</TD>
												<TD width="10">~</TD>
												<TD align="right" width="50">종료</TD>
												<TD align="right" width="40"><asp:textbox id="txtEndHour" style="TEXT-ALIGN: right" runat="server" Font-Size="9pt" BackColor="#EEEEE9"
														Width="40px" Height="20px" BorderColor="DimGray" BorderStyle="Solid" BorderWidth="1px"></asp:textbox></TD>
												<TD width="10">시</TD>
												<TD align="right" width="40"><asp:textbox id="txtEndMinute" style="TEXT-ALIGN: right" runat="server" Font-Size="9pt" BackColor="#EEEEE9"
														Width="40px" Height="20px" BorderColor="DimGray" BorderStyle="Solid" BorderWidth="1px"></asp:textbox></TD>
												<TD>분</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="right" width="100%" colSpan="8">&nbsp;<INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
											type="button" value="초기화" name="btnReset">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:button id="btnAdd" runat="server" Font-Size="9pt" Width="60px" Height="20px" BorderStyle="Outset"
											Text="등 록"></asp:button>&nbsp;&nbsp;
									</TD>
								</TR>
							</TABLE>
						</FIELDSET>
						<BR>
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 9pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"
							DESIGNTIMEDRAGDROP="86"><LEGEND align="top">[입력결과]</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD vAlign="top" align="center" colSpan="3"><igtbl:ultrawebgrid id="uwgE_HT" runat="server" Height="360px" Width="100%">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="uwgExHT" TableLayout="Fixed" CellClickActionDefault="RowSelect">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray"></Style>
												</AddNewBox>
												<Pager>
													<Style VerticalAlign="Middle" BorderWidth="1px" BorderStyle="Solid" HorizontalAlign="Center"
														BackColor="LightGray"></Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" BorderColor="Black" ForeColor="Black" BackColor="LightGray" Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<GroupByRowStyleDefault ForeColor="Black" BackColor="#95B9F0"></GroupByRowStyleDefault>
												<RowSelectorStyleDefault BorderStyle="Solid"></RowSelectorStyleDefault>
												<FrameStyle Width="100%" Cursor="Default" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림"
													BorderColor="DimGray" BorderStyle="Solid" BackColor="Silver" Height="360px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<GroupByBox>
													<Style BackColor="#B5CFF7"></Style>
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
														<igtbl:UltraGridColumn HeaderText="일자" Key="ExhaustDate" Format="yyyy-MM-dd" BaseColumnName="ExhaustDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="소진 품목번호" Key="ExhaustItemNum" Width="150px" BaseColumnName="ExhaustItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left">
																<Padding Left="2px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단위" Key="ExhaustUnit" Width="40px" BaseColumnName="ExhaustUnit">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="소진수량" Key="ExhaustQuantity" Width="80px" Format="###,###,##0.00" BaseColumnName="ExhaustQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ExhaustItemDrawNum" Hidden="True" BaseColumnName="ExhaustItemDrawNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="소진 품목명" Key="ExhaustItemName" Width="150px" BaseColumnName="ExhaustItemName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left">
																<Padding Left="2px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="생성수량" Key="CreateQuantity" Width="80px" Format="###,###,##0.00" BaseColumnName="CreateQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="생성 품목번호" Key="CreateItemNum" Width="150px" BaseColumnName="CreateItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left">
																<Padding Left="2px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="생성 품목명" Key="CreateItemName" Width="150px" BaseColumnName="CreateItemName">
															<CellStyle HorizontalAlign="Left">
																<Padding Left="2px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단위" Key="CreateUnit" Width="40px" BaseColumnName="CreateUnit">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="용도" Key="Use" Width="150px" BaseColumnName="Use">
															<CellStyle HorizontalAlign="Left">
																<Padding Left="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="규격" Key="ExhaustStandard" Hidden="True" BaseColumnName="ExhaustStandard">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="CreateItemDrawNum" Hidden="True" BaseColumnName="CreateItemDrawNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="규격" Key="CreateStandard" Hidden="True" BaseColumnName="CreateStandard">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="시작시간" Key="StartTime" Width="80px" BaseColumnName="StartTime">
															<CellStyle HorizontalAlign="Left">
																<Padding Left="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="종료시간" Key="EndTime" Width="80px" BaseColumnName="EndTime">
															<CellStyle HorizontalAlign="Left">
																<Padding Left="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="자재소진원장번호" Key="ExhaustHistoryIndex" Hidden="True" BaseColumnName="ExhaustHistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
													</Columns>
													<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
														<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
													</RowTemplateStyle>
													<RowEditTemplate>
														<P align="center">&nbsp;</P>
													</RowEditTemplate>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
