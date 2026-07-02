<%@ Page language="c#" Codebehind="WCProductionCalendarInfo.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.BasisInformation.ProductionCalendarInfo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ProductionCalendarInfo</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../StyleSheet2.css" type="text/css" rel="stylesheet">
		<STYLE type="text/css">BODY { SCROLLBAR-ARROW-COLOR: #400040; SCROLLBAR-FACE-COLOR: #ffffff; SCROLLBAR-DARKSHADOW-COLOR: #808080; SCROLLBAR-HIGHLIGHT-COLOR: #ffffff; SCROLLBAR-SHADOW-COLOR: #ffffff; SCROLLBAR-TRACK-COLOR: #ffffff; SCROLLBAR-3DLIGHT-COLOR: #808080 }
		</STYLE>
		<script>
		
		function OK(text)
		{
			if(confirm(text + "하시겠습니까?"))
				return true;
			else
				return false;
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
			<table id="a" width="800" style="POSITION: absolute; HEIGHT: 20px; TOP: 10px; LEFT: 10px"
				cellSpacing="0" cellPadding="0">
				<tr>
					<td align="right" style="WIDTH: 48px"><FONT face="굴림" size="2" style="FONT-SIZE: 9pt">작업장:
						</FONT>
					</td>
					<td align="left" style="WIDTH: 527px"><FONT face="굴림">
							<asp:DropDownList id="dl_WCName" runat="server" Width="184px" Height="20px" BackColor="#EEEEE9"></asp:DropDownList>&nbsp;<FONT size="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</FONT>&nbsp;
						</FONT>
						<asp:DropDownList id="dl_Year" runat="server" Width="80px" Height="20px" BackColor="#EEEEE9">
							<asp:ListItem Value="2015">2015</asp:ListItem>
							<asp:ListItem Value="2016">2016</asp:ListItem>
							<asp:ListItem Value="2017">2017</asp:ListItem>
							<asp:ListItem Value="2018">2018</asp:ListItem>
							<asp:ListItem Value="2019">2019</asp:ListItem>
							<asp:ListItem Value="2020">2020</asp:ListItem>
						</asp:DropDownList><FONT face="굴림" style="FONT-SIZE: x-small">년 </FONT>
						<asp:DropDownList id="dl_Month" runat="server" Width="50px" Height="20px" BackColor="#EEEEE9">
							<asp:ListItem Value="1">1</asp:ListItem>
							<asp:ListItem Value="2">2</asp:ListItem>
							<asp:ListItem Value="3">3</asp:ListItem>
							<asp:ListItem Value="4">4</asp:ListItem>
							<asp:ListItem Value="5">5</asp:ListItem>
							<asp:ListItem Value="6">6</asp:ListItem>
							<asp:ListItem Value="7">7</asp:ListItem>
							<asp:ListItem Value="8">8</asp:ListItem>
							<asp:ListItem Value="9">9</asp:ListItem>
							<asp:ListItem Value="10">10</asp:ListItem>
							<asp:ListItem Value="11">11</asp:ListItem>
							<asp:ListItem Value="12">12</asp:ListItem>
						</asp:DropDownList><FONT face="굴림" style="FONT-SIZE: x-small">월
							<asp:Button id="bt_Select" runat="server" Width="60px" Height="20px" Text="선 택"></asp:Button></FONT></td>
					<TD><FONT face="굴림"></FONT></TD>
					<td><FONT face="굴림"></FONT></td>
				</tr>
			</table>
			<FIELDSET style="POSITION: absolute; WIDTH: 800px; HEIGHT: 520px; TOP: 40px; LEFT: 11px" align="left"><legend>[ 
					입 력 ]</legend>
				<TABLE WIDTH="800" id="table1" style="WIDTH: 800px; HEIGHT: 500px" cellSpacing="0" cellPadding="0">
					<TR>
						<TD width="500">
							<asp:Calendar id="Calendar1" runat="server" Width="490px" Height="490px" BackColor="White" ForeColor="Black"
								Font-Size="9pt" Font-Names="Verdana" BorderColor="Black" BorderStyle="Solid" NextPrevFormat="ShortMonth"
								CellSpacing="1">
								<TodayDayStyle ForeColor="White" BackColor="#999999"></TodayDayStyle>
								<DayStyle BackColor="#CCCCCC"></DayStyle>
								<NextPrevStyle Font-Size="8pt" Font-Bold="True" ForeColor="White"></NextPrevStyle>
								<DayHeaderStyle Font-Size="8pt" Font-Bold="True" Height="8pt" ForeColor="#333333"></DayHeaderStyle>
								<SelectedDayStyle ForeColor="White" BackColor="#333399"></SelectedDayStyle>
								<TitleStyle Font-Size="12pt" Font-Bold="True" Height="12pt" ForeColor="White" BackColor="#333399"></TitleStyle>
								<OtherMonthDayStyle ForeColor="#999999"></OtherMonthDayStyle>
							</asp:Calendar></TD>
						<td width="300">
							<table width="280" id="table2" style="WIDTH: 300px; HEIGHT: 500px" cellSpacing="0" cellPadding="0">
								<tr>
									<td align="center" style="COLOR: #ff0033; FONT-SIZE: 20px" colspan="4">
										<P><FONT face="굴림">
												<asp:Label id="lb_Year" runat="server"></asp:Label>년</FONT></P>
										<P><asp:Label id="lb_Month" runat="server"></asp:Label>&nbsp;월&nbsp;&nbsp;&nbsp;&nbsp;
											<asp:Label id="lb_Day" runat="server"></asp:Label>일
										</P>
									</td>
								</tr>
								<TR>
									<TD style=" BACKGROUND-COLOR: green; WIDTH: 39px; HEIGHT: 8px" align="left"></TD>
									<TD align="left" style="WIDTH: 70px; HEIGHT: 8px">
										<asp:RadioButton id="RadioButton1" runat="server" Text="10시간" Font-Size="8pt" GroupName="CalendarGroup"
											AutoPostBack="True" BackColor="#EEEEE9"></asp:RadioButton></TD>
									<TD style="BACKGROUND-COLOR: olive; HEIGHT: 8px" align="left"></TD>
									<TD align="left" style="WIDTH: 70px; HEIGHT: 8px">
										<asp:RadioButton id="RadioButton2" runat="server" Text="8시간" Font-Size="8pt" GroupName="CalendarGroup"
											AutoPostBack="True" BackColor="#EEEEE9"></asp:RadioButton></TD>
								</TR>
								<TR>
									<TD style="BACKGROUND-COLOR: chartreuse; WIDTH: 39px; HEIGHT: 8px" align="left"><FONT face="굴림"></FONT></TD>
									<TD align="left" style="WIDTH: 70px; HEIGHT: 8px">
										<asp:RadioButton id="RadioButton3" runat="server" Text="6시간" Font-Size="8pt" GroupName="CalendarGroup"
											AutoPostBack="True" BackColor="#EEEEE9"></asp:RadioButton></TD>
									<TD style="BACKGROUND-COLOR: blue; WIDTH: 36px; HEIGHT: 8px" align="left"></TD>
									<TD style="WIDTH: 70px; HEIGHT: 8px" align="left">
										<asp:RadioButton id="RadioButton4" runat="server" Text="4시간" Font-Size="8pt" GroupName="CalendarGroup"
											AutoPostBack="True" BackColor="#EEEEE9"></asp:RadioButton></TD>
								</TR>
								<TR>
									<TD style="BACKGROUND-COLOR: darkorange; WIDTH: 39px; HEIGHT: 1px" align="left"><FONT face="굴림"></FONT></TD>
									<TD align="left" style="WIDTH: 70px; HEIGHT: 1px">
										<asp:RadioButton id="RadioButton5" runat="server" Text="기타" Font-Size="8pt" GroupName="CalendarGroup"
											AutoPostBack="True" BackColor="#EEEEE9"></asp:RadioButton></TD>
									<TD style="BACKGROUND-COLOR: red; WIDTH: 36px; HEIGHT: 1px" align="left">
									</TD>
									<TD align="left" style="HEIGHT: 1px">
										<asp:RadioButton id="RadioButton6" runat="server" Text="휴일" Font-Size="8pt" GroupName="CalendarGroup"
											AutoPostBack="True" BackColor="#EEEEE9"></asp:RadioButton></TD>
								</TR>
								<TR>
									<TD align="left" style="WIDTH: 267px" colspan="4"><FONT face="굴림" style="FONT-SIZE: x-small">
											<asp:TextBox id="tb_Time" style="TEXT-ALIGN: right" runat="server" Height="20px" Font-Size="8pt"
												BackColor="#EEEEE9">0</asp:TextBox>분</FONT>
									</TD>
								</TR>
								<tr>
									<td align="center" style="WIDTH: 267px" colspan="4">
										<P>
											<asp:Label id="Label1" runat="server" Height="29px" Font-Size="Small">사유 :</asp:Label><FONT face="굴림">&nbsp;
											</FONT>
											<asp:TextBox id="tb_Content" runat="server" Width="202px" TextMode="MultiLine" Font-Size="8pt"
												BackColor="#EEEEE9"></asp:TextBox></P>
									</td>
								</tr>
								<tr>
									<td align="center" style="WIDTH: 267px" colspan="4">
										<asp:Button id="bt_Update" runat="server" Width="60px" Height="20px" Text="수  정" Font-Size="8pt"></asp:Button><FONT face="굴림">&nbsp;</FONT>
									</td>
								</tr>
							</table>
							<asp:Label id="lb_Index" runat="server" Width="50px" Visible="False">0</asp:Label>
						</td>
					</TR>
				</TABLE>
			</FIELDSET>
		</form>
	</body>
</HTML>
