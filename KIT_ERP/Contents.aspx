<%@ Page language="c#" Codebehind="Contents.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Contents" codePage="949" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Contents</title>
		<LINK rel="stylesheet" type="text/css" href="StyleSheet1.css">
	</HEAD>
	<body bottomMargin="5" leftMargin="5" rightMargin="5" topMargin="5" bgColor="#f7f6f6"
		ms_positioning="GridLayout">
		<TABLE border="0" cellSpacing="0" cellPadding="0" width="372" height="809" ms_2d_layout="TRUE">
			<TR vAlign="top">
				<TD height="809" width="372">
					<form id="Form1" method="post" runat="server">
						<TABLE border="0" cellSpacing="0" cellPadding="0" width="802" height="365" ms_2d_layout="TRUE">
							<TR vAlign="top">
								<TD height="10" width="1"></TD>
								<TD width="801"></TD>
							</TR>
							<TR vAlign="top">
								<TD height="355"></TD>
								<TD>
									<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="800" align="center"
										height="354">
										<TR>
											<TD height="150" vAlign="top" align="center"><FONT face="굴림">
													<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="0" width="335">
														<tr>
															<td><IMG alt="" src="images/newNotice.gif" width="270" height="24"></td>
															<td><A href="Community/NoticeList.aspx"><IMG border="0" alt="" src="images/grid_3.gif" width="40" height="16"></A></td>
															<td><IMG alt="" src="images/grid_4.gif" width="25" height="24"></td>
														</tr>
														<TR>
															<!--[공지사항 최근리스트]-->
															<td vAlign="top" width="335" colSpan="3" align="center"><asp:datagrid id="DataGrid1" runat="server" Width="335px" PageSize="4" BorderStyle="None" BorderWidth="0px"
																	BackColor="#F7F6F6" CellPadding="0" GridLines="Horizontal" AllowCustomPaging="True" AutoGenerateColumns="False">
																	<SelectedItemStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Top" BackColor="#F7F6F6"></SelectedItemStyle>
																	<AlternatingItemStyle HorizontalAlign="Left" VerticalAlign="Top"></AlternatingItemStyle>
																	<ItemStyle HorizontalAlign="Left" ForeColor="#333333" VerticalAlign="Top" BackColor="#F7F6F6"></ItemStyle>
																	<HeaderStyle Font-Size="1pt" Wrap="False" HorizontalAlign="Left" Height="8px" ForeColor="#333333"
																		VerticalAlign="Top" BackColor="#F7F6F6"></HeaderStyle>
																	<Columns>
																		<asp:HyperLinkColumn Target="_self" DataNavigateUrlField="seq" DataNavigateUrlFormatString="Community/NoticeContent.aspx?seq={0}"
																			DataTextField="Title">
																			<HeaderStyle Font-Size="1pt" HorizontalAlign="Center" Height="0px" Width="235px" VerticalAlign="Top"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
																		</asp:HyperLinkColumn>
																		<asp:BoundColumn DataField="RegistrationDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
																			<HeaderStyle Font-Size="1pt" HorizontalAlign="Center" Height="0px" Width="100px" VerticalAlign="Top"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
																		</asp:BoundColumn>
																	</Columns>
																	<PagerStyle VerticalAlign="Top" Height="16px" HorizontalAlign="Left" Wrap="False" Mode="NumericPages"></PagerStyle>
																</asp:datagrid></td>
															<!--[최근리스트]--></TR>
													</TABLE>
												</FONT>
											</TD>
											<TD height="150" vAlign="top" align="center"><FONT face="굴림">
													<TABLE border="0" cellSpacing="0" cellPadding="0" width="335">
														<tr>
															<td><IMG alt="" src="images/newNotice1.gif" width="270" height="24"></td>
															<td><A href="Community/PresidentNoticeList.aspx"><IMG border="0" alt="" src="images/grid_3.gif" width="40" height="16"></A></td>
															<td><IMG alt="" src="images/grid_4.gif" width="25" height="24"></td>
														</tr>
														<TR>
															<!--[대표 공지 최근리스트]-->
															<td vAlign="top" width="335" colSpan="3" align="center"><asp:datagrid id="DataGrid7" runat="server" Width="335px" PageSize="4" BorderStyle="None" BorderWidth="0px"
																	BackColor="#F7F6F6" CellPadding="0" GridLines="Horizontal" AllowCustomPaging="True" AutoGenerateColumns="False">
																	<SelectedItemStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Top" BackColor="#F7F6F6"></SelectedItemStyle>
																	<AlternatingItemStyle HorizontalAlign="Left" VerticalAlign="Top"></AlternatingItemStyle>
																	<ItemStyle HorizontalAlign="Left" ForeColor="#333333" VerticalAlign="Top" BackColor="#F7F6F6"></ItemStyle>
																	<HeaderStyle Font-Size="1pt" Wrap="False" HorizontalAlign="Left" Height="8px" ForeColor="#333333"
																		VerticalAlign="Top" BackColor="#F7F6F6"></HeaderStyle>
																	<Columns>
																		<asp:HyperLinkColumn Target="_self" DataNavigateUrlField="seq" DataNavigateUrlFormatString="Community/PresidentNoticeContent.aspx?seq={0}"
																			DataTextField="Title">
																			<HeaderStyle Font-Size="1pt" HorizontalAlign="Center" Height="0px" Width="235px" VerticalAlign="Top"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
																		</asp:HyperLinkColumn>
																		<asp:BoundColumn DataField="RegistrationDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
																			<HeaderStyle Font-Size="1pt" HorizontalAlign="Center" Height="0px" Width="100px" VerticalAlign="Top"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
																		</asp:BoundColumn>
																	</Columns>
																	<PagerStyle VerticalAlign="Top" Height="16px" HorizontalAlign="Left" Wrap="False" Mode="NumericPages"></PagerStyle>
																</asp:datagrid></td>
															<!--[최근리스트]--></TR>
													</TABLE>
												</FONT>
											</TD>
										</TR>
										<tr>
											<TD height="150" vAlign="top" align="center"><FONT face="굴림">
													<TABLE border="0" cellSpacing="0" cellPadding="0" width="335">
														<TR>
															<TD><FONT face="굴림"><IMG alt="" src="images/newWorkDiary.gif" width="270" height="24"></FONT></TD>
															<TD><A href="Community/WorkReportList.aspx"><FONT face="굴림"><IMG border="0" alt="" src="images/grid_3.gif" width="40" height="16"></FONT></A></TD>
															<TD><FONT face="굴림"><IMG alt="" src="images/grid_4.gif" width="25" height="24"></FONT></TD>
														</TR>
														<TR> <!--[업무일지 최근리스트]-->
															<TD vAlign="top" width="335" colSpan="3" align="center"><asp:datagrid id="DataGrid5" runat="server" Width="335px" PageSize="4" BorderStyle="None" BorderWidth="0px"
																	BackColor="#F7F6F6" CellPadding="0" GridLines="Horizontal" AllowCustomPaging="True" AutoGenerateColumns="False">
																	<SelectedItemStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Top" BackColor="#F7F6F6"></SelectedItemStyle>
																	<AlternatingItemStyle HorizontalAlign="Left" VerticalAlign="Top"></AlternatingItemStyle>
																	<ItemStyle HorizontalAlign="Left" ForeColor="#333333" VerticalAlign="Top" BackColor="#F7F6F6"></ItemStyle>
																	<HeaderStyle Font-Size="1pt" Wrap="False" HorizontalAlign="Left" Height="8px" ForeColor="#333333"
																		VerticalAlign="Top" BackColor="#F7F6F6"></HeaderStyle>
																	<Columns>
																		<asp:HyperLinkColumn Target="_self" DataNavigateUrlField="Index" DataNavigateUrlFormatString="Community/WorkPageMove.aspx?Index={0}"
																			DataTextField="ReportDate">
																			<HeaderStyle Font-Size="1pt" HorizontalAlign="Center" Height="0px" Width="235px" VerticalAlign="Top"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
																		</asp:HyperLinkColumn>
																		<asp:BoundColumn DataField="RegistrationDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
																			<HeaderStyle Font-Size="1pt" HorizontalAlign="Center" Height="0px" Width="100px" VerticalAlign="Top"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
																		</asp:BoundColumn>
																	</Columns>
																	<PagerStyle VerticalAlign="Top" Height="16px" HorizontalAlign="Left" Wrap="False" Mode="NumericPages"></PagerStyle>
																</asp:datagrid><FONT face="굴림"></FONT></TD> <!--[최근리스트]--></TR>
													</TABLE>
												</FONT>
											</TD>
											<TD height="150" vAlign="top" align="center"><FONT face="굴림">
													<TABLE id="Table4" border="0" cellSpacing="0" cellPadding="0" width="335">
														<TR>
															<TD><IMG alt="" src="images/newProduct.gif" width="270" height="24"></TD>
															<TD><A href="Community/ProductList.aspx"><IMG border="0" alt="" src="images/grid_3.gif" width="40" height="16"></A></TD>
															<TD><IMG alt="" src="images/grid_4.gif" width="25" height="24"></TD>
														</TR>
														<TR> <!--[생산 자재 최근리스트]-->
															<TD vAlign="top" width="335" colSpan="3" align="center"><asp:datagrid id="DataGrid3" runat="server" Width="335px" PageSize="4" BorderStyle="None" BorderWidth="0px"
																	BackColor="#F7F6F6" CellPadding="0" GridLines="Horizontal" AllowCustomPaging="True" AutoGenerateColumns="False">
																	<SelectedItemStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Top" BackColor="#F7F6F6"></SelectedItemStyle>
																	<AlternatingItemStyle HorizontalAlign="Left" VerticalAlign="Top"></AlternatingItemStyle>
																	<ItemStyle HorizontalAlign="Left" ForeColor="#333333" VerticalAlign="Top" BackColor="#F7F6F6"></ItemStyle>
																	<HeaderStyle Font-Size="1pt" Wrap="False" HorizontalAlign="Left" Height="8px" ForeColor="#333333"
																		VerticalAlign="Top" BackColor="#F7F6F6"></HeaderStyle>
																	<Columns>
																		<asp:HyperLinkColumn Target="_self" DataNavigateUrlField="seq" DataNavigateUrlFormatString="Community/ProductContent.aspx?seq={0}"
																			DataTextField="Title">
																			<HeaderStyle Font-Size="1pt" HorizontalAlign="Center" Height="0px" Width="235px" VerticalAlign="Top"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
																		</asp:HyperLinkColumn>
																		<asp:BoundColumn DataField="RegistrationDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
																			<HeaderStyle Font-Size="1pt" HorizontalAlign="Center" Height="0px" Width="100px" VerticalAlign="Top"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
																		</asp:BoundColumn>
																	</Columns>
																	<PagerStyle VerticalAlign="Top" Height="16px" HorizontalAlign="Left" Wrap="False" Mode="NumericPages"></PagerStyle>
																</asp:datagrid></TD> <!--[최근리스트]--></TR>
													</TABLE>
												</FONT>
											</TD>
										</tr>
										<TR>
											<TD height="150" vAlign="top" align="center"><FONT face="굴림">
													<TABLE id="Table3" border="0" cellSpacing="0" cellPadding="0" width="335">
														<TR>
															<TD><IMG alt="" src="images/newDevelopment-1.GIF" width="270" height="24"></TD>
															<TD><A href="Community/DevelopmentList.aspx"><IMG border="0" alt="" src="images/grid_3.gif" width="40" height="16"></A></TD>
															<TD><IMG alt="" src="images/grid_4.gif" width="25" height="24"></TD>
														</TR>
														<TR> <!--[레이저 최근리스트]-->
															<TD vAlign="top" width="335" colSpan="3" align="center"><asp:datagrid id="DataGrid6" runat="server" Width="335px" PageSize="4" BorderStyle="None" BorderWidth="0px"
																	BackColor="#F7F6F6" CellPadding="0" GridLines="Horizontal" AllowCustomPaging="True" AutoGenerateColumns="False">
																	<SelectedItemStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Top" BackColor="#F7F6F6"></SelectedItemStyle>
																	<AlternatingItemStyle HorizontalAlign="Left" VerticalAlign="Top"></AlternatingItemStyle>
																	<ItemStyle HorizontalAlign="Left" ForeColor="#333333" VerticalAlign="Top" BackColor="#F7F6F6"></ItemStyle>
																	<HeaderStyle Font-Size="1pt" Wrap="False" HorizontalAlign="Left" Height="8px" ForeColor="#333333"
																		VerticalAlign="Top" BackColor="#F7F6F6"></HeaderStyle>
																	<Columns>
																		<asp:HyperLinkColumn Target="_self" DataNavigateUrlField="seq" DataNavigateUrlFormatString="Community/DevelopmentContent.aspx?seq={0}"
																			DataTextField="Title">
																			<HeaderStyle Font-Size="1pt" HorizontalAlign="Center" Height="0px" Width="235px" VerticalAlign="Top"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
																		</asp:HyperLinkColumn>
																		<asp:BoundColumn DataField="RegistrationDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
																			<HeaderStyle Font-Size="1pt" HorizontalAlign="Center" Height="0px" Width="100px" VerticalAlign="Top"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
																		</asp:BoundColumn>
																	</Columns>
																	<PagerStyle VerticalAlign="Top" Height="16px" HorizontalAlign="Left" Wrap="False" Mode="NumericPages"></PagerStyle>
																</asp:datagrid></TD> <!--[최근리스트]--></TR>
													</TABLE>
												</FONT>
											</TD>
											<TD height="150" vAlign="top" align="center"><FONT face="굴림">
													<TABLE id="Table7" border="0" cellSpacing="0" cellPadding="0" width="335">
														<TR>
															<TD><IMG alt="" src="images/newPipe.GIF" width="270" height="24"></TD>
															<TD><A href="Community/PipeList.aspx"><IMG border="0" alt="" src="images/grid_3.gif" width="40" height="16"></A></TD>
															<TD><IMG alt="" src="images/grid_4.gif" width="25" height="24"></TD>
														</TR>
														<TR> <!--[파이프 생산관리 최근리스트]-->
															<TD vAlign="top" width="335" colSpan="3" align="center"><asp:datagrid id="DataGrid2" runat="server" Width="335px" PageSize="4" BorderStyle="None" BorderWidth="0px"
																	BackColor="#F7F6F6" CellPadding="0" GridLines="Horizontal" AllowCustomPaging="True" AutoGenerateColumns="False">
																	<SelectedItemStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Top" BackColor="#F7F6F6"></SelectedItemStyle>
																	<AlternatingItemStyle HorizontalAlign="Left" VerticalAlign="Top"></AlternatingItemStyle>
																	<ItemStyle HorizontalAlign="Left" ForeColor="#333333" VerticalAlign="Top" BackColor="#F7F6F6"></ItemStyle>
																	<HeaderStyle Font-Size="1pt" Wrap="False" HorizontalAlign="Left" Height="8px" ForeColor="#333333"
																		VerticalAlign="Top" BackColor="#F7F6F6"></HeaderStyle>
																	<Columns>
																		<asp:HyperLinkColumn Target="_self" DataNavigateUrlField="seq" DataNavigateUrlFormatString="Community/PipeContent.aspx?seq={0}"
																			DataTextField="Title">
																			<HeaderStyle Font-Size="1pt" HorizontalAlign="Center" Height="0px" Width="235px" VerticalAlign="Top"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
																		</asp:HyperLinkColumn>
																		<asp:BoundColumn DataField="RegistrationDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
																			<HeaderStyle Font-Size="1pt" HorizontalAlign="Center" Height="0px" Width="100px" VerticalAlign="Top"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
																		</asp:BoundColumn>
																	</Columns>
																	<PagerStyle VerticalAlign="Top" Height="16px" HorizontalAlign="Left" Wrap="False" Mode="NumericPages"></PagerStyle>
																</asp:datagrid></TD> <!--[최근리스트]--></TR>
													</TABLE>
												</FONT>
											</TD>
										</TR>
										<tr>
											<TD height="150" vAlign="top" align="center"><FONT face="굴림">
													<TABLE id="Table3" border="0" cellSpacing="0" cellPadding="0" width="335">
														<TR>
															<TD><IMG alt="" src="images/newInstitute.GIF" width="270" height="24"></TD>
															<TD><A href="Community/InstituteList.aspx"><IMG border="0" alt="" src="images/grid_3.gif" width="40" height="16"></A></TD>
															<TD><IMG alt="" src="images/grid_4.gif" width="25" height="24"></TD>
														</TR>
														<TR> <!--[연구소 최근리스트]-->
															<TD vAlign="top" width="335" colSpan="3" align="center"><asp:datagrid id="DataGrid4" runat="server" Width="335px" PageSize="4" BorderStyle="None" BorderWidth="0px"
																	BackColor="#F7F6F6" CellPadding="0" GridLines="Horizontal" AllowCustomPaging="True" AutoGenerateColumns="False">
																	<SelectedItemStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Top" BackColor="#F7F6F6"></SelectedItemStyle>
																	<AlternatingItemStyle HorizontalAlign="Left" VerticalAlign="Top"></AlternatingItemStyle>
																	<ItemStyle HorizontalAlign="Left" ForeColor="#333333" VerticalAlign="Top" BackColor="#F7F6F6"></ItemStyle>
																	<HeaderStyle Font-Size="1pt" Wrap="False" HorizontalAlign="Left" Height="8px" ForeColor="#333333"
																		VerticalAlign="Top" BackColor="#F7F6F6"></HeaderStyle>
																	<Columns>
																		<asp:HyperLinkColumn Target="_self" DataNavigateUrlField="seq" DataNavigateUrlFormatString="Community/InstituteContent.aspx?seq={0}"
																			DataTextField="Title">
																			<HeaderStyle Font-Size="1pt" HorizontalAlign="Center" Height="0px" Width="235px" VerticalAlign="Top"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
																		</asp:HyperLinkColumn>
																		<asp:BoundColumn DataField="RegistrationDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
																			<HeaderStyle Font-Size="1pt" HorizontalAlign="Center" Height="0px" Width="100px" VerticalAlign="Top"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
																		</asp:BoundColumn>
																	</Columns>
																	<PagerStyle VerticalAlign="Top" Height="16px" HorizontalAlign="Left" Wrap="False" Mode="NumericPages"></PagerStyle>
																</asp:datagrid></TD> <!--[최근리스트]--></TR>
													</TABLE>
												</FONT>
											</TD>
											<TD height="150" vAlign="top" align="center"><FONT face="굴림">
													<TABLE id="Table5" border="0" cellSpacing="0" cellPadding="0" width="80">
														<TR>
															<TD><IMG alt="" src="images/newMaterials.gif" width="270" height="24"></TD>
															<TD><A href="Community/MaterialsList.aspx"><IMG border="0" alt="" src="images/grid_3.gif" width="40" height="16"></A></TD>
															<TD><IMG alt="" src="images/grid_4.gif" width="25" height="24"></TD>
														</TR>
														<TR> <!--[영업 QC 최근리스트]-->
															<TD vAlign="top" width="335" colSpan="3" align="center"><asp:datagrid id="DataGrid8" runat="server" Width="335px" PageSize="4" BorderStyle="None" BorderWidth="0px"
																	BackColor="#F7F6F6" CellPadding="0" GridLines="Horizontal" AllowCustomPaging="True" AutoGenerateColumns="False">
																	<SelectedItemStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Top" BackColor="#F7F6F6"></SelectedItemStyle>
																	<AlternatingItemStyle HorizontalAlign="Left" VerticalAlign="Top"></AlternatingItemStyle>
																	<ItemStyle HorizontalAlign="Left" ForeColor="#333333" VerticalAlign="Top" BackColor="#F7F6F6"></ItemStyle>
																	<HeaderStyle Font-Size="1pt" Wrap="False" HorizontalAlign="Left" Height="8px" ForeColor="#333333"
																		VerticalAlign="Top" BackColor="#F7F6F6"></HeaderStyle>
																	<Columns>
																		<asp:HyperLinkColumn Target="_self" DataNavigateUrlField="seq" DataNavigateUrlFormatString="Community/MaterialsContent.aspx?seq={0}"
																			DataTextField="Title">
																			<HeaderStyle Font-Size="1pt" HorizontalAlign="Center" Height="0px" Width="235px" VerticalAlign="Top"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
																		</asp:HyperLinkColumn>
																		<asp:BoundColumn DataField="RegistrationDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
																			<HeaderStyle Font-Size="1pt" HorizontalAlign="Center" Height="0px" Width="100px" VerticalAlign="Top"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
																		</asp:BoundColumn>
																	</Columns>
																	<PagerStyle VerticalAlign="Top" Height="16px" HorizontalAlign="Left" Wrap="False" Mode="NumericPages"></PagerStyle>
																</asp:datagrid></TD> <!--[최근리스트]--></TR>
													</TABLE>
												</FONT>
											</TD>
										</tr>
										<!--[로더생산현황 최근리스트]-->
										<!--
										<tr>
											<TD height="150" vAlign="top" align="center"><FONT face="굴림">
													<TABLE id="Table6" border="0" cellSpacing="0" cellPadding="0" width="80">
														<TR>
															<TD><IMG alt="" src="images/newLoader.gif" width="270" height="24"></TD>
															<TD><A href="Community/LoaderList.aspx"><IMG border="0" alt="" src="images/grid_3.gif" width="40" height="16"></A></TD>
															<TD><IMG alt="" src="images/grid_4.gif" width="25" height="24"></TD>
														</TR>
														<TR> 
															<TD vAlign="top" width="335" colSpan="3" align="center"><asp:datagrid id="DataGrid9" runat="server" Width="335px" PageSize="4" BorderStyle="None" BorderWidth="0px"
																	BackColor="#F7F6F6" CellPadding="0" GridLines="Horizontal" AllowCustomPaging="True" AutoGenerateColumns="False">
																	<SelectedItemStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Top" BackColor="#F7F6F6"></SelectedItemStyle>
																	<AlternatingItemStyle HorizontalAlign="Left" VerticalAlign="Top"></AlternatingItemStyle>
																	<ItemStyle HorizontalAlign="Left" ForeColor="#333333" VerticalAlign="Top" BackColor="#F7F6F6"></ItemStyle>
																	<HeaderStyle Font-Size="1pt" Wrap="False" HorizontalAlign="Left" Height="8px" ForeColor="#333333"
																		VerticalAlign="Top" BackColor="#F7F6F6"></HeaderStyle>
																	<Columns>
																		<asp:HyperLinkColumn Target="_self" DataNavigateUrlField="seq" DataNavigateUrlFormatString="Community/LoaderContent.aspx?seq={0}"
																			DataTextField="Title">
																			<HeaderStyle Font-Size="1pt" HorizontalAlign="Center" Height="0px" Width="235px" VerticalAlign="Top"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
																		</asp:HyperLinkColumn>
																		<asp:BoundColumn DataField="RegistrationDate" ReadOnly="True" DataFormatString="{0:yyyy-MM-dd}">
																			<HeaderStyle Font-Size="1pt" HorizontalAlign="Center" Height="0px" Width="100px" VerticalAlign="Top"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
																		</asp:BoundColumn>
																	</Columns>
																	<PagerStyle VerticalAlign="Top" Height="16px" HorizontalAlign="Left" Wrap="False" Mode="NumericPages"></PagerStyle>
																</asp:datagrid></TD> </TR>
													</TABLE>
												</FONT>
											</TD>
											<TD height="150" vAlign="top" align="center"></TD>
										</tr>
										-->
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</form>
				</TD>
			</TR>
		</TABLE>
	</body>
</HTML>
