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
	/// StorehouseReceiptsAndDisbursements에 대한 요약 설명입니다.
	/// </summary>
	public class StorehouseReceiptsAndDisbursements : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btItemSearch;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.DropDownList ddlStore;
		protected System.Web.UI.WebControls.DropDownList ddlYear;
		protected System.Web.UI.WebControls.DropDownList ddlMonth;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
	
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

			if(!Page.IsPostBack)
			{
				for(int a = 0; a<=ddlYear.Items.Count; a++) 
				{
					if(ddlYear.Items[a].Value == DateTime.Now.Year.ToString())
					{
						ddlYear.SelectedIndex = a;
						break;
					}					
				}

				ddlMonth.SelectedIndex = DateTime.Now.Month-1;
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
			this.btItemSearch.Click += new System.EventHandler(this.btItemSearch_Click);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btItemSearch_Click(object sender, System.EventArgs e)
		{

			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
		}

		private DataTable Search()
		{
			DataTable dt = new DataTable();
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();
			try
			{
				string str = "SPStorehouseReceiptsAndDisbursements";
				SqlCommand comm = new SqlCommand();
				comm.Transaction = tr;
				comm.Connection = conn;
				comm.CommandText = str;
				comm.CommandType = CommandType.StoredProcedure;
				comm.Parameters.Add("@Store",ddlStore.SelectedItem.Text.Trim());
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
				comm.Parameters.Add("@Year",ddlYear.SelectedItem.Text);
				comm.Parameters.Add("@Month",ddlMonth.SelectedItem.Text);
				SqlDataAdapter da = new SqlDataAdapter(comm) ;
				DataSet ds = new DataSet() ;
				da.Fill(ds);


				
				DataColumn dc = new DataColumn("ItemNum", typeof(string));
				dt.Columns.Add(dc);
				dc = new DataColumn("ItemDrawNum", typeof(string));
				dt.Columns.Add(dc);
				dc = new DataColumn("ItemName", typeof(string));
				dt.Columns.Add(dc);
				dc = new DataColumn("ProcessSequenceNum", typeof(int));
				dt.Columns.Add(dc);
				dc = new DataColumn("ProcessCode", typeof(string));
				dt.Columns.Add(dc);
				dc = new DataColumn("ProcessName", typeof(string));
				dt.Columns.Add(dc);
				dc = new DataColumn("TransferStockQuantity", typeof(Decimal));
				dt.Columns.Add(dc);
				dc = new DataColumn("ReceiveStockQuantity", typeof(Decimal));
				dt.Columns.Add(dc);
				dc = new DataColumn("OutStockQuantity", typeof(Decimal));
				dt.Columns.Add(dc);
				dc = new DataColumn("NowStockQuantity", typeof(Decimal));
				dt.Columns.Add(dc);

				
				for(int i = 0 ; i < ds.Tables[0].Rows.Count; i++)
				{

						
					DataRow dr = dt.NewRow();
					dr["ItemNum"] = ds.Tables[0].Rows[i]["ItemNum"].ToString();
					dr["ItemDrawNum"] = ds.Tables[0].Rows[i]["ItemDrawNum"].ToString();
					dr["ItemName"] = ds.Tables[0].Rows[i]["ItemName"].ToString();
					dr["ProcessSequenceNum"] = int.Parse(ds.Tables[0].Rows[i]["ProcessSequenceNum"].ToString());
					dr["ProcessCode"] = ds.Tables[0].Rows[i]["ProcessCode"].ToString();
					dr["ProcessName"] = ds.Tables[0].Rows[i]["ProcessName"].ToString();
					dr["TransferStockQuantity"] = TransferStockQuantity(conn,tr,dr["ItemNum"].ToString(),dr["ProcessCode"].ToString());
					dr["ReceiveStockQuantity"] = ReceiveStockQuantity(conn,tr,dr["ItemNum"].ToString(),dr["ProcessCode"].ToString());
					dr["OutStockQuantity"] = OutStockQuantity(conn,tr,dr["ItemNum"].ToString(),dr["ProcessCode"].ToString());
					//dr["NowStockQuantity"] = decimal.Parse(dr["TransferStockQuantity"].ToString()) + decimal.Parse(dr["ReceiveStockQuantity"].ToString()) - decimal.Parse(dr["OutStockQuantity"].ToString())  ;//NowStockQuantity(conn,tr,dr["ItemNum"].ToString(),dr["ProcessCode"].ToString());
					dr["NowStockQuantity"] = NowStockQuantity(conn,tr,dr["ItemNum"].ToString(),dr["ProcessCode"].ToString());
				
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

		private decimal TransferStockQuantity(SqlConnection con, SqlTransaction trans, string ItemNum, string ProcessCode)
		{
			decimal  Quantity = 0;
			string str = "";
			int year = DateTime.Now.Year;
			int a = int.Parse(ddlMonth.SelectedItem.Text)-1;
			
			
			SqlCommand comm = new SqlCommand();
			comm.Connection=con;
			comm.Transaction = trans;

			switch(ddlStore.SelectedItem.Text)
			{
				case "원자재창고":
					if(a == 0)
					{
						str = "Select LastYearTransferQuantity as StockQuantity From RMS_MT where ItemNum=@ItemNum and RecodingState = 1 and [Year] = @year";
					}
					else
					{
						str = "Select StockQuantity"+a+" as StockQuantity From RMS_MT where ItemNum=@ItemNum and RecodingState = 1 and [Year] = @year";
					}
					comm.Parameters.Add("@ItemNum", ItemNum);
					comm.Parameters.Add("@year",year);
					break;
				case "생산창고":
					if(a == 0)
					{
						str = "Select LastYearTransferQuantity as StockQuantity From PS_MT where ItemNum=@ItemNum and ProcessCode = @ProcessCode and RecodingState = 1 and [Year] = @year";
					}
					else
					{
						str = "Select StockQuantity"+a+" as StockQuantity From PS_MT where ItemNum=@ItemNum and ProcessCode = @ProcessCode and RecodingState = 1 and [Year] = @year";
					}
					comm.Parameters.Add("@ItemNum", ItemNum);
					comm.Parameters.Add("@ProcessCode", ProcessCode);
					comm.Parameters.Add("@year",year);
					break;
				case "외주창고":
					if(a == 0)
					{
						str = "Select LastYearTransferQuantity as StockQuantity From OS_MT where ItemNum=@ItemNum and ProcessCode = @ProcessCode and RecodingState = 1 and [Year] = @year";
					}
					else
					{
						str = "Select Sum(StockQuantity"+a+") as StockQuantity From OS_MT where ItemNum=@ItemNum and ProcessCode = @ProcessCode and RecodingState = 1 and [Year] = @year";
					}
					
					comm.Parameters.Add("@ItemNum", ItemNum);
					comm.Parameters.Add("@ProcessCode", ProcessCode);
					comm.Parameters.Add("@year",year);
					break;
				case "보용품창고":
					if(a == 0)
					{
						str = "Select LastYearTransferQuantity as StockQuantity From AS_MT where ItemNum=@ItemNum and RecodingState = 1 and [Year] = @year";
					}
					else
					{
						str = "Select StockQuantity"+a+" as StockQuantity From AS_MT where ItemNum=@ItemNum and RecodingState = 1 and [Year] = @year";
					}
					
					comm.Parameters.Add("@ItemNum", ItemNum);
					comm.Parameters.Add("@year",year);
					break;
				default:
					if(a == 0)
					{
						str = "Select LastYearTransferQuantity as StockQuantity From BS_MT where ItemNum=@ItemNum and RecodingState = 1 and [Year] = @year";
					}
					else
					{
						str = "Select Sum(StockQuantity"+a+") as StockQuantity From BS_MT where ItemNum=@ItemNum and RecodingState = 1 and [Year] = @year";
					}
					
					comm.Parameters.Add("@ItemNum", ItemNum);
					comm.Parameters.Add("@year",year);
					break;
			}
			comm.CommandText = str;
			SqlDataReader dr = comm.ExecuteReader();
			comm.Parameters.Clear();
			while(dr.Read())
			{
				Quantity = decimal.Parse(dr["StockQuantity"].ToString());
			}
			dr.Close();

			return Quantity;
		}


		private decimal ReceiveStockQuantity(SqlConnection con, SqlTransaction trans, string ItemNum, string ProcessCode)
		{
			decimal  Quantity = 0;
			string str = "";
			SqlCommand comm = new SqlCommand();
			comm.Connection=con;
			comm.Transaction = trans;

			switch(ddlStore.SelectedItem.Text)
			{
				case "원자재창고":
					str = "Select isnull(Sum(Quantity),0) as Quantity  From SH_HT where ItemNum=@ItemNum and Division = 1 and datepart(yy, [Date]) = @Year and datepart(MM, [Date]) = @Month and StoreName = '원자재창고'";
					comm.Parameters.Add("@ItemNum", ItemNum);
					comm.Parameters.Add("@Year", ddlYear.SelectedItem.Text.Trim());
					comm.Parameters.Add("@Month", ddlMonth.SelectedItem.Text.Trim());
					break;
				case "생산창고":
					str = "Select isnull(Sum(Quantity),0) as Quantity  From SH_HT where ItemNum=@ItemNum and Division = 1 and datepart(yy, [Date]) = @Year and datepart(MM, [Date]) = @Month and ProcessCode = @ProcessCode and StoreName = '생산창고'";
					comm.Parameters.Add("@ItemNum", ItemNum);
					comm.Parameters.Add("@ProcessCode", ProcessCode);
					comm.Parameters.Add("@Year", ddlYear.SelectedItem.Text.Trim());
					comm.Parameters.Add("@Month", ddlMonth.SelectedItem.Text.Trim());
					break;
				case "외주창고":
					str = "Select isnull(Sum(Quantity),0) Quantity  From SH_HT where ItemNum=@ItemNum and Division = 1 and datepart(yy, [Date]) = @Year and datepart(MM, [Date]) = @Month and StoreName = '외주창고'";
					comm.Parameters.Add("@ItemNum", ItemNum);
					comm.Parameters.Add("@ProcessCode", ProcessCode);
					comm.Parameters.Add("@Year", ddlYear.SelectedItem.Text.Trim());
					comm.Parameters.Add("@Month", ddlMonth.SelectedItem.Text.Trim());
					break;
				case "보용품창고":
					str = "Select isnull(Sum(Quantity),0) as Quantity  From SH_HT where ItemNum=@ItemNum and Division = 1 and datepart(yy, [Date]) = @Year and datepart(MM, [Date]) = @Month and StoreName = '보용품창고'";
					comm.Parameters.Add("@ItemNum", ItemNum);
					comm.Parameters.Add("@Year", ddlYear.SelectedItem.Text.Trim());
					comm.Parameters.Add("@Month", ddlMonth.SelectedItem.Text.Trim());
					break;
				default:
					str = "Select isnull(Sum(Quantity),0) Quantity  From SH_HT where ItemNum=@ItemNum and Division = 1 and datepart(yy, [Date]) = @Year and datepart(MM, [Date]) = @Month and StoreName = '영업창고'";
					comm.Parameters.Add("@ItemNum", ItemNum);
					comm.Parameters.Add("@Year", ddlYear.SelectedItem.Text.Trim());
					comm.Parameters.Add("@Month", ddlMonth.SelectedItem.Text.Trim());
					break;
			}
			comm.CommandText = str;
			SqlDataReader dr = comm.ExecuteReader();
			comm.Parameters.Clear();
			while(dr.Read())
			{
				Quantity = decimal.Parse(dr["Quantity"].ToString());
			}
			dr.Close();

			return Quantity;
		}

		private decimal OutStockQuantity(SqlConnection con, SqlTransaction trans, string ItemNum, string ProcessCode)
		{
			decimal  Quantity = 0;
			string str = "";
			SqlCommand comm = new SqlCommand();
			comm.Connection=con;
			comm.Transaction = trans;

			switch(ddlStore.SelectedItem.Text)
			{
				case "원자재창고":
					str = "Select isnull(Sum(Quantity),0) as Quantity  From SH_HT where ItemNum=@ItemNum and Division = 0 and datepart(yy, [Date]) = @Year and datepart(MM, [Date]) = @Month and StoreName = '원자재창고'";
					comm.Parameters.Add("@ItemNum", ItemNum);
					comm.Parameters.Add("@Year", ddlYear.SelectedItem.Text.Trim());
					comm.Parameters.Add("@Month", ddlMonth.SelectedItem.Text.Trim());
					break;
				case "생산창고":
					str = "Select isnull(Sum(Quantity),0) Quantity  From SH_HT where ItemNum=@ItemNum and Division = 0 and datepart(yy, [Date]) = @Year and datepart(MM, [Date]) = @Month and ProcessCode = @ProcessCode and StoreName = '생산창고'";
					comm.Parameters.Add("@ItemNum", ItemNum);
					comm.Parameters.Add("@ProcessCode", ProcessCode);
					comm.Parameters.Add("@Year", ddlYear.SelectedItem.Text.Trim());
					comm.Parameters.Add("@Month", ddlMonth.SelectedItem.Text.Trim());
					break;
				case "외주창고":
					str = "Select isnull(Sum(Quantity),0) as Quantity  From SH_HT where ItemNum=@ItemNum and Division = 0 and datepart(yy, [Date]) = @Year and datepart(MM, [Date]) = @Month and StoreName = '외주창고'";
					comm.Parameters.Add("@ItemNum", ItemNum);
					comm.Parameters.Add("@ProcessCode", ProcessCode);
					comm.Parameters.Add("@Year", ddlYear.SelectedItem.Text.Trim());
					comm.Parameters.Add("@Month", ddlMonth.SelectedItem.Text.Trim());
					break;
				case "보용품창고":
					str = "Select isnull(Sum(Quantity),0) as Quantity  From SH_HT where ItemNum=@ItemNum and Division = 0 and datepart(yy, [Date]) = @Year and datepart(MM, [Date]) = @Month and StoreName = '보용품창고'";
					comm.Parameters.Add("@ItemNum", ItemNum);
					comm.Parameters.Add("@Year", ddlYear.SelectedItem.Text.Trim());
					comm.Parameters.Add("@Month", ddlMonth.SelectedItem.Text.Trim());
					break;
				default:
					str = "Select isnull(Sum(Quantity),0) as Quantity  From SH_HT where ItemNum=@ItemNum and Division = 0 and datepart(yy, [Date]) = @Year and datepart(MM, [Date]) = @Month and StoreName = '영업창고'";
					comm.Parameters.Add("@ItemNum", ItemNum);
					comm.Parameters.Add("@Year", ddlYear.SelectedItem.Text.Trim());
					comm.Parameters.Add("@Month", ddlMonth.SelectedItem.Text.Trim());
					break;
			}
			comm.CommandText = str;
			SqlDataReader dr = comm.ExecuteReader();
			comm.Parameters.Clear();
			while(dr.Read())
			{
				Quantity = decimal.Parse(dr["Quantity"].ToString());
			}
			dr.Close();

			return Quantity;
		}

		private decimal NowStockQuantity(SqlConnection con, SqlTransaction trans, string ItemNum, string ProcessCode)
		{
			decimal  Quantity = 0;
			string str = "";
			int a = int.Parse(ddlMonth.SelectedItem.Text);
			
			SqlCommand comm = new SqlCommand();
			comm.Connection=con;
			comm.Transaction = trans;

			switch(ddlStore.SelectedItem.Text)
			{
				case "원자재창고":
					str = "Select StockQuantity"+a+" as StockQuantity From RMS_MT where ItemNum=@ItemNum and RecodingState = 1 and [Year] = @year";
					comm.Parameters.Add("@ItemNum", ItemNum);
					comm.Parameters.Add("@year",DateTime.Now.Year);
					break;
				case "생산창고":
					str = "Select StockQuantity"+a+" as StockQuantity From PS_MT where ItemNum=@ItemNum and ProcessCode = @ProcessCode and RecodingState = 1 and [Year] = @year";
					comm.Parameters.Add("@ItemNum", ItemNum);
					comm.Parameters.Add("@ProcessCode", ProcessCode);
					comm.Parameters.Add("@year",DateTime.Now.Year);
					break;
				case "외주창고":
					str = "Select Sum(StockQuantity"+a+") as StockQuantity From OS_MT where ItemNum=@ItemNum and ProcessCode = @ProcessCode and RecodingState = 1 and [Year] = @year";
					comm.Parameters.Add("@ItemNum", ItemNum);
					comm.Parameters.Add("@ProcessCode", ProcessCode);
					comm.Parameters.Add("@year",DateTime.Now.Year);
					break;
				case "보용품창고":
					str = "Select StockQuantity"+a+" as StockQuantity From AS_MT where ItemNum=@ItemNum and RecodingState = 1 and [Year] = @year";
					comm.Parameters.Add("@ItemNum", ItemNum);
					comm.Parameters.Add("@year",DateTime.Now.Year);
					break;
				default:
					str = "Select Sum(StockQuantity"+a+") as StockQuantity From BS_MT where ItemNum=@ItemNum and RecodingState = 1 and [Year] = @year";
					comm.Parameters.Add("@ItemNum", ItemNum);
					comm.Parameters.Add("@year",DateTime.Now.Year);
					break;
			}
			comm.CommandText = str;
			SqlDataReader dr = comm.ExecuteReader();
			comm.Parameters.Clear();
			while(dr.Read())
			{
				Quantity = decimal.Parse(dr["StockQuantity"].ToString());
			}
			dr.Close();

			return Quantity;
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

		

	}
}
