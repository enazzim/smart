<%@ Page language="c#" Codebehind="WebForm2.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ManagementInfomation.WebForm2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm2</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<FONT face="굴림">
				<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px; BORDER-COLLAPSE: collapse"
					cellSpacing="0" cellPadding="0" width="1050" align="center" border="0" x:str>
					<COLGROUP>
						<COL style="WIDTH: 60pt" width="80">
						<COL style="WIDTH: 26pt; mso-width-source: userset; mso-width-alt: 967" span="18" width="34">
						<COL style="WIDTH: 26pt; mso-width-source: userset; mso-width-alt: 967" width="34">
						<COL style="WIDTH: 26pt; mso-width-source: userset; mso-width-alt: 967" width="34">
						<TR style="HEIGHT: 13.5pt; mso-height-source: userset" height="18">
							<TD class="xl26" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext 0.5pt solid; BORDER-LEFT: windowtext 0.5pt solid; WIDTH: 424pt; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 64.5pt; BACKGROUND-COLOR: transparent"
								align="center" colSpan="15" height="86" rowSpan="2"><FONT face="돋움" size="6">
									<asp:Label id="Label1" runat="server">Label</asp:Label>월 원자재 입고대장</FONT></TD>
							<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext 0.5pt solid; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="50"><FONT face="돋움" size="2">담당</FONT></TD>
							<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext 0.5pt solid; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="50"><FONT face="돋움" size="2">영업</FONT></TD>
							<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext 0.5pt solid; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="50"><FONT face="돋움" size="2">품질</FONT></TD>
							<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext 0.5pt solid; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="50"><FONT face="돋움" size="2">생산</FONT></TD>
							<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext 0.5pt solid; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="50"><FONT face="돋움" size="2">관리</FONT></TD>
							<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext 0.5pt solid; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="50"><FONT face="돋움" size="2">사장</FONT></TD>
						</TR>
						<TR style="HEIGHT: 51pt; mso-height-source: userset" height="68">
							<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 51pt; BACKGROUND-COLOR: transparent"
								width="50" height="68"><FONT face="돋움" size="2"> </FONT>
							</TD>
							<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								width="50"><FONT face="돋움" size="2"> </FONT>
							</TD>
							<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								width="50"><FONT face="돋움" size="2"> </FONT>
							</TD>
							<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								width="50"><FONT face="돋움" size="2"> </FONT>
							</TD>
							<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								width="50"><FONT face="돋움" size="2"> </FONT>
							</TD>
							<TD class="xl28" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								width="50"><FONT face="돋움" size="2"> </FONT>
							</TD>
						</TR>
						<TR style="HEIGHT: 13.5pt" height="18">
							<TD class="xl24" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; FONT-SIZE: 8pt; BORDER-LEFT: windowtext 0.5pt solid; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 27pt; BACKGROUND-COLOR: transparent"
								align="center" width="50" height="36" rowSpan="2"><FONT face="돋움">품목번호</FONT></TD>
							<TD class="xl25" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext 0.5pt solid; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="200" colSpan="4"><FONT face="돋움" size="2">당월입고</FONT></TD>
							<TD class="xl25" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext 0.5pt solid; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="200" colSpan="4"><FONT face="돋움" size="2">당월입고</FONT></TD>
							<TD class="xl25" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext 0.5pt solid; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="200" colSpan="4"><FONT face="돋움" size="2">당월입고</FONT></TD>
							<TD class="xl25" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext 0.5pt solid; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="200" colSpan="4"><FONT face="돋움" size="2">당월입고</FONT></TD>
							<TD class="xl25" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext 0.5pt solid; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="200" colSpan="4"><FONT face="돋움" size="2">당월입고</FONT></TD>
						</TR>
						<TR style="HEIGHT: 13.5pt" height="18">
							<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; HEIGHT: 13.5pt; BACKGROUND-COLOR: transparent"
								align="center" width="50" height="18"><FONT face="돋움" size="1">입고중량</FONT></TD>
							<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="50"><FONT face="돋움" size="1">단가</FONT></TD>
							<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="50"><FONT face="돋움" size="1">일자</FONT></TD>
							<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="50"><FONT face="돋움" size="1">업체명</FONT></TD>
							<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="50"><FONT face="돋움" size="1">입고중량</FONT></TD>
							<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="50"><FONT face="돋움" size="1">단가</FONT></TD>
							<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="50"><FONT face="돋움" size="1">일자</FONT></TD>
							<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="50"><FONT face="돋움" size="1">업체명</FONT></TD>
							<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="50"><FONT face="돋움" size="1">입고중량</FONT></TD>
							<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="50"><FONT face="돋움" size="1">단가</FONT></TD>
							<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="50"><FONT face="돋움" size="1">일자</FONT></TD>
							<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="50"><FONT face="돋움" size="1">업체명</FONT></TD>
							<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="50"><FONT face="돋움" size="1">입고중량</FONT></TD>
							<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="50"><FONT face="돋움" size="1">단가</FONT></TD>
							<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="50"><FONT face="돋움" size="1">일자</FONT></TD>
							<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="50"><FONT face="돋움" size="1">업체명</FONT></TD>
							<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="50"><FONT face="돋움" size="1">입고중량</FONT></TD>
							<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="50"><FONT face="돋움" size="1">단가</FONT></TD>
							<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="50"><FONT face="돋움" size="1">일자</FONT></TD>
							<TD class="xl27" style="BORDER-RIGHT: windowtext 0.5pt solid; BORDER-TOP: windowtext; BORDER-LEFT: windowtext; BORDER-BOTTOM: windowtext 0.5pt solid; BACKGROUND-COLOR: transparent"
								align="center" width="50"><FONT face="돋움" size="1">업체명</FONT></TD>
						</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
