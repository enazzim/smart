<%@ Page language="c#" Codebehind="WebForm1.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ManagementInfomation.WebForm1" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" height="550"
				cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD height="60">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 100%; 90BORDER-RIGHT: black 1px solid"
							align="absBottom"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색조건 ]
							</LEGEND>
							<TABLE id="Table1" height="50" cellSpacing="0" cellPadding="0" width="100%" align="center"
								border="0">
								<TR>
									<TD style="FONT-SIZE: 9pt" align="right" colSpan="12" height="28">
										<asp:DropDownList id="DropDownList1" runat="server" AutoPostBack="True">
											<asp:ListItem Value="원자재입고대장">원자재입고대장</asp:ListItem>
											<asp:ListItem Value="제품출고대장">제품출고대장</asp:ListItem>
											<asp:ListItem Value="원자재제품수불">원자재제품수불</asp:ListItem>
										</asp:DropDownList><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
											type="button" value="초기화" name="btnReset"><FONT face="굴림">&nbsp;</FONT>
										<asp:button id="btnSearch" runat="server" Width="60px" Height="20px" Text="검  색"></asp:button><FONT face="굴림">&nbsp;</FONT></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD>
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 100%; 90BORDER-RIGHT: black 1px solid"
							align="middle"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색결과 ]
							</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD width="800"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="440px">
											<DisplayLayout RowHeightDefault="25px" Version="3.00" BorderCollapseDefault="Separate" RowSelectorsDefault="No"
												Name="UltraWebGrid2">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</AddNewBox>
												<Pager PageSize="15" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</Pager>
												<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray" Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<FrameStyle Width="800px" BorderWidth="1px" Font-Size="X-Small" Font-Names="Verdana" BorderStyle="Solid"
													Height="440px"></FrameStyle>
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
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="" BaseColumnName="">
															<HeaderStyle Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="입고중량" Key="" Format="###,###,###" BaseColumnName="">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단가" Key="" Format="###,###,###" BaseColumnName="">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="일자" Key="" Format="yyyy-MM-dd" BaseColumnName=""></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="업체명" Key="" BaseColumnName=""></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="입고중량" Key="" Format="###,###,###" BaseColumnName="">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단가" Key="" Format="###,###,###" BaseColumnName="">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="일자" Key="" Format="yyyy-MM-dd" BaseColumnName=""></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="업체명" Key="" BaseColumnName=""></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="입고중량" Key="" Format="###,###,###" BaseColumnName="">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단가" Key="" Format="###,###,###" BaseColumnName="">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="일자" Key="" Format="yyyy-MM-dd" BaseColumnName=""></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="업체명" Key="" BaseColumnName=""></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="입고중량" Key="" Format="###,###,###" BaseColumnName="">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단가" Key="" Format="###,###,###" BaseColumnName="">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="일자" Key="" Format="yyyy-MM-dd" BaseColumnName=""></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="업체명" Key="" BaseColumnName=""></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="입고중량" Key="" Format="###,###,###" BaseColumnName="">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단가" Key="" Format="###,###,###" BaseColumnName="">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="일자" Key="" Format="yyyy-MM-dd" BaseColumnName=""></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="업체명" Key="" BaseColumnName=""></igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid>
										<igtbl:UltraWebGrid id="UltraWebGrid2" runat="server" Width="800px" Height="440px" Visible="False">
											<DisplayLayout RowHeightDefault="25px" Version="3.00" BorderCollapseDefault="Separate" RowSelectorsDefault="No"
												Name="UltraWebGrid2">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</AddNewBox>
												<Pager PageSize="15" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</Pager>
												<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray" Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<FrameStyle Width="800px" BorderWidth="1px" Font-Size="X-Small" Font-Names="Verdana" BorderStyle="Solid"
													Height="440px"></FrameStyle>
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
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="" BaseColumnName="">
															<HeaderStyle Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="출고수량" Key="" Format="###,###,###" BaseColumnName="">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단가" Key="" Format="###,###,###" BaseColumnName="">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="일자" Key="" Format="yyyy-MM-dd" BaseColumnName=""></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="업체명" Key="" BaseColumnName=""></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="출고수량" Key="" Format="###,###,###" BaseColumnName="">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단가" Key="" Format="###,###,###" BaseColumnName="">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="일자" Key="" Format="yyyy-MM-dd" BaseColumnName=""></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="업체명" Key="" BaseColumnName=""></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="출고수량" Key="" Format="###,###,###" BaseColumnName="">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단가" Key="" Format="###,###,###" BaseColumnName="">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="일자" Key="" Format="yyyy-MM-dd" BaseColumnName=""></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="업체명" Key="" BaseColumnName=""></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="출고수량" Key="" Format="###,###,###" BaseColumnName="">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단가" Key="" Format="###,###,###" BaseColumnName="">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="일자" Key="" Format="yyyy-MM-dd" BaseColumnName=""></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="업체명" Key="" BaseColumnName=""></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="출고수량" Key="" Format="###,###,###" BaseColumnName="">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단가" Key="" Format="###,###,###" BaseColumnName="">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="일자" Key="" Format="yyyy-MM-dd" BaseColumnName=""></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="업체명" Key="" BaseColumnName=""></igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:UltraWebGrid>
										<table border="0" cellpadding="0" cellspacing="0">
											<tr>
												<td style="BORDER-RIGHT: #000000 thick solid" width="400">
													<igtbl:UltraWebGrid id="UltraWebGrid3" runat="server" Width="395px" Height="440px" Visible="False">
														<DisplayLayout RowHeightDefault="25px" Version="3.00" BorderCollapseDefault="Separate" RowSelectorsDefault="No"
															Name="UltraWebGrid3">
															<AddNewBox>
																<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

																</Style>
															</AddNewBox>
															<Pager PageSize="15" StyleMode="ComboBox" AllowPaging="True">
																<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

																</Style>
															</Pager>
															<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray" Height="25px">
																<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
															</HeaderStyleDefault>
															<FrameStyle Width="395px" BorderWidth="1px" Font-Size="X-Small" Font-Names="Verdana" BorderStyle="Solid"
																Height="440px"></FrameStyle>
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
															<igtbl:UltraGridBand>
																<Columns>
																	<igtbl:UltraGridColumn HeaderText="품목번호" Key="" BaseColumnName="">
																		<HeaderStyle Height="25px"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="이월재고" Key="" Format="###,###,###" BaseColumnName="">
																		<CellStyle HorizontalAlign="Right"></CellStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="당월입고" Key="" Format="###,###,###" BaseColumnName="">
																		<CellStyle HorizontalAlign="Right"></CellStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="당월투입" Key="" Format="###,###,###" BaseColumnName=""></igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="사용품명" Key="" BaseColumnName=""></igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="절단수량" Key="" Format="###,###,###" BaseColumnName="">
																		<CellStyle HorizontalAlign="Right"></CellStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="절단길이" Key="" Format="##,###,####" BaseColumnName="">
																		<CellStyle HorizontalAlign="Right"></CellStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="절단중량" Key="" Format="###,###,###" BaseColumnName=""></igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="제품중량" Key="" Format="###,###,###" BaseColumnName=""></igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="바리중량" Key="" Format="###,###,###" BaseColumnName="">
																		<CellStyle HorizontalAlign="Right"></CellStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="기말재고" Key="" Format="###,###,###" BaseColumnName="">
																		<CellStyle HorizontalAlign="Right"></CellStyle>
																	</igtbl:UltraGridColumn>
																</Columns>
															</igtbl:UltraGridBand>
														</Bands>
													</igtbl:UltraWebGrid>
												</td>
												<td width="400">
													<igtbl:UltraWebGrid id="UltraWebGrid4" runat="server" Width="395px" Height="440px" Visible="False">
														<DisplayLayout RowHeightDefault="25px" Version="3.00" BorderCollapseDefault="Separate" RowSelectorsDefault="No"
															Name="UltraWebGrid4">
															<AddNewBox>
																<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

																</Style>
															</AddNewBox>
															<Pager PageSize="15" StyleMode="ComboBox" AllowPaging="True">
																<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

																</Style>
															</Pager>
															<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray" Height="25px">
																<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
															</HeaderStyleDefault>
															<FrameStyle Width="395px" BorderWidth="1px" Font-Size="X-Small" Font-Names="Verdana" BorderStyle="Solid"
																Height="440px"></FrameStyle>
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
															<igtbl:UltraGridBand>
																<Columns>
																	<igtbl:UltraGridColumn HeaderText="품목번호" Key="" BaseColumnName="">
																		<HeaderStyle Height="25px"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="이월재고" Key="" Format="###,###,###" BaseColumnName="">
																		<CellStyle HorizontalAlign="Right"></CellStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="절단투입" Key="" Format="###,###,###" BaseColumnName="">
																		<CellStyle HorizontalAlign="Right"></CellStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="생산수량" Key="" Format="###,###,###" BaseColumnName=""></igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="불량수량" Key="" Format="###,###,###" BaseColumnName=""></igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="제품중량" Key="" Format="###,###,###" BaseColumnName="">
																		<CellStyle HorizontalAlign="Right"></CellStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="생산중량" Key="" Format="###,###,###" BaseColumnName="">
																		<CellStyle HorizontalAlign="Right"></CellStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="불량중량" Key="" Format="###,###,###" BaseColumnName=""></igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="바리중량" Key="" Format="###,###,###" BaseColumnName=""></igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="Loss합계" Key="" Format="###,###,###" BaseColumnName="">
																		<CellStyle HorizontalAlign="Right"></CellStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="제품출고" Key="" Format="###,###,###" BaseColumnName="">
																		<CellStyle HorizontalAlign="Right"></CellStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="기말재고" Key="" Format="###,###,###" BaseColumnName=""></igtbl:UltraGridColumn>
																</Columns>
															</igtbl:UltraGridBand>
														</Bands>
													</igtbl:UltraWebGrid>
												</td>
											</tr>
										</table>
									</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 9pt" vAlign="middle" align="left"><FONT face="굴림"></FONT></TD>
								</TR>
								<TR height="40">
									<TD>&nbsp;
										<asp:button id="Button2" runat="server" Width="60px" Height="20px" Text="Excel"></asp:button></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
