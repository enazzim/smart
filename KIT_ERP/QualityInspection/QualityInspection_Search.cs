using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;


namespace KIT_ERP.QualityInspection
{
	/// <summary>
	/// 품질검사 모듈의 전 페이지에서 검색버튼을 누를경우 이 클래스를 이용합니다.
	/// </summary>
	public class QualityInspection_Search : QualityInspection_BaseClass
	{

		// 열거형으로 정의되는 현재 페이지의 이름
		private PageNames PageNames;
		private string strItemNumber, strItemName, strItemDrawNum, strCustomerName,strCustomerNum, strStartDate, strEndDate;
		private string strDiv, strProcessName, strItemClassification1, strItemClassification2, strItemClassification3, strItemClassification4 ;
		private SByte sbyteProcessDiv;
		private string strWCinfoindex;
		private string strDivision;

		// 품질검사등록 생성자
		/// <summary>
		/// 품질검사등록 페이지 생성자
		/// </summary>
		/// <param name="PageNames">열거형으로 정의된 페이지 이름</param>
		/// <param name="strItemNumber">품목번호</param>
		/// <param name="strCustomerName">거래처 명</param>
		/// <param name="strStartDate">검색 시작 날짜</param>
		/// <param name="strEndDate">검색 종료 날짜</param>
		public QualityInspection_Search(	 
										  PageNames PageNames
										, string strItemNumber
										, string strItemDrawNum
										, string strItemName
										, string strCustomerName
										, string strStartDate
										, string strEndDate
										)
		{
			this.PageNames = PageNames;
			this.strItemNumber = strItemNumber;
			this.strItemDrawNum = strItemDrawNum;
			this.strItemName = strItemName;
			this.strCustomerName = strCustomerName;
			this.strStartDate = strStartDate;
			this.strEndDate = strEndDate;			
		}



		// 품질검사현황 생성자
		public QualityInspection_Search(	 
										  PageNames PageNames
										, string strItemNumber
										, string strItemDrawNum
										, string strItemName
										, string strStartDate
										, string strEndDate
										, string strCustomerName
										, string strCustomerNum
										, SByte sbyteProcessDiv
										, string strDivision
										)
		{
			this.PageNames = PageNames;
			this.strItemNumber = strItemNumber;
			this.strItemDrawNum = strItemDrawNum;
			this.strItemName = strItemName;
			this.strCustomerName = strCustomerName;
			this.strCustomerNum = strCustomerNum;
			this.strStartDate = strStartDate;
			this.strEndDate = strEndDate;			
			this.sbyteProcessDiv = sbyteProcessDiv;
			this.strDivision = strDivision;
						
		}


		

		/// <summary>
		/// 자주검사현황 , 자주검사지표 생성자
		/// </summary>
		/// <param name="PageNames">열거형으로 정의된 페이지 이름</param>
		/// <param name="strWCinfoindex">작업장 번호가 올수도 있고 작업장 명이 올수도 있다.</param>
		/// <param name="strProcessName"></param>
		/// <param name="strItemName"></param>
		/// <param name="strStartDate"></param>
		/// <param name="strEndDate"></param>
		public QualityInspection_Search(	 
										PageNames PageNames
										, string strWCinfoindex		// 작업장 번호가 올수도 있고 작업장 명이 올수도 있다.
										, string strProcessName
										, string strItemName
										, string strStartDate
										, string strEndDate
									    )
		{
			this.PageNames = PageNames;
			this.strWCinfoindex = strWCinfoindex;
				
			if ( PageNames == PageNames.자주검사현황 )
			{
				this.strProcessName = strProcessName;
				this.strItemName = strItemName;
				this.strStartDate = strStartDate;
				this.strEndDate = strEndDate;
			}
			else
			{
				this.strItemClassification1 = strProcessName;
				this.strItemClassification2 = strItemName;
				this.strItemClassification3 = strStartDate;
				this.strItemClassification4 = strEndDate;	
			}
		}



