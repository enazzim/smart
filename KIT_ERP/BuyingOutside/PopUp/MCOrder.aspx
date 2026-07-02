<%@ Page language="c#" Codebehind="MCOrder.aspx.cs" AutoEventWireup="false" Inherits="hanaro.BuyingOutside.PopUp.MCOrder" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>MCOrder</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table style="FONT-SIZE: 9pt; LEFT: 5px; POSITION: absolute; TOP: 50px" cellSpacing="0"
				cellPadding="0" width="700" border="0">
				<tr>
					<td style="BORDER-BOTTOM: black 1px solid" align="center" width="400">
						<table style="FONT-SIZE: 9pt" cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td style="FONT-SIZE: 25pt" width="400" height="60"><div class="style1" align="center"><U>Ãâ&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
											¹®&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;Áõ</U></div>
								</td>
							</tr>
							<tr>
								<td>&nbsp;¹ß Çà ÀÏ :
									<asp:TextBox id="lb_Year" runat="server" Height="18px" BorderStyle="None" Width="34px" style="TEXT-ALIGN: right"></asp:TextBox>³â&nbsp;
									<asp:TextBox id="lb_Mon" runat="server" Height="18px" BorderStyle="None" Width="34px" style="TEXT-ALIGN: right"></asp:TextBox>¿ù
									<asp:TextBox id="lb_Day" runat="server" Height="18px" BorderStyle="None" Width="34px" style="TEXT-ALIGN: right"></asp:TextBox>ÀÏ<br>
									&nbsp;Ãâ ÇÏ ¼± :
									<asp:Label id="lb_Com" runat="server"></asp:Label></td>
							</tr>
						</table>
					</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 2px solid; BORDER-BOTTOM: black 1px solid"
						width="300">
						<table style="FONT-SIZE: 9pt" cellSpacing="0" cellPadding="0" width="300" border="0">
							<tr>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; FONT-SIZE: 9pt"
									align="center" rowSpan="2"><FONT face="±¼¸²">°á
										<p>
										Àç</FONT></P></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; FONT-SIZE: 9pt"
									vAlign="middle" align="center" height="23"><FONT face="±¼¸²">´ã´ç</FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; FONT-SIZE: 9pt"
									vAlign="middle" align="center"><FONT face="±¼¸²">°úÀå</FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; FONT-SIZE: 9pt"
									vAlign="middle" align="center"><FONT face="±¼¸²">Â÷Àå</FONT></td>
								<td style="BORDER-RIGHT: black 2px solid; BORDER-TOP: black 1px solid; FONT-SIZE: 9pt"
									vAlign="middle" align="center"><FONT face="±¼¸²">ºÎÀå</FONT></td>
							</tr>
							<tr>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" height="69"><FONT face="±¼¸²">&nbsp;</FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;</FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;</FONT></td>
								<td style="BORDER-RIGHT: black 2px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;</FONT></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td style="BORDER-LEFT: black 2px solid; BORDER-BOTTOM: black 2px solid" colSpan="2">
						<table style="FONT-SIZE: 9pt" cellSpacing="0" cellPadding="0" width="700" border="0">
							<tr>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; FONT-SIZE: 9pt"
									vAlign="middle" align="center" width="70" height="30"><FONT face="±¼¸²">¼ö ÁÖ Ã³</FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; FONT-SIZE: 9pt"
									vAlign="middle" align="center" width="80" height="30"><FONT face="±¼¸²">±â&nbsp;&nbsp; 
										Á¾</FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; FONT-SIZE: 9pt"
									vAlign="middle" align="center" width="80" height="30"><FONT face="±¼¸²">¼ÒÀç¹øÈ£</FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; FONT-SIZE: 9pt"
									vAlign="middle" align="center" width="100" height="30"><FONT face="±¼¸²">Ç°¹ø(µµ¹ø)</FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; FONT-SIZE: 9pt"
									vAlign="middle" align="center" width="100" height="30"><FONT face="±¼¸²">Ç°&nbsp;&nbsp; 
										¸í</FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; FONT-SIZE: 9pt"
									vAlign="middle" align="center" width="60" height="30"><FONT face="±¼¸²">¼ö·®</FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; FONT-SIZE: 9pt"
									vAlign="middle" align="center" width="70" height="30"><FONT face="±¼¸²">°øÁ¤</FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; FONT-SIZE: 9pt"
									vAlign="middle" align="center" width="50" height="30"><FONT face="±¼¸²">±¸ºÐ</FONT></td>
								<td style="BORDER-RIGHT: black 2px solid; BORDER-TOP: black 1px solid; FONT-SIZE: 9pt"
									vAlign="middle" align="center" height="30"><FONT face="±¼¸²">ºñ°í</FONT></td>
							</tr>
							<tr>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:label id="Label1" runat="server"></asp:label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:label id="Label2" runat="server"></asp:label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:label id="Label3" runat="server"></asp:label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:label id="Label4" runat="server"></asp:label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:label id="Label5" runat="server"></asp:label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="right"
									height="30"><FONT face="±¼¸²"><asp:label id="Label6" runat="server"></asp:label>&nbsp;</FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:label id="Label7" runat="server"></asp:label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;</FONT></td>
								<td style="BORDER-RIGHT: black 2px solid; BORDER-TOP: black 1px solid" align="center"
									rowSpan="7"><FONT face="±¼¸²">&nbsp;</FONT></td>
							</tr>
							<tr>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:label id="Label8" runat="server"></asp:label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:label id="Label9" runat="server"></asp:label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:label id="Label10" runat="server"></asp:label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:label id="Label11" runat="server"></asp:label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:label id="Label12" runat="server"></asp:label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="right"
									height="30"><FONT face="±¼¸²"><asp:label id="Label13" runat="server"></asp:label>&nbsp;</FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:label id="Label14" runat="server"></asp:label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;</FONT></td>
							</tr>
							<tr>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:label id="Label15" runat="server"></asp:label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:label id="Label16" runat="server"></asp:label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:label id="Label17" runat="server"></asp:label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:label id="Label18" runat="server"></asp:label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:label id="Label19" runat="server"></asp:label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="right"
									height="30"><FONT face="±¼¸²"><asp:label id="Label20" runat="server"></asp:label>&nbsp;</FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:label id="Label21" runat="server"></asp:label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;</FONT></td>
							</tr>
							<tr>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:label id="Label22" runat="server"></asp:label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:label id="Label23" runat="server"></asp:label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:label id="Label24" runat="server"></asp:label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:label id="Label25" runat="server"></asp:label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:label id="Label26" runat="server"></asp:label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="right"
									height="30"><FONT face="±¼¸²">
										<asp:Label id="Label27" runat="server"></asp:Label>&nbsp;</FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label28" runat="server"></asp:Label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;</FONT></td>
							</tr>
							<tr>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label29" runat="server"></asp:Label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label30" runat="server"></asp:Label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label31" runat="server"></asp:Label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label32" runat="server"></asp:Label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label33" runat="server"></asp:Label></FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="right"
									height="30"><FONT face="±¼¸²">
										<asp:Label id="Label34" runat="server"></asp:Label>&nbsp;</FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="left"
									height="30"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label35" runat="server"></asp:Label></FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;</FONT></td>
							</tr>
							<tr>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label36" runat="server"></asp:Label></FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label37" runat="server"></asp:Label></FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label38" runat="server"></asp:Label></FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label39" runat="server"></asp:Label></FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label40" runat="server"></asp:Label></FONT></td>
								<td height="30" align="right" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">
										<asp:Label id="Label41" runat="server"></asp:Label>&nbsp;</FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label42" runat="server"></asp:Label></FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;</FONT></td>
							</tr>
							<tr>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label43" runat="server"></asp:Label></FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label44" runat="server"></asp:Label></FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label45" runat="server"></asp:Label></FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label46" runat="server"></asp:Label></FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label47" runat="server"></asp:Label></FONT></td>
								<td height="30" align="right" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">
										<asp:Label id="Label48" runat="server"></asp:Label>&nbsp;</FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label49" runat="server"></asp:Label></FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;</FONT></td>
							</tr>
							<tr>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label50" runat="server"></asp:Label></FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label51" runat="server"></asp:Label></FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label52" runat="server"></asp:Label></FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label53" runat="server"></asp:Label></FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label54" runat="server"></asp:Label></FONT></td>
								<td height="30" align="right" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">
										<asp:Label id="Label55" runat="server"></asp:Label>&nbsp;</FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label56" runat="server"></asp:Label></FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;</FONT></td>
								<td height="30" align="center" style="BORDER-RIGHT: black 2px solid; BORDER-TOP: black 1px solid; FONT-SIZE: 9pt">ÀÎ¼öÀÚ</td>
							</tr>
							<tr>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label57" runat="server"></asp:Label></FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label58" runat="server"></asp:Label></FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label59" runat="server"></asp:Label></FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label60" runat="server"></asp:Label></FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label61" runat="server"></asp:Label></FONT></td>
								<td height="30" align="right" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">
										<asp:Label id="Label62" runat="server"></asp:Label>&nbsp;</FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label63" runat="server"></asp:Label></FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;</FONT></td>
								<td rowspan="2" align="center" style="BORDER-RIGHT: black 2px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;</FONT></td>
							</tr>
							<tr>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label64" runat="server"></asp:Label></FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label65" runat="server"></asp:Label></FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label66" runat="server"></asp:Label></FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label67" runat="server"></asp:Label></FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label68" runat="server"></asp:Label></FONT></td>
								<td height="30" align="right" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">
										<asp:Label id="Label69" runat="server"></asp:Label>&nbsp;</FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;
										<asp:Label id="Label70" runat="server"></asp:Label></FONT></td>
								<td height="30" align="left" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid"><FONT face="±¼¸²">&nbsp;</FONT></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td style="FONT-SIZE: 9pt" height="20">&nbsp; MC 7- 28/0
					</td>
					<td style="FONT-SIZE: 9pt" height="20"><div align="right">¸í Ãµ °ø ¾÷ »ç&nbsp;&nbsp;
						</div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
