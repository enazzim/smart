<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="UserInfo.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.UserInfo" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics.WebUI.WebDataInput.v1.1, Version=1.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>UserInfo</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<STYLE type="text/css">BODY { SCROLLBAR-ARROW-COLOR: #400040; SCROLLBAR-FACE-COLOR: #ffffff; SCROLLBAR-DARKSHADOW-COLOR: #808080; SCROLLBAR-HIGHLIGHT-COLOR: #ffffff; SCROLLBAR-SHADOW-COLOR: #ffffff; SCROLLBAR-TRACK-COLOR: #ffffff; SCROLLBAR-3DLIGHT-COLOR: #808080 }
		</STYLE>
		<script language="javascript">
		function IDConfirm()
	    {
			//요청 URL + 입력텍스트 값
			var Url = "IDCheck.aspx?param=" + document.all["tb_ID"].value;
			//XML Request 객체 선언및 Open
			var xmlRequest = new ActiveXObject("Microsoft.XMLHTTP");
			xmlRequest.open("POST", Url, false);
			//헤더값 설정
			xmlRequest.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
			//요청
			xmlRequest.send(null);
			//요청한값 확인하기
			if (xmlRequest.ResponseText=="true")
				alert("사용할 수 있는 아이디 입니다.");
			else if(xmlRequest.ResponseText=="null")
				alert("아이디를 입력하세요.");
			else
				alert("이미 존재하는 아이디 입니다.");

		}
		function OK(text)
		{
			if(confirm(text + "하시겠습니까?"))
				return true;
			else
				return false;
		}
		function UltraWebGrid1_AfterSelectChangeHandler(gridName, id){
			//Add code to handle your event here.
			var row = igtbl_getRowById(id);
			UserInfo.lb_ReferenceTable.value = row.getCellFromKey("CommunityDataTableID").getValue();
			UserInfo.lb_ReferenceIndex.value = row.getCellFromKey("CDTIndex").getValue();
		}		
		</script>
	</HEAD>
	<body bgColor="#f7f6f6" MS_POSITIONING="GridLayout">
		<form id="UserInfo" method="post" encType="multipart/form-data" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; POSITION: absolute; TOP: 10px; LEFT: 10px" cellSpacing="0"
				cellPadding="0" width="800" border="0" height="550">
				<TR>
					<TD>
						<table style="WIDTH: 800px" height="540" cellSpacing="0" cellPadding="0">
							<tr>
								<td>
									<table id="a" height="20" cellSpacing="0" cellPadding="0">
										<TR>
											<td style="WIDTH: 70px" vAlign="middle" align="right" width="70" height="20">아이디선택&nbsp;</td>
											<td align="left" width="700" colSpan="7" vAlign="middle" height="20"><igcmbo:webcombo id="wc_IDChoice" runat="server" BorderStyle="Solid" BorderWidth="1px" SelBackColor="DarkBlue"
													BackColor="#EEEEE9" SelectedIndex="-1" BorderColor="DimGray" SelForeColor="White" Version="3.00" Editable="True" ForeColor="Black" Font-Size="10pt" Height="20px"
													Width="100px" DropImageXP1="/ig_common/WebGrid3/ig_cmboDown1.bmp" DropImageXP2="/ig_common/WebGrid3/ig_cmboDown1.bmp">
													<Columns>
														<igtbl:UltraGridColumn HeaderText="ID" Key="ID" Width="250px" BaseColumnName="ID"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="이름" Key="Name" Width="150px" BaseColumnName="Name"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="번호" Key="UserInfoIndex" Hidden="True" BaseColumnName="UserInfoIndex"></igtbl:UltraGridColumn>
													</Columns>
													<DropDownLayout DropdownWidth="420px" BorderCollapse="Separate" RowSelectors="No" RowHeightDefault="20px"
														HeaderClickAction="Select" AutoGenerateColumns="False" DropdownHeight="200px" TableLayout="Fixed"
														StationaryMargins="Header">
														<RowStyle BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="White">
															<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
														</RowStyle>
														<SelectedRowStyle ForeColor="White" BackColor="DarkBlue"></SelectedRowStyle>
														<HeaderStyle BorderStyle="Solid" BackColor="LightGray">
															<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
														</HeaderStyle>
														<FrameStyle Width="100%" Cursor="Default" BorderWidth="2px" Font-Size="10pt" Font-Names="Verdana"
															BorderStyle="Ridge" BackColor="Silver" Height="100%"></FrameStyle>
													</DropDownLayout>
													<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
												</igcmbo:webcombo><FONT face="굴림">&nbsp;&nbsp;
													<asp:label id="lb_Index" runat="server" Font-Size="10pt" Height="4px" Width="16px" Visible="False">0</asp:label></FONT></td>
											<td align="right" width="100" vAlign="middle" height="20"><INPUT id="hd_User" style="WIDTH: 30px; HEIGHT: 20px" type="hidden" size="1" name="lb_ReferenceTable"
													runat="server">
												<asp:linkbutton id="LinkButton1" runat="server">사용자보기</asp:linkbutton></td>
										</TR>
									</table>
									<FIELDSET style="BORDER-BOTTOM: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; HEIGHT: 500px; BORDER-TOP: dimgray 2px solid; BORDER-RIGHT: dimgray 2px solid"
										align="left"><LEGEND>[ 입 력 ]</LEGEND>
										<TABLE id="b" cellSpacing="0" cellPadding="0" width="800" border="0">
											<TR>
												<TD style="WIDTH: 60px" align="right" width="60"><FONT style="COLOR: red" face="굴림">*아이디&nbsp;</FONT></TD>
												<TD align="left" width="100"><asp:textbox id="tb_ID" runat="server" BorderStyle="Solid" BackColor="#EEEEE9" Font-Size="9pt"
														Width="75px" BorderColor="DimGray" BorderWidth="1px"></asp:textbox><INPUT style="HEIGHT: 20px" type="button" value="..." id="btCheck" onclick="IDConfirm()"></TD>
												<TD style="WIDTH: 196px" align="right" width="196"><FONT style="COLOR: red" face="굴림">*패스워드&nbsp;</FONT></TD>
												<TD style="WIDTH: 81px" align="left" width="81"><asp:textbox id="tb_Password" runat="server" BorderStyle="Solid" BackColor="#EEEEE9" Font-Size="9pt"
														Width="100px" TextMode="Password" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
												<TD style="WIDTH: 192px" align="right" width="192"><FONT face="굴림"></FONT></TD>
												<TD align="left" width="100"></TD>
												<TD style="WIDTH: 245px" align="right" width="245"></TD>
												<TD style="WIDTH: 104px" align="left" width="104"></TD>
												<TD width="100"></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 60px" align="right" width="60"><FONT style="COLOR: red" face="굴림">*이름&nbsp;</FONT></TD>
												<TD align="left" width="100"><asp:textbox id="tb_Name" runat="server" BorderStyle="Solid" BackColor="#EEEEE9" Font-Size="9pt"
														Width="100px" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
												<TD style="WIDTH: 196px" align="right" width="196"><FONT face="굴림">부서&nbsp;</FONT></TD>
												<TD style="WIDTH: 81px" align="left" width="81"><asp:dropdownlist id="dl_Post" runat="server" BackColor="#EEEEE9" Width="100px"></asp:dropdownlist></TD>
												<TD style="WIDTH: 192px" align="right" width="192"><FONT face="굴림">직책&nbsp;</FONT></TD>
												<TD align="left" width="100"><asp:textbox id="tb_Responsibility" runat="server" BorderStyle="Solid" BackColor="#EEEEE9" Font-Size="9pt"
														Width="100px" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
												<TD style="WIDTH: 245px" align="right" width="245"><FONT face="굴림">주민등록번호&nbsp;</FONT></TD>
												<TD align="left" width="100" colSpan="2"><asp:textbox id="tb_IdentificationNumber" runat="server" BorderStyle="Solid" BackColor="#EEEEE9"
														Font-Size="9pt" Width="160px" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 60px" align="right" width="60"><FONT style="COLOR: red" face="굴림">*연락처1&nbsp;</FONT></TD>
												<TD align="left" width="100"><asp:textbox id="tb_Telephone1" runat="server" BorderStyle="Solid" BackColor="#EEEEE9" Font-Size="9pt"
														Width="100px" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
												<TD style="WIDTH: 196px" align="right" width="196"><FONT face="굴림">연락처2&nbsp;</FONT></TD>
												<TD style="WIDTH: 81px" align="left" width="81"><asp:textbox id="tb_Telephone2" runat="server" BorderStyle="Solid" BackColor="#EEEEE9" Font-Size="9pt"
														Width="100px" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
												<TD style="WIDTH: 192px" align="right" width="192"><FONT face="굴림">주소&nbsp;</FONT></TD>
												<TD style="WIDTH: 276px" align="left" width="276" colSpan="3"><FONT size="1"><asp:textbox id="tb_Address" runat="server" BorderStyle="Solid" BackColor="#EEEEE9" Font-Size="9pt"
															Width="230px" BorderColor="DimGray" BorderWidth="1px"></asp:textbox><asp:button id="bt_Post1" runat="server" Font-Size="10pt" Height="20px" Width="20px" Text="‥·"></asp:button></FONT></TD>
												<TD style="HEIGHT: 75px" width="100" rowSpan="4"><asp:image id="im_Picture" runat="server" Height="90px" Width="70px"></asp:image></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 60px; HEIGHT: 19px" align="right" width="60"><FONT face="굴림">입사일&nbsp;</FONT></TD>
												<TD style="HEIGHT: 19px" align="left" width="100">
													<igtxt:WebDateTimeEdit id="wdt_EnterDate" runat="server" DataMode="EditModeText" Width="96px" BackColor="#EEEEE9">
														<SpinButtons Display="OnRight"></SpinButtons>
													</igtxt:WebDateTimeEdit></TD>
												<TD style="WIDTH: 196px; HEIGHT: 19px" align="right" width="196"><FONT face="굴림">HP쓰기권한&nbsp;</FONT></TD>
												<TD style="WIDTH: 81px; HEIGHT: 19px" align="left" width="81"><asp:dropdownlist id="dl_HomepageCompetence" runat="server" BackColor="#EEEEE9" Width="88px">
														<asp:ListItem Value="0">아니오</asp:ListItem>
														<asp:ListItem Value="1">예</asp:ListItem>
													</asp:dropdownlist></TD>
												<TD style="WIDTH: 192px; HEIGHT: 19px" align="right" width="192"><FONT face="굴림">등록권한&nbsp;</FONT></TD>
												<TD style="HEIGHT: 19px" align="left" width="100"><asp:dropdownlist id="dl_StandardiInfoCompetence" runat="server" BackColor="#EEEEE9" Width="80px">
														<asp:ListItem Value="0">아니오</asp:ListItem>
														<asp:ListItem Value="1">예</asp:ListItem>
													</asp:dropdownlist></TD>
												<TD style="WIDTH: 245px; HEIGHT: 19px" align="right" width="245"><FONT style="COLOR: red" face="굴림">*사용권한&nbsp;</FONT></TD>
												<TD style="WIDTH: 104px; HEIGHT: 19px" align="left" width="104"><asp:dropdownlist id="dl_UserRank" runat="server" BackColor="#EEEEE9" Width="80px">
														<asp:ListItem Value="00 권한">00 권한</asp:ListItem>
														<asp:ListItem Value="01 권한">01 권한</asp:ListItem>
														<asp:ListItem Value="02 권한">02 권한</asp:ListItem>
														<asp:ListItem Value="03 권한">03 권한</asp:ListItem>
														<asp:ListItem Value="04 권한">04 권한</asp:ListItem>
														<asp:ListItem Value="05 권한">05 권한</asp:ListItem>
														<asp:ListItem Value="06 권한">06 권한</asp:ListItem>
														<asp:ListItem Value="07 권한">07 권한</asp:ListItem>
														<asp:ListItem Value="08 권한">08 권한</asp:ListItem>
														<asp:ListItem Value="09 권한">09 권한</asp:ListItem>
														<asp:ListItem Value="10 권한">10 권한</asp:ListItem>
														<asp:ListItem Value="11 권한">11 권한</asp:ListItem>
														<asp:ListItem Value="12 권한">12 권한</asp:ListItem>
														<asp:ListItem Value="13 권한">13 권한</asp:ListItem>
														<asp:ListItem Value="14 권한">14 권한</asp:ListItem>
														<asp:ListItem Value="15 권한">15 권한</asp:ListItem>
														<asp:ListItem Value="16 권한">16 권한</asp:ListItem>
														<asp:ListItem Value="17 권한">17 권한</asp:ListItem>
														<asp:ListItem Value="18 권한">18 권한</asp:ListItem>
														<asp:ListItem Value="19 권한">19 권한</asp:ListItem>
														<asp:ListItem Value="20 권한">20 권한</asp:ListItem>
													</asp:dropdownlist></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 60px" align="right" width="60"><FONT face="굴림">사진&nbsp;</FONT></TD>
												<TD align="left" colSpan="5"><INPUT id="Picture" style="BACKGROUND-COLOR: #eeeee9; WIDTH: 344px; HEIGHT: 22px" type="file"
														size="38" runat="server" NAME="Picture"></TD>
												<TD style="WIDTH: 245px" align="right" width="245"><FONT face="굴림">업체명&nbsp;</FONT></TD>
												<TD style="WIDTH: 104px" align="left" width="104"><asp:dropdownlist id="dl_Company" runat="server" BackColor="#EEEEE9" Width="80px"></asp:dropdownlist></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 60px; HEIGHT: 14px" align="right" width="60"><FONT face="굴림">E-Mail&nbsp;</FONT></TD>
												<TD style="WIDTH: 276px; HEIGHT: 14px" align="left" width="276" colSpan="3"><asp:textbox id="tb_EMail" runat="server" BorderStyle="Solid" BackColor="#EEEEE9" Font-Size="9pt"
														Width="232px" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
												<TD style="WIDTH: 192px; HEIGHT: 14px" align="right" width="192"><FONT face="굴림">업무일지&nbsp;</FONT></TD>
												<TD style="HEIGHT: 14px" align="left" width="100">
													<asp:dropdownlist id="ddlWorkDiary" runat="server" Width="80px" BackColor="#EEEEE9">
														<asp:ListItem Value="00 그룹">00 그룹</asp:ListItem>
														<asp:ListItem Value="01 그룹">01 그룹</asp:ListItem>
														<asp:ListItem Value="02 그룹">02 그룹</asp:ListItem>
														<asp:ListItem Value="03 그룹">03 그룹</asp:ListItem>
														<asp:ListItem Value="04 그룹">04 그룹</asp:ListItem>
														<asp:ListItem Value="05 그룹">05 그룹</asp:ListItem>
														<asp:ListItem Value="06 그룹">06 그룹</asp:ListItem>
														<asp:ListItem Value="07 그룹">07 그룹</asp:ListItem>
														<asp:ListItem Value="08 그룹">08 그룹</asp:ListItem>
														<asp:ListItem Value="09 그룹">09 그룹</asp:ListItem>
														<asp:ListItem Value="10 그룹">10 그룹</asp:ListItem>
														<asp:ListItem Value="11 그룹">11 그룹</asp:ListItem>
														<asp:ListItem Value="12 그룹">12 그룹</asp:ListItem>
														<asp:ListItem Value="13 그룹">13 그룹</asp:ListItem>
														<asp:ListItem Value="14 그룹">14 그룹</asp:ListItem>
														<asp:ListItem Value="15 그룹">15 그룹</asp:ListItem>
														<asp:ListItem Value="16 그룹">16 그룹</asp:ListItem>
													</asp:dropdownlist></TD>
												<TD style="WIDTH: 245px; HEIGHT: 14px" align="right" width="245"></TD>
												<TD style="WIDTH: 104px; HEIGHT: 14px" width="104"></TD>
											</TR>
											<tr height="5">
												<td colSpan="9" height="80"><FONT face="굴림"></FONT></td>
											</tr>
											<TR>
												<TD style="WIDTH: 60px" align="right" width="60"><asp:button id="bt_RelationView" runat="server" Height="20px" Width="60px" Text="참고보기"></asp:button></TD>
												<TD align="left" width="100"><asp:label id="lb_Picture" runat="server"></asp:label></TD>
												<TD style="WIDTH: 196px" align="right" width="196"><FONT face="굴림"><INPUT id="lb_ReferenceTable" style="WIDTH: 30px" type="hidden" name="lb_ReferenceTable"
															runat="server"></FONT></TD>
												<TD style="WIDTH: 81px" align="left" width="81"><FONT face="굴림"><INPUT id="lb_ReferenceIndex" style="WIDTH: 30px" type="hidden" value="0" name="lb_ReferenceTable"
															runat="server"></FONT></TD>
												<TD style="WIDTH: 192px" align="right" width="192"><FONT face="굴림"></FONT></TD>
												<TD align="right" width="451" colSpan="4"><asp:button id="bt_Clear" runat="server" Height="20px" Width="60px" Text="초기화"></asp:button>
													<asp:button id="bt_Update" runat="server" Height="20px" Width="60px" Text="수정" Enabled="False"></asp:button>
													<asp:button id="bt_Delete" runat="server" Height="20px" Width="60px" Text="삭제" Enabled="False"></asp:button>
													<asp:button id="bt_Registration" runat="server" Height="20px" Width="60px" Text="등록"></asp:button>&nbsp;</TD>
											</TR>
											<TR>
												<TD style="WIDTH: 60px" align="right" width="60" height="5"><FONT face="굴림"></FONT></TD>
												<TD align="left" width="100" height="5"></TD>
												<TD style="WIDTH: 196px" align="right" width="196" height="5"></TD>
												<TD style="WIDTH: 81px" align="left" width="81" height="5"></TD>
												<TD style="WIDTH: 192px" align="right" width="192" height="5"></TD>
												<TD align="right" width="451" colSpan="4" height="5"></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 473px" align="left" width="473" colSpan="9"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="230px" Width="800px">
														<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
															RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Extended" AllowColumnMovingDefault="OnServer"
															HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
															RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect"
															NoDataMessage="">
															<AddNewBox>
																<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

																</Style>
															</AddNewBox>
															<Pager>
																<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

																</Style>
															</Pager>
															<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray">
																<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
															</HeaderStyleDefault>
															<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
															<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderColor="DimGray"
																BorderStyle="Solid" BackColor="Silver" Height="230px"></FrameStyle>
															<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
																<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
															</FooterStyleDefault>
															<ClientSideEvents AfterSelectChangeHandler="UltraWebGrid1_AfterSelectChangeHandler"></ClientSideEvents>
															<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
															<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
															<RowAlternateStyleDefault Cursor="Hand" BorderColor="DarkGray" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
															<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
																<Padding Left="3px"></Padding>
																<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
															</RowStyleDefault>
														</DisplayLayout>
														<Bands>
															<igtbl:UltraGridBand>
																<Columns>
																	<igtbl:UltraGridColumn HeaderText="사용자정보테이블" Key="StandardinfoTableID" Width="150px" BaseColumnName="StandardinfoTableID">
																		<HeaderStyle Height="25px"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="품목정보인덱스" Key="SITIndex" Width="50px" Hidden="True" BaseColumnName="SITIndex"></igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="자료실테이블" Key="CommunityDataTableID" Width="150px" BaseColumnName="CommunityDataTableID"></igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="자료실인덱스" Key="CDTIndex" Width="50px" Hidden="True" BaseColumnName="CDTIndex"></igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="제목" Key="Title" Width="500px" HeaderClickAction="SortSingle" BaseColumnName="Title"></igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="번호" Key="CRelationShipIndex" Width="50px" Hidden="True" BaseColumnName="CRelationShipIndex"></igtbl:UltraGridColumn>
																</Columns>
															</igtbl:UltraGridBand>
														</Bands>
													</igtbl:ultrawebgrid></TD>
											</TR>
										</TABLE>
									</FIELDSET>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
