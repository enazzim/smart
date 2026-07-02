<%@ Page language="c#" Codebehind="SubDataRegistration.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.CooperationCompany.SubDataRegistration" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SubDataRegistration</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
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
		<form id="Form1" method="post" enctype="multipart/form-data" runat="server">
			<TABLE id="Table2" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" borderColor="#0" cellSpacing="0" cellPadding="0" width="800" align="center" border="0" height="590">
				<TR>
					<td width="20"></td>
					<TD vAlign="top">
						<TABLE id="Table1" borderColor="#000000" cellSpacing="0" cellPadding="0" width="800" align="center" border="1" height="530" borderColorDark="dimgray" borderColorLight="#ffffff">
							<TR>
								<TD style="WIDTH: 100px" bgColor="whitesmoke" height="30" vAlign="center"><FONT face="굴림">
										<P align="center" style="FONT-WEIGHT: bold">작 성 자</P>
									</FONT>
								</TD>
								<TD bgColor="whitesmoke" height="30" vAlign="center"><asp:textbox id="RegistrationPerson" runat="server" Height="20px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" bgColor="whitesmoke" height="30" vAlign="center"><FONT face="굴림">
										<P align="center" style="FONT-WEIGHT: bold">제&nbsp;&nbsp; &nbsp; 목</P>
									</FONT>
								</TD>
								<TD bgColor="whitesmoke" height="30" vAlign="center"><asp:textbox id="Title" runat="server" MaxLength="50" Width="99.9%" Height="20px" BorderColor="Gray" BorderStyle="Inset" BorderWidth="1px" Font-Size="9pt" BackColor="#EEEEE9"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" Display="None" ControlToValidate="Title" ErrorMessage="제목을 입력해주세요!"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" bgColor="whitesmoke"><FONT face="굴림">
										<P align="center" style="FONT-WEIGHT: bold">내&nbsp;&nbsp; &nbsp; 용</P>
									</FONT>
								</TD>
								<TD bgColor="whitesmoke"><FONT face="굴림"><asp:textbox id="Content" runat="server" Width="100%" Rows="25" TextMode="MultiLine" Height="440px" BorderColor="Gray" BorderStyle="Inset" BorderWidth="1px" Font-Size="9pt" BackColor="#EEEEE9"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" Display="None" ControlToValidate="Content" ErrorMessage="내용을 입력해주세요!"></asp:requiredfieldvalidator></FONT></TD>
							</TR>
							<tr>
								<td align="middle" bgColor="whitesmoke" height="30"><FONT face="굴림" style="FONT-WEIGHT: bold">
										첨부파일</FONT></td>
								<td bgColor="whitesmoke" height="30"><FONT face="굴림"><INPUT id="File1" type="file" size="95" name="File1" runat="server" style="BORDER-RIGHT: gray 1px solid; BORDER-TOP: gray 1px solid; BORDER-LEFT: gray 1px solid; BORDER-BOTTOM: gray 1px solid"></FONT></td>
							</tr>
						</TABLE>
						<div align="right"></div>
						<FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림">
						</FONT><FONT face="굴림"></FONT><FONT face="굴림"></FONT>
						<div align="right"><asp:validationsummary id="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="List" ShowSummary="False"></asp:validationsummary><asp:button id="btnCancel" runat="server" Text="취   소" CausesValidation="False" Height="20px" Width="65px" Font-Size="9pt"></asp:button><asp:button id="btnApply" runat="server" Text="등   록" Height="20px" Width="65px" CausesValidation="False" Font-Size="9pt"></asp:button><FONT face="굴림">
							</FONT>
						</div>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
