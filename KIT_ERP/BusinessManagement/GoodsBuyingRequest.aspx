<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Page language="c#" Codebehind="GoodsBuyingRequest.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BusinessManagement.GoodsBuyingRequest" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>GoodsBuyingRequest</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--
		
		function ResettxtBox()
		{
			ResetTextBox();
			ResetBox();
			var objChooser1 = igdrp_getComboById("wdcFromDate");
			var objChooser2 = igdrp_getComboById("wdcToDate");
			var objChooser3 = igdrp_getComboById("wdcBeginDate");
			var objChooser4 = igdrp_getComboById("wdcEndDate");
			objChooser1.setValue(null);
			objChooser2.setValue(null);
			objChooser3.setValue(null);
			objChooser4.setValue(null);
		}
		
		
		var GridName;
		
		function UltraWebGrid1_ColumnHeaderClickHandler(gridName, columnId, button)
		{
			GridName = gridName;
    			if(document.GoodsBuyingRequest.chkAll.value == "true")
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
			document.GoodsBuyingRequest.chkAll.value = "false";
		}


		function AllUncheck() // 전부체크 해제하기
		{
			var grid = igtbl_getGridById(GridName);
			
			for(var i=0; i<grid.Rows.length; i++)
			{
				var row = grid.Rows.getRow(i);
				row.getCellFromKey("chk").setValue(false);
			}
			document.GoodsBuyingRequest.chkAll.value = "true";
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
	<body MS_POSITIONING="GridLayout">
		<form id="GoodsBuyingRequest" method="post" runat="server">
			<TABLE id="Table1" style="PADDING-LEFT: 10px; LEFT: 10px; PADDING-TOP: 10px; POSITION: absolute; TOP: 10px"
				height="540" cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 800px; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top"><asp:label id="searchTitle" Runat="server" text="[검색조건]" Font-Size="10pt">[검색조건]</asp:label></LEGEND>
							<TABLE id="Table2" style="HEIGHT: 2px" cellSpacing="0" cellPadding="0" width="800">
								<TR>
									<TD align="left" width="800" colSpan="9" height="30">
										<table id="table4" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<tr>
												<td align="left" width="25%" height="30"><FONT face="굴림"><uc1:companysearchcontrol id="CSC1" runat="server"></uc1:companysearchcontrol></FONT></td>
												<td align="left" width="75%" height="30"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></td>
											</tr>
										</table>
									</TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30"><FONT face="굴림">수주일자&nbsp; </FONT>
									</TD>
									<TD width="100" height="30"><FONT face="굴림"><igsch:webdatechooser id="wdcBeginDate" runat="server" Font-Size="9pt" Text=" " NullDateLabel=" " MaxDate="2076-06-06"
												Width="100px" BorderStyle="Solid" BackColor="#EEEEE9" Height="20px" BorderColor="DimGray">
												<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
													MaxDate="2076-06-06" ShowTitle="False" ShowFooter="False">
													<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
													<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
													<DropDownStyle BackColor="White"></DropDownStyle>
													<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
												</CalendarLayout>
												<DropDownStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></DropDownStyle>
												<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
												<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
											</igsch:webdatechooser></FONT></TD>
									<TD align="center" width="10" height="30"><FONT face="굴림">~</FONT></TD>
									<TD width="100" height="30"><igsch:webdatechooser id="wdcEndDate" runat="server" Font-Size="9pt" Text=" " NullDateLabel=" " MaxDate="2076-06-06"
											Width="100px" BorderStyle="Solid" BackColor="#EEEEE9" Height="20px" BorderColor="DimGray">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												MaxDate="2076-06-06" ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD align="right" width="70" height="30"><FONT face="굴림">납기일&nbsp; </FONT>
									</TD>
									<TD width="100" height="30"><FONT face="굴림"><igsch:webdatechooser id="wdcFromDate" runat="server" Font-Size="9pt" Text=" " NullDateLabel=" " MaxDate="2076-06-06"
												Width="100px" BorderStyle="Solid" BackColor="#EEEEE9" Height="20px" BorderColor="DimGray">
												<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
													MaxDate="2076-06-06" ShowTitle="False" ShowFooter="False">
													<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
													<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
													<DropDownStyle BackColor="White"></DropDownStyle>
													<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
												</CalendarLayout>
												<DropDownStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></DropDownStyle>
												<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
												<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
											</igsch:webdatechooser></FONT></TD>
									<TD align="center" width="10" height="30"><FONT face="굴림">~</FONT></TD>
									<TD width="100" height="30"><igsch:webdatechooser id="wdcToDate" runat="server" Font-Size="9pt" Text=" " NullDateLabel=" " MaxDate="2076-06-06"
											Width="100px" BorderStyle="Solid" BackColor="#EEEEE9" Height="20px" BorderColor="DimGray">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												MaxDate="2076-06-06" ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD align="right" width="240" height="30"><FONT face="굴림"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
												type="button" value="초기화" name="btnReset">&nbsp;</FONT>
										<asp:button id="bt_Search" runat="server" Text="검색" Width="60px" Height="20px"></asp:button><FONT face="굴림">&nbsp;
										</FONT>
									</TD>
								</TR>
								<TR>
									<TD align="right" colSpan="9" height="8"><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"></FONT></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND align="top"><asp:label id="Label2" Runat="server" text="[검색조건]" Font-Size="10pt">[검색결과]</asp:label></LEGEND>
							<TABLE id="Table3" style="HEIGHT: 440px" cellSpacing="1" cellPadding="1" width="800" border="0">
								<TR>
									<TD><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="100%" Height="411px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" AllowColSizingDefault="Free" RowSelectorsDefault="No"
												Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="Edit">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</AddNewBox>
												<Pager PageSize="18" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</Pager>
												<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<FrameStyle Width="100%" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
													BackColor="Silver" Height="411px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents ColumnHeaderClickHandler="UltraWebGrid1_ColumnHeaderClickHandler"></ClientSideEvents>
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
														<igtbl:UltraGridColumn HeaderText="전체" Key="chk" Width="30px" Type="CheckBox" HeaderClickAction="Select"
															BaseColumnName="chk" AllowResize="Free" AllowUpdate="Yes">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="110px" HeaderClickAction="SortSingle" BaseColumnName="ItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ItemDrawNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="110px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Left"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" HeaderClickAction="SortMulti" BaseColumnName="CompanyName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사업자번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="BusinessRegistrationNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="자산분류" Key="PropertyClassification" Width="80px" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="PropertyClassification">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수주일자" Key="ReceivingOrderDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="ReceivingOrderDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="납기요구일" Key="DeliveryRequestDate1" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="DeliveryRequestDate1">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총수주량" Key="TotalReceiveingOrderQuantity" Width="82px" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="TotalReceiveingOrderQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="가용재고" Key="RemainQuantity" Width="80px" BaseColumnName="RemainQuantity">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="의뢰량" Key="RequestQuantity" Width="80px" Format="###,###" BaseColumnName="RequestQuantity"
															AllowUpdate="Yes">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Hidden="True" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
															BaseColumnName="ApplyUnitCost">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="1차납기요구량" Key="DeliveryRequestQuantity1" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestQuantity1">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="2차납기요구량" Key="DeliveryRequestQuantity2" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestQuantity2">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="2차납기요구일" Key="DeliveryRequestDate2" Hidden="True" Format="yyyy-MM-dd"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestDate2">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="3차납기요구량" Key="DeliveryRequestQuantity3" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestQuantity3">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="3차납기요구일" Key="DeliveryRequestDate3" Hidden="True" Format="yyyy-MM-dd"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestDate3">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="4차납기요구량" Key="DeliveryRequestQuantity4" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestQuantity4">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="4차납기요구일" Key="DeliveryRequestDate4" Hidden="True" Format="yyyy-MM-dd"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestDate4">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="5차납기요구량" Key="DeliveryRequestQuantity5" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestQuantity5">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="5차납기요구일" Key="DeliveryRequestDate5" Hidden="True" Format="yyyy-MM-dd"
															HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestDate5">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="출고수량" Key="OutStorehouseQuantity" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="OutStorehouseQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="합격수량" Key="SuitabilityQuantity" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="SuitabilityQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="미검수량" Key="UnInspectionQuantity" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="UnInspectionQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="볼륨번호" Key="VolumNum" Hidden="True" HeaderClickAction="SortMulti" BaseColumnName="VolumNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ProgressCondition">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="납품잔량" Key="RemainderQuantity" Hidden="True" Format="###,###,###.##"
															HeaderClickAction="SortMulti" BaseColumnName="RemainderQuantity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationPerson">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="RegistrationPersonID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingPerson">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingPersonID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
															BaseColumnName="UpdatingDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="번호" Key="ReceivingOrderHistoryIndex" Hidden="True" HeaderClickAction="SortMulti"
															BaseColumnName="ReceivingOrderHistoryIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
								<tr>
									<td align="right"><INPUT id="chkAll" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" value="true"><asp:button id="bt_Register" runat="server" Text="구매의뢰" Width="60px" Height="20px"></asp:button></td>
								</tr>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
