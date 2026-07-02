<%@ Page language="c#" Codebehind="PurchaseOrder.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BuyingOutside.PopUp.PurchaseOrder" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Purchase</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" style="POSITION: absolute; TOP: 8px; LEFT: 8px" height="530" cellSpacing="0"
				cellPadding="0" width="1000" border="0">
				<tr>
					<td width="1000">
						<table id="Table2" cellSpacing="0" cellPadding="0" width="1000" border="0">
							<tr>
								<td style="FONT-SIZE: 11pt" width="220" height="50">발주번호 :&nbsp;
									<asp:textbox id="TextBox1" runat="server" Width="110px" BorderStyle="None"></asp:textbox></td>
								<td style="FONT-SIZE: 25pt; FONT-WEIGHT: bold" align="center" width="520" height="50"
									rowSpan="2">외주 주 문 서
								</td>
								<td style="FONT-SIZE: 15pt" width="260" height="50" rowSpan="2">&nbsp;유한책임회사 신동공업
								</td>
							</tr>
							<tr>
								<td style="FONT-SIZE: 11pt" height="50"><asp:label id="lbYear" runat="server"></asp:label>년
									<asp:label id="lbMon" runat="server"></asp:label>월
									<asp:label id="lbDay" runat="server"></asp:label>일
								</td>
							</tr>
							<tr>
								<td style="FONT-SIZE: 11pt" height="50">FAX :&nbsp;
									<asp:label id="lbFax" runat="server"></asp:label></td>
								<td style="PADDING-LEFT: 50px; FONT-SIZE: 13pt" height="50">아래와 같이 주문하오니 납기내 납품하여 
									주시기 바랍니다.
								</td>
								<td style="FONT-SIZE: 11pt" height="50">주소 : 사천시 곤양면 곤북로 82
								</td>
							</tr>
							<tr>
								<td style="FONT-SIZE: 11pt" height="50">업체명 :
									<asp:label id="lbCompany" runat="server"></asp:label>&nbsp;</td>
								<td style="PADDING-LEFT: 50px; FONT-SIZE: 13pt" vAlign="middle" height="50"><FONT face="굴림">문의사항은 
										아래 발주담당자에게 문의요망. </FONT>
								</td>
								<td style="FONT-SIZE: 11pt" height="50">전화 : 055) 855-0145 FAX: 855-0143
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td width="1000">
						<table id="Table3" cellSpacing="0" cellPadding="0" width="1000" align="center" border="0">
							<tr>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="30" height="25">순번</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="120" height="25">품명</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="120" height="25">품번</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="65" height="25">재질</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="180" height="25">규격</td>
								<TD style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="65" height="25"><FONT face="굴림">시작공정</FONT></TD>
								<TD style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="70" height="25"><FONT face="굴림">종료공정</FONT></TD>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45" height="25">단위</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="50" height="25">수량</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="50" height="25"><FONT face="굴림">단가</FONT></td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="90" height="25"><FONT face="굴림">금액</FONT></td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="90" height="25">납기일</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid"
									vAlign="middle" align="center" width="70" height="25">비고</td>
							</tr>
							<tr>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="30" height="25"><asp:label id="Label1" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="120" height="25"><asp:label id="Label2" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="120" height="25"><asp:label id="Label3" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="65" height="25"><asp:label id="Label4" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="195" height="25"><asp:label id="Label5" runat="server"></asp:label>&nbsp;</td>
								<TD style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="65" height="25"><asp:label id="Label123" runat="server"></asp:label><FONT face="굴림">&nbsp;</FONT></TD>
								<TD style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45" height="25"><asp:label id="Label112" runat="server"></asp:label>&nbsp;</TD>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45" height="25"><asp:label id="Label6" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; PADDING-RIGHT: 2px; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50" height="25"><asp:label id="Label7" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50" height="25">&nbsp;
									<asp:label id="Label89" runat="server"></asp:label></td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="90" height="25"><asp:label id="Label90" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="90" height="25"><asp:label id="Label8" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid"
									vAlign="middle" align="center" width="70" height="25">
									<asp:label id="Label134" runat="server"></asp:label>&nbsp;</td>
							</tr>
							<tr>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="30" height="25"><asp:label id="Label9" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="120" height="25"><asp:label id="Label10" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="120" height="25"><asp:label id="Label11" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="65" height="25"><asp:label id="Label12" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="195" height="25"><asp:label id="Label13" runat="server"></asp:label>&nbsp;</td>
								<TD style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="65" height="25"><asp:label id="Label124" runat="server"></asp:label><FONT face="굴림">&nbsp;</FONT></TD>
								<TD style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45" height="25"><asp:label id="Label113" runat="server"></asp:label>&nbsp;</TD>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45" height="25"><asp:label id="Label14" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; PADDING-RIGHT: 2px; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50" height="25"><asp:label id="Label15" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50" height="25"><asp:label id="Label92" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="90" height="25"><asp:label id="Label93" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="90" height="25"><asp:label id="Label16" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid"
									vAlign="middle" align="center" width="70" height="25">&nbsp;
									<asp:label id="Label135" runat="server"></asp:label>&nbsp;</td>
							</tr>
							<tr>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="30" height="25"><asp:label id="Label17" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="120" height="25"><asp:label id="Label18" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="120" height="25"><asp:label id="Label19" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="65" height="25"><asp:label id="Label20" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="195" height="25"><asp:label id="Label21" runat="server"></asp:label>&nbsp;</td>
								<TD style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="65" height="25"><asp:label id="Label125" runat="server"></asp:label><FONT face="굴림">&nbsp;</FONT></TD>
								<TD style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45" height="25"><asp:label id="Label114" runat="server"></asp:label>&nbsp;</TD>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45" height="25"><asp:label id="Label22" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; PADDING-RIGHT: 2px; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50" height="25"><asp:label id="Label23" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50" height="25"><asp:label id="Label94" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="90" height="25"><asp:label id="Label95" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="90" height="25"><asp:label id="Label24" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid"
									vAlign="middle" align="center" width="70" height="25">&nbsp;
									<asp:label id="Label136" runat="server"></asp:label>&nbsp;</td>
							</tr>
							<tr>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="30" height="25"><asp:label id="Label25" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="120" height="25"><asp:label id="Label26" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="120" height="25"><asp:label id="Label27" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="65" height="25"><asp:label id="Label28" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="195" height="25"><asp:label id="Label29" runat="server"></asp:label>&nbsp;</td>
								<TD style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="65" height="25"><asp:label id="Label126" runat="server"></asp:label><FONT face="굴림">&nbsp;</FONT></TD>
								<TD style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45" height="25"><asp:label id="Label115" runat="server"></asp:label>&nbsp;</TD>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45" height="25"><asp:label id="Label30" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; PADDING-RIGHT: 2px; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50" height="25"><asp:label id="Label31" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50" height="25">&nbsp;
									<asp:label id="Label96" runat="server"></asp:label></td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="90" height="25">&nbsp;
									<asp:label id="Label97" runat="server"></asp:label></td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="90" height="25"><asp:label id="Label32" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid"
									vAlign="middle" align="center" width="70" height="25">
									<asp:label id="Label137" runat="server"></asp:label>&nbsp;</td>
							</tr>
							<tr>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="30" height="25"><asp:label id="Label33" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="120" height="25"><asp:label id="Label34" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="120" height="25"><asp:label id="Label35" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="65" height="25"><asp:label id="Label36" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="195" height="25"><asp:label id="Label37" runat="server"></asp:label>&nbsp;</td>
								<TD style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="65" height="25"><asp:label id="Label127" runat="server"></asp:label><FONT face="굴림">&nbsp;</FONT></TD>
								<TD style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45" height="25"><asp:label id="Label116" runat="server"></asp:label>&nbsp;</TD>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45" height="25"><asp:label id="Label38" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; PADDING-RIGHT: 2px; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50" height="25"><asp:label id="Label39" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50" height="25">&nbsp;
									<asp:label id="Label98" runat="server"></asp:label></td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="90" height="25"><asp:label id="Label99" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="90" height="25"><asp:label id="Label40" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid"
									vAlign="middle" align="center" width="70" height="25">
									<asp:label id="Label138" runat="server"></asp:label><FONT face="굴림">&nbsp;</FONT></td>
							</tr>
							<tr>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="30" height="25"><asp:label id="Label41" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="120" height="25"><asp:label id="Label42" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="120" height="25"><asp:label id="Label43" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="65" height="25"><asp:label id="Label44" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="195" height="25"><asp:label id="Label45" runat="server"></asp:label>&nbsp;</td>
								<TD style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="65" height="25"><asp:label id="Label128" runat="server"></asp:label><FONT face="굴림">&nbsp;</FONT></TD>
								<TD style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45" height="25"><asp:label id="Label117" runat="server"></asp:label>&nbsp;</TD>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45" height="25"><asp:label id="Label46" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; PADDING-RIGHT: 2px; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50" height="25"><asp:label id="Label47" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50" height="25"><asp:label id="Label100" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="90" height="25"><asp:label id="Label101" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="90" height="25"><asp:label id="Label48" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid"
									vAlign="middle" align="center" width="70" height="25">
									<asp:label id="Label139" runat="server"></asp:label><FONT face="굴림">&nbsp;</FONT></td>
							</tr>
							<tr>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="30" height="25"><asp:label id="Label49" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="120" height="25"><asp:label id="Label50" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="120" height="25"><asp:label id="Label51" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="65" height="25"><asp:label id="Label52" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="195" height="25"><asp:label id="Label53" runat="server"></asp:label>&nbsp;</td>
								<TD style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="65" height="25"><asp:label id="Label129" runat="server"></asp:label><FONT face="굴림">&nbsp;</FONT></TD>
								<TD style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45" height="25"><asp:label id="Label118" runat="server"></asp:label>&nbsp;</TD>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45" height="25"><asp:label id="Label54" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; PADDING-RIGHT: 2px; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50" height="25"><asp:label id="Label55" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50" height="25"><asp:label id="Label102" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="90" height="25"><asp:label id="Label103" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="90" height="25"><asp:label id="Label56" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid"
									vAlign="middle" align="center" width="70" height="25">
									<asp:label id="Label140" runat="server"></asp:label>&nbsp;</td>
							</tr>
							<tr>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="30" height="25"><asp:label id="Label57" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="120" height="25"><asp:label id="Label58" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="120" height="25"><asp:label id="Label59" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="65" height="25"><asp:label id="Label60" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="195" height="25"><asp:label id="Label61" runat="server"></asp:label>&nbsp;</td>
								<TD style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="65" height="25"><asp:label id="Label130" runat="server"></asp:label><FONT face="굴림">&nbsp;</FONT></TD>
								<TD style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45" height="25"><asp:label id="Label119" runat="server"></asp:label>&nbsp;</TD>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45" height="25"><asp:label id="Label62" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; PADDING-RIGHT: 2px; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50" height="25"><asp:label id="Label63" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50" height="25">&nbsp;
									<asp:label id="Label104" runat="server"></asp:label></td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="90" height="25">&nbsp;
									<asp:label id="Label105" runat="server"></asp:label></td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="90" height="25"><asp:label id="Label64" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid"
									vAlign="middle" align="center" width="70" height="25">
									<asp:label id="Label141" runat="server"></asp:label>&nbsp;</td>
							</tr>
							<tr>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="30" height="25"><asp:label id="Label65" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="120" height="25"><asp:label id="Label66" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="120" height="25"><asp:label id="Label67" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="65" height="25"><asp:label id="Label68" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="195" height="25"><asp:label id="Label69" runat="server"></asp:label>&nbsp;</td>
								<TD style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="65" height="25"><asp:label id="Label131" runat="server"></asp:label><FONT face="굴림">&nbsp;</FONT></TD>
								<TD style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45" height="25"><asp:label id="Label120" runat="server"></asp:label>&nbsp;</TD>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45" height="25"><asp:label id="Label70" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; PADDING-RIGHT: 2px; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50" height="25"><asp:label id="Label71" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50" height="25">&nbsp;
									<asp:label id="Label106" runat="server"></asp:label></td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="90" height="25"><asp:label id="Label107" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="90" height="25"><asp:label id="Label72" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid"
									vAlign="middle" align="center" width="70" height="25">
									<asp:label id="Label142" runat="server"></asp:label>&nbsp;</td>
							</tr>
							<tr>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="30" height="25"><asp:label id="Label73" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="120" height="25"><asp:label id="Label74" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="120" height="25"><asp:label id="Label75" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="65" height="25"><asp:label id="Label76" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="195" height="25"><asp:label id="Label77" runat="server"></asp:label>&nbsp;</td>
								<TD style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="65" height="25"><asp:label id="Label132" runat="server"></asp:label><FONT face="굴림">&nbsp;</FONT></TD>
								<TD style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45" height="25"><asp:label id="Label121" runat="server"></asp:label>&nbsp;</TD>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45" height="25"><asp:label id="Label78" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; PADDING-RIGHT: 2px; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50" height="25"><asp:label id="Label79" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50" height="25">&nbsp;
									<asp:label id="Label108" runat="server"></asp:label></td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="90" height="25"><asp:label id="Label109" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="90" height="25"><asp:label id="Label80" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid"
									vAlign="middle" align="center" width="70" height="25">
									<asp:label id="Label143" runat="server"></asp:label><FONT face="굴림">&nbsp;</FONT></td>
							</tr>
							<tr>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="30" height="25"><asp:label id="Label81" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="120" height="25"><asp:label id="Label82" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="120" height="25"><asp:label id="Label83" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="65" height="25"><asp:label id="Label84" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="195" height="25"><asp:label id="Label85" runat="server"></asp:label>&nbsp;</td>
								<TD style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="65" height="25"><asp:label id="Label133" runat="server"></asp:label><FONT face="굴림">&nbsp;</FONT></TD>
								<TD style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45" height="25"><asp:label id="Label122" runat="server"></asp:label>&nbsp;</TD>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="45" height="25"><asp:label id="Label86" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; PADDING-RIGHT: 2px; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50" height="25"><asp:label id="Label87" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; PADDING-RIGHT: 3px; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="50" height="25"><asp:label id="Label110" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="right" width="90" height="25"><asp:label id="Label111" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="90" height="25"><asp:label id="Label88" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid"
									vAlign="middle" align="center" width="70" height="25">
									<asp:label id="Label144" runat="server"></asp:label>&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td style="BORDER-BOTTOM: black 1px solid" width="1000"><table id="Table4" cellSpacing="0" cellPadding="0" width="1000" align="center" border="0">
							<tr>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid; PADDING-TOP: 10px"
									vAlign="top" width="820" height="10" rowSpan="2">
									<FONT face="굴림">&nbsp;거래처 협조사항 :<br>
										&nbsp;① 납품서에 품번을 반드시 명기할 것<br>
										&nbsp;② 주문서의 단가 이상시 담당자에 연락할 것</FONT><textarea name="content" cols="94" rows="3"></textarea>
									<br>
									<FONT face="굴림">&nbsp;발주자 :&nbsp;
										<asp:label id="lbName" runat="server"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
										&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
										&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
										&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
										&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:label id="Label91" runat="server"></asp:label></FONT>
									<P></P>
								</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="30" height="10" rowSpan="2">
									<p>결</p>
									<p>재</p>
								</td>
								<td style="BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="50" height="10">담당</td>
								<td style="BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid"
									vAlign="middle" align="center" width="50" height="10">차장</td>
								<td style="BORDER-BOTTOM: black 1px solid; BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid"
									vAlign="middle" align="center" width="50" height="10">전무</td>
							</tr>
							<tr>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt" vAlign="middle" align="center"
									width="50" height="30"><FONT face="굴림">&nbsp;</FONT></td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt" vAlign="middle" align="center"
									width="50" height="30"><FONT face="굴림">&nbsp; </FONT>
								</td>
								<td style="BORDER-LEFT: black 1px solid; FONT-SIZE: 9pt; BORDER-RIGHT: black 1px solid"
									vAlign="middle" align="center" width="50" height="30"><FONT face="굴림">&nbsp; </FONT>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td style="PADDING-TOP: 5px" width="1000">
						<table id="Table5" cellSpacing="0" cellPadding="0" align="center" border="0">
							<tr>
								<td style="MARGIN-TOP: 15px; PADDING-LEFT: 20px; FONT-SIZE: 9pt" vAlign="middle" align="left"
									width="300">SDQ-05-002-4</td>
								<td style="FONT-SIZE: 9pt" vAlign="middle" align="center" width="400">유한책임회사 신 동 공 
									업</td>
								<td style="PADDING-RIGHT: 50px; FONT-SIZE: 9pt" vAlign="middle" align="right" width="300">A4(210X297)</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
