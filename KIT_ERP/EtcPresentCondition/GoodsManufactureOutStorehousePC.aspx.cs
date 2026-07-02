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
using Infragistics.WebUI;
using Infragistics.WebUI.WebCombo;
using Infragistics.WebUI.UltraWebGrid;

namespace KIT_ERP.EtcPresentCondition
{
	/// <summary>
	/// GoodsManufactureOutStorehousePC에 대한 요약 설명입니다.
	/// </summary>
	public class Form : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button bt_Search;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button bt_Excel;
		protected System.Web.UI.WebControls.DropDownList dlProgressState;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcFromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcToDate;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.Commodity = true; //상품 바인딩
			ItemSearchControl1.Products = true; //제품 바인딩
			ItemSearchControl1.Phantom = true;//펜텀 바인딩

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.

			if(!Page.IsPostBack)
			{
				
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
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.bt_Excel.Click += new System.EventHandler(this.bt_Excel_Click);
			this.bt_Search.Click += new System.EventHandler(this.bt_Search_Click);
			this.ID = "Form";
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void bt_Search_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			UltraWebGrid1.DataSource = search();
			UltraWebGrid1.DataBind();			
			
		}



		private DataSet search()
		{
			Search search;
			if(ItemSearchControl1.hdItem.Trim() == "")
				search = new Search("GoodsManufactureOutStorehousePC",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,wdcFromDate,wdcToDate,CSC1.Company,CSC1.BusinessRegistrationNum,dlProgressState.SelectedItem.Value);
			else
				search = new Search("GoodsManufactureOutStorehousePC",ItemSearchControl1.ItemNum, "", "",wdcFromDate,wdcToDate,CSC1.Company,CSC1.BusinessRegistrationNum,dlProgressState.SelectedItem.Value);
			return search.DataSet_search();
		}

		/// <summary>
		/// 페이지 이동을 위할때
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = search();
			UltraWebGrid1.DataBind();

		}


		/// <summary>
		/// 엑셀저장버튼
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Excel_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid uwg_OS = new UltraWebGrid();
			uwg_OS = UltraWebGrid1;
			uwg_OS.DisplayLayout.Pager.AllowPaging = false;
			uwg_OS.DataSource = search();
			uwg_OS.DataBind();
			UltraWebGridExcelExporter1.Export(uwg_OS);
		}

		/// <summary>
		/// 삭제버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			Delete del = new Delete(UltraWebGrid1,"GoodsManufactureOutStorehousePC");
			del.MainRowDelete();
			UltraWebGrid1.DataSource = search();
			UltraWebGrid1.DataBind();
		}
	}
}
