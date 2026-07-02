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
using Infragistics.WebUI.UltraWebGrid;

namespace KIT_ERP
{
	/// <summary>
	/// VideoButton에 대한 요약 설명입니다.
	/// </summary>
	public class VideoButton
	{
		private UltraWebGrid m_Grid;
		private string m_PageName;
		private string m_Vol;
		private string m_Table;

		/// <summary>
		/// 비디오버튼 생성자
		/// </summary>
		/// <param name="grid">해당그리드</param>
		/// <param name="pagename">해당페이지</param>
		/// <param name="vol">볼륨번호</param>
		public VideoButton(UltraWebGrid grid, string pagename, string vol)
		{
			m_Grid = grid;
			if(vol == "")
				m_Vol = "0";
			else
				m_Vol = vol;
			m_PageName = pagename;
			switch(m_PageName)
			{
				case "ReceiveingPC":	//수주현황
					m_Table = "RO_HT";
					break;
				case "ProductionRequestPC":	//생산의뢰
					m_Table = "PR_HT";
					break;
				case "GoodsBuyingRequestPC":	//상품구매의뢰
					m_Table = "BR_HT";
					break;
					
				case "RowMaterialRequriementPC":	//원자재구매의뢰현황
					m_Table = "BR_HT";
					break;
				case "ProductionPlanPC":	//생산계획현황
					m_Table = "PP_HT";
					break;
				case "RowMaterialRequirementCalculatePC":	//자재소요산출현황
					m_Table = "PP_HT";
					break;
				case "WCOrder":	//작업지시
					m_Table = "WDWP_HT";
					break;
				case "OutsideRequestPC":	//외주의뢰현황
					m_Table = "OOR_HT";
					break;
				case "BuyingOrderPC":	//구매발주현황
					m_Table = "BO_HT";
					break;
				case "OutSideOrderPC":	//외주발주현황
					m_Table = "OO_HT";
					break;
				case "WCPlanPC":	//작업계획 현황
					m_Table = "WDWP_HT";
					break;
				default :
					break;
			}
			
		}


		public DataSet FindVolum()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "";

