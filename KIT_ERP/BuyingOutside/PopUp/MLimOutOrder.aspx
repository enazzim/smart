<%@ Page language="c#" Codebehind="MLimOutOrder.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BuyingOutside.PopUp.MLimOutOrder" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>MLimOutOrder</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" style="FONT-SIZE: 9pt; LEFT: 0px; POSITION: absolute; TOP: 50px" cellSpacing="0"
				cellPadding="0" width="710" border="0">
				<tr>
					<td style="FONT-SIZE: 30pt" align="center" colSpan="12" height="95">발&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
						주&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 서</td>
				</tr>
				<TR>
					<TD style="PADDING-LEFT: 5px; FONT-SIZE: 9pt" align="left" colSpan="12" height="25"><FONT face="굴림">발주번호 
							:
							<asp:textbox id="Textbox1" runat="server" BorderStyle="None"></asp:textbox></FONT></TD>
				</TR>
				<tr>
					<td style="BORDER-RIGHT: black 1px solid" colSpan="12">
						<table id="Table4" style="FONT-SIZE: 9pt" cellSpacing="0" cellPadding="0" width="710" border="0">
							<tr>
								<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
									align="center" width="40" height="40" rowSpan="3">거<br>
									래<br>
									처</td>
								<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" align="center"
									width="80" height="40">업체명</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
									align="center" width="100" height="40">&nbsp;
									<asp:label id="lbCompany" runat="server"></asp:label></td>
								<td style="BORDER-TOP: black 1px solid; BORDER-BOTTOM: black 1px solid" align="center"
									width="40" height="40" rowSpan="3">발<br>
									주<br>
									처</td>
								<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" align="center"
									width="60" height="40">업체명</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
									align="center" colSpan="3" height="40">(주) 명 림</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" align="center"
									width="40" height="40" rowSpan="2">결
									<p>재</p>
								</td>
								<td style="PADDING-RIGHT: 0px; BORDER-TOP: black 1px solid; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; PADDING-TOP: 0px"
									align="center" width="120" colSpan="3" height="40" rowSpan="2">
									<table id="Table2" style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; FONT-SIZE: 9pt; PADDING-BOTTOM: 0px; MARGIN: 0px; PADDING-TOP: 0px"
										cellSpacing="0" cellPadding="0" width="180" border="0">
										<tr>
											<td style="BORDER-RIGHT: black 1px solid; BORDER-BOTTOM: black 1px solid" vAlign="middle"
												align="center" width="60" height="20"><FONT face="굴림">과장</FONT></td>
											<td style="BORDER-RIGHT: black 1px solid; BORDER-BOTTOM: black 1px solid" vAlign="middle"
												align="center" width="60" height="20"><FONT face="굴림">부장</FONT></td>
											<td style="BORDER-BOTTOM: black 1px solid" vAlign="middle" align="center" width="60"
												height="20"><FONT face="굴림">승인</FONT></td>
										</tr>
										<tr>
											<td style="BORDER-RIGHT: black 1px solid" height="60">&nbsp;</td>
											<td style="BORDER-RIGHT: black 1px solid" height="60">&nbsp;</td>
											<td height="60">&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" align="center"
									height="40">담당자</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
									align="center" height="40">&nbsp;
									<asp:label id="lbPerson" runat="server"></asp:label></td>
								<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" align="center"
									height="40">담당자</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
									align="center" colSpan="3" height="40"><asp:label id="lbName" runat="server"></asp:label>&nbsp;</td>
							</tr>
							<tr>
								<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
									align="center" height="40">연락처</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
									align="center" height="40">&nbsp;
									<asp:label id="lbTel" runat="server"></asp:label></td>
								<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
									align="center" height="40">연락처</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
									align="center" colSpan="3" height="40">055) 758-7377,7577
								</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-BOTTOM: black 1px solid"
									align="center" height="40">발행일</td>
								<td style="BORDER-TOP: black 1px solid; BORDER-BOTTOM: black 1px solid" align="center"
									colSpan="3" height="40">&nbsp;
									<asp:textbox id="tbDate" runat="server" BorderStyle="None"></asp:textbox></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td style="BORDER-TOP-WIDTH: 1px; BORDER-RIGHT: black 1px solid; FONT-SIZE: 10pt; BORDER-LEFT: black 1px solid; BORDER-TOP-COLOR: black; BORDER-BOTTOM: black 1px solid"
						align="center" colSpan="12" height="35">하기와 같이 물품을 주문하오니 품질, 납기 준수하여 납품하여 주시기 
						바랍니다.
					</td>
				</tr>
				<tr>
					<td style="BORDER-RIGHT: black 1px solid; PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: black 1px solid; PADDING-TOP: 0px; BORDER-BOTTOM: black 1px solid"
						vAlign="top" colSpan="12">
						<table id="Table3" style="FONT-SIZE: 9pt" cellSpacing="0" cellPadding="0" width="710" border="0">
							<tr>
								<td style="BORDER-RIGHT: black 1px solid" vAlign="middle" align="center" width="40"
									height="30">No</td>
								<td style="BORDER-RIGHT: black 1px solid" vAlign="middle" align="center" width="100"
									height="30">공정</td>
								<td style="BORDER-RIGHT: black 1px solid" vAlign="middle" align="center" width="100"
									height="30">도번</td>
								<td style="BORDER-RIGHT: black 1px solid" vAlign="middle" align="center" width="100"
									colSpan="2" height="30">품명</td>
								<td style="BORDER-RIGHT: black 1px solid" vAlign="middle" align="center" width="40"
									height="30">단위</td>
								<td style="BORDER-RIGHT: black 1px solid" vAlign="middle" align="center" width="70"
									height="30">수량</td>
								<td style="BORDER-RIGHT: black 1px solid" vAlign="middle" align="center" width="60"
									height="30">단가</td>
								<td style="BORDER-RIGHT: black 1px solid" vAlign="middle" align="center" width="100"
									colSpan="3" height="30">납기일자</td>
								<td vAlign="middle" align="center" height="30" width="100">치 수</td>
							</tr>
							<tr>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30"><asp:label id="Label1" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30"><asp:label id="Label2" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30"><asp:label id="Label3" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" colSpan="2" height="30"><asp:label id="Label4" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30"><asp:label id="Label5" runat="server"></asp:label><FONT face="굴림">&nbsp;</FONT></td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="right" height="30"><asp:label id="Label6" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="right" height="30"><asp:label id="Label7" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" colSpan="3" height="30"><asp:label id="Label8" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-TOP: black 1px solid" vAlign="middle" align="center" height="30"><FONT face="굴림">
										<asp:label id="Label89" runat="server"></asp:label>&nbsp;</FONT></td>
							</tr>
							<tr>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30"><asp:label id="Label9" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30"><asp:label id="Label10" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30"><asp:label id="Label11" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" colSpan="2" height="30"><asp:label id="Label12" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30"><asp:label id="Label13" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="right" height="30"><asp:label id="Label14" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="right" height="30"><asp:label id="Label15" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" colSpan="3" height="30"><asp:label id="Label16" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-TOP: black 1px solid" vAlign="middle" align="center" height="30"><FONT face="굴림">
										<asp:label id="Label90" runat="server"></asp:label>&nbsp;</FONT></td>
							</tr>
							<tr>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30"><asp:label id="Label17" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30"><asp:label id="Label18" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30"><asp:label id="Label19" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" colSpan="2" height="30"><asp:label id="Label20" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30"><asp:label id="Label21" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="right" height="30"><asp:label id="Label22" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="right" height="30"><asp:label id="Label23" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" colSpan="3" height="30"><asp:label id="Label24" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-TOP: black 1px solid" vAlign="middle" align="center" height="30"><FONT face="굴림">
										<asp:label id="Label91" runat="server"></asp:label>&nbsp;</FONT></td>
							</tr>
							<tr>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30"><asp:label id="Label25" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30"><asp:label id="Label26" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30"><asp:label id="Label27" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" colSpan="2" height="30"><asp:label id="Label28" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30"><asp:label id="Label29" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="right" height="30"><asp:label id="Label30" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="right" height="30"><asp:label id="Label31" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" colSpan="3" height="30"><asp:label id="Label32" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-TOP: black 1px solid" vAlign="middle" align="center" height="30"><FONT face="굴림">
										<asp:label id="Label92" runat="server"></asp:label>&nbsp;</FONT></td>
							</tr>
							<tr>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30"><asp:label id="Label33" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30"><asp:label id="Label34" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30"><asp:label id="Label35" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" colSpan="2" height="30"><asp:label id="Label36" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30"><asp:label id="Label37" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="right" height="30"><asp:label id="Label38" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="right" height="30"><asp:label id="Label39" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" colSpan="3" height="30"><asp:label id="Label40" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-TOP: black 1px solid" vAlign="middle" align="center" height="30"><FONT face="굴림">
										<asp:label id="Label93" runat="server"></asp:label>&nbsp;</FONT></td>
							</tr>
							<tr>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30"><asp:label id="Label41" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30"><asp:label id="Label42" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30"><asp:label id="Label43" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" colSpan="2" height="30"><asp:label id="Label44" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30"><asp:label id="Label45" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="right" height="30"><asp:label id="Label46" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="right" height="30"><asp:label id="Label47" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" colSpan="3" height="30"><asp:label id="Label48" runat="server"></asp:label>&nbsp;</td>
								<td style="BORDER-TOP: black 1px solid" vAlign="middle" align="center" height="30"><FONT face="굴림">
										<asp:label id="Label94" runat="server"></asp:label>&nbsp;</FONT></td>
							</tr>
							<tr>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30">
									<asp:Label id="Label49" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30">
									<asp:Label id="Label50" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30">
									<asp:Label id="Label51" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" colSpan="2" height="30">
									<asp:Label id="Label52" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30">
									<asp:Label id="Label53" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="right" height="30">
									<asp:Label id="Label54" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="right" height="30">
									<asp:Label id="Label55" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" colSpan="3" height="30">
									<asp:Label id="Label56" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-TOP: black 1px solid" vAlign="middle" align="center" height="30"><FONT face="굴림">
										<asp:label id="Label95" runat="server"></asp:label>&nbsp;</FONT></td>
							</tr>
							<tr>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30">
									<asp:Label id="Label57" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30">
									<asp:Label id="Label58" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30">
									<asp:Label id="Label59" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" colSpan="2" height="30">
									<asp:Label id="Label60" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30">
									<asp:Label id="Label61" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="right" height="30">
									<asp:Label id="Label62" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="right" height="30">
									<asp:Label id="Label63" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" colSpan="3" height="30">
									<asp:Label id="Label64" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-TOP: black 1px solid" vAlign="middle" align="center" height="30"><FONT face="굴림">
										<asp:label id="Label96" runat="server"></asp:label>&nbsp;</FONT></td>
							</tr>
							<tr>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30">
									<asp:Label id="Label65" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30">
									<asp:Label id="Label66" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30">
									<asp:Label id="Label67" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" colSpan="2" height="30">
									<asp:Label id="Label68" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30">
									<asp:Label id="Label69" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="right" height="30">
									<asp:Label id="Label70" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="right" height="30">
									<asp:Label id="Label71" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" colSpan="3" height="30">
									<asp:Label id="Label72" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-TOP: black 1px solid" align="center" height="30"><FONT face="굴림">
										<asp:label id="Label97" runat="server"></asp:label>&nbsp;</FONT></td>
							</tr>
							<tr>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30">
									<asp:Label id="Label73" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30">
									<asp:Label id="Label74" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30">
									<asp:Label id="Label75" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" colSpan="2" height="30">
									<asp:Label id="Label76" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30">
									<asp:Label id="Label77" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="right" height="30">
									<asp:Label id="Label78" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="right" height="30">
									<asp:Label id="Label79" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" colSpan="3" height="30">
									<asp:Label id="Label80" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-TOP: black 1px solid" align="center" height="30"><FONT face="굴림">
										<asp:label id="Label98" runat="server"></asp:label>&nbsp;</FONT></td>
							</tr>
							<tr>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30">
									<asp:Label id="Label81" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30">
									<asp:Label id="Label82" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30">
									<asp:Label id="Label83" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" colSpan="2" height="30">
									<asp:Label id="Label84" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" height="30">
									<asp:Label id="Label85" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="right" height="30">
									<asp:Label id="Label86" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="right" height="30">
									<asp:Label id="Label87" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid" vAlign="middle"
									align="center" colSpan="3" height="30">
									<asp:Label id="Label88" runat="server"></asp:Label>&nbsp;</td>
								<td style="BORDER-TOP: black 1px solid" vAlign="middle" align="center" height="30"><FONT face="굴림">
										<asp:label id="Label99" runat="server"></asp:label>&nbsp;</FONT></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-LEFT: black 1px solid; PADDING-TOP: 10px; BORDER-BOTTOM: black 1px solid"
						vAlign="top" colSpan="12" height="200">&nbsp; 특기사항</td>
				</tr>
				<tr>
					<td style="BORDER-RIGHT: black 1px solid; PADDING-LEFT: 10px; BORDER-LEFT: black 1px solid; PADDING-TOP: 5px; BORDER-BOTTOM: black 1px solid"
						colSpan="12">특약 : 1. 납품장소는 당사의지정된 장소로 한다.
						<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 2.기타 명세되지 아니한 것은 일반 상례에 따른다.</p>
					</td>
				</tr>
				<tr>
					<td colSpan="12" height="15">
						<table id="Table5" width="710" border="0" cellspacing="0" cellpadding="0" style="FONT-SIZE: 9pt">
							<tr>
								<td style="PADDING-LEFT: 3px; PADDING-TOP: 3px" align="left">QF061-2(Rev.0)</td>
								<td style="PADDING-TOP: 3px" align="center">(주)명림</td>
								<td style="PADDING-RIGHT: 3px; PADDING-TOP: 3px" align="right">A4(210X297mm)</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
