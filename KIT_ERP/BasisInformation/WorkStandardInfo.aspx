<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Page language="c#" Codebehind="WorkStandardInfo.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.WorkStandardInfo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WCStandardInfo</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<STYLE type="text/css">BODY { SCROLLBAR-FACE-COLOR: #ffffff; SCROLLBAR-HIGHLIGHT-COLOR: #ffffff; SCROLLBAR-SHADOW-COLOR: #ffffff; SCROLLBAR-3DLIGHT-COLOR: #808080; SCROLLBAR-ARROW-COLOR: #400040; SCROLLBAR-TRACK-COLOR: #ffffff; SCROLLBAR-DARKSHADOW-COLOR: #808080 }
		</STYLE>
		<SCRIPT type="text/javascript"><!--
		
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
		
		function UltraWebGrid1_DblClickHandler(gridName, cellId)
		{
		
			//Add code to handle your event here.
			var row = igtbl_getRowById(cellId);
			
			
			
			
			//WC명
			for(var i=0;i<document.WorkStandardInfo.dl_WCName.options.length;i++)
			{
				if(document.WorkStandardInfo.dl_WCName.options[i].value == row.getCellFromKey("WCName").getValue())
				{
					document.WorkStandardInfo.dl_WCName.options[i].selected=true;
				}
			}
			
			//주작업자
			for(var i=0;i<document.WorkStandardInfo.dl_MainWorker.options.length;i++)
			{
				if(document.WorkStandardInfo.dl_MainWorker.options[i].value == row.getCellFromKey("MainWorkerID").getValue())
				{
					document.WorkStandardInfo.dl_MainWorker.options[i].selected=true;
					
				}
			}
			
			//사용공구1
			for(var i=0;i<document.WorkStandardInfo.dl_UseTool1.options.length;i++)
			{
				if(document.WorkStandardInfo.dl_UseTool1.options[i].innerText == row.getCellFromKey("ToolName1").getValue())
				{
					document.WorkStandardInfo.dl_UseTool1.options[i].selected=true;
					
				}
			}
			
			//사용치구1
			for(var i=0;i<document.WorkStandardInfo.dl_UseJig1.options.length;i++)
			{
				if(document.WorkStandardInfo.dl_UseJig1.options[i].innerText == row.getCellFromKey("JigName1").getValue())
				{
					document.WorkStandardInfo.dl_UseJig1.options[i].selected=true;
					
				}
			}
			
			//사용공구2
			for(var i=0;i<document.WorkStandardInfo.dl_UseTool2.options.length;i++)
			{
				if(document.WorkStandardInfo.dl_UseTool2.options[i].innerText == row.getCellFromKey("ToolName2").getValue())
				{
					document.WorkStandardInfo.dl_UseTool2.options[i].selected=true;
					
				}
			}
			
			//사용치구2
			for(var i=0;i<document.WorkStandardInfo.dl_UseJig2.options.length;i++)
			{
				if(document.WorkStandardInfo.dl_UseJig2.options[i].innerText == row.getCellFromKey("JigName2").getValue())
				{
					document.WorkStandardInfo.dl_UseJig2.options[i].selected=true;
					
				}
			}
			
			//사용공구3
			for(var i=0;i<document.WorkStandardInfo.dl_UseTool3.options.length;i++)
			{
				if(document.WorkStandardInfo.dl_UseTool3.options[i].innerText == row.getCellFromKey("ToolName3").getValue())
				{
					document.WorkStandardInfo.dl_UseTool3.options[i].selected=true;
					
				}
			}
			
			//사용치구3
			for(var i=0;i<document.WorkStandardInfo.dl_UseJig3.options.length;i++)
			{
				if(document.WorkStandardInfo.dl_UseJig3.options[i].innerText == row.getCellFromKey("JigName3").getValue())
				{
					document.WorkStandardInfo.dl_UseJig3.options[i].selected=true;
					
				}
			}
			
			
			
			
			
			document.WorkStandardInfo.tb_PriorityOrder.value = row.getCellFromKey("PriorityOrder").getValue()	//우선순위
			document.WorkStandardInfo.hdProcess.value = row.getCellFromKey("ProcessSequenceNum").getValue() //공정순서
			document.WorkStandardInfo.lb_ProcessCode.value = row.getCellFromKey("ProcessCode").getValue() //공정코드
			
			
				
			document.WorkStandardInfo.tb_Cavity.value = row.getCellFromKey("Cavity").getValue();//Cavity
			document.WorkStandardInfo.tb_SetupTime.value = row.getCellFromKey("SetupTime").getValue()	//셋업타임
			document.WorkStandardInfo.tb_StandardTime.value = row.getCellFromKey("StandardTime").getValue();//표준시간
			document.WorkStandardInfo.tb_RealProcessingTime.value = row.getCellFromKey("RealProcessingTime").getValue()	//실가공시간
			document.WorkStandardInfo.tb_SpaceTime.value = row.getCellFromKey("SpaceTime").getValue();//여유시간
			document.WorkStandardInfo.tb_WaitTime.value = row.getCellFromKey("WaitTime").getValue();	//대기시간
			document.WorkStandardInfo.tb_LotSize.value = row.getCellFromKey("LotSize").getValue();//LotSize
			document.WorkStandardInfo.lb_Index.value = row.getCellFromKey("WorkStandardInfoIndex").getValue();
			
			document.WorkStandardInfo.bt_Update.disabled = false;
			document.WorkStandardInfo.bt_Delete.disabled = false;
			document.WorkStandardInfo.bt_Registration.disabled = true;
		}
		
		function Ultrawebgrid3_DblClickHandler(gridName, cellId){
			//Add code to handle your event here.
			var row = igtbl_getRowById(cellId);
			document.WorkStandardInfo.hdProcessNum.value = row.getCellFromKey("ProcessSequenceNum").getValue() //공정순서
			document.WorkStandardInfo.lb_ProcessCode.value = row.getCellFromKey("ProcessCode").getValue() //공정코드
			document.WorkStandardInfo.hdProcess.value = row.getCellFromKey("ProcessCode").getValue() //공정코드
			
		}
		
		function Ultrawebgrid2_AfterSelectChangeHandler(gridName, id){
			//Add code to handle your event here.
			var row = igtbl_getRowById(id);
			WorkStandardInfo.lb_ReferenceTable.value = row.getCellFromKey("CommunityDataTableID").getValue();
			WorkStandardInfo.lb_ReferenceIndex.value = row.getCellFromKey("CDTIndex").getValue();
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
		function Ultrawebgrid3_MouseOverHandler(gridName, id, button){
			//Add code to handle your event here.
			if(button == 0) { // Are we over a cell
            var cell = igtbl_getElementById(id);
            cell.style.cursor = 'hand';
            var parts = id.split("_");
            document.WorkStandardInfo.lb_RowIndex.value = parts[1];
            }
		}
		
		
--></SCRIPT>
	</HEAD>
	<body bgColor="#f7f6f6" MS_POSITIONING="GridLayout">
		<form id="WorkStandardInfo" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 0px" height="550"
				cellSpacing="0" cellPadding="0" width="800" border="0">
				<TR>
					<TD width="800">
						<table cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<td vAlign="bottom" align="right" width="70"><FONT face="굴림"></FONT></td>
								<td vAlign="bottom" align="left" width="230"></td>
								<TD vAlign="bottom" align="right"><FONT face="굴림"></FONT></TD>
								<td vAlign="bottom" align="left" width="400"></td>
								<TD vAlign="bottom" align="right" width="200"><asp:linkbutton id="LinkButton1" runat="server" Font-Size="9pt">작업표준정보보기&nbsp;</asp:linkbutton></TD>
							</TR>
						</table>
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid"
							align="left"><LEGEND>[입력]</LEGEND>
							<TABLE id="n" style="WIDTH: 800px" cellSpacing="0" cellPadding="0">
								<tr>
									<td align="left" width="600" colSpan="6" height="30"><uc1:itemsearchcontrol id="ItemSearchControl1" runat="server"></uc1:itemsearchcontrol><asp:linkbutton id="LinkButton2" runat="server" Visible="False">LinkButton</asp:linkbutton></td>
									<td align="right" width="70" height="30">자산분류&nbsp;</td>
									<td align="left" width="130" height="30"><asp:textbox id="tb_PropertyClassification" runat="server" Font-Size="8pt" BorderWidth="1px"
											BorderColor="DimGray" BorderStyle="Solid" BackColor="#EEEEE9" Height="20px" Width="100px" ReadOnly="True"></asp:textbox></td>
								</tr>
								<TR>
									<TD align="right" width="800" colSpan="8"><FONT face="굴림"><igtbl:ultrawebgrid id="Ultrawebgrid3" runat="server" Height="100px" Width="800px">
												<DisplayLayout CompactRendering="False" StationaryMargins="Header" AutoGenerateColumns="False"
													AllowSortingDefault="OnClient" RowHeightDefault="20px" RowSizingDefault="Free" Version="3.00"
													SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti"
													BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="Ultrawebgrid3"
													TableLayout="Fixed" CellClickActionDefault="RowSelect">
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
													<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderColor="DimGray"
														BorderStyle="Solid" BackColor="Silver" Height="100px"></FrameStyle>
													<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
														<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
													</FooterStyleDefault>
													<ClientSideEvents DblClickHandler="Ultrawebgrid3_DblClickHandler" MouseOverHandler="Ultrawebgrid3_MouseOverHandler"></ClientSideEvents>
													<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
													<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
													<RowAlternateStyleDefault Cursor="Hand" BorderColor="DarkGray" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
													<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
														<Padding Left="3px"></Padding>
														<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
													</RowStyleDefault>
												</DisplayLayout>
												<Bands>
													<igtbl:UltraGridBand AllowSorting="OnClient" AllowColSizing="Free" AllowColumnMoving="OnServer" RowSizing="Free">
														<Columns>
															<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" Width="200px" BaseColumnName="ItemNum">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" Width="200px" BaseColumnName="ItemDrawNum">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" Width="200px" BaseColumnName="ItemName">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="공정순서" Key="ProcessSequenceNum" Width="100px" BaseColumnName="ProcessSequenceNum">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="공정코드" Key="ProcessCode" Hidden="True" BaseColumnName="ProcessCode">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="공정명" Key="SmallClassificationName" Width="100px" BaseColumnName="SmallClassificationName">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="번호" Key="WorkStandardInfoIndex" Hidden="True" BaseColumnName="WorkStandardInfoIndex">
																<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
																<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
															</igtbl:UltraGridColumn>
														</Columns>
													</igtbl:UltraGridBand>
												</Bands>
											</igtbl:ultrawebgrid></FONT></TD>
								</TR>
								<TR>
									<TD vAlign="top" align="right" width="800" colSpan="8" height="5"></TD>
								</TR>
								<TR>
									<td vAlign="top" align="right" width="800" colSpan="8"><igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="110px" Width="800px">
											<DisplayLayout CompactRendering="False" StationaryMargins="Header" AutoGenerateColumns="False"
												AllowSortingDefault="OnClient" RowHeightDefault="20px" RowSizingDefault="Free" Version="3.00"
												SelectTypeRowDefault="Single" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti"
												BorderCollapseDefault="Separate" AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1"
												TableLayout="Fixed" CellClickActionDefault="RowSelect">
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
												<FrameStyle Width="800px" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana" BorderColor="DimGray"
													BorderStyle="Solid" BackColor="Silver" Height="110px"></FrameStyle>
												<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
													<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
												</FooterStyleDefault>
												<ClientSideEvents DblClickHandler="UltraWebGrid1_DblClickHandler"></ClientSideEvents>
												<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>
												<SelectedRowStyleDefault Cursor="Hand" ForeColor="White" BackColor="Navy"></SelectedRowStyleDefault>
												<RowAlternateStyleDefault Cursor="Hand" BorderColor="DarkGray" BackColor="LightSteelBlue"></RowAlternateStyleDefault>
												<RowStyleDefault Cursor="Hand" BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" BackColor="#EBEFF6">
													<Padding Left="3px"></Padding>
													<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
												</RowStyleDefault>
											</DisplayLayout>
											<Bands>
												<igtbl:UltraGridBand AllowSorting="OnClient" AllowColSizing="Free" AllowColumnMoving="OnServer" RowSizing="Free">
													<Columns>
														<igtbl:UltraGridColumn HeaderText="공정순서" Key="ProcessSequenceNum" BaseColumnName="ProcessSequenceNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="25px"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정코드" Key="ProcessCode" Hidden="True" BaseColumnName="ProcessCode">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정명" Key="SmallClassificationName" BaseColumnName="SmallClassificationName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="WC명" Key="WCName" BaseColumnName="WCName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="우선순위" Key="PriorityOrder" BaseColumnName="PriorityOrder">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="주작업자ID" Key="MainWorkerID" Hidden="True" BaseColumnName="MainWorkerID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="주작업자" Key="MainWorker" BaseColumnName="MainWorker">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사용공구1" Key="ToolName1" BaseColumnName="ToolName1">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사용공구2" Key="ToolName2" BaseColumnName="ToolName2">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사용공구3" Key="ToolName3" BaseColumnName="ToolName3">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사용치구1" Key="JigName1" BaseColumnName="JigName1">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사용치구2" Key="JigName2" BaseColumnName="JigName2">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="사용치구3" Key="JigName3" BaseColumnName="JigName3">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="셋업타임" Key="SetupTime" BaseColumnName="SetupTime">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="실가공시간" Key="RealProcessingTime" BaseColumnName="RealProcessingTime">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="여유시간" Key="SpaceTime" BaseColumnName="SpaceTime">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="표준시간" Key="StandardTime" BaseColumnName="StandardTime">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="대기시간" Key="WaitTime" BaseColumnName="WaitTime">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="LotSize" Key="LotSize" BaseColumnName="LotSize">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="Cavity" Key="Cavity" BaseColumnName="Cavity">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
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
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="등록일" Key="RegistrationDate" Format="yyyy-MM-dd" BaseColumnName="RegistrationDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자" Key="UpdatingPerson" BaseColumnName="UpdatingPerson">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정자ID" Key="UpdatingPersonID" Hidden="True" BaseColumnName="UpdatingPersonID">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="수정일" Key="UpdatingDate" Format="yyyy-MM-dd" BaseColumnName="UpdatingDate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="번호" Key="WorkStandardInfoIndex" Hidden="True" BaseColumnName="WorkStandardInfoIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목번호" Key="ItemNum" BaseColumnName="ItemNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="도면번호" Key="ItemDrawNum" BaseColumnName="ItemDrawNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="품목명" Key="ItemName" BaseColumnName="ItemName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
															<HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center"></HeaderStyle>
														</igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></td>
								</TR>
								<TR>
									<TD align="right" width="70" height="5"></TD>
									<TD align="left" width="130" height="5"></TD>
									<TD align="right" width="70" height="5"></TD>
									<TD align="left" width="130" height="5"></TD>
									<TD align="right" width="70" height="5"></TD>
									<TD align="left" width="130" height="5"></TD>
									<TD align="right" width="70" height="5"></TD>
									<TD align="left" width="130" height="5"></TD>
								</TR>
								<tr>
									<td align="right" width="70"><FONT face="굴림" color="#ff0000">*WC명&nbsp;</FONT></td>
									<td align="left" width="130"><asp:dropdownlist id="dl_WCName" runat="server" Font-Size="8pt" BackColor="#EEEEE9" Height="20px"
											Width="100px"></asp:dropdownlist></td>
									<td align="right" width="70"><FONT style="COLOR: #ff0000" face="굴림">*우선순위&nbsp;</FONT>
									</td>
									<td align="left" width="130"><asp:textbox id="tb_PriorityOrder" style="TEXT-ALIGN: right" runat="server" Font-Size="8pt" BorderWidth="1px"
											BorderColor="DimGray" BorderStyle="Solid" BackColor="#EEEEE9" Height="20px" Width="100px"></asp:textbox></td>
									<TD align="right" width="70"><FONT face="굴림" color="#ff0000">*주작업자&nbsp;</FONT>
									</TD>
									<TD align="left" width="130"><asp:dropdownlist id="dl_MainWorker" runat="server" Font-Size="8pt" BackColor="#EEEEE9" Height="20px"
											Width="100px"></asp:dropdownlist></TD>
									<TD align="right" width="70"><FONT face="굴림">Cavity&nbsp;</FONT>
									</TD>
									<TD align="left" width="130"><asp:textbox id="tb_Cavity" style="TEXT-ALIGN: right" runat="server" Font-Size="8pt" BorderWidth="1px"
											BorderColor="DimGray" BorderStyle="Solid" BackColor="#EEEEE9" Height="20px" Width="100px">0</asp:textbox></TD>
								</tr>
								<tr>
									<td align="right" width="70"><FONT face="굴림">사용공구1&nbsp;</FONT></td>
									<td align="left" width="130"><FONT face="굴림"><asp:dropdownlist id="dl_UseTool1" runat="server" Font-Size="8pt" BackColor="#EEEEE9" Height="20px"
												Width="100px"></asp:dropdownlist></FONT></td>
									<td align="right" width="70"><FONT face="굴림">사용치구2&nbsp;</FONT>
									</td>
									<TD align="left" width="130"><asp:dropdownlist id="dl_UseJig1" runat="server" Font-Size="8pt" BackColor="#EEEEE9" Height="20px"
											Width="100px"></asp:dropdownlist></TD>
									<TD align="right" width="70"><FONT face="굴림"><FONT face="굴림">사용공구2</FONT>&nbsp;</FONT></TD>
									<TD align="left" width="130"><asp:dropdownlist id="dl_UseTool2" runat="server" Font-Size="8pt" BackColor="#EEEEE9" Height="20px"
											Width="100px"></asp:dropdownlist></TD>
									<TD align="right" width="70"><FONT face="굴림"><FONT face="굴림">사용치구2</FONT>&nbsp;</FONT></TD>
									<TD align="left" width="130"><asp:dropdownlist id="dl_UseJig2" runat="server" Font-Size="8pt" BackColor="#EEEEE9" Height="20px"
											Width="100px"></asp:dropdownlist></TD>
								</tr>
								<TR>
									<TD align="right" width="70"><FONT face="굴림">사용공구3&nbsp;</FONT></TD>
									<TD align="left" width="130"><asp:dropdownlist id="dl_UseTool3" runat="server" Font-Size="8pt" BackColor="#EEEEE9" Height="20px"
											Width="100px"></asp:dropdownlist></TD>
									<TD align="right" width="70"><FONT face="굴림">사용치구3&nbsp;</FONT></TD>
									<TD align="left" width="130"><asp:dropdownlist id="dl_UseJig3" runat="server" Font-Size="8pt" BackColor="#EEEEE9" Height="20px"
											Width="100px"></asp:dropdownlist></TD>
									<TD align="right" width="70"><FONT face="굴림">셋업타임&nbsp;</FONT></TD>
									<TD align="left" width="130"><FONT face="굴림" size="2"><asp:textbox id="tb_SetupTime" style="TEXT-ALIGN: right" runat="server" Font-Size="8pt" BorderWidth="1px"
												BorderColor="DimGray" BorderStyle="Solid" BackColor="#EEEEE9" Height="20px" Width="90px">0</asp:textbox>초</FONT></TD>
									<TD align="right" width="70"><FONT face="굴림" color="#ff0000">표준시간&nbsp;</FONT></TD>
									<TD align="left" width="130"><FONT face="굴림" size="2"><FONT face="굴림" size="2"><asp:textbox id="tb_StandardTime" style="TEXT-ALIGN: right" runat="server" Font-Size="8pt" BorderWidth="1px"
													BorderColor="DimGray" BorderStyle="Solid" BackColor="#EEEEE9" Height="20px" Width="100px">0</asp:textbox>초</FONT></FONT></TD>
								</TR>
								<TR>
									<TD align="right" width="70"><FONT face="굴림">실가공시간&nbsp;</FONT></TD>
									<TD align="left" width="130"><FONT face="굴림" size="2"><asp:textbox id="tb_RealProcessingTime" style="TEXT-ALIGN: right" runat="server" Font-Size="8pt"
												BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid" BackColor="#EEEEE9" Height="20px" Width="90px">0</asp:textbox>초</FONT>
									</TD>
									<TD align="right" width="70"><FONT face="굴림">여유시간&nbsp;</FONT></TD>
									<TD align="left" width="130"><FONT face="굴림" size="2"><FONT face="굴림" size="2"><asp:textbox id="tb_SpaceTime" style="TEXT-ALIGN: right" runat="server" Font-Size="8pt" BorderWidth="1px"
													BorderColor="DimGray" BorderStyle="Solid" BackColor="#EEEEE9" Height="20px" Width="90px">0</asp:textbox>초</FONT></FONT></TD>
									<TD align="right" width="70"><FONT face="굴림">대기시간&nbsp;</FONT></TD>
									<TD align="left" width="130"><FONT face="굴림" size="2"><asp:textbox id="tb_WaitTime" style="TEXT-ALIGN: right" runat="server" Font-Size="8pt" BorderWidth="1px"
												BorderColor="DimGray" BorderStyle="Solid" BackColor="#EEEEE9" Height="20px" Width="90px">0</asp:textbox>초</FONT>
									</TD>
									<TD align="right" width="70"><FONT face="굴림">LotSize&nbsp;</FONT></TD>
									<TD align="left" width="130"><FONT face="굴림" size="2"><asp:textbox id="tb_LotSize" style="TEXT-ALIGN: right" runat="server" Font-Size="8pt" BorderWidth="1px"
												BorderColor="DimGray" BorderStyle="Solid" BackColor="#EEEEE9" Height="20px" Width="100px">0</asp:textbox></FONT></TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="5"></TD>
									<TD align="left" width="130" height="5"></TD>
									<TD align="right" width="70" height="5"></TD>
									<TD align="left" width="130" height="5"></TD>
									<TD align="right" width="70" height="5"></TD>
									<TD align="left" width="130" height="5"></TD>
									<TD align="right" width="70" height="5"></TD>
									<TD align="left" width="130" height="5"></TD>
								</TR>
								<TR>
									<TD align="right" width="70"><asp:button id="bt_RelationView" runat="server" Font-Size="8pt" Height="20px" Width="60px" Text="참고보기"></asp:button></TD>
									<TD align="right" width="130">
										<asp:button id="Button2" runat="server" Width="90px" Height="20px" Text="작업표준 복사"></asp:button></TD>
									<TD align="right" width="70"><INPUT id="lb_ReferenceTable" style="WIDTH: 30px" type="hidden" name="lb_ReferenceTable"
											runat="server"><INPUT id="lb_ReferenceIndex" style="WIDTH: 30px" type="hidden" name="lb_ReferenceIndex"
											runat="server"></TD>
									<TD align="left" width="130"><FONT face="굴림"><INPUT id="hdProcessNum" style="WIDTH: 28px; HEIGHT: 22px" type="hidden" size="1" value="0"
												name="Hidden1" runat="server"><INPUT id="hdItemNum" style="WIDTH: 34px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1"
												runat="server"><INPUT id="lb_Index" style="WIDTH: 50px" type="hidden" value="0" name="Hidden1" runat="server"></FONT></TD>
									<TD align="right" width="400" colSpan="4"><INPUT id="hdProcess" style="WIDTH: 50px; HEIGHT: 20px" type="hidden" value="0" name="Hidden1"
											runat="server"><asp:button id="bt_Clear" runat="server" Font-Size="10pt" Height="20px" Width="60px" Text="초기화"></asp:button><FONT face="굴림">&nbsp;</FONT><asp:button id="bt_Update" runat="server" Font-Size="10pt" Height="20px" Width="60px" Text="수  정"
											Enabled="False"></asp:button><FONT face="굴림">&nbsp;</FONT><asp:button id="bt_Delete" runat="server" Font-Size="10pt" Height="20px" Width="60px" Text="삭  제"
											Enabled="False"></asp:button><FONT face="굴림">&nbsp;</FONT><asp:button id="bt_Registration" runat="server" Font-Size="10pt" Height="20px" Width="60px"
											Text="등  록"></asp:button>&nbsp;</TD>
								</TR>
								<TR>
									<TD width="70" height="5"><FONT face="굴림"></FONT></TD>
									<TD align="left" width="130" height="5"></TD>
									<TD width="70" height="5"></TD>
									<TD align="left" width="130" height="5"></TD>
									<TD align="right" width="400" colSpan="4" height="5"></TD>
								</TR>
								<TR>
									<TD width="70" colSpan="8"><FONT face="굴림"><igtbl:ultrawebgrid id="Ultrawebgrid2" runat="server" Height="140px" Width="800px">
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
														BorderStyle="Solid" BackColor="Silver" Height="140px"></FrameStyle>
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
															<igtbl:UltraGridColumn HeaderText="작업표준정보테이블" Key="StandardinfoTableID" Width="150px" BaseColumnName="StandardinfoTableID">
																<HeaderStyle Height="25px"></HeaderStyle>
															</igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="품목정보인덱스" Key="SITIndex" Width="50px" Hidden="True" BaseColumnName="SITIndex"></igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="자료실테이블" Key="CommunityDataTableID" Width="150px" BaseColumnName="CommunityDataTableID"></igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="자료실인덱스" Key="CDTIndex" Width="50px" Hidden="True" BaseColumnName="CDTIndex"></igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="제목" Key="Title" Width="500px" HeaderClickAction="SortSingle" BaseColumnName="Title"></igtbl:UltraGridColumn>
															<igtbl:UltraGridColumn HeaderText="번호" Key="CRelationShipIndex" Width="50px" Hidden="True" BaseColumnName="CRelationShipIndex"></igtbl:UltraGridColumn>
														</Columns>
														<RowTemplateStyle BorderColor="White" BorderStyle="Ridge" BackColor="White">
															<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
														</RowTemplateStyle>
														<RowEditTemplate>
															<P align="right">작업표준정보테이블&nbsp; <INPUT id="igtbl_TextBox_0_0" style="WIDTH: 150px" type="text" columnKey="StandardinfoTableID"><BR>
																자료실테이블&nbsp; <INPUT id="igtbl_TextBox_0_2" style="WIDTH: 150px" type="text" columnKey="CommunityDataTableID"><BR>
																제목&nbsp; <INPUT id="igtbl_TextBox_0_4" style="WIDTH: 150px" type="text" columnKey="Title"><BR>
																번호&nbsp; <INPUT id="igtbl_TextBox_0_5" style="WIDTH: 150px" type="text" columnKey="CRelationShipIndex"><BR>
															</P>
															<BR>
															<P align="center">
																<INPUT id="igtbl_reOkBtn" style="WIDTH: 50px" onclick="igtbl_gRowEditButtonClick(event);"
																	type="button" value="OK">&nbsp; <INPUT id="igtbl_reCancelBtn" style="WIDTH: 50px" onclick="igtbl_gRowEditButtonClick(event);"
																	type="button" value="Cancel"></P>
														</RowEditTemplate>
													</igtbl:UltraGridBand>
												</Bands>
											</igtbl:ultrawebgrid></FONT></TD>
								</TR>
								<tr>
									<td colSpan="8" height="10"><FONT face="굴림"></FONT></td>
								</tr>
							</TABLE>
						</FIELDSET>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="lb_RowIndex" style="Z-INDEX: 108; LEFT: 536px; WIDTH: 50px; POSITION: absolute; TOP: 600px; HEIGHT: 20px"
				type="hidden" name="Hidden1" runat="server">
			<asp:textbox id="TextBox1" style="Z-INDEX: 102; LEFT: 16px; POSITION: absolute; TOP: 600px" runat="server"
				Visible="False" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid" BackColor="#EEEEE9"
				Height="20px" Width="168px"></asp:textbox><asp:button id="pop" style="Z-INDEX: 103; LEFT: 184px; POSITION: absolute; TOP: 608px" runat="server"
				Visible="False" Height="20px" Width="20px" Text="..."></asp:button><INPUT id="lb_ItemIndex" style="Z-INDEX: 104; LEFT: 328px; WIDTH: 50px; POSITION: absolute; TOP: 600px; HEIGHT: 20px"
				type="hidden" value="0" name="Hidden1" runat="server"><INPUT id="lb_Item" style="Z-INDEX: 105; LEFT: 384px; WIDTH: 50px; POSITION: absolute; TOP: 600px; HEIGHT: 20px"
				type="hidden" name="Hidden1" runat="server"><INPUT id="ProcessSequenceInfoIndex" style="Z-INDEX: 106; LEFT: 440px; WIDTH: 30px; POSITION: absolute; TOP: 600px"
				type="hidden" name="ItemIndex" runat="server"><INPUT id="lb_ProcessCode1" style="Z-INDEX: 107; LEFT: 472px; WIDTH: 50px; POSITION: absolute; TOP: 600px; HEIGHT: 20px"
				type="hidden" name="Hidden1" runat="server">
			<asp:button id="bt_WorkStandard" style="Z-INDEX: 109; LEFT: 624px; POSITION: absolute; TOP: 616px"
				runat="server" Font-Size="9pt" Visible="False" Height="20px" Width="90px" Text="작업표준보기"></asp:button><INPUT id="lb_ProcessCode" style="Z-INDEX: 110; LEFT: 280px; POSITION: absolute; TOP: 600px"
				type="hidden" size="1" name="Hidden1" runat="server"></form>
	</body>
</HTML>
