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

namespace KIT_ERP.CooperationCompany
{
	/// <summary>
	/// DeliveryPC에 대한 요약 설명입니다.
	/// </summary>
	public class DeliveryPC : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnInit;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Button btnExcel;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcStartDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgD_PC;
		protected System.Web.UI.WebControls.Button Button3;
	

		//페이지 로드시
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			if(!Page.IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 협력사정보 > 납품현황';</script>");

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
			this.uwgD_PC.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.uwgD_PC_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		//초기화 버튼 클릭시
		private void btnInit_Click(object sender, System.EventArgs e)
		{
			this.wdcStartDate.Value = "";
			this.wdcEndDate.Value = "";
		}


		//검색버튼 클릭시
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			KIT_ERP.Search search = new KIT_ERP.Search("DeliveryPC",wdcStartDate,wdcEndDate,Session["ID"].ToString());			
			this.uwgD_PC.DataSource = search.DataSet_search();
			this.uwgD_PC.DataBind();
		}


		//그리드 페이징기능 ...아래의 페이지 버튼을 눌렀을때...
		private void uwgD_PC_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			uwgD_PC.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			KIT_ERP.Search search = new KIT_ERP.Search("DeliveryPC",wdcStartDate,wdcEndDate,Session["ID"].ToString());	
			this.uwgD_PC.DataSource = search.DataSet_search();
			this.uwgD_PC.DataBind();
		}
	}
}
