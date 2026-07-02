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
	/// Equipment에 대한 요약 설명입니다.
	/// </summary>
	public class Equipment : System.Web.UI.Page
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

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = PageLoad();
			UltraWebGrid1.DataBind();
		}

		private void bt_Excel_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DataSource = PageLoad();
			UltraWebGrid1.DisplayLayout.Pager.AllowPaging = false;
			UltraWebGrid1.DataBind();
			UltraWebGridExcelExporter1.Export(UltraWebGrid1);
		}

		private DataSet PageLoad()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = @"Select EquipmentNum, EquipmentName, PUC_MT.SmallClassificationName as EquipmentClassification, 
					Standard, Capacity, ElectricCapacity, UnitTimeUseCost, PUC_MT1.SmallClassificationName as Unit, WCName, InjectionStaffNum, PUC_MT_1.SmallClassificationName as Location, OccupancyArea, 
					case BuyingDate when '2076-06-06' then '' else BuyingDate end as BuyingDate, 
					BuyingCost, InstrumentNum, EquipmentState, 
					case CheckDate when '2076-06-06' then '' else CheckDate end as CheckDate, 
					case ValidPeriod when '2076-06-06' then '' else ValidPeriod end as ValidPeriod, 
					CheckAgency, ReadyTime, Cavity, ValidityYear, DesignShot, FirstShot, TotalShot, WorkShot, ManagePeriod1, 
					case ManageDate1 when '2076-06-06' then '' else ManageDate1 end  as ManageDate1, ManagePeriod2, 
					case ManageDate2 when '2076-06-06' then '' else ManageDate2 end as ManageDate2, ManagePeriod3, 
					case ManageDate3 when '2076-06-06' then '' else ManageDate3 end as ManageDate3, ManagePeriod4, 
					case ManageDate4 when '2076-06-06' then '' else ManageDate4 end as ManageDate4,EquipmentInfoIndex
					From EI_MT inner join PUC_MT on EI_MT.EquipmentClassification = PUC_MT.SmallClassificationCode
					left outer join PUC_MT PUC_MT_1 on EI_MT.Location = PUC_MT_1.SmallClassificationCode
					left outer join PUC_MT PUC_MT1 on EI_MT.Unit = PUC_MT1.SmallClassificationCode
					where EI_MT.RecodingState =1 order by EquipmentName";
			SqlDataAdapter da = new SqlDataAdapter(str,conn);
			DataSet ds = new DataSet();
			da.Fill(ds);
			return ds;
		}
	}
}
