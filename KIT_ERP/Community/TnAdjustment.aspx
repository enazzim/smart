<%@ Page language="c#" Codebehind="TnAdjustment.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Community.TnAdjustment" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>기술자료실-Adjustment</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
			if ( confirm("수정겠습니까?") )
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
			<TABLE id="Table1" style="LEFT: 10px; POSITION: absolute; TOP: 10px" height="550" cellSpacing="0"
				cellPadding="0" width="800" align="center" border="0">
				<TR>
					<TD style="FONT-SIZE: 0pt; HEIGHT: 323px" vAlign="top" width="800" colSpan="2" height="323">
						<TABLE id="Table2" borderColor="#000000" cellSpacing="0" borderColorDark="dimgray" cellPadding="0"
							width="800" borderColorLight="#ffffff" border="1">
							<TR>
								<TD width="100" bgColor="whitesmoke" height="30">
									<P align="center"><FONT face="굴림"><STRONG>작 성 자</STRONG></FONT></P>
								</TD>
								<TD width="100" bgColor="whitesmoke" height="30">
									<P align="center"><asp:label id="lblRegistrationPerson" runat="server" Width="76px" ForeColor="ControlDarkDark"></asp:label></P>
								</TD>
								<TD style="WIDTH: 100px" width="100" bgColor="whitesmoke" height="30">
									<P align="center"><FONT face="굴림"><STRONG>작 성 일</STRONG></FONT></P>
								</TD>
								<TD width="250" bgColor="whitesmoke" colSpan="2" height="30">
									<P align="center"><asp:label id="lblRegistrationDate" runat="server" ForeColor="ControlDarkDark"></asp:label></P>
								</TD>
								<TD style="WIDTH: 106px" width="106" bgColor="whitesmoke" height="30">
									<P align="center"><FONT face="굴림"><STRONG>조 회 수</STRONG></FONT></P>
								</TD>
								<TD width="150" bgColor="whitesmoke" height="30">
									<P align="center"><asp:label id="lblHits" runat="server" ForeColor="ControlDarkDark"></asp:label></P>
								</TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 20px" vAlign="middle" width="100" bgColor="#f5f5f5" height="20"><FONT face="굴림">
										<P align="center"><FONT face="굴림"><STRONG>제&nbsp;&nbsp;&nbsp; 목</STRONG></FONT></P>
									</FONT>
								</TD>
								<TD style="WIDTH: 272px; HEIGHT: 20px" vAlign="middle" width="272" bgColor="#f5f5f5"
									colSpan="4" height="20"><FONT face="굴림"><asp:textbox id="txtTitle" runat="server" Width="100%" BackColor="#EEEEE9" Font-Size="9pt" ReadOnly="True"
											BorderWidth="1px" BorderStyle="Inset" BorderColor="Gray" Height="20px" MaxLength="50"></asp:textbox><FONT face="굴림"><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="제목을 입력해주세요!" ControlToValidate="txtTitle"
												Display="None"></asp:requiredfieldvalidator></FONT></FONT></TD>
								<TD style="WIDTH: 106px; HEIGHT: 20px" vAlign="middle" width="106" bgColor="#f5f5f5"
									height="20"><FONT face="굴림">
										<P align="center"><FONT face="굴림"><STRONG>Category</STRONG></FONT></P>
									</FONT>
								</TD>
								<TD style="HEIGHT: 20px" vAlign="middle" width="250" bgColor="#f5f5f5" height="20"><FONT face="굴림"><asp:dropdownlist id="DropDownList1" runat="server" BackColor="#EEEEE9"></asp:dropdownlist></FONT></TD>
							</TR>
							<TR>
								<TD vAlign="middle" align="center" width="100" bgColor="whitesmoke" height="30"><FONT face="굴림"><STRONG>첨부파일</STRONG></FONT></TD>
								<TD style="WIDTH: 276px" vAlign="middle" align="left" width="200" bgColor="whitesmoke"
									colSpan="2" height="30"><FONT face="굴림">&nbsp;
										<asp:hyperlink id="HyperLink1" runat="server" Font-Bold="True">HyperLink</asp:hyperlink><asp:label id="Label7" runat="server" ForeColor="Blue"></asp:label></FONT></TD>
								<td vAlign="middle" align="left" width="500" bgColor="whitesmoke" colSpan="4" height="30"><FONT face="굴림">&nbsp;<STRONG>첨부파일변경:</STRONG><INPUT id="File1" style="BORDER-RIGHT: gray 1px solid; BORDER-TOP: gray 1px solid; BORDER-LEFT: gray 1px solid; WIDTH: 345px; BORDER-BOTTOM: gray 1px solid; HEIGHT: 20px; BACKGROUND-COLOR: #eeeee9"
											type="file" size="38" name="File1" runat="server"></FONT></td>
							</TR>
							<TR>
								<TD width="100" bgColor="whitesmoke" height="100">
									<P align="center"><FONT face="굴림"><STRONG>내&nbsp;&nbsp;&nbsp;&nbsp;용</STRONG></FONT></P>
								</TD>
								<TD width="700" bgColor="whitesmoke" colSpan="7" height="100"><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"><asp:textbox id="txtContents" runat="server" Width="700px" BackColor="#EEEEE9" Font-Size="9pt"
											ReadOnly="True" BorderWidth="1px" BorderStyle="Inset" BorderColor="Gray" Height="250px" Rows="23" TextMode="MultiLine"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="내용을 입력해주세요!" ControlToValidate="txtContents"
											Display="None"></asp:requiredfieldvalidator></FONT></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr align="right">
					<td vAlign="middle" align="left" width="400">&nbsp;<asp:linkbutton id="lbkList" runat="server">목록</asp:linkbutton>&nbsp;
						<asp:hyperlink id="hlnkPreContent" runat="server">윗글</asp:hyperlink>&nbsp;
						<asp:hyperlink id="hlnkNextContent" runat="server">아랫글</asp:hyperlink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
					<td vAlign="middle" align="right" width="400" height="20"><br>
						<asp:validationsummary id="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"
							DisplayMode="List"></asp:validationsummary>&nbsp;
						<asp:button id="btnEdit" runat="server" Width="65px" Height="20px" Enabled="False" Text="수   정"
							CausesValidation="False"></asp:button>&nbsp;
						<asp:button id="btnDelete" runat="server" Width="65px" Height="20px" Enabled="False" Text="삭   제"
							CausesValidation="False"></asp:button>&nbsp;
					</td>
				</tr>
				<TR>
					<TD style="HEIGHT: 110px" align="left" width="800" colSpan="2"><FONT face="굴림"></FONT><FONT face="굴림"></FONT><br>
						<FIELDSET style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; BORDER-LEFT: #000000 1px solid; WIDTH: 800px; BORDER-BOTTOM: #000000 1px solid; HEIGHT: 88px"
							align="absBottom"><LEGEND>기준정보 참고자료 등록</LEGEND>
							<TABLE id="Table3" style="HEIGHT: 64px" cellSpacing="0" cellPadding="0" width="800" border="0">
								<TR>
									<td style="HEIGHT: 20px" width="10"><FONT face="굴림"></FONT></td>
									<TD style="HEIGHT: 20px" align="center" height="20"><asp:button id="btnCompanyInfo" runat="server" Width="155px" Height="20px" Enabled="False" Text="거래처 정보"
											CausesValidation="False"></asp:button></TD>
									<TD style="HEIGHT: 20px" align="center" height="20"><asp:button id="btnItemInfo" runat="server" Width="155px" Height="20px" Enabled="False" Text="품 목 정 보"
											CausesValidation="False"></asp:button></TD>
									<TD style="HEIGHT: 20px" align="center" height="20"><asp:button id="btnItemOrganizationInfo" runat="server" Width="155px" Height="20px" Enabled="False"
											Text="품목구성 정보" CausesValidation="False"></asp:button></TD>
									<TD style="HEIGHT: 20px" align="center" height="20"><asp:button id="btnEquipmentInfo" runat="server" Width="155px" Height="20px" Enabled="False"
											Text="설 비 정 보" CausesValidation="False"></asp:button></TD>
									<TD style="HEIGHT: 20px" align="center" height="20"><asp:button id="btnWCInfo" runat="server" Width="155px" Height="20px" Enabled="False" Text="작업장 정보"
											CausesValidation="False"></asp:button></TD>
									<td width="10"></td>
								</TR>
								<TR>
									<td width="10"></td>
									<TD align="center" height="21"><FONT face="굴림"><asp:button id="btnProcessSequenceInfo" runat="server" Width="155px" Height="20px" Enabled="False"
												Text="공정순서 정보" CausesValidation="False"></asp:button></FONT></TD>
									<TD align="center" height="21"><FONT face="굴림"><asp:button id="btnWorkStandardInfo" runat="server" Width="155px" Height="20px" Enabled="False"
												Text="작업표준 정보" CausesValidation="False"></asp:button></FONT></TD>
									<TD align="center" height="21"><asp:button id="btnSellUnitCostInfo" runat="server" Width="155px" Height="20px" Enabled="False"
											Text="판매단가 정보" CausesValidation="False"></asp:button></TD>
									<TD align="center" height="21"><asp:button id="btnOutSideOrderUnitCostInfo" runat="server" Width="155px" Height="20px" Enabled="False"
											Text="외주단가 정보" CausesValidation="False"></asp:button></TD>
									<TD align="center" height="21"><asp:button id="btnBuyUnitCostInfo" runat="server" Width="155px" Height="20px" Enabled="False"
											Text="구매단가 정보" CausesValidation="False"></asp:button></TD>
									<td width="10"></td>
								</TR>
								<TR>
									<td width="10"><FONT face="굴림"></FONT></td>
									<TD align="center" height="21"><asp:button id="btnUserInfo" runat="server" Width="155px" Height="20px" Enabled="False" Text="사용자 정보"
											CausesValidation="False"></asp:button></TD>
									<TD align="center" height="21"><asp:button id="btnPublicUseCode" runat="server" Width="155px" Height="20px" Enabled="False"
											Text="공용코드 정보" CausesValidation="False"></asp:button></TD>
									<TD align="center" height="21"><asp:button id="btnBusinessPlanInfo" runat="server" Width="155px" Height="20px" Enabled="False"
											Text="사업계획 정보" CausesValidation="False"></asp:button></TD>
									<TD align="center" height="21"><asp:button id="btnExecutionPlanInfo" runat="server" Width="155px" Height="20px" Enabled="False"
											Text="실행계획 정보" CausesValidation="False"></asp:button></TD>
									<TD align="center" height="21"><asp:button id="btnMonthlyClosingInfo" runat="server" Width="155px" Height="20px" Enabled="False"
											Text="월마감 정보" CausesValidation="False"></asp:button></TD>
									<td width="10"></td>
								</TR>
							</TABLE>
							<FONT style="FONT-SIZE: 0pt" face="굴림"></FONT>
						</FIELDSET>
					</TD>
				</TR>
				<tr>
					<td width="800" colspan="2" vAlign="top" align="center"><asp:datagrid id="DataGrid1" runat="server" Width="800px" Height="24px" AutoGenerateColumns="False">
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
						</asp:datagrid></td>
				</tr>
			</TABLE>
			<FONT face="굴림"></FONT>
		</form>
	</body>
</HTML>
