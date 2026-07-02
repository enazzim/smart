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
using Infragistics.WebUI.UltraWebGrid;

namespace KIT_ERP.EtcPresentCondition
{
	/// <summary>
	/// CustomerPurchaseTotalMonthResult에 대한 요약 설명입니다.
	/// </summary>
	public class CustomerPurchaseTotalMonthResult : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btComSearch;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button Button1;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected System.Web.UI.WebControls.DropDownList ddlDivision;
		protected System.Web.UI.WebControls.DropDownList ddlYear;
		protected System.Web.UI.WebControls.DropDownList ddlMon;
		protected System.Web.UI.WebControls.DropDownList ddlItemGroup2;
		protected System.Web.UI.WebControls.DropDownList ddlItemGroup1;
		protected System.Web.UI.WebControls.DropDownList ddlItemGroup3;
		protected System.Web.UI.WebControls.DropDownList ddlItemGroup4;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CompanySearchControl1;


		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.Commodity = true;
			ItemSearchControl1.Products = true;
			ItemSearchControl1.RawMaterials = true;
			ItemSearchControl1.Phantom = false;
			ItemSearchControl1.HalfFinishedProducts = true;
			CompanySearchControl1.UnitCostDistinction="구매외주";

			if(!Page.IsPostBack)
			{
				Load_Bind();
			}

		}
		private void Load_Bind()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			//	*************************************************
			//	**  품목분류1 드롭다운리스트... 데이타바인딩... **		
			//	*************************************************
			//코드분류표에서 대분류명이 품목분류1인 소분류명을 가지고 옴.
			string str_ItemClassification1 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0210' order by SmallClassificationName" ;
			SqlCommand comm_ItemClassification1 = new SqlCommand(str_ItemClassification1,conn);
			SqlDataAdapter da_ItemClassification1 = new SqlDataAdapter(comm_ItemClassification1) ;
			DataSet ds_ItemClassification1 = new DataSet() ;
			da_ItemClassification1.Fill(ds_ItemClassification1);
				
			ddlItemGroup1.DataSource = ds_ItemClassification1;
			ddlItemGroup1.DataTextField = ds_ItemClassification1.Tables[0].Columns[0].ToString();
			ddlItemGroup1.DataValueField = ds_ItemClassification1.Tables[0].Columns[1].ToString();
			ddlItemGroup1.DataBind();
			ddlItemGroup1.Items.Insert(0, "-선택하세요-") ;
			ddlItemGroup1.Items[0].Value = "";



			


			//	*************************************************
			//	**  품목분류2 드롭다운리스트... 데이타바인딩... **		
			//	*************************************************
			//코드분류표에서 대분류명이 품목분류2인 소분류명을 가지고 옴.
			string str_ItemClassification2 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0220' order by SmallClassificationName" ;
			SqlCommand comm_ItemClassification2 = new SqlCommand(str_ItemClassification2,conn);
			SqlDataAdapter da_ItemClassification2 = new SqlDataAdapter(comm_ItemClassification2) ;
			DataSet ds_ItemClassification2 = new DataSet() ;
			da_ItemClassification2.Fill(ds_ItemClassification2);
				
			ddlItemGroup2.DataSource = ds_ItemClassification2;
			ddlItemGroup2.DataTextField = ds_ItemClassification2.Tables[0].Columns[0].ToString();
			ddlItemGroup2.DataValueField = ds_ItemClassification2.Tables[0].Columns[1].ToString();
			ddlItemGroup2.DataBind();
			ddlItemGroup2.Items.Insert(0, "-선택하세요-") ;
			ddlItemGroup2.Items[0].Value = "";



			//	*************************************************
			//	**  품목분류3 드롭다운리스트... 데이타바인딩... **		
			//	*************************************************
			//코드분류표에서 대분류명이 품목분류3인 소분류명을 가지고 옴.
			string str_ItemClassification3 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0230' order by SmallClassificationName" ;
			SqlCommand comm_ItemClassification3 = new SqlCommand(str_ItemClassification3,conn);
			SqlDataAdapter da_ItemClassification3 = new SqlDataAdapter(comm_ItemClassification3) ;
			DataSet ds_ItemClassification3 = new DataSet() ;
			da_ItemClassification3.Fill(ds_ItemClassification3);
				
			ddlItemGroup3.DataSource = ds_ItemClassification3;
			ddlItemGroup3.DataTextField = ds_ItemClassification3.Tables[0].Columns[0].ToString();
			ddlItemGroup3.DataValueField = ds_ItemClassification3.Tables[0].Columns[1].ToString();
			ddlItemGroup3.DataBind();
			ddlItemGroup3.Items.Insert(0, "-선택하세요-") ;
			ddlItemGroup3.Items[0].Value = "";

			//	*************************************************
			//	**  품목분류1 드롭다운리스트... 데이타바인딩... **		
			//	*************************************************
			//코드분류표에서 대분류명이 품목분류1인 소분류명을 가지고 옴.
			string str_ItemClassification4 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0240' order by SmallClassificationName" ;
			SqlCommand comm_ItemClassification4 = new SqlCommand(str_ItemClassification4,conn);
			SqlDataAdapter da_ItemClassification4 = new SqlDataAdapter(comm_ItemClassification4) ;
			DataSet ds_ItemClassification4 = new DataSet() ;
			da_ItemClassification4.Fill(ds_ItemClassification4);
				
			ddlItemGroup4.DataSource = ds_ItemClassification4;
			ddlItemGroup4.DataTextField = ds_ItemClassification4.Tables[0].Columns[0].ToString();
			ddlItemGroup4.DataValueField = ds_ItemClassification4.Tables[0].Columns[1].ToString();
			ddlItemGroup4.DataBind();
			ddlItemGroup4.Items.Insert(0, "-선택하세요-") ;
			ddlItemGroup4.Items[0].Value = "";

			for(int a = 0; a<=ddlYear.Items.Count; a++) 
			{
				if(ddlYear.Items[a].Value == DateTime.Now.Year.ToString())
				{
					ddlYear.SelectedIndex = a;
					break;
				}					
			}

			ddlMon.SelectedIndex = DateTime.Now.Month;

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
			this.btComSearch.Click += new System.EventHandler(this.btComSearch_Click);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btComSearch_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
			UltraWebGrid1.Bands[0].Columns.FromKey("CompanyName").MergeCells = true;
		}

		private DataTable Search()
		{
			DataTable dt = new DataTable();
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();
			try
			{
				//string str = "SPCustomerPurchaseTotalMonthResult";
				string str = "SPCustomerPurchaseTotalMonthResult1";
				SqlCommand comm = new SqlCommand();
				comm.Transaction = tr;
				comm.Connection = conn;
				comm.CommandText = str;
				comm.CommandType = CommandType.StoredProcedure;
				comm.Parameters.Add("@ItemNum",ItemSearchControl1.ItemNum);
				if(ItemSearchControl1.hdItem.Trim() == "")
				{
					comm.Parameters.Add("@ItemDrawNum",ItemSearchControl1.ItemDrawNum);
					comm.Parameters.Add("@ItemName",ItemSearchControl1.ItemName);
				}
				else
				{
					comm.Parameters.Add("@ItemDrawNum","");
					comm.Parameters.Add("@ItemName","");
				}
				comm.Parameters.Add("@BusinessRegistrationNum",CompanySearchControl1.BusinessRegistrationNum);
				comm.Parameters.Add("@CompanyName",CompanySearchControl1.Company);
				comm.Parameters.Add("@ItemClassification1",ddlItemGroup1.SelectedValue.Trim());
				comm.Parameters.Add("@ItemClassification2",ddlItemGroup2.SelectedValue.Trim());
				comm.Parameters.Add("@ItemClassification3",ddlItemGroup3.SelectedValue.Trim());	
				comm.Parameters.Add("@ItemClassification4",ddlItemGroup4.SelectedValue.Trim());	
				comm.Parameters.Add("@Division",ddlDivision.SelectedItem.Value);
				comm.Parameters.Add("@Year",ddlYear.SelectedItem.Value);
				comm.Parameters.Add("@Month",ddlMon.SelectedItem.Value);
				SqlDataAdapter da = new SqlDataAdapter(comm) ;
				DataSet ds = new DataSet() ;
				da.Fill(ds);

				DataColumn dc = new DataColumn("CompanyName", typeof(string));
				dt.Columns.Add(dc);
				dc = new DataColumn("BusinessRegistrationNum", typeof(string));
				dt.Columns.Add(dc);
				dc = new DataColumn("ItemNum", typeof(string));
				dt.Columns.Add(dc);
				dc = new DataColumn("ItemDrawNum", typeof(string));
				dt.Columns.Add(dc);
				dc = new DataColumn("ItemName", typeof(string));
				dt.Columns.Add(dc);
				dc = new DataColumn("ProcessCode", typeof(string));
				dt.Columns.Add(dc);
				dc = new DataColumn("ProcessName", typeof(string));
				dt.Columns.Add(dc);
				dc = new DataColumn("TransferStockQuantity", typeof(Decimal));
				dt.Columns.Add(dc);
				dc = new DataColumn("ThisInStoreQuantity", typeof(Decimal));
				dt.Columns.Add(dc);
				dc = new DataColumn("EtcInStoreQuantity", typeof(Decimal));
				dt.Columns.Add(dc);
				dc = new DataColumn("EtcOutStockQuantity", typeof(Decimal));
				dt.Columns.Add(dc);
				dc = new DataColumn("ThisOutStockQuantity", typeof(Decimal));
				dt.Columns.Add(dc);
				dc = new DataColumn("MonthStockQuantity", typeof(Decimal));
				dt.Columns.Add(dc);

				//단가
				dc = new DataColumn("ApplyUnitCost", typeof(Decimal));
				dt.Columns.Add(dc);

				//재고금액
				dc = new DataColumn("StockCost", typeof(Decimal));
				dt.Columns.Add(dc);


				dc = new DataColumn("TotalCost", typeof(Decimal));
				dt.Columns.Add(dc);
				dc = new DataColumn("ItemClassification1", typeof(string));
				dt.Columns.Add(dc);
				dc = new DataColumn("ItemClassification2", typeof(string));
				dt.Columns.Add(dc);
				dc = new DataColumn("ItemClassification3", typeof(string));
				dt.Columns.Add(dc);
				dc = new DataColumn("ItemClassification4", typeof(string));
				dt.Columns.Add(dc);
				dc = new DataColumn("Year", typeof(int));
				dt.Columns.Add(dc);
				dc = new DataColumn("Month", typeof(int));
				dt.Columns.Add(dc);
				
				for(int i = 0 ; i < ds.Tables[0].Rows.Count; i++)
				{
					DataRow dr = dt.NewRow();

					if(ds.Tables[0].Rows[i]["ProcessCode"].ToString() == "14000000" || ds.Tables[0].Rows[i]["ProcessCode"].ToString() == "14009999")
					{
						dr["CompanyName"] = ds.Tables[0].Rows[i]["CompanyName"].ToString();
						dr["BusinessRegistrationNum"] = ds.Tables[0].Rows[i]["BusinessRegistrationNum"].ToString();
						dr["ItemNum"] = ds.Tables[0].Rows[i]["ItemNum"].ToString();
						dr["ItemDrawNum"] = ds.Tables[0].Rows[i]["ItemDrawNum"].ToString();
						dr["ItemName"] = ds.Tables[0].Rows[i]["ItemName"].ToString();
						dr["ProcessCode"] = ds.Tables[0].Rows[i]["ProcessCode"].ToString();
						dr["ProcessName"] = ds.Tables[0].Rows[i]["ProcessName"].ToString();
						dr["Year"] = int.Parse(ds.Tables[0].Rows[i]["Year"].ToString());
						dr["Month"] = int.Parse(ds.Tables[0].Rows[i]["Month"].ToString());
						dr["TransferStockQuantity"] = TransferStockQuantity(conn,tr,dr["ItemNum"].ToString(), int.Parse(ds.Tables[0].Rows[i]["Month"].ToString()));
						dr["ThisInStoreQuantity"] = ThisInStoreQuantity(conn,tr,dr["BusinessRegistrationNum"].ToString(),dr["ItemNum"].ToString(),int.Parse(dr["Year"].ToString()),int.Parse(ds.Tables[0].Rows[i]["Month"].ToString()));
						dr["EtcInStoreQuantity"] = EtcInStoreQuantity(conn,tr,dr["BusinessRegistrationNum"].ToString(), dr["ItemNum"].ToString(),int.Parse(dr["Year"].ToString()),int.Parse(ds.Tables[0].Rows[i]["Month"].ToString()));
						dr["EtcOutStockQuantity"] = EtcOutStockQuantity(conn,tr,dr["ItemNum"].ToString(),int.Parse(dr["Year"].ToString()),int.Parse(ds.Tables[0].Rows[i]["Month"].ToString()));
						dr["ThisOutStockQuantity"] = ThisOutStockQuantity(conn,tr,dr["ItemNum"].ToString(),int.Parse(dr["Year"].ToString()),int.Parse(ds.Tables[0].Rows[i]["Month"].ToString()));
						dr["MonthStockQuantity"] = decimal.Parse(dr["TransferStockQuantity"].ToString()) + decimal.Parse(dr["ThisInStoreQuantity"].ToString()) + decimal.Parse(dr["EtcInStoreQuantity"].ToString())  - decimal.Parse(dr["EtcOutStockQuantity"].ToString())  - decimal.Parse(dr["ThisOutStockQuantity"].ToString()) ;		 //MonthStockQuantity(conn,tr,dr["ItemNum"].ToString(),int.Parse(dr["Year"].ToString()),int.Parse(ds.Tables[0].Rows[i]["Month"].ToString()));
						//단가
						dr["ApplyUnitCost"] = decimal.Parse(ds.Tables[0].Rows[i]["ApplyUnitCost"].ToString());
						//재고금액
						dr["StockCost"] = (decimal.Parse(ds.Tables[0].Rows[i]["ApplyUnitCost"].ToString()) * decimal.Parse(dr["MonthStockQuantity"].ToString()) );

						dr["TotalCost"] = ds.Tables[0].Rows[i]["TotalCost"].ToString();
						dr["ItemClassification1"] = ds.Tables[0].Rows[i]["ItemClassification1"].ToString();
						dr["ItemClassification2"] = ds.Tables[0].Rows[i]["ItemClassification2"].ToString();
						dr["ItemClassification3"] = ds.Tables[0].Rows[i]["ItemClassification3"].ToString();
						dr["ItemClassification4"] = ds.Tables[0].Rows[i]["ItemClassification4"].ToString();
						
					}
					else if(ds.Tables[0].Rows[i]["ProcessCode"].ToString() == "" || ds.Tables[0].Rows[i]["ProcessCode"] == Convert.DBNull)
					{
						dr["CompanyName"] = ds.Tables[0].Rows[i]["CompanyName"].ToString();
						dr["BusinessRegistrationNum"] = ds.Tables[0].Rows[i]["BusinessRegistrationNum"].ToString();
						dr["ItemNum"] = ds.Tables[0].Rows[i]["ItemNum"].ToString();
						dr["ItemDrawNum"] = ds.Tables[0].Rows[i]["ItemDrawNum"].ToString();
						dr["ItemName"] = ds.Tables[0].Rows[i]["ItemName"].ToString();
						dr["ProcessCode"] = ds.Tables[0].Rows[i]["ProcessCode"].ToString();
						dr["ProcessName"] = ds.Tables[0].Rows[i]["ProcessName"].ToString();
						dr["Year"] = ds.Tables[0].Rows[i]["Year"].ToString();
						dr["Month"] = ds.Tables[0].Rows[i]["Month"].ToString();
						dr["TransferStockQuantity"] = Convert.DBNull;
						dr["ThisInStoreQuantity"] = decimal.Parse(ds.Tables[0].Rows[i]["DeliveryQuantity"].ToString());
						dr["EtcInStoreQuantity"] = Convert.DBNull;
						dr["EtcOutStockQuantity"] = Convert.DBNull;
						dr["ThisOutStockQuantity"] = decimal.Parse(ds.Tables[0].Rows[i]["DeliveryQuantity"].ToString());
						dr["MonthStockQuantity"] = Convert.DBNull;

						//단가
						dr["ApplyUnitCost"] = decimal.Parse(ds.Tables[0].Rows[i]["ApplyUnitCost"].ToString());
						//재고금액
						dr["StockCost"] =  Convert.DBNull;//(decimal.Parse(ds.Tables[0].Rows[i]["ApplyUnitCost"].ToString()) * decimal.Parse(dr["MonthStockQuantity"].ToString()) );

						dr["TotalCost"] = ds.Tables[0].Rows[i]["TotalCost"].ToString();
						dr["ItemClassification1"] = ds.Tables[0].Rows[i]["ItemClassification1"].ToString();
						dr["ItemClassification2"] = ds.Tables[0].Rows[i]["ItemClassification2"].ToString();
						dr["ItemClassification3"] = ds.Tables[0].Rows[i]["ItemClassification3"].ToString();
						dr["ItemClassification4"] = ds.Tables[0].Rows[i]["ItemClassification4"].ToString();
					}
					else
					{
						dr["CompanyName"] = ds.Tables[0].Rows[i]["CompanyName"].ToString();
						dr["BusinessRegistrationNum"] = ds.Tables[0].Rows[i]["BusinessRegistrationNum"].ToString();
						dr["ItemNum"] = ds.Tables[0].Rows[i]["ItemNum"].ToString();
						dr["ItemDrawNum"] = ds.Tables[0].Rows[i]["ItemDrawNum"].ToString();
						dr["ItemName"] = ds.Tables[0].Rows[i]["ItemName"].ToString();
						dr["ProcessCode"] = ds.Tables[0].Rows[i]["ProcessCode"].ToString();
						dr["ProcessName"] = ds.Tables[0].Rows[i]["ProcessName"].ToString();
						dr["Year"] = ds.Tables[0].Rows[i]["Year"].ToString();
						dr["Month"] = ds.Tables[0].Rows[i]["Month"].ToString();
						dr["TransferStockQuantity"] = TransferStockQuantity(conn,tr,dr["ItemNum"].ToString(),dr["ProcessCode"].ToString(), int.Parse(ds.Tables[0].Rows[i]["Month"].ToString()));
						dr["ThisInStoreQuantity"] = ThisInStoreQuantity(conn,tr,int.Parse(dr["Year"].ToString()),int.Parse(ds.Tables[0].Rows[i]["Month"].ToString()),dr["ItemNum"].ToString(),dr["ProcessCode"].ToString());
						dr["EtcInStoreQuantity"] = EtcInStoreQuantity(conn,tr,int.Parse(dr["Year"].ToString()),int.Parse(ds.Tables[0].Rows[i]["Month"].ToString()),dr["ItemNum"].ToString(),dr["ProcessCode"].ToString());
						dr["EtcOutStockQuantity"] = EtcOutStockQuantity(conn,tr,dr["ItemNum"].ToString(),dr["ProcessCode"].ToString(),int.Parse(dr["Year"].ToString()),int.Parse(ds.Tables[0].Rows[i]["Month"].ToString()));
						dr["ThisOutStockQuantity"] = ThisOutStockQuantity(conn,tr,dr["ItemNum"].ToString(),dr["ProcessCode"].ToString(),int.Parse(dr["Year"].ToString()),int.Parse(ds.Tables[0].Rows[i]["Month"].ToString()));
						//dr["ThisOutStockQuantity"] = ThisOutStockQuantity(conn,tr,dr["ItemNum"].ToString(),int.Parse(dr["Year"].ToString()),int.Parse(ds.Tables[0].Rows[i]["Month"].ToString()));
						dr["MonthStockQuantity"] = decimal.Parse(dr["TransferStockQuantity"].ToString()) + decimal.Parse(dr["ThisInStoreQuantity"].ToString()) + decimal.Parse(dr["EtcInStoreQuantity"].ToString())  - decimal.Parse(dr["EtcOutStockQuantity"].ToString())  - decimal.Parse(dr["ThisOutStockQuantity"].ToString()) ;		 //MonthStockQuantity(conn,tr,dr["ItemNum"].ToString(),int.Parse(dr["Year"].ToString()),int.Parse(ds.Tables[0].Rows[i]["Month"].ToString()));


						//단가
						dr["ApplyUnitCost"] = decimal.Parse(ds.Tables[0].Rows[i]["ApplyUnitCost"].ToString());
						//재고금액
						dr["StockCost"] = (decimal.Parse(ds.Tables[0].Rows[i]["ApplyUnitCost"].ToString()) * decimal.Parse(dr["MonthStockQuantity"].ToString()) );


						dr["TotalCost"] = ds.Tables[0].Rows[i]["TotalCost"].ToString();
						dr["ItemClassification1"] = ds.Tables[0].Rows[i]["ItemClassification1"].ToString();
						dr["ItemClassification2"] = ds.Tables[0].Rows[i]["ItemClassification2"].ToString();
						dr["ItemClassification3"] = ds.Tables[0].Rows[i]["ItemClassification3"].ToString();
						dr["ItemClassification4"] = ds.Tables[0].Rows[i]["ItemClassification4"].ToString();
						
					}

					dt.Rows.Add(dr);
				}
				
				tr.Commit();
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
			return dt;
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid uwg = new UltraWebGrid();
			uwg = UltraWebGrid1;
			uwg.DisplayLayout.Pager.AllowPaging = false;
			uwg.DataSource = Search();
			uwg.DataBind();
			UltraWebGridExcelExporter1.Export(uwg);
		}

		
		
		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
		}



		
		/// <summary>
		/// 전월 이월재고 구하는 함수
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="ItemNum"></param>
		/// <param name="Month"></param>
		/// <returns></returns>
		private decimal TransferStockQuantity(SqlConnection con, SqlTransaction trans, string ItemNum, int Month)
		{
			int PreMonth = Month -1;
			string str = "";
			decimal Quantity = 0;
			SqlCommand comm = new SqlCommand();
			comm.Connection=con;
			comm.Transaction = trans;
			if(Division(con,trans,ItemNum))
			{
				if(PreMonth == 0)
				{
					str = "Select LastYearTransferQuantity as TransferQuantity  From RMS_MT where ItemNum = @ItemNum and RecodingState = 1 and [Year] = @year";
					comm.Parameters.Add("@year",DateTime.Now.Year);
					comm.Parameters.Add("@ItemNum",ItemNum);
				}
				else
				{
					str = "Select StockQuantity"+PreMonth+" as TransferQuantity From RMS_MT where ItemNum = @ItemNum and RecodingState = 1 and [Year] = @year";
					comm.Parameters.Add("@year",DateTime.Now.Year);
					comm.Parameters.Add("@ItemNum",ItemNum);
				}
			}
			else
			{
				if(PreMonth == 0)
				{
					str = @"Select (sum(BS_MT.LastYearTransferQuantity) + Sum(PS_MT.LastYearTransferQuantity) ) as TransferQuantity
						From BS_MT inner join PS_MT on BS_MT.ItemNum = PS_MT.ItemNum
						where BS_MT.RecodingState =1 and PS_MT.RecodingState = 1 and PS_MT.ProcessCode = '14009999'
						and BS_MT.[Year] = @year and PS_MT.[Year] = @year and BS_MT.ItemNum = @ItemNum";
					comm.Parameters.Add("@year",DateTime.Now.Year);
					comm.Parameters.Add("@ItemNum",ItemNum);
				}
				else
				{
					str = "Select (Sum(BS_MT.StockQuantity"+PreMonth+")  + Sum(PS_MT.StockQuantity"+PreMonth+")) as TransferQuantity "+ 
						" From BS_MT inner join PS_MT on BS_MT.ItemNum = PS_MT.ItemNum "+
						" where BS_MT.RecodingState =1 and PS_MT.RecodingState = 1 and PS_MT.ProcessCode = '14009999' " +
						" and BS_MT.[Year] = @year and PS_MT.[Year] = @year and BS_MT.ItemNum = @ItemNum";
					comm.Parameters.Add("@year",DateTime.Now.Year);
					comm.Parameters.Add("@ItemNum",ItemNum);
				}
			}
			comm.CommandText = str;
			SqlDataReader dr = comm.ExecuteReader();
			
			while(dr.Read())
			{
				Quantity = decimal.Parse(dr["TransferQuantity"].ToString());
			}
			dr.Close();
			comm.Parameters.Clear();
			return Quantity;
		}

		/// <summary>
		/// 전월 이월재고 구하는 함수
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="ItemNum"></param>
		/// <param name="Month"></param>
		/// <returns></returns>
		private decimal TransferStockQuantity(SqlConnection con, SqlTransaction trans, string ItemNum, string ProcessCode, int Month)
		{
			int PreMonth = Month -1;
			string str = "";
			decimal Quantity = 0;
			SqlCommand comm = new SqlCommand();
			comm.Connection=con;
			comm.Transaction = trans;
			
			if(PreMonth == 0)
			{
				str = "Select LastYearTransferQuantity as TransferQuantity  From PS_MT where ItemNum = @ItemNum and ProcessCode = @ProcessCode and RecodingState = 1 and [Year] = @year";
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.Parameters.Add("@ItemNum",ItemNum);
				comm.Parameters.Add("@ProcessCode",ProcessCode);
			}
			else
			{
				str = "Select StockQuantity"+PreMonth+" as TransferQuantity From PS_MT where ItemNum = @ItemNum and ProcessCode = @ProcessCode and RecodingState = 1 and [Year] = @year";
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.Parameters.Add("@ItemNum",ItemNum);
				comm.Parameters.Add("@ProcessCode",ProcessCode);
			}
			
			comm.CommandText = str;
			SqlDataReader dr = comm.ExecuteReader();
			
			while(dr.Read())
			{
				Quantity = decimal.Parse(dr["TransferQuantity"].ToString());
			}
			dr.Close();
			comm.Parameters.Clear();
			return Quantity;
		}

		/// <summary>
		/// 금월 정상입고 수량 구하는 함수(구매발주 혹은 외주발주를 통한 입고)
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="ItemNum"></param>
		/// <param name="Year"></param>
		/// <param name="Month"></param>
		/// <returns></returns>
		private decimal ThisInStoreQuantity(SqlConnection con, SqlTransaction trans, string BusinessRegistrationNum, string ItemNum, int Year, int Month)
		{
			string str = "SPThisInStoreQuantity";
			decimal Quantity = 0;
			SqlCommand comm = new SqlCommand();
			comm.Connection=con;
			comm.Transaction = trans;
			comm.CommandText = str;
			comm.CommandType = CommandType.StoredProcedure;
			comm.Parameters.Add("@ItemNum", ItemNum);
			comm.Parameters.Add("@BusinessRegistrationNum", BusinessRegistrationNum);
			comm.Parameters.Add("@Year", Year);
			comm.Parameters.Add("@Month", Month);
			SqlDataReader dr = comm.ExecuteReader();
			comm.Parameters.Clear();
			while(dr.Read())
			{
				Quantity = decimal.Parse(dr["ThisInStoreQuantity"].ToString());
			}
			dr.Close();

			return Quantity;
		}


		/// <summary>
		/// 금월 정상입고 수량 구하는 함수(작업일보)
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="ItemNum"></param>
		/// <param name="Year"></param>
		/// <param name="Month"></param>
		/// <returns></returns>
		private decimal ThisInStoreQuantity(SqlConnection con, SqlTransaction trans, int Year, int Month, string ItemNum, string ProcessCode )
		{
			string str = "SPThisItemInStoreQuantity";
			decimal Quantity = 0;
			SqlCommand comm = new SqlCommand();
			comm.Connection=con;
			comm.Transaction = trans;
			comm.CommandText = str;
			comm.CommandType = CommandType.StoredProcedure;
			comm.Parameters.Add("@ItemNum", ItemNum);
			comm.Parameters.Add("@ProcessCode",ProcessCode);
			
			comm.Parameters.Add("@Year", Year);
			comm.Parameters.Add("@Month", Month);
			SqlDataReader dr = comm.ExecuteReader();
			comm.Parameters.Clear();
			while(dr.Read())
			{
				Quantity = decimal.Parse(dr["ThisItemInStoreQuantity"].ToString());
			}
			dr.Close();

			return Quantity;
		}

		/// <summary>
		/// 기타 입고를 구하는 함수 (기타입출고, 보용품입고 , 공통자재 생성) 
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="ItemNum"></param>
		/// <param name="Year"></param>
		/// <param name="Month"></param>
		/// <returns></returns>
		private decimal EtcInStoreQuantity(SqlConnection con, SqlTransaction trans, string BusinessRegistrationNum, string ItemNum, int Year, int Month)
		{
			//string str = "SPEtcInStoreQuantity";
			string str = "SPEtcInStoreQuantity1";
			decimal Quantity = 0;
			SqlCommand comm = new SqlCommand();
			comm.Connection=con;
			comm.Transaction = trans;
			comm.CommandText = str;
			comm.CommandType = CommandType.StoredProcedure;
			comm.Parameters.Add("@ItemNum", ItemNum);
			//comm.Parameters.Add("@BusinessRegistrationNum", BusinessRegistrationNum);
			comm.Parameters.Add("@Year", Year);
			comm.Parameters.Add("@Month", Month);
			SqlDataReader dr = comm.ExecuteReader();
			comm.Parameters.Clear();
			while(dr.Read())
			{
				Quantity = decimal.Parse(dr["EtcInStoreQuantity"].ToString());
			}
			dr.Close();

			return Quantity;
		}


		/// <summary>
		/// 기타 입고를 구하는 함수 (기타입출고) 
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="ItemNum"></param>
		/// <param name="Year"></param>
		/// <param name="Month"></param>
		/// <returns></returns>
		private decimal EtcInStoreQuantity(SqlConnection con, SqlTransaction trans, int Year, int Month, string ItemNum,string ProcessCode)
		{
			string str = "SPEtcItemInStoreQuantity";
			decimal Quantity = 0;
			SqlCommand comm = new SqlCommand();
			comm.Connection=con;
			comm.Transaction = trans;
			comm.CommandText = str;
			comm.CommandType = CommandType.StoredProcedure;
			comm.Parameters.Add("@ItemNum", ItemNum);
			comm.Parameters.Add("@ProcessCode",ProcessCode);
			comm.Parameters.Add("@Year", Year);
			comm.Parameters.Add("@Month", Month);
			SqlDataReader dr = comm.ExecuteReader();
			comm.Parameters.Clear();
			while(dr.Read())
			{
				Quantity = decimal.Parse(dr["EtcItemInStoreQuantity"].ToString());
			}
			dr.Close();

			return Quantity;
		}

		/// <summary>
		/// 기타 출고를 구하는 함수(기타입출고의 출고, 공통자재 소진, 보용품 출고)
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="ItemNum"></param>
		/// <param name="Year"></param>
		/// <param name="Month"></param>
		/// <returns></returns>
		private decimal EtcOutStockQuantity(SqlConnection con, SqlTransaction trans, string ItemNum, int Year, int Month)
		{
			string str = "SPEtcOutStockQuantity";
			decimal Quantity = 0;
			SqlCommand comm = new SqlCommand();
			comm.Connection=con;
			comm.Transaction = trans;
			comm.CommandText = str;
			comm.CommandType = CommandType.StoredProcedure;
			comm.Parameters.Add("@ItemNum", ItemNum);
			comm.Parameters.Add("@Year", Year);
			comm.Parameters.Add("@Month", Month);
			SqlDataReader dr = comm.ExecuteReader();
			comm.Parameters.Clear();
			while(dr.Read())
			{
				Quantity = decimal.Parse(dr["EtcOutStockQuantity"].ToString());
			}
			dr.Close();

			return Quantity;
		}

		/// <summary>
		/// 기타 출고를 구하는 함수(기타입출고의 출고, 공통자재 소진, 보용품 출고)
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="ItemNum"></param>
		/// <param name="Year"></param>
		/// <param name="Month"></param>
		/// <returns></returns>
		private decimal EtcOutStockQuantity(SqlConnection con, SqlTransaction trans, string ItemNum, string ProcessCode, int Year, int Month)
		{
			string str = "SPEtcItemOutStockQuantity";
			decimal Quantity = 0;
			SqlCommand comm = new SqlCommand();
			comm.Connection=con;
			comm.Transaction = trans;
			comm.CommandText = str;
			comm.CommandType = CommandType.StoredProcedure;
			comm.Parameters.Add("@ItemNum", ItemNum);
			comm.Parameters.Add("@ProcessCode", ProcessCode);
			comm.Parameters.Add("@Year", Year);
			comm.Parameters.Add("@Month", Month);
			SqlDataReader dr = comm.ExecuteReader();
			comm.Parameters.Clear();
			while(dr.Read())
			{
				Quantity = decimal.Parse(dr["EtcItemOutStockQuantity"].ToString());
			}
			dr.Close();

			return Quantity;
		}

		/// <summary>
		/// 금월 정상 출고 고하는 함수 외주출고, 상품출고
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="ItemNum"></param>
		/// <param name="Year"></param>
		/// <param name="Month"></param>
		/// <returns></returns>
		private decimal ThisOutStockQuantity(SqlConnection con, SqlTransaction trans, string ItemNum, int Year, int Month)
		{
			string str = "SPThisOutStockQuantity";
			decimal Quantity = 0;
			SqlCommand comm = new SqlCommand();
			comm.Connection=con;
			comm.Transaction = trans;
			comm.CommandText = str;
			comm.CommandType = CommandType.StoredProcedure;
			comm.Parameters.Add("@ItemNum", ItemNum);
			comm.Parameters.Add("@Year", Year);
			comm.Parameters.Add("@Month", Month);
			SqlDataReader dr = comm.ExecuteReader();
			comm.Parameters.Clear();
			while(dr.Read())
			{
				Quantity = decimal.Parse(dr["ThisOutStockQuantity"].ToString());
			}
			dr.Close();

			return Quantity;
		}

		/// <summary>
		/// 금월 정상 출고 고하는 함수 외주출고, 상품출고
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="ItemNum"></param>
		/// <param name="Year"></param>
		/// <param name="Month"></param>
		/// <returns></returns>
		private decimal ThisOutStockQuantity(SqlConnection con, SqlTransaction trans, string ItemNum, string ProcessCode, int Year, int Month)
		{
			string str = "SPThisItemOutStockQuantity";
			decimal Quantity = 0;
			SqlCommand comm = new SqlCommand();
			comm.Connection=con;
			comm.Transaction = trans;
			comm.CommandText = str;
			comm.CommandType = CommandType.StoredProcedure;
			comm.Parameters.Add("@ItemNum", ItemNum);
			comm.Parameters.Add("@ProcessCode", ProcessCode);
			comm.Parameters.Add("@Year", Year);
			comm.Parameters.Add("@Month", Month);
			SqlDataReader dr = comm.ExecuteReader();
			comm.Parameters.Clear();
			while(dr.Read())
			{
				Quantity = decimal.Parse(dr["ThisItemOutStockQuantity"].ToString());
			}
			dr.Close();

			return Quantity;
		}

		

		/// <summary>
		/// 자산분류가 원자재이면 true를 리턴하는 함수
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="ItemNum"></param>
		/// <returns></returns>
		private bool Division(SqlConnection conn, SqlTransaction tr, string ItemNum)
		{
			string str = "Select PropertyClassification From II_MT where RecodingState =1 and ItemNum = @ItemNum";
			string PropertyClassification = "";
			SqlCommand comm = new SqlCommand();
			comm.Connection=conn;
			comm.Transaction = tr;
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum", ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			comm.Parameters.Clear();
			while(dr.Read())
			{
				PropertyClassification = dr["PropertyClassification"].ToString();
			}
			dr.Close();

			if(PropertyClassification == "원자재")
				return true;
			else
				return false;
		}
	}
}
