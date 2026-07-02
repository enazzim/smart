<%@ Page language="c#" Codebehind="BuyPurchase1.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BuyingOutside.PopUp.BuyPurchase1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BuyPurchase</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script src="//code.jquery.com/jquery-1.11.2.min.js"></script>
		<script src="//code.jquery.com/jquery-migrate-1.2.1.min.js"></script>
		<link href="http://ts.daumcdn.net/custom/blog/0/606/skin/images/nanumgothic.css" rel="stylesheet"
			type="text/css">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="1000" height="530" border="0" cellpadding="0" cellspacing="0" id="Table1"
				style="POSITION: absolute; TOP: 20px; LEFT: 10px">
				<tr>
					<td height="120" colspan="12"><table id="Table2" width="1000" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td width="220" height="50" style="FONT-SIZE: 11pt">발주번호 :&nbsp;
									<asp:TextBox id="TextBox1" runat="server" BorderStyle="None" Width="110px"></asp:TextBox></td>
								<td width="520" rowspan="2" align="center" style="FONT-SIZE: 25pt; FONT-WEIGHT: bold"
									height="50">주 문 서
								</td>
								<td width="260" rowspan="2" style="FONT-SIZE: 15pt" height="50">&nbsp;유한책임회사 신동공업
								</td>
							</tr>
							<tr>
								<td height="50" style="FONT-SIZE: 11pt">
									<asp:Label id="lbYear" runat="server"></asp:Label>년
									<asp:Label id="lbMon" runat="server"></asp:Label>월
									<asp:Label id="lbDay" runat="server"></asp:Label>
									일
								</td>
							</tr>
							<tr>
								<td height="50" style="FONT-SIZE: 11pt">FAX :&nbsp;
									<asp:Label id="lbFax" runat="server"></asp:Label>
								</td>
								<td height="50" style="PADDING-LEFT: 50px; FONT-SIZE: 13pt">아래와 같이 주문하오니 납기내 납품하여 
									주시기 바랍니다.
								</td>
								<td height="50" style="FONT-SIZE: 11pt">주소 : 사천시 곤양면 곤북로 82
								</td>
							</tr>
							<tr>
								<td height="50" style="FONT-SIZE: 11pt">업체명 :
									<asp:Label id="lbCompany" runat="server"></asp:Label>&nbsp;</td>
								<td height="50" style="PADDING-LEFT: 50px; FONT-SIZE: 13pt" vAlign="middle"><FONT face="굴림">문의사항은 
										아래 발주담당자에게 문의요망. </FONT>
								</td>
								<td height="50" style="FONT-SIZE: 11pt">전화 : 055) 855-0145 FAX: 855-0143
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td height="5" colspan="12" style="FONT-SIZE: 1pt">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="12" vAlign="top">
						<table width="1000" border="0" cellpadding="0" cellspacing="0" id="Table3" align="center">
							<tr>
								<td width="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" height="25">순번</td>
								<td width="140" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" height="25">품명</td>
								<td width="140" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" height="25">품번</td>
								<td width="60" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" height="25">재질</td>
								<td width="140" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" height="25">규격</td>
								<td width="45" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" height="25">단위</td>
								<td width="40" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" height="25">대개</td>
								<td width="50" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" height="25">수량</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="80">
									납기일</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="75">
									현재고</td>
								<td width="75" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid"
									vAlign="middle" align="center" height="25">기타</td>
							</tr>
							<tr>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="25">
									<asp:Label id="Label1" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label2" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label3" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="60">
									<asp:Label id="Label4" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label5" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45">
									<asp:Label id="Label6" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="40">&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; PADDING-RIGHT: 2px; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50">
									<asp:Label id="Label7" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="80">
									<asp:Label id="Label8" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="75">
									<asp:Label id="Label112" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid"
									height="25" vAlign="middle" align="right" width="75">&nbsp;
								</td>
							</tr>
							<tr>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="25">
									<asp:Label id="Label9" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label10" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label11" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="60">
									<asp:Label id="Label12" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label13" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45">
									<asp:Label id="Label14" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="40">&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; PADDING-RIGHT: 2px; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50">
									<asp:Label id="Label15" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="80">
									<asp:Label id="Label16" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="75">
									<asp:Label id="Label113" runat="server"></asp:Label><FONT face="굴림">&nbsp;</FONT></td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid"
									height="25" vAlign="middle" align="right" width="75">&nbsp;</td>
							</tr>
							<tr>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="25">
									<asp:Label id="Label17" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label18" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label19" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="60">
									<asp:Label id="Label20" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label21" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45">
									<asp:Label id="Label22" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="40">&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; PADDING-RIGHT: 2px; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50">
									<asp:Label id="Label23" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="80">
									<asp:Label id="Label24" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="75">
									<asp:Label id="Label114" runat="server"></asp:Label><FONT face="굴림">&nbsp;</FONT></td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid"
									height="25" vAlign="middle" align="right" width="75">&nbsp;</td>
							</tr>
							<tr>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="25">
									<asp:Label id="Label25" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label26" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label27" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="60">
									<asp:Label id="Label28" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label29" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45">
									<asp:Label id="Label30" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="40">&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; PADDING-RIGHT: 2px; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50">
									<asp:Label id="Label31" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="80">
									<asp:Label id="Label32" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="75">
									<asp:Label id="Label115" runat="server"></asp:Label><FONT face="굴림">&nbsp;</FONT></td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid"
									height="25" vAlign="middle" align="right" width="75">&nbsp;</td>
							</tr>
							<tr>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="25">
									<asp:Label id="Label33" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label34" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label35" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="60">
									<asp:Label id="Label36" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label37" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45">
									<asp:Label id="Label38" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="40">&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; PADDING-RIGHT: 2px; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50">
									<asp:Label id="Label39" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="80">
									<asp:Label id="Label40" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="75">
									<asp:Label id="Label116" runat="server"></asp:Label><FONT face="굴림">&nbsp;</FONT></td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid"
									height="25" vAlign="middle" align="right" width="75">&nbsp;</td>
							</tr>
							<tr>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="25">
									<asp:Label id="Label41" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label42" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label43" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="60">
									<asp:Label id="Label44" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label45" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45">
									<asp:Label id="Label46" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="40">&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; PADDING-RIGHT: 2px; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50">
									<asp:Label id="Label47" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="80">
									<asp:Label id="Label48" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="75">
									<asp:Label id="Label117" runat="server"></asp:Label><FONT face="굴림">&nbsp;</FONT></td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid"
									height="25" vAlign="middle" align="right" width="75">&nbsp;</td>
							</tr>
							<tr>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="25">
									<asp:Label id="Label49" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label50" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label51" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="60">
									<asp:Label id="Label52" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label53" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45">
									<asp:Label id="Label54" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="40">&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; PADDING-RIGHT: 2px; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50">
									<asp:Label id="Label55" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="80">
									<asp:Label id="Label56" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="75">
									<asp:Label id="Label118" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid"
									height="25" vAlign="middle" align="right" width="75">&nbsp;</td>
							</tr>
							<tr>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="25">
									<asp:Label id="Label57" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label58" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label59" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="60">
									<asp:Label id="Label60" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label61" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45">
									<asp:Label id="Label62" runat="server"></asp:Label><FONT face="굴림">&nbsp;</FONT></td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="40">&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; PADDING-RIGHT: 2px; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50">
									<asp:Label id="Label63" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="80">
									<asp:Label id="Label64" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="75">
									<asp:Label id="Label119" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid"
									height="25" vAlign="middle" align="right" width="75">&nbsp;</td>
							</tr>
							<tr>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="25">
									<asp:Label id="Label65" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label66" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label67" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="60">
									<asp:Label id="Label68" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label69" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45">
									<asp:Label id="Label70" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; PADDING-RIGHT: 2px; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="40">&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="50">
									<asp:Label id="Label71" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="80">
									<asp:Label id="Label72" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="75">
									<asp:Label id="Label120" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid"
									height="25" vAlign="middle" align="right" width="75">&nbsp;</td>
							</tr>
							<tr>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="25">
									<asp:Label id="Label73" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label74" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label75" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="60">
									<asp:Label id="Label76" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label77" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45">
									<asp:Label id="Label78" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="40">&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; PADDING-RIGHT: 2px; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50">
									<asp:Label id="Label79" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="80">
									<asp:Label id="Label80" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="75">
									<asp:Label id="Label121" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid"
									height="25" vAlign="middle" align="right" width="75">&nbsp;</td>
							</tr>
							<tr>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="25">
									<asp:Label id="Label81" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label82" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label83" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="60">
									<asp:Label id="Label84" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="140">
									<asp:Label id="Label85" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45">
									<asp:Label id="Label86" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="40">&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; PADDING-RIGHT: 2px; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50">
									<asp:Label id="Label87" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="80">
									<asp:Label id="Label88" runat="server"></asp:Label>&nbsp;</td>
								<td height="25" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="75">
									<asp:Label id="Label122" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid"
									height="25" vAlign="middle" align="right" width="75">&nbsp;</td>
							</tr>
							<TR>
								<TD style="BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid"
									vAlign="middle" align="center" colSpan="13"><FONT face="굴림"></FONT>
									<table id="tb" border="0" cellpadding="0" cellspacing="0" align="center" width="1000">
										<TBODY>
											<tr>
												<td rowspan="2" style="FONT-SIZE: 9pt; PADDING-TOP: 5px" width="770" height="10" vAlign="top">
													<textarea name="content" cols="40" rows="5"></textarea>
													<P><FONT face="굴림">&nbsp;발주자 :&nbsp;
															<asp:Label id="lbName" runat="server"></asp:Label>
															&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
															&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
															&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
															&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
															&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
															<asp:Label id="Label91" runat="server"></asp:Label></FONT></P>
												</td>
												<td rowspan="2" width="30" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt" vAlign="middle"
													align="center" height="10"><p>결</p>
													<p>재</p>
												</td>
												<td width="50" height="10" style="BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt"
													vAlign="middle" align="center">담당</td>
												<td width="50" height="10" style="BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt"
													vAlign="middle" align="center">팀장</td>
												<td width="50" height="10" style="BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt"
													vAlign="middle" align="center">전무</td>
												<td width="50" height="10" style="BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt"
													vAlign="middle" align="center">사장</td>
											</tr>
											<tr>
												<td width="50" height="30" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt" vAlign="middle"
													align="center"><FONT face="굴림"></FONT></td>
												<td width="50" height="30" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt" vAlign="middle"
													align="center"><FONT face="굴림"></FONT></td>
												<td width="50" height="30" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt" vAlign="middle"
													align="center"><FONT face="굴림"></FONT></td>
												<td width="50" height="30" style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt" vAlign="middle"
													align="center"></td>
											</tr>
							</TR>
						</table>
					</td>
				</tr>
			</table>
			<br>
			<table id="4" border="0" cellpadding="0" cellspacing="0" align="center">
				<tr>
					<td style="MARGIN-TOP: 15px; PADDING-LEFT: 20px; FONT-SIZE: 9pt" vAlign="middle" align="left"
						width="300">SDQ-05-002-4</td>
					<td style="FONT-SIZE: 9pt" vAlign="middle" align="center" width="400">유한책임회사 신 동 공 
						업</td>
					<td style="PADDING-RIGHT: 50px; FONT-SIZE: 9pt" vAlign="middle" align="right" width="300">A4(210X297)</td>
				</tr>
			</table>
			</TD></TR></TBODY></TABLE> &nbsp;
		</form>
	</body>
</HTML>
