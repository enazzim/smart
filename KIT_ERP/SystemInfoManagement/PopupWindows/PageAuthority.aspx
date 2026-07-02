<%@ Page language="c#" Codebehind="PageAuthority.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.SystemInfoManagement.PopupWindows.PageAuthority" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>페이지 권한 설정</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body bgColor="#d4d0c8" MS_POSITIONING="GridLayout">
		<base target="_self">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px" height="800"
				cellSpacing="0" cellPadding="0" width="910" border="0">
				<TBODY>
					<TR>
						<TD>
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 0pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 40px"><LEGEND align="top"><asp:label id="Label1" Runat="server" Font-Size="10pt">[영업 관리]</asp:label></LEGEND>
								<TABLE id="Table3" style="WIDTH: 906px; HEIGHT: 40px" cellSpacing="0" cellPadding="0" width="906"
									border="0">
									<TR>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox1" runat="server" Font-Size="10pt" Text="수주 등록"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox2" runat="server" Font-Size="10pt" Text="수주일괄 등록"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox3" runat="server" Font-Size="10pt" Text="수주 현황"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox4" runat="server" Font-Size="10pt" Text="수주별 진행 보기"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox5" runat="server" Font-Size="10pt" Text="생산의뢰"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox6" runat="server" Font-Size="10pt" Text="생산의뢰 현황"></asp:checkbox></TD>
									</TR>
									<TR>
										<TD style="HEIGHT: 1px" width="150"><asp:checkbox id="CheckBox7" runat="server" Font-Size="10pt" Text="생산의뢰 추가"></asp:checkbox></TD>
										<TD style="HEIGHT: 1px" width="150"><asp:checkbox id="CheckBox8" runat="server" Font-Size="10pt" Text="상품구매 의뢰"></asp:checkbox></TD>
										<TD style="HEIGHT: 1px" width="150"><asp:checkbox id="CheckBox9" runat="server" Font-Size="10pt" Text="상품구매의뢰 현황"></asp:checkbox></TD>
										<TD style="HEIGHT: 1px" width="150"><asp:checkbox id="CheckBox10" runat="server" Font-Size="10pt" Text="상품구매의뢰 추가"></asp:checkbox></TD>
										<TD style="HEIGHT: 1px" width="150"><asp:checkbox id="CheckBox11" runat="server" Font-Size="10pt" Text="영업창고 입고"></asp:checkbox></TD>
										<TD style="HEIGHT: 1px" width="150"><asp:checkbox id="CheckBox12" runat="server" Font-Size="10pt" Text="영업창고 입고 현황"></asp:checkbox></TD>
									</TR>
									<TR>
										<TD width="150"><asp:checkbox id="CheckBox13" runat="server" Font-Size="10pt" Text="상품/제품 출고"></asp:checkbox></TD>
										<TD width="150"><asp:checkbox id="CheckBox14" runat="server" Font-Size="10pt" Text="상품/제품 출고 현황"></asp:checkbox></TD>
										<TD width="150"><asp:checkbox id="CheckBox15" runat="server" Font-Size="10pt" Text="매출원장 등록"></asp:checkbox></TD>
										<TD width="150"><asp:checkbox id="CheckBox16" runat="server" Font-Size="10pt" Text="매출원장 현황"></asp:checkbox></TD>
										<TD width="150"><asp:checkbox id="CheckBox17" runat="server" Font-Size="10pt" Text="수금 등록"></asp:checkbox></TD>
										<TD width="150"><asp:checkbox id="CheckBox18" runat="server" Font-Size="10pt" Text="수금 현황"></asp:checkbox></TD>
									</TR>
									<TR>
										<TD width="150"><asp:checkbox id="CheckBox19" runat="server" Font-Size="10pt" Text="미수금 보기"></asp:checkbox></TD>
										<TD width="150"><asp:checkbox id="CheckBox20" runat="server" Font-Size="10pt" Text="클레임 등록"></asp:checkbox></TD>
										<TD width="150"><asp:checkbox id="CheckBox21" runat="server" Font-Size="10pt" Text="클레임 현황"></asp:checkbox></TD>
										<TD width="150"><asp:checkbox id="CheckBox22" runat="server" Font-Size="10pt" Text="창고 이동"></asp:checkbox></TD>
										<TD width="150"><asp:checkbox id="CheckBox23" runat="server" Font-Size="10pt" Text="창고이동 현황"></asp:checkbox></TD>
										<TD width="150"></TD>
									</TR>
								</TABLE>
							</FIELDSET>
							<BR>
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 0pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 40px"><LEGEND align="top"><asp:label id="Label3" Runat="server" Font-Size="10pt">[생산 관리]</asp:label></LEGEND>
								<TABLE id="Table4" style="WIDTH: 906px; HEIGHT: 40px" cellSpacing="0" cellPadding="0" width="906"
									border="0">
									<TR>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox24" runat="server" Font-Size="10pt" Text="생산계획"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox25" runat="server" Font-Size="10pt" Text="생산계획 현황"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox26" runat="server" Font-Size="10pt" Text="생산계획 추가"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox27" runat="server" Font-Size="10pt" Text="자재소요량 산출"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox28" runat="server" Font-Size="10pt" Text="자재소요량 산출 현황"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox29" runat="server" Font-Size="10pt" Text="원자재 구매의뢰"></asp:checkbox></TD>
									</TR>
									<TR>
										<TD style="HEIGHT: 1px" width="150"><asp:checkbox id="CheckBox30" runat="server" Font-Size="10pt" Text="원자재 구매의뢰 현황"></asp:checkbox></TD>
										<TD style="HEIGHT: 1px" width="150"><asp:checkbox id="CheckBox31" runat="server" Font-Size="10pt" Text="원자재 구매의뢰 추가"></asp:checkbox></TD>
										<TD style="HEIGHT: 1px" width="150"><asp:checkbox id="CheckBox32" runat="server" Font-Size="10pt" Text="작업계획"></asp:checkbox></TD>
										<TD style="HEIGHT: 1px" width="150"><asp:checkbox id="CheckBox33" runat="server" Font-Size="10pt" Text="작업지시"></asp:checkbox></TD>
										<TD style="HEIGHT: 1px" width="150"><asp:checkbox id="CheckBox34" runat="server" Font-Size="10pt" Text="작업일보 등록"></asp:checkbox></TD>
										<TD style="HEIGHT: 1px" width="150"><asp:checkbox id="CheckBox35" runat="server" Font-Size="10pt" Text="작업일보 현황"></asp:checkbox></TD>
									</TR>
									<TR>
										<TD width="150"><asp:checkbox id="CheckBox36" runat="server" Font-Size="10pt" Text="외주의뢰"></asp:checkbox></TD>
										<TD width="150"><asp:checkbox id="CheckBox37" runat="server" Font-Size="10pt" Text="외주의뢰 현황"></asp:checkbox></TD>
										<TD width="150"></TD>
										<TD width="150"></TD>
										<TD width="150"></TD>
										<TD width="150"></TD>
									</TR>
								</TABLE>
							</FIELDSET>
							<BR>
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 0pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 40px"><LEGEND align="top"><asp:label id="Label4" Runat="server" Font-Size="10pt">[구매/외주 관리]</asp:label></LEGEND>
								<TABLE id="Table5" style="WIDTH: 906px; HEIGHT: 40px" cellSpacing="0" cellPadding="0" width="906"
									border="0">
									<TR>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox38" runat="server" Font-Size="10pt" Text="구매발주"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox39" runat="server" Font-Size="10pt" Text="구매발주 현황"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox40" runat="server" Font-Size="10pt" Text="구매발주 추가"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox41" runat="server" Font-Size="10pt" Text="구매납품"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox42" runat="server" Font-Size="10pt" Text="구매납품 현황"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"></TD>
									</TR>
									<TR>
										<TD style="HEIGHT: 15px" width="150"><asp:checkbox id="CheckBox43" runat="server" Font-Size="10pt" Text="외주발주"></asp:checkbox></TD>
										<TD style="HEIGHT: 15px" width="150"><asp:checkbox id="CheckBox44" runat="server" Font-Size="10pt" Text="외주발주 현황"></asp:checkbox></TD>
										<TD style="HEIGHT: 15px" width="150"><asp:checkbox id="CheckBox45" runat="server" Font-Size="10pt" Text="외주출고"></asp:checkbox></TD>
										<TD style="HEIGHT: 15px" width="150"><asp:checkbox id="CheckBox46" runat="server" Font-Size="10pt" Text="외주출고 현황"></asp:checkbox></TD>
										<TD style="HEIGHT: 15px" width="150"><asp:checkbox id="CheckBox47" runat="server" Font-Size="10pt" Text="외주납품"></asp:checkbox></TD>
										<TD style="HEIGHT: 15px" width="150"><asp:checkbox id="CheckBox48" runat="server" Font-Size="10pt" Text="외주납품 현황"></asp:checkbox></TD>
									</TR>
									<TR>
										<TD width="150"><asp:checkbox id="CheckBox49" runat="server" Font-Size="10pt" Text="지급계획/실적등록"></asp:checkbox></TD>
										<TD width="150"><asp:checkbox id="CheckBox50" runat="server" Font-Size="10pt" Text="지급계획/실적현황"></asp:checkbox></TD>
										<TD width="150"><asp:checkbox id="CheckBox51" runat="server" Font-Size="10pt" Text="품질검사 등록"></asp:checkbox></TD>
										<TD width="150"><asp:checkbox id="CheckBox52" runat="server" Font-Size="10pt" Text="품질검사 현황"></asp:checkbox></TD>
										<TD width="150"><asp:checkbox id="CheckBox116" runat="server" Font-Size="10pt" Text="품질검사 추가" Visible="False"></asp:checkbox></TD>
										<TD width="150"></TD>
									</TR>
								</TABLE>
							</FIELDSET>
							<BR>
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 0pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 40px"><LEGEND align="top"><asp:label id="Label6" Runat="server" Font-Size="10pt">[경영 정보]</asp:label></LEGEND>
								<TABLE id="Table7" style="WIDTH: 906px; HEIGHT: 40px" cellSpacing="0" cellPadding="0" width="906"
									border="0">
									<tr>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox53" runat="server" Font-Size="10pt" Text="사업계획등록"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox54" runat="server" Font-Size="10pt" Text="사업계획현황"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox55" runat="server" Font-Size="10pt" Text="실행계획등록"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox56" runat="server" Font-Size="10pt" Text="실행계획현황"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox57" runat="server" Font-Size="10pt" Text="기타입출고"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox58" runat="server" Font-Size="10pt" Text="기타입출고현황"></asp:checkbox></TD>
									</tr>
									<TR>
										<TD style="HEIGHT: 17px" width="150"><asp:checkbox id="CheckBox59" runat="server" Font-Size="10pt" Text="사업계획/실적지표"></asp:checkbox></TD>
										<TD style="HEIGHT: 17px" width="150"><asp:checkbox id="CheckBox60" runat="server" Font-Size="10pt" Text="매출 총이익 지표"></asp:checkbox></TD>
										<TD style="HEIGHT: 17px" width="150"><asp:checkbox id="CheckBox61" runat="server" Font-Size="10pt" Text="매출 지표"></asp:checkbox></TD>
										<TD style="HEIGHT: 17px" width="150"><asp:checkbox id="CheckBox62" runat="server" Font-Size="10pt" Text="매입 지표"></asp:checkbox></TD>
										<TD style="HEIGHT: 17px" width="150"></TD>
										<TD style="HEIGHT: 17px" width="150"></TD>
									</TR>
								</TABLE>
							</FIELDSET>
							<BR>
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 0pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 12px"><LEGEND align="top"><asp:label id="Label5" Runat="server" Font-Size="10pt">[통계 및 지표]</asp:label></LEGEND>
								<TABLE id="Table6" style="WIDTH: 906px; HEIGHT: 4px" cellSpacing="0" cellPadding="0" width="906"
									border="0">
									<TR>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox63" runat="server" Font-Size="10pt" Text="품질검사 통계"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox64" runat="server" Font-Size="10pt" Text="부적합현상 지표"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox65" runat="server" Font-Size="10pt" Text="부적합원인 지표"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox66" runat="server" Font-Size="10pt" Text="자주검사 현황"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox67" runat="server" Font-Size="10pt" Text="자주검사 통계"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox68" runat="server" Font-Size="10pt" Text="자주검사 지표"></asp:checkbox></TD>
									</TR>
									<TR>
										<TD style="HEIGHT: 1px" width="150"><asp:checkbox id="CheckBox69" runat="server" Font-Size="10pt" Text="금액기준 지표"></asp:checkbox></TD>
										<TD style="HEIGHT: 1px" width="150"><asp:checkbox id="CheckBox70" runat="server" Font-Size="10pt" Text="수량기준 지표"></asp:checkbox></TD>
										<TD style="HEIGHT: 1px" width="150"><asp:checkbox id="CheckBox71" runat="server" Font-Size="10pt" Text="로트기준 지표"></asp:checkbox></TD>
										<TD style="HEIGHT: 1px" width="150"><asp:checkbox id="CheckBox72" runat="server" Font-Size="10pt" Text="품목별 재고 지표"></asp:checkbox></TD>
										<TD style="HEIGHT: 1px" width="150"><asp:checkbox id="CheckBox73" runat="server" Font-Size="10pt" Text="창고별 재고 지표"></asp:checkbox></TD>
										<TD style="HEIGHT: 1px" width="150"><asp:checkbox id="CheckBox74" runat="server" Font-Size="10pt" Text="재고 금액 지표"></asp:checkbox></TD>
									</TR>
									<TR>
										<TD style="HEIGHT: 21px" width="150"><asp:checkbox id="CheckBox75" runat="server" Font-Size="10pt" Text="가동률 지표"></asp:checkbox></TD>
										<TD style="HEIGHT: 21px" width="150"><asp:checkbox id="CheckBox76" runat="server" Font-Size="10pt" Text="비가동 지표"></asp:checkbox></TD>
										<TD style="HEIGHT: 21px" width="150"><asp:checkbox id="CheckBox77" runat="server" Font-Size="10pt" Text="생산성 지표"></asp:checkbox></TD>
										<TD style="HEIGHT: 21px" width="150"><asp:checkbox id="CheckBox78" runat="server" Font-Size="10pt" Text="생산실적 보기" Visible="False"></asp:checkbox></TD>
										<TD style="HEIGHT: 21px" width="150"></TD>
										<TD style="HEIGHT: 21px" width="150"></TD>
									</TR>
								</TABLE>
							</FIELDSET>
							<BR>
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 0pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 49px"><LEGEND align="top"><asp:label id="Label2" Runat="server" Font-Size="10pt">[기준 정보]</asp:label></LEGEND>
								<TABLE id="Table2" style="WIDTH: 906px; HEIGHT: 40px" cellSpacing="0" cellPadding="0" width="906"
									border="0">
									<TR>
										<TD style="HEIGHT: 15px" width="150"><asp:checkbox id="CheckBox79" runat="server" Font-Size="10pt" Text="거래처 정보"></asp:checkbox></TD>
										<TD style="HEIGHT: 15px" width="150"><asp:checkbox id="CheckBox80" runat="server" Font-Size="10pt" Text="품목 정보"></asp:checkbox></TD>
										<TD style="HEIGHT: 15px" width="150"><asp:checkbox id="CheckBox81" runat="server" Font-Size="10pt" Text="품목구성 정보"></asp:checkbox></TD>
										<TD style="HEIGHT: 15px" width="150"><asp:checkbox id="CheckBox82" runat="server" Font-Size="10pt" Text="작업장 정보"></asp:checkbox></TD>
										<TD style="HEIGHT: 15px" width="150"><asp:checkbox id="CheckBox83" runat="server" Font-Size="10pt" Text="공정 정보"></asp:checkbox></TD>
										<TD style="HEIGHT: 15px" width="150"><asp:checkbox id="CheckBox84" runat="server" Font-Size="10pt" Text="설비 정보"></asp:checkbox></TD>
									</TR>
									<TR>
										<TD width="150"><asp:checkbox id="CheckBox85" runat="server" Font-Size="10pt" Text="작업표준 정보"></asp:checkbox></TD>
										<TD width="150"><asp:checkbox id="CheckBox86" runat="server" Font-Size="10pt" Text="판매단가 정보"></asp:checkbox></TD>
										<TD width="150"><asp:checkbox id="CheckBox87" runat="server" Font-Size="10pt" Text="외주단가 정보"></asp:checkbox></TD>
										<TD width="150"><asp:checkbox id="CheckBox88" runat="server" Font-Size="10pt" Text="구매단가 정보"></asp:checkbox></TD>
										<TD width="150"><asp:checkbox id="CheckBox89" runat="server" Font-Size="10pt" Text="기본생산달력 정보"></asp:checkbox></TD>
										<TD width="150"><asp:checkbox id="CheckBox90" runat="server" Font-Size="10pt" Text="WC별 생산달력 정보"></asp:checkbox></TD>
									</TR>
									<TR>
										<TD width="150"><asp:checkbox id="CheckBox91" runat="server" Font-Size="10pt" Text="사용자 정보"></asp:checkbox></TD>
										<TD width="150"><asp:checkbox id="CheckBox92" runat="server" Font-Size="10pt" Text="공용코드 정보"></asp:checkbox></TD>
										<TD width="150"></TD>
										<TD width="150"></TD>
										<TD width="150"></TD>
										<TD width="150"></TD>
									</TR>
								</TABLE>
							</FIELDSET>
							<BR>
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 0pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 28px"><LEGEND align="top"><asp:label id="Label8" Runat="server" Font-Size="10pt">[협력사 정보]</asp:label></LEGEND>
								<TABLE id="Table9" style="WIDTH: 906px; HEIGHT: 40px" cellSpacing="0" cellPadding="0" width="906"
									border="0">
									<TR>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox93" runat="server" Font-Size="10pt" Text="수주및 납품의뢰"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox94" runat="server" Font-Size="10pt" Text="납품의뢰 현황"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox95" runat="server" Font-Size="10pt" Text="납품 현황"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox96" runat="server" Font-Size="10pt" Text="출고 현황"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox97" runat="server" Font-Size="10pt" Text="재고 현황"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox98" runat="server" Font-Size="10pt" Text="품질검사 현황"></asp:checkbox></TD>
									</TR>
									<TR>
										<TD style="HEIGHT: 3px" width="150"><asp:checkbox id="CheckBox99" runat="server" Font-Size="10pt" Text="품질검 지표"></asp:checkbox></TD>
										<TD style="HEIGHT: 3px" width="150"><asp:checkbox id="CheckBox100" runat="server" Font-Size="10pt" Text="공지사항"></asp:checkbox></TD>
										<TD style="HEIGHT: 3px" width="150"><asp:checkbox id="CheckBox101" runat="server" Font-Size="10pt" Text="자유게시판"></asp:checkbox></TD>
										<TD style="HEIGHT: 3px" width="150"><asp:checkbox id="CheckBox102" runat="server" Font-Size="10pt" Text="자료실"></asp:checkbox></TD>
										<TD style="HEIGHT: 3px" width="150"></TD>
										<TD style="HEIGHT: 3px" width="150"></TD>
									</TR>
								</TABLE>
							</FIELDSET>
							<BR>
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 0pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 40px"><LEGEND align="top"><asp:label id="Label9" Runat="server" Font-Size="10pt">[커뮤니티]</asp:label></LEGEND>
								<TABLE id="Table10" style="WIDTH: 906px; HEIGHT: 7px" cellSpacing="0" cellPadding="0" width="906"
									border="0">
									<TR>
										<TD style="HEIGHT: 15px" width="150"><asp:checkbox id="CheckBox103" runat="server" Font-Size="10pt" Text="공지사항"></asp:checkbox></TD>
										<TD style="HEIGHT: 15px" width="150"><asp:checkbox id="CheckBox104" runat="server" Font-Size="10pt" Text="자유게시판"></asp:checkbox></TD>
										<TD style="HEIGHT: 15px" width="150"><asp:checkbox id="CheckBox105" runat="server" Font-Size="10pt" Text="일반자료실"></asp:checkbox></TD>
										<TD style="HEIGHT: 15px" width="150"><asp:checkbox id="CheckBox106" runat="server" Font-Size="10pt" Text="기술자료실"></asp:checkbox></TD>
										<TD style="HEIGHT: 15px" width="150"><asp:checkbox id="CheckBox107" runat="server" Font-Size="10pt" Text="사내표준 현황"></asp:checkbox></TD>
										<TD style="HEIGHT: 15px" width="150"><asp:checkbox id="CheckBox108" runat="server" Font-Size="10pt" Text="즐겨찾기"></asp:checkbox></TD>
									</TR>
									<TR>
										<TD style="HEIGHT: 21px" width="150"><asp:checkbox id="CheckBox109" runat="server" Font-Size="10pt" Text="업무일지"></asp:checkbox></TD>
										<TD style="HEIGHT: 21px" width="150"></TD>
										<TD style="HEIGHT: 21px" width="150"></TD>
										<TD style="HEIGHT: 21px" width="150"></TD>
										<TD style="HEIGHT: 21px" width="150"></TD>
										<TD style="HEIGHT: 21px" width="150"></TD>
									</TR>
								</TABLE>
							</FIELDSET>
							<BR>
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 0pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 40px"><LEGEND align="top"><asp:label id="Label7" Runat="server" Font-Size="10pt">[시스템 정보]</asp:label></LEGEND>
								<TABLE id="Table8" style="WIDTH: 906px; HEIGHT: 16px" cellSpacing="0" cellPadding="0" width="906"
									border="0">
									<TR>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox110" runat="server" Font-Size="10pt" Text="기업정보 입력"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox111" runat="server" Font-Size="10pt" Text="기초정보 일괄입력"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox112" runat="server" Font-Size="10pt" Text="시스템 기능 설정"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox113" runat="server" Font-Size="10pt" Text="사용 권한 설정"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox114" runat="server" Font-Size="10pt" Text="데이터 백업/복구"></asp:checkbox></TD>
										<TD style="HEIGHT: 2px" width="150"><asp:checkbox id="CheckBox115" runat="server" Font-Size="10pt" Text="원장정리"></asp:checkbox></TD>
									</TR>
								</TABLE>
							</FIELDSET>
							<BR>
							<FIELDSET style="BORDER-RIGHT: dimgray 2px solid; BORDER-TOP: dimgray 2px solid; FONT-SIZE: 0pt; BORDER-LEFT: dimgray 2px solid; WIDTH: 100%; BORDER-BOTTOM: dimgray 2px solid; HEIGHT: 40px"><LEGEND align="top"><asp:label id="Label10" Runat="server" Font-Size="10pt">[권한설정]</asp:label></LEGEND>
								<TABLE id="Table11" style="WIDTH: 906px; HEIGHT: 7px" cellSpacing="0" cellPadding="0" width="906"
									border="0">
									<TR>
										<TD style="HEIGHT: 15px" align="center" width="150"><asp:label id="Label11" runat="server" Font-Size="10pt" ForeColor="Red">* 권&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;한</asp:label></TD>
										<TD style="HEIGHT: 15px" width="150"><asp:dropdownlist id="ddlAuthority" runat="server" Height="20px" BackColor="#EEEEE9"></asp:dropdownlist></TD>
										<TD style="HEIGHT: 15px" align="center" width="150"><asp:label id="Label12" runat="server" Font-Size="10pt" ForeColor="Red">* 권한 설명</asp:label></TD>
										<TD style="HEIGHT: 15px" width="150" colSpan="3"><asp:textbox id="TextBox1" runat="server" Height="20px" Width="500px" BackColor="#EEEEE9"></asp:textbox></TD>
									</TR>
								</TABLE>
							</FIELDSET>
						</TD>
					</TR>
					<TR>
						<TD style="PADDING-TOP: 20px" align="right"><asp:button id="Button1" Runat="server" Height="20px" Width="65px"></asp:button></TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
		</FONT>
	</body>
</HTML>
