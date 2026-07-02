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
	/// BuyingDeliveryPC에 대한 요약 설명입니다.
	/// </summary>
	public class BuyingDeliveryPC : System.Web.UI.Page
	{
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcStartDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
		protected System.Web.UI.WebControls.Button btnExcel;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.WebControls.LinkButton linkUpdate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdRowIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdquantity;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgBD_HT;
		protected System.Web.UI.WebControls.Button btnStop;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter uwgExcel;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Literal Literal1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdOldMonth;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdOldYear;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdReason;
		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdOldCost;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdmon;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdyear;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
			
		private void Page_Load(object sender, System.EventArgs e)
		{
			Literal1.Text = "";
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}


			//품목정보 초기화
			//btnInit.Attributes.Add("onClick","ResetTextBox()");

			ItemSearchControl1.RawMaterials = true; //원자재 바인딩
			ItemSearchControl1.Commodity = true; //상품 바인딩
			CSC1.UnitCostDistinction = "구매거래처";

			if(!Page.IsPostBack)
			{

				// 중단버튼에 대하여 onclick속성을 추가 시킴
				btnStop.Attributes.Add("onClick", "return Confirm('선택한 품목들의 대하여 납품을 중단하시겠습니까?');");
				//취소버튼에 대하여 onclick속성을 추가 시킴
				btnCancel.Attributes.Add("onClick","return Confirm('선택한 품목들에 대하여 납품을 취소하시겠습니까?');");

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
			this.uwgBD_HT.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.uwgBD_HT_PageIndexChanged);
			this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
			this.linkUpdate.Click += new System.EventHandler(this.linkUpdate_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		//검색버튼 클릭
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			uwgBD_HT.DisplayLayout.Pager.CurrentPageIndex = 1;
			this.uwgBD_HT.DataSource = Search();

			this.uwgBD_HT.DataBind();

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
                search = new KIT_ERP.Search("BuyingDeliveryPC",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,wdcStartDate, ddlItemClassification1.SelectedItem.Value, wdcEndDate, CSC1.Company, CSC1.BusinessRegistrationNum);
			else
				search = new KIT_ERP.Search("BuyingDeliveryPC",ItemSearchControl1.ItemNum, "", "",wdcStartDate, ddlItemClassification1.SelectedItem.Value, wdcEndDate, CSC1.Company, CSC1.BusinessRegistrationNum);

			return search.DataSet_search();
		}

		

		//그리드에 현제 나타난 항목을 엑셀 파일로 다운로드
		private void btnExcel_Click(object sender, System.EventArgs e)
		{
			// 현재 Grid의 내용을 Excel로 Export
			Infragistics.WebUI.UltraWebGrid.UltraWebGrid  uwgExcelImport = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
			uwgExcelImport = uwgBD_HT;
			uwgExcelImport.DisplayLayout.Pager.AllowPaging = false;
			uwgExcelImport.Columns.FromKey("chk").Hidden = true;


			this.uwgBD_HT.DataSource = Search();
			this.uwgBD_HT.DataBind();

			uwgExcel.Export(uwgBD_HT);	
	
			DataSet ds = new DataSet();
			ds = Search();

			decimal TotalCost = 0;
			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				TotalCost += decimal.Parse(dr["TotalCost"].ToString());
			}

			Literal1.Text = "총 금액<b>\" " + TotalCost + " \"</b> 원" ;
		}

		//중지버튼 클릭
		private void btnStop_Click(object sender, System.EventArgs e)
		{
//			if(MonthClosing())
//			{
//				KIT_ERP.Stop stop = new Stop(uwgBD_HT,"BuyingDeliveryPC",Session["ID"].ToString());
//				stop.MainTableStop();
//
//			}
//			else
//			{
//				RegisterStartupScript("","<script>alert('월마감이 되어 중단이 불가능 합니다!');</script>");
//			}
			

			KIT_ERP.Stop stop = new Stop(uwgBD_HT,"BuyingDeliveryPC",Session["ID"].ToString());
			stop.MainTableStop();

			//중단버튼을 눌러 데이터를 중단한 후에 다시 검색 객체를 호출해서 그리드에 바인딩을 다시 시킴.
			this.uwgBD_HT.DataSource = Search();
			this.uwgBD_HT.DataBind();



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

		//삭제버튼 클릭
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
//			if(MonthClosing())
//			{
//				KIT_ERP.Delete cancel = new Delete(uwgBD_HT,"BuyingDeliveryPC",Session["UserName"].ToString(),Session["ID"].ToString());
//				cancel.MainRowDelete();
//
//			}
//			else
//			{
//				RegisterStartupScript("","<script>alert('월마감이 되어 삭제가 불가능 합니다!');</script>");
//			}

			KIT_ERP.Delete cancel = new Delete(uwgBD_HT,"BuyingDeliveryPC",Session["UserName"].ToString(),Session["ID"].ToString());
			cancel.MainRowDelete();
			
			//삭제버튼을 눌러 데이터를 삭제한 후에 다시 검색 객체를 호출해서 그리드에 바인딩을 다시 시킴.
			this.uwgBD_HT.DataSource = Search();
			this.uwgBD_HT.DataBind();



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

		//Grid Paging 기능 
		private void uwgBD_HT_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			uwgBD_HT.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			this.uwgBD_HT.DataSource = Search();
			uwgBD_HT.DataBind();
		}

		
		
		//Template Row 에서 수정을 하면 다른 원장에도 수량 조정
		private void linkUpdate_Click(object sender, System.EventArgs e)
		{

			if(MonthClosing())
			{
				int rowcount = int.Parse(hdRowIndex.Value);
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();
				SqlTransaction tr = conn.BeginTransaction();
			
				try
				{
									
					BD_HT_Update(conn,tr);//납품원장 수정하는 함수 호출(검사품목과 무검사품목 모두 납품원장 수정)

					//수정하고자 하는 품목이 검사 품목일 경우
					if(Check(conn,tr) == true)
					{	
						if(uwgBD_HT.Rows[rowcount].Cells.FromKey("ProgressCondition").ToString() != "완료")
						{
							QI_HT_Update(conn,tr);//품질검사원장 수정하는 함수호출
							BO_HTWaitingQuantityUpdate(conn,tr);//구매발주원장의 입고대기수량 수정
						}
						else
							throw new Exception("이미 품질검사가 완료된 항목이라 수정이 불가능 합니다!");
					}
					
						//수정하고자 하는 품목이 무검사 품목일 경우			
					else
					{
						BO_HT_Update(conn,tr);//발주원장 수정하는 함수 호출(무검사품이면서 상품과 원자재 모두 발주원장 수정)

						BSI_MT_Update(conn,tr);//매입매출테이블 수정하는 함수 호출

						B_HT_Update(conn,tr);//매입원장 수정하는 함수 호출

						//수정하고자 하는 품목이 무검사품이면서 원자재인경우
						if(Division(conn,tr) == 1)
						{	
							NonQI_HT_Update(conn,tr);//무검사품을 품질검사원장 수정하는 함수호출	
							NonHistory(conn,tr);		//무검사품 수정시 수정되는 이력원장
							RMS_MT_Update(conn,tr);//원자재창고 수정하는 함수 호출	
							SH_HT_RMS_MTUpdate(conn,tr);	//	무검사 원자재수량 수정			
							BOS_HTRowUpdate(conn,tr);//구매이력원장 수량 및 단가 일자 수정시 함수 호출
						}
							//수정하고자 하는 품목이 무검사품이면서 상품인경우
						else if(Division(conn,tr) == 4)
						{	
							NonQI_HT_Update(conn,tr);//무검사품을 품질검사원장 수정하는 함수호출		
							ISR_HT_Update(conn,tr);//입고의뢰원장 수정하는 함수 호출

							PS_MT_Update(conn,tr);//생산창고 수정하는 함수 호출
							SH_HT_PS_MTUpdate(conn,tr);
							BOS_HTComUpdate(conn,tr);//구매이력원장 수량 및 단가 일자 수정시 함수 호출
						}
					}			
				
				
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

					this.uwgBD_HT.DataSource = Search();
					this.uwgBD_HT.DataBind();

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
			else
			{
				RegisterStartupScript("","<script>alert('월마감이 되어 수정이 불가능 합니다!');</script>");

				this.uwgBD_HT.DataSource = Search();
				this.uwgBD_HT.DataBind();

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

		




		
		//수정하고자 하는 품목이 검사품목인지 무검사 품목인지 Check하는 함수
		private bool Check(SqlConnection con,SqlTransaction trans)
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			string aa = "";
			string str = "Select CheckDistinction From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm =  new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = uwgBD_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
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
		private int Division(SqlConnection con,SqlTransaction trans)
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			string aa="";

			string str = "Select PropertyClassification From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm =  new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value =uwgBD_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
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

		//품질검사원장 수정하는 함수
		private void QI_HT_Update(SqlConnection con,SqlTransaction trans)
		{
			//기존 품질검사원장에서 납품수량을 Update
			int rowcount = int.Parse(hdRowIndex.Value);
			string str =" UPDATE QI_HT SET [Year]=@year, [Month] = @mon, RequestQuantity = @quantity, ApplyUnitCost = @cost, RegistrationDate = @date WHERE HistorySection1= '구매납품' and HistoryIndex1 = @INDEX";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;		
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString());
			comm.Parameters.Add("@date", DateTime.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("RegistrationDate").Text).ToShortDateString());
			comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = int.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("BuyingDeliveryHistoryIndex").Value.ToString());
			comm.Parameters.Add("@year",SqlDbType.Int).Value = int.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("Year").Value.ToString());
			comm.Parameters.Add("@mon",SqlDbType.Int).Value = int.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("Month").Value.ToString());
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}

		private void SH_HT_RMS_MTUpdate(SqlConnection con,SqlTransaction trans)
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			string str = "Update SH_HT Set Quantity=@Quantity, [Date] = @Date where HistoryDivision='구매납품' and  HistoryIndex=@index ";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@Quantity",decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString()));//합격수량
			comm.Parameters.Add("@Date", DateTime.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryDate").Value.ToString()).ToShortDateString());//외주출고일자
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("BuyingDeliveryHistoryIndex").Value.ToString());//출고원장인덱스
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}

		private void SH_HT_PS_MTUpdate(SqlConnection con,SqlTransaction trans)
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			string str = "Update SH_HT Set Quantity=@Quantity, [Date] = @Date where HistoryDivision='구매납품' and  HistoryIndex=@index ";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@Quantity",decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString()));//합격수량
			comm.Parameters.Add("@Date", DateTime.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryDate").Value.ToString()).ToShortDateString());//외주출고일자
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("BuyingDeliveryHistoryIndex").Value.ToString());//출고원장인덱스
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}

		private void BOS_HTRowUpdate(SqlConnection con,SqlTransaction trans)
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			string str=@"Insert into BOS_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,CompanyName, BusinessRegistrationNum,Quantity,UnitCost,UpdateHistoryReason, [Date],UpdatePerson, UpdatePersonID,HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,0,'14000000','소재',@CompanyName,@BusinessRegistrationNum,@Quantity,@UnitCost,@UpdateHistoryReason, @Date,@Person, @PersonID,'구매납품',@HistoryIndex)";
				
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			
			comm.Parameters.Add("@itemnum", uwgBD_HT.Rows[rowcount].Cells.FromKey("ItemNum").Text);	//품목번호
			comm.Parameters.Add("@itemdrawnum", uwgBD_HT.Rows[rowcount].Cells.FromKey("ItemDrawNum").Text);								//도면번호
			comm.Parameters.Add("@itemname",uwgBD_HT.Rows[rowcount].Cells.FromKey("ItemName").Text);									//품목명
			comm.Parameters.Add("@CompanyName",uwgBD_HT.Rows[rowcount].Cells.FromKey("CompanyName").Text);								//업체명
			comm.Parameters.Add("@BusinessRegistrationNum",uwgBD_HT.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Text);
			//comm.Parameters.Add("@ProcessSequenceNum",uwgBD_HT.Rows[rowcount].Cells.FromKey("ProcessSequenceNum").Text);											//공정순서
			//comm.Parameters.Add("@ProcessCode",uwgBD_HT.Rows[rowcount].Cells.FromKey("ProcessCode").Text);											//공정코드
			//comm.Parameters.Add("@ProcessName",uwgBD_HT.Rows[rowcount].Cells.FromKey("ProcessName").Text);												//공정명
			comm.Parameters.Add("@Quantity",decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString()));//합격수량
			comm.Parameters.Add("@UnitCost",decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString()));//합격수량
			comm.Parameters.Add("@UpdateHistoryReason",hdReason.Value.Trim());												//공정명
			comm.Parameters.Add("@Date", DateTime.Now.ToShortDateString());//외주출고일자
			comm.Parameters.Add("@Person",Session["UserName"].ToString());
			comm.Parameters.Add("@PersonID", Session["ID"].ToString());	
			comm.Parameters.Add("@HistoryIndex",int.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("BuyingDeliveryHistoryIndex").Text));												//원장번호

			comm.ExecuteNonQuery();
		}

		private void BOS_HTComUpdate(SqlConnection con,SqlTransaction trans)
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			string str=@"Insert into BOS_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,CompanyName, BusinessRegistrationNum,Quantity,UnitCost,UpdateHistoryReason, [Date],UpdatePerson, UpdatePersonID,HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,99,'14009999','최종품',@CompanyName,@BusinessRegistrationNum,@Quantity,@UnitCost,@UpdateHistoryReason, @Date,@Person, @PersonID,'구매납품',@HistoryIndex)";
				
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			
			comm.Parameters.Add("@itemnum", uwgBD_HT.Rows[rowcount].Cells.FromKey("ItemNum").Text);	//품목번호
			comm.Parameters.Add("@itemdrawnum", uwgBD_HT.Rows[rowcount].Cells.FromKey("ItemDrawNum").Text);								//도면번호
			comm.Parameters.Add("@itemname",uwgBD_HT.Rows[rowcount].Cells.FromKey("ItemName").Text);									//품목명
			comm.Parameters.Add("@CompanyName",uwgBD_HT.Rows[rowcount].Cells.FromKey("CompanyName").Text);								//업체명
			comm.Parameters.Add("@BusinessRegistrationNum",uwgBD_HT.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Text);
			//comm.Parameters.Add("@ProcessSequenceNum",uwgBD_HT.Rows[rowcount].Cells.FromKey("ProcessSequenceNum").Text);											//공정순서
			//comm.Parameters.Add("@ProcessCode",uwgBD_HT.Rows[rowcount].Cells.FromKey("ProcessCode").Text);											//공정코드
			//comm.Parameters.Add("@ProcessName",uwgBD_HT.Rows[rowcount].Cells.FromKey("ProcessName").Text);												//공정명
			comm.Parameters.Add("@Quantity",decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString()));//합격수량
			comm.Parameters.Add("@UnitCost",decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString()));//합격수량
			comm.Parameters.Add("@UpdateHistoryReason",hdReason.Value.Trim());												//공정명
			comm.Parameters.Add("@Date", DateTime.Now.ToShortDateString());//외주출고일자
			comm.Parameters.Add("@Person",Session["UserName"].ToString());
			comm.Parameters.Add("@PersonID", Session["ID"].ToString());	
			comm.Parameters.Add("@HistoryIndex",int.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("BuyingDeliveryHistoryIndex").Text));												//원장번호

			comm.ExecuteNonQuery();
		}
		

		/// <summary>
		/// 구매발주원장 입고대기수량 수정
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		private void BO_HTWaitingQuantityUpdate(SqlConnection con,SqlTransaction trans)
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			decimal quantity = decimal.Parse(hdquantity.Value);

			string str =" UPDATE BO_HT SET InStoreWaitingQuantity = InStoreWaitingQuantity - @InStoreWaitingQuantity WHERE BuyingOrderHistoryIndex = @INDEX";
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			comm.CommandText = str;
			comm.Parameters.Add("@InStoreWaitingQuantity", quantity);
			comm.Parameters.Add("@INDEX",int.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("BuyingOrderHistoryIndex").Value.ToString()));
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			str =" UPDATE BO_HT SET InStoreWaitingQuantity = InStoreWaitingQuantity + @InStoreWaitingQuantity WHERE BuyingOrderHistoryIndex = @INDEX";
			comm.CommandText = str;
			comm.Parameters.Add("@InStoreWaitingQuantity", SqlDbType.Decimal).Value = decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());
			comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = int.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("BuyingOrderHistoryIndex").Value.ToString());
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
						
			int idx = int.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("BuyingDeliveryHistoryIndex").Value.ToString());
			int idx1 = 0;

			if(uwgBD_HT.Rows[rowcount].Cells.FromKey("BuyingDeliveryRequestHistoryIndex").Value == null)
			{
				idx1 = int.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("BuyingOrderHistoryIndex").Value.ToString());
				Non_QI_HTUpdatd(con,trans,idx,idx1,"구매발주");
			}
			else
			{
				idx1 = int.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("BuyingDeliveryRequestHistoryIndex").Value.ToString());
				Non_QI_HTUpdatd(con,trans,idx,idx1,"구매납품의뢰");
			}		
		}


		private void NonHistory(SqlConnection con,SqlTransaction trans)
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			string str=" UPDATE SH_HT SET Quantity = @Quantity, [Date] = @date WHERE HistoryDivision= '구매납품' and HistoryIndex = @index ";
			SqlCommand comm = new SqlCommand();	
			comm.Connection = con;			
			comm.Transaction = trans;
			comm.Parameters.Add("@date", DateTime.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryDate").Value.ToString()).ToShortDateString());
			comm.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("BuyingOrderHistoryIndex").Value.ToString());
			comm.CommandText = str;
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();			
		}


		private void Non_QI_HTUpdatd(SqlConnection conn, SqlTransaction tr, int index, int index1, string section)
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			string Condition="";
			SqlCommand comm = new SqlCommand();			
			string str  = @"Select * From QI_HT WHERE HistorySection1= '구매납품' and HistoryIndex1 = @index and HistorySection2= @section and HistoryIndex2 = @index1";
			comm.Connection = conn;			
			comm.Transaction = tr;
			comm.Parameters.Add("@index",SqlDbType.Int).Value = index;
			comm.Parameters.Add("@index1",SqlDbType.Int).Value = index1;
			comm.Parameters.Add("@section",SqlDbType.VarChar).Value = section;
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
				str =" UPDATE QI_HT SET [Year] = @year, [Month] = @mon, RequestQuantity = @quantity, SuitabilityQuantity=@quantity,ApplyUnitCost = @cost, QualityInspectionCompleteDate = @QualityInspectionCompleteDate WHERE HistorySection1= '구매납품' and HistoryIndex1 = @index and HistorySection2= @section and HistoryIndex2 = @index1";
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());
				comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString());
				comm.Parameters.Add("@QualityInspectionCompleteDate", DateTime.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).ToShortDateString());
				comm.Parameters.Add("@index",SqlDbType.Int).Value = index;
				comm.Parameters.Add("@index1",SqlDbType.Int).Value = index1;
				comm.Parameters.Add("@section",SqlDbType.VarChar).Value = section;
				comm.Parameters.Add("@year",SqlDbType.Int).Value = int.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("Year").Value.ToString());
				comm.Parameters.Add("@mon",SqlDbType.Int).Value = int.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("Month").Value.ToString());
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}


		}

		//구매납품원장 수정하는 함수
		private void BD_HT_Update(SqlConnection con,SqlTransaction trans)
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			
			//기존 품질검사원장에서 납품수량을 증가
