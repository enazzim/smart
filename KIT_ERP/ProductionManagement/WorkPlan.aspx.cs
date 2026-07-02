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

namespace KIT_ERP.ProductionManagement
{
	/// <summary>
	/// WorkPlan에 대한 요약 설명입니다.
	/// </summary>
	public class WorkPlan : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label3;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_FromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_ToDate;
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected System.Web.UI.WebControls.Button bt_Register;
		protected System.Web.UI.HtmlControls.HtmlInputHidden chkAll;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Label searchTitle;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected KIT_ERP.ProductionManagement.dsWorkTable ds_WDWP; //WC작업계획원장
		protected KIT_ERP.ProductionManagement.dsWorkTable ds_Available = new KIT_ERP.ProductionManagement.dsWorkTable();	//AvailableTimeTable
		protected KIT_ERP.ProductionManagement.dsWorkTable ds_Standby = new KIT_ERP.ProductionManagement.dsWorkTable();		//대기테이블
		protected KIT_ERP.ProductionManagement.dsWorkTable ds_Progress = new KIT_ERP.ProductionManagement.dsWorkTable();	//진행테이블
		protected KIT_ERP.ProductionManagement.dsWorkTable ds_Work = new KIT_ERP.ProductionManagement.dsWorkTable();		//작업테이블
		protected KIT_ERP.ProductionManagement.dsWorkTable ds_View = new KIT_ERP.ProductionManagement.dsWorkTable();		//달력으로 보여줄 테이블(AT와 형태동일
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPresentRowMaterialUsed;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOutOrderUsed;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidWorkPlan2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidWorkPlan3;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidWorkPlan1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidBundlingUsed;	
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidInWorkConsidering;
		protected ArrayList List = new ArrayList();
		protected ArrayList alist = new ArrayList();//작업시작일과 납기요구일, 생산계획번호를 담은 ArrayList
		protected ArrayList arrlist = new ArrayList();//품목명,도면번호,공정명,리드타임등을 넣기위한 ArrayList
		protected int Vol=0;									//작업계획수립시 입력할 Volum번호
		SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMinusStockAddUsed;		
		//SqlTransaction tr;

		int ProcessCodeExist = 0;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification2;
		
		DataSet ds = new DataSet();

		struct WCSub
		{
			public string ItemNum;
			public string ProcessCode;
			public decimal Quantity;	//작업수량
		}

		struct WCSubItem
		{
			public string ItemNum;
			public string ProcessCode;
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
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
				//	*************************************************
				//	**  품목분류1 드롭다운리스트... 데이타바인딩... **		
				//	*************************************************
				//코드분류표에서 대분류명이 품목분류1인 소분류명을 가지고 옴.
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


				//	*************************************************
				//	**  품목분류1 드롭다운리스트... 데이타바인딩... **		
				//	*************************************************
				//코드분류표에서 대분류명이 품목분류1인 소분류명을 가지고 옴.
				string str_ItemClassification2 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0220'" ;
				SqlCommand comm_ItemClassification2 = new SqlCommand(str_ItemClassification2,conn);
				SqlDataAdapter da_ItemClassification2 = new SqlDataAdapter(comm_ItemClassification2) ;
				DataSet ds_ItemClassification2 = new DataSet() ;
				da_ItemClassification2.Fill(ds_ItemClassification2);
				
				ddlItemClassification2.DataSource = ds_ItemClassification2;
				ddlItemClassification2.DataTextField = ds_ItemClassification2.Tables[0].Columns[0].ToString();
				ddlItemClassification2.DataValueField = ds_ItemClassification2.Tables[0].Columns[1].ToString();
				ddlItemClassification2.DataBind();
				ddlItemClassification2.Items.Insert(0, "-선택하세요-") ;
				ddlItemClassification2.Items[0].Value = "";




				//bt_Register.Attributes.Add("onClick", "return confirm('WC작업계획을 수립 하시겠습니까?');");
				// 시스템정보의 셋팅값을 가져옴.
				SqlConnection conn1 = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
				conn1.Open();

				string strSQL = @"SELECT PresentRowMaterialUsed, SafeyRowMaterialUsed, OrderNonInStorehouseUsed, 
								  OrderGapInStorehouseUsed, MinimumGapUsed,OrderRequestStandbyUsed,OutOrderUsed,
								  BundlingUsed,WorkPlan1, WorkPlan2, WorkPlan3,InWorkConsidering,MinusStockAddUsed FROM SCS_T";

				SqlCommand comm = new SqlCommand(strSQL, conn1);

				SqlDataReader reader = comm.ExecuteReader();

