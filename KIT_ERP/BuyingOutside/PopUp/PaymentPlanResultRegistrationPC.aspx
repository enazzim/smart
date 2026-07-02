<%@ Page language="c#" Codebehind="PaymentPlanResultRegistrationPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BuyingOutside.PaymentPlanResultPC" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>WebForm1</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" src="../MessageWindows.js"></script>
		<script language="javascript" src="../GridCheck.js"></script>
		<LINK href="/KIT_ERP/StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--
	
		function uwgPPR_HT_AfterRowTemplateCloseHandler(gridName, rowId, bSaveChanges){
		
			var frm = document.Form1;
			
			if(event.srcElement.id == "igtbl_reOkBtn")
			{
				var row = igtbl_getRowById(rowId);
				var combo1 = igdrp_getComboById("uwgPPRxHTxxctl0xwdcPaymentDate");
				var combo2 = igdrp_getComboById("uwgPPRxHTxxctl0xwdcBillPaymentDate1");
				var combo3 = igdrp_getComboById("uwgPPRxHTxxctl0xwdcBillPaymentDate2");
				var combo4 = igdrp_getComboById("uwgPPRxHTxxctl0xwdcBillPaymentDate3");
				
				document.Form1.hdYear.value = combo1.getValue().getFullYear();
				document.Form1.hdMon.value = combo1.getValue().getMonth()+1;
				
				
				row.getCellFromKey("PaymentDate").setValue(combo1.getValue());
				row.getCellFromKey("BillPaymentDate1").setValue(combo2.getValue());
				row.getCellFromKey("BillPaymentDate2").setValue(combo3.getValue());
				row.getCellFromKey("BillPaymentDate3").setValue(combo4.getValue());		
				
				//결재방법
				row.getCellFromKey("DecisionMethodCode").setValue(frm.uwgPPR_HT__ctl0_DDL_DecisionMethod.options[frm.uwgPPR_HT__ctl0_DDL_DecisionMethod.selectedIndex].value);
				row.getCellFromKey("DecisionMethod").setValue(frm.uwgPPR_HT__ctl0_DDL_DecisionMethod.options[frm.uwgPPR_HT__ctl0_DDL_DecisionMethod.selectedIndex].innerText);
				
				
				//결재은행1
				if(frm.uwgPPR_HT__ctl0_DDL_Bank1.options[frm.uwgPPR_HT__ctl0_DDL_Bank1.selectedIndex].value == "")
				{
					row.getCellFromKey("BankCode1").setValue(frm.uwgPPR_HT__ctl0_DDL_Bank1.options[0].value);
					row.getCellFromKey("BankName1").setValue(frm.uwgPPR_HT__ctl0_DDL_Bank1.options[0].value);
				}
				else
				{
					row.getCellFromKey("BankCode1").setValue(frm.uwgPPR_HT__ctl0_DDL_Bank1.options[frm.uwgPPR_HT__ctl0_DDL_Bank1.selectedIndex].value);
					row.getCellFromKey("BankName1").setValue(frm.uwgPPR_HT__ctl0_DDL_Bank1.options[frm.uwgPPR_HT__ctl0_DDL_Bank1.selectedIndex].innerText);
				}
				//결재은행2
				if(frm.uwgPPR_HT__ctl0_DDL_Bank2.options[frm.uwgPPR_HT__ctl0_DDL_Bank2.selectedIndex].value == "")
				{
					row.getCellFromKey("BankCode2").setValue(frm.uwgPPR_HT__ctl0_DDL_Bank2.options[0].value);
					row.getCellFromKey("BankName2").setValue(frm.uwgPPR_HT__ctl0_DDL_Bank2.options[0].value);
				}
				else
				{
					row.getCellFromKey("BankCode2").setValue(frm.uwgPPR_HT__ctl0_DDL_Bank2.options[frm.uwgPPR_HT__ctl0_DDL_Bank2.selectedIndex].value);
					row.getCellFromKey("BankName2").setValue(frm.uwgPPR_HT__ctl0_DDL_Bank2.options[frm.uwgPPR_HT__ctl0_DDL_Bank2.selectedIndex].innerText);
				}
				//결재은행3
				if(frm.uwgPPR_HT__ctl0_DDL_Bank3.options[frm.uwgPPR_HT__ctl0_DDL_Bank3.selectedIndex].value == "")
				{
					row.getCellFromKey("BankCode3").setValue(frm.uwgPPR_HT__ctl0_DDL_Bank3.options[0].value);
					row.getCellFromKey("BankName3").setValue(frm.uwgPPR_HT__ctl0_DDL_Bank3.options[0].value);
				}
				else
				{
					row.getCellFromKey("BankCode3").setValue(frm.uwgPPR_HT__ctl0_DDL_Bank3.options[frm.uwgPPR_HT__ctl0_DDL_Bank3.selectedIndex].value);
					row.getCellFromKey("BankName3").setValue(frm.uwgPPR_HT__ctl0_DDL_Bank3.options[frm.uwgPPR_HT__ctl0_DDL_Bank3.selectedIndex].innerText);
				}
			}		
		}
	
		
		function uwgPPR_HT_AfterRowTemplateOpenHandler(gridName, rowId){
					
			var frm = document.Form1;
			var row = igtbl_getRowById(rowId);
			
			///////////RowIndex Hidden 에 저장////////////////
			var num = rowId.split('_');
			document.Form1.hdRowIndex.value = num[1];
			/////////////////////////////////////////////////
			
									
			var combo1 = igdrp_getComboById("uwgPPRxHTxxctl0xwdcPaymentDate");
			var combo2 = igdrp_getComboById("uwgPPRxHTxxctl0xwdcBillPaymentDate1");
			var combo3 = igdrp_getComboById("uwgPPRxHTxxctl0xwdcBillPaymentDate2");
			var combo4 = igdrp_getComboById("uwgPPRxHTxxctl0xwdcBillPaymentDate3");
			
			
			combo1.setValue(row.getCellFromKey("PaymentDate").getValue());
			combo2.setValue(row.getCellFromKey("BillPaymentDate1").getValue());
			combo3.setValue(row.getCellFromKey("BillPaymentDate2").getValue());
			combo4.setValue(row.getCellFromKey("BillPaymentDate3").getValue());	
		
			frm.hd_ItemPaymentCost.value = row.getCellFromKey("ItemPaymentCost").getValue();// Hidden 필드에 수정전 물품액 저장
						
			//결재방법	
			for(var i=0;i<frm.uwgPPR_HT__ctl0_DDL_DecisionMethod.options.length;i++)
			{			
				if(frm.uwgPPR_HT__ctl0_DDL_DecisionMethod.options[i].innerText == document.getElementById(rowId).children[7].innerText)			
				{				
				frm.uwgPPR_HT__ctl0_DDL_DecisionMethod.options[i].selected=true;	
				break;
				}			
				else
				frm.uwgPPR_HT__ctl0_DDL_DecisionMethod.options[0].selected=true;				
			}
			
			//결재은행1		
			for(var j=0;j<frm.uwgPPR_HT__ctl0_DDL_Bank1.options.length;j++)	
			{				
				if(frm.uwgPPR_HT__ctl0_DDL_Bank1.options[j].innerText == document.getElementById(rowId).children[12].innerText)
				{				
					frm.uwgPPR_HT__ctl0_DDL_Bank1.options[j].selected=true;		
					break;		
				}			
				else					
					frm.uwgPPR_HT__ctl0_DDL_Bank1.options[0].selected=true;
			}			
		
			
			//결재은행2	
			for(var k=0;k<frm.uwgPPR_HT__ctl0_DDL_Bank2.options.length;k++)	
			{
				if(frm.uwgPPR_HT__ctl0_DDL_Bank2.options[k].innerText == document.getElementById(rowId).children[16].innerText)		
				{
					frm.uwgPPR_HT__ctl0_DDL_Bank2.options[k].selected=true;	
					break;					
				}		
				else					
					frm.uwgPPR_HT__ctl0_DDL_Bank2.options[0].selected=true;
			}	
					
			//결재은행3	
			for(var l=0;l<frm.uwgPPR_HT__ctl0_DDL_Bank3.options.length;l++)	
			{		
			
				if(frm.uwgPPR_HT__ctl0_DDL_Bank3.options[l].innerText == document.getElementById(rowId).children[20].innerText)
				{			
					frm.uwgPPR_HT__ctl0_DDL_Bank3.options[l].selected=true;	
					break;
				}
				else
				frm.uwgPPR_HT__ctl0_DDL_Bank3.options[0].selected=true;
			}	
		}
