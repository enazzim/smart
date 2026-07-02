<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Page language="c#" Codebehind="StorehouseMovingPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BusinessManagement.StorehouseMovingPC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>StorehouseMovingPC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		var GridName;
		
		function UltraWebGrid1_ColumnHeaderClickHandler(gridName, columnId, button)
		{
			GridName = gridName;
    			if(document.StorehouseMovingPC.chkAll.value == "true")
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
			document.StorehouseMovingPC.chkAll.value = "false";
		}


		function AllUncheck() // 전부체크 해제하기
		{
			var grid = igtbl_getGridById(GridName);
			
			for(var i=0; i<grid.Rows.length; i++)
			{
				var row = grid.Rows.getRow(i);
				row.getCellFromKey("chk").setValue(false);
			}
			document.StorehouseMovingPC.chkAll.value = "true";
		}
		function OK(text)
		{
			if(confirm(text + "하시겠습니까?"))
				return true;
			else
				return false;
		}
		// 그리드 Row인덱스 찾기
		function UltraWebGrid1_MouseOverHandler(gridName, id, button)
		{
			//Add code to handle your event here.
			if(button == 0)
			{ // Are we over a cell
				var cell = igtbl_getElementById(id);
				cell.style.cursor = 'hand';
				//var label = igtbl_getElementById("lb_RowIndex");
				var parts = id.split("_");
				document.StorehouseMovingPC.lb_RowIndex.value = parts[1];
			}
		}
		//수정창이 열릴때
		function UltraWebGrid1_AfterRowTemplateOpenHandler(gridName, rowId)
		{
			//Add code to handle your event here.
			//그리드의 Row 값을 저장함
			var row = igtbl_getRowById(rowId);
			document.StorehouseMovingPC.lb_RowSelectIndex.value = document.StorehouseMovingPC.lb_RowIndex.value
			document.StorehouseMovingPC.lb_Quantity.value = document.getElementById(rowId).children[8].innerText;
			
			for(var i=0;i<document.StorehouseMovingPC.UltraWebGrid1__ctl0_dl_EditStore.options.length;i++)
			{
				if(document.StorehouseMovingPC.UltraWebGrid1__ctl0_dl_EditStore.options[i].innerText == document.getElementById(rowId).children[7].innerText)
				{
					document.StorehouseMovingPC.UltraWebGrid1__ctl0_dl_EditStore.options[i].selected=true
				}
			}
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#f7f6f6">
		<form id="StorehouseMovingPC" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" height="550"
				cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD width="20"></TD>
					<TD vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND style="FONT-SIZE: 9pt" align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD width="600" colSpan="4" height="30">
										<uc1:ItemSearchControl id="ItemSearchControl1" runat="server"></uc1:ItemSearchControl></TD>
									<TD height="30"></TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림">이동일자&nbsp;
										</FONT>
									</TD>
									<TD width="100" height="30">
										<igsch:webdatechooser id="wdc_FromDate" runat="server" BorderStyle="Solid" BorderColor="DimGray" BackColor="#EEEEE9"
											Text=" " NullDateLabel=" " Width="115px">
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
										</igsch:webdatechooser></TD>
									<TD align="center" width="15" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림">~</FONT></TD>
									<TD width="415" height="30">
										<igsch:webdatechooser id="wdc_ToDate" runat="server" BorderStyle="Solid" BorderColor="DimGray" BackColor="#EEEEE9"
											Text=" " NullDateLabel=" " Width="115px">
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
										</igsch:webdatechooser></TD>
									<TD align="right" width="200" height="30">
										<asp:button id="bt_Clear" runat="server" Text="초기화" Width="60px" Height="20px"></asp:button><FONT face="굴림">&nbsp;</FONT>
										<asp:button id="bt_Search" runat="server" Text="검  색" Width="60px" Height="20px"></asp:button><FONT face="굴림">&nbsp;</FONT></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="20"></TD>
					<TD vAlign="top" align="center" width="800">&nbsp;
						<BR>
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top" style="FONT-SIZE: 9pt">[검색결과]</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<TD colSpan="2"><FONT face="굴림">
											<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="406px">
												<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
													RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
													HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
													RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect"
													AllowUpdateDefault="RowTemplateOnly">
													<AddNewBox>
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														</Style>
													</AddNewBox>
													<Pager PageSize="17" StyleMode="ComboBox" AllowPaging="True">
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														</Style>
													</Pager>
													<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</HeaderStyleDefault>
													<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
													<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="Verdana" BorderColor="DimGray"
														BorderStyle="Solid" BackColor="Silver" Height="406px"></FrameStyle>
													<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</FooterStyleDefault>
													<ClientSideEvents ColumnHeaderClickHandler="UltraWebGrid1_ColumnHeaderClickHandler" AfterRowTemplateOpenHandler="UltraWebGrid1_AfterRowTemplateOpenHandler"
														MouseOverHandler="UltraWebGrid1_MouseOverHandler"></ClientSideEvents>
													<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
													<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
													<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
													<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
														<Padding Left="3px"></Padding>
														<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
													</RowStyleDefault>
												</DisplayLayout>
												<Bands>
													<igtbl:UltraGridBand AllowUpdate="RowTemplateOnly">
														<Columns>
															<igtbl:UltraGridColumn HeaderText="선택" Key="chk" Width="30px" Type="CheckBox" HeaderClickAction="Select"
																BaseColumnName="chk" AllowUpdate="Yes">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" HeaderClickAction="SortMulti" BaseColumnName="ItemNum"></igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" HeaderClickAction="SortMulti" BaseColumnName="ItemDrawNum"></igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" HeaderClickAction="SortMulti" BaseColumnName="ItemName"></igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="공정코드" Key="ProcessCode" Hidden="True" HeaderClickAction="SortMulti"
																BaseColumnName="ProcessCode"></igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="원창고명" Key="OriginalStorehouseName" HeaderClickAction="SortMulti" BaseColumnName="OriginalStorehouseName">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="원창고번호" Key="BusinessStorehouseNum1" Hidden="True" HeaderClickAction="SortMulti"
																BaseColumnName="BusinessStorehouseNum1"></igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="이동창고명" Key="MovingStorehoseName" Format="" HeaderClickAction="SortMulti"
																BaseColumnName="MovingStorehoseName"></igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="이동수량" Key="MovingQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
																BaseColumnName="MovingQuantity">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="이동창고번호" Key="BusinessStorehouseNum2" Hidden="True" Format="" HeaderClickAction="SortMulti"
																BaseColumnName="BusinessStorehouseNum2"></igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" HeaderClickAction="SortMulti" BaseColumnName="RegistrationPerson"></igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" HeaderClickAction="SortMulti"
																BaseColumnName="RegistrationPersonID">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
																BaseColumnName="RegistrationDate"></igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" HeaderClickAction="SortMulti" BaseColumnName="UpdatingPerson"></igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" HeaderClickAction="SortMulti"
																BaseColumnName="UpdatingPersonID">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
																BaseColumnName="UpdatingDate"></igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="창고이동원장번호" Key="StorehouseMovingHistoryIndex" HeaderClickAction="SortMulti"
																BaseColumnName="StorehouseMovingHistoryIndex"></igtbl:UltraGridColumn>
														</Columns>
														<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
															<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
														</RowTemplateStyle>
														<RowEditTemplate>
															<TABLE style="WIDTH: 858px; HEIGHT: 58px">
																<TR>
																	<TD align="right">
																		<asp:Label id="Label1" runat="server" Font-Size="10pt">품목번호</asp:Label></TD>
																	<TD>
																		<INPUT id="igtbl_TextBox_0_1" style="WIDTH: 100px" readOnly type="text" columnKey="ItemNum"></TD>
																	<TD align="right">
																		<asp:Label id="Label7" runat="server" Font-Size="10pt">도면번호</asp:Label></TD>
																	<TD><FONT face="굴림"> <INPUT id="igtbl_TextBox_0_2" style="WIDTH: 100px" readOnly type="text" columnKey="ItemDrawNum"></FONT></TD>
																	<TD align="right">
																		<asp:Label id="Label8" runat="server" Font-Size="10pt">품목명</asp:Label></TD>
																	<TD><FONT face="굴림"> <INPUT id="igtbl_TextBox_0_3" style="WIDTH: 100px" readOnly type="text" columnKey="ItemName"></FONT></TD>
																	<TD align="right">
																		<asp:Label id="Label9" runat="server" Font-Size="10pt">원창고명</asp:Label></TD>
																	<TD>
																		<INPUT id="igtbl_TextBox_0_5" style="WIDTH: 100px" readOnly type="text" columnKey="OriginalStorehouseName"></TD>
																</TR>
																<TR>
																	<TD align="right">
																		<asp:Label id="Label6" runat="server" Font-Size="10pt">이동창고</asp:Label></TD>
																	<TD align="left">
																		<asp:DropDownList id="dl_EditStore" runat="server" columnKey="MovingStorehoseName">
																			<asp:ListItem Value="영업1창고">영업1창고</asp:ListItem>
																			<asp:ListItem Value="영업2창고">영업2창고</asp:ListItem>
																			<asp:ListItem Value="영업3창고">영업3창고</asp:ListItem>
																		</asp:DropDownList></TD>
																	<TD>
																		<asp:Label id="Label10" runat="server" Font-Size="10pt">이동수량</asp:Label></TD>
																	<TD>
																		<INPUT id="igtbl_TextBox_0_8" style="WIDTH: 100px; TEXT-ALIGN: right" type="text" columnKey="MovingQuantity"></TD>
																	<TD align="right"><FONT face="굴림"></FONT></TD>
																	<TD></TD>
																	<TD></TD>
																	<TD><FONT face="굴림"></FONT></TD>
																</TR>
															</TABLE>
															<BR>
															<P align="center">
																<INPUT id="igtbl_reOkBtn" style="WIDTH: 60px; HEIGHT: 20px" onclick="igtbl_gRowEditButtonClick(event);__doPostBack('lnk_Update','')"
																	type="button" value="수  정">&nbsp; <INPUT id="igtbl_reCancelBtn" style="WIDTH: 60px; HEIGHT: 20px" onclick="igtbl_gRowEditButtonClick(event);"
																	type="button" size="20" value="취  소"></P>
														</RowEditTemplate>
													</igtbl:UltraGridBand>
												</Bands>
											</igtbl:ultrawebgrid></FONT></TD>
								</TR>
								<TR>
									<TD height="30"><FONT face="굴림">&nbsp;</FONT><asp:button id="bt_Excel" runat="server" Font-Size="10pt" Width="60px" Height="20px" Text="Excel"></asp:button></TD>
									<TD align="right" height="30"><INPUT id="lb_Quantity" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1"
											runat="server"><INPUT id="lb_RowIndex" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1"
											runat="server"><INPUT id="lb_RowSelectIndex" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1"
											runat="server">
										<asp:linkbutton id="lnk_Update" runat="server" Visible="False" Font-Size="9pt"></asp:linkbutton><INPUT id="chkAll" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" value="true"><asp:button id="bt_Delete" runat="server" Font-Size="10pt" Width="60px" Height="20px" Text="삭  제"></asp:button>&nbsp;</TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			<igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter></form>
	</body>
</HTML>
