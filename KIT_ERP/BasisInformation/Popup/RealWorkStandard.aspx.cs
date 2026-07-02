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

namespace KIT_ERP.BasisInformation.Popup
{
	/// <summary>
	/// RealWorkStandard에 대한 요약 설명입니다.
	/// </summary>
	public class RealWorkStandard : System.Web.UI.Page
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
			//conn.Open();
			string str = @"SELECT RWSI_MT.ItemNum, RWSI_MT.ProcessSequenceNum, PUC_MT.SmallClassificationName, RWSI_MT.ProcessCode, II_MT.ItemDrawNum, 
				II_MT.ItemName, RWSI_MT.WCName, RWSI_MT.PriorityOrder, RWSI_MT.MainWorkerID, RWSI_MT.MainWorker, RWSI_MT.ToolName1, RWSI_MT.JigName1, 
				RWSI_MT.ToolName2, RWSI_MT.JigName2, RWSI_MT.ToolName3, RWSI_MT.JigName3, RWSI_MT.SetupTime, 
				RWSI_MT.RealProcessingTime, RWSI_MT.SpaceTime, RWSI_MT.StandardTime, RWSI_MT.WaitTime, RWSI_MT.LotSize, RWSI_MT.Cavity, RWSI_MT.RecodingState, 
				RWSI_MT.RegistrationPerson, RWSI_MT.RegistrationPersonID, RWSI_MT.RegistrationDate, RWSI_MT.UpdatingPerson, RWSI_MT.UpdatingPersonID, 
				RWSI_MT.UpdatingDate, RWSI_MT.RealWorkStandardInfoIndex FROM RWSI_MT INNER JOIN II_MT ON RWSI_MT.ItemNum = II_MT.ItemNum INNER JOIN 
				PUC_MT ON RWSI_MT.ProcessCode = PUC_MT.SmallClassificationCode Where RWSI_MT.RecodingState = 1 order by RWSI_MT.ItemNum, RWSI_MT.ProcessSequenceNum";
			SqlDataAdapter da = new SqlDataAdapter(str,conn);
			DataSet ds = new DataSet();
			da.Fill(ds);

			return ds;
		}
	}
}
