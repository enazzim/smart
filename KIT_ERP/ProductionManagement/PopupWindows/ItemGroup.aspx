<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="ItemGroup.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.PopupWindows.ItemGroup" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ItemGroup</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			function Search()
			{				
				window.opener.SearchButton();self.close();	
			}
		</script>
	</HEAD>
	<body bgColor="#d4d0c8" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 5px; POSITION: absolute; TOP: 0px" cellSpacing="0"
				cellPadding="0" bgColor="#d4d0c8" border="0">
				<TR>
					<TD style="HEIGHT: 289px" width="900" colSpan="2"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="900px" Height="296px">
							<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
								TabDirection="TopToBottom" RowHeightDefault="20px" Version="3.00" AllowColumnMovingDefault="OnServer"
								HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
								RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="Edit">
								<AddNewBox>
									<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

									</Style>
								</AddNewBox>
								<Pager>
									<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

									</Style>
								</Pager>
								<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
									<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
								</HeaderStyleDefault>
								<FrameStyle Width="900px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
									Height="296px"></FrameStyle>
								<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
									<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
								</FooterStyleDefault>
								<EditCellStyleDefault BorderWidth="0px" BorderStyle="None" HorizontalAlign="Right"></EditCellStyleDefault>
								<RowAlternateStyleDefault Cursor="Hand"></RowAlternateStyleDefault>
								<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EDEFF6">
									<Padding Left="3px"></Padding>
									<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
								</RowStyleDefault>
							</DisplayLayout>
							<Bands>
								<igtbl:UltraGridBand>
									<Columns>
										<igtbl:UltraGridColumn HeaderText="기종" Key="Type" BaseColumnName="Type"></igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="품목번호" Key="" Width="120px" BaseColumnName="ItemNum">
											<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="117px" BaseColumnName="ItemName">
											<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="수주잔량" Key="OrderRemainQuantity" Width="60px" Format="" BaseColumnName="OrderRemainQuantity">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
												<Padding Right="4px"></Padding>
											</CellStyle>
											<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="계획잔량" Key="PlanRemainQuantity" Width="60px" Format="" BaseColumnName="PlanRemainQuantity">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
												<Padding Right="4px"></Padding>
											</CellStyle>
											<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="과부족량" Key="InsufficiencyQuantity" Width="60px" BaseColumnName="InsufficiencyQuantity">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
												<Padding Right="4px"></Padding>
											</CellStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="현재고" Key="StockQuantity" Width="60px" BaseColumnName="StockQuantity">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
												<Padding Right="4px"></Padding>
											</CellStyle>
											<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="금번계획량" Key="ThisPlanQuantity" Width="70px" BaseColumnName="ThisPlanQuantity">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
												<Padding Right="4px"></Padding>
											</CellStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="필요량" Key="NeedQuantity" Width="60px" Format="" BaseColumnName="NeedQuantity"
											AllowUpdate="Yes">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
												<Padding Right="4px"></Padding>
											</CellStyle>
											<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="고객생산일" Key="ProductionCompleteDate" EditorControlID="wdcMinDate" Width="90px"
											Type="Custom" Format="yyyy-MM-dd" BaseColumnName="ProductionCompleteDate" AllowUpdate="Yes">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="생산계획일" Key="ProductionBeginDate" EditorControlID="Webdatechooser1" Width="90px"
											Type="Custom" Format="yyyy-MM-dd" BaseColumnName="ProductionBeginDate" AllowUpdate="Yes">
											<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="분류" Key="PropertyClassification" Width="60px" BaseColumnName="PropertyClassification"></igtbl:UltraGridColumn>
									</Columns>
								</igtbl:UltraGridBand>
							</Bands>
						</igtbl:ultrawebgrid></TD>
				</TR>
				<TR>
					<td vAlign="top" width="80"><asp:button id="btnExcel" runat="server" Width="50px" Height="20px" Text="Excel" CommandName="Cancle"
							Font-Size="10pt"></asp:button></td>
					<TD vAlign="top" align="center" width="820"><FONT face="굴림"><asp:button id="btnCancle" runat="server" Width="65px" Height="20px" Text="취   소" CommandName="Cancle"
								Font-Size="10pt"></asp:button>&nbsp;&nbsp;
							<asp:button id="btnOK" runat="server" Width="65px" Height="20px" Text="확   인" CommandName="OK"
								Font-Size="10pt"></asp:button></FONT></TD>
				</TR>
				<tr>
					<td width="900" colSpan="2"><igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter><igsch:webdatechooser id="wdcMinDate" runat="server" Width="100px" Height="18px" Text=" " Font-Size="10pt"
							BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" NullDateLabel=" ">
							<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
								ShowTitle="False" ShowFooter="False">
								<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
								<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
								<DropDownStyle BackColor="White"></DropDownStyle>
								<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
							</CalendarLayout>
							<DropDownStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></DropDownStyle>
							<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
							<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
						</igsch:webdatechooser>
						<igsch:webdatechooser id="Webdatechooser1" runat="server" Width="100px" Height="20px" Text=" " Font-Size="10pt"
							BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray" NullDateLabel=" ">
							<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
								ShowTitle="False" ShowFooter="False">
								<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
								<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
								<DropDownStyle BackColor="White"></DropDownStyle>
								<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
							</CalendarLayout>
							<DropDownStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></DropDownStyle>
							<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
							<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
						</igsch:webdatechooser>
					</td>
					</TD></tr>
				<tr>
					<td colSpan="2" width="900"></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
