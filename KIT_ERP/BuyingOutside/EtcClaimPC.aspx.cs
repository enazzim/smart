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
	/// EtcClaimPC에 대한 요약 설명입니다.
	/// </summary>
	public class EtcClaimPC : System.Web.UI.Page
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
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdYear;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdMon;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;

	
		private void Page_Load(object sender, System.EventArgs e)
		{
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
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.bt_Excel.Click += new System.EventHandler(this.bt_Excel_Click);
			this.lnk_Update.Click += new System.EventHandler(this.lnk_Update_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
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
			if(CSC1.BusinessRegistrationNum.Trim() == "")
				search = new Search("EtcClaimPC",wdcFromDate,wdcToDate,CSC1.Company,"");
			else
				search = new Search("EtcClaimPC",wdcFromDate,wdcToDate,"",CSC1.BusinessRegistrationNum);
			return search.DataSet_search();
		}

		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			Delete del = new Delete(UltraWebGrid1,"EtcClaimPC");
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

			if(MonthClosing())
			{
				
				KIT_ERP.Update up = new KIT_ERP.Update("EtcClaimPC",arr,UltraWebGrid1,Session["ID"].ToString());
				up.MainTableUpdate();
			}
			else
			{
				RegisterStartupScript("","<script>alert('월마감이 되어 수정이 불가능 합니다!');</script>");
			}
			
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

		
		/// <summary>
		/// 월마감 여부를 확인하는 함수
		/// </summary>
		/// <returns></returns>
		private bool MonthClosing()
		{
			int count = int.Parse(lb_RowSelectIndex.Value);
			int year = 0;
			int month = 0;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select ClosingYear, ClosingMonth From MCI_MT Where AffairDistinction = @Distinction";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@Distinction",SqlDbType.VarChar).Value = "구매";
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				year = int.Parse(dr[0].ToString());
				month = int.Parse(dr[1].ToString());
			}
			conn.Close();

			if(year < DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("ReceiptDate").Text).Year)
				return true;
			else if(year == DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("ReceiptDate").Text).Year)
			{
				if(month < DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("ReceiptDate").Text).Month)
					return true;
				else
					return false;
			}
			else
			{
				return false;
			}		

			
		}
	}
}
