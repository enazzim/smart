<%@ Page language="c#" Codebehind="NoAdjustment.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.NoAdjustment" %>
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
				if(confirm("삭제하시겠습니까?"))
					return true;
				else
					return false;
			}
			
		//-->
		</script>
	</HEAD>
	<body bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" height="550" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
				<TR>
					<td width="20"></td>
					<TD vAlign="top" colSpan="2">
						<TABLE id="Table2" borderColor="#000000" height="527" cellSpacing="0" borderColorDark="dimgray" cellPadding="0" width="800" align="center" borderColorLight="#ffffff" border="1">
							<TR>
								<TD style="WIDTH: 73px" bgColor="whitesmoke" height="30">
									<P align="center"><FONT face="굴림"><STRONG>작 성 자</STRONG></FONT></P>
								</TD>
								<TD width="170" bgColor="whitesmoke" height="30">
									<P align="center"><asp:label id="lblRegistrationPerson" runat="server" ForeColor="ControlDarkDark"></asp:label></P>
								</TD>
								<TD width="90" bgColor="whitesmoke" height="30">
									<P align="center"><FONT face="굴림"><STRONG>작 성 일</STRONG></FONT></P>
								</TD>
								<TD width="210" bgColor="whitesmoke" height="30">
									<P align="center"><asp:label id="lblRegistrationDate" runat="server" ForeColor="ControlDarkDark"></asp:label></P>
								</TD>
								<TD width="90" bgColor="whitesmoke" height="30">
									<P align="center"><FONT face="굴림"><STRONG>조 회 수</STRONG></FONT></P>
								</TD>
								<TD width="140" bgColor="whitesmoke" height="30">
									<P align="center"><asp:label id="lblHits" runat="server" ForeColor="ControlDarkDark"></asp:label></P>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 73px" vAlign="center" bgColor="whitesmoke" height="30">
									<P align="center"><FONT face="굴림"><STRONG>제&nbsp;&nbsp; 목</STRONG></FONT></P>
								</TD>
								<TD vAlign="center" bgColor="whitesmoke" colSpan="6" height="30"><FONT face="굴림"></FONT><asp:textbox id="txtTitle" runat="server" ReadOnly="True" BorderColor="Gray" BorderWidth="1px" BorderStyle="Inset" Width="720px" Height="20px" MaxLength="50" BackColor="#EEEEE9" Font-Size="9pt"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="제목을 입력해주세요!" ControlToValidate="txtTitle" Display="None"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 73px" bgColor="whitesmoke">
									<P align="center"><FONT face="굴림"><STRONG>내&nbsp;&nbsp; 용</STRONG></FONT></P>
								</TD>
								<TD bgColor="whitesmoke" colSpan="6"><FONT face="굴림"><asp:textbox id="txtContents" runat="server" ReadOnly="True" BorderColor="Gray" BorderWidth="1px" BorderStyle="Inset" Width="722px" Height="470px" Rows="23" TextMode="MultiLine" BackColor="#EEEEE9" Font-Size="9pt"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="내용을 입력해주세요!" ControlToValidate="txtContents" Display="None"></asp:requiredfieldvalidator></FONT></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td vAlign="center" width="20" height="28"><FONT face="굴림"></FONT></td>
					<td vAlign="center" align="left" width="400" height="28">
						<DIV align="left"><FONT face="굴림">&nbsp;</FONT><asp:linkbutton id="lbkList" runat="server">목록</asp:linkbutton><FONT face="굴림">&nbsp;&nbsp;
								<asp:hyperlink id="hlnkPreContent" runat="server">윗글</asp:hyperlink>&nbsp;
								<asp:HyperLink id="hlnkNextContent" runat="server">아랫글</asp:HyperLink></FONT></DIV>
					</td>
					<td vAlign="center" align="right" width="400" height="28"><asp:validationsummary id="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True" DisplayMode="List"></asp:validationsummary><asp:button id="btnEdit" runat="server" Height="20px" Width="65px" Text="수   정" Enabled="False"></asp:button><FONT face="굴림">&nbsp;</FONT><asp:button id="btnDelete" runat="server" Height="20px" Width="65px" Text="삭   제" Enabled="False"></asp:button></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
