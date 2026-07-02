<%@ Page language="c#" Codebehind="ItemState.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.EtcPresentCondition.ItemState" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ItemState</title>
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
			ResetBox();
			var date = new Date();
			var year = date.getFullYear();
			var month = date.getMonth()+1;
			document.CustomerPurchaseTotalMonthResult.ddlDivision.options[0].selected=true;
			
			
			document.CustomerPurchaseTotalMonthResult.ddlItemGroup1.options[0].selected = true;
			document.CustomerPurchaseTotalMonthResult.ddlItemGroup2.options[0].selected = true;
			document.CustomerPurchaseTotalMonthResult.ddlItemGroup3.options[0].selected = true;		
			document.CustomerPurchaseTotalMonthResult.ddlItemGroup4.options[0].selected = true;			
			
			for(var i=0;i<document.CustomerPurchaseTotalMonthResult.ddlYear.options.length;i++)
			{
				if(document.CustomerPurchaseTotalMonthResult.ddlYear.options[i].innerText == year)
				{
					document.CustomerPurchaseTotalMonthResult.ddlYear.options[i].selected=true;
					break;
				}
				else
					document.CustomerPurchaseTotalMonthResult.ddlYear.options[0].selected=true;
			}
			
			for(var i=0;i<document.CustomerPurchaseTotalMonthResult.ddlMon.options.length;i++)
			{
				if(document.CustomerPurchaseTotalMonthResult.ddlMon.options[i].innerText == month)
				{
					document.CustomerPurchaseTotalMonthResult.ddlMon.options[i].selected=true;
					break;
				}
				else
					document.CustomerPurchaseTotalMonthResult.ddlMon.options[0].selected=true;
			}
			
		}
		//-->
		</script>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="ItemState" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; POSITION: absolute; TOP: 8px; LEFT: 8px" cellSpacing="0"
				cellPadding="0" width="800" border="0">
				<TR>
					<TD style="HEIGHT: 103px; PADDING-TOP: 10px" vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-BOTTOM: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-TOP: #696969 2px solid; BORDER-RIGHT: #696969 2px solid"><LEGEND align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<td width="40" align="right"><FONT face="굴림">구분&nbsp; </FONT>
									</td>
									<td width="70">
										<asp:DropDownList id="ddlDivision" runat="server" Width="70px" BackColor="#EEEEE9">
											<asp:ListItem Value="전체">전체</asp:ListItem>
											<asp:ListItem Value="구매">구매</asp:ListItem>
											<asp:ListItem Value="외주">외주</asp:ListItem>
											<asp:ListItem Value="기타">기타</asp:ListItem>
											<asp:ListItem Value="부자재">부자재</asp:ListItem>
										</asp:DropDownList>
									</td>
									<td width="40" align="right">년도
									</td>
									<td width="60">
										<asp:dropdownlist id="ddlYear" runat="server" Width="55px" BackColor="#EEEEE9">
											<asp:ListItem Value="선택">선택</asp:ListItem>
											<asp:ListItem Value="2008">2015</asp:ListItem>
											<asp:ListItem Value="2009">2016</asp:ListItem>
											<asp:ListItem Value="2010">2017</asp:ListItem>
											<asp:ListItem Value="2011">2018</asp:ListItem>
											<asp:ListItem Value="2012">2019</asp:ListItem>
											<asp:ListItem Value="2013">2020</asp:ListItem>
										</asp:dropdownlist>
									</td>
									<td width="50" align="right">월
									</td>
									<td width="50">
										<asp:dropdownlist id="ddlMon" runat="server" Width="50px" BackColor="#EEEEE9">
											<asp:ListItem Value="선택">선택</asp:ListItem>
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
										</asp:dropdownlist>
									</td>
									<td width="490" align="right" colspan="5">
										<FONT face="굴림"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
												type="button" value="초기화" name="btnReset">
											<asp:button id="btComSearch" runat="server" Text="검  색" Width="60px" Height="20px"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
										</FONT>
									</td>
								</TR>
								<TR>
									<TD align="right" width="40" height="10"></TD>
									<TD width="70" height="10"></TD>
									<TD align="right" width="40" height="10"></TD>
									<TD width="60" height="10"></TD>
									<TD align="right" width="50" height="10"></TD>
									<TD width="50" height="10"></TD>
									<TD align="right" width="490" colSpan="5" height="10"></TD>
								</TR>
							</TABLE>
							<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="800">
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" width="800" height="10"></TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-BOTTOM: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-TOP: #696969 2px solid; BORDER-RIGHT: #696969 2px solid"><LEGEND align="top">[검색결과]</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800">
								<TR>
									<TD colSpan="6" height="450"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="100%" Height="467px">
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
													BorderStyle="Solid" ForeColor="Transparent" BackColor="Silver" Height="467px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<SelectedHeaderStyleDefault Cursor="Hand"></SelectedHeaderStyleDefault>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BackColor="Transparent"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" Width="110px" BaseColumnName="CompanyName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" BaseColumnName="BusinessRegistrationNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="110px" BaseColumnName="ItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="110px" BaseColumnName="ItemName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정명" Key="ProcessName" Width="70px" BaseColumnName="ProcessName">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정코드" Key="ProcessCode" Hidden="True" BaseColumnName="ProcessCode"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="전월이월재고" Key="TransferStockQuantity" Width="80px" Format="###,###,###"
															BaseColumnName="TransferStockQuantity">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="금월입고" Key="ThisInStoreQuantity" Width="80px" Format="###,###,###.#"
															BaseColumnName="ThisInStoreQuantity">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="기타입고" Key="EtcInStoreQuantity" Width="80px" Format="###,###,###.#" BaseColumnName="EtcInStoreQuantity">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="기타출고" Key="EtcOutStockQuantity" Width="80px" Format="###,###,###.#"
															BaseColumnName="EtcOutStockQuantity">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="금월출고" Key="ThisOutStockQuantity" Width="80px" Format="###,###,###.#"
															BaseColumnName="ThisOutStockQuantity">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="월말재고" Key="MonthStockQuantity" Width="80px" Format="###,###,###.#" BaseColumnName="MonthStockQuantity">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총 금액" Key="TotalCost" Width="87px" Format="###,###,###" BaseColumnName="TotalCost">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
															<HeaderStyle Cursor="Hand" VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="매입년" Key="Year" Width="40px" BaseColumnName="Year"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="매입월" Key="Month" Width="40px" BaseColumnName="Month"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목분류1" Key="ItemClassification1" BaseColumnName="ItemClassification1">
															<CellStyle HorizontalAlign="Left">
																<Padding Left="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목분류2" Key="ItemClassification2" BaseColumnName="ItemClassification2">
															<CellStyle HorizontalAlign="Left">
																<Padding Left="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목분류3" Key="ItemClassification3" BaseColumnName="ItemClassification3">
															<CellStyle HorizontalAlign="Left">
																<Padding Left="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목분류4" Key="" BaseColumnName="ItemClassification4" NullText="ItemClassification4">
															<CellStyle HorizontalAlign="Left">
																<Padding Left="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
								<tr>
									<td colspan="6">
										<TABLE cellSpacing="0" cellPadding="0">
											<TR>
												<TD align="right" height="8" width="60"><FONT face="굴림"></FONT></TD>
												<TD height="8" width="100"></TD>
												<TD align="right" height="8" width="60"></TD>
												<TD height="8" width="100"></TD>
												<TD align="right" height="8" width="60"></TD>
												<TD height="8" width="100"></TD>
												<TD align="right" height="8" width="60"></TD>
												<TD align="left" height="8" width="100"></TD>
												<TD align="right" height="8" width="160">
													<INPUT id="hdLotNum" style="WIDTH: 67px; HEIGHT: 21px" type="hidden" size="5" runat="server"
														NAME="hdLotNum"><INPUT id="IndexNum" style="WIDTH: 15px; HEIGHT: 21px" type="hidden" size="1" runat="server"
														NAME="IndexNum"><INPUT id="HistoryIndex" style="WIDTH: 15px; HEIGHT: 21px" type="hidden" size="1" runat="server"
														NAME="HistoryIndex"><INPUT id="UnitCost" style="WIDTH: 15px; HEIGHT: 21px" type="hidden" size="1" name="Hidden1"
														runat="server"></TD>
											</TR>
											<TR>
												<TD colspan="4">
													<uc1:itemsearchcontrol style="Z-INDEX: 0" id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol>
												</TD>
												<TD title="품목명:" align="right" width="60">입고량&nbsp;</TD>
												<TD width="100"><asp:textbox id="txtDeliveryQuantity" runat="server" style="TEXT-ALIGN: right" Width="100px"
														BorderStyle="Solid" BackColor="#EEEEE9" Height="20px" BorderColor="DimGray" BorderWidth="1px">0</asp:textbox></TD>
												<TD align="right" width="60">단가 &nbsp;</TD>
												<TD align="left" width="100">
													<asp:textbox id="txtCost" style="TEXT-ALIGN: right" runat="server" BorderColor="DimGray" Height="20px"
														BackColor="#EEEEE9" BorderWidth="1px" BorderStyle="Solid" Width="100px">0</asp:textbox></TD>
												<TD align="right" width="160">년도
													<asp:dropdownlist id="Dropdownlist1" runat="server" Width="55px" BackColor="#EEEEE9">
														<asp:ListItem Value="2008">2008</asp:ListItem>
														<asp:ListItem Value="2009">2009</asp:ListItem>
														<asp:ListItem Value="2010">2010</asp:ListItem>
														<asp:ListItem Value="2011">2011</asp:ListItem>
														<asp:ListItem Value="2012">2012</asp:ListItem>
														<asp:ListItem Value="2013">2013</asp:ListItem>
														<asp:ListItem Value="2014">2014</asp:ListItem>
														<asp:ListItem Value="2015">2015</asp:ListItem>
														<asp:ListItem Value="2016">2016</asp:ListItem>
														<asp:ListItem Value="2017">2017</asp:ListItem>
														<asp:ListItem Value="2018">2018</asp:ListItem>
													</asp:dropdownlist>월
													<asp:dropdownlist id="Dropdownlist2" runat="server" Width="40px" BackColor="#EEEEE9">
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
											</TR>
											<TR>
												<TD title="발주수량:" style="HEIGHT: 24px" align="right" width="60">발주량&nbsp;</TD>
												<TD style="HEIGHT: 24px" width="100"><asp:textbox id="txtOrderQuantity" style="TEXT-ALIGN: right" runat="server" Width="100px" Enabled="False"
														BorderStyle="Solid" BackColor="#EEEEE9" Height="20px" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
												<TD style="HEIGHT: 24px" align="right" width="60">납입량&nbsp;</TD>
												<TD style="HEIGHT: 24px" width="100"><asp:textbox id="txtCashQuantity" style="TEXT-ALIGN: right" runat="server" Width="100px" Enabled="False"
														BorderStyle="Solid" BackColor="#EEEEE9" Height="20px" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
												<TD style="HEIGHT: 24px" align="right" width="60">잔량&nbsp;</TD>
												<TD style="HEIGHT: 24px" width="100"><asp:textbox id="txtRemainQuantity" style="TEXT-ALIGN: right" runat="server" Width="100px" Enabled="False"
														BorderStyle="Solid" BackColor="#EEEEE9" Height="20px" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
												<TD style="HEIGHT: 24px" align="right" width="60">
													입고일&nbsp;</TD>
												<TD style="HEIGHT: 24px" align="left" width="100"><igsch:webdatechooser id="wdcDeliveryDate" runat="server" NullDateLabel=" " Width="100px" BackColor="#EEEEE9"
														Height="20px">
														<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
															ShowTitle="False" ShowFooter="False">
															<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
															<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
															<DropDownStyle BackColor="White"></DropDownStyle>
															<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
														</CalendarLayout>
														<DropDownStyle BorderStyle="Inset"></DropDownStyle>
														<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
														<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
													</igsch:webdatechooser></TD>
												<TD align="right" width="160"><asp:button id="btnRegistration" runat="server" Width="65px" Height="20px" Text="등   록" Font-Size="9pt"></asp:button>&nbsp;</TD>
											</TR>
										</TABLE>
									</td>
								</tr>
								<TR>
									<TD style="WIDTH: 265px" align="left" colSpan="2" height="30"><FONT face="굴림">&nbsp;</FONT>
										<asp:button id="Button1" runat="server" Font-Size="10pt" Text="Excel" Width="60px" Height="20px"></asp:button></TD>
									<TD align="left" colSpan="4" height="30">&nbsp;&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
						<igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter></TD>
				</TR>
			</TABLE>
			&nbsp;
		</form>
	</body>
</HTML>
