<%@ Page language="c#" Codebehind="LoaderWrite.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.LoaderWrite" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>LoaderWrite</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../StyleSheet1.css">
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
		<form id="Form1" encType="multipart/form-data" method="post" runat="server">
			<FONT face="굴림">
				<table style="Z-INDEX: 101; POSITION: absolute; TOP: 0px; LEFT: 0px" id="Table2" border="0"
					cellSpacing="0" borderColor="#0" cellPadding="0" width="800" align="center">
					<TR>
						<TD style="HEIGHT: 21px" width="20">
							<P><FONT face="굴림"></FONT>&nbsp;</P>
						</TD>
						<TD style="HEIGHT: 21px"><FONT face="굴림"></FONT></TD>
					</TR>
					<TR>
						<TD width="20"><FONT face="굴림"></FONT></TD>
						<TD vAlign="top">
							<TABLE id="Table1" border="1" cellSpacing="0" borderColor="#000000" borderColorLight="#ffffff"
								borderColorDark="dimgray" cellPadding="0" width="780" align="center">
								<TR>
									<TD style="WIDTH: 100px" bgColor="whitesmoke" height="30" vAlign="middle"><FONT face="굴림">
											<P align="center"><STRONG>작 성 자</STRONG></P>
										</FONT>
									</TD>
									<TD bgColor="whitesmoke" height="30" vAlign="middle"><asp:textbox id="RegistrationPerson" runat="server" BackColor="#EEEEE9" BorderColor="Gray" BorderStyle="Inset"
											BorderWidth="1px" ReadOnly="True" Height="20px" Font-Size="9pt"></asp:textbox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 100px" bgColor="whitesmoke" height="30" vAlign="middle"><FONT face="굴림">
											<P align="center"><STRONG>제&nbsp;&nbsp;&nbsp; 목</STRONG></P>
										</FONT>
									</TD>
									<TD bgColor="whitesmoke" height="30" vAlign="middle"><asp:textbox id="Title" runat="server" BackColor="#EEEEE9" BorderColor="Gray" BorderStyle="Inset"
											BorderWidth="1px" Height="20px" Font-Size="9pt" Width="700px" MaxLength="50"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="제목을 입력해주세요!" ControlToValidate="Title"
											Display="None"></asp:requiredfieldvalidator></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 100px" bgColor="whitesmoke"><FONT face="굴림">
											<P align="center"><STRONG>내&nbsp;&nbsp;&nbsp; 용</STRONG></P>
										</FONT>
									</TD>
									<TD bgColor="whitesmoke"><FONT face="굴림"><asp:textbox id="Content" runat="server" BackColor="#EEEEE9" BorderColor="Gray" BorderStyle="Inset"
												BorderWidth="1px" Height="200px" Font-Size="9pt" Width="700px" TextMode="MultiLine" Rows="25"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="내용을 입력해주세요!" ControlToValidate="Content"
												Display="None"></asp:requiredfieldvalidator></FONT></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 85px" bgColor="#f5f5f5" height="30" align="center">
										<P align="center"><STRONG>필 독 자</STRONG></P>
									</TD>
									<TD bgColor="#f5f5f5" height="30"><asp:listbox id=ListBox1 runat="server" BackColor="#F7F6F6" Height="200px" Width="150px" SelectionMode="Multiple" DataValueField="ID" DataTextField="Name" DataSource="<%# dsUser1 %>"></asp:listbox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 85px" bgColor="whitesmoke" height="30" align="center"><FONT face="굴림"><STRONG>첨부파일</STRONG></FONT></TD>
									<TD bgColor="whitesmoke" height="30"><FONT face="굴림"><INPUT style="BORDER-BOTTOM: gray 1px solid; BORDER-LEFT: gray 1px solid; BACKGROUND-COLOR: #eeeee9; WIDTH: 700px; BORDER-TOP: gray 1px solid; BORDER-RIGHT: gray 1px solid"
												id="File1" size="95" type="file" name="File1" runat="server"></FONT></TD>
								</TR>
							</TABLE>
							<DIV align="right"></DIV>
							<FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림">
							</FONT><FONT face="굴림"></FONT><FONT face="굴림"></FONT>
							<DIV align="right">&nbsp;</DIV>
							<DIV align="right"><asp:validationsummary id="ValidationSummary1" runat="server" ShowSummary="False" DisplayMode="List" ShowMessageBox="True"></asp:validationsummary><asp:button id="btnCancel" runat="server" Height="20px" Width="65px" CausesValidation="False"
									Text="취   소"></asp:button><FONT face="굴림">&nbsp;</FONT>
								<asp:button id="btnApply" runat="server" Height="20px" Width="65px" CausesValidation="False"
									Text="등   록"></asp:button><FONT face="굴림"></FONT></DIV>
						</TD>
					</TR>
				</table>
			</FONT>
		</form>
	</body>
</HTML>