				while(reader.Read())
				{
					// 현재고량
					if(bool.Parse(reader["PresentRowMaterialUsed"].ToString()))
						hidPresentRowMaterialUsed.Value = "true";
					else
						hidPresentRowMaterialUsed.Value = "false";
					//외주재고를 자가재고로 감안여부
					if(bool.Parse(reader["OutOrderUsed"].ToString()))
						hidOutOrderUsed.Value = "true";
					else
						hidOutOrderUsed.Value = "false";
					//작업계획전략I
					if(bool.Parse(reader["WorkPlan1"].ToString()))
						hidWorkPlan1.Value = "true";
					else
						hidWorkPlan1.Value = "false";
					//작업계획전략II
					if(bool.Parse(reader["WorkPlan2"].ToString()))
						hidWorkPlan2.Value = "true";
					else
						hidWorkPlan2.Value = "false";
					//작업계획전략I
					if(bool.Parse(reader["WorkPlan3"].ToString()))
						hidWorkPlan3.Value = "true";
					else
						hidWorkPlan3.Value = "false";
					//동일공정묶음여부
					if(bool.Parse(reader["BundlingUsed"].ToString()))
						hidBundlingUsed.Value = "true";
					else
						hidBundlingUsed.Value = "false";
					//외주최대수량에 맞춰 자가수량변경여부
					if(bool.Parse(reader["InWorkConsidering"].ToString()))
						hidInWorkConsidering.Value = "true";
					else
						hidInWorkConsidering.Value = "false";
					//마이너스작업수량을 추가로 작업계획을 수립할지 여부
					if(bool.Parse(reader["MinusStockAddUsed"].ToString()))
						hidMinusStockAddUsed.Value = "true";
					else
						hidMinusStockAddUsed.Value = "false";

				}
				reader.Close();
				conn1.Close();
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
			this.bt_Clear.Click += new System.EventHandler(this.bt_Clear_Click);
			this.bt_Search.Click += new System.EventHandler(this.bt_Search_Click);
			this.bt_Register.Click += new System.EventHandler(this.bt_Register_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion



		/// <summary>
		/// 검색 함수
		/// </summary>
		/// <returns></returns>
		private DataSet PP_Search()
		{
			Search search = new Search("WorkPlan",ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum, ItemSearchControl1.ItemName, ddlItemClassification1.SelectedItem.Value.ToString(), wdc_FromDate,wdc_ToDate,ddlItemClassification2.SelectedItem.Value.ToString());
			return search.DataSet_search();
		}

		/// <summary>
		/// 초기화버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Clear_Click(object sender, System.EventArgs e)
		{
			ItemSearchControl1.ClearTextBox();
			wdc_FromDate.Value ="";
			wdc_ToDate.Value = "";
			ddlItemClassification1.SelectedIndex = 0;
			ddlItemClassification2.SelectedIndex = 0;
		}

		/// <summary>
		/// 검색버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Search_Click(object sender, System.EventArgs e)
		{
			
			UltraWebGrid1.DataSource = PP_Search();
			UltraWebGrid1.DataBind();
		}

		/// <summary>
		/// 수립버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Register_Click(object sender, System.EventArgs e)
		{
			ds = PP_Search();
			conn.Open();
			//tr = conn.BeginTransaction();

			string str = "Select isnull(Max(VolumNum),0) From WDWP_HT";
			SqlCommand comm = new SqlCommand(str,conn);
			Vol = int.Parse(comm.ExecuteScalar().ToString())+1;

			try
			{

				for(int i = 0; i <UltraWebGrid1.Rows.Count;i++)
				{
					// 체크되지 않으면 Value값이 null이다 그러므로 확인후 false값을 넣어준다.
					if(UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value == null)
					{
						UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value = false;
					}
					if(!bool.Parse(UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value.ToString()))
					{
						DataRow[] row = ds.Tables[0].Select("ProductionPlanHistoryIndex = '" + UltraWebGrid1.Rows[i].Cells.FromKey("ProductionPlanHistoryIndex").Value + "'");
						ds.Tables[0].Rows.Remove(row[0]);
					}
				}

				//작업계획원장을 만들어 등록시킨다.
				if(UltraWebGrid1.Rows.Count == 0 || ds.Tables[0].Rows.Count == 0 )
				{
					RegisterStartupScript("","<script>alert('항목을 선택해주세요!');</script>");	
				}
				else
				{
					
					WCPlan();
					PPUpdate();
                    
					
					RegisterStartupScript("","<script>alert('수립되었습니다!');</script>");			
				}

				
			}
			catch(Exception ee)
			{
				Response.Write("<script>alert('"+ee.Message+"')</script>");
				//tr.Rollback();
			}
			finally
			{
				conn.Close();

				UltraWebGrid1.DataSource = PP_Search();
				UltraWebGrid1.DataBind();
			}
		}

		/// <summary>
		/// 생산계획원장 진행상태 업데이트 함수
		/// </summary>
		private void PPUpdate()
		{
			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				string str = "Update PP_HT Set ProgressCondition = '진행' where ProductionPlanHistoryIndex = @Index";
				SqlCommand comm = new SqlCommand(str,conn);
				//comm.Transaction = tr;
				comm.Parameters.Add("@Index",int.Parse(dr["ProductionPlanHistoryIndex"].ToString()));
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
		}

		/// <summary>
		/// 작업계획원장 생성 함수
		/// </summary>
		private void WCPlan()
		{
			string str = "delete From TBPreWorkPlan";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.ExecuteNonQuery();
			
			if(hidWorkPlan1.Value == "true" || hidWorkPlan3.Value == "true")
				goto aaa;
			
			//잉여 재고 테이블 생성(생산,외주,원자재) ①
			RedundantStockTableMaking();

			//작업계획 잔량 처리②
			WorkRemainderProcessing();

			//외주잔량 처리 ③
			OutOrderRemainderProcessing();

			//자가외주 재고 통합 ④
			InOutStockSumming();

			//완성제품 제거하기 ⑤
			LastProgressStockRemoving();

			//마이너스 재고 보상하기 ⑥
			//위 함수들 수행후 나타나는 재고의 마이너스 값을 모두 0으로 세팅해준다
			//if(hidMinusStockAddUsed.Value == "true")
			MinusStockCompensating();

			aaa:
				for(int count =0; count < ds.Tables[0].Rows.Count; count++)
				{
					// 제품을 공정별로 푼 BOMTable생성 ⑦
					WorkPlaning(ds.Tables[0].Rows[count]["ItemNum"].ToString(),decimal.Parse(ds.Tables[0].Rows[count]["ProductionPlanQuantity"].ToString()));
					
					// 작업계획 수립전략1을 선택한경우는 그냥 넘어간다
					if(hidWorkPlan1.Value == "true" )
						goto bbb;
					else if(hidWorkPlan3.Value == "true")
					{
							goto bbb;
					}
					// ⑧ 잉여창고 수량및 작업량 조절함수
					RedundentStockConsidering();
				
				bbb:
					//외주레코드 생성함수 ⑨
					OutOrderRecordGenerating(ds.Tables[0].Rows[count]["ItemNum"].ToString(),decimal.Parse(ds.Tables[0].Rows[count]["ProductionPlanQuantity"].ToString()));
			
					if(hidWorkPlan1.Value == "true" || hidWorkPlan3.Value == "true") goto ccc;

					// ⑩ 외주수량 조절 함수
					OutOrderQuantityAdjusting();

				ccc: 
					
					alist.Add(ds.Tables[0].Rows[count]["ProductionBeginDate"].ToString());//생산시작일
					alist.Add(ds.Tables[0].Rows[count]["ProductionPlanHistoryIndex"].ToString());//생산계획번호
					alist.Add(ds.Tables[0].Rows[count]["DeliveryDate"].ToString());//납기요구일
					//Pre작업계획DB에 저장한다 ⑪
					InsertPreWorkPlanDB(ds.Tables[0].Rows[count]["ItemNum"].ToString(),decimal.Parse(ds.Tables[0].Rows[count]["ProductionPlanQuantity"].ToString()), alist);
					alist.Clear();
				}
			if(hidBundlingUsed.Value == "true")// 공정과 납기일이 동일한 레코드 합산
				BundlingforDeliveryDate();// ⑫
			//else
			InsertWorkPlanDB();//Pre작업계획테이블에 레코드를 실제 WDWP_HT원장에 입력한다. ⑬
		}


		/// <summary>
		/// ① 생산창고 외주창고 원자재 창고 잉여테이블 생성함수
		/// TBOutOrderRedundant, TBRawRedundant, TBWorkRedundant 테이블에 각 창고 수량 입력
		/// </summary>
		private void RedundantStockTableMaking()
		{
			string str = "SPRedundantStockTableMaking";
			SqlCommand comm = new SqlCommand(str,conn);
			//comm.Transaction = tr;
			comm.CommandType = CommandType.StoredProcedure;
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// ②작업계획 원장 잔량 처리 함수
		/// </summary>
		private void WorkRemainderProcessing()
		{
			//자가공정의 대기,지시,진행인 잔량 처리 함수②-1
			InWorkRemainderProcessing();
			//외주공정 대기 잔량 처리 함수②-2
			OutWorkRemainderProcessing();
		}

		/// <summary>
		/// ②-1 작업계획원장의 자가 공정의 진행상태가 대기, 지시, 진행인 잔량을 처리하는 함수
		/// </summary>
		private void InWorkRemainderProcessing()
		{

			//작업계획원장의 진행상태가 대기,지시, 진행인 공정품들을 모두 Select 해온다.(작업구분이 외주이던 자가이던 상관 없음)
			// 외주인경우라도 진행상태가 대기인 것들은 아직 외주의뢰가 나가지 않은 공정품들이므로 생산잉여창고 테이블에서 수량을 중가 혹은 감소시킨다.
			string str = "SPInWorkRemainderQuantity";
			SqlCommand comm = new SqlCommand(str,conn);
			//comm.Transaction = tr;
			comm.CommandType = CommandType.StoredProcedure;
			DataSet dataset = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(comm);
			da.Fill(dataset);

			int a = 0;

			//해당 공정품들에 대해 각 공정품을 더해주고 하위공정품을 마이너스 해준다.
			foreach(DataRow dr in dataset.Tables[0].Rows)
			{

				if(a > 400)
					a = a+1;
				else
					a = a+1;

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
		}

		/// <summary>
		/// ②-2 작업계획원장의 작업구분이 외주인 공정품의 대기량을 처리하는 함수
		/// </summary>
		private void OutWorkRemainderProcessing()
		{
			//작업계획원장의 진행상태가 대기,지시, 진행인 공정품들을 모두 Select 해온다.(작업구분이 외주이던 자가이던 상관 없음)
			// 외주인경우라도 진행상태가 대기인 것들은 아직 외주의뢰가 나가지 않은 공정품들이므로 생산잉여창고 테이블에서 수량을 중가 혹은 감소시킨다.
			string str = "SPOutWorkRemainderQuantity";
			SqlCommand comm = new SqlCommand(str,conn);
			//comm.Transaction = tr;
			comm.CommandType = CommandType.StoredProcedure;
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
		}
		


		/// <summary>
		/// ③외주 잔량 처리함수
		/// 외주 의뢰원장의 시작
		/// </summary>
		private void OutOrderRemainderProcessing()
		{
			string str = "SPOutOrderRemainderProcessing";
			SqlCommand comm = new SqlCommand(str,conn);
			//comm.Transaction = tr;
			comm.CommandType = CommandType.StoredProcedure;
			DataSet dataset = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(comm);
			da.Fill(dataset);
			foreach(DataRow dr in dataset.Tables[0].Rows)
			{
				// ③-1 해당 종료공정의 작업구분이 외주인 경우는 무조건 생산잉여창고에 수량을 증가시킨다
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
				
					// ③-2 하위 출고함수저장하는 ArrayList
					ArrayList arr = SubItemSearch(dr["ItemNum"].ToString(),dr["BeginProcessCode"].ToString(),decimal.Parse(dr["OrderQuantity"].ToString()));

					// 외주잉여창고에 출고품목을 마이너스한다
					for(int count = 0; count < arr.Count; count++)
					{
						WCSub wcsub = new WCSub();
						wcsub = (WCSub)arr[count];
				
						str = "Update TBOutOrderRedundant Set Quantity = Quantity - @Quantity Where ItemNum = @ItemNum and ProcessCode = @ProcessCode";
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
							RegisterClientScriptBlock("","<script>alert('"+dr["ItemNum"].ToString() + " 품목의 하위품목들에 대한 외주창고가 생성되어 있지 않습니다!');</script>");
							RegisterClientScriptBlock("","<script>location.href = WorkPlan.aspx;</script>");
						}
					}
			
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
				
						// ③-2 
						ArrayList arr = SubItemSearch(dr["ItemNum"].ToString(),dr["BeginProcessCode"].ToString(),decimal.Parse(dr["OrderQuantity"].ToString()));

						for(int count = 0; count < arr.Count; count++)
						{
							WCSub wcsub = new WCSub();
							wcsub = (WCSub)arr[count];

							str = "Update TBOutOrderRedundant Set Quantity = Quantity - @Quantity Where ItemNum = @ItemNum and ProcessCode = @ProcessCode";
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
								RegisterClientScriptBlock("","<script>alert('"+dr["ItemNum"].ToString() + " 품목의 하위품목들에 대한 외주창고가 생성되어 있지 않습니다!');</script>");
								RegisterClientScriptBlock("","<script>location.href = WorkPlan.aspx;</script>");
							}
						}						
					}
					else
					{
						// ③-2 외주잉여창고에 해당 품목의 하위품목 수량을 '0'으로 맞춰준다
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
								RegisterClientScriptBlock("","<script>alert('"+dr["ItemNum"].ToString() + " 품목의 하위품목들에 대한 외주창고가 생성되어 있지 않습니다!');</script>");
								RegisterClientScriptBlock("","<script>location.href = WorkPlan.aspx;</script>");
							}
						}
					}
				}
			}
		}


		/// <summary>
		/// ③-2출고 품목 찾기 함수
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <param name="ProcessCode"></param>
		/// <param name="Quantity"></param>
		/// <returns></returns>
		private ArrayList SubItemSearch(string ItemNum, string ProcessCode,decimal Quantity)
		{
			List.Clear();
			//품목을 던져주고
			string str = "SPDeliveryItemBOM";
			SqlCommand comm = new SqlCommand(str,conn);
			//comm.Transaction = tr;
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
							// ③-2
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
								if(ExistenceRecord(dataset.Tables[0].Rows[rowcount]["ItemNum"].ToString(), dataset.Tables[0].Rows[rowcount]["ProcessCode"].ToString()) >=1)
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
				RegisterClientScriptBlock("","<script>alert('품목에 해당공정이 존재하지 않습니다!')</script>");
				RegisterClientScriptBlock("","<script>location.href = WorkPlan.aspx</script>");

				return null;
			}
		}

		/// <summary>
		/// ③-1-1단가테이블에 종료공정이 존재하는 레코드가 있는가 확인하는 함수
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <param name="ProcessCode"></param>
		/// <returns></returns>
		private int ExistenceRecord(string ItemNum, string ProcessCode)
		{

			string str = "Select Count(*) From UCI_MT Where RecodingState = 1 and ItemNum = @ItemNum and EndProcessCode = @ProcessCode";
			SqlCommand comm = new SqlCommand(str,conn);
			//comm.Transaction = tr;
			int result = int.Parse(comm.ExecuteScalar().ToString());
			return result;
		}




		/// <summary>
		/// ④ 자가 외주 재고 통합 함수
		/// </summary>
		private void InOutStockSumming()
		{
			string str = "SPInOutStockSumming";
			SqlCommand comm = new SqlCommand(str,conn);
			//comm.Transaction = tr;
			comm.CommandType = CommandType.StoredProcedure;
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// ⑤ 최종 공정 제품 제거함수
		/// </summary>
		private void LastProgressStockRemoving()
		{
			string str = @"Select * From TBWorkRedundantSum  where TBWorkRedundantSum.ProcessSequenceNum = (Select Max(T2.ProcessSequenceNum) from TBWorkRedundantSum T2 where T2.ItemNum = TBWorkRedundantSum.ItemNum Group by T2.ItemNum)";
			SqlCommand comm = new SqlCommand(str,conn);
			//comm.Transaction = tr;
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
		}


		/// <summary>
		/// ⑥마이너스 재고 보상 함수
		/// </summary>
		private void MinusStockCompensating()
		{
			// ⑥-1
			//생산외주 통합 테이블에 마이너스 재고레코드가 존재하지 않으면 함수를 그냥 빠져나간다. 
			step1:
				if(CountMinusStock() <=0)
					return;
			// 마이너스 재고가 존재하면 먼저 해당 되는 레코드를 선택한다.
			string str = @"Select * From TBWorkRedundantSum Where Quantity < 0 order by ItemNum";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			//comm.Transaction = tr;
			comm.CommandText = str;
			comm.CommandType = CommandType.Text;
			DataSet dataset = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(comm);
			da.Fill(dataset);
					
			foreach(DataRow dr in dataset.Tables[0].Rows)
			{
				// 현재품목과 공정코드,수량을 저장한다
				string ItemNum = dr["ItemNum"].ToString();
				string ProcessCode = dr["ProcessCode"].ToString();
				decimal Quantity = -decimal.Parse(dr["Quantity"].ToString());

				// 마이너스 재고이므로 마이너스를 한번더 붙여 플러스 수량으로 변경시킨다.
				// 그래야 자신의 하위레코드에 대한
				// 해당 레코드 수량을 0으로 맞춘다
				str = "Update TBWorkRedundantSum Set Quantity = 0 Where ItemNum = @ItemNum and ProcessCode = @ProcessCode";
				comm.CommandText = str;
				comm.CommandType = CommandType.Text;
				comm.Parameters.Add("@ItemNum",ItemNum);
				comm.Parameters.Add("@ProcessCode",ProcessCode);
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();

				//해당 공정품 이하의 공정품을 감안해 수량을 감소시킨다.
				
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
						// 현재공정의 다음레코드를 선택
						// 다음레코드의 Itemnum과 ItemNum이 동일하면 다음레코드의 품목과 공정으로 창고의 수량정리
						if(dsItem.Tables[0].Rows[Item+1]["ItemNum"].ToString().Trim() == ItemNum)
						{
								
							str = "SPWorkRemainderProcessingMinus";
							comm.CommandText = str;
							comm.CommandType = CommandType.StoredProcedure;
							comm.Parameters.Add("@ItemNum",dsItem.Tables[0].Rows[Item+1]["ItemNum"].ToString());
							comm.Parameters.Add("@Quantity",decimal.Parse(dsItem.Tables[0].Rows[Item+1]["Quantity"].ToString()));
							comm.Parameters.Add("@ProcessCode",dsItem.Tables[0].Rows[Item+1]["ProcessCode"].ToString());
							comm.ExecuteNonQuery();
							comm.Parameters.Clear();

							// 마이너스 재고에 대한 작업계획 추가여부에 따라 작업계획을 추가시킨다.
							if(hidMinusStockAddUsed.Value == "true")
							{
								if(Division(dsItem.Tables[0].Rows[Item+1]["ItemNum"].ToString()) == false)
								{
									if(Division(dsItem.Tables[0].Rows[Item+1]["ItemNum"].ToString(),int.Parse(dsItem.Tables[0].Rows[Item+1]["ProcessSequenceNum"].ToString())) == true)
									{
										str = @"Insert into TBPreWorkPlan (ItemNum,ProcessSequenceNum, ProcessCode, WorkDistinction,WorkPlanQuantity) 
											Values(@ItemNum,@ProcessSequenceNum, @ProcessCode,'자가', @Quantity)";
									}
									else
									{
										str = @"Insert into TBPreWorkPlan (ItemNum,ProcessSequenceNum, ProcessCode, WorkDistinction,WorkPlanQuantity) 
											Values(@ItemNum,@ProcessSequenceNum, @ProcessCode,'외주', @Quantity)";
									}
									comm.CommandText = str;
									comm.CommandType = CommandType.Text;
									comm.Parameters.Add("@ItemNum",dsItem.Tables[0].Rows[Item+1]["ItemNum"].ToString());
									comm.Parameters.Add("@ProcessSequenceNum",int.Parse(dsItem.Tables[0].Rows[Item+1]["ProcessSequenceNum"].ToString()));
									comm.Parameters.Add("@ProcessCode",dsItem.Tables[0].Rows[Item+1]["ProcessCode"].ToString());
									comm.Parameters.Add("@Quantity", decimal.Parse(dsItem.Tables[0].Rows[Item+1]["Quantity"].ToString()));
									comm.ExecuteNonQuery();
									comm.Parameters.Clear();
								}
							}
								
						}
						else
						{
							//Level2의 모든 레코드들에 대해 첫레코드를 선택해 수량을 마이너스 시킨다
							str = "SPWorkRemainderProcessingNextLvl";
							comm.CommandText = str;
							comm.CommandType = CommandType.StoredProcedure;
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

								// 마이너스 재고에 대한 작업계획 추가여부에 따라 작업계획을 추가시킨다.
								if(hidMinusStockAddUsed.Value == "true")
								{
									if(Division(dsItem.Tables[0].Rows[Item+1]["ItemNum"].ToString()) == false)
									{

										if(Division(dsItem.Tables[0].Rows[Item+1]["ItemNum"].ToString(),int.Parse(dsItem.Tables[0].Rows[Item+1]["ProcessSequenceNum"].ToString())) == true)
										{
											str = @"Insert into TBPreWorkPlan (ItemNum,ProcessSequenceNum, ProcessCode, WorkDistinction,WorkPlanQuantity) 
											Values(@ItemNum,@ProcessSequenceNum, @ProcessCode,'자가', @Quantity)";
										}
										else
										{
											str = @"Insert into TBPreWorkPlan (ItemNum,ProcessSequenceNum, ProcessCode, WorkDistinction,WorkPlanQuantity) 
											Values(@ItemNum,@ProcessSequenceNum, @ProcessCode,'외주', @Quantity)";
										}
										comm.CommandText = str;
										comm.CommandType = CommandType.Text;
										comm.Parameters.Add("@ItemNum",dsItem.Tables[0].Rows[Item+1]["ItemNum"].ToString());
										comm.Parameters.Add("@ProcessSequenceNum",int.Parse(dsItem.Tables[0].Rows[Item+1]["ProcessSequenceNum"].ToString()));
										comm.Parameters.Add("@ProcessCode",dsItem.Tables[0].Rows[Item+1]["ProcessCode"].ToString());
										comm.Parameters.Add("@Quantity", decimal.Parse(dsItem.Tables[0].Rows[Item+1]["Quantity"].ToString()));
										comm.ExecuteNonQuery();
										comm.Parameters.Clear();
									}
								}
							}
						}
						comm.Parameters.Clear();
						break;
					}
				}

			}
			goto step1;
				

		}


		/// <summary>
		/// ⑥-1생산외주 통합 잉여테이블에 마이너스재고 레코드 갯수를 파악하는 함수
		/// </summary>
		/// <returns></returns>
		private int CountMinusStock()
		{
			string str ="Select Count(*) From TBWorkRedundantSum where Quantity < 0";
			SqlCommand comm = new SqlCommand(str,conn);
			//comm.Transaction = tr;
			return int.Parse(comm.ExecuteScalar().ToString());
		}

		/// <summary>
		/// ⑦ 제품에대한 공정품을 BOM으로 푼 테이블 생성
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <param name="Quantity"></param>
		private void WorkPlaning(string ItemNum,decimal Quantity)
		{
			//TBWorkDivision,TBWorkPlanning 테이블 생성
			string str = "SPWorkPlaning";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			//comm.Transaction = tr;
			comm.CommandText = str;
			comm.CommandType = CommandType.StoredProcedure;
			comm.Parameters.Add("@ItemNum",ItemNum);
			comm.Parameters.Add("@Quantity",Quantity);
			comm.ExecuteNonQuery();


			str = "Select * From TBWorkPlanning";
			comm.CommandText = str;
			comm.CommandType = CommandType.Text;
			DataSet dsProcessCodeExist = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(comm);
			da.Fill(dsProcessCodeExist);
			foreach(DataRow dr in dsProcessCodeExist.Tables[0].Rows)
			{
				ProcessCodeExist = ProcessCodeExist + int.Parse(dr["ProcessSequenceNum"].ToString());
			}
			
			if(ProcessCodeExist == 0)
			{
				throw new Exception(ItemNum + " 품목의 공정정보가 존재하지 않습니다!");
			}
			

			
		}

		/// <summary>
		/// ⑧ 잉여창고 수량 변경함수
		/// </summary>
		private void RedundentStockConsidering()
		{
			string str = " select * From TBWorkPlanning order by Path, ProcessSequenceNum desc";
			
			SqlDataAdapter da = new SqlDataAdapter(str,conn);
			
			SqlCommandBuilder bulder = new SqlCommandBuilder(da);

			DataSet dataset = new DataSet();
			
			da.Fill(dataset);
			decimal ConsideringQuantity = 0;
			
			for(int rowcount = 0; rowcount < dataset.Tables[0].Rows.Count; rowcount++)
			{
				//해당 제품에 대한 작업계획수량에 대해 각 공정별 작업수량을 조사한다
				string ItemNum = dataset.Tables[0].Rows[rowcount]["ItemNum"].ToString();
				string ProcessCode = dataset.Tables[0].Rows[rowcount]["ProcessCode"].ToString();
				decimal Quantity = decimal.Parse(dataset.Tables[0].Rows[rowcount]["Quantity"].ToString());
				//만약 작업계획수량이 0이면 다음레코드로 바로 이동
				if(Quantity == 0)
					continue;

				ConsideringQuantity = NowStockQuantity(ItemNum,ProcessCode);

				//⑧-1현재 해당 공정품의 수량을 변수에 넣어둔다
				// 현재고량이 작업수량보다 많으면 재고량을 (재고량-작업수량)으로 변경시키고
				// 작업수량을 0으로 세팅하면서 이후 ConsideringQuantity에 변경된 재고량을 넣어둔다
				if(Quantity - ConsideringQuantity <= 0)
				{
					//재고량 변경
					//str = "Update TBWorkRedundant Set Quantity = @Quantity where ItemNum = @ItemNum and ProcessCode = @ProcessCode";
					str = "Update TBWorkRedundantSum Set Quantity = @Quantity where ItemNum = @ItemNum and ProcessCode = @ProcessCode";
					SqlCommand comm = new SqlCommand(str,conn);
					comm.CommandText = str;
					comm.Parameters.Add("@ItemNum",ItemNum);
					comm.Parameters.Add("@ProcessCode",ProcessCode);
					comm.Parameters.Add("@Quantity",(ConsideringQuantity-Quantity));
					comm.ExecuteNonQuery();


					// 작업수량을 0으로 세팅
					dataset.Tables[0].Rows[rowcount]["Quantity"] = 0;

					//기존의 작업수량에 넣어둔다
					ConsideringQuantity = Quantity;
						
				}
				else
				{
					// 현재고량이 작업수량보다 작으면 현재고량을 0으로 변경시키고
					// 작업수량을 (작업수량-현재고량)으로 변경시킨다.
					//str = "Update TBWorkRedundant Set Quantity = 0 where ItemNum = @ItemNum and ProcessCode = @ProcessCode";
					str = "Update TBWorkRedundantSum Set Quantity = 0 where ItemNum = @ItemNum and ProcessCode = @ProcessCode";
					SqlCommand comm = new SqlCommand(str,conn);
					comm.CommandText = str;
					comm.Parameters.Add("@ItemNum",ItemNum);
					comm.Parameters.Add("@ProcessCode",ProcessCode);
					comm.ExecuteNonQuery();


					// 작업수량을 변경
					dataset.Tables[0].Rows[rowcount]["Quantity"] = Quantity-ConsideringQuantity;
				}

				//해당 공정의 하위품목 공정의 작업수량을 변경시킨다
				for(int a = rowcount+1; a < dataset.Tables[0].Rows.Count; a++)
				{
					
					if(dataset.Tables[0].Rows[rowcount]["ItemNum"].ToString() != dataset.Tables[0].Rows[a]["ItemNum"].ToString() &&
						int.Parse(dataset.Tables[0].Rows[rowcount]["Lvl"].ToString()) <= int.Parse(dataset.Tables[0].Rows[a]["Lvl"].ToString()))
						break;
					else
					{
						//하위자품목에 대한 분자량/분모량 비율
						decimal X = 1 ;

						// ⑧-2 해당레코드와 선택레코의 레벨이 다르면 선택레코드는 자품목이므로 소요량분자/소요량분모 비율을 계산해서
						//작업수량에 적용시켜야 한다 
						if(dataset.Tables[0].Rows[rowcount]["Lvl"].ToString() != dataset.Tables[0].Rows[a]["Lvl"].ToString())
							X = FindingRatio(dataset.Tables[0].Rows[rowcount]["ItemNum"].ToString(),dataset.Tables[0].Rows[a]["ItemNum"].ToString());
						if(X == 0) continue;
						//작업수량은 변경한다.
						dataset.Tables[0].Rows[a]["Quantity"] = (decimal.Parse(dataset.Tables[0].Rows[a]["Quantity"].ToString())-(X*ConsideringQuantity));
					}
				}				
			}
			da.Update(dataset);
		}


		/// <summary>
		/// ⑧-1해당 공정품의 현재 잉여재고테이블에 존재하는 수량을 찾아오는 함수
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <param name="ProcessCode"></param>
		/// <returns></returns>
		private decimal NowStockQuantity(string ItemNum, string ProcessCode)
		{
			string str = "Select Quantity From TBWorkRedundantSum where ItemNum = @ItemNum and ProcessCode = @ProcessCode";
			SqlCommand comm = new SqlCommand(str,conn);
			//comm.Transaction = tr;
			comm.Parameters.Add("@ItemNum",ItemNum);
			comm.Parameters.Add("@ProcessCode",ProcessCode);

			return decimal.Parse(comm.ExecuteScalar().ToString());
		}


		/// <summary>
		/// ⑧-2 소요량 비율 구하는 함수
		/// </summary>
		/// <param name="Parent">모품목</param>
		/// <param name="Child">자품목</param>
		/// <returns></returns>
		private decimal FindingRatio(string Parent, string Child)
		{
			if(Parent == Child)
				return 1;
			else
			{
				//먼저 던져진 품목 Parent 를 가지고 BOM을 만든다
				string str = @"SPBOMTree";
				SqlCommand comm = new SqlCommand();
				comm.Connection = conn;
				comm.CommandText = str;
				comm.CommandType = CommandType.StoredProcedure;
				comm.Parameters.Add("@ItemNum",Parent);
				comm.Parameters.Add("@Quantity",1);
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();

				decimal ChildQuantity = 0;

				str = @"Select  Count(*) From TBTempBOMTree where ItemNum = @ItemNum";
				comm.Parameters.Add("@ItemNum",Child);
				comm.CommandText = str;
				comm.CommandType = CommandType.Text;
				int count = int.Parse(comm.ExecuteScalar().ToString());
				if(count != 0)
				{
					str = @"Select  Quantity From TBTempBOMTree where ItemNum = @ItemNum";
					comm.CommandText = str;
					comm.CommandType = CommandType.Text;
					ChildQuantity = decimal.Parse(comm.ExecuteScalar().ToString());
					comm.Parameters.Clear();
				}
				
				return (ChildQuantity);
			}

		}



		/// <summary>
		/// 해당 품목의 자산분류가 원자재인지 아닌지 판단하는 함수
		/// </summary>
		/// <param name="Item">품목번호</param>
		/// <returns>해당 품목의 자산분류 값을 리턴</returns>
		private bool Division(string Item)
		{
			string aa="";
			string str = "Select PropertyClassification From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm =  new SqlCommand(str,conn);
			//comm.Transaction = tr;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = Item;
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = dr["PropertyClassification"].ToString();
			}
			dr.Close();

			if(aa.ToString() == "원자재")
				return true;
			else 
				return false;
		}
		

		/// <summary>
		/// ③-1해당 공정품의 작업구분이 외주인지 판단하는 함수
		/// </summary>
		/// <param name="Item"></param>
		/// <param name="Code"></param>
		/// <returns></returns>
		private bool Division(string Item, string Code)
		{
			string aa="";

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select WorkDistinction From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
			SqlCommand comm =  new SqlCommand(str,conn);
			//comm.Transaction = tr;
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
		/// ⑨ 제품에 대한 해당공정 작업계획 수량 수립시
		/// 자가외주에 대하여 외주레코드를 추가하는 함수
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <param name="Quantity"></param>
		private void OutOrderRecordGenerating(string ItemNum, decimal Quantity)
		{
			//string str = "SPWorkItemBOM";
			string str = @"select T.ItemNum, P.ProcessSequenceNum, P.ProcessCode, P.WorkDistinction,T.Quantity, T.Lvl, T.Path
						From TBWorkPlanning T join PSI_MT P on  T.ItemNum = P.ItemNum and T.ProcessSequenceNum = P.ProcessSequenceNum
						Where P.RecodingState = 1
						order by Path, T.ProcessSequenceNum desc";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			//comm.Transaction = tr;
			comm.CommandText = str;
			//comm.CommandType = CommandType.StoredProcedure;
			comm.Parameters.Add("@ItemNum",ItemNum);
			comm.Parameters.Add("@Quantity",Quantity);
			DataSet dataset = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(comm);
			da.Fill(dataset);
			comm.Parameters.Clear();
			
			
			foreach(DataRow dr in dataset.Tables[0].Rows)
			{
				//해당공정품이 자가외주이면 TBWorkDivision테이블에 외주레코드 하나를 더 생성한다.
				if(dr["WorkDistinction"].ToString() == "자가/외주")
				{
					str = "SPUpdateWorkDistinction";
					comm.CommandType = CommandType.StoredProcedure;
					comm.CommandText = str;
					comm.Parameters.Add("@ItemNum",dr["ItemNum"].ToString());
					comm.Parameters.Add("@ProcessSequenceNum",int.Parse(dr["ProcessSequenceNum"].ToString()));
					comm.Parameters.Add("@ProcessCode",dr["ProcessCode"].ToString());
					comm.Parameters.Add("@Quantity",decimal.Parse(dr["Quantity"].ToString()));
					comm.Parameters.Add("@Lvl",int.Parse(dr["Lvl"].ToString()));
					comm.Parameters.Add("@Path",dr["Path"].ToString());
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
				}
			}
		}

		/// <summary>
		/// ⑩ 외주수량에 대해 자가작업계획 수량 조절함수
		/// </summary>
		private void OutOrderQuantityAdjusting()
		{
			//먼저 TBWorkPlanning테이블에서 작업구분이 외주인레코드만 선택해 가져온다
			string str = "SPOutOrderRecordExtracting";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			//comm.Transaction = tr;
			comm.CommandText = str;
			comm.CommandType = CommandType.StoredProcedure;
			DataSet dataset = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(comm);
			da.Fill(dataset);

			
			foreach(DataRow dr in dataset.Tables[0].Rows)
			{
				//⑩-1 가져온 공정을 가지고 시작공정을 찾아 넣어둔다
				//만일 없으면 다음레코드로 이동한다
				string StartWorkStep = null;
				StartWorkStep = ExistinFinalWorkStep(dr["ItemNum"].ToString(),dr["ProcessCode"].ToString());
				if(StartWorkStep == null) continue;

				// ⑩-2 해당 품목과 찾은 공정을 가지고 출고품목을 찾아 ArrayList에 넣어둔다
				ArrayList OutItem = SubItemSearch(dr["ItemNum"].ToString(),StartWorkStep);


				// 외주 입고품이후공정을 삭제하기위해
				// 임시로 Bom테이블을 만든다
				str = "SPTempOutWorkPlanning";
				comm.CommandText = str;
				//comm.Transaction = tr;
				comm.CommandType = CommandType.StoredProcedure;
				comm.Parameters.Add("@ItemNum",dr["ItemNum"].ToString());
				comm.Parameters.Add("@Quantity",decimal.Parse(dr["Quantity"].ToString()));
				DataSet ds = new DataSet();
				SqlDataAdapter da1 = new SqlDataAdapter(comm);
				da1.Fill(ds);
				comm.Parameters.Clear();


				// 먼저 해당 레코드의 작업을 제외한 상위작업들을 삭제한다
				DeleteUpperWorkStep(dr["ItemNum"].ToString(),dr["ProcessCode"].ToString());

				for(int count = 0; count < OutItem.Count;count++)
				{
					WCSubItem wcsubitem = new WCSubItem();
					wcsubitem = (WCSubItem)OutItem[count];

					// 해당 시작공정작업의  이후공정작업들에 대하여 삭제한다
					DeleteLowerStep(wcsubitem.ItemNum,wcsubitem.ProcessCode);
				}

				OutItem.Clear();
				decimal MaxQuantity = 0;

				// 위의 문장들을 수행하고 나면 외주에서 작업할 공정들만 남게된다
				// 그러면 다시 TBTempWorkPlanning테이블의 레코드르 가져온다
				str = "Select * From TBTempWorkPlanning order by Path,ProcessSequenceNum desc";
				comm.CommandText = str;
				comm.CommandType = CommandType.Text;
				DataSet dset = new DataSet();
				SqlDataAdapter da2 = new SqlDataAdapter(comm);
				da2.Fill(dset);
				
				//해당 외주수량중에서 가장 큰 값을 MaxQuantity에 넣어둔다
				foreach(DataRow dt in dset.Tables[0].Rows)
				{
					decimal x = FindingRatio(dr["ItemNum"].ToString(),dt["ItemNum"].ToString());
					if(x == 0)	continue;
					MaxQuantity = FindingMaxQuantity(MaxQuantity,x,dt["ItemNum"].ToString(),dt["ProcessCode"].ToString());
				}

				foreach(DataRow dq in dset.Tables[0].Rows)
				{
					decimal x = FindingRatio(dr["ItemNum"].ToString(),dq["ItemNum"].ToString());
					if(x == 0)	continue;
					decimal InQuantity = AdjustOutQuantity(MaxQuantity,x,dq["ItemNum"].ToString(),dq["ProcessCode"].ToString());

					
					//자가수량변경에 true가 되있으면
					if(hidInWorkConsidering.Value == "true")
						AdjustInQuantity(InQuantity,x,dq["ItemNum"].ToString(),dq["ProcessCode"].ToString());
				}
			}
		}


		/// <summary>
		/// 외주수량을 최대값으로 적용하는 함수
		/// </summary>
		/// <param name="MaxQun"></param>
		/// <param name="Ratio"></param>
		/// <param name="ItemNum"></param>
		/// <param name="ProcessCode"></param>
		/// <returns></returns>
		private decimal FindingMaxQuantity(decimal MaxQun, decimal Ratio, string ItemNum, string ProcessCode)
		{
			decimal Quantity = 0;
			string str = "Select Quantity From TBTempWorkPlanning Where ItemNum = @ItemNum and ProcessCode = @ProcessCode";
			SqlCommand comm = new SqlCommand(str,conn);
			//comm.Transaction = tr;
			comm.Parameters.Add("@ItemNum",ItemNum);
			comm.Parameters.Add("@ProcessCode",ProcessCode);
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				Quantity = decimal.Parse(dr["Quantity"].ToString())/Ratio;
			}
			dr.Close();


			if(MaxQun > Quantity)
				return MaxQun;
			else
				return Quantity;
		}


		/// <summary>
		/// 최대외주수량 함수
		/// </summary>
		/// <param name="MaxQun"></param>
		/// <param name="Ratio"></param>
		/// <param name="ItemNum"></param>
		/// <param name="ProcessCode"></param>
		/// <returns></returns>
		private decimal AdjustOutQuantity(decimal MaxQun, decimal Ratio, string ItemNum, string ProcessCode)
		{
			decimal Quantity = 0;
			string str = "Select Quantity From TBTempWorkPlanning Where ItemNum = @ItemNum and ProcessCode = @ProcessCode";
			SqlCommand comm = new SqlCommand(str,conn);
			//comm.Transaction = tr;
			comm.Parameters.Add("@ItemNum",ItemNum);
			comm.Parameters.Add("@ProcessCode",ProcessCode);
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				Quantity = decimal.Parse(dr["Quantity"].ToString())*Ratio;
			}
			dr.Close();
			comm.Parameters.Clear();

			// TBWorkPlanning테이블의 외주수량을 최대값으로 변경
			str  = "Update TBWorkPlanning Set Quantity = @Quantity where ItemNum = @ItemNum and ProcessCode = @ProcessCode and WorkDistinction = '외주'";
			comm.CommandText = str;
			comm.CommandType = CommandType.Text;
			comm.Parameters.Add("@ItemNum",ItemNum);
			comm.Parameters.Add("@ProcessCode",ProcessCode);
			comm.Parameters.Add("@Quantity",MaxQun);
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			return (MaxQun - Quantity);
			
		}


		/// <summary>
		/// 자가수량 변경 함수
		/// </summary>
		/// <param name="MaxQun"></param>
		/// <param name="Ratio"></param>
		/// <param name="ItemNum"></param>
		/// <param name="ProcessCode"></param>
		/// <returns></returns>
		private void AdjustInQuantity(decimal MaxQun, decimal Ratio, string ItemNum, string ProcessCode)
		{
			decimal Quantity = 0;
			string str = "Select Quantity From TBWorkPlanning Where ItemNum = @ItemNum and ProcessCode = @ProcessCode and WorkDistinction = '자가'";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@ItemNum",ItemNum);
			comm.Parameters.Add("@ProcessCode",ProcessCode);
			//comm.Transaction = tr;
			SqlDataReader dr = comm.ExecuteReader();
			comm.Parameters.Clear();
			if(dr.Read())
			{
				Quantity = decimal.Parse(dr["Quantity"].ToString())*Ratio;
			}
			dr.Close();

			if(MaxQun > Quantity)
				Quantity = MaxQun - Quantity;
			else
				Quantity = Quantity - MaxQun;

			if(Quantity != 0)
			{
				str  = "Update TBWorkPlanning Set Quantity = @Quantity where ItemNum = @ItemNum and ProcessCode = @ProcessCode and WorkDistinction = '자가'";
				comm.CommandText = str;
				comm.Parameters.Add("@ItemNum",ItemNum);
				comm.Parameters.Add("@ProcessCode",ProcessCode);
				comm.Parameters.Add("@Quantity",Quantity);
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}


			
		}
        
		/// <summary>
		/// ⑩-1종료공정을 가지고 시작공정을 찾는 함수
		/// </summary>
		/// <param name="ItemNum">품목번호</param>
		/// <param name="ProcessCode">종료공정</param>
		/// <returns></returns>
		private string ExistinFinalWorkStep(string ItemNum, string ProcessCode)
		{
			string Begin = null;
			string str = "Select * From UCI_MT Where ItemNum = @ItemNum and RecodingState = 1 and EndProcessCode = @ProcessCode";
			SqlCommand comm = new SqlCommand(str,conn);
			//comm.Transaction = tr;
			comm.Parameters.Add("@ItemNum",ItemNum);
			comm.Parameters.Add("@ProcessCode",ProcessCode);

			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Begin = dr["BeginProcessCode"].ToString();
			}
			dr.Close();

			return Begin;
		}

		/// <summary>
		/// 해당 공정품의 상위공정 작업을 삭제하는 함수
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <param name="ProcessCode"></param>
		private void DeleteUpperWorkStep(string ItemNum, string ProcessCode)
		{
			

			string str = "Select * From TBTempWorkPlanning order by Path, ProcessSequenceNum desc";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.CommandText = str;
			comm.Connection = conn;
			comm.CommandType = CommandType.Text;
			DataSet ds = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(comm);
			da.Fill(ds);
			comm.Parameters.Clear();

			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				if(ItemNum == dr["ItemNum"].ToString() && ProcessCode == dr["ProcessCode"].ToString())
				{
					break;
				}
				else
				{
					str = @"Delete From TBTempWorkPlanning Where ItemNum = @ItemNum and ProcessCode = @ProcessCode";
					comm.CommandText= str;
					comm.CommandType = CommandType.Text;
					comm.Parameters.Add("@ItemNum",dr["ItemNum"].ToString());
					comm.Parameters.Add("@ProcessCode",dr["ProcessCode"].ToString());
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
					continue;
				}
			}

			
		}


		/// <summary>
		/// 해당 공정품의 하위공정 작업을 삭제하는 함수
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <param name="ProcessCode"></param>
		private void DeleteLowerStep(string ItemNum, string ProcessCode)
		{
			//먼저 종료공정 이전의 품목이 삭제된 TBTempWorkPlanning 테이블을 가져온다
			string str = "Select * From TBTempWorkPlanning order by Path, ProcessSequenceNum desc";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			//comm.Transaction = tr;
			comm.CommandText = str;
			comm.CommandType = CommandType.Text;
			DataSet dataset = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(comm);
			da.Fill(dataset);
			comm.Parameters.Clear();

			// 해당 품목과 공정정보와 동일한지 처음부터 내려가면서
			// 비교해본다. 만약 동일한 품목과 공정이 발견되면 해당
			// 행의 위치값을 가지고 빠져나온다.
			int count = 0;
			bool process = false;
			for( ; count < dataset.Tables[0].Rows.Count; count++)
			{
				if(dataset.Tables[0].Rows[count]["ItemNum"].ToString() == ItemNum && 
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
				if(int.Parse(dataset.Tables[0].Rows[count]["ProcessSequenceNum"].ToString()) > MinProcessSequenceNum(ItemNum))
				{
					
					//해당공정품의 바로 아랫단계에 모든 레코드를 TempWorkPlanning테이블에서 삭제한다.
					int count1 = count;
					for( ; count1 < dataset.Tables[0].Rows.Count; count1++)
					{
																											
						str = @"Delete From TBTempWorkPlanning Where ItemNum = @ItemNum and ProcessCode = @ProcessCode";
						comm.CommandText= str;
						comm.CommandType = CommandType.Text;
						comm.Parameters.Add("@ItemNum",ItemNum);
						comm.Parameters.Add("@ProcessCode",dataset.Tables[0].Rows[count1]["ProcessCode"].ToString());
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();
						
					}

				}
				else
				{
					//그렇지 않으면 선택된 공정품에서 시작해 끝까지 돌면서
					//출고품목들을 찾아낸다.
					for(int rowcount = count; rowcount < dataset.Tables[0].Rows.Count; rowcount++)
					{
						
						// ③-2 원자재가 아닌경우는 해당 공정품 이후에 달려있는 하위품목들의 레코드는 삭제해주어야 한다
						// 삭제 대상은 해당 공정의 Lvl 값과 동일한 값을 가진 레코드를 만날때까지 삭제한다.
						//해당공정품의 바로 아랫단계에 있는 레코드부터  TempWorkPlanning테이블에서 삭제한다
						str = @"Delete From TBTempWorkPlanning Where ItemNum = @ItemNum and ProcessCode = @ProcessCode";
						comm.CommandText= str;
						comm.CommandType = CommandType.Text;
						comm.Parameters.Add("@ItemNum",dataset.Tables[0].Rows[rowcount]["ItemNum"].ToString());
						comm.Parameters.Add("@ProcessCode",dataset.Tables[0].Rows[rowcount]["ProcessCode"].ToString());
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();
							
						// 출고품목이 확정된 경우 지금 공정의 하위에 달려있는 공정들은 의미가 없으므로
						// 넘어간다
						// 단 차례로 넘어가다 보면 해당 공정의 Lvl값과 동일한 값이나 그이상을 가지는 레코드를 만나게
						// 될 수 있는데 이때 빠져나온다.(물론 해당 품목과 동일한 품목인 경우는 삭제해야 한다)
						int a = rowcount+1;
						for( ; a< dataset.Tables[0].Rows.Count; a++)
						{

							str = @"Delete From TBTempWorkPlanning Where ItemNum = @ItemNum and ProcessCode = @ProcessCode";
							comm.CommandText= str;
							comm.CommandType = CommandType.Text;
							comm.Parameters.Add("@ItemNum",dataset.Tables[0].Rows[a]["ItemNum"].ToString());
							comm.Parameters.Add("@ProcessCode",dataset.Tables[0].Rows[a]["ProcessCode"].ToString());
							comm.ExecuteNonQuery();
							comm.Parameters.Clear();
						}
						break;
					}
				}
			}
		}

		/// <summary>
		/// 예비작업계획원장에 넣어두는 함수
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <param name="Quantity"></param>
		/// <param name="arr"></param>
		private void InsertPreWorkPlanDB(string ItemNum,decimal Quantity, ArrayList arr)
		{
			//string str = "Select * From TBWorkPlanning order by Path, ProcessSequenceNum desc";
			//string str = "Select * From TBWorkPlanning order by Lvl, Path, ItemNum, ProcessSequenceNum";
			string str = @"Select TB.ItemNum, CompleteLeadTime ,TB.Lvl,  TB.ProcessSequenceNum, TB.ProcessCode, TB.WorkDistinction, 
							TB.Quantity, TB.Path, [ID] From TBWorkPlanning TB inner join II_MT on TB.ItemNum = II_MT.ItemNum where recodingstate=1 order by ID";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			//comm.Transaction = tr;
			comm.CommandText = str;
			comm.CommandType = CommandType.Text;
			comm.Parameters.Add("@ItemNum",ItemNum);
			comm.Parameters.Add("@Quantity",Quantity);
			DataSet dataset = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(comm);
			da.Fill(dataset);
			comm.Parameters.Clear();
			for(int a = 0; a<dataset.Tables[0].Rows.Count; a++)
			{
				//리드타임 입력
				//int Lvl = 1;
				//int LeadTime = 0;


				str = @"Insert into TBPreWorkPlan (ItemNum, ProcessSequenceNum, ProcessCode,WorkDistinction, WorkPlanQuantity, WorkDate, WorkCompleteDate,
					DeliveryDate, ProductionPlanHistoryIndex)
					Values(@ItemNum, @ProcessSequenceNum, @ProcessCode, @WorkDistinction, @WorkPlanQuantity, @WorkDate, @WorkCompleteDate,
					@DeliveryDate, @ProductionPlanHistoryIndex)";

				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = dataset.Tables[0].Rows[a]["ItemNum"].ToString().Trim();
				comm.Parameters.Add("@ProcessSequenceNum",SqlDbType.TinyInt).Value = int.Parse(dataset.Tables[0].Rows[a]["ProcessSequenceNum"].ToString());
				comm.Parameters.Add("@ProcessCode",SqlDbType.VarChar).Value = dataset.Tables[0].Rows[a]["ProcessCode"].ToString();
				comm.Parameters.Add("@WorkDistinction",SqlDbType.VarChar).Value = dataset.Tables[0].Rows[a]["WorkDistinction"].ToString();
				comm.Parameters.Add("@WorkPlanQuantity",SqlDbType.Decimal).Value = decimal.Parse(dataset.Tables[0].Rows[a]["Quantity"].ToString());
				comm.Parameters.Add("@WorkDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(arr[0].ToString()).ToShortDateString();
				comm.Parameters.Add("@ProductionPlanHistoryIndex",SqlDbType.Int).Value = int.Parse(arr[1].ToString());
				comm.Parameters.Add("@DeliveryDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(arr[2].ToString()).ToShortDateString();
				

				comm.Parameters.Add("@WorkCompleteDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(arr[2].ToString()).ToShortDateString();
//				if(a==0)
//				{
//					comm.Parameters.Add("@WorkCompleteDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(arr[2].ToString()).ToShortDateString();
//					
//					LeadTime =  int.Parse(dataset.Tables[0].Rows[a]["CompleteLeadTime"].ToString());
//				}
//				else
//				{
//					if(Lvl == int.Parse(dataset.Tables[0].Rows[a]["Lvl"].ToString()))
//					{
//						comm.Parameters.Add("@WorkCompleteDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(arr[2].ToString()).AddDays(LeadTime).ToShortDateString();
//					}
//				}
				comm.CommandText = str;
				comm.CommandType = CommandType.Text;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
//			foreach(DataRow dr in dataset.Tables[0].Rows)
//			{
//				str = @"Insert into TBPreWorkPlan (ItemNum, ProcessSequenceNum, ProcessCode,WorkDistinction, WorkPlanQuantity, WorkDate, WorkCompleteDate,
//					DeliveryDate, ProductionPlanHistoryIndex)
//					Values(@ItemNum, @ProcessSequenceNum, @ProcessCode, @WorkDistinction, @WorkPlanQuantity, @WorkDate, @WorkCompleteDate,
//					@DeliveryDate, @ProductionPlanHistoryIndex)";
//
//				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = dr["ItemNum"].ToString().Trim();
//				comm.Parameters.Add("@ProcessSequenceNum",SqlDbType.TinyInt).Value = int.Parse(dr["ProcessSequenceNum"].ToString());
//				comm.Parameters.Add("@ProcessCode",SqlDbType.VarChar).Value = dr["ProcessCode"].ToString();
//				comm.Parameters.Add("@WorkDistinction",SqlDbType.VarChar).Value = dr["WorkDistinction"].ToString();
//				comm.Parameters.Add("@WorkPlanQuantity",SqlDbType.Decimal).Value = decimal.Parse(dr["Quantity"].ToString());
//				comm.Parameters.Add("@WorkDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(arr[0].ToString()).ToShortDateString();
//				comm.Parameters.Add("@ProductionPlanHistoryIndex",SqlDbType.Int).Value = int.Parse(arr[1].ToString());
//				if(arr[2].ToString() == "")
//				{
//					comm.Parameters.Add("@DeliveryDate",SqlDbType.SmallDateTime).Value = Convert.DBNull;
//					comm.Parameters.Add("@WorkCompleteDate",SqlDbType.SmallDateTime).Value = Convert.DBNull;
//				}
//				else
//				{
//					comm.Parameters.Add("@DeliveryDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(arr[2].ToString()).ToShortDateString();
//					comm.Parameters.Add("@WorkCompleteDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(arr[2].ToString()).ToShortDateString();
//				}
//				comm.CommandText = str;
//				comm.CommandType = CommandType.Text;
//				comm.ExecuteNonQuery();
//				comm.Parameters.Clear();
//			}
			
		}

		/// <summary>
		/// 예비작업계획원장에 공정과 납기일이 동일하면 수량을 Sum하는 함수
		/// </summary>
		private void BundlingforDeliveryDate()
		{
			string str  = "SPBundleforDeliveryDate";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.CommandType = CommandType.StoredProcedure;
			comm.ExecuteNonQuery();

		}

		/// <summary>
		/// 실제 작업계획원장에 인서트 하는 함수
		/// </summary>
		private void InsertWorkPlanDB()
		{
			string str = "Select * From TBPreWorkPlan ";
			SqlCommand comm = new SqlCommand(str,conn);
			//comm.Transaction = tr;
			DataSet dataset = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(comm);
			da.Fill(dataset);

			foreach(DataRow dr in dataset.Tables[0].Rows)
			{
				if(decimal.Parse(dr["WorkPlanQuantity"].ToString()) != 0)
				{
					// 품목명, 도면번호, 공정명, 제품명, 작업장명, 리드타임 등을 입력하기위한 ArrayList 				
					if(dr["ProductionPlanHistoryIndex"].ToString() == null || dr["ProductionPlanHistoryIndex"].ToString().Trim() == "")
					{
						arrlist = FindItem(dr["ItemNum"].ToString(),dr["ProcessCode"].ToString());
					}
					else
					{
						arrlist = FindItem(dr["ItemNum"].ToString(),dr["ProcessCode"].ToString(),int.Parse(dr["ProductionPlanHistoryIndex"].ToString()));
					}

					string str_Up = @"Insert into WDWP_HT (ItemNum, ItemDrawNum, ItemName, ProcessSequenceNum, ProcessCode, EtcText, PreProcessName,
					ProcessName, ProductItemNum, ProductDrawNum, ProductName, WCName, WorkDistinction, WorkPlanQuantity, WorkCompletionQuantity, OrderLeadTime, WorkDate, WorkCompleteDate,
					DeliveryDate, VolumNum , ProgressCondition, RegistrationPerson, RegistrationPersonID, RegistrationDate, ProductionPlanHistoryIndex)
					Values(@ItemNum, @ItemDrawNum, @ItemName, @ProcessSequenceNum, @ProcessCode, @EtcText,@PreProcessName,
					@ProcessName, @ProductItemNum, @ProductDrawNum, @ProductName, @WCName, @WorkDistinction, @WorkPlanQuantity, @WorkCompletionQuantity, @OrderLeadTime, @WorkDate, @WorkCompleteDate,
					@DeliveryDate, @VolumNum,'대기', @RegistrationPerson, @RegistrationPersonID, @RegistrationDate, @ProductionPlanHistoryIndex)";
					comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = dr["ItemNum"].ToString().Trim();
					comm.Parameters.Add("@ItemDrawNum",SqlDbType.VarChar).Value = arrlist[0].ToString();
					comm.Parameters.Add("@ItemName",SqlDbType.VarChar).Value = arrlist[1].ToString();
					comm.Parameters.Add("@ProcessSequenceNum",SqlDbType.TinyInt).Value = int.Parse(dr["ProcessSequenceNum"].ToString());
					comm.Parameters.Add("@ProcessCode",SqlDbType.VarChar).Value = dr["ProcessCode"].ToString();
					comm.Parameters.Add("@ProcessName",SqlDbType.VarChar).Value = arrlist[2].ToString();
					comm.Parameters.Add("@EtcText",SqlDbType.VarChar).Value = EtcText(dr["ItemNum"].ToString().Trim(),dr["ProcessCode"].ToString());
					comm.Parameters.Add("@PreProcessName",SqlDbType.VarChar).Value = StoreManage(dr["ItemNum"].ToString().Trim());
					comm.Parameters.Add("@ProductItemNum",SqlDbType.VarChar).Value = arrlist[3].ToString();
					comm.Parameters.Add("@ProductDrawNum",SqlDbType.VarChar).Value = arrlist[4].ToString();
					comm.Parameters.Add("@ProductName",SqlDbType.VarChar).Value = arrlist[5].ToString();
					comm.Parameters.Add("@WorkDistinction",SqlDbType.VarChar).Value = dr["WorkDistinction"].ToString();
					if(dr["WorkDistinction"].ToString() == "자가")
					{
						comm.Parameters.Add("@OrderLeadTime",SqlDbType.Decimal).Value = 0;
						comm.Parameters.Add("@WCName",SqlDbType.VarChar).Value = arrlist[6].ToString();
					}			
					else
					{
						comm.Parameters.Add("@OrderLeadTime",SqlDbType.Decimal).Value = decimal.Parse(arrlist[7].ToString());				
						comm.Parameters.Add("@WCName",SqlDbType.VarChar).Value = "";
					}
					comm.Parameters.Add("@WorkPlanQuantity",SqlDbType.Decimal).Value = decimal.Parse(dr["WorkPlanQuantity"].ToString());
					comm.Parameters.Add("@WorkCompletionQuantity",SqlDbType.Decimal).Value = 0;
					if(dr["WorkDate"].ToString() == null || dr["WorkDate"].ToString().Trim() == "")
						//comm.Parameters.Add("@WorkDate",SqlDbType.SmallDateTime).Value = Convert.DBNull;
						comm.Parameters.Add("@WorkDate",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					else
						comm.Parameters.Add("@WorkDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(dr["WorkDate"].ToString()).ToShortDateString();

					if(dr["WorkCompleteDate"].ToString() == null || dr["WorkCompleteDate"].ToString().Trim() == "")
						comm.Parameters.Add("@WorkCompleteDate",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					else
						comm.Parameters.Add("@WorkCompleteDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(dr["WorkCompleteDate"].ToString()).ToShortDateString();

					if(dr["DeliveryDate"].ToString()== null || dr["DeliveryDate"].ToString().Trim() =="")
						comm.Parameters.Add("@DeliveryDate",SqlDbType.SmallDateTime).Value = "1999-01-01";
					else
						comm.Parameters.Add("@DeliveryDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(dr["DeliveryDate"].ToString()).ToShortDateString();
					comm.Parameters.Add("@VolumNum",SqlDbType.Int).Value = Vol;
					comm.Parameters.Add("@RegistrationPerson",SqlDbType.VarChar).Value = Session["UserName"].ToString();
					comm.Parameters.Add("@RegistrationPersonID",SqlDbType.VarChar).Value = Session["ID"].ToString();
					comm.Parameters.Add("@RegistrationDate",SqlDbType.DateTime).Value = DateTime.Now.ToShortDateString();
					if(dr["ProductionPlanHistoryIndex"].ToString() == null || dr["ProductionPlanHistoryIndex"].ToString() == "")
						comm.Parameters.Add("@ProductionPlanHistoryIndex",SqlDbType.Int).Value = Convert.DBNull;
					else
						comm.Parameters.Add("@ProductionPlanHistoryIndex",SqlDbType.Int).Value = int.Parse(dr["ProductionPlanHistoryIndex"].ToString());
					comm.CommandText = str_Up;
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();

					arrlist.Clear();
				}	
			}
		}



		private string EtcText(string ItemNum, string ProcessCode)
		{
			string Etc = "";
			string str = "Select EtcText From PSI_MT Where ItemNum = @ItemNum and ProcessCode = @ProcessCode and RecodingState = 1";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum",ItemNum);
			comm.Parameters.Add("@ProcessCode",ProcessCode);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Etc = dr["EtcText"].ToString();
			}
			dr.Close();	
			comm.Parameters.Clear();
			return Etc;

		}


		/// <summary>
		/// 재고관리여부
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <returns></returns>
		private string StoreManage(string ItemNum)
		{
			string store = "아니오";
			string str = "Select Unit4 From II_MT Where ItemNum = @ItemNum and RecodingState = 1";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum",ItemNum);			
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				store = dr["Unit4"].ToString();
					 		
			}
			dr.Close();	
			comm.Parameters.Clear();

			return store;
		}



		/// <summary>
		/// 작업추가된 품목의 도면번호및 작업장 찾기
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <param name="ProcessCode"></param>
		/// <returns></returns>
		private ArrayList FindItem(string ItemNum, string ProcessCode)
		{
			//품목명, 도면번호 찾기
			string str = "Select ItemDrawNum, ItemName From II_MT Where ItemNum = @ItemNum and RecodingState = 1";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			//comm.Transaction = tr;
			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				arrlist.Add(dr["ItemDrawNum"].ToString());
				arrlist.Add(dr["ItemName"].ToString());
			}
			dr.Close();	
			comm.Parameters.Clear();
			
			//공정명 찾기
			str = "Select * From PUC_MT Where SmallClassificationCode = @ProcessCode and RecodingState= 1";
			comm.CommandText = str;
			comm.Parameters.Add("@ProcessCode",ProcessCode);
			SqlDataReader dr1 = comm.ExecuteReader();
			while(dr1.Read())
			{
				arrlist.Add(dr1["SmallClassificationName"].ToString());
			}
			dr1.Close();
			comm.Parameters.Clear();

			// 추가되는 항목에 대해서는 제품이름을 입력하지 않는다
			arrlist.Add("");
			arrlist.Add("");
			arrlist.Add("");


			//작업장명 찾기
			str = @"Select * From WSI_MT Where ItemNum = @ItemNum and ProcessCode = @ProcessCode and RecodingState = 1 and 
				PriorityOrder = (Select Min(PriorityOrder) From WSI_MT Where  ItemNum = @ItemNum and ProcessCode = @ProcessCode and RecodingState = 1)";
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum",ItemNum);
			comm.Parameters.Add("@ProcessCode",ProcessCode);
			DataSet dset = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(comm);
			da.Fill(dset);
			if(dset.Tables[0].Rows.Count == 0)
				arrlist.Add("");
			else
				arrlist.Add(dset.Tables[0].Rows[0]["WCName"].ToString());				
			
			//외주리드타임 찾기
			str = "Select * From PSI_MT Where ItemNum = @ItemNum and ProcessCode = @ProcessCode and RecodingState = 1";
			comm.CommandText = str;
			DataSet dataset = new DataSet();
			SqlDataAdapter da1 = new SqlDataAdapter(comm);
			da1.Fill(dataset);

			if(dataset.Tables[0].Rows.Count == 0)
				arrlist.Add(0);
			else
			{
				arrlist.Add(dataset.Tables[0].Rows[0]["LeadTime"].ToString());
			}
			comm.Parameters.Clear();

			return arrlist;


		}

		/// <summary>
		/// 품목의 도면번호및 작업장 찾기
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <param name="ProcessCode"></param>
		/// <param name="Index"></param>
		/// <returns></returns>
		private ArrayList FindItem(string ItemNum, string ProcessCode, int Index)
		{
			//품목명, 도면번호 찾기
			string str = "Select ItemDrawNum, ItemName From II_MT Where ItemNum = @ItemNum and RecodingState = 1";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			//comm.Transaction = tr;
			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				arrlist.Add(dr["ItemDrawNum"].ToString());
				arrlist.Add(dr["ItemName"].ToString());
			}
			dr.Close();	
			comm.Parameters.Clear();
			
			//공정명 찾기
			str = "Select * From PUC_MT Where SmallClassificationCode = @ProcessCode and RecodingState= 1";
			comm.CommandText = str;
			comm.Parameters.Add("@ProcessCode",ProcessCode);
			SqlDataReader dr1 = comm.ExecuteReader();
			while(dr1.Read())
			{
				arrlist.Add(dr1["SmallClassificationName"].ToString());
			}
			dr1.Close();
			comm.Parameters.Clear();

			//제품찾기
			str = "Select * From PP_HT Where ProductionPlanHistoryIndex = @Index";
			comm.CommandText = str;
			comm.Parameters.Add("@Index",Index);
			SqlDataReader dr2 = comm.ExecuteReader();
			while(dr2.Read())
			{
				arrlist.Add(dr2["ItemNum"].ToString());
				arrlist.Add(dr2["ItemDrawNum"].ToString());
				arrlist.Add(dr2["ItemName"].ToString());
			}
			dr2.Close();
			comm.Parameters.Clear();


			//작업장명 찾기
			str = @"Select * From WSI_MT Where ItemNum = @ItemNum and ProcessCode = @ProcessCode and RecodingState = 1 and 
				PriorityOrder = (Select Min(PriorityOrder) From WSI_MT Where  ItemNum = @ItemNum and ProcessCode = @ProcessCode and RecodingState = 1)";
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum",ItemNum);
			comm.Parameters.Add("@ProcessCode",ProcessCode);
			DataSet dset = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(comm);
			da.Fill(dset);
			if(dset.Tables[0].Rows.Count == 0)
				arrlist.Add("");
			else
				arrlist.Add(dset.Tables[0].Rows[0]["WCName"].ToString());				
			
			//외주리드타임 찾기
			str = "Select * From PSI_MT Where ItemNum = @ItemNum and ProcessCode = @ProcessCode and RecodingState = 1";
			comm.CommandText = str;
			DataSet dataset = new DataSet();
			SqlDataAdapter da1 = new SqlDataAdapter(comm);
			da1.Fill(dataset);

			if(dataset.Tables[0].Rows.Count == 0)
				arrlist.Add(0);
			else
			{
				arrlist.Add(dataset.Tables[0].Rows[0]["LeadTime"].ToString());
			}
			comm.Parameters.Clear();

			return arrlist;

		}

		/// <summary>
		/// ⑩-2 출고 품목 찾기 함수
		/// </summary>
		/// <param name="ItemNum">품목번호</param>
		/// <param name="ProcessCode">공정코드</param>
		/// <returns></returns>
		private ArrayList SubItemSearch(string ItemNum, string ProcessCode)
		{
			List.Clear();

			if(ProcessCode == null)
				ProcessCode = "14000000";

			//품목을 던져주고
			string str = "SPSubItemBOM";
			SqlCommand comm = new SqlCommand(str,conn);
			//comm.Transaction = tr;
			comm.CommandType = CommandType.StoredProcedure;
			comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = ItemNum;
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
					WCSubItem wcsub = new WCSubItem();
					wcsub.ItemNum = dataset.Tables[0].Rows[count+1]["ItemNum"].ToString();
					wcsub.ProcessCode = dataset.Tables[0].Rows[count+1]["ProcessCode"].ToString();
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
							WCSubItem wcsub = new WCSubItem();
							wcsub.ItemNum = dataset.Tables[0].Rows[rowcount]["ItemNum"].ToString();
							wcsub.ProcessCode = "14000000";
							List.Add(wcsub);
							continue;
						}
						else
						{
							// ③-2 원자재가 아닌경우는 선택 품목의 공정의 작업구분이 자가인지 외주인지 판단한다
							// 만일 해당 공정이 자가인경우에는 해당 공정품이 출고품목이 된다. 
							// 또한 주의할 것은 해당 공정품 이후에 달려있는 하위품목들의 레코드는 삭제해주어야 한다
							// 삭제 대상은 해당 공정의 Lvl 값과 동일한 값을 가진 레코드를 만날때까지 삭제한다.
							if(Division(dataset.Tables[0].Rows[rowcount]["ItemNum"].ToString(),int.Parse(dataset.Tables[0].Rows[rowcount]["ProcessSequenceNum"].ToString())) == true)
							{
								//해당 공정이 자가이므로 해당 품목이 출고품목이 된다.
								WCSubItem wcsub = new WCSubItem();
								wcsub.ItemNum = dataset.Tables[0].Rows[rowcount]["ItemNum"].ToString();
								wcsub.ProcessCode = dataset.Tables[0].Rows[rowcount]["ProcessCode"].ToString();
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
								rowcount = a;
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
									WCSubItem wcsub = new WCSubItem();
									wcsub.ItemNum = dataset.Tables[0].Rows[rowcount]["ItemNum"].ToString();
									wcsub.ProcessCode = dataset.Tables[0].Rows[rowcount]["ProcessCode"].ToString();
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
									rowcount = a;
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
				RegisterClientScriptBlock("","<script>alert('품목에 해당공정이 존재하지 않습니다!')</script>");
				RegisterClientScriptBlock("","<script>location.href = WorkPlan.aspx;</script>");
				
				return null;
			}
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
			string str = "Select Min(ProcessSequenceNum) From PSI_MT Where ItemNum = @ItemNum and RecodingState = 1";
			SqlCommand comm = new SqlCommand(str,conn);
			//comm.Transaction = tr;
			comm.Parameters.Add("@ItemNum",ItemNum);
			int Process = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();

			return Process;
		}


		/// <summary>
		/// ③-2 출고품목 찾기 함수(SubItemSearch())에서 호출되는 자가구분여부를
		/// 체크하는 함수
		/// </summary>
		/// <param name="Item">품목명</param>
		/// <param name="Code">공정순서</param>
		/// <returns>자가인경우 true, 그외의 경우 false를 리턴한다.</returns>
		private bool Division(string Item, int Code)
		{
			string aa="";
			string str = "Select WorkDistinction From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessSequenceNum = @code";
			SqlCommand comm =  new SqlCommand(str,conn);
			//comm.Transaction = tr;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = Item;
			comm.Parameters.Add("@code",SqlDbType.TinyInt).Value = Code;
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = dr["WorkDistinction"].ToString();
			}
			dr.Close();

			if(aa.ToString().Trim() == "자가")
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

	}
}
