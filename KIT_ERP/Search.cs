using System;
using System.Configuration;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Infragistics.WebUI;
using Infragistics.WebUI.WebCombo;
using Infragistics.WebUI.UltraWebGrid;
using Infragistics.WebUI.WebSchedule;


namespace KIT_ERP
{
	/// <summary>
	/// Search에 대한 요약 설명입니다.
	/// </summary>
	public class Search
	{
		private string m_Page = "";
		private string m_ItemNum = "";
		private string m_DrawNum = "";
		private string m_ItemName = "";
		private string m_BeginDate = "1999-01-01";
		private string m_EndDate="2079-06-01";
		private string BeginDate = "1999-01-01";//납기요구일
		private string EndDate="2079-06-01"; //납기요구일
		
		private string m_fromDate = "1999-01-01";//작업완료일
		private string m_toDate="2079-06-01";//작업완료일
		private string m_Com = "";
		private string m_Comnum = ""; //사업자등록번호 (창고검색시 사용)
		private string m_ProgressState = "";
		private string m_Source = "";
		private string m_DecisionMethod = "";
		private string m_Store="";		//창고명
		private int m_StoreNum;			//창고번호(영업창고)
		private string m_WCName = "";
		private string m_Classification1 = "";	//거래처분류1
		private string m_Classification2 = "";	//거래처분류2


//		private string m_ItemClassification1 = "";	//품목분류1
//		private string m_ItemClassification2 = "";	//품목분류2
//		private string m_ItemClassification3 = "";	//품목분류3
//		private string m_ItemClassification4 = "";	//품목분류4
		
		private string m_WorkDivision = "";		//부적합현황페이지내의 작업구분 담는 필드
		private string m_Process = "";			//부적합현황페이지내의 공정을 담는 필드
		private string m_Worker = "";
		private string m_WorkerID = "";
		private string m_OrderNum = "";
		private string m_Property = "";			//소모품, 부자재구분 필드

		private string m_hdItemNum = "";
		private string m_hdChildItemNum = "";
		private string m_ChildItemNum = "";		//원자재 소진 현황페이지 생성품번
		private string m_ChildItemDrawNum = "";		//원자재 소진 현황페이지 생성도번
		private string m_ChildItemName = "";		//원자재 소진 현황페이지 생성품명
		private string m_Distinction = "";
		


		public Search(string RequestPage, string RequesthdItemNum, string RequstItemNum, string RequestDrawNum, string RequestItemName, string Com, string Num, string RequestDistinction)
		{
			m_Page = RequestPage.Trim();

			m_hdItemNum = RequesthdItemNum;
			m_ItemNum = RequstItemNum.Trim();
			m_DrawNum = RequestDrawNum.Trim();
			m_ItemName = RequestItemName.Trim();

			m_Com = Com;
			m_Comnum = Num;
			if(RequestDistinction.Trim() == "전체")
				m_Distinction = "";
			else
				m_Distinction = RequestDistinction.Trim();

		}
		
