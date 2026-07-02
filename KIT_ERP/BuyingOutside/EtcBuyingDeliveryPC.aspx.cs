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
using System.Text;


namespace KIT_ERP.BuyingOutside
{
	/// <summary>
	/// EtcBuyingDeliveryPC에 대한 요약 설명입니다.
	/// </summary>
	public class EtcBuyingDeliveryPC : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnDelete;
		protected System.Web.UI.WebControls.LinkButton lkbtnUpdate;
		protected System.Web.UI.WebControls.Button btnExcel;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.TextBox txtItemName;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wcDeliveryDate;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdExistingQuantity;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wcDeliveryDate1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdUpdateRegion;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdApplyUnitCost;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdYear;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdMon;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdOldYear;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdOldMon;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;


		/// <summary>
		/// 편집모드에서 기타구매사유 바인딩시키는 데이터셋을 리턴
		/// </summary>
		/// <returns></returns>
		protected DataSet EtcStore
		{
			get
			{
				KIT_ERP.Update Etc = new KIT_ERP.Update(Session["ID"].ToString());
				DataSet ds_Reson = Etc.EtcBuy();
				
				return ds_Reson;
			}
		}
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			CSC1.UnitCostDistinction = "구매거래처";

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			if ( !IsPostBack )
			{
				PageLoad_Bind();
			}

//			// true 가 반환되면 등록가능
//			if ( MonthClosing() )
//			{
//				Response.Write("<script>var MonthCloseing = \"0\"; </script>");	// 전역변수에 "0" 을 할당
//			}
//			else
//			{
//				// 월마감 되었음. - 등록 불가
//				Response.Write("<script>var MonthCloseing = \"1\"; </script>");	// 전역변수에 "1" 을 할당
//			}
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
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
			this.lkbtnUpdate.Click += new System.EventHandler(this.lkbtnUpdate_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void PageLoad_Bind()
		{
			btnDelete.Attributes.Add("onclick", " return DeleteCheck(); ");
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			string[] strArrValue = new string[5];
			strArrValue[0] = txtItemName.Text;
			strArrValue[1] = CSC1.Company;
			strArrValue[2] = CSC1.BusinessRegistrationNum;
			strArrValue[3] = wcDeliveryDate.Text;
			strArrValue[4] = wcDeliveryDate1.Text;

			this.DataBind(strArrValue);
		}

		private void DataBind(string[] strArrValue)
		{
			SqlDataAdapter adap = new SqlDataAdapter("EtcBuyingDeliveryPC_Search", ConfigurationSettings.AppSettings["DSN"]);
			adap.SelectCommand.Parameters.Add("@ItemName", SqlDbType.VarChar).Value = strArrValue[0];
			adap.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = strArrValue[1];
			adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum", SqlDbType.VarChar).Value = strArrValue[2];
			adap.SelectCommand.Parameters.Add("@DeliveryDemandDate", SqlDbType.VarChar).Value = strArrValue[3] ;
			adap.SelectCommand.Parameters.Add("@DeliveryDemandDate1", SqlDbType.VarChar).Value = strArrValue[4] ;

			
			adap.SelectCommand.CommandType = CommandType.StoredProcedure;
			DataSet ds = new DataSet();
			adap.Fill(ds);

			UltraWebGrid1.DataSource = ds.Tables[0].DefaultView;
			UltraWebGrid1.DataBind();
		}

		

		// 수정
		private void lkbtnUpdate_Click(object sender, System.EventArgs e)
		{
			
			if(MonthClosing())
			{
				
				string[] strArrUdValue = new string[18];
				// 현재 수정된 행의 번호(DB 인덱스 번호가 아님)
				int i = int.Parse(hdIndex.Value);

				strArrUdValue[0] = UltraWebGrid1.Rows[i].Cells.FromKey("BusinessRegistrationNum").Value.ToString();			// 거래처등록번호
				strArrUdValue[1] = UltraWebGrid1.Rows[i].Cells.FromKey("OrderQuantity").Value.ToString();						// 발주수량
				strArrUdValue[2] = hdExistingQuantity.Value;																	// 기존납품수량
				strArrUdValue[3] = UltraWebGrid1.Rows[i].Cells.FromKey("DeliveryQuantity").Value.ToString();					// 수정된 수량
				strArrUdValue[4] = UltraWebGrid1.Rows[i].Cells.FromKey("ApplyUnitCost").Value.ToString();						// 개별단가
				strArrUdValue[5] = Session["ID"].ToString();																	// 수정자 ID
				strArrUdValue[6] = UltraWebGrid1.Rows[i].Cells.FromKey("EtcBuyingDeliveryHistoryIndex").Value.ToString();		// 납품원장 인덱스
				strArrUdValue[7] = UltraWebGrid1.Rows[i].Cells.FromKey("EtcBuyingOrderHistoryIndex").Value.ToString();		// 발주원장 인덱스
				strArrUdValue[8] = Convert.ToDateTime(UltraWebGrid1.Rows[i].Cells.FromKey("DeliveryDemandDate").Text).ToString();		// 발주원장 인덱스
				strArrUdValue[9] = hdUpdateRegion.Value;		//수정사유
				strArrUdValue[10] = UltraWebGrid1.Rows[i].Cells.FromKey("CompanyName").Text;		//수정사유
				strArrUdValue[11] = UltraWebGrid1.Rows[i].Cells.FromKey("ItemName").Text;		//수정사유
				strArrUdValue[12] = UltraWebGrid1.Rows[i].Cells.FromKey("Year").Text;		//수정매입년도
				strArrUdValue[13] = UltraWebGrid1.Rows[i].Cells.FromKey("Month").Text;		//수정매입월
				strArrUdValue[14] = hdOldYear.Value;
				strArrUdValue[15] = hdOldMon.Value;
				strArrUdValue[16] = UltraWebGrid1.Rows[i].Cells.FromKey("ReasonCode").Value.ToString();
				strArrUdValue[17] = UltraWebGrid1.Rows[i].Cells.FromKey("ReasonName").Value.ToString();



				DB_Update(strArrUdValue) ;
				Response.Write("<script>alert('수정 되었습니다.')</script>");
			}
			else
			{
				RegisterStartupScript("","<script>alert('월마감이 되어 수정이 불가능 합니다!');</script>");
			}

			

			btnSearch_Click(sender, e);
		}

		private void DB_Update(string[] strArrUdValue)
		{
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand cmd = new SqlCommand("EtcBuyingDelivery_Update", con);
			cmd.CommandType = CommandType.StoredProcedure;
			SqlTransaction tran = null;

			cmd.Parameters.Add("@BusinessRegNum", SqlDbType.VarChar).Value = strArrUdValue[0];
			cmd.Parameters.Add("@OrderQuantity", SqlDbType.Decimal).Value = strArrUdValue[1];
			cmd.Parameters.Add("@ExistingQuantity", SqlDbType.Decimal).Value = strArrUdValue[2];
			cmd.Parameters.Add("@DeliveryQuantity", SqlDbType.Decimal).Value = strArrUdValue[3];
			cmd.Parameters.Add("@ApplyUnitCost", SqlDbType.Decimal).Value = strArrUdValue[4];
			cmd.Parameters.Add("@UpdatingPersonID", SqlDbType.VarChar).Value = strArrUdValue[5];
			cmd.Parameters.Add("@EtcBuyingDeliveryHistoryIndex", SqlDbType.Int).Value = strArrUdValue[6];
			cmd.Parameters.Add("@EtcBuyingOrderHistoryIndex", SqlDbType.Int).Value = strArrUdValue[7];
			cmd.Parameters.Add("@DeliveryDemandDate", SqlDbType.SmallDateTime).Value = strArrUdValue[8];
			cmd.Parameters.Add("@UpdateRegion",strArrUdValue[9]);
			cmd.Parameters.Add("@CompanyName",strArrUdValue[10]);
			cmd.Parameters.Add("@ItemNum",strArrUdValue[11]);
			cmd.Parameters.Add("@DeliveryYear",strArrUdValue[12]);
			cmd.Parameters.Add("@DeliveryMon",strArrUdValue[13]);
			cmd.Parameters.Add("@OldYear",strArrUdValue[14]);
			cmd.Parameters.Add("@OldMon",strArrUdValue[15]);
			cmd.Parameters.Add("@ReasonCode",strArrUdValue[16]);
			cmd.Parameters.Add("@ReasonName",strArrUdValue[17]);



			try
			{
				con.Open();
				tran = con.BeginTransaction();
				cmd.Transaction = tran;

				cmd.ExecuteNonQuery();
				tran.Commit();
			}
			catch(Exception ex)
			{
				Response.Write("<script>alert(\"" + ex.Message + "\")</script>");
				tran.Rollback();
			}
			finally
			{
				con.Close();
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			
			if (UltraWebGrid1.Rows.Count == 0)
			{
				Response.Write("<script>alert('삭제할 품목이 없습니다.');</script>");
			}
			else
			{
				
				bool bRowDelete = false;
				bool test = false;

				for( int i = 0 ; i < UltraWebGrid1.Rows.Count ; i ++)
				{
					if (Convert.ToBoolean( UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value ))
					{
						if(MonthClosing(i))
						{
							string[] strArrUdValue = new string[13];
							strArrUdValue[0] = UltraWebGrid1.Rows[i].Cells.FromKey("BusinessRegistrationNum").Value.ToString();			// 거래처등록번호
							strArrUdValue[1] = UltraWebGrid1.Rows[i].Cells.FromKey("OrderQuantity").Value.ToString();						// 발주수량
							strArrUdValue[2] = UltraWebGrid1.Rows[i].Cells.FromKey("DeliveryQuantity").Value.ToString();																					// 수정된 수량
							strArrUdValue[3] = UltraWebGrid1.Rows[i].Cells.FromKey("TotalCost").Value.ToString();						// 납품 총 금액
							strArrUdValue[4] = Session["ID"].ToString();																	// 수정자 ID
							strArrUdValue[5] = UltraWebGrid1.Rows[i].Cells.FromKey("EtcBuyingDeliveryHistoryIndex").Value.ToString();		// 납품원장 인덱스
							strArrUdValue[6] = UltraWebGrid1.Rows[i].Cells.FromKey("EtcBuyingOrderHistoryIndex").Value.ToString();		// 발주원장 인덱스
							strArrUdValue[7] = Convert.ToDateTime(UltraWebGrid1.Rows[i].Cells.FromKey("DeliveryDemandDate").Value).ToString();		// 납품일자
							strArrUdValue[8] = UltraWebGrid1.Rows[i].Cells.FromKey("CompanyName").Value.ToString();			// 거래처등록번호
							strArrUdValue[9] = UltraWebGrid1.Rows[i].Cells.FromKey("ItemName").Value.ToString();			// 거래처등록번호
							strArrUdValue[10] = UltraWebGrid1.Rows[i].Cells.FromKey("ApplyUnitCost").Value.ToString();			// 거래처등록번호
							strArrUdValue[11] = UltraWebGrid1.Rows[i].Cells.FromKey("Year").Value.ToString();			// 거래처등록번호
							strArrUdValue[12] = UltraWebGrid1.Rows[i].Cells.FromKey("Month").Value.ToString();	

							int mon = Convert.ToDateTime(UltraWebGrid1.Rows[i].Cells.FromKey("DeliveryDemandDate").Text).Month;
							DB_Delete( strArrUdValue ,mon) ;

							bRowDelete = true;
						}
						else
						{
							bRowDelete = false;
							test = true;
							RegisterStartupScript("","<script>alert('월마감이 되어 삭제가 불가능 합니다!');</script>");
							break;
						}
						
					}
				}
				
				if ( bRowDelete )
				{
					if(!test)
						Response.Write("<script>alert('삭제 되었습니다.')</script>");
				}
				else
					Response.Write("<script>alert('선택된 행이 없습니다.')</script>");	
				
			}

			btnSearch_Click(sender, e);
			
		}
				

		private void DB_Delete( string[] strArrUdValue, int month)
		{
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "EtcBuyingDeliveryPC_Delete";
			SqlCommand cmd = new SqlCommand();
			cmd.Connection = con;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = str;
			SqlTransaction tran = null;

			cmd.Parameters.Add("@BusinessRegNum", SqlDbType.VarChar).Value = strArrUdValue[0];
			cmd.Parameters.Add("@OrderQuantity", SqlDbType.Decimal).Value = strArrUdValue[1];
			cmd.Parameters.Add("@DeliveryQuantity", SqlDbType.Decimal).Value = strArrUdValue[2];
			cmd.Parameters.Add("@TotalCost", SqlDbType.Decimal).Value = strArrUdValue[3];
			cmd.Parameters.Add("@UpdatingPersonID", SqlDbType.VarChar).Value = strArrUdValue[4];
			cmd.Parameters.Add("@EtcBuyingDeliveryHistoryIndex", SqlDbType.Int).Value = strArrUdValue[5];
			cmd.Parameters.Add("@EtcBuyingOrderHistoryIndex", SqlDbType.Int).Value = strArrUdValue[6];
			cmd.Parameters.Add("@DeliveryDemandDate", SqlDbType.SmallDateTime).Value = strArrUdValue[7];
			cmd.Parameters.Add("@CompanyName",strArrUdValue[8]);
			cmd.Parameters.Add("@ItemNum",strArrUdValue[9]);
			cmd.Parameters.Add("@ApplyUnitCost",strArrUdValue[10]);
			cmd.Parameters.Add("@OldYear",strArrUdValue[11]);
			cmd.Parameters.Add("@OldMon",strArrUdValue[12]);


			try
			{
				con.Open();
				tran = con.BeginTransaction();
				cmd.Transaction = tran;

				cmd.ExecuteNonQuery();
				
				
				tran.Commit();
			}
			catch(Exception ex)
			{
				Response.Write("<script>alert(\"" + ex.Message + "\")</script>");
				tran.Rollback();
			}
			finally
			{
				con.Close();
			}
		}

		// Excel
		private void btnExcel_Click(object sender, System.EventArgs e)
		{
			
			SqlDataAdapter adap = new SqlDataAdapter("EtcBuyingDeliveryPC_Search", ConfigurationSettings.AppSettings["DSN"]);
			adap.SelectCommand.Parameters.Add("@ItemName", SqlDbType.VarChar).Value = txtItemName.Text;
			adap.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = CSC1.Company;
			adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum", SqlDbType.VarChar).Value = CSC1.BusinessRegistrationNum;
			adap.SelectCommand.Parameters.Add("@DeliveryDemandDate", SqlDbType.VarChar).Value = wcDeliveryDate.Text;
			adap.SelectCommand.Parameters.Add("@DeliveryDemandDate1", SqlDbType.VarChar).Value = wcDeliveryDate1.Text ;

			adap.SelectCommand.CommandType = CommandType.StoredProcedure;
			DataSet ds = new DataSet();
			adap.Fill(ds);


			UltraWebGrid1.DisplayLayout.Pager.AllowPaging = false;
			UltraWebGrid1.Columns.FromKey("chk").Hidden = true;
			UltraWebGrid1.DataSource = ds.Tables[0].DefaultView;
			UltraWebGrid1.DataBind();
			UltraWebGridExcelExporter1.Export(UltraWebGrid1);
		}

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;

			SqlDataAdapter adap = new SqlDataAdapter("EtcBuyingDeliveryPC_Search", ConfigurationSettings.AppSettings["DSN"]);
			adap.SelectCommand.Parameters.Add("@ItemName", SqlDbType.VarChar).Value = txtItemName.Text;
			adap.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = CSC1.Company;
			adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum", SqlDbType.VarChar).Value = CSC1.BusinessRegistrationNum;
			adap.SelectCommand.Parameters.Add("@DeliveryDemandDate", SqlDbType.VarChar).Value = wcDeliveryDate.Text;
			adap.SelectCommand.Parameters.Add("@DeliveryDemandDate1", SqlDbType.VarChar).Value = wcDeliveryDate1.Text ;

			adap.SelectCommand.CommandType = CommandType.StoredProcedure;
			DataSet ds = new DataSet();
			adap.Fill(ds);

			UltraWebGrid1.DataSource = ds.Tables[0].DefaultView;
			UltraWebGrid1.DataBind();
		}

		/// <summary>
		/// 월마감 여부를 확인하는 함수
		/// </summary>
		/// <returns></returns>
		private bool MonthClosing()
		{
			int count = int.Parse(hdIndex.Value);
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

			
			
			if(year < int.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("Year").Text))
				return true;
			else if(year == int.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("Year").Text))
			{
				if(month < int.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("Month").Text))
					return true;
				else
					return false;
			}
			else
			{
				return false;
			}		
		}

		/// <summary>
		/// 월마감 여부를 확인하는 함수
		/// </summary>
		/// <returns></returns>
		private bool MonthClosing(int count)
		{
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

			
			if(year < int.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("Year").Text))
			{
				return true;
			}
			else if(year == int.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("Year").Text)) 
			{
				if(month < int.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("Month").Text))
					return true;
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
			
//			if(year <= int.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("Year").Text))
//			{
//				if(month < int.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("Month").Text))
//					return true;
//				else
//					return false;
//			}
//			else
//			{
//				return false;
//			}		
		}

	}
}
