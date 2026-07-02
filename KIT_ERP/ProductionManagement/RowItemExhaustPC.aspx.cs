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

namespace KIT_ERP.ProductionManagement
{
	/// <summary>
	/// RowItemExhaustPC에 대한 요약 설명입니다.
	/// </summary>
	public class RowItemExhaustPC : System.Web.UI.Page
	{
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcStartDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter uwgExcel;
		protected System.Web.UI.WebControls.Button btnExcel;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.LinkButton linkUpdate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdRowIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdquantity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdOldMonth;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdOldYear;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgE_HT;
		protected KIT_ERP.ProductionManagement.ItemSearch.ItemSearch ItemSearch1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdOldCreateQuantity;
		protected System.Web.UI.WebControls.DropDownList ddlWorker;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;



		/// <summary>
		/// 작업자
		/// </summary>
		protected DataSet Worker
		{
			get
			{
				Update source = new Update(Session["ID"].ToString());
				DataSet ds_Worker = source.Worker();
				return ds_Worker;
			}
		}
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.RawMaterials =  true; //원자재 바인딩
			ItemSearchControl1.lbItemNum.Text = "소진 품번";

			ItemSearch1.ChildRawMaterials = true; //원자재 바인딩	
			ItemSearch1.ChildHalfFinishedProducts =  true; //반제품 바인딩	
			ItemSearch1.lbChildItemNum.Text = "생성 품번";
			
			if(!Page.IsPostBack)
			{
				btnCancel.Attributes.Add("onClick", "return OK('선택한 항목을 삭제');");


				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				
				string str = "Select ID, Name From UI_MT where RecodingState = 1 and BusinessRegistrationNum = '' and [ID] != 'admin'  order by Name";
				SqlCommand comm_Id = new SqlCommand(str,conn);
				SqlDataAdapter da_Id = new SqlDataAdapter(comm_Id) ;
				DataSet ds_Id = new DataSet() ;
				da_Id.Fill(ds_Id);
				
				ddlWorker.DataSource = ds_Id;
				ddlWorker.DataTextField = ds_Id.Tables[0].Columns[1].ToString();
				ddlWorker.DataValueField = ds_Id.Tables[0].Columns[0].ToString();
				ddlWorker.DataBind();
				ddlWorker.Items.Insert(0, "-선택-") ;
				ddlWorker.Items[0].Value = "";

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
			this.uwgE_HT.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.uwgE_HT_PageIndexChanged);
			this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
			this.linkUpdate.Click += new System.EventHandler(this.linkUpdate_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			uwgE_HT.DisplayLayout.Pager.CurrentPageIndex = 1;
			this.uwgE_HT.DataSource = Search();
			this.uwgE_HT.DataBind();

		}

		/// <summary>
		/// 검색함수
		/// </summary>
		/// <returns></returns>
		private DataSet Search()
		{
			KIT_ERP.Search search;
			search = new KIT_ERP.Search("RowItemExhaustPC",ItemSearchControl1.hdItem,ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,ItemSearch1.hdChildItem,ItemSearch1.ChildItemNum, ItemSearch1.ChildItemDrawNum, ItemSearch1.ChildItemName, wdcStartDate,wdcEndDate , ddlWorker.SelectedItem.Value);
			return search.DataSet_search();
		}

		/// <summary>
		/// 엑셀저장버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnExcel_Click(object sender, System.EventArgs e)
		{
			// 현재 Grid의 내용을 Excel로 Export
			Infragistics.WebUI.UltraWebGrid.UltraWebGrid  uwgExcelImport = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
			uwgExcelImport = uwgE_HT;
			uwgExcelImport.DisplayLayout.Pager.AllowPaging = false;
			uwgExcelImport.Columns.FromKey("chk").Hidden = true;


			this.uwgE_HT.DataSource = Search();
			this.uwgE_HT.DataBind();

			uwgExcel.Export(uwgE_HT);	
		}

		/// <summary>
		/// 삭제버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			KIT_ERP.Delete cancel = new Delete(uwgE_HT,"RowItemExhaustPC");
			cancel.MainRowDelete();

			//삭제버튼을 눌러 데이터를 삭제한 후에 다시 검색 객체를 호출해서 그리드에 바인딩을 다시 시킴.
			this.uwgE_HT.DataSource = Search();
			this.uwgE_HT.DataBind();

		}

        /// <summary>
        /// 원자재 소진원장 삭제
        /// </summary>
        /// <param name="count"></param>
        /// <param name="con"></param>
        /// <param name="trans"></param>
		private void E_HT_Delete(int count,SqlConnection con,SqlTransaction trans)
		{
			//기존 품질검사원장에서 납품수량을 Update
			
			string str =" Delete From E_HT WHERE ExhaustHistoryIndex = @INDEX";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;		
			comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = int.Parse(uwgE_HT.Rows[count].Cells.FromKey("ExhaustHistoryIndex").Value.ToString());
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}



