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

using Microsoft.Win32;
using System.Data.SqlClient;
using System.Configuration;

namespace KIT_ERP.ProductionManagement
{
	/// <summary>
	/// RowMaterialRequirementCalculate에 대한 요약 설명입니다.
	/// </summary>
	public class RowMaterialRequirementCalculate : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label3;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgPP_HT;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcMinDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcMaxDate;
		protected System.Web.UI.WebControls.Button btnClear;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Button btnProduce;
		protected System.Web.UI.WebControls.Label searchTitle;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPresentRowMaterialUsed;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSafeyRowMaterialUsed;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOrderNonInStorehouseUsed;
		protected System.Web.UI.HtmlControls.HtmlInputHidden chkOrderGapInStorehouseUsed;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMinimumGapUsed;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOrderRequestStandbyUsed;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOrderGapInStorehouseUsed;
		protected KIT_ERP.ProductionManagement.dsRowMaterial dsRowMaterial1 = new KIT_ERP.ProductionManagement.dsRowMaterial();
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected Stack st = new Stack();
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOutOrderUsed;
		protected ArrayList List = new ArrayList();
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdWorkPlan2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdWorkPlan3;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdWorkPlan1;
		protected int a = 0;
		struct WC
		{
			public string ItemNum;
			public decimal Quantity;	//작업수량
		}
		struct WCSub
		{
			public string ItemNum;
			public string ProcessCode;
			public decimal Quantity;	//작업수량
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.Products = true;
			ItemSearchControl1.HalfFinishedProducts = true;

			if(!Page.IsPostBack)
			{
				// MainTitle Frame에 해당 페이지 Title출력
				////Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 생산관리 > 자재소요량 산출';</script>");

				// 산출버튼 onclick속성 추가
				//btnProduce.Attributes.Add("onclick", "Popup('PopupWindows/RowMaterialMessage.aspx', window, '374', '140');");
				// 시스템정보의 셋팅(자재소요량 산출관련)값을 가져옴.
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
				conn.Open();

				//2005.08.21 select 문장에 OutOrderUsed 필드를 추가했음
				//이전 Select 문 => @"SELECT PresentRowMaterialUsed, SafeyRowMaterialUsed, OrderNonInStorehouseUsed, OrderGapInStorehouseUsed, MinimumGapUsed,OrderRequestStandbyUsed FROM SCS_T"
				string strSQL = @"SELECT PresentRowMaterialUsed, SafeyRowMaterialUsed, OrderNonInStorehouseUsed, 
								  OrderGapInStorehouseUsed, MinimumGapUsed,OrderRequestStandbyUsed,OutOrderUsed, WorkPlan1, WorkPlan2, WorkPlan3 FROM SCS_T";

				SqlCommand comm = new SqlCommand(strSQL, conn);

				SqlDataReader reader = comm.ExecuteReader();

				while(reader.Read())
				{
					// 현재고량
					if(bool.Parse(reader["PresentRowMaterialUsed"].ToString()))
						hidPresentRowMaterialUsed.Value = "true";
					else
						hidPresentRowMaterialUsed.Value = "false";

					// 안전재고량
					if(bool.Parse(reader["SafeyRowMaterialUsed"].ToString()))
						hidSafeyRowMaterialUsed.Value = "true";
					else
						hidSafeyRowMaterialUsed.Value = "false";

					//발주간격수량
					if(bool.Parse(reader["OrderGapInStorehouseUsed"].ToString()))
						hidOrderGapInStorehouseUsed.Value = "true";
					else
						hidOrderGapInStorehouseUsed.Value = "false";

					//최소발주량
					if(bool.Parse(reader["MinimumGapUsed"].ToString()))
						hidMinimumGapUsed.Value = "true";
					else
						hidMinimumGapUsed.Value = "false";

					//작업계획1
					if(bool.Parse(reader["WorkPlan1"].ToString()))
						hdWorkPlan1.Value = "true";
					else
						hdWorkPlan1.Value = "false";

					//작업계획2
					if(bool.Parse(reader["WorkPlan2"].ToString()))
						hdWorkPlan2.Value = "true";
					else
						hdWorkPlan2.Value = "false";

					//작업계획3
					if(bool.Parse(reader["WorkPlan3"].ToString()))
						hdWorkPlan3.Value = "true";
					else
						hdWorkPlan3.Value = "false";



					//2005.08.21 자가/외주 재고 감안여부 추가 시작
					// 자가/외주 재고 감안여부
					if(bool.Parse(reader["OutOrderUsed"].ToString()))
						hidOutOrderUsed.Value = "true";
					else
						hidOutOrderUsed.Value = "false";
					//2005.08.21 자가/외주 재고 감안여부 추가 끝

				}
				reader.Close();
				conn.Close();

				PageLoad();
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
			this.btnClear.Click += new System.EventHandler(this.button_Click);
			this.btnSearch.Click += new System.EventHandler(this.button_Click);
			this.LinkButton1.Click += new System.EventHandler(this.LinkButton1_Click);
			this.btnProduce.Click += new System.EventHandler(this.btnProduce_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void PageLoad()
		{
			//	*************************************************
			//	**  품목분류1 드롭다운리스트... 데이타바인딩... **		
			//	*************************************************
			//코드분류표에서 대분류명이 품목분류1인 소분류명을 가지고 옴.
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str_ItemClassification1 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0210'" ;
			SqlCommand comm_ItemClassification1 = new SqlCommand(str_ItemClassification1,conn);
			SqlDataAdapter da_ItemClassification1 = new SqlDataAdapter(comm_ItemClassification1) ;
			DataSet ds_ItemClassification1 = new DataSet() ;
			da_ItemClassification1.Fill(ds_ItemClassification1);
				
			ddlItemClassification1.DataSource = ds_ItemClassification1;
			ddlItemClassification1.DataTextField = ds_ItemClassification1.Tables[0].Columns[0].ToString();
			ddlItemClassification1.DataValueField = ds_ItemClassification1.Tables[0].Columns[1].ToString();
			ddlItemClassification1.DataBind();
			ddlItemClassification1.Items.Insert(0, "-선택하세요-") ;
			ddlItemClassification1.Items[0].Value = "";
		}

		// TODO : 각 버튼 클릭시
		private void button_Click(object sender, System.EventArgs e)
		{
			// sender 매개 변수를 이용하여 이벤트를 발생시킨 버튼의 참조를 얻는다.
			Button button = (Button)sender;
			
			// 각 버튼 Click시 버튼의 CommandName에 따라 비디오버튼동작에 대한 객체를 호출한다.
			switch(button.CommandName.ToString())
			{
					// TODO : 검색버튼 클릭시
				case "Search" :
					// 검색객체 생성
					KIT_ERP.Search search = new KIT_ERP.Search("RowMaterialRequirementCalculate", ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum, ItemSearchControl1.ItemName, wdcMinDate, wdcMaxDate, ddlItemClassification1.SelectedItem.Value);
					uwgPP_HT.DataSource = search.DataSet_search();;
					uwgPP_HT.DataBind();

					if(uwgPP_HT.Rows.Count > 0)
						btnProduce.Enabled = true;
					else
						btnProduce.Enabled = false;
					break;

					// TODO : 초기화버튼 클릭시
				case "Clear" :
					// 생산시작일과 품목명 검색을 초기화
					wdcMinDate.Value = null;
					wdcMaxDate.Value = null;
					
					ItemSearchControl1.ClearTextBox();
					break;
			}
		}

		private DataSet Search()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = @"Select ItemNum, Sum(StockQuantity12) as ProductionPlanQuantity From PS_MT where RecodingState = 1 and [Year] ='2009' and StockQuantity12 <> 0 and ProcessCode != '14009999'  group by ItemNum ";
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);

			return ds;
		}


		// TODO : 순소요량, 발주의뢰량 계산 및 자재소요원장의 기록을 위해 자재소요원장형태의 DataSet의 현재내용 기록
		public void RowMaterialCalculate()
		{
			// 자재소요원장의 볼륨번호기록을 위해 볼륨번호를 구함
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			SqlCommand comm = new SqlCommand("SELECT MAX(VolumNum) AS VolumNum FROM MRC_HT", conn);

			int volumNum = 0;
			
			if(comm.ExecuteScalar() == null || comm.ExecuteScalar().ToString() == "")
			{
				volumNum = 1;
			}
			else
			{
				volumNum = int.Parse(comm.ExecuteScalar().ToString()) + 1;
			}

			foreach(DataRow row in dsRowMaterial1.Tables["RowMaterial"].Rows)
			{
				// 품목번호가 BomTree형태로 나오므로 그 부분을 수정
				row["ItemNum"] = row["ItemNum"].ToString().Trim();
				row["DeliveryOrderRequestQuantity"] = (decimal)row["DeliveryOrderRequestQuantity"];

				DataRow dsMRC_HT = dsRowMaterial1.Tables["MRC_HT"].NewRow();
				dsMRC_HT["ItemNum"] = row["ItemNum"].ToString();
				dsMRC_HT["ItemDrawNum"] = row["ItemDrawNum"].ToString();
				dsMRC_HT["ItemName"] = row["ItemName"].ToString();
				dsMRC_HT["DeliveryOrderRequestQuantity"] = (decimal)row["DeliveryOrderRequestQuantity"];
				dsMRC_HT["RowMaterialQuantityReflection"] = bool.Parse(hidPresentRowMaterialUsed.Value.ToString());
				dsMRC_HT["OrderNonInStorehouseReflection"] = false;
				dsMRC_HT["OrderRequestReadyQuantityReflection"] = false;
				dsMRC_HT["SafyRowMaterialQuantityReflection"] = bool.Parse(hidSafeyRowMaterialUsed.Value.ToString());
				dsMRC_HT["MinimumOrderQuantityReflection"] = false;
				dsMRC_HT["OrderGapQuantityReflection"] = false;
				
				dsMRC_HT["VolumNum"] = volumNum;
				dsMRC_HT["ProgressCondition"] = "대기";
				// 사용자 관련부분
				dsMRC_HT["RegistrationPerson"] = Session["ID"].ToString();
				dsMRC_HT["RegistrationPersonID"] = Session["UserName"].ToString();
				dsMRC_HT["RegistrationDate"] = DateTime.Now.ToShortDateString();
				dsMRC_HT["UpdatingPerson"] = "";
				dsMRC_HT["UpdatingPersonID"] = "";
				dsMRC_HT["UpdatingDate"] = DateTime.Now.ToShortDateString();
				dsRowMaterial1.Tables["MRC_HT"].Rows.Add(dsMRC_HT);
			}
		}

