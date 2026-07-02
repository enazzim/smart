<%@ Page language="c#" Codebehind="StorehouseMoving.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BusinessManagement.StorehouseMoving" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>StorehouseMoving</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<SCRIPT type="text/javascript"><!--

		function OK(text)
		{
			if(confirm(text + "하시겠습니까?"))
				return true;
			else
				return false;
		}
		
		function OnKeyDown_Float(obj) {   
						if (event.ctrlKey || event.shiftKey || event.altKey)   {
							SetEventPass();
							return true;
						}
						var sFieldValue = new String(obj.value);
						// 숫자검사 + Numeric Keypad 숫자검사
						if( (event.keyCode<45 || event.keyCode>57) && (event.keyCode<96 || event.keyCode>105))  {
    						if( event.keyCode == 110 || event.keyCode == 190 ) {
    							// "." 가 2개있는지 검사
	    						if( sFieldValue.indexOf(".") != -1 )	{
	    							SetEventCancel();
									return obj.value;
	    						}
							} else if( !IsNumericKey() ) {
	    						SetEventCancel();
								return obj.value;
							}	    
						} else {    
    						if( sFieldValue.indexOf(".") == -1 ) {
	    						// "0" 을 입력했을 경우 "0" 다음에 "." 가 아니면 무시하여야 한다.	
	    						// 48, 96(Numeric Keypad)
								if( sFieldValue.substr(0,1) == "0" && (event.keyCode == 48 || event.keyCode == 96) ) {
									SetEventCancel();
									return obj.value;
								}
							}
						}
						SetEventPass();
						return true;
					}

					function OnFocus_Obj(obj) {
						obj.select();
					}
					
					function OnBlur_Float(obj) {
						if (ValidateFloat(obj.value) == false) {
							obj.value = "";
    						return false;
						}
					}
					
					function ValidateFloat(a_sValue)	{	
						var s_FieldValue = new String(a_sValue);
						
						if (isNaN(s_FieldValue))
							return false;
							
						return true;
					}
		
