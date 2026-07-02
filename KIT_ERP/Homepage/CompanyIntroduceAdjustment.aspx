<%@ Page language="c#" Codebehind="CompanyIntroduceAdjustment.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Homepage.CompanyIntroduceAdjustment" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CompanyIntroduceAdjustment</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!--
			function Edit()
			{
				if(confirm("수정하시겠습니까?"))
					return true;
				else
					return false;
			}
			
			function Delete()
			{
				if(confirm("삭제하시겠습니까?"))
					return true;
				else
					return false;
			}
		//-->
		</script>
	</HEAD>
	<body MS_POSITIONING="FlowLayout" bgcolor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
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
								<TABLE id="Table2" style="FONT-SIZE: 10pt" height="100%" cellSpacing="0" cellPadding="3"
									width="100%" border="2" borderColorDark="dimgray" borderColorLight="#ffffff">
									<TR height="30">
										<TD width="120" align="center">
											<asp:Label id="Label2" runat="server" Font-Bold="True" ForeColor="ControlDarkDark">작 성 자</asp:Label></TD>
										<TD align="center">
											<asp:Label id="lblRegistrationPerson" runat="server"></asp:Label></TD>
										<TD align="center">
											<asp:Label id="Label9" runat="server" Font-Bold="True" ForeColor="ControlDarkDark">작 성 일</asp:Label></TD>
										<TD align="center">
											<asp:Label id="lblRegistrationDate" runat="server"></asp:Label></TD>
										<TD align="center">
											<asp:Label id="Label8" runat="server" Font-Bold="True" ForeColor="ControlDarkDark">조 회 수</asp:Label></TD>
										<TD align="center">
											<asp:Label id="lblHits" runat="server"></asp:Label></TD>
									</TR>
									<TR height="30">
										<TD width="120" align="center">
											<asp:Label id="Label3" runat="server" Font-Bold="True" ForeColor="ControlDarkDark">제&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;목</asp:Label></TD>
										<TD colSpan="5">
											<asp:TextBox id="Title" runat="server" Width="100%" Height="100%" BorderColor="DimGray" BorderStyle="Solid"
												BorderWidth="1px" ReadOnly="True" MaxLength="33" BackColor="#EEEEE9"></asp:TextBox>
											<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" Display="None" ControlToValidate="Title"
												ErrorMessage="제목을 입력해주세요!"></asp:RequiredFieldValidator></TD>
									</TR>
									<TR height="30">
										<TD width="120" align="center">
											<asp:Label id="Label4" runat="server" Font-Bold="True" ForeColor="ControlDarkDark">첨부파일</asp:Label></TD>
										<TD colSpan="2" width="35%">
											<asp:HyperLink id="HyperLink1" runat="server" Font-Bold="True">HyperLink</asp:HyperLink>
											<asp:Label id="Label6" runat="server" ForeColor="Blue"></asp:Label></TD>
										<TD colSpan="3">첨부파일변경 : <INPUT id="File1" type="file" size="24" name="File1" runat="server" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BORDER-BOTTOM: dimgray 1px solid; BACKGROUND-COLOR: #eeeee9"></TD>
									</TR>
									<TR>
										<TD align="center">
											<asp:Label id="Label5" runat="server" Font-Bold="True" ForeColor="ControlDarkDark">내&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;용</asp:Label></TD>
										<TD colSpan="5">
											<asp:TextBox id="Content" runat="server" Width="100%" Height="100%" TextMode="MultiLine" BorderColor="DimGray"
												BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" BackColor="#EEEEE9"></asp:TextBox>
											<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" Display="None" ControlToValidate="Content"
												ErrorMessage="내용을 입력해주세요!"></asp:RequiredFieldValidator></TD>
									</TR>
								</TABLE>
							</FONT>
						</div>
					</TD>
				</TR>
				<tr height="25" align="center">
					<td width="10"><FONT face="굴림"></FONT></td>
					<td align="center" colspan="3" style="FONT-SIZE: 0pt"><FONT face="굴림">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<TABLE id="Table3" style="FONT-SIZE: 10pt" height="100%" cellSpacing="0" cellPadding="0"
								width="100%" border="0">
								<TR>
									<TD align="left">
										<asp:LinkButton id="LinkButton1" runat="server">목록</asp:LinkButton>&nbsp;
										<asp:HyperLink id="HyperLink3" runat="server">윗글</asp:HyperLink>&nbsp;
										<asp:HyperLink id="HyperLink4" runat="server">아랫글</asp:HyperLink></TD>
									<TD align="right">
										<asp:ValidationSummary id="ValidationSummary1" runat="server" DisplayMode="List" ShowSummary="False" ShowMessageBox="True"></asp:ValidationSummary>
										<asp:Button id="Button2" runat="server" Text="수  정" Width="65px" Height="20px" Enabled="False"
											CausesValidation="False"></asp:Button>
										<asp:Button id="Button1" runat="server" Text="삭  제" Width="65px" Height="20px" Enabled="False"
											CausesValidation="False"></asp:Button>&nbsp;&nbsp;</TD>
								</TR>
							</TABLE>
							&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </FONT>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
