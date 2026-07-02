<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ItemSearch.ascx.cs" Inherits="KIT_ERP.BasisInformation.Popup.ReversalItem.ItemSearch" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<script language="javascript">
<!--
	function openPopupChild(id, controlName, propertyClassification, businessRegistrationNum, unitcostDistinction)
	{
		var value = document.all[id].value;
		var arr = null;

		arr = window.showModalDialog
			("ReversalItem/popUp_ItemSearch.aspx?tag="+ controlName + "&value=" + value + "&propertyClassification=" + propertyClassification + "&businessregistrationnum=" + businessRegistrationNum + "&unitcostdistinction=" + unitcostDistinction ,"f", "dialogHeight: 400px; dialogWidth: 807px; center: yes; help: no; resizable: no; status: no;");

		if (arr != null) 
		{				
			document.all["<%= ChildtxtItemNum.ClientID %>"].value = arr["ItemNum"];
			document.all["<%= ChildtxtItemDrawNum.ClientID %>"].value = arr["ItemDrawNum"];
			document.all["<%= ChildtxtItemName.ClientID %>"].value = arr["ItemName"];
			
			if ( <%=strPost%> )
			{
				DoPostChild();
				
			}
		}
	}
	
	function ResetBox()
	{
		document.all["<%= ChildtxtItemNum.ClientID %>"].value = "";
		document.all["<%= ChildtxtItemDrawNum.ClientID %>"].value = "";
		document.all["<%= ChildtxtItemName.ClientID %>"].value = "";
	}
		
//-->
</script>
<table border="0" cellpadding="0" cellspacing="0" width="600" height="20">
	<tr>
		<td width="70" vAlign="middle" align="right" style="FONT-SIZE: 9pt">
			품목번호&nbsp;
		</td>
		<td width="131" style="WIDTH: 131px">
			<asp:TextBox id="ChildtxtItemNum" runat="server" Width="100px" Height="20px" BorderStyle="Solid"
				BorderWidth="1px" BorderColor="Gray" BackColor="#EEEEE9"></asp:TextBox>
			<INPUT type="button" value="..." style="WIDTH: 20px; HEIGHT: 20px" size="20" id="btnItemNumOpen"
				name="Button1" runat="server">
		</td>
		<td width="70" vAlign="middle" align="right" style="FONT-SIZE: 9pt">
			도면번호&nbsp;
		</td>
		<td width="130">
			<asp:TextBox id="ChildtxtItemDrawNum" runat="server" Width="100px" Height="20px" BorderStyle="Solid"
				BorderWidth="1px" BorderColor="Gray" BackColor="#EEEEE9"></asp:TextBox>
			<INPUT type="button" value="..." size="20" style="WIDTH: 20px; HEIGHT: 20px" id="btnItemDrawNumOpen"
				name="Button2" runat="server">
		</td>
		<td width="70" vAlign="middle" align="right" style="FONT-SIZE: 9pt">
			품목명&nbsp;
		</td>
		<td width="130">
			<asp:TextBox id="ChildtxtItemName" runat="server" Width="100px" Height="20px" BorderStyle="Solid"
				BorderWidth="1px" BorderColor="Gray" BackColor="#EEEEE9"></asp:TextBox>
			<INPUT type="button" value="..." size="20" style="WIDTH: 20px; HEIGHT: 20px" id="btnItemNameOpen"
				name="Button3" runat="server">
		</td>
	</tr>
</table>

