<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="CompanySearchControl" Src="../Common/CompanySearchControl/CompanySearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Page language="c#" Codebehind="OutSideDelivery.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BuyingOutside.OutSideDelivery" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../MessageWindows.js"></script>
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--
		function ResettxtBox()
		{
			ResetTextBox();
			ResetBox();
			var objChooser1 = igdrp_getComboById("wdcStartDate");
			var objChooser2 = igdrp_getComboById("wdcEndDate");
			objChooser1.setValue(null);
			objChooser2.setValue(null);
			document.Form1.ddlItemClassification1.options[0].selected=true;
		}
		
		
		function Lot(inputValue)
        {
			document.Form1.hdLotNum.value = inputValue;
			document.Form1.Textbox1.value = inputValue;
			
		}
		function open()
		{
			window.open("../LotNumberManagement.aspx","","width=760,height=400,left=300,top=400, center=yes, resizable= yes");
		}
		
		function UltraWebGrid2_AfterSelectChangeHandler(gridName, id){
			rowindex(id);
		}
		
		//Grid에서 선택된 RowIndex를 hidden필드에 넣는 함수
		function rowindex(id)
		{
			var str = id;
			var num = new Array(3);
			var temp = '_';
			
			num = id.split('_');
			if(num.length > 2)
			{				
				document.Form1.HistoryIndex.value = "1";	
												
			}
			else
			{
				document.Form1.HistoryIndex.value = "0";
			}
			//document.Form1.rowindex.value = num[1];
			
			
		}
		function uwgOD_HT_DblClickHandler(gridName, cellId){
			
			// 초기화
			document.Form1.txtItemNum.value = "";
			//document.Form1.txtItemDrawNum.value = "";
			document.Form1.txtItemName.value = "";
			document.Form1.txtDeliveryQuantity.value = "0";
			document.Form1.txtCost.value = "0";			
			document.Form1.txtOrderQuantity.value = "";
			document.Form1.txtCashQuantity.value = "";
			document.Form1.txtRemainQuantity.value = "";
			Form1.btnRegistration.focus();
			var row = igtbl_getRowById(cellId);//선택한 로우의 인덱스
			
			
			if(document.Form1.HistoryIndex.value == 1)
			{
				//그리드에서 자식 그리드의 내용을 선택했을때 항목 값채우기
				document.Form1.txtItemNum.value = row.getCellFromKey("ItemNum").getValue();//품목번호
				//document.Form1.txtItemDrawNum.value = row.getCellFromKey("ItemDrawNum").getValue();//도면번호
				document.Form1.txtItemName.value = row.getCellFromKey("ItemName").getValue();//품목명
				document.Form1.txtDeliveryQuantity.value = row.getCellFromKey("DeliveryRequestQuantity").getValue();//납품의뢰수량
				document.Form1.txtCost.value = row.getCellFromKey("ApplyUnitCost").getValue();//단가
				document.Form1.txtOrderQuantity.value = "";
				document.Form1.txtCashQuantity.value = "";
				document.Form1.txtRemainQuantity.value = "";
				document.Form1.UnitCost.value = row.getCellFromKey("UnitCost").getValue();//표준단가				
				document.Form1.HistoryIndex.value = "1";//원장도면 (납품의뢰원장을 선택한 것을 표시함)
				document.Form1.IndexNum.value = row.getCellFromKey("OutSideDeliveryRequestHistoryIndex").getValue();//외주납품의뢰원장번호				
			}
			else
			{
				//그리드에서 부모내용을 선택했을때 항목 값채우기
				document.Form1.txtItemNum.value = row.getCellFromKey("ItemNum").getValue();//품목번호
				//document.Form1.txtItemDrawNum.value = row.getCellFromKey("ItemDrawNum").getValue();//도면번호
				document.Form1.txtItemName.value = row.getCellFromKey("ItemName").getValue();//품목명
				document.Form1.txtDeliveryQuantity.value = row.getCellFromKey("RemainQuantity").getValue();//납품수량
				document.Form1.txtOrderQuantity.value = row.getCellFromKey("OrderQuantity").getValue();//발주수량
				document.Form1.txtCashQuantity.value = Number(row.getCellFromKey("OrderQuantity").getValue()) - Number(row.getCellFromKey("RemainQuantity").getValue());//납입수량
				document.Form1.txtRemainQuantity.value = row.getCellFromKey("RemainQuantity").getValue();//잔량
				document.Form1.txtCost.value = row.getCellFromKey("ApplyUnitCost").getValue();//단가
				document.Form1.UnitCost.value = row.getCellFromKey("UnitCost").getValue();//표준단가
				//document.Form1.UnitCost.value = row.getCellFromKey("UnitCost").getValue();//표준단가
				document.Form1.HistoryIndex.value = "0";//원장도면 (발주원장을 선택한 것을 표시함)
				document.Form1.IndexNum.value = row.getCellFromKey("OutSideOrderHistoryIndex").getValue();//외주발주원장번호
			}
	
		}
		
		function Process()
		{
			var quantity = document.Form1.txtRemainQuantity.value - document.Form1.txtDeliveryQuantity.value;
			//발주비율은 0%가 될수없으므로 0%가 된다면 메세지창을 띄워주고 기존의 값들로 다시 채운다.
			if(quantity < 0)
			{
				if(confirm("입고수량이 잔량보다 많습니다! 그래도입고하시겠습니까?")) 
				{
				
						return true;
				}
				else
				{
					Form1.txtDeliveryQuantity.value = 0;	
					Form1.txtDeliveryQuantity.focus();		
						return false;
				}
			}
		
		}

