<%@ Page language="c#" Codebehind="HandockOrder.aspx.cs" AutoEventWireup="false" Inherits="hanaro.BuyingOutside.PopUp.HandockOrder" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>HandockOrder</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="1000" border="0" cellspacing="0" cellpadding="0" id="tabel1" style="FONT-SIZE: 9pt; LEFT: 0px; POSITION: absolute; TOP: 0px">
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; HEIGHT: 19px"
						colspan="2" vAlign="middle" align="center" height="19">발주번호</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; HEIGHT: 19px"
						colspan="2" vAlign="middle" align="center" height="19">
						<asp:Label id="lbNum" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; FONT-WEIGHT: bold; FONT-SIZE: 35pt"
						colspan="5" rowspan="3" vAlign="middle" align="center" height="20"><FONT face="굴림">발&nbsp; 
							&nbsp;주&nbsp;&nbsp; 서</FONT>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; HEIGHT: 19px" colspan="2" vAlign="middle" align="center"
						height="19">발주일자</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; HEIGHT: 19px"
						colspan="2" vAlign="middle" align="center" height="19">
						<asp:Label id="lbDate" runat="server"></asp:Label>&nbsp;</td>
				</tr>
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" width="40" rowspan="4"
						vAlign="middle" align="center" height="20">
						<P>발</P>
						<P>신</P>
					</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" width="110" vAlign="middle"
						align="center" height="20">업체명</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="2" vAlign="middle" align="center" height="20">韓 獨 機 械 (株)
					</td>
					<td style="BORDER-TOP: black 1px solid" width="40" rowspan="4" vAlign="middle" align="center"
						height="20">
						<P>수</P>
						<P>신</P>
					</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" width="40" vAlign="middle"
						align="center" height="20">업체명</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="2" vAlign="middle" align="left" height="20"><FONT face="굴림">&nbsp;</FONT>
						<asp:Label id="lbCompany" runat="server"></asp:Label>&nbsp;</td>
				</tr>
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">주소</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="2" vAlign="middle" align="center" height="20">경남 진주시 상평동 200-1
					</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">TEL</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="2" height="20">&nbsp;
						<asp:Label id="lbTEL" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">연락처</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" width="40" vAlign="middle"
						align="center" height="20">TEL</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" width="140" vAlign="middle"
						align="center" height="20">055) 755-9706
					</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" width="70" vAlign="middle"
						align="center" height="20">담당</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colSpan="4" height="20">&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:Label id="lbName" runat="server"></asp:Label></td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">FAX</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="2" height="20">&nbsp;
						<asp:Label id="lbFAX" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">FAX</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">055) 758-5742</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">e-mail</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colSpan="4" height="20">&nbsp;handok9706@hanmail.net&nbsp;&nbsp;&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">담당</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="2" height="20">&nbsp;
						<asp:Label id="lbPerson" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; FONT-SIZE: 1pt; BORDER-LEFT: black 1px solid"
						colspan="13" height="10">&nbsp;</td>
				</tr>
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="30">No</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" colspan="2" vAlign="middle"
						align="center" height="30">품명</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="30">품번</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="30">단위</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" width="70" vAlign="middle"
						align="center" height="30">차종</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="5" vAlign="middle" align="center" height="30">납기요구일 및 수량
					</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT-WIDTH: 1px; BORDER-LEFT-COLOR: black"
						width="100" vAlign="middle" align="center" height="30">합계</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						vAlign="middle" align="center" height="30">&nbsp;<FONT face="굴림">비고</FONT></td>
				</tr>
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20" width="40">&nbsp;
						<asp:Label id="Label1" runat="server"></asp:Label></td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" colspan="2" rowspan="2"
						vAlign="middle" align="center" height="20">
						<asp:Label id="Label2" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">
						<asp:TextBox id="Label3" runat="server" Width="120px" BorderStyle="None"></asp:TextBox>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label4" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">
						<asp:TextBox id="Label5" runat="server" Width="65px" BorderStyle="None"></asp:TextBox>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" width="80" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label6" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" width="80" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label8" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" width="80" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label10" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="2" width="80" vAlign="middle" align="center" height="20">
						<asp:Label id="Label12" runat="server"></asp:Label>&nbsp;</td>
					<td rowspan="2" align="right" vAlign="middle" style="BORDER-TOP: black 1px solid; BORDER-LEFT-WIDTH: 1px; BORDER-LEFT-COLOR: black">
						<asp:Label id="Label14" runat="server"></asp:Label>&nbsp;</td>
					<td rowspan="2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						vAlign="middle" align="center" height="20">&nbsp;</td>
				</tr>
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label7" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label9" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label11" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="2" vAlign="middle" align="right" height="20">
						<asp:Label id="Label13" runat="server"></asp:Label>&nbsp;</td>
				</tr>
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; HEIGHT: 39px"
						rowspan="2" vAlign="middle" align="center" height="39" width="40">&nbsp;
						<asp:Label id="Label15" runat="server"></asp:Label></td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; HEIGHT: 39px"
						colspan="2" rowspan="2" vAlign="middle" align="center" height="39">
						<asp:Label id="Label16" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; HEIGHT: 39px"
						rowspan="2" vAlign="middle" align="center" height="39">
						<asp:TextBox id="Label17" runat="server" Width="120px" BorderStyle="None"></asp:TextBox>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; HEIGHT: 39px"
						rowspan="2" vAlign="middle" align="center" height="39">
						<asp:Label id="Label18" runat="server"></asp:Label><FONT face="굴림">&nbsp;</FONT></td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; HEIGHT: 39px"
						rowspan="2" vAlign="middle" align="center" height="39">
						<asp:TextBox id="Label19" runat="server" Width="65px" BorderStyle="None"></asp:TextBox>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label20" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label22" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label24" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="2" vAlign="middle" align="center" height="20">
						<asp:Label id="Label26" runat="server"></asp:Label>&nbsp;</td>
					<td rowspan="2" align="right" vAlign="middle" style="BORDER-TOP: black 1px solid; BORDER-LEFT-WIDTH: 1px; BORDER-LEFT-COLOR: black; HEIGHT: 39px">
						<asp:Label id="Label28" runat="server"></asp:Label>&nbsp;</td>
					<td rowspan="2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; HEIGHT: 39px"
						vAlign="middle" align="center" height="39">&nbsp;</td>
				</tr>
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; HEIGHT: 19px"
						vAlign="middle" align="right" height="19">
						<asp:Label id="Label21" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; HEIGHT: 19px"
						vAlign="middle" align="right" height="19">
						<asp:Label id="Label23" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; HEIGHT: 19px"
						vAlign="middle" align="right" height="19">
						<asp:Label id="Label25" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; HEIGHT: 19px"
						colspan="2" vAlign="middle" align="right" height="19">
						<asp:Label id="Label27" runat="server"></asp:Label>&nbsp;</td>
				</tr>
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20" width="40">&nbsp;
						<asp:Label id="Label29" runat="server"></asp:Label></td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" colspan="2" rowspan="2"
						vAlign="middle" align="center" height="20">
						<asp:Label id="Label30" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">&nbsp;
						<asp:TextBox id="Label31" runat="server" Width="120px" BorderStyle="None"></asp:TextBox></td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label32" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">&nbsp;
						<asp:TextBox id="Label33" runat="server" Width="65px" BorderStyle="None"></asp:TextBox></td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label34" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label36" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label38" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="2" vAlign="middle" align="center" height="20">
						<asp:Label id="Label40" runat="server"></asp:Label>&nbsp;</td>
					<td rowspan="2" align="right" vAlign="middle" style="BORDER-TOP: black 1px solid; BORDER-LEFT-WIDTH: 1px; BORDER-LEFT-COLOR: black">
						<asp:Label id="Label42" runat="server"></asp:Label>&nbsp;</td>
					<td rowspan="2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						vAlign="middle" align="center" height="20">&nbsp;</td>
				</tr>
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label35" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label37" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label39" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="2" vAlign="middle" align="right" height="20">
						<asp:Label id="Label41" runat="server"></asp:Label>&nbsp;</td>
				</tr>
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20" width="40"><FONT face="굴림">&nbsp;</FONT>
						<asp:Label id="Label43" runat="server"></asp:Label></td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" colspan="2" rowspan="2"
						vAlign="middle" align="center" height="20">
						<asp:Label id="Label44" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">
						<asp:TextBox id="Label45" runat="server" Width="120px" BorderStyle="None"></asp:TextBox>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label46" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">
						<asp:TextBox id="Label47" runat="server" Width="65px" BorderStyle="None"></asp:TextBox>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label48" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label50" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label52" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="2" vAlign="middle" align="center" height="20">
						<asp:Label id="Label54" runat="server"></asp:Label>&nbsp;</td>
					<td rowspan="2" align="right" vAlign="middle" style="BORDER-TOP: black 1px solid; BORDER-LEFT-WIDTH: 1px; BORDER-LEFT-COLOR: black">
						<asp:Label id="Label56" runat="server"></asp:Label>&nbsp;</td>
					<td rowspan="2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						vAlign="middle" align="center" height="20">&nbsp;</td>
				</tr>
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label49" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label51" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label53" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="2" vAlign="middle" align="right" height="20">
						<asp:Label id="Label55" runat="server"></asp:Label>&nbsp;</td>
				</tr>
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20" width="40"><FONT face="굴림">&nbsp;</FONT>
						<asp:Label id="Label57" runat="server"></asp:Label></td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" colspan="2" rowspan="2"
						vAlign="middle" align="center" height="20">
						<asp:Label id="Label58" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">
						<asp:TextBox id="Label59" runat="server" Width="120px" BorderStyle="None"></asp:TextBox>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label60" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">
						<asp:TextBox id="Label61" runat="server" Width="65px" BorderStyle="None"></asp:TextBox>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label62" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label64" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label66" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="2" vAlign="middle" align="center" height="20">
						<asp:Label id="Label68" runat="server"></asp:Label>&nbsp;</td>
					<td rowspan="2" align="right" vAlign="middle" style="BORDER-TOP: black 1px solid; BORDER-LEFT-WIDTH: 1px; BORDER-LEFT-COLOR: black">
						<asp:Label id="Label70" runat="server"></asp:Label>&nbsp;</td>
					<td rowspan="2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						vAlign="middle" align="center" height="20">&nbsp;</td>
				</tr>
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label63" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label65" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label67" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="2" vAlign="middle" align="right" height="20">
						<asp:Label id="Label69" runat="server"></asp:Label>&nbsp;</td>
				</tr>
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20" width="40"><FONT face="굴림">&nbsp;</FONT>
						<asp:Label id="Label71" runat="server"></asp:Label></td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" colspan="2" rowspan="2"
						vAlign="middle" align="center" height="20">
						<asp:Label id="Label72" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">&nbsp;
						<asp:TextBox id="Label73" runat="server" Width="120px" BorderStyle="None"></asp:TextBox></td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label74" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">
						<asp:TextBox id="Label75" runat="server" Width="65px" BorderStyle="None"></asp:TextBox><FONT face="굴림">&nbsp;</FONT></td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label76" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label78" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label80" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="2" vAlign="middle" align="center" height="20">
						<asp:Label id="Label82" runat="server"></asp:Label>&nbsp;</td>
					<td rowspan="2" align="right" vAlign="middle" style="BORDER-TOP: black 1px solid; BORDER-LEFT-WIDTH: 1px; BORDER-LEFT-COLOR: black">
						<asp:Label id="Label84" runat="server"></asp:Label>&nbsp;</td>
					<td rowspan="2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						vAlign="middle" align="center" height="20">&nbsp;</td>
				</tr>
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label77" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label79" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label81" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="2" vAlign="middle" align="right" height="20">
						<asp:Label id="Label83" runat="server"></asp:Label>&nbsp;</td>
				</tr>
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20" width="40"><FONT face="굴림">&nbsp;</FONT>
						<asp:Label id="Label85" runat="server"></asp:Label></td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" colspan="2" rowspan="2"
						vAlign="middle" align="center" height="20">
						<asp:Label id="Label86" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">
						<asp:TextBox id="Label87" runat="server" Width="120px" BorderStyle="None"></asp:TextBox>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label88" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">
						<asp:TextBox id="Label89" runat="server" Width="65px" BorderStyle="None"></asp:TextBox>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label90" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label92" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label94" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="2" vAlign="middle" align="center" height="20">
						<asp:Label id="Label96" runat="server"></asp:Label>&nbsp;</td>
					<td rowspan="2" align="right" vAlign="middle" style="BORDER-TOP: black 1px solid; BORDER-LEFT-WIDTH: 1px; BORDER-LEFT-COLOR: black">
						<asp:Label id="Label98" runat="server"></asp:Label>&nbsp;</td>
					<td rowspan="2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						vAlign="middle" align="center" height="20">&nbsp;</td>
				</tr>
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label91" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label93" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label95" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="2" vAlign="middle" align="right" height="20">
						<asp:Label id="Label97" runat="server"></asp:Label>&nbsp;</td>
				</tr>
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20" width="40"><FONT face="굴림">&nbsp;</FONT>
						<asp:Label id="Label99" runat="server"></asp:Label></td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" colspan="2" rowspan="2"
						vAlign="middle" align="center" height="20">
						<asp:Label id="Label100" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">
						<asp:TextBox id="Label101" runat="server" Width="120px" BorderStyle="None"></asp:TextBox>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label102" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">
						<asp:TextBox id="Label103" runat="server" Width="65px" BorderStyle="None"></asp:TextBox>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label104" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label106" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label108" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="2" vAlign="middle" align="center" height="20">
						<asp:Label id="Label110" runat="server"></asp:Label>&nbsp;</td>
					<td rowspan="2" align="right" vAlign="middle" style="BORDER-TOP: black 1px solid; BORDER-LEFT-WIDTH: 1px; BORDER-LEFT-COLOR: black">
						<asp:Label id="Label112" runat="server"></asp:Label>&nbsp;</td>
					<td rowspan="2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						vAlign="middle" align="center" height="20">&nbsp;</td>
				</tr>
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label105" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label107" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label109" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="2" vAlign="middle" align="right" height="20">
						<asp:Label id="Label111" runat="server"></asp:Label>&nbsp;</td>
				</tr>
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20" width="40"><FONT face="굴림">&nbsp;</FONT>
						<asp:Label id="Label113" runat="server"></asp:Label></td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" colspan="2" rowspan="2"
						vAlign="middle" align="center" height="20">
						<asp:Label id="Label114" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">
						<asp:TextBox id="Label115" runat="server" Width="120px" BorderStyle="None"></asp:TextBox>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label116" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">
						<asp:TextBox id="Label117" runat="server" Width="65px" BorderStyle="None"></asp:TextBox>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label118" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label120" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label122" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="2" vAlign="middle" align="center" height="20">
						<asp:Label id="Label124" runat="server"></asp:Label>&nbsp;</td>
					<td rowspan="2" align="right" vAlign="middle" style="BORDER-TOP: black 1px solid; BORDER-LEFT-WIDTH: 1px; BORDER-LEFT-COLOR: black">
						<asp:Label id="Label126" runat="server"></asp:Label>&nbsp;</td>
					<td rowspan="2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						vAlign="middle" align="center" height="20">&nbsp;</td>
				</tr>
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label119" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label121" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label123" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="2" vAlign="middle" align="right" height="20">
						<asp:Label id="Label125" runat="server"></asp:Label>&nbsp;</td>
				</tr>
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20" width="40"><FONT face="굴림">&nbsp;</FONT>
						<asp:Label id="Label127" runat="server"></asp:Label></td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" colspan="2" rowspan="2"
						vAlign="middle" align="center" height="20">
						<asp:Label id="Label128" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">
						<asp:TextBox id="Label129" runat="server" Width="120px" BorderStyle="None"></asp:TextBox>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label130" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">
						<asp:TextBox id="Label131" runat="server" Width="65px" BorderStyle="None"></asp:TextBox>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label132" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label134" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">
						<asp:Label id="Label136" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="2" vAlign="middle" align="center" height="20">
						<asp:Label id="Label138" runat="server"></asp:Label>&nbsp;</td>
					<td rowspan="2" align="right" vAlign="middle" style="BORDER-TOP: black 1px solid; BORDER-LEFT-WIDTH: 1px; BORDER-LEFT-COLOR: black">
						<asp:Label id="Label140" runat="server"></asp:Label>&nbsp;</td>
					<td rowspan="2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						vAlign="middle" align="center" height="20">&nbsp;</td>
				</tr>
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label133" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label135" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label137" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="2" vAlign="middle" align="right" height="20">
						<asp:Label id="Label139" runat="server"></asp:Label>&nbsp;</td>
				</tr>
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20" width="40"><FONT face="굴림">計</FONT>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" colspan="2" rowspan="2"
						vAlign="middle" align="center" height="20">&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" rowspan="2" vAlign="middle"
						align="center" height="20">&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="center" height="20">&nbsp;</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="2" vAlign="middle" align="center" height="20">&nbsp;</td>
					<td rowspan="2" align="right" vAlign="middle" style="BORDER-TOP: black 1px solid; BORDER-LEFT-WIDTH: 1px; BORDER-LEFT-COLOR: black">
						<asp:Label id="Label145" runat="server"></asp:Label>&nbsp;</td>
					<td rowspan="2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						vAlign="middle" align="center" height="20">&nbsp;</td>
				</tr>
				<tr>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label141" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label142" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid" vAlign="middle"
						align="right" height="20">
						<asp:Label id="Label143" runat="server"></asp:Label>&nbsp;</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid"
						colspan="2" vAlign="middle" align="right" height="20">
						<asp:Label id="Label144" runat="server"></asp:Label>&nbsp;</td>
				</tr>
				<TR>
					<td style="BORDER-TOP: black 1px solid; PADDING-LEFT: 5px; FONT-WEIGHT: bold; FONT-SIZE: 10pt; BORDER-LEFT: black 1px solid; PADDING-TOP: 5px; BORDER-BOTTOM: black 1px solid"
						colSpan="9">
						특기사항<p>
							1. 본 발주서의 납기는 최종납기일이다.<br>
							2. 본 발주서상 미비한 사항은 필히 당사의 사전 승인을 득해야 한다.<br>
							3. 기타 사항은 당사와 체결된 거래 기본 계약서에 따른다.<br>
						</p>
					</td>
					<td style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; FONT-WEIGHT: bold; FONT-SIZE: 11pt; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
						colSpan="4" align="center">
						위와 같이 주문하오니<br>
						납품하여주시기 바랍니다<p>
							한독기계 (주)</p>
					</td>
				</TR>
				<TR>
					<TD colSpan="4" style="PADDING-LEFT: 20px; FONT-SIZE: 8pt; PADDING-TOP: 5px" height="10"><FONT face="굴림">양식 
							QP-0601-02(REV.1)</FONT></TD>
					<TD colSpan="5" style="PADDING-TOP: 5px" align="center" height="10"><FONT face="굴림">한독기계(주)</FONT></TD>
					<TD colSpan="4" style="FONT-SIZE: 8pt; PADDING-TOP: 5px" align="center" height="10"><FONT face="굴림">A4 
							(210 X 297)mm</FONT></TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
