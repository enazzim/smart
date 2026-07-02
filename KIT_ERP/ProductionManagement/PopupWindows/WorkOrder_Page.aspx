<%@ Page language="c#" Codebehind="WorkOrder_Page.aspx.cs" AutoEventWireup="false" Inherits="mlim_ERP.ProductionManagement.PopupWindows.WorkOrder_Page" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WorkOrder_Page</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--		
		function test()
		{
			alert('선택한 항목들 중 다른 품목이 존재합니다');	
			window.opener.SearchButton();self.close();		
		}

--></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" width="710" border="0" cellpadding="0" cellspacing="0" style="BORDER-BOTTOM: black thin solid; POSITION: absolute; BORDER-LEFT: black thin solid; BORDER-TOP: black thin solid; TOP: 8px; BORDER-RIGHT: black thin solid; LEFT: 8px">
				<TR>
					<TD height="80">
						<table id="Table7" border="0" cellpadding="0" cellspacing="0" width="100%">
							<tr>
								<td width="5%" rowspan="2" style="BORDER-RIGHT: black thin solid" align="center" height="25">
									<P><FONT face="굴림">생</FONT></P>
									<P><FONT face="굴림">산</FONT></P>
								</td>
								<td width="10%" style="BORDER-BOTTOM: black thin solid; BORDER-RIGHT: black thin solid"
									vAlign="middle" align="center" height="25"><FONT face="굴림">과 장</FONT></td>
								<td width="10%" style="BORDER-BOTTOM: black thin solid" vAlign="middle" align="center"
									height="25"><FONT face="굴림">부 장</FONT></td>
								<td width="50%" rowspan="2" style="BORDER-LEFT: black thin solid; BORDER-RIGHT: black thin solid"
									height="25">
									<table id="Table8" border="0" cellpadding="0" cellspacing="0" width="100%">
										<tr>
											<td height="55" vAlign="middle" align="center" width="100%"><FONT style="FONT-SIZE: 30pt; FONT-WEIGHT: bold" face="굴림">작업지시서</FONT></td>
										</tr>
										<tr>
											<td width="100%" vAlign="bottom" align="left" height="25"><FONT face="굴림">&nbsp;규격 :
													<asp:Label id="Label20" runat="server">&nbsp;</asp:Label>
													<asp:Label style="Z-INDEX: 0" id="Label22" runat="server">&nbsp;</asp:Label>재질 
													:
													<asp:Label style="Z-INDEX: 0" id="Label21" runat="server">&nbsp;</asp:Label>
												</FONT>
											</td>
										</tr>
									</table>
								</td>
								<td width="5%" rowspan="2" style="BORDER-RIGHT: black thin solid" align="center" height="25">
									<P><FONT face="굴림">관</FONT></P>
									<P><FONT face="굴림">리</FONT></P>
								</td>
								<td width="10%" style="BORDER-BOTTOM: black thin solid; BORDER-RIGHT: black thin solid"
									align="center" height="25"><FONT face="굴림">과 장</FONT></td>
								<td width="10%" style="BORDER-BOTTOM: black thin solid" align="center" height="25"><FONT face="굴림">부 
										장</FONT></td>
							</tr>
							<tr>
								<td width="10%" style="BORDER-RIGHT: black thin solid" height="55">&nbsp;</td>
								<td width="10%" height="55">&nbsp;</td>
								<td width="10%" style="BORDER-RIGHT: black thin solid" height="55">&nbsp;</td>
								<td width="10%" height="55">&nbsp;</td>
							</tr>
						</table>
					</TD>
				</TR>
				<tr>
					<td height="35">
						<table id="Table3" height="35" width="100%" border="0" cellspacing="0" cellpadding="0"
							style="BORDER-BOTTOM: black thin solid; BORDER-TOP: black thin solid">
							<tr>
								<td width="15%" height="35" align="center" style="BORDER-RIGHT: black thin solid">품목분류</td>
								<td width="15%" align="center" style="BORDER-RIGHT: black thin solid">
									<asp:Label id="Label1" runat="server">&nbsp;</asp:Label></td>
								<td width="5%" align="center" style="BORDER-RIGHT: black thin solid">기종</td>
								<td width="15%" align="center" style="BORDER-RIGHT: black thin solid">
									<asp:Label id="Label2" runat="server">&nbsp;</asp:Label></td>
								<td width="5%" align="center" style="BORDER-RIGHT: black thin solid">품명</td>
								<td width="15%" align="center" style="BORDER-RIGHT: black thin solid">
									<asp:Label id="Label3" runat="server">&nbsp;</asp:Label>
								</td>
								<td width="10%" align="center" style="BORDER-RIGHT: black thin solid">품목번호</td>
								<td width="20%" align="center">
									<asp:Label id="Label4" runat="server">&nbsp;</asp:Label>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="center">
						<table id="Table4" width="100%" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td width="20%" height="30" align="center" style="BORDER-BOTTOM: black thin solid; BORDER-RIGHT: black thin solid">작업지시일자</td>
								<td width="15%" align="center" style="BORDER-BOTTOM: black thin solid; BORDER-RIGHT: black thin solid">
									<asp:Label id="Label5" runat="server">&nbsp;</asp:Label>
								</td>
								<td width="20%" align="center" style="BORDER-BOTTOM: black thin solid; BORDER-RIGHT: black thin solid">완료요구일자</td>
								<td width="15%" align="center" style="BORDER-BOTTOM: black thin solid; BORDER-RIGHT: black thin solid">
									<asp:Label id="Label6" runat="server">&nbsp;</asp:Label></td>
								<td width="20%" align="center" style="BORDER-BOTTOM: black thin solid; BORDER-RIGHT: black thin solid">작업지시수량</td>
								<td width="10%" align="center" style="BORDER-BOTTOM: black thin solid">
									<asp:Label id="Label7" runat="server">&nbsp;</asp:Label>
								</td>
							</tr>
						</table>
						<table id="Table5" width="100%" border="0" cellspacing="0" cellpadding="0">
							<tr align="center">
								<td height="25" colspan="7">롯 트 분 리 및 불 량 발 생 기 록</td>
							</tr>
							<tr align="center">
								<td width="5%" height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">공정</td>
								<td width="15%" height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">구분</td>
								<td width="10%" height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">수량</td>
								<td width="10%" height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">작업자</td>
								<td width="30%" height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">사&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
									유</td>
								<td width="25%" height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">LOT분리 
									NO</td>
								<td width="5%" height="25" style="BORDER-TOP: black thin solid">비고</td>
							</tr>
							<tr>
								<td height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"><FONT face="굴림">&nbsp;</FONT>
								</td>
								<td height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"><FONT face="굴림">&nbsp;</FONT>
								</td>
								<td height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"><FONT face="굴림">&nbsp;</FONT>
								</td>
								<td height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"><FONT face="굴림">&nbsp;</FONT>
								</td>
								<td height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"><FONT face="굴림">&nbsp;</FONT>
								</td>
								<td height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"><FONT face="굴림">&nbsp;</FONT>
								</td>
								<td height="25" style="BORDER-TOP: black thin solid">&nbsp;
								</td>
							</tr>
							<tr>
								<td height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"><FONT face="굴림">&nbsp;</FONT>
								</td>
								<td height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"><FONT face="굴림">&nbsp;</FONT>
								</td>
								<td height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"><FONT face="굴림">&nbsp;</FONT>
								</td>
								<td height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"><FONT face="굴림">&nbsp;</FONT>
								</td>
								<td height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"><FONT face="굴림">&nbsp;</FONT>
								</td>
								<td height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"><FONT face="굴림">&nbsp;</FONT>
								</td>
								<td height="25" style="BORDER-TOP: black thin solid"><FONT face="굴림">&nbsp;</FONT>
								</td>
							</tr>
							<tr>
								<td height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"><FONT face="굴림">&nbsp;</FONT>
								</td>
								<td height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"><FONT face="굴림">&nbsp;</FONT>
								</td>
								<td height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"><FONT face="굴림">&nbsp;</FONT>
								</td>
								<td height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"><FONT face="굴림">&nbsp;</FONT>
								</td>
								<td height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"><FONT face="굴림">&nbsp;</FONT>
								</td>
								<td height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"><FONT face="굴림">&nbsp;</FONT>
								</td>
								<td height="25" style="BORDER-TOP: black thin solid"><FONT face="굴림">&nbsp;</FONT>
								</td>
							</tr>
						</table>
						<table id="Table6" width="100%" border="0" cellspacing="0" cellpadding="0" style="BORDER-BOTTOM: black thin solid">
							<tr align="center">
								<td width="7%" height="25" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">공정<br>
									번호</td>
								<td width="13%" height="25" align="center" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">공정명</td>
								<td width="11%" rowspan="2" height="25" align="center" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">장비별<br>
									관리번호</td>
								<td width="8%" rowspan="2" height="25" align="center" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">작업자</td>
								<td width="8%" rowspan="2" height="25" align="center" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">작업일</td>
								<td width="11%" height="25" align="center" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">시작시간</td>
								<td width="11%" height="25" align="center" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">비작업시간</td>
								<td width="10%" rowspan="2" height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">작업수량</td>
								<td width="10%" height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">합격수량</td>
								<td width="5%" rowspan="2" height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">입력<br>
									확인</td>
								<td rowspan="2" height="25" style="BORDER-TOP: black thin solid">비고</td>
							</tr>
							<tr>
								<td height="25" align="center" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">작업장</td>
								<td align="center" height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">종료시간</td>
								<td align="center" height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">비작업사유</td>
								<td align="center" height="25" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">불합격수량</td>
							</tr>
							<TR>
								<td height="15" rowspan="10" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"
									align="center">
									<asp:Label id="Label8" runat="server">&nbsp;</asp:Label></td>
								<td height="15" rowspan="5" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"
									align="center">
									<asp:Label id="Label9" runat="server">&nbsp;</asp:Label></td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" rowspan="5" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"
									align="center">
									<asp:Label id="Label10" runat="server">&nbsp;</asp:Label></td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" rowspan="10" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"
									align="center">
									<asp:Label id="Label11" runat="server">&nbsp;</asp:Label></td>
								<td height="15" rowspan="5" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"
									align="center">
									<asp:Label id="Label12" runat="server">&nbsp;</asp:Label></td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" rowspan="5" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"
									align="center">
									<asp:Label id="Label13" runat="server">&nbsp;</asp:Label></td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" rowspan="10" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"
									align="center">
									<asp:Label id="Label14" runat="server">&nbsp;</asp:Label></td>
								<td height="15" rowspan="5" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"
									align="center">
									<asp:Label id="Label15" runat="server">&nbsp;</asp:Label></td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" rowspan="5" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"
									align="center">
									<asp:Label id="Label16" runat="server">&nbsp;</asp:Label></td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" rowspan="10" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"
									align="center">&nbsp;
									<asp:Label id="Label17" runat="server">&nbsp;</asp:Label></td>
								<td height="15" rowspan="5" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"
									align="center">&nbsp;
									<asp:Label id="Label18" runat="server">&nbsp;</asp:Label></td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" rowspan="5" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid"
									align="center">
									<asp:Label id="Label19" runat="server">&nbsp;</asp:Label>&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" rowspan="2" style="BORDER-TOP: black thin solid">&nbsp;</td>
							</TR>
							<TR>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
								<td height="15" style="BORDER-TOP: black thin solid; BORDER-RIGHT: black thin solid">&nbsp;</td>
							</TR>
						</table>
						<br>
						(주)명림
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
