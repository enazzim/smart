<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ParentItemSearch.ascx.cs" Inherits="KIT_ERP.Community.ParentItemControl.ParentItemSearch" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<script language="javascript">
<!--
	function openPopup(id, controlName, propertyClassification, businessRegistrationNum, unitcostDistinction)
	{
		var value = document.all[id].value;
		var arr = null;
		arr = window.showModalDialog
			("../ParentItemControl/popUp_ItemSearch.aspx?tag="+ controlName + "&value=" + value + "&propertyClassification=" + propertyClassification + "&businessregistrationnum=" + businessRegistrationNum + "&unitcostdistinction=" + unitcostDistinction ,"f", "dialogHeight: 400px; dialogWidth: 807px; center: yes; help: no; resizable: no; status: no;");

		if (arr != null) 
		{				
			document.all["<%= txtItemNum.ClientID %>"].value = arr["ItemNum"];
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
	}
		
//-->
</script>
<table height="20" cellSpacing="0" cellPadding="0" width="600" border="0">
	<tr>
		<td style="FONT-SIZE: 9pt" vAlign="middle" align="right" width="70"><asp:label id="lbItemNum" runat="server">품목번호</asp:label>&nbsp;
		</td>
		<td style="WIDTH: 131px" width="131"><asp:textbox id="txtItemNum" runat="server" BackColor="#EEEEE9" BorderColor="Gray" BorderWidth="1px"
				BorderStyle="Solid" Height="20px" Width="100px"></asp:textbox><INPUT id="btnItemNumOpen" style="WIDTH: 20px; HEIGHT: 20px" type="button" size="20" value="..."
				name="Button1" runat="server">
		</td>
		<td style="FONT-SIZE: 9pt" vAlign="middle" align="right" width="70"><asp:label id="lbItemDrawNum" runat="server">도면번호</asp:label>&nbsp;
		</td>
		<td width="130"><asp:textbox id="txtItemDrawNum" runat="server" BackColor="#EEEEE9" BorderColor="Gray" BorderWidth="1px"
				BorderStyle="Solid" Height="20px" Width="100px"></asp:textbox><INPUT id="btnItemDrawNumOpen" style="WIDTH: 20px; HEIGHT: 20px" type="button" size="20"
				value="..." name="Button2" runat="server">
		</td>
		<td style="FONT-SIZE: 9pt" vAlign="middle" align="right" width="70"><asp:label id="lbItemName" runat="server">품목명</asp:label></td>
		<td width="130"><asp:textbox id="txtItemName" runat="server" BackColor="#EEEEE9" BorderColor="Gray" BorderWidth="1px"
				BorderStyle="Solid" Height="20px" Width="100px"></asp:textbox><INPUT id="btnItemNameOpen" style="WIDTH: 20px; HEIGHT: 20px" type="button" size="20" value="..."
				name="Button3" runat="server">
		</td>
	</tr>
</table>
