<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="History.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.SystemInfoManagement.PopupWindows.History" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>History</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript"><!--
		function ResettxtBox()
		{
			var objChooser1 = igdrp_getComboById("wdcFromDate");
			var objChooser2 = igdrp_getComboById("wdcToDate");
			objChooser1.setValue(null);
			objChooser2.setValue(null);
		}
		-->
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="RealReversalBOM" method="post" runat="server">
			<TABLE id="Table2" style="POSITION: absolute; TOP: 10px; LEFT: 5px" cellSpacing="0" cellPadding="0"
				width="800" border="0">
				<TR>
					<TD>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD align="right" width="70" height="30">등록일&nbsp;</FONT>&nbsp;
								</TD>
								<TD width="115" height="30"><igsch:webdatechooser id="wdcFromDate" runat="server" BackColor="#EEEEE9" Height="20px" Width="115px"
										Text=" " BorderStyle="Solid" Font-Size="9pt" BorderColor="DimGray" NullDateLabel=" "></igsch:webdatechooser><CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
										DayNameFormat="FirstLetter"><SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
										<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
										<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
										<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
									</CALENDARLAYOUT><DROPDOWNSTYLE BorderColor="DimGray" BorderStyle="Solid" BorderWidth="1px"></DROPDOWNSTYLE><DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON><EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS></TD>
								<td align="center" width="15"><FONT face="굴림">~</FONT></td>
								<td align="left" width="130"><igsch:webdatechooser id="wdcToDate" runat="server" BackColor="#EEEEE9" Height="20px" Width="115px" Text=" "
										BorderStyle="Solid" Font-Size="9pt" BorderColor="DimGray" NullDateLabel=" "></igsch:webdatechooser><CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
										DayNameFormat="FirstLetter"><SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
										<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
										<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
										<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
									</CALENDARLAYOUT><DROPDOWNSTYLE BorderColor="DimGray" BorderStyle="Solid" BorderWidth="1px"></DROPDOWNSTYLE><DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON><EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS></td>
								<TD align="right" width="470" height="30"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
										type="button" value="초기화" name="btnReset">&nbsp;&nbsp;<FONT face="굴림">&nbsp;</FONT>
									<asp:button id="btSearch" runat="server" Height="20px" Width="60px" Text="검  색"></asp:button><FONT face="굴림">&nbsp;&nbsp;</FONT></TD>
							</TR>
						</table>
					</TD>
				</TR>
				<TR>
					<TD height="5"></TD>
				</TR>
				<TR>
					<TD height="10">
						<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="445px">
							<DisplayLayout ColFootersVisibleDefault="Yes" StationaryMargins="Header" AutoGenerateColumns="False"
								RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
								HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
								RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect">
								<AddNewBox>
									<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
									</Style>
								</AddNewBox>
								<Pager PageSize="18" StyleMode="ComboBox" Alignment="Center" AllowPaging="True">
									<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
									</Style>
								</Pager>
								<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray" Height="25px">
									<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
								</HeaderStyleDefault>
								<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="Verdana" BorderColor="DimGray"
									BorderStyle="Solid" BackColor="Silver" Height="445px"></FrameStyle>
								<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
									<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
								</FooterStyleDefault>
								<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
								<SelectedRowStyleDefault Cursor="Hand" Font-Bold="True" BorderColor="Gray" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
								<RowAlternateStyleDefault Cursor="Hand"></RowAlternateStyleDefault>
								<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
									<Padding Left="3px"></Padding>
									<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
								</RowStyleDefault>
							</DisplayLayout>
							<Bands>
								<igtbl:UltraGridBand>
									<Columns>
										<igtbl:UltraGridColumn HeaderText="등록자" Key="UserName" Width="200px" BaseColumnName="UserName">
											<HeaderStyle Height="25px"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="ID" Key="UserID" Width="170px" Hidden="True" BaseColumnName="UserID"></igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="일자" Key="Date" Width="200px" Format="yyyy-MM-dd" BaseColumnName="Date">
											<CellStyle VerticalAlign="Middle"></CellStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="창고" Key="Division" Width="200px" BaseColumnName="Division"></igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="사유" Key="Resion" Width="200px" BaseColumnName="Resion">
											<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										</igtbl:UltraGridColumn>
									</Columns>
								</igtbl:UltraGridBand>
							</Bands>
						</igtbl:ultrawebgrid></TD>
				</TR>
				<TR>
					<TD>&nbsp;</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
