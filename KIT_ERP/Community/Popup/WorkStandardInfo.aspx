<%@ Register TagPrefix="uc1" TagName="ItemSearch" Src="../ItemControl/ItemSearch.ascx" %>
<%@ Page language="c#" Codebehind="WorkStandardInfo.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.Popup.WorkStandardInfo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>작업표준 정보</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="LEFT: 0px; POSITION: absolute; TOP: 0px" cellSpacing="1" cellPadding="1"
				width="700" border="0">
				<TR>
					<TD align="center" height="30">
						<asp:Label id="Label1" runat="server" Font-Size="13pt" ForeColor="SteelBlue" Font-Bold="True"></asp:Label></TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center"><FONT face="굴림">
							<TABLE id="table2" cellSpacing="0" cellPadding="0" border="0">
								<TR>
									<TD align="left" width="600">
										<uc1:ItemSearch id="ISC1" runat="server"></uc1:ItemSearch></TD>
									<TD align="right" width="100">
										<asp:button id="btSearch" runat="server" Width="60px" Height="20px" Text="검  색"></asp:button></TD>
								</TR>
							</TABLE>
						</FONT>
					</TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 0pt; HEIGHT: 179px" vAlign="top" align="center">
						<asp:DataGrid id="DataGrid1" runat="server" Width="700px" AutoGenerateColumns="False" Font-Size="10pt"
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
								<asp:BoundColumn DataField="WorkStandardInfoIndex" HeaderText="Index No.">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ItemNum" HeaderText="품목번호">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ItemName" HeaderText="품목명"></asp:BoundColumn>
								<asp:BoundColumn DataField="ProcessSequenceNum" HeaderText="공정순서">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SmallClassificationName" HeaderText="공정명">
									<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="PriorityOrder" HeaderText="우선순위">
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
