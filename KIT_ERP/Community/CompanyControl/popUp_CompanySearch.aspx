<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="popUp_CompanySearch.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.CompanyControl.popUp_CompanySearch" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>거래처검색</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<base target="_self">
		<SCRIPT type="text/javascript"><!--

		function UltraWebGrid1_AfterRowActivateHandler(gridName, rowId){
			//Add code to handle your event here.
			var objRow = igtbl_getActiveRow(gridName);
			var CompanyName = objRow.getCellFromKey("CompanyName").getValue();
			var BusinessRegistrationNum = objRow.getCellFromKey("BusinessRegistrationNum").getValue();
			var PresidentName = objRow.getCellFromKey("PresidentName").getValue();
			var arrReturnValues = new Array();
			arrReturnValues["CompanyName"] = CompanyName;
			arrReturnValues["BusinessRegistrationNum"] = BusinessRegistrationNum;
			arrReturnValues["PresidentName"] = PresidentName;
			window.returnValue = arrReturnValues;
			window.close();
		}
--></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" cellSpacing="0"
				cellPadding="0" width="352" border="0">
				<TR>
					<TD vAlign="top" height="330">
						<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="352px" Height="328px">
							<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" RowHeightDefault="20px" Version="3.00"
								BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1"
								TableLayout="Fixed" CellClickActionDefault="RowSelect">
								<AddNewBox>
									<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
									</Style>
								</AddNewBox>
								<Pager QuickPages="10" PageSize="14" StyleMode="ComboBox" Alignment="Center" AllowPaging="True">
									<Style BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderStyle="Solid" BackColor="LightGray"
										Height="20px">
									</Style>
								</Pager>
								<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray" Height="23px">
									<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
								</HeaderStyleDefault>
								<FrameStyle Width="352px" BorderWidth="1px" Font-Size="9pt" Font-Names="굴림" BorderColor="DimGray"
									BorderStyle="Solid" Height="328px"></FrameStyle>
								<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
									<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
								</FooterStyleDefault>
								<ClientSideEvents AfterRowActivateHandler="UltraWebGrid1_AfterRowActivateHandler"></ClientSideEvents>
								<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
								<RowAlternateStyleDefault BackColor="LightSteelBlue"></RowAlternateStyleDefault>
								<RowStyleDefault BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
									<Padding Left="3px"></Padding>
									<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
								</RowStyleDefault>
							</DisplayLayout>
							<Bands>
								<igtbl:UltraGridBand>
									<Columns>
										<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" Width="150px" BaseColumnName="CompanyName">
											<HeaderStyle Height="25px"></HeaderStyle>
										</igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Width="100px" BaseColumnName="BusinessRegistrationNum"></igtbl:UltraGridColumn>
										<igtbl:UltraGridColumn HeaderText="대표자" Key="PresidentName" Width="100px" BaseColumnName="PresidentName"></igtbl:UltraGridColumn>
									</Columns>
								</igtbl:UltraGridBand>
							</Bands>
						</igtbl:ultrawebgrid></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
