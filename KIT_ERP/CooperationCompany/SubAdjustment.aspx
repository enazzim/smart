<%@ Page language="c#" Codebehind="SubAdjustment.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.CooperationCompany.SubAdjustment" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SubAdjustment</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!--
			function Edit_Check()			
			{
				if ( confirm("수정겠습니까?") )
					return true;
				else
					return false;
			}
			
			function Delete_Check()
			{
				if(confirm("삭제하시겠습니까?"))
					return true;
				else
					return false;
			}
			
		//-->
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; WIDTH: 802px; POSITION: absolute; TOP: 10px; HEIGHT: 520px"
				height="520" cellSpacing="0" cellPadding="0" width="802" align="center" border="0">
				<TR>
					<td style="HEIGHT: 442px" width="20"></td>
					<TD style="HEIGHT: 442px" vAlign="top" colSpan="2">
						<TABLE id="Table2" style="WIDTH: 800px" borderColor="#000000" height="500" cellSpacing="0"
							borderColorDark="dimgray" cellPadding="3" width="800" align="center" borderColorLight="#ffffff"
							border="1">
							<TR>
								<TD style="WIDTH: 100px" bgColor="whitesmoke" height="30">
									<P align="center"><FONT style="FONT-WEIGHT: bold" face="굴림">작 성 자</FONT></P>
								</TD>
								<TD width="180" bgColor="whitesmoke" height="30">
									<P align="center"><asp:label id="lblRegistrationPerson" runat="server" ForeColor="ControlDarkDark"></asp:label></P>
								</TD>
								<TD width="100" bgColor="whitesmoke" height="30">
									<P align="center"><FONT style="FONT-WEIGHT: bold" face="굴림">작 성 일</FONT></P>
								</TD>
								<TD width="250" bgColor="whitesmoke" height="30">
									<P align="center"><asp:label id="lblRegistrationDate" runat="server" ForeColor="ControlDarkDark"></asp:label></P>
								</TD>
								<TD width="100" bgColor="whitesmoke" height="30">
									<P align="center"><FONT style="FONT-WEIGHT: bold" face="굴림">조 회 수</FONT></P>
								</TD>
								<TD width="150" bgColor="whitesmoke" height="30">
									<P align="center"><asp:label id="lblHits" runat="server" ForeColor="ControlDarkDark"></asp:label></P>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" vAlign="middle" bgColor="whitesmoke" height="30">
									<P align="center"><FONT style="FONT-WEIGHT: bold" face="굴림">제&nbsp;&nbsp;&nbsp; 목</FONT></P>
								</TD>
								<TD vAlign="middle" bgColor="whitesmoke" colSpan="6" height="30"><asp:textbox id="txtTitle" runat="server" BackColor="#EEEEE9" Font-Size="9pt" ReadOnly="True"
										BorderColor="Gray" BorderWidth="1px" BorderStyle="Inset" Width="99.9%" Height="20px" MaxLength="50"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="제목을 입력해주세요!" ControlToValidate="txtTitle"
										Display="None"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" bgColor="whitesmoke">
									<P align="center"><FONT style="FONT-WEIGHT: bold" face="굴림">내&nbsp;&nbsp;&nbsp; 용</FONT></P>
								</TD>
								<TD bgColor="whitesmoke" colSpan="6"><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"><asp:textbox id="txtContents" runat="server" BackColor="#EEEEE9" Font-Size="9pt" ReadOnly="True"
											BorderColor="Gray" BorderWidth="1px" BorderStyle="Inset" Width="100%" Height="100%" Rows="23" TextMode="MultiLine"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="내용을 입력해주세요!" ControlToValidate="txtContents"
											Display="None"></asp:requiredfieldvalidator></FONT></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td vAlign="middle" width="20" height="10"><FONT face="굴림"></FONT></td>
					<td vAlign="middle" align="left" width="440" height="10">
						<div align="left"><asp:validationsummary id="Validationsummary2" runat="server" DisplayMode="List" ShowMessageBox="True"
								ShowSummary="False"></asp:validationsummary><asp:linkbutton id="lbkList" runat="server">목록</asp:linkbutton>&nbsp;
							<asp:hyperlink id="hlnkPreContent" runat="server">윗글</asp:hyperlink>&nbsp;
							<asp:hyperlink id="hlnkNextContent" runat="server">아랫글</asp:hyperlink></FONT></div>
					</td>
					<td vAlign="middle" align="right" width="440" height="10"><asp:validationsummary id="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
							ShowSummary="False"></asp:validationsummary><asp:button id="btnEdit" runat="server" Font-Size="9pt" Width="65px" Height="20px" Enabled="False"
							Text="수   정"></asp:button><asp:button id="btnDelete" runat="server" Font-Size="9pt" Width="65px" Height="20px" Enabled="False"
							Text="삭   제"></asp:button></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