		public DataSet returnDateSet()
		{
			return dsRowMaterial1;
		}
		
		// TODO : 취소시 빌려씀...
		public void RowMaterialCalculatePC()
		{
			// 자재소요원장의 볼륨번호기록을 위해 볼륨번호를 구함
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			SqlCommand comm = new SqlCommand("SELECT MAX(VolumNum) AS VolumNum FROM MRC_HT", conn);

			int volumNum = 0;
			
			if(comm.ExecuteScalar() != null || comm.ExecuteScalar().ToString() != "")
			{
				volumNum = int.Parse(comm.ExecuteScalar().ToString()) + 1;
			}

			foreach(DataRow row in dsRowMaterial1.Tables["RowMaterial"].Rows)
			{
				// 품목번호가 BomTree형태로 나오므로 그 부분을 수정
				row["ItemNum"] = row["ItemNum"].ToString().Trim();
				row["DeliveryOrderRequestQuantity"] = (decimal)row["PureQuantity"];
				DataRow dsMRC_HT = dsRowMaterial1.Tables["MRC_HT"].NewRow();
				dsMRC_HT["ItemNum"] = row["ItemNum"].ToString();
				dsMRC_HT["ItemDrawNum"] = row["ItemDrawNum"].ToString();
				dsMRC_HT["ItemName"] = row["ItemName"].ToString();
				dsMRC_HT["DeliveryOrderRequestQuantity"] = (decimal)row["DeliveryOrderRequestQuantity"];
				dsRowMaterial1.Tables["MRC_HT"].Rows.Add(dsMRC_HT);
			}
		}

		/// <summary>
		/// TODO : 자재소요량 산출 
		/// </summary>
		/// <param name="itemNum"> 품목번호</param>
		/// <param name="productionPlanQuantity">생산계획량</param>
		/// <param name="productionPlanHistoryIndex">생산계획번호</param>
		public void RowMaterial(string itemNum,decimal productionPlanQuantity, int productionPlanHistoryIndex)
		{
			// 해당품목의 원자재 총 소요량을 산출
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			SqlCommand comm = new SqlCommand("dbo.RowBomTree", conn);
			comm.CommandType = CommandType.StoredProcedure;

			comm.Parameters.Add("@ItemNum", SqlDbType.VarChar, 30).Value = itemNum;
			comm.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = productionPlanQuantity;
			comm.Parameters.Add("@Tree", SqlDbType.Bit).Value = 1;

			SqlDataAdapter adapter = new SqlDataAdapter(comm);
			DataSet dsTotalRowMaterial = new DataSet();
				
			adapter.Fill(dsTotalRowMaterial);

			foreach(DataRow dsTotal in dsTotalRowMaterial.Tables[0].Rows)
			{
				if(dsRowMaterial1.Tables["RowMaterial"].Rows.Count <= 0)
				{
					DataRow row = dsRowMaterial1.Tables["RowMaterial"].NewRow();
					row["ItemNum"] = dsTotal["ItemNum"].ToString();
					row["ItemDrawNum"] = dsTotal["ItemDrawNum"].ToString();
					row["ItemName"] = dsTotal["ItemName"].ToString();
					row["TotalRowMaterialQuantityReflection"] = dsTotal["Quantity"].ToString();
					dsRowMaterial1.Tables["RowMaterial"].Rows.Add(row);
				}
				else
				{
					bool check = false;
					for(int i = 0; i < dsRowMaterial1.Tables["RowMaterial"].Rows.Count; i++)
					{
						if(dsTotal["ItemNum"].ToString().Trim() == dsRowMaterial1.Tables["RowMaterial"].Rows[i]["ItemNum"].ToString().Trim())
						{
							dsRowMaterial1.Tables["RowMaterial"].Rows[i]["TotalRowMaterialQuantityReflection"] = decimal.Parse(dsRowMaterial1.Tables["RowMaterial"].Rows[i]["TotalRowMaterialQuantityReflection"].ToString()) + decimal.Parse(dsTotal["Quantity"].ToString());
							check = true;
							break;
						}
					}

					if(!check)
					{
						DataRow row = dsRowMaterial1.Tables["RowMaterial"].NewRow();
						row["ItemNum"] = dsTotal["ItemNum"].ToString();
						row["ItemDrawNum"] = dsTotal["ItemDrawNum"].ToString();
						row["ItemName"] = dsTotal["ItemName"].ToString();
						row["TotalRowMaterialQuantityReflection"] = dsTotal["Quantity"].ToString();
						dsRowMaterial1.Tables["RowMaterial"].Rows.Add(row);
					}
				}
			}
			conn.Close();
		}

		/// <summary>
		/// 산출버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnProduce_Click(object sender, System.EventArgs e)
		{
			DataSet ds= new DataSet();
			
			KIT_ERP.Search search = new KIT_ERP.Search("RowMaterialRequirementCalculate", ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum, ItemSearchControl1.ItemName, wdcMinDate, wdcMaxDate);
			ds = search.DataSet_search();
//			ds = Search();
			for(int i = 0; i <uwgPP_HT.Rows.Count;i++)
			{
				// 체크되지 않으면 Value값이 null이다 그러므로 확인후 false값을 넣어준다.
				if(uwgPP_HT.Rows[i].Cells.FromKey("chk").Value == null)
				{
					uwgPP_HT.Rows[i].Cells.FromKey("chk").Value = false;
				}
				if(!bool.Parse(uwgPP_HT.Rows[i].Cells.FromKey("chk").Value.ToString()))
				{
					DataRow[] row = ds.Tables[0].Select("ProductionPlanHistoryIndex = '" + uwgPP_HT.Rows[i].Cells.FromKey("ProductionPlanHistoryIndex").Value + "'");
					ds.Tables[0].Rows.Remove(row[0]);
				}
			}

			
			if(ds.Tables[0].Rows.Count == 0)
			{
				Response.Write("<script>alert('선택된 품목이 없습니다.');</script>");
			}
			else
			{
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				
				string str = "Delete From TBTotalNeed";
				SqlCommand comm = new SqlCommand(str,conn);
				conn.Open();
				comm.ExecuteNonQuery();
				conn.Close();
				//총소요량 계산
				TotalNeedCalculation(ds);

				if(hidPresentRowMaterialUsed.Value == "false"  && (hdWorkPlan1.Value == "true" || hdWorkPlan3.Value =="true"))
					goto LinePureNeedCalculation ;
				//잉여 재고 테이블 생성(생산,외주,원자재)
				RedundantStockTableMaking();

				//작업계획 잔량 처리
				WorkRemainderProcessing();

				//외주잔량 처리
				OutOrderRemainderProcessing();

				//자가외주 재고 통합
				InOutStockSumming();

				//완성제품 제거하기
				LastProgressStockRemoving();

				//반제품생산계획 수량제거하기
				//PP_HalfProductStockRemoving();

				//품목 통합하기
				ItemGrouping();

				//생산계획 반영하기(생산계획대기 각 품목에 대해 품목구성을 이용해 요구량 처리 재귀함수 호출)
				ProductionPlanConsidering() ;

				//재고부족분 보상(생산잉여창고의 부족품목에 대해 품목구성을 이용해 요구량 처리 재귀함수 호출)
				MinusStockCompensating();

				//순소요량 산출(소요량 산출 요구품목에 대해 품목구성을 이용해 요구량 처리 재귀함수 호출)
				PureNeedCalculation(ds) ;

			LinePureNeedCalculation :
				//실제 소요량 산출
				PureNeed();
                
				//자재의뢰대기량,구매의뢰대기량,구매발주잔량,안전재고량 계산
				PurchasingQuantityCalculation();

				//계산 결과 보여주기
				ShowResult() ;

				
				//자재의뢰원장에 기록하는 함수
				RowMaterialCalculate();

				Session["dataSet"] = dsRowMaterial1;
				Session["dataGrid"] = uwgPP_HT;

				if(a == 0)
					//Page.RegisterClientScriptBlock("SEND", "<script>window.open('./PopupWindows/RowMaterialRequirementPC.aspx','','width=1006px,height=340px,center=yes,help=no,resizable=no,status=no,menubar=no,toolbar=no');</script>");
					Page.RegisterClientScriptBlock("SEND", "<script>window.open('./PopupWindows/RowMaterialRequirement.aspx','','width=910px,height=370px,center=yes,help=no,resizable=no,status=no,menubar=no,toolbar=no');</script>");
			}
		}

