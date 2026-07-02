<%@ Page language="c#" Codebehind="DevelopmentContent.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.DevelopmentContent" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DevelopmentContent</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../StyleSheet1.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!--
		function Delete_Check()
		{
			if ( confirm("해당 항목을 삭제 하시겠습니까?") )
				return true;
			else
				return false;
		}
		function Edit_Check()			
		{
			if ( confirm("수정페이지로 이동하시겠습니까?") )
				return true;
			else
				return false;
		}
		
		function ContentDelete_Check()
		{
			if(confirm("삭제하시겠습니까?"))
				return true;
			else
				return false;
		}
		//-->
		</script>
	</HEAD>
	<body bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; POSITION: absolute; TOP: 8px; LEFT: 8px" cellSpacing="0"
				cellPadding="0" width="800" align="center" border="0">
				<TR>
					<TD vAlign="bottom" width="20"><FONT face="굴림"></FONT></TD>
					<TD vAlign="bottom" align="right" colSpan="2"><asp:button id="btnReply" runat="server" Height="20px" Width="65px" Text="답   변"></asp:button><FONT face="굴림">&nbsp;
						</FONT>
					</TD>
				</TR>
				<TR>
					<TD width="20"></TD>
					<TD vAlign="top" colSpan="2">
						<TABLE id="Table2" style="WIDTH: 800px; TOP: 1px" borderColor="#000000" cellSpacing="0"
							borderColorDark="dimgray" cellPadding="0" width="800" align="center" borderColorLight="#ffffff"
							border="1">
							<TR>
								<TD style="WIDTH: 100px" bgColor="whitesmoke" height="30">
									<P align="center"><FONT style="FONT-WEIGHT: bold" face="굴림">작 성 자</FONT></P>
								</TD>
								<TD width="180" bgColor="whitesmoke" height="30">
									<P align="center"><asp:label id="lbWriter" runat="server" ForeColor="ControlDarkDark"></asp:label></P>
								</TD>
								<TD width="100" bgColor="whitesmoke" height="30">
									<P align="center"><FONT face="굴림"><STRONG>작 성 일</STRONG></FONT></P>
								</TD>
								<TD width="250" bgColor="whitesmoke" colSpan="3" height="30">
									<P align="center"><asp:label id="lblRegistrationDate" runat="server" ForeColor="ControlDarkDark"></asp:label></P>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" vAlign="middle" bgColor="whitesmoke" height="30">
									<P align="center"><FONT face="굴림"><STRONG>제&nbsp;&nbsp;&nbsp; 목</STRONG></FONT></P>
								</TD>
								<TD vAlign="middle" bgColor="whitesmoke" colSpan="6" height="30"><asp:textbox id="txtTitle" runat="server" Height="20px" Width="99.9%" MaxLength="50" BorderStyle="Inset"
										BorderColor="Gray" BorderWidth="1px" ReadOnly="True" BackColor="#EEEEE9" Font-Size="9pt"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="제목을 입력해주세요!" ControlToValidate="txtTitle"
										Display="None"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" bgColor="#f5f5f5" height="30"><FONT face="굴림">
										<P align="center"><FONT face="굴림"><STRONG>첨부파일</STRONG></FONT></P>
									</FONT>
								</TD>
								<TD style="WIDTH: 180px" bgColor="#f5f5f5" height="30"><FONT face="굴림"><asp:hyperlink id="HyperLink1" runat="server" Font-Bold="True">HyperLink</asp:hyperlink><asp:label id="Label1" runat="server"></asp:label></FONT></TD>
								<TD style="WIDTH: 100px" bgColor="#f5f5f5" height="30">
									<P align="center"><FONT face="굴림"><STRONG>파일변경</STRONG></FONT></P>
								</TD>
								<TD bgColor="#f5f5f5" colSpan="4" height="30"><INPUT id="File1" style="BORDER-BOTTOM: gray 1px solid; BORDER-LEFT: gray 1px solid; BACKGROUND-COLOR: #eeeee9; WIDTH: 376px; HEIGHT: 20px; BORDER-TOP: gray 1px solid; BORDER-RIGHT: gray 1px solid"
										type="file" size="43" name="File1" runat="server"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" bgColor="whitesmoke" height="200">
									<P align="center"><FONT face="굴림"><STRONG>내&nbsp;&nbsp;&nbsp; 용</STRONG></FONT></P>
								</TD>
								<TD bgColor="whitesmoke" colSpan="6" height="200"><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"><asp:textbox id="txtContents" runat="server" Height="200px" Width="100%" BorderStyle="Inset"
											BorderColor="Gray" BorderWidth="1px" ReadOnly="True" BackColor="#EEEEE9" Font-Size="9pt" Rows="23" TextMode="MultiLine"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="내용을 입력해주세요!" ControlToValidate="txtContents"
											Display="None"></asp:requiredfieldvalidator></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" bgColor="#f5f5f5" height="30"><FONT face="굴림">
										<P align="center"><FONT face="굴림"><STRONG>필&nbsp; 독&nbsp; 자</STRONG></FONT></P>
									</FONT>
								</TD>
								<TD width="180" bgColor="#f5f5f5" height="30"><asp:listbox id="ListBox1" runat="server" Height="140px" Width="150px" BackColor="WhiteSmoke"></asp:listbox></TD>
								<TD width="100" bgColor="#f5f5f5" height="30"><FONT face="굴림">
										<P align="center"><FONT face="굴림"><STRONG>확 인 자</STRONG></FONT></P>
									</FONT>
								</TD>
								<TD width="250" bgColor="#f5f5f5" colSpan="3" height="30"><asp:datagrid id="DataGrid1" runat="server" Width="250px" BorderColor="White" BorderWidth="0px"
										CellPadding="5" AutoGenerateColumns="False" ShowHeader="False">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<Columns>
											<asp:BoundColumn DataField="Name">
												<HeaderStyle Width="100px"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="ReadDate">
												<HeaderStyle Width="150px"></HeaderStyle>
											</asp:BoundColumn>
										</Columns>
									</asp:datagrid><FONT face="굴림"></FONT></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="middle" width="20" height="5"><FONT face="굴림"></FONT></TD>
					<TD vAlign="middle" align="left" width="440" height="28">
						<DIV align="left"><FONT face="굴림">&nbsp;</FONT>
							<asp:linkbutton id="lbkList" runat="server">목록</asp:linkbutton><FONT face="굴림">&nbsp;
								<asp:hyperlink id="hlnkPreContent" runat="server">윗글</asp:hyperlink>&nbsp;
								<asp:hyperlink id="hlnkNextContent" runat="server">아랫글</asp:hyperlink></FONT></DIV>
					</TD>
					<TD vAlign="middle" align="right" width="440" height="28"><asp:validationsummary id="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
							ShowSummary="False"></asp:validationsummary><asp:button id="btnEdit" runat="server" Height="20px" Width="65px" Text="수   정" Enabled="False"
							CausesValidation="False"></asp:button><FONT face="굴림">&nbsp; </FONT>
						<asp:button id="btnDelete" runat="server" Height="20px" Width="65px" Text="삭   제" Enabled="False"
							CausesValidation="False"></asp:button></TD>
				</TR>
				<tr>
					<TD vAlign="middle" width="800" height="5" align="center"><FONT face="굴림"></FONT></TD>
					<TD vAlign="middle" align="center" width="800" height="28" colSpan="2"><FONT face="굴림">
							<FIELDSET style="BORDER-BOTTOM: #000000 1px solid; BORDER-LEFT: #000000 1px solid; WIDTH: 720px; HEIGHT: 88px; BORDER-TOP: #000000 1px solid; BORDER-RIGHT: #000000 1px solid"
								align="absBottom"><LEGEND>기준정보 참고자료 등록</LEGEND>
								<TABLE id="Table3" style="HEIGHT: 64px" cellSpacing="0" cellPadding="0" width="720" border="0">
									<TR>
										<TD style="HEIGHT: 20px" align="center" width="120" height="20">
											<asp:button id="btnCompanyInfo" runat="server" Text="거래처 정보" Width="120px" Height="20px" CausesValidation="False"
												Enabled="False"></asp:button></TD>
										<TD style="HEIGHT: 20px" align="center" width="120" height="20">
											<asp:button id="btnItemInfo" runat="server" Text="품 목 정 보" Width="120px" Height="20px" CausesValidation="False"
												Enabled="False"></asp:button></TD>
										<TD style="HEIGHT: 20px" align="center" width="120" height="20">
											<asp:button id="btnItemOrganizationInfo" runat="server" Text="품목구성 정보" Width="120px" Height="20px"
												CausesValidation="False" Enabled="False"></asp:button></TD>
										<TD style="HEIGHT: 20px" align="center" width="120" height="20">
											<asp:button id="btnEquipmentInfo" runat="server" Text="설 비 정 보" Width="120px" Height="20px"
												CausesValidation="False" Enabled="False"></asp:button></TD>
										<TD style="HEIGHT: 20px" align="center" width="120" height="20">
											<asp:button id="btnWCInfo" runat="server" Text="작업장 정보" Width="120px" Height="20px" CausesValidation="False"
												Enabled="False"></asp:button></TD>
										<TD width="120"><FONT face="굴림">
												<asp:button id="btnProcessSequenceInfo" runat="server" Text="공정순서 정보" Width="120px" Height="20px"
													CausesValidation="False" Enabled="False"></asp:button></FONT></TD>
									</TR>
									<TR>
										<TD style="HEIGHT: 4px" align="center" height="4"><FONT face="굴림">
												<asp:button id="btnWorkStandardInfo" runat="server" Text="작업표준 정보" Width="120px" Height="20px"
													CausesValidation="False" Enabled="False"></asp:button></FONT></TD>
										<TD style="HEIGHT: 4px" align="center" height="4"><FONT face="굴림">
												<asp:button id="btnSaleUnitCostInfo" runat="server" Text="판매단가 정보" Width="120px" Height="20px"
													CausesValidation="False" Enabled="False"></asp:button></FONT></TD>
										<TD style="HEIGHT: 4px" align="center" height="4">
											<asp:button id="btnOutSideOrderUnitCostInfo" runat="server" Text="외주단가 정보" Width="120px" Height="20px"
												CausesValidation="False" Enabled="False"></asp:button></TD>
										<TD style="HEIGHT: 4px" align="center" height="4">
											<asp:button id="btnBuyUnitCostInfo" runat="server" Text="구매단가 정보" Width="120px" Height="20px"
												CausesValidation="False" Enabled="False"></asp:button></TD>
										<TD style="HEIGHT: 4px" align="center" height="4">
											<asp:button id="btnUserInfo" runat="server" Text="사용자 정보" Width="120px" Height="20px" CausesValidation="False"
												Enabled="False"></asp:button></TD>
										<TD style="HEIGHT: 4px" width="10">
											<asp:button id="btnPublicUseCode" runat="server" Text="공용코드 정보" Width="120px" Height="20px"
												CausesValidation="False" Enabled="False"></asp:button></TD>
									</TR>
									<TR>
										<TD style="HEIGHT: 4px" align="center" height="4">
											<asp:button id="btnBusinessPlanInfo" runat="server" Text="사업계획 정보" Width="120px" Height="20px"
												CausesValidation="False" Enabled="False"></asp:button></TD>
										<TD style="HEIGHT: 4px" align="center" height="4">
											<asp:button id="btnExecutionPlanInfo" runat="server" Text="실행계획 정보" Width="120px" Height="20px"
												CausesValidation="False" Enabled="False"></asp:button></TD>
										<TD style="HEIGHT: 4px" align="center" height="4">
											<asp:button id="btnRealProcessSequenceInfo" runat="server" Text="진공정정보" Width="120px" Height="20px"
												CausesValidation="False" Enabled="False"></asp:button></TD>
										<TD style="HEIGHT: 4px" align="center" height="4">
											<asp:button id="btnRealItemOrganizationInfo" runat="server" Text="진품목구성정보" Width="120px" Height="20px"
												CausesValidation="False" Enabled="False"></asp:button></TD>
										<TD style="HEIGHT: 4px" align="center" height="4">
											<asp:button id="btnRealWorkStandardInfo" runat="server" Text="진작업표준정보" Width="120px" Height="20px"
												CausesValidation="False" Enabled="False"></asp:button></TD>
										<TD style="HEIGHT: 4px" width="10"></TD>
									</TR>
								</TABLE>
								<FONT style="FONT-SIZE: 0pt" face="굴림"></FONT>
							</FIELDSET>
						</FONT>
					</TD>
				</tr>
				<tr>
					<TD vAlign="middle" width="800" colspan="3">
						<asp:datagrid id="Datagrid2" runat="server" Width="800px" Height="24px" AutoGenerateColumns="False">
							<ItemStyle Height="25px"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" Height="20px" ForeColor="ControlDarkDark"
								BackColor="LightGray"></HeaderStyle>
							<FooterStyle Font-Bold="True" ForeColor="Red"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="삭제">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
									<ItemTemplate>
										<asp:LinkButton id="LinkButton1" runat="server" Text="삭제" CausesValidation="false" CommandName="Delete"></asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="StandardinfoTableID" HeaderText="기준정보 Table명">
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SITIndex" HeaderText="기준정보 Table Index No.">
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CommunityDataTableID" HeaderText="자료실 Table명">
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="CDTIndex" HeaderText="자료실 Table Index No.">
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
						</asp:datagrid>
					</TD>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
