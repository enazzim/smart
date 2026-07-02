<%@ Page language="c#" Codebehind="SubFreeRegistration.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.CooperationCompany.SubFreeRegistration" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>SubFreeRegistration</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
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
	</head>
	<body onload="document.Form1.Title.focus();">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px" borderColor="#0"
				height="590" cellSpacing="0" cellPadding="0" width="900" align="center" border="0">
				<tr height="35">
					<td width="20"></td>
					<td><FONT face="굴림"></FONT></td>
				</tr>
				<TR>
					<td width="20"></td>
					<TD vAlign="top">
						<TABLE id="Table1" borderColor="#000000" height="530" cellSpacing="0" cellPadding="3" width="880"
							align="center" border="1" borderColorDark="dimgray" borderColorLight="#ffffff">
							<TR>
								<TD style="WIDTH: 100px" vAlign="middle" bgColor="whitesmoke" height="30"><FONT face="굴림">
										<P align="center"><asp:label id="Label1" runat="server" ForeColor="ControlDarkDark" Font-Bold="True">작&nbsp;성&nbsp;자</asp:label></P>
									</FONT>
								</TD>
								<TD vAlign="middle" bgColor="whitesmoke" height="30"><asp:textbox id="RegistrationPerson" runat="server" Height="20px" BorderStyle="Solid" BorderColor="Gray"
										BorderWidth="1px" ReadOnly="True"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" vAlign="middle" bgColor="whitesmoke" height="30"><FONT face="굴림">
										<P align="center"><asp:label id="Label2" runat="server" ForeColor="ControlDarkDark" Font-Bold="True">제&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;목</asp:label></P>
									</FONT>
								</TD>
								<TD vAlign="middle" bgColor="whitesmoke" height="30"><asp:textbox id="Title" runat="server" Height="20px" Width="99.9%" MaxLength="50" BorderStyle="Solid"
										BorderColor="Gray" BorderWidth="1px"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="제목을 입력해주세요!" ControlToValidate="Title"
										Display="None"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" bgColor="whitesmoke"><FONT face="굴림">
										<P align="center"><asp:label id="Label3" runat="server" ForeColor="ControlDarkDark" Font-Bold="True">내&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;용</asp:label></P>
									</FONT>
								</TD>
								<TD bgColor="whitesmoke"><FONT face="굴림"><asp:textbox id="Content" runat="server" Height="470px" Width="100%" TextMode="MultiLine" Rows="25"
											BorderStyle="Solid" BorderColor="Gray" BorderWidth="1px"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="내용을 입력해주세요!" ControlToValidate="Content"
											Display="None"></asp:requiredfieldvalidator></FONT></TD>
							</TR>
						</TABLE>
						<div align="right"></div>
						<FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림">
						</FONT><FONT face="굴림"></FONT><FONT face="굴림"></FONT>
						<div align="right"><asp:validationsummary id="ValidationSummary1" runat="server" ShowSummary="False" DisplayMode="List" ShowMessageBox="True"></asp:validationsummary><asp:button id="btnCancel" runat="server" CausesValidation="False" Text="취   소" Height="20px"
								Width="65px"></asp:button><asp:button id="btnApply" runat="server" Text="등   록" Height="20px" Width="65px" CausesValidation="False"></asp:button><FONT face="굴림"></FONT></div>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</html>
