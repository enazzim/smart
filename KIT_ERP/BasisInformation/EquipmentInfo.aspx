<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="EquipmentInfo.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.EquipmentInfo" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>EquipmentInfo</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<STYLE type="text/css">BODY { SCROLLBAR-FACE-COLOR: #ffffff; SCROLLBAR-HIGHLIGHT-COLOR: #ffffff; SCROLLBAR-SHADOW-COLOR: #ffffff; SCROLLBAR-3DLIGHT-COLOR: #808080; SCROLLBAR-ARROW-COLOR: #400040; SCROLLBAR-TRACK-COLOR: #ffffff; SCROLLBAR-DARKSHADOW-COLOR: #808080 }
		</STYLE>
		<script language="javascript">
		function OK(text)
		{
			if(confirm(text + "하시겠습니까?"))
				return true;
			else
				return false;
		}
		function UltraWebGrid1_AfterSelectChangeHandler(gridName, id){
			//Add code to handle your event here.
			var row = igtbl_getRowById(id);
			Form1.lb_ReferenceTable.value = row.getCellFromKey("CommunityDataTableID").getValue();
			Form1.lb_ReferenceIndex.value = row.getCellFromKey("CDTIndex").getValue();
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
	<body bgColor="#f7f6f6" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" cellSpacing="0"
				cellPadding="0" width="800" border="0" height="550">
				<TR>
					<TD>
						<table style="WIDTH: 800px" cellSpacing="0" cellPadding="0" width="800" border="0" height="540">
							<tr>
								<td height="20">
									<table id="a" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td align="right" width="70"><FONT face="굴림">설비선택&nbsp;</FONT></td>
											<td style="WIDTH: 630px" width="630" colSpan="7"><igcmbo:webcombo id="wc_EquipmentChoice" runat="server" Editable="True" BorderStyle="Inset" Version="3.00"
													BackColor="#EEEEE9" BorderWidth="1px" BorderColor="LightGray" SelBackColor="DarkBlue" SelectedIndex="-1" SelForeColor="White" ForeColor="Black"
													Height="22px" Width="100px" DropImageXP1="/ig_common/WebGrid3/ig_cmboDown1.bmp" DropImageXP2="/ig_common/WebGrid3/ig_cmboDown1.bmp">
													<Columns>
														<igtbl:UltraGridColumn HeaderText="설비번호" Key="EquipmentNum" Width="250px" BaseColumnName="EquipmentNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="설비명" Key="EquipmentName" Width="250px" BaseColumnName="EquipmentName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="기기고유번호" Key="InstrumentNum" Width="250px" BaseColumnName="InstrumentNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="번호" Key="EquipmentInfoIndex" Hidden="True" BaseColumnName="EquipmentInfoIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
													</Columns>
													<DropDownLayout DropdownWidth="750px" BorderCollapse="Separate" RowSelectors="No" AllowColSizing="Free"
														RowHeightDefault="20px" HeaderClickAction="SortMulti" AutoGenerateColumns="False" AllowSorting="OnClient"
														RowSizing="Free" DropdownHeight="400px" TableLayout="Fixed" StationaryMargins="Header" AllowRowSizing="Free">
														<RowStyle BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="White">
															<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
														</RowStyle>
														<SelectedRowStyle ForeColor="White" BackColor="DarkBlue"></SelectedRowStyle>
														<HeaderStyle BorderStyle="Solid" BackColor="LightGray">
															<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
														</HeaderStyle>
														<FrameStyle Width="100%" Cursor="Default" BorderWidth="2px" Font-Size="10pt" Font-Names="Verdana"
															BorderStyle="Ridge" BackColor="Silver" Height="100%"></FrameStyle>
													</DropDownLayout>
													<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
												</igcmbo:webcombo><asp:label id="lb_EquipmentIndex" runat="server" Width="50px" Visible="False" Font-Size="10pt">0</asp:label><asp:label id="lb_EquipmentName" runat="server" Visible="False">Label</asp:label></td>
											<td align="right" vAlign="bottom"><asp:linkbutton id="LinkButton1" runat="server" Font-Size="10pt">설비정보 보기</asp:linkbutton></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td vAlign="top" height="520">
									<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 400px"
										align="left"><LEGEND>[ 입 력 ]</LEGEND>
										<TABLE id="b" cellSpacing="0" cellPadding="0" width="800" border="0">
											<TR>
												<TD style="WIDTH: 92px" align="right" width="92"><FONT style="COLOR: red" face="굴림">*설비번호&nbsp;</FONT></TD>
												<TD align="left" width="110">
													<asp:textbox id="tb_EquipmentNum" runat="server" Width="100px" Height="20px" BackColor="#EEEEE9"
														BorderStyle="Solid" Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
												<TD style="WIDTH: 80px" align="right" width="80"><FONT style="COLOR: red" face="굴림">*설비명&nbsp;</FONT></TD>
												<TD align="left" width="110">
													<asp:textbox id="tb_EquipmentName" runat="server" Width="100px" Height="20px" BackColor="#EEEEE9"
														BorderStyle="Solid" Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
												<TD style="WIDTH: 108px" align="right" width="108"><FONT style="COLOR: red" face="굴림">*설비분류&nbsp;</FONT></TD>
												<TD align="left" width="110">
													<asp:dropdownlist id="dl_EquipmentClassification" runat="server" Width="80px" Height="15px" BackColor="#EEEEE9"
														Font-Size="10pt"></asp:dropdownlist></TD>
												<TD style="WIDTH: 73px" align="right" width="73"><FONT face="굴림">규격&nbsp;</FONT></TD>
												<TD align="left" width="120">
													<asp:textbox id="tb_Standard" runat="server" Width="100px" Height="20px" BackColor="#EEEEE9"
														BorderStyle="Solid" Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 92px" align="right" width="92"><FONT face="굴림">Capacity&nbsp;</FONT></TD>
												<TD align="left" width="110">
													<asp:textbox id="tb_Capacity" style="TEXT-ALIGN: right" runat="server" Width="70px" Height="20px"
														BackColor="#EEEEE9" BorderStyle="Solid" Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px">0</asp:textbox></TD>
												<TD style="WIDTH: 80px" align="right" width="80"><FONT face="굴림">전력량&nbsp;</FONT></TD>
												<TD align="left" width="110">
													<asp:textbox id="tb_ElectricCapacity" style="TEXT-ALIGN: right" runat="server" Width="70px" Height="20px"
														BackColor="#EEEEE9" BorderStyle="Solid" Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px">0</asp:textbox>Kw
												</TD>
												<TD style="WIDTH: 108px" align="right" width="108"><FONT style="FONT-SIZE: 8pt" face="굴림">시간당 
														사용료&nbsp;</FONT></TD>
												<TD align="left" width="90"><FONT face="굴림">
														<asp:textbox id="tb_UnitTimeUseCost" style="TEXT-ALIGN: right" runat="server" Width="70px" Height="20px"
															BackColor="#EEEEE9" BorderStyle="Solid" Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px">0</asp:textbox>원</FONT></TD>
												<TD style="WIDTH: 73px" align="right" width="73"><FONT face="굴림">단위&nbsp;</FONT></TD>
												<TD align="left" width="120">
													<asp:dropdownlist id="dl_Unit" runat="server" Width="80px" Height="15px" BackColor="#EEEEE9" Font-Size="10pt"></asp:dropdownlist></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 92px" align="right" width="92"><FONT style="COLOR: red" face="굴림">*WC명&nbsp;</FONT></TD>
												<TD align="left" width="110">
													<asp:dropdownlist id="dl_WCName" runat="server" Width="80px" Height="15px" BackColor="#EEEEE9" Font-Size="10pt"></asp:dropdownlist></TD>
												<TD style="WIDTH: 80px" align="right" width="80"><FONT face="굴림">투입인원&nbsp;</FONT></TD>
												<TD align="left" width="110">
													<asp:textbox id="tb_InjectionStaffNum" style="TEXT-ALIGN: right" runat="server" Width="70px"
														Height="20px" BackColor="#EEEEE9" BorderStyle="Solid" Font-Size="9pt" BorderColor="DimGray"
														BorderWidth="1px">0</asp:textbox>명
												</TD>
												<TD style="WIDTH: 108px" align="right" width="108"><FONT face="굴림">위치&nbsp;</FONT></TD>
												<TD align="left" width="110">
													<asp:dropdownlist id="dl_Location" runat="server" Width="80px" Height="15px" BackColor="#EEEEE9" Font-Size="10pt"></asp:dropdownlist></TD>
												<TD style="WIDTH: 73px" align="right" width="73"><FONT face="굴림">차지면적&nbsp;</FONT></TD>
												<TD align="left" width="110">
													<asp:textbox id="tb_OccupancyArea" style="TEXT-ALIGN: right" runat="server" Width="70px" Height="20px"
														BackColor="#EEEEE9" BorderStyle="Solid" Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px">0</asp:textbox>㎡
												</TD>
											</TR>
											<TR>
												<TD style="WIDTH: 92px" align="right" width="92"><FONT face="굴림">구입일&nbsp;</FONT></TD>
												<TD align="left" width="110">
													<igsch:webdatechooser id="wdc_BuyingDate" runat="server" Width="90px" Height="20px" BackColor="#EEEEE9"
														NullDateLabel=" " Text="Null" MaxDate="2079-06-06">
														<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
															MaxDate="2079-06-06" ShowTitle="False" ShowFooter="False">
															<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
															<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
															<DropDownStyle BackColor="White"></DropDownStyle>
															<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
														</CalendarLayout>
														<DropDownStyle BorderStyle="Inset"></DropDownStyle>
														<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
														<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
													</igsch:webdatechooser></TD>
												<TD style="WIDTH: 80px" align="right" width="80"><FONT face="굴림">구입가격&nbsp;</FONT></TD>
												<TD align="left" width="110">
													<asp:textbox id="tb_BuyingCost" style="TEXT-ALIGN: right" runat="server" Width="70px" Height="20px"
														BackColor="#EEEEE9" BorderStyle="Solid" Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px">0</asp:textbox></TD>
												<TD style="WIDTH: 108px" align="right" width="108"><FONT style="FONT-SIZE: 8pt" face="굴림">기기고유번호&nbsp;</FONT></TD>
												<TD align="left" width="100">
													<asp:textbox id="tb_InstrumentNum" runat="server" Width="80px" Height="20px" BackColor="#EEEEE9"
														BorderStyle="Solid" Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
												<TD style="WIDTH: 73px" align="right" width="73"><FONT face="굴림"></FONT></TD>
												<TD align="left" width="110"><FONT face="굴림"></FONT></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 92px" align="right" width="92"><FONT face="굴림">설비상태&nbsp;</FONT></TD>
												<TD align="left" width="100">
													<asp:dropdownlist id="dl_EquipmentState" runat="server" Width="80px" Height="15px" BackColor="#EEEEE9"
														Font-Size="10pt">
														<asp:ListItem Value="가동중">가동중</asp:ListItem>
														<asp:ListItem Value="가동중지중">가동중지중</asp:ListItem>
														<asp:ListItem Value="대여">대여</asp:ListItem>
													</asp:dropdownlist></TD>
												<TD style="WIDTH: 80px" align="right" width="80"><FONT face="굴림">검교정일&nbsp;</FONT></TD>
												<TD align="left" width="100">
													<igsch:webdatechooser id="wdc_CheckDate" runat="server" Width="90px" Height="20px" BackColor="#EEEEE9"
														NullDateLabel=" " Text="Null" MaxDate="2079-06-06">
														<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
															MaxDate="2079-06-06" ShowTitle="False" ShowFooter="False">
															<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
															<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
															<DropDownStyle BackColor="White"></DropDownStyle>
															<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
														</CalendarLayout>
														<DropDownStyle BorderStyle="Inset"></DropDownStyle>
														<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
														<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
													</igsch:webdatechooser></TD>
												<TD style="WIDTH: 108px" align="right" width="108"><FONT face="굴림">유효기간&nbsp;</FONT></TD>
												<TD align="left" width="110">
													<igsch:webdatechooser id="wdc_ValidPeriod" runat="server" Width="90px" Height="20px" BackColor="#EEEEE9"
														NullDateLabel=" " Text="Null" MaxDate="2079-06-06">
														<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
															MaxDate="2079-06-06" ShowTitle="False" ShowFooter="False">
															<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
															<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
															<DropDownStyle BackColor="White"></DropDownStyle>
															<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
														</CalendarLayout>
														<DropDownStyle BorderStyle="Inset"></DropDownStyle>
														<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
														<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
													</igsch:webdatechooser></TD>
												<TD style="WIDTH: 73px" align="right" width="73"><FONT face="굴림">검교정기관&nbsp;</FONT></TD>
												<TD align="left" width="100">
													<asp:textbox id="tb_CheckAgency" runat="server" Width="80px" Height="20px" BackColor="#EEEEE9"
														BorderStyle="Solid" Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 92px" align="right" width="92"><FONT face="굴림">가동준비시간&nbsp;</FONT></TD>
												<TD align="left" width="110">
													<asp:textbox id="tb_ReadyTime" style="TEXT-ALIGN: right" runat="server" Width="70px" Height="20px"
														BackColor="#EEEEE9" BorderStyle="Solid" Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px">0</asp:textbox>분
												</TD>
												<TD style="WIDTH: 80px" align="right" width="80"><FONT face="굴림">Cavity&nbsp;</FONT></TD>
												<TD align="left" width="110">
													<asp:textbox id="tb_Cavity" style="TEXT-ALIGN: right" runat="server" Width="70px" Height="20px"
														BackColor="#EEEEE9" BorderStyle="Solid" Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px">1</asp:textbox></TD>
												<TD style="WIDTH: 108px" align="right" width="108"><FONT face="굴림">내용년수&nbsp;</FONT></TD>
												<TD align="left" width="110">
													<asp:textbox id="tb_ValidityYear" style="TEXT-ALIGN: right" runat="server" Width="70px" Height="20px"
														BackColor="#EEEEE9" BorderStyle="Solid" Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px">0</asp:textbox>년
												</TD>
												<TD style="WIDTH: 73px" align="right" width="73"></TD>
												<TD align="left" width="110"></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 92px" align="right" width="92"><FONT face="굴림">설계Shot&nbsp;&nbsp;</FONT></TD>
												<TD align="left" width="110">
													<asp:textbox id="tb_DesignShot" style="TEXT-ALIGN: right" runat="server" Width="70px" Height="20px"
														BackColor="#EEEEE9" BorderStyle="Solid" Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px">0</asp:textbox></TD>
												<TD style="WIDTH: 80px" align="right" width="80"><FONT face="굴림">초기Shot&nbsp;</FONT></TD>
												<TD align="left" width="110">
													<asp:textbox id="tb_FirstShot" style="TEXT-ALIGN: right" runat="server" Width="70px" Height="20px"
														BackColor="#EEEEE9" BorderStyle="Solid" Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px">0</asp:textbox></TD>
												<TD style="WIDTH: 108px" align="right" width="108"><FONT face="굴림">누계Shot&nbsp;</FONT></TD>
												<TD align="left" width="110">
													<asp:textbox id="tb_TotalShot" style="TEXT-ALIGN: right" runat="server" Width="70px" Height="20px"
														BackColor="#EEEEE9" BorderStyle="Solid" Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px">0</asp:textbox></TD>
												<TD style="WIDTH: 73px" align="right" width="73"><FONT face="굴림">작업Shot&nbsp;</FONT></TD>
												<TD align="left" width="110">
													<asp:textbox id="tb_WorkShot" style="TEXT-ALIGN: right" runat="server" Width="70px" Height="20px"
														BackColor="#EEEEE9" BorderStyle="Solid" Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px">0</asp:textbox></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 92px" align="right" width="92"><FONT face="굴림">관리주기1&nbsp;</FONT></TD>
												<TD align="left" width="110">
													<asp:textbox id="tb_ManagePeriod1" runat="server" Width="80px" Height="20px" BackColor="#EEEEE9"
														BorderStyle="Solid" Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
												<TD style="WIDTH: 80px" align="right" width="80"><FONT face="굴림">관리일1&nbsp;</FONT></TD>
												<TD align="left" width="110">
													<igsch:webdatechooser id="wdc_ManageDate1" runat="server" Width="90px" Height="20px" BackColor="#EEEEE9"
														NullDateLabel=" " Text="Null" MaxDate="2079-06-06">
														<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
															MaxDate="2079-06-06" ShowTitle="False" ShowFooter="False">
															<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
															<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
															<DropDownStyle BackColor="White"></DropDownStyle>
															<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
														</CalendarLayout>
														<DropDownStyle BorderStyle="Inset"></DropDownStyle>
														<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
														<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
													</igsch:webdatechooser></TD>
												<TD style="WIDTH: 108px" align="right" width="108"><FONT face="굴림">관리주기2&nbsp;</FONT></TD>
												<TD align="left" width="110">
													<asp:textbox id="tb_ManagePeriod2" runat="server" Width="80px" Height="20px" BackColor="#EEEEE9"
														BorderStyle="Solid" Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
												<TD style="WIDTH: 73px" align="right" width="73"><FONT face="굴림">관리일2&nbsp;</FONT></TD>
												<TD align="left" width="110">
													<igsch:webdatechooser id="wdc_ManageDate2" runat="server" Width="90px" Height="20px" BackColor="#EEEEE9"
														NullDateLabel=" " Text="Null" MaxDate="2079-06-06">
														<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
															MaxDate="2079-06-06" ShowTitle="False" ShowFooter="False">
															<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
															<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
															<DropDownStyle BackColor="White"></DropDownStyle>
															<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
														</CalendarLayout>
														<DropDownStyle BorderStyle="Inset"></DropDownStyle>
														<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
														<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
													</igsch:webdatechooser></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 92px" align="right" width="92"><FONT face="굴림">관리주기3&nbsp;</FONT></TD>
												<TD align="left" width="110">
													<asp:textbox id="tb_ManagePeriod3" runat="server" Width="80px" Height="20px" BackColor="#EEEEE9"
														BorderStyle="Solid" Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
												<TD style="WIDTH: 80px" align="right" width="80"><FONT face="굴림">관리일3&nbsp;</FONT></TD>
												<TD align="left" width="110">
													<igsch:webdatechooser id="wdc_ManageDate3" runat="server" Width="90px" Height="20px" BackColor="#EEEEE9"
														NullDateLabel=" " Text="Null" MaxDate="2079-06-06">
														<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
															MaxDate="2079-06-06" ShowTitle="False" ShowFooter="False">
															<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
															<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
															<DropDownStyle BackColor="White"></DropDownStyle>
															<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
														</CalendarLayout>
														<DropDownStyle BorderStyle="Inset"></DropDownStyle>
														<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
														<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
													</igsch:webdatechooser></TD>
												<TD style="WIDTH: 108px" align="right" width="108"><FONT face="굴림">관리주기4&nbsp;</FONT></TD>
												<TD align="left" width="110">
													<asp:textbox id="tb_ManagePeriod4" runat="server" Width="80px" Height="20px" BackColor="#EEEEE9"
														BorderStyle="Solid" Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
												<TD style="WIDTH: 73px" align="right" width="73"><FONT face="굴림">관리일4&nbsp;</FONT></TD>
												<TD align="left" width="110">
													<igsch:webdatechooser id="wdc_ManageDate4" runat="server" Width="90px" Height="20px" BackColor="#EEEEE9"
														NullDateLabel=" " Text="Null" MaxDate="2079-06-06">
														<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
															MaxDate="2079-06-06" ShowTitle="False" ShowFooter="False">
															<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
															<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
															<DropDownStyle BackColor="White"></DropDownStyle>
															<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
														</CalendarLayout>
														<DropDownStyle BorderStyle="Inset"></DropDownStyle>
														<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
														<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
													</igsch:webdatechooser></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 92px" align="right" width="92" height="10"><FONT face="굴림"></FONT></TD>
												<TD align="left" width="110" height="10"></TD>
												<TD style="WIDTH: 80px" align="right" width="80" height="10"></TD>
												<TD align="left" width="110" height="10"></TD>
												<TD style="WIDTH: 108px" align="right" width="108" height="10"></TD>
												<TD align="left" width="110" height="10"></TD>
												<TD style="WIDTH: 73px" align="right" width="73" height="10"></TD>
												<TD align="left" width="110" height="10"></TD>
											</TR>
											<TR>
												<TD align="right" width="70">
													<asp:button id="bt_RelationView" runat="server" Width="60px" Height="20px" Text="참고보기"></asp:button></TD>
												<TD align="left" width="130">
													<asp:label id="lb_Index" runat="server" Width="10px" Visible="False"></asp:label></TD>
												<TD align="right" width="70"><INPUT id="lb_ReferenceIndex" style="WIDTH: 30px" type="hidden" name="lb_ReferenceIndex"
														runat="server"></FONT><FONT face="굴림"><INPUT id="lb_ReferenceTable" style="WIDTH: 30px" type="hidden" name="lb_ReferenceTable"
															runat="server"></FONT>
												</TD>
												<TD align="left" width="130"><FONT face="굴림"></FONT></TD>
												<TD align="right" width="400" colSpan="4">
													<asp:button id="bt_Clear" runat="server" Width="60px" Height="20px" Text="초기화"></asp:button><FONT face="굴림">&nbsp;</FONT>
													<asp:button id="bt_Update" runat="server" Width="60px" Height="20px" Text="수  정" Enabled="False"></asp:button><FONT face="굴림">&nbsp;</FONT>
													<asp:button id="bt_Delete" runat="server" Width="60px" Height="20px" Text="삭  제" Enabled="False"></asp:button><FONT face="굴림">&nbsp;</FONT>
													<asp:button id="bt_Registration" runat="server" Width="60px" Height="20px" Text="등  록"></asp:button><FONT face="굴림">&nbsp;</FONT>&nbsp;</TD>
											</TR>
											<TR>
												<TD colSpan="8" height="10">
													<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="280px">
														<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
															RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Extended" AllowColumnMovingDefault="OnServer"
															HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
															RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect"
															NoDataMessage="">
															<AddNewBox>
																<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
																</Style>
															</AddNewBox>
															<Pager>
																<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
																</Style>
															</Pager>
															<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray">
																<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
															</HeaderStyleDefault>
															<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
															<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderColor="DimGray"
																BorderStyle="Solid" BackColor="Silver" Height="280px"></FrameStyle>
															<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
																<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
															</FooterStyleDefault>
															<ClientSideEvents AfterSelectChangeHandler="UltraWebGrid1_AfterSelectChangeHandler"></ClientSideEvents>
															<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
															<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
															<RowAlternateStyleDefault Cursor="Hand" BorderColor="DarkGray" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
															<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
																<Padding Left="3px"></Padding>
																<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
															</RowStyleDefault>
														</DisplayLayout>
														<Bands>
															<igtbl:UltraGridBand>
																<Columns>
																	<igtbl:UltraGridColumn HeaderText="설비정보테이블" Key="StandardinfoTableID" Width="150px" BaseColumnName="StandardinfoTableID">
																		<HeaderStyle Height="25px"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="품목정보인덱스" Key="SITIndex" Width="50px" Hidden="True" BaseColumnName="SITIndex"></igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="자료실테이블" Key="CommunityDataTableID" Width="150px" BaseColumnName="CommunityDataTableID"></igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="자료실인덱스" Key="CDTIndex" Width="50px" Hidden="True" BaseColumnName="CDTIndex"></igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="제목" Key="Title" Width="500px" HeaderClickAction="SortSingle" BaseColumnName="Title"></igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="번호" Key="CRelationShipIndex" Width="50px" Hidden="True" BaseColumnName="CRelationShipIndex"></igtbl:UltraGridColumn>
																</Columns>
															</igtbl:UltraGridBand>
														</Bands>
													</igtbl:ultrawebgrid></TD>
											</TR>
										</TABLE>
									</FIELDSET>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
