<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="BuyingUnitCodeInfo.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.BuyingUnitCodeInfo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BuyingUnitCostInfo</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
		function OK(text)
		{
			if(confirm(text + "하시겠습니까?"))
				return true;
			else
				return false;
		}
		function UltraWebGrid1_AfterSelectChangeHandler(gridName, id)
		{
			
			document.BuyingUnitCodeInfo.wc_Company.value = document.getElementById(id).children[4].value;			
			document.BuyingUnitCodeInfo.tb_OrderRate.value = document.getElementById(id).children[7].value;			
		}
		
			
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="BuyingUnitCodeInfo" method="post" runat="server">
			<table style="WIDTH: 832px; HEIGHT: 26px">
				<TR>
					<td align="right" width="100"><asp:label id="lb_ItemChoice" runat="server" Font-Size="9.5pt" Width="72px" Height="2px" ForeColor="Black">품목선택</asp:label></td>
					<td align="left" width="600" colSpan="6"><DROPDOWNLAYOUT DropdownWidth="325px" BorderCollapse="Separate" RowHeightDefault="20px" HeaderClickAction="Select"
							DropdownHeight="130px"><igcmbo:webcombo id="wc_ItemChoice" runat="server" Width="90px" Height="22px" ForeColor="Black" SelForeColor="White"
								SelectedIndex="-1" SelBackColor="DarkBlue" BorderColor="LightGray" BorderWidth="1px" BackColor="White" Version="3.00"
								BorderStyle="Solid">
								<DropDownLayout DropdownWidth="325px" BorderCollapse="Separate" RowHeightDefault="20px" HeaderClickAction="Select"
									DropdownHeight="130px">
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
							</igcmbo:webcombo>
							<ROWSTYLE BorderColor="Gray" BorderWidth="1px" BackColor="White" BorderStyle="Solid">
								<BORDERDETAILS WidthTop="0px" WidthLeft="0px"></BORDERDETAILS>
							</ROWSTYLE>
							<SELECTEDROWSTYLE ForeColor="White" BackColor="DarkBlue"></SELECTEDROWSTYLE>
							<HEADERSTYLE BackColor="LightGray" BorderStyle="Solid">
								<BORDERDETAILS WidthTop="1px" WidthLeft="1px" ColorLeft="White" ColorTop="White"></BORDERDETAILS>
							</HEADERSTYLE>
							<FRAMESTYLE Font-Size="10pt" Width="100%" Height="100%" BorderWidth="2px" BackColor="Silver"
								BorderStyle="Ridge" Font-Names="Verdana" Cursor="Default"></FRAMESTYLE>
						</DROPDOWNLAYOUT><EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS><DROPDOWNLAYOUT DropdownWidth="325px" BorderCollapse="Separate" RowHeightDefault="20px" HeaderClickAction="Select"
							DropdownHeight="130px"><ROWSTYLE BorderColor="Gray" BorderWidth="1px" BackColor="White" BorderStyle="Solid"><BORDERDETAILS WidthTop="0px" WidthLeft="0px"></BORDERDETAILS>
							</ROWSTYLE>
							<SELECTEDROWSTYLE ForeColor="White" BackColor="DarkBlue"></SELECTEDROWSTYLE>
							<HEADERSTYLE BackColor="LightGray" BorderStyle="Solid">
								<BORDERDETAILS WidthTop="1px" WidthLeft="1px" ColorLeft="White" ColorTop="White"></BORDERDETAILS>
							</HEADERSTYLE>
							<FRAMESTYLE Font-Size="10pt" Width="100%" Height="100%" BorderWidth="2px" BackColor="Silver"
								BorderStyle="Ridge" Font-Names="Verdana" Cursor="Default"></FRAMESTYLE>
						</DROPDOWNLAYOUT><EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS><ROWSTYLE BorderColor="Gray" BorderWidth="1px" BackColor="White" BorderStyle="Solid"><BORDERDETAILS WidthTop="0px" WidthLeft="0px"></BORDERDETAILS>
						</ROWSTYLE><SELECTEDROWSTYLE ForeColor="White" BackColor="DarkBlue"></SELECTEDROWSTYLE><HEADERSTYLE BackColor="LightGray" BorderStyle="Solid"><BORDERDETAILS WidthTop="1px" WidthLeft="1px" ColorLeft="White" ColorTop="White"></BORDERDETAILS>
						</HEADERSTYLE><FRAMESTYLE Font-Size="10pt" Width="100%" Height="100%" BorderWidth="2px" BackColor="Silver"
							BorderStyle="Ridge" Font-Names="Verdana" Cursor="Default"></FRAMESTYLE></DROPDOWNLAYOUT><EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS></td>
					<td align="right" width="100"><asp:linkbutton id="LinkButton1" runat="server" Font-Size="10pt">단가정보보기</asp:linkbutton></td>
				</TR>
			</table>
			<FIELDSET style="LEFT: 10px; WIDTH: 816px; TOP: 8px; HEIGHT: 480px" align="left"><legend>[ 
					입 력 ]</legend>
				<table style="WIDTH: 832px; HEIGHT: 200px">
					<tr>
						<td align="right" width="100">&nbsp;
							<asp:label id="lb_ItemNum" runat="server" Font-Size="9.5pt" Width="59px" Height="2px" ForeColor="Red">*품목번호</asp:label></td>
						<td align="left" width="100"><asp:textbox id="tb_ItemNum" runat="server" Width="90px" Height="20px" BorderStyle="Groove" ReadOnly="True"></asp:textbox></td>
						<td align="right" width="100"><asp:label id="lb_ItemDrawNum" runat="server" Font-Size="9.5pt" Width="52px" Height="2px" ForeColor="Red">도면번호</asp:label></td>
						<td width="100"><asp:textbox id="tb_ItemDrawNum" runat="server" Width="90px" Height="20px" BorderStyle="Groove"
								ReadOnly="True"></asp:textbox></td>
						<td align="right" width="100"><asp:label id="lb_ItemName" runat="server" Font-Size="9.5pt" Width="40px" Height="2px" ForeColor="Red">품목명</asp:label></td>
						<td width="100"><asp:textbox id="tb_ItemName" runat="server" Width="90px" Height="20px" BorderStyle="Groove"
								ReadOnly="True"></asp:textbox></td>
						<td style="WIDTH: 97px" align="right" width="97"><asp:label id="lb_PropertyClassification" runat="server" Font-Size="9.5pt" Width="52px" Height="2px"
								ForeColor="Red">자산분류</asp:label></td>
						<td width="100"><asp:textbox id="tb_PropertyClassification" runat="server" Width="90px" Height="20px" BorderStyle="Groove"
								ReadOnly="True"></asp:textbox></td>
					</tr>
					<tr>
						<td align="right" width="100"><asp:label id="lb_Unit" runat="server" Font-Size="9.5pt" Width="26px" Height="2px" ForeColor="Red">단위</asp:label></td>
						<td align="left" width="100"><asp:textbox id="tb_Unit" runat="server" Width="90px" Height="20px" BorderStyle="Groove" ReadOnly="True"></asp:textbox></td>
						<td align="right" width="100"><asp:label id="lb_Standard" runat="server" Font-Size="9.5pt" Width="26px" Height="2px" ForeColor="Red">규격</asp:label></td>
						<td width="100"><asp:textbox id="tb_Standard" runat="server" Width="90px" Height="20px" BorderStyle="Groove"
								ReadOnly="True"></asp:textbox></td>
						<td width="100"><FONT face="굴림"><asp:label id="lb_ItemIndex" runat="server" Font-Size="10pt" Visible="False"></asp:label></FONT></td>
						<td align="center" width="100"><FONT face="굴림"></FONT></td>
						<td style="WIDTH: 97px" align="center" width="97"><FONT face="굴림">&nbsp;</FONT></td>
						<td align="left" width="100"><asp:button id="bt_BuyingUnitCost" runat="server" Width="90px" Height="20px" Text="구매단가보기"></asp:button></td>
					</tr>
					<tr>
						<td style="HEIGHT: 157px" vAlign="top" align="center" width="800" colSpan="8" height="157"></FONT><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="200px">
								<DisplayLayout StationaryMargins="Header" AllowSortingDefault="OnClient" RowHeightDefault="20px"
									Version="3.00" AllowColumnMovingDefault="OnServer" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
									Name="UltraWebGrid1" TableLayout="Fixed">
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
									<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</HeaderStyleDefault>
									<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
										Height="200px"></FrameStyle>
									<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</FooterStyleDefault>
									<ClientSideEvents AfterSelectChangeHandler="UltraWebGrid1_AfterSelectChangeHandler"></ClientSideEvents>
									<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
									<RowStyleDefault BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid">
										<Padding Left="3px"></Padding>
										<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
									</RowStyleDefault>
								</DisplayLayout>
								<Bands>
									<igtbl:UltraGridBand></igtbl:UltraGridBand>
								</Bands>
							</igtbl:ultrawebgrid><ADDNEWBOX>
								<STYLE BorderWidth="1px" BackColor="LightGray" BorderStyle="Solid"></STYLE>
							</ADDNEWBOX><PAGER>
								<STYLE BorderWidth="1px" BackColor="LightGray" BorderStyle="Solid"></STYLE>
							</PAGER><HEADERSTYLEDEFAULT BackColor="LightGray" BorderStyle="Solid"><BORDERDETAILS WidthTop="1px" WidthLeft="1px" ColorLeft="White" ColorTop="White"></BORDERDETAILS>
							</HEADERSTYLEDEFAULT><FRAMESTYLE Font-Size="8pt" Width="800px" Height="150px" BorderWidth="1px" BorderStyle="Solid"
								Font-Names="Verdana"></FRAMESTYLE><FOOTERSTYLEDEFAULT BorderWidth="1px" BackColor="LightGray" BorderStyle="Solid"><BORDERDETAILS WidthTop="1px" WidthLeft="1px" ColorLeft="White" ColorTop="White"></BORDERDETAILS>
							</FOOTERSTYLEDEFAULT><EDITCELLSTYLEDEFAULT BorderWidth="0px" BorderStyle="None"></EDITCELLSTYLEDEFAULT><ROWSTYLEDEFAULT BorderColor="Gray" BorderWidth="1px" BorderStyle="Solid"><PADDING Left="3px"></PADDING>
								<BORDERDETAILS WidthTop="0px" WidthLeft="0px"></BORDERDETAILS>
							</ROWSTYLEDEFAULT></DISPLAYLAYOUT><BANDS></BANDS></td>
					</tr>
					<tr>
						<td align="right" width="100"><asp:label id="lb_Company" runat="server" Font-Size="9.5pt" Width="59px" Height="2px" ForeColor="Red">*거래처</asp:label>&nbsp;</td>
						<td align="left" width="100"><FONT face="굴림"><igcmbo:webcombo id="wc_Company" runat="server" Width="90px" Height="23px" ForeColor="Black" SelForeColor="White"
									SelectedIndex="-1" SelBackColor="DarkBlue" BorderColor="LightGray" BorderWidth="1px" BackColor="White" Version="3.00" BorderStyle="Solid"
									Editable="True">
									<DropDownLayout DropdownWidth="325px" BorderCollapse="Separate" AllowColSizing="Free" RowHeightDefault="20px"
										HeaderClickAction="Select" DropdownHeight="130px">
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
								</igcmbo:webcombo></FONT><DROPDOWNLAYOUT DropdownWidth="325px" BorderCollapse="Separate" RowHeightDefault="20px" HeaderClickAction="Select"
								DropdownHeight="130px"><ROWSTYLE BorderColor="Gray" BorderWidth="1px" BackColor="White" BorderStyle="Solid"><BORDERDETAILS WidthTop="0px" WidthLeft="0px"></BORDERDETAILS>
								</ROWSTYLE>
								<SELECTEDROWSTYLE ForeColor="White" BackColor="DarkBlue"></SELECTEDROWSTYLE>
								<HEADERSTYLE BackColor="LightGray" BorderStyle="Solid">
									<BORDERDETAILS WidthTop="1px" WidthLeft="1px" ColorLeft="White" ColorTop="White"></BORDERDETAILS>
								</HEADERSTYLE>
								<FRAMESTYLE Font-Size="10pt" Width="100%" Height="100%" BorderWidth="2px" BackColor="Silver"
									BorderStyle="Ridge" Font-Names="Verdana" Cursor="Default"></FRAMESTYLE>
							</DROPDOWNLAYOUT><EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS></td>
						<td align="right" width="100"><asp:label id="lb_OrderRate" runat="server" Font-Size="9.5pt" Width="59px" Height="2px" ForeColor="Red">*발주비율</asp:label></td>
						<td align="left" width="100"><FONT face="굴림" size="3"><asp:textbox id="tb_OrderRate" runat="server" Width="70px" Height="23px"></asp:textbox>%</FONT></td>
						<TD align="right" width="100"><FONT face="굴림"><asp:label id="lb_StandardUnitCost" runat="server" Font-Size="9.5pt" Width="62px" Height="2px"
									ForeColor="Red">*기준단가</asp:label></FONT></TD>
						<TD width="100"><FONT face="굴림"><asp:textbox id="tb_StandardUnitCost" runat="server" Width="70px" Height="23px"></asp:textbox>원</FONT></TD>
						<TD style="WIDTH: 97px" align="right" width="97"><FONT face="굴림"><asp:label id="lb_DiscountUnitCost" runat="server" Font-Size="9.5pt" Width="59px" Height="2px"
									ForeColor="Black">할인단가</asp:label></FONT></TD>
						<TD width="100"><DROPDOWNLAYOUT DropdownWidth="325px" BorderCollapse="Separate" RowHeightDefault="20px" HeaderClickAction="Select"
								DropdownHeight="130px"><FONT face="굴림"><asp:textbox id="tb_DiscountUnitCost" runat="server" Width="70px" Height="23px"></asp:textbox>원</FONT><ROWSTYLE BorderColor="Gray" BorderWidth="1px" BackColor="White" BorderStyle="Solid"><BORDERDETAILS WidthTop="0px" WidthLeft="0px"></BORDERDETAILS>
								</ROWSTYLE>
								<SELECTEDROWSTYLE ForeColor="White" BackColor="DarkBlue"></SELECTEDROWSTYLE>
								<HEADERSTYLE BackColor="LightGray" BorderStyle="Solid">
									<BORDERDETAILS WidthTop="1px" WidthLeft="1px" ColorLeft="White" ColorTop="White"></BORDERDETAILS>
								</HEADERSTYLE>
								<FRAMESTYLE Font-Size="10pt" Width="100%" Height="100%" BorderWidth="2px" BackColor="Silver"
									BorderStyle="Ridge" Font-Names="Verdana" Cursor="Default"></FRAMESTYLE>
							</DROPDOWNLAYOUT><EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS></TD>
					</tr>
					<tr>
						<td align="right" width="100"><asp:label id="lb_BeginDate" runat="server" Font-Size="9.5pt" Width="75px" Height="2px" ForeColor="Red">*적용시작일</asp:label></td>
						<td align="left" width="100"><FONT face="굴림"><igsch:webdatechooser id="wdc_BeginDate" runat="server" Width="90px" Height="20px" Text="Null" NullDateLabel=" "
									MaxDate="2079-06-06">
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
								</igsch:webdatechooser><CALENDARLAYOUT FooterFormat="Today: {0:d}"></CALENDARLAYOUT><EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS></FONT></td>
						<td align="right" width="100"><asp:label id="lb_EndDate" runat="server" Font-Size="9.5pt" Width="75px" Height="2px" ForeColor="Black">적용종료일</asp:label></td>
						<td width="100"><FONT face="굴림"><igsch:webdatechooser id="wdc_EndDate" runat="server" Width="90px" Height="20px" Text="Null" NullDateLabel=" "
									MaxDate="2079-06-06">
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
								</igsch:webdatechooser><CALENDARLAYOUT FooterFormat="Today: {0:d}"></CALENDARLAYOUT><EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS></FONT></td>
						<td align="right" width="100">&nbsp;</td>
						<td align="left" width="100"><FONT face="굴림"></FONT></td>
						<td style="WIDTH: 97px" width="97"><FONT face="굴림"></FONT></td>
						<td width="100"><FONT face="굴림"></FONT></td>
					</tr>
					<tr>
						<td align="center" width="100"><asp:button id="bt_RelationView" runat="server" Width="60px" Height="20px" Text="참고보기"></asp:button></td>
						<td align="right" width="100"><asp:label id="lb_Index" runat="server" Font-Size="10pt" Width="10px" Visible="False"></asp:label></td>
						<td width="100"><FONT face="굴림"></FONT></td>
						<td width="100"><FONT face="굴림"></FONT></td>
						<td width="100"></td>
						<td align="right" width="300" colSpan="3"><asp:button id="bt_Clear" runat="server" Width="60px" Height="20px" Text="초기화"></asp:button><FONT face="굴림">&nbsp;</FONT><asp:button id="bt_Update" runat="server" Width="60px" Height="20px" Text="수  정"></asp:button><FONT face="굴림">&nbsp;</FONT><asp:button id="bt_Delete" runat="server" Width="60px" Height="20px" Text="삭  제"></asp:button><FONT face="굴림">&nbsp;</FONT><asp:button id="bt_Registration" runat="server" Width="60px" Height="20px" Text="등  록"></asp:button></td>
					</tr>
				</table>
				<igtbl:ultrawebgrid id="UltraWebGrid2" runat="server" Width="800px" Height="150px">
					<DisplayLayout RowHeightDefault="20px" Version="3.00" BorderCollapseDefault="Separate" Name="UltraWebGrid2">
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
						<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
							<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
						</HeaderStyleDefault>
						<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
							Height="150px"></FrameStyle>
						<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
							<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
						</FooterStyleDefault>
						<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
						<RowStyleDefault BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid">
							<Padding Left="3px"></Padding>
							<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
						</RowStyleDefault>
					</DisplayLayout>
					<Bands>
						<igtbl:UltraGridBand></igtbl:UltraGridBand>
					</Bands>
				</igtbl:ultrawebgrid>
		</form>
		</FIELDSET>
	</body>
</HTML>
