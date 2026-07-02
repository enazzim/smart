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
using Infragistics.WebUI.UltraWebGrid;
using Infragistics.WebUI.WebCombo;

namespace KIT_ERP.BusinessManagement
{
	/// <summary>
	/// BusinessStorehouseInStorehousePC에 대한 요약 설명입니다.
	/// </summary>
	public class BusinessStorehouseInStorehousePC : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label searchTitle;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox TextBox2;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label3;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_FromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_ToDate;
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected System.Web.UI.WebControls.Button bt_Excel;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected System.Web.UI.HtmlControls.HtmlInputHidden chkAll;
		protected System.Web.UI.WebControls.Label Label2;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_quantity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Index;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowSelectIndex;
		protected System.Web.UI.WebControls.LinkButton lnk_Update;
		protected DataSet ds;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hd_year;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hd_month;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
	
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
			if(!Page.IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 영업관리 > 영업창고입고현황';</script>");

				bt_Delete.Attributes.Add("onClick", "return confirm('선택한 항목을 삭제하시겠습니까?');");
				bt_Clear.Attributes.Add("onClick", "ResetTextBox()");
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
			this.lnk_Update.Click += new System.EventHandler(this.lnk_Update_Click);
			this.bt_Clear.Click += new System.EventHandler(this.bt_Clear_Click);
			this.bt_Search.Click += new System.EventHandler(this.bt_Search_Click);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.bt_Excel.Click += new System.EventHandler(this.bt_Excel_Click);
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
			ds = search();
			UltraWebGrid1.DataSource = ds;
			UltraWebGrid1.DataBind();
		}

		/// <summary>
		/// 검색함수
		/// </summary>
		/// <returns></returns>
		private DataSet search()
		{
			Search aa = new Search("BusinessStorehouseInStorehousePC",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,wdc_FromDate,wdc_ToDate);
			return aa.DataSet_search();
			
		}

		/// <summary>
		/// 초기화버튼
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Clear_Click(object sender, System.EventArgs e)
		{			
			wdc_FromDate.Value = "";
			wdc_ToDate.Value = "";
		}

		/// <summary>
		/// 삭제버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
				Delete del = new Delete(UltraWebGrid1,"BusinessStorehouseInStorehousePC");
				del.MainRowDelete();
			
			ds =search();
			UltraWebGrid1.DataSource = ds;
			UltraWebGrid1.DataBind();
		}

		

		/// <summary>
		/// 페이지 이동
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
			uwg_OS.Columns.FromKey("chk").Hidden = true;
			uwg_OS.DataSource = search();
			uwg_OS.DataBind();
			UltraWebGridExcelExporter1.Export(uwg_OS);
		}

		/// <summary>
		/// 수정버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lnk_Update_Click(object sender, System.EventArgs e)
		{
			ArrayList arr = new ArrayList();
			arr.Add(decimal.Parse(lb_quantity.Value));//변경전 수량
			arr.Add(int.Parse(lb_Index.Value));//기존의 영업창고번호
			arr.Add(int.Parse(hd_year.Value));//기존의 입고년도
			arr.Add(int.Parse(hd_month.Value));//기존의 입고월
			arr.Add(int.Parse(lb_RowSelectIndex.Value));//선택된 그리드인덱스
			
			if(!MonthClosing())
			{
				Update up = new Update("BusinessStorehouseInStorehousePC",arr,UltraWebGrid1,Session["ID"].ToString());
				up.MainTableUpdate();		
			}
			else
			{
				RegisterStartupScript("","<script>alert('월마감이 되어 수정이 불가능 합니다!');</script>");
			}
			
			UltraWebGrid1.DataSource = search();
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
			comm.Parameters.Add("@Distinction",SqlDbType.VarChar).Value = "영업";
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				year = int.Parse(dr[0].ToString());
				month = int.Parse(dr[1].ToString());
			}
			conn.Close();

			if(year < DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("InStoreDate").Text).Year)
				return true;
			else if(year == DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("InStoreDate").Text).Year)
			{
				if(month < DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("InStoreDate").Text).Month)
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
