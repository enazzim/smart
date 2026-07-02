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
	/// InputResult에 대한 요약 설명입니다.
	/// </summary>
	public class InputResult : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button2;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_FromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_ToDate;
		protected System.Web.UI.WebControls.Button bt_Excel;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid2;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected Infragistics.WebUI.WebCombo.WebCombo wc_WCName;
		
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			ItemSearchControl1.Products = true;
			ItemSearchControl1.Commodity = true;
			ItemSearchControl1.HalfFinishedProducts = true;
			ItemSearchControl1.RawMaterials = true;

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			string str1 = "Select WCName, WCInfoIndex From WCI_MT where RecodingState = 1 order by WCName";
			SqlCommand comm1 = new SqlCommand(str1,conn);
			SqlDataAdapter da1 = new SqlDataAdapter(comm1) ;
			DataSet ds1 = new DataSet() ;
			da1.Fill(ds1);

			wc_WCName.DataSource = ds1;
			wc_WCName.DataTextField = ds1.Tables[0].Columns[0].ToString();
			wc_WCName.DataValueField = ds1.Tables[0].Columns[0].ToString();
			wc_WCName.DataBind();
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
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.UltraWebGrid2.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid2_PageIndexChanged);
			this.bt_Excel.Click += new System.EventHandler(this.bt_Excel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button2_Click(object sender, System.EventArgs e)
		{
			ItemSearchControl1.ClearTextBox();
			wc_WCName.DataValue = "";
			wdc_FromDate.Value="";
			wdc_ToDate.Value = "";
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid2.DataSource = search();
			UltraWebGrid2.DataBind();
		}

		private void bt_Excel_Click(object sender, System.EventArgs e)
		{

			if(UltraWebGrid2.Rows.Count == 0)
			{
				RegisterClientScriptBlock("","<script>alert('저장할 항목이 없습니다!');</script>");
			}
			else
			{
				Infragistics.WebUI.UltraWebGrid.UltraWebGrid  uwgExcelImport = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
				uwgExcelImport = UltraWebGrid2;
				uwgExcelImport.DisplayLayout.Pager.AllowPaging = false;

				uwgExcelImport.DataSource = search();
				uwgExcelImport.DataBind();

				UltraWebGridExcelExporter1.Export(uwgExcelImport);		
			}
//			if(UltraWebGrid2.Rows.Count == 0)
//			{
//				RegisterClientScriptBlock("","<script>alert('저장할 항목이 없습니다!');</script>");
//			}
//			else
//			{
//				Infragistics.WebUI.UltraWebGrid uwg_Excel = new Infragistics.WebUI.UltraWebGrid();
//				uwg_Excel = UltraWebGrid2;
//				uwg_Excel.DisplayLayout.Pager.AllowPaging = false;
//				uwg_Excel.DataSource = search();
//				uwg_Excel.DataBind();
//				UltraWebGridExcelExporter1.Export(uwg_Excel);
//			}
		}

		private void UltraWebGrid2_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid2.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;			
			UltraWebGrid2.DataSource = search();
			UltraWebGrid2.DataBind();
		}

		private DataSet search()
		{
			
			string ItemName = ItemSearchControl1.ItemName.ToString().Trim();
			string WCName = "";
			string BeginDate = "";
			string EndDate = "";
			if(wc_WCName.DataValue ==null)
				WCName = Convert.ToString(wc_WCName.DisplayValue);
			else
				WCName = Convert.ToString(wc_WCName.DataValue);
			if(Convert.ToString(wdc_FromDate.Value).Trim() == "")
				BeginDate = "1999-01-01";
			else
				BeginDate = wdc_FromDate.Text;
			if(Convert.ToString(wdc_ToDate.Value).Trim() == "")
				EndDate = "2079-06-01";
			else
				EndDate = wdc_ToDate.Text;

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand comm = new SqlCommand("InputResult",conn);
			comm.CommandType = CommandType.StoredProcedure;
			comm.Parameters.Add("@ItemName","%"+ItemName+"%");
			comm.Parameters.Add("@WCName","%"+WCName+"%");
			comm.Parameters.Add("@BeginDate",BeginDate);
			comm.Parameters.Add("@EndDate",EndDate);

			DataSet ds = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(comm);
			da.Fill(ds);

			return ds;

		}

		
	}
}
