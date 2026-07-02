<%@ Page language="c#" Codebehind="GuestBookReply.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Homepage.GuestBookReply" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>GuestBookReply</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!--
			function Registration_Check()
			{
				if(confirm("등록하시겠습니까?"))
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
	<body MS_POSITIONING="FlowLayout" bgcolor="whitesmoke" onload="document.Form1.RegistrationPerson.focus();">
		<form id="Form1" method="post" runat="server" style="FONT-SIZE: 10pt">
			<TABLE id="Table1" style="FONT-SIZE: 0pt; Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px"
				cellSpacing="0" cellPadding="0" width="800" border="0" align="center">
				<TR height="30">
					<TD vAlign="middle" width="10"><FONT face="굴림"></FONT></TD>
					<TD vAlign="middle" align="center" colSpan="3">
						<TABLE id="Table1" style="FONT-SIZE: 0pt" height="30" cellSpacing="0" cellPadding="0" width="150"
							border="0">
							<TR height="30">
								<TD style="BORDER-BOTTOM: dimgray 2px solid" vAlign="bottom" align="center" height="30"><asp:label id="Label1" runat="server" Font-Bold="True" ForeColor="SteelBlue" Font-Size="11pt"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR height="10">
					<TD width="10"><FONT face="굴림"></FONT></TD>
					<TD align="right" colSpan="3" style="FONT-SIZE: 10pt"><FONT style="FONT-SIZE: 0pt" face="굴림">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</FONT></TD>
				</TR>
				<TR height="350">
					<TD width="10" align="center"><FONT face="굴림"></FONT></TD>
					<TD style="FONT-SIZE: 10pt" vAlign="top" colSpan="3" align="center"><FONT face="굴림"></FONT>
						<div align="center"><FONT face="굴림">
								<TABLE id="Table2" cellSpacing="0" cellPadding="3" width="100%" border="2" style="FONT-SIZE: 10pt"
									height="100%" borderColorDark="dimgray" borderColorLight="#ffffff">
									<TR height="30">
										<TD align="center" width="120">
											<asp:Label id="Label4" runat="server" Font-Bold="True" DESIGNTIMEDRAGDROP="457" ForeColor="ControlDarkDark">작 성 자</asp:Label></TD>
										<TD width="30%">
											<asp:TextBox id="RegistrationPerson" runat="server" Height="100%" Width="100%" BorderWidth="1px"
												BorderStyle="Solid" BorderColor="DimGray" MaxLength="5" BackColor="#EEEEE9"></asp:TextBox>
											<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" Display="None" ControlToValidate="RegistrationPerson"
												ErrorMessage="작성를 입력해주세요!"></asp:RequiredFieldValidator></TD>
										<TD id="TD1" align="center" width="150">
											<asp:Label id="Label5" runat="server" Font-Bold="True" ForeColor="ControlDarkDark">비밀번호</asp:Label></TD>
										<TD width="30%">
											<asp:TextBox id="Password" runat="server" Height="100%" Width="90px" BorderWidth="1px" BorderStyle="Solid"
												BorderColor="DimGray" TextMode="Password" BackColor="#EEEEE9"></asp:TextBox>
											<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" Display="None" ControlToValidate="Password"
												ErrorMessage="비밀번호를 입력해주세요!"></asp:RequiredFieldValidator></TD>
									</TR>
									<TR valign="middle" height="30">
										<TD vAlign="middle" align="center" width="120">
											<asp:Label id="Label2" runat="server" Font-Bold="True" ForeColor="ControlDarkDark">제&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;목</asp:Label></TD>
										<TD vAlign="middle" colSpan="3">
											<asp:TextBox id="Title" runat="server" Height="100%" Width="100%" BorderWidth="1px" BorderStyle="Solid"
												BorderColor="DimGray" MaxLength="33" BackColor="#EEEEE9"></asp:TextBox>
											<asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" Display="None" ControlToValidate="Title"
												ErrorMessage="제목을 입력해주세요!"></asp:RequiredFieldValidator></TD>
									</TR>
									<TR>
										<TD align="center" width="120">
											<asp:Label id="Label3" runat="server" Font-Bold="True" ForeColor="ControlDarkDark">내&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;용</asp:Label></TD>
										<TD colSpan="3">
											<asp:TextBox id="Content" runat="server" Height="100%" Width="100%" BorderWidth="1px" BorderStyle="Solid"
												BorderColor="DimGray" TextMode="MultiLine" BackColor="#EEEEE9"></asp:TextBox>
											<asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" Display="None" ControlToValidate="Content"
												ErrorMessage="내용을 입력해주세요!"></asp:RequiredFieldValidator></TD>
									</TR>
								</TABLE>
							</FONT>
						</div>
					</TD>
				</TR>
				<tr height="25" align="center">
					<td width="10"><FONT face="굴림"></FONT></td>
					<td align="right" colspan="3" style="FONT-SIZE: 10pt">
						<asp:ValidationSummary id="ValidationSummary1" runat="server" DisplayMode="List" ShowSummary="False" ShowMessageBox="True"></asp:ValidationSummary>
						<asp:Button id="Button1" runat="server" Text="취  소" Height="20px" Width="65px" CausesValidation="False"></asp:Button>
						<asp:Button id="Button2" runat="server" Text="등  록" Height="20px" Width="65px" CausesValidation="False"></asp:Button><FONT face="굴림">&nbsp;&nbsp;
						</FONT>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
