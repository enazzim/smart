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

namespace KIT_ERP.BuyingOutside
{
	/// <summary>
	/// ClaimPC에 대한 요약 설명입니다.
	/// </summary>
	public class ClaimPC : System.Web.UI.Page
	{
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcFromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcToDate;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button bt_Excel;
		protected System.Web.UI.WebControls.LinkButton lnk_Update;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowSelectIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Cost;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdYear;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdMon;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
	


		/// <summary>
		/// 클레임원인
		/// </summary>
		protected DataSet ClaimCause
		{
			get
			{
				Update source = new Update(Session["ID"].ToString());
				DataSet ds_Cause = source.ClaimCause();			
				return ds_Cause;
			}
		}

		/// <summary>
		/// 클레임현상
		/// </summary>
		protected DataSet ClaimStatus
		{
			get
			{
				Update source = new Update(Session["ID"].ToString());
				DataSet ds_Status = source.ClaimStatus();
				return ds_Status;
			}
		}





		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			ItemSearchControl1.Commodity = true; //상품 바인딩				
			ItemSearchControl1.Products = true; //제품 바인딩
			ItemSearchControl1.HalfFinishedProducts = true;//반제품 바인딩
			ItemSearchControl1.RawMaterials = true;//원자재 바인딩
			CSC1.UnitCostDistinction = "구매외주";

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				bt_Delete.Attributes.Add("onClick","return confirm('선택한 항목을 삭제하시겠습니까?');");
				
				
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
			this.bt_Excel.Click += new System.EventHandler(this.bt_Excel_Click);
			this.lnk_Update.Click += new System.EventHandler(this.lnk_Update_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void bt_Search_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DataSource = Find();
			UltraWebGrid1.DataBind();
		}

		/// <summary>
		/// 검색함수
		/// </summary>
		/// <returns></returns>
		private DataSet Find()
		{
			Search search;
			if(ItemSearchControl1.hdItem.Trim() == "")
                search = new Search("ClaimPC",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,wdcFromDate,wdcToDate,CSC1.Company,CSC1.BusinessRegistrationNum);
			else
				search = new Search("ClaimPC",ItemSearchControl1.ItemNum, "", "",wdcFromDate,wdcToDate,CSC1.Company,CSC1.BusinessRegistrationNum);
			return search.DataSet_search();
		}

		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			Delete del = new Delete(UltraWebGrid1,"ClaimPC");
			del.MainRowDelete();
			UltraWebGrid1.DataSource = Find();
			UltraWebGrid1.DataBind();
		}

		private void lnk_Update_Click(object sender, System.EventArgs e)
		{
			ArrayList arr = new ArrayList();
			arr.Add(decimal.Parse(lb_Cost.Value));//기존의 금액
			arr.Add(int.Parse(hdYear.Value));//기존년도
            arr.Add(int.Parse(hdMon.Value));//기존월
			arr.Add(int.Parse(lb_RowSelectIndex.Value));//선택 그리드 Row인덱스

			KIT_ERP.Update up = new KIT_ERP.Update("ClaimPC",arr,UltraWebGrid1,Session["ID"].ToString());
			up.MainTableUpdate();
			UltraWebGrid1.DataSource = Find();
			UltraWebGrid1.DataBind();
		}

		private void bt_Excel_Click(object sender, System.EventArgs e)
		{
			if(UltraWebGrid1.Rows.Count == 0)
			{
				RegisterStartupScript("","<script>alert('저장할 항목이 없습니다!');</script>");
			}
			else
			{
				Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwg_CL = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
				uwg_CL = UltraWebGrid1;
				uwg_CL.DisplayLayout.Pager.AllowPaging = false;
				uwg_CL.Columns.FromKey("chk").Hidden = true;
				uwg_CL.DataSource = Find();
				uwg_CL.DataBind();
				UltraWebGridExcelExporter1.Export(uwg_CL);

			}
		}

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = Find();
			UltraWebGrid1.DataBind();
		}
	}
}
