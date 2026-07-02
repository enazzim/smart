<%@ Page language="c#" Codebehind="UserInfo.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.Popup.UserInfo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>사용자 정보</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 0px; WIDTH: 664px; POSITION: absolute; TOP: 0px; HEIGHT: 237px"
				cellSpacing="1" cellPadding="1" width="664" border="0">
				<TR>
					<TD align="center" height="30">
						<asp:Label id="Label1" runat="server" Font-Size="13pt" ForeColor="SteelBlue" Font-Bold="True"></asp:Label></TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 0pt; HEIGHT: 179px" vAlign="top" align="center">
						<asp:DataGrid id="DataGrid1" runat="server" Width="684px" AutoGenerateColumns="False" Font-Size="10pt"
							GridLines="Horizontal" AllowCustomPaging="True" AllowPaging="True">
							<ItemStyle Height="30px"></ItemStyle>
							<HeaderStyle Font-Bold="True" Height="25px" ForeColor="ControlDarkDark" BackColor="LightGray"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="선택">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									<ItemTemplate>
										<asp:CheckBox id="CheckBox1" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="UserInfoIndex" HeaderText="Index No.">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ID" HeaderText="아이디">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Name" HeaderText="이름">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PostCode" HeaderText="부서">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Telephone1" HeaderText="연락처">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="UserRank" HeaderText="사용자등급">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid></TD>
				</TR>
				<TR>
					<TD align="center">
						<asp:Button id="Button1" runat="server" Width="60px" Text="등  록" Height="20px"></asp:Button><FONT face="굴림">&nbsp;
						</FONT>
						<asp:Button id="Button2" runat="server" Width="60px" Text="창닫기" Height="20px"></asp:Button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
