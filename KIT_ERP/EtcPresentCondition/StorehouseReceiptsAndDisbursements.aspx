<%@ Page language="c#" Codebehind="StorehouseReceiptsAndDisbursements.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.EtcPresentCondition.StorehouseReceiptsAndDisbursements" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>StorehouseReceiptsAndDisbursements</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!--
		function ResettxtBox()
		{
			
			ResetTextBox();
			var date = new Date();
			var year = date.getFullYear();
			var month = date.getMonth()+1;
			document.StorehouseReceiptsAndDisbursements.ddlStore.options[0].selected=true;
			
			
			for(var i=0;i<document.StorehouseReceiptsAndDisbursements.ddlYear.options.length;i++)
			{
				if(document.StorehouseReceiptsAndDisbursements.ddlYear.options[i].innerText == year)
				{
					document.StorehouseReceiptsAndDisbursements.ddlYear.options[i].selected=true;
					break;
				}
				else
					document.StorehouseReceiptsAndDisbursements.ddlYear.options[0].selected=true;
			}
			
			for(var i=0;i<document.StorehouseReceiptsAndDisbursements.ddlMonth.options.length;i++)
			{
				if(document.StorehouseReceiptsAndDisbursements.ddlMonth.options[i].innerText == month)
				{
					document.StorehouseReceiptsAndDisbursements.ddlMonth.options[i].selected=true;
					break;
				}
				else
					document.StorehouseReceiptsAndDisbursements.ddlMonth.options[0].selected=true;
			}
			
			
		}
		//-->
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="StorehouseReceiptsAndDisbursements" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; POSITION: absolute; TOP: 8px; LEFT: 8px" cellSpacing="0"
				cellPadding="0" width="800" border="0">
				<TR>
					<TD style="HEIGHT: 103px; PADDING-TOP: 10px" vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-BOTTOM: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-TOP: #696969 2px solid; BORDER-RIGHT: #696969 2px solid"><LEGEND align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD style="HEIGHT: 30px" align="right" width="70" height="30"><FONT face="굴림">창&nbsp; 
											고&nbsp; </FONT>
									</TD>
									<TD style="HEIGHT: 30px" align="left" width="100" height="30"><FONT face="굴림">
											<asp:dropdownlist id="ddlStore" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Width="100px" Height="20px">
												<asp:ListItem Value="원자재창고">원자재창고</asp:ListItem>
												<asp:ListItem Value="생산창고">생산창고</asp:ListItem>
												<asp:ListItem Value="외주창고">외주창고</asp:ListItem>
												<asp:ListItem Value="영업창고">영업창고</asp:ListItem>
												<asp:ListItem Value="보용품창고">보용품창고</asp:ListItem>
											</asp:dropdownlist></FONT></TD>
									<TD style="HEIGHT: 30px" vAlign="middle" align="left" width="630" colSpan="3" height="30"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30"><FONT face="굴림">년&nbsp; 도&nbsp;</FONT></TD>
									<TD width="100" height="30"><FONT face="굴림">
											<asp:dropdownlist id="ddlYear" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Width="70px" Height="20px">
												<asp:ListItem Value="2020">2020</asp:ListItem>
												<asp:ListItem Value="2021">2021</asp:ListItem>
												<asp:ListItem Value="2022">2022</asp:ListItem>
												<asp:ListItem Value="2023">2023</asp:ListItem>
												<asp:ListItem Value="2024">2024</asp:ListItem>
												<asp:ListItem Value="2025">2025</asp:ListItem>
												<asp:ListItem Value="2026">2026</asp:ListItem>
												<asp:ListItem Value="2027">2027</asp:ListItem>
											</asp:dropdownlist></FONT></TD>
									<TD align="center" width="15" height="30"><FONT face="굴림">월</FONT></TD>
									<TD width="100" height="30"><FONT face="굴림">
											<asp:dropdownlist id="ddlMonth" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Width="50px" Height="20px">
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
											</asp:dropdownlist></FONT></TD>
									<TD align="right" width="515" height="30"><FONT face="굴림"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
												type="button" value="초기화" name="btnReset">&nbsp;&nbsp;
											<asp:button id="btItemSearch" runat="server" Height="20px" Width="60px" Text="검 색"></asp:button>&nbsp;&nbsp;
										</FONT>
									</TD>
								</TR>
							</TABLE>
							<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="800">
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-BOTTOM: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-TOP: #696969 2px solid; BORDER-RIGHT: #696969 2px solid"><LEGEND align="top">[검색결과]</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800">
								<TR>
									<TD colSpan="6" height="450"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="450px" Width="100%">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" RowHeightDefault="20px" Version="3.00"
												SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" BorderCollapseDefault="Separate"
												AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed"
												CellClickActionDefault="RowSelect">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</AddNewBox>
												<Pager PageSize="20" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray" Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<FrameStyle Width="100%" BorderWidth="1px" Font-Size="9pt" Font-Names="Verdana" BorderColor="DimGray"
													BorderStyle="Solid" ForeColor="Transparent" BackColor="Silver" Height="450px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<SelectedHeaderStyleDefault Cursor="Hand"></SelectedHeaderStyleDefault>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="120px" BaseColumnName="ItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="120px" BaseColumnName="ItemName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정순서" Key="ProcessSequenceNum" Width="70px" BaseColumnName="ProcessSequenceNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정명" Key="ProcessName" Width="120px" BaseColumnName="ProcessName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="전월재고" Key="TransferStockQuantity" Width="95px" Format="###,###,###.##"
															BaseColumnName="TransferStockQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="입고량" Key="ReceiveStockQuantity" Width="90px" Format="###,###,###.##"
															BaseColumnName="ReceiveStockQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
															<HeaderStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="출고량" Key="OutStockQuantity" Width="90px" Format="###,###,###.##" BaseColumnName="OutStockQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="금월재고" Key="NowStockQuantity" Width="93px" Format="###,###,###.##" BaseColumnName="NowStockQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
															<HeaderStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 265px" align="left" colSpan="2" height="30"><FONT face="굴림">&nbsp;</FONT>
										<asp:button id="Button1" runat="server" Height="20px" Width="60px" Text="Excel" Font-Size="10pt"></asp:button></TD>
									<TD align="left" colSpan="4" height="30">&nbsp;&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
						<igtblexp:UltraWebGridExcelExporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:UltraWebGridExcelExporter>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
