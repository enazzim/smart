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
	/// RealProcessSequence에 대한 요약 설명입니다.
	/// </summary>
	public class RealProcessSequence : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button bt_Excel;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				
				UltraWebGrid1.DataSource = PageLoad();
				UltraWebGrid1.DataBind();
			}
		}

		private DataSet PageLoad()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			//conn.Open();
			string str = @"SELECT   RPSI_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName,
				RPSI_MT.ProcessSequenceNum, RPSI_MT.ProcessCode, PUC_MT.SmallClassificationName, RPSI_MT.WorkDistinction, RPSI_MT.WCName, 
				RPSI_MT.OutsideOrderRate, RPSI_MT.ProgressRate, RPSI_MT.LeadTime, RPSI_MT.EtcText, RPSI_MT.RecodingState, RPSI_MT.RegistrationPerson, 
				RPSI_MT.RegistrationPersonID, RPSI_MT.RegistrationDate, RPSI_MT.UpdatingPerson, RPSI_MT.UpdatingPersonID, RPSI_MT.UpdatingDate, 
				RPSI_MT.RealProcessSequenceInfoIndex FROM      RPSI_MT INNER JOIN II_MT ON RPSI_MT.ItemNum = II_MT.ItemNum INNER JOIN 
				PUC_MT ON RPSI_MT.ProcessCode = PUC_MT.SmallClassificationCode 
				where II_MT.RecodingState = 1 and RPSI_MT.RecodingState = 1 order by RPSI_MT.ItemNum , RPSI_MT.ProcessSequenceNum";
			SqlDataAdapter da = new SqlDataAdapter(str,conn);
			DataSet ds = new DataSet();
			da.Fill(ds);
			//conn.Close();

			return ds;
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
	}
}