		private void PureNeed()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "";
			str = @"delete From TBPureNeed";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.CommandText = str;
			comm.ExecuteNonQuery();

			//재고고려여부
			if(hidPresentRowMaterialUsed.Value == "false")
				str = @"Insert into TBPureNeed
							Select * From TBTotalNeedResult";
			else
				str = @"Insert into TBPureNeed
							Select * From TBRawRedundantResult";
			comm.CommandText = str;
			comm.ExecuteNonQuery();
			conn.Close();
		}

		/// <summary>
		/// 총소요량 계산
		/// </summary>
		private void TotalNeedCalculation(DataSet dataset)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.CommandType = CommandType.StoredProcedure;
			

			//선택된 제품들을 돌면서 필요한 원자재들을 TBTotalNeed 테이블에 하나씩 추가한다. 이때
			// 동일한 원자재들이 중복될수 있다.
			foreach(DataRow dr in dataset.Tables[0].Rows)
			{
				str = "SPTotalNeedCalculation1";
				comm.CommandText = str;
				comm.Parameters.Add("@ItemNum", SqlDbType.VarChar).Value = dr["ItemNum"].ToString();
				comm.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Decimal.Parse(dr["ProductionPlanQuantity"].ToString());
				comm.Parameters.Add("@Tree", SqlDbType.Bit).Value = 1;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}

			// TBTotalNeed 테이블을 Groupby 하면 실제 해당 제품들을 생산하는데 필요한 원자재 수량이 나온다
			// 이 원자재들에 대해 TBTotalNeedResult에 넣어둔다
			str = "SPTotalNeedCalculation2";
			comm.CommandText = str;
			comm.ExecuteNonQuery();

