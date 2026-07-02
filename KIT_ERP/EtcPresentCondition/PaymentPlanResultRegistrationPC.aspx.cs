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
using Infragistics.WebUI;
using Infragistics.WebUI.UltraWebGrid;


namespace KIT_ERP.EtcPresentCondition
{
	/// <summary>
	/// PaymentPlanResultRegistrationPC에 대한 요약 설명입니다.
	/// </summary>
	public class PaymentPlanResultRegistrationPC : System.Web.UI.Page
	{
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcFromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcToDate;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected System.Web.UI.WebControls.Button bt_Excel;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgPPR_HT;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			CSC1.UnitCostDistinction = "구매외주";
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
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
			this.bt_Search.Click += new System.EventHandler(this.bt_Search_Click);
			this.uwgPPR_HT.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.uwgPPR_HT_PageIndexChanged);
			this.bt_Excel.Click += new System.EventHandler(this.bt_Excel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// 검색버튼
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Search_Click(object sender, System.EventArgs e)
		{
			uwgPPR_HT.DisplayLayout.Pager.CurrentPageIndex = 1;
			uwgPPR_HT.DataSource = Search();
			uwgPPR_HT.DataBind();
		}

		/// <summary>
		/// 검색함수
		/// </summary>
		private DataSet Search()
		{
			Search aa = new Search("PaymentPlanResultRegistrationPC",wdcFromDate,wdcToDate,CSC1.Company,CSC1.BusinessRegistrationNum);
			return aa.DataSet_search();
			
		}

		/// <summary>
		/// 엑셀저장
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Excel_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid uwg_Excel = new UltraWebGrid();
			uwg_Excel = uwgPPR_HT;
			uwg_Excel.DisplayLayout.Pager.AllowPaging = false;
			uwg_Excel.DataSource = Search();
			uwg_Excel.DataBind();
			UltraWebGridExcelExporter1.Export(uwg_Excel);
		}

		/// <summary>
		/// 페이지 이동
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void uwgPPR_HT_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			uwgPPR_HT.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			uwgPPR_HT.DataSource = Search();
			uwgPPR_HT.DataBind();
		}
	}
}
