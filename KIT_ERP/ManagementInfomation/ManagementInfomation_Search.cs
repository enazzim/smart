using System;
using System.Data;
using System.Data.SqlClient;

namespace KIT_ERP.ManagementInfomation
{

	/// <summary>
	/// 품질경영관리(ManagementInfomation) 모듈의 전 페이지에서 검색시 본 클래스를 이용한다.
	/// </summary>
	public class  ManagementInfomation_Search : KIT_ERP.QualityInspection.QualityInspection_BaseClass
	{
		// 본 클래스는 QualityInspection 모듈과 동일한 기능을 수행하므로 KIT_ERP.QualityInspection.QualityInspection_BaseClass 를 
		// 상속받아 제작 되었음.

		PageNames Pagenames;
		string strReceiveingOrderCompanyName;	// 수주거래처
		string strBusinessRegistrationNum;
		string strItemClassification1;
		string strItemClassification2;
		string strItemClassification3;
		string strItemClassification4;
		string strTradeClassification1;
		string strBuyingOrderCompanyName;
		string strBuyingDiv;		// 매입형태
		string strWcName;
		bool webComboSelected;	// 웹콤보 선택여부
		string strDIV;

		public ManagementInfomation_Search()
		{
		}

		/// <summary>
		/// 사업계획실적지표, 매출지표, 금액기준지표, 수량기준지표, 로트기준지표 공용 생성자.
		/// </summary>
		/// <param name="Pagenames">열거형으로 정의된 페이지 이름</param>
		/// <param name="strItemClassification1">품목분류1</param>
		/// <param name="strItemClassification2">품목분류2</param>
		/// <param name="strItemClassification3">품목분류3</param>
		/// <param name="strItemClassification4">품목분류4</param>
		/// <param name="strBusinessRegistrationNum">거래처의 사업자등록번호</param>
		/// <param name="strCompanyName">거래처명</param>
		public ManagementInfomation_Search
			( PageNames Pagenames, string strItemClassification1, string strItemClassification2, string strItemClassification3, string strItemClassification4,
			string strBusinessRegistrationNum ,string strCompanyName)
		{
			this.Pagenames = Pagenames;
			if ( Pagenames == PageNames.사업계획실적지표)	
			{
				this.strItemClassification1 = strItemClassification1;
				this.strItemClassification2 = strItemClassification2;
				this.strItemClassification3 = strItemClassification3;
				this.strItemClassification4 = strItemClassification4;
				
				this.strBusinessRegistrationNum = strBusinessRegistrationNum;
				this.strReceiveingOrderCompanyName = strCompanyName;
			}
			else if ( Pagenames == PageNames.금액기준지표 || Pagenames == PageNames.수량기준지표 
				|| Pagenames == PageNames.로트기준지표)
			{
				this.strItemClassification1 = strItemClassification1;
				this.strItemClassification2 = strItemClassification2;
				this.strItemClassification3 = strItemClassification3;
				this.strItemClassification4 = strItemClassification4;

				this.strDIV = strBusinessRegistrationNum;
				this.strReceiveingOrderCompanyName = strCompanyName;
			}
			else if(Pagenames == PageNames.매출지표)
			{
				this.strItemClassification1 = strItemClassification1;
				this.strItemClassification2 = strItemClassification2;
				this.strItemClassification3 = strItemClassification3;
				this.strItemClassification4 = strItemClassification4;
				
				this.strBusinessRegistrationNum = strBusinessRegistrationNum;
				this.strReceiveingOrderCompanyName = strCompanyName;
			}
		}

		/// <summary>
		/// 매입지표 전용 생성자.
		/// </summary>
		/// <param name="Pagenames">열거형으로 정의된 페이지 이름</param>
		/// <param name="strItemClassification1">품목분류1</param>
		/// <param name="strItemClassification2">품목분류2</param>
		/// <param name="strItemClassification3">품목분류3</param>
		/// <param name="strItemClassification4">품목분류4</param>
		/// <param name="strBusinessRegistrationNum">거래처의 사업자등록번호</param>
		/// <param name="strCompanyName">거래처명</param>
		/// <param name="strBuyingDiv">매입구분</param>
		public ManagementInfomation_Search
			( PageNames Pagenames, string strItemClassification1, string strItemClassification2, string strItemClassification3, string strItemClassification4,
			string strBusinessRegistrationNum ,string strCompanyName, string strBuyingDiv)
		{
			this.Pagenames = Pagenames;
			if ( Pagenames == PageNames.매입지표)	
			{
				this.strItemClassification1 = strItemClassification1;
				this.strItemClassification2 = strItemClassification2;
				this.strItemClassification3 = strItemClassification3;
				this.strItemClassification4 = strItemClassification4;
				this.strBuyingDiv = strBuyingDiv;

				this.strBusinessRegistrationNum = strBusinessRegistrationNum;
				this.strBuyingOrderCompanyName = strCompanyName;
			}
			
		}

