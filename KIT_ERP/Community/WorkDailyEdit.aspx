<%@ Page language="c#" Codebehind="WorkDailyEdit.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.WorkDailyEdit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WorkDailyEdit</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!--
		function Delete_Check()
		{
			if ( confirm("해당 항목을 삭제 하시겠습니까?") )
				return true;
			else
				return false;
		}
		
		function Edit_Check()			
		{
			if ( confirm("수정하시겠습니까?") )
				return true;
			else
				return false;
		}
		
		function ContentDelete_Check()
		{
			if(confirm("취소하시겠습니까?"))
				return true;
			else
				return false;
		}
		//-->
		</script>
	</HEAD>
	<body bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" cellSpacing="0"
				cellPadding="0" width="800" align="center" border="0">
				<TR>
					<TD vAlign="bottom" width="20"><FONT face="굴림"></FONT></TD>
					<TD vAlign="bottom" align="right" colSpan="2"><FONT face="굴림">&nbsp; </FONT>
					</TD>
				</TR>
				<TR>
					<TD width="20"></TD>
					<TD vAlign="top" colSpan="2">
						<TABLE id="Table2" style="WIDTH: 800px; TOP: 1px" borderColor="#000000" cellSpacing="0"
							borderColorDark="dimgray" cellPadding="0" width="800" align="center" borderColorLight="#ffffff"
							border="1">
							<TR>
								<TD style="WIDTH: 100px" bgColor="whitesmoke" height="30">
									<P align="center"><FONT style="FONT-WEIGHT: bold" face="굴림">작 성 자</FONT></P>
								</TD>
								<TD width="180" bgColor="whitesmoke" height="30">
									<P align="center"><asp:label id="lbWriter" runat="server" ForeColor="ControlDarkDark"></asp:label></P>
								</TD>
								<TD width="100" bgColor="whitesmoke" height="30">
									<P align="center"><FONT face="굴림"><STRONG>작 성 일</STRONG></FONT></P>
								</TD>
								<TD width="250" bgColor="whitesmoke" colSpan="3" height="30">
									<P align="center"><asp:label id="lblRegistrationDate" runat="server" ForeColor="ControlDarkDark"></asp:label></P>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" vAlign="middle" bgColor="whitesmoke" height="30">
									<P align="center"><FONT face="굴림"><STRONG>제&nbsp;&nbsp;&nbsp; 목</STRONG></FONT></P>
								</TD>
								<TD vAlign="middle" bgColor="whitesmoke" colSpan="6" height="30"><asp:textbox id="txtTitle" runat="server" Height="20px" Width="99.9%" MaxLength="50" BorderStyle="Inset"
										BorderColor="Gray" BorderWidth="1px" ReadOnly="True" BackColor="#EEEEE9" Font-Size="9pt"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="제목을 입력해주세요!" ControlToValidate="txtTitle"
										Display="None"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" bgColor="#f5f5f5" height="30"><FONT face="굴림">
										<P align="center"><FONT face="굴림"><STRONG>첨부파일</STRONG></FONT></P>
									</FONT>
								</TD>
								<TD style="WIDTH: 180px" bgColor="#f5f5f5" height="30"><FONT face="굴림"><asp:hyperlink id="HyperLink1" runat="server" Font-Bold="True">HyperLink</asp:hyperlink><asp:label id="Label1" runat="server"></asp:label></FONT></TD>
								<TD style="WIDTH: 100px" bgColor="#f5f5f5" height="30">
									<P align="center"><FONT face="굴림"><STRONG>파일변경</STRONG></FONT></P>
								</TD>
								<TD bgColor="#f5f5f5" colSpan="4" height="30"><INPUT id="File1" style="BORDER-RIGHT: gray 1px solid; BORDER-TOP: gray 1px solid; BORDER-LEFT: gray 1px solid; WIDTH: 376px; BORDER-BOTTOM: gray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: #eeeee9"
										type="file" size="43" name="File1" runat="server"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" bgColor="whitesmoke" height="200">
									<P align="center"><FONT face="굴림"><STRONG>내&nbsp;&nbsp;&nbsp; 용</STRONG></FONT></P>
								</TD>
								<TD bgColor="whitesmoke" colSpan="6" height="200"><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"><asp:textbox id="txtContents" runat="server" Height="400px" Width="100%" BorderStyle="Inset"
											BorderColor="Gray" BorderWidth="1px" ReadOnly="True" BackColor="#EEEEE9" Font-Size="9pt" Rows="23" TextMode="MultiLine"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="내용을 입력해주세요!" ControlToValidate="txtContents"
											Display="None"></asp:requiredfieldvalidator></FONT></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="middle" width="20" height="5"><FONT face="굴림"></FONT></TD>
					<TD vAlign="middle" align="left" width="440" height="28">
						<DIV align="left"><FONT face="굴림">&nbsp;</FONT>&nbsp;<FONT face="굴림">&nbsp;&nbsp;&nbsp;<asp:validationsummary id="ValidationSummary1" runat="server" Width="200px" DisplayMode="List" ShowMessageBox="True"
									ShowSummary="False"></asp:validationsummary></FONT></DIV>
					</TD>
					<TD vAlign="middle" align="right" width="440" height="28"><asp:button id="btnEdit" runat="server" Height="20px" Width="65px" Text="수   정" CausesValidation="False"></asp:button><FONT face="굴림">&nbsp;
						</FONT>
						<asp:button id="btnDelete" runat="server" Height="20px" Width="65px" Text="취  소" CausesValidation="False"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
