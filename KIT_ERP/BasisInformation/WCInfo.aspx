<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="WCInfo.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.WCInfo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WCNameInfor</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<STYLE type="text/css">BODY { SCROLLBAR-FACE-COLOR: #ffffff; SCROLLBAR-HIGHLIGHT-COLOR: #ffffff; SCROLLBAR-SHADOW-COLOR: #ffffff; SCROLLBAR-3DLIGHT-COLOR: #808080; SCROLLBAR-ARROW-COLOR: #400040; SCROLLBAR-TRACK-COLOR: #ffffff; SCROLLBAR-DARKSHADOW-COLOR: #808080 }
		</STYLE>
		<script language="javascript">
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
			Form1.lb_ReferenceTable.value = row.getCellFromKey("CommunityDataTableID").getValue();
			Form1.lb_ReferenceIndex.value = row.getCellFromKey("CDTIndex").getValue();
		}
		
		function OnKeyDown_Currency(obj) {   
			if (event.ctrlKey || event.shiftKey || event.altKey) {
				SetEventPass();
				return true;
			}
			var sFieldValue = new String(obj.value);        
			if( (event.keyCode<45 || event.keyCode>57) && (event.keyCode<96 || event.keyCode>105))	 {
    			if( event.keyCode == 110 || event.keyCode == 190 )	// "."(점) 이라면
    			{
    				// "." 가 2개있는지 검사
	    			if( sFieldValue.indexOf(".") != -1 ) 	{
	    				SetEventCancel();
						return obj.value;
	    			}
				} else if( !IsNumericKey() ){
	    			SetEventCancel();
					return obj.value;
				}	    
			}	else	{    
    			if( sFieldValue.indexOf(".") == -1 )	{
	    			// "0" 을 입력했을 경우 "0" 다음에 "." 가 아니면 무시하여야 한다.	
	    			// 48, 96(Numeric Keypad)
					if( sFieldValue.substr(0,1) == "0" && (event.keyCode == 48 || event.keyCode == 96) )	{
						SetEventCancel();
						return obj.value;
					}
				}
			}
			SetEventPass();
			return true;
		}
		
		function OnKeyUp_Currency(obj)		{
			sFieldValue = new String(obj.value);
			sFieldValue = sFieldValue.replace(/,/gi,"");
			if ( IsNumericKey() )		{
				SetEventPass();
				return true;
			}
			obj.value = MakeCommaSeparate(sFieldValue);
		}
		
		function OnBlur_Cur(obj)	{
			if (ValidateCurrency(obj.value) == false) {
				obj.value = "";
    			return false;
			}
		}
		
		function ValidateCurrency( a_sValue ) {	
			var s_FieldValue = new String(a_sValue);
			
			if (isNaN(parseFloat(s_FieldValue)))
				return false;
				
			return true;
		}

		// OnKeyUp_Currency 에서 호출할 소수점 찍기
		function MakeCommaSeparate(a_Value)	{
			var fl = "";
			var nPointPos = 0;    
			
			if(isNaN(a_Value)) 	{ 
    			event.returnValue = false;
    			this.value = "";
    			return "";
			}
			
			if(a_Value == 0) return a_Value;
			
			if(a_Value < 0) { 
				a_Value=a_Value*(-1);
				fl = "-";
			}else if(a_Value == 0)	{
    			// 처음 입력값이 0부터 시작할때 이것을 제거한다.
    			a_Value = a_Value*1; 
			}
			
			var a_Value = new String(a_Value);
			var temp = "";
			var sRemain = "";
			var co = 3;
			
			nPointPos = a_Value.indexOf(".");

			if( nPointPos == -1 )  {
    			num_len = a_Value.length;
			}else{
    			// "." 가 포함되어 있을 경우에 재계산
    			if( parseInt(a_Value.substr(0,nPointPos)) == 0 )	{
    				a_Value = "0" + a_Value.substr(nPointPos);
    				nPointPos = a_Value.indexOf(".");
    			}
    			num_len = nPointPos;
    			sRemain = a_Value.substr(nPointPos);
			}
			
			while (num_len>0)	{
				num_len = num_len - co;
				
				if(num_len<0)	{
        			co=num_len+co;
        			num_len=0;
				}
				temp = "," + a_Value.substr(num_len,co) + temp;
			}
			return fl + temp.substr(1) + sRemain;
		}
		
		function IsNumericKey()  {
			if (
				event.keyCode == 8 ||
				event.keyCode == 9 ||
				event.keyCode == 35 ||
				event.keyCode == 36 ||
				event.keyCode == 37 ||
				event.keyCode == 39 ||
				event.keyCode == 38 ||
				event.keyCode == 40 ||
				event.keyCode == 46 ||
				event.keyCode == 189 ||
				event.keyCode == 109 ||
				event.keyCode == 110 ||		// "."
				event.keyCode == 190			// 키패드 "."
				)
    			return true;
			else
    			return false;
		}
							
		function SetEventPass() {
				event.cancelBubble = false;
				event.returnValue = true;
		}

		function SetEventCancel() {
				event.cancelBubble = true;
				event.returnValue = false;
		}

		function OnFocus_Obj(obj) {
			obj.select();
		}
	
		function ValidateFloat(a_sValue)	{	
			var s_FieldValue = new String(a_sValue);
			
			if (isNaN(s_FieldValue))
				return false;
				
			return true;
		}		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#f7f6f6">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" cellSpacing="0"
				cellPadding="0" width="800" border="0" height="550">
				<TR>
					<TD>
						<table border="0" width="800" cellSpacing="0" cellPadding="0" height="550">
							<tr>
								<td>
									<table id="a" border="0" width="100%" cellSpacing="0" cellPadding="0" height="20">
										<tr>
											<td width="70" align="right" style="HEIGHT: 21px"><FONT face="굴림">작업장선택&nbsp;</FONT></td>
											<td width="628" colspan="7" style="WIDTH: 628px; HEIGHT: 21px">
												<igcmbo:WebCombo id="wc_WCChoice" runat="server" Width="150px" Height="20px" ForeColor="Black" SelForeColor="White"
													SelectedIndex="-1" SelBackColor="DarkBlue" BorderColor="LightGray" BorderWidth="1px" BackColor="#EEEEE9"
													Version="3.00" BorderStyle="Inset" Editable="True" DropImageXP2="/ig_common/WebGrid3/ig_cmboDown1.bmp"
													DropImageXP1="/ig_common/WebGrid3/ig_cmboDown1.bmp">
													<Columns>
														<igtbl:UltraGridColumn HeaderText="작업장명" Key="WCName" Width="250px" BaseColumnName="WCName"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="번호" Key="WCInfoIndex" Width="100px" BaseColumnName="WCInfoIndex"></igtbl:UltraGridColumn>
													</Columns>
													<DropDownLayout DropdownWidth="350px" BorderCollapse="Separate" RowSelectors="No" AllowColSizing="Free"
														RowHeightDefault="20px" HeaderClickAction="SortMulti" AutoGenerateColumns="False" AllowSorting="OnClient"
														RowSizing="Free" DropdownHeight="200px" TableLayout="Fixed" StationaryMargins="Header" AllowRowSizing="Free">
														<RowStyle Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="White">
															<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
														</RowStyle>
														<SelectedRowStyle ForeColor="White" BackColor="DarkBlue"></SelectedRowStyle>
														<HeaderStyle Cursor="Hand" VerticalAlign="Middle" BorderStyle="Solid" HorizontalAlign="Center"
															BackColor="LightGray">
															<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
														</HeaderStyle>
														<FrameStyle Width="100%" Cursor="Default" BorderWidth="2px" Font-Size="10pt" Font-Names="Verdana"
															BorderStyle="Ridge" BackColor="Silver" Height="100%"></FrameStyle>
													</DropDownLayout>
													<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
												</igcmbo:WebCombo>
												<asp:label id="lb_WCInfo" runat="server" Width="10px" Font-Size="10pt" Visible="False">0</asp:label>
											</td>
											<td align="right" vAlign="bottom" style="HEIGHT: 21px">
												<asp:LinkButton id="LinkButton1" runat="server" Font-Size="9pt">작업장 보기</asp:LinkButton>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td>
									<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; LEFT: 10px; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"
										align="left"><legend>[ 입 력 ]</legend>
										<table width="800" cellSpacing="0" cellPadding="0">
											<tr>
												<td width="70" align="right" style="HEIGHT: 11px"><FONT style="COLOR: #ff0000" face="굴림">*작업장명&nbsp;</FONT></td>
												<td width="130" align="left" style="HEIGHT: 11px">
													<asp:textbox id="tb_WCName" runat="server" Width="100px" Height="20px" Font-Size="8pt" BackColor="#EEEEE9"
														BorderStyle="Solid" BorderWidth="1px" BorderColor="DimGray"></asp:textbox></td>
												<td width="70" align="right" style="HEIGHT: 11px"><FONT style="COLOR: #ff0000" face="굴림">*대표공정&nbsp;</FONT></td>
												<td width="130" align="left" style="HEIGHT: 11px">
													<asp:DropDownList id="dl_MainProcessCode" runat="server" Width="100px" Height="20px" Font-Size="8pt"
														BackColor="#EEEEE9">
														<asp:ListItem Value="-선택-">-선택-</asp:ListItem>
													</asp:DropDownList></td>
												<td width="70" align="right" style="HEIGHT: 11px"><FONT style="COLOR: #ff0000" face="굴림">*보유인원&nbsp;</FONT>
												</td>
												<td width="130" align="left" style="HEIGHT: 11px" vAlign="middle">
													<asp:TextBox id="tb_RetentionStaff" style="TEXT-ALIGN: right" runat="server" Height="20px" Width="100px"
														Font-Size="9pt" BackColor="#EEEEE9" BorderStyle="Solid" BorderWidth="1px" BorderColor="DimGray">0</asp:TextBox>명
												</td>
												<td width="80" align="right" style="HEIGHT: 11px"><FONT style="COLOR: #ff0000" face="굴림">*현상태&nbsp;</FONT>
												</td>
												<td width="120" align="left" style="HEIGHT: 11px">
													<asp:DropDownList id="dl_State" runat="server" Width="100px" Height="20px" Font-Size="8pt" BackColor="#EEEEE9">
														<asp:ListItem Value="1">가동중</asp:ListItem>
														<asp:ListItem Value="0">가동중지</asp:ListItem>
													</asp:DropDownList></td>
											</tr>
											<tr>
												<td width="70" align="right" style="HEIGHT: 13px"><FONT style="COLOR: #ff0000" face="굴림">*Capa구분&nbsp;</FONT>
												</td>
												<td width="130" align="left" style="HEIGHT: 13px">
													<asp:DropDownList id="dl_CapacityDistinction" runat="server" Width="100px" Height="20px" Font-Size="8pt"
														BackColor="#EEEEE9">
														<asp:ListItem Value="0">작업시간</asp:ListItem>
														<asp:ListItem Value="1">작업시간*인원</asp:ListItem>
													</asp:DropDownList>
												</td>
												<td width="70" align="right" style="HEIGHT: 13px"><FONT style="COLOR: #ff0000" face="굴림">*가동시간&nbsp;</FONT></td>
												<td width="130" align="left" vAlign="middle" style="HEIGHT: 13px">
													<asp:TextBox id="tb_OperationTime" style="TEXT-ALIGN: right" runat="server" Height="20px" Width="100px"
														Font-Size="9pt" BackColor="#EEEEE9" BorderStyle="Solid" BorderWidth="1px" BorderColor="DimGray">0</asp:TextBox>분
												</td>
												<td width="70" align="right" style="HEIGHT: 13px"><FONT face="굴림">전력용량&nbsp;</FONT>
												</td>
												<td width="130" align="left" vAlign="middle" style="HEIGHT: 13px">
													<asp:TextBox id="tb_ElectricCapacity" style="TEXT-ALIGN: right" runat="server" Height="20px"
														Width="100px" Font-Size="9pt" BackColor="#EEEEE9" BorderStyle="Solid" BorderWidth="1px" BorderColor="DimGray">0</asp:TextBox>Kwh
												</td>
												<td width="100" align="right" style="HEIGHT: 13px"><FONT face="굴림">시간당 사용료&nbsp;</FONT>
												</td>
												<td width="120" align="left" vAlign="top" style="HEIGHT: 13px">
													<asp:TextBox id="tb_UnitTimeUseCost" style="TEXT-ALIGN: right" runat="server" Height="20px" Width="100px"
														Font-Size="9pt" BackColor="#EEEEE9" BorderStyle="Solid" BorderWidth="1px" BorderColor="DimGray">0</asp:TextBox><FONT face="굴림">원</FONT>
												</td>
											</tr>
											<tr>
												<td width="70" align="right">정렬순서
												</td>
												<td width="730" align="left" colspan="7">
													<asp:TextBox id="tb_Sorting" style="TEXT-ALIGN: right" runat="server" BorderStyle="Solid" BackColor="#EEEEE9"
														Width="100px" Font-Size="9pt" BorderWidth="1px" BorderColor="DimGray">0</asp:TextBox>
												</td>
											</tr>
											<tr>
												<td height="5" colspan="8">
												</td>
											</tr>
											<tr>
												<td width="70" align="right">
													<asp:button id="bt_Referance" runat="server" Width="60px" Height="20px" Text="참고보기" Font-Size="8pt"></asp:button></td>
												<td width="400" colspan="4">
													<asp:label id="lb_Index" runat="server" Width="10px" Visible="False"></asp:label><INPUT id="lb_ReferenceIndex" style="WIDTH: 30px; HEIGHT: 20px" type="hidden" name="lb_ReferenceIndex"
														runat="server"><INPUT id="lb_ReferenceTable" style="WIDTH: 30px; HEIGHT: 20px" type="hidden" name="lb_ReferenceTable"
														runat="server"></td>
												<td align="right" width="350" colSpan="3"><asp:button id="bt_Clear" runat="server" Height="20px" Width="65px" Text="초기화" Font-Size="10pt"></asp:button><FONT face="굴림">&nbsp;</FONT>
													<asp:button id="bt_Update" runat="server" Height="20px" Width="65px" Text="수  정" Enabled="False"
														Font-Size="10pt"></asp:button><FONT face="굴림">&nbsp;</FONT>
													<asp:button id="bt_Delete" runat="server" Height="20px" Width="65px" Text="삭  제" Enabled="False"
														Font-Size="10pt"></asp:button><FONT face="굴림">&nbsp;</FONT>
													<asp:button id="bt_Registration" runat="server" Height="20px" Width="65px" Text="등  록" Font-Size="10pt"></asp:button><FONT face="굴림">&nbsp;</FONT>&nbsp;</td>
											</tr>
											<TR>
												<TD align="right" width="70" height="5"><FONT face="굴림"></FONT></TD>
												<TD width="400" colSpan="4" height="5"></TD>
												<TD align="right" width="350" colSpan="3" height="5"></TD>
											</TR>
											<tr>
												<td height="10" colspan="8" width="800"><FONT face="굴림">
														<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="410px" Width="800px">
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
																	BorderStyle="Solid" BackColor="Silver" Height="410px"></FrameStyle>
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
																		<igtbl:UltraGridColumn HeaderText="WC정보테이블" Key="StandardinfoTableID" Width="150px" BaseColumnName="StandardinfoTableID">
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
														</igtbl:ultrawebgrid></FONT>
												</td>
											</tr>
										</table>
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
