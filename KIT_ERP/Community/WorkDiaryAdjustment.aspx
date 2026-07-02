<%@ Page language="c#" Codebehind="WorkDiaryAdjustment.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BuyingOutside.WorkDiaryAdjustment" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WorkDiaryAdjustment</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
		<script language="javascript">
<!--
		function Closing()			
		{
			if ( confirm("마감겠습니까?") )
				return true;
			else
				return false;
		}
//-->
		</script>
	</HEAD>
	<body bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<table style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px" borderColor="darkgray" cellSpacing="0" cellPadding="0" width="900" align="center" border="0">
				<TBODY>
					<tr height="10">
						<td width="20"></td>
						<td colSpan="3"><FONT face="굴림"></FONT></td>
					</tr>
					<tr height="27">
						<td width="20"></td>
						<td align="right" colSpan="3"><FONT face="굴림">
								<P style="FONT-SIZE: 15px; COLOR: red" align="left">
									<asp:Label id="Label1" runat="server" ForeColor="Red">Label</asp:Label>&nbsp;년
									<asp:Label id="Label2" runat="server" ForeColor="Red">Label</asp:Label>월&nbsp;
									<asp:Label id="Label3" runat="server" ForeColor="Red">Label</asp:Label>
								일&nbsp;</FONT></P></td>
					</tr>
					<TR>
						<td width="20"></td>
						<td vAlign="top" colSpan="3"><asp:datagrid id=DataGrid1 runat="server" PageSize="15" BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="0" GridLines="Horizontal" AutoGenerateColumns="False" AllowCustomPaging="True" Width="800px" DataSource="<%# dataSet11 %>" DataKeyField="CWorkDiaryIndex" BorderColor="Gray">
								<SelectedItemStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#339966"></SelectedItemStyle>
								<EditItemStyle HorizontalAlign="Left"></EditItemStyle>
								<AlternatingItemStyle HorizontalAlign="Center"></AlternatingItemStyle>
								<ItemStyle HorizontalAlign="Center" Height="32px" ForeColor="#333333" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="ControlDarkDark" BackColor="LightGray"></HeaderStyle>
								<FooterStyle Font-Bold="True" ForeColor="Red"></FooterStyle>
								<Columns>
									<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="확인" HeaderText="선 택" CancelText="취소" EditText="수정">
										<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
										<ItemStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="Red"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:EditCommandColumn>
									<asp:BoundColumn Visible="False" DataField="CWorkDiaryIndex" SortExpression="CWorkDiaryIndex" HeaderText="CWorkDiaryIndex"></asp:BoundColumn>
									<asp:BoundColumn DataField="Name" ReadOnly="True" HeaderText="이 름">
										<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
										<ItemStyle Font-Bold="True" HorizontalAlign="Center"></ItemStyle>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="MorningWork" HeaderText="오전업무 내용">
										<HeaderStyle Width="20%"></HeaderStyle>
										<ItemTemplate>
											<asp:TextBox id=MorningWork runat="server" Width="100%" BorderStyle="None" Text='<%#DataBinder.Eval(Container.DataItem,"MorningWork") %>' Height="80px" TextMode="MultiLine" ReadOnly="True">
											</asp:TextBox>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id=TextBox1 Width="100%" Text='<%#DataBinder.Eval(Container.DataItem,"MorningWork") %>' Height="80px" TextMode="MultiLine" Runat="server">
											</asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="AfternoonWork" HeaderText="오후업무 내용">
										<HeaderStyle Width="20%"></HeaderStyle>
										<ItemTemplate>
											<FONT face="굴림">
												<asp:TextBox id=AfternoonWork runat="server" Width="100%" BorderStyle="None" Text='<%#DataBinder.Eval(Container.DataItem,"AfternoonWork") %>' Height="80px" TextMode="MultiLine" ReadOnly="True">
												</asp:TextBox></FONT>
										</ItemTemplate>
										<EditItemTemplate>
											<FONT face="굴림">
												<asp:TextBox id=TextBox2 runat="server" Width="100%" Text='<%#DataBinder.Eval(Container.DataItem,"AfternoonWork") %>' Height="80px" TextMode="MultiLine">
												</asp:TextBox></FONT>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="NightWork" HeaderText="야간업무 내용">
										<HeaderStyle Width="20%"></HeaderStyle>
										<ItemTemplate>
											<FONT face="굴림">
												<asp:TextBox id=NightWork runat="server" Width="100%" BorderStyle="None" Text='<%#DataBinder.Eval(Container.DataItem,"NightWork") %>' Height="80px" TextMode="MultiLine" ReadOnly="True">
												</asp:TextBox></FONT>
										</ItemTemplate>
										<EditItemTemplate>
											<FONT face="굴림">
												<asp:TextBox id=TextBox3 runat="server" Width="100%" Text='<%#DataBinder.Eval(Container.DataItem,"NightWork") %>' Height="80px" TextMode="MultiLine">
												</asp:TextBox></FONT>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="TomorrowWork" HeaderText="내일계획">
										<HeaderStyle Width="20%"></HeaderStyle>
										<ItemTemplate>
											<FONT face="굴림">
												<asp:TextBox id=TomorrowWork runat="server" Width="100%" BorderStyle="None" Text='<%#DataBinder.Eval(Container.DataItem,"TomorrowWork") %>' Height="80px" TextMode="MultiLine" ReadOnly="True">
												</asp:TextBox></FONT>
										</ItemTemplate>
										<EditItemTemplate>
											<FONT face="굴림">
												<asp:TextBox id=TextBox4 runat="server" Width="100%" Text='<%#DataBinder.Eval(Container.DataItem,"TomorrowWork") %>' Height="80px" TextMode="MultiLine">
												</asp:TextBox></FONT>
										</EditItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn Visible="False" DataField="Id" SortExpression="Id" HeaderText="Id"></asp:BoundColumn>
								</Columns>
								<PagerStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="ControlDarkDark" BackColor="LightGray" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></td>
					</TR>
					<tr height="31">
						<td width="20"></td>
						<td align="left" width="300"><FONT face="굴림">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								<asp:LinkButton id="LinkButton1" runat="server">목록으로</asp:LinkButton>
							</FONT>
						</td>
						<td align="middle" width="300"></td>
						<td width="300"><FONT face="굴림">
								<P align="right">
									<asp:Button id="Button1" runat="server" Width="60px" Height="20px" Enabled="False" Text="마  감" CausesValidation="False"></asp:Button>&nbsp;&nbsp;&nbsp;</P>
							</FONT>
						</td>
					</tr>
				</TBODY>
			</table>
		</form>
	</body>
</HTML>
