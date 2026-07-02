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
using Infragistics.WebUI.UltraWebGrid;

using System.Data.SqlClient;
using System.Configuration;

namespace KIT_ERP.BusinessManagement.PopUp
{
	/// <summary>
	/// StockRemainder에 대한 요약 설명입니다.
	/// </summary>
	public class StockRemainder : System.Web.UI.Page
	{
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid2;

		private string ItemNum;
		private string ItemDrawNum;
		private string ItemName;
		//DataSet ds= new DataSet();

	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				search();
			}
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
			this.UltraWebGrid1.DataBinding += new System.EventHandler(this.UltraWebGrid1_DataBinding);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void search()
		{
			UltraWebGrid2 = (Infragistics.WebUI.UltraWebGrid.UltraWebGrid)Session["uwg"];

			for(int i = UltraWebGrid2.Rows.Count-1; i >= 0;i--)
			{
				// 체크되지 않으면 Value값이 null이다 그러므로 확인후 false값을 넣어준다.
				if(UltraWebGrid2.Rows[i].Cells.FromKey("chk").Value == null || UltraWebGrid2.Rows[i].Cells.FromKey("chk").Value.ToString() == "false")
				{
					//UltraWebGrid2.Rows[i].Cells.FromKey("chk").Value = false;
					UltraWebGrid2.Rows[i].Delete();
				}
			}

			ItemNum = UltraWebGrid2.Rows[0].Cells.FromKey("ItemNum").Value.ToString().Trim();
			ItemDrawNum = UltraWebGrid2.Rows[0].Cells.FromKey("ItemDrawNum").Value.ToString().Trim();
			ItemName = UltraWebGrid2.Rows[0].Cells.FromKey("ItemName").Value.ToString().Trim();

			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			//검사할 품목을 초기화 한다
			string str  = @"Delete From TBRemainderTable";
			SqlCommand com = new SqlCommand();
			com.Connection = conn;
			com.CommandText = str;
			com.CommandType = CommandType.Text;
			com.ExecuteNonQuery();


			// 생산공정중의 제품 최종공정이 완료된 품목수량 구하기
			decimal PS_MTStock = 0;
			str  = @"select P.ItemNum, P.StockQuantity12 From PS_MT P where P.ItemNum = @ItemNum and RecodingState = 1
				and P.ProcessSequenceNum = (Select Max(P2.ProcessSequenceNum) from PS_MT P2 where P.ItemNum = P2.ItemNum Group by P2.ItemNum)";
			com.Parameters.Add("@ItemNum",ItemNum);			
			SqlDataReader dr = com.ExecuteReader();
			while(dr.Read())
			{
				PS_MTStock = decimal.Parse(dr["StockQuantity12"].ToString());
			}
			dr.Close();
			
			// 영업창고 제품 수량 구하기
			decimal BS_MTStock = 0;
			str = @"Select ItemNum, sum(StockQuantity12) as Quantity From BS_MT Where ItemNum = @ItemNum and RecodingState = '1' Group by ItemNum";
			com.CommandText = str;
			SqlDataReader dr1 = com.ExecuteReader();
			while(dr1.Read())
			{
				BS_MTStock = decimal.Parse(dr1["Quantity"].ToString());
			}
			dr1.Close();

			//납품창고 수량구하기
			decimal DS_MTStock = 0;
			str = @"Select ItemNum, StockQuantity12 as Quantity From DS_MT Where ItemNum = @ItemNum and RecodingState = '1' ";
			com.CommandText = str;
			SqlDataReader dr2 = com.ExecuteReader();
			while(dr2.Read())
			{
				DS_MTStock = decimal.Parse(dr2["Quantity"].ToString());
			}
			dr2.Close();

			//생산의뢰 대기량 구하기
			decimal PR_HTStock = 0;
			str = @"Select ItemNum, sum(ProductionRequestQuantity) as Quantity From PR_HT Where ItemNum = @ItemNum  and ProgressCondition = '대기' Group by ItemNum";
			com.CommandText = str;
			SqlDataReader dr3 = com.ExecuteReader();
			while(dr3.Read())
			{
				PR_HTStock = decimal.Parse(dr3["Quantity"].ToString());
			}
			dr3.Close();

			// 작업계획 마지막 공정 잔량(대기,지시,진행)
			decimal WDWP_HTStock = 0;
			str = @"select W.ItemNum, W.ProcessSequenceNum, Sum(W.WorkPlanQuantity - W.WorkCompletionQuantity) as Quantity From WDWP_HT W where
				W.ItemNum =@ItemNum and (W.ProgressCondition = '대기' or W.ProgressCondition = '지시' or W.ProgressCondition = '진행')
				and W.ProcessSequenceNum = (Select Max(P.ProcessSequenceNum) From PSI_MT P Where P.ItemNum = W.ItemNum and P.RecodingState = 1 
				Group by P.ItemNum)
				Group by W.ItemNum, W.ProcessSequenceNum";
			com.CommandText = str;
			SqlDataReader dr4 = com.ExecuteReader();
			while(dr4.Read())
			{
				WDWP_HTStock = decimal.Parse(dr4["Quantity"].ToString());
			}
			dr4.Close();

			//외주발주원장 품목이 제품의 마지막공정인 잔량(대기,진행)
			decimal OO_HTStock = 0;
			str = @"select O.ItemNum, O.EndProcessCode, Sum(O.OrderQuantity - O.RemainQuantity) as Quantity From OO_HT O where
				O.ItemNum =@ItemNum and (O.ProgressCondition = '대기' or O.ProgressCondition = '진행')
				and O.EndProcessCode = (Select P.ProcessCode From PSI_MT P Where P.ItemNum = O.ItemNum and P.RecodingState = 1 
				and P.ProcessSequenceNum = (Select Max(P2.ProcessSequenceNum) From PSI_MT P2 Where P2.ItemNum = O.ItemNum and P2.RecodingState = 1 
				Group by P2.ItemNum))
				Group by O.ItemNum, O.EndProcessCode";
			com.CommandText = str;
			SqlDataReader dr5 = com.ExecuteReader();
			while(dr5.Read())
			{
				OO_HTStock = decimal.Parse(dr5["Quantity"].ToString());
			}
			dr5.Close();


			// 수주원장 잔량(진행)
			decimal RO_HTStock = 0;
			str = @"Select ItemNum, Sum(RemainderQuantity) as Quantity From RO_HT
				where ItemNum = @ItemNum and ProgressCondition = '진행' Group by ItemNum";
			com.CommandText = str;
			SqlDataReader dr6 = com.ExecuteReader();
			while(dr6.Read())
			{
				RO_HTStock = decimal.Parse(dr6["Quantity"].ToString());
			}
			dr6.Close();
			decimal RemainderQuantity = 0;
			RemainderQuantity = (PS_MTStock + BS_MTStock + DS_MTStock + PR_HTStock + WDWP_HTStock) - RO_HTStock;
			
			//테이블에 행 삽입
			str= @"InSert into TBRemainderTable 
				(ItemNum,ItemDrawNum,ItemName, PS_MTStock, BS_MTStock, DS_MTStock, PR_HTStock, WDWP_HTStock, OO_HTStock, RO_HTStock, RemainderQuantity) 
				Values(@ItemNum,@ItemDrawNum,@ItemName, @PS_MTStock, @BS_MTStock, @DS_MTStock, @PR_HTStock, @WDWP_HTStock, @OO_HTStock, @RO_HTStock, @RemainderQuantity)";
			com.CommandText = str;
			com.Parameters.Add("@ItemDrawNum",ItemDrawNum);
			com.Parameters.Add("@ItemName",ItemName);
			com.Parameters.Add("@PS_MTStock",PS_MTStock);
			com.Parameters.Add("@BS_MTStock",BS_MTStock);
			com.Parameters.Add("@DS_MTStock",DS_MTStock);
			com.Parameters.Add("@PR_HTStock",PR_HTStock);
			com.Parameters.Add("@WDWP_HTStock",WDWP_HTStock);
			com.Parameters.Add("@OO_HTStock",OO_HTStock);
			com.Parameters.Add("@RO_HTStock",RO_HTStock);
			com.Parameters.Add("@RemainderQuantity",RemainderQuantity);

			com.ExecuteNonQuery();

			//this.UltraWebGrid1.DataBind();
			this.DataBind();
			Session.Remove("uwg");
		}


