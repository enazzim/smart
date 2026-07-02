<%@ Page language="c#" Codebehind="ReceiptDisburse.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ManagementInfomation.ReceiptDisburse" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ReceiptDisburse</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT type="text/javascript"><!--

		function UltraWebGrid1_InitializeLayoutHandler(gridName){
			//Add code to handle your event here.
			var grid = igtbl_getGridById(gridName); // 그리드 객체를 가져옵니다.
			var band = grid.Bands[0]; // 0번째 Band를 가져옵니다.
			var len = 4; // 합쳐야하는 컬럼의 갯수를 입력합니다.
			for(var i=0; i<band.Columns.length; i+=len)
			{
				var main = igtbl_getDocumentElement(band.Columns[i].Id);
				main.colSpan=len;
				for(var j=1; j<len; j++)
				{
					var tar = igtbl_getDocumentElement(band.Columns[i+j].Id);
					tar.style.display = "none";
				}
			}
		}
		
--></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<FONT face="굴림">
				<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" height="530"
					cellSpacing="0" cellPadding="0" width="800" border="0">
					<TR>
						<TD vAlign="top" align="center" width="100%" style="HEIGHT: 88px">
							<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND style="FONT-SIZE: 9pt" align="top">[검색조건]</LEGEND>
								<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800" border="0">
									<TR>
										<TD align="right" height="25"><FONT style="FONT-SIZE: 9pt" face="굴림">&nbsp; </FONT><FONT face="굴림">
											</FONT>
										</TD>
									</TR>
									<TR>
										<TD align="right" height="25"><FONT face="굴림"></FONT><asp:button id="bt_Clear" runat="server" Text="초기화" Height="20px" Width="60px"></asp:button><asp:button id="bt_Search" runat="server" Text="검  색" Height="20px" Width="60px"></asp:button><FONT face="굴림">&nbsp;</FONT></TD>
									</TR>
								</TABLE>
							</FIELDSET>
						</TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center" height="10"></TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center">
							<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND style="FONT-SIZE: 9pt" align="top">[검색결과]</LEGEND>
								<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
									<TR>
										<TD align="center" colSpan="8">
											<igtbl:UltraWebGrid id="UltraWebGrid1" runat="server" Width="100%" Height="200px">
												<Rows>
													<igtbl:UltraGridRow Height="">
														<Cells>
															<igtbl:UltraGridCell Key="" Text="abc"></igtbl:UltraGridCell>
															<igtbl:UltraGridCell Key="" Text="abc"></igtbl:UltraGridCell>
															<igtbl:UltraGridCell Key="" Text="abc"></igtbl:UltraGridCell>
															<igtbl:UltraGridCell Key="" Text="abc"></igtbl:UltraGridCell>
														</Cells>
													</igtbl:UltraGridRow>
													<igtbl:UltraGridRow Height="">
														<Cells>
															<igtbl:UltraGridCell Key="" Text="abc"></igtbl:UltraGridCell>
															<igtbl:UltraGridCell Key="" Text="abc"></igtbl:UltraGridCell>
															<igtbl:UltraGridCell Key="" Text="abc"></igtbl:UltraGridCell>
															<igtbl:UltraGridCell Key="" Text="abc"></igtbl:UltraGridCell>
														</Cells>
													</igtbl:UltraGridRow>
													<igtbl:UltraGridRow Height="">
														<Cells>
															<igtbl:UltraGridCell Key="" Text="abc"></igtbl:UltraGridCell>
															<igtbl:UltraGridCell Key="" Text="abc"></igtbl:UltraGridCell>
															<igtbl:UltraGridCell Key="" Text="abc"></igtbl:UltraGridCell>
															<igtbl:UltraGridCell Key="" Text="abc"></igtbl:UltraGridCell>
														</Cells>
													</igtbl:UltraGridRow>
													<igtbl:UltraGridRow Height="">
														<Cells>
															<igtbl:UltraGridCell Key="" Text="abc"></igtbl:UltraGridCell>
															<igtbl:UltraGridCell Key="" Text="abc"></igtbl:UltraGridCell>
															<igtbl:UltraGridCell Key="" Text="abc"></igtbl:UltraGridCell>
															<igtbl:UltraGridCell Key="" Text="abc"></igtbl:UltraGridCell>
														</Cells>
													</igtbl:UltraGridRow>
													<igtbl:UltraGridRow Height="">
														<Cells>
															<igtbl:UltraGridCell Key="" Text="abc"></igtbl:UltraGridCell>
															<igtbl:UltraGridCell Key="" Text="abc"></igtbl:UltraGridCell>
															<igtbl:UltraGridCell Key="" Text="abc"></igtbl:UltraGridCell>
															<igtbl:UltraGridCell Key="" Text="abc"></igtbl:UltraGridCell>
														</Cells>
													</igtbl:UltraGridRow>
													<igtbl:UltraGridRow Height="">
														<Cells>
															<igtbl:UltraGridCell Key="" Text="abc"></igtbl:UltraGridCell>
															<igtbl:UltraGridCell Key="" Text="abc"></igtbl:UltraGridCell>
															<igtbl:UltraGridCell Key="" Text="abc"></igtbl:UltraGridCell>
															<igtbl:UltraGridCell Key="" Text="abc"></igtbl:UltraGridCell>
														</Cells>
													</igtbl:UltraGridRow>
												</Rows>
												<DisplayLayout RowHeightDefault="20px" Version="3.00" BorderCollapseDefault="Separate" Name="UltraWebGrid1">
													<AddNewBox>
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

														</Style>
													</AddNewBox>
													<Pager>
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

														</Style>
													</Pager>
													<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</HeaderStyleDefault>
													<FrameStyle Width="100%" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
														Height="200px"></FrameStyle>
													<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</FooterStyleDefault>
													<ClientSideEvents InitializeLayoutHandler="UltraWebGrid1_InitializeLayoutHandler"></ClientSideEvents>
													<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
													<RowStyleDefault BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid">
														<Padding Left="3px"></Padding>
														<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
													</RowStyleDefault>
												</DisplayLayout>
												<Bands>
													<igtbl:UltraGridBand>
														<Columns>
															<igtbl:UltraGridColumn HeaderText="당월출고" Key="" BaseColumnName=""></igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn Key="" BaseColumnName=""></igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn Key="" BaseColumnName=""></igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn Key="" BaseColumnName=""></igtbl:UltraGridColumn>
														</Columns>
													</igtbl:UltraGridBand>
												</Bands>
											</igtbl:UltraWebGrid></TD>
									</TR>
									<TR>
										<TD></TD>
										<TD></TD>
										<TD><FONT face="굴림"></FONT></TD>
										<TD></TD>
										<TD><FONT face="굴림"></FONT></TD>
										<TD></TD>
										<TD align="right"></TD>
									</TR>
								</TABLE>
							</FIELDSET>
						</TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
