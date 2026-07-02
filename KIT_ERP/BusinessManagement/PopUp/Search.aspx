<%@ Page language="c#" Codebehind="Search.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BusinessManagement.PopUp.Search" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Search</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--
		function UltraWebGrid1_AfterSelectChangeHandler(gridName, id){
			//document.Search.lb_RowSelectIndex.value = document.Search.lb_RowIndex.value;
			var objGrid = igtbl_getActiveRow(gridName)//현재 선택되어진 객체의 행의 가져옴
			var row = igtbl_getRowById(id);
			document.Search.lb_RowSelectIndex.value = row.getCellFromKey("ReceivingOrderHistoryIndex").getValue();
			
			window.opener.SubmitLinkButton(document.all['<%= lb_RowSelectIndex.ClientID %>'].value);self.close();	
			
		}
		
		function UltraWebGrid1_MouseOverHandler(gridName, id, button){
			//Add code to handle your event here.
			if(button == 0) { // Are we over a cell
            var cell = igtbl_getElementById(id);
            cell.style.cursor = 'hand';
            //var label = igtbl_getElementById("lb_RowIndex");
            var parts = id.split("_");
            document.Search.lb_RowIndex.value = parts[1];
            }
		}
		
--></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Search" method="post" runat="server">
			<igtbl:ultrawebgrid id="UltraWebGrid1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px"
				runat="server" Height="450px" Width="800px">
				<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
					RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
					BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1"
					TableLayout="Fixed" CellClickActionDefault="RowSelect">
					<AddNewBox>
						<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

						</Style>
					</AddNewBox>
					<Pager PageSize="20">
						<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

						</Style>
					</Pager>
					<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
						<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
					</HeaderStyleDefault>
					<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
					<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
						BackColor="Silver" Height="450px"></FrameStyle>
					<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
						<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
					</FooterStyleDefault>
					<ClientSideEvents AfterSelectChangeHandler="UltraWebGrid1_AfterSelectChangeHandler" MouseOverHandler="UltraWebGrid1_MouseOverHandler"></ClientSideEvents>
					<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
					<SelectedRowStyleDefault Cursor="Hand" BackColor="#E0E5CD"></SelectedRowStyleDefault>
					<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
					<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EDEFF6">
						<Padding Left="3px"></Padding>
						<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
					</RowStyleDefault>
				</DisplayLayout>
				<Bands>
					<igtbl:UltraGridBand AllowUpdate="RowTemplateOnly">
						<Columns>
							<igtbl:UltraGridColumn HeaderText="발주번호" Key="OrderNum" HeaderClickAction="SortMulti" BaseColumnName="OrderNum">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemNum">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
								<HeaderStyle Height="25px"></HeaderStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Hidden="True" HeaderClickAction="SortMulti"
								BaseColumnName="ItemDrawNum">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="150px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" HeaderClickAction="SortMulti" BaseColumnName="CompanyName">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" HeaderClickAction="SortMulti"
								BaseColumnName="BusinessRegistrationNum">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
								<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="자산분류" Key="PropertyClassification" HeaderClickAction="SortMulti" BaseColumnName="PropertyClassification">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="수주일자" Key="ReceivingOrderDate" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
								BaseColumnName="ReceivingOrderDate">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="1차납기요구량" Key="DeliveryRequestQuantity1" Hidden="True" Format="###,###,###.##"
								HeaderClickAction="SortMulti" BaseColumnName="DeliveryRequestQuantity1">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="총수주량" Key="" Format="###,###,###.##" HeaderClickAction="SortMulti" BaseColumnName="">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
									<Padding Right="4px"></Padding>
								</CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="납기요구일" Key="DeliveryRequestDate1" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
								BaseColumnName="DeliveryRequestDate1">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
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
							<igtbl:UltraGridColumn HeaderText="단가" Key="" Format="\ ###,###,##0" HeaderClickAction="SortMulti" BaseColumnName="">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
									<Padding Right="4px"></Padding>
								</CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="총금액" Key="" Format="\ ###,###,##0" HeaderClickAction="SortMulti" BaseColumnName="">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
									<Padding Right="4px"></Padding>
								</CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="출고수량" Key="OutStorehouseQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
								BaseColumnName="OutStorehouseQuantity">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
									<Padding Right="4px"></Padding>
								</CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="적합수량" Key="SuitabilityQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
								BaseColumnName="SuitabilityQuantity">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
									<Padding Right="4px"></Padding>
								</CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="미검수량" Key="UnInspectionQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
								BaseColumnName="UnInspectionQuantity">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
									<Padding Right="4px"></Padding>
								</CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="납품잔량" Key="RemainderQuantity" Format="###,###,###.##" HeaderClickAction="SortMulti"
								BaseColumnName="RemainderQuantity">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Right">
									<Padding Right="4px"></Padding>
								</CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="납품장소" Key="DeliveryPlace" HeaderClickAction="SortMulti" BaseColumnName="DeliveryPlace">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" HeaderClickAction="SortMulti" BaseColumnName="ProgressCondition">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" Hidden="True" HeaderClickAction="SortMulti"
								BaseColumnName="RegistrationPerson">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
								<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Hidden="True" Format="yyyy-MM-dd" HeaderClickAction="SortMulti"
								BaseColumnName="RegistrationDate">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" Hidden="True" HeaderClickAction="SortMulti"
								BaseColumnName="UpdatingPerson">
								<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
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
			</igtbl:ultrawebgrid>
			<INPUT id="lb_RowIndex" style="Z-INDEX: 103; LEFT: 752px; WIDTH: 50px; POSITION: absolute; TOP: 456px; HEIGHT: 20px"
				type="hidden" runat="server"> <INPUT id="lb_RowSelectIndex" style="Z-INDEX: 102; LEFT: 848px; WIDTH: 50px; POSITION: absolute; TOP: 456px; HEIGHT: 20px"
				type="hidden" name="lb_RowSelectIndex" runat="server">
		</form>
	</body>
</HTML>
