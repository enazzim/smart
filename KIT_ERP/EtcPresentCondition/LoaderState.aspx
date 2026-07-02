<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="LoaderState.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.EtcPresentCondition.LoaderState" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>LoaderState</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../StyleSheet2.css">
		<script language="javascript">
		<!--
		function ResettxtBox()
		{
			var date = new Date();
			var year = date.getFullYear();
			var month = date.getMonth()+1;
			
			for(var i=0;i<document.LoaderState.ddlYear.options.length;i++)
			{
				if(document.LoaderState.ddlYear.options[i].innerText == year)
				{
					document.LoaderState.ddlYear.options[i].selected=true;
					break;
				}
				else
					document.LoaderState.ddlYear.options[0].selected=true;
			}
			
			for(var i=0;i<document.LoaderState.ddlMon.options.length;i++)
			{
				if(document.LoaderState.ddlMon.options[i].innerText == month)
				{
					document.LoaderState.ddlMon.options[i].selected=true;
					break;
				}
				else
					document.LoaderState.ddlMon.options[0].selected=true;
			}
			
			document.LoaderState.Delevery.value = "0";
			document.LoaderState.Assay.value = "0";
			
			
			var objChooser1 = igdrp_getComboById("wdcAssayDate");
			var objChooser2 = igdrp_getComboById("wdcDeliveryDate");
			objChooser1.setValue(null);
			objChooser2.setValue(null);
			document.LoaderState.txtAssayQuantity.value = "0";
			document.LoaderState.txtDeliveryQuantity.value = "0";
			
			
		}
		//-->
		</script>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="LoaderState" method="post" runat="server">
			<TABLE style="Z-INDEX: 101; POSITION: absolute; TOP: 8px; LEFT: 8px" border="0" cellSpacing="0"
				cellPadding="0" width="800">
				<TR>
					<TD style="PADDING-TOP: 10px" vAlign="top" width="800" align="center">
						<FIELDSET style="BORDER-BOTTOM: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-TOP: #696969 2px solid; BORDER-RIGHT: #696969 2px solid"><LEGEND align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="0" width="800">
								<TR>
									<td width="40" align="right">년도
									</td>
									<td width="60"><asp:dropdownlist id="ddlYear" runat="server" Width="55px" BackColor="#EEEEE9">
											<asp:ListItem Value="2015">2015</asp:ListItem>
											<asp:ListItem Value="2016">2016</asp:ListItem>
											<asp:ListItem Value="2017">2017</asp:ListItem>
											<asp:ListItem Value="2018">2018</asp:ListItem>
											<asp:ListItem Value="2019">2019</asp:ListItem>
											<asp:ListItem Value="2020">2020</asp:ListItem>
										</asp:dropdownlist></td>
									<td width="50" align="right">월
									</td>
									<td width="50"><asp:dropdownlist id="ddlMon" runat="server" Width="50px" BackColor="#EEEEE9">
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
										</asp:dropdownlist></td>
									<td width="40" align="right"><FONT face="굴림"></FONT></td>
									<td width="70"></td>
									<td width="490" colSpan="5" align="right"><FONT face="굴림"><INPUT style="WIDTH: 65px; HEIGHT: 20px" id="btnReset" onclick="javascript:ResettxtBox()"
												value="초기화" type="button" name="btnReset">
											<asp:button id="btComSearch" runat="server" Width="60px" Text="검  색" Height="20px"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
										</FONT>
									</td>
								</TR>
								<TR>
									<TD height="10" width="40" align="right"></TD>
									<TD height="10" width="70"></TD>
									<TD height="10" width="40" align="right"></TD>
									<TD height="10" width="60"></TD>
									<TD height="10" width="50" align="right"></TD>
									<TD height="10" width="50"></TD>
									<TD height="10" width="490" colSpan="5" align="right"></TD>
								</TR>
							</TABLE>
							<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="800">
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD height="10" vAlign="top" width="800" align="center"></TD>
				</TR>
				<TR>
					<TD vAlign="top" width="800" align="center">
						<FIELDSET style="BORDER-BOTTOM: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-TOP: #696969 2px solid; BORDER-RIGHT: #696969 2px solid"><LEGEND align="top">[검색결과]</LEGEND>
							<TABLE id="LoaderState_Table" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD height="420" colSpan="6">
										<table id="table" border="1" cellSpacing="0" borderColor="#000000" cellPadding="0" align="center">
											<tr>
												<td class="size" align="center">날짜</td>
												<td class="size" colSpan="2" align="center">1</td>
												<td class="size" colSpan="2" align="center">2</td>
												<td class="size" colSpan="2" align="center">3</td>
												<td class="size" colSpan="2" align="center">4</td>
												<td class="size" colSpan="2" align="center">5</td>
												<td class="size" colSpan="2" align="center">6</td>
												<td class="size" colSpan="2" align="center">7</td>
												<td class="size" colSpan="2" align="center">8</td>
												<td class="size" colSpan="2" align="center">9</td>
												<td class="size" colSpan="2" align="center">10</td>
											</tr>
											<tr class="size">
												<td class="size" rowSpan="2" width="150" align="center">조립</td>
												<td height="28" width="90" align="center"><asp:label style="Z-INDEX: 0" id="Label1" runat="server" Font-Size="Larger">&nbsp;</asp:label>&nbsp;</td>
												<td width="90">&nbsp;</td>
												<td width="90" align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label2" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td width="90">&nbsp;</td>
												<td width="90" align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label3" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td width="90">&nbsp;</td>
												<td width="90" align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label4" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td width="90">&nbsp;</td>
												<td width="90" align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label5" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td width="90">&nbsp;</td>
												<td width="90" align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label6" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td width="90">&nbsp;</td>
												<td width="90" align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label7" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td width="90">&nbsp;</td>
												<td width="90" align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label8" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td width="90">&nbsp;</td>
												<td width="90" align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label9" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td width="90">&nbsp;</td>
												<td width="90" align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label10" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td width="90">&nbsp;</td>
											</tr>
											<tr>
												<td height="28">&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label11" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label12" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label13" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label14" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label15" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label16" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label17" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label18" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label19" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label20" runat="server">&nbsp;</asp:label>&nbsp;</td>
											</tr>
											<tr>
												<td class="size" rowSpan="2" align="center">출하</td>
												<td height="28" align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label21" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label22" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label23" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label24" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label25" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label26" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label27" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label28" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label29" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label30" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
											</tr>
											<tr>
												<td height="28">&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label31" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label32" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label33" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label34" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label35" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label36" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label37" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label38" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label39" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label40" runat="server">&nbsp;</asp:label>&nbsp;</td>
											</tr>
											<tr>
												<td class="size" align="center">날짜</td>
												<td class="size" colSpan="2" align="center">11</td>
												<td class="size" colSpan="2" align="center">12</td>
												<td class="size" colSpan="2" align="center">13</td>
												<td class="size" colSpan="2" align="center">14</td>
												<td class="size" colSpan="2" align="center">15</td>
												<td class="size" colSpan="2" align="center">16</td>
												<td class="size" colSpan="2" align="center">17</td>
												<td class="size" colSpan="2" align="center">18</td>
												<td class="size" colSpan="2" align="center">19</td>
												<td class="size" colSpan="2" align="center">20</td>
											</tr>
											<tr>
												<td class="size" rowSpan="2" align="center">조립</td>
												<td height="28" align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label41" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label42" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label43" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label44" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label45" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label46" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label47" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label48" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label49" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label50" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
											</tr>
											<tr>
												<td height="28">&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label51" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label52" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label53" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label54" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label55" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label56" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label57" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label58" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label59" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label60" runat="server">&nbsp;</asp:label>&nbsp;</td>
											</tr>
											<tr>
												<td class="size" rowSpan="2" align="center">출하</td>
												<td height="28" align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label61" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label62" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label63" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label64" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label65" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label66" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label67" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label68" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label69" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label70" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
											</tr>
											<tr>
												<td height="28">&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label71" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label72" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label73" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label74" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label75" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label76" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label77" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label78" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label79" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label80" runat="server">&nbsp;</asp:label>&nbsp;</td>
											</tr>
											<tr>
												<td class="size" align="center">날짜</td>
												<td class="size" colSpan="2" align="center">21</td>
												<td class="size" colSpan="2" align="center">22</td>
												<td class="size" colSpan="2" align="center">23</td>
												<td class="size" colSpan="2" align="center">24</td>
												<td class="size" colSpan="2" align="center">25</td>
												<td class="size" colSpan="2" align="center">26</td>
												<td class="size" colSpan="2" align="center">27</td>
												<td class="size" colSpan="2" align="center">28</td>
												<td class="size" colSpan="2" align="center">29</td>
												<td class="size" colSpan="2" align="center">30</td>
											</tr>
											<tr>
												<td class="size" rowSpan="2" align="center">조립</td>
												<td height="28" align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label81" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label82" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label83" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label84" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label85" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label86" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label87" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label88" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label89" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label90" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
											</tr>
											<tr>
												<td height="28">&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label91" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label92" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label93" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label94" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label95" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label96" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label97" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label98" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label99" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label100" runat="server">&nbsp;</asp:label>&nbsp;</td>
											</tr>
											<tr>
												<td class="size" rowSpan="2" align="center">출하</td>
												<td height="28" align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label101" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label102" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label103" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label104" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label105" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label106" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label107" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label108" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label109" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label110" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
											</tr>
											<tr>
												<td height="28">&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label111" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label112" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label113" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label114" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label115" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label116" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label117" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label118" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label119" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label120" runat="server">&nbsp;</asp:label>&nbsp;</td>
											</tr>
											<tr>
												<td class="size" align="center">날짜</td>
												<td class="size" colSpan="2" align="center">31</td>
												<td class="size" colSpan="2" align="center">&nbsp;</td>
												<td class="size" colSpan="2" align="center">&nbsp;</td>
												<td class="size" colSpan="2" align="center">&nbsp;</td>
												<td class="size" colSpan="2" align="center">&nbsp;</td>
												<td class="size" colSpan="2" align="center">&nbsp;</td>
												<td class="size" colSpan="2" align="center">&nbsp;</td>
												<td class="size" colSpan="2" align="center">&nbsp;</td>
												<td class="size" colSpan="2" align="center">&nbsp;</td>
												<td class="size" colSpan="2" align="center">&nbsp;</td>
											</tr>
											<tr>
												<td class="size" rowSpan="2" align="center">조립</td>
												<td height="28" align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label121" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label122" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label123" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label124" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label125" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label126" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label127" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label128" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label129" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label130" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
											</tr>
											<tr>
												<td height="28">&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label131" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label132" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label133" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label134" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label135" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label136" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label137" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label138" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label139" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label140" runat="server">&nbsp;</asp:label>&nbsp;</td>
											</tr>
											<tr>
												<td class="size" rowSpan="2" align="center">출하</td>
												<td height="28" align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label141" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label142" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label143" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label144" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label145" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label146" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label147" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label148" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label149" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label150" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
											</tr>
											<tr>
												<td height="28">&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label151" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label152" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label153" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label154" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label155" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label156" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label157" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label158" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label159" runat="server">&nbsp;</asp:label>&nbsp;</td>
												<td>&nbsp;</td>
												<td align="center"><asp:label  Font-Size="Larger" style="Z-INDEX: 0" id="Label160" runat="server">&nbsp;</asp:label>&nbsp;</td>
											</tr>
										</table>
										<INPUT id="Assay" value="0" type="hidden" name="Hidden1" runat="server"><INPUT style="Z-INDEX: 0" id="Delevery" value="0" type="hidden" name="Hidden1" runat="server">
									</TD>
								</TR>
								<tr>
									<td colSpan="6" align="right">
										<TABLE cellSpacing="0" cellPadding="0">
											<TR>
												<TD style="FONT-SIZE: 9pt" width="70" align="right">조립량&nbsp;</TD>
												<TD width="100" align="left"><asp:textbox style="TEXT-ALIGN: right" id="txtAssayQuantity" runat="server" Width="100px" BackColor="#EEEEE9"
														Height="20px" BorderStyle="Solid" BorderColor="DimGray" BorderWidth="1px">0</asp:textbox></TD>
												<TD style="FONT-SIZE: 9pt" width="70" align="right"><FONT face="굴림">조립일</FONT>&nbsp;&nbsp;</TD>
												<TD width="100" align="left"><igsch:webdatechooser style="Z-INDEX: 0" id="wdcAssayDate" runat="server" Width="100px" BackColor="#EEEEE9"
														Height="20px" NullDateLabel=" ">
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
												<TD width="140" align="center"><FONT face="굴림"><asp:button style="Z-INDEX: 0" id="Button1" runat="server" Width="65px" Text="등   록" Height="20px"
															Font-Size="9pt"></asp:button></FONT></TD>
												<TD style="FONT-SIZE: 9pt" title="품목명:" width="70" align="right">출고량&nbsp;</TD>
												<TD width="100"><asp:textbox style="TEXT-ALIGN: right" id="txtDeliveryQuantity" runat="server" Width="100px"
														BackColor="#EEEEE9" Height="20px" BorderStyle="Solid" BorderColor="DimGray" BorderWidth="1px">0</asp:textbox></TD>
												<TD style="FONT-SIZE: 9pt" width="70" align="right"><FONT face="굴림">출고일</FONT>&nbsp;&nbsp;</TD>
												<TD width="100" align="left"><igsch:webdatechooser style="Z-INDEX: 0" id="wdcDeliveryDate" runat="server" Width="100px" BackColor="#EEEEE9"
														Height="20px" NullDateLabel=" ">
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
												<TD height="50" width="140" align="center"><FONT face="굴림"><asp:button style="Z-INDEX: 0" id="btnRegistration" runat="server" Width="65px" Text="등   록"
															Height="20px" Font-Size="9pt"></asp:button></FONT></TD>
											</TR>
										</TABLE>
									</td>
								</tr>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			&nbsp;
		</form>
	</body>
</HTML>
