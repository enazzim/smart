<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="WebForm2.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.SystemInfoManagement.WebForm2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm2</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="Table" cellSpacing="0" cellPadding="0">
				<tr>
					<td style="HEIGHT: 77px">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; Z-INDEX: 103; BORDER-LEFT: #696969 2px solid; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top"><asp:label id="searchTitle" text="[검색조건]" Runat="server" Font-Size="10pt">[검색조건]</asp:label></LEGEND>
							<TABLE id="Table2" style="HEIGHT: 2px" cellSpacing="0" cellPadding="0" width="800">
								<TR>
									<TD align="right" width="40" height="30"><FONT face="굴림">모&nbsp; 듈&nbsp; </FONT>
									</TD>
									<TD align="right" width="100" height="30"><asp:dropdownlist id="DropDownList1" runat="server" Width="100px">
											<asp:ListItem Value=" ">- 선 택 -</asp:ListItem>
											<asp:ListItem Value="영업">영업</asp:ListItem>
											<asp:ListItem Value="생산">생산</asp:ListItem>
											<asp:ListItem Value="구매/외주">구매/외주</asp:ListItem>
											<asp:ListItem Value="경영정보">경영정보</asp:ListItem>
											<asp:ListItem Value="통계및지표">통계및지표</asp:ListItem>
											<asp:ListItem Value="기준정보">기준정보</asp:ListItem>
											<asp:ListItem Value="협력사">협력사</asp:ListItem>
											<asp:ListItem Value="커뮤니티">커뮤니티</asp:ListItem>
											<asp:ListItem Value="시스템정보">시스템정보</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD align="right" colSpan="3" height="30"><FONT face="굴림"></FONT><FONT face="굴림">&nbsp;</FONT>
										<asp:button id="bt_Search" runat="server" Height="20px" Width="60px" Text="검색"></asp:button><FONT face="굴림">&nbsp;
										</FONT>
									</TD>
								</TR>
								<TR>
									<TD align="right" width="70" colSpan="5" height="8"><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"></FONT></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</td>
				</tr>
				<tr>
					<td>
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top"><asp:label id="Label2" Runat="server" Font-Size="10pt" Width="72px">[검색결과]</asp:label></LEGEND>
							<TABLE id="Table3" style="WIDTH: 806px; HEIGHT: 446px" cellSpacing="1" cellPadding="1"
								width="806" border="0">
								<TR>
									<TD height="400"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="400px">
											<DisplayLayout RowHeightDefault="20px" Version="3.00" AllowColumnMovingDefault="OnServer" BorderCollapseDefault="Separate"
												AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1" NoDataMessage=""
												AllowUpdateDefault="Yes">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</AddNewBox>
												<Pager PageSize="20" Alignment="Center">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</Pager>
												<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray" Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<FrameStyle BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid" BackColor="Silver"
													Height="400px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<RowStyleDefault BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand></igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
								<TR>
									<TD align="right" height="30">&nbsp;
										<asp:button id="bt_Register" runat="server" Height="20px" Width="60px" Text="설   정"></asp:button></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</td>
				</tr>
			</table>
			&nbsp;</form>
	</body>
</HTML>
