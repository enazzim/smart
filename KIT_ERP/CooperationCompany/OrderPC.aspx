<%@ Page language="c#" Codebehind="OrderPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.CooperationCompany.OrderPC" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		
		//그리드에서 행을 선택했을때 
		function uwgBO_HT_AfterSelectChangeHandler(gridName, rowId){
			
			var row = igtbl_getRowById(rowId);								
			var frm = document.Form1;
					
			///////////RowIndex Hidden 에 저장////////////////
			var num = rowId.split('_');
			frm.hdRowIndex.value = num[1];
			/////////////////////////////////////////////////
		}
				
		function uwgBO_HT_DblClickHandler(gridName, cellId){
			
			// 초기화
			document.Form1.txtItemNum.value = "";
			document.Form1.txtItemDrawNum.value = "";
			document.Form1.txtItemName.value = "";
			document.Form1.txtDeliveryQuantity.value = "";
			document.Form1.txtOrderQuantity.value = "";
			document.Form1.txtCashQuantity.value = "";
			document.Form1.txtRemainQuantity.value = "";
			
			var row = igtbl_getRowById(cellId);//선택한 로우의 인덱스		
			
			if(row.getCellFromKey("OutSideOrderHistoryIndex").getValue() != null)
			{				
				document.Form1.hddivision.value = "0";//외주납품의뢰을 선택한 것을 표시함		
			}
			else
			{										
				document.Form1.hddivision.value = "1";//구매납품의뢰을 선택한 것을 표시함			
			}
				//그리드에서 부모내용을 선택했을때 항목 값채우기
				document.Form1.txtItemNum.value = row.getCellFromKey("ItemNum").getValue();//그리드에서 선택한 품목번호를 유저컨트롤내의 품목번호 컨트롤에 넣음
				document.Form1.txtItemDrawNum.value = row.getCellFromKey("ItemDrawNum").getValue();//그리드에서 선택한 도면번호를 유저컨트롤내의 품목번호 컨트롤에 넣음
				document.Form1.txtItemName.value = row.getCellFromKey("ItemName").getValue();//그리드에서 선택한 품목명를 유저컨트롤내의 품목번호 컨트롤에 넣음
			
				document.Form1.txtDeliveryQuantity.value = row.getCellFromKey("RemainQuantity").getValue();//납품수량
				document.Form1.txtOrderQuantity.value = row.getCellFromKey("OrderQuantity").getValue();//발주수량
				document.Form1.txtCashQuantity.value = Number(row.getCellFromKey("OrderQuantity").getValue()) - Number(row.getCellFromKey("RemainQuantity").getValue());//납입수량
				document.Form1.txtRemainQuantity.value = row.getCellFromKey("RemainQuantity").getValue();//잔량					
			
		}
		
	
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px; HEIGHT: 550px"
				cellSpacing="0" cellPadding="0" align="center" border="0" width="800">
				<TBODY>
					<TR>
						<TD style="WIDTH: 20px; HEIGHT: 249px" vAlign="top" align="left"><FONT face="굴림"></FONT></TD>
						<TD style="HEIGHT: 249px" vAlign="top" align="right"><FONT face="굴림"></FONT></TD>
						<TD style="HEIGHT: 249px" vAlign="top" align="center">
							<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 800px; BORDER-BOTTOM: #696969 2px solid; HEIGHT: 232px"
								align="top"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 발주현황 ]
								</LEGEND>
								<TABLE id="Table4" style="WIDTH: 808px; HEIGHT: 160px" cellSpacing="0" cellPadding="0">
									<tr>
										<td vAlign="top" align="center"></FONT><igtbl:ultrawebgrid id="uwgBO_HT" runat="server" Width="100%" Height="230px">
												<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
													RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
													HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
													RowSelectorsDefault="No" Name="uwgBOxHT" TableLayout="Fixed" CellClickActionDefault="RowSelect"
													NoDataMessage="해당 데이터가 없습니다.">
													<AddNewBox>
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														</Style>
													</AddNewBox>
													<Pager>
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														</Style>
													</Pager>
													<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray" Height="25px">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</HeaderStyleDefault>
													<RowSelectorStyleDefault Cursor="Hand" BackColor="Gold"></RowSelectorStyleDefault>
													<FrameStyle Width="100%" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DimGray"
														BorderStyle="Solid" Height="230px"></FrameStyle>
													<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</FooterStyleDefault>
													<ClientSideEvents AfterSelectChangeHandler="uwgBO_HT_AfterSelectChangeHandler" DblClickHandler="uwgBO_HT_DblClickHandler"></ClientSideEvents>
													<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
													<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
													<RowAlternateStyleDefault BackColor="LightSteelBlue"></RowAlternateStyleDefault>
													<RowStyleDefault Cursor="Hand" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="Gray"
														BorderStyle="Solid" BackColor="#EBEFF6">
														<Padding Left="3px"></Padding>
														<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
													</RowStyleDefault>
												</DisplayLayout>
												<Bands>
													<igtbl:UltraGridBand>
														<Columns>
															<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="120px" BaseColumnName="ItemNum">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" BaseColumnName="ItemDrawNum">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" BaseColumnName="ItemName">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" BaseColumnName="CompanyName">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" BaseColumnName="BusinessRegistrationNum">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="시작공정코드" Key="BeginProcessCode" Hidden="True" BaseColumnName="BeginProcessCode">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="시작공정" Key="BeginProcess" BaseColumnName="BeginProcess">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="종료공정코드" Key="EndProcessCode" Hidden="True" BaseColumnName="EndProcessCode">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="종료공정" Key="EndProcess" BaseColumnName="EndProcess">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="발주비율" Key="OrderRate" Format="###,###,##0.00" BaseColumnName="OrderRate">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="1차납품요구량" Key="FirstDeliveryDemandQuantity" Format="###,###,##0.00" BaseColumnName="FirstDeliveryDemandQuantity">
																<CellStyle HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="1차납품요구일" Key="FirstDeliveryDemandDate" Format="yyyy-MM-dd" BaseColumnName="FirstDeliveryDemandDate">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="2차납품요구량" Key="SecondDeliveryDemandQuantity" Format="###,###,##0.00"
																BaseColumnName="SecondDeliveryDemandQuantity">
																<CellStyle HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="2차납품요구일" Key="SecondDeliveryDemandDate" Format="yyyy-MM-dd" BaseColumnName="SecondDeliveryDemandDate">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="3차납품요구량" Key="ThirdDeliveryDemandQuantity" Format="###,###,##0.00" BaseColumnName="ThirdDeliveryDemandQuantity">
																<CellStyle HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="3차납품요구일" Key="ThirdDeliveryDemandDate" Format="yyyy-MM-dd" BaseColumnName="ThirdDeliveryDemandDate">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="4차납품요구량" Key="FourthDeliveryDemandQuantity" Format="###,###,##0.00"
																BaseColumnName="FourthDeliveryDemandQuantity">
																<CellStyle HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="4차납품요구일" Key="FourthDeliveryDemandDate" Format="yyyy-MM-dd" BaseColumnName="FourthDeliveryDemandDate">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="5차납품요구량" Key="FifthDeliveryDemandQuantity" Format="###,###,##0.00" BaseColumnName="FifthDeliveryDemandQuantity">
																<CellStyle HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="5차납품요구일" Key="FifthDeliveryDemandDate" Format="yyyy-MM-dd" BaseColumnName="FifthDeliveryDemandDate">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="총발주량" Key="OrderQuantity" Format="###,###,##0.00" BaseColumnName="OrderQuantity">
																<CellStyle HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Format="###,###,##0.00" BaseColumnName="ApplyUnitCost">
																<CellStyle HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="총금액" Key="TotalCost" Format="###,###,##0.00" BaseColumnName="TotalCost">
																<CellStyle HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="잔량" Key="RemainQuantity" Format="###,###,##0.00" BaseColumnName="RemainQuantity">
																<CellStyle HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="볼륨번호" Key="VolumNum" BaseColumnName="VolumNum">
																<CellStyle HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" BaseColumnName="ProgressCondition">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" BaseColumnName="RegistrationPerson">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate">
																<CellStyle HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="구매의뢰원장번호" Key="BuyingRequestHistoryIndex" Hidden="True" BaseColumnName="BuyingRequestHistoryIndex">
																<CellStyle HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="구매발주원장번호" Key="BuyingOrderHistoryIndex" Hidden="True" BaseColumnName="BuyingOrderHistoryIndex">
																<CellStyle HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="외주의뢰원장번호" Key="OutSideOrderRequestHistoryIndex" Hidden="True" BaseColumnName="OutSideOrderRequestHistoryIndex">
																<CellStyle HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="외주발주원장번호" Key="OutSideOrderHistoryIndex" Hidden="True" BaseColumnName="OutSideOrderHistoryIndex">
																<CellStyle HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
														</Columns>
													</igtbl:UltraGridBand>
												</Bands>
											</igtbl:ultrawebgrid></td>
									</tr>
								</TABLE>
							</FIELDSET>
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 20px; HEIGHT: 44px" vAlign="top" align="left"><FONT face="굴림"></FONT></TD>
						<TD style="HEIGHT: 44px" vAlign="top" align="right"><FONT face="굴림"></FONT></TD>
						<TD style="HEIGHT: 44px" vAlign="middle" align="right"><FONT face="굴림">
								<TABLE id="Table3" style="HEIGHT: 42px" cellSpacing="0" cellPadding="0" width="100%">
									<TR>
										<TD align="right" height="8"></TD>
										<TD style="WIDTH: 149px" height="8"></TD>
										<TD align="right" height="8"></TD>
										<TD height="8"></TD>
										<TD align="right" height="8"></TD>
										<TD style="WIDTH: 12px" height="8"></TD>
										<TD style="WIDTH: 96px" align="right" height="8"></TD>
										<TD style="WIDTH: 134px" align="left" height="8"></TD>
									</TR>
									<TR>
										<TD align="right" style="HEIGHT: 23px">품목번호&nbsp;</TD>
										<TD style="WIDTH: 149px; HEIGHT: 23px"><asp:textbox id="txtItemNum" runat="server" Width="120px" ReadOnly="True" BorderStyle="Solid"
												BackColor="#EEEEE9" Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
										<TD align="right" style="HEIGHT: 23px">도면번호&nbsp;</TD>
										<TD style="HEIGHT: 23px"><asp:textbox id="txtItemDrawNum" runat="server" Width="120px" ReadOnly="True" BorderStyle="Solid"
												BackColor="#EEEEE9" Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
										<TD title="품목명:" align="right" style="HEIGHT: 23px">품목명&nbsp;</TD>
										<TD style="WIDTH: 12px; HEIGHT: 23px"><asp:textbox id="txtItemName" runat="server" Width="120px" ReadOnly="True" BorderStyle="Solid"
												BackColor="#EEEEE9" Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
										<TD style="WIDTH: 96px; HEIGHT: 23px" align="right">납품의뢰수량&nbsp;</TD>
										<TD style="WIDTH: 134px; HEIGHT: 23px" align="left"><asp:textbox id="txtDeliveryQuantity" runat="server" Width="120px" BorderStyle="Solid" BackColor="#EEEEE9"
												Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
									</TR>
									<TR>
										<TD title="발주수량:" style="HEIGHT: 4px" align="right">발주수량&nbsp;</TD>
										<TD style="WIDTH: 162px; HEIGHT: 4px"><asp:textbox id="txtOrderQuantity" runat="server" Width="120px" ReadOnly="True" BorderStyle="Solid"
												BackColor="#EEEEE9" Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
										<TD style="HEIGHT: 4px" align="right">납입수량&nbsp;</TD>
										<TD style="HEIGHT: 4px"><asp:textbox id="txtCashQuantity" runat="server" Width="120px" ReadOnly="True" BorderStyle="Solid"
												BackColor="#EEEEE9" Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
										<TD style="HEIGHT: 4px" align="right">잔량&nbsp;</TD>
										<TD style="WIDTH: 12px; HEIGHT: 4px"><asp:textbox id="txtRemainQuantity" runat="server" Width="120px" ReadOnly="True" BorderStyle="Solid"
												BackColor="#EEEEE9" Font-Size="9pt" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
										<TD style="WIDTH: 96px; HEIGHT: 4px" align="right"><INPUT id="hdRowIndex" style="WIDTH: 32px; HEIGHT: 21px" type="hidden" size="1" name="Hidden1"
												runat="server"><INPUT id="hddivision" style="WIDTH: 32px; HEIGHT: 21px" type="hidden" size="1" name="Hidden1"
												runat="server"></TD>
										<TD style="WIDTH: 134px; HEIGHT: 4px" align="right"><asp:button id="btnRegister" runat="server" Width="65px" Height="20px" Text="등   록" Font-Size="9pt"></asp:button>&nbsp;</TD>
									</TR>
								</TABLE>
							</FONT>
						</TD>
					</TR>
					<TR>
						<TD style="WIDTH: 20px; HEIGHT: 139px" vAlign="top" align="left"><FONT face="굴림"></FONT></TD>
						<TD style="HEIGHT: 139px" vAlign="top" align="right"><FONT face="굴림"></FONT></TD>
						<TD style="HEIGHT: 139px" vAlign="top" align="left"><FONT face="굴림">
								<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid; HEIGHT: 176px"
									align="top"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 납품의뢰현황 ]
									</LEGEND>
									<TABLE id="Table2" style="HEIGHT: 184px" width="100%" cellSpacing="0" cellPadding="0">
										<TBODY>
											<TR>
												<TD style="HEIGHT: 1px" vAlign="top" align="right"><FONT face="굴림"></FONT></TD>
											</TR>
											<TR>
												<TD vAlign="top" align="center">
							</FONT>
							<igtbl:ultrawebgrid id="uwgDR_HT" runat="server" Width="100%" Height="185px">
								<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
									RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
									HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
									RowSelectorsDefault="No" Name="uwgDRxHT" TableLayout="Fixed" CellClickActionDefault="RowSelect">
									<AddNewBox>
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										</Style>
									</AddNewBox>
									<Pager>
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										</Style>
									</Pager>
									<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray" Height="25px">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</HeaderStyleDefault>
									<FrameStyle Width="100%" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DimGray"
										BorderStyle="Solid" Height="185px"></FrameStyle>
									<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</FooterStyleDefault>
									<ClientSideEvents ColumnHeaderClickHandler="ColumnHeaderClickHandler"></ClientSideEvents>
									<SelectedHeaderStyleDefault Cursor="Hand"></SelectedHeaderStyleDefault>
									<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
									<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
									<RowAlternateStyleDefault BackColor="LightSteelBlue"></RowAlternateStyleDefault>
									<RowStyleDefault Cursor="Hand" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="Gray"
										BorderStyle="Solid" BackColor="#EBEFF6">
										<Padding Left="3px"></Padding>
										<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
									</RowStyleDefault>
								</DisplayLayout>
								<Bands>
									<igtbl:UltraGridBand>
										<Columns>
											<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="120px" BaseColumnName="ItemNum">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" BaseColumnName="ItemDrawNum">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" BaseColumnName="ItemName">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="공정순서번호" Key="ProcessSequenceNum" BaseColumnName="ProcessSequenceNum">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="공정코드" Key="ProcessCode" BaseColumnName="ProcessCode">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="공정명" Key="ProcessName" BaseColumnName="ProcessName">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" BaseColumnName="CompanyName">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" BaseColumnName="BusinessRegistrationNum">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="납품의뢰량" Key="DeliveryRequestQuantity" Format="###,###,##0.00" BaseColumnName="DeliveryRequestQuantity">
												<CellStyle HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="단가" Key="ApplyUnitCost" Format="###,###,##0.00" BaseColumnName="ApplyUnitCost">
												<CellStyle HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="총금액" Key="TotalCost" Format="###,###,##0.00" BaseColumnName="TotalCost">
												<CellStyle HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" BaseColumnName="ProgressCondition">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" BaseColumnName="RegistrationPerson">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" BaseColumnName="UpdatingPerson">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
												<CellStyle HorizontalAlign="Center"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="구매발주원장번호" Key="BuyingOrderHistoryIndex" Hidden="True" BaseColumnName="BuyingOrderHistoryIndex">
												<CellStyle HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="구매납품의뢰원장번호" Key="BuyingDeliveryRequestHistoryIndex" Hidden="True" BaseColumnName="BuyingDeliveryRequestHistoryIndex">
												<CellStyle HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="외주발주원장번호" Key="OutSideOrderHistoryIndex" Hidden="True" BaseColumnName="OutSideOrderHistoryIndex">
												<CellStyle HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="외주납품의뢰원장번호" Key="OutSideDeliveryRequestHistoryIndex" Hidden="True" BaseColumnName="OutSideDeliveryRequestHistoryIndex">
												<CellStyle HorizontalAlign="Right"></CellStyle>
											</igtbl:UltraGridColumn>
										</Columns>
									</igtbl:UltraGridBand>
								</Bands>
							</igtbl:ultrawebgrid></TD>
					</TR>
				</TBODY>
			</TABLE>
			</FIELDSET></FONT></TD></TR>
			<TR>
				<TD></TD>
				<TD vAlign="top" align="right"><FONT face="굴림"></FONT></TD>
				<TD vAlign="top" align="right">
					<TABLE width="100%">
						<TR>
							<TD style="WIDTH: 16px" height="8"></TD>
							<TD style="WIDTH: 11px" height="8"></TD>
							<TD height="8"></TD>
							<TD align="right" height="8"></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 16px"><asp:button id="btnExcel_BO_HT" runat="server" Font-Size="9pt" Width="65px" Height="20px" Text="Excel수주"></asp:button></TD>
							<TD style="WIDTH: 11px"><asp:button id="btnXML_OO_HT" runat="server" Font-Size="9pt" Width="65px" Height="20px" Enabled="False"
									Text="XML수주"></asp:button></TD>
							<TD><asp:button id="btnAutoReceive_OO_HT" runat="server" Font-Size="9pt" Width="65px" Height="20px"
									Enabled="False" Text="자동수주"></asp:button>
								<igtblexp:ultrawebgridexcelexporter id="uwgExcel_BO_HT" runat="server"></igtblexp:ultrawebgridexcelexporter></TD>
							<TD align="right"></TD>
						</TR>
					</TABLE>
					<FONT face="굴림"></FONT>
				</TD>
			</TR>
			</TBODY></TABLE></form>
	</body>
</HTML>
