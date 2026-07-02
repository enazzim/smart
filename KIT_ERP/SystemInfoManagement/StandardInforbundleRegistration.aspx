<%@ Page language="c#" Codebehind="StandardInforbundleRegistration.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.SystemInfoManagement.StandardInforbundleRegistration" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>StandardInforbundleRegistration</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<FONT face="굴림">
				<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" height="550"
					cellSpacing="0" cellPadding="0" width="800" border="0">
					<TR>
						<TD width="20"></TD>
						<TD style="PADDING-TOP: 10px" vAlign="bottom" align="center" width="800">
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 0pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 436px"
								DESIGNTIMEDRAGDROP="113">
								<LEGEND align="top">
									<asp:Label id="Label2" Font-Size="10pt" Runat="server"> [입력순서]</asp:Label></LEGEND>
								<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" border="0">
									<TR>
										<TD style="PADDING-LEFT: 30px" vAlign="middle" align="center" width="180" height="70">
											<asp:HyperLink id="HyperLink1" runat="server" ImageUrl="../images/1.gif" NavigateUrl="../ExcelDown/1.xls">HyperLink</asp:HyperLink></TD>
										<TD vAlign="middle" align="center" width="30" height="70">
											<asp:Image id="Image1" runat="server" ImageUrl="../images/right.gif"></asp:Image></TD>
										<TD vAlign="middle" align="center" width="180" height="70">
											<asp:HyperLink id="HyperLink2" runat="server" ImageUrl="../images/2.gif" NavigateUrl="../ExcelDown/2.xls">HyperLink</asp:HyperLink></TD>
										<TD vAlign="middle" align="center" width="30" height="70">
											<asp:Image id="Image2" runat="server" ImageUrl="../images/right.gif"></asp:Image></TD>
										<TD vAlign="middle" align="center" width="180" height="70">
											<asp:HyperLink id="HyperLink3" runat="server" ImageUrl="../images/3.gif" NavigateUrl="../ExcelDown/3.xls">HyperLink</asp:HyperLink></TD>
										<TD vAlign="middle" align="center" width="30" height="70">
											<asp:Image id="Image3" runat="server" ImageUrl="../images/right.gif"></asp:Image></TD>
										<TD style="PADDING-RIGHT: 30px" vAlign="middle" align="center" width="180" height="70">
											<asp:HyperLink id="HyperLink4" runat="server" ImageUrl="../images/4.gif" NavigateUrl="../ExcelDown/4.xls">HyperLink</asp:HyperLink></TD>
									</TR>
									<TR>
										<TD style="PADDING-LEFT: 30px" vAlign="middle" align="center" width="180" height="30"></TD>
										<TD vAlign="middle" align="center" width="30" height="30"></TD>
										<TD vAlign="middle" align="center" width="180" height="30"></TD>
										<TD vAlign="middle" align="center" width="30" height="30"></TD>
										<TD vAlign="middle" align="center" width="180" height="30"></TD>
										<TD vAlign="middle" align="center" width="30" height="30"></TD>
										<TD style="PADDING-RIGHT: 30px" vAlign="middle" align="center" width="180" height="30">
											<asp:Image id="Image10" runat="server" ImageUrl="../images/down.gif"></asp:Image></TD>
									</TR>
									<TR>
										<TD style="PADDING-LEFT: 30px" vAlign="middle" align="center" width="180" height="70">
											<asp:HyperLink id="HyperLink8" runat="server" ImageUrl="../images/8.gif" NavigateUrl="../ExcelDown/8.xls">HyperLink</asp:HyperLink></TD>
										<TD vAlign="middle" align="center" width="30" height="70">
											<asp:Image id="Image9" runat="server" ImageUrl="../images/left.gif"></asp:Image></TD>
										<TD vAlign="middle" align="center" width="180" height="70">
											<asp:HyperLink id="HyperLink7" runat="server" ImageUrl="../images/7.gif" NavigateUrl="../ExcelDown/7.xls">HyperLink</asp:HyperLink></TD>
										<TD vAlign="middle" align="center" width="30" height="70">
											<asp:Image id="Image8" runat="server" ImageUrl="../images/left.gif"></asp:Image></TD>
										<TD vAlign="middle" align="center" width="180" height="70">
											<asp:HyperLink id="HyperLink6" runat="server" ImageUrl="../images/6.gif" NavigateUrl="../ExcelDown/6.xls">HyperLink</asp:HyperLink></TD>
										<TD vAlign="middle" align="center" width="30" height="70">
											<asp:Image id="Image7" runat="server" ImageUrl="../images/left.gif"></asp:Image></TD>
										<TD style="PADDING-RIGHT: 30px" vAlign="middle" align="center" width="180" height="70">
											<asp:HyperLink id="HyperLink5" runat="server" ImageUrl="../images/5.gif" NavigateUrl="../ExcelDown/5.xls">HyperLink</asp:HyperLink></TD>
									</TR>
									<TR>
										<TD style="PADDING-LEFT: 30px" vAlign="middle" align="center" width="180" height="30">
											<asp:Image id="Image11" runat="server" ImageUrl="../images/down.gif"></asp:Image></TD>
										<TD vAlign="middle" align="center" width="30" height="30"></TD>
										<TD vAlign="middle" align="center" width="180" height="30"></TD>
										<TD vAlign="middle" align="center" width="30" height="30"></TD>
										<TD vAlign="middle" align="center" width="180" height="30"></TD>
										<TD vAlign="middle" align="center" width="30" height="30"></TD>
										<TD style="PADDING-RIGHT: 30px" vAlign="middle" align="center" width="180" height="30"></TD>
									</TR>
									<TR>
										<TD style="PADDING-LEFT: 30px" vAlign="middle" align="center" width="180" height="70">
											<asp:HyperLink id="HyperLink9" runat="server" ImageUrl="../images/9.gif" NavigateUrl="../ExcelDown/9.xls">HyperLink</asp:HyperLink></TD>
										<TD vAlign="middle" align="center" width="30" height="70"></TD>
										<TD vAlign="middle" align="center" width="180" height="70"></TD>
										<TD vAlign="middle" align="center" width="30" height="70"></TD>
										<TD vAlign="middle" align="center" width="180" height="70"></TD>
										<TD vAlign="middle" align="center" width="30" height="70"></TD>
										<TD style="PADDING-RIGHT: 30px" vAlign="middle" align="center" width="180" height="70"></TD>
									</TR>
									<TR>
										<TD style="PADDING-LEFT: 50px" width="100%" colSpan="7">
											<asp:Label id="Label3" runat="server" Font-Size="10pt" ForeColor="Red">※ 각 이미지를 Click할 경우 해당 양식지를 다운 받을 수 있습니다.<br>&nbsp;&nbsp;&nbsp;&nbsp;각 양식지를 다운받아 위의 입력순서에 맞춰서 입력하십시오.<br><br><br><b>
													☞ 입력순서 위반으로 인한 시스템 오류는 책임을 지지 않습니다.</b></asp:Label>
											<br>
										</TD>
									</TR>
								</TABLE>
							</FIELDSET>
							<br>
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 0pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 88px"><LEGEND align="top">
									<asp:Label id="Label1" Font-Size="10pt" Runat="server">[자료입력]</asp:Label></LEGEND>
								<TABLE id="Table3" style="HEIGHT: 64px" cellSpacing="0" cellPadding="0" width="800" border="0">
									<TR>
										<TD style="PADDING-LEFT: 30px" vAlign="middle" width="20%">
											<asp:DropDownList id="DropDownList1" runat="server" Width="150px" Height="20px" BackColor="#EEEEE9">
												<asp:ListItem Value="거래처 정보" Selected="True">거래처 정보</asp:ListItem>
												<asp:ListItem Value="품목 정보">품목 정보</asp:ListItem>
												<asp:ListItem Value="품목 구성 정보">품목 구성 정보</asp:ListItem>
												<asp:ListItem Value="작업장 정보">작업장 정보</asp:ListItem>
												<asp:ListItem Value="공정순서 정보">공정순서 정보</asp:ListItem>
												<asp:ListItem Value="설비 정보">설비 정보</asp:ListItem>
												<asp:ListItem Value="작업표준 정보">작업표준 정보</asp:ListItem>
												<asp:ListItem Value="단가 정보">단가 정보</asp:ListItem>
												<asp:ListItem Value="사용자 정보">사용자 정보</asp:ListItem>
												<asp:ListItem Value="진품목구성정보">진품목구성정보</asp:ListItem>
												<asp:ListItem Value="진공정정보">진공정정보</asp:ListItem>
												<asp:ListItem Value="진작업표준정보">진작업표준정보</asp:ListItem>
											</asp:DropDownList></TD>
										<TD style="PADDING-LEFT: 20px; WIDTH: 415px" vAlign="middle" width="415"><INPUT id="File1" style="WIDTH: 416px; HEIGHT: 22px; BACKGROUND-COLOR: #eeeee9" type="file"
												size="50" name="File1" runat="server"></TD>
										<TD style="PADDING-LEFT: 30px" vAlign="middle">
											<asp:Button id="Button1" runat="server" Width="65px" Height="20px" Text="정   리"></asp:Button></TD>
									</TR>
								</TABLE>
							</FIELDSET>
						</TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
