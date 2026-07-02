<%@ Page language="c#" Codebehind="SubFreeAdjustment.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.CooperationCompany.SubFreeAdjustment" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SubFreeAdjustment</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
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
	<body bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" height="550"
				cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
				<tr height="20">
					<td vAlign="bottom" width="20" height="20"><FONT face="굴림"></FONT></td>
					<td vAlign="bottom" align="right" colSpan="2" height="20"><asp:button id="btnReply" runat="server" Height="20px" Width="65px" Text="답   변"></asp:button><FONT face="굴림">&nbsp;
						</FONT>
					</td>
				</tr>
				<TR>
					<td width="20"></td>
					<TD vAlign="top" colSpan="2">
						<TABLE id="Table2" borderColor="#000000" height="527" cellSpacing="0" cellPadding="0" width="800"
							align="center" border="1" borderColorDark="dimgray" borderColorLight="#ffffff">
							<TR>
								<TD style="WIDTH: 100px" bgColor="whitesmoke" height="30">
									<P align="center"><FONT style="FONT-WEIGHT: bold" face="굴림">작 성 자</FONT></P>
								</TD>
								<TD width="180" bgColor="whitesmoke" height="30">
									<P align="center"><asp:label id="lblRegistrationPerson" runat="server" ForeColor="ControlDarkDark"></asp:label></P>
								</TD>
								<TD width="100" bgColor="whitesmoke" height="30">
									<P align="center"><FONT style="FONT-WEIGHT: bold" face="굴림">작 성 일</FONT></P>
								</TD>
								<TD width="250" bgColor="whitesmoke" height="30">
									<P align="center"><asp:label id="lblRegistrationDate" runat="server" ForeColor="ControlDarkDark"></asp:label></P>
								</TD>
								<TD width="100" bgColor="whitesmoke" height="30">
									<P align="center"><FONT style="FONT-WEIGHT: bold" face="굴림">조 회 수</FONT></P>
								</TD>
								<TD width="150" bgColor="whitesmoke" height="30">
									<P align="center"><asp:label id="lblHits" runat="server" ForeColor="ControlDarkDark"></asp:label></P>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" vAlign="middle" bgColor="whitesmoke" height="30">
									<P align="center"><FONT style="FONT-WEIGHT: bold" face="굴림">제&nbsp;&nbsp;&nbsp;&nbsp; 목</FONT></P>
								</TD>
								<TD vAlign="middle" bgColor="whitesmoke" colSpan="6" height="30"><asp:textbox id="txtTitle" runat="server" Height="20px" Width="99.9%" MaxLength="50" BorderStyle="Inset"
										BorderColor="Gray" BorderWidth="1px" ReadOnly="True" BackColor="#EEEEE9" Font-Size="9pt"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="제목을 입력해주세요!" ControlToValidate="txtTitle"
										Display="None"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" bgColor="whitesmoke">
									<P align="center"><FONT style="FONT-WEIGHT: bold" face="굴림">내&nbsp;&nbsp; &nbsp;&nbsp; 
											용</FONT></P>
								</TD>
								<TD bgColor="whitesmoke" colSpan="6"><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"><asp:textbox id="txtContents" runat="server" Height="462px" Width="100%" Rows="23" TextMode="MultiLine"
											BorderStyle="Inset" BorderColor="Gray" BorderWidth="1px" ReadOnly="True" BackColor="#EEEEE9" Font-Size="9pt"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="내용을 입력해주세요!" ControlToValidate="txtContents"
											Display="None"></asp:requiredfieldvalidator></FONT></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td vAlign="middle" width="20" height="28"><FONT face="굴림"></FONT></td>
					<td vAlign="middle" align="left" width="440" height="28">
						<DIV align="left"><FONT face="굴림">&nbsp;</FONT><asp:linkbutton id="lbkList" runat="server">목록</asp:linkbutton><FONT face="굴림">&nbsp;
								<asp:HyperLink id="hlnkPreContent" runat="server">윗글</asp:HyperLink>&nbsp;
								<asp:HyperLink id="hlnkNextContent" runat="server">아랫글</asp:HyperLink></FONT></DIV>
					</td>
					<td vAlign="middle" align="right" width="440" height="28"><asp:validationsummary id="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
							ShowSummary="False"></asp:validationsummary><asp:button id="btnEdit" runat="server" Height="20px" Width="65px" Text="수   정" Enabled="False"
							CausesValidation="False" Font-Size="9pt"></asp:button><asp:button id="btnDelete" runat="server" Height="20px" Width="65px" Text="삭   제" Enabled="False"
							CausesValidation="False" Font-Size="9pt"></asp:button></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
