<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v1.1, Version=1.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="BusinessDetailedStatement.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BusinessManagement.PopUp.BusinessDetailedStatement" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>거래명세표</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../StyleSheet2.css" type="text/css" rel="stylesheet">
  </HEAD>
	<body MS_POSITIONING="GridLayout">
		<table cellspacing="0" width="650" height="460" style="BORDER-RIGHT:black 1px solid; BORDER-TOP:black 1px solid; BORDER-LEFT:black 1px solid; BORDER-BOTTOM:black 1px solid">
			<tr>
				<td colspan="2" height="60" style="BORDER-BOTTOM:black 1px solid">
					<table width="100%" border="0">
						<tr>
							<td width="30%" rowspan="2" valign="bottom">일 자 :
							</td>
							<td width="40%" style="FONT-SIZE: 20pt"><div align="center"><span class="style1">거 래 명 세 표 </span>
								</div>
							</td>
							<td width="30%" rowspan="2" valign="bottom">거래명세표번호 :
							</td>
						</tr>
						<tr>
							<td><div align="center"><span class="style1"><span class="style2">(공급자 보관용)</span></span></div>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td width="350" height="100">
					<table border="1" cellspacing="0" bordercolordark="white" bordercolorlight="black" width="100%"
						height="100%">
						<tr>
							<td width="5%" rowspan="4"><p align="center">공<br>
									급<br>
									자
								</p>
							</td>
							<td height="25%" width="10%"><div align="center">등록<br>
									번호</div>
							</td>
							<td colspan="3"><div align="center">
									<asp:Label id="Label1" runat="server">&nbsp;</asp:Label></div>
							</td>
						</tr>
						<tr>
							<td height="25%"><div align="center">상호</div>
							</td>
							<td width="43%"><div align="center">
									<asp:Label id="Label2" runat="server">&nbsp;</asp:Label></div>
							</td>
							<td height="20" width="6%"><div align="center">성<br>
									명</div>
							</td>
							<td width="36%"><div align="center">
									<asp:Label id="Label3" runat="server" DESIGNTIMEDRAGDROP="1428">&nbsp;</asp:Label></div>
							</td>
						</tr>
						<tr>
							<td><div align="center">주소</div>
							</td>
							<td colspan="3"><div align="center">
									<DIV align="center">
										<asp:Label id="Label4" runat="server" DESIGNTIMEDRAGDROP="1430">&nbsp;</asp:Label></DIV>
								</div>
							</td>
						</tr>
						<tr>
							<td height="25%"><div align="center">업태</div>
							</td>
							<td><div align="center">
									<DIV align="center">
										<asp:Label id="Label5" runat="server" DESIGNTIMEDRAGDROP="1432">&nbsp;</asp:Label></DIV>
								</div>
							</td>
							<td><div align="center">종<br>
									목</div>
							</td>
							<td><div align="center">
									<DIV align="center">
										<asp:Label id="Label6" runat="server" DESIGNTIMEDRAGDROP="1434">&nbsp;</asp:Label></DIV>
								</div>
							</td>
						</tr>
					</table>
				</td>
				<td width="350" height="100">
					<table border="1" cellspacing="0" bordercolordark="white" bordercolorlight="black" width="100%"
						height="100%">
						<tr>
							<td width="5%" rowspan="4"><p align="center">공<br>
									급<br>
									받<br>
									는<br>
									자<br>
								</p>
							</td>
							<td height="25%" width="10%"><div align="center">등록<br>
									번호</div>
							</td>
							<td colspan="3"><div align="center">
									<DIV align="center">
										<asp:Label id="Label7" runat="server" DESIGNTIMEDRAGDROP="1436">&nbsp;</asp:Label></DIV>
								</div>
							</td>
						</tr>
						<tr>
							<td height="25%"><div align="center">상호</div>
							</td>
							<td width="43%"><div align="center">
									<DIV align="center">
										<asp:Label id="Label8" runat="server" DESIGNTIMEDRAGDROP="1438">&nbsp;</asp:Label></DIV>
								</div>
							</td>
							<td height="20" width="6%"><div align="center">성<br>
									명</div>
							</td>
							<td width="36%"><div align="center">
									<DIV align="center">
										<asp:Label id="Label9" runat="server" DESIGNTIMEDRAGDROP="1440">&nbsp;</asp:Label></DIV>
								</div>
							</td>
						</tr>
						<tr>
							<td height="25"><div align="center">주소</div>
							</td>
							<td height="25" colspan="3"><div align="center">
									<DIV align="center">
										<asp:Label id="Label10" runat="server" DESIGNTIMEDRAGDROP="1442">&nbsp;</asp:Label></DIV>
								</div>
							</td>
						</tr>
						<tr>
							<td height="25%"><div align="center">업태</div>
							</td>
							<td><div align="center">
									<DIV align="center">
										<asp:Label id="Label11" runat="server" DESIGNTIMEDRAGDROP="1444">&nbsp;</asp:Label></DIV>
								</div>
							</td>
							<td><div align="center">종<br>
									목</div>
							</td>
							<td><div align="center">
									<DIV align="center">
										<asp:Label id="Label12" runat="server" DESIGNTIMEDRAGDROP="1446">&nbsp;</asp:Label></DIV>
								</div>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td colspan="2" height="230">
					<table border="1" cellspacing="0" bordercolordark="white" bordercolorlight="black" width="100%"
						height="100%">
						<tr>
							<td width="10"><div align="center">순번</div>
							</td>
							<td width="60"><div align="center">품번</div>
							</td>
							<td><div align="center">품명 및 규격
								</div>
							</td>
							<td width="40"><div align="center">수량<br>
									중량</div>
							</td>
							<td width="17" style="WIDTH: 17px"><div align="center">단<br>
									위</div>
							</td>
							<td width="60"><div align="center">단 가
								</div>
							</td>
							<td width="70"><div align="center">공급가액</div>
							</td>
							<td width="60"><div align="center">세액</div>
							</td>
							<td width="60"><div align="center">비고</div>
							</td>
						</tr>
						<tr>
							<td style="HEIGHT: 21px">&nbsp;
								<asp:Label id="Label25" runat="server" DESIGNTIMEDRAGDROP="1991">&nbsp</asp:Label></td>
							<td style="HEIGHT: 21px" align="left"><FONT style="FONT-SIZE: 5pt" face="굴림">&nbsp;&nbsp;&nbsp;</FONT>
								<asp:Label id="Label26" runat="server">&nbsp</asp:Label></td>
							<td style="HEIGHT: 21px">&nbsp;
								<asp:Label id="Label27" runat="server" DESIGNTIMEDRAGDROP="1993">&nbsp</asp:Label></td>
							<td style="HEIGHT: 21px" align="left">&nbsp;
								<asp:Label id="Label28" runat="server" DESIGNTIMEDRAGDROP="1994"></asp:Label></td>
							<td style="WIDTH: 17px; HEIGHT: 21px" align="center">&nbsp;
								<asp:Label id="Label29" runat="server" DESIGNTIMEDRAGDROP="1996"></asp:Label></td>
							<td style="HEIGHT: 21px" align="right">&nbsp;
								<asp:Label id="Label30" runat="server" DESIGNTIMEDRAGDROP="1997"></asp:Label></td>
							<td style="HEIGHT: 21px" align="right">&nbsp;
								<asp:Label id="Label31" runat="server" DESIGNTIMEDRAGDROP="1998"></asp:Label></td>
							<td style="HEIGHT: 21px" align="right">&nbsp;
								<asp:Label id="Label32" runat="server" DESIGNTIMEDRAGDROP="1999"></asp:Label></td>
							<td>&nbsp;</td>
						</tr>
						<tr>
							<td>&nbsp;
								<asp:Label id="Label33" runat="server" DESIGNTIMEDRAGDROP="2013">&nbsp</asp:Label></td>
							<td align="left">&nbsp;
								<asp:Label id="Label34" runat="server" DESIGNTIMEDRAGDROP="2014">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label35" runat="server" DESIGNTIMEDRAGDROP="2015">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label36" runat="server" DESIGNTIMEDRAGDROP="2016"></asp:Label></td>
							<td style="WIDTH: 17px" align="center">&nbsp;
								<asp:Label id="Label37" runat="server" DESIGNTIMEDRAGDROP="2017"></asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label38" runat="server" DESIGNTIMEDRAGDROP="2018"></asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label39" runat="server" DESIGNTIMEDRAGDROP="2019"></asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label40" runat="server" DESIGNTIMEDRAGDROP="2020"></asp:Label></td>
							<td>&nbsp;</td>
						</tr>
						<tr>
							<td>&nbsp;
								<asp:Label id="Label41" runat="server" DESIGNTIMEDRAGDROP="2021">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label42" runat="server" DESIGNTIMEDRAGDROP="2022">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label43" runat="server" DESIGNTIMEDRAGDROP="2023">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label44" runat="server" DESIGNTIMEDRAGDROP="2024">&nbsp</asp:Label></td>
							<td style="WIDTH: 17px" align="center">&nbsp;
								<asp:Label id="Label45" runat="server" DESIGNTIMEDRAGDROP="2025"></asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label46" runat="server" DESIGNTIMEDRAGDROP="2026"></asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label47" runat="server" DESIGNTIMEDRAGDROP="2027"></asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label48" runat="server" DESIGNTIMEDRAGDROP="2028"></asp:Label></td>
							<td>&nbsp;</td>
						</tr>
						<tr>
							<td>&nbsp;
								<asp:Label id="Label49" runat="server" DESIGNTIMEDRAGDROP="2029">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label50" runat="server" DESIGNTIMEDRAGDROP="2030">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label51" runat="server" DESIGNTIMEDRAGDROP="2031">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label52" runat="server" DESIGNTIMEDRAGDROP="2032">&nbsp</asp:Label></td>
							<td style="WIDTH: 17px" align="center">&nbsp;
								<asp:Label id="Label53" runat="server" DESIGNTIMEDRAGDROP="2033"></asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label54" runat="server" DESIGNTIMEDRAGDROP="2034"></asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label55" runat="server" DESIGNTIMEDRAGDROP="2035"></asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label56" runat="server" DESIGNTIMEDRAGDROP="2036"></asp:Label></td>
							<td>&nbsp;</td>
						</tr>
						<tr>
							<td>&nbsp;
								<asp:Label id="Label57" runat="server" DESIGNTIMEDRAGDROP="2037">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label58" runat="server" DESIGNTIMEDRAGDROP="2038">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label59" runat="server" DESIGNTIMEDRAGDROP="2039">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label60" runat="server" DESIGNTIMEDRAGDROP="2040">&nbsp</asp:Label></td>
							<td style="WIDTH: 17px" align="center">&nbsp;
								<asp:Label id="Label61" runat="server" DESIGNTIMEDRAGDROP="2041"></asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label62" runat="server" DESIGNTIMEDRAGDROP="2042"></asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label63" runat="server" DESIGNTIMEDRAGDROP="2043"></asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label64" runat="server" DESIGNTIMEDRAGDROP="2044"></asp:Label></td>
							<td>&nbsp;</td>
						</tr>
						<tr>
							<td>&nbsp;
								<asp:Label id="Label65" runat="server" DESIGNTIMEDRAGDROP="2045">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label66" runat="server" DESIGNTIMEDRAGDROP="2046">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label67" runat="server" DESIGNTIMEDRAGDROP="2047">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label68" runat="server" DESIGNTIMEDRAGDROP="2048">&nbsp</asp:Label></td>
							<td style="WIDTH: 17px" align="center">&nbsp;
								<asp:Label id="Label69" runat="server" DESIGNTIMEDRAGDROP="2049"></asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label70" runat="server" DESIGNTIMEDRAGDROP="2050"></asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label71" runat="server" DESIGNTIMEDRAGDROP="2051"></asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label72" runat="server" DESIGNTIMEDRAGDROP="2052"></asp:Label></td>
							<td>&nbsp;</td>
						</tr>
						<tr>
							<td>&nbsp;
								<asp:Label id="Label73" runat="server" DESIGNTIMEDRAGDROP="2053">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label74" runat="server" DESIGNTIMEDRAGDROP="2054">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label75" runat="server" DESIGNTIMEDRAGDROP="2055">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label76" runat="server" DESIGNTIMEDRAGDROP="2056">&nbsp</asp:Label></td>
							<td style="WIDTH: 17px" align="center">&nbsp;
								<asp:Label id="Label77" runat="server" DESIGNTIMEDRAGDROP="2057"></asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label78" runat="server" DESIGNTIMEDRAGDROP="2058"></asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label79" runat="server" DESIGNTIMEDRAGDROP="2059"></asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label80" runat="server" DESIGNTIMEDRAGDROP="2060"></asp:Label></td>
							<td>&nbsp;</td>
						</tr>
						<tr>
							<td>&nbsp;
								<asp:Label id="Label81" runat="server" DESIGNTIMEDRAGDROP="2061">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label82" runat="server" DESIGNTIMEDRAGDROP="2062">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label83" runat="server" DESIGNTIMEDRAGDROP="2063">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label84" runat="server" DESIGNTIMEDRAGDROP="2064">&nbsp</asp:Label></td>
							<td style="WIDTH: 17px" align="center">&nbsp;
								<asp:Label id="Label85" runat="server" DESIGNTIMEDRAGDROP="2065"></asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label86" runat="server" DESIGNTIMEDRAGDROP="2066"></asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label87" runat="server" DESIGNTIMEDRAGDROP="2067"></asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label88" runat="server" DESIGNTIMEDRAGDROP="2068"></asp:Label></td>
							<td>&nbsp;</td>
						</tr>
						<tr>
							<td>&nbsp;
								<asp:Label id="Label89" runat="server" DESIGNTIMEDRAGDROP="2069">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label90" runat="server" DESIGNTIMEDRAGDROP="2070">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label91" runat="server" DESIGNTIMEDRAGDROP="2071">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label92" runat="server" DESIGNTIMEDRAGDROP="2072">&nbsp</asp:Label></td>
							<td style="WIDTH: 17px" align="center">&nbsp;
								<asp:Label id="Label93" runat="server" DESIGNTIMEDRAGDROP="2073"></asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label94" runat="server" DESIGNTIMEDRAGDROP="2074"></asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label95" runat="server" DESIGNTIMEDRAGDROP="2075"></asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label96" runat="server"></asp:Label></td>
							<td>&nbsp;</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td height="50" colspan="2">
					<table border="1" cellspacing="0" bordercolordark="white" bordercolorlight="black" width="100%"
						height="100%">
						<tr>
							<td width="15%">확 인
							</td>
							<td width="15%"><div align="right">인</div>
							</td>
							<td width="15%">합 계
							</td>
							<td colspan="3">&nbsp;
								<asp:Label id="Label97" runat="server"></asp:Label></td>
						</tr>
						<tr>
							<td>총 금 액
							</td>
							<td colspan="2" align="right" vAlign=bottom>
