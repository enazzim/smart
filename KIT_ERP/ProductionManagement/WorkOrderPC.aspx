<%@ Page language="c#" Codebehind="WorkOrderPC.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.WorkOrderPC" codePage="949" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics.WebUI.UltraWebGrid.ExcelExport.v3.1" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WorkOrderPC</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!--
		var GridName;
		function RegistCheck(strArg)
		{
			if ( MonthCloseing == "0" )
			{
				return ( confirm("선택한 품목을 \"" + strArg + "\" 하시겠습니까?") );
			}
			else
			{
				alert('월 마감되어 등록할 수 없습니다.');
				return false;
			}
		}
		
		function TextBox_Reset()
		{
			ResetTextBox();
			var frm = document.Form1;
			
			var objCombo = igcmbo_getComboById("wcWCName");
			objCombo.setDisplayValue("");
						
			var objDChooser = igdrp_getComboById("wcStartDate");
			objDChooser.setValue(null);
			
			frm.ddlItemClassification1.options[0].selected = true
			frm.ddlItemClassification2.options[0].selected = true
			frm.ddlItemClassification3.options[0].selected = true
			frm.ddlItemClassification4.options[0].selected = true
			
			frm.ddlState.options[0].selected = true;
		}
		
		
		function UltraWebGrid1_ColumnHeaderClickHandler(gridName, columnId, button)
		{
			GridName = gridName;
    			if(document.Form1.chkAll.value == "true")
    			{
					AllCheck();
				}
				else
				{
					AllUncheck();
				}
		}
		
		function AllCheck() // 전부 체크하기
		{
			var grid = igtbl_getGridById(GridName);
			
			for(var i=0; i<grid.Rows.length; i++)
			{
				var row = grid.Rows.getRow(i);
				row.getCellFromKey("chk").setValue(true);
			}
			document.Form1.chkAll.value = "false";
		}


		function AllUncheck() // 전부체크 해제하기
		{
			var grid = igtbl_getGridById(GridName);
			
			for(var i=0; i<grid.Rows.length; i++)
			{
				var row = grid.Rows.getRow(i);
				row.getCellFromKey("chk").setValue(false);
			}
			document.Form1.chkAll.value = "true";
		}