		/// <summary>
		/// 자주검사통계 페이지 생성자
		/// </summary>
		/// <param name="PageNames">열거형으로 정의된 페이지 이름</param>
		/// <param name="strWCinfoindex"></param>
		/// <param name="strStartDate"></param>
		/// <param name="strEndDate"></param>
		/// <param name="strItemClassification1">품목분류1</param>
		/// <param name="strItemClassification2">품목분류2</param>
		/// <param name="strItemClassification3">품목분류3</param>
		/// <param name="strItemClassification4">품목분류4</param>
		public QualityInspection_Search(
											PageNames PageNames
											, string strWCinfoindex
											, string strStartDate
											, string strEndDate
											, string strItemClassification1
											, string strItemClassification2
											, string strItemClassification3
											, string strItemClassification4
											)
		{
			this.PageNames = PageNames;
			this.strWCinfoindex = strWCinfoindex;
			this.strStartDate = strStartDate;
			this.strEndDate = strEndDate;
			this.strItemClassification1 = strItemClassification1;
			this.strItemClassification2 = strItemClassification2;
			this.strItemClassification3 = strItemClassification3;
			this.strItemClassification4 = strItemClassification4;
		}



		/// <summary>
		/// 품질검사 통계 페이지 생성자
		/// </summary>
		/// <param name="PageNames">열거형으로 정의된 페이지 이름</param>
		/// <param name="strDiv">검색구분</param>
		/// <param name="strStartDate">검색 시작 날짜</param>
		/// <param name="strEndDate">검색 종료 날짜</param>
		public QualityInspection_Search(
										PageNames PageNames
										, string strDiv
										, string strStartDate
										, string strEndDate
										)
		{
			this.PageNames = PageNames;
			this.strStartDate = strStartDate;
			this.strEndDate = strEndDate;	
			this.strDiv = strDiv;
		}

		


		/// <summary>
		/// 부적합 현상지표, 부적합 원인지표 생성자
		/// </summary>
		/// <param name="PageNames">열거형으로 정의된 페이지 이름</param>
		/// <param name="strDiv">검색구분</param>
		public QualityInspection_Search(
										PageNames PageNames
										, string strDiv
										)
		{
			this.PageNames = PageNames;
			this.strDiv = strDiv;
		}



