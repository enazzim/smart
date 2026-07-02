<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="WCChange.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.PopupWindows.WCChange" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WCChange</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT type="text/javascript"><!--

		function Change()
		{
			alert("작업구분을 변경하였습니다!");
			window.opener.SubmitLinkButton();self.close();	
		}
		function Stop()
		{
			alert("작업을 중단하였습니다!");	
			window.opener.SubmitLinkButton();self.close();	
		}
		function Order()
		{
			window.opener.SubmitLinkButton();self.close();	
		}
		
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
		
		function OK(text)
		{
			if(confirm(text + "하시겠습니까?"))
				return true;
			else
				return false;
		}
	--></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server" onblur="self.focus()">
			<FIELDSET style="Z-INDEX: 101; LEFT: 8px; WIDTH: 757px; POSITION: absolute; TOP: 8px; HEIGHT: 368px"
				align="left">
				<DIV>
					<TABLE id="table1" style="WIDTH: 744px; HEIGHT: 342px">
						<TR>
							<TD style="HEIGHT: 285px" width="700" vAlign="top"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="336px" Width="800px">
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
												<igtbl:UltraGridColumn HeaderText="선택" Key="chk" Width="40px" Type="CheckBox" HeaderClickAction="Select"
													BaseColumnName="chk" AllowUpdate="Yes">
													<SelectedCellStyle HorizontalAlign="Center"></SelectedCellStyle>
													<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
													<HeaderStyle Height="25px"></HeaderStyle>
												</igtbl:UltraGridColumn>
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
					<table style="WIDTH: 800px; HEIGHT: 32px" cellSpacing="0" cellPadding="0">
						<tr>
							<td valign="middle" style="WIDTH: 76px"><FONT style="FONT-SIZE: x-small" face="굴림">작업시작일 
									:</FONT>
							</td>
							<td>
								<igsch:webdatechooser id="wdcWorkDate" runat="server" Width="104px" Height="18px" Text=" " Font-Size="10pt"
									NullDateLabel=" " BackColor="#EEEEE9" BorderStyle="Solid" BorderColor="DimGray">
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
								</igsch:webdatechooser></FONT>
							</td>
						</tr>
						<tr>
							<td style="WIDTH: 76px"><FONT face="굴림"></FONT><INPUT id="chkAll" style="WIDTH: 56px; HEIGHT: 22px" type="hidden" size="4" value="true"
									name="chkAll" runat="server">&nbsp;
							</td>
							<td align="right">
								<asp:button id="btChange" runat="server" Height="20px" Text="작업 구분 변경" Width="104px"></asp:button><FONT face="굴림">&nbsp;&nbsp;
									<asp:button id="btnCancel" runat="server" Width="60px" Height="20px" Text="취  소"></asp:button>&nbsp;
								</FONT>
								<asp:button id="btStop" runat="server" Height="20px" Text="중  단"></asp:button><FONT face="굴림">&nbsp;&nbsp;
								</FONT>
								<asp:button id="btWorkOrder" runat="server" Height="20px" Text="작업 지시"></asp:button></td>
						</tr>
					</table>
				</DIV>
			</FIELDSET>
			&nbsp;&nbsp;&nbsp;&nbsp; </FONT></form>
	</body>
</HTML>