		/// <summary>
		/// 매출 총 이익지표 전용 생성자.
		/// </summary>
		/// <param name="Pagenames">열거형으로 정의된 페이지 이름</param>
		/// <param name="strItemClassification1">품목분류1</param>
		/// <param name="strItemClassification2">품목분류2</param>
		/// <param name="strItemClassification3">품목분류3</param>
		/// <param name="strItemClassification4">품목분류4</param>
		public ManagementInfomation_Search( PageNames Pagenames, string strItemClassification1, string strItemClassification2, 
			string strItemClassification3, string strItemClassification4 )
		{
			this.Pagenames = Pagenames;

			if ( Pagenames == PageNames.매출총이익지표)	
			{
				this.strItemClassification1 = strItemClassification1;
				this.strItemClassification2 = strItemClassification2;
				this.strItemClassification3 = strItemClassification3;
				this.strItemClassification4 = strItemClassification4;
			}
		}

		/// <summary>
		/// 가동률지표, 비가동지표, 생산성지표 공용 생성자.
		/// </summary>
		/// <param name="Pagenames">열거형으로 정의된 페이지 이름</param>
		/// <param name="strWcName">작업장 명</param>
		/// <param name="webComboSelected">해당 페이지에서 웹콤보의 선택 여부</param>
		public ManagementInfomation_Search(PageNames Pagenames, string strWcName, bool webComboSelected)
		{
			this.webComboSelected = webComboSelected;
			this.Pagenames = Pagenames;
			
			if ( Pagenames == PageNames.가동률지표 || Pagenames == PageNames.비가동지표 || Pagenames == PageNames.생산성지표)
			{
				this.strWcName = strWcName;
			}
		
		}

