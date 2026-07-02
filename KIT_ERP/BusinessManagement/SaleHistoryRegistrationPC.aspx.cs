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

namespace KIT_ERP.BusinessManagement
{
	/// <summary>
	/// SaleHistoryRegistrationPC에 대한 요약 설명입니다.
	/// </summary>
	public class SaleHistoryRegistrationPC : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label searchTitle;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label7;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected System.Web.UI.WebControls.Button bt_Excel;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowSelectIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_quantity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_totalcost;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_tax;
		protected System.Web.UI.WebControls.LinkButton lnk_Update;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdYear;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdMon;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcFromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcToDate;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.Commodity = true; //상품 바인딩				
			ItemSearchControl1.Products = true; //제품 바인딩
			ItemSearchControl1.HalfFinishedProducts = true;//반제품 바인딩
			ItemSearchControl1.RawMaterials = true;//원자재 바인딩
			CSC1.UnitCostDistinction = "수주거래처";

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
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
			this.bt_Search.Click += new System.EventHandler(this.bt_Search_Click);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.bt_Excel.Click += new System.EventHandler(this.bt_Excel_Click);
			this.lnk_Update.Click += new System.EventHandler(this.lnk_Update_Click);
			this.bt_Update.Click += new System.EventHandler(this.bt_Update_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// 검색버튼
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Search_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
		}

		private DataSet Search()
		{
			Search search = new Search("SaleHistoryRegistrationPC",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,wdcFromDate,wdcToDate,CSC1.Company,CSC1.BusinessRegistrationNum);
			return search.DataSet_search();
		}

		/// <summary>
		/// 페이지 이동
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
		}

		/// <summary>
		/// 엑셀저장버튼
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Excel_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid uwg_Excel = new UltraWebGrid();
			uwg_Excel = UltraWebGrid1;
			uwg_Excel.DisplayLayout.Pager.AllowPaging = false;
			uwg_Excel.Columns.FromKey("chk").Hidden = true;
			uwg_Excel.DataSource = Search();
			uwg_Excel.DataBind();
			UltraWebGridExcelExporter1.Export(uwg_Excel);
		}

		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			Delete del = new Delete(UltraWebGrid1,"SaleHistoryRegistrationPC",Session["UserName"].ToString(),Session["ID"].ToString());
			del.MainRowDelete();
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
		}

		private void lnk_Update_Click(object sender, System.EventArgs e)
		{
			ArrayList arr = new ArrayList();
			arr.Add(decimal.Parse(lb_quantity.Value));//이전 적합수량
			arr.Add(decimal.Parse(lb_totalcost.Value));//이전 총 금액
			arr.Add(decimal.Parse(lb_tax.Value));//이전 부가세율
			arr.Add(hdYear.Value);
			arr.Add(hdMon.Value);
			arr.Add(int.Parse(lb_RowSelectIndex.Value));//그리드 인덱스

			Update up = new Update("SaleHistoryRegistrationPC",arr,UltraWebGrid1,Session["ID"].ToString());
			up.MainTableUpdate();

			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();

		}

		private void bt_Update_Click(object sender, System.EventArgs e)
		{
			bool check = true;
			foreach(UltraGridRow row in UltraWebGrid1.Rows)
			{
				if(row.Cells.FromKey("chk").Value == null)
					row.Delete();
			}
			if(UltraWebGrid1.Rows.Count == 0)
			{
				check = false;
				RegisterStartupScript("","<script>alert('선택한 항목이 없습니다!');</script>");
			}
			else
			{
				foreach(UltraGridRow row in UltraWebGrid1.Rows)
				{
					if(row.Cells.FromKey("BusinessRegistrationNum").Text != UltraWebGrid1.Rows[0].Cells.FromKey("BusinessRegistrationNum").Text)
					{
						RegisterStartupScript("","<script>alert('다른거래처가 존재하여 계산서발행이 불가능합니다!');</script>");
						check = false;
						break;
					}
				}
			}

			if(check)
			{
				Session["UWG"] = UltraWebGrid1;
				RegisterStartupScript("","<script>window.open('./Popup/Tex.aspx','','width=720,scrollbars=yes,menubar=yes,status=no,toolbar=yes,center=yes');</script>");
			}


			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();

		}

	}

}
