using System;
using System.Configuration;
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
using Infragistics.WebUI.UltraWebGrid;
using Infragistics.WebUI;
using System.Text;


namespace KIT_ERP.BuyingOutside
{
	/// <summary>
	/// EtcBuyingOrderPC에 대한 요약 설명입니다.
	/// </summary>
	public class EtcBuyingOrderPC : System.Web.UI.Page
	{
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wcDeliveryDate;
		protected System.Web.UI.WebControls.TextBox txtItemName;
		protected System.Web.UI.WebControls.DropDownList ddlState;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdIndex;
		protected System.Web.UI.WebControls.Button btnExcel;
		protected System.Web.UI.WebControls.LinkButton lkbtnUpdate;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wcDeliveryDate1;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Button btnDelete;
		protected System.Web.UI.WebControls.Button Button1;
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
				Response.Write("<script>var MonthCloseing = \"0\"; </script>");	// 전역변수에 "0" 을 할당
			}

			// 월마감을 체크하여 등록, 수정, 삭제 여부를 클라이언트 단에서 체크하기 위해 JavaScript 전역변수값을 출력 
			// true 가 반환되면 등록가능
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.lkbtnUpdate.Click += new System.EventHandler(this.lkbtnUpdate_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void PageLoad_Bind()
		{
			btnDelete.Attributes.Add("onclick", " return DeleteCheck(); ");
		}



		#region 검색버튼 클릭시 이벤트 핸들러와 관련 메서드
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex =1;
			string[] strArrValue = new string[6];
			strArrValue[0] = txtItemName.Text;
			strArrValue[1] = CSC1.Company;
			strArrValue[2] = CSC1.BusinessRegistrationNum;
			strArrValue[3] = wcDeliveryDate.Text;
			strArrValue[4] = wcDeliveryDate1.Text;
			strArrValue[5] = ddlState.SelectedItem.Value;

			this.DataBind(strArrValue);
		}
		private void DataBind(string[] strArrValue)
		{
			SqlDataAdapter adap = new SqlDataAdapter("EtcBuyingOrderPC_Search", ConfigurationSettings.AppSettings["DSN"]);
			adap.SelectCommand.Parameters.Add("@ItemName", SqlDbType.VarChar).Value = strArrValue[0];
			adap.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = strArrValue[1];
			adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum", SqlDbType.VarChar).Value = strArrValue[2];
			adap.SelectCommand.Parameters.Add("@DeliveryDemandDate", SqlDbType.VarChar).Value = strArrValue[3] ;
			adap.SelectCommand.Parameters.Add("@DeliveryDemandDate1", SqlDbType.VarChar).Value = strArrValue[4] ;
			adap.SelectCommand.Parameters.Add("@RecodingState", SqlDbType.VarChar).Value = strArrValue[5];

			adap.SelectCommand.CommandType = CommandType.StoredProcedure;
			DataSet ds = new DataSet();
			adap.Fill(ds);

			UltraWebGrid1.DataSource = ds.Tables[0].DefaultView;
			UltraWebGrid1.DataBind();
		}
		#endregion



