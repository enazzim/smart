<%@ Page language="c#" Codebehind="HistoryAdjustment.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.SystemInfoManagement.HistoryAdjustment" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>HistoryAdjustment</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<FONT face="굴림">
				<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; WIDTH: 807px; POSITION: absolute; TOP: 10px; HEIGHT: 368px"
					cellSpacing="0" cellPadding="0" width="807" border="0">
					<TR>
						<TD width="20"></TD>
						<TD vAlign="top" align="center" width="780">
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 0pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 320px">
								<P><LEGEND align="top">
										<asp:Label id="Label2" Runat="server" Font-Size="10pt"> [주의사항]</asp:Label></LEGEND><BR>
									<BR>
									<BR>
									<BR>
								</P>
								<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" border="0" style="WIDTH: 800px; HEIGHT: 272px">
									<TR>
										<TD background="../images/system-bg3.gif" style="HEIGHT: 184px"></TD>
									</TR>
									<TR>
										<TD vAlign="bottom" bgColor="#f7f6f6">
											<asp:Label id="Label3" runat="server" Font-Size="10pt" ForeColor="Red" Height="120px" BackColor="#F7F6F6">※ 원장 정리를 하기 전 반드시 백업을 받으십시오.<br>&nbsp;&nbsp;&nbsp;&nbsp;원장을 정리한 경우 해당 기간내의 자료들의 대해서는 각종 통계 자료치를 볼 수 없습니다.<br>&nbsp;&nbsp;&nbsp;&nbsp;원장을 정리한 후 정리된 내용의 자료를 보고 싶을 경우 현재 상태를 백업 받고<br>&nbsp;&nbsp;&nbsp;&nbsp;정리한 시점의 백업 받은 자료를 복원 후 해당 내용을 확인하고<br>&nbsp;&nbsp;&nbsp;&nbsp;다시 현재 상태로 복원하십시오.<br><br><b>
													☞ 사용자 부 주의로 인한 시스템 오류는 책임을 지지 않습니다.</b></asp:Label></TD>
									</TR>
								</TABLE>
							</FIELDSET>
							<br>
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 0pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top">
									<asp:Label id="Label1" Font-Size="10pt" Runat="server">[원장정리]</asp:Label></LEGEND>
								<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" border="0" height="50" align="center">
									<TR height="20">
										<TD vAlign="middle">
											<asp:DropDownList id="DropDownList1" runat="server" Width="150px" Height="20px" AutoPostBack="True"
												BackColor="#EEEEE9"></asp:DropDownList></TD>
										<TD vAlign="middle">
											<asp:DropDownList id="DropDownList2" runat="server" Width="150px" Height="20px" Enabled="False" BackColor="#EEEEE9"></asp:DropDownList></TD>
										<TD vAlign="middle" align="right">
											<igsch:WebDateChooser id="WebDateChooser1" runat="server" Font-Size="10pt" Width="100px" Height="18px"
												Text="날짜 선택" NullDateLabel=" " BackColor="#EEEEE9">
												<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
													ShowTitle="False" ShowFooter="False">
													<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
													<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
													<DropDownStyle BackColor="White"></DropDownStyle>
													<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
												</CalendarLayout>
												<DropDownStyle BorderStyle="Inset"></DropDownStyle>
												<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
												<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
											</igsch:WebDateChooser></TD>
										<TD style="WIDTH: 3px" vAlign="middle">
											~</TD>
										<TD style="WIDTH: 75px" vAlign="middle">
											<igsch:WebDateChooser id="Webdatechooser2" runat="server" Font-Size="10pt" Width="100px" Height="18px"
												Text="날짜 선택" NullDateLabel=" " BackColor="#EEEEE9">
												<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
													ShowTitle="False" ShowFooter="False">
													<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
													<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
													<DropDownStyle BackColor="White"></DropDownStyle>
													<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
												</CalendarLayout>
												<DropDownStyle BorderStyle="Inset"></DropDownStyle>
												<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
												<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
											</igsch:WebDateChooser></TD>
										<TD vAlign="middle">
											<asp:Button id="Button1" runat="server" Width="65px" Height="20px" Text="정   리"></asp:Button></TD>
									</TR>
								</TABLE>
							</FIELDSET>
						</TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
