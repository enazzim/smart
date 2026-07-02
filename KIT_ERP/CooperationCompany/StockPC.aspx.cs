using System;
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
using System.Configuration;

namespace KIT_ERP.CooperationCompany
{
	/// <summary>
	/// StockPC에 대한 요약 설명입니다.
	/// </summary>
	public class StockPC : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnInit;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Button btnExcel;
		protected System.Web.UI.WebControls.Button btnXML;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter uwgExcelExporter1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid3;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;

	
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.RawMaterials = true; //원자재 바인딩
			ItemSearchControl1.Commodity = true; //상품 바인딩
			ItemSearchControl1.HalfFinishedProducts = true; //반제품 바인딩
			ItemSearchControl1.Products = true; //제품 바인딩

			if(!Page.IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 협력사정보 > 재고현황';</script>");				

				//품목정보 초기화
				btnInit.Attributes.Add("onClick","ResetTextBox()");			
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
			this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.UltraWebGrid3.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid3_PageIndexChanged);
			this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		//초기화버튼 클릭
		private void btnInit_Click(object sender, System.EventArgs e)
		{
			
		}

		//엑셀내리기 버튼 클릭
		private void btnExcel_Click(object sender, System.EventArgs e)
		{
			this.uwgExcelExporter1.Export(UltraWebGrid3);
		}

		//검색버튼 클릭
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			KIT_ERP.Search search = new KIT_ERP.Search("StockPC",Session["id"].ToString(),ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName);			                  
			this.UltraWebGrid3.DataSource = search.DataSet_search();;
			this.UltraWebGrid3.DataBind();
		}

		
		//그리드 페이징기능 ...아래의 페이지 버튼을 눌렀을때...
		private void UltraWebGrid3_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid3.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			KIT_ERP.Search search = new KIT_ERP.Search("StockPC",Session["id"].ToString(),ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName);			                  
			this.UltraWebGrid3.DataSource = search.DataSet_search();;
			this.UltraWebGrid3.DataBind();
		}
	}
}
