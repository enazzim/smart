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
	/// WorkStandard에 대한 요약 설명입니다.
	/// </summary>
	public class WorkStandard : System.Web.UI.Page
	{
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.WebControls.Button bt_Excel;
	
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
			string str = @"SELECT WSI_MT.ItemNum, WSI_MT.ProcessSequenceNum, PUC_MT.SmallClassificationName, WSI_MT.ProcessCode, II_MT.ItemDrawNum, 
				II_MT.ItemName, WSI_MT.WCName, WSI_MT.PriorityOrder, WSI_MT.MainWorkerID, WSI_MT.MainWorker, WSI_MT.ToolName1, WSI_MT.JigName1, 
				WSI_MT.ToolName2, WSI_MT.JigName2, WSI_MT.ToolName3, WSI_MT.JigName3, WSI_MT.SetupTime, 
				WSI_MT.RealProcessingTime, WSI_MT.SpaceTime, WSI_MT.StandardTime, WSI_MT.WaitTime, WSI_MT.LotSize, WSI_MT.Cavity, WSI_MT.RecodingState, 
				WSI_MT.RegistrationPerson, WSI_MT.RegistrationPersonID, WSI_MT.RegistrationDate, WSI_MT.UpdatingPerson, WSI_MT.UpdatingPersonID, 
				WSI_MT.UpdatingDate, WSI_MT.WorkStandardInfoIndex FROM WSI_MT INNER JOIN II_MT ON WSI_MT.ItemNum = II_MT.ItemNum INNER JOIN 
				PUC_MT ON WSI_MT.ProcessCode = PUC_MT.SmallClassificationCode Where WSI_MT.RecodingState = 1 order by WSI_MT.ItemNum, WSI_MT.ProcessSequenceNum";
			SqlDataAdapter da = new SqlDataAdapter(str,conn);
			DataSet ds = new DataSet();
			da.Fill(ds);

			return ds;
		}
	}
}