//////////////////////////////////////////////////////////////////////////////////////////
//					Currency															//
//////////////////////////////////////////////////////////////////////////////////////////
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
							} else if( !IsCurrencyKey() ){
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
		
	


		function UltraWebGrid1_MouseOverHandler(gridName, id, button){
			//Add code to handle your event here.
			if(button == 0) { // Are we over a cell
            var cell = igtbl_getElementById(id);
            cell.style.cursor = 'hand';
            //var label = igtbl_getElementById("lb_RowIndex");
            var parts = id.split("_");
            document.StorehouseMoving.lb_RowIndex.value = parts[1];
            }
            
		}
		
		function UltraWebGrid1_AfterSelectChangeHandler(gridName, id){
			//Add code to handle your event here.
			document.StorehouseMoving.tb_ItemNum.value = document.getElementById(id).children[0].innerText;
			document.StorehouseMoving.tb_ItemDrawNum.value = document.getElementById(id).children[1].innerText;
			document.StorehouseMoving.tb_ItemName.value = document.getElementById(id).children[2].innerText;
			document.StorehouseMoving.lb_RowSelectIndex.value = document.StorehouseMoving.lb_RowIndex.value
		}
		
--></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bgColor="#f7f6f6">
		<form id="StorehouseMoving" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" height="550"
				cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
				<TR>
					<TD width="20"></TD>
					<TD vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND style="FONT-SIZE: 9pt" align="top">[검색조건]</LEGEND>
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="800">
								<TR>
									<TD width="600" height="30">
										<uc1:ItemSearchControl id="ItemSearchControl1" runat="server"></uc1:ItemSearchControl></TD>
									<TD align="right" width="70" height="30"><FONT style="FONT-SIZE: 9pt" face="굴림">창 
											고&nbsp; </FONT>
									</TD>
									<TD width="130" height="30">
										<asp:dropdownlist id="dl_Store" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Height="20px" Width="100px">
											<asp:ListItem Value="0">- 선 택 -</asp:ListItem>
											<asp:ListItem Value="1">영업1창고</asp:ListItem>
											<asp:ListItem Value="2">영업2창고</asp:ListItem>
											<asp:ListItem Value="3">영업3창고</asp:ListItem>
										</asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD height="30"></TD>
									<TD align="right" colSpan="2" height="30"><FONT face="굴림"></FONT>
										<asp:button id="bt_Clear" runat="server" Height="20px" Width="60px" Text="초기화"></asp:button><FONT face="굴림">&nbsp;</FONT>
										<asp:button id="bt_Search" runat="server" Height="20px" Width="60px" Text="검  색"></asp:button><FONT face="굴림">&nbsp;</FONT></TD>
								</TR>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="20"></TD>
					<TD vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"></FONT><FONT face="굴림"></FONT><BR>
							<LEGEND style="FONT-SIZE: 9pt" align="top">
								[검색결과]</LEGEND>
							<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="371px" Width="800px">
								<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
									RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer"
									HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
									RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed" CellClickActionDefault="RowSelect">
									<AddNewBox>
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										</Style>
									</AddNewBox>
									<Pager PageSize="16" StyleMode="ComboBox" AllowPaging="True">
										<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										</Style>
									</Pager>
									<HeaderStyleDefault Cursor="Hand" BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</HeaderStyleDefault>
									<RowSelectorStyleDefault Cursor="Hand"></RowSelectorStyleDefault>
									<FrameStyle Width="800px" BorderWidth="1px" Font-Size="9pt" Font-Names="Verdana" BorderColor="DimGray"
										BorderStyle="Solid" BackColor="Silver" Height="371px"></FrameStyle>
									<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
										<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
									</FooterStyleDefault>
									<ClientSideEvents AfterSelectChangeHandler="UltraWebGrid1_AfterSelectChangeHandler" MouseOverHandler="UltraWebGrid1_MouseOverHandler"></ClientSideEvents>
									<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
									<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
									<RowAlternateStyleDefault Cursor="Hand" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
									<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
										<Padding Left="3px"></Padding>
										<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
									</RowStyleDefault>
								</DisplayLayout>
								<Bands>
									<igtbl:UltraGridBand>
										<Columns>
											<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="150px" HeaderClickAction="Select" BaseColumnName="ItemNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Width="150px" HeaderClickAction="SortMulti"
												BaseColumnName="ItemDrawNum">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="198px" HeaderClickAction="SortMulti" BaseColumnName="ItemName">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="재고수량" Key="StockQuantity12" Width="150px" Format="###,###,###.##" HeaderClickAction="SortMulti"
												BaseColumnName="StockQuantity12">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="재고금액" Key="StockCost12" Width="150px" Format="\ ###,###,##0" HeaderClickAction="SortMulti"
												BaseColumnName="StockCost12">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
											<igtbl:UltraGridColumn HeaderText="번호" Key="StorehouseIndex" Width="150px" Hidden="True" HeaderClickAction="SortMulti"
												BaseColumnName="StorehouseIndex">
												<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
												<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
											</igtbl:UltraGridColumn>
										</Columns>
									</igtbl:UltraGridBand>
								</Bands>
							</igtbl:ultrawebgrid></FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD width="20"></TD>
					<TD vAlign="top" align="center" width="800">
						<FIELDSET style="BORDER-RIGHT: #696969 2px solid; BORDER-TOP: #696969 2px solid; BORDER-LEFT: #696969 2px solid; WIDTH: 100%; BORDER-BOTTOM: #696969 2px solid"><LEGEND style="FONT-SIZE: 9pt" align="top">[이 
								동]</LEGEND>
							<TABLE id="Table3" style="HEIGHT: 5px" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD style="HEIGHT: 27px" align="right" width="70"><FONT style="FONT-SIZE: 9pt" face="굴림">품목번호</FONT></TD>
									<TD style="WIDTH: 101px; HEIGHT: 27px" align="left">
										<asp:textbox id="tb_ItemNum" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Height="20px"
											Width="100px" BorderStyle="Inset"></asp:textbox></TD>
									<TD style="HEIGHT: 27px" align="right" width="70"><FONT style="FONT-SIZE: 9pt" face="굴림">도면번호</FONT></TD>
									<TD style="WIDTH: 85px; HEIGHT: 27px" align="left">
										<asp:textbox id="tb_ItemDrawNum" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Height="20px"
											Width="100px" BorderStyle="Inset"></asp:textbox></TD>
									<TD style="WIDTH: 55px; HEIGHT: 27px" align="right"><FONT style="FONT-SIZE: 9pt" face="굴림">품 
											목 명</FONT></TD>
									<TD style="WIDTH: 49px; HEIGHT: 27px" align="left">
										<asp:textbox id="tb_ItemName" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Height="20px"
											Width="100px" BorderStyle="Inset"></asp:textbox></TD>
									<TD style="WIDTH: 183px; HEIGHT: 27px" align="right"></TD>
									<TD style="WIDTH: 160px; HEIGHT: 27px" align="left"><INPUT id="lb_RowIndex" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" runat="server"><INPUT id="lb_RowSelectIndex" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" name="Hidden1"
											runat="server"></TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 27px" align="right" width="70"><FONT style="FONT-SIZE: 9pt" face="굴림">이동창고</FONT></TD>
									<TD style="WIDTH: 101px; HEIGHT: 27px" align="left"><FONT face="굴림">
											<asp:dropdownlist id="dl_MovingStore" runat="server" Font-Size="9pt" BackColor="#EEEEE9" Height="20px"
												Width="100px">
												<asp:ListItem Value="0">- 선 택 -</asp:ListItem>
												<asp:ListItem Value="1">영업1창고</asp:ListItem>
												<asp:ListItem Value="2">영업2창고</asp:ListItem>
												<asp:ListItem Value="3">영업3창고</asp:ListItem>
											</asp:dropdownlist></FONT></TD>
									<TD style="HEIGHT: 27px" align="right" width="70"><FONT face="굴림">이동수량</FONT></TD>
									<TD style="WIDTH: 85px; HEIGHT: 27px" align="left">
										<asp:textbox id="tb_MovingQuantity" style="TEXT-ALIGN: right" runat="server" Font-Size="9pt"
											BackColor="#EEEEE9" Height="20px" Width="100px" BorderStyle="Inset">0</asp:textbox></TD>
									<TD align="right" colSpan="4"><FONT face="굴림">
											<asp:Button id="bt_Reset" runat="server" Height="20px" Width="60px" Text="초기화"></asp:Button>&nbsp;
											<asp:Button id="bt_Move" runat="server" Height="20px" Width="60px" Text="이  동"></asp:Button>&nbsp;</FONT></TD>
								</TR>
							</TABLE>
							<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="800">
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
