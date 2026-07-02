<%@ Page language="c#" Codebehind="PostSearch.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.Popup.PostSearch" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PostSearch</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<STYLE type="text/css">BODY { SCROLLBAR-FACE-COLOR: #ffffff; SCROLLBAR-HIGHLIGHT-COLOR: #ffffff; SCROLLBAR-SHADOW-COLOR: #ffffff; SCROLLBAR-3DLIGHT-COLOR: #808080; SCROLLBAR-ARROW-COLOR: #400040; SCROLLBAR-TRACK-COLOR: #ffffff; SCROLLBAR-DARKSHADOW-COLOR: #808080 }
		</STYLE>
		<SCRIPT type="text/javascript"><!--
		function OnFocus_Obj(obj) {
						obj.select();
					}

		
		function UltraWebGrid1_AfterSelectChangeHandler(gridName, id){
			//Add code to handle your event here.
			document.Form1.index.value = document.getElementById(id).children[0].innerText;
		}
--></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="buttonface">
		<form id="Form1" method="post" runat="server">
			<asp:Button id="Button1" style="Z-INDEX: 101; LEFT: 176px; POSITION: absolute; TOP: 16px" runat="server"
				Height="20px" Text="찾 기"></asp:Button>
			<asp:TextBox id="TextBox1" style="Z-INDEX: 102; LEFT: 16px; IME-MODE: active; POSITION: absolute; TOP: 16px"
				runat="server">동(읍/면/리)을 입력하세요</asp:TextBox>
			<igtbl:UltraWebGrid id="UltraWebGrid1" style="Z-INDEX: 103; LEFT: 0px; POSITION: absolute; TOP: 48px"
				runat="server" Height="200px" Width="305px">
				<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" RowHeightDefault="20px" Version="3.00"
					SelectTypeRowDefault="Single" BorderCollapseDefault="Separate" RowSelectorsDefault="No" Name="UltraWebGrid1"
					TableLayout="Fixed" CellClickActionDefault="RowSelect">
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
					<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
					<FrameStyle Width="305px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderStyle="Solid"
						Height="200px"></FrameStyle>
					<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
						<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
					</FooterStyleDefault>
					<ClientSideEvents AfterSelectChangeHandler="UltraWebGrid1_AfterSelectChangeHandler"></ClientSideEvents>
					<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
					<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid">
						<Padding Left="3px"></Padding>
						<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
					</RowStyleDefault>
				</DisplayLayout>
				<Bands>
					<igtbl:UltraGridBand>
						<Columns>
							<igtbl:UltraGridColumn HeaderText="번호" Key="Temp_index" Hidden="True" BaseColumnName="Temp_index">
								<HeaderStyle Height="20px"></HeaderStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="우편번호" Key="Temp_zip" Width="70px" BaseColumnName="Temp_zip">
								<HeaderStyle Height="20px"></HeaderStyle>
							</igtbl:UltraGridColumn>
							<igtbl:UltraGridColumn HeaderText="주    소" Key="Temp_add" Width="230px" BaseColumnName="Temp_add"></igtbl:UltraGridColumn>
						</Columns>
					</igtbl:UltraGridBand>
				</Bands>
			</igtbl:UltraWebGrid><INPUT id="index" style="Z-INDEX: 104; LEFT: 224px; WIDTH: 20px; POSITION: absolute; TOP: 16px"
				type="hidden" value="0" runat="server">
		</form>
	</body>
</HTML>
