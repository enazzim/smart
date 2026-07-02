<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="ItemOrganization.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.Popup.ItemOrganization" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ItemOrganization</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<STYLE type="text/css">BODY { SCROLLBAR-ARROW-COLOR: #400040; SCROLLBAR-FACE-COLOR: #ffffff; SCROLLBAR-DARKSHADOW-COLOR: #808080; SCROLLBAR-HIGHLIGHT-COLOR: #ffffff; SCROLLBAR-SHADOW-COLOR: #ffffff; SCROLLBAR-TRACK-COLOR: #ffffff; SCROLLBAR-3DLIGHT-COLOR: #808080 }
		</STYLE>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="buttonface">
		<form id="Form1" method="post" runat="server">
			<FIELDSET style="Z-INDEX: 101; BORDER-BOTTOM: dimgray 2px solid; POSITION: absolute; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; HEIGHT: 450px; BORDER-TOP: dimgray 2px solid; TOP: 8px; BORDER-RIGHT: dimgray 2px solid; LEFT: 8px"
				align="left"><LEGEND style="FONT-SIZE: 9pt">[ 품목구성 ]</LEGEND>
				<asp:Button id="bt_Excel" runat="server" Text="Excel" Width="60px" Height="20px"></asp:Button>
				<table WIDTH="800" HEIGHT="450">
					<tr>
						<td WIDTH="850" HEIGHT="10">
							<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="448px">
								<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
									RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
									HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
									RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect">
									<AddNewBox>
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

										</Style>
									</AddNewBox>
									<Pager PageSize="19" StyleMode="ComboBox" AllowPaging="True">
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

										</Style>
									</Pager>
									<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</HeaderStyleDefault>
									<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
										BackColor="Silver" Height="448px"></FrameStyle>
									<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</FooterStyleDefault>
									<ClientSideEvents AfterSelectChangeHandler="UltraWebGrid1_AfterSelectChangeHandler"></ClientSideEvents>
									<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
									<SelectedRowStyleDefault BackColor="#E0E5CD"></SelectedRowStyleDefault>
									<RowAlternateStyleDefault BorderColor="DarkGray" BackColor="#EBEFF6"></RowAlternateStyleDefault>
									<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="LightSteelBlue">
										<Padding Left="3px"></Padding>
										<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
									</RowStyleDefault>
								</DisplayLayout>
								<Bands>
									<igtbl:UltraGridBand>
										<Columns>
											<igtbl:UltraGridColumn HeaderText="모품목번호" Key="ParentItemNum" HeaderClickAction="SortMulti" BaseColumnName="ParentItemNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle Height="25px"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="자품목번호" Key="ChildItemNum" HeaderClickAction="SortMulti" BaseColumnName="ChildItemNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="자품도면번호" Key="ItemDrawNum" HeaderClickAction="SortMulti" BaseColumnName="ItemDrawNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="자품목명" Key="ItemName" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="자품목분류" Key="PropertyClassification" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="PropertyClassification">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="단위" Key="Unit" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="Unit">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="규격" Key="Standard" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="Standard">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="소요량분자" Key="NeedQuantityNumerator" HeaderClickAction="SortMulti" BaseColumnName="NeedQuantityNumerator">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="소요량분모" Key="NeedQuantityDenominator" HeaderClickAction="SortMulti" BaseColumnName="NeedQuantityDenominator">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="공정관리여부" Key="ProcessManagement" Type="DropDownList" HeaderClickAction="SortMulti"
												BaseColumnName="ProcessManagement">
												<ValueList>
													<ValueListItems>
														<igtbl:ValueListItem Key="1" DisplayText="예"></igtbl:ValueListItem>
														<igtbl:ValueListItem Key="0" DisplayText="아니오"></igtbl:ValueListItem>
													</ValueListItems>
												</ValueList>
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="하위구분" Key="SubDivision" Type="DropDownList" HeaderClickAction="SortMulti"
												BaseColumnName="SubDivision">
												<ValueList>
													<ValueListItems>
														<igtbl:ValueListItem Key="1" DisplayText="예"></igtbl:ValueListItem>
														<igtbl:ValueListItem Key="1" DisplayText="아니오"></igtbl:ValueListItem>
													</ValueListItems>
												</ValueList>
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="조달구분" Key="SupplyDivision" Type="DropDownList" HeaderClickAction="SortMulti"
												BaseColumnName="SupplyDivision">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="BOM단위" Key="BOMUnit" Type="DropDownList" HeaderClickAction="SortMulti"
												BaseColumnName="BOMUnit">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="적용시작일" Key="BeginDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="BeginDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="적용종료일" Key="EndDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="EndDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="레코드상태" Key="RecodingState" Hidden="True" BaseColumnName="RecodingState">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPerson">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="RegistrationDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" HeaderClickAction="SortMulti" BaseColumnName="UpdatingPerson">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
												BaseColumnName="UpdatingDate">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="번호" Key="ItemOrganizationInfoIndex" Hidden="True" BaseColumnName="ItemOrganizationInfoIndex">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
										</Columns>
									</igtbl:UltraGridBand>
								</Bands>
							</igtbl:ultrawebgrid>
						</td>
					</tr>
				</table>
			</FIELDSET>
			<igtbl:ultrawebgrid id="UltraWebGrid2" style="Z-INDEX: 102; POSITION: absolute; TOP: 528px; LEFT: 16px"
				runat="server" Height="448px" Width="800px" Visible="False">
				<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
					RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
					HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
					RowSelectorsDefault="No" Name="UltraWebGrid2" TableLayout="Fixed" CellClickActionDefault="RowSelect">
					<AddNewBox>
						<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

						</Style>
					</AddNewBox>
					<Pager PageSize="19" StyleMode="ComboBox" Alignment="Center" AllowPaging="True">
						<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

						</Style>
					</Pager>
					<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
						<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
					</HeaderStyleDefault>
					<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
						BackColor="Transparent" Height="448px"></FrameStyle>
					<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
						<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
					</FooterStyleDefault>
					<ClientSideEvents AfterSelectChangeHandler="UltraWebGrid1_AfterSelectChangeHandler"></ClientSideEvents>
					<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
					<SelectedRowStyleDefault Cursor="Hand" BackColor="Transparent"></SelectedRowStyleDefault>
					<RowAlternateStyleDefault Cursor="Hand" BorderColor="Gray"></RowAlternateStyleDefault>
					<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid">
						<Padding Left="3px"></Padding>
						<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
					</RowStyleDefault>
				</DisplayLayout>
				<Bands>
					<igtbl:UltraGridBand>
						<Columns>
							<igtbl:UltraGridColumn HeaderText="모품목번호" Key="ParentItemNum" HeaderClickAction="SortMulti" BaseColumnName="ParentItemNum">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
								<HeaderStyle Height="25px"></HeaderStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="자품목번호" Key="ChildItemNum" HeaderClickAction="SortMulti" BaseColumnName="ChildItemNum">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="규격" Key="Standard" BaseColumnName="Standard"></igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="소요량분자" Key="NeedQuantityNumerator" HeaderClickAction="SortMulti" BaseColumnName="NeedQuantityNumerator">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="소요량분모" Key="NeedQuantityDenominator" HeaderClickAction="SortMulti" BaseColumnName="NeedQuantityDenominator">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="공정관리" Key="ProcessManagement" Type="DropDownList" HeaderClickAction="SortMulti"
								BaseColumnName="ProcessManagement">
								<ValueList>
									<ValueListItems>
										<igtbl:ValueListItem Key="1" DisplayText="예"></igtbl:ValueListItem>
										<igtbl:ValueListItem Key="0" DisplayText="아니오"></igtbl:ValueListItem>
									</ValueListItems>
								</ValueList>
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="하위구분" Key="SubDivision" Type="DropDownList" HeaderClickAction="SortMulti"
								BaseColumnName="SubDivision">
								<ValueList>
									<ValueListItems>
										<igtbl:ValueListItem Key="1" DisplayText="예"></igtbl:ValueListItem>
										<igtbl:ValueListItem Key="1" DisplayText="아니오"></igtbl:ValueListItem>
									</ValueListItems>
								</ValueList>
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="조달구분" Key="SupplyDivision" Type="DropDownList" HeaderClickAction="SortMulti"
								BaseColumnName="SupplyDivision">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="BOM단위" Key="BOMUnit" Type="DropDownList" HeaderClickAction="SortMulti"
								BaseColumnName="BOMUnit">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="시작일" Key="BeginDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
								BaseColumnName="BeginDate">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="종료일" Key="EndDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
								BaseColumnName="EndDate">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
						</Columns>
					</igtbl:UltraGridBand>
				</Bands>
			</igtbl:ultrawebgrid>
			<igtblexp:UltraWebGridExcelExporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:UltraWebGridExcelExporter>
		</form>
	</body>
</HTML>
