<%@ Page language="c#" Codebehind="UserCompanyInput.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.SystemInfoManagement.UserCompanyInput" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>UserCompanyInput</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="../PopupWindows.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<FONT face="굴림">
				<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" height="550"
					cellSpacing="0" cellPadding="0" width="800" border="0">
					<TR>
						<TD vAlign="middle" align="center" width="800" background="../images/system-bg4.gif"></TD>
					</TR>
					<TR>
						<TD vAlign="bottom" align="center" width="800" background="../images/Logo2.gif" height="150">
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 0pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 184px"><LEGEND align="top">
									<asp:Label id="Label2" Font-Size="10pt" Runat="server"> [현&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;황]</asp:Label></LEGEND>
								<TABLE id="Table2" height="100%" cellSpacing="0" cellPadding="0" width="800" border="0">
									<TR>
										<TD align="right" width="120" noWrap>
											<asp:Label id="Label3" runat="server" Font-Size="9pt" ForeColor="Red">* 회 사 명 :</asp:Label></TD>
										<TD width="130">
											<asp:TextBox id="txtCompanyName" runat="server" Width="130px" Height="20px" BackColor="#EEEEE9"></asp:TextBox></TD>
										<TD align="right" width="70" style="FONT-SIZE: 9pt; COLOR: #ff0000">
											* 대표자명</TD>
										<TD width="100">
											<asp:TextBox id="txtPresidentName" runat="server" Width="80px" Height="20px" BackColor="#EEEEE9"></asp:TextBox></TD>
										<TD align="right" width="70" style="FONT-SIZE: 9pt; COLOR: #ff0000">
											* 사업자번호</TD>
										<TD width="100">
											<asp:TextBox id="txtBusinessCompanyNum" runat="server" Width="100px" Height="20px" BackColor="#EEEEE9"></asp:TextBox></TD>
										<TD align="right" width="70">
											<asp:Label id="Label9" runat="server" Font-Size="9pt">법인번호 :</asp:Label></TD>
										<TD align="right" width="100">
											<asp:TextBox id="txtCorporationRegistrationNum" runat="server" Width="100px" Height="20px" BackColor="#EEEEE9"></asp:TextBox></TD>
									</TR>
									<TR>
										<TD align="right" style="FONT-SIZE: 9pt; COLOR: #ff0000; HEIGHT: 27px" width="120" noWrap>
											* 업 태 :</TD>
										<TD width="130" rowSpan="1">
											<asp:TextBox id="txtBusinessClassification" runat="server" Width="100px" Height="20px" BackColor="#EEEEE9"></asp:TextBox></TD>
										<TD align="right" width="70" style="FONT-SIZE: 9pt; COLOR: #ff0000">
											* 종 목</TD>
										<TD width="100">
											<asp:TextBox id="txtBusinessItem" runat="server" Width="100px" Height="20px" BackColor="#EEEEE9"></asp:TextBox></TD>
										<TD width="340" colspan="4"></TD>
									</TR>
									<TR>
										<TD style="FONT-SIZE: 9pt; COLOR: red; HEIGHT: 30px" align="right" width="120" noWrap>
											* 사업장 주소 :</TD>
										<TD width="130" rowSpan="1">
											<asp:TextBox id="txtPost1" runat="server" Height="20px" Width="30px" MaxLength="3" ReadOnly="True"
												BackColor="#EEEEE9"></asp:TextBox>&nbsp;-
											<asp:TextBox id="txtPost2" runat="server" Width="30px" Height="20px" MaxLength="3" ReadOnly="True"
												BackColor="#EEEEE9"></asp:TextBox>&nbsp;
											<asp:Button id="btnPost1" runat="server" Height="20px" Width="20px" Text="..."></asp:Button></TD>
										<TD colSpan="6" width="510">
											<asp:TextBox id="txtBusinessCompanyAddress" runat="server" Width="540px" Height="20px" BackColor="#EEEEE9"></asp:TextBox></TD>
									</TR>
									<TR>
										<TD align="right" width="120" style="FONT-SIZE: 9pt; COLOR: #ff0000" noWrap>
											*&nbsp; 거래명세서 주소 :</TD>
										<TD width="130" rowSpan="1">
											<asp:TextBox id="txtPost3" runat="server" Height="20px" Width="30px" MaxLength="3" ReadOnly="True"
												BackColor="#EEEEE9"></asp:TextBox>&nbsp;-
											<asp:TextBox id="txtPost4" runat="server" Height="20px" Width="30px" MaxLength="3" ReadOnly="True"
												BackColor="#EEEEE9"></asp:TextBox>&nbsp;
											<asp:Button id="btnPost2" runat="server" Height="20px" Width="20px" Text="..."></asp:Button></TD>
										<TD colSpan="6" width="510">
											<asp:TextBox id="txtBusinessBillAddress" runat="server" Height="20px" Width="540px" BackColor="#EEEEE9"></asp:TextBox></TD>
									</TR>
									<TR>
										<TD align="right" width="120" style="FONT-SIZE: 9pt; COLOR: red" noWrap>
											*&nbsp; 세금계산서 주소 :</TD>
										<TD width="130" rowSpan="1">
											<asp:TextBox id="txtPost5" runat="server" Width="30px" Height="20px" MaxLength="3" ReadOnly="True"
												BackColor="#EEEEE9"></asp:TextBox>&nbsp;-
											<asp:TextBox id="txtPost6" runat="server" Width="30px" Height="20px" MaxLength="3" ReadOnly="True"
												BackColor="#EEEEE9"></asp:TextBox>&nbsp;
											<asp:Button id="btnPost3" runat="server" Width="20px" Height="20px" Text="..."></asp:Button></TD>
										<TD colSpan="6" width="510">
											<asp:TextBox id="txtTaxBillAddress" runat="server" Width="540px" Height="20px" BackColor="#EEEEE9"></asp:TextBox></TD>
									</TR>
									<TR>
										<TD align="right" colspan="8">
											<asp:Button id="btnOK" runat="server" Height="20px" Width="65px" Text="등   록" ForeColor="Black"></asp:Button>&nbsp;&nbsp;</TD>
									</TR>
								</TABLE>
							</FIELDSET>
						</TD>
					</TR>
				</TABLE>
				<asp:ValidationSummary id="ValidationSummary1" style="Z-INDEX: 103; LEFT: 24px; POSITION: absolute; TOP: 624px"
					runat="server" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
				<asp:RegularExpressionValidator id="RegularExpressionValidator1" style="Z-INDEX: 117; LEFT: 24px; POSITION: absolute; TOP: 592px"
					runat="server" ErrorMessage="사업자번호가 올바르게 입력되지 않았습니다." Display="None" ControlToValidate="txtBusinessCompanyNum"
					ValidationExpression="\d{3}-\d{2}-\d{5}"></asp:RegularExpressionValidator>
			</FONT>
		</form>
	</body>
</HTML>