--></SCRIPT>
	</HEAD>
	<body bottomMargin="0" bgColor="#f7f6f6" leftMargin="0" topMargin="0" rightMargin="0"
		MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; POSITION: absolute; WIDTH: 800px; HEIGHT: 550px; TOP: 10px; LEFT: 10px"
				cellSpacing="0" cellPadding="0" width="300" border="0">
				<TR>
					<TD style="HEIGHT: 84px" width="20" height="84"><FONT face="굴림"></FONT></TD>
					<TD style="HEIGHT: 84px" height="84">
						<FIELDSET style="BORDER-BOTTOM: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; HEIGHT: 60px; BORDER-TOP: dimgray 2px solid; BORDER-RIGHT: dimgray 2px solid"
							align="top"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색 ]
							</LEGEND>
							<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="800" align="center" border="0">
								<TR height="35">
									<TD align="left" width="600" colSpan="8" height="30">&nbsp;
										<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" border="0">
											<TR>
												<TD align="left" width="200" height="30">
													<uc1:companysearchcontrol id="CSC1" runat="server"></uc1:companysearchcontrol></TD>
												<TD align="left" width="600" height="30">
													<uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol>
													<INPUT id="hdLotNum" style="WIDTH: 67px; HEIGHT: 21px" type="hidden" size="5" name="hdLotNum"
														runat="server"></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="30">납기요구일&nbsp;</TD>
									<TD align="left" width="100" height="30">
										<igsch:webdatechooser id="wdcStartDate" runat="server" Width="100px" BorderStyle="Solid" BackColor="#EEEEE9"
											Height="20px" BorderColor="DimGray" Text=" " NullDateLabel=" ">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderWidth="1px" BorderStyle="Solid"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<TD align="center" width="5" height="30">~</TD>
									<TD width="100" height="30">
										<igsch:webdatechooser id="wdcEndDate" runat="server" Width="100px" BorderStyle="Solid" BackColor="#EEEEE9"
											Height="20px" BorderColor="DimGray" Text=" " NullDateLabel=" ">
											<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
												ShowTitle="False" ShowFooter="False">
												<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
												<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
												<DropDownStyle BackColor="White"></DropDownStyle>
												<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
											</CalendarLayout>
											<DropDownStyle BorderWidth="1px" BorderStyle="Solid"></DropDownStyle>
											<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igsch:webdatechooser></TD>
									<td align="right" width="70">제품팀&nbsp;</td>
									<td align="left" width="100"><asp:dropdownlist id="ddlItemClassification1" runat="server" Width="115px" BackColor="#EEEEE9"></asp:dropdownlist></td>
									<TD align="right" width="355" colSpan="2" height="30"><INPUT id="btnReset" style="WIDTH: 65px; HEIGHT: 20px" onclick="javascript:ResettxtBox()"
											type="button" value="초기화" name="btnReset">&nbsp;
										<asp:button id="btnSearch" runat="server" Font-Size="9pt" Height="20px" Width="60px" Text="검   색"
											CommandName="Search"></asp:button>&nbsp;</TD>
								</TR>
								<TR>
									<TD align="right" width="70" colSpan="8" height="8"></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="20" height="380"></TD>
					<TD vAlign="top" height="380">
						<FIELDSET style="BORDER-BOTTOM: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; HEIGHT: 380px; BORDER-TOP: dimgray 2px solid; BORDER-RIGHT: dimgray 2px solid"
							align="top"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색결과 ]
							</LEGEND>
							<TABLE id="Table3" style="WIDTH: 100%; HEIGHT: 380px" cellSpacing="0" cellPadding="0">
								<TR>
									<TD style="WIDTH: 880px" vAlign="top" align="center">
										<igtbl:ultrawebgrid id="uwgOD_HT" runat="server" Width="100%" Height="386px">
											<DisplayLayout AutoGenerateColumns="False" AllowSortingDefault="Yes" RowHeightDefault="20px" Version="3.00"
												ViewType="Hierarchical" SelectTypeRowDefault="Extended" ScrollBarView="Horizontal" ScrollBar="Always"
												AllowColumnMovingDefault="OnServer" SelectTypeCellDefault="Single" HeaderClickActionDefault="SortMulti"
												BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="uwgODxHT"
												CellClickActionDefault="RowSelect">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</AddNewBox>
												<Pager PageSize="16" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

													</Style>
												</Pager>
												<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray" Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<FrameStyle Width="100%" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="386px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents AfterSelectChangeHandler="UltraWebGrid2_AfterSelectChangeHandler" DblClickHandler="uwgOD_HT_DblClickHandler"></ClientSideEvents>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="Gray"
													BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" BaseColumnName="ItemNum" NullText="">
															<CellStyle HorizontalAlign="Left">
																<Padding Left="2px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" BaseColumnName="ItemDrawNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" BaseColumnName="ItemName">
															<CellStyle HorizontalAlign="Left">
																<Padding Left="2px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단위" Key="Unit" Width="35px" BaseColumnName="Unit">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="단가구분" Key="UnitCostDistinction" Hidden="True" BaseColumnName="UnitCostDistinction">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="시작공정코드" Key="BeginProcessCode" Hidden="True" BaseColumnName="BeginProcessCode">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="종료공정코드" Key="EndProcessCode" Hidden="True" BaseColumnName="EndProcessCode">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="거래처" Key="CompanyName" BaseColumnName="CompanyName">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" BaseColumnName="BusinessRegistrationNum">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="1차납품요구량" Key="FirstDeliveryDemandQuantity" Hidden="True" Format="###,###,##0.00"
															BaseColumnName="FirstDeliveryDemandQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총발주량" Key="OrderQuantity" Width="70px" Format="###,###,##0.00" BaseColumnName="OrderQuantity">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="잔량" Key="RemainQuantity" Width="70px" Format="###,###,##0.00" BaseColumnName="RemainQuantity">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="납기요구일" Key="FirstDeliveryDemandDate" Format="yyyy-MM-dd" BaseColumnName="FirstDeliveryDemandDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="2차납품요구량" Key="SecondDeliveryDemandQuantity" Hidden="True" Format="###,###,##0.00"
															BaseColumnName="SecondDeliveryDemandQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="2차납품요구일" Key="SecondDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
															BaseColumnName="SecondDeliveryDemandDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="3차납품요구량" Key="ThirdDeliveryDemandQuantity" Hidden="True" Format="###,###,##0.00"
															BaseColumnName="ThirdDeliveryDemandQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="3차납품요구일" Key="ThirdDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
															BaseColumnName="ThirdDeliveryDemandDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="4차납품요구량" Key="FourthDeliveryDemandQuantity" Hidden="True" Format="###,###,##0.00"
															BaseColumnName="FourthDeliveryDemandQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="4차납품요구일" Key="FourthDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
															BaseColumnName="FourthDeliveryDemandDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="5차납품요구량" Key="FifthDeliveryDemandQuantity" Hidden="True" Format="###,###,##0.00"
															BaseColumnName="FifthDeliveryDemandQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="5차납품요구일" Key="FifthDeliveryDemandDate" Hidden="True" Format="yyyy-MM-dd"
															BaseColumnName="FifthDeliveryDemandDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="표준단가" Key="UnitCost" Width="60px" Format="###,###,##0.00" BaseColumnName="UnitCost"
															NullText="">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="발주 단가" Key="ApplyUnitCost" Width="60px" Format="###,###,##0.00" BaseColumnName="ApplyUnitCost"
															NullText="">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="총금액" Key="TotalCost" Width="80px" Format="###,###,##0.00" BaseColumnName="TotalCost">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="시작공정" Key="BeginProcess" Width="80px" BaseColumnName="BeginProcess">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="종료공정" Key="EndProcess" Width="80px" BaseColumnName="EndProcess">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="비율" Key="OrderRate" Width="50px" Format="###,###,##0.00" BaseColumnName="OrderRate">
															<CellStyle HorizontalAlign="Right">
																<Padding Right="4px"></Padding>
															</CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="볼륨번호" Key="VolumNum" Hidden="True" BaseColumnName="VolumNum">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" Hidden="True" BaseColumnName="ProgressCondition">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" Width="80px" BaseColumnName="RegistrationPerson">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Width="80px" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" Width="80px" BaseColumnName="UpdatingPerson">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Width="80px" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
															<CellStyle HorizontalAlign="Center"></CellStyle>
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
										</igtbl:ultrawebgrid></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="20"></TD>
					<TD>
						<TABLE id="Table4" style="WIDTH:800px; HEIGHT:46px" cellSpacing="0" cellPadding="0">
							<TR>
								<TD align="right" height="8" width="60"></TD>
								<TD height="8" width="100"></TD>
								<TD align="right" height="8" width="60"></TD>
								<TD height="8" width="100"></TD>
								<TD align="right" height="8" width="60"></TD>
								<TD height="8" width="100"></TD>
								<TD align="right" height="8" width="60"></TD>
								<TD align="left" height="8" width="100"></TD>
								<TD align="right" height="8" width="160"><INPUT id="Hidden1" style="WIDTH: 67px; HEIGHT: 21px" type="hidden" size="5" runat="server"
										NAME="Hidden1"><INPUT id="IndexNum" style="WIDTH: 15px; HEIGHT: 21px" type="hidden" size="1" runat="server"
										NAME="IndexNum"><INPUT id="HistoryIndex" style="WIDTH: 15px; HEIGHT: 21px" type="hidden" size="1" runat="server"
										NAME="HistoryIndex"><INPUT id="UnitCost" style="WIDTH: 15px; HEIGHT: 21px" type="hidden" size="1" name="Hidden1"
										runat="server"></TD>
							</TR>
							<TR>
								<TD align="right" width="60">품번&nbsp;</TD>
								<TD width="100"><asp:textbox id="txtItemNum" runat="server" Width="100px" Enabled="False" BorderStyle="Solid"
										BackColor="#EEEEE9" Height="20px" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
								<TD align="right" width="60">품명&nbsp;</TD>
								<TD width="100"><asp:textbox id="txtItemName" runat="server" Width="100px" Enabled="False" BorderStyle="Solid"
										BackColor="#EEEEE9" Height="20px" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
								<TD title="품목명:" align="right" width="60">입고량&nbsp;</TD>
								<TD width="100"><asp:textbox id="txtDeliveryQuantity" runat="server" style="TEXT-ALIGN: right" Width="100px"
										BorderStyle="Solid" BackColor="#EEEEE9" Height="20px" BorderColor="DimGray" BorderWidth="1px">0</asp:textbox></TD>
								<TD align="right" width="60">단가 &nbsp;</TD>
								<TD align="left" width="100">
									<asp:textbox id="txtCost" style="TEXT-ALIGN: right" runat="server" BorderColor="DimGray" Height="20px"
										BackColor="#EEEEE9" BorderWidth="1px" BorderStyle="Solid" Width="100px">0</asp:textbox></TD>
								<TD align="right" width="160">년도
									<asp:dropdownlist id="ddlYear" runat="server" Width="55px" BackColor="#EEEEE9">
										<asp:ListItem Value="2020">2020</asp:ListItem>
												<asp:ListItem Value="2021">2021</asp:ListItem>
												<asp:ListItem Value="2022">2022</asp:ListItem>
												<asp:ListItem Value="2023>2023</asp:ListItem>
												<asp:ListItem Value="2024">2024</asp:ListItem>
												<asp:ListItem Value="2025">2025</asp:ListItem>
												<asp:ListItem Value="2026">2026</asp:ListItem>
												<asp:ListItem Value="2027">2027</asp:ListItem>
												<asp:ListItem Value="2028">2028</asp:ListItem>
												<asp:ListItem Value="2029">2029</asp:ListItem>
												<asp:ListItem Value="2030">2030</asp:ListItem>
									</asp:dropdownlist>월
									<asp:dropdownlist id="ddlMon" runat="server" Width="40px" BackColor="#EEEEE9">
										<asp:ListItem Value="1">1</asp:ListItem>
										<asp:ListItem Value="2">2</asp:ListItem>
										<asp:ListItem Value="3">3</asp:ListItem>
										<asp:ListItem Value="4">4</asp:ListItem>
										<asp:ListItem Value="5">5</asp:ListItem>
										<asp:ListItem Value="6">6</asp:ListItem>
										<asp:ListItem Value="7">7</asp:ListItem>
										<asp:ListItem Value="8">8</asp:ListItem>
										<asp:ListItem Value="9">9</asp:ListItem>
										<asp:ListItem Value="10">10</asp:ListItem>
										<asp:ListItem Value="11">11</asp:ListItem>
										<asp:ListItem Value="12">12</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD title="발주수량:" style="HEIGHT: 24px" align="right" width="60">발주량&nbsp;</TD>
								<TD style="HEIGHT: 24px" width="100"><asp:textbox id="txtOrderQuantity" style="TEXT-ALIGN: right" runat="server" Width="100px" Enabled="False"
										BorderStyle="Solid" BackColor="#EEEEE9" Height="20px" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
								<TD style="HEIGHT: 24px" align="right" width="60">납입량&nbsp;</TD>
								<TD style="HEIGHT: 24px" width="100"><asp:textbox id="txtCashQuantity" style="TEXT-ALIGN: right" runat="server" Width="100px" Enabled="False"
										BorderStyle="Solid" BackColor="#EEEEE9" Height="20px" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
								<TD style="HEIGHT: 24px" align="right" width="60">잔량&nbsp;</TD>
								<TD style="HEIGHT: 24px" width="100"><asp:textbox id="txtRemainQuantity" style="TEXT-ALIGN: right" runat="server" Width="100px" Enabled="False"
										BorderStyle="Solid" BackColor="#EEEEE9" Height="20px" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
								<TD style="HEIGHT: 24px" align="right" width="60">
									입고일&nbsp;</TD>
								<TD style="HEIGHT: 24px" align="left" width="100"><igsch:webdatechooser id="wdcDeliveryDate" runat="server" NullDateLabel=" " Width="100px" BackColor="#EEEEE9"
										Height="20px">
										<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
											ShowTitle="False" ShowFooter="False">
											<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
											<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
											<DropDownStyle BackColor="White"></DropDownStyle>
											<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
										</CalendarLayout>
										<DropDownStyle BorderStyle="Inset"></DropDownStyle>
										<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
										<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
									</igsch:webdatechooser></TD>
								<TD align="right" width="160"><asp:button id="btnRegistration" runat="server" Width="65px" Height="20px" Text="등   록" Font-Size="9pt"></asp:button>&nbsp;</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			&nbsp;&nbsp;
		</form>
	</body>
</HTML>
