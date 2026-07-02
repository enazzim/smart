<%@ Page language="c#" Codebehind="PublicUseCode.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.SystemInfoManagement.PublicUseCode" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PublicUseCode</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--
		
		function OK(text)
		{
			if(confirm(text + "하시겠습니까?"))
				return true;
			else
				return false;
		}		
		

		function wg_Small_AfterSelectChangeHandler(gridName, id){
			var row = igtbl_getRowById(id);
			document.PublicUseCode.tb_SmallClassificationCode.value = row.getCellFromKey("SmallClassificationCode").getValue();
			document.PublicUseCode.tb_SmallClassificationName.value = row.getCellFromKey("SmallClassificationName").getValue();
			document.PublicUseCode.lb_Index.value = row.getCellFromKey("PublicUseCodeIndex").getValue();
			
			document.PublicUseCode.bt_Registration.disabled = true;
			document.PublicUseCode.bt_Update.disabled = false;			
		}
		function wg_Large_AfterSelectChangeHandler(gridName, id){
			var row = igtbl_getRowById(id);
			document.PublicUseCode.tb_LargeClassificationCode.value = row.getCellFromKey("LargeClassificationCode").getValue();
			document.PublicUseCode.tb_LargeClassificationName.value = row.getCellFromKey("LargeClassificationName").getValue();
			document.PublicUseCode.tb_SmallClassificationCode.value = "";
			document.PublicUseCode.tb_SmallClassificationName.value = "";
		}
		function Ultrawebgrid2_AfterSelectChangeHandler(gridName, id){
			var row = igtbl_getRowById(id);
			PublicUseCode.lb_ReferenceTable.value = row.getCellFromKey("CommunityDataTableID").getValue();
			PublicUseCode.lb_ReferenceIndex.value = row.getCellFromKey("CDTIndex").getValue();
		}