//-->
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="800" border="0" style="LEFT: 10px; POSITION: absolute; TOP: 10px">
				<TR>
					<TD style="HEIGHT: 11px" width="20"></TD>
					<TD style="HEIGHT: 11px" colSpan="2">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 60px"
							align="middle"><LEGEND style="FONT-SIZE: 9pt" align="left">[ 검색조건 ]
							</LEGEND>
							<TABLE id="Table2" height="40" cellSpacing="0" cellPadding="0" width="800" align="center"
								border="0">
								<TR>
									<TD align="left" width="120" colSpan="6" height="28"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></TD>
									<TD align="right" width="70" height="28">진행상태&nbsp;
									</TD>
									<TD vAlign="middle" align="left" width="130" colSpan="3" height="28"><asp:dropdownlist id="ddlState" runat="server" Height="20px" Width="120px">
											<asp:ListItem Value=" ">전체</asp:ListItem>
											<asp:ListItem Value="대기">대기</asp:ListItem>
											<asp:ListItem Value="지시">지시</asp:ListItem>
											<asp:ListItem Value="진행">진행</asp:ListItem>
											<asp:ListItem Value="완료">완료</asp:ListItem>
											<asp:ListItem Value="중단">중단</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD height="28"></TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="28">품목분류1&nbsp;
									</TD>
									<TD width="130" height="28"><asp:dropdownlist id="ddlItemClassification1" runat="server" Width="120px" BackColor="#EEEEE9"></asp:dropdownlist></TD>
									<TD align="right" width="70" height="28">품목분류2&nbsp;</TD>
									<TD align="left" width="130" height="28"><asp:dropdownlist id="ddlItemClassification2" runat="server" Width="120px" BackColor="#EEEEE9"></asp:dropdownlist></TD>
									<TD align="right" width="70" height="28">품목분류3&nbsp;</TD>
									<TD align="left" width="130" height="28"><asp:dropdownlist id="ddlItemClassification3" runat="server" Width="120px" BackColor="#EEEEE9"></asp:dropdownlist></TD>
									<TD align="right" height="28">품목분류4&nbsp;</TD>
									<TD vAlign="middle" align="left" width="130" colSpan="3" height="28"><asp:dropdownlist id="ddlItemClassification4" runat="server" Width="120px" BackColor="#EEEEE9"></asp:dropdownlist></TD>
									<TD height="28"></TD>
								</TR>
								<TR>
									<TD align="right" height="28">작업장&nbsp;
									</TD>
									<TD height="28"><igcmbo:webcombo id="wcWCName" runat="server" Height="20px" Width="120px" BackColor="#EEEEE9" SelBackColor="10, 36, 106"
											SelectedIndex="-1" ForeColor="Black" SelForeColor="White" DropImageXP1="/ig_common/WebGrid3/ig_cmboDown1.bmp"
											DropImageXP2="/ig_common/WebGrid3/ig_cmboDown2.bmp" Version="3.00" BorderColor="Gray" BorderWidth="1px" BorderStyle="Solid">
											<DropDownLayout DropdownWidth="365px" BorderCollapse="Separate" RowSelectors="No" RowHeightDefault="20px"
												HeaderClickAction="Select" DropdownHeight="350px" TableLayout="Fixed" StationaryMargins="Header">
												<RowStyle BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="White">
													<Padding Left="5px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyle>
												<SelectedRowStyle ForeColor="White" BackColor="#0A246A"></SelectedRowStyle>
												<HeaderStyle BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyle>
												<FrameStyle Width="100%" Cursor="Default" BorderWidth="2px" Font-Size="10pt" Font-Names="Verdana"
													BorderStyle="Ridge" BackColor="Silver" Height="100%"></FrameStyle>
											</DropDownLayout>
											<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
										</igcmbo:webcombo></TD>
									<TD align="right" height="28">
										<P>작업시작일&nbsp;</P>
									</TD>
									<TD align="left" height="28"><igsch:webdatechooser id="wcStartDate" runat="server" Height="20px" BackColor="#EEEEE9" BorderColor="Gray"
											BorderStyle="Solid" NullDateLabel=" " Text="Null">
											<CALENDARLAYOUT ShowFooter="False" ShowTitle="False" ShowNextPrevMonth="False" FooterFormat="Today: {0:d}"
												DayNameFormat="FirstLetter">
												<SELECTEDDAYSTYLE BackColor="#0A246A" ForeColor="White"></SELECTEDDAYSTYLE>
												<OTHERMONTHDAYSTYLE ForeColor="White"></OTHERMONTHDAYSTYLE>
												<DROPDOWNSTYLE BackColor="White"></DROPDOWNSTYLE>
												<DAYHEADERSTYLE BackColor="Gray" ForeColor="#D4D0C8"></DAYHEADERSTYLE>
											</CALENDARLAYOUT>
											<DROPDOWNSTYLE BorderColor="Gray" BorderWidth="1px" BorderStyle="Solid"></DROPDOWNSTYLE>
											<DROPBUTTON ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp" ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp"></DROPBUTTON>
											<EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
										</igsch:webdatechooser></TD>
									<TD align="left" colSpan="2" height="28"></TD>
									<TD align="right" colSpan="4" height="28"><INPUT style="WIDTH: 65px; HEIGHT: 20px" onclick="TextBox_Reset()" type="button" value="초기화">
										<asp:button id="btnSearch" runat="server" Height="20px" Width="65px" Text="검  색"></asp:button>&nbsp;&nbsp;</TD>
								</TR>
								<TR>
									<TD align="right" colSpan="10" height="8"></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="20" height="5"></TD>
					<TD vAlign="top" align="center" colSpan="2" height="5"></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 400px" width="20"></TD>
					<TD vAlign="top" align="center" colSpan="2">
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-WEIGHT: normal; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 100%"
							align="middle"><LEGEND style="FONT-SIZE: 10pt" align="left">[ 검색 ]
							</LEGEND>
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="800" align="center">
								<TR height="35">
									<TD vAlign="top" align="center" rowSpan="1"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="400px" Width="800px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" RowHeightDefault="20px" Version="3.00"
												BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1"
												TableLayout="Fixed" CellClickActionDefault="RowSelect">
												<AddNewBox>
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</AddNewBox>
												<Pager PageSize="17" StyleMode="ComboBox" AllowPaging="True">
													<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													</Style>
												</Pager>
												<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray" Height="25px">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</HeaderStyleDefault>
												<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="400px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents ColumnHeaderClickHandler="UltraWebGrid1_ColumnHeaderClickHandler"></ClientSideEvents>
												<RowAlternateStyleDefault BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand>
													<Columns>
														<igtbl:UltraGridColumn HeaderText="선택" Key="chk" Width="40px" Type="CheckBox" BaseColumnName="" AllowUpdate="Yes">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" BaseColumnName="ItemNum"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Width="140px" BaseColumnName="ItemDrawNum"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="140px" BaseColumnName="ItemName"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정순서번호" Key="ProcessSequenceNum" Width="80px" BaseColumnName="ProcessSequenceNum">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="ProcessCode" Key="ProcessCode" Width="110px" Hidden="True" BaseColumnName="ProcessCode">
															<CellStyle HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정명" Key="ProcessName" Width="100px" Format="" BaseColumnName="ProcessName">
															<CellStyle HorizontalAlign="Left"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="제품번호" Key="ProductItemNum" Hidden="True" BaseColumnName="ProductItemNum"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업장명" Key="WCName" Format="" BaseColumnName="WCName">
															<CellStyle HorizontalAlign="Left"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업구분" Key="WorkDistinction" Width="70px" Format="" BaseColumnName="WorkDistinction">
															<CellStyle HorizontalAlign="Left"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업계획수량" Key="WorkPlanQuantity" Width="120px" Format="###,###,##0.00"
															BaseColumnName="WorkPlanQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업완료수량" Key="WorkCompletionQuantity" Format="###,###,##0.00" BaseColumnName="WorkCompletionQuantity">
															<CellStyle HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="OrderLeadTime" Key="OrderLeadTime" Hidden="True" BaseColumnName="OrderLeadTime"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업일" Key="WorkDate" Format="yyyy-MM-dd" BaseColumnName="WorkDate"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="진행상태" Key="ProgressCondition" Width="70px" BaseColumnName="ProgressCondition"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" BaseColumnName="RegistrationPersonID"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" BaseColumnName="RegistrationPerson"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" BaseColumnName="UpdatingPersonID"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" BaseColumnName="UpdatingPerson"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="인덱스번호" Key="WCDailyWorkPlanHistoryIndex" Hidden="True" Format="#########"
															BaseColumnName="WCDailyWorkPlanHistoryIndex"></igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="20" height="30"></TD>
					<TD align="left" height="30">&nbsp;
						<asp:button id="Button1" runat="server" Height="20px" Width="65px" Text="Excel"></asp:button><igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter><INPUT id="chkAll" style="WIDTH: 48px; HEIGHT: 21px" type="hidden" size="2" value="true"
							name="chkAll"></TD>
					<TD align="right" height="30">&nbsp;
						<asp:button id="Button3" runat="server" Height="20px" Width="65px" Text="중 단"></asp:button>&nbsp;
						<asp:Button id="Button2" runat="server" Text="지시취소" Height="20px" Width="65px"></asp:Button>&nbsp;&nbsp;
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