		// 해당객체의 DataSet을 반환
		public DataSet GetDataSet()
		{
			SqlDataAdapter adap = new SqlDataAdapter("", base.GetConnectionString);
			adap.SelectCommand.CommandType = CommandType.StoredProcedure;

			DataSet ds = new DataSet();

			if ( PageNames == PageNames.품질검사등록 )
			{
				adap.SelectCommand.CommandText = "QualityInspectionRegistration_Search";
				adap.SelectCommand.Parameters.Add("@품목번호", SqlDbType.VarChar,50).Value = strItemNumber;
				adap.SelectCommand.Parameters.Add("@도면번호", SqlDbType.VarChar,50).Value = strItemDrawNum;
				adap.SelectCommand.Parameters.Add("@품목명", SqlDbType.VarChar,50).Value = strItemName;
				adap.SelectCommand.Parameters.Add("@시작일자", SqlDbType.VarChar,50).Value = strStartDate;
				adap.SelectCommand.Parameters.Add("@종료일자", SqlDbType.VarChar,50).Value = strEndDate;
				adap.SelectCommand.Parameters.Add("@거래처명", SqlDbType.VarChar,30).Value = strCustomerName;				
			}
			else if ( PageNames == PageNames.품질검사현황 )
			{
				adap.SelectCommand.CommandText = "QualityInspectionPC_Search";
				adap.SelectCommand.Parameters.Add("@품목번호", SqlDbType.VarChar,50).Value = strItemNumber;
				adap.SelectCommand.Parameters.Add("@도면번호", SqlDbType.VarChar,50).Value = strItemDrawNum;
				adap.SelectCommand.Parameters.Add("@품목명", SqlDbType.VarChar,50).Value = strItemName;
				adap.SelectCommand.Parameters.Add("@시작일자", SqlDbType.VarChar,50).Value = strStartDate;
				adap.SelectCommand.Parameters.Add("@종료일자", SqlDbType.VarChar,50).Value = strEndDate;
				adap.SelectCommand.Parameters.Add("@거래처명", SqlDbType.VarChar,30).Value = strCustomerName;		
				adap.SelectCommand.Parameters.Add("@거래처번호", SqlDbType.VarChar,30).Value = strCustomerNum;		
				adap.SelectCommand.Parameters.Add("@진행상태", SqlDbType.Char).Value = sbyteProcessDiv;
				adap.SelectCommand.Parameters.Add("@구분", SqlDbType.VarChar,30).Value = strDivision;		
			}
			else if ( PageNames == PageNames.품질검사통계 )
			{
				adap.SelectCommand.CommandText="QualityInspectionStatistics_Search";
				adap.SelectCommand.Parameters.Add("@div", SqlDbType.VarChar,1).Value = strDiv;
				adap.SelectCommand.Parameters.Add("@startDate", SqlDbType.VarChar,15).Value = strStartDate;
				adap.SelectCommand.Parameters.Add("@endDate", SqlDbType.VarChar,15).Value = strEndDate;				
			}
			else if ( PageNames == PageNames.부적합현상지표 )
			{
				adap.SelectCommand.CommandText = "IncongruityPresentIndex_Search";
				adap.SelectCommand.Parameters.Add("@div", SqlDbType.VarChar,1).Value = strDiv;			
			}
			else if ( PageNames == PageNames.부적합원인지표)
			{
				adap.SelectCommand.CommandText = "IncongruityCauseIndex_Search";
				adap.SelectCommand.Parameters.Add("@div", SqlDbType.VarChar,1).Value = strDiv;
			}
			else if ( PageNames == PageNames.자주검사현황)
			{
				adap.SelectCommand.CommandText = "AutonomyInspectPC_Search";

				if ( strWCinfoindex != "" )
				{
					try
					{
						adap.SelectCommand.Parameters.Add("@WCinfoindex", SqlDbType.SmallInt).Value = Convert.ToInt16(strWCinfoindex) ;
					}
					catch
					{
						adap.SelectCommand.Parameters.Add("@WCName_arg", SqlDbType.VarChar, 30).Value = strWCinfoindex;
					}
				}
				
				adap.SelectCommand.Parameters.Add("@processName" , SqlDbType.VarChar ,  20).Value = strProcessName;
				adap.SelectCommand.Parameters.Add("@itemName" ,  SqlDbType.VarChar ,  20).Value = strItemName;
				adap.SelectCommand.Parameters.Add("@startDate" ,SqlDbType.VarChar, 11).Value = strStartDate;
				adap.SelectCommand.Parameters.Add("@endDate" ,SqlDbType.VarChar, 11).Value = strEndDate;
			}
			else if ( PageNames == PageNames.자주검사통계 )
			{
				adap.SelectCommand.CommandText = "AutonomyInspectStatistics_Search";

				if ( strWCinfoindex != "" )
				{
					try
					{
						adap.SelectCommand.Parameters.Add("@WCinfoindex", SqlDbType.SmallInt).Value = Convert.ToInt16(strWCinfoindex) ;
					}
					catch
					{
						adap.SelectCommand.Parameters.Add("@WCName_arg", SqlDbType.VarChar, 30).Value = strWCinfoindex;
					}
				}

				adap.SelectCommand.Parameters.Add("@ItemClassification1", SqlDbType.VarChar, 15).Value = strItemClassification1 ;
				adap.SelectCommand.Parameters.Add("@ItemClassification2", SqlDbType.VarChar, 15).Value = strItemClassification2 ;
				adap.SelectCommand.Parameters.Add("@ItemClassification3", SqlDbType.VarChar, 15).Value = strItemClassification3 ;
				adap.SelectCommand.Parameters.Add("@ItemClassification4", SqlDbType.VarChar, 15).Value = strItemClassification4 ;
				adap.SelectCommand.Parameters.Add("@startDate", SqlDbType.VarChar, 12).Value = strStartDate ;
				adap.SelectCommand.Parameters.Add("@endDate", SqlDbType.VarChar, 12).Value = strEndDate ;
			}
			else if ( PageNames == PageNames.자주검사지표 )
			{
				adap.SelectCommand.CommandText = "AutonomyInspectIndex_Search";

				if ( strWCinfoindex != "" )
				{
					try
					{
						adap.SelectCommand.Parameters.Add("@WCinfoindex", SqlDbType.SmallInt).Value = Convert.ToInt16(strWCinfoindex) ;
					}
					catch
					{
						adap.SelectCommand.Parameters.Add("@WCName_arg", SqlDbType.VarChar, 30).Value = strWCinfoindex;
					}
				}
				
				adap.SelectCommand.Parameters.Add("@ItemClassification1", SqlDbType.VarChar, 15).Value = strItemClassification1 ;
				adap.SelectCommand.Parameters.Add("@ItemClassification2", SqlDbType.VarChar, 15).Value = strItemClassification2 ;
				adap.SelectCommand.Parameters.Add("@ItemClassification3", SqlDbType.VarChar, 15).Value = strItemClassification3 ;
				adap.SelectCommand.Parameters.Add("@ItemClassification4", SqlDbType.VarChar, 15).Value = strItemClassification4 ;
			}

			adap.Fill( ds );
			return ds;
		}
	}
}
