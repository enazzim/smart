<%@ Register TagPrefix="uc1" TagName="ItemSearch" Src="ItemSearch/ItemSearch.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Page language="c#" Codebehind="CopyRealBOM.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.CopyRealBOM" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CopyRealBOM</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" style="LEFT: 5px; POSITION: absolute; TOP: 10px" cellSpacing="0" cellPadding="0"
				width="800" border="0">
				<TR>
					<TD height="20">
						<table cellSpacing="0" cellPadding="0" width="800" border="0">
							<tr>
								<td style="COLOR: #ff0000" align="right" width="80">복사 품목</td>
								<td align="left" width="720" colSpan="2"><FONT face="굴림"><uc1:itemsearch id="ItemSearch1" runat="server"></uc1:itemsearch></FONT></td>
							</tr>
							<tr>
								<td style="COLOR: #ff0000" align="right" width="80">원&nbsp;품목</td>
								<td align="left" width="720" colSpan="2"><FONT face="굴림"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></FONT></td>
							</tr>
						</table>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 20px" align="right" height="20"><asp:button id="Button1" runat="server" Text="BOM 복사" Height="20px" Width="80px"></asp:button></TD>
				</TR>
				<TR>
					<TD height="10"></TD>
				</TR>
				<TR>
					<TD>&nbsp;</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
