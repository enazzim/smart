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

namespace KIT_ERP.BasisInformation.Popup
{
	/// <summary>
	/// ExecutionPlan에 대한 요약 설명입니다.
	/// </summary>
	public class ExecutionPlan : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button bt_Excel;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.

			if(!Page.IsPostBack)
			{
				
				UltraWebGrid1.DataSource = PageLoad();
				UltraWebGrid1.DataBind();
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
			this.bt_Excel.Click += new System.EventHandler(this.bt_Excel_Click);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void bt_Excel_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DataSource = PageLoad();
			UltraWebGrid1.DisplayLayout.Pager.AllowPaging = false;
			UltraWebGrid1.DataBind();
			UltraWebGridExcelExporter1.Export(UltraWebGrid1);
		}

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = PageLoad();
			UltraWebGrid1.DataBind();
		}

		private DataSet PageLoad()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str  = @"SELECT EPI_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, EPI_MT.PlanDate, EPI_MT.PlanQuantity,
				EPI_MT.SaleUnitCost, EPI_MT.PlanTotalCost, EPI_MT.RecodingState, EPI_MT.RegistrationPerson,
				EPI_MT.RegistrationPersonID, EPI_MT.RegistrationDate, EPI_MT.UpdatingPerson, EPI_MT.UpdatingPersonID,
				EPI_MT.UpdatingDate, EPI_MT.ExecutionPlanInfoIndex FROM EPI_MT INNER JOIN II_MT ON
				EPI_MT.ItemNum = II_MT.ItemNum WHERE (EPI_MT.RecodingState = '1') order by EPI_MT.ItemNum";
			SqlCommand comm = new SqlCommand(str,conn);
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);
			return ds;
		}
	}
}