--></SCRIPT>
	</HEAD>
	<body bgColor="#f7f6f6" topMargin="10" MS_POSITIONING="GridLayout">
		<form id="PublicUseCode" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" cellSpacing="0"
				cellPadding="0" width="800" border="0" height="550">
				<TR>
					<TD vAlign="top">
						<TABLE id="Table1" style="HEIGHT: 20px" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD vAlign="bottom" align="right" width="800">
									<asp:linkbutton id="LinkButton1" runat="server" Font-Size="10pt">공용코드보기</asp:linkbutton></TD>
							</TR>
						</TABLE>
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 500px"
							align="left"><LEGEND>[ 입 력 ]</LEGEND>
							<TABLE id="a" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD style="WIDTH: 79px" align="right" width="79"><FONT face="굴림">대분류코드&nbsp;</FONT>
									</TD>
									<TD align="left" width="130">
										<asp:textbox id="tb_LargeClassificationCode" runat="server" BorderWidth="1px" BorderColor="DimGray"
											BackColor="#EEEEE9" Width="100px" BorderStyle="Solid" ReadOnly="True" Font-Size="9pt"></asp:textbox></TD>
									<TD align="left" width="70"></TD>
									<TD width="130"></TD>
									<TD align="right" width="70"></TD>
									<TD style="WIDTH: 79px" align="right" width="79"><FONT face="굴림">소분류코드&nbsp;</FONT></TD>
									<TD width="130">
										<asp:textbox id="tb_SmallClassificationCode" runat="server" BorderWidth="1px" BorderColor="DimGray"
											BackColor="#EEEEE9" Width="100px" BorderStyle="Solid" ReadOnly="True" Font-Size="9pt" Height="20px"></asp:textbox></TD>
									<TD width="130"><INPUT id="lb_Index" type="hidden" value="0" name="lb_Index" runat="server"></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 79px" align="right" width="79"><FONT face="굴림">대분류명&nbsp;</FONT></TD>
									<TD align="left" width="130">
										<asp:textbox id="tb_LargeClassificationName" runat="server" BorderWidth="1px" BorderColor="DimGray"
											BackColor="#EEEEE9" Width="100px" BorderStyle="Solid" ReadOnly="True" Font-Size="9pt"></asp:textbox></TD>
									<TD align="left" width="70"></TD>
									<TD width="130"><FONT face="굴림"></FONT></TD>
									<TD align="right" width="70"></TD>
									<TD style="WIDTH: 79px" align="right" width="79"><FONT face="굴림">소분류명&nbsp;</FONT></TD>
									<TD width="130">
										<asp:textbox id="tb_SmallClassificationName" runat="server" BorderWidth="1px" BorderColor="DimGray"
											BackColor="#EEEEE9" Width="100px" BorderStyle="Solid" Font-Size="9pt" Height="20px"></asp:textbox></TD>
									<TD align="right" width="130">
										<asp:button id="bt_Update" runat="server" Height="20px" Text="수정" Enabled="False"></asp:button><FONT face="굴림">&nbsp;&nbsp;</FONT>
										<asp:button id="bt_Registration" runat="server" Height="20px" Text="등록"></asp:button><FONT face="굴림">&nbsp;</FONT></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 79px" align="right" width="79" height="5"></TD>
									<TD align="left" width="130" height="5"><FONT face="굴림"></FONT></TD>
									<TD align="left" width="70" height="5"></TD>
									<TD width="130" height="5"></TD>
									<TD align="right" width="70" height="5"></TD>
									<TD style="WIDTH: 79px" align="right" width="79" height="5"></TD>
									<TD width="130" height="5"></TD>
									<TD align="right" width="130" height="5"></TD>
								</TR>
								<TR>
									<TD align="center" width="400" colSpan="4">
										<igtbl:ultrawebgrid id="wg_Large" runat="server" Width="327px" Height="200px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="wgxLarge" TableLayout="Fixed" CellClickActionDefault="RowSelect">
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
												<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
												<FrameStyle Width="327px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="200px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents AfterSelectChangeHandler="wg_Large_AfterSelectChangeHandler"></ClientSideEvents>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedRowStyleDefault ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="대분류코드" Key="LargeClassificationCode" Width="300px" Hidden="True" BaseColumnName="LargeClassificationCode">
															<CellStyle Width="325px" VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="대분류명" Key="LargeClassificationName" Width="325px" BaseColumnName="LargeClassificationName">
															<CellStyle Width="325px" VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="소분류코드" Key="SmallClassificationCode" Hidden="True" BaseColumnName="SmallClassificationCode">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="소분류명" Key="SmallClassificationName" Hidden="True" BaseColumnName="SmallClassificationName">
															<CellStyle Cursor="Auto" VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
									<TD width="100"><FONT face="굴림"></FONT></TD>
									<TD style="WIDTH: 327px" align="left" width="327" colSpan="3">
										<igtbl:ultrawebgrid id="wg_Small" runat="server" Width="325px" Height="200px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="wgxSmall" TableLayout="Fixed" CellClickActionDefault="RowSelect">
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
												<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
												<FrameStyle Width="325px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="200px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents AfterSelectChangeHandler="wg_Small_AfterSelectChangeHandler"></ClientSideEvents>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedRowStyleDefault ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="소분류코드" Key="SmallClassificationCode" Width="160px" BaseColumnName="SmallClassificationCode">
															<CellStyle Width="300px" VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="소분류명" Key="SmallClassificationName" Width="160px" BaseColumnName="SmallClassificationName">
															<CellStyle Width="300px" VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="번호" Key="PublicUseCodeIndex" Hidden="True" BaseColumnName="PublicUseCodeIndex"></igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 79px" align="right" width="79">
										<asp:button id="Button1" runat="server" Height="20px" Text="참조보기"></asp:button></TD>
									<TD align="right" width="130"><INPUT id="lb_ReferenceIndex" style="WIDTH: 30px" type="hidden" name="lb_ReferenceIndex"
											runat="server"></FONT>
									</TD>
									<TD align="left" width="70"><FONT face="굴림"><INPUT id="lb_ReferenceTable" style="WIDTH: 30px" type="hidden" name="lb_ReferenceTable"
												runat="server"></FONT></TD>
									<TD width="130"></TD>
									<TD align="right" width="70"></TD>
									<TD style="WIDTH: 79px" width="79"></TD>
									<TD width="70"></TD>
									<TD width="130"></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 79px" align="right" width="79" height="5"><FONT face="굴림"></FONT></TD>
									<TD align="right" width="130" height="5"></TD>
									<TD align="left" width="70" height="5"></TD>
									<TD width="130" height="5"></TD>
									<TD align="right" width="70" height="5"></TD>
									<TD style="WIDTH: 79px" width="79" height="5"></TD>
									<TD width="70" height="5"></TD>
									<TD width="130" height="5"></TD>
								</TR>
								<TR>
									<TD colSpan="8" height="10">
										<igtbl:ultrawebgrid id="Ultrawebgrid2" runat="server" Width="800px" Height="235px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Extended" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="Ultrawebgrid2" TableLayout="Fixed" CellClickActionDefault="RowSelect"
												NoDataMessage="">
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
												<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
												<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="235px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents AfterSelectChangeHandler="Ultrawebgrid2_AfterSelectChangeHandler"></ClientSideEvents>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BorderColor="DarkGray" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="공용코드정보테이블" Key="StandardinfoTableID" Width="150px" BaseColumnName="StandardinfoTableID">
															<HeaderStyle Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목정보인덱스" Key="SITIndex" Width="50px" Hidden="True" BaseColumnName="SITIndex"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="자료실테이블" Key="CommunityDataTableID" Width="150px" BaseColumnName="CommunityDataTableID"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="자료실인덱스" Key="CDTIndex" Width="50px" Hidden="True" BaseColumnName="CDTIndex"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="제목" Key="Title" Width="500px" HeaderClickAction="SortSingle" BaseColumnName="Title"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="번호" Key="CRelationShipIndex" Width="50px" Hidden="True" BaseColumnName="CRelationShipIndex"></igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
