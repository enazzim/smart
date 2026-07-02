<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="DeliveryRequestPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.CooperationCompany.DeliveryRequest" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK rel="stylesheet" type="text/css" href="http://localhost/KIT_ERP/StyleSheet2.css">
		<SCRIPT type="text/javascript"><!--

		//Row Template 창이 열리기 전에 진행상태가 대기가 아닌것은 수정을 할 수 없도록 경고 메세지를 띄운다.
		function uwgDR_PC_BeforeRowTemplateOpenHandler(gridName, rowId, templateId){
		
			var frm = document.Form1;
			var row = igtbl_getRowById(rowId);
			if(row.getCellFromKey("ProgressCondition").getValue() != "대기")
			{
				alert("진행상태가 완료인 항목은 수정할 수 없습니다.!");
				return true;
			}else if(row.getCellFromKey("OutSideOrderHistoryIndex").getValue() != null)
			{				
				frm.hddivision.value = "0";//외주납품의뢰을 선택한 것을 표시함				
			}
			
			else
			{										
				frm.hddivision.value = "1";//구매납품의뢰을 선택한 것을 표시함						
			}		
		}
		function uwgDR_PC_AfterRowTemplateOpenHandler(gridName, rowId){
			
			var frm = document.Form1;
			var row = igtbl_getRowById(rowId);
			
			///////////RowIndex Hidden 에 저장////////////////
			var num = rowId.split('_');
			frm.hdRowIndex.value = num[1];
			/////////////////////////////////////////////////		
		}
		
		
		
--></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; WIDTH: 800px; POSITION: absolute; TOP: 10px; HEIGHT: 550px"
				cellSpacing="0" cellPadding="0" align="center" border="0" width="800">
				<TR>
					<TD style="WIDTH: 20px" vAlign="top" align="left"><FONT face="굴림"></FONT></TD>
					<TD vAlign="top" align="right">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 811px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 544px"
							align="top"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 납품의뢰수정 ]
							</LEGEND>
							<table id="Table2" style="WIDTH: 808px; HEIGHT: 472px" width="808" cellSpacing="0" cellPadding="0">
								<tr>
									<td vAlign="middle" align="center"><FONT face="굴림">
											<igtbl:UltraWebGrid id="uwgDR_PC" runat="server" Height="525px" Width="100%">
												<DisplayLayout AutoGenerateColumns="False" AllowSortingDefault="Yes" RowHeightDefault="20px" Version="3.00"
													SelectTypeRowDefault="Extended" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti"
													BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="uwgDRxPC"
													CellClickActionDefault="RowSelect" AllowUpdateDefault="RowTemplateOnly">
													<AddNewBox>
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														</Style>
													</AddNewBox>
													<Pager>
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														</Style>
													</Pager>
													<HeaderStyleDefault BorderColor="DimGray" BorderStyle="Solid" BackColor="LightGray" Height="25px">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</HeaderStyleDefault>
													<FrameStyle Width="100%" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DimGray"
														BorderStyle="Solid" Height="525px"></FrameStyle>
													<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</FooterStyleDefault>
													<ClientSideEvents BeforeRowTemplateOpenHandler="uwgDR_PC_BeforeRowTemplateOpenHandler" AfterRowTemplateOpenHandler="uwgDR_PC_AfterRowTemplateOpenHandler"></ClientSideEvents>
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
															<igtbl:UltraGridColumn HeaderText="공정코드" Key="ProcessCode" Hidden="True" BaseColumnName="ProcessCode">
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
														<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
															<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
														</RowTemplateStyle>
														<RowEditTemplate>
															<P align="right">
																<TABLE id="Table2" style="HEIGHT: 61px" cellSpacing="0" cellPadding="0" width="800" border="0">
																	<TR>
																		<TD align="right"><FONT face="굴림">품목번호:</FONT></TD>
																		<TD><FONT face="굴림"> <INPUT id="txtItemNum" style="WIDTH: 100px" disabled readOnly type="text" columnKey="ItemNum"></FONT></TD>
																		<TD align="right"><FONT face="굴림">도면번호:</FONT></TD>
																		<TD><FONT face="굴림"> <INPUT id="txtItemDrawNum" style="WIDTH: 100px" disabled readOnly type="text" columnKey="ItemDrawNum"></FONT></TD>
																		<TD align="right"><FONT face="굴림">품목명:</FONT></TD>
																		<TD>
																			<INPUT id="txtItemName" style="WIDTH: 100px" disabled readOnly type="text" columnKey="ItemName"></TD>
																	</TR>
																	<TR>
																		<TD align="right"><FONT face="굴림">납품의뢰량:</FONT></TD>
																		<TD width="1">
																			<asp:TextBox id="txtDeliveryRequest" runat="server" Width="100px" columnKey="DeliveryRequestQuantity"></asp:TextBox></TD>
																		<TD></TD>
																		<TD><FONT face="굴림"></FONT></TD>
																		<TD><FONT face="굴림"></FONT></TD>
																		<TD align="right">
																			<INPUT id="igtbl_reOkBtn" style="WIDTH: 60px" onclick="igtbl_gRowEditButtonClick(event); __doPostBack('linkUpdate','');"
																				type="button" value="수   정">&nbsp; &nbsp;
																		</TD>
																	</TR>
																</TABLE>
																&nbsp;&nbsp;
															</P>
														</RowEditTemplate>
													</igtbl:UltraGridBand>
												</Bands>
											</igtbl:UltraWebGrid></FONT>
									</td>
								</tr>
							</table>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			<asp:LinkButton id="linkUpdate" style="Z-INDEX: 102; LEFT: 808px; POSITION: absolute; TOP: 600px"
				runat="server" Visible="False">linkUpdate</asp:LinkButton><INPUT id="hddivision" style="Z-INDEX: 103; LEFT: 768px; WIDTH: 32px; POSITION: absolute; TOP: 600px; HEIGHT: 21px"
				type="hidden" size="1" name="hddivision" runat="server"> <INPUT id="hdRowIndex" style="Z-INDEX: 104; LEFT: 704px; WIDTH: 48px; POSITION: absolute; TOP: 600px; HEIGHT: 21px"
				type="hidden" size="2" name="hdRowIndex" runat="server">
		</form>
	</body>
</HTML>
