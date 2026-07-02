<%@ Page language="c#" Codebehind="View1.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.Popup.View1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>View1</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../StyleSheet2.css" type="text/css" rel="stylesheet">
		<STYLE type="text/css">BODY { SCROLLBAR-FACE-COLOR: #ffffff; SCROLLBAR-HIGHLIGHT-COLOR: #ffffff; SCROLLBAR-SHADOW-COLOR: #ffffff; SCROLLBAR-3DLIGHT-COLOR: #808080; SCROLLBAR-ARROW-COLOR: #400040; SCROLLBAR-TRACK-COLOR: #ffffff; SCROLLBAR-DARKSHADOW-COLOR: #808080 }
		</STYLE>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<table id="tb" style="Z-INDEX: 101; LEFT: 8px; WIDTH: 100%; POSITION: absolute; TOP: 8px; HEIGHT: 100%"
				border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td>
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="1" height="100%">
							<tr height="5%">
								<td width="6%" align="center">작성자</td>
								<td width="18%" style="PADDING-LEFT: 2px">
									<asp:Label id="lbWriter" runat="server">Label</asp:Label></td>
								<td width="6%">작성일</td>
								<td style="PADDING-LEFT: 2px">
									<asp:Label id="lbDate" runat="server">Label</asp:Label></td>
							</tr>
							<tr height="5%">
								<td width="6%" align="center">제목</td>
								<td colspan="3" style="PADDING-LEFT: 2px">
									<asp:Label id="lbTitle" runat="server">Label</asp:Label></td>
							</tr>
							<tr height="5%">
								<td width="6%" align="center">파일</td>
								<td colspan="3" style="PADDING-LEFT: 2px">
									<asp:HyperLink id="HyperLink1" runat="server">HyperLink</asp:HyperLink><FONT face="굴림">&nbsp;</FONT>
									<asp:Label id="lbSize" runat="server">Label</asp:Label></td>
							</tr>
							<tr>
								<td width="6%" align="center">내용</td>
								<td colspan="3" style="PADDING-LEFT: 2px; PADDING-TOP: 2px" vAlign="top">
									<asp:Label id="lbContents" runat="server">Label</asp:Label></td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td align="right">
						<asp:Button id="bt_Close" runat="server" Height="20px" Width="56px" Text="닫 기" Font-Size="9pt"></asp:Button>
					</td>
				</tr>
			</table>
			<br>
		</form>
	</body>
</HTML>
