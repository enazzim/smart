//
//		종류 - 공통 클래스(기본 클래스)
//		이름 - QualityInspection_BaseClass
//		부모클래스상속 - System.Web.UI.Page
//		용도 - 품질검사 모듈과 경영정보 모듈에서 사용됨 
//
//		구성맴버변수 - 없음
//		구성매서드 - 		
//		품질 PPM 검사 , 연결문자열 받아내기 
//		, 페이지에 자바스크립트 출력 , 화면에 자바스크립트 alert() 화면 출력
//		, 월마감 여부확인 , 클래스 외부에 페이지를 식별하는 enum 
//
using System;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Win32;		// 연결문자열 레지스트리에서 받아오기 위한 클래스 네임 스페이스

public enum PageNames
{
	품질검사등록 ,
	품질검사현황,
	품질검사통계,
	부적합현상지표,
	부적합원인지표,
	자주검사현황,
	자주검사통계,
	자주검사지표,
	사업계획실적지표,
	매출총이익지표,
	매출지표,
	매입지표,
	품목별재고지표,
	창고별재고지표,
	재고금액지표,
	가동률지표,
	비가동지표,
	생산성지표,
	금액기준지표,
	수량기준지표,
	로트기준지표
}

namespace KIT_ERP.QualityInspection
{
	public class QualityInspection_BaseClass : System.Web.UI.Page
	{
		public QualityInspection_BaseClass()
		{}


		/// <summary>
		///  부적합수량과 전체수량을 입력하면 PPM을 계산하여 돌려준다.
		/// </summary>
		/// <param name="de_IncongruityQuantity">부적합 수량</param>
		/// <param name="de_TotalQuantity">전체 수량</param>
		/// <returns>계산된 PPM</returns>
		protected decimal CalculatePPM( decimal de_IncongruityQuantity, decimal de_TotalQuantity )
		{
			if ( de_TotalQuantity != 0 )
				return (de_IncongruityQuantity / de_TotalQuantity) * 1000000.00m;
			else
				return 0.00m;
		}
		

		/// <summary>
		/// 연결문자열 받아오기
		/// </summary>
		protected string GetConnectionString
		{
			
			get	
			{
				return ConfigurationSettings.AppSettings["DSN"];
			}
		}


		/// <summary>
		/// 자바스크립트를 출력
		/// </summary>
		/// <param name="strMessage">화면에 출력될 자바스크립트 소스</param>
		protected void ResponseWrite_Script(string strMessage)
		{
			HttpContext hc = HttpContext.Current;
			hc.Response.Write("<script>" + strMessage + "</script>");
		}


		/// <summary>
		/// 화면에 alert 스크립트 출력하기
		/// </summary>
		/// <param name="Message">화면에 출력될 메세지</param>
		protected void Alert(string Message)
		{
			HttpContext hc = HttpContext.Current;
			hc.Response.Write("<script> alert(\"" + Message + "\")</script>");
		}


		/// <summary>
		/// 월마감 여부를 확인하는 메소드
		/// </summary>
		/// <returns></returns>
		public bool MonthClosing()
		{
			
			return true;
			
		}


	}
}
