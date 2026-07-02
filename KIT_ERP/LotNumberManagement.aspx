<%@ Page language="c#" Codebehind="LotNumberManagement.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.LotNumberManagement" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>LotNumberManagement</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<script language="javascript">
			function SubmitConfirm(){
			if(confirm("등록하시겠습니까?")) 
				return true;
			else 
				return false
			}
			function SubmitConfirm2(){
			if(confirm("종료하시겠습니까?")) 
				return true;
			else 
				return false
			}
			function SubmitConfirm3(){
			if(confirm("삭제하시겠습니까?")) 
				return true;
			else 
				return false
			}
			
			function CloseWindow(){
				window.opener.Lot(document.all['<%= tbLotSelect.ClientID %>'].value);self.close();
			}
		</script>
		<form id="Form1" method="post" runat="server">
			<FONT face="굴림">
				<asp:listbox id=lbLotNumber style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 32px" runat="server" DataValueField="ID" DataMember="LotLedger" DataTextField="LotNumber" DataSource="<%# dsLotNumber %>" Enabled="False" Height="288px" Width="320px">
				</asp:listbox><asp:textbox id="tbLotSelect" style="Z-INDEX: 114; LEFT: 416px; POSITION: absolute; TOP: 232px"
					runat="server" Enabled="False" Width="248px"></asp:textbox><asp:textbox id="tbLotIntegrate" style="Z-INDEX: 113; LEFT: 416px; POSITION: absolute; TOP: 112px"
					runat="server" Enabled="False" Width="248px"></asp:textbox><asp:textbox id="tbLotDivide" style="Z-INDEX: 112; LEFT: 416px; POSITION: absolute; TOP: 72px"
					runat="server" Enabled="False" Width="248px"></asp:textbox><asp:button id="btLotSelect" style="Z-INDEX: 110; LEFT: 336px; POSITION: absolute; TOP: 232px"
					runat="server" Width="72px" Text="Lot 선택"></asp:button><asp:button id="btLotDelete" style="Z-INDEX: 109; LEFT: 336px; POSITION: absolute; TOP: 192px"
					runat="server" Text="Lot 삭제"></asp:button><asp:button id="btLotEnd" style="Z-INDEX: 108; LEFT: 336px; POSITION: absolute; TOP: 152px"
					runat="server" Text="Lot 종료"></asp:button><asp:button id="btLotIntegrate" style="Z-INDEX: 107; LEFT: 336px; POSITION: absolute; TOP: 112px"
					runat="server" Text="Lot 통합"></asp:button><asp:button id="btLotDivide" style="Z-INDEX: 106; LEFT: 336px; POSITION: absolute; TOP: 72px"
					runat="server" Text="Lot 분할"></asp:button><asp:label id="Label1" style="Z-INDEX: 102; LEFT: 8px; POSITION: absolute; TOP: 8px" runat="server"
					Height="8px" Width="160px">Active Lot Numbers</asp:label><asp:button id="btListAll" style="Z-INDEX: 103; LEFT: 8px; POSITION: absolute; TOP: 320px" runat="server"
					Width="136px" Text="ListAll"></asp:button><asp:button id="btSelectCancel" style="Z-INDEX: 104; LEFT: 232px; POSITION: absolute; TOP: 320px"
					runat="server" Width="97px" Text="취소"></asp:button><asp:button id="btLotCreate" style="Z-INDEX: 105; LEFT: 336px; POSITION: absolute; TOP: 32px"
					runat="server" Text="Lot 생성"></asp:button><asp:textbox id="tbLotCreate" style="Z-INDEX: 111; LEFT: 416px; POSITION: absolute; TOP: 32px"
					runat="server" Enabled="False" Width="248px"></asp:textbox><asp:button id="btClose" style="Z-INDEX: 115; LEFT: 608px; POSITION: absolute; TOP: 320px" runat="server"
					Width="56px" Text="닫기"></asp:button><asp:button id="btConfirm" style="Z-INDEX: 116; LEFT: 336px; POSITION: absolute; TOP: 272px"
					runat="server" Height="32px" Width="328px" Text="확인"></asp:button></FONT></form>
	</body>
</HTML>