		/// <summary>
		/// 원자재 소진현황 페이지 생성자
		/// </summary>
		/// <param name="RequestPage">요청페이지</param>
		/// <param name="RequsthdItem">소진 히든품번</param>
		/// <param name="RequestItem">소진품번</param>
		/// <param name="RequestDrawNum">소진도번</param>
		/// <param name="RequestItemName">소진품명</param>
		/// <param name="RequesthdChildItemNum">생성 히든품번</param>
		/// <param name="RequestChildItemNum">생성품번</param>
		/// <param name="RequestChildItemDrawNum">생성도번</param>
		/// <param name="RequestChildItemName">생성품명</param>
		/// <param name="RequestBeginDate">소진시작일</param>
		/// <param name="RequestEndDate">소진종료일</param>
		public Search(string RequestPage, string RequsthdItem, string RequestItem, string RequestDrawNum, string RequestItemName,string RequesthdChildItemNum, string RequestChildItemNum, string RequestChildItemDrawNum , string RequestChildItemName, WebDateChooser RequestBeginDate, WebDateChooser RequestEndDate, string requestworker)
		{
			m_Page = RequestPage.Trim();
			m_ItemNum = RequestItem.Trim();
			m_DrawNum = RequestDrawNum.Trim();
			m_ItemName = RequestItemName.Trim();

			m_ChildItemNum = RequestChildItemNum.Trim();
			m_ChildItemDrawNum = RequestChildItemDrawNum.Trim();
			m_ChildItemName = RequestChildItemName.Trim();
			if(requestworker.Trim() != "")
				m_WorkerID = requestworker.Trim();

			if(Convert.ToString(RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = RequestBeginDate.Text.Trim();
			if(Convert.ToString(RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = RequestEndDate.Text.Trim();
			m_hdChildItemNum = RequesthdChildItemNum.Trim();
			m_hdItemNum = RequsthdItem.Trim();
		}
		
		
		/// <summary>
		/// 자재소요량 산출현황 페이지 생성자
		/// </summary>
		/// <param name="RequestPage">요청페이지</param>
		/// <param name="RequestItem">요청품목번호</param>
		/// <param name="RequestDrawNum">요청도면번호</param>
		/// <param name="RequestItemName">요청품목명</param>
		/// <param name="RequestBeginDate">요청생산시작일(From)</param>
		/// <param name="RequestEndDate">요청생산시작일(To)</param>
		/// <param name="mBeginDate">요청생산완료일(From)</param>
		/// <param name="mEndDate">요청생산완료일(To)</param>
		/// <param name="ItemClassification">요청제품팀</param>
		public Search(string RequestPage, string RequestItem, string RequestDrawNum, string RequestItemName,  WebDateChooser RequestBeginDate, WebDateChooser RequestEndDate, WebDateChooser mBeginDate, WebDateChooser mEndDate, string ItemClassification)
		{
			m_Page = RequestPage.Trim();
			m_ItemNum = RequestItem.Trim();
			m_DrawNum = RequestDrawNum.Trim();
			m_ItemName = RequestItemName.Trim();
			if(Convert.ToString(RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = RequestBeginDate.Text.Trim();
			if(Convert.ToString(RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = RequestEndDate.Text.Trim();

			if(Convert.ToString(mBeginDate.Value).Trim() == "")
				BeginDate = "1999-01-01";
			else
				BeginDate = mBeginDate.Text.Trim();
			if(Convert.ToString(mEndDate.Value).Trim() == "")
				EndDate = "2079-06-01";
			else
				EndDate = mEndDate.Text.Trim();

			m_Classification1 = ItemClassification;

		}


		/// <summary>
		/// 작업계획현황 페이지
		/// </summary>
		/// <param name="RequestPage">요청페이지명</param>
		/// <param name="RequestItem">요청품번</param>
		/// <param name="RequestDrawNum">요청도면번호</param>
		/// <param name="RequestItemName">요청품명</param>
		/// <param name="ProgressState">요청진행상태</param>
		/// <param name="RequestBeginDate">요청생산시작일(From)</param>
		/// <param name="RequestEndDate">요청생산시작일(To)</param>
		/// <param name="mBeginDate">요청납기일(From)</param>
		/// <param name="mEndDate">요청납기일(To)</param>
		/// <param name="ItemClassification">요청제품팀</param>
		
		
		public Search(string RequestPage, string RequestItem, string RequestDrawNum, string RequestItemName, string ProgressState,  WebDateChooser RequestBeginDate, WebDateChooser RequestEndDate, WebDateChooser mBeginDate, WebDateChooser mEndDate, WebDateChooser mfromDate, WebDateChooser mtoDate,string ItemClassification1, string ItemClassification2)
		{
			m_Page = RequestPage.Trim();
			m_ItemNum = RequestItem.Trim();
			m_DrawNum = RequestDrawNum.Trim();
			m_ItemName = RequestItemName.Trim();

			if(ProgressState != "-선 택-")
				m_ProgressState = ProgressState;

			if(Convert.ToString(RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = RequestBeginDate.Text.Trim();
			if(Convert.ToString(RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = RequestEndDate.Text.Trim();

			if(Convert.ToString(mBeginDate.Value).Trim() == "")
				BeginDate = "1999-01-01";
			else
				BeginDate = mBeginDate.Text.Trim();
			if(Convert.ToString(mEndDate.Value).Trim() == "")
				EndDate = "2079-06-01";
			else
				EndDate = mEndDate.Text.Trim();


			if(Convert.ToString(mfromDate.Value).Trim() != "")
				m_fromDate = mfromDate.Text.Trim();
			if(Convert.ToString(mEndDate.Value).Trim() != "")
				m_toDate = mtoDate.Text.Trim();

			m_Classification1 = ItemClassification1;
			m_Classification2 = ItemClassification2;

		}
	
		/// <summary>
		/// 작업계획 생성자
		/// </summary>
		/// <param name="RequestPage"></param>
		/// <param name="RequestItem"></param>
		/// <param name="RequestDrawNum"></param>
		/// <param name="RequestItemName"></param>
		/// <param name="RequestBeginDate"></param>
		/// <param name="RequestEndDate"></param>
		/// <param name="ItemClassification1"></param>
		/// <param name="ItemClassification2"></param>
		public Search(string RequestPage, string RequestItem, string RequestDrawNum, string RequestItemName, string ItemClassification1, WebDateChooser RequestBeginDate, WebDateChooser RequestEndDate, string ItemClassification2)
		{
			m_Page = RequestPage.Trim();
			m_ItemNum = RequestItem.Trim();
			m_DrawNum = RequestDrawNum.Trim();
			m_ItemName = RequestItemName.Trim();

			if(Convert.ToString(RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = RequestBeginDate.Text.Trim();
			if(Convert.ToString(RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = RequestEndDate.Text.Trim();

			

			m_Classification1 = ItemClassification1;
			m_Classification2 = ItemClassification2;

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="WCName"></param>
		/// <param name="RequestPage"></param>
		/// <param name="RequestItem"></param>
		/// <param name="RequestDrawNum"></param>
		/// <param name="RequestItemName"></param>
		/// <param name="RequestBeginDate"></param>
		/// <param name="RequestEndDate"></param>
		public Search(DropDownList WCName, string RequestPage, string RequestItem, string RequestDrawNum, string RequestItemName,  WebDateChooser RequestBeginDate, WebDateChooser RequestEndDate)
		{
			if(WCName.SelectedIndex != 0)
				m_WCName = WCName.SelectedItem.Text.Trim();
			m_Page = RequestPage.Trim();
			m_ItemNum = RequestItem.Trim();
			m_DrawNum = RequestDrawNum.Trim();
			m_ItemName = RequestItemName.Trim();
			if(Convert.ToString(RequestBeginDate.Value).Trim() != "")
				m_BeginDate = RequestBeginDate.Text.Trim();
			if(Convert.ToString(RequestEndDate.Value).Trim() != "")
				m_EndDate = RequestEndDate.Text.Trim();


		}




		/// <summary>
		/// 실행계획 생산수립시 생성자
		/// </summary>
		/// <param name="m_RequestItem"></param>
		/// <param name="m_RequestBeginDate"></param>
		/// <param name="m_RequestEndDate"></param>
		public Search(string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName, WebDateChooser m_RequestBeginDate, WebDateChooser m_RequestEndDate)
		{
			m_Page = "ExecutionPlanInfo";

			m_ItemNum = m_RequestItem.Trim();
			m_DrawNum = m_RequestDrawNum.Trim();
			m_ItemName = m_RequestItemName.Trim();

			if(Convert.ToString(m_RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = m_RequestBeginDate.Text.Trim();
			if(Convert.ToString(m_RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = m_RequestEndDate.Text.Trim();
		}


		/// <summary>
		/// 미수금보기 현황페이지 생성자
		/// </summary>
		/// <param name="RequestItem">거래처</param>
		/// <param name="Classification1">거래처분류1</param>
		/// <param name="Classification1">거래처분류2</param>
		public Search(string ComName, string Num)
		{
			m_Page = "UnCollectMoneyLook";
			m_Com = ComName;
			m_Comnum = Num;
		}

		/// <summary>
		///영업창고입고 페이지 생성자
		///보용품입고현황 페이지 생성자
		///
		/// </summary>
		/// <param name="m_RequestPage"></param>
		/// <param name="m_RequestItem"></param>
		/// <param name="m_RequestDrawNum"></param>
		/// <param name="m_RequestItemName"></param>
		/// <param name="m_RequestBeginDate"></param>
		/// <param name="m_RequestEndDate"></param>
		public Search(string m_RequestPage, string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName, WebDateChooser m_RequestBeginDate, WebDateChooser m_RequestEndDate)
		{
			m_Page =  m_RequestPage;

			if(Convert.ToString(m_RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = m_RequestBeginDate.Text.Trim();
			if(Convert.ToString(m_RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = m_RequestEndDate.Text.Trim();

			m_ItemNum = m_RequestItem.Trim();
			m_DrawNum = m_RequestDrawNum.Trim();
			m_ItemName = m_RequestItemName.Trim();


		}




		/// <summary>
		/// 작업일보 등록 페이지 생성자
		/// </summary>
		/// <param name="m_RequestPage"></param>
		/// <param name="m_RequestItem"></param>
		/// <param name="m_RequestDrawNum"></param>
		/// <param name="m_RequestItemName"></param>
		/// <param name="wcname"></param>
		/// <param name="m_RequestBeginDate"></param>
		/// <param name="m_RequestEndDate"></param>
		public Search(string m_RequestPage, string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName,  DropDownList wcname, WebDateChooser m_RequestBeginDate, WebDateChooser m_RequestEndDate)
		{
			
			m_Page =  m_RequestPage;

			if(Convert.ToString(m_RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = m_RequestBeginDate.Text.Trim();
			if(Convert.ToString(m_RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = m_RequestEndDate.Text.Trim();
		
			m_ItemNum = m_RequestItem.Trim();
			m_DrawNum = m_RequestDrawNum.Trim();
			m_ItemName = m_RequestItemName.Trim();

			if(wcname.SelectedItem.Value.Trim() =="")
				m_WCName = "";
			else
				m_WCName = wcname.SelectedItem.Text.Trim();


		}

		/// <summary>
		/// 작업일보현황 페이지 생성자
		/// </summary>
		/// <param name="m_RequestPage">요청페이지</param>
		/// <param name="m_RequestItem">품목명</param>
		/// <param name="wcname">WC명</param>
		/// <param name="m_RequestBeginDate">시작일</param>
		/// <param name="m_RequestEndDate">종료일</param>
		public Search(string m_RequestPage, string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName,  WebCombo wcname, WebDateChooser m_RequestBeginDate, WebDateChooser m_RequestEndDate,DropDownList dlWorker)
		{
			
			m_Page =  m_RequestPage;

			if(Convert.ToString(m_RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = m_RequestBeginDate.Text.Trim();
			if(Convert.ToString(m_RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = m_RequestEndDate.Text.Trim();
		
			m_ItemNum = m_RequestItem.Trim();
			m_DrawNum = m_RequestDrawNum.Trim();
			m_ItemName = m_RequestItemName.Trim();

			if(wcname.DataValue ==null)
				m_WCName = Convert.ToString(wcname.DisplayValue);
			else
				m_WCName = Convert.ToString(wcname.DataValue);
			
			if(dlWorker.SelectedIndex == 0)
				m_Worker = "";
			else
				m_Worker = dlWorker.SelectedItem.Text;


		}


		/// <summary>
		/// 현황관리페이지내의 작업일보현황페이지
		/// </summary>
		/// <param name="m_RequestPage"></param>
		/// <param name="m_RequestItem"></param>
		/// <param name="m_RequestDrawNum"></param>
		/// <param name="m_RequestItemName"></param>
		/// <param name="dlwcname"></param>
		/// <param name="m_RequestBeginDate"></param>
		/// <param name="m_RequestEndDate"></param>
		/// <param name="dlWorker"></param>
		public Search(string m_RequestPage, string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName,  DropDownList dlwcname, WebDateChooser m_RequestBeginDate, WebDateChooser m_RequestEndDate,DropDownList dlWorker)
		{
			
			m_Page =  m_RequestPage;

			if(Convert.ToString(m_RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = m_RequestBeginDate.Text.Trim();
			if(Convert.ToString(m_RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = m_RequestEndDate.Text.Trim();
		
			m_ItemNum = m_RequestItem.Trim();
			m_DrawNum = m_RequestDrawNum.Trim();
			m_ItemName = m_RequestItemName.Trim();

			if(dlWorker.SelectedIndex == 0)
				m_WCName = "";
			else
				m_WCName = dlWorker.SelectedItem.Text;
			
			if(dlWorker.SelectedIndex == 0)
				m_Worker = "";
			else
				m_Worker = dlWorker.SelectedItem.Text;


		}


		/// <summary>
		/// 협력사 품질지표페이지 
		/// </summary>
		/// <param name="m_RequestPage">요청페이지</param>
		/// <param name="m_RequestBeginDate">시작일</param>
		/// <param name="m_RequestEndDate">종료일</param>
		/// <param name="m_RequestCom">거래처</param>
		public Search(string m_RequestPage, WebDateChooser m_RequestBeginDate, WebDateChooser m_RequestEndDate,WebCombo m_RequestCom)
		{
			
			m_Page =  m_RequestPage;

			if(Convert.ToString(m_RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = m_RequestBeginDate.Text;
			if(Convert.ToString(m_RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = m_RequestEndDate.Text.Trim();
		
		
			if(m_RequestCom.DataValue ==null)
				m_Com = Convert.ToString(m_RequestCom.DisplayValue);
			else
				m_Com = Convert.ToString(m_RequestCom.DataValue);


		}
	

		/// <summary>
		/// 상품구매의뢰페이지 생성자
		/// </summary>
		/// <param name="m_RequestPage">요청페이지명</param>
		/// <param name="m_RequestItem">품목번호</param>
		/// <param name="m_RequestDrawNum">도면번호</param>
		/// <param name="m_RequestItemName">품목명</param>
		/// <param name="m_RequestBeginDate">수주검색시작일</param>
		/// <param name="m_RequestEndDate">수주검색종료일</param>
		/// <param name="m_FromDate">납기검색시작일</param>
		/// <param name="m_ToDate">납기검색종료일</param>
		/// <param name="CompanyName">회사명</param>
		/// <param name="BusinessRegistrationNum">사업자번호</param>
		public Search(string m_RequestPage, string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName, WebDateChooser m_RequestBeginDate, WebDateChooser m_RequestEndDate, WebDateChooser m_FromDate, WebDateChooser m_ToDate, string CompanyName, string BusinessRegistrationNum)
		{
			m_Page =  m_RequestPage.Trim();
			
			if(Convert.ToString(m_RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = m_RequestBeginDate.Text.Trim();
			if(Convert.ToString(m_RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = m_RequestEndDate.Text.Trim();


			if(Convert.ToString(m_FromDate.Value).Trim() == "")
				BeginDate = "1999-01-01";
			else
				BeginDate = m_FromDate.Text.Trim();
			if(Convert.ToString(m_ToDate.Value).Trim() == "")
				EndDate = "2079-06-01";
			else
				EndDate = m_ToDate.Text.Trim();
		

			m_Com = CompanyName.Trim();
			m_Comnum = BusinessRegistrationNum.Trim();

			m_ItemNum = m_RequestItem.Trim();
			m_DrawNum = m_RequestDrawNum.Trim();
			m_ItemName = m_RequestItemName.Trim();
		}


		/// <summary>
		/// 클레임현황페이지
		/// 매출등록페이지 생성자, 매출등록현황페이지 생성자,
		/// 부자재 입고페이지 생성자
		/// </summary>
		/// <param name="m_RequestPage">요청페이지</param>
		/// <param name="m_RequestItem">요청품목번호</param>
		/// <param name="m_RequestDrawNum">요청도면번호</param>
		/// <param name="m_RequestItemName">요청품목명</param>
		/// <param name="m_RequestBeginDate">요청시작일</param>
		/// <param name="m_RequestEndDate">요청종료일</param>
		/// <param name="CompanyName">요청회사명</param>
		/// <param name="BusinessRegistrationNum">요청사업자번호</param>
		public Search(string m_RequestPage, string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName, WebDateChooser m_RequestBeginDate, WebDateChooser m_RequestEndDate, string CompanyName, string BusinessRegistrationNum)
		{
			m_Page =  m_RequestPage.Trim();
			
			if(Convert.ToString(m_RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = m_RequestBeginDate.Text.Trim();
			if(Convert.ToString(m_RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = m_RequestEndDate.Text.Trim();
			
			m_Com = CompanyName.Trim();
			m_Comnum = BusinessRegistrationNum.Trim();
			m_ItemNum = m_RequestItem.Trim();
			m_DrawNum = m_RequestDrawNum.Trim();
			m_ItemName = m_RequestItemName.Trim();
		}
		
		/// <summary>
		/// 생산의뢰 페이지 생성자, 구매가입고페이지 생성자, 
		/// 구매가입고현황페이지 생성자, 외주가입고페이지 생성자, 
		/// 외주의뢰현황페이지 생성자
		/// </summary>
		/// <param name="m_RequestPage">요청페이지</param>
		/// <param name="m_RequestItem">요청품번</param>
		/// <param name="m_RequestDrawNum">요청도번</param>
		/// <param name="m_RequestItemName">요청품목</param>
		/// <param name="m_RequestBeginDate">요청시작일</param>
		/// <param name="Classification">요청제품팀</param>
		/// <param name="m_RequestEndDate">요청종료일</param>
		/// <param name="CompanyName">요청회사</param>
		/// <param name="BusinessRegistrationNum">요청회사번호</param>
		public Search(string m_RequestPage, string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName, WebDateChooser m_RequestBeginDate, string Classification, WebDateChooser m_RequestEndDate, string CompanyName, string BusinessRegistrationNum)
		{
			m_Page =  m_RequestPage.Trim();
			
			if(Convert.ToString(m_RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = m_RequestBeginDate.Text.Trim();
			if(Convert.ToString(m_RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = m_RequestEndDate.Text.Trim();
			
			m_Com = CompanyName.Trim();
			m_Comnum = BusinessRegistrationNum.Trim();
			m_ItemNum = m_RequestItem.Trim();
			m_DrawNum = m_RequestDrawNum.Trim();
			m_ItemName = m_RequestItemName.Trim();

			m_Classification1 = Classification.Trim();
		}

		/// <summary>
		/// 부자재 입고현황 페이지
		/// </summary>
		/// <param name="m_RequestPage">요청페이지</param>
		/// <param name="m_RequestItem">요청품번</param>
		/// <param name="m_RequestDrawNum">요청도번</param>
		/// <param name="m_RequestItemName">요청품목</param>
		/// <param name="m_RequestBeginDate">요청시작일</param>
		/// <param name="Classification">요청제품팀</param>
		/// <param name="m_RequestEndDate">요청종료일</param>
		/// <param name="CompanyName">요청회사</param>
		/// <param name="BusinessRegistrationNum">요청회사번호</param>
		public Search(string m_RequestPage, string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName, WebDateChooser m_RequestBeginDate, string Classification, WebDateChooser m_RequestEndDate, string CompanyName, string BusinessRegistrationNum, DropDownList ddlProperty)
		{
			m_Page =  m_RequestPage.Trim();
			
			if(Convert.ToString(m_RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = m_RequestBeginDate.Text.Trim();
			if(Convert.ToString(m_RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = m_RequestEndDate.Text.Trim();
			
			m_Com = CompanyName.Trim();
			m_Comnum = BusinessRegistrationNum.Trim();
			m_ItemNum = m_RequestItem.Trim();
			m_DrawNum = m_RequestDrawNum.Trim();
			m_ItemName = m_RequestItemName.Trim();

			m_Classification1 = Classification.Trim();
			m_Property = ddlProperty.SelectedItem.Value;
		}


		/// <summary>
		/// 상품/제품 출고페이지 생성자
		/// 상품/제품 출고현황페이지 생성자, 외주출고현황페이지 생성자
		/// </summary>
		/// <param name="m_RequestPage">요청페이지명</param>
		/// <param name="m_RequestItem">요청품번</param>
		/// <param name="m_RequestDrawNum">요청도번</param>
		/// <param name="m_RequestItemName">요청품명</param>
		/// <param name="m_RequestBeginDate">요청시작일</param>
		/// <param name="m_RequestEndDate">요청종료일</param>
		/// <param name="CompanyName">요청회사명</param>
		/// <param name="BusinessRegistrationNum">요청사업자등록번호</param>
		/// <param name="State">진행상태</param>
		public Search(string m_RequestPage, string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName, WebDateChooser m_RequestBeginDate, WebDateChooser m_RequestEndDate, string CompanyName, string BusinessRegistrationNum, string State)
		{
			m_Page =  m_RequestPage.Trim();
			
			if(Convert.ToString(m_RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = m_RequestBeginDate.Text.Trim();
			if(Convert.ToString(m_RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = m_RequestEndDate.Text.Trim();
			
			m_Com = CompanyName.Trim();
			m_Comnum = BusinessRegistrationNum.Trim();
			m_ItemNum = m_RequestItem.Trim();
			m_DrawNum = m_RequestDrawNum.Trim();
			m_ItemName = m_RequestItemName.Trim();
			if(m_Page == "GoodsManufactureOutStorehouse")
				m_OrderNum = State.Trim();
			else if(m_Page == "SubBuyingOrderPC")
			{
				if(State.Trim() != "전체")
					m_ProgressState = State.Trim();
			}
			else
				m_ProgressState = State.Trim();
		}

		/// <summary>
		/// 상품/제품 출고현황페이지 생성자
		/// </summary>
		/// <param name="m_RequestPage">요청페이지명</param>
		/// <param name="m_RequestItem">요청품번</param>
		/// <param name="m_RequestDrawNum">요청도번</param>
		/// <param name="m_RequestItemName">요청품명</param>
		/// <param name="m_RequestBeginDate">요청시작일</param>
		/// <param name="m_RequestEndDate">요청종료일</param>
		/// <param name="CompanyName">요청회사명</param>
		/// <param name="BusinessRegistrationNum">요청사업자등록번호</param>
		/// <param name="State">진행상태</param>
		/// <param name="StoreNum">자산분류</param>
		public Search(string m_RequestPage, string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName, WebDateChooser m_RequestBeginDate, WebDateChooser m_RequestEndDate, string CompanyName, string BusinessRegistrationNum, string State, int StoreNum)
		{
			m_Page =  m_RequestPage.Trim();

			m_StoreNum = StoreNum;
			if(Convert.ToString(m_RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = m_RequestBeginDate.Text.Trim();
			if(Convert.ToString(m_RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = m_RequestEndDate.Text.Trim();
			
			m_Com = CompanyName.Trim();
			m_Comnum = BusinessRegistrationNum.Trim();
			m_ItemNum = m_RequestItem.Trim();
			m_DrawNum = m_RequestDrawNum.Trim();
			m_ItemName = m_RequestItemName.Trim();
			if(m_Page == "GoodsManufactureOutStorehouse")
				m_OrderNum = State.Trim();
			else
				m_ProgressState = State.Trim();
		}


		/// <summary>
		/// 수주현황페이지 생성자, 보용품수주현황 페이지 생성자
		/// 생산의뢰현황페이지 생성자,
		/// 구매발주현황페이지 생성자, 외주가입고현황페이지 생성자
		/// 외주발주현황페이지 생성자, 외주출고페이지 생성자
		/// 외주출고현황페이지 생성자
		/// </summary>
		/// <param name="m_RequestPage">요청페이지</param>
		/// <param name="m_RequestItem">요청품목</param>
		/// <param name="m_RequestDrawNum">요청도번</param>
		/// <param name="m_RequestItemName">요청품명</param>
		/// <param name="m_RequestBeginDate">요청시작일</param>
		/// <param name="m_RequestEndDate">요청종료일</param>
		/// <param name="CompanyName">요청회사명</param>
		/// <param name="BusinessRegistrationNum">요청회사번호</param>
		/// <param name="State">진행상태</param>
		/// <param name="Classification">제품팀</param>
		public Search(string m_RequestPage, string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName, WebDateChooser m_RequestBeginDate, WebDateChooser m_RequestEndDate, string CompanyName, string BusinessRegistrationNum, string State, string Classification)
		{
			m_Page =  m_RequestPage.Trim();
			
			if(Convert.ToString(m_RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = m_RequestBeginDate.Text.Trim();
			if(Convert.ToString(m_RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = m_RequestEndDate.Text.Trim();
			
			m_Com = CompanyName.Trim();
			m_Comnum = BusinessRegistrationNum.Trim();
			m_ItemNum = m_RequestItem.Trim();
			m_DrawNum = m_RequestDrawNum.Trim();
			m_ItemName = m_RequestItemName.Trim();
			m_ProgressState = State.Trim();

			if(m_Page == "ProductionRequestPC")
				m_Source = Classification.Trim();
			else
				m_Classification1 = Classification.Trim();


		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="m_RequestPage">요청페이지</param>
		/// <param name="m_RequestItem">요청품목명</param>
		/// <param name="m_RequestBeginDate">시작일</param>
		/// <param name="m_RequestEndDate">종료일</param>
		/// <param name="m_RequestCom">거래처명</param>
		/// <param name="m_RequestProgressState">진행상태</param>
		public Search(string m_RequestPage, string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName, WebDateChooser m_RequestBeginDate, WebDateChooser m_RequestEndDate, WebCombo m_RequestCom, string m_RequestProgressState)
		{
			m_Page =  m_RequestPage.Trim();
			if(m_RequestProgressState.ToString() =="0")
				m_ProgressState = Convert.ToString(Convert.DBNull);
			else
				m_ProgressState = m_RequestProgressState.Trim();
			
			if(Convert.ToString(m_RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = m_RequestBeginDate.Text.Trim();
			if(Convert.ToString(m_RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = m_RequestEndDate.Text.Trim();

			m_ItemNum = m_RequestItem.Trim();
			m_DrawNum = m_RequestDrawNum.Trim();
			m_ItemName = m_RequestItemName.Trim();

			if(m_RequestCom.DataValue ==null)
				m_Com = Convert.ToString(m_RequestCom.DisplayValue);
			else
				m_Com = Convert.ToString(m_RequestCom.DataValue);
			
		}


		/// <summary>
		/// 생산계획수립 페이지 생성자, 작업계획수립페이지 생성자, 
		/// 자재의뢰현황 페이지 생성자, 자재소요량산출 페이지 생성자
		/// </summary>
		/// <param name="m_RequestPage"></param>
		/// <param name="m_RequestItem"></param>
		/// <param name="m_RequestDrawNum"></param>
		/// <param name="m_RequestItemName"></param>
		/// <param name="m_RequestBeginDate"></param>
		/// <param name="m_RequestEndDate"></param>
		/// <param name="m_Classification"></param>
		public Search(string m_RequestPage, string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName, WebDateChooser m_RequestBeginDate, WebDateChooser m_RequestEndDate, string m_Classification)
		{
			m_Page =  m_RequestPage.Trim();
			m_Classification1 = m_Classification;
			
			if(Convert.ToString(m_RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = m_RequestBeginDate.Text.Trim();
			if(Convert.ToString(m_RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = m_RequestEndDate.Text.Trim();

			m_ItemNum = m_RequestItem.Trim();
			m_DrawNum = m_RequestDrawNum.Trim();
			m_ItemName = m_RequestItemName.Trim();
			
		}


		/// <summary>
		/// 생산계획현황페이지 생성자
		/// </summary>
		/// <param name="m_RequestItem">품목번호</param>
		/// <param name="m_RequestDrawNum">도면번호</param>
		/// <param name="m_RequestItemName">품목명</param>
		/// <param name="m_RequestBeginDate">시작일</param>
		/// <param name="m_RequestEndDate">종료일</param>
		/// <param name="m_RequestMinDate">납기시작일</param>
		/// <param name="m_RequestMaxDate">납기종료일</param>
		/// <param name="m_Classification">제품팀</param>
		/// <param name="state">진행상태</param>
		/// <param name="m_RequestPage">의뢰페이지</param>
		public Search(string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName, WebDateChooser m_RequestBeginDate, WebDateChooser m_RequestEndDate, WebDateChooser m_RequestMinDate, WebDateChooser m_RequestMaxDate, string m_Classification, string state,string m_RequestPage)
		{
			m_Page =  m_RequestPage.Trim();
			m_Classification1 = m_Classification;
			m_ProgressState = state;
			
			if(Convert.ToString(m_RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = m_RequestBeginDate.Text.Trim();
			if(Convert.ToString(m_RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = m_RequestEndDate.Text.Trim();

			if(Convert.ToString(m_RequestMinDate.Value).Trim() == "")
				BeginDate = "1999-01-01";
			else
				BeginDate = m_RequestMinDate.Text.Trim();
			if(Convert.ToString(m_RequestMaxDate.Value).Trim() == "")
				EndDate = "2079-06-01";
			else
				EndDate = m_RequestMaxDate.Text.Trim();

			m_ItemNum = m_RequestItem.Trim();
			m_DrawNum = m_RequestDrawNum.Trim();
			m_ItemName = m_RequestItemName.Trim();
			
		}

		/// <summary>
		/// 생산의뢰현황 페이지 생성자,  
		/// </summary>
		/// <param name="m_RequestPage">요청페이지</param>
		/// <param name="m_RequestItem">요청품목</param>
		/// <param name="m_RequestBeginDate">시작일</param>
		/// <param name="m_RequestEndDate">종료일</param>
		/// <param name="m_RequestCom">거래처명</param>
		/// <param name="m_RequestProgressState">진행상태</param>
		/// <param name="m_RequestSource">의뢰원천</param>
		public Search(string m_RequestPage, string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName, WebDateChooser m_RequestBeginDate, WebDateChooser m_RequestEndDate, WebCombo m_RequestCom, string m_RequestProgressState, string m_RequestSource)
		{
			m_Page =  m_RequestPage.ToString();
			m_ProgressState = m_RequestProgressState.Trim();
			
			m_Source = m_RequestSource.Trim();

			if(Convert.ToString(m_RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = m_RequestBeginDate.Text.Trim();
			if(Convert.ToString(m_RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = m_RequestEndDate.Text.Trim();


			m_ItemNum = m_RequestItem.Trim();
			m_DrawNum = m_RequestDrawNum.Trim();
			m_ItemName = m_RequestItemName.Trim();

			if(m_RequestCom.DataValue ==null)
				m_Com = Convert.ToString(m_RequestCom.DisplayValue);
			else
				m_Com = Convert.ToString(m_RequestCom.DataValue);
			
			
		}

		/// <summary>
		/// 상품구매의뢰현황 생성자
		/// </summary>
		/// <param name="m_RequestPage"></param>
		/// <param name="m_RequestItem"></param>
		/// <param name="m_RequestBeginDate"></param>
		/// <param name="m_RequestEndDate"></param>
		/// <param name="m_RequestProgressState"></param>
		/// <param name="m_RequestSource"></param>
		public Search(string m_RequestPage, string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName, string m_RequestProgressState, string m_RequestSource, WebDateChooser m_RequestBeginDate, WebDateChooser m_RequestEndDate)
		{
			m_Page =  m_RequestPage.Trim();
			m_ProgressState = m_RequestProgressState.Trim();
			m_Source = m_RequestSource.Trim();

			if(Convert.ToString(m_RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = m_RequestBeginDate.Text.Trim();
			if(Convert.ToString(m_RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = m_RequestEndDate.Text.Trim();


			m_ItemNum = m_RequestItem.Trim();
			m_DrawNum = m_RequestDrawNum.Trim();
			m_ItemName = m_RequestItemName.Trim();

			
			
			
		}

		

		/// <summary>
		/// 수금현황페이지 생성자, 지급현황페이지 생성자
		/// </summary>
		/// <param name="i">페이지도면자 'a'</param>
		/// <param name="m_RequestPage">요청페이지명</param>
		/// <param name="m_RequestBeginDate">시작일</param>
		/// <param name="m_RequestEndDate">종료일</param>
		/// <param name="m_RequestCom">거래처명</param>
		/// <param name="m_RequestDecisionMethod">결재방법</param>
		//public Search(char i, string m_RequestPage, WebDateChooser m_RequestBeginDate, WebDateChooser m_RequestEndDate, WebCombo m_RequestCom, string m_RequestDecisionMethod)
		public Search(string m_RequestPage, WebDateChooser m_RequestBeginDate, WebDateChooser m_RequestEndDate, WebCombo m_RequestCom, string m_RequestDecisionMethod)
		{
			m_Page =  m_RequestPage.Trim();
			m_DecisionMethod = m_RequestDecisionMethod.Trim();

			if(Convert.ToString(m_RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = m_RequestBeginDate.Text.Trim();
			if(Convert.ToString(m_RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = m_RequestEndDate.Text.Trim();


			if(m_RequestCom.DataValue ==null)
				m_Com = Convert.ToString(m_RequestCom.DisplayValue);
			else
				m_Com = Convert.ToString(m_RequestCom.DataValue);
			
			
		}


		/// <summary>
		/// 수금등록현황페이지 생성자, 기타공제 등록 현황페이지 생성자
		/// </summary>
		/// <param name="m_RequestPage">요청페이지</param>
		/// <param name="m_RequestBeginDate">요청시작일</param>
		/// <param name="m_RequestEndDate">요청종료일</param>
		/// <param name="Com">요청회사명</param>
		/// <param name="Num">요청사업자번호</param>
		public Search(string m_RequestPage, WebDateChooser m_RequestBeginDate, WebDateChooser m_RequestEndDate, string Com, string Num)
		{
			m_Page =  m_RequestPage.Trim();

			if(Convert.ToString(m_RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = m_RequestBeginDate.Text.Trim();
			if(Convert.ToString(m_RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = m_RequestEndDate.Text.Trim();

			m_Com = Com;
			m_Comnum = Num;
		}


		
		/// <summary>
		/// 창고이동페이지 생성자
		/// </summary>
		/// <param name="m_RequestPage">요청페이지</param>
		/// <param name="m_RequestItem">요청품목명</param>
		/// <param name="num">영업창고번호</param>
		public Search(string m_RequestPage, string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName, int count)
		{
			m_Page =  m_RequestPage.Trim();
			m_Store = "BS_MT";
			
			m_StoreNum = count;
			
			m_ItemNum = m_RequestItem.Trim();
			m_DrawNum = m_RequestDrawNum.Trim();
			m_ItemName = m_RequestItemName.Trim();
		}


		/// <summary>
		/// 기타입출고 등록 페이지 생성자
		/// </summary>
		/// <param name="m_RequestPage">요청페이지</param>
		/// <param name="m_RequestItem">품목명</param>
		/// <param name="m_RequestCom">외주창고내의 거래처번호(사업자번호)</param>
		/// <param name="m_Requeststore">창고명</param>
		/// <param name="count">영업창고번호</param>
		public Search(string m_RequestPage, string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName, WebCombo m_RequestCom, string m_Requeststore, int count)
		{
			m_Page =  m_RequestPage.Trim();
			m_Store = m_Requeststore.Trim();
			if(m_Store == "원자재창고")
				m_Store = "RMS_MT";
			else if(m_Store == "생산창고")
				m_Store = "PS_MT";
			else if(m_Store == "외주창고")
				m_Store = "OS_MT";
			else if(m_Store == "영업1창고")
				m_Store = "BS_MT";
			else if(m_Store == "영업2창고")
				m_Store = "BS_MT";
			else if(m_Store == "영업3창고")
				m_Store = "BS_MT";
			else
				m_Store = "DS_MT";

			m_StoreNum = count;
			
			m_ItemNum = m_RequestItem.Trim();
			m_DrawNum = m_RequestDrawNum.Trim();
			m_ItemName = m_RequestItemName.Trim();
			m_hdItemNum = m_RequestItem.Trim();
			if(m_RequestCom.DataValue == null)
				m_Comnum = Convert.ToString(m_RequestCom.DataValue);
			else
				m_Comnum = Convert.ToString(m_RequestCom.DataValue);
		}

		/// <summary>
		/// 하위품목투입 등록 페이지 생성자
		/// </summary>
		/// <param name="m_RequestPage">요청페이지</param>
		/// <param name="m_RequestItem">품목명</param>
		/// <param name="m_Requeststore">창고명</param>
		/// <param name="count">영업창고번호</param>
		public Search(string m_RequestPage, string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName, string m_Requeststore, int count)
		{
			m_Page =  m_RequestPage.Trim();
			m_Store = m_Requeststore.Trim();
			if(m_Store == "원자재창고")
				m_Store = "RMS_MT";
			else if(m_Store == "생산창고")
				m_Store = "PS_MT";
			else if(m_Store == "영업1창고")
				m_Store = "BS_MT";
			else if(m_Store == "영업2창고")
				m_Store = "BS_MT";
			else if(m_Store == "영업3창고")
				m_Store = "BS_MT";

			m_StoreNum = count;
			
			m_ItemNum = m_RequestItem.Trim();
			m_DrawNum = m_RequestDrawNum.Trim();
			m_ItemName = m_RequestItemName.Trim();
		}


		/// <summary>
		/// 창고이동현황 페이지 생성자
		/// </summary>
		/// <param name="m_RequestPage">요청페이지</param>
		/// <param name="m_RequestItem">요청품목명</param>
		/// <param name="i">페이지도면자</param>
		/// <param name="m_RequestBeginDate">시작일</param>
		/// <param name="m_RequestEndDate">종료일</param>
		public Search(string m_RequestPage, string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName, int i, WebDateChooser m_RequestBeginDate, WebDateChooser m_RequestEndDate)
		{
			m_Page =  m_RequestPage.Trim();
			
			if(Convert.ToString(m_RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = m_RequestBeginDate.Text.Trim();
			if(Convert.ToString(m_RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = m_RequestEndDate.Text.Trim();


			m_ItemNum = m_RequestItem;
			m_DrawNum = m_RequestDrawNum;
			m_ItemName = m_RequestItemName;
		}
		
		/// <summary>
		/// 품목별 재고현황페이지 생성자, 
		/// 협력사 재고현황페이지 생성자		///
		/// </summary>
		/// <param name="m_RequestPage">요청페이지</param>
		/// <param name="m_RequestItem">요청품목명</param>
		public Search(string m_RequestPage, string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName)
		{
			m_Page =  m_RequestPage.Trim();
			m_ItemNum = m_RequestItem.Trim();
			m_DrawNum = m_RequestDrawNum.Trim();
			m_ItemName = m_RequestItemName.Trim();
		}

//		/// <summary>
//		/// 협력사 재고현황페이지 생성자
//		/// </summary>
//		/// <param name="m_RequestPage">요청페이지</param>
//		/// <param name="m_RequestItem">요청품목명</param>
//		public Search(string m_RequestPage, string name, string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName)
//		{
//			m_Page =  m_RequestPage.ToString();
//            
//			
//			m_ItemNum = m_RequestItem.Trim();
//			m_DrawNum = m_RequestDrawNum.Trim();
//			m_ItemName = m_RequestItemName.Trim();
//		}

		


		/// <summary>
		/// 창고별 재고현황페이지 생성자, 
		/// </summary>
		/// <param name="m_RequestPage">요청페이지</param>
		/// <param name="m_RequestStore">요청창고명</param>
		/// <param name="a">영업창고 번호</param>
		public Search(string m_RequestPage, string m_RequestStore, int a,  string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName, string m_RequesthdItem)
		{
			m_Page =  m_RequestPage.Trim();
			m_Store = m_RequestStore.Trim();
			if(m_Store == "원자재창고")
				m_Store = "RMS_MT";
			else if(m_Store == "생산창고")
				m_Store = "PS_MT";
			else if(m_Store == "외주창고")
				m_Store = "OS_MT";
			else if(m_Store == "영업1창고")
				m_Store = "BS_MT";
			else if(m_Store == "영업2창고")
				m_Store = "BS_MT";
			else if(m_Store == "영업3창고")
				m_Store = "BS_MT";
			else if(m_Store == "보용품창고")
				m_Store = "AS_MT";
			else
				m_Store = "DS_MT";

			m_ItemNum = m_RequestItem;
			m_DrawNum = m_RequestDrawNum;
			m_ItemName = m_RequestItemName;
			m_hdItemNum = m_RequesthdItem;
			m_StoreNum = a;
		}

		/// <summary>
		/// 창고별 재고현황페이지 생성자(외주창고 선택시) 
		/// </summary>
		/// <param name="m_RequestPage">요청페이지</param>
		/// <param name="m_RequestStore">요청창고명</param>
		/// <param name="a">영업창고 번호</param>
		/// <param name="com">거래처웹콤보</param>
		public Search(string m_RequestPage, string m_RequestStore, int a, WebCombo com, string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName)
		{
			m_Page =  m_RequestPage.Trim();
			m_Store = m_RequestStore.Trim();

			if(com.DataValue ==null)
				m_Com = Convert.ToString(com.DisplayValue);
			else
				m_Comnum = Convert.ToString(com.DataValue);

			if(m_Store == "원자재창고")
				m_Store = "RMS_MT";
			else if(m_Store == "생산창고")
				m_Store = "PS_MT";
			else if(m_Store == "외주창고")
				m_Store = "OS_MT";
			else if(m_Store == "영업1창고")
				m_Store = "BS_MT";
			else if(m_Store == "영업2창고")
				m_Store = "BS_MT";
			else if(m_Store == "영업3창고")
				m_Store = "BS_MT";
			else
				m_Store = "DS_MT";
			m_StoreNum = a;

			m_ItemNum = m_RequestItem.Trim();
			m_DrawNum = m_RequestDrawNum.Trim();
			m_ItemName = m_RequestItemName.Trim();
		}


		/// <summary>
		/// 기타입출고현황페이지 생성자
		/// </summary>
		/// <param name="m_RequestPage">요청페이지</param>
		/// <param name="m_RequestItem">요청품목명</param>
		/// <param name="m_RequestStore">요청창고명</param>
		/// <param name="m_RequestBeginDate">시작일</param>
		/// <param name="m_RequestEndDate">종료일</param>
		public Search(WebDateChooser m_RequestBeginDate, WebDateChooser m_RequestEndDate, string m_RequestPage, string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName, string m_RequestStore)
		{
			m_Page =  m_RequestPage.Trim();
			if(m_RequestStore.ToString() == "0")
				m_Store = "";
			else
				m_Store = m_RequestStore.Trim();
					
			

			if(Convert.ToString(m_RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = m_RequestBeginDate.Text.Trim();
			if(Convert.ToString(m_RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = m_RequestEndDate.Text.Trim();

			m_ItemNum = m_RequestItem.Trim();
			m_DrawNum = m_RequestDrawNum.Trim();
			m_ItemName = m_RequestItemName.Trim();
		}




		/// <summary>
		/// 거래처명
		/// </summary>
		/// <param name="m_RequestPage"></param>
		/// <param name="com"></param>
		/// <param name="ddl1"></param>
		/// <param name="ddl2"></param>
		public Search(string m_RequestPage,WebCombo com, DropDownList ddl1, DropDownList ddl2)
		{
			m_Page =  m_RequestPage.Trim();
			if(com.DataValue ==null)
				m_Com = Convert.ToString(com.DisplayValue);
			else
				m_Com = Convert.ToString(com.DataValue);
			m_Classification1 = ddl1.SelectedValue.ToString();
			m_Classification2 = ddl2.SelectedValue.ToString();
		}


		/// <summary>
		/// 협력사 납품현황페이지,
		/// 협력사 출고현황페이지,
		/// </summary>
		/// <param name="m_RequestPage">요청페이지</param>
		/// <param name="ID">사용자 아이디</param>
		/// <param name="m_RequestBeginDate">시작일</param>
		/// <param name="m_RequestEndDate">종료일</param>
		public Search(string m_RequestPage,WebDateChooser m_RequestBeginDate, WebDateChooser m_RequestEndDate, string ID)
		{
			m_Page =  m_RequestPage.Trim();

			if(Convert.ToString(m_RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = m_RequestBeginDate.Text.Trim();
			if(Convert.ToString(m_RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = m_RequestEndDate.Text.Trim();

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select BusinessRegistrationNum From UI_MT Where ID = @id";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Parameters.Add("@id",SqlDbType.VarChar).Value = ID;
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				m_Com = dr["BusinessRegistrationNum"].ToString();
			}
			conn.Close();

		}


		/// <summary>
		/// 협력사 품질현황페이지
		/// 협력사 품질지표페이지
		/// </summary>
		/// <param name="m_RequestPage">요청페이지</param>
		/// <param name="UserID">사용자 아이디</param>
		/// <param name="m_RequestItem">품목웹콤보</param>
		/// <param name="m_RequestBeginDate">시작일</param>
		/// <param name="m_RequestEndDate">종료일</param>
		public Search(string m_RequestPage, string UserID , string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName, WebDateChooser m_RequestBeginDate, WebDateChooser m_RequestEndDate)
		{
			m_Page =  m_RequestPage.Trim();

			if(Convert.ToString(m_RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = m_RequestBeginDate.Text.Trim();
			if(Convert.ToString(m_RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = m_RequestEndDate.Text.Trim();

			m_ItemNum = m_RequestItem;
			m_DrawNum = m_RequestDrawNum;
			m_ItemName = m_RequestItemName;

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select BusinessRegistrationNum From UI_MT Where ID = @id";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Parameters.Add("@id",SqlDbType.VarChar).Value = UserID;
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				m_Com = dr["BusinessRegistrationNum"].ToString();
			}
			conn.Close();

		}

		/// <summary>
		/// 작업계획현황
		/// </summary>
		/// <param name="m_RequestPage"></param>
		/// <param name="m_RequestItem"></param>
		/// <param name="m_RequestDrawNum"></param>
		/// <param name="m_RequestItemName"></param>
		/// <param name="dlProgress"></param>
		/// <param name="m_RequestBeginDate"></param>
		/// <param name="m_RequestEndDate"></param>
		/// <param name="wcName"></param>
		/// <param name="FromDate"></param>
		/// <param name="ToDate"></param>	
		public Search(string m_RequestPage, string m_RequestItem, string m_RequestDrawNum, string m_RequestItemName, DropDownList dlProgress,  WebDateChooser m_RequestBeginDate, WebDateChooser m_RequestEndDate, WebCombo wcName, WebDateChooser FromDate, WebDateChooser ToDate)
		{
			m_Page =  m_RequestPage.Trim();

			if(Convert.ToString(m_RequestBeginDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = m_RequestBeginDate.Text.Trim();
			if(Convert.ToString(m_RequestEndDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";
			else
				m_EndDate = m_RequestEndDate.Text.Trim();

			if(Convert.ToString(FromDate.Value).Trim() == "")
				BeginDate = "1999-01-01";
			else
				BeginDate = FromDate.Text.Trim();

			if(Convert.ToString(ToDate.Value).Trim() == "")
				EndDate = "2079-06-01";
			else
				EndDate = ToDate.Text.Trim();

			m_ItemNum = m_RequestItem.Trim();
			m_DrawNum = m_RequestDrawNum.Trim();
			m_ItemName = m_RequestItemName.Trim();

			if(wcName.DataValue ==null)
				m_WCName = Convert.ToString(wcName.DisplayValue);
			else
				m_WCName = Convert.ToString(wcName.DataValue);

			if(dlProgress.SelectedIndex == 0)
				m_ProgressState = Convert.ToString(Convert.DBNull);
			else
				m_ProgressState = dlProgress.SelectedItem.Value;

			
		}

		/// <summary>
		/// 부적합현황페이지생성자
		/// </summary>
		/// <param name="m_RequestPage"></param>
		/// <param name="ItemNum"></param>
		/// <param name="ItemDrawNum"></param>
		/// <param name="ItemName"></param>
		/// <param name="dlWorkDivision"></param>
		/// <param name="wcName"></param>
		/// <param name="dlProcess"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <param name="ddlWorker"></param>
		public Search(string m_RequestPage, string ItemNum, string ItemDrawNum, string ItemName, DropDownList dlWorkDivision, WebCombo wcName, DropDownList dlProcess, WebDateChooser startDate, WebDateChooser endDate, DropDownList ddlWorker)
		{
			m_Page = m_RequestPage.Trim();

			m_ItemNum = ItemNum.Trim();
			m_DrawNum = ItemDrawNum.Trim();
			m_ItemName = ItemName.Trim();

			if(dlWorkDivision.SelectedIndex == 0)
				m_WorkDivision = Convert.ToString(Convert.DBNull);
			else
				m_WorkDivision = dlWorkDivision.SelectedItem.Value;


			if(wcName.DataValue == null)
				m_WCName = Convert.ToString(wcName.DisplayValue);
			else
				m_WCName = Convert.ToString(wcName.DataValue);

			if(dlProcess.SelectedIndex == 0)
				m_Process = Convert.ToString(Convert.DBNull);
			else
				m_Process = dlProcess.SelectedItem.Value;

			if(ddlWorker.SelectedIndex == 0)
				m_Worker = "";
			else
				m_Worker = ddlWorker.SelectedItem.Value;


			if(Convert.ToString(startDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = startDate.Text.Trim();
			if(Convert.ToString(endDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";// 23:59:29";
			else
				m_EndDate = endDate.Text.Trim();//+" 23:59:29";
		}


		/// <summary>
		/// 보용품입고페이지
		/// </summary>
		/// <param name="m_RequestPage">요청페이지</param>
		/// <param name="ItemNum">품번</param>
		/// <param name="ItemDrawNum">도번</param>
		/// <param name="ItemName">품명</param>
		/// <param name="StoreName">창고명</param>
		public Search(string m_RequestPage, string ItemNum, string ItemDrawNum, string ItemName, string StoreName)
		{
			m_Page = m_RequestPage.Trim();

			m_ItemNum = ItemNum.Trim();
			m_DrawNum = ItemDrawNum.Trim();
			m_ItemName = ItemName.Trim();
			m_Store = StoreName.Trim();

		}


		/// <summary>
		/// 시스템일괄등록 이력보기
		/// </summary>
		/// <param name="m_RequestPage"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		public Search(string m_RequestPage, WebDateChooser startDate, WebDateChooser endDate)
		{
			m_Page = m_RequestPage.Trim();

			if(Convert.ToString(startDate.Value).Trim() == "")
				m_BeginDate = "1999-01-01";
			else
				m_BeginDate = startDate.Text.Trim();
			if(Convert.ToString(endDate.Value).Trim() == "")
				m_EndDate = "2079-06-01";// 23:59:29";
			else
				m_EndDate = endDate.Text.Trim();//+" 23:59:29";

		}
		
		public DataSet DataSet_search()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			
			string str = "";
			switch(m_Page)
			{
				case "History":
					str = @"Select UserName, UserID, [Date], Division, Resion From SS_HT where ([Date] >= @begin and [Date] <= @end)";
					break;
				case "ItemGroupPlanRegistration":
					str = @"Select SmallClassificationName as ItemState, ItemGroup_HT.ItemNum, ItemGroup_HT.ItemName, Quantity, ReceivingOrderDate, DeliveryDate, ItemGroupIndex
							From ItemGroup_HT 
							inner join II_MT on ItemGroup_HT.ItemNum = II_MT.ItemNum 
							left outer join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
							Where II_MT.RecodingState = 1 and ProgressCondition = '대기' and ItemGroup_HT.ItemNum Like @num and ItemGroup_HT.ItemDrawNum Like @draw and ItemGroup_HT.ItemName Like @name and (DeliveryDate >= @begin and DeliveryDate <= @end) ";
					break;
				case "ItemGroupPlanRegistrationPC":
					str = @"Select isnull(PUC1.SmallClassificationName,'') as ItemState,  PP_HT.ItemNum, PP_HT.ItemDrawNum, PP_HT.ItemName, ProductionPlanQuantity, ProductionBeginDate, DeliveryDate, 
						VolumNum, ProgressCondition, PP_HT.RegistrationPerson, PP_HT.RegistrationPersonID, 
						PP_HT.RegistrationDate, PP_HT.UpdatingPerson, PP_HT.UpdatingPersonID, PP_HT.UpdatingDate, 
						HistoryIndex, HistorySection, ProductionPlanHistoryIndex From PP_HT 
						inner join II_MT on PP_HT.ItemNum = II_MT.ItemNum 
						left outer join PUC_MT PUC1 on II_MT.ItemState = PUC1.SmallClassificationCode
						Where II_MT.RecodingState = 1 and  PP_HT.ItemNum Like @num and PP_HT.ItemDrawNum Like @draw and PP_HT.ItemName Like @name and (DeliveryDate >= @begin and DeliveryDate <= @end) and ItemClassification1 Like @Classification1 and HistorySection = '기종별등록' order by ProductionPlanHistoryIndex desc";
					break;
				case "EtcWCPlanPC"://현황관리 작업계획현황
					str = @"select WDWP_HT.EtcText, WDWP_HT.ItemNum, WDWP_HT.ItemDrawNum, WDWP_HT.ItemName, ProcessSequenceNum, ProcessCode, ProcessName, PreProcessName, ProductItemNum, ProductDrawNum, ProductName, ParentItemNum, ParentDrawNum, ParentName, WCName, WorkDistinction, WorkPlanQuantity, WorkCompletionQuantity, OrderLeadTime, WorkDate, WorkDenotationDate, 
						DeliveryDate,WorkCompleteDate, VolumNum, ProgressCondition, WDWP_HT.RegistrationPerson, WDWP_HT.RegistrationPersonID, WDWP_HT.RegistrationDate, WDWP_HT.UpdatingPerson, WDWP_HT.UpdatingPersonID, WDWP_HT.UpdatingDate, ProductionPlanHistoryIndex, WorkPlanHistoryIndex, WCDailyWorkPlanHistoryIndex 
						From WDWP_HT left outer join II_MT on WDWP_HT.ItemNum = II_MT.ItemNum
						Where WDWP_HT.ItemNum Like @num and WDWP_HT.ItemDrawNum Like @draw and WDWP_HT.ItemName Like @name 
						and ProgressCondition Like @progress 
						and (WorkDate >= @begin and WorkDate <= @end) 
						and ((WorkCompleteDate >= @fromDate and WorkCompleteDate <= @toDate) or WorkCompleteDate is null) 
						and ((DeliveryDate >= @from and DeliveryDate <= @to) or DeliveryDate is null)
						and ItemClassification1 Like @Classification1
						and ItemClassification2 Like @Classification2
						Order by  WDWP_HT.DeliveryDate asc, WorkPlanHistoryIndex desc, WDWP_HT.WorkDate asc,  WDWP_HT.ProcessSequenceNum asc";
					break;
				case "WCPlanPC"://작업계획현황
					str = @"select II_MT.Standard, WDWP_HT.EtcText, WDWP_HT.ItemNum,  WDWP_HT.ItemDrawNum,  WDWP_HT.ItemName,  WDWP_HT.ProcessSequenceNum,  WDWP_HT.ProcessCode,  WDWP_HT.ProcessName, PreProcessName, ProductItemNum, ProductDrawNum, ProductName, ParentItemNum, ParentDrawNum, ParentName,  WDWP_HT.WCName,  WDWP_HT.WorkDistinction, WorkPlanQuantity, WorkCompletionQuantity,  WDWP_HT.OrderLeadTime, WorkDate, WorkDenotationDate, 
						DeliveryDate,WorkCompleteDate, VolumNum, WDWP_HT.ProgressCondition, WDWP_HT.RegistrationPerson, WDWP_HT.RegistrationPersonID, WDWP_HT.RegistrationDate, WDWP_HT.UpdatingPerson, WDWP_HT.UpdatingPersonID, WDWP_HT.UpdatingDate, ProductionPlanHistoryIndex, WorkPlanHistoryIndex, WCDailyWorkPlanHistoryIndex From WDWP_HT 
						inner join II_MT on WDWP_HT.ItemNum = II_MT.ItemNum						
						Where II_MT.RecodingState = 1 and  WDWP_HT.ItemNum Like @num and  WDWP_HT.ItemDrawNum Like @draw and  WDWP_HT.ItemName Like @name 
						and  WDWP_HT.ProgressCondition Like @progress 
						and ItemClassification1 like @Classification1
						and ItemClassification2 like @Classification2
						and (WorkDate >= @begin and WorkDate <= @end) 
						and ((WorkCompleteDate >= @fromDate and WorkCompleteDate <= @toDate) or WorkCompleteDate is null) 
						and ((DeliveryDate >= @from and DeliveryDate <= @to) or DeliveryDate is null)
						Order by  WDWP_HT.DeliveryDate asc, WorkPlanHistoryIndex desc, WDWP_HT.WorkDate asc,  WDWP_HT.ProcessSequenceNum asc";
					break;
				case "ExecutionPlanInfo":
					str = @"Select SmallClassificationName as ItemState, EPI_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, PlanDate, PlanQuantity, SaleUnitCost, PlanTotalCost, EPI_MT.RecodingState,  EPI_MT.RegistrationPerson,  EPI_MT.RegistrationPersonID,  EPI_MT.RegistrationDate,  EPI_MT.UpdatingPerson,  EPI_MT.UpdatingPersonID,  EPI_MT.UpdatingDate,  EPI_MT.ExecutionPlanInfoIndex  
							From EPI_MT join II_MT on II_MT.ItemNum = EPI_MT.ItemNum 
							left outer join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
							Where II_MT.RecodingState = 1 and EPI_MT.RecodingState = 1 and EPI_MT.State = 0 and EPI_MT.ItemNum Like @num and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name and (PlanDate >= @begin and PlanDate <= @end)";
					break;
				case "ReceiveingPC"://수주현황
				{
					if(m_ProgressState == "미납")
					{
						str = @"select isnull(PUC_MT.SmallClassificationName,'') as ItemState, RO_HT.ItemNum, RO_HT.ItemDrawNum, RO_HT.ItemName, CompanyName, BusinessRegistrationNum, RO_HT.PropertyClassification, 
						case ProductionRequestDivision when 1 then '예' else '아니오' end as ProductionRequestDivision, 
						ReceivingOrderDate, ApplyUnitCost, DeliveryRequestQuantity1, DeliveryRequestDate1, 
						DeliveryRequestQuantity2, DeliveryRequestDate2, DeliveryRequestQuantity3, DeliveryRequestDate3, 
						DeliveryRequestQuantity4, DeliveryRequestDate4, DeliveryRequestQuantity5, DeliveryRequestDate5, 
						TotalReceiveingOrderQuantity, TotalCost, OutStorehouseQuantity, SuitabilityQuantity, 
						UnInspectionQuantity, RemainderQuantity, OrderNum, DeliveryPlace, VolumNum, 
						case ProgressCondition when '대기' then '미납' when '진행' then '미납' when '중단' then '중단' else '완료' end as Progress,	ProgressCondition,
						RO_HT.RegistrationPerson, RO_HT.RegistrationPersonID, RO_HT.RegistrationDate, 
						RO_HT.UpdatingPerson, RO_HT.UpdatingPersonID, RO_HT.UpdatingDate, ReceivingOrderHistoryIndex, P1.SmallClassificationName as ItemClassification1,  P2.SmallClassificationName as ItemClassification2, P3.SmallClassificationName as ItemClassification3, P4.SmallClassificationName as ItemClassification4
						From RO_HT inner join II_MT on RO_HT.ItemNum = II_MT.ItemNum
						left outer join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
						left outer join PUC_MT P1 on II_MT.ItemClassification1 = P1.SmallClassificationCode
						left outer join PUC_MT P2 on II_MT.ItemClassification2 = P2.SmallClassificationCode
						left outer join PUC_MT P3 on II_MT.ItemClassification3 = P3.SmallClassificationCode
						left outer join PUC_MT P4 on II_MT.ItemClassification4 = P4.SmallClassificationCode
						Where II_MT.RecodingState = 1 and RO_HT.ItemNum Like @num and RO_HT.ItemDrawNum Like @draw and RO_HT.ItemName Like @name and CompanyName Like @company and (ProgressCondition = '대기' or ProgressCondition = '진행') and (DeliveryRequestDate1 >= @begin and DeliveryRequestDate1 <= @end) and ItemClassification1 Like @Classification1 order by ReceivingOrderHistoryIndex desc";
					}
					else if(m_ProgressState == "-선 택-")
					{
						str = @"select isnull(PUC_MT.SmallClassificationName,'') as ItemState, RO_HT.ItemNum, RO_HT.ItemDrawNum, RO_HT.ItemName, CompanyName, BusinessRegistrationNum, RO_HT.PropertyClassification, 
						case ProductionRequestDivision when 1 then '예' else '아니오' end as ProductionRequestDivision, 
						ReceivingOrderDate, ApplyUnitCost, DeliveryRequestQuantity1, DeliveryRequestDate1, 
						DeliveryRequestQuantity2, DeliveryRequestDate2, DeliveryRequestQuantity3, DeliveryRequestDate3, 
						DeliveryRequestQuantity4, DeliveryRequestDate4, DeliveryRequestQuantity5, DeliveryRequestDate5, 
						TotalReceiveingOrderQuantity, TotalCost, OutStorehouseQuantity, SuitabilityQuantity, 
						UnInspectionQuantity, RemainderQuantity, OrderNum, DeliveryPlace, VolumNum, 
						case ProgressCondition when '대기' then '미납' when '진행' then '미납' when '중단' then '중단' else '완료' end as Progress,	ProgressCondition,
						RO_HT.RegistrationPerson, RO_HT.RegistrationPersonID, RO_HT.RegistrationDate, 
						RO_HT.UpdatingPerson, RO_HT.UpdatingPersonID, RO_HT.UpdatingDate, ReceivingOrderHistoryIndex, P1.SmallClassificationName as ItemClassification1,  P2.SmallClassificationName as ItemClassification2, P3.SmallClassificationName as ItemClassification3, P4.SmallClassificationName as ItemClassification4
						From RO_HT inner join II_MT on RO_HT.ItemNum = II_MT.ItemNum
						left outer join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
						left outer join PUC_MT P1 on II_MT.ItemClassification1 = P1.SmallClassificationCode
						left outer join PUC_MT P2 on II_MT.ItemClassification2 = P2.SmallClassificationCode
						left outer join PUC_MT P3 on II_MT.ItemClassification3 = P3.SmallClassificationCode
						left outer join PUC_MT P4 on II_MT.ItemClassification4 = P4.SmallClassificationCode
						Where II_MT.RecodingState = 1 and RO_HT.ItemNum Like @num and RO_HT.ItemDrawNum Like @draw and RO_HT.ItemName Like @name and CompanyName Like @company and (DeliveryRequestDate1 >= @begin and DeliveryRequestDate1 <= @end)  order by ReceivingOrderHistoryIndex desc";

					}
					else
					{
						str = @"select isnull(PUC_MT.SmallClassificationName,'') as ItemState, RO_HT.ItemNum, RO_HT.ItemDrawNum, RO_HT.ItemName, CompanyName, BusinessRegistrationNum, RO_HT.PropertyClassification, 
						case ProductionRequestDivision when 1 then '예' else '아니오' end as ProductionRequestDivision, 
						ReceivingOrderDate, ApplyUnitCost, DeliveryRequestQuantity1, DeliveryRequestDate1, 
						DeliveryRequestQuantity2, DeliveryRequestDate2, DeliveryRequestQuantity3, DeliveryRequestDate3, 
						DeliveryRequestQuantity4, DeliveryRequestDate4, DeliveryRequestQuantity5, DeliveryRequestDate5, 
						TotalReceiveingOrderQuantity, TotalCost, OutStorehouseQuantity, SuitabilityQuantity, 
						UnInspectionQuantity, RemainderQuantity, OrderNum, DeliveryPlace, VolumNum, 
						case ProgressCondition when '대기' then '미납' when '진행' then '미납' when '중단' then '중단' else '완료' end as Progress,	ProgressCondition,
						RO_HT.RegistrationPerson, RO_HT.RegistrationPersonID, RO_HT.RegistrationDate, 
						RO_HT.UpdatingPerson, RO_HT.UpdatingPersonID, RO_HT.UpdatingDate, ReceivingOrderHistoryIndex, P1.SmallClassificationName as ItemClassification1,  P2.SmallClassificationName as ItemClassification2, P3.SmallClassificationName as ItemClassification3, P4.SmallClassificationName as ItemClassification4
						From RO_HT inner join II_MT on RO_HT.ItemNum = II_MT.ItemNum
						left outer join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
						left outer join PUC_MT P1 on II_MT.ItemClassification1 = P1.SmallClassificationCode
						left outer join PUC_MT P2 on II_MT.ItemClassification2 = P2.SmallClassificationCode
						left outer join PUC_MT P3 on II_MT.ItemClassification3 = P3.SmallClassificationCode
						left outer join PUC_MT P4 on II_MT.ItemClassification4 = P4.SmallClassificationCode
						Where  II_MT.RecodingState = 1 and RO_HT.ItemNum Like @num and RO_HT.ItemDrawNum Like @draw and RO_HT.ItemName Like @name and CompanyName Like @company and ProgressCondition Like @progress and (DeliveryRequestDate1 >= @begin and DeliveryRequestDate1 <= @end) and ItemClassification1 Like @Classification1  order by ReceivingOrderHistoryIndex desc";

					}
					break;
				}
				case "ProductionRequest"://생산의뢰
					if(m_Comnum.Trim() == "")
					{
						str = @"select SmallClassificationName as ItemState, RO_HT.ItemNum, RO_HT.ItemDrawNum, RO_HT.ItemName, CompanyName, BusinessRegistrationNum, RO_HT.PropertyClassification, 
						case ProductionRequestDivision when 1 then '예' else '아니오' end as ProductionRequestDivision, 
						ReceivingOrderDate, ApplyUnitCost, DeliveryRequestQuantity1, DeliveryRequestDate1, 
						DeliveryRequestQuantity2, DeliveryRequestDate2, DeliveryRequestQuantity3, DeliveryRequestDate3, 
						DeliveryRequestQuantity4, DeliveryRequestDate4, DeliveryRequestQuantity5, DeliveryRequestDate5, 
						TotalReceiveingOrderQuantity, TotalCost, OutStorehouseQuantity, SuitabilityQuantity, 
						UnInspectionQuantity, RemainderQuantity, OrderNum, DeliveryPlace, VolumNum, 
						case ProgressCondition when '대기' then '미납' when '진행' then '미납' when '중단' then '중단' else '완료' end as ProgressCondition,
						RO_HT.RegistrationPerson, RO_HT.RegistrationPersonID, RO_HT.RegistrationDate, 
						RO_HT.UpdatingPerson, RO_HT.UpdatingPersonID, RO_HT.UpdatingDate, ReceivingOrderHistoryIndex
						From RO_HT inner join II_MT on RO_HT.ItemNum = II_MT.ItemNum 
						left outer join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
						Where II_MT.RecodingState = 1 and (RO_HT.PropertyClassification = '제품' or RO_HT.PropertyClassification = '팬텀') and ProductionRequestDivision = 1 and RO_HT.ItemNum Like @num and RO_HT.ItemDrawNum Like @draw and RO_HT.ItemName Like @name and CompanyName Like @company and ItemClassification1 like @Classification1 and ProgressCondition = '대기' and (DeliveryRequestDate1 >= @begin and DeliveryRequestDate1 <= @end)";
					}
					else
					{
						str = @"select isnull(SmallClassificationName,'') as ItemState, RO_HT.ItemNum, RO_HT.ItemDrawNum, RO_HT.ItemName, CompanyName, BusinessRegistrationNum, RO_HT.PropertyClassification, 
						case ProductionRequestDivision when 1 then '예' else '아니오' end as ProductionRequestDivision, 
						ReceivingOrderDate, ApplyUnitCost, DeliveryRequestQuantity1, DeliveryRequestDate1, 
						DeliveryRequestQuantity2, DeliveryRequestDate2, DeliveryRequestQuantity3, DeliveryRequestDate3, 
						DeliveryRequestQuantity4, DeliveryRequestDate4, DeliveryRequestQuantity5, DeliveryRequestDate5, 
						TotalReceiveingOrderQuantity, TotalCost, OutStorehouseQuantity, SuitabilityQuantity, 
						UnInspectionQuantity, RemainderQuantity, OrderNum, DeliveryPlace, VolumNum, 
						case ProgressCondition when '대기' then '미납' when '진행' then '미납' when '중단' then '중단' else '완료' end as ProgressCondition,
						RO_HT.RegistrationPerson, RO_HT.RegistrationPersonID, RO_HT.RegistrationDate, 
						RO_HT.UpdatingPerson, RO_HT.UpdatingPersonID, RO_HT.UpdatingDate, ReceivingOrderHistoryIndex
						From RO_HT inner join II_MT on RO_HT.ItemNum = II_MT.ItemNum 
						left outer join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
						Where II_MT.RecodingState = 1 and (RO_HT.PropertyClassification = '제품' or RO_HT.PropertyClassification = '팬텀') and ProductionRequestDivision = 1 and RO_HT.ItemNum Like @num and RO_HT.ItemDrawNum Like @draw and RO_HT.ItemName Like @name and CompanyName Like @company and ItemClassification1 like @Classification1 and BusinessRegistrationNum = @comnum and ProgressCondition = '대기' and (DeliveryRequestDate1 >= @begin and DeliveryRequestDate1 <= @end) ";
					}
					break;
				case "ProductionRequestPC"://생산의뢰현황
					if(m_Comnum.Trim() == "")
					{
						str = @"select isnull(SmallClassificationName,'') as ItemState, PR_HT.ItemNum, PR_HT.ItemDrawNum, PR_HT.ItemName, PR_HT.PropertyClassification, ProductionRequestSourceCode, 
						ProductionRequestSource, CompanyName, BusinessRegistrationNum, ProductionRequestQuantity, 
						RequestQuantity1, RequestDate1, RequestQuantity2, RequestDate2, RequestQuantity3, RequestDate3, 
						RequestQuantity4, RequestDate4, RequestQuantity5, RequestDate5, ApplyUnitCost, VolumNum, ProgressCondition, 
						PR_HT.RegistrationPerson, PR_HT.RegistrationPersonID, PR_HT.RegistrationDate, PR_HT.UpdatingPerson, PR_HT.UpdatingPersonID, PR_HT.UpdatingDate, 
						ReceivingOrderHistoryIndex, ProductionRequestHistoryIndex
						From PR_HT inner join II_MT on PR_HT.ItemNum = II_MT.ItemNum 
						left outer join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
						Where II_MT.RecodingState = 1 and PR_HT.ItemNum Like @num and PR_HT.ItemDrawNum Like @draw and PR_HT.ItemName Like @name and CompanyName Like @company and ProgressCondition Like @progress and ProductionRequestSourceCode Like @source and (PR_HT.RequestDate1 >= @begin and PR_HT.RequestDate1 <= @end) order by ProductionRequestHistoryIndex desc";
					}
					else
					{
						str = @"select isnull(SmallClassificationName,'') as ItemState, PR_HT.ItemNum, PR_HT.ItemDrawNum, PR_HT.ItemName, PR_HT.PropertyClassification, ProductionRequestSourceCode, 
						ProductionRequestSource, CompanyName, BusinessRegistrationNum, ProductionRequestQuantity, 
						RequestQuantity1, RequestDate1, RequestQuantity2, RequestDate2, RequestQuantity3, RequestDate3, 
						RequestQuantity4, RequestDate4, RequestQuantity5, RequestDate5, ApplyUnitCost, VolumNum, ProgressCondition, 
						PR_HT.RegistrationPerson, PR_HT.RegistrationPersonID, PR_HT.RegistrationDate, PR_HT.UpdatingPerson, PR_HT.UpdatingPersonID, PR_HT.UpdatingDate, 
						ReceivingOrderHistoryIndex, ProductionRequestHistoryIndex
						From PR_HT inner join II_MT on PR_HT.ItemNum = II_MT.ItemNum 
						left outer join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
						Where II_MT.RecodingState = 1 and PR_HT.ItemNum Like @num and PR_HT.ItemDrawNum Like @draw and PR_HT.ItemName Like @name and CompanyName Like @company and BusinessRegistrationNum = @comnum and  ProgressCondition Like @progress and ProductionRequestSourceCode Like @source and (PR_HT.RequestDate1 >= @begin and PR_HT.RequestDate1 <= @end) order by ProductionRequestHistoryIndex desc";
					}
					break;
				case "GoodsBuyingRequest"://상품구매의뢰
					str = @"select ItemNum, ItemDrawNum, ItemName, CompanyName, BusinessRegistrationNum, PropertyClassification, 
						case ProductionRequestDivision when 1 then '예' else '아니오' end as ProductionRequestDivision, 
						ReceivingOrderDate, ApplyUnitCost, DeliveryRequestQuantity1, DeliveryRequestDate1, 
						DeliveryRequestQuantity2, DeliveryRequestDate2, DeliveryRequestQuantity3, DeliveryRequestDate3, 
						DeliveryRequestQuantity4, DeliveryRequestDate4, DeliveryRequestQuantity5, DeliveryRequestDate5, 
						TotalReceiveingOrderQuantity, TotalCost, OutStorehouseQuantity, SuitabilityQuantity, 
						UnInspectionQuantity, RemainderQuantity, OrderNum, DeliveryPlace, VolumNum, 
						ProgressCondition, RegistrationPerson, RegistrationPersonID, RegistrationDate, 
						UpdatingPerson, UpdatingPersonID, UpdatingDate, ReceivingOrderHistoryIndex
						From RO_HT Where PropertyClassification = '상품' and ItemNum Like @num and ItemDrawNum Like @draw and ItemName Like @name and CompanyName Like @company and ProgressCondition = '대기' and (ReceivingOrderDate >= @begin and ReceivingOrderDate <= @end) ";
					break;
				case "GoodsBuyingRequestPC"://상품구매의뢰현황
					str = @"Select ItemNum, ItemDrawNum, ItemName, PropertyClassification, BuyingRequestSourceCode, BuyingRequestSource, 
						FirstDeliveryDemandQuantity, FirstDeliveryDemandDate, SecondDeliveryDemandQuantity, 
						SecondDeliveryDemandDate, ThirdDeliveryDemandQuantity, ThirdDeliveryDemandDate, 
						FourthDeliveryDemandQuantity, FourthDeliveryDemandDate, FifthDeliveryDemandQuantity, FifthDeliveryDemandDate, 
						OrderQuantity, ApplyUnitCost, TotalCost, VolumNum, RequestPostCode, RequestPost, ProgressCondition, 
						RegistrationPerson, RegistrationPersonID, RegistrationDate, UpdatingPerson, UpdatingPersonID, UpdatingDate, 
						HistoryIndex, HistorySection, BuyingRequestHistoryIndex 
						From BR_HT Where ItemNum Like @num and ItemDrawNum Like @draw and ItemName Like @name and ProgressCondition Like @progress and BuyingRequestSourceCode Like @source and PropertyClassification = '상품' and  (RegistrationDate >= @begin and RegistrationDate <= @end) order by BuyingRequestHistoryIndex desc";
					break;
				case "BusinessStorehouseInStorehouse"://영업창고입고
					str = @"Select ISR_HT.ItemNum, ISR_HT.ItemDrawNum, ISR_HT.ItemName, ProcessSequenceNum, ProcessCode, ProcessName, CompanyName, 
						BusinessRegistrationNum, InstorehouseRequestQuantity, InStoreDate, ProgressCondition, ISR_HT.RegistrationPerson, 
						ISR_HT.RegistrationPersonID, ISR_HT.RegistrationDate, ISR_HT.UpdatingPerson, ISR_HT.UpdatingPersonID, ISR_HT.UpdatingDate, 
						HistoryIndex, HistorySection, InStorehouseRequestHistoryIndex 
						From ISR_HT inner join II_MT on ISR_HT.ItemNum = II_MT.ItemNum Where InstorehouseRequestQuantity !=0 and ProgressCondition = '대기' and II_MT.RecodingState = 1 and ISR_HT.ItemNum Like @num and ISR_HT.ItemDrawNum Like @draw and ISR_HT.ItemName Like @name and (ISR_HT.RegistrationDate >= @begin and ISR_HT.RegistrationDate <= @end) ";//and PropertyClassification ='상품'" ;
					break;
				case "BusinessStorehouseInStorehousePC"://영업창고입고현황
					str = @"Select ItemNum, ItemDrawNum, ItemName, ProcessSequenceNum, ProcessCode, ProcessName, InStorehouseQuantity, 
						case BusinessStorehouseNum when 1 then '영업1창고' when 2 then '영업2창고' else '영업3창고' end as BusinessStorehouseNum,InStoreDate,
						ProgressCondition, RegistrationPerson, RegistrationPersonID, RegistrationDate, UpdatingPerson, UpdatingPersonID, UpdatingDate, 
						InStorehouseRequestHistoryIndex, InStorehouseHistoryIndex From IS_HT 
						Where InStorehouseQuantity !=0 and ItemNum Like @num and ItemDrawNum Like @draw and ItemName Like @name and (InStoreDate >= @begin and InStoreDate <= @end) order by InStorehouseHistoryIndex desc";
					break;
				case "GoodsManufactureOutStorehouse"://상품제품출고
					if(m_StoreNum == 1)
					{
						str = @"select RO_HT.ItemNum, RO_HT.ItemDrawNum, RO_HT.ItemName, CompanyName, BusinessRegistrationNum, RO_HT.PropertyClassification, 
						case ProductionRequestDivision when 1 then '예' else '아니오' end as ProductionRequestDivision, 
						ReceivingOrderDate, RO_HT.ApplyUnitCost, DeliveryRequestQuantity1, DeliveryRequestDate1, 
						DeliveryRequestQuantity2, DeliveryRequestDate2, DeliveryRequestQuantity3, DeliveryRequestDate3, 
						DeliveryRequestQuantity4, DeliveryRequestDate4, DeliveryRequestQuantity5, DeliveryRequestDate5, 
						TotalReceiveingOrderQuantity, TotalCost, OutStorehouseQuantity, SuitabilityQuantity, 
						UnInspectionQuantity, RemainderQuantity, OrderNum, DeliveryPlace, VolumNum, 
						ProgressCondition, RO_HT.RegistrationPerson, RO_HT.RegistrationPersonID, RO_HT.RegistrationDate, 
						RO_HT.UpdatingPerson, RO_HT.UpdatingPersonID, RO_HT.UpdatingDate, ReceivingOrderHistoryIndex
						From RO_HT inner join II_MT on RO_HT.ItemNum = II_MT.ItemNum
						Where (RO_HT.PropertyClassification = '원자재' or RO_HT.PropertyClassification = '반제품') and II_MT.RecodingState = 1 and   (ProgressCondition != '완료' and ProgressCondition != '중단') and RO_HT.ItemNum Like @num and RO_HT.ItemDrawNum Like @draw and RO_HT.ItemName Like @name and OrderNum Like @ordernum and CompanyName Like @company and (ReceivingOrderDate >= @begin and ReceivingOrderDate <= @end) ";
					}
					else
					{
						str = @"select ItemNum,ItemDrawNum, ItemName, CompanyName, BusinessRegistrationNum, PropertyClassification, 
						case ProductionRequestDivision when 1 then '예' else '아니오' end as ProductionRequestDivision, 
						ReceivingOrderDate, ApplyUnitCost, DeliveryRequestQuantity1, DeliveryRequestDate1, 
						DeliveryRequestQuantity2, DeliveryRequestDate2, DeliveryRequestQuantity3, DeliveryRequestDate3, 
						DeliveryRequestQuantity4, DeliveryRequestDate4, DeliveryRequestQuantity5, DeliveryRequestDate5, 
						TotalReceiveingOrderQuantity, TotalCost, OutStorehouseQuantity, SuitabilityQuantity, 
						UnInspectionQuantity, RemainderQuantity, OrderNum, DeliveryPlace, VolumNum, 
						ProgressCondition, RegistrationPerson, RegistrationPersonID, RegistrationDate, 
						UpdatingPerson, UpdatingPersonID, UpdatingDate, ReceivingOrderHistoryIndex
						From RO_HT 
						Where  (ProgressCondition != '완료' and ProgressCondition != '중단') and ItemNum Like @num and ItemDrawNum Like @draw and ItemName Like @name and OrderNum Like @ordernum and CompanyName Like @company and (ReceivingOrderDate >= @begin and ReceivingOrderDate <= @end) ";
					}
					break;
				case "GoodsManufactureOutStorehousePC"://상품제품출고현황
					str = @"Select ItemNum, ItemDrawNum, ItemName, CompanyName, BusinessRegistrationNum, OutStoreDate, OutStorehouseQuantity, 
						UnInspectionQuantity, SuitabilityQuantity, UnSuitabilityQuantity, UnSuitabilityCauseCode, OrderNum,
						UnSuitabilityCauseMeaning, UnSuitabilityStatusCode, UnSuitabilityStatusMeaning, UnSuitabilityDetailMeaning, 
						UnSuitabilityCost, ApplyUnitCost, InspectionDecisionCode, InspectionDecisionMeaning, BusinessStorehouseNum,
						ItemizeAccountNum, ProgressCondition, RegistrationPerson, RegistrationPersonID, RegistrationDate, UpdatingPerson,
						UpdatingPersonID, UpdatingDate, ReceivingOrderHistoryIndex, OutStorehouseHistoryIndex
						From OS_HT Where ItemNum Like @num and ItemDrawNum Like @draw and ItemName Like @name and CompanyName Like @company and ProgressCondition like @progress and (OutStoreDate >= @begin and OutStoreDate <= @end) order by OutStorehouseHistoryIndex desc";
					
					break;
				case "SaleHistoryRegistration"://매출원장등록
					str = @"Select II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, II_MT.SupplementaryValueTaxRate,OS_HT.CompanyName, OS_HT.BusinessRegistrationNum, OS_HT.OutStorehouseQuantity, 
						OS_HT.UnInspectionQuantity, OS_HT.SuitabilityQuantity, OS_HT.UnSuitabilityQuantity, OS_HT.UnSuitabilityCauseCode, OS_HT.OrderNum, OS_HT.OutStoreDate,
						OS_HT.UnSuitabilityCauseMeaning, OS_HT.UnSuitabilityStatusCode, OS_HT.UnSuitabilityStatusMeaning, OS_HT.UnSuitabilityDetailMeaning, 
						OS_HT.UnSuitabilityCost, OS_HT.ApplyUnitCost, OS_HT.InspectionDecisionCode, OS_HT.InspectionDecisionMeaning,
						case OS_HT.BusinessStorehouseNum when 1 then '영업1창고' when 2 then '영업2창고' else '영업3창고' end as BusinessStorehouseNum,
						OS_HT.ItemizeAccountNum, OS_HT.ProgressCondition, OS_HT.RegistrationPerson, OS_HT.RegistrationPersonID, OS_HT.RegistrationDate, OS_HT.UpdatingPerson,
						OS_HT.UpdatingPersonID, OS_HT.UpdatingDate, OS_HT.ReceivingOrderHistoryIndex, OS_HT.OutStorehouseHistoryIndex
						From OS_HT
						inner join II_MT on OS_HT.ItemNum = II_MT.ItemNum  Where II_MT.RecodingState = 1 and (ProgressCondition != '완료' and ProgressCondition != '중단') and II_MT.ItemNum Like @num and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name and OS_HT.CompanyName Like @company and (OS_HT.OutStoreDate >= @begin and OS_HT.OutStoreDate <= @end) ";
					break;
				case "SaleHistoryRegistrationPC"://매출원장현황
					str = @"Select ItemNum, ItemDrawNum, ItemName, CompanyName, BusinessRegistrationNum, OutStorehouseQuantity, OrderNum,
						SuitabilityQuantity, ApplyUnitCost, TotalCost, SupplementaryValueTaxRate, BillNum, SupplementaryValueTaxFloatationDate,SaleDate,
						RegistrationPerson, RegistrationPersonID, RegistrationDate,	UpdatingPerson, UpdatingPersonID, UpdatingDate, 
						OutStorehouseHistoryIndex, ReceivingOrderHistoryIndex, SaleHistoryIndex 
						From S_HT Where ItemNum Like @num and ItemDrawNum Like @draw and ItemName Like @name and CompanyName Like @company and (SaleDate >= @begin and SaleDate <= @end) order by SaleHistoryIndex desc";
					break;
				case "CollectMoneyRegistrationPC"://수금현황
					str = @"Select CompanyName, BusinessRegistrationNum, CollectMoneyDate, ItemPaymentCost, SupplementaryValueTaxPaymentCost, 
						DecisionMethodCode, DecisionMethod, BillNum1, BillPaymentDate1, BankCode1, BankName1, BillNum2, BillPaymentDate2, 
						BankCode2, BankName2, BillNum3, BillPaymentDate3, BankCode3, BankName3, RegistrationPerson, RegistrationPersonID, 
						RegistrationDate, UpdatingPerson, UpdatingPersonID, UpdatingDate, CollectMoneyHistoryIndex
						From CM_HT Where CompanyName Like @company and DecisionMethodCode Like @decisionMethod and (CollectMoneyDate >= @begin and CollectMoneyDate <= @end) order by CollectMoneyHistoryIndex desc";
					break;
				case "UnCollectMoneyLook"://미수금보기
//					str = @"SELECT   CI_MT.CompanyName, CI_MT.BusinessRegistrationNum,
//						case BSI_MT.SaleDistinction when '1' then '예' else '아니오' end as SaleDistinction,
//						case BSI_MT.BuyingDistinction when '1' then '예' else '아니오' end as BuyingDistinction,
//						LastYearSaleTransferCost , LastYearBuyingTransferCost,
//						BSI_MT.TotalSaleCost1, BSI_MT.CollectMoney1, BSI_MT.UncollectMoney1, BSI_MT.TotalBuyingCost1, BSI_MT.PaymentMoney1, BSI_MT.UnPaymentMoney1 ,
//						BSI_MT.TotalSaleCost2, BSI_MT.CollectMoney2, BSI_MT.UncollectMoney2, BSI_MT.TotalBuyingCost2, BSI_MT.PaymentMoney2, BSI_MT.UnPaymentMoney2 ,
//						BSI_MT.TotalSaleCost3, BSI_MT.CollectMoney3, BSI_MT.UncollectMoney3, BSI_MT.TotalBuyingCost3, BSI_MT.PaymentMoney3, BSI_MT.UnPaymentMoney3 ,
//						BSI_MT.TotalSaleCost4, BSI_MT.CollectMoney4, BSI_MT.UncollectMoney4, BSI_MT.TotalBuyingCost4, BSI_MT.PaymentMoney4, BSI_MT.UnPaymentMoney4 ,
//						BSI_MT.TotalSaleCost5, BSI_MT.CollectMoney5, BSI_MT.UncollectMoney5, BSI_MT.TotalBuyingCost5, BSI_MT.PaymentMoney5, BSI_MT.UnPaymentMoney5 ,
//						BSI_MT.TotalSaleCost6, BSI_MT.CollectMoney6, BSI_MT.UncollectMoney6, BSI_MT.TotalBuyingCost6, BSI_MT.PaymentMoney6, BSI_MT.UnPaymentMoney6 ,
//						BSI_MT.TotalSaleCost7, BSI_MT.CollectMoney7, BSI_MT.UncollectMoney7, BSI_MT.TotalBuyingCost7, BSI_MT.PaymentMoney7, BSI_MT.UnPaymentMoney7 ,
//						BSI_MT.TotalSaleCost8, BSI_MT.CollectMoney8, BSI_MT.UncollectMoney8, BSI_MT.TotalBuyingCost8, BSI_MT.PaymentMoney8, BSI_MT.UnPaymentMoney8 ,
//						BSI_MT.TotalSaleCost9, BSI_MT.CollectMoney9, BSI_MT.UncollectMoney9, BSI_MT.TotalBuyingCost9, BSI_MT.PaymentMoney9, BSI_MT.UnPaymentMoney9 ,
//						BSI_MT.TotalSaleCost10, BSI_MT.CollectMoney10, BSI_MT.UncollectMoney10, BSI_MT.TotalBuyingCost10, BSI_MT.PaymentMoney10, BSI_MT.UnPaymentMoney10 ,
//						BSI_MT.TotalSaleCost11, BSI_MT.CollectMoney11, BSI_MT.UncollectMoney11, BSI_MT.TotalBuyingCost11, BSI_MT.PaymentMoney11, BSI_MT.UnPaymentMoney11 ,
//						BSI_MT.TotalSaleCost12, BSI_MT.CollectMoney12, BSI_MT.UncollectMoney12, BSI_MT.TotalBuyingCost12, BSI_MT.PaymentMoney12, BSI_MT.UnPaymentMoney12 
//						FROM BSI_MT join CI_MT ON 
//						BSI_MT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum 
//						WHERE [Year] = @year and  CI_MT.RecodingState = 1 and BSI_MT.RecodingState = 1 and CI_MT.CompanyName Like @company and CI_MT.BusinessRegistrationNum Like @comnum order by CI_MT.CompanyName ";

                    str = @"SELECT   CI_MT.CompanyName, CI_MT.BusinessRegistrationNum,
						case BSI_MT.SaleDistinction when '1' then '예' else '아니오' end as SaleDistinction,
						case BSI_MT.BuyingDistinction when '1' then '예' else '아니오' end as BuyingDistinction, BSI_MT.TotalSaleCost12, 
						BSI_MT.CollectMoney12, BSI_MT.UncollectMoney12, BSI_MT.TotalBuyingCost12, 
						BSI_MT.PaymentMoney12, BSI_MT.UnPaymentMoney12 
						FROM BSI_MT join CI_MT ON 
						BSI_MT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum 
						WHERE [Year] = @year and  CI_MT.RecodingState = 1 and BSI_MT.RecodingState = 1 and CI_MT.CompanyName Like @company and CI_MT.BusinessRegistrationNum Like @comnum order by CI_MT.CompanyName ";
					break;
				case "StorehouseMoving"://창고이동
					str = @"SELECT BS_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, BS_MT.ProcessSequenceNum, BS_MT.BusinessStorehouseIndex AS StorehouseIndex,
						BS_MT.StockQuantity12, BS_MT.StockCost12 
						FROM BS_MT INNER JOIN II_MT ON BS_MT.ItemNum = II_MT.ItemNum
						Where BS_MT.ItemNum Like @num and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name  and BusinessStorehouseNum = @storenum and II_MT.RecodingState = 1 and BS_MT.RecodingState = 1";
					break;
				case "StorehouseMovingPC"://창고이동현황
					str = "Select * From SM_HT "+
						"Where ItemNum Like @num and ItemDrawNum Like @draw and ItemName Like @name and (RegistrationDate >= @begin and RegistrationDate <= @end)";
					break;
				case "ItemsStockIndex"://품목별재고현황
					str = "Select * From ItemStore Where ItemNum Like @num and ItemDrawNum Like @draw and ItemName Like @name and RecodingState = 1";
					break;


				case "StoreHouseStockIndex"://창고별재고현황
				{
					if(m_hdItemNum.Trim() == "")
					{
						if(m_Store.ToString() == "BS_MT")
						{
							str = @" SELECT BS_MT.ItemNum, PUC_MT.SmallClassificationName AS ProcessName, 
							 II_MT.ItemDrawNum, II_MT.ItemName, BS_MT.ProcessSequenceNum,
							 BS_MT.ProcessCode, BS_MT.BusinessStorehouseIndex AS StorehouseIndex,LastYearTransferQuantity, 0 as StorehouseQuantity,
							(InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) as 'InStorehouseQuantity',
							(OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 )  as 'OutStorehouseQuantity', 
							 BS_MT.StockQuantity12,  ((BS_MT.StockQuantity12)*(II_MT.StandardUnitCost) ) as StockCost12  
							 FROM BS_MT INNER JOIN 
							 II_MT ON BS_MT.ItemNum = II_MT.ItemNum INNER JOIN 
							 PUC_MT ON BS_MT.ProcessCode = PUC_MT.SmallClassificationCode
							 where II_MT.ItemNum Like @num and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name and BusinessStorehouseNum = @storenum and BS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and
							 (StockQuantity12 !=0 or (InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) != 0 or  (OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 ) != 0 ) 
							and [Year] = @year order by BS_MT.ItemNum ";
						}
						else if(m_Store.ToString() == "PS_MT")
						{
							str = @"SELECT II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum, 
								PS_MT.ProcessSequenceNum, PS_MT.ProcessCode, 
								PUC_MT.SmallClassificationName AS ProcessName,LastYearTransferQuantity,  0 as StorehouseQuantity,
								(InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) as 'InStorehouseQuantity',
							(OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 )  as 'OutStorehouseQuantity',  
								PS_MT.StockQuantity12,((PS_MT.StockQuantity12 * ProgressRate/100) * (II_MT.StandardUnitCost) ) as StockCost12
								FROM PS_MT INNER JOIN 
								II_MT ON PS_MT.ItemNum = II_MT.ItemNum INNER JOIN 
								PUC_MT ON PS_MT.ProcessCode = PUC_MT.SmallClassificationCode 
								inner join PSI_MT on PS_MT.ItemNum = PSI_MT.ItemNum and PS_MT.ProcessCode = PSI_MT.ProcessCode
								Where II_MT.ItemNum Like @num and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name and PS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and PSI_MT.RecodingState = 1 and
								(StockQuantity12 !=0 or (InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) != 0 or  (OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 ) != 0 ) 
								 and [Year] = @year order by PS_MT.ItemNum, PS_MT.ProcessSequenceNum";
						}
						else if(m_Store.ToString() == "RMS_MT")
						{
							str = @"SELECT Distinct  II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum,
								PUC_MT.SmallClassificationName AS ProcessName, 
								RMS_MT.ProcessSequenceNum, RMS_MT.ProcessCode, 
								RMS_MT.RowMetarialStorehouseIndex AS StorehouseIndex, LastYearTransferQuantity, 0 as StorehouseQuantity,
								(InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) as 'InStorehouseQuantity',
							(OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 )  as 'OutStorehouseQuantity', 
								RMS_MT.StockQuantity12,  isnull(((RMS_MT.StockQuantity12)*(UCI_MT.StandardUnitCost) ),0) as StockCost12 
								FROM II_MT LEFT OUTER JOIN 
								UCI_MT ON II_MT.ItemNum = UCI_MT.ItemNum INNER JOIN
								RMS_MT ON II_MT.ItemNum = RMS_MT.ItemNum INNER JOIN 
								PUC_MT ON RMS_MT.ProcessCode = PUC_MT.SmallClassificationCode
								Where II_MT.ItemNum Like @num and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name  and( UCI_MT.RecodingState = 1 or UCI_MT.RecodingState is null) and RMS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and 
								(StockQuantity12 !=0 or (InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) != 0 or  (OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 ) != 0 ) 
								and ( UCI_MT.OrderRate = (Select isnull(Max(U.OrderRate),0) From UCI_MT U Where U.RecodingState = 1 and U.ItemNum = UCI_MT.ItemNum) or UCI_MT.OrderRate is null ) 
								and [Year] = @year  order by  II_MT.ItemDrawNum,ProcessName,
								RMS_MT.ProcessSequenceNum, RMS_MT.ProcessCode, LastYearTransferQuantity";
						}
						else if(m_Store.ToString() == "OS_MT")
						{
							str = @" SELECT   II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum,  PUC_MT.SmallClassificationName AS ProcessName, 
								OS_MT.ProcessSequenceNum, OS_MT.ProcessCode, OS_MT.StockQuantity12, 
								( (OS_MT.StockQuantity12 * ProgressRate/100) * (II_MT.StandardUnitCost) ) as StockCost12, LastYearTransferQuantity,  0 as StorehouseQuantity,
								(InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) as 'InStorehouseQuantity',
							(OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 )  as 'OutStorehouseQuantity', 
								CI_MT.CompanyName, CI_MT.BusinessRegistrationNum 
								FROM OS_MT
								INNER JOIN II_MT ON  II_MT.ItemNum = OS_MT.ItemNum 
								INNER JOIN PUC_MT ON OS_MT.ProcessCode =  PUC_MT.SmallClassificationCode 
								left outer JOIN PSI_MT on OS_MT.ItemNum = PSI_MT.ItemNum and OS_MT.ProcessCode = PSI_MT.ProcessCode
								INNER JOIN CI_MT ON OS_MT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
								WHERE II_MT.ItemNum Like @num and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name AND OS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and CI_MT.RecodingState = 1 and PSI_MT.RecodingState = 1 and
								(StockQuantity12 !=0 or (InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) != 0 or  (OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 ) != 0 ) 
								 and [Year] = @year and OS_MT.RecodingState = 1 and OS_MT.BusinessRegistrationNum Like @comnum 
								order by OS_MT.ItemNum, OS_MT.ProcessSequenceNum";
						}
						else if(m_Store =="AS_MT")
						{
							str = @"SELECT II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum, 
								'99' as ProcessSequenceNum, '14009999' as ProcessCode, 
								'보용품' AS ProcessName,LastYearTransferQuantity,  0 as StorehouseQuantity,
								(InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) as 'InStorehouseQuantity',
							(OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 )  as 'OutStorehouseQuantity',  
								AS_MT.StockQuantity12,((AS_MT.StockQuantity12) * (II_MT.StandardUnitCost) ) as StockCost12
								FROM AS_MT INNER JOIN 
								II_MT ON AS_MT.ItemNum = II_MT.ItemNum
								Where II_MT.ItemNum Like @num and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name  and AS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and 
							(StockQuantity12 !=0 or (InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) != 0 or  (OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 ) != 0 ) 
							 and [Year] = @year order by AS_MT.ItemNum";

						}
						else
						{
							str = @" SELECT   II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum,
							 PUC_MT.SmallClassificationName AS ProcessName, LastYearTransferQuantity, 0 as StorehouseQuantity,
							(InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) as 'InStorehouseQuantity',
							(OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 )  as 'OutStorehouseQuantity', 
							 DS_MT.ProcessSequenceNum, DS_MT.ProcessCode, DS_MT.StockQuantity12, 
							  ((DS_MT.StockQuantity12)*(II_MT.StandardUnitCost)) as StockCost12, DS_MT.DeliveryStorehouseIndex AS StorehouseIndex 
							 FROM      II_MT INNER JOIN 
							 DS_MT ON II_MT.ItemNum = DS_MT.ItemNum INNER JOIN
							 PUC_MT ON DS_MT.ProcessCode = PUC_MT.SmallClassificationCode 
							Where II_MT.ItemNum Like @num and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name and DS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and 
							(StockQuantity12 !=0 or (InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) != 0 or  (OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 ) != 0 ) 
							and [Year] = @year order by DS_MT.ItemNum";
						}
					}
					else
					{
						if(m_Store.ToString() == "BS_MT")
						{
							str = @" SELECT BS_MT.ItemNum, PUC_MT.SmallClassificationName AS ProcessName, 
							 II_MT.ItemDrawNum, II_MT.ItemName, BS_MT.ProcessSequenceNum,
							 BS_MT.ProcessCode, BS_MT.BusinessStorehouseIndex AS StorehouseIndex,LastYearTransferQuantity, 0 as StorehouseQuantity,
							(InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) as 'InStorehouseQuantity',
							(OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 )  as 'OutStorehouseQuantity', 
							 BS_MT.StockQuantity12,  ((BS_MT.StockQuantity12)*(II_MT.StandardUnitCost) ) as StockCost12  
							 FROM BS_MT INNER JOIN 
							 II_MT ON BS_MT.ItemNum = II_MT.ItemNum INNER JOIN 
							 PUC_MT ON BS_MT.ProcessCode = PUC_MT.SmallClassificationCode
							 where II_MT.ItemNum = @hdnum and BusinessStorehouseNum = @storenum and BS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and 
							(StockQuantity12 !=0 or (InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) != 0 or  (OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 ) != 0 ) 
							 and [Year] = @year order by BS_MT.ItemNum ";
						}
						else if(m_Store.ToString() == "PS_MT")
						{
							str = @"SELECT II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum, 
								PS_MT.ProcessSequenceNum, PS_MT.ProcessCode, 
								PUC_MT.SmallClassificationName AS ProcessName,LastYearTransferQuantity, 0 as StorehouseQuantity,
								(InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) as 'InStorehouseQuantity',
							(OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 )  as 'OutStorehouseQuantity',  
								PS_MT.StockQuantity12,((PS_MT.StockQuantity12 * ProgressRate/100) * (II_MT.StandardUnitCost) ) as StockCost12
								FROM PS_MT INNER JOIN 
								II_MT ON PS_MT.ItemNum = II_MT.ItemNum INNER JOIN 
								PUC_MT ON PS_MT.ProcessCode = PUC_MT.SmallClassificationCode 
								inner join PSI_MT on PS_MT.ItemNum = PSI_MT.ItemNum and PS_MT.ProcessCode = PSI_MT.ProcessCode
								Where II_MT.ItemNum = @hdnum and PS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and PSI_MT.RecodingState = 1 and
							(StockQuantity12 !=0 or (InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) != 0 or  (OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 ) != 0 ) 
							 and [Year] = @year order by PS_MT.ItemNum, PS_MT.ProcessSequenceNum";
						}
						else if(m_Store.ToString() == "RMS_MT")
						{
							str = @"SELECT Distinct  II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum,
								PUC_MT.SmallClassificationName AS ProcessName, 
								RMS_MT.ProcessSequenceNum, RMS_MT.ProcessCode, 
								RMS_MT.RowMetarialStorehouseIndex AS StorehouseIndex, LastYearTransferQuantity, 0 as StorehouseQuantity,
								(InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) as 'InStorehouseQuantity',
							(OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 )  as 'OutStorehouseQuantity', 
								RMS_MT.StockQuantity12,  isnull(((RMS_MT.StockQuantity12)*(UCI_MT.StandardUnitCost) ),0) as StockCost12 
								FROM II_MT LEFT OUTER JOIN 
								UCI_MT ON II_MT.ItemNum = UCI_MT.ItemNum INNER JOIN
								RMS_MT ON II_MT.ItemNum = RMS_MT.ItemNum INNER JOIN 
								PUC_MT ON RMS_MT.ProcessCode = PUC_MT.SmallClassificationCode
								Where II_MT.ItemNum = @hdnum and( UCI_MT.RecodingState = 1 or UCI_MT.RecodingState is null) and RMS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and 
								(StockQuantity12 !=0 or (InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) != 0 or  (OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 ) != 0 ) 
								and ( UCI_MT.OrderRate = (Select isnull(Max(U.OrderRate),0) From UCI_MT U Where U.RecodingState = 1 and U.ItemNum = UCI_MT.ItemNum) or UCI_MT.OrderRate is null ) 
								and [Year] = @year  order by  II_MT.ItemDrawNum,ProcessName,
								RMS_MT.ProcessSequenceNum, RMS_MT.ProcessCode, LastYearTransferQuantity";
						}
						else if(m_Store.ToString() == "OS_MT")
						{
							str = @" SELECT   II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum,  PUC_MT.SmallClassificationName AS ProcessName, 
								OS_MT.ProcessSequenceNum, OS_MT.ProcessCode, OS_MT.StockQuantity12, 
								( (OS_MT.StockQuantity12 * ProgressRate/100) * (II_MT.StandardUnitCost) ) as StockCost12, LastYearTransferQuantity, 0 as StorehouseQuantity,
								(InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) as 'InStorehouseQuantity',
							(OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 )  as 'OutStorehouseQuantity', 
								CI_MT.CompanyName, CI_MT.BusinessRegistrationNum 
								FROM OS_MT
								INNER JOIN II_MT ON  II_MT.ItemNum = OS_MT.ItemNum 
								INNER JOIN PUC_MT ON OS_MT.ProcessCode =  PUC_MT.SmallClassificationCode 
								left outer JOIN PSI_MT on OS_MT.ItemNum = PSI_MT.ItemNum and OS_MT.ProcessCode = PSI_MT.ProcessCode
								INNER JOIN CI_MT ON OS_MT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
								WHERE II_MT.ItemNum = @hdnum AND OS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and CI_MT.RecodingState = 1 and PSI_MT.RecodingState = 1 and
								(StockQuantity12 !=0 or (InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) != 0 or  (OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 ) != 0 ) 
								 and [Year] = @year and OS_MT.RecodingState = 1 and OS_MT.BusinessRegistrationNum Like @comnum 
								order by OS_MT.ItemNum, OS_MT.ProcessSequenceNum";
						}
						else if(m_Store =="AS_MT")
						{
							str = @"SELECT II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum, 
								'99' as ProcessSequenceNum, '14009999' as ProcessCode, 
								'보용품' AS ProcessName,LastYearTransferQuantity, 0 as StorehouseQuantity,
								(InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) as 'InStorehouseQuantity',
							(OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 )  as 'OutStorehouseQuantity',  
								AS_MT.StockQuantity12,((AS_MT.StockQuantity12) * (II_MT.StandardUnitCost) ) as StockCost12
								FROM AS_MT INNER JOIN 
								II_MT ON AS_MT.ItemNum = II_MT.ItemNum
								Where II_MT.ItemNum = @hdnum and AS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and 
							(StockQuantity12 !=0 or (InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) != 0 or  (OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 ) != 0 ) 
							 and [Year] = @year order by AS_MT.ItemNum";

						}
						else 
						{
							str = @" SELECT   II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum,
							 PUC_MT.SmallClassificationName AS ProcessName, LastYearTransferQuantity, 0 as StorehouseQuantity,
							(InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) as 'InStorehouseQuantity',
							(OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 )  as 'OutStorehouseQuantity', 
							 DS_MT.ProcessSequenceNum, DS_MT.ProcessCode, DS_MT.StockQuantity12, 
							  ((DS_MT.StockQuantity12)*(II_MT.StandardUnitCost)) as StockCost12, DS_MT.DeliveryStorehouseIndex AS StorehouseIndex 
							 FROM      II_MT INNER JOIN 
							 DS_MT ON II_MT.ItemNum = DS_MT.ItemNum INNER JOIN
							 PUC_MT ON DS_MT.ProcessCode = PUC_MT.SmallClassificationCode 
							WHERE II_MT.ItemNum = @hdnum and DS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and 
							(StockQuantity12 !=0 or (InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) != 0 or  (OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 ) != 0 ) 
							and [Year] = @year order by DS_MT.ItemNum";
						}
					}//WHERE II_MT.ItemNum = @hdnum
				}
					break;
				case "ExcelStoreHouseStockIndex"://창고별재고현황 Excel다운로드
				{
					if(m_hdItemNum.Trim() == "")
					{
						if(m_Store.ToString() == "BS_MT")
						{
							str = @" SELECT BS_MT.ItemNum, PUC_MT.SmallClassificationName AS ProcessName, PUC1.SmallClassificationName as ItemClassification1, 
							PUC2.SmallClassificationName as ItemClassification2,
							PUC3.SmallClassificationName as ItemClassification3,
							PUC4.SmallClassificationName as ItemClassification4,
							 II_MT.ItemDrawNum, II_MT.ItemName, BS_MT.ProcessSequenceNum,
							 BS_MT.ProcessCode, BS_MT.BusinessStorehouseIndex AS StorehouseIndex,LastYearTransferQuantity,
							(InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) as 'InStorehouseQuantity',
							(OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 )  as 'OutStorehouseQuantity', 
							 BS_MT.StockQuantity12,  ((BS_MT.StockQuantity12)*(II_MT.StandardUnitCost) ) as StockCost12  
							 FROM BS_MT INNER JOIN 
							 II_MT ON BS_MT.ItemNum = II_MT.ItemNum INNER JOIN 
							 PUC_MT ON BS_MT.ProcessCode = PUC_MT.SmallClassificationCode
							 left outer join PUC_MT PUC1 on II_MT.ItemClassification1 = PUC1.SmallClassificationCode
							left outer join PUC_MT PUC2 on II_MT.ItemClassification2 = PUC2.SmallClassificationCode
							left outer join PUC_MT PUC3 on II_MT.ItemClassification3 = PUC3.SmallClassificationCode
							left outer join PUC_MT PUC4 on II_MT.ItemClassification4 = PUC4.SmallClassificationCode							
							 where II_MT.ItemNum Like @num and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name and BusinessStorehouseNum = 1 and BS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and
							 (StockQuantity12 !=0 or (InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) != 0 or  (OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 ) != 0 ) 
							and [Year] = @year order by BS_MT.ItemNum ";
						}
						else if(m_Store.ToString() == "PS_MT")
						{
							str = @"SELECT II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum, 
							PUC1.SmallClassificationName as ItemClassification1, 
							PUC2.SmallClassificationName as ItemClassification2,
							PUC3.SmallClassificationName as ItemClassification3,
							PUC4.SmallClassificationName as ItemClassification4,
								PS_MT.ProcessSequenceNum, PS_MT.ProcessCode, 
								PUC_MT.SmallClassificationName AS ProcessName,LastYearTransferQuantity,
								(InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) as 'InStorehouseQuantity',
							(OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 )  as 'OutStorehouseQuantity',  
								PS_MT.StockQuantity12,((PS_MT.StockQuantity12 * ProgressRate/100) * (II_MT.StandardUnitCost) ) as StockCost12
								FROM PS_MT INNER JOIN 
								II_MT ON PS_MT.ItemNum = II_MT.ItemNum INNER JOIN 
								PUC_MT ON PS_MT.ProcessCode = PUC_MT.SmallClassificationCode 
								left outer join PUC_MT PUC1 on II_MT.ItemClassification1 = PUC1.SmallClassificationCode
							left outer join PUC_MT PUC2 on II_MT.ItemClassification2 = PUC2.SmallClassificationCode
							left outer join PUC_MT PUC3 on II_MT.ItemClassification3 = PUC3.SmallClassificationCode
							left outer join PUC_MT PUC4 on II_MT.ItemClassification4 = PUC4.SmallClassificationCode		
							inner join PSI_MT on PS_MT.ItemNum = PSI_MT.ItemNum and PS_MT.ProcessCode = PSI_MT.ProcessCode
								Where II_MT.ItemNum Like @num and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name and PS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and 
								(StockQuantity12 !=0 or (InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) != 0 or  (OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 ) != 0 ) 
								 and [Year] = @year order by PS_MT.ItemNum, PS_MT.ProcessSequenceNum";
						}
						else if(m_Store.ToString() == "RMS_MT")
						{
							str = @"SELECT Distinct  II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum,
							PUC1.SmallClassificationName as ItemClassification1, 
							PUC2.SmallClassificationName as ItemClassification2,
							PUC3.SmallClassificationName as ItemClassification3,
							PUC4.SmallClassificationName as ItemClassification4,
								PUC_MT.SmallClassificationName AS ProcessName, 
								RMS_MT.ProcessSequenceNum, RMS_MT.ProcessCode, 
								RMS_MT.RowMetarialStorehouseIndex AS StorehouseIndex, LastYearTransferQuantity,
								(InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) as 'InStorehouseQuantity',
							(OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 )  as 'OutStorehouseQuantity', 
								RMS_MT.StockQuantity12,  isnull(((RMS_MT.StockQuantity12)*(UCI_MT.StandardUnitCost) ),0) as StockCost12 
								FROM II_MT LEFT OUTER JOIN 
								UCI_MT ON II_MT.ItemNum = UCI_MT.ItemNum INNER JOIN
								RMS_MT ON II_MT.ItemNum = RMS_MT.ItemNum INNER JOIN 
								PUC_MT ON RMS_MT.ProcessCode = PUC_MT.SmallClassificationCode
								left outer join PUC_MT PUC1 on II_MT.ItemClassification1 = PUC1.SmallClassificationCode
							left outer join PUC_MT PUC2 on II_MT.ItemClassification2 = PUC2.SmallClassificationCode
							left outer join PUC_MT PUC3 on II_MT.ItemClassification3 = PUC3.SmallClassificationCode
							left outer join PUC_MT PUC4 on II_MT.ItemClassification4 = PUC4.SmallClassificationCode		
								Where II_MT.ItemNum Like @num and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name  and( UCI_MT.RecodingState = 1 or UCI_MT.RecodingState is null) and RMS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and 
								(StockQuantity12 !=0 or (InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) != 0 or  (OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 ) != 0 ) 
								and ( UCI_MT.OrderRate = (Select isnull(Max(U.OrderRate),0) From UCI_MT U Where U.RecodingState = 1 and U.ItemNum = UCI_MT.ItemNum) or UCI_MT.OrderRate is null ) 
								and [Year] = @year  order by  II_MT.ItemDrawNum,ProcessName,
								RMS_MT.ProcessSequenceNum, RMS_MT.ProcessCode, LastYearTransferQuantity";
						}
						else if(m_Store.ToString() == "OS_MT")
						{
							str = @" SELECT   II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum,  PUC_MT.SmallClassificationName AS ProcessName, 
							PUC1.SmallClassificationName as ItemClassification1, 
							PUC2.SmallClassificationName as ItemClassification2,
							PUC3.SmallClassificationName as ItemClassification3,
							PUC4.SmallClassificationName as ItemClassification4,
								OS_MT.ProcessSequenceNum, OS_MT.ProcessCode, OS_MT.StockQuantity12, 
								( (OS_MT.StockQuantity12 * ProgressRate/100) * (II_MT.StandardUnitCost) ) as StockCost12, LastYearTransferQuantity,
								(InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) as 'InStorehouseQuantity',
							(OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 )  as 'OutStorehouseQuantity', 
								CI_MT.CompanyName, CI_MT.BusinessRegistrationNum 
								FROM OS_MT
								INNER JOIN II_MT ON  II_MT.ItemNum = OS_MT.ItemNum 
								INNER JOIN PUC_MT ON OS_MT.ProcessCode =  PUC_MT.SmallClassificationCode 
								left outer JOIN PSI_MT on OS_MT.ItemNum = PSI_MT.ItemNum and OS_MT.ProcessCode = PSI_MT.ProcessCode
								INNER JOIN CI_MT ON OS_MT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
							left outer join PUC_MT PUC1 on II_MT.ItemClassification1 = PUC1.SmallClassificationCode
							left outer join PUC_MT PUC2 on II_MT.ItemClassification2 = PUC2.SmallClassificationCode
							left outer join PUC_MT PUC3 on II_MT.ItemClassification3 = PUC3.SmallClassificationCode
							left outer join PUC_MT PUC4 on II_MT.ItemClassification4 = PUC4.SmallClassificationCode		
								WHERE II_MT.ItemNum Like @num and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name AND OS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and CI_MT.RecodingState = 1 and 
								(StockQuantity12 !=0 or (InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) != 0 or  (OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 ) != 0 ) 
								 and [Year] = @year and OS_MT.RecodingState = 1 and OS_MT.BusinessRegistrationNum Like @comnum 
								order by OS_MT.ItemNum, OS_MT.ProcessSequenceNum";
						}
						else if(m_Store =="AS_MT")
						{
							str = @"SELECT II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum, 
								PUC1.SmallClassificationName as ItemClassification1, 
							PUC2.SmallClassificationName as ItemClassification2,
							PUC3.SmallClassificationName as ItemClassification3,
							PUC4.SmallClassificationName as ItemClassification4,
								'99' as ProcessSequenceNum, '14009999' as ProcessCode, 
								'보용품' AS ProcessName,LastYearTransferQuantity,
								(InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) as 'InStorehouseQuantity',
							(OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 )  as 'OutStorehouseQuantity',  
								AS_MT.StockQuantity12,((AS_MT.StockQuantity12) * (II_MT.StandardUnitCost) ) as StockCost12
								FROM AS_MT INNER JOIN 
								II_MT ON AS_MT.ItemNum = II_MT.ItemNum
								left outer join PUC_MT PUC1 on II_MT.ItemClassification1 = PUC1.SmallClassificationCode
							left outer join PUC_MT PUC2 on II_MT.ItemClassification2 = PUC2.SmallClassificationCode
							left outer join PUC_MT PUC3 on II_MT.ItemClassification3 = PUC3.SmallClassificationCode
							left outer join PUC_MT PUC4 on II_MT.ItemClassification4 = PUC4.SmallClassificationCode	
								Where II_MT.ItemNum Like @num and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name  and AS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and 
							(StockQuantity12 !=0 or (InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) != 0 or  (OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 ) != 0 ) 
							 and [Year] = @year order by AS_MT.ItemNum";

						}
						else
						{
							str = @" SELECT   II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum,
							PUC1.SmallClassificationName as ItemClassification1, 
							PUC2.SmallClassificationName as ItemClassification2,
							PUC3.SmallClassificationName as ItemClassification3,
							PUC4.SmallClassificationName as ItemClassification4,
							 PUC_MT.SmallClassificationName AS ProcessName, LastYearTransferQuantity,
							(InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) as 'InStorehouseQuantity',
							(OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 )  as 'OutStorehouseQuantity', 
							 DS_MT.ProcessSequenceNum, DS_MT.ProcessCode, DS_MT.StockQuantity12, 
							  ((DS_MT.StockQuantity12)*(II_MT.StandardUnitCost)) as StockCost12, DS_MT.DeliveryStorehouseIndex AS StorehouseIndex 
							 FROM      II_MT INNER JOIN 
							 DS_MT ON II_MT.ItemNum = DS_MT.ItemNum INNER JOIN
							 PUC_MT ON DS_MT.ProcessCode = PUC_MT.SmallClassificationCode 
							left outer join PUC_MT PUC1 on II_MT.ItemClassification1 = PUC1.SmallClassificationCode
							left outer join PUC_MT PUC2 on II_MT.ItemClassification2 = PUC2.SmallClassificationCode
							left outer join PUC_MT PUC3 on II_MT.ItemClassification3 = PUC3.SmallClassificationCode
							left outer join PUC_MT PUC4 on II_MT.ItemClassification4 = PUC4.SmallClassificationCode		
							Where II_MT.ItemNum Like @num and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name and DS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and 
							(StockQuantity12 !=0 or (InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) != 0 or  (OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 ) != 0 ) 
							and [Year] = @year order by DS_MT.ItemNum";
						}
					}
					else
					{
						if(m_Store.ToString() == "BS_MT")
						{
							str = @"SELECT BS_MT.ItemNum, PUC_MT.SmallClassificationName AS ProcessName, 
							PUC1.SmallClassificationName as ItemClassification1, 
							PUC2.SmallClassificationName as ItemClassification2,
							PUC3.SmallClassificationName as ItemClassification3,
							PUC4.SmallClassificationName as ItemClassification4,
							 II_MT.ItemDrawNum, II_MT.ItemName, BS_MT.ProcessSequenceNum,
							 BS_MT.ProcessCode, BS_MT.BusinessStorehouseIndex AS StorehouseIndex,LastYearTransferQuantity,
							(InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) as 'InStorehouseQuantity',
							(OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 )  as 'OutStorehouseQuantity', 
							 BS_MT.StockQuantity12,  ((BS_MT.StockQuantity12)*(II_MT.StandardUnitCost) ) as StockCost12  
							 FROM BS_MT INNER JOIN 
							 II_MT ON BS_MT.ItemNum = II_MT.ItemNum INNER JOIN 
							 PUC_MT ON BS_MT.ProcessCode = PUC_MT.SmallClassificationCode
							 left outer join PUC_MT PUC1 on II_MT.ItemClassification1 = PUC1.SmallClassificationCode
							left outer join PUC_MT PUC2 on II_MT.ItemClassification2 = PUC2.SmallClassificationCode
							left outer join PUC_MT PUC3 on II_MT.ItemClassification3 = PUC3.SmallClassificationCode
							left outer join PUC_MT PUC4 on II_MT.ItemClassification4 = PUC4.SmallClassificationCode							
							where II_MT.ItemNum = @hdnum and BusinessStorehouseNum = @storenum and BS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and 
							(StockQuantity12 !=0 or (InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) != 0 or  (OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 ) != 0 ) 
							 and [Year] = @year order by BS_MT.ItemNum ";
						}
						else if(m_Store.ToString() == "PS_MT")
						{
							str = @"SELECT II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum, 
							PUC1.SmallClassificationName as ItemClassification1, 
							PUC2.SmallClassificationName as ItemClassification2,
							PUC3.SmallClassificationName as ItemClassification3,
							PUC4.SmallClassificationName as ItemClassification4,
								PS_MT.ProcessSequenceNum, PS_MT.ProcessCode, 
								PUC_MT.SmallClassificationName AS ProcessName,LastYearTransferQuantity,
								(InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) as 'InStorehouseQuantity',
							(OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 )  as 'OutStorehouseQuantity',  
								PS_MT.StockQuantity12,((PS_MT.StockQuantity12 * ProgressRate/100) * (II_MT.StandardUnitCost) ) as StockCost12
								FROM PS_MT INNER JOIN 
								II_MT ON PS_MT.ItemNum = II_MT.ItemNum INNER JOIN 
								PUC_MT ON PS_MT.ProcessCode = PUC_MT.SmallClassificationCode 
								inner join PSI_MT on PS_MT.ItemNum = PSI_MT.ItemNum and PS_MT.ProcessCode = PSI_MT.ProcessCode
							left outer join PUC_MT PUC1 on II_MT.ItemClassification1 = PUC1.SmallClassificationCode
							left outer join PUC_MT PUC2 on II_MT.ItemClassification2 = PUC2.SmallClassificationCode
							left outer join PUC_MT PUC3 on II_MT.ItemClassification3 = PUC3.SmallClassificationCode
							left outer join PUC_MT PUC4 on II_MT.ItemClassification4 = PUC4.SmallClassificationCode		
								Where II_MT.ItemNum = @hdnum and PS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and 
							(StockQuantity12 !=0 or (InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) != 0 or  (OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 ) != 0 ) 
							 and [Year] = @year order by PS_MT.ItemNum, PS_MT.ProcessSequenceNum";
						}
						else if(m_Store.ToString() == "RMS_MT")
						{
							str = @"SELECT Distinct  II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum,
							PUC1.SmallClassificationName as ItemClassification1, 
							PUC2.SmallClassificationName as ItemClassification2,
							PUC3.SmallClassificationName as ItemClassification3,
							PUC4.SmallClassificationName as ItemClassification4,
								PUC_MT.SmallClassificationName AS ProcessName, 
								RMS_MT.ProcessSequenceNum, RMS_MT.ProcessCode, 
								RMS_MT.RowMetarialStorehouseIndex AS StorehouseIndex, LastYearTransferQuantity,
								(InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) as 'InStorehouseQuantity',
							(OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 )  as 'OutStorehouseQuantity', 
								RMS_MT.StockQuantity12,  isnull(((RMS_MT.StockQuantity12)*(UCI_MT.StandardUnitCost) ),0) as StockCost12 
								FROM II_MT LEFT OUTER JOIN 
								UCI_MT ON II_MT.ItemNum = UCI_MT.ItemNum INNER JOIN
								RMS_MT ON II_MT.ItemNum = RMS_MT.ItemNum INNER JOIN 
								PUC_MT ON RMS_MT.ProcessCode = PUC_MT.SmallClassificationCode
								left outer join PUC_MT PUC1 on II_MT.ItemClassification1 = PUC1.SmallClassificationCode
							left outer join PUC_MT PUC2 on II_MT.ItemClassification2 = PUC2.SmallClassificationCode
							left outer join PUC_MT PUC3 on II_MT.ItemClassification3 = PUC3.SmallClassificationCode
							left outer join PUC_MT PUC4 on II_MT.ItemClassification4 = PUC4.SmallClassificationCode		
								Where II_MT.ItemNum = @hdnum and( UCI_MT.RecodingState = 1 or UCI_MT.RecodingState is null) and RMS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and 
								(StockQuantity12 !=0 or (InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) != 0 or  (OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 ) != 0 ) 
								and ( UCI_MT.OrderRate = (Select isnull(Max(U.OrderRate),0) From UCI_MT U Where U.RecodingState = 1 and U.ItemNum = UCI_MT.ItemNum) or UCI_MT.OrderRate is null ) 
								and [Year] = @year  order by  II_MT.ItemDrawNum,ProcessName,
								RMS_MT.ProcessSequenceNum, RMS_MT.ProcessCode, LastYearTransferQuantity";
						}
						else if(m_Store.ToString() == "OS_MT")
						{
							str = @" SELECT   II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum,  PUC_MT.SmallClassificationName AS ProcessName, 
							PUC1.SmallClassificationName as ItemClassification1, 
							PUC2.SmallClassificationName as ItemClassification2,
							PUC3.SmallClassificationName as ItemClassification3,
							PUC4.SmallClassificationName as ItemClassification4,
								OS_MT.ProcessSequenceNum, OS_MT.ProcessCode, OS_MT.StockQuantity12, 
								( (OS_MT.StockQuantity12 * ProgressRate/100) * (II_MT.StandardUnitCost) ) as StockCost12, LastYearTransferQuantity,
								(InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) as 'InStorehouseQuantity',
							(OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 )  as 'OutStorehouseQuantity', 
								CI_MT.CompanyName, CI_MT.BusinessRegistrationNum 
								FROM OS_MT
								INNER JOIN II_MT ON  II_MT.ItemNum = OS_MT.ItemNum 
								INNER JOIN PUC_MT ON OS_MT.ProcessCode =  PUC_MT.SmallClassificationCode 
								left outer JOIN PSI_MT on OS_MT.ItemNum = PSI_MT.ItemNum and OS_MT.ProcessCode = PSI_MT.ProcessCode
								left outer join PUC_MT PUC1 on II_MT.ItemClassification1 = PUC1.SmallClassificationCode
							left outer join PUC_MT PUC2 on II_MT.ItemClassification2 = PUC2.SmallClassificationCode
							left outer join PUC_MT PUC3 on II_MT.ItemClassification3 = PUC3.SmallClassificationCode
							left outer join PUC_MT PUC4 on II_MT.ItemClassification4 = PUC4.SmallClassificationCode		
								INNER JOIN CI_MT ON OS_MT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
								WHERE II_MT.ItemNum = @hdnum AND OS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and CI_MT.RecodingState = 1 and 
								(StockQuantity12 !=0 or (InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) != 0 or  (OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 ) != 0 ) 
								 and [Year] = @year and OS_MT.RecodingState = 1 and OS_MT.BusinessRegistrationNum Like @comnum 
								order by OS_MT.ItemNum, OS_MT.ProcessSequenceNum";
						}
						else if(m_Store =="AS_MT")
						{
							str = @"SELECT II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum, 
								PUC1.SmallClassificationName as ItemClassification1, 
							PUC2.SmallClassificationName as ItemClassification2,
							PUC3.SmallClassificationName as ItemClassification3,
							PUC4.SmallClassificationName as ItemClassification4,
								'99' as ProcessSequenceNum, '14009999' as ProcessCode, 
								'보용품' AS ProcessName,LastYearTransferQuantity,
								(InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) as 'InStorehouseQuantity',
							(OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 )  as 'OutStorehouseQuantity',  
								AS_MT.StockQuantity12,((AS_MT.StockQuantity12) * (II_MT.StandardUnitCost) ) as StockCost12
								FROM AS_MT INNER JOIN 
								II_MT ON AS_MT.ItemNum = II_MT.ItemNum
								left outer join PUC_MT PUC1 on II_MT.ItemClassification1 = PUC1.SmallClassificationCode
							left outer join PUC_MT PUC2 on II_MT.ItemClassification2 = PUC2.SmallClassificationCode
							left outer join PUC_MT PUC3 on II_MT.ItemClassification3 = PUC3.SmallClassificationCode
							left outer join PUC_MT PUC4 on II_MT.ItemClassification4 = PUC4.SmallClassificationCode	
								WHERE II_MT.ItemNum = @hdnum and AS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and 
							(StockQuantity12 !=0 or (InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) != 0 or  (OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 ) != 0 ) 
							 and [Year] = @year order by AS_MT.ItemNum";

						}
						else
						{
							str = @" SELECT   II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum,
							PUC1.SmallClassificationName as ItemClassification1, 
							PUC2.SmallClassificationName as ItemClassification2,
							PUC3.SmallClassificationName as ItemClassification3,
							PUC4.SmallClassificationName as ItemClassification4,
							 PUC_MT.SmallClassificationName AS ProcessName, LastYearTransferQuantity,
							(InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) as 'InStorehouseQuantity',
							(OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 )  as 'OutStorehouseQuantity', 
							 DS_MT.ProcessSequenceNum, DS_MT.ProcessCode, DS_MT.StockQuantity12, 
							  ((DS_MT.StockQuantity12)*(II_MT.StandardUnitCost)) as StockCost12, DS_MT.DeliveryStorehouseIndex AS StorehouseIndex 
							 FROM      II_MT INNER JOIN 
							 DS_MT ON II_MT.ItemNum = DS_MT.ItemNum INNER JOIN
							 PUC_MT ON DS_MT.ProcessCode = PUC_MT.SmallClassificationCode 
							left outer join PUC_MT PUC1 on II_MT.ItemClassification1 = PUC1.SmallClassificationCode
							left outer join PUC_MT PUC2 on II_MT.ItemClassification2 = PUC2.SmallClassificationCode
							left outer join PUC_MT PUC3 on II_MT.ItemClassification3 = PUC3.SmallClassificationCode
							left outer join PUC_MT PUC4 on II_MT.ItemClassification4 = PUC4.SmallClassificationCode		
							WHERE II_MT.ItemNum = @hdnum and DS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and 
							(StockQuantity12 !=0 or (InStorehouseQuantity1 + InStorehouseQuantity2 + InStorehouseQuantity3 + InStorehouseQuantity4 + InStorehouseQuantity5 + InStorehouseQuantity6 + InStorehouseQuantity7 +  InStorehouseQuantity8 +  InStorehouseQuantity9 +  InStorehouseQuantity10 +  InStorehouseQuantity11 +  InStorehouseQuantity12 ) != 0 or  (OutStorehouseQuantity1 + OutStorehouseQuantity2 + OutStorehouseQuantity3 + OutStorehouseQuantity4 + OutStorehouseQuantity5 + OutStorehouseQuantity6 + OutStorehouseQuantity7 +  OutStorehouseQuantity8 +  OutStorehouseQuantity9 +  OutStorehouseQuantity10 +  OutStorehouseQuantity11 +  OutStorehouseQuantity12 ) != 0 ) 
							and [Year] = @year order by DS_MT.ItemNum";
						}
					}//WHERE II_MT.ItemNum = @hdnum
				}
					break;

				case "OtherInOutStorehouse"://기타입출고
				{
					if(m_hdItemNum.Trim() == "")
					{
						if(m_Store.ToString() == "BS_MT")
						{
							str = " SELECT BS_MT.ItemNum, PUC_MT.SmallClassificationName AS ProcessName, " +
								" II_MT.ItemDrawNum, II_MT.ItemName, BS_MT.ProcessSequenceNum, " +
								" BS_MT.ProcessCode, BS_MT.BusinessStorehouseIndex AS StorehouseIndex, " +
								" BS_MT.StockQuantity12, BS_MT.StockCost12 " +
								" FROM BS_MT INNER JOIN " +
								" II_MT ON BS_MT.ItemNum = II_MT.ItemNum INNER JOIN " +
								" PUC_MT ON BS_MT.ProcessCode = PUC_MT.SmallClassificationCode "+
								" where II_MT.ItemNum Like @num and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name and BS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and BusinessStorehouseNum = @storenum and [Year] = @year order by BS_MT.ItemNum";
						}
						else if(m_Store.ToString() == "PS_MT")
						{
							str = " SELECT II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum, " +
								" PS_MT.ProcessSequenceNum, PS_MT.ProcessCode, " +
								" PUC_MT.SmallClassificationName AS ProcessName, " +
								" PS_MT.ProductionStorehouseIndex AS StorehouseIndex, PS_MT.StockQuantity12, " +
								" PS_MT.StockCost12 " +
								" FROM PS_MT INNER JOIN " +
								" II_MT ON PS_MT.ItemNum = II_MT.ItemNum INNER JOIN " +
								" PUC_MT ON PS_MT.ProcessCode = PUC_MT.SmallClassificationCode "+
								"Where II_MT.ItemNum Like @num and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name and PS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and [Year] = @year order by PS_MT.ItemNum, PS_MT.ProcessSequenceNum";
						}
						else if(m_Store.ToString() == "RMS_MT")
						{
							str = " SELECT   II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum, " +
								" PUC_MT.SmallClassificationName AS ProcessName, " +
								" RMS_MT.ProcessSequenceNum, RMS_MT.ProcessCode, " +
								" RMS_MT.RowMetarialStorehouseIndex AS StorehouseIndex, " +
								" RMS_MT.StockQuantity12, RMS_MT.StockCost12 " +
								" FROM      II_MT INNER JOIN " +
								" RMS_MT ON II_MT.ItemNum = RMS_MT.ItemNum INNER JOIN " +
								" PUC_MT ON RMS_MT.ProcessCode = PUC_MT.SmallClassificationCode "+
								" Where II_MT.ItemNum Like @num and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name and RMS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and [Year] = @year order by RMS_MT.ItemNum";

						}
						else if(m_Store.ToString() == "OS_MT")
						{
							str = " SELECT   II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum, "+
								" OS_MT.ProcessSequenceNum, OS_MT.ProcessCode, "+
								" PUC_MT.SmallClassificationName AS ProcessName, " +
								" OS_MT.OutSideStorehouseIndex AS StorehouseIndex, "+
								" OS_MT.StockQuantity12,  OS_MT.StockCost12, " +
								" CI_MT.CompanyName, CI_MT.BusinessRegistrationNum " +
								" FROM		 II_MT INNER JOIN "+
								" OS_MT ON  II_MT.ItemNum = OS_MT.ItemNum INNER JOIN "+
								" PUC_MT ON OS_MT.ProcessCode =  PUC_MT.SmallClassificationCode " +
								" INNER JOIN CI_MT ON OS_MT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum " +
								" WHERE II_MT.ItemNum Like @num and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name AND OS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and CI_MT.RecodingState = 1 AND OS_MT.BusinessRegistrationNum like @comnum and [Year] = @year order by OS_MT.ItemNum, OS_MT.ProcessSequenceNum ";
						}
						else
						{
							str = " SELECT   II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum, " +
								" PUC_MT.SmallClassificationName AS ProcessName, " +
								" DS_MT.ProcessSequenceNum, DS_MT.ProcessCode, DS_MT.StockQuantity12, " +
								" DS_MT.StockCost12, DS_MT.DeliveryStorehouseIndex AS StorehouseIndex " +
								" FROM      II_MT INNER JOIN " +
								" DS_MT ON II_MT.ItemNum = DS_MT.ItemNum INNER JOIN " +
								" PUC_MT ON DS_MT.ProcessCode = PUC_MT.SmallClassificationCode "+
								"Where II_MT.ItemNum Like @num and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name and DS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and [Year] = @year order by DS_MT.ItemNum";
						}
					}
					else
					{
						if(m_Store.ToString() == "BS_MT")
						{
							str = " SELECT BS_MT.ItemNum, PUC_MT.SmallClassificationName AS ProcessName, " +
								" II_MT.ItemDrawNum, II_MT.ItemName, BS_MT.ProcessSequenceNum, " +
								" BS_MT.ProcessCode, BS_MT.BusinessStorehouseIndex AS StorehouseIndex, " +
								" BS_MT.StockQuantity12, BS_MT.StockCost12 " +
								" FROM BS_MT INNER JOIN " +
								" II_MT ON BS_MT.ItemNum = II_MT.ItemNum INNER JOIN " +
								" PUC_MT ON BS_MT.ProcessCode = PUC_MT.SmallClassificationCode "+
								" where II_MT.ItemNum = @hdnum and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name and BS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and BusinessStorehouseNum = @storenum and [Year] = @year order by BS_MT.ItemNum";
						}
						else if(m_Store.ToString() == "PS_MT")
						{
							str = " SELECT II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum, " +
								" PS_MT.ProcessSequenceNum, PS_MT.ProcessCode, " +
								" PUC_MT.SmallClassificationName AS ProcessName, " +
								" PS_MT.ProductionStorehouseIndex AS StorehouseIndex, PS_MT.StockQuantity12, " +
								" PS_MT.StockCost12 " +
								" FROM PS_MT INNER JOIN " +
								" II_MT ON PS_MT.ItemNum = II_MT.ItemNum INNER JOIN " +
								" PUC_MT ON PS_MT.ProcessCode = PUC_MT.SmallClassificationCode "+
								"Where II_MT.ItemNum = @hdnum and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name and PS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and [Year] = @year order by PS_MT.ItemNum, PS_MT.ProcessSequenceNum";
						}
						else if(m_Store.ToString() == "RMS_MT")
						{
							str = " SELECT   II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum, " +
								" PUC_MT.SmallClassificationName AS ProcessName, " +
								" RMS_MT.ProcessSequenceNum, RMS_MT.ProcessCode, " +
								" RMS_MT.RowMetarialStorehouseIndex AS StorehouseIndex, " +
								" RMS_MT.StockQuantity12, RMS_MT.StockCost12 " +
								" FROM      II_MT INNER JOIN " +
								" RMS_MT ON II_MT.ItemNum = RMS_MT.ItemNum INNER JOIN " +
								" PUC_MT ON RMS_MT.ProcessCode = PUC_MT.SmallClassificationCode "+
								" Where II_MT.ItemNum = @hdnum and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name and RMS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and [Year] = @year order by RMS_MT.ItemNum";

						}
						else if(m_Store.ToString() == "OS_MT")
						{
							str = " SELECT   II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum, "+
								" OS_MT.ProcessSequenceNum, OS_MT.ProcessCode, "+
								" PUC_MT.SmallClassificationName AS ProcessName, " +
								" OS_MT.OutSideStorehouseIndex AS StorehouseIndex, "+
								" OS_MT.StockQuantity12,  OS_MT.StockCost12, " +
								" CI_MT.CompanyName, CI_MT.BusinessRegistrationNum " +
								" FROM		 II_MT INNER JOIN "+
								" OS_MT ON  II_MT.ItemNum = OS_MT.ItemNum INNER JOIN "+
								" PUC_MT ON OS_MT.ProcessCode =  PUC_MT.SmallClassificationCode " +
								" INNER JOIN CI_MT ON OS_MT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum " +
								" WHERE II_MT.ItemNum = @hdnum and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name AND OS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and CI_MT.RecodingState = 1 AND OS_MT.BusinessRegistrationNum like @comnum and [Year] = @year order by OS_MT.ItemNum, OS_MT.ProcessSequenceNum ";
						}
						else
						{
							str = " SELECT   II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum, " +
								" PUC_MT.SmallClassificationName AS ProcessName, " +
								" DS_MT.ProcessSequenceNum, DS_MT.ProcessCode, DS_MT.StockQuantity12, " +
								" DS_MT.StockCost12, DS_MT.DeliveryStorehouseIndex AS StorehouseIndex " +
								" FROM      II_MT INNER JOIN " +
								" DS_MT ON II_MT.ItemNum = DS_MT.ItemNum INNER JOIN " +
								" PUC_MT ON DS_MT.ProcessCode = PUC_MT.SmallClassificationCode "+
								"Where II_MT.ItemNum = @hdnum and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name and DS_MT.RecodingState = 1 and II_MT.RecodingState = 1 and [Year] = @year order by DS_MT.ItemNum";
						}
					}
				}
					break;
				case "OtherInOutStorehousePC"://기타입출고현황
					str = "Select ItemNum, ItemDrawNum, ItemName, ProcessCode, ProcessSequenceNum,ProcessName, StorehouseName, BusinessStorehouseNum, "+
						"case InOutStorehouseDistinction when 1 then '입고' else '출고' end as InOutStorehouseDistinction, InOutDate, "+
						"InOutStorehouseQuantity, InOutStorehouseReasonCode, InOutStorehouseReason, "+
						"InOutStorehouseDetailReason, CompanyName, BusinessRegistrationNum, RegistrationPerson, "+
						"RegistrationPersonID, RegistrationDate, UpdatingPerson, UpdatingPersonID, UpdatingDate, "+
						"OtherInOutStorehouseHistoryIndex From OIOS_HT "+
						"Where ItemNum Like @num and ItemDrawNum Like @draw and ItemName Like @name and StorehouseName Like @store and (InOutDate >= @begin and InOutDate <= @end) order by OtherInOutStorehouseHistoryIndex desc";
					break;
				case "ClaimRegistrationPC"://클레임등록현황
					str = "Select CL_HT.* From CL_HT inner join II_MT on CL_HT.ItemNum = II_MT.ItemNum Where II_MT.RecodingState = 1 and CL_HT.ItemNum Like @num and CL_HT.ItemDrawNum Like @draw and II_MT.ItemName Like @name and CompanyName Like @company and (CL_HT.RegistrationDate >= @begin and CL_HT.RegistrationDate <= @end) order by ClameHistoryIndex desc";
					break;				
				case "ProductionPlan"://생산계획
					str = @"Select isnull(SmallClassificationName,'') as ItemState,  PR_HT.* 
						From PR_HT inner join II_MT on PR_HT.ItemNum = II_MT.ItemNum 
						left outer join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
						Where II_MT.RecodingState = 1 and ProgressCondition = '대기' and PR_HT.ItemNum Like @num and PR_HT.ItemDrawNum Like @draw and PR_HT.ItemName Like @name and (RequestDate1 >= @begin and RequestDate1 <= @end)  and  ItemClassification1 Like @Classification1 ";
					break;
				case "ProductionPlanPC"://생산계획현황
					str = @"Select isnull(PUC1.SmallClassificationName,'') as ItemState,  PP_HT.ItemNum, PP_HT.ItemDrawNum, PP_HT.ItemName, ProductionPlanHistorySourceCode, 
						ProductionPlanHistorySource, ProductionPlanQuantity, ProductionBeginDate, DeliveryDate, 
						case RowMaterialCalculation when 1 then '산출' else '미산출' end as RowMaterialCalculation, 
						VolumNum, ProgressCondition, PP_HT.RegistrationPerson, PP_HT.RegistrationPersonID, 
						PP_HT.RegistrationDate, PP_HT.UpdatingPerson, PP_HT.UpdatingPersonID, PP_HT.UpdatingDate, 
						HistoryIndex, HistorySection, ProductionPlanHistoryIndex From PP_HT 
						inner join II_MT on PP_HT.ItemNum = II_MT.ItemNum 
						inner join PUC_MT PUC on II_MT.ItemClassification1 = PUC.SmallClassificationCode
						left outer join PUC_MT PUC1 on II_MT.ItemState = PUC1.SmallClassificationCode
						Where II_MT.RecodingState = 1 and  PP_HT.ItemNum Like @num and PP_HT.ItemDrawNum Like @draw and PP_HT.ItemName Like @name 
						and (ProductionBeginDate >= @begin and ProductionBeginDate <= @end) 
						and (DeliveryDate >= @from and DeliveryDate <= @to) 				
						and ItemClassification1 Like @Classification1
						and ProgressCondition Like @progress
						 and ((HistorySection != '기종별등록') OR (HistorySection IS NULL)) order by ProductionPlanHistoryIndex desc";
					break;
				case "ProductionResultPC"://생산실적보기
					str = "Select * From SM_HT Where ItemName Like @num and (RegistrationDate >= @begin and RegistrationDate <= @end) order by StorehouseMovingHistoryIndex desc";
					break;
				case "RowMaterialRequirementCalculate"://자재소요량산출(생산계획원장)
					str = @"Select isnull(PUC1.SmallClassificationName,'') as ItemState,  PP_HT.ItemNum, PP_HT.ItemDrawNum, PP_HT.ItemName, ProductionPlanHistorySourceCode,
						ProductionPlanHistorySource, ProductionPlanQuantity, ProductionBeginDate,
						case RowMaterialCalculation when 1 then '산출' else '미산출' end as RowMaterialCalculation,
						VolumNum, ProgressCondition, PP_HT.RegistrationPerson, PP_HT.RegistrationPersonID,
						PP_HT.RegistrationDate, PP_HT.UpdatingPerson, PP_HT.UpdatingPersonID, PP_HT.UpdatingDate,
						HistoryIndex, HistorySection, ProductionPlanHistoryIndex From PP_HT
						inner join II_MT on PP_HT.ItemNum = II_Mt.ItemNum
						inner join PUC_MT PUC on II_MT.ItemClassification1 = PUC.SmallClassificationCode
						left outer join PUC_MT PUC1 on ItemState = PUC1.SmallClassificationCode
						Where II_MT.RecodingState = 1 and PUC.RecodingState = 1 and (PUC1.RecodingState =1  or PUC1.RecodingState is null) and  PP_HT.ItemNum Like @num and PP_HT.ItemDrawNum Like @draw and PP_HT.ItemName Like @name and RowMaterialCalculation = '0' and (ProductionBeginDate >= @begin and ProductionBeginDate <= @end) and ItemClassification1 Like @Classification1   ";
					break;
				case "RowMaterialRequirementCalculatePC"://자재소요량산출현황(생산계획원장)
					str = "Select ItemNum, ItemDrawNum, ItemName, ProductionPlanHistorySourceCode, "+
						"ProductionPlanHistorySource, ProductionPlanQuantity, ProductionBeginDate, DeliveryDate, "+
						"case RowMaterialCalculation when 1 then '산출' else '미산출' end as RowMaterialCalculation, "+
						"VolumNum, ProgressCondition, RegistrationPerson, RegistrationPersonID, "+
						"RegistrationDate, UpdatingPerson, UpdatingPersonID, UpdatingDate, "+
						"HistoryIndex, HistorySection, ProductionPlanHistoryIndex From PP_HT "+
						"Where ItemNum Like @num and ItemDrawNum Like @draw and ItemName Like @name and (ProductionBeginDate >= @begin and ProductionBeginDate <= @end) order by ProductionPlanHistoryIndex desc";
					break;
				case "WorkPlan"://작업계획(생산계획원장)
					str = @"Select II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, ProductionPlanHistorySourceCode,
						ProductionPlanHistorySource, ProductionPlanQuantity, ProductionBeginDate, DeliveryDate,
						case RowMaterialCalculation when 1 then '산출' else '미산출' end as RowMaterialCalculation,
						VolumNum, ProgressCondition, PP_HT.RegistrationPerson, PP_HT.RegistrationPersonID,
						PP_HT.RegistrationDate, PP_HT.UpdatingPerson, PP_HT.UpdatingPersonID, PP_HT.UpdatingDate,
						HistoryIndex, HistorySection, ProductionPlanHistoryIndex From PP_HT 
						inner join II_MT on PP_HT.ItemNum = II_MT.ItemNum 						
						Where PP_HT.ProgressCondition = '대기' and  II_MT.RecodingState = 1 and  PP_HT.ItemNum Like @num and PP_HT.ItemDrawNum Like @draw and PP_HT.ItemName Like @name and (ProductionBeginDate >= @begin and ProductionBeginDate <= @end)
						and ItemClassification1 like @Classification1
						and ItemClassification2 like @Classification2
						order by ProductionPlanHistoryIndex ";
					break;
				case "WorkDailyReportRegistration"://작업일보등록
					str = @"Select * From WDWP_HT Where (ProgressCondition = '지시' or ProgressCondition = '진행') and WorkDistinction != '외주' and ItemNum Like @num and ItemDrawNum Like @draw and ItemName Like @name and (WorkDate >= @begin and WorkDate <= @end) and WCName Like @wcname order by ItemNum, ProcessSequenceNum ";
					break;
				case "WorkDailyReportRegistration1":
					str = @"Select WDWP_HT.* From WDWP_HT 
						left outer join PP_HT on WDWP_HT.ProductionPlanHistoryIndex = PP_HT.ProductionPlanHistoryIndex
						Where ( (WDWP_HT.ProgressCondition = '지시' or WDWP_HT.ProgressCondition = '진행' or WDWP_HT.ProgressCondition = '대기')
						and WorkDistinction != '외주')
						and WDWP_HT.ItemNum Like @num and WDWP_HT.ItemDrawNum Like @draw and WDWP_HT.ItemName Like @name 
						and (WDWP_HT.WorkDate >= @begin and WDWP_HT.WorkDate <= @end) and WDWP_HT.WCName Like @wcname 
						order by WDWP_HT.WCDailyWorkPlanHistoryIndex";
//					str = @"Select WDWP_HT.* From WDWP_HT 
//						left outer join PP_HT on WDWP_HT.ProductionPlanHistoryIndex = PP_HT.ProductionPlanHistoryIndex
//						Where( ( (WDWP_HT.ProgressCondition = '지시' or WDWP_HT.ProgressCondition = '진행' or WDWP_HT.ProgressCondition = '대기')
//						and WorkDistinction != '외주'
//						and WDWP_HT.ProcessSequenceNum = (Select Max(ProcessSequenceNum) From PSI_MT where ItemNum = WDWP_HT.ItemNum and RecodingState =1) )
//						or ( (WDWP_HT.ProgressCondition = '지시' or WDWP_HT.ProgressCondition = '진행' or WDWP_HT.ProgressCondition = '대기') and  WDWP_HT.ProductionPlanHistoryIndex is null and WorkDistinction != '외주') )
//						and WDWP_HT.ItemNum Like @num and WDWP_HT.ItemDrawNum Like @draw and WDWP_HT.ItemName Like @name 
//						and (WDWP_HT.WorkDate >= @begin and WDWP_HT.WorkDate <= @end) and WDWP_HT.WCName Like @wcname 
//						order by WDWP_HT.WCDailyWorkPlanHistoryIndex";
//					str = @"Select WDWP_HT.* From WDWP_HT 
//						inner join PP_HT on WDWP_HT.ProductionPlanHistoryIndex = PP_HT.ProductionPlanHistoryIndex
//						Where (WDWP_HT.ProgressCondition = '지시' or WDWP_HT.ProgressCondition = '진행' or WDWP_HT.ProgressCondition = '대기')
//						and WorkDistinction != '외주'  and  WDWP_HT.ProductionPlanHistoryIndex is not null
//						and WDWP_HT.ProcessSequenceNum = (Select Max(ProcessSequenceNum) From PSI_MT where ItemNum = WDWP_HT.ItemNum and RecodingState =1)
//						and WDWP_HT.ProductItemNum = WDWP_HT.ItemNum
//						and WDWP_HT.ItemNum Like @num and WDWP_HT.ItemDrawNum Like @draw and WDWP_HT.ItemName Like @name 
//						and (WDWP_HT.WorkDate >= @begin and WDWP_HT.WorkDate <= @end) and WDWP_HT.WCName Like @wcname 
//						order by WDWP_HT.WCDailyWorkPlanHistoryIndex   ";
					break;
				case "WorkDailyReportRegistrationPC"://작업일보현황
					if(m_Worker == "")
					{
						str = @"select ItemNum, ItemDrawNum, ItemName, ProcessSequenceNum, ProcessCode, ProcessName, ProductItemNum, ProductDrawNum, 
						ProductName, ParentItemNum, ParentDrawNum, ParentName, WCInfoIndex, WCName, WorkPlanQuantity, WorkCompletionQuantity, 
						ThisWorkCompletionQuantity, RemainQuantity, SuitabilityQuantity, UnSuitabilityQuantity, UnSuitabilityCost, 
						cast(WorkBeginTime as smalldatetime) as WorkBeginTime, cast(WorkEndTime as smalldatetime) as WorkEndTime, Worker, WorkerID, 
						NonWorkTime1, NonWorkTimeCode1, NonWorkTimeReason1, NonWorkTime2, NonWorkTimeCode2, NonWorkTimeReason2, 
						NonWorkTime3, NonWorkTimeCode3, NonWorkTimeReason3, UnSuitabilityStatusCode, UnSuitabilityStatusMeaning, 
						UnSuitabilityDetailMeaning, UnSuitabilityCauseCode, UnSuitabilityCauseMeaning, InspectionDecisionCode, EtcNum1,EtcNum2,
						InspectionDecision, UseTool1, UseJig1, UseTool2, UseJig2, UseTool3, UseJig3, 
						RegistrationPerson, RegistrationPersonID, RegistrationDate, 
						UpdatingPerson, UpdatingPersonID, UpdatingDate, 
						ProductionPlanHistoryIndex, WCDailyWorkPlanHistoryIndex, WorkDailyReportHistoryIndex 
						From WDR_HT Where WCName Like @wcname and ItemNum Like @num and ItemDrawNum Like @draw and ItemName Like @name and (WorkBeginTime >= @begin and WorkBeginTime <= @end) ORDER  By WorkBeginTime desc, WorkDailyReportHistoryIndex desc";
					}
					else
					{
						str = @"select ItemNum, ItemDrawNum, ItemName, ProcessSequenceNum, ProcessCode, ProcessName, ProductItemNum, ProductDrawNum, 
						ProductName, ParentItemNum, ParentDrawNum, ParentName, WCInfoIndex, WCName, WorkPlanQuantity, WorkCompletionQuantity, 
						ThisWorkCompletionQuantity, RemainQuantity, SuitabilityQuantity, UnSuitabilityQuantity, UnSuitabilityCost, 
						cast(WorkBeginTime as smalldatetime) as WorkBeginTime, cast(WorkEndTime as smalldatetime) as WorkEndTime, Worker, WorkerID, 
						NonWorkTime1, NonWorkTimeCode1, NonWorkTimeReason1, NonWorkTime2, NonWorkTimeCode2, NonWorkTimeReason2, 
						NonWorkTime3, NonWorkTimeCode3, NonWorkTimeReason3, UnSuitabilityStatusCode, UnSuitabilityStatusMeaning, 
						UnSuitabilityDetailMeaning, UnSuitabilityCauseCode, UnSuitabilityCauseMeaning, InspectionDecisionCode,  EtcNum1,EtcNum2,
						InspectionDecision, UseTool1, UseJig1, UseTool2, UseJig2, UseTool3, UseJig3, 
						RegistrationPerson, RegistrationPersonID, RegistrationDate, 
						UpdatingPerson, UpdatingPersonID, UpdatingDate, 
						ProductionPlanHistoryIndex, WCDailyWorkPlanHistoryIndex, WorkDailyReportHistoryIndex 
						From WDR_HT Where WCName Like @wcname and ItemNum Like @num and ItemDrawNum Like @draw and ItemName Like @name and (WorkBeginTime >= @begin and WorkBeginTime <= @end) and Worker = @Worker ORDER  By WorkBeginTime desc, WorkDailyReportHistoryIndex desc";
					}
					break;
				case "RowMaterialRequriementPC"://원자재구매의뢰현황
					str = @"Select BR_HT.ItemNum, BR_HT.ItemDrawNum, BR_HT.ItemName, P.SmallClassificationName as Unit, II_MT.Standard, BR_HT.PropertyClassification, BR_HT.BuyingRequestSourceCode, BR_HT.BuyingRequestSource, BR_HT.FirstDeliveryDemandQuantity, BR_HT.FirstDeliveryDemandDate, BR_HT.SecondDeliveryDemandQuantity, SecondDeliveryDemandDate, ThirdDeliveryDemandQuantity, ThirdDeliveryDemandDate, FourthDeliveryDemandQuantity, FourthDeliveryDemandDate, FifthDeliveryDemandQuantity, FifthDeliveryDemandDate, BR_HT.OrderQuantity, BR_HT.ApplyUnitCost, BR_HT.TotalCost, VolumNum, BR_HT.RequestPostCode, BR_HT.RequestPost, BR_HT.ProgressCondition, BR_HT.RegistrationPerson, BR_HT.RegistrationPersonID, BR_HT.RegistrationDate, BR_HT.UpdatingPerson, BR_HT.UpdatingPersonID, BR_HT.UpdatingDate, BR_HT.HistoryIndex, BR_HT.HistorySection, BR_HT.BuyingRequestHistoryIndex From BR_HT
					inner join II_MT on BR_HT.ItemNum = II_MT.ItemNum
					inner join PUC_MT P on II_MT.Unit = P.SmallClassificationCode					
					left outer join PUC_MT P1 on II_MT.ItemClassification1 = P1.SmallClassificationCode
					Where II_MT.RecodingState = 1 and BR_HT.ItemNum Like @num and BR_HT.ItemDrawNum Like @draw and BR_HT.ItemName Like @name and (BR_HT.RegistrationDate >= @begin and BR_HT.RegistrationDate <= @end) and BR_HT.PropertyClassification = '원자재' and ItemClassification1 like @Classification1  order by BuyingRequestHistoryIndex desc";
					break;
				case "OutsideRequestPC"://외주의뢰현황
					//str = @"OOR_HT.ItemNum, OOR_HT.ItemDrawNum, OOR_HT.ItemName, OOR_HT.UnitCostDistinction, OOR_HT.BeginProcessCode, OOR_HT.BeginProcess, OOR_HT.EndProcessCode, OOR_HT.EndProcess, OOR_HT.ApplyUnitCost, OOR_HT.FirstDeliveryDemandQuantity, OOR_HT.FirstDeliveryDemandDate, SecondDeliveryDemandQuantity, SecondDeliveryDemandDate, ThirdDeliveryDemandQuantity, ThirdDeliveryDemandDate, FourthDeliveryDemandQuantity, FourthDeliveryDemandDate, FifthDeliveryDemandQuantity, FifthDeliveryDemandDate, OOR_HT.OrderQuantity, OOR_HT.TotalCost, OOR_HT.VolumNum, OOR_HT.ProgressCondition, OOR_HT.RegistrationPerson, OOR_HT.RegistrationPersonID, OOR_HT.RegistrationDate, OOR_HT.UpdatingPerson, OOR_HT.UpdatingPersonID, OOR_HT.UpdatingDate, OOR_HT.ProductionPlanHistoryIndex, OOR_HT.OutSideOrderRequestHistoryIndex";
					//str = "Select * From OOR_HT Where ItemNum Like @num and ItemDrawNum Like @draw and ItemName Like @name and (RegistrationDate >= @begin and RegistrationDate <= @end) order by OutSideOrderRequestHistoryIndex desc";
					break;
				case "BuyingOrderPC"://구매발주현황
					if(m_ProgressState == "전체")
					{
						str = @"Select BO_HT.*, P.SmallClassificationName as Unit,  Standard, P1.SmallClassificationName as ItemClassification1,  P2.SmallClassificationName as ItemClassification2, P3.SmallClassificationName as ItemClassification3, P4.SmallClassificationName as ItemClassification4  
							From BO_HT inner join II_MT on BO_HT.ItemNum = II_MT.ItemNum 
							inner join PUC_MT P on II_MT.Unit = P.SmallClassificationCode
							left outer join PUC_MT P1 on II_MT.ItemClassification1 = P1.SmallClassificationCode
							left outer join PUC_MT P2 on II_MT.ItemClassification2 = P2.SmallClassificationCode
							left outer join PUC_MT P3 on II_MT.ItemClassification3 = P3.SmallClassificationCode
							left outer join PUC_MT P4 on II_MT.ItemClassification4 = P4.SmallClassificationCode
							inner join PUC_MT P5 on II_MT.ItemClassification1 = P5.SmallClassificationCode
							Where II_MT.RecodingState =  1 and  CompanyName Like @company and BusinessRegistrationNum like @comnum and  BO_HT.ItemNum Like @num and BO_HT.ItemDrawNum Like @draw and BO_HT.ItemName Like @name and (BO_HT.FirstDeliveryDemandDate >= @begin and BO_HT.FirstDeliveryDemandDate <= @end) and II_MT.ItemClassification1 Like @Classification1  order by BuyingOrderHistoryIndex desc" ;
					}
					else if(m_ProgressState == "완료")
					{
						str = @"Select BO_HT.*, P.SmallClassificationName as Unit,  Standard, P1.SmallClassificationName as ItemClassification1,  P2.SmallClassificationName as ItemClassification2, P3.SmallClassificationName as ItemClassification3, P4.SmallClassificationName as ItemClassification4  
							From BO_HT inner join II_MT on BO_HT.ItemNum = II_MT.ItemNum 
							inner join PUC_MT P on II_MT.Unit = P.SmallClassificationCode
							left outer join PUC_MT P1 on II_MT.ItemClassification1 = P1.SmallClassificationCode
							left outer join PUC_MT P2 on II_MT.ItemClassification2 = P2.SmallClassificationCode
							left outer join PUC_MT P3 on II_MT.ItemClassification3 = P3.SmallClassificationCode
							left outer join PUC_MT P4 on II_MT.ItemClassification4 = P4.SmallClassificationCode
							inner join PUC_MT P5 on II_MT.ItemClassification1 = P5.SmallClassificationCode
							Where II_MT.RecodingState =  1 and ProgressCondition = '완료' and CompanyName Like @company and BusinessRegistrationNum like @comnum and  BO_HT.ItemNum Like @num and BO_HT.ItemDrawNum Like @draw and BO_HT.ItemName Like @name and (BO_HT.FirstDeliveryDemandDate >= @begin and BO_HT.FirstDeliveryDemandDate <= @end) and II_MT.ItemClassification1 Like @Classification1 order by BuyingOrderHistoryIndex desc";
					}
					else if(m_ProgressState == "미납")
					{
						str = @"Select BO_HT.*, P.SmallClassificationName as Unit,  Standard, P1.SmallClassificationName as ItemClassification1,  P2.SmallClassificationName as ItemClassification2, P3.SmallClassificationName as ItemClassification3, P4.SmallClassificationName as ItemClassification4  
							From BO_HT inner join II_MT on BO_HT.ItemNum = II_MT.ItemNum 
							inner join PUC_MT P on II_MT.Unit = P.SmallClassificationCode
							left outer join PUC_MT P1 on II_MT.ItemClassification1 = P1.SmallClassificationCode
							left outer join PUC_MT P2 on II_MT.ItemClassification2 = P2.SmallClassificationCode
							left outer join PUC_MT P3 on II_MT.ItemClassification3 = P3.SmallClassificationCode
							left outer join PUC_MT P4 on II_MT.ItemClassification4 = P4.SmallClassificationCode
							inner join PUC_MT P5 on II_MT.ItemClassification1 = P5.SmallClassificationCode
							Where II_MT.RecodingState =  1 and (ProgressCondition = '대기' or ProgressCondition = '진행') and CompanyName Like @company and BusinessRegistrationNum like @comnum and  BO_HT.ItemNum Like @num and BO_HT.ItemDrawNum Like @draw and BO_HT.ItemName Like @name and (BO_HT.FirstDeliveryDemandDate >= @begin and BO_HT.FirstDeliveryDemandDate <= @end) and II_MT.ItemClassification1 Like @Classification1 order by BuyingOrderHistoryIndex desc";
					
					}
					else  if(m_ProgressState == "대기")
					{
						str = @"Select BO_HT.*, P.SmallClassificationName as Unit,  Standard, P1.SmallClassificationName as ItemClassification1,  P2.SmallClassificationName as ItemClassification2, P3.SmallClassificationName as ItemClassification3, P4.SmallClassificationName as ItemClassification4  
							From BO_HT inner join II_MT on BO_HT.ItemNum = II_MT.ItemNum 
							inner join PUC_MT P on II_MT.Unit = P.SmallClassificationCode
							left outer join PUC_MT P1 on II_MT.ItemClassification1 = P1.SmallClassificationCode
							left outer join PUC_MT P2 on II_MT.ItemClassification2 = P2.SmallClassificationCode
							left outer join PUC_MT P3 on II_MT.ItemClassification3 = P3.SmallClassificationCode
							left outer join PUC_MT P4 on II_MT.ItemClassification4 = P4.SmallClassificationCode
							inner join PUC_MT P5 on II_MT.ItemClassification1 = P5.SmallClassificationCode
							Where II_MT.RecodingState =  1 and ProgressCondition = '대기' and CompanyName Like @company and BusinessRegistrationNum like @comnum and  BO_HT.ItemNum Like @num and BO_HT.ItemDrawNum Like @draw and BO_HT.ItemName Like @name and (BO_HT.FirstDeliveryDemandDate >= @begin and BO_HT.FirstDeliveryDemandDate <= @end) and II_MT.ItemClassification1 Like @Classification1 order by BuyingOrderHistoryIndex desc";					
					}
					else  if(m_ProgressState == "진행")
					{
						str = @"Select BO_HT.*, P.SmallClassificationName as Unit,  Standard, P1.SmallClassificationName as ItemClassification1,  P2.SmallClassificationName as ItemClassification2, P3.SmallClassificationName as ItemClassification3, P4.SmallClassificationName as ItemClassification4  
							From BO_HT inner join II_MT on BO_HT.ItemNum = II_MT.ItemNum 
							inner join PUC_MT P on II_MT.Unit = P.SmallClassificationCode
							left outer join PUC_MT P1 on II_MT.ItemClassification1 = P1.SmallClassificationCode
							left outer join PUC_MT P2 on II_MT.ItemClassification2 = P2.SmallClassificationCode
							left outer join PUC_MT P3 on II_MT.ItemClassification3 = P3.SmallClassificationCode
							left outer join PUC_MT P4 on II_MT.ItemClassification4 = P4.SmallClassificationCode
							inner join PUC_MT P5 on II_MT.ItemClassification1 = P5.SmallClassificationCode
							Where II_MT.RecodingState =  1 and ProgressCondition = '진행' and CompanyName Like @company and BusinessRegistrationNum like @comnum and  BO_HT.ItemNum Like @num and BO_HT.ItemDrawNum Like @draw and BO_HT.ItemName Like @name and (BO_HT.FirstDeliveryDemandDate >= @begin and BO_HT.FirstDeliveryDemandDate <= @end) and II_MT.ItemClassification1 Like @Classification1 order by BuyingOrderHistoryIndex desc";
					
					}
					else
					{
						str = @"Select BO_HT.*, P.SmallClassificationName as Unit,  Standard, P1.SmallClassificationName as ItemClassification1,  P2.SmallClassificationName as ItemClassification2, P3.SmallClassificationName as ItemClassification3, P4.SmallClassificationName as ItemClassification4  
							From BO_HT inner join II_MT on BO_HT.ItemNum = II_MT.ItemNum 
							inner join PUC_MT P on II_MT.Unit = P.SmallClassificationCode
							left outer join PUC_MT P1 on II_MT.ItemClassification1 = P1.SmallClassificationCode
							left outer join PUC_MT P2 on II_MT.ItemClassification2 = P2.SmallClassificationCode
							left outer join PUC_MT P3 on II_MT.ItemClassification3 = P3.SmallClassificationCode
							left outer join PUC_MT P4 on II_MT.ItemClassification4 = P4.SmallClassificationCode
							inner join PUC_MT P5 on II_MT.ItemClassification1 = P5.SmallClassificationCode
							Where II_MT.RecodingState =  1 and ProgressCondition = '중단' and CompanyName Like @company and BusinessRegistrationNum like @comnum and  BO_HT.ItemNum Like @num and BO_HT.ItemDrawNum Like @draw and BO_HT.ItemName Like @name and (BO_HT.FirstDeliveryDemandDate >= @begin and BO_HT.FirstDeliveryDemandDate <= @end) and II_MT.ItemClassification1 Like @Classification1 order by BuyingOrderHistoryIndex desc";
					
					}
					break;
				case "BuyingDelivery"://구매납품
					str = @"Select BO_HT.*, SmallClassificationName as Unit, II_MT.Standard, UCI_MT.StandardUnitCost as UnitCost 
							From BO_HT 
							inner join II_MT on BO_HT.ItemNum = II_MT.ItemNum  
							inner join PUC_MT on II_MT.Unit = SmallClassificationCode  
							inner join UCI_MT on BO_HT.ItemNum = UCI_MT.ItemNum and BO_HT.BusinessRegistrationNum = UCI_MT.BusinessRegistrationNum
						Where (InStoreWaitingQuantity < RemainQuantity and OrderQuantity != 0) and
						(ProgressCondition != '완료' and ProgressCondition != '중단') and ItemClassification1 Like @Classification1 and II_MT.RecodingState = 1 and
						CompanyName Like @company and BO_HT.ItemNum Like @num and		
						BO_HT.ItemDrawNum Like @draw and BO_HT.ItemName Like @name and 
						(FirstDeliveryDemandDate >= @begin and FirstDeliveryDemandDate <= @end) and II_MT.RecodingState = '1'
						and UCI_MT.RecodingState = 1 and UCI_MT.UnitCostDistinction = '구매단가' order by BuyingOrderHistoryIndex";
					break;
				case "BuyingDeliveryPC"://구매납품현황(납품일자)
					str = @"Select BD_HT.*,CheckDistinction, SmallClassificationName as Unit, II_MT.Standard From BD_HT inner join II_MT on BD_HT.ItemNum = II_MT.ItemNum  inner join PUC_MT on II_MT.Unit = SmallClassificationCode  Where 
						CompanyName Like @company and BD_HT.ItemNum Like @num and  II_MT.RecodingState = 1 and ItemClassification1 Like @Classification1	and
						BD_HT.ItemDrawNum Like @draw and BD_HT.ItemName Like @name and  (DeliveryDate >= @begin and DeliveryDate <= @end) and II_MT.RecodingState = '1' order by BuyingDeliveryHistoryIndex desc";
				
					break;
				case "OutSideOrderPC"://외주발주현황
					if(m_ProgressState == "전체")
						str = @"Select OO_HT.*, PUC_MT.SmallClassificationName as Unit, P1.SmallClassificationName as ItemClassification1,  P2.SmallClassificationName as ItemClassification2, P3.SmallClassificationName as ItemClassification3, P4.SmallClassificationName as ItemClassification4  From OO_HT 
						INNER JOIN II_MT ON OO_HT.ItemNum = II_MT.ItemNum 
						inner join PUC_MT on II_MT.Unit = SmallClassificationCode 
						left outer join PUC_MT P1 on II_MT.ItemClassification1 = P1.SmallClassificationCode
						left outer join PUC_MT P2 on II_MT.ItemClassification2 = P2.SmallClassificationCode
						left outer join PUC_MT P3 on II_MT.ItemClassification3 = P3.SmallClassificationCode
						left outer join PUC_MT P4 on II_MT.ItemClassification4 = P4.SmallClassificationCode
						Where II_MT.RecodingState = 1 and CompanyName Like @company and BusinessRegistrationNum like @comnum and  OO_HT.ItemNum Like @num and OO_HT.ItemDrawNum Like @draw and OO_HT.ItemName Like @name and (OO_HT.FirstDeliveryDemandDate >= @begin and OO_HT.FirstDeliveryDemandDate <= @end) and ItemClassification1 Like @Classification1  order by OutSideOrderHistoryIndex desc";
					else if(m_ProgressState == "완료")
						str = @"Select OO_HT.*, PUC_MT.SmallClassificationName as Unit, P1.SmallClassificationName as ItemClassification1,  P2.SmallClassificationName as ItemClassification2, P3.SmallClassificationName as ItemClassification3, P4.SmallClassificationName as ItemClassification4 From OO_HT  
						INNER JOIN II_MT ON OO_HT.ItemNum = II_MT.ItemNum 
						inner join PUC_MT on II_MT.Unit = SmallClassificationCode 
						left outer join PUC_MT P1 on II_MT.ItemClassification1 = P1.SmallClassificationCode
						left outer join PUC_MT P2 on II_MT.ItemClassification2 = P2.SmallClassificationCode
						left outer join PUC_MT P3 on II_MT.ItemClassification3 = P3.SmallClassificationCode
						left outer join PUC_MT P4 on II_MT.ItemClassification4 = P4.SmallClassificationCode
						Where II_MT.RecodingState = 1 and ProgressCondition = '완료' and CompanyName Like @company and BusinessRegistrationNum like @comnum and  OO_HT.ItemNum Like @num and OO_HT.ItemDrawNum Like @draw and OO_HT.ItemName Like @name and (OO_HT.FirstDeliveryDemandDate >= @begin and OO_HT.FirstDeliveryDemandDate <= @end) and ItemClassification1 Like @Classification1   order by OutSideOrderHistoryIndex desc";
					else if(m_ProgressState == "중단")
						str = @"Select OO_HT.*, PUC_MT.SmallClassificationName as Unit, P1.SmallClassificationName as ItemClassification1,  P2.SmallClassificationName as ItemClassification2, P3.SmallClassificationName as ItemClassification3, P4.SmallClassificationName as ItemClassification4 From OO_HT  
						INNER JOIN II_MT ON OO_HT.ItemNum = II_MT.ItemNum 
						inner join PUC_MT on II_MT.Unit = SmallClassificationCode 
						left outer join PUC_MT P1 on II_MT.ItemClassification1 = P1.SmallClassificationCode
						left outer join PUC_MT P2 on II_MT.ItemClassification2 = P2.SmallClassificationCode
						left outer join PUC_MT P3 on II_MT.ItemClassification3 = P3.SmallClassificationCode
						left outer join PUC_MT P4 on II_MT.ItemClassification4 = P4.SmallClassificationCode
						Where II_MT.RecodingState = 1 and ProgressCondition = '중단' and CompanyName Like @company and BusinessRegistrationNum like @comnum and  OO_HT.ItemNum Like @num and OO_HT.ItemDrawNum Like @draw and OO_HT.ItemName Like @name and (OO_HT.FirstDeliveryDemandDate >= @begin and OO_HT.FirstDeliveryDemandDate <= @end) and ItemClassification1 Like @Classification1   order by OutSideOrderHistoryIndex desc";
					else
						str = @"Select OO_HT.*, PUC_MT.SmallClassificationName as Unit, P1.SmallClassificationName as ItemClassification1,  P2.SmallClassificationName as ItemClassification2, P3.SmallClassificationName as ItemClassification3, P4.SmallClassificationName as ItemClassification4 From OO_HT  
						INNER JOIN II_MT ON OO_HT.ItemNum = II_MT.ItemNum 
						inner join PUC_MT on II_MT.Unit = SmallClassificationCode
						left outer join PUC_MT P1 on II_MT.ItemClassification1 = P1.SmallClassificationCode
						left outer join PUC_MT P2 on II_MT.ItemClassification2 = P2.SmallClassificationCode
						left outer join PUC_MT P3 on II_MT.ItemClassification3 = P3.SmallClassificationCode
						left outer join PUC_MT P4 on II_MT.ItemClassification4 = P4.SmallClassificationCode
						where II_MT.RecodingState = 1 and (ProgressCondition = '대기' or ProgressCondition = '진행') and CompanyName Like @company and BusinessRegistrationNum like @comnum and  OO_HT.ItemNum Like @num and OO_HT.ItemDrawNum Like @draw and OO_HT.ItemName Like @name and (OO_HT.FirstDeliveryDemandDate >= @begin and OO_HT.FirstDeliveryDemandDate <= @end) and ItemClassification1 Like @Classification1  order by OutSideOrderHistoryIndex desc";
					break;
					
				case "OutSideProgressLook"://외주진행보기
					str = "Select * From OO_HT Where CompanyName Like @company and ItemNum Like @num and ItemDrawNum Like @draw and ItemName Like @name and (RegistrationDate >= @begin and RegistrationDate <= @end)";
					break;
				case "OutSideOutStorehouse"://외주출고
					str = "Select OO_HT.*, SmallClassificationName as Unit From OO_HT Inner join II_MT on OO_HT.ItemNum = II_Mt.ItemNum inner join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode  Where II_MT.RecodingState = 1 and  (ProgressCondition != '완료' and ProgressCondition != '중단') and CompanyName Like @company and BusinessRegistrationNum like @comnum and OO_HT.ItemNum Like @num and OO_HT.ItemDrawNum Like @draw and OO_HT.ItemName Like @name and (FirstDeliveryDemandDate >= @begin and FirstDeliveryDemandDate <= @end) and ItemClassification1 Like @Classification1";
					break;
				case "OutSideOutStorehousePC"://외주출고현황
					str = "Select OOS_HT.*, SmallClassificationName as Unit From OOS_HT Inner join II_MT on OOS_HT.ItemNum = II_Mt.ItemNum inner join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode  Where II_MT.RecodingState = 1 and CompanyName Like @company and BusinessRegistrationNum like @comnum and OOS_HT.ItemNum Like @num and OOS_HT.ItemDrawNum Like @draw and OOS_HT.ItemName Like @name and ThistimeOutStorehouseQuantity <> 0 and (OutStorehouseDate >= @begin and OutStorehouseDate <= @end) and ItemClassification1 Like @Classification1 order by  OutSideOutStorehouseHistoryIndex desc, OutStorehouseDate desc ";
					break;
				case "OutSideDelivery"://외주납품
					str = @"Select OO_HT.*, SmallClassificationName as Unit, UCI_MT.StandardUnitCost as UnitCost  
						From OO_HT Inner join II_MT on OO_HT.ItemNum = II_Mt.ItemNum  
						inner join PUC_MT on II_MT.Unit = SmallClassificationCode  
						inner join UCI_MT on OO_HT.ItemNum = UCI_MT.ItemNum and OO_HT.BusinessRegistrationNum = UCI_MT.BusinessRegistrationNum and OO_HT.BeginProcessCode = UCI_MT.BeginProcessCode and OO_HT.EndProcessCode = UCI_MT.EndProcessCode
						Where UCI_MT.RecodingState = 1 and UCI_MT.UnitCostDistinction = '외주단가' and II_MT.RecodingState = 1 and (InStoreWaitingQuantity < RemainQuantity and OrderQuantity != 0) and
						(ProgressCondition != '완료' and ProgressCondition != '중단') and 
						CompanyName Like @company and OO_HT.ItemNum Like @num and		
						OO_HT.ItemDrawNum Like @draw and OO_HT.ItemName Like @name and 
						ItemClassification1 Like @Classification1 and 
						(FirstDeliveryDemandDate >= @begin and FirstDeliveryDemandDate <= @end) order by OutSideOrderHistoryIndex";
					break;
				case "OutSideDeliveryPC"://외주납품현황
					if(m_ProgressState == "전체")
					{
						str = @"Select OSD_HT.*, SmallClassificationName as Unit From OSD_HT Inner join II_MT on OSD_HT.ItemNum = II_Mt.ItemNum inner join PUC_MT on II_MT.Unit = SmallClassificationCode  Where II_MT.RecodingState =1 and CompanyName Like @company and OSD_HT.ItemNum Like @num and OSD_HT.ItemDrawNum Like @draw and OSD_HT.ItemName Like @name and (DeliveryDate >= @begin and DeliveryDate <= @end) and ItemClassification1 Like @Classification1 order by OutSideDeliveryHistoryIndex desc";
					}
					else if(m_ProgressState == "완료")
					{
						str = @"Select OSD_HT.*, SmallClassificationName as Unit From OSD_HT 
								Inner join II_MT on OSD_HT.ItemNum = II_Mt.ItemNum 
								inner join PUC_MT on II_MT.Unit = SmallClassificationCode  
								Where II_MT.RecodingState =1 and ProgressCondition = '완료' and CompanyName Like @company and OSD_HT.ItemNum Like @num and OSD_HT.ItemDrawNum Like @draw and OSD_HT.ItemName Like @name and (DeliveryDate >= @begin and DeliveryDate <= @end) and ItemClassification1 Like @Classification1 order by OutSideDeliveryHistoryIndex desc";							
					}
					else if(m_ProgressState == "미납")
					{
						str = @"Select OSD_HT.*, SmallClassificationName as Unit From OSD_HT 
								Inner join II_MT on OSD_HT.ItemNum = II_Mt.ItemNum 
								inner join PUC_MT on II_MT.Unit = SmallClassificationCode  
								Where II_MT.RecodingState =1 and ProgressCondition != '완료' and CompanyName Like @company and OSD_HT.ItemNum Like @num and OSD_HT.ItemDrawNum Like @draw and OSD_HT.ItemName Like @name and (DeliveryDate >= @begin and DeliveryDate <= @end) and ItemClassification1 Like @Classification1 order by OutSideDeliveryHistoryIndex desc";												
					}

					break;
				case "PaymentPlanResultRegistrationPC"://지급실적현황
					str = "Select * From P_HT Where CompanyName Like @company and DecisionMethod Like @decisionMethod and (PaymentDate >= @begin and PaymentDate <= @end) order by PaymentHistoryIndex desc";
					break;
				case "ClaimPC"://클레임등록현황
					str = "Select * From PCL_HT Where ItemNum Like @num and ItemDrawNum Like @draw and ItemName Like @name and CompanyName Like @company and BusinessRegistrationNum like @comnum and (ReceiptDate >= @begin and ReceiptDate <= @end) order by ClameHistoryIndex desc";
					break;	
				case "OutStorehousePC":	//협력사 출고현황
					str = "Select * From OS_HT Where CompanyName Like @company and (RegistrationDate >= @begin and RegistrationDate <= @end)";
					break;
				case "QualityPC":		// 협력사 품질현황
					str = "Select * From  QI_HT Where CompanyName Like @company and ItemNum Like @num and ItemDrawNum Like @draw and ItemName Like @name and (RegistrationDate >= @begin and RegistrationDate <= @end)";
					break;
				case "QualityIndex":	// 품질지표현황
					str = "Select * From  QI_HT Where CompanyName Like @company and ItemNum Like @num and ItemDrawNum Like @draw and ItemName Like @name and (RegistrationDate >= @begin and RegistrationDate <= @end)";
					break;
				case "DeliveryPC":	//협력사 납품현황
					str = "Select * From DeliveryPC Where CompanyName Like @company and (RegistrationDate >= @begin and RegistrationDate <= @end)";
					break;
				case "StockPC":	//협력사 재고현황
					str = "Select * From StoreHousePC Where ItemNum Like @num and ItemDrawNum Like @draw and ItemName Like @name ";
					break;
				case "SubItemDelete"://하위품목 투입
				{
					if(m_Store.ToString() == "BS_MT")
					{
						str = " SELECT BS_MT.ItemNum, PUC_MT.SmallClassificationName AS ProcessName, " +
							" II_MT.ItemDrawNum, II_MT.ItemName, BS_MT.ProcessSequenceNum, " +
							" BS_MT.ProcessCode, BS_MT.BusinessStorehouseIndex AS StorehouseIndex, " +
							" BS_MT.StockQuantity12, BS_MT.StockCost12 " +
							" FROM BS_MT INNER JOIN " +
							" II_MT ON BS_MT.ItemNum = II_MT.ItemNum INNER JOIN " +
							" PUC_MT ON BS_MT.ProcessCode = PUC_MT.SmallClassificationCode "+
							" where II_MT.ItemNum Like @num and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name and BS_MT.RecodingState = 1 and [Year] = @year and II_MT.RecodingState = 1 and BusinessStorehouseNum = @storenum order by BS_MT.ItemNum";
					}
					else if(m_Store.ToString() == "PS_MT")
					{
						str = " SELECT II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum, " +
							" PS_MT.ProcessSequenceNum, PS_MT.ProcessCode, " +
							" PUC_MT.SmallClassificationName AS ProcessName, " +
							" PS_MT.ProductionStorehouseIndex AS StorehouseIndex, PS_MT.StockQuantity12, " +
							" PS_MT.StockCost12 " +
							" FROM PS_MT INNER JOIN " +
							" II_MT ON PS_MT.ItemNum = II_MT.ItemNum INNER JOIN " +
							" PUC_MT ON PS_MT.ProcessCode = PUC_MT.SmallClassificationCode "+
							"Where II_MT.ItemNum Like @num and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name and PS_MT.RecodingState = 1 and [Year] = @year and II_MT.RecodingState = 1 order by PS_MT.ItemNum, PS_MT.ProcessSequenceNum";
					}
					else if(m_Store.ToString() == "RMS_MT")
					{
						str = " SELECT   II_MT.ItemDrawNum, II_MT.ItemName, II_MT.ItemNum, " +
							" PUC_MT.SmallClassificationName AS ProcessName, " +
							" RMS_MT.ProcessSequenceNum, RMS_MT.ProcessCode, " +
							" RMS_MT.RowMetarialStorehouseIndex AS StorehouseIndex, " +
							" RMS_MT.StockQuantity12, RMS_MT.StockCost12 " +
							" FROM      II_MT INNER JOIN " +
							" RMS_MT ON II_MT.ItemNum = RMS_MT.ItemNum INNER JOIN " +
							" PUC_MT ON RMS_MT.ProcessCode = PUC_MT.SmallClassificationCode "+
							" Where II_MT.ItemNum Like @num and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name and RMS_MT.RecodingState = 1  and [Year] = @year and II_MT.RecodingState = 1 order by RMS_MT.ItemNum";
					}					
				}
					break;
				case "SubItemDeletePC"://하위품목투입현황
				{
					str = @"Select * From ST_HT Where ItemNum Like @num and ItemDrawNum Like @draw and ItemName Like @name
						and WCName Like @wcname and (ThrowingDate >= @begin and ThrowingDate <= @end) order by ThrowingDate ";
				}
					break;
				case "IncongruenceIndex"://부적합현황페이지
				{
					if(m_WorkDivision == "")
					{
						if(m_Worker == "")
						{
							str = @"Select ItemNum, ItemDrawNum, ItemName, WCName, ProcessSequenceNum, 
						ProcessName, SmallClassificationCode as ProcessCode,ThisWorkCompletionQuantity, SuitabilityQuantity, 
						UnSuitabilityQuantity, UnSuitabilityCost, UnSuitabilityStatusMeaning, 
						UnSuitabilityCauseMeaning, UnSuitabilityDetailMeaning, WorkBeginTime, 
						WorkEndTime, Worker, I.RegistrationDate, I.RegistrationPerson, I.UpdatingDate, 
						I.UpdatingPerson From IncongruenceIndex I inner join PUC_MT on ProcessName = SmallClassificationName
						where UnSuitabilityQuantity != 0 and PUC_MT.RecodingState = 1 and ItemNum Like @num and ItemDrawNum Like @draw and ItemName Like @name
						and SmallClassificationCode like @ProcessCode and WCName Like @wcname and (WorkBeginTime >= @begin and WorkBeginTime <= @end)  order by WorkBeginTime ";
						}
						else
						{
							str = @"Select ItemNum, ItemDrawNum, ItemName, WCName, ProcessSequenceNum, 
						ProcessName, SmallClassificationCode as ProcessCode,ThisWorkCompletionQuantity, SuitabilityQuantity, 
						UnSuitabilityQuantity, UnSuitabilityCost, UnSuitabilityStatusMeaning, 
						UnSuitabilityCauseMeaning, UnSuitabilityDetailMeaning, WorkBeginTime, 
						WorkEndTime, Worker, I.RegistrationDate, I.RegistrationPerson, I.UpdatingDate, 
						I.UpdatingPerson From IncongruenceIndex I inner join PUC_MT on ProcessName = SmallClassificationName
						where UnSuitabilityQuantity != 0 and PUC_MT.RecodingState = 1 and ItemNum Like @num and ItemDrawNum Like @draw and ItemName Like @name
						and SmallClassificationCode like @ProcessCode and WCName Like @wcname and (WorkBeginTime >= @begin and WorkBeginTime <= @end) and WorkerID = @Worker  order by WorkBeginTime ";
						}
						
					}
					else if(m_WorkDivision == "구매")
					{
						str = @"select ItemNum,ItemDrawNum,ItemName,'' as WCName, EndProcessSequenceNum as ProcessSequenceNum,
						SmallClassificationName as ProcessName,
						RequestQuantity as ThisWorkCompletionQuantity,
						SuitabilityQuantity,
						UnSuitabilityQuantity,
						UnSuitabilityCost,
						UnSuitabilityStatusMeaning,
						UnSuitabilityCauseMeaning,
						UnSuitabilityDetailMeaning,	
						QualityInspectionCompleteDate as WorkBeginTime,
						QualityInspectionCompleteDate as WorkEndTime,
						Investigator as Worker,
						QI_HT.RegistrationDate,
						QI_HT.RegistrationPerson,
						QI_HT.UpdatingDate,
						QI_HT.UpdatingPerson
						From QI_HT inner join PUC_MT on EndProcessName = SmallClassificationCode
						where UnSuitabilityQuantity != 0 and  PUC_MT.RecodingState = 1 and  ItemNum Like @num and ItemDrawNum Like @draw and ItemName Like @name
						and EndProcessName like @ProcessCode and HistorySection1 = '구매납품' and (QualityInspectionCompleteDate >= @begin and QualityInspectionCompleteDate <= @end) order by QualityInspectionCompleteDate ";
					}
					else if(m_WorkDivision == "외주")
					{
						str = @"select ItemNum,ItemDrawNum,ItemName,'' as WCName, EndProcessSequenceNum as ProcessSequenceNum,EndProcessName,
						SmallClassificationName as ProcessName,
						RequestQuantity as ThisWorkCompletionQuantity,
						SuitabilityQuantity,
						UnSuitabilityQuantity,
						UnSuitabilityCost,
						UnSuitabilityStatusMeaning,
						UnSuitabilityCauseMeaning,
						UnSuitabilityDetailMeaning,	
						QualityInspectionCompleteDate as WorkBeginTime,
						QualityInspectionCompleteDate as WorkEndTime,
						Investigator as Worker,
						QI_HT.RegistrationDate,
						QI_HT.RegistrationPerson,
						QI_HT.UpdatingDate,
						QI_HT.UpdatingPerson
						From QI_HT inner join PUC_MT on EndProcessName = SmallClassificationCode
						where UnSuitabilityQuantity != 0 and  PUC_MT.RecodingState = 1 and  ItemNum Like @num and ItemDrawNum Like @draw and ItemName Like @name
						and EndProcessName like @ProcessCode and HistorySection1 = '외주납품' and (QualityInspectionCompleteDate >= @begin and QualityInspectionCompleteDate <= @end) order by QualityInspectionCompleteDate ";
					}
					else if(m_WorkDivision == "자가")
					{
						if(m_Worker == "")
						{
							str = @"Select * From WDR_HT Where UnSuitabilityQuantity != 0 and  ItemNum Like @num and ItemDrawNum Like @draw and ItemName Like @name
						and ProcessCode like @ProcessCode and WCName Like @wcname and (WorkBeginTime >= @begin and WorkBeginTime <= @end) order by WorkBeginTime ";
						}
						else
						{
							str = @"Select * From WDR_HT Where UnSuitabilityQuantity != 0 and  ItemNum Like @num and ItemDrawNum Like @draw and ItemName Like @name
						and ProcessCode like @ProcessCode and WCName Like @wcname and (WorkBeginTime >= @begin and WorkBeginTime <= @end) and WorkerID = @Worker order by WorkBeginTime ";
						}
					}
				}
					break;
				case "AddItemInStore":
				{
					if(m_Store == "원자재창고")
						str = @"Select RMS_MT.ItemNum,II_MT.ItemDrawNum, II_MT.ItemName, ProcessCode, RMS_MT.ProcessSequenceNum, SmallClassificationName as ProcessName,StockQuantity12,StockCost12, RowMetarialStorehouseIndex as StorehouseIndex From RMS_MT
								inner join II_MT on II_MT.ItemNum = RMS_MT.ItemNum
								inner join PUC_MT on SmallClassificationCode = RMS_MT.ProcessCode
								where II_MT.RecodingState = 1 and RMS_MT.RecodingState = 1 and PUC_MT.Recodingstate = 1 and [Year] = @year
								and II_MT.ItemNum Like @num and II_MT.ItemDrawNum Like @draw and II_MT.ItemName Like @name order by II_MT.ItemNum";
					else
						str = @"Select P.ItemNum,I.ItemDrawNum, I.ItemName, P.ProcessCode, U.SmallClassificationName as ProcessName,PS.ProcessSequenceNum  ,StockQuantity12, P.ProductionStorehouseIndex as StorehouseIndex
								From PS_MT P
								inner join  II_MT I on P.ItemNum = I.ItemNum
								inner join PUC_MT U on U.SmallClassificationCode = P.ProcessCode
								inner join PSI_MT PS on PS.ItemNum = P.ItemNum and PS.ProcessCode = P.ProcessCode
								where P.RecodingState = 1 and U.RecodingState = 1 and PS.RecodingState = 1 and I.RecodingState = 1
								and PS.ProcessSequenceNum = (Select Max(P1.ProcessSequenceNum) From PSI_MT P1 where PS.ItemNum = P1.ItemNum and P1.RecodingState =1) 
								and [Year] = @year
								and I.ItemNum Like @num and I.ItemDrawNum Like @draw and I.ItemName Like @name order by I.ItemNum";
					break;
				}
				case "AddItemInStorePC": //보용품 입고현황
				{
					str = @"Select * From AS_HT where ItemNum Like @num and ItemDrawNum Like @draw and ItemName Like @name and (InstoreDate >= @begin and InstoreDate <= @end) order by InstoreDate ";
					break;
				}
				case "AddItemReceivePC"://수주현황
				{
					if(m_ProgressState == "미납")
					{
						str = @"select isnull(PUC_MT.SmallClassificationName,'') as ItemState, RO_HT.ItemNum, RO_HT.ItemDrawNum, RO_HT.ItemName, CompanyName, BusinessRegistrationNum, RO_HT.PropertyClassification, 
						case ProductionRequestDivision when 1 then '예' else '아니오' end as ProductionRequestDivision, 
						ReceivingOrderDate, ApplyUnitCost, DeliveryRequestQuantity1, DeliveryRequestDate1, 
						DeliveryRequestQuantity2, DeliveryRequestDate2, DeliveryRequestQuantity3, DeliveryRequestDate3, 
						DeliveryRequestQuantity4, DeliveryRequestDate4, DeliveryRequestQuantity5, DeliveryRequestDate5, 
						TotalReceiveingOrderQuantity, TotalCost, OutStorehouseQuantity, SuitabilityQuantity, 
						UnInspectionQuantity, RemainderQuantity, OrderNum, DeliveryPlace, VolumNum, 
						case ProgressCondition when '대기' then '미납' when '진행' then '미납' when '중단' then '중단' else '완료' end as Progress,	ProgressCondition,
						RO_HT.RegistrationPerson, RO_HT.RegistrationPersonID, RO_HT.RegistrationDate, 
						RO_HT.UpdatingPerson, RO_HT.UpdatingPersonID, RO_HT.UpdatingDate, ReceivingOrderHistoryIndex, P1.SmallClassificationName as ItemClassification1,  P2.SmallClassificationName as ItemClassification2, P3.SmallClassificationName as ItemClassification3, P4.SmallClassificationName as ItemClassification4
						From RO_HT inner join II_MT on RO_HT.ItemNum = II_MT.ItemNum
						left outer join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
						left outer join PUC_MT P1 on II_MT.ItemClassification1 = P1.SmallClassificationCode
						left outer join PUC_MT P2 on II_MT.ItemClassification2 = P2.SmallClassificationCode
						left outer join PUC_MT P3 on II_MT.ItemClassification3 = P3.SmallClassificationCode
						left outer join PUC_MT P4 on II_MT.ItemClassification4 = P4.SmallClassificationCode
						Where (RO_HT.PropertyClassification = '원자재' or RO_HT.PropertyClassification = '반제품') and II_Mt.ItemClassification1 like @Classification1 and II_MT.RecodingState = 1 and RO_HT.ItemNum Like @num and RO_HT.ItemDrawNum Like @draw and RO_HT.ItemName Like @name and CompanyName Like @company and (ProgressCondition = '대기' or ProgressCondition = '진행') and (DeliveryRequestDate1 >= @begin and DeliveryRequestDate1 <= @end) and ItemClassification1 Like @Classification1 order by ReceivingOrderHistoryIndex desc";
					}
					else if(m_ProgressState == "-선 택-")
					{
						str = @"select isnull(PUC_MT.SmallClassificationName,'') as ItemState, RO_HT.ItemNum, RO_HT.ItemDrawNum, RO_HT.ItemName, CompanyName, BusinessRegistrationNum, RO_HT.PropertyClassification, 
						case ProductionRequestDivision when 1 then '예' else '아니오' end as ProductionRequestDivision, 
						ReceivingOrderDate, ApplyUnitCost, DeliveryRequestQuantity1, DeliveryRequestDate1, 
						DeliveryRequestQuantity2, DeliveryRequestDate2, DeliveryRequestQuantity3, DeliveryRequestDate3, 
						DeliveryRequestQuantity4, DeliveryRequestDate4, DeliveryRequestQuantity5, DeliveryRequestDate5, 
						TotalReceiveingOrderQuantity, TotalCost, OutStorehouseQuantity, SuitabilityQuantity, 
						UnInspectionQuantity, RemainderQuantity, OrderNum, DeliveryPlace, VolumNum, 
						case ProgressCondition when '대기' then '미납' when '진행' then '미납' when '중단' then '중단' else '완료' end as Progress,	ProgressCondition,
						RO_HT.RegistrationPerson, RO_HT.RegistrationPersonID, RO_HT.RegistrationDate, 
						RO_HT.UpdatingPerson, RO_HT.UpdatingPersonID, RO_HT.UpdatingDate, ReceivingOrderHistoryIndex, P1.SmallClassificationName as ItemClassification1,  P2.SmallClassificationName as ItemClassification2, P3.SmallClassificationName as ItemClassification3, P4.SmallClassificationName as ItemClassification4
						From RO_HT inner join II_MT on RO_HT.ItemNum = II_MT.ItemNum
						left outer join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
						left outer join PUC_MT P1 on II_MT.ItemClassification1 = P1.SmallClassificationCode
						left outer join PUC_MT P2 on II_MT.ItemClassification2 = P2.SmallClassificationCode
						left outer join PUC_MT P3 on II_MT.ItemClassification3 = P3.SmallClassificationCode
						left outer join PUC_MT P4 on II_MT.ItemClassification4 = P4.SmallClassificationCode
						Where (RO_HT.PropertyClassification = '원자재' or RO_HT.PropertyClassification = '반제품') and II_Mt.ItemClassification1 like @Classification1 and II_MT.RecodingState = 1 and RO_HT.ItemNum Like @num and RO_HT.ItemDrawNum Like @draw and RO_HT.ItemName Like @name and CompanyName Like @company and (DeliveryRequestDate1 >= @begin and DeliveryRequestDate1 <= @end)  order by ReceivingOrderHistoryIndex desc";

					}
					else
					{
						str = @"select isnull(P1.SmallClassificationName,'') as ItemState, RO_HT.ItemNum, RO_HT.ItemDrawNum, RO_HT.ItemName, CompanyName, BusinessRegistrationNum, RO_HT.PropertyClassification, 
						case ProductionRequestDivision when 1 then '예' else '아니오' end as ProductionRequestDivision, 
						ReceivingOrderDate, ApplyUnitCost, DeliveryRequestQuantity1, DeliveryRequestDate1, 
						DeliveryRequestQuantity2, DeliveryRequestDate2, DeliveryRequestQuantity3, DeliveryRequestDate3, 
						DeliveryRequestQuantity4, DeliveryRequestDate4, DeliveryRequestQuantity5, DeliveryRequestDate5, 
						TotalReceiveingOrderQuantity, TotalCost, OutStorehouseQuantity, SuitabilityQuantity, 
						UnInspectionQuantity, RemainderQuantity, OrderNum, DeliveryPlace, VolumNum, 
						case ProgressCondition when '대기' then '미납' when '진행' then '미납' when '중단' then '중단' else '완료' end as Progress,	ProgressCondition,
						RO_HT.RegistrationPerson, RO_HT.RegistrationPersonID, RO_HT.RegistrationDate, 
						RO_HT.UpdatingPerson, RO_HT.UpdatingPersonID, RO_HT.UpdatingDate, ReceivingOrderHistoryIndex
						From RO_HT inner join II_MT on RO_HT.ItemNum = II_MT.ItemNum
						left outer join PUC_MT P1 on II_MT.ItemState = P1.SmallClassificationCode
						Where (RO_HT.PropertyClassification = '원자재' or RO_HT.PropertyClassification = '반제품') and II_Mt.ItemClassification1 like @Classification1 and II_MT.RecodingState = 1 and RO_HT.ItemNum Like @num and RO_HT.ItemDrawNum Like @draw and RO_HT.ItemName Like @name and CompanyName Like @company and ProgressCondition Like @progress and (DeliveryRequestDate1 >= @begin and DeliveryRequestDate1 <= @end)  order by ReceivingOrderHistoryIndex desc";

					}
					break;
				}
				case "SubBuyingOrderPC":
				{
					str = @"Select SBO_HT.*, PropertyClassification From SBO_HT inner join II_MT on SBO_HT.ItemNum = II_MT.ItemNum
						where SBO_HT.ItemNum Like @num and SBO_HT.ItemDrawNum Like @draw and SBO_HT.ItemName Like @name and II_MT.RecodingState = 1
						and ProgressCondition like @progress and (DeliveryDate >= @begin and DeliveryDate <= @end) order by SubBuyingOrderHistoryIndex ";
					break;
				}
				case "SubBuyingDelivery":
				{
					str = @"Select SBO_HT.*, SmallClassificationName as Unit, II_MT.Standard , UCI_MT.StandardUnitCost as UnitCost
							From SBO_HT 
							inner join II_MT on SBO_HT.ItemNum = II_MT.ItemNum  
							inner join PUC_MT on II_MT.Unit = SmallClassificationCode  
							inner join UCI_MT on SBO_HT.ItemNum = UCI_MT.ItemNum and SBO_HT.BusinessRegistrationNum = UCI_MT.BusinessRegistrationNum
						Where (DeliveryRemainQuantity != 0 and DeliveryQuantity != 0) and
						(ProgressCondition != '완료') and  II_MT.RecodingState = 1 and
						UCI_MT.RecodingState =1 and UCI_MT.UnitCostDistinction = '구매단가' and 
						CompanyName Like @company and SBO_HT.ItemNum Like @num and		
						SBO_HT.ItemDrawNum Like @draw and SBO_HT.ItemName Like @name and 
						(DeliveryDate >= @begin and DeliveryDate <= @end) and II_MT.RecodingState = '1'
						 order by SubBuyingOrderHistoryIndex";
					break;
				}
				case "SubBuyingDeliveryPC":
				{
					str = @"Select SBD_HT.*,CheckDistinction, SmallClassificationName as Unit,PropertyClassification, II_MT.Standard 
						From SBD_HT inner join II_MT on SBD_HT.ItemNum = II_MT.ItemNum  inner join PUC_MT on II_MT.Unit = SmallClassificationCode  Where 
						CompanyName Like @company and SBD_HT.ItemNum Like @num and  II_MT.RecodingState = 1 and ItemClassification1 Like @Classification1	and PropertyClassification like @PropertyClassification and
						SBD_HT.ItemDrawNum Like @draw and SBD_HT.ItemName Like @name and  (DeliveryDate >= @begin and DeliveryDate <= @end) and II_MT.RecodingState = '1' order by SubBuyingDeliveryHistoryIndex desc";
				
					break;
				}
				case "RowItemExhaustPC":
				{
					if(m_hdItemNum == "")
					{
						if(m_hdChildItemNum == "")
						{
							if(m_WorkerID == "")
							{
								str = @"Select E_HT.* , PUC.SmallClassificationName as ExhaustUnit, II.Standard as ExhaustStandard,
							PUC1.SmallClassificationName as CreateUnit, I.Standard as CreateStandard 
							From E_HT inner join II_MT II on E_HT.ExhaustItemNum = II.ItemNum
							inner join II_MT I on E_HT.CreateItemNum = I.ItemNum
							inner join PUC_MT PUC on II.Unit = PUC.SmallClassificationCode
							inner join PUC_MT PUC1 on I.Unit = PUC1.SmallClassificationCode
							Where I.RecodingState = 1 and II.RecodingState = 1 and PUC.RecodingState = 1 and PUC1.RecodingState = 1 and
							ExhaustItemNum like @num and ExhaustItemDrawNum like @draw and ExhaustItemName like @name and
							CreateItemNum like @childnum and CreateItemDrawNum like @childdraw and CreateItemName like @childname and 
							(ExhaustDate >= @begin and ExhaustDate <= @end) order by E_HT.RegistrationDate desc ";
							}
							else
							{
								str = @"Select E_HT.* , PUC.SmallClassificationName as ExhaustUnit, II.Standard as ExhaustStandard,
							PUC1.SmallClassificationName as CreateUnit, I.Standard as CreateStandard 
							From E_HT inner join II_MT II on E_HT.ExhaustItemNum = II.ItemNum
							inner join II_MT I on E_HT.CreateItemNum = I.ItemNum
							inner join PUC_MT PUC on II.Unit = PUC.SmallClassificationCode
							inner join PUC_MT PUC1 on I.Unit = PUC1.SmallClassificationCode
							Where I.RecodingState = 1 and II.RecodingState = 1 and PUC.RecodingState = 1 and PUC1.RecodingState = 1 and
							ExhaustItemNum like @num and ExhaustItemDrawNum like @draw and ExhaustItemName like @name and
							CreateItemNum like @childnum and CreateItemDrawNum like @childdraw and CreateItemName like @childname and WorkerID = @WorkerID and
							(ExhaustDate >= @begin and ExhaustDate <= @end)  order by E_HT.RegistrationDate desc  ";
							}
						}
						else
						{
							if(m_WorkerID =="")
							{
								str = @"Select E_HT.* , PUC.SmallClassificationName as ExhaustUnit, II.Standard as ExhaustStandard,
							PUC1.SmallClassificationName as CreateUnit, I.Standard as CreateStandard 
							From E_HT inner join II_MT II on E_HT.ExhaustItemNum = II.ItemNum
							inner join II_MT I on E_HT.CreateItemNum = I.ItemNum
							inner join PUC_MT PUC on II.Unit = PUC.SmallClassificationCode
							inner join PUC_MT PUC1 on I.Unit = PUC1.SmallClassificationCode
							Where I.RecodingState = 1 and II.RecodingState = 1 and PUC.RecodingState = 1 and PUC1.RecodingState = 1 and
							ExhaustItemNum like @num and ExhaustItemDrawNum like @draw and ExhaustItemName like @name and 
							CreateItemNum = @hdchildnum and
							(ExhaustDate >= @begin and ExhaustDate <= @end)   order by E_HT.RegistrationDate desc ";
							}
							else
							{
								str = @"Select E_HT.* , PUC.SmallClassificationName as ExhaustUnit, II.Standard as ExhaustStandard,
							PUC1.SmallClassificationName as CreateUnit, I.Standard as CreateStandard 
							From E_HT inner join II_MT II on E_HT.ExhaustItemNum = II.ItemNum
							inner join II_MT I on E_HT.CreateItemNum = I.ItemNum
							inner join PUC_MT PUC on II.Unit = PUC.SmallClassificationCode
							inner join PUC_MT PUC1 on I.Unit = PUC1.SmallClassificationCode
							Where I.RecodingState = 1 and II.RecodingState = 1 and PUC.RecodingState = 1 and PUC1.RecodingState = 1 and
							ExhaustItemNum like @num and ExhaustItemDrawNum like @draw and ExhaustItemName like @name and WorkerID = @WorkerID and
							CreateItemNum = @hdchildnum and
							(ExhaustDate >= @begin and ExhaustDate <= @end)  order by E_HT.RegistrationDate desc  ";
							}
						}
					}
					else
					{
						if(m_hdChildItemNum == "")
						{
							if(m_WorkerID =="")
							{
								str = @"Select E_HT.* , PUC.SmallClassificationName as ExhaustUnit, II.Standard as ExhaustStandard,
							PUC1.SmallClassificationName as CreateUnit, I.Standard as CreateStandard 
							From E_HT inner join II_MT II on E_HT.ExhaustItemNum = II.ItemNum
							inner join II_MT I on E_HT.CreateItemNum = I.ItemNum
							inner join PUC_MT PUC on II.Unit = PUC.SmallClassificationCode
							inner join PUC_MT PUC1 on I.Unit = PUC1.SmallClassificationCode
							Where I.RecodingState = 1 and II.RecodingState = 1 and PUC.RecodingState = 1 and PUC1.RecodingState = 1 and
							ExhaustItemNum = @hdnum and 
							CreateItemNum like @childnum and CreateItemDrawNum like @childdraw and CreateItemName like @childname and 
							(ExhaustDate >= @begin and ExhaustDate <= @end)  order by E_HT.RegistrationDate desc  ";
							}
							else
							{
								str = @"Select E_HT.* , PUC.SmallClassificationName as ExhaustUnit, II.Standard as ExhaustStandard,
							PUC1.SmallClassificationName as CreateUnit, I.Standard as CreateStandard 
							From E_HT inner join II_MT II on E_HT.ExhaustItemNum = II.ItemNum
							inner join II_MT I on E_HT.CreateItemNum = I.ItemNum
							inner join PUC_MT PUC on II.Unit = PUC.SmallClassificationCode
							inner join PUC_MT PUC1 on I.Unit = PUC1.SmallClassificationCode
							Where I.RecodingState = 1 and II.RecodingState = 1 and PUC.RecodingState = 1 and PUC1.RecodingState = 1 and
							ExhaustItemNum = @hdnum and 
							CreateItemNum like @childnum and CreateItemDrawNum like @childdraw and CreateItemName like @childname and WorkerID = @WorkerID and
							(ExhaustDate >= @begin and ExhaustDate <= @end)  order by E_HT.RegistrationDate desc  ";
							}
						}
						else
						{
							if(m_WorkerID =="")
							{
								str = @"Select E_HT.* , PUC.SmallClassificationName as ExhaustUnit, II.Standard as ExhaustStandard,
							PUC1.SmallClassificationName as CreateUnit, I.Standard as CreateStandard 
							From E_HT inner join II_MT II on E_HT.ExhaustItemNum = II.ItemNum
							inner join II_MT I on E_HT.CreateItemNum = I.ItemNum
							inner join PUC_MT PUC on II.Unit = PUC.SmallClassificationCode
							inner join PUC_MT PUC1 on I.Unit = PUC1.SmallClassificationCode
							Where I.RecodingState = 1 and II.RecodingState = 1 and PUC.RecodingState = 1 and PUC1.RecodingState = 1 and
							ExhaustItemNum = @hdnum and  CreateItemNum = @hdchildnum and (ExhaustDate >= @begin and ExhaustDate <= @end)
							 order by E_HT.RegistrationDate desc   ";
							}
							else
							{
								str = @"Select E_HT.* , PUC.SmallClassificationName as ExhaustUnit, II.Standard as ExhaustStandard,
							PUC1.SmallClassificationName as CreateUnit, I.Standard as CreateStandard 
							From E_HT inner join II_MT II on E_HT.ExhaustItemNum = II.ItemNum
							inner join II_MT I on E_HT.CreateItemNum = I.ItemNum
							inner join PUC_MT PUC on II.Unit = PUC.SmallClassificationCode
							inner join PUC_MT PUC1 on I.Unit = PUC1.SmallClassificationCode
							Where I.RecodingState = 1 and II.RecodingState = 1 and PUC.RecodingState = 1 and PUC1.RecodingState = 1 and WorkerID Like @WorkerID and
							ExhaustItemNum = @hdnum and  CreateItemNum = @hdchildnum and (ExhaustDate >= @begin and ExhaustDate <= @end)   order by E_HT.RegistrationDate desc ";
							}
						}
					}
					
					break;
				}
				case "EtcClaimPC":		//기타공제 등록현황
				{
					if(m_Comnum == "")
					{
						str = @"Select * From ECL_HT Where CompanyName Like @company and (ReceiptDate >= @begin and ReceiptDate <= @end) order by EtcClameHistoryIndex desc";
					}
					else
					{
						str = @"Select * From ECL_HT Where BusinessRegistrationNum = @comnum and (ReceiptDate >= @begin and ReceiptDate <= @end) order by EtcClameHistoryIndex desc";
					}
				}
					break;
				case "UnitCostView":			// 거래처별 단가보기
				{
					if(m_ItemNum == "")
					{
						if(m_Comnum == "")
						{
							str = @"Select UnitCostDistinction, UCI_MT.ItemNum, II_MT.ItemName, Standard, P1.SmallClassificationName as StartProcess, 
							P2.SmallClassificationName as EndProcess, CI_MT.CompanyName , CI_MT.BusinessRegistrationNum, 
							UCI_MT.StandardUnitCost, UCI_MT.OrderRate
							From UCI_MT inner join II_MT on UCI_MT.ItemNum = II_MT.ItemNum
							inner join PUC_MT P1 on UCI_MT.BeginProcessCode = P1.SmallClassificationCode
							inner join PUC_MT P2 on UCI_MT.EndProcessCode = P2.SmallClassificationCode 
							inner join CI_MT on UCI_MT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
							Where UCI_MT.ItemNum Like @num and II_MT.ItemDrawNum like @draw and II_MT.ItemName like @name
							and CI_MT.CompanyName Like @company and UCI_MT.UnitCostDistinction like @Distinction
							and UCI_MT.RecodingState  =1 and CI_MT.RecodingState =1 and P1.RecodingState = 1 and P2.RecodingState = 1 order by UnitCostDistinction, CompanyName, II_MT.ItemNum";					
						}
						else
						{
							str = @"Select UnitCostDistinction, UCI_MT.ItemNum, II_MT.ItemName,Standard, P1.SmallClassificationName as StartProcess, 
							P2.SmallClassificationName as EndProcess, CI_MT.CompanyName , CI_MT.BusinessRegistrationNum, 
							UCI_MT.StandardUnitCost, UCI_MT.OrderRate
							From UCI_MT inner join II_MT on UCI_MT.ItemNum = II_MT.ItemNum
							inner join PUC_MT P1 on UCI_MT.BeginProcessCode = P1.SmallClassificationCode
							inner join PUC_MT P2 on UCI_MT.EndProcessCode = P2.SmallClassificationCode 
							inner join CI_MT on UCI_MT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
							Where UCI_MT.ItemNum Like @num and II_MT.ItemDrawNum like @draw and II_MT.ItemName like @name
							and CI_MT.BusinessRegistrationNum = @comnum and UCI_MT.UnitCostDistinction like @Distinction
							and UCI_MT.RecodingState  =1 and CI_MT.RecodingState =1 and P1.RecodingState = 1 and P2.RecodingState = 1 order by UnitCostDistinction, CompanyName, II_MT.ItemNum";					
						}
					}
					else
					{
						if(m_Comnum == "")
						{
							str = @"Select UnitCostDistinction, UCI_MT.ItemNum, II_MT.ItemName,Standard, P1.SmallClassificationName as StartProcess, 
							P2.SmallClassificationName as EndProcess, CI_MT.CompanyName , CI_MT.BusinessRegistrationNum, 
							UCI_MT.StandardUnitCost, UCI_MT.OrderRate
							From UCI_MT inner join II_MT on UCI_MT.ItemNum = II_MT.ItemNum
							inner join PUC_MT P1 on UCI_MT.BeginProcessCode = P1.SmallClassificationCode
							inner join PUC_MT P2 on UCI_MT.EndProcessCode = P2.SmallClassificationCode 
							inner join CI_MT on UCI_MT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
							Where UCI_MT.ItemNum = @hdnum and CI_MT.CompanyName Like @company and UCI_MT.UnitCostDistinction like @Distinction
							and UCI_MT.RecodingState  =1 and CI_MT.RecodingState =1 and P1.RecodingState = 1 and P2.RecodingState = 1 order by UnitCostDistinction, CompanyName, II_MT.ItemNum";					
						}
						else
						{
							str = @"Select UnitCostDistinction, UCI_MT.ItemNum, II_MT.ItemName,Standard, P1.SmallClassificationName as StartProcess, 
							P2.SmallClassificationName as EndProcess, CI_MT.CompanyName , CI_MT.BusinessRegistrationNum, 
							UCI_MT.StandardUnitCost, UCI_MT.OrderRate
							From UCI_MT inner join II_MT on UCI_MT.ItemNum = II_MT.ItemNum
							inner join PUC_MT P1 on UCI_MT.BeginProcessCode = P1.SmallClassificationCode
							inner join PUC_MT P2 on UCI_MT.EndProcessCode = P2.SmallClassificationCode 
							inner join CI_MT on UCI_MT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
							Where UCI_MT.ItemNum = @hdnum and CI_MT.BusinessRegistrationNum = @comnum and UCI_MT.UnitCostDistinction like @Distinction
							and UCI_MT.RecodingState  =1 and CI_MT.RecodingState =1 and P1.RecodingState = 1 and P2.RecodingState = 1 order by UnitCostDistinction, CompanyName, II_MT.ItemNum";					
						}
				
					}

				}
					break;
				default:
					break;
				
			}


			SqlCommand comm = new SqlCommand(str,conn);

			if(m_ItemNum == null || m_ItemNum == "")
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = "%%";
			else
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = "%"+m_ItemNum+"%";

			if(m_DrawNum == null || m_DrawNum == "")
				comm.Parameters.Add("@draw",SqlDbType.VarChar).Value = "%%";
			else
				comm.Parameters.Add("@draw",SqlDbType.VarChar).Value = "%"+m_DrawNum+"%";

			if(m_ItemName == null || m_ItemName == "")
				comm.Parameters.Add("@name",SqlDbType.VarChar).Value = "%%";
			else
				comm.Parameters.Add("@name",SqlDbType.VarChar).Value = "%"+m_ItemName+"%";

			
			if(m_Com == null || m_Com == "")
				comm.Parameters.Add("@company",SqlDbType.VarChar).Value = "%%";
			else
				comm.Parameters.Add("@company",SqlDbType.VarChar).Value = "%"+m_Com+"%";
			if(m_Comnum == null || m_Comnum == "")
				comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = "%%";
			else
				comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_Comnum;

			if(m_WCName == null || m_WCName == "")
				comm.Parameters.Add("@wcname",SqlDbType.VarChar).Value = "%%";
			else
				comm.Parameters.Add("@wcname",SqlDbType.VarChar).Value = "%"+m_WCName+"%";

			comm.Parameters.Add("@ProcessCode",SqlDbType.VarChar).Value = "%"+m_Process+"%";
			comm.Parameters.Add("@progress",SqlDbType.VarChar).Value = "%"+m_ProgressState+"%";
			comm.Parameters.Add("@source",SqlDbType.VarChar).Value = "%"+m_Source+"%";
			comm.Parameters.Add("@begin",SqlDbType.SmallDateTime).Value = m_BeginDate;
			comm.Parameters.Add("@end",SqlDbType.SmallDateTime).Value = m_EndDate+" 23:59:29";
			comm.Parameters.Add("@from",SqlDbType.SmallDateTime).Value = BeginDate;
			comm.Parameters.Add("@to",SqlDbType.SmallDateTime).Value = EndDate;
			comm.Parameters.Add("@decisionMethod",SqlDbType.VarChar).Value = "%"+m_DecisionMethod+"%";
			
			
			comm.Parameters.Add("@store",SqlDbType.VarChar).Value = "%"+m_Store+"%";
			comm.Parameters.Add("@storenum",SqlDbType.Int).Value = m_StoreNum;
			comm.Parameters.Add("@trade1",SqlDbType.VarChar).Value = m_Classification1;
			comm.Parameters.Add("@trade2",SqlDbType.VarChar).Value = m_Classification2;
			comm.Parameters.Add("@Worker",SqlDbType.VarChar).Value = m_Worker;
			comm.Parameters.Add("@year",DateTime.Now.Year);
			comm.Parameters.Add("@Classification1","%"+m_Classification1+"%");
			comm.Parameters.Add("@Classification2","%"+m_Classification2+"%");
			comm.Parameters.Add("@ordernum",SqlDbType.VarChar).Value = "%"+m_OrderNum+"%";//발주번

			comm.Parameters.Add("@hdnum",m_hdItemNum);
			comm.Parameters.Add("@hdchildnum", m_hdChildItemNum);

			if(m_ChildItemNum == null || m_ChildItemNum == "")
				comm.Parameters.Add("@childnum",SqlDbType.VarChar).Value = "%%";
			else
				comm.Parameters.Add("@childnum",SqlDbType.VarChar).Value = "%"+m_ItemNum+"%";

			if(m_ChildItemDrawNum == null || m_ChildItemDrawNum == "")
				comm.Parameters.Add("@childdraw",SqlDbType.VarChar).Value = "%%";
			else
				comm.Parameters.Add("@childdraw",SqlDbType.VarChar).Value = "%"+m_DrawNum+"%";

			if(m_ChildItemName == null || m_ChildItemName == "")
				comm.Parameters.Add("@childname",SqlDbType.VarChar).Value = "%%";
			else
				comm.Parameters.Add("@childname",SqlDbType.VarChar).Value = "%"+m_ItemName+"%";

			//작업완료일 파라미터
			comm.Parameters.Add("@fromDate",SqlDbType.SmallDateTime).Value = m_fromDate;
			comm.Parameters.Add("@toDate",SqlDbType.SmallDateTime).Value = m_toDate;

			comm.Parameters.Add("@PropertyClassification" , "%"+ m_Property +"%");

			comm.Parameters.Add("@Distinction" , '%' +m_Distinction + "%");
			if(m_WorkerID != "")
			comm.Parameters.Add("@WorkerID",m_WorkerID);
			

			SqlDataAdapter da = new SqlDataAdapter(comm);
            DataSet ds = new DataSet();
			da.Fill(ds);

			return ds;

		}
	}
}
