<%@ Page language="c#" Codebehind="Contents.aspx.cs" AutoEventWireup="false" Inherits="KIT_ERP.Homepage.Contents" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Contents</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="JavaScript">
		<!--
		//쿠키값을 가져오는 함수
		function getCookie(name) 
		{
			var from_idx = document.cookie.indexOf(name+'=');
			if (from_idx != -1) 
			{ 
				from_idx += name.length + 1
				to_idx = document.cookie.indexOf(';', from_idx) 
				if (to_idx == -1) 
				{
					to_idx = document.cookie.length
				}
				return unescape(document.cookie.substring(from_idx, to_idx))
			}
		}
		/*
		var CloseDate = new Date("2004/01/20"); //팝업 윈도우를 닫고자 하는 날짜를 입력하세요.
		var Today = new Date(); //오늘 날짜
		if (Today < CloseDate)
		{
		*/
			//getCookie 함수를 호출하여 쿠키값을 가져온다. 
			var blnCookie = getCookie("op1");
			//쿠키값이 true가 아닐 경우에만 새 창을 띄운다.
			if ( !blnCookie ) 
			{
				//win = window.open('../popup.htm','popup','toolbar=no, width=520, height=350, left=50, top=50')
				//win.focus();
			}
			/*
		}
		*/
		//-->
		</script>
	</HEAD>
	<BODY bgColor="whitesmoke" leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout" MARGINHEIGHT="0"
		MARGINWIDTH="0" style="BACKGROUND-POSITION:100% 100%; BACKGROUND-ATTACHMENT:fixed; BACKGROUND-IMAGE:url(../Images/bg-main.gif); BACKGROUND-REPEAT:no-repeat"
		background="../Images/bg-main.gif">
		<form id="Form1" method="post" runat="server">
			<TABLE BORDER="0" CELLPADDING="0" CELLSPACING="0" align="center" width="466">
				<TR>
					<TD width="466" height="51" style="BACKGROUND-ATTACHMENT:fixed; BACKGROUND-REPEAT:no-repeat">
						<p><img src="../Images/bg-mains1.gif" width="485" height="51" border="0"></p>
					</TD>
				</TR>
				<tr>
					<TD width="466" height="150" background="../Images/bg-mains2.gif" style="BACKGROUND-ATTACHMENT:fixed; BACKGROUND-IMAGE:url(../Images/bg-mains2.gif); BACKGROUND-REPEAT:no-repeat"
						align="center">
						<MARQUEE style="WIDTH: 427px; HEIGHT: 96px" scrollAmount="1" direction="up"><FONT face="굴림" size="2" color=#330099>당사 
								홈페이지 방문을 진심으로 환영합니다.
								<P>
								당사는 미래지향적인 기업으로 홈페이지와	ERP시스템을 <p>
								통합운영하고 있습니다.<P>
								당사에 대한 요구사항은 방명록에, 홍보사항이나 기타의견은
								<p>자유게시판에 올려 주시고 연락처를 남겨 주시면 
								<P>정성껏 회신 하겠습니다.
								<P>당신의 건강과 행복을 기원합니다.
								<P>감사합니다
								<P>
								임직원 일동</FONT> </P></MARQUEE>
					</TD>
				</tr>
				<tr>
					<TD width="466" height="20" style="BACKGROUND-ATTACHMENT:fixed; BACKGROUND-REPEAT:no-repeat">
						<p><img src="../Images/bg-mains3.gif" width="485" height="20" border="0"></p>
					</TD>
				</tr>
			</TABLE>
		</form>
	</BODY>
</HTML>