			conn.Close();
		}


		/// <summary>
		/// 생산창고,원자재창고, 외주창고에 대해 잉여테이블을 생성
		/// 앞으로 이 생성된 테이블을 가지고 모든 작업을 한다
		/// </summary>
		private void RedundantStockTableMaking()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);

			string str = "SPRedundantStockTableMaking";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.CommandType = CommandType.StoredProcedure;
			conn.Open();
			comm.ExecuteNonQuery();
			conn.Close();

			//생성된 원자재창고 잉여테이블 : TBRawRedundant, 
			//생성된 생산창고 잉여테이블 : TBWorkRedundant,
			//생성된 외주창고 잉여테이블 : TBOutOrderRedundant(품목별로 Group By 되어 있음)
		}


		/// <summary>
		/// 작업계획 대기,지시,진행 인 잔량을 각종 잉여테이블에서 처리하는 함수
		/// </summary>
		private void WorkRemainderProcessing()
		{

			//자가공정의 잔량처리
			InWorkRemainderProcessing();
			//외주공정의 잔량 처리
			OutWorkRemainderProcessing();

			
		}



		/// <summary>
		/// 작업계획원장의 자가 공정의 진행상태가 대기, 지시, 진행인 잔량을 처리하는 함수
		/// </summary>
		private void InWorkRemainderProcessing()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);

			//작업계획원장의 진행상태가 대기,지시, 진행인 공정품들을 모두 Select 해온다.(작업구분이 외주이던 자가이던 상관 없음)
			// 외주인경우라도 진행상태가 대기인 것들은 아직 외주의뢰가 나가지 않은 공정품들이므로 생산잉여창고 테이블에서 수량을 중가 혹은 감소시킨다.
			string str = "SPInWorkRemainderQuantity";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.CommandType = CommandType.StoredProcedure;
			conn.Open();
			DataSet dataset = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(comm);
			da.Fill(dataset);

			//해당 공정품들에 대해 각 공정품을 더해주고 하위공정품을 마이너스 해준다.
			foreach(DataRow dr in dataset.Tables[0].Rows)
			{
				// 현재품목과 공정코드,수량을 저장한 다음 현재공정품과 바로 아래 하위품목의 모든 공정품을 가져와서 
				// 해당 품목공정의 다음레코드를 선택해서 Lvl이 1이면 다음레코드의 공정품 수량만큼 잉여테이블에 수량을 감소시키고
				// 다음레코드 Lvl이 2이면 Lvl 2인 공정품들중 최종 공정품만을 선택해서 잉여테이블에 해당 수량만큼 마이너스 시킨다.
				// 그리고 나서 현재레코드의 공정품에 대해 잉여테이블에 해당 수량만큼 증가시킨다.
				string ItemNum = dr["ItemNum"].ToString();
				string ProcessCode = dr["ProcessCode"].ToString();
				decimal Quantity = decimal.Parse(dr["Quantity"].ToString());
				str = "SPWorkRemainderProcessing";
				comm.CommandText = str;
				comm.Parameters.Add("@ItemNum",ItemNum);
				comm.Parameters.Add("@Quantity",Quantity);
				comm.Parameters.Add("@Tree",1);
				DataSet dsItem = new DataSet();
				SqlDataAdapter daItem = new SqlDataAdapter(comm);
				daItem.Fill(dsItem);
				comm.Parameters.Clear();
				for(int Item = 0 ; Item < dsItem.Tables[0].Rows.Count; Item++)
				{
					if(ItemNum == dsItem.Tables[0].Rows[Item]["ItemNum"].ToString().Trim() &&
						ProcessCode == dsItem.Tables[0].Rows[Item]["ProcessCode"].ToString().Trim())
					{
						//현재공정의 다음레코드를 선택
						// 1이면 다음레코드의 품목과 공정으로 창고수량을 Quantity만큼 마이너스
						if(dsItem.Tables[0].Rows[Item+1]["Lvl"].ToString().Trim() == "1")
						{
								
							str = "SPWorkRemainderProcessingMinus";
							comm.CommandText = str;
							
							//comm.Parameters.Add("@ItemNum",dsItem.Tables[0].Rows[Item+1]["ItemNum"].ToString());
							//comm.Parameters.Add("@Quantity",decimal.Parse(dsItem.Tables[0].Rows[Item+1]["Quantity"].ToString()));
							
							comm.Parameters.Add("@ItemNum",ItemNum);
							comm.Parameters.Add("@Quantity",Quantity);
							comm.Parameters.Add("@ProcessCode",dsItem.Tables[0].Rows[Item+1]["ProcessCode"].ToString());
							comm.ExecuteNonQuery();
							comm.Parameters.Clear();
								
						}
						else
						{
							//Level2의 모든 레코드들에 대해 첫레코드를 선택해 수량을 마이너스 시킨다
							str = "SPWorkRemainderProcessingNextLvl";
							comm.CommandText = str;
							DataSet dsNext = new DataSet();
							SqlDataAdapter daNext = new SqlDataAdapter(comm);
							daNext.Fill(dsNext);
							
							foreach(DataRow drNext in dsNext.Tables[0].Rows)
							{
								str = "SPWorkRemainderProcessingMinus";
								comm.CommandText = str;
								comm.Parameters.Add("@ItemNum",drNext["ItemNum"].ToString());
								comm.Parameters.Add("@Quantity",decimal.Parse(drNext["Quantity"].ToString()));
								comm.Parameters.Add("@ProcessCode",drNext["ProcessCode"].ToString());
								comm.ExecuteNonQuery();
								comm.Parameters.Clear();
							}
						}
						comm.Parameters.Clear();

						// 해당공정을 플러스 시킨다
						str = "SPWorkRemainderProcessingPlus";
						comm.CommandText = str;
						comm.Parameters.Add("@ItemNum",dsItem.Tables[0].Rows[Item]["ItemNum"].ToString());
						comm.Parameters.Add("@Quantity",decimal.Parse(dsItem.Tables[0].Rows[Item]["Quantity"].ToString()));
						comm.Parameters.Add("@ProcessCode",dsItem.Tables[0].Rows[Item]["ProcessCode"].ToString());
						comm.ExecuteNonQuery();
						comm.Parameters.Clear(); 
						break;
					}
				}
			}
			conn.Close();
		}

		/// <summary>
		/// 작업계획원장의 작업구분이 외주인 공정품의 대기량을 처리하는 함수
		/// </summary>
		private void OutWorkRemainderProcessing()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);

			//작업계획원장의 진행상태가 대기,지시, 진행인 공정품들을 모두 Select 해온다.(작업구분이 외주이던 자가이던 상관 없음)
			// 외주인경우라도 진행상태가 대기인 것들은 아직 외주의뢰가 나가지 않은 공정품들이므로 생산잉여창고 테이블에서 수량을 중가 혹은 감소시킨다.
			string str = "SPOutWorkRemainderQuantity";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.CommandType = CommandType.StoredProcedure;
			conn.Open();
			DataSet dsOut = new DataSet();
			SqlDataAdapter daOut = new SqlDataAdapter(comm);
			daOut.Fill(dsOut);
			foreach(DataRow row in dsOut.Tables[0].Rows)
			{
				// 현재품목과 공정코드,수량을 저장한다
				string ItemNum = row["ItemNum"].ToString();
				string ProcessCode = row["ProcessCode"].ToString();
				decimal Quantity = decimal.Parse(row["Quantity"].ToString());
				str = "SPWorkRemainderProcessing";
				comm.CommandText = str;
				comm.CommandType = CommandType.StoredProcedure;
				comm.Parameters.Add("@ItemNum",ItemNum);
				comm.Parameters.Add("@Quantity",Quantity);
				comm.Parameters.Add("@Tree",1);
				DataSet dsItem = new DataSet();
				SqlDataAdapter daItem = new SqlDataAdapter(comm);
				daItem.Fill(dsItem);
				comm.Parameters.Clear();

				for(int Item = 0 ; Item < dsItem.Tables[0].Rows.Count; Item++)
				{
					if(ItemNum == dsItem.Tables[0].Rows[Item]["ItemNum"].ToString().Trim() &&
						ProcessCode == dsItem.Tables[0].Rows[Item]["ProcessCode"].ToString().Trim())
					{
						//현재공정의 다음레코드를 선택
						// 1이면 다음레코드의 품목과 공정으로 창고 정리
						if(dsItem.Tables[0].Rows[Item+1]["Lvl"].ToString().Trim() == "1")
						{
								
							str = "SPWorkRemainderProcessingMinus";
							comm.CommandText = str;
							comm.Parameters.Add("@ItemNum",dsItem.Tables[0].Rows[Item+1]["ItemNum"].ToString());
							comm.Parameters.Add("@Quantity",decimal.Parse(dsItem.Tables[0].Rows[Item+1]["Quantity"].ToString()));
							comm.Parameters.Add("@ProcessCode",dsItem.Tables[0].Rows[Item+1]["ProcessCode"].ToString());
							comm.ExecuteNonQuery();
							comm.Parameters.Clear();
								
						}
						else
						{
							//Level2의 모든 레코드들에 대해 첫레코드를 선택해 수량을 마이너스 시킨다
							str = "SPWorkRemainderProcessingNextLvl";
							comm.CommandText = str;
							DataSet dsNext = new DataSet();
							SqlDataAdapter daNext = new SqlDataAdapter(comm);
							daNext.Fill(dsNext);
							
							foreach(DataRow drNext in dsNext.Tables[0].Rows)
							{
								str = "SPWorkRemainderProcessingMinus";
								comm.CommandText = str;
								comm.Parameters.Add("@ItemNum",drNext["ItemNum"].ToString());
								comm.Parameters.Add("@Quantity",decimal.Parse(drNext["Quantity"].ToString()));
								comm.Parameters.Add("@ProcessCode",drNext["ProcessCode"].ToString());
								comm.ExecuteNonQuery();
								comm.Parameters.Clear();
							}
						}
						comm.Parameters.Clear();

						// 해당공정을 플러스 시킨다
						str = "SPWorkRemainderProcessingPlus";
						comm.CommandText = str;
						comm.Parameters.Add("@ItemNum",dsItem.Tables[0].Rows[Item]["ItemNum"].ToString());
						comm.Parameters.Add("@Quantity",decimal.Parse(dsItem.Tables[0].Rows[Item]["Quantity"].ToString()));
						comm.Parameters.Add("@ProcessCode",dsItem.Tables[0].Rows[Item]["ProcessCode"].ToString());
						comm.ExecuteNonQuery();
						comm.Parameters.Clear(); 
						break;
					}
				}
			}
			conn.Close();
		}
		


		/// <summary>
		/// 외주 잔량 처리함수
		/// 외주 의뢰원장의 시작
		/// </summary>
		private void OutOrderRemainderProcessing()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "SPOutOrderRemainderProcessing";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.CommandType = CommandType.StoredProcedure;
			DataSet dataset = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(comm);
			da.Fill(dataset);
			comm.CommandType = CommandType.Text;
			foreach(DataRow dr in dataset.Tables[0].Rows)
			{
				//해당 종료공정의 작업구분이 외주인 경우는 무조건 생산잉여창고에 수량을 증가시킨다
				if(Division(dr["ItemNum"].ToString(),dr["EndProcessCode"].ToString()) == true)
				{
					str = @"Update TBWorkRedundant Set  Quantity = (Quantity + @Quantity) Where ItemNum = @ItemNum and ProcessCode = @ProcessCode";
					comm.CommandText = str;
					comm.CommandType = CommandType.Text;
					comm.Parameters.Add("@ItemNum",dr["ItemNum"].ToString());
					comm.Parameters.Add("@ProcessCode",dr["EndProcessCode"].ToString());
					comm.Parameters.Add("@Quantity",decimal.Parse(dr["OrderQuantity"].ToString()));
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
				
					ArrayList arr = SubItemSearch(dr["ItemNum"].ToString(),dr["BeginProcessCode"].ToString(),decimal.Parse(dr["OrderQuantity"].ToString()));

					for(int count = 0; count < arr.Count; count++)
					{
						WCSub wcsub = new WCSub();
						wcsub = (WCSub)arr[count];
				
						str = "Update TBOutOrderRedundant Set Quantity = Quantity - @Quantity Where ItemNum = @ItemNum and ProcessCode = @ProcessCode";
						comm.CommandText = str;
						comm.Parameters.Add("@ItemNum",wcsub.ItemNum);
						comm.Parameters.Add("@ProcessCode",wcsub.ProcessCode);
						comm.Parameters.Add("@Quantity",wcsub.Quantity);

				
						//만약 외주창고에 해당 자품목들이 존재하지 않으면 오류메시지를 출력하고 강제종료시켜야 한다.
						int check = comm.ExecuteNonQuery();
						comm.Parameters.Clear();
						
						if(check == 0)
						{
							a = 1;
							RegisterClientScriptBlock("","<script>alert('"+dr["ItemNum"].ToString() + " 품목의 하위품목들에 대한 외주창고가 생성되어 있지 않습니다!');</script>");
							RegisterClientScriptBlock("","<script>location.href = RowMaterialRequirementCalculate.aspx;</script>");
						}
					}
					arr.Clear();
			
				}
				else
				{
					//자가/외주 재고 감안여부가 true이면 해당 수량들에 대한 처리를 한다.
					if(hidOutOrderUsed.Value.ToString() == "true")
					{
						str = @"Update TBWorkRedundant Set  Quantity = (Quantity + @Quantity) Where ItemNum = @ItemNum and ProcessCode = @ProcessCode";
						comm.CommandText = str;
						comm.CommandType = CommandType.Text;
						comm.Parameters.Add("@ItemNum",dr["ItemNum"].ToString());
						comm.Parameters.Add("@ProcessCode",dr["EndProcessCode"].ToString());
						comm.Parameters.Add("@Quantity",decimal.Parse(dr["OrderQuantity"].ToString()));
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();
				
						ArrayList arr = SubItemSearch(dr["ItemNum"].ToString(),dr["BeginProcessCode"].ToString(),decimal.Parse(dr["OrderQuantity"].ToString()));

						for(int count = 0; count < arr.Count; count++)
						{
							WCSub wcsub = new WCSub();
							wcsub = (WCSub)arr[count];

							str = "Update TBOutOrderRedundant Set Quantity = Quantity - @Quantity Where ItemNum = @ItemNum and ProcessCode = @ProcessCode";
							comm.CommandText = str;
							comm.Parameters.Add("@ItemNum",wcsub.ItemNum);
							comm.Parameters.Add("@ProcessCode",wcsub.ProcessCode);
							comm.Parameters.Add("@Quantity",wcsub.Quantity);

					
							//만약 외주창고에 해당 자품목들이 존재하지 않으면 오류메시지를 출력하고 강제종료시켜야 한다.
							int check = comm.ExecuteNonQuery();
							comm.Parameters.Clear();
						
							if(check == 0)
							{
								a = 1;
								RegisterClientScriptBlock("","<script>alert('"+dr["ItemNum"].ToString() + " 품목의 하위품목들에 대한 외주창고가 생성되어 있지 않습니다!');</script>");
								RegisterClientScriptBlock("","<script>location.href = RowMaterialRequirementCalculate.aspx;</script>");
							}
						}
						arr.Clear();
					}
					else
					{
				
				
						//외주잉여창고에 해당 품목의 하위품목 수량을 '0'으로 맞춰준다
						ArrayList arr = SubItemSearch(dr["ItemNum"].ToString(),dr["BeginProcessCode"].ToString(),decimal.Parse(dr["OrderQuantity"].ToString()));

						for(int count = 0; count < arr.Count; count++)
						{
							WCSub wcsub = new WCSub();
							wcsub = (WCSub)arr[count];
					
							str = "Update TBOutOrderRedundant Set Quantity = 0 Where ItemNum = @ItemNum and ProcessCode = @ProcessCode";
							comm.CommandText = str;
							comm.CommandType = CommandType.Text;
							comm.Parameters.Add("@ItemNum",wcsub.ItemNum);
							comm.Parameters.Add("@ProcessCode",wcsub.ProcessCode);
							comm.Parameters.Add("@Quantity",wcsub.Quantity);

							//만약 외주창고에 해당 자품목들이 존재하지 않으면 오류메시지를 출력하고 강제종료시켜야 한다.
							int check = comm.ExecuteNonQuery();
							comm.Parameters.Clear();
						
							if(check == 0)
							{
								a = 1;
								RegisterClientScriptBlock("","<script>alert('"+dr["ItemNum"].ToString() + " 품목의 하위품목들에 대한 외주창고가 생성되어 있지 않습니다!');</script>");
								RegisterClientScriptBlock("","<script>location.href = RowMaterialRequirementCalculate.aspx;</script>");
							}
						}
						arr.Clear();
					}
				}
			}
			conn.Close();
		}


		/// <summary>
		/// 출고 품목 찾기 함수
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <param name="ProcessCode"></param>
		/// <param name="Quantity"></param>
		/// <returns></returns>
		private ArrayList SubItemSearch(string ItemNum, string ProcessCode,decimal Quantity)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			//품목을 던져주고
			string str = "SPDeliveryItemBOM";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.CommandType = CommandType.StoredProcedure;
			comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = ItemNum;
			comm.Parameters.Add("@Quantity",SqlDbType.VarChar).Value = Quantity;
			DataSet dataset = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(comm);
			da.Fill(dataset);

			// BOM을 공정순서와 조인한 결과를 가지고
			// 해당 품목과 공정정보와 동일한지 처음부터 내려가면서
			// 비교해본다. 만약 동일한 품목과 공정이 발견되면 해당
			// 행의 위치값을 가지고 빠져나온다.
			int count = 0;
			bool process = false;
			for( ; count < dataset.Tables[0].Rows.Count; count++)
			{
				if(dataset.Tables[0].Rows[count]["Lvl"].ToString() == "1" && 
					dataset.Tables[0].Rows[count]["ProcessCode"].ToString() == ProcessCode)
				{
					process = true;
					break;
				}
			}

			if(process == true)
			{
			
				// 선택된 count에 위치한 공정순서가 해당품목의 최하위 공정보다 크면
				// 출고품목은 count+1 에 위치한 품목의 공정이 된다.
				// 만일 최하위공정과 선택 품목의 공정순서가 동일하면
				// 하위품목들중의 출고품목을 찾아가야한다.
				if(int.Parse(dataset.Tables[0].Rows[count]["ProcessSequenceNum"].ToString()) > MinProcessSequenceNum(dataset.Tables[0].Rows[count]["ItemNum"].ToString()))
				{
					//해당공품의 바로 아랫단계에 있는 공정코드와 수량을 List에 담아 리턴해준다
					WCSub wcsub = new WCSub();
					wcsub.ItemNum = dataset.Tables[0].Rows[count+1]["ItemNum"].ToString();
					wcsub.ProcessCode = dataset.Tables[0].Rows[count+1]["ProcessCode"].ToString();
					wcsub.Quantity = decimal.Parse(dataset.Tables[0].Rows[count+1]["Quantity"].ToString());
					List.Add(wcsub);
				}
				else
				{
					//그렇지 않으면 선택된 공정품의 바로 아랫단계에서 시작해 끝까지 돌면서
					//출고품목들을 찾아낸다.
					for(int rowcount = count+1; rowcount < dataset.Tables[0].Rows.Count; rowcount++)
					{
						//선택된 품목의 공정이 없는 원자재인경우 원자재를 List에 담아둔다
						if(int.Parse(dataset.Tables[0].Rows[rowcount]["ProcessSequenceNum"].ToString()) == 0)
						{											 
							WCSub wcsub = new WCSub();
							wcsub.ItemNum = dataset.Tables[0].Rows[rowcount]["ItemNum"].ToString();
							wcsub.ProcessCode = "14000000";
							wcsub.Quantity = decimal.Parse(dataset.Tables[0].Rows[rowcount]["Quantity"].ToString());
							List.Add(wcsub);
							continue;
						}
						else
						{
							// 원자재가 아닌경우는 선택 품목의 공정의 작업구분이 자가인지 외주인지 판단한다
							// 만일 해당 공정이 자가인경우에는 해당 공정품이 출고품목이 된다. 
							// 또한 주의할 것은 해당 공정품 이후에 달려있는 하위품목들의 레코드는 삭제해주어야 한다
							// 삭제 대상은 해당 공정의 Lvl 값과 동일한 값을 가진 레코드를 만날때까지 삭제한다.
							if(Division(dataset.Tables[0].Rows[rowcount]["ItemNum"].ToString(),int.Parse(dataset.Tables[0].Rows[rowcount]["ProcessSequenceNum"].ToString())) == true)
							{
								//해당 공정이 자가이므로 해당 품목이 출고품목이 된다.
								WCSub wcsub = new WCSub();
								wcsub.ItemNum = dataset.Tables[0].Rows[rowcount]["ItemNum"].ToString();
								wcsub.ProcessCode = dataset.Tables[0].Rows[rowcount]["ProcessCode"].ToString();
								wcsub.Quantity = decimal.Parse(dataset.Tables[0].Rows[rowcount]["Quantity"].ToString());
								List.Add(wcsub);
							
								// 출고품목이 확정된 경우 지금 공정의 하위에 달려있는 공정들은 의미가 없으므로
								// 넘어간다
								// 단 차례로 넘어가다 보면 해당 공정의 Lvl값과 동일한 값이나 그이상을 가지는 레코드를 만나게
								// 될 수 있는데 이때 빠져나온다.(물론 해당 품목과 동일한 품목인 경우는 삭제해야 한다)
								int a = rowcount;
								for( ; a< dataset.Tables[0].Rows.Count; a++)
								{
									// 품목번호가 다르고 Lvl값이 동일하거나 그 이상의 레코드를 만날때까지 반복한다.
									if(dataset.Tables[0].Rows[rowcount]["ItemNum"].ToString() != dataset.Tables[0].Rows[a]["ItemNum"].ToString() &&
										int.Parse(dataset.Tables[0].Rows[rowcount]["Lvl"].ToString()) >= int.Parse(dataset.Tables[0].Rows[a]["Lvl"].ToString()))
										break;
								}
								rowcount = a-1;
								continue;
							}
							else
							{
								// 선택된 공정품의 작업구분이 자가가 아닌경우에는
								// 외주단가테이블에 가서 해당 공정품이 단가 테이블의 종료공정으로
								// 존재하는 레코드가 있는가 확인한다.
								// 단가테이블의 종료공정으로 존재하면 이 공정품이 출고 품목이다.
								if(ExistenceRecord(dataset.Tables[0].Rows[rowcount]["ItemNum"].ToString(),dataset.Tables[0].Rows[rowcount]["ProcessCode"].ToString()) >=1)
								{
									//해당 품목이 출고품목이 된다.
									WCSub wcsub = new WCSub();
									wcsub.ItemNum = dataset.Tables[0].Rows[rowcount]["ItemNum"].ToString();
									wcsub.ProcessCode = dataset.Tables[0].Rows[rowcount]["ProcessCode"].ToString();
									wcsub.Quantity = decimal.Parse(dataset.Tables[0].Rows[rowcount]["Quantity"].ToString());
									List.Add(wcsub);


									// 출고품목이 확정된 경우 지금 공정의 하위에 달려있는 공정들은 의미가 없으므로
									// 넘어간다
									// 단 차례로 넘어가다 보면 해당 공정의 Lvl값과 동일한 값을 가지는 레코드를 만나게
									// 될 수 있는데 이때 빠져나온다.(물론 해당 품목과 동일한 품목인 경우는 삭제해야 한다)
									int a = rowcount;
									for( ; a< dataset.Tables[0].Rows.Count; a++)
									{
										// 품목번호가 다르고 Lvl값이 동일하거나 그 이상의 레코드를 만날때까지 반복한다.
										if(dataset.Tables[0].Rows[rowcount]["ItemNum"].ToString() != dataset.Tables[0].Rows[a]["ItemNum"].ToString() &&
											int.Parse(dataset.Tables[0].Rows[rowcount]["Lvl"].ToString()) >= int.Parse(dataset.Tables[0].Rows[a]["Lvl"].ToString()))
											break;
									}
									rowcount = a-1;
									continue;
								}
							}
						}
					}
				}
				return List;
			}
			else
			{
				a = 1;
				RegisterClientScriptBlock("","<script>alert('품목에 해당공정이 존재하지 않습니다!');</script>");
				RegisterClientScriptBlock("","<script>location.href = RowMaterialRequirementCalculate.aspx;</script>");

				return null;
			}
		}


		private int ExistenceRecord(string ItemNum, string ProcessCode)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "Select Count(*) From UCI_MT Where RecodingState = 1 and ItemNum = @ItemNum and EndProcessCode = @ProcessCode";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@ItemNum",ItemNum);
			comm.Parameters.Add("@ProcessCode",ProcessCode);
			comm.ExecuteNonQuery();
			int result = int.Parse(comm.ExecuteScalar().ToString());
			conn.Close();
			return result;
		}


		/// <summary>
		/// 생산계획원장에 있는 반제품 재고 수량반영
		/// </summary>
		private void PP_HalfProductStockRemoving()
		{
			string Code = "";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = @"Select PP_HT.ItemNum as ItemNum, ProductionPlanQuantity From PP_HT inner join II_MT
				on PP_HT.ItemNum = II_MT.ItemNum where PropertyClassification = '반제품' 
				and RecodingState = 1 and RowMaterialCalculation = 1 and ProgressCondition = '완료'";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.CommandType = CommandType.Text;
			DataSet dataset = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(comm);
			da.Fill(dataset);
			foreach(DataRow dr in dataset.Tables[0].Rows)
			{
				//해당품목의 최상위공정코드
				Code = FindCode(dr["ItemNum"].ToString());

				str = @"Update TBWorkRedundantSum  Set Quantity = Quantity - @Quantity where TBWorkRedundantSum.ItemNum = @ItemNum and ProcessCode = @Code";
				comm.CommandText = str;
				comm.CommandType = CommandType.Text;
				comm.Parameters.Add("@ItemNum",dr["ItemNum"].ToString());
				comm.Parameters.Add("@Quantity",decimal.Parse(dr["ProductionPlanQuantity"].ToString()));
				comm.Parameters.Add("@Code",Code);
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
			
			conn.Close();
		}

		/// <summary>
		/// 해당 품목의 최종공정 찾는 함수
		/// </summary>
		/// <param name="Item"></param>
		/// <returns></returns>
		private string FindCode(string Item)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string code = "";
			string str = @"Select ProcessCode From PSI_MT where ItemNum = @ItemNum and RecodingState = 1 
						and ProcessSequenceNum = (Select Max(P.ProcessSequenceNum) From PSI_MT P where P.RecodingState = 1 and P.ItemNum = @ItemNum)";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@ItemNum",Item);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				code = dr["ProcessCode"].ToString();
			}
			dr.Close();
			conn.Close();

			return code;
		}



		/// <summary>
		/// 자가 외주 재고 통합 함수
		/// </summary>
		private void InOutStockSumming()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "SPInOutStockSumming";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.CommandType = CommandType.StoredProcedure;
			comm.ExecuteNonQuery();
			
			conn.Close();

		}

		/// <summary>
		/// 최종 공정 제품 제거함수
		/// </summary>
		private void LastProgressStockRemoving()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = @"Select * From TBWorkRedundantSum  where TBWorkRedundantSum.ProcessSequenceNum = (Select Max(T2.ProcessSequenceNum) from TBWorkRedundantSum T2 where T2.ItemNum = TBWorkRedundantSum.ItemNum Group by T2.ItemNum)";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.CommandType = CommandType.Text;
			DataSet dataset = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(comm);
			da.Fill(dataset);
			foreach(DataRow dr in dataset.Tables[0].Rows)
			{
				if(decimal.Parse(dr["Quantity"].ToString()) != 0 && Classification(dr["ItemNum"].ToString()))
				{
					str = @"Update TBWorkRedundantSum  Set Quantity = 0 where TBWorkRedundantSum.ItemNum = @ItemNum and ProcessCode = @Code";
					comm.CommandText = str;
					comm.CommandType = CommandType.Text;
					comm.Parameters.Add("@ItemNum",dr["ItemNum"].ToString());
					comm.Parameters.Add("@Code",dr["ProcessCode"].ToString());
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
				}
			}
			
			conn.Close();

		}

		/// <summary>
		/// 품목 통합 함수
		/// </summary>
		private void ItemGrouping()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "SPItemGrouping";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.CommandType = CommandType.StoredProcedure;
			comm.ExecuteNonQuery();
			
			conn.Close();

		}

		/// <summary>
		/// 생산계획반영함수
		/// </summary>
		private void ProductionPlanConsidering()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			//먼저 생산게획 대기 제품에 대한 재귀 처리를 위한 테이블 생성한다
			string str  = @"SPRedundantItemPlanResult";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.CommandType = CommandType.StoredProcedure;
			conn.Open();
			comm.ExecuteNonQuery();
			conn.Close();

			//생산계획원장에 대기 수량을 가져와 생성한 테이블에서 해당 수량만큼 감안해준다.
			str = @"Select ItemNum, Sum(ProductionPlanQuantity) as Quantity
				From PP_HT Where RowMaterialCalculation = '1' and ProgressCondition = '대기'
				Group by ItemNum";
			comm.CommandText = str;
			comm.CommandType = CommandType.Text;
			DataSet dsPP_HT = new DataSet();
			SqlDataAdapter daPP_HT = new SqlDataAdapter(comm);
			daPP_HT.Fill(dsPP_HT);
			foreach(DataRow drPP_HT in dsPP_HT.Tables[0].Rows)
			{
				decimal ProductQuantity = decimal.Parse(drPP_HT["Quantity"].ToString());
				ItemNeedProcessingPlan(drPP_HT["ItemNum"].ToString(),ProductQuantity);	
			}
		}

		/// <summary>
		/// 재고 부족분 보상 함수
		/// </summary>
		private void MinusStockCompensating()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			//먼저 재고부족분을 처리하기위한 테이블 생성한다
			string str  = @"SPRedundantItemLastResult";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.CommandType = CommandType.StoredProcedure;
			conn.Open();
			comm.ExecuteNonQuery();
			conn.Close();

			str = "Select * From TBWorkRedundantItemLastResult Where Quantity < 0";
			comm.CommandText = str;
			comm.CommandType = CommandType.Text;
			DataSet dataset = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(comm);
			da.Fill(dataset);
			foreach(DataRow dr in dataset.Tables[0].Rows)
			{
				decimal ProductQuantity = decimal.Parse(dr["Quantity"].ToString());
				ItemNeedProcessingLast(dr["ItemNum"].ToString(),-ProductQuantity);	
			}
		}

		/// <summary>
		/// 순소요량 산출 함수
		/// </summary>
		private void PureNeedCalculation(DataSet dataset)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			// 실제 순소요량을 처리하기 위한 테이블을 생성한다.
			string str  = @"SPRedundantResult";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.CommandType = CommandType.StoredProcedure;
			conn.Open();
			comm.ExecuteNonQuery();
			conn.Close();


			foreach(DataRow dr in dataset.Tables[0].Rows)
			{
				decimal ProductQuantity = decimal.Parse(dr["ProductionPlanQuantity"].ToString());
				ItemNeedProcessing(dr["ItemNum"].ToString(),ProductQuantity);	
			}

		}

		/// <summary>
		/// 각종 구매발주 의뢰량 계산 함수
		/// </summary>
		private void PurchasingQuantityCalculation()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = @"SPPurchasingQuantityCalculation";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.CommandType = CommandType.StoredProcedure;
			conn.Open();
			comm.ExecuteNonQuery();
			conn.Close();

		}

		/// <summary>
		///  계산 결과 보여주는 함수
		/// </summary>
		private void ShowResult()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			decimal Safety = 0;
			string str = @"SPPurchasingQuantityCalculation";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.CommandType = CommandType.StoredProcedure;
			DataSet dataset = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(comm);
			da.Fill(dataset);
			
			foreach(DataRow dr in dataset.Tables[0].Rows)
			{	
				if(IOCheck(dr["ItemNum"].ToString()))
				{
					DataRow row = dsRowMaterial1.Tables["RowMaterial"].NewRow();

					row["ItemNum"] = dr["ItemNum"].ToString();
					row["ItemDrawNum"] = dr["ItemDrawNum"].ToString();
					row["ItemName"] = dr["ItemName"].ToString();
					row["Unit"] = Unit(dr["ItemNum"].ToString());//단위
					row["Standard"] = Standard(dr["ItemNum"].ToString());//규격
					row["TotalQuantity"] = decimal.Parse(dr["TotalNeed"].ToString());							//총소요량	

					//현재고 감안 여부
					if(hidPresentRowMaterialUsed.Value == "false")
						row["PureQuantity"]  = decimal.Parse(dr["TotalNeed"].ToString());
					else
						row["PureQuantity"] = decimal.Parse(dr["PureNeed"].ToString());								//순소요량

					row["RowMaterialReadyQuantity"] = decimal.Parse(dr["RawRequestReady"].ToString());				//자재의뢰대기량
					row["OrderRequestReadyQuantityReflection"] = decimal.Parse(dr["PurchasingRequestReady"].ToString());	//구매의뢰대기랑
					row["OrderNonInStorehouseReflection"] = decimal.Parse(dr["PurchasingOrderRemainder"].ToString());	//구매발주미입고량
				
					str = "Select * From II_MT Where RecodingState = 1 and ItemNum = @Item";
					comm.CommandText = str;
					comm.CommandType = CommandType.Text;
					comm.Parameters.Add("@Item",dr["ItemNum"].ToString());
					SqlDataReader reader = comm.ExecuteReader();
					while(reader.Read())
					{
						row["SafyRowMaterialQuantityReflection"] = decimal.Parse(reader["SafetyStockQuantity"].ToString());
					}
					reader.Close();
					comm.Parameters.Clear();

				
					//순소요량 + 안전재고량
					Safety = decimal.Parse(row["PureQuantity"].ToString()) + decimal.Parse(row["SafyRowMaterialQuantityReflection"].ToString());

					decimal OrderIntervalQuantity = OrderGap(dr["ItemNum"].ToString());
					decimal MinOrderQuantity = MinimumGap(dr["ItemNum"].ToString());
					//발주간격수량
					if(hidOrderGapInStorehouseUsed.Value == "false")
						row["OrderGapQuantityReflection"] = 0;
					else
						row["OrderGapQuantityReflection"] = OrderIntervalQuantity;
					//최소발주량
					if(hidMinimumGapUsed.Value == "false")
						row["MinimumOrderQuantityReflection"] = 0;
					else
						row["MinimumOrderQuantityReflection"] = MinOrderQuantity;


					if(hidOrderGapInStorehouseUsed.Value == "true" && hidMinimumGapUsed.Value == "true")
					{
						//발주의뢰량
						if(Safety <= 0)
							row["DeliveryOrderRequestQuantity"] = 0;
						else
						{
							//최소발주량보다 작으면 최소발주량이 발주의뢰량
							if(Safety <= MinOrderQuantity)
								row["DeliveryOrderRequestQuantity"] = row["MinimumOrderQuantityReflection"];
							else
							{
								//최소발주량보다 크면 발주간격수량 비교(발주의뢰량은 최소발주량+발주간격수량)
								decimal Result = OrderIntervalQuantity+MinOrderQuantity;
								if(OrderIntervalQuantity == 0)
								{
									row["DeliveryOrderRequestQuantity"] = Safety;
								}
								else
								{
									while(1>0)
									{
										if(Result >= Safety)
											break;
										else
											Result += OrderIntervalQuantity;
									}
									row["DeliveryOrderRequestQuantity"] = Result;
								}
							}
						}
					}
					else if(hidOrderGapInStorehouseUsed.Value == "true" && hidMinimumGapUsed.Value == "false")
					{
						//발주의뢰량
						if(Safety <= 0)
							row["DeliveryOrderRequestQuantity"] = 0;
						else
						{
							//최소발주량보다 작으면 최소발주량이 발주의뢰량
							if(Safety <=  MinOrderQuantity)
								row["DeliveryOrderRequestQuantity"] = row["MinimumOrderQuantityReflection"];
							else
							{
								//최소발주량보다 크면 발주간격수량 비교(발주의뢰량은 최소발주량+발주간격수량)
								decimal Result = OrderIntervalQuantity+MinOrderQuantity;
								if(OrderIntervalQuantity == 0)
								{
									row["DeliveryOrderRequestQuantity"] = Safety;
								}
								else
								{
									while(1>0)
									{
										if(Result >= Safety)
											break;
										else
											Result += OrderIntervalQuantity;
									}
									row["DeliveryOrderRequestQuantity"] = Result;
								}
							}
						}
					}
					else if(hidOrderGapInStorehouseUsed.Value == "false" && hidMinimumGapUsed.Value == "true")
					{
						//발주의뢰량
						if(Safety <= 0)
							row["DeliveryOrderRequestQuantity"] = 0;
						else
						{
							//최소발주량보다 작으면 최소발주량이 발주의뢰량
							if(Safety <= MinOrderQuantity)
								row["DeliveryOrderRequestQuantity"] = MinOrderQuantity;
							else
								row["DeliveryOrderRequestQuantity"] = Safety;
						}
					}
					else
					{
						//발주의뢰량
						if(Safety <= 0)
							row["DeliveryOrderRequestQuantity"] = 0;
						else
							row["DeliveryOrderRequestQuantity"] = Safety;
					}



					dsRowMaterial1.Tables["RowMaterial"].Rows.Add(row);
				}
			}
			conn.Close();
		}


		private bool IOCheck(string ItemNum)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			bool IOChackable = false;
			string str = @"Select IOChackable From II_MT where RecodingState = 1 and ItemNum = @ItemNum";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				IOChackable = bool.Parse(dr["IOChackable"].ToString());
			}
			conn.Close();

			return IOChackable;
		}


		private decimal OrderGap(string ItemNum)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			decimal OrderGapQuantity = 0;
			string str = @"Select OrderIntervalQuantity From II_MT where RecodingState = 1 and ItemNum = @ItemNum";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				OrderGapQuantity = decimal.Parse(dr["OrderIntervalQuantity"].ToString());
			}
			conn.Close();

			return OrderGapQuantity;
		}

		private decimal MinimumGap(string ItemNum)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			decimal MinOrderQuantity = 0;
			string str = @"Select MinOrderQuantity From II_MT where RecodingState = 1 and ItemNum = @ItemNum";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				MinOrderQuantity = decimal.Parse(dr["MinOrderQuantity"].ToString());
			}
			conn.Close();

			return MinOrderQuantity;
		}


		/// <summary>
		/// 순소요량 산출 함수에서 호출되는 재귀함수
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <param name="Quantity"></param>
		private void ItemNeedProcessing(string ItemNum,decimal Quantity)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			string str ="";
						
			// 넘어온 품목으로 재고량을 조사한다
			decimal ResultQuantity = 0;
			// 넘어온 품목이 원자재면 원자재 창고에서 처리
			if(Division(ItemNum))
			{
				SqlCommand comm = new SqlCommand(str,conn);
				str = @"Update TBRawRedundantResult Set Quantity = Quantity - @Quantity where ItemNum = @ItemNum";
				comm.Parameters.Add("@Quantity",Quantity);
				comm.Parameters.Add("@ItemNum",ItemNum);
				comm.CommandText = str;
				conn.Open();
				comm.ExecuteNonQuery();
				conn.Close();
				comm.Parameters.Clear();

				pop_Function();
			}
			else
			{//넘어온 품목이 원자재가 아니면 생산창고에서 수량처리
				str ="Select Quantity From TBWorkRedundantResult where ItemNum = @ItemNum";
				SqlCommand comm = new SqlCommand(str,conn);
				comm.Parameters.Add("@ItemNum",ItemNum);
				conn.Open();
				
				ResultQuantity = decimal.Parse(comm.ExecuteScalar().ToString());
				comm.Parameters.Clear();
				conn.Close();
				//				int a = 0;
				//				SqlDataReader dr1 = comm.ExecuteReader();
				//				while(dr1.Read())
				//				{
				//					ResultQuantity = decimal.Parse(dr1["Quantity"].ToString());
				//					a=1;
				//				}
				//                conn.Close();
				//				comm.Parameters.Clear();
				//
				//				if(a==0)
				//				{
				//					RegisterClientScriptBlock("","<script>alert('창고에 "+ItemNum+"의 레코드가 존재하지 않습니다!');</script>");
				//					RegisterClientScriptBlock("","<script>location.href = RowMaterialRequirementCalculate.aspx;</script>");
				//				}

				if(Quantity - ResultQuantity > 0)
				{
					conn.Open();
					//해당 요구량이 생산창고 잉여테이블 그룹바이 한 품목의 수량보다 많은경우 
					//생산잉여테이블 의품목 수량은 0으로 처리한다음 그 차이의 수량만큼 호출한다
					str  = @"Update TBWorkRedundantResult Set Quantity = 0 where ItemNum = @ItemNum";
					comm.CommandText = str;
					comm.Parameters.Add("@ItemNum",ItemNum);
					comm.ExecuteNonQuery();


					
					str = @"Select ChildItemNum , NeedQuantityNumerator/NeedQuantityDenominator as Need From IOI_MT Where ParentItemNum = @ItemNum and RecodingState = 1";
					comm.CommandText = str;
					SqlDataReader dr = comm.ExecuteReader();
					
					while(dr.Read())
					{
						WC wc = new WC();
						wc.ItemNum = dr["ChildItemNum"].ToString();
						wc.Quantity = decimal.Parse(dr["Need"].ToString()) * (Quantity - ResultQuantity);						
						
						st.Push(wc);
						
					}
					dr.Close();
					conn.Close();
					comm.Parameters.Clear();

					pop_Function();
				}
				else if(Quantity - ResultQuantity == 0)
				{
					str = @"Update TBWorkRedundantResult Set Quantity = 0 where ItemNum = @ItemNum";
					comm.CommandText = str;
					comm.Parameters.Add("@ItemNum",ItemNum);
					conn.Open();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
					conn.Close();

					pop_Function();
				}
				else
				{
					str = @"Update TBWorkRedundantResult Set Quantity = @Quantity where ItemNum = @ItemNum";
					comm.CommandText = str;
					comm.Parameters.Add("@ItemNum",ItemNum);
					comm.Parameters.Add("@Quantity",(ResultQuantity - Quantity));
					conn.Open();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
					conn.Close();

					pop_Function();
				}
			}
			
		}


		private void pop_Function()
		{
			if(st.Count == 0)
				return;
			else
			{
				WC wc1 = new WC();
				wc1 = (WC)st.Pop();
				ItemNeedProcessing(wc1.ItemNum, wc1.Quantity);
			}
		}
		
	

		/// <summary>
		/// 생산계획 대기량 처리시 호출하는 재귀함수
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <param name="Quantity"></param>
		private void ItemNeedProcessingPlan(string ItemNum,decimal Quantity)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			string str ="";
						
			// 넘어온 품목으로 재고량을 조사한다
			decimal ResultQuantity = 0;
			// 넘어온 품목이 원자재면 원자재 창고에서 처리
			if(Division(ItemNum))
			{
				SqlCommand comm = new SqlCommand(str,conn);
				str = @"Update TBRawRedundantItemPlanResult Set Quantity = Quantity - @Quantity where ItemNum = @ItemNum";
				comm.Parameters.Add("@Quantity",Quantity);
				comm.Parameters.Add("@ItemNum",ItemNum);
				comm.CommandText = str;
				conn.Open();
				comm.ExecuteNonQuery();
				conn.Close();
				comm.Parameters.Clear();

				pop_FunctionPlan();
			}
			else
			{//넘어온 품목이 원자재가 아니면 생산창고에서 수량처리
				str ="Select Quantity From TBWorkRedundantItemPlanResult where ItemNum = @ItemNum";
				SqlCommand comm = new SqlCommand(str,conn);
				comm.Parameters.Add("@ItemNum",ItemNum);
				conn.Open();
				ResultQuantity = decimal.Parse(comm.ExecuteScalar().ToString());
				conn.Close();
				comm.Parameters.Clear();

				if(Quantity - ResultQuantity > 0)
				{
					conn.Open();
					//해당 요구량이 생산창고 잉여테이블 그룹바이 한 품목의 수량보다 많은경우 
					//생산잉여테이블 의품목 수량은 0으로 처리한다음 그 차이의 수량만큼 호출한다
					str  = @"Update TBWorkRedundantItemPlanResult Set Quantity = 0 where ItemNum = @ItemNum";
					comm.CommandText = str;
					comm.Parameters.Add("@ItemNum",ItemNum);
					comm.ExecuteNonQuery();


					
					str = @"Select ChildItemNum , NeedQuantityNumerator/NeedQuantityDenominator as Need From IOI_MT Where ParentItemNum = @ItemNum and RecodingState = 1";
					comm.CommandText = str;
					//comm.Parameters.Add("@ItemNum",ItemNum);
					SqlDataReader dr = comm.ExecuteReader();
					
					while(dr.Read())
					{
						WC wc = new WC();
						wc.ItemNum = dr["ChildItemNum"].ToString();
						wc.Quantity = decimal.Parse(dr["Need"].ToString()) * (Quantity - ResultQuantity);						
						
						st.Push(wc);
						
					}
					dr.Close();
					conn.Close();
					comm.Parameters.Clear();

					pop_FunctionPlan();
				}
				else if(Quantity - ResultQuantity == 0)
				{
					str = @"Update TBWorkRedundantItemPlanResult Set Quantity = 0 where ItemNum = @ItemNum";
					comm.CommandText = str;
					comm.Parameters.Add("@ItemNum",ItemNum);
					conn.Open();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
					conn.Close();

					pop_FunctionPlan();
				}
				else
				{
					str = @"Update TBWorkRedundantItemPlanResult Set Quantity = @Quantity where ItemNum = @ItemNum";
					comm.CommandText = str;
					comm.Parameters.Add("@ItemNum",ItemNum);
					comm.Parameters.Add("@Quantity",(ResultQuantity - Quantity));
					conn.Open();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
					conn.Close();

					pop_FunctionPlan();
				}
			}
			
		}


		private void pop_FunctionPlan()
		{
			if(st.Count == 0)
				return;
			else
			{
				WC wc1 = new WC();
				wc1 = (WC)st.Pop();
				ItemNeedProcessingPlan(wc1.ItemNum, wc1.Quantity);
			}
		}



		/// <summary>
		/// 재고부족분 처리 함수에서 호출하는 재귀함수
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <param name="Quantity"></param>
		private void ItemNeedProcessingLast(string ItemNum,decimal Quantity)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			string str ="";
						
			// 넘어온 품목으로 재고량을 조사한다
			decimal ResultQuantity = 0;
			// 넘어온 품목이 원자재면 원자재 창고에서 처리
			if(Division(ItemNum))
			{
				SqlCommand comm = new SqlCommand(str,conn);
				str = @"Update TBRawRedundantItemLastResult Set Quantity = Quantity - @Quantity where ItemNum = @ItemNum";
				comm.Parameters.Add("@Quantity",Quantity);
				comm.Parameters.Add("@ItemNum",ItemNum);
				comm.CommandText = str;
				conn.Open();
				comm.ExecuteNonQuery();
				conn.Close();
				comm.Parameters.Clear();

				pop_FunctionLast();
			}
			else
			{//넘어온 품목이 원자재가 아니면 생산창고에서 수량처리
				str ="Select Quantity From TBWorkRedundantItemLastResult where ItemNum = @ItemNum";
				SqlCommand comm = new SqlCommand(str,conn);
				comm.Parameters.Add("@ItemNum",ItemNum);
				conn.Open();
				ResultQuantity = decimal.Parse(comm.ExecuteScalar().ToString());
				conn.Close();
				comm.Parameters.Clear();

				if(Quantity - ResultQuantity > 0)
				{
					conn.Open();
					//해당 요구량이 생산창고 잉여테이블 그룹바이 한 품목의 수량보다 많은경우 
					//생산잉여테이블 의품목 수량은 0으로 처리한다음 그 차이의 수량만큼 호출한다
					str  = @"Update TBWorkRedundantItemLastResult Set Quantity = 0 where ItemNum = @ItemNum";
					comm.CommandText = str;
					comm.Parameters.Add("@ItemNum",ItemNum);
					comm.ExecuteNonQuery();


					
					str = @"Select ChildItemNum , NeedQuantityNumerator/NeedQuantityDenominator as Need From IOI_MT Where ParentItemNum = @ItemNum and RecodingState = 1";
					comm.CommandText = str;
					//comm.Parameters.Add("@ItemNum",ItemNum);
					SqlDataReader dr = comm.ExecuteReader();
					
					while(dr.Read())
					{
						WC wc = new WC();
						wc.ItemNum = dr["ChildItemNum"].ToString();
						wc.Quantity = decimal.Parse(dr["Need"].ToString()) * (Quantity - ResultQuantity);						
						
						st.Push(wc);
						
					}
					dr.Close();
					conn.Close();
					comm.Parameters.Clear();

					pop_FunctionLast();
				}
				else if(Quantity - ResultQuantity == 0)
				{
					str = @"Update TBWorkRedundantItemLastResult Set Quantity = 0 where ItemNum = @ItemNum";
					comm.CommandText = str;
					comm.Parameters.Add("@ItemNum",ItemNum);
					conn.Open();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
					conn.Close();

					pop_FunctionLast();
				}
				else
				{
					str = @"Update TBWorkRedundantItemLastResult Set Quantity = @Quantity where ItemNum = @ItemNum";
					comm.CommandText = str;
					comm.Parameters.Add("@ItemNum",ItemNum);
					comm.Parameters.Add("@Quantity",(ResultQuantity - Quantity));
					conn.Open();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
					conn.Close();

					pop_FunctionLast();
				}
			}
			
		}


		private void pop_FunctionLast()
		{
			if(st.Count == 0)
				return;
			else
			{
				WC wc1 = new WC();
				wc1 = (WC)st.Pop();
				ItemNeedProcessingLast(wc1.ItemNum, wc1.Quantity);
			}
		}
		

		

		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			KIT_ERP.Search search = new KIT_ERP.Search("RowMaterialRequirementCalculate", ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum, ItemSearchControl1.ItemName, wdcMinDate, wdcMaxDate);

			uwgPP_HT.DataSource = search.DataSet_search();;
			uwgPP_HT.DataBind();

			if(uwgPP_HT.Rows.Count > 0)
				btnProduce.Enabled = true;
			else
				btnProduce.Enabled = false;
		}

		/// <summary>
		/// 해당 품목의 자산분류가 원자재인지 아닌지 판단하는 함수
		/// </summary>
		/// <param name="Item">품목번호</param>
		/// <returns>해당 품목의 자산분류 값을 리턴</returns>
		private bool Division(string Item)
		{
			string aa="";

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select PropertyClassification From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = Item.ToString();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = dr["PropertyClassification"].ToString();
			}
			conn.Close();

			if(aa.ToString() == "원자재")
				return true;
			else 
				return false;
		}
		
		/// <summary>
		/// 자산분류가 제품인경우
		/// </summary>
		/// <param name="Item"></param>
		/// <returns></returns>
		private bool Classification(string Item)
		{
			string aa="";

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select PropertyClassification From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = Item.ToString();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = dr["PropertyClassification"].ToString();
			}
			conn.Close();

			if(aa.ToString() == "제품")
				return true;
			else 
				return false;
		}
		private bool Division(string Item, string Code)
		{
			string aa="";

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select WorkDistinction From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = Item;
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value = Code;
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = dr["WorkDistinction"].ToString();
			}
			conn.Close();

			if(aa.ToString().Trim() == "외주")
				return true;
			else 
				return false;
		}


		/// <summary>
		/// 출고품목 찾기 함수(SubItemSearch())에서 호출되는 
		/// 해당 품목의 최하위 공정순서을 찾아오는 함수로 
		/// 최하위 공정순서을 리턴해준다.
		/// </summary>
		/// <param name="ItemNum">품목번호</param>
		/// <returns>최하위 공정순서를 리턴해준다</returns>
		private int MinProcessSequenceNum(string ItemNum)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "Select Min(ProcessSequenceNum) From PSI_MT Where ItemNum = @ItemNum and RecodingState = 1";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@ItemNum",ItemNum);
			conn.Open();
			int Process = int.Parse(comm.ExecuteScalar().ToString());
			conn.Close();
			comm.Parameters.Clear();

			return Process;
		}


		/// <summary>
		/// 출고품목 찾기 함수(SubItemSearch())에서 호출되는 자가구분여부를
		/// 체크하는 함수
		/// </summary>
		/// <param name="Item">품목명</param>
		/// <param name="Code">공정순서</param>
		/// <returns>자가인경우 true, 그외의 경우 false를 리턴한다.</returns>
		private bool Division(string Item, int Code)
		{
			string aa="";
			
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select WorkDistinction From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessSequenceNum = @code";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = Item;
			comm.Parameters.Add("@code",SqlDbType.TinyInt).Value = Code;
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = dr["WorkDistinction"].ToString();
			}
			conn.Close();

			if(aa.ToString().Trim() == "자가")
				return true;
			else 
				return false;
		}

		private string Unit(string Item)
		{
			string aa="";
			
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select SmallClassificationName From PUC_MT inner join II_MT on SmallClassificationCode = Unit Where PUC_MT.RecodingState = 1 and II_MT.RecodingState = 1 and ItemNum = @num";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = Item;

			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = dr["SmallClassificationName"].ToString();
			}
			conn.Close();

			return aa;
		}


		private string Standard(string Item)
		{
			string aa="";
			
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select Standard From II_MT Where II_MT.RecodingState = 1 and ItemNum = @num";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = Item;

			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = dr["Standard"].ToString();
			}
			conn.Close();

			return aa;
		}
	}
}
