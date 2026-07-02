<%@ Page language="c#" Codebehind="Login.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Login" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>안녕하세요!</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body onload="document.Form1.TextBox1.focus();">
		<form id="Form1" method="post" runat="server">
			<table cellpadding="0" cellspacing="0" width="100%" height="100%">
				<tr>
					<td height="100"></td>
				</tr>
				<tr>
					<td valign="top">
						<table cellpadding="0" cellspacing="0" width="100%" height="80%">
							<tr>
								<td align="center">
									<!--로그인 이미지-->
									<TABLE WIDTH="720" BORDER="0" CELLPADDING="0" CELLSPACING="0">
										<TR>
											<TD COLSPAN="5">
												<IMG SRC="images/smartlogin_01.gif" WIDTH="720" HEIGHT="182" ALT=""></TD>
											<TD>
												<IMG SRC="images/Spacer.gif" WIDTH="1" HEIGHT="182" ALT=""></TD>
										</TR>
										<TR>
											<TD COLSPAN="3">
												<IMG SRC="images/smartlogin_02.gif" WIDTH="528" HEIGHT="18" ALT=""></TD>
											<TD ROWSPAN="4">
												<asp:ImageButton id="ImageButton1" runat="server" Width="65px" Height="65px" ImageUrl="images/smartlogin_03.gif"></asp:ImageButton></TD>
											<TD ROWSPAN="6">
												<IMG SRC="images/smartlogin_04.gif" WIDTH="127" HEIGHT="231" ALT=""></TD>
											<TD>
												<IMG SRC="images/Spacer.gif" WIDTH="1" HEIGHT="18" ALT=""></TD>
										</TR>
										<TR>
											<TD ROWSPAN="5">
												<IMG SRC="images/smartlogin_05.gif" WIDTH="386" HEIGHT="213" ALT=""></TD>
											<TD>
												<asp:TextBox id="TextBox1" runat="server" Width="125px" Height="20px" BorderColor="#E0E0E0" BorderStyle="Solid"
													BorderWidth="1px"></asp:TextBox></TD>
											<TD ROWSPAN="5">
												<IMG SRC="images/smartlogin_07.gif" WIDTH="16" HEIGHT="213" ALT=""></TD>
											<TD>
												<IMG SRC="images/Spacer.gif" WIDTH="1" HEIGHT="22" ALT=""></TD>
										</TR>
										<TR>
											<TD>
												<IMG SRC="images/smartlogin_08.gif" WIDTH="126" HEIGHT="11" ALT=""></TD>
											<TD>
												<IMG SRC="images/Spacer.gif" WIDTH="1" HEIGHT="11" ALT=""></TD>
										</TR>
										<TR>
											<TD ROWSPAN="2"><FONT face="굴림">
													<asp:TextBox id="TextBox2" runat="server" Width="125px" Height="20px" BorderColor="#E0E0E0" BorderStyle="Solid"
														BorderWidth="1px" TextMode="Password"></asp:TextBox></FONT></TD>
											<TD>
												<IMG SRC="images/Spacer.gif" WIDTH="1" HEIGHT="14" ALT=""></TD>
										</TR>
										<TR>
											<TD ROWSPAN="2">
												<IMG SRC="images/smartlogin_10.gif" WIDTH="65" HEIGHT="166" ALT=""></TD>
											<TD>
												<IMG SRC="images/Spacer.gif" WIDTH="1" HEIGHT="8" ALT=""></TD>
										</TR>
										<TR>
											<TD>
												<IMG SRC="images/smartlogin_11.gif" WIDTH="126" HEIGHT="158" ALT=""></TD>
											<TD>
												<IMG SRC="images/Spacer.gif" WIDTH="1" HEIGHT="158" ALT=""></TD>
										</TR>
									</TABLE>
									<!--로그인 이미지-->
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
