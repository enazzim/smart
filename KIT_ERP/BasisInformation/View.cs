using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;



namespace KIT_ERP.BasisInformation
{
	/// <summary>
	/// View에 대한 요약 설명입니다.
	/// </summary>
	public class View :System.Web.UI.Page
	{
		private string itemnum="";
		private string processSequenceNum ="";
		private string requestpage="";

		
		public View(string m_itemnum, string m_page)
		{
			itemnum= m_itemnum.ToString();
			requestpage = m_page.ToString();
		}

		public View(string m_itemnum, string ProcessSequenceNum, string m_page)
		{
			itemnum= m_itemnum.ToString();
			processSequenceNum = ProcessSequenceNum.ToString();
			requestpage = m_page.ToString();
		}

		public DataSet ItemView()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			
			string str = "";
			
			switch(requestpage)
			{
				case "BuyingUnitCodeInfo":

					str = @"SELECT   UCI_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, UCI_MT.UnitCostDistinction, CI_MT.CompanyName,
						UCI_MT.BusinessRegistrationNum, PUC_MT.SmallClassificationName, PUC_MT_1.SmallClassificationName, UCI_MT.OrderRate, 
						UCI_MT.StandardUnitCost, UCI_MT.DiscountUnitCost, UCI_MT.BeginDate, 
						case UCI_MT.EndDate when '2076-06-06' then '' end as EndDate, 
						UCI_MT.RecodingState, UCI_MT.RegistrationPerson, 
						UCI_MT.RegistrationPersonID, UCI_MT.RegistrationDate, UCI_MT.UpdatingPerson, UCI_MT.UpdatingPersonID, UCI_MT.UpdatingDate, 
						UCI_MT.UnitCostInfoIndex FROM PUC_MT PUC_MT_1 INNER JOIN PUC_MT ON PUC_MT_1.PublicUseCodeIndex = PUC_MT.PublicUseCodeIndex INNER JOIN 
						II_MT INNER JOIN UCI_MT ON II_MT.ItemNum = UCI_MT.ItemNum INNER JOIN CI_MT ON UCI_MT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum ON 
						PUC_MT.SmallClassificationCode = UCI_MT.BeginProcessCode AND PUC_MT_1.SmallClassificationCode = UCI_MT.EndProcessCode 
						Where CI_MT.RecodingState = 1 and UCI_MT.RecodingState = 1 and UCI_MT.UnitCostDistinction = '구매단가' and II_MT.RecodingState = 1 and II_MT.ItemNum = @num";
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = itemnum.ToString();
					break;
				case "SaleUnitCodeInfo":
					str = @"SELECT   UCI_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, UCI_MT.UnitCostDistinction, CI_MT.CompanyName, 
						UCI_MT.BusinessRegistrationNum, PUC_MT.SmallClassificationName, PUC_MT_1.SmallClassificationName, UCI_MT.OrderRate, 
						UCI_MT.StandardUnitCost, UCI_MT.DiscountUnitCost, UCI_MT.BeginDate, 
						case UCI_MT.EndDate when '2076-06-06' then '' end as EndDate, UCI_MT.RecodingState, UCI_MT.RegistrationPerson, 
						UCI_MT.RegistrationPersonID, UCI_MT.RegistrationDate, UCI_MT.UpdatingPerson, UCI_MT.UpdatingPersonID, UCI_MT.UpdatingDate, 
						UCI_MT.UnitCostInfoIndex FROM PUC_MT PUC_MT_1 INNER JOIN PUC_MT ON PUC_MT_1.PublicUseCodeIndex = PUC_MT.PublicUseCodeIndex INNER JOIN 
						II_MT INNER JOIN UCI_MT ON II_MT.ItemNum = UCI_MT.ItemNum INNER JOIN CI_MT ON UCI_MT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum ON 
						PUC_MT.SmallClassificationCode = UCI_MT.BeginProcessCode AND PUC_MT_1.SmallClassificationCode = UCI_MT.EndProcessCode 
						Where CI_MT.RecodingState = 1 and UCI_MT.RecodingState = 1 and UCI_MT.UnitCostDistinction = '판매단가' and II_MT.RecodingState = 1 and II_MT.ItemNum = @num ";
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = itemnum.ToString();
					break;
				case "OutSideOrderUnitCodeInfo":
					str = @"SELECT   UCI_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, UCI_MT.UnitCostDistinction, CI_MT.CompanyName,
						UCI_MT.BusinessRegistrationNum, UCI_MT.BeginProcessCode, PUC_MT_1.SmallClassificationName as BeginProcess, UCI_MT.EndProcessCode, PUC_MT_2.SmallClassificationName as EndProcess, UCI_MT.OrderRate,
						UCI_MT.StandardUnitCost, UCI_MT.DiscountUnitCost, UCI_MT.BeginDate, 
						case UCI_MT.EndDate when '2076-06-06' then '' end as EndDate, UCI_MT.RecodingState, UCI_MT.RegistrationPerson, 
						UCI_MT.RegistrationPersonID, UCI_MT.RegistrationDate, UCI_MT.UpdatingPerson, UCI_MT.UpdatingPersonID, UCI_MT.UpdatingDate, 
						UCI_MT.UnitCostInfoIndex 
						FROM PUC_MT PUC_MT_1 
						INNER JOIN  UCI_MT 
						ON PUC_MT_1.SmallClassificationCode  = UCI_MT.BeginProcessCode 
						join PUC_MT PUC_MT_2 
						ON PUC_MT_2.SmallClassificationCode  = UCI_MT.EndProcessCode 
						join ii_mt 
						on ii_mt.ItemNum = uci_mt.itemnum 
						join ci_mt 
						on ci_mt.BusinessRegistrationNum = uci_mt.BusinessRegistrationNum 
						Where CI_MT.RecodingState = 1 and  UCI_MT.RecodingState = 1 and UCI_MT.UnitCostDistinction = '외주단가' and II_MT.RecodingState = 1 and II_MT.ItemNum = @num";
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = itemnum.ToString();
					break;
				case "ItemOrganizationInfo":
					str = @" SELECT   IOI_MT.ParentItemNum, IOI_MT.ChildItemNum, II_MT_1.ItemDrawNum, II_MT_1.ItemName, 
						II_MT_1.PropertyClassification, II_MT_1.Unit, PUC_MT_2.SmallClassificationName as ChildUnit, II_MT_1.Standard, IOI_MT.NeedQuantityNumerator, 
						IOI_MT.NeedQuantityDenominator, CASE IOI_MT.ProcessManagement  WHEN 1 THEN '예' ELSE '아니오' END AS ProcessManagement , 
						CASE IOI_MT.SubDivision   WHEN 1 THEN '예' ELSE '아니오' END AS SubDivision,IOI_MT.SupplyDivision,PUC_MT_1.SmallClassificationName AS Division, IOI_MT.BOMUnit, 
						PUC_MT.SmallClassificationName, IOI_MT.BeginDate,
						case IOI_MT.EndDate when '2076-06-06' then '' end as EndDate,
						IOI_MT.RecodingState, IOI_MT.RegistrationPerson, IOI_MT.RegistrationPersonID, 
						IOI_MT.RegistrationDate, IOI_MT.UpdatingPerson, IOI_MT.UpdatingPersonID, 
						IOI_MT.ItemOrganizationInfoIndex, IOI_MT.UpdatingDate 
						FROM IOI_MT INNER JOIN 
						II_MT ON IOI_MT.ParentItemNum = II_MT.ItemNum INNER JOIN 
						II_MT II_MT_1 ON IOI_MT.ChildItemNum = II_MT_1.ItemNum INNER JOIN 
						PUC_MT PUC_MT_2 ON II_MT_1.Unit = PUC_MT_2.SmallClassificationCode left outer join
						PUC_MT ON IOI_MT.BOMUnit = PUC_MT.SmallClassificationCode  left outer join
						PUC_MT PUC_MT_1 ON IOI_MT.SupplyDivision = PUC_MT_1.SmallClassificationCode 
						where II_MT.RecodingState = 1 and II_MT_1.RecodingState = 1 and IOI_MT.RecodingState = 1 and IOI_MT.ParentItemNum = @num";
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = itemnum.ToString();
					break;
				case "RealItemOrganizationInfo":
					str = @" SELECT   IOI_MT.ParentItemNum, IOI_MT.ChildItemNum, II_MT_1.ItemDrawNum, II_MT_1.ItemName, 
						II_MT_1.PropertyClassification, II_MT_1.Unit, PUC_MT_2.SmallClassificationName as ChildUnit, II_MT_1.Standard, IOI_MT.NeedQuantityNumerator, 
						IOI_MT.NeedQuantityDenominator, CASE IOI_MT.ProcessManagement  WHEN 1 THEN '예' ELSE '아니오' END AS ProcessManagement , 
						CASE IOI_MT.SubDivision   WHEN 1 THEN '예' ELSE '아니오' END AS SubDivision,IOI_MT.SupplyDivision,PUC_MT_1.SmallClassificationName AS Division, IOI_MT.BOMUnit, 
						PUC_MT.SmallClassificationName, IOI_MT.BeginDate,
						case IOI_MT.EndDate when '2076-06-06' then '' end as EndDate,
						IOI_MT.RecodingState, IOI_MT.RegistrationPerson, IOI_MT.RegistrationPersonID, 
						IOI_MT.RegistrationDate, IOI_MT.UpdatingPerson, IOI_MT.UpdatingPersonID, 
						IOI_MT.RealItemOrganizationInfoIndex, IOI_MT.UpdatingDate 
						FROM RIOI_MT IOI_MT INNER JOIN 
						II_MT ON IOI_MT.ParentItemNum = II_MT.ItemNum INNER JOIN 
						II_MT II_MT_1 ON IOI_MT.ChildItemNum = II_MT_1.ItemNum INNER JOIN 
						PUC_MT PUC_MT_2 ON II_MT_1.Unit = PUC_MT_2.SmallClassificationCode left outer join
						PUC_MT ON IOI_MT.BOMUnit = PUC_MT.SmallClassificationCode  left outer join
						PUC_MT PUC_MT_1 ON IOI_MT.SupplyDivision = PUC_MT_1.SmallClassificationCode 
						where II_MT.RecodingState = 1 and II_MT_1.RecodingState = 1 and IOI_MT.RecodingState = 1 and IOI_MT.ParentItemNum = @num";
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = itemnum.ToString();
					break;
				case "ProcessSequenceInfo":
					str = @"SELECT   PSI_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, PSI_MT.EtcText,
						PSI_MT.ProcessSequenceNum, PSI_MT.ProcessCode, PUC_MT.SmallClassificationName, PSI_MT.WorkDistinction, PSI_MT.WCName, 
						PSI_MT.OutsideOrderRate, PSI_MT.ProgressRate, PSI_MT.LeadTime, PSI_MT.RecodingState, PSI_MT.RegistrationPerson, 
						PSI_MT.RegistrationPersonID, PSI_MT.RegistrationDate, PSI_MT.UpdatingPerson, PSI_MT.UpdatingPersonID, PSI_MT.UpdatingDate, 
						PSI_MT.ProcessSequenceInfoIndex FROM PSI_MT INNER JOIN II_MT ON PSI_MT.ItemNum = II_MT.ItemNum INNER JOIN 
						PUC_MT ON PSI_MT.ProcessCode = PUC_MT.SmallClassificationCode 
						where II_MT.RecodingState = 1 and PSI_MT.RecodingState = 1 and II_MT.ItemNum = @num order by PSI_MT.ProcessSequenceNum";

					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = itemnum.ToString();
					break;
				case "RealProcessSequenceInfo":
					str = @"SELECT   RPSI_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, RPSI_MT.EtcText,
						RPSI_MT.ProcessSequenceNum, RPSI_MT.ProcessCode, PUC_MT.SmallClassificationName, RPSI_MT.WorkDistinction, RPSI_MT.WCName, 
						RPSI_MT.OutsideOrderRate, RPSI_MT.ProgressRate, RPSI_MT.LeadTime, RPSI_MT.RecodingState, RPSI_MT.RegistrationPerson, 
						RPSI_MT.RegistrationPersonID, RPSI_MT.RegistrationDate, RPSI_MT.UpdatingPerson, RPSI_MT.UpdatingPersonID, RPSI_MT.UpdatingDate, 
						RPSI_MT.RealProcessSequenceInfoIndex FROM RPSI_MT INNER JOIN II_MT ON RPSI_MT.ItemNum = II_MT.ItemNum INNER JOIN 
						PUC_MT ON RPSI_MT.ProcessCode = PUC_MT.SmallClassificationCode 
						where II_MT.RecodingState = 1 and RPSI_MT.RecodingState = 1 and II_MT.ItemNum = @num order by RPSI_MT.ProcessSequenceNum";

					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = itemnum.ToString();
					break;

				case "WorkStandardInfo":
					str = @"SELECT WSI_MT.ItemNum, WSI_MT.ProcessSequenceNum, PUC_MT.SmallClassificationName, WSI_MT.ProcessCode, II_MT.ItemDrawNum, 
						II_MT.ItemName, WSI_MT.WCName, WSI_MT.PriorityOrder, WSI_MT.MainWorkerID, WSI_MT.MainWorker, WSI_MT.ToolName1, WSI_MT.JigName1, 
						WSI_MT.ToolName2, WSI_MT.JigName2, WSI_MT.ToolName3, WSI_MT.JigName3, WSI_MT.SetupTime, 
						WSI_MT.RealProcessingTime, WSI_MT.SpaceTime, WSI_MT.StandardTime, WSI_MT.WaitTime, WSI_MT.LotSize, WSI_MT.Cavity, WSI_MT.RecodingState, 
						WSI_MT.RegistrationPerson, WSI_MT.RegistrationPersonID, WSI_MT.RegistrationDate, WSI_MT.UpdatingPerson, WSI_MT.UpdatingPersonID, 
						WSI_MT.UpdatingDate, WSI_MT.WorkStandardInfoIndex FROM WSI_MT INNER JOIN II_MT ON WSI_MT.ItemNum = II_MT.ItemNum INNER JOIN 
						PUC_MT ON WSI_MT.ProcessCode = PUC_MT.SmallClassificationCode Where II_MT.RecodingState = '1' and WSI_MT.RecodingState = 1 and WSI_MT.ItemNum = @num and WSI_MT.ProcessSequenceNum = @sequence";
					
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = itemnum.ToString();
					comm.Parameters.Add("@sequence",SqlDbType.TinyInt).Value = processSequenceNum.ToString();

					break;
				case "RealWorkStandardInfo":
					str = @"SELECT RWSI_MT.ItemNum, RWSI_MT.ProcessSequenceNum, PUC_MT.SmallClassificationName, RWSI_MT.ProcessCode, II_MT.ItemDrawNum, 
						II_MT.ItemName, RWSI_MT.WCName, RWSI_MT.PriorityOrder, RWSI_MT.MainWorkerID, RWSI_MT.MainWorker, RWSI_MT.ToolName1, RWSI_MT.JigName1, 
						RWSI_MT.ToolName2, RWSI_MT.JigName2, RWSI_MT.ToolName3, RWSI_MT.JigName3, RWSI_MT.SetupTime, 
						RWSI_MT.RealProcessingTime, RWSI_MT.SpaceTime, RWSI_MT.StandardTime, RWSI_MT.WaitTime, RWSI_MT.LotSize, RWSI_MT.Cavity, RWSI_MT.RecodingState, 
						RWSI_MT.RegistrationPerson, RWSI_MT.RegistrationPersonID, RWSI_MT.RegistrationDate, RWSI_MT.UpdatingPerson, RWSI_MT.UpdatingPersonID, 
						RWSI_MT.UpdatingDate, RWSI_MT.RealWorkStandardInfoIndex FROM RWSI_MT INNER JOIN II_MT ON RWSI_MT.ItemNum = II_MT.ItemNum INNER JOIN 
						PUC_MT ON RWSI_MT.ProcessCode = PUC_MT.SmallClassificationCode Where II_MT.RecodingState = '1' and RWSI_MT.RecodingState = 1 and RWSI_MT.ItemNum = @num and RWSI_MT.ProcessSequenceNum = @sequence";
					
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = itemnum.ToString();
					comm.Parameters.Add("@sequence",SqlDbType.TinyInt).Value = processSequenceNum.ToString();

					break;
					

				case "BusinessPlanInfo":
					str = @"SELECT   II_MT.ItemNum as ItemNum, II_MT.ItemDrawNum as ItemDrawNum, II_MT.ItemName as ItemName, BPI_MT.PlanYear as PlanYear, BPI_MT.PlanMonth as PlanMonth, BPI_MT.PlanDay as PlanDay, BPI_MT.PlanQuantity as PlanQuantity, BPI_MT.SaleUnitCost as SaleUnitCost,
						BPI_MT.PlanTotalCost as PlanTotalCost, CI_MT.CompanyName as CompanyName, CI_MT.BusinessRegistrationNum as BusinessRegistrationNum, BPI_MT.RecodingState as RecodingState, BPI_MT.RegistrationPerson as RegistrationPerson,
						BPI_MT.RegistrationPersonID, BPI_MT.RegistrationDate, BPI_MT.UpdatingPerson, BPI_MT.UpdatingPersonID, BPI_MT.UpdatingDate, 
						BPI_MT.BusinessPlanInfoIndex as BusinessPlanInfoIndex FROM BPI_MT INNER JOIN II_MT ON BPI_MT.ItemNum = II_MT.ItemNum INNER JOIN CI_MT ON BPI_MT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
						WHERE II_MT.RecodingState =  '1' and (BPI_MT.RecodingState = '1') AND (II_MT.ItemNum = @num) order by II_MT.ItemNum, BPI_MT.PlanYear, BPI_MT.PlanMonth";
					
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = itemnum.ToString();
					

					break;

					
					
				case "ExecutionPlanInfo":
					str  = @"SELECT EPI_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, EPI_MT.PlanDate, EPI_MT.PlanQuantity, 
						EPI_MT.SaleUnitCost, EPI_MT.PlanTotalCost, EPI_MT.RecodingState, EPI_MT.RegistrationPerson, 
						EPI_MT.RegistrationPersonID, EPI_MT.RegistrationDate, EPI_MT.UpdatingPerson, EPI_MT.UpdatingPersonID, 
						EPI_MT.UpdatingDate, EPI_MT.ExecutionPlanInfoIndex FROM EPI_MT INNER JOIN II_MT ON 
						EPI_MT.ItemNum = II_MT.ItemNum WHERE II_MT.RecodingState = '1' and (EPI_MT.RecodingState = '1') and (EPI_MT.ItemNum = @num) order by II_MT.ItemNum, EPI_MT.PlanDate";
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = itemnum.ToString();
					break;

					
				default:
					break;
			}

			comm.CommandText = str;
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);
			//SqlDataReader dr = comm.ExecuteReader();
			conn.Close();
			return ds;
			

		}
	}
}
