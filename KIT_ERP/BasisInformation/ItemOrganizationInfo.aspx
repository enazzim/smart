<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearch" Src="ItemSearch/ItemSearch.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="ItemOrganizationInfo.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.ItemOrganizationInfo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ItemOrganizationInfo</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<STYLE type="text/css">BODY { SCROLLBAR-FACE-COLOR: #ffffff; SCROLLBAR-HIGHLIGHT-COLOR: #ffffff; SCROLLBAR-SHADOW-COLOR: #ffffff; SCROLLBAR-3DLIGHT-COLOR: #808080; SCROLLBAR-ARROW-COLOR: #400040; SCROLLBAR-TRACK-COLOR: #ffffff; SCROLLBAR-DARKSHADOW-COLOR: #808080 }
		</STYLE>
		<script language="javascript">
		
		
		
		var webComboId = document.getElementById("wc_ItemChoice1") ;
		function OK(text)
		{
			if(confirm(text + "하시겠습니까?"))
				return true;
			else
				return false;
		}
		
		function DoPost()
		{
			__doPostBack('LinkButton2','');
		}
		
		function DoPostChild()
		{
			__doPostBack('LinkButton3','');
		}
		
		
		
		function UltraWebGrid1_DblClickHandler(gridName, cellId){
			//Add code to handle your event here.
			var row = igtbl_getRowById(cellId);
			var combo = igdrp_getComboById("wdcBeginDate");//적용일
			var combo1 = igdrp_getComboById("wdcEndDate");//종료일
			
			combo.setValue(row.getCellFromKey("BeginDate").getValue());//적용일
			combo1.setValue(row.getCellFromKey("EndDate").getValue());//종료일
			
			document.ItemOrganizationInfo.ItemSearch1_ChildtxtItemNum.value = row.getCellFromKey("ChildItemNum").getValue()	//자품번호
			document.ItemOrganizationInfo.hdItemNum.value = row.getCellFromKey("ChildItemNum").getValue()	//자품번호
			document.ItemOrganizationInfo.ItemSearch1_ChildtxtItemDrawNum.value = row.getCellFromKey("ItemDrawNum").getValue()	//자품번호
			document.ItemOrganizationInfo.ItemSearch1_ChildtxtItemName.value = row.getCellFromKey("ItemName").getValue()	//자품번호
			document.ItemOrganizationInfo.tb_ChildPropertyClassification.value = row.getCellFromKey("PropertyClassification").getValue();//자품목분류
			
			document.ItemOrganizationInfo.tb_ChildUnit.value = row.getCellFromKey("ChildUnit").getValue()	//자품단위
			if(row.getCellFromKey("Standard").getValue() == null)
				document.ItemOrganizationInfo.tb_ChildStandard.value = "";
			else
				document.ItemOrganizationInfo.tb_ChildStandard.value = row.getCellFromKey("Standard").getValue()	//자품규격
			document.ItemOrganizationInfo.tb_NeedQuantityNumerator.value = row.getCellFromKey("NeedQuantityNumerator").getValue()	//소요량분자
			document.ItemOrganizationInfo.tb_NeedQuantityDenominator.value = row.getCellFromKey("NeedQuantityDenominator").getValue();//소요량분모
			
			//공정관리
			if(document.ItemOrganizationInfo.dl_ProcessManagement.options[0].innerText == row.getCellFromKey("ProcessManagement").getValue())
				document.ItemOrganizationInfo.dl_ProcessManagement.options[0].selected=true;
			else
				document.ItemOrganizationInfo.dl_ProcessManagement.options[1].selected=true;
				
			//하위구분
			if(document.ItemOrganizationInfo.dl_SubDivision.options[0].innerText == row.getCellFromKey("SubDivision").getValue())
				document.ItemOrganizationInfo.dl_SubDivision.options[0].selected=true;
			else
				document.ItemOrganizationInfo.dl_SubDivision.options[1].selected=true;
				
			
			
			//조달구분
			for(var i=0;i<document.ItemOrganizationInfo.dl_SupplyDivision.options.length;i++)
			{
				if(document.ItemOrganizationInfo.dl_SupplyDivision.options[i].innerText == row.getCellFromKey("Division").getValue())
				{
					document.ItemOrganizationInfo.dl_SupplyDivision.options[i].selected=true;
				}
			}
			
			//Bom단위
			for(var i=0;i<document.ItemOrganizationInfo.dl_BOMUnit.options.length;i++)
			{
				if(document.ItemOrganizationInfo.dl_BOMUnit.options[i].innerText == row.getCellFromKey("SmallClassificationName").getValue())
				{
					document.ItemOrganizationInfo.dl_BOMUnit.options[i].selected=true;
				}
			}
			
			document.ItemOrganizationInfo.lb_Index.value = row.getCellFromKey("ItemOrganizationInfoIndex").getValue();
			
			document.ItemOrganizationInfo.bt_Update.disabled = false;
		    document.ItemOrganizationInfo.bt_Delete.disabled = false;
		    document.ItemOrganizationInfo.bt_Registration.disabled = true;
		    document.ItemOrganizationInfo.ItemSearch1_btnItemNumOpen.disabled = true;
		    document.ItemOrganizationInfo.ItemSearch1_btnItemDrawNumOpen.disabled = true;
		    document.ItemOrganizationInfo.ItemSearch1_btnItemNameOpen.disabled = true;
		    document.ItemOrganizationInfo.ItemSearch1_ChildtxtItemNum.disabled = true;
		    document.ItemOrganizationInfo.ItemSearch1_ChildtxtItemDrawNum.disabled = true;
			document.ItemOrganizationInfo.ItemSearch1_ChildtxtItemName.disabled = true;
			
			
		}
		
		
		
		function Ultrawebgrid2_AfterSelectChangeHandler(gridName, id){
			//Add code to handle your event here.
			var row = igtbl_getRowById(id);
			ItemOrganizationInfo.lb_ReferenceTable.value = row.getCellFromKey("CommunityDataTableID").getValue();
			ItemOrganizationInfo.lb_ReferenceIndex.value = row.getCellFromKey("CDTIndex").getValue();
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
	<body bgColor="#f7f6f6" ms_positioning="GridLayout">
		<form id="ItemOrganizationInfo" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 0px" height="550"
				cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD>
						<table cellSpacing="0" cellPadding="0">
							<tr>
								<td>
									<table id="table1" style="HEIGHT: 20px" cellSpacing="0" cellPadding="0" width="100%" bgColor="#f7f6f6">
										<TR>
											<td align="left" width="600" colSpan="3"><INPUT id="ItemIndex" style="WIDTH: 30px" type="hidden" name="ItemIndex" runat="server"><asp:label id="lb_ItemIndex" runat="server" Font-Size="10pt" Visible="False"></asp:label><FONT face="굴림">&nbsp;&nbsp;
												</FONT>
											</td>
											<td vAlign="bottom" align="right" width="200" colSpan="5"><asp:linkbutton id="LinkButton1" runat="server" Font-Size="9pt">품목구성보기</asp:linkbutton></td>
										</TR>
									</table>
								</td>
							</tr>
							<tr>
								<td>
									<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 480px"
										align="left"><LEGEND>[ 입 력 ]</LEGEND>
										<TABLE style="WIDTH: 800px; HEIGHT: 368px" cellSpacing="0" cellPadding="0">
											<TR>
												<TD align="left" width="600" colSpan="6" height="20"><FONT face="굴림"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></FONT></TD>
												<TD align="right" width="70" height="20"><FONT face="굴림"><FONT face="굴림">자산분류&nbsp;</FONT></FONT></TD>
												<TD align="left" width="130" height="20"><FONT face="굴림"><asp:textbox id="tb_ParentPropertyClassification" runat="server" Font-Size="9pt" BackColor="#EEEEE9"
															BorderStyle="Solid" Height="20px" Width="100px" ReadOnly="True" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></FONT></TD>
											</TR>
											<TR>
												<TD align="right" width="70"><FONT face="굴림">단 위&nbsp;</FONT></TD>
												<TD align="left" width="130"><asp:textbox id="tb_ParentUnit" runat="server" Font-Size="9pt" BackColor="#EEEEE9" BorderStyle="Solid"
														Height="20px" Width="100px" ReadOnly="True" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
												<TD align="right" width="70"><FONT face="굴림">규 격&nbsp;</FONT></TD>
												<TD align="left" width="130"><asp:textbox id="tb_ParentStandard" runat="server" Font-Size="9pt" BackColor="#EEEEE9" BorderStyle="Solid"
														Height="20px" Width="100px" ReadOnly="True" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
												<TD align="right" width="70"><FONT face="굴림"></FONT></TD>
												<TD align="right" width="330" colSpan="3"><asp:button id="Button2" runat="server" Height="20px" Width="65px" Text="BOM 복사"></asp:button>&nbsp;
													<asp:button id="Button1" runat="server" Height="20px" Width="65px" Text="역 BOM"></asp:button>&nbsp;<asp:button id="bt_Expansion" runat="server" Height="20px" Width="65px" Text="BOM전개"></asp:button>&nbsp;
													<asp:button id="bt_ExpansionView" runat="server" Height="20px" Width="90px" Text="소요자재보기"></asp:button>&nbsp;</TD>
											</TR>
											<TR>
												<TD style="HEIGHT: 15px" align="right" width="70" height="15"><FONT face="굴림"></FONT></TD>
												<TD style="HEIGHT: 15px" align="left" width="130" height="15"></TD>
												<TD style="HEIGHT: 15px" align="right" width="70" height="15"></TD>
												<TD style="HEIGHT: 15px" align="left" width="130" height="15"></TD>
												<TD style="HEIGHT: 15px" align="right" width="70" height="15"></TD>
												<TD style="HEIGHT: 15px" align="right" width="330" colSpan="3" height="15"><asp:linkbutton id="LinkButton2" runat="server" Visible="False">LinkButton</asp:linkbutton></TD>
											</TR>
											<TR>
												<TD vAlign="top" align="left" width="800" colSpan="8" height="180"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="190px" Width="800px">
														<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="Yes"
															RowHeightDefault="20px" RowSizingDefault="Free" Version="3.00" SelectTypeRowDefault="Single"
															AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate"
															AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed"
															CellClickActionDefault="RowSelect">
															<AddNewBox>
																<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
																</Style>
															</AddNewBox>
															<Pager>
																<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
																</Style>
															</Pager>
															<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray" Height="25px">
																<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
															</HeaderStyleDefault>
															<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="Verdana" BorderColor="DimGray"
																BorderStyle="Solid" BackColor="Silver" Height="190px"></FrameStyle>
															<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
																<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
															</FooterStyleDefault>
															<ClientSideEvents DblClickHandler="UltraWebGrid1_DblClickHandler"></ClientSideEvents>
															<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
															<SelectedRowStyleDefault ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
															<RowAlternateStyleDefault BorderColor="DarkGray" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
															<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
																<Padding Left="3px"></Padding>
																<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
															</RowStyleDefault>
														</DisplayLayout>
														<Bands>
															<igtbl:UltraGridBand>
																<Columns>
																	<igtbl:UltraGridColumn HeaderText="모품목번호" Key="ParentItemNum" BaseColumnName="ParentItemNum">
																		<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																		<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="자품목번호" Key="ChildItemNum" BaseColumnName="ChildItemNum">
																		<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																		<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="자품도면번호" Key="ItemDrawNum" BaseColumnName="ItemDrawNum">
																		<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																		<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="자품목명" Key="ItemName" BaseColumnName="ItemName">
																		<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																		<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="자품목분류" Key="PropertyClassification" Hidden="True" BaseColumnName="PropertyClassification">
																		<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																		<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="단위" Key="Unit" Hidden="True" BaseColumnName="Unit">
																		<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																		<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="단위코드" Key="ChildUnit" Hidden="True" BaseColumnName="ChildUnit">
																		<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																		<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="규격" Key="Standard" Hidden="True" BaseColumnName="Standard">
																		<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																		<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="소요량분자" Key="NeedQuantityNumerator" BaseColumnName="NeedQuantityNumerator">
																		<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																		<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="소요량분모" Key="NeedQuantityDenominator" BaseColumnName="NeedQuantityDenominator">
																		<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
																		<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="공정관리여부" Key="ProcessManagement" Type="DropDownList" BaseColumnName="ProcessManagement">
																		<ValueList>
																			<ValueListItems>
																				<igtbl:ValueListItem Key="1" DisplayText="예"></igtbl:ValueListItem>
																				<igtbl:ValueListItem Key="0" DisplayText="아니오"></igtbl:ValueListItem>
																			</ValueListItems>
																		</ValueList>
																		<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																		<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="하위구분" Key="SubDivision" Type="DropDownList" BaseColumnName="SubDivision">
																		<ValueList>
																			<ValueListItems>
																				<igtbl:ValueListItem Key="1" DisplayText="예"></igtbl:ValueListItem>
																				<igtbl:ValueListItem Key="1" DisplayText="아니오"></igtbl:ValueListItem>
																			</ValueListItems>
																		</ValueList>
																		<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																		<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="조달구분 코드" Key="SupplyDivision" Hidden="True" Type="DropDownList" BaseColumnName="SupplyDivision">
																		<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																		<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="조달구분" Key="Division" BaseColumnName="Division">
																		<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																		<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="BOM단위 코드" Key="BOMUnit" Hidden="True" Type="DropDownList" BaseColumnName="BOMUnit">
																		<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																		<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="BOM단위" Key="SmallClassificationName" BaseColumnName="SmallClassificationName">
																		<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																		<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="적용시작일" Key="BeginDate" Format="yyyy-MM-dd" BaseColumnName="BeginDate">
																		<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																		<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="적용종료일" Key="EndDate" Format="yyyy-MM-dd" BaseColumnName="EndDate">
																		<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																		<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="레코드상태" Key="RecodingState" Hidden="True" BaseColumnName="RecodingState">
																		<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																		<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" BaseColumnName="RegistrationPerson">
																		<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																		<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID">
																		<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate">
																		<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																		<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" BaseColumnName="UpdatingPerson">
																		<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																		<HeaderStyle VerticalAlign="Middle"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
																		<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
																		<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																		<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																	<igtbl:UltraGridColumn HeaderText="번호" Key="ItemOrganizationInfoIndex" Hidden="True" BaseColumnName="ItemOrganizationInfoIndex">
																		<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																		<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
																	</igtbl:UltraGridColumn>
																</Columns>
															</igtbl:UltraGridBand>
														</Bands>
													</igtbl:ultrawebgrid></TD>
											</TR>
											<TR>
												<TD style="HEIGHT: 25px" align="left" width="800" colSpan="8"><INPUT id="Hidden1" style="WIDTH: 30px" type="hidden" name="ItemIndex" runat="server">
													<asp:label id="lb_ItemOrganization" runat="server" Font-Size="10pt" Visible="False"></asp:label><asp:label id="lb_ItemIndex1" runat="server" Font-Size="10pt" Visible="False"></asp:label></TD>
											</TR>
											<TR>
												<TD align="left" width="600" colSpan="6" height="20"><uc1:itemsearch id="ItemSearch1" runat="server"></uc1:itemsearch><asp:linkbutton id="LinkButton3" runat="server" Visible="False">LinkButton</asp:linkbutton><INPUT id="lb_ReferenceIndex" style="WIDTH: 30px" type="hidden" name="lb_ReferenceIndex"
														runat="server"> <INPUT id="lb_ReferenceTable" style="WIDTH: 30px" type="hidden" name="lb_ReferenceTable"
														runat="server"><INPUT id="hdItemNum" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="Hidden2"
														runat="server"> <INPUT id="lb_Index" style="WIDTH: 47px; HEIGHT: 22px" type="hidden" size="2" value="0"
														name="lb_Index" runat="server"></TD>
												<TD align="right" width="70" height="20"><FONT face="굴림">자산분류&nbsp;</FONT></TD>
												<TD align="left" width="130" height="20"><asp:textbox id="tb_ChildPropertyClassification" runat="server" Font-Size="9pt" BackColor="#EEEEE9"
														BorderStyle="Solid" Height="20px" Width="100px" ReadOnly="True" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
											</TR>
											<TR>
												<TD align="right" width="70"><FONT face="굴림">단 위&nbsp;</FONT></TD>
												<TD align="left" width="130"><asp:textbox id="tb_ChildUnit" runat="server" Font-Size="9pt" BackColor="#EEEEE9" BorderStyle="Solid"
														Height="20px" Width="100px" ReadOnly="True" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
												<TD align="right" width="70"><FONT face="굴림">규 격&nbsp;</FONT></TD>
												<TD align="left" width="130"><asp:textbox id="tb_ChildStandard" runat="server" Font-Size="9pt" BackColor="#EEEEE9" BorderStyle="Solid"
														Height="20px" Width="100px" ReadOnly="True" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
												<TD align="right" width="70">&nbsp;<FONT style="FONT-SIZE: 8pt; COLOR: red" face="굴림">*소요량분자&nbsp;</FONT></TD>
												<TD align="left" width="130"><asp:textbox id="tb_NeedQuantityNumerator" style="TEXT-ALIGN: right" runat="server" Font-Size="9pt"
														BackColor="#EEEEE9" BorderStyle="Solid" Height="20px" Width="100px" BorderColor="DimGray" BorderWidth="1px">0</asp:textbox></TD>
												<TD align="right" width="70"><FONT style="FONT-SIZE: 8pt; COLOR: red" face="굴림">*소요량분모&nbsp;</FONT></TD>
												<TD align="left" width="130"><asp:textbox id="tb_NeedQuantityDenominator" style="TEXT-ALIGN: right" runat="server" Font-Size="9pt"
														BackColor="#EEEEE9" BorderStyle="Solid" Height="20px" Width="100px" BorderColor="DimGray" BorderWidth="1px">1</asp:textbox></TD>
											</TR>
											<TR>
												<TD style="HEIGHT: 18px" align="right" width="70"><FONT style="COLOR: red" face="굴림">공정관리&nbsp;</FONT></TD>
												<TD align="left" width="130"><asp:dropdownlist id="dl_ProcessManagement" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Height="20px"
														Width="65px">
														<asp:ListItem Value="1">예</asp:ListItem>
														<asp:ListItem Value="0">아니오</asp:ListItem>
													</asp:dropdownlist></TD>
												<TD align="right" width="70"><FONT face="굴림">하위구분&nbsp;</FONT></TD>
												<TD style="HEIGHT: 18px" align="left" width="130"><asp:dropdownlist id="dl_SubDivision" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Height="20px"
														Width="65px">
														<asp:ListItem Value="0">아니오</asp:ListItem>
														<asp:ListItem Value="1">예</asp:ListItem>
													</asp:dropdownlist></TD>
												<TD style="HEIGHT: 18px" align="right" width="70"><FONT face="굴림">조달구분&nbsp;</FONT></TD>
												<TD style="HEIGHT: 18px" width="130"><asp:dropdownlist id="dl_SupplyDivision" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Height="20px"
														Width="100px"></asp:dropdownlist></TD>
												<TD style="HEIGHT: 18px" align="right" width="70"><FONT style="FONT-SIZE: 8pt" face="굴림">BOM단위&nbsp;</FONT></TD>
												<TD style="HEIGHT: 18px" width="130"><asp:dropdownlist id="dl_BOMUnit" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Height="20px"
														Width="100px"></asp:dropdownlist></TD>
											</TR>
											<TR>
												<TD style="HEIGHT: 25px" align="right" width="70"><FONT style="FONT-SIZE: 8pt; COLOR: red" face="굴림">*적용시작일&nbsp;</FONT></TD>
												<TD style="HEIGHT: 25px" align="left" width="130"><igsch:webdatechooser id="wdcBeginDate" runat="server" Font-Size="9pt" BackColor="#EEEEE9" BorderStyle="Solid"
														Height="20px" Width="95px" BorderColor="DimGray" Text="Null" NullDateLabel=" " MaxDate="2079-06-06">
														<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
															MaxDate="2079-06-06" ShowTitle="False" ShowFooter="False">
															<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
															<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
															<DropDownStyle BackColor="White"></DropDownStyle>
															<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
														</CalendarLayout>
														<DropDownStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></DropDownStyle>
														<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
														<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
													</igsch:webdatechooser></TD>
												<TD style="HEIGHT: 25px" align="right" width="70"><FONT style="FONT-SIZE: 8pt" face="굴림">적용종료일&nbsp;</FONT></TD>
												<TD style="HEIGHT: 25px" width="130"><igsch:webdatechooser id="wdcEndDate" runat="server" Font-Size="9pt" BackColor="#EEEEE9" BorderStyle="Solid"
														Height="20px" Width="95px" BorderColor="DimGray" Text="Null" NullDateLabel=" " MaxDate="2079-06-06">
														<CalendarLayout DayNameFormat="FirstLetter" FooterFormat="Today: {0:d}" ShowNextPrevMonth="False"
															MaxDate="2079-06-06" ShowTitle="False" ShowFooter="False">
															<SelectedDayStyle ForeColor="White" BackColor="#0A246A"></SelectedDayStyle>
															<OtherMonthDayStyle ForeColor="White"></OtherMonthDayStyle>
															<DropDownStyle BackColor="White"></DropDownStyle>
															<DayHeaderStyle ForeColor="#D4D0C8" BackColor="Gray"></DayHeaderStyle>
														</CalendarLayout>
														<DropDownStyle BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></DropDownStyle>
														<DropButton ImageUrl2="/ig_common/webschedule1/ig_cmboDown2.bmp" ImageUrl1="/ig_common/webschedule1/ig_cmboDown1.bmp"></DropButton>
														<ExpandEffects ShadowColor="LightGray"></ExpandEffects>
													</igsch:webdatechooser></TD>
												<TD align="right" width="70"><FONT face="굴림">변경사유&nbsp;</FONT></TD>
												<TD align="left" width="200" colSpan="2"><asp:textbox id="tbUpdateReason" runat="server" Font-Size="9pt" BackColor="#EEEEE9" BorderStyle="Solid"
														Height="20px" Width="180px" BorderColor="DimGray" BorderWidth="1px"></asp:textbox></TD>
												<TD style="HEIGHT: 25px" width="130">&nbsp;
													<asp:button id="btHistory" runat="server" Height="20px" Width="60px" Text="이력보기"></asp:button></TD>
											</TR>
											<TR>
												<TD align="right" width="70"><asp:button id="bt_Reference" runat="server" Height="20px" Width="60px" Text="참고 보기"></asp:button>&nbsp;</TD>
												<TD align="right" colSpan="3">&nbsp;&nbsp;</FONT><FONT face="굴림">&nbsp;<INPUT id="File1" style="WIDTH: 300px; HEIGHT: 20px; BACKGROUND-COLOR: #eeeee9" type="file"
															size="31" name="File1" runat="server"></FONT><FONT face="굴림"></FONT></TD>
												<TD align="right" width="70">
													<asp:button id="tbTotalUpdate" runat="server" Width="60px" Height="20px" Text="일괄수정"></asp:button></TD>
												<TD align="right" width="330" colSpan="3"><asp:button id="bt_Clear" runat="server" Height="20px" Width="60px" Text="초기화"></asp:button><FONT face="굴림">&nbsp;</FONT><asp:button id="bt_Update" runat="server" Height="20px" Width="60px" Text="수  정" Enabled="False"></asp:button><FONT face="굴림">&nbsp;</FONT><asp:button id="bt_Delete" runat="server" Height="20px" Width="60px" Text="삭  제" Enabled="False"></asp:button><FONT face="굴림">&nbsp;</FONT><asp:button id="bt_Registration" runat="server" Height="20px" Width="60px" Text="등  록"></asp:button>&nbsp;</TD>
											</TR>
											<TR>
												<TD align="right" width="70" height="5"></TD>
												<TD align="left" width="130" height="5"><FONT face="굴림"></FONT></TD>
												<TD align="right" width="70" height="5"></TD>
												<TD align="left" width="130" height="5"></TD>
												<TD align="right" width="70" height="5"></TD>
												<TD align="right" width="330" colSpan="3" height="5"></TD>
											</TR>
											<TR>
												<TD colSpan="8" height="10"><igtbl:ultrawebgrid id="Ultrawebgrid2" runat="server" Height="128px" Width="800px">
														<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" RowHeightDefault="20px" Version="3.00"
															SelectTypeRowDefault="Extended" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti"
															BorderCollapseDefault="Separate" RowSelectorsDefault="No" Name="Ultrawebgrid2" TableLayout="Fixed"
															CellClickActionDefault="RowSelect" NoDataMessage="" AllowUpdateDefault="Yes">
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
															<HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
																<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
															</HeaderStyleDefault>
															<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
															<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderColor="DimGray"
																BorderStyle="Solid" BackColor="Silver" Height="128px"></FrameStyle>
															<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
																<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
															</FooterStyleDefault>
															<ClientSideEvents AfterSelectChangeHandler="Ultrawebgrid2_AfterSelectChangeHandler"></ClientSideEvents>
															<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
															<SelectedRowStyleDefault ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
															<RowAlternateStyleDefault BorderColor="DarkGray" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
															<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
																<Padding Left="3px"></Padding>
																<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
															</RowStyleDefault>
														</DisplayLayout>
														<Bands>
															<igtbl:UltraGridBand>
																<Columns>
																	<igtbl:UltraGridColumn HeaderText="품목구성정보테이블" Key="StandardinfoTableID" Width="150px" BaseColumnName="StandardinfoTableID">
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
