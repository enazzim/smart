<%@ Page language="c#" Codebehind="WCPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.PopupWindows.WCPC" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WCPC</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<FIELDSET style="Z-INDEX: 101; LEFT: 8px; WIDTH: 757px; POSITION: absolute; TOP: 8px; HEIGHT: 368px"
				align="left">
				<DIV>
					<TABLE id="table1" style="WIDTH: 744px; HEIGHT: 342px">
						<TR>
							<TD style="HEIGHT: 285px" vAlign="top" width="700">
								<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="352px" Width="800px">
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
										<FrameStyle Width="800px" Cursor="Default" BorderWidth="1px" Font-Size="10pt" Font-Names="굴림"
											BorderStyle="Solid" BackColor="Silver" Height="352px"></FrameStyle>
										<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
											<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
										</FooterStyleDefault>
										<ClientSideEvents ColumnHeaderClickHandler="UltraWebGrid1_ColumnHeaderClickHandler"></ClientSideEvents>
										<GroupByBox>
											<Style BackColor="#B5CFF7">
											</Style>
											<BandLabelStyle ForeColor="White" BackColor="#6372D4"></BandLabelStyle>
										</GroupByBox>
										<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
										<SelectedGroupByRowStyleDefault BorderColor="Yellow" BackColor="Yellow"></SelectedGroupByRowStyleDefault>
										<SelectedRowStyleDefault Cursor="Hand" Font-Bold="True" BorderColor="White" ForeColor="Linen" BackColor="Navy"></SelectedRowStyleDefault>
										<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
										<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" ForeColor="Black"
											BackColor="#EDEFF6">
											<Padding Left="3px"></Padding>
											<BorderDetails ColorTop="Gray" WidthLeft="0px" WidthTop="0px" ColorLeft="Gray"></BorderDetails>
										</RowStyleDefault>
									</DisplayLayout>
									<Bands>
										<igtbl:UltraGridBand AllowUpdate="No">
											<Columns>
												<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
													<SelectedCellStyle HorizontalAlign="Center"></SelectedCellStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" HeaderClickAction="SortMulti" BaseColumnName="ItemDrawNum">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="공정순서번호" Key="ProcessSequenceNum" HeaderClickAction="SortMulti" BaseColumnName="ProcessSequenceNum">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="공정코드" Key="ProcessCode" Hidden="True" HeaderClickAction="SortMulti"
													BaseColumnName="ProcessCode">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="공정명" Key="ProcessName" HeaderClickAction="SortMulti" BaseColumnName="ProcessName">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="제품번호" Key="ProductItemNum" HeaderClickAction="SortMulti" BaseColumnName="ProductItemNum">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="제품도면번호" Key="ProductDrawNum" HeaderClickAction="SortMulti" BaseColumnName="ProductDrawNum">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="제품명" Key="ProductName" HeaderClickAction="SortMulti" BaseColumnName="ProductName">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="모품목번호" Key="ParentItemNum" HeaderClickAction="SortMulti" BaseColumnName="ParentItemNum">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="모품목도면번호" Key="ParentDrawNum" HeaderClickAction="SortMulti" BaseColumnName="ParentDrawNum">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="모품목명" Key="ParentName" HeaderClickAction="SortMulti" BaseColumnName="ParentName">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="작업장명" Key="WCName" HeaderClickAction="SortMulti" BaseColumnName="WCName">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="작업구분" Key="WorkDistinction" HeaderClickAction="SortMulti" BaseColumnName="WorkDistinction">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="작업계획수량" Key="WorkPlanQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
													BaseColumnName="WorkPlanQuantity">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="작업완료수량" Key="WorkCompletionQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
													BaseColumnName="WorkCompletionQuantity">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="외주리드타임" Key="OrderLeadTime" Format="#########" HeaderClickAction="SortMulti"
													BaseColumnName="OrderLeadTime">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="작업시작일" Key="WorkDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
													BaseColumnName="WorkDate">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" HeaderClickAction="SortMulti" BaseColumnName="ProgressCondition">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="생산계획번호" Key="ProductionPlanHistoryIndex" Format="#########" HeaderClickAction="SortSingle"
													BaseColumnName="ProductionPlanHistoryIndex">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="WC작업계획번호" Key="WCDailyWorkPlanHistoryIndex" Hidden="True" Format="#########"
													HeaderClickAction="SortMulti" BaseColumnName="WCDailyWorkPlanHistoryIndex">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPerson">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" BaseColumnName="RegistrationPersonID">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
													BaseColumnName="RegistrationDate">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" HeaderClickAction="SortMulti" BaseColumnName="UpdatingPerson">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" BaseColumnName="UpdatingPersonID">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
												<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="" HeaderClickAction="SortMulti" BaseColumnName="UpdatingDate">
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												</igtbl:UltraGridColumn>
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
					</TABLE>
					<TABLE id="Table2" style="WIDTH: 800px; HEIGHT: 32px" cellSpacing="0" cellPadding="0">
						<FONT face="굴림"></FONT>
					</TABLE>
				</DIV>
			</FIELDSET>
		</form>
	</body>
</HTML>
