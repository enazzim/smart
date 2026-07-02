<%@ Page language="c#" Codebehind="WorkDailyWrite.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.WorkDailyWrite" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WorkDailyWrite</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!--
			function Registration_Check()
			{
				if ( confirm("ÀÛ¼ºÇÑ ±ÛÀ» µî·ÏÇÏ½Ã°Ú½À´Ï±î?") )
					return true;
				else
					return false;
			}
			
			function Cancel_Check()
			{
				if(confirm("Ãë¼ÒÇÏ½Ã°Ú½À´Ï±î?"))
					return true;
				else
					return false;
			}
		//-->
		</script>
	</HEAD>
	<body onload="document.Form1.Title.focus();" bgColor="#f7f6f6">
		<form id="Form1" method="post" encType="multipart/form-data" runat="server">
			<FONT face="±¼¸²">
				<TABLE id="Table2" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px" borderColor="#0"
					height="550" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
					<TR>
						<TD style="HEIGHT: 21px" width="20">
							<P><FONT face="±¼¸²"></FONT>&nbsp;</P>
						</TD>
						<TD style="HEIGHT: 21px"><FONT face="±¼¸²"></FONT></TD>
					</TR>
					<TR>
						<TD width="20"><FONT face="±¼¸²"></FONT></TD>
						<TD vAlign="top">
							<TABLE id="Table1" borderColor="#000000" height="520" cellSpacing="0" borderColorDark="dimgray"
								cellPadding="0" width="780" align="center" borderColorLight="#ffffff" border="1">
								<TR>
									<TD style="WIDTH: 100px" vAlign="middle" bgColor="whitesmoke" height="30"><FONT face="±¼¸²">
											<P align="center"><STRONG>ÀÛ ¼º ÀÚ</STRONG></P>
										</FONT>
									</TD>
									<TD vAlign="middle" bgColor="whitesmoke" height="30">
										<asp:textbox id="RegistrationPerson" runat="server" BackColor="#EEEEE9" BorderColor="Gray" BorderStyle="Inset"
											BorderWidth="1px" ReadOnly="True" Height="20px" Font-Size="9pt"></asp:textbox></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 100px" vAlign="middle" bgColor="whitesmoke" height="30"><FONT face="±¼¸²">
											<P align="center"><STRONG>Á¦&nbsp;&nbsp;&nbsp; ¸ñ</STRONG></P>
										</FONT>
									</TD>
									<TD vAlign="middle" bgColor="whitesmoke" height="30">
										<asp:textbox id="Title" runat="server" BackColor="#EEEEE9" BorderColor="Gray" BorderStyle="Inset"
											BorderWidth="1px" Width="700px" Height="20px" Font-Size="9pt" MaxLength="50"></asp:textbox>
										<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Á¦¸ñÀ» ÀÔ·ÂÇØÁÖ¼¼¿ä!" ControlToValidate="Title"
											Display="None"></asp:requiredfieldvalidator></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 100px; HEIGHT: 70px" bgColor="whitesmoke"><FONT face="±¼¸²">
											<P align="center"><STRONG>³»&nbsp;&nbsp;&nbsp; ¿ë</STRONG></P>
										</FONT>
									</TD>
									<TD style="HEIGHT: 276px" bgColor="whitesmoke"><FONT face="±¼¸²">
											<asp:textbox id="Content" runat="server" BackColor="#EEEEE9" BorderColor="Gray" BorderStyle="Inset"
												BorderWidth="1px" Width="700px" Font-Size="9pt" TextMode="MultiLine" Rows="25"></asp:textbox>
											<asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="³»¿ëÀ» ÀÔ·ÂÇØÁÖ¼¼¿ä!" ControlToValidate="Content"
												Display="None"></asp:requiredfieldvalidator></FONT></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 85px" align="center" bgColor="whitesmoke" height="30"><FONT face="±¼¸²"><STRONG>Ã·ºÎÆÄÀÏ</STRONG></FONT></TD>
									<TD bgColor="whitesmoke" height="30"><FONT face="±¼¸²"><INPUT id="File1" style="BORDER-RIGHT: gray 1px solid; BORDER-TOP: gray 1px solid; BORDER-LEFT: gray 1px solid; WIDTH: 700px; BORDER-BOTTOM: gray 1px solid; BACKGROUND-COLOR: #eeeee9"
												type="file" size="95" name="File1" runat="server"></FONT></TD>
								</TR>
							</TABLE>
							<DIV align="right"></DIV>
							<FONT face="±¼¸²"></FONT><FONT face="±¼¸²"></FONT><FONT face="±¼¸²"></FONT><FONT face="±¼¸²">
							</FONT><FONT face="±¼¸²"></FONT><FONT face="±¼¸²"></FONT>
							<DIV align="right">&nbsp;</DIV>
							<DIV align="right">
								<asp:validationsummary id="ValidationSummary1" runat="server" ShowSummary="False" DisplayMode="List" ShowMessageBox="True"></asp:validationsummary>
								<asp:button id="btnCancel" runat="server" Text="Ãë   ¼Ò" Width="65px" Height="20px" CausesValidation="False"></asp:button><FONT face="±¼¸²">&nbsp;</FONT>
								<asp:button id="btnApply" runat="server" Text="µî   ·Ï" Width="65px" Height="20px" CausesValidation="False"></asp:button><FONT face="±¼¸²"></FONT></DIV>
						</TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
