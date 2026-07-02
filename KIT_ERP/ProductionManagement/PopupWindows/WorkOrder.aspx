<%@ Page language="c#" Codebehind="WorkOrder.aspx.cs" AutoEventWireup="false" Inherits="hanaro.ProductionManagement.PopupWindows.WorkOrder" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WorkOrder</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
		</form>
		<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px; BORDER-COLLAPSE: collapse"
			cellSpacing="0" cellPadding="0" border="0" x:str>
			<COLGROUP>
				<COL style="WIDTH: 33pt; mso-width-source: userset; mso-width-alt: 1251" width="44">
				<COL style="WIDTH: 69pt; mso-width-source: userset; mso-width-alt: 2616" width="92">
				<COL style="WIDTH: 93pt; mso-width-source: userset; mso-width-alt: 3527" width="124">
				<COL style="WIDTH: 74pt; mso-width-source: userset; mso-width-alt: 2816" width="99">
				<COL style="WIDTH: 88pt; mso-width-source: userset; mso-width-alt: 3328" width="117">
				<COL style="WIDTH: 75pt; mso-width-source: userset; mso-width-alt: 2844" width="100">
				<COL style="WIDTH: 77pt; mso-width-source: userset; mso-width-alt: 2929" width="103">
				<TR style="HEIGHT: 17.25pt; mso-height-source: userset" height="23">
					<TD class="xl32" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; WIDTH: 509pt; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 17.25pt; BACKGROUND-COLOR: transparent"
						width="679" colSpan="7" height="23"><STRONG><FONT face="돋움">작업지시서 및 작업일보</FONT></STRONG></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
						height="26"><FONT face="돋움">구분</FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">지시번호</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">지시일자</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">제품번호</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; WIDTH: 33pt; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 80.4pt; BACKGROUND-COLOR: transparent"
						width="44" height="104" rowSpan="4"><FONT face="돋움">작업표준</FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">작업장명</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">작업자</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">품목번호</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
						height="26"><FONT face="돋움">품목명</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">공정명</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">표준시간</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
						height="26"><FONT face="돋움">사용공구1</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">사용치구1</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">사용공구2</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
						height="26"><FONT face="돋움">사용치구2</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; WIDTH: 33pt; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 60.3pt; BACKGROUND-COLOR: transparent"
						width="44" height="78" rowSpan="3"><FONT face="돋움">지시<BR>
							및<BR>
							결과</FONT></TD>
					<TD class="xl29" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움" size="2">작업계획수량</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl29" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움" size="2">이전완료수량</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl29" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움" size="2">금일완료수량</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
						height="26"><FONT face="돋움">적합수량</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl29" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움" size="2">작업시작시간</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl29" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움" size="2">작업종료시간</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
						height="26"><FONT face="돋움">비작업시간1</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">비작업사유1</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; WIDTH: 33pt; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 40.2pt; BACKGROUND-COLOR: transparent"
						width="44" height="52" rowSpan="2"><FONT face="돋움">검사결과</FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">부적합수량</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">검사판정</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">부적합현상</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
						height="26"><FONT face="돋움">부적합원인</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">세부내역</FONT></TD>
					<TD class="xl33" style="BORDER-RIGHT: black 0.5pt solid; BORDER-TOP: windowtext 0.5pt solid; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
						colSpan="3"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 18.75pt; mso-height-source: userset" height="25">
					<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; WIDTH: 33pt; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 18.75pt; BACKGROUND-COLOR: transparent"
						width="44" height="25"><FONT face="돋움">비고</FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext 0.5pt solid; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
						colSpan="6"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 5.25pt; mso-height-source: userset" height="7">
					<TD class="xl30" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; HEIGHT: 5.25pt; BACKGROUND-COLOR: transparent"
						height="7"><FONT face="돋움"></FONT></TD>
					<TD class="xl31" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl30" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl31" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl30" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl31" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl30" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 5.25pt; mso-height-source: userset" height="7">
					<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; HEIGHT: 5.25pt; BACKGROUND-COLOR: transparent"
						height="7"><FONT face="돋움"></FONT></TD>
					<TD class="xl25" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl25" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl25" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 17.25pt; mso-height-source: userset" height="23">
					<TD class="xl32" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 17.25pt; BACKGROUND-COLOR: transparent"
						colSpan="7" height="23"><STRONG><FONT face="돋움">작업지시서 및 작업일보</FONT></STRONG></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
						height="26"><FONT face="돋움">구분</FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">지시번호</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">지시일자</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">제품번호</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; WIDTH: 33pt; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 80.4pt; BACKGROUND-COLOR: transparent"
						width="44" height="104" rowSpan="4"><FONT face="돋움">작업표준</FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">작업장명</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">작업자</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">품목번호</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
						height="26"><FONT face="돋움">품목명</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">공정명</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">표준시간</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
						height="26"><FONT face="돋움">사용공구1</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">사용치구1</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">사용공구2</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
						height="26"><FONT face="돋움">사용치구2</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; WIDTH: 33pt; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 60.3pt; BACKGROUND-COLOR: transparent"
						width="44" height="78" rowSpan="3"><FONT face="돋움">지시<BR>
							및<BR>
							결과</FONT></TD>
					<TD class="xl29" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움" size="2">작업계획수량</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">이전완료수량</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl29" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움" size="2">금일완료수량</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
						height="26"><FONT face="돋움">적합수량</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl29" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움" size="2">작업시작시간</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">작업종료시간</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
						height="26"><FONT face="돋움">비작업시간1</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">비작업사유1</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; WIDTH: 33pt; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 40.2pt; BACKGROUND-COLOR: transparent"
						width="44" height="52" rowSpan="2"><FONT face="돋움">검사결과</FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">부적합수량</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">검사판정</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">부적합현상</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
						height="26"><FONT face="돋움">부적합원인</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">세부내역</FONT></TD>
					<TD class="xl33" style="BORDER-RIGHT: black 0.5pt solid; BORDER-TOP: windowtext 0.5pt solid; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
						colSpan="3"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 18pt; mso-height-source: userset" height="24">
					<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; WIDTH: 33pt; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 18pt; BACKGROUND-COLOR: transparent"
						width="44" height="24"><FONT face="돋움">비고</FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext 0.5pt solid; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
						colSpan="6"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 6.75pt; mso-height-source: userset" height="9">
					<TD class="xl30" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; HEIGHT: 6.75pt; BACKGROUND-COLOR: transparent"
						height="9"><FONT face="돋움"></FONT></TD>
					<TD class="xl31" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl30" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl31" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl30" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl31" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl30" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 6pt; mso-height-source: userset" height="8">
					<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; HEIGHT: 6pt; BACKGROUND-COLOR: transparent"
						height="8"><FONT face="돋움"></FONT></TD>
					<TD class="xl25" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl25" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl25" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl24" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 17.25pt; mso-height-source: userset" height="23">
					<TD class="xl32" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: #d4d0c8; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 17.25pt; BACKGROUND-COLOR: transparent"
						colSpan="7" height="23"><STRONG><FONT face="돋움">작업지시서 및 작업일보</FONT></STRONG></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
						height="26"><FONT face="돋움">구분</FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">지시번호</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">지시일자</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">제품번호</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; WIDTH: 33pt; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 80.4pt; BACKGROUND-COLOR: transparent"
						width="44" height="104" rowSpan="4"><FONT face="돋움">작업표준</FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">작업장명</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">작업자</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">품목번호</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
						height="26"><FONT face="돋움">품목명</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">공정명</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">표준시간</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
						height="26"><FONT face="돋움">사용공구1</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">사용치구1</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">사용공구2</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
						height="26"><FONT face="돋움">사용치구2</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; WIDTH: 33pt; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 60.3pt; BACKGROUND-COLOR: transparent"
						width="44" height="78" rowSpan="3"><FONT face="돋움">지시<BR>
							및<BR>
							결과</FONT></TD>
					<TD class="xl29" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움" size="2">작업계획수량</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">이전완료수량</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl29" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움" size="2">금일완료수량</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
						height="26"><FONT face="돋움">적합수량</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl29" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움" size="2">작업시작시간</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">작업종료시간</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
						height="26"><FONT face="돋움">비작업시간1</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">비작업사유1</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; WIDTH: 33pt; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 40.2pt; BACKGROUND-COLOR: transparent"
						width="44" height="52" rowSpan="2"><FONT face="돋움">검사결과</FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">부적합수량</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">검사판정</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">부적합현상</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 20.1pt; mso-height-source: userset" height="26">
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 20.1pt; BACKGROUND-COLOR: transparent"
						height="26"><FONT face="돋움">부적합원인</FONT></TD>
					<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"><FONT face="돋움">세부내역</FONT></TD>
					<TD class="xl33" style="BORDER-RIGHT: black 0.5pt solid; BORDER-TOP: windowtext 0.5pt solid; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
						colSpan="3"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 19.5pt; mso-height-source: userset" height="26">
					<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext 0.5pt solid; WIDTH: 33pt; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 19.5pt; BACKGROUND-COLOR: transparent"
						width="44" height="26"><FONT face="돋움">비고</FONT></TD>
					<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext 0.5pt solid; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
						colSpan="6"><FONT face="돋움"></FONT></TD>
				</TR>
				<TR style="HEIGHT: 6pt; mso-height-source: userset" height="8">
					<TD class="xl30" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; HEIGHT: 6pt; BACKGROUND-COLOR: transparent"
						height="8"><FONT face="돋움"></FONT></TD>
					<TD class="xl31" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl30" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl31" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl30" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl31" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
					<TD class="xl30" style="BORDER-RIGHT: #d4d0c8; BORDER-TOP: windowtext; BORDER-LEFT: #d4d0c8; BORDER-BOTTOM: #d4d0c8 0.5pt; BACKGROUND-COLOR: transparent"><FONT face="돋움"></FONT></TD>
				</TR>
		</TABLE>
		&nbsp;
		<P></P>
	</body>
</HTML>
