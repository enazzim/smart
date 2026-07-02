<%@ Page language="c#" Codebehind="GnRegistration.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.GnRegistration" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>일반자료실-글쓰기</title>
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
		<form id="Form1" method="post" encType="multipart/form-data" runat="server">
			<TABLE id="Table2" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px" borderColor="#0"
				height="550" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
				<tr>
					<td width="20" style="HEIGHT: 21px">
						<P><FONT face="굴림"></FONT>&nbsp;</P>
					</td>
					<td style="HEIGHT: 21px"><FONT face="굴림"></FONT></td>
				</tr>
				<TR>
					<td width="20"><FONT face="굴림"></FONT></td>
					<TD vAlign="top">
						<TABLE id="Table1" borderColor="#000000" height="520" cellSpacing="0" borderColorDark="dimgray"
							cellPadding="0" width="780" align="center" borderColorLight="#ffffff" border="1">
							<TR>
								<TD style="WIDTH: 100px" vAlign="middle" align="center" bgColor="#f5f5f5" height="20"><FONT face="굴림"><STRONG style="COLOR: buttontext">Category</STRONG></FONT></TD>
								<TD vAlign="middle" bgColor="#f5f5f5" colSpan="3" height="20"><FONT face="굴림">
										<asp:DropDownList id="DropDownList1" runat="server" BackColor="#EEEEE9"></asp:DropDownList>
										<asp:CheckBox id="CheckBox1" runat="server" Text="Category 직접입력 : " Font-Bold="True" ForeColor="ControlText"
											AutoPostBack="True"></asp:CheckBox>
										<asp:TextBox id="TextBox1" runat="server" BackColor="#EEEEE9" BorderColor="Gray" BorderStyle="Solid"
											BorderWidth="1px" Width="180px" Visible="False"></asp:TextBox></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" vAlign="middle" bgColor="whitesmoke" height="30"><FONT face="굴림">
										<P align="center"><STRONG>작 성 자</STRONG></P>
									</FONT>
								</TD>
								<TD vAlign="middle" bgColor="whitesmoke" height="30"><asp:textbox id="RegistrationPerson" runat="server" ReadOnly="True" BorderWidth="1px" BorderStyle="Inset"
										BorderColor="Gray" Height="20px" BackColor="#EEEEE9" Font-Size="9pt"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" vAlign="middle" bgColor="whitesmoke" height="30"><FONT face="굴림">
										<P align="center"><STRONG>제&nbsp;&nbsp;&nbsp; 목</STRONG></P>
									</FONT>
								</TD>
								<TD vAlign="middle" bgColor="whitesmoke" height="30"><asp:textbox id="Title" runat="server" BorderWidth="1px" BorderStyle="Inset" BorderColor="Gray"
										Height="20px" Width="700px" MaxLength="50" BackColor="#EEEEE9" Font-Size="9pt"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="제목을 입력해주세요!" ControlToValidate="Title"
										Display="None"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px; HEIGHT: 70px" bgColor="whitesmoke"><FONT face="굴림">
										<P align="center"><STRONG>내&nbsp;&nbsp;&nbsp; 용</STRONG></P>
									</FONT>
								</TD>
								<TD bgColor="whitesmoke" style="HEIGHT: 276px"><FONT face="굴림"><asp:textbox id="Content" runat="server" BorderWidth="1px" BorderStyle="Inset" BorderColor="Gray"
											Width="700px" TextMode="MultiLine" Rows="25" BackColor="#EEEEE9" Font-Size="9pt"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="내용을 입력해주세요!" ControlToValidate="Content"
											Display="None"></asp:requiredfieldvalidator></FONT></TD>
							</TR>
							<tr>
								<td align="center" bgColor="whitesmoke" height="30" style="WIDTH: 85px"><FONT face="굴림"><STRONG>첨부파일</STRONG></FONT></td>
								<td bgColor="whitesmoke" height="30"><FONT face="굴림"><INPUT id="File1" style="BORDER-RIGHT: gray 1px solid; BORDER-TOP: gray 1px solid; BORDER-LEFT: gray 1px solid; WIDTH: 700px; BORDER-BOTTOM: gray 1px solid; BACKGROUND-COLOR: #eeeee9"
											type="file" size="95" name="File1" runat="server"></FONT></td>
							</tr>
						</TABLE>
						<div align="right"></div>
						<FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림">
						</FONT><FONT face="굴림"></FONT><FONT face="굴림"></FONT>
						<div align="right">&nbsp;</div>
						<DIV align="right"><asp:validationsummary id="ValidationSummary1" runat="server" ShowSummary="False" DisplayMode="List" ShowMessageBox="True"></asp:validationsummary><asp:button id="btnCancel" runat="server" Height="20px" Width="65px" CausesValidation="False"
								Text="취   소"></asp:button><FONT face="굴림">&nbsp;</FONT><asp:button id="btnApply" runat="server" Height="20px" Width="65px" CausesValidation="False"
								Text="등   록"></asp:button><FONT face="굴림"></FONT></DIV>
					</TD>
				</TR>
			</TABLE>
			&nbsp;
		</form>
	</body>
</HTML>
