<%@ Page language="c#" Codebehind="PostSearch.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.SystemInfoManagement.PopupWindows.PostSearch" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>우편번호 검색</title>
		<meta content="False" name="vs_snapToGrid">
		<STYLE type="text/css">BODY { SCROLLBAR-FACE-COLOR: #ffffff; SCROLLBAR-HIGHLIGHT-COLOR: #ffffff; SCROLLBAR-SHADOW-COLOR: #ffffff; SCROLLBAR-3DLIGHT-COLOR: #808080; SCROLLBAR-ARROW-COLOR: #400040; SCROLLBAR-TRACK-COLOR: #ffffff; SCROLLBAR-DARKSHADOW-COLOR: #808080 }
		</STYLE>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
		<!--
			function Close(State, Post1, Post2, Address)
			{
				switch(State)
				{
					case "Post1" :
						window.dialogArguments.Form1.txtPost1.value = Post1;
						window.dialogArguments.Form1.txtPost2.value = Post2;
						window.dialogArguments.Form1.txtBusinessCompanyAddress.value = Address;
					break;
					case "Post2" :
						window.dialogArguments.Form1.txtPost3.value = Post1;
						window.dialogArguments.Form1.txtPost4.value = Post2;
						window.dialogArguments.Form1.txtBusinessBillAddress.value = Address;
					break;
					case "Post3" :
						window.dialogArguments.Form1.txtPost5.value = Post1;
						window.dialogArguments.Form1.txtPost6.value = Post2;
						window.dialogArguments.Form1.txtTaxBillAddress.value = Address;
					break;
				}
				window.close();
			}
		//-->
		</script>
	</HEAD>
	<body bgColor="#d4d0c8" MS_POSITIONING="GridLayout" onload="document.Form1.TextBox1.focus();">
		<base target="_self">
		<form id="Form1" method="post" runat="server">
			<asp:label id="Label1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" runat="server"
				Font-Bold="True" Font-Names="돋움" ForeColor="ControlDarkDark" Font-Size="10pt">주소지의 동(읍/면/리) 등을 입력하세요.<br><font color="blue">예) 상대동, 가좌동</asp:label><asp:listbox id="ListBox1" style="Z-INDEX: 102; LEFT: 8px; POSITION: absolute; TOP: 80px" runat="server"
				ForeColor="Black" Width="304px" Height="152px" Rows="10" AutoPostBack="True"></asp:listbox><asp:textbox id="TextBox1" style="Z-INDEX: 103; LEFT: 8px; POSITION: absolute; TOP: 48px" runat="server"
				ForeColor="ControlDarkDark" Font-Size="10pt" Width="184px" Height="20px" BorderColor="Black"></asp:textbox>
			<asp:Button id="Button1" style="Z-INDEX: 104; LEFT: 216px; POSITION: absolute; TOP: 48px" runat="server"
				Width="96px" Height="20px" Text="우편번호 찾기"></asp:Button></form>
	</body>
</HTML>
