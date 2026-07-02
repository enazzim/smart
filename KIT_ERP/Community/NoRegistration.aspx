<%@ Page language="c#" Codebehind="NoRegistration.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.NoRegistration" %>
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
				if ( confirm("작성한 글을 등록하시겠습니까?") )
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
	<body onload="document.Form1.Title.focus();" bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px" borderColor="#0" cellSpacing="0" cellPadding="0" width="800" align="center" border="0" height="550">
				<tr>
					<td width="20"></td>
					<td><FONT face="굴림"></FONT></td>
				</tr>
				<TR>
					<td width="20"></td>
					<TD vAlign="top">
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="800" align="center" border="2" height="530" borderColorDark="dimgray" borderColorLight="#ffffff" borderColor="black">
							<TR>
								<TD style="WIDTH: 100px" bgColor="whitesmoke" height="30" vAlign="center"><FONT face="굴림">
										<P align="center"><STRONG>작 성 자</STRONG></P>
									</FONT>
								</TD>
								<TD bgColor="whitesmoke" height="30" vAlign="center"><asp:textbox id="RegistrationPerson" runat="server" Height="20px" ReadOnly="True" BorderStyle="Inset" BorderWidth="1px" BorderColor="Gray" Width="180px" BackColor="#EEEEE9" Font-Size="9pt"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" bgColor="whitesmoke" height="30" vAlign="center"><FONT face="굴림">
										<P align="center"><STRONG>제&nbsp; &nbsp; 목</STRONG></P>
									</FONT>
								</TD>
								<TD bgColor="whitesmoke" height="30" vAlign="center"><asp:textbox id="Title" runat="server" MaxLength="50" Width="99.9%" Height="20px" BorderStyle="Inset" BorderWidth="1px" BorderColor="Gray" BackColor="#EEEEE9" Font-Size="9pt"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" Display="None" ControlToValidate="Title" ErrorMessage="제목을 입력해주세요!"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" bgColor="whitesmoke"><FONT face="굴림">
										<P align="center"><STRONG>내&nbsp;&nbsp;&nbsp; 용</STRONG></P>
									</FONT>
								</TD>
								<TD bgColor="whitesmoke"><FONT face="굴림"><asp:textbox id="Content" runat="server" Width="100%" Rows="25" TextMode="MultiLine" Height="470px" BorderStyle="Inset" BorderWidth="1px" BorderColor="Gray" BackColor="#EEEEE9" Font-Size="9pt"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" Display="None" ControlToValidate="Content" ErrorMessage="내용을 입력해주세요!"></asp:requiredfieldvalidator></FONT></TD>
							</TR>
						</TABLE>
						<div align="right"></div>
						<FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림">
						</FONT><FONT face="굴림"></FONT><FONT face="굴림"></FONT>
						<div align="right">&nbsp;
						</div>
						<DIV align="right"><asp:validationsummary id="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="List" ShowSummary="False"></asp:validationsummary><asp:button id="btnCancel" runat="server" Text="취   소" CausesValidation="False" Height="20px" Width="65px"></asp:button><FONT face="굴림">&nbsp;</FONT><asp:button id="btnApply" runat="server" Text="등   록" Height="20px" Width="65px" CausesValidation="False"></asp:button><FONT face="굴림">
							</FONT>
						</DIV>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
