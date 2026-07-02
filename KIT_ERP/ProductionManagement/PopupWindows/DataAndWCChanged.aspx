<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="DataAndWCChanged.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.PopupWindows.DataAndWCChanged" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../ItemSearchControl/ItemSearchControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>DataAndWCChanged</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT type="text/javascript"><!--

		var GridName;
		
		function UltraWebGrid1_ColumnHeaderClickHandler(gridName, columnId, button)
		{
			GridName = gridName;
    			if(document.Form1.chkAll.value == "true")
    			{
					AllCheck();
				}
				else
				{
					AllUncheck();
				}
		}
		
		function AllCheck() // 전부 체크하기
		{
			var grid = igtbl_getGridById(GridName);
			
			for(var i=0; i<grid.Rows.length; i++)
			{
				var row = grid.Rows.getRow(i);
				row.getCellFromKey("chk").setValue(true);
			}
			document.Form1.chkAll.value = "false";
		}


		function AllUncheck() // 전부체크 해제하기
		{
			var grid = igtbl_getGridById(GridName);
			
			for(var i=0; i<grid.Rows.length; i++)
			{
				var row = grid.Rows.getRow(i);
				row.getCellFromKey("chk").setValue(false);
			}
			document.Form1.chkAll.value = "true";
		}
		
		function Cancel()
		{
			alert("취소하였습니다");
			window.opener.SubmitLinkButton();self.close();	
		}
		
		
				
