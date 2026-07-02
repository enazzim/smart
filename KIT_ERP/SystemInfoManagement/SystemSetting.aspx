<%@ Page language="c#" Codebehind="SystemSetting.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.SystemInfoManagement.SystemSetting" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SystemSetting</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 0pt; Z-INDEX: 101; LEFT: 8px; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; POSITION: absolute; TOP: 8px"><LEGEND align="top">
					<asp:label id="Label2" Runat="server" Font-Size="10pt"> [기능설정]</asp:label></LEGEND><br>
				<TABLE id="Table2" cellSpacing="0" cellPadding="0" border="0">
					<TR>
						<TD style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; FONT-SIZE: 0pt; PADDING-BOTTOM: 10px; HEIGHT: 146px"
							vAlign="top" align="left" colSpan="2">
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; PADDING-RIGHT: 10px; BORDER-TOP: dimgray 2px solid; PADDING-LEFT: 10px; PADDING-BOTTOM: 10px; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 150px">
								<P><LEGEND align="top">
										<asp:label id="Label4" Runat="server" Font-Size="10pt">[자재소요산출및 작업계획 수립 운영전략]</asp:label></LEGEND><BR>
									<BR>
									<asp:CheckBox id="CheckBox1" runat="server" Font-Size="10pt" AutoPostBack="True" Width="165px"
										Text="현재고량"></asp:CheckBox>
									<asp:CheckBox id="CheckBox2" runat="server" Font-Size="10pt" AutoPostBack="True" Width="240px"
										Text="외주재고에대한 자가재고 포함 여부"></asp:CheckBox>
									<asp:CheckBox id="CheckBox3" runat="server" Font-Size="10pt" AutoPostBack="True" Width="288px"
										Text="마이너스 재고 보상 위한 작업계획 추가 여부"></asp:CheckBox></P>
								<FONT face="굴림"></FONT>
								<P><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"></FONT><br>
									<FONT size="3">
										<asp:CheckBox id="CheckBox4" runat="server" Font-Size="10pt" Text="동일공정 묶음 여부" Width="165px"></asp:CheckBox>
										<asp:CheckBox id="CheckBox5" runat="server" Font-Size="10pt" Text="외주수량에 대한 자가수량변경여부" Width="248px"></asp:CheckBox>&nbsp;</FONT><FONT size="3">&nbsp;</FONT></P>
								<P>
									<asp:RadioButton id="RadioButton1" runat="server" Font-Size="10pt" AutoPostBack="True" Text="작업계획 산출 전략 I"
										GroupName="WorkPlan" Width="165px"></asp:RadioButton><FONT face="굴림" size="3">&nbsp;</FONT>
									<asp:RadioButton id="RadioButton2" runat="server" Font-Size="10pt" AutoPostBack="True" Text="작업계획 산출 전략 II"
										GroupName="WorkPlan"></asp:RadioButton><FONT face="굴림" size="3">&nbsp;&nbsp;&nbsp;&nbsp;
									</FONT>
									<asp:RadioButton id="RadioButton3" runat="server" Font-Size="10pt" Text="작업계획 산출 전략 III" GroupName="WorkPlan"></asp:RadioButton></P>
							</FIELDSET>
							<p></p>
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; PADDING-RIGHT: 10px; BORDER-TOP: dimgray 2px solid; PADDING-LEFT: 10px; PADDING-BOTTOM: 10px; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 60px">
								<P><LEGEND align="top">
										<asp:label id="Label3" Font-Size="10pt" Runat="server">[구매발주 운영전략]</asp:label></LEGEND><BR>
									<BR>
									<asp:CheckBox id="CheckBox8" runat="server" Font-Size="10pt" Text="구매발주미입고량" Width="144px" AutoPostBack="True"></asp:CheckBox><FONT size="3">
										<asp:CheckBox id="CheckBox7" runat="server" Font-Size="10pt" Text="발주간격수량" Width="144px" AutoPostBack="True"></asp:CheckBox>
										<asp:CheckBox id="CheckBox6" runat="server" Font-Size="10pt" Text="최소발주량" Width="128px" AutoPostBack="True"></asp:CheckBox>
										<asp:CheckBox id="CheckBox9" runat="server" Font-Size="10pt" Text="의뢰대기량" Width="128px" AutoPostBack="True"></asp:CheckBox>&nbsp;</FONT><FONT size="3">&nbsp;</FONT></P>
							</FIELDSET>
						</TD>
					</TR>
					<TR>
						<TD style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; FONT-SIZE: 0pt; PADDING-BOTTOM: 10px; PADDING-TOP: 10px; HEIGHT: 37px"
							vAlign="top" align="center">
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 60px"><LEGEND align="top">
									<asp:label id="Label5" Runat="server" Font-Size="10pt">[마이너스 재고 허용 여부]</asp:label></LEGEND>
								<TABLE id="Table8" cellSpacing="0" cellPadding="0" width="380" border="0">
									<TR>
										<TD style="PADDING-LEFT: 30px">
											<asp:radiobutton id="rdoMinusRowMaterialPermissionUsedTrue" runat="server" Font-Size="10pt" Text="예"
												GroupName="MinusRowMaterialPermissionUsed" Checked="True"></asp:radiobutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
											&nbsp;
											<asp:radiobutton id="rdoMinusRowMaterialPermissionUsedFalse" runat="server" Font-Size="10pt" Text="아니요"
												GroupName="MinusRowMaterialPermissionUsed"></asp:radiobutton></TD>
									</TR>
								</TABLE>
							</FIELDSET>
						</TD>
						<TD style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; FONT-SIZE: 0pt; PADDING-BOTTOM: 10px; PADDING-TOP: 10px; HEIGHT: 37px"
							vAlign="top" align="center">
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 60px"><LEGEND align="top">
									<asp:label id="Label1" Runat="server" Font-Size="10pt">[월 마감]</asp:label></LEGEND>
								<TABLE id="a" cellSpacing="0" cellPadding="0" width="380">
									<TBODY>
										<TR>
											<TD style="FONT-SIZE: 9pt" align="right" width="40" height="10"><FONT style="FONT-SIZE: 9pt" face="굴림">년도</FONT>
											</TD>
											<TD align="left" width="70">
												<asp:DropDownList id="dl_ClosingYear" runat="server" BackColor="#EEEEE9" Width="70px">
													<asp:ListItem Value="=선 택=">=선 택=</asp:ListItem>
													<asp:ListItem Value="2008">2008</asp:ListItem>
													<asp:ListItem Value="2009">2009</asp:ListItem>
													<asp:ListItem Value="2010">2010</asp:ListItem>
													<asp:ListItem Value="2011">2011</asp:ListItem>
													<asp:ListItem Value="2012">2012</asp:ListItem>
													<asp:ListItem Value="2013">2013</asp:ListItem>
													<asp:ListItem Value="2014">2014</asp:ListItem>
													<asp:ListItem Value="2015">2015</asp:ListItem>
												</asp:DropDownList></TD>
											<TD align="right" width="20"><FONT style="FONT-SIZE: 9pt" face="굴림">월</FONT>
											</TD>
											<TD align="left" width="70">
												<asp:dropdownlist id="dl_ClosingMonth" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Width="70px">
													<asp:ListItem Value="0">=선 택= </asp:ListItem>
													<asp:ListItem Value="1">1</asp:ListItem>
													<asp:ListItem Value="2">2</asp:ListItem>
													<asp:ListItem Value="3">3</asp:ListItem>
													<asp:ListItem Value="4">4</asp:ListItem>
													<asp:ListItem Value="5">5</asp:ListItem>
													<asp:ListItem Value="6">6</asp:ListItem>
													<asp:ListItem Value="7">7</asp:ListItem>
													<asp:ListItem Value="8">8</asp:ListItem>
													<asp:ListItem Value="9">9</asp:ListItem>
													<asp:ListItem Value="10">10</asp:ListItem>
													<asp:ListItem Value="11">11</asp:ListItem>
													<asp:ListItem Value="12">12</asp:ListItem>
												</asp:dropdownlist></TD>
											<TD align="right" width="100"><FONT style="FONT-SIZE: 9pt" face="굴림">마감지시자</FONT>
											</TD>
											<TD style="HEIGHT: 14px" align="left" width="80">
												<asp:dropdownlist id="dl_ClosingPerson" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Width="100px"></asp:dropdownlist></TD>
										</TR>
										<TR>
						</TD>
						<TD style="WIDTH: 110px" align="right" colSpan="2"><FONT face="굴림"></FONT></TD>
						<TD align="right" width="20"></TD>
						<TD align="left" width="70"><FONT face="굴림"></FONT></TD>
						<TD align="right" width="100"></TD>
						<TD align="right" width="80">
							<asp:button id="bt_Update" runat="server" Text="수  정" Width="60px" Height="20px"></asp:button></TD>
					</TR>
				</TABLE>
			</FIELDSET>
			</TD></TR>
			<TR>
				<TD style="PADDING-RIGHT: 10px; PADDING-BOTTOM: 10px; PADDING-TOP: 10px" vAlign="top"
					align="right" colSpan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:button id="Button2" runat="server" Font-Size="10pt" Text="설   정" Width="65px" Height="20px"></asp:button></TD>
			</TR>
			</TBODY></TABLE></FIELDSET>
		</form>
	</body>
</HTML>
