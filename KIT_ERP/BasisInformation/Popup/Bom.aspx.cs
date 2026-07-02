using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data.SqlClient;

namespace KIT_ERP.BasisInformation.Popup
{
	/// <summary>
	/// Bom에 대한 요약 설명입니다.
	/// </summary>
	public class Bom : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button1;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				UltraWebGrid1.DataSource = Databind();
				UltraWebGrid1.DataBind();
			}
		}

		private DataTable Databind()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
		
			string str = "Bom";
			SqlCommand comm = new SqlCommand();
			comm.CommandText = str;
			comm.CommandType = CommandType.StoredProcedure;
			comm.Connection = conn;
			
			comm.Parameters.Add("@ItemNum",Request.QueryString["ItemNum"].ToString().Trim());

			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);

			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();

			DataTable dt = new DataTable();
			DataColumn dc = new DataColumn("ItemNum", typeof(string));//품목번호
			dt.Columns.Add(dc);
			dc = new DataColumn("ItemDrawNum", typeof(string));//도면번호
			dt.Columns.Add(dc);
			dc = new DataColumn("ItemName", typeof(string));//품목명
			dt.Columns.Add(dc);
			dc = new DataColumn("Unit", typeof(string));//단위
			dt.Columns.Add(dc);
			dc = new DataColumn("Standard", typeof(string));//규격
			dt.Columns.Add(dc);
			dc = new DataColumn("MateralQuality", typeof(string));//재질
			dt.Columns.Add(dc);
			dc = new DataColumn("PropertyClassification", typeof(string));//자산분류
			dt.Columns.Add(dc);
			dc = new DataColumn("CompanyName", typeof(string));//거래처
			dt.Columns.Add(dc);			
			dc = new DataColumn("StandardUnitCost", typeof(double));//단가
			dt.Columns.Add(dc);
			dc = new DataColumn("NeedQuantity", typeof(double));//필요량
			dt.Columns.Add(dc);
			dc = new DataColumn("Quantity", typeof(double));//재고량
			dt.Columns.Add(dc);
			dc = new DataColumn("RemainQuantity", typeof(double));//미입고
			dt.Columns.Add(dc);
			dc = new DataColumn("Lvl", typeof(string));//레벨
			dt.Columns.Add(dc);

			dc = new DataColumn("ItemClassification1", typeof(string));//품목분류1
			dt.Columns.Add(dc);

			dc = new DataColumn("IOChackable", typeof(string));//자재산출여부
			dt.Columns.Add(dc);

			dc = new DataColumn("PSIWC", typeof(string));//공정작업장
			dt.Columns.Add(dc);

			dc = new DataColumn("WSIWC", typeof(string));//작업표준작업장
			dt.Columns.Add(dc);

			dc = new DataColumn("OutSideCompany", typeof(string));//외주거래처
			dt.Columns.Add(dc);

			dc = new DataColumn("OutSideCost", typeof(double));//외주단가
			dt.Columns.Add(dc);

			dc = new DataColumn("OutSideQuantity", typeof(double));//외주재고
			dt.Columns.Add(dc);

			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				DataRow drow = dt.NewRow();
				drow["ItemNum"] = dr["ItemNum"].ToString();
				drow["Lvl"] = dr["Lvl"].ToString();
				drow["ItemClassification1"] = ItemClassification1(dr["ItemNum"].ToString().Trim(), conn, tr);	
				drow["ItemDrawNum"] = dr["ItemDrawNum"].ToString();
				drow["ItemName"] = dr["ItemName"].ToString();
				drow["Unit"] = Unit(dr["ItemNum"].ToString().Trim(),conn, tr);
				drow["Standard"] = Standard(dr["ItemNum"].ToString().Trim(), conn, tr);
				drow["MateralQuality"] = MateralQuality(dr["ItemNum"].ToString().Trim(), conn, tr);
				drow["PropertyClassification"] = PropertyClassification(dr["ItemNum"].ToString().Trim(),conn, tr);
				if(drow["PropertyClassification"].ToString().Trim() == "원자재")
				{
					drow["CompanyName"] = CompanyName(dr["ItemNum"].ToString().Trim(),conn, tr);
					drow["StandardUnitCost"] = StandardUnitCost(dr["ItemNum"].ToString().Trim(),conn,tr);
				}
				else
				{
					drow["CompanyName"] = Convert.DBNull;
					drow["StandardUnitCost"] = Convert.DBNull;
				}
				
				drow["NeedQuantity"] = decimal.Parse(dr["Quantity"].ToString().Trim());
				drow["Quantity"] = Quantity(dr["ItemNum"].ToString().Trim(),conn,tr);
				drow["PSIWC"] = PSIWC(dr["ItemNum"].ToString().Trim(),conn,tr);
				drow["WSIWC"] = WSIWC(dr["ItemNum"].ToString().Trim(),conn, tr);

				drow["OutSideCompany"] = OutSideCompanyName(dr["ItemNum"].ToString().Trim(),conn, tr);
				drow["OutSideCost"] = OutSideCost(dr["ItemNum"].ToString().Trim(),conn, tr);
				drow["OutSideQuantity"] = OutSideQuantity(dr["ItemNum"].ToString().Trim(),conn, tr);



				if(PropertyClassification(dr["ItemNum"].ToString().Trim(),conn, tr) == "원자재")
				{
					drow["RemainQuantity"] = RemainQuantity(dr["ItemNum"].ToString().Trim(),conn, tr);
					drow["IOChackable"] = IOChackable(dr["ItemNum"].ToString().Trim(),conn,tr);
				}
				else
				{
					drow["RemainQuantity"] = "0";
					drow["IOChackable"] = "";
				}
					
				dt.Rows.Add(drow);
			}
			
			conn.Close();
			return dt;
		}




		private string IOChackable(string ItemNum, SqlConnection conn, SqlTransaction tr)
		{
			bool IOChack = false;
			string str = @"Select IOChackable From II_MT where RecodingState = 1 and ItemNum = @ItemNum";
			//SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			//SqlCommand comm = new SqlCommand();
			//comm.Connection = conn;
			
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;

			comm.CommandText = str;
			//conn.Open();
			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				IOChack = bool.Parse(dr["IOChackable"].ToString());
			}
			dr.Close();			
			//conn.Close();


			if(IOChack)
				return "예";
			else
				return "아니오";
		}


		private string OutSideCompanyName(string ItemNum, SqlConnection conn, SqlTransaction tr)
		{

			string CompanyName = "";
			string str = @"Select Top 1 CompanyName From UCI_MT U inner join CI_MT C 
							on U.BusinessRegistrationNum = C.BusinessRegistrationNum 
							Where U.RecodingState = 1 and U.BusinessRegistrationNum = (Select  BusinessRegistrationNum From UCI_MT  Where UCI_MT.RecodingState = 1 and UCI_MT.ItemNum = @ItemNum and UCI_MT.UnitCostDistinction = '외주단가' and UCI_MT.OrderRate = (Select Max(U.OrderRate) From UCI_MT U Where U.RecodingState = 1 and U.ItemNum = @ItemNum and U.UnitCostDistinction = '외주단가')
							and EndProcessCode = (Select P.ProcessCode From PSI_MT P where P.RecodingState = 1 and P.ProcessSequenceNum = (Select Max(ProcessSequenceNum) From PSI_MT where RecodingState = 1 and ItemNum = @ItemNum and WorkDistinction != '자가')
							and P.ItemNum = @ItemNum))";
			str= @"Select CompanyName From UCI_MT inner join CI_MT C 
							on UCI_MT.BusinessRegistrationNum = C.BusinessRegistrationNum 
					 Where UCI_MT.RecodingState = 1 and UCI_MT.ItemNum = @ItemNum and UCI_MT.UnitCostDistinction = '외주단가' and UCI_MT.OrderRate = (Select Max(U.OrderRate) From UCI_MT U Where U.RecodingState = 1 and U.ItemNum = @ItemNum and U.UnitCostDistinction = '외주단가')
							and EndProcessCode = (Select P.ProcessCode From PSI_MT P where P.RecodingState = 1 and P.ProcessSequenceNum = (Select Max(ProcessSequenceNum) From PSI_MT where RecodingState = 1 and WorkDistinction !='자가'  and ItemNum = @ItemNum)
							and P.ItemNum = @ItemNum)";
			//string str = @"Select Top 1 CompanyName From UCI_MT U inner join CI_MT C 
			//	on U.BusinessRegistrationNum = C.BusinessRegistrationNum 
			//	Where U.RecodingState = 1 and C.RecodingState = 1 and OrderRate = (Select Max(OrderRate) From UCI_MT UCI inner join CI_MT CI 
			//	on UCI.BusinessRegistrationNum = CI.BusinessRegistrationNum 
			//	Where UCI.RecodingState =1 and CI.RecodingState =1 and UCI.UnitCostDistinction = '외주단가' and UCI.ItemNum = @ItemNum)  and UnitCostDistinction = '외주단가' and ItemNum = @ItemNum";
			//SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			//SqlCommand comm = new SqlCommand();
			//comm.Connection = conn;

			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;

			comm.CommandText = str;
			//conn.Open();
			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				CompanyName = dr["CompanyName"].ToString();
			}
			dr.Close();			
			//conn.Close();


			return CompanyName;
		}


		private decimal OutSideCost(string ItemNum, SqlConnection conn, SqlTransaction tr)
		{
			decimal StandardUnitCost = 0;
			string str = @"Select StandardUnitCost From UCI_MT  Where UCI_MT.RecodingState = 1 and UCI_MT.ItemNum = @ItemNum and UCI_MT.UnitCostDistinction = '외주단가' and UCI_MT.OrderRate = (Select Max(U.OrderRate) From UCI_MT U Where U.RecodingState = 1 and U.ItemNum = @ItemNum and U.UnitCostDistinction = '외주단가')
							and EndProcessCode = (Select P.ProcessCode From PSI_MT P where P.RecodingState = 1 and P.ProcessSequenceNum = (Select Max(ProcessSequenceNum) From PSI_MT where RecodingState = 1 and WorkDistinction !='자가'  and ItemNum = @ItemNum)
							and P.ItemNum = @ItemNum)";
			
			//SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			//SqlCommand comm = new SqlCommand(str,conn);
			//conn.Open();

			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.CommandText = str;

			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				if(dr["StandardUnitCost"].ToString().Trim() != "")
					StandardUnitCost = decimal.Parse(dr["StandardUnitCost"].ToString());
			}
			dr.Close();
			comm.Parameters.Add("@ItemNum",ItemNum);

			return StandardUnitCost;
		}


		/// <summary>
		/// 외주재고량 파악
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <returns></returns>
		private decimal OutSideQuantity(string ItemNum, SqlConnection conn, SqlTransaction tr)
		{
			decimal Quantity = 0;
			string str = @"Select sum(StockQuantity12) as StockQuantity12 From OS_MT  Where RecodingState = 1 and ItemNum = @ItemNum and [Year] = @year";
			
			//SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			//SqlCommand comm = new SqlCommand(str,conn);
			//conn.Open();

			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.CommandText = str;

			comm.Parameters.Add("@ItemNum",ItemNum);
			comm.Parameters.Add("@year",DateTime.Now.Year);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				if(dr["StockQuantity12"].ToString().Trim() != "")
					Quantity =  decimal.Parse(dr["StockQuantity12"].ToString());
			}
			dr.Close();
			
			

			return Quantity;
		}

		private string PSIWC(string ItemNum, SqlConnection conn, SqlTransaction tr)
		{
			string WC = "";
			string str = "Select WCName From PSI_MT where RecodingState = 1 and ProcessSequenceNum = (Select Max(P.ProcessSequenceNum) From PSI_MT P Where P.RecodingState =1 and P.ItemNum = @ItemNum and WorkDistinction = '자가') and  ItemNum = @ItemNum   and WorkDistinction = '자가'";
			//string str = "Select WCName From PSI_MT where RecodingState = 1 and ProcessSequenceNum = (Select Min(P.ProcessSequenceNum) From PSI_MT P Where P.RecodingState =1 and P.ItemNum = @ItemNum) and  ItemNum = @ItemNum";
			//SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			//SqlCommand comm = new SqlCommand(str,conn);
			//conn.Open();


			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.CommandText = str;


			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				WC = dr["WCName"].ToString();
			}
			dr.Close();

			return WC;
		}


		private string WSIWC(string ItemNum, SqlConnection conn, SqlTransaction tr)
		{
			string WC = "";
			//string str = "Select WCName From WSI_MT where RecodingState = 1 and ProcessSequenceNum = (Select Min(P.ProcessSequenceNum) From WSI_MT P Where P.RecodingState =1 and P.ItemNum = @ItemNum) and PriorityOrder = (Select Min(PriorityOrder) From WSI_MT W  Where W.RecodingState =1 and W.ItemNum = @ItemNum) and ItemNum = @ItemNum";
			string str = "Select WCName From WSI_MT where RecodingState = 1 and ProcessSequenceNum = (Select Max(P.ProcessSequenceNum) From WSI_MT P Where P.RecodingState =1 and P.ItemNum = @ItemNum) and PriorityOrder = (Select Min(PriorityOrder) From WSI_MT W  Where W.RecodingState =1 and W.ItemNum = @ItemNum) and ItemNum = @ItemNum";
			//SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.Transaction = tr;

			//conn.Open();
			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				WC = dr["WCName"].ToString();
			}
			dr.Close();
			comm.Parameters.Clear();

			if(WC != "")
			{
				str  = "Select WCName From WSI_MT where RecodingState = 1 and PriorityOrder = (Select Min(W.PriorityOrder) From WSI_MT W Where W.RecodingState =1 and w.ItemNum = @ItemNum) and ItemNum = @ItemNum";
				comm.CommandText = str;
				comm.Parameters.Add("@ItemNum",ItemNum);
				SqlDataReader dr1 = comm.ExecuteReader();
				while(dr1.Read())
				{
					WC = dr1["WCName"].ToString();
				}
				dr1.Close();
			}
            //conn.Close();

			return WC;
		}

		private string ItemClassification1(string ItemNum, SqlConnection conn, SqlTransaction tr)
		{
			string ItemClassification1 = "";
			string str = "Select SmallClassificationName From II_MT left outer join PUC_MT  on ItemClassification1 = SmallClassificationCode Where PUC_MT.RecodingState = 1 and II_MT.RecodingState = 1 and ItemNum = @ItemNum";
			//SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			//SqlCommand comm = new SqlCommand(str,conn);
			//conn.Open();

			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.Transaction = tr;



			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				ItemClassification1 = dr["SmallClassificationName"].ToString();
			}
			dr.Close();

			return ItemClassification1;
		}		


		private string PropertyClassification(string ItemNum, SqlConnection conn, SqlTransaction tr)
		{
			string Classification = "";
			string str = "Select PropertyClassification From II_MT Where II_MT.RecodingState = 1 and ItemNum = @ItemNum";
			//SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			//SqlCommand comm = new SqlCommand(str,conn);
			//conn.Open();
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.Transaction = tr;

			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Classification = dr["PropertyClassification"].ToString();
			}
			dr.Close();

			return Classification;
		}

		private string Unit(string ItemNum, SqlConnection conn, SqlTransaction tr)
		{
			string Unit = "";
			string str = "Select SmallClassificationName From II_MT inner join PUC_MT on Unit = SmallClassificationCode Where II_MT.RecodingState = 1 and PUC_MT.RecodingState = 1 and ItemNum = @ItemNum";
			//SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			//SqlCommand comm = new SqlCommand(str,conn);
			//conn.Open();
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.Transaction = tr;
			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Unit = dr["SmallClassificationName"].ToString();
			}
			dr.Close();

			return Unit;

		}

		private string Standard(string ItemNum, SqlConnection conn, SqlTransaction tr)
		{
			string Standard = "";
			string str = "Select Standard From II_MT Where RecodingState = 1 and ItemNum = @ItemNum";
			//SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			//SqlCommand comm = new SqlCommand(str,conn);
			//conn.Open();
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.Transaction = tr;

			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Standard = dr["Standard"].ToString();
			}
			dr.Close();

			return Standard;
		}

		private string MateralQuality(string ItemNum, SqlConnection conn, SqlTransaction tr)
		{
			string MateralQuality = "";
			string str = "Select  SmallClassificationName From II_MT inner join PUC_MT on MateralQuality = SmallClassificationCode Where II_MT.RecodingState = 1 and PUC_MT.RecodingState = 1and ItemNum = @ItemNum";
			//SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			//SqlCommand comm = new SqlCommand(str,conn);
			//conn.Open();
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.Transaction = tr;


			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				MateralQuality = dr["SmallClassificationName"].ToString();
			}
			dr.Close();

			return MateralQuality;
		}

		private string CompanyName(string ItemNum, SqlConnection conn, SqlTransaction tr)
		{
			string PropertyClassification = "";
			string CompanyName = "";
			string str = "Select PropertyClassification From II_MT Where RecodingState = 1 and ItemNum = @ItemNum";
			//SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.Transaction = tr;
			//conn.Open();
			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				PropertyClassification = dr["PropertyClassification"].ToString();
			}
			dr.Close();

			if(PropertyClassification == "제품" || PropertyClassification == "상품")
			{
				str = @"Select Top 1 CompanyName From UCI_MT U inner join CI_MT C 
				on U.BusinessRegistrationNum = C.BusinessRegistrationNum 
				Where U.RecodingState = 1 and C.RecodingState = 1 and OrderRate = (Select Max(OrderRate) From UCI_MT UCI inner join CI_MT CI 
				on UCI.BusinessRegistrationNum = CI.BusinessRegistrationNum 
				Where UCI.RecodingState =1 and CI.RecodingState =1 and UCI.UnitCostDistinction = '판매단가' and UCI.ItemNum = @ItemNum)  and UnitCostDistinction = '판매단가' and ItemNum = @ItemNum";
				comm.CommandText = str;
				SqlDataReader dr1 = comm.ExecuteReader();
				while(dr1.Read())
				{
					CompanyName = dr1["CompanyName"].ToString();
				}
				dr1.Close();
			}
			else if(PropertyClassification == "원자재")
			{
				str = @"Select Top 1 CompanyName From UCI_MT U inner join CI_MT C 
				on U.BusinessRegistrationNum = C.BusinessRegistrationNum 
				Where U.RecodingState = 1 and C.RecodingState = 1  and OrderRate = (Select Max(OrderRate) From UCI_MT UCI inner join CI_MT CI 
				on UCI.BusinessRegistrationNum = CI.BusinessRegistrationNum 
				Where UCI.RecodingState =1 and CI.RecodingState =1 and UCI.UnitCostDistinction = '구매단가' and UCI.ItemNum = @ItemNum)   and UnitCostDistinction = '구매단가' and ItemNum = @ItemNum";
				comm.CommandText = str;
				SqlDataReader dr1 = comm.ExecuteReader();
				while(dr1.Read())
				{
					CompanyName = dr1["CompanyName"].ToString();
				}
				dr1.Close();
			}
			//conn.Close();


			return CompanyName;
			
		}

		private decimal Quantity(string ItemNum, SqlConnection conn, SqlTransaction tr)
		{
			decimal Quantity = 0;
			string PropertyClassification = "";
			//	string CompanyName = "";
			string str = "Select PropertyClassification From II_MT Where RecodingState = 1 and ItemNum = @ItemNum";
			//SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.Transaction = tr;
			
			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				PropertyClassification = dr["PropertyClassification"].ToString();
			}
			dr.Close();
			comm.Parameters.Clear();

			if(PropertyClassification == "제품")
			{
				str = @"Select isnull(Sum(StockQuantity12),0) as Quantity From BS_MT Where RecodingState = 1 and ItemNum = @ItemNum and [Year] = @year";
				comm.CommandText = str;
				comm.Parameters.Add("@ItemNum",ItemNum);
				comm.Parameters.Add("@year",DateTime.Now.Year);
				SqlDataReader dr1 = comm.ExecuteReader();
				while(dr1.Read())
				{
					Quantity = Quantity + decimal.Parse(dr1["Quantity"].ToString());
				}
				dr1.Close();

				str = @"Select isnull(Sum(StockQuantity12),0) as Quantity From PS_MT
				Where PS_MT.RecodingState = 1 and PS_MT.ItemNum = @ItemNum and [Year] = @year";// and ProcessSequenceNum = (Select Max(ProcessSequenceNum) From PSI_MT Where RecodingState = 1 and ItemNum = @ItemNum)";
				comm.CommandText = str;
				SqlDataReader dr2 = comm.ExecuteReader();
				while(dr2.Read())
				{
					Quantity = Quantity + decimal.Parse(dr2["Quantity"].ToString());
				}
				dr2.Close();
				comm.Parameters.Clear();
			}
			else if(PropertyClassification == "상품")
			{
				str = @"Select isnull(Sum(StockQuantity12),0) as Quantity From BS_MT	Where RecodingState = 1 and ItemNum = @ItemNum and [Year] = @year";
				comm.CommandText = str;
				comm.Parameters.Add("@ItemNum",ItemNum);
				comm.Parameters.Add("@year",DateTime.Now.Year);
				SqlDataReader dr1 = comm.ExecuteReader();
				while(dr1.Read())
				{
					Quantity = Quantity + decimal.Parse(dr1["Quantity"].ToString());
				}
				dr1.Close();

				str = @"Select isnull(Sum(StockQuantity12),0) as Quantity From PS_MT Where PS_MT.RecodingState = 1 and PS_MT.ItemNum = @ItemNum and [Year] = @year";
				comm.CommandText = str;
				SqlDataReader dr2 = comm.ExecuteReader();
				while(dr2.Read())
				{
					Quantity = Quantity + decimal.Parse(dr2["Quantity"].ToString());
				}
				dr2.Close();
				comm.Parameters.Clear();
			}
			else if(PropertyClassification == "반제품")
			{
				str = @"Select isnull(Sum(StockQuantity12),0) as Quantity From PS_MT	Where RecodingState = 1 and ItemNum = @ItemNum and [Year] = @year";
				comm.CommandText = str;
				comm.Parameters.Add("@ItemNum",ItemNum);
				comm.Parameters.Add("@year",DateTime.Now.Year);
				SqlDataReader dr1 = comm.ExecuteReader();
				while(dr1.Read())
				{
					Quantity = Quantity + decimal.Parse(dr1["Quantity"].ToString());
				}
				dr1.Close();
				comm.Parameters.Clear();
			}
			else if(PropertyClassification == "원자재")
			{
				str = @"Select isnull(Sum(StockQuantity12),0) as Quantity From RMS_MT	Where RecodingState = 1 and ItemNum = @ItemNum and [Year] = @year";
				comm.CommandText = str;
				comm.Parameters.Add("@ItemNum",ItemNum);
				comm.Parameters.Add("@year",DateTime.Now.Year);
				SqlDataReader dr1 = comm.ExecuteReader();
				while(dr1.Read())
				{
					Quantity = Quantity + decimal.Parse(dr1["Quantity"].ToString());
				}
				dr1.Close();
				comm.Parameters.Clear();
			}
			//conn.Close();
			
			return Quantity;

		}

		private decimal StandardUnitCost(string ItemNum, SqlConnection conn, SqlTransaction tr)
		{
			decimal StandardUnitCost = 0;
			string str = "Select StandardUnitCost From UCI_MT Where RecodingState = 1 and ItemNum = @ItemNum and UnitCostDistinction = '구매단가' and OrderRate = (Select Max(OrderRate) From UCI_MT Where RecodingState = 1 and ItemNum = @ItemNum and UnitCostDistinction = '구매단가')";
			//SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			//SqlCommand comm = new SqlCommand(str,conn);
			//conn.Open();

			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.Transaction = tr;

			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				if(dr["StandardUnitCost"].ToString().Trim() != "")
					StandardUnitCost = decimal.Parse(dr["StandardUnitCost"].ToString());
			}
			dr.Close();
			comm.Parameters.Clear();
			return StandardUnitCost;
		}

		private decimal RemainQuantity(string ItemNum, SqlConnection conn, SqlTransaction tr)
		{
			decimal RemainQuantity = 0;
			string str = "Select isnull(Sum(RemainQuantity),0) as RemainQuantity From BO_HT Where ItemNum = @ItemNum and (ProgressCondition = '대기' or ProgressCondition = '진행')";
			//SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			//SqlCommand comm = new SqlCommand(str,conn);
			//conn.Open();

			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.Transaction = tr;


			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				RemainQuantity = decimal.Parse(dr["RemainQuantity"].ToString());
			}
			dr.Close();
			comm.Parameters.Clear();

			return RemainQuantity;
		}	


		

		#region Web Form 디자이너에서 생성한 코드
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 이 호출은 ASP.NET Web Form 디자이너에 필요합니다.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다.
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			UltraWebGridExcelExporter1.Export(UltraWebGrid1);
		}
	}
}
