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
using System.Data.SqlClient;
using System.Configuration;
using Infragistics.WebUI.UltraWebGrid;

namespace KIT_ERP.BusinessManagement
{
	/// <summary>
	/// AddItemInStorePC에 대한 요약 설명입니다.
	/// </summary>
	public class AddItemInStorePC : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btSearch;
		protected System.Web.UI.WebControls.Button btDelete;
		protected System.Web.UI.WebControls.Button btExcel;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcToDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcFromDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowIndex;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Quantity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdYear;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdMon;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReasonCode;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Reason;
		protected System.Web.UI.WebControls.LinkButton lnk_Update;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Index;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			ItemSearchControl1.Products = true; //제품 바인딩
			ItemSearchControl1.HalfFinishedProducts = true;//반제품 바인딩
			ItemSearchControl1.RawMaterials = true; //원자재

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			if(!Page.IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 영업관리 > 기타입출고현황';</script>");


				btDelete.Attributes.Add("onclick","return OK('선택항목을 삭제');");				
			}
		}

		/// <summary>
		/// 편집모드에서 입출고사유 바인딩시키는 데이터셋을 리턴
		/// </summary>
		/// <returns></returns>
		protected DataSet AddItemStore
		{
			get
			{
				Update reason = new Update(Session["ID"].ToString());
				DataSet ds_Reson = reason.AddItemStore();
				
				return ds_Reson;
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
			this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.btExcel.Click += new System.EventHandler(this.btExcel_Click);
			this.lnk_Update.Click += new System.EventHandler(this.lnk_Update_Click);
			this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btSearch_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
		}
	

		private DataSet Search()
		{
			Search search;
			search = new Search("AddItemInStorePC",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName, wdcFromDate, wdcToDate);
			return search.DataSet_search();
			
		}

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
		}

		private void btExcel_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid uwg = new UltraWebGrid();
			uwg = UltraWebGrid1;
			uwg.DisplayLayout.Pager.AllowPaging = false;
			uwg.Columns.FromKey("chk").Hidden = true;
			uwg.DataSource = Search();
			uwg.DataBind();
			UltraWebGridExcelExporter1.Export(uwg);
		}

		private void btDelete_Click(object sender, System.EventArgs e)
		{
			Delete del = new Delete(UltraWebGrid1,"AddItemInStorePC");
			del.MainRowDelete();
			
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
		}

		private void lnk_Update_Click(object sender, System.EventArgs e)
		{
			
			ArrayList arr = new ArrayList();
			arr.Add(decimal.Parse(lb_Quantity.Value.Trim()));//기존수량
			arr.Add(lb_ReasonCode.Value.Trim());//변경 입고 사유코드
			arr.Add(lb_Reason.Value.Trim());//변경 입고 사유
			arr.Add(hdYear.Value);
			arr.Add(hdMon.Value);

			//그리드 선택 인덱스
			arr.Add(int.Parse(lb_Index.Value.Trim()));
			
			if(MonthClosing())
			{
				Update up = new Update("AddItemInStorePC",arr,UltraWebGrid1,Session["ID"].ToString());
				up.MainTableUpdate();				
			}
			else
			{
				RegisterStartupScript("","<script>alert('월마감이 되어 수정이 불가능 합니다!');</script>");
			}
            
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
		}


		/// <summary>
		/// 월마감 여부를 확인하는 함수
		/// </summary>
		/// <returns></returns>
		private bool MonthClosing()
		{
			int count = int.Parse(lb_Index.Value.Trim());
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

			
			if(year < DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("InstoreDate").Text).Year)
				return true;
			else if(year == DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("InstoreDate").Text).Year)
			{
				if(month < DateTime.Parse(UltraWebGrid1.Rows[count].Cells.FromKey("InstoreDate").Text).Month)
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
