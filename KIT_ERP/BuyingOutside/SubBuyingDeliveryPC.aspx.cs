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
using System.Data.SqlClient;
using System.Configuration;


namespace KIT_ERP.BuyingOutside
{
	/// <summary>
	/// SubBuyingDeliveryPC에 대한 요약 설명입니다.
	/// </summary>
	public class SubBuyingDeliveryPC : System.Web.UI.Page
	{
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcStartDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgSBD_HT;
		protected System.Web.UI.WebControls.Button btnExcel;
		protected System.Web.UI.WebControls.LinkButton linkUpdate;
		protected System.Web.UI.WebControls.Literal Literal1;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter uwgExcel;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdmon;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdyear;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdOldCost;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdReason;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdOldYear;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdOldMonth;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdquantity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdRowIndex;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
		protected System.Web.UI.WebControls.DropDownList ddlProperty;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			Literal1.Text = "";
			ItemSearchControl1.SubMaterials = true; //부자재 바인딩
			ItemSearchControl1.Expendable = true;// 소모품
			CSC1.UnitCostDistinction = "구매거래처";

			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			if(!Page.IsPostBack)
			{

				//취소버튼에 대하여 onclick속성을 추가 시킴
				btnCancel.Attributes.Add("onClick","return Confirm('선택한 품목들에 대하여 입고를 취소하시겠습니까?');");

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
			this.uwgSBD_HT.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.uwgSBD_HT_PageIndexChanged);
			this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
			this.linkUpdate.Click += new System.EventHandler(this.linkUpdate_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			uwgSBD_HT.DisplayLayout.Pager.CurrentPageIndex = 1;
			this.uwgSBD_HT.DataSource = Search();

			this.uwgSBD_HT.DataBind();

			DataSet ds = new DataSet();
			ds = Search();

			decimal TotalCost = 0;
			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				TotalCost += decimal.Parse(dr["TotalCost"].ToString());
			}

			Literal1.Text += "총 금액 : <b>\"";
			Literal1.Text += String.Format("{0:#,###}",TotalCost);
			Literal1.Text += "\"</b> 원" ;
		}

		private DataSet Search()
		{
			KIT_ERP.Search search;
			if(ItemSearchControl1.hdItem.Trim() =="")
				search = new KIT_ERP.Search("SubBuyingDeliveryPC",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,wdcStartDate, ddlItemClassification1.SelectedItem.Value, wdcEndDate, CSC1.Company, CSC1.BusinessRegistrationNum, ddlProperty);
			else
				search = new KIT_ERP.Search("SubBuyingDeliveryPC",ItemSearchControl1.ItemNum, "", "",wdcStartDate, ddlItemClassification1.SelectedItem.Value, wdcEndDate, CSC1.Company, CSC1.BusinessRegistrationNum,ddlProperty);

			return search.DataSet_search();
		}

		private void btnExcel_Click(object sender, System.EventArgs e)
		{
			// 현재 Grid의 내용을 Excel로 Export
			Infragistics.WebUI.UltraWebGrid.UltraWebGrid  uwgExcelImport = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
			uwgExcelImport = uwgSBD_HT;
			uwgExcelImport.DisplayLayout.Pager.AllowPaging = false;
			uwgExcelImport.Columns.FromKey("chk").Hidden = true;


			this.uwgSBD_HT.DataSource = Search();
			this.uwgSBD_HT.DataBind();

			uwgExcel.Export(uwgSBD_HT);	
	
			DataSet ds = new DataSet();
			ds = Search();

			decimal TotalCost = 0;
			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				TotalCost += decimal.Parse(dr["TotalCost"].ToString());
			}

			Literal1.Text = "총 금액<b>\" " + TotalCost + " \"</b> 원" ;
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			KIT_ERP.Delete cancel = new Delete(uwgSBD_HT,"SubBuyingDeliveryPC",Session["UserName"].ToString(),Session["ID"].ToString());
			cancel.MainRowDelete();

			//삭제버튼을 눌러 데이터를 삭제한 후에 다시 검색 객체를 호출해서 그리드에 바인딩을 다시 시킴.
			this.uwgSBD_HT.DataSource = Search();
			this.uwgSBD_HT.DataBind();



			DataSet ds = new DataSet();
			ds = Search();