--></SCRIPT>
</HEAD>
	<BODY bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; WIDTH: 800px; POSITION: absolute; TOP: 10px; HEIGHT: 550px"
				cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD colspan="2" align="left">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 60px"
							align="top"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색 ]
							</LEGEND>
							<TABLE style="WIDTH: 800px; HEIGHT: 30px; cellSpacing: " cellPadding="0" cellSpacing="0">
								<TR>
									<TD align="right" width="70" height="30"><FONT face="굴림">거래처명&nbsp;</FONT></TD>
									<TD width="120" height="30"><font face="굴림"></font>
										<igcmbo:webcombo BorderColor="DimGray" SelForeColor="White" Height="20px" SelectedIndex="-1" ForeColor="Black"
											BorderWidth="1px" SelBackColor="10, 36, 106" BackColor="#EEEEE9" BorderStyle="Solid" Width="120px"
											id="wcbCompany" runat="server" Font-Size="9pt" Version="3.00" DropImageXP1="/ig_common/WebGrid3/ig_cmboDown1.bmp"
											DropImageXP2="/ig_common/WebGrid3/ig_cmboDown2.bmp">
											<DropDownLayout DropdownWidth="550px" BorderCollapse="Separate" RowSelectors="No" RowHeightDefault="20px"
												HeaderClickAction="Select" DropdownHeight="350px" TableLayout="Fixed" StationaryMargins="Header">
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
										</igcmbo:webcombo></TD>
									<TD align="right" center width="70" height="30"><FONT face="굴림">결제방법&nbsp;</FONT></TD>
									<TD style="WIDTH: 100px; vAlign: " centeralign="middle" height="30"><asp:dropdownlist id="SettleDropDown" runat="server" Height="20px" Width="100px" BackColor="#EEEEE9"
											Font-Size="9pt"></asp:dropdownlist></TD>
									<TD align="right" center width="70" height="30"><FONT face="굴림">지급일&nbsp;</FONT></TD>
									<TD centeralign="middle" width="100" height="30"><FONT face="굴림"></FONT><igsch:webdatechooser id="wdcStartDate" runat="server" Width="100px" Text=" " NullDateLabel=" " Height="20px"
											BackColor="#EEEEE9" Font-Size="9pt" BorderStyle="Solid" BorderColor="DimGray">
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
									<TD align="center" center width="10" height="30"><FONT face="굴림">~</FONT></TD>
									<TD centeralign="middle" width="85" height="30"><igsch:webdatechooser id="wdcEndDate" runat="server" Width="100px" Text=" " NullDateLabel=" " DropDownAlignment="Center"
											Height="20px" BackColor="#EEEEE9" Font-Size="9pt" BorderStyle="Solid" BorderColor="DimGray">
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
									<TD colspan="2" vAlign="middle" align="right" width="145" height="30"><asp:button id="btnInit" runat="server" Height="20px" Width="65px" Text="초기화" Font-Size="9pt"></asp:button><FONT face="굴림">&nbsp;</FONT>
										<asp:button id="btnSearch" runat="server" Height="20px" Width="65px" Text="검   색" Font-Size="9pt"></asp:button><FONT face="굴림"></FONT></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD colspan="2" style="HEIGHT: 444px" vAlign="top" align="left">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 440px"
							align="top"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색결과 ]
							</LEGEND>
							<TABLE style="WIDTH: 100%; HEIGHT: 432px" cellSpacing="0" cellPadding="0">
								<TR>
									<TD vAlign="top" align="center"><FONT face="굴림"><igtbl:ultrawebgrid id="uwgPPR_HT" runat="server" Height="435px" Width="800px">
