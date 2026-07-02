<%@ Page language="c#" Codebehind="FreeRegistration.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.FreeRegistration" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>NoRegistration</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!--
			function Registration_Check()
			{
				if ( confirm("등록하시겠습니까?") )
					return true;
				else
					return false;
			}
			
			function Cancel_Check()
			{
				if(confirm("취소하시겠습니까?"))
					return true;
				else
					return false;
			}
		//-->
		</script>
	</HEAD>
	<body onload="document.Form1.Title.focus();" ms_positioning="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellpadding="0" cellspacing="0" style="LEFT: 10px; POSITION: absolute; TOP: 10px">
				<tr>
					<td><TABLE id="Table1" borderColor="#000000" height="530" cellSpacing="0" borderColorDark="dimgray" cellPadding="0" width="787" align="left" borderColorLight="#ffffff" border="1">
							<TR>
								<TD style="WIDTH: 100px" vAlign="center" bgColor="whitesmoke" height="30"><FONT face="굴림">
										<P align="center"><STRONG>작 성 자</STRONG></P>
									</FONT>
								</TD>
								<TD vAlign="center" bgColor="whitesmoke" height="30">
									<asp:textbox id="RegistrationPerson" runat="server" ReadOnly="True" BorderWidth="1px" BorderColor="Gray" BorderStyle="Inset" BackColor="#EEEEE9" Font-Size="9pt"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" vAlign="center" bgColor="whitesmoke" height="30"><FONT face="굴림">
										<P align="center"><STRONG>제&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 목</STRONG></P>
									</FONT>
								</TD>
								<TD vAlign="center" bgColor="whitesmoke" height="30">
									<asp:textbox id="Title" runat="server" BorderWidth="1px" BorderColor="Gray" BorderStyle="Inset" MaxLength="50" Width="676px" BackColor="#EEEEE9" Font-Size="9pt"></asp:textbox>
									<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" Display="None" ControlToValidate="Title" ErrorMessage="제목을 입력해주세요!"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" bgColor="whitesmoke"><FONT face="굴림">
										<P align="center"><STRONG>내&nbsp; &nbsp;&nbsp; 용</STRONG></P>
									</FONT>
								</TD>
								<TD bgColor="whitesmoke"><FONT face="굴림">
										<asp:textbox id="Content" runat="server" BorderWidth="1px" BorderColor="Gray" BorderStyle="Inset" Width="678px" Rows="25" TextMode="MultiLine" BackColor="#EEEEE9" Font-Size="9pt" Height="458px"></asp:textbox></FONT></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<TR>
					<td align="right"><br>
						<asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" Display="None" ControlToValidate="Content" ErrorMessage="내용을 입력해주세요!"></asp:requiredfieldvalidator>
						<asp:validationsummary id="ValidationSummary1" runat="server" ShowSummary="False" DisplayMode="List" ShowMessageBox="True"></asp:validationsummary>
						<asp:button id="btnCancel" runat="server" CausesValidation="False" Text="취   소" Height="20px" Width="65px"></asp:button><FONT face="굴림">&nbsp;
						</FONT>
						<asp:button id="btnApply" runat="server" Text="등   록" Height="20px" Width="65px" CausesValidation="False"></asp:button><FONT face="굴림"></FONT>
					</td>
				</TR>
			</table>
		</form>
	</body>
</HTML>