--></SCRIPT>
</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<FONT face="굴림">
				<TABLE id="Table1" style="Z-INDEX: 100; LEFT: 10px; WIDTH: 800px; POSITION: absolute; TOP: 10px"
					height="550" cellSpacing="0" cellPadding="0" width="800" border="0">
					<TR>
						<TD style="WIDTH: 18px; HEIGHT: 550px" width="18"></TD>
						<TD style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; PADDING-TOP: 10px; HEIGHT: 550px"
							vAlign="top" align="center" width="800">
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 0pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 40px"><LEGEND align="top"><asp:label id="searchTitle" Font-Size="10pt" Runat="server">[검색조건]</asp:label></LEGEND>
								<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" border="0">
									<TR>
										<TD style="FONT-SIZE: 9pt" vAlign="middle" align="left" width="600" colSpan="2" height="30">
											<uc1:ItemSearchControl id="ItemSearchControl1" runat="server"></uc1:ItemSearchControl></TD>
										<TD style="FONT-SIZE: 9pt" vAlign="middle" align="left" width="200" colSpan="5" height="30">&nbsp;
											<asp:button id="btnClear" runat="server" Font-Size="10pt" CommandName="Clear" Text="초기화" Width="65px"
												Height="20px"></asp:button>&nbsp;
											<asp:button id="btnSearch" runat="server" Font-Size="10pt" CommandName="Search" Text="검   색"
												Width="65px" Height="20px"></asp:button>&nbsp;
										</TD>
									</TR>
								</TABLE>
							</FIELDSET>
							<BR>
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 0pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 489px"><LEGEND align="top"><asp:label id="Label2" Font-Size="10pt" Runat="server">[검색결과]</asp:label></LEGEND>
								<TABLE id="Table3" height="432" cellSpacing="0" cellPadding="0" width="800" border="0">
									<TBODY>
										<TR>
											<TD vAlign="top" align="center" colSpan="3" height="376"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="450px">
													<DisplayLayout AutoGenerateColumns="False" AllowSortingDefault="OnClient" RowHeightDefault="20px"
														Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti"
														BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1"
														CellClickActionDefault="RowSelect" AllowUpdateDefault="Yes">
														<AddNewBox>
															<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
															</Style>
														</AddNewBox>
														<Pager PageSize="15" AllowPaging="True">
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
															BorderStyle="Solid" BackColor="Silver" Height="450px"></FrameStyle>
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
														<SelectedRowStyleDefault Cursor="Hand" Font-Bold="True" ForeColor="Linen" BackColor="#E0E5CD"></SelectedRowStyleDefault>
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
																<igtbl:UltraGridColumn HeaderText="선택" Key="chk" Width="35px" Type="CheckBox" HeaderClickAction="Select" 
 BaseColumnName="chk" AllowUpdate="Yes">
																	<CellButtonStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellButtonStyle>
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" HeaderClickAction="SortMulti" BaseColumnName="ItemDrawNum">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="공정순서번호" Key="ProcessSequenceNum" Format="#########" HeaderClickAction="SortMulti" 
 BaseColumnName="ProcessSequenceNum">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="공정코드" Key="ProcessCode" Hidden="True" HeaderClickAction="SortMulti" 
 BaseColumnName="ProcessCode">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="공정명" Key="ProcessName" HeaderClickAction="SortMulti" BaseColumnName="ProcessName">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="이전공정" Key="PreProcessName" HeaderClickAction="SortMulti" BaseColumnName="PreProcessName">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="제품번호" Key="ProductItemNum" Hidden="True" HeaderClickAction="SortMulti" 
 BaseColumnName="ProductItemNum">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="제품도면번호" Key="ProductDrawNum" Hidden="True" HeaderClickAction="SortMulti" 
 BaseColumnName="ProductDrawNum">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="제품명" Key="ProductName" HeaderClickAction="SortMulti" BaseColumnName="ProductName">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="모품목번호" Key="ParentItemNum" Hidden="True" HeaderClickAction="SortMulti" 
 BaseColumnName="ParentItemNum">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="모품목도면번호" Key="ParentDrawNum" Hidden="True" HeaderClickAction="SortMulti" 
 BaseColumnName="ParentDrawNum">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="모품목명" Key="ParentName" HeaderClickAction="SortMulti" BaseColumnName="ParentName">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="작업장명" Key="WCName" HeaderClickAction="SortMulti" BaseColumnName="WCName">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="작업구분" Key="WorkDistinction" HeaderClickAction="SortMulti" BaseColumnName="WorkDistinction">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="작업계획수량" Key="WorkPlanQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti" 
 BaseColumnName="WorkPlanQuantity">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="작업완료수량" Key="WorkCompletionQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti" 
 BaseColumnName="WorkCompletionQuantity">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="외주리드타임" Key="OrderLeadTime" Format="#########" HeaderClickAction="SortMulti" 
 BaseColumnName="OrderLeadTime">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="작업일자" Key="WorkDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti" 
 BaseColumnName="WorkDate">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" HeaderClickAction="SortMulti" BaseColumnName="ProgressCondition">
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
																<igtbl:UltraGridColumn HeaderText="생산계획번호" Key="ProductionPlanHistoryIndex" HeaderClickAction="SortMulti" 
 BaseColumnName="ProductionPlanHistoryIndex">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="작업계획번호" Key="WorkPlanHistoryIndex" HeaderClickAction="SortMulti" BaseColumnName="WorkPlanHistoryIndex">
																	<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																</igtbl:UltraGridColumn>
																<igtbl:UltraGridColumn HeaderText="WC일자별작업계획번호" Key="WCDailyWorkPlanHistoryIndex" HeaderClickAction="SortMulti" 
 BaseColumnName="WCDailyWorkPlanHistoryIndex">
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
										<TR>
											<TD align="right" colSpan="3"><FONT face="굴림"><INPUT id="chkAll" style="WIDTH: 51px; HEIGHT: 22px" type="hidden" size="3" value="true">&nbsp;&nbsp;&nbsp;
													<asp:button id="bt_Delete" runat="server" Font-Size="10pt" Text="취  소" Width="65px" Height="20px"></asp:button></FONT><FONT face="굴림">&nbsp;&nbsp;
												</FONT>
											</TD>
										</TR>
									</TBODY>
								</TABLE>
							</FIELDSET>
						</TD>
					</TR>
				</TABLE>
				&nbsp;&nbsp;
				<igcmbo:webcombo id="wcbItemName" style="Z-INDEX: 101; LEFT: 824px; POSITION: absolute; TOP: 400px"
					runat="server" Font-Size="10pt" Width="125px" Height="18px" Version="3.00" BackColor="White"
					BorderStyle="Groove" BorderWidth="1px" ForeColor="Black" BorderColor="Gray" SelBackColor="10, 36, 106"
					SelectedIndex="-1" SelForeColor="White" Editable="True" DropImageXP1="/ig_common/WebGrid3/ig_cmboDown1.bmp"
					DropImageXP2="/ig_common/WebGrid3/ig_cmboDown2.bmp" Visible="False">
					<Columns>
						<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="160px" BaseColumnName="ItemNum">
							<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
						</igtbl:UltraGridColumn>
						<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Width="160px" BaseColumnName="ItemDrawNum">
							<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
						</igtbl:UltraGridColumn>
						<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="160px" BaseColumnName="ItemName">
							<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
						</igtbl:UltraGridColumn>
						<igtbl:UltraGridColumn HeaderText="번호" Key="ItemInfoIndex" Width="20px" Hidden="True" BaseColumnName="ItemInfoIndex"></igtbl:UltraGridColumn>
					</Columns>
					<DropDownLayout DropdownWidth="500px" BorderCollapse="Separate" AllowColSizing="Free" RowHeightDefault="20px"
						HeaderClickAction="Select" AutoGenerateColumns="False" AllowSorting="OnClient" DropdownHeight="300px">
						<RowStyle BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="White">
							<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
						</RowStyle>
						<SelectedRowStyle ForeColor="White" BackColor="#0A246A"></SelectedRowStyle>
						<HeaderStyle BorderStyle="Solid" BackColor="LightGray">
							<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
						</HeaderStyle>
						<FrameStyle Width="100%" Cursor="Default" BorderWidth="2px" Font-Size="10pt" Font-Names="Verdana"
							BorderStyle="Ridge" BackColor="Silver" Height="100%"></FrameStyle>
					</DropDownLayout>
					<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
				</igcmbo:webcombo></FONT></form>
	</body>
</HTML>
