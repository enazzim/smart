<%@ Page language="c#" Codebehind="WCProductionCalendar.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.PopupWindows.WCProductionCalendar" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WCProductionCalendar</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<FONT face="±¼¸²" size="2">
				<FIELDSET style="Z-INDEX: 101; LEFT: 10px; WIDTH: 600px; POSITION: absolute; TOP: 40px; HEIGHT: 356px"
					align="left"><LEGEND>[»ý»ê´Þ·Â º¯°æ]</LEGEND>
					<TABLE id="table1" style="WIDTH: 600px; HEIGHT: 322px" width="552" cellSpacing="0" cellPadding="0">
						<TR>
							<TD style="WIDTH: 370px" width="370">
								<asp:Calendar id="Calendar1" runat="server" BackColor="White" ForeColor="Black" Font-Size="9pt"
									Font-Names="Verdana" BorderColor="Black" BorderStyle="Solid" NextPrevFormat="ShortMonth" CellSpacing="1"
									Height="300px" Width="350px">
									<TodayDayStyle ForeColor="White" BackColor="#999999"></TodayDayStyle>
									<DayStyle BackColor="#CCCCCC"></DayStyle>
									<NextPrevStyle Font-Size="8pt" Font-Bold="True" ForeColor="White"></NextPrevStyle>
									<DayHeaderStyle Font-Size="8pt" Font-Bold="True" Height="8pt" ForeColor="#333333"></DayHeaderStyle>
									<SelectedDayStyle ForeColor="White" BackColor="#333399"></SelectedDayStyle>
									<TitleStyle Font-Size="12pt" Font-Bold="True" Height="12pt" ForeColor="White" BackColor="#333399"></TitleStyle>
									<OtherMonthDayStyle ForeColor="#999999"></OtherMonthDayStyle>
								</asp:Calendar></TD>
							<TD width="330" align="right">
								<TABLE id="table2" style="WIDTH: 233px; HEIGHT: 304px" width="233" cellSpacing="0" cellPadding="0">
									<TR>
										<TD style="FONT-SIZE: 20px; COLOR: #ff0033; HEIGHT: 44px" align="center" colspan="4">
											<P><FONT face="±¼¸²" size="3">
													<asp:Label id="year" tabIndex="3" runat="server" Width="32px"></asp:Label>³â
													<asp:Label id="mon" tabIndex="3" runat="server" Width="10px"></asp:Label>
													¿ù
													<asp:Label id="day" tabIndex="3" runat="server" Width="32px"></asp:Label>ÀÏ</FONT></P>
										</TD>
									</TR>
									<TR>
										<TD style="HEIGHT: 7px" align="left" colspan="4"></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 32px; HEIGHT: 7px; BACKGROUND-COLOR: green" align="left"></TD>
										<TD style="WIDTH: 78px; HEIGHT: 7px" align="left">
											<asp:RadioButton id="RadioButton1" runat="server" Text="10½Ã°£" Font-Size="X-Small" GroupName="CalendarGroup"
												AutoPostBack="True"></asp:RadioButton></TD>
										<TD style="WIDTH: 36px; HEIGHT: 7px; BACKGROUND-COLOR: olive" align="left"></TD>
										<TD style="HEIGHT: 7px" align="left">
											<asp:RadioButton id="RadioButton2" runat="server" Text="8½Ã°£" Font-Size="X-Small" GroupName="CalendarGroup"
												AutoPostBack="True"></asp:RadioButton></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 32px; HEIGHT: 14px; BACKGROUND-COLOR: chartreuse" align="left"></TD>
										<TD style="WIDTH: 78px; HEIGHT: 7px" align="left">
											<asp:RadioButton id="RadioButton3" runat="server" Text="6½Ã°£" Font-Size="X-Small" GroupName="CalendarGroup"
												AutoPostBack="True"></asp:RadioButton></TD>
										<TD style="WIDTH: 36px; HEIGHT: 7px; BACKGROUND-COLOR: blue" align="left"></TD>
										<TD style="HEIGHT: 7px" align="left">
											<asp:RadioButton id="RadioButton4" runat="server" Text="4½Ã°£" Font-Size="X-Small" GroupName="CalendarGroup"
												AutoPostBack="True"></asp:RadioButton></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 32px; HEIGHT: 10px; BACKGROUND-COLOR: darkorange" align="left"></TD>
										<TD style="WIDTH: 78px; HEIGHT: 10px" align="left">
											<asp:RadioButton id="RadioButton5" runat="server" Text="±âÅ¸" Font-Size="X-Small" GroupName="CalendarGroup"
												AutoPostBack="True"></asp:RadioButton></TD>
										<TD style="WIDTH: 36px; HEIGHT: 10px; BACKGROUND-COLOR: red" align="left">
										</TD>
										<TD style="HEIGHT: 10px" align="left">
											<asp:RadioButton id="RadioButton6" runat="server" Text="ÈÞÀÏ" Font-Size="X-Small" GroupName="CalendarGroup"
												AutoPostBack="True"></asp:RadioButton></TD>
									</TR>
									<TR>
										<TD style="HEIGHT: 35px" align="left" colspan="4"><FONT face="±¼¸²"></FONT>
											<asp:TextBox id="tb_Time" runat="server" style="TEXT-ALIGN: right" Width="144px" BackColor="#EEEEE9">0</asp:TextBox><FONT face="±¼¸²" style="FONT-SIZE: x-small">ºÐ</FONT>
										</TD>
									</TR>
									<TR>
										<TD style="HEIGHT: 96px" align="center" colspan="4"><P><FONT face="±¼¸²"> </FONT>
												<asp:TextBox id="tb_Content" runat="server" Width="212px" TextMode="MultiLine" Height="86px"
													BackColor="#EEEEE9"></asp:TextBox></P>
										</TD>
									</TR>
									<TR>
										<TD align="center" colspan="4" vAlign="top">
											<asp:Button id="bt_Update" runat="server" Height="20px" Width="60px" Text="¼ö  Á¤"></asp:Button><FONT face="±¼¸²">&nbsp;</FONT>
										</TD>
									</TR>
								</TABLE>
								<asp:Button id="Button1" runat="server" Width="60px" Height="20px" Text="È®  ÀÎ"></asp:Button>
							</TD>
						</TR>
					</TABLE>
				</FIELDSET>
				<TABLE id="Table3" style="Z-INDEX: 102; LEFT: 8px; WIDTH: 600px; POSITION: absolute; TOP: 8px; HEIGHT: 20px"
					width="600" cellSpacing="0" cellPadding="0">
					<TR>
						<TD style="WIDTH: 52px" valign="middle" align="right"><FONT face="±¼¸²" size="2" style="FONT-SIZE: x-small">ÀÛ¾÷Àå 
								: </FONT>
						</TD>
						<TD style="WIDTH: 477px">
							<asp:DropDownList id="dl_WCName" runat="server" BackColor="#EEEEE9"></asp:DropDownList>
							<asp:DropDownList id="dl_Year" runat="server" Width="80px" BackColor="#EEEEE9">
								<asp:ListItem Value="2004">2004</asp:ListItem>
								<asp:ListItem Value="2005">2005</asp:ListItem>
								<asp:ListItem Value="2006">2006</asp:ListItem>
								<asp:ListItem Value="2007">2007</asp:ListItem>
								<asp:ListItem Value="2008">2008</asp:ListItem>
								<asp:ListItem Value="2009">2009</asp:ListItem>
								<asp:ListItem Value="2010">2010</asp:ListItem>
							</asp:DropDownList><FONT face="±¼¸²" style="FONT-SIZE: x-small">³â </FONT>
							<asp:DropDownList id="dl_Month" runat="server" Width="50px" BackColor="#EEEEE9">
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
							</asp:DropDownList><FONT face="±¼¸²" style="FONT-SIZE: x-small">¿ù&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								<asp:Button id="bt_Select" runat="server" Height="20px" Width="60px" Text="¼± ÅÃ"></asp:Button></FONT></TD>
						<TD align="right">
							<asp:Label id="lb_Index" runat="server" Width="50px" Visible="False">0</asp:Label></TD>
						<TD><FONT face="±¼¸²"></FONT></TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
