<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ItemSearchControl.ascx.cs" Inherits="KIT_ERP.Common.ItemSearchControl.ItemSearchControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<script language="javascript">
<!--
	function openPopup(id, controlName, propertyClassification, businessRegistrationNum, unitcostDistinction)
	{
		var value = document.all[id].value;
		var arr = null;
		arr = window.showModalDialog
			("../Common/ItemSearchControl/popUp_ItemSearch.aspx?tag="+ controlName + "&value=" + value + "&propertyClassification=" + propertyClassification + "&businessregistrationnum=" + businessRegistrationNum + "&unitcostdistinction=" + unitcostDistinction ,"f", "dialogHeight: 400px; dialogWidth: 807px; center: yes; help: no; resizable: no; status: no;");

		if (arr != null) 
		{				
			document.all["<%= txtItemNum.ClientID %>"].value = arr["ItemNum"];
			document.all["<%= hdItemNum.ClientID %>"].value = arr["ItemNum"];
			document.all["<%= txtItemDrawNum.ClientID %>"].value = arr["ItemDrawNum"];
			document.all["<%= txtItemName.ClientID %>"].value = arr["ItemName"];
			
			if ( <%=strPost%> )
			{
				DoPost();
			}
		}
	}
	
	function ResetTextBox()
	{
		document.all["<%= txtItemNum.ClientID %>"].value = "";
		document.all["<%= txtItemDrawNum.ClientID %>"].value = "";
		document.all["<%= txtItemName.ClientID %>"].value = "";
		document.all["<%= hdItemNum.ClientID %>"].value = "";
	}
		
//-->
</script>
<table border="0" cellpadding="0" cellspacing="0" width="600" height="20">
	<tr>
		<td width="70" vAlign="middle" align="right" style="FONT-SIZE: 9pt">
			<asp:Label id="lbItemNum" runat="server">품목번호</asp:Label>&nbsp;
		</td>
		<td width="131" style="WIDTH: 131px">
			<asp:TextBox id="txtItemNum" runat="server" Width="100px" Height="20px" BorderStyle="Solid" BorderWidth="1px"
				BorderColor="Gray" BackColor="#EEEEE9"></asp:TextBox>
			<INPUT type="button" value="..." style="WIDTH: 20px; HEIGHT: 20px" size="20" id="btnItemNumOpen"
				name="Button1" runat="server">
		</td>
		<td width="70" vAlign="middle" align="right" style="FONT-SIZE: 9pt">
			<asp:Label id="lbItemDrawNum" runat="server">도면번호</asp:Label>&nbsp;
		</td>
		<td width="130">
			<asp:TextBox id="txtItemDrawNum" runat="server" Width="100px" Height="20px" BorderStyle="Solid"
				BorderWidth="1px" BorderColor="Gray" BackColor="#EEEEE9"></asp:TextBox>
			<INPUT type="button" value="..." size="20" style="WIDTH: 20px; HEIGHT: 20px" id="btnItemDrawNumOpen"
				name="Button2" runat="server">
		</td>
		<td width="70" vAlign="middle" align="right" style="FONT-SIZE: 9pt">
			<asp:Label id="lbItemName" runat="server">품목명</asp:Label>
		</td>
		<td width="130">
			<asp:TextBox id="txtItemName" runat="server" Width="100px" Height="20px" BorderStyle="Solid"
				BorderWidth="1px" BorderColor="Gray" BackColor="#EEEEE9"></asp:TextBox>
			<INPUT type="button" value="..." size="20" style="WIDTH: 20px; HEIGHT: 20px" id="btnItemNameOpen"
				name="Button3" runat="server">
		</td>
	</tr>
</table>
<INPUT id="hdItemNum" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="hdItemNum"
	runat="server">
