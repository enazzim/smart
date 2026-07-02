<%@ Page language="c#" Codebehind="ProcessSequenceInfo.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.ProcessSequenceNumInfor" %>
<%@ Register TagPrefix="uc1" TagName="ItemSearchControl" Src="../Common/ItemSearchControl/ItemSearchControl.ascx" %>
<%@ Register TagPrefix="igcmbo" Namespace="Infragistics.WebUI.WebCombo" Assembly="Infragistics.WebUI.WebCombo.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3.1, Version=3.1.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ProcessSequenceNumInfo</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
		
		function DoPost()
		{
			__doPostBack('LinkButton2','');
		}
		
		function UltraWebGrid1_DblClickHandler(gridName, cellId)
		{
		
			//Add code to handle your event here.
			var row = igtbl_getRowById(cellId);
			
			
			//공정명
			for(var i=0;i<document.ProcessSequenceNumInfo.dl_ProcessCode.options.length;i++)
			{
				if(document.ProcessSequenceNumInfo.dl_ProcessCode.options[i].value == row.getCellFromKey("ProcessCode").getValue())
				{
					document.ProcessSequenceNumInfo.dl_ProcessCode.options[i].selected=true;
					
				}
			}
			
			
			//작업구분
			for(var i=0;i<document.ProcessSequenceNumInfo.dl_WorkDistinction.options.length;i++)
			{
				if(document.ProcessSequenceNumInfo.dl_WorkDistinction.options[i].value == row.getCellFromKey("WorkDistinction").getValue())
				{
					document.ProcessSequenceNumInfo.dl_WorkDistinction.options[i].selected=true;
					
					if(document.ProcessSequenceNumInfo.dl_WorkDistinction.options[i].innerText == "외주")
					{
						document.ProcessSequenceNumInfo.dl_WCName.disabled = true;
						document.ProcessSequenceNumInfo.dl_WCName.value = "";
					}
					else
						document.ProcessSequenceNumInfo.dl_WCName.disabled = false;
				}
				
				
			}
			
			
			//WC명
			for(var i=0;i<document.ProcessSequenceNumInfo.dl_WCName.options.length;i++)
			{
				if(document.ProcessSequenceNumInfo.dl_WCName.options[i].value == row.getCellFromKey("WCName").getValue())
				{
					document.ProcessSequenceNumInfo.dl_WCName.options[i].selected=true;
					document.ProcessSequenceNumInfo.hdWCName.value = row.getCellFromKey("WCName").getValue();
				}
			}
			
			
			
			
			document.ProcessSequenceNumInfo.tb_ProcessSequanceNum.value = row.getCellFromKey("ProcessSequenceNum").getValue()	//공정순서
			document.ProcessSequenceNumInfo.tbOutsideOrderRate.value = row.getCellFromKey("OutsideOrderRate").getValue();//외주비율
			document.ProcessSequenceNumInfo.tb_ProgressRate.value = row.getCellFromKey("ProgressRate").getValue();	//진척비율
			document.ProcessSequenceNumInfo.tb_LeadTime.value = row.getCellFromKey("LeadTime").getValue();//리드타임
			if(row.getCellFromKey("EtcText").getValue() == null)
				document.ProcessSequenceNumInfo.tbEtcText.value = '';
			else
				document.ProcessSequenceNumInfo.tbEtcText.value = row.getCellFromKey("EtcText").getValue();//기타
			
			document.ProcessSequenceNumInfo.lb_Index.value = row.getCellFromKey("ProcessSequenceInfoIndex").getValue();
			
			document.ProcessSequenceNumInfo.bt_Update.disabled = false;
			document.ProcessSequenceNumInfo.bt_Delete.disabled = false;
			document.ProcessSequenceNumInfo.bt_Registration.disabled = true;
			
			if(row.getCellFromKey("WorkDistinction").getValue() == '자가') 
			{
				document.ProcessSequenceNumInfo.tb_LeadTime.disabled = true;				//리드타임	비활성화
				document.ProcessSequenceNumInfo.tbOutsideOrderRate.disabled = true;		//발주비율	비활성화
			}
			else if(row.getCellFromKey("WorkDistinction").getValue() == '외주')
			{
				document.ProcessSequenceNumInfo.tb_LeadTime.disabled = false;				//리드타임 활성화
				document.ProcessSequenceNumInfo.tbOutsideOrderRate.disabled = true;		//발주비율 비활성화
			}
			else
			{
				document.ProcessSequenceNumInfo.tb_LeadTime.disabled = false;				//리드타임 활성화
				document.ProcessSequenceNumInfo.tbOutsideOrderRate.disabled = false;		//발주비율 활성화
			}
			
			
			
		}
		
		
		function Ultrawebgrid2_AfterSelectChangeHandler(gridName, id){
			//Add code to handle your event here.
			var row = igtbl_getRowById(id);
			ProcessSequenceNumInfo.lb_ReferenceTable.value = row.getCellFromKey("CommunityDataTableID").getValue();
			ProcessSequenceNumInfo.lb_ReferenceIndex.value = row.getCellFromKey("CDTIndex").getValue();
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
	<body bgColor="#f7f6f6" MS_POSITIONING="GridLayout">
		<form id="ProcessSequenceNumInfo" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 10px" cellSpacing="0"
				cellPadding="0" width="800" border="0" height="550">
				<TR>
					<TD vAlign="top">
						<table style="HEIGHT: 20px" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD align="right" width="600" colSpan="7"></TD>
								<td vAlign="bottom" align="right" width="200"><asp:linkbutton id="LinkButton1" runat="server" Font-Size="10pt">공정순서보기</asp:linkbutton></td>
							</TR>
						</table>
						<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; BORDER-LEFT: dimgray 2px solid; WIDTH: 800px; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 475px"
							align="left"><LEGEND>[ 입 력 ]</LEGEND>
							<TABLE style="HEIGHT: 248px" cellSpacing="0" cellPadding="0" width="800">
								<TR>
									<TD align="left" width="600" colSpan="6" height="20" vAlign="bottom">
										<uc1:ItemSearchControl id="ItemSearchControl1" runat="server"></uc1:ItemSearchControl></TD>
									<TD align="right" width="70" height="20"><FONT style="COLOR: black" face="굴림">자산분류&nbsp;</FONT></TD>
									<TD align="left" width="130" height="20"><FONT face="굴림">
											<asp:textbox id="tb_PropertyClassification" runat="server" BackColor="#EEEEE9" Height="20px"
												Width="100px" Font-Size="9pt" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"
												ReadOnly="True"></asp:textbox></FONT></TD>
								</TR>
								<TR>
									<TD align="right" width="70"><FONT style="COLOR: black" face="굴림">단위&nbsp;</FONT></TD>
									<TD align="left" width="130">
										<asp:textbox id="tb_Unit" runat="server" BackColor="#EEEEE9" Height="20px" Width="100px" Font-Size="9pt"
											BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid" ReadOnly="True"></asp:textbox></TD>
									<TD align="right" width="70"><FONT style="COLOR: black" face="굴림">규격&nbsp;</FONT></TD>
									<TD align="left" width="130">
										<asp:textbox id="tb_Standard" runat="server" BackColor="#EEEEE9" Height="20px" Width="100px"
											Font-Size="9pt" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid" ReadOnly="True"></asp:textbox></TD>
									<TD align="right" width="70">
										<asp:label id="lb_ItemIndex" runat="server" Font-Size="10pt" Visible="False"></asp:label></TD>
									<TD align="left" width="130">
										<asp:LinkButton id="LinkButton2" runat="server" Visible="False">LinkButton</asp:LinkButton></TD>
									<TD align="right" width="70">
										<asp:button id="Button2" runat="server" Width="65px" Height="20px" Text="공정 복사"></asp:button>&nbsp;
									</TD>
									<TD align="right" width="130">
										<asp:button id="bt_ProcessSequence" runat="server" Height="20px" Width="100px" Text="공정순서보기"></asp:button><FONT face="굴림">&nbsp;</FONT></TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="5"><FONT face="굴림"></FONT></TD>
									<TD align="left" width="130" height="5"></TD>
									<TD align="right" width="70" height="5"></TD>
									<TD align="left" width="130" height="5"></TD>
									<TD align="right" width="70" height="5"></TD>
									<TD align="left" width="130" height="5"></TD>
									<TD align="right" width="70" height="5"></TD>
									<TD align="right" width="130" height="5"></TD>
								</TR>
								<TR>
									<TD vAlign="top" align="left" width="800" colSpan="8" height="180">
										<igtbl:ultrawebgrid id="UltraWebGrid1" runat="server" Height="215px" Width="800px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
												RowHeightDefault="20px" RowSizingDefault="Free" Version="3.00" SelectTypeRowDefault="Single"
												AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate"
												AllowColSizingDefault="Free" RowSelectorsDefault="No" Name="UltraWebGrid1" TableLayout="Fixed"
												CellClickActionDefault="RowSelect">
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
													BorderStyle="Solid" BackColor="Silver" Height="215px"></FrameStyle>
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
														<igtbl:UltraGridColumn HeaderText="공정순서번호" Key="ProcessSequenceNum" BaseColumnName="ProcessSequenceNum">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정코드" Key="ProcessCode" Hidden="True" BaseColumnName="ProcessCode">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="공정명" Key="SmallClassificationName" BaseColumnName="SmallClassificationName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="작업구분" Key="WorkDistinction" BaseColumnName="WorkDistinction">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="WC명" Key="WCName" BaseColumnName="WCName">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="발주비율" Key="OutsideOrderRate" BaseColumnName="OutsideOrderRate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="진척비율" Key="ProgressRate" BaseColumnName="ProgressRate">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="리드타임" Key="LeadTime" BaseColumnName="LeadTime">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Right"></CellStyle>
														</igtbl:UltraGridColumn>
														<igtbl:UltraGridColumn HeaderText="기타" Key="EtcText" BaseColumnName="EtcText"></igtbl:UltraGridColumn>
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
														<igtbl:UltraGridColumn HeaderText="번호" Key="ProcessSequenceInfoIndex" BaseColumnName="ProcessSequenceInfoIndex">
															<CellStyle VerticalAlign="Middle" HorizontalAlign="Center"></CellStyle>
														</igtbl:UltraGridColumn>
													</Columns>
												</igtbl:UltraGridBand>
											</Bands>
										</igtbl:ultrawebgrid></TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="5"><FONT face="굴림"></FONT></TD>
									<TD align="left" width="130" height="5"></TD>
									<TD align="right" width="70" height="5"></TD>
									<TD align="left" width="130" height="5"></TD>
									<TD align="right" width="70" height="5"></TD>
									<TD align="left" width="130" height="5"></TD>
									<TD align="right" width="70" height="5"></TD>
									<TD align="left" width="130" height="5"></TD>
								</TR>
								<TR>
									<TD align="right" width="70"><FONT style="COLOR: red" face="굴림">*순서번호&nbsp;</FONT></TD>
									<TD align="left" width="130">
										<asp:textbox id="tb_ProcessSequanceNum" style="TEXT-ALIGN: right" runat="server" BackColor="#EEEEE9"
											Height="20px" Width="100px" Font-Size="9pt" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid"></asp:textbox></TD>
									<TD align="right" width="70"><FONT style="COLOR: red" face="굴림">*공정명&nbsp;</FONT></TD>
									<TD align="left" width="130">
										<asp:dropdownlist id="dl_ProcessCode" runat="server" BackColor="#EEEEE9" Height="8px" Width="100px"
											Font-Size="10pt">
											<asp:ListItem Value="-선택-">-선택-</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD align="right" width="70"><FONT style="COLOR: red" face="굴림">*작업구분&nbsp;</FONT></TD>
									<TD align="left" width="130">
										<asp:dropdownlist id="dl_WorkDistinction" runat="server" BackColor="#EEEEE9" Height="8px" Width="100px"
											Font-Size="10pt" AutoPostBack="True">
											<asp:ListItem Value="-선택-">-선택-</asp:ListItem>
											<asp:ListItem Value="자가">자가</asp:ListItem>
											<asp:ListItem Value="외주">외주</asp:ListItem>
											<asp:ListItem Value="자가/외주">자가/외주</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD align="right" width="70"><FONT style="COLOR: red" face="굴림">*WC명&nbsp;</FONT></TD>
									<TD align="left" width="130">
										<asp:dropdownlist id="dl_WCName" runat="server" BackColor="#EEEEE9" Height="8px" Width="100px" Font-Size="10pt"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD align="right" width="70"><FONT style="COLOR: red" face="굴림">*발주비율&nbsp;</FONT></TD>
									<TD align="left" width="130">
										<asp:textbox id="tbOutsideOrderRate" style="TEXT-ALIGN: right" runat="server" BackColor="#EEEEE9"
											Height="20px" Width="100px" Font-Size="9pt" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid">0</asp:textbox>%
									</TD>
									<TD align="right" width="70"><FONT style="COLOR: red" face="굴림">*진척비율&nbsp;</FONT></TD>
									<TD align="left" width="130">
										<asp:textbox id="tb_ProgressRate" style="TEXT-ALIGN: right" runat="server" BackColor="#EEEEE9"
											Height="20px" Width="100px" Font-Size="9pt" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid">0</asp:textbox>%
									</TD>
									<TD align="right" width="70"><FONT face="굴림">리드타임&nbsp;</FONT></TD>
									<TD align="left" width="130">
										<asp:textbox id="tb_LeadTime" style="TEXT-ALIGN: right" runat="server" BackColor="#EEEEE9" Height="20px"
											Width="100px" Font-Size="9pt" BorderWidth="1px" BorderColor="DimGray" BorderStyle="Solid">0</asp:textbox>일
									</TD>
									<TD align="right" width="70"><FONT face="굴림">기 타&nbsp;</FONT></TD>
									<TD align="left" width="130">
										<asp:textbox id="tbEtcText" runat="server" Font-Size="9pt" BorderStyle="Solid" BorderColor="DimGray"
											BorderWidth="1px" Width="100px" Height="20px" BackColor="#EEEEE9"></asp:textbox></TD>
								</TR>
								<TR>
									<TD align="right" width="70">
										<asp:button id="bt_ReferanseView" runat="server" Height="20px" Width="60px" Text="참고보기"></asp:button></TD>
									<TD style="HEIGHT: 25px" align="left" width="130"><INPUT id="lb_Index" style="WIDTH: 80px; HEIGHT: 22px" type="hidden" size="8" value="0"
											name="lb_Index" runat="server"></TD>
									<TD style="HEIGHT: 25px" align="right" width="70"><INPUT id="lb_ReferenceIndex" style="WIDTH: 30px" type="hidden" name="lb_ReferenceIndex"
											runat="server"></FONT><FONT face="굴림"><INPUT id="lb_ReferenceTable" style="WIDTH: 30px" type="hidden" name="lb_ReferenceTable"
												runat="server"></FONT>
									</TD>
									<TD style="HEIGHT: 25px" align="left" width="130"><FONT face="굴림"><INPUT id="hdWCName" style="WIDTH: 49px; HEIGHT: 22px" type="hidden" size="2" name="Hidden1"
												runat="server"></FONT></TD>
									<TD style="HEIGHT: 25px" align="right" width="70"></TD>
									<TD style="HEIGHT: 25px" align="right" width="400" colSpan="3">
										<asp:button id="bt_Clear" runat="server" Height="20px" Width="65px" Text="초기화"></asp:button><FONT face="굴림">&nbsp;</FONT>
										<asp:button id="bt_Update" runat="server" Height="20px" Width="65px" Text="수정" Enabled="False"></asp:button><FONT face="굴림">&nbsp;</FONT>
										<asp:button id="bt_Delete" runat="server" Height="20px" Width="65px" Text="삭제" Enabled="False"></asp:button><FONT face="굴림">&nbsp;</FONT>
										<asp:button id="bt_Registration" runat="server" Height="20px" Width="65px" Text="등록"></asp:button><FONT face="굴림">&nbsp;</FONT>&nbsp;</TD>
								</TR>
								<TR>
									<TD align="right" width="70" height="5"></TD>
									<TD align="left" width="130" height="5"></TD>
									<TD align="right" width="70" height="5"></TD>
									<TD align="left" width="130" height="5"><FONT face="굴림"></FONT></TD>
									<TD align="right" width="70" height="5"><FONT face="굴림"></FONT></TD>
									<TD align="right" width="400" colSpan="3" height="5"></TD>
								</TR>
								<TR>
									<TD colSpan="8" height="10">
										<igtbl:ultrawebgrid id="Ultrawebgrid2" runat="server" Height="150px" Width="800px">
											<DisplayLayout StationaryMargins="Header" AutoGenerateColumns="False" AllowSortingDefault="OnClient"
												RowHeightDefault="20px" Version="3.00" SelectTypeRowDefault="Extended" AllowColumnMovingDefault="OnServer"
												HeaderClickActionDefault="SortMulti" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
												RowSelectorsDefault="No" Name="Ultrawebgrid2" TableLayout="Fixed" CellClickActionDefault="RowSelect"
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
													BorderStyle="Solid" BackColor="Silver" Height="150px"></FrameStyle>
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
														<igtbl:UltraGridColumn HeaderText="공정순서정보테이블" Key="StandardinfoTableID" Width="150px" BaseColumnName="StandardinfoTableID">
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
