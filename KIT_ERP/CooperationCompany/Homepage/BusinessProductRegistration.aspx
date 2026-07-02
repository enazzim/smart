<%@ Page language="c#" Codebehind="BusinessProductRegistration.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Homepage.BusinessProductRegistration" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BusinessProductRegistration</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
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
	<body MS_POSITIONING="FlowLayout" bgcolor="whitesmoke" onload="document.Form1.Title.focus();">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="FONT-SIZE: 0pt; Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 0px"
				cellSpacing="0" cellPadding="0" width="800" border="0" align="center">
				<TR height="30">
					<TD vAlign="middle" width="10"><FONT face="굴림"></FONT></TD>
					<TD vAlign="middle" align="center" colSpan="3">
						<TABLE id="Table1" style="FONT-SIZE: 0pt" height="30" cellSpacing="0" cellPadding="0" width="180"
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
								<TABLE id="Table2" cellSpacing="0" cellPadding="3" width="800" border="2" style="FONT-SIZE: 10pt"
									height="100%" borderColorDark="dimgray" borderColorLight="white">
									<tr>
										<td align="center" width="120" height="30">
											<asp:Label id="Label5" runat="server" ForeColor="ControlDarkDark" Font-Bold="True">Category</asp:Label></td>
										<td height="30">
											<asp:DropDownList id="DropDownList1" runat="server"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp; 
											&nbsp;&nbsp;
											<asp:CheckBox id="CheckBox1" runat="server" ForeColor="ControlDarkDark" Font-Bold="True" Text="Category 직접입력 : "
												AutoPostBack="True"></asp:CheckBox>
											<asp:TextBox id="TextBox1" runat="server" MaxLength="10" BorderColor="Gray" BorderStyle="Solid"
												BorderWidth="1px" Width="180px" Visible="False" BackColor="#EEEEE9"></asp:TextBox></td>
									</tr>
									<TR>
										<TD align="center" width="120" height="30">
											<asp:Label id="Label2" runat="server" Font-Bold="True" ForeColor="ControlDarkDark">제&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;목</asp:Label></TD>
										<TD height="30">
											<asp:TextBox id="Title" runat="server" Height="100%" Width="100%" BorderWidth="1px" BorderStyle="Solid"
												BorderColor="DimGray" MaxLength="33" BackColor="#EEEEE9"></asp:TextBox>
											<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="제목을 입력해주세요!" ControlToValidate="Title"
												Display="None"></asp:RequiredFieldValidator></TD>
									</TR>
									<TR valign="middle">
										<TD vAlign="middle" align="center" width="120">
											<asp:Label id="Label3" runat="server" Font-Bold="True" ForeColor="ControlDarkDark">내&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;용</asp:Label></TD>
										<TD vAlign="middle">
											<asp:TextBox id="Content" runat="server" Height="100%" Width="100%" BorderWidth="1px" BorderStyle="Solid"
												TextMode="MultiLine" BorderColor="DimGray" BackColor="#EEEEE9"></asp:TextBox>
											<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="내용을 입력해주세요!" ControlToValidate="Content"
												Display="None"></asp:RequiredFieldValidator></TD>
									</TR>
									<TR>
										<TD align="center" width="120" height="30">
											<asp:Label id="Label4" runat="server" Font-Bold="True" ForeColor="ControlDarkDark">첨부파일</asp:Label></TD>
										<TD height="30"><INPUT id="File1" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 22px; BACKGROUND-COLOR: #eeeee9"
												type="file" size="84" name="File1" runat="server"></TD>
									</TR>
								</TABLE>
							</FONT>
						</div>
					</TD>
				</TR>
				<tr height="25" align="center">
					<td width="10"><FONT face="굴림"></FONT></td>
					<td align="right" colspan="3" style="FONT-SIZE: 10pt">
						<asp:ValidationSummary id="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"
							DisplayMode="List"></asp:ValidationSummary>
						<asp:Button id="Button1" runat="server" Text="취  소" Height="20px" Width="65px" CausesValidation="False"></asp:Button>
						<asp:Button id="Button2" runat="server" Text="등  록" Height="20px" Width="65px" CausesValidation="False"></asp:Button><FONT face="굴림">&nbsp;&nbsp;
						</FONT>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
