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

using Infragistics.WebUI;
using Infragistics.WebUI.UltraWebGrid;

namespace KIT_ERP.BasisInformation.Popup
{
	/// <summary>
	/// CompanyAll에 대한 요약 설명입니다.
	/// </summary>
	public class CompanyAll : System.Web.UI.Page
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

			string str = @"Select case ReceiveingOrderCompany When 1 then '예' else '아니오' end as ReceiveingOrderCompany,
						case OutSideOrderCompany When 1 then '예' else '아니오' end as OutSideOrderCompany, 
						case BuyingCompany When 1 then '예' else '아니오' end as BuyingCompany,
						case CostCompany When 1 then '예' else '아니오' end as CostCompany, 
						CompanyName, PresidentName, BusinessRegistrationNum, CorporationRegistrationNum, BusinessCompanyAddress, 
						TaxBillAddress, HomepageAddress, BusinessClassification, BusinessItem, 
						case CurrentTradeState When 1 then '예' else '아니오' end as CurrentTradeState, 
						case SupplementaryValueTax When 1 then '예' else '아니오' end as SupplementaryValueTax, 
						SupplementaryValueTaxRate, TelephoneNum, FaxNum, 
						PUC_MT.SmallClassificationName as TradeClassification1, 
						PUC_MT1.SmallClassificationName as TradeClassification2, 
						PUC_MT2.SmallClassificationName as TradeClassification3, 
						SaleStandardDate, SaleStandardBillDate, BillApprovalStandard, FixPeriodCollectMoneyDate1, FixPeriodCollectMoneyDate2, 
						CompanyPersonInCharge, 
						CompanyPersonInChargeEmail, CompanyInfoIndex
						From CI_MT 
						left outer join PUC_MT on CI_MT.TradeClassification1 = PUC_MT.SmallClassificationCode
						left outer join PUC_MT PUC_MT1 on CI_MT.TradeClassification2 = PUC_MT1.SmallClassificationCode
						left outer join PUC_MT PUC_MT2 on CI_MT.TradeClassification3 = PUC_MT2.SmallClassificationCode
						Where CI_MT.RecodingState = 1 order by CompanyName";
			SqlDataAdapter da = new SqlDataAdapter(str,conn);
			DataSet ds = new DataSet();
			da.Fill(ds);
			//conn.Close();

			return ds;
			
		}
	}
}