<DisplayLayout AutoGenerateColumns="False" AllowSortingDefault="Yes" RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="uwgPPRxHT" CellClickActionDefault="RowSelect" AllowUpdateDefault="RowTemplateOnly">

<AddNewBox>

<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

</Style>

</AddNewBox>

<Pager PageSize="20" StyleMode="ComboBox" AllowPaging="True">

<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

</Style>

</Pager>

<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray" Height="25px">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

</HeaderStyleDefault>

<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DimGray" BorderStyle="Solid" BackColor="Silver" Height="435px">
</FrameStyle>

<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

</FooterStyleDefault>

<ClientSideEvents ColumnHeaderClickHandler="ColumnHeaderClickHandler" AfterRowTemplateOpenHandler="uwgPPR_HT_AfterRowTemplateOpenHandler" AfterRowTemplateCloseHandler="uwgPPR_HT_AfterRowTemplateCloseHandler">
</ClientSideEvents>

<EditCellStyleDefault BorderWidth="0px" BorderStyle="None">
</EditCellStyleDefault>

<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy">
</SelectedRowStyleDefault>

<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue">
</RowAlternateStyleDefault>

<RowStyleDefault Cursor="Hand" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">

<Padding Left="3px">
</Padding>

<BorderDetails WidthLeft="0px" WidthTop="0px">
</BorderDetails>

