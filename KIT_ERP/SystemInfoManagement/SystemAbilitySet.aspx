<%@ Page language="c#" Codebehind="SystemAbilitySet.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.SystemInfoManagement.SystemAbilitySet" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>SystemAbilitySet</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../GridCheck.js"></script>
		<script language="javascript" src="../PopupWindows.js"></script>
</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#f6f7f7">
		<form id="Form1" method="post" runat="server">
			<FONT face="굴림">
				<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" height="545"
					cellSpacing="0" cellPadding="0" width="800" border="0">
					<TBODY>
						<TR>
							<TD style="FONT-SIZE: 9pt; WIDTH: 641px; PADDING-TOP: 10px" vAlign="top" colSpan="2">
								<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 0pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 100%"><LEGEND align="top"><asp:label id="Label2" Runat="server" Font-Size="10pt"> [기능설정]</asp:label></LEGEND>
									<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" border="0">
										<TBODY>
											<TR>
												<TD style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; FONT-SIZE: 0pt; PADDING-BOTTOM: 10px; PADDING-TOP: 10px; HEIGHT: 41px"
													vAlign="top" colSpan="2">
													<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; PADDING-RIGHT: 10px; BORDER-TOP: dimgray 2px solid; PADDING-LEFT: 10px; PADDING-BOTTOM: 5px; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; PADDING-TOP: 10px; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top"><asp:label id="Label3" Runat="server" Font-Size="10pt">[자재소요량 산출]</asp:label></LEGEND>
														<TABLE id="Table4" style="HEIGHT: 20px" cellSpacing="0" cellPadding="0" width="100%" border="0">
															<TR>
																<TD width="133"><asp:checkbox id="chkPresentRowMaterialUsed" runat="server" Font-Size="10pt" Text="현재고량"></asp:checkbox></TD>
																<TD width="133"><asp:checkbox id="chkSafeyRowMaterialUsed" runat="server" Font-Size="10pt" Text="안전재고량"></asp:checkbox></TD>
																<TD width="134"><asp:checkbox id="chkOrderNonInStorehouseUsed" runat="server" Font-Size="10pt" Text="발주미입고량"></asp:checkbox></TD>
																<TD width="133"><asp:checkbox id="chkOrderGapInStorehouseUsed" runat="server" Font-Size="10pt" Text="발주간격수량"></asp:checkbox></TD>
																<TD width="133"><asp:checkbox id="chkMinimumGapUsed" runat="server" Font-Size="10pt" Text="최소발주량"></asp:checkbox></TD>
																<TD width="134"><asp:checkbox id="chkOrderRequestStandbyUsed" runat="server" Font-Size="10pt" Text="발주의뢰대기량"></asp:checkbox></TD>
															</TR>
														</TABLE>
													</FIELDSET>
												</TD>
											</TR>
											<TR>
												<TD style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; FONT-SIZE: 0pt; PADDING-BOTTOM: 10px; HEIGHT: 146px"
													vAlign="top" align="center" colSpan="2">
													<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; PADDING-RIGHT: 10px; BORDER-TOP: dimgray 2px solid; PADDING-LEFT: 10px; PADDING-BOTTOM: 10px; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 150px"><LEGEND align="top"><asp:label id="Label4" Runat="server" Font-Size="10pt">[WC별 운영전략]</asp:label></LEGEND><BR>
														<BR>
														<TABLE id="Table5" style="HEIGHT: 104px" cellSpacing="0" cellPadding="0" width="100%" border="0">
															<TR>
																<TD width="50%">
																	<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 99%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 100%"><LEGEND align="top"><asp:label id="Label6" Runat="server" Font-Size="10pt">[연속공정배치전략]</asp:label></LEGEND>
																		<TABLE id="Table6" height="85" cellSpacing="0" cellPadding="0" width="100%" border="0">
																			<TR>
																				<TD style="PADDING-LEFT: 30px"><asp:radiobutton id="rdoContinuityWorkProgressTrue" runat="server" Font-Size="10pt" Text="공정단위별 작업배치"
																						GroupName="ContinuityWorkProgress" Checked="True"></asp:radiobutton>&nbsp;&nbsp;&nbsp;&nbsp;
																					<asp:radiobutton id="rdoContinuityWorkProgressFalse" runat="server" Font-Size="10pt" Text="파이프라인 작업배치"
																						GroupName="ContinuityWorkProgress"></asp:radiobutton></TD>
																			</TR>
																		</TABLE>
																	</FIELDSET>
																</TD>
																<TD style="FONT-SIZE: 0pt" align="right" width="50%">
																	<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 99%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 100%"><LEGEND align="top"><asp:label id="Label7" Runat="server" Font-Size="10pt">[공정시작시간선택]</asp:label></LEGEND>
																		<TABLE id="Table7" height="85" cellSpacing="0" cellPadding="0" width="100%" border="0">
																			<TR>
																				<TD style="PADDING-LEFT: 30px"><asp:radiobutton id="rdoDailyWorkProgressTrue" runat="server" Font-Size="10pt" Text="작업장 일일 시작시간"
																						GroupName="DailyWorkProgress" Checked="True"></asp:radiobutton>&nbsp;&nbsp;&nbsp;
																					<asp:radiobutton id="rdoDailyWorkProgressFalse" runat="server" Font-Size="10pt" Text="작업장 가동 임의시간"
																						GroupName="DailyWorkProgress"></asp:radiobutton></TD>
																			</TR>
																		</TABLE>
																	</FIELDSET>
																</TD>
															</TR>
														</TABLE>
													</FIELDSET>
												</TD>
											</TR>
											<TR>
												<TD style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; FONT-SIZE: 0pt; PADDING-BOTTOM: 10px; PADDING-TOP: 10px; HEIGHT: 37px"
													vAlign="top" align="center">
													<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 60px"><LEGEND align="top"><asp:label id="Label5" Runat="server" Font-Size="10pt">[마이너스 재고 허용 여부]</asp:label></LEGEND>
														<TABLE id="Table8" cellSpacing="0" cellPadding="0" width="380"
															border="0">
															<TR>
																<TD style="PADDING-LEFT: 30px"><asp:radiobutton id="rdoMinusRowMaterialPermissionUsedTrue" runat="server" Font-Size="10pt" Text="예"
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
													<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid"><LEGEND align="top"><asp:label id="Label1" Runat="server" Font-Size="10pt">[월 마감]</asp:label></LEGEND>
														<table id="a" cellSpacing="0" cellPadding="0" width="380">
															<TBODY>
																<tr>
																	<TD style="FONT-SIZE: 9pt" align="right" width="40"><FONT style="FONT-SIZE: 9pt" face="굴림">년도</FONT> </TD>
																	<td align="left" width="70">
																		<asp:DropDownList id="dl_ClosingYear" runat="server" BackColor="#EEEEE9" Width="70px">
																		<asp:ListItem Value="=선 택=">=선 택=</asp:ListItem>
																		<asp:ListItem Value="2004">2004</asp:ListItem>
																		<asp:ListItem Value="2005">2005</asp:ListItem>
																		<asp:ListItem Value="2006">2006</asp:ListItem>
																		<asp:ListItem Value="2007">2007</asp:ListItem>
																		<asp:ListItem Value="2008">2008</asp:ListItem>
																		<asp:ListItem Value="2009">2009</asp:ListItem>
																		<asp:ListItem Value="2010">2010</asp:ListItem>
																		<asp:ListItem Value="2011">2011</asp:ListItem>
																		<asp:ListItem Value="2012">2012</asp:ListItem>
																		<asp:ListItem Value="2013">2013</asp:ListItem>
																		<asp:ListItem Value="2014">2014</asp:ListItem>
																		<asp:ListItem Value="2015">2015</asp:ListItem>
																		</asp:DropDownList>
																	</td>
																	<td align="right" width="20"><FONT style="FONT-SIZE: 9pt" face="굴림">월</FONT>
																	</td>
																	<td align="left" width="70"><asp:dropdownlist id="dl_ClosingMonth" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Width="70px">
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
																		</asp:dropdownlist></td>
																	<td align="right" width="100"><FONT style="FONT-SIZE: 9pt" face="굴림">마감지시자</FONT>
																	</td>
																	<td style="HEIGHT: 14px" align="left" width="80"><asp:dropdownlist id="dl_ClosingPerson" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Width="100px"></asp:dropdownlist></td></tr>
																<tr></TD>
												<td align="right" colspan="2" style="WIDTH: 110px"></td>
												<td align="right" width="20"></td>
												<td align="left" width="70"></td>
												<td align="right" width="100"></td>
												<td align="right" width="80">
													<asp:button id="bt_Update" runat="server" Height="20px" Width="60px" Text="수  정"></asp:button>
												</td>
											</TR></TBODY></TABLE></FIELDSET> 
												</TD></TR>
							<TR>
							<TD style="PADDING-RIGHT: 10px; PADDING-BOTTOM: 10px; PADDING-TOP: 10px" vAlign="top"
								align="right" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								<asp:button id="Button2" runat="server" Font-Size="10pt" Text="설   정" Width="65px" Height="20px"></asp:button></TD>
						</TR></TBODY></TABLE></FIELDSET> 
    </TD></TR></TBODY></TABLE></FONT></form>
	</body>
</HTML>
