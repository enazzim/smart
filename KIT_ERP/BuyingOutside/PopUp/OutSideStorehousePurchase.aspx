<%@ Page language="c#" Codebehind="OutSideStorehousePurchase.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BuyingOutside.PopUp.OutSideStorehousePurchase" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>OutSideOutStorehousePurchase</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../StyleSheet3.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<td height="50">
						<table id="table2" cellSpacing="0" cellPadding="0" width="750" background="0">
							<tr>
								<td style="FONT-SIZE: 11pt" width="630" colSpan="2" height="42">
									<p align="center">외주발주 / 자재 출문증 (발주처 보관용)</p>
								</td>
								<td width="120" height="42" rowSpan="2">
									<table cellSpacing="0" cellPadding="0" width="120" background="0">
										<tr>
											<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
												width="60" height="18">발주처</td>
											<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
												align="center" width="60" height="18">거래처</td>
										</tr>
										<tr>
											<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" width="60"
												height="42">&nbsp;</td>
											<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
												width="60" height="42">&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td width="315" height="18">&nbsp;■ 거래처명 :
									<asp:label id="lbComName" runat="server">&nbsp;</asp:label>
								</td>
								<td width="315" height="18">&nbsp;■ 출고일자 :
									<asp:label id="lbOutDate" runat="server">&nbsp;</asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table id="Table3" cellSpacing="0" cellPadding="0" width="750" border="0">
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									width="20" height="18">NO</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									width="145" height="18">출고자재 품번</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									width="145" height="18">출고자재 품명</td>
								<td style="BORDER-TOP: #000000 1px solid; FONT-SIZE: 8pt; BORDER-LEFT: #000000 1px solid"
									align="center" width="30" height="18">수량</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									width="145" height="18">외주발주 품번</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									width="145" height="18">외주발주 품명</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									width="30" height="18">수량</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									width="30" height="18">공정</td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" width="60" height="18">납품일</td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label1" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18"><asp:label id="Label2" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label3" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label4" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label5" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label6" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label7" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label8" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" height="18"><asp:label id="Label9" runat="server">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label10" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18"><asp:label id="Label11" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label12" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><FONT face="굴림"><asp:label id="Label13" runat="server">&nbsp;</asp:label></FONT></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label14" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label15" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label16" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label17" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" height="18"><asp:label id="Label18" runat="server">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label19" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18"><asp:label id="Label20" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label21" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label22" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label23" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label24" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label25" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label26" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" height="18"><asp:label id="Label27" runat="server">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label28" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18"><asp:label id="Label29" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label30" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label31" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label32" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label33" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label34" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label35" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" height="18"><asp:label id="Label36" runat="server">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label37" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18"><asp:label id="Label38" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label39" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label40" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label41" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label42" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label43" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label44" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" height="18"><asp:label id="Label45" runat="server">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label46" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18"><asp:label id="Label47" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label48" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label49" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label50" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label51" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label52" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label53" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" height="18"><asp:label id="Label54" runat="server">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label55" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18"><asp:label id="Label56" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label57" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label58" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label59" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label60" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label61" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label62" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" height="18"><asp:label id="Label63" runat="server">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label64" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label65" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label66" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label67" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label68" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label69" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label70" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label71" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" height="18"><asp:label id="Label72" runat="server">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label73" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label74" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label75" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label76" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label77" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label78" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label79" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label80" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" height="18"><asp:label id="Label81" runat="server">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label82" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label83" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label84" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label85" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label86" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label87" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label88" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label89" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" height="18"><asp:label id="Label90" runat="server">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label91" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label92" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label93" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label94" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label95" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label96" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label97" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label98" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" height="18"><asp:label id="Label99" runat="server">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label100" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label101" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label102" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label103" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label104" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label105" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label106" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label107" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" height="18"><asp:label id="Label108" runat="server">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label109" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label110" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label111" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label112" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label113" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label114" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label115" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label116" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" height="18"><asp:label id="Label117" runat="server">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									align="center" height="18"><FONT face="굴림">
										<asp:label id="Label118" runat="server">&nbsp;</asp:label></FONT></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									align="left" height="18"><FONT face="굴림">
										<asp:label id="Label119" runat="server">&nbsp;</asp:label></FONT></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label120" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									align="right" height="18">
									<asp:label id="Label121" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label122" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label123" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									align="right" height="18">
									<asp:label id="Label124" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									align="center" height="18">
									<asp:label id="Label125" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									align="center" height="18"><FONT face="굴림">
										<asp:label id="Label126" runat="server">&nbsp;</asp:label></FONT></td>
							</tr>
							<tr>
								<td colSpan="9" height="18">&nbsp;※ 거래처 협조사항 :</td>
							</tr>
							<tr>
								<td colSpan="9">
									<p align="center">㈜ 신 동 공 업
									</p>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="center" height="40">
						<table border="0" cellpadding="0" cellspacing="0" width="750">
							<tr>
								<td height="10" style="BORDER-BOTTOM: #000000 2px solid"><FONT face="굴림">&nbsp;</FONT>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table id="table2" cellSpacing="0" cellPadding="0" width="750" background="0">
							<tr>
								<td style="FONT-SIZE: 11pt" width="630" colSpan="2" height="42">
									<p align="center">외주발주 / 자재 출문증 (거래처 보관용)</p>
								</td>
								<td width="120" height="42" rowSpan="2">
									<table cellSpacing="0" cellPadding="0" width="120" background="0">
										<tr>
											<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
												width="60" height="18">발주처</td>
											<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
												align="center" width="60" height="18">거래처</td>
										</tr>
										<tr>
											<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" width="60"
												height="42">&nbsp;</td>
											<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
												width="60" height="42">&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td width="315" height="18">&nbsp;■ 거래처명 :
									<asp:label id="lbComName1" runat="server">&nbsp;</asp:label>
								</td>
								<td width="315" height="18">&nbsp;■ 출고일자 :
									<asp:label id="lbOutDate1" runat="server">&nbsp;</asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table id="Table3" cellSpacing="0" cellPadding="0" width="750" border="0">
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									width="20" height="18">NO</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									width="145" height="18">출고자재 품번</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									width="145" height="18">출고자재 품명</td>
								<td style="BORDER-TOP: #000000 1px solid; FONT-SIZE: 8pt; BORDER-LEFT: #000000 1px solid"
									align="center" width="30" height="18">수량</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									width="145" height="18">외주발주 품번</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									width="145" height="18">외주발주 품명</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									width="30" height="18">수량</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									width="30" height="18">공정</td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" width="60" height="18">납품일</td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label127" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18"><asp:label id="Label128" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label129" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label130" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label131" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label132" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label133" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label134" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" height="18"><asp:label id="Label135" runat="server">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label136" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18"><asp:label id="Label137" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label138" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><FONT face="굴림"><asp:label id="Label139" runat="server">&nbsp;</asp:label></FONT></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label140" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label141" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label142" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label143" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" height="18"><asp:label id="Label144" runat="server">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label145" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18"><asp:label id="Label146" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label147" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label148" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label149" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label150" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label151" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label152" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" height="18"><asp:label id="Label153" runat="server">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label154" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18"><asp:label id="Label155" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label156" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label157" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label158" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label159" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label160" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label161" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" height="18"><asp:label id="Label162" runat="server">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label163" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18"><asp:label id="Label164" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label165" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label166" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label167" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label168" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label169" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label170" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" height="18"><asp:label id="Label171" runat="server">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label172" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18"><asp:label id="Label173" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label174" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label175" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label176" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label177" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label178" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label179" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" height="18"><asp:label id="Label180" runat="server">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label181" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18"><asp:label id="Label182" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label183" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label184" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label185" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label186" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label187" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label188" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" height="18"><asp:label id="Label189" runat="server">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label190" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label191" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label192" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label193" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label194" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label195" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label196" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label197" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" height="18"><asp:label id="Label198" runat="server">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label199" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label200" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label201" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label202" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label203" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label204" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label205" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label206" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" height="18"><asp:label id="Label207" runat="server">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label208" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label209" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label210" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label211" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label212" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label213" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label214" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label215" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" height="18"><asp:label id="Label216" runat="server">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label217" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label218" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label219" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label220" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label221" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label222" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label223" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label224" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" height="18"><asp:label id="Label225" runat="server">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label226" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label227" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label228" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label229" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label230" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label231" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label232" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label233" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" height="18"><asp:label id="Label234" runat="server">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label235" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label236" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label237" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label238" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label239" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label240" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="right" height="18"><asp:label id="Label241" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18"><asp:label id="Label242" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" height="18"><asp:label id="Label243" runat="server">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									align="center" height="18"><FONT face="굴림">
										<asp:label id="Label244" runat="server">&nbsp;</asp:label></FONT></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									align="left" height="18"><FONT face="굴림">
										<asp:label id="Label245" runat="server">&nbsp;</asp:label></FONT></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label246" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									align="right" height="18">
									<asp:label id="Label247" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label248" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									align="left" height="18">
									<asp:label id="Label249" runat="server">&nbsp;</asp:label></td>
								<td style="PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									align="right" height="18">
									<asp:label id="Label250" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									align="center" height="18">
									<asp:label id="Label251" runat="server">&nbsp;</asp:label></td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									align="center" height="18"><FONT face="굴림">
										<asp:label id="Label252" runat="server">&nbsp;</asp:label></FONT></td>
							</tr>
							<tr>
								<td colSpan="9" height="18">&nbsp;※ 거래처 협조사항 :</td>
							</tr>
							<tr>
								<td colSpan="9">
									<p align="center">㈜ 신 동 공 업
									</p>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="center" width="750" height="40">
						<table border="0" cellpadding="0" cellspacing="0" width="750">
							<tr>
								<td height="10" style="BORDER-BOTTOM: #000000 2px double"><FONT face="굴림">&nbsp;</FONT>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td width="750" height="50">
						<table id="Table6" cellSpacing="0" cellPadding="0" width="750" background="0">
							<tr>
								<td width="570" colSpan="2" height="42" style="FONT-SIZE: 11pt">
									<p align="center">
										출고자재 손망실 (거래처 제출용)</p>
								</td>
								<td width="180" height="42" rowSpan="2">
									<table cellSpacing="0" cellPadding="0" width="180" background="0">
										<tr>
											<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
												width="60" height="18">납품업체</td>
											<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
												width="60" height="18">인수자</td>
											<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
												align="center" width="60" height="18">QC</td>
										</tr>
										<tr>
											<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" width="60"
												height="42">&nbsp;</td>
											<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" width="60"
												height="42">&nbsp;</td>
											<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
												width="60" height="42">&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td width="285" height="18">&nbsp;■ 거래처명 :
									<asp:label id="lbComName2" runat="server">&nbsp;</asp:label>
								</td>
								<td width="285" height="18">&nbsp;■ 입고일자 :
									<asp:label id="lbInDate" runat="server">&nbsp;</asp:label>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table id="Table7" cellSpacing="0" cellPadding="0" width="750" border="0">
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18" width="20">NO</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18" width="130">외주납품 품번</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18" width="130">외주납품 품명</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18" width="50">납품수량</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18" width="50">외주공정</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18" width="130">손망실 출고자재</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18" width="30">수량</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" align="center"
									height="18" width="100">사유</td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									align="center" height="18" width="110">발주처 QC 결정</td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="center">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="left">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="left">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="right">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="center">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="left">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="right">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="left"><FONT face="굴림"></FONT>&nbsp;</td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									height="18" align="center"><FONT face="굴림"></FONT>&nbsp;</td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="center">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="left">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="left">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="right">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="center">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="left">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="right">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="left"><FONT face="굴림"></FONT>&nbsp;</td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									height="18" align="center">&nbsp;</td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="center">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="left">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="left">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="right">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="center">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="left">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="right">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="left">&nbsp;</td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									height="18" align="center">&nbsp;</td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="center">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="left">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="left">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="right">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="center">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="left">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="right">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid" height="18"
									align="left"><FONT face="굴림"></FONT>&nbsp;</td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid"
									height="18" align="center">&nbsp;</td>
							</tr>
							<tr>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									height="18" align="center">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									height="18" align="left">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									height="18" align="left">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									height="18" align="right">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									height="18" align="center">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									height="18" align="left">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									height="18" align="right">&nbsp;</td>
								<td style="BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									height="18" align="left">&nbsp;</td>
								<td style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid"
									height="18" align="center">&nbsp;</td>
							</tr>
							<tr>
								<td colspan="9" height="36">
									&nbsp;※ 거래처 협조사항 : 출고자재 손망실 발생시 거래명세서와 함께 제출해 주시기 바랍니다.
									<br>
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
									미제출시에는 출고자재에 대한 외주처 재고조사시 외주 입고수량 대비 결손 자재를 일괄 변상처리하겠습니다.
								</td>
							</tr>
							<tr>
								<td colspan="9">
									<p align="center">㈜ 신 동 공 업
									</p>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
