<%@ Page language="c#" Codebehind="Top.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Top" codePage="949"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Top</title>
		<META http-equiv="Content-Type" content="text/html; charset=ks_c_5601-1987">
		<script language="javascript">
		function LogOut()
		{
			parent.location.href = "Login.aspx";
		}		
		function helpUrlSearch()
		{
			Form1.helpUrl.value = window.parent.frames("Contents").location;
		}
		//
		</script>
		<style>
			FORM { DISPLAY: inline }
			BODY { FONT-SIZE: 12px; COLOR: #999999; FONT-FAMILY: 돋움 }
			A { FONT-SIZE: 12px; COLOR: #646507; TEXT-DECORATION: none }
			A:hover { FONT-SIZE: 12px; COLOR: #e33500; TEXT-DECORATION: none }
			</style>
	</HEAD>
	<BODY text="black" vLink="purple" aLink="red" link="blue" bgColor="white" ms_positioning="GridLayout"
		background="./images/top_bgbg.jpg">
		<TABLE height="1009" cellSpacing="0" cellPadding="0" width="48" border="0" ms_2d_layout="TRUE">
			<TR vAlign="top">
				<TD width="48" height="1009">
					<form id="Form1" method="post" runat="server">
						<TABLE height="31" cellSpacing="0" cellPadding="0" width="997" border="0" ms_2d_layout="TRUE">
							<TR vAlign="top">
								<TD width="1" height="31"></TD>
								<TD width="996">
									<table height="30" cellSpacing="0" cellPadding="0" width="995">
										<tr>
											<td width="180" align="center" vAlign="middle">
												<asp:Label id="Label1" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
											</td>
											<td align="right">
												<INPUT id="helpUrl" type="hidden" name="helpUrl" runat="server">&nbsp; <img src="./images/top_alow.jpg" align="absMiddle" width="10" height="11">
												<asp:hyperlink id="HyperLink1" runat="server" Font-Names="돋움" Font-Size="10pt" Target="_parent"
													NavigateUrl="Main.htm"> HOME</asp:hyperlink>&nbsp; <img src="./images/top_alow.jpg" align="absMiddle" width="10" height="11">
												<asp:linkbutton id="LinkButton2" runat="server" Font-Names="돋움" Font-Size="10pt"> HELP </asp:linkbutton>&nbsp;
												<img src="./images/top_alow.jpg" align="absMiddle" width="10" height="11">
												<asp:linkbutton id="LinkButton1" runat="server" Font-Names="돋움" Font-Size="10pt">  LOGOUT </asp:linkbutton>
											</td>
										</tr>
									</table>
								</TD>
							</TR>
						</TABLE>
					</form>
				</TD>
			</TR>
		</TABLE>
	</BODY>
</HTML>