		/// <summary>
		/// 수정버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void linkUpdate_Click(object sender, System.EventArgs e)
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();
			try
			{
				E_HT_Update(rowcount,conn,tr);//원자재 소진원장 수정하는 함수 호출
				RMS_MT_Update(rowcount,conn,tr);//원자재창고 수정하는 함수 호출	
				SH_HTUpdate(rowcount,conn,tr);	//창고수불이력원장 수정
				tr.Commit();
				Response.Write("<script language=javascript>");
				Response.Write("alert('수정되었습니다!');");
				Response.Write("</script>");
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

				this.uwgE_HT.DataSource = Search();
				this.uwgE_HT.DataBind();
			}

		}

		/// <summary>
		/// 원자재 소진원장 수량및 일자 수정
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		private void E_HT_Update(int count,SqlConnection con,SqlTransaction trans)
		{
			//기존 품질검사원장에서 납품수량을 Update
			
			string str =@" UPDATE E_HT SET ExhaustQuantity =@ExhaustQuantity, ExhaustDate = @ExhaustDate, CreateQuantity = @CreateQuantity,
						[Use] = @Use, WorkerName = @WorkerName , WorkerID = @WorkerID, StartTime = @StartTime, EndTime = @EndTime
				WHERE ExhaustHistoryIndex = @INDEX";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;		
			comm.Parameters.Add("@ExhaustQuantity", SqlDbType.Decimal).Value = decimal.Parse(uwgE_HT.Rows[count].Cells.FromKey("ExhaustQuantity").Value.ToString());
			comm.Parameters.Add("@CreateQuantity", SqlDbType.Decimal).Value = decimal.Parse(uwgE_HT.Rows[count].Cells.FromKey("CreateQuantity").Value.ToString());
			comm.Parameters.Add("@ExhaustDate", DateTime.Parse(uwgE_HT.Rows[count].Cells.FromKey("ExhaustDate").Text).ToShortDateString());
			comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = int.Parse(uwgE_HT.Rows[count].Cells.FromKey("ExhaustHistoryIndex").Value.ToString());
			comm.Parameters.Add("@Use",uwgE_HT.Rows[count].Cells.FromKey("Use").Value.ToString());
			comm.Parameters.Add("@WorkerID",uwgE_HT.Rows[count].Cells.FromKey("WorkerID").Value.ToString());
			comm.Parameters.Add("@WorkerName",uwgE_HT.Rows[count].Cells.FromKey("WorkerName").Value.ToString());
			comm.Parameters.Add("@StartTime",uwgE_HT.Rows[count].Cells.FromKey("StartTime").Value.ToString());
			comm.Parameters.Add("@EndTime",uwgE_HT.Rows[count].Cells.FromKey("EndTime").Value.ToString());
			comm.Parameters.Add("@person",Session["UserName"].ToString());
			comm.Parameters.Add("@id",Session["ID"].ToString());		

			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}

		private void SH_HTUpdate(int count,SqlConnection con,SqlTransaction trans)
		{
			string str = "Update SH_HT Set Quantity=@Quantity,[Date] = @date  where HistoryDivision='공통소진' and  Division = '0' and  HistoryIndex=@index ";
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			comm.CommandText = str;
			
			comm.Parameters.Add("@date", DateTime.Parse(uwgE_HT.Rows[count].Cells.FromKey("ExhaustDate").Value.ToString()).ToShortDateString());
			comm.Parameters.Add("@Quantity",decimal.Parse(uwgE_HT.Rows[count].Cells.FromKey("ExhaustQuantity").Value.ToString()));//합격수량
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(uwgE_HT.Rows[count].Cells.FromKey("ExhaustHistoryIndex").Value.ToString());//기타입출고원장인덱스
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();


			str = "Update SH_HT Set Quantity=@Quantity,[Date] = @date  where HistoryDivision='공통소진' and  Division = '1' and  HistoryIndex=@index ";
			comm.CommandText = str;
			
			comm.Parameters.Add("@date", DateTime.Parse(uwgE_HT.Rows[count].Cells.FromKey("ExhaustDate").Value.ToString()).ToShortDateString());
			comm.Parameters.Add("@Quantity",decimal.Parse(uwgE_HT.Rows[count].Cells.FromKey("CreateQuantity").Value.ToString()));//합격수량
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(uwgE_HT.Rows[count].Cells.FromKey("ExhaustHistoryIndex").Value.ToString());//기타입출고원장인덱스
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}


		/// <summary>
		/// 원자재 수량 수정
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <summary>
		/// 원자재 소진시 원자재창고수정
		/// </summary>
		private void RMS_MT_Update(int count, SqlConnection con, SqlTransaction trans)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;

			Table table1 = new Table(int.Parse(hdOldYear.Value),int.Parse(hdOldMonth.Value));
			comm.Parameters.Add("@num",uwgE_HT.Rows[count].Cells.FromKey("ExhaustItemNum").Text);
			comm.Parameters.Add("@quantity",-decimal.Parse(hdquantity.Value));
			comm.Parameters.Add("@cost",TotalCost(-decimal.Parse(hdquantity.Value),uwgE_HT.Rows[count].Cells.FromKey("ExhaustItemNum").Text));
			comm.Parameters.Add("@year",int.Parse(hdOldYear.Value));
			comm.CommandText = table1.OutRowTable();//원자재 창고 소진수량 마이너스감소
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			Table table = new Table(DateTime.Parse(uwgE_HT.Rows[count].Cells.FromKey("ExhaustDate").Text).Year,DateTime.Parse(uwgE_HT.Rows[count].Cells.FromKey("ExhaustDate").Text).Month);
			comm.Parameters.Add("@num",uwgE_HT.Rows[count].Cells.FromKey("ExhaustItemNum").Text);
			comm.Parameters.Add("@quantity",decimal.Parse(uwgE_HT.Rows[count].Cells.FromKey("ExhaustQuantity").Text));
			comm.Parameters.Add("@cost",TotalCost(decimal.Parse(uwgE_HT.Rows[count].Cells.FromKey("ExhaustQuantity").Text),uwgE_HT.Rows[count].Cells.FromKey("ExhaustItemNum").Text));
			comm.Parameters.Add("@year",DateTime.Parse(uwgE_HT.Rows[count].Cells.FromKey("ExhaustDate").Text).Year);
			comm.CommandText = table.OutRowTable();//원자재 창고 소진수량 감소
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();


			Table table2 = new Table(int.Parse(hdOldYear.Value),int.Parse(hdOldMonth.Value));
			comm.Parameters.Add("@num",uwgE_HT.Rows[count].Cells.FromKey("CreateItemNum").Text);
			comm.Parameters.Add("@quantity",-decimal.Parse(hdOldCreateQuantity.Value));
			comm.Parameters.Add("@cost",TotalCost(-decimal.Parse(hdOldCreateQuantity.Value),uwgE_HT.Rows[count].Cells.FromKey("CreateItemNum").Text));
			comm.Parameters.Add("@year",int.Parse(hdOldYear.Value));
			comm.CommandText = table2.InRowTable();//원자재 창고 생성수량 마이너스 증가
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			Table table3 = new Table(DateTime.Parse(uwgE_HT.Rows[count].Cells.FromKey("ExhaustDate").Text).Year,DateTime.Parse(uwgE_HT.Rows[count].Cells.FromKey("ExhaustDate").Text).Month);
			comm.Parameters.Add("@num",uwgE_HT.Rows[count].Cells.FromKey("CreateItemNum").Text);
			comm.Parameters.Add("@quantity",decimal.Parse(uwgE_HT.Rows[count].Cells.FromKey("CreateQuantity").Text));
			comm.Parameters.Add("@cost",TotalCost(decimal.Parse(uwgE_HT.Rows[count].Cells.FromKey("CreateQuantity").Text),uwgE_HT.Rows[count].Cells.FromKey("CreateItemNum").Text));
			comm.Parameters.Add("@year",DateTime.Parse(uwgE_HT.Rows[count].Cells.FromKey("ExhaustDate").Text).Year);
			comm.CommandText = table3.InRowTable();//원자재 창고 생성수량 증가
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14000000' and [Year] = @year";
				comm.Parameters.Add("@ItemNum",uwgE_HT.Rows[count].Cells.FromKey("ExhaustItemNum").Text);
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName = uwgE_HT.Rows[count].Cells.FromKey("ExhaustItemNum").Text;
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 출고가 불가능 합니다.");
				}
			}
		}


		/// <summary>
		/// 원자재 창고 금액
		/// </summary>
		/// <param name="Quantity"></param>
		/// <param name="ItemNum"></param>
		/// <returns></returns>
		private decimal TotalCost(decimal Quantity, string ItemNum)
		{
			decimal aa = 0;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select StandardUnitCost From II_MT where ItemNum = @ItemNum and RecodingState = 1";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = decimal.Parse(dr["StandardUnitCost"].ToString());
			}
			dr.Close();
			conn.Close();

			return (aa * Quantity);
		}


		/// <summary>
		/// 마이너스 재고 허용여부
		/// </summary>
		/// <returns></returns>
		private bool MinusStore()
		{
			bool aa = true;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
			conn.Open();
			string str = "Select MinusRowMaterialPermissionUsed From SCS_T";
			SqlCommand comm =  new SqlCommand(str,conn);
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = bool.Parse(dr["MinusRowMaterialPermissionUsed"].ToString());
			}
			dr.Close();
			conn.Close();

			return aa;
		}


		/// <summary>
		/// 페이지 넘김
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void uwgE_HT_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			uwgE_HT.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			this.uwgE_HT.DataSource = Search();
			this.uwgE_HT.DataBind();
			
		}
	}
}