</RowStyleDefault>

</DisplayLayout>

<Bands>
<igtbl:UltraGridBand>
<Columns>
<igtbl:UltraGridColumn HeaderText="선택" Key="chk" Width="40px" Type="CheckBox" HeaderClickAction="Select" BaseColumnName="" AllowUpdate="Yes">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="거래처" Key="CompanyName" BaseColumnName="CompanyName">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" BaseColumnName="BusinessRegistrationNum">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="지급일" Key="PaymentDate" Format="yyyy-MM-dd" BaseColumnName="PaymentDate">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="수금액(물품대)" Key="ItemPaymentCost" Format="###,###,###.00" BaseColumnName="ItemPaymentCost">
<CellStyle HorizontalAlign="Right">

<Padding Right="4px">
</Padding>

</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="지급액(부가세)" Key="SupplementaryValueTaxPaymentCost" Format="###,###,###.00" BaseColumnName="SupplementaryValueTaxPaymentCost">
<CellStyle HorizontalAlign="Right">

<Padding Right="4px">
</Padding>

</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="결제방법코드" Key="DecisionMethodCode" Hidden="True" BaseColumnName="DecisionMethodCode">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="결제방법" Key="DecisionMethod" BaseColumnName="DecisionMethod">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="증거자료" Key="ProofData" Hidden="True" BaseColumnName="ProofData">
<CellStyle HorizontalAlign="Left">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="어음번호1" Key="BillNum1" BaseColumnName="BillNum1">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="결제일자1" Key="BillPaymentDate1" Format="yyyy-MM-dd" BaseColumnName="BillPaymentDate1">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="은행코드1" Key="BankCode1" Hidden="True" BaseColumnName="BankCode1">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="은행명1" Key="BankName1" BaseColumnName="BankName1">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="어음번호2" Key="BillNum2" BaseColumnName="BillNum2">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="결제일자2" Key="BillPaymentDate2" Format="yyyy-MM-dd" BaseColumnName="BillPaymentDate2">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="은행코드2" Key="BankCode2" Hidden="True" BaseColumnName="BankCode2">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="은행명2" Key="BankName2" BaseColumnName="BankName2">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="어음번호3" Key="BillNum3" BaseColumnName="BillNum3">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="결제일자3" Key="BillPaymentDate3" Format="yyyy-MM-dd" BaseColumnName="BillPaymentDate3">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="은행코드3" Key="BankCode3" Hidden="True" BaseColumnName="BankCode3">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="은행명3" Key="BankName3" BaseColumnName="BankName3">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" BaseColumnName="RegistrationPerson">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" BaseColumnName="RegistrationPersonID">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" BaseColumnName="UpdatingPerson">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" BaseColumnName="UpdatingPersonID">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
<CellStyle HorizontalAlign="Center">
</CellStyle>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn HeaderText="지급원장번호" Key="PaymentHistoryIndex" BaseColumnName="PaymentHistoryIndex">
<CellStyle HorizontalAlign="Right">
</CellStyle>
</igtbl:UltraGridColumn>
</Columns>