			decimal TotalCost = 0;
			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				TotalCost += decimal.Parse(dr["TotalCost"].ToString());
			}

			//Literal1.Text = "총 금액<b>\" " + TotalCost + " \"</b> 원" ;
			Literal1.Text = "총 금액<b>\" " + TotalCost.ToString().Substring(0, TotalCost.ToString().IndexOf(".") + 2) + " \"</b> 원" ;
			
		}

		private void linkUpdate_Click(object sender, System.EventArgs e)
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();
			
			try
			{
				if(MonthClosing())
				{
				
					SBD_HT_Update(conn,tr);//부자재입고원장 수정하는 함수 호출
					SBO_HT_Update(conn,tr);//부자재발주원장 수정하는 함수 호출
					SBSI_MT_Update(conn,tr);//매입매출테이블 수정하는 함수 호출
					BOS_HTComUpdate(conn,tr);//부자재구매이력원장 수량 및 단가 일자 수정시 함수 호출

					tr.Commit();
					Response.Write("<script language=javascript>");
					Response.Write("alert('수정되었습니다!');");
					Response.Write("</script>");				
				}
				else
				{
					throw new Exception("월마감이 되어 수정이 불가능 합니다!");
				}
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

				this.uwgSBD_HT.DataSource = Search();
				this.uwgSBD_HT.DataBind();

				DataSet ds = new DataSet();
				ds = Search();

				decimal TotalCost = 0;
				foreach(DataRow dr in ds.Tables[0].Rows)
				{
					TotalCost += decimal.Parse(dr["TotalCost"].ToString());
				}

				
				//Literal1.Text = "총 금액<b>\" " + TotalCost + " \"</b> 원" ;
				Literal1.Text = "총 금액<b>\" " + TotalCost.ToString().Substring(0, TotalCost.ToString().IndexOf(".") + 2) + " \"</b> 원" ;
				
			}
		}

		private void uwgSBD_HT_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			uwgSBD_HT.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			this.uwgSBD_HT.DataSource = Search();
			uwgSBD_HT.DataBind();
		}


		/// <summary>
		/// 부자재구매입고원장 수장
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		private void SBD_HT_Update(SqlConnection con,SqlTransaction trans)
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			
			//기존 품질검사원장에서 납품수량을 증가
			string str1 =" UPDATE SBD_HT SET [Year] = @year, [Month] = @mon,DeliveryQuantity = @quantity , DeliveryDate = @DeliveryDate,  ApplyUnitCost = @cost, TotalCost = @totalcost,UpdatingPerson = @person, UpdatingPersonID = @id, UpdatingDate = @date WHERE SubBuyingDeliveryHistoryIndex= @INDEX";
			SqlCommand comm1 = new SqlCommand(str1,con);
			comm1.Transaction = trans;
			
			comm1.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(uwgSBD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());
			comm1.Parameters.Add("@DeliveryDate",DateTime.Parse(uwgSBD_HT.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).ToShortDateString());
			comm1.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(uwgSBD_HT.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString());
			comm1.Parameters.Add("@totalcost", SqlDbType.Decimal).Value = decimal.Parse(uwgSBD_HT.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString()) * decimal.Parse(uwgSBD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());
			comm1.Parameters.Add("@INDEX",SqlDbType.Int).Value = int.Parse(uwgSBD_HT.Rows[rowcount].Cells.FromKey("SubBuyingDeliveryHistoryIndex").Value.ToString());
			comm1.Parameters.Add("@person",SqlDbType.VarChar).Value = Session["UserName"].ToString();
			comm1.Parameters.Add("@id",SqlDbType.VarChar).Value = Session["ID"].ToString();
			comm1.Parameters.Add("@date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm1.Parameters.Add("@year",SqlDbType.Int).Value = int.Parse(uwgSBD_HT.Rows[rowcount].Cells.FromKey("Year").Value.ToString());
			comm1.Parameters.Add("@mon",SqlDbType.Int).Value = int.Parse(uwgSBD_HT.Rows[rowcount].Cells.FromKey("Month").Value.ToString());
			comm1.ExecuteNonQuery();
		}

		
		/// <summary>
		/// 부자재 구매발주원장 수정하는 함수
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		private void SBO_HT_Update(SqlConnection con,SqlTransaction trans)
		{
			//기존 구매발주원장에서 납입수량,잔량,진행상태를 Update			
			int rowcount = int.Parse(hdRowIndex.Value);
			int index= int.Parse(uwgSBD_HT.Rows[rowcount].Cells.FromKey("SubBuyingOrderHistoryIndex").Value.ToString());
			
			string str1 = "select DeliveryRemainQuantity from SBO_HT where SubBuyingOrderHistoryIndex= @INDEX";
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;			
			comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = index;
			comm.CommandText = str1;
			decimal requantity = decimal.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();

			string str =" UPDATE SBO_HT SET DeliveryRemainQuantity = @requantity WHERE SubBuyingOrderHistoryIndex= @INDEX";
			//발주원장의 잔량에 기존잔량값 + 기존납품량 - 수정후납품량 을 넣는다.
			comm.Parameters.Add("@requantity", SqlDbType.Decimal).Value = requantity + (decimal.Parse(hdquantity.Value.ToString()))-(decimal.Parse(uwgSBD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString()));
			comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = index;
			comm.CommandText = str;
			comm.ExecuteNonQuery();	
			comm.Parameters.Clear();
			
			//기존잔량값 + 기존납품량 - 수정후납품량
			if(requantity + (decimal.Parse(hdquantity.Value.ToString()))-(decimal.Parse(uwgSBD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString())) == 0)
			{				
				str =" UPDATE SBO_HT SET ProgressCondition = @progress WHERE SubBuyingOrderHistoryIndex= @INDEX";
				comm.Parameters.Add("@progress",SqlDbType.VarChar).Value = "완료";
				comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = index;
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
			else
			{			
				str =" UPDATE SBO_HT SET ProgressCondition = @progress WHERE SubBuyingOrderHistoryIndex= @INDEX";
				comm.Parameters.Add("@progress",SqlDbType.VarChar).Value = "진행";
				comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = index;
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
		}


		/// <summary>
		/// 부자재 매입금액 수정 함수
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		private void SBSI_MT_Update(SqlConnection con,SqlTransaction trans)
		{
			//기존테이블에서 이전물품액을 마이너스 증가
			int rowcount = int.Parse(hdRowIndex.Value);
			int year = int.Parse(hdyear.Value);
			int mon = int.Parse(hdmon.Value);

			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = uwgSBD_HT.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(hdOldCost.Value));
			comm.Parameters.Add("@year",year);

			Table table = new Table(year,mon);
			comm.CommandText = table.PaymentIncreaseTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			year = int.Parse(uwgSBD_HT.Rows[rowcount].Cells.FromKey("Year").Value.ToString());
			mon = int.Parse(uwgSBD_HT.Rows[rowcount].Cells.FromKey("Month").Value.ToString());
			Table table1 = new Table(year,mon);
			//새로변경되는 물품액 증가
			comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = uwgSBD_HT.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = (decimal.Parse(uwgSBD_HT.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString()))*(decimal.Parse(uwgSBD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString()));// 물품금액
			comm.Parameters.Add("@year",year);
			comm.CommandText = table1.PaymentIncreaseTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}

		/// <summary>
		/// 부자재 입고이력원장 수정
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		private void BOS_HTComUpdate(SqlConnection con,SqlTransaction trans)
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			string str=@"Insert into BOS_HT (ItemNum,ItemDrawNum,ItemName,CompanyName, BusinessRegistrationNum,ProcessCode, ProcessName, Quantity,UnitCost,UpdateHistoryReason, [Date],UpdatePerson, UpdatePersonID,HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,@CompanyName,@BusinessRegistrationNum,'', '', @Quantity,@UnitCost,@UpdateHistoryReason, @Date,@Person, @PersonID,'부자재구매입고',@HistoryIndex)";
				
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			
			comm.Parameters.Add("@itemnum", uwgSBD_HT.Rows[rowcount].Cells.FromKey("ItemNum").Text);	//품목번호
			comm.Parameters.Add("@itemdrawnum", uwgSBD_HT.Rows[rowcount].Cells.FromKey("ItemDrawNum").Text);								//도면번호
			comm.Parameters.Add("@itemname",uwgSBD_HT.Rows[rowcount].Cells.FromKey("ItemName").Text);									//품목명
			comm.Parameters.Add("@CompanyName",uwgSBD_HT.Rows[rowcount].Cells.FromKey("CompanyName").Text);								//업체명
			comm.Parameters.Add("@BusinessRegistrationNum",uwgSBD_HT.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Text);
			comm.Parameters.Add("@Quantity",decimal.Parse(uwgSBD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString()));//합격수량
			comm.Parameters.Add("@UnitCost",decimal.Parse(uwgSBD_HT.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString()));//합격수량
			comm.Parameters.Add("@UpdateHistoryReason",hdReason.Value.Trim());												//공정명
			comm.Parameters.Add("@Date", DateTime.Now.ToShortDateString());//외주출고일자
			comm.Parameters.Add("@Person",Session["UserName"].ToString());
			comm.Parameters.Add("@PersonID", Session["ID"].ToString());	
			comm.Parameters.Add("@HistoryIndex",int.Parse(uwgSBD_HT.Rows[rowcount].Cells.FromKey("SubBuyingDeliveryHistoryIndex").Text));												//원장번호

			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 월마감 여부를 확인하는 함수
		/// </summary>
		/// <returns></returns>
		private bool MonthClosing()
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			int year = 0;
			int month = 0;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select ClosingYear, ClosingMonth From MCI_MT Where AffairDistinction = @Distinction";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@Distinction",SqlDbType.VarChar).Value = "구매";
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				year = int.Parse(dr[0].ToString());
				month = int.Parse(dr[1].ToString());
			}
			conn.Close();

			if(year < int.Parse(uwgSBD_HT.Rows[rowcount].Cells.FromKey("Year").Text))
			{
				return true;
			}
			else if(year == int.Parse(uwgSBD_HT.Rows[rowcount].Cells.FromKey("Year").Text)) 
			{
				if(month < int.Parse(uwgSBD_HT.Rows[rowcount].Cells.FromKey("Year").Text))
					return true;
				else
					return false;
			}
			else
			{
				return false;
			}	
			

			
		}
	}
}
