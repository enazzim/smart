<%@ Page language="c#" Codebehind="FreeReply.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.FreeReply" %>
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
			function Cancel_Check()
			{
				if(confirm("취소하시겠습니까?"))
					return true;
				else
					return false;
			}
			
			function Registration_Check()
			{
				if(confirm("등록하시겠습니까?"))
					return true;
				else
					return false;
			}
		//-->
		</script>
	</HEAD>
	<body onload="document.Form1.Content.focus();">
		&nbsp;
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" borderColor="#0"
				height="550" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
				<TR>
					<td width="20"></td>
					<TD vAlign="top">
						<TABLE id="Table1" borderColor="#000000" height="489" cellSpacing="0" cellPadding="0" width="800"
							align="center" border="1" borderColorDark="dimgray" borderColorLight="#ffffff" style="WIDTH: 800px; HEIGHT: 489px">
							<TR>
								<TD style="WIDTH: 100px" vAlign="middle" bgColor="whitesmoke" height="30"><FONT face="굴림">
										<P align="center"><STRONG>작 &nbsp;성&nbsp; 자</STRONG></P>
									</FONT>
								</TD>
								<TD vAlign="middle" bgColor="whitesmoke" height="30"><asp:textbox id="RegistrationPerson" runat="server" Height="20px" BorderColor="Gray" BorderStyle="Inset"
										BorderWidth="1px" ReadOnly="True" BackColor="#EEEEE9" Font-Size="9pt"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" vAlign="middle" bgColor="whitesmoke" height="30"><FONT face="굴림">
										<P align="center"><STRONG>제&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 목</STRONG></P>
									</FONT>
								</TD>
								<TD vAlign="middle" bgColor="whitesmoke" height="30"><asp:textbox id="Title" runat="server" Height="20px" MaxLength="50" Width="99.9%" BorderColor="Gray"
										BorderStyle="Inset" BorderWidth="1px" BackColor="#EEEEE9" Font-Size="9pt"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" Display="None" ControlToValidate="Title"
										ErrorMessage="제목을 입력해주세요!"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" bgColor="whitesmoke"><FONT face="굴림">
										<P align="center"><STRONG>내&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;용</STRONG></P>
									</FONT>
								</TD>
								<TD bgColor="whitesmoke"><FONT face="굴림"><asp:textbox id="Content" runat="server" Height="440px" Width="100%" Rows="25" TextMode="MultiLine"
											BorderColor="Gray" BorderStyle="Inset" BorderWidth="1px" BackColor="#EEEEE9" Font-Size="9pt"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" Display="None" ControlToValidate="Content"
											ErrorMessage="내용을 입력해주세요!"></asp:requiredfieldvalidator></FONT></TD>
							</TR>
						</TABLE>
						<br>
						<div align="right"><asp:validationsummary id="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="List"
								ShowSummary="False"></asp:validationsummary><asp:button id="btnCancel" runat="server" Text="취   소" CausesValidation="False" Height="20px"
								Width="60px"></asp:button><FONT face="굴림">&nbsp; </FONT>
							<asp:button id="btnApply" runat="server" Text="등   록" Height="20px" Width="65px" CausesValidation="False"></asp:button><FONT face="굴림"></FONT></div>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
