<%@ Page language="c#" Codebehind="InstituteWrite.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.InstituteWrite" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>InstituteWrite</title>
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
	<body bgColor="#f7f6f6" onload="document.Form1.Title.focus();">
		<form id="Form1" method="post" encType="multipart/form-data" runat="server">
			<FONT face="굴림">
				<table borderColor="#0" cellSpacing="0" cellPadding="0" width="800" align="center" border="0"
					id="Table2" style="Z-INDEX: 101; POSITION: absolute; TOP: 0px; LEFT: 0px">
					<TR>
						<TD style="HEIGHT: 21px" width="20">
							<P><FONT face="굴림"></FONT>&nbsp;</P>
						</TD>
						<TD style="HEIGHT: 21px"><FONT face="굴림"></FONT></TD>
					</TR>
					<TR>
						<TD width="20"><FONT face="굴림"></FONT></TD>
						<TD vAlign="top">
							<TABLE id="Table1" borderColor="#000000" cellSpacing="0" borderColorDark="dimgray" cellPadding="0"
								width="780" align="center" borderColorLight="#ffffff" border="1">
								<TR>
									<TD style="WIDTH: 100px" vAlign="middle" bgColor="whitesmoke" height="30"><FONT face="굴림">
											<P align="center"><STRONG>작 성 자</STRONG></P>
										</FONT>
									</TD>
									<TD vAlign="middle" bgColor="whitesmoke" height="30"><asp:textbox id="RegistrationPerson" runat="server" Font-Size="9pt" Height="20px" ReadOnly="True"
											BorderWidth="1px" BorderStyle="Inset" BorderColor="Gray" BackColor="#EEEEE9"></asp:textbox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 100px" vAlign="middle" bgColor="whitesmoke" height="30"><FONT face="굴림">
											<P align="center"><STRONG>제&nbsp;&nbsp;&nbsp; 목</STRONG></P>
										</FONT>
									</TD>
									<TD vAlign="middle" bgColor="whitesmoke" height="30"><asp:textbox id="Title" runat="server" Font-Size="9pt" Height="20px" BorderWidth="1px" BorderStyle="Inset"
											BorderColor="Gray" BackColor="#EEEEE9" MaxLength="50" Width="700px"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" Display="None" ControlToValidate="Title"
											ErrorMessage="제목을 입력해주세요!"></asp:requiredfieldvalidator></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 100px" bgColor="whitesmoke"><FONT face="굴림">
											<P align="center"><STRONG>내&nbsp;&nbsp;&nbsp; 용</STRONG></P>
										</FONT>
									</TD>
									<TD bgColor="whitesmoke"><FONT face="굴림"><asp:textbox id="Content" runat="server" Font-Size="9pt" Height="200px" BorderWidth="1px" BorderStyle="Inset"
												BorderColor="Gray" BackColor="#EEEEE9" Width="700px" Rows="25" TextMode="MultiLine"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" Display="None" ControlToValidate="Content"
												ErrorMessage="내용을 입력해주세요!"></asp:requiredfieldvalidator></FONT></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 85px" align="center" bgColor="#f5f5f5" height="30">
										<P align="center"><STRONG>필 독 자</STRONG></P>
									</TD>
									<TD bgColor="#f5f5f5" height="30"><asp:listbox id=ListBox1 runat="server" Height="200px" BackColor="#F7F6F6" Width="150px" DataSource="<%# dsUser1 %>" DataTextField="Name" DataValueField="ID" SelectionMode="Multiple"></asp:listbox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 85px" align="center" bgColor="whitesmoke" height="30"><FONT face="굴림"><STRONG>첨부파일</STRONG></FONT></TD>
									<TD bgColor="whitesmoke" height="30"><FONT face="굴림"><INPUT id="File1" style="BORDER-BOTTOM: gray 1px solid; BORDER-LEFT: gray 1px solid; BACKGROUND-COLOR: #eeeee9; WIDTH: 700px; BORDER-TOP: gray 1px solid; BORDER-RIGHT: gray 1px solid"
												type="file" size="95" name="File1" runat="server"></FONT></TD>
								</TR>
							</TABLE>
							<DIV align="right"></DIV>
							<FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림">
							</FONT><FONT face="굴림"></FONT><FONT face="굴림"></FONT>
							<DIV align="right">&nbsp;</DIV>
							<DIV align="right"><asp:validationsummary id="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="List"
									ShowSummary="False"></asp:validationsummary>
								<asp:button id="btnCancel" runat="server" Text="취   소" Width="65px" Height="20px" CausesValidation="False"></asp:button><FONT face="굴림">&nbsp;</FONT>
								<asp:button id="btnApply" runat="server" Text="등   록" Width="65px" Height="20px" CausesValidation="False"></asp:button><FONT face="굴림"></FONT></DIV>
						</TD>
					</TR>
				</table>
			</FONT>
		</form>
	</body>
</HTML>
