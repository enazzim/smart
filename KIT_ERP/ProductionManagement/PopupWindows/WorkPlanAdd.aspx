<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v1.1, Version=1.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="WorkPlanAdd.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.PopupWindows.WorkPlanAdd" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WorkPlanAdd</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--
		function DoPost()
		{
			__doPostBack("LinkButton1","");
		}
		
		function DataRegister()
		{
			if(document.Form1.txtItemState.value == '양산품')
				Register()
			else
			{
				if(confirm("선택한 품목은 양산품이 아닙니다. 계속 등록하시겠습니까?") )	
				{
					this.Register();
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		
		function Register()
		{
			__doPostBack('LinkButton2');
			
		}
				
		--></SCRIPT>
	</HEAD>
	<body bgColor="#ffffff" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" style="POSITION: absolute; TOP: 0px; LEFT: 0px" cellSpacing="1" cellPadding="1"
				width="670" border="0">
				<TR>
					<TD vAlign="top" align="center">
						<P><FONT face="굴림">
								<TABLE id="Table1" style="BORDER-BOTTOM: darkgray thin outset; BORDER-LEFT: darkgray thin outset; HEIGHT: 289px; FONT-SIZE: x-small; BORDER-TOP: darkgray thin outset; BORDER-RIGHT: darkgray thin outset"
									cellSpacing="0" cellPadding="0" width="670" border="0">
									<TBODY>
										<TR>
											<TD align="left" bgColor="gainsboro" colSpan="8"><FONT face="굴림"></FONT><FONT face="굴림"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></FONT></TD>
										</TR>
										<TR>
											<TD style="BORDER-BOTTOM-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-TOP-COLOR: white; BORDER-TOP-STYLE: none; HEIGHT: 15px; FONT-SIZE: 9pt; BORDER-LEFT-STYLE: none"
												align="right" width="70" bgColor="gainsboro"><FONT face="굴림">공정&nbsp;</FONT></TD>
											<TD style="HEIGHT: 15px" width="100" bgColor="gainsboro"><asp:dropdownlist id="ddlProcessName" runat="server" BackColor="#E0E0E0" Width="97px"></asp:dropdownlist></TD>
											<TD style="HEIGHT: 15px; FONT-SIZE: 9pt" align="right" width="50" bgColor="gainsboro"><FONT face="굴림">작업장</FONT></TD>
											<TD style="HEIGHT: 15px" width="100" bgColor="gainsboro"><asp:dropdownlist id="ddlWCName" runat="server" BackColor="#E0E0E0" Width="97px"></asp:dropdownlist></TD>
											<TD style="HEIGHT: 15px; FONT-SIZE: 9pt" align="right" width="50" bgColor="#dcdcdc">작업량</TD>
											<TD style="HEIGHT: 15px" align="left" width="80" bgColor="#dcdcdc"><igtxt:webnumericedit id="wneWorkQuantity" runat="server" BackColor="#EEEEE9" Width="97px" ValueText="0"
													Height="20px" BorderStyle="Groove" BorderColor="Transparent"></igtxt:webnumericedit></TD>
											<TD style="HEIGHT: 15px; FONT-SIZE: 9pt" align="right" width="70" bgColor="#dcdcdc">LeadTime</TD>
											<TD style="HEIGHT: 15px" align="left" width="100" bgColor="gainsboro"><igtxt:webnumericedit id="wneLeadTime" runat="server" BackColor="#EEEEE9" Width="60px" ValueText="0" Height="20px"
													BorderStyle="Groove" BorderColor="Transparent" Enabled="False"></igtxt:webnumericedit></TD>
										</TR>
										<TR>
											<TD style="BORDER-BOTTOM-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-TOP-COLOR: white; BORDER-TOP-STYLE: none; HEIGHT: 16px; FONT-SIZE: 9pt; BORDER-LEFT-STYLE: none"
												align="right" width="70" bgColor="#dcdcdc">작업구분&nbsp;</TD>
											<TD style="HEIGHT: 16px" width="100" bgColor="#dcdcdc"><asp:dropdownlist id="Dropdownlist2" runat="server" BackColor="#E0E0E0" Width="97px" AutoPostBack="True">
													<asp:ListItem Value="자가">자가</asp:ListItem>
													<asp:ListItem Value="외주">외주</asp:ListItem>
												</asp:dropdownlist></TD>
											<TD style="HEIGHT: 16px; FONT-SIZE: 9pt" align="right" width="50" bgColor="#dcdcdc">작업일</TD>
											<TD style="HEIGHT: 16px" width="100" bgColor="#dcdcdc">
												<igsch:webdatechooser id="wdcWorkDate" runat="server" BackColor="#EEEEE9" Width="97px" Height="18px" BorderStyle="Solid"
													BorderColor="DimGray" Text=" " NullDateLabel=" " Font-Size="10pt">
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
											<TD style="HEIGHT: 16px; FONT-SIZE: 9pt" align="right" width="50" bgColor="#dcdcdc">납기일</TD>
											<TD style="HEIGHT: 16px" align="left" width="80" bgColor="#dcdcdc">
												<igsch:webdatechooser id="wdcDeliveryDate" runat="server" BackColor="#EEEEE9" Width="97px" Height="18px"
													BorderStyle="Solid" BorderColor="DimGray" Text=" " NullDateLabel=" " Font-Size="10pt">
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
											<TD style="HEIGHT: 16px" align="right" width="70" bgColor="#dcdcdc">품목상태&nbsp;</TD>
											<TD style="HEIGHT: 16px" align="left" width="100" bgColor="#dcdcdc"><FONT face="굴림"><INPUT id="txtItemState" style="BACKGROUND-COLOR: #eeeee9; WIDTH: 100px" readOnly name="txtItemState"
														runat="server"></FONT></TD>
										</TR>
										<TR>
											<TD style="HEIGHT: 3px" align="right" width="70" bgColor="lightgrey"><FONT face="굴림"></FONT></TD>
											<TD style="HEIGHT: 3px" width="100" bgColor="lightgrey"><asp:linkbutton id="LinkButton1" runat="server" Visible="False">LinkButton</asp:linkbutton></TD>
											<TD style="HEIGHT: 3px" align="right" width="50" bgColor="lightgrey"></TD>
											<TD style="HEIGHT: 3px" align="center" width="100" bgColor="lightgrey">
												<asp:linkbutton id="LinkButton2" runat="server" Visible="False">LinkButton</asp:linkbutton></TD>
											<TD style="HEIGHT: 3px" align="right" width="50" bgColor="#d3d3d3"></TD>
											<TD style="HEIGHT: 3px" align="right" width="80" bgColor="#d3d3d3"></TD>
											<TD style="HEIGHT: 3px" align="right" width="70" bgColor="#d3d3d3"></TD>
											<TD style="HEIGHT: 3px" align="right" width="200" bgColor="lightgrey"></TD>
										</TR>
										<TR>
											<TD style="HEIGHT: 21px" align="right" width="70" bgColor="lightgrey">&nbsp;</TD>
											<TD style="HEIGHT: 21px" width="100" bgColor="lightgrey"><FONT face="굴림"></FONT></TD>
											<TD style="HEIGHT: 21px" align="right" width="50" bgColor="lightgrey">&nbsp;</TD>
											<TD align="center" width="100" bgColor="lightgrey"><FONT face="굴림"></FONT></TD>
											<TD style="HEIGHT: 21px" align="right" width="50" bgColor="#d3d3d3"></TD>
											<TD style="HEIGHT: 21px" align="right" width="80" bgColor="#d3d3d3"></TD>
											<TD style="HEIGHT: 21px" align="right" width="70" bgColor="#d3d3d3"></TD>
											<TD style="HEIGHT: 21px" align="right" width="200" bgColor="lightgrey">
												<asp:button id="Button2" runat="server" Width="60px" Height="20px" Font-Size="9pt" Text="초기화"></asp:button>&nbsp;<INPUT id="btAdd" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:DataRegister()"
													type="button" value="추  가" name="btnReset" runat="server">&nbsp;</TD>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 28px" align="center" width="70" bgColor="#dcdcdc"></TD>
					<TD style="HEIGHT: 28px" width="100" bgColor="#dcdcdc"></TD>
					<TD style="HEIGHT: 28px" align="right" width="50" bgColor="#dcdcdc"></TD>
					<TD style="HEIGHT: 28px" align="right" width="100" bgColor="#dcdcdc"></TD>
					<TD style="HEIGHT: 28px" align="right" width="50" bgColor="#dcdcdc"></TD>
					<TD style="HEIGHT: 28px" align="right" width="80" bgColor="#dcdcdc"></TD>
					<TD style="HEIGHT: 28px" align="right" width="70" bgColor="#dcdcdc"></TD>
					<TD style="HEIGHT: 28px" align="right" width="100" bgColor="#dcdcdc"></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 13px" align="right" width="70" bgColor="#dcdcdc"></TD>
					<TD style="HEIGHT: 13px" width="100" bgColor="#dcdcdc"></TD>
					<TD style="HEIGHT: 13px" align="right" width="50" bgColor="#dcdcdc"><FONT face="굴림"></FONT></TD>
					<TD style="HEIGHT: 13px" width="100" bgColor="#dcdcdc">
						<P>&nbsp;</P>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
					</TD>
					<TD style="HEIGHT: 13px" align="right" width="50" bgColor="#dcdcdc"></TD>
					<TD style="HEIGHT: 13px" vAlign="bottom" align="center" width="80" bgColor="#dcdcdc"></TD>
					<TD style="HEIGHT: 13px" vAlign="bottom" align="center" width="70" bgColor="#dcdcdc"></TD>
					<TD style="HEIGHT: 13px" vAlign="bottom" align="center" width="100" bgColor="#dcdcdc"><FONT face="굴림"></FONT></TD>
				</TR>
			</TABLE>
			</FONT></P></TD></TR></TBODY></TABLE></form>
	</body>
</HTML>