		/// <summary>
		/// 데이터를 돌려준다.
		/// </summary>
		/// <returns>DataSet 데이터</returns>
		public DataSet GetDataSet()
		{
			SqlDataAdapter adap = new SqlDataAdapter("", base.GetConnectionString);
			adap.SelectCommand.CommandType = CommandType.StoredProcedure;
			DataSet ds = new DataSet();
			
			// 각 페이지에 맞는 Stored Procedure 를 실행한다.
			if ( this.Pagenames == PageNames.사업계획실적지표)	
			{
				adap.SelectCommand.CommandText = "BizPlanResultIndex_Search";
				adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum", SqlDbType.VarChar, 30).Value = this.strBusinessRegistrationNum;
				adap.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar, 50).Value = this.strReceiveingOrderCompanyName;
				adap.SelectCommand.Parameters.Add("@ItemClassification1", SqlDbType.VarChar, 15).Value = this.strItemClassification1;
				adap.SelectCommand.Parameters.Add("@ItemClassification2", SqlDbType.VarChar, 15).Value = this.strItemClassification2;
				adap.SelectCommand.Parameters.Add("@ItemClassification3", SqlDbType.VarChar, 15).Value = this.strItemClassification3;
				adap.SelectCommand.Parameters.Add("@ItemClassification4", SqlDbType.VarChar, 15).Value = this.strItemClassification4;
			}
			else if (  this.Pagenames == PageNames.매출총이익지표 )
			{
				adap.SelectCommand.CommandText = "SaleTatalProfitIndex_Search";
				adap.SelectCommand.Parameters.Add("@ItemClassification1", SqlDbType.VarChar, 15).Value = this.strItemClassification1;
				adap.SelectCommand.Parameters.Add("@ItemClassification2", SqlDbType.VarChar, 15).Value = this.strItemClassification2;
				adap.SelectCommand.Parameters.Add("@ItemClassification3", SqlDbType.VarChar, 15).Value = this.strItemClassification3;
				adap.SelectCommand.Parameters.Add("@ItemClassification4", SqlDbType.VarChar, 15).Value = this.strItemClassification4;
			}
			else if (  this.Pagenames == PageNames.매출지표 )
			{
				adap.SelectCommand.CommandText = "SaleIndex_Search";
				adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum", SqlDbType.VarChar, 30).Value = this.strBusinessRegistrationNum;
				adap.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar, 50).Value = this.strReceiveingOrderCompanyName;
				adap.SelectCommand.Parameters.Add("@ItemClassification1", SqlDbType.VarChar, 15).Value = this.strItemClassification1;
				adap.SelectCommand.Parameters.Add("@ItemClassification2", SqlDbType.VarChar, 15).Value = this.strItemClassification2;
				adap.SelectCommand.Parameters.Add("@ItemClassification3", SqlDbType.VarChar, 15).Value = this.strItemClassification3;
				adap.SelectCommand.Parameters.Add("@ItemClassification4", SqlDbType.VarChar, 15).Value = this.strItemClassification4;
			}
			else if ( this.Pagenames == PageNames.매입지표 )
			{

				adap.SelectCommand.CommandText = "BuyingIndex_Search";
				adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum", SqlDbType.VarChar, 30).Value = this.strBusinessRegistrationNum;
				adap.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar, 50).Value = this.strReceiveingOrderCompanyName;
				adap.SelectCommand.Parameters.Add("@ItemClassification1", SqlDbType.VarChar, 15).Value = this.strItemClassification1;
				adap.SelectCommand.Parameters.Add("@ItemClassification2", SqlDbType.VarChar, 15).Value = this.strItemClassification2;
				adap.SelectCommand.Parameters.Add("@ItemClassification3", SqlDbType.VarChar, 15).Value = this.strItemClassification3;
				adap.SelectCommand.Parameters.Add("@ItemClassification4", SqlDbType.VarChar, 15).Value = this.strItemClassification4;
				adap.SelectCommand.Parameters.Add("@BuyDiv", SqlDbType.VarChar, 10).Value = this.strBuyingDiv;
				
			}
			else if ( this.Pagenames == PageNames.가동률지표)
			{
				adap.SelectCommand.CommandText = "WorkingRatioIndex_Search";
				adap.SelectCommand.Parameters.Add("@WCname", SqlDbType.VarChar, 20).Value = this.strWcName;
			}
			else if ( Pagenames == PageNames.비가동지표 )
			{
				adap.SelectCommand.CommandText = "NonWorkingRatioIndex_Search";
				if ( webComboSelected )
				{
					adap.SelectCommand.Parameters.Add("@wcinfoindex", SqlDbType.Int).Value = this.strWcName;			
				}
				else
				{
					adap.SelectCommand.Parameters.Add("@WCname", SqlDbType.VarChar, 20).Value = this.strWcName;			
				}
			}
			else if ( Pagenames == PageNames.생산성지표 )
			{
				adap.SelectCommand.CommandText = "ProductivityIndex_Search";

				if ( webComboSelected )
				{
					adap.SelectCommand.Parameters.Add("@wcinfoindex", SqlDbType.Int).Value = this.strWcName;			
				}
				else
				{
					adap.SelectCommand.Parameters.Add("@WCname", SqlDbType.VarChar, 20).Value = this.strWcName;			
				}
			}
			else if ( Pagenames == PageNames.금액기준지표 )
			{
				adap.SelectCommand.CommandText = "MoneyStandardIndex_Search";
				adap.SelectCommand.Parameters.Add("@companyname", SqlDbType.VarChar, 20).Value = this.strReceiveingOrderCompanyName;			
				adap.SelectCommand.Parameters.Add("@ItemClassification1", SqlDbType.VarChar, 15).Value = this.strItemClassification1;
				adap.SelectCommand.Parameters.Add("@ItemClassification2", SqlDbType.VarChar, 15).Value = this.strItemClassification2;
				adap.SelectCommand.Parameters.Add("@ItemClassification3", SqlDbType.VarChar, 15).Value = this.strItemClassification3;
				adap.SelectCommand.Parameters.Add("@ItemClassification4", SqlDbType.VarChar, 15).Value = this.strItemClassification4;
				adap.SelectCommand.Parameters.Add("@div", SqlDbType.VarChar, 20).Value = this.strDIV;
			}
			else if ( Pagenames == PageNames.수량기준지표 )
			{
				adap.SelectCommand.CommandText = "QuantityStandardIndex_Search";
				adap.SelectCommand.Parameters.Add("@companyname", SqlDbType.VarChar, 20).Value = this.strReceiveingOrderCompanyName;			
				adap.SelectCommand.Parameters.Add("@ItemClassification1", SqlDbType.VarChar, 15).Value = this.strItemClassification1;
				adap.SelectCommand.Parameters.Add("@ItemClassification2", SqlDbType.VarChar, 15).Value = this.strItemClassification2;
				adap.SelectCommand.Parameters.Add("@ItemClassification3", SqlDbType.VarChar, 15).Value = this.strItemClassification3;
				adap.SelectCommand.Parameters.Add("@ItemClassification4", SqlDbType.VarChar, 15).Value = this.strItemClassification4;
				adap.SelectCommand.Parameters.Add("@div", SqlDbType.VarChar, 20).Value = this.strDIV;
			}
			else if ( Pagenames == PageNames.로트기준지표 )
			{
				adap.SelectCommand.CommandText = "LotStandardIndex_Search";
				adap.SelectCommand.Parameters.Add("@companyname", SqlDbType.VarChar, 20).Value = this.strReceiveingOrderCompanyName;			
				adap.SelectCommand.Parameters.Add("@ItemClassification1", SqlDbType.VarChar, 15).Value = this.strItemClassification1;
				adap.SelectCommand.Parameters.Add("@ItemClassification2", SqlDbType.VarChar, 15).Value = this.strItemClassification2;
				adap.SelectCommand.Parameters.Add("@ItemClassification3", SqlDbType.VarChar, 15).Value = this.strItemClassification3;
				adap.SelectCommand.Parameters.Add("@ItemClassification4", SqlDbType.VarChar, 15).Value = this.strItemClassification4;
				adap.SelectCommand.Parameters.Add("@div", SqlDbType.VarChar, 20).Value = this.strDIV;
			}

			adap.Fill(ds);
			return ds;
		}
	}
}
