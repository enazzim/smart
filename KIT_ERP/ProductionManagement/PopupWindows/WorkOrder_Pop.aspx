<%@ Page language="c#" Codebehind="WorkOrder_Pop.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.PopupWindows.WorkOrder_Pop" codePage="949" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML xmlns:o = 
"urn:schemas-microsoft-com:office:office" xmlns:x = 
"urn:schemas-microsoft-com:office:excel">
  <HEAD>
		<title>WorkOrder_Pop</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta content="Excel.Sheet" name="ProgId">
		<meta content="Microsoft Excel 11" name="Generator">
		<LINK id="Main-File" href="WorkOrder_Pop.aspx" rel="Main-File">
		<LINK href="filelist.xml" rel="File-List">
		<LINK href="editdata.mso" rel="Edit-Time-Data">
		<LINK href="../../StyleSheet2.css" type="text/css" rel="stylesheet">
  </HEAD>
	<body ms_positioning="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; WIDTH: 511pt; POSITION: absolute; TOP: 8px; BORDER-COLLAPSE: collapse"
				cellSpacing="0" cellPadding="0" width="682" border="0" x:str>
				<COLGROUP>
					<COL style="WIDTH: 33pt; mso-width-source: userset; mso-width-alt: 1251" width="44">
					<COL style="WIDTH: 69pt; mso-width-source: userset; mso-width-alt: 2616" width="92">
					<COL style="WIDTH: 93pt; mso-width-source: userset; mso-width-alt: 3527" width="124">
					<COL style="WIDTH: 74pt; mso-width-source: userset; mso-width-alt: 2816" width="99">
					<COL style="WIDTH: 88pt; mso-width-source: userset; mso-width-alt: 3328" width="117">
					<COL style="WIDTH: 75pt; mso-width-source: userset; mso-width-alt: 2844" width="100">
					<COL style="WIDTH: 77pt; mso-width-source: userset; mso-width-alt: 2929" width="103">
					<COL style="WIDTH: 2pt; mso-width-source: userset; mso-width-alt: 85" width="3">
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset">
						<TD class="xl26" style="BORDER-TOP: windowtext 0px solid; BORDER-LEFT-WIDTH: 0.5pt; FONT-SIZE: 0.5pt; BORDER-LEFT-COLOR: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent; BORDER-RIGHT-WIDTH: 0.5pt; BORDER-RIGHT-COLOR: windowtext"
							align="center" colSpan="8" height="1" width="1"></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext 0px solid; FONT-SIZE: 9pt; BORDER-LEFT: windowtext 0.5pt solid; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
							align="center" height="26"><FONT face="돋움" size="2">구분</FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							align="center" width="80"><FONT face="돋움" size="2">지시번호</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							align="left" width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							align="center" width="80"><FONT face="돋움" size="2">지시일자</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							align="center" width="100"><FONT face="돋움" size="2"><asp:label id="Label2" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							align="center" width="80"><FONT face="돋움" size="2">제품번호</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							align="center" width="100"><FONT face="돋움" size="2"><asp:label id="Label3" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							align="center" width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 80.4pt; BACKGROUND-COLOR: transparent"
							align="center" height="104" rowSpan="4">
							<P><FONT face="돋움" size="2">작업</FONT></P>
							<P><FONT face="돋움" size="2">표준</FONT></P>
						</TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;작업장명</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label10" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;작업자</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label9" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;품목번호</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label4" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
							height="26" width="80"><FONT face="돋움" size="2">&nbsp;품목명</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label11" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;공정명</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label8" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;표준시간</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label5" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
							height="26" width="80"><FONT face="돋움" size="2">&nbsp;사용공구1</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label12" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;사용치구1</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label7" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;사용공구2</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label6" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
							height="26" width="80"><FONT face="돋움" size="2">&nbsp;사용치구2</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label13" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움"></FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움"></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움"></FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 60.3pt; BACKGROUND-COLOR: transparent"
							align="center" height="78" rowSpan="3"><FONT face="돋움" size="2">지시<BR>
								및<BR>
								결과</FONT></TD>
						<TD class="xl29" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;작업계획량</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label14" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl29" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;이전완료량</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label19" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl29" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;금일완료량</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
							height="26" width="80"><FONT face="돋움" size="2">&nbsp;적합수량</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl29" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;시작시간</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl29" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;종료시간</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
							height="26" width="80"><FONT face="돋움" size="2">&nbsp;비작업시간1</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;비작업사유1</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움"></FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; FONT-SIZE: 10pt; BORDER-LEFT: windowtext 0.5pt solid; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 40.2pt; BACKGROUND-COLOR: transparent"
							align="center" height="52" rowSpan="2">
							<P><FONT face="돋움" size="2">검사</FONT><br>
								<FONT face="돋움" size="2">결과</FONT></P>
						</TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;부적합수량</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;검사판정</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;부적합현상</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
							height="26" width="80"><FONT style="FONT-SIZE: 10pt" face="돋움" size="2">&nbsp;부적합원인</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;세부내역</FONT></TD>
						<TD class="xl33" style="BORDER-RIGHT: black 0.5pt solid; BORDER-TOP: windowtext 0.5pt solid; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							colSpan="3" width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 18.75pt; mso-height-source: userset" height="25">
						<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 18.75pt; BACKGROUND-COLOR: transparent"
							align="center" height="25"><FONT face="돋움" size="2">&nbsp;비고</FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext 0.5pt solid; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							colSpan="6"><FONT face="돋움">
