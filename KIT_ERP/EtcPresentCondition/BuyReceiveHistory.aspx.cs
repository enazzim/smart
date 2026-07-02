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
	/// BuyReceiveHistory에 대한 요약 설명입니다.
	/// </summary>
	public class BuyReceiveHistory : System.Web.UI.Page
	{
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcFromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcToDate;
		protected System.Web.UI.WebControls.Button btItemSearch;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button Button1;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected System.Web.UI.WebControls.DropDownList ddlDivision;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CompanySearchControl1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{

			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			ItemSearchControl1.Commodity = true;
			ItemSearchControl1.Products = true;
			ItemSearchControl1.RawMaterials = true;
			ItemSearchControl1.Phantom = false;
			ItemSearchControl1.HalfFinishedProducts = true;
			CompanySearchControl1.UnitCostDistinction="";
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

			DataTable dt = new DataTable();
			dt = Search();
			if(dt.Rows.Count != 0)
			{
				UltraWebGrid1.DataSource = dt;
				UltraWebGrid1.DataBind();
			}
			else
			{
				DataTable dt1 = new DataTable();
				UltraWebGrid1.DataSource = dt1;
				UltraWebGrid1.DataBind();
				Response.Write("<script language=javascript>");
				Response.Write("alert('검색결과가 없습니다!');");
				Response.Write("</script>");
			}
		}

		private DataTable Search()
		{

			DataTable dt = new DataTable();

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "SPBuyReceiveHistory";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.CommandType = CommandType.StoredProcedure;
			comm.Parameters.Add("@ItemNum",ItemSearchControl1.ItemNum);
			comm.Parameters.Add("@ItemDrawNum",ItemSearchControl1.ItemDrawNum);
			comm.Parameters.Add("@ItemName",ItemSearchControl1.ItemName);
			comm.Parameters.Add("@hdItemNum",ItemSearchControl1.hdItem);
			comm.Parameters.Add("@CompanyName",CompanySearchControl1.Company);
			comm.Parameters.Add("@BusinessRegistrationNum",CompanySearchControl1.BusinessRegistrationNum);
			comm.Parameters.Add("@Division",ddlDivision.SelectedIndex);
			comm.Parameters.Add("@FromDate",wdcFromDate.Text.Trim());
			comm.Parameters.Add("@ToDate",wdcToDate.Text.Trim());

			SqlDataAdapter da = new SqlDataAdapter(comm) ;
			DataSet ds = new DataSet() ;
			da.Fill(ds);

			return ds.Tables[0];
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
