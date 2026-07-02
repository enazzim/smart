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

namespace KIT_ERP.ManagementInfomation
{
	/// <summary>
	/// CompanyItems에 대한 요약 설명입니다.
	/// </summary>
	public class CompanyItems : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Button btnExcel;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter uwgExcel;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwg1;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ISC1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Homepage/default.htm';</script>");
			}

			CSC1.UnitCostDistinction = "";
			ISC1.Commodity = true;
			ISC1.RawMaterials = true;
			ISC1.HalfFinishedProducts = true;
			ISC1.Products = true;
			
			
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
			this.uwg1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.uwg1_PageIndexChanged);
			this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			uwg1.DisplayLayout.Pager.CurrentPageIndex = 1;
			uwg1.DataSource = Search();
			uwg1.DataBind();
			
		}

		private DataSet Search()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlDataAdapter adap = new SqlDataAdapter("SPCompanyItems",conn);
			adap.SelectCommand.Parameters.Add("@CompanyName",CSC1.Company);
			adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum",CSC1.BusinessRegistrationNum);
			adap.SelectCommand.Parameters.Add("@ItemNum",ISC1.ItemNum);
			adap.SelectCommand.Parameters.Add("@ItemDrawNum",ISC1.ItemDrawNum);
			adap.SelectCommand.Parameters.Add("@ItemName",ISC1.ItemName);
			adap.SelectCommand.CommandType = CommandType.StoredProcedure;
			
			DataSet ds = new DataSet();
			
			adap.Fill(ds);	
		
			return ds;
		}

		private void uwg1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			uwg1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			uwg1.DataSource = Search();
			uwg1.DataBind();
		}

		private void btnExcel_Click(object sender, System.EventArgs e)
		{
			// 현재 Grid의 내용을 Excel로 Export
			Infragistics.WebUI.UltraWebGrid.UltraWebGrid  uwgExcelImport = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
			uwgExcelImport = uwg1;
			uwgExcelImport.DisplayLayout.Pager.AllowPaging = false;


			this.uwg1.DataSource = Search();
			this.uwg1.DataBind();

			uwgExcel.Export(uwg1);	
		}

		

		
	}
}