			switch(m_PageName)
			{
				case "ReceiveingPC":
					str =  @"select  SmallClassificationName as ItemState, RO_HT.ItemNum, RO_HT.ItemDrawNum, RO_HT.ItemName, CompanyName, BusinessRegistrationNum, RO_HT.PropertyClassification, 
						case ProductionRequestDivision when 1 then '예' else '아니오' end as ProductionRequestDivision, 
						ReceivingOrderDate, ApplyUnitCost, DeliveryRequestQuantity1, DeliveryRequestDate1, 
						DeliveryRequestQuantity2, DeliveryRequestDate2, DeliveryRequestQuantity3, DeliveryRequestDate3, 
						DeliveryRequestQuantity4, DeliveryRequestDate4, DeliveryRequestQuantity5, DeliveryRequestDate5, 
						TotalReceiveingOrderQuantity, TotalCost, OutStorehouseQuantity, SuitabilityQuantity, 
						UnInspectionQuantity, RemainderQuantity, OrderNum, DeliveryPlace, VolumNum, 
						ProgressCondition, RO_HT.RegistrationPerson, RO_HT.RegistrationPersonID, RO_HT.RegistrationDate, 
						RO_HT.UpdatingPerson, RO_HT.UpdatingPersonID, RO_HT.UpdatingDate, ReceivingOrderHistoryIndex 
						From RO_HT inner join II_MT on RO_HT.ItemNum = II_MT.ItemNum
						inner join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
						Where II_MT.RecodingState = 1 and VolumNum = @num";
					break;
				case "GoodsBuyingRequestPC":
					str = "Select * From "+m_Table.ToString()+" Where VolumNum = @num and PropertyClassification = '상품'";
					break;
				case "RowMaterialRequriementPC":
					//str = "Select BR_HT.*, SmallClassificationName as Unit, II_MT.Standard  From "+m_Table.ToString()+" inner join II_MT on BR_HT.ItemNum = II_MT.ItemNum inner join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode Where VolumNum = @num and BR_HT.PropertyClassification = '원자재' and II_MT.RecodingState = 1";
					str = @"SELECT BR_HT.ItemNum, BR_HT.ItemDrawNum, BR_HT.ItemName, SmallClassificationName as Unit, CI_MT.CompanyName, StockQuantity12 as NowStockQuantity, II_MT.Standard, BR_HT.PropertyClassification, BuyingRequestSourceCode, BuyingRequestSource, FirstDeliveryDemandQuantity, FirstDeliveryDemandDate, SecondDeliveryDemandQuantity, SecondDeliveryDemandDate, ThirdDeliveryDemandQuantity, ThirdDeliveryDemandDate, FourthDeliveryDemandQuantity, FourthDeliveryDemandDate, FifthDeliveryDemandQuantity, FifthDeliveryDemandDate, OrderQuantity, UCI_MT.StandardUnitCost as ApplyUnitCost,(UCI_MT.StandardUnitCost * FirstDeliveryDemandQuantity) as TotalCost, VolumNum, ProgressCondition, BR_HT.RegistrationPerson, BR_HT.RegistrationPersonID, BR_HT.RegistrationDate, BR_HT.UpdatingPerson, BR_HT.UpdatingPersonID, BR_HT.UpdatingDate, HistoryIndex, HistorySection, BuyingRequestHistoryIndex FROM BR_HT
				Inner join II_MT on BR_HT.ItemNum = II_MT.ItemNum 				
				inner join UCI_MT on II_MT.ItemNum = UCI_MT.ItemNum
				inner join CI_MT on UCI_MT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
				Inner join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode
				inner join RMS_MT on II_MT.ItemNum = RMS_Mt.ItemNum
				WHERE  RMS_MT.RecodingState = 1 and  PUC_MT.RecodingState =1 and CI_MT.RecodingState = 1 and BR_HT.PropertyClassification = '원자재' and  II_MT.RecodingState = 1 and CI_MT.RecodingState =1 and UCI_MT.RecodingState = 1 and UCI_MT.UnitCostDistinction = '구매단가' 
				and VolumNum = @num	and [Year] = @year		";
					break;
				case "RowMaterialRequirementCalculatePC":
					//str = "Select * From "+m_Table.ToString()+" Where MaterialRequirementVolumNum = @num";
					str = @"Select PP_HT.ItemNum, PP_HT.ItemDrawNum, PP_HT.ItemName, ProductionPlanHistorySourceCode, 
						ProductionPlanHistorySource, ProductionPlanQuantity, ProductionBeginDate, 
						case RowMaterialCalculation when 1 then '산출' else '미산출' end as RowMaterialCalculation, 
						VolumNum, MaterialRequirementVolumNum,ProgressCondition, PP_HT.RegistrationPerson, PP_HT.RegistrationPersonID, 
						PP_HT.RegistrationDate, PP_HT.UpdatingPerson, PP_HT.UpdatingPersonID, PP_HT.UpdatingDate, 
						HistoryIndex, HistorySection, ProductionPlanHistoryIndex 
						From PP_HT 
						Where MaterialRequirementVolumNum = @num";
					break;
				case "ProductionPlanPC":
					str = @"Select SmallClassificationName as ItemState, PP_HT.ItemNum, PP_HT.ItemDrawNum, PP_HT.ItemName, ProductionPlanHistorySourceCode,
						ProductionPlanHistorySource, ProductionPlanQuantity, ProductionBeginDate,
						case RowMaterialCalculation when 1 then '산출' else '미산출' end as RowMaterialCalculation,
						VolumNum, ProgressCondition, PP_HT.RegistrationPerson, PP_HT.RegistrationPersonID,
						PP_HT.RegistrationDate, PP_HT.UpdatingPerson, PP_HT.UpdatingPersonID, PP_HT.UpdatingDate,
						HistoryIndex, HistorySection, ProductionPlanHistoryIndex 
						From PP_HT inner join II_MT on PP_HT.ItemNum = II_MT.ItemNum 
						inner join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
						Where II_MT.RecodingState = 1 and VolumNum = @num";
					break;
				case "BuyingOrderPC":
					str = @"Select BO_HT.*,SmallClassificationName as Unit, II_MT.Standard 
						From BO_HT inner join II_MT on BO_HT.ItemNum = II_MT.ItemNum
						inner join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode
						Where II_MT.RecodingState = 1 and BO_HT.VolumNum = @num";
					break;
				case "ProductionRequestPC":
					str = @"Select SmallClassificationName as ItemState, PR_HT.* 
						From PR_HT inner join II_MT on PR_HT.ItemNum = II_MT.ItemNum 
						inner join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
						Where II_MT.RecodingState = 1 and VolumNum = @num";
					break;
				case "OutsideRequestPC":
					str = @"Select OOR_HT.*, SmallClassificationName as Unit
				From OOR_HT
				inner join II_MT on OOR_HT.ItemNum = II_MT.ItemNum
				inner join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode				
				WHERE II_MT.RecodingState = 1 and VolumNum = @num	order by OutSideOrderRequestHistoryIndex desc";
					break;
				case "OutSideOrderPC":
					str = @"Select OO_HT.*, SmallClassificationName as Unit
				From OO_HT
				inner join II_MT on OO_HT.ItemNum = II_MT.ItemNum
				inner join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode				
				WHERE II_MT.RecodingState = 1 and VolumNum = @num	order by OutSideOrderHistoryIndex desc";
					break;
				default:
					str = "Select * From "+m_Table.ToString()+" Where VolumNum = @num";
					break;
			}
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.Int).Value = int.Parse(m_Vol.ToString());
			comm.Parameters.Add("@year",SqlDbType.Int).Value = DateTime.Now.Year;
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();

			da.Fill(ds);

			return ds;
		}
	}
}
