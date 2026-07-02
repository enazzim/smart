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
	/// OutSideDeliveryPC에 대한 요약 설명입니다.
	/// </summary>
	public class OutSideDeliveryPC : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Button btnDelete;
		protected System.Web.UI.WebControls.Button btnStop;
		protected System.Web.UI.WebControls.Button btnExcel;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter uwgExcel;
		protected System.Web.UI.WebControls.LinkButton linkUpdate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdquantity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdRowIndex;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcStartDate;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgOSD_HT;
		protected System.Web.UI.WebControls.Literal Literal1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdOldMonth;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdReason;
		
		protected System.Web.UI.WebControls.DropDownList ddlState;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdYear;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdMonth;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdApplyCost;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hd_year;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdmon;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}


			ItemSearchControl1.HalfFinishedProducts = true; //반제품 바인딩
			ItemSearchControl1.Products = true; //제품 바인딩
			CSC1.UnitCostDistinction = "외주거래처";

			if(!Page.IsPostBack)
			{
			
				// 중단버튼에 대하여 onclick속성을 추가 시킴
				btnStop.Attributes.Add("onClick","return Confirm('선택한 품목들의 대하여 납품을 중단시겠습니까?');");
				// 삭제버튼에 대하여 onclick속성을 추가 시킴
				btnDelete.Attributes.Add("onClick","return Confirm('선택한 품목들의 대하여 삭제를 하시겠습니까?');");
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
			this.uwgOSD_HT.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.uwgOSD_HT_PageIndexChanged);
			this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
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
				search = new KIT_ERP.Search("OutSideDeliveryPC",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName, wdcStartDate,wdcEndDate,CSC1.Company, CSC1.BusinessRegistrationNum, ddlState.SelectedItem.Value,ddlItemClassification1.SelectedItem.Value);
			else
				search = new KIT_ERP.Search("OutSideDeliveryPC",ItemSearchControl1.ItemNum, "", "", wdcStartDate,wdcEndDate,CSC1.Company, CSC1.BusinessRegistrationNum, ddlState.SelectedItem.Value,ddlItemClassification1.SelectedItem.Value);
			return search.DataSet_search();
		}

		//검색버튼
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			uwgOSD_HT.DisplayLayout.Pager.CurrentPageIndex = 1;
			this.uwgOSD_HT.DataSource = Search();
			this.uwgOSD_HT.DataBind();

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

		//중단버튼 
		private void btnStop_Click(object sender, System.EventArgs e)
		{
			
			if(MonthClosing())
			{
				KIT_ERP.Stop stop = new Stop(uwgOSD_HT,"OutSideDeliveryPC",Session["ID"].ToString());
				stop.MainTableStop();
			}
			else
			{
				RegisterStartupScript("","<script>alert('월마감이 되어 중단이 불가능 합니다!');</script>");
			}

			

			//중단버튼을 눌러 데이터를 중단한 후에 다시 검색 객체를 호출해서 그리드에 바인딩을 다시 시킴.
			this.uwgOSD_HT.DataSource = Search();
			this.uwgOSD_HT.DataBind();

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

		//삭제버튼
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			
//			if(MonthClosing())
//			{
//				KIT_ERP.Delete delete = new Delete(uwgOSD_HT,"OutSideDeliveryPC",Session["UserName"].ToString(), Session["ID"].ToString());
//				delete.MainRowDelete();
//			}
//			else
//			{
//				RegisterStartupScript("","<script>alert('월마감이 되어 삭제가 불가능 합니다!');</script>");
//			}


			KIT_ERP.Delete delete = new Delete(uwgOSD_HT,"OutSideDeliveryPC",Session["UserName"].ToString(), Session["ID"].ToString());
			delete.MainRowDelete();

			//삭제버튼을 눌러 데이터를 삭제한 후에 다시 검색 객체를 호출해서 그리드에 바인딩을 다시 시킴.
			this.uwgOSD_HT.DataSource = Search();
			this.uwgOSD_HT.DataBind();

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

		//현제 그리드에 나타나있는 데이터를 엑셀파일로 다운로드
		private void btnExcel_Click(object sender, System.EventArgs e)
		{
			// 현재 Grid의 내용을 Excel로 Export
			Infragistics.WebUI.UltraWebGrid.UltraWebGrid  uwgExcelImport = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
			uwgExcelImport = uwgOSD_HT;
			uwgExcelImport.DisplayLayout.Pager.AllowPaging = false;
			uwgExcelImport.Columns.FromKey("chk").Hidden = true;

			uwgOSD_HT.DataSource = Search();
			uwgExcelImport.DataBind();
			
			uwgExcel.Export(uwgExcelImport);
		}

		//Grid Paging 기능
		private void uwgOSD_HT_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			uwgOSD_HT.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			uwgOSD_HT.DataSource =  Search();
			uwgOSD_HT.DataBind();

			DataSet ds = new DataSet();
			ds =  Search();

			decimal TotalCost = 0;
			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				TotalCost += decimal.Parse(dr["TotalCost"].ToString());
			}

				
			//Literal1.Text = "총 금액<b>\" " + TotalCost + " \"</b> 원" ;
			Literal1.Text = "총 금액<b>\" " + TotalCost.ToString().Substring(0, TotalCost.ToString().IndexOf(".") + 2) + " \"</b> 원" ;
		}



		/// <summary>
		/// Template Row 에서 수정을 하면 다른 원장에도 수량 조정
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void linkUpdate_Click(object sender, System.EventArgs e)
		{
			

			int rowcount = int.Parse(hdRowIndex.Value);

			//수정하는 함수 내에서 모든함수를 트렌젝션 처리
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();
			
			try
			{	
				if(MonthClosing())
				{
					OSD_HT_Update(conn,tr); //외주납품원장을 수정하는 함수 호출(검사품목과 무검사품목 모두 외주납품원장 수정)
		
					//수정하고자 하는 품목이 검사 품목일 경우
					if(Check(conn,tr) == true)
					{
						QI_HT_Update(conn,tr);//품질검사원장 수정하는 함수호출
						OO_HTWaitingQuantityUpdate(conn,tr);//구매발주원장의 입고대기수량 수정
					}
					else//수정하고자 하는 품목이 무검사 품목일 경우
					{		
						NonQI_HT_Update(conn,tr);//품질검사 수정
						PS_MT_Update(conn,tr);//생산창고 수정하는 함수 호출
						OO_HT_Update(conn,tr);//외주발주원장을 수정하는 함수 호출
						BSI_MT_Update(conn,tr);//매입매출테이블을 수정하는 함수 호출
						B_HT_Update(conn,tr);//매입원장을 수정하는 함수 호출
						OS_MT_Update(conn,tr);//외주창고를 수정하는 함수 호출
						BOS_HTUpdate(conn,tr);

						//수정하고자 하는 품목이 무검사품이면서 제품인경우
						if(Division(conn,tr) == 3)
						{	
							if(!Last(conn,tr,uwgOSD_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString(),int.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("ProcessSequenceNum").Value.ToString())))
							{
								ISR_HT_Update(conn,tr);//입고의뢰원장을 수정하는 함수 호출
								BS_MTUpdate(conn,tr); //영업창고 수량 수정
							}
						}
					

					}
				}
				else
				{
					throw new Exception("월마감이 되어 수정이 불가능 합니다!");
				}

				
				
				tr.Commit();
				
				Response.Write("<script language=javascript>");
				Response.Write("alert('수정하였습니다.');");
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

				this.uwgOSD_HT.DataSource = Search();
				this.uwgOSD_HT.DataBind();

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


		private void BOS_HTUpdate(SqlConnection con, SqlTransaction trans)
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			string str=@"Insert into BOS_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,CompanyName, BusinessRegistrationNum,Quantity,UnitCost,UpdateHistoryReason, [Date],UpdatePerson, UpdatePersonID,HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,@CompanyName,@BusinessRegistrationNum,@Quantity,@UnitCost,@UpdateHistoryReason, @Date,@Person, @PersonID,'외주납품',@HistoryIndex)";
				
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			
			comm.Parameters.Add("@itemnum", uwgOSD_HT.Rows[rowcount].Cells.FromKey("ItemNum").Text);	//품목번호
			comm.Parameters.Add("@itemdrawnum", uwgOSD_HT.Rows[rowcount].Cells.FromKey("ItemDrawNum").Text);							//도면번호
			comm.Parameters.Add("@itemname",uwgOSD_HT.Rows[rowcount].Cells.FromKey("ItemName").Text);									//품목명
			comm.Parameters.Add("@CompanyName",uwgOSD_HT.Rows[rowcount].Cells.FromKey("CompanyName").Text);								//업체명
			comm.Parameters.Add("@BusinessRegistrationNum",uwgOSD_HT.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Text);
			comm.Parameters.Add("@ProcessSequenceNum",int.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("ProcessSequenceNum").Text));	//공정순서
			comm.Parameters.Add("@ProcessCode",uwgOSD_HT.Rows[rowcount].Cells.FromKey("ProcessCode").Text);								//공정코드
			comm.Parameters.Add("@ProcessName",uwgOSD_HT.Rows[rowcount].Cells.FromKey("ProcessName").Text);								//공정명
			comm.Parameters.Add("@Quantity",decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString()));//합격수량
			comm.Parameters.Add("@UnitCost",decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString()));//합격수량
			comm.Parameters.Add("@UpdateHistoryReason",hdReason.Value.Trim());												//공정명
			comm.Parameters.Add("@Date", DateTime.Now.ToShortDateString());//외주출고일자
			comm.Parameters.Add("@Person",Session["UserName"].ToString());
			comm.Parameters.Add("@PersonID", Session["ID"].ToString());	
			comm.Parameters.Add("@HistoryIndex",int.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("OutSideDeliveryHistoryIndex").Text));												//원장번호

			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}

		//무검사인 품목의 최종공정인지 파악하는 함수
		//false이면 최종공정
		private bool Last(SqlConnection conn, SqlTransaction tr, string itemnum, int sequence)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			string str  = "select count(*) From PSI_MT Where RecodingState = 1 and ItemNum = @item and ProcessSequenceNum > @num";
			comm.Parameters.Add("@item",itemnum);
			comm.Parameters.Add("@num",sequence);
			comm.CommandText = str;
			int count = int.Parse(comm.ExecuteScalar().ToString());
			if(count == 0)
				return false;
			else
				return true;
		}


		//수정하고자 하는 품목이 검사품목인지 무검사 품목인지 Check하는 함수
		private bool Check(SqlConnection con, SqlTransaction trans)
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			string aa = "";
			string str = "Select CheckDistinction From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm =  new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = uwgOSD_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			SqlDataReader dr = comm.ExecuteReader();

			if(dr.Read())
			{
				aa = dr["CheckDistinction"].ToString();
			}
			dr.Close();

			if(aa.ToString() == "False")
				return false;
			else
				return true;
		}
		
		//수정하고자 하는 품목이 상품인지 원자재인지 판단하는 함수
		private int Division(SqlConnection con, SqlTransaction trans)
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			string aa="";

			string str = "Select PropertyClassification From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm =  new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value =uwgOSD_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = dr["PropertyClassification"].ToString();
			}
			dr.Close();

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

		//외주납품원장을 수정하는 함수
		private void OSD_HT_Update(SqlConnection con, SqlTransaction trans)
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			//기존 품질검사원장에서 납품수량을 증가
			//string str1 =" UPDATE OSD_HT SET DeliveryQuantity = @quantity , TotalCost = @cost, LotNum = @LotNum  WHERE OutSideDeliveryHistoryIndex= @INDEX";
			string str1 =" UPDATE OSD_HT SET [Year] = @year, [Month] = @mon, DeliveryQuantity = @quantity , TotalCost = @cost, LotNum = @LotNum, DeliveryDate = @date WHERE OutSideDeliveryHistoryIndex= @INDEX";
			SqlCommand comm1 = new SqlCommand(str1,con);
			comm1.Transaction = trans;
					
			comm1.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());
			comm1.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString()) * decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());
			comm1.Parameters.Add("@date", DateTime.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).ToShortDateString());
			comm1.Parameters.Add("@LotNum", uwgOSD_HT.Rows[rowcount].Cells.FromKey("LotNum").Text);
			comm1.Parameters.Add("@INDEX",SqlDbType.Int).Value = int.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("OutSideDeliveryHistoryIndex").Value.ToString());
			comm1.Parameters.Add("@year",int.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("Year").Value.ToString()));
			comm1.Parameters.Add("@mon",int.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("Month").Value.ToString()));
			comm1.ExecuteNonQuery();
		}

		//품질검사원장 수정하는 함수
		private void QI_HT_Update(SqlConnection con, SqlTransaction trans)
		{
			
			//기존 품질검사원장에서 납품수량을 Update
			int rowcount = int.Parse(hdRowIndex.Value);
			string str =" UPDATE QI_HT SET [Year] = @year, [Month] = @mon,  RequestQuantity = @quantity, ApplyUnitCost = @cost, RegistrationDate = @date WHERE HistorySection1= '외주납품' and HistoryIndex1 = @INDEX";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString());
			comm.Parameters.Add("@date", DateTime.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("RegistrationDate").Text).ToShortDateString());
			comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = int.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("OutSideDeliveryHistoryIndex").Value.ToString());
			comm.Parameters.Add("@year",int.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("Year").Value.ToString()));
			comm.Parameters.Add("@mon",int.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("Month").Value.ToString()));
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 외주발주원장 입고대기수량 수정
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		private void OO_HTWaitingQuantityUpdate(SqlConnection con,SqlTransaction trans)
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			decimal quantity = decimal.Parse(hdquantity.Value);

			string str =" UPDATE OO_HT SET InStoreWaitingQuantity = InStoreWaitingQuantity - @InStoreWaitingQuantity WHERE OutSideOrderHistoryIndex = @INDEX";
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			comm.CommandText = str;
			comm.Parameters.Add("@InStoreWaitingQuantity", quantity);
			comm.Parameters.Add("@INDEX",int.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("OutSideOrderHistoryIndex").Value.ToString()));
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			str =" UPDATE OO_HT SET InStoreWaitingQuantity = InStoreWaitingQuantity + @InStoreWaitingQuantity WHERE OutSideOrderHistoryIndex = @INDEX";
			comm.CommandText = str;
			comm.Parameters.Add("@InStoreWaitingQuantity", SqlDbType.Decimal).Value = decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());
			comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = int.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("OutSideOrderHistoryIndex").Value.ToString());
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}


		//무검사품의 품질검사원장 수정하는 함수
		private void NonQI_HT_Update(SqlConnection con,SqlTransaction trans)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;

			int rowcount = int.Parse(hdRowIndex.Value);
			string Condition="";

			string str  = @"Select * From QI_HT WHERE HistorySection1= '외주납품' and HistoryIndex1 = @INDEX";
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());
			comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = int.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("OutSideDeliveryHistoryIndex").Value.ToString());
			comm.CommandText = str;
			SqlDataAdapter daQI = new SqlDataAdapter(comm);
			DataSet dsQI = new DataSet();
			daQI.Fill(dsQI);
			comm.Parameters.Clear();

			foreach(DataRow dr in dsQI.Tables[0].Rows)
			{
				Condition = dr["ProgressCondition"].ToString();
			}

			if(Condition == "입고완료")
			{
				throw new Exception("이미 입고완료된 품목입니다!");
			}
			else
			{
				//기존 품질검사원장에서 납품수량을 Update
				str =" UPDATE QI_HT SET [Year] = @year, [Month] = @mon, RequestQuantity = @quantity, SuitabilityQuantity=@quantity,ApplyUnitCost = @cost, QualityInspectionCompleteDate = @QualityInspectionCompleteDate WHERE HistorySection1= '외주납품' and HistoryIndex1 = @INDEX";
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());
				comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString());
				comm.Parameters.Add("@QualityInspectionCompleteDate", DateTime.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).ToShortDateString());
				comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = int.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("OutSideDeliveryHistoryIndex").Value.ToString());
				comm.Parameters.Add("@year",int.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("Year").Value.ToString()));
				comm.Parameters.Add("@mon",int.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("Month").Value.ToString()));
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}

		}




		//생산창고의 수량을 수정하는 함수
		private void PS_MT_Update(SqlConnection con, SqlTransaction trans)
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			int year = int.Parse(hdYear.Value);
			int mon = int.Parse(hdMonth.Value);
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			
			//품목정보테이블에서 현제 수정하고자하는 품목의 단가를 가지고있는 변수
			decimal unitcost = decimal.Parse(hdApplyCost.Value);
			
			//기존테이블에서 이전수량을 마이너스 증가
		
			comm.Parameters.Add("@cost", SqlDbType.VarChar).Value = -(unitcost*decimal.Parse(hdquantity.Value.ToString()));
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(hdquantity.Value.ToString()));
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = uwgOSD_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@code", SqlDbType.VarChar).Value = uwgOSD_HT.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
			comm.Parameters.Add("@year",year);

			Table table = new Table(year,mon);
			comm.CommandText = table.OutProductionTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//새로변경되는 수정한 수량을 증가
			year = DateTime.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Year;
			mon = DateTime.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Month;
			comm.Parameters.Add("@cost", SqlDbType.VarChar).Value = decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Text)*decimal.Parse(hdquantity.Value.ToString());
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = uwgOSD_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@code", SqlDbType.VarChar).Value = uwgOSD_HT.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
			comm.Parameters.Add("@year",year);
			Table table1 = new Table(year,mon);

			comm.CommandText = table1.OutProductionTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @code and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  uwgOSD_HT.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				comm.Parameters.Add("@code",uwgOSD_HT.Rows[rowcount].Cells.FromKey("ProcessCode").Text);
				comm.Parameters.Add("@year",DateTime.Now.Year);

				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
				comm.Parameters.Clear();
						
				string StockQuantity = "False";
				string ItemName = uwgOSD_HT.Rows[rowcount].Cells.FromKey("ItemNum").Text;
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

		//입고의뢰원장의 수량을 수정하는 함수
		private void ISR_HT_Update(SqlConnection con, SqlTransaction trans)
		{
			//기존 품질검사원장에서 납품수량과 금액을 Update
			int rowcount = int.Parse(hdRowIndex.Value);

			string str =" UPDATE ISR_HT SET InstorehouseRequestQuantity = @quantity WHERE HistorySection ='외주납품' and HistoryIndex = @idx";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;		
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());
			comm.Parameters.Add("@idx",SqlDbType.Int).Value = uwgOSD_HT.Rows[rowcount].Cells.FromKey("HistoryIndex").Value.ToString();
			
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 영업창고 수량 수정
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		private void BS_MTUpdate(SqlConnection con, SqlTransaction trans)
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			int year = int.Parse(hdYear.Value);
			int mon = int.Parse(hdMonth.Value);
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			
			//품목정보테이블에서 현제 수정하고자하는 품목의 단가를 가지고있는 변수
			decimal unitcost = decimal.Parse(hdApplyCost.Value);
			
			//기존테이블에서 이전수량을 마이너스 증가
		
			comm.Parameters.Add("@cost", SqlDbType.VarChar).Value = -(unitcost*decimal.Parse(hdquantity.Value.ToString()));
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(hdquantity.Value.ToString()));
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = uwgOSD_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@storenum", SqlDbType.VarChar).Value = 1;
			comm.Parameters.Add("@year",year);

			Table table = new Table(year,mon);
			comm.CommandText = table.InBusinessTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//새로변경되는 수정한 수량을 증가
			year = DateTime.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Year;
			mon = DateTime.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Month;
			comm.Parameters.Add("@cost", SqlDbType.VarChar).Value = decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Text)*decimal.Parse(hdquantity.Value.ToString());
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = uwgOSD_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@storenum", SqlDbType.VarChar).Value = 1;
			comm.Parameters.Add("@year",year);
			Table table1 = new Table(year,mon);

			comm.CommandText = table1.InBusinessTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM BS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and BusinessStorehouseNum = 1 and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  uwgOSD_HT.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				comm.Parameters.Add("@year",DateTime.Now.Year);

				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
				comm.Parameters.Clear();
						
				string StockQuantity = "False";
				string ItemName = uwgOSD_HT.Rows[rowcount].Cells.FromKey("ItemNum").Text;
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
					throw new Exception(ItemName + " 의 재고량 부족으로 수정이 불가능 합니다.");
				}
			}
		}

		//기존 외주발주원장에서 납입수량,잔량,진행상태를 Update			
		private void OO_HT_Update(SqlConnection con, SqlTransaction trans)
		{			
			int rowcount = int.Parse(hdRowIndex.Value);
			int index= int.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("OutSideOrderHistoryIndex").Value.ToString());;
			string str1 = "select RemainQuantity from OO_HT where OutSideOrderHistoryIndex= @INDEX";
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.CommandText = str1;
			comm.Transaction = trans;
		
			comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = index;
		
			decimal requantity = decimal.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();

			string str =" UPDATE OO_HT SET RemainQuantity = @requantity WHERE OutSideOrderHistoryIndex= @INDEX";
			
			//발주원장의 잔량에 기존잔량값 + 기존납품량 - 수정후납품량 을 넣는다.
			comm.Parameters.Add("@requantity", SqlDbType.Decimal).Value = requantity + (decimal.Parse(hdquantity.Value.ToString()))-(decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString()));
			comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = index;
			comm.CommandText = str;
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
			
		
			//기존잔량값 + 기존납품량 - 수정후납품량
			if(requantity + (decimal.Parse(hdquantity.Value.ToString()))-(decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString())) == 0)
			{				
				str =" UPDATE OO_HT SET ProgressCondition = @progress WHERE OutSideOrderHistoryIndex= @INDEX";
				comm.Parameters.Add("@progress",SqlDbType.VarChar).Value = "완료";
				comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = index;
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();

			}
			else
			{			
				str =" UPDATE OO_HT SET ProgressCondition = @progress WHERE OutSideOrderHistoryIndex= @INDEX";
				comm.Parameters.Add("@progress",SqlDbType.VarChar).Value = "진행";
				comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = index;
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();

			}
		}

		//매입매출원장의 금액 수정하는 함수
		private void BSI_MT_Update(SqlConnection con, SqlTransaction trans)
		{
			
			//기존테이블에서 이전물품액을 마이너스 증가
			int rowcount = int.Parse(hdRowIndex.Value);
			int year = int.Parse(hd_year.Value);
			int mon = int.Parse(hdmon.Value);
			decimal total = decimal.Parse(hdApplyCost.Value)*decimal.Parse(hdquantity.Value);
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = uwgOSD_HT.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -total;
			comm.Parameters.Add("@year",year);

			Table table = new Table(year,mon);
			comm.CommandText = table.PaymentIncreaseTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//새로변경되는 물품액 증가
			year = int.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("Year").Text);
			mon = int.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("Month").Text);
			comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = uwgOSD_HT.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = (decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString()))*(decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString()));// 물품금액
			comm.Parameters.Add("@year",year);

			Table table1 = new Table(year,mon);
			comm.CommandText = table1.PaymentIncreaseTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}

		//매입원장의 금액과 수량 수정하는 함수
		private void B_HT_Update(SqlConnection con, SqlTransaction trans)
		{
			int rowcount = int.Parse(hdRowIndex.Value);

			//새로 변경되는 품질검사원장에서 납품수량과 금액을 Update
			string str =" UPDATE B_HT SET BuyingQuantity = @quantity,TotalCost = @cost WHERE HistorySection= '외주납품' and HistoryIndex = @INDEX";
			SqlCommand comm = new SqlCommand(str,con);
            comm.Transaction = trans;		
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = ((decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString()))*(decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString())));
			comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = int.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("OutSideDeliveryHistoryIndex").Value.ToString());
			comm.ExecuteNonQuery();
		}

		//외주창고의 수량을 수정하는 함수
		private void OS_MT_Update(SqlConnection con, SqlTransaction trans)
		{

			int rowcount = int.Parse(hdRowIndex.Value);
			//시작공정코드,시작공정순서
			string begincode = "";
			int beginsequence = 0;
			string itemnum = uwgOSD_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();

			//발주원장 인덱스가 동일한 것을 찾으면 시작공정을 찾을수 있다.
			string str = @"Select ItemNum, BeginProcessCode, BusinessRegistrationNum  From OO_HT Where OutSideOrderHistoryIndex = @idx";
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			comm.Parameters.Add("@idx",SqlDbType.Int).Value = int.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("OutSideOrderHistoryIndex").Value.ToString());
			comm.CommandText = str;
			DataSet ds_code = new DataSet();
			SqlDataAdapter da_code = new SqlDataAdapter(comm);
			da_code.Fill(ds_code);
			comm.Parameters.Clear();

			foreach(DataRow dr_code in ds_code.Tables[0].Rows)
			{
				begincode = dr_code["BeginProcessCode"].ToString();
				str = "Select * From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
				comm.Parameters.Add("@num", dr_code["ItemNum"].ToString());
				comm.Parameters.Add("@code",dr_code["BeginProcessCode"].ToString());
				comm.CommandText = str;
				SqlDataReader dr1 = comm.ExecuteReader();
				while(dr1.Read())
				{
					beginsequence = int.Parse(dr1["ProcessSequenceNum"].ToString());
				}
				dr1.Close();
				comm.Parameters.Clear();

			}
			
			//해당 품목이 가장 낮은 공정이면 하위품목들의 최상위공정을 떨어주고
			//그렇지 않으면 해당공정의 바로 아래공정품을 외주창고에서 떨어낸다
			//이때 하위품목의 자산분류가 원자재이면 해당 품목을 떨어준다

			Decimal m_cost = 0;
			Decimal m_progressrate = 100;

			str = @"Select isnull(Min(ProcessSequenceNum),0) From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessSequenceNum < @sequence";
			comm.Parameters.Add("@num",itemnum);
			comm.Parameters.Add("@sequence",beginsequence);
			comm.CommandText = str;
			

			if(int.Parse(comm.ExecuteScalar().ToString()) == 0)
			{
				comm.Parameters.Clear();
				//공정순서에 가서 해당자품목을 떨어준다
				str = @"Select * From IOI_MT Where RecodingState = 1 and ParentItemNum = @num";
				comm.Parameters.Add("@num",itemnum);
				comm.CommandText = str;
				DataSet ds = new DataSet();
				SqlDataAdapter da = new SqlDataAdapter(comm);
				da.Fill(ds);
				comm.Parameters.Clear();
				foreach(DataRow dr in ds.Tables[0].Rows)
				{
					//하위품목이 원자재이면 바로 떨어준다
					if(Division(dr["ChildItemNum"].ToString()) == 1)
					{
						// 품목의 금액
						string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
						SqlCommand comm_cost = new SqlCommand(str_cost,con);
						comm_cost.Transaction = trans;
						comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = dr["ChildItemNum"].ToString();
						SqlDataReader dr_cost = comm_cost.ExecuteReader();
			
						if(dr_cost.Read())
							m_cost = Decimal.Parse(dr_cost["StandardUnitCost"].ToString());
						dr_cost.Close();

						
						decimal a = 0;
						str = @"Select * From IOI_MT Where RecodingState = 1 and ParentItemNum = @parent and ChildItemNum = @child";
						SqlCommand comm1 = new SqlCommand(str,con);
						comm1.Transaction = trans;
						comm1.Parameters.Add("@parent",itemnum);
						comm1.Parameters.Add("@child",dr["ChildItemNum"].ToString());
						SqlDataReader dr_1 =  comm1.ExecuteReader();
						while(dr_1.Read())
						{
							a = decimal.Parse(dr_1["NeedQuantityNumerator"].ToString())/decimal.Parse(dr_1["NeedQuantityDenominator"].ToString());
						}
						dr_1.Close();
						comm1.Parameters.Clear();

						if(a == 0)
							throw new Exception(itemnum + " 품목의 품목구성이 없거나 올바르지 않습니다!");
						

						//기존테이블에서 이전수량을 마이너스 증가
						int year = int.Parse(hdYear.Value);
						int mon = int.Parse(hdMonth.Value);
						comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(a *decimal.Parse(hdquantity.Value.ToString())* Decimal.Parse(m_cost.ToString()) * Decimal.Parse(m_progressrate.ToString())/100);
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(a* decimal.Parse(hdquantity.Value.ToString()));
						comm.Parameters.Add("@num", SqlDbType.VarChar).Value = dr["ChildItemNum"].ToString();
						comm.Parameters.Add("@code", SqlDbType.VarChar).Value = "14000000";
						comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = uwgOSD_HT.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
						comm.Parameters.Add("@year",year);

						Table table = new Table(year,mon);
						comm.CommandText = table.OutTable();
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();


						//새로변경되는 수정한 수량을 증가
						year = DateTime.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Year;
						mon = DateTime.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Month;
						comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = (a *decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString())* Decimal.Parse(m_cost.ToString()) * Decimal.Parse(m_progressrate.ToString())/100);
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = (a* decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString()));
						comm.Parameters.Add("@num", SqlDbType.VarChar).Value = dr["ChildItemNum"].ToString();
						comm.Parameters.Add("@code", SqlDbType.VarChar).Value = "14000000";
						comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = uwgOSD_HT.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
						comm.Parameters.Add("@year",year);
						Table table1 = new Table(year,mon);
						comm.CommandText = table1.OutTable();
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();


						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM OS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
							comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  dr["ChildItemNum"].ToString();
							comm.Parameters.Add("@code",SqlDbType.VarChar).Value = "14000000";
							comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = uwgOSD_HT.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
							comm.Parameters.Add("@year" ,DateTime.Now.Year);
							comm.CommandText = strSQL;
							SqlDataReader reader = comm.ExecuteReader();
						
							string StockQuantity = "False";
							string ItemName = dr["ChildItemNum"].ToString();
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
								throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
							}

						}

					}
					else
					{
						//공정품이면 자품목의 최대공정을 떨어준다.
						string code = "";
						str = @"Select ProcessCode, ProcessSequenceNum From PSI_MT Where RecodingState = 1 and 
							ItemNum = @num and ProcessSequenceNum = (Select Max(ProcessSequenceNum) From PSI_MT 
							Where RecodingState = 1 and ItemNum = @num)";
						comm.Parameters.Add("@num",dr["ChildItemNum"].ToString());
						comm.CommandText = str;
						SqlDataReader dr_Code = comm.ExecuteReader();
						while(dr_Code.Read())
						{
							code = dr_Code["ProcessCode"].ToString();
						}
						dr_Code.Close();
						comm.Parameters.Clear();

						// 품목의 금액
						string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
						SqlCommand comm_cost = new SqlCommand(str_cost,con);
						comm_cost.Transaction = trans;
						comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = dr["ChildItemNum"].ToString();
						SqlDataReader dr_cost = comm_cost.ExecuteReader();
			
						if(dr_cost.Read())
							m_cost = Decimal.Parse(dr_cost["StandardUnitCost"].ToString());
						dr_cost.Close();

						// 품목의 진척율
						string str_rate = "Select ProgressRate From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
						SqlCommand comm_rate = new SqlCommand(str_rate,con);
						comm_rate.Transaction = trans;
						comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = dr["ChildItemNum"].ToString();
						comm_rate.Parameters.Add("@code",SqlDbType.VarChar).Value = code;
						SqlDataReader dr_rate = comm_rate.ExecuteReader();			
						if(dr_rate.Read())
							m_progressrate = Decimal.Parse(dr_rate["ProgressRate"].ToString());
						dr_rate.Close();
						comm_rate.Parameters.Clear();

						//자품목분자/분모량
						decimal a = 0;
						str = @"Select * From IOI_MT Where RecodingState = 1 and ParentItemNum = @parent and ChildItemNum = @child";
						SqlCommand comm1 = new SqlCommand(str,con);
						comm1.Transaction = trans;
						comm1.Parameters.Add("@parent",itemnum);
						comm1.Parameters.Add("@child",dr["ChildItemNum"].ToString());
						SqlDataReader dr_1 =  comm1.ExecuteReader();
						while(dr_1.Read())
						{
							a = decimal.Parse(dr_1["NeedQuantityNumerator"].ToString())/decimal.Parse(dr_1["NeedQuantityDenominator"].ToString());
						}
						dr_1.Close();
						comm1.Parameters.Clear();

						if(a == 0)
							throw new Exception(itemnum + " 품목의 품목구성이 없거나 올바르지 않습니다!");

						//기존테이블에서 이전수량을 마이너스 증가		
						int year = int.Parse(hdYear.Value);
						int mon = int.Parse(hdMonth.Value);
						comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(a *decimal.Parse(hdquantity.Value.ToString())* Decimal.Parse(m_cost.ToString()) * Decimal.Parse(m_progressrate.ToString())/100);
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(a* decimal.Parse(hdquantity.Value.ToString()));
						comm.Parameters.Add("@num", SqlDbType.VarChar).Value = dr["ChildItemNum"].ToString();
						comm.Parameters.Add("@code", SqlDbType.VarChar).Value = code;
						comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = uwgOSD_HT.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
						comm.Parameters.Add("@year",year);

						Table table = new Table(year,mon);
						comm.CommandText = table.OutTable();
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();


						//새로변경되는 수정한 수량을 증가
						year = DateTime.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Year;
						mon = DateTime.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Month;
						comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = (a *decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString())* Decimal.Parse(m_cost.ToString()) * Decimal.Parse(m_progressrate.ToString())/100);
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = (a* decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString()));
						comm.Parameters.Add("@num", SqlDbType.VarChar).Value = dr["ChildItemNum"].ToString();
						comm.Parameters.Add("@code", SqlDbType.VarChar).Value = code;
						comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = uwgOSD_HT.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
						comm.Parameters.Add("@year",year);
						Table table1 = new Table(year,mon);
						comm.CommandText = table1.OutTable();
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();



						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM OS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
							comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  dr["ChildItemNum"].ToString();
							comm.Parameters.Add("@code",SqlDbType.VarChar).Value = code;
							comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = uwgOSD_HT.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
							comm.Parameters.Add("@year" ,DateTime.Now.Year);
							comm.CommandText = strSQL;
							SqlDataReader reader = comm.ExecuteReader();
						
							string StockQuantity = "False";
							string ItemName = dr["ChildItemNum"].ToString();
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
								throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
							}

						}

						

					}
				}
				

			}
			else
			{
				comm.Parameters.Clear();
				//해당품목의 바로 아래 공정을 떨어준다
				string code = "";
				str = @"Select ProcessCode, ProcessSequenceNum From PSI_MT Where RecodingState = 1 and 
							ItemNum = @num and ProcessSequenceNum = (Select Max(ProcessSequenceNum) From PSI_MT 
							Where RecodingState = 1 and ItemNum = @num and ProcessSequenceNum < @sequence)";
				comm.Parameters.Add("@num",itemnum);
				comm.Parameters.Add("@sequence",beginsequence);
				comm.CommandText = str;				
				SqlDataReader dr_Code = comm.ExecuteReader();
				while(dr_Code.Read())
				{
					code = dr_Code["ProcessCode"].ToString();
				}
				dr_Code.Close();
				comm.Parameters.Clear();

				// 품목의 금액
				string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
				SqlCommand comm_cost = new SqlCommand(str_cost,con);
				comm_cost.Transaction = trans;
				comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = itemnum;
				SqlDataReader dr_cost = comm_cost.ExecuteReader();
			
				if(dr_cost.Read())
					m_cost = Decimal.Parse(dr_cost["StandardUnitCost"].ToString());
				dr_cost.Close();

				// 품목의 진척율
				string str_rate = "Select ProgressRate From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
				SqlCommand comm_rate = new SqlCommand(str_rate,con);
				comm_rate.Transaction = trans;
				comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = itemnum;
				comm_rate.Parameters.Add("@code",SqlDbType.VarChar).Value = code;
				SqlDataReader dr_rate = comm_rate.ExecuteReader();			
				if(dr_rate.Read())
					m_progressrate = Decimal.Parse(dr_rate["ProgressRate"].ToString());
				dr_rate.Close();
				comm_rate.Parameters.Clear();

				//				//외주창고에 떨어줄 품목
				int year = int.Parse(hdYear.Value);
				int mon = int.Parse(hdMonth.Value);
				comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(hdquantity.Value.ToString())* Decimal.Parse(m_cost.ToString()) * Decimal.Parse(m_progressrate.ToString())/100);
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(hdquantity.Value.ToString()));
				comm.Parameters.Add("@num", SqlDbType.VarChar).Value = itemnum;
				comm.Parameters.Add("@code", SqlDbType.VarChar).Value = code;
				comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = uwgOSD_HT.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
				comm.Parameters.Add("@year",year);

				Table table = new Table(year,mon);
				comm.CommandText = table.OutTable();
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();


				//새로변경되는 수정한 수량을 증가
				year = DateTime.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Year;
				mon = DateTime.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Month;
				comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = (decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString())* Decimal.Parse(m_cost.ToString()) * Decimal.Parse(m_progressrate.ToString())/100);
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = (decimal.Parse(uwgOSD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString()));
				comm.Parameters.Add("@num", SqlDbType.VarChar).Value = itemnum;
				comm.Parameters.Add("@code", SqlDbType.VarChar).Value = code;
				comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = uwgOSD_HT.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
				comm.Parameters.Add("@year",year);
				Table table1 = new Table(year,mon);

				comm.CommandText = table1.OutTable();
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();


				if(MinusStore() == false)
				{
					string strSQL = "SELECT StockQuantity12 FROM OS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  itemnum;
					comm.Parameters.Add("@code",SqlDbType.VarChar).Value = code;
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = uwgOSD_HT.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
					comm.Parameters.Add("@year" ,DateTime.Now.Year);
					comm.CommandText = strSQL;
					SqlDataReader reader = comm.ExecuteReader();
						
					string StockQuantity = "False";
					string ItemName = itemnum;
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
						throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
					}
				}
			}

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

		private int Division(string ItemNum)
		{
			string aa="";

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select PropertyClassification From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = ItemNum.ToString();
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
			else //if(aa.ToString() =="팬텀")
				return 5;
		}

		/// <summary>
		/// 월마감 여부를 확인하는 함수
		/// </summary>
		/// <returns></returns>
		private bool MonthClosing()
		{
			int count = int.Parse(hdRowIndex.Value);
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

			if(year < int.Parse(uwgOSD_HT.Rows[count].Cells.FromKey("Year").Text))
				return true;
			else if(year == int.Parse(uwgOSD_HT.Rows[count].Cells.FromKey("Year").Text))
			{
				if(month < int.Parse(uwgOSD_HT.Rows[count].Cells.FromKey("Month").Text))
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
