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
	/// ItemOrganization에 대한 요약 설명입니다.
	/// </summary>
	public class ItemOrganization : System.Web.UI.Page
	{
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid2;
		protected System.Web.UI.WebControls.Button bt_Excel;
	
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
			conn.Open();
			
			string str = @"SELECT   IOI_MT.ParentItemNum, IOI_MT.ChildItemNum, II_MT_1.ItemDrawNum,
				II_MT_1.ItemName ,II_MT.Standard, IOI_MT.NeedQuantityNumerator,
				IOI_MT.NeedQuantityDenominator, case IOI_MT.ProcessManagement  when 1 then '예' else '아니오' end as ProcessManagement,
				case IOI_MT.SubDivision   when 1 then '예' else '아니오' end as SubDivision,p.SmallClassificationName as SupplyDivision,
				PUC_MT.SmallClassificationName as BOMUnit, IOI_MT.BeginDate, IOI_MT.EndDate,
				IOI_MT.RecodingState, IOI_MT.RegistrationPerson, IOI_MT.RegistrationPersonID,
				IOI_MT.RegistrationDate, IOI_MT.UpdatingPerson, IOI_MT.UpdatingPersonID,
				IOI_MT.ItemOrganizationInfoIndex, IOI_MT.UpdatingDate
				FROM IOI_MT INNER JOIN 
				II_MT ON IOI_MT.ParentItemNum = II_MT.ItemNum INNER JOIN 
				II_MT II_MT_1 ON IOI_MT.ChildItemNum = II_MT_1.ItemNum left outer JOIN 
				PUC_MT ON IOI_MT.BOMUnit = PUC_MT.SmallClassificationCode left outer join
				PUC_MT p on IOI_MT.SupplyDivision = p.SmallClassificationCode
				where II_MT.RecodingState = 1 and II_MT_1.RecodingState = 1 and IOI_MT.RecodingState = 1 order by IOI_MT.ParentItemNum";
			
			SqlDataAdapter da = new SqlDataAdapter(str,conn);
			DataSet ds = new DataSet();
			da.Fill(ds);
			
			return ds;
		}
	}
}
