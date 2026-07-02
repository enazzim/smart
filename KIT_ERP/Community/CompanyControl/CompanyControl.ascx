<%@ Control Language="c#" AutoEventWireup="false" Codebehind="CompanyControl.ascx.cs" Inherits="KIT_ERP.Community.CompanyControl.CompanyControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<script language="javascript">
<!--
	function Popupopen(id, controlName,unitcostDistinction,ItemNum)
	{
		var value = document.all[id].value;
		var arr = null;
		arr = window.showModalDialog
			("../CompanyControl/popUp_CompanySearch.aspx?tag="+ controlName + "&value=" + value+"&unitcostdistinction=" + unitcostDistinction+"&ItemNum="+ItemNum ,"f", "dialogHeight: 370px; dialogWidth: 370px; center: yes; help: no; resizable: no; status: no;");

		if (arr != null) 
		{				
			document.all["<%= txtCompanyName.ClientID %>"].value = arr["CompanyName"];
			document.all["<%= txtBusinessRegistrationNum.ClientID %>"].value = arr["BusinessRegistrationNum"];
			
			if ( <%=strPost%> )
			{
				DoPost1();
			}
		}
	}
	
	function ResetComBox()
	{
		document.all["<%= txtCompanyName.ClientID %>"].value = "";	
		document.all["<%= txtBusinessRegistrationNum.ClientID %>"].value = "";	
	}
		
//-->
</script>
<table border="0" cellpadding="0" cellspacing="0" width="200" height="20">
	<tr>
		<td width="70" vAlign="middle" align="right" style="FONT-SIZE: 9pt">
			거래처명&nbsp;
		</td>
		<td width="130">
			<asp:TextBox id="txtCompanyName" runat="server" Width="100px" Height="20px" BorderStyle="Solid"
				BorderWidth="1px" BorderColor="Gray" BackColor="#EEEEE9"></asp:TextBox>
			<INPUT type="button" value="..." style="WIDTH: 20px; HEIGHT: 20px" size="20" id="btnCompanyNameOpen"
				name="Button1" runat="server">&nbsp; <INPUT id="txtBusinessRegistrationNum" style="WIDTH: 32px; HEIGHT: 22px" type="hidden"
				size="1" runat="server" NAME="txtBusinessRegistrationNum">
		</td>
	</tr>
</table>
