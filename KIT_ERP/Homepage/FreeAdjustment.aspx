<%@ Page language="c#" Codebehind="FreeAdjustment.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Homepage.FreeAdjustment" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FreeAdjustment</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
				if(confirm("답글도 모두함께 삭제됩니다. 삭제하시겠습니까?"))
					return true;
				else
					return false;
			}
		//-->
		</script>
	</HEAD>
	<body bgColor="whitesmoke" MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="FONT-SIZE: 0pt; Z-INDEX: 101; LEFT: 10px; WIDTH: 802px; POSITION: absolute; TOP: 0px; HEIGHT: 376px"
				cellSpacing="0" cellPadding="0" width="802" align="center" border="0">
				<TR height="30">
					<TD vAlign="middle" width="10"><FONT face="굴림"></FONT></TD>
					<TD vAlign="middle" align="center" colSpan="3">
						<TABLE id="Table1" style="FONT-SIZE: 0pt" height="30" cellSpacing="0" cellPadding="0" width="150"
							border="0">
							<TR height="30">
								<TD style="BORDER-BOTTOM: dimgray 2px solid" vAlign="bottom" align="center" height="30"><asp:label id="Label1" runat="server" Font-Size="11pt" ForeColor="SteelBlue" Font-Bold="True"></asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR height="27">
					<TD width="10"><FONT face="굴림"></FONT></TD>
					<TD style="FONT-SIZE: 10pt" align="right" colSpan="3"><FONT style="FONT-SIZE: 0pt" face="굴림"><asp:button id="Button3" runat="server" Text="답  변" Width="65px" Height="20px"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</FONT></TD>
				</TR>
				<TR height="330">
					<TD align="center" width="10"><FONT face="굴림"></FONT></TD>
					<TD style="FONT-SIZE: 10pt" vAlign="top" align="center" colSpan="3"><FONT face="굴림"></FONT>
						<div align="center"><FONT face="굴림">
								<TABLE id="Table2" style="FONT-SIZE: 10pt; WIDTH: 800px" height="100%" cellSpacing="0"
									borderColorDark="dimgray" cellPadding="3" width="800" borderColorLight="#ffffff" border="2">
									<TR height="30">
										<TD align="center" width="120"><asp:label id="Label2" runat="server" ForeColor="ControlDarkDark" Font-Bold="True">작 성 자</asp:label></TD>
										<TD align="center"><asp:label id="lblRegistrationPerson" runat="server"></asp:label></TD>
										<TD align="center"><asp:label id="Label9" runat="server" ForeColor="ControlDarkDark" Font-Bold="True">작 성 일</asp:label></TD>
										<TD align="center"><asp:label id="lblRegistrationDate" runat="server"></asp:label></TD>
										<TD align="center"><asp:label id="Label8" runat="server" ForeColor="ControlDarkDark" Font-Bold="True">조 회 수</asp:label></TD>
										<TD align="center"><asp:label id="lblHits" runat="server"></asp:label></TD>
									</TR>
									<TR height="30">
										<TD align="center" width="120"><asp:label id="Label3" runat="server" ForeColor="ControlDarkDark" Font-Bold="True">제&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;목</asp:label></TD>
										<TD colSpan="5"><asp:textbox id="Title" runat="server" Width="100%" Height="100%" BorderWidth="1px" BorderStyle="Solid"
												BorderColor="DimGray" MaxLength="33" BackColor="#EEEEE9"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="※ 제목을 입력해 주세요!" ControlToValidate="Title"
												Display="None"></asp:requiredfieldvalidator></TD>
									</TR>
									<TR>
										<TD align="center"><asp:label id="Label5" runat="server" ForeColor="ControlDarkDark" Font-Bold="True">내&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;용</asp:label></TD>
										<TD colSpan="5"><asp:textbox id="Content" runat="server" Width="100%" Height="100%" BorderWidth="1px" BorderStyle="Solid"
												BorderColor="DimGray" TextMode="MultiLine" BackColor="#EEEEE9"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="※ 내용을 입력해 주세요!" ControlToValidate="Content"
												Display="None"></asp:requiredfieldvalidator></TD>
									</TR>
								</TABLE>
							</FONT>
						</div>
					</TD>
				</TR>
				<tr align="center" height="25">
					<td width="10"><FONT face="굴림"></FONT></td>
					<td style="FONT-SIZE: 0pt" align="center" colSpan="3"><FONT face="굴림">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<TABLE id="Table3" style="FONT-SIZE: 10pt; WIDTH: 800px; HEIGHT: 8px" height="8" cellSpacing="0"
								cellPadding="0" width="800" border="0">
								<TR>
									<TD align="left"><asp:linkbutton id="LinkButton1" runat="server">목록</asp:linkbutton>&nbsp;
										<asp:hyperlink id="HyperLink3" runat="server">윗글</asp:hyperlink>&nbsp;
										<asp:hyperlink id="HyperLink4" runat="server">아랫글</asp:hyperlink></TD>
									<TD align="right"><asp:validationsummary id="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"
											DisplayMode="List"></asp:validationsummary>비밀번호&nbsp;
										<asp:textbox id="TextBox1" runat="server" Width="120px" BorderWidth="1px" BorderStyle="Solid"
											BorderColor="DimGray" TextMode="Password" Height="20px" BackColor="#EEEEE9"></asp:textbox><asp:button id="Button2" runat="server" Text="수  정" Width="65px" Height="20px" CausesValidation="False"></asp:button><asp:button id="Button1" runat="server" Text="삭  제" Width="65px" Height="20px" CausesValidation="False"></asp:button></TD>
								</TR>
							</TABLE>
							&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </FONT>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
