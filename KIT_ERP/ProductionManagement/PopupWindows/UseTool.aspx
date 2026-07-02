<%@ Page language="c#" Codebehind="UseTool.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.PopupWindows.UseTool" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>UseTool</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--
		var GridName;
		
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
		function UltraWebGrid1_ColumnHeaderClickHandler(gridName, columnId, button){
			//Add code to handle your event here.
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
		
		function Register()
		{
			alert("등록 했습니다!");
			window.opener.SubmitLinkButton();self.close();	
		}
		
		function Register1()
		{
			window.opener.SubmitLinkButton();self.close();	
		}
		
		
--></SCRIPT>
	</HEAD>
	<body style="FONT-SIZE: 9pt" bgColor="#eeeee9" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<FIELDSET style="Z-INDEX: 101; BORDER-BOTTOM: dimgray 2px solid; POSITION: absolute; BORDER-LEFT: dimgray 2px solid; WIDTH: 630px; HEIGHT: 90px; BORDER-TOP: dimgray 2px solid; TOP: 8px; BORDER-RIGHT: dimgray 2px solid; LEFT: 8px"
				align="left"><LEGEND>[입 력]</LEGEND>
				<DIV>
					<TABLE id="table2" style="FONT-SIZE: 12px" cellSpacing="0" cellPadding="0">
						<TR>
							<TD style="WIDTH: 80px; HEIGHT: 20px" align="right"><FONT face="굴림">사용공구1&nbsp;</FONT></TD>
							<TD style="WIDTH: 130px; HEIGHT: 20px"><asp:dropdownlist id="dl_UseTool1" runat="server" BackColor="#F7F6F6" Width="128px"></asp:dropdownlist></TD>
							<TD style="WIDTH: 80px; HEIGHT: 20px" align="right"><FONT face="굴림">사용공구2&nbsp;</FONT></TD>
							<TD style="WIDTH: 130px; HEIGHT: 20px"><asp:dropdownlist id="dl_UseTool2" runat="server" BackColor="#F7F6F6" Width="128px"></asp:dropdownlist></TD>
							<TD style="WIDTH: 80px; HEIGHT: 20px" align="right"><FONT face="굴림">사용공구3&nbsp;</FONT></TD>
							<TD style="WIDTH: 130px; HEIGHT: 20px"><asp:dropdownlist id="dl_UseTool3" runat="server" BackColor="#F7F6F6" Width="128px"></asp:dropdownlist></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 80px; HEIGHT: 20px" align="right"><FONT face="굴림">사용치구1&nbsp;</FONT></TD>
							<TD style="WIDTH: 130px; HEIGHT: 20px"><FONT face="굴림"></FONT><asp:dropdownlist id="dl_UseJig1" runat="server" BackColor="#F7F6F6" Width="128px"></asp:dropdownlist></TD>
							<TD style="WIDTH: 80px; HEIGHT: 20px" align="right"><FONT face="굴림">사용치구2&nbsp;</FONT></TD>
							<TD style="WIDTH: 130px; HEIGHT: 20px"><asp:dropdownlist id="dl_UseJig2" runat="server" BackColor="#F7F6F6" Width="128px"></asp:dropdownlist></TD>
							<TD style="WIDTH: 80px; HEIGHT: 20px" align="right"><FONT face="굴림">사용치구3&nbsp;</FONT></TD>
							<TD style="WIDTH: 130px; HEIGHT: 20px"><asp:dropdownlist id="dl_UseJig3" runat="server" BackColor="#F7F6F6" Width="128px"></asp:dropdownlist></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 80px; HEIGHT: 20px" align="right"><FONT face="굴림">작업장명&nbsp;</FONT></TD>
							<TD style="WIDTH: 130px; HEIGHT: 20px"><FONT face="굴림"><asp:dropdownlist id="dl_WCName" runat="server" BackColor="#F7F6F6" Width="128px"></asp:dropdownlist></FONT></TD>
							<TD style="WIDTH: 80px; HEIGHT: 20px" align="right">작업자&nbsp;</TD>
							<TD style="WIDTH: 130px; HEIGHT: 20px"><FONT face="굴림"><asp:dropdownlist id="dl_Worker" runat="server" BackColor="#F7F6F6" Width="128px"></asp:dropdownlist></FONT></TD>
							<TD style="WIDTH: 80px; HEIGHT: 20px" align="right"><FONT face="굴림"><INPUT id="chkAll" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" value="true"
										name="Hidden1" runat="server"></FONT></TD>
							<TD style="WIDTH: 130px; HEIGHT: 20px"><asp:button id="Button1" runat="server" Height="20px" Text="등  록"></asp:button></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 80px; HEIGHT: 20px" align="right"><FONT face="굴림">하위품목&nbsp;</FONT></TD>
							<TD style="WIDTH: 130px; HEIGHT: 20px"><FONT face="굴림"></FONT></TD>
							<TD style="WIDTH: 80px; HEIGHT: 20px" align="right"></TD>
							<TD style="WIDTH: 130px; HEIGHT: 20px"></TD>
							<TD style="WIDTH: 80px; HEIGHT: 20px" align="right"></TD>
							<TD style="WIDTH: 130px; HEIGHT: 20px"><FONT face="굴림"></FONT></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 630px; HEIGHT: 20px" align="left" colSpan="6"><FONT face="굴림"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="630px" Height="336px">
										<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
											TabDirection="TopToBottom" RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single"
											ScrollBarView="Vertical" ScrollBar="Always" AllowColumnMovingDefault="OnServer" BorderCollapseDefault="Separate"
											AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed"
											CellClickActionDefault="Edit">
											<AddNewBox>
												<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
												</Style>
											</AddNewBox>
											<Pager>
												<Style VerticalAlign="Middle" BorderWidth="1px" BorderStyle="Solid" HorizontalAlign="Center"
													BackColor="LightGray">
												</Style>
											</Pager>
											<HeaderStyleDefault Font-Bold="True" BorderColor="Black" BorderStyle="Solid" ForeColor="Black" BackColor="PowderBlue">
												<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
											</HeaderStyleDefault>
											<GroupByRowStyleDefault ForeColor="Black" BackColor="#95B9F0"></GroupByRowStyleDefault>
											<RowSelectorStyleDefault BorderStyle="Solid"></RowSelectorStyleDefault>
											<FrameStyle Width="630px" Cursor="Default" BorderWidth="1px" Font-Size="10pt" Font-Names="굴림"
												BorderStyle="Solid" BackColor="Silver" Height="336px"></FrameStyle>
											<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
												<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
											</FooterStyleDefault>
											<ClientSideEvents ColumnHeaderClickHandler="UltraWebGrid1_ColumnHeaderClickHandler"></ClientSideEvents>
											<GroupByBox>
												<Style BackColor="#B5CFF7">
												</Style>
												<BandLabelStyle ForeColor="White" BackColor="#6372D4"></BandLabelStyle>
											</GroupByBox>
											<SelectedHeaderStyleDefault BackColor="LightCyan"></SelectedHeaderStyleDefault>
											<EditCellStyleDefault BorderWidth="0px" BorderStyle="None" BackColor="#FFE0C0"></EditCellStyleDefault>
											<SelectedGroupByRowStyleDefault BorderColor="Yellow" BackColor="Yellow"></SelectedGroupByRowStyleDefault>
											<SelectedRowStyleDefault Cursor="Hand" Font-Bold="True" BorderColor="Black" ForeColor="Linen" BackColor="Transparent"></SelectedRowStyleDefault>
											<RowAlternateStyleDefault Cursor="Hand" BackColor="AliceBlue"></RowAlternateStyleDefault>
											<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" ForeColor="Black"
												BackColor="#EDEFF6">
												<Padding Left="3px"></Padding>
												<BorderDetails ColorTop="Gray" WidthLeft="0px" WidthTop="0px" ColorLeft="Gray"></BorderDetails>
											</RowStyleDefault>
										</DisplayLayout>
										<Bands>
											<igtbl:UltraGridBand AllowUpdate="No">
												<Columns>
													<igtbl:UltraGridColumn HeaderText="선택" Key="chk" Width="40px" Type="CheckBox" HeaderClickAction="Select"
														BaseColumnName="chk" AllowUpdate="Yes">
														<SelectedCellStyle HorizontalAlign="Center"></SelectedCellStyle>
														<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														<HeaderStyle Height="25px"></HeaderStyle>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="소진 품목번호" Key="ItemNum" Width="162px" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
														<SelectedCellStyle HorizontalAlign="Center"></SelectedCellStyle>
														<CellStyle VerticalAlign="Middle" HorizontalAlign="Left">
															<Padding Left="2px"></Padding>
														</CellStyle>
														<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Width="150px" Hidden="True" HeaderClickAction="SortMulti"
														BaseColumnName="ItemDrawNum">
														<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="소진 품목명" Key="ItemName" Width="160px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
														<CellStyle VerticalAlign="Middle" HorizontalAlign="Left">
															<Padding Left="2px"></Padding>
														</CellStyle>
														<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="순서" Key="ProcessSequenceNum" Width="50px" HeaderClickAction="SortMulti"
														BaseColumnName="ProcessSequenceNum">
														<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="공정코드" Key="ProcessCode" Width="100px" Hidden="True" HeaderClickAction="SortMulti"
														BaseColumnName="ProcessCode">
														<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="공정명" Key="ProcessName" Width="95px" HeaderClickAction="SortMulti" BaseColumnName="ProcessName">
														<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="소요수량" Key="WorkPlanQuantity" Width="104px" Format="###,###,###.##" HeaderClickAction="SortMulti"
														BaseColumnName="WorkPlanQuantity" NullText="0" AllowUpdate="Yes">
														<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
															<Padding Right="4px"></Padding>
														</CellStyle>
														<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
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
									</igtbl:ultrawebgrid></FONT></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 80px; HEIGHT: 20px" align="right" colSpan="6"><FONT face="굴림"></FONT><FONT face="굴림"></FONT></TD>
						</TR>
					</TABLE>
				</DIV>
			</FIELDSET>
		</form>
	</body>
</HTML>