<asp:label id=Label1 runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 5.25pt; mso-height-source: userset" height="7">
						<TD class="xl30" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; HEIGHT: 5.25pt; BACKGROUND-COLOR: transparent"
							height="7"><FONT face="돋움"></FONT></TD>
						<TD class="xl31" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움"></FONT></TD>
						<TD class="xl30" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움"></FONT></TD>
						<TD class="xl31" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움"></FONT></TD>
						<TD class="xl30" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움"></FONT></TD>
						<TD class="xl31" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움"></FONT></TD>
						<TD class="xl30" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 5.25pt; mso-height-source: userset" height="7">
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; HEIGHT: 5.25pt; BACKGROUND-COLOR: transparent"
							height="7"><FONT face="돋움"></FONT></TD>
						<TD class="xl25" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움"></FONT></TD>
						<TD class="xl25" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움"></FONT></TD>
						<TD class="xl25" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset">
						<TD class="xl26" style="BORDER-TOP: windowtext 0px solid; BORDER-LEFT-WIDTH: 0.5pt; FONT-SIZE: 0.5pt; BORDER-LEFT-COLOR: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent; BORDER-RIGHT-WIDTH: 0.5pt; BORDER-RIGHT-COLOR: windowtext"
							align="center" colSpan="8" height="1" width="1"></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
							align="center" height="26"><FONT face="돋움" size="2">구분</FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							align="center" width="80"><FONT face="돋움" size="2">&nbsp;지시번호</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							align="center" width="80"><FONT face="돋움" size="2">&nbsp;지시일자</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							align="center" width="100"><FONT face="돋움" size="2"><asp:label id="Label28" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							align="center" width="80"><FONT face="돋움" size="2">&nbsp;제품번호</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							align="center" width="100"><FONT face="돋움" size="2"><asp:label id="Label29" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 80.4pt; BACKGROUND-COLOR: transparent"
							align="center" height="104" rowSpan="4">
							<P><FONT face="돋움" size="2">작업</FONT></P>
							<P><FONT face="돋움" size="2">표준</FONT></P>
						</TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;작업장명</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label30" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;작업자</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label31" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;품목번호</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label32" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
							height="26" width="80"><FONT face="돋움" size="2">&nbsp;품목명</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label33" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;공정명</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label34" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;표준시간</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label35" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
							height="26" width="80"><FONT face="돋움" size="2">&nbsp;사용공구1</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label36" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;사용치구1</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label37" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;사용공구2</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label38" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
							height="26" width="80"><FONT face="돋움" size="2">&nbsp;사용치구2</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label39" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움"></FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움"></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움"></FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 60.3pt; BACKGROUND-COLOR: transparent"
							align="center" height="78" rowSpan="3"><FONT face="돋움" size="2">&nbsp;지시<BR>
								및<BR>
								결과</FONT></TD>
						<TD class="xl29" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;작업계획량</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label40" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;이전완료량</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label41" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl29" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;금일완료량</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
							height="26" width="80"><FONT face="돋움" size="2">&nbsp;적합수량</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl29" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;시작시간</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;종료시간</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
							height="26" width="80"><FONT face="돋움" size="2">&nbsp;비작업시간1</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;비작업사유1</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움"></FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 40.2pt; BACKGROUND-COLOR: transparent"
							align="center" height="52" rowSpan="2">
							<P><FONT face="돋움" size="2">검사</FONT><br>
								<FONT face="돋움" size="2">결과</FONT></P>
						</TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;부적합수량</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;검사판정</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;부적합현상</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
							height="26" width="80"><FONT face="돋움" size="2">&nbsp;부적합원인</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;세부내역</FONT></TD>
						<TD class="xl33" style="BORDER-RIGHT: black 0.5pt solid; BORDER-TOP: windowtext 0.5pt solid; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							colSpan="3" width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 18pt; mso-height-source: userset" height="24">
						<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 18pt; BACKGROUND-COLOR: transparent"
							align="center" height="24"><FONT face="돋움" size="2">&nbsp;비고</FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext 0.5pt solid; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							colSpan="6" ><FONT face="돋움">
