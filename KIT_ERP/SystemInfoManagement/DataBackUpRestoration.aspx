<%@ Page language="c#" Codebehind="DataBackUpRestoration.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.SystemInfoManagement.DataBackUpRestoration" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DataBackUpRestoration</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../GridCheck.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<FONT face="굴림">
				<TABLE id="Table1" style="Z-INDEX: 101; POSITION: absolute; TOP: 0px; LEFT: 10px" height="550"
					cellSpacing="0" cellPadding="0" width="800" border="0">
					<TR>
						<TD width="10"></TD>
						<TD style="PADDING-BOTTOM: 0px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; HEIGHT: 10px; PADDING-TOP: 10px"
							vAlign="top" align="center" width="800">&nbsp;<BR>
							<FIELDSET style="BORDER-BOTTOM: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; HEIGHT: 462px; FONT-SIZE: 0pt; BORDER-TOP: dimgray 2px solid; BORDER-RIGHT: dimgray 2px solid"><LEGEND align="top">
									<asp:label id="Label2" Font-Size="10pt" Runat="server">[백업/복구]</asp:label></LEGEND>
								<TABLE id="Table3" height="500" cellSpacing="0" cellPadding="0" width="800" border="0">
									<TR>
										<TD vAlign="top" align="center" colSpan="3" height="335" style="HEIGHT: 335px">
											<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="100%" Height="432px">
												<DisplayLayout AutoGenerateColumns="False" AllowSortingDefault="OnClient" RowHeightDefault="20px"
													Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti"
													BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1"
													CellClickActionDefault="RowSelect">
													<AddNewBox>
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														</Style>
													</AddNewBox>
													<Pager>
														<Style VerticalAlign="Middle" BorderWidth="1px" BorderStyle="Solid" HorizontalAlign="Center"
															BackColor="LightGray">
														</Style>
													</Pager>
													<HeaderStyleDefault Font-Bold="True" BorderColor="Black" BorderStyle="Solid" ForeColor="Black" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</HeaderStyleDefault>
													<GroupByRowStyleDefault ForeColor="Black" BackColor="#95B9F0"></GroupByRowStyleDefault>
													<RowSelectorStyleDefault BorderStyle="Solid"></RowSelectorStyleDefault>
													<FrameStyle Width="100%" Cursor="Default" BorderWidth="1px" Font-Size="10pt" Font-Names="굴림"
														BorderStyle="Solid" BackColor="Silver" Height="432px"></FrameStyle>
													<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</FooterStyleDefault>
													<ClientSideEvents ColumnHeaderClickHandler="ColumnHeaderClickHandler"></ClientSideEvents>
													<GroupByBox>
														<Style BackColor="#B5CFF7">
														</Style>
														<BandLabelStyle ForeColor="White" BackColor="#6372D4"></BandLabelStyle>
													</GroupByBox>
													<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
													<SelectedGroupByRowStyleDefault BorderColor="Yellow" BackColor="Yellow"></SelectedGroupByRowStyleDefault>
													<SelectedRowStyleDefault Font-Bold="True" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
													<RowAlternateStyleDefault BackColor="LightSteelBlue"></RowAlternateStyleDefault>
													<RowStyleDefault BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" ForeColor="Black" BackColor="#EBEFF6">
														<Padding Left="3px"></Padding>
														<BorderDetails ColorTop="Gray" WidthLeft="0px" WidthTop="0px" ColorLeft="Gray"></BorderDetails>
													</RowStyleDefault>
												</DisplayLayout>
												<Bands>
													<igtbl:UltraGridBand AllowUpdate="No">
														<Columns>
															<igtbl:UltraGridColumn HeaderText="선택" Key="chk" Width="40px" Type="CheckBox" HeaderClickAction="Select"
																BaseColumnName="chk" AllowUpdate="Yes">
																<CellStyle HorizontalAlign="Center"></CellStyle>
																<HeaderStyle Height="25px"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="백업일자" Key="RegisterDate" Width="200px" HeaderClickAction="SortMulti"
																BaseColumnName="RegisterDate">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="등록자" Key="Register" Width="120px" HeaderClickAction="SortMulti" BaseColumnName="Register">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="파일명" Key="Filename" Width="200px" HeaderClickAction="SortMulti" BaseColumnName="Filename">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="백업사유" Key="BackUpReason" Width="235px" BaseColumnName="BackUpReason"></igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn Key="idx" Hidden="True" BaseColumnName="idx"></igtbl:UltraGridColumn>
														</Columns>
														<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
															<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
														</RowTemplateStyle>
														<RowEditTemplate>
															<P align="center">&nbsp;</P>
														</RowEditTemplate>
													</igtbl:UltraGridBand>
												</Bands>
											</igtbl:ultrawebgrid></TD>
									</TR>
									<TR>
										<TD vAlign="top" align="center">
											<TABLE id="Table2" style="BORDER-BOTTOM: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 378px; HEIGHT: 40px; BORDER-TOP: dimgray 2px solid; BORDER-RIGHT: dimgray 2px solid"
												cellSpacing="0" cellPadding="0" width="378" border="1">
												<TR>
													<TD style="WIDTH: 110px" align="center" height="25">
														<asp:Label id="Label1" runat="server" Font-Size="10pt">백 업 일 자</asp:Label></TD>
													<TD align="center" height="25">
														<asp:Label id="Label5" runat="server" Font-Size="10pt">Label</asp:Label></TD>
												</TR>
												<TR>
													<TD style="WIDTH: 110px" align="center" height="25">
														<asp:Label id="Label3" runat="server" Font-Size="10pt">등&nbsp;&nbsp;&nbsp;록&nbsp;&nbsp;&nbsp;자</asp:Label></TD>
													<TD align="center" height="25">
														<asp:Label id="Label6" runat="server" Font-Size="10pt">Label</asp:Label></TD>
												</TR>
												<TR>
													<TD style="WIDTH: 110px" align="center" height="25">
														<asp:Label id="Label4" runat="server" Font-Size="10pt">파&nbsp;&nbsp;&nbsp;일&nbsp;&nbsp;&nbsp;명</asp:Label></TD>
													<TD align="center" height="25">
														<asp:Label id="Label7" runat="server" Font-Size="10pt">Label</asp:Label></TD>
												</TR>
											</TABLE>
										</TD>
										<TD vAlign="middle" align="center" colSpan="2" height="100">
											<table id="Table4" cellpadding="0" cellspacing="0" border="0" width="400">
												<tr>
													<td width="100" align="right" height="30">
														백업사유 :&nbsp;
													</td>
													<td width="300" height="30">
														<asp:TextBox id="TextBox1" runat="server" Width="300px" BackColor="#EEEEE9"></asp:TextBox>
													</td>
												</tr>
												<tr>
													<td colspan="2" align="right" height="30">
														<asp:button id="Button2" runat="server" Font-Size="10pt" Height="20px" Width="65px" Text="백   업"></asp:button>
														<asp:button id="Button3" runat="server" Font-Size="10pt" Height="20px" Width="65px" Text="삭   제"></asp:button>
														<asp:button id="Button1" runat="server" Font-Size="10pt" Height="20px" Width="65px" Text="복   원"></asp:button></td>
												</tr>
											</table>
										</TD>
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