		private void UltraWebGrid1_DataBinding(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			DataSet ds = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(@"select * From TBRemainderTable "+
			@"select ItemNum, ItemDrawNum, ItemName, CompanyName, BusinessRegistrationNum, PropertyClassification, 
			case ProductionRequestDivision when 1 then '예' else '아니오' end as ProductionRequestDivision, 
			ReceivingOrderDate, ApplyUnitCost, DeliveryRequestQuantity1, DeliveryRequestDate1, 
			DeliveryRequestQuantity2, DeliveryRequestDate2, DeliveryRequestQuantity3, DeliveryRequestDate3, 
			DeliveryRequestQuantity4, DeliveryRequestDate4, DeliveryRequestQuantity5, DeliveryRequestDate5, 
			TotalReceiveingOrderQuantity, TotalCost, OutStorehouseQuantity, SuitabilityQuantity, 
			UnInspectionQuantity, RemainderQuantity, OrderNum, DeliveryPlace, VolumNum, 
			ProgressCondition, RegistrationPerson, RegistrationPersonID, RegistrationDate, 
			UpdatingPerson, UpdatingPersonID, UpdatingDate, ReceivingOrderHistoryIndex
			From RO_HT Where ProgressCondition = '진행' and ItemNum = @ItemNum ",conn);
			da.SelectCommand.Parameters.Add("@ItemNum",ItemNum);
			da.Fill(ds);

			ds.Relations.Add(ds.Tables[0].Columns["ItemNum"],
				ds.Tables[1].Columns["ItemNum"]);
			
			this.UltraWebGrid1.DataSource = ds.Tables[0].DefaultView;
			//this.UltraWebGrid1.DataBind();
		}
	}
}
