<%@ Page language="c#" Codebehind="RowMaterialMessage.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.ProductionManagement.PopupWindows.RowMaterialMessage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>RowMaterialMessage</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">
		<!--
		function Close(msg)
		{
			if(msg == "OK")
			{
				// 현재고량
				if(document.Form1.chkPresentRowMaterialUsed.checked)
				{
					window.dialogArguments.Form1.hidPresentRowMaterialUsed.value = "true";
				}
				else
				{
					window.dialogArguments.Form1.hidPresentRowMaterialUsed.value = "false";
				}
				
				// 안전재고량
				if(document.Form1.chkSafeyRowMaterialUsed.checked)
				{
					window.dialogArguments.Form1.hidSafeyRowMaterialUsed.value = "true";
				}
				else
				{
					window.dialogArguments.Form1.hidSafeyRowMaterialUsed.value = "false";
				}

				// 발주미입고량
				if(document.Form1.chkOrderNonInStorehouseUsed.checked)
				{
					window.dialogArguments.Form1.hidOrderNonInStorehouseUsed.value = "true";
				}
				else
				{
					window.dialogArguments.Form1.hidOrderNonInStorehouseUsed.value = "false";
				}

				// 발주간격수량
				if(document.Form1.chkOrderGapInStorehouseUsed.checked)
				{
					window.dialogArguments.Form1.hidOrderGapInStorehouseUsed.value = "true";
				}
				else
				{
					window.dialogArguments.Form1.hidOrderGapInStorehouseUsed.value = "false";
				}

				// 최소발주량
				if(document.Form1.chkMinimumGapUsed.checked)
				{
					window.dialogArguments.Form1.hidMinimumGapUsed.value = "true";
				}
				else
				{
					window.dialogArguments.Form1.hidMinimumGapUsed.value = "false";
				}

				// 발주의뢰대기량
				if(document.Form1.chkOrderRequestStandbyUsed.checked)
				{
					window.dialogArguments.Form1.hidOrderRequestStandbyUsed.value = "true";
				}
				else
				{
					window.dialogArguments.Form1.hidOrderRequestStandbyUsed.value = "false";
				}
			}
			else
			{
				window.dialogArguments.Form1.hidPresentRowMaterialUsed.value = "Cancle";
			}
			
			window.close();
		}
		-->
		</script>
</HEAD>

	<body MS_POSITIONING="GridLayout">
		<base target="_self">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px" cellSpacing="0"
				cellPadding="0" width="368" border="0" bgColor="#d4d0c8">
				<TR>
					<TD style="WIDTH: 30px">
						<asp:Image id="Image1" runat="server" ImageUrl="../../images/question.gif"></asp:Image></TD>
					<TD colSpan="3" style="PADDING-LEFT: 4px"><FONT face="굴림">
							<asp:Label id="Label1" runat="server" Font-Size="9pt">자재소요량 산출시 고려해야 할 사항들을 선택하세요.</asp:Label></FONT></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 30px"><FONT face="굴림"></FONT></TD>
					<TD>
						<asp:CheckBox id="chkPresentRowMaterialUsed" runat="server" Text="현재고량" Font-Size="9pt"></asp:CheckBox></TD>
					<TD>
						<asp:CheckBox id="chkSafeyRowMaterialUsed" runat="server" Text="안전재고량" Font-Size="9pt"></asp:CheckBox></TD>
					<TD>
						<asp:CheckBox id="chkOrderNonInStorehouseUsed" runat="server" Text="발주미입고량" Font-Size="9pt"></asp:CheckBox></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 30px"></TD>
					<TD>
						<asp:CheckBox id="chkOrderGapInStorehouseUsed" runat="server" Text="발주간격수량" Font-Size="9pt"></asp:CheckBox></TD>
					<TD>
						<asp:CheckBox id="chkMinimumGapUsed" runat="server" Text="최소발주량" Font-Size="9pt"></asp:CheckBox></TD>
					<TD>
						<asp:CheckBox id="chkOrderRequestStandbyUsed" runat="server" Text="발주의뢰대기량" Font-Size="9pt"></asp:CheckBox></TD>
				</TR>
				<TR>
					<TD style="PADDING-BOTTOM: 10px; PADDING-TOP: 10px" align="center" width="100%" colSpan="4"><FONT face="굴림"></FONT>
						<asp:Button id="btnOK" runat="server" Text="확인" Height="20px" Width="65px" CommandName="OK"></asp:Button><FONT face="굴림">&nbsp;&nbsp;&nbsp;
						</FONT>
						<asp:Button id="btnCancle" runat="server" Text="취소" Height="20px" Width="65px" CommandName="Cancle"></asp:Button><FONT face="굴림"></FONT></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
