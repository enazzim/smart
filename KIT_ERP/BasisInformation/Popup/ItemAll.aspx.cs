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
	/// ItemAll에 대한 요약 설명입니다.
	/// </summary>
	public class ItemAll : System.Web.UI.Page
	{
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid2;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
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

			UltraWebGrid2.DataSource = PageLoad();
			UltraWebGrid2.DisplayLayout.Pager.AllowPaging = false;
			UltraWebGrid2.DataBind();
			UltraWebGridExcelExporter1.Export(UltraWebGrid2);


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
			string str = @"select ItemNum, ItemDrawNum, ItemName,
					PropertyClassification, PUC_MT.SmallClassificationName as Unit, Standard, 
					case Texture WHEN 1 THEN '예' ELSE '아니오' END AS Texture,
					SupplementaryValueTaxRate,
					PUC_MT1.SmallClassificationName as StockUnit, 
					PUC_MT2.SmallClassificationName as BOMUnit, 
					PUC_MT3.SmallClassificationName as PurchaseUnit, 
					PUC_MT4.SmallClassificationName as SaleUnit, 
					case IOChackable WHEN 1 THEN '예' ELSE '아니오' END AS IOChackable, 
					case StockManagable WHEN 1 THEN '예' ELSE '아니오' END AS StockManagable, 
					case CheckDistinction WHEN 1 THEN '검사' ELSE '무검사' END AS CheckDistinction,  
					case OrderPlan WHEN 1 THEN '계산발주' ELSE '임의발주' END AS OrderPlan,
					PUC_MT5.SmallClassificationName as ItemType, 
					PUC_MT6.SmallClassificationName as MateralQuality, 
					ItemState, Maker, 
					PUC_MT7.SmallClassificationName as ItemClassification1, 
					PUC_MT8.SmallClassificationName as ItemClassification2, 
					PUC_MT9.SmallClassificationName as ItemClassification3,  
					PUC_MT10.SmallClassificationName as ItemClassification4, 
					Standard1, 
					PUC_MT11.SmallClassificationName as Unit1, 
					Standard2, 
					PUC_MT12.SmallClassificationName as Unit2, 
					Standard3, 
					PUC_MT13.SmallClassificationName as Unit3, 
					Standard4, 
					PUC_MT14.SmallClassificationName as Unit4, 
					ProductWeight, MaterialWeight, SupplyTerm, CompleteLeadTime, 
					case DomesticImportDistinction WHEN 1 THEN '내자' ELSE '외자' END AS DomesticImportDistinction, StandardUnitCost
					From II_MT
					left outer join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode 
					left outer join PUC_MT PUC_MT1 on II_MT.StockUnit = PUC_MT1.SmallClassificationCode
					left outer join PUC_MT PUC_MT2 on II_MT.BOMUnit = PUC_MT2.SmallClassificationCode
					left outer join PUC_MT PUC_MT3 on II_MT.PurchaseUnit = PUC_MT3.SmallClassificationCode
					left outer join PUC_MT PUC_MT4 on II_MT.SaleUnit = PUC_MT4.SmallClassificationCode
					left outer join PUC_MT PUC_MT5 on II_MT.ItemType = PUC_MT5.SmallClassificationCode
					left outer join PUC_MT PUC_MT6 on II_MT.MateralQuality = PUC_MT6.SmallClassificationCode
					left outer join PUC_MT PUC_MT7 on II_MT.ItemClassification1 = PUC_MT7.SmallClassificationCode
					left outer join PUC_MT PUC_MT8 on II_MT.ItemClassification2 = PUC_MT8.SmallClassificationCode
					left outer join PUC_MT PUC_MT9 on II_MT.ItemClassification3 = PUC_MT9.SmallClassificationCode
					left outer join PUC_MT PUC_MT10 on II_MT.ItemClassification4 = PUC_MT10.SmallClassificationCode
					left outer join PUC_MT PUC_MT11 on II_MT.Unit1 = PUC_MT11.SmallClassificationCode
					left outer join PUC_MT PUC_MT12 on II_MT.Unit2 = PUC_MT12.SmallClassificationCode
					left outer join PUC_MT PUC_MT13 on II_MT.Unit3 = PUC_MT13.SmallClassificationCode
					left outer join PUC_MT PUC_MT14 on II_MT.Unit4 = PUC_MT14.SmallClassificationCode
					Where II_MT.RecodingState = 1 order by ItemNum";
			SqlDataAdapter da = new SqlDataAdapter(str,conn);
			DataSet ds = new DataSet();
			da.Fill(ds);

			return ds;

			/*
			 * 
			 *  case DomesticImportDistinction WHEN 1 THEN '내자' ELSE '외자' END AS DomesticImportDistinction, 
					SafetyStockQuantity, OrderIntervalQuantity, MinOrderQuantity, StiffenDeep, ProductHeatTreatmentDescription, MaterialHeatTreatmentDescription, 
					MaterialHeatTreatmentRequestDegree, CuttingSpace, TariffRate, StandardUnitCost, MainPurchaseCompany, MainOutSideOrderCompany, MainSaleCompany, ChargePerson, ItemInfoIndex 
					
			*/
			
		}
	}
}
