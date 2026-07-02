<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearch" Src="ItemSearch/ItemSearch.ascx" %>
<%@ Page language="c#" Codebehind="UpdateRealItemOrganizationInfoHistory.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.UpdateRealItemOrganizationInfoHistory" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>UpdateRealItemOrganizationInfoHistory</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript">
		<!--
		function ResettxtBox()
		{
			ResetTextBox();
			ResetBox();
		}
		//-->
		</SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="PADDING-LEFT: 10px; LEFT: 0px; PADDING-TOP: 10px; POSITION: absolute; TOP: 0px"
				height="540" cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 800px; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top"><asp:label id="searchTitle" Runat="server" text="[검색조건]" Font-Size="10pt">[검색조건]</asp:label></LEGEND>
							<TABLE id="Table2" style="HEIGHT: 2px" cellSpacing="0" cellPadding="0" width="800">
								<TBODY>
									<TR>
										<TD align="left" width="600" colSpan="4" height="30"><uc1:itemsearch id="ItemSearch1" runat="server"></uc1:itemsearch></TD>
										<TD align="right" width="200"></TD>
									</TR>
									<TR>
										<TD align="left" width="600" colSpan="4" height="30"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></TD>
										<TD align="right" width="200"><INPUT style="WIDTH: 60px; HEIGHT: 20px" onclick="ResettxtBox()" type="button" value="초기화">&nbsp;
											<asp:button id="bt_Search" runat="server" Width="60px" Height="20px" Text="검색"></asp:button>&nbsp;</TD>
					</TD>
				</TR>
			</TABLE>
			</FIELDSET> </TD></TR>
			<TR>
				<TD vAlign="top" align="center" width="800">
					<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top"><asp:label id="Label2" Runat="server" text="[검색조건]" Font-Size="10pt">[검색결과]</asp:label></LEGEND>
						<TABLE id="Table3" style="HEIGHT: 440px" cellSpacing="1" cellPadding="1" width="800" border="0">
							<TR>
								<TD><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="447px">
										<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
											RowHeightDefault="20px" RowSizingDefault="Free" Version="3.00" SelectTypeRowDefault="Single"
											AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate"
											AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed"
											CellClickActionDefault="RowSelect">
											<AddNewBox>
												<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
												</Style>
											</AddNewBox>
											<Pager PageSize="19" StyleMode="ComboBox" AllowPaging="True">
												<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
												</Style>
											</Pager>
											<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray" Height="25px">
												<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
											</HeaderStyleDefault>
											<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="Verdana" BorderColor="DimGray"
												BorderStyle="Solid" BackColor="Silver" Height="447px"></FrameStyle>
											<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
												<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
											</FooterStyleDefault>
											<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
											<SelectedRowStyleDefault ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
											<RowAlternateStyleDefault BorderColor="DarkGray" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
											<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
												<Padding Left="3px"></Padding>
												<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
											</RowStyleDefault>
										</DisplayLayout>
										<Bands>
											<igtbl:UltraGridBand>
												<Columns>
													<igtbl:UltraGridColumn HeaderText="자품목번호" Key="ChildItemNum" BaseColumnName="ChildItemNum"></igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="자품목명" Key="ChildItemName" BaseColumnName="ChildItemName"></igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="모품목번호" Key="ParentItemNum" BaseColumnName="ParentItemNum">
														<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="도면번호" Key="ParentItemDrawNum" Hidden="True" BaseColumnName="ParentItemDrawNum"></igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="모품목명" Key="ParentItemName" BaseColumnName="ParentItemName"></igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="변경사유" Key="UpdateReason" BaseColumnName="UpdateReason"></igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="자품목번호" Key="ChildItemNum" Hidden="True" BaseColumnName="ChildItemNum">
														<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="자품도면번호" Key="ItemDrawNum" Hidden="True" BaseColumnName="ItemDrawNum">
														<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="자품목명" Key="ItemName" Hidden="True" BaseColumnName="ItemName">
														<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="분자량" Key="NeedQuantityNumerator" Width="54px" BaseColumnName="NeedQuantityNumerator">
														<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="분모량" Key="NeedQuantityDenominator" Width="54px" BaseColumnName="NeedQuantityDenominator">
														<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="적용시작일" Key="BeginDate" Width="95px" Format="yyyy-MM-dd" BaseColumnName="BeginDate">
														<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" BaseColumnName="UpdatingPerson">
														<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														<HeaderStyle VerticalAlign="Middle"></HeaderStyle>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
														<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Width="95px" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
														<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="번호" Key="ItemOrganizationInfoIndex" Hidden="True" BaseColumnName="ItemOrganizationInfoIndex">
														<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
													</igtbl:UltraGridColumn>
												</Columns>
											</igtbl:UltraGridBand>
										</Bands>
									</igtbl:ultrawebgrid></TD>
							</TR>
							<tr>
								<td align="right"></td>
							</tr>
						</TABLE>
					</FIELDSET>
				</TD>
			</TR>
			</TBODY></TABLE></form>
	</body>
</HTML>
