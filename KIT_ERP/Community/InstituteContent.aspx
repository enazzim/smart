<%@ Page language="c#" Codebehind="InstituteContent.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.InstituteContent" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>InstituteContent</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../StyleSheet1.css">
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
			<TABLE style="Z-INDEX: 101; POSITION: absolute; TOP: 8px; LEFT: 8px" id="Table1" border="0"
				cellSpacing="0" cellPadding="0" width="800" align="center">
				<TR>
					<TD vAlign="bottom" width="20"><FONT face="굴림"></FONT></TD>
					<TD vAlign="bottom" colSpan="2" align="right"><asp:button id="btnReply" runat="server" Text="답   변" Width="65px" Height="20px"></asp:button><FONT face="굴림">&nbsp;
						</FONT>
					</TD>
				</TR>
				<TR>
					<TD width="20"></TD>
					<TD vAlign="top" colSpan="2">
						<TABLE style="WIDTH: 800px; TOP: 1px" id="Table2" border="1" cellSpacing="0" borderColor="#000000"
							borderColorLight="#ffffff" borderColorDark="dimgray" cellPadding="0" width="800" align="center">
							<TR>
								<TD style="WIDTH: 100px" bgColor="whitesmoke" height="30">
									<P align="center"><FONT style="FONT-WEIGHT: bold" face="굴림">작 성 자</FONT></P>
								</TD>
								<TD bgColor="whitesmoke" height="30" width="180">
									<P align="center"><asp:label id="lbWriter" runat="server" ForeColor="ControlDarkDark"></asp:label>&nbsp;</P>
								</TD>
								<TD bgColor="whitesmoke" height="30" width="100">
									<P align="center"><FONT face="굴림"><STRONG>작 성 일</STRONG>&nbsp;</FONT></P>
								</TD>
								<TD bgColor="whitesmoke" height="30" width="250" colSpan="3">
									<P align="center"><asp:label id="lblRegistrationDate" runat="server" ForeColor="ControlDarkDark"></asp:label></P>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" bgColor="whitesmoke" height="30" vAlign="middle">
									<P align="center"><FONT face="굴림"><STRONG>제&nbsp;&nbsp;&nbsp; 목</STRONG></FONT></P>
								</TD>
								<TD bgColor="whitesmoke" height="30" vAlign="middle" colSpan="6"><asp:textbox id="txtTitle" runat="server" Width="99.9%" Height="20px" Font-Size="9pt" BackColor="#EEEEE9"
										ReadOnly="True" BorderWidth="1px" BorderColor="Gray" BorderStyle="Inset" MaxLength="50"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" Display="None" ControlToValidate="txtTitle"
										ErrorMessage="제목을 입력해주세요!"></asp:requiredfieldvalidator></TD>
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
								<TD bgColor="#f5f5f5" height="30" colSpan="4"><INPUT style="BORDER-BOTTOM: gray 1px solid; BORDER-LEFT: gray 1px solid; BACKGROUND-COLOR: #eeeee9; WIDTH: 376px; HEIGHT: 20px; BORDER-TOP: gray 1px solid; BORDER-RIGHT: gray 1px solid"
										id="File1" size="43" type="file" name="File1" runat="server"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" bgColor="whitesmoke" height="200">
									<P align="center"><FONT face="굴림"><STRONG>내&nbsp;&nbsp;&nbsp; 용</STRONG></FONT></P>
								</TD>
								<TD bgColor="whitesmoke" height="200" colSpan="6"><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"><asp:textbox id="txtContents" runat="server" Width="100%" Height="200px" Font-Size="9pt" BackColor="#EEEEE9"
											ReadOnly="True" BorderWidth="1px" BorderColor="Gray" BorderStyle="Inset" TextMode="MultiLine" Rows="23"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" Display="None" ControlToValidate="txtContents"
											ErrorMessage="내용을 입력해주세요!"></asp:requiredfieldvalidator></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px" bgColor="#f5f5f5" height="30"><FONT face="굴림">
										<P align="center"><FONT face="굴림"><STRONG>필&nbsp; 독&nbsp; 자</STRONG></FONT></P>
									</FONT>
								</TD>
								<TD bgColor="#f5f5f5" height="30" width="180"><asp:listbox id="ListBox1" runat="server" Width="150px" Height="140px" BackColor="WhiteSmoke"></asp:listbox></TD>
								<TD bgColor="#f5f5f5" height="30" width="100"><FONT face="굴림">
										<P align="center"><FONT face="굴림"><STRONG>확 인 자</STRONG></FONT></P>
									</FONT>
								</TD>
								<TD bgColor="#f5f5f5" height="30" width="250" colSpan="3"><asp:datagrid id="DataGrid1" runat="server" Width="250px" BorderWidth="0px" BorderColor="White"
										ShowHeader="False" AutoGenerateColumns="False" CellPadding="5">
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
					<TD height="5" vAlign="middle" width="20"><FONT face="굴림"></FONT></TD>
					<TD height="28" vAlign="middle" width="440" align="left">
						<DIV align="left"><FONT face="굴림">&nbsp;</FONT>
							<asp:linkbutton id="lbkList" runat="server">목록</asp:linkbutton><FONT face="굴림">&nbsp;
								<asp:hyperlink id="hlnkPreContent" runat="server">윗글</asp:hyperlink>&nbsp;
								<asp:hyperlink id="hlnkNextContent" runat="server">아랫글</asp:hyperlink></FONT></DIV>
					</TD>
					<TD height="28" vAlign="middle" width="440" align="right"><asp:validationsummary id="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"
							DisplayMode="List"></asp:validationsummary><asp:button id="btnEdit" runat="server" Text="수   정" Width="65px" Height="20px" CausesValidation="False"
							Enabled="False"></asp:button><FONT face="굴림">&nbsp; </FONT>
						<asp:button id="btnDelete" runat="server" Text="삭   제" Width="65px" Height="20px" CausesValidation="False"
							Enabled="False"></asp:button></TD>
				</TR>
				<tr>
					<TD height="5" vAlign="middle" width="800" align="center"><FONT face="굴림"></FONT></TD>
					<TD height="28" vAlign="middle" width="800" colSpan="2" align="center"><FONT face="굴림">
							<FIELDSET style="BORDER-BOTTOM: #000000 1px solid; BORDER-LEFT: #000000 1px solid; WIDTH: 720px; HEIGHT: 88px; BORDER-TOP: #000000 1px solid; BORDER-RIGHT: #000000 1px solid"
								align="absBottom"><LEGEND>기준정보 참고자료 등록</LEGEND>
								<TABLE style="HEIGHT: 64px" id="Table3" border="0" cellSpacing="0" cellPadding="0" width="720">
									<TR>
										<TD style="HEIGHT: 20px" height="20" width="120" align="center"><asp:button id="btnCompanyInfo" runat="server" Text="거래처 정보" Width="120px" Height="20px" CausesValidation="False"
												Enabled="False"></asp:button></TD>
										<TD style="HEIGHT: 20px" height="20" width="120" align="center"><asp:button id="btnItemInfo" runat="server" Text="품 목 정 보" Width="120px" Height="20px" CausesValidation="False"
												Enabled="False"></asp:button></TD>
										<TD style="HEIGHT: 20px" height="20" width="120" align="center"><asp:button id="btnItemOrganizationInfo" runat="server" Text="품목구성 정보" Width="120px" Height="20px"
												CausesValidation="False" Enabled="False"></asp:button></TD>
										<TD style="HEIGHT: 20px" height="20" width="120" align="center"><asp:button id="btnEquipmentInfo" runat="server" Text="설 비 정 보" Width="120px" Height="20px"
												CausesValidation="False" Enabled="False"></asp:button></TD>
										<TD style="HEIGHT: 20px" height="20" width="120" align="center"><asp:button id="btnWCInfo" runat="server" Text="작업장 정보" Width="120px" Height="20px" CausesValidation="False"
												Enabled="False"></asp:button></TD>
										<TD width="120"><FONT face="굴림"><asp:button id="btnProcessSequenceInfo" runat="server" Text="공정순서 정보" Width="120px" Height="20px"
													CausesValidation="False" Enabled="False"></asp:button></FONT></TD>
									</TR>
									<TR>
										<TD style="HEIGHT: 4px" height="4" align="center"><FONT face="굴림"><asp:button id="btnWorkStandardInfo" runat="server" Text="작업표준 정보" Width="120px" Height="20px"
													CausesValidation="False" Enabled="False"></asp:button></FONT></TD>
										<TD style="HEIGHT: 4px" height="4" align="center"><FONT face="굴림"><asp:button id="btnSaleUnitCostInfo" runat="server" Text="판매단가 정보" Width="120px" Height="20px"
													CausesValidation="False" Enabled="False"></asp:button></FONT></TD>
										<TD style="HEIGHT: 4px" height="4" align="center"><asp:button id="btnOutSideOrderUnitCostInfo" runat="server" Text="외주단가 정보" Width="120px" Height="20px"
												CausesValidation="False" Enabled="False"></asp:button></TD>
										<TD style="HEIGHT: 4px" height="4" align="center"><asp:button id="btnBuyUnitCostInfo" runat="server" Text="구매단가 정보" Width="120px" Height="20px"
												CausesValidation="False" Enabled="False"></asp:button></TD>
										<TD style="HEIGHT: 4px" height="4" align="center"><asp:button id="btnUserInfo" runat="server" Text="사용자 정보" Width="120px" Height="20px" CausesValidation="False"
												Enabled="False"></asp:button></TD>
										<TD style="HEIGHT: 4px" width="10"><asp:button id="btnPublicUseCode" runat="server" Text="공용코드 정보" Width="120px" Height="20px"
												CausesValidation="False" Enabled="False"></asp:button></TD>
									</TR>
									<TR>
										<TD style="HEIGHT: 4px" height="4" align="center"><asp:button id="btnBusinessPlanInfo" runat="server" Text="사업계획 정보" Width="120px" Height="20px"
												CausesValidation="False" Enabled="False"></asp:button></TD>
										<TD style="HEIGHT: 4px" height="4" align="center"><asp:button id="btnExecutionPlanInfo" runat="server" Text="실행계획 정보" Width="120px" Height="20px"
												CausesValidation="False" Enabled="False"></asp:button></TD>
										<TD style="HEIGHT: 4px" height="4" align="center"><asp:button id="btnRealProcessSequenceInfo" runat="server" Text="진공정정보" Width="120px" Height="20px"
												CausesValidation="False" Enabled="False"></asp:button></TD>
										<TD style="HEIGHT: 4px" height="4" align="center"><asp:button id="btnRealItemOrganizationInfo" runat="server" Text="진품목구성정보" Width="120px" Height="20px"
												CausesValidation="False" Enabled="False"></asp:button></TD>
										<TD style="HEIGHT: 4px" height="4" align="center"><asp:button id="btnRealWorkStandardInfo" runat="server" Text="진작업표준정보" Width="120px" Height="20px"
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
					<TD vAlign="middle" width="800" colSpan="3"><asp:datagrid id="Datagrid2" runat="server" Width="800px" Height="24px" AutoGenerateColumns="False">
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
						</asp:datagrid></TD>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