<igtxt:WebNumericEdit id=WebNumericEdit1 runat="server" BorderStyle="None" Height="20px"></igtxt:WebNumericEdit><FONT 
            face=굴림>&nbsp;&nbsp; </FONT>&nbsp;</td>
							<td width="10%">비고</td>
							<td width="10%">미결금액
							</td>
							<td>&nbsp;</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		<!--[줄긋기]-->
		<table cellpadding="0" cellspacing="0" border="0" width="650">
			<tr>
				<td height="20" style="BORDER-BOTTOM:#000000 1px dashed">&nbsp;
				</td>
			</tr>
			<tr>
				<td height="20">&nbsp;
				</td>
			</tr>
		</table>
		<!--[줄긋기]-->
		<table cellspacing="0" width="650" height="460" style="BORDER-RIGHT:black 1px solid; BORDER-TOP:black 1px solid; BORDER-LEFT:black 1px solid; BORDER-BOTTOM:black 1px solid">
			<tr>
				<td colspan="2" height="60" style="BORDER-BOTTOM:black 1px solid">
					<table width="100%" border="0">
						<tr>
							<td width="30%" rowspan="2" valign="bottom">일 자 :
							</td>
							<td width="40%" style="FONT-SIZE: 20pt"><div align="center"><span class="style1">거 래 명 세 표 </span>
								</div>
							</td>
							<td width="30%" rowspan="2" valign="bottom">거래명세표번호 :
							</td>
						</tr>
						<tr>
							<td><div align="center"><span class="style1"><span class="style2">(공급받는자 보관용)</span></span></div>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td width="350" height="100">
					<table border="1" cellspacing="0" bordercolordark="white" bordercolorlight="black" width="100%"
						height="100%">
						<tr>
							<td width="5%" rowspan="4"><p align="center">공<br>
									급<br>
									자
								</p>
							</td>
							<td height="25%" width="10%"><div align="center">등록<br>
									번호</div>
							</td>
							<td colspan="3"><div align="center">
									<DIV align="center">
										<asp:Label id="Label13" runat="server" DESIGNTIMEDRAGDROP="1448">&nbsp;</asp:Label></DIV>
								</div>
							</td>
						</tr>
						<tr>
							<td height="25%"><div align="center">상호</div>
							</td>
							<td width="43%"><div align="center">
									<DIV align="center">
										<asp:Label id="Label14" runat="server" DESIGNTIMEDRAGDROP="1450">&nbsp;</asp:Label></DIV>
								</div>
							</td>
							<td height="20" width="6%"><div align="center">성<br>
									명</div>
							</td>
							<td width="36%"><div align="center">
									<DIV align="center">
										<asp:Label id="Label15" runat="server" DESIGNTIMEDRAGDROP="1452">&nbsp;</asp:Label></DIV>
								</div>
							</td>
						</tr>
						<tr>
							<td height="25"><div align="center">주소</div>
							</td>
							<td colspan="3" height="25"><div align="center">
									<DIV align="center">
										<asp:Label id="Label16" runat="server" DESIGNTIMEDRAGDROP="1454">&nbsp;</asp:Label></DIV>
								</div>
							</td>
						</tr>
						<tr>
							<td height="25%"><div align="center">업태</div>
							</td>
							<td><div align="center">
									<DIV align="center">
										<asp:Label id="Label17" runat="server" DESIGNTIMEDRAGDROP="1456">&nbsp;</asp:Label></DIV>
								</div>
							</td>
							<td><div align="center">종<br>
									목</div>
							</td>
							<td><div align="center">
									<DIV align="center">
										<asp:Label id="Label18" runat="server" DESIGNTIMEDRAGDROP="1458">&nbsp;</asp:Label></DIV>
								</div>
							</td>
						</tr>
					</table>
				</td>
				<td width="350" height="100">
					<table border="1" cellspacing="0" bordercolordark="white" bordercolorlight="black" width="100%"
						height="100%">
						<tr>
							<td width="5%" rowspan="4"><p align="center">공<br>
									급<br>
									받<br>
									는<br>
									자<br>
								</p>
							</td>
							<td height="25%" width="10%"><div align="center">등록<br>
									번호</div>
							</td>
							<td colspan="3"><div align="center">
									<DIV align="center">
										<asp:Label id="Label19" runat="server" DESIGNTIMEDRAGDROP="1460">&nbsp;</asp:Label></DIV>
								</div>
							</td>
						</tr>
						<tr>
							<td height="25%"><div align="center">상호</div>
							</td>
							<td width="43%"><div align="center">
									<DIV align="center">
										<asp:Label id="Label20" runat="server" DESIGNTIMEDRAGDROP="1462">&nbsp;</asp:Label></DIV>
								</div>
							</td>
							<td height="20" width="6%"><div align="center">성<br>
									명</div>
							</td>
							<td width="36%"><div align="center">
									<DIV align="center">
										<asp:Label id="Label21" runat="server" DESIGNTIMEDRAGDROP="1464">&nbsp;</asp:Label></DIV>
								</div>
							</td>
						</tr>
						<tr>
							<td height="25"><div align="center">주소</div>
							</td>
							<td height="25" colspan="3"><div align="center">
									<DIV align="center">
										<asp:Label id="Label22" runat="server" DESIGNTIMEDRAGDROP="1466">&nbsp;</asp:Label></DIV>
								</div>
							</td>
						</tr>
						<tr>
							<td height="25%"><div align="center">업태</div>
							</td>
							<td><div align="center">
									<DIV align="center">
										<asp:Label id="Label23" runat="server" DESIGNTIMEDRAGDROP="1468">&nbsp;</asp:Label></DIV>
								</div>
							</td>
							<td><div align="center">종<br>
									목</div>
							</td>
							<td><div align="center">
									<DIV align="center">
										<asp:Label id="Label24" runat="server">&nbsp;</asp:Label></DIV>
								</div>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td colspan="2" height="230">
					<table border="1" cellspacing="0" bordercolordark="white" bordercolorlight="black" width="100%"
						height="100%">
						<tr>
							<td width="10"><div align="center">순번</div>
							</td>
							<td width="60"><div align="center">품번</div>
							</td>
							<td><div align="center">품명 및 규격
								</div>
							</td>
							<td width="40"><div align="center">수량<br>
									중량</div>
							</td>
							<td width="10"><div align="center">단<br>
									위</div>
							</td>
							<td width="60"><div align="center">단 가
								</div>
							</td>
							<td width="70"><div align="center">공급가액</div>
							</td>
							<td width="60"><div align="center">세액</div>
							</td>
							<td width="60"><div align="center">비고</div>
							</td>
						</tr>
						<tr>
							<td>&nbsp;
								<asp:Label id="Label98" runat="server" DESIGNTIMEDRAGDROP="4704">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label107" runat="server" DESIGNTIMEDRAGDROP="4713">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label116" runat="server" DESIGNTIMEDRAGDROP="4722">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label125" runat="server">Label</asp:Label></td>
							<td align="center">
								<asp:Label id="Label134" runat="server">Label</asp:Label>&nbsp;</td>
							<td align="right">&nbsp;
								<asp:Label id="Label143" runat="server">Label</asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label152" runat="server">Label</asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label161" runat="server">Label</asp:Label></td>
							<td>&nbsp;</td>
						</tr>
						<tr>
							<td>&nbsp;
								<asp:Label id="Label99" runat="server" DESIGNTIMEDRAGDROP="4705">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label108" runat="server" DESIGNTIMEDRAGDROP="4714">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label117" runat="server" DESIGNTIMEDRAGDROP="4723">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label126" runat="server">Label</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label135" runat="server">Label</asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label144" runat="server">Label</asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label153" runat="server">Label</asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label162" runat="server">Label</asp:Label></td>
							<td>&nbsp;</td>
						</tr>
						<tr>
							<td>&nbsp;
								<asp:Label id="Label100" runat="server" DESIGNTIMEDRAGDROP="4706">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label109" runat="server" DESIGNTIMEDRAGDROP="4715">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label118" runat="server" DESIGNTIMEDRAGDROP="4724">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label127" runat="server">Label</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label136" runat="server">Label</asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label145" runat="server">Label</asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label154" runat="server">Label</asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label163" runat="server">Label</asp:Label></td>
							<td>&nbsp;</td>
						</tr>
						<tr>
							<td>&nbsp;
								<asp:Label id="Label101" runat="server" DESIGNTIMEDRAGDROP="4707">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label110" runat="server" DESIGNTIMEDRAGDROP="4716">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label119" runat="server" DESIGNTIMEDRAGDROP="4725">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label128" runat="server">Label</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label137" runat="server">Label</asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label146" runat="server">Label</asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label155" runat="server">Label</asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label164" runat="server">Label</asp:Label></td>
							<td>&nbsp;</td>
						</tr>
						<tr>
							<td>&nbsp;
								<asp:Label id="Label102" runat="server" DESIGNTIMEDRAGDROP="4708">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label111" runat="server" DESIGNTIMEDRAGDROP="4717">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label120" runat="server" DESIGNTIMEDRAGDROP="4726">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label129" runat="server" Width="100px">Label</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label138" runat="server">Label</asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label147" runat="server">Label</asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label156" runat="server">Label</asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label165" runat="server">Label</asp:Label></td>
							<td>&nbsp;</td>
						</tr>
						<tr>
							<td>&nbsp;
								<asp:Label id="Label103" runat="server" DESIGNTIMEDRAGDROP="4709">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label112" runat="server" DESIGNTIMEDRAGDROP="4718">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label121" runat="server" DESIGNTIMEDRAGDROP="4727">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label130" runat="server">Label</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label139" runat="server">Label</asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label148" runat="server">Label</asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label157" runat="server">Label</asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label166" runat="server">Label</asp:Label></td>
							<td>&nbsp;</td>
						</tr>
						<tr>
							<td>&nbsp;
								<asp:Label id="Label104" runat="server" DESIGNTIMEDRAGDROP="4710">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label113" runat="server" DESIGNTIMEDRAGDROP="4719">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label122" runat="server" DESIGNTIMEDRAGDROP="4728">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label131" runat="server">Label</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label140" runat="server">Label</asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label149" runat="server">Label</asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label158" runat="server">Label</asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label167" runat="server">Label</asp:Label></td>
							<td>&nbsp;</td>
						</tr>
						<tr>
							<td>&nbsp;
								<asp:Label id="Label105" runat="server" DESIGNTIMEDRAGDROP="4711">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label114" runat="server" DESIGNTIMEDRAGDROP="4720">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label123" runat="server" DESIGNTIMEDRAGDROP="4729">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label132" runat="server">Label</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label141" runat="server">Label</asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label150" runat="server">Label</asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label159" runat="server">Label</asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label168" runat="server">Label</asp:Label></td>
							<td>&nbsp;</td>
						</tr>
						<tr>
							<td>&nbsp;
								<asp:Label id="Label106" runat="server">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label115" runat="server">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label124" runat="server">&nbsp</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label133" runat="server">Label</asp:Label></td>
							<td>&nbsp;
								<asp:Label id="Label142" runat="server">Label</asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label151" runat="server">Label</asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label160" runat="server">Label</asp:Label></td>
							<td align="right">&nbsp;
								<asp:Label id="Label169" runat="server">Label</asp:Label></td>
							<td>&nbsp;</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td height="50" colspan="2">
					<table border="1" cellspacing="0" bordercolordark="white" bordercolorlight="black" width="100%"
						height="100%">
						<tr>
							<td width="15%">확 인
							</td>
							<td width="15%"><div align="right">인</div>
							</td>
							<td width="15%">합 계
							</td>
							<td colspan="3">&nbsp;
								<asp:Label id="Label170" runat="server" Visible="False">Label</asp:Label></td>
						</tr>
						<tr>
							<td>총 금 액
							</td>
							<td colspan="2" align="right" vAlign=bottom>
<igtxt:WebNumericEdit id=WebNumericEdit2 runat="server" BorderStyle="None" Height="18px"></igtxt:WebNumericEdit><FONT 
            face=굴림>&nbsp;&nbsp; </FONT>&nbsp;</td>
							<td width="10%">비고</td>
							<td width="10%">미결금액
							</td>
							<td>&nbsp;</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</body>
</HTML>
