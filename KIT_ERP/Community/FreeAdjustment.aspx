<%@ Page language="c#" Codebehind="FreeAdjustment.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.FreeAdjustment" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>NoAdjustment</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!--
			function Edit_Check()			
			{
				if ( confirm("수정겠습니까?") )
					return true;
				else
					return false;
			}
			
			function Delete_Check()
			{
				if(confirm("답글도 모두함께 삭제됩니다. 삭제하시겠습니까?"))
					return true;
				else
					return false;
			}
		//-->
		</script>
	</HEAD>
	<body ms_positioning="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 13px; HEIGHT: 550px" height="621" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
				<tr>
					<td vAlign="bottom" width="20"><FONT face="굴림"></FONT></td>
					<td vAlign="bottom" align="right" colSpan="2"><asp:button id="btnReply" runat="server" Height="20px" Width="65px" Text="답   변"></asp:button><FONT face="굴림">&nbsp;
						</FONT>
					</td>
				</tr>
				<TR>
					<td width="20"></td>
					<TD vAlign="top" colSpan="2">
						<TABLE id="Table2" borderColor="#000000" height="492" cellSpacing="0" cellPadding="0" width="800" align="center" border="1" borderColorDark="dimgray" borderColorLight="#ffffff" style="WIDTH: 800px; TOP: 1px; HEIGHT: 492px">
							<TR>
								<TD style="WIDTH: 100px" bgColor="whitesmoke" height="30">
									<P align="center"><FONT style="FONT-WEIGHT: bold" face="굴림">작 성 자</FONT></P>
								</TD>
								<TD width="180" bgColor="whitesmoke" height="30">
									<P align="center"><asp:label id="lblRegistrationPerson" runat="server" ForeColor="ControlDarkDark"></asp:label></P>
								</TD>
								<TD width="100" bgColor="whitesmoke" height="30">
									<P align="center"><FONT face="굴림"><STRONG>작 성 일</STRONG></FONT></P>
								</TD>
								<TD width="250" bgColor="whitesmoke" height="30">
									<P align="center"><asp:label id="lblRegistrationDate" runat="server" ForeColor="ControlDarkDark"></asp:label></P>
								</TD>
								<TD width="100" bgColor="whitesmoke" height="30">
									<P align="center"><FONT face="굴림"><STRONG>조 회 수</STRONG></FONT></P>
								</TD>
								<TD width="150" bgColor="whitesmoke" height="30">
									<P align="center"><asp:label id="lblHits" runat="server" ForeColor="ControlDarkDark"></asp:label></P>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" vAlign="center" bgColor="whitesmoke" height="30">
									<P align="center"><FONT face="굴림"><STRONG>제&nbsp;&nbsp;&nbsp; 목</STRONG></FONT></P>
								</TD>
								<TD vAlign="center" bgColor="whitesmoke" colSpan="6" height="30"><asp:textbox id="txtTitle" runat="server" Height="20px" Width="99.9%" MaxLength="50" BorderStyle="Inset" BorderColor="Gray" BorderWidth="1px" ReadOnly="True" BackColor="#EEEEE9" Font-Size="9pt"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="제목을 입력해주세요!" ControlToValidate="txtTitle" Display="None"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" bgColor="whitesmoke">
									<P align="center"><FONT face="굴림"><STRONG>내&nbsp;&nbsp;&nbsp; 용</STRONG></FONT></P>
								</TD>
								<TD bgColor="whitesmoke" colSpan="6"><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"><asp:textbox id="txtContents" runat="server" Height="440px" Width="100%" Rows="23" TextMode="MultiLine" BorderStyle="Inset" BorderColor="Gray" BorderWidth="1px" ReadOnly="True" BackColor="#EEEEE9" Font-Size="9pt"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="내용을 입력해주세요!" ControlToValidate="txtContents" Display="None"></asp:requiredfieldvalidator></FONT></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td vAlign="center" width="20" height="5"><FONT face="굴림"></FONT></td>
					<td vAlign="center" align="left" width="440" height="28">
						<DIV align="left"><FONT face="굴림">&nbsp;</FONT><asp:linkbutton id="lbkList" runat="server">목록</asp:linkbutton><FONT face="굴림">&nbsp;
								<asp:HyperLink id="hlnkPreContent" runat="server">윗글</asp:HyperLink>&nbsp;
								<asp:HyperLink id="hlnkNextContent" runat="server">아랫글</asp:HyperLink></FONT></DIV>
					</td>
					<td vAlign="center" align="right" width="440" height="28"><asp:validationsummary id="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False"></asp:validationsummary><asp:button id="btnEdit" runat="server" Height="20px" Width="65px" Text="수   정" Enabled="False" CausesValidation="False"></asp:button><FONT face="굴림">&nbsp;
						</FONT>
						<asp:button id="btnDelete" runat="server" Height="20px" Width="65px" Text="삭   제" Enabled="False" CausesValidation="False"></asp:button></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
