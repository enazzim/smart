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
	/// UnitCost에 대한 요약 설명입니다.
	/// </summary>
	public class UnitCost : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button bt_Excel;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;

		private void Page_Load(object sender, System.EventArgs e)
		{
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
			
			string str = @"SELECT   UCI_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, UCI_MT.UnitCostDistinction, CI_MT.CompanyName, 
				UCI_MT.BusinessRegistrationNum, PUC_MT.SmallClassificationName as BeginProcess, PUC_MT.SmallClassificationCode as BeginCode,  PUC_MT_1.SmallClassificationName as EndProcess, PUC_MT_1.SmallClassificationCode as EndCode, UCI_MT.OrderRate, 
				UCI_MT.StandardUnitCost, UCI_MT.DiscountUnitCost, UCI_MT.BeginDate, 
				case UCI_MT.EndDate when '2076-06-06' then '' end as EndDate, UCI_MT.RecodingState, UCI_MT.RegistrationPerson, 
				UCI_MT.RegistrationPersonID, UCI_MT.RegistrationDate, UCI_MT.UpdatingPerson, UCI_MT.UpdatingPersonID, UCI_MT.UpdatingDate, 
				UCI_MT.UnitCostInfoIndex 
				FROM UCI_MT INNER JOIN II_MT ON II_MT.ItemNum = UCI_MT.ItemNum 
				INNER JOIN CI_MT ON UCI_MT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum 
				Inner join PUC_MT  on PUC_MT.SmallClassificationCode = UCI_MT.BeginProcessCode 
				inner join PUC_MT PUC_MT_1 on PUC_MT_1.SmallClassificationCode = UCI_MT.EndProcessCode 
				Where CI_MT.RecodingState = 1 and UCI_MT.RecodingState = 1 and II_MT.RecodingState = 1 order by UCI_MT.ItemNum";				
//				Where UCI_MT.UnitCostDistinction = '외주단가' and CI_MT.RecodingState = 1 and UCI_MT.RecodingState = 1 and II_MT.RecodingState = 1 order by UCI_MT.UnitCostDistinction";//UCI_MT.ItemNum";
			SqlCommand comm = new SqlCommand(str,conn);
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);
			return ds;
		}
	}
}
