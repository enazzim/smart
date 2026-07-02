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
using Infragistics.WebUI;
using Infragistics.WebUI.UltraWebGrid;

namespace KIT_ERP.EtcPresentCondition
{
	/// <summary>
	/// UnitCostView에 대한 요약 설명입니다.
	/// </summary>
	public class UnitCostView : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnExcel;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter uwgExcel;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwg;
		protected System.Web.UI.WebControls.DropDownList ddlDistinction;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.Commodity = true; //상품 바인딩				
			ItemSearchControl1.Products = true; //제품 바인딩
			ItemSearchControl1.Phantom = true;//펜텀 바인딩
			ItemSearchControl1.HalfFinishedProducts  = true;//반제품
			ItemSearchControl1.RawMaterials = true;//원자재
			ItemSearchControl1.SubMaterials = true;//부자재
			CSC1.UnitCostDistinction = "";

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
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.uwg.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.uwg_PageIndexChanged);
			this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			uwg.DisplayLayout.Pager.CurrentPageIndex = 1;
			uwg.DataSource = Search();
			uwg.DataBind();
		}

		private void uwg_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			uwg.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			uwg.DataSource = Search();
			uwg.DataBind();
		}

		private void btnExcel_Click(object sender, System.EventArgs e)
		{
			if(uwg.Rows.Count == 0)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('저장할 항목이 없습니다!');");
				Response.Write("</script>");
			}
			else
			{
				UltraWebGrid uwg_UCI = new UltraWebGrid();
				uwg_UCI = uwg;
				uwg_UCI.DisplayLayout.Pager.AllowPaging = false;
				
				uwg_UCI.DataSource = Search();
				
				uwg_UCI.DataBind();
				uwgExcel.Export(uwg_UCI);

			}
		}

		/// <summary>
		/// 검색 함수
		/// </summary>
		private DataSet Search()
		{
			
			KIT_ERP.Search search;
			if(ItemSearchControl1.hdItem.Trim() == "")
			{
				if(CSC1.BusinessRegistrationNum.Trim() == "")
					search = new Search("UnitCostView",ItemSearchControl1.hdItem,ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,CSC1.Company, "",ddlDistinction.SelectedItem.Value);
				else
					search = new Search("UnitCostView",ItemSearchControl1.hdItem,ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,"",CSC1.BusinessRegistrationNum,ddlDistinction.SelectedItem.Value);
			}
			else
			{
				if(CSC1.BusinessRegistrationNum.Trim() == "")
					search = new Search("UnitCostView",ItemSearchControl1.hdItem,ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,CSC1.Company, "",ddlDistinction.SelectedItem.Value);
				else
					search = new Search("UnitCostView",ItemSearchControl1.hdItem,ItemSearchControl1.ItemNum,"", "","", CSC1.BusinessRegistrationNum, ddlDistinction.SelectedItem.Value);
			}
			return  search.DataSet_search();
			
		}
	}
}
