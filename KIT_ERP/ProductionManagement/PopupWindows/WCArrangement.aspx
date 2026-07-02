<%@ Page language="c#" Codebehind="WCArrangement.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.PopupWindows.WCArrangement" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WC 운영전략 선택</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; Z-INDEX: 101; LEFT: 8px; BORDER-LEFT: dimgray 2px solid; WIDTH: 300px; BORDER-BOTTOM: dimgray 2px solid; POSITION: absolute; TOP: 8px; HEIGHT: 216px"
				align="left"><LEGEND>[WC별 운영전략 선택]</LEGEND><div>
					<TABLE id="table1" style="WIDTH: 300px; HEIGHT: 92px" width="304" cellSpacing="0" cellPadding="0">
						<TR>
							<TD width="300" valign="middle" style="HEIGHT: 88px"><FONT face="굴림">
									<FIELDSET style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 296px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 64px"
										align="left"><LEGEND>[연속공정배치전략]</LEGEND>
										<DIV>
											<asp:RadioButton id="RadioButton1" runat="server" Text="공정단위별 작업배치" Font-Size="X-Small" GroupName="RadioWC"></asp:RadioButton><br>
											<asp:RadioButton id="RadioButton2" runat="server" Text="파이프라인 작업 배치" Font-Size="X-Small" GroupName="RadioWC"></asp:RadioButton></DIV>
									</FIELDSET>
								</FONT>
							</TD>
						</TR>
						<tr>
							<TD width="300" valign="middle"><FONT face="굴림">
									<FIELDSET style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; WIDTH: 296px; BORDER-BOTTOM: dimgray 1px solid; HEIGHT: 72px"
										align="left"><LEGEND>[공정시작시간선택]</LEGEND>
										<DIV><asp:RadioButton id="Radiobutton3" runat="server" Text="작업장 일일 시작시간" Font-Size="X-Small" GroupName="RadioTime"></asp:RadioButton><br>
											<asp:RadioButton id="Radiobutton4" runat="server" Text="작업장 가동 임의시간" Font-Size="X-Small" GroupName="RadioTime"></asp:RadioButton></DIV>
									</FIELDSET>
								</FONT>
							</TD>
						</tr>
					</TABLE>
				</div>
			</FIELDSET>
			&nbsp;&nbsp;
			<asp:Button id="Button1" style="Z-INDEX: 102; LEFT: 264px; POSITION: absolute; TOP: 200px" runat="server"
				Height="20px" Text="확 인"></asp:Button>
		</form>
	</body>
</HTML>
