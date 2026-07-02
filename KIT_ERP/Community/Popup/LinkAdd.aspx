<%@ Page language="c#" Codebehind="LinkAdd.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.Popup.LinkAdd" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>즐겨찾기 추가</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body onload="document.Form1.txtSiteName.focus();">
		<form id="Form1" method="post" runat="server">
			<FONT face="굴림">
				<TABLE id="Table1" style="FONT-SIZE: 10pt; Z-INDEX: 101; LEFT: 0px; WIDTH: 392px; POSITION: absolute; TOP: 0px; HEIGHT: 133px"
					borderColor="white" cellSpacing="0" cellPadding="0" width="392" bgColor="whitesmoke"
					border="1">
					<TR>
						<TD align="center" colSpan="2"><asp:label id="Label1" runat="server" Font-Size="13pt" Font-Bold="True" ForeColor="SteelBlue">즐겨찾기 추가</asp:label></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 80px" align="center"><asp:label id="Label2" runat="server" Font-Bold="True">Category</asp:label></TD>
						<TD style="FONT-SIZE: 10pt"><asp:dropdownlist id="DropDownList1" runat="server"></asp:dropdownlist>&nbsp;<FONT style="FONT-WEIGHT: bold; FONT-SIZE: 9pt" color="#ff0000">&nbsp;&nbsp;&nbsp;<FONT color="blue">링크주소 
									입력시</FONT> 'http://'&nbsp;<FONT color="blue">생략</FONT></FONT></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 80px; HEIGHT: 25px" align="center"><asp:label id="Label3" runat="server" Font-Bold="True">사이트 명</asp:label></TD>
						<TD><asp:textbox id="txtSiteName" runat="server" Width="100%" Height="100%" BorderStyle="Groove"></asp:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 80px; HEIGHT: 25px" align="center"><asp:label id="Label4" runat="server" Font-Bold="True">링크주소</asp:label></TD>
						<TD><asp:textbox id="txtLink" runat="server" Width="100%" Height="100%" BorderStyle="Groove"></asp:textbox></TD>
					</TR>
					<TR>
						<TD align="right" colSpan="2"><asp:button id="Button1" runat="server" Width="60px" Height="20px" CausesValidation="False"
								Text="등  록"></asp:button><asp:button id="Button2" runat="server" Width="60px" Height="20px" CausesValidation="False"
								Text="취  소"></asp:button></TD>
					</TR>
				</TABLE>
			</FONT>
		</form>
	</body>
</HTML>