<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">

<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px">
</BorderDetails>

</RowTemplateStyle>

<RowEditTemplate>
															<P align="right">
																<TABLE id="Table2" style="HEIGHT: 104px" cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD style="WIDTH: 65px" align="right">거래처</TD>
																		<TD><INPUT id="txtCompanyName" style="WIDTH: 100px; HEIGHT: 20px" type="text" columnKey="CompanyName"></TD>
																		<TD style="WIDTH: 62px" align="right">지급일&nbsp;</TD>
																		<TD>
																			<igsch:WebDateChooser id="wdcPaymentDate" runat="server" Width="100px" Height="20px" NullDateLabel=" " 
 Text="Null">
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
																			</igsch:WebDateChooser></TD>
																		<TD style="WIDTH: 92px" align="right">수금액(물품대)</TD>
																		<TD><INPUT id="txtItemPaymentCost" style="WIDTH: 100px; HEIGHT: 20px" type="text" columnKey="ItemPaymentCost"></TD>
																		<TD style="WIDTH: 96px" align="right">지급액(부가세)</TD>
																		<TD><INPUT id="txtSupplementaryValueTaxPaymentCost" style="WIDTH: 100px; HEIGHT: 20px" type="text"
																				columnKey="SupplementaryValueTaxPaymentCost"></TD>
																	</TR>
																	<TR>
																		<TD style="WIDTH: 65px" align="right">결제방법</TD>
																		<TD>
																			<asp:DropDownList id=DDL_DecisionMethod runat="server" Width="100px" DataValueField="SmallClassificationCode" DataSource="<%# dsDecision %>" DataTextField="SmallClassificationName">
																			</asp:DropDownList>
																		<TD style="WIDTH: 62px" align="right">증거자료</TD>
																		<TD colSpan="5"><INPUT id="txtProofData" style="WIDTH: 464px; HEIGHT: 20px" type="text" size="72" columnKey="ProofData"></TD>
																	</TR>
																	<TR>
																		<TD style="WIDTH: 65px; HEIGHT: 4px" align="right">어음번호1</TD>
																		<TD style="HEIGHT: 4px"><INPUT id="Bill1" style="WIDTH: 100px; HEIGHT: 20px" type="text" columnKey="BillNum1"></TD>
																		<TD style="WIDTH: 62px; HEIGHT: 4px" align="right">결제일자1</TD>
																		<TD style="HEIGHT: 4px">
																			<igsch:WebDateChooser id="wdcBillPaymentDate1" runat="server" Width="100px" Height="20px" NullDateLabel=" " 
 Text="Null">
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
																			</igsch:WebDateChooser></TD>
																		<TD style="WIDTH: 92px; HEIGHT: 4px" align="right">은행명1&nbsp;</TD>
																		<TD style="HEIGHT: 4px">
																			<asp:DropDownList id=DDL_Bank1 runat="server" Width="100px" Height="20px" DataValueField="SmallClassificationCode" DataSource="<%# dsBank %>" DataTextField="SmallClassificationName">
																			</asp:DropDownList></TD>
																		<TD style="WIDTH: 96px; HEIGHT: 4px"></TD>
																		<TD style="HEIGHT: 4px"></TD>
																	</TR>
																	<TR>
																		<TD style="WIDTH: 65px" align="right">어음번호2</TD>
																		<TD><INPUT id="Bill2" style="WIDTH: 100px; HEIGHT: 20px" type="text" columnKey="BillNum2"></TD>
																		<TD style="WIDTH: 62px" align="right">결제일자2</TD>
																		<TD>
																			<igsch:WebDateChooser id="wdcBillPaymentDate2" runat="server" Width="100px" Height="20px" NullDateLabel=" " 
 Text="Null">
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
																			</igsch:WebDateChooser></TD>
																		<TD style="WIDTH: 92px" align="right">은행명2&nbsp;</TD>
																		<TD>
																			<asp:DropDownList id=DDL_Bank2 runat="server" Width="100px" Height="20px" DataValueField="SmallClassificationCode" DataSource="<%# dsBank %>" DataTextField="SmallClassificationName">
																			</asp:DropDownList></TD>
																		<TD style="WIDTH: 96px"></TD>
																		<TD></TD>
																	</TR>
																	<TR>
																		<TD style="WIDTH: 65px" align="right">어음번호3</TD>
																		<TD><INPUT id="Bill3" style="WIDTH: 100px; HEIGHT: 20px" type="text" columnKey="BillNum3"></TD>
																		<TD style="WIDTH: 62px" align="right">결제일자3</TD>
																		<TD>
																			<igsch:WebDateChooser id="wdcBillPaymentDate3" runat="server" Width="100px" Height="20px" NullDateLabel=" " 
 Text="Null">
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
																			</igsch:WebDateChooser></TD>
																		<TD style="WIDTH: 92px" align="right">은행명3&nbsp;</TD>
																		<TD>
																			<asp:DropDownList id=DDL_Bank3 runat="server" Width="100px" Height="20px" DataValueField="SmallClassificationCode" DataSource="<%# dsBank %>" DataTextField="SmallClassificationName">
																			</asp:DropDownList></TD>
																		<TD style="WIDTH: 96px" colSpan="1"></TD>
																		<TD><FONT face="굴림">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</FONT>
																			<INPUT id="igtbl_reOkBtn" style="WIDTH: 53px; HEIGHT: 20px" onclick="igtbl_gRowEditButtonClick(event);__doPostBack('linkUpdate','');"
																				type="button" value="수   정">&nbsp;&nbsp;
																		</TD>
																	</TR>
																</TABLE>
															</P>
															<BR>
															<P align="center">&nbsp;
															</P>
															<P align="center">&nbsp;</P>
															<P align="center">&nbsp;</P>
															<P align="center">&nbsp;</P>
															<P align="center">&nbsp;</P>
														
