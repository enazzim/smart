<%@ Page language="c#" Codebehind="OutSideOrderUnitCodeInfo.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.OutSideOrderUnitCodeInfo" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v1.2, Version=1.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>OutSideUnitCostInfo</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../StyleSheet2.css">
		<STYLE type="text/css">BODY { SCROLLBAR-ARROW-COLOR: #400040; SCROLLBAR-FACE-COLOR: #ffffff; SCROLLBAR-DARKSHADOW-COLOR: #808080; SCROLLBAR-HIGHLIGHT-COLOR: #ffffff; SCROLLBAR-SHADOW-COLOR: #ffffff; SCROLLBAR-TRACK-COLOR: #ffffff; SCROLLBAR-3DLIGHT-COLOR: #808080 }
		</STYLE>
		<script language="javascript">
		<!--
		
		
		function OK(text)
		{
			if(confirm(text + "하시겠습니까?"))
				return true;
			else
				return false;
		}
		
		function DoPost()
		{
			__doPostBack('lnk_Update','');
		}
		
		function SubmitLinkButton(inputValue)
        {
			document.OutSideOrderUnitCodeInfo.ItemIndex.value = inputValue;
			__doPostBack('lnk_Update','');
		}
		
			
		function UltraWebGrid1_DblClickHandler(gridName, cellId)
		{
		
			//Add code to handle your event here.
			var row = igtbl_getRowById(cellId);
			
			var combo = igcmbo_getComboById("wcCompany");//거래처명
			var combo2 = igdrp_getComboById("wdcBeginDate");//적용일
			var combo3 = igdrp_getComboById("wdcEndDate");//종료일
			
			combo.setDataValue(row.getCellFromKey("BusinessRegistrationNum").getValue());//사업자등록번호
			combo.setDisplayValue(row.getCellFromKey("CompanyName").getValue());//거래처명
			
			combo2.setValue(row.getCellFromKey("BeginDate").getValue());//적용일
			combo3.setValue(row.getCellFromKey("EndDate").getValue());//종료일
			
			//시작공정
			for(var i=0;i<document.OutSideOrderUnitCodeInfo.dl_BeginProcess.options.length;i++)
			{
				if(document.OutSideOrderUnitCodeInfo.dl_BeginProcess.options[i].value == row.getCellFromKey("BeginProcessCode").getValue())
				{
					document.OutSideOrderUnitCodeInfo.dl_BeginProcess.options[i].selected=true;
				}
			}
			
			//종료공정
			for(var i=0;i<document.OutSideOrderUnitCodeInfo.dl_EndProcess.options.length;i++)
			{
				if(document.OutSideOrderUnitCodeInfo.dl_EndProcess.options[i].value == row.getCellFromKey("EndProcessCode").getValue())
				{
					document.OutSideOrderUnitCodeInfo.dl_EndProcess.options[i].selected=true;
				}
			}
			
			
			
			
			document.OutSideOrderUnitCodeInfo.tb_OrderRate.value = row.getCellFromKey("OrderRate").getValue()	//발주비율
			document.OutSideOrderUnitCodeInfo.tb_StandardUnitCost.value = MakeCommaSeparate(row.getCellFromKey("StandardUnitCost").getValue());//기준단가
			document.OutSideOrderUnitCodeInfo.tb_DiscountUnitCost.value = MakeCommaSeparate(row.getCellFromKey("DiscountUnitCost").getValue());	//할인단가
			
			document.OutSideOrderUnitCodeInfo.lb_Index.value = row.getCellFromKey("UnitCostInfoIndex").getValue();
			
			document.OutSideOrderUnitCodeInfo.bt_Update.disabled = false;
			document.OutSideOrderUnitCodeInfo.bt_Delete.disabled = false;
			document.OutSideOrderUnitCodeInfo.bt_Registration.disabled = true;
			//document.OutSideOrderUnitCodeInfo.dl_BeginProcess.disabled = false;
			//document.OutSideOrderUnitCodeInfo.dl_EndProcess.disabled = false;
		}
		
		function Ultrawebgrid2_AfterSelectChangeHandler(gridName, id){
			//Add code to handle your event here.
			var row = igtbl_getRowById(id);
			OutSideOrderUnitCodeInfo.lb_ReferenceTable.value = row.getCellFromKey("CommunityDataTableID").getValue();
			OutSideOrderUnitCodeInfo.lb_ReferenceIndex.value = row.getCellFromKey("CDTIndex").getValue();
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
		function wcCompany_AfterSelectChange(webComboId){
			//Add code to handle your event here. 
			if(document.OutSideOrderUnitCodeInfo.tb_OrderRate.value == '0')
				document.OutSideOrderUnitCodeInfo.tb_OrderRate.value = 100
		}
		//-->
		</script>
	</HEAD>
	<body bgColor="#f7f6f6" MS_POSITIONING="GridLayout">
		<form id="OutSideOrderUnitCodeInfo" method="post" runat="server">
			<TABLE style="Z-INDEX: 101; POSITION: absolute; TOP: 10px; LEFT: 10px" id="Table1" border="0"
				cellSpacing="0" cellPadding="0" width="800" height="550">
				<TR>
					<TD vAlign="top">
						<table style="HEIGHT: 20px" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<td width="600" colSpan="7" align="right"><FONT face="굴림"></FONT>&nbsp;<DROPDOWNLAYOUT DropdownHeight="130px" HeaderClickAction="Select" RowHeightDefault="20px" BorderCollapse="Separate"
										DropdownWidth="325px">
										<ROWSTYLE BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray" BackColor="White">
											<BORDERDETAILS WidthLeft="0px" WidthTop="0px"></BORDERDETAILS>
										</ROWSTYLE>
										<SELECTEDROWSTYLE BackColor="DarkBlue" ForeColor="White"></SELECTEDROWSTYLE>
										<HEADERSTYLE BorderStyle="Solid" BackColor="LightGray">
											<BORDERDETAILS WidthLeft="1px" WidthTop="1px" ColorTop="White" ColorLeft="White"></BORDERDETAILS>
										</HEADERSTYLE>
										<FRAMESTYLE BorderStyle="Ridge" BorderWidth="2px" BackColor="Silver" Cursor="Default" Font-Names="Verdana"
											Font-Size="10pt" Width="100%" Height="100%"></FRAMESTYLE>
									</DROPDOWNLAYOUT><EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS><DROPDOWNLAYOUT DropdownHeight="130px" HeaderClickAction="Select" RowHeightDefault="20px" BorderCollapse="Separate"
										DropdownWidth="325px"><ROWSTYLE BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray" BackColor="White"><BORDERDETAILS WidthLeft="0px" WidthTop="0px"></BORDERDETAILS>
										</ROWSTYLE>
										<SELECTEDROWSTYLE BackColor="DarkBlue" ForeColor="White"></SELECTEDROWSTYLE>
										<HEADERSTYLE BorderStyle="Solid" BackColor="LightGray">
											<BORDERDETAILS WidthLeft="1px" WidthTop="1px" ColorTop="White" ColorLeft="White"></BORDERDETAILS>
										</HEADERSTYLE>
										<FRAMESTYLE BorderStyle="Ridge" BorderWidth="2px" BackColor="Silver" Cursor="Default" Font-Names="Verdana"
											Font-Size="10pt" Width="100%" Height="100%"></FRAMESTYLE>
									</DROPDOWNLAYOUT><EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS><ROWSTYLE BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray" BackColor="White"><BORDERDETAILS WidthLeft="0px" WidthTop="0px"></BORDERDETAILS>
									</ROWSTYLE><SELECTEDROWSTYLE BackColor="DarkBlue" ForeColor="White"></SELECTEDROWSTYLE><HEADERSTYLE BorderStyle="Solid" BackColor="LightGray"><BORDERDETAILS WidthLeft="1px" WidthTop="1px" ColorTop="White" ColorLeft="White"></BORDERDETAILS>
									</HEADERSTYLE><FRAMESTYLE BorderStyle="Ridge" BorderWidth="2px" BackColor="Silver" Cursor="Default" Font-Names="Verdana"
										Font-Size="10pt" Width="100%" Height="100%"></FRAMESTYLE></DROPDOWNLAYOUT><EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS></td>
								<td vAlign="bottom" width="200" align="right"><asp:linkbutton id="LinkButton1" runat="server" Font-Size="10pt">단가정보보기</asp:linkbutton></td>
							</TR>
						</table>
						<FIELDSET style="BORDER-BOTTOM: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; HEIGHT: 480px; BORDER-TOP: dimgray 2px solid; BORDER-RIGHT: dimgray 2px solid"
							align="left"><LEGEND>[ 입 력 ]</LEGEND>
							<TABLE style="WIDTH: 800px; HEIGHT: 200px" cellSpacing="0" cellPadding="0">
								<TR>
									<TD height="20" width="600" colSpan="6" align="left"><FONT face="굴림"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol></FONT></TD>
									<TD height="20" width="70" align="right"><FONT style="COLOR: black" face="굴림">자산분류&nbsp;</FONT></TD>
									<TD height="20" width="130" align="left"><asp:textbox id="tb_PropertyClassification" runat="server" BorderStyle="Solid" BorderWidth="1px"
											BorderColor="DimGray" BackColor="#EEEEE9" Width="90px" Height="20px" ReadOnly="True"></asp:textbox></TD>
								</TR>
								<TR>
									<TD width="70" align="right"><FONT style="COLOR: black" face="굴림">단위&nbsp;</FONT></TD>
									<TD width="130" align="left"><asp:textbox id="tb_Unit" runat="server" BorderStyle="Solid" BorderWidth="1px" BorderColor="DimGray"
											BackColor="#EEEEE9" Width="100px" Height="20px" ReadOnly="True"></asp:textbox></TD>
									<TD width="70" align="right"><FONT style="COLOR: black" face="굴림">규격&nbsp;</FONT></TD>
									<TD width="130" align="left"><asp:textbox id="tb_Standard" runat="server" BorderStyle="Solid" BorderWidth="1px" BorderColor="DimGray"
											BackColor="#EEEEE9" Width="100px" Height="20px" ReadOnly="True"></asp:textbox></TD>
									<TD width="70" align="right"><asp:linkbutton id="lnk_Update" runat="server" Visible="False">lnk_Update</asp:linkbutton></TD>
									<TD width="130" align="center"><INPUT style="WIDTH: 30px" id="ItemIndex" type="hidden" name="ItemIndex" runat="server">
										<asp:linkbutton id="LinkButton2" runat="server" Visible="False">LinkButton</asp:linkbutton></TD>
									<TD width="70" align="center">&nbsp;
									</TD>
									<TD width="130" align="right"><asp:button id="bt_OutSideUnitCost" runat="server" Width="90px" Height="20px" Text="외주단가보기"></asp:button><FONT face="굴림">&nbsp;</FONT></TD>
								</TR>
								<TR>
									<TD height="5" width="70" align="right"><FONT face="굴림"></FONT></TD>
									<TD height="5" width="130" align="left"></TD>
									<TD height="5" width="70" align="right"></TD>
									<TD height="5" width="130" align="left"></TD>
									<TD height="5" width="70" align="right"></TD>
									<TD height="5" width="130" align="center"></TD>
									<TD height="5" width="70" align="center"></TD>
									<TD height="5" width="130" align="right"></TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 157px" height="157" vAlign="top" width="800" colSpan="8" align="left"><DISPLAYLAYOUT RowHeightDefault="20px" BorderCollapseDefault="Separate" Name="UltraWebGrid1" Version="3.00"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Width="800px" Height="230px">
												<DisplayLayout AutoGenerateColumns="False" AllowSortingDefault="OnClient" RowHeightDefault="20px"
													Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti"
													BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1"
													CellClickActionDefault="RowSelect">
													<AddNewBox>
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														</Style>
													</AddNewBox>
													<Pager>
														<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
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
													<ClientSideEvents DblClickHandler="UltraWebGrid1_DblClickHandler"></ClientSideEvents>
													<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
													<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
													<RowAlternateStyleDefault Cursor="Hand" BorderColor="Gray" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
													<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
														<Padding Left="3px"></Padding>
														<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
													</RowStyleDefault>
												</DisplayLayout>
												<Bands>
													<igtbl:UltraGridBand>
														<Columns>
															<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" BaseColumnName="ItemNum">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle Height="25px"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" BaseColumnName="ItemDrawNum">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" BaseColumnName="ItemName">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="단가구분" Key="UnitCostDistinction" BaseColumnName="UnitCostDistinction">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" BaseColumnName="CompanyName">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Hidden="True" BaseColumnName="BusinessRegistrationNum">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="시작공정코드" Key="BeginProcessCode" Hidden="True" BaseColumnName="BeginProcessCode">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="시작공정" Key="BeginProcess" BaseColumnName="BeginProcess">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="종료공정코드" Key="EndProcessCode" Hidden="True" BaseColumnName="EndProcessCode">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="종료공정" Key="EndProcess" BaseColumnName="EndProcess">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="발주비율" Key="OrderRate" Format="###,###,###.##" BaseColumnName="OrderRate">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="기준단가" Key="StandardUnitCost" Format="\ ###,###,###.##" BaseColumnName="StandardUnitCost">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="할인단가" Key="DiscountUnitCost" Format="\ ###,###,##0" BaseColumnName="DiscountUnitCost">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="적용시작일" Key="BeginDate" Format="yyyy-MM-dd" BaseColumnName="BeginDate">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="적용종료일" Key="EndDate" Format="yyyy-MM-dd" BaseColumnName="EndDate">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="레코드상태" Key="RecodingState" Hidden="True" BaseColumnName="RecodingState">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="등록자" Key="RegistrationPerson" BaseColumnName="RegistrationPerson">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="등록자ID" Key="RegistrationPersonID" Hidden="True" BaseColumnName="RegistrationPersonID">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" BaseColumnName="UpdatingPerson">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="번호" Key="UnitCostInfoIndex" Hidden="True" BaseColumnName="UnitCostInfoIndex">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															</igtbl:UltraGridColumn>
														</Columns>
													</igtbl:UltraGridBand>
												</Bands>
											</igtbl:ultrawebgrid>
											<ADDNEWBOX>
												<STYLE BorderStyle="Solid" BorderWidth="1px" BackColor="LightGray">
												</STYLE>
											</ADDNEWBOX>
											<PAGER>
												<STYLE BorderStyle="Solid" BorderWidth="1px" BackColor="LightGray">
												</STYLE>
											</PAGER>
											<HEADERSTYLEDEFAULT BorderStyle="Solid" BackColor="LightGray">
												<BORDERDETAILS WidthLeft="1px" WidthTop="1px" ColorTop="White" ColorLeft="White"></BORDERDETAILS>
											</HEADERSTYLEDEFAULT>
											<FRAMESTYLE BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" Width="800px"
												Height="150px"></FRAMESTYLE>
											<FOOTERSTYLEDEFAULT BorderStyle="Solid" BorderWidth="1px" BackColor="LightGray">
												<BORDERDETAILS WidthLeft="1px" WidthTop="1px" ColorTop="White" ColorLeft="White"></BORDERDETAILS>
											</FOOTERSTYLEDEFAULT>
											<EDITCELLSTYLEDEFAULT BorderStyle="None" BorderWidth="0px"></EDITCELLSTYLEDEFAULT>
											<ROWSTYLEDEFAULT BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray">
												<PADDING Left="3px"></PADDING>
												<BORDERDETAILS WidthLeft="0px" WidthTop="0px"></BORDERDETAILS>
											</ROWSTYLEDEFAULT>
										</DISPLAYLAYOUT><BANDS></BANDS></TD>
								</TR>
								<TR>
									<TD height="5" width="70" align="right"><FONT face="굴림"></FONT></TD>
									<TD height="5" width="130" align="left"></TD>
									<TD height="5" width="70" align="right"></TD>
									<TD height="5" width="130"></TD>
									<TD height="5" width="70" align="right"></TD>
									<TD height="5" width="130"></TD>
									<TD height="5" width="70" align="right"></TD>
									<TD height="5" width="130"></TD>
								</TR>
								<TR>
									<TD width="70" align="right"><FONT style="COLOR: red" face="굴림">*거래처&nbsp;</FONT></TD>
									<TD width="130" align="left"><DROPDOWNLAYOUT DropdownHeight="130px" HeaderClickAction="Select" RowHeightDefault="20px" BorderCollapse="Separate"
											DropdownWidth="325px"><igcmbo:webcombo id="wcCompany" runat="server" BorderStyle="Inset" BorderWidth="1px" BorderColor="Gray"
												BackColor="#EEEEE9" ForeColor="Black" Font-Size="10pt" Width="115px" Height="20px" Version="3.00" DropImageXP2="/ig_common/WebGrid3/ig_cmboDown2.bmp"
												DropImageXP1="/ig_common/WebGrid3/ig_cmboDown1.bmp" SelBackColor="10, 36, 106" SelectedIndex="-1" SelForeColor="White">
												<Columns>
													<igtbl:UltraGridColumn HeaderText="거래처명" Key="CompanyName" Width="250px" BaseColumnName="CompanyName">
														<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="대표자명" Key="PresidentName" Hidden="True" BaseColumnName="PresidentName">
														<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="사업자등록번호" Key="BusinessRegistrationNum" Width="300px" BaseColumnName="BusinessRegistrationNum">
														<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
													</igtbl:UltraGridColumn>
													<igtbl:UltraGridColumn HeaderText="번호" Key="CompanyInfoIndex" Hidden="True" BaseColumnName="CompanyInfoIndex">
														<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
													</igtbl:UltraGridColumn>
												</Columns>
												<ClientSideEvents AfterSelectChange="wcCompany_AfterSelectChange"></ClientSideEvents>
												<DropDownLayout DropdownWidth="550px" BorderCollapse="Separate" RowSelectors="No" RowHeightDefault="20px"
													HeaderClickAction="Select" AutoGenerateColumns="False" DropdownHeight="400px" TableLayout="Fixed"
													StationaryMargins="Header">
													<RowStyle BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="White">
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
											</igcmbo:webcombo>
											<ROWSTYLE BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray" BackColor="White">
												<BORDERDETAILS WidthLeft="0px" WidthTop="0px"></BORDERDETAILS>
											</ROWSTYLE>
											<SELECTEDROWSTYLE BackColor="DarkBlue" ForeColor="White"></SELECTEDROWSTYLE>
											<HEADERSTYLE BorderStyle="Solid" BackColor="LightGray">
												<BORDERDETAILS WidthLeft="1px" WidthTop="1px" ColorTop="White" ColorLeft="White"></BORDERDETAILS>
											</HEADERSTYLE>
											<FRAMESTYLE BorderStyle="Ridge" BorderWidth="2px" BackColor="Silver" Cursor="Default" Font-Names="Verdana"
												Font-Size="10pt" Width="100%" Height="100%"></FRAMESTYLE>
										</DROPDOWNLAYOUT><EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS></TD>
									<TD width="70" align="right"><FONT color="#ff0000" face="굴림">*시작공정&nbsp;</FONT></TD>
									<TD style="HEIGHT: 20px" width="130"><asp:dropdownlist id="dl_BeginProcess" runat="server" BackColor="#EEEEE9" Width="80px"></asp:dropdownlist></TD>
									<TD style="HEIGHT: 20px" width="70" align="right"><FONT color="#ff0000" face="굴림">*종료공정&nbsp;</FONT></TD>
									<TD style="HEIGHT: 20px" width="130"><asp:dropdownlist id="dl_EndProcess" runat="server" BackColor="#EEEEE9" Width="90px"></asp:dropdownlist></TD>
									<TD width="70" align="right"><FONT style="COLOR: red" face="굴림">*발주비율&nbsp;</FONT>
									</TD>
									<TD style="HEIGHT: 20px" width="130"><DROPDOWNLAYOUT DropdownHeight="130px" HeaderClickAction="Select" RowHeightDefault="20px" BorderCollapse="Separate"
											DropdownWidth="325px"><asp:textbox style="TEXT-ALIGN: right" id="tb_OrderRate" runat="server" BorderStyle="Solid" BorderWidth="1px"
												BorderColor="DimGray" BackColor="#EEEEE9" Width="90px" Height="20px">0</asp:textbox>%<ROWSTYLE BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray" BackColor="White">
												<BORDERDETAILS WidthLeft="0px" WidthTop="0px"></BORDERDETAILS>
											</ROWSTYLE><SELECTEDROWSTYLE BackColor="DarkBlue" ForeColor="White"></SELECTEDROWSTYLE><HEADERSTYLE BorderStyle="Solid" BackColor="LightGray">
												<BORDERDETAILS WidthLeft="1px" WidthTop="1px" ColorTop="White" ColorLeft="White"></BORDERDETAILS>
											</HEADERSTYLE><FRAMESTYLE BorderStyle="Ridge" BorderWidth="2px" BackColor="Silver" Cursor="Default" Font-Names="Verdana"
												Font-Size="10pt" Width="100%" Height="100%"></FRAMESTYLE></DROPDOWNLAYOUT><EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS></TD>
								</TR>
								<TR>
									<TD width="70" align="right"><FONT style="COLOR: red" face="굴림">*기준단가&nbsp;</FONT></TD>
									<TD width="130" align="left"><asp:textbox style="TEXT-ALIGN: right" id="tb_StandardUnitCost" runat="server" BorderStyle="Solid"
											BorderWidth="1px" BorderColor="DimGray" BackColor="#EEEEE9" Width="80" Height="20px"></asp:textbox>원<CALENDARLAYOUT FooterFormat="Today: {0:d}"></CALENDARLAYOUT><EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
									</TD>
									<TD width="70" align="right"><FONT face="굴림">할인단가&nbsp;</FONT></TD>
									<TD width="130" align="left"><asp:textbox style="TEXT-ALIGN: right" id="tb_DiscountUnitCost" runat="server" BorderStyle="Inset"
											BackColor="#EEEEE9" Width="80" Height="20px">0</asp:textbox>원<CALENDARLAYOUT FooterFormat="Today: {0:d}"></CALENDARLAYOUT><EXPANDEFFECTS ShadowColor="LightGray"></EXPANDEFFECTS>
									</TD>
									<TD width="70" align="right"><FONT style="COLOR: red" face="굴림">*적용시작일&nbsp;</FONT></TD>
									<TD width="130" align="left"><igsch:webdatechooser id="wdcBeginDate" runat="server" BorderStyle="Solid" BorderColor="DimGray" BackColor="#EEEEE9"
											Width="90px" Height="20px" Text="Null" NullDateLabel=" " MaxDate="2079-06-06">
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
									<TD width="70" align="right"><FONT face="굴림">적용종료일&nbsp;</FONT></TD>
									<TD width="130" align="left"><igsch:webdatechooser id="wdcEndDate" runat="server" BorderStyle="Solid" BorderColor="DimGray" BackColor="#EEEEE9"
											Width="90px" Height="20px" Text="Null" NullDateLabel=" " MaxDate="2079-06-06">
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
								</TR>
								<TR>
									<TD height="20" width="70" align="right"><FONT face="굴림">변경사유&nbsp; </FONT>
									</TD>
									<TD height="20" width="200" colSpan="2" align="left"><FONT face="굴림"><asp:textbox id="tbUpdateReason" runat="server" BorderStyle="Solid" BorderWidth="1px" BorderColor="DimGray"
												BackColor="#EEEEE9" Width="180px" Height="20px"></asp:textbox></FONT></TD>
									<TD height="20" width="130" align="left"><FONT face="굴림"></FONT></TD>
									<TD height="20" width="70" align="right"></TD>
									<TD height="20" width="130" align="left"></TD>
									<TD height="20" width="70" align="right"></TD>
									<TD height="20" width="130" align="left"><asp:button id="btHistory" runat="server" Width="60px" Height="20px" Text="이력보기"></asp:button></TD>
								</TR>
								<TR>
									<TD width="70" align="center"><asp:button id="bt_RelationView" runat="server" Width="60px" Height="20px" Text="참고보기"></asp:button></TD>
									<TD width="400" colSpan="4" align="left"><FONT face="굴림">&nbsp;
											<asp:button id="Button1" runat="server" Width="60px" Height="20px" Text="창고생성"></asp:button><INPUT style="WIDTH: 68px; HEIGHT: 22px" id="lb_Index" value="0" size="6" type="hidden"
												name="lb_Index" runat="server"><INPUT style="WIDTH: 30px" id="lb_ReferenceTable" type="hidden" name="lb_ReferenceTable"
												runat="server"><INPUT style="WIDTH: 30px" id="lb_ReferenceIndex" type="hidden" name="lb_ReferenceIndex"
												runat="server"> </FONT>
									</TD>
									<TD width="400" colSpan="3" align="right"><FONT face="굴림">&nbsp;</FONT>
										<asp:button id="bt_Clear" runat="server" Width="60px" Height="20px" Text="초기화"></asp:button><FONT face="굴림">&nbsp;</FONT>
										<asp:button id="bt_Update" runat="server" Width="60px" Height="20px" Text="수  정" Enabled="False"></asp:button><FONT face="굴림">&nbsp;</FONT>
										<asp:button id="bt_Delete" runat="server" Width="60px" Height="20px" Text="삭  제" Enabled="False"></asp:button><FONT face="굴림">&nbsp;</FONT>
										<asp:button id="bt_Registration" runat="server" Width="60px" Height="20px" Text="등  록"></asp:button><FONT face="굴림">&nbsp;</FONT></TD>
								</TR>
								<TR>
									<TD height="5" width="70" align="center"><FONT face="굴림"></FONT></TD>
									<TD height="5" width="130" align="left"></TD>
									<TD height="5" width="70"></TD>
									<TD height="5" width="130"><FONT face="굴림"></FONT></TD>
									<TD height="5" width="70"><FONT face="굴림"></FONT></TD>
									<TD height="5" width="400" colSpan="3" align="right"><FONT face="굴림"></FONT></TD>
								</TR>
								<TR>
									<TD height="10" colSpan="8"><igtbl:ultrawebgrid id="Ultrawebgrid2" runat="server" Width="800px" Height="135px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Extended" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="Ultrawebgrid2" TableLayout="Fixed" CellClickActionDefault="RowSelect"
												NoDataMessage="" AllowUpdateDefault="Yes">
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
													BorderStyle="Solid" BackColor="Silver" Height="135px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents AfterSelectChangeHandler="Ultrawebgrid2_AfterSelectChangeHandler"></ClientSideEvents>
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
														<igtbl:UltraGridColumn HeaderText="단가정보테이블" Key="StandardinfoTableID" Width="150px" BaseColumnName="StandardinfoTableID">
															<HeaderStyle Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목정보인덱스" Key="SITIndex" Width="50px" Hidden="True" BaseColumnName="SITIndex"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="자료실테이블" Key="CommunityDataTableID" Width="150px" BaseColumnName="CommunityDataTableID"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="자료실인덱스" Key="CDTIndex" Width="50px" Hidden="True" BaseColumnName="CDTIndex"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="제목" Key="Title" Width="498px" HeaderClickAction="SortSingle" BaseColumnName="Title"></igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="번호" Key="CRelationShipIndex" Width="50px" Hidden="True" BaseColumnName="CRelationShipIndex"></igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
