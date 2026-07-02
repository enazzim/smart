<%@ Page language="c#" Codebehind="OrderPaper1.aspx.cs" AutoEventWireup="false" Inherits="hanaro.BuyingOutside.PopUp.OrderPaper1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>OrderPaper1</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="1000" height="530" border="0" cellpadding="0" cellspacing="0" id="Table1">
				<tr>
					<td height="120" colspan="12"><table id="Table2" width="1000" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td width="220" height="30" style="FONT-SIZE: 11pt">발주번호</td>
								<td width="520" rowspan="2" align="center" style="FONT-WEIGHT: bold; FONT-SIZE: 25pt">주 
									문 서
								</td>
								<td width="260" rowspan="2" style="FONT-SIZE: 15pt">&nbsp;(주) 신동공업
								</td>
							</tr>
							<tr>
								<td height="30" style="FONT-SIZE: 11pt">
									<asp:Label id="lbYear" runat="server"></asp:Label>년
									<asp:Label id="lbMon" runat="server"></asp:Label>월
									<asp:Label id="lbDay" runat="server"></asp:Label>
									일
								</td>
							</tr>
							<tr>
								<td height="30" style="FONT-SIZE: 11pt">FAX :&nbsp;
									<asp:Label id="lbFax" runat="server"></asp:Label>
								</td>
								<td height="30" style="PADDING-LEFT: 50px; FONT-SIZE: 13pt">아래와 같이 주문하오니
								</td>
								<td height="30" style="FONT-SIZE: 12pt">주소 : 경남 진주시 상평동 203-10
								</td>
							</tr>
							<tr>
								<td height="30" style="FONT-SIZE: 11pt">업체명 :
									<asp:Label id="lbCompany" runat="server"></asp:Label>&nbsp;</td>
								<td height="30" style="PADDING-LEFT: 50px; FONT-SIZE: 13pt">납기내 납품하여 주시기 바랍니다.
								</td>
								<td height="30" style="FONT-SIZE: 12pt">전화 : 055) 752-2882 FAX: 755-1388
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
						<table width="1000" border="0" cellpadding="0" cellspacing="0" id="Table3" align="center"
							style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid">
							<tr>
								<td width="30" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center" height="25">순번</td>
								<td width="180" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center" height="25">품명</td>
								<td width="140" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center" height="25">도면번호</td>
								<td width="110" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center" height="25">재질</td>
								<td width="120" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center" height="25">규격</td>
								<td width="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center" height="25">단위</td>
								<td width="100" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center" height="25">수량</td>
								<td width="100" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center" height="25">납기일</td>
								<td width="180" colspan="4" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center" height="25">비고</td>
							</tr>
							<tr>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label1" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label2" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label3" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label4" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label5" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label6" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="right">
									<asp:Label id="Label7" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label8" runat="server"></asp:Label>&nbsp;</td>
								<td colspan="4" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									height="40" vAlign="middle" align="center">&nbsp;</td>
							</tr>
							<tr>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label9" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label10" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label11" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label12" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label13" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label14" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="right">
									<asp:Label id="Label15" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label16" runat="server"></asp:Label>&nbsp;</td>
								<td colspan="4" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									height="40" vAlign="middle" align="center">&nbsp;</td>
							</tr>
							<tr>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label17" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label18" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label19" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label20" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label21" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label22" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="right">
									<asp:Label id="Label23" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label24" runat="server"></asp:Label>&nbsp;</td>
								<td colspan="4" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									height="40" vAlign="middle" align="center">&nbsp;</td>
							</tr>
							<tr>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label25" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label26" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label27" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label28" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label29" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label30" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="right">
									<asp:Label id="Label31" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label32" runat="server"></asp:Label>&nbsp;</td>
								<td colspan="4" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									height="40" vAlign="middle" align="center">&nbsp;</td>
							</tr>
							<tr>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label33" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label34" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label35" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label36" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label37" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label38" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="right">
									<asp:Label id="Label39" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label40" runat="server"></asp:Label>&nbsp;</td>
								<td colspan="4" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									height="40" vAlign="middle" align="center">&nbsp;</td>
							</tr>
							<tr>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label41" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label42" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label43" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label44" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label45" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label46" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="right">
									<asp:Label id="Label47" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label48" runat="server"></asp:Label>&nbsp;</td>
								<td colspan="4" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									height="40" vAlign="middle" align="center">&nbsp;</td>
							</tr>
							<tr>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label49" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label50" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label51" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label52" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label53" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label54" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="right">
									<asp:Label id="Label55" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label56" runat="server"></asp:Label>&nbsp;</td>
								<td colspan="4" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									height="40" vAlign="middle" align="center">&nbsp;</td>
							</tr>
							<tr>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label57" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label58" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label59" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label60" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label61" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label62" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="right">
									<asp:Label id="Label63" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label64" runat="server"></asp:Label>&nbsp;</td>
								<td colspan="4" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									height="40" vAlign="middle" align="center">&nbsp;</td>
							</tr>
							<tr>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label65" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label66" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label67" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label68" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label69" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label70" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="right">
									<asp:Label id="Label71" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label72" runat="server"></asp:Label>&nbsp;</td>
								<td colspan="4" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									height="40" vAlign="middle" align="center">&nbsp;</td>
							</tr>
							<tr>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label73" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label74" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label75" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label76" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label77" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label78" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="right">
									<asp:Label id="Label79" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label80" runat="server"></asp:Label>&nbsp;</td>
								<td colspan="4" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									height="40" vAlign="middle" align="center">&nbsp;</td>
							</tr>
							<tr>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label81" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label82" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label83" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label84" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label85" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label86" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="right">
									<asp:Label id="Label87" runat="server"></asp:Label>&nbsp;</td>
								<td height="40" style="BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 3px; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">
									<asp:Label id="Label88" runat="server"></asp:Label>&nbsp;</td>
								<td colspan="4" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									height="40" vAlign="middle" align="center">&nbsp;</td>
							</tr>
							<tr>
								<td colspan="8" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: black 1px solid"
									rowspan="2" height="25">&nbsp;</td>
								<td width="30" rowspan="2" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: black 1px solid"
									vAlign="middle" align="center" height="25"><p>결</p>
									<p>재</p>
								</td>
								<td width="50" height="25" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">담당</td>
								<td width="50" height="25" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">차장</td>
								<td width="50" height="25" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									vAlign="middle" align="center">전무</td>
							</tr>
							<tr>
								<td height="60" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: black 1px solid">&nbsp;</td>
								<td height="60" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: black 1px solid">&nbsp;</td>
								<td height="60" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: 9pt; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: black 1px solid">&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