		// 삭제버튼
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			if (UltraWebGrid1.Rows.Count == 0)
			{
				Response.Write("<script>alert('삭제할 품목이 없습니다.');</script>");
			}
			else
			{
				// 시작 크기 6 의 StringBuilder 객체 생성.
				StringBuilder sb = new StringBuilder(6);
				bool test = false;

				for( int i = 0 ; i < UltraWebGrid1.Rows.Count ; i ++)
				{
					if(MonthClosing1(i))
					{
						if (Convert.ToBoolean( UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value ))
						{
							sb.Append (UltraWebGrid1.Rows[i].Cells.FromKey("EtcBuyingOrderHistoryIndex").Text );
							sb.Append(",");
						}
					}
					else
					{
						test = true;
						RegisterStartupScript("","<script>alert('월마감이 되어 삭제가 불가능 합니다!');</script>");
						break;
					}

				}
				
				if(!test)
				{
					if ( sb.Length > 0 )
					{
						// SQL 쿼리문의 in 절을 사용하기 위해 마지막 콤마 제거
						string strDeleteIndex = sb.ToString().Substring(0, sb.ToString().Length - 1);
				
						DB_Delete( strDeleteIndex );
						Response.Write("<script>alert('삭제 되었습니다.')</script>");
						btnSearch_Click(sender, e);
					}
					else
					{
						Response.Write("<script>alert('선택된 행이 없습니다.')</script>");
					}
				}
			}
		}


		// 물리적인 DB에 삭제를 수행하는 메서드
		private void DB_Delete( string strDeleteIndex )
		{
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand cmd = new SqlCommand("EtcBuyingOrderPC_Delete", con);
			SqlTransaction tran = null;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@Delindex", SqlDbType.VarChar).Value = strDeleteIndex;

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



		// 성공적으로 수정되었으면 true 값을 리턴
		private bool DB_Update(string[] strArrUdValue)
		{	
			bool bResult = false;
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand cmd = new SqlCommand("EtcBuyingOrderPC_Update", con);
			SqlTransaction tran = null;
			cmd.CommandType = CommandType.StoredProcedure;
		
			cmd.Parameters.Add("@itemNum", SqlDbType.VarChar).Value = strArrUdValue[0];
			cmd.Parameters.Add("@ApplyUnitCost", SqlDbType.Decimal).Value = strArrUdValue[1];
			cmd.Parameters.Add("@OrderQuantity", SqlDbType.Decimal).Value = strArrUdValue[2];
			cmd.Parameters.Add("@TotalCost", SqlDbType.Decimal).Value = strArrUdValue[3];
			cmd.Parameters.Add("@DeliveryDemandDate", SqlDbType.SmallDateTime).Value = strArrUdValue[4];
			cmd.Parameters.Add("@UpdatePersonID", SqlDbType.VarChar).Value = strArrUdValue[5];
			cmd.Parameters.Add("@EtcBuyingOrderHistoryIndex", SqlDbType.Int).Value = strArrUdValue[6];
			cmd.Parameters.Add("@ReasonName", SqlDbType.VarChar).Value = strArrUdValue[7];
			cmd.Parameters.Add("@ReasonCode", SqlDbType.VarChar).Value = strArrUdValue[8];

			try
			{
				con.Open();
				tran = con.BeginTransaction();
				cmd.Transaction = tran;

				cmd.ExecuteNonQuery();
				tran.Commit();
				bResult = true;
			}
			catch(Exception ex)
			{
				Response.Write("<script>alert(\"" + ex.Message + "\")</script>");
				tran.Rollback();
				bResult = false;;
			}
			finally
			{
				con.Close();
			}

			return bResult;
		}



		// Row Template 의 수정버튼 클릭시 발생하는 __doPostBack 이벤트
		private void lkbtnUpdate_Click(object sender, System.EventArgs e)
		{
			if(MonthClosing())
			{
				string[] strArrUdValue = new string[9];
				// 현재 수정된 행의 번호(DB 인덱스 번호가 아님)
				int i = int.Parse(hdIndex.Value);
			

				strArrUdValue[0] = UltraWebGrid1.Rows[i].Cells.FromKey("ItemName").Value.ToString();
				strArrUdValue[1] = UltraWebGrid1.Rows[i].Cells.FromKey("ApplyUnitCost").Value.ToString();
				strArrUdValue[2] = UltraWebGrid1.Rows[i].Cells.FromKey("OrderQuantity").Value.ToString();
				strArrUdValue[3] = UltraWebGrid1.Rows[i].Cells.FromKey("TotalCost").Value.ToString();
				strArrUdValue[4] = UltraWebGrid1.Rows[i].Cells.FromKey("DeliveryDemandDate").Value.ToString();	
				strArrUdValue[5] = Session["ID"].ToString();
				strArrUdValue[6] = UltraWebGrid1.Rows[i].Cells.FromKey("EtcBuyingOrderHistoryIndex").Value.ToString();
				strArrUdValue[7] = UltraWebGrid1.Rows[i].Cells.FromKey("ReasonName").Value.ToString();
				strArrUdValue[8] = UltraWebGrid1.Rows[i].Cells.FromKey("ReasonCode").Value.ToString();

				if ( DB_Update(strArrUdValue) )
				{
					Response.Write("<script>alert('수정 되었습니다.')</script>");
				}
			}
			else
			{
				RegisterStartupScript("","<script>alert('월마감이 되어 수정이 불가능 합니다!');</script>");
			}

			btnSearch_Click(sender, e);
		}

		

		//Excel
		private void btnExcel_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.AllowPaging = false;
			UltraWebGrid1.Columns.FromKey("chk").Hidden = true;

			SqlDataAdapter adap = new SqlDataAdapter("EtcBuyingOrderPC_Search", ConfigurationSettings.AppSettings["DSN"]);
			adap.SelectCommand.Parameters.Add("@ItemName", SqlDbType.VarChar).Value = txtItemName.Text;
			adap.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = CSC1.Company;
			adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum", SqlDbType.VarChar).Value = CSC1.BusinessRegistrationNum;
			adap.SelectCommand.Parameters.Add("@DeliveryDemandDate", SqlDbType.VarChar).Value = wcDeliveryDate.Text;
			adap.SelectCommand.Parameters.Add("@DeliveryDemandDate1", SqlDbType.VarChar).Value = wcDeliveryDate1.Text;
			adap.SelectCommand.Parameters.Add("@RecodingState", SqlDbType.VarChar).Value =ddlState.SelectedItem.Value;

			adap.SelectCommand.CommandType = CommandType.StoredProcedure;
			DataSet ds = new DataSet();
			adap.Fill(ds);

			UltraWebGrid1.DataSource = ds.Tables[0].DefaultView;
			UltraWebGrid1.DataBind();

			UltraWebGridExcelExporter1.Export(UltraWebGrid1);
		}

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			
			SqlDataAdapter adap = new SqlDataAdapter("EtcBuyingOrderPC_Search", ConfigurationSettings.AppSettings["DSN"]);
			adap.SelectCommand.Parameters.Add("@ItemName", SqlDbType.VarChar).Value = txtItemName.Text;
			adap.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = CSC1.Company;
			adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum", SqlDbType.VarChar).Value = CSC1.BusinessRegistrationNum;
			adap.SelectCommand.Parameters.Add("@DeliveryDemandDate", SqlDbType.VarChar).Value = wcDeliveryDate.Text;
			adap.SelectCommand.Parameters.Add("@DeliveryDemandDate1", SqlDbType.VarChar).Value = wcDeliveryDate1.Text;
			adap.SelectCommand.Parameters.Add("@RecodingState", SqlDbType.VarChar).Value =ddlState.SelectedItem.Value;

			adap.SelectCommand.CommandType = CommandType.StoredProcedure;
			DataSet ds = new DataSet();
			adap.Fill(ds);

			UltraWebGrid1.DataSource = ds.Tables[0].DefaultView;
			UltraWebGrid1.DataBind();
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			Infragistics.WebUI.UltraWebGrid.UltraWebGrid UWG = UltraWebGrid1;
			bool check = true;
			for(int count = UltraWebGrid1.Rows.Count-1 ; count >=0 ; count--)
			{
				if(UWG.Rows[count].Cells.FromKey("chk").Value == null || UWG.Rows[count].Cells.FromKey("chk").Value.ToString() == "false")
					UWG.Rows[count].Delete();
			}

			if(UWG.Rows.Count == 0)
			{
				check = false;
				RegisterStartupScript("","<script>alert('선택한 항목이 없습니다!');</script>");
			}
			else if(UWG.Rows.Count > 12)
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
				RegisterStartupScript("","<script>window.open('./Popup/EtcPurchase.aspx','','width=1000, heigth=800,scrollbars=yes,menubar=yes,status=yes,toolbar=yes,center=yes');</script>");
							
			}
			else
			{
				UltraWebGrid1.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
				UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;


				SqlDataAdapter adap = new SqlDataAdapter("EtcBuyingOrderPC_Search", ConfigurationSettings.AppSettings["DSN"]);
				adap.SelectCommand.Parameters.Add("@ItemName", SqlDbType.VarChar).Value = txtItemName.Text;
				adap.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = CSC1.Company;
				adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum", SqlDbType.VarChar).Value = CSC1.BusinessRegistrationNum;
				adap.SelectCommand.Parameters.Add("@DeliveryDemandDate", SqlDbType.VarChar).Value = wcDeliveryDate.Text;
				adap.SelectCommand.Parameters.Add("@DeliveryDemandDate1", SqlDbType.VarChar).Value = wcDeliveryDate1.Text;
				adap.SelectCommand.Parameters.Add("@RecodingState", SqlDbType.VarChar).Value =ddlState.SelectedItem.Value;

				adap.SelectCommand.CommandType = CommandType.StoredProcedure;
				DataSet ds = new DataSet();
				adap.Fill(ds);

				UltraWebGrid1.DataSource = ds.Tables[0].DefaultView;
				UltraWebGrid1.DataBind();
			}
		}

		/// <summary>
		/// 수정시 월마감 여부를 확인하는 함수
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

			
			if(year < DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryDemandDate").Text).Year)
				return true; 
			else if(year == DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryDemandDate").Text).Year)
			{
				if(month < DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryDemandDate").Text).Month)
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
		/// 삭제시월마감 여부를 확인하는 함수
		/// </summary>
		/// <returns></returns>
		private bool MonthClosing1(int count)
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

		
			if (Convert.ToBoolean( UltraWebGrid1.Rows[count].Cells.FromKey("chk").Value ))
			{
				if(year < DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryDemandDate").Text).Year)
					return true; 
				else if(year == DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryDemandDate").Text).Year)
				{
					if(month < DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("DeliveryDemandDate").Text).Month)
						return true;
					else
						return false;
				}
				else
				{
					return false;
				}	
			}
			else
				return true;
			
		}

	}
}
