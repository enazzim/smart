using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace KIT_ERP.BuyingOutside
{
	/// <summary>
	/// OutSideOutStorehousePC에 대한 요약 설명입니다.
	/// </summary>
	public class OutSideOutStorehousePC : System.Web.UI.Page
	{
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcStartDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Button btnDelete;
		protected System.Web.UI.WebControls.Button btnStop;
		protected System.Web.UI.WebControls.Button btnExcel;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgOOS_HT;
		protected System.Web.UI.WebControls.LinkButton linkUpdate;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter uwgExcel;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdRowIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdquantity;
		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdYear;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdMonth;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected System.Web.UI.WebControls.Button Button1;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}


			ItemSearchControl1.RawMaterials = true; //원자재 바인딩
			ItemSearchControl1.HalfFinishedProducts = true; //반제품 바인딩
			ItemSearchControl1.Products = true; //제품 바인딩
			CSC1.UnitCostDistinction = "외주거래처";
				

			if(!Page.IsPostBack)
			{

				// 중단버튼에 대하여 onclick속성을 추가 시킴
				btnStop.Attributes.Add("onClick","return Confirm('선택한 품목들의 대하여 출고를 중단 하시겠습니까?');");
				// 삭제버튼에 대하여 onclick속성을 추가 시킴
				btnDelete.Attributes.Add("onClick","return Confirm('선택한 품목들의 대하여 출고를 삭제 하시겠습니까?');");

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
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.uwgOOS_HT.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.uwgOOS_HT_PageIndexChanged);
			this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.linkUpdate.Click += new System.EventHandler(this.linkUpdate_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private DataSet Search()
		{
			KIT_ERP.Search search;
			if(ItemSearchControl1.hdItem.Trim() == "")
				search = new KIT_ERP.Search("OutSideOutStorehousePC",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,wdcStartDate,wdcEndDate,CSC1.Company,CSC1.BusinessRegistrationNum,"대기",ddlItemClassification1.SelectedItem.Value);
			else
				search = new KIT_ERP.Search("OutSideOutStorehousePC",ItemSearchControl1.ItemNum, "", "",wdcStartDate,wdcEndDate,CSC1.Company,CSC1.BusinessRegistrationNum,"대기",ddlItemClassification1.SelectedItem.Value);
			return search.DataSet_search();
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			uwgOOS_HT.DisplayLayout.Pager.CurrentPageIndex = 1;
			this.uwgOOS_HT.DataSource = Search();
			this.uwgOOS_HT.DataBind();			
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			KIT_ERP.Delete delete = new Delete(uwgOOS_HT,"OutSideOutStorehousePC");
			delete.MainRowDelete();
			
			//삭제버튼을 눌러 데이터를 삭제한 후에 다시 검색 객체를 호출해서 그리드에 바인딩을 다시 시킴.
			this.uwgOOS_HT.DataSource = Search();
			this.uwgOOS_HT.DataBind();	
		}

		private void btnStop_Click(object sender, System.EventArgs e)
		{
			KIT_ERP.Stop stop = new Stop(uwgOOS_HT,"OutSideOutStorehousePC","정지");
			stop.MainTableStop();

			//중단버튼을 눌러 데이터를 중단한 후에 다시 검색 객체를 호출해서 그리드에 바인딩을 다시 시킴.
			this.uwgOOS_HT.DataSource = Search();
			this.uwgOOS_HT.DataBind();	
		}

		private void btnExcel_Click(object sender, System.EventArgs e)
		{
			// 현재 Grid의 내용을 Excel로 Export
			Infragistics.WebUI.UltraWebGrid.UltraWebGrid  uwgExcelImport = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
			uwgExcelImport = uwgOOS_HT;
			uwgExcelImport.DisplayLayout.Pager.AllowPaging = false;
			uwgExcelImport.Columns.FromKey("chk").Hidden = true;

			this.uwgOOS_HT.DataSource = Search();
			uwgExcelImport.DataBind();			

			uwgExcel.Export(uwgOOS_HT);	
		}

		private void linkUpdate_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlCommand comm = new SqlCommand();
			SqlTransaction tr = conn.BeginTransaction();
			comm.Connection = conn;
			comm.Transaction = tr;

			try
			{			

				OOS_HT_Update(); //외주출고원장 수량 수정
				OS_MT_Update(); //외주창고 수량 수정

				//수정하고자 하는 품목이 반제품 또는 제품인경우 생산창고 수량 수정
				if(Division() == 2 || Division() == 3)
				{	
					if(!WorkPlan3() || (StoreManage(conn,tr) =="예"))
					{
						PS_MT_Update();
						PS_MTHistory();
					}
					
				}
					//수정하고자 하는 품목이 원자재인 경우 원자재창고 수량 수정
				else if(Division() ==1)
				{
					if(!WorkPlan3() || (StoreManage(conn,tr) =="예"))
						RMS_MT_Update();
					RMS_MTHistory();
				}
				
				tr.Commit();
				Response.Write("<script language=javascript>");
				Response.Write("alert('수정되었습니다!');");
				Response.Write("</script>");

				this.uwgOOS_HT.DataSource = Search();
				this.uwgOOS_HT.DataBind();			

			}
			catch(Exception ee)
			{
				
				Response.Write("<script language=javascript>");
				Response.Write("alert('"+ee.Message+"');");
				Response.Write("</script>");

				tr.Rollback();
			}
			finally
			{
				conn.Close();
			}
		}



		/// <summary>
		/// 재고관리여부
		/// </summary>
		/// <returns></returns>
		private string StoreManage(SqlConnection con, SqlTransaction trans)
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			string store = "아니오";
			string str = "Select Unit4 From II_MT Where ItemNum = @ItemNum and RecodingState = 1";
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			comm.CommandText = str;
			comm.CommandType = CommandType.Text;

			comm.Parameters.Add("@ItemNum",uwgOOS_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString());
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				store = dr["Unit4"].ToString();				
			}
			dr.Close();
			return store;
		}

		/// <summary>
		/// 작업계획전략3인지 판단함수
		/// </summary>
		/// <returns></returns>
		private bool WorkPlan3()
		{
			bool Work = false;
			// 현재 설정되어있는 각종 정보를 가져옴
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
			conn.Open();

			string strSQL = @"SELECT WorkPlan3 FROM SCS_T";
			SqlCommand comm = new SqlCommand(strSQL, conn);

			SqlDataReader reader = comm.ExecuteReader();
			if(reader.Read())
			{
				Work = bool.Parse(reader["WorkPlan3"].ToString());
			}
			conn.Close();

			return Work;

		}
		


		//수정하고자 하는 품목이 상품인지 원자재인지 판단하는 함수
		private int Division()
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			string aa="";

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select PropertyClassification From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value =uwgOOS_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = dr["PropertyClassification"].ToString();
			}
			conn.Close();

			if(aa.ToString() == "원자재")
				return 1;
			else if(aa.ToString() =="반제품")
				return 2;
			else if(aa.ToString() =="제품")
				return 3;
			else if(aa.ToString() =="상품")
				return 4;
			else 
				return 5;
		}

		//외주출고원장의 수량을 수정하는 함수
		private void OOS_HT_Update()
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
		
			string itemnum= uwgOOS_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			conn.Open();	
			
			string str = "select StandardUnitCost from II_MT where RecodingState = 1 and ItemNum= @itemnum";
			SqlCommand comm = new SqlCommand(str,conn);			
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = itemnum;			
			
		
			//품목정보테이블에서 현제 수정하고자하는 품목의 단가를 가지고있는 변수
			decimal unitcost = Decimal.Parse(comm.ExecuteScalar().ToString());
						
			
			string str1 = "Update OOS_HT set ThistimeOutStorehouseQuantity = @quantity,OutStorehouseDate = @outdate , UpdatingPerson=@Person,UpdatingPersonID=@PersonID,UpdatingDate=@Date Where OutSideOutStorehouseHistoryIndex = @Index";
			SqlCommand comm1 = new SqlCommand(str1,conn);

			//새로변경되는 수정한 수량을 증가
			comm1.Connection = conn;
			comm1.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(uwgOOS_HT.Rows[rowcount].Cells.FromKey("ThistimeOutStorehouseQuantity").Value.ToString());
			comm1.Parameters.Add("@outdate", SqlDbType.SmallDateTime).Value = DateTime.Parse(uwgOOS_HT.Rows[rowcount].Cells.FromKey("OutStorehouseDate").Value.ToString());
			comm1.Parameters.Add("@Person", SqlDbType.VarChar).Value = Session["UserName"].ToString();
			comm1.Parameters.Add("@PersonID", SqlDbType.VarChar).Value = Session["ID"].ToString();
			comm1.Parameters.Add("@Date", SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm1.Parameters.Add("@Index", SqlDbType.Int).Value = int.Parse(uwgOOS_HT.Rows[rowcount].Cells.FromKey("OutSideOutStorehouseHistoryIndex").Value.ToString());
			comm1.ExecuteNonQuery();

			conn.Close();	
			
		}

		
		
		//생산창고의 수량을 수정하는 함수
		private void PS_MT_Update()
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			Decimal m_progressrate = 100;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string itemnum= uwgOOS_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			conn.Open();	
			
			// 품목의 진척율
			string str_rate = "Select ProgressRate From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
			SqlCommand comm_rate = new SqlCommand(str_rate,conn);
			comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = uwgOOS_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			comm_rate.Parameters.Add("@code",SqlDbType.VarChar).Value = uwgOOS_HT.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
			SqlDataReader dr_rate = comm_rate.ExecuteReader();

			if(dr_rate.Read())
			{
				if(dr_rate["ProgressRate"].ToString() == null || dr_rate["ProgressRate"].ToString().Trim() =="")
					m_progressrate = 0;
				else
					m_progressrate = Decimal.Parse(dr_rate["ProgressRate"].ToString());
			}		
			dr_rate.Close();
		

			string str = "select StandardUnitCost from II_MT where RecodingState = 1 and ItemNum= @itemnum";
			SqlCommand comm = new SqlCommand(str,conn);			
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = itemnum;			
			//품목정보테이블에서 현제 수정하고자하는 품목의 단가를 가지고있는 변수
			decimal unitcost = Decimal.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();


		
			//기존테이블에서 이전수량을 마이너스 증가
			int year = int.Parse(hdYear.Value);
			int mon = int.Parse(hdMonth.Value);
			comm.Parameters.Add("@cost", SqlDbType.VarChar).Value = (-(decimal.Parse(hdquantity.Value.ToString()))*(decimal.Parse(unitcost.ToString()))* (decimal.Parse(m_progressrate.ToString())/100));
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(hdquantity.Value.ToString()));
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = uwgOOS_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@code", SqlDbType.VarChar).Value = uwgOOS_HT.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
			comm.Parameters.Add("@year",year);

			Table table = new Table(year,mon);
			comm.CommandText = table.OutProductionTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//새로변경되는 수정한 수량을 증가
			year = DateTime.Parse(uwgOOS_HT.Rows[rowcount].Cells.FromKey("OutStorehouseDate").Text).Year ;
			mon = DateTime.Parse(uwgOOS_HT.Rows[rowcount].Cells.FromKey("OutStorehouseDate").Text).Month ;
			comm.Parameters.Add("@cost", SqlDbType.VarChar).Value = decimal.Parse(uwgOOS_HT.Rows[rowcount].Cells.FromKey("ThistimeOutStorehouseQuantity").Value.ToString())*decimal.Parse(unitcost.ToString())*(decimal.Parse(m_progressrate.ToString())/100);
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(uwgOOS_HT.Rows[rowcount].Cells.FromKey("ThistimeOutStorehouseQuantity").Value.ToString());
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = uwgOOS_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@code", SqlDbType.VarChar).Value = uwgOOS_HT.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
			comm.Parameters.Add("@year",year);
			Table table1 = new Table(year,mon);

			comm.CommandText = table1.OutProductionTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			conn.Close();
		}

		//외주창고의 수량을 수정하는 함수
		private void OS_MT_Update()
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			Decimal m_progressrate = 100;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string itemnum= uwgOOS_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			conn.Open();	

			// 품목의 진척율
			string str_rate = "Select ProgressRate From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
			SqlCommand comm_rate = new SqlCommand(str_rate,conn);
			comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = uwgOOS_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			comm_rate.Parameters.Add("@code",SqlDbType.VarChar).Value = uwgOOS_HT.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
			SqlDataReader dr_rate = comm_rate.ExecuteReader();

			if(dr_rate.Read())
			{
				if(dr_rate["ProgressRate"].ToString() == null || dr_rate["ProgressRate"].ToString().Trim() =="")
					m_progressrate = 0;
				else
					m_progressrate = Decimal.Parse(dr_rate["ProgressRate"].ToString());
			}		
			dr_rate.Close();
			

			string str = "select StandardUnitCost from II_MT where RecodingState = 1 and ItemNum= @itemnum";
			SqlCommand comm = new SqlCommand(str,conn);
			
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = itemnum;
			
			//품목정보테이블에서 현재 수정하고자하는 품목의 단가를 가지고있는 변수
			decimal unitcost = Decimal.Parse(comm.ExecuteScalar().ToString());
			
			comm.Parameters.Clear();
		
			//기존테이블에서 이전수량을 마이너스 증가
			int year = int.Parse(hdYear.Value);
			int mon = int.Parse(hdMonth.Value);
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = (-(decimal.Parse(hdquantity.Value.ToString()))*(decimal.Parse(unitcost.ToString()))* (decimal.Parse(m_progressrate.ToString())/100));
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(hdquantity.Value.ToString()));
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = uwgOOS_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@code", SqlDbType.VarChar).Value = uwgOOS_HT.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
			comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = uwgOOS_HT.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
			comm.Parameters.Add("@year",year);
			
			Table table = new Table(year,mon);
			comm.CommandText = table.InTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//새로변경되는 수정한 수량을 증가
			year = DateTime.Parse(uwgOOS_HT.Rows[rowcount].Cells.FromKey("OutStorehouseDate").Text).Year ;
			mon = DateTime.Parse(uwgOOS_HT.Rows[rowcount].Cells.FromKey("OutStorehouseDate").Text).Month ;
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(uwgOOS_HT.Rows[rowcount].Cells.FromKey("ThistimeOutStorehouseQuantity").Value.ToString())*decimal.Parse(unitcost.ToString())*(decimal.Parse(m_progressrate.ToString())/100);
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(uwgOOS_HT.Rows[rowcount].Cells.FromKey("ThistimeOutStorehouseQuantity").Value.ToString());
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = uwgOOS_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@code", SqlDbType.VarChar).Value = uwgOOS_HT.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
			comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = uwgOOS_HT.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
			comm.Parameters.Add("@year",year);

			Table table1 = new Table(year,mon);
			comm.CommandText = table1.InTable();
			comm.ExecuteNonQuery();

			conn.Close();
		}


		/// <summary>
		/// 공정품 출고량 수정시 수정되는 이력원장
		/// </summary>
		private void PS_MTHistory()
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str=" UPDATE SH_HT SET Quantity = @Quantity WHERE HistoryDivision= '외주출고' and HistoryIndex = @index and ItemNum = @itemnum";
			SqlCommand comm = new SqlCommand(str,conn);	
			comm.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = decimal.Parse(uwgOOS_HT.Rows[rowcount].Cells.FromKey("ThistimeOutStorehouseQuantity").Value.ToString());
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(uwgOOS_HT.Rows[rowcount].Cells.FromKey("OutSideOutStorehouseHistoryIndex").Value.ToString());
			comm.Parameters.Add("@itemnum",uwgOOS_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString());
			conn.Open();	
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
			conn.Close();
		}



		/// <summary>
		/// 원자재 출고량 수정시 수정되는 이력원장
		/// </summary>
		private void RMS_MTHistory()
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str=" UPDATE SH_HT SET Quantity = @Quantity WHERE HistoryDivision= '외주출고' and HistoryIndex = @index and ItemNum = @itemnum";
			SqlCommand comm = new SqlCommand(str,conn);	
			comm.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = decimal.Parse(uwgOOS_HT.Rows[rowcount].Cells.FromKey("ThistimeOutStorehouseQuantity").Value.ToString());
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(uwgOOS_HT.Rows[rowcount].Cells.FromKey("OutSideOutStorehouseHistoryIndex").Value.ToString());
			comm.Parameters.Add("@itemnum",uwgOOS_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString());
			conn.Open();	
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
			conn.Close();
		}

		//원자재창고 수량 수정하는 함수
		private void RMS_MT_Update()
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string itemnum= uwgOOS_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			conn.Open();	
			
			string str = "select StandardUnitCost from II_MT where RecodingState = 1 and ItemNum= @itemnum";
			SqlCommand comm = new SqlCommand(str,conn);

			
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = itemnum;
			
			//품목정보테이블에서 현제 수정하고자하는 품목의 단가를 가지고있는 변수
			decimal unitcost = decimal.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();
		
			//기존테이블에서 이전수량을 마이너스 증가
			int year = int.Parse(hdYear.Value);
			int mon = int.Parse(hdMonth.Value);			
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(hdquantity.Value.ToString())*unitcost);
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(hdquantity.Value.ToString()));
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = uwgOOS_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@year",year);

			Table table = new Table(year,mon);
			comm.CommandText = table.OutRowTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//새로변경되는 수정한 수량을 증가
			year = DateTime.Parse(uwgOOS_HT.Rows[rowcount].Cells.FromKey("OutStorehouseDate").Text).Year ;
			mon = DateTime.Parse(uwgOOS_HT.Rows[rowcount].Cells.FromKey("OutStorehouseDate").Text).Month ;
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(uwgOOS_HT.Rows[rowcount].Cells.FromKey("ThistimeOutStorehouseQuantity").Value.ToString())*unitcost;
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(uwgOOS_HT.Rows[rowcount].Cells.FromKey("ThistimeOutStorehouseQuantity").Value.ToString());
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = uwgOOS_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@year",year);

			Table table1 = new Table(year,mon);
			comm.CommandText = table1.OutRowTable();
			comm.ExecuteNonQuery();

			conn.Close();
		}

		private void uwgOOS_HT_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			uwgOOS_HT.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			this.uwgOOS_HT.DataSource = Search();
			this.uwgOOS_HT.DataBind();	
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			Infragistics.WebUI.UltraWebGrid.UltraWebGrid UWG = uwgOOS_HT;
			bool check = true;
			for(int count = uwgOOS_HT.Rows.Count-1 ; count >=0 ; count--)
			{
				if(UWG.Rows[count].Cells.FromKey("chk").Value == null || UWG.Rows[count].Cells.FromKey("chk").Value.ToString() == "false")
					UWG.Rows[count].Delete();
			}

			if(UWG.Rows.Count == 0)
			{
				check = false;
				RegisterStartupScript("","<script>alert('선택한 항목이 없습니다!');</script>");
			}
			else if(UWG.Rows.Count >= 15)
			{
				check = false;
				RegisterStartupScript("","<script>alert('선택한 항목이 너무 많습니다!');</script>");
			}
			else
			{
				for(int i = 0; i < UWG.Rows.Count; i++)
				{
					// 체크되지 않으면 Value값이 null이다 그러므로 확인후 false값을 넣어준다.
					if(UWG.Rows[0].Cells.FromKey("BusinessRegistrationNum").Text != UWG.Rows[i].Cells.FromKey("BusinessRegistrationNum").Text)
					{
						check = false;
						RegisterStartupScript("","<script>alert('발행할 거래처가 2곳 이상입니다!');</script>");
					}
				}
			}


			if(check)
			{
					
				Session["Grid"] = UWG;
				RegisterStartupScript("","<script>window.open('./Popup/OutSideStorehousePurchase.aspx','','width=800,scrollbars=yes,menubar=yes,status=yes,toolbar=yes,center=yes');</script>");
				//RegisterStartupScript("","<script>window.open('./Popup/OutSideOutStorehousePurchase.aspx','','width=1100,scrollbars=yes,menubar=yes,status=yes,toolbar=yes,center=yes');</script>");

				
			}
			else
			{
				uwgOOS_HT.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
				uwgOOS_HT.DisplayLayout.Pager.CurrentPageIndex = 1;
				
				this.uwgOOS_HT.DataSource = Search();
				this.uwgOOS_HT.DataBind();
			}
		}

	}
}