<asp:label id=Label15 runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 6.75pt; mso-height-source: userset" height="9">
						<TD class="xl30" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; HEIGHT: 6.75pt; BACKGROUND-COLOR: transparent"
							height="9"><FONT face="돋움"></FONT></TD>
						<TD class="xl31" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움"></FONT></TD>
						<TD class="xl30" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움"></FONT></TD>
						<TD class="xl31" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움"></FONT></TD>
						<TD class="xl30" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움"></FONT></TD>
						<TD class="xl31" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움"></FONT></TD>
						<TD class="xl30" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 6pt; mso-height-source: userset" height="8">
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; HEIGHT: 6pt; BACKGROUND-COLOR: transparent"
							height="8"><FONT face="돋움"></FONT></TD>
						<TD class="xl25" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움"></FONT></TD>
						<TD class="xl25" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움"></FONT></TD>
						<TD class="xl25" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset">
						<TD class="xl26" style="BORDER-TOP: windowtext 0px solid; BORDER-LEFT-WIDTH: 0.5pt; FONT-SIZE: 0.5pt; BORDER-LEFT-COLOR: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent; BORDER-RIGHT-WIDTH: 0.5pt; BORDER-RIGHT-COLOR: windowtext"
							align="center" colSpan="8" height="1" width="1"></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
							align="center" height="26"><FONT face="돋움" size="2">구분</FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							align="center" width="80"><FONT face="돋움" size="2">&nbsp;지시번호</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							align="center" width="80"><FONT face="돋움" size="2">&nbsp;지시일자</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							align="center" width="100"><FONT face="돋움" size="2"><asp:label id="Label54" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							align="center" width="80"><FONT face="돋움" size="2">&nbsp;제품번호</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							align="center" width="100"><FONT face="돋움" size="2"><asp:label id="Label55" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 80.4pt; BACKGROUND-COLOR: transparent"
							align="center" height="104" rowSpan="4">
							<P><FONT face="돋움" size="2">작업</FONT></P>
							<P><FONT face="돋움" size="2">표준</FONT></P>
						</TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;작업장명</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label56" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;작업자</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label57" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;품목번호</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label58" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
							height="26" width="80"><FONT face="돋움" size="2">&nbsp;품목명</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label61" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;공정명</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label60" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;표준시간</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label59" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
							height="26" width="80"><FONT face="돋움" size="2">&nbsp;사용공구1</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label64" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;사용치구1</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label63" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;사용공구2</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label62" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
							height="26" width="80"><FONT face="돋움" size="2">&nbsp;사용치구2</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label65" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움"></FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움"></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움"></FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 60.3pt; BACKGROUND-COLOR: transparent"
							align="center" height="78" rowSpan="3"><FONT face="돋움" size="2">&nbsp;지시<BR>
								및<BR>
								결과</FONT></TD>
						<TD class="xl29" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;작업계획량</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label66" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;이전완료량</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움" size="2"><FONT face="굴림">&nbsp;</FONT><asp:label id="Label75" runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl29" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;금일완료량</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
							height="26" width="80"><FONT face="돋움" size="2">&nbsp;적합수량</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl29" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;시작시간</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;종료시간</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
							height="26" width="80"><FONT face="돋움" size="2">&nbsp;비작업시간1</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;비작업사유1</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움"></FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="돋움"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 40.2pt; BACKGROUND-COLOR: transparent"
							align="center" height="52" rowSpan="2">
							<P><FONT face="돋움" size="2">검사</FONT><br>
								<FONT face="돋움" size="2">결과</FONT></P>
						</TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;부적합수량</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;검사판정</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;부적합현상</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
							height="26" width="80"><FONT face="돋움" size="2">&nbsp;부적합원인</FONT></TD>
						<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							width="80"><FONT face="돋움" size="2">&nbsp;세부내역</FONT></TD>
						<TD class="xl33" style="BORDER-RIGHT: black 0.5pt solid; BORDER-TOP: windowtext 0.5pt solid; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							colSpan="3" width="100"><FONT face="굴림" size="2"></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
					<TR style="HEIGHT: 19.5pt; mso-height-source: userset" height="26">
						<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 19.5pt; BACKGROUND-COLOR: transparent"
							align="center" height="26"><FONT face="돋움" size="2">&nbsp;비고</FONT></TD>
						<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext 0.5pt solid; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
							colSpan="6" ><FONT face="돋움">
<asp:label id=Label16 runat="server">&nbsp;</asp:label></FONT></TD>
						<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"
							width="1"><FONT face="돋움"></FONT></TD>
					</TR>
			</TABLE>
		</form>
	</body>
</HTML>