//			string str1 =" UPDATE BD_HT SET DeliveryQuantity = @quantity , TotalCost = @cost, LotNum = @LotNum, UpdatingPerson = @person, UpdatingPersonID = @id, UpdatingDate = @date WHERE BuyingDeliveryHistoryIndex= @INDEX";
			string str1 =" UPDATE BD_HT SET [Year] = @year, [Month] = @mon,DeliveryQuantity = @quantity , DeliveryDate = @DeliveryDate,  ApplyUnitCost = @cost, TotalCost = @totalcost,UpdatingPerson = @person, UpdatingPersonID = @id, UpdatingDate = @date WHERE BuyingDeliveryHistoryIndex= @INDEX";
			SqlCommand comm1 = new SqlCommand(str1,con);
			comm1.Transaction = trans;
			
		    comm1.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());
			comm1.Parameters.Add("@DeliveryDate",DateTime.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).ToShortDateString());
			comm1.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString());
			comm1.Parameters.Add("@totalcost", SqlDbType.Decimal).Value = decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString()) * decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());
			//comm1.Parameters.Add("@LotNum", SqlDbType.VarChar).Value = uwgBD_HT.Rows[rowcount].Cells.FromKey("@LotNum").Value.ToString();
			comm1.Parameters.Add("@INDEX",SqlDbType.Int).Value = int.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("BuyingDeliveryHistoryIndex").Value.ToString());
			comm1.Parameters.Add("@person",SqlDbType.VarChar).Value = Session["UserName"].ToString();
            comm1.Parameters.Add("@id",SqlDbType.VarChar).Value = Session["ID"].ToString();
			comm1.Parameters.Add("@date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm1.Parameters.Add("@year",SqlDbType.Int).Value = int.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("Year").Value.ToString());
			comm1.Parameters.Add("@mon",SqlDbType.Int).Value = int.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("Month").Value.ToString());
			comm1.ExecuteNonQuery();
		}

		//구매발주원장 수정하는 함수
		private void BO_HT_Update(SqlConnection con,SqlTransaction trans)
		{
			//기존 구매발주원장에서 납입수량,잔량,진행상태를 Update			
			int rowcount = int.Parse(hdRowIndex.Value);
			int index= int.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("BuyingOrderHistoryIndex").Value.ToString());
			
			string str1 = "select RemainQuantity from BO_HT where BuyingOrderHistoryIndex= @INDEX";
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;			
			comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = index;
			comm.CommandText = str1;
			decimal requantity = decimal.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();

			string str =" UPDATE BO_HT SET RemainQuantity = @requantity WHERE BuyingOrderHistoryIndex= @INDEX";
			//발주원장의 잔량에 기존잔량값 + 기존납품량 - 수정후납품량 을 넣는다.
			comm.Parameters.Add("@requantity", SqlDbType.Decimal).Value = requantity + (decimal.Parse(hdquantity.Value.ToString()))-(decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString()));
			comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = index;
			comm.CommandText = str;
			comm.ExecuteNonQuery();	
			comm.Parameters.Clear();
			
			//기존잔량값 + 기존납품량 - 수정후납품량
			if(requantity + (decimal.Parse(hdquantity.Value.ToString()))-(decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString())) == 0)
			{				
				str =" UPDATE BO_HT SET ProgressCondition = @progress WHERE BuyingOrderHistoryIndex= @INDEX";
				comm.Parameters.Add("@progress",SqlDbType.VarChar).Value = "완료";
				comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = index;
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
			else
			{			
				str =" UPDATE BO_HT SET ProgressCondition = @progress WHERE BuyingOrderHistoryIndex= @INDEX";
				comm.Parameters.Add("@progress",SqlDbType.VarChar).Value = "진행";
				comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = index;
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
		}

		//원자재창고 수량 수정하는 함수
		private void RMS_MT_Update(SqlConnection con,SqlTransaction trans)
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			//품목정보테이블에서 현제 수정하고자하는 품목의 단가를 가지고있는 변수
			decimal unitcost = decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Text);
			
			//기존테이블에서 이전수량을 마이너스 증가
			int year = int.Parse(hdOldYear.Value);
			int mon = int.Parse(hdOldMonth.Value);
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -decimal.Parse(hdOldCost.Value);
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(hdquantity.Value.ToString()));
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = uwgBD_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@year",year);

			Table table = new Table(year,mon);
			comm.CommandText = table.InRowTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//새로변경되는 수정한 수량을 증가
			year = DateTime.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Year;
			mon = DateTime.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Month;
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = unitcost*decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = uwgBD_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@year",year);
			Table table1 = new Table(year,mon);
			comm.CommandText = table1.InRowTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14000000' and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  uwgBD_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
				comm.Parameters.Clear();
						
				string StockQuantity = "False";
				string ItemName = uwgBD_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
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

		//매입매출원장의 금액 수정하는 함수
		private void BSI_MT_Update(SqlConnection con,SqlTransaction trans)
		{
			//기존테이블에서 이전물품액을 마이너스 증가
			int rowcount = int.Parse(hdRowIndex.Value);
			//int year = int.Parse(hdOldYear.Value);
			//int mon = int.Parse(hdOldMonth.Value);
			int year = int.Parse(hdyear.Value);
			int mon = int.Parse(hdmon.Value);

			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = uwgBD_HT.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(hdOldCost.Value));
			comm.Parameters.Add("@year",year);

			Table table = new Table(year,mon);
			comm.CommandText = table.PaymentIncreaseTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			year = int.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("Year").Value.ToString());
			mon = int.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("Month").Value.ToString());
			Table table1 = new Table(year,mon);
			//새로변경되는 물품액 증가
			comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = uwgBD_HT.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = (decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString()))*(decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString()));// 물품금액
			comm.Parameters.Add("@year",year);
			comm.CommandText = table1.PaymentIncreaseTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}

		//매입원장의 금액과 수량 수정하는 함수
		private void B_HT_Update(SqlConnection con,SqlTransaction trans)
		{
			int rowcount = int.Parse(hdRowIndex.Value);

			//새로 변경되는 품질검사원장에서 납품수량과 금액을 Update
			string str1 =" UPDATE B_HT SET BuyingQuantity = @quantity,TotalCost = @cost WHERE HistorySection= '구매납품' and HistoryIndex = @INDEX";
			SqlCommand comm1 = new SqlCommand(str1,con);
			comm1.Transaction = trans;
			comm1.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());
			comm1.Parameters.Add("@cost", SqlDbType.Decimal).Value = ((decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString()))*(decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString())));
			comm1.Parameters.Add("@INDEX",SqlDbType.Int).Value = int.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("BuyingDeliveryHistoryIndex").Value.ToString());
			comm1.ExecuteNonQuery();
		}

		//생산창고의 수량을 수정하는 함수
		private void PS_MT_Update(SqlConnection con,SqlTransaction trans)
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			//품목정보테이블에서 현제 수정하고자하는 품목의 단가를 가지고있는 변수
			decimal unitcost = decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Text);
			
			//기존테이블에서 이전수량을 마이너스 증가
		
			int year = int.Parse(hdOldYear.Value);
			int mon = int.Parse(hdOldMonth.Value);
			comm.Parameters.Add("@cost", SqlDbType.VarChar).Value = -decimal.Parse(hdOldCost.Value);
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(hdquantity.Value.ToString()));
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = uwgBD_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@code", SqlDbType.VarChar).Value = "14009999";
			comm.Parameters.Add("@year",year);

			Table table = new Table(year,mon);
			
			comm.CommandText = table.OutProductionTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			Table table1 = new Table(DateTime.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Year, DateTime.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Month);
			//새로변경되는 수정한 수량을 증가
			comm.Parameters.Add("@cost", SqlDbType.VarChar).Value = unitcost*decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = uwgBD_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@code", SqlDbType.VarChar).Value = "14009999";
			comm.Parameters.Add("@year",year);

			comm.CommandText = table1.OutProductionTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM PS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14009999' and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  uwgBD_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
				comm.Parameters.Clear();
						
				string StockQuantity = "False";
				string ItemName = uwgBD_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
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
		private void ISR_HT_Update(SqlConnection con,SqlTransaction trans)
		{
			//기존 품질검사원장에서 납품수량과 금액을 Update
			int rowcount = int.Parse(hdRowIndex.Value);

			string str =" UPDATE ISR_HT SET InstorehouseRequestQuantity = @quantity WHERE ItemNum = @num";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;		
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(uwgBD_HT.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = uwgBD_HT.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			
			comm.ExecuteNonQuery();
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

			if(year < int.Parse(uwgBD_HT.Rows[count].Cells.FromKey("Year").Text))
				return true;
			else if(year == int.Parse(uwgBD_HT.Rows[count].Cells.FromKey("Year").Text))
			{
				if(month < int.Parse(uwgBD_HT.Rows[count].Cells.FromKey("Month").Text))
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
