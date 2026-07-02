<%@ Page language="c#" Codebehind="SubDataAdjustment.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.CooperationCompany.SubDataAdjustment" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SubDataAdjustment</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
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
			if ( confirm("수정겠습니까?") )
				return true;
			else
				return false;
		}
		
		function ContentDelete_Check()
		{
			if(confirm("삭제하시겠습니까?"))
				return true;
			else
				return false;
		}
		//-->
		</script>
	</HEAD>
	<body bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" borderColor="#0"
				cellSpacing="0" cellPadding="0" width="800" align="center" border="0" height="550">
				<TR>
					<td width="20"></td>
					<TD vAlign="top">
						<TABLE id="Table2" borderColor="#000000" cellSpacing="0" cellPadding="0" width="805" align="center"
							border="1" borderColorDark="dimgray" borderColorLight="#ffffff" height="536" style="WIDTH: 805px; HEIGHT: 536px">
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
								<TD width="250" bgColor="whitesmoke" colSpan="2" height="30">
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
									<P align="center"><FONT style="FONT-WEIGHT: bold" face="굴림">제&nbsp;&nbsp; &nbsp;목</FONT></P>
								</TD>
								<TD vAlign="middle" bgColor="whitesmoke" colSpan="7" height="30"><asp:textbox id="txtTitle" runat="server" MaxLength="50" Width="99.9%" Height="20px" BorderColor="Gray"
										BorderStyle="Inset" BorderWidth="1px" ReadOnly="True" Font-Size="9pt" BackColor="#EEEEE9"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" Display="None" ControlToValidate="txtTitle"
										ErrorMessage="제목을 입력해주세요!"></asp:requiredfieldvalidator></TD>
							</TR>
							<tr>
								<td vAlign="middle" align="center" bgColor="whitesmoke" height="30"><FONT style="FONT-WEIGHT: bold" face="굴림">첨부파일</FONT></td>
								<td vAlign="middle" align="left" bgColor="whitesmoke" colSpan="2" height="30"><FONT face="굴림">&nbsp;
										<asp:hyperlink id="HyperLink1" runat="server" Font-Bold="True">HyperLink</asp:hyperlink><asp:label id="Label7" runat="server" ForeColor="Blue"></asp:label></FONT></td>
								<td vAlign="middle" align="left" bgColor="whitesmoke" colSpan="4" height="30"><FONT face="굴림" style="FONT-WEIGHT: bold">&nbsp; 
										첨부파일변경: <INPUT id="File1" style="BORDER-RIGHT: gray 1px solid; BORDER-TOP: gray 1px solid; BORDER-LEFT: gray 1px solid; WIDTH: 303px; BORDER-BOTTOM: gray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: #eeeee9"
											type="file" size="31" name="File1" runat="server"></FONT></td>
							</tr>
							<TR>
								<TD style="WIDTH: 100px; HEIGHT: 396px" bgColor="whitesmoke">
									<P align="center"><FONT style="FONT-WEIGHT: bold" face="굴림">내&nbsp;&nbsp;&nbsp; 용</FONT></P>
								</TD>
								<TD bgColor="whitesmoke" colSpan="7" style="HEIGHT: 396px"><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"><asp:textbox id="txtContents" runat="server" Width="100%" Height="100%" TextMode="MultiLine"
											Rows="23" BorderColor="Gray" BorderStyle="Inset" BorderWidth="1px" ReadOnly="True" Font-Size="9pt" BackColor="#EEEEE9"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" Display="None" ControlToValidate="txtContents"
											ErrorMessage="내용을 입력해주세요!"></asp:requiredfieldvalidator></FONT></TD>
							</TR>
							<tr>
								<td colspan="8">
									<table id="aaa" width="800" cellpadding="0" cellspacing="0">
										<tr>
											<td align="left">
												<asp:validationsummary id="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="List"
													ShowSummary="False" Width="80px"></asp:validationsummary><asp:linkbutton id="lbkList" runat="server">목록</asp:linkbutton>&nbsp;
												<asp:hyperlink id="hlnkPreContent" runat="server">윗글</asp:hyperlink>&nbsp;
												<asp:hyperlink id="hlnkNextContent" runat="server">아랫글</asp:hyperlink>
											</td>
											<td align="right">
												<asp:button id="btnEdit" runat="server" Width="65px" Height="20px" Enabled="False" Text="수   정"
													CausesValidation="False"></asp:button><asp:button id="btnDelete" runat="server" Width="65px" Height="20px" Enabled="False" Text="삭   제"
													CausesValidation="False"></asp:button></FONT>&nbsp;
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