</RowEditTemplate>
</igtbl:UltraGridBand>
</Bands>
											</igtbl:ultrawebgrid></FONT></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD colspan="2" vAlign="top" align="left">
						<table style="WIDTH: 100%; HEIGHT: 8px" height="8">
							<TR>
								<TD style="WIDTH: 716px" align="left" height="5"><FONT face="굴림"></FONT></TD>
								<TD align="right" height="5"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 716px" align="left"><asp:button id="btnExcel" runat="server" Height="20px" Width="65px" Text="Excel" Font-Size="9pt"></asp:button><igtblexp:ultrawebgridexcelexporter id="uwgExcel" runat="server"></igtblexp:ultrawebgridexcelexporter><INPUT id="hd_ItemPaymentCost" style="WIDTH: 75px; HEIGHT: 22px; BACKGROUND-COLOR: #eeeee9"
										type="hidden" size="7" value="0" name="Hidden1" runat="server">
									<asp:linkbutton id="linkUpdate" runat="server" Visible="False">LinkButton</asp:linkbutton><INPUT id="hdRowIndex" style="WIDTH: 75px; HEIGHT: 22px; BACKGROUND-COLOR: #eeeee9" type="hidden"
										size="4" value="0" name="Hidden1" runat="server"><INPUT id="hdYear" style="WIDTH: 24px; HEIGHT: 22px; BACKGROUND-COLOR: #eeeee9" type="hidden"
										size="1" name="Hidden1" runat="server"><INPUT id="hdMon" style="WIDTH: 19px; HEIGHT: 22px; BACKGROUND-COLOR: #eeeee9" type="hidden"
										size="1" name="Hidden1" runat="server"></TD>
								<TD align="right"><asp:button id="btnDelete" runat="server" Height="20px" Width="65px" Text="삭   제" Font-Size="9pt"></asp:button><FONT face="굴림"></FONT></TD>
							</TR>
						</table>
					</TD>
				</TR>
			</TABLE>
		</form>
	</BODY>
</HTML>
