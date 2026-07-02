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

using System.Data.SqlClient;
using System.Configuration;

namespace KIT_ERP.ProductionManagement.PopupWindows
{
	/// <summary>
	/// RowMaterialRequirementPC에 대한 요약 설명입니다.
	/// </summary>
	public class RowMaterialRequirementPC : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnOK;
		protected System.Web.UI.WebControls.Button btnCancle;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgMRC_HT;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button Button1;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid2;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcDate;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgPP_HT;
		
		

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				DataSet dsRowMaterial1 = (KIT_ERP.ProductionManagement.dsRowMaterial)Session["dataset"];
				UltraWebGrid1.DataSource = dsRowMaterial1.Tables["RowMaterial"];
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.btnCancle.Click += new System.EventHandler(this.button_Click);
			this.btnOK.Click += new System.EventHandler(this.button_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		// TODO : 버튼 클릭시
		private void button_Click(object sender, System.EventArgs e)
		{
			Button button = (Button)sender;

			if(button.ID.ToString() == "btnOK")
			{
				DataSet dsRowMaterial1 = (KIT_ERP.ProductionManagement.dsRowMaterial)Session["dataSet"];
				uwgMRC_HT.DataSource = dsRowMaterial1.Tables["MRC_HT"];
				uwgMRC_HT.DataBind();

				KIT_ERP.Register register = new KIT_ERP.Register(uwgMRC_HT,(Infragistics.WebUI.UltraWebGrid.UltraWebGrid)Session["dataGrid"], "RowMaterialRequirementCalculate",Session["ID"].ToString(), wdcDate);
				// 자재소요원장 등록
				register.GridRegistration();
				
				// 부모창의 검색버튼 동작
				Page.RegisterClientScriptBlock("SEND", "<script>Search()</script>");
			}
			Session.Remove("dataSet");
			Session.Remove("dataGrid");
			RegisterStartupScript("","<script>window.close();</script>");
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			DataSet dsRowMaterial1 = (KIT_ERP.ProductionManagement.dsRowMaterial)Session["dataset"];
			UltraWebGrid1.DataSource = dsRowMaterial1.Tables["RowMaterial"];
			UltraWebGrid1.DataBind();				
			UltraWebGridExcelExporter1.Export(UltraWebGrid1);
		}
	}
}