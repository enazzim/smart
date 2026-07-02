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
using Infragistics.WebUI.UltraWebGrid;

namespace KIT_ERP.BusinessManagement
{
	/// <summary>
	/// StorehouseMovingPC에 대한 요약 설명입니다.
	/// </summary>
	public class StorehouseMovingPC : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label searchTitle;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_FromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_ToDate;
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected System.Web.UI.WebControls.Button bt_Excel;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.WebControls.LinkButton lnk_Update;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowSelectIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Quantity;
		protected System.Web.UI.WebControls.Label Label2;
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

			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 영업관리 > 창고이동현황';</script>");

				bt_Delete.Attributes.Add("onclick","return OK('선택항목을 삭제 '); ");
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
			this.bt_Clear.Click += new System.EventHandler(this.bt_Clear_Click);
			this.bt_Search.Click += new System.EventHandler(this.bt_Search_Click);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.bt_Excel.Click += new System.EventHandler(this.bt_Excel_Click);
			this.lnk_Update.Click += new System.EventHandler(this.lnk_Update_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// 검색버튼 클릭
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
			Search search = new Search("StorehouseMovingPC",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,1,wdc_FromDate,wdc_ToDate);
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

		/// <summary>
		/// 삭제버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			Delete del = new Delete(UltraWebGrid1,"StorehouseMovingPC");
			del.MainRowDelete();
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
		}

		//업데이트
		private void lnk_Update_Click(object sender, System.EventArgs e)
		{
			ArrayList arr = new ArrayList();
			arr.Add(decimal.Parse(lb_Quantity.Value));//이전수량
			arr.Add(UltraWebGrid1.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("MovingStorehoseName").Text);//이동창고명이지만 실제는 이동창고번호가 들어간다
			//이동할 창고번호
			if(UltraWebGrid1.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("MovingStorehoseName").Text == "영업1창고")
				arr.Add(1);
			else if(UltraWebGrid1.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("MovingStorehoseName").Text == "영업2창고")
				arr.Add(2);
			else
				arr.Add(3);

			arr.Add(int.Parse(lb_RowSelectIndex.Value));//선택된 그리드 인덱스

			Update up = new Update("StorehouseMovingPC",arr,UltraWebGrid1,Session["ID"].ToString());
			up.MainTableUpdate();
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
		}

		private void bt_Clear_Click(object sender, System.EventArgs e)
		{
			wdc_FromDate.Value = "";
			wdc_ToDate.Value ="";
		}
	}
}